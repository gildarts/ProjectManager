namespace ProjectManager.Editor
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
            ActiproSoftware.SyntaxEditor.Document document1 = new ActiproSoftware.SyntaxEditor.Document();
            this.xmlSyntaxLanguage1 = new ActiproSoftware.SyntaxEditor.Addons.Xml.XmlSyntaxLanguage(this.components);
            this.syntaxEditor1 = new ActiproSoftware.SyntaxEditor.SyntaxEditor();
            this.SuspendLayout();
            // 
            // syntaxEditor1
            // 
            this.syntaxEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            document1.Language = this.xmlSyntaxLanguage1;
            this.syntaxEditor1.Document = document1;
            this.syntaxEditor1.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.syntaxEditor1.Location = new System.Drawing.Point(0, 0);
            this.syntaxEditor1.Name = "syntaxEditor1";
            this.syntaxEditor1.Size = new System.Drawing.Size(310, 173);
            this.syntaxEditor1.TabIndex = 0;
            this.syntaxEditor1.KeyTyped += new ActiproSoftware.SyntaxEditor.KeyTypedEventHandler(this.syntaxEditor1_KeyTyped);
            this.syntaxEditor1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.syntaxEditor1_KeyUp);
            // 
            // XmlEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.syntaxEditor1);
            this.Name = "XmlEditor";
            this.Size = new System.Drawing.Size(310, 173);
            this.ResumeLayout(false);

        }

        #endregion

        private ActiproSoftware.SyntaxEditor.SyntaxEditor syntaxEditor1;
        private ActiproSoftware.SyntaxEditor.Addons.Xml.XmlSyntaxLanguage xmlSyntaxLanguage1;
    }
}
