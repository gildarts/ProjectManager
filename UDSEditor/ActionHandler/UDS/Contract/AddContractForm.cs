using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProjectManager.ActionHandler.UDS.Contract
{
    public partial class AddContractForm : Form
    {
        internal string ContractName { get; private set; }

        internal event EventHandler Added;

        private UDSNodeHandler UDSNodeHandler{get;set;}

        internal AddContractForm(UDSNodeHandler parent)
        {
            InitializeComponent();
            UDSNodeHandler = parent;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            err.Clear();

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                err.SetError(txtName, "名稱不可空白");
                return;
            }

            if(UDSNodeHandler.UDSHandler.ContainContract(txtName.Text))
                    err.SetError(txtName, "此名稱已被其它 Contract 使用");
            

            ContractName = txtName.Text;

            ExtendType extend = ExtendType.open;
            
            if (rbAdmin.Checked)
                extend = ExtendType.admin;

            if (rbSA.Checked)
                extend = ExtendType.sa;

            if (rbOther.Checked)
                extend = ExtendType.none;

            if (rbTA.Checked)
                extend = ExtendType.ta;

            try
            {
                UDSNodeHandler.UDSHandler.AddContract(ContractName, extend);

                if (Added != null)
                    Added.Invoke(this, EventArgs.Empty);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("新增失敗 :\n" + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
