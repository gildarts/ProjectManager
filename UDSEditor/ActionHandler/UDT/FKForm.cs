using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProjectManager.Project.UDT;
using System.Xml;
using FISCA.DSAClient;

namespace ProjectManager.ActionHandler.UDT
{
    public partial class FKForm : Form
    {
        public event EventHandler Completed;

        private Project.UDT.UDTTable UDTTable;
        private DataGridView _dgFields;

        internal FKForm(UDTTable uDTTable, DataGridView dgFields)
        {
            this.UDTTable = uDTTable;
            this._dgFields = dgFields;
            InitializeComponent();
        }

        private void FKForm_Load(object sender, EventArgs e)
        {
            List<string> allTables = MainForm.CurrentUDT.ListAllTables();
            this.cboRefTable.Items.AddRange(allTables.ToArray());
            
            if (allTables.Count > 0)            
                this.cboRefTable.SelectedIndex = 0;

            foreach (DataGridViewRow row in _dgFields.Rows)
            {
                string fieldname = row.Cells["colName"].Value + string.Empty;
                cboMainField.Items.Add(fieldname);
            }

            if (cboMainField.Items.Count > 0)
                cboMainField.SelectedIndex = 0;

            this.cboOnDelete.SelectedIndex = 0;
            this.cboOnUpdate.SelectedIndex = 0;
        }

        private void cboRefTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnRefTableChanged();
        }

        private void OnRefTableChanged()
        {
            this.dgField.Rows.Clear();
            this.cboRefField.Items.Clear();
            string tableName = cboRefTable.SelectedItem.ToString();
            List<string> fields = MainForm.CurrentUDT.ListFields(tableName);
            this.cboRefField.Items.AddRange(fields.ToArray());

            if (fields.Count > 0)
                this.cboRefField.SelectedIndex = 0;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int rowIndex = dgField.Rows.Add();
            dgField.Rows[rowIndex].Cells[colLocal.Name].Value = cboMainField.SelectedItem.ToString();
            dgField.Rows[rowIndex].Cells[colRef.Name].Value = cboRefField.SelectedItem.ToString();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgField.SelectedRows)
                dgField.Rows.Remove(row);
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            err.Clear();
            if (dgField.Rows.Count == 0)
            {
                err.SetError(dgField, "必須加入欄位");
                return;
            }

            if (this.Completed != null)
                Completed.Invoke(this, EventArgs.Empty);

            this.Close();
        }

        public XmlElement GetResult()
        {
            XmlHelper h = new XmlHelper("<ForeignKey/>");
            h.SetAttribute(".","Name",this.GetName());

            h.AddElement(".", "MainTable");
            h.SetAttribute("MainTable", "Name", this.UDTTable.Name);
            h.SetAttribute("MainTable", "Type", "udt");

            h.AddElement(".", "RefTable");
            string refTableName = this.cboRefTable.SelectedItem.ToString();
            string refTableType = "static";
            if (refTableName.StartsWith("$"))
            {
                refTableName = refTableName.Substring(1);
                refTableType = "udt";
            }
            h.SetAttribute("RefTable", "Name", refTableName);
            h.SetAttribute("RefTable", "Type", refTableType);
            
            foreach (DataGridViewRow row in dgField.Rows)
            {
                string mf = row.Cells[colLocal.Name].Value + string.Empty;
                string rf = row.Cells[colRef.Name].Value + string.Empty;

                h.AddElement("MainTable", "FieldName", mf);
                h.AddElement("RefTable", "FieldName", rf);
            }

            //h.AddElement(".", "UpdateAction", cboOnUpdate.SelectedItem.ToString());
            h.SetAttribute(".", "DeleteAction", cboOnDelete.SelectedItem.ToString());
            h.SetAttribute(".", "UpdateAction", cboOnUpdate.SelectedItem.ToString());
            return h.GetElement(".");
        }

        private string GetName()
        {
            StringBuilder sb = new StringBuilder("fk_main_$");
            sb.Append(this.UDTTable.Name).Append("_");

            List<string> mainFields = new List<string>();
            List<string> refFields = new List<string>();
            foreach (DataGridViewRow row in dgField.Rows)
            {
                string mf = row.Cells[colLocal.Name].Value + string.Empty;
                string rf = row.Cells[colRef.Name].Value + string.Empty;

                mainFields.Add(mf);
                refFields.Add(rf);
            }

            mainFields.Sort();
            refFields.Sort();

            foreach (string field in mainFields)
                sb.Append(field).Append("_");

            sb.Append("ref_").Append(cboRefTable.SelectedItem.ToString());

            foreach (string field in refFields)
                sb.Append("_").Append(field);

            return sb.ToString();
        }
    }
}
