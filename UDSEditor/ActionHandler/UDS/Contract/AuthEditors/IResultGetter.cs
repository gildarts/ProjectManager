using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ProjectManager.ActionHandler.UDS.Contract.AuthEditors
{
    interface IResultGetter
    {
        void SetDefinition(XmlElement definition);
        XmlElement GetAuthElement();
    }
}
