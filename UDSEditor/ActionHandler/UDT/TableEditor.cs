using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProjectManager.Project.UDT;
using System.Xml;
using FISCA.DSAClient;

namespace ProjectManager.ActionHandler.UDT
{
    internal partial class TableEditor : UserControl
    {
        internal event EventHandler DataChanged;
        internal event EventHandler ChangeRecovered;

        internal TableNodeHandler TableNodeHandler { get; private set; }

        private XmlHelper _tableContent;
        private bool _initialized = false;

        internal TableEditor(TableNodeHandler parent)
        {
            InitializeComponent();
            TableNodeHandler = parent;
        }

        private void TableEditor_Load(object sender, EventArgs e)
        {
            txtName.Text = TableNodeHandler.Table.Name;
            _tableContent = new XmlHelper(TableNodeHandler.Table.GetContent());

            dgFields.Rows.Clear();
            foreach (XmlElement field in _tableContent.GetElements("Field"))
            {
                int index = dgFields.Rows.Add();
                DataGridViewRow row = dgFields.Rows[index];
                row.Cells[colName.Name].Value = field.GetAttribute("Name");
                row.Cells[colDataType.Name].Value = field.GetAttribute("DataType");
                row.Cells[colIndexed.Name].Value = field.GetAttribute("Indexed");
                row.Cells[colAllowNull.Name].Value = field.GetAttribute("AllowNull");
                row.Cells[colDefault.Name].Value = field.GetAttribute("Default");

                row.Tag = field;

                if (UDTTable.IsDefaultField(field))
                    row.ReadOnly = true;
            }

            foreach (XmlElement uniq in _tableContent.GetElements("Unique"))
            {
                XmlHelper uniqHelper = new XmlHelper(uniq);
                string uniqFields = "";
                foreach (XmlElement field in uniqHelper.GetElements("FieldName"))
                    uniqFields += "+" + field.InnerText;

                if (!string.IsNullOrWhiteSpace(uniqFields))
                    uniqFields = uniqFields.Substring(1);

                int index = dgUniq.Rows.Add(uniq.Name);
                DataGridViewRow row = dgUniq.Rows[index];
                row.Cells[colUniqName.Name].Value = uniq.GetAttribute("Name");
                row.Cells[colFields.Name].Value = uniqFields;
            }

            foreach (XmlElement fk in _tableContent.GetElements("ForeignKey"))
            {
                this.AddForeignKey(fk);
            }

            this.TableNodeHandler.Table.Renamed += new EventHandler(Table_Renamed);
            _initialized = true;
        }

        void Table_Renamed(object sender, EventArgs e)
        {
            this.txtName.Text = this.TableNodeHandler.Table.Name;
        }

        private void btnAddField_Click(object sender, EventArgs e)
        {
            string newName = "NewField";
            int index = dgFields.Rows.Add();
            DataGridViewRow row = dgFields.Rows[index];
            DataGridViewCell nameCell = row.Cells[colName.Name];
            nameCell.Value = newName;
            row.Cells[colDataType.Name].Value = "String";
            row.Cells[colIndexed.Name].Value = false;

            nameCell.Selected = true;
            dgFields.BeginEdit(true);

            CheckDataChanged(true);
        }

        private void btnRemoveField_Click(object sender, EventArgs e)
        {
            if (dgFields.SelectedRows.Count == 0) return;

            DataGridViewRow row = dgFields.SelectedRows[0];
            string fieldName = row.Cells[colName.Name].Value + string.Empty;

            if (UDTTable.IsDefaultField(fieldName))
            {
                MessageBox.Show("系統預設欄位不可刪除", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            dgFields.Rows.Remove(dgFields.SelectedRows[0]);
            CheckDataChanged(true);
        }

        private void dgFields_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dgFields.Rows[e.RowIndex];
            string fieldname = row.Cells[colName.Name].Value + string.Empty;

            if (string.IsNullOrWhiteSpace(fieldname))
            {
                btnRemoveField.Enabled = false;
                return;
            }

            if (UDTTable.IsDefaultField(fieldname))
            {
                btnRemoveField.Enabled = false;
                return;
            }
            btnRemoveField.Enabled = true;
        }

        private void btnAddUniq_Click(object sender, EventArgs e)
        {
            AddUniqForm uniqForm = new AddUniqForm(txtName.Text, this.dgFields, this.dgUniq, null);
            uniqForm.StartPosition = FormStartPosition.CenterParent;
            uniqForm.ShowDialog();
        }

        private void btnEditUniq_Click(object sender, EventArgs e)
        {
            if (dgUniq.SelectedRows.Count == 0) return;

            AddUniqForm uniqForm = new AddUniqForm(txtName.Text, this.dgFields, this.dgUniq, dgUniq.SelectedRows[0]);
            uniqForm.StartPosition = FormStartPosition.CenterParent;
            uniqForm.ShowDialog();
        }

        private void dgFields_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == colName.Index)
            {
                DataGridViewCell cell = dgFields.Rows[e.RowIndex].Cells[e.ColumnIndex];

                cell.ErrorText = string.Empty;
                string value = string.Empty;
                if (cell.Value != null)
                {
                    value = cell.Value.ToString().ToLower();
                    cell.Value = value;
                }

                if (string.IsNullOrWhiteSpace(value))
                {
                    cell.ErrorText = "欄位名稱不可空白";
                    return;
                }

                foreach (DataGridViewRow row in dgFields.Rows)
                {
                    if (row.Index == e.RowIndex) continue;

                    string fieldName = row.Cells[colName.Name].Value + string.Empty;
                    if (fieldName == value)
                    {
                        cell.ErrorText = "欄位名稱重覆";
                        return;
                    }
                }
            }
            CheckDataChanged(true);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!Valid)
            {
                MessageBox.Show("文件內容有誤, 請修正後再行儲存", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.Save();
        }

        #region IEditable 成員

        public string DocumentTitle { get { return string.Empty; } }

        public Control Editor
        {
            get { return this; }
        }

        public bool Valid
        {
            get
            {
                bool valid = true;
                foreach (DataGridViewRow row in this.dgFields.Rows)
                {
                    if (!string.IsNullOrWhiteSpace(row.ErrorText)) valid = false;
                    foreach (DataGridViewCell cell in row.Cells)
                        if (!string.IsNullOrWhiteSpace(cell.ErrorText)) valid = false;
                }

                foreach (DataGridViewRow row in this.dgUniq.Rows)
                {
                    if (!string.IsNullOrWhiteSpace(row.ErrorText)) valid = false;
                    foreach (DataGridViewCell cell in row.Cells)
                        if (!string.IsNullOrWhiteSpace(cell.ErrorText)) valid = false;
                }

                return valid;
            }
        }

        public void Save()
        {
            if (!Valid)
                return;

            XmlElement current = this.GetCurrentSchema();           
            try
            {
                UDTNodeHandler udt = this.TableNodeHandler.Node.Parent.Tag as UDTNodeHandler;
                udt.UDTHandler.UpdateSchema(current);

                CheckDataChanged(false);
                MessageBox.Show("儲存完畢！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (DSAServerException ex)
            {
                StringBuilder sb = new StringBuilder("儲存失敗！");
                sb.AppendLine(ex.Message);
                sb.AppendLine("----------- 詳細資訊 ------------");
                sb.AppendLine(ex.Response);

                MessageBox.Show(sb.ToString(), "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private XmlElement GetCurrentSchema()
        {
            XmlHelper h = new XmlHelper("<Table/>");
            h.SetAttribute(".", "Name", txtName.Text);

            List<string> list = new List<string>();
            foreach (DataGridViewRow row in this.dgFields.Rows)
            {
                string fieldName = row.Cells[colName.Name].Value.ToString();
                string datatype = row.Cells[colDataType.Name].Value.ToString();
                string indexed = row.Cells[colIndexed.Name].Value.ToString();
                string allowNull = row.Cells[colAllowNull.Name].Value + string.Empty;
                string defaultValue = row.Cells[colDefault.Name].Value + string.Empty;

                allowNull = string.IsNullOrWhiteSpace(allowNull) ? "true" : allowNull;
                XmlElement fieldElement = h.AddElement(".", "Field");
                fieldElement.SetAttribute("Name", fieldName);
                fieldElement.SetAttribute("DataType", datatype);
                fieldElement.SetAttribute("Indexed", indexed);
                fieldElement.SetAttribute("AllowNull", allowNull);
                fieldElement.SetAttribute("Default", defaultValue);
                list.Add(fieldName);
            }

            foreach (DataGridViewRow row in this.dgUniq.Rows)
            {
                XmlElement uniq = h.AddElement(".", "Unique");

                string uniqName = row.Cells[colUniqName.Name].Value.ToString();
                string fields = row.Cells[colFields.Name].Value.ToString();
                string[] fs = fields.Split('+');

                uniq.SetAttribute("Name", uniqName);
                int count = 0;
                foreach (string fname in fs)
                {
                    if (string.IsNullOrWhiteSpace(fname)) continue;

                    string fieldName = fname.ToLower().Trim();
                    bool contains = false;
                    foreach (string n in list)
                    {
                        if (string.Equals(n, fieldName, StringComparison.CurrentCultureIgnoreCase))
                        {
                            contains = true;
                            break;
                        }
                    }

                    if (contains)
                    {
                        h.AddElement("Unique", "FieldName", fieldName);
                        count++;
                    }
                }
                if (count == 0)
                    h.GetElement(".").RemoveChild(uniq);
            }

            foreach (DataGridViewRow row in dgFK.Rows)
            {
                XmlElement x = row.Tag as XmlElement;
                if (x != null)
                    h.AddElement(".", x);
            }
            return h.GetElement(".");
        }

        #endregion

        private void dgUniq_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != colFields.Index) return;

            DataGridViewCell cell = dgUniq.Rows[e.RowIndex].Cells[e.ColumnIndex];
            cell.ErrorText = string.Empty;

            string fields = cell.Value as string;
            if (fields == null)
                cell.ErrorText = "不可空白";

            string[] fs = fields.Split('+');

            List<string> list = new List<string>();
            foreach (DataGridViewRow row in this.dgFields.Rows)
            {
                string fieldName = row.Cells[colName.Name].Value.ToString();
                list.Add(fieldName);
            }

            int count = 0;
            foreach (string fname in fs)
            {
                if (string.IsNullOrWhiteSpace(fname)) continue;

                string fieldName = fname.ToLower().Trim();
                bool contains = false;
                foreach (string n in list)
                {
                    if (string.Equals(n, fieldName, StringComparison.CurrentCultureIgnoreCase))
                    {
                        contains = true;
                        break;
                    }
                }

                if (contains)
                {
                    count++;
                }
                else
                {
                    cell.ErrorText = "找不到對應欄位 : " + fieldName;
                    return;
                }
            }
            if (count == 0)
            {
                cell.ErrorText = "至少指定一個欄位";
            }
        }

        private void btnDeleteUniq_Click(object sender, EventArgs e)
        {
            if (dgUniq.SelectedRows.Count == 0) return;
            dgUniq.Rows.Remove(dgUniq.SelectedRows[0]);
        }

        private void CheckDataChanged(bool changed)
        {
            if (!_initialized) return;

            if (DataChanged != null && changed)
                DataChanged.Invoke(this, EventArgs.Empty);

            else if (ChangeRecovered != null && !changed)
                ChangeRecovered.Invoke(this, EventArgs.Empty);
        }

        private void dgUniq_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            CheckDataChanged(true);
        }

        private void dgUniq_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            CheckDataChanged(true);
        }

        private void btnAddFK_Click(object sender, EventArgs e)
        {
            FKForm fkForm = new FKForm(this.TableNodeHandler.Table, this.dgFields);
            fkForm.Completed += new EventHandler(fkForm_Completed);
            fkForm.ShowDialog();
        }

        void fkForm_Completed(object sender, EventArgs e)
        {
            FKForm fkForm = sender as FKForm;
            XmlElement result = fkForm.GetResult();
            AddForeignKey(result);
            CheckDataChanged(true);
        }

        private void AddForeignKey(XmlElement e)
        {
            int index = dgFK.Rows.Add();
            DataGridViewRow row = dgFK.Rows[index];
            row.Tag = e;

            XmlHelper h = new XmlHelper(e);
            row.Cells[colFKName.Name].Value = e.GetAttribute("Name");

            XmlElement rtElement = h.GetElement("RefTable");
            string rtName = rtElement.GetAttribute("Name");
            string rfType = rtElement.GetAttribute("Type");

            if (rfType.ToLower() == "udt")
                rtName = "$" + rtName;

            row.Cells[colFKRefTable.Name].Value = rtName;

            StringBuilder sb = new StringBuilder();
            foreach (XmlElement fnElement in h.GetElements("MainTable/FieldName"))
                sb.Append(fnElement.InnerText).Append("+");
            if (sb.Length > 0)
                sb.Remove(sb.Length - 1, 1);
            row.Cells[colFKMainFields.Name].Value = sb.ToString();

            sb = new StringBuilder();
            foreach (XmlElement fnElement in h.GetElements("RefTable/FieldName"))
                sb.Append(fnElement.InnerText).Append("+");
            if (sb.Length > 0)
                sb.Remove(sb.Length - 1, 1);
            row.Cells[colFKRefFields.Name].Value = sb.ToString();
            row.Cells[colFKOnDelete.Name].Value = e.GetAttribute("DeleteAction");
            row.Cells[colFKOnUpdate.Name].Value = e.GetAttribute("UpdateAction");
        }

        private void btnRemoveFK_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgFK.SelectedRows)
                dgFK.Rows.Remove(row);
            CheckDataChanged(true);
        }
    }
}
