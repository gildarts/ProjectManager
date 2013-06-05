namespace ProjectManager
{
    partial class CheckVersionForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CheckVersionForm));
            this.lblLastest = new System.Windows.Forms.Label();
            this.lblCurrent = new System.Windows.Forms.Label();
            this.webDesc = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // lblLastest
            // 
            this.lblLastest.AutoSize = true;
            this.lblLastest.Location = new System.Drawing.Point(15, 37);
            this.lblLastest.Name = "lblLastest";
            this.lblLastest.Size = new System.Drawing.Size(56, 16);
            this.lblLastest.TabIndex = 6;
            this.lblLastest.Text = "最新版本";
            // 
            // lblCurrent
            // 
            this.lblCurrent.AutoSize = true;
            this.lblCurrent.Location = new System.Drawing.Point(15, 12);
            this.lblCurrent.Name = "lblCurrent";
            this.lblCurrent.Size = new System.Drawing.Size(56, 16);
            this.lblCurrent.TabIndex = 4;
            this.lblCurrent.Text = "目前版本";
            // 
            // webDesc
            // 
            this.webDesc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.webDesc.Location = new System.Drawing.Point(22, 66);
            this.webDesc.MinimumSize = new System.Drawing.Size(20, 20);
            this.webDesc.Name = "webDesc";
            this.webDesc.Size = new System.Drawing.Size(348, 161);
            this.webDesc.TabIndex = 11;
            this.webDesc.Url = new System.Uri("https://docs.google.com/document/pub?id=11c7h91DzmOD3elnAo--CcgAGXjhmk0OAo59pCZhg" +
                    "jKo", System.UriKind.Absolute);
            // 
            // CheckVersionForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(382, 248);
            this.Controls.Add(this.webDesc);
            this.Controls.Add(this.lblLastest);
            this.Controls.Add(this.lblCurrent);
            this.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CheckVersionForm";
            this.Text = "版本資訊";
            this.Load += new System.EventHandler(this.CheckVersionForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblLastest;
        private System.Windows.Forms.Label lblCurrent;
        private System.Windows.Forms.WebBrowser webDesc;
    }
}