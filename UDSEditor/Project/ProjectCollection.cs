using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.DSAClient;
using System.Xml;
using ProjectManager.Login;
using ProjectManager.Project.UDS;
using ProjectManager.Project.UDT;

namespace ProjectManager.Project
{
    class ProjectCollection
    {
        internal const string LOCAL_FILE_PATH = "LocalFilePath";

        internal event EventHandler ProjectRemoved;
        internal event EventHandler<ProjectEventArgs> ProjectAdded;

        private const string PROJECT_LIST_PS_NAME = "ischool.project.list";

        private List<string> _projects;

        internal bool Contains(string projectName)
        {
            if (_projects == null) return false;
            return _projects.Contains(projectName);
        }

        internal List<string> ListProjects()
        {
            if (_projects != null)
                return _projects;

            XmlHelper req = new XmlHelper("<Request/>");
            req.AddElement(".", "Condition");
            req.AddElement("Condition", "Name", PROJECT_LIST_PS_NAME);
            Envelope evn = MainForm.LoginArgs.GreeningConnection.SendRequest("GetMySpace", new Envelope(req));
            XmlHelper rsp = new XmlHelper(evn.Body);

            _projects = new List<string>();

            if (rsp.GetElement("Space") == null)
            {
                req = new XmlHelper("<Request/>");
                req.AddElement(".", "Space");
                req.AddElement("Space", "Name", PROJECT_LIST_PS_NAME);
                XmlElement content = req.AddElement("Space", "Content");
                XmlCDataSection section = content.OwnerDocument.CreateCDataSection("<ProjectList/>");
                content.AppendChild(section);
                MainForm.LoginArgs.GreeningConnection.SendRequest("CreateSpace", new Envelope(req));
            }
            else
            {
                string content = rsp.GetText("Space/Content");
                XmlHelper h = new XmlHelper(content);

                foreach (XmlElement projectElement in h.GetElements("Project"))
                    _projects.Add(projectElement.GetAttribute("Name"));
            }
            return _projects;
        }

        internal void SetProjectList(string[] projectList)
        {
            XmlHelper req = new XmlHelper("<Request/>");
            req.AddElement(".", "Space");
            req.AddElement("Space", "Name", PROJECT_LIST_PS_NAME);
            XmlElement content = req.AddElement("Space", "Content");

            XmlHelper list = new XmlHelper("<ProjectList/>");

            foreach (string projectName in projectList)
            {
                XmlElement p = list.AddElement(".", "Project");
                p.SetAttribute("Name", projectName);
            }
            XmlCDataSection section = content.OwnerDocument.CreateCDataSection(list.GetElement(".").OuterXml);

            content.AppendChild(section);
            MainForm.LoginArgs.GreeningConnection.SendRequest("UpdateSpace", new Envelope(req));

            _projects = new List<string>(projectList);
        }

        internal void RemoveProject(string projectName)
        {
            if (!this.Contains(projectName)) return;

            XmlHelper h = new XmlHelper();
            h.AddElement(".", "Space");
            h.AddElement("Space", "Name", ProjectHandler.PROJECT_PS_PREFIX + projectName);
            MainForm.LoginArgs.GreeningConnection.SendRequest("DeleteSpace", new Envelope(h));

            _projects.Remove(projectName);
            this.SetProjectList(_projects.ToArray());

            if (ProjectRemoved != null)
                ProjectRemoved.Invoke(this, EventArgs.Empty);
        }
        
        internal void AddProject(string projectName, Login.DevSiteLoginInfo info, string directory)
        {
            InternalAddProject(projectName, info, directory);

            if (ProjectAdded != null)
                ProjectAdded.Invoke(this, new ProjectEventArgs(projectName, info));
        }

        private void InternalAddProject(string p, Login.DevSiteLoginInfo info, string directory)
        {
            _projects.Add(p);

            XmlHelper h = new XmlHelper("<Preference/>");
            //h.AddElement(".", "DevSite", info.XmlString, true);
            XmlElement ds = h.AddElement(".", "Property", info.XmlString, true);
            ds.SetAttribute("Name", "DevSite");

            XmlElement udt = h.AddElement(".", "Property");
            udt.SetAttribute("Name", "UDT");

            XmlElement uds = h.AddElement(".", "Property");
            uds.SetAttribute("Name", "UDS");

            XmlElement localDirectry = h.AddElement(".", "Property");
            uds.SetAttribute("Name", "UDS");

            string spaceName = ProjectHandler.PROJECT_PS_PREFIX + p;
            XmlHelper req = new XmlHelper("<Request/>");
            req.AddElement(".", "Space");
            req.AddElement("Space", "Name", spaceName);
            XmlElement content = req.AddElement("Space", "Content");
            XmlCDataSection section = content.OwnerDocument.CreateCDataSection(h.XmlString);
            content.AppendChild(section);

            MainForm.LoginArgs.GreeningConnection.SendRequest("CreateSpace", new Envelope(req));

            MainForm.Storage.SetPropertyValues(LOCAL_FILE_PATH, p, directory);

            this.SetProjectList(_projects.ToArray());
        }

        internal ProjectHandler CreateProjectHandler(string projectName, DevSiteLoginInfo siteInfo)
        {
            foreach (string p in _projects)
            {
                if (p == projectName)
                    return new ProjectHandler(projectName, siteInfo);
            }
            return null;
        }

        internal ProjectHandler LoadProjectHandler(string projectName)
        {
            ProjectHandler ph = LoadUnloadProjectHandler(projectName);
            if (ph.Status == ProjectStatus.Unload)
                ph.Init(true);
            return ph;
        }

        internal ProjectHandler LoadUnloadProjectHandler(string projectName)
        {
            foreach (string p in _projects)
            {
                if (p == projectName)
                {
                    return new ProjectHandler(projectName);                    
                }
            }
            return ProjectHandler.CreateFailProjectHandler(projectName);
        }

        internal void LoadProject(string projectName, DevSiteLoginInfo devSite, bool checkDup, XmlElement source)
        {
            if (this._projects.Contains(projectName))
                throw new Exception("專案已經存在");

            XmlHelper sourceHelper = new XmlHelper(source);
            if(checkDup)
            {
                Connection devConnection = devSite.TryConnect();
                Envelope evn = devConnection.SendRequest("UDSManagerService.ListContracts", new Envelope());
                XmlHelper rsp = new XmlHelper(evn.Body);
                StringBuilder contracts = new StringBuilder();
                foreach (XmlElement contractElement in sourceHelper.GetElements("Property/Contract"))
                {
                    string contractName = contractElement.GetAttribute("Name");
                    if (rsp.GetElement("Contract[@Name='" + contractName + "']") != null)
                        contracts.Append(contractName).Append("\n");
                }

                evn = devConnection.SendRequest("UDTService.DDL.ListTables", new Envelope());
                rsp = new XmlHelper(evn.Body);
                StringBuilder tables = new StringBuilder();
                foreach (XmlElement contractElement in rsp.GetElements("Property/Table"))
                {
                    string tableName = contractElement.GetAttribute("Name");
                    if (rsp.GetElement("Table[@Name='" + tableName + "']") != null)
                        tables.Append(tableName).Append("\n");
                }
                StringBuilder msg = new StringBuilder();
                if (tables.Length > 0)                
                    msg.Append("檢查到重覆資料表 : \n").Append(tables);
                
                if(contracts.Length > 0)
                    msg.Append("檢查到重覆 Contract : \n").Append(contracts);

                if (msg.Length > 0)
                    throw new Exception(msg.ToString());
            }

            string filepath = MainForm.Storage.GetPropertyValue(LOCAL_FILE_PATH, projectName);
            this.InternalAddProject(projectName, devSite, filepath);

            ProjectHandler ph = this.CreateProjectHandler(projectName, devSite);
            
            UDSHandler uds = ph.GetUDSHandler();            
            XmlElement ce = sourceHelper.GetElement("Property[@Name='UDS']");
            if (ce != null)
                uds.ImportContracts(ce, true);

            UDTHandler udt = ph.GetUDT();
            XmlElement te = sourceHelper.GetElement("Property[@Name='UDT']");
            if (te != null)
                udt.SetTables(te, true);

            if(ProjectAdded != null)
                ProjectAdded.Invoke(this, new ProjectEventArgs(projectName,devSite));
        }
    }

    class ProjectEventArgs : EventArgs
    {
        internal string ProjectName { get; private set; }
        internal DevSiteLoginInfo DevSite { get; private set; }

        internal ProjectEventArgs(string projectName, DevSiteLoginInfo devSite)
        {
            ProjectName = projectName;
            DevSite = devSite;
        }
    }
}
