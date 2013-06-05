using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using FISCA.DSAClient;
using System.Xml;
using System.IO;

namespace ProjectManager.ActionHandler.UDT
{
    public partial class UDTTestForm : Form
    {
        public UDTTestForm()
        {
            InitializeComponent();
        }

        private void UDTTestForm_Load(object sender, EventArgs e)
        {
            txtSQL.Text = MainForm.Storage.GetProperty("UDTSQL");
            txtSQL.Document.LoadLanguageFromXml(Assembly.GetExecutingAssembly().GetManifestResourceStream("ProjectManager.ActiproSoftware.SQL.xml"), 0);
        }

        private void tsbtnExecute_Click(object sender, EventArgs e)
        {
            ExecuteSQL();
        }

        private void txtSQL_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                ExecuteSQL();
            }
        }

        private void ExecuteSQL()
        {
            string text = txtSQL.Text;
            if (!string.IsNullOrWhiteSpace(txtSQL.SelectedView.SelectedText))
                text = txtSQL.SelectedView.SelectedText;

            text = text.Trim();
            string[] sqls = text.Split(";\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            if (sqls.Length == 0) return;

            List<string> commands = new List<string>();
            List<string> querys = new List<string>();
            string lastSQL = sqls[sqls.Length - 1];

            foreach (string sql in sqls)
            {
                if (sql.StartsWith("select", StringComparison.CurrentCultureIgnoreCase))
                    querys.Add(sql);
                else
                    commands.Add(sql);
            }

            StringBuilder sb = new StringBuilder();
            Envelope env = null;
            bool showSelect = false;
            bool occurError = false;
            long t1 = System.Environment.TickCount;

            if (commands.Count > 0)
            {
                sb.Append("執行 ").Append(commands.Count).Append(" Command\n");

                XmlHelper req = new XmlHelper();
                foreach (string sql in commands)
                {
                    XmlElement xml = req.AddElement(".", "Command");
                    XmlCDataSection section = xml.OwnerDocument.CreateCDataSection(sql);
                    xml.AppendChild(section);
                }
                try
                {
                    env = MainForm.CurrentProject.SendRequest("UDTService.DML.Command", new Envelope(req));
                }
                catch (Exception ex)
                {
                    sb.Append("執行 Commands 時發生錯誤.\n").Append(ex.Message).Append("\n");
                    occurError = true;
                }
            }

            if (lastSQL.StartsWith("select", StringComparison.CurrentCultureIgnoreCase))
            {
                sb.Append("執行 SQL Query : ").Append(lastSQL).Append("\n");

                XmlHelper req = new XmlHelper();
                XmlElement xml = req.AddElement("SQL");
                XmlCDataSection section = xml.OwnerDocument.CreateCDataSection(lastSQL);
                xml.AppendChild(section);

                try
                {
                    env = MainForm.CurrentProject.SendRequest("UDTService.DML.Query", new Envelope(req));
                }
                catch (Exception ex)
                {
                    sb.Append("執行 Query 時發生錯誤.\n").Append(ex.Message).Append("\n");
                    occurError = true;
                }
                showSelect = true;
            }
            long t2 = System.Environment.TickCount;

            tsLabel.Text = "執行時間 : " + (t2 - t1) + "ms";

            dgResult.Rows.Clear();
            dgResult.Columns.Clear();

            if (!occurError)
            {
                XmlHelper rsp = new XmlHelper(env.Body);
                if (showSelect)
                {
                    foreach (XmlElement col in rsp.GetElements("Metadata/Column"))
                    {
                        dgResult.Columns.Add("col" + col.GetAttribute("Index"), col.GetAttribute("Field"));
                    }

                    foreach (XmlElement record in rsp.GetElements("Record"))
                    {
                        int rowIndex = dgResult.Rows.Add();
                        DataGridViewRow row = dgResult.Rows[rowIndex];

                        XmlHelper h = new XmlHelper(record);
                        foreach (XmlElement col in h.GetElements("Column"))
                        {
                            string columnName = "col" + col.GetAttribute("Index");
                            string value = col.InnerText;
                            row.Cells[columnName].Value = value;
                        }
                    }
                }
                else
                {
                    dgResult.Columns.Add("colResult", "Result");
                    dgResult.Rows.Add();
                    dgResult.Rows[0].Cells["colResult"].Value = rsp.TryGetInteger("Update", 0);
                }
            }
            txtInfo.Text = sb.ToString();

            if (occurError)
            {
                tabControl1.SelectedTab = tpInfo;
            }
            else
            {
                tabControl1.SelectedTab = tpResult;
            }
        }

        private void UDTTestForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainForm.Storage.SetProperty("UDTSQL", txtSQL.Text);
        }

        private void tsbtnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.AddExtension = true;
            sfd.DefaultExt = "sql";
            sfd.Filter = "*.sql|*.sql";
            sfd.FileName = "MyCommand.sql";

            DialogResult dr = sfd.ShowDialog();

            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                if (File.Exists(sfd.FileName))
                {
                    File.Delete(sfd.FileName);
                }
                using (StreamWriter sw = File.CreateText(sfd.FileName))
                {                    
                    sw.Write(txtSQL.Text);
                    sw.Close();
                }
                MessageBox.Show("儲存完成","完成",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }

        private void rsbtnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog sfd = new OpenFileDialog();
            sfd.AddExtension = true;
            sfd.DefaultExt = "sql";
            sfd.Filter = "*.sql|*.sql";

            DialogResult dr = sfd.ShowDialog();

            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                StringBuilder sb = new StringBuilder();
                using (StreamReader sr = new StreamReader(sfd.FileName))
                {
                    string line;                    
                    while ((line = sr.ReadLine()) != null)
                    {
                        sb.Append(line).Append("\r\n");
                    }
                }
                txtSQL.Text = sb.ToString();
            }
        }
        
        private void cmTextMode_Click(object sender, EventArgs e)
        {
            OpenView(_rigthSelectedText, ViewMode.Text);
        }

        private void cmXmlMode_Click(object sender, EventArgs e)
        {
            OpenView(_rigthSelectedText, ViewMode.XML);
        }

        private void OpenView(string text, ViewMode mode)
        {
            TextViewForm form = new TextViewForm(text, mode);
            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowDialog();
        }

        private string _rigthSelectedText = string.Empty;

        private void cmRight_Opening(object sender, CancelEventArgs e)
        {
            Point p = dgResult.PointToClient(Control.MousePosition);

            DataGridView.HitTestInfo info = dgResult.HitTest(p.X, p.Y);
            if (info.ColumnIndex == -1 || info.RowIndex == -1)
            {
                e.Cancel = true;
                return;
            }
            object textObject = dgResult.Rows[info.RowIndex].Cells[info.ColumnIndex].Value;
            _rigthSelectedText = textObject + string.Empty;
        }
    }
}
