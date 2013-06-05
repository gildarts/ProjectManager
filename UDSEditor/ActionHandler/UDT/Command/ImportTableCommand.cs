using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.DSAClient;
using System.Xml;

namespace ProjectManager.ActionHandler.UDT.Command
{
    class ImportTableCommand : IUDTCommand
    {
        #region IUDTCommand 成員

        public string Type
        {
            get { return "ImportTable"; }
        }

        public string Description
        {
            get
            {
                XmlHelper h = new XmlHelper(Result);
                StringBuilder sb = new StringBuilder("建立完整資料表『");
                sb.Append(h.GetText("Table/@Name")).Append("』;");
                return sb.ToString();
            }
        }

        public System.Xml.XmlElement Sample
        {
            get
            {
                XmlHelper h = new XmlHelper("<Command />");

                h.AddElement(".", "Table");
                h.SetAttribute("Table", "Name", "新資料表名稱");
                XmlElement fe = h.AddElement("Table", "Field");
                fe.SetAttribute("Name", "欄位名稱");
                fe.SetAttribute("DataType", "欄位型態");
                fe.SetAttribute("Indexed", "是否建立索引");
                fe.SetAttribute("AllowNull", "是否允許空白");
                fe.SetAttribute("Default", "預設值");

                h.AddElement("Table", "Unique");
                h.AddElement("Table/Unique", "FieldName", "欄位名稱");

                XmlElement fkElement = h.AddElement("Table", "ForeignKey");
                fkElement.SetAttribute("DeleteAction", "NO ACTION");
                fkElement.SetAttribute("UpdateAction", "NO ACTION");

                h.AddElement("Table/ForeignKey", "MainTable");
                h.AddElement("Table/ForeignKey", "RefTable");
                h.AddElement("Table/ForeignKey/MainTable", "FieldName", "欄位名稱");
                h.AddElement("Table/ForeignKey/RefTable", "FieldName", "欄位名稱");

                h.SetAttribute("Table/ForeignKey/MainTable", "Name", "主資料表名稱");
                h.SetAttribute("Table/ForeignKey/RefTable", "Name", "參照資料表名稱");

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
