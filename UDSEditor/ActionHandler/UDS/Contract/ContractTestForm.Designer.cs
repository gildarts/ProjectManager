namespace ProjectManager.ActionHandler.UDS.Contract
{
    partial class ContractTestForm
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
            ActiproSoftware.SyntaxEditor.Document document1 = new ActiproSoftware.SyntaxEditor.Document();
            ActiproSoftware.SyntaxEditor.Document document2 = new ActiproSoftware.SyntaxEditor.Document();
            ActiproSoftware.SyntaxEditor.Document document3 = new ActiproSoftware.SyntaxEditor.Document();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ContractTestForm));
            this.xmlSyntaxLanguage1 = new ActiproSoftware.SyntaxEditor.Addons.Xml.XmlSyntaxLanguage(this.components);
            this.btnTest = new System.Windows.Forms.Button();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.txtPwd = new System.Windows.Forms.TextBox();
            this.txtProvider = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSiteURL = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtContract = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.chkPassport = new System.Windows.Forms.CheckBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpInfo = new System.Windows.Forms.TabPage();
            this.txtInfo = new ActiproSoftware.SyntaxEditor.SyntaxEditor();
            this.tpUserInfo = new System.Windows.Forms.TabPage();
            this.txtUserInfo = new ActiproSoftware.SyntaxEditor.SyntaxEditor();
            this.tpPassport = new System.Windows.Forms.TabPage();
            this.txtPassport = new ActiproSoftware.SyntaxEditor.SyntaxEditor();
            this.tabControl1.SuspendLayout();
            this.tpInfo.SuspendLayout();
            this.tpUserInfo.SuspendLayout();
            this.tpPassport.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnTest
            // 
            this.btnTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTest.Location = new System.Drawing.Point(437, 127);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 6;
            this.btnTest.Text = "測試登入";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // txtUser
            // 
            this.txtUser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUser.Location = new System.Drawing.Point(99, 38);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(139, 23);
            this.txtUser.TabIndex = 2;
            // 
            // txtPwd
            // 
            this.txtPwd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPwd.Location = new System.Drawing.Point(309, 41);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.PasswordChar = '●';
            this.txtPwd.Size = new System.Drawing.Size(203, 23);
            this.txtPwd.TabIndex = 3;
            // 
            // txtProvider
            // 
            this.txtProvider.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtProvider.Enabled = false;
            this.txtProvider.Location = new System.Drawing.Point(110, 98);
            this.txtProvider.Name = "txtProvider";
            this.txtProvider.Size = new System.Drawing.Size(402, 23);
            this.txtProvider.TabIndex = 5;
            this.txtProvider.Text = "https://auth.ischool.com.tw/dsa/greening";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(244, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 16);
            this.label3.TabIndex = 8;
            this.label3.Text = "登入密碼";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(48, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 16);
            this.label2.TabIndex = 9;
            this.label2.Text = "發證單位";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 16);
            this.label1.TabIndex = 7;
            this.label1.Text = "登入帳號";
            // 
            // txtSiteURL
            // 
            this.txtSiteURL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSiteURL.Location = new System.Drawing.Point(97, 12);
            this.txtSiteURL.Name = "txtSiteURL";
            this.txtSiteURL.ReadOnly = true;
            this.txtSiteURL.Size = new System.Drawing.Size(206, 23);
            this.txtSiteURL.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 16);
            this.label4.TabIndex = 16;
            this.label4.Text = "AccessPoint";
            // 
            // txtContract
            // 
            this.txtContract.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtContract.Location = new System.Drawing.Point(373, 12);
            this.txtContract.Name = "txtContract";
            this.txtContract.ReadOnly = true;
            this.txtContract.Size = new System.Drawing.Size(139, 23);
            this.txtContract.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(309, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 16);
            this.label5.TabIndex = 18;
            this.label5.Text = "Contract";
            // 
            // chkPassport
            // 
            this.chkPassport.AutoSize = true;
            this.chkPassport.Location = new System.Drawing.Point(28, 72);
            this.chkPassport.Name = "chkPassport";
            this.chkPassport.Size = new System.Drawing.Size(102, 20);
            this.chkPassport.TabIndex = 4;
            this.chkPassport.Text = "使用 Passport";
            this.chkPassport.UseVisualStyleBackColor = true;
            this.chkPassport.CheckedChanged += new System.EventHandler(this.chkPassport_CheckedChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tpInfo);
            this.tabControl1.Controls.Add(this.tpUserInfo);
            this.tabControl1.Controls.Add(this.tpPassport);
            this.tabControl1.Location = new System.Drawing.Point(12, 156);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(500, 310);
            this.tabControl1.TabIndex = 7;
            // 
            // tpInfo
            // 
            this.tpInfo.Controls.Add(this.txtInfo);
            this.tpInfo.Location = new System.Drawing.Point(4, 25);
            this.tpInfo.Name = "tpInfo";
            this.tpInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tpInfo.Size = new System.Drawing.Size(492, 281);
            this.tpInfo.TabIndex = 0;
            this.tpInfo.Text = "訊息";
            this.tpInfo.UseVisualStyleBackColor = true;
            // 
            // txtInfo
            // 
            this.txtInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            document1.Outlining.Mode = ActiproSoftware.SyntaxEditor.OutliningMode.Automatic;
            this.txtInfo.Document = document1;
            this.txtInfo.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInfo.Location = new System.Drawing.Point(3, 3);
            this.txtInfo.Name = "txtInfo";
            this.txtInfo.Size = new System.Drawing.Size(486, 275);
            this.txtInfo.TabIndex = 0;
            // 
            // tpUserInfo
            // 
            this.tpUserInfo.Controls.Add(this.txtUserInfo);
            this.tpUserInfo.Location = new System.Drawing.Point(4, 22);
            this.tpUserInfo.Name = "tpUserInfo";
            this.tpUserInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tpUserInfo.Size = new System.Drawing.Size(492, 284);
            this.tpUserInfo.TabIndex = 1;
            this.tpUserInfo.Text = "使用者屬性";
            this.tpUserInfo.UseVisualStyleBackColor = true;
            // 
            // txtUserInfo
            // 
            this.txtUserInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            document2.Language = this.xmlSyntaxLanguage1;
            document2.Outlining.Mode = ActiproSoftware.SyntaxEditor.OutliningMode.Automatic;
            this.txtUserInfo.Document = document2;
            this.txtUserInfo.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserInfo.Location = new System.Drawing.Point(3, 3);
            this.txtUserInfo.Name = "txtUserInfo";
            this.txtUserInfo.Size = new System.Drawing.Size(486, 278);
            this.txtUserInfo.TabIndex = 2;
            // 
            // tpPassport
            // 
            this.tpPassport.Controls.Add(this.txtPassport);
            this.tpPassport.Location = new System.Drawing.Point(4, 22);
            this.tpPassport.Name = "tpPassport";
            this.tpPassport.Size = new System.Drawing.Size(492, 284);
            this.tpPassport.TabIndex = 2;
            this.tpPassport.Text = "Passport";
            this.tpPassport.UseVisualStyleBackColor = true;
            // 
            // txtPassport
            // 
            this.txtPassport.Dock = System.Windows.Forms.DockStyle.Fill;
            document3.Language = this.xmlSyntaxLanguage1;
            document3.Outlining.Mode = ActiproSoftware.SyntaxEditor.OutliningMode.Automatic;
            this.txtPassport.Document = document3;
            this.txtPassport.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassport.Location = new System.Drawing.Point(0, 0);
            this.txtPassport.Name = "txtPassport";
            this.txtPassport.Size = new System.Drawing.Size(492, 284);
            this.txtPassport.TabIndex = 2;
            // 
            // ContractTestForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(524, 478);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.chkPassport);
            this.Controls.Add(this.txtContract);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtSiteURL);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.txtPwd);
            this.Controls.Add(this.txtProvider);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ContractTestForm";
            this.Text = "Contract 連線測試";
            this.Load += new System.EventHandler(this.ContractTestForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tpInfo.ResumeLayout(false);
            this.tpUserInfo.ResumeLayout(false);
            this.tpPassport.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.TextBox txtPwd;
        private System.Windows.Forms.TextBox txtProvider;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSiteURL;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtContract;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkPassport;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpInfo;
        private System.Windows.Forms.TabPage tpUserInfo;
        private System.Windows.Forms.TabPage tpPassport;
        private ActiproSoftware.SyntaxEditor.SyntaxEditor txtInfo;
        private ActiproSoftware.SyntaxEditor.SyntaxEditor txtUserInfo;
        private ActiproSoftware.SyntaxEditor.SyntaxEditor txtPassport;
        private ActiproSoftware.SyntaxEditor.Addons.Xml.XmlSyntaxLanguage xmlSyntaxLanguage1;
    }
}