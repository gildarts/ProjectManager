using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProjectManager.Util;

namespace ProjectManager.ActionHandler.UDS.Contract.AuthEditors
{
    public partial class AddExtPropForm : Form
    {
        public event EventHandler Completed;
        private string[] _fields;

        internal string FieldName { get; private set; }
        internal string AliasName { get; private set; }

        public AddExtPropForm(string[] fields)
        {
            InitializeComponent();
            _fields = fields;
        }

        private void AddExtPropForm_Load(object sender, EventArgs e)
        {
            cboField.Items.AddRange(_fields);
        }

        private void cboField_SelectedIndexChanged(object sender, EventArgs e)
        {            
            txtAlias.Text = StringUtil.ConvertToDisplayName(cboField.Text);         
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            err.Clear();

            FieldName = cboField.Text;
            AliasName = txtAlias.Text;

            bool valid = true;
            if (!_fields.Contains(FieldName))
            {
                err.SetError(cboField, "此欄位不在資料表中");
                valid = false;
            }

            if (string.IsNullOrWhiteSpace(AliasName))
            {
                err.SetError(txtAlias, "不可空白");
                valid = false;
            }

            if (!valid) return;

            if (Completed != null)
                Completed.Invoke(this, EventArgs.Empty);

            this.Close();
        }
    }
}
