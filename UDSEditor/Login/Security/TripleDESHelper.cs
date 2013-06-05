using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace ProjectManager.Login.Security
{
    public class TripleDESHelper
    {
        public static string Decrypt(string strString, string key)
        {
            TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider();

            DES.Key = ProcessKey(key);
            DES.Mode = CipherMode.ECB;
            ICryptoTransform DESDecrypt = DES.CreateDecryptor();

            byte[] Buffer = Convert.FromBase64String(strString);
            return UTF8Encoding.UTF8.GetString(DESDecrypt.TransformFinalBlock(Buffer, 0, Buffer.Length));
        }

        private string Encrypt(string strString, string key)
        {
            TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider();
            DES.Key = ProcessKey(key);

            DES.Mode = CipherMode.ECB;

            ICryptoTransform DESEncrypt = DES.CreateEncryptor();

            byte[] Buffer = Encoding.UTF8.GetBytes(strString);
            return Convert.ToBase64String(DESEncrypt.TransformFinalBlock(Buffer, 0, Buffer.Length));
        }

        private static byte[] ProcessKey(string key)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = UTF8Encoding.UTF8.GetBytes(key);
            byte[] targetData = md5.ComputeHash(fromData);
            key = Convert.ToBase64String(targetData);
            return UTF8Encoding.UTF8.GetBytes(key);
        }
    }
}
