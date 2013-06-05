using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.DSAClient;
using System.Xml;

namespace ProjectManager.ActionHandler.UDT.Command
{
    class AddFieldCommand : IUDTCommand
    {
        #region IUDTCommand 成員

        public string Type
        {
            get { return "AddField"; }
        }

        public string Description
        {
            get {
                XmlHelper h = new XmlHelper(Result);
                StringBuilder sb = new StringBuilder("資料表『");
                sb.Append(h.GetText("TableName")).Append("』中新增欄位「");
                sb.Append(h.GetText("Field/@Name")).Append("」;");

                return sb.ToString();
            }
        }

        public System.Xml.XmlElement Sample
        {
            get
            {
                XmlHelper h = new XmlHelper("<Command />");
                h.AddElement(".", "TableName", "資料表名稱");
                XmlElement fieldElement = h.AddElement(".", "Field");
                fieldElement.SetAttribute("Name", "欄位名稱");
                fieldElement.SetAttribute("Indexed", "是否建立索引");
                fieldElement.SetAttribute("DataType", "資料型態");
                fieldElement.SetAttribute("AllowNull", "是否允許空白");
                XmlElement cmdElement = h.GetElement(".");
                cmdElement.SetAttribute("Type", this.Type);
                return cmdElement;
            }
        }

        public XmlElement Result { get; set; }

        #endregion
    }
}
