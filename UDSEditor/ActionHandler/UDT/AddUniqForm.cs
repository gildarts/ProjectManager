using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProjectManager.Project.UDT;
using FISCA.DSAClient;

namespace ProjectManager.ActionHandler.UDT
{
    public partial class AddUniqForm : Form
    {
        private const char Spec = '+';

        private DataGridView ParentFields { get; set; }
        private DataGridViewRow SelectedRow { get; set; }
        private DataGridView ParentUniq { get; set; }
        private string TableName { get; set; }
       

        public AddUniqForm(string tableName, DataGridView fieldGrid, DataGridView dgUniq, DataGridViewRow selectedRow)
        {      
            InitializeComponent();
            SelectedRow = selectedRow;
            ParentUniq = dgUniq;
            ParentFields = fieldGrid;
            TableName = tableName;
        }

        private void AddUniqForm_Load(object sender, EventArgs e)
        {
            string[] selectedField = new string[0];
            
            if (SelectedRow != null)
            {
                this.textBox1.Text = SelectedRow.Cells["colUniqName"].Value.ToString();                
                string fields = SelectedRow.Cells["colFields"].Value.ToString();
                selectedField = fields.Split(Spec);
            }

            foreach (DataGridViewRow row in ParentFields.Rows)
            {
                string fieldName = row.Cells["colName"].Value + string.Empty;
                if (UDTTable.IsDefaultField(fieldName)) continue;

                int index = this.dgFields.Rows.Add();
                DataGridViewRow r = this.dgFields.Rows[index];
                r.Cells[colFieldName.Name].Value = fieldName;
                r.Cells[colCheck.Name].Value = selectedField.Contains(fieldName);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool valid = this.CheckValid();
            if (!valid) return;

            string result = string.Empty;
            foreach (DataGridViewRow row in dgFields.Rows)
            {
                bool check = (bool)row.Cells[colCheck.Name].Value;
                if (!check) continue;

                result += "+" + row.Cells[colFieldName.Name].Value.ToString();
            }
            if (result.Length > 0)
                result = result.Substring(1);

            if (SelectedRow == null)
            {
                int index = ParentUniq.Rows.Add();
                SelectedRow = ParentUniq.Rows[index];
                SelectedRow.Cells["colUniqName"].Value = textBox1.Text;
            }

            SelectedRow.Cells["colUniqName"].Value = textBox1.Text;
            SelectedRow.Cells["colFields"].Value = result;
            this.Close();
        }

        private bool CheckValid()
        {
            bool valid = true;
            err.Clear();
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                err.SetError(textBox1, "名稱不可空白");
                valid = false;
            }

            bool contains = false;
            foreach (DataGridViewRow row in this.dgFields.Rows)
            {
                bool check = (bool)row.Cells[colCheck.Name].Value;
                if (check)
                {
                    contains = true;
                    break;
                }
            }

            if (!contains)
            {
                err.SetError(this.label2, "請至少勾選一個欄位");
                valid = false;
            }

            return valid;
        }

        private void dgFields_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != colCheck.Index) return;

            List<string> list = new List<string>();
            foreach (DataGridViewRow row in this.dgFields.Rows)
            {
                if ((bool)row.Cells[e.ColumnIndex].Value)
                    list.Add(row.Cells[colFieldName.Index].Value.ToString().ToLower());
            }

            list.Sort();

            StringBuilder sb = new StringBuilder("uniq_" + this.TableName);
            foreach (string fieldName in list)
            {
                sb.Append("_").Append(fieldName);
            }
            textBox1.Text = sb.ToString();
        }
    }
}
