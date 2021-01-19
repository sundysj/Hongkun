using System;

namespace TWTools.Push
{
    public partial class Push
    {
        public static class Config
        {
            /// <summary>
            /// 默认配置的推送环境
            /// </summary>
            public static bool APNSIsProduction
            {
                get
                {
                    try
                    {
                        return (Convert.ToInt32(Common.GetAppSettingValue("AppPushIsProduction")) > 0);
                    }
                    catch (Exception)
                    {
                        return true;
                    }
                }
            }

            /// <summary>
            /// 推送来源
            /// </summary>
            public static string Source
            {
                get
                {
                    try
                    {
                        return Common.GetAppSettingValue("Source") ?? Common.GetAppSettingValue("LoggerSource") ?? "未知";
                    }
                    catch (Exception)
                    {
                        return "未知";
                    }
                }
            }
        }

        private Push()
        {

        }

        public static string Send(PushModel model)
        {
            switch (model.Channel)
            {
                case PushChannel.JPush:
                    return new JPushHandler(model).Send();
                default:
                    return null;
            }
        }

        public static string SendAsync(PushModel model)
        {
            switch (model.Channel)
            {
                case PushChannel.JPush:
                    return new JPushHandler(model).SendAsync().Result;
                case PushChannel.HWPush:
                    new HuaWeiPushHandler(model).PushHuaWeiInfoAsync();
                    return "1";
                case PushChannel.XMPush:
                    new XiaomiPush(model).PushXiaoMi();
                    return "1";
                default:
                    return null;
            }
        }
    }
}
