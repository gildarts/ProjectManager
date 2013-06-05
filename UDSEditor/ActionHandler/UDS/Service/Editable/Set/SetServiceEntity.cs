using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectManager.Project.UDS.Service;
using FISCA.DSAClient;
using System.Xml;
using ProjectManager.Project.UDT;
using ProjectManager.Util;

namespace ProjectManager.ActionHandler.UDS.Service.Editable.Set
{
    internal class SetServiceEntity : ServiceEntity
    {
        private string _tableName;
        private string _serviceName;

        internal SetServiceEntity(string serviceName, string tableName)
            : base(serviceName, tableName)
        {
            _serviceName = serviceName;
            _tableName = tableName;

            if (!MainForm.CurrentUDT.ExistsInAllTables(tableName))
                throw new Exception("資料表不存在 : " + tableName);
        }

        internal override System.Xml.XmlElement GetServiceXml()
        {
            List<string> fields = MainForm.CurrentUDT.ListFields(_tableName);

            StringBuilder dmsb = new StringBuilder("select ");
            //string fs = string.Join(",", fields.ToArray());
            //dmsb.Append(fs);

            StringBuilder fieldsb = new StringBuilder();
            for (int i = 0; i < fields.Count; i++)
            {
                string field = fields[i];
                if (!field.EndsWith("id", StringComparison.CurrentCultureIgnoreCase)) continue;

                if (fieldsb.Length > 0)
                    fieldsb.Append(",");

                fieldsb.Append(field);
            }

            dmsb.Append(fieldsb).Append(" from ").Append(_tableName);

            XmlHelper h = new XmlHelper("<Service/>");
            h.SetAttribute(".", "Enabled", "true");
            h.SetAttribute(".", "Name", _serviceName);

            h.AddElement(".", "Definition");
            h.SetAttribute("Definition", "Type", "dbhelper");

            h.AddElement("Definition", "Action", ServiceAction.Set.ToString());


            string displayName = _tableName.StartsWith("$") ? _tableName.Substring(1) : _tableName;
            displayName = StringUtil.ConvertToDisplayName(displayName);

            h.AddElement("Definition", "RequestRecordElement", displayName);
            h.AddElement("Definition", "TargetTableName", _tableName);

            h.AddElement("Definition", "Mappings");
            XmlElement dmElement = h.AddElement("Definition/Mappings", "DefaultMapping");
            XmlNode dmsbNode = dmElement.OwnerDocument.CreateCDataSection(dmsb.ToString());
            dmElement.AppendChild(dmsbNode);

            XmlElement flElement = h.AddElement("Definition", "FieldList");
            flElement.SetAttribute("Source", "");

            foreach (string field in fields)
            {
                XmlElement fe = h.AddElement("Definition/FieldList", "Field");
                fe.SetAttribute("Source", StringUtil.ConvertToDisplayName(field));
                fe.SetAttribute("Target", field);

                if (field.Equals("Uid", StringComparison.CurrentCultureIgnoreCase))
                    fe.SetAttribute("AutoNumber", "true");

                if (field.EndsWith("id", StringComparison.CurrentCultureIgnoreCase))
                {
                    fe.SetAttribute("Identity", "true");
                }
            }
            return h.GetElement(".");
        }
    }
}
