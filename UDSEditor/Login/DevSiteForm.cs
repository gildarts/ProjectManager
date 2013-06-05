using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FISCA.DSAClient;

namespace ProjectManager.Login
{
    public partial class DevSiteForm : Form
    {
        public DevSiteForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                MainForm.DefaultDevSite.User = txtUser.Text;
                MainForm.DefaultDevSite.Password = txtPassword.Text;
                MainForm.DefaultDevSite.AccessPoint = txtAP.Text;
                MainForm.DefaultDevSite.ContractName = cboContract.Text;

                XmlHelper apXml = new XmlHelper("<AppContent/>");
                apXml.AddElement(".", "AccessPoint", txtAP.Text);
                apXml.AddElement(".", "ContractName", cboContract.Text);
                apXml.AddElement(".", "User", txtUser.Text);
                apXml.AddElement(".", "Password", txtPassword.Text);

                MainForm.Storage.SetPropertyXml("DevLoginAP", txtAP.Text, apXml.GetElement("."));
                MainForm.Storage.SetProperty("DevLastLoginAP", txtAP.Text);
                MainForm.Storage.SetProperty("DevLastLoginContract", cboContract.Text);
                MainForm.Storage.SetProperty("DevLastLoginName", txtUser.Text);
                MainForm.Storage.SetProperty("DevLastLoginPassword", txtPassword.Text);
                MainForm.Storage.SetProperty("DevAutoLogin", chkAutoLogin.Checked.ToString().ToLower());
                
                MainForm.LoginArgs.StaticPreference = apXml.GetElement("."); ;

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DevSiteForm_Load(object sender, EventArgs e)
        {
            AutoCompleteStringCollection c = new AutoCompleteStringCollection();
            c.AddRange(MainForm.Storage.GetPropertyValueNames("DevLoginAP"));
            txtUser.AutoCompleteCustomSource = c;
            
            txtAP.Text = MainForm.Storage.GetProperty("DevLastLoginAP");
            cboContract.Text = MainForm.Storage.GetProperty("DevLastLoginContract");
            txtUser.Text = MainForm.Storage.GetProperty("DevLastLoginName");
            txtPassword.Text = MainForm.Storage.GetProperty("DevLastLoginPassword");
            chkAutoLogin.Checked = (MainForm.Storage.GetProperty("DevAutoLogin") == "true");
        }

        private void txtAP_Leave(object sender, EventArgs e)
        {
            XmlHelper value = MainForm.Storage.GetPropertyXml("DevLoginAP", txtAP.Text);
            if (value == null) return;

            cboContract.Text = value.GetText("ContractName");
            txtUser.Text = value.GetText("User");
            txtPassword.Text = value.GetText("Password");
        }
    }
}
