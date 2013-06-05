using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.DSAClient;
using System.Xml;

namespace ProjectManager.ActionHandler.UDT.Command
{
    class RemoveForeignKeyCommand : IUDTCommand
    {
        #region IUDTCommand 成員

        public string Type
        {
            get { return "RemoveForeignKey"; }
        }

        public string Description
        {
            get
            {
                XmlHelper h = new XmlHelper(Result);
                StringBuilder sb = new StringBuilder("刪除資料表『");
                sb.Append(h.GetText("TableName")).Append("』之 ForeignKey「").Append(h.GetText("ForeignKeyName")).Append("」;");
                return sb.ToString();
            }
        }

        public System.Xml.XmlElement Sample
        {
            get
            {
                XmlHelper h = new XmlHelper("<Command />");

                h.AddElement(".", "TableName", "資料表名稱");
                h.AddElement(".", "ForeignKeyName", "欲刪除鍵值名稱");

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
