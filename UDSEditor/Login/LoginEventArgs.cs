using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.DSAClient;
using System.Xml;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using ProjectManager.Login.Security;

namespace ProjectManager.Login
{
    internal class LoginEventArgs : EventArgs
    {
        private const string WIDGET_KEY = "ischool.cms";
        
        public Connection GreeningConnection { get; set; }
        internal Connection ModuleConnection { private get; set; }
        public string FtpUser { get; set; }
        public string FtpPassword { get; set; }
        public string FtpURL { get; set; }
        public string LoginUser { get; set; }

        public XmlElement StaticPreference
        {
            get { return this.GetPreference("1"); }
            set { this.SetPreference("1", value); }
        }

        //public XmlElement Preference
        //{
        //    get { return this.GetPreference(LoginEventArgs.UniqName); }
        //    set { this.SetPreference(LoginEventArgs.UniqName, value); }
        //}
          
        private XmlElement GetPreference(string instanceKey)
        {
            XmlHelper h = new XmlHelper();
            h.AddElement(".", "WidgetKey", WIDGET_KEY);
            h.AddElement(".", "InstanceKey", instanceKey);
            Envelope env = new Envelope(h);
            env = this.GreeningConnection.SendRequest("GetMyWidgetPreference", env);

            h = new XmlHelper(env.Body);
            string content = h.GetText("WidgetPreference/Content");
            if (string.IsNullOrWhiteSpace(content))
                return null;
            return XmlHelper.ParseAsDOM(content);
        }

        private void SetPreference(string instanceKey, XmlElement value)
        {
            XmlHelper h = new XmlHelper();
            h.AddElement(".", "WidgetPreference");
            h.AddElement("WidgetPreference", "WidgetKey", WIDGET_KEY);
            h.AddElement("WidgetPreference", "InstanceKey", instanceKey);
            XmlElement content = h.AddElement("WidgetPreference", "Content");
            XmlCDataSection section = content.OwnerDocument.CreateCDataSection(value.OuterXml);
            content.AppendChild(section);

            Envelope env = new Envelope(h);

            if (this.GetPreference(instanceKey) == null)
                this.GreeningConnection.SendRequest("AddWidgetPreference", env);
            else
                this.GreeningConnection.SendRequest("UpdateWidgetPreference", env);
        }

        public Envelope SendModuleRequest(string srvName, Envelope request)
        {
            try
            {
                return ModuleConnection.SendRequest(srvName, request);
            }
            catch (DSAServerException ex)
            {
                if (ex.Status == "511")
                {
                    ConnectModuleServer();
                    return SendModuleRequest(srvName, request);
                }
                else
                {
                    throw ex;
                }
            }
        }
        
        public string GreeningID { get; set; }

        public bool SucceedModuleLogin { get; set; }

        private string _moduleURL;
        internal void ConnectModuleServer()
        {           
            this.ModuleConnection = new Connection();
            ModuleConnection.EnableSession = true;
            ModuleConnection.EnableSecureTunnel = true;

            Envelope rsp = this.GreeningConnection.SendRequest("DS.Base.GetPassportToken", new Envelope());
            PassportSecurityToken stt = new PassportSecurityToken(rsp.Body.XmlString);

            ModuleConnection.Connect(_moduleURL, "developer", stt);
        }

        internal void SetModuleConnectionInfo(string moduleURL)
        {
            _moduleURL = moduleURL;
        }
    }
}
