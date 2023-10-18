using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FISCA.DSAClient;
using System.Xml;
using System.IO;
using ProjectManager.Login.Security;

namespace ProjectManager.Login
{
    public partial class LoginForm : Form
    {
        private const string USE_KEY = "prometheus.lie@gmail.com";

        internal event EventHandler<LoginEventArgs> Logined;

        public LoginForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Login();
        }

        private void txtUser_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            Login();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Login()
        {
            string greeningURL = "http://10.1.1.163/greening2/shared";
            string moduleURL = "http://10.1.1.163/modules/shared";

            XmlDocument doc = new XmlDocument();
            string filename = Path.Combine(Environment.CurrentDirectory, "Setup.config");
            this.ShowMessage("載入設定檔");

            if (File.Exists(filename))
            {
                doc.Load(filename);
                XmlHelper config = new XmlHelper(doc.DocumentElement);
                greeningURL = config.GetText("GreeningAccessPoint");
                moduleURL = config.GetText("ModuleAccessPoint");
            }

            try
            {
                this.ShowMessage("登入 ischool ....");
                Connection greenCon = new Connection();
                greenCon.EnableSecureTunnel = true;
                greenCon.EnableSession = false;
                try 
                {
                    greenCon.Connect(greeningURL, "user", txtUser.Text.Trim(), txtPassword.Text);
                }
                catch (Exception ex)
                {
                    throw new Exception("ischool Account 認證失敗" + ex.Message);
                }

                Envelope rsp = greenCon.SendRequest("DS.Base.GetPassportToken", new Envelope());
                PassportSecurityToken stt = new PassportSecurityToken(rsp.Body.XmlString);

                XmlHelper h1 = new XmlHelper(rsp.Body);
                string id = h1.GetText("Content/Attributes/ID");

                string ftpUser = string.Empty;
                string ftpPassword = string.Empty;
                string ftpUrl = string.Empty;
                bool succeedLoginModuleService = false;
                Connection modCon = null;
                try
                {
                    this.ShowMessage("載入線上儲存空間設定...");

                    modCon = new Connection();
                    modCon.EnableSession = true;
                    modCon.EnableSecureTunnel = true;

                    modCon.Connect(moduleURL, "developer", stt);

                    Envelope env = modCon.SendRequest("GetFTPInfo", new Envelope());
                    XmlHelper h = new XmlHelper(env.Body);
                    ftpUser = TripleDESHelper.Decrypt(h.GetText("User"), USE_KEY);
                    ftpPassword = TripleDESHelper.Decrypt(h.GetText("Password"), USE_KEY);
                    ftpUrl = TripleDESHelper.Decrypt(h.GetText("FTP"), USE_KEY);
                    succeedLoginModuleService = true;
                }
                catch(Exception ex )
                {
                    this.ShowMessage("載入失敗！" + ex.Message);
                    succeedLoginModuleService = false;
                }
                string pwd = string.Empty;
                if (chkRemember.Checked)
                    pwd = txtPassword.Text;
                MainForm.Storage.SetPropertyValues("LoginName", txtUser.Text, pwd);
                MainForm.Storage.SetProperty("LastLoginName", txtUser.Text);
                MainForm.Storage.SetProperty("LastLoginPassword", pwd);
                MainForm.Storage.SetProperty("RememberPassword", chkRemember.Checked.ToString().ToLower());

                if (Logined != null)
                {
                    this.ShowMessage("登入開發站台...");
                    LoginEventArgs arg = new LoginEventArgs();
                    arg.FtpPassword = ftpPassword;
                    arg.FtpUser = ftpUser;
                    arg.FtpURL = ftpUrl;
                    arg.GreeningConnection = greenCon;
                    arg.ModuleConnection = modCon;
                    arg.SetModuleConnectionInfo(moduleURL);
                    arg.LoginUser = txtUser.Text;
                    arg.GreeningID = id;
                    arg.SucceedModuleLogin = succeedLoginModuleService;

                    this.ShowMessage("載入開發站台資訊");
                    Logined.Invoke(this, arg);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.ShowMessage("※請輸入 ischool Account 密碼");
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            AutoCompleteStringCollection c = new AutoCompleteStringCollection();
            c.AddRange(MainForm.Storage.GetPropertyValueNames("LoginName"));
            txtUser.AutoCompleteCustomSource = c;

            txtUser.Text = MainForm.Storage.GetProperty("LastLoginName");
            txtPassword.Text = MainForm.Storage.GetProperty("LastLoginPassword");
            chkRemember.Checked = (MainForm.Storage.GetProperty("RememberPassword") == "true");
        }

        private void txtUser_Leave(object sender, EventArgs e)
        {
            string value = MainForm.Storage.GetPropertyValue("LoginName", txtUser.Text);
            if (string.IsNullOrWhiteSpace(value)) return;

            txtPassword.Text = value;
        }

        private void lnkSetup_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SetupConfigForm setupForm = new SetupConfigForm();
            setupForm.StartPosition = FormStartPosition.CenterParent;
            setupForm.ShowDialog();
        }

        private void ShowMessage(string msg)
        {
            this.lblInfo.Text = msg;
            Application.DoEvents();
        }
    }
}
