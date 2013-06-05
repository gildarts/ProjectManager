using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using FISCA.DSAClient;

namespace ProjectManager.Project.UDS.Service.Variable
{
    class SQLVariable: IVariable
    {
         internal SQLVariable()
        {
            this.Editor = new SQLVariableEditor();
        }

         internal SQLVariable(XmlElement varElement)
        {
            SQLVariableEditor e = new SQLVariableEditor();
            e.VariableName = varElement.GetAttribute("Name");
            e.VariableKey = varElement.GetAttribute("Key");
            this.Editor = e;            
        }

        #region IVariable 成員

         public string Name
         {
             get
             {
                 SQLVariableEditor editor = this.Editor as SQLVariableEditor;
                 return editor.VariableName;
             }
         }

         public string Memo
         {
             get
             {
                 SQLVariableEditor editor = this.Editor as SQLVariableEditor;
                 return editor.VariableKey;
             }
         }

        public string Source { get { return "Database"; } }

        public System.Windows.Forms.Control Editor { get; private set; }

        public System.Xml.XmlElement GetXml()
        {
            SQLVariableEditor editor = this.Editor as SQLVariableEditor;
            XmlHelper h = new XmlHelper("<Variable/>");
            h.SetAttribute(".", "Name", editor.VariableName);
            h.SetAttribute(".", "Source", this.Source);

            XmlElement q = h.AddElement(".", "SqlQuery");
            XmlCDataSection section =  q.OwnerDocument.CreateCDataSection(editor.VariableKey);
            q.AppendChild(section);
            
            return h.GetElement(".");
        }

        public bool Valid
        {
            get
            {
                SQLVariableEditor editor = this.Editor as SQLVariableEditor;
                StringBuilder sb = new StringBuilder();
                if (string.IsNullOrWhiteSpace(editor.VariableName))
                    sb.Append("變數名稱不可空白");
                if (string.IsNullOrWhiteSpace(editor.VariableKey))
                {
                    if (sb.Length > 0)
                        sb.Append('\n');
                    sb.Append("SQL 不可空白");
                }
                InvalidMessage = sb.ToString();

                return string.IsNullOrWhiteSpace(InvalidMessage);
            }
        }

        public string InvalidMessage { get; private set; }
        #endregion
    }
}
