using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;

namespace ProjectManager.Util.Converter
{
    class ComponentConverter : IServiceConverter
    {
        #region IServiceConverter 成員

        public XElement ToUDS(System.Xml.Linq.XElement physicalService)
        {
            string name = physicalService.Attribute("Name").Value;

            XElement x = new XElement("Service", new XAttribute("Name", name), new XAttribute("Enabled", "true"));
            XElement def = new XElement("Definition", new XAttribute("Type", "DBHelper"));
            x.Add(def);

            XElement handler = physicalService.XPathSelectElement("Property[@Name='Definition']/Service/ServiceDescription/Handler");
            foreach (XElement e in handler.Elements())
                def.Add(e);

            return x;
        }

        public XElement ToPhysical(System.Xml.Linq.XElement udsService)
        {
            string name = udsService.Attribute("Name").Value;
            XElement x = new XElement("Service", new XAttribute("Name", name));
            XElement prop = new XElement("Property", new XAttribute("Name", "Definition"));
            x.Add(prop);

            XElement serv = new XElement("Service");
            prop.Add(serv);

            XElement sd = new XElement("ServiceDescription");
            serv.Add(sd);

            XElement handler = new XElement("Handler", new XAttribute("ExecType", "Java.Method"));
            sd.Add(handler);

            XElement udsDefinition = udsService.Element("Definition");
            foreach (XElement e in udsDefinition.Elements())
                sd.Add(e);

            XElement usage = new XElement("Usage");
            sd.Add(usage);

            XElement sh = new XElement("ServiceHelp");
            sh.Add(new XElement("Description"));
            sh.Add(new XElement("RequestSDDL"));
            sh.Add(new XElement("ResponseSDDL"));
            sh.Add(new XElement("Errors"));
            sh.Add(new XElement("RelatedDocumentServices"));
            sh.Add(new XElement("Samples"));
            serv.Add(sh);
            return x;
        }

        #endregion
    }
}
