using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;

namespace ProjectManager.Util.Converter
{
    class ServiceConverterFactory
    {
        public static IServiceConverter CreateToPhysicalConverterInstance(XElement udsServiceElement)
        {
            XElement x = udsServiceElement.Element("Definition");
            string type = x.Attribute("Type").Value;
            return CreateInstance(type);
        }

        public static IServiceConverter CreateToUDSConverterInstance(XElement physicalServiceElement)
        {
            XElement handler = physicalServiceElement.XPathSelectElement("Property[@Name='Definition']/Service/ServiceDescription/Handler");
            string type = handler.Attribute("ExecType").Value;
            return CreateInstance(type);
        }

        public static IServiceConverter CreateInstance(string type)
        {
            type = type.ToLower();
            if (type == "dbhelper" || type == "util.dbhelper" || type == "util.dbhelper2" || type == "util.dbhelper3")
                return new DBHelperConverter();
            if (type == "component" || type == "java.method")
                return new ComponentConverter();
            if (type == "remote" || type == "remotecomponent")
                return new RemoteComponentConverter();
            throw new Exception("Not support type : " + type);
        }
    }
}
