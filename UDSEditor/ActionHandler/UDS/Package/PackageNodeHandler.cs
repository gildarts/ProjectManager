using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProjectManager.ActionHandler.UDS.Service;
using ProjectManager.Project.UDS.Package;
using ProjectManager.Project.UDS.Service;
using ProjectManager.ActionHandler.UDS.Contract;

namespace ProjectManager.ActionHandler.UDS.Package
{
    class PackageNodeHandler : INodeHandler, IAddable, IRenameable, IReloadable, IDeleteable
    {
        internal PackageHandler Package { get; private set; }

        internal PackageNodeHandler(TreeNode node, PackageHandler package)
        {
            this.Package = package;
            this.Node = node;

            this.Reload();
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
            Node.BeginEdit();
        }

        void TreeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            Node.TreeView.AfterLabelEdit -= new NodeLabelEditEventHandler(TreeView_AfterLabelEdit);
            Node.TreeView.LabelEdit = false;

            try
            {
                this.Package.Rename(e.Label);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "重新命名失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.CancelEdit = true;
            }
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
