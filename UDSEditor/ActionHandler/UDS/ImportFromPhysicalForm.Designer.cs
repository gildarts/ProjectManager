namespace ProjectManager.ActionHandler.UDS
{
    partial class ImportFromPhysicalForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportFromPhysicalForm));
            this.btnExecute = new System.Windows.Forms.Button();
            this.pnlToFile = new System.Windows.Forms.Panel();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBrowser = new System.Windows.Forms.Button();
            this.pnlToSite = new System.Windows.Forms.Panel();
            this.txtAccessPoint = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtContract = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.rbToFile = new System.Windows.Forms.RadioButton();
            this.rbToSite = new System.Windows.Forms.RadioButton();
            this.err = new System.Windows.Forms.ErrorProvider(this.components);
            this.pnlToFile.SuspendLayout();
            this.pnlToSite.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.err)).BeginInit();
            this.SuspendLayout();
            // 
            // btnExecute
            // 
            this.btnExecute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExecute.Location = new System.Drawing.Point(384, 314);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(75, 23);
            this.btnExecute.TabIndex = 8;
            this.btnExecute.Text = "開始匯入";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // pnlToFile
            // 
            this.pnlToFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlToFile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlToFile.Controls.Add(this.txtPath);
            this.pnlToFile.Controls.Add(this.label1);
            this.pnlToFile.Controls.Add(this.btnBrowser);
            this.pnlToFile.Location = new System.Drawing.Point(20, 33);
            this.pnlToFile.Name = "pnlToFile";
            this.pnlToFile.Size = new System.Drawing.Size(439, 69);
            this.pnlToFile.TabIndex = 5;
            // 
            // txtPath
            // 
            this.txtPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPath.Location = new System.Drawing.Point(17, 30);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(371, 23);
            this.txtPath.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "指定檔案路徑";
            // 
            // btnBrowser
            // 
            this.btnBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowser.Location = new System.Drawing.Point(394, 30);
            this.btnBrowser.Name = "btnBrowser";
            this.btnBrowser.Size = new System.Drawing.Size(29, 23);
            this.btnBrowser.TabIndex = 2;
            this.btnBrowser.Text = "...";
            this.btnBrowser.UseVisualStyleBackColor = true;
            this.btnBrowser.Click += new System.EventHandler(this.btnBrowser_Click);
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
            this.pnlToSite.Location = new System.Drawing.Point(20, 150);
            this.pnlToSite.Name = "pnlToSite";
            this.pnlToSite.Size = new System.Drawing.Size(439, 143);
            this.pnlToSite.TabIndex = 7;
            // 
            // txtAccessPoint
            // 
            this.txtAccessPoint.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAccessPoint.Location = new System.Drawing.Point(94, 15);
            this.txtAccessPoint.Name = "txtAccessPoint";
            this.txtAccessPoint.Size = new System.Drawing.Size(315, 23);
            this.txtAccessPoint.TabIndex = 0;
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
            // txtContract
            // 
            this.txtContract.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtContract.Location = new System.Drawing.Point(94, 43);
            this.txtContract.Name = "txtContract";
            this.txtContract.Size = new System.Drawing.Size(315, 23);
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
            // txtUserName
            // 
            this.txtUserName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUserName.Location = new System.Drawing.Point(94, 71);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(315, 23);
            this.txtUserName.TabIndex = 2;
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
            // txtPassword
            // 
            this.txtPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPassword.Location = new System.Drawing.Point(94, 99);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '●';
            this.txtPassword.Size = new System.Drawing.Size(315, 23);
            this.txtPassword.TabIndex = 3;
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
            // rbToFile
            // 
            this.rbToFile.AutoSize = true;
            this.rbToFile.Checked = true;
            this.rbToFile.Location = new System.Drawing.Point(20, 6);
            this.rbToFile.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rbToFile.Name = "rbToFile";
            this.rbToFile.Size = new System.Drawing.Size(150, 20);
            this.rbToFile.TabIndex = 4;
            this.rbToFile.TabStop = true;
            this.rbToFile.Text = "從實體Service部署檔案";
            this.rbToFile.UseVisualStyleBackColor = true;
            this.rbToFile.CheckedChanged += new System.EventHandler(this.CheckedChanged);
            // 
            // rbToSite
            // 
            this.rbToSite.AutoSize = true;
            this.rbToSite.Location = new System.Drawing.Point(20, 123);
            this.rbToSite.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rbToSite.Name = "rbToSite";
            this.rbToSite.Size = new System.Drawing.Size(110, 20);
            this.rbToSite.TabIndex = 6;
            this.rbToSite.Text = "從指定開發站台";
            this.rbToSite.UseVisualStyleBackColor = true;
            this.rbToSite.CheckedChanged += new System.EventHandler(this.CheckedChanged);
            // 
            // err
            // 
            this.err.ContainerControl = this;
            // 
            // ImportFromPhysicalForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(479, 349);
            this.Controls.Add(this.btnExecute);
            this.Controls.Add(this.pnlToFile);
            this.Controls.Add(this.pnlToSite);
            this.Controls.Add(this.rbToFile);
            this.Controls.Add(this.rbToSite);
            this.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ImportFromPhysicalForm";
            this.Text = "從實體Service中載入UDS";
            this.pnlToFile.ResumeLayout(false);
            this.pnlToFile.PerformLayout();
            this.pnlToSite.ResumeLayout(false);
            this.pnlToSite.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.err)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.Panel pnlToFile;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBrowser;
        private System.Windows.Forms.Panel pnlToSite;
        private System.Windows.Forms.TextBox txtAccessPoint;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtContract;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rbToFile;
        private System.Windows.Forms.RadioButton rbToSite;
        private System.Windows.Forms.ErrorProvider err;
    }
}