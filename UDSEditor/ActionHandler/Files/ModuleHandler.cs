using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using FISCA.DSAClient;
using System.Security.Cryptography;
using System.Xml;
using ModuleFileManager.Utils;
using System.Windows.Forms;
using ProjectManager.Downloader;
using ProjectManager.Util;


namespace ProjectManager.ActionHandler.Files
{
    class ModuleHandler
    {
        internal event EventHandler<ModuleStatusEventArgs> StatusChanged;

        internal string Name { get; private set; }
        internal FileSystemWatcher Watcher { get; private set; }
        internal XmlHelper Source { get; private set; }
        internal string LocalPath { get; private set; }
        internal ModuleStatus Status { get; private set; }
        internal TreeNode Node { get; private set; }
        internal string FtpURL { get; private set; }
        internal string HttpURL { get; private set; }
      
        public ModuleHandler(TreeNode Node, string moduleName)
        {
            this.Name = moduleName;            
            this.Node = Node;
            this.Reload();

           StartWatch();
            this.Status = this.CheckItemStatus();
        }

        internal void StartWatch()
        {
            this.LocalPath = MainForm.CurrentProject.TryGetLocalPath();

            FileSystemWatcher w = new FileSystemWatcher(this.LocalPath);            
            w.IncludeSubdirectories = true;
            w.SynchronizingObject = Node.TreeView;

            w.Created += new FileSystemEventHandler(w_OnChanged);
            w.Changed += new FileSystemEventHandler(w_OnChanged);
            w.Deleted += new FileSystemEventHandler(w_OnChanged);
            w.Renamed += new RenamedEventHandler(w_Renamed);
            w.EnableRaisingEvents = true;

            this.Watcher = w;
        }

        void w_Renamed(object sender, RenamedEventArgs e)
        {
            FileSystemWatcher w = sender as FileSystemWatcher;
            this.OnModuleFileChanged(w);
        }

        void w_OnChanged(object sender, FileSystemEventArgs e)
        {
            FileSystemWatcher w = sender as FileSystemWatcher;
            this.OnModuleFileChanged(w);
        }

        private void OnModuleFileChanged(FileSystemWatcher w)
        {
            this.Status = this.CheckItemStatus();
            if(StatusChanged != null)
                StatusChanged.Invoke(this, new ModuleStatusEventArgs(this.Status));
        }

        internal ModuleStatus CheckItemStatus()
        {
            if (string.IsNullOrWhiteSpace(this.LocalPath))
            {
                SetItemWarning(this.Node);
                return ModuleStatus.LocalPathDoNotExist;
            }

            DirectoryInfo dir = new DirectoryInfo(this.LocalPath);
            if (!dir.Exists)
            {
                SetItemWarning(this.Node, "指定路徑不存在 : " + this.LocalPath);
                return ModuleStatus.LocalPathDoNotExist;
            }

            ModifierCollection mc = this.CheckUploadModifier();
            if (mc.Count > 0)
            {
                this.Node.ImageKey = "reload";
                this.Node.SelectedImageKey = "reload";
                this.Node.ToolTipText = "本機檔案已變更, 可進行同步處理";
                return ModuleStatus.Modified;
            }
            this.Node.ImageKey = "module";
            this.Node.SelectedImageKey = "module";
            this.Node.ToolTipText = "已完成同步";
            return ModuleStatus.Nomal;
        }

        private void SetItemWarning(TreeNode node)
        {
            SetItemWarning(node, string.Empty);
        }

        private void SetItemWarning(TreeNode node, string tip)
        {
            if (string.IsNullOrWhiteSpace(tip))
                tip = "未指定本機對應路徑";
            node.ImageKey = "warning";
            node.SelectedImageKey = "warning";
            node.ToolTipText = tip;
        }

        public void Dispose()
        {
            if (Watcher != null)
            {
                Watcher.EnableRaisingEvents = false;
                Watcher.Dispose();
            }
        }

        public bool Reload()
        {
            XmlHelper h = new XmlHelper();
            h.AddElement(".", "ModuleName", this.Name);
            h.AddElement(".", "ContainDirectory", "true");

            Envelope rsp = MainForm.LoginArgs.SendModuleRequest("GetMyModules", new Envelope(h));
            h = new XmlHelper(rsp.Body);
            XmlElement moduleElement = h.GetElement("Module");

            if (moduleElement != null)
            {
                this.Source = new XmlHelper(h.GetElement("Module"));
                this.FtpURL = h.GetText("Module/FTP");
                this.HttpURL = h.GetText("Module/HTTP");
                this.CheckItemStatus();
                return true;
            }
            return false;
        }

        public void RemoveItem()
        {
            this.Dispose();        
        }

        public void Remove()
        {
            XmlHelper h = new XmlHelper();
            h.AddElement(".", "ModuleName", this.Name);
            MainForm.LoginArgs.SendModuleRequest("RemoveModule", new Envelope(h));
            this.RemoveItem();
        }

        public ModifierCollection CheckUploadModifier()
        {
            ModifierCollection modifiers = new ModifierCollection();
            if (string.IsNullOrWhiteSpace(this.LocalPath)) return modifiers;

            XmlElement dirElement = this.Source.GetElement("Directory");
            DirectoryInfo dir = new DirectoryInfo(this.LocalPath);
            return this.CompareDir(dir, dirElement);
        }

        private ModifierCollection CompareDir(DirectoryInfo dir, XmlElement dirElement)
        {
            ModifierCollection list = new ModifierCollection();

            XmlHelper h = new XmlHelper(dirElement);
            foreach (XmlElement subDir in h.GetElements("Directory"))
            {
                string name = subDir.GetAttribute("Name");
                DirectoryInfo sd = null;
                foreach (DirectoryInfo sub in dir.GetDirectories())
                {
                    if (sub.Name == name)
                    {
                        sd = sub;
                        break;
                    }
                }
                if (sd == null)
                {
                    Modifier m = new Modifier();
                    m.ActionMode = ActionType.SendDelete;
                    m.FileMode = FileType.Directory;
                    m.Path = PathHelper.GetServerPath(this, subDir);
                    list.Add(m);
                }
                else
                {
                    ModifierCollection mc = CompareDir(sd, subDir);
                    list.Import(mc);
                }
            }

            foreach (DirectoryInfo sub in dir.GetDirectories())
            {
                if (h.GetElement("Directory[@Name='" + sub.Name + "']") == null)
                {
                    ModifierCollection mc = this.GetNoDirModifiers(this.LocalPath, sub);
                    list.Import(mc);
                }
            }

            foreach (XmlElement subFile in h.GetElements("File"))
            {
                string name = subFile.GetAttribute("Name");
                FileInfo file = null;
                foreach (FileInfo sub in dir.GetFiles())
                {
                    if (sub.Name != name)
                        continue;
                    file = sub;
                    break;
                }
                if (file == null)
                {
                    Modifier m = new Modifier();
                    m.ActionMode = ActionType.SendDelete;
                    m.FileMode = FileType.File;
                    m.Path = PathHelper.GetServerPath(this, subFile);
                    list.Add(m);
                }
                else
                {
                    string md5 = subFile.GetAttribute("MD5");
                    string fileMD5 = this.ComputeFileMD5(file);

                    if (md5 != fileMD5)
                    {
                        Modifier m = new Modifier();
                        m.ActionMode = ActionType.UpdateFile;
                        m.FileMode = FileType.File;
                        m.Path = file.FullName;
                        list.Add(m);
                    }
                }
            }

            foreach (FileInfo sub in dir.GetFiles())
            {
                if (h.GetElement("File[@Name='" + sub.Name + "']") == null)
                {
                    Modifier m = new Modifier();
                    m.ActionMode = ActionType.UpdateFile;
                    m.FileMode = FileType.File;
                    m.Path = sub.FullName;
                    list.Add(m);
                }
            }
            return list;
        }

        private ModifierCollection GetNoDirModifiers(string moduleRootDir, DirectoryInfo dir)
        {
            ModifierCollection list = new ModifierCollection();

            Modifier m = new Modifier();
            m.ActionMode = ActionType.UpdateFile;
            m.FileMode = FileType.Directory;
            m.Path = PathHelper.GetServerPath(moduleRootDir, dir.FullName);
            list.Add(m);

            foreach (FileInfo file in dir.GetFiles())
            {
                Modifier fm = new Modifier();
                fm.FileMode = FileType.File;
                fm.ActionMode = ActionType.UpdateFile;
                fm.Path = file.FullName;
                list.Add(fm);
            }

            foreach (DirectoryInfo sub in dir.GetDirectories())
            {
                list.Import(GetNoDirModifiers(moduleRootDir, sub));
            }

            return list;
        }

        public List<DownloadFile> CheckDownloadFile()
        {
            List<DownloadFile> files = new List<DownloadFile>();
            if (string.IsNullOrWhiteSpace(this.LocalPath)) return files;

            XmlElement dirElement = this.Source.GetElement("Directory");
            DirectoryInfo dir = new DirectoryInfo(this.LocalPath);
            return this.CompareDownloadDir(dir, dirElement);
        }

        public List<DownloadFile> CompareDownloadDir(DirectoryInfo dir, XmlElement dirElement)
        {
            List<DownloadFile> list = new List<DownloadFile>();
            XmlHelper h = new XmlHelper(dirElement);

            if (!dir.Exists)
                dir.Create();

            //TODO HELELE
            foreach (DirectoryInfo subDir in dir.GetDirectories())
            {
                bool contains = false;
                foreach (XmlElement subDirElement in h.GetElements("Directory"))
                {
                    if (subDirElement.GetAttribute("Name") != subDir.Name)
                        continue;
                    contains = true;
                }

                if (!contains)
                    subDir.Delete(true);
            }

            foreach (FileInfo file in dir.GetFiles())
            {
                bool contains = false;
                foreach (XmlElement subDirElement in h.GetElements("File"))
                {
                    if (subDirElement.GetAttribute("Name") != file.Name)
                        continue;
                    contains = true;
                }

                if (!contains)
                    file.Delete();
            }

            foreach (XmlElement subDirElement in h.GetElements("Directory"))
            {
                string name = subDirElement.GetAttribute("Name");
                DirectoryInfo dirInfo = null;
                foreach (DirectoryInfo subDir in dir.GetDirectories())
                {
                    if (subDir.Name != name) continue;
                    dirInfo = subDir;
                }
                if (dirInfo == null) //本機沒有, 要新增                
                    dirInfo = dir.CreateSubdirectory(name);

                List<DownloadFile> subList = this.CompareDownloadDir(dirInfo, subDirElement);
                list.AddRange(subList);
            }

            foreach (XmlElement subElement in h.GetElements("File"))
            {
                string name = subElement.GetAttribute("Name");
                FileInfo fileInfo = null;
                foreach (FileInfo file in dir.GetFiles())
                {
                    if (file.Name != name) continue;
                    fileInfo = file;
                }
                XmlHelper subHelper = new XmlHelper(subElement);
                string path = Path.Combine(dir.FullName, name);
                string url = PathHelper.GetHttpFilePath(this.Source, subElement);
                decimal size = subHelper.TryGetDecimal("@Size", 0);
                if (fileInfo == null) //本機沒有, 要新增                
                {
                    AddDownloadFile(list, path, url, size);
                }
                else //本機有, 要比對 MD5
                {
                    string md5 = this.ComputeFileMD5(fileInfo);
                    if (subElement.GetAttribute("MD5") == md5) continue;

                    AddDownloadFile(list, path, url, size);
                }
            }
            return list;
        }

        private void AddDownloadFile(List<DownloadFile> list, string path, string url, decimal size)
        {
            DownloadFile file = new DownloadFile(url, path, size);
            list.Add(file);
        }

        private string ComputeFileMD5(FileInfo file)
        {
            if (!file.Exists) return string.Empty;

            FileStream myIFS = file.OpenRead();
            MD5 md5 = MD5.Create();
            byte[] hash = md5.ComputeHash(myIFS);
            myIFS.Close();

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString().ToLower();
        }

        public bool LocalPathExists
        {
            get
            {
                return Directory.Exists(this.LocalPath);
            }
        }

        public static ModuleHandler CreateNew(string projectName, string desc, TreeNode node)
        {
            XmlHelper h = new XmlHelper();
            XmlElement xml = h.AddElement("Module");
            h.SetAttribute("Module", "Name", projectName);
            XmlElement memo = h.AddElement("Module", "Memo");
            XmlCDataSection cdata = xml.OwnerDocument.CreateCDataSection(desc);
            memo.AppendChild(cdata);

            string localpath = MainForm.CurrentProject.TryGetLocalPath();
            DirectoryInfo dir = new DirectoryInfo(localpath);
            h.AddElement(".", GetDirElement(dir).GetElement("."));

            Envelope env = new Envelope(h);
            env = MainForm.LoginArgs.SendModuleRequest("AddModule", env);
                        
            h = new XmlHelper(env.Body);
            return new ModuleHandler(node, projectName);
        }

        public static bool Exists(string projectName)
        {
            XmlHelper h = new XmlHelper();
            h.AddElement(".", "ModuleName", projectName);
            h.AddElement(".", "ContainDirectory", "false");

            Envelope rsp = MainForm.LoginArgs.SendModuleRequest("GetMyModules", new Envelope(h));
            h = new XmlHelper(rsp.Body);
            XmlElement moduleElement = h.GetElement("Module");

            if (moduleElement != null)            
                return true;
            
            return false;
        }

        private static XmlHelper GetDirElement(DirectoryInfo dir)
        {
            XmlHelper h = new XmlHelper("<Directory/>");
            h.SetAttribute(".", "Name", dir.Name);

            foreach (DirectoryInfo subDir in dir.EnumerateDirectories("*", SearchOption.TopDirectoryOnly))
            {
                XmlHelper sub = GetDirElement(subDir);
                h.AddElement(".", sub.GetElement("."));
            }

            foreach (FileInfo subFile in dir.EnumerateFiles("*", SearchOption.TopDirectoryOnly))
            {
                XmlElement e = h.AddElement(".", "File");
                e.SetAttribute("Name", subFile.Name);
            }
            return h;
        }

        internal void DeletePath(string path)
        {
            XmlHelper content = new XmlHelper();
            content.AddElement(".", "ModuleName", this.Name);
            content.AddElement(".", "FilePath", path);

            Envelope req = new Envelope(content);
            MainForm.LoginArgs.SendModuleRequest("DeletePath", req);

            this.Reload();
        }
    }

    enum ModuleStatus
    {
        LocalPathDoNotExist, Modified, Nomal
    }

    class ModuleStatusEventArgs : EventArgs
    {
        internal ModuleStatus Status {get; private set;}
        internal ModuleStatusEventArgs(ModuleStatus status)
        {
            this.Status = status;
        }
    }
}
