using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProjectManager.Project.UDT;
using FISCA.DSAClient;
using System.Xml;

namespace ProjectManager.ActionHandler.UDT
{
    class UDTNodeHandler : INodeHandler, IAddable, IReloadable, ITestable, IEditorManager, IExportable, IImportable
    {
        internal UDTHandler UDTHandler { get; private set; }
        internal TreeNode OutProjectUDTNode { get; private set; }

        internal UDTNodeHandler(TreeNode node)
        {
            Node = node;
            Editors = new List<IEditable>();
            UDTNodeEditable edit = new UDTNodeEditable(this);
            Editors.Add(edit);
            this.CurrentEditor = edit;
        }

        #region INodeHandler 成員

        public TreeNode Node { get; private set; }

        public bool IsFirstClick { get; private set; }

        public void OnFirstClick() { }

        public void OnClick() { }

        #endregion

        #region IReloadable 成員

        public void Reload()
        {
            try
            {
                this.UDTHandler = MainForm.CurrentProject.GetUDT();
            }
            catch (Exception ex)
            {
                string msg = "載入 UDT 時發生錯誤 : \n" + ex.Message;
                MessageBox.Show(msg, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Node.ToolTipText = msg;
                return;
            }

            this.Node.Nodes.Clear();
            if (this.UDTHandler == null)
            {
                return;
            }

            this.UDTHandler.ProjectTableChanged += new EventHandler(UDTHandler_ProjectTableChanged);
            
            foreach (UDTTable table in UDTHandler.Tables)
            {
                TreeNode udtNode1 = this.Node.Nodes.Add(table.Name);
                udtNode1.ImageKey = "udt";
                udtNode1.SelectedImageKey = "udt";
                udtNode1.Tag = new LeaveableTableNodeHandler(udtNode1, table);
            }

            OutProjectUDTNode = this.Node.Nodes.Add("專案外資料表");
            OutProjectUDTNode.ImageKey = "outudts";
            OutProjectUDTNode.SelectedImageKey = "outudts";
            
            int count = 0;
            foreach (UDTTable table in UDTHandler.AllUDTTables)
            {
                if (UDTHandler.Tables.Contains(table)) continue;

                TreeNode udtNode1 = OutProjectUDTNode.Nodes.Add(table.Name);
                udtNode1.ImageKey = "outudt";
                udtNode1.SelectedImageKey = "outudt";
                udtNode1.Tag = new JoinableTableNodeHandler(udtNode1, table);
                count++;
            }
            OutProjectUDTNode.Text = "專案外資料表 ( 共 " + count + " 個 )";

            this.Node.ExpandAll();
            this.Node.TreeView.SelectedNode = this.Node;
            CollapseOutsideProject();
        }

        void UDTHandler_ProjectTableChanged(object sender, EventArgs e)
        {
            this.Reload();
        }

        #endregion

        internal void CollapseOutsideProject()
        {
            if(OutProjectUDTNode != null)
                OutProjectUDTNode.Collapse();
        }
        
        #region IAddable 成員

        public void Add()
        {
            AddUDTForm udtForm = new AddUDTForm(this);
            udtForm.StartPosition = FormStartPosition.CenterParent;
            udtForm.ShowDialog();
        }

        public string TitleOfAdd { get { return "新增資料表(&A)"; } }



        #endregion

        #region ITestable 成員

        public void Test()
        {
            UDTTestForm form = new UDTTestForm();
            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowDialog();
        }

        public string TitleOfTest
        {
            get { return "執行 SQL 測試"; }
        }

        public string TestImageKey
        {
            get { return "sql"; }
        }

        #endregion

        #region IEditorManager 成員

        public List<IEditable> Editors { get; private set; }

        public IEditable CurrentEditor { get; set; }
        
        #endregion



        #region IExportable 成員

        private const string FILE_EXT = "tsml";
        public void Export()
        {           
            XmlHelper h = new XmlHelper();
            foreach (UDTTable table in this.UDTHandler.Tables)
            {
                h.AddElement(".", table.GetContent());
            }

            SaveFileDialog sd = new SaveFileDialog();
            sd.FileName = MainForm.CurrentProject.Name + "_Tables." + FILE_EXT;
            sd.DefaultExt = FILE_EXT;
            sd.Filter = "*." + FILE_EXT + "|*." + FILE_EXT;
            DialogResult dr = sd.ShowDialog();

            if (dr != DialogResult.OK) return;

            XmlElement e = XmlHelper.ParseAsDOM(XmlHelper.Format(h.XmlString));
            e.OwnerDocument.Save(sd.FileName);
        }

        #endregion

        #region IImportable 成員

        public void Import()
        {
            UDTImportForm form = new UDTImportForm(this);
            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowDialog();
        }

        #endregion
    }
}
