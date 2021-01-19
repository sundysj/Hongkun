using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TianChengEntranceSyncService.Util
{
    public class Log
    {
        private static object locker = new object();
        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="text"></param>
        public static void WriteLog(string text, string dir = "")
        {
            if (string.IsNullOrEmpty(dir))
            {
                dir = "MoredianLogs\\";
            }
            if (!dir.EndsWith("\\"))
            {
                dir += "\\";
            }
            dir = Path.Combine(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase, dir);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            string fileName = DateTime.Now.ToString("yyyyMMddHHmm") + ".txt";
            string path = Path.Combine(dir, fileName);
            lock (locker)
            {
                using (StreamWriter output = System.IO.File.AppendText(path))
                {
                    output.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "：" + text + "\r\n");
                    output.Close();
                }
            }
        }
    }
}
