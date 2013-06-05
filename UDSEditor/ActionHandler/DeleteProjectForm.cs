using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProjectManager.Project;
using ProjectManager.Project.UDS.Contract;
using ProjectManager.Project.UDT;
using System.Threading.Tasks;
using FISCA.DSAClient;

namespace ProjectManager.ActionHandler
{
    public partial class DeleteProjectForm : Form
    {       
        private ProjectHandler _project;

        internal DeleteProjectForm(ProjectHandler project)
        {
            InitializeComponent();
            _project = project;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DeleteProjectForm_Load(object sender, EventArgs e)
        {
            this.btnCancel.Focus();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (rbAll.Checked)
            {
                XmlHelper sh = new XmlHelper();
                XmlHelper th = new XmlHelper();

                Parallel.ForEach(MainForm.CurrentUDS.Contracts, contract =>
                {
                    sh.AddElement(".", "ContractName", contract.Name);
                });
                
                Parallel.ForEach(MainForm.CurrentUDT.Tables, table =>
                {
                    th.AddElement(".", "TableName", table.Name);
                });

                try
                {
                    _project.SendRequest("UDSManagerService.DeleteContracts", new Envelope(sh));
                }
                catch { }
                try
                {
                    _project.SendRequest("UDTService.DDL.DropTables", new Envelope(th));
                }
                catch { }
            }

            MainForm.Projects.RemoveProject(_project.Name);            
            this.Close();
        }
    }
}
