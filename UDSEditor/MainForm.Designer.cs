namespace ProjectManager
{
    partial class MainForm
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("我的專案");
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.帳號AToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmLogin = new System.Windows.Forms.ToolStripMenuItem();
            this.cmSite = new System.Windows.Forms.ToolStripMenuItem();
            this.cmQuit = new System.Windows.Forms.ToolStripMenuItem();
            this.專案ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmAddProject = new System.Windows.Forms.ToolStripMenuItem();
            this.cmOpenProject = new System.Windows.Forms.ToolStripMenuItem();
            this.cmLoadProjectFromFile = new System.Windows.Forms.ToolStripMenuItem();
            this.cmProxyDeploy = new System.Windows.Forms.ToolStripMenuItem();
            this.工具TToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmSetupConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.cmProjectEditor = new System.Windows.Forms.ToolStripMenuItem();
            this.cmServiceConvert = new System.Windows.Forms.ToolStripMenuItem();
            this.cmToUDSService = new System.Windows.Forms.ToolStripMenuItem();
            this.說明HToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.cmCheckVersion = new System.Windows.Forms.ToolStripMenuItem();
            this.menuProject = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.imgs = new System.Windows.Forms.ImageList(this.components);
            this.actImgs = new System.Windows.Forms.ImageList(this.components);
            this.BottomToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.TopToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbtnJoinProject = new System.Windows.Forms.ToolStripButton();
            this.tsbtnLeaveProject = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.rsbtnSave = new System.Windows.Forms.ToolStripButton();
            this.tsbtnAdd = new System.Windows.Forms.ToolStripButton();
            this.tsbtnReload = new System.Windows.Forms.ToolStripButton();
            this.tsbtnRename = new System.Windows.Forms.ToolStripButton();
            this.tsbtnDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnExport = new System.Windows.Forms.ToolStripButton();
            this.tsbtnImport = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.cmDeploy = new System.Windows.Forms.ToolStripButton();
            this.tsbtnTest = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsVersionLabel = new System.Windows.Forms.ToolStripLabel();
            this.tsbtnDeployToPhysical = new System.Windows.Forms.ToolStripButton();
            this.tsbtnImportFromPhysical = new System.Windows.Forms.ToolStripButton();
            this.RightToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.LeftToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.ContentPanel = new System.Windows.Forms.ToolStripContentPanel();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tvProjects = new System.Windows.Forms.TreeView();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.cmUpload = new System.Windows.Forms.ToolStripButton();
            this.cmDownload = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.cmSetup = new System.Windows.Forms.ToolStripButton();
            this.tsEditMode = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.tsEditMode.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.帳號AToolStripMenuItem,
            this.專案ToolStripMenuItem,
            this.工具TToolStripMenuItem,
            this.說明HToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(1029, 26);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 帳號AToolStripMenuItem
            // 
            this.帳號AToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmLogin,
            this.cmSite,
            this.cmQuit});
            this.帳號AToolStripMenuItem.Name = "帳號AToolStripMenuItem";
            this.帳號AToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.帳號AToolStripMenuItem.Text = "帳號(&A)";
            // 
            // cmLogin
            // 
            this.cmLogin.Image = ((System.Drawing.Image)(resources.GetObject("cmLogin.Image")));
            this.cmLogin.Name = "cmLogin";
            this.cmLogin.Size = new System.Drawing.Size(189, 22);
            this.cmLogin.Text = "重新登入(&L)";
            this.cmLogin.Click += new System.EventHandler(this.cmLogin_Click);
            // 
            // cmSite
            // 
            this.cmSite.Name = "cmSite";
            this.cmSite.Size = new System.Drawing.Size(189, 22);
            this.cmSite.Text = "設定預設開發站台(&D)";
            this.cmSite.Click += new System.EventHandler(this.cmSite_Click);
            // 
            // cmQuit
            // 
            this.cmQuit.Name = "cmQuit";
            this.cmQuit.Size = new System.Drawing.Size(189, 22);
            this.cmQuit.Text = "關閉(&Q)";
            this.cmQuit.Click += new System.EventHandler(this.cmQuit_Click);
            // 
            // 專案ToolStripMenuItem
            // 
            this.專案ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmAddProject,
            this.cmOpenProject,
            this.cmLoadProjectFromFile,
            this.cmProxyDeploy});
            this.專案ToolStripMenuItem.Name = "專案ToolStripMenuItem";
            this.專案ToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            this.專案ToolStripMenuItem.Text = "專案(&M)";
            // 
            // cmAddProject
            // 
            this.cmAddProject.Image = ((System.Drawing.Image)(resources.GetObject("cmAddProject.Image")));
            this.cmAddProject.Name = "cmAddProject";
            this.cmAddProject.Size = new System.Drawing.Size(160, 22);
            this.cmAddProject.Text = "新增專案(&A)";
            this.cmAddProject.Click += new System.EventHandler(this.cmAddProject_Click);
            // 
            // cmOpenProject
            // 
            this.cmOpenProject.Image = ((System.Drawing.Image)(resources.GetObject("cmOpenProject.Image")));
            this.cmOpenProject.Name = "cmOpenProject";
            this.cmOpenProject.Size = new System.Drawing.Size(160, 22);
            this.cmOpenProject.Text = "開啟專案(&O)";
            // 
            // cmLoadProjectFromFile
            // 
            this.cmLoadProjectFromFile.Image = ((System.Drawing.Image)(resources.GetObject("cmLoadProjectFromFile.Image")));
            this.cmLoadProjectFromFile.Name = "cmLoadProjectFromFile";
            this.cmLoadProjectFromFile.Size = new System.Drawing.Size(160, 22);
            this.cmLoadProjectFromFile.Text = "從檔案載入專案";
            this.cmLoadProjectFromFile.Click += new System.EventHandler(this.cmLoadProjectFromFile_Click);
            // 
            // cmProxyDeploy
            // 
            this.cmProxyDeploy.Image = ((System.Drawing.Image)(resources.GetObject("cmProxyDeploy.Image")));
            this.cmProxyDeploy.Name = "cmProxyDeploy";
            this.cmProxyDeploy.Size = new System.Drawing.Size(160, 22);
            this.cmProxyDeploy.Text = "代理部署(&P)";
            this.cmProxyDeploy.Click += new System.EventHandler(this.cmProxyDeploy_Click);
            // 
            // 工具TToolStripMenuItem
            // 
            this.工具TToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmSetupConfig,
            this.cmProjectEditor,
            this.cmServiceConvert,
            this.cmToUDSService});
            this.工具TToolStripMenuItem.Name = "工具TToolStripMenuItem";
            this.工具TToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.工具TToolStripMenuItem.Text = "工具(&T)";
            // 
            // cmSetupConfig
            // 
            this.cmSetupConfig.Name = "cmSetupConfig";
            this.cmSetupConfig.Size = new System.Drawing.Size(228, 22);
            this.cmSetupConfig.Text = "設定(&S)";
            this.cmSetupConfig.Click += new System.EventHandler(this.cmSetupConfig_Click);
            // 
            // cmProjectEditor
            // 
            this.cmProjectEditor.Name = "cmProjectEditor";
            this.cmProjectEditor.Size = new System.Drawing.Size(228, 22);
            this.cmProjectEditor.Text = "專案編輯器(&E)";
            this.cmProjectEditor.Click += new System.EventHandler(this.cmProjectEditor_Click);
            // 
            // cmServiceConvert
            // 
            this.cmServiceConvert.Name = "cmServiceConvert";
            this.cmServiceConvert.Size = new System.Drawing.Size(228, 22);
            this.cmServiceConvert.Text = "UDS 轉實體 Service (&U)";
            this.cmServiceConvert.Click += new System.EventHandler(this.cmServiceConvert_Click);
            // 
            // cmToUDSService
            // 
            this.cmToUDSService.Name = "cmToUDSService";
            this.cmToUDSService.Size = new System.Drawing.Size(228, 22);
            this.cmToUDSService.Text = "實體 Service 轉換成 UDS (&P)";
            this.cmToUDSService.Click += new System.EventHandler(this.cmToUDSService_Click);
            // 
            // 說明HToolStripMenuItem
            // 
            this.說明HToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmAbout,
            this.cmCheckVersion});
            this.說明HToolStripMenuItem.Name = "說明HToolStripMenuItem";
            this.說明HToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.說明HToolStripMenuItem.Text = "說明(&H)";
            // 
            // cmAbout
            // 
            this.cmAbout.Image = ((System.Drawing.Image)(resources.GetObject("cmAbout.Image")));
            this.cmAbout.Name = "cmAbout";
            this.cmAbout.Size = new System.Drawing.Size(215, 22);
            this.cmAbout.Text = "關於 模組開發管理工具(&A)";
            this.cmAbout.Click += new System.EventHandler(this.cmAbout_Click);
            // 
            // cmCheckVersion
            // 
            this.cmCheckVersion.Image = ((System.Drawing.Image)(resources.GetObject("cmCheckVersion.Image")));
            this.cmCheckVersion.Name = "cmCheckVersion";
            this.cmCheckVersion.Size = new System.Drawing.Size(215, 22);
            this.cmCheckVersion.Text = "版本資訊(&V)";
            this.cmCheckVersion.Click += new System.EventHandler(this.cmCheckVersion_Click);
            // 
            // menuProject
            // 
            this.menuProject.Name = "menuProject";
            this.menuProject.Size = new System.Drawing.Size(61, 4);
            this.menuProject.Opening += new System.ComponentModel.CancelEventHandler(this.menuProject_Opening);
            // 
            // imgs
            // 
            this.imgs.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgs.ImageStream")));
            this.imgs.TransparentColor = System.Drawing.Color.Transparent;
            this.imgs.Images.SetKeyName(0, "projects");
            this.imgs.Images.SetKeyName(1, "project");
            this.imgs.Images.SetKeyName(2, "udt");
            this.imgs.Images.SetKeyName(3, "udts");
            this.imgs.Images.SetKeyName(4, "udss");
            this.imgs.Images.SetKeyName(5, "uds");
            this.imgs.Images.SetKeyName(6, "upload");
            this.imgs.Images.SetKeyName(7, "contracts");
            this.imgs.Images.SetKeyName(8, "contract");
            this.imgs.Images.SetKeyName(9, "package");
            this.imgs.Images.SetKeyName(10, "service");
            this.imgs.Images.SetKeyName(11, "outudt");
            this.imgs.Images.SetKeyName(12, "outuds");
            this.imgs.Images.SetKeyName(13, "module");
            this.imgs.Images.SetKeyName(14, "warning");
            this.imgs.Images.SetKeyName(15, "reload");
            this.imgs.Images.SetKeyName(16, "upload");
            this.imgs.Images.SetKeyName(17, "download");
            // 
            // actImgs
            // 
            this.actImgs.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("actImgs.ImageStream")));
            this.actImgs.TransparentColor = System.Drawing.Color.Transparent;
            this.actImgs.Images.SetKeyName(0, "save");
            this.actImgs.Images.SetKeyName(1, "login");
            this.actImgs.Images.SetKeyName(2, "reload");
            this.actImgs.Images.SetKeyName(3, "delete");
            this.actImgs.Images.SetKeyName(4, "document");
            this.actImgs.Images.SetKeyName(5, "info");
            this.actImgs.Images.SetKeyName(6, "rename");
            this.actImgs.Images.SetKeyName(7, "export");
            this.actImgs.Images.SetKeyName(8, "import");
            this.actImgs.Images.SetKeyName(9, "add");
            this.actImgs.Images.SetKeyName(10, "xml");
            this.actImgs.Images.SetKeyName(11, "edit");
            this.actImgs.Images.SetKeyName(12, "sql");
            this.actImgs.Images.SetKeyName(13, "test");
            this.actImgs.Images.SetKeyName(14, "table");
            this.actImgs.Images.SetKeyName(15, "exec_old");
            this.actImgs.Images.SetKeyName(16, "join");
            this.actImgs.Images.SetKeyName(17, "leave");
            this.actImgs.Images.SetKeyName(18, "download");
            this.actImgs.Images.SetKeyName(19, "upload");
            this.actImgs.Images.SetKeyName(20, "setup");
            this.actImgs.Images.SetKeyName(21, "deploy");
            this.actImgs.Images.SetKeyName(22, "deployToPhysical");
            this.actImgs.Images.SetKeyName(23, "importFromPhysical");
            this.actImgs.Images.SetKeyName(24, "exec");
            this.actImgs.Images.SetKeyName(25, "jseditor");
            // 
            // BottomToolStripPanel
            // 
            this.BottomToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.BottomToolStripPanel.Name = "BottomToolStripPanel";
            this.BottomToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.BottomToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.BottomToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // TopToolStripPanel
            // 
            this.TopToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.TopToolStripPanel.Name = "TopToolStripPanel";
            this.TopToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.TopToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.TopToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // toolStrip1
            // 
            this.toolStrip1.CanOverflow = false;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnJoinProject,
            this.tsbtnLeaveProject,
            this.toolStripSeparator4,
            this.rsbtnSave,
            this.tsbtnAdd,
            this.tsbtnReload,
            this.tsbtnRename,
            this.tsbtnDelete,
            this.toolStripSeparator1,
            this.tsbtnExport,
            this.tsbtnImport,
            this.toolStripSeparator2,
            this.cmDeploy,
            this.tsbtnTest,
            this.toolStripSeparator3,
            this.tsVersionLabel,
            this.tsbtnDeployToPhysical,
            this.tsbtnImportFromPhysical});
            this.toolStrip1.Location = new System.Drawing.Point(90, 25);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(335, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.EndDrag += new System.EventHandler(this.toolStrip1_EndDrag);
            // 
            // tsbtnJoinProject
            // 
            this.tsbtnJoinProject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnJoinProject.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnJoinProject.Image")));
            this.tsbtnJoinProject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnJoinProject.Name = "tsbtnJoinProject";
            this.tsbtnJoinProject.Size = new System.Drawing.Size(23, 22);
            this.tsbtnJoinProject.Text = "加入專案";
            this.tsbtnJoinProject.Click += new System.EventHandler(this.tsbtnJoinProject_Click);
            // 
            // tsbtnLeaveProject
            // 
            this.tsbtnLeaveProject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnLeaveProject.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnLeaveProject.Image")));
            this.tsbtnLeaveProject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnLeaveProject.Name = "tsbtnLeaveProject";
            this.tsbtnLeaveProject.Size = new System.Drawing.Size(23, 22);
            this.tsbtnLeaveProject.Text = "從專案中移除";
            this.tsbtnLeaveProject.Click += new System.EventHandler(this.tsbtnLeaveProject_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // rsbtnSave
            // 
            this.rsbtnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.rsbtnSave.Image = ((System.Drawing.Image)(resources.GetObject("rsbtnSave.Image")));
            this.rsbtnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.rsbtnSave.Name = "rsbtnSave";
            this.rsbtnSave.Size = new System.Drawing.Size(23, 22);
            this.rsbtnSave.Text = "儲存";
            this.rsbtnSave.Click += new System.EventHandler(this.rsbtnSave_Click);
            // 
            // tsbtnAdd
            // 
            this.tsbtnAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnAdd.Enabled = false;
            this.tsbtnAdd.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnAdd.Image")));
            this.tsbtnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnAdd.Name = "tsbtnAdd";
            this.tsbtnAdd.Size = new System.Drawing.Size(23, 22);
            this.tsbtnAdd.Text = "新增";
            this.tsbtnAdd.Click += new System.EventHandler(this.tsbtnAdd_Click);
            // 
            // tsbtnReload
            // 
            this.tsbtnReload.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnReload.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnReload.Image")));
            this.tsbtnReload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnReload.Name = "tsbtnReload";
            this.tsbtnReload.Size = new System.Drawing.Size(23, 22);
            this.tsbtnReload.Text = "重新整理";
            this.tsbtnReload.Click += new System.EventHandler(this.tsbtnReload_Click);
            // 
            // tsbtnRename
            // 
            this.tsbtnRename.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnRename.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnRename.Image")));
            this.tsbtnRename.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnRename.Name = "tsbtnRename";
            this.tsbtnRename.Size = new System.Drawing.Size(23, 22);
            this.tsbtnRename.Text = "重新命名";
            this.tsbtnRename.Click += new System.EventHandler(this.tsbtnRename_Click);
            // 
            // tsbtnDelete
            // 
            this.tsbtnDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnDelete.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnDelete.Image")));
            this.tsbtnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnDelete.Name = "tsbtnDelete";
            this.tsbtnDelete.Size = new System.Drawing.Size(23, 22);
            this.tsbtnDelete.Text = "刪除(&D)";
            this.tsbtnDelete.ToolTipText = "刪除";
            this.tsbtnDelete.Click += new System.EventHandler(this.tsbtnDelete_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbtnExport
            // 
            this.tsbtnExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnExport.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnExport.Image")));
            this.tsbtnExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnExport.Name = "tsbtnExport";
            this.tsbtnExport.Size = new System.Drawing.Size(23, 22);
            this.tsbtnExport.Text = "匯出";
            this.tsbtnExport.Click += new System.EventHandler(this.tsbtnExport_Click);
            // 
            // tsbtnImport
            // 
            this.tsbtnImport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnImport.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnImport.Image")));
            this.tsbtnImport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnImport.Name = "tsbtnImport";
            this.tsbtnImport.Size = new System.Drawing.Size(23, 22);
            this.tsbtnImport.Text = "匯入";
            this.tsbtnImport.Click += new System.EventHandler(this.tsbtnImport_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // cmDeploy
            // 
            this.cmDeploy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmDeploy.Image = ((System.Drawing.Image)(resources.GetObject("cmDeploy.Image")));
            this.cmDeploy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmDeploy.Name = "cmDeploy";
            this.cmDeploy.Size = new System.Drawing.Size(23, 22);
            this.cmDeploy.Text = "部署";
            this.cmDeploy.Click += new System.EventHandler(this.cmDeploy_Click);
            // 
            // tsbtnTest
            // 
            this.tsbtnTest.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnTest.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnTest.Image")));
            this.tsbtnTest.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnTest.Name = "tsbtnTest";
            this.tsbtnTest.Size = new System.Drawing.Size(23, 22);
            this.tsbtnTest.Text = "測試";
            this.tsbtnTest.Click += new System.EventHandler(this.cmTest_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // tsVersionLabel
            // 
            this.tsVersionLabel.ForeColor = System.Drawing.Color.Red;
            this.tsVersionLabel.Image = ((System.Drawing.Image)(resources.GetObject("tsVersionLabel.Image")));
            this.tsVersionLabel.Name = "tsVersionLabel";
            this.tsVersionLabel.Size = new System.Drawing.Size(84, 22);
            this.tsVersionLabel.Text = "已有更新版";
            this.tsVersionLabel.Visible = false;
            this.tsVersionLabel.Click += new System.EventHandler(this.tsVersionLabel_Click);
            // 
            // tsbtnDeployToPhysical
            // 
            this.tsbtnDeployToPhysical.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnDeployToPhysical.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnDeployToPhysical.Image")));
            this.tsbtnDeployToPhysical.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnDeployToPhysical.Name = "tsbtnDeployToPhysical";
            this.tsbtnDeployToPhysical.Size = new System.Drawing.Size(23, 22);
            this.tsbtnDeployToPhysical.Text = "部署到實體";
            this.tsbtnDeployToPhysical.Click += new System.EventHandler(this.tsbtnDeployToPhysical_Click);
            // 
            // tsbtnImportFromPhysical
            // 
            this.tsbtnImportFromPhysical.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnImportFromPhysical.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnImportFromPhysical.Image")));
            this.tsbtnImportFromPhysical.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnImportFromPhysical.Name = "tsbtnImportFromPhysical";
            this.tsbtnImportFromPhysical.Size = new System.Drawing.Size(23, 22);
            this.tsbtnImportFromPhysical.Text = "從實體 Service 匯入";
            this.tsbtnImportFromPhysical.Click += new System.EventHandler(this.tsbtnImportFromPhysical_Click);
            // 
            // RightToolStripPanel
            // 
            this.RightToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.RightToolStripPanel.Name = "RightToolStripPanel";
            this.RightToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.RightToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.RightToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // LeftToolStripPanel
            // 
            this.LeftToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.LeftToolStripPanel.Name = "LeftToolStripPanel";
            this.LeftToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.LeftToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.LeftToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // ContentPanel
            // 
            this.ContentPanel.Size = new System.Drawing.Size(950, 772);
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.splitContainer1);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(1029, 594);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 26);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(1029, 644);
            this.toolStripContainer1.TabIndex = 2;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.tsEditMode);
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip2);
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tvProjects);
            this.splitContainer1.Size = new System.Drawing.Size(1029, 594);
            this.splitContainer1.SplitterDistance = 314;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 2;
            // 
            // tvProjects
            // 
            this.tvProjects.ContextMenuStrip = this.menuProject;
            this.tvProjects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvProjects.HideSelection = false;
            this.tvProjects.ImageKey = "projects";
            this.tvProjects.ImageList = this.imgs;
            this.tvProjects.Location = new System.Drawing.Point(0, 0);
            this.tvProjects.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tvProjects.Name = "tvProjects";
            treeNode1.ImageKey = "project";
            treeNode1.Name = "Node0";
            treeNode1.Text = "我的專案";
            this.tvProjects.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.tvProjects.SelectedImageIndex = 0;
            this.tvProjects.ShowNodeToolTips = true;
            this.tvProjects.Size = new System.Drawing.Size(314, 594);
            this.tvProjects.TabIndex = 0;
            this.tvProjects.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvProjects_BeforeSelect);
            this.tvProjects.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvProjects_AfterSelect);
            this.tvProjects.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvProjects_NodeMouseClick);
            this.tvProjects.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tvProjects_MouseDoubleClick);
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmUpload,
            this.cmDownload,
            this.toolStripSeparator5,
            this.cmSetup});
            this.toolStrip2.Location = new System.Drawing.Point(3, 25);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(87, 25);
            this.toolStrip2.TabIndex = 1;
            // 
            // cmUpload
            // 
            this.cmUpload.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmUpload.Image = ((System.Drawing.Image)(resources.GetObject("cmUpload.Image")));
            this.cmUpload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmUpload.Name = "cmUpload";
            this.cmUpload.Size = new System.Drawing.Size(23, 22);
            this.cmUpload.Text = "同步至伺服器";
            this.cmUpload.Click += new System.EventHandler(this.cmUpload_Click);
            // 
            // cmDownload
            // 
            this.cmDownload.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmDownload.Image = ((System.Drawing.Image)(resources.GetObject("cmDownload.Image")));
            this.cmDownload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmDownload.Name = "cmDownload";
            this.cmDownload.Size = new System.Drawing.Size(23, 22);
            this.cmDownload.Text = "從伺服器同步";
            this.cmDownload.Click += new System.EventHandler(this.cmDownload_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // cmSetup
            // 
            this.cmSetup.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmSetup.Image = ((System.Drawing.Image)(resources.GetObject("cmSetup.Image")));
            this.cmSetup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmSetup.Name = "cmSetup";
            this.cmSetup.Size = new System.Drawing.Size(23, 22);
            this.cmSetup.Text = "設定";
            this.cmSetup.Click += new System.EventHandler(this.cmSetup_Click);
            // 
            // tsEditMode
            // 
            this.tsEditMode.CanOverflow = false;
            this.tsEditMode.Dock = System.Windows.Forms.DockStyle.None;
            this.tsEditMode.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel2});
            this.tsEditMode.Location = new System.Drawing.Point(82, 0);
            this.tsEditMode.Name = "tsEditMode";
            this.tsEditMode.Size = new System.Drawing.Size(68, 25);
            this.tsEditMode.TabIndex = 2;
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(56, 22);
            this.toolStripLabel2.Text = "編輯模式";
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1029, 670);
            this.Controls.Add(this.toolStripContainer1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "模組開發管理";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.tsEditMode.ResumeLayout(false);
            this.tsEditMode.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 專案ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cmAddProject;
        private System.Windows.Forms.ImageList imgs;
        private System.Windows.Forms.ContextMenuStrip menuProject;
        private System.Windows.Forms.ToolStripMenuItem 帳號AToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cmLogin;
        private System.Windows.Forms.ToolStripMenuItem cmSite;
        private System.Windows.Forms.ToolStripMenuItem cmOpenProject;
        private System.Windows.Forms.ToolStripMenuItem 說明HToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cmAbout;
        private System.Windows.Forms.ToolStripMenuItem cmCheckVersion;
        private System.Windows.Forms.ToolStripMenuItem cmQuit;
        private System.Windows.Forms.ImageList actImgs;
        private System.Windows.Forms.ToolStripPanel BottomToolStripPanel;
        private System.Windows.Forms.ToolStripPanel TopToolStripPanel;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton rsbtnSave;
        private System.Windows.Forms.ToolStripButton tsbtnAdd;
        private System.Windows.Forms.ToolStripButton tsbtnReload;
        private System.Windows.Forms.ToolStripButton tsbtnRename;
        private System.Windows.Forms.ToolStripButton tsbtnDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbtnExport;
        private System.Windows.Forms.ToolStripButton tsbtnImport;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbtnTest;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripLabel tsVersionLabel;
        private System.Windows.Forms.ToolStripPanel RightToolStripPanel;
        private System.Windows.Forms.ToolStripPanel LeftToolStripPanel;
        private System.Windows.Forms.ToolStripContentPanel ContentPanel;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView tvProjects;
        private System.Windows.Forms.ToolStripMenuItem 工具TToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cmSetupConfig;
        private System.Windows.Forms.ToolStripButton tsbtnJoinProject;
        private System.Windows.Forms.ToolStripButton tsbtnLeaveProject;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStrip tsEditMode;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripMenuItem cmProjectEditor;
        private System.Windows.Forms.ToolStripMenuItem cmLoadProjectFromFile;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton cmUpload;
        private System.Windows.Forms.ToolStripButton cmDownload;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton cmSetup;
        private System.Windows.Forms.ToolStripButton cmDeploy;
        private System.Windows.Forms.ToolStripMenuItem cmServiceConvert;
        private System.Windows.Forms.ToolStripMenuItem cmToUDSService;
        private System.Windows.Forms.ToolStripButton tsbtnDeployToPhysical;
        private System.Windows.Forms.ToolStripButton tsbtnImportFromPhysical;
        private System.Windows.Forms.ToolStripMenuItem cmProxyDeploy;
    }
}

