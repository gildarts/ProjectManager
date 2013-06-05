using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProjectManager.Uploader;
using System.Threading;
using ProjectManager.Util;
using System.IO;

namespace ProjectManager.ActionHandler.Files
{
    public partial class ProgressForm : Form
    {
        private UploadFile[] files;
        private string url;
        private string user;
        private string password;
        private long total;
        private long currentBytes;
        private System.Collections.Generic.Queue<UploadFile> ques;
        private IUploader uploader;
        private int currentPercent;

        public ProgressForm(UploadFile[] files, string url, string user, string password)
        {
            InitializeComponent();
            this.files = files;
            this.url = url;
            this.user = user;
            this.password = password;
            total = 0;
            currentBytes = 0;
            ques = new Queue<UploadFile>(files);
            uploader = UploaderFactory.CreateInstance();
            currentPercent = 0;
        }

        private void ProgressForm_Load(object sender, EventArgs e)
        {
            try
            {
                StartUpload();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Close();
            }
        }

        public void StartUpload()
        {
            foreach (UploadFile file in this.files)
            {
                FileInfo fileInfo = file.File;
                long length = fileInfo.Length;
                total += length;
            }

            if (total == 0)
            {
                this.Close();
                return;
            }

            this.progressBar1.Maximum = 100;
            this.progressBar1.Minimum = 0;

            uploader.ProgressChanged += new EventHandler<ProgressEventArgs>(uploader_ProgressChanged);
            uploader.UploadCompleted += new EventHandler(uploader_UploadCompleted);
            uploader.UploadError += new EventHandler<ThreadExceptionEventArgs>(uploader_UploadError);


            if (ques.Count > 0)
                this.UploadFile(ques.Dequeue());
            else
                this.Close();
        }

        void uploader_UploadError(object sender, ThreadExceptionEventArgs e)
        {
            Invoke(new Action(delegate()
            {
                MessageBox.Show(e.Exception.Message);
                this.Close();
            }));
        }

        void UploadFile(UploadFile file)
        {
            Invoke(new Action(() =>
            {
                lblUploadFile.Text = "上傳檔案 : " + file.File.Name;

                int count = this.files.Length;
                lblFileCount.Text = "檔案數 : " + (count - ques.Count) + "/" + this.files.Length;
            }));

            //string ftp = Path.Combine(this.url, file.ServerPath);
            string ftp = PathHelper.CombineURL(this.url, file.ServerPath);
            ftp = ftp.Replace(@"\", "/");
            uploader.Upload(ftp, file.File.FullName, this.user, this.password);
        }

        void uploader_UploadCompleted(object sender, EventArgs e)
        {
            if (ques.Count > 0)
                this.UploadFile(ques.Dequeue());
            else
            {
                //MessageBox.Show("檔案上傳完成", "完成", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //Thread.Sleep(1000);
                BackgroundWorker w = new BackgroundWorker();
                w.DoWork += new DoWorkEventHandler(w_DoWork);
                w.RunWorkerCompleted += new RunWorkerCompletedEventHandler(w_RunWorkerCompleted);
                Application.DoEvents();
                w.RunWorkerAsync();
            }
        }

        void w_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Close();
        }

        void w_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(1000);
        }

        void uploader_ProgressChanged(object sender, ProgressEventArgs e)
        {
            this.currentBytes += e.CurrentBytesToSend;
            int p = Convert.ToInt32((currentBytes * 100) / total);
            if (p > 100) p = 100;

            this.lblPercent.Text = "上傳進度 : " + CalcUtil.GetVisualSize(currentBytes) + "/" + CalcUtil.GetVisualSize(total);

            if (currentPercent != p)
            {
                currentPercent = p;
                this.progressBar1.Value = p;
                this.Text = "傳輸進度( " + p + "% )";
                Application.DoEvents();
            }
        }
    }
}
