using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProjectManager.Project.UDS.Contract;
using ProjectManager.ActionHandler.UDS.Contract.AuthEditors;
using System.Xml;
using FISCA.DSAClient;
using ProjectManager.ActionHandler.UDT;

namespace ProjectManager.ActionHandler.UDS.Contract
{
    public partial class ContractEditor : UserControl
    {
        internal event EventHandler DataChanged;
        internal event EventHandler ChangeRecovered;

        internal ContractNodeHandler ContractNodeHandler { get; private set; }
        internal ContractHandler Contract { get { return ContractNodeHandler.Contract; } }
        internal ContractUIEditable _owner;

        internal ContractEditor(ContractNodeHandler parent, ContractUIEditable owner)
        {
            InitializeComponent();
            ContractNodeHandler = parent;
            _owner = owner;
        }

        private void ContractEditor_Load(object sender, EventArgs e)
        {
            txtName.Text = Contract.Name;
            ChangeType(Contract.ExtendType);
            this.ContractNodeHandler.Contract.Renamed += delegate(object s, EventArgs arg)
            {
                ContractHandler ch = s as ContractHandler;
                txtName.Text = ch.Name;
            };
        }

        private ExtendType CurrentExtType
        {
            get
            {
                if (rbAdmin.Checked)
                    return ExtendType.admin;
                if (rbTA.Checked)
                    return ExtendType.ta;
                if (rbSA.Checked)
                    return ExtendType.sa;
                if (rbOther.Checked)
                    return ExtendType.none;
                if (rbPublic.Checked)
                    return ExtendType.open;
                return ExtendType.open;
            }
        }

        #region IEditable 成員

        public Control Editor { get; private set; }
                
        public bool Valid
        {
            get
            {
                IContractEditor edit = Editor as IContractEditor;
                return edit.Valid;
            }
        }

        public void Save()
        {
            if (!Valid)
                return;

            IContractEditor edit = Editor as IContractEditor;
            edit.Save();

            IResultGetter result = Editor as IResultGetter;
            XmlElement authElement = result.GetAuthElement();

            XmlHelper h = new XmlHelper("<Definition/>");
            h.AddElement(".", authElement);

            try
            {
                Contract.SetDefinition(h.GetElement("."), _owner);

                MessageBox.Show("儲存完畢！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("儲存失敗！" + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        private void rbPublic_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPublic.Checked)
                ChangeType();
        }

        private void rbTA_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTA.Checked)
                ChangeType();
        }

        private void rbSA_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSA.Checked)
                ChangeType();
        }

        private void rbAdmin_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAdmin.Checked)
                ChangeType();
        }

        private void rbOther_CheckedChanged(object sender, EventArgs e)
        {
            if (rbOther.Checked)
                ChangeType();
        }

        private void ChangeType()
        {
            this.ChangeType(CurrentExtType);
        }

        private void ChangeType(ExtendType type)
        {
            IContractEditor editor = null;
            if (type == ExtendType.open)
            {
                rbPublic.Checked = true;
                editor = new PublicAuthEditor();
            }
            else if (type == ExtendType.ta)
            {
                rbTA.Checked = true;
                editor = new ExtendAuthEditor(type);
            }
            else if (type == ExtendType.sa)
            {
                rbSA.Checked = true;
                editor = new ExtendAuthEditor(type);
            }
            else if (type == ExtendType.admin)
            {
                rbAdmin.Checked = true;
                editor = new ExtendAuthEditor(type);
            }
            else if (type == ExtendType.none)
            {
                rbOther.Checked = true;
                editor = new AdvAuthEditor();
            }
            else if (type == ExtendType.parent)
            {
                rbParent.Checked = true;
                editor = new ExtendAuthEditor(type);
            }

            DockControl(editor);
            editor.DataChanged += new EventHandler(editor_DataChanged);
            editor.ChangeRecovered += new EventHandler(editor_ChangeRecovered);
        }

        void editor_ChangeRecovered(object sender, EventArgs e)
        {
            if (ChangeRecovered != null)
                ChangeRecovered.Invoke(this, e);
        }

        void editor_DataChanged(object sender, EventArgs e)
        {
            if (DataChanged != null)
                DataChanged.Invoke(this, e);
        }

        private void DockControl(IContractEditor editor)
        {
            Control ctrl = editor as UserControl;
            panel.Controls.Clear();
            ctrl.Dock = DockStyle.Fill;
            panel.Controls.Add(ctrl);
            this.Editor = ctrl;

            if (Contract.Definition != null)
            {
                IResultGetter rg = ctrl as IResultGetter;
                rg.SetDefinition(Contract.Definition);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!Valid)
            {
                MessageBox.Show("文件內容有誤, 請修正後再行儲存", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.Save();
        }

        private void rbParent_CheckedChanged(object sender, EventArgs e)
        {
            if (rbParent.Checked)
                ChangeType();
        }

    }
}
