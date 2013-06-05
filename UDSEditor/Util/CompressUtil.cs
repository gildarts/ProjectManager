using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Compression;

namespace ProjectManager.Util
{
    class CompressUtil
    {
        public static byte[] CompressString(string value, string encodeKey)
        {
            byte[] compressedBuffer = CompressString(value);
            return AESHelper.Encrypt(compressedBuffer, encodeKey);
        }

        public static byte[] CompressString(string value)
        {
            byte[] b = Encoding.UTF8.GetBytes(value);
            return Compress(b);
        }

        public static byte[] Compress(byte[] bytes)
        {
            MemoryStream stream = new MemoryStream();        
            using (GZipStream zip = new GZipStream(stream,CompressionMode.Compress))
            {
                zip.Write(bytes, 0, bytes.Length);             
            }
            return stream.ToArray();            
        }

        public static void CompressStringToFile(string fileName, string value)
        {
            byte[] b = CompressString(value);
            File.WriteAllBytes(fileName, b);
        }

        public static void CompressStringToFile(string fileName, string value, string encodeKey)
        {
            byte[] b = CompressString(value, encodeKey);
            File.WriteAllBytes(fileName, b);
        }

        public static byte[] Decompress(byte[] bytes)
        {
            MemoryStream stream = new MemoryStream(bytes);
            MemoryStream s2 = new MemoryStream();
            int count = 0;
            int bufferSize = 4096;
            byte[] buffer = new byte[bufferSize];
            using (GZipStream zip = new GZipStream(stream, CompressionMode.Decompress,true))
            {
                while (true)
                {
                    count = zip.Read(buffer, 0, bufferSize);
                    if (count != 0)
                        s2.Write(buffer, 0, count);

                    if (count != bufferSize)            
                        break;
                }
            }          
            return s2.ToArray();
        }

        public static byte[] Decompress(byte[] bytes, string encodeKey)
        {
            bytes = AESHelper.Decrypt(bytes, encodeKey);
            return Decompress(bytes);
        }

        public static string DecompressString(string value)
        {
            byte[] b = Encoding.UTF8.GetBytes(value);
            return Encoding.UTF8.GetString(Decompress(b));
        }

        public static string DecompressString(string value, string encodeKey)
        {
            byte[] b = Encoding.UTF8.GetBytes(value);
            return Encoding.UTF8.GetString(Decompress(b, encodeKey));
        }

        public static string DecompressStringFromFile(string filename, string encodeKey)
        {
            byte[] b = File.ReadAllBytes(filename);
            b = Decompress(b, encodeKey);
            return Encoding.UTF8.GetString(b);
        }

        public static string DecompressStringFromFile(string filename)
        {
            byte[] b = File.ReadAllBytes(filename);
            b = Decompress(b);
            string str = Encoding.UTF8.GetString(b);
            return str;
        }


    }
}
