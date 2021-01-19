using System.Collections.Generic;

namespace TWTools.Push
{
    /// <summary>
    /// 推送数据模型
    /// </summary>
    public class PushModel
    {
        private string _appIdentifier = "unknown";
        private string _sound = "default";
        private Dictionary<string, string> _extra;
        private PushAudienceObject _audience;
        
        public PushModel() : this(null, null)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appKey"></param>
        /// <param name="appSecret"></param>
        /// <param name="channel">推送通道，</param>
        /// <param name="platform">默认iOS+Android</param>
        public PushModel(string appKey, string appSecret, PushChannel channel = PushChannel.JPush, int platform = 3)
        {
            this.AppKey = appKey;
            this.AppSecret = appSecret;
            this.APNSIsProduction = Push.Config.APNSIsProduction;
            this.Channel = channel;
            this.Platform = platform;
            this.LoggerName = Common.DefaultLoggerName;
            this.LoggerSource = Common.DefaultLoggerSource;
            this.Audience.Category = PushAudienceCategory.Alias;
            this.NotificationWay = NotificationWay.APNs;
            this.LowerExtraKey = true;
        }

        /// <summary>
        /// 是否将extra key转换为小写
        /// </summary>
        public bool LowerExtraKey { get; set; }

        /// <summary>
        /// 通知方式，默认为APNs方式
        /// </summary>
        public NotificationWay NotificationWay { get; set; }

        /// <summary>
        /// log4net日志记录名称，用于创建日志文件夹路径
        /// </summary>
        public string LoggerName { get; set; }

        /// <summary>
        /// log4net日志源，用于创建日志文件夹路径
        /// </summary>
        public string LoggerSource { get; set; }

        /// <summary>
        /// App标识
        /// </summary>
        public string AppIdentifier
        {
            get { return _appIdentifier; }
            set { _appIdentifier = value ?? "unknown"; }
        }

        /// <summary>
        /// AppKey
        /// </summary>
        public string AppKey { get; set; }

        /// <summary>
        /// AppSecret
        /// </summary>
        public string AppSecret { get; set; }

        /// <summary>
        /// 推送环境（测试、生产），仅适用于iOS，配置文件配置项
        /// </summary>
        public bool APNSIsProduction { get; set; }

        /// <summary>
        /// 命令名称，主要用于App客户端解析
        /// </summary>
        public string Command { get; set; }

        /// <summary>
        /// 命令名称，主要用于记录日志
        /// </summary>
        public string CommandName { get; set; }

        /// <summary>
        /// 推送标题，仅适用于Android
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 推送消息内容，显示在通知栏的文本内容
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 关键信息，用于记录日志
        /// </summary>
        public string KeyInfomation { get; set; }

        /// <summary>
        /// 需要传递给App客户端的额外信息
        /// </summary>
        public Dictionary<string, string> Extras
        {
            get
            {
                if (_extra == null)
                {
                    _extra = new Dictionary<string, string>();
                }
                return _extra;
            }
        }

        /// <summary>
        /// 声音文件名称，默认为“default”
        /// </summary>
        public string Sound
        {
            get { return _sound; }
            set { _sound = value; }
        }

        /// <summary>
        /// 推送消息数量角标
        /// </summary>
        public int Badge { get; set; }

        /// <summary>
        /// 推送第三方通道，默认极光推送
        /// </summary>
        public PushChannel Channel { get; set; }

        /// <summary>
        /// 推送平台，默认为0，全平台
        /// </summary>
        public int Platform { get; set; }

        /// <summary>
        /// 推送目标
        /// </summary>
        public PushAudienceObject Audience
        {
            get
            {
                if (_audience == null)
                {
                    _audience = new PushAudienceObject();
                }
                return _audience;
            }
        }


        /// <summary>
        /// 用于PMS版本区分当前消息模型是哪一个操作在进行推送
        /// </summary>
        public int PMSIncidentAction
        {
            get; set;
        }


        /// <summary>
        /// PMS版本推送短信消息的内容
        /// </summary>
        public string PMSIncidentShortMessage
        {
            get; set;
        }
    }
}
