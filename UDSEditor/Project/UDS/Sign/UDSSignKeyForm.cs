using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace ProjectManager.Project.UDS.Sign
{
    /// <summary>
    /// 設定 UDS 簽章 key 的畫面。版面直接用程式碼排，沒有設計工具檔。
    /// </summary>
    class UDSSignKeyForm : Form
    {
        private TextBox txtKey;
        private CheckBox chkShow;
        private Label lblStatus;
        private Button btnTest;
        private Button btnOK;
        private Button btnCancel;

        /// <summary>
        /// 顯示設定畫面；使用者設定好有效的 key 回傳 true，取消回傳 false。
        /// </summary>
        internal static bool Prompt(string message)
        {
            using (UDSSignKeyForm form = new UDSSignKeyForm(message))
            {
                return form.ShowDialog() == DialogResult.OK;
            }
        }

        private UDSSignKeyForm(string message)
        {
            this.Text = "設定 UDS 簽章 key";
            this.Font = new Font("微軟正黑體", 9F);
            this.AutoScaleMode = AutoScaleMode.None;
            this.ClientSize = new Size(540, 250);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            Label lblMessage = new Label();
            lblMessage.Text = message;
            lblMessage.Location = new Point(16, 14);
            lblMessage.Size = new Size(508, 40);
            this.Controls.Add(lblMessage);

            LinkLabel lnkKeyhub = new LinkLabel();
            lnkKeyhub.Text = "到金鑰中心登入並產生「UDS 簽章 key」（ks_ 開頭，最長 7 天）";
            lnkKeyhub.Location = new Point(16, 58);
            lnkKeyhub.AutoSize = true;
            lnkKeyhub.LinkClicked += delegate
            {
                try { Process.Start(UDSSigner.KeyhubUrl + "/"); }
                catch (Exception ex) { SetStatus("無法開啟瀏覽器：" + ex.Message, true); }
            };
            this.Controls.Add(lnkKeyhub);

            Label lblKey = new Label();
            lblKey.Text = "簽章 key：";
            lblKey.Location = new Point(16, 94);
            lblKey.AutoSize = true;
            this.Controls.Add(lblKey);

            txtKey = new TextBox();
            txtKey.Location = new Point(16, 116);
            txtKey.Size = new Size(508, 23);
            txtKey.UseSystemPasswordChar = true;
            txtKey.Text = UDSSigner.Key;
            this.Controls.Add(txtKey);

            chkShow = new CheckBox();
            chkShow.Text = "顯示 key";
            chkShow.Location = new Point(16, 146);
            chkShow.AutoSize = true;
            chkShow.CheckedChanged += delegate { txtKey.UseSystemPasswordChar = !chkShow.Checked; };
            this.Controls.Add(chkShow);

            lblStatus = new Label();
            lblStatus.Location = new Point(16, 176);
            lblStatus.Size = new Size(508, 22);
            this.Controls.Add(lblStatus);

            btnTest = new Button();
            btnTest.Text = "測試";
            btnTest.Location = new Point(16, 208);
            btnTest.Size = new Size(84, 28);
            btnTest.Click += delegate { CheckKey(); };
            this.Controls.Add(btnTest);

            btnOK = new Button();
            btnOK.Text = "確定";
            btnOK.Location = new Point(350, 208);
            btnOK.Size = new Size(84, 28);
            btnOK.Click += delegate
            {
                if (!CheckKey()) return;

                UDSSigner.Key = txtKey.Text;
                this.DialogResult = DialogResult.OK;
                this.Close();
            };
            this.Controls.Add(btnOK);

            btnCancel = new Button();
            btnCancel.Text = "取消";
            btnCancel.Location = new Point(440, 208);
            btnCancel.Size = new Size(84, 28);
            btnCancel.DialogResult = DialogResult.Cancel;
            this.Controls.Add(btnCancel);

            this.AcceptButton = btnOK;
            this.CancelButton = btnCancel;
        }

        /// <summary>
        /// 向金鑰中心確認輸入的 key 可用，結果顯示在畫面上。
        /// </summary>
        private bool CheckKey()
        {
            string key = txtKey.Text.Trim();
            if (key.Length == 0)
            {
                SetStatus("請貼上簽章 key。", true);
                return false;
            }

            SetStatus("向金鑰中心確認中...", false);
            this.Enabled = false;
            this.Cursor = Cursors.WaitCursor;
            Application.DoEvents();

            try
            {
                UDSSigner.TestKey(key);
                SetStatus("這把 key 可以使用。", false);
                return true;
            }
            catch (Exception ex)
            {
                SetStatus(ex.Message, true);
                return false;
            }
            finally
            {
                this.Cursor = Cursors.Default;
                this.Enabled = true;
            }
        }

        private void SetStatus(string text, bool error)
        {
            lblStatus.ForeColor = error ? Color.Firebrick : Color.ForestGreen;
            lblStatus.Text = text;
        }
    }
}
