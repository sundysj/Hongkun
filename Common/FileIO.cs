using System;
using System.Data;
using System.IO;
using System.Text;

namespace MobileSoft.Common
{
    public class FileIO
    {
        public static void WriteFile(string content, string fileSavePath)
        {
            if (System.IO.File.Exists(fileSavePath))
            {
                System.IO.File.Delete(fileSavePath);
            }
            using (System.IO.FileStream fs = System.IO.File.Create(fileSavePath))
            {
                byte[] info =System.Text.Encoding.GetEncoding("gb2312").GetBytes(content);   //防止乱码
                fs.Write(info, 0, info.Length);
            }
        }

        public string ReadFile(string fileOpenPath)
        {
            if (!System.IO.File.Exists(fileOpenPath))
            {
                return null;
            }
            using (System.IO.StreamReader sr = System.IO.File.OpenText(fileOpenPath))
            {
                return sr.ReadToEnd().ToString();
            }
        }
    }
}
