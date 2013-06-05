using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FISCA.DSAClient;
using System.Xml;
using ProjectManager.ActionHandler.UDT;

namespace ProjectManager.ActionHandler.UDS.Contract.AuthEditors
{
    public partial class AdvAuthEditor : UserControl, IResultGetter, IContractEditor
    {
        private bool _initialized;

        public AdvAuthEditor()
        {
            InitializeComponent();
            _initialized = false;

            txtGetUserDataQuery.TextChanged+=new EventHandler(txtGetUserDataQuery_TextChanged);
            txtGetUserRoleQuery.TextChanged+=new EventHandler(txtGetUserRoleQuery_TextChanged);
        }

        #region IResultGetter 成員

        public void SetDefinition(System.Xml.XmlElement definition)
        {
            XmlHelper h = new XmlHelper(definition);
            _initialized = false;

            //Check Basic Tab
            chkBasic.Checked = CheckEnable(h.GetElement("Authentication/Basic"), true);
            cbHashProvider.Text = h.GetText("Authentication/Basic/PasswordHashProvider/@DriverClass");
            txtGetUserDataQuery.Text = h.GetText("Authentication/Basic/UserInfoStorage/DBSchema/GetUserDataQuery");
            txtGetUserRoleQuery.Text = h.GetText("Authentication/Basic/UserInfoStorage/DBSchema/GetUserRolesQuery");

            //Check Session Tab
            chkSession.Checked = h.TryGetBoolean("Authentication/Session/@Enabled", true);
            txtTimeout.Text = h.TryGetInteger("Authentication/Session/@Timeout", 20).ToString();

            //Check Passport Tab
            chkPassport.Checked = CheckEnable(h.GetElement("Authentication/Passport"), false);
            txtIssuer.Text = h.GetText("Authentication/Passport/Issuer/@Name");
            txtCertProvider.Text = h.GetText("Authentication/Passport/Issuer/CertificateProvider");
            cbALTable.Text = h.GetText("Authentication/Passport/AccountLinking/TableName");
            cboMappingField.Text = h.GetText("Authentication/Passport/AccountLinking/MappingField");
            cbUserNameField.Text = h.GetText("Authentication/Passport/AccountLinking/UserNameField");

            dgExtProp.Rows.Clear();
            foreach (XmlElement pe in h.GetElements("Authentication/Passport/AccountLinking/Properties/Property"))
            {
                int index = dgExtProp.Rows.Add();
                DataGridViewRow row = dgExtProp.Rows[index];
                row.Cells[colAlias.Name].Value = pe.GetAttribute("Alias");
                row.Cells[colDBField.Name].Value = pe.GetAttribute("Field");
            }

            CheckTabs();
            _initialized = true;
        }

        private bool CheckEnable(XmlElement xml, bool defaultValue)
        {
            if (xml == null) return false;
            XmlHelper h = new XmlHelper(xml);
            return h.TryGetBoolean("@Enabled", defaultValue);
        }

        public System.Xml.XmlElement GetAuthElement()
        {
            XmlHelper h = new XmlHelper("<Authentication/>");
            h.AddElement(".", "Basic");
            h.SetAttribute("Basic", "Enabled", chkBasic.Enabled.ToString());
            h.AddElement("Basic", "PasswordHashProvider");
            h.SetAttribute("Basic/PasswordHashProvider", "DriverClass", cbHashProvider.Text);
            h.AddElement("Basic", "UserInfoStorage");
            h.SetAttribute("Basic/UserInfoStorage", "Type", "Database");
            h.AddElement("Basic/UserInfoStorage", "DBSchema");
            XmlElement xml = h.AddElement("Basic/UserInfoStorage/DBSchema", "GetUserDataQuery");
            XmlCDataSection section = xml.OwnerDocument.CreateCDataSection(txtGetUserDataQuery.Text);
            xml.AppendChild(section);
            xml = h.AddElement("Basic/UserInfoStorage/DBSchema", "GetUserRolesQuery");
            section = xml.OwnerDocument.CreateCDataSection(txtGetUserRoleQuery.Text);
            xml.AppendChild(section);

            xml = h.AddElement(".", "Session");
            xml.SetAttribute("Enabled", chkSession.Checked.ToString());
            xml.SetAttribute("Timeout", txtTimeout.Text);

            h.AddElement(".", "Passport");
            h.SetAttribute("Passport", "Enabled", chkPassport.Enabled.ToString());
            h.AddElement("Passport", "Issuer");
            h.SetAttribute("Passport/Issuer", "Name", txtIssuer.Text);
            h.AddElement("Passport/Issuer", "CertificateProvider", txtCertProvider.Text);
            h.SetAttribute("Passport/Issuer/CertificateProvider", "Type", "HttpGet");

            if (rbEnable.Checked)
            {
                h.AddElement("Passport", "AccountLinking");
                h.SetAttribute("Passport/AccountLinking", "Type", "mapping");
                h.AddElement("Passport/AccountLinking", "TableName", cbALTable.Text);
                h.AddElement("Passport/AccountLinking", "UserNameField", cbUserNameField.Text);
                h.AddElement("Passport/AccountLinking", "MappingField", cboMappingField.Text);

                if (dgExtProp.Rows.Count > 0)
                    h.AddElement("Passport/AccountLinking", "Properties");

                foreach (DataGridViewRow row in dgExtProp.Rows)
                {
                    xml = h.AddElement("Passport/AccountLinking/Properties", "Property");
                    xml.SetAttribute("Field", row.Cells[colDBField.Name].Value.ToString());
                    xml.SetAttribute("Alias", row.Cells[colAlias.Name].Value.ToString());
                }
            }
            return h.GetElement(".");
        }

        #endregion

        private void chkBasic_CheckedChanged(object sender, EventArgs e)
        {
            CheckTab(tpBasic);
            if (_initialized)
                DoChanged(true);
        }

        private void chkSession_CheckedChanged(object sender, EventArgs e)
        {
            CheckTab(tpSession);
            if (_initialized)
                DoChanged(true);
        }

        private void chkPassport_CheckedChanged(object sender, EventArgs e)
        {
            CheckTab(tpPassport);
            if (_initialized)
                DoChanged(true);
        }

        private void EnableTab(TabPage tp)
        {
            foreach (Control ctrl in tp.Controls)
                ctrl.Enabled = true;
        }

        private void DisableTab(TabPage tp)
        {
            foreach (Control ctrl in tp.Controls)
                ctrl.Enabled = (ctrl is CheckBox);
        }

        private void CheckTab(TabPage tp)
        {
            CheckBox checkBox = null;
            foreach (Control ctrl in tp.Controls)
            {
                if (ctrl is CheckBox)
                {
                    checkBox = ctrl as CheckBox;
                    break;
                }
            }

            if (checkBox.Checked)
                EnableTab(tp);
            else
                DisableTab(tp);
        }

        private void CheckTabs()
        {
            foreach (TabPage tp in tab.TabPages)
                CheckTab(tp);
        }

        private void picCheck_Click(object sender, EventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load(txtCertProvider.Text);
                MessageBox.Show("測試取得公開金鑰成功 : \n" + doc.OuterXml, "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("此網址目前無法正確取得公開金鑰 : \n" + ex.Message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //#region IEditable 成員

        //public string DocumentTitle { get { return string.Empty; } }

        //public Control Editor
        //{
        //    get { return this; }
        //}

        //private bool _changed;
        //public bool DataChanged
        //{
        //    get { return _changed; }
        //    private set
        //    {
        //        _changed = value;
        //    }
        //}

        //public bool Valid
        //{
        //    get
        //    {
        //        err.Clear();
        //        bool valid = true;

        //        if (chkBasic.Checked)
        //        {
        //            if (string.IsNullOrWhiteSpace(txtGetUserDataQuery.Text))
        //            {
        //                err.SetError(txtGetUserDataQuery, "不可空白");
        //                valid = false;
        //            }
        //            if (string.IsNullOrWhiteSpace(txtGetUserRoleQuery.Text))
        //            {
        //                err.SetError(txtGetUserRoleQuery, "不可空白");
        //                valid = false;
        //            }
        //        }

        //        if (chkSession.Checked)
        //        {
        //            int timeout;
        //            if (!int.TryParse(txtTimeout.Text, out timeout))
        //            {
        //                err.SetError(label4, "必須為整數");
        //                valid = false;
        //            }
        //        }

        //        if (chkPassport.Checked)
        //        {
        //            if (string.IsNullOrWhiteSpace(txtIssuer.Text))
        //            {
        //                err.SetError(txtIssuer, "不可為空白");
        //                valid = false;
        //            }

        //            if (string.IsNullOrWhiteSpace(txtCertProvider.Text))
        //            {
        //                err.SetError(txtCertProvider, "不可為空白");
        //                valid = false;
        //            }

        //            if (rbEnable.Checked)
        //            {
        //                if (string.IsNullOrWhiteSpace(cbALTable.Text))
        //                {
        //                    err.SetError(cbALTable, "不可為空白");
        //                    valid = false;
        //                }

        //                if (string.IsNullOrWhiteSpace(cboMappingField.Text))
        //                {
        //                    err.SetError(cboMappingField, "不可為空白");
        //                    valid = false;
        //                }

        //                if (string.IsNullOrWhiteSpace(cbUserNameField.Text))
        //                {
        //                    err.SetError(cbUserNameField, "不可為空白");
        //                    valid = false;
        //                }
        //            }
        //        }

        //        return valid;
        //    }
        //}

        //public void Save()
        //{
        //    this.DataChanged = false;
        //}

        //#endregion

        private void AdvAuthEditor_Load(object sender, EventArgs e)
        {
            List<string> tables = MainForm.CurrentUDT.ListAllTables();
            cbALTable.Items.Clear();
            cbALTable.Items.AddRange(tables.ToArray());
        }

        private void cbALTable_TextChanged(object sender, EventArgs e)
        {
            if (!MainForm.CurrentUDT.ExistsInAllTables(cbALTable.Text)) return;

            List<string> list = MainForm.CurrentUDT.ListFields(cbALTable.Text);

            cboMappingField.Items.Clear();
            cbUserNameField.Items.Clear();
            cboMappingField.Text = string.Empty;
            cbUserNameField.Text = string.Empty;
            cboMappingField.Items.AddRange(list.ToArray());
            cbUserNameField.Items.AddRange(list.ToArray());
            dgExtProp.Rows.Clear();

            if (_initialized)
                DoChanged(true);
        }

        private void txtIssuer_TextChanged(object sender, EventArgs e)
        {
            if (_initialized)
                DoChanged(true);
        }

        private void txtCertProvider_TextChanged(object sender, EventArgs e)
        {
            if (_initialized)
                DoChanged(true);
        }

        private void rbDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (_initialized)
                DoChanged(true);
        }

        private void rbEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (_initialized)
                DoChanged(true);
        }

        private void cboMappingField_TextChanged(object sender, EventArgs e)
        {
            if (_initialized)
                DoChanged(true);
        }

        private void cbUserNameField_TextChanged(object sender, EventArgs e)
        {
            if (_initialized)
                DoChanged(true);
        }

        private void cbHashProvider_TextChanged(object sender, EventArgs e)
        {
            if (_initialized)
                DoChanged(true);
        }

        private void txtGetUserDataQuery_TextChanged(object sender, EventArgs e)
        {
            if (_initialized)
                DoChanged(true);
        }

        private void txtGetUserRoleQuery_TextChanged(object sender, EventArgs e)
        {
            if (_initialized)
                DoChanged(true);
        }

        private void txtTimeout_TextChanged(object sender, EventArgs e)
        {
            if (_initialized)
                DoChanged(true);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgExtProp.SelectedRows.Count == 0) return;

            dgExtProp.Rows.Remove(dgExtProp.SelectedRows[0]);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cbALTable.Text))
                return;

            List<string> list = MainForm.CurrentUDT.ListFields(cbALTable.Text);
            AddExtPropForm addForm = new AddExtPropForm(list.ToArray());
            addForm.StartPosition = FormStartPosition.CenterParent;
            addForm.Completed += new EventHandler(addForm_Completed);
            addForm.ShowDialog();
        }

        void addForm_Completed(object sender, EventArgs e)
        {
            AddExtPropForm addForm = sender as AddExtPropForm;
            addForm.Completed -= new EventHandler(addForm_Completed);

            int index = dgExtProp.Rows.Add();
            dgExtProp.Rows[index].Cells[colDBField.Name].Value = addForm.FieldName;
            dgExtProp.Rows[index].Cells[colAlias.Name].Value = addForm.AliasName;
        }

        private bool _changed;
        private void DoChanged(bool changed)
        {
            if (changed && DataChanged != null)
                DataChanged.Invoke(this, EventArgs.Empty);

            if (_changed && !changed && ChangeRecovered != null)
                ChangeRecovered.Invoke(this, EventArgs.Empty);

            _changed = changed;
        }

        #region IContractEditor 成員

        public bool Valid
        {
            get
            {
                err.Clear();
                bool valid = true;

                if (chkBasic.Checked)
                {
                    if (string.IsNullOrWhiteSpace(txtGetUserDataQuery.Text))
                    {
                        err.SetError(txtGetUserDataQuery, "不可空白");
                        valid = false;
                    }
                    if (string.IsNullOrWhiteSpace(txtGetUserRoleQuery.Text))
                    {
                        err.SetError(txtGetUserRoleQuery, "不可空白");
                        valid = false;
                    }
                }

                if (chkSession.Checked)
                {
                    int timeout;
                    if (!int.TryParse(txtTimeout.Text, out timeout))
                    {
                        err.SetError(label4, "必須為整數");
                        valid = false;
                    }
                }

                if (chkPassport.Checked)
                {
                    if (string.IsNullOrWhiteSpace(txtIssuer.Text))
                    {
                        err.SetError(txtIssuer, "不可為空白");
                        valid = false;
                    }

                    if (string.IsNullOrWhiteSpace(txtCertProvider.Text))
                    {
                        err.SetError(txtCertProvider, "不可為空白");
                        valid = false;
                    }

                    if (rbEnable.Checked)
                    {
                        if (string.IsNullOrWhiteSpace(cbALTable.Text))
                        {
                            err.SetError(cbALTable, "不可為空白");
                            valid = false;
                        }

                        if (string.IsNullOrWhiteSpace(cboMappingField.Text))
                        {
                            err.SetError(cboMappingField, "不可為空白");
                            valid = false;
                        }

                        if (string.IsNullOrWhiteSpace(cbUserNameField.Text))
                        {
                            err.SetError(cbUserNameField, "不可為空白");
                            valid = false;
                        }
                    }
                }

                return valid;
            }
        }

        public void Save()
        {
            DoChanged(false);
        }

        public event EventHandler DataChanged;

        public event EventHandler ChangeRecovered;

        #endregion

    }
}
