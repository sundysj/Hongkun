using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileSoft.Common;
using System.Data.SqlClient;
using Dapper;
using MobileSoft.DBUtility;

namespace Business
{
    public partial class CostInfo
    {
        private const string ConnStrRX = "Pooling=false;Data Source=.;Initial Catalog=HM_wygl_new_1528;User ID=LFUser;Password=LF123SPoss";

        /// <summary>
        /// 融信，优惠券使用回调函数
        /// </summary>
        private string CouponUsedCallback_RongXin(DataRow row)
        {
            // http://tool.chacuo.net/cryptdes
            string key = "WwW.tw369.Com";

            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区ID不能为空");
            }
            if (!row.Table.Columns.Contains("FeesIDs") || string.IsNullOrEmpty(row["FeesIDs"].ToString()))
            {
                return JSONHelper.FromString(false, "费用ID不能为空");
            }
            if (!row.Table.Columns.Contains("TotalAmount") || string.IsNullOrEmpty(row["TotalAmount"].ToString()))
            {
                return JSONHelper.FromString(false, "缴费金额不能为空");
            }

            try
            {
                string connStr = "Pooling=false;Data Source=125.64.16.14,8433;Initial Catalog=HM_wygl_new_1528;User ID=LFUser;Password=LF123SPoss";
                string feesIds = row["FeesIDs"].ToString().Trim(',');

                decimal totalAmount = 0.0m;
                decimal couponAmount = 0.0M;

                try
                {
                    totalAmount = AppGlobal.StrToDec(DES.Decrypt(row["TotalAmount"].ToString(), key));

                    if (row.Table.Columns.Contains("CouponAmount") && !string.IsNullOrEmpty(row["CouponAmount"].ToString()))
                    {
                        couponAmount = AppGlobal.StrToDec(DES.Decrypt(row["CouponAmount"].ToString(), key));
                    }
                }
                catch (Exception)
                {
                    return JSONHelper.FromString(false, "解密字符串错误");
                }

                using (IDbConnection conn = new SqlConnection(connStr))
                {
                    // 1、获取费用信息
                    string sql = string.Format(@"SELECT FeesID,DebtsAmount FROM Tb_HSPR_Fees WHERE isnull(IsCharge,0)=0 AND FeesID IN({0})", feesIds);

                    IEnumerable<dynamic> feesResultSet = conn.Query(sql);

                    if (feesResultSet.Count() > 0)
                    {
                        DynamicParameters parameters = new DynamicParameters();
                        parameters.Add("@FeesIDs", feesIds);
                        parameters.Add("@ChargeAmount", totalAmount);
                        parameters.Add("@WaivAmount", couponAmount);
                        parameters.Add("@IsSucceed", 0, DbType.Int32, ParameterDirection.Output);

                        conn.Execute("Proc_HSPR_FeesReceipts_Insert_Phone", parameters, null, null, CommandType.StoredProcedure);

                        int IsSucceed = parameters.Get<int>("@IsSucceed");

                        if (IsSucceed == 1)
                        {
                            return JSONHelper.FromString(true, "操作成功");
                        }
                        else
                        {

                            return JSONHelper.FromString(false, "操作失败");
                        }

                        /*
                        foreach (dynamic item in feesResultSet)
                        {
                            decimal itemWaivAmount = 0.0m;      // 当前费用减免金额
                            decimal itemDebtsAmount = 0.0m;     // 当前未缴费用欠费金额
                            decimal itemAmount = 0.0m;          // 销账的实际金额

                            // 当前费用欠费
                            itemDebtsAmount = AppGlobal.StrToDec(item.DebtsAmount.ToString());

                            // 优惠减免金额
                            if (couponAmount > 0)
                            {
                                itemWaivAmount = (couponAmount > itemDebtsAmount ? itemDebtsAmount : couponAmount);
                            }

                            itemAmount = (totalAmount > (itemDebtsAmount - itemWaivAmount) ? (itemDebtsAmount - itemWaivAmount) : totalAmount);

                            // 下账，在存储过程中插入费用明细
                            conn.Execute("Proc_HSPR_FeesReceipts_Insert_Phone",
                                new
                                {
                                    FeesID = item.FeesID,
                                    ChargeAmount = itemAmount,
                                    WaivAmount = itemWaivAmount
                                }, null, null, CommandType.StoredProcedure);

                            // 插入减免明细
                            if (itemWaivAmount > 0)
                            {
                                conn.Execute("Proc_HSPR_WaiversFee_Insert_HongBao", new { FeesID = item.FeesID, WaivAmount = itemWaivAmount }, null, null, CommandType.StoredProcedure);
                            }

                            couponAmount -= itemWaivAmount;
                            totalAmount -= itemAmount;
                        }
                        */
                    }
                    else
                    {
                        return JSONHelper.FromString(false, "未查询到未缴费用信息");
                    }
                }
            }
            catch (Exception ex)
            {
                return JSONHelper.FromString(false, ex.Message);
            }
        }

        //获取报事信息
        private string GetIncidentInfo_RongXin(DataRow row)
        {
            if (!row.Table.Columns.Contains("CustID") || string.IsNullOrEmpty(row["CustID"].ToString()))
            {
                return JSONHelper.FromString(false, "业主ID不能为空");
            }

            string sql = "";
            sql += "select i.CustID,i.IncidentID,RoomSign,IncidentDate,IncidentMan,IncidentContent,DispTypeState,DispDate,State,MainEndDate,ISNULL(f.DueAmount,0) IncidnetAmount";
            sql += " from view_HSPR_IncidentSeach_Filter  i left join Tb_HSPR_Fees f on i.CommID=f.CommID and i.IncidentID=f.IncidentID";
            sql += " where ISNULL(i.IsDelete,0)=0 and i.CustID=" + row["CustID"].ToString();
            DataSet ds = new DbHelperSQLP(ConnStrRX).Query(sql.ToString());
            return JSONHelper.FromString(ds.Tables[0]);
        }

        //获取车辆信息
        private string GetParkingInfo_RongXin(DataRow row)
        {
            string CustID = "0";
            string RoomID = "0";

            if (row.Table.Columns.Contains("CustID"))
            {
                if (row["CustID"].ToString() != "")
                {
                    CustID = row["CustID"].ToString();
                }
            }
            if (row.Table.Columns.Contains("RoomID") )
            {
                if (row["RoomID"].ToString()!="")
                {
                    RoomID = row["RoomID"].ToString();
                }

            }
            if (CustID=="0"&&RoomID=="0")
            {
                return JSONHelper.FromString(false, "请输入客户或房屋编号");
            }

            string sql = "";
            sql += "select CustName,CustID,RoomName,RoomID,ParkID,CarparkName,ParkType,CostName,CostID,StanName,StanAmount,ParkName as ParkNO,NCustName,NCustID,NRoomSign,NRoomID,ParkStartDate,ParkEndDate,CarSign,HandDate,HandID,Handling";
            sql += " from view_HSPR_ParkingSel_Filter";
            sql += " where ISNULL(IsDelete,0)=0";
            if (CustID != "0")
            {
                sql += " and CustID=" + CustID;
            }
            if (RoomID != "0")
            {
                sql += " and RoomID=" + RoomID;
            }

            DataSet ds = new DbHelperSQLP(ConnStrRX).Query(sql.ToString());
            return JSONHelper.FromString(ds.Tables[0]);
        }

    }
}
