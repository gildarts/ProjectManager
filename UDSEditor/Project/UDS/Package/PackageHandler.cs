using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.DSAClient;
using ProjectManager.Project.UDS.Contract;
using System.Xml;
using ProjectManager.Project.UDS.Service;
using ProjectManager.ActionHandler.UDS.Service;
using ProjectManager.ActionHandler.UDS.Service.DAL;

namespace ProjectManager.Project.UDS.Package
{
    class PackageHandler
    {
        internal event EventHandler Renamed;

        internal string Name { get; private set; }
        internal ContractHandler Contract { get; private set; }

        private List<string> _service;
        internal List<string> Services
        {
            get
            {
                if (_service == null)
                    LoadServices();

                return _service;
            }
        }

        internal PackageHandler(ContractHandler contract, string name)
        {
            this.Contract = contract;
            this.Name = name;
        }

        internal PackageHandler(ContractHandler contract, XmlElement packageDefinition)
        {
            this.Contract = contract;
            this.Name = packageDefinition.GetAttribute("Name");

            _service = new List<string>();

            XmlHelper ph = new XmlHelper(packageDefinition);
            foreach (XmlElement each in ph.GetElements("Service"))
            {
                string serviceName = each.GetAttribute("Name");
                _service.Add(serviceName);
            }
        }

        internal void Rename(string newName)
        {
            XmlHelper req = new XmlHelper("<Request/>");
            req.AddElement(".", "ContractName", this.Contract.Name);
            req.AddElement(".", "PackageName", this.Name);
            req.AddElement(".", "NewPackageName", newName);
            MainForm.CurrentProject.SendRequest("UDSManagerService.RenamePackage", new Envelope(req));

            this.Name = newName;

            if (Renamed != null)
                Renamed.Invoke(this, EventArgs.Empty);
        }

        internal static PackageHandler CreateNew(ContractHandler contract, string packageName)
        {
            return new PackageHandler(contract, packageName);
        }

        internal bool Contains(string serviceName)
        {
            return this.Services.Contains(serviceName);            
        }

        private void LoadServices()
        {
            _service = new List<string>();

            XmlHelper req = new XmlHelper();
            req.AddElement(".", "ContractName", this.Contract.Name);
            req.AddElement(".", "PackageName", this.Name);

            Envelope env = MainForm.CurrentProject.SendRequest("UDSManagerService.ExportPackage", new Envelope(req));
            XmlHelper rsp = new XmlHelper(env.Body);

            foreach (XmlElement each in rsp.GetElements("Service"))
            {
                string serviceName = each.GetAttribute("Name");
                _service.Add(serviceName);
            }
        }

        internal void AddService(ServiceEntity sh)
        {
            ServiceDAL.AddService(this.Contract.Name, this.Name, sh);
            LoadServices();
        }

        internal void DeleteService(string serviceName)
        {
            XmlHelper req = new XmlHelper();
            req.AddElement(".", "ContractName", this.Contract.Name);
            req.AddElement(".", "PackageName", this.Name);
            req.AddElement(".", "ServiceName", serviceName);

            MainForm.CurrentProject.SendRequest("UDSManagerService.DeleteService", new Envelope(req));
                        
            this.Services.Remove(serviceName);            
        }

        internal XmlElement GetXml()
        {
            XmlHelper h = new XmlHelper("<Package/>");
            h.SetAttribute(".", "Name", this.Name);

            foreach (string service in this.Services)
            {
                XmlElement def = ServiceDAL.GetRawService(this.Contract.Name, this.Name, service);
                h.AddElement(".", def);
            }

            return h.GetElement(".");
        }


    }
}
