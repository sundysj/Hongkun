using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace TWTools.Logger
{
    public static class TWRollingFileLogger
    {
        private static readonly ConcurrentDictionary<string, ILog> loggerContainer = new ConcurrentDictionary<string, ILog>();

        private static readonly Dictionary<string, TWReadParameterAppender> appenderContainer = new Dictionary<string, TWReadParameterAppender>();
        private static object lockObj = new object();

        // 默认配置  
        private const int MAX_SIZE_ROLL_BACKUPS = 20;
        private const string LAYOUT_PATTERN = "%d{yyyy-MM-dd HH:mm:ss} - %m%n";
        private const string DATE_PATTERN = "yyyyMMdd'.txt'";
        private const string MAXIMUM_FILE_SIZE = "256MB";
        private const string LEVEL = "info";

        /// <summary>
        /// 读取配置文件并缓存  
        /// </summary>
        static TWRollingFileLogger()
        {
            Uri uri = new Uri(Common.AssemblyFullPath + ".config");
            log4net.Config.XmlConfigurator.Configure(uri);
            IAppender[] appenders = LogManager.GetRepository().GetAppenders();

            for (int i = 0; i < appenders.Length; i++)
            {
                if (appenders[i] is TWReadParameterAppender)
                {
                    TWReadParameterAppender appender = (TWReadParameterAppender)appenders[i];
                    if (appender.MaxSizeRollBackups == 0)
                    {
                        appender.MaxSizeRollBackups = MAX_SIZE_ROLL_BACKUPS;
                    }
                    if (appender.Layout != null && appender.Layout is log4net.Layout.PatternLayout)
                    {
                        appender.LayoutPattern = ((log4net.Layout.PatternLayout)appender.Layout).ConversionPattern;
                    }
                    if (string.IsNullOrEmpty(appender.LayoutPattern))
                    {
                        appender.LayoutPattern = LAYOUT_PATTERN;
                    }
                    if (string.IsNullOrEmpty(appender.DatePattern))
                    {
                        appender.DatePattern = DATE_PATTERN;
                    }
                    if (string.IsNullOrEmpty(appender.MaximumFileSize))
                    {
                        appender.MaximumFileSize = MAXIMUM_FILE_SIZE;
                    }
                    if (string.IsNullOrEmpty(appender.Level))
                    {
                        appender.Level = LEVEL;
                    }
                    lock (lockObj)
                    {
                        appenderContainer[appenders[i].Name] = appender;
                    }
                }
            }
        }

        public static void Test(string loggerName, string category)
        {
            ILog logger = TWTools.Logger.TWRollingFileLogger.GetCustomLogger(Common.DefaultLoggerName, Common.DefaultLoggerSource, category);
            logger.Info("测试日志输出");
        }

        /// <summary>
        /// 获取日志设置
        /// </summary>
        public static ILog GetCustomLogger(string loggerName, string loggerSource, string category , bool additivity = false)
        {
            return loggerContainer.GetOrAdd(loggerName + (category ?? "") + loggerSource, delegate (string name)
               {
                   RollingFileAppender newAppender = null;
                   TWReadParameterAppender appender = null;

                   if (appenderContainer.ContainsKey(loggerName))
                   {
                       appender = appenderContainer[loggerName];

                     // 判断日志路径
                     string filePath = appender.File;
                       if (filePath != null && filePath.EndsWith("\\"))
                       {
                           if (string.IsNullOrEmpty(category))
                           {
                               filePath = string.Format(@"{0}\{1}\{2}\{3}\{4}\{5}.txt", filePath, loggerName, loggerSource,
                                   DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day.ToString().PadLeft(2, '0'));
                           }
                           else
                           {
                               filePath = string.Format(@"{0}\{1}\{2}\{3}\{4}\{5}\{6}.txt", filePath, loggerName, category, loggerSource,
                                   DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day.ToString().PadLeft(2, '0'));
                           }
                       }

                       newAppender = GetNewFileApender(loggerName,
                           filePath,
                           appender.MaxSizeRollBackups,
                           appender.AppendToFile,
                           true,
                           appender.MaximumFileSize,
                           RollingFileAppender.RollingMode.Composite,
                           appender.DatePattern,
                           appender.LayoutPattern);
                   }
                   else
                   {
                       newAppender = GetNewFileApender(loggerName, GetFile(category, loggerName), MAX_SIZE_ROLL_BACKUPS, true, true, 
                           MAXIMUM_FILE_SIZE, RollingFileAppender.RollingMode.Composite, DATE_PATTERN, LAYOUT_PATTERN);
                   }

                   log4net.Repository.Hierarchy.Hierarchy repository = (log4net.Repository.Hierarchy.Hierarchy)LogManager.GetRepository();
                   log4net.Repository.Hierarchy.Logger logger = repository.LoggerFactory.CreateLogger(repository, loggerName);
                   logger.Hierarchy = repository;
                   logger.Parent = repository.Root;
                   logger.Level = GetLoggerLevel(appender == null ? LEVEL : appender.Level);
                   logger.Additivity = additivity;
                   logger.AddAppender(newAppender);
                   logger.Repository.Configured = true;
                   return new LogImpl(logger);
               });
        }

        // 如果没有指定文件路径则在运行路径下建立 log\{loggerName}.txt  
        private static string GetFile(string category, string loggerName)
        {
            if (string.IsNullOrEmpty(category))
            {
                return string.Format(@"log\{0}\{1}", loggerName, DateTime.Now.ToString(DATE_PATTERN));
            }
            else
            {
                return string.Format(@"log\{0}\{1}\{2}", loggerName, category, DateTime.Now.ToString(DATE_PATTERN));
            }
        }

        private static Level GetLoggerLevel(string level)
        {
            if (!string.IsNullOrEmpty(level))
            {
                switch (level.ToLower().Trim())
                {
                    case "debug":
                        return Level.Debug;

                    case "info":
                        return Level.Info;

                    case "warn":
                        return Level.Warn;

                    case "error":
                        return Level.Error;

                    case "fatal":
                        return Level.Fatal;
                }
            }
            return Level.Debug;
        }

        private static RollingFileAppender GetNewFileApender(string appenderName, string file, int maxSizeRollBackups, bool appendToFile = true, bool staticLogFileName = false, string maximumFileSize = "5MB", RollingFileAppender.RollingMode rollingMode = RollingFileAppender.RollingMode.Composite, string datePattern = "yyyyMMdd\".txt\"", string layoutPattern = "%d{yyyy-MM-dd HH:mm:ss} - %m%n")
        {
            RollingFileAppender appender = new RollingFileAppender
            {
                LockingModel = new FileAppender.MinimalLock(),
                Name = appenderName,
                File = file,
                AppendToFile = appendToFile,
                MaxSizeRollBackups = maxSizeRollBackups,
                MaximumFileSize = maximumFileSize,
                StaticLogFileName = staticLogFileName,
                RollingStyle = rollingMode,
                DatePattern = datePattern
            };
            PatternLayout layout = new PatternLayout(layoutPattern);
            appender.Layout = layout;
            layout.ActivateOptions();
            appender.ActivateOptions();
            return appender;
        }
    }
}
