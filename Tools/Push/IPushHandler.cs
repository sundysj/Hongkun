using log4net;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace TWTools.Push
{
    /// <summary>
    /// 推送数据模型抽象处理类
    /// </summary>
    internal abstract class IPushHandler
    {
        /// <summary>
        /// 推送来源
        /// </summary>
        public string Source;

        /// <summary>
        /// 推送数据模型
        /// </summary>
        public PushModel PushModel { get; protected set; }

        public IPushHandler(PushModel model)
        {
            string command = "未知";
            if (model != null && string.IsNullOrEmpty(model.Command))
            {
                command = model.Command;
            }

            ILog logger = Logger.TWRollingFileLogger.GetCustomLogger(model.LoggerName, model.LoggerSource, model.AppIdentifier);

            if (model.AppKey == null)
            {
                logger.Error(string.Format("Exception，AppKey为空，Command={0}", command));
            }
            if (model.AppSecret == null)
            {
                logger.Error(string.Format("Exception，AppSecret为空，Command={0}", command));
            }
            if (model.Command == null)
            {
                logger.Error("Exception，Command为空");
            }
            if (model.Message == null)
            {
                logger.Error(string.Format("Exception，Message为空，Command={0}", command));
            }
            if (model.Audience.Objects.Count == 0 && model.Audience.SecondObjects.Objects.Count == 0)
            {
                logger.Error(string.Format("Exception，无推送目标，Command={0}", command));
            }
            
            this.PushModel = model;
            this.Handler();
        }

        /// <summary>
        /// 自定义推送模型处理方法
        /// </summary>
        protected abstract void Handler();

        /// <summary>
        /// 发送通知
        /// </summary>
        public abstract string Send();

        /// <summary>
        /// 异步发送通知
        /// </summary>
        public abstract Task<string> SendAsync();
    }
}
