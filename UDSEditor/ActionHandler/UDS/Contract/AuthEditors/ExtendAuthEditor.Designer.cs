namespace ProjectManager.ActionHandler.UDS.Contract.AuthEditors
{
    partial class ExtendAuthEditor
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
            this.dgRestrict = new System.Windows.Forms.DataGridView();
            this.colRestrictType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCondition = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.dgExt = new System.Windows.Forms.DataGridView();
            this.dgExtType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgExtDisplay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAddRestrict = new System.Windows.Forms.Button();
            this.btnDeleteRestrict = new System.Windows.Forms.Button();
            this.btnEditRestrict = new System.Windows.Forms.Button();
            this.btnAddExt = new System.Windows.Forms.Button();
            this.btnEditExt = new System.Windows.Forms.Button();
            this.btnDeleteExt = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgRestrict)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgExt)).BeginInit();
            this.SuspendLayout();
            // 
            // dgRestrict
            // 
            this.dgRestrict.AllowUserToAddRows = false;
            this.dgRestrict.AllowUserToDeleteRows = false;
            this.dgRestrict.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgRestrict.BackgroundColor = System.Drawing.Color.White;
            this.dgRestrict.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgRestrict.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colRestrictType,
            this.colCondition});
            this.dgRestrict.Location = new System.Drawing.Point(32, 51);
            this.dgRestrict.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgRestrict.Name = "dgRestrict";
            this.dgRestrict.RowTemplate.Height = 24;
            this.dgRestrict.Size = new System.Drawing.Size(518, 219);
            this.dgRestrict.TabIndex = 3;
            // 
            // colRestrictType
            // 
            this.colRestrictType.HeaderText = "限制方式";
            this.colRestrictType.Name = "colRestrictType";
            this.colRestrictType.ReadOnly = true;
            // 
            // colCondition
            // 
            this.colCondition.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colCondition.HeaderText = "限制條件";
            this.colCondition.Name = "colCondition";
            this.colCondition.ReadOnly = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "增加限制";
            // 
            // dgExt
            // 
            this.dgExt.AllowUserToAddRows = false;
            this.dgExt.AllowUserToDeleteRows = false;
            this.dgExt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgExt.BackgroundColor = System.Drawing.Color.White;
            this.dgExt.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgExt.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgExtType,
            this.dgExtDisplay});
            this.dgExt.Location = new System.Drawing.Point(32, 327);
            this.dgExt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgExt.Name = "dgExt";
            this.dgExt.RowTemplate.Height = 24;
            this.dgExt.Size = new System.Drawing.Size(518, 155);
            this.dgExt.TabIndex = 7;
            // 
            // dgExtType
            // 
            this.dgExtType.HeaderText = "延伸方式";
            this.dgExtType.Name = "dgExtType";
            this.dgExtType.ReadOnly = true;
            // 
            // dgExtDisplay
            // 
            this.dgExtDisplay.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgExtDisplay.HeaderText = "延伸條件";
            this.dgExtDisplay.Name = "dgExtDisplay";
            this.dgExtDisplay.ReadOnly = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 307);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "延伸屬性";
            // 
            // btnAddRestrict
            // 
            this.btnAddRestrict.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddRestrict.Location = new System.Drawing.Point(367, 21);
            this.btnAddRestrict.Name = "btnAddRestrict";
            this.btnAddRestrict.Size = new System.Drawing.Size(57, 23);
            this.btnAddRestrict.TabIndex = 0;
            this.btnAddRestrict.Text = "新增";
            this.btnAddRestrict.UseVisualStyleBackColor = true;
            this.btnAddRestrict.Click += new System.EventHandler(this.btnAddRestrict_Click);
            // 
            // btnDeleteRestrict
            // 
            this.btnDeleteRestrict.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteRestrict.Location = new System.Drawing.Point(493, 21);
            this.btnDeleteRestrict.Name = "btnDeleteRestrict";
            this.btnDeleteRestrict.Size = new System.Drawing.Size(57, 23);
            this.btnDeleteRestrict.TabIndex = 2;
            this.btnDeleteRestrict.Text = "刪除";
            this.btnDeleteRestrict.UseVisualStyleBackColor = true;
            this.btnDeleteRestrict.Click += new System.EventHandler(this.btnDeleteRestrict_Click);
            // 
            // btnEditRestrict
            // 
            this.btnEditRestrict.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditRestrict.Location = new System.Drawing.Point(430, 21);
            this.btnEditRestrict.Name = "btnEditRestrict";
            this.btnEditRestrict.Size = new System.Drawing.Size(57, 23);
            this.btnEditRestrict.TabIndex = 1;
            this.btnEditRestrict.Text = "編輯";
            this.btnEditRestrict.UseVisualStyleBackColor = true;
            this.btnEditRestrict.Click += new System.EventHandler(this.btnEditRestrict_Click);
            // 
            // btnAddExt
            // 
            this.btnAddExt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddExt.Location = new System.Drawing.Point(367, 297);
            this.btnAddExt.Name = "btnAddExt";
            this.btnAddExt.Size = new System.Drawing.Size(57, 23);
            this.btnAddExt.TabIndex = 4;
            this.btnAddExt.Text = "新增";
            this.btnAddExt.UseVisualStyleBackColor = true;
            this.btnAddExt.Click += new System.EventHandler(this.btnAddExt_Click);
            // 
            // btnEditExt
            // 
            this.btnEditExt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditExt.Location = new System.Drawing.Point(430, 297);
            this.btnEditExt.Name = "btnEditExt";
            this.btnEditExt.Size = new System.Drawing.Size(57, 23);
            this.btnEditExt.TabIndex = 5;
            this.btnEditExt.Text = "編輯";
            this.btnEditExt.UseVisualStyleBackColor = true;
            this.btnEditExt.Click += new System.EventHandler(this.btnEditExt_Click);
            // 
            // btnDeleteExt
            // 
            this.btnDeleteExt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteExt.Location = new System.Drawing.Point(493, 297);
            this.btnDeleteExt.Name = "btnDeleteExt";
            this.btnDeleteExt.Size = new System.Drawing.Size(57, 23);
            this.btnDeleteExt.TabIndex = 6;
            this.btnDeleteExt.Text = "刪除";
            this.btnDeleteExt.UseVisualStyleBackColor = true;
            this.btnDeleteExt.Click += new System.EventHandler(this.btnDeleteExt_Click);
            // 
            // ExtendAuthEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnDeleteExt);
            this.Controls.Add(this.btnEditExt);
            this.Controls.Add(this.btnDeleteRestrict);
            this.Controls.Add(this.btnAddExt);
            this.Controls.Add(this.btnEditRestrict);
            this.Controls.Add(this.btnAddRestrict);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgExt);
            this.Controls.Add(this.dgRestrict);
            this.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ExtendAuthEditor";
            this.Size = new System.Drawing.Size(602, 582);
            ((System.ComponentModel.ISupportInitialize)(this.dgRestrict)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgExt)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgRestrict;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgExt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnAddRestrict;
        private System.Windows.Forms.Button btnDeleteRestrict;
        private System.Windows.Forms.Button btnEditRestrict;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRestrictType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCondition;
        private System.Windows.Forms.Button btnAddExt;
        private System.Windows.Forms.Button btnEditExt;
        private System.Windows.Forms.Button btnDeleteExt;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgExtType;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgExtDisplay;
    }
}
