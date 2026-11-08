using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FISCA.DSAClient;
using System.Xml;
using System.IO;
using System.Diagnostics;
using System.Threading;
using ProjectManager.Downloader;

namespace ProjectManager
{
    public partial class CheckVersionForm : Form
    {
        public CheckVersionForm()
        {
            InitializeComponent();
        }

        private void CheckVersionForm_Load(object sender, EventArgs e)
        {
            // 已停用自動更新，只顯示目前版本
            lblLastest.Visible = false;
            lblCurrent.Text = "目前版本 : " + MainForm.Version.ToString();
        }
    }
}
