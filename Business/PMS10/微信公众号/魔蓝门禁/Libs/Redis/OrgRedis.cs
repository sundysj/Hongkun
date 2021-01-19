using Aop.Api;
using Aop.Api.Model;
using Aop.Api.Request;
using Aop.Api.Response;
using Dapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Log = TianChengEntranceSyncService.Util.Log;

namespace TianChengEntranceSyncService.Redis
{
    public class OrgRedis : Redis
    {
        public static readonly string KEY_MOREDIAN_ORG = "ENTRANCE:MOREDIANORG";
        public static MoredianOrg GetMoredianOrg()
        {
            #region 先从Redis取值
            if (mRedis.KeyExists(KEY_MOREDIAN_ORG))
            {
                string json = mRedis.StringGet(KEY_MOREDIAN_ORG);
                try
                {
                    MoredianOrg moredianOrg = JsonConvert.DeserializeObject<MoredianOrg>(json);
                    // 检查是否存在OrgId以及OrgAuthKey这2个重要参数
                    if (null != moredianOrg && 0 != moredianOrg.orgId && !string.IsNullOrEmpty(moredianOrg.orgAuthKey))
                    {
                        return moredianOrg;
                    }
                }
                catch (Exception)
                {
                }
            }
            #endregion

            #region 不存在的情况下去数据库查询
            using (IDbConnection conn = new SqlConnection(Config.EntranceConnectionStr))
            {
                MoredianOrg moredianOrg = null;
                dynamic info = conn.QueryFirstOrDefault("SELECT OrgId,OrgAuthKey FROM Tb_HSPR_Entrance_Org WITH(NOLOCK)");
                if (null == info)
                {
                    #region 数据库也不存在，进行创建
                    IMoredianApiClient client = new DefaultMoredianApiClient(Config.MoreDian.APIURL);
                    MoredianCreateOrgRequest request = new MoredianCreateOrgRequest
                    {
                        moredianOrg = new MoredianOrg
                        {
                            orgName = Config.CorpName,
                            tpId = Config.CorpID
                        }
                    };
                    MoredianCreateOrgResponse response = client.Execute(request);
                    if (null == response || !response.IsSucc())
                    {
                        // 创建失败，需要排查错误信息
                        Log.WriteLog($"创建机构失败({JsonConvert.SerializeObject(response)})");
                        return null;
                    }
                    moredianOrg = new MoredianOrg
                    {
                        orgId = response.orgId,
                        orgAuthKey = response.orgAuthKey
                    };
                    #endregion
                }
                else
                {
                    moredianOrg = new MoredianOrg
                    {
                        orgId = Convert.ToInt64(info.OrgId),
                        orgAuthKey = Convert.ToString(info.OrgAuthKey),
                    };
                }
                // 刷新缓存值
                mRedis.StringSet(KEY_MOREDIAN_ORG, JsonConvert.SerializeObject(moredianOrg));
                return moredianOrg;
            }
            #endregion
        }
    }
}
