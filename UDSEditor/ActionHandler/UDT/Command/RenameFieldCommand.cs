using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.DSAClient;
using System.Xml;

namespace ProjectManager.ActionHandler.UDT.Command
{
    class RenameFieldCommand : IUDTCommand
    {
        #region IUDTCommand 成員

        public string Type
        {
            get { return "RenameField"; }
        }

        public string Description
        {
            get
            {
                XmlHelper h = new XmlHelper(Result);
                StringBuilder sb = new StringBuilder("將資料表『");
                sb.Append(h.GetText("TableName")).Append("』中的欄位「").Append(h.GetText("FieldName")).Append("」重新命名為「");
                sb.Append(h.GetText("NewFieldName")).Append("」;");
                return sb.ToString();
            }
        }

        public System.Xml.XmlElement Sample
        {
            get
            {
                XmlHelper h = new XmlHelper("<Command />");

                h.AddElement(".", "TableName", "資料表名稱");
                h.AddElement(".", "FieldName", "原欄位名稱");
                h.AddElement(".", "NewFieldName", "新欄位名稱");

                XmlElement cmdElement = h.GetElement(".");
                cmdElement.SetAttribute("Type", this.Type);
                return cmdElement;
            }
        }

        public System.Xml.XmlElement Result
        {
            get;
            set;
        }

        #endregion
    }
}
