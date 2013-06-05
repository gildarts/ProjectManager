using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ProjectManager.ActionHandler.UDT.Command
{
    interface IUDTCommand
    {
        string Type { get; }
        string Description { get; }
        XmlElement Sample { get; }
        XmlElement Result { get; set; }
    }
}
