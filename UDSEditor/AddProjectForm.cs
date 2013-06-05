using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FISCA.DSAClient;
using ProjectManager.Login;
using System.DirectoryServices;
using System.IO;
using System.Text.RegularExpressions;
using ProjectManager.Project;

namespace ProjectManager
{
    public partial class AddProjectForm : Form
    {
        //public event EventHandler ProjectAdded;

        internal string ProjectName { get; private set; }
        internal DevSiteLoginInfo DevSite { get; private set; }

        public AddProjectForm()
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

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            err.Clear();

            if (string.IsNullOrWhiteSpace(txtProjectName.Text))
                err.SetError(txtProjectName, "專案名稱不可空白");
            else if (!ProjectHandler.ValidName(txtProjectName.Text))
                err.SetError(txtProjectName, "專案名稱必須為英文或數字組成");

            if (MainForm.Projects.Contains(txtProjectName.Text))
                err.SetError(txtProjectName, "專案名稱已被使用");

            if (!chkUseDefault.Checked)
            {
                if (string.IsNullOrWhiteSpace(txtAP.Text))
                    err.SetError(txtAP, "開發站不可空白");

                if (string.IsNullOrWhiteSpace(txtUser.Text) && cboContract.Text != "_dev")
                    err.SetError(txtUser, "帳號不可空白");

                if (string.IsNullOrWhiteSpace(txtPwd.Text) && cboContract.Text != "_dev")
                    err.SetError(txtPwd, "密碼不可空白");
            }

            if (string.IsNullOrWhiteSpace(txtProjectPath.Text))
                err.SetError(txtProjectPath, "專案本機檔案路徑不可空白");
            else if (!Directory.Exists(txtProjectPath.Text))
                err.SetError(txtProjectPath, "指定的本機檔案路徑不存在");

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
                    connection.Connect(txtAP.Text, cboContract.Text, txtUser.Text, txtPwd.Text);
                }
            }
            catch (Exception ex)
            {
                if (chkUseDefault.Checked)
                    err.SetError(chkUseDefault, "登入失敗 : " + ex.Message);
                else
                    err.SetError(txtAP, "登入失敗 : " + ex.Message);
                return;
            }

            DevSiteLoginInfo info = new DevSiteLoginInfo();

            if (chkUseDefault.Checked)
                info = MainForm.DefaultDevSite;
            else
            {
                info.AccessPoint = txtAP.Text;
                info.ContractName = cboContract.Text;
                info.User = txtUser.Text;
                info.Password = txtPwd.Text;
            }
            MainForm.Projects.AddProject(txtProjectName.Text, info, txtProjectPath.Text);

            this.Close();
        }

        private void btnFileDir_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fd = new FolderBrowserDialog();
            DialogResult dr = fd.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                txtProjectPath.Text = fd.SelectedPath;
            }
        }

        private void AddProjectForm_Load(object sender, EventArgs e)
        {
            if (MainForm.DefaultDevSite != null)
            {
                txtAP.Text = MainForm.DefaultDevSite.AccessPoint;
                cboContract.Text = MainForm.DefaultDevSite.ContractName;
                txtUser.Text = MainForm.DefaultDevSite.User;
                txtPwd.Text = MainForm.DefaultDevSite.Password;
            }
        }
    }
}
