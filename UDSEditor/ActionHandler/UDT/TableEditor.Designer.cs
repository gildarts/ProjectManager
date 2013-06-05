namespace ProjectManager.ActionHandler.UDT
{
    partial class TableEditor
    {
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.dgFields = new System.Windows.Forms.DataGridView();
            this.btnAddField = new System.Windows.Forms.Button();
            this.btnRemoveField = new System.Windows.Forms.Button();
            this.btnEditUniq = new System.Windows.Forms.Button();
            this.btnDeleteUniq = new System.Windows.Forms.Button();
            this.dgUniq = new System.Windows.Forms.DataGridView();
            this.colUniqName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFields = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAddUniq = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnRemoveFK = new System.Windows.Forms.Button();
            this.dgFK = new System.Windows.Forms.DataGridView();
            this.colFKName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFKMainFields = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFKRefTable = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFKRefFields = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFKOnUpdate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFKOnDelete = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAddFK = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDataType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colIndexed = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colAllowNull = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colDefault = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgFields)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgUniq)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgFK)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "資料表名稱";
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(88, 11);
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(623, 23);
            this.txtName.TabIndex = 0;
            // 
            // dgFields
            // 
            this.dgFields.AllowUserToAddRows = false;
            this.dgFields.AllowUserToDeleteRows = false;
            this.dgFields.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgFields.BackgroundColor = System.Drawing.Color.White;
            this.dgFields.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgFields.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colName,
            this.colDataType,
            this.colIndexed,
            this.colAllowNull,
            this.colDefault});
            this.dgFields.Location = new System.Drawing.Point(17, 31);
            this.dgFields.MultiSelect = false;
            this.dgFields.Name = "dgFields";
            this.dgFields.RowTemplate.Height = 24;
            this.dgFields.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgFields.Size = new System.Drawing.Size(694, 191);
            this.dgFields.TabIndex = 2;
            this.dgFields.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgFields_CellEndEdit);
            this.dgFields.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgFields_RowEnter);
            // 
            // btnAddField
            // 
            this.btnAddField.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddField.Location = new System.Drawing.Point(605, 3);
            this.btnAddField.Name = "btnAddField";
            this.btnAddField.Size = new System.Drawing.Size(50, 22);
            this.btnAddField.TabIndex = 0;
            this.btnAddField.Text = "新增";
            this.btnAddField.UseVisualStyleBackColor = true;
            this.btnAddField.Click += new System.EventHandler(this.btnAddField_Click);
            // 
            // btnRemoveField
            // 
            this.btnRemoveField.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveField.Enabled = false;
            this.btnRemoveField.Location = new System.Drawing.Point(661, 3);
            this.btnRemoveField.Name = "btnRemoveField";
            this.btnRemoveField.Size = new System.Drawing.Size(50, 22);
            this.btnRemoveField.TabIndex = 1;
            this.btnRemoveField.Text = "移除";
            this.btnRemoveField.UseVisualStyleBackColor = true;
            this.btnRemoveField.Click += new System.EventHandler(this.btnRemoveField_Click);
            // 
            // btnEditUniq
            // 
            this.btnEditUniq.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditUniq.Location = new System.Drawing.Point(611, 12);
            this.btnEditUniq.Name = "btnEditUniq";
            this.btnEditUniq.Size = new System.Drawing.Size(50, 22);
            this.btnEditUniq.TabIndex = 1;
            this.btnEditUniq.Text = "編輯";
            this.btnEditUniq.UseVisualStyleBackColor = true;
            this.btnEditUniq.Click += new System.EventHandler(this.btnEditUniq_Click);
            // 
            // btnDeleteUniq
            // 
            this.btnDeleteUniq.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteUniq.Location = new System.Drawing.Point(667, 12);
            this.btnDeleteUniq.Name = "btnDeleteUniq";
            this.btnDeleteUniq.Size = new System.Drawing.Size(50, 22);
            this.btnDeleteUniq.TabIndex = 2;
            this.btnDeleteUniq.Text = "刪除";
            this.btnDeleteUniq.UseVisualStyleBackColor = true;
            this.btnDeleteUniq.Click += new System.EventHandler(this.btnDeleteUniq_Click);
            // 
            // dgUniq
            // 
            this.dgUniq.AllowUserToAddRows = false;
            this.dgUniq.AllowUserToDeleteRows = false;
            this.dgUniq.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgUniq.BackgroundColor = System.Drawing.Color.White;
            this.dgUniq.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgUniq.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colUniqName,
            this.colFields});
            this.dgUniq.Location = new System.Drawing.Point(17, 40);
            this.dgUniq.Name = "dgUniq";
            this.dgUniq.RowTemplate.Height = 24;
            this.dgUniq.Size = new System.Drawing.Size(694, 50);
            this.dgUniq.TabIndex = 3;
            this.dgUniq.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgUniq_CellEndEdit);
            this.dgUniq.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgUniq_RowsAdded);
            this.dgUniq.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dgUniq_RowsRemoved);
            // 
            // colUniqName
            // 
            this.colUniqName.FillWeight = 125F;
            this.colUniqName.HeaderText = "名稱";
            this.colUniqName.Name = "colUniqName";
            this.colUniqName.ReadOnly = true;
            this.colUniqName.Width = 125;
            // 
            // colFields
            // 
            this.colFields.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colFields.HeaderText = "條件欄位";
            this.colFields.Name = "colFields";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "設定唯一條件";
            // 
            // btnAddUniq
            // 
            this.btnAddUniq.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddUniq.Location = new System.Drawing.Point(555, 12);
            this.btnAddUniq.Name = "btnAddUniq";
            this.btnAddUniq.Size = new System.Drawing.Size(50, 22);
            this.btnAddUniq.TabIndex = 0;
            this.btnAddUniq.Text = "新增";
            this.btnAddUniq.UseVisualStyleBackColor = true;
            this.btnAddUniq.Click += new System.EventHandler(this.btnAddUniq_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "資料欄位設定";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel4, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(737, 513);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btnRemoveFK);
            this.panel4.Controls.Add(this.dgFK);
            this.panel4.Controls.Add(this.btnAddFK);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 399);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(731, 111);
            this.panel4.TabIndex = 3;
            // 
            // btnRemoveFK
            // 
            this.btnRemoveFK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveFK.Location = new System.Drawing.Point(667, 12);
            this.btnRemoveFK.Name = "btnRemoveFK";
            this.btnRemoveFK.Size = new System.Drawing.Size(50, 22);
            this.btnRemoveFK.TabIndex = 2;
            this.btnRemoveFK.Text = "刪除";
            this.btnRemoveFK.UseVisualStyleBackColor = true;
            this.btnRemoveFK.Click += new System.EventHandler(this.btnRemoveFK_Click);
            // 
            // dgFK
            // 
            this.dgFK.AllowUserToAddRows = false;
            this.dgFK.AllowUserToDeleteRows = false;
            this.dgFK.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgFK.BackgroundColor = System.Drawing.Color.White;
            this.dgFK.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgFK.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colFKName,
            this.colFKMainFields,
            this.colFKRefTable,
            this.colFKRefFields,
            this.colFKOnUpdate,
            this.colFKOnDelete});
            this.dgFK.Location = new System.Drawing.Point(17, 40);
            this.dgFK.Name = "dgFK";
            this.dgFK.RowTemplate.Height = 24;
            this.dgFK.Size = new System.Drawing.Size(694, 52);
            this.dgFK.TabIndex = 3;
            // 
            // colFKName
            // 
            this.colFKName.HeaderText = "名稱";
            this.colFKName.Name = "colFKName";
            this.colFKName.Width = 151;
            // 
            // colFKMainFields
            // 
            this.colFKMainFields.HeaderText = "本地欄位";
            this.colFKMainFields.Name = "colFKMainFields";
            this.colFKMainFields.Width = 200;
            // 
            // colFKRefTable
            // 
            this.colFKRefTable.HeaderText = "參考資料表";
            this.colFKRefTable.Name = "colFKRefTable";
            // 
            // colFKRefFields
            // 
            this.colFKRefFields.HeaderText = "參考欄位";
            this.colFKRefFields.Name = "colFKRefFields";
            this.colFKRefFields.Width = 200;
            // 
            // colFKOnUpdate
            // 
            this.colFKOnUpdate.HeaderText = "更新時";
            this.colFKOnUpdate.Name = "colFKOnUpdate";
            // 
            // colFKOnDelete
            // 
            this.colFKOnDelete.HeaderText = "刪除時";
            this.colFKOnDelete.Name = "colFKOnDelete";
            // 
            // btnAddFK
            // 
            this.btnAddFK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddFK.Location = new System.Drawing.Point(611, 12);
            this.btnAddFK.Name = "btnAddFK";
            this.btnAddFK.Size = new System.Drawing.Size(50, 22);
            this.btnAddFK.TabIndex = 0;
            this.btnAddFK.Text = "新增";
            this.btnAddFK.UseVisualStyleBackColor = true;
            this.btnAddFK.Click += new System.EventHandler(this.btnAddFK_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 16);
            this.label4.TabIndex = 0;
            this.label4.Text = "設定 Foreign Key";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnAddField);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.dgFields);
            this.panel2.Controls.Add(this.btnRemoveField);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 53);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(731, 225);
            this.panel2.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnDeleteUniq);
            this.panel3.Controls.Add(this.dgUniq);
            this.panel3.Controls.Add(this.btnEditUniq);
            this.panel3.Controls.Add(this.btnAddUniq);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 284);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(731, 109);
            this.panel3.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtName);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(731, 44);
            this.panel1.TabIndex = 0;
            // 
            // colName
            // 
            this.colName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colName.HeaderText = "欄位";
            this.colName.Name = "colName";
            // 
            // colDataType
            // 
            this.colDataType.HeaderText = "資料型態";
            this.colDataType.Items.AddRange(new object[] {
            "String",
            "Number",
            "Datetime",
            "Boolean",
            "Integer",
            "BigInt"});
            this.colDataType.Name = "colDataType";
            // 
            // colIndexed
            // 
            this.colIndexed.HeaderText = "索引";
            this.colIndexed.Name = "colIndexed";
            // 
            // colAllowNull
            // 
            this.colAllowNull.HeaderText = "允許空值";
            this.colAllowNull.Name = "colAllowNull";
            // 
            // colDefault
            // 
            this.colDefault.HeaderText = "預設值";
            this.colDefault.Name = "colDefault";
            this.colDefault.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colDefault.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // TableEditor
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "TableEditor";
            this.Size = new System.Drawing.Size(737, 513);
            this.Load += new System.EventHandler(this.TableEditor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgFields)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgUniq)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgFK)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.DataGridView dgFields;
        private System.Windows.Forms.Button btnAddField;
        private System.Windows.Forms.Button btnRemoveField;
        private System.Windows.Forms.Button btnEditUniq;
        private System.Windows.Forms.Button btnDeleteUniq;
        private System.Windows.Forms.DataGridView dgUniq;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnAddUniq;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUniqName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFields;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnRemoveFK;
        private System.Windows.Forms.DataGridView dgFK;
        private System.Windows.Forms.Button btnAddFK;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFKName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFKMainFields;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFKRefTable;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFKRefFields;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFKOnUpdate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFKOnDelete;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewComboBoxColumn colDataType;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colIndexed;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colAllowNull;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDefault;
    }
}
