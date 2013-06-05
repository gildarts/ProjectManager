namespace ProjectManager.ActionHandler.UDS.Contract
{
    partial class AddContractForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddContractForm));
            this.label1 = new System.Windows.Forms.Label();
            this.rbTA = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.rbSA = new System.Windows.Forms.RadioButton();
            this.rbAdmin = new System.Windows.Forms.RadioButton();
            this.rbOther = new System.Windows.Forms.RadioButton();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtName = new System.Windows.Forms.TextBox();
            this.err = new System.Windows.Forms.ErrorProvider(this.components);
            this.rbPublic = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.err)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Contract 名稱";
            // 
            // rbTA
            // 
            this.rbTA.AutoSize = true;
            this.rbTA.Location = new System.Drawing.Point(157, 48);
            this.rbTA.Name = "rbTA";
            this.rbTA.Size = new System.Drawing.Size(50, 20);
            this.rbTA.TabIndex = 3;
            this.rbTA.Text = "教師";
            this.rbTA.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "使用者身份";
            // 
            // rbSA
            // 
            this.rbSA.AutoSize = true;
            this.rbSA.Location = new System.Drawing.Point(213, 48);
            this.rbSA.Name = "rbSA";
            this.rbSA.Size = new System.Drawing.Size(50, 20);
            this.rbSA.TabIndex = 4;
            this.rbSA.Text = "學生";
            this.rbSA.UseVisualStyleBackColor = true;
            // 
            // rbAdmin
            // 
            this.rbAdmin.AutoSize = true;
            this.rbAdmin.Location = new System.Drawing.Point(269, 48);
            this.rbAdmin.Name = "rbAdmin";
            this.rbAdmin.Size = new System.Drawing.Size(86, 20);
            this.rbAdmin.TabIndex = 5;
            this.rbAdmin.Text = "系統管理員";
            this.rbAdmin.UseVisualStyleBackColor = true;
            // 
            // rbOther
            // 
            this.rbOther.AutoSize = true;
            this.rbOther.Location = new System.Drawing.Point(361, 48);
            this.rbOther.Name = "rbOther";
            this.rbOther.Size = new System.Drawing.Size(50, 20);
            this.rbOther.TabIndex = 6;
            this.rbOther.Text = "其它";
            this.rbOther.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(336, 95);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 7;
            this.btnAdd.Text = "確定";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(102, 13);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(309, 23);
            this.txtName.TabIndex = 0;
            // 
            // err
            // 
            this.err.ContainerControl = this;
            // 
            // rbPublic
            // 
            this.rbPublic.AutoSize = true;
            this.rbPublic.Checked = true;
            this.rbPublic.Location = new System.Drawing.Point(102, 48);
            this.rbPublic.Name = "rbPublic";
            this.rbPublic.Size = new System.Drawing.Size(50, 20);
            this.rbPublic.TabIndex = 2;
            this.rbPublic.TabStop = true;
            this.rbPublic.Text = "公開";
            this.rbPublic.UseVisualStyleBackColor = true;
            // 
            // AddContractForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(429, 130);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.rbOther);
            this.Controls.Add(this.rbAdmin);
            this.Controls.Add(this.rbSA);
            this.Controls.Add(this.rbPublic);
            this.Controls.Add(this.rbTA);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddContractForm";
            this.Text = "新增 Contract";
            ((System.ComponentModel.ISupportInitialize)(this.err)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbTA;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rbSA;
        private System.Windows.Forms.RadioButton rbAdmin;
        private System.Windows.Forms.RadioButton rbOther;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.ErrorProvider err;
        private System.Windows.Forms.RadioButton rbPublic;
    }
}