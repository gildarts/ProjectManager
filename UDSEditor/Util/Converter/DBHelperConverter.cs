using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;

namespace ProjectManager.Util.Converter
{
    class DBHelperConverter : IServiceConverter
    {
        #region IServiceConverter 成員

        public XElement ToUDS(XElement physicalService)
        {
            string name = physicalService.Attribute("Name").Value;

            XElement x = new XElement("Service", new XAttribute("Name", name), new XAttribute("Enabled", "true"));
            XElement def = new XElement("Definition", new XAttribute("Type", "DBHelper"));
            x.Add(def);

            XElement handler = physicalService.XPathSelectElement("Property[@Name='Definition']/Service/ServiceDescription/Handler[@ExecType='Util.DBHelper']");
            string resourceName = handler.Element("ResourceName").Value;

            XElement resource = handler.XPathSelectElement("Resources/Resource[@Name='" + resourceName + "']");
            
            foreach (XElement e in resource.Elements())
                def.Add(e);
            
            return x;
        }

        public XElement ToPhysical(XElement udsService)
        {
            string name = udsService.Attribute("Name").Value;
            XElement x = new XElement("Service", new XAttribute("Name", name));
            XElement prop = new XElement("Property", new XAttribute("Name", "Definition"));
            x.Add(prop);

            XElement serv = new XElement("Service");
            prop.Add(serv);

            XElement sd = new XElement("ServiceDescription");
            serv.Add(sd);

            XElement handler = new XElement("Handler", new XAttribute("ExecType", "Util.DBHelper"));
            sd.Add(handler);

            XElement resourceName = new XElement("ResourceName", "DBHelperRule");
            handler.Add(resourceName);

            XElement resources = new XElement("Resources");
            handler.Add(resources);

            XElement resource = new XElement("Resource", new XAttribute("Name", "DBHelperRule"));
            resources.Add(resource);

            XElement udsDefinition = udsService.Element("Definition");
            foreach (XElement e in udsDefinition.Elements())
                resource.Add(e);

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
