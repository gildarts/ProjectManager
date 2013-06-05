using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectManager.Downloader
{
    internal class DownloadFile
    {
        internal DownloadFile(string http, string path, decimal size)
        {
            this.HttpURL = http;
            this.TargetPath = path;
            this.FileSize = size;
        }

        internal string HttpURL { get; private set; }
        internal string TargetPath { get; private set; }
        internal decimal FileSize { get; private set; }
    }
}
