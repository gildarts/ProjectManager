using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.DSAClient;
using System.Xml;
using System.Reflection;
using ProjectManager.Util;
using System.Windows.Forms;

namespace ProjectManager.Project.UDS.Service
{
    class Condition
    {
        internal bool Quote { get; set; }

        internal string Source { get; set; }

        internal string Target { get; set; }

        internal string Comparer { get; set; }

        internal ConverterType InputConverter { get; set; }

        internal bool Required { get; set; }

        internal string EmptyReplacement { get; set; }

        internal SourceType SourceType { get; set; }

        internal Condition(XmlElement conditionElement)
        {
            XmlHelper h = new XmlHelper(conditionElement);
            this.Comparer = conditionElement.GetAttribute("Comparer");
            this.EmptyReplacement = conditionElement.GetAttribute("EmptyReplacement");
            this.InputConverter = ConverterType.Parse(conditionElement.GetAttribute("InputConverter"));
            this.Quote = h.TryGetBoolean("@Quote", true);
            this.Required = h.TryGetBoolean("@Required", false);
            this.Source = conditionElement.GetAttribute("Source");

            SourceType sType = Service.SourceType.Request;
            if (!Enum.TryParse<SourceType>(conditionElement.GetAttribute("SourceType"), true, out sType))
                sType = Service.SourceType.Request;
            this.SourceType = sType;

            this.Target = conditionElement.GetAttribute("Target");
        }

        private Condition(string fieldName)
        {
            this.Target = fieldName;
            this.Source = StringUtil.ConvertToDisplayName(fieldName);
            this.Comparer = "=";
            this.EmptyReplacement = string.Empty;
            this.InputConverter = ConverterType.Empty;
            this.Quote = true;
            this.Required = false;
            this.SourceType = Service.SourceType.Request;     
        }

        public Condition()
        {
            // TODO: Complete member initialization       
            this.Comparer = "=";
            this.EmptyReplacement = string.Empty;
            this.InputConverter = ConverterType.Empty;
            this.Quote = true;
            this.Required = false;
            this.SourceType = Service.SourceType.Request;     
        }

        internal XmlElement GetXml()
        {
            XmlHelper h = new XmlHelper("<Condition/>");
            XmlElement e = h.GetElement(".");

            e.SetAttribute("Source", this.Source);
            e.SetAttribute("Target", this.Target);
            if(this.Comparer != "=" && this.Comparer != string.Empty)
                e.SetAttribute("Comparer", this.Comparer);
            if(!string.IsNullOrWhiteSpace(this.EmptyReplacement))
                e.SetAttribute("EmptyReplacement", this.Comparer);
            if (!this.InputConverter.Equals(ConverterType.Empty))
                e.SetAttribute("InputConverter", this.InputConverter.ToString());
            if(!this.Quote)
                e.SetAttribute("Quote", this.Quote.ToString());
            if(this.Required)
                e.SetAttribute("Required", this.Required.ToString());
            if(this.SourceType == Service.SourceType.Variable)
                e.SetAttribute("SourceType", this.SourceType.ToString());            
            return e;         
        }

        internal static Condition CreateNew(string fieldName)
        {
            return new Condition(fieldName);
        }

        internal static Condition Parse(DataGridViewRow row)
        {
            Condition condition = new Condition();
            condition.Comparer = GetValue(row, "Comparer");
            condition.EmptyReplacement = GetValue(row, "EmptyReplacement");
            condition.InputConverter = ConverterType.Parse(GetValue(row, "InputConverter"));
            condition.Quote = GetBoolean(row, "Quote", true);
            condition.Required = GetBoolean(row, "Required", false);
            condition.Source = GetValue(row, "Source");
            condition.SourceType = GetSourceType(row, "SourceType");
            condition.Target = GetValue(row, "Target");
            return condition;
        }

        private static SourceType GetSourceType(DataGridViewRow row, string title)
        {
            string value = GetValue(row, title);
            SourceType type;
            if (!Enum.TryParse<SourceType>(value, true, out type))
                type = SourceType.Request;
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

        private static string GetValue(DataGridViewRow row, string name)
        {
            foreach (DataGridViewColumn col in row.DataGridView.Columns)
            {
                if (col.HeaderText.ToLower() == name.ToLower())
                    return row.Cells[col.Name].Value + string.Empty;
            }
            return string.Empty;
        }
    }
}
