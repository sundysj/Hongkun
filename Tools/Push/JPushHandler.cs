using cn.jpush.api;
using cn.jpush.api.push;
using cn.jpush.api.push.mode;
using cn.jpush.api.push.notification;
using log4net;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace TWTools.Push
{
    /// <summary>
    /// 极光推送处理类
    /// </summary>
    internal class JPushHandler : IPushHandler
    {
        /// <summary>
        /// 极光推送载体
        /// </summary>
        private PushPayload pushPayload;

        public JPushHandler(PushModel model) : base(model)
        {
            this.Source = Push.Config.Source;
        }

        /// <summary>
        /// 极光推送处理方法
        /// </summary>
        protected override void Handler()
        {
            pushPayload = new PushPayload();
            pushPayload.audience = PushModel.Audience == null ? Audience.all() : GetAudience();
            pushPayload.options.apns_production = PushModel.APNSIsProduction;

            if (PushModel.NotificationWay == NotificationWay.All)
            {
                pushPayload.message = Message.content(PushModel.Message);
                pushPayload.notification = new Notification().setAlert(PushModel.Message);
            }
            else if (PushModel.NotificationWay == NotificationWay.APNs)
            {
                pushPayload.notification = new Notification().setAlert(PushModel.Message);
            }
            else
            {
                pushPayload.message = Message.content(PushModel.Message);
            }

            // 平台判断
            switch (PushModel.Platform)
            {
                case 1: // Android
                    if (pushPayload.notification != null)
                    {
                        pushPayload.notification.AndroidNotification = new AndroidNotification();
                    }
                    pushPayload.platform = Platform.android();
                    break;

                case 2: // iOS
                    if (pushPayload.notification != null)
                    {
                        pushPayload.notification.IosNotification = new IosNotification();
                    }
                    pushPayload.platform = Platform.ios();
                    break;

                case 3: // Android + iOS
                    if (pushPayload.notification != null)
                    {
                        pushPayload.notification.IosNotification = new IosNotification();
                        pushPayload.notification.AndroidNotification = new AndroidNotification();
                    }
                    pushPayload.platform = Platform.android_ios();
                    break;

                case 4: // WinPhone
                    if (pushPayload.notification != null)
                    {
                        pushPayload.notification.WinphoneNotification = new WinphoneNotification();
                    }
                    pushPayload.platform = Platform.winphone();
                    break;

                case 5: // Android + WinPhone
                    if (pushPayload.notification != null)
                    {
                        pushPayload.notification.AndroidNotification = new AndroidNotification();
                        pushPayload.notification.WinphoneNotification = new WinphoneNotification();
                    }
                    pushPayload.platform = Platform.android_winphone();
                    break;

                case 6: // iOS + WinPhone
                    if (pushPayload.notification != null)
                    {
                        pushPayload.notification.IosNotification = new IosNotification();
                        pushPayload.notification.WinphoneNotification = new WinphoneNotification();
                    }
                    pushPayload.platform = Platform.ios_winphone();
                    break;

                default:// 全平台
                    if (pushPayload.notification != null)
                    {
                        pushPayload.notification.IosNotification = new IosNotification();
                        pushPayload.notification.AndroidNotification = new AndroidNotification();
                        pushPayload.notification.WinphoneNotification = new WinphoneNotification();
                    }
                    pushPayload.platform = Platform.all();
                    break;
            }

            // APNs
            if (pushPayload.notification != null)
            {
                // iOS设置声音、消息内容、角标、额外信息
                if (pushPayload.notification.IosNotification != null)
                {
                    pushPayload.notification.IosNotification.setSound(PushModel.Sound);
                    pushPayload.notification.IosNotification.AddExtra("command", PushModel.Command);

                    if (PushModel.Badge <= 1)
                    {
                        pushPayload.notification.IosNotification.incrBadge(1);
                    }
                    else
                    {
                        pushPayload.notification.IosNotification.setBadge(PushModel.Badge);
                    }

                    // 额外信息
                    if (PushModel.Extras.Keys.Count != 0)
                    {
                        foreach (var item in PushModel.Extras.Keys)
                        {
                            pushPayload.notification.IosNotification.AddExtra(PushModel.LowerExtraKey ? item.ToLower() : item, PushModel.Extras[item]);
                        }
                    }
                }

                // Android设置标题、消息内容、额外信息
                if (pushPayload.notification.AndroidNotification != null)
                {
                    pushPayload.notification.AndroidNotification.AddExtra("command", PushModel.Command);
                    pushPayload.notification.AndroidNotification.setBig_text(PushModel.Message);

                    if (PushModel.Title != null)
                    {
                        pushPayload.notification.AndroidNotification.setTitle(PushModel.Title);
                    }

                    // 额外信息
                    if (PushModel.Extras.Keys.Count != 0)
                    {
                        foreach (var item in PushModel.Extras.Keys)
                        {
                            pushPayload.notification.AndroidNotification.AddExtra(PushModel.LowerExtraKey ? item.ToLower() : item, PushModel.Extras[item]);
                        }
                    }
                }

                // WinPhone设置标题、消息内容、额外信息
                if (pushPayload.notification.WinphoneNotification != null)
                {
                    pushPayload.notification.WinphoneNotification.AddExtra("command", PushModel.Command);

                    if (PushModel.Title != null)
                    {
                        pushPayload.notification.WinphoneNotification.setTitle(PushModel.Title);
                    }

                    // 额外信息
                    if (PushModel.Extras.Keys.Count != 0)
                    {
                        foreach (var item in PushModel.Extras.Keys)
                        {
                            pushPayload.notification.WinphoneNotification.AddExtra(PushModel.LowerExtraKey ? item.ToLower() : item, PushModel.Extras[item]);
                        }
                    }
                }
            }

            // 消息透传
            if (pushPayload.message != null)
            {
                pushPayload.message.AddExtras("command", PushModel.Command);

                if (PushModel.Title != null)
                {
                    pushPayload.message.setTitle(PushModel.Title);
                }

                // 额外信息
                if (PushModel.Extras.Keys.Count != 0)
                {
                    foreach (var item in PushModel.Extras.Keys)
                    {
                        pushPayload.message.AddExtras(PushModel.LowerExtraKey ? item.ToLower() : item, PushModel.Extras[item]);
                    }
                }
            }
        }

        /// <summary>
        /// 获取推送目标
        /// </summary>
        private Audience GetAudience()
        {
            Audience audience = null;

            switch (PushModel.Audience.Category)
            {
                // 标签
                case PushAudienceCategory.Tags:
                    audience = Audience.s_tag(PushModel.Audience.Objects.ToArray());
                    break;

                // 别名
                case PushAudienceCategory.Alias:
                    audience = Audience.s_alias(PushModel.Audience.Objects.ToArray());
                    break;

                // RegistrationID
                case PushAudienceCategory.RegistrationID:
                    audience = Audience.s_registrationId(PushModel.Audience.Objects.ToArray());
                    break;

                // 群组，暂未支持
                case PushAudienceCategory.UserGroup:
                    return null;

                // 广播
                default:
                    return Audience.all();
            }

            if (PushModel.Audience.SecondObjects != null && PushModel.Audience.SecondObjects.Objects.Count > 0)
            {
                // 二级关联取交集
                if (PushModel.Audience.SecondObjects.Category == PushAudienceSecondCategory.TagsAnd)
                {
                    audience.tag_and(PushModel.Audience.SecondObjects.Objects.ToArray());
                }

                // 二级关联取非，当前SDK暂不支持
                //if (PushModel.Audience.SecondObjects.Category == PushAudienceSecondCategory.TagsNot)
                //{

                //}
            }
            return audience;
        }

        /// <summary>
        /// 推送
        /// </summary>
        public override string Send()
        {
            MessageResult result = null;
            try
            {
                result = result = new JPushClient(PushModel.AppKey, PushModel.AppSecret).SendPush(pushPayload);
                bool isResultOK = result.isResultOK();

                if (Environment.UserInteractive)
                {
                    Console.WriteLine(isResultOK);
                }

                ILog logger = Logger.TWRollingFileLogger.GetCustomLogger(PushModel.LoggerName, PushModel.LoggerSource, PushModel.AppIdentifier);
                if (isResultOK)
                {
                    Task.Factory.StartNew(() =>
                    {
                        logger.Info(string.Format("{0}->成功，Source={1}，Command={2}，Key={3}，Result={4}",
                        this.PushModel.APNSIsProduction ? "生产环境" : "测试环境",
                        this.Source,
                        string.IsNullOrEmpty(PushModel.CommandName) ? PushModel.Command : PushModel.CommandName,
                        PushModel.KeyInfomation ?? "",
                        result?.ResponseResult.responseContent));
                    });
                }
                else
                {
                    Task.Factory.StartNew(() =>
                    {
                        logger.Error(string.Format("{0}->失败，Source={1}，Command={2}，Key={3}，Audience={4}，Result={5}，{6}，JPushError={7}，{8}，Url={9}",
                        this.PushModel.APNSIsProduction ? "生产环境" : "测试环境",
                        this.Source,
                        string.IsNullOrEmpty(PushModel.CommandName) ? PushModel.Command : PushModel.CommandName,
                        PushModel.KeyInfomation ?? "",
                        JsonConvert.SerializeObject(this.pushPayload.audience.dictionary, Formatting.Indented),
                        result?.ResponseResult.responseContent,
                        result?.ResponseResult.exceptionString,
                        result?.ResponseResult.jpushError.error.code,
                        result?.ResponseResult.jpushError.error.message,
                        result?.ResponseResult.url));
                    });
                }

                return result?.ResponseResult.responseContent;
            }
            catch (Exception ex)
            {
                try
                {
                    Task.Factory.StartNew(() =>
                    {
                        ILog logger = Logger.TWRollingFileLogger.GetCustomLogger(PushModel.LoggerName, PushModel.LoggerSource, PushModel.AppIdentifier);
                        logger.Error(string.Format("{0}->异常，Source={1}，Command={2}，Key={3}，Exception={4}，AppKey={5}, AppSecret={6}, Url={7}",
                            this.PushModel.APNSIsProduction ? "生产环境" : "测试环境",
                            this.Source,
                            string.IsNullOrEmpty(PushModel.CommandName) ? PushModel.Command : PushModel.CommandName,
                            PushModel.KeyInfomation ?? "",
                            ex.Message + Environment.NewLine + ex.StackTrace+ex.InnerException,
                            PushModel.AppKey, PushModel.AppSecret, result?.ResponseResult.url));
                    });

                    return ex.Message;
                }
                catch (Exception ex2)
                {
                    Task.Factory.StartNew(() =>
                    {
                        EventLog log = new EventLog("TWInterface.Push");
                        if (!EventLog.SourceExists("TWInterface"))
                            EventLog.CreateEventSource("TWInterface", "TWInterface.Push");

                        log.WriteEntry(ex2.Message + Environment.NewLine + ex.StackTrace, EventLogEntryType.Error);
                    });
                    return ex2.Message + Environment.NewLine + ex.StackTrace;
                }
            }
        }


        /// <summary>
        /// 异步推送
        /// </summary>
        public override Task<string> SendAsync()
        {
            return Task.Factory.StartNew(() =>
            {
                return Send();
            });
        }
    }
}
