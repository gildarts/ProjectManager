using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectManager.ActionHandler.Files
{
    public class Modifier
    {
        public ActionType ActionMode { get; set; }
        public FileType FileMode { get; set; }
        public string Path { get; set; }
        public string URL { get; set; }
    }

    public enum FileType { Directory, File }
    public enum ActionType { SendDelete, UpdateFile, DownloadFile }
}
