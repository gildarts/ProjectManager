namespace ProjectManager.ActionHandler
{
    partial class DeployForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeployForm));
            this.txtVersion = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnDeploy = new System.Windows.Forms.Button();
            this.err = new System.Windows.Forms.ErrorProvider(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.txtLastest = new System.Windows.Forms.TextBox();
            this.lblInfo = new System.Windows.Forms.Label();
            this.txtProvider = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dgCommand = new System.Windows.Forms.DataGridView();
            this.colType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnDeleteCmd = new System.Windows.Forms.Button();
            this.btnAddCmd = new System.Windows.Forms.Button();
            this.gbCommand = new System.Windows.Forms.GroupBox();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnMatch = new System.Windows.Forms.Button();
            this.btnEditAll = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.rbNew = new System.Windows.Forms.RadioButton();
            this.rbExtend = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPreVerURL = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.err)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgCommand)).BeginInit();
            this.gbCommand.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtVersion
            // 
            this.txtVersion.Location = new System.Drawing.Point(85, 63);
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.Size = new System.Drawing.Size(126, 23);
            this.txtVersion.TabIndex = 0;
            this.txtVersion.Text = "1.0.0.0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "版本編號";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Location = new System.Drawing.Point(36, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(469, 48);
            this.label2.TabIndex = 1;
            this.label2.Text = "佈署前請先確定專案路徑, 本程式將自動在專案檔案中建立 udm.xml 專案檔,並同步上傳資料夾內所有檔案\r\n\r\n";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(352, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 16);
            this.label3.TabIndex = 1;
            this.label3.Text = "( 範例 : 1.0.14.8)";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // btnDeploy
            // 
            this.btnDeploy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeploy.Location = new System.Drawing.Point(430, 517);
            this.btnDeploy.Name = "btnDeploy";
            this.btnDeploy.Size = new System.Drawing.Size(75, 23);
            this.btnDeploy.TabIndex = 4;
            this.btnDeploy.Text = "開始佈署";
            this.btnDeploy.UseVisualStyleBackColor = true;
            this.btnDeploy.Click += new System.EventHandler(this.btnDeploy_Click);
            // 
            // err
            // 
            this.err.ContainerControl = this;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(56, 183);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 16);
            this.label4.TabIndex = 1;
            this.label4.Text = "前版編號";
            // 
            // txtLastest
            // 
            this.txtLastest.Location = new System.Drawing.Point(123, 183);
            this.txtLastest.Name = "txtLastest";
            this.txtLastest.ReadOnly = true;
            this.txtLastest.Size = new System.Drawing.Size(388, 23);
            this.txtLastest.TabIndex = 3;
            this.txtLastest.TabStop = false;
            this.txtLastest.Text = "-- 無 --";
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(28, 520);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(0, 16);
            this.lblInfo.TabIndex = 1;
            // 
            // txtProvider
            // 
            this.txtProvider.Location = new System.Drawing.Point(279, 63);
            this.txtProvider.Name = "txtProvider";
            this.txtProvider.Size = new System.Drawing.Size(228, 23);
            this.txtProvider.TabIndex = 1;
            this.txtProvider.Text = "ischool";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(229, 66);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 16);
            this.label5.TabIndex = 1;
            this.label5.Text = "提供者";
            // 
            // dgCommand
            // 
            this.dgCommand.AllowUserToAddRows = false;
            this.dgCommand.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgCommand.BackgroundColor = System.Drawing.Color.White;
            this.dgCommand.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgCommand.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colType,
            this.colDesc});
            this.dgCommand.Location = new System.Drawing.Point(17, 31);
            this.dgCommand.Name = "dgCommand";
            this.dgCommand.RowTemplate.Height = 24;
            this.dgCommand.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgCommand.Size = new System.Drawing.Size(413, 218);
            this.dgCommand.TabIndex = 0;
            this.dgCommand.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgCommand_KeyUp);
            // 
            // colType
            // 
            this.colType.HeaderText = "動作類型";
            this.colType.Name = "colType";
            // 
            // colDesc
            // 
            this.colDesc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colDesc.HeaderText = "描述";
            this.colDesc.Name = "colDesc";
            // 
            // btnDeleteCmd
            // 
            this.btnDeleteCmd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteCmd.Location = new System.Drawing.Point(386, 255);
            this.btnDeleteCmd.Name = "btnDeleteCmd";
            this.btnDeleteCmd.Size = new System.Drawing.Size(44, 23);
            this.btnDeleteCmd.TabIndex = 6;
            this.btnDeleteCmd.Text = "刪除";
            this.btnDeleteCmd.UseVisualStyleBackColor = true;
            this.btnDeleteCmd.Click += new System.EventHandler(this.btnDeleteCmd_Click);
            // 
            // btnAddCmd
            // 
            this.btnAddCmd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddCmd.Location = new System.Drawing.Point(336, 255);
            this.btnAddCmd.Name = "btnAddCmd";
            this.btnAddCmd.Size = new System.Drawing.Size(44, 23);
            this.btnAddCmd.TabIndex = 5;
            this.btnAddCmd.Text = "新增";
            this.btnAddCmd.UseVisualStyleBackColor = true;
            this.btnAddCmd.Click += new System.EventHandler(this.btnAddCmd_Click);
            // 
            // gbCommand
            // 
            this.gbCommand.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbCommand.Controls.Add(this.btnDown);
            this.gbCommand.Controls.Add(this.btnUp);
            this.gbCommand.Controls.Add(this.btnAddCmd);
            this.gbCommand.Controls.Add(this.dgCommand);
            this.gbCommand.Controls.Add(this.btnMatch);
            this.gbCommand.Controls.Add(this.btnEditAll);
            this.gbCommand.Controls.Add(this.btnDeleteCmd);
            this.gbCommand.Controls.Add(this.label7);
            this.gbCommand.Location = new System.Drawing.Point(26, 227);
            this.gbCommand.Name = "gbCommand";
            this.gbCommand.Size = new System.Drawing.Size(483, 284);
            this.gbCommand.TabIndex = 4;
            this.gbCommand.TabStop = false;
            this.gbCommand.Text = "UDT 異動";
            // 
            // btnDown
            // 
            this.btnDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDown.Image = ((System.Drawing.Image)(resources.GetObject("btnDown.Image")));
            this.btnDown.Location = new System.Drawing.Point(436, 67);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(30, 30);
            this.btnDown.TabIndex = 2;
            this.btnDown.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnUp
            // 
            this.btnUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUp.Image = ((System.Drawing.Image)(resources.GetObject("btnUp.Image")));
            this.btnUp.Location = new System.Drawing.Point(436, 31);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(30, 30);
            this.btnUp.TabIndex = 1;
            this.btnUp.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnMatch
            // 
            this.btnMatch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnMatch.Location = new System.Drawing.Point(92, 255);
            this.btnMatch.Name = "btnMatch";
            this.btnMatch.Size = new System.Drawing.Size(69, 23);
            this.btnMatch.TabIndex = 4;
            this.btnMatch.Text = "比對編輯";
            this.btnMatch.UseVisualStyleBackColor = true;
            this.btnMatch.Click += new System.EventHandler(this.btnMatch_Click);
            // 
            // btnEditAll
            // 
            this.btnEditAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEditAll.Location = new System.Drawing.Point(17, 255);
            this.btnEditAll.Name = "btnEditAll";
            this.btnEditAll.Size = new System.Drawing.Size(69, 23);
            this.btnEditAll.TabIndex = 3;
            this.btnEditAll.Text = "整份編輯";
            this.btnEditAll.UseVisualStyleBackColor = true;
            this.btnEditAll.Click += new System.EventHandler(this.btnEditAll_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(279, 258);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 16);
            this.label7.TabIndex = 1;
            this.label7.Text = "單項編輯";
            // 
            // rbNew
            // 
            this.rbNew.AutoSize = true;
            this.rbNew.Checked = true;
            this.rbNew.Location = new System.Drawing.Point(26, 101);
            this.rbNew.Name = "rbNew";
            this.rbNew.Size = new System.Drawing.Size(86, 20);
            this.rbNew.TabIndex = 5;
            this.rbNew.TabStop = true;
            this.rbNew.Text = "建立新版本";
            this.rbNew.UseVisualStyleBackColor = true;
            // 
            // rbExtend
            // 
            this.rbExtend.AutoSize = true;
            this.rbExtend.Location = new System.Drawing.Point(26, 127);
            this.rbExtend.Name = "rbExtend";
            this.rbExtend.Size = new System.Drawing.Size(98, 20);
            this.rbExtend.TabIndex = 5;
            this.rbExtend.Text = "延續先前版本";
            this.rbExtend.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(25, 156);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 16);
            this.label6.TabIndex = 7;
            this.label6.Text = "前版 PML 網址";
            // 
            // txtPreVerURL
            // 
            this.txtPreVerURL.Location = new System.Drawing.Point(123, 153);
            this.txtPreVerURL.Name = "txtPreVerURL";
            this.txtPreVerURL.Size = new System.Drawing.Size(388, 23);
            this.txtPreVerURL.TabIndex = 2;
            this.txtPreVerURL.Text = "http://";
            // 
            // DeployForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(538, 552);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtPreVerURL);
            this.Controls.Add(this.rbExtend);
            this.Controls.Add(this.rbNew);
            this.Controls.Add(this.gbCommand);
            this.Controls.Add(this.btnDeploy);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtLastest);
            this.Controls.Add(this.txtProvider);
            this.Controls.Add(this.txtVersion);
            this.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(554, 590);
            this.Name = "DeployForm";
            this.Text = "UDT/UDS 佈署設定";
            this.Load += new System.EventHandler(this.DeployForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.err)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgCommand)).EndInit();
            this.gbCommand.ResumeLayout(false);
            this.gbCommand.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtVersion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnDeploy;
        private System.Windows.Forms.ErrorProvider err;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtLastest;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtProvider;
        private System.Windows.Forms.DataGridView dgCommand;
        private System.Windows.Forms.DataGridViewTextBoxColumn colType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDesc;
        private System.Windows.Forms.Button btnAddCmd;
        private System.Windows.Forms.Button btnDeleteCmd;
        private System.Windows.Forms.GroupBox gbCommand;
        private System.Windows.Forms.RadioButton rbExtend;
        private System.Windows.Forms.RadioButton rbNew;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtPreVerURL;
        private System.Windows.Forms.Button btnMatch;
        private System.Windows.Forms.Button btnEditAll;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnUp;

    }
}