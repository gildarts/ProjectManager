using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProjectManager.ActionHandler
{
    interface IEditable
    {
        event EventHandler EditorChanged;

        bool IsEditing { get; set; }

        string ModeTitle { get; }

        string ImageKey { get; }

        string DocumentTitle { get; }

        Control Editor { get; }

        event EventHandler DataChanged;

        event EventHandler ChangeRecovered;

        event EventHandler EditorReloaded;

        bool Valid { get; }

        void OnStartEditing();

        void Save();
    }
}
