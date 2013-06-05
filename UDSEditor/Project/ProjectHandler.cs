using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using FISCA.DSAClient;
using ProjectManager.Login;
using ProjectManager.Project.UDT;
using ProjectManager.Project.UDS.Contract;
using ProjectManager.Project.UDS;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ProjectManager.Project
{
    class ProjectHandler
    {
        internal const string PROJECT_PS_PREFIX = "ischool.project.";

        internal string Name { get; private set; }

        private Connection DevConnection { get; set; }

        internal DevSiteLoginInfo DevSite { get; private set; }

        internal XmlElement Preference { get; private set; }

        internal ProjectHandler(string name, DevSiteLoginInfo info)
        {
            this.Name = name;
            DevConnection = info.TryConnect();
            DevSite = info;
            Preference = GetProjectPreference();
        }

        internal ProjectStatus Status { get; private set; }

        private ProjectHandler(string name, bool loadsucceed)
        {
            this.Name = name;
            this.Status = ProjectStatus.LoadFail;
        }

        internal ProjectHandler(string name)
        {
            this.Name = name;
            this.Status = ProjectStatus.Unload;
        }

        internal void Init(bool warring)
        {
            Status = ProjectStatus.Unload;

            Preference = GetProjectPreference();

            if (Preference == null)
            {
                if (warring)
                    MessageBox.Show("無法取得專案設定檔!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            XmlHelper pref = new XmlHelper(Preference);
            XmlElement devSiteElement = pref.GetElement("Property[@Name='DevSite']/DevSite");

            if (devSiteElement == null)
            {
                string msg = "無法取得專案『" + this.Name + "』開發站台連線設定, 是否重新設定開發站台位置?";
                ShowResetMessage(this.Name, msg);
            }
            else
            {
                DevSite = DevSiteLoginInfo.Load(devSiteElement);

                try
                {
                    DevConnection = DevSite.TryConnect();
                    Status = ProjectStatus.Succeed;
                }
                catch
                {
                    string msg = "專案『" + this.Name + "』開發站台連線失敗, 是否重新設定開發站台位置?";
                    ShowResetMessage(this.Name, msg);
                }
            }
        }

        private void ShowResetMessage(string name, string msg)
        {
            DialogResult dr = MessageBox.Show(msg, "無法連線", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            if (dr == DialogResult.No) return;

            ResetDevSiteForm dsf = new ResetDevSiteForm(name, DevSite);
            dsf.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            dsf.Completed += delegate(object sender, EventArgs arg)
            {
                SetDevSite(dsf.GetDevSite());
            };
            dsf.ShowDialog();
        }

        private void AddDefaultContract(XmlHelper pref)
        {
            XmlElement devElement = pref.GetElement("Property[@Name='DevSite']/DevSite");
            if (devElement != null)
            {
                XmlHelper h = new XmlHelper(devElement);
                if (h.GetElement("ContractName") == null)
                    h.AddElement(".", "ContractName", DevSiteLoginInfo.DEFAULT_CONTRACT_NAME);
            }
        }

        private void SetDevSite(DevSiteLoginInfo devSite)
        {
            this.DevSite = devSite;
            try
            {
                DevConnection = this.DevSite.TryConnect();
                Status = ProjectStatus.Succeed;
            }
            catch
            {
                Status = ProjectStatus.LoadFail;
            }

            XmlHelper h = new XmlHelper(this.Preference);
            XmlElement de = h.GetElement("Property/DevSite");
            XmlNode dep = de.ParentNode;
            dep.RemoveChild(de);
            de = devSite.XmlContent.GetElement(".");
            XmlNode node = dep.OwnerDocument.ImportNode(de, true);
            dep.AppendChild(node);

            if (MainForm.CurrentProject != null)
                MainForm.CurrentProject.UpdateProjectPreference(this.Preference);
        }

        internal UDTHandler GetUDT()
        {
            if (DevConnection == null)
            {
                return null;
            }

            Envelope env = DevConnection.SendRequest("UDTService.DDL.ListTableNames", new Envelope());
            XmlHelper h = new XmlHelper(env.Body);
            return new UDTHandler(this, h.GetElement("."));
        }

        internal UDSHandler GetUDSHandler()
        {
            if (DevConnection == null)
                return null;

            Envelope env = DevConnection.SendRequest("UDSManagerService.ListContracts", new Envelope());
            XmlHelper h = new XmlHelper(env.Body);
            return new UDSHandler(this, h.GetElement("."));
        }

        private static object lockObject = new object();
        internal void UpdateProjectPreference(XmlElement pref)
        {
            if (MainForm.CurrentProject == null)
                lockObject = new object();
            else
                lockObject = MainForm.CurrentProject;

            lock (lockObject)
            {
                string psName = PROJECT_PS_PREFIX + Name;
                XmlHelper req = new XmlHelper("<Request/>");
                req.AddElement(".", "Space");
                req.AddElement("Space", "Name", psName);
                XmlElement content = req.AddElement("Space", "Content");
                XmlCDataSection section = content.OwnerDocument.CreateCDataSection(pref.OuterXml);
                content.AppendChild(section);

                MainForm.LoginArgs.GreeningConnection.SendRequest("UpdateSpace", new Envelope(req));
                this.Preference = pref;
            }
        }

        public XmlElement GetProjectPreference()
        {
            string psName = PROJECT_PS_PREFIX + Name;
            XmlHelper req = new XmlHelper("<Request/>");
            req.AddElement(".", "Condition");
            req.AddElement("Condition", "Name", psName);
            Envelope evn = MainForm.LoginArgs.GreeningConnection.SendRequest("GetMySpace", new Envelope(req));
            XmlHelper rsp = new XmlHelper(evn.Body);

            string content = rsp.GetText("Space/Content");
            if (string.IsNullOrWhiteSpace(content))
                return null;

            XmlElement x = XmlHelper.ParseAsDOM(content);
            XmlHelper pref = new XmlHelper(x);
            AddDefaultContract(pref);

            return x;
        }

        internal XmlElement Query(string sql)
        {
            string psName = PROJECT_PS_PREFIX + Name;
            XmlHelper req = new XmlHelper("<Request/>");
            XmlElement sqlElement = req.AddElement(".", "SQL");
            XmlCDataSection section = sqlElement.OwnerDocument.CreateCDataSection(sql);
            sqlElement.AppendChild(section);

            Envelope evn = this.DevConnection.SendRequest("UDTService.DML.Query", new Envelope(req));
            XmlHelper rsp = new XmlHelper(evn.Body);
            return rsp.GetElement(".");
        }

        internal List<string> ListPhysicalTables()
        {
            string sql = "SELECT table_name FROM information_schema.tables where (table_schema='public' or table_schema='dbo') and table_name not like '_$_%'";
            XmlElement result = this.Query(sql);
            XmlHelper h = new XmlHelper(result);

            List<string> tables = new List<string>();
            foreach (XmlElement record in h.GetElements("Record/Column"))
            {
                string tableName = record.InnerText;
                if (tableName.StartsWith("_")) continue;

                tables.Add(tableName);
            }

            tables.Sort();
            return tables;
        }

        internal string LocalPath { get; private set; }

        internal string TryGetLocalPath()
        {
            string path = LocalPath;
            if (string.IsNullOrWhiteSpace(path))
                path = GetStoragePath();

            if (!string.IsNullOrWhiteSpace(path))
            {
                if (Directory.Exists(path))
                    return path;
            }
            return SetLocalPath();
        }

        private string GetStoragePath()
        {
            string path = MainForm.Storage.GetPropertyValue(SetLocalPathForm.LOCAL_FILE_PATH, this.Name);
            if (!string.IsNullOrWhiteSpace(path))
            {
                LocalPath = path;
                return LocalPath;
            }
            return string.Empty;
        }

        internal string SetLocalPath()
        {
            SetLocalPathForm pathForm = new SetLocalPathForm(this.Name);
            pathForm.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            pathForm.ShowDialog();

            LocalPath = MainForm.Storage.GetPropertyValue(SetLocalPathForm.LOCAL_FILE_PATH, this.Name);
            return LocalPath;
        }

        internal static bool ValidName(string p)
        {
            Regex reg = new Regex("^[a-zA-Z0-9_\\.-]+$");
            return reg.IsMatch(p);
        }

        internal Envelope SendRequest(string srvName, Envelope request)
        {
            try
            {
                return this.DevConnection.SendRequest(srvName, request);
            }
            catch (DSAServerException ex)
            {
                if (ex.Status == "511")
                {
                    ReConnect();
                    return DevConnection.SendRequest(srvName, request);
                }
                else
                {
                    throw;
                }
            }
        }

        internal void ReConnect()
        {
            DevConnection = DevSite.TryConnect();
        }

        //private string _deployHttpUrl;
        internal string DeployHttpURL
        {
            get
            {
                XmlHelper pref = new XmlHelper(Preference);
                return pref.GetText("Property[@Name='DeployHttpURL']");
            }
            set
            {
                XmlHelper pref = new XmlHelper(Preference);
                XmlElement e = pref.GetElement("Property[@Name='DeployHttpURL']");
                if (e != null)
                    Preference.RemoveChild(e);
                e = pref.AddElement(".", "Property", value);
                e.SetAttribute("Name", "DeployHttpURL");
                UpdateProjectPreference(Preference);
            }
        }

        internal static ProjectHandler CreateFailProjectHandler(string name)
        {
            return new ProjectHandler(name, false);
        }
    }

    internal enum ProjectStatus
    {
        Succeed, Unload, LoadFail
    }
}
