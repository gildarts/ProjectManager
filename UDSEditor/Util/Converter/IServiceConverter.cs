using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ProjectManager.Util.Converter
{
    interface IServiceConverter
    {
        XElement ToUDS(XElement physicalService);
        XElement ToPhysical(XElement udsService);
    }
}
