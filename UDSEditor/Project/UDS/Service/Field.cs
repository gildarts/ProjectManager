using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using FISCA.DSAClient;
using ProjectManager.Util;
using ProjectManager.ActionHandler.UDS.Service;
using System.Windows.Forms;

namespace ProjectManager.Project.UDS.Service
{
    class Field
    {
        internal string Source { get; set; }
        internal bool Mandatory { get; set; }
        internal string Target { get; set; }
        internal IOType InputType { get; set; }
        internal bool Required { get; set; }
        internal string Alias { get; set; }
        internal SourceType SourceType { get; set; }
        internal IOType OutputType { get; set; }
        internal bool AutoNumber { get; set; }
        internal bool Quote { get; set; }
        internal ConverterType InputConverter { get; set; }
        internal ConverterType OutputConverter { get; set; }

        internal Field() { }

        internal Field(XmlElement fieldElement)
        {
            XmlHelper h = new XmlHelper(fieldElement);

            this.Source = fieldElement.GetAttribute("Source");
            this.Target = fieldElement.GetAttribute("Target");
            this.InputConverter = ConverterType.Parse(fieldElement.GetAttribute("InputConverter"));
            this.OutputConverter = ConverterType.Parse(fieldElement.GetAttribute("OutputConverter"));
            this.Quote = h.TryGetBoolean("@Quote", true);
            this.Mandatory = h.TryGetBoolean("@Mandatory", false);
            this.Alias = fieldElement.GetAttribute("Alias");
            this.Required = h.TryGetBoolean("@Required", false);
            this.AutoNumber = h.TryGetBoolean("@AutoNumber", false);

            SourceType sType = SourceType.Request;
            if (!Enum.TryParse<SourceType>(fieldElement.GetAttribute("SourceType"), true, out sType))
                sType = SourceType.Request;
            this.SourceType = sType;

            IOType iType = IOType.Element;
            if (!Enum.TryParse<IOType>(fieldElement.GetAttribute("InputType"), true, out iType))
                iType = IOType.Element;
            this.InputType = iType;

            IOType oType = IOType.Element;
            if (!Enum.TryParse<IOType>(fieldElement.GetAttribute("OutputType"), true, out oType))
                oType = IOType.Element;
            this.OutputType = oType;

        }

        internal static Field CreateNew(string fieldName)
        {
            Field field = new Field();
            field.Target = fieldName;
            field.Mandatory = true;
            field.InputConverter = ConverterType.Empty;
            field.OutputConverter = ConverterType.Empty;
            field.Quote = true;
            field.AutoNumber = false;
            field.Required = false;
            field.Source = StringUtil.ConvertToDisplayName(fieldName);
            field.Alias = StringUtil.ConvertToDisplayName(fieldName);

            return field;
        }

        internal XmlElement GetXml(ActionHandler.UDS.Service.ServiceAction serviceAction)
        {
            XmlHelper h = new XmlHelper("<Field/>");
            XmlElement e = h.GetElement(".");

            e.SetAttribute("Source", this.Source);

            if (serviceAction == ServiceAction.Select && !string.IsNullOrWhiteSpace(this.Alias))
                e.SetAttribute("Alias", this.Alias);

            if (serviceAction == ServiceAction.Insert && this.AutoNumber)
                e.SetAttribute("AutoNumber", this.AutoNumber.ToString());

            if (serviceAction != ServiceAction.Select && !this.InputConverter.Equals(ConverterType.Empty))
                e.SetAttribute("InputConverter", this.InputConverter.ToString());

            if (serviceAction == ServiceAction.Select && !this.OutputConverter.Equals(ConverterType.Empty))
                e.SetAttribute("OutputConverter", this.OutputConverter.ToString());

            if (serviceAction != ServiceAction.Select && this.InputType != IOType.Element)
                e.SetAttribute("InputType", this.InputType.ToString());

            if (serviceAction == ServiceAction.Select && this.Mandatory)
                e.SetAttribute("Mandatory", this.Mandatory.ToString());

            if (serviceAction == ServiceAction.Select && this.OutputType != IOType.Element)
                e.SetAttribute("OutputType", this.OutputType.ToString());

            if (serviceAction != ServiceAction.Select && !this.Quote)
                e.SetAttribute("Quote", this.Quote.ToString());

            if (serviceAction != ServiceAction.Select && this.Required)
                e.SetAttribute("Required", this.Required.ToString());

            if (serviceAction != ServiceAction.Select && this.SourceType != Service.SourceType.Request)
                e.SetAttribute("SourceType", this.SourceType.ToString());

            e.SetAttribute("Target", this.Target);

            return e;
        }

        internal static Field Parse(System.Windows.Forms.DataGridViewRow row)
        {
            Field field = new Field();
            field.Alias = GetValue(row, "Alias");
            field.AutoNumber = GetBoolean(row, "AutoNumber", false);
            field.InputConverter = ConverterType.Parse(GetValue(row, "InputConverter"));
            field.InputType = GetIOType(row, "InputType");
            field.Mandatory = GetBoolean(row, "Mandatory", false);
            field.OutputConverter = ConverterType.Parse(GetValue(row, "OutputConverter"));
            field.OutputType = GetIOType(row, "OutputType");
            field.Quote = GetBoolean(row, "Quote", true);
            field.Required = GetBoolean(row, "Required", false);
            field.Source = GetValue(row, "Source");
            field.SourceType = GetSourceType(row, "SourceType");
            field.Target = GetValue(row, "Target");
            return field;
        }

        private static SourceType GetSourceType(DataGridViewRow row, string title)
        {
            string value = GetValue(row, title);
            SourceType type;
            if (!Enum.TryParse<SourceType>(value, true, out type))
                type = SourceType.Request;
            return type;
        }

        private static IOType GetIOType(DataGridViewRow row, string title)
        {
            string value = GetValue(row, title);
            IOType type;
            if (!Enum.TryParse<IOType>(value, true, out type))
                type = IOType.Element;
            return type;
        }

        private static bool GetBoolean(DataGridViewRow row, string title, bool defaultValue)
        {
            string value = GetValue(row, title);
            if (value.ToLower() == "true")
                return true;
            if (value.ToLower() == "false")
                return false;
            return defaultValue;
        }

        private static string GetValue(DataGridViewRow row, string title)
        {
            foreach (DataGridViewColumn col in row.DataGridView.Columns)
            {
                if (col.HeaderText.ToLower() == title.ToLower())
                    return row.Cells[col.Name].Value + string.Empty;
            }
            return string.Empty;
        }
    }


    enum IOType
    {
        Element, Attribute, Xml, CDATA
    }

    enum SourceType
    {
        Request, Variable
    }
}
