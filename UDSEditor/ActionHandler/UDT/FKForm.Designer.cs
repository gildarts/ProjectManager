namespace ProjectManager.ActionHandler.UDT
{
    partial class FKForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FKForm));
            this.cboRefTable = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cboMainField = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboRefField = new System.Windows.Forms.ComboBox();
            this.dgField = new System.Windows.Forms.DataGridView();
            this.colLocal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRef = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cboOnDelete = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cboOnUpdate = new System.Windows.Forms.ComboBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.err = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgField)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.err)).BeginInit();
            this.SuspendLayout();
            // 
            // cboRefTable
            // 
            this.cboRefTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRefTable.FormattingEnabled = true;
            this.cboRefTable.Location = new System.Drawing.Point(86, 42);
            this.cboRefTable.Name = "cboRefTable";
            this.cboRefTable.Size = new System.Drawing.Size(206, 24);
            this.cboRefTable.TabIndex = 0;
            this.cboRefTable.SelectedIndexChanged += new System.EventHandler(this.cboRefTable_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "參照資料表";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "本地欄位";
            // 
            // cboMainField
            // 
            this.cboMainField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMainField.FormattingEnabled = true;
            this.cboMainField.Location = new System.Drawing.Point(86, 12);
            this.cboMainField.Name = "cboMainField";
            this.cboMainField.Size = new System.Drawing.Size(206, 24);
            this.cboMainField.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "參照欄位";
            // 
            // cboRefField
            // 
            this.cboRefField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRefField.FormattingEnabled = true;
            this.cboRefField.Location = new System.Drawing.Point(86, 72);
            this.cboRefField.Name = "cboRefField";
            this.cboRefField.Size = new System.Drawing.Size(206, 24);
            this.cboRefField.TabIndex = 4;
            // 
            // dgField
            // 
            this.dgField.AllowUserToAddRows = false;
            this.dgField.BackgroundColor = System.Drawing.Color.White;
            this.dgField.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgField.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colLocal,
            this.colRef});
            this.dgField.Location = new System.Drawing.Point(15, 144);
            this.dgField.Name = "dgField";
            this.dgField.RowHeadersVisible = false;
            this.dgField.RowTemplate.Height = 24;
            this.dgField.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgField.Size = new System.Drawing.Size(277, 120);
            this.dgField.TabIndex = 6;
            // 
            // colLocal
            // 
            this.colLocal.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colLocal.HeaderText = "本地欄位";
            this.colLocal.Name = "colLocal";
            // 
            // colRef
            // 
            this.colRef.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colRef.HeaderText = "參照欄位";
            this.colRef.Name = "colRef";
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(217, 102);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 23);
            this.btnRemove.TabIndex = 7;
            this.btnRemove.Text = "刪除";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(136, 102);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 7;
            this.btnAdd.Text = "新增";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(36, 303);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 16);
            this.label4.TabIndex = 11;
            this.label4.Text = "刪除時";
            // 
            // cboOnDelete
            // 
            this.cboOnDelete.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboOnDelete.FormattingEnabled = true;
            this.cboOnDelete.Items.AddRange(new object[] {
            "NO ACTION",
            "RESTRICT",
            "CASCADE",
            "SET NULL",
            "SET DEFAULT"});
            this.cboOnDelete.Location = new System.Drawing.Point(86, 300);
            this.cboOnDelete.Name = "cboOnDelete";
            this.cboOnDelete.Size = new System.Drawing.Size(206, 24);
            this.cboOnDelete.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(36, 273);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 16);
            this.label5.TabIndex = 9;
            this.label5.Text = "更新時";
            // 
            // cboOnUpdate
            // 
            this.cboOnUpdate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboOnUpdate.FormattingEnabled = true;
            this.cboOnUpdate.Items.AddRange(new object[] {
            "NO ACTION",
            "RESTRICT",
            "CASCADE",
            "SET NULL",
            "SET DEFAULT"});
            this.cboOnUpdate.Location = new System.Drawing.Point(86, 270);
            this.cboOnUpdate.Name = "cboOnUpdate";
            this.cboOnUpdate.Size = new System.Drawing.Size(206, 24);
            this.cboOnUpdate.TabIndex = 8;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(217, 342);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 7;
            this.btnSubmit.Text = "確定";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // err
            // 
            this.err.ContainerControl = this;
            // 
            // FKForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(320, 385);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cboOnDelete);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cboOnUpdate);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.dgField);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cboRefField);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboMainField);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboRefTable);
            this.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FKForm";
            this.Text = "Foreign Key 設定";
            this.Load += new System.EventHandler(this.FKForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgField)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.err)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboRefTable;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboMainField;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboRefField;
        private System.Windows.Forms.DataGridView dgField;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLocal;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRef;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboOnDelete;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboOnUpdate;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.ErrorProvider err;
    }
}