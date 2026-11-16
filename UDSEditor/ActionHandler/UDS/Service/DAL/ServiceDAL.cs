using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.DSAClient;
using System.Xml;
using ProjectManager.Project.UDS.Service;
using ProjectManager.Project.UDS.Sign;

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

            // 簽章綁了 service 名稱，改名後要用新名稱重簽。
            // 先簽好再改名：簽不成就什麼都不動，不會留下改了名卻沒簽章的 service。
            XmlElement definition = GetServiceDefinition(contractName, packageName, serviceName);
            XmlElement signed = definition == null ? null : UDSSigner.Sign(contractName, packageName, newName, definition);

            MainForm.CurrentProject.SendRequest("UDSManagerService.RenameService", new Envelope(req));

            if (signed != null)
                SendDefinition(contractName, packageName, newName, signed);
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

        /// <summary>
        /// 儲存 service 定義。儲存前會先送金鑰中心簽章，簽不成會丟出例外、不會存。
        /// </summary>
        public static void SetDefinition(string contractName, string packageName, string serviceName, XmlElement definition)
        {
            XmlElement signed = UDSSigner.Sign(contractName, packageName, serviceName, definition);
            SendDefinition(contractName, packageName, serviceName, signed);
        }

        private static void SendDefinition(string contractName, string packageName, string serviceName, XmlElement definition)
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

            XmlElement serviceXml = service.GetServiceXml();
            XmlElement definition = serviceXml.SelectSingleNode("Definition") as XmlElement;
            if (definition != null)
            {
                // 新增的 service 也要先簽章，把簽好的 definition 換回去再送出
                XmlElement signed = UDSSigner.Sign(contractName, packageName, serviceXml.GetAttribute("Name"), definition);
                serviceXml.ReplaceChild(serviceXml.OwnerDocument.ImportNode(signed, true), definition);
            }
            req.AddElement(".", serviceXml);

            MainForm.CurrentProject.SendRequest("UDSManagerService.CreateService", new Envelope(req));

        }
    }
}
