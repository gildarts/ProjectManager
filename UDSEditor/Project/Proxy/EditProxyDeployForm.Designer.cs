namespace ProjectManager.Project.Proxy
{
    partial class EditProxyDeployForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditProxyDeployForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSiteName = new System.Windows.Forms.TextBox();
            this.txtAccessPoint = new System.Windows.Forms.TextBox();
            this.txtContract = new System.Windows.Forms.TextBox();
            this.pnlBasic = new System.Windows.Forms.Panel();
            this.txtBasicPassword = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtBasicAccount = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.rbBasic = new System.Windows.Forms.RadioButton();
            this.rbPassport = new System.Windows.Forms.RadioButton();
            this.pnlPassport = new System.Windows.Forms.Panel();
            this.txtIssuer = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtIssuerPassword = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtIssuerAccount = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.err = new System.Windows.Forms.ErrorProvider(this.components);
            this.pnlBasic.SuspendLayout();
            this.pnlPassport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.err)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(55, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "名稱";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "AccessPoint";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "Contract";
            // 
            // txtSiteName
            // 
            this.txtSiteName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSiteName.Location = new System.Drawing.Point(93, 18);
            this.txtSiteName.Name = "txtSiteName";
            this.txtSiteName.Size = new System.Drawing.Size(404, 23);
            this.txtSiteName.TabIndex = 0;
            // 
            // txtAccessPoint
            // 
            this.txtAccessPoint.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAccessPoint.Location = new System.Drawing.Point(93, 44);
            this.txtAccessPoint.Name = "txtAccessPoint";
            this.txtAccessPoint.Size = new System.Drawing.Size(404, 23);
            this.txtAccessPoint.TabIndex = 1;
            // 
            // txtContract
            // 
            this.txtContract.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtContract.Location = new System.Drawing.Point(93, 69);
            this.txtContract.Name = "txtContract";
            this.txtContract.Size = new System.Drawing.Size(404, 23);
            this.txtContract.TabIndex = 2;
            // 
            // pnlBasic
            // 
            this.pnlBasic.Controls.Add(this.txtBasicPassword);
            this.pnlBasic.Controls.Add(this.label5);
            this.pnlBasic.Controls.Add(this.txtBasicAccount);
            this.pnlBasic.Controls.Add(this.label4);
            this.pnlBasic.Location = new System.Drawing.Point(34, 123);
            this.pnlBasic.Name = "pnlBasic";
            this.pnlBasic.Size = new System.Drawing.Size(463, 84);
            this.pnlBasic.TabIndex = 4;
            // 
            // txtBasicPassword
            // 
            this.txtBasicPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBasicPassword.Location = new System.Drawing.Point(46, 45);
            this.txtBasicPassword.Name = "txtBasicPassword";
            this.txtBasicPassword.PasswordChar = '●';
            this.txtBasicPassword.Size = new System.Drawing.Size(392, 23);
            this.txtBasicPassword.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 16);
            this.label5.TabIndex = 4;
            this.label5.Text = "密碼";
            // 
            // txtBasicAccount
            // 
            this.txtBasicAccount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBasicAccount.Location = new System.Drawing.Point(46, 16);
            this.txtBasicAccount.Name = "txtBasicAccount";
            this.txtBasicAccount.Size = new System.Drawing.Size(392, 23);
            this.txtBasicAccount.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 16);
            this.label4.TabIndex = 2;
            this.label4.Text = "帳號";
            // 
            // rbBasic
            // 
            this.rbBasic.AutoSize = true;
            this.rbBasic.Checked = true;
            this.rbBasic.Location = new System.Drawing.Point(34, 98);
            this.rbBasic.Name = "rbBasic";
            this.rbBasic.Size = new System.Drawing.Size(108, 20);
            this.rbBasic.TabIndex = 3;
            this.rbBasic.TabStop = true;
            this.rbBasic.Text = "使用 Basic 認證";
            this.rbBasic.UseVisualStyleBackColor = true;
            this.rbBasic.CheckedChanged += new System.EventHandler(this.CheckedChanged);
            // 
            // rbPassport
            // 
            this.rbPassport.AutoSize = true;
            this.rbPassport.Location = new System.Drawing.Point(34, 222);
            this.rbPassport.Name = "rbPassport";
            this.rbPassport.Size = new System.Drawing.Size(128, 20);
            this.rbPassport.TabIndex = 5;
            this.rbPassport.Text = "使用 Passport 認證";
            this.rbPassport.UseVisualStyleBackColor = true;
            this.rbPassport.CheckedChanged += new System.EventHandler(this.CheckedChanged);
            // 
            // pnlPassport
            // 
            this.pnlPassport.Controls.Add(this.txtIssuer);
            this.pnlPassport.Controls.Add(this.label8);
            this.pnlPassport.Controls.Add(this.txtIssuerPassword);
            this.pnlPassport.Controls.Add(this.label6);
            this.pnlPassport.Controls.Add(this.txtIssuerAccount);
            this.pnlPassport.Controls.Add(this.label7);
            this.pnlPassport.Enabled = false;
            this.pnlPassport.Location = new System.Drawing.Point(34, 247);
            this.pnlPassport.Name = "pnlPassport";
            this.pnlPassport.Size = new System.Drawing.Size(463, 110);
            this.pnlPassport.TabIndex = 6;
            // 
            // txtIssuer
            // 
            this.txtIssuer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtIssuer.Location = new System.Drawing.Point(46, 16);
            this.txtIssuer.Name = "txtIssuer";
            this.txtIssuer.Size = new System.Drawing.Size(392, 23);
            this.txtIssuer.TabIndex = 0;
            this.txtIssuer.Text = "http://web.ischool.com.tw/service/shared";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 19);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(32, 16);
            this.label8.TabIndex = 10;
            this.label8.Text = "單位";
            // 
            // txtIssuerPassword
            // 
            this.txtIssuerPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtIssuerPassword.Location = new System.Drawing.Point(46, 74);
            this.txtIssuerPassword.Name = "txtIssuerPassword";
            this.txtIssuerPassword.PasswordChar = '●';
            this.txtIssuerPassword.Size = new System.Drawing.Size(392, 23);
            this.txtIssuerPassword.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 77);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(32, 16);
            this.label6.TabIndex = 8;
            this.label6.Text = "密碼";
            // 
            // txtIssuerAccount
            // 
            this.txtIssuerAccount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtIssuerAccount.Location = new System.Drawing.Point(46, 45);
            this.txtIssuerAccount.Name = "txtIssuerAccount";
            this.txtIssuerAccount.Size = new System.Drawing.Size(392, 23);
            this.txtIssuerAccount.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 48);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 16);
            this.label7.TabIndex = 6;
            this.label7.Text = "帳號";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(418, 373);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "儲存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnTest
            // 
            this.btnTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTest.Location = new System.Drawing.Point(337, 373);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 7;
            this.btnTest.Text = "測試認證";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // err
            // 
            this.err.ContainerControl = this;
            // 
            // EditProxyDeployForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(523, 408);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.rbPassport);
            this.Controls.Add(this.pnlPassport);
            this.Controls.Add(this.rbBasic);
            this.Controls.Add(this.pnlBasic);
            this.Controls.Add(this.txtContract);
            this.Controls.Add(this.txtAccessPoint);
            this.Controls.Add(this.txtSiteName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "EditProxyDeployForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "編輯代理伺服器";
            this.Load += new System.EventHandler(this.EditProxyDeployForm_Load);
            this.pnlBasic.ResumeLayout(false);
            this.pnlBasic.PerformLayout();
            this.pnlPassport.ResumeLayout(false);
            this.pnlPassport.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.err)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSiteName;
        private System.Windows.Forms.TextBox txtAccessPoint;
        private System.Windows.Forms.TextBox txtContract;
        private System.Windows.Forms.Panel pnlBasic;
        private System.Windows.Forms.TextBox txtBasicPassword;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtBasicAccount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rbBasic;
        private System.Windows.Forms.RadioButton rbPassport;
        private System.Windows.Forms.Panel pnlPassport;
        private System.Windows.Forms.TextBox txtIssuer;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtIssuerPassword;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtIssuerAccount;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.ErrorProvider err;
    }
}