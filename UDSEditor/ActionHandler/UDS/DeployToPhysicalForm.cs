using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProjectManager.Util.DeployConverter;
using System.Xml;
using System.Xml.Linq;
using FISCA.DSAClient;

namespace ProjectManager.ActionHandler.UDS
{
    public partial class DeployToPhysicalForm : Form
    {
        private UDSNodeHandler _nodeHandler;

        internal DeployToPhysicalForm(UDSNodeHandler nodeHandler)
        {
            _nodeHandler = nodeHandler;
            InitializeComponent();
        }

        private void CheckedChanged(object sender, EventArgs e)
        {
            if (rbToFile.Checked)
            {
                pnlToFile.Enabled = true;
                pnlToSite.Enabled = false;
            }
            else
            {
                pnlToFile.Enabled = false;
                pnlToSite.Enabled = true;
            }
        }

        private void btnBrowser_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "XML檔案(*.xml)|*.xml";
            sfd.FileName = "AppDeploy.xml";
            DialogResult dr = sfd.ShowDialog();
            if (dr != System.Windows.Forms.DialogResult.OK) return;

            txtPath.Text = sfd.FileName;          
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            if (rbToFile.Checked)
                SaveToFile();
            else
                SaveToSite();
        }

        private XElement GetAppDeployElement()
        {
            XmlElement element = _nodeHandler.GetContractsXml();
            XElement xe = XElement.Parse(element.OuterXml);

            return DeployConverter.ToPhysicalDeployElement(xe);
        }

        private void SaveToFile()
        {
            err.Clear();
            if (string.IsNullOrWhiteSpace(txtPath.Text))
            {
                err.SetError(btnBrowser, "必須指定儲存位置");
                return;
            }
            try
            {              
                XElement element = GetAppDeployElement();
                element.Save(txtPath.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("匯出時發生錯誤\n" + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void SaveToSite()
        {
            err.Clear();
            if (string.IsNullOrWhiteSpace(txtAccessPoint.Text))
            {
                err.SetError(txtAccessPoint, "主機位置不可空白");
                return;
            }

            XElement result = GetAppDeployElement();

            Connection con = new Connection();
            try
            {
                con.Connect(txtAccessPoint.Text, txtContract.Text, txtUserName.Text, txtPassword.Text);
                XmlHelper h = new XmlHelper(result.ToString(SaveOptions.None));
                con.SendRequest("Server.DeployApplication", new Envelope(h));
                con.SendRequest("LoadBalance.ReloadServer", new Envelope());
            }
            catch (Exception ex)
            {
                MessageBox.Show("部署時發生錯誤\n" + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show("儲存完成", "完成", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
