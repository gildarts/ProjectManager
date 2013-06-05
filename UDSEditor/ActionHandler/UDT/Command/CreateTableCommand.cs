using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.DSAClient;
using System.Xml;

namespace ProjectManager.ActionHandler.UDT.Command
{
    class CreateTableCommand : IUDTCommand
    {
        #region IUDTCommand 成員

        public string Type
        {
            get { return "CreateTable"; }
        }

        public string Description
        {
            get
            {
                XmlHelper h = new XmlHelper(Result);
                StringBuilder sb = new StringBuilder("建立新資料表『");
                sb.Append(h.GetText("TableName")).Append("』;");
                return sb.ToString();
            }
        }

        public System.Xml.XmlElement Sample
        {
            get
            {
                XmlHelper h = new XmlHelper("<Command />");

                h.AddElement(".", "TableName", "資料表名稱");

                XmlElement cmdElement = h.GetElement(".");
                cmdElement.SetAttribute("Type", this.Type);
                return cmdElement;
            }
        }

        public System.Xml.XmlElement Result { get; set; }

        #endregion
    }
}
