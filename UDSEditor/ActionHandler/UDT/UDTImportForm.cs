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

namespace ProjectManager.ActionHandler.UDT
{
    public partial class UDTImportForm : Form
    {
        private string FILE_EXT = "tsml";
        private UDTNodeHandler _udtNodeHandler;

        internal UDTImportForm(UDTNodeHandler udtNodeHandler)
        {
            InitializeComponent();
            _udtNodeHandler = udtNodeHandler;
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
                if(rbTSML.Checked)
                    _udtNodeHandler.UDTHandler.SetTables(xml.DocumentElement, rbImport.Checked);
                else
                    _udtNodeHandler.UDTHandler.SetTable(xml.DocumentElement);
            }
            catch (Exception ex)
            {
                MessageBox.Show("匯入發生錯誤 : \n" + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.Close();
        }

        private void rbTML_CheckedChanged(object sender, EventArgs e)
        {
            txtFileName.Text = string.Empty;
            if (rbTML.Checked)
            {
                groupBox1.Enabled = false;
                FILE_EXT = "tml";                
            }
            else
            {
                groupBox1.Enabled = true;
                FILE_EXT = "tsml";
            }
        }
    }
}
