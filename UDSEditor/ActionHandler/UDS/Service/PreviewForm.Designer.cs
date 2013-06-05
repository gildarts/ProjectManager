namespace ProjectManager.ActionHandler.UDS.Service
{
    partial class PreviewForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PreviewForm));
            this.xmlEditor1 = new ProjectManager.Editor.XmlEditor();
            this.SuspendLayout();
            // 
            // xmlEditor1
            // 
            this.xmlEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xmlEditor1.Location = new System.Drawing.Point(0, 0);
            this.xmlEditor1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.xmlEditor1.Name = "xmlEditor1";
            this.xmlEditor1.Size = new System.Drawing.Size(606, 349);
            this.xmlEditor1.TabIndex = 0;
            // 
            // PreviewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(606, 349);
            this.Controls.Add(this.xmlEditor1);
            this.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "PreviewForm";
            this.Text = "XML 預覽視窗";
            this.Load += new System.EventHandler(this.PreviewForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Editor.XmlEditor xmlEditor1;
    }
}