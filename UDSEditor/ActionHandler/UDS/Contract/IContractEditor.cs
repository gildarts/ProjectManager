using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectManager.ActionHandler.UDS.Contract
{
    interface IContractEditor
    {
        bool Valid { get; }
        void Save();
        event EventHandler DataChanged;
        event EventHandler ChangeRecovered;

    }
}
