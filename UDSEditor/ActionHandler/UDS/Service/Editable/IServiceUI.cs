using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using ProjectManager.Project.UDS.Service.Variable;

namespace ProjectManager.ActionHandler.UDS.Service.Editable
{
    interface IServiceUI
    {
        event EventHandler DataChanged;
        event EventHandler ChangeRecovered;
        event EventHandler SuggestionLoaded;

        void Initialize(string serviceFullName, XmlElement source);
        UserControl Editor { get; }
        bool Valid { get; }
        List<string> SuggestionTarget { get; }
        XmlElement GetResult();
        EditVariableForm EditVarForm { get; }
    }
}
