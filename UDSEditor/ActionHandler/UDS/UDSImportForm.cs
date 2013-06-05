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

namespace ProjectManager.ActionHandler.UDS
{
    public partial class UDSImportForm : Form
    {
        private UDSNodeHandler _udsNodeHandler;
        private string FILE_EXT = "csml";

        internal UDSImportForm(UDSNodeHandler udsNodeHandler)
        {
            InitializeComponent();
            _udsNodeHandler = udsNodeHandler;
        }

        private void rbTSML_CheckedChanged(object sender, EventArgs e)
        {
            txtFileName.Text = string.Empty;
            if (rbTML.Checked)
            {
                groupBox1.Enabled = false;
                FILE_EXT = "cml";
            }
            else
            {
                groupBox1.Enabled = true;
                FILE_EXT = "csml";
            }
        }

        private void btnBro_Click(object sender, EventArgs e)
        {
            OpenFileDialog sd = new OpenFileDialog();
            sd.DefaultExt = FILE_EXT;
            sd.Filter = "*." + FILE_EXT + "|*." + FILE_EXT;
            DialogResult dr = sd.ShowDialog();

            if (dr == System.Windows.Forms.DialogResult.OK)
                txtFileName.Text = sd.FileName;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            err.Clear();
            if (string.IsNullOrWhiteSpace(txtFileName.Text))
            {
                err.SetError(btnBro, "請選擇檔案");
                return;
            }

            if (!File.Exists(txtFileName.Text))
            {
                err.SetError(btnBro, "檔案不存在");
                return;
            }

            FileInfo file = new FileInfo(txtFileName.Text);
            if (!file.Extension.Equals("." + FILE_EXT, StringComparison.CurrentCultureIgnoreCase))
            {
                err.SetError(btnBro, "檔案類型不正確");
                return;
            }

            XmlDocument xml = new XmlDocument();
            try
            {
                xml.Load(txtFileName.Text);
            }
            catch (Exception ex)
            {
                err.SetError(btnBro, "檔案類型不正確 : \n" + ex.Message);
                return;
            }

            try
            {
                if (rbTSML.Checked)
                    _udsNodeHandler.UDSHandler.ImportContracts(xml.DocumentElement, rbImport.Checked);
                else
                    _udsNodeHandler.UDSHandler.ImportContract(xml.DocumentElement);
            }
            catch (Exception ex)
            {
                MessageBox.Show("匯入發生錯誤 : \n" + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.Close();
        }
    }
}
