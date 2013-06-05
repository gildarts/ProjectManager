using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProjectManager.Project.UDS.Service;
using ProjectManager.Project.UDS.Service.Variable;

namespace ProjectManager.ActionHandler.UDS.Service
{
    public partial class EditVariableForm : Form
    {
        internal event EventHandler Completed;

        private ServiceEntity _service;
        private IVariable _variable;

        internal EditVariableForm(ServiceEntity service)
        {
            InitializeComponent();
            _service = service;
        }

        private void EditVariableForm_Load(object sender, EventArgs e)
        {
            cboSource.Items.Clear();
            cboSource.Items.AddRange(VariableFactory.VariableTypes.ToArray());
            cboSource.SelectedIndex = 0;
        }

        private void cboSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            string source = cboSource.Text;
            _variable = VariableFactory.Create(source);
            this.panel1.Controls.Clear();
            this.panel1.Controls.Add(_variable.Editor);
            _variable.Editor.Dock = DockStyle.Fill;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!_variable.Valid)
            {
                MessageBox.Show(_variable.InvalidMessage, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            foreach (IVariable v in _service.Variables)
            {
                if (v.Name == _variable.Name)
                {
                    MessageBox.Show("名稱重覆", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            _service.Variables.Add(_variable);

            if (Completed != null)                         
                Completed.Invoke(this, e);
            this.Hide();
        }

        private void EditVariableForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }
    }
}
