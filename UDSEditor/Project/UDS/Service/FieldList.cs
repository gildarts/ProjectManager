using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using FISCA.DSAClient;
using ProjectManager.ActionHandler.UDS.Service;

namespace ProjectManager.Project.UDS.Service
{
    class FieldList
    {
        internal string Name { get; set; }
        internal string Source { get; set; }        
        internal List<Field> Fields { get; private set; }

        internal FieldList(string name, string source)
        {
            this.Name = name;
            this.Source = source;
            Fields = new List<Field>();
        }

        internal FieldList(XmlElement fieldListElement)
        {
            Fields = new List<Field>();
            this.Name = string.Empty;       
            this.Source = string.Empty;

            if (fieldListElement == null)
                return;
            
            this.Name = fieldListElement.GetAttribute("Name");
            this.Source = fieldListElement.GetAttribute("Source");

           
            XmlHelper h = new XmlHelper(fieldListElement);
            foreach (XmlElement fieldElement in h.GetElements("Field"))
            {
                Field field = new Field(fieldElement);
                Fields.Add(field);
            }
        }

        internal XmlElement GetXml(ServiceAction serviceAction)
        {
            XmlHelper h = new XmlHelper("<FieldList/>");
            h.SetAttribute(".", "Name", this.Name);
            h.SetAttribute(".", "Source", this.Source);

            foreach (Field field in this.Fields)
            {
                h.AddElement(".", field.GetXml(serviceAction));
            }
            return h.GetElement(".");
        }
    }
}
