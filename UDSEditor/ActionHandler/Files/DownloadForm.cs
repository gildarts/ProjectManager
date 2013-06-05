using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProjectManager.Downloader;
using ProjectManager.Util;
using System.Threading;

namespace ProjectManager.ActionHandler.Files
{
    public partial class DownloadForm : Form
    {
        private DownloadFile[] _files;
        private HttpDownloader _downloader;
        private Queue<DownloadFile> _ques;
        private decimal _totalSize;
        private decimal _currentSize;
        private decimal _lastReceive;
        private int _currentPercent;

        internal DownloadForm(DownloadFile[] files)
        {
            InitializeComponent();
            _files = files;
            _ques = new Queue<DownloadFile>(_files);
            _totalSize = 0;
            _currentSize = 0;
            _currentPercent = 0;
        }

        private void DownloadForm_Load(object sender, EventArgs e)
        {
            if (_ques.Count == 0)
                this.Close();

            _downloader = new HttpDownloader();
            _downloader.DownloadFileCompleted += new EventHandler<AsyncCompletedEventArgs>(_downloader_DownloadFileCompleted);
            _downloader.DownloadProgressChanged += new EventHandler<System.Net.DownloadProgressChangedEventArgs>(_downloader_DownloadProgressChanged);

            this.progressBar1.Maximum = 100;
            this.progressBar1.Minimum = 0;

            foreach (DownloadFile file in _files)
            {
                _totalSize += file.FileSize;
            }

            this.DownlaodNext();
        }

        private void DownlaodNext()
        {
            _lastReceive = 0;
            DownloadFile file = _ques.Dequeue();
            label1.Text = "檔案來源 : " + file.HttpURL;
            label2.Text = "下載目的 : " + file.TargetPath;

            _downloader.Download(file);
        }

        void _downloader_DownloadProgressChanged(object sender, System.Net.DownloadProgressChangedEventArgs e)
        {
            decimal rec = e.BytesReceived - _lastReceive;
            _currentSize += rec;

            int p = Convert.ToInt32((_currentSize * 100) / _totalSize);

            if (p > 100) p = 100;

            if (p != _currentPercent)
            {
                this.progressBar1.Value = p;
                _currentPercent = p;
                label3.Text = "下載進度 : " + CalcUtil.GetVisualSize(_currentSize) + "/" + CalcUtil.GetVisualSize(_totalSize);
                this.Text = "下載同步 ( " + p + "% )";
                Application.DoEvents();
            }
        }

        void _downloader_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (_ques.Count == 0)
            {
                BackgroundWorker w = new BackgroundWorker();
                w.DoWork += new DoWorkEventHandler(w_DoWork);
                w.RunWorkerCompleted += new RunWorkerCompletedEventHandler(w_RunWorkerCompleted);
                w.RunWorkerAsync();
            }
            else
            {
                DownlaodNext();
            }
        }

        void w_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Close();
        }

        void w_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(1500);
        }
    }
}
