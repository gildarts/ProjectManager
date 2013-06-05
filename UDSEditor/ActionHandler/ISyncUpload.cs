using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectManager.ActionHandler
{
    interface ISyncUpload
    {
        //event EventHandler<NeedStatusEventArgs> NeedUploadChanged;
        void Upload();
    }

    class NeedStatusEventArgs : EventArgs
    {
        internal bool IsNeed { get; private set; }
        internal NeedStatusEventArgs(bool need)
        {
            IsNeed = need;
        }
    }
}
