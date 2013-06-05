using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectManager.ActionHandler
{
    interface IAddable
    {        
        string TitleOfAdd { get; }
        void Add();
    }
}
