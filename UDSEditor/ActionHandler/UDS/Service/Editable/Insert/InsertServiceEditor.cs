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
using ProjectManager.ActionHandler.UDS.Service.Editable.Delete;
using ProjectManager.ActionHandler.UDS.Service.Editable.Select;
using ProjectManager.Project.UDS.Service.Converter;

namespace ProjectManager.ActionHandler.UDS.Service.Editable.Insert
{
    public partial class InsertServiceEditor : UserControl, IServiceUI
    {
        internal ServiceEntity Service { get; private set; }
        private const BindingFlags FLAG = BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetProperty;
        //private bool _init;
        private string _original;
           
        internal InsertServiceEditor()
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

        private void LoadConverters()
        {
            dgConverters.Rows.Clear();
            foreach (IConverter converter in this.Service.Converters)
            {
                int index = dgConverters.Rows.Add();
                DataGridViewRow row = dgConverters.Rows[index];
                row.Cells[colConverterName.Name].Value = converter.Name;
                row.Cells[colConverterType.Name].Value = converter.Type;
                row.Tag = converter;
            }
        }
                private void LoadFields()
        {
            this.txtFieldListName.Text = Service.FieldList.Name;
            this.txtFieldListSource.Text = Service.FieldList.Source;

            dgFieldList.Columns.Clear();

            foreach (PropertyInfo info in typeof(Field).GetProperties(FLAG))
            {
                DataGridViewColumn column;
                
                if (info.Name == "OutputType") continue;
                if (info.Name == "OutputConverter") continue;
                if (info.Name == "Alias") continue;

                if (info.PropertyType == typeof(bool))
                {
                    column = new DataGridViewCheckBoxColumn();
                    column.CellTemplate = new DataGridViewCheckBoxCell();
                }
                else
                {
                    column = new DataGridViewTextBoxColumn();
                    column.CellTemplate = new DataGridViewTextBoxCell();
                }
                column.Name = info.Name;
                column.HeaderText = info.Name;
                dgFieldList.Columns.Add(column);
            }

            dgFieldList.Rows.Clear();
            foreach (Field field in Service.FieldList.Fields)
            {
                int index = dgFieldList.Rows.Add();
                DataGridViewRow row = dgFieldList.Rows[index];

                FillFieldRow(row, field);
            }
        }

        private void FillFieldRow(DataGridViewRow row, Field field)
        {
            row.Tag = field;
            foreach (DataGridViewColumn col in dgFieldList.Columns)
            {
                foreach (PropertyInfo info in field.GetType().GetProperties(FLAG))
                {
                    if (info.Name == col.Name)
                        row.Cells[col.Name].Value = info.GetValue(field, null).ToString();
                }
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

        private void MakeSuggest()
        {
            SuggestionTarget = new List<string>();
            string patternString = string.Empty;

            patternString = @"\sinto\s(.*)\s";

            PatternEntity pe = new PatternEntity();
            pe.PatternString = patternString;
            pe.SQL = txtSQLTemplate.Text;

            BackgroundWorker w = new BackgroundWorker();
            w.DoWork += new DoWorkEventHandler(w_DoWork);
            w.RunWorkerCompleted += new RunWorkerCompletedEventHandler(w_RunWorkerCompleted);
            w.RunWorkerAsync(pe);

        }

        void w_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            SuggestionTarget = new List<string>();
            if (e.Error == null)
            {
                SuggestionTarget = e.Result as List<string>;
                if (SuggestionLoaded != null)
                    SuggestionLoaded.Invoke(this, EventArgs.Empty);
            }
        }

        void w_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                PatternEntity pe = e.Argument as PatternEntity;
                Regex reg = new Regex(pe.PatternString, RegexOptions.IgnoreCase | RegexOptions.Singleline);
                Match m = reg.Match(pe.SQL);
                String sql = string.Empty;

                if (m.Success)
                {
                    sql = m.Groups[1].Value;
                }
                sql = "select * from " + sql + " where 1=0";

                List<string> list = new List<string>();
                XmlHelper rsp = new XmlHelper(MainForm.CurrentProject.Query(sql));
                foreach (XmlElement xml in rsp.GetElements("Metadata/Column"))
                {
                    list.Add(xml.GetAttribute("Field"));
                }
                e.Result = list;
            }
            catch { }
        }

        private void txtSQLTemplate_Leave(object sender, EventArgs e)
        {
            MakeSuggest();
        }

        #region IServiceUI 成員
        public List<string> SuggestionTarget { get; private set; }
        
        public event EventHandler SuggestionLoaded;

        public event EventHandler DataChanged;

        public event EventHandler ChangeRecovered;

        public EditVariableForm EditVarForm { get; private set; }

        public void Initialize(string serviceFullName, XmlElement source)
        {                      
            this.Service = ServiceEntity.Parse(source);

            this.txtServiceName.Text = serviceFullName;
            this.txtSQLTemplate.Text = Service.SQLTemplate.Trim();
            this.LoadVariables();
            this.EditVarForm = new EditVariableForm(this.Service);
            this.EditVarForm.Completed += delegate(object s, EventArgs arg)
            {
                LoadVariables();
            };

            txtRequestElement.Text = Service.RequestRecordElement;
            LoadFields();
            LoadPreprocesses();
            LoadConverters();

            this.txtSQLTemplate.Document.LoadLanguageFromXml(Assembly.GetExecutingAssembly().GetManifestResourceStream("ProjectManager.ActiproSoftware.SQL.xml"), 0);
            MakeSuggest();

            _original = this.GetResult().OuterXml;
            //_init = true;
        }

        public UserControl Editor
        {
            get { return this; }
        }

        public bool Valid
        {
            //TODO
            get { return true; }
        }

        public XmlElement GetResult()
        {
            //TODO
            XmlHelper h = new XmlHelper("<Definition />");
            h.SetAttribute(".", "Type", "DBHelper");
            h.AddElement(".", "Action", ServiceAction.Insert.ToString());
            
            XmlElement sqlTmp = h.AddElement(".", "SQLTemplate");
            XmlCDataSection section = sqlTmp.OwnerDocument.CreateCDataSection(this.txtSQLTemplate.Text);
            sqlTmp.AppendChild(section);

            h.AddElement(".", "RequestRecordElement", txtRequestElement.Text);
            FieldList fields = new FieldList(this.txtFieldListName.Text, this.txtFieldListSource.Text);
            foreach (DataGridViewRow row in this.dgFieldList.Rows)
            {
                Field f = Field.Parse(row);
                fields.Fields.Add(f);
            }
            h.AddElement(".", fields.GetXml(ServiceAction.Insert));
                  
            if (this.Service.Variables.Count > 0)
            {
                XmlElement varElement = h.AddElement(".", "InternalVariable");                
                foreach (IVariable v in this.Service.Variables)                
                    h.AddElement("InternalVariable", v.GetXml());                
            }

            if (this.Service.Converters.Count > 0)
            {
                XmlElement cvElement = h.AddElement(".", "Converters");
                foreach (IConverter c in this.Service.Converters)
                    h.AddElement("Converters", c.Output());
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
            EditFieldForm addFieldForm = new EditFieldForm(this, Service.Action, null, this.Service);
            addFieldForm.StartPosition = FormStartPosition.CenterParent;
            addFieldForm.Completed += delegate(object s, FieldEventArgs args)
            {
                int index = dgFieldList.Rows.Add();
                DataGridViewRow row = dgFieldList.Rows[index];
                FillFieldRow(row, args.Field);
                this.LoadVariables();
            };
            addFieldForm.ShowDialog();
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
                FillFieldRow(row, args.Field);
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

        private void btnAddConverter_Click(object sender, EventArgs e)
        {
            EditConverterForm editForm = new EditConverterForm();
            editForm.StartPosition = FormStartPosition.CenterScreen;
            editForm.Completed += delegate(object senderObj, EventArgs args)
            {
                int index = dgConverters.Rows.Add();
                IConverter converter = editForm.GetResult();
                DataGridViewRow row = dgConverters.Rows[index];
                row.Cells[colConverterName.Name].Value = converter.Name;
                row.Cells[colConverterType.Name].Value = converter.Type;

                row.Tag = converter;
                this.Service.Converters.Add(converter);
            };
            editForm.ShowDialog();
        }

        private void btnEditConverter_Click(object sender, EventArgs e)
        {
            if (dgConverters.SelectedRows.Count == 0) return;

            DataGridViewRow row = dgConverters.SelectedRows[0];
            IConverter converter = row.Tag as IConverter;
            EditConverterForm editForm = new EditConverterForm(converter);
            editForm.StartPosition = FormStartPosition.CenterScreen;
            editForm.Completed += delegate(object senderObj, EventArgs args)
            {
                row.Tag = editForm.GetResult();
            };
            editForm.ShowDialog();
        }

        private void btnDeleteConverter_Click(object sender, EventArgs e)
        {
            if (this.dgConverters.SelectedRows.Count == 0) return;

            DataGridViewRow r = dgConverters.SelectedRows[0];
            IConverter var = r.Tag as IConverter;
            dgConverters.Rows.Remove(r);
            Service.Converters.Remove(var);
        }
    }
}
