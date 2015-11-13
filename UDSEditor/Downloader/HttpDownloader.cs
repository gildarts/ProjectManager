using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.ComponentModel;
using ProjectManager.ActionHandler.Files;
using System.IO;

namespace ProjectManager.Downloader
{
    class HttpDownloader
    {
        public event EventHandler<AsyncCompletedEventArgs> DownloadFileCompleted;
        public event EventHandler<DownloadProgressChangedEventArgs> DownloadProgressChanged;

        internal void Download(DownloadFile file)
        {
            this.Download(file.HttpURL, file.TargetPath);
        }

        internal void Download(string url, string targetPath)
        {
            WebClient wc = new WebClient();

            wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(wc_DownloadProgressChanged);
            wc.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(wc_DownloadFileCompleted);
            wc.DownloadFileAsync(new Uri(url), targetPath);
        }

        void wc_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (DownloadFileCompleted != null)
                DownloadFileCompleted.Invoke(this, e);
        }

        void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            if (DownloadProgressChanged != null)
                DownloadProgressChanged.Invoke(this, e);
        }
    }
}
