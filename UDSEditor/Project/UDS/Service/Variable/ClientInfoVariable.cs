using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using FISCA.DSAClient;

namespace ProjectManager.Project.UDS.Service.Variable
{
    class ClientInfoVariable : IVariable
    {
        internal ClientInfoVariable()
        {
            this.Editor = new ClientInfoVariableEditor();
        }

        internal ClientInfoVariable(XmlElement varElement)
        {
            ClientInfoVariableEditor e = new ClientInfoVariableEditor();
            e.VariableName = varElement.GetAttribute("Name");
            e.VariableKey = varElement.GetAttribute("Key");
            this.Editor = e;
        }

        #region IVariable 成員

        public string Name
        {
            get
            {
                ClientInfoVariableEditor editor = this.Editor as ClientInfoVariableEditor;
                return editor.VariableName;
            }
        }

        public string Memo
        {
            get
            {
                ClientInfoVariableEditor editor = this.Editor as ClientInfoVariableEditor;
                return editor.VariableKey;
            }
        }

        public string Source { get { return "ClientInfo"; } }

        public System.Windows.Forms.Control Editor { get; private set; }

        public System.Xml.XmlElement GetXml()
        {
            ClientInfoVariableEditor editor = this.Editor as ClientInfoVariableEditor;
            XmlHelper h = new XmlHelper("<Variable/>");
            h.SetAttribute(".", "Name", editor.VariableName);
            h.SetAttribute(".", "Source", this.Source);
            h.SetAttribute(".", "Key", editor.VariableKey);
            return h.GetElement(".");
        }

        public bool Valid
        {
            get
            {
                ClientInfoVariableEditor editor = this.Editor as ClientInfoVariableEditor;
                StringBuilder sb = new StringBuilder();
                if (string.IsNullOrWhiteSpace(editor.VariableName))
                    sb.Append("變數名稱不可空白");

                if (string.IsNullOrWhiteSpace(editor.VariableKey))
                {
                    if (sb.Length > 0)
                        sb.Append('\n');
                    sb.Append("用戶端項目不可空白");
                }
                InvalidMessage = sb.ToString();

                return string.IsNullOrWhiteSpace(InvalidMessage);
            }
        }

        public string InvalidMessage { get; private set; }

        #endregion
    }
}
