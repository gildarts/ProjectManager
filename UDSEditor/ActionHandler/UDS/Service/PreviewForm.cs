using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace ProjectManager.ActionHandler.UDS.Service
{
    public partial class PreviewForm : Form
    {
        private XmlElement _source;

        public PreviewForm(XmlElement xml)
        {
            InitializeComponent();
            _source = xml;
        }

        private void PreviewForm_Load(object sender, EventArgs e)
        {
            this.xmlEditor1.Text = _source.OuterXml;                
        }
    }
}
