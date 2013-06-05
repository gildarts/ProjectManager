using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using FISCA.DSAClient;
using ProjectManager.ActionHandler;

namespace ProjectManager.Project.UDT
{
    class UDTHandler
    {
        internal event EventHandler ProjectTableChanged;

        internal ProjectHandler Parent { get; private set; }
        internal List<UDTTable> Tables { get; private set; }

        internal List<UDTTable> AllUDTTables { get; private set; }
        internal List<string> PhysicalTables { get; private set; }

        private List<string> _allUDTs { get; set; }

        internal UDTHandler(ProjectHandler project, XmlElement source)
        {
            Parent = project;
            Tables = new List<UDTTable>();
            AllUDTTables = new List<UDTTable>();

            _allUDTs = new List<string>();

            XmlHelper h = new XmlHelper(source);

            XmlHelper ph = new XmlHelper(project.Preference);
            List<string> list = new List<string>();
            foreach (XmlElement e in ph.GetElements("Property/UDT"))
            {
                string name = e.GetAttribute("Name");
                list.Add(name.ToLower());
                _allUDTs.Add(name);
            }

            foreach (XmlElement tableElement in h.GetElements("TableName"))
            {
                UDTTable table = new UDTTable(tableElement.InnerText);
                if (list.Contains(table.Name.ToLower()))
                    Tables.Add(table);
                _allUDTs.Add(table.Name.ToLower());
                AllUDTTables.Add(table);
            }
        }

        internal bool Exists(string tableName)
        {
            foreach (string name in _allUDTs)
            {
                if (string.Equals(tableName, name, StringComparison.CurrentCultureIgnoreCase))
                    return true;
            }
            return false;
        }

        internal UDTTable CreateTable(string tableName)
        {
            tableName = tableName.ToLower();
            XmlHelper req = new XmlHelper("<Request/>");
            req.AddElement(".", "TableName", tableName);
            Parent.SendRequest("UDTService.DDL.CreateTable", new Envelope(req));

            _allUDTs.Add(tableName);
            UDTTable table = UDTTable.CreateNew(tableName);
            AllUDTTables.Add(table);

            JoinProject(table);

            return table;
        }

        internal void JoinProject(UDTTable table)
        {
            List<UDTTable> tables = new List<UDTTable>();
            tables.Add(table);
            JoinProjects(tables);
        }

        internal void JoinProjects(List<UDTTable> tables)
        {
            ModifyProject(tables, null);
        }

        internal void LeaveProject(UDTTable table)
        {
            List<UDTTable> tables = new List<UDTTable>();
            tables.Add(table);
            LeaveProjects(tables);
        }

        internal void LeaveProjects(List<UDTTable> tables)
        {
            ModifyProject(null, tables);
        }

        internal void ModifyProject(List<UDTTable> joinProjects, List<UDTTable> leaveProjects)
        {
            XmlHelper pref = new XmlHelper(Parent.Preference);
            if (joinProjects != null)
            {
                foreach (UDTTable table in joinProjects)
                {
                    XmlElement udt = pref.AddElement("Property[@Name='UDT']", "UDT");
                    udt.SetAttribute("Name", table.Name.ToLower());
                    Tables.Add(table);
                }
            }

            if (leaveProjects != null)
            {
                foreach (UDTTable table in leaveProjects)
                {
                    XmlElement udtElement = pref.GetElement("Property[@Name='UDT']");
                    XmlElement tableElement = pref.GetElement("Property[@Name='UDT']/UDT[@Name='" + table.Name + "']");

                    if (tableElement != null)
                    {
                        udtElement.RemoveChild(tableElement);
                    }
                    Tables.Remove(table);
                }
            }

            Parent.UpdateProjectPreference(pref.GetElement("."));

            if (ProjectTableChanged != null)
                ProjectTableChanged.Invoke(this, EventArgs.Empty);
        }

        internal void UpdateSchema(XmlElement newSchema)
        {
            string tableName = newSchema.GetAttribute("Name").Trim().ToLower();
            XmlHelper req = new XmlHelper(newSchema);

            Parent.SendRequest("UDTService.DDL.SetTable", new Envelope(req));
        }

        internal void DropTable(string tableName)
        {

            tableName = tableName.ToLower();

            UDTTable targetTable = null;
            foreach (UDTTable table in Tables)
            {
                if (string.Equals(table.Name, tableName, StringComparison.CurrentCultureIgnoreCase))
                {
                    targetTable = table;
                    break;
                }
            }
            //原本這段是要限制僅能刪除專案內的資料表, 但之後不限制了, 故註解
            //if (targetTable == null) return;

            XmlHelper req = new XmlHelper("<Request/>");
            req.AddElement(".", "TableName", tableName);
            Parent.SendRequest("UDTService.DDL.DropTable", new Envelope(req));

            if (targetTable != null)
                LeaveProject(targetTable);
        }

        internal List<string> ListAllTables()
        {
            if (PhysicalTables == null)
                PhysicalTables = MainForm.CurrentProject.ListPhysicalTables();

            List<string> list = new List<string>();

            foreach (UDTTable table in Tables)
                list.Add("$" + table.Name);

            list.Sort();

            _allUDTs.Sort();
            foreach (string tableName in _allUDTs)
            {
                string name = "$" + tableName;
                if (list.Contains(name)) continue;
                list.Add(name);
            }

            list.AddRange(PhysicalTables);
            return list;
        }

        internal bool ExistsInAllTables(string tableName)
        {
            if (tableName.StartsWith("$"))
            {
                string trimed = tableName.Substring(1);
                return Exists(trimed);
            }

            return PhysicalTables.Contains(tableName);
        }

        internal bool ExistsInProject(string tableName)
        {
            foreach (UDTTable table in this.Tables)
            {
                if (tableName.ToLower() == table.Name.ToLower())
                    return true;
            }
            return false;
        }

        internal UDTTable GetProjectTable(string tableName)
        {
            foreach (UDTTable table in this.Tables)
            {
                if (tableName.ToLower() == table.Name.ToLower())
                    return table;
            }
            return null;
        }

        internal List<string> ListFields(string tableName)
        {
            if (PhysicalTables == null)
                PhysicalTables = MainForm.CurrentProject.ListPhysicalTables();

            List<string> fields = new List<string>();
            if (PhysicalTables.Contains(tableName))
            {
                XmlElement result = MainForm.CurrentProject.Query("select * from \"" + tableName + "\" where 1=0");
                XmlHelper h = new XmlHelper(result);
                foreach (XmlElement column in h.GetElements("Metadata/Column"))
                    fields.Add(column.GetAttribute("Field"));
            }
            else
            {
                tableName = tableName.Substring(1);

                foreach (UDTTable table in AllUDTTables)
                {
                    if (table.Name != tableName) continue;

                    XmlElement content = table.GetContent();
                    XmlHelper h = new XmlHelper(content);
                    foreach (XmlElement field in h.GetElements("Field"))
                        fields.Add(field.GetAttribute("Name"));
                }
            }
            return fields;
        }

        internal void SetTables(XmlElement tablesElement, bool import)
        {
            XmlHelper h = new XmlHelper(tablesElement);
            List<UDTTable> jp = new List<UDTTable>();
            List<UDTTable> lp = new List<UDTTable>();

            foreach (XmlElement tableElement in h.GetElements("Table"))
            {
                string tableName = tableElement.GetAttribute("Name");
                UDTTable table = this.GetProjectTable(tableName);

                if (table == null)
                {
                    jp.Add(UDTTable.CreateNew(tableName));
                }
            }

            foreach (UDTTable table in this.Tables)
            {
                bool contains = false;
                foreach (XmlElement tableElement in h.GetElements("Table"))
                {
                    string tableName = tableElement.GetAttribute("Name");
                    if (table.Name.ToLower() == tableName.ToLower())
                    {
                        contains = true;
                        break;
                    }
                }

                if (!contains && import)
                    lp.Add(table);
            }

            Parent.SendRequest("UDTService.DDL.SetTables", new Envelope(h));

            this.ModifyProject(jp, lp);
        }

        internal void SetTable(XmlElement xmlElement)
        {
            string tableName = xmlElement.GetAttribute("Name");
            UDTTable table = this.GetProjectTable(tableName);

            XmlHelper h = new XmlHelper(xmlElement);
            Parent.SendRequest("UDTService.DDL.SetTable", new Envelope(h));

            if (table != null) return;

            this.JoinProject(UDTTable.CreateNew(tableName));
        }

        internal XmlElement GetTableInfo(string tableName)
        {
            XmlHelper h = new XmlHelper("<Request/>");
            h.AddElement(".", "TableName", tableName);
            Envelope env = Parent.SendRequest("UDTService.DDL.GetTableInfo", new Envelope(h));
            h = new XmlHelper(env.Body);
            return h.GetElement(".");
        }

        internal static List<string> ListTableNames()
        {
            Envelope env = MainForm.CurrentProject.SendRequest("UDTService.DDL.ListTableNames", new Envelope());
            XmlHelper h = new XmlHelper(env.Body);

            List<string> list = new List<string>();
            foreach (XmlElement e in h.GetElements("TableName"))
                list.Add(e.InnerText);

            return list;
        }

        internal static List<string> ListProjectTables()
        {
            XmlHelper ph = new XmlHelper(MainForm.CurrentProject.Preference);
            List<string> list = new List<string>();
            foreach (XmlElement e in ph.GetElements("Property/UDT"))
            {
                string name = e.GetAttribute("Name");
                list.Add(name.ToLower());
                list.Add(name);
            }

            return list;
        }
    }
}
