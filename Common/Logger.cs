using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Common
{
    /// <summary>
    /// 日志处理
    /// </summary>
    public class Logger
    {
        #region 创建 Logger 处理器对象

        public static List<MLogger> LsLogger { get; set; }
        //static Logger() 
        //{
        //    LsLogger = new List<MLogger>();
        //}
        //static readonly Logger uniqueInstance = new Logger();


        //public static Logger Instance
        //{
        //    get { return uniqueInstance; }
        //}

        private static Logger logger = null;


        /// <summary>
        /// 创建 Logger 处理器对象
        /// </summary>
        /// <value>Logger处理器对象</value>
        public static Logger Current
        {
            get
            {
                if (logger == null)
                    logger = new Logger();
                return logger;
            }
        }

        public Logger()
        {
            LsLogger = new List<MLogger>();
        }
        #endregion


        public List<MLogger> GetLsLogger()
        {
            return LsLogger;
        }


        /// <summary>
        /// 系统日志，默认每天一个独立日志文件
        /// </summary>
        /// <param name="logInfo">日志信息</param>
        public void WriteLog(string logInfo)
        {
            try
            {
                WriteLog(logInfo, false);
            }
            catch
            {

            }
        }

        /// <summary>
        /// 系统日志
        /// </summary>
        /// <param name="logInfo">日志信息</param>
        /// <param name="singleFile">
        ///     是否记录在单个文件中，true 记录在单个文件，false 每天生成一个独立日志文件
        /// </param>
        public void WriteLog(string logInfo, bool singleFile)
        {
            try
            {
                if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "/sys_log/"))
                {
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "/sys_log/");
                }
                MLogger MLogger = new MLogger();
                MLogger.LoggerTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                MLogger.LoggerInfo = logInfo;
                LsLogger.Add(MLogger);

                string strFilePath = AppDomain.CurrentDomain.BaseDirectory + "/sys_log/系统日志.txt";

                FileStream fs = null;
                if (singleFile)
                {
                    //单个日志文件
                    fs = new FileStream(strFilePath, FileMode.Append);
                }
                else
                {
                    //每天一个日志文件
                    strFilePath = AppDomain.CurrentDomain.BaseDirectory + "/sys_log/" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                    fs = new FileStream(strFilePath, File.Exists(strFilePath) ? FileMode.Append : FileMode.Create);
                }
                if (fs.Length > 1024 * 5)
                {
                    fs = new FileStream(strFilePath, FileMode.Create);
                }
                StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Default);
                sw.WriteLine(logInfo + "    " + DateTime.Now.ToString() + " \n");
                sw.Close();
                fs.Close();
            }
            catch
            {

            }
        }

        /// <summary>
        /// 创建文本文件，并写入信息
        /// </summary>
        /// <param name="filePath">文件全路径</param>
        /// <param name="txt">写入信息</param>
        /// <param name="newFile">是否创建新文件</param>
        public void WriteTxt(string filePath, string txt, bool newFile)
        {
            try
            {
                FileStream fs = fs = new FileStream(filePath, newFile ? FileMode.Create : FileMode.Append);
                StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Default);
                sw.WriteLine(txt);
                sw.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                WriteLog("WriteTxt：文件【" + filePath + "】" + ex.Message);
            }
        }


        #region 写日志文件
        public void WriteLog(string FileFlag, string Message)
        {
            string fileLogPath = @"d:\logFiles\";

            DateTime d = DateTime.Now;
            string LogFileName = FileFlag + d.ToString("yyyyMMdd").ToString() + ".txt";

            //DirectoryInfo path=new DirectoryInfo(LogFileName);
            //如果日志文件目录不存在,则创建
            if (!Directory.Exists(fileLogPath))
            {
                Directory.CreateDirectory(fileLogPath);
            }

            FileInfo finfo = new FileInfo(fileLogPath + LogFileName);

            try
            {
                FileStream fs = new FileStream(fileLogPath + LogFileName, FileMode.Append);
                StreamWriter strwriter = new StreamWriter(fs);
                try
                {
                    strwriter.WriteLine(Message);
                    strwriter.WriteLine();
                    strwriter.Flush();
                }
                catch
                {
                    //Console.WriteLine("日志文件写入失败信息:"+ee.ToString()); 
                }
                finally
                {
                    strwriter.Close();
                    strwriter = null;
                    fs.Close();
                    fs = null;
                }
            }
            catch
            {

            }
        }

        public void WriteLogE(string FileFlag, string Message)
        {
            string fileLogPath = @"E:\logFiles\";

            DateTime d = DateTime.Now;
            string LogFileName = FileFlag + d.ToString("yyyyMMdd").ToString() + ".txt";

            //DirectoryInfo path=new DirectoryInfo(LogFileName);
            //如果日志文件目录不存在,则创建
            if (!Directory.Exists(fileLogPath))
            {
                Directory.CreateDirectory(fileLogPath);
            }

            FileInfo finfo = new FileInfo(fileLogPath + LogFileName);

            try
            {
                FileStream fs = new FileStream(fileLogPath + LogFileName, FileMode.Append);
                StreamWriter strwriter = new StreamWriter(fs);
                try
                {
                    strwriter.WriteLine(Message);
                    strwriter.WriteLine();
                    strwriter.Flush();
                }
                catch
                {
                    //Console.WriteLine("日志文件写入失败信息:"+ee.ToString()); 
                }
                finally
                {
                    strwriter.Close();
                    strwriter = null;
                    fs.Close();
                    fs = null;
                }
            }
            catch
            {

            }
        }

        #endregion

    }
}