using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectManager.ActionHandler.Files
{
    class FileEditable : AbstractUIEditable
    {
        private FileNodeHandler _fileNodeHandler;
        public FileEditable(FileNodeHandler fileNodeHandler)
        {
            _fileNodeHandler = fileNodeHandler;
        }

        public override bool Valid
        {
            get { return true; }
        }
       
        public override void Save()
        {
            
        }

        protected override void OnInitialEditor()
        {
            _editorInstance = CreateEditor();
        }

        private System.Windows.Forms.Control CreateEditor()
        {
            // 還沒設定本機目錄就顯示設定畫面，不建立檔案管理畫面
            if (_fileNodeHandler.ModuleHandler == null)
                return new LocalPathSetupPanel(_fileNodeHandler);
            return new FileUIEditor(_fileNodeHandler);
        }

        /// <summary>
        /// 檔案管理狀態變了（設定好目錄、重新整理）時更新右邊畫面。還沒開過畫面就等開啟時再建立。
        /// </summary>
        internal void Refresh()
        {
            if (_editorInstance == null) return;

            FileUIEditor ui = _editorInstance as FileUIEditor;
            if (ui != null && _fileNodeHandler.ModuleHandler != null)
            {
                ui.Reload();
                return;
            }
            OnEditorChanged(CreateEditor());
        }

        public override string DocumentTitle
        {
            get
            {
                return "檔案管理(&F)";
            }
        }
    }
}
