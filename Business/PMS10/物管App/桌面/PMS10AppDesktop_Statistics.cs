using Dapper;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Business
{
    /// <summary>
    /// PMS10信息桌面统计
    /// </summary>
    public partial class PMS10AppDesktop
    {
        private int _commId;
        private string _usercode;
        private string _organCode;

        /// <summary>
        /// 获取信息桌面统计详情
        /// </summary>
        private string GetAppDesktopStatisticsDetails(DataRow row)
        {
            if (!row.Table.Columns.Contains("Code") || string.IsNullOrEmpty(row["Code"].ToString()))
            {
                return JSONHelper.FromString(false, "缺少桌面代码参数");
            }

            var code = row["Code"].ToString();
            var usercode = Global_Var.LoginUserCode;

            var organCode = "";
            var commId = 0;

            if (row.Table.Columns.Contains("CommID"))
            {
                commId = AppGlobal.StrToInt(row["CommID"].ToString());
            }

            if (row.Table.Columns.Contains("OrganCode"))
            {
                organCode = row["OrganCode"].ToString();

                if (string.IsNullOrEmpty(organCode))
                {
                    organCode = "01";
                }
            }

            return GetResultJson(code, usercode, organCode, commId);
        }

        private string GetResultJson(string code, string usercode, string organCode, int commId)
        {
            switch (code)
            {
                case "999901":  // 项目员工信息桌面
                    return new ApiResult(true, GetStatisticsDetails_999901(usercode, organCode, commId)).toJson();

                case "999902":  // 项目管家信息桌面
                    return new ApiResult(true, GetStatisticsDetails_999902(usercode, organCode, commId)).toJson();

                case "999903":  // 客服主管信息桌面
                    return new ApiResult(true, GetStatisticsDetails_999903(usercode, organCode, commId)).toJson();

                case "999904":  // 工程主管信息桌面
                    return new ApiResult(true, GetStatisticsDetails_999904(usercode, organCode, commId)).toJson();

                case "999905":  // 秩序主管信息桌面
                    return new ApiResult(true, GetStatisticsDetails_999905(usercode, organCode, commId)).toJson();

                case "999906":  // 环境主管信息桌面
                    return new ApiResult(true, GetStatisticsDetails_999906(usercode, organCode, commId)).toJson();

                case "999907":  // 项目经理信息桌面
                    return new ApiResult(true, GetStatisticsDetails_999907(usercode, organCode, commId)).toJson();

                case "999908":  // 管理层级信息桌面
                    return new ApiResult(true, GetStatisticsDetails_999908(usercode, organCode, commId)).toJson();

                default:
                    return new ApiResult(true, new string[] { }).toJson();
            }
        }

        /// <summary>
        /// 刷新信息桌面统计数据
        /// </summary>
        private string RefreshAppDesktopStatistics(DataRow row)
        {
            if (!row.Table.Columns.Contains("Code") || string.IsNullOrEmpty(row["Code"].ToString()))
            {
                return JSONHelper.FromString(false, "桌面代码不能为空");
            }
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区id不能为空");
            }

            var code = row["Code"].ToString();
            var commId = AppGlobal.StrToInt(row["CommID"].ToString());

            if (GetDailyStatisticsState(commId) == false && GetMonthlyStatisticsState(commId) == false)
            {
                return JSONHelper.FromString(false, "数据统计中，请稍后");
            }

            if (code == "999901" || code == "999902")
            {
                GetProjectSynthesis(commId, Global_Var.LoginUserCode);
            }

            var usercode = Global_Var.LoginUserCode;
            var organCode = "01";

            if (row.Table.Columns.Contains("UserCode"))
            {
                usercode = row["UserCode"].ToString();
            }

            if (row.Table.Columns.Contains("CommID"))
            {
                commId = AppGlobal.StrToInt(row["CommID"].ToString());
            }

            if (row.Table.Columns.Contains("OrganCode"))
            {
                organCode = row["OrganCode"].ToString();

                if (string.IsNullOrEmpty(organCode))
                {
                    organCode = "01";
                }
            }

            return GetResultJson(code, usercode, organCode, commId);
        }

        /// <summary>
        /// 日清统计是否完成
        /// </summary>
        private bool GetDailyStatisticsState(int commId, IDbConnection db = null, IDbTransaction trans = null)
        {
            var conn = db ?? new SqlConnection(PubConstant.hmWyglConnectionString);

            try
            {
                var sql = @"SELECT * FROM dbo.Tb_TaskManagement_Schedule 
                            WHERE type='Proc_TaskManagement_DailyDetailTable' AND CommId=@CommId;";

                var data = conn.Query(sql, new { CommID = commId }, trans).FirstOrDefault();
                if (data == null)
                {
                    var connStr = PubConstant.hmWyglConnectionString;
                    // 日清明细
                    Task.Run(() =>
                    {
                        using (var taskConn = new SqlConnection(connStr))
                        {
                            taskConn.Execute("Proc_TaskManagement_DailyDetailTable", new { CommID = commId, TaskType = "", UserCode = "" },
                                null, null, CommandType.StoredProcedure);
                        }
                    });
                    return false;
                }
                else if (data.Schedule?.ToString() != "6")
                {
                    return false;
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                if (db == null)
                    conn.Dispose();
            }
        }

        /// <summary>
        /// 月结统计是否完成
        /// </summary>
        private bool GetMonthlyStatisticsState(int commId, IDbConnection db = null, IDbTransaction trans = null)
        {
            var conn = db ?? new SqlConnection(PubConstant.hmWyglConnectionString);

            try
            {
                var sql = @"SELECT * FROM dbo.Tb_TaskManagement_Schedule 
                            WHERE type='Proc_TaskManagement_DailyDetailTable' AND CommId=@CommId;";

                var data = conn.Query(sql, new { CommID = commId }, trans).FirstOrDefault();
                if (data == null)
                {
                    var connStr = PubConstant.hmWyglConnectionString;
                    // 月清明细
                    Task.Run(() =>
                    {
                        using (var taskConn = new SqlConnection(connStr))
                        {
                            taskConn.Execute("Proc_TaskManagement_DailyDetailTable", new { CommID = commId, TaskType = "", UserCode = "" },
                                null, null, CommandType.StoredProcedure);
                        }
                    });
                    return false;
                }
                else if (data.Schedule?.ToString() != "6")
                {
                    return false;
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                if (db == null)
                    conn.Dispose();
            }
        }

        /// <summary>
        /// 实时统计
        /// </summary>
        private bool GetProjectSynthesis(int commId, string usercode, IDbConnection db = null, IDbTransaction trans = null)
        {
            var conn = db ?? new SqlConnection(PubConstant.hmWyglConnectionString);

            try
            {
                conn.Execute("Proc_ProjectSynthesis", new { CommID = commId, UserCode = usercode },
                               null, null, CommandType.StoredProcedure);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                if (db == null)
                    conn.Dispose();
            }
        }

        #region 信息桌面
        /// <summary>
        /// 项目员工信息桌面
        /// </summary>
        private List<object> GetStatisticsDetails_999901(string usercode, string organcode, int commId,
            IDbConnection db = null, IDbTransaction trans = null)
        {
            var conn = db ?? new SqlConnection(PubConstant.hmWyglConnectionString);

            try
            {
                var sql = @"SELECT count(1) FROM Tb_App_ProjectSynthesis 
                            WHERE UserCode=@UserCode
                            AND convert(varchar(10),Timelimit,120)=convert(varchar(10),getdate(),120);";
                if (conn.Query<int>(sql, new { UserCode = usercode }).FirstOrDefault() == 0)
                {
                    conn.Execute("Proc_ProjectSynthesis", new { CommID = commId, UserCode = usercode },
                                null, null, CommandType.StoredProcedure);
                }

                sql = @"SELECT ThisDayWaitNum FROM dbo.Tb_TaskManagement_DailyDetailTable WHERE CommID=@CommID AND UserCode=@UserCode AND TaskType='合计';
                        SELECT * FROM Tb_App_ProjectSynthesis WHERE UserCode=@UserCode AND TypeName='本日到期任务';
                        SELECT * FROM Tb_App_ProjectSynthesis WHERE UserCode=@UserCode AND TypeName='本日完成任务';";

                var reader = conn.QueryMultiple(sql, new { CommID = commId, UserCode = usercode }, trans);
                var _1 = reader.Read<int>().FirstOrDefault();
                var _2 = reader.Read().Count();
                var _3 = reader.Read().Count();

                return new List<object>()
                {
                    new { Name = "本人今日待办任务数", Value = _1.ToString(), Unit = "条" },
                    new { Name = "本人今日到期任务数", Value = _2.ToString(), Unit = "条" },
                    new { Name = "本人今日完成任务数", Value = _3.ToString(), Unit = "条" }
                };
            }
            catch (Exception)
            {
                return new List<object>()
                {
                    new { Name = "本人今日待办任务数", Value = "0", Unit = "条" },
                    new { Name = "本人今日到期任务数", Value = "0", Unit = "条" },
                    new { Name = "本人今日完成任务数", Value = "0", Unit = "条" }
                };
            }
            finally
            {
                if (db == null)
                    conn.Dispose();
            }
        }

        /// <summary>
        /// 项目管家信息桌面
        /// </summary>
        private List<object> GetStatisticsDetails_999902(string usercode, string organcode, int commId,
            IDbConnection db = null, IDbTransaction trans = null)
        {
            var conn = db ?? new SqlConnection(PubConstant.hmWyglConnectionString);

            try
            {
                if (GetMonthlyStatisticsState(commId, conn, trans) == false)
                {
                    throw new Exception();
                }

                var sql = @"SELECT ThisMonthShouldCompleteNum1 - ThisMonthCompleteNum1
                            FROM Tb_TaskManagement_MonthlyDetailTable WHERE CommID=@CommID AND UserCode=@UserCode AND TaskType='报事工单';
                            SELECT ThisMonthCompleteRate FROM Tb_TaskManagement_MonthlyDetailTable 
                            WHERE CommID=@CommID AND UserCode=@UserCode AND TaskType='物管费往年清欠';
                            SELECT ThisMonthCompleteRate FROM Tb_TaskManagement_MonthlyDetailTable 
                            WHERE CommID=@CommID AND UserCode=@UserCode AND TaskType='物管费本年收款';

                            SELECT ThisDayWaitNum FROM dbo.Tb_TaskManagement_DailyDetailTable WHERE CommID=@CommID AND UserCode=@UserCode AND TaskType='合计';
                            SELECT * FROM Tb_App_ProjectSynthesis WHERE UserCode=@UserCode AND TypeName='本日到期任务'
                            AND convert(nvarchar(10),Timelimit)=convert(nvarchar(10),getdate());
                            SELECT * FROM Tb_App_ProjectSynthesis WHERE UserCode=@UserCode AND TypeName='本日完成任务'
                            AND convert(nvarchar(10),Timelimit)=convert(nvarchar(10),getdate());";

                var reader = conn.QueryMultiple(sql, new { CommID = commId, UserCode = usercode });
                var _1 = reader.Read<int>().FirstOrDefault();
                var _2 = reader.Read<decimal>().FirstOrDefault();
                var _3 = reader.Read<decimal>().FirstOrDefault();
                var _4 = reader.Read<int>().FirstOrDefault();
                var _5 = reader.Read().Count();
                var _6 = reader.Read().Count();

                return new List<object>()
                {
                    new { Name = "服务客户未处理报事", Value = _1.ToString(), Unit = "条" },
                    new { Name = "服务客户累计清欠率", Value = (Math.Floor(_2 * 100) * 0.01m).ToString("f2"), Unit = "%" },
                    new { Name = "服务客户累计收费率", Value = (Math.Floor(_3 * 100) * 0.01m).ToString("f2"), Unit = "%" },
                    new { Name = "本人今日待办任务数", Value = _4.ToString(), Unit = "条" },
                    new { Name = "本人今日到期任务数", Value = _5.ToString(), Unit = "条" },
                    new { Name = "本人今日完成任务数", Value = _6.ToString(), Unit = "条" }
                };
            }
            catch (Exception)
            {
                return new List<object>()
                {
                    new { Name = "服务客户未处理报事", Value = "0", Unit = "条" },
                    new { Name = "服务客户累计清欠率", Value = "0.00", Unit = "%" },
                    new { Name = "服务客户累计收费率", Value = "0.00", Unit = "%" },
                    new { Name = "本人今日待办任务数", Value = "0", Unit = "条" },
                    new { Name = "本人今日到期任务数", Value = "0", Unit = "条" },
                    new { Name = "本人今日完成任务数", Value = "0", Unit = "条" }
                };
            }
            finally
            {
                if (db == null)
                    conn.Dispose();
            }
        }

        /// <summary>
        /// 客服主管信息桌面
        /// </summary>
        private List<object> GetStatisticsDetails_999903(string usercode, string organcode, int commId,
            IDbConnection db = null, IDbTransaction trans = null)
        {
            var conn = db ?? new SqlConnection(PubConstant.hmWyglConnectionString);

            var sql = @"SELECT ComletedRate FROM Tb_TaskManagement_MonthlyTotalTable 
                        WHERE OrganCode=@OrganCode AND len(CommId)=0 AND TaskType='报事工单' AND TaskUnit='全部';
                        SELECT ComletedRate FROM Tb_TaskManagement_MonthlyTotalTable 
                        WHERE OrganCode=@OrganCode AND len(CommId)=0 AND TaskType='客户拜访' AND TaskUnit='任务';
                        SELECT ComletedRate FROM Tb_TaskManagement_MonthlyTotalTable 
                        WHERE OrganCode=@OrganCode AND len(CommId)=0 AND TaskType='公区巡查' AND TaskUnit='任务';
                        SELECT ComletedRate FROM Tb_TaskManagement_MonthlyTotalTable 
                        WHERE OrganCode=@OrganCode AND len(CommId)=0 AND TaskType='空房巡查' AND TaskUnit='任务';
                        SELECT ComletedRate FROM Tb_TaskManagement_MonthlyTotalTable 
                        WHERE OrganCode=@OrganCode AND len(CommId)=0 AND TaskType='物管费往年清欠' AND TaskUnit='金额';
                        SELECT ComletedRate FROM Tb_TaskManagement_MonthlyTotalTable 
                        WHERE OrganCode=@OrganCode AND len(CommId)=0 AND TaskType='物管费本年收款' AND TaskUnit='金额';";

            if (commId != 0)
            {
                sql = @"SELECT ComletedRate FROM Tb_TaskManagement_MonthlyTotalTable WHERE CommId=@CommId AND TaskType='报事工单' AND TaskUnit='全部';
                        SELECT ComletedRate FROM Tb_TaskManagement_MonthlyTotalTable WHERE CommId=@CommId AND TaskType='客户拜访' AND TaskUnit='任务';
                        SELECT ComletedRate FROM Tb_TaskManagement_MonthlyTotalTable WHERE CommId=@CommId AND TaskType='公区巡查' AND TaskUnit='任务';
                        SELECT ComletedRate FROM Tb_TaskManagement_MonthlyTotalTable WHERE CommId=@CommId AND TaskType='空房巡查' AND TaskUnit='任务';
                        SELECT ComletedRate FROM Tb_TaskManagement_MonthlyTotalTable WHERE CommId=@CommId AND TaskType='物管费往年清欠' AND TaskUnit='金额';
                        SELECT ComletedRate FROM Tb_TaskManagement_MonthlyTotalTable WHERE CommId=@CommId AND TaskType='物管费本年收款' AND TaskUnit='金额';";
            }

            try
            {
                var reader = conn.QueryMultiple(sql, new { OrganCode = organcode, CommId = commId });
                var _1 = reader.Read<decimal>().FirstOrDefault();
                var _2 = reader.Read<decimal>().FirstOrDefault();
                var _3 = reader.Read<decimal>().FirstOrDefault();
                var _4 = reader.Read<decimal>().FirstOrDefault();
                var _5 = reader.Read<decimal>().FirstOrDefault();
                var _6 = reader.Read<decimal>().FirstOrDefault();

                return new List<object>()
                {
                    new { Name = "本月报事工单完成率", Value = (Math.Floor(_1 * 100) * 0.01m).ToString("f2"), Unit = "%" },
                    new { Name = "本月客户拜访完成率", Value = (Math.Floor(_2 * 100) * 0.01m).ToString("f2"), Unit = "%" },
                    new { Name = "本月公区巡查完成率", Value = (Math.Floor(_3 * 100) * 0.01m).ToString("f2"), Unit = "%" },
                    new { Name = "本月空房巡查完成率", Value = (Math.Floor(_4 * 100) * 0.01m).ToString("f2"), Unit = "%" },
                    new { Name = "往年欠费累计清欠率", Value = (Math.Floor(_5 * 100) * 0.01m).ToString("f2"), Unit = "%" },
                    new { Name = "本年应收累计收费率", Value = (Math.Floor(_6 * 100) * 0.01m).ToString("f2"), Unit = "%" }
                };
            }
            catch (Exception)
            {
                return new List<object>()
                {
                    new { Name = "本月报事工单完成率", Value = "0.00", Unit = "%" },
                    new { Name = "本月客户拜访完成率", Value = "0.00", Unit = "%" },
                    new { Name = "本月公区巡查完成率", Value = "0.00", Unit = "%" },
                    new { Name = "本月空房巡查完成率", Value = "0.00", Unit = "%" },
                    new { Name = "往年欠费累计清欠率", Value = "0.00", Unit = "%" },
                    new { Name = "本年应收累计收费率", Value = "0.00", Unit = "%" }
                };
            }
            finally
            {
                if (db == null)
                    conn.Dispose();
            }
        }

        /// <summary>
        /// 工程主管信息桌面
        /// </summary>
        private List<object> GetStatisticsDetails_999904(string usercode, string organcode, int commId,
            IDbConnection db = null, IDbTransaction trans = null)
        {
            var conn = db ?? new SqlConnection(PubConstant.hmWyglConnectionString);

            var sql = @"SELECT ComletedRate FROM Tb_TaskManagement_MonthlyTotalTable 
                        WHERE OrganCode=@OrganCode AND len(CommId)=0 AND TaskType='报事工单' AND TaskUnit='全部';
                        SELECT ComletedRate FROM Tb_TaskManagement_MonthlyTotalTable 
                        WHERE OrganCode=@OrganCode AND len(CommId)=0 AND TaskType='设备巡检' AND TaskUnit='任务';
                        SELECT ComletedRate FROM Tb_TaskManagement_MonthlyTotalTable 
                        WHERE OrganCode=@OrganCode AND len(CommId)=0 AND TaskType='设备维保' AND TaskUnit='任务';
                        SELECT ComletedRate FROM Tb_TaskManagement_MonthlyTotalTable 
                        WHERE OrganCode=@OrganCode AND len(CommId)=0 AND TaskType='分户查验' AND TaskUnit='任务';
                        SELECT ComletedRate FROM Tb_TaskManagement_MonthlyTotalTable 
                        WHERE OrganCode=@OrganCode AND len(CommId)=0 AND TaskType='设备故障' AND TaskUnit='故障率';";

            if (commId != 0)
            {
                sql = @"SELECT ComletedRate FROM Tb_TaskManagement_MonthlyTotalTable WHERE CommId=@CommId AND TaskType='报事工单' AND TaskUnit='全部';
                        SELECT ComletedRate FROM Tb_TaskManagement_MonthlyTotalTable WHERE CommId=@CommId AND TaskType='设备巡检' AND TaskUnit='任务';
                        SELECT ComletedRate FROM Tb_TaskManagement_MonthlyTotalTable WHERE CommId=@CommId AND TaskType='设备维保' AND TaskUnit='任务';
                        SELECT ComletedRate FROM Tb_TaskManagement_MonthlyTotalTable WHERE CommId=@CommId AND TaskType='分户查验' AND TaskUnit='任务';
                        SELECT ComletedRate FROM Tb_TaskManagement_MonthlyTotalTable WHERE CommId=@CommId AND TaskType='设备故障' AND TaskUnit='故障率';";
            }

            try
            {
                var reader = conn.QueryMultiple(sql, new { OrganCode = organcode, CommId = commId });
                var _1 = reader.Read<decimal>().FirstOrDefault();
                var _2 = reader.Read<decimal>().FirstOrDefault();
                var _3 = reader.Read<decimal>().FirstOrDefault();
                var _4 = reader.Read<decimal>().FirstOrDefault();
                var _5 = reader.Read<decimal>().FirstOrDefault();

                return new List<object>()
                {
                    new { Name = "本月报事工单完成率", Value = (Math.Floor(_1 * 100) * 0.01m).ToString("f2"), Unit = "%" },
                    new { Name = "本月设备巡检完成率", Value = (Math.Floor(_2 * 100) * 0.01m).ToString("f2"), Unit = "%" },
                    new { Name = "本月设备维保完成率", Value = (Math.Floor(_3 * 100) * 0.01m).ToString("f2"), Unit = "%" },
                    new { Name = "本月分户查验完成率", Value = (Math.Floor(_4 * 100) * 0.01m).ToString("f2"), Unit = "%" },
                    new { Name = "本月设备故障发生率", Value = (Math.Floor(_5 * 100) * 0.01m).ToString("f2"), Unit = "%" }
                };
            }
            catch (Exception)
            {
                return new List<object>()
                {
                    new { Name = "本月报事工单完成率", Value = "0.00", Unit = "%" },
                    new { Name = "本月设备巡检完成率", Value = "0.00", Unit = "%" },
                    new { Name = "本月设备维保完成率", Value = "0.00", Unit = "%" },
                    new { Name = "本月分户查验完成率", Value = "0.00", Unit = "%" },
                    new { Name = "本月设备故障发生率", Value = "0.00", Unit = "%" }
                };
            }
            finally
            {
                if (db == null)
                    conn.Dispose();
            }
        }

        /// <summary>
        /// 秩序主管信息桌面
        /// </summary>
        private List<object> GetStatisticsDetails_999905(string usercode, string organcode, int commId,
            IDbConnection db = null, IDbTransaction trans = null)
        {
            var conn = db ?? new SqlConnection(PubConstant.hmWyglConnectionString);

            var sql = @"SELECT ComletedRate FROM Tb_TaskManagement_MonthlyTotalTable 
                        WHERE OrganCode=@OrganCode AND len(CommId)=0 AND TaskType='安全巡更' AND TaskUnit='任务';
                        SELECT ComletedRate FROM Tb_TaskManagement_MonthlyTotalTable 
                        WHERE OrganCode=@OrganCode AND len(CommId)=0 AND TaskType='消防巡检' AND TaskUnit='任务';";

            if (commId != 0)
            {
                sql = @"SELECT ComletedRate FROM Tb_TaskManagement_MonthlyTotalTable WHERE CommId=@CommId AND TaskType='安全巡更' AND TaskUnit='任务';
                        SELECT ComletedRate FROM Tb_TaskManagement_MonthlyTotalTable WHERE CommId=@CommId AND TaskType='消防巡检' AND TaskUnit='任务';";
            }

            try
            {
                var reader = conn.QueryMultiple(sql, new { OrganCode = organcode, CommId = commId });
                var _1 = reader.Read<decimal>().FirstOrDefault();
                var _2 = reader.Read<decimal>().FirstOrDefault();

                return new List<object>()
                {
                    new { Name = "本月安全巡更完成率", Value = (Math.Floor(_1 * 100) * 0.01m).ToString("f2"), Unit = "%" },
                    new { Name = "本月消防巡检完成率", Value = (Math.Floor(_2 * 100) * 0.01m).ToString("f2"), Unit = "%" }
                };
            }
            catch (Exception)
            {
                return new List<object>()
                {
                    new { Name = "本月安全巡更完成率", Value = "0.00", Unit = "%" },
                    new { Name = "本月消防巡检完成率", Value = "0.00", Unit = "%" }
                };
            }
            finally
            {
                if (db == null)
                    conn.Dispose();
            }
        }

        /// <summary>
        /// 环境主管信息桌面
        /// </summary>
        private List<object> GetStatisticsDetails_999906(string usercode, string organcode, int commId,
            IDbConnection db = null, IDbTransaction trans = null)
        {
            var conn = db ?? new SqlConnection(PubConstant.hmWyglConnectionString);

            var sql = @"SELECT ComletedRate FROM Tb_TaskManagement_MonthlyTotalTable 
                        WHERE OrganCode=@OrganCode AND len(CommId)=0 AND TaskType='环境保洁' AND TaskUnit='任务';
                        SELECT ComletedRate FROM Tb_TaskManagement_MonthlyTotalTable 
                        WHERE OrganCode=@OrganCode AND len(CommId)=0 AND TaskType='绿化养护' AND TaskUnit='任务';
                        SELECT ComletedRate FROM Tb_TaskManagement_MonthlyTotalTable 
                        WHERE OrganCode=@OrganCode AND len(CommId)=0 AND TaskType='四害消杀' AND TaskUnit='任务';
                        SELECT ComletedRate FROM Tb_TaskManagement_MonthlyTotalTable 
                        WHERE OrganCode=@OrganCode AND len(CommId)=0 AND TaskType='垃圾清运' AND TaskUnit='任务';";

            if (commId != 0)
            {
                sql = @"SELECT ComletedRate FROM Tb_TaskManagement_MonthlyTotalTable WHERE CommId=@CommId AND TaskType='环境保洁' AND TaskUnit='任务';
                        SELECT ComletedRate FROM Tb_TaskManagement_MonthlyTotalTable WHERE CommId=@CommId AND TaskType='绿化养护' AND TaskUnit='任务';
                        SELECT ComletedRate FROM Tb_TaskManagement_MonthlyTotalTable WHERE CommId=@CommId AND TaskType='四害消杀' AND TaskUnit='任务';
                        SELECT ComletedRate FROM Tb_TaskManagement_MonthlyTotalTable WHERE CommId=@CommId AND TaskType='垃圾清运' AND TaskUnit='任务';";
            }

            try
            {
                var reader = conn.QueryMultiple(sql, new { OrganCode = organcode, CommId = commId });
                var _1 = reader.Read<decimal>().FirstOrDefault();
                var _2 = reader.Read<decimal>().FirstOrDefault();
                var _3 = reader.Read<decimal>().FirstOrDefault();
                var _4 = reader.Read<decimal>().FirstOrDefault();

                return new List<object>()
                {
                    new { Name = "本月环境保洁完成率", Value = (Math.Floor(_1 * 100) * 0.01m).ToString("f2"), Unit = "%" },
                    new { Name = "本月绿化养护完成率", Value = (Math.Floor(_2 * 100) * 0.01m).ToString("f2"), Unit = "%" },
                    new { Name = "本月四害消杀完成率", Value = (Math.Floor(_3 * 100) * 0.01m).ToString("f2"), Unit = "%" },
                    new { Name = "本月垃圾清运完成率", Value = (Math.Floor(_4 * 100) * 0.01m).ToString("f2"), Unit = "%" }
                };
            }
            catch (Exception)
            {
                return new List<object>()
                {
                    new { Name = "本月环境保洁完成率", Value = "0.00", Unit = "%" },
                    new { Name = "本月绿化养护完成率", Value = "0.00", Unit = "%" },
                    new { Name = "本月四害消杀完成率", Value = "0.00", Unit = "%" },
                    new { Name = "本月垃圾清运完成率", Value = "0.00", Unit = "%" }
                };
            }
            finally
            {
                if (db == null)
                    conn.Dispose();
            }
        }

        /// <summary>
        /// 项目经理信息桌面
        /// </summary>
        private List<object> GetStatisticsDetails_999907(string usercode, string organcode, int commId,
            IDbConnection db = null, IDbTransaction trans = null)
        {
            var conn = db ?? new SqlConnection(PubConstant.hmWyglConnectionString);

            var sql = @"SELECT ComletedRate FROM Tb_TaskManagement_MonthlyTotalTable 
                        WHERE OrganCode=@OrganCode AND len(CommId)=0 AND TaskType='报事工单' AND TaskUnit='全部';
                        SELECT ComletedRate FROM Tb_TaskManagement_MonthlyTotalTable 
                        WHERE OrganCode=@OrganCode AND len(CommId)=0 AND TaskType='设备巡检' AND TaskUnit='任务';
                        SELECT ComletedRate FROM Tb_TaskManagement_MonthlyTotalTable 
                        WHERE OrganCode=@OrganCode AND len(CommId)=0 AND TaskType='安全巡更' AND TaskUnit='任务';
                        SELECT ComletedRate FROM Tb_TaskManagement_MonthlyTotalTable 
                        WHERE OrganCode=@OrganCode AND len(CommId)=0 AND TaskType='环境保洁' AND TaskUnit='任务';
                        SELECT ComletedRate FROM Tb_TaskManagement_MonthlyTotalTable 
                        WHERE OrganCode=@OrganCode AND len(CommId)=0 AND TaskType='物管费往年清欠' AND TaskUnit='金额';
                        SELECT ComletedRate FROM Tb_TaskManagement_MonthlyTotalTable 
                        WHERE OrganCode=@OrganCode AND len(CommId)=0 AND TaskType='物管费本年收款' AND TaskUnit='金额';";

            if (commId != 0)
            {
                sql = @"SELECT ComletedRate FROM Tb_TaskManagement_MonthlyTotalTable WHERE CommId=@CommId AND TaskType='报事工单' AND TaskUnit='全部';
                        SELECT ComletedRate FROM Tb_TaskManagement_MonthlyTotalTable WHERE CommId=@CommId AND TaskType='设备巡检' AND TaskUnit='任务';
                        SELECT ComletedRate FROM Tb_TaskManagement_MonthlyTotalTable WHERE CommId=@CommId AND TaskType='安全巡更' AND TaskUnit='任务';
                        SELECT ComletedRate FROM Tb_TaskManagement_MonthlyTotalTable WHERE CommId=@CommId AND TaskType='环境保洁' AND TaskUnit='任务';
                        SELECT ComletedRate FROM Tb_TaskManagement_MonthlyTotalTable WHERE CommId=@CommId AND TaskType='物管费往年清欠' AND TaskUnit='金额';
                        SELECT ComletedRate FROM Tb_TaskManagement_MonthlyTotalTable WHERE CommId=@CommId AND TaskType='物管费本年收款' AND TaskUnit='金额';";
            }

            try
            {
                var reader = conn.QueryMultiple(sql, new { OrganCode = organcode, CommId = commId });
                var _1 = reader.Read<decimal>().FirstOrDefault();
                var _2 = reader.Read<decimal>().FirstOrDefault();
                var _3 = reader.Read<decimal>().FirstOrDefault();
                var _4 = reader.Read<decimal>().FirstOrDefault();
                var _5 = reader.Read<decimal>().FirstOrDefault();
                var _6 = reader.Read<decimal>().FirstOrDefault();

                return new List<object>()
                {
                    new { Name = "本月报事工单完成率", Value = (Math.Floor(_1 * 100) * 0.01m).ToString("f2"), Unit = "%" },
                    new { Name = "本月设备巡检完成率", Value = (Math.Floor(_2 * 100) * 0.01m).ToString("f2"), Unit = "%" },
                    new { Name = "本月安全巡更完成率", Value = (Math.Floor(_3 * 100) * 0.01m).ToString("f2"), Unit = "%" },
                    new { Name = "本月环境保洁完成率", Value = (Math.Floor(_4 * 100) * 0.01m).ToString("f2"), Unit = "%" },
                    new { Name = "往年欠费累计清欠率", Value = (Math.Floor(_5 * 100) * 0.01m).ToString("f2"), Unit = "%" },
                    new { Name = "本年应收累计收费率", Value = (Math.Floor(_6 * 100) * 0.01m).ToString("f2"), Unit = "%" }
                };
            }
            catch (Exception)
            {
                return new List<object>()
                {
                    new { Name = "本月报事工单完成率", Value = "0.00", Unit = "%" },
                    new { Name = "本月设备巡检完成率", Value = "0.00", Unit = "%" },
                    new { Name = "本月安全巡更完成率", Value = "0.00", Unit = "%" },
                    new { Name = "本月环境保洁完成率", Value = "0.00", Unit = "%" },
                    new { Name = "往年欠费累计清欠率", Value = "0.00", Unit = "%" },
                    new { Name = "本年应收累计收费率", Value = "0.00", Unit = "%" }
                };
            }
            finally
            {
                if (db == null)
                    conn.Dispose();
            }
        }

        /// <summary>
        /// 管理层级信息桌面
        /// </summary>
        private List<object> GetStatisticsDetails_999908(string usercode, string organcode, int commId,
            IDbConnection db = null, IDbTransaction trans = null)
        {
            return GetStatisticsDetails_999907(usercode, organcode, commId, db, trans);
        }
        #endregion
    }
}
