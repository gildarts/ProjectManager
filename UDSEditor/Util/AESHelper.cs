using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace ProjectManager.Util
{
    public class AESHelper
    {
        public static byte[] Encrypt(byte[] bytes, string password)
        {
            AesManaged am = new AesManaged();
            am.Mode = CipherMode.ECB;
            am.Key = ProcessKey(password);

            ICryptoTransform AESEncrypt = am.CreateEncryptor();                        
            return AESEncrypt.TransformFinalBlock(bytes, 0, bytes.Length);
        }

        public static string Encrypt(string text, string password)
        {
            AesManaged am = new AesManaged();
            am.Mode = CipherMode.ECB;
            am.Key = ProcessKey(password);

            ICryptoTransform AESEncrypt = am.CreateEncryptor();

            byte[] Buffer = Encoding.UTF8.GetBytes(text);
            return Convert.ToBase64String(AESEncrypt.TransformFinalBlock(Buffer, 0, Buffer.Length));
        }

        public static byte[] Decrypt(byte[] bytes, string password)
        {
            AesManaged AES = new AesManaged();

            AES.Key = ProcessKey(password);
            AES.Mode = CipherMode.ECB;

            ICryptoTransform AESDecrypt = AES.CreateDecryptor();
            return AESDecrypt.TransformFinalBlock(bytes, 0, bytes.Length);
        }

        public static string Decrypt(string text, string password)
        {
            byte[] Buffer = Convert.FromBase64String(text);
            Buffer = Decrypt(Buffer, password);
            return UTF8Encoding.UTF8.GetString(Buffer);
        }

        private static byte[] ProcessKey(string key)
        {
            if (string.IsNullOrEmpty(key))
                key = "";

            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = UTF8Encoding.UTF8.GetBytes(key);
            byte[] targetData = md5.ComputeHash(fromData);
            return targetData;
            //key = Convert.ToBase64String(targetData);
            //return UTF8Encoding.UTF8.GetBytes(key);
        }
    }
}
