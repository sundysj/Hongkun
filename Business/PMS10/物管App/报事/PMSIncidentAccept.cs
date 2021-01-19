using Aop.Api.Domain;
using Business.PMS10.物管App.报事.Enum;
using Business.PMS10.物管App.报事.Models;
using Common;
using Common.Extenions;
using Dapper;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using static Dapper.SqlMapper;

namespace Business
{
    public partial class PMSIncidentAccept : PubInfo
    {
        /// <summary>
        /// 物资详情
        /// </summary>
        class MaterialInfo
        {
            public string Id { get; set; }
            public string StorageId { get; set; }
            public string MaterialId { get; set; }
            public string PurchaseDetailId { get; set; }
            public string PurchasePlanDetailid { get; set; }
            public double PurchasePrice { get; set; }
            public double Price { get; set; }
            public double Taxes { get; set; }
            public double NoTaxPrice { get; set; }
            public double Quantity { get; set; }
            public double Amount { get; set; }
            public double NoTaxAmount { get; set; }
            public string CheckOut { get; set; }
            public string WareHouseId { get; set; }
            public string SupplierId { get; set; }
            public string State { get; set; }
            public string TypeCode { get; set; }
            public string Sort { get; set; }
            public string Num { get; set; }
            public string Name { get; set; }
            public string Spell { get; set; }
            public string Property { get; set; }
            public string Unit { get; set; }
            public string Brand { get; set; }
            public string Specification { get; set; }
            public string Color { get; set; }
            public string OriginPlace { get; set; }
            public string WarrantyPeriod { get; set; }
            public string Describe { get; set; }
            public string SortCode { get; set; }
            public string UnitName { get; set; }
            public string ColorName { get; set; }
            public string MaterialTypeName { get; set; }
            public string WareHouseName { get; set; }
            public DateTime ReceiptDate { get; set; }
            public string CostCode { get; set; }
            public string ReceiptSign { get; set; }
        }

        public PMSIncidentAccept()
        {
            base.Token = "20190930PMSIncidentAccept";
        }

        public override void Operate(ref Transfer Trans)
        {
            DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
            DataRow Row = dAttributeTable.Rows[0];

            //验证登录
            if (!new Login().isLogin(ref Trans))
                return;

            //防止未捕获异常出现
            try
            {
                switch (Trans.Command)
                {
                    case "GetIncidentBadge":                        // 报事阶段角标
                        Trans.Result = GetIncidentBadge(Row);
                        break;


                    // 报事对象
                    case "GetIncidentRegionals":                    // 获取公区列表
                        Trans.Result = GetIncidentRegionals(Row);
                        break;
                    case "GetIncidentPublicPosition":               // 获取公区方位列表
                        Trans.Result = GetIncidentPublicPosition();
                        break;
                    case "GetIncidentPublicFunction":               // 获取公区功能列表
                        Trans.Result = GetIncidentPublicFunction();
                        break;
                    case "GetIncidentPublicConfig":                 // 公区方位、公区功能是否必填
                        Trans.Result = GetIncidentPublicConfig();
                        break;
                    case "GetIncidentEquipments":                   // 模糊查询报事设备
                        Trans.Result = GetIncidentEquipments(Row);
                        break;


                    // 报事类别
                    case "GetIncidentBigType":                      // 获取报事大类列表
                        Trans.Result = GetIncidentBigType(Row);
                        break;
                    case "GetIncidentSmallType":                    // 获取报事细类列表
                        Trans.Result = GetIncidentSmallType(Row);
                        break;


                    // 报事动作
                    case "IncidentAccept":                          // 受理
                        Trans.Result = IncidentAccept(Row);
                        break;
                    case "IncidentAssign":                          // 派单 & 抢单
                        Trans.Result = IncidentAssign(Row);
                        break;
                    case "IncidentReceive":                         // 接单
                        Trans.Result = IncidentReceive(Row);
                        break;
                    case "IncidentArrive":                          // 到场
                        Trans.Result = IncidentArrive(Row);
                        break;
                    case "IncidentDeal":                            // 处理
                    case "IncidentProcessingSave":
                        Trans.Result = IncidentDeal(Row);
                        break;
                    case "IncidentClose":                           // 正常关闭
                        Trans.Result = IncidentClose(Row);
                        break;
                    case "IncidentCloseReturn":                     // 报事关闭退回
                        Trans.Result = IncidentCloseReturn(Row);
                        break;
                    case "IncidentFollow":                          // 跟进
                        Trans.Result = IncidentFollow(Row);
                        break;
                    case "IncidentTransmit":                        // 转发
                    case "IncidentTranspond":
                        Trans.Result = IncidentTranspond(Row);
                        break;
                    case "IncidentDiscard":                         // 废弃
                        Trans.Result = IncidentDiscard(Row);
                        break;


                    // 报事流程
                    case "IncidentDelayApply":                      // 延期申请
                        Trans.Result = IncidentDelayApply(Row);
                        break;
                    case "IncidentAssistApply":                     // 协助申请
                        Trans.Result = IncidentAssistApply(Row);
                        break;
                    case "IncidentUnnormalCloseApply":              // 非正常关闭申请
                        Trans.Result = IncidentUnnormalCloseApply(Row);
                        break;
                    case "IncidentFreeApply":                       // 免费申请
                        Trans.Result = IncidentFreeApply(Row);
                        break;


                    // 列表筛选
                    case "GetIncidentPoolList":                     // 获取工单池列表
                        Trans.Result = GetIncidentPoolList(Row);
                        break;
                    case "GetIncidentDealList":                     // 获取报事处理列表
                        Trans.Result = GetIncidentDealList(Row);
                        break;
                    case "GetIncidentCloseList":                    // 获取报事关闭列表
                        Trans.Result = GetIncidentCloseList(Row);
                        break;
                    case "GetIncidentAuditList":                    // 获取报事审核列表
                        Trans.Result = GetIncidentAuditList(Row);
                        break;
                    case "GetIncidentWarningList":                  // 获取报事预警列表
                        Trans.Result = GetIncidentWarningList(Row);
                        break;
                    case "IncidentSearch":                          // 报事查询
                        Trans.Result = IncidentSearch(Row);
                        break;
                    case "IncidentSearchMyList":                    // 报事查询我的工单
                        Trans.Result = IncidentSearchMyList(Row);
                        break;


                    // 报事信息
                    case "GetIncidentDetail":                       // 获取报事详情
                        Trans.Result = GetIncidentDetail(Row);
                        break;
                    case "GetIncidentSimpleDetail":                 // 获取报事主要信息
                        Trans.Result = GetIncidentSimpleDetail(Row);
                        break;
                    case "GetIncidentLifeCycle":                    // 获取报事生命周期
                        Trans.Result = GetIncidentLifeCycle(Row);
                        break;


                    // 其他列表
                    case "GetIncidentFollowHistory":                // 获取报事跟进历史
                        Trans.Result = GetIncidentFollowHistory(Row);
                        break;
                    case "GetIncidentTranspondHistory":             // 获取报事转发历史
                        Trans.Result = GetIncidentTranspondHistory(Row);
                        break;
                    case "GetIncidentFees":                         // 获取报事费用信息
                        Trans.Result = GetIncidentFees(Row);
                        break;
                    case "GetIncidentCloseBackList":                // 获取报事关闭退回列表
                        Trans.Result = GetIncidentCloseBackList(Row);
                        break;
                    case "GetIncidentReplyBackList":                // 获取报事回访退回列表
                        Trans.Result = GetIncidentReplyBackList(Row);
                        break;


                    // 其它业务
                    case "SetIncidentFeesNotFree":                  // 设置报事为收费
                        Trans.Result = SetIncidentFeesNotFree(Row);
                        break;
                    case "SaveConfirmFeesImg":                      // 保存确认费用签名图片
                        Trans.Result = SaveConfirmFeesImg(Row);
                        break;
                    case "CanAssign":                               // 当前人员对于该报事类别是否有分配权限
                        Trans.Result = CanAssign(Row);
                        break;
                    case "GetCanDealIncidentUserList":              // 获取可以处理当前报事的责任人或协助人
                        Trans.Result = GetCanDealIncidentUserList(Row);
                        break;
                    case "GetTranspondTargetUser":                  // 报事转发，获取可转发给的责任人
                        Trans.Result = GetTranspondTargetUser(Row);
                        break;
                    case "GetIncidentFeesStandard":                 // 获取报事收费标准
                        Trans.Result = GetIncidentFeesStandard(Row);
                        break;
                    case "GetIncidentWorkingHoursStandard":         // 获取报事工时标准图片
                        Trans.Result = GetIncidentWorkingHoursStandard(Row);
                        break;
                    case "GetIncidentMaterialStandard":             // 获取报事材料标准图片
                        Trans.Result = GetIncidentMaterialStandard(Row);
                        break;
                    case "SaveIncidentMaterialCheckOut":            // 报事领料
                        Trans.Result = SaveIncidentMaterialCheckOut(Row);  
                        break;
                    default:
                        Trans.Result = new ApiResult(false, "未知错误").toJson();
                        break;
                }
            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace + Environment.NewLine + ex.Source);
                Trans.Result = new ApiResult(false, ex.Message + ex.StackTrace).toJson();
            }
        }

        /// <summary>
        /// 报事管控设置
        /// </summary>
        public static Tb_HSPR_IncidentControlSet GetIncidentControlSet(IDbConnection db = null)
        {
            var control = Tb_HSPR_IncidentControlSet.Default;

            try
            {
                var conn = db ?? new SqlConnection(PubConstant.hmWyglConnectionString);
                {
                    var sql = @"SELECT * FROM Tb_HSPR_IncidentControlModelSet";

                    var model = conn.Query<Tb_HSPR_IncidentControlModelSet>(sql).FirstOrDefault();
                    if (model != null)
                    {
                        control.DefaultIndoorIncidentAcceptTypeID = model.TypeCode_Accep;
                        control.DefaultPublicIncidentAcceptTypeID = model.PubilcTypeCode_Accep;
                        control.DefaultNotifyOpenWaterIncidentAcceptTypeID = model.DefaultNotifyOpenWaterIncidentAcceptTypeID;
                        control.DefaultNotifyOpenPowerIncidentAcceptTypeID = model.DefaultNotifyOpenPowerIncidentAcceptTypeID;
                        control.DefaultLetPersonPassIncidentAcceptTypeID = model.DefaultLetPersonPassIncidentAcceptTypeID;
                        control.DefaultLetCarPassIncidentAcceptTypeID = model.DefaultLetCarPassIncidentAcceptTypeID;

                        control.AllowCustomerSelectDealMan = model.IsSelPeople_Accep == "是";
                        control.CustomerSelectDealManMaxOrderLimit = model.Num_Accep;
                        control.AllowCrossRegionWorkOrder = model.IsOrge_Assigned == "是";
                        control.DealManMaxOrderLimit = model.Num_Assigned;
                        control.SystemAssignSelectDealManMaxOrderLimit = model.Num2_Assigned;
                        control.IncidentFeesNeedConfirm = model.IsConfirm_Handle == "是";
                        control.IncidentWarningStatisticsInterval = model.Minute;

                        // 业主端报事，客户选择处理人员在线状态限制
                        switch (model.EmployeeStatus_Accep)
                        {
                            case "不限": control.CustomerSelectDealManOnlineStatus = PMSUserOnlineStatus.All; break;
                            case "离线": control.CustomerSelectDealManOnlineStatus = PMSUserOnlineStatus.Offline; break;
                            case "在线":
                            default: control.CustomerSelectDealManOnlineStatus = PMSUserOnlineStatus.Online; break;
                        }

                        // 系统自动派单，选择处理人员在线状态限制
                        switch (model.EmployeeStatus_Assigned)
                        {
                            case "不限": control.SystemAssignSelectDealManOnlineStatus = PMSUserOnlineStatus.All; break;
                            case "离线": control.CustomerSelectDealManOnlineStatus = PMSUserOnlineStatus.Offline; break;
                            case "在线":
                            default: control.CustomerSelectDealManOnlineStatus = PMSUserOnlineStatus.Online; break;
                        }

                        // 到场、完成选择图片方式
                        switch (model.IsUploadImg_Handle)
                        {
                            case "直接拍照": control.IncidentDealSelectImgMode = PMSImageSourceMode.TakePicture; break;
                            case "相册选择": control.IncidentDealSelectImgMode = PMSImageSourceMode.FromAlbum; break;
                            case "不限":
                            default: control.IncidentDealSelectImgMode = PMSImageSourceMode.TakePicture | PMSImageSourceMode.FromAlbum; break;
                        }

                        // 报事回访限制报事来源
                        switch (model.Source_Reply)
                        {
                            case "客户报事": control.IncidentReplyIncidentSource = PMSIncidentSource.FromCustomer; break;
                            case "自查报事": control.IncidentReplyIncidentSource = PMSIncidentSource.FromEmployee; break;
                            case "全部":
                            default: control.IncidentReplyIncidentSource = PMSIncidentSource.FromCustomer | PMSIncidentSource.FromEmployee; break;
                        }

                        // 报事回访限制报事位置
                        switch (model.Source_Reply)
                        {
                            case "户内": control.IncidentReplyIncidentPlace = PMSIncidentPlace.Indoor; break;
                            case "公区": control.IncidentReplyIncidentPlace = PMSIncidentPlace.Public; break;
                            case "全部":
                            default: control.IncidentReplyIncidentPlace = PMSIncidentPlace.Indoor | PMSIncidentPlace.Public; break;
                        }

                        var tmp = new List<string>();

                        // 报事回访综合满意度评价维度
                        if (model.Checkbox_1 == 1)
                            tmp.Add("非常满意");
                        if (model.Checkbox_2 == 1)
                            tmp.Add("满意");
                        if (model.Checkbox_3 == 1)
                            tmp.Add("一般");
                        if (model.Checkbox_4 == 1)
                            tmp.Add("不满意");
                        if (model.Checkbox_5 == 1)
                            tmp.Add("非常不满意");
                        if (model.Checkbox_6 == 1)
                            tmp.Add("无效评价");

                        control.IncidentComprehensiveEvaluationOptional = string.Join(",", tmp);
                        tmp.Clear();

                        // 报事回访其他评价维度
                        if (model.Checkbox_7 == 1)
                            tmp.Add("服务是否及时");
                        if (model.Checkbox_8 == 1)
                            tmp.Add("问题是否解决");
                        if (model.Checkbox_9 == 1)
                            tmp.Add("言行是否合规");

                        control.IncidentOtherEvaluationOptional = string.Join(",", tmp);
                    }

                    if (db == null)
                    {
                        conn.Dispose();
                    }

                    return control;
                }
            }
            catch (Exception e)
            {
                return control;
            }
        }

        /// <summary>
        /// 获取角标
        /// </summary>
        private string GetIncidentBadge(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return new ApiResult(false, "缺少参数CommID").toJson();
            }
            var CommID = AppGlobal.StrToInt(row["CommID"].ToString());

            // 工单池数量
            int poolNum = 0;
            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                // 2、查询能分派或处理的报事
                var sql = $@"SELECT count(1) AS Count FROM Tb_HSPR_IncidentAccept WHERE CommID=@CommID AND Drclass=1
                            AND IncidentID IN
                            (
                                /*分派岗位、处理岗位*/
                                SELECT IncidentID FROM Tb_HSPR_IncidentAccept
                                WHERE isnull(IsDelete,0)=0 AND isnull(DispType,0)=0 AND CommID=@CommID
                                    AND BigCorpTypeID IN
                                    (
                                        SELECT CorpTypeID FROM Tb_HSPR_IncidentType WHERE CommID=@CommID AND CorpTypeID IN
                                        (
                                            SELECT CorpTypeID FROM Tb_HSPR_IncidentTypeAssignedPost WHERE RoleCode IN(
                                                SELECT SysRoleCode FROM Tb_Sys_Role WHERE RoleCode IN(
                                                SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode=@UserCode))
                                            UNION
                                            SELECT CorpTypeID FROM Tb_HSPR_IncidentTypeProcessPost WHERE RoleCode IN(
                                                SELECT SysRoleCode FROM Tb_Sys_Role WHERE RoleCode IN(
                                                SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode=@UserCode))
                                        )
                                    )
                                UNION
                                /*栋楼管家，设置到楼栋*/
                                SELECT IncidentID FROM Tb_HSPR_IncidentAccept a
                                WHERE isnull(a.IsDelete,0)=0 AND isnull(DispType,0)=0 AND CommID=@CommID
                                    AND (a.IncidentPlace='户内' OR (a.IncidentPlace='公区' AND a.RoomID<>0))
                                    AND a.RoomID IN(SELECT RoomID FROM Tb_HSPR_Room WHERE CommID=@CommID AND BuildSNum IN
                                                    (SELECT BuildSNum FROM Tb_HSPR_BuildHousekeeper WHERE CommID=@CommID AND UserCode=@UserCode))
                                UNION
                                SELECT IncidentID FROM Tb_HSPR_IncidentAccept a
                                WHERE isnull(a.IsDelete,0)=0 AND isnull(DispType,0)=0 AND CommID=@CommID
                                    AND (a.IncidentPlace='户内' OR (a.IncidentPlace='公区' AND a.RoomID<>0))
                                    AND a.RoomID IN(SELECT RoomID FROM Tb_HSPR_Room
                                                    WHERE CommID=@CommID AND UserCode=@UserCode)
                                UNION
                                /*公区管家*/
                                SELECT IncidentID FROM Tb_HSPR_IncidentAccept a
                                WHERE isnull(a.IsDelete,0)=0 AND isnull(DispType,0)=0 AND CommID=@CommID
                                    AND a.IncidentPlace='公区'
                                    AND a.RegionalID IN(SELECT RegionalID FROM Tb_HSPR_IncidentRegional
                                                        WHERE CommID=@CommID AND UserCode=@UserCode)
                            )";

                poolNum = conn.Query<int>(sql, new
                {
                    CommID = CommID,
                    UserCode = Global_Var.LoginUserCode,
                }).FirstOrDefault();
            }

            // 报事处理数量
            int dealNum = 0;
            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = @"SELECT * FROM Tb_HSPR_IncidentAccept WHERE DrClass=2 AND isnull(IsDelete,0)=0 AND isnull(DealState,0)=0
                            AND CommID=@CommID AND BigCorpTypeID IS NOT NULL
                            AND (DealManCode=@UserCode OR DealManCode IS NULL OR DealManCode='')
                            AND BigCorpTypeID IN
                            (
                                SELECT CorpTypeID FROM Tb_HSPR_IncidentType WHERE CommID=@CommID AND CorpTypeID IN
                                (
                                    SELECT CorpTypeID FROM Tb_HSPR_IncidentTypeAssignedPost WHERE RoleCode IN(
                                        SELECT SysRoleCode FROM Tb_Sys_Role WHERE RoleCode IN(
                                        SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode=@UserCode))
                                    UNION
                                    SELECT CorpTypeID FROM Tb_HSPR_IncidentTypeProcessPost WHERE RoleCode IN(
                                        SELECT SysRoleCode FROM Tb_Sys_Role WHERE RoleCode IN(
                                        SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode=@UserCode))
                                )
                            )";

                int count1 = conn.Query(sql, new { CommID = CommID, UserCode = Global_Var.LoginUserCode }).Count();

                // 查询是处理人或协助人
                sql = @"SELECT * FROM Tb_HSPR_IncidentAccept WHERE DrClass=1 AND isnull(IsDelete,0)=0 AND isnull(DispType,0)=1
                        AND isnull(DealState,0)=0 AND CommID=@CommID
                        AND IncidentID IN
                        (
                            SELECT IncidentID FROM Tb_HSPR_IncidentAccept WHERE isnull(IsDelete,0)=0 AND isnull(DispType,0)=1
                                AND isnull(DealState,0)=0 AND CommID=@CommID AND DealManCode=@UserCode
                            UNION
                            SELECT IncidentID FROM Tb_HSPR_IncidentAccept
                                WHERE isnull(IsDelete,0)=0 AND isnull(DispType,0)=1 AND isnull(DealState,0)=0 AND CommID=@CommID
                                AND IncidentID IN (SELECT IncidentID FROM Tb_HSPR_IncidentAssistApply WHERE AuditState='已审核'
                                AND IID IN(SELECT AssistID FROM Tb_HSPR_IncidentAssistDetail WHERE UserCode=@UserCode))
                        )";

                int count2 = conn.Query(sql, new { CommID = CommID, UserCode = Global_Var.LoginUserCode }).Count();

                dealNum = count1 + count2;
            }

            // 报事关闭数量
            int closeNum = 0;
            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                // 判断是否有报事关闭模块权限
                string sql = @"SELECT count(1) AS Count FROM Tb_Sys_RolePope a LEFT JOIN Tb_Sys_PowerNode b ON a.PNodeCode=b.PNodeCode
                                WHERE b.PNodeName='报事关闭'  AND a.RoleCode IN
                                (
                                    /* 直接是通用岗位 */
                                    SELECT RoleCode FROM Tb_Sys_Role
                                    WHERE RoleCode IN
                                    (
                                        SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode=@UserCode
                                    ) AND SysRoleCode IS NULL

                                    UNION ALL
                                    /* 从具体岗位读通用岗位 */
                                    SELECT SysRoleCode AS RoleCode FROM Tb_Sys_Role
                                    WHERE RoleCode IN
                                    (
                                        SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode=@UserCode
                                    ) AND SysRoleCode IS NOT NULL
                                )";

                if (conn.Query(sql, new { UserCode = Global_Var.LoginUserCode }).First().Count == 0)
                {
                    closeNum = 0;
                }
                else
                {
                    sql = $@"SELECT count(1) AS Count FROM Tb_HSPR_IncidentAccept
                            WHERE IncidentID IN
                            (
                                /*分派岗位*/
                                SELECT IncidentID FROM Tb_HSPR_IncidentAccept
                                    WHERE isnull(IsDelete,0)=0 AND isnull(DealState,0)=1 AND isnull(IsClose,0)=0 AND CommID=@CommID
                                    AND BigCorpTypeID IN (SELECT CorpTypeID FROM Tb_HSPR_IncidentTypeAssignedPost WHERE RoleCode IN
                                        (SELECT SysRoleCode FROM Tb_Sys_Role WHERE RoleCode IN
                                            (SELECT RoleCode FROM view_Sys_User_RoleData_Filter2 WHERE CommID=@CommID AND UserCode=@UserCode)))
                                UNION
                                /*栋楼管家，设置到楼栋*/
                                SELECT IncidentID FROM Tb_HSPR_IncidentAccept a
                                    WHERE isnull(IsDelete,0)=0 AND isnull(DealState,0)=1 AND isnull(IsClose,0)=0 AND CommID=@CommID
                                    AND (a.IncidentPlace='户内' OR (a.IncidentPlace='公区' AND a.RoomID<>0))
                                    AND a.RoomID IN(SELECT RoomID FROM Tb_HSPR_Room WHERE CommID=@CommID AND BuildSNum IN
                                                    (SELECT BuildSNum FROM Tb_HSPR_BuildHousekeeper WHERE CommID=@CommID AND UserCode=@UserCode))
                                UNION
                                SELECT IncidentID FROM Tb_HSPR_IncidentAccept a WHERE isnull(a.IsDelete,0)=0 AND isnull(DealState,0)=1
                                    AND isnull(IsClose,0)=0 AND CommID=@CommID
                                    AND (a.IncidentPlace='户内' OR (a.IncidentPlace='公区' AND a.RoomID<>0))
                                    AND a.RoomID IN(SELECT RoomID FROM Tb_HSPR_Room WHERE CommID=@CommID AND UserCode=@UserCode)
                                UNION
                                /*公区管家*/
                                SELECT IncidentID FROM Tb_HSPR_IncidentAccept a WHERE isnull(a.IsDelete,0)=0 AND isnull(DealState,0)=1
                                    AND isnull(IsClose,0)=0 AND CommID=@CommID
                                    AND a.IncidentPlace='公区'
                                    AND a.RegionalID IN(SELECT RegionalID FROM Tb_HSPR_IncidentRegional
                                                        WHERE CommID=@CommID AND UserCode=@UserCode)
                            )";

                    closeNum = conn.Query<int>(sql, new
                    {
                        CommID = CommID,
                        UserCode = Global_Var.LoginUserCode
                    }).FirstOrDefault();
                }
            }

            // 报事预警
            int warning1 = 0, warning2 = 0, warning3 = 0, warning4 = 0, warning5 = 0;
            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                // 1：已受理未分派，分派超时
                // 2：已分派未接单，接单超时
                // 3：已接单未处理，处理超时
                // 4：已处理未关闭，关闭超时
                // 5：已关闭未回访，回访超时
                string sql = @"SELECT Count(1) AS Count1 FROM Tb_HSPR_IncidentWarningPush WHERE PushUsers LIKE @PushUsers AND IncidentStep=1;
                               SELECT Count(1) AS Count2 FROM Tb_HSPR_IncidentWarningPush WHERE PushUsers LIKE @PushUsers AND IncidentStep=2;
                               SELECT Count(1) AS Count3 FROM Tb_HSPR_IncidentWarningPush WHERE PushUsers LIKE @PushUsers AND IncidentStep=3;
                               SELECT Count(1) AS Count4 FROM Tb_HSPR_IncidentWarningPush WHERE PushUsers LIKE @PushUsers AND IncidentStep=4;
                               SELECT Count(1) AS Count4 FROM Tb_HSPR_IncidentWarningPush WHERE PushUsers LIKE @PushUsers AND IncidentStep=5";

                GridReader gridReader = conn.QueryMultiple(sql, new { PushUsers = string.Format("%{0}%", Global_Var.UserCode) });

                warning1 = gridReader.Read<int>().First();
                warning2 = gridReader.Read<int>().First();
                warning3 = gridReader.Read<int>().First();
                warning4 = gridReader.Read<int>().First();
                //warning5 = gridReader.Read<int>().First();
            }

            //int auditNum = new AppDesktop().报事审核角标(Global_Var.LoginUserCode, null, CommID);

            int auditNum = 0;

            return new ApiResult(true, new
            {
                IncidentPoolCount = poolNum,
                IncidentDealCount = dealNum,
                IncidentCloseCount = closeNum,
                IncidentAuditCount = auditNum,
                IncidentWarning = new
                {
                    WarningCount = warning1 + warning2 + warning3 + warning4 + warning5,
                    AssignWarningCount = warning1,
                    ReceivingWarningCount = warning2,
                    DealWarningCount = warning3,
                    CloseWarningCount = warning4,
                    ReplyWarningCount = warning5
                }
            }).toJson();
        }

        /// <summary>
        /// 获取报事大类列表
        /// </summary>
        private string GetIncidentBigType(DataRow row)
        {
            #region 基础数据校验
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].AsString()))
            {
                return new ApiResult(false, "缺少参数CommID").toJson();
            }
            if (!row.Table.Columns.Contains("DrClass") || string.IsNullOrEmpty(row["DrClass"].AsString()))
            {
                return new ApiResult(false, "缺少参数DrClass").toJson();
            }
            if (!row.Table.Columns.Contains("Duty") || string.IsNullOrEmpty(row["Duty"].AsString()))
            {
                return new ApiResult(false, "缺少参数Duty").toJson();
            }
            if (!row.Table.Columns.Contains("IncidentPlace") || string.IsNullOrEmpty(row["IncidentPlace"].AsString()))
            {
                return new ApiResult(false, "缺少参数IncidentPlace").toJson();
            }

            var commID = AppGlobal.StrToInt(row["CommID"].AsString());
            int drClass = AppGlobal.StrToInt(row["DrClass"].AsString());
            int isTousu = -1;
            var duty = row["Duty"].AsString();
            var incidentPlace = row["IncidentPlace"].AsString();

            if (row.Table.Columns.Contains("IsTousu") && !string.IsNullOrEmpty(row["IsTousu"].AsString()))
            {
                isTousu = AppGlobal.StrToInt(row["IsTousu"].AsString());
            }
            #endregion

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var dt = conn.ExecuteReader("Proc_HSPR_IncidentType_GetAllNodes_NewComm_Big",
                         new { CommID = commID, ClassID = drClass, CostName = "", IsEnabled = 1, IncidentPlace = incidentPlace, Duty = duty },
                         null, null, CommandType.StoredProcedure).ToDataSet().Tables[0];

                var list = new List<Dictionary<string, object>>();
                foreach (DataRow item in dt.Rows)
                {
                    if (isTousu != -1 && item["IsTousu"] != null && isTousu.ToString() != item["IsTousu"].ToString())
                    {
                        continue;
                    }

                    // 添加数据
                    var dic = new Dictionary<string, object>();
                    dic.Add("CorpTypeID", item["CorpTypeID"]);
                    dic.Add("TypeID", item["TypeID"]);
                    dic.Add("TypeCode", item["TypeCode"]);
                    dic.Add("TypeName", item["TypeName"]);
                    dic.Add("RatedWorkHour", item["RatedWorkHour"]);
                    dic.Add("KPIRatio", item["KPIRatio"]);
                    dic.Add("DealLimit", item["DealLimit"]);
                    dic.Add("DealLimit2", item["DealLimit2"]);
                    dic.Add("IsTousu", item["IsTousu"]);

                    list.Add(dic);
                }

                return new ApiResult(true, list).toJson();
            }
        }

        /// <summary>
        /// 获取报事细类列表
        /// </summary>
        private string GetIncidentSmallType(DataRow row)
        {
            #region 基础数据校验
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].AsString()))
            {
                return new ApiResult(false, "缺少参数CommID").toJson();
            }
            if (!row.Table.Columns.Contains("DrClass") || string.IsNullOrEmpty(row["DrClass"].AsString()))
            {
                return new ApiResult(false, "缺少参数DrClass").toJson();
            }
            if (!row.Table.Columns.Contains("Duty") || string.IsNullOrEmpty(row["Duty"].AsString()))
            {
                return new ApiResult(false, "缺少参数Duty").toJson();
            }
            if (!row.Table.Columns.Contains("TypeCode") || string.IsNullOrEmpty(row["TypeCode"].AsString()))
            {
                return new ApiResult(false, "缺少参数TypeCode").toJson();
            }
            if (!row.Table.Columns.Contains("IncidentPlace") || string.IsNullOrEmpty(row["IncidentPlace"].AsString()))
            {
                return new ApiResult(false, "缺少参数IncidentPlace").toJson();
            }

            var commID = AppGlobal.StrToInt(row["CommID"].AsString());
            int drClass = AppGlobal.StrToInt(row["DrClass"].AsString());
            int isTousu = -1;
            var duty = row["Duty"].AsString();
            var typeCode = row["TypeCode"].AsString();
            var incidentPlace = row["IncidentPlace"].AsString();

            if (row.Table.Columns.Contains("IsTousu") && !string.IsNullOrEmpty(row["IsTousu"].AsString()))
            {
                isTousu = AppGlobal.StrToInt(row["IsTousu"].AsString());
            }

            #endregion

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var data = conn.Query("Proc_HSPR_IncidentType_GetSmallClass_Phone",
                    new
                    {
                        CommID = commID,
                        ClassID = drClass,
                        IncidentPlace = incidentPlace,
                        Duty = duty,
                        TypeCode = typeCode,
                        IsTousu = isTousu
                    }, null, true, null, CommandType.StoredProcedure);

                // 修复可能存在的问题数据
                if (data.Count() == 0)
                {
                    var sql = @"UPDATE Tb_HSPR_IncidentType SET IncidentPlace='户内,公区' WHERE isnull(IncidentPlace,'')='' OR IncidentPlace='NULL';
                                UPDATE Tb_HSPR_CorpIncidentType SET IncidentPlace='户内,公区' WHERE isnull(IncidentPlace,'')='' OR IncidentPlace='NULL';";

                    conn.Execute(sql);

                    data = conn.Query("Proc_HSPR_IncidentType_GetSmallClass_Phone",
                    new
                    {
                        CommID = commID,
                        ClassID = drClass,
                        IncidentPlace = incidentPlace,
                        Duty = duty,
                        TypeCode = typeCode,
                        IsTousu = isTousu
                    }, null, true, null, CommandType.StoredProcedure);
                }

                return new ApiResult(true, data).toJson();
            }
        }

        /// <summary>
        /// 设置报事为收费
        /// </summary>
        private string SetIncidentFeesNotFree(DataRow row)
        {
            if (!row.Table.Columns.Contains("IncidentID") || string.IsNullOrEmpty(row["IncidentID"].AsString()))//报事ID
            {
                return new ApiResult(false, "缺少参数IncidentID").toJson();
            }

            var incidentId = AppGlobal.StrToLong(row["IncidentID"].ToString());

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = $@"UPDATE Tb_HSPR_IncidentAccept SET IsFee=1 WHERE IncidentID=@IncidentID";
                var count = conn.Execute(sql, new { IncidentID = incidentId });

                return new ApiResult(true, "设置成功").toJson();
            }
        }

        /// <summary>
        /// 保存报事费用业主确认签字图片
        /// </summary>
        private string SaveConfirmFeesImg(DataRow row)
        {
            if (!row.Table.Columns.Contains("IncidentID") || string.IsNullOrEmpty(row["IncidentID"].AsString()))
            {
                return new ApiResult(false, "缺少参数IncidentID").toJson();
            }
            if (!row.Table.Columns.Contains("ImgUrl") || string.IsNullOrEmpty(row["ImgUrl"].AsString()))
            {
                return new ApiResult(false, "缺少参数ImgUrl").toJson();
            }
            var incidentId = AppGlobal.StrToLong(row["IncidentID"].ToString());
            var ImgUrl = row["ImgUrl"].ToString();

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = @"UPDATE Tb_HSPR_IncidentAccept SET SignatoryConfirmImg=@ImgUrl WHERE IncidentID=@IncidentID";

                conn.Execute(sql, new { ImgUrl = ImgUrl, IncidentID = incidentId });

                return JSONHelper.FromString(true, "保存成功");
            }
        }

        /// <summary>
        /// 当前人员对于该报事类别是否有分配权限
        /// </summary>
        private string CanAssign(DataRow row)
        {
            #region 基础数据校验
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].AsString()))
            {
                return new ApiResult(false, "缺少参数CommID").toJson();
            }
            if (!row.Table.Columns.Contains("CorpTypeID") || string.IsNullOrEmpty(row["CorpTypeID"].AsString()))
            {
                return new ApiResult(false, "缺少参数CorpTypeID").toJson();
            }

            var commID = AppGlobal.StrToInt(row["CommID"].AsString());
            var corpTypeID = AppGlobal.StrToLong(row["CorpTypeID"].ToString());
            var roomID = 0L;
            var regionID = 0L;
            var userCode = Global_Var.UserCode;

            if (row.Table.Columns.Contains("RegionID") && !string.IsNullOrEmpty(row["RegionID"].AsString()))
            {
                regionID = AppGlobal.StrToLong(row["RegionID"].ToString());
            }
            if (row.Table.Columns.Contains("RoomID") && !string.IsNullOrEmpty(row["RoomID"].AsString()))
            {
                roomID = AppGlobal.StrToLong(row["RoomID"].ToString());
            }

            #endregion

            var canAssign = CanAssign(commID, corpTypeID, roomID, regionID, Global_Var.UserCode);

            return new ApiResult(true, Convert.ToInt32(canAssign)).toJson();
        }

        /// <summary>
        /// 是否能分派
        /// </summary>
        private bool CanAssign(int commId, long corpTypeId, long roomId, long reginalId, string usercode, IDbConnection db = null)
        {
            var conn = db ?? new SqlConnection(PubConstant.hmWyglConnectionString);

            try
            {
                var sql = "";

                // 公区报事，查询公区管家
                if (reginalId > 0)
                {
                    sql = @"SELECT count(*) FROM Tb_HSPR_RegionHousekeeper WHERE UserCode=@UserCode AND RegionID=@RegionID";
                    if (conn.Query<int>(sql, new { UserCode = usercode, RegionID = reginalId }).FirstOrDefault() > 0)
                    {
                        return true;
                    }
                }
                else
                {
                    // 户内管家
                    sql = @"SELECT count(*) FROM 
                            (
                                SELECT a.UserCode FROM Tb_HSPR_BuildHousekeeper a
                                LEFT JOIN Tb_HSPR_Room b ON a.CommID=b.CommID AND a.BuildSNum=b.BuildSNum
                                WHERE a.UserCode=@UserCode
                                UNION
                                SELECT UserCode FROM Tb_HSPR_RoomHousekeeper b WHERE UserCode=@UserCode AND RoomID=@RoomID
                                UNION
                                SELECT UserCode FROM Tb_HSPR_Room c WHERE UserCode=@UserCode AND RoomID=@RoomID
                            ) AS t";
                    if (conn.Query<int>(sql, new { UserCode = usercode, RoomID = roomId }).FirstOrDefault() > 0)
                    {
                        return true;
                    }
                }

                // 报事类别分派权限
                sql = @"SELECT COUNT(*) FROM Tb_HSPR_IncidentTypeAssignedPost a
                        LEFT JOIN Tb_HSPR_IncidentType b ON a.CorpTypeID=b.CorpTypeID
                        WHERE a.CorpTypeID=@CorpTypeID AND b.CommID=@CommID AND RoleCode IN
                        (
                            /* 直接是通用岗位 */
                            SELECT RoleCode FROM Tb_Sys_Role
                            WHERE RoleCode IN
                            (
                                SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode=@UserCode
                            ) AND SysRoleCode IS NULL

                            UNION ALL
                            /* 从具体岗位读通用岗位 */
                            SELECT SysRoleCode AS RoleCode FROM Tb_Sys_Role
                            WHERE RoleCode IN
                            (
                                SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode=@UserCode
                            ) AND SysRoleCode IS NOT NULL
                        )";

                if (conn.Query<int>(sql, new { CorpTypeID = corpTypeId, CommID = commId, UserCode = usercode }).FirstOrDefault() > 0)
                {
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                if (db == null)
                {
                    conn.Dispose();
                }
            }
        }

        /// <summary>
        /// 是否能抢单
        /// </summary>
        private bool CanSnatch(int commId, long corpTypeId, string usercode, IDbConnection db = null)
        {
            var conn = db ?? new SqlConnection(PubConstant.hmWyglConnectionString);

            try
            {
                var sql = @"SELECT COUNT(*) FROM Tb_HSPR_IncidentTypeProcessPost a
                            LEFT JOIN Tb_HSPR_IncidentType b ON a.CorpTypeID=b.CorpTypeID
                            WHERE a.CorpTypeID=@CorpTypeID AND b.CommID=@CommID AND RoleCode IN
                            (
                                /* 直接是通用岗位 */
                                SELECT RoleCode FROM Tb_Sys_Role
                                WHERE RoleCode IN
                                (
                                    SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode=@UserCode
                                ) AND SysRoleCode IS NULL

                                UNION ALL
                                /* 从具体岗位读通用岗位 */
                                SELECT SysRoleCode AS RoleCode FROM Tb_Sys_Role
                                WHERE RoleCode IN
                                (
                                    SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode=@UserCode
                                ) AND SysRoleCode IS NOT NULL
                            )";

                if (conn.Query<int>(sql, new { CorpTypeID = corpTypeId, CommID = commId, UserCode = usercode }).FirstOrDefault() > 0)
                {
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                if (db == null)
                {
                    conn.Dispose();
                }
            }
        }

        /// <summary>
        /// 是否能处理该报事
        /// </summary>
        private bool CanDeal(long incidentId, string usercode, IDbConnection db = null)
        {
            var conn = db ?? new SqlConnection(PubConstant.hmWyglConnectionString);

            try
            {
                var sql = "";
                var incidentInfo = GetIncidentInfo(incidentId);
                if (incidentInfo.DrClass == 1)
                {
                    // 书面
                    sql = @"SELECT DealManCode AS UserCode FROM Tb_HSPR_IncidentAccept WHERE IncidentID=@IncidentID AND DealManCode=@UserCode
                            UNION ALL
                            SELECT UserCode FROM Tb_HSPR_IncidentAssistDetail
                            WHERE AssistID IN
                            (
                                SELECT IID FROM Tb_HSPR_IncidentAssistApply WHERE IncidentID=@IncidentID AND AuditState='已审核'
                            ) AND UserCode=@UserCode";
                }
                else
                {
                    // 口派
                    sql = @"SELECT RoleCode AS UserCode FROM Tb_HSPR_IncidentTypeProcessPost
                            WHERE CorpTypeID=@CorpTypeID
                            AND RoleCode IN
                            (
                                /* 直接是通用岗位 */
                                SELECT RoleCode FROM Tb_Sys_Role
                                WHERE RoleCode IN
                                (
                                    SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode=@UserCode
                                ) AND SysRoleCode IS NULL

                                UNION ALL
                                /* 从具体岗位读通用岗位 */
                                SELECT SysRoleCode AS RoleCode FROM Tb_Sys_Role
                                WHERE RoleCode IN
                                (
                                    SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode=@UserCode
                                ) AND SysRoleCode IS NOT NULL
                            )
                            UNION ALL
                            SELECT DealManCode AS UserCode FROM Tb_HSPR_IncidentAccept WHERE IncidentID=@IncidentID AND DealManCode=@UserCode";
                }

                if (conn.Query<string>(sql, new { IncidentID = incidentId, CorpTypeID = incidentInfo.BigCorpTypeID, UserCode = usercode }).Count() > 0)
                {
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                if (db == null)
                {
                    conn.Dispose();
                }
            }
        }

        /// <summary>
        /// 获取可以处理当前报事的责任人或协助人
        /// <summary>
        private string GetCanDealIncidentUserList(DataRow row)
        {
            #region 基础数据校验
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].AsString()))
            {
                return new ApiResult(false, "缺少参数CommID").toJson();
            }

            if (!row.Table.Columns.Contains("CorpTypeID") || string.IsNullOrEmpty(row["CorpTypeID"].AsString()))
            {
                return new ApiResult(false, "缺少参数DrClass").toJson();
            }

            var commId = AppGlobal.StrToInt(row["CommID"].AsString());
            var corpTypeId = AppGlobal.StrToLong(row["CorpTypeID"].AsString());
            #endregion

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = "SELECT * FROM Tb_HSPR_IncidentTypeProcessPost WHERE CorpTypeID=@CorpTypeID;";
                var count = conn.Query(sql, new { CorpTypeID = corpTypeId }).Count();
                if (count > 0)
                {
                    sql = @"SELECT DISTINCT UserCode,CommID,UserName,EmployeeProfession,EmployeeStar,EmployeeStatus,
                                dbo.funGetUserSysRoleNamesByCommID(UserCode, CommID) AS RoleName,
                                (
                                    SELECT count(1) FROM Tb_HSPR_IncidentAccept 
                                    WHERE DealManCode=UserCode AND isnull(DealState,0)=0 AND IsDelete=0
                                ) AS IncidentCount,
                                '' AS ClockLocation,'0' AS ClockOnlineState
                            FROM View_HSPR_IncidentTypeProcessPostUser 
                            WHERE CommID=@CommID AND CorpTypeID=@CorpTypeID;";
                }
                else
                {
                    sql = @"SELECT DISTINCT UserCode,CommID,UserName,EmployeeProfession,EmployeeStar,EmployeeStatus,
                                dbo.funGetUserSysRoleNamesByCommID(UserCode, CommID) AS RoleName,
                                (
                                    SELECT count(1) FROM Tb_HSPR_IncidentAccept 
                                    WHERE DealManCode=UserCode AND isnull(DealState,0)=0
                                ) AS IncidentCount,
                                '' AS ClockLocation,'0' AS ClockOnlineState
                            FROM View_HSPR_IncidentTypeProcessPostUserAll 
                            WHERE CommID=@CommID;";
                }

                var data = conn.Query(sql, new { CommID = commId, CorpTypeID = corpTypeId }).ToList();

                sql = @"SELECT ClockLocation,convert(varchar(1),ClockOnlineState) AS ClockOnlineState FROM Tb_HSPR_AppOnlineClock 
                        WHERE UserCode=@UserCode ORDER BY ClockTime DESC;";


                // 隆泰-获取处理人当前位置
                var lastLocationSql = @"SELECT top 1 LastLocation FROM Tb_HSPR_AppRealTimeLocation 
                                        WHERE UserCode=@UserCode ORDER BY LocationTime DESC";

                // 是否有实时定位表
                var hasLastLocation = false;
                if (conn.Query<long>(@"SELECT isnull(object_id(N'Tb_HSPR_AppRealTimeLocation',N'U'),0);").FirstOrDefault() != 0)
                {
                    hasLastLocation = true;
                }

                data.Foreach(obj =>
                {
                    var location = conn.Query(sql, new { UserCode = obj.UserCode }).FirstOrDefault();

                    obj.ClockLocation = location == null ? null : location.ClockLocation;
                    obj.ClockOnlineState = location == null ? "0" : location.ClockOnlineState;

                    // 有定位表，才去读取数据
                    if (hasLastLocation)
                    {
                        var lastLocation = conn.Query(lastLocationSql, new { UserCode = obj.UserCode }).FirstOrDefault();
                        obj.LastLocation = lastLocation == null ? "" : lastLocation.LastLocation;
                    }
                });

                var list = data.OrderByDescending(obj => obj.ClockOnlineState);
                return new ApiResult(true, list).toJson();
            }
        }

        /// <summary>
        /// 报事转发，获取可转发给的责任人
        /// </summary>
        private string GetTranspondTargetUser(DataRow row)
        {
            #region 基础数据校验
            if (!row.Table.Columns.Contains("IncidentID") || string.IsNullOrEmpty(row["IncidentID"].AsString()))
            {
                return new ApiResult(false, "缺少参数IncidentID").toJson();
            }
            if (!row.Table.Columns.Contains("CorpTypeID") || string.IsNullOrEmpty(row["CorpTypeID"].AsString()))
            {
                return new ApiResult(false, "缺少参数CorpTypeID").toJson();
            }

            var incidentID = AppGlobal.StrToLong(row["IncidentID"].AsString());
            var corpTypeID = AppGlobal.StrToLong(row["CorpTypeID"].AsString());

            #endregion 基础数据校验

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var incidentInfo = conn.Query<PMSIncidentAcceptModel>(@"SELECT * FROM Tb_HSPR_IncidentAccept WHERE IncidentID=@IncidentID;",
                       new { IncidentID = incidentID }).FirstOrDefault();

                if (incidentInfo == null || incidentInfo.IsDelete == 1)
                    return JSONHelper.FromString(false, $"该报事已被{incidentInfo?.DeleteMan}废弃");


                // 判断当前人是否是管家
                var isHousekeeper = false;
                var sql = "";
                if (incidentInfo.IncidentPlace == "户内")
                {
                    sql = @"/* 房屋管家 */
                            SELECT UserCode FROM Tb_HSPR_Room WHERE RoomID=@RoomID

                            UNION ALL
                            /* 房屋管家 */
                            SELECT UserCode FROM Tb_HSPR_RoomHousekeeper WHERE RoomID=@RoomID

                            UNION ALL
                            /* 楼栋管家 */
                            SELECT UserCode FROM Tb_HSPR_BuildHousekeeper 
                            WHERE CommID=@CommID AND BuildSNum=(SELECT BuildSNum FROM Tb_HSPR_Room WHERE RoomID=@RoomID)";

                    var users = conn.Query<string>(sql, new { CommID = incidentInfo.CommID, RoomID = incidentInfo.RoomID });
                    if (users.Count() > 0)
                    {
                        isHousekeeper = true;
                    }
                }
                else
                {
                    sql = @"/* 公区管家 */
                            SELECT UserCode FROM Tb_HSPR_IncidentRegional 
                            WHERE CommID=@CommID AND RegionalID=@RegionalID AND UserCode=@UserCode;";

                    var users = conn.Query<string>(sql, new
                    {
                        CommID = incidentInfo.CommID,
                        RegionalID = incidentInfo.RegionalID,
                        UserCode = Global_Var.LoginUserCode
                    });
                    if (users.Count() > 0)
                    {
                        isHousekeeper = true;
                    }
                }

                // 判断当前人是否有分派权限
                sql = @"SELECT UserCode FROM Tb_Sys_UserRole 
                        WHERE UserCode=@UserCode AND RoleCode IN
                        (
                            SELECT RoleCode '通用岗位下具体岗位' FROM Tb_Sys_Role 
                            WHERE SysRoleCode IN
	                        (
                                SELECT RoleCode AS '可分派通用岗位' FROM Tb_HSPR_IncidentTypeAssignedPost WHERE CorpTypeID=@CorpTypeID
                            )
                        )";
                var canAssign = conn.Query(sql, new { UserCode = Global_Var.LoginUserCode, CorpTypeID = corpTypeID }).Count() > 0;

                // 管家
                var str1 = @"SELECT UserCode,CommID,UserName,EmployeeProfession,EmployeeStar,EmployeeStatus,'#楼栋管家#' AS Tip,
                                dbo.funGetUserSysRoleNamesByCommID(UserCode, CommID) AS RoleName,
                                (
                                    SELECT convert(VARCHAR(10), count(*)) FROM Tb_HSPR_IncidentAccept
                                    WHERE DealManCode=UserCode AND isnull(IsDelete,0)=0 AND isnull(DealState,0)=0
                                ) AS IncidentCount,
	                            (
                                    SELECT TOP 1 ClockLocation FROM Tb_HSPR_AppOnlineClock x 
		                            WHERE x.UserCode=a.UserCode AND isnull(x.IsDelete,0)=0 ORDER BY LastLocationUpdateTime DESC
                                ) AS ClockLocation,
	                            convert(varchar(1),isnull(
                                (
                                    SELECT TOP 1 ClockOnlineState FROM Tb_HSPR_AppOnlineClock x 
		                            WHERE x.UserCode=a.UserCode AND isnull(x.IsDelete,0)=0 ORDER BY LastLocationUpdateTime DESC
                                ),0)) AS ClockOnlineState
                            FROM Tb_Sys_User a
                            WHERE isnull(IsDelete,0)=0 AND UserCode IN
                            (
                                SELECT UserCode FROM Tb_HSPR_BuildHousekeeper WHERE CommID=@CommID AND BuildSNum=
                                (SELECT BuildSNum FROM Tb_HSPR_Room WHERE RoomID=@RoomID)
                            )

                            UNION ALL
                            SELECT UserCode,CommID,UserName,EmployeeProfession,EmployeeStar,EmployeeStatus,'#公区管家#' AS Tip,
                                dbo.funGetUserSysRoleNamesByCommID(UserCode, CommID) AS RoleName,
                                (
                                    SELECT convert(VARCHAR(10), count(*)) FROM Tb_HSPR_IncidentAccept
                                    WHERE DealManCode=UserCode AND isnull(IsDelete,0)=0 AND isnull(DealState,0)=0
                                ) AS IncidentCount,
	                            (
                                    SELECT TOP 1 ClockLocation FROM Tb_HSPR_AppOnlineClock x 
		                            WHERE x.UserCode=a.UserCode AND isnull(x.IsDelete,0)=0 ORDER BY LastLocationUpdateTime DESC
                                ) AS ClockLocation,
	                            convert(varchar(1),isnull(
                                (
                                    SELECT TOP 1 ClockOnlineState FROM Tb_HSPR_AppOnlineClock x 
		                            WHERE x.UserCode=a.UserCode AND isnull(x.IsDelete,0)=0 ORDER BY LastLocationUpdateTime DESC
                                ),0)) AS ClockOnlineState
                            FROM Tb_Sys_User a
                            WHERE isnull(IsDelete,0)=0 AND UserCode IN
                            (
                                SELECT UserCode FROM Tb_HSPR_IncidentRegional WHERE CommID=@CommID AND RegionalID=@RegionalID
                            )

                            UNION ALL
                            SELECT UserCode,CommID,UserName,EmployeeProfession,EmployeeStar,EmployeeStatus,'#房屋管家#' AS Tip,
                                dbo.funGetUserSysRoleNamesByCommID(UserCode, CommID) AS RoleName,
                                (
                                    SELECT convert(VARCHAR(10), count(*)) FROM Tb_HSPR_IncidentAccept
                                    WHERE DealManCode=UserCode AND isnull(IsDelete,0)=0 AND isnull(DealState,0)=0
                                ) AS IncidentCount,
	                            (
                                    SELECT TOP 1 ClockLocation FROM Tb_HSPR_AppOnlineClock x 
		                            WHERE x.UserCode=a.UserCode AND isnull(x.IsDelete,0)=0 ORDER BY LastLocationUpdateTime DESC
                                ) AS ClockLocation,
	                            convert(varchar(1),isnull(
                                (
                                    SELECT TOP 1 ClockOnlineState FROM Tb_HSPR_AppOnlineClock x 
		                            WHERE x.UserCode=a.UserCode AND isnull(x.IsDelete,0)=0 ORDER BY LastLocationUpdateTime DESC
                                ),0)) AS ClockOnlineState
                            FROM Tb_Sys_User a
                            WHERE isnull(IsDelete,0)=0 AND UserCode IN
                            (
                                SELECT UserCode FROM Tb_HSPR_Room WHERE RoomID=@RoomID
                                UNION
                                SELECT UserCode FROM Tb_HSPR_RoomHousekeeper WHERE RoomID=@RoomID
                            )";

                // 分派岗位
                var str2 = @"SELECT DISTINCT UserCode,CommID,UserName,EmployeeProfession,EmployeeStar,EmployeeStatus,'#分派岗位#' AS Tip,RoleName,
                                (
                                    SELECT convert(VARCHAR(10), count(*)) FROM Tb_HSPR_IncidentAccept
                                    WHERE DealManCode=UserCode AND isnull(IsDelete,0)=0 AND isnull(DealState,0)=0
                                ) AS IncidentCount,
	                            (
                                    SELECT TOP 1 ClockLocation FROM Tb_HSPR_AppOnlineClock x 
		                            WHERE x.UserCode=a.UserCode AND isnull(x.IsDelete,0)=0 ORDER BY LastLocationUpdateTime DESC
                                ) AS ClockLocation,
	                            convert(varchar(1),isnull(
                                (
                                    SELECT TOP 1 ClockOnlineState FROM Tb_HSPR_AppOnlineClock x 
		                            WHERE x.UserCode=a.UserCode AND isnull(x.IsDelete,0)=0 ORDER BY LastLocationUpdateTime DESC
                                ),0)) AS ClockOnlineState
                            FROM View_HSPR_IncidentTypeAssignedPostUser a
                            WHERE CommID=@CommID AND CorpTypeID=@CorpTypeID";

                // 处理岗位
                var str3 = @"SELECT DISTINCT UserCode,CommID,UserName,EmployeeProfession,EmployeeStar,EmployeeStatus,'#处理岗位#' AS Tip,RoleName,
                                (
                                    SELECT convert(VARCHAR(10), count(*)) FROM Tb_HSPR_IncidentAccept
                                        WHERE DealManCode=UserCode AND isnull(IsDelete,0)=0 AND isnull(DealState,0)=0
                                ) AS IncidentCount,
	                            (
                                    SELECT TOP 1 ClockLocation FROM Tb_HSPR_AppOnlineClock x 
		                            WHERE x.UserCode=a.UserCode AND isnull(x.IsDelete,0)=0 ORDER BY LastLocationUpdateTime DESC
                                ) AS ClockLocation,
	                            convert(varchar(1),isnull(
                                (
                                    SELECT TOP 1 ClockOnlineState FROM Tb_HSPR_AppOnlineClock x 
		                            WHERE x.UserCode=a.UserCode AND isnull(x.IsDelete,0)=0 ORDER BY LastLocationUpdateTime DESC
                                ),0)) AS ClockOnlineState
                            FROM View_HSPR_IncidentTypeProcessPostUser a
                            WHERE CommID=@CommID AND CorpTypeID=@CorpTypeID";

                // 管家、可转给其他分派岗位、当前报事类别下处理人
                if (isHousekeeper)
                {
                    sql = string.Join(" UNION ALL ", str2, str3);
                }
                // 分派岗位，可转给管家、当前报事分派岗位、当前报事类别下处理人
                else if (canAssign)
                {
                    sql = string.Join(" UNION ALL ", str1, str2, str3);
                }
                // 处理岗位，可转给楼管、当前报事类别分派岗位
                else
                {
                    sql = string.Join(" UNION ALL ", str1, str2);
                }

                var data = conn.Query<PMSIncidentSelUserModel>(sql, new
                {
                    CommID = incidentInfo.CommID,
                    RoomID = incidentInfo.RoomID,
                    RegionalID = incidentInfo.RegionalID,
                    IncidentID = incidentID,
                    CorpTypeID = corpTypeID,
                    BigCorpTypeID = incidentInfo.BigCorpTypeID,
                })
                .Where(obj => obj.UserCode != Global_Var.LoginUserCode).ToList();

                var tmp1 = data.FindAll(obj => obj.Tip.Contains("管家"));
                var tmp2 = data.FindAll(obj => obj.Tip.Contains("分派岗位"));
                var tmp3 = data.FindAll(obj => obj.Tip.Contains("处理岗位"));

                foreach (var item in tmp2)
                {
                    var tmp = tmp1.Find(obj => obj.UserCode == item.UserCode);
                    if (tmp == null)
                        tmp1.Add(item);
                }
                foreach (var item in tmp3)
                {
                    var tmp = tmp1.Find(obj => obj.UserCode == item.UserCode);
                    if (tmp == null)
                        tmp1.Add(item);
                }

                tmp1 = tmp1.OrderBy(obj => obj.Tip).ToList();

                sql = @"SELECT ClockLocation,convert(varchar(1),ClockOnlineState) AS ClockOnlineState FROM Tb_HSPR_AppOnlineClock 
                        WHERE UserCode=@UserCode ORDER BY ClockTime DESC;";

                // 隆泰-获取处理人当前位置
                var lastLocationSql = @"SELECT top 1 LastLocation FROM Tb_HSPR_AppRealTimeLocation 
                                        WHERE UserCode=@UserCode ORDER BY LocationTime DESC";

                // 是否有实时定位表
                var hasLastLocation = false;
                if (conn.Query<long>(@"SELECT isnull(object_id(N'Tb_HSPR_AppRealTimeLocation',N'U'),0);").FirstOrDefault() != 0)
                {
                    hasLastLocation = true;
                }
                tmp1.Foreach(obj =>
                {
                    var location = conn.Query(sql, new { UserCode = obj.UserCode }).FirstOrDefault();

                    obj.ClockLocation = location == null ? null : location.ClockLocation;
                    obj.ClockOnlineState = location == null ? "0" : location.ClockOnlineState;


                    // 有定位表，才去读取数据
                    if (hasLastLocation)
                    {
                        var lastLocation = conn.Query(lastLocationSql, new { UserCode = obj.UserCode }).FirstOrDefault();
                        obj.LastLocation = lastLocation == null ? "" : lastLocation.LastLocation;
                    }
                });

                var list = tmp1.OrderByDescending(obj => obj.ClockOnlineState);

                return new ApiResult(true, list).toJson();
            }
        }

        /// <summary>
        /// 获取报事收费标准图片
        /// </summary>
        private string GetIncidentFeesStandard(DataRow row)
        {
            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                string sql = "SELECT Type,Name,ImageUrl FROM  Tb_HSPR_IncidentStandardUpload WHERE IsDelete=0";
                var data = conn.QueryMultiple(sql).Read();
                return new ApiResult(true, data).toJson();
            }
        }

        /// <summary>
        /// 获取报事工时标准图片
        /// </summary>
        private string GetIncidentWorkingHoursStandard(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return new ApiResult(false, "缺少参数CommID").toJson();
            }

            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var list = conn.Query("SELECT ImageUrl,Name,Memo,Type FROM TB_HSPR_IncidentStandardUpload WHERE Type='工时标准' AND ISNULL(IsDelete,0)=0");
                return new ApiResult(true, list).toJson();
            }
        }

        /// <summary>
        /// 获取报事材料标准图片
        /// </summary>
        private string GetIncidentMaterialStandard(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return new ApiResult(false, "缺少参数CommID").toJson();
            }

            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var list = conn.Query("SELECT ImageUrl,Name,Memo,Type FROM TB_HSPR_IncidentStandardUpload WHERE Type ='材料标准' AND ISNULL(IsDelete,0)=0");
                return new ApiResult(true, list).toJson();
            }

        }

        /// <summary>
        /// 报事领料 
        /// </summary>
        private string SaveIncidentMaterialCheckOut(DataRow Row)
        {

            #region 基础参数校验
            if (!Row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(Row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            string CommID = Row["CommID"].ToString();

            if (!Row.Table.Columns.Contains("WareHouseId") || string.IsNullOrEmpty(Row["WareHouseId"].ToString()))
            {
                return JSONHelper.FromString(false, "仓库ID不能为空");
            }
            string WareHouseId = Row["WareHouseId"].ToString();

            if (!Row.Table.Columns.Contains("CheckOutType") || string.IsNullOrEmpty(Row["CheckOutType"].ToString()))
            {
                return JSONHelper.FromString(false, "出库类型不能为空");
            }
            string CheckOutType = Row["CheckOutType"].ToString();


            string Memo = "";
            if (Row.Table.Columns.Contains("Memo") && !string.IsNullOrEmpty(Row["Memo"].ToString()))
            {
                Memo = Row["Memo"].ToString();
            }
            if (!Row.Table.Columns.Contains("IncidentNum") || string.IsNullOrEmpty(Row["IncidentNum"].ToString()))
            {
                return JSONHelper.FromString(false, "报事编号不能为空");
            }
            string IncidentNum = Row["IncidentNum"].ToString();

            string UseDepName = "";
            if (Row.Table.Columns.Contains("UseDepName") && !string.IsNullOrEmpty(Row["UseDepName"].ToString()))
            {
                UseDepName = Row["UseDepName"].ToString();
            }
            string UseDepCode = "";
            if (Row.Table.Columns.Contains("UseDepCode") && !string.IsNullOrEmpty(Row["UseDepCode"].ToString()))
            {
                UseDepCode = Row["UseDepCode"].ToString();
            }

            string UseUserName = "";
            if (Row.Table.Columns.Contains("UseUserName") && !string.IsNullOrEmpty(Row["UseUserName"].ToString()))
            {
                UseUserName = Row["UseUserName"].ToString();
            }

            if (!Row.Table.Columns.Contains("Purpose") || string.IsNullOrEmpty(Row["Purpose"].ToString()))
            {
                return JSONHelper.FromString(false, "领料用途不能为空");
            }
            string Purpose = Row["Purpose"].ToString();

            string UserReason = "";
            if (Row.Table.Columns.Contains("UserReason") && !string.IsNullOrEmpty(Row["UserReason"].ToString()))
            {
                UserReason = Row["UserReason"].ToString();
            }

            string UsePosition = "";
            if (Row.Table.Columns.Contains("UsePosition") && !string.IsNullOrEmpty(Row["UsePosition"].ToString()))
            {
                UserReason = Row["UsePosition"].ToString();
            }

            // 只有华南城才有费用归属
            string FeesBelong = null;
            if (Global_Var.LoginCorpID == "1975")
            {
                if (!Row.Table.Columns.Contains("FeesBelong") || string.IsNullOrEmpty(Row["FeesBelong"].ToString()))
                {
                    return JSONHelper.FromString(false, "费用归属不能为空");
                }
                FeesBelong = Row["FeesBelong"].ToString();
            }

            if (Row.Table.Columns.Contains("DutyPerson") && !string.IsNullOrEmpty(Row["DutyPerson"].ToString()))
            {
                UserReason = Row["DutyPerson"].ToString();
            }

            if (!Row.Table.Columns.Contains("IncidentID") || string.IsNullOrEmpty(Row["IncidentID"].ToString()))
            {
                return JSONHelper.FromString(false, "报事ID不能为空");
            }
            string IncidentID = Row["IncidentID"].ToString();

            //if (!Row.Table.Columns.Contains("WorkFlowTypeId") || string.IsNullOrEmpty(Row["WorkFlowTypeId"].ToString()))
            //{
            //    return JSONHelper.FromString(false, "审批流程不能为空");
            //}
            //string WorkFlowTypeId = Row["WorkFlowTypeId"].ToString();

            string CheckOutId = Guid.NewGuid().ToString();

            if (!Row.Table.Columns.Contains("CheckOutDetail") || string.IsNullOrEmpty(Row["CheckOutDetail"].ToString()))
            {
                return JSONHelper.FromString(false, "出库物料不能为空(1)");
            }
            DataTable CheckOutDetailDataTable = JSONHelper.JsonToDataTable(Row["CheckOutDetail"].ToString());
            if (null == CheckOutDetailDataTable || CheckOutDetailDataTable.Rows.Count == 0)
            {
                return JSONHelper.FromString(false, "出库物料不能为空(2)");
            }

            DateTime ReceiptDate = DateTime.Now;
            #endregion

            #region 新增出库单
            // 获取入库单号
            string ReceiptSign;
            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                ReceiptSign = conn.Query<string>("Proc_Mt_GetReceiptSign", new { @ReceiptHead = "CK" }, null, true, null, CommandType.StoredProcedure).FirstOrDefault();
            }
            if (string.IsNullOrEmpty(ReceiptSign))
            {
                return new ApiResult(false, "生成出库单号失败,请重试").toJson();
            }

            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {

                string sqlStr = $@"INSERT INTO Tb_Mt_CheckOut([Id], [WareHouseId], [CheckOutType], [ReceiptSign], [ReceiptDate], 
                                    [DepCode], [CheckOutor], [Originator], [IncidentNum], [Memo], [AllocationId], [InventoryId], 
                                    [IsCancel], [CancelDate], [State], [IncidentID], [UseDepName], [UseUserName], [Purpose], 
                                    [UserReason], [UsePosition]) 
                                VALUES (@Id, @WareHouseId, @CheckOutType, @ReceiptSign, @ReceiptDate, @DepCode, @CheckOutor, 
                                    @Originator, @IncidentNum, @Memo, @AllocationId, @InventoryId, @IsCancel, @CancelDate, 
                                    @State, @IncidentID, @UseDepName, @UseUserName, @Purpose, @UserReason, @UsePosition);";

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Id", CheckOutId);
                parameters.Add("WareHouseId", WareHouseId);
                parameters.Add("CheckOutType", CheckOutType);
                parameters.Add("ReceiptSign", ReceiptSign);
                parameters.Add("ReceiptDate", ReceiptDate);
                parameters.Add("DepCode", "");
                parameters.Add("CheckOutor", "");
                parameters.Add("Originator", Global_Var.LoginUserCode);
                parameters.Add("IncidentNum", IncidentNum);
                parameters.Add("Memo", Memo);
                parameters.Add("AllocationId", "");
                parameters.Add("InventoryId", "");
                parameters.Add("IsCancel", "");
                parameters.Add("CancelDate", "");
                parameters.Add("State", "未启动");
                parameters.Add("IncidentID", IncidentID);
                parameters.Add("UseDepName", UseDepName);
                parameters.Add("UseUserName", UseUserName);
                parameters.Add("Purpose", Purpose);
                parameters.Add("UserReason", UserReason);
                parameters.Add("UsePosition", UsePosition);


                // 禅道需求-9585
                // 判断数据库是否有UseDepNameAll字段
                var sql = @"SELECT isnull(col_length('Tb_Mt_CheckOut','UseDepNameAll'),0)";
                var length = conn.Query<int>(sql).FirstOrDefault();
                var useDepNameAll = "";
                if (length > 0)
                {
                    sql = $@"SELECT isnull(DepName,'') AS DepName FROM Tb_Sys_Department WHERE DepCode IN
                            (
                                SELECT ParentId FROM Tb_Sys_Department WHERE DepCode='{UseDepCode}'
                            )
                            AND isnull(IsDelete,0)=0";
                    useDepNameAll = conn.Query<string>(sql).FirstOrDefault();
                    parameters.Add("UseDepNameAll", useDepNameAll);


                    sqlStr = $@"INSERT INTO Tb_Mt_CheckOut([Id], [WareHouseId], [CheckOutType], [ReceiptSign], [ReceiptDate], 
                                    [DepCode], [CheckOutor], [Originator], [IncidentNum], [Memo], [AllocationId], [InventoryId], 
                                    [IsCancel], [CancelDate], [State], [IncidentID], [UseDepName], [UseUserName], [Purpose], 
                                    [UserReason], [UsePosition], [UseDepNameAll]) 
                                VALUES (@Id, @WareHouseId, @CheckOutType, @ReceiptSign, @ReceiptDate, @DepCode, @CheckOutor, 
                                    @Originator, @IncidentNum, @Memo, @AllocationId, @InventoryId, @IsCancel, @CancelDate, 
                                    @State, @IncidentID, @UseDepName, @UseUserName, @Purpose, @UserReason, @UsePosition, @UseDepNameAll);";
                }

                int count = conn.Execute(sqlStr, parameters, null, null, CommandType.Text);
                if (count == 0)
                {
                    return new ApiResult(false, "新增出库单失败,请重试").toJson();
                }
            }
            #endregion

            #region 保存物料明细
            #region 删除此盘点单已有的明细
            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                // 乐观模式,不考虑删除失败的情况
                conn.Execute("DELETE Tb_Mt_CheckOutDetail WHERE CheckOutId = @CheckOutId", new { CheckOutId = CheckOutId }, null, null, CommandType.Text);
            }
            #endregion

            #region 保存出库物料明细
            {
                List<DynamicParameters> parameterList = new List<DynamicParameters>();
                double useCount;
                DynamicParameters parameters;
                foreach (DataRow item in CheckOutDetailDataTable.Rows)
                {
                    try
                    {
                        useCount = Double.Parse(item["Count"].ToString());
                    }
                    catch (Exception)
                    {
                        useCount = 0d;
                    }
                    // 获取出库Sql参数
                    parameters = createCheckOutDetailParameter(item["Id"].ToString(), CheckOutId, useCount, Purpose, UseDepName, UseUserName, UsePosition, Memo);
                    if (null != parameters)
                    {
                        parameterList.Add(parameters);
                    }
                }
                // 如果构建的参数数量和添加的物资数量不一致
                // 代表某种物资库存不足或者无法出库
                // 返回失败
                if (parameterList.Count != CheckOutDetailDataTable.Rows.Count)
                {
                    return new ApiResult(false, "有物资由于库存不足或其他原因无法出库,请重试").toJson();
                }
                string sql = "INSERT INTO [Tb_Mt_CheckOutDetail]([Id], [CheckOutId], [MaterialId], [Price], [Quantity], [Amount], [StorageDetailId], [Purpose], [UseDepName], [UseUserName], [UsePosition], [Memo], [CostCode]) VALUES (@Id, @CheckOutId, @MaterialId, @Price, @Quantity, @Amount, @StorageDetailId, @Purpose, @UseDepName, @UseUserName, @UsePosition, @Memo, @CostCode)";
                foreach (DynamicParameters item in parameterList)
                {
                    using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
                    {
                        // 乐观模式,不考虑Sql添加失败的原因,后续根据需求调整
                        conn.Execute(sql, item, null, null, CommandType.Text);
                    }
                }
            }

            #endregion
            #endregion

            string BussId = CheckOutId;
            string InstanceId;
            if (Row.Table.Columns.Contains("InstanceId") && !string.IsNullOrEmpty(Row["InstanceId"].ToString()))
            {
                InstanceId = Row["InstanceId"].ToString();

                string BussListID = ""; // 流程实例id 为Tb_WorkFlow_BussList的主键id

                SqlParameter[] parameters = {
                    new SqlParameter("@BussID", SqlDbType.VarChar,50),
                    new SqlParameter("@InstanceId", SqlDbType.VarChar,50),
                    new SqlParameter("@UserCode", SqlDbType.VarChar,50),
                    new SqlParameter("@WorkFLowInFoID", SqlDbType.Int)
                };
                parameters[0].Value = BussId;
                parameters[1].Value = InstanceId;
                parameters[2].Value = Global_Var.LoginUserCode;
                parameters[3].Direction = ParameterDirection.Output;
                DbHelperSQL.RunProcedure("Proc_WorkFlow_SaveWorkAllData", parameters, "RetDataSet");
                BussListID = parameters[3].Value.ToString();
                //new BussListWorkFlow_HNC().StartWorkFlow(BussListID, "0015", BussId);
            }



            #region 保存业务审批单
            //using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString)) {
            //    DynamicParameters parameters = new DynamicParameters();
            //    parameters.Add("Id", DBNull.Value, DbType.String, ParameterDirection.Output);
            //    parameters.Add("BType", "物资出库");
            //    parameters.Add("BussId", BussId);
            //    parameters.Add("ClassName", "CheckOutCallBack");
            //    parameters.Add("DocumentUrl", DBNull.Value);
            //    parameters.Add("IsDelete", DBNull.Value);
            //    parameters.Add("Memo", "");
            //    parameters.Add("Description", DBNull.Value);
            //    parameters.Add("OriginatorCode", Global_Var.LoginUserCode);
            //    parameters.Add("Tb_WorkFlow_FlowSort_InfoId", AppGlobal.StrToInt(WorkFlowTypeId));
            //    parameters.Add("TitleName", ReceiptDate.ToString() + "物资出库(" + ReceiptSign + ")");
            //    parameters.Add("UserCode", Global_Var.LoginUserCode);
            //    parameters.Add("WorkStartDate", DateTime.Now);
            //    conn.Execute("Proc_Tb_WorkFlow_BussList_ADD", parameters, null, null, CommandType.StoredProcedure);
            //    InstanceId = parameters.Get<string>("Id");
            //}
            #endregion

            #region 发起审批单 保存工作流(APP暂时不发起审批流程)
            //// 删除原始数据,有问题也可以由发起人重新发起重跑
            //using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString)) {
            //    conn.ExecuteReader("Proc_WorkFlow_InitSaveWorkFlowData", new { InstanceId = Global_Fun.StrToInt(InstanceId), Tb_Dictionary_InstanceType_DictionaryCode = "0015" }, null, null, CommandType.StoredProcedure);
            //}

            ////乐观模式,不考虑获取返回ID不存在的问题
            //string InstanceInfoId = "";
            //// 更新工作流程实例
            //using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            //{
            //    DynamicParameters parameters = new DynamicParameters();
            //    parameters.Add("InstanceId", Global_Fun.StrToInt(InstanceId));
            //    parameters.Add("Tb_Dictionary_InstanceType_DictionaryCode", "0015");
            //    parameters.Add("InstanceMark", DBNull.Value);
            //    parameters.Add("NoticeMsg", 0);
            //    parameters.Add("NoticeMail", 0);
            //    parameters.Add("NoticePhone", 0);
            //    parameters.Add("NoticeHaveDeal", 0);
            //    parameters.Add("NoticeStartDeal", 0);
            //    parameters.Add("NoticeOtherUser", 0);
            //    parameters.Add("IsComplete", 0);
            //    parameters.Add("IsSuccess", 0);
            //    parameters.Add("CompleteDay", DBNull.Value);
            //    DataTable dt = conn.ExecuteReader("Proc_WorkFlow_Instance_Insert", parameters, null, null, CommandType.StoredProcedure).ToDataSet().Tables[0];
            //    if (null != dt && dt.Rows.Count > 0) {
            //        InstanceInfoId = dt.Rows[0]["IDENTITY"].ToString();
            //    }
            //}

            //// 更新工作流程节点
            ////乐观模式,不考虑获取返回ID不存在的问题
            //string InfoId = "";
            //using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            //{

            //    DynamicParameters parameters = new DynamicParameters();
            //    parameters.Add("Tb_WorkFlow_Instance_InfoId", Global_Fun.StrToInt(InstanceInfoId));
            //    parameters.Add("FlowNodeName", "送审");
            //    parameters.Add("FlowSort", 1);
            //    parameters.Add("TimeOutDay", 0);
            //    parameters.Add("TimeOutDays", 0);
            //    parameters.Add("Tb_Dictionary_NodeOprMethod_DictionaryCode", "0001");
            //    parameters.Add("Tb_Dictionary_NodeOprType_DictionaryCode", "0002");
            //    parameters.Add("JumpFlowSort", 0);
            //    parameters.Add("IsUpdateFlow", 0);
            //    parameters.Add("Tb_Dictionary_OprState_DictionaryCode", "0001");
            //    parameters.Add("IsPrint", 1);
            //    parameters.Add("IsStartUser", 0);
            //    parameters.Add("WorkFlowStartDate", DBNull.Value);
            //    parameters.Add("ReturnNode", 0);
            //    parameters.Add("CheckLevel", 2);
            //    DataTable dt = conn.ExecuteReader("Proc_WorkFlow_FlowNode_Insert", parameters, null, null, CommandType.StoredProcedure).ToDataSet().Tables[0];
            //    if (null != dt && dt.Rows.Count > 0)
            //    {
            //        InfoId = dt.Rows[0]["IDENTITY"].ToString();
            //    }
            //}

            //// 更新工作流程节点部门

            #endregion

            return new ApiResult(true, "完成").toJson();
        }

        /// <summary>
        /// 创建出库详情Parameter
        /// </summary>
        /// <param name="Id">物资ID</param>
        /// <param name="CheckOutId">出库单ID</param>
        /// <param name="UseCount">出库数量</param>
        /// <param name="Purpose">领料用途</param>
        /// <param name="UseDepName">使用部门</param>
        /// <param name="UseUserName">使用人</param>
        /// <param name="UsePosition">使用位置</param>
        /// <param name="Memo">备注</param>
        /// <returns>返回DynamicParameters,为空代表不能出库</returns>
        private DynamicParameters createCheckOutDetailParameter(string Id, string CheckOutId, double UseCount, string Purpose, string UseDepName, string UseUserName, string UsePosition, string Memo)
        {
            MaterialInfo materialInfo = getMaterialInfo(Id);
            if (null == materialInfo)
            {
                return null;
            }
            // 检查是否可出库
            if (!checkCanCheckOut(UseCount, materialInfo))
            {
                return null;
            }

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", Guid.NewGuid().ToString());
            parameters.Add("CheckOutId", CheckOutId);
            parameters.Add("MaterialId", materialInfo.MaterialId);
            parameters.Add("StorageDetailId", materialInfo.Id);
            parameters.Add("Price", materialInfo.Price);
            parameters.Add("Quantity", UseCount);
            parameters.Add("Amount", materialInfo.Amount);
            parameters.Add("Purpose", Purpose);
            parameters.Add("UseDepName", UseDepName);
            parameters.Add("UseUserName", UseUserName);
            parameters.Add("UsePosition", UsePosition);
            parameters.Add("Memo", Memo);
            parameters.Add("CostCode", materialInfo.CostCode);
            return parameters;
        }

        /// <summary>
        /// 获取物资详情
        /// </summary>
        /// <param name="Id">物资ID</param>
        /// <returns></returns>
        private MaterialInfo getMaterialInfo(string Id)
        {
            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                return conn.Query<MaterialInfo>("SELECT * FROM View_Tb_Mt_MaterialCheckOut1001_Filter WHERE Id = @Id", new { Id = Id }, null, true, null, CommandType.Text).FirstOrDefault();
            }
        }

        /// <summary>
        /// 检查是否可出库
        /// </summary>
        /// <returns></returns>
        private bool checkCanCheckOut(double outCount, MaterialInfo materialInfo)
        {
            if (outCount <= 0 || null == materialInfo)
            {
                return false;
            }
            // 如果物资数量小于出库数量,返回false
            if (materialInfo.Quantity < outCount)
            {
                return false;
            }
            #region 检查库存数量是否足够
            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                double count = conn.Query<double>(@"SELECT SUM(ISNULL(Quantity,0)) AS Count FROM Tb_Mt_Store 
                                                    WHERE WareHouseId = @WareHouseId and MaterialId = @MaterialId",
                                                    new
                                                    {
                                                        WareHouseId = materialInfo.WareHouseId,
                                                        MaterialId = materialInfo.MaterialId
                                                    }
                                                    ).FirstOrDefault();
                if (count < outCount)
                {
                    return false;
                }
            }
            #endregion
            #region 检查出库单可出库数量是否足够
            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                double canCheckOutCount = conn.Query<double>(@"SELECT (SUM(ISNULL(Quantity,0)) - SUM(ISNULL(CheckOut,0))) AS Count 
                                                                FROM Tb_Mt_StorageDetail WHERE Id = @Id",
                                                                new { Id = materialInfo.Id }).FirstOrDefault();
                if (canCheckOutCount < outCount)
                {
                    return false;
                }
            }

            #endregion
            #region 检查出库单的出库顺序是否正确
            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                // 查询当前出库的物资所在的入库明细在今天之前的可出库的入库明细
                DataTable dt = conn.ExecuteReader(@"SELECT * FROM View_Tb_Mt_StorageDetail_Filter 
                                                    WHERE ReceiptDate < @ReceiptDate AND MaterialId = @MaterialId 
                                                    AND WareHouseId = @WareHouseId AND ISNULL(Quantity,0)-ISNULL(CheckOut,0)>0 
                                                    and [State]='已审核'", new
                {
                    ReceiptDate = materialInfo.ReceiptDate,
                    MaterialId = materialInfo.MaterialId,
                    WareHouseId = materialInfo.WareHouseId
                }).ToDataSet().Tables[0];
                // 如果之前还有未出完的
                if (null != dt && dt.Rows.Count > 0)
                {
                    // 判断入库明细中是否有该ID
                    // 没有就违反先入先出规则
                    DataRow[] ExistRows = dt.Select("Id = '" + materialInfo.Id + "'");
                    if (null == ExistRows || ExistRows.Length <= 0)
                    {
                        return false;
                    }

                    double canCheckOutCount;
                    try
                    {
                        canCheckOutCount = Double.Parse(ExistRows[0]["Quantity"].ToString());
                    }
                    catch (Exception)
                    {
                        canCheckOutCount = 0;
                    }
                    try
                    {
                        canCheckOutCount = canCheckOutCount - Double.Parse(ExistRows[0]["CheckOut"].ToString());
                    }
                    catch (Exception)
                    {
                        canCheckOutCount -= 0;
                    }
                    if (canCheckOutCount < outCount)
                    {
                        return false;
                    }
                }

            }
            #endregion
            return true;
        }
    }
}
