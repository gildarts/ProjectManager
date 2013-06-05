using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.DSAClient;
using System.Xml;

namespace ProjectManager.ActionHandler.UDT.Command
{
    class RenameTableCommand : IUDTCommand
    {
        #region IUDTCommand 成員

        public string Type
        {
            get { return "RenameTable"; }
        }

        public string Description
        {
            get
            {
                XmlHelper h = new XmlHelper(Result);
                StringBuilder sb = new StringBuilder("將資料表『");
                sb.Append(h.GetText("TableName")).Append("』重新命名為「").Append(h.GetText("NewName")).Append("」;");
                return sb.ToString();
            }
        }

        public System.Xml.XmlElement Sample
        {
            get
            {
                XmlHelper h = new XmlHelper("<Command />");

                h.AddElement(".", "TableName", "原資料表名稱");
                h.AddElement(".", "NewName", "新資料表名稱");
                
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
