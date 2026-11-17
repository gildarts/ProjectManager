using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ProjectManager.Project
{
    public partial class SetLocalPathForm : Form
    {
        internal const string LOCAL_FILE_PATH = "LocalFilePath";

        internal event EventHandler Completed;

        private string _projectName;
        public SetLocalPathForm(string projectName)
        {
            InitializeComponent();
            _projectName = projectName;
            SetupCancel();
        }

        /// <summary>
        /// 原本的畫面沒有取消，只能選好路徑才關得掉；補上取消按鈕、右上角 X 與 Esc。
        /// </summary>
        private void SetupCancel()
        {
            this.ControlBox = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            Button btnCancel = new Button();
            btnCancel.Text = "取消";
            btnCancel.Size = btnOK.Size;
            btnCancel.Location = btnOK.Location;
            btnCancel.Font = btnOK.Font;
            btnCancel.DialogResult = DialogResult.Cancel;
            this.Controls.Add(btnCancel);

            btnOK.Left = btnCancel.Left - btnOK.Width - 6;

            this.AcceptButton = btnOK;
            this.CancelButton = btnCancel;
        }

        private void btnBro_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fd = new FolderBrowserDialog();
            fd.Description = "選擇專案『" + _projectName + "』在本機的資料夾";
            if (Directory.Exists(txtLocalPath.Text))
                fd.SelectedPath = txtLocalPath.Text;
            DialogResult dr =  fd.ShowDialog(this);
            if (dr == DialogResult.OK)
            {
                txtLocalPath.Text = fd.SelectedPath;
            }
        }

        private void SetLocalPathForm_Load(object sender, EventArgs e)
        {
            string value = MainForm.Storage.GetPropertyValue(LOCAL_FILE_PATH, _projectName);
            txtLocalPath.Text = value;
            this.Text = "設定專案檔案路徑 ( 專案 : " + _projectName + " )";

              if (!Directory.Exists(txtLocalPath.Text))
                err.SetError(btnBro, "路徑不存在");
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            bool valid = true;
            err.Clear();
            if (!Directory.Exists(txtLocalPath.Text)){
                err.SetError(btnBro, "路徑不存在");
                valid = false;
            }
            if (!valid) return;

            MainForm.Storage.SetPropertyValues(LOCAL_FILE_PATH, _projectName, txtLocalPath.Text);

            if (Completed != null)
                Completed.Invoke(this, EventArgs.Empty);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
