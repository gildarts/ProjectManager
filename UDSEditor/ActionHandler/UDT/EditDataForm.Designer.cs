namespace ProjectManager.ActionHandler.UDT
{
    partial class EditDataForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditDataForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnFilter = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnExecute = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tscboLimit = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.cmExportPlainText = new System.Windows.Forms.ToolStripMenuItem();
            this.cmExportCompress = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButton2 = new System.Windows.Forms.ToolStripDropDownButton();
            this.cmImportPlainText = new System.Windows.Forms.ToolStripMenuItem();
            this.cmImportCompress = new System.Windows.Forms.ToolStripMenuItem();
            this.dgData = new System.Windows.Forms.DataGridView();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator2,
            this.tsbtnFilter,
            this.toolStripSeparator4,
            this.tsbtnExecute,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.tscboLimit,
            this.toolStripSeparator3,
            this.tsbtnDelete,
            this.toolStripSeparator5,
            this.toolStripDropDownButton1,
            this.toolStripDropDownButton2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(477, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbtnFilter
            // 
            this.tsbtnFilter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnFilter.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnFilter.Image")));
            this.tsbtnFilter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnFilter.Name = "tsbtnFilter";
            this.tsbtnFilter.Size = new System.Drawing.Size(23, 22);
            this.tsbtnFilter.Text = "過瀘/排序";
            this.tsbtnFilter.Click += new System.EventHandler(this.tsbtnFilter_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbtnExecute
            // 
            this.tsbtnExecute.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnExecute.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnExecute.Image")));
            this.tsbtnExecute.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnExecute.Name = "tsbtnExecute";
            this.tsbtnExecute.Size = new System.Drawing.Size(23, 22);
            this.tsbtnExecute.Text = "執行";
            this.tsbtnExecute.Click += new System.EventHandler(this.tsbtnExecute_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(56, 22);
            this.toolStripLabel1.Text = "限制筆數";
            // 
            // tscboLimit
            // 
            this.tscboLimit.Items.AddRange(new object[] {
            "10",
            "50",
            "100",
            "500",
            "不限制",
            ""});
            this.tscboLimit.Name = "tscboLimit";
            this.tscboLimit.Size = new System.Drawing.Size(75, 25);
            this.tscboLimit.Text = "10";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbtnDelete
            // 
            this.tsbtnDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnDelete.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnDelete.Image")));
            this.tsbtnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnDelete.Name = "tsbtnDelete";
            this.tsbtnDelete.Size = new System.Drawing.Size(23, 22);
            this.tsbtnDelete.Text = "toolStripButton1";
            this.tsbtnDelete.Click += new System.EventHandler(this.tsbtnDelete_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmExportPlainText,
            this.cmExportCompress});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(29, 22);
            this.toolStripDropDownButton1.Text = "備份";
            // 
            // cmExportPlainText
            // 
            this.cmExportPlainText.Name = "cmExportPlainText";
            this.cmExportPlainText.Size = new System.Drawing.Size(152, 22);
            this.cmExportPlainText.Text = "匯出明文(&P)";
            this.cmExportPlainText.Click += new System.EventHandler(this.cmExportPlainText_Click);
            // 
            // cmExportCompress
            // 
            this.cmExportCompress.Name = "cmExportCompress";
            this.cmExportCompress.Size = new System.Drawing.Size(152, 22);
            this.cmExportCompress.Text = "匯出壓縮檔(&C)";
            this.cmExportCompress.Click += new System.EventHandler(this.cmExportCompress_Click);
            // 
            // toolStripDropDownButton2
            // 
            this.toolStripDropDownButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmImportPlainText,
            this.cmImportCompress});
            this.toolStripDropDownButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton2.Image")));
            this.toolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton2.Name = "toolStripDropDownButton2";
            this.toolStripDropDownButton2.Size = new System.Drawing.Size(29, 22);
            this.toolStripDropDownButton2.Text = "備份還原";
            this.toolStripDropDownButton2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmImportPlainText
            // 
            this.cmImportPlainText.Name = "cmImportPlainText";
            this.cmImportPlainText.Size = new System.Drawing.Size(164, 22);
            this.cmImportPlainText.Text = "從明文還原(&P)";
            this.cmImportPlainText.Click += new System.EventHandler(this.cmImportPlainText_Click);
            // 
            // cmImportCompress
            // 
            this.cmImportCompress.Name = "cmImportCompress";
            this.cmImportCompress.Size = new System.Drawing.Size(164, 22);
            this.cmImportCompress.Text = "從壓縮檔還原(&C)";
            this.cmImportCompress.Click += new System.EventHandler(this.cmImportCompress_Click);
            // 
            // dgData
            // 
            this.dgData.AllowUserToDeleteRows = false;
            this.dgData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgData.BackgroundColor = System.Drawing.Color.White;
            this.dgData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgData.Location = new System.Drawing.Point(12, 28);
            this.dgData.Name = "dgData";
            this.dgData.RowTemplate.Height = 24;
            this.dgData.Size = new System.Drawing.Size(453, 309);
            this.dgData.TabIndex = 1;
            this.dgData.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgData_CellValidating);
            this.dgData.RowValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgData_RowValidated);
            this.dgData.RowValidating += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgData_RowValidating);
            this.dgData.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgData_KeyUp);
            // 
            // EditDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(477, 349);
            this.Controls.Add(this.dgData);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "EditDataForm";
            this.Text = "編輯資料";
            this.Load += new System.EventHandler(this.EditDataForm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbtnFilter;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsbtnExecute;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripComboBox tscboLimit;
        private System.Windows.Forms.DataGridView dgData;
        private System.Windows.Forms.ToolStripButton tsbtnDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem cmExportPlainText;
        private System.Windows.Forms.ToolStripMenuItem cmExportCompress;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton2;
        private System.Windows.Forms.ToolStripMenuItem cmImportPlainText;
        private System.Windows.Forms.ToolStripMenuItem cmImportCompress;
    }
}