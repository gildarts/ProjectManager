using FISCA.DSAClient;
using ProjectManager.ActionHandler.UDS.Service;
using ProjectManager.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace ProjectManager.ActionHandler
{
    class JavaScriptTranspilerHandler
    {
        public const string TSXPath = "Resources/Resource[@Name = 'TypeScript']";

        private FileSystemWatcher _fs_js_watch = new FileSystemWatcher();
        private FileSystemWatcher _fs_rc_watch = new FileSystemWatcher();
        private AutoResetEvent _save_required = new AutoResetEvent(false);
        private CancellationTokenSource _auto_save_token = new CancellationTokenSource();

        private Process _ts_compiler;

        private string _working_dir;
        private string _rc_dir;
        private string _src_dir;
        private string _template_dir; // TypeScript 樣版目錄
        private string _js_name = "service.js";
        private string _ts_name = "service.ts";
        private string _previous_js_code = ""; // 用於判斷是否要存檔的內容。

        private bool _use_typescript = false;

        internal XmlElement _source { get; set; }

        private TaskScheduler _ui_context;

        public JavaScriptTranspilerHandler(ServiceNodeHandler srvNode,
            XmlElement source,
            bool useTypeScript)
        {
            _use_typescript = useTypeScript;
            _ui_context = TaskScheduler.FromCurrentSynchronizationContext();
            _source = source;
            EnsureWorkingFolder(srvNode); //確保工作目錄存在。
            PrepareWorkingFolder(); //準備工作目錄。
            CopySourceToWorkingFolder(); //複製程式碼到工作目錄。
            CreateFileSystemWatcher();  //建立檔案系統監控器。
        }

        internal void StartEditor()
        {
            ProcessStartInfo psi = new ProcessStartInfo("code");
            psi.CreateNoWindow = true;
            psi.WindowStyle = ProcessWindowStyle.Hidden;
            psi.Arguments = $"-n \"{_working_dir}\"";

            Process pro = new Process();
            pro.StartInfo = psi;
            pro.Start();

            if (_use_typescript)
            {
                Task.Factory.StartNew(() =>
                {
                    ProcessStartInfo ts_psi = new ProcessStartInfo("tsc");
                    ts_psi.CreateNoWindow = true;
                    ts_psi.WindowStyle = ProcessWindowStyle.Hidden;
                    ts_psi.Arguments = $"-w -p \"{Path.Combine(_working_dir, "tsconfig.json")}\"";

                    _ts_compiler = new Process();
                    _ts_compiler.StartInfo = ts_psi;
                    _ts_compiler.Start();
                    _ts_compiler.WaitForExit();

                    Console.WriteLine($"TSC Exit Code: {_ts_compiler.ExitCode}");
                });
            }
        }


        internal string GetJavaScript()
        {
            return JSCode;
        }

        internal string GetTypeScript()
        {
            return TSCode;
        }

        /// <summary>
        /// 當原始程式碼變更時。
        /// </summary>
        public event EventHandler SourceUpdate;

        private XmlElement Source
        {
            get
            {
                if (_source == null)
                    _source = XmlHelper.ParseAsDOM("<Definition Type=\"JavaScript\"/>");
                return _source;
            }
        }

        private string JSCode
        {
            get
            {
                EnsureJSNode();
                return GetCDATAText(_source.SelectSingleNode("Code"));
            }
            set
            {
                EnsureJSNode();
                var jsnode = _source.SelectSingleNode("Code");
                jsnode.InnerText = "";
                var cdata = jsnode.OwnerDocument.CreateCDataSection(value);
                jsnode.AppendChild(cdata);
            }
        }

        private void EnsureJSNode()
        {
            if (_source.SelectSingleNode("Code") == null)
            {
                var code = _source.OwnerDocument.CreateElement("Code");
                _source.AppendChild(code);
            }
        }

        private string TSCode
        {
            get
            {
                EnsureTSNode();
                string ts = GetCDATAText(_source.SelectSingleNode(TSXPath));

                // 如果沒有 TypeScript 程式碼就用 JavaScript 程式碼。
                // 免得被蓋掉。
                if (string.IsNullOrWhiteSpace(ts))
                    return JSCode;
                else
                    return ts;
            }
            set
            {
                EnsureTSNode();
                var tsnode = _source.SelectSingleNode(TSXPath);
                tsnode.InnerText = "";
                var cdata = tsnode.OwnerDocument.CreateCDataSection(value);
                tsnode.AppendChild(cdata);
            }
        }

        private void EnsureTSNode()
        {
            if (_source.SelectSingleNode("Resources") == null)
            {
                var rcs = _source.OwnerDocument.CreateElement("Resources");
                _source.AppendChild(rcs);
            }

            if (_source.SelectSingleNode(TSXPath) == null)
            {
                var tsrc = _source.OwnerDocument.CreateElement("Resource");
                tsrc.SetAttribute("Name", "TypeScript");
                _source.SelectSingleNode("Resources").AppendChild(tsrc);
            }
        }

        private void CreateFileSystemWatcher()
        {
            _fs_js_watch.Path = _src_dir;
            _fs_js_watch.Filter = "service.js";
            _fs_js_watch.NotifyFilter = NotifyFilters.LastWrite;
            _fs_js_watch.Changed += JS_Changed;
            _fs_js_watch.EnableRaisingEvents = true;

            _fs_rc_watch.Path = _rc_dir;
            _fs_rc_watch.Filter = "*.*";
            //_fs_rc_watch.NotifyFilter = NotifyFilters.LastWrite;
            _fs_rc_watch.Changed += JS_Changed;
            _fs_rc_watch.Deleted += JS_Changed;
            _fs_rc_watch.EnableRaisingEvents = true;

            Task.Factory.StartNew(() =>
            {
                while (!_auto_save_token.IsCancellationRequested)
                {
                    _save_required.WaitOne();
                    //如果取消代表不在自動儲存。
                    if (_auto_save_token.IsCancellationRequested) break;
                    // 讓執行緒停住一下，防止連續儲存。
                    Thread.Sleep(250);
                    _save_required.Reset(); //防止未儲存又放行，自已先行封鎖。

                    ReadSourceFromWorkingFolder();
                    RaiseUISourceUpdate();
                    _previous_js_code = JSCode;
                }
            }, _auto_save_token.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }

        private void JS_Changed(object sender, FileSystemEventArgs e)
        {
            _save_required.Set();
        }

        private void RaiseUISourceUpdate()
        {
            var task = new Task(() =>
            {
                SourceUpdate?.Invoke(this, EventArgs.Empty);
                _previous_js_code = JSCode;
            });
            task.RunSynchronously(_ui_context);
        }

        private void PrepareWorkingFolder()
        {
            if (Directory.Exists(_working_dir))
                Directory.Delete(_working_dir, true);

            Directory.CreateDirectory(_src_dir);
            Directory.CreateDirectory(_rc_dir);

            if (Directory.Exists(_template_dir))
                Folder.Copy(_template_dir, _working_dir);
        }

        private void CopySourceToWorkingFolder()
        {
            File.WriteAllText(Path.Combine(_src_dir, _js_name), JSCode, Encoding.UTF8);

            if (_use_typescript)
                File.WriteAllText(Path.Combine(_src_dir, _ts_name), TSCode, Encoding.UTF8);

            foreach (XmlNode rc in Source.SelectNodes("Resources/Resource"))
            {
                if (rc is XmlElement)
                {
                    string attValue = rc.Attributes["Name"].InnerText;

                    if (attValue == "TypeScript") continue; // TypeScript 不出輸。

                    string typeString = "";
                    var typeAtt = rc.Attributes["Type"]; //檔案類型。

                    if (typeAtt != null) typeString = typeAtt.Value.ToLower();

                    string name = EnsureValidName(attValue);

                    if (!string.IsNullOrWhiteSpace(typeString)) //有指定類型
                    {
                        string fn = $"{name}{typeString}";
                        string fileName = Path.Combine(_rc_dir, fn);
                        File.WriteAllText(fileName, GetCDATAText(rc));
                    }
                    else //沒有指定類型。
                    {
                        string fn = $"{name}.rc.xml";
                        string fileName = Path.Combine(_rc_dir, fn);
                        File.WriteAllText(fileName, rc.OuterXml);
                    }
                }
            }
        }

        private void ReadSourceFromWorkingFolder()
        {
            var jsfile = Path.Combine(_src_dir, _js_name);
            var tsfile = Path.Combine(_src_dir, _ts_name);

            if (File.Exists(jsfile)) JSCode = ReadAllTextWait(jsfile);
            if (File.Exists(tsfile)) TSCode = ReadAllTextWait(tsfile);

            foreach (XmlNode n in Source.SelectNodes("Resources/Resource"))
            {
                var name = n.Attributes["Name"].InnerText;
                if (name == "TypeScript") continue; //ts 的不要動，其他的都移掉。

                n.ParentNode.RemoveChild(n);
            }

            //*.rc.xml、*.txt、*.sql、*.js、*.json

            var rcs = Source.SelectSingleNode("Resources");
            foreach (var file in Directory.EnumerateFiles(_rc_dir, "*.*"))
            {
                try
                {
                    var fileInfo = new FileInfo(file);

                    if (fileInfo.Name.ToLower() == "README.TXT".ToLower()) continue;
                    
                    if(fileInfo.Name.EndsWith(".rc.xml"))
                    {
                        var xmldoc = new XmlDocument();
                        xmldoc.Load(file);

                        var newrc = rcs.OwnerDocument.ImportNode(xmldoc.DocumentElement, true);
                        rcs.AppendChild(newrc);
                    } else
                    {
                        var xmldoc = new XmlDocument();
                        xmldoc.LoadXml("<Resource/>");
                        xmldoc.DocumentElement.SetAttribute("Name", fileInfo.Name.Replace(fileInfo.Extension, ""));
                        xmldoc.DocumentElement.SetAttribute("Type", fileInfo.Extension);
                        var cdata = xmldoc.CreateCDataSection(File.ReadAllText(file));
                        xmldoc.DocumentElement.AppendChild(cdata);

                        var newrc = rcs.OwnerDocument.ImportNode(xmldoc.DocumentElement, true);
                        rcs.AppendChild(newrc);
                    }
                }
                catch { }
            }
        }

        internal void DisconnectEditor()
        {
            _fs_js_watch.EnableRaisingEvents = false;
            _fs_js_watch.Dispose();
            _fs_js_watch = null;

            _fs_rc_watch.EnableRaisingEvents = false;
            _fs_rc_watch.Dispose();
            _fs_rc_watch = null;

            if (_ts_compiler != null && !_ts_compiler.HasExited)
                _ts_compiler.Kill();

            _auto_save_token.Cancel(); //先取消。
            _save_required.Set(); //再放行。
        }

        private string ReadAllTextWait(string fileName)
        {
            for (int i = 1; i <= 30; i++)
            {
                try
                {
                    return File.ReadAllText(fileName);
                }
                catch
                {
                    Thread.Sleep(100);
                    Console.WriteLine($"Read {fileName} Error {i}");
                }
            }

            throw new FileLoadException("讀取檔案錯誤。", fileName);
        }

        private void EnsureWorkingFolder(ServiceNodeHandler srvNode)
        {
            string baseUrl = Path.Combine(Application.StartupPath, "typescript");
            var accessPoint = EnsureValidName(MainForm.CurrentProject.DevSite.AccessPoint);
            var contractName = EnsureValidName(srvNode.ContractName);
            var packageName = EnsureValidName(srvNode.PackageName);
            var serviceName = EnsureValidName(srvNode.ServiceName);

            _working_dir = Path.Combine(baseUrl, accessPoint, contractName, packageName, serviceName);
            _rc_dir = Path.Combine(_working_dir, "rc");
            _src_dir = Path.Combine(_working_dir, "src");
            _template_dir = Path.Combine(baseUrl, "template");

            if (!Directory.Exists(_working_dir))
                Directory.CreateDirectory(_working_dir);

            if (!Directory.Exists(_rc_dir))
                Directory.CreateDirectory(_rc_dir);

            if (!Directory.Exists(_src_dir))
                Directory.CreateDirectory(_src_dir);
        }

        private string EnsureValidName(string name)
        {
            string output = name;
            foreach (var c in Path.GetInvalidFileNameChars())
                output = output.Replace(c, '_');

            return output;
        }

        private string GetCDATAText(XmlNode node)
        {
            if (node is XmlElement)
            {
                foreach (XmlNode n in node.ChildNodes)
                {
                    if (n is XmlCDataSection)
                        return n.InnerText;
                }
            }

            return node.InnerText;
        }

        private bool IsSupportExt(string extension)
        {
            var supports = new List<string>(new string[] { ".sql", ".txt", ".json", ".js", ".pgsql" });
            return supports.FindIndex((v) => v == extension) >= 0;
        }
    }
}
