using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using ActiproSoftware.SyntaxEditor;
using JSBeautifyLib;
using System.Diagnostics;
using System.Text;
using ProjectManager.ActionHandler.UDS.Service;

namespace ProjectManager.ActionHandler
{
    internal partial class JSEditor : UserControl
    {
        private string _sourceString = string.Empty;

        private bool _changed = false;

        private ServiceJSEditable _editor_owner;
        private JavaScriptTranspilerHandler _transpilerHandler;

        private bool _isTypeScript = false;

        public JSEditor(ServiceJSEditable editorOwner)
        {
            InitializeComponent();
            _editor_owner = editorOwner;
            HideInof();
        }

        private void XmlEditor_Load(object sender, EventArgs e)
        {
            try
            {
                string langname = "ProjectManager.SyntaxLanguage.ActiproSoftware.JScript.xml";
                Stream lang = Assembly.GetExecutingAssembly().GetManifestResourceStream(langname);
                jsEditor1.Document.LoadLanguageFromXml(lang, 0);

                Unlocked();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool IsTypeScript()
        {
            return _editor_owner.Source.SelectSingleNode(JavaScriptTranspilerHandler.TSXPath) != null;
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

        // 不在使用，有新的可以用。
        //private void btnExtEditor_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string vscode = GetExecutablePath();

        //        if (string.IsNullOrWhiteSpace(vscode))
        //            return;

        //        string path = Path.Combine(Application.StartupPath, "vscode");
        //        string file = Path.Combine(path, "service.js");

        //        if (!Directory.Exists(path)) Directory.CreateDirectory(path);

        //        File.WriteAllText(file, jsEditor1.Text, Encoding.UTF8);

        //        ProcessStartInfo psi = new ProcessStartInfo(vscode);
        //        psi.CreateNoWindow = true;
        //        psi.WindowStyle = ProcessWindowStyle.Hidden;
        //        psi.Arguments = $"-w \"{path}\"";

        //        Process pro = new Process();
        //        pro.StartInfo = psi;

        //        pro.Start();
        //        pro.WaitForExit();

        //        if (pro.ExitCode == 0)
        //        {
        //            jsEditor1.Text = File.ReadAllText(file, Encoding.UTF8);
        //            _editor_owner.Save();
        //            //lblMsg.Visible = true;
        //        }
        //        else
        //            MessageBox.Show("外部編輯器錯誤。");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        throw;
        //    }
        //}

        private void btnJS_Click(object sender, EventArgs e)
        {
            try
            {
                if (MainForm.VSCodeInstalled)
                    StartScriptEditor("JavaScript");
                else
                {
                    var msg = "您必須安裝 VSCode 才可使用此功能。";
                    MessageBox.Show(msg);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"編輯器錯誤：{ex.Message}");
            }
        }

        private void btnTypeScript_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_isTypeScript)
                {
                    var msg = "這將使用目前程式碼直接轉為 TypeScript，確定要使用 TypeScript？";
                    DialogResult dr = MessageBox.Show(msg, "使用 TypeScript？", MessageBoxButtons.OKCancel);
                    if (dr == DialogResult.Cancel)
                        return;
                }

                if (MainForm.VSCodeInstalled && MainForm.TSCInstalled)
                {
                    StartScriptEditor("TypeScript");
                    _isTypeScript = true;
                }
                else
                {
                    var msg = "您必須安裝 VSCode、TypeScript 才可使用此功能。";
                    MessageBox.Show(msg);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"編輯器錯誤：{ex.Message}");
            }
        }

        private void StartScriptEditor(string transpiler = "JavaScript")
        {
            HideInof();
            Locked();

            _transpilerHandler = new JavaScriptTranspilerHandler(
                _editor_owner.ServiceNodeHandler,
                _editor_owner.Source,
                transpiler == "TypeScript");

            _transpilerHandler.SourceUpdate += delegate
            {
                try
                {
                    ShowInfo("資料儲存中...");
                    Application.DoEvents();
                    jsEditor1.Text = _transpilerHandler.GetJavaScript();
                    // Source 可能會整個被調整過。
                    _editor_owner.Source = _transpilerHandler._source;
                    // Save 會把 jsEditor1.Text 寫入 _editor_owner.Soure 屬性。
                    // 這是原來的邏輯，未修改，需要呼叫是因為要儲存到 Server。
                    _editor_owner.Save();
                    ShowInfo("儲存完成...");
                    lblMessage.Visible = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("error: " + ex.Message);
                }
            };
            _transpilerHandler.StartEditor();
            btnStopSyncSave.Enabled = true;
        }

        private string GetExecutablePath()
        {
            string path = Application.UserAppDataRegistry.GetValue("vscode", @"code").ToString();

            return path;
        }

        public void Locked()
        {
            if (_editor_owner.Source != null && IsTypeScript())
                _isTypeScript = true;

            btnTypeScript.Enabled = false;
            btnJS.Enabled = false;

            jsEditor1.Document.ReadOnly = true;
        }

        public void Unlocked()
        {
            if (_editor_owner.Source != null && IsTypeScript())
                _isTypeScript = true;

            btnTypeScript.Enabled = true;
            btnJS.Enabled = true;

            if (_isTypeScript)
            {
                btnJS.Enabled = false;
                ShowInfo("注意！已經使用 TypeScript ，請使用 VSCode 編輯程式碼。");
            }
            else
            {
                HideInof();
            }

            // 如果是 TypeScript 不允許直接修改。
            jsEditor1.Document.ReadOnly = _isTypeScript;
        }

        private void ShowInfo(string msg)
        {
            lblMessage.Text = msg;
            lblMessage.Visible = true;
        }

        private void HideInof()
        {
            lblMessage.Visible = false;
        }

        private void btnStopSyncSave_Click(object sender, EventArgs e)
        {
            if (_transpilerHandler != null)
            {
                _transpilerHandler.DisconnectEditor();
                _transpilerHandler = null;
                Unlocked();
            }

            btnStopSyncSave.Enabled = _transpilerHandler != null;
        }
    }
}
