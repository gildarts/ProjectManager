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
using ProjectManager.Login.Security;
using System.Globalization;

namespace ProjectManager.Project.Proxy
{
    public partial class EditProxyDeployForm : Form
    {
        internal event EventHandler Completed;
        private XmlElement _siteElement;

        public EditProxyDeployForm()
        {
            InitializeComponent();
        }

        public EditProxyDeployForm(XmlElement siteElement)
        {
            _siteElement = siteElement;
            InitializeComponent();
        }

        private void EditProxyDeployForm_Load(object sender, EventArgs e)
        {
            if (_siteElement == null) return;

            XmlHelper h = new XmlHelper(_siteElement);

            txtSiteName.Text = _siteElement.GetAttribute("Name");
            txtAccessPoint.Text = h.GetText("AccessPoint");
            txtContract.Text = h.GetText("Contract");
            string authType = h.GetText("Authentication/@Type").ToLower();

            if (authType == "basic")
            {
                rbBasic.Checked = true;
                txtBasicAccount.Text = h.GetText("Authentication/UserName");
                txtBasicPassword.Text = h.GetText("Authentication/Password");
            }
            else
            {
                rbPassport.Checked = true;

                txtIssuer.Text = h.GetText("Authentication/Issuer");
                txtIssuerAccount.Text = h.GetText("Authentication/UserName");
                txtIssuerPassword.Text = h.GetText("Authentication/Password");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!Valid())
            {
                MessageBox.Show("設定資料無法連線", "失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (Completed != null)
                Completed.Invoke(this, EventArgs.Empty);

            this.Close();
        }

        private bool Valid()
        {
            bool valid = true;
            err.Clear();

            if (string.IsNullOrWhiteSpace(txtSiteName.Text))
            {
                err.SetError(txtSiteName, "不可空白");
                valid = false;
            }

            if (string.IsNullOrWhiteSpace(txtAccessPoint.Text))
            {
                err.SetError(txtAccessPoint, "不可空白");
                valid = false;
            }

            try
            {
                ConvertDSNS(txtAccessPoint.Text);
                Test();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("安全通道"))
                {
                    err.SetError(txtAccessPoint, ex.Message);
                }
                else if (rbBasic.Checked)
                {
                    if (ex.Message.Contains("帳號"))
                        err.SetError(txtBasicAccount, ex.Message);
                    if (ex.Message.Contains("密碼"))
                        err.SetError(txtBasicPassword, ex.Message);
                    else
                        err.SetError(rbBasic, ex.Message);
                }
                else
                {
                    err.SetError(rbPassport, ex.Message);
                }
                
                valid = false;
            }

            return valid;
        }

        internal XmlElement GetResultXml()
        {
            XmlHelper h = new XmlHelper("<ProxySite />");
            h.SetAttribute(".", "Name", txtSiteName.Text);
            h.AddElement(".", "AccessPoint", txtAccessPoint.Text);
            h.AddElement(".", "Contract", txtContract.Text);
            h.AddElement(".", "Authentication");

            if (rbBasic.Checked)
            {
                h.SetAttribute("Authentication", "Type", "Basic");
                h.AddElement("Authentication", "UserName", txtBasicAccount.Text);
                h.AddElement("Authentication", "Password", txtBasicPassword.Text);
            }
            else
            {
                h.SetAttribute("Authentication", "Type", "Passport");
                h.AddElement("Authentication", "Issuer", txtIssuer.Text);
                h.AddElement("Authentication", "UserName", txtIssuerAccount.Text);
                h.AddElement("Authentication", "Password", txtIssuerPassword.Text);
            }
            
            return h.GetElement(".");
        }

        private void CheckedChanged(object sender, EventArgs e)
        {
            pnlBasic.Enabled = false;
            pnlPassport.Enabled = false;

            if (rbBasic.Checked)
                pnlBasic.Enabled = true;
            else
                pnlPassport.Enabled = true;
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            try
            {
                this.Test();
                MessageBox.Show("驗證成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Test()
        {
            string url = this.ConvertDSNS(txtAccessPoint.Text);
            
            Connection con = new Connection();
            con.EnableSecureTunnel = true;

            SecurityToken st;
            if (rbBasic.Checked)
            {
                st = new BasicSecurityToken(txtBasicAccount.Text, txtBasicPassword.Text);
            }
            else
            {
                Connection c = new Connection();
                c.EnableSecureTunnel = true;
                c.Connect(txtIssuer.Text, "", txtIssuerAccount.Text, txtIssuerPassword.Text);
                Envelope env = c.SendRequest("DS.Base.GetPassportToken", new Envelope());                
                st = new PassportSecurityToken(env.Body.XmlString);
            }

            con.Connect(url, txtContract.Text, st);
        }

        private string ConvertDSNS(string name)
        {
            if(name.StartsWith("http://",true,CultureInfo.CurrentCulture))
                return name;

            if(name.StartsWith("https://",true,CultureInfo.CurrentCulture))
                return name;

            Connection con = new Connection();
            con.EnableSession = false;
            con.EnableSecureTunnel = false;
            con.Connect("http://dsns1.ischool.com.tw/dsns/", "dsns", new PublicSecurityToken());

            XmlHelper h = new XmlHelper("<a>" + name + "</a>");
            Envelope env = con.SendRequest("DS.NameService.GetDoorwayURL", new Envelope(h));
            h = new XmlHelper(env.Body);
            string value = h.GetText(".");
            if (string.IsNullOrWhiteSpace(value))
                throw new Exception("DSNS 名稱解析失敗");
            return value;
        }
    }
}
