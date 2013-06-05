using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectManager.ActionHandler
{
    interface IEditorManager
    {
        List<IEditable> Editors { get; }
        IEditable CurrentEditor { get; set; }
    }
}
