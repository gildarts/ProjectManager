using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using ProjectManager.Util.DeployConverter;
using FISCA.DSAClient;

namespace ProjectManager.ActionHandler.UDS
{
    public partial class ImportFromPhysicalForm : Form
    {
        private UDSNodeHandler _udsNodeHandler;

        internal ImportFromPhysicalForm(UDSNodeHandler uDSNodeHandler)
        {
            this._udsNodeHandler = uDSNodeHandler;
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

        private void btnExecute_Click(object sender, EventArgs e)
        {
            if (rbToFile.Checked)
                ImportFromFile();
            else
                ImportFromSite();
        }

        private void ImportFromFile()
        {
            err.Clear();
            if (string.IsNullOrWhiteSpace(txtPath.Text))
            {
                err.SetError(btnBrowser, "路徑不可空白");
                return;
            }

            if (!File.Exists(txtPath.Text))
            {
                err.SetError(btnBrowser, "檔案不存在");
                return;
            }
            XmlDocument doc = new XmlDocument();
            doc.Load(txtPath.Text);
            this.Import(doc.DocumentElement);
        }

        private void ImportFromSite()
        {
            err.Clear();
            if (string.IsNullOrWhiteSpace(txtAccessPoint.Text))
            {
                err.SetError(txtAccessPoint, "主機位置不可空白");
                return;
            }

            Connection connection = new Connection();
            connection.EnableSecureTunnel= true;
            try
            {
                connection.Connect(txtAccessPoint.Text, txtContract.Text, txtUserName.Text, txtPassword.Text);
            }
            catch (Exception ex)
            {
                err.SetError(txtAccessPoint, "主機連線失敗 : \n" + ex.Message);
                return;
            }

            XmlHelper h = new XmlHelper();
            h.AddElement(".","ApplicationName","shared");
            Envelope env = new Envelope(h);
            
            env = connection.SendRequest("Server.ExportApplication", env);
            h = new XmlHelper(env.Body);
            Console.WriteLine(h.XmlString);
            this.Import(h.GetElement("."));
        }

        private void btnBrowser_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "XML 檔案(*.xml)|*.xml";
            DialogResult dr = ofd.ShowDialog();
            if (dr != System.Windows.Forms.DialogResult.OK) return;

            txtPath.Text = ofd.FileName;
        }

        private void Import(XmlElement element)
        {
            try
            {
                XElement xe = XElement.Parse(element.OuterXml);
                XElement nxe = DeployConverter.ToUDSDeployElement(xe);

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(nxe.ToString());
                element = doc.DocumentElement;

                _udsNodeHandler.UDSHandler.ImportContracts(element, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("匯入時發生錯誤\n" + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show("匯入完成", "完成", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
