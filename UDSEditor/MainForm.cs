using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using FISCA.Deployment;
using FISCA.DSAClient;
using ModuleFileManager.Utils;
using ProjectManager.ActionHandler;
using ProjectManager.ActionHandler.Files;
using ProjectManager.ActionHandler.UDS;
using ProjectManager.ActionHandler.UDT;
using ProjectManager.Login;
using ProjectManager.Project;
using ProjectManager.Project.Proxy;
using ProjectManager.Project.UDS;
using ProjectManager.Project.UDT;
using ProjectManager.Util.Converter.UI;

namespace ProjectManager
{
    public partial class MainForm : Form
    {
        internal static LoginEventArgs LoginArgs { get; private set; }
        internal static DevSiteLoginInfo DefaultDevSite { get; private set; }
        internal static ProjectCollection Projects { get; private set; }
        internal static ProjectHandler CurrentProject { get; private set; }
        internal static UDTHandler CurrentUDT { get; private set; }
        internal static UDSHandler CurrentUDS { get; set; }
        internal static FileNodeHandler CurrentFileNodeHandler { get; private set; }

        internal static bool IsClosing { get; private set; }

        internal static readonly Version Version;
        internal static readonly LocalStorage Storage;

        private DevSiteLoginInfo DevSite { get; set; }
        private bool _currentDataChanged;
        private IEditable _currentEditable;
        private UpdateHelper _updateHelper;

        static MainForm()
        {
            Version = Assembly.GetExecutingAssembly().GetName().Version;
            Storage = new LocalStorage();
            DefaultDevSite = new DevSiteLoginInfo();
            Projects = new ProjectCollection();
        }

        public MainForm()
        {
            InitializeComponent();
        }

        private void cmServiceConvert_Click(object sender, EventArgs e)
        {
            ConvertToPhysicalForm cpForm = new ConvertToPhysicalForm();
            cpForm.ShowDialog();
        }

        private void cmToUDSService_Click(object sender, EventArgs e)
        {
            ConvertToUDSForm cuForm = new ConvertToUDSForm();
            cuForm.ShowDialog();
        }

        private void cmProxyDeploy_Click(object sender, EventArgs e)
        {
            if (CurrentProject == null)
            {
                MessageBox.Show("請先建立或選擇專案", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ProxyDeployForm pdf = new ProxyDeployForm(CurrentProject);
            pdf.ShowDialog();
        }

        private void cmAddProject_Click(object sender, EventArgs e)
        {
            AddProjectForm addProjectForm = new AddProjectForm();
            addProjectForm.StartPosition = FormStartPosition.CenterParent;
            addProjectForm.ShowDialog();
        }

        private void LoadDevProject(string projectName, DevSiteLoginInfo devSite)
        {
            ReloadProjectList();

            foreach (ToolStripMenuItem item in cmOpenProject.DropDownItems)
                item.Checked = item.Text == projectName;

            MainForm.CurrentProject = Projects.LoadProjectHandler(projectName);

            if (CurrentProject == null)
                return;

            if (CurrentProject.Status == ProjectStatus.LoadFail)
                return;

            //ProjectHandler project = null;
            //if (devSite != null)
            //    project = new ProjectHandler(projectName, devSite);
            //else
            //    project = new ProjectHandler(projectName);
            ProjectHandler project = MainForm.CurrentProject;

            tvProjects.Nodes.Clear();

            TreeNode tn = tvProjects.Nodes.Add(projectName);
            tn.SelectedImageKey = "project";
            tn.ImageKey = "project";
            ProjectNodeHandler projectNodeHandler = new ProjectNodeHandler(tn, project);
            projectNodeHandler.Reloaded += new EventHandler(projectNodeHandler_Reloaded);
            tn.Tag = projectNodeHandler;

            TreeNode udtNode = tn.Nodes.Add("UDT");
            udtNode.ImageKey = "udts";
            udtNode.SelectedImageKey = "udts";
            udtNode.ToolTipText = "自訂資料表";
            UDTNodeHandler udtNodeHandler = new UDTNodeHandler(udtNode);
            udtNode.Tag = udtNodeHandler;
            udtNodeHandler.Reload();
            MainForm.CurrentUDT = udtNodeHandler.UDTHandler;

            TreeNode udsNode = tn.Nodes.Add("UDS");
            udsNode.ImageKey = "udss";
            udsNode.SelectedImageKey = "udss";
            udsNode.ToolTipText = "自訂服務";
            UDSNodeHandler udsNodeHandler = new UDSNodeHandler(udsNode);
            udsNode.Tag = udsNodeHandler;
            udsNodeHandler.Reload();
            MainForm.CurrentUDS = udsNodeHandler.UDSHandler;

            TreeNode fileNode = tn.Nodes.Add("檔案管理");
            fileNode.SelectedImageKey = "upload";
            fileNode.ImageKey = "upload";

            MainForm.CurrentFileNodeHandler = null;
            if (MainForm.LoginArgs.SucceedModuleLogin)
            {
                fileNode.ToolTipText = string.Empty;
                FileNodeHandler fileNodeHandler = new FileNodeHandler(fileNode);
                fileNode.Tag = fileNodeHandler;
                MainForm.CurrentFileNodeHandler = fileNodeHandler;
            }
            else
            {
                fileNode.ToolTipText = "檔案管理伺服器登入失敗";
            }
            tvProjects.ExpandAll();
            udtNodeHandler.CollapseOutsideProject();
            udsNodeHandler.CollapseOutsideProject();
        }

        void projectNodeHandler_Reloaded(object sender, EventArgs e)
        {
            ProjectNodeHandler ph = sender as ProjectNodeHandler;
            LoadDevProject(ph.Project.Name);
        }

        private void LoadDevProject(string projectName)
        {
            LoadDevProject(projectName, null);
        }

        private void tvProjects_AfterSelect(object sender, TreeViewEventArgs e)
        {
            INodeHandler handler = e.Node.Tag as INodeHandler;
            if (handler != null)
            {
                if (handler.IsFirstClick)
                    handler.OnFirstClick();
                else
                    handler.OnClick();
            }
            splitContainer1.Panel2.Controls.Clear();

            IDeleteable del = handler as IDeleteable;
            tsbtnDelete.Enabled = false;
            if (del != null)
            {
                tsbtnDelete.Enabled = true;
                tsbtnDelete.ToolTipText = del.TitleOfDelete;
                menuProject.Items.Add(del.TitleOfDelete, actImgs.Images["delete"], cmDelete_Click);
            }

            IAddable add = handler as IAddable;
            tsbtnAdd.Enabled = false;
            if (add != null)
            {
                tsbtnAdd.Enabled = true;
                tsbtnAdd.ToolTipText = add.TitleOfAdd;
                menuProject.Items.Add(add.TitleOfAdd, actImgs.Images["add"], cmAdd_Click);
            }

            IReloadable reload = handler as IReloadable;
            tsbtnReload.Enabled = false;
            if (reload != null)
                tsbtnReload.Enabled = true;

            ITestable test = handler as ITestable;
            tsbtnTest.Enabled = false;
            if (test != null)
            {
                tsbtnTest.Image = actImgs.Images[test.TestImageKey];
                tsbtnTest.Enabled = true;
            }

            IRenameable rename = handler as IRenameable;
            tsbtnRename.Enabled = false;
            if (rename != null)
                tsbtnRename.Enabled = true;

            IExportable export = handler as IExportable;
            tsbtnExport.Enabled = false;
            if (export != null)
                tsbtnExport.Enabled = true;

            ISetupable setup = handler as ISetupable;
            cmSetup.Enabled = false;
            if (setup != null)
                cmSetup.Enabled = true;

            IDeployable deploy = handler as IDeployable;
            cmDeploy.Enabled = false;
            if (deploy != null)
                cmDeploy.Enabled = true;

            IDeployToPhysical phyDeploy = handler as IDeployToPhysical;
            tsbtnDeployToPhysical.Enabled = false;
            if (phyDeploy != null)
                tsbtnDeployToPhysical.Enabled = true;

            IImportFromPhysical phyImport = handler as IImportFromPhysical;
            tsbtnImportFromPhysical.Enabled = false;
            if (phyImport != null)
                tsbtnImportFromPhysical.Enabled = true;

            IImportable import = handler as IImportable;
            tsbtnImport.Enabled = false;
            if (import != null)
                tsbtnImport.Enabled = true;

            IJoinProject joinProject = handler as IJoinProject;
            tsbtnJoinProject.Enabled = false;
            if (joinProject != null)
                tsbtnJoinProject.Enabled = true;

            ILeaveProject leaveProject = handler as ILeaveProject;
            tsbtnLeaveProject.Enabled = false;
            if (leaveProject != null)
                tsbtnLeaveProject.Enabled = true;

            ISyncUpload syncUplaod = handler as ISyncUpload;
            cmUpload.Enabled = false;
            if (syncUplaod != null)
                cmUpload.Enabled = true;

            ISyncUpload syncDownload = handler as ISyncUpload;
            cmDownload.Enabled = false;
            if (syncDownload != null)
                cmDownload.Enabled = true;

            tsEditMode.Items.Clear();
            rsbtnSave.Enabled = false;
            IEditorManager manager = handler as IEditorManager;
            if (manager != null)
            {
                rsbtnSave.Enabled = false;
                foreach (IEditable editable in manager.Editors)
                {
                    ToolStripItem item = tsEditMode.Items.Add(editable.ModeTitle, actImgs.Images[editable.ImageKey], cmEdit_Click);
                    item.ToolTipText = editable.ModeTitle;
                    item.Text = string.Empty;
                    item.Tag = editable;
                }
            }
        }

        private void menuProject_Opening(object sender, CancelEventArgs e)
        {
            if (tvProjects.SelectedNode == null)
            {
                e.Cancel = true;
                return;
            }

            if (menuProject.Items.Count == 0)
                e.Cancel = true;
        }

        private void cmLogin_Click(object sender, EventArgs e)
        {
            LoginGreening();
        }

        private void LoginGreening()
        {
            LoginForm loginForm = new LoginForm();
            loginForm.StartPosition = FormStartPosition.CenterParent;
            loginForm.Logined += new EventHandler<LoginEventArgs>(loginForm_Logined);
            loginForm.FormClosed += new FormClosedEventHandler(loginForm_FormClosed);
            loginForm.ShowDialog();
        }

        void loginForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (MainForm.LoginArgs == null)
            {
                this.Close();
                return;
            }

            ReloadProjectList();

            XmlElement pref = MainForm.LoginArgs.StaticPreference;
            if (pref != null)
            {
                XmlHelper h = new XmlHelper(pref);
                string ap = h.GetText("AccessPoint");
                string user = h.GetText("User");
                string pwd = h.GetText("Password");

                MainForm.DefaultDevSite.AccessPoint = ap;
                MainForm.DefaultDevSite.User = user;
                MainForm.DefaultDevSite.Password = pwd;

                //try
                //{
                //    MainForm.DefaultDevSite.TryConnect();
                //}
                //catch (Exception ex)
                //{
                //    DialogResult dr = MessageBox.Show("嘗試登入預設開發站台失敗!\n" + ex.Message + "\n是否重新設定預設開發站台?", "失敗", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                //    if (dr == System.Windows.Forms.DialogResult.Yes)
                //        LoginDevSite();
                //}
            }
            else
            {
                LoginDevSite();
            }

            string lastLoginProject = MainForm.Storage.GetProperty("LastestProject");
            if (!string.IsNullOrWhiteSpace(lastLoginProject))
                LoadDevProject(lastLoginProject);
        }

        void loginForm_Logined(object sender, LoginEventArgs e)
        {
            MainForm.LoginArgs = e;
        }

        private void LoginDevSite()
        {
            DevSiteForm dsForm = new DevSiteForm();
            dsForm.StartPosition = FormStartPosition.CenterParent;
            dsForm.ShowDialog();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            string title = this.Text + "( 版本 : " + MainForm.Version.ToString() + ")";
            this.Text = title;

            ManifestResolver resolver = new ManifestResolver(Program.UpdateURL, VersionOption.Stable);
            _updateHelper = new UpdateHelper(resolver, new InstallDescriptor(Application.StartupPath));

            System.Threading.Timer timer = new System.Threading.Timer(timer_Tick, null, new TimeSpan(0), new TimeSpan(1, 0, 0));

            LoginGreening();

            XmlHelper xml = MainForm.Storage.GetPropertyXml(toolStrip1.Name, "Location");
            if (xml != null)
            {
                int x = xml.TryGetInteger("X", 0);
                int y = xml.TryGetInteger("Y", 0);
                toolStrip1.Location = new Point(x, y);
            }

            MainForm.Projects.ProjectRemoved += new EventHandler(Projects_ProjectRemoved);
            MainForm.Projects.ProjectAdded += new EventHandler<ProjectEventArgs>(Projects_ProjectAdded);
        }

        void Projects_ProjectAdded(object sender, ProjectEventArgs e)
        {
            LoadDevProject(e.ProjectName, e.DevSite);
        }

        void Projects_ProjectRemoved(object sender, EventArgs e)
        {
            tvProjects.Nodes.Clear();
            ReloadProjectList();
            MainForm.CurrentProject = null;
            tvProjects.Nodes.Add("尚無載入專案");
        }

        void timer_Tick(object state)
        {
            try
            {
                if (_updateHelper.CheckUpdate())
                {
                    PaddingScript script = new PaddingScript();

                    script.WaitRelease(Application.ExecutablePath);
                    _updateHelper.Update(script);
                    script.Delete(Path.Combine(Application.StartupPath, "_update_padding.xml"));
                    script.Delete(Path.Combine(Application.StartupPath, _updateHelper.Install.TemporalFolder));
                    script.StartProcess(Application.ExecutablePath);
                    script.Save(Path.Combine(Application.StartupPath, "_update_padding.xml"));

                    Invoke(new Action(() =>
                    {
                        tsVersionLabel.Visible = true;
                    }));
                }
            }
            catch { }
        }

        private void cmSite_Click(object sender, EventArgs e)
        {
            LoginDevSite();
        }

        private void ReloadProjectList()
        {
            cmOpenProject.DropDownItems.Clear();

            foreach (string projectName in MainForm.Projects.ListProjects())
            {
                ToolStripItem projectItem = cmOpenProject.DropDownItems.Add(projectName);
                projectItem.Click += delegate(object sender, EventArgs e)
                {
                    ToolStripItem item = sender as ToolStripItem;
                    this.LoadDevProject(item.Text);
                    MainForm.Storage.SetProperty("LastestProject", item.Text);
                };
            }

            if (cmOpenProject.DropDownItems.Count == 0)
            {
                ToolStripItem item = cmOpenProject.DropDownItems.Add("( 無 )");
                item.Enabled = false;
            }
        }

        private void tvProjects_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                tvProjects.SelectedNode = e.Node;

                menuProject.Items.Clear();

                //判斷是否可以開啟
                IEditorManager editorManager = e.Node.Tag as IEditorManager;
                if (editorManager != null)
                {
                    if (editorManager.Editors.Count == 1)
                    {
                        IEditable editable = editorManager.Editors[0];
                        ToolStripItem item = menuProject.Items.Add(editable.DocumentTitle, actImgs.Images[editable.ImageKey], cmEdit_Click);
                        item.Tag = editable;
                    }
                    else if (editorManager.Editors.Count > 0)
                    {
                        ToolStripMenuItem menuItem = new ToolStripMenuItem("開啟");
                        menuProject.Items.Add(menuItem);

                        foreach (IEditable editable in editorManager.Editors)
                        {
                            ToolStripItem item = menuItem.DropDownItems.Add(editable.DocumentTitle, actImgs.Images[editable.ImageKey], cmEdit_Click);
                            item.Tag = editable;
                        }
                    }
                }

                //判斷是否可以改名
                ToolStripMenuItem cmRename = new ToolStripMenuItem("重新命名(&N)");
                this.CheckMenuItemVisiable(typeof(IRenameable), cmRename);
                cmRename.Click += new EventHandler(cmRename_Click);
                cmRename.Image = actImgs.Images["rename"];

                //判斷是否可以新增
                IAddable addable = e.Node.Tag as IAddable;
                if (addable != null)
                    menuProject.Items.Add(addable.TitleOfAdd, actImgs.Images["add"], cmAdd_Click);

                //判斷是否可以重新整理
                ToolStripMenuItem cmReload = new ToolStripMenuItem("重新整理(&R)");
                this.CheckMenuItemVisiable(typeof(IReloadable), cmReload);
                cmReload.Click += new EventHandler(cmReload_Click);
                cmReload.Image = actImgs.Images["reload"];

                //判斷是否可以刪除
                IDeleteable deleteable = e.Node.Tag as IDeleteable;
                if (deleteable != null)
                    menuProject.Items.Add(deleteable.TitleOfDelete, actImgs.Images["delete"], cmDelete_Click);

                //判斷是否可以測試
                ITestable testable = e.Node.Tag as ITestable;
                if (testable != null)
                    menuProject.Items.Add(testable.TitleOfTest, actImgs.Images[testable.TestImageKey], cmTest_Click);

                //判斷是否可以匯出
                ToolStripMenuItem cmExport = new ToolStripMenuItem("匯出(&E)");
                this.CheckMenuItemVisiable(typeof(IExportable), cmExport);
                cmExport.Click += new EventHandler(cmExport_Click);
                cmExport.Image = actImgs.Images["export"];

                //判斷是否可以匯入
                ToolStripMenuItem cmImport = new ToolStripMenuItem("匯入(&I)");
                this.CheckMenuItemVisiable(typeof(IImportable), cmImport);
                cmImport.Click += new EventHandler(cmImport_Click);
                cmImport.Image = actImgs.Images["import"];

                //判斷是否可以加入專案
                IJoinProject join = e.Node.Tag as IJoinProject;
                if (join != null)
                    menuProject.Items.Add("加入專案(&J)", actImgs.Images["join"], cmJoinProject_Click);

                //判斷是否可以匯入
                ILeaveProject leave = e.Node.Tag as ILeaveProject;
                if (leave != null)
                    menuProject.Items.Add("從專案卸載(&L)", actImgs.Images["leave"], cmLeaveProject_Click);

                //判斷是否可以上傳
                ISyncUpload upload = e.Node.Tag as ISyncUpload;
                if (upload != null)
                    menuProject.Items.Add("同步至伺服器(&U)", actImgs.Images["upload"], cmUpload_Click);

                //判斷是否可以下載
                ISyncDownload download = e.Node.Tag as ISyncDownload;
                if (download != null)
                    menuProject.Items.Add("自伺服器同步(&D)", actImgs.Images["download"], cmDownload_Click);

                //判斷是否可以設定
                ISetupable setup = e.Node.Tag as ISetupable;
                if (setup != null)
                    menuProject.Items.Add(setup.SetupTitle + "(&S)", actImgs.Images["setup"], cmSetup_Click);

                //判斷是否可以佈署
                IDeployable deploy = e.Node.Tag as IDeployable;
                if (deploy != null)
                    menuProject.Items.Add("佈署(&L)", actImgs.Images["deploy"], cmDeploy_Click);

                //判斷是否可以佈署到實體
                IDeployToPhysical deployToPhysical = e.Node.Tag as IDeployToPhysical;
                if (deployToPhysical != null)
                    menuProject.Items.Add("佈署到實體(&P)", actImgs.Images["deployToPhysical"], tsbtnDeployToPhysical_Click);

                //判斷是否可以從實體 Service 載入
                IImportFromPhysical importFromPhysical = e.Node.Tag as IImportFromPhysical;
                if (importFromPhysical != null)
                    menuProject.Items.Add("實體 Service 載入(&X)", actImgs.Images["importFromPhysical"], tsbtnImportFromPhysical_Click);

                menuProject.Show(e.Location);
            }
        }

        #region ContextMenuItem Click Events

        void cmLeaveProject_Click(object sender, EventArgs e)
        {
            this.DoLeaveProject();
        }

        void cmJoinProject_Click(object sender, EventArgs e)
        {
            this.DoJoinProject();
        }

        void cmImport_Click(object sender, EventArgs e)
        {
            this.DoImport();
        }

        void cmExport_Click(object sender, EventArgs e)
        {
            this.DoExport();
        }

        void cmDelete_Click(object sender, EventArgs e)
        {
            this.DoDelete();
        }

        void cmTest_Click(object sender, EventArgs e)
        {
            this.DoTest();
        }

        void cmRename_Click(object sender, EventArgs e)
        {
            this.DoRename();
        }

        void cmReload_Click(object sender, EventArgs e)
        {
            this.DoReload();
        }

        void cmAdd_Click(object sender, EventArgs e)
        {
            this.DoAdd();
        }

        void cmEdit_Click(object sender, EventArgs e)
        {
            ToolStripItem ctrl = sender as ToolStripItem;
            IEditable edit = ctrl.Tag as IEditable;

            if (edit == null) return;

            IEditorManager editManager = tvProjects.SelectedNode.Tag as IEditorManager;
            if (editManager != null)
            {
                if (editManager.Editors.Contains(edit))
                    editManager.CurrentEditor = edit;
            }
            this.DoEdit(edit);
        }

        private void cmLoadProjectFromFile_Click(object sender, EventArgs e)
        {
            LoadProjectFromFileForm lpffForm = new LoadProjectFromFileForm();
            lpffForm.StartPosition = FormStartPosition.CenterParent;
            lpffForm.ShowDialog();
        }
        #endregion

        private void CheckMenuItemVisiable(Type type, ToolStripMenuItem item)
        {
            if (tvProjects.SelectedNode == null) return;
            if (tvProjects.SelectedNode.Tag == null) return;

            if (type.IsInstanceOfType(tvProjects.SelectedNode.Tag))
                menuProject.Items.Add(item);
        }

        private void tvProjects_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            CheckBeforeMove(e);
        }

        private void cmAbout_Click(object sender, EventArgs e)
        {
            AboutForm af = new AboutForm();
            af.StartPosition = FormStartPosition.CenterParent;
            af.ShowDialog();
        }

        private void cmCheckVersion_Click(object sender, EventArgs e)
        {
            CheckVersionForm cvForm = new CheckVersionForm();
            cvForm.StartPosition = FormStartPosition.CenterParent;
            cvForm.ShowDialog();
        }

        #region ToolStripButton Click Events

        private void tsbtnDelete_Click(object sender, EventArgs e)
        {
            this.DoDelete();
        }

        private void tsbtnAdd_Click(object sender, EventArgs e)
        {
            this.DoAdd();
        }

        private void tsbtnReload_Click(object sender, EventArgs e)
        {
            this.DoReload();
        }

        private void tsbtnRename_Click(object sender, EventArgs e)
        {
            this.DoRename();
        }

        private void tsbtnExport_Click(object sender, EventArgs e)
        {
            this.DoExport();
        }

        private void tsbtnImport_Click(object sender, EventArgs e)
        {
            this.DoImport();
        }

        private void rsbtnSave_Click(object sender, EventArgs e)
        {
            DoSave();
        }

        private void tsbtnJoinProject_Click(object sender, EventArgs e)
        {
            DoJoinProject();
        }

        private void tsbtnLeaveProject_Click(object sender, EventArgs e)
        {
            DoLeaveProject();
        }
        #endregion

        #region DO CLICK Function
        private void DoJoinProject()
        {
            IJoinProject join = tvProjects.SelectedNode.Tag as IJoinProject;
            join.JoinProject();
        }

        private void DoLeaveProject()
        {
            ILeaveProject leave = tvProjects.SelectedNode.Tag as ILeaveProject;
            leave.LeaveProject();
        }

        private void DoTest()
        {
            ITestable test = tvProjects.SelectedNode.Tag as ITestable;
            test.Test();
        }

        private void DoDelete()
        {
            IDeleteable del = tvProjects.SelectedNode.Tag as IDeleteable;
            if (del == null) return;

            string text = tvProjects.SelectedNode.Text;
            if (MessageBox.Show("確定要刪除『" + text + "』?", "確定", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                del.Delete();
            }
        }

        private void DoAdd()
        {
            IAddable add = tvProjects.SelectedNode.Tag as IAddable;
            add.Add();
        }

        private void DoReload()
        {
            IReloadable action = tvProjects.SelectedNode.Tag as IReloadable;
            action.Reload();
        }

        private void DoRename()
        {
            IRenameable action = tvProjects.SelectedNode.Tag as IRenameable;
            action.Rename();
        }

        private void DoExport()
        {
            IExportable action = tvProjects.SelectedNode.Tag as IExportable;
            action.Export();
        }

        private void DoImport()
        {
            IImportable action = tvProjects.SelectedNode.Tag as IImportable;
            action.Import();
        }

        private void DoSave()
        {
            IEditorManager action = tvProjects.SelectedNode.Tag as IEditorManager;
            if (action == null) return;

            if (action.CurrentEditor == null) return;

            if (!action.CurrentEditor.Valid)
            {
                MessageBox.Show("資料驗證錯誤 !", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                action.CurrentEditor.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show("資料儲存失敗 !\n" + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DoUpload()
        {
            ISyncUpload upload = tvProjects.SelectedNode.Tag as ISyncUpload;
            if (upload == null) return;

            upload.Upload();
        }

        private void DoDownload()
        {
            ISyncDownload download = tvProjects.SelectedNode.Tag as ISyncDownload;
            if (download == null) return;

            download.Download();
        }

        private void DoSetup()
        {
            ISetupable setup = tvProjects.SelectedNode.Tag as ISetupable;
            if (setup == null) return;

            setup.Setup();
        }

        private void DoDeploy()
        {
            IDeployable deploy = tvProjects.SelectedNode.Tag as IDeployable;
            if (deploy == null) return;

            deploy.Deploy();
        }

        private void DoDeployToPhysical()
        {
            IDeployToPhysical deploy = tvProjects.SelectedNode.Tag as IDeployToPhysical;
            if (deploy == null) return;

            deploy.DeployToPhysical();
        }

        private void DoImportFromPhysical()
        {
            IImportFromPhysical import = tvProjects.SelectedNode.Tag as IImportFromPhysical;
            if (import == null) return;

            import.ImportFromPhysical();
        }

        private void DoEdit(IEditable editor)
        {
            _currentDataChanged = false;

            IEditorManager manager = tvProjects.SelectedNode.Tag as IEditorManager;
            manager.CurrentEditor = editor;

            if (editor != null)
            {
                editor.OnStartEditing();
                rsbtnSave.Enabled = false;
                this.SetEditor(editor.Editor);

                editor.EditorChanged += delegate(object s, EventArgs arg)
                {
                    this.SetEditor(editor.Editor);
                };
                editor.DataChanged += delegate(object s, EventArgs arg)
                {
                    _currentDataChanged = true;
                    rsbtnSave.Enabled = true;
                };
                editor.ChangeRecovered += delegate(object s, EventArgs arg)
                {
                    _currentDataChanged = false;
                    rsbtnSave.Enabled = false;
                };

                if (_currentEditable != null)
                {
                    _currentEditable.IsEditing = false;
                    _currentEditable.EditorReloaded -= new EventHandler(_currentEditable_EditorReloaded);
                }
                _currentEditable = editor;
                _currentEditable.IsEditing = true;
                _currentEditable.EditorReloaded += new EventHandler(_currentEditable_EditorReloaded);
            }
        }

        void SetEditor(Control control)
        {
            splitContainer1.Panel2.Controls.Clear();
            control.Dock = DockStyle.Fill;
            splitContainer1.Panel2.Controls.Add(control);
        }

        void _currentEditable_EditorReloaded(object sender, EventArgs e)
        {
            DoEdit(_currentEditable);
        }
        #endregion

        private void cmQuit_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("是否關閉此工具?", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == System.Windows.Forms.DialogResult.Yes)
                this.Close();
        }

        private void tsVersionLabel_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("必須重新啟動以套用新版, 是否現在重新啟動?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == System.Windows.Forms.DialogResult.Yes)
                Application.Restart();
        }

        private void toolStrip1_EndDrag(object sender, EventArgs e)
        {
            XmlHelper h = new XmlHelper("<Control/>");
            h.SetAttribute(".", "Name", toolStrip1.Name);
            h.AddElement(".", "X", toolStrip1.Location.X.ToString());
            h.AddElement(".", "Y", toolStrip1.Location.Y.ToString());
            MainForm.Storage.SetPropertyXml(toolStrip1.Name, "Location", h.GetElement("."));
        }

        private void CheckBeforeMove(CancelEventArgs e)
        {
            if (!_currentDataChanged) return;

            TreeNode selected = tvProjects.SelectedNode;
            if (selected == null) return;

            IEditorManager em = selected.Tag as IEditorManager;
            if (em == null) return;

            IEditable edit = em.CurrentEditor;
            if (edit == null) return;

            string msg = "編輯文件已變更且尚未儲存, 離開前是否要進行儲存?\n";
            msg += "按「是」儲存後離開。\n";
            msg += "按「否」不儲存離開。\n";
            msg += "按「取消」停留在原文件。\n";
            DialogResult r = MessageBox.Show(msg, "提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            if (r == DialogResult.Cancel)
                e.Cancel = true;
            else if (r == DialogResult.Yes)
            {
                if (edit.Valid)
                    edit.Save();
                else
                {
                    MessageBox.Show("文件內容有誤, 請修正後再行儲存", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                }
            }
            else
            {
                _currentDataChanged = false;
                rsbtnSave.Enabled = false;
            }
        }

        private void tvProjects_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            IEditorManager em = tvProjects.SelectedNode.Tag as IEditorManager;
            if (em == null) return;
            if (em.CurrentEditor == null) return;
            DoEdit(em.CurrentEditor);
        }

        private void cmSetupConfig_Click(object sender, EventArgs e)
        {
            SetupConfigForm setupForm = new SetupConfigForm();
            setupForm.StartPosition = FormStartPosition.CenterParent;
            setupForm.SetupChanged += delegate(object s, EventArgs arg)
            {
                MessageBox.Show("設定已變更, 請重啟本程式!", "通知", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
            };
            setupForm.ShowDialog();
        }

        private void cmProjectEditor_Click(object sender, EventArgs e)
        {
            ProjectPreferenceForm form = new ProjectPreferenceForm();
            form.StartPosition = FormStartPosition.CenterParent;
            form.Saved += delegate(object s, EventArgs arg)
            {
                ProjectPreferenceForm f = s as ProjectPreferenceForm;
                if (MainForm.CurrentProject != null)
                    this.LoadDevProject(MainForm.CurrentProject.Name, MainForm.CurrentProject.DevSite);
            };
            form.ShowDialog();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            IsClosing = true;
        }

        private void cmUpload_Click(object sender, EventArgs e)
        {
            DoUpload();
        }

        private void cmDownload_Click(object sender, EventArgs e)
        {
            DoDownload();
        }

        private void cmSetup_Click(object sender, EventArgs e)
        {
            DoSetup();
        }

        private void cmDeploy_Click(object sender, EventArgs e)
        {
            DoDeploy();
        }

        private void tsbtnDeployToPhysical_Click(object sender, EventArgs e)
        {
            DoDeployToPhysical();
        }

        private void tsbtnImportFromPhysical_Click(object sender, EventArgs e)
        {
            DoImportFromPhysical();
        }


    }
}
