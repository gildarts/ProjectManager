using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using FISCA.DSAClient;

namespace ProjectManager.Project.UDS.Service.Variable
{
    class UUIDVariable : IVariable
    {
        #region IVariable 成員

       internal UUIDVariable()
        {
            this.Editor = new UUIDVariableEditor();
        }

       internal UUIDVariable(XmlElement varElement)
        {
            UUIDVariableEditor e = new UUIDVariableEditor();
            e.VariableName = varElement.GetAttribute("Name");
        
            this.Editor = e;            
        }
        
        public string Name
        {
            get
            {
                UUIDVariableEditor editor = this.Editor as UUIDVariableEditor;
                return editor.VariableName;
            }
        }

        public string Memo
        {
            get
            {
                UUIDVariableEditor editor = this.Editor as UUIDVariableEditor;
                return editor.VariableKey;
            }
        }

        public string Source { get { return "UUID"; } }

        public System.Windows.Forms.Control Editor { get; private set; }

        public System.Xml.XmlElement GetXml()
        {
            UUIDVariableEditor editor = this.Editor as UUIDVariableEditor;
            XmlHelper h = new XmlHelper("<Variable/>");
            h.SetAttribute(".", "Name", editor.VariableName);
            h.SetAttribute(".", "Source", this.Source);
            //h.SetAttribute(".", "Key", editor.VariableKey);
            return h.GetElement(".");
        }

        public bool Valid
        {
            get
            {
                UUIDVariableEditor editor = this.Editor as UUIDVariableEditor;
                StringBuilder sb = new StringBuilder();
                if (string.IsNullOrWhiteSpace(editor.VariableName))
                    sb.Append("變數名稱不可空白");
               
                InvalidMessage = sb.ToString();

                return string.IsNullOrWhiteSpace(InvalidMessage);
            }
        }

        public string InvalidMessage { get; private set; }

        #endregion
    }
}
