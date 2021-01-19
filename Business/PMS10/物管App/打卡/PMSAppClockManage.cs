using Common;
using Dapper;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using MobileSoft.Model.HSPR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using static Dapper.SqlMapper;
namespace Business
{
    public class PMSAppClockManage : PubInfo
    {
        public PMSAppClockManage()
        {
            base.Token = "20200616PMSAppClockManage";
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
                    case "GetOnlineState":                        // 获取当前打卡状态
                        Trans.Result = GetOnlineState(Row);
                        break;
                    case "UpdateLocation":                        // 更新当前打卡状态
                        Trans.Result = UpdateLocation(Row);
                        break;
                    case "UpdateCurrentLocation":                 // 更新当前位置信息，用于实时定位
                        Trans.Result = UpdateCurrentLocation(Row);
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
        /// 获取当前打卡状态
        /// </summary>
        private string GetOnlineState(DataRow row)
        {
            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = @"SELECT isnull(ClockOnlineState,0) FROM Tb_HSPR_AppOnlineClock 
                            WHERE convert(varchar(10),ClockTime,120)=convert(varchar(10),getdate(),120) 
                            AND UserCode=@UserCode AND IsDelete=0 
                            ORDER BY ClockTime DESC";

                var state = conn.Query<int>(sql, new { UserCode = Global_Var.LoginUserCode }).FirstOrDefault();

                return new ApiResult(true, state).toJson();
            }
        }


        /// <summary>
        /// 更新当前打卡状态
        /// </summary>
        private string UpdateLocation(DataRow row)
        {
            if (!row.Table.Columns.Contains("Location") || string.IsNullOrEmpty(row["Location"].ToString()))
            {
                return new ApiResult(false, "缺少参数Location").toJson();
            }
            if (!row.Table.Columns.Contains("FullLocation") || string.IsNullOrEmpty(row["FullLocation"].ToString()))
            {
                return new ApiResult(false, "缺少参数FullLocation").toJson();
            }
            if (!row.Table.Columns.Contains("Longitude") || string.IsNullOrEmpty(row["Longitude"].ToString()))
            {
                return new ApiResult(false, "缺少参数Longitude").toJson();
            }
            if (!row.Table.Columns.Contains("Latitude") || string.IsNullOrEmpty(row["Latitude"].ToString()))
            {
                return new ApiResult(false, "缺少参数Latitude").toJson();
            }
            if (!row.Table.Columns.Contains("State") || string.IsNullOrEmpty(row["State"].ToString()))
            {
                return new ApiResult(false, "缺少参数State").toJson();
            }
            var Location = row["Location"].ToString();
            var FullLocation = row["FullLocation"].ToString();
            var Longitude = AppGlobal.StrToDec(row["Longitude"].ToString());
            var Latitude = AppGlobal.StrToDec(row["Latitude"].ToString());
            // App传入1，则将状态设置为在线；如传入0，则设置为离线
            var State = AppGlobal.StrToInt(row["State"].ToString());

            var Date = DateTime.Now.ToString("yyyy-MM-dd");//获取当前年月日

            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                conn.Open();
                var trans = conn.BeginTransaction();
                try
                {
                    int count = conn.Query<int>($@"SELECT COUNT(*) FROM Tb_HSPR_AppOnlineClock WHERE CONVERT(varchar(10),ClockTime,120) = '{Date}' 
                                                    AND UserCode = {Global_Var.LoginUserCode} AND IsDelete = 0", null, trans).FirstOrDefault();
                    //判断今天是否有打卡记录
                    if (count > 0)//有打卡记录 更新数据
                    {
                        if (State == -1)
                        {
                            return UpdateLocation(conn, trans, Location, FullLocation, Longitude, Latitude, State, Date);
                        }
                        else
                        {
                            int OnlineState = conn.Query<int>($@"SELECT ISNULL(ClockOnlineState,0) FROM Tb_HSPR_AppOnlineClock WHERE CONVERT(varchar(10),ClockTime,120) = '{Date}' 
                                                                AND UserCode = {Global_Var.LoginUserCode} AND IsDelete = 0 ORDER BY ClockTime DESC", null, trans).FirstOrDefault();
                            if (OnlineState == State)//状态一致 更新
                            {
                                return UpdateLocation(conn, trans, Location, FullLocation, Longitude, Latitude, State, Date);
                            }
                            else
                            {
                                //状态不一致 插入新的数据
                                return InsertLocation(conn, trans, Location, FullLocation, Longitude, Latitude, State, Date);
                            }
                        }
                    }
                    else
                    {
                        return InsertLocation(conn, trans, Location, FullLocation, Longitude, Latitude, State, Date);
                    }
                }
                catch (Exception e)
                {
                    trans.Rollback();
                    return new ApiResult(false, "定位更新失败" + e.Message).toJson();
                }
            }
        }

        /// <summary>
        /// 更新定位数据公用方法
        /// </summary>
        private string UpdateLocation(IDbConnection conn, IDbTransaction trans, string Location, string FullLocation, decimal Longitude, decimal Latitude, int State, string Date)
        {
            int a = conn.Execute($@"UPDATE Tb_HSPR_AppOnlineClock 
                                            SET LastLocation = '{Location}',LastFullLocation = '{FullLocation}',
                                            LastLongitude = '{Longitude}',LastLatitude = '{Latitude}',LastLocationUpdateTime = GETDATE() 
                                            WHERE CONVERT(varchar(10),LastLocationUpdateTime,120) = '{Date}'AND UserCode ='{Global_Var.LoginUserCode}' AND IsDelete = 0", null, trans);
            if (a >= 1)
            {
                var EmployeeStatus = State == 1 ? "在线" : "离线";
                conn.Execute($@"UPDATE Tb_Sys_User SET EmployeeStatus='{EmployeeStatus}' WHERE UserCode ='{Global_Var.LoginUserCode}' AND isnull(IsDelete,0)=0", null, trans);

                trans.Commit();
                return new ApiResult(true, "打卡成功").toJson();
            }
            else
            {
                trans.Rollback();
                return new ApiResult(false, "打卡失败").toJson();
            }
        }


        private string InsertLocation(IDbConnection conn, IDbTransaction trans, string Location, string FullLocation, decimal Longitude, decimal Latitude, int State, string Date)
        {
            int a = conn.Execute($@"INSERT INTO Tb_HSPR_AppOnlineClock(IID,UserCode,UserName,ClockLocation,ClockFullLocation,ClockLongitude,ClockLatitude,
                                     ClockOnlineState,ClockTime,LastLocation,LastFullLocation,LastLongitude,LastLatitude,LastLocationUpdateTime,IsDelete)
                                        VALUES(NEWID(),'{Global_Var.LoginUserCode}','{Global_Var.LoginUserName}','{Location}','{FullLocation}',{Longitude},
                                        {Latitude},{State},GETDATE(),'{Location}','{FullLocation}',{Longitude},{Latitude},GETDATE(),0)", null, trans);
            if (a == 1)
            {
                var EmployeeStatus = State == 1 ? "在线" : "离线";
                conn.Execute($@"UPDATE Tb_Sys_User SET EmployeeStatus='{EmployeeStatus}' WHERE UserCode ='{Global_Var.LoginUserCode}' AND isnull(IsDelete,0)=0", null, trans);
                trans.Commit();
                return new ApiResult(true, "打卡成功").toJson();
            }
            else
            {
                trans.Rollback();
                return new ApiResult(false, "打卡失败").toJson();
            }
        }


        /// <summary>
        /// 更新当前位置信息，用于实时定位
        /// </summary>
        private string UpdateCurrentLocation(DataRow row)
        {
            if (!row.Table.Columns.Contains("LastLocation") || string.IsNullOrEmpty(row["LastLocation"].ToString()))
            {
                return new ApiResult(false, "缺少参数LastLocation").toJson();
            }
            if (!row.Table.Columns.Contains("LastFullLocation") || string.IsNullOrEmpty(row["LastFullLocation"].ToString()))
            {
                return new ApiResult(false, "缺少参数LastFullLocation").toJson();
            }
            if (!row.Table.Columns.Contains("LocationLongitude") || string.IsNullOrEmpty(row["LocationLongitude"].ToString()))
            {
                return new ApiResult(false, "缺少参数LocationLongitude").toJson();
            }
            if (!row.Table.Columns.Contains("LocationLatitude") || string.IsNullOrEmpty(row["LocationLatitude"].ToString()))
            {
                return new ApiResult(false, "缺少参数LocationLatitude").toJson();
            }
            
            var LastLocation = row["LastLocation"].ToString();
            var LastFullLocation = row["LastFullLocation"].ToString();
            var LocationLongitude = AppGlobal.StrToDec(row["LocationLongitude"].ToString());
            var LocationLatitude = AppGlobal.StrToDec(row["LocationLatitude"].ToString());

            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                try
                {
                    int a = conn.Execute($@"INSERT INTO Tb_HSPR_AppRealTimeLocation(IID,UserCode,UserName,LastLocation,LastFullLocation,LocationLongitude,
                                            LocationLatitude,LocationTime,IsDelete)
                                        VALUES(NEWID(),'{Global_Var.LoginUserCode}','{Global_Var.LoginUserName}','{LastLocation}','{LastFullLocation}',{LocationLongitude},
                                        {LocationLatitude},GETDATE(),0)", null);

                    return new ApiResult(true, "定位成功").toJson();
                }
                catch (Exception e)
                {
                    return new ApiResult(false, "定位更新失败" + e.Message).toJson();
                }
            }
        }


    }
}
