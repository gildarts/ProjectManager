using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using ProjectManager.Project.UDS.Service;

namespace ProjectManager.ActionHandler.UDS.Service.Editable.Javascript
{
    internal class JSServiceEntity : ServiceEntity
    {
        private string _serviceName;

        internal JSServiceEntity(string serviceName)
            : base(serviceName, "")
        {
            _serviceName = serviceName;
        }

        internal override System.Xml.XmlElement GetServiceXml()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml("<Service Enabled=\"true\" Name=\"" + _serviceName + "\"><Definition Type=\"JavaScript\"><Code></Code></Definition></Service>");

            return doc.DocumentElement;
        }
    }
}
