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

        /// <summary>最後一次設定的本機目錄（任何專案），下次選資料夾從它附近開始</summary>
        private const string LAST_FOLDER = "LastLocalFolder";

        private static void RememberLastFolder(string path)
        {
            try { MainForm.Storage.SetProperty(LAST_FOLDER, path); }
            catch { }
        }

        /// <summary>
        /// 選資料夾的起點：目前填的路徑 → 上次設定的資料夾 → 往上找到還存在的那一層。
        /// 上次的資料夾是別的專案時，從它的上一層開始（通常各專案放在同一個資料夾底下）。
        /// </summary>
        private string GetStartFolder()
        {
            string current = txtLocalPath.Text.Trim();
            if (current.Length > 0)
            {
                string existing = NearestExisting(current);
                if (existing != null) return existing;
            }

            string last = null;
            try { last = MainForm.Storage.GetProperty(LAST_FOLDER); }
            catch { }
            if (string.IsNullOrWhiteSpace(last)) return null;

            string parent = null;
            try { parent = Path.GetDirectoryName(last.TrimEnd('\\', '/')); }
            catch { }
            return NearestExisting(string.IsNullOrEmpty(parent) ? last : parent);
        }

        private static string NearestExisting(string path)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(path);
                while (dir != null && !dir.Exists)
                    dir = dir.Parent;
                return dir == null ? null : dir.FullName;
            }
            catch
            {
                return null;
            }
        }

        private void btnBro_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fd = new FolderBrowserDialog();
            fd.Description = "選擇專案『" + _projectName + "』在本機的資料夾";
            string start = GetStartFolder();
            if (!string.IsNullOrEmpty(start))
                fd.SelectedPath = start;
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

            // 還沒設定過：上次設定的資料夾旁邊剛好有同名資料夾，就直接幫忙填好
            if (string.IsNullOrWhiteSpace(value))
            {
                string start = GetStartFolder();
                if (start != null)
                {
                    string guess = Path.Combine(start, _projectName);
                    if (Directory.Exists(guess))
                        txtLocalPath.Text = guess;
                }
            }

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
            RememberLastFolder(txtLocalPath.Text);

            if (Completed != null)
                Completed.Invoke(this, EventArgs.Empty);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
