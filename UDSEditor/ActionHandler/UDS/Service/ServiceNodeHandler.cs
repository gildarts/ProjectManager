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

            RefreshSignDisplay();

            //Service = service;
            //Service.Reloaded += new EventHandler(Service_Reloaded);                       
        }

        /// <summary>沒簽章的 Service 用紅字並在名稱後面加「（未簽章）」</summary>
        internal void RefreshSignDisplay()
        {
            bool unsigned = this.PackageNodeHandler.Package.IsSigned(this.ServiceName) == false;
            this.Node.Text = unsigned ? this.ServiceName + "（未簽章）" : this.ServiceName;
            this.Node.ForeColor = unsigned ? System.Drawing.Color.Firebrick : System.Drawing.Color.Empty;
            this.Node.ToolTipText = unsigned ? "這個 Service 沒有簽章，DSA 會拒絕執行；重新儲存即可自動簽章" : string.Empty;
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
            bool isJavascriptService = sd.GetAttribute("Type").ToLower() == "javascript";

            this.Editors = new List<IEditable>();
            IEditable editable = null;
            if (isDBHelperService)
            {
                editable = new ServiceUIEditable("Service - " + this.ServiceName, this);
                this.Editors.Add(editable);
            }
            if (isJavascriptService)
            {
                editable = new ServiceJSEditable(ServiceName, this);
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
            // 編輯時只顯示純名稱，避免「（未簽章）」被當成名稱的一部分
            Node.Text = this.ServiceName;
            Node.BeginEdit();
        }

        void TreeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            Node.TreeView.AfterLabelEdit -= new NodeLabelEditEventHandler(TreeView_AfterLabelEdit);
            Node.TreeView.LabelEdit = false;

            // 沒輸入或名稱沒變就不用改名（也不必重簽）
            // 節點文字一律自己設定（含「（未簽章）」），不讓 TreeView 套用編輯框的內容
            e.CancelEdit = true;

            if (string.IsNullOrWhiteSpace(e.Label) || e.Label == this.ServiceName)
            {
                RefreshSignDisplay();
                return;
            }

            try
            {
                ServiceDAL.Rename(this.ContractName, this.PackageName, this.ServiceName, e.Label);
                this.ServiceName = e.Label;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "重新命名失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            RefreshSignDisplay();
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
