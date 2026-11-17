using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ProjectManager.Login;
using ProjectManager.Project;

namespace ProjectManager.ActionHandler
{
    /// <summary>
    /// 開發站台連不上（或沒設定）時的專案狀態，給離線節點與設定畫面共用。
    /// </summary>
    class OfflineProjectState
    {
        internal ProjectHandler Project { get; private set; }
        internal ConnectResult Result { get; private set; }

        /// <summary>重新連線並載入專案（由 MainForm 提供）</summary>
        internal Action Reconnect { get; private set; }

        internal OfflineProjectState(ProjectHandler project, ConnectResult result, Action reconnect)
        {
            Project = project;
            Result = result;
            Reconnect = reconnect;
        }
    }

    /// <summary>
    /// 離線時樹上的節點：結構照專案設定畫出來，點了只會在右邊顯示開發站台設定畫面。
    /// </summary>
    class OfflineProjectNodeHandler : INodeHandler, IEditorManager
    {
        public OfflineProjectNodeHandler(TreeNode node, OfflineProjectState state)
        {
            Node = node;
            Editors = new List<IEditable>();
            Editors.Add(new DevSiteSetupEditable(state));
            CurrentEditor = Editors[0];
        }

        public TreeNode Node { get; private set; }

        public bool IsFirstClick { get { return false; } }

        public void OnFirstClick() { }

        public void OnClick() { }

        public List<IEditable> Editors { get; private set; }

        public IEditable CurrentEditor { get; set; }
    }

    class DevSiteSetupEditable : AbstractUIEditable
    {
        private readonly OfflineProjectState _state;

        public DevSiteSetupEditable(OfflineProjectState state)
        {
            _state = state;
            DocumentName = state.Project.Name;
        }

        public override string DocumentTitle
        {
            get { return "開發站台設定"; }
        }

        public override bool Valid { get { return true; } }

        public override void Save() { }

        protected override void OnInitialEditor()
        {
            _editorInstance = new DevSiteSetupPanel(_state);
        }
    }

    /// <summary>
    /// 開發站台連不上時右邊顯示的畫面：說明原因，提供「重新連線」與「設定開發站台」。
    /// 版面直接用程式碼排，沒有設計工具檔。
    /// </summary>
    class DevSiteSetupPanel : UserControl
    {
        private readonly OfflineProjectState _state;

        public DevSiteSetupPanel(OfflineProjectState state)
        {
            _state = state;

            this.Font = new Font("微軟正黑體", 10F);
            this.BackColor = SystemColors.Window;
            this.Dock = DockStyle.Fill;

            bool noDevSite = state.Result == ConnectResult.NoDevSite;
            bool cancelled = state.Result == ConnectResult.Cancelled;

            Label lblTitle = new Label();
            lblTitle.Text = noDevSite ? "尚未設定開發站台" : cancelled ? "已取消連線" : "開發站台連線失敗";
            lblTitle.Font = new Font("微軟正黑體", 14F, FontStyle.Bold);
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(32, 32);
            this.Controls.Add(lblTitle);

            string site = state.Project.DevSite == null ? string.Empty : state.Project.DevSite.AccessPoint;
            Label lblMessage = new Label();
            lblMessage.Text = (noDevSite ? "這個專案的設定裡沒有開發站台。"
                             : cancelled ? "已取消連線開發站台，需要時再按「重新連線」。"
                             : "無法連線到開發站台：" + site + "\n可能是站台暫時無法連線、網路問題，或帳號密碼已變更。")
                            + "\n\n左邊灰色的結構是專案設定裡記錄的 Contract 與資料表，連上開發站台後才能開啟編輯。";
            lblMessage.AutoSize = true;
            lblMessage.MaximumSize = new Size(640, 0);
            lblMessage.Location = new Point(34, 76);
            this.Controls.Add(lblMessage);

            Button btnReconnect = new Button();
            btnReconnect.Text = "重新連線";
            btnReconnect.Size = new Size(140, 34);
            btnReconnect.Location = new Point(34, 190);
            btnReconnect.Enabled = !noDevSite;
            btnReconnect.Click += delegate { _state.Reconnect(); };
            this.Controls.Add(btnReconnect);

            Button btnSetup = new Button();
            btnSetup.Text = "設定開發站台...";
            btnSetup.Size = new Size(160, 34);
            btnSetup.Location = new Point(184, 190);
            btnSetup.Click += delegate { SetupDevSite(); };
            this.Controls.Add(btnSetup);
        }

        private void SetupDevSite()
        {
            ProjectHandler project = _state.Project;
            ResetDevSiteForm form = new ResetDevSiteForm(project.Name, project.DevSite ?? new DevSiteLoginInfo());
            form.StartPosition = FormStartPosition.CenterParent;

            bool saved = false;
            form.Completed += delegate
            {
                try
                {
                    project.SaveDevSite(form.GetDevSite());
                    saved = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("儲存開發站台設定失敗：" + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };
            form.ShowDialog(this.FindForm());

            // 存好就用新設定重新載入專案
            if (saved)
                _state.Reconnect();
        }
    }
}
