using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using FISCA.DSAClient;
using System.Numerics;

namespace ProjectManager.Project.UDT
{
    class UDTTable
    {
        internal event EventHandler Renamed;
        internal event EventHandler Saved;

        public string Name { get; private set; }

        public UDTTable(string tableName)
        {
            Name = tableName.ToLower();
        }

        internal static UDTTable CreateNew(string tableName)
        {
            return new UDTTable(tableName);
        }

        internal void Rename(string newName)
        {
            newName = newName.ToLower();
            XmlHelper req = new XmlHelper("<Request/>");
            req.AddElement(".", "TableName", this.Name);
            req.AddElement(".", "NewName", newName);
            MainForm.CurrentProject.SendRequest("UDTService.DDL.RenameTable", new Envelope(req));

            XmlHelper pref = new XmlHelper(MainForm.CurrentProject.Preference);
            XmlElement e = pref.GetElement("Property[@Name='UDT']/UDT[@Name='" + this.Name + "']");
            e.SetAttribute("Name", newName);
            MainForm.CurrentProject.UpdateProjectPreference(pref.GetElement("."));

            this.Name = newName;

            if (Renamed != null)
                Renamed.Invoke(this, EventArgs.Empty);
        }

        internal XmlElement GetContent()
        {
            return MainForm.CurrentUDT.GetTableInfo(this.Name);
        }

        internal void OnSave(object saver)
        {
            if (Saved != null)
                Saved.Invoke(saver, EventArgs.Empty);
        }

        internal static bool IsDefaultField(XmlElement fieldElement)
        {
            string fieldname = fieldElement.GetAttribute("Name");
            return IsDefaultField(fieldname);
        }

        internal static bool IsDefaultField(string fieldname)
        {
            fieldname = fieldname.ToLower();
            if (fieldname == "uid" || fieldname == "last_update")
                return true;
            return false;
        }

        internal static void ValidFieldValue(XmlElement xml, string currentValue)
        {
            bool allowNull = true;
            if (!bool.TryParse(xml.GetAttribute("AllowNull"), out allowNull))
                allowNull = true;

            if (!allowNull && string.IsNullOrWhiteSpace(currentValue))
                throw new Exception("此欄位不允許空白");

            string datatype = xml.GetAttribute("DataType").ToLower();
            ValidDataType(datatype, allowNull, currentValue);
        }

        internal static void ValidDataType(string datatype, bool allownull, string currentValue)
        {
            if (allownull && string.IsNullOrWhiteSpace(currentValue)) return;
            if(datatype == "string") return;

            if (datatype == "number")
            {
                decimal d = 0;
                if (!decimal.TryParse(currentValue, out d))
                    throw new Exception("必須為數字");
            }

            if (datatype == "integer")
            {
                int d = 0;
                if (!int.TryParse(currentValue, out d))
                    throw new Exception("必須為整數");
            }

            if (datatype == "bigint")
            {
                BigInteger d = 0;
                if (!BigInteger.TryParse(currentValue, out d))
                    throw new Exception("必須為整數");
            }

            if (datatype == "boolean")
            {
                bool b = false;
                if (!bool.TryParse(currentValue, out b))
                    throw new Exception("必須為布林值");
            }

            if (datatype == "datetime")
            {
                DateTime d = DateTime.Now;
                if (!DateTime.TryParse(currentValue, out d))
                    throw new Exception("必須為yyyy/MM/dd hh:mm:ss");
            }
        }
    }
}
