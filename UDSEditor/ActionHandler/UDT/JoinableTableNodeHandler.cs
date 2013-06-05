using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProjectManager.Project.UDT;

namespace ProjectManager.ActionHandler.UDT
{
    class JoinableTableNodeHandler : TableNodeHandler, IJoinProject
    {
        internal JoinableTableNodeHandler(TreeNode node, UDTTable table)
            : base(node, table)
        {
        }

        #region IJoinProject 成員

        public void JoinProject()
        {
            MainForm.CurrentUDT.JoinProject(this.Table);
        }

        #endregion
    }
}
