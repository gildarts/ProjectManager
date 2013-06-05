using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectManager.ActionHandler
{
    interface ISyncDownload
    {
        //event EventHandler<NeedStatusEventArgs> NeedDownloadChanged;
        void Download();
    }
}
