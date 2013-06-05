using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProjectManager.Project;
using System.Xml;
using FISCA.DSAClient;
using ProjectManager.Project.UDT;
using ProjectManager.Project.UDS.Contract;

namespace ProjectManager.ActionHandler
{
    class ProjectNodeHandler : INodeHandler, IExportable, IDeleteable, IReloadable, IDeployable
    {
        internal ProjectHandler Project { get; private set; }

        public ProjectNodeHandler(TreeNode node, ProjectHandler project)
        {
            this.Node = node;
            Project = project;
        }
        #region IExportable 成員
        private const string FILE_EXT = "pml";
        public void Export()
        {
            foreach(ContractHandler contract in MainForm.CurrentUDS.Contracts){
                if (contract.Ready) continue;

                MessageBox.Show("尚有 Contract 未準備完成, 請稍侯再試!", "資訊", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog sd = new SaveFileDialog();
            sd.FileName = this.Project.Name + "." + FILE_EXT;
            sd.DefaultExt = FILE_EXT;
            sd.Filter = "*." + FILE_EXT + "|*." + FILE_EXT;
            DialogResult dr = sd.ShowDialog();

            if (dr != DialogResult.OK) return;

            this.ExportPML(sd.FileName);
        }

        #endregion

        #region INodeHandler 成員

        public TreeNode Node { get; private set; }

        public bool IsFirstClick { get; private set; }

        public void OnFirstClick() { }

        public void OnClick() { }

        #endregion

        #region IDeleteable 成員

        public void Delete()
        {
            DeleteProjectForm form = new DeleteProjectForm(this.Project);
            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowDialog();
        }

        public string TitleOfDelete
        {
            get { return "刪除專案「" + Project.Name + "」"; }
        }

        #endregion

        #region IReloadable 成員
        internal event EventHandler Reloaded;

        public void Reload()
        {
            if (Reloaded != null)
                Reloaded.Invoke(this, EventArgs.Empty);
        }

        #endregion

        #region IDeployable 成員

        public void Deploy()
        {
            foreach (ContractHandler contract in MainForm.CurrentUDS.Contracts)
            {
                if (contract.Ready) continue;

                MessageBox.Show("尚有 Contract 未準備完成, 請稍侯再試!", "資訊", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MainForm.LoginArgs.SucceedModuleLogin)
            {
                DeployForm df = new DeployForm(this);
                df.StartPosition = FormStartPosition.CenterParent;
                df.ShowDialog();
            }
            else
            {                
                MessageBox.Show("連線模組檔案管理失敗.", "資訊", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        #endregion

        internal void ExportPML(string filename)
        {
            XmlHelper h = new XmlHelper("<Project/>");
            XmlElement e = h.GetElement(".");
            e.SetAttribute("Name", Project.Name);

            XmlElement udtElement = h.AddElement(".", "Property");
            udtElement.SetAttribute("Name", "UDT");
            XmlHelper udtHelper = new XmlHelper(udtElement);
            foreach (UDTTable table in MainForm.CurrentUDT.Tables)
            {
                udtHelper.AddElement(".", table.GetContent());
            }

            XmlElement udsElement = h.AddElement(".", "Property");
            udsElement.SetAttribute("Name", "UDS");
            XmlHelper udsHelper = new XmlHelper(udsElement);
            foreach (ContractHandler contract in MainForm.CurrentUDS.Contracts)
            {
                udsHelper.AddElement(".", contract.GetXml());
            }

            e.OwnerDocument.Save(filename);
        }
    }
}
