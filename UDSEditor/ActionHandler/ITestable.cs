using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectManager.ActionHandler
{
    interface ITestable
    {
        string TitleOfTest { get; }
        string TestImageKey { get; }
        void Test();
    }
}
