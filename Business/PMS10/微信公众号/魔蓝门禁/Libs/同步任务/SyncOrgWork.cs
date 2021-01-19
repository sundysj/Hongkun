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
using TianChengEntranceSyncService.Attribute;
using TianChengEntranceSyncService.Redis;

namespace TianChengEntranceSyncService.同步任务
{
    /// <summary>
    /// 机构同步
    /// </summary>
    public class SyncOrgWork
    {
        /// <summary>
        /// 外部串行控制，不需要再开线程
        /// </summary>
        public static MoredianOrg Run()
        {
            try
            {
                /* 1.初始化组织机构*/
                // 从Redis获取组织机构，如果不存在就去数据库查并缓存，如果数据库也不存在，就进行创建然后存储到数据库并缓存
                MoredianOrg moredianOrg = OrgRedis.GetMoredianOrg();
                return moredianOrg;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
