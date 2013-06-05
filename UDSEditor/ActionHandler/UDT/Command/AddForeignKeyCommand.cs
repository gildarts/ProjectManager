using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.DSAClient;
using System.Xml;

namespace ProjectManager.ActionHandler.UDT.Command
{
    class AddForeignKeyCommand : IUDTCommand
    {
        #region IUDTCommand 成員

        public string Type
        {
            get { return "AddForeignKey"; }
        }

        public string Description
        {
            get
            {
                XmlHelper h = new XmlHelper(Result);
                StringBuilder sb = new StringBuilder("資料表『");
                sb.Append(h.GetText("ForeignKey/MainTable/@Name")).Append("』中欄位「");

                foreach (XmlElement e in h.GetElements("ForeignKey/MainTable/FieldName"))
                    sb.Append(e.InnerText).Append("、");

                sb.Remove(sb.Length - 1, 1);
                sb.Append("」與資料表『").Append(h.GetText("ForeignKey/RefTable/@Name")).Append("』建立 ForeignKey 關係");
                
                return sb.ToString();
            }
        }

        public System.Xml.XmlElement Sample
        {
            get
            {
                XmlHelper h = new XmlHelper("<Command />");

                XmlElement fkElement = h.AddElement(".", "ForeignKey");
                fkElement.SetAttribute("DeleteAction", "NO ACTION");
                fkElement.SetAttribute("UpdateAction", "NO ACTION");

                h.AddElement("ForeignKey", "MainTable");
                h.AddElement("ForeignKey", "RefTable");
                h.AddElement("ForeignKey/MainTable", "FieldName", "欄位名稱");
                h.AddElement("ForeignKey/RefTable", "FieldName", "欄位名稱");

                h.SetAttribute("ForeignKey/MainTable", "Name", "主資料表名稱");
                h.SetAttribute("ForeignKey/RefTable", "Name", "參照資料表名稱");

                XmlElement cmdElement = h.GetElement(".");
                cmdElement.SetAttribute("Type", this.Type);
                return cmdElement;
            }
        }

        public System.Xml.XmlElement Result { get; set; }

        #endregion
    }
}
