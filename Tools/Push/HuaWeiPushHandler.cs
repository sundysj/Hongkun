using AGConnectAdmin;
using AGConnectAdmin.Messaging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TWTools.Push
{
    public class HuaWeiPushHandler
    {
        PushModel model;

        AGConnectApp app = null;
        public HuaWeiPushHandler(PushModel pushModel)
        {
            model = pushModel;
        }

        public async void PushHuaWeiInfoAsync()
        {
            try
            {
                if (app == null)
                {
                    app = AGConnectApp.DefaultInstance;
                    if (app == null)
                    {
                        app = AGConnectApp.Create(new AppOptions());
                    }
                }

                string strResult = await AGConnectMessaging.DefaultInstance.SendAsync(new Message()
                {
                    Android = new AndroidConfig()
                    {
                        Notification = new AndroidNotification()
                        {
                            Title = "报事工单提醒",
                            Body = model.Message,
                            ClickAction = ClickAction.OpenApp(),
                            DefaultSound = true
                        }
                    },
                    Token = model.Audience.Objects.ToList()
                    //Token = new List<string>() { "AQAAAACy0W9KAACT8pBDiwO3pU89GoxbUJTAgEYju39uxw3kXNoqRAVz6gSLL17DbLFk8TMg51OS_x7UHfMPl7Oso79gywwxm8FFcPPYSF-I9loMGw" }
                });

                var Message = new
                {
                    title = "报事工单提醒",
                    body = model.Message
                };

                string data = JsonConvert.SerializeObject(Message);
                strResult = await AGConnectMessaging.DefaultInstance.SendAsync(new Message()
                {
                    Android = new AndroidConfig()
                    {
                        Data= data
                    },
                    Token = model.Audience.Objects.ToList()
                    //Token = new List<string>() { "AQAAAACy0W9KAACT8pBDiwO3pU89GoxbUJTAgEYju39uxw3kXNoqRAVz6gSLL17DbLFk8TMg51OS_x7UHfMPl7Oso79gywwxm8FFcPPYSF-I9loMGw" }
                });
                               
            }
            catch (Exception eex)
            {
                string str = eex.Message;
            }
        }
    }
}
