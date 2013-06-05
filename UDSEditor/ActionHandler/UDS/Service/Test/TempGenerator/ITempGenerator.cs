using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using ProjectManager.Project.UDS.Service;

namespace ProjectManager.ActionHandler.UDS.Service.Test.TempGenerator
{
    interface ITempGenerator
    {
        XmlElement Generate(XmlElement service);
    }
}
