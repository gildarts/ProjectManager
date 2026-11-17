using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using FISCA.DSAClient;

namespace ProjectManager.Project
{
    /// <summary>
    /// 專案總覽：可搜尋的專案清單，右側預覽專案內的 Contract 與 UDT 資料表。
    /// 版面直接用程式碼排，沒有設計工具檔。
    /// </summary>
    class ProjectBrowserForm : Form
    {
        // 本機設定（lucifer.config）裡用到的名稱
        private const string CacheStorageName = "ProjectBrowserCache";
        private const string FavoriteStorageName = "ProjectFavorites";
        private const string LastOpenedStorageName = "ProjectLastOpened";

        private const int ColFavorite = 0;
        private const int ColName = 1;
        private const int ColSite = 2;
        private const int ColContracts = 3;
        private const int ColTables = 4;
        private const int ColLastOpened = 5;

        private class ProjectInfo
        {
            public string Name;
            public string Site = string.Empty;
            public string User = string.Empty;
            public List<string> Contracts = new List<string>();
            public List<string> Tables = new List<string>();
            public bool Loaded;
            public bool LoadFailed;
            public DateTime LastOpened = DateTime.MinValue;
            public bool Favorite;
        }

        private readonly List<ProjectInfo> _projects = new List<ProjectInfo>();
        private readonly Dictionary<string, ListViewItem> _items = new Dictionary<string, ListViewItem>();
        private readonly string _currentProject;

        private TextBox txtSearch;
        private Label lblStatus;
        private ListView lvProjects;
        private TreeView tvPreview;
        private Button btnFavorite;
        private Button btnOpen;
        private Button btnCancel;

        private int _sortColumn = ColLastOpened;
        private bool _sortAscending = false;
        private volatile bool _closed;

        /// <summary>
        /// 使用者選定要開啟的專案名稱。
        /// </summary>
        internal string SelectedProject { get; private set; }

        /// <summary>
        /// 顯示專案總覽；使用者選了專案回傳名稱，取消回傳 null。
        /// </summary>
        internal static string Prompt(IWin32Window owner, IEnumerable<string> projectNames, string currentProject)
        {
            using (ProjectBrowserForm form = new ProjectBrowserForm(projectNames, currentProject))
            {
                if (form.ShowDialog(owner) == DialogResult.OK)
                    return form.SelectedProject;
                return null;
            }
        }

        /// <summary>
        /// 記錄專案的最近開啟時間（存本機）。
        /// </summary>
        internal static void MarkOpened(string projectName)
        {
            if (string.IsNullOrWhiteSpace(projectName)) return;
            try
            {
                MainForm.Storage.SetPropertyValues(LastOpenedStorageName, projectName,
                    DateTime.Now.ToString("o", CultureInfo.InvariantCulture));
            }
            catch { }
        }

        private ProjectBrowserForm(IEnumerable<string> projectNames, string currentProject)
        {
            _currentProject = currentProject;

            this.Text = "開啟專案";
            this.Font = new Font("微軟正黑體", 9F);
            this.AutoScaleMode = AutoScaleMode.None;
            this.ClientSize = new Size(1000, 620);
            this.MinimumSize = new Size(700, 400);
            this.ShowInTaskbar = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.KeyPreview = true;

            BuildLayout();
            LoadLocalData(projectNames);
            RefreshList();

            this.Shown += delegate
            {
                txtSearch.Focus();
                StartBackgroundLoad();
            };
            this.FormClosed += delegate { _closed = true; };
        }

        #region 版面

        private void BuildLayout()
        {
            // 上方：搜尋框
            Panel top = new Panel();
            top.Dock = DockStyle.Top;
            top.Height = 44;
            top.Padding = new Padding(12, 10, 12, 6);

            Label lblSearch = new Label();
            lblSearch.Text = "搜尋：";
            lblSearch.AutoSize = true;
            lblSearch.Location = new Point(12, 14);
            top.Controls.Add(lblSearch);

            txtSearch = new TextBox();
            txtSearch.Location = new Point(60, 10);
            txtSearch.Width = 420;
            txtSearch.TextChanged += delegate { RefreshList(); };
            txtSearch.KeyDown += txtSearch_KeyDown;
            top.Controls.Add(txtSearch);

            Label lblHint = new Label();
            lblHint.Text = "可搜尋專案名稱、站台、Contract、資料表；空白分隔多個關鍵字";
            lblHint.ForeColor = SystemColors.GrayText;
            lblHint.AutoSize = true;
            lblHint.Location = new Point(490, 14);
            top.Controls.Add(lblHint);

            // 下方：狀態與按鈕
            Panel bottom = new Panel();
            bottom.Dock = DockStyle.Bottom;
            bottom.Height = 48;

            lblStatus = new Label();
            lblStatus.AutoSize = true;
            lblStatus.ForeColor = SystemColors.GrayText;
            lblStatus.Location = new Point(12, 17);
            bottom.Controls.Add(lblStatus);

            btnCancel = new Button();
            btnCancel.Text = "取消";
            btnCancel.Size = new Size(84, 28);
            btnCancel.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            btnCancel.Location = new Point(this.ClientSize.Width - 96, 10);
            btnCancel.DialogResult = DialogResult.Cancel;
            bottom.Controls.Add(btnCancel);

            btnOpen = new Button();
            btnOpen.Text = "開啟";
            btnOpen.Size = new Size(84, 28);
            btnOpen.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            btnOpen.Location = new Point(this.ClientSize.Width - 186, 10);
            btnOpen.Click += delegate { OpenSelected(); };
            bottom.Controls.Add(btnOpen);

            btnFavorite = new Button();
            btnFavorite.Text = "★ 最愛";
            btnFavorite.Size = new Size(96, 28);
            btnFavorite.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            btnFavorite.Location = new Point(this.ClientSize.Width - 300, 10);
            btnFavorite.Click += delegate { ToggleFavorite(); };
            bottom.Controls.Add(btnFavorite);

            // 中間：左清單、右預覽
            SplitContainer split = new SplitContainer();
            split.Dock = DockStyle.Fill;
            split.FixedPanel = FixedPanel.Panel2;

            lvProjects = new ListView();
            lvProjects.Dock = DockStyle.Fill;
            lvProjects.View = View.Details;
            lvProjects.FullRowSelect = true;
            lvProjects.HideSelection = false;
            lvProjects.MultiSelect = false;
            lvProjects.Columns.Add("★", 32);
            lvProjects.Columns.Add("專案名稱", 220);
            lvProjects.Columns.Add("開發站台", 200);
            lvProjects.Columns.Add("Contract", 70, HorizontalAlignment.Right);
            lvProjects.Columns.Add("資料表", 60, HorizontalAlignment.Right);
            lvProjects.Columns.Add("最近開啟", 130);
            lvProjects.ColumnClick += lvProjects_ColumnClick;
            lvProjects.SelectedIndexChanged += delegate { ShowPreview(); };
            lvProjects.DoubleClick += delegate { OpenSelected(); };
            lvProjects.MouseClick += lvProjects_MouseClick;
            lvProjects.KeyDown += lvProjects_KeyDown;
            split.Panel1.Controls.Add(lvProjects);

            tvPreview = new TreeView();
            tvPreview.Dock = DockStyle.Fill;
            tvPreview.ShowRootLines = true;
            split.Panel2.Controls.Add(tvPreview);

            this.Controls.Add(split);
            this.Controls.Add(top);
            this.Controls.Add(bottom);

            split.SplitterDistance = this.ClientSize.Width - 290;

            this.CancelButton = btnCancel;
        }

        #endregion

        #region 本機資料（快取、最愛、最近開啟）

        private void LoadLocalData(IEnumerable<string> projectNames)
        {
            HashSet<string> favorites = new HashSet<string>(ReadFavorites());
            Dictionary<string, XmlElement> cache = ReadCache();

            foreach (string name in projectNames)
            {
                ProjectInfo p = new ProjectInfo();
                p.Name = name;
                p.Favorite = favorites.Contains(name);

                string opened = SafeGetPropertyValue(LastOpenedStorageName, name);
                DateTime dt;
                if (!string.IsNullOrEmpty(opened) &&
                    DateTime.TryParse(opened, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out dt))
                    p.LastOpened = dt;

                XmlElement cached;
                if (cache.TryGetValue(name, out cached))
                    ApplyCache(p, cached);

                _projects.Add(p);
            }
        }

        private static string SafeGetPropertyValue(string propertyName, string attr)
        {
            try { return MainForm.Storage.GetPropertyValue(propertyName, attr); }
            catch { return string.Empty; }
        }

        private static List<string> ReadFavorites()
        {
            List<string> list = new List<string>();
            try
            {
                string text = MainForm.Storage.GetProperty(FavoriteStorageName);
                if (!string.IsNullOrEmpty(text))
                {
                    foreach (string s in text.Split('\n'))
                        if (s.Trim().Length > 0) list.Add(s.Trim());
                }
            }
            catch { }
            return list;
        }

        private void SaveFavorites()
        {
            List<string> list = new List<string>();
            foreach (ProjectInfo p in _projects)
                if (p.Favorite) list.Add(p.Name);
            try { MainForm.Storage.SetProperty(FavoriteStorageName, string.Join("\n", list.ToArray())); }
            catch { }
        }

        private static Dictionary<string, XmlElement> ReadCache()
        {
            Dictionary<string, XmlElement> result = new Dictionary<string, XmlElement>();
            try
            {
                XmlHelper h = MainForm.Storage.GetPropertyXml(CacheStorageName, "All");
                if (h != null)
                {
                    foreach (XmlElement e in h.GetElements("Project"))
                        result[e.GetAttribute("Name")] = e;
                }
            }
            catch { }
            return result;
        }

        private void SaveCache()
        {
            try
            {
                // 只存顯示用的資訊，不存密碼
                XmlHelper h = new XmlHelper("<Projects/>");
                foreach (ProjectInfo p in _projects)
                {
                    if (!p.Loaded) continue;
                    XmlElement pe = h.AddElement(".", "Project");
                    pe.SetAttribute("Name", p.Name);
                    pe.SetAttribute("Site", p.Site);
                    pe.SetAttribute("User", p.User);
                    XmlHelper ph = new XmlHelper(pe);
                    foreach (string c in p.Contracts)
                        ph.AddElement(".", "Contract").SetAttribute("Name", c);
                    foreach (string t in p.Tables)
                        ph.AddElement(".", "Table").SetAttribute("Name", t);
                }
                MainForm.Storage.SetPropertyXml(CacheStorageName, "All", h.GetElement("."));
            }
            catch { }
        }

        private static void ApplyCache(ProjectInfo p, XmlElement e)
        {
            p.Site = e.GetAttribute("Site");
            p.User = e.GetAttribute("User");
            p.Contracts.Clear();
            p.Tables.Clear();
            XmlHelper h = new XmlHelper(e);
            foreach (XmlElement c in h.GetElements("Contract"))
                p.Contracts.Add(c.GetAttribute("Name"));
            foreach (XmlElement t in h.GetElements("Table"))
                p.Tables.Add(t.GetAttribute("Name"));
            p.Loaded = true;
        }

        #endregion

        #region 背景讀取各專案設定

        private void StartBackgroundLoad()
        {
            List<ProjectInfo> targets = new List<ProjectInfo>(_projects);
            // 最愛、最近開過的先讀
            targets.Sort(delegate (ProjectInfo a, ProjectInfo b)
            {
                if (a.Favorite != b.Favorite) return a.Favorite ? -1 : 1;
                return b.LastOpened.CompareTo(a.LastOpened);
            });

            int total = targets.Count;
            SetStatus("正在更新專案資訊 0/" + total + "...");

            Task.Run(delegate
            {
                int done = 0;
                foreach (ProjectInfo p in targets)
                {
                    if (_closed) return;

                    ProjectInfo fresh = FetchProject(p.Name);
                    done++;
                    int count = done;

                    try
                    {
                        this.BeginInvoke(new Action(delegate
                        {
                            if (_closed) return;
                            if (fresh != null)
                            {
                                p.Site = fresh.Site;
                                p.User = fresh.User;
                                p.Contracts = fresh.Contracts;
                                p.Tables = fresh.Tables;
                                p.Loaded = true;
                                p.LoadFailed = false;
                            }
                            else if (!p.Loaded)
                            {
                                p.LoadFailed = true;
                            }
                            UpdateItem(p);
                            SetStatus(count < total ? "正在更新專案資訊 " + count + "/" + total + "..." : "共 " + total + " 個專案");
                        }));
                    }
                    catch (InvalidOperationException) { return; } // 表單已關閉
                }

                try
                {
                    this.BeginInvoke(new Action(delegate
                    {
                        if (_closed) return;
                        SaveCache();
                        RefreshList(); // 資訊補齊後重新套用搜尋與排序
                    }));
                }
                catch (InvalidOperationException) { }
            });
        }

        /// <summary>
        /// 向 Greening 讀取專案設定（背景執行緒）。失敗回傳 null。
        /// </summary>
        private static ProjectInfo FetchProject(string name)
        {
            try
            {
                XmlHelper req = new XmlHelper("<Request/>");
                req.AddElement(".", "Condition");
                req.AddElement("Condition", "Name", ProjectHandler.PROJECT_PS_PREFIX + name);
                Envelope evn = MainForm.LoginArgs.GreeningConnection.SendRequest("GetMySpace", new Envelope(req));
                XmlHelper rsp = new XmlHelper(evn.Body);

                string content = rsp.GetText("Space/Content");
                if (string.IsNullOrWhiteSpace(content))
                    return null;

                XmlHelper pref = new XmlHelper(XmlHelper.ParseAsDOM(content));
                ProjectInfo p = new ProjectInfo();
                p.Name = name;
                p.Site = pref.GetText("Property[@Name='DevSite']//AccessPoint") ?? string.Empty;
                p.User = pref.GetText("Property[@Name='DevSite']//User") ?? string.Empty;
                foreach (XmlElement e in pref.GetElements("Property[@Name='UDS']/Contract"))
                    p.Contracts.Add(e.GetAttribute("Name"));
                foreach (XmlElement e in pref.GetElements("Property[@Name='UDT']/UDT"))
                    p.Tables.Add(e.GetAttribute("Name"));
                p.Contracts.Sort(StringComparer.OrdinalIgnoreCase);
                p.Tables.Sort(StringComparer.OrdinalIgnoreCase);
                return p;
            }
            catch
            {
                return null;
            }
        }

        private void SetStatus(string text)
        {
            lblStatus.Text = text;
        }

        #endregion

        #region 清單

        private void RefreshList()
        {
            string selected = SelectedName();
            string[] terms = txtSearch.Text.Trim().ToLowerInvariant()
                .Split(new char[] { ' ', '　' }, StringSplitOptions.RemoveEmptyEntries);

            List<ProjectInfo> visible = new List<ProjectInfo>();
            foreach (ProjectInfo p in _projects)
                if (Matches(p, terms)) visible.Add(p);

            visible.Sort(CompareProjects);

            lvProjects.BeginUpdate();
            lvProjects.Items.Clear();
            _items.Clear();
            foreach (ProjectInfo p in visible)
            {
                ListViewItem item = new ListViewItem(new string[6]);
                item.Tag = p;
                FillItem(item, p);
                lvProjects.Items.Add(item);
                _items[p.Name] = item;
            }
            lvProjects.EndUpdate();

            // 盡量維持原本選取；沒有就選第一筆（目前專案優先）
            ListViewItem target = null;
            if (selected != null) _items.TryGetValue(selected, out target);
            if (target == null && terms.Length == 0 && _currentProject != null) _items.TryGetValue(_currentProject, out target);
            if (target == null && lvProjects.Items.Count > 0) target = lvProjects.Items[0];
            if (target != null)
            {
                target.Selected = true;
                target.Focused = true;
                target.EnsureVisible();
            }
            ShowPreview();

            if (!lblStatus.Text.StartsWith("正在"))
                SetStatus(terms.Length == 0 ? "共 " + _projects.Count + " 個專案" : "符合 " + visible.Count + " / " + _projects.Count + " 個專案");
        }

        private static bool Matches(ProjectInfo p, string[] terms)
        {
            foreach (string term in terms)
            {
                if (Contains(p.Name, term) || Contains(p.Site, term)) continue;
                bool hit = false;
                foreach (string c in p.Contracts)
                    if (Contains(c, term)) { hit = true; break; }
                if (!hit)
                    foreach (string t in p.Tables)
                        if (Contains(t, term)) { hit = true; break; }
                if (!hit) return false;
            }
            return true;
        }

        private static bool Contains(string text, string term)
        {
            return !string.IsNullOrEmpty(text) && text.ToLowerInvariant().Contains(term);
        }

        private int CompareProjects(ProjectInfo a, ProjectInfo b)
        {
            // 最愛一律在最上面
            if (a.Favorite != b.Favorite) return a.Favorite ? -1 : 1;

            int r;
            switch (_sortColumn)
            {
                case ColSite: r = string.Compare(a.Site, b.Site, StringComparison.OrdinalIgnoreCase); break;
                case ColContracts: r = a.Contracts.Count.CompareTo(b.Contracts.Count); break;
                case ColTables: r = a.Tables.Count.CompareTo(b.Tables.Count); break;
                case ColLastOpened: r = a.LastOpened.CompareTo(b.LastOpened); break;
                default: r = 0; break;
            }
            if (!_sortAscending) r = -r;
            if (r == 0) r = string.Compare(a.Name, b.Name, StringComparison.OrdinalIgnoreCase);
            return r;
        }

        private void FillItem(ListViewItem item, ProjectInfo p)
        {
            item.SubItems[ColFavorite].Text = p.Favorite ? "★" : "☆";
            item.SubItems[ColName].Text = p.Name;
            item.SubItems[ColSite].Text = p.Loaded ? p.Site : (p.LoadFailed ? "（讀取失敗）" : "…");
            item.SubItems[ColContracts].Text = p.Loaded ? p.Contracts.Count.ToString() : "";
            item.SubItems[ColTables].Text = p.Loaded ? p.Tables.Count.ToString() : "";
            item.SubItems[ColLastOpened].Text = p.LastOpened == DateTime.MinValue ? "" : p.LastOpened.ToString("yyyy-MM-dd HH:mm");

            item.UseItemStyleForSubItems = true;
            item.ForeColor = p.Favorite ? Color.DarkGoldenrod : SystemColors.WindowText;
            item.Font = p.Name == _currentProject ? new Font(this.Font, FontStyle.Bold) : this.Font;
        }

        private void UpdateItem(ProjectInfo p)
        {
            ListViewItem item;
            if (_items.TryGetValue(p.Name, out item))
            {
                FillItem(item, p);
                if (item.Selected) ShowPreview();
            }
        }

        private string SelectedName()
        {
            if (lvProjects == null || lvProjects.SelectedItems.Count == 0) return null;
            return ((ProjectInfo)lvProjects.SelectedItems[0].Tag).Name;
        }

        private ProjectInfo SelectedInfo()
        {
            if (lvProjects.SelectedItems.Count == 0) return null;
            return (ProjectInfo)lvProjects.SelectedItems[0].Tag;
        }

        private void ShowPreview()
        {
            ProjectInfo p = SelectedInfo();
            btnOpen.Enabled = p != null;
            btnFavorite.Enabled = p != null;
            btnFavorite.Text = p != null && p.Favorite ? "☆ 取消最愛" : "★ 最愛";

            tvPreview.BeginUpdate();
            tvPreview.Nodes.Clear();
            if (p != null)
            {
                TreeNode root = tvPreview.Nodes.Add(p.Name);
                if (!p.Loaded)
                {
                    root.Nodes.Add(p.LoadFailed ? "讀取失敗" : "讀取中...");
                }
                else
                {
                    root.Nodes.Add("站台：" + p.Site);
                    if (!string.IsNullOrEmpty(p.User))
                        root.Nodes.Add("帳號：" + p.User);

                    TreeNode uds = root.Nodes.Add("UDS Contract（" + p.Contracts.Count + "）");
                    foreach (string c in p.Contracts) uds.Nodes.Add(c);

                    TreeNode udt = root.Nodes.Add("UDT 資料表（" + p.Tables.Count + "）");
                    foreach (string t in p.Tables) udt.Nodes.Add(t);
                }
                root.ExpandAll();
                root.EnsureVisible();
            }
            tvPreview.EndUpdate();
        }

        private void ToggleFavorite()
        {
            ProjectInfo p = SelectedInfo();
            if (p == null) return;
            p.Favorite = !p.Favorite;
            SaveFavorites();
            RefreshList();
        }

        private void OpenSelected()
        {
            ProjectInfo p = SelectedInfo();
            if (p == null) return;
            SelectedProject = p.Name;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void lvProjects_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == ColFavorite) return;
            if (_sortColumn == e.Column)
                _sortAscending = !_sortAscending;
            else
            {
                _sortColumn = e.Column;
                // 名稱、站台預設由小到大；數量、時間預設由大到小
                _sortAscending = e.Column == ColName || e.Column == ColSite;
            }
            RefreshList();
        }

        private void lvProjects_MouseClick(object sender, MouseEventArgs e)
        {
            // 點星號欄切換最愛
            ListViewHitTestInfo hit = lvProjects.HitTest(e.Location);
            if (hit.Item != null && hit.SubItem != null && hit.Item.SubItems.IndexOf(hit.SubItem) == ColFavorite)
                ToggleFavorite();
        }

        private void lvProjects_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                OpenSelected();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Space)
            {
                ToggleFavorite();
                e.Handled = true;
            }
            else if (!e.Control && !e.Alt && (char.IsLetterOrDigit((char)e.KeyValue) || e.KeyCode == Keys.Back))
            {
                // 在清單上直接打字就回到搜尋框
                txtSearch.Focus();
            }
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                OpenSelected();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up)
            {
                MoveSelection(e.KeyCode == Keys.Down ? 1 : -1);
                e.Handled = true;
            }
        }

        private void MoveSelection(int delta)
        {
            if (lvProjects.Items.Count == 0) return;
            int index = lvProjects.SelectedIndices.Count > 0 ? lvProjects.SelectedIndices[0] : -1;
            index = Math.Max(0, Math.Min(lvProjects.Items.Count - 1, index + delta));
            ListViewItem item = lvProjects.Items[index];
            item.Selected = true;
            item.Focused = true;
            item.EnsureVisible();
        }

        #endregion
    }
}
