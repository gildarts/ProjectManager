using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.DSAClient;
using System.Xml;

namespace ProjectManager.ActionHandler.UDT.Command
{
    class SetUniqueCommand : IUDTCommand
    {
        #region IUDTCommand 成員

        public string Type
        {
            get { return "SetUnique"; }
        }

        public string Description
        {
            get
            {
                XmlHelper h = new XmlHelper(Result);
                StringBuilder sb = new StringBuilder("將資料表『");
                sb.Append(h.GetText("TableName")).Append("』中「");
                sb.Append(h.GetText("Unique/FieldName")).Append("」等欄位設為不重覆;");

                return sb.ToString();
            }
        }

        public System.Xml.XmlElement Sample
        {
            get
            {
                XmlHelper h = new XmlHelper("<Command/>");
                h.AddElement(".", "TableName", "資料表名稱");
                XmlElement ue = h.AddElement(".", "Unique");
                XmlComment xc = ue.OwnerDocument.CreateComment("可一到多個欄位");
                ue.AppendChild(xc);
                h.AddElement("Unique", "FieldName", "唯一欄位名稱");
                h.SetAttribute(".", "Type", this.Type);
                return h.GetElement(".");
                //XmlHelper h = new XmlHelper("<Command />");
                //h.AddElement(".", "Table");
                //h.SetAttribute("Table", "Name", "資料表名稱");
                //XmlElement uniqElement = h.AddElement("Table", "Unique");
                //uniqElement.SetAttribute("Name", "唯一鍵名稱");

                //h.AddElement("Table/Unique", "FieldName", "唯一欄名位稱");

                //XmlElement cmdElement = h.GetElement(".");
                //cmdElement.SetAttribute("Type", this.Type);
                //return cmdElement;
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
