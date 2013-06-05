using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;

namespace ProjectManager.Project.UDS.Service.Variable
{
    interface IVariable
    {
        string Name { get; }
        string Source { get; }
        bool Valid { get; }
        string InvalidMessage { get; }
        Control Editor { get; }
        XmlElement GetXml();

        string Memo { get; }
    }
}
