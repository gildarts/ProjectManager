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
            HandleConnectResult(Connect(), warring);
        }

        /// <summary>取得專案設定並連線開發站台；不碰 UI，可在背景執行緒呼叫</summary>
        internal ConnectResult Connect()
        {
            Status = ProjectStatus.Unload;

            try
            {
                Preference = GetProjectPreference();
            }
            catch
            {
                Preference = null;
            }

            if (Preference == null)
                return ConnectResult.NoPreference;

            XmlHelper pref = new XmlHelper(Preference);
            XmlElement devSiteElement = pref.GetElement("Property[@Name='DevSite']/DevSite");

            if (devSiteElement == null)
                return ConnectResult.NoDevSite;

            DevSite = DevSiteLoginInfo.Load(devSiteElement);

            try
            {
                DevConnection = DevSite.TryConnect();
                Status = ProjectStatus.Succeed;
                return ConnectResult.Succeed;
            }
            catch
            {
                return ConnectResult.ConnectFail;
            }
        }

        /// <summary>依 Connect 的結果提示使用者；要在 UI 執行緒呼叫</summary>
        internal void HandleConnectResult(ConnectResult result, bool warring)
        {
            switch (result)
            {
                case ConnectResult.NoPreference:
                    if (warring)
                        MessageBox.Show("無法取得專案設定檔!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case ConnectResult.NoDevSite:
                    ShowResetMessage(this.Name, "無法取得專案『" + this.Name + "』開發站台連線設定, 是否重新設定開發站台位置?");
                    break;
                case ConnectResult.ConnectFail:
                    ShowResetMessage(this.Name, "專案『" + this.Name + "』開發站台連線失敗, 是否重新設定開發站台位置?");
                    break;
            }
        }

        private void ShowResetMessage(string name, string msg)
        {
            DialogResult dr = MessageBox.Show(msg, "無法連線", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            if (dr == DialogResult.No) return;

            ResetDevSiteForm dsf = new ResetDevSiteForm(name, DevSite ?? new DevSiteLoginInfo());
            dsf.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            dsf.Completed += delegate(object sender, EventArgs arg)
            {
                SetDevSite(dsf.GetDevSite(), dsf.Connection);
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
            SetDevSite(devSite, null);
        }

        /// <summary>connection 為已連好的連線時直接沿用，不再連第二次</summary>
        private void SetDevSite(DevSiteLoginInfo devSite, Connection connection)
        {
            this.DevSite = devSite;
            if (connection != null)
            {
                DevConnection = connection;
                Status = ProjectStatus.Succeed;
            }
            else
            {
                try
                {
                    DevConnection = this.DevSite.TryConnect();
                    Status = ProjectStatus.Succeed;
                }
                catch
                {
                    Status = ProjectStatus.LoadFail;
                }
            }

            SaveDevSite(devSite);
        }

        /// <summary>
        /// 把開發站台設定寫回這個專案的設定並存到 Greening。
        /// 原本是用 MainForm.CurrentProject 存檔：開專案時 CurrentProject 還是「上一個」專案，
        /// 會把這個專案的設定寫進上一個專案；沒有開過專案時則根本沒存。設定裡沒有 DevSite 時也會當掉。
        /// </summary>
        internal void SaveDevSite(DevSiteLoginInfo devSite)
        {
            this.DevSite = devSite;
            if (this.Preference == null) return;

            XmlHelper h = new XmlHelper(this.Preference);
            XmlElement prop = h.GetElement("Property[@Name='DevSite']");
            if (prop == null)
            {
                prop = h.AddElement(".", "Property");
                prop.SetAttribute("Name", "DevSite");
            }
            prop.InnerXml = string.Empty;
            prop.AppendChild(prop.OwnerDocument.ImportNode(devSite.XmlContent.GetElement("."), true));

            this.UpdateProjectPreference(this.Preference);
        }

        /// <summary>
        /// 從專案設定讀出專案內的名稱清單（不需連線開發站台）。
        /// xpath 例：Property[@Name='UDS']/Contract、Property[@Name='UDT']/UDT
        /// </summary>
        internal List<string> ListPreferenceNames(string xpath)
        {
            List<string> names = new List<string>();
            if (this.Preference == null) return names;

            XmlHelper h = new XmlHelper(this.Preference);
            foreach (XmlElement e in h.GetElements(xpath))
                names.Add(e.GetAttribute("Name"));
            names.Sort(StringComparer.OrdinalIgnoreCase);
            return names;
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

        /// <summary>
        /// 取得已設定且存在的本機目錄；沒有設定或資料夾不存在回傳空字串，不會跳出設定畫面。
        /// </summary>
        internal string GetConfiguredLocalPath()
        {
            string path = LocalPath;
            if (string.IsNullOrWhiteSpace(path))
                path = GetStoragePath();

            if (!string.IsNullOrWhiteSpace(path) && Directory.Exists(path))
                return path;
            return string.Empty;
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
            // 使用者取消、或路徑不存在時回傳空字串，呼叫端據此中止
            return Directory.Exists(LocalPath) ? LocalPath : string.Empty;
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

    internal enum ConnectResult
    {
        Succeed, NoPreference, NoDevSite, ConnectFail,

        /// <summary>使用者在連線等待畫面按了取消（只用於顯示離線結構）</summary>
        Cancelled
    }
}
