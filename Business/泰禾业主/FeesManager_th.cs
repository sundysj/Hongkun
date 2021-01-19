using Dapper;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using MobileSoft.Model.Unified;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class FeesManager_th : PubInfo
    {
        public FeesManager_th()
        {
            base.Token = "20171030FeesManager";
        }

        public override void Operate(ref Common.Transfer Trans)
        {
            Trans.Result = JSONHelper.FromString(false, "未知错误");

            DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
            DataRow Row = dAttributeTable.Rows[0];

            switch (Trans.Command)
            {
                case "GetFeesStatistics":             // 获取欠费或预存信息
                    Trans.Result = GetFeesStatistics(Row);
                    break;
                case "GetFeesStatistics_ZhongJi":             // 获取欠费或预存信息
                    Trans.Result = GetFeesStatistics_ZhongJi(Row);
                    break;   
                case "GetDebtsFeesList":              // 获取欠费列表
                    Trans.Result = GetDebtsFeesList(Row);
                    break;
                case "GetDebtsFeesList_ZhongJi":              // 获取欠费列表
                    Trans.Result = GetDebtsFeesList_ZhongJi(Row);
                    break;
                case "GetFeesHistoryList":             // 获取历史账单
                    Trans.Result = GetFeesHistoryList(Row);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 获取欠费或预存信息
        /// </summary>
        private string GetFeesStatistics(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编码不能为空");
            }
            if (!row.Table.Columns.Contains("RoomID") || string.IsNullOrEmpty(row["RoomID"].ToString()))
            {
                return JSONHelper.FromString(false, "房间编码不能为空");
            }

            string communityId = row["CommunityId"].ToString();
            string RoomID = row["RoomID"].ToString();

            //查询小区
            Tb_Community Community = GetCommunity(communityId);
            //构造链接字符串
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }

            string strcon = GetConnectionStringStr(Community);

            using (var conn = new SqlConnection(GetConnectionStringStr(Community)))
            {
                var sql = @"SELECT PrecAmount FROM
	                        (
		                        SELECT isnull(sum(isnull(a.PrecAmount,0)),0) AS PrecAmount FROM view_HSPR_PreCosts_Filter a
			                        WHERE a.CommID=@CommID AND a.RoomID=@RoomID
				                        AND convert(varchar(8000),a.CostNames)='住宅物业服务费'
						                        AND a.PrecAmount>0 AND a.IsPrec=1
	                        ) AS t";

                var PrecAmount = conn.Query<decimal>(sql, new { CommID = Community.CommID, RoomID = RoomID }).FirstOrDefault();

                sql = @"SELECT isnull(sum(isnull(DebtsAmount,0) + isnull(LateFeeAmount,0)),0) AS DebtsAmount  
                        FROM
                        (
                            SELECT DebtsAmount,
                                case when isnull(a.DebtsLateAmount,0) > 0 
                                    then dbo.funGetLateFeeDebts(a.CommID,a.FeesID,isnull(a.DebtsLateAmount,0))    
                                else     
                                    dbo.funGetLateFeeAll(isnull(f.DelinType,isnull(b.DelinType,0))    
                                   ,isnull(f.DelinDay,isnull(b.DelinDay,0))    
                                   ,isnull(f.DelinDelay,isnull(b.DelinDelay,0))    
                                   ,isnull(f.DelinRates,isnull(b.DelinRates,0))    
                                   ,b.RoundingNum    
                                   ,a.DebtsAmount    
                                   ,isnull(a.AccountsDueDate,a.FeesDueDate)    
                                   ,getdate()    
                                   ,a.CommID,a.FeesID,a.IsProperty,a.IsPrec,a.ContID,a.LeaseContID)    
                                end as LateFeeAmount  
                            FROM Tb_HSPR_Fees a 
                            LEFT JOIN Tb_HSPR_CostItem AS b ON a.CommID=b.CommID AND a.CostID=b.CostID   
                            LEFT JOIN Tb_HSPR_CostStandard AS f ON a.StanID=f.StanID  
                            WHERE a.RoomID=@RoomID 
                            AND ISNULL(a.IsCharge, 0)=0 AND ISNULL(a.IsBank,0)=0 AND ISNULL(a.IsPrec,0)=0 AND ISNULL(a.IsFreeze,0)=0 
                            AND CustID IN(SELECT CustID FROM Tb_HSPR_CustomerLive xx where xx.RoomID=@RoomID AND ISNULL(IsDelLive,0)=0)
                        ) AS t";

                var DebtsAmount = conn.Query<decimal>(sql, new { CommID = Community.CommID, RoomID = RoomID }).FirstOrDefault();

                // 俊发判断是不是成都地区，如果是成都，按月缴费，其他地区按年
                int BillingCycle = 1;
                if (Community.CorpID == 1985)
                {
                    if (conn.Query(@"SELECT * FROM Tb_HSPR_Community WHERE CommID=@CommID AND OrganCode='0104'",
                            new { CommID = Community.CommID }).Count() > 0)
                    {
                        BillingCycle = 1;
                    }
                    else
                    {
                        BillingCycle = 12;
                    }
                }

                return new ApiResult(true, new { PrecAmount = PrecAmount, DebtsAmount = DebtsAmount, BillingCycle = BillingCycle }).toJson();
            }
        }

        /// <summary>
        /// 获取欠费或预存信息
        /// </summary>
        private string GetFeesStatistics_ZhongJi(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编码不能为空");
            }
            if (!row.Table.Columns.Contains("RoomID") || string.IsNullOrEmpty(row["RoomID"].ToString()))
            {
                return JSONHelper.FromString(false, "房间编码不能为空");
            }

            string communityId = row["CommunityId"].ToString();
            string RoomID = row["RoomID"].ToString();

            //查询小区
            Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(communityId);
            //构造链接字符串
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }
            string strcon = new CostInfo().GetConnectionStringStr(Community);

            string sql = string.Format(@"SELECT PrecAmount FROM
	                                    (
		                                    SELECT SUM(a.PrecAmount) AS PrecAmount FROM view_HSPR_PreCosts_Filter a
			                                    WHERE a.CommID={0} AND a.RoomID={1}
				                                    AND convert(varchar(8000),a.CostNames)='住宅物业服务费'
						                                    AND a.PrecAmount>0 AND a.IsPrec=1
	                                    ) AS t", Community.CommID, RoomID);
            DataSet ds = new DbHelperSQLP(strcon).Query(sql);
            if (ds != null & ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                decimal PrecAmount = AppGlobal.StrToDec(ds.Tables[0].Rows[0]["PrecAmount"].ToString());

                DateTime dateTime = DateTime.Now.AddMonths(1);
                dateTime = new DateTime(dateTime.Year, dateTime.Month, 1).AddDays(-1);

                // 读取未缴
                sql = string.Format(@"SELECT SUM(ISNULL(x.DebtsAmount,0) + ISNULL(x.LateFeeAmount,0)) AS DebtsAmount FROM 
						view_HSPR_Fees_SearchFilter x WHERE x.DebtsAmount > 0 AND ISNULL(x.IsCharge, 0) = 0 AND ISNULL(x.IsBank, 0) = 0 AND ISNULL(x.IsPrec, 0) = 0 AND ISNULL(IsFreeze, 0) = 0
						 AND x.RoomID = {0}", RoomID);
                sql += " AND x.CustID IN(SELECT CustID FROM Tb_HSPR_CustomerLive xx where xx.RoomID=x.RoomID AND ISNULL(IsDelLive,0)=0 AND LiveType = 1) ";

                DataTable dt = new DbHelperSQLP(strcon).Query(sql).Tables[0];

                decimal DebtsAmount = AppGlobal.StrToDec(dt.Rows[0]["DebtsAmount"].ToString());

                // 俊发判断是不是成都地区，如果是成都，按月缴费，其他地区按年
                int BillingCycle = 1;
                if (Community.CorpID == 1985)
                {
                    using (var conn = new SqlConnection(strcon))
                    {
                        if (conn.Query(@"SELECT * FROM Tb_HSPR_Community WHERE CommID=@CommID AND OrganCode='0104'",
                            new { CommID = Community.CommID }).Count() > 0)
                        {
                            BillingCycle = 1;
                        }
                        else
                        {
                            BillingCycle = 12;
                        }
                    }
                }

                return new ApiResult(true, new { PrecAmount = PrecAmount, DebtsAmount = DebtsAmount, BillingCycle = BillingCycle }).toJson();
            }
            else
            {
                return new ApiResult(true, new { PrecAmount = 0, DebtsAmount = 0, BillingCycle = 1 }).toJson();
            }
        }

        /// <summary>
        /// 获取未缴账单
        /// </summary>
        private string GetDebtsFeesList(DataRow row)
        {
            if (!row.Table.Columns.Contains("RoomID") || string.IsNullOrEmpty(row["RoomID"].ToString()))
            {
                return JSONHelper.FromString(false, "用户编码不能为空");
            }
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编码不能为空");
            }

            string RoomID = row["RoomID"].ToString();
            string CommunityId = row["CommunityId"].ToString();

            //查询小区
            Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(CommunityId);
            //构造链接字符串
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }
            string strcon = new Business.CostInfo().GetConnectionStringStr(Community);

            StringBuilder sb = new StringBuilder();
            sb.Append(@" select FeesID,SysCostSign,CostName,DueAmount,isnull(DebtsAmount,0) DebtsAmount,isnull(WaivAmount,0) AS WaivAmount,isnull(PrecAmount,0) AS PrecAmount,
                        isnull(PaidAmount,0) AS PaidAmount,ISNULL(LateFeeAmount,0) AS LateFeeAmount,CONVERT(varchar(100),FeesDueDate,111) as FeesDueDate, ");
            sb.Append("isnull(IsCharge,0) As IsCharge,FeesStateDate,FeesEndDate from view_HSPR_Fees_SearchFilter ");

            sb.AppendFormat(" where CommID={0}", Community.CommID);
            sb.AppendFormat(" and RoomID={0}", RoomID);
            sb.Append(" and custid in(select x.CustID from Tb_HSPR_CustomerLive x LEFT JOIN view_HSPR_Fees_Filter y ON x.RoomID =y.RoomID  where  isnull(IsDelLive,0)=0) ");
            sb.Append(" and ischarge=0 and IsBank=0 and IsFreeze=0 and IsProperty=0 and ISNULL(IsPrec,0)=0 ");
            sb.Append("  order by FeesID desc ");
            DataSet ds = new DbHelperSQLP(strcon).Query(sb.ToString());

            return JSONHelper.FromString(ds.Tables[0]);
        }

        /// <summary>
        /// 中集 只获取业主的未缴费项
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string GetDebtsFeesList_ZhongJi(DataRow row)
        {
            if (!row.Table.Columns.Contains("RoomID") || string.IsNullOrEmpty(row["RoomID"].ToString()))
            {
                return JSONHelper.FromString(false, "用户编码不能为空");
            }
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编码不能为空");
            }

            string RoomID = row["RoomID"].ToString();
            string CommunityId = row["CommunityId"].ToString();

            //查询小区
            Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(CommunityId);
            //构造链接字符串
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }
            string strcon = new Business.CostInfo().GetConnectionStringStr(Community);

            StringBuilder sb = new StringBuilder();
            sb.Append(@" select FeesID,CostName,DueAmount,isnull(DebtsAmount,0) DebtsAmount,isnull(WaivAmount,0) AS WaivAmount,isnull(PrecAmount,0) AS PrecAmount,
                        isnull(PaidAmount,0) AS PaidAmount,ISNULL(LateFeeAmount,0) AS LateFeeAmount,CONVERT(varchar(100),FeesDueDate,111) as FeesDueDate, ");
            sb.Append("isnull(IsCharge,0) As IsCharge,FeesStateDate,FeesEndDate from view_HSPR_Fees_SearchFilter ");

            sb.AppendFormat(" where CommID={0}", Community.CommID);
            sb.AppendFormat(" and RoomID={0}", RoomID);
            sb.Append(" and custid in(select x.CustID from Tb_HSPR_CustomerLive x LEFT JOIN view_HSPR_Fees_Filter y ON x.RoomID =y.RoomID  where  isnull(IsDelLive,0)=0 and LiveType = 1)");
            sb.Append(" and ischarge=0 and IsBank=0 and IsFreeze=0 and IsProperty=0 and ISNULL(IsPrec,0)=0 ");
            sb.Append("  order by FeesID desc ");
            DataSet ds = new DbHelperSQLP(strcon).Query(sb.ToString());

            return JSONHelper.FromString(ds.Tables[0]);
        }

        /// <summary>
        /// 获取历史账单
        /// </summary>
        private string GetFeesHistoryList(DataRow row)
        {
            if (!row.Table.Columns.Contains("RoomID") || string.IsNullOrEmpty(row["RoomID"].ToString()))
            {
                return JSONHelper.FromString(false, "用户编码不能为空");
            }
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编码不能为空");
            }

            string RoomID = row["RoomID"].ToString();
            string CommunityId = row["CommunityId"].ToString();

            //查询小区
            Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(CommunityId);
            //构造链接字符串
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }
            string strcon = new Business.CostInfo().GetConnectionStringStr(Community);

            StringBuilder sb = new StringBuilder();
            sb.Append(" select FeesID,CostName,DueAmount,DebtsAmount,WaivAmount,0 AS PrecAmount,(PaidAmount+PrecAmount) AS PaidAmount,CONVERT(varchar(100),FeesDueDate,111) as FeesDueDate, CONVERT(varchar(100),FeesChargeDate,111) as FeesChargeDate,");
            sb.Append("isnull(IsCharge,0) As IsCharge,FeesStateDate,FeesEndDate from view_HSPR_Fees_Filter ");

            sb.AppendFormat(" where CommID={0}", Community.CommID);
            sb.AppendFormat(" and RoomID={0}", RoomID);
            sb.Append(" and (IsCharge=1 or ISNULL(IsPrec,0)=1) order by FeesID desc ");
            DataSet ds = new DbHelperSQLP(strcon).Query(sb.ToString());

            return JSONHelper.FromString(ds.Tables[0]);
        }
    }
}
