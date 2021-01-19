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

namespace TianChengEntranceSyncService.Redis
{
    public class MemberRedis : Redis
    {
        public static readonly string KEY_MOREDIAN_MEMBER = "ENTRANCE:MOREDIANMEMBER:{0}";
        /// <summary>
        /// 获取人员信息
        /// </summary>
        /// <param name="mobile">人员手机号</param>
        /// <returns></returns>
        public static MoredianMember GetMoredianMember(string mobile)
        {
            #region 先从Redis取值
            if (mRedis.KeyExists(string.Format(KEY_MOREDIAN_MEMBER, mobile)))
            {
                string json = mRedis.StringGet(string.Format(KEY_MOREDIAN_MEMBER, mobile));
                try
                {
                    MoredianMember moredianMember = JsonConvert.DeserializeObject<MoredianMember>(json);
                    // 检查是否存在OrgId以及OrgAuthKey这2个重要参数
                    if (null != moredianMember && 0 != moredianMember.memberId)
                    {
                        return moredianMember;
                    }
                }
                catch (Exception)
                {
                }
            }
            #endregion
            return UpdateMoredianMember(mobile);
        }

        public static MoredianMember UpdateMoredianMember(string mobile)
        {
            #region 不存在的情况下去数据库查询
            using (IDbConnection conn = new SqlConnection(Config.EntranceConnectionStr))
            {
                dynamic info = conn.QueryFirstOrDefault("SELECT Mobile,MemberId,Face FROM Tb_HSPR_Entrance_Member WITH(NOLOCK) WHERE Mobile = @Mobile", new { Mobile = mobile });
                if (null == info)
                {
                    return null;
                }
                MoredianMember moredianMember = new MoredianMember
                {
                    memberId = Convert.ToInt64(info.MemberId),
                    mobile = Convert.ToString(info.Mobile),
                    showFace = Convert.ToString(info.Face),
                };
                // 刷新缓存值
                mRedis.StringSet(string.Format(KEY_MOREDIAN_MEMBER, mobile), JsonConvert.SerializeObject(moredianMember));
                return moredianMember;
            }
            #endregion
        }
    }
}
