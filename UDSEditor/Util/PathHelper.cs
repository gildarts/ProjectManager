using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using FISCA.DSAClient;
using System.Xml;
using ProjectManager.ActionHandler.Files;

namespace ProjectManager.Util
{
    class PathHelper
    {
        /// <summary>
        /// 取得指定檔案在本機中傳送的目標絕對路徑, 用於拖曳資料夾中取出指定檔案的目標路徑
        /// 如模組在本機路徑指定為 C:/module1
        /// 目標節點為 folder1
        /// 欲傳送檔案資料夾為 D:/drag_folder1, 其中含有子檔案 D:/drag_folder1/sub_folder1/file1.txt
        /// 則結果會傳回 C:/module1/folder1/drag_folder1/sub_folder1/file1.txt
        /// </summary>
        /// <param name="localFullPath">模組在本機中的絕對路徑</param>
        /// <param name="targetPath">傳送目標的節點路徑</param>
        /// <param name="fromDir">傳送檔案之指定來源資料夾</param>
        /// <param name="fromFile">傳送檔案之指定子檔案</param>
        /// <returns></returns>
        internal static string GetLocalFileFullName(string localFullPath, string targetPath, DirectoryInfo fromDir, FileInfo fromFile)
        {
            string path = fromFile.FullName.Replace(fromDir.FullName, "");
            path = path.Substring(1);
            path = Path.Combine(localFullPath, targetPath, fromDir.Name, path);
            return path;
        }

        /// <summary>
        /// 取得指定檔案在本機中傳送的目標絕對路徑, 用於拖曳資料夾中取出指定子資料夾的目標路徑
        /// 如模組在本機路徑指定為 C:/module1
        /// 目標節點為 folder1
        /// 欲傳送檔案資料夾為 D:/drag_folder1, 其中含有子資料夾 D:/drag_folder1/sub_folder1
        /// 則結果會傳回 C:/module1/folder1/drag_folder1/sub_folder1
        /// </summary>
        /// <param name="localFullPath">模組在本機中的絕對路徑</param>
        /// <param name="targetPath">傳送目標的節點路徑</param>
        /// <param name="fromDir">傳送檔案之指定來源資料夾</param>
        /// <param name="subDir">傳送檔案之指定資料夾</param>
        /// <returns></returns>
        internal static string GetLocalFileFullName(string localFullPath, string targetPath, DirectoryInfo fromDir, DirectoryInfo subDir)
        {
            string path = subDir.FullName.Replace(fromDir.FullName, "");
            path = path.Substring(1);
            path = Path.Combine(localFullPath, targetPath, fromDir.Name, path);
            return path;
        }

        /// <summary>
        /// 從指定節點中取回該節點在模組中的資料夾路徑
        /// </summary>
        /// <param name="currentNode">指定節點</param>
        /// <returns>短路徑, 如 path1/path2/</returns>
        internal static string GetNodeDirectory(TreeNode currentNode)
        {
            if (currentNode == currentNode.TreeView.Nodes[0])
                return string.Empty;

            if (currentNode.ImageIndex != 0 && currentNode.ImageIndex != 1) //大於1表示不是資料夾            
                currentNode = currentNode.Parent;

            string path = string.Empty;
            TreeNode rootNode = currentNode.TreeView.Nodes[0];

            while (currentNode != rootNode)
            {
                path = currentNode.Text + "/" + path;
                currentNode = currentNode.Parent;
            }
            return path;
        }

        /// <summary>
        /// 從指定節點中取回該節點在模組中的短路徑
        /// </summary>
        /// <param name="currentNode">指定節點</param>
        /// <returns>短路徑, 如 path1/path2/abc.txt</returns>
        internal static string GetNodeFullName(TreeNode currentNode)
        {
            string dir = GetNodeDirectory(currentNode);
            if (currentNode.ImageIndex == 0 || currentNode.ImageIndex == 1) //大於1表示不是資料夾            
                return dir;

            return dir + currentNode.Text;
        }

        /// <summary>
        /// 取得上傳至 FTP Server 的 ftp 路徑
        /// </summary>
        /// <param name="moduleRootDir">模組於本機之路徑</param>
        /// <param name="path"></param>
        /// <returns></returns>
        internal static string GetServerPath(string moduleRootDir, string path)
        {
            DirectoryInfo dir = new DirectoryInfo(moduleRootDir);
            path = path.Replace(dir.FullName, "");
            if (path.StartsWith("\\"))
                path = path.Substring(1);
            return path;
        }

        internal static string GetServerPath(ModuleHandler handler, XmlElement element)
        {
            XmlHelper h = handler.Source;
            string path = element.GetAttribute("Name");

            XmlElement xml = h.GetElement("Directory");
            XmlElement currentXml = element.ParentNode as XmlElement;

            while (xml != currentXml)
            {
                path = currentXml.GetAttribute("Name") + "/" + path;
                currentXml = currentXml.ParentNode as XmlElement;
            }

            return path;
        }

        internal static string GetFtpPath(string ftpURL, string username, string moduleName)
        {
            if (!ftpURL.EndsWith("/"))
                ftpURL += "/";
            ftpURL += username + "/" + moduleName;
            return ftpURL;
        }

        internal static string CombineTargetPath(string targetPath, string filename)
        {
            return Path.Combine(targetPath, filename).Replace(@"\", "/");
        }

        internal static string CombineLocalPath(string localModulePath, string nodePath)
        {
            if (nodePath.StartsWith("/"))
                nodePath = nodePath.Substring(1);
            string path = Path.Combine(localModulePath, nodePath);
            return path.Replace("/", @"\");
        }

        internal static void PrepareDirectory(string path)
        {
            if (Directory.Exists(path)) return;
            Directory.CreateDirectory(path);
        }

        internal static string CombineURL(string url, params string[] other)
        {
            if (url.EndsWith("/"))
                url = url.Substring(1);
            foreach (string o in other)
            {
                string oname = o;
                if (o.StartsWith("/") || o.StartsWith("\\"))
                    oname = o.Substring(1);
                url += "/" + oname;
            }
            return url;
        }

        internal static string GetHttpFilePath(XmlHelper moduleHelper, XmlElement fileElement)
        {
            string http = moduleHelper.GetText("HTTP");
            XmlElement rootDir = moduleHelper.GetElement("Directory");
            string filename = fileElement.GetAttribute("Name");

            while (!Object.Equals(fileElement.ParentNode, rootDir))
            {
                XmlElement parentElement = fileElement.ParentNode as XmlElement;
                filename = parentElement.GetAttribute("Name") + "/" + filename;
                fileElement = parentElement;
            }

            filename = CombineURL(http, filename);
            return filename;
        }
    }
}
