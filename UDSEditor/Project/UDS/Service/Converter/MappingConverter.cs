using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using FISCA.DSAClient;

namespace ProjectManager.Project.UDS.Service.Converter
{
    class MappingConverter : IConverter
    {
        public const string ConverterType = "Mapping";

        public string Name { get; private set; }
        public string Type { get { return ConverterType; } }
        public string DefaultValue { get; private set; }
        public Dictionary<string, string> Map { get; private set; }

        public MappingConverter()
        {
            Map = new Dictionary<string, string>();
        }

        #region IConverter 成員


        public void Load(XmlElement source)
        {
            Name = source.GetAttribute("Name");
            XmlHelper h = new XmlHelper(source);

            if (h.GetElement("Default") != null)
                DefaultValue = h.GetText("Default");
            else
                DefaultValue = null;

            Map = new Dictionary<string, string>();

            foreach (XmlElement e in h.GetElements("Item"))
            {
                string key = e.GetAttribute("If");
                string value = e.InnerText;

                Map.Add(key, value);
            }
        }

        public XmlElement Output()
        {
            XmlHelper h = new XmlHelper("<Converter />");
            h.SetAttribute(".", "Name", this.Name);
            h.SetAttribute(".", "Type", this.Type);
            
            foreach(string key in Map.Keys)
            {
                XmlElement e = h.AddElement(".", "Item", Map[key]);
                e.SetAttribute("If", key);
            }

            if(DefaultValue != null)
                h.AddElement(".", "Default", this.DefaultValue);

            return h.GetElement(".");
        }

        public XmlElement GetSample()
        {
            XmlHelper h = new XmlHelper("<Converter />");
            h.SetAttribute(".", "Name", this.Name);
            h.SetAttribute(".", "Type", this.Type);

            XmlElement e = h.AddElement(".", "Item", "return_value");
            e.SetAttribute("If", "get_value");

            h.AddElement(".", "Default", "no_match_default_value");

            return h.GetElement(".");
        }

        #endregion
    }
}
