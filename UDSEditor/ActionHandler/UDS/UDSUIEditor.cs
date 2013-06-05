using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProjectManager.ActionHandler.UDS.Contract;
using ProjectManager.Project.UDS.Contract;

namespace ProjectManager.ActionHandler.UDS
{
    public partial class UDSUIEditor : UserControl, IContractEditor
    {
        private UDSNodeHandler _udsNodeHandler;

        internal UDSUIEditor(UDSNodeHandler udsNodeHandler)
        {
            InitializeComponent();
            _udsNodeHandler = udsNodeHandler;
        }

        #region IContractEditor 成員

        public bool Valid
        {
            get { return true; }
        }

        public void Save()
        {
            List<ContractHandler> jt = JoinContracts;
            List<ContractHandler> lt = LeaveContracts;
            _udsNodeHandler.UDSHandler.JoinProjects(jt);
            _udsNodeHandler.UDSHandler.LeaveProjects(lt);

            if (ChangeRecovered != null)
                ChangeRecovered.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler DataChanged;

        public event EventHandler ChangeRecovered;

        #endregion

        private void UDSUIEditor_Load(object sender, EventArgs e)
        {
            lstProject.DisplayMember = "Name";
            lstAll.DisplayMember = "Name";

            foreach (ContractHandler contract in _udsNodeHandler.UDSHandler.Contracts)
                lstProject.Items.Add(contract);

            foreach (ContractHandler contract in _udsNodeHandler.UDSHandler.AllContracts)
            {
                if (!_udsNodeHandler.UDSHandler.Contracts.Contains(contract))
                    lstAll.Items.Add(contract);
            }
        }

        private void btnGoRight_Click(object sender, EventArgs e)
        {
            if (lstAll.SelectedItems.Count == 0) return;

            List<object> items = new List<object>();
            foreach (object item in lstAll.SelectedItems)
                items.Add(item);

            foreach (object item in items)
            {
                lstProject.Items.Add(item);
                lstAll.Items.Remove(item);
            }

            RaiseEvents();
        }

        private void RaiseEvents()
        {
            if (JoinContracts.Count == 0 && LeaveContracts.Count == 0)
            {
                if (ChangeRecovered != null)
                    ChangeRecovered.Invoke(this, EventArgs.Empty);
            }
            else
            {
                if (DataChanged != null)
                    DataChanged.Invoke(this, EventArgs.Empty);
            }
        }

        private void btnGoLeft_Click(object sender, EventArgs e)
        {
            if (lstProject.SelectedItems.Count == 0) return;

            List<object> items = new List<object>();
            foreach (object item in lstProject.SelectedItems)
                items.Add(item);

            foreach (object item in items)
            {
                lstAll.Items.Add(item);
                lstProject.Items.Remove(item);
            }

            RaiseEvents();
        }

        internal List<ContractHandler> LeaveContracts
        {
            get
            {
                List<ContractHandler> list = new List<ContractHandler>();

                foreach (ContractHandler contract in _udsNodeHandler.UDSHandler.Contracts)
                {
                    if (lstProject.Items.Contains(contract)) continue;
                    list.Add(contract);
                }

                return list;
            }
        }

        internal List<ContractHandler> JoinContracts
        {
            get
            {
                List<ContractHandler> list = new List<ContractHandler>();
                foreach (ContractHandler contract in lstProject.Items)
                {
                    if (_udsNodeHandler.UDSHandler.Contracts.Contains(contract)) continue;
                    list.Add(contract);
                }

                return list;
            }
        }
    }
}
