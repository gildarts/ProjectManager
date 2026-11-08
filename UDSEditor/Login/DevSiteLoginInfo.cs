using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.DSAClient;
using System.Xml;
using ProjectManager.Login.Security;

namespace ProjectManager.Login
{
    class DevSiteLoginInfo
    {
        internal const string DEFAULT_CONTRACT_NAME = "admin";

        private string _contractName;

        internal string AccessPoint { get; set; }
        internal string User { get; set; }
        internal string Password { get; set; }
        internal string ContractName
        {
            get
            {
                //if (string.IsNullOrWhiteSpace(_contractName))
                //    return DEFAULT_CONTRACT_NAME;

                return _contractName;
            }
            set { _contractName = value; }
        }

        /// <summary>
        /// 先用目前登入的 ischool 帳號（Passport）連線，失敗且有設定帳密時改用 Basic。
        /// 帳密空白則只走 Passport。
        /// </summary>
        internal Connection TryConnect()
        {
            Exception passportError;
            try
            {
                return ConnectByPassport();
            }
            catch (Exception ex)
            {
                passportError = ex;
            }

            if (string.IsNullOrWhiteSpace(User))
                throw new Exception("Passport 連線失敗：" + passportError.Message, passportError);

            try
            {
                return ConnectByBasic();
            }
            catch (Exception ex)
            {
                throw new Exception("Passport 連線失敗：" + passportError.Message
                    + "\n帳密連線失敗：" + ex.Message, ex);
            }
        }

        private Connection ConnectByPassport()
        {
            if (MainForm.LoginArgs == null || MainForm.LoginArgs.GreeningConnection == null)
                throw new Exception("尚未登入 ischool 帳號。");

            // 每次都重新取得，避免 passport 過期
            Envelope rsp = MainForm.LoginArgs.GreeningConnection.SendRequest("DS.Base.GetPassportToken", new Envelope());
            PassportSecurityToken token = new PassportSecurityToken(rsp.Body.XmlString);

            Connection connection = NewConnection();
            connection.Connect(AccessPoint, ContractName, token);
            return connection;
        }

        private Connection ConnectByBasic()
        {
            Connection connection = NewConnection();
            connection.Connect(AccessPoint, ContractName, User, Password);
            return connection;
        }

        private static Connection NewConnection()
        {
            Connection connection = new Connection();
            connection.EnableSession = false;
            connection.EnableSecureTunnel = true;
            return connection;
        }

        internal bool IsInitialized { get { return (AccessPoint != null); } }

        internal XmlHelper XmlContent
        {
            get
            {
                XmlHelper h = new XmlHelper("<DevSite/>");
                h.AddElement(".", "AccessPoint", this.AccessPoint);
                h.AddElement(".", "ContractName", this.ContractName);
                h.AddElement(".", "User", this.User);
                h.AddElement(".", "Password", this.Password);
                return h;
            }
        }

        internal string XmlString { get { return XmlContent.XmlString; } }

        internal static DevSiteLoginInfo Load(XmlElement devSiteElement)
        {
            XmlHelper h = new XmlHelper(devSiteElement);
            DevSiteLoginInfo info = new DevSiteLoginInfo();
            info.AccessPoint = h.GetText("AccessPoint");
            info.ContractName = h.GetText("ContractName");
            info.User = h.GetText("User");
            info.Password = h.GetText("Password");

            if (string.IsNullOrWhiteSpace(h.GetText("ContractName")))
                info.ContractName = DEFAULT_CONTRACT_NAME;
            else
                info.ContractName = h.GetText("ContractName");

            return info;
        }
    }
}
