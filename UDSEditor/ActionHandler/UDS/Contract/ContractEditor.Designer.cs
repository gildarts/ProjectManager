namespace ProjectManager.ActionHandler.UDS.Contract
{
    partial class ContractEditor
    {
        public string DocumentTitle { get { return string.Empty; } }

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
            this.txtName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.rbOther = new System.Windows.Forms.RadioButton();
            this.rbAdmin = new System.Windows.Forms.RadioButton();
            this.rbSA = new System.Windows.Forms.RadioButton();
            this.rbPublic = new System.Windows.Forms.RadioButton();
            this.rbTA = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.panel = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.rbParent = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(107, 22);
            this.txtName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(390, 23);
            this.txtName.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Contract 名稱";
            // 
            // rbOther
            // 
            this.rbOther.AutoSize = true;
            this.rbOther.Location = new System.Drawing.Point(422, 52);
            this.rbOther.Name = "rbOther";
            this.rbOther.Size = new System.Drawing.Size(50, 20);
            this.rbOther.TabIndex = 6;
            this.rbOther.Text = "其它";
            this.rbOther.UseVisualStyleBackColor = true;
            this.rbOther.CheckedChanged += new System.EventHandler(this.rbOther_CheckedChanged);
            // 
            // rbAdmin
            // 
            this.rbAdmin.AutoSize = true;
            this.rbAdmin.Location = new System.Drawing.Point(330, 52);
            this.rbAdmin.Name = "rbAdmin";
            this.rbAdmin.Size = new System.Drawing.Size(86, 20);
            this.rbAdmin.TabIndex = 5;
            this.rbAdmin.Text = "系統管理員";
            this.rbAdmin.UseVisualStyleBackColor = true;
            this.rbAdmin.CheckedChanged += new System.EventHandler(this.rbAdmin_CheckedChanged);
            // 
            // rbSA
            // 
            this.rbSA.AutoSize = true;
            this.rbSA.Location = new System.Drawing.Point(218, 52);
            this.rbSA.Name = "rbSA";
            this.rbSA.Size = new System.Drawing.Size(50, 20);
            this.rbSA.TabIndex = 4;
            this.rbSA.Text = "學生";
            this.rbSA.UseVisualStyleBackColor = true;
            this.rbSA.CheckedChanged += new System.EventHandler(this.rbSA_CheckedChanged);
            // 
            // rbPublic
            // 
            this.rbPublic.AutoSize = true;
            this.rbPublic.Checked = true;
            this.rbPublic.Location = new System.Drawing.Point(107, 52);
            this.rbPublic.Name = "rbPublic";
            this.rbPublic.Size = new System.Drawing.Size(50, 20);
            this.rbPublic.TabIndex = 2;
            this.rbPublic.TabStop = true;
            this.rbPublic.Text = "公開";
            this.rbPublic.UseVisualStyleBackColor = true;
            this.rbPublic.CheckedChanged += new System.EventHandler(this.rbPublic_CheckedChanged);
            // 
            // rbTA
            // 
            this.rbTA.AutoSize = true;
            this.rbTA.Location = new System.Drawing.Point(162, 52);
            this.rbTA.Name = "rbTA";
            this.rbTA.Size = new System.Drawing.Size(50, 20);
            this.rbTA.TabIndex = 3;
            this.rbTA.Text = "教師";
            this.rbTA.UseVisualStyleBackColor = true;
            this.rbTA.CheckedChanged += new System.EventHandler(this.rbTA_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "使用者身份";
            // 
            // panel
            // 
            this.panel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel.Location = new System.Drawing.Point(24, 92);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(473, 498);
            this.panel.TabIndex = 7;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(422, 608);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "儲存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // rbParent
            // 
            this.rbParent.AutoSize = true;
            this.rbParent.Location = new System.Drawing.Point(274, 52);
            this.rbParent.Name = "rbParent";
            this.rbParent.Size = new System.Drawing.Size(50, 20);
            this.rbParent.TabIndex = 4;
            this.rbParent.Text = "家長";
            this.rbParent.UseVisualStyleBackColor = true;
            this.rbParent.CheckedChanged += new System.EventHandler(this.rbParent_CheckedChanged);
            // 
            // ContractEditor
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.rbOther);
            this.Controls.Add(this.rbAdmin);
            this.Controls.Add(this.rbParent);
            this.Controls.Add(this.rbSA);
            this.Controls.Add(this.rbPublic);
            this.Controls.Add(this.rbTA);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ContractEditor";
            this.Size = new System.Drawing.Size(526, 653);
            this.Load += new System.EventHandler(this.ContractEditor_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbOther;
        private System.Windows.Forms.RadioButton rbAdmin;
        private System.Windows.Forms.RadioButton rbSA;
        private System.Windows.Forms.RadioButton rbPublic;
        private System.Windows.Forms.RadioButton rbTA;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.RadioButton rbParent;
    }
}
