using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using FISCA.DSAClient;
using System.Xml.Linq;

namespace ProjectManager.ActionHandler.UDS.Contract.AuthEditors
{
    public partial class ExtendAuthEditor : UserControl, IContractEditor, IResultGetter
    {
        private ExtendType Type { get; set; }


        private string[] PropertySuggest
        {
            get
            {
                List<string> propList = new List<string>();

                if (Type == ExtendType.ta)
                {
                    propList.Add("TeacherID");
                    propList.Add("LoginType");
                }
                else if (Type == ExtendType.sa)
                {
                    propList.Add("StudentID");
                    propList.Add("LoginType");
                    propList.Add("LoginName");

                }
                return propList.ToArray();
            }
        }

        private string[] RoleSuggest
        {
            get
            {
                List<string> roles = new List<string>();

                if (Type == ExtendType.ta)
                    roles.Add("Teacher");

                else if (Type == ExtendType.sa)
                    roles.Add("Student");

                else if (Type == ExtendType.admin)
                    roles.Add("sysadmin");

                return roles.ToArray();
            }
        }

        internal ExtendAuthEditor(ExtendType type)
        {
            InitializeComponent();
            this.Type = type;
        }

        private void btnAddRestrict_Click(object sender, EventArgs e)
        {
            AddRestrictForm addRestrictForm = new AddRestrictForm(this.PropertySuggest, this.RoleSuggest);
            addRestrictForm.Completed += new EventHandler<RestrictEventArgs>(addRestrictForm_Completed);
            addRestrictForm.StartPosition = FormStartPosition.CenterParent;
            addRestrictForm.ShowDialog();
        }

        void addRestrictForm_Completed(object sender, RestrictEventArgs e)
        {
            AddRestrictForm addRestrictForm = sender as AddRestrictForm;
            addRestrictForm.Completed -= new EventHandler<RestrictEventArgs>(addRestrictForm_Completed);

            int index = dgRestrict.Rows.Add();
            DataGridViewRow row = dgRestrict.Rows[index];

            this.SetData(row, e);
        }


        private void btnDeleteRestrict_Click(object sender, EventArgs e)
        {
            if (dgRestrict.SelectedRows.Count == 0) return;

            dgRestrict.Rows.Remove(dgRestrict.SelectedRows[0]);
        }

        private void btnEditRestrict_Click(object sender, EventArgs e)
        {
            if (dgRestrict.SelectedRows.Count == 0) return;

            XmlElement xml = dgRestrict.SelectedRows[0].Tag as XmlElement;
            AddRestrictForm editRestrictForm = new AddRestrictForm(PropertySuggest, RoleSuggest, xml);
            editRestrictForm.StartPosition = FormStartPosition.CenterParent;
            editRestrictForm.Completed += new EventHandler<RestrictEventArgs>(editRestrictForm_Completed);
            editRestrictForm.ShowDialog();
        }

        void editRestrictForm_Completed(object sender, RestrictEventArgs e)
        {
            AddRestrictForm editRestrictForm = sender as AddRestrictForm;
            editRestrictForm.Completed -= new EventHandler<RestrictEventArgs>(editRestrictForm_Completed);

            DataGridViewRow row = dgRestrict.SelectedRows[0];
            SetData(row, e);
        }


        private void SetData(DataGridViewRow row, RestrictEventArgs arg)
        {
            row.Tag = arg.Result;
            row.Cells[colCondition.Name].Value = ExtendAuthEditor.GetDisplay(arg.Result);
            row.Cells[colRestrictType.Name].Value = arg.RestrictType.ToString();

            DoChanged(true);
        }

        private void btnAddExt_Click(object sender, EventArgs e)
        {
            ExtPropForm extForm = new ExtPropForm();
            extForm.StartPosition = FormStartPosition.CenterParent;
            extForm.Completed += new EventHandler<ExtEventArg>(extForm_Completed);
            extForm.ShowDialog();
        }

        void extForm_Completed(object sender, ExtEventArg e)
        {
            ExtPropForm extForm = sender as ExtPropForm;
            extForm.Completed -= new EventHandler<ExtEventArg>(extForm_Completed);

            int index = dgExt.Rows.Add();
            dgExt.Rows[index].Cells[dgExtType.Name].Value = e.ExtType;
            dgExt.Rows[index].Cells[dgExtDisplay.Name].Value = ExtendAuthEditor.GetDisplay(e.Result);
            dgExt.Rows[index].Tag = e.Result;
        }

        #region IResultGetter 成員

        public XmlElement GetAuthElement()
        {
            XmlHelper h = new XmlHelper("<Authentication />");
            h.SetAttribute(".", "Extends", Type.ToString());


            if (dgRestrict.Rows.Count > 0 || dgExt.Rows.Count > 0)
                h.AddElement(".", "UserInfoProcessors");

            if (dgRestrict.Rows.Count > 0)
            {
                XmlElement xml = h.AddElement("UserInfoProcessors", "Processor");
                xml.SetAttribute("Type", "restrict");

                XmlHelper xh = new XmlHelper(xml);
                foreach (DataGridViewRow row in this.dgRestrict.Rows)
                {
                    XmlElement res = row.Tag as XmlElement;
                    xh.AddElement(".", res);
                }
            }

            foreach (DataGridViewRow row in this.dgExt.Rows)
            {
                XmlElement xml = h.AddElement("UserInfoProcessors", "Processor");
                xml.SetAttribute("Type", "ExtendProperty");

                XmlHelper xh = new XmlHelper(xml);

                XmlElement res = row.Tag as XmlElement;
                xh.AddElement(".", res);
            }

            return h.GetElement(".");
        }

        #endregion

        private void btnEditExt_Click(object sender, EventArgs e)
        {
            if (dgExt.SelectedRows.Count == 0) return;

            DataGridViewRow row = dgExt.SelectedRows[0];
            XmlElement x = row.Tag as XmlElement;

            ExtPropForm extEdotForm = new ExtPropForm(x);
            extEdotForm.StartPosition = FormStartPosition.CenterParent;
            extEdotForm.Completed += new EventHandler<ExtEventArg>(extEdotForm_Completed);
            extEdotForm.ShowDialog();
        }

        void extEdotForm_Completed(object sender, ExtEventArg e)
        {
            DataGridViewRow row = dgExt.SelectedRows[0];
            row.Cells[dgExtDisplay.Name].Value = ExtendAuthEditor.GetDisplay(e.Result);
            row.Cells[dgExtType.Name].Value = e.ExtType;
            row.Tag = e.Result;
        }

        private void btnDeleteExt_Click(object sender, EventArgs e)
        {
            if (dgExt.SelectedRows.Count == 0) return;

            DataGridViewRow row = dgExt.SelectedRows[0];
            dgExt.Rows.Remove(row);
        }

        #region IResultGetter 成員

        public void SetDefinition(XmlElement definition)
        {
            XmlHelper h = new XmlHelper(definition);

            dgExt.Rows.Clear();
            dgRestrict.Rows.Clear();

            foreach (XmlElement x in h.GetElements("Authentication/UserInfoProcessors/Processor/Restrict"))
            {
                int index = dgRestrict.Rows.Add();
                DataGridViewRow row = dgRestrict.Rows[index];
                row.Cells[colRestrictType.Name].Value = x.GetAttribute("Type");
                row.Cells[colCondition.Name].Value = ExtendAuthEditor.GetDisplay(x);
                row.Tag = x;
            }

            foreach (XmlElement x in h.GetElements("Authentication/UserInfoProcessors/Processor/ExtendProperty"))
            {
                int index = dgExt.Rows.Add();
                DataGridViewRow row = dgExt.Rows[index];
                row.Cells[dgExtType.Name].Value = x.GetAttribute("Type");
                row.Cells[dgExtDisplay.Name].Value = ExtendAuthEditor.GetDisplay(x);
                row.Tag = x;
            }
        }

        #endregion

        internal static string GetDisplay(XmlElement xml)
        {
            string display = string.Empty;
            if (xml.Name == "Restrict")
            {
                string type = xml.GetAttribute("Type");
                if (type.Equals("PropertyEquals", StringComparison.CurrentCultureIgnoreCase))
                    display = "使用者資訊屬性『" + xml.GetAttribute("Property") + "』必須為「" + xml.GetAttribute("Value") + "」";
                else if (type.Equals("RoleContain", StringComparison.CurrentCultureIgnoreCase))
                    display = "使用者必須含有身份『" + xml.GetAttribute("Role") + "』";
                else
                    display = "使用者通過資料庫檢驗『" + xml.InnerText + "』";
            }
            else
                display = xml.InnerText;

            return display;
        }

        private bool _changed;
        private void DoChanged(bool changed)
        {
            if (changed && DataChanged != null)            
                DataChanged.Invoke(this, EventArgs.Empty);                
            
            if(_changed && !changed && ChangeRecovered != null)
                ChangeRecovered.Invoke(this, EventArgs.Empty);

            _changed = changed;
        }

        #region IContractEditor 成員

        public bool Valid
        {
            get { return true; }
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
