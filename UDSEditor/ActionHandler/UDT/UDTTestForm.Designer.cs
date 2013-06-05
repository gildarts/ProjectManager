namespace ProjectManager.ActionHandler.UDT
{
    partial class UDTTestForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UDTTestForm));
            ActiproSoftware.SyntaxEditor.Document document5 = new ActiproSoftware.SyntaxEditor.Document();
            ActiproSoftware.SyntaxEditor.Document document6 = new ActiproSoftware.SyntaxEditor.Document();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbtnExecute = new System.Windows.Forms.ToolStripButton();
            this.tsbtnSave = new System.Windows.Forms.ToolStripButton();
            this.rsbtnOpen = new System.Windows.Forms.ToolStripButton();
            this.txtSQL = new ActiproSoftware.SyntaxEditor.SyntaxEditor();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpResult = new System.Windows.Forms.TabPage();
            this.dgResult = new System.Windows.Forms.DataGridView();
            this.tpInfo = new System.Windows.Forms.TabPage();
            this.txtInfo = new ActiproSoftware.SyntaxEditor.SyntaxEditor();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.cmRight = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmTextMode = new System.Windows.Forms.ToolStripMenuItem();
            this.cmXmlMode = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpResult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgResult)).BeginInit();
            this.tpInfo.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.cmRight.SuspendLayout();
            this.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.toolStrip1);
            this.splitContainer1.Panel1.Controls.Add(this.txtSQL);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Panel2.Controls.Add(this.statusStrip1);
            this.splitContainer1.Size = new System.Drawing.Size(698, 504);
            this.splitContainer1.SplitterDistance = 232;
            this.splitContainer1.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnExecute,
            this.tsbtnSave,
            this.rsbtnOpen});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(698, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbtnExecute
            // 
            this.tsbtnExecute.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnExecute.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnExecute.Image")));
            this.tsbtnExecute.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnExecute.Name = "tsbtnExecute";
            this.tsbtnExecute.Size = new System.Drawing.Size(23, 22);
            this.tsbtnExecute.Text = "執行(F5)";
            this.tsbtnExecute.Click += new System.EventHandler(this.tsbtnExecute_Click);
            // 
            // tsbtnSave
            // 
            this.tsbtnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnSave.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnSave.Image")));
            this.tsbtnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnSave.Name = "tsbtnSave";
            this.tsbtnSave.Size = new System.Drawing.Size(23, 22);
            this.tsbtnSave.Text = "儲存";
            this.tsbtnSave.Click += new System.EventHandler(this.tsbtnSave_Click);
            // 
            // rsbtnOpen
            // 
            this.rsbtnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.rsbtnOpen.Image = ((System.Drawing.Image)(resources.GetObject("rsbtnOpen.Image")));
            this.rsbtnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.rsbtnOpen.Name = "rsbtnOpen";
            this.rsbtnOpen.Size = new System.Drawing.Size(23, 22);
            this.rsbtnOpen.Text = "開啟檔案";
            this.rsbtnOpen.Click += new System.EventHandler(this.rsbtnOpen_Click);
            // 
            // txtSQL
            // 
            this.txtSQL.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            document5.Text = "SELECT * FROM";
            this.txtSQL.Document = document5;
            this.txtSQL.Location = new System.Drawing.Point(3, 28);
            this.txtSQL.Name = "txtSQL";
            this.txtSQL.Size = new System.Drawing.Size(692, 204);
            this.txtSQL.TabIndex = 0;
            this.txtSQL.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtSQL_KeyUp);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpResult);
            this.tabControl1.Controls.Add(this.tpInfo);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(698, 246);
            this.tabControl1.TabIndex = 1;
            // 
            // tpResult
            // 
            this.tpResult.Controls.Add(this.dgResult);
            this.tpResult.Location = new System.Drawing.Point(4, 25);
            this.tpResult.Name = "tpResult";
            this.tpResult.Padding = new System.Windows.Forms.Padding(3);
            this.tpResult.Size = new System.Drawing.Size(690, 217);
            this.tpResult.TabIndex = 0;
            this.tpResult.Text = "資料輸出";
            this.tpResult.UseVisualStyleBackColor = true;
            // 
            // dgResult
            // 
            this.dgResult.AllowUserToAddRows = false;
            this.dgResult.AllowUserToDeleteRows = false;
            this.dgResult.BackgroundColor = System.Drawing.Color.White;
            this.dgResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgResult.ContextMenuStrip = this.cmRight;
            this.dgResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgResult.Location = new System.Drawing.Point(3, 3);
            this.dgResult.Name = "dgResult";
            this.dgResult.ReadOnly = true;
            this.dgResult.RowTemplate.Height = 24;
            this.dgResult.Size = new System.Drawing.Size(684, 211);
            this.dgResult.TabIndex = 0;
            // 
            // tpInfo
            // 
            this.tpInfo.Controls.Add(this.txtInfo);
            this.tpInfo.Location = new System.Drawing.Point(4, 22);
            this.tpInfo.Name = "tpInfo";
            this.tpInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tpInfo.Size = new System.Drawing.Size(690, 220);
            this.tpInfo.TabIndex = 1;
            this.tpInfo.Text = "訊息";
            this.tpInfo.UseVisualStyleBackColor = true;
            // 
            // txtInfo
            // 
            this.txtInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtInfo.Document = document6;
            this.txtInfo.Location = new System.Drawing.Point(3, 3);
            this.txtInfo.Name = "txtInfo";
            this.txtInfo.Size = new System.Drawing.Size(684, 214);
            this.txtInfo.TabIndex = 0;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 246);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(698, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsLabel
            // 
            this.tsLabel.Name = "tsLabel";
            this.tsLabel.Size = new System.Drawing.Size(56, 17);
            this.tsLabel.Text = "執行時間";
            // 
            // cmRight
            // 
            this.cmRight.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmTextMode,
            this.cmXmlMode});
            this.cmRight.Name = "cmRight";
            this.cmRight.Size = new System.Drawing.Size(170, 70);
            this.cmRight.Opening += new System.ComponentModel.CancelEventHandler(this.cmRight_Opening);
            // 
            // cmTextMode
            // 
            this.cmTextMode.Name = "cmTextMode";
            this.cmTextMode.Size = new System.Drawing.Size(169, 22);
            this.cmTextMode.Text = "Text 檢視模式(&T)";
            this.cmTextMode.Click += new System.EventHandler(this.cmTextMode_Click);
            // 
            // cmXmlMode
            // 
            this.cmXmlMode.Name = "cmXmlMode";
            this.cmXmlMode.Size = new System.Drawing.Size(169, 22);
            this.cmXmlMode.Text = "XML 模視模式(&X)";
            this.cmXmlMode.Click += new System.EventHandler(this.cmXmlMode_Click);
            // 
            // UDTTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(698, 504);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "UDTTestForm";
            this.Text = "UDT 測試工具";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UDTTestForm_FormClosing);
            this.Load += new System.EventHandler(this.UDTTestForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tpResult.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgResult)).EndInit();
            this.tpInfo.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.cmRight.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbtnExecute;
        private ActiproSoftware.SyntaxEditor.SyntaxEditor txtSQL;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsLabel;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpResult;
        private System.Windows.Forms.DataGridView dgResult;
        private System.Windows.Forms.TabPage tpInfo;
        private ActiproSoftware.SyntaxEditor.SyntaxEditor txtInfo;
        private System.Windows.Forms.ToolStripButton tsbtnSave;
        private System.Windows.Forms.ToolStripButton rsbtnOpen;
        private System.Windows.Forms.ContextMenuStrip cmRight;
        private System.Windows.Forms.ToolStripMenuItem cmTextMode;
        private System.Windows.Forms.ToolStripMenuItem cmXmlMode;
    }
}