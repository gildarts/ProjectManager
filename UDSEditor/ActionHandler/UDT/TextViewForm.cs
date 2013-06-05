using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProjectManager.ActionHandler.UDT
{
    public partial class TextViewForm : Form
    {
        private string _text;
        private ViewMode _mode;
        public TextViewForm(string text, ViewMode mode)
        {
            InitializeComponent();

            _text = text;
            _mode = mode;
        }

        private void TextViewForm_Load(object sender, EventArgs e)
        {
            txtContent.Text = _text;

            if (_mode == ViewMode.XML)
            {
                txtContent.Document.Language = xmlSyntaxLanguage1;
                xmlSyntaxLanguage1.FormatDocument(txtContent.Document);
            }
        }
    }

    public enum ViewMode{
        Text, XML
    }
}
