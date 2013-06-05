namespace ProjectManager.ActionHandler.UDS.Contract.AuthEditors
{
    partial class AddRestrictForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddRestrictForm));
            this.rbPropertyEquals = new System.Windows.Forms.RadioButton();
            this.rbRoleContain = new System.Windows.Forms.RadioButton();
            this.rbDB = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPropertyValue = new System.Windows.Forms.TextBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.txtPropertyName = new System.Windows.Forms.TextBox();
            this.txtRole = new System.Windows.Forms.TextBox();
            this.txtSQL = new ProjectManager.Editor.SQLText();
            this.SuspendLayout();
            // 
            // rbPropertyEquals
            // 
            this.rbPropertyEquals.AutoSize = true;
            this.rbPropertyEquals.Checked = true;
            this.rbPropertyEquals.Location = new System.Drawing.Point(22, 22);
            this.rbPropertyEquals.Name = "rbPropertyEquals";
            this.rbPropertyEquals.Size = new System.Drawing.Size(74, 20);
            this.rbPropertyEquals.TabIndex = 0;
            this.rbPropertyEquals.TabStop = true;
            this.rbPropertyEquals.Text = "包含屬性";
            this.rbPropertyEquals.UseVisualStyleBackColor = true;
            this.rbPropertyEquals.CheckedChanged += new System.EventHandler(this.rbPropertyEquals_CheckedChanged);
            // 
            // rbRoleContain
            // 
            this.rbRoleContain.AutoSize = true;
            this.rbRoleContain.Location = new System.Drawing.Point(22, 105);
            this.rbRoleContain.Name = "rbRoleContain";
            this.rbRoleContain.Size = new System.Drawing.Size(74, 20);
            this.rbRoleContain.TabIndex = 3;
            this.rbRoleContain.Text = "具備角色";
            this.rbRoleContain.UseVisualStyleBackColor = true;
            this.rbRoleContain.CheckedChanged += new System.EventHandler(this.rbRoleContain_CheckedChanged);
            // 
            // rbDB
            // 
            this.rbDB.AutoSize = true;
            this.rbDB.Location = new System.Drawing.Point(22, 180);
            this.rbDB.Name = "rbDB";
            this.rbDB.Size = new System.Drawing.Size(163, 20);
            this.rbDB.TabIndex = 5;
            this.rbDB.Text = "使用者必須通過 SQL 驗證";
            this.rbDB.UseVisualStyleBackColor = true;
            this.rbDB.CheckedChanged += new System.EventHandler(this.rbDB_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "使用者資訊屬性";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(241, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "必須等於";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(45, 128);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "使用者必須擁有角色";
            // 
            // txtPropertyValue
            // 
            this.txtPropertyValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPropertyValue.Location = new System.Drawing.Point(303, 49);
            this.txtPropertyValue.Name = "txtPropertyValue";
            this.txtPropertyValue.Size = new System.Drawing.Size(68, 23);
            this.txtPropertyValue.TabIndex = 2;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSubmit.Location = new System.Drawing.Point(296, 305);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 7;
            this.btnSubmit.Text = "確定";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // txtPropertyName
            // 
            this.txtPropertyName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPropertyName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtPropertyName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtPropertyName.Location = new System.Drawing.Point(143, 49);
            this.txtPropertyName.Name = "txtPropertyName";
            this.txtPropertyName.Size = new System.Drawing.Size(92, 23);
            this.txtPropertyName.TabIndex = 1;
            // 
            // txtRole
            // 
            this.txtRole.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRole.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtRole.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtRole.Enabled = false;
            this.txtRole.Location = new System.Drawing.Point(167, 125);
            this.txtRole.Name = "txtRole";
            this.txtRole.Size = new System.Drawing.Size(92, 23);
            this.txtRole.TabIndex = 4;
            // 
            // txtSQL
            // 
            this.txtSQL.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSQL.Location = new System.Drawing.Point(48, 207);
            this.txtSQL.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtSQL.Name = "txtSQL";
            this.txtSQL.Size = new System.Drawing.Size(323, 91);
            this.txtSQL.TabIndex = 6;
            // 
            // AddRestrictForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(396, 350);
            this.Controls.Add(this.txtSQL);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.txtRole);
            this.Controls.Add(this.txtPropertyName);
            this.Controls.Add(this.txtPropertyValue);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rbDB);
            this.Controls.Add(this.rbRoleContain);
            this.Controls.Add(this.rbPropertyEquals);
            this.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(397, 388);
            this.Name = "AddRestrictForm";
            this.Text = "新增限制條件";
            this.Load += new System.EventHandler(this.AddRestrictForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rbPropertyEquals;
        private System.Windows.Forms.RadioButton rbRoleContain;
        private System.Windows.Forms.RadioButton rbDB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPropertyValue;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.TextBox txtPropertyName;
        private System.Windows.Forms.TextBox txtRole;
        private Editor.SQLText txtSQL;
    }
}