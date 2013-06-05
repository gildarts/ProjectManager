using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FISCA.DSAClient;
using System.IO;
using ProjectManager.Login;
using System.Xml;

namespace ProjectManager
{
    public partial class LoadProjectFromFileForm : Form
    {
        public LoadProjectFromFileForm()
        {
            InitializeComponent();
        }

        private void chkUseDefault_CheckedChanged(object sender, EventArgs e)
        {
            this.panel1.Enabled = !chkUseDefault.Checked;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBro_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "*.pml|*.pml";
            fd.FileName = string.Empty;
            DialogResult dr =  fd.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
                txtFileName.Text = fd.FileName;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            err.Clear();

            if (string.IsNullOrWhiteSpace(txtProjectName.Text))
                err.SetError(txtProjectName, "專案名稱不可空白");

            if (MainForm.Projects.Contains(txtProjectName.Text))
                err.SetError(txtProjectName, "專案名稱已被使用");

            if (string.IsNullOrEmpty(txtFileName.Text))
                err.SetError(btnBro, "請選擇檔案");

            if(!File.Exists(txtFileName.Text))
                err.SetError(btnBro, "檔案不存在");
            
            if (!chkUseDefault.Checked)
            {
                if (string.IsNullOrWhiteSpace(txtAP.Text))
                    err.SetError(txtAP, "開發站不可空白");

                if (string.IsNullOrWhiteSpace(txtUser.Text))
                    err.SetError(txtUser, "帳號不可空白");

                if (string.IsNullOrWhiteSpace(txtPwd.Text))
                    err.SetError(txtPwd, "密碼不可空白");
            }

            foreach (Control ctrl in this.Controls)
            {
                if (string.IsNullOrEmpty(err.GetError(ctrl))) continue;
                return;
            }

            try
            {
                if (chkUseDefault.Checked)
                    MainForm.DefaultDevSite.TryConnect();
                else
                {
                    Connection connection = new Connection();
                    connection.Connect(txtAP.Text, "admin", txtUser.Text, txtPwd.Text);
                }
            }
            catch (Exception ex)
            {
                err.SetError(txtAP, "登入失敗 : " + ex.Message);
                return;
            }
            
            DevSiteLoginInfo info = new DevSiteLoginInfo();

            if (chkUseDefault.Checked)
                info = MainForm.DefaultDevSite;
            else
            {
                info.AccessPoint = txtAP.Text;
                info.User = txtUser.Text;
                info.Password = txtPwd.Text;
            }

            XmlDocument doc = new XmlDocument();
            doc.Load(txtFileName.Text);

            try
            {
                MainForm.Projects.LoadProject(txtProjectName.Text, info, rbCheck.Checked, doc.DocumentElement);
            }
            catch (Exception ex)
            {
                MessageBox.Show("載入失敗 : \n" + ex.Message, "失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.Close();
        }
    }
}
