using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using FISCA.DSAClient;

namespace ProjectManager.Project.UDS.Service
{
    class OrderList
    {
        internal string Source { get; set; }
        internal string Name { get; set; }
        internal List<Order> Orders { get; private set; }

        internal OrderList(XmlElement orderElement)
        {
            this.Orders = new List<Order>();
            this.Name = string.Empty;
            this.Source = string.Empty;

            if (orderElement == null) return;

            this.Name = orderElement.GetAttribute("Name");
            this.Source = orderElement.GetAttribute("Source");
         
            XmlHelper h = new XmlHelper(orderElement);
            foreach (XmlElement xml in h.GetElements("Order"))
                Orders.Add(new Order(xml));
        }

        internal OrderList(string name, string source)
        {
            this.Name = name;
            this.Source = source;
            this.Orders = new List<Order>();
        }

        internal XmlElement GetXml()
        {
            XmlHelper h = new XmlHelper("<Orders/>");
            h.SetAttribute(".", "Name", this.Name);
            h.SetAttribute(".", "Source", this.Source);

            foreach (Order order in this.Orders)
                h.AddElement(".", order.GetXml());

            return h.GetElement(".");
        }
    }
}
