using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FISCA.DSAClient;
using ProjectManager.Project.UDS.Service;
using ProjectManager.Project.UDS.Contract;

namespace ProjectManager.ActionHandler.UDS.Service.Test
{
    public partial class SecurityTokenForm : Form
    {
        internal SecurityToken SecurityToken { get; private set; }
                
        //private ContractHandler _contract;
        private string _contractName;
        private string _contractUniqName;
        private bool _inited = false;

        internal SecurityTokenForm(string contractName, string contractUniqName)
        {
            InitializeComponent();
            _contractName = contractName;
            _contractUniqName = contractUniqName;
        }

        private void rbBasic_CheckedChanged(object sender, EventArgs e)
        {
            CheckedChanged();
        }

        private void rbPassport_CheckedChanged(object sender, EventArgs e)
        {
            CheckedChanged();
        }

        private void CheckedChanged()
        {
            label2.Enabled = rbPassport.Checked;
            txtProvider.Enabled = rbPassport.Checked;
        }

        private void SecurityTokenForm_Load(object sender, EventArgs e)
        {
            XmlHelper xml = MainForm.Storage.GetPropertyXml("ServiceTestTemp", _contractUniqName);
            if (xml == null)
            {
                rbPassport.Checked = true;
                txtUser.Text = MainForm.CurrentProject.DevSite.User;
                txtPwd.Text = MainForm.CurrentProject.DevSite.Password;
                txtProvider.Enabled = true;
            }
            else
            {
                rbPassport.Checked = xml.TryGetBoolean("UsePassport", false);
                txtUser.Text = xml.GetText("User");
                txtPwd.Text = xml.GetText("Password");
                txtProvider.Text = xml.GetText("Auth");
            }
            _inited = true;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            XmlHelper h = new XmlHelper();
            h.AddElement(".", "UsePassport", rbPassport.Checked.ToString());
            h.AddElement(".", "User", txtUser.Text);
            h.AddElement(".", "Password", txtPwd.Text);
            h.AddElement(".", "Auth", txtProvider.Text);
            MainForm.Storage.SetPropertyXml("ServiceTestTemp", _contractUniqName, h.GetElement("."));
            this.Hide();
        }

        internal SecurityToken GetSecurityToken()
        {
            if (_inited)
            {
                if (rbBasic.Checked)
                {
                    return new BasicSecurityToken(txtUser.Text, txtPwd.Text);
                }
                else
                {
                    Connection greenCon = new Connection();
                    greenCon.EnableSecureTunnel = true;
                    greenCon.EnableSession = true;
                    greenCon.Connect(txtProvider.Text, string.Empty, txtUser.Text.Trim(), txtPwd.Text);

                    Envelope rsp = greenCon.SendRequest("DS.Base.GetPassportToken", new Envelope());
                    PassportSecurityToken stt = new PassportSecurityToken(rsp.Body.XmlString);
                    return stt;
                }
            }
            else
            {
                XmlHelper xml = MainForm.Storage.GetPropertyXml("ServiceTestTemp", _contractUniqName);
                if (xml == null)
                {
                    return new BasicSecurityToken(MainForm.CurrentProject.DevSite.User, MainForm.CurrentProject.DevSite.Password);
                }
                else
                {
                    bool passport = xml.TryGetBoolean("UsePassport", false);
                    if (passport)
                    {
                        Connection greenCon = new Connection();
                        greenCon.EnableSecureTunnel = true;
                        greenCon.EnableSession = true;
                        greenCon.Connect(xml.GetText("Auth"), string.Empty, xml.GetText("User"), xml.GetText("Password"));

                        Envelope rsp = greenCon.SendRequest("DS.Base.GetPassportToken", new Envelope());
                        return new PassportSecurityToken(rsp.Body.XmlString);
                    }
                    else
                    {
                        return new BasicSecurityToken(xml.GetText("User"), xml.GetText("Password"));
                    }
                }
            }
        }

        private void SecurityTokenForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            Connection con = new Connection();
            con.EnableSecureTunnel = true;
            SecurityToken stt = null;
            try
            {
                stt = this.GetSecurityToken();
                con.Connect(MainForm.CurrentProject.DevSite.AccessPoint, _contractName, stt);
                MessageBox.Show("測試成功",  "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);         
            }
            catch(Exception ex)
            {
                MessageBox.Show("無法登入 :  \n" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);                
                return;
            }
        }

        private void txtUser_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPwd_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtProvider_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }


    }
}
