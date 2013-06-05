using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProjectManager.Project.UDS;
using ProjectManager.Project.UDS.Contract;
using ProjectManager.ActionHandler.UDS.Package;
using ProjectManager.Project.UDS.Package;
using FISCA.DSAClient;
using System.Xml;
using System.ComponentModel;

namespace ProjectManager.ActionHandler.UDS.Contract
{
    class ContractNodeHandler : INodeHandler, IAddable, IReloadable, IRenameable, IDeleteable, IExportable, IEditorManager, ITestable
    {
        public ContractHandler Contract { get; private set; }
        public string Name { get { return Contract.Name; } }

        public ContractNodeHandler(TreeNode node, ContractHandler contract)
        {
            this.IsFirstClick = true;

            Node = node;
            Contract = contract;
        }

        #region INodeHandler 成員

        public System.Windows.Forms.TreeNode Node { get; private set; }

        public bool IsFirstClick { get; private set; }

        public void OnFirstClick()
        {
            this.Editors = new List<IEditable>();
            ContractUIEditable editor = new ContractUIEditable("Contract-" + Contract.Name, this);
            ContractXmlEditable xeditor = new ContractXmlEditable("Contract-" + Contract.Name, this);

            this.Editors.Add(editor);
            this.Editors.Add(xeditor);
            this.CurrentEditor = editor;
            this.Contract.Reloaded += new EventHandler(Contract_Reloaded);
            this.Reload();

            this.IsFirstClick = false;
        }

        public void OnClick() { }

        #endregion

        #region IAddable 成員

        public void Add()
        {
            AddPackageForm addPackForm = new AddPackageForm();
            addPackForm.StartPosition = FormStartPosition.CenterParent;
            addPackForm.Completed += delegate
            {
                string packageName = addPackForm.PackageName;
                PackageHandler ph = this.Contract.AddPackage(packageName);
                TreeNode node = this.Node.Nodes.Add(addPackForm.PackageName);

                node.SelectedImageKey = "package";
                node.ImageKey = "package";
                node.Tag = new PackageNodeHandler(node, ph);
                node.ExpandAll();
            };
            addPackForm.ShowDialog();
        }

        public string TitleOfAdd { get { return "新增 Package(&A)"; } }

        #endregion

        internal bool Ready { get; private set; }

        #region IReloadable 成員

        public void Reload()
        {
            Ready = false;
            this.Node.Text = this.Contract.Name + "(資料準備中...)";
            this.Contract.Reload();
        }

        void Contract_Reloaded(object sender, EventArgs e)
        {
            if (MainForm.IsClosing) return;

            this.Node.Nodes.Clear();
            List<PackageHandler> list = this.Contract.Packages;

            foreach (PackageHandler ph in list)
            {
                TreeNode node = this.Node.Nodes.Add(ph.Name);
                node.ImageKey = "package";
                node.SelectedImageKey = "package";
                node.Tag = new PackageNodeHandler(node, ph);
            }

            this.Node.ExpandAll();
            this.Node.Text = this.Contract.Name;
            Ready = true;
        }

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
                if (!string.IsNullOrWhiteSpace(e.Label))
                    this.Contract.Rename(e.Label);
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
            UDSNodeHandler udsNodeHandler = this.Node.Parent.Tag as UDSNodeHandler;
            if (this.Node.Parent.Text.StartsWith("專案外"))
            {
                MessageBox.Show("不可刪除專案外之 Contract", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            udsNodeHandler.DeleteContract(this);
        }

        public string TitleOfDelete { get { return "刪除 Contract 「" + this.Contract.Name + "」(&D)"; } }
        #endregion

        internal void DeletePackage(PackageNodeHandler packageNodeHandler)
        {
            this.Contract.DeletePackage(packageNodeHandler.Package.Name);
            this.Node.Nodes.Remove(packageNodeHandler.Node);
        }

        #region IExportable 成員
        private const string FILE_EXT = "cml";
        public void Export()
        {
            if (!this.Contract.Ready)
            {
                MessageBox.Show("Contract 尚未準備完成, 請稍侯再試!", "資訊", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            XmlElement e = Contract.GetXml();

            SaveFileDialog sd = new SaveFileDialog();
            sd.FileName = this.Contract.Name + "." + FILE_EXT;
            sd.DefaultExt = FILE_EXT;
            sd.Filter = "*." + FILE_EXT + "|*." + FILE_EXT;
            DialogResult dr = sd.ShowDialog();

            if (dr != DialogResult.OK) return;

            e.OwnerDocument.Save(sd.FileName);
        }

        #endregion

        #region IEditorManager 成員

        private IEditable _currentEditor;

        public List<IEditable> Editors { get; private set; }

        public IEditable CurrentEditor
        {
            get
            {
                if(this.Ready)
                    return _currentEditor;
                return null;
            }
            set
            {
                _currentEditor = value;
            }
        }

        #endregion

        #region ITestable 成員

        public string TitleOfTest
        {
            get { return "測試 Contract 設定"; }
        }

        public string TestImageKey
        {
            get { return "exec"; }
        }

        public void Test()
        {
            ContractTestForm testForm = new ContractTestForm(this.Contract);
            testForm.StartPosition = FormStartPosition.CenterParent;
            testForm.ShowDialog();
        }

        #endregion
    }
}
