using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ProjectManager.Project.UDS.Service;
using System.Reflection;
using ProjectManager.Project.UDS.Service.Variable;
using System.Text.RegularExpressions;
using FISCA.DSAClient;
using ProjectManager.Util;
using ProjectManager.ActionHandler.UDS.Service.DAL;
using ProjectManager.Project.UDT;

namespace ProjectManager.ActionHandler.UDS.Service.Editable.Set
{
    public partial class SetServiceEditor : UserControl, IServiceUI
    {
        internal ServiceEntity Service { get; private set; }
        private const BindingFlags FLAG = BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetProperty;
        private bool _init;
        private string _original;
        private XmlHelper _source;

        internal SetServiceEditor()
        {
            InitializeComponent();
        }

        private void LoadPreprocesses()
        {
            dgProcessor.Rows.Clear();
            foreach (Preprocess p in this.Service.Preprocesses)
            {
                int index = dgProcessor.Rows.Add();
                DataGridViewRow row = dgProcessor.Rows[index];
                row.Cells[colProcessorName.Name].Value = p.Name;
                row.Cells[colProcessorType.Name].Value = p.Type.ToString();
                row.Cells[colProcessorSQL.Name].Value = p.SQL;
                row.Cells[colProcessorInvalidMessage.Name].Value = p.InvalidMessage;
                row.Tag = p;
            }
        }

        private void LoadFields()
        {
            this.txtFieldListSource.Text = Service.FieldList.Source;
            this.cboTable.Text = _source.GetText("TargetTableName");

            dgFieldList.Rows.Clear();
            foreach (XmlElement field in _source.GetElements("FieldList/Field"))
            {
                int index = dgFieldList.Rows.Add();
                DataGridViewRow row = dgFieldList.Rows[index];

                row.Cells[colSource.Name].Value = field.GetAttribute("Source");
                row.Cells[colTarget.Name].Value = field.GetAttribute("Target");
                row.Cells[colRequired.Name].Value = ToBoolean(field.GetAttribute("Required"));
                row.Cells[colAutoNumber.Name].Value = ToBoolean(field.GetAttribute("AutoNumber"));
                row.Cells[colConverter.Name].Value = field.GetAttribute("Converter");
                row.Cells[colSourceType.Name].Value = GetSourceType(field.GetAttribute("SourceType"));
                row.Cells[colIdentity.Name].Value = ToBoolean(field.GetAttribute("Identity"));
                row.Cells[colQuote.Name].Value = ToBoolean(field.GetAttribute("Quote"), true);
            }
        }

        private void LoadVariables()
        {
            this.dgVariables.Rows.Clear();
            foreach (IVariable v in Service.Variables)
            {
                int index = dgVariables.Rows.Add();
                DataGridViewRow row = dgVariables.Rows[index];
                row.Cells[colVarName.Name].Value = v.Name;
                row.Cells[colVarSource.Name].Value = v.Source;
                row.Cells[colOthers.Name].Value = v.Memo;
                row.Tag = v;
            }
        }

        #region IServiceUI 成員
        public List<string> SuggestionTarget { get; private set; }

        public event EventHandler SuggestionLoaded;

        protected void OnSuggestionLoaded()
        {
            if (SuggestionLoaded != null)
                SuggestionLoaded(this, EventArgs.Empty);
        }

        public event EventHandler DataChanged;

        public event EventHandler ChangeRecovered;

        public EditVariableForm EditVarForm { get; private set; }

        public void Initialize(string serviceFullName, XmlElement source)
        {
            this.Service = ServiceEntity.Parse(source);

            _source = new XmlHelper(source);

            this.txtServiceName.Text = serviceFullName;
            this.LoadVariables();
            this.EditVarForm = new EditVariableForm(this.Service);
            this.EditVarForm.Completed += delegate(object s, EventArgs arg)
            {
                LoadVariables();
            };
            this.cboTable.Text = _source.GetText("TargetTableName");
            txtRequestElement.Text = Service.RequestRecordElement;
            LoadFields();
            LoadPreprocesses();

            _original = source.OuterXml;
            _init = true;
        }

        public UserControl Editor
        {
            get { return this; }
        }

        public bool Valid
        {
            get
            {
                err.Clear();
                bool valid = true;

                if (!MainForm.CurrentUDT.ExistsInAllTables(cboTable.Text))
                {
                    err.SetError(cboTable, "資料表不存在");
                    return false;
                }

                List<string> fields = MainForm.CurrentUDT.ListFields(cboTable.Text);

                foreach (DataGridViewRow row in dgFieldList.Rows)
                {
                    row.ErrorText = string.Empty;
                    foreach (DataGridViewCell cell in row.Cells)
                        cell.ErrorText = string.Empty;
                }
                //檢查 RequestRecordElement
                if (string.IsNullOrWhiteSpace(txtRequestElement.Text))
                {
                    err.SetError(txtRequestElement, "不可空白");
                    valid = false;
                }

                // 檢查 autonumber
                int autoNumberCount = 0;
                int identityCount = 0;
                foreach (DataGridViewRow row in this.dgFieldList.Rows)
                {
                    bool autoNumber = this.GetBooleanValue(row.Cells[colAutoNumber.Name]);
                    if (autoNumber)
                        autoNumberCount++;

                    bool identity = this.GetBooleanValue(row.Cells[colIdentity.Name]);
                    if (identity)
                        identityCount++;

                    DataGridViewCell cell = row.Cells[colTarget.Name];
                    string target = cell.Value + string.Empty;
                    if (string.IsNullOrWhiteSpace(target))
                    {
                        cell.ErrorText = "不可空白";
                        valid = false;
                    }

                    if (!fields.Contains(target))
                    {
                        cell.ErrorText = "查無此欄位";
                        valid = false;
                    }

                    cell = row.Cells[colSource.Name];
                    string source = cell.Value + string.Empty;
                    if (string.IsNullOrWhiteSpace(source))
                    {
                        cell.ErrorText = "不可空白";
                        valid = false;
                    }
                }

                if (autoNumberCount > 1)
                {
                    err.SetError(dgFieldList, "最多指定一個 AutoNumber 欄位");
                    valid = false;
                }
                else if (autoNumberCount == 0)
                {
                    err.SetError(dgFieldList, "必須指定一個 AutoNumber 欄位");
                    valid = false;
                }

                if (identityCount == 0)
                {
                    err.SetError(dgFieldList, "至少指定一個以上的 Identity 欄位");
                    valid = false;
                }
                return valid;
            }
        }

        public XmlElement GetResult()
        {
            //TODO
            XmlHelper h = new XmlHelper("<Definition />");
            h.SetAttribute(".", "Type", "DBHelper");
            h.AddElement(".", "Action", ServiceAction.Set.ToString());

            h.AddElement(".", "RequestRecordElement", txtRequestElement.Text);
            h.AddElement(".", "TargetTableName", cboTable.Text);
            h.AddElement(".", "Mappings");
            XmlElement dfmElement = h.AddElement("Mappings", "DefaultMapping");
            StringBuilder sb = new StringBuilder("select ");

            foreach (DataGridViewRow row in this.dgFieldList.Rows)
            {
                bool autoNumber = this.GetBooleanValue(row.Cells[colAutoNumber.Name]);
                if (autoNumber)
                {
                    string target = row.Cells[colTarget.Name].Value + string.Empty;
                    string source = row.Cells[colSource.Name].Value + string.Empty;
                    sb.Append("\"").Append(target).Append("\" as \"").Append(source).Append("\"");
                    break;
                }
            }
            foreach (DataGridViewRow row in this.dgFieldList.Rows)
            {
                bool identity = this.GetBooleanValue(row.Cells[colIdentity.Name]);
                if (!identity) continue;

                string target = row.Cells[colTarget.Name].Value + string.Empty;
                //string source = row.Cells[colSource.Name].Value + string.Empty;
                sb.Append(", \"").Append(target).Append("\"");
                //sb.Append(", \"").Append(target).Append("\" as \"").Append(source).Append("\"");
            }
            sb.Append(" from ").Append(cboTable.Text);
            XmlNode node = dfmElement.OwnerDocument.CreateCDataSection(sb.ToString());
            dfmElement.AppendChild(node);

            XmlElement fieldListElement = h.AddElement(".", "FieldList");
            fieldListElement.SetAttribute("Source", txtFieldListSource.Text);
            foreach (DataGridViewRow row in this.dgFieldList.Rows)
            {
                XmlElement fieldElement = h.AddElement("FieldList", "Field");
                string source = row.Cells[colSource.Name].Value + string.Empty;
                fieldElement.SetAttribute("Source", source);

                string target = row.Cells[colTarget.Name].Value + string.Empty;
                fieldElement.SetAttribute("Target", target);

                bool required = this.GetBooleanValue(row.Cells[colRequired.Name]);
                if (required)
                    fieldElement.SetAttribute("Required", "true");

                bool autoNumber = this.GetBooleanValue(row.Cells[colAutoNumber.Name]);
                if (autoNumber)
                    fieldElement.SetAttribute("AutoNumber", "true");

                bool identity = this.GetBooleanValue(row.Cells[colIdentity.Name]);
                if (identity)
                    fieldElement.SetAttribute("Identity", "true");

                string converter = row.Cells[colConverter.Name].Value + string.Empty;
                if (!string.IsNullOrWhiteSpace(converter))
                    fieldElement.SetAttribute("Converter", converter);

                string sourceType = row.Cells[colSourceType.Name].Value + string.Empty;
                if (!string.Equals(sourceType, "Request", StringComparison.CurrentCultureIgnoreCase))
                    fieldElement.SetAttribute("SourceType", sourceType);

                bool quote = this.GetBooleanValue(row.Cells[colQuote.Name]);
                if (!quote)
                    fieldElement.SetAttribute("Quote", "false");
            }

            if (this.Service.Variables.Count > 0)
            {
                XmlElement varElement = h.AddElement(".", "InternalVariable");
                foreach (IVariable v in this.Service.Variables)
                    h.AddElement("InternalVariable", v.GetXml());
            }

            if (this.dgProcessor.Rows.Count > 0)
            {
                XmlElement proElement = h.AddElement(".", "Preprocesses");
                foreach (DataGridViewRow row in this.dgProcessor.Rows)
                {
                    Preprocess pp = row.Tag as Preprocess;
                    if (pp == null) continue;

                    h.AddElement("Preprocesses", pp.GetXml());
                }
            }
            return h.GetElement(".");
        }

        #endregion

        private void btnAddField_Click(object sender, EventArgs e)
        {
            int index = dgFieldList.Rows.Add();
            DataGridViewRow row = dgFieldList.Rows[index];
        }

        private void btnEditField_Click(object sender, EventArgs e)
        {
            this.EditSelectedField();
        }

        private void dgFieldList_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.EditSelectedField();
        }

        private void EditSelectedField()
        {
            if (dgFieldList.SelectedRows.Count == 0) return;
            DataGridViewRow row = dgFieldList.SelectedRows[0];
            Field field = row.Tag as Field;
            EditFieldForm addFieldForm = new EditFieldForm(this, Service.Action, field, this.Service);
            addFieldForm.StartPosition = FormStartPosition.CenterParent;
            addFieldForm.Completed += delegate(object s, FieldEventArgs args)
            {
                //FillFieldRow(row, args.Field);
                this.LoadVariables();
            };
            addFieldForm.ShowDialog();
        }

        private void btnDeleteField_Click(object sender, EventArgs e)
        {
            RemoveField();
        }

        private void dgFieldList_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                RemoveField();
            }
        }

        private void RemoveField()
        {
            if (dgFieldList.SelectedRows.Count == 0) return;

            foreach (DataGridViewRow row in dgFieldList.SelectedRows)
            {
                dgFieldList.Rows.Remove(row);
            }
        }

        private void btnAddVariable_Click(object sender, EventArgs e)
        {
            EditVarForm.ShowDialog();
        }

        private void btnRemoveVariable_Click(object sender, EventArgs e)
        {
            if (this.dgVariables.SelectedRows.Count == 0) return;

            DataGridViewRow r = dgVariables.SelectedRows[0];
            IVariable var = r.Tag as IVariable;
            dgVariables.Rows.Remove(r);
            Service.Variables.Remove(var);
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            XmlElement xml = this.GetResult();

            PreviewForm pf = new PreviewForm(xml);
            pf.ShowDialog();
        }

        private void btnRemoveProcessor_Click(object sender, EventArgs e)
        {
            this.RemoveProcessor();
        }

        private void RemoveProcessor()
        {
            foreach (DataGridViewRow row in this.dgProcessor.SelectedRows)
                dgProcessor.Rows.Remove(row);
        }

        private void dgProcessor_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                this.RemoveProcessor();
        }

        private void btnAddProcessor_Click(object sender, EventArgs e)
        {
            int rowIndex = dgProcessor.Rows.Add();
            DataGridViewRow row = dgProcessor.Rows[rowIndex];
            this.OpenPreprocess(row);
        }

        private void btnEditPreprocess_Click(object sender, EventArgs e)
        {
            this.EditPreprocess();
        }

        private void EditPreprocess()
        {
            if (dgProcessor.SelectedRows.Count == 0) return;
            DataGridViewRow row = dgProcessor.SelectedRows[0];
            this.OpenPreprocess(row);
        }

        private void OpenPreprocess(DataGridViewRow row)
        {
            EditProcessorForm editPreForm = new EditProcessorForm(row);
            editPreForm.StartPosition = FormStartPosition.CenterParent;
            editPreForm.ShowDialog();
            OnDataChanged(this, EventArgs.Empty);
        }

        private void dgProcessor_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow row = dgProcessor.Rows[e.RowIndex];
            OpenPreprocess(row);
        }

        private void OnDataChanged(object sender, EventArgs e)
        {
            if (!_init) return;

            string current = this.GetResult().OuterXml;

            if (current == _original)
            {
                if (this.ChangeRecovered != null)
                    ChangeRecovered.Invoke(this, e);
            }
            else
            {
                if (DataChanged != null)
                    DataChanged.Invoke(this, e);
            }
        }

        private void OnDataChanged(object sender, DataGridViewRowsAddedEventArgs e)
        {
            OnDataChanged(sender, EventArgs.Empty);
        }

        private void OnDataChanged(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            OnDataChanged(sender, EventArgs.Empty);
        }

        private bool ToBoolean(string value)
        {
            return ToBoolean(value, false);
        }

        private bool ToBoolean(string value, bool defaultValue)
        {
            bool b;
            if (!bool.TryParse(value, out b))
                return defaultValue;
            return b;
        }

        private string GetSourceType(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return "Request";
            else
            {
                if (value.Equals("Variable", StringComparison.CurrentCultureIgnoreCase))
                    return "Variable";
            }
            return "Request";
        }

        private bool GetBooleanValue(DataGridViewCell cell)
        {
            if (cell.Value == null)
                return false;
            bool b;
            if (!bool.TryParse(cell.Value.ToString(), out b))
                b = false;
            return b;
        }

        private void dgFieldList_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            this.OnDataChanged(this, EventArgs.Empty);
        }
    }
}
