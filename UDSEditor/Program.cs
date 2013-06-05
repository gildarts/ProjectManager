using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.IO;
using FISCA.Deployment;

namespace ProjectManager
{
    static class Program
    {
        public static string UpdateURL = "http://dl.dropbox.com/u/16912340/ProjectManager/app.xml";
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            {
                return true;
            };

            string script = Path.Combine(Application.StartupPath, "_update_padding.xml");
            if (File.Exists(script))
            {
                UpdateHelper.ExecuteScript(script, true);
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
