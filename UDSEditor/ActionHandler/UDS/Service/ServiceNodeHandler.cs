using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProjectManager.Project.UDS.Service;
using FISCA.DSAClient;
using ProjectManager.ActionHandler.UDS.Package;
using System.Xml;
using ProjectManager.ActionHandler.UDS.Service.Test;
using ProjectManager.ActionHandler.UDS.Service.DAL;
using ProjectManager.ActionHandler.UDS.Service.Editable;

namespace ProjectManager.ActionHandler.UDS.Service
{
    class ServiceNodeHandler : INodeHandler, IRenameable, IEditorManager, IDeleteable, IExportable, IImportable, ITestable
    {
        //internal IServiceHandler Service { get; private set; }
        internal string ServiceName { get; private set; }
        internal PackageNodeHandler PackageNodeHandler { get; private set; }
        internal string PackageName { get; private set; }
        internal string ContractName { get; private set; }

        internal ServiceNodeHandler(TreeNode node, string serviceName, PackageNodeHandler parent)
        {
            this.IsFirstClick = true;
            this.Node = node;
            this.ServiceName = serviceName;

            this.PackageNodeHandler = parent;
            this.PackageName = parent.Package.Name;
            this.ContractName = parent.Package.Contract.Name;
            parent.Package.Contract.Renamed += new EventHandler(Contract_Renamed);

            //Service = service;
            //Service.Reloaded += new EventHandler(Service_Reloaded);                       
        }

        void Contract_Renamed(object sender, EventArgs e)
        {
            this.ContractName = PackageNodeHandler.Package.Contract.Name;
        }

        #region INodeHandler 成員

        public TreeNode Node { get; private set; }

        public bool IsFirstClick { get; private set; }

        public void OnFirstClick()
        {
            XmlElement sd = ServiceDAL.GetServiceDefinition(this.ContractName, this.PackageName, this.ServiceName);
            bool isDBHelperService = sd.GetAttribute("Type").ToLower() == "dbhelper";

            this.Editors = new List<IEditable>();
            IEditable editable = null;
            if (isDBHelperService)
            {
                editable = new ServiceUIEditable("Service - " + this.ServiceName, this);
                this.Editors.Add(editable);
            }
            IEditable xmlEditable = new ServiceXmlEditable("Service - " + this.ServiceName, this);
            this.Editors.Add(xmlEditable);

            if (editable == null)
                this.CurrentEditor = xmlEditable;
            else
                this.CurrentEditor = editable;

            this.IsFirstClick = false;
        }

        public void OnClick() { }

        #endregion

        #region IRenameable 成員

        public void Rename()
        {
            Node.TreeView.AfterLabelEdit += new NodeLabelEditEventHandler(TreeView_AfterLabelEdit);
            Node.TreeView.LabelEdit = true;
            Node.BeginEdit();
        }

        void TreeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            Node.TreeView.AfterLabelEdit -= new NodeLabelEditEventHandler(TreeView_AfterLabelEdit);
            Node.TreeView.LabelEdit = false;

            try
            {
                ServiceDAL.Rename(this.ContractName, this.PackageName, this.ServiceName, e.Label);
                this.ServiceName = e.Label;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "重新命名失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.CancelEdit = true;
            }
        }

        #endregion

        #region IDeleteable 成員

        public void Delete()
        {
            this.PackageNodeHandler.DeleteService(this);
        }

        public string TitleOfDelete { get { return "刪除 Service 「" + this.ServiceName + "」(&D)"; } }
        #endregion

        #region IExportable 成員

        public void Export()
        {
            XmlElement e = ServiceDAL.GetRawService(this.ContractName, this.PackageName, this.ServiceName);

            SaveFileDialog sd = new SaveFileDialog();
            sd.DefaultExt = "xml";
            sd.FileName = this.ServiceName + ".xml";
            sd.Filter = "(*.xml)|*.xml";

            DialogResult dr = sd.ShowDialog();

            if (dr != DialogResult.OK) return;

            string formated = XmlHelper.Format(e.OwnerDocument.OuterXml);
            XmlHelper h = new XmlHelper(formated);
            h.WriteTo(sd.FileName);
        }

        #endregion

        #region IImportable 成員

        public void Import()
        {
            OpenFileDialog fd = new OpenFileDialog();
            DialogResult dr = fd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(fd.FileName);

                try
                {
                    ServiceDAL.SetDefinition(this.ContractName, this.PackageName, this.ServiceName, xml.DocumentElement);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("匯入失敗 : \n" + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion

        #region IEditorManager 成員

        public List<IEditable> Editors { get; private set; }


        public IEditable CurrentEditor { get; set; }

        #endregion

        #region ITestable 成員

        public string TitleOfTest
        {
            get { return "測試 Service『" + this.ServiceName + "』(&T)"; }
        }

        public string TestImageKey
        {
            get { return "exec"; }
        }

        public void Test()
        {
            XmlElement definition = ServiceDAL.GetServiceDefinition(this.ContractName, this.PackageName, this.ServiceName);
            TestServiceForm testForm = new TestServiceForm(this.ContractName, this.PackageName, this.ServiceName, definition);
            testForm.StartPosition = FormStartPosition.CenterParent;
            testForm.ShowDialog();
        }

        #endregion

        //#region IReloadable 成員

        //public void Reload()
        //{
        //    this.Service.Reload();
        //}

        //void Service_Reloaded(object sender, EventArgs e)
        //{
        //    this.Editors = new List<IEditable>();
        //    IEditable editable = new ServiceUIEditable("Service - " + this.Service.Name, this);
        //    this.Editors.Add(editable);

        //    IEditable xmlEditable = new ServiceXmlEditable("Service - " + this.Service.Name, this);
        //    this.Editors.Add(xmlEditable);

        //    if (this.CurrentEditor.GetType() == editable.GetType())
        //        CurrentEditor = editable;
        //    else
        //        CurrentEditor = xmlEditable;
        //}

        //#endregion
    }
}
