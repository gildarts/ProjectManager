using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FISCA.DSAClient;
using ProjectManager.Project;

namespace ProjectManager
{
    public partial class ProjectPreferenceForm : Form
    {
        public event EventHandler Saved;
        private string _originalData;
        internal ProjectHandler EditingProject {get; private set;}

        public ProjectPreferenceForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtPref.Text != _originalData)
            {
                DialogResult dr = MessageBox.Show("若編輯錯誤可能導致專案毀損, 是否繼續儲存 ?", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == System.Windows.Forms.DialogResult.No) return;

                EditingProject.UpdateProjectPreference(XmlHelper.ParseAsDOM(txtPref.Text));

                if (Saved != null)
                    Saved.Invoke(this, e);
            }
            this.Close();
        }

        private void ProjectPreferenceForm_Load(object sender, EventArgs e)
        {
            cboProjects.Items.Clear();
            int selected = 0;
            int index = 0;
            foreach (string projectName in MainForm.Projects.ListProjects())
            {
                cboProjects.Items.Add(projectName);
                if (MainForm.CurrentProject != null && projectName == MainForm.CurrentProject.Name)
                {
                    selected = index;                    
                }
                index++;
            }

            cboProjects.SelectedIndex = selected;

            //this.txtPref.Text = MainForm.CurrentProject.Preference.OuterXml;           
            
            //_originalData = this.txtPref.Text;
        }

        private void cboProjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            string projectName = cboProjects.SelectedItem.ToString();

            EditingProject = MainForm.Projects.LoadUnloadProjectHandler(projectName);

            this.txtPref.Text = EditingProject.GetProjectPreference().OuterXml;

            _originalData = this.txtPref.Text;
        }
    }
}
