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
            this.Node.ToolTipText = unsigned ? "這個 Service 沒有簽章，DSA 會拒絕執行；確認內容後按右鍵「簽章 Service」" : string.Empty;
        }

        /// <summary>這個 Service 目前被判定為沒有簽章</summary>
        internal bool IsUnsigned
        {
            get { return this.PackageNodeHandler.Package.IsSigned(this.ServiceName) == false; }
        }

        /// <summary>
        /// 不改內容，直接把開發站台上目前的定義重新簽章後存回去。
        /// 簽章代表確認這份內容可以執行，所以一次只簽一個，並先讓使用者確認。
        /// </summary>
        /// <returns>簽章成功回傳 true；取消或失敗回傳 false</returns>
        internal bool SignCurrentDefinition()
        {
            DialogResult dr = MessageBox.Show(
                (this.IsUnsigned ? "" : "這個 Service 已經有簽章，重新簽章會以新的簽章取代。\n\n")
                + "將以開發站台上目前的內容為 Service「" + this.ServiceName + "」簽章。\n\n"
                + "簽章代表確認這份內容可以執行，建議先打開檢查過內容再簽。\n"
                + "\n確定要簽章？",
                "簽章 Service", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dr != DialogResult.OK) return false;

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                ServiceDAL.Sign(this.ContractName, this.PackageName, this.ServiceName);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("簽章失敗：\n" + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        /// <summary>丟掉目前的編輯器，下次選到時重新從站台讀取定義</summary>
        internal void ResetEditors()
        {
            this.IsFirstClick = true;
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
