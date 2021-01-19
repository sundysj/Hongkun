using Aop.Api;
using Aop.Api.Request;
using Aop.Api.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TianChengEntranceSyncService.Redis
{
    public class AppTokenRedis : Redis
    {
        public static readonly string KEY_APP_TOKEN = "ENTRANCE:APPTOKEN";
        public static string GetAppToken()
        {
            if (mRedis.KeyExists(KEY_APP_TOKEN))
            {
                return mRedis.StringGet(KEY_APP_TOKEN);
            }
            IMoredianApiClient client = new DefaultMoredianApiClient(Config.MoreDian.APIURL);
            MoredianAppTokenRequest request = new MoredianAppTokenRequest
            {
                AppId = Config.MoreDian.APPID,
                Secret = Config.MoreDian.SECRET
            };
            MoredianAppTokenResponse response = client.Execute(request);
            if (null == response || !response.IsSucc())
            {
                return "";
            }
            mRedis.StringSet(KEY_APP_TOKEN, response.appToken, TimeSpan.FromSeconds(response.expires));
            return response.appToken;
        }
    }
}
