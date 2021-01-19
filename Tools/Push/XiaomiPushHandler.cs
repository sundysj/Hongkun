using com.xiaomi.xmpush.server;
//using OpenXmlPowerTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace TWTools.Push
{
    public class XiaomiPush
    {
        PushModel model;

        public XiaomiPush(PushModel pushModel)
        {
            model = pushModel;
        }
        public void PushXiaoMi()
        {
            Constants.useOfficial();//正式环境
            //Constants.useSandbox();//测试环境，只针对IOS
            string messagePayload = "工单报事提醒";
            string title = model.Title;
            string description = model.Message;
            Sender androidSender = new Sender("Z/eYknXuJ8J1xSkVTZSr7g==");//你的AppSecret
            com.xiaomi.xmpush.server.Message androidMsg = new com.xiaomi.xmpush.server.Message.Builder()
           .title(title)
           .description(description)//通知栏展示的通知描述
           .payload(messagePayload)//透传消息
           .passThrough(1)//设置是否透传1:透传, 0通知栏消息
           .notifyId(new java.lang.Integer(Convert.ToInt32((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds)))//取时间戳，避免通知覆盖
           .restrictedPackageName("hongkun_cust.test.android")//包名
           .notifyType(new java.lang.Integer(1)) //使用默认提示音提示
           .notifyType(new java.lang.Integer(2)) //使用默认震动
           .notifyType(new java.lang.Integer(3)) //使用默认LED灯光
           .timeToLive(3600000 * 336)//服务器默认保留两周（毫秒）
           //.extra("key", "value")//字符数不能超过1024最多十组
           .build();
            foreach (var regId in model.Audience.Objects.ToList())
            {
                com.xiaomi.xmpush.server.Result androidPushResult = androidSender.send(androidMsg, regId, 3);
                string str = "";
            }
        }
    }
}