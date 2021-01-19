using App.Model;
using Common;
using Dapper;
using DapperExtensions;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using MobileSoft.Model.Unified;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Business
{
    public class AppPoint : PubInfo
    {
        public AppPoint()
        {
            base.Token = "20181024AppPoint";
        }

        public override void Operate(ref Transfer Trans)
        {
            try
            {
                DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
                DataRow Row = dAttributeTable.Rows[0];
                switch (Trans.Command)
                {
                    case "DailyCheckIn":                                    // 每日签到
                        Trans.Result = DailyCheckIn(Row);
                        break;
                    case "GetPointBalance":                                 // 用户积分余额
                        Trans.Result = GetPointBalance(Row);
                        break;
                    case "GetPointPresentedHistory":                        // 积分赠送历史
                        Trans.Result = GetPointPresentedHistory(Row);
                        break;
                    case "GetPointUseHistory":                              // 积分使用历史
                        Trans.Result = GetPointUseHistory(Row);
                        break;
                    case "GetPointHistory":                                 // 积分明细，【积分赠送历史】+【积分使用历史】
                        Trans.Result = GetPointHistory(Row);
                        break;
                    case "GetPointControlAboutPropertyFees":                // 获取物业缴费相关积分权限控制信息
                        Trans.Result = GetPointControlAboutPropertyFees(Row);
                        break;
                    case "GetPointControlAboutGoodsFees":                   // 获取购物相关积分权限控制信息
                        Trans.Result = GetPointControlAboutGoodsFees(Row);
                        break;
                    case "GetPointDeductionRuleAboutPropertyFees":          // 获取物业缴费积分抵用规则
                        Trans.Result = GetPointDeductionRuleAboutPropertyFees(Row);
                        break;
                    case "GetPointDeductionRuleAboutGoods":                 // 获取购物积分抵用规则
                        Trans.Result = GetPointDeductionRuleAboutGoods(Row);
                        break;
                    case "LockedPointsOnPayPropertyFees":                   // 物业相关费用缴费时锁定积分
                        Trans.Result = LockedPointsOnPayPropertyFees(Row);
                        break;
                    case "PointReturnWithPropertyFees":                     // 退还积分
                        Trans.Result = PointReturnWithPropertyFees(Row);
                        break;
                    default:
                        Trans.Result = new ApiResult(false, "接口不存在").toJson();
                        break;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                Trans.Result = new ApiResult(false, ex.Message + Environment.CommandLine + ex.StackTrace).toJson();
            }
        }

        /// <summary>
        /// 每日签到
        /// </summary>
        private string DailyCheckIn(DataRow row)
        {
            if (!row.Table.Columns.Contains("UserId") || string.IsNullOrEmpty(row["UserId"].ToString()) || row["UserId"].ToString() == "(null)")
            {
                return new ApiResult(false, "UserId不能为空").toJson();
            }
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return new ApiResult(false, "CommunityId不能为空").toJson();
            }
            string UserId = row["UserId"].ToString();
            string CommunityId = row["CommunityId"].ToString();

            Tb_Community tb_Community = GetCommunity(CommunityId);
            if (null == tb_Community)
            {
                return new ApiResult(false, "小区不存在").toJson();
            }
            using (IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                var trans = conn.BeginTransaction();

                // 查询是否已经签到
                if (conn.Query(@"SELECT * FROM Tb_App_DailyCheckIn WHERE UserID=@UserID AND CheckInTime 
                                BETWEEN convert(varchar(10), getDate(), 120) + ' 00:00:00' 
                                AND convert(varchar(10), getDate(), 120) + ' 23:59:59'", new { UserID = UserId }, trans).Count() > 0)
                {
                    return new ApiResult(false, "今日已签到").toJson();
                }

                // 查询该公司是否允许签到奖励
                if (!conn.Query<bool>(@"SELECT AllowDailyCheckInAward FROM Tb_Control_AppPoint 
                        WHERE CorpID=@CorpID AND (CommunityID=@CommunityID OR CommunityID IS NULL) AND IsEnable=1",
                    new { CommunityID = tb_Community.Id, CorpID = tb_Community.CorpID }, trans).FirstOrDefault())
                {
                    trans.Commit();
                    return new ApiResult(false, "功能暂未开通").toJson();
                }

                // 查询该公司是否设置了签到奖励规则
                var controlInfo = conn.Query<Tb_Control_AppPoint_DailyCheckIn>(@"SELECT * FROM Tb_Control_AppPoint_DailyCheckIn 
                        WHERE CorpID=@CorpID AND (CommunityID=@CommunityID OR CommunityID IS NULL) AND IsEnable=1",
                    new { CommunityID = tb_Community.Id, CorpID = tb_Community.CorpID }, trans).FirstOrDefault();

                if (controlInfo == null || controlInfo.IsEnable == false)
                {
                    controlInfo = Tb_Control_AppPoint_DailyCheckIn.DefaultControl;
                }

                try
                {
                    // 用户积分记录
                    if (conn.Query("SELECT * FROM Tb_App_UserPoint WHERE UserID=@UserID", new { UserID = UserId }, trans).Count() == 0)
                    {
                        // 用户积分
                        conn.Execute(@"INSERT INTO Tb_App_UserPoint(UserID, PointBalance) VALUES(@UserID, @PointBalance)",
                            new { UserID = UserId, PointBalance = controlInfo.BaseRewardPoints }, trans);

                        // 签到记录
                        conn.Execute("INSERT INTO Tb_App_DailyCheckIn(UserID, RewardPoints) VALUES(@UserID, @RewardPoints)",
                            new { UserID = UserId, RewardPoints = controlInfo.BaseRewardPoints }, trans);

                        // 赠送历史
                        conn.Execute(@"INSERT INTO Tb_App_Point_PresentedHistory(UserID, PresentedWay, PresentedPoints, PointBalance, Remark) 
                                        VALUES(@UserID, @PresentedWay, @PresentedPoints, @PointBalance, '每日签到')",
                            new
                            {
                                UserID = UserId,
                                PresentedWay = AppPointPresentedWayConverter.GetKey(AppPointPresentedWay.DailyCheckIn),
                                PresentedPoints = controlInfo.BaseRewardPoints,
                                PointBalance = controlInfo.BaseRewardPoints
                            }, trans);

                        trans.Commit();
                        return new ApiResult(true, new { PointBalance = controlInfo.BaseRewardPoints, RewardPoint = controlInfo.BaseRewardPoints }).toJson();
                    }
                    else
                    {
                        // 1、获取用户持续签到天数
                        string sql = $@"DECLARE @now DATETIME=getdate();
                                        SELECT count(*) FROM (
                                            SELECT datediff(DAY, CheckInTime, @now) a,                      /* 签到时间对比今天的差值 */
                                                row_number() OVER (ORDER BY CheckInTime DESC) b             /* 排序字段 */
                                            FROM Tb_App_DailyCheckIn
                                            WHERE UserID=@UserID AND datediff(DAY, CheckInTime, @now)>0     /* 条件排除今天的签到记录 */
                                        ) t WHERE a=b;";
                        int continuousDays = conn.Query<int>(sql, new { UserID = UserId }, trans).FirstOrDefault() + 1;
                        // 赠送方式
                        AppPointPresentedWay way = (continuousDays == 1 ? AppPointPresentedWay.DailyCheckIn : AppPointPresentedWay.ContinuousCheckInReward);

                        // 2、计算奖励的积分数量
                        int rewardPoints = CalcRewardPoints(continuousDays, controlInfo, out int additionalRewardPoints);
                        int pointBalance = conn.Query<int>("SELECT PointBalance FROM Tb_App_UserPoint WHERE UserID=@UserID",
                            new { UserID = UserId }, trans).FirstOrDefault();

                        // 3、签到记录
                        conn.Execute(@"INSERT INTO Tb_App_DailyCheckIn(UserID, RewardPoints, AdditionalRewardPoints, IsAdditionalReward) 
                                        VALUES(@UserID, @RewardPoints, @AdditionalRewardPoints, @IsAdditionalReward)",
                            new
                            {
                                UserID = UserId,
                                RewardPoints = controlInfo.BaseRewardPoints,
                                AdditionalRewardPoints = additionalRewardPoints,
                                IsAdditionalReward = (additionalRewardPoints > 0 ? 1 : 0)
                            }, trans);

                        // 4、插入积分赠送历史
                        conn.Execute(@"INSERT INTO Tb_App_Point_PresentedHistory(UserID, PresentedWay, PresentedPoints, PointBalance, Remark) 
                                        VALUES(@UserID, @PresentedWay, @PresentedPoints, @PointBalance, '每日签到')",
                            new
                            {
                                UserID = UserId,
                                PresentedWay = AppPointPresentedWayConverter.GetKey(way),
                                PresentedPoints = (rewardPoints + additionalRewardPoints),
                                PointBalance = (pointBalance + rewardPoints + additionalRewardPoints)
                            }, trans);

                        // 5、更新积分余额
                        conn.Execute("UPDATE Tb_App_UserPoint SET PointBalance=(PointBalance+@RewardPoint+@AdditionalRewardPoints) WHERE UserID=@UserID",
                            new
                            {
                                RewardPoint = rewardPoints,
                                AdditionalRewardPoints = additionalRewardPoints,
                                UserID = UserId
                            }, trans);

                        trans.Commit();
                        return new ApiResult(true, new { PointBalance = pointBalance, RewardPoint = rewardPoints }).toJson();
                    }
                }
                catch (Exception)
                {
                    trans.Rollback();
                    return new ApiResult(false, "签到异常").toJson();
                }
            }
        }

        /// <summary>
        /// 用户积分余额
        /// </summary>
        private string GetPointBalance(DataRow row)
        {
            if (!row.Table.Columns.Contains("UserId") || string.IsNullOrEmpty(row["UserId"].ToString()))
            {
                return new ApiResult(false, "UserId不能为空").toJson();
            }
            string UserId = row["UserId"].ToString();

            using (IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                int balance = conn.Query<int>("SELECT PointBalance FROM Tb_App_UserPoint WHERE UserID=@UserID",
                    new { UserID = UserId }).FirstOrDefault();

                return new ApiResult(true, new { PointBalance = balance }).toJson();
            }
        }

        /// <summary>
        /// 积分赠送历史
        /// </summary>
        private string GetPointPresentedHistory(DataRow row)
        {
            if (!row.Table.Columns.Contains("UserId") || string.IsNullOrEmpty(row["UserId"].ToString()))
            {
                return new ApiResult(false, "UserId不能为空").toJson();
            }

            string UserId = row["UserId"].ToString();

            int pageSize = 10;
            int pageIndex = 1;

            if (row.Table.Columns.Contains("PageSize") && !string.IsNullOrEmpty(row["PageSize"].ToString()))
            {
                pageSize = AppGlobal.StrToInt(row["PageSize"].ToString());
            }
            if (row.Table.Columns.Contains("PageIndex") && !string.IsNullOrEmpty(row["PageIndex"].ToString()))
            {
                pageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());
            }

            string sql = $@"SELECT a.PresentedTime,a.PresentedPoints,b.Value AS PresentedWay FROM Tb_App_Point_PresentedHistory a
                            LEFT JOIN Tb_Dictionary_Point_PresentedWay b ON a.PresentedWay=b.[Key]
                            WHERE a.UserID='{UserId}'";

            DataTable dataTable = GetList(out int pageCount, out int counts, sql, pageIndex, pageSize, "PresentedTime", 1, "IID",
                PubConstant.UnifiedContionString).Tables[0];

            string result = JSONHelper.FromString(true, dataTable);
            result = result.Insert(result.Length - 1, ",\"PageCount\":" + pageCount);
            return result;
        }

        /// <summary>
        /// 积分使用历史
        /// </summary>
        private string GetPointUseHistory(DataRow row)
        {
            if (!row.Table.Columns.Contains("UserId") || string.IsNullOrEmpty(row["UserId"].ToString()))
            {
                return new ApiResult(false, "UserId不能为空").toJson();
            }

            string UserId = row["UserId"].ToString();

            int pageSize = 10;
            int pageIndex = 1;

            if (row.Table.Columns.Contains("PageSize") && !string.IsNullOrEmpty(row["PageSize"].ToString()))
            {
                pageSize = AppGlobal.StrToInt(row["PageSize"].ToString());
            }
            if (row.Table.Columns.Contains("PageIndex") && !string.IsNullOrEmpty(row["PageIndex"].ToString()))
            {
                pageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());
            }

            string sql = $@"SELECT a.UseTime,a.UsePoints,b.Value AS UseWay FROM Tb_App_Point_UseHistory a
                            LEFT JOIN Tb_Dictionary_Point_UseWay b ON a.UseWay=b.[Key]
                            WHERE a.UserID='{UserId}' AND IsEffect=1";

            DataTable dataTable = GetList(out int pageCount, out int counts, sql, pageIndex, pageSize, "UseTime", 1, "IID",
                PubConstant.UnifiedContionString).Tables[0];

            string result = JSONHelper.FromString(true, dataTable);
            result = result.Insert(result.Length - 1, ",\"PageCount\":" + pageCount);
            return result;
        }

        /// <summary>
        /// 积分明细，【积分赠送历史】+【积分使用历史】
        /// </summary>
        private string GetPointHistory(DataRow row)
        {
            if (!row.Table.Columns.Contains("UserId") || string.IsNullOrEmpty(row["UserId"].ToString()))
            {
                return new ApiResult(false, "UserId不能为空").toJson();
            }

            string UserId = row["UserId"].ToString();

            int pageSize = 10;
            int pageIndex = 1;

            if (row.Table.Columns.Contains("PageSize") && !string.IsNullOrEmpty(row["PageSize"].ToString()))
            {
                pageSize = AppGlobal.StrToInt(row["PageSize"].ToString());
            }
            if (row.Table.Columns.Contains("PageIndex") && !string.IsNullOrEmpty(row["PageIndex"].ToString()))
            {
                pageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());
            }

            string sql = $@"SELECT * FROM (
                            SELECT a.IID,a.PresentedTime AS Time,a.PresentedPoints AS Points,b.Value AS Way FROM Tb_App_Point_PresentedHistory a
                                                        LEFT JOIN Tb_Dictionary_Point_PresentedWay b ON a.PresentedWay=b.[Key]
                                                        WHERE a.UserID='{UserId}'
                            UNION
                            SELECT a.IID,a.UseTime AS Time,(a.UsePoints*-1) AS Points,b.Value AS Way FROM Tb_App_Point_UseHistory a
                                                        LEFT JOIN Tb_Dictionary_Point_UseWay b ON a.UseWay=b.[Key]
                                                        WHERE a.UserID='{UserId}' AND a.IsEffect=1
                            ) as t";

            DataTable dataTable = GetList(out int pageCount, out int counts, sql, pageIndex, pageSize, "Time", 1, "IID",
                PubConstant.UnifiedContionString).Tables[0];

            string result = JSONHelper.FromString(true, dataTable);
            result = result.Insert(result.Length - 1, ",\"PageCount\":" + pageCount);
            return result;
        }

        /// <summary>
        /// 获取物业缴费相关积分权限控制信息
        /// </summary>
        private string GetPointControlAboutPropertyFees(DataRow row)
        {
            if (!row.Table.Columns.Contains("UserId") || string.IsNullOrEmpty(row["UserId"].ToString()))
            {
                return new ApiResult(false, "UserId不能为空").toJson();
            }
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return new ApiResult(false, "CommunityId不能为空").toJson();
            }
            string UserId = row["UserId"].ToString();
            string CommunityId = row["CommunityId"].ToString();

            using (IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                var sql = @"SELECT * FROM Tb_Control_AppPoint WHERE CommunityID LIKE @_CommunityId AND isnull(CommunityID,'')<>'' AND IsEnable=1
                            UNION ALL 
                            SELECT * FROM Tb_Control_AppPoint WHERE CorpID=(SELECT CorpID FROM Tb_Community WHERE Id=@CommunityId)
                                AND isnull(CommunityID,'')='' AND IsEnable=1";
                var controlInfo = conn.Query<Tb_Control_AppPoint>(sql, new { CommunityId = CommunityId, _CommunityId = $"%{CommunityId}%" }).FirstOrDefault();

                if (controlInfo == null || controlInfo.IsEnable == false)
                {
                    controlInfo = Tb_Control_AppPoint.DefaultControl;
                    controlInfo.CommunityID = Guid.Empty.ToString();
                }

                // 用户积分余额
                int balance = conn.Query<int>("SELECT PointBalance FROM Tb_App_UserPoint WHERE UserID=@UserID",
                    new { UserID = UserId }).FirstOrDefault();

                List<string> costSign = new List<string>();
                // 允许抵用物业费
                if (controlInfo.AllowDeductionPropertyFees)
                {
                    costSign.Add("B0001");
                }
                // 允许抵用车位费
                if (controlInfo.AllowDeductionParkingFees)
                {
                    costSign.Add("B0002");
                }

                return new ApiResult(true, new
                {
                    UserPointBalance = balance,
                    AllowDeductionPropertyFees = controlInfo.AllowDeductionPropertyFees,
                    AllowDeductionParkingFees = controlInfo.AllowDeductionParkingFees,
                    AllowDeductionOtherPropertyFees = controlInfo.AllowDeductionOtherPropertyFees,
                    PointExchangeRatio = controlInfo.PointExchangeRatio,
                    SysCostSign = costSign
                }).toJson();
            }
        }

        /// <summary>
        /// 获取购物相关积分权限控制信息
        /// </summary>
        private string GetPointControlAboutGoodsFees(DataRow row)
        {
            if (!row.Table.Columns.Contains("UserId") || string.IsNullOrEmpty(row["UserId"].ToString()))
            {
                return new ApiResult(false, "UserId不能为空").toJson();
            }
            if (!row.Table.Columns.Contains("CorpID") || string.IsNullOrEmpty(row["CorpID"].ToString()))
            {
                return new ApiResult(false, "CorpID不能为空").toJson();
            }
            string UserId = row["UserId"].ToString();
            string CorpID = row["CorpID"].ToString();

            using (IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                var controlInfo = conn.Query<Tb_Control_AppPoint>(@"SELECT * FROM Tb_Control_AppPoint WHERE CorpID=@CorpID AND CommunityID IS NULL AND IsEnable=1",
                    new { CorpID = CorpID }).FirstOrDefault();

                if (controlInfo == null || controlInfo.IsEnable == false)
                {
                    controlInfo = Tb_Control_AppPoint.DefaultControl;
                }

                // 用户积分余额
                int balance = conn.Query<int>("SELECT PointBalance FROM Tb_App_UserPoint WHERE UserID=@UserID",
                    new { UserID = UserId }).FirstOrDefault();

                return new ApiResult(true, new
                {
                    UserPointBalance = balance,
                    AllowDeductionGoodsFees = controlInfo.AllowDeductionGoodsFees,
                    PointExchangeRatio = controlInfo.PointExchangeRatio
                }).toJson();
            }
        }

        /// <summary>
        /// 获取物业缴费积分抵用规则
        /// </summary>
        private string GetPointDeductionRuleAboutPropertyFees(DataRow row)
        {
            if (!row.Table.Columns.Contains("UserId") || string.IsNullOrEmpty(row["UserId"].ToString()))
            {
                return new ApiResult(false, "UserId不能为空").toJson();
            }
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return new ApiResult(false, "CommunityId不能为空").toJson();
            }
            string UserId = row["UserId"].ToString();
            string CommunityId = row["CommunityId"].ToString();

            using (IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                List<string> deductionObject = new List<string>();

                // 读取积分抵用权限设置
                var controlInfo = conn.Query<Tb_Control_AppPoint>(@"SELECT * FROM Tb_Control_AppPoint WHERE CommunityID=@CommunityId",
                    new { CommunityId = CommunityId }).FirstOrDefault();

                if (controlInfo == null || controlInfo.IsEnable == false)
                {
                    controlInfo = Tb_Control_AppPoint.DefaultControl;
                    controlInfo.CommunityID = Guid.Empty.ToString();
                }

                // 允许抵用物业费
                if (controlInfo.AllowDeductionPropertyFees)
                {
                    deductionObject.Add($@"'{AppPointUsableObjectConverter.GetKey(AppPointUsableObject.PropertyFee)}'");
                }
                // 允许抵用车位费
                if (controlInfo.AllowDeductionParkingFees)
                {
                    deductionObject.Add($@"'{AppPointUsableObjectConverter.GetKey(AppPointUsableObject.ParkingFee)}'");
                }

                if (deductionObject.Count == 0)
                {
                    return new ApiResult(false, "暂不支持积分抵用功能").toJson();
                }

                // 用户积分余额
                int balance = conn.Query<int>("SELECT PointBalance FROM Tb_App_UserPoint WHERE UserID=@UserID",
                    new { UserID = UserId }).FirstOrDefault();

                var ruleInfo = conn.Query($@"SELECT IID,ConditionAmount,DiscountsAmount,DeductionObject,b.Remark AS SysCostSign,StartTime,EndTime 
                                        FROM Tb_App_Point_PropertyDeductionRule a LEFT JOIN Tb_Dictionary_Point_UsableObject b
                                        ON a.DeductionObject=b.[Key] 
                                        WHERE CommunityID=@CommunityId AND DeductionObject IN({string.Join(", ", deductionObject) }) 
                                        AND getdate() BETWEEN StartTime AND EndTime AND a.IsDelete=0 ORDER BY DeductionObject,ConditionAmount",
                                  new { CommunityId = CommunityId });

                var list = new List<dynamic>();
                foreach (var item in ruleInfo)
                {
                    var _o = list.Find(obj => obj.DeductionObject == item.DeductionObject);
                    if (_o == null)
                    {
                        _o = new
                        {
                            DeductionObject = item.DeductionObject,
                            SysCostSign = item.SysCostSign,
                            Rules = new List<dynamic>() {
                                new {
                                    ConditionAmount = item.ConditionAmount,
                                    DiscountsAmount = item.DiscountsAmount,
                                    StartTime = item.StartTime,
                                    EndTime = item.EndTime,
                                }
                            }
                        };
                        list.Add(_o);
                    }
                    else
                    {
                        _o.Rules.Add(new
                        {
                            ConditionAmount = item.ConditionAmount,
                            DiscountsAmount = item.DiscountsAmount,
                            StartTime = item.StartTime,
                            EndTime = item.EndTime,
                        });
                    }
                }

                return new ApiResult(true, new
                {
                    UserPointBalance = balance,
                    PointExchangeRatio = controlInfo.PointExchangeRatio,
                    DeductionRules = list
                }).toJson();
            }
        }

        /// <summary>
        /// 获取购物积分抵用规则
        /// </summary>
        private string GetPointDeductionRuleAboutGoods(DataRow row)
        {
            if (!row.Table.Columns.Contains("UserId") || string.IsNullOrEmpty(row["UserId"].ToString()))
            {
                return new ApiResult(false, "UserId不能为空").toJson();
            }
            if (!row.Table.Columns.Contains("CorpID") || string.IsNullOrEmpty(row["CorpID"].ToString()))
            {
                return new ApiResult(false, "CorpID不能为空").toJson();
            }
            string UserId = row["UserId"].ToString();
            string CorpID = row["CorpID"].ToString();

            using (IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                List<string> deductionObject = new List<string>();

                var controlInfo = conn.Query<Tb_Control_AppPoint>(@"SELECT * FROM Tb_Control_AppPoint WHERE CorpID=@CorpID AND CommunityID IS NULL AND IsEnable=1",
                    new { CorpID = CorpID }).FirstOrDefault();

                if (controlInfo == null || controlInfo.IsEnable == false)
                {
                    controlInfo = Tb_Control_AppPoint.DefaultControl;
                    controlInfo.CommunityID = Guid.Empty.ToString();
                }

                // 允许抵用商品
                if (!controlInfo.AllowDeductionGoodsFees)
                {
                    return new ApiResult(true, new object()).toJson();
                }

                // 用户积分余额
                int balance = conn.Query<int>("SELECT PointBalance FROM Tb_App_UserPoint WHERE UserID=@UserID",
                    new { UserID = UserId }).FirstOrDefault();

                var ruleInfo = conn.Query($@"SELECT IID,ConditionAmount,DiscountsAmount,DeductionObject,StartTime,EndTime 
                                            FROM Tb_App_Point_PropertyDeductionRule a LEFT JOIN Tb_Dictionary_Point_UsableObject b
                                            ON a.DeductionObject=b.[Key] 
                                            WHERE CorpID=@CorpID AND CommunityID IS NULL AND DeductionObject=@DeductionObject
                                            AND getdate() BETWEEN StartTime AND EndTime AND a.IsDelete=0 ORDER BY ConditionAmount",
                                            new { CorpID = CorpID, DeductionObject = AppPointUsableObjectConverter.GetKey(AppPointUsableObject.Goods) });

                var list = new List<dynamic>();
                foreach (var item in ruleInfo)
                {
                    var _o = list.Find(obj => obj.DeductionObject == item.DeductionObject);
                    if (_o == null)
                    {
                        _o = new
                        {
                            DeductionObject = item.DeductionObject,
                            Rules = new List<dynamic>() {
                                new {
                                    ConditionAmount = item.ConditionAmount,
                                    DiscountsAmount = item.DiscountsAmount,
                                    StartTime = item.StartTime,
                                    EndTime = item.EndTime,
                                }
                            }
                        };
                        list.Add(_o);
                    }
                    else
                    {
                        _o.Rules.Add(new
                        {
                            ConditionAmount = item.ConditionAmount,
                            DiscountsAmount = item.DiscountsAmount,
                            StartTime = item.StartTime,
                            EndTime = item.EndTime,
                        });
                    }
                }

                return new ApiResult(true, new
                {
                    UserPointBalance = balance,
                    PointExchangeRatio = controlInfo.PointExchangeRatio,
                    DeductionRules = list
                }).toJson();
            }
        }


        /// <summary>
        /// 物业相关费用缴费时锁定积分
        /// </summary>
        private string LockedPointsOnPayPropertyFees(DataRow row)
        {
            if (!row.Table.Columns.Contains("UserId") || string.IsNullOrEmpty(row["UserId"].ToString()))
            {
                return new ApiResult(false, "UserId不能为空").toJson();
            }
            if (!row.Table.Columns.Contains("UsePoints") || string.IsNullOrEmpty(row["UsePoints"].ToString()))
            {
                return new ApiResult(false, "UsePoints不能为空").toJson();
            }

            string UserId = row["UserId"].ToString();
            int UsePoints = AppGlobal.StrToInt(row["UsePoints"].ToString());

            using (IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                var trans = conn.BeginTransaction();

                // 要使用的积分是否大于用户积分余额
                int balance = conn.Query<int>("SELECT PointBalance FROM Tb_App_UserPoint WHERE UserID=@UserID",
                    new { UserID = UserId }, trans).FirstOrDefault();
                if (balance < UsePoints)
                {
                    return new ApiResult(false, "积分余额不足").toJson();
                }

                string useHistoryID = Guid.NewGuid().ToString();
                conn.Execute($@"UPDATE Tb_App_UserPoint SET PointBalance=(PointBalance-@UsePoints) WHERE UserID=@UserID;
                                INSERT INTO Tb_App_Point_UseHistory(IID, UserID, UseWay, UsePoints, PointBalance)
                                    VALUES(@UseHistoryID, @UserID, @UseWay, @UsePoints, @PointBalance);
                                INSERT INTO Tb_App_Point_Locked(UserID, UseHistoryID, LockedPoints) 
                                    VALUES (@UserID, @UseHistoryID, @LockedPoints);",
                new
                {
                    UsePoints = UsePoints,
                    UserID = UserId,
                    UseHistoryID = useHistoryID,
                    UseWay = AppPointUseWayConverter.GetKey(AppPointUseWay.PropertyFeeDeduction),
                    PointBalance = balance - UsePoints,
                    LockedPoints = UsePoints
                }, trans);

                return new ApiResult(true, useHistoryID).toJson();
            }
        }


        /// <summary>
        /// 退还积分
        /// </summary>
        private string PointReturnWithPropertyFees(DataRow row)
        {
            if (!row.Table.Columns.Contains("UserId") || string.IsNullOrEmpty(row["UserId"].ToString()))
            {
                return new ApiResult(false, "UserId不能为空").toJson();
            }
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return new ApiResult(false, "CommunityId不能为空").toJson();
            }
            if (!row.Table.Columns.Contains("Payway") || string.IsNullOrEmpty(row["Payway"].ToString()))
            {
                return new ApiResult(false, "Payway不能为空").toJson();
            }
            if (!row.Table.Columns.Contains("TradeNo") || string.IsNullOrEmpty(row["TradeNo"].ToString()))
            {
                return new ApiResult(false, "TradeNo不能为空").toJson();
            }
            if (!row.Table.Columns.Contains("UseHistoryID") || string.IsNullOrEmpty(row["UseHistoryID"].ToString()))
            {
                return new ApiResult(false, "UseHistoryID不能为空").toJson();
            }
            string UserId = row["UserId"].ToString();
            string CommunityId = row["CommunityId"].ToString();
            string Payway = row["Payway"].ToString();
            string TradeNo = row["TradeNo"].ToString();
            string UseHistoryID = row["UseHistoryID"].ToString();

            using (IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                var trans = conn.BeginTransaction();

                Tb_Community community = conn.Query<Tb_Community>(@"SELECT * FROM Tb_Community WHERE Id=@id OR CommID=@id",
                    new { id = CommunityId }, trans).FirstOrDefault();

                if (community == null)
                {
                    return new ApiResult(false, "未找到小区信息").toJson();
                }

                PubConstant.hmWyglConnectionString = GetConnectionStr(community);

                int orderResult = -99;
                bool canReturn = false;
                using (IDbConnection erpConn = new SqlConnection(PubConstant.hmWyglConnectionString))
                {
                    switch (Payway.ToLower())
                    {
                        case "alipay":
                            orderResult = erpConn.Query<int>(@"SELECT IsSucc FROM Tb_OL_AlipayOrder WHERE out_trade_no=@TradeNo",
                                                                    new { TradeNo = TradeNo }).FirstOrDefault();
                            break;
                        case "wechatpay":
                            orderResult = erpConn.Query<int>(@"SELECT IsSucc FROM Tb_OL_WeixinPayOrder WHERE out_trade_no=@TradeNo",
                                                                    new { TradeNo = TradeNo }).FirstOrDefault();
                            break;
                    }

                    if (orderResult == 0)
                    {
                        canReturn = true;
                    }
                }

                using (IDbConnection erpConn = new SqlConnection(PubConstant.BusinessContionString))
                {
                    switch (Payway.ToLower())
                    {
                        case "alipay_buss":
                        case "wechatpay_buss":
                            orderResult = erpConn.Query<int>(@"SELECT COUNT(1) FROM Tb_Charge_Receipt where OrderId=@OrderId",
                                                                    new { OrderId = TradeNo }).FirstOrDefault();
                            break;
                    }

                    if (orderResult == 1)
                    {
                        canReturn = true;
                    }
                }

                // 可以退积分
                if (canReturn)
                {
                    conn.Execute($@"DECLARE @UsePoints INT=0;
                                        SELECT @UsePoints=UsePoints FROM Tb_App_Point_UseHistory WHERE IID=@UserHistoryID AND IsEffect IS NULL;
                                        IF @UsePoints<>0
                                            BEGIN
                                                UPDATE Tb_App_Point_UseHistory SET IsEffect=0 WHERE IID=@UserHistoryID;
                                                UPDATE Tb_App_UserPoint SET PointBalance=(PointBalance+@UsePoints) WHERE UserID=@UserID;
                                            END",
                                new { UserHistoryID = UseHistoryID, UserID = UserId }, trans);
                    trans.Commit();
                    return new ApiResult(true, "积分退还成功").toJson();
                }
                else
                {
                    return new ApiResult(false, "积分已使用，无法退还").toJson();
                }
            }
        }

        /// <summary>
        /// 计算签到奖励积分数量
        /// </summary>
        private int CalcRewardPoints(int continuousDays, Tb_Control_AppPoint_DailyCheckIn controlInfo, out int additionalRewardPoints)
        {
            additionalRewardPoints = 0;
            switch (AppPointContinuousCheckInRewardModeConverter.Convert(controlInfo.ContinuousCheckInRewardMode))
            {
                case AppPointContinuousCheckInRewardMode.Basal:
                    return controlInfo.BaseRewardPoints;
                case AppPointContinuousCheckInRewardMode.Increase:
                    {
                        int rewardPoint = controlInfo.BaseRewardPoints;
                        if (controlInfo.ContinuousCheckInLimitDays <= 0)
                        {
                            // 持续签到天数不封顶
                            rewardPoint = controlInfo.BaseRewardPoints * continuousDays;
                        }
                        else
                        {
                            if (continuousDays > controlInfo.ContinuousCheckInLimitDays)
                            {
                                // 持续签到天数有封顶限制
                                rewardPoint = controlInfo.BaseRewardPoints * controlInfo.ContinuousCheckInLimitDays;
                            }
                            else
                            {
                                rewardPoint = controlInfo.BaseRewardPoints * continuousDays;
                            }

                            // 设置了额外奖励天数
                            if (controlInfo.ContinuousCheckInAdditionalRewardLimitDays > 0 &&
                                controlInfo.ContinuousCheckInAdditionalRewardLimitDays > controlInfo.ContinuousCheckInLimitDays)
                            {
                                // 判断是否满足额外奖励


                                // 获取上一次额外奖励时间

                            }
                        }
                        return rewardPoint;
                    }
                case AppPointContinuousCheckInRewardMode.Accumulate:        // 暂不支持
                    return controlInfo.BaseRewardPoints;
                case AppPointContinuousCheckInRewardMode.PropertyDefined:   // 暂不支持
                    return controlInfo.BaseRewardPoints;
                default:
                    return controlInfo.BaseRewardPoints;
            }
        }

        /// <summary>
        /// 计算物业类、车位类赠送积分数量
        /// </summary>
        public void CalcPresentedPointForPropertyFees(string communityId, decimal paidPropertyFeeAmount, decimal paidParkingFeeAmount,
            out int propertyRulePresentedPoint, out int parkingRulePresentedPoint)
        {
            propertyRulePresentedPoint = 0;
            parkingRulePresentedPoint = 0;

            using (IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                try
                {
                    string propertyFeeKey = AppPointUsableObjectConverter.GetKey(AppPointUsableObject.PropertyFee);
                    string parkingFeeKey = AppPointUsableObjectConverter.GetKey(AppPointUsableObject.ParkingFee);

                    var rules = conn.Query<Tb_App_Point_PropertyPresentedRule>(@"SELECT * FROM Tb_App_Point_PropertyPresentedRule 
                                        WHERE CommunityID=@CommunityID AND (PresentedObject=@PresentedObject1 OR PresentedObject=@PresentedObject2)
                                        AND IsDelete=0 ORDER BY ConditionAmount",
                                        new
                                        {
                                            CommunityID = communityId,
                                            PresentedObject1 = propertyFeeKey,
                                            PresentedObject2 = parkingFeeKey
                                        });

                    if (rules.Count() > 0)
                    {
                        Tb_App_Point_PropertyPresentedRule propertyRule = null;
                        Tb_App_Point_PropertyPresentedRule parkingRule = null;

                        foreach (var item in rules)
                        {
                            // 物业类
                            if (item.PresentedObject == propertyFeeKey)
                            {
                                if (paidPropertyFeeAmount >= item.ConditionAmount)
                                {
                                    propertyRule = item;
                                }
                            }

                            // 车位类
                            if (item.PresentedObject == parkingFeeKey)
                            {
                                if (paidParkingFeeAmount >= item.ConditionAmount)
                                {
                                    parkingRule = item;
                                }
                            }
                        }

                        if (propertyRule != null)
                        {
                            propertyRulePresentedPoint = propertyRule.PresentedPoints;
                        }

                        if (parkingRule != null)
                        {
                            parkingRulePresentedPoint = parkingRule.PresentedPoints;
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        /// <summary>
        /// 计算商品赠送积分
        /// </summary>
        public int CalcPresentedPointForGoods(int corpId, decimal paidAmount)
        {
            using (IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                try
                {
                    string key = AppPointUsableObjectConverter.GetKey(AppPointUsableObject.Goods);

                    var rules = conn.Query<Tb_App_Point_PropertyPresentedRule>(@"SELECT * FROM Tb_App_Point_PropertyPresentedRule 
                                        WHERE CorpID=@CorpID AND CommunityID IS NULL AND PresentedObject=@PresentedObject AND IsDelete=0 
                                        ORDER BY ConditionAmount,DiscountsAmount",
                                        new
                                        {
                                            CorpID = corpId,
                                            PresentedObject = key
                                        });

                    if (rules.Count() > 0)
                    {
                        Tb_App_Point_PropertyPresentedRule rule = null;

                        foreach (var item in rules)
                        {
                            if (paidAmount >= item.ConditionAmount)
                            {
                                rule = item;
                            }
                        }

                        if (rule != null)
                        {
                            return rule.PresentedPoints;
                        }
                    }

                    return 0;
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// 力帆赠送积分
        /// </summary>
        public int CalcPresentedPointForPrec_lf(int months)
        {
            // 活动截止3月31号
            if (DateTime.Now >= new DateTime(2019, 4, 1, 0, 0, 0))
            {
                return 0;
            }
            var points = 0;

            if (months >= 5 && months < 11)//满6个月送18000
            {
                points = 18000;
            }
            else if (months >= 11 && months < 17)//满12个月送45000
            {
                points = 45000;
            }
            else if (months >= 17 && months < 23)//满18个月送63000
            {
                points = 63000;
            }
            else if (months >= 23)//满24个月送90000
            {
                points = 90000;
            }
            return points;
        }

        /// <summary>
        /// 力帆，欠费缴清，赠送积分
        /// </summary>
        public int CalcPresentedPointForPayAll()
        {
            return 3000;
        }
    }
}
