using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using FISCA.DSAClient;

namespace ProjectManager.ActionHandler.UDS.Contract.AuthEditors
{
    public partial class ExtPropForm : Form
    {
        internal event EventHandler<ExtEventArg> Completed;

        private XmlElement Xml { get; set; }

        public ExtPropForm()
        {
            InitializeComponent();
        }

        public ExtPropForm(XmlElement xml)
        {
            InitializeComponent();
            Xml = xml;
        }
        
        private void ExtPropForm_Load(object sender, EventArgs e)
        {
            if (Xml != null)
            {
                txtSQL.Text = Xml.InnerText;
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Completed != null)
            {                
                XmlElement result = XmlHelper.ParseAsDOM("<ExtendProperty/>");
                result.SetAttribute("Type", "SQL");
                XmlNode section = result.OwnerDocument.CreateCDataSection(txtSQL.Text);
                result.AppendChild(section);

                ExtEventArg arg = new ExtEventArg(result, "SQL");
                Completed.Invoke(this, arg);
            }
            this.Close();
        }
    }

    internal class ExtEventArg : EventArgs
    {
        internal XmlElement Result { get; private set; }        
        internal string ExtType { get; private set; }

        internal ExtEventArg(XmlElement result, string type)
        {
            this.Result = result;            
            this.ExtType = type;
        }
    }
}
