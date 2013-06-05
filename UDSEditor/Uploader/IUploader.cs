using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ProjectManager.Uploader
{
    interface IUploader
    {
        event EventHandler UploadCompleted;
        event EventHandler<ProgressEventArgs> ProgressChanged;
        event EventHandler<ThreadExceptionEventArgs> UploadError;

        void Upload(string ftpUri, string filename, string username, string password);
    }
}
