namespace ProjectManager.ActionHandler.Files
{
    partial class FileUIEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileUIEditor));
            this.treeDir = new System.Windows.Forms.TreeView();
            this.menuDir = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmDeleteFile = new System.Windows.Forms.ToolStripMenuItem();
            this.cmShowDir = new System.Windows.Forms.ToolStripMenuItem();
            this.cmOpenFile = new System.Windows.Forms.ToolStripMenuItem();
            this.imgs = new System.Windows.Forms.ImageList(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtLocalPath = new System.Windows.Forms.TextBox();
            this.txtHttp = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.menuDir.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeDir
            // 
            this.treeDir.AllowDrop = true;
            this.treeDir.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeDir.ContextMenuStrip = this.menuDir;
            this.treeDir.ImageIndex = 0;
            this.treeDir.ImageList = this.imgs;
            this.treeDir.Location = new System.Drawing.Point(19, 116);
            this.treeDir.Margin = new System.Windows.Forms.Padding(12, 13, 12, 13);
            this.treeDir.Name = "treeDir";
            this.treeDir.SelectedImageIndex = 0;
            this.treeDir.ShowNodeToolTips = true;
            this.treeDir.Size = new System.Drawing.Size(562, 397);
            this.treeDir.TabIndex = 8;
            this.treeDir.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeDir_AfterSelect);
            this.treeDir.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeDir_NodeMouseClick);
            this.treeDir.DragDrop += new System.Windows.Forms.DragEventHandler(this.treeDir_DragDrop);
            this.treeDir.DragEnter += new System.Windows.Forms.DragEventHandler(this.treeDir_DragEnter);
            this.treeDir.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.treeDir_MouseDoubleClick);
            // 
            // menuDir
            // 
            this.menuDir.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmDeleteFile,
            this.cmShowDir,
            this.cmOpenFile});
            this.menuDir.Name = "menuDir";
            this.menuDir.Size = new System.Drawing.Size(175, 92);
            this.menuDir.Opening += new System.ComponentModel.CancelEventHandler(this.menuDir_Opening);
            // 
            // cmDeleteFile
            // 
            this.cmDeleteFile.Name = "cmDeleteFile";
            this.cmDeleteFile.Size = new System.Drawing.Size(174, 22);
            this.cmDeleteFile.Text = "刪除檔案(&D)";
            this.cmDeleteFile.Click += new System.EventHandler(this.cmDeleteFile_Click);
            // 
            // cmShowDir
            // 
            this.cmShowDir.Name = "cmShowDir";
            this.cmShowDir.Size = new System.Drawing.Size(174, 22);
            this.cmShowDir.Text = "在資料夾中顯示(&F)";
            this.cmShowDir.Click += new System.EventHandler(this.cmShowDir_Click);
            // 
            // cmOpenFile
            // 
            this.cmOpenFile.Name = "cmOpenFile";
            this.cmOpenFile.Size = new System.Drawing.Size(174, 22);
            this.cmOpenFile.Text = "開啟檔案(&O)";
            this.cmOpenFile.Click += new System.EventHandler(this.cmOpenFile_Click);
            // 
            // imgs
            // 
            this.imgs.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgs.ImageStream")));
            this.imgs.TransparentColor = System.Drawing.Color.Transparent;
            this.imgs.Images.SetKeyName(0, "folder_close");
            this.imgs.Images.SetKeyName(1, "folder_opened");
            this.imgs.Images.SetKeyName(2, "file");
            this.imgs.Images.SetKeyName(3, "mov");
            this.imgs.Images.SetKeyName(4, "mp3");
            this.imgs.Images.SetKeyName(5, "mp4");
            this.imgs.Images.SetKeyName(6, "dll");
            this.imgs.Images.SetKeyName(7, "exe");
            this.imgs.Images.SetKeyName(8, "xml");
            this.imgs.Images.SetKeyName(9, "zip");
            this.imgs.Images.SetKeyName(10, "jpg");
            this.imgs.Images.SetKeyName(11, "jpeg");
            this.imgs.Images.SetKeyName(12, "ico");
            this.imgs.Images.SetKeyName(13, "wmv");
            this.imgs.Images.SetKeyName(14, "wav");
            this.imgs.Images.SetKeyName(15, "pdf");
            this.imgs.Images.SetKeyName(16, "tiff");
            this.imgs.Images.SetKeyName(17, "rar");
            this.imgs.Images.SetKeyName(18, "png");
            this.imgs.Images.SetKeyName(19, "html");
            this.imgs.Images.SetKeyName(20, "htm");
            this.imgs.Images.SetKeyName(21, "xhtml");
            this.imgs.Images.SetKeyName(22, "gif");
            this.imgs.Images.SetKeyName(23, "mpg");
            this.imgs.Images.SetKeyName(24, "mpeg");
            this.imgs.Images.SetKeyName(25, "config");
            this.imgs.Images.SetKeyName(26, "doc");
            this.imgs.Images.SetKeyName(27, "xlt");
            this.imgs.Images.SetKeyName(28, "xls");
            this.imgs.Images.SetKeyName(29, "pps");
            this.imgs.Images.SetKeyName(30, "ppt");
            this.imgs.Images.SetKeyName(31, "key");
            this.imgs.Images.SetKeyName(32, "txt");
            this.imgs.Images.SetKeyName(33, "java");
            this.imgs.Images.SetKeyName(34, "class");
            this.imgs.Images.SetKeyName(35, "jar");
            this.imgs.Images.SetKeyName(36, "war");
            this.imgs.Images.SetKeyName(37, "au");
            this.imgs.Images.SetKeyName(38, "iso");
            this.imgs.Images.SetKeyName(39, "ini");
            this.imgs.Images.SetKeyName(40, "css");
            this.imgs.Images.SetKeyName(41, "bat");
            this.imgs.Images.SetKeyName(42, "img");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 16);
            this.label2.TabIndex = 12;
            this.label2.Text = "本機：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 16);
            this.label1.TabIndex = 11;
            this.label1.Text = "HTTP：";
            // 
            // txtLocalPath
            // 
            this.txtLocalPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLocalPath.Location = new System.Drawing.Point(71, 52);
            this.txtLocalPath.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtLocalPath.Name = "txtLocalPath";
            this.txtLocalPath.ReadOnly = true;
            this.txtLocalPath.Size = new System.Drawing.Size(510, 23);
            this.txtLocalPath.TabIndex = 9;
            // 
            // txtHttp
            // 
            this.txtHttp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHttp.Location = new System.Drawing.Point(71, 19);
            this.txtHttp.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtHttp.Name = "txtHttp";
            this.txtHttp.ReadOnly = true;
            this.txtHttp.Size = new System.Drawing.Size(510, 23);
            this.txtHttp.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(371, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(210, 16);
            this.label3.TabIndex = 12;
            this.label3.Text = "※可將檔案或資料夾拖曳至目錄中上傳";
            // 
            // FileUIEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeDir);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtLocalPath);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtHttp);
            this.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FileUIEditor";
            this.Size = new System.Drawing.Size(600, 542);
            this.Load += new System.EventHandler(this.FileUIEditor_Load);
            this.menuDir.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeDir;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtLocalPath;
        private System.Windows.Forms.TextBox txtHttp;
        private System.Windows.Forms.ImageList imgs;
        private System.Windows.Forms.ContextMenuStrip menuDir;
        private System.Windows.Forms.ToolStripMenuItem cmDeleteFile;
        private System.Windows.Forms.ToolStripMenuItem cmShowDir;
        private System.Windows.Forms.ToolStripMenuItem cmOpenFile;
        private System.Windows.Forms.Label label3;
    }
}
