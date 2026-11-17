using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProjectManager.ActionHandler.UDS.Service;
using ProjectManager.Project.UDS.Package;
using ProjectManager.Project.UDS.Service;
using ProjectManager.ActionHandler.UDS.Contract;
using ProjectManager.ActionHandler.UDS.Service.DAL;

namespace ProjectManager.ActionHandler.UDS.Package
{
    class PackageNodeHandler : INodeHandler, IAddable, IRenameable, IReloadable, IDeleteable
    {
        internal PackageHandler Package { get; private set; }

        internal PackageNodeHandler(TreeNode node, PackageHandler package)
        {
            this.Package = package;
            this.Node = node;

            ServiceDAL.ServiceSigned += ServiceDAL_ServiceSigned;
            this.Reload();
        }

        /// <summary>Service 存檔簽章後，更新樹上的「未簽章」標示</summary>
        void ServiceDAL_ServiceSigned(string contractName, string packageName, string serviceName)
        {
            // 節點已經不在樹上（重新整理、換專案）就不再接收
            if (this.Node.TreeView == null)
            {
                ServiceDAL.ServiceSigned -= ServiceDAL_ServiceSigned;
                return;
            }
            if (contractName != this.Package.Contract.Name || packageName != this.Package.Name) return;

            this.Package.SetSigned(serviceName, true);
            foreach (TreeNode node in this.Node.Nodes)
            {
                ServiceNodeHandler sh = node.Tag as ServiceNodeHandler;
                if (sh != null && sh.ServiceName == serviceName)
                    sh.RefreshSignDisplay();
            }
            RefreshSignDisplay();
        }

        /// <summary>Package 底下有沒簽章的 Service 就標出數量，並更新上層 Contract</summary>
        internal void RefreshSignDisplay()
        {
            int count = this.Package.UnsignedCount;
            this.Node.Text = count > 0 ? this.Package.Name + "（" + count + " 個未簽章）" : this.Package.Name;
            this.Node.ForeColor = count > 0 ? System.Drawing.Color.Firebrick : System.Drawing.Color.Empty;

            ContractNodeHandler contractNode = this.Node.Parent == null ? null : this.Node.Parent.Tag as ContractNodeHandler;
            if (contractNode != null)
                contractNode.RefreshDisplay();
        }

        #region INodeHandler 成員

        public System.Windows.Forms.TreeNode Node { get; private set; }

        public bool IsFirstClick { get; private set; }

        public void OnFirstClick() { }

        public void OnClick() { }
        #endregion

        #region IAddable 成員

        public void Add()
        {
            AddServiceForm serviceForm = new AddServiceForm(this.Package);
            serviceForm.StartPosition = FormStartPosition.CenterParent;
            serviceForm.Completed += delegate(object sender, ServiceEventArg e)
            {
                ServiceEntity sh = ServiceEntity.CreateNew(e.ServiceName, e.TargetTable, e.Action);
                try
                {
                    this.Package.AddService(sh);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("新增 Service 失敗 : \n" + ex.Message, "失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                TreeNode node = this.Node.Nodes.Add(e.ServiceName);
                node.SelectedImageKey = "service";
                node.ImageKey = "service";
                node.Tag = new ServiceNodeHandler(node, e.ServiceName, this);
                this.Node.TreeView.SelectedNode = node;
            };
            serviceForm.ShowDialog();
        }

        public string TitleOfAdd { get { return "新增 Service(&A)"; } }

        #endregion

        #region IRenameable 成員

        public void Rename()
        {
            Node.TreeView.AfterLabelEdit += new NodeLabelEditEventHandler(TreeView_AfterLabelEdit);
            Node.TreeView.LabelEdit = true;
            // 編輯時只顯示純名稱，避免「（n 個未簽章）」被當成名稱的一部分
            Node.Text = this.Package.Name;
            Node.BeginEdit();
        }

        void TreeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            Node.TreeView.AfterLabelEdit -= new NodeLabelEditEventHandler(TreeView_AfterLabelEdit);
            Node.TreeView.LabelEdit = false;

            // 節點文字一律自己設定，不讓 TreeView 套用編輯框的內容
            e.CancelEdit = true;
            try
            {
                if (!string.IsNullOrWhiteSpace(e.Label) && e.Label != this.Package.Name)
                    this.Package.Rename(e.Label);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "重新命名失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            RefreshSignDisplay();
        }

        #endregion

        #region IReloadable 成員

        public void Reload()
        {
            this.Node.Nodes.Clear();

            foreach (string serviceName in this.Package.Services)
            {
                TreeNode node = this.Node.Nodes.Add(serviceName);
                node.SelectedImageKey = "service";
                node.ImageKey = "service";
                node.Tag = new ServiceNodeHandler(node, serviceName, this);
            }

            this.Node.ExpandAll();
            RefreshSignDisplay();
        }

        #endregion

        #region IDeleteable 成員

        public void Delete()
        {
            ContractNodeHandler contractNode = this.Node.Parent.Tag as ContractNodeHandler;
            contractNode.DeletePackage(this);
        }

        public string TitleOfDelete { get { return "刪除 Package「 " + this.Package.Name + "」(&D)"; } }
        #endregion

        internal void DeleteService(ServiceNodeHandler serviceNodeHandler)
        {
            this.Package.DeleteService(serviceNodeHandler.ServiceName);
            this.Node.Nodes.Remove(serviceNodeHandler.Node);
        }

        internal void ReloadService(string serviceName)
        {
            this.Reload();

            foreach (TreeNode node in this.Node.Nodes)
            {
                ServiceNodeHandler sh = node.Tag as ServiceNodeHandler;
                if (sh.ServiceName != serviceName) continue;

                this.Node.TreeView.SelectedNode = node;
            }
        }
    }
}
