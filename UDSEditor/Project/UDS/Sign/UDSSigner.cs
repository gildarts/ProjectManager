using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web.Script.Serialization;
using System.Xml;

namespace ProjectManager.Project.UDS.Sign
{
    /// <summary>
    /// 簽章失敗（連不上金鑰中心、內容被拒絕、使用者取消設定 key 等）。
    /// </summary>
    class UDSSignException : Exception
    {
        public UDSSignException(string message) : base(message) { }
    }

    /// <summary>
    /// key 本身有問題（無效、過期、權限不對），重新設定 key 之後可以再試。
    /// </summary>
    class UDSSignKeyException : UDSSignException
    {
        public UDSSignKeyException(string message) : base(message) { }
    }

    /// <summary>
    /// 把 UDS Service 的 definition 送到金鑰中心代簽。
    /// UDS 是強制驗章，沒簽章的 service 存上去不能執行，所以儲存前一律先簽。
    /// </summary>
    static class UDSSigner
    {
        internal const string KeyhubUrl = "https://dsa-keyhub.1campus.net";

        private const string StorageName = "UDSSignKey";

        /// <summary>
        /// UDS 簽章 key（ks_ 開頭）。存在本機 lucifer.config，由 LocalStorage 以 Windows 使用者身分加密。
        /// </summary>
        internal static string Key
        {
            get
            {
                string key = MainForm.Storage.GetProperty(StorageName);
                return key == null ? string.Empty : key.Trim();
            }
            set
            {
                MainForm.Storage.SetProperty(StorageName, value == null ? string.Empty : value.Trim());
            }
        }

        /// <summary>
        /// 簽章並回傳已插好簽章的 definition。沒設定 key、key 無效或過期時會跳出設定畫面，
        /// 設定好自動重試；使用者取消則丟出 UDSSignException。
        /// 必須在 UI 執行緒呼叫（可能會顯示設定畫面）。
        /// </summary>
        internal static XmlElement Sign(string contractName, string packageName, string serviceName, XmlElement definition)
        {
            string prompt = null;

            while (true)
            {
                string key = Key;

                if (key.Length == 0 || prompt != null)
                {
                    if (prompt == null)
                        prompt = "儲存 UDS Service 前需要先設定簽章 key。";

                    if (!UDSSignKeyForm.Prompt(prompt))
                        throw new UDSSignException("尚未設定有效的 UDS 簽章 key，已取消儲存。");

                    prompt = null;
                    continue;
                }

                try
                {
                    return SignWithKey(key, contractName, packageName, serviceName, definition);
                }
                catch (UDSSignKeyException ex)
                {
                    prompt = ex.Message;
                }
            }
        }

        private static XmlElement SignWithKey(string key, string contractName, string packageName, string serviceName, XmlElement definition)
        {
            Dictionary<string, object> body = new Dictionary<string, object>();
            body["contract"] = contractName;
            body["package"] = packageName;
            body["service"] = serviceName;
            body["definition"] = definition.OuterXml;

            Dictionary<string, object> rsp = Post(key, "/api/uds/sign", body);

            string signed = GetString(rsp, "definition");
            if (string.IsNullOrEmpty(signed))
                throw new UDSSignException("金鑰中心沒有回傳簽章後的內容。");

            try
            {
                // 回傳的內容要原樣部署，保留空白避免動到內容
                XmlDocument doc = new XmlDocument();
                doc.PreserveWhitespace = true;
                doc.LoadXml(signed);
                return doc.DocumentElement;
            }
            catch (XmlException ex)
            {
                throw new UDSSignException("金鑰中心回傳的內容不是合法的 XML：" + ex.Message);
            }
        }

        /// <summary>
        /// 確認 key 可用；不可用時丟出 UDSSignKeyException（key 的問題）或 UDSSignException（其他）。
        /// 用「只正規化、不簽章」的端點測試，不會留下簽章紀錄。
        /// </summary>
        internal static void TestKey(string key)
        {
            Dictionary<string, object> body = new Dictionary<string, object>();
            body["definition"] = "<Definition Type=\"JavaScript\"><Code>return {};</Code></Definition>";

            Post(key, "/api/uds/canonicalize", body);
        }

        private static Dictionary<string, object> Post(string key, string path, Dictionary<string, object> body)
        {
            JavaScriptSerializer json = new JavaScriptSerializer();
            json.MaxJsonLength = int.MaxValue;

            byte[] payload = Encoding.UTF8.GetBytes(json.Serialize(body));

            ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(KeyhubUrl + path);
            req.Method = "POST";
            req.ContentType = "application/json; charset=utf-8";
            req.Accept = "application/json";
            req.Timeout = 30000;
            req.Headers["Authorization"] = "Bearer " + key;
            // Program.cs 全域關掉了憑證驗證；這裡會送出簽章 key，一定要驗憑證
            req.ServerCertificateValidationCallback = delegate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
            {
                return errors == SslPolicyErrors.None;
            };

            string text;
            try
            {
                req.ContentLength = payload.Length;
                using (Stream s = req.GetRequestStream())
                    s.Write(payload, 0, payload.Length);

                using (HttpWebResponse rsp = (HttpWebResponse)req.GetResponse())
                    text = ReadAll(rsp);
            }
            catch (WebException ex)
            {
                HttpWebResponse rsp = ex.Response as HttpWebResponse;
                if (rsp == null)
                    throw new UDSSignException("無法連線到金鑰中心：" + ex.Message);

                int status = (int)rsp.StatusCode;
                string error = string.Empty;
                string message = string.Empty;
                try
                {
                    Dictionary<string, object> err = json.Deserialize<Dictionary<string, object>>(ReadAll(rsp));
                    error = GetString(err, "error");
                    message = GetString(err, "message");
                }
                catch { }
                finally
                {
                    rsp.Close();
                }

                if (status == 401)
                {
                    if (error == "key_expired")
                        throw new UDSSignKeyException("UDS 簽章 key 已過期，請到金鑰中心重新產生後貼上。");
                    throw new UDSSignKeyException("UDS 簽章 key 無效，請重新設定。");
                }

                if (status == 403 && error == "key_scope")
                    throw new UDSSignKeyException("這把 key 不能用來簽 UDS，請改用「UDS 簽章 key」（ks_ 開頭）。");

                if (string.IsNullOrEmpty(message))
                    message = ex.Message;
                throw new UDSSignException("金鑰中心拒絕這次簽章（" + status + "）：" + message);
            }

            try
            {
                return json.Deserialize<Dictionary<string, object>>(text);
            }
            catch (Exception ex)
            {
                throw new UDSSignException("看不懂金鑰中心的回應：" + ex.Message);
            }
        }

        private static string ReadAll(HttpWebResponse rsp)
        {
            using (Stream s = rsp.GetResponseStream())
            using (StreamReader reader = new StreamReader(s, Encoding.UTF8))
                return reader.ReadToEnd();
        }

        private static string GetString(Dictionary<string, object> values, string name)
        {
            object value;
            if (values != null && values.TryGetValue(name, out value) && value != null)
                return value.ToString();
            return string.Empty;
        }
    }
}
