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
using ProjectManager.Util;
using ProjectManager.ActionHandler.UDS.Service.Editable;
using ProjectManager.Project.UDS.Service.Converter;

namespace ProjectManager.ActionHandler.UDS.Service
{
    public partial class EditFieldForm : Form
    {
        internal event EventHandler<FieldEventArgs> Completed;

        private ServiceAction _action;
        private Field _field;
        private ServiceEntity _service;
        private IServiceUI _editor;

        internal EditFieldForm(IServiceUI editor, ServiceAction action, Field field, ServiceEntity service)
        {
            InitializeComponent();
            _action = action;
            _field = field;
            _service = service;
            _editor = editor;
        }

        private void EditFieldForm_Load(object sender, EventArgs e)
        {
            _editor.EditVarForm.Completed += new EventHandler(EditVarForm_Completed);
            _editor.SuggestionLoaded += new EventHandler(_editor_SuggestionLoaded);

            LoadSuggestion();

            foreach (string st in Enum.GetNames(typeof(SourceType)))
                cboSourceType.Items.Add(st);

            cboSourceType.Text = SourceType.Request.ToString();

            foreach(IConverter c in _service.Converters)
            {
                cboInputConverter.Items.Add(c.Name);
                cboOutputConverter.Items.Add(c.Name);
            }
            
            foreach (string st in ConverterType.Converters)
            {
                cboInputConverter.Items.Add(st);
                cboOutputConverter.Items.Add(st);
            }
            cboInputConverter.Text = ConverterType.Empty.ToString();
            cboOutputConverter.Text = ConverterType.Empty.ToString();

            foreach (string st in Enum.GetNames(typeof(IOType)))
            {
                cboInputType.Items.Add(st);
                cboOutputType.Items.Add(st);
            }
            cboInputType.Text = IOType.Element.ToString();
            cboOutputType.Text = IOType.Element.ToString();

            if (_action == ServiceAction.Insert || _action == ServiceAction.Update)
            {
                txtAlias.Enabled = false;
                cboOutputType.Enabled = false;
                cboOutputConverter.Enabled = false;
                chkMandatory.Enabled = false;
            }
            else if (_action == ServiceAction.Select)
            {
                cboInputType.Enabled = false;
                cboInputConverter.Enabled = false;
                chkAutoNumber.Enabled = false;
                chkRequired.Enabled = false;
            }
            else
            {
                this.Close();
                return;
            }

            if (_field == null) return;

            cboSourceType.Text = _field.SourceType.ToString();
            cboTarget.Text = _field.Target;
            txtSource.Text = _field.Source;
            chkQuote.Checked = _field.Quote;

            if (_action == ServiceAction.Select)
            {
                txtAlias.Text = _field.Alias;
                cboOutputConverter.Text = _field.OutputConverter.ToString();
                cboOutputType.Text = _field.OutputType.ToString();
                chkMandatory.Checked = _field.Mandatory;
            }
            else if (_action == ServiceAction.Insert)
            {
                chkAutoNumber.Checked = _field.AutoNumber;
                cboInputConverter.Text = _field.InputConverter.ToString();
                cboInputType.Text = _field.InputType.ToString();
                chkRequired.Checked = _field.Required;
            }
            else
            {
                cboInputConverter.Text = _field.InputConverter.ToString();
                cboInputType.Text = _field.InputType.ToString();
                chkRequired.Checked = _field.Required;
            }
        }

        void _editor_SuggestionLoaded(object sender, EventArgs e)
        {
            LoadSuggestion();
        }

        private void LoadSuggestion()
        {
            cboTarget.Items.Clear();
            if (_editor.SuggestionTarget != null)
                cboTarget.Items.AddRange(_editor.SuggestionTarget.ToArray());
        }

        void EditVarForm_Completed(object sender, EventArgs e)
        {
            ReloadSource();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            err.Clear();
            bool valid = true;

            if (string.IsNullOrWhiteSpace(txtSource.Text))
            {
                err.SetError(txtSource, "不可空白");
                valid = false;
            }

            if (string.IsNullOrWhiteSpace(cboTarget.Text))
            {
                err.SetError(cboTarget, "不可空白");
                valid = false;
            }

            if (cboSourceType.Text == SourceType.Variable.ToString())
            {
                bool contains = false;
                foreach (IVariable v in _service.Variables)
                {
                    if (v.Name == txtSource.Text)
                    {
                        contains = true;
                        break;
                    }
                }

                if (!contains)
                {
                    foreach (Preprocess p in _service.Preprocesses)
                    {
                        if (p.Type == PreprocessType.Variable && p.Name == txtSource.Text)
                        {
                            contains = true;
                            break;
                        }
                    }
                }

                if (!contains)
                {
                    err.SetError(txtSource, "InternalVariable 中不包含此變數");
                    valid = false;
                }
            }

            if (!valid) return;

            if (Completed == null)
            {
                this.Close();
                return;
            }

            Field field = new Field();
            field.Alias = txtAlias.Text;
            field.AutoNumber = chkAutoNumber.Checked;
            field.InputConverter = ConverterType.Parse(cboInputConverter.Text);
            field.OutputConverter = ConverterType.Parse(cboOutputConverter.Text);
            field.Mandatory = chkMandatory.Checked;
            field.Quote = chkQuote.Checked;
            field.Required = chkRequired.Checked;
            field.Source = txtSource.Text;
            field.Target = cboTarget.Text;

            IOType itype = IOType.Element;
            if (!Enum.TryParse<IOType>(cboInputType.Text, true, out itype))
                itype = IOType.Element;

            field.InputType = itype;

            IOType otype = IOType.Element;
            if (!Enum.TryParse<IOType>(cboOutputType.Text, true, out otype))
                otype = IOType.Element;

            field.OutputType = otype;

            SourceType stype = SourceType.Request;
            if (!Enum.TryParse<SourceType>(cboSourceType.Text, true, out stype))
                stype = SourceType.Request;

            field.SourceType = stype;

            Completed.Invoke(this, new FieldEventArgs(field));
            this.Close();
        }

        private void lnkEditVariable_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _editor.EditVarForm.ShowDialog();
        }

        void ReloadSource()
        {
            txtSource.Items.Clear();
            txtSource.AutoCompleteCustomSource.Clear();
            foreach (IVariable v in _service.Variables)
            {
                txtSource.AutoCompleteCustomSource.Add(v.Name);
                txtSource.Items.Add(v.Name);
            }
        }

        private void cboSourceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSourceType.Text == SourceType.Request.ToString())
            {
                this.lnkEditVariable.Enabled = false;
                txtSource.Items.Clear();
            }
            else
            {
                this.lnkEditVariable.Enabled = true;
                ReloadSource();
            }
        }

        private void cboTarget_TextChanged(object sender, EventArgs e)
        {
            if (cboSourceType.Text == SourceType.Request.ToString())
            {
                if (_editor.SuggestionTarget == null) return;
                if (_editor.SuggestionTarget.Contains(cboTarget.Text))
                    txtSource.Text = StringUtil.ConvertToDisplayName(cboTarget.Text);
            }
        }

        private void cboTarget_KeyUp(object sender, KeyEventArgs e)
        {
            if (cboSourceType.Text == SourceType.Request.ToString())
            {
                if (e.KeyCode == Keys.Enter)
                    txtSource.Text = StringUtil.ConvertToDisplayName(cboTarget.Text);
            }
        }
    }

    internal class FieldEventArgs : EventArgs
    {
        internal Field Field { get; private set; }

        internal FieldEventArgs(Field field)
        {
            this.Field = field;
        }
    }
}
