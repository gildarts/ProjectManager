using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using FISCA.DSAClient;

namespace ProjectManager.Project.UDS.Service.Converter
{
    class DateFormatConverter:IConverter
    {
        public const string ConverterType = "DateFormater";
        public const string DefaultInputFormat = "yyyy-MM-dd HH:mm:ss";
        public const string DefaultOutputFormat = "yyyy/MM/dd HH:mm:ss";

        public string InputFormat { get; private set; }
        public string OutputFormat { get; private set; }

        #region IConverter 成員

        public string Name { get; private set; }

        public string Type
        {
            get { return ConverterType; }
        }

        public void Load(XmlElement source)
        {
            Name = source.GetAttribute("Name");
            XmlHelper h = new XmlHelper(source);

            InputFormat = h.GetText("InputFormat");
            if (string.IsNullOrWhiteSpace("InputFormat"))
                InputFormat = DefaultInputFormat;

            OutputFormat = h.GetText("OutputFormat");
            if (string.IsNullOrWhiteSpace(this.OutputFormat))
                OutputFormat = DefaultOutputFormat;
        }

        public XmlElement Output()
        {
            XmlHelper h = new XmlHelper("<Converter />");
            h.SetAttribute(".", "Name", this.Name);
            h.SetAttribute(".", "Type", this.Type);

            h.AddElement(".", "InputFormat", this.InputFormat);
            h.AddElement(".", "OutputFormat", this.OutputFormat);

            return h.GetElement(".");
        }

        public XmlElement GetSample()
        {
            XmlHelper h = new XmlHelper("<Converter />");
            h.SetAttribute(".", "Name", this.Name);
            h.SetAttribute(".", "Type", this.Type);

            h.AddElement(".", "InputFormat", DefaultInputFormat);
            h.AddElement(".", "OutputFormat", DefaultOutputFormat);

            return h.GetElement(".");
        }
        #endregion
    }
}
