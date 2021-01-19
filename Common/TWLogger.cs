using log4net;
using log4net.Config;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading;

namespace Common
{
    public sealed class TWLogger
    {
        /// <summary>
        /// 记录消息Queue
        /// </summary>
        private readonly ConcurrentQueue<TWLogMessage> _que;

        /// <summary>
        /// 信号
        /// </summary>
        private readonly ManualResetEvent _mre;

        /// <summary>
        /// 日志
        /// </summary>
        private readonly ILog _log;

        /// <summary>
        /// 日志
        /// </summary>
        private static TWLogger _twLogger = new TWLogger();


        private TWLogger()
        {
            var configFile = new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log4net.config"));
            if (!configFile.Exists)
            {
                throw new Exception("未配置log4net配置文件！");
            }

            // 设置日志配置文件路径
            XmlConfigurator.Configure(configFile);

            _que = new ConcurrentQueue<TWLogMessage>();
            _mre = new ManualResetEvent(false);
            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }

        /// <summary>
        /// 实现单例
        /// </summary>
        public static TWLogger Instance()
        {
            return _twLogger;
        }

        /// <summary>
        /// 另一个线程记录日志，只在程序初始化时调用一次
        /// </summary>
        public void Register()
        {
            Thread t = new Thread(new ThreadStart(WriteLog));
            t.IsBackground = false;
            t.Start();
        }

        /// <summary>
        /// 从队列中写日志至磁盘
        /// </summary>
        private void WriteLog()
        {
            while (true)
            {
                // 等待信号通知
                _mre.WaitOne();

                TWLogMessage msg;
                // 判断是否有内容需要如磁盘 从列队中获取内容，并删除列队中的内容
                while (_que.Count > 0 && _que.TryDequeue(out msg))
                {
                    // 判断日志等级，然后写日志
                    switch (msg.Level)
                    {
                        case TWLogLevel.Debug:
                            _log.Debug(msg.Message, msg.Exception);
                            break;
                        case TWLogLevel.Info:
                            _log.Info(msg.Message, msg.Exception);
                            break;
                        case TWLogLevel.Error:
                            _log.Error(msg.Message, msg.Exception);
                            break;
                        case TWLogLevel.Warn:
                            _log.Warn(msg.Message, msg.Exception);
                            break;
                        case TWLogLevel.Fatal:
                            _log.Fatal(msg.Message, msg.Exception);
                            break;
                    }
                }

                // 重新设置信号
                _mre.Reset();
                Thread.Sleep(1);
            }
        }


        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="message">日志文本</param>
        /// <param name="level">等级</param>
        /// <param name="ex">Exception</param>
        public void EnqueueMessage(string message, TWLogLevel level, Exception ex = null)
        {
            if ((level == TWLogLevel.Debug && _log.IsDebugEnabled)
             || (level == TWLogLevel.Error && _log.IsErrorEnabled)
             || (level == TWLogLevel.Fatal && _log.IsFatalEnabled)
             || (level == TWLogLevel.Info && _log.IsInfoEnabled)
             || (level == TWLogLevel.Warn && _log.IsWarnEnabled))
            {
                _que.Enqueue(new TWLogMessage
                {
                    Message = "[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss,fff") + "]\r\n" + message,
                    Level = level,
                    Exception = ex
                });

                // 通知线程往磁盘中写日志
                _mre.Set();
            }
        }

        public static void Debug(string msg, Exception ex = null)
        {
            Instance().EnqueueMessage(msg, TWLogLevel.Debug, ex);
        }

        public static void Error(string msg, Exception ex = null)
        {
            Instance().EnqueueMessage(msg, TWLogLevel.Error, ex);
        }

        public static void Fatal(string msg, Exception ex = null)
        {
            Instance().EnqueueMessage(msg, TWLogLevel.Fatal, ex);
        }

        public static void Info(string msg, Exception ex = null)
        {
            Instance().EnqueueMessage(msg, TWLogLevel.Info, ex);
        }

        public static void Warn(string msg, Exception ex = null)
        {
            Instance().EnqueueMessage(msg, TWLogLevel.Warn, ex);
        }

    }

    /// <summary>
    /// 日志等级
    /// </summary>
    public enum TWLogLevel
    {
        Debug,
        Info,
        Error,
        Warn,
        Fatal
    }


    /// <summary>
    /// 日志内容
    /// </summary>
    public class TWLogMessage
    {
        public string Message { get; set; }
        public TWLogLevel Level { get; set; }
        public Exception Exception { get; set; }

    }
}
