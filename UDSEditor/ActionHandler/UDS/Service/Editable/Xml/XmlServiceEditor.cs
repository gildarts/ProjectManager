using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FISCA.DSAClient;

namespace ProjectManager.ActionHandler.UDS.Service.Editable.Xml
{
    public partial class XmlServiceEditor : UserControl, IServiceUI
    {
        private string _original;

        public XmlServiceEditor()
        {
            InitializeComponent();
        }

        #region IServiceUI 成員

        public event EventHandler DataChanged;

        public event EventHandler ChangeRecovered;

        public event EventHandler SuggestionLoaded;

        protected virtual void OnSuggestionLoaded()
        {
            if (SuggestionLoaded != null)
                SuggestionLoaded.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnDataChanged()
        {
            if (DataChanged != null)
                DataChanged.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnChangeRecovered()
        {
            if (ChangeRecovered != null)
                ChangeRecovered.Invoke(this, EventArgs.Empty);
        }

        private void OnDataChanged(object sender, EventArgs e)
        {
            string result = txtDefinition.Text;
            if (result != this._original)
                OnDataChanged();
            else
                OnChangeRecovered();
        }

        public void Initialize(string serviceFullName, System.Xml.XmlElement source)
        {
            XmlHelper h = new XmlHelper(source);
            this.txtServiceAction.Text = h.GetText("Action");
            this.txtServiceName.Text = serviceFullName;
                      
            this.txtDefinition.Text = source.OuterXml;
            _original = this.txtDefinition.Text;
        }

        public UserControl Editor
        {
            get { return this; }
        }

        public bool Valid
        {
            get
            {
                this.errorProvider1.Clear();
                try
                {
                    XmlHelper.ParseAsDOM(txtDefinition.Text);
                    return true;
                }
                catch
                {
                    this.errorProvider1.SetError(this.label3, "內容格式錯誤");
                    return false;
                }
            }
        }

        public List<string> SuggestionTarget
        {
            get { return null; }
        }

        public System.Xml.XmlElement GetResult()
        {
            return XmlHelper.ParseAsDOM(txtDefinition.Text);
        }

        public EditVariableForm EditVarForm
        {
            get { return null; }
        }

        #endregion

        private void XmlServiceEditor_Load(object sender, EventArgs e)
        {
            this.txtDefinition.Editor.TextChanged += new EventHandler(OnDataChanged);
        }
    }
}
