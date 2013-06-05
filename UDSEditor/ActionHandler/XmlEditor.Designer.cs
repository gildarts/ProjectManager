namespace ProjectManager.ActionHandler
{
    partial class XmlEditor
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XmlEditor));
            ActiproSoftware.SyntaxEditor.Document document1 = new ActiproSoftware.SyntaxEditor.Document();
            this.xmlSyntaxLanguage1 = new ActiproSoftware.SyntaxEditor.Addons.Xml.XmlSyntaxLanguage(this.components);
            this.txtFormat = new System.Windows.Forms.Button();
            this.btnUndo = new System.Windows.Forms.Button();
            this.err = new System.Windows.Forms.ErrorProvider(this.components);
            this.txtXML = new ActiproSoftware.SyntaxEditor.SyntaxEditor();
            ((System.ComponentModel.ISupportInitialize)(this.err)).BeginInit();
            this.SuspendLayout();
            // 
            // txtFormat
            // 
            this.txtFormat.Image = ((System.Drawing.Image)(resources.GetObject("txtFormat.Image")));
            this.txtFormat.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtFormat.Location = new System.Drawing.Point(6, 13);
            this.txtFormat.Name = "txtFormat";
            this.txtFormat.Size = new System.Drawing.Size(75, 30);
            this.txtFormat.TabIndex = 0;
            this.txtFormat.Text = "格式化";
            this.txtFormat.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.txtFormat.UseVisualStyleBackColor = true;
            this.txtFormat.Click += new System.EventHandler(this.txtFormat_Click);
            // 
            // btnUndo
            // 
            this.btnUndo.Image = ((System.Drawing.Image)(resources.GetObject("btnUndo.Image")));
            this.btnUndo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUndo.Location = new System.Drawing.Point(87, 13);
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Size = new System.Drawing.Size(75, 30);
            this.btnUndo.TabIndex = 1;
            this.btnUndo.Text = "復原";
            this.btnUndo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnUndo.UseVisualStyleBackColor = true;
            this.btnUndo.Click += new System.EventHandler(this.btnUndo_Click);
            // 
            // err
            // 
            this.err.ContainerControl = this;
            // 
            // txtXML
            // 
            this.txtXML.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            document1.Language = this.xmlSyntaxLanguage1;
            document1.Text = "<XmlDocument/>";
            this.txtXML.Document = document1;
            this.txtXML.Font = new System.Drawing.Font("微軟正黑體", 11F);
            this.txtXML.Location = new System.Drawing.Point(6, 49);
            this.txtXML.Name = "txtXML";
            this.txtXML.Size = new System.Drawing.Size(639, 405);
            this.txtXML.TabIndex = 2;
            // 
            // XmlEditor
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.btnUndo);
            this.Controls.Add(this.txtFormat);
            this.Controls.Add(this.txtXML);
            this.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Name = "XmlEditor";
            this.Size = new System.Drawing.Size(645, 457);
            this.Load += new System.EventHandler(this.XmlEditor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.err)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button txtFormat;
        private System.Windows.Forms.Button btnUndo;
        private ActiproSoftware.SyntaxEditor.Addons.Xml.XmlSyntaxLanguage xmlSyntaxLanguage1;
        private System.Windows.Forms.ErrorProvider err;
        private ActiproSoftware.SyntaxEditor.SyntaxEditor txtXML;
    }
}
