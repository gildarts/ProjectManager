using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProjectManager.Project.UDS.Package;

namespace ProjectManager.ActionHandler.UDS.Service
{
    public partial class AddServiceForm : Form
    {
        internal event EventHandler<ServiceEventArg> Completed;

        private PackageHandler _package;
        private List<string> _tables;

        internal AddServiceForm(PackageHandler package)
        {
            InitializeComponent();
            _package = package;
            _tables =  MainForm.CurrentUDT.ListAllTables();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            err.Clear();
            bool valid = true;
            if (string.IsNullOrWhiteSpace(txtServiceName.Text))
            {
                err.SetError(txtServiceName, "不可空白");
                valid = false;
            }

            if (string.IsNullOrWhiteSpace(cboTable.Text))
            {
                err.SetError(cboTable, "不可空白");
                valid = false;
            }

            if (!_tables.Contains(cboTable.Text))
            {
                err.SetError(cboTable, "資料表不存在");
                valid = false;
            }

            if (_package.Contains(txtServiceName.Text))
            {
                err.SetError(txtServiceName, "Service 名稱重覆");
                valid = false;
            }

            if (!valid) return;

            if (Completed != null)
            {
                ServiceAction action = ServiceAction.Select;

                if (rbDelete.Checked)
                    action = ServiceAction.Delete;
                else if (rbUpdate.Checked)
                    action = ServiceAction.Update;
                else if (rbInsert.Checked)
                    action = ServiceAction.Insert;
                else if (rbSet.Checked)
                    action = ServiceAction.Set;
                else
                    action = ServiceAction.Select;

                ServiceEventArg arg = new ServiceEventArg(this.txtServiceName.Text, action, this.cboTable.Text);
                Completed.Invoke(this, arg);
            }
            this.Close();
        }

        private void AddServiceForm_Load(object sender, EventArgs e)
        {
            cboTable.Items.Clear();
            foreach (string table in _tables)
            {
                cboTable.Items.Add(table);
            }
        }
    }

    internal class ServiceEventArg : EventArgs
    {
        internal string ServiceName { get; private set; }
        internal ServiceAction Action { get; private set; }
        internal string TargetTable { get; private set; }

        public ServiceEventArg(string serviceName, ServiceAction action, string targetTable)
        {
            this.ServiceName = serviceName;
            this.Action = action;
            this.TargetTable = targetTable;
        }
    }

    public enum ServiceAction
    {
        Insert, Select, Update, Delete, Set
    }
}
