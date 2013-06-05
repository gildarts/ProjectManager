using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using ProjectManager.ActionHandler.UDS.Contract;
using FISCA.DSAClient;
using ProjectManager.Project.UDS.Package;
using ProjectManager.ActionHandler.UDS.Package;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Forms;

namespace ProjectManager.Project.UDS.Contract
{
    class ContractHandler
    {
        internal event EventHandler Renamed;
        internal event EventHandler Reloaded;

        internal bool Ready { get; private set; }
        public string Name { get; private set; }
        internal bool Enabled { get; set; }
        internal ExtendType ExtendType { get; private set; }
        internal XmlElement Definition { get; private set; }
        internal event EventHandler Saved;

        internal List<PackageHandler> _packages;
        internal List<PackageHandler> Packages
        {
            get
            {
                return _packages;
            }
        }

        private ContractHandler(string name, ExtendType extend)
        {
            Name = name;
            ExtendType = extend;

            //List<string> files = new List<string>();
            //Parallel.ForEach(files, file =>
            //{
                
            //});
            //Parallel.For(0, 10, i =>
            //{
                
            //});
            //
        }

        internal ContractHandler(XmlElement contractElement)
        {
            Name = contractElement.GetAttribute("Name");

            XmlHelper h = new XmlHelper(contractElement);
            Enabled = h.TryGetBoolean("@Enabled", true);
            this.InitDefinition(h.GetElement("Definition"));

            init();
        }

        internal PackageHandler AddPackage(string packageName)
        {
            XmlHelper req = new XmlHelper("<Request/>");
            req.AddElement(".", "ContractName", this.Name);
            req.AddElement(".", "PackageName", packageName);
            MainForm.CurrentProject.SendRequest("UDSManagerService.CreatePackage", new Envelope(req));

            PackageHandler ph = PackageHandler.CreateNew(this, packageName);
            _packages.Add(ph);
            return ph;
        }

        internal static ContractHandler CreateNew(string name, ExtendType extend)
        {
            return new ContractHandler(name, extend);
        }

        internal void Rename(string newName)
        {
            XmlHelper req = new XmlHelper("<Request/>");
            req.AddElement(".", "ContractName", this.Name);
            req.AddElement(".", "NewContractName", newName);
            MainForm.CurrentProject.SendRequest("UDSManagerService.RenameContract", new Envelope(req));

            XmlHelper pref = new XmlHelper(MainForm.CurrentProject.Preference);
            XmlElement e = pref.GetElement("Property[@Name='UDS']/Contract[@Name='" + this.Name + "']");
            e.SetAttribute("Name", newName);
            MainForm.CurrentProject.UpdateProjectPreference(pref.GetElement("."));

            this.Name = newName;

            if (Renamed != null)
                Renamed.Invoke(this, EventArgs.Empty);
        }

        internal void SetDefinition(XmlElement xmlElement, object saver)
        {
            XmlHelper req = new XmlHelper("<Request/>");
            req.AddElement(".", "ContractName", this.Name);
            req.AddElement(".", xmlElement);

            MainForm.CurrentProject.SendRequest("UDSManagerService.SetContractDefinition", new Envelope(req));
            this.InitDefinition(xmlElement);

            if (Saved != null)
                Saved.Invoke(saver, EventArgs.Empty);
        }

        private void InitDefinition(XmlElement definition)
        {
            this.Definition = definition;
            XmlHelper h = new XmlHelper(definition);

            string extType = h.GetText("Authentication/@Extends").ToLower();
            if (extType == ExtendType.admin.ToString())
                ExtendType = ExtendType.admin;
            else if (extType == ExtendType.sa.ToString())
                ExtendType = ExtendType.sa;
            else if (extType == ExtendType.ta.ToString())
                ExtendType = ExtendType.ta;
            else if (h.TryGetBoolean("Authentication/Public/@Enabled", false))
                ExtendType = ExtendType.open;
            else
                ExtendType = ExtendType.none;
        }

        internal void Reload()
        {
            init();           
        }

        private void init()
        {
            Ready = false;
            BackgroundWorker w = new BackgroundWorker();
            w.DoWork += new DoWorkEventHandler(w_DoWork);
            w.RunWorkerCompleted += new RunWorkerCompletedEventHandler(w_RunWorkerCompleted);
            w.RunWorkerAsync(this.Name);
            //_packages = new List<PackageHandler>();

            //XmlHelper req = new XmlHelper();
            //req.AddElement(".", "ContractName", this.Name);

            //Envelope env = MainForm.CurrentProject.DevConnection.SendRequest("UDSManagerService.ExportContract", new Envelope(req));
            //XmlHelper rsp = new XmlHelper(env.Body);
            //this.Definition = rsp.GetElement("Definition");
            //foreach (XmlElement each in rsp.GetElements("Package"))
            //{
            //    PackageHandler ph = new PackageHandler(this, each);
            //    _packages.Add(ph);
            //}
            //if (Reloaded != null)
            //    Reloaded.Invoke(this, EventArgs.Empty);
        }

        void w_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (MainForm.IsClosing) return;

            //Application.DoEvents();

            XmlHelper rsp = e.Result as XmlHelper;

            if (rsp == null) return;

            _packages = new List<PackageHandler>();
            this.Definition = rsp.GetElement("Definition");

            foreach (XmlElement each in rsp.GetElements("Package"))
            {
                PackageHandler ph = new PackageHandler(this, each);
                _packages.Add(ph);
            }

            Ready = true;
            if (Reloaded != null)
                Reloaded.Invoke(this, EventArgs.Empty);
        }

        void w_DoWork(object sender, DoWorkEventArgs e)
        {
            String name = e.Argument.ToString();
            XmlHelper req = new XmlHelper();
            req.AddElement(".", "ContractName", name);

            Envelope request = new Envelope(req);
            try
            {
                Envelope env = MainForm.CurrentProject.SendRequest("UDSManagerService.ExportContract", request);
                XmlHelper rsp = new XmlHelper(env.Body);
                e.Result = rsp;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }          
        }

        internal void DeletePackage(string packageName)
        {
            XmlHelper req = new XmlHelper();
            req.AddElement(".", "ContractName", this.Name);
            req.AddElement(".", "PackageName", packageName);

            Envelope env = MainForm.CurrentProject.SendRequest("UDSManagerService.DeletePackage", new Envelope(req));

            PackageHandler p = null;
            foreach (PackageHandler ph in _packages)
            {
                if (ph.Name == packageName)
                    p = ph;
            }
            if (p != null)
                _packages.Remove(p);
        }

        internal XmlElement GetXml()
        {
            XmlHelper h = new XmlHelper("<Contract/>");
            h.SetAttribute(".", "Name", this.Name);
            h.SetAttribute(".", "Enabled", this.Enabled.ToString());

            h.AddElement(".", this.Definition);

            foreach (PackageHandler p in this.Packages)
            {
                h.AddElement(".", p.GetXml());
            }
            string content = XmlHelper.Format(h.XmlString);
            return XmlHelper.ParseAsDOM(content);            
        }
    }
}
