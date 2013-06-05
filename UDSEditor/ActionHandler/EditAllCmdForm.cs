using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProjectManager.ActionHandler.UDT.Command;
using FISCA.DSAClient;
using System.Xml;

namespace ProjectManager.ActionHandler
{
    public partial class EditAllCmdForm : Form
    {
        private DataGridView _dgView;
        public EditAllCmdForm(DataGridView datagrid)
        {
            InitializeComponent();
            _dgView = datagrid;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void EditAllCmdForm_Load(object sender, EventArgs e)
        {
            XmlHelper h = new XmlHelper("<UDT/>");

            foreach (DataGridViewRow row in _dgView.Rows)
            {
                 IUDTCommand cmd = row.Tag as IUDTCommand;
                 h.AddElement(".", cmd.Result);
            }
            this.xmlEditor1.Text = h.XmlString;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _dgView.Rows.Clear();

            XmlHelper h = new XmlHelper(this.xmlEditor1.Text);
            foreach (XmlElement element in h.GetElements("Command"))
            {
                string type = element.GetAttribute("Type");
                IUDTCommand cmd = UDTCmdProvider.GetCmd(type);
                cmd.Result = element;

                int index = _dgView.Rows.Add();
                DataGridViewRow row = _dgView.Rows[index];
                row.Tag = cmd;
                row.Cells["colType"].Value = cmd.Type;
                row.Cells["colDesc"].Value = cmd.Description;
            }
            this.Close();
        }
    }
}
