using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectManager.Uploader
{
    public class ProgressEventArgs : EventArgs
    {
        public long TotalUploadedBytes { get; private set; }
        public long TotalBytesToSend { get; private set; }
        public int CurrentBytesToSend { get; set; }

        public ProgressEventArgs(long totalByteToSend, long totalUploadedBytes, int currentBytesToSend)
        {
            this.TotalBytesToSend = totalByteToSend;
            this.TotalUploadedBytes = totalUploadedBytes;
            this.CurrentBytesToSend = currentBytesToSend;
        }
    }
}
