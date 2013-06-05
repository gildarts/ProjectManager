using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using ProjectManager.Project.UDS.Contract;
using FISCA.DSAClient;
using ProjectManager.ActionHandler.UDS.Contract;

namespace ProjectManager.Project.UDS
{
    class UDSHandler
    {
        internal event EventHandler ProjectContractChanged;

        internal List<ContractHandler> Contracts { get; private set; }
        internal ProjectHandler Parent { get; private set; }
        internal List<ContractHandler> AllContracts { get; private set; }

        public UDSHandler(ProjectHandler project, XmlElement source)
        {
            Parent = project;
            Contracts = new List<ContractHandler>();
            AllContracts = new List<ContractHandler>();

            XmlHelper ph = new XmlHelper(project.Preference);
            List<string> list = new List<string>();
            foreach (XmlElement e in ph.GetElements("Property/Contract"))
            {
                string name = e.GetAttribute("Name");
                list.Add(name);
            }

            XmlHelper h = new XmlHelper(source);
            foreach (XmlElement contractElement in h.GetElements("Contract"))
            {
                ContractHandler ch = new ContractHandler(contractElement);

                if (list.Contains(ch.Name))
                    Contracts.Add(ch);

                AllContracts.Add(ch);
            }
        }

        internal ContractHandler AddContract(string ContractName, ExtendType extend)
        {
            XmlHelper req = new XmlHelper("<Request/>");
            req.AddElement(".", "ContractName", ContractName);

            req.AddElement(".", "Definition");
            XmlElement authElement = req.AddElement("Definition", "Authentication");

            if (extend == ExtendType.open)
            {
                XmlElement e = req.AddElement("Definition/Authentication", "Public");
                e.SetAttribute("Enabled", "true");
            }
            else
            {
                if (extend != ExtendType.none && extend != ExtendType.open)
                    authElement.SetAttribute("Extends", extend.ToString());
            }

            Parent.SendRequest("UDSManagerService.CreateContract", new Envelope(req));

            ContractHandler contract = ContractHandler.CreateNew(ContractName, extend);
            JoinProject(contract);
            return contract;
        }

        internal bool ContainContract(string contractName)
        {
            foreach (ContractHandler ch in AllContracts)
                if (ch.Name == contractName) return true;
            return false;
        }

        internal void DeleteContract(string contractName)
        {
            XmlHelper h = new XmlHelper();
            h.AddElement(".", "ContractName", contractName);

            Parent.SendRequest("UDSManagerService.DeleteContract", new Envelope(h));

            LeaveProject(contractName);
        }

        internal void JoinProject(ContractHandler contract)
        {
            List<ContractHandler> cs = new List<ContractHandler>();
            cs.Add(contract);
            JoinProjects(cs);
        }

        internal void JoinProjects(List<ContractHandler> jt)
        {
            ModifyProjects(jt, null);
        }

        internal void LeaveProject(string contractName)
        {
            ContractHandler contract = null;
            foreach (ContractHandler ch in this.Contracts)
                if (ch.Name == contractName) contract = ch;

            if (contract == null) return;
            List<ContractHandler> cs = new List<ContractHandler>();
            cs.Add(contract);
            LeaveProjects(cs);
        }

        internal void LeaveProjects(List<ContractHandler> lt)
        {
            ModifyProjects(null, lt);
        }

        internal void ModifyProjects(List<ContractHandler> joinProjects, List<ContractHandler> leaveProjects)
        {
            XmlHelper pref = new XmlHelper(Parent.Preference);
            if (joinProjects != null)
            {
                foreach (ContractHandler contract in joinProjects)
                {
                    XmlElement udt = pref.AddElement("Property[@Name='UDS']", "Contract");
                    udt.SetAttribute("Name", contract.Name);
                    this.Contracts.Add(contract);
                }
            }

            if (leaveProjects != null)
            {
                foreach (ContractHandler contract in leaveProjects)
                {
                    XmlElement udtElement = pref.GetElement("Property[@Name='UDS']");
                    XmlElement tableElement = pref.GetElement("Property[@Name='UDS']/Contract[@Name='" + contract.Name + "']");

                    if (tableElement != null)
                    {
                        udtElement.RemoveChild(tableElement);
                    }
                    this.Contracts.Remove(contract);
                }
            }

            Parent.UpdateProjectPreference(pref.GetElement("."));

            if (ProjectContractChanged != null)
                ProjectContractChanged.Invoke(this, EventArgs.Empty);
        }

        internal void ImportContract(XmlElement xmlElement)
        {
            string tableName = xmlElement.GetAttribute("Name");
            ContractHandler contract = this.GetProjectContract(tableName);

            XmlHelper h = new XmlHelper(xmlElement);
            Parent.SendRequest("UDSManagerService.ImportContract", new Envelope(new XmlHelper(xmlElement)));

            if (contract != null) return;

            this.JoinProject(ContractHandler.CreateNew(tableName, ExtendType.none));
        }

        private ContractHandler GetProjectContract(string contractName)
        {
            foreach (ContractHandler ch in this.Contracts)
            {
                if (ch.Name == contractName)
                    return ch;
            }
            return null;
        }

        internal void ImportContracts(XmlElement xmlElement, bool import)
        {
            XmlHelper h = new XmlHelper(xmlElement);
            List<ContractHandler> jp = new List<ContractHandler>();
            List<ContractHandler> lp = new List<ContractHandler>();

            foreach (XmlElement contractElement in h.GetElements("Contract"))
            {
                string contractName = contractElement.GetAttribute("Name");
                ContractHandler contract = this.GetProjectContract(contractName);

                if (contract == null)
                {
                    jp.Add(ContractHandler.CreateNew(contractName, ExtendType.none));
                }
            }

            foreach (ContractHandler contract in this.Contracts)
            {
                bool contains = false;
                foreach (XmlElement contractElement in h.GetElements("Contract"))
                {
                    string contractName = contractElement.GetAttribute("Name");
                    if (contract.Name.ToLower() == contractName.ToLower())
                    {
                        contains = true;
                        break;
                    }
                }

                if (!contains && import)
                    lp.Add(contract);
            }

            Parent.SendRequest("UDSManagerService.ImportContracts", new Envelope(h));

            this.ModifyProjects(jp, lp);

            XmlElement pref = MainForm.CurrentProject.Preference;
            XmlHelper ph = new XmlHelper(pref);
            foreach (XmlElement e in h.GetElements("Property"))
            {
                string attr = e.GetAttribute("Name");
                XmlElement target = null;
                foreach (XmlElement p in ph.GetElements("Property"))
                {
                    string name = p.GetAttribute("Name");
                    if (attr != name) continue;

                    target = p;
                }
                if (target != null)
                    pref.RemoveChild(target);

                e.SetAttribute("Source", "Import");
                ph.AddElement(".", e);
            }
            //Console.WriteLine(ph.XmlString);
            MainForm.CurrentProject.UpdateProjectPreference(pref);
        }
    }
}
