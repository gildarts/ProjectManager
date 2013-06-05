using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProjectManager.Project.UDT;
using ProjectManager.ActionHandler.UDS.Contract;

namespace ProjectManager.ActionHandler.UDT
{
    public partial class UDTTableEditor : UserControl, IContractEditor
    {
        private UDTNodeHandler _udtNodeHandler;

        internal UDTTableEditor()
        {
            InitializeComponent();            
        }
        
        internal void LoadData(UDTNodeHandler udtNodeHandler)
        {
            _udtNodeHandler = udtNodeHandler;

            lstProject.DisplayMember = "Name";
            lstAll.DisplayMember = "Name";

            foreach (UDTTable table in _udtNodeHandler.UDTHandler.Tables)
                lstProject.Items.Add(table);

            foreach (UDTTable table in _udtNodeHandler.UDTHandler.AllUDTTables)
            {
                if (!_udtNodeHandler.UDTHandler.Tables.Contains(table))
                    lstAll.Items.Add(table);
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
            if (JoinTables.Count == 0 && LeaveTables.Count == 0)
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

        internal List<UDTTable> LeaveTables
        {
            get
            {
                List<UDTTable> list = new List<UDTTable>();

                foreach (UDTTable table in _udtNodeHandler.UDTHandler.Tables)
                {
                    if (lstProject.Items.Contains(table)) continue;
                    list.Add(table);
                }

                return list;
            }
        }

        internal List<UDTTable> JoinTables
        {
            get
            {
                List<UDTTable> list = new List<UDTTable>();
                foreach (UDTTable table in lstProject.Items)
                {
                    if (_udtNodeHandler.UDTHandler.Tables.Contains(table)) continue;
                    list.Add(table);
                }

                return list;
            }
        }

        #region IContractEditor 成員

        public bool Valid
        {
            get { return true; }
        }

        public void Save()
        {
            List<UDTTable> jt = JoinTables;
            List<UDTTable> lt = LeaveTables;            
            _udtNodeHandler.UDTHandler.ModifyProject(jt, lt);

            if (ChangeRecovered != null)
                ChangeRecovered.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler DataChanged;

        public event EventHandler ChangeRecovered;

        #endregion
    }
}
