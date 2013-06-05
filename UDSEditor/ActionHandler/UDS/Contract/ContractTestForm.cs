using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProjectManager.Project.UDS.Contract;
using FISCA.DSAClient;
using ProjectManager.ActionHandler.UDS.Service.Test;

namespace ProjectManager.ActionHandler.UDS.Contract
{
    public partial class ContractTestForm : Form
    {
        private ContractHandler _contract;

        internal ContractTestForm(ContractHandler contract)
        {
            InitializeComponent();
            _contract = contract;
        }

        private void ContractTestForm_Load(object sender, EventArgs e)
        {
            txtContract.Text = _contract.Name;
            txtSiteURL.Text = MainForm.CurrentProject.DevSite.AccessPoint;
          
        }

        private void chkPassport_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPassport.Checked)
                txtProvider.Enabled = true;
            else
                txtProvider.Enabled = false;
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            txtInfo.Text = string.Empty;
            txtUserInfo.Text = string.Empty;
            txtPassport.Text = string.Empty;

            Connection con = new Connection();
            con.EnableSession = false;
            con.EnableSecureTunnel = true;
            SecurityToken stt = null;
            try
            {
                stt = this.GetSecurityToken();
                con.Connect(MainForm.CurrentProject.DevSite.AccessPoint, _contract.Name, stt);
                txtInfo.Text = "連線成功";                
            }
            catch (Exception ex)
            {
                txtInfo.Text = "連線失敗 : \n" + ex.Message;
                return;
            }
                        
            try
            {
                Envelope env = con.SendRequest("DS.Base.Connect", new Envelope());
                txtUserInfo.Text = env.Headers["UserInfo"];
                xmlSyntaxLanguage1.FormatDocument(txtUserInfo.Document);
            }
            catch 
            {               
            }
        }

        internal SecurityToken GetSecurityToken()
        {
            if (!chkPassport.Checked)
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
                txtPassport.Text = rsp.Body.XmlString;
                xmlSyntaxLanguage1.FormatDocument(txtPassport.Document);
                PassportSecurityToken stt = new PassportSecurityToken(rsp.Body.XmlString);
                return stt;
            }
        }
    }
}
