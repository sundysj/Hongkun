using Aop.Api;
using Aop.Api.Model;
using Newtonsoft.Json;
using System.Threading.Tasks;
using TianChengEntranceSyncService.Attribute;

namespace TianChengEntranceSyncService.同步任务
{
    /// <summary>
    /// 同步主任务，控制同步流程
    /// </summary>
    public class EntranceSyncWork
    {
        /// <summary>
        /// 异步进行
        /// </summary>
        public static void Run()
        {
            Task.Run(() =>
            {
                try
                {
                    /* 设备激活通过人工执行（通常执行一次就行）数据库存储设备id和设备sn设备表记录楼栋信息 */
                    /* 1.初始化组织机构*/
                    /* 2.按照规则初始化群组结构*/
                    /* 2.1需要先判断是否存在成员，存在成员的话，根据成员手机号，查询WChat2020的Tb_User_Bind表绑定关系，根据绑定关系进行分类，聚合楼栋单元信息，进行创建群组以及权限分配*/
                    /* 2.2群组创建完成后，根据设备表信息，将设备与群组绑定*/

                    #region 先检查是否创建了机构，如果当前账号未创建机构，就新建一个，默认使用第一个机构的信息数据（机构信息等同于天问系统的公司级别信息）
                    MoredianOrg moredianOrg = SyncOrgWork.Run();
                    // 如果没返回orgid，代表数据有问题，不进行后续的同步
                    if(null == moredianOrg || moredianOrg.orgId <= 0)
                    {
                        Log.WriteLog($"SyncOrgWork错误，返回的机构信息有误：{JsonConvert.SerializeObject(moredianOrg)}");
                        return;
                    }
                    #endregion

                    #region 同步设备信息
                    SyncDeviceWork.Run(moredianOrg);
                    #endregion

                    #region 同步群组信息
                    SyncGroupWork.Run(moredianOrg);
                    #endregion

                    #region 同步成员信息
                    SyncMemberWork.Run(moredianOrg);
                    #endregion
                }
                catch (SyncWorkException ex)
                {
                    // 内部会往外部抛异常，由外层统一接收处理
                    Log.WriteLog(ex.Message);
                }
            });
        }
    }
}
