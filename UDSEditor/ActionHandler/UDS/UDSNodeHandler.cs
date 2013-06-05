using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProjectManager.Project.UDS;
using ProjectManager.Project.UDS.Contract;
using ProjectManager.ActionHandler.UDS.Contract;
using System.Xml;
using FISCA.DSAClient;

namespace ProjectManager.ActionHandler.UDS
{
    internal class UDSNodeHandler : INodeHandler, IReloadable, IAddable, IEditorManager, IImportable, IExportable, IDeployToPhysical, IImportFromPhysical
    {
        internal UDSHandler UDSHandler { get; private set; }

        internal TreeNode OutProjectUDSNode { get; private set; }

        internal UDSNodeHandler(TreeNode node)
        {
            this.Node = node;
            Editors = new List<IEditable>();
            UDSNodeEditable edit = new UDSNodeEditable(this);
            Editors.Add(edit);
            this.CurrentEditor = edit;
        }

        #region IActionHandler 成員

        private UDSEditor _editor;

        public System.Windows.Forms.UserControl Editor
        {
            get
            {
                if (_editor == null)
                    _editor = new UDSEditor();
                return _editor;
            }
        }

        public System.Windows.Forms.TreeNode Node { get; private set; }

        #endregion

        #region IReloadable 成員

        public void Reload()
        {
            try
            {
                this.UDSHandler = MainForm.CurrentProject.GetUDSHandler();
            }
            catch (Exception ex)
            {
                string msg = "載入 UDS 時發生錯誤 : \n" + ex.Message;
                MessageBox.Show(msg, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Node.ToolTipText = msg;
                return;
            }

            this.Node.Nodes.Clear();

            if (this.UDSHandler == null)
                return;

            this.UDSHandler.ProjectContractChanged += new EventHandler(UDSHandler_ProjectContractChanged);

            foreach (ContractHandler contract in UDSHandler.Contracts)
            {
                TreeNode node = this.Node.Nodes.Add(contract.Name);
                node.ImageKey = "contracts";
                node.SelectedImageKey = "contracts";
                node.Tag = new LeaveableContractNodeHandler(node, contract);
            }

            OutProjectUDSNode = this.Node.Nodes.Add("專案外 Contract");
            OutProjectUDSNode.ImageKey = "outudts";
            OutProjectUDSNode.SelectedImageKey = "outudts";

            int count = 0;
            foreach (ContractHandler contract in UDSHandler.AllContracts)
            {
                if (UDSHandler.Contracts.Contains(contract)) continue;

                TreeNode node = this.OutProjectUDSNode.Nodes.Add(contract.Name);
                node.ImageKey = "outuds";
                node.SelectedImageKey = "outuds";
                node.Tag = new JoinableContractNodeHandler(node, contract);
                count++;
            }
            OutProjectUDSNode.Text = "專案外 Contract ( 共 " + count + " 個 )";

            this.Node.ExpandAll();
            this.Node.TreeView.SelectedNode = this.Node;
            CollapseOutsideProject();
        }

        void UDSHandler_ProjectContractChanged(object sender, EventArgs e)
        {
            this.Reload();
        }

        #endregion

        internal void CollapseOutsideProject()
        {
            if (OutProjectUDSNode != null)
                OutProjectUDSNode.Collapse();
        }

        #region IAddable 成員

        public void Add()
        {
            AddContractForm addForm = new AddContractForm(this);
            addForm.StartPosition = FormStartPosition.CenterParent;
            addForm.Added += new EventHandler(addForm_Added);
            addForm.ShowDialog();
        }

        void addForm_Added(object sender, EventArgs e)
        {
            this.Reload();
        }

        public string TitleOfAdd { get { return "新增 Contract(&A)"; } }

        #endregion

        internal void DeleteContract(ContractNodeHandler contractNodeHandler)
        {
            this.UDSHandler.DeleteContract(contractNodeHandler.Name);

            this.Node.Nodes.Remove(contractNodeHandler.Node);

            MainForm.CurrentUDS = this.UDSHandler;
        }

        #region IEditorManager 成員

        public List<IEditable> Editors { get; private set; }

        public IEditable CurrentEditor { get; set; }

        #endregion

        #region IImportable 成員
        private const string FILE_EXT = "csml";
        public void Import()
        {
            UDSImportForm form = new UDSImportForm(this);
            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowDialog();
            //OpenFileDialog sd = new OpenFileDialog();
            //sd.Multiselect = false;
            //sd.DefaultExt = FILE_EXT;
            //sd.Filter = "*." + FILE_EXT + "|*." + FILE_EXT;
            //DialogResult dr = sd.ShowDialog();

            //if (dr != DialogResult.OK) return;

            //XmlDocument doc = new XmlDocument();
            //doc.Load(sd.FileName);
            //XmlElement contractElement = doc.DocumentElement;

            //try
            //{
            //    this.UDSHandler.ImportContract(contractElement);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("匯入時發生錯誤:\n" + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}

            //string contractName = contractElement.GetAttribute("Name");
            //ContractHandler contract = null;
            //bool projectExists = false;
            //foreach (ContractHandler ch in this.UDSHandler.Contracts)
            //{
            //    if (ch.Name == contractName)
            //    {
            //        contract = ch;
            //        projectExists = true;
            //        break;
            //    }
            //}

            //bool outprojExists = false;
            //foreach (ContractHandler ch in this.UDSHandler.AllContracts)
            //{
            //    if (this.UDSHandler.Contracts.Contains(ch)) continue;
            //    if (ch.Name == contractName)
            //    {
            //        contract = ch;
            //        outprojExists = true;
            //        break;
            //    }
            //}

            //if (projectExists)
            //{
            //    //reload
            //}
            //else if (outprojExists)
            //{
            //    this.UDSHandler.JoinProject(contract);
            //}
            //else
            //{
            //    //new contract
            //    ContractHandler ch = ContractHandler.CreateNew(contractName, ExtendType.none);
            //    this.UDSHandler.JoinProject(ch);
            //}

            //this.Reload();

            //foreach (TreeNode treeNode in this.Node.Nodes)
            //{
            //    if (treeNode.Text == contractName)
            //    {
            //        this.Node.TreeView.SelectedNode = treeNode;
            //    }
            //}
        }

        #endregion

        #region IExportable 成員

        public void Export()
        {
            foreach (ContractHandler contract in this.UDSHandler.Contracts)
            {
                if (contract.Ready) continue;

                MessageBox.Show("尚有 Contract 未準備完成, 請稍侯再試!", "資訊", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog sd = new SaveFileDialog();
            sd.FileName = MainForm.CurrentProject.Name + "_Contracts." + FILE_EXT;
            sd.DefaultExt = FILE_EXT;
            sd.Filter = "*." + FILE_EXT + "|*." + FILE_EXT;
            DialogResult dr = sd.ShowDialog();

            if (dr != DialogResult.OK) return;
            
            XmlElement e = this.GetContractsXml();
            e.OwnerDocument.Save(sd.FileName);
        }

        #endregion

        internal XmlElement GetContractsXml()
        {
            XmlHelper h = new XmlHelper();
            XmlElement pref = MainForm.CurrentProject.Preference;
            XmlHelper ph = new XmlHelper(pref);

            foreach (XmlElement prop in ph.GetElements("Property[@Source='Import']"))
            {
                XmlElement p = (XmlElement)prop.CloneNode(true);
                p.RemoveAttribute("Source");
                h.AddElement(".", p);
            }

            foreach (ContractHandler contract in this.UDSHandler.Contracts)
            {
                h.AddElement(".", contract.GetXml());
            }
            XmlElement e = XmlHelper.ParseAsDOM(XmlHelper.Format(h.XmlString));
            return e;
        }

        #region IDeployToPhysical 成員

        public void DeployToPhysical()
        {
            DeployToPhysicalForm f = new DeployToPhysicalForm(this);
            f.ShowDialog();
        }

        #endregion

        #region IImportFromPhysical 成員

        public void ImportFromPhysical()
        {
            ImportFromPhysicalForm form = new ImportFromPhysicalForm(this);
            form.ShowDialog();
        }

        #endregion

        #region INodeHandler 成員

        public bool IsFirstClick { get; private set; }

        public void OnFirstClick() { }

        public void OnClick() { }

        #endregion
    }
}
