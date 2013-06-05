using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;

namespace ProjectManager.ActionHandler
{
    abstract class AbstractXmlEditable : IEditable
    {
        public string DocumentName { get; protected set; }

        protected XmlEditor _xmlEditor;

        protected XmlElement Source { set; get; }

        #region IEditable 成員

        public bool IsEditing { get; set; }

        public virtual string ModeTitle
        {
            get { return "XML 編輯"; }
        }

        public virtual string ImageKey
        {
            get { return "xml"; }
        }

        public virtual string DocumentTitle
        {
            get { return "XML 編輯 「" + DocumentName + "」(&X)"; }
        }

        public virtual System.Windows.Forms.Control Editor
        {
            get
            {
                if (_xmlEditor == null)
                {
                    OnInitialEditor(Source);
                }
                return _xmlEditor;
            }
        }

        protected virtual void OnInitialEditor(XmlElement source)
        {
            Source = source;
            _xmlEditor = new XmlEditor(Source);
            _xmlEditor.DataChanged += new EventHandler(_xmlEditor_DataChanged);
            _xmlEditor.ChangeRecovered += new EventHandler(_xmlEditor_ChangeRecovered);
        }

        void _xmlEditor_ChangeRecovered(object sender, EventArgs e)
        {
            OnChangeRecovered();
        }

        void _xmlEditor_DataChanged(object sender, EventArgs e)
        {
            OnDataChanged();
        }

        public virtual event EventHandler DataChanged;

        public virtual event EventHandler ChangeRecovered;

        public virtual bool Valid
        {
            get
            {
                _xmlEditor.ClearError();
                try { XmlElement e = this._xmlEditor.Xml; }
                catch (Exception ex)
                {
                    _xmlEditor.SetError(ex.Message);
                    return false;                    
                }
                return true;
            }
        }

        public abstract void Save();

        public virtual event EventHandler EditorReloaded;


        protected void OnEditorReloaded(XmlElement source)
        {
            if (this.IsEditing)
            {
                DialogResult dr = MessageBox.Show("「" + this.DocumentName + "」 資料來源已重新整理, 是否重新載入 XML 編輯畫面?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.No) return;
            }
            OnInitialEditor(source);

            if (EditorReloaded != null)
                EditorReloaded(this, EventArgs.Empty);
        }

        public virtual void OnStartEditing()
        {
        }
        #endregion

        protected virtual void OnChangeRecovered()
        {
            if (ChangeRecovered != null)
                ChangeRecovered.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnDataChanged()
        {
            if (DataChanged != null)
                DataChanged.Invoke(this, EventArgs.Empty);
        }

        protected void OnDataSaved()
        {
            OnChangeRecovered();
            this._xmlEditor.Reset();
        }

        #region IEditable 成員

        public event EventHandler EditorChanged;

        protected virtual void OnEditorChanged()
        {
            if (EditorChanged != null)
                EditorChanged.Invoke(this, EventArgs.Empty);
        }
        #endregion
    }
}
