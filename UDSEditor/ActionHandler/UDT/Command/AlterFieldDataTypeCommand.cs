using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.DSAClient;
using System.Xml;

namespace ProjectManager.ActionHandler.UDT.Command
{
    class AlterFieldDataTypeCommand : IUDTCommand
    {
        #region IUDTCommand 成員

        public string Type
        {
            get { return "AlterFieldDataType"; }
        }

        public string Description
        {
            get
            {
                XmlHelper h = new XmlHelper(Result);
                StringBuilder sb = new StringBuilder("資料表『");
                sb.Append(h.GetText("TableName")).Append("』欄位「").Append(h.GetText("FieldName")).Append("」型態變更為「");
                sb.Append(h.GetText("DataType")).Append("」;");
                return sb.ToString();
            }
        }

        public System.Xml.XmlElement Sample
        {
            get
            {
                XmlHelper h = new XmlHelper("<Command />");

                h.AddElement(".", "TableName", "資料表名稱");
                h.AddElement(".", "FieldName", "欄位名稱");
                h.AddElement(".", "DataType", "新資料型態");

                XmlElement cmdElement = h.GetElement(".");
                cmdElement.SetAttribute("Type", this.Type);
                return cmdElement;
            }
        }

        public System.Xml.XmlElement Result { get; set; }

        #endregion
    }
}
