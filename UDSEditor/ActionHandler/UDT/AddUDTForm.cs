using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProjectManager.Project.UDT;

namespace ProjectManager.ActionHandler.UDT
{
    public partial class AddUDTForm : Form
    {
        private UDTNodeHandler UDTNodeHandler { get; set; }

        internal AddUDTForm(UDTNodeHandler nodeHandler)
        {
            InitializeComponent();
            UDTNodeHandler = nodeHandler;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            err.Clear();
            bool valid = true;
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                valid = false;
                err.SetError(txtName, "名稱不可空白");
            }

            if (!valid) return;

            UDTHandler udt = UDTNodeHandler.UDTHandler;

            if (udt.Exists(txtName.Text))
            {
                valid = false;
                err.SetError(txtName, "資料表名稱已存在");
            }

            if (!valid) return;

            UDTTable table = null;
            try
            {
                table = udt.CreateTable(txtName.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //int nodeIndex = UDTNodeHandler.Node.Nodes.Count - 1;
            //TreeNode newNode = UDTNodeHandler.Node.Nodes.Insert(nodeIndex, txtName.Text.ToLower());
            //newNode.SelectedImageKey = "udt";
            //newNode.ImageKey = "udt";
            //newNode.Tag = new TableNodeHandler(newNode, table);
            //newNode.TreeView.SelectedNode = newNode;

            foreach (TreeNode node in UDTNodeHandler.Node.Nodes)
            {
                if (node.Text == txtName.Text.ToLower())
                {
                    node.TreeView.SelectedNode = node;
                    break;
                }
            }

            this.Close();
        }
    }
}
