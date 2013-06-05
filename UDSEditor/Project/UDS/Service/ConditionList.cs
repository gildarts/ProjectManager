using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.DSAClient;
using System.Xml;

namespace ProjectManager.Project.UDS.Service
{
    class ConditionList
    {
        internal string Name { get; set; }
        internal string Source { get; set; }
        internal bool Required { get; set; }
        internal List<Condition> Conditions { get; private set; }

        internal ConditionList(string name, string source, bool required)
        {
            this.Name = name;
            this.Source = source;
            this.Required = required;
            Conditions = new List<Condition>();
        }

        internal ConditionList(XmlElement conditionsElement)
        {
            Conditions = new List<Condition>();
            this.Name = string.Empty;
            this.Required = false;
            this.Source = string.Empty;

            if (conditionsElement == null)
                return;

            XmlHelper h = new XmlHelper(conditionsElement);
            this.Name = conditionsElement.GetAttribute("Name");
            this.Source = conditionsElement.GetAttribute("Source");
            this.Required = h.TryGetBoolean("@Required", false);
                        
            foreach (XmlElement conElement in h.GetElements("Condition"))
            {
                Condition condition = new Condition(conElement);
                Conditions.Add(condition);
            }
        }

        internal XmlElement GetXml()
        {
            XmlHelper h = new XmlHelper("<Conditions/>");
            h.SetAttribute(".", "Name", this.Name);
            h.SetAttribute(".", "Source", this.Source);
            h.SetAttribute(".", "Required", this.Required.ToString());

            foreach (Condition condition in this.Conditions)
            {
                h.AddElement(".", condition.GetXml());
            }
            return h.GetElement(".");
        }
    }
}
