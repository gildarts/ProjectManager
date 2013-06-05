using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FISCA.DSAClient;
using System.Xml;
using System.IO;
using ProjectManager.Util;
using System.Diagnostics;

namespace ProjectManager.ActionHandler.Files
{
    public partial class FileUIEditor : UserControl
    {
        private FileNodeHandler _fileNodeHandler;
        private ModuleHandler _moduleHandler;

        internal FileUIEditor(FileNodeHandler fileNodeHandler)
        {
            InitializeComponent();
            _fileNodeHandler = fileNodeHandler;
            _moduleHandler = fileNodeHandler.ModuleHandler;
        }

        private void FileUIEditor_Load(object sender, EventArgs e)
        {
            this.Reload();
        }

        internal void Reload()
        {
            treeDir.Nodes.Clear();
            txtLocalPath.Text = MainForm.CurrentProject.TryGetLocalPath();
            txtHttp.Text = MainForm.LoginArgs.FtpURL;

            string projectName = MainForm.CurrentProject.Name;
            XmlHelper h = new XmlHelper();
            h.AddElement(".", "ModuleName", projectName);
            Envelope env = MainForm.LoginArgs.SendModuleRequest("GetMyModules", new Envelope(h));
            h = new XmlHelper(env.Body);

            XmlElement rootDirElement = _moduleHandler.Source.GetElement("Directory");
            TreeNode rootNode = treeDir.Nodes.Add(rootDirElement.GetAttribute("Name"));
            rootNode.Tag = rootDirElement;
            rootNode.ImageIndex = 0;
            ImportNode(rootDirElement, rootNode);
        }

        private void ImportNode(XmlElement element, TreeNode treeNode)
        {
            XmlHelper h = new XmlHelper(element);
            decimal size = h.TryGetDecimal("@Size", 0);
            treeNode.ToolTipText = "Size :" + CalcUtil.GetVisualSize(size);

            foreach (XmlElement dirElement in h.GetElements("Directory"))
            {
                TreeNode tn = treeNode.Nodes.Add(dirElement.GetAttribute("Name"));
                tn.Tag = dirElement;
                ImportNode(dirElement, tn);
                tn.ImageIndex = 0;
                tn.SelectedImageIndex = 1;
            }

            foreach (XmlElement fileElement in h.GetElements("File"))
            {
                string name = fileElement.GetAttribute("Name");
                FileInfo fileInfo = new FileInfo(name);
                string ext = fileInfo.Extension.ToLower();

                if (ext.StartsWith("."))
                    ext = ext.Remove(0, 1);

                TreeNode tn = treeNode.Nodes.Add(fileElement.GetAttribute("Name"));
                tn.Tag = fileElement;

                if (imgs.Images.ContainsKey(ext))
                {
                    tn.ImageKey = ext;
                    tn.SelectedImageKey = ext;
                }
                else
                {
                    tn.ImageIndex = 2;
                    tn.SelectedImageIndex = 2;
                }
                XmlHelper fh = new XmlHelper(fileElement);
                decimal fsize = fh.TryGetDecimal("@Size", 0);
                tn.ToolTipText = "Size :" + CalcUtil.GetVisualSize(fsize);
            }         
        }

        private void treeDir_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode rootNode = e.Node.TreeView.Nodes[0];
            XmlElement dirElement = rootNode.Tag as XmlElement;
            XmlElement moduleElement = dirElement.ParentNode as XmlElement;
            XmlHelper h = new XmlHelper(moduleElement);
            string http = h.GetText("HTTP");

            TreeNode currentNode = e.Node;
            string path = string.Empty;

            while (currentNode != rootNode)
            {
                path = "/" + currentNode.Text + path;
                currentNode = currentNode.Parent;
            }

            txtHttp.Text = http + path;
                       
            if (!string.IsNullOrWhiteSpace(_moduleHandler.LocalPath))
                txtLocalPath.Text = PathHelper.CombineLocalPath(_moduleHandler.LocalPath, path);
        }

        private void treeDir_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            treeDir.SelectedNode = e.Node;
        }

        private void treeDir_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
                OpenFile(txtLocalPath.Text);
        }

        private void OpenFile(string filename)
        {
            if (!File.Exists(filename)) return;

            try
            {
                Process.Start(filename);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmDeleteFile_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("確定刪除所選檔案?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.Yes) return;

            if (treeDir.SelectedNode == null)
                return;

            TreeNode currentNode = treeDir.SelectedNode;
            string path = PathHelper.GetNodeFullName(currentNode);

            try
            {
                _moduleHandler.DeletePath(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show("刪除伺服器檔案發生錯誤 : \n" + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string filepath = PathHelper.CombineLocalPath(_moduleHandler.LocalPath, path);
            try
            {
                if (Directory.Exists(filepath))
                    Directory.Delete(filepath, true);
                else if (File.Exists(filepath))
                    File.Delete(filepath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("刪除本機檔案發生錯誤 : \n" + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            treeDir.Nodes.Remove(treeDir.SelectedNode);
        }

        private void cmShowDir_Click(object sender, EventArgs e)
        {
            string dir = txtLocalPath.Text;

            if (!IsDir(treeDir.SelectedNode))
            {
                FileInfo file = new FileInfo(txtLocalPath.Text);
                dir = file.Directory.FullName;
            }
            Process.Start(dir);
        }

        private bool IsDir(TreeNode node)
        {
            if (node.ImageIndex != 0 && node.ImageIndex != 1) return false;
            return true;
        }

        private void cmOpenFile_Click(object sender, EventArgs e)
        {
            OpenFile(txtLocalPath.Text);
        }

        private void treeDir_DragDrop(object sender, DragEventArgs e)
        {
            Point p = treeDir.PointToClient(new Point(e.X, e.Y));
            TreeViewHitTestInfo info = treeDir.HitTest(p);
            if (info == null) return;

            bool fd = false;
            foreach (string each in e.Data.GetFormats())
            {
                if (each != DataFormats.FileDrop) continue;
                fd = true;
            }

            if (!fd) return;

            TreeNode target = info.Node;
            XmlElement nodeElement = target.Tag as XmlElement;

            string targetPath = PathHelper.GetNodeDirectory(target);

            object dropObjectArray = e.Data.GetData(DataFormats.FileDrop);
            string[] dropFiles = dropObjectArray as string[];

            StringBuilder sb = new StringBuilder();
            XmlHelper content = new XmlHelper();
            content.AddElement(".", "ModuleName", _moduleHandler.Name);
            List<UploadFile> files = new List<UploadFile>();

            bool prefed = !string.IsNullOrWhiteSpace(_moduleHandler.LocalPath);

            foreach (string filename in dropFiles)
            {
                if (Directory.Exists(filename))
                {
                    DirectoryInfo dir = new DirectoryInfo(filename);
                    string dirName = targetPath + "/" + dir.Name;
                    content.AddElement(".", "Path", dirName);

                    if (prefed)
                    {
                        string copyTo = Path.Combine(_moduleHandler.LocalPath, targetPath, dir.Name);
                        Directory.CreateDirectory(copyTo);
                    }

                    foreach (DirectoryInfo sub in dir.GetDirectories("*", SearchOption.AllDirectories))
                    {
                        string subDirName = dirName + sub.FullName.Replace(dir.FullName, "").Replace(@"\", "/");
                        content.AddElement(".", "Path", subDirName);

                        if (prefed)
                        {
                            string copyTo = PathHelper.GetLocalFileFullName(_moduleHandler.LocalPath, targetPath, dir, sub);
                            Directory.CreateDirectory(copyTo);
                        }
                    }

                    foreach (FileInfo sub in dir.GetFiles("*", SearchOption.AllDirectories))
                    {
                        string subFileName = dirName + sub.FullName.Replace(dir.FullName, "").Replace(@"\", "/");
                        UploadFile uf = new UploadFile();
                        uf.File = sub;
                        uf.ServerPath = subFileName;
                        files.Add(uf);

                        if (prefed)
                        {
                            string copyTo = PathHelper.GetLocalFileFullName(_moduleHandler.LocalPath, targetPath, dir, sub);
                            sub.CopyTo(copyTo, true);
                        }
                    }
                }
                else
                {
                    UploadFile uf = new UploadFile();
                    FileInfo file = new FileInfo(filename);
                    uf.File = file;
                    uf.ServerPath = PathHelper.CombineTargetPath(targetPath, uf.File.Name);
                    files.Add(uf);

                    if (prefed)
                    {
                        string copyTo = Path.Combine(_moduleHandler.LocalPath, targetPath, file.Name);
                        file.CopyTo(copyTo, true);
                    }
                }
                sb.Append(filename).Append("\n");
            }

            if (content.GetElement("Path") != null)
                MainForm.LoginArgs.SendModuleRequest("PrepareDirectory", new Envelope(content));

            if (files.Count > 0)
            {
                string ftp = PathHelper.GetFtpPath(MainForm.LoginArgs.FtpURL, MainForm.LoginArgs.GreeningID, _moduleHandler.Name);
                ProgressForm pf = new ProgressForm(files.ToArray(), ftp, MainForm.LoginArgs.FtpUser, MainForm.LoginArgs.FtpPassword);
                pf.ShowDialog();
            }

            _moduleHandler.Reload();
            this.Reload();
        }

        private void treeDir_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void menuDir_Opening(object sender, CancelEventArgs e)
        {
            if (treeDir.SelectedNode == null || treeDir.SelectedNode == treeDir.Nodes[0])
            {
                e.Cancel = true;
                return;
            }
            cmDeleteFile.Text = "刪除檔案(&D) - " + PathHelper.GetNodeFullName(treeDir.SelectedNode) + "";

            cmOpenFile.Enabled = true;
            if (IsDir(treeDir.SelectedNode))
                cmOpenFile.Enabled = false;
        }
    }
}
