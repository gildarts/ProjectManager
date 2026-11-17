using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.DSAClient;
using System.Xml;
using ProjectManager.Project.UDS.Service;
using ProjectManager.Project.UDS.Sign;

namespace ProjectManager.ActionHandler.UDS.Service.DAL
{
    class ServiceDAL
    {
        /// <summary>
        /// Service 的簽章狀態改變後觸發，參數為 Contract、Package、Service 名稱與是否已簽章，樹上的「未簽章」標示據此更新。
        /// </summary>
        internal static event Action<string, string, string, bool> SignStateChanged;

        private static void OnSignStateChanged(string contractName, string packageName, string serviceName, bool signed)
        {
            if (SignStateChanged != null)
                SignStateChanged(contractName, packageName, serviceName, signed);
        }

        private const string SIGNATURE_PREFIX = "keyhub-sig:";

        /// <summary>
        /// 回傳拿掉簽章註解的複本（不動到傳進來的 XML）。
        /// 儲存、匯入、改名都不簽章：開發中的內容不能帶著舊簽章上線，簽章一律由使用者手動確認後才做。
        /// </summary>
        private static XmlElement StripSignature(XmlElement definition, out bool hadSignature)
        {
            hadSignature = false;
            XmlElement copy = (XmlElement)definition.CloneNode(true);
            List<XmlNode> remove = new List<XmlNode>();
            foreach (XmlNode child in copy.ChildNodes)
            {
                XmlComment comment = child as XmlComment;
                if (comment != null && comment.Value.Trim().StartsWith(SIGNATURE_PREFIX))
                    remove.Add(child);
            }
            foreach (XmlNode node in remove)
                copy.RemoveChild(node);
            hadSignature = remove.Count > 0;

            // 簽章註解插在 Definition 最前面並帶著換行；只拿掉註解會留下空白，
            // 每存／簽一次就多一行。把開頭的純空白節點一併清掉（也順便清掉以前累積的）。
            while (copy.FirstChild != null && IsBlank(copy.FirstChild))
                copy.RemoveChild(copy.FirstChild);
            return copy;
        }

        private static bool IsBlank(XmlNode node)
        {
            if (node is XmlWhitespace || node is XmlSignificantWhitespace) return true;
            XmlText text = node as XmlText;
            return text != null && text.Value.Trim().Length == 0;
        }

        public static void Rename(string contractName, string packageName, string serviceName, string newName)
        {
            XmlHelper req = new XmlHelper();
            req.AddElement(".", "ContractName", contractName);
            req.AddElement(".", "PackageName", packageName);
            req.AddElement(".", "ServiceName", serviceName);
            req.AddElement(".", "NewServiceName", newName);

            MainForm.CurrentProject.SendRequest("UDSManagerService.RenameService", new Envelope(req));

            // 簽章綁了 service 名稱，改名後舊簽章就失效了；拿掉它，等使用者確認後再手動簽
            XmlElement definition = GetServiceDefinition(contractName, packageName, newName);
            if (definition != null)
            {
                bool hadSignature;
                XmlElement stripped = StripSignature(definition, out hadSignature);
                if (hadSignature)
                    SendDefinition(contractName, packageName, newName, stripped);
            }
            OnSignStateChanged(contractName, packageName, newName, false);
        }

        public static XmlElement GetRawService(string contractName, string packageName, string serviceName)
        {
            XmlHelper req = new XmlHelper();
            req.AddElement(".", "ContractName", contractName);
            req.AddElement(".", "PackageName", packageName);
            req.AddElement(".", "ServiceName", serviceName);

            Envelope env = MainForm.CurrentProject.SendRequest("UDSManagerService.GetServiceDefinition", new Envelope(req));
            XmlHelper rsp = new XmlHelper(env.Body);
            return rsp.GetElement(".");
        }

        public static XmlElement GetServiceDefinition(string contractName, string packageName, string serviceName)
        {
            XmlElement raw = GetRawService(contractName, packageName, serviceName);
            XmlHelper h = new XmlHelper(raw);
            return h.GetElement("Definition");
        }

        /// <summary>
        /// 儲存 service 定義，不簽章；原本帶的簽章會拿掉（內容可能改過，舊簽章不該跟著上線）。
        /// 要簽章請用 Sign，由使用者確認後手動執行。
        /// </summary>
        public static void SetDefinition(string contractName, string packageName, string serviceName, XmlElement definition)
        {
            bool hadSignature;
            XmlElement stripped = StripSignature(definition, out hadSignature);
            SendDefinition(contractName, packageName, serviceName, stripped);
            OnSignStateChanged(contractName, packageName, serviceName, false);
        }

        /// <summary>
        /// 把開發站台上目前的定義送金鑰中心簽章後存回去，內容不做任何修改。
        /// 簽不成會丟出例外、不會存。必須在 UI 執行緒呼叫（可能會顯示 key 設定畫面）。
        /// </summary>
        public static void Sign(string contractName, string packageName, string serviceName)
        {
            XmlElement definition = GetServiceDefinition(contractName, packageName, serviceName);
            if (definition == null)
                throw new Exception("讀不到這個 Service 的定義。");

            bool hadSignature;
            XmlElement stripped = StripSignature(definition, out hadSignature);
            XmlElement signed = UDSSigner.Sign(contractName, packageName, serviceName, stripped);
            SendDefinition(contractName, packageName, serviceName, signed);
            OnSignStateChanged(contractName, packageName, serviceName, true);
        }

        private static void SendDefinition(string contractName, string packageName, string serviceName, XmlElement definition)
        {
            XmlHelper req = new XmlHelper();
            req.AddElement(".", "ContractName", contractName);
            req.AddElement(".", "PackageName", packageName);
            req.AddElement(".", "ServiceName", serviceName);
            req.AddElement(".", definition);

            MainForm.CurrentProject.SendRequest("UDSManagerService.SetServiceDefinition", new Envelope(req));
        }

        public static void AddService(string contractName, string packageName, ServiceEntity service)
        {
            XmlHelper req = new XmlHelper();
            req.AddElement(".", "ContractName", contractName);
            req.AddElement(".", "PackageName", packageName);

            XmlElement serviceXml = service.GetServiceXml();
            XmlElement definition = serviceXml.SelectSingleNode("Definition") as XmlElement;
            if (definition != null)
            {
                // 新增的 service 不簽章（可能是匯入、帶著別處的簽章），一律拿掉，等使用者確認後再手動簽
                bool hadSignature;
                XmlElement stripped = StripSignature(definition, out hadSignature);
                serviceXml.ReplaceChild(stripped, definition);
            }
            req.AddElement(".", serviceXml);

            MainForm.CurrentProject.SendRequest("UDSManagerService.CreateService", new Envelope(req));

        }
    }
}
