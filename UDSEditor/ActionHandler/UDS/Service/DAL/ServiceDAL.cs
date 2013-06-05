using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.DSAClient;
using System.Xml;
using ProjectManager.Project.UDS.Service;

namespace ProjectManager.ActionHandler.UDS.Service.DAL
{
    class ServiceDAL
    {
        public static void Rename(string contractName, string packageName, string serviceName, string newName)
        {
            XmlHelper req = new XmlHelper();
            req.AddElement(".", "ContractName", contractName);
            req.AddElement(".", "PackageName", packageName);
            req.AddElement(".", "ServiceName", serviceName);
            req.AddElement(".", "NewServiceName", newName);

            MainForm.CurrentProject.SendRequest("UDSManagerService.RenameService", new Envelope(req));
        }

        public static XmlElement GetRawService(string contractName, string packageName, string serviceName)
        {
            XmlHelper req = new XmlHelper();
            req.AddElement(".", "ContractName", contractName);
            req.AddElement(".", "PackageName", packageName);
            req.AddElement(".", "ServiceName", serviceName);

            Envelope env = MainForm.CurrentProject.SendRequest("UDSManagerService.GetServiceDefinition", new Envelope(req));
            XmlHelper rsp = new XmlHelper(env.Body);
            return rsp.GetElement(".");
        }

        public static XmlElement GetServiceDefinition(string contractName, string packageName, string serviceName)
        {
            XmlElement raw = GetRawService(contractName, packageName, serviceName);
            XmlHelper h = new XmlHelper(raw);
            return h.GetElement("Definition");
        }

        public static void SetDefinition(string contractName, string packageName, string serviceName, XmlElement definition)
        {
            XmlHelper req = new XmlHelper();
            req.AddElement(".", "ContractName", contractName);
            req.AddElement(".", "PackageName", packageName);
            req.AddElement(".", "ServiceName", serviceName);
            req.AddElement(".", definition);

            MainForm.CurrentProject.SendRequest("UDSManagerService.SetServiceDefinition", new Envelope(req));
        }

        public static void AddService(string contractName, string packageName, ServiceEntity service)
        {
            XmlHelper req = new XmlHelper();
            req.AddElement(".", "ContractName", contractName);
            req.AddElement(".", "PackageName", packageName);
            req.AddElement(".", service.GetServiceXml());
         
            MainForm.CurrentProject.SendRequest("UDSManagerService.CreateService", new Envelope(req));

        }
    }
}
