using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectManager.Uploader
{
    class UploaderFactory
    {
        internal static IUploader CreateInstance()
        {
            return new FtpUploader();
        }
    }
}
