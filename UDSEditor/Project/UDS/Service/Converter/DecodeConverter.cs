using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using FISCA.DSAClient;

namespace ProjectManager.Project.UDS.Service.Converter
{
    class DecodeConverter : IConverter
    {
        public const string ConverterType = "Decode";

        #region IConverter 成員

        public string Name { get; private set; }

        public string Type { get { return ConverterType; } }

        public string Key { get; private set; }

        public void Load(XmlElement source)
        {
            Name = source.GetAttribute("Name");
            XmlHelper h = new XmlHelper(source);
            this.Key = h.GetText("Key");
        }

        public XmlElement Output()
        {
            XmlHelper h = new XmlHelper("<Converter />");
            h.SetAttribute(".", "Name", this.Name);
            h.SetAttribute(".", "Type", this.Type);
            h.AddElement(".", "Key", this.Key);
            return h.GetElement(".");
        }

        public XmlElement GetSample()
        {
            XmlHelper h = new XmlHelper("<Converter />");
            h.SetAttribute(".", "Name", this.Name);
            h.SetAttribute(".", "Type", this.Type);

            h.AddElement(".", "Key", "decode_key");

            return h.GetElement(".");
        }
        #endregion
    }
}
