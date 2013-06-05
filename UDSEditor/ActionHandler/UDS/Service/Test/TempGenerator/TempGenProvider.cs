using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectManager.Project.UDS.Service;
using System.Xml;
using FISCA.DSAClient;

namespace ProjectManager.ActionHandler.UDS.Service.Test.TempGenerator
{
    class TempGenProvider
    {
        public static string GenerateTemp(XmlElement service){
            XmlHelper h = new XmlHelper(service);
            string serviceAction = h.GetText("Action");
            ServiceAction action = ServiceFactory.ConvertServiceAction(serviceAction);

            ITempGenerator t;
            if (action == ServiceAction.Select)
                t = new SelectTempGenerator();
            else if (action == ServiceAction.Delete)
                t = new DeleteTempGenerator();
            else if (action == ServiceAction.Insert)
                t = new InsertTempGenerator();
            else
                t = new UpdateTempGenerator();
            XmlElement e =  t.Generate(service);
            return XmlHelper.Format(e.OuterXml);
        }
    }
}
