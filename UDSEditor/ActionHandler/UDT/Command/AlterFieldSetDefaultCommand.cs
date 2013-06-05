using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using FISCA.DSAClient;

namespace ProjectManager.ActionHandler.UDT.Command
{
    class AlterFieldSetDefaultCommand : IUDTCommand
    {
        #region IUDTCommand 成員

        public string Type
        {
            get { return "AlterFieldSetDefault"; }
        }

        public string Description
        {
            get
            {
                XmlHelper h = new XmlHelper(Result);
                StringBuilder sb = new StringBuilder("資料表『");
                sb.Append(h.GetText("TableName")).Append("』欄位「").Append(h.GetText("FieldName")).Append("」預設值變更為「");
                sb.Append(h.GetText("Default")).Append("」;");
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
                h.AddElement(".", "Default", "預設值");

                XmlElement cmdElement = h.GetElement(".");
                cmdElement.SetAttribute("Type", this.Type);
                return cmdElement;
            }
        }

        public System.Xml.XmlElement Result { get; set; }

        #endregion
    }
}
