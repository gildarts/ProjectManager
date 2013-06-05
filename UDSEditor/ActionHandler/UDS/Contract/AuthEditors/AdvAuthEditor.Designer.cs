namespace ProjectManager.ActionHandler.UDS.Contract.AuthEditors
{
    partial class AdvAuthEditor
    {
        /// <summary> 
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 元件設計工具產生的程式碼

        /// <summary> 
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdvAuthEditor));
            this.tab = new System.Windows.Forms.TabControl();
            this.tpBasic = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.txtGetUserRoleQuery = new ProjectManager.Editor.SQLText();
            this.label2 = new System.Windows.Forms.Label();
            this.txtGetUserDataQuery = new ProjectManager.Editor.SQLText();
            this.chkBasic = new System.Windows.Forms.CheckBox();
            this.cbHashProvider = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tpSession = new System.Windows.Forms.TabPage();
            this.txtTimeout = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.chkSession = new System.Windows.Forms.CheckBox();
            this.tpPassport = new System.Windows.Forms.TabPage();
            this.picCheck = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbDisable = new System.Windows.Forms.RadioButton();
            this.rbEnable = new System.Windows.Forms.RadioButton();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.dgExtProp = new System.Windows.Forms.DataGridView();
            this.colDBField = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAlias = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cboMappingField = new System.Windows.Forms.ComboBox();
            this.cbUserNameField = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cbALTable = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.chkPassport = new System.Windows.Forms.CheckBox();
            this.txtCertProvider = new System.Windows.Forms.TextBox();
            this.txtIssuer = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.err = new System.Windows.Forms.ErrorProvider(this.components);
            this.tab.SuspendLayout();
            this.tpBasic.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tpSession.SuspendLayout();
            this.tpPassport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCheck)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgExtProp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.err)).BeginInit();
            this.SuspendLayout();
            // 
            // tab
            // 
            this.tab.Controls.Add(this.tpBasic);
            this.tab.Controls.Add(this.tpSession);
            this.tab.Controls.Add(this.tpPassport);
            this.tab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tab.Location = new System.Drawing.Point(0, 0);
            this.tab.Name = "tab";
            this.tab.SelectedIndex = 0;
            this.tab.Size = new System.Drawing.Size(595, 518);
            this.tab.TabIndex = 0;
            // 
            // tpBasic
            // 
            this.tpBasic.Controls.Add(this.tableLayoutPanel1);
            this.tpBasic.Controls.Add(this.chkBasic);
            this.tpBasic.Controls.Add(this.cbHashProvider);
            this.tpBasic.Controls.Add(this.label1);
            this.tpBasic.Location = new System.Drawing.Point(4, 25);
            this.tpBasic.Name = "tpBasic";
            this.tpBasic.Padding = new System.Windows.Forms.Padding(3);
            this.tpBasic.Size = new System.Drawing.Size(587, 489);
            this.tpBasic.TabIndex = 0;
            this.tpBasic.Text = "基本認證";
            this.tpBasic.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtGetUserRoleQuery, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtGetUserDataQuery, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(15, 83);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(557, 400);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 200);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "取得使用者身份";
            // 
            // txtGetUserRoleQuery
            // 
            this.txtGetUserRoleQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGetUserRoleQuery.Location = new System.Drawing.Point(103, 205);
            this.txtGetUserRoleQuery.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtGetUserRoleQuery.Name = "txtGetUserRoleQuery";
            this.txtGetUserRoleQuery.Size = new System.Drawing.Size(451, 190);
            this.txtGetUserRoleQuery.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "取得使用者資訊";
            // 
            // txtGetUserDataQuery
            // 
            this.txtGetUserDataQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGetUserDataQuery.Location = new System.Drawing.Point(103, 4);
            this.txtGetUserDataQuery.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtGetUserDataQuery.Name = "txtGetUserDataQuery";
            this.txtGetUserDataQuery.Size = new System.Drawing.Size(451, 192);
            this.txtGetUserDataQuery.TabIndex = 0;
            // 
            // chkBasic
            // 
            this.chkBasic.AutoSize = true;
            this.chkBasic.Location = new System.Drawing.Point(15, 17);
            this.chkBasic.Name = "chkBasic";
            this.chkBasic.Size = new System.Drawing.Size(159, 20);
            this.chkBasic.TabIndex = 0;
            this.chkBasic.Text = "啟用帳號密碼認證(Basic)";
            this.chkBasic.UseVisualStyleBackColor = true;
            this.chkBasic.CheckedChanged += new System.EventHandler(this.chkBasic_CheckedChanged);
            // 
            // cbHashProvider
            // 
            this.cbHashProvider.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbHashProvider.AutoCompleteCustomSource.AddRange(new string[] {
            "SHA1",
            "MD5",
            "不加密"});
            this.cbHashProvider.FormattingEnabled = true;
            this.cbHashProvider.Items.AddRange(new object[] {
            "SHA1",
            "MD5"});
            this.cbHashProvider.Location = new System.Drawing.Point(107, 43);
            this.cbHashProvider.Name = "cbHashProvider";
            this.cbHashProvider.Size = new System.Drawing.Size(132, 24);
            this.cbHashProvider.TabIndex = 1;
            this.cbHashProvider.TextChanged += new System.EventHandler(this.cbHashProvider_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "密碼加密演算法";
            // 
            // tpSession
            // 
            this.tpSession.Controls.Add(this.txtTimeout);
            this.tpSession.Controls.Add(this.label4);
            this.tpSession.Controls.Add(this.label5);
            this.tpSession.Controls.Add(this.chkSession);
            this.tpSession.Location = new System.Drawing.Point(4, 22);
            this.tpSession.Name = "tpSession";
            this.tpSession.Padding = new System.Windows.Forms.Padding(3);
            this.tpSession.Size = new System.Drawing.Size(587, 492);
            this.tpSession.TabIndex = 1;
            this.tpSession.Text = "Session 認證";
            this.tpSession.UseVisualStyleBackColor = true;
            // 
            // txtTimeout
            // 
            this.txtTimeout.Location = new System.Drawing.Point(185, 18);
            this.txtTimeout.Name = "txtTimeout";
            this.txtTimeout.Size = new System.Drawing.Size(47, 23);
            this.txtTimeout.TabIndex = 2;
            this.txtTimeout.Text = "20";
            this.txtTimeout.TextChanged += new System.EventHandler(this.txtTimeout_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(238, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "分鐘";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(135, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 16);
            this.label5.TabIndex = 1;
            this.label5.Text = "，時效";
            // 
            // chkSession
            // 
            this.chkSession.AutoSize = true;
            this.chkSession.Checked = true;
            this.chkSession.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSession.Location = new System.Drawing.Point(17, 20);
            this.chkSession.Name = "chkSession";
            this.chkSession.Size = new System.Drawing.Size(112, 16);
            this.chkSession.TabIndex = 0;
            this.chkSession.Text = "啟用 Session 認證";
            this.chkSession.UseVisualStyleBackColor = true;
            this.chkSession.CheckedChanged += new System.EventHandler(this.chkSession_CheckedChanged);
            // 
            // tpPassport
            // 
            this.tpPassport.Controls.Add(this.picCheck);
            this.tpPassport.Controls.Add(this.groupBox1);
            this.tpPassport.Controls.Add(this.chkPassport);
            this.tpPassport.Controls.Add(this.txtCertProvider);
            this.tpPassport.Controls.Add(this.txtIssuer);
            this.tpPassport.Controls.Add(this.label6);
            this.tpPassport.Controls.Add(this.label8);
            this.tpPassport.Location = new System.Drawing.Point(4, 22);
            this.tpPassport.Name = "tpPassport";
            this.tpPassport.Size = new System.Drawing.Size(587, 492);
            this.tpPassport.TabIndex = 2;
            this.tpPassport.Text = "Passport 認證";
            this.tpPassport.UseVisualStyleBackColor = true;
            // 
            // picCheck
            // 
            this.picCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picCheck.Image = ((System.Drawing.Image)(resources.GetObject("picCheck.Image")));
            this.picCheck.Location = new System.Drawing.Point(525, 71);
            this.picCheck.Name = "picCheck";
            this.picCheck.Size = new System.Drawing.Size(22, 22);
            this.picCheck.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picCheck.TabIndex = 15;
            this.picCheck.TabStop = false;
            this.picCheck.Click += new System.EventHandler(this.picCheck_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.rbDisable);
            this.groupBox1.Controls.Add(this.rbEnable);
            this.groupBox1.Controls.Add(this.btnDelete);
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Controls.Add(this.dgExtProp);
            this.groupBox1.Controls.Add(this.cboMappingField);
            this.groupBox1.Controls.Add(this.cbUserNameField);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.cbALTable);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Location = new System.Drawing.Point(18, 109);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(551, 319);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "帳號連結";
            // 
            // rbDisable
            // 
            this.rbDisable.AutoSize = true;
            this.rbDisable.Location = new System.Drawing.Point(208, 22);
            this.rbDisable.Name = "rbDisable";
            this.rbDisable.Size = new System.Drawing.Size(107, 16);
            this.rbDisable.TabIndex = 1;
            this.rbDisable.Text = "不使用帳號連結";
            this.rbDisable.UseVisualStyleBackColor = true;
            this.rbDisable.CheckedChanged += new System.EventHandler(this.rbDisable_CheckedChanged);
            // 
            // rbEnable
            // 
            this.rbEnable.AutoSize = true;
            this.rbEnable.Checked = true;
            this.rbEnable.Location = new System.Drawing.Point(104, 22);
            this.rbEnable.Name = "rbEnable";
            this.rbEnable.Size = new System.Drawing.Size(95, 16);
            this.rbEnable.TabIndex = 0;
            this.rbEnable.TabStop = true;
            this.rbEnable.Text = "啟用帳號連結";
            this.rbEnable.UseVisualStyleBackColor = true;
            this.rbEnable.CheckedChanged += new System.EventHandler(this.rbEnable_CheckedChanged);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Location = new System.Drawing.Point(466, 256);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(48, 23);
            this.btnDelete.TabIndex = 7;
            this.btnDelete.Text = "刪除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Location = new System.Drawing.Point(412, 256);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(48, 23);
            this.btnAdd.TabIndex = 6;
            this.btnAdd.Text = "新增";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // dgExtProp
            // 
            this.dgExtProp.AllowUserToAddRows = false;
            this.dgExtProp.AllowUserToDeleteRows = false;
            this.dgExtProp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgExtProp.BackgroundColor = System.Drawing.Color.White;
            this.dgExtProp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgExtProp.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colDBField,
            this.colAlias});
            this.dgExtProp.Location = new System.Drawing.Point(36, 166);
            this.dgExtProp.Name = "dgExtProp";
            this.dgExtProp.ReadOnly = true;
            this.dgExtProp.RowTemplate.Height = 24;
            this.dgExtProp.Size = new System.Drawing.Size(478, 84);
            this.dgExtProp.TabIndex = 5;
            // 
            // colDBField
            // 
            this.colDBField.HeaderText = "資料表欄位";
            this.colDBField.Name = "colDBField";
            this.colDBField.ReadOnly = true;
            this.colDBField.Width = 150;
            // 
            // colAlias
            // 
            this.colAlias.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colAlias.HeaderText = "輸出名";
            this.colAlias.Name = "colAlias";
            this.colAlias.ReadOnly = true;
            // 
            // cboMappingField
            // 
            this.cboMappingField.AutoCompleteCustomSource.AddRange(new string[] {
            "SHA1",
            "MD5",
            "不加密"});
            this.cboMappingField.FormattingEnabled = true;
            this.cboMappingField.Location = new System.Drawing.Point(104, 78);
            this.cboMappingField.Name = "cboMappingField";
            this.cboMappingField.Size = new System.Drawing.Size(193, 24);
            this.cboMappingField.TabIndex = 3;
            this.cboMappingField.TextChanged += new System.EventHandler(this.cboMappingField_TextChanged);
            // 
            // cbUserNameField
            // 
            this.cbUserNameField.AutoCompleteCustomSource.AddRange(new string[] {
            "SHA1",
            "MD5",
            "不加密"});
            this.cbUserNameField.FormattingEnabled = true;
            this.cbUserNameField.Location = new System.Drawing.Point(104, 108);
            this.cbUserNameField.Name = "cbUserNameField";
            this.cbUserNameField.Size = new System.Drawing.Size(193, 24);
            this.cbUserNameField.TabIndex = 4;
            this.cbUserNameField.TextChanged += new System.EventHandler(this.cbUserNameField_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(18, 81);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(80, 16);
            this.label10.TabIndex = 4;
            this.label10.Text = "比對帳號欄位";
            // 
            // cbALTable
            // 
            this.cbALTable.AutoCompleteCustomSource.AddRange(new string[] {
            "SHA1",
            "MD5",
            "不加密"});
            this.cbALTable.FormattingEnabled = true;
            this.cbALTable.Location = new System.Drawing.Point(104, 48);
            this.cbALTable.Name = "cbALTable";
            this.cbALTable.Size = new System.Drawing.Size(193, 24);
            this.cbALTable.TabIndex = 2;
            this.cbALTable.TextChanged += new System.EventHandler(this.cbALTable_TextChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(33, 147);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(56, 16);
            this.label11.TabIndex = 4;
            this.label11.Text = "延伸屬性";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(18, 111);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(80, 16);
            this.label9.TabIndex = 4;
            this.label9.Text = "輸出帳號欄位";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(30, 51);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 16);
            this.label7.TabIndex = 4;
            this.label7.Text = "帳號資料表";
            // 
            // chkPassport
            // 
            this.chkPassport.AutoSize = true;
            this.chkPassport.Location = new System.Drawing.Point(17, 13);
            this.chkPassport.Name = "chkPassport";
            this.chkPassport.Size = new System.Drawing.Size(116, 16);
            this.chkPassport.TabIndex = 0;
            this.chkPassport.Text = "啟用 Passport 認證";
            this.chkPassport.UseVisualStyleBackColor = true;
            this.chkPassport.CheckedChanged += new System.EventHandler(this.chkPassport_CheckedChanged);
            // 
            // txtCertProvider
            // 
            this.txtCertProvider.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCertProvider.Location = new System.Drawing.Point(73, 70);
            this.txtCertProvider.Name = "txtCertProvider";
            this.txtCertProvider.Size = new System.Drawing.Size(446, 23);
            this.txtCertProvider.TabIndex = 2;
            this.txtCertProvider.Text = "http://new.iteacher.tw/service/shared/info/Public.GetPublicKey";
            this.txtCertProvider.TextChanged += new System.EventHandler(this.txtCertProvider_TextChanged);
            // 
            // txtIssuer
            // 
            this.txtIssuer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtIssuer.Location = new System.Drawing.Point(73, 39);
            this.txtIssuer.Name = "txtIssuer";
            this.txtIssuer.Size = new System.Drawing.Size(446, 23);
            this.txtIssuer.TabIndex = 1;
            this.txtIssuer.Text = "greening.shared.user";
            this.txtIssuer.TextChanged += new System.EventHandler(this.txtIssuer_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 73);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 16);
            this.label6.TabIndex = 4;
            this.label6.Text = "公鑰網址";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(15, 42);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 16);
            this.label8.TabIndex = 5;
            this.label8.Text = "發證單位";
            // 
            // err
            // 
            this.err.ContainerControl = this;
            // 
            // AdvAuthEditor
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.tab);
            this.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Name = "AdvAuthEditor";
            this.Size = new System.Drawing.Size(595, 518);
            this.Load += new System.EventHandler(this.AdvAuthEditor_Load);
            this.tab.ResumeLayout(false);
            this.tpBasic.ResumeLayout(false);
            this.tpBasic.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tpSession.ResumeLayout(false);
            this.tpSession.PerformLayout();
            this.tpPassport.ResumeLayout(false);
            this.tpPassport.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCheck)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgExtProp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.err)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tab;
        private System.Windows.Forms.TabPage tpBasic;
        private System.Windows.Forms.TabPage tpSession;
        private System.Windows.Forms.CheckBox chkBasic;
        private System.Windows.Forms.ComboBox cbHashProvider;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkSession;
        private System.Windows.Forms.TextBox txtTimeout;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TabPage tpPassport;
        private System.Windows.Forms.CheckBox chkPassport;
        private System.Windows.Forms.TextBox txtCertProvider;
        private System.Windows.Forms.TextBox txtIssuer;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cboMappingField;
        private System.Windows.Forms.ComboBox cbUserNameField;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cbALTable;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RadioButton rbDisable;
        private System.Windows.Forms.RadioButton rbEnable;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.DataGridView dgExtProp;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDBField;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAlias;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.PictureBox picCheck;
        private System.Windows.Forms.ErrorProvider err;
        private Editor.SQLText txtGetUserRoleQuery;
        private Editor.SQLText txtGetUserDataQuery;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
