namespace ProjectManager.ActionHandler.UDS
{
    partial class DeployToPhysicalForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeployToPhysicalForm));
            this.rbToSite = new System.Windows.Forms.RadioButton();
            this.rbToFile = new System.Windows.Forms.RadioButton();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBrowser = new System.Windows.Forms.Button();
            this.txtAccessPoint = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtContract = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.pnlToSite = new System.Windows.Forms.Panel();
            this.pnlToFile = new System.Windows.Forms.Panel();
            this.btnExecute = new System.Windows.Forms.Button();
            this.err = new System.Windows.Forms.ErrorProvider(this.components);
            this.pnlToSite.SuspendLayout();
            this.pnlToFile.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.err)).BeginInit();
            this.SuspendLayout();
            // 
            // rbToSite
            // 
            this.rbToSite.AutoSize = true;
            this.rbToSite.Location = new System.Drawing.Point(22, 124);
            this.rbToSite.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rbToSite.Name = "rbToSite";
            this.rbToSite.Size = new System.Drawing.Size(134, 20);
            this.rbToSite.TabIndex = 1;
            this.rbToSite.Text = "直接部署至指定站台";
            this.rbToSite.UseVisualStyleBackColor = true;
            this.rbToSite.CheckedChanged += new System.EventHandler(this.CheckedChanged);
            // 
            // rbToFile
            // 
            this.rbToFile.AutoSize = true;
            this.rbToFile.Checked = true;
            this.rbToFile.Location = new System.Drawing.Point(22, 13);
            this.rbToFile.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rbToFile.Name = "rbToFile";
            this.rbToFile.Size = new System.Drawing.Size(134, 20);
            this.rbToFile.TabIndex = 0;
            this.rbToFile.TabStop = true;
            this.rbToFile.Text = "儲存成實體部署檔案";
            this.rbToFile.UseVisualStyleBackColor = true;
            this.rbToFile.CheckedChanged += new System.EventHandler(this.CheckedChanged);
            // 
            // txtPath
            // 
            this.txtPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPath.Location = new System.Drawing.Point(17, 30);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(362, 23);
            this.txtPath.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "指定檔案路徑名稱";
            // 
            // btnBrowser
            // 
            this.btnBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowser.Location = new System.Drawing.Point(385, 30);
            this.btnBrowser.Name = "btnBrowser";
            this.btnBrowser.Size = new System.Drawing.Size(29, 23);
            this.btnBrowser.TabIndex = 2;
            this.btnBrowser.Text = "...";
            this.btnBrowser.UseVisualStyleBackColor = true;
            this.btnBrowser.Click += new System.EventHandler(this.btnBrowser_Click);
            // 
            // txtAccessPoint
            // 
            this.txtAccessPoint.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAccessPoint.Location = new System.Drawing.Point(94, 15);
            this.txtAccessPoint.Name = "txtAccessPoint";
            this.txtAccessPoint.Size = new System.Drawing.Size(306, 23);
            this.txtAccessPoint.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "AccessPoint";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "Contract";
            // 
            // txtContract
            // 
            this.txtContract.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtContract.Location = new System.Drawing.Point(94, 43);
            this.txtContract.Name = "txtContract";
            this.txtContract.Size = new System.Drawing.Size(306, 23);
            this.txtContract.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(56, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "帳號";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(56, 102);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 16);
            this.label5.TabIndex = 7;
            this.label5.Text = "密碼";
            // 
            // txtUserName
            // 
            this.txtUserName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUserName.Location = new System.Drawing.Point(94, 71);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(306, 23);
            this.txtUserName.TabIndex = 2;
            // 
            // txtPassword
            // 
            this.txtPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPassword.Location = new System.Drawing.Point(94, 99);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '●';
            this.txtPassword.Size = new System.Drawing.Size(306, 23);
            this.txtPassword.TabIndex = 3;
            // 
            // pnlToSite
            // 
            this.pnlToSite.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlToSite.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlToSite.Controls.Add(this.txtAccessPoint);
            this.pnlToSite.Controls.Add(this.label5);
            this.pnlToSite.Controls.Add(this.txtContract);
            this.pnlToSite.Controls.Add(this.label4);
            this.pnlToSite.Controls.Add(this.txtUserName);
            this.pnlToSite.Controls.Add(this.label3);
            this.pnlToSite.Controls.Add(this.txtPassword);
            this.pnlToSite.Controls.Add(this.label2);
            this.pnlToSite.Enabled = false;
            this.pnlToSite.Location = new System.Drawing.Point(12, 151);
            this.pnlToSite.Name = "pnlToSite";
            this.pnlToSite.Size = new System.Drawing.Size(430, 141);
            this.pnlToSite.TabIndex = 2;
            // 
            // pnlToFile
            // 
            this.pnlToFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlToFile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlToFile.Controls.Add(this.txtPath);
            this.pnlToFile.Controls.Add(this.label1);
            this.pnlToFile.Controls.Add(this.btnBrowser);
            this.pnlToFile.Location = new System.Drawing.Point(12, 34);
            this.pnlToFile.Name = "pnlToFile";
            this.pnlToFile.Size = new System.Drawing.Size(430, 69);
            this.pnlToFile.TabIndex = 0;
            // 
            // btnExecute
            // 
            this.btnExecute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExecute.Location = new System.Drawing.Point(367, 298);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(75, 23);
            this.btnExecute.TabIndex = 3;
            this.btnExecute.Text = "開始執行";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // err
            // 
            this.err.ContainerControl = this;
            // 
            // DeployToPhysicalForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 333);
            this.Controls.Add(this.btnExecute);
            this.Controls.Add(this.pnlToFile);
            this.Controls.Add(this.pnlToSite);
            this.Controls.Add(this.rbToFile);
            this.Controls.Add(this.rbToSite);
            this.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "DeployToPhysicalForm";
            this.Text = "部署 UDS 成實體 Service";
            this.pnlToSite.ResumeLayout(false);
            this.pnlToSite.PerformLayout();
            this.pnlToFile.ResumeLayout(false);
            this.pnlToFile.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.err)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rbToSite;
        private System.Windows.Forms.RadioButton rbToFile;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBrowser;
        private System.Windows.Forms.TextBox txtAccessPoint;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtContract;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Panel pnlToSite;
        private System.Windows.Forms.Panel pnlToFile;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.ErrorProvider err;
    }
}