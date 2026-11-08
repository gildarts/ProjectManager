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

namespace ProjectManager
{
    public partial class SetupConfigForm : Form
    {
        internal const string DefaultGreeningURL = "https://greening.ischool.com.tw/dsa/greening";
        internal const string DefaultModuleURL = "https://module.ischool.com.tw/is4/shared";

        private string _greeningURL;
        private string _moduleURL;

        /// <summary>設定值是空的就用預設值</summary>
        internal static string OrDefault(string value, string defaultValue)
        {
            return string.IsNullOrWhiteSpace(value) ? defaultValue : value.Trim();
        }

        internal event EventHandler SetupChanged;

        public SetupConfigForm()
        {
            InitializeComponent();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("確定要刪除本機暫存資訊?", "問題", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == System.Windows.Forms.DialogResult.Yes)
            {
                string path = Path.Combine(Environment.CurrentDirectory, "lucifer.config");
                if (File.Exists(path))
                    File.Delete(path);
                MessageBox.Show("刪除完畢", "完成", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void SetupConfigForm_Load(object sender, EventArgs e)
        {
            txtGreening.Text = DefaultGreeningURL;
            txtModule.Text = DefaultModuleURL;

            string path = Path.Combine(Environment.CurrentDirectory, "Setup.config");
            if (File.Exists(path))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                XmlHelper h = new XmlHelper(doc.DocumentElement);
                txtGreening.Text = OrDefault(h.GetText("GreeningAccessPoint"), DefaultGreeningURL);
                txtModule.Text = OrDefault(h.GetText("ModuleAccessPoint"), DefaultModuleURL);
            }

            _greeningURL = txtGreening.Text;
            _moduleURL = txtModule.Text;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtModule.Text == _moduleURL && txtGreening.Text == _greeningURL)
            {
                this.Close();
                return;
            }
            DialogResult dr = MessageBox.Show("設定已變更, 儲存後必須重新啟動程式以套用新設定。\n是否儲存?", "問題", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == System.Windows.Forms.DialogResult.Yes)
            {
                string path = Path.Combine(Environment.CurrentDirectory, "Setup.config");
                //if (File.Exists(path))
                //    File.Delete(path);

                XmlHelper h = new XmlHelper("<Setup/>");
                h.AddElement(".", "GreeningAccessPoint", txtGreening.Text);
                h.AddElement(".", "ModuleAccessPoint", txtModule.Text);
                h.GetElement(".").OwnerDocument.Save(path);

                if (SetupChanged != null)
                    SetupChanged.Invoke(this, e);
                else
                    MessageBox.Show("儲存完畢", "完成", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            this.Close();
        }
    }
}
