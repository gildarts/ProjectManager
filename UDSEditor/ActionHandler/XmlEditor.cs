using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using FISCA.DSAClient;

namespace ProjectManager.ActionHandler
{
    public partial class XmlEditor : UserControl
    {
        private XmlElement _source;
        private string _sourceString;
        internal event EventHandler DataChanged;
        internal event EventHandler ChangeRecovered;

        internal string XmlText { get { return txtXML.Text; } }

        internal XmlElement Xml { get { return XmlHelper.ParseAsDOM(txtXML.Text); } }

        private bool _changed;

        public XmlEditor(XmlElement source)
        {
            InitializeComponent();
            _source = source;
        }

        private void XmlEditor_Load(object sender, EventArgs e)
        {
            this.txtXML.Text = _source.OuterXml;
            this.xmlSyntaxLanguage1.FormatDocument(txtXML.Document);
            _sourceString = txtXML.Text;
            this.txtXML.TextChanged += new EventHandler(txtXML_TextChanged);
        }

        internal void Reset()
        {
            _changed = false;
            _sourceString = txtXML.Text;
        }

        void txtXML_TextChanged(object sender, EventArgs e)
       {
            CheckChanged();
        }

        private void CheckChanged()
        {
            if (this.txtXML.Text != _sourceString && DataChanged != null)
            {
                _changed = true;
                DataChanged.Invoke(this, EventArgs.Empty);
            }

            else if (_changed && this.txtXML.Text == _sourceString && ChangeRecovered != null)
            {
                _changed = false;
                ChangeRecovered.Invoke(this, EventArgs.Empty);
            }
        }

        private void txtFormat_Click(object sender, EventArgs e)
        {
            this.xmlSyntaxLanguage1.FormatDocument(txtXML.Document);
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否復原成初始編輯資料?", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.txtXML.Text = _source.OuterXml;
                this.xmlSyntaxLanguage1.FormatDocument(txtXML.Document);
            }
        }

        internal void ClearError()
        {
            err.Clear();
        }

        internal void SetError(string message)
        {
            err.SetError(btnUndo, message);
        }
    }
}
