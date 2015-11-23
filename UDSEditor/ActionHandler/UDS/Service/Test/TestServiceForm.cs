using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProjectManager.Project.UDS.Service;
using System.IO;
using FISCA.DSAClient;
using System.Xml;
using ProjectManager.ActionHandler.UDS.Service.Test.TempGenerator;
using System.Diagnostics;
using System.Security.Cryptography;

namespace ProjectManager.ActionHandler.UDS.Service.Test
{
    public partial class TestServiceForm : Form
    {
        private SecurityTokenForm _sttForm;
        internal string ServiceName { get; private set; }
        internal string PackageName { get; private set; }
        internal string ContractName { get; private set; }
        internal XmlElement ServiceDefinition { get; private set; }

        private string ServiceFullName { get { return string.Join(".", PackageName, ServiceName); } }

        internal TestServiceForm(string contractName, string packageName, string serviceName, XmlElement definition)
        {
            InitializeComponent();
            ActiproSoftware.SyntaxEditor.SemanticParserService.Start();

            this.ServiceName = serviceName;
            this.PackageName = packageName;
            this.ContractName = contractName;
            this.ServiceDefinition = definition;
        }

        private void TestServiceForm_Load(object sender, EventArgs e)
        {
            string siteURL = Path.Combine(MainForm.CurrentProject.DevSite.AccessPoint, this.ContractName, this.PackageName + "." + this.ServiceName);
            txtSiteURL.Text = MainForm.CurrentProject.DevSite.AccessPoint;
            txtContract.Text = this.ContractName;
            txtService.Text = this.ServiceFullName;
            _sttForm = new SecurityTokenForm(this.ContractName, this.ContractUniqName);

            try
            {
                string dir = Path.Combine(Environment.CurrentDirectory, "TestServiceConfig");
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                string filename = Path.Combine(dir, this.ServiceUniqName);
                if (File.Exists(filename))
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(filename);
                    txtRequest.Text = doc.DocumentElement.OuterXml;
                    txtRequest.FormatXml();
                }
            }
            catch { }
            txtRequest.UserKeyUp += new KeyEventHandler(txtRequest_UserKeyUp);
        }

        void txtRequest_UserKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
                Execute();
        }

        private void tsbtnSend_Click(object sender, EventArgs e)
        {
            Execute();
        }

        private void Execute()
        {
            Connection con = new Connection();
            con.EnableSecureTunnel = cmSSLEnable.Checked;
            SecurityToken stt = null;
            try
            {
                stt = _sttForm.GetSecurityToken();
                con.Connect(txtSiteURL.Text, txtContract.Text, stt); ;
            }
            catch
            {
                DialogResult dr = MessageBox.Show("Service 認證失敗 ! \n 是否開啟認證設定畫面? ", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
                if (dr == System.Windows.Forms.DialogResult.Yes)
                    _sttForm.ShowDialog();
                return;
            }
            Stopwatch w = new Stopwatch();
            w.Start();
            XmlHelper req = new XmlHelper();
            if (!string.IsNullOrWhiteSpace(txtRequest.Text))
                req = XmlHelper.ParseAsHelper(txtRequest.Text);

            XmlHelper h = new XmlHelper();
            try
            {
                Envelope re = new Envelope(req);
                Envelope env = con.SendRequest(txtService.Text, re);
                txtResponse.Text = env.Body.XmlString;

                try
                {
                    h = new XmlHelper(env.Body);
                    txtResponse.Text = h.XmlString;
                    txtRequest.FormatXml();
                }
                catch { }

                txtHeader.Text = env.Headers.XmlString;
                txtHeader.FormatXml();
            }
            catch (DSAServerException ex)
            {
                txtResponse.Text = ex.Response;
                txtResponse.FormatXml();
            }
            catch (Exception ex)
            {
                txtResponse.Text = ex.Message;
            }
            w.Stop();
            tsLabelTime.Text = w.ElapsedMilliseconds.ToString();

            try
            {
                string dir = Path.Combine(Environment.CurrentDirectory, "TestServiceConfig");
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                string filename = Path.Combine(dir, this.ServiceUniqName);
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(txtRequest.Text);
                doc.Save(filename);
            }
            catch { }
        }

        private void cmSSLEnable_Click(object sender, EventArgs e)
        {
            this.cmSSLEnable.Checked = true;
            this.cmSSLDisable.Checked = false;
        }

        private void cmSSLDisable_Click(object sender, EventArgs e)
        {
            this.cmSSLEnable.Checked = false;
            this.cmSSLDisable.Checked = true;
        }

        private void cmAuth_Click(object sender, EventArgs e)
        {
            _sttForm.StartPosition = FormStartPosition.CenterParent;
            _sttForm.ShowDialog();
        }

        private void cmTemp_Click(object sender, EventArgs e)
        {
            txtRequest.Text = TempGenProvider.GenerateTemp(this.ServiceDefinition);
        }

        internal string ContractUniqName
        {
            get
            {
                return txtSiteURL.Text + "/" + txtContract.Text;
            }
        }

        internal string ServiceUniqName
        {
            get
            {
                string str = txtSiteURL.Text + "/" + txtContract.Text + this.ServiceFullName;
                byte[] Original = Encoding.Default.GetBytes(str); //將字串來源轉為Byte[] 
                MD5 s1 = MD5.Create(); //使用MD5 
                byte[] Change = s1.ComputeHash(Original);//進行加密 
                return BitConverter.ToString(Change).Replace("-", string.Empty) + ".xml";
            }
        }

        private void growRequest_Click(object sender, EventArgs e)
        {
            var style = mainLayout.RowStyles[0];

            mainLayout.RowStyles.Clear();

            float reqSize = style.Height + 20;

            if (reqSize >= 100)
                reqSize = 100;

            float rspSize = 100 - reqSize;

            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, reqSize > 0 ? reqSize : 0));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, rspSize > 0 ? rspSize : 0));
        }

        private void growResponse_Click(object sender, EventArgs e)
        {
            var style = mainLayout.RowStyles[0];

            mainLayout.RowStyles.Clear();

            float reqSize = style.Height - 20;

            if (reqSize <= 0)
                reqSize = 0;

            float rspSize = 100 - reqSize;

            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, reqSize > 0 ? reqSize : 0));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, rspSize > 0 ? rspSize : 0));
        }
    }
}
