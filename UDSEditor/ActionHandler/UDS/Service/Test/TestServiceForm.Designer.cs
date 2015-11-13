namespace ProjectManager.ActionHandler.UDS.Service.Test
{
    partial class TestServiceForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestServiceForm));
            this.txtService = new System.Windows.Forms.TextBox();
            this.txtContract = new System.Windows.Forms.TextBox();
            this.txtSiteURL = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tpHeader = new System.Windows.Forms.TabControl();
            this.tpContent = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbtnSend = new System.Windows.Forms.ToolStripButton();
            this.cmTemp = new System.Windows.Forms.ToolStripButton();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.cmAuth = new System.Windows.Forms.ToolStripMenuItem();
            this.passport登入ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmSSLEnable = new System.Windows.Forms.ToolStripMenuItem();
            this.cmSSLDisable = new System.Windows.Forms.ToolStripMenuItem();
            this.tsLabelTime = new System.Windows.Forms.Label();
            this.txtRequest = new ProjectManager.Editor.XmlEditor();
            this.txtResponse = new ProjectManager.Editor.XmlEditor();
            this.txtHeader = new ProjectManager.Editor.XmlEditor();
            this.tpHeader.SuspendLayout();
            this.tpContent.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtService
            // 
            this.txtService.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtService.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtService.Location = new System.Drawing.Point(698, 31);
            this.txtService.Name = "txtService";
            this.txtService.ReadOnly = true;
            this.txtService.Size = new System.Drawing.Size(203, 23);
            this.txtService.TabIndex = 2;
            // 
            // txtContract
            // 
            this.txtContract.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtContract.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtContract.Location = new System.Drawing.Point(494, 31);
            this.txtContract.Name = "txtContract";
            this.txtContract.ReadOnly = true;
            this.txtContract.Size = new System.Drawing.Size(128, 23);
            this.txtContract.TabIndex = 2;
            // 
            // txtSiteURL
            // 
            this.txtSiteURL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSiteURL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSiteURL.Location = new System.Drawing.Point(89, 31);
            this.txtSiteURL.Name = "txtSiteURL";
            this.txtSiteURL.ReadOnly = true;
            this.txtSiteURL.Size = new System.Drawing.Size(337, 23);
            this.txtSiteURL.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(634, 34);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 16);
            this.label5.TabIndex = 1;
            this.label5.Text = "Service";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(432, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 16);
            this.label4.TabIndex = 1;
            this.label4.Text = "Contract";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "AccessPoint";
            // 
            // tpHeader
            // 
            this.tpHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tpHeader.Controls.Add(this.tpContent);
            this.tpHeader.Controls.Add(this.tabPage2);
            this.tpHeader.Location = new System.Drawing.Point(12, 314);
            this.tpHeader.Name = "tpHeader";
            this.tpHeader.SelectedIndex = 0;
            this.tpHeader.Size = new System.Drawing.Size(889, 255);
            this.tpHeader.TabIndex = 1;
            // 
            // tpContent
            // 
            this.tpContent.Controls.Add(this.txtResponse);
            this.tpContent.Location = new System.Drawing.Point(4, 25);
            this.tpContent.Name = "tpContent";
            this.tpContent.Padding = new System.Windows.Forms.Padding(3);
            this.tpContent.Size = new System.Drawing.Size(881, 226);
            this.tpContent.TabIndex = 0;
            this.tpContent.Text = "Content";
            this.tpContent.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txtHeader);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(881, 226);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Header";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnSend,
            this.cmTemp,
            this.toolStripDropDownButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(913, 25);
            this.toolStrip1.TabIndex = 0;
            // 
            // tsbtnSend
            // 
            this.tsbtnSend.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnSend.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnSend.Image")));
            this.tsbtnSend.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnSend.Name = "tsbtnSend";
            this.tsbtnSend.Size = new System.Drawing.Size(23, 22);
            this.tsbtnSend.Text = "執行(&F5)";
            this.tsbtnSend.Click += new System.EventHandler(this.tsbtnSend_Click);
            // 
            // cmTemp
            // 
            this.cmTemp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmTemp.Image = ((System.Drawing.Image)(resources.GetObject("cmTemp.Image")));
            this.cmTemp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmTemp.Name = "cmTemp";
            this.cmTemp.Size = new System.Drawing.Size(23, 22);
            this.cmTemp.Text = "產生 Request 文件";
            this.cmTemp.Click += new System.EventHandler(this.cmTemp_Click);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmAuth,
            this.passport登入ToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(29, 22);
            this.toolStripDropDownButton1.Text = "設定";
            // 
            // cmAuth
            // 
            this.cmAuth.Name = "cmAuth";
            this.cmAuth.Size = new System.Drawing.Size(138, 22);
            this.cmAuth.Text = "授權設定(&A)";
            this.cmAuth.Click += new System.EventHandler(this.cmAuth_Click);
            // 
            // passport登入ToolStripMenuItem
            // 
            this.passport登入ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmSSLEnable,
            this.cmSSLDisable});
            this.passport登入ToolStripMenuItem.Name = "passport登入ToolStripMenuItem";
            this.passport登入ToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.passport登入ToolStripMenuItem.Text = "加密傳輸";
            // 
            // cmSSLEnable
            // 
            this.cmSSLEnable.CheckOnClick = true;
            this.cmSSLEnable.Name = "cmSSLEnable";
            this.cmSSLEnable.Size = new System.Drawing.Size(128, 22);
            this.cmSSLEnable.Text = "使用(&Y)";
            this.cmSSLEnable.Click += new System.EventHandler(this.cmSSLEnable_Click);
            // 
            // cmSSLDisable
            // 
            this.cmSSLDisable.Checked = true;
            this.cmSSLDisable.CheckOnClick = true;
            this.cmSSLDisable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cmSSLDisable.Name = "cmSSLDisable";
            this.cmSSLDisable.Size = new System.Drawing.Size(128, 22);
            this.cmSSLDisable.Text = "不使用(&N)";
            this.cmSSLDisable.Click += new System.EventHandler(this.cmSSLDisable_Click);
            // 
            // tsLabelTime
            // 
            this.tsLabelTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tsLabelTime.AutoSize = true;
            this.tsLabelTime.Location = new System.Drawing.Point(9, 572);
            this.tsLabelTime.Name = "tsLabelTime";
            this.tsLabelTime.Size = new System.Drawing.Size(56, 16);
            this.tsLabelTime.TabIndex = 4;
            this.tsLabelTime.Text = "花費時間";
            // 
            // txtRequest
            // 
            this.txtRequest.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRequest.Location = new System.Drawing.Point(12, 65);
            this.txtRequest.Margin = new System.Windows.Forms.Padding(3, 207, 3, 207);
            this.txtRequest.Name = "txtRequest";
            this.txtRequest.Size = new System.Drawing.Size(889, 236);
            this.txtRequest.TabIndex = 3;
            // 
            // txtResponse
            // 
            this.txtResponse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtResponse.Location = new System.Drawing.Point(3, 3);
            this.txtResponse.Margin = new System.Windows.Forms.Padding(3, 116, 3, 116);
            this.txtResponse.Name = "txtResponse";
            this.txtResponse.Size = new System.Drawing.Size(875, 220);
            this.txtResponse.TabIndex = 0;
            // 
            // txtHeader
            // 
            this.txtHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtHeader.Location = new System.Drawing.Point(3, 3);
            this.txtHeader.Margin = new System.Windows.Forms.Padding(3, 116, 3, 116);
            this.txtHeader.Name = "txtHeader";
            this.txtHeader.Size = new System.Drawing.Size(875, 223);
            this.txtHeader.TabIndex = 0;
            // 
            // TestServiceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(913, 597);
            this.Controls.Add(this.tsLabelTime);
            this.Controls.Add(this.txtRequest);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.tpHeader);
            this.Controls.Add(this.txtService);
            this.Controls.Add(this.txtContract);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSiteURL);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "TestServiceForm";
            this.Text = "測試 Service";
            this.Load += new System.EventHandler(this.TestServiceForm_Load);
            this.tpHeader.ResumeLayout(false);
            this.tpContent.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbtnSend;
        private System.Windows.Forms.ToolStripButton cmTemp;
        private System.Windows.Forms.TextBox txtSiteURL;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem cmAuth;
        private System.Windows.Forms.TextBox txtService;
        private System.Windows.Forms.TextBox txtContract;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TabControl tpHeader;
        private System.Windows.Forms.TabPage tpContent;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ToolStripMenuItem passport登入ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cmSSLEnable;
        private System.Windows.Forms.ToolStripMenuItem cmSSLDisable;
        private Editor.XmlEditor txtRequest;
        private Editor.XmlEditor txtResponse;
        private Editor.XmlEditor txtHeader;
        private System.Windows.Forms.Label tsLabelTime;

    }
}