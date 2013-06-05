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
    public partial class EditConditionForm : Form
    {
        internal event EventHandler<ConditionEventArgs> Completed;
        private ServiceEntity _service;
        private Condition _condition;
        private IServiceUI _editor;

        internal EditConditionForm(IServiceUI parentEditor)
        {
            InitializeComponent();
            _editor = parentEditor;
            this._editor.EditVarForm.Completed += new EventHandler(EditVarForm_Completed);
        }

        internal EditConditionForm(IServiceUI parentEditor, ServiceEntity serviceHandler)
        {
            InitializeComponent();
            _editor = parentEditor;
            this._editor.EditVarForm.Completed += new EventHandler(EditVarForm_Completed);
            this._service = serviceHandler;
        }

        void EditVarForm_Completed(object sender, EventArgs e)
        {
            ReloadSource();
        }

        internal EditConditionForm(IServiceUI parentEditor, ServiceEntity serviceHandler, Condition condition)
        {
            InitializeComponent();
            // TODO: Complete member initialization
            this._service = serviceHandler;
            this._condition = condition;
            _editor = parentEditor;
            this._editor.EditVarForm.Completed += new EventHandler(EditVarForm_Completed);
            this._service = serviceHandler;
        }

        private void EditConditionForm_Load(object sender, EventArgs e)
        {
            LoadSuggestion();
            _editor.SuggestionLoaded += new EventHandler(_editor_SuggestionLoaded);

            foreach (string st in Enum.GetNames(typeof(SourceType)))
                cboSourceType.Items.Add(st);

            cboSourceType.Text = SourceType.Request.ToString();

            foreach (IConverter converter in _service.Converters)
            {
                cboInputConverter.Items.Add(converter.Name);
            }

            foreach (string st in ConverterType.Converters)
            {
                cboInputConverter.Items.Add(st);
            }
            cboInputConverter.Text = ConverterType.Empty.ToString();

            if (_condition == null) return;

            cboSourceType.Text = _condition.SourceType.ToString();
            cboTarget.Text = _condition.Target;
            txtSource.Text = _condition.Source;
            chkQuote.Checked = _condition.Quote;
            cboComparer.Text = _condition.Comparer;
            cboInputConverter.Text = _condition.InputConverter.ToString();
            chkRequired.Checked = _condition.Required;
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

            Condition condition = new Condition();
            condition.InputConverter = ConverterType.Parse(cboInputConverter.Text);
            condition.Required = chkRequired.Checked;
            condition.Source = txtSource.Text;
            condition.Target = cboTarget.Text;
            condition.Comparer = cboComparer.Text;
            condition.EmptyReplacement = string.Empty;

            SourceType stype = SourceType.Request;
            if (!Enum.TryParse<SourceType>(cboSourceType.Text, true, out stype))
                stype = SourceType.Request;

            condition.SourceType = stype;

            Completed.Invoke(this, new ConditionEventArgs(condition));
            this.Close();
        }

        private void lnkEditVariable_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this._editor.EditVarForm.ShowDialog();
        }

        void ReloadSource()
        {
            txtSource.AutoCompleteCustomSource.Clear();
            txtSource.Items.Clear();
            foreach (IVariable v in _service.Variables)
            {
                txtSource.AutoCompleteCustomSource.Add(v.Name);
                txtSource.Items.Add(v.Name);
            }
        }

        private void cboSourceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboTarget.Items.Clear();
            if (cboSourceType.Text == SourceType.Request.ToString())
            {
                this.lnkEditVariable.Enabled = false;
                if(_editor.SuggestionTarget != null)
                    cboTarget.Items.AddRange(_editor.SuggestionTarget.ToArray());
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
            if (cboSourceType.Text != SourceType.Request.ToString()) return;

            if (_editor.SuggestionTarget.Contains(cboTarget.Text))
                txtSource.Text = StringUtil.ConvertToDisplayName(cboTarget.Text);
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

    internal class ConditionEventArgs : EventArgs
    {
        internal Condition Condition { get; private set; }

        internal ConditionEventArgs(Condition condition)
        {
            this.Condition = condition;
        }
    }
}
