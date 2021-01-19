using System;
using System.Configuration;
using System.Reflection;

namespace TWTools
{
    /// <summary>
    /// 通用
    /// </summary>
    public class Common
    {
        private static string _assemblyFullPath;
        private static string _defaultLoggerName;
        private static string _defaultLoggerSource;

        /// <summary>
        /// 程序集文件路径
        /// </summary>
        public static string AssemblyFullPath
        {
            get
            {
                if (_assemblyFullPath == null)
                {
                    _assemblyFullPath = string.Format("{0}\\{1}.dll",
                        AppDomain.CurrentDomain.RelativeSearchPath ?? AppDomain.CurrentDomain.BaseDirectory,
                        Assembly.GetAssembly(typeof(Common)).GetName().Name);
                }
                return _assemblyFullPath;
            }
        }

        /// <summary>
        /// log4net默认loggerName
        /// </summary>
        public static string DefaultLoggerName
        {
            get
            {
                if (_defaultLoggerName == null)
                {
                    _defaultLoggerName = GetAppSettingValue("LoggerName") ?? "Unknown";
                }
                return _defaultLoggerName;
            }
        }

        /// <summary>
        /// log4net默认loggerSource
        /// </summary>
        public static string DefaultLoggerSource
        {
            get
            {
                if (_defaultLoggerSource == null)
                {
                    _defaultLoggerSource = GetAppSettingValue("LoggerSource") ?? GetAppSettingValue("Source") ?? "Unknown";
                }
                return _defaultLoggerSource;
            }
        }

        /// <summary>
        /// 获取配置节点的值
        /// </summary>
        public static string GetAppSettingValue(string key)
        {
            Configuration AppConfig = ConfigurationManager.OpenExeConfiguration(Common.AssemblyFullPath);
            KeyValueConfigurationCollection keyValue = AppConfig.AppSettings.Settings;

            if (keyValue != null)
            {
                KeyValueConfigurationElement element = keyValue[key];
                if (element != null)
                {
                    return element.Value;
                }
            }
            return null;
        }
    }
}
