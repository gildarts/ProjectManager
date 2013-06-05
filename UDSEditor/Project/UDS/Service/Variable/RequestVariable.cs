using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using FISCA.DSAClient;

namespace ProjectManager.Project.UDS.Service.Variable
{
    class RequestVariable : IVariable
    {
        internal RequestVariable()
        {
            this.Editor = new RequestVariableEditor();
        }

        internal RequestVariable(XmlElement varElement)
        {
            RequestVariableEditor e = new RequestVariableEditor();
            e.VariableName = varElement.GetAttribute("Name");
            e.VariableKey = varElement.GetAttribute("Key");
            this.Editor = e;            
        }

        #region IVariable 成員

        public string Name
        {
            get
            {
                RequestVariableEditor editor = this.Editor as RequestVariableEditor;
                return editor.VariableName;
            }
        }

        public string Memo
        {
            get
            {
                RequestVariableEditor editor = this.Editor as RequestVariableEditor;
                return editor.VariableKey;
            }
        }

        public string Source { get { return "Request"; } }

        public System.Windows.Forms.Control Editor { get; private set; }

        public System.Xml.XmlElement GetXml()
        {
            RequestVariableEditor editor = this.Editor as RequestVariableEditor;
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
                RequestVariableEditor editor = this.Editor as RequestVariableEditor;
                StringBuilder sb = new StringBuilder();
                if (string.IsNullOrWhiteSpace(editor.VariableName))
                    sb.Append("變數名稱不可空白");
                if (string.IsNullOrWhiteSpace(editor.VariableKey))
                {
                    if (sb.Length > 0)
                        sb.Append('\n');
                    sb.Append("XPath 不可空白");
                }
                InvalidMessage = sb.ToString();

                return string.IsNullOrWhiteSpace(InvalidMessage);
            }
        }

        public string InvalidMessage { get; private set; }
        #endregion
    }
}
