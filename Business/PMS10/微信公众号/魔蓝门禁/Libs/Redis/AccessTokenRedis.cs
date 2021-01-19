using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aop.Api;
using Aop.Api.Request;
using Aop.Api.Response;

namespace TianChengEntranceSyncService.Redis
{
    public class AccessTokenRedis : Redis
    {
        public static readonly string KEY_ACCESS_TOKEN = "ENTRANCE:ACCESSTOKEN:{0}";
        public static string GetAccessToken(string OrgId, string OrgAuthKey)
        {
            if (mRedis.KeyExists(string.Format(KEY_ACCESS_TOKEN, OrgId)))
            {
                return mRedis.StringGet(string.Format(KEY_ACCESS_TOKEN, OrgId));
            }
            IMoredianApiClient client = new DefaultMoredianApiClient(Config.MoreDian.APIURL);
            MoredianAccessTokenRequest request = new MoredianAccessTokenRequest
            {
                OrgId = OrgId,
                OrgAuthKey = OrgAuthKey
            };
            MoredianAccessTokenResponse response = client.Execute(request, AppTokenRedis.GetAppToken(), null);
            if (null == response || !response.IsSucc())
            {
                return "";
            }
            mRedis.StringSet(string.Format(KEY_ACCESS_TOKEN, OrgId), response.accessToken, TimeSpan.FromSeconds(response.expires));
            return response.accessToken;
        }
    }
}
