namespace ProjectManager.ActionHandler
{
    partial class JSEditor 
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
            ActiproSoftware.SyntaxEditor.Document document1 = new ActiproSoftware.SyntaxEditor.Document();
            this.jsEditor1 = new ActiproSoftware.SyntaxEditor.SyntaxEditor();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnExtEditor = new System.Windows.Forms.Button();
            this.lblMsg = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // jsEditor1
            // 
            this.jsEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            document1.Text = "JavaScript";
            this.jsEditor1.Document = document1;
            this.jsEditor1.Font = new System.Drawing.Font("微軟正黑體", 11F);
            this.jsEditor1.LineNumberMarginVisible = true;
            this.jsEditor1.Location = new System.Drawing.Point(3, 46);
            this.jsEditor1.Name = "jsEditor1";
            this.jsEditor1.Size = new System.Drawing.Size(731, 493);
            this.jsEditor1.TabIndex = 2;
            this.jsEditor1.KeyTyped += new ActiproSoftware.SyntaxEditor.KeyTypedEventHandler(this.jsEditor1_KeyTyped);
            this.jsEditor1.TextChanged += new System.EventHandler(this.jsEditor1_TextChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.jsEditor1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 43F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(737, 542);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblMsg);
            this.panel1.Controls.Add(this.btnExtEditor);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(731, 37);
            this.panel1.TabIndex = 3;
            // 
            // btnExtEditor
            // 
            this.btnExtEditor.Location = new System.Drawing.Point(7, 7);
            this.btnExtEditor.Name = "btnExtEditor";
            this.btnExtEditor.Size = new System.Drawing.Size(73, 23);
            this.btnExtEditor.TabIndex = 0;
            this.btnExtEditor.Text = "VSCode";
            this.btnExtEditor.UseVisualStyleBackColor = true;
            this.btnExtEditor.Click += new System.EventHandler(this.btnExtEditor_Click);
            // 
            // lblMsg
            // 
            this.lblMsg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblMsg.AutoSize = true;
            this.lblMsg.ForeColor = System.Drawing.Color.Red;
            this.lblMsg.Location = new System.Drawing.Point(86, 10);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(384, 16);
            this.lblMsg.TabIndex = 1;
            this.lblMsg.Text = "使用外部編輯器，記得 Project Manager 也需存檔，否則資料會遺失。";
            // 
            // JSEditor
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Name = "JSEditor";
            this.Size = new System.Drawing.Size(737, 542);
            this.Load += new System.EventHandler(this.XmlEditor_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ActiproSoftware.SyntaxEditor.SyntaxEditor jsEditor1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnExtEditor;
        private System.Windows.Forms.Label lblMsg;
    }
}
