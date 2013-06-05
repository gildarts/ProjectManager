using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using FISCA.DSAClient;

namespace ProjectManager.Project.UDS.Service
{
    class Order
    {
        internal string Target { get; private set; }
        internal string Source { get; private set; }

        internal Order(XmlElement order)
        {
            this.Target = order.GetAttribute("Target");
            this.Source = order.GetAttribute("Source");
        }

        internal Order(string target, string source)
        {
            this.Target = target;
            this.Source = source;
        }

        internal XmlElement GetXml()
        {
            XmlHelper h = new XmlHelper("<Order/>");
            h.SetAttribute(".", "Target", Target);
            h.SetAttribute(".", "Source", Source);
            return h.GetElement(".");
        }
    }
}
