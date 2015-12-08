using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.DSAClient;
using System.Xml;

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

        internal Connection TryConnect()
        {
            Connection connection = new Connection();
            connection.EnableSession = false;
            connection.EnableSecureTunnel = true;
            connection.Connect(AccessPoint, ContractName, User, Password);
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
