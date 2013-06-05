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
using FISCA.Deployment;

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
            Version serverVer = new Version(1, 0, 0, 0);
            ManifestResolver r = new ManifestResolver(Program.UpdateURL, VersionOption.Stable);
            try
            {
                r.Resolve();
                serverVer = r.ReleaseManifest.Version;
            }
            catch
            {
            }

            lblLastest.Text = "最新版本 : " + serverVer;

            Version localVersion = new Version("1.0.0.0");
            try
            {
                ReleaseManifest rm = new ReleaseManifest();
                rm.Load(Path.Combine(Application.StartupPath, "release.xml"));
                localVersion = rm.Version;
            }
            catch
            {
                localVersion = MainForm.Version;
            }
            lblCurrent.Text = "目前版本 : " + localVersion.ToString();
        }
    }
}
