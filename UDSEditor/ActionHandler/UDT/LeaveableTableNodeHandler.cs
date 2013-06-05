using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProjectManager.Project.UDT;

namespace ProjectManager.ActionHandler.UDT
{
    class LeaveableTableNodeHandler : TableNodeHandler, ILeaveProject
    {
        internal LeaveableTableNodeHandler(TreeNode node, UDTTable table)
            : base(node, table)
        {
        }
        #region ILeaveProject 成員

        public void LeaveProject()
        {
            MainForm.CurrentUDT.LeaveProject(this.Table);
        }

        #endregion
    }
}
