using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ProjectManager.Project.UDS.Service.Converter
{
    public interface IConverter
    {
        string Name { get; }
        string Type { get; }

        void Load(XmlElement source);
        XmlElement Output();
        XmlElement GetSample();
    }
}
