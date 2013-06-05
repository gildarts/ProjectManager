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
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsLabelTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.txtRequest = new ProjectManager.Editor.XmlEditor();
            this.txtService = new System.Windows.Forms.TextBox();
            this.txtContract = new System.Windows.Forms.TextBox();
            this.txtSiteURL = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tpHeader = new System.Windows.Forms.TabControl();
            this.tpContent = new System.Windows.Forms.TabPage();
            this.txtResponse = new ProjectManager.Editor.XmlEditor();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txtHeader = new ProjectManager.Editor.XmlEditor();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbtnSend = new System.Windows.Forms.ToolStripButton();
            this.cmTemp = new System.Windows.Forms.ToolStripButton();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.cmAuth = new System.Windows.Forms.ToolStripMenuItem();
            this.passport登入ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmSSLEnable = new System.Windows.Forms.ToolStripMenuItem();
            this.cmSSLDisable = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tpHeader.SuspendLayout();
            this.tpContent.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.BottomToolStripPanel
            // 
            this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.statusStrip1);
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.splitContainer1);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(734, 465);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(734, 512);
            this.toolStripContainer1.TabIndex = 0;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsLabelTime,
            this.tsTime});
            this.statusStrip1.Location = new System.Drawing.Point(0, 0);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(734, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsLabelTime
            // 
            this.tsLabelTime.Name = "tsLabelTime";
            this.tsLabelTime.Size = new System.Drawing.Size(56, 17);
            this.tsLabelTime.Text = "執行時間";
            // 
            // tsTime
            // 
            this.tsTime.Name = "tsTime";
            this.tsTime.Size = new System.Drawing.Size(0, 17);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.txtRequest);
            this.splitContainer1.Panel1.Controls.Add(this.txtService);
            this.splitContainer1.Panel1.Controls.Add(this.txtContract);
            this.splitContainer1.Panel1.Controls.Add(this.txtSiteURL);
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tpHeader);
            this.splitContainer1.Size = new System.Drawing.Size(734, 465);
            this.splitContainer1.SplitterDistance = 254;
            this.splitContainer1.TabIndex = 0;
            // 
            // txtRequest
            // 
            this.txtRequest.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRequest.Location = new System.Drawing.Point(8, 40);
            this.txtRequest.Margin = new System.Windows.Forms.Padding(3, 7, 3, 7);
            this.txtRequest.Name = "txtRequest";
            this.txtRequest.Size = new System.Drawing.Size(714, 207);
            this.txtRequest.TabIndex = 3;
            // 
            // txtService
            // 
            this.txtService.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtService.Location = new System.Drawing.Point(524, 7);
            this.txtService.Name = "txtService";
            this.txtService.ReadOnly = true;
            this.txtService.Size = new System.Drawing.Size(203, 23);
            this.txtService.TabIndex = 2;
            // 
            // txtContract
            // 
            this.txtContract.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtContract.Location = new System.Drawing.Point(320, 7);
            this.txtContract.Name = "txtContract";
            this.txtContract.ReadOnly = true;
            this.txtContract.Size = new System.Drawing.Size(128, 23);
            this.txtContract.TabIndex = 2;
            // 
            // txtSiteURL
            // 
            this.txtSiteURL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSiteURL.Location = new System.Drawing.Point(92, 5);
            this.txtSiteURL.Name = "txtSiteURL";
            this.txtSiteURL.ReadOnly = true;
            this.txtSiteURL.Size = new System.Drawing.Size(160, 23);
            this.txtSiteURL.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(460, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 16);
            this.label5.TabIndex = 1;
            this.label5.Text = "Service";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(258, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 16);
            this.label4.TabIndex = 1;
            this.label4.Text = "Contract";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "AccessPoint";
            // 
            // tpHeader
            // 
            this.tpHeader.Controls.Add(this.tpContent);
            this.tpHeader.Controls.Add(this.tabPage2);
            this.tpHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tpHeader.Location = new System.Drawing.Point(0, 0);
            this.tpHeader.Name = "tpHeader";
            this.tpHeader.SelectedIndex = 0;
            this.tpHeader.Size = new System.Drawing.Size(734, 207);
            this.tpHeader.TabIndex = 1;
            // 
            // tpContent
            // 
            this.tpContent.Controls.Add(this.txtResponse);
            this.tpContent.Location = new System.Drawing.Point(4, 25);
            this.tpContent.Name = "tpContent";
            this.tpContent.Padding = new System.Windows.Forms.Padding(3);
            this.tpContent.Size = new System.Drawing.Size(726, 178);
            this.tpContent.TabIndex = 0;
            this.tpContent.Text = "Content";
            this.tpContent.UseVisualStyleBackColor = true;
            // 
            // txtResponse
            // 
            this.txtResponse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtResponse.Location = new System.Drawing.Point(3, 3);
            this.txtResponse.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtResponse.Name = "txtResponse";
            this.txtResponse.Size = new System.Drawing.Size(720, 172);
            this.txtResponse.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txtHeader);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(726, 181);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Header";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // txtHeader
            // 
            this.txtHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtHeader.Location = new System.Drawing.Point(3, 3);
            this.txtHeader.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtHeader.Name = "txtHeader";
            this.txtHeader.Size = new System.Drawing.Size(720, 175);
            this.txtHeader.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnSend,
            this.cmTemp,
            this.toolStripDropDownButton1});
            this.toolStrip1.Location = new System.Drawing.Point(3, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(87, 25);
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
            this.cmAuth.Size = new System.Drawing.Size(140, 22);
            this.cmAuth.Text = "授權設定(&A)";
            this.cmAuth.Click += new System.EventHandler(this.cmAuth_Click);
            // 
            // passport登入ToolStripMenuItem
            // 
            this.passport登入ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmSSLEnable,
            this.cmSSLDisable});
            this.passport登入ToolStripMenuItem.Name = "passport登入ToolStripMenuItem";
            this.passport登入ToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.passport登入ToolStripMenuItem.Text = "加密傳輸";
            // 
            // cmSSLEnable
            // 
            this.cmSSLEnable.Checked = true;
            this.cmSSLEnable.CheckOnClick = true;
            this.cmSSLEnable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cmSSLEnable.Name = "cmSSLEnable";
            this.cmSSLEnable.Size = new System.Drawing.Size(130, 22);
            this.cmSSLEnable.Text = "使用(&Y)";
            this.cmSSLEnable.Click += new System.EventHandler(this.cmSSLEnable_Click);
            // 
            // cmSSLDisable
            // 
            this.cmSSLDisable.CheckOnClick = true;
            this.cmSSLDisable.Name = "cmSSLDisable";
            this.cmSSLDisable.Size = new System.Drawing.Size(130, 22);
            this.cmSSLDisable.Text = "不使用(&N)";
            this.cmSSLDisable.Click += new System.EventHandler(this.cmSSLDisable_Click);
            // 
            // TestServiceForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(734, 512);
            this.Controls.Add(this.toolStripContainer1);
            this.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "TestServiceForm";
            this.Text = "測試 Service";
            this.Load += new System.EventHandler(this.TestServiceForm_Load);
            this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tpHeader.ResumeLayout(false);
            this.tpContent.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsLabelTime;
        private System.Windows.Forms.SplitContainer splitContainer1;
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
        private System.Windows.Forms.ToolStripStatusLabel tsTime;
        private Editor.XmlEditor txtRequest;
        private Editor.XmlEditor txtResponse;
        private Editor.XmlEditor txtHeader;

    }
}