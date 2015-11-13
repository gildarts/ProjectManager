namespace ProjectManager.ActionHandler.UDS.Service
{
    partial class AddServiceForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddServiceForm));
            this.txtServiceName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.rbInsert = new System.Windows.Forms.RadioButton();
            this.rbUpdate = new System.Windows.Forms.RadioButton();
            this.rbDelete = new System.Windows.Forms.RadioButton();
            this.rbSelect = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cboTable = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.err = new System.Windows.Forms.ErrorProvider(this.components);
            this.rbSet = new System.Windows.Forms.RadioButton();
            this.rbJavascript = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.err)).BeginInit();
            this.SuspendLayout();
            // 
            // txtServiceName
            // 
            this.txtServiceName.Location = new System.Drawing.Point(95, 16);
            this.txtServiceName.Name = "txtServiceName";
            this.txtServiceName.Size = new System.Drawing.Size(259, 23);
            this.txtServiceName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Service 名稱";
            // 
            // rbInsert
            // 
            this.rbInsert.AutoSize = true;
            this.rbInsert.Checked = true;
            this.rbInsert.Location = new System.Drawing.Point(95, 47);
            this.rbInsert.Name = "rbInsert";
            this.rbInsert.Size = new System.Drawing.Size(50, 20);
            this.rbInsert.TabIndex = 3;
            this.rbInsert.TabStop = true;
            this.rbInsert.Text = "新增";
            this.rbInsert.UseVisualStyleBackColor = true;
            // 
            // rbUpdate
            // 
            this.rbUpdate.AutoSize = true;
            this.rbUpdate.Location = new System.Drawing.Point(156, 47);
            this.rbUpdate.Name = "rbUpdate";
            this.rbUpdate.Size = new System.Drawing.Size(50, 20);
            this.rbUpdate.TabIndex = 4;
            this.rbUpdate.Text = "修改";
            this.rbUpdate.UseVisualStyleBackColor = true;
            // 
            // rbDelete
            // 
            this.rbDelete.AutoSize = true;
            this.rbDelete.Location = new System.Drawing.Point(209, 47);
            this.rbDelete.Name = "rbDelete";
            this.rbDelete.Size = new System.Drawing.Size(50, 20);
            this.rbDelete.TabIndex = 5;
            this.rbDelete.Text = "刪除";
            this.rbDelete.UseVisualStyleBackColor = true;
            // 
            // rbSelect
            // 
            this.rbSelect.AutoSize = true;
            this.rbSelect.Location = new System.Drawing.Point(95, 71);
            this.rbSelect.Name = "rbSelect";
            this.rbSelect.Size = new System.Drawing.Size(50, 20);
            this.rbSelect.TabIndex = 6;
            this.rbSelect.Text = "查詢";
            this.rbSelect.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "執行類型";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 16);
            this.label3.TabIndex = 9;
            this.label3.Text = "主要資料表";
            // 
            // cboTable
            // 
            this.cboTable.FormattingEnabled = true;
            this.cboTable.Location = new System.Drawing.Point(95, 97);
            this.cboTable.Name = "cboTable";
            this.cboTable.Size = new System.Drawing.Size(258, 24);
            this.cboTable.TabIndex = 10;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(279, 136);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "確定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // err
            // 
            this.err.ContainerControl = this;
            // 
            // rbSet
            // 
            this.rbSet.AutoSize = true;
            this.rbSet.Location = new System.Drawing.Point(156, 71);
            this.rbSet.Name = "rbSet";
            this.rbSet.Size = new System.Drawing.Size(47, 20);
            this.rbSet.TabIndex = 7;
            this.rbSet.Text = "SET";
            this.rbSet.UseVisualStyleBackColor = true;
            // 
            // rbJavascript
            // 
            this.rbJavascript.AutoSize = true;
            this.rbJavascript.Location = new System.Drawing.Point(209, 71);
            this.rbJavascript.Name = "rbJavascript";
            this.rbJavascript.Size = new System.Drawing.Size(81, 20);
            this.rbJavascript.TabIndex = 8;
            this.rbJavascript.Text = "Javascript";
            this.rbJavascript.UseVisualStyleBackColor = true;
            this.rbJavascript.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // AddServiceForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(372, 175);
            this.Controls.Add(this.rbJavascript);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cboTable);
            this.Controls.Add(this.rbSet);
            this.Controls.Add(this.rbSelect);
            this.Controls.Add(this.rbDelete);
            this.Controls.Add(this.rbUpdate);
            this.Controls.Add(this.rbInsert);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtServiceName);
            this.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddServiceForm";
            this.Text = "新增 Service";
            this.Load += new System.EventHandler(this.AddServiceForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.err)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtServiceName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbInsert;
        private System.Windows.Forms.RadioButton rbUpdate;
        private System.Windows.Forms.RadioButton rbDelete;
        private System.Windows.Forms.RadioButton rbSelect;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboTable;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ErrorProvider err;
        private System.Windows.Forms.RadioButton rbSet;
        private System.Windows.Forms.RadioButton rbJavascript;
    }
}