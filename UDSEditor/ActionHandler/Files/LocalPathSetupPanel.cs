using System;
using System.Drawing;
using System.Windows.Forms;

namespace ProjectManager.ActionHandler.Files
{
    /// <summary>
    /// 專案還沒設定本機目錄時，點「檔案管理」右邊顯示的設定畫面。
    /// 版面直接用程式碼排，沒有設計工具檔。
    /// </summary>
    class LocalPathSetupPanel : UserControl
    {
        public LocalPathSetupPanel(FileNodeHandler owner)
        {
            this.Font = new Font("微軟正黑體", 10F);
            this.BackColor = SystemColors.Window;
            this.Dock = DockStyle.Fill;

            Label lblTitle = new Label();
            lblTitle.Text = "尚未設定本機目錄";
            lblTitle.Font = new Font("微軟正黑體", 14F, FontStyle.Bold);
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(32, 32);
            this.Controls.Add(lblTitle);

            Label lblMessage = new Label();
            lblMessage.Text = "檔案管理需要對應到這台電腦上的一個資料夾，用來比對、上傳、下載模組檔案。\n"
                            + "這個設定只存在本機，換電腦或刪除設定檔後需要重新設定。";
            lblMessage.AutoSize = true;
            lblMessage.MaximumSize = new Size(640, 0);
            lblMessage.Location = new Point(34, 76);
            this.Controls.Add(lblMessage);

            Button btnSetup = new Button();
            btnSetup.Text = "設定本機目錄...";
            btnSetup.Size = new Size(160, 34);
            btnSetup.Location = new Point(34, 136);
            btnSetup.Click += delegate { owner.Setup(); };
            this.Controls.Add(btnSetup);
        }
    }
}
