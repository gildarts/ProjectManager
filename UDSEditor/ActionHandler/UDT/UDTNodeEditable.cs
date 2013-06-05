using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectManager.ActionHandler.UDS.Contract;
using System.Windows.Forms;

namespace ProjectManager.ActionHandler.UDT
{
    class UDTNodeEditable : IEditable
    {
        private UDTNodeHandler _udtNodeHandler;
        internal UDTNodeEditable(UDTNodeHandler udtNodeHandler)
        {
            _udtNodeHandler = udtNodeHandler;
        }

        void edit_ChangeRecovered(object sender, EventArgs e)
        {
            if (ChangeRecovered != null)
                ChangeRecovered.Invoke(this, e);
        }

        void edit_DataChanged(object sender, EventArgs e)
        {
            if (DataChanged != null)
                DataChanged.Invoke(this, e);
        }

        #region IEditable 成員

        public bool IsEditing { get; set; }

        public string ModeTitle
        {
            get { return "專案 Table 管理(&E)"; }
        }

        public string ImageKey
        {
            get { return "edit"; }
        }

        public string DocumentTitle
        {
            get { return "專案 Table 管理"; }
        }

        private UDTTableEditor _editor;
        public System.Windows.Forms.Control Editor
        {
            get
            {
                if (_editor == null)
                {
                    _editor = new UDTTableEditor();
                    _editor.LoadData(_udtNodeHandler);
                    _editor.DataChanged += new EventHandler(edit_DataChanged);
                    _editor.ChangeRecovered += new EventHandler(edit_ChangeRecovered);
                }
                return _editor;
            }
        }

        public event EventHandler DataChanged;

        public event EventHandler ChangeRecovered;

        public event EventHandler SaveCompleted;

        public bool Valid
        {
            get { return true; }
        }

        public void Save()
        {
            IContractEditor edit = this.Editor as IContractEditor;
            edit.Save();

            if (SaveCompleted != null)
                SaveCompleted.Invoke(this, EventArgs.Empty);
        }

        public void LoadData(object source)
        {
            UDTNodeHandler s = source as UDTNodeHandler;
            _editor.LoadData(s);
        }
        #endregion

        #region IEditable 成員


        public event EventHandler EditorReloaded;

        protected void OnEditorReloaded()
        {
            if (EditorReloaded != null)
                EditorReloaded(this, EventArgs.Empty);
        }

        public void OnStartEditing()
        {
        }
        #endregion

        #region IEditable 成員

        public event EventHandler EditorChanged;

        protected virtual void OnEditorChanged()
        {
            if (EditorChanged != null)
                EditorChanged(this, EventArgs.Empty);
        }
        #endregion
    }
}
