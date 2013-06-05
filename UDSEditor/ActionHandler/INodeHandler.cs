using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProjectManager.ActionHandler
{
    interface INodeHandler
    {
        TreeNode Node { get; }

        bool IsFirstClick { get; }

        void OnFirstClick();

        void OnClick();
    }
}
