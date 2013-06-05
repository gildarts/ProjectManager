using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProjectManager.Project.UDT;
using System.Reflection;
using FISCA.DSAClient;
using System.Xml;

namespace ProjectManager.ActionHandler.UDT
{
    public partial class FilterConditionForm : Form
    {
        private EditDataForm _editForm;
        public FilterConditionForm(EditDataForm editForm)
        {
            InitializeComponent();
            _editForm = editForm;
        }

        private void FilterConditionForm_Load(object sender, EventArgs e)
        {
            cboFields.Items.Clear();
            cboFields.Items.Add(string.Empty);
            XmlHelper h = new XmlHelper(_editForm.Table.GetContent());
            foreach (XmlElement field in h.GetElements("Field"))
                cboFields.Items.Add(field.GetAttribute("Name"));
            txtCondition.Document.LoadLanguageFromXml(Assembly.GetExecutingAssembly().GetManifestResourceStream("ProjectManager.ActiproSoftware.SQL.xml"), 0);
        }

        private void btnASC_Click(object sender, EventArgs e)
        {
            if (cboFields.Text == string.Empty) return;

            int index = dgOrder.Rows.Add();
            DataGridViewRow row = dgOrder.Rows[index];
            row.Cells[colColumnName.Name].Value = cboFields.Text;
            row.Cells[colSort.Name].Value = "ASC";

            cboFields.Items.Remove(cboFields.SelectedItem);
            cboFields.SelectedIndex = 0;
        }

        private void btnDesc_Click(object sender, EventArgs e)
        {
            if (cboFields.Text == string.Empty) return;

            int index = dgOrder.Rows.Add();
            DataGridViewRow row = dgOrder.Rows[index];
            row.Cells[colColumnName.Name].Value = cboFields.Text;
            row.Cells[colSort.Name].Value = "DESC";

            cboFields.Items.Remove(cboFields.SelectedItem);
            cboFields.SelectedIndex = 0;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (dgOrder.SelectedRows.Count == 0) return;

            foreach (DataGridViewRow row in dgOrder.SelectedRows)
            {
                string str = row.Cells[colColumnName.Name].Value.ToString();
                cboFields.Items.Add(str);
                dgOrder.Rows.Remove(row);                
            }
        }

        private void FilterConditionForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
            this.Visible = false;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            _editForm.Filters.Condition = txtCondition.Text;
            _editForm.Filters.Orders.Clear();

            foreach (DataGridViewRow row in dgOrder.Rows)
            {
                string fieldName = row.Cells[colColumnName.Name].Value.ToString();
                string sort = row.Cells[colSort.Name].Value.ToString();
                _editForm.Filters.Orders.Add(fieldName + " " + sort);
            }
            _editForm.Execute();
            this.Hide();
        }
    }
}
