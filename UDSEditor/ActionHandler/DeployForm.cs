using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using FISCA.DSAClient;
using ProjectManager.ActionHandler.Files;
using ProjectManager.Project.UDS.Contract;
using ProjectManager.Util;
using ProjectManager.ActionHandler.UDT.Command;
using ProjectManager.Project.UDT;

namespace ProjectManager.ActionHandler
{
    public partial class DeployForm : Form
    {
        private ProjectNodeHandler _projectNodeHandler;
        private FileNodeHandler _fileNodeHandler;

        internal DeployForm(ProjectNodeHandler projectNodeHandler)
        {
            InitializeComponent();
            _projectNodeHandler = projectNodeHandler;

            foreach (TreeNode node in _projectNodeHandler.Node.Nodes)
            {
                FileNodeHandler fh = node.Tag as FileNodeHandler;
                if (fh == null) continue;
                _fileNodeHandler = fh;
                break;
            }
        }
        
        private void DeployForm_Load(object sender, EventArgs e)
        {
            txtProvider.Text = MainForm.LoginArgs.LoginUser;
            dgCommand.Rows.Clear();

            //找看看有沒有前一版
            string localPath =  MainForm.CurrentProject.TryGetLocalPath();
            string path = Path.Combine(localPath, "udm.xml");
            string vstr = string.Empty;

            if (File.Exists(path))
            {
                rbExtend.Checked = true;
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                vstr = doc.DocumentElement.GetAttribute("Version");

                Version ver;
                if (!Version.TryParse(vstr, out ver))
                    ver = new Version();
                txtLastest.Text = ver.ToString();
                txtProvider.Text = doc.DocumentElement.GetAttribute("Provider");

                XmlHelper h = new XmlHelper(doc.DocumentElement);
                txtPreVerURL.Text = h.GetText("Release/@URL");
            }
            else
            {
                rbNew.Checked = true;
                txtLastest.Text = "無前版資訊";

                foreach (UDTTable table in MainForm.CurrentUDT.Tables)
                {
                    ImportTableCommand cmd = new ImportTableCommand();
                    XmlHelper h = new XmlHelper("<Command/>");
                    h.SetAttribute(".", "Type", cmd.Type);
                    h.AddElement(".", table.GetContent());

                    cmd.Result = h.GetElement(".");
                    this.AddCommand(cmd);
                }
            }            
        }

        private void btnDeploy_Click(object sender, EventArgs e)
        {
            err.Clear();
            bool valid = true;
            if (string.IsNullOrWhiteSpace(txtVersion.Text))
            {
                err.SetError(txtVersion, "版本不可空白");
                valid = false;
            }
            Version version;
            if (!Version.TryParse(txtVersion.Text, out version))
            {
                err.SetError(txtVersion, "無效的版本編號");
                valid = false;
            }

            if (!valid) return;

            string localPath = MainForm.CurrentProject.TryGetLocalPath();
            string path = Path.Combine(localPath, "udm.xml");
            string dirPath = Path.Combine(localPath, "udm");

            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);

            string vs = version.ToString().Replace(".","_");
            
            string filename = MainForm.CurrentProject.Name + "_v_" + vs + ".pml";
            string udtfname = "UDT_" + vs + ".tcmd";
            string targetFilename = Path.Combine(dirPath,filename);
            string udtFileName = Path.Combine(dirPath, udtfname);
            //string ufUrl = PathHelper.CombineURL(_fileNodeHandler.ModuleHandler.HttpURL, "udm", udtfname);
            string ufUrl = PathHelper.CombineURL("udm", udtfname);

            lblInfo.Text = "匯出 tcmd 檔 「" + filename + "」 中 .....";
            Application.DoEvents();
            this.GetCmdElement().OwnerDocument.Save(udtFileName);

            lblInfo.Text = "匯出 PML 檔 「" + filename + "」 中 .....";
            Application.DoEvents();
            // 這是舊版寫法, 新版的要另外寫
            //_projectNodeHandler.ExportPML(targetFilename);
            #region 產生 PML 檔 (新版)
            XmlHelper h = new XmlHelper("<Project/>");
            h.SetAttribute(".", "Name", MainForm.CurrentProject.Name);

            XmlElement udtElement = h.AddElement(".", "Property");
            udtElement.SetAttribute("Name", "UDT");

            if (rbExtend.Checked)
            {
                XmlDocument d = new XmlDocument();
                try
                {
                    string url = PathHelper.CombineURL(_fileNodeHandler.ModuleHandler.HttpURL, txtPreVerURL.Text);
                    //string path = Path.Combine(_fileNodeHandler.ModuleHandler.HttpURL, txtPreVerURL.Text);
                    d.Load(url);
                }
                catch (Exception ex)
                {
                    err.SetError(txtPreVerURL, "載入失敗 : " + ex.Message);
                    return;
                }
                
                XmlHelper h1 = new XmlHelper(d.DocumentElement);
                foreach (XmlElement preUDTElement in h1.GetElements("Property[@Name='UDT']/Release"))
                {
                    h.AddElement("Property[@Name='UDT']",preUDTElement);                    
                }                
            }

            XmlElement ne = h.AddElement("Property[@Name='UDT']", "Release");
            ne.SetAttribute("Version", version.ToString());
            ne.SetAttribute("URL", ufUrl);

            XmlElement udsElement = h.AddElement(".", "Property");
            udsElement.SetAttribute("Name", "UDS");
            XmlHelper udsHelper = new XmlHelper(udsElement);
            foreach (ContractHandler contract in MainForm.CurrentUDS.Contracts)
            {
                udsHelper.AddElement(".", contract.GetXml());
            }

            h.WriteTo(targetFilename);
            #endregion


            lblInfo.Text = "產生 UDM 檔案中......";
            Application.DoEvents();

            h = new XmlHelper("<Module/>");
            h.SetAttribute(".", "Name", MainForm.CurrentProject.Name);
            h.SetAttribute(".", "Version", version.ToString());
            h.SetAttribute(".", "Provider", txtProvider.Text);
            XmlElement download = h.AddElement(".", "Release");

            //string downloadURL = PathHelper.CombineURL(_fileNodeHandler.ModuleHandler.HttpURL, "udm", filename);
            string downloadURL = PathHelper.CombineURL("udm", filename);
            download.SetAttribute("URL", downloadURL);
            
            h.GetElement(".").OwnerDocument.Save(path);

            lblInfo.Text = "佈署上傳中 ......";
            Application.DoEvents();

            _fileNodeHandler.Upload();

            lblInfo.Text = string.Empty;
            
            _projectNodeHandler.Project.DeployHttpURL = PathHelper.CombineURL(_fileNodeHandler.ModuleHandler.HttpURL,"udm.xml");
            
            Application.DoEvents();

            MessageBox.Show("佈署完成", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information); 

            this.Close();
        }

        #region add command
        private void btnAddCmd_Click(object sender, EventArgs e)
        {
            CmdEditorForm cmdForm = new CmdEditorForm();
            cmdForm.StartPosition = FormStartPosition.CenterParent;
            cmdForm.Completed += delegate(object s, EventArgs arg)
            {
                IUDTCommand cmd = cmdForm.Command;
                AddCommand(cmd);
            };
            cmdForm.ShowDialog();
        }

        private void AddCommand(IUDTCommand cmd)
        {
            int index = this.dgCommand.Rows.Add();
            DataGridViewRow row = dgCommand.Rows[index];
            row.Cells[colType.Name].Value = cmd.Type;
            row.Cells[colDesc.Name].Value = cmd.Description;
            row.Tag = cmd;
        }
        #endregion

        #region remove command
        private void btnDeleteCmd_Click(object sender, EventArgs e)
        {
            RemoveCommand();
        }

        private void dgCommand_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                RemoveCommand();
        }

        private void RemoveCommand()
        {
            foreach (DataGridViewRow row in dgCommand.SelectedRows)
                dgCommand.Rows.Remove(row);
        }
        #endregion

        private XmlElement GetCmdElement()
        {
            XmlHelper h = new XmlHelper("<UDT/>");

            foreach (DataGridViewRow row in dgCommand.Rows)
            {
                IUDTCommand cmd = row.Tag as IUDTCommand;
                if (cmd == null) continue;

                h.AddElement(".", cmd.Result);
            }

            return h.GetElement(".");
        }

        private void btnEditAll_Click(object sender, EventArgs e)
        {
            EditAllCmdForm form = new EditAllCmdForm(this.dgCommand);
            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowDialog();
        }

        private void btnMatch_Click(object sender, EventArgs e)
        {
            EditMatchForm form = new EditMatchForm(this.dgCommand);
            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowDialog();
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            if (dgCommand.SelectedRows.Count != 1)
            {
                MessageBox.Show("請選擇一筆資料", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataGridViewRow row = dgCommand.SelectedRows[0];
            int rowIndex = row.Index;
            if(rowIndex == 0) return;
                       
            dgCommand.Rows.Remove(row);
            dgCommand.Rows.Insert(rowIndex - 1, row);

            foreach (DataGridViewRow r in dgCommand.SelectedRows)
                r.Selected = false;
            row.Selected = true;
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            if (dgCommand.SelectedRows.Count != 1)
            {
                MessageBox.Show("請選擇一筆資料", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataGridViewRow row = dgCommand.SelectedRows[0];
            int rowIndex = row.Index;
            if (rowIndex == dgCommand.Rows.Count -1) return;

            dgCommand.Rows.Remove(row);
            dgCommand.Rows.Insert(rowIndex + 1, row);

            foreach (DataGridViewRow r in dgCommand.SelectedRows)
                r.Selected = false;
            row.Selected = true;
        }
    }
}
