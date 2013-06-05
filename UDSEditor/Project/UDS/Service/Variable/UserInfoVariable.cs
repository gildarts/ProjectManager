using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.DSAClient;
using System.Xml;

namespace ProjectManager.Project.UDS.Service.Variable
{
    class UserInfoVariable : IVariable
    {
        internal UserInfoVariable()
        {
            this.Editor = new UserInfoVariableEditor();
        }

        internal UserInfoVariable(XmlElement varElement)
        {
            UserInfoVariableEditor e =  new UserInfoVariableEditor();
            e.VariableName = varElement.GetAttribute("Name");
            e.VariableKey = varElement.GetAttribute("Key");
            this.Editor = e;            
        }

        #region IVariable 成員

        public string Name
        {
            get
            {
                UserInfoVariableEditor editor = this.Editor as UserInfoVariableEditor;
                return editor.VariableName;
            }
        }

        public string Memo
        {
            get
            {
                UserInfoVariableEditor editor = this.Editor as UserInfoVariableEditor;
                return editor.VariableKey;
            }
        }

        public string Source { get { return "UserInfo"; } }

        public System.Windows.Forms.Control Editor { get; private set; }

        public System.Xml.XmlElement GetXml()
        {
            UserInfoVariableEditor editor = this.Editor as UserInfoVariableEditor;
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
                UserInfoVariableEditor editor = this.Editor as UserInfoVariableEditor;
                StringBuilder sb = new StringBuilder();
                if (string.IsNullOrWhiteSpace(editor.VariableName))
                    sb.Append("變數名稱不可空白");
                if (string.IsNullOrWhiteSpace(editor.VariableKey))
                {
                    if (sb.Length > 0)
                        sb.Append('\n');
                    sb.Append("使用者屬性不可空白");
                }
                InvalidMessage = sb.ToString();

                return string.IsNullOrWhiteSpace(InvalidMessage);
            }
        }

        public string InvalidMessage { get; private set; }
        #endregion
    }
}
