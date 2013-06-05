using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProjectManager.Project.UDS.Contract;

namespace ProjectManager.ActionHandler.UDS.Contract
{
    class LeaveableContractNodeHandler:ContractNodeHandler, ILeaveProject
    {
        internal LeaveableContractNodeHandler(TreeNode node, ContractHandler contract)
            : base(node, contract)
        {
        }

        #region ILeaveProject 成員

        public void LeaveProject()
        {
            MainForm.CurrentUDS.LeaveProject(this.Contract.Name);
        }

        #endregion
    }
}
