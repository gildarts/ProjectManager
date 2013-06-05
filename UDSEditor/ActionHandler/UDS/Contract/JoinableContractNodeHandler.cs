using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProjectManager.Project.UDS.Contract;

namespace ProjectManager.ActionHandler.UDS.Contract
{
    class JoinableContractNodeHandler : ContractNodeHandler, IJoinProject
    {
        internal JoinableContractNodeHandler(TreeNode node, ContractHandler contract)
            : base(node, contract)
        {
        }

        #region IJoinProject 成員

        public void JoinProject()
        {
            MainForm.CurrentUDS.JoinProject(this.Contract);
        }

        #endregion
    }
}
