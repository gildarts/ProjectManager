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

            // 停用的 Contract 連不上也不能呼叫，在樹上用灰字標出來
            if (!contract.Enabled)
            {
                node.ForeColor = System.Drawing.Color.Gray;
                node.ToolTipText = "此 Contract 已停用，無法連線或呼叫其中的 Service";
            }
            node.Text = this.DisplayName;
        }

        /// <summary>
        /// 樹上顯示的文字：停用的 Contract 在名稱後面加上「(停用)」。
        /// </summary>
        private string DisplayName
        {
            get
            {
                string text = Contract.Enabled ? Contract.Name : Contract.Name + " (停用)";

                // 已載入的 Contract 才知道簽章狀態
                if (Contract.Ready)
                {
                    int unsigned = 0;
                    foreach (PackageHandler ph in Contract.Packages)
                        unsigned += ph.UnsignedCount;
                    if (unsigned > 0)
                        text += "（" + unsigned + " 個未簽章）";
                }
                return text;
            }
        }

        /// <summary>重新計算顯示文字（簽章狀態變了時由 Package 節點呼叫）</summary>
        internal void RefreshDisplay()
        {
            if (this.Ready)
                this.Node.Text = this.DisplayName;
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

            if (this.Contract.Ready)
            {
                // 已經載入過（專案內的 Contract），直接畫出 Package，不重複下載
                Contract_Reloaded(this.Contract, EventArgs.Empty);
            }
            else if (this.Contract.Loading)
            {
                // 下載中，等 Reloaded 事件回來再畫
                Ready = false;
                this.Node.Text = this.DisplayName + "(資料準備中...)";
            }
            else
            {
                this.Reload();
            }

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
            this.Node.Text = this.DisplayName + "(資料準備中...)";
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
            this.Node.Text = this.DisplayName;
            Ready = true;
        }

        #endregion

        #region IRenameable 成員

        public void Rename()
        {
            Node.TreeView.AfterLabelEdit += new NodeLabelEditEventHandler(TreeView_AfterLabelEdit);
            Node.TreeView.LabelEdit = true;
            // 編輯時只顯示純名稱，避免「(停用)」被當成名稱的一部分
            Node.Text = this.Contract.Name;
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
            }

            // 節點文字一律自己設定（含「(停用)」），不讓 TreeView 套用編輯框的內容
            e.CancelEdit = true;
            Node.Text = this.DisplayName;
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
                // 還沒點開過的 Contract 先開始載入，稍後再匯出即可
                this.Contract.EnsureLoaded();
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
