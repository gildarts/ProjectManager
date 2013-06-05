using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProjectManager.ActionHandler
{
    abstract class AbstractUIEditable : IEditable
    {
        public event EventHandler EditorChanged;
        public string DocumentName { get; protected set; }

        #region IEditable 成員
        public bool IsEditing { get; set; }

        public virtual string ModeTitle
        {
            get { return "UI 編輯"; }
        }

        public virtual string ImageKey
        {
            get { return "edit"; }
        }

        public virtual string DocumentTitle
        {
            get { return "UI編輯 「" + DocumentName + "」(&U)"; }
        }

        protected Control _editorInstance;

        public Control Editor
        {
            get
            {
                if (_editorInstance == null)
                    OnInitialEditor();
                return _editorInstance;
            }
        }

        protected abstract void OnInitialEditor();

        public event EventHandler DataChanged;

        public event EventHandler ChangeRecovered;
        
        public abstract bool Valid { get; }

        public abstract void Save();

        public virtual event EventHandler EditorReloaded;
        #endregion

        protected virtual void OnChangeRecovered()
        {
            if (ChangeRecovered != null)
                ChangeRecovered.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnDataChange()
        {
            if (DataChanged != null)
                DataChanged.Invoke(this, EventArgs.Empty);
        }

        protected void OnEditorReloaded()
        {
            if (this.IsEditing)
            {
                DialogResult dr = MessageBox.Show("「" + this.DocumentName + "」 資料來源已重新整理, 是否重新載入編輯畫面?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.No) return;
            }
            OnInitialEditor();

            if (EditorReloaded != null)
                EditorReloaded(this, EventArgs.Empty);
        }

        public virtual void OnStartEditing()
        {
        }

        protected virtual void OnEditorChanged(Control editor)
        {
            _editorInstance = editor;
            if (EditorChanged != null)
                EditorChanged.Invoke(this, EventArgs.Empty);
        }
    }
}
