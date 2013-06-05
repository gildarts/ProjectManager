﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectManager.ActionHandler
{
    interface ISetupable
    {
        string SetupTitle { get; }
        void Setup();
    }
}
