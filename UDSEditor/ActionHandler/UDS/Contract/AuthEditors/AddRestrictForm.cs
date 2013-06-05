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
using System.Reflection;

namespace ProjectManager.ActionHandler.UDS.Contract.AuthEditors
{
    public partial class AddRestrictForm : Form
    {
        internal event EventHandler<RestrictEventArgs> Completed;

        private string[] SuggestProperties { get; set; }

        private string[] SuggestRoles { get; set; }

        private XmlElement Xml { get; set; }

        public AddRestrictForm(string[] suggestProperties, string[] suggestRoles)
        {
            InitializeComponent();

            this.SuggestProperties = suggestProperties;
            this.SuggestRoles = suggestRoles;
        }

        public AddRestrictForm(string[] suggestProperties, string[] suggestRoles, XmlElement xml)
        {
            InitializeComponent();

            this.SuggestProperties = suggestProperties;
            this.SuggestRoles = suggestRoles;
            Xml = xml;
        }

        private void AddRestrictForm_Load(object sender, EventArgs e)
        {
            AutoCompleteStringCollection c = new AutoCompleteStringCollection();
            c.AddRange(this.SuggestProperties);
            txtPropertyName.AutoCompleteCustomSource = c;

            AutoCompleteStringCollection c1 = new AutoCompleteStringCollection();
            c1.AddRange(this.SuggestRoles);
            txtRole.AutoCompleteCustomSource = c1;

            if (Xml != null)
            {
                string type = Xml.GetAttribute("Type");
                if (type.Equals("PropertyEquals",StringComparison.CurrentCulture))
                {
                    rbPropertyEquals.Checked = true;
                    txtPropertyName.Text = Xml.GetAttribute("Property");
                    txtPropertyValue.Text = Xml.GetAttribute("Value");
                }
                else if (type.Equals("RoleContain", StringComparison.CurrentCulture))
                {
                    rbRoleContain.Checked = true;
                    txtRole.Text = Xml.GetAttribute("Role");
                }
                else
                {
                    rbDB.Checked = true;
                    txtSQL.Text = Xml.InnerText.Trim();
                }
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Completed != null)
            {
                XmlElement result = null;                
                RestrictType restrictType = RestrictType.PropertyEquals;

                XmlHelper h = new XmlHelper("<Restrict/>");

                if (rbPropertyEquals.Checked)
                {                    
                    restrictType = RestrictType.PropertyEquals;                                        
                    h.SetAttribute(".", "Property", txtPropertyName.Text);
                    h.SetAttribute(".", "Value", txtPropertyValue.Text);
                }

                if (rbRoleContain.Checked)
                {                    
                    restrictType = RestrictType.RoleContain;
                    h.SetAttribute(".", "Role", txtRole.Text);                  
                }

                if (rbDB.Checked)
                {                    
                    restrictType = RestrictType.SQL;
                    XmlElement element = h.GetElement(".");
                    XmlCDataSection section = element.OwnerDocument.CreateCDataSection(txtSQL.Text);
                    element.AppendChild(section);
                }

                h.SetAttribute(".", "Type", restrictType.ToString());
                result = h.GetElement(".");

                Completed.Invoke(this, new RestrictEventArgs(result, restrictType));
            }
            this.Close();
        }

        private void rbPropertyEquals_CheckedChanged(object sender, EventArgs e)
        {
            this.txtPropertyName.Enabled = true;
            this.txtPropertyValue.Enabled = true;
            this.txtRole.Enabled = false;
            this.txtSQL.Enabled = false;
        }

        private void rbRoleContain_CheckedChanged(object sender, EventArgs e)
        {
            this.txtPropertyName.Enabled = false;
            this.txtPropertyValue.Enabled = false;
            this.txtRole.Enabled = true;
            this.txtSQL.Enabled = false;
        }

        private void rbDB_CheckedChanged(object sender, EventArgs e)
        {
            this.txtPropertyName.Enabled = false;
            this.txtPropertyValue.Enabled = false;
            this.txtRole.Enabled = false;
            this.txtSQL.Enabled = true;
        }
    }

    internal class RestrictEventArgs : EventArgs
    {
        internal XmlElement Result { get; private set; }        
        internal RestrictType RestrictType { get; private set; }

        internal RestrictEventArgs(XmlElement result, RestrictType restrictType)
        {
            Result = result;            
            RestrictType = restrictType;
        }
    }

    internal enum RestrictType
    {
        PropertyEquals, RoleContain, SQL
    }
}
