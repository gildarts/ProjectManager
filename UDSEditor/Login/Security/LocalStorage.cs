using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.DSAClient;
using System.IO;
using System.Security.Cryptography;
using System.Xml;

namespace ModuleFileManager.Utils
{
    class LocalStorage
    {
        private readonly byte[] EncryptoKey;
        private readonly string LocalPath;

        private XmlHelper _storage;

        public LocalStorage()
        {
            LocalPath = Path.Combine(Environment.CurrentDirectory, "lucifer.config");
            EncryptoKey = Encoding.UTF8.GetBytes("lucifer");
        }

        private XmlHelper Storage
        {
            get
            {
                if (_storage != null)
                    return _storage;

                string text = "<Storage/>";
                if (File.Exists(LocalPath))
                {
                    try
                    {
                        text = File.ReadAllText(LocalPath);
                        byte[] bs = Convert.FromBase64String(text);
                        byte[] decoded = ProtectedData.Unprotect(bs, EncryptoKey, DataProtectionScope.CurrentUser);
                        text = Encoding.UTF8.GetString(decoded);
                    }
                    catch
                    {
                        text = "<Storage/>";
                    }
                }
                _storage = new XmlHelper(text);
                return _storage;
            }
        }

        internal string GetProperty(string propertyName)
        {
            XmlHelper h = this.Storage;
            return h.GetText("Property[@Name='" + propertyName + "']");
        }

        internal void SetProperty(string propertyName, string value)
        {
            if (value == null)
                value = string.Empty;

            XmlHelper h = this.Storage;
            XmlElement element = h.GetElement("Property[@Name='" + propertyName + "']");
            if (element == null)
            {
                element = h.AddElement(".", "Property", value);
                element.SetAttribute("Name", propertyName);
            }
            else
            {
                element.InnerText = value;
            }
            byte[] bs = Encoding.UTF8.GetBytes(h.XmlString);
            bs = ProtectedData.Protect(bs, EncryptoKey, DataProtectionScope.CurrentUser);
            string text = Convert.ToBase64String(bs);

            File.WriteAllText(LocalPath, text);
        }

        internal void SetPropertyValues(string propertyName, string attr, string value)
        {
            if (value == null)
                value = string.Empty;

            XmlHelper h = this.Storage;
            XmlElement element = h.GetElement("Property[@Name='" + propertyName + "']");
            if (element == null)
            {
                element = h.AddElement(".", "Property");
                element.SetAttribute("Name", propertyName);
            }

            XmlHelper vh = new XmlHelper(element);
            XmlElement targetElement = null;
            foreach (XmlElement ve in vh.GetElements("Value"))
            {
                if (ve.GetAttribute("Name") == attr)
                {
                    targetElement = ve;
                    break;
                }
            }

            if (targetElement == null)
            {
                targetElement = vh.AddElement(".", "Value");
                targetElement.SetAttribute("Name", attr);
            }

            targetElement.InnerText = value;


            byte[] bs = Encoding.UTF8.GetBytes(h.XmlString);
            bs = ProtectedData.Protect(bs, EncryptoKey, DataProtectionScope.CurrentUser);
            string text = Convert.ToBase64String(bs);

            File.WriteAllText(LocalPath, text);
        }

        internal string[] GetPropertyValueNames(string propertyName)
        {
            XmlHelper h = this.Storage;
            List<string> values = new List<string>();
            foreach (XmlElement e in h.GetElements("Property[@Name='" + propertyName + "']/Value"))
            {
                values.Add(e.GetAttribute("Name"));
            }
            return values.ToArray();
        }

        internal string GetPropertyValue(string propertyName, string attr)
        {
            XmlHelper h = this.Storage;
            XmlElement e = h.GetElement("Property[@Name='" + propertyName + "']/Value[@Name='" + attr + "']");
            if (e != null)
                return e.InnerText;
            return string.Empty;
        }

        internal XmlHelper GetPropertyXml(string propertyName, string attr)
        {
            XmlHelper h = this.Storage;
            XmlElement e = h.GetElement("Property[@Name='" + propertyName + "']/Value[@Name='" + attr + "']");
            if (e != null)
            {
                foreach (XmlNode node in e.ChildNodes)
                {
                    if (node is XmlElement)
                    {
                        XmlElement sub = node as XmlElement;
                        return new XmlHelper(sub);
                    }
                }
            }
            return null;
        }

        internal void SetPropertyXml(string propertyName, string attr, XmlElement content)
        {
            XmlHelper h = this.Storage;
            XmlElement element = h.GetElement("Property[@Name='" + propertyName + "']");
            if (element == null)
            {
                element = h.AddElement(".", "Property");
                element.SetAttribute("Name", propertyName);
            }

            XmlHelper vh = new XmlHelper(element);
            XmlElement targetElement = null;
            foreach (XmlElement ve in vh.GetElements("Value"))
            {
                if (ve.GetAttribute("Name") == attr)
                {
                    targetElement = ve;
                    break;
                }
            }

            if (targetElement == null)
            {
                targetElement = vh.AddElement(".", "Value");
                targetElement.SetAttribute("Name", attr);
            }
            else
            {
                targetElement.InnerXml = string.Empty;
            }

            if (content.OwnerDocument != targetElement.OwnerDocument)
                content = targetElement.OwnerDocument.ImportNode(content, true) as XmlElement;

            targetElement.AppendChild(content);

            byte[] bs = Encoding.UTF8.GetBytes(h.XmlString);
            bs = ProtectedData.Protect(bs, EncryptoKey, DataProtectionScope.CurrentUser);
            string text = Convert.ToBase64String(bs);

            File.WriteAllText(LocalPath, text);
        }
    }
}
