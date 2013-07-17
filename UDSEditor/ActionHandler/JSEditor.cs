using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using ActiproSoftware.SyntaxEditor;
using JSBeautifyLib;
using System.Diagnostics;
using System.Text;

namespace ProjectManager.ActionHandler
{
    public partial class JSEditor : UserControl
    {
        private string _sourceString = string.Empty;

        private bool _changed = false;

        public JSEditor()
        {
            InitializeComponent();
        }

        private void XmlEditor_Load(object sender, EventArgs e)
        {
            try
            {
                string langname = "ProjectManager.SyntaxLanguage.ActiproSoftware.JScript.xml";
                Stream lang = Assembly.GetExecutingAssembly().GetManifestResourceStream(langname);
                jsEditor1.Document.LoadLanguageFromXml(lang, 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void jsEditor1_KeyTyped(object sender, KeyTypedEventArgs e)
        {
            if (e.KeyData == (Keys.Shift | Keys.Control | Keys.F))
            {
                JSBeautify js = new JSBeautify(jsEditor1.Text, new JSBeautifyOptions());
                jsEditor1.Text = js.GetResult();
            }
        }

        public string JavaScriptCode
        {
            get { return jsEditor1.Text; }
            set
            {
                _sourceString = value;
                jsEditor1.Text = value;
                _changed = false;
            }
        }

        private void jsEditor1_TextChanged(object sender, EventArgs e)
        {
            CheckChanged();
        }

        private void CheckChanged()
        {
            if (this.jsEditor1.Text != _sourceString && DataChanged != null)
            {
                _changed = true;
                DataChanged.Invoke(this, EventArgs.Empty);
            }
            else if (_changed && this.jsEditor1.Text == _sourceString && ChangeRecovered != null)
            {
                _changed = false;
                ChangeRecovered.Invoke(this, EventArgs.Empty);
            }
        }

        public event EventHandler DataChanged;

        public event EventHandler ChangeRecovered;

        private void btnExtEditor_Click(object sender, EventArgs e)
        {
            try
            {
                string sublime = GetExecutablePath();

                if (string.IsNullOrWhiteSpace(sublime))
                    return;

                string path = Application.StartupPath;
                string file = Path.Combine(path, "service.js");

                File.WriteAllText(file, jsEditor1.Text, Encoding.UTF8);

                ProcessStartInfo psi = new ProcessStartInfo(sublime);
                psi.Arguments = "-w \"" + file + "\"";

                Process pro = new Process();
                pro.StartInfo = psi;

                pro.Start();

                pro.WaitForExit();
                jsEditor1.Text = File.ReadAllText(file, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }

        private string GetExecutablePath()
        {
            string path = Application.UserAppDataRegistry.GetValue("Sublime", @"C:\Program Files\Sublime Text 2\sublime_text.exe").ToString();

            if (!File.Exists(path))
            {
                MessageBox.Show("請選擇 Sublime 的執行檔。");
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "*.exe|*.exe";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    Application.UserAppDataRegistry.SetValue("Sublime", dialog.FileName);
                    return dialog.FileName;
                }
                else
                    return string.Empty;
            }

            return path;
        }

        public void Locked()
        {
            Enabled = false;
        }

        public void Unlocked()
        {
            Enabled = true;
        }
    }
}
