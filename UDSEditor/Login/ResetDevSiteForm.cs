using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using FISCA.DSAClient;

namespace ProjectManager.Login
{
    public partial class ResetDevSiteForm : Form
    {
        public event EventHandler Completed;

        private DevSiteLoginInfo _devSite;

        private string _projectName;

        /// <summary>儲存設定時已連好的連線</summary>
        internal Connection Connection { get; private set; }

        internal ResetDevSiteForm(string projectName, DevSiteLoginInfo devSite)
        {            
            InitializeComponent();
            _devSite = devSite;
            _projectName = projectName;
        }

        private void ResetDevSiteForm_Load(object sender, EventArgs e)
        {
            txtAP.Text = _devSite.AccessPoint;
            txtPassword.Text = _devSite.Password;
            txtUser.Text = _devSite.User;
            cboContract.Text = _devSite.ContractName;
            txtProjectName.Text = _projectName;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            err.Clear();

            DevSiteLoginInfo dev = GetDevSite();

            // 連線在背景跑，避免站台連不到時畫面 hang 住；取消就回到本畫面
            Task<Connection> connect = Task.Run(() => dev.TryConnect());
            ConnectingForm waitForm = new ConnectingForm("正在連線開發站台...", connect);
            if (waitForm.ShowDialog(this) != DialogResult.OK)
                return;

            try
            {
                Connection = connect.Result;
            }
            catch (AggregateException aex)
            {
                Exception ex = aex.InnerException ?? aex;
                StringBuilder sb = new StringBuilder(ex.Message);
                if (ex.InnerException != null)
                {
                    sb.Append("\n").Append(ex.InnerException.StackTrace);
                }

                err.SetError(txtProjectName, sb.ToString());
                return;
            }

            if (Completed != null)
                Completed.Invoke(this, EventArgs.Empty);

            this.Close();
        }

        internal DevSiteLoginInfo GetDevSite()
        {
            DevSiteLoginInfo dev = new DevSiteLoginInfo();
            dev.AccessPoint = txtAP.Text;
            dev.User = txtUser.Text;
            dev.ContractName = cboContract.Text;
            dev.Password = txtPassword.Text;
            return dev;
        }
    }
}
