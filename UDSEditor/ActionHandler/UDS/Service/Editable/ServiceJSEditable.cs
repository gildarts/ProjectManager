using System;
using System.Windows.Forms;
using System.Xml;
using ProjectManager.ActionHandler.UDS.Service.DAL;
using FISCA.DSAClient;
using System.Xml.Linq;

namespace ProjectManager.ActionHandler.UDS.Service
{
    class ServiceJSEditable : IEditable
    {
        internal ServiceNodeHandler ServiceNodeHandler { get; private set; }

        internal string ServiceName { get; set; }

        internal XmlElement Source { get; set; }

        public ServiceJSEditable(string srvName, ServiceNodeHandler serviceNode)
        {
            ServiceNodeHandler = serviceNode;
            ServiceName = srvName;
        }

        #region IEditable 成員

        public event EventHandler EditorChanged;

        public event EventHandler DataChanged;

        public event EventHandler ChangeRecovered;

        public event EventHandler EditorReloaded;

        public bool IsEditing { get; set; }

        public string ModeTitle
        {
            get { return "JavaScript 編輯"; }
        }

        public string ImageKey
        {
            get { return "jseditor"; }
        }

        public string DocumentTitle
        {
            get { return "JavaScript 編輯 「" + ServiceName + "」(&X)"; }
        }

        private JSEditor _js_editor = null;
        public Control Editor
        {
            get
            {
                if (_js_editor == null)
                {
                    _js_editor = new JSEditor();
                    _js_editor.DataChanged += new EventHandler(_js_editor_DataChanged);
                    _js_editor.ChangeRecovered += new EventHandler(_js_editor_ChangeRecovered);
                }

                return _js_editor;
            }
        }

        void _js_editor_ChangeRecovered(object sender, EventArgs e)
        {
            if (ChangeRecovered != null)
                ChangeRecovered(this, EventArgs.Empty);
        }

        void _js_editor_DataChanged(object sender, EventArgs e)
        {
            if (DataChanged != null)
                DataChanged(this, EventArgs.Empty);
        }

        public bool Valid
        {
            get { return true; }
        }

        public void OnStartEditing()
        {
            Source = ServiceDAL.GetServiceDefinition(
                ServiceNodeHandler.ContractName,
                ServiceNodeHandler.PackageName,
                ServiceNodeHandler.ServiceName);

            ((JSEditor)Editor).JavaScriptCode = Source.SelectSingleNode("Code").InnerText;

            if (ChangeRecovered != null)
                ChangeRecovered(this, EventArgs.Empty);
        }

        public void Save()
        {
            ServiceDAL.SetDefinition(
                this.ServiceNodeHandler.ContractName,
                this.ServiceNodeHandler.PackageName,
                this.ServiceNodeHandler.ServiceName, GenerateServiceDefinition());

            if (ChangeRecovered != null)
                ChangeRecovered(this, EventArgs.Empty);
        }

        private XmlElement GenerateServiceDefinition()
        {
            if (Source == null)
                Source = XmlHelper.ParseAsDOM("<Definition Type=\"JavaScript\"/>");

            if (Source.SelectSingleNode("Code") == null)
            {
                XmlHelper xh = new XmlHelper(Source);
                xh.AddElement("Code");
            }

            Source.SelectSingleNode("Code").InnerText = ((JSEditor)Editor).JavaScriptCode;

            return Source;
        }
        #endregion
    }
}
