namespace ProjectManager.ActionHandler.UDS
{
    partial class UDSImportForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UDSImportForm));
            this.rbTSML = new System.Windows.Forms.RadioButton();
            this.rbTML = new System.Windows.Forms.RadioButton();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnBro = new System.Windows.Forms.Button();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.err = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbAdd = new System.Windows.Forms.RadioButton();
            this.rbImport = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.err)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rbTSML
            // 
            this.rbTSML.AutoSize = true;
            this.rbTSML.Checked = true;
            this.rbTSML.Location = new System.Drawing.Point(22, 22);
            this.rbTSML.Name = "rbTSML";
            this.rbTSML.Size = new System.Drawing.Size(158, 20);
            this.rbTSML.TabIndex = 0;
            this.rbTSML.TabStop = true;
            this.rbTSML.Text = "匯入專案 Contract(csml)";
            this.rbTSML.UseVisualStyleBackColor = true;
            this.rbTSML.CheckedChanged += new System.EventHandler(this.rbTSML_CheckedChanged);
            // 
            // rbTML
            // 
            this.rbTML.AutoSize = true;
            this.rbTML.Location = new System.Drawing.Point(184, 22);
            this.rbTML.Name = "rbTML";
            this.rbTML.Size = new System.Drawing.Size(156, 20);
            this.rbTML.TabIndex = 1;
            this.rbTML.Text = "匯入單一 Contract (cml)";
            this.rbTML.UseVisualStyleBackColor = true;
            this.rbTML.CheckedChanged += new System.EventHandler(this.rbTSML_CheckedChanged);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(259, 169);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 5;
            this.btnStart.Text = "開始匯入";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnBro
            // 
            this.btnBro.Location = new System.Drawing.Point(307, 140);
            this.btnBro.Name = "btnBro";
            this.btnBro.Size = new System.Drawing.Size(27, 23);
            this.btnBro.TabIndex = 4;
            this.btnBro.Text = "...";
            this.btnBro.UseVisualStyleBackColor = true;
            this.btnBro.Click += new System.EventHandler(this.btnBro_Click);
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(16, 140);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(285, 23);
            this.txtFileName.TabIndex = 3;
            // 
            // err
            // 
            this.err.ContainerControl = this;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbAdd);
            this.groupBox1.Controls.Add(this.rbImport);
            this.groupBox1.Location = new System.Drawing.Point(16, 59);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(318, 75);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "專案 Contract 內容";
            // 
            // rbAdd
            // 
            this.rbAdd.AutoSize = true;
            this.rbAdd.Location = new System.Drawing.Point(168, 22);
            this.rbAdd.Name = "rbAdd";
            this.rbAdd.Size = new System.Drawing.Size(137, 20);
            this.rbAdd.TabIndex = 1;
            this.rbAdd.Text = "外加匯入的 Contract";
            this.rbAdd.UseVisualStyleBackColor = true;
            // 
            // rbImport
            // 
            this.rbImport.AutoSize = true;
            this.rbImport.Checked = true;
            this.rbImport.Location = new System.Drawing.Point(13, 22);
            this.rbImport.Name = "rbImport";
            this.rbImport.Size = new System.Drawing.Size(134, 20);
            this.rbImport.TabIndex = 0;
            this.rbImport.TabStop = true;
            this.rbImport.Text = "與資料來源保持一致";
            this.rbImport.UseVisualStyleBackColor = true;
            // 
            // UDSImportForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(349, 206);
            this.Controls.Add(this.rbTSML);
            this.Controls.Add(this.rbTML);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnBro);
            this.Controls.Add(this.txtFileName);
            this.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UDSImportForm";
            this.Text = "匯入 Contract";
            ((System.ComponentModel.ISupportInitialize)(this.err)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rbTSML;
        private System.Windows.Forms.RadioButton rbTML;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnBro;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.ErrorProvider err;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbAdd;
        private System.Windows.Forms.RadioButton rbImport;
    }
}