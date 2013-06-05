using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProjectManager.Project;
using System.Xml;
using FISCA.DSAClient;
using System.Globalization;
using ProjectManager.Login.Security;
using ProjectManager.ActionHandler.Files;
using System.IO;
using ProjectManager.Util;

namespace ProjectManager.Project.Proxy
{
    public partial class ProxyDeployForm : Form
    {
        private ProjectHandler _project;

        internal ProxyDeployForm(ProjectHandler project)
        {
            InitializeComponent();
            _project = project;
        }

        private void ProxyDeployForm_Load(object sender, EventArgs e)
        {
            txtProjectName.Text = _project.Name;
            XmlElement pref = _project.Preference;

            dgSites.Rows.Clear();
            foreach (XmlElement siteElement in pref.SelectNodes("Property[@Name='ProxyDeploy']/ProxySite"))
            {
                AddSiteRow(siteElement);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            EditProxyDeployForm form = new EditProxyDeployForm();
            form.Completed += delegate(object s, EventArgs arg)
            {
                XmlElement siteElement = form.GetResultXml();
                AddSiteRow(siteElement);
                SavePreference();
            };

            form.ShowDialog();
        }
        
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgSites.SelectedRows.Count == 0) return;

            DataGridViewRow row = dgSites.SelectedRows[0];
            XmlElement siteElement = row.Tag as XmlElement;

            EditProxyDeployForm form = new EditProxyDeployForm(siteElement);
            form.Completed += delegate(object s, EventArgs arg)
            {
                XmlElement result = form.GetResultXml();
                BindSiteToRow(row, result);
                SavePreference();
            };

            form.ShowDialog();
        }

        private void AddSiteRow(XmlElement siteElement)
        {
            int index = dgSites.Rows.Add();
            DataGridViewRow row = dgSites.Rows[index];
            BindSiteToRow(row, siteElement);           
        }

        private void BindSiteToRow(DataGridViewRow row, XmlElement siteElement)
        {
            XmlHelper h = new XmlHelper(siteElement);
            row.Cells[colSiteName.Name].Value = siteElement.GetAttribute("Name");
            row.Cells[colAccessPoint.Name].Value = h.GetText("AccessPoint");
            row.Tag = siteElement;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgSites.SelectedRows.Count == 0)
            {
                MessageBox.Show("請先選擇欲刪除的記錄", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("確定要該除所選資料?", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                return;

            List<DataGridViewRow> rows = new List<DataGridViewRow>();
            foreach(DataGridViewRow row in dgSites.SelectedRows)
                rows.Add(row);

            foreach (DataGridViewRow row in rows)
                dgSites.Rows.Remove(row);

            SavePreference();
        }

        private void SavePreference()
        {
            XmlElement pref = _project.Preference;
            foreach (XmlNode node in pref.SelectNodes("Property[@Name='ProxyDeploy']"))
            {
                pref.RemoveChild(node);
            }

            XmlHelper h = new XmlHelper(pref);
            XmlElement p = h.AddElement(".", "Property");
            p.SetAttribute("Name", "ProxyDeploy");

            XmlHelper ph = new XmlHelper(p);
            foreach(DataGridViewRow row in dgSites.Rows)
            {
                if (row.IsNewRow) continue;

                XmlElement t = row.Tag as XmlElement;
                if (t == null) continue;

                ph.AddElement(".", t);
            }
            
            _project.UpdateProjectPreference(pref);
        }

        private void btnDeploy_Click(object sender, EventArgs e)
        {
            FileNodeHandler fileNodeHandler = MainForm.CurrentFileNodeHandler;
            if (fileNodeHandler == null)
            {
                MessageBox.Show("模組站台登入失敗, 無法使用此功能","錯誤",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }

            string url = _project.DeployHttpURL;

            foreach (DataGridViewRow row in dgSites.Rows)
            {
                XmlElement x = row.Tag as XmlElement;
                DataGridViewCell cell = row.Cells[colStatus.Name] ;
                cell.Value = string.Empty;
                row.ErrorText = string.Empty;
                try
                {
                    Deploy(x, url);
                    cell.Value = "Done";
                }
                catch (Exception ex)
                {
                    row.ErrorText = ex.Message;
                    cell.Value = "Error";
                }
                Application.DoEvents();
            }
        }

        internal static Connection DeployConnect(XmlElement siteElement)
        {
            XmlHelper h = new XmlHelper(siteElement);
            string accesspoint = h.GetText("AccessPoint");
            string contract = h.GetText("Contract");
            string authType = h.GetText("Authentication/@Type").ToLower();
            string username = h.GetText("Authentication/UserName");
            string password = h.GetText("Authentication/Password");
            string issuer = h.GetText("Authentication/Issuer");

            string url = ConvertDSNS(accesspoint);
            
            Connection con = new Connection();
            con.EnableSecureTunnel = true;

            SecurityToken st;
            if (authType == "basic")
            {                
                st = new BasicSecurityToken(username, password);
            }
            else
            {
                Connection c = new Connection();
                c.EnableSecureTunnel = true;
                c.Connect(issuer, "", username, password);
                Envelope env = c.SendRequest("DS.Base.GetPassportToken", new Envelope());                
                st = new PassportSecurityToken(env.Body.XmlString);
            }

            con.Connect(url, contract, st);
            return con;
        }

        internal static void Deploy(XmlElement siteElement, string url)
        {
            Connection connection = DeployConnect(siteElement);
            Envelope rsp = connection.SendRequest("UDMService.ListModules", new Envelope());
            XmlHelper rspHelper = new XmlHelper(rsp.Body);

            string projectName = MainForm.CurrentProject.Name;
            bool contains = false;
            foreach (XmlElement nameElement in rspHelper.GetElements("Module/Name"))
            {
                string name = nameElement.InnerText;
                if (name == projectName)
                {
                    contains = true;
                    break;
                }
            }

            if (contains)
            {
                XmlHelper h = new XmlHelper("<Request/>");
                h.AddElement(".","ModuleName", projectName);
                connection.SendRequest("UDMService.UpdateModule", new Envelope(h));
                //rspHelper = new XmlHelper(rsp.Body);                
            }
            else
            {
                XmlHelper h = new XmlHelper("<Request/>");
                h.AddElement(".", "URL", url);
                
                connection.SendRequest("UDMService.ForceRegistry", new Envelope(h));
            }
        }

        internal static string ConvertDSNS(string name)
        {
            if (name.StartsWith("http://", true, CultureInfo.CurrentCulture))
                return name;

            if (name.StartsWith("https://", true, CultureInfo.CurrentCulture))
                return name;

            Connection con = new Connection();
            con.EnableSession = false;
            con.EnableSecureTunnel = false;
            con.Connect("http://dsns1.ischool.com.tw/dsns/", "dsns", new PublicSecurityToken());

            XmlHelper h = new XmlHelper("<a>" + name + "</a>");
            Envelope env = con.SendRequest("DS.NameService.GetDoorwayURL", new Envelope(h));
            h = new XmlHelper(env.Body);
            string value = h.GetText(".");
            if (string.IsNullOrWhiteSpace(value))
                throw new Exception("DSNS 名稱解析失敗");
            return value;
        }
    }
}
