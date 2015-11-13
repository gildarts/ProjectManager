using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FISCA.DSAClient;
using System.IO;
using ProjectManager.Util;
using ProjectManager.Downloader;
using ProjectManager.Project;
using System.Text.RegularExpressions;

namespace ProjectManager.ActionHandler.Files
{
    internal class FileNodeHandler : INodeHandler, IEditorManager, IReloadable, IDeleteable, ISyncUpload, ISyncDownload, ISetupable
    {
        public TreeNode Node { get; private set; }
        internal ModuleHandler ModuleHandler { get; private set; }
        internal string ProjectName { get; private set; }

        internal FileNodeHandler(TreeNode treeNode)
        {
            this.Node = treeNode;

            this.Reload();
        }

        //void ModuleHandler_StatusChanged(object sender, ModuleStatusEventArgs e)
        //{
        //    if (NeedUploadChanged != null)
        //    {
        //        if (e.Status == ModuleStatus.Modified)
        //            NeedUploadChanged.Invoke(this, new NeedStatusEventArgs(true));
        //        else
        //            NeedUploadChanged.Invoke(this, new NeedStatusEventArgs(false));
        //    }
        //}

        #region IEditorManager 成員

        public List<IEditable> Editors { get; private set; }

        public IEditable CurrentEditor { get; set; }

        #endregion

        #region IReloadable 成員

        public void Reload()
        {
            this.ProjectName = MainForm.CurrentProject.Name;

            try
            {
                if (!ModuleHandler.Exists(ProjectName))
                    ModuleHandler = ModuleHandler.CreateNew(ProjectName, string.Empty, this.Node);
                else
                    ModuleHandler = new ModuleHandler(this.Node, MainForm.CurrentProject.Name);
            }
            catch (Exception ex)
            {
                string msg = ex.Message;

                string err = "檔案管理服務無法使用({0})";
                if (!ProjectHandler.ValidName(ProjectName))
                    this.Node.Text = string.Format(err, "專案名稱含有不合法字元");
                else
                {
                    Regex reg = new Regex("ischool.dsa.exception.DSAServiceException: (.*)\n");
                    Match m = reg.Match(msg);
                    if (m.Success)
                        this.Node.Text = string.Format(err, m.Groups[1].Value);
                    else
                        this.Node.Text = string.Format(err, "檔案管理服務初始化失敗");
                    this.Node.ToolTipText = ex.Message;
                }

                this.Editors = new List<IEditable>();
                return;
            }

            //ModuleHandler.StatusChanged += new EventHandler<ModuleStatusEventArgs>(ModuleHandler_StatusChanged);
            if (this.Editors == null)
            {
                this.Editors = new List<IEditable>();
                FileEditable edit = new FileEditable(this);

                Editors.Add(edit);
                this.CurrentEditor = edit;
            }
            else
            {
                FileEditable fe = CurrentEditor as FileEditable;
                FileUIEditor ui = fe.Editor as FileUIEditor;
                ui.Reload();
            }

            if (Directory.Exists(ModuleHandler.LocalPath))
                this.Node.Text = "檔案管理 ( 本機路徑 : " + ModuleHandler.LocalPath + " )";
            else
                this.Node.Text = "檔案管理 ( 本機路徑不存在 : " + ModuleHandler.LocalPath + " )";

            ModuleHandler.CheckItemStatus();
        }

        #endregion

        #region IDeleteable 成員

        public void Delete()
        {
            if (MessageBox.Show("確定刪除所有檔案?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.Yes) return;

            try
            {
                ModuleHandler.Remove();
            }
            catch (Exception ex)
            {
                MessageBox.Show("刪除失敗 : " + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public string TitleOfDelete
        {
            get { return "刪除模組檔案"; }
        }

        #endregion

        #region ISyncUpload 成員

        public void Upload()
        {
            string msg = "確定要「上傳」差異檔案到主機？這會覆蓋主機的檔案。";
            if (MessageBox.Show(msg, "檔案同步", MessageBoxButtons.YesNo) == DialogResult.No)
                return;


            if (!ModuleHandler.LocalPathExists) return;

            string moduleName = ModuleHandler.Name;

            ModifierCollection list = ModuleHandler.CheckUploadModifier();
            List<UploadFile> files = new List<UploadFile>();
            XmlHelper req = new XmlHelper();
            req.AddElement(".", "ModuleName", moduleName);

            foreach (Modifier m in list)
            {
                if (m.ActionMode == ActionType.SendDelete)
                    ModuleHandler.DeletePath(m.Path);
                else if (m.ActionMode == ActionType.UpdateFile && m.FileMode == FileType.Directory)
                {
                    req.AddElement(".", "Path", m.Path);
                }
                else if (m.ActionMode == ActionType.UpdateFile && m.FileMode == FileType.File)
                {
                    UploadFile f = new UploadFile();
                    FileInfo file = new FileInfo(m.Path);
                    f.File = file;
                    f.ServerPath = PathHelper.GetServerPath(ModuleHandler.LocalPath, m.Path);
                    files.Add(f);
                }
            }

            if (req.GetElement("Path") != null)
                MainForm.LoginArgs.SendModuleRequest("PrepareDirectory", new Envelope(req));

            if (files.Count > 0)
            {
                string ftp = PathHelper.GetFtpPath(MainForm.LoginArgs.FtpURL, MainForm.LoginArgs.GreeningID, moduleName);
                ProgressForm pf = new ProgressForm(files.ToArray(), ftp, MainForm.LoginArgs.FtpUser, MainForm.LoginArgs.FtpPassword);
                pf.ShowDialog();
            }

            ModuleHandler.Reload();

            //if (NeedUploadChanged != null)
            //    NeedUploadChanged.Invoke(this, new NeedStatusEventArgs(false));

            FileEditable fe = CurrentEditor as FileEditable;
            FileUIEditor ui = fe.Editor as FileUIEditor;
            ui.Reload();
        }

        #endregion

        #region ISyncDownload 成員

        public void Download()
        {
            if (!ModuleHandler.LocalPathExists) return;

            string msg = "確定要「下載」差異檔案到本機？這會覆蓋本機的檔案。";
            if (MessageBox.Show(msg, "檔案同步", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            string moduleName = ModuleHandler.Name;
            List<DownloadFile> list = ModuleHandler.CheckDownloadFile();

            if (list.Count == 0) return;

            ModuleHandler.Watcher.EnableRaisingEvents = false;
            DownloadForm dlForm = new DownloadForm(list.ToArray());
            dlForm.FormClosed += new FormClosedEventHandler(dlForm_FormClosed);
            dlForm.ShowDialog();

            //if (NeedDownloadChanged != null)
            //    NeedDownloadChanged.Invoke(this, new NeedStatusEventArgs(false));

            FileEditable fe = CurrentEditor as FileEditable;
            FileUIEditor ui = fe.Editor as FileUIEditor;
            ui.Reload();
        }

        void dlForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ModuleHandler.Reload();
            ModuleHandler.Watcher.EnableRaisingEvents = true;
        }
        #endregion

        #region ISetupable 成員

        public string SetupTitle
        {
            get { return "設定本機路徑"; }
        }

        public void Setup()
        {
            MainForm.CurrentProject.SetLocalPath();
            Reload();
        }

        #endregion

        #region INodeHandler 成員

        public bool IsFirstClick { get; private set; }

        public void OnFirstClick() { }

        public void OnClick() { }

        #endregion
    }
}
