using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProjectManager.Project.UDT;
using System.Xml;
using FISCA.DSAClient;

namespace ProjectManager.ActionHandler.UDT
{
    class TableNodeHandler : INodeHandler, IRenameable, IDeleteable, IExportable, IEditorManager, ITestable
    {
        internal UDTTable Table { get; private set; }

        internal TableNodeHandler(TreeNode node, UDTTable table)
        {
            Node = node;
            Table = table;

            this.Editors = new List<IEditable>();
            IEditable uieditor = new TableUIEditable("資料表-" + Table.Name, this);
            Editors.Add(uieditor);

            IEditable xmlEditor = new TableXmlEditable("資料表-" + Table.Name, this);
            Editors.Add(xmlEditor);

            CurrentEditor = uieditor;
        }

        #region INodeHandler 成員

        public System.Windows.Forms.TreeNode Node { get; private set; }

        public bool IsFirstClick { get; private set; }

        public void OnFirstClick() { }

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
                this.Table.Rename(e.Label);
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
            DialogResult dr = MessageBox.Show("確定刪除資料表「" + Table.Name + "」?", "問題", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                UDTNodeHandler udt = this.Node.Parent.Tag as UDTNodeHandler;

                //如果是 null, 唯一合理的解釋是此 table 是非專案內的資料表, 故再往上找一層
                if (udt == null)
                    udt = this.Node.Parent.Parent.Tag as UDTNodeHandler;

                //如果還是 null, 除了程式寫錯之外沒有任何合理的解釋
                if (udt == null)
                {
                    MessageBox.Show("節點位置錯誤");
                    return;
                }

                try
                {
                    udt.UDTHandler.DropTable(Table.Name);
                    udt.Reload();
                    MessageBox.Show("刪除完成", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("刪除失敗:\n" + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public string TitleOfDelete { get { return "刪除資料表 「" + this.Table.Name + "」(&D)"; } }

        #endregion

        #region IExportable 成員
        private const string FILE_EXT = "tml";
        public void Export()
        {
            XmlElement e = this.Table.GetContent();

            SaveFileDialog sd = new SaveFileDialog();
            sd.FileName = this.Table.Name + "." + FILE_EXT;
            sd.DefaultExt = FILE_EXT;
            sd.Filter = "*." + FILE_EXT + "|*." + FILE_EXT;
            DialogResult dr = sd.ShowDialog();

            if (dr != DialogResult.OK) return;

            e.OwnerDocument.Save(sd.FileName);
        }

        #endregion

        #region IEditorManager 成員

        public List<IEditable> Editors { get; private set; }

        public IEditable CurrentEditor { get; set; }

        #endregion

        #region ITestable 成員

        public string TitleOfTest
        {
            get { return "檢視資料"; }
        }

        public string TestImageKey
        {
            get { return "table"; }
        }

        public void Test()
        {
            EditDataForm editDataForm = new EditDataForm(this.Table);
            editDataForm.StartPosition = FormStartPosition.CenterParent;
            editDataForm.ShowDialog();
        }

        #endregion
    }
}
