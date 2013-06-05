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
            _editorInstance = new FileUIEditor(_fileNodeHandler);
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
