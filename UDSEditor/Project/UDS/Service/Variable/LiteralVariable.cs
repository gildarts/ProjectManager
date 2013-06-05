using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using FISCA.DSAClient;

namespace ProjectManager.Project.UDS.Service.Variable
{
    class LiteralVariable : IVariable
    {
        internal LiteralVariable()
        {
            this.Editor = new LiteralVariableEditor();
        }

        internal LiteralVariable(XmlElement varElement)
        {
            LiteralVariableEditor e = new LiteralVariableEditor();
            e.VariableName = varElement.GetAttribute("Name");
            e.VariableKey = varElement.InnerText;
            this.Editor = e;
        }

        #region IVariable 成員

        public string Name
        {
            get
            {
                LiteralVariableEditor editor = this.Editor as LiteralVariableEditor;
                return editor.VariableName;
            }
        }

        public string Memo
        {
            get
            {
                LiteralVariableEditor editor = this.Editor as LiteralVariableEditor;
                return editor.VariableKey;
            }
        }

        public string Source { get { return "Literal"; } }

        public System.Windows.Forms.Control Editor { get; private set; }

        public System.Xml.XmlElement GetXml()
        {
            LiteralVariableEditor editor = this.Editor as LiteralVariableEditor;
            XmlHelper h = new XmlHelper("<Variable/>");
            h.SetAttribute(".", "Name", editor.VariableName);
            h.SetAttribute(".", "Source", this.Source);
            
            XmlElement e = h.GetElement(".");
            XmlCDataSection section = e.OwnerDocument.CreateCDataSection(editor.VariableKey);
            e.AppendChild(section);
            return e;
        }

        public bool Valid
        {
            get
            {
                LiteralVariableEditor editor = this.Editor as LiteralVariableEditor;
                StringBuilder sb = new StringBuilder();
                if (string.IsNullOrWhiteSpace(editor.VariableName))
                    sb.Append("變數名稱不可空白");

                if (string.IsNullOrWhiteSpace(editor.VariableKey))
                {
                    if (sb.Length > 0)
                        sb.Append('\n');
                    sb.Append("常數值不可空白");
                }
                InvalidMessage = sb.ToString();

                return string.IsNullOrWhiteSpace(InvalidMessage);
            }
        }

        public string InvalidMessage { get; private set; }

        #endregion
    }
}
