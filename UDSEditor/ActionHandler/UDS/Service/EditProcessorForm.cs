using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ProjectManager.Project.UDS.Service;

namespace ProjectManager.ActionHandler.UDS.Service
{
    public partial class EditProcessorForm : Form
    {
        private DataGridViewRow _row;
        private Preprocess _preprocess;
        private bool _saved = false;

        public EditProcessorForm(DataGridViewRow row)
        {
            InitializeComponent();
            _row = row;
        }

        private void EditProcessorForm_Load(object sender, EventArgs e)
        {
            this.cboType.Items.AddRange(Enum.GetNames(typeof(PreprocessType)));

            Preprocess source = _row.Tag as Preprocess;
            if (source != null)
            {
                _preprocess = source;
                this.txtName.Text = _preprocess.Name;
                this.txtSQL.Text = _preprocess.SQL;
                this.txtInvalidMessage.Text = _preprocess.InvalidMessage;
                this.cboType.Text = _preprocess.Type.ToString();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            PreprocessType type;
            if (!Enum.TryParse<PreprocessType>(cboType.Text, true, out type))
                type = PreprocessType.Variable;

            Preprocess p = Preprocess.Create(txtName.Text, type, txtSQL.Text, txtInvalidMessage.Text);
            _row.Tag = p;

            _row.Cells[0].Value = p.Type.ToString();
            _row.Cells[1].Value = p.Name;
            _row.Cells[2].Value = p.SQL;
            _row.Cells[3].Value = p.InvalidMessage;

            _saved = true;
            this.Close();
        }

        private void EditProcessorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_saved) return;
            if (_preprocess == null)
                _row.DataGridView.Rows.Remove(_row);
        }
    }
}
