using Common;
using Dapper;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using MobileSoft.Model.Unified;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Business
{
    /// <summary>
    /// 在线缴费
    /// </summary>
    public partial class CostInfo : PubInfo
    {
        public CostInfo()
        {
            base.Token = "20160826OCostInfo";
        }

        public override void Operate(ref Common.Transfer Trans)
        {
            Trans.Result = JSONHelper.FromString(false, "未知错误");

            DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
            DataRow Row = dAttributeTable.Rows[0];

            switch (Trans.Command)
            {
                case "GetArrearsList":
                    Trans.Result = GetArrearsList(Row);//当前未缴
                    break;
                case "GetBillList":
                    Trans.Result = GetBillList(Row);//每月账单
                    break;
                case "GetHistoricalPaymentList":
                    Trans.Result = GetHistoricalPaymentList(Row);//历史缴费
                    break;
                case "GetAdvanceList"://当前预缴
                    Trans.Result = GetAdvanceList(Row);
                    break;
                case "GetPrecCost"://查询预存费项
                    Trans.Result = GetPrecCost(Row);
                    break;
                case "GetPrecCost2":
                    //查询预存费项
                    Trans.Result = GetPrecCost2(Row);
                    break;
                case "GetOffsetPreDetail"://查询冲抵记录
                    Trans.Result = GetOffsetPreDetail(Row);
                    break;
                case "CheckArrears"://查询是否欠费
                    Trans.Result = CheckArrears(Row);
                    break;
                case "CalcAdvancePrice"://计算预交费用
                    Trans.Result = CalcAdvancePrice_TY(Row);
                    break;
                case "CouponUsedCallback_RongXin":              // 融信，优惠券使用回调函数
                    Trans.Result = CouponUsedCallback_RongXin(Row);
                    break;
                case "GetParkingInfo_RongXin":
                    Trans.Result = GetParkingInfo_RongXin(Row);// 融信 获取车位信息
                    break;
                case "GetIncidentInfo_RongXin":
                    Trans.Result = GetIncidentInfo_RongXin(Row);//融信 获取报事信息
                    break;
                case "PayFees"://实收、预收下帐，可通用【CRM使用】
                    Trans.Result = PayFees(Row);
                    break;
                case "GetIncidentFeesList": //华南城获取报事费用
                    Trans.Result = GetIncidentFeesList(Row);
                    break;
                case "GetPreCostDetailList": //华南城获取预缴历史
                    Trans.Result = GetPreCostDetailList(Row);
                    break;

                case "GetOffsetPreDetailList"://华南城获取冲抵历史
                    Trans.Result = GetOffsetPreDetailList(Row);
                    break;
                case "GetPrecCost_BF":
                    Trans.Result = GetPrecCost_BF(Row); // 北方物业获取预存费项
                    break;
                case "CalcAdvancePrice_BF":
                    Trans.Result = CalcAdvancePrice_BF(Row); // 北方物业获取预存费项
                    break;
                case "ChargePrice_BF":
                    Trans.Result = ChargePrice_BF(Row); // 北方物业预存下账
                    break;
                case "GetAdvanceList_BF"://当前预缴 北方
                    Trans.Result = GetAdvanceList_BF(Row);
                    break;
                case "CaclPrecAmount":
                case "CalcPrecAmount":
                    Trans.Result = CalcPrecAmount(Row);
                    break;
                case "GetParkCarList":
                    Trans.Result = GetParkCarList(Row);
                    break;
                default:
                    break;
            }
        }

        private string GetParkCarList(DataRow row)
        {
            string CommunityId = string.Empty;
            if (row.Table.Columns.Contains("CommunityId"))
            {
                CommunityId = row["CommunityId"].ToString();
            }
            if (string.IsNullOrEmpty(CommunityId))
            {

                return new ApiResult(false, "小区编码不能为空").toJson();
            }
            //查询小区
            Tb_Community tb_Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(CommunityId);
            //构造链接字符串
            if (tb_Community == null)
            {
                return new ApiResult(false, "该项目不存在").toJson();
            }
            string connStr = GetConnectionStringStr(tb_Community);

            if (!row.Table.Columns.Contains("CustID") || !long.TryParse(row["CustID"].ToString(), out long CustID))
            {
                CustID = 0;
            }
            if (!row.Table.Columns.Contains("RoomID") || !long.TryParse(row["RoomID"].ToString(), out long RoomID))
            {
                RoomID = 0;
            }
            using (IDbConnection conn = new SqlConnection(connStr))
            {
                List<dynamic> list = conn.Query("Proc_HSPR_ParkingHand_Filter", new { SQLEx = $" AND ISNULL(IsDelete,0) = 0 AND CustID = {CustID} AND RoomID = {RoomID} " }, null, true, null, CommandType.StoredProcedure).ToList();
                if (null == list)
                {
                    list = new List<dynamic>();
                }
                return new ApiResult(true, list).toJson();
            }

        }

        /// <summary>
        /// 按月计算预存金额
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string CalcPrecAmount(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return new ApiResult(false, "小区编码不能为空").toJson();
            }
            string CommunityId = row["CommunityId"].ToString();

            //查询小区
            Tb_Community tb_Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(CommunityId);
            //构造链接字符串
            if (tb_Community == null)
            {
                return new ApiResult(false, "该项目不存在").toJson();
            }

            string connStr = GetConnectionStringStr(tb_Community);

            if (!row.Table.Columns.Contains("CustID") || string.IsNullOrEmpty(row["CustID"].ToString()))
            {
                return new ApiResult(false, "CustID不能为空").toJson();
            }
            string CustID = row["CustID"].ToString();
            if (!row.Table.Columns.Contains("RoomID") || string.IsNullOrEmpty(row["RoomID"].ToString()))
            {
                return new ApiResult(false, "RoomID不能为空").toJson();
            }
            string RoomID = row["RoomID"].ToString();
            if (!row.Table.Columns.Contains("CostID") || string.IsNullOrEmpty(row["CostID"].ToString()))
            {
                return new ApiResult(false, "CostID不能为空").toJson();
            }
            string CostID = row["CostID"].ToString();
            if (!row.Table.Columns.Contains("StanID") || string.IsNullOrEmpty(row["StanID"].ToString()))
            {
                return new ApiResult(false, "StanID不能为空").toJson();
            }
            string StanID = row["StanID"].ToString();
            int Count = 1;
            if (row.Table.Columns.Contains("Count"))
            {
                if (!int.TryParse(row["Count"].ToString(), out Count))
                {
                    Count = 1;
                }
            }
            int HandID = 0;
            int Amount = 0;
            int Amount2 = 0;

            DateTime DateNow = DateTime.Now;
            DateTime FeesStateDate = DateNow.AddDays(1 - DateNow.Day).Date;
            DateTime FeesEndDate = DateNow.AddMonths(Count).AddDays(1 - DateTime.Now.Day).Date.AddMonths(1).AddSeconds(-1);

            using (IDbConnection conn = new SqlConnection(connStr))
            {
                dynamic info = conn.QueryFirstOrDefault("Proc_HSPR_Fees_CalcAmount", new { CommID = tb_Community.CommID, CustID, RoomID, HandID, CostID, StanID, FeesStateDate, FeesEndDate, Amount, Amount2 }, null, null, CommandType.StoredProcedure);
                if (null == info)
                {
                    return new ApiResult(false, "计算费用失败,请重试").toJson();
                }
                return new ApiResult(true, info).toJson();
            }
        }


        /// <summary>
        /// 华南城获取冲抵历史
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string GetOffsetPreDetailList(DataRow row)
        {
            //运营小区ID
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return new ApiResult(false, "小区编号不能为空").toJson();
            }
            string communityId = row["CommunityId"].ToString();
            Tb_Community tb_Community = GetCommunity(communityId);
            if (null == tb_Community)
            {
                return new ApiResult(false, "该小区不存在").toJson();
            }
            string connStr = GetConnectionStringStr(tb_Community);
            if (!row.Table.Columns.Contains("CustID") || string.IsNullOrEmpty(row["CustID"].ToString()))
            {
                return new ApiResult(false, "客户编号不能为空").toJson();
            }
            if (!row.Table.Columns.Contains("RoomID") || string.IsNullOrEmpty(row["RoomID"].ToString()))
            {
                return new ApiResult(false, "房间编号不能为空").toJson();
            }
            string CommID = tb_Community.CommID;
            string CustID = row["CustID"].ToString();
            string RoomID = row["RoomID"].ToString();
            Dictionary<string, object> resultDic = new Dictionary<string, object>();
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();

            int page;
            if (!row.Table.Columns.Contains("Page") || string.IsNullOrEmpty(row["Page"].ToString()))
            {
                page = 1;
            }
            else
            {
                if (!int.TryParse(row["Page"].ToString(), out page))
                {
                    page = 1;
                }
            }

            int size;
            if (!row.Table.Columns.Contains("Size") || string.IsNullOrEmpty(row["Size"].ToString()))
            {
                size = 10;
            }
            else
            {
                if (!int.TryParse(row["Size"].ToString(), out size))
                {
                    size = 10;
                }
            }
            int PageCount;
            int Counts;
            string sqlStr = string.Format("AND CommID = {0} AND CustID = {1} AND RoomID = {2}", CommID, CustID, RoomID);
            DataSet Ds = BaseGetData.GetList(connStr, out PageCount, out Counts, sqlStr, page, size, "AuditDate", 1, "view_HSPR_OffsetPreDetail_Filter", "IID");
            if (null == Ds || Ds.Tables.Count == 0)
            {
                resultDic.Add("pages", PageCount);
                resultDic.Add("counts", Counts);
                resultDic.Add("list", list);
                return new ApiResult(true, resultDic).toJson();
            }
            DataTable dt = Ds.Tables[0];
            if (null == dt || dt.Rows.Count == 0)
            {
                resultDic.Add("pages", PageCount);
                resultDic.Add("counts", Counts);
                resultDic.Add("list", list);
                return new ApiResult(true, resultDic).toJson();
            }
            Dictionary<string, object> dictionary;
            foreach (DataRow item in dt.Rows)
            {
                dictionary = new Dictionary<string, object>();
                dictionary.Add("CostName", item["CostName"]);
                dictionary.Add("OffsetAmount", item["OffsetAmount"]);
                dictionary.Add("TakeWiseName", item["TakeWiseName"]);
                dictionary.Add("AuditDate", DBNull.Value == item["AuditDate"] ? "" : ((DateTime)item["AuditDate"]).ToString());
                dictionary.Add("FeesDueDate", DBNull.Value == item["FeesDueDate"] ? "" : ((DateTime)item["FeesDueDate"]).ToString());
                list.Add(dictionary);
            }
            resultDic.Add("pages", PageCount);
            resultDic.Add("counts", Counts);
            resultDic.Add("list", list);
            return new ApiResult(true, resultDic).toJson();
        }

        /// <summary>
        /// 华南城获取预缴历史
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string GetPreCostDetailList(DataRow row)
        {
            //运营小区ID
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return new ApiResult(false, "小区编号不能为空").toJson();
            }
            string communityId = row["CommunityId"].ToString();
            Tb_Community tb_Community = GetCommunity(communityId);
            if (null == tb_Community)
            {
                return new ApiResult(false, "该小区不存在").toJson();
            }
            string connStr = GetConnectionStringStr(tb_Community);
            if (!row.Table.Columns.Contains("CustID") || string.IsNullOrEmpty(row["CustID"].ToString()))
            {
                return new ApiResult(false, "客户编号不能为空").toJson();
            }
            if (!row.Table.Columns.Contains("RoomID") || string.IsNullOrEmpty(row["RoomID"].ToString()))
            {
                return new ApiResult(false, "房间编号不能为空").toJson();
            }
            string CommID = tb_Community.CommID;
            string CustID = row["CustID"].ToString();
            string RoomID = row["RoomID"].ToString();
            Dictionary<string, object> resultDic = new Dictionary<string, object>();
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();

            int page;
            if (!row.Table.Columns.Contains("Page") || string.IsNullOrEmpty(row["Page"].ToString()))
            {
                page = 1;
            }
            else
            {
                if (!int.TryParse(row["Page"].ToString(), out page))
                {
                    page = 1;
                }
            }

            int size;
            if (!row.Table.Columns.Contains("Size") || string.IsNullOrEmpty(row["Size"].ToString()))
            {
                size = 10;
            }
            else
            {
                if (!int.TryParse(row["Size"].ToString(), out size))
                {
                    size = 10;
                }
            }
            int PageCount;
            int Counts;
            string sqlStr = string.Format("AND CommID = {0} AND CustID = {1} AND RoomID = {2}", CommID, CustID, RoomID);
            DataSet Ds = BaseGetData.GetList(connStr, out PageCount, out Counts, sqlStr, page, size, "PrecDate", 1, "view_HSPR_PreCostsDetail_Filter", "RecdID");
            if (null == Ds || Ds.Tables.Count == 0)
            {
                resultDic.Add("pages", PageCount);
                resultDic.Add("counts", Counts);
                resultDic.Add("list", list);
                return new ApiResult(true, resultDic).toJson();
            }
            DataTable dt = Ds.Tables[0];
            if (null == dt || dt.Rows.Count == 0)
            {
                resultDic.Add("pages", PageCount);
                resultDic.Add("counts", Counts);
                resultDic.Add("list", list);
                return new ApiResult(true, resultDic).toJson();
            }
            Dictionary<string, object> dictionary;
            foreach (DataRow item in dt.Rows)
            {
                dictionary = new Dictionary<string, object>();
                dictionary.Add("CostName", item["CostName"]);
                dictionary.Add("PrecAmount", item["PrecAmount"]);
                dictionary.Add("ChargeMode", item["ChargeMode"]);
                dictionary.Add("PrecDate", DBNull.Value == item["PrecDate"] ? "" : ((DateTime)item["PrecDate"]).ToString());
                list.Add(dictionary);
            }
            resultDic.Add("pages", PageCount);
            resultDic.Add("counts", Counts);
            resultDic.Add("list", list);
            return new ApiResult(true, resultDic).toJson();
        }


        private string GetIncidentFeesList(DataRow row)
        {
            //运营小区ID
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return new ApiResult(false, "小区编号不能为空").toJson();
            }
            string communityId = row["CommunityId"].ToString();
            Tb_Community tb_Community = GetCommunity(communityId);
            if (null == tb_Community)
            {
                return new ApiResult(false, "该小区不存在").toJson();
            }
            string connStr = GetConnectionStringStr(tb_Community);
            if (!row.Table.Columns.Contains("IncidentID") || string.IsNullOrEmpty(row["IncidentID"].ToString()))
            {
                return new ApiResult(false, "报事ID不能为空").toJson();
            }
            string CommID = tb_Community.CommID;
            string IncidentID = row["IncidentID"].ToString();

            //应收DueAmount
            //减免WaivAmount
            //冲抵PrecAmount
            //欠费DebtsAmount
            //应收-减免-冲抵=欠费
            DataTable dt = null;
            using (IDbConnection conn = new SqlConnection(connStr))
            {
                string sqlStr = "SELECT x.FeesID, x.CostName, x.CustName, x.FeeDueYearMonth, ISNULL(x.DebtsAmount,0) AS DueAmount, 0 AS WaivAmount FROM view_HSPR_Fees_Filter x WHERE x.DebtsAmount > 0 AND ISNULL(x.IsCharge,0) = 0 AND ISNULL(x.IsBank,0) = 0 AND ISNULL(x.IsPrec,0) = 0 AND ISNULL(IsFreeze,0) = 0 AND x.CommID = @CommID AND x.IncidentID = @IncidentID ORDER BY FeesDueDate DESC";
                dt = conn.ExecuteReader(sqlStr, new { CommID = CommID, IncidentID = IncidentID }, null, null, CommandType.Text).ToDataSet().Tables[0];
            }
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            if (null == dt || dt.Rows.Count == 0)
            {
                return new ApiResult(true, list).toJson();
            }
            Dictionary<string, object> dictionary;
            foreach (DataRow item in dt.Rows)
            {
                dictionary = new Dictionary<string, object>();
                dictionary.Add("FeesID", item["FeesID"]);
                dictionary.Add("CostName", item["CostName"]);
                dictionary.Add("CustName", item["CustName"]);
                dictionary.Add("FeeDueYearMonth", item["FeeDueYearMonth"]);
                dictionary.Add("DueAmount", item["DueAmount"]);
                dictionary.Add("WaivAmount", item["WaivAmount"]);
                list.Add(dictionary);
            }
            return new ApiResult(true, list).toJson();
        }

        /// <summary>
        /// PayFees   下帐【可通用实收、预收下帐】
        /// </summary>
        /// <param name="row"></param>
        /// CustID
        /// CommID
        /// FeesIds             费用编码
        /// PrecAmount          下帐金额
        /// ChargeMode              来源【默认自助缴费】
        /// memo                    备注
        /// 调整：2017-12-19修改
        ///     下帐方式固定为：CRM
        ///     增加阀门配置
        ///     增加对违约金收取支持
        /// <returns></returns>
        private string PayFees(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编码不能为空");
            }
            if (!row.Table.Columns.Contains("FeesIds") || string.IsNullOrEmpty(row["FeesIds"].ToString()))
            {
                return JSONHelper.FromString(false, "费用编码不能为空");
            }
            if (!row.Table.Columns.Contains("PrecAmount") || AppGlobal.StrToDec(row["PrecAmount"].ToString()) <= 0)
            {
                return JSONHelper.FromString(false, "下帐金额有误");
            }
            string ErrorMsg = "";
            string backstr = "";
            try
            {
                string CommID = AppGlobal.StrToStr(row["CommID"].ToString());
                Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(CommID);

                string FeesIds = AppGlobal.StrToStr(row["FeesIds"].ToString());
                decimal PrecAmount = AppGlobal.StrToDec(row["PrecAmount"].ToString());
                string memo = "";
                string ChargeMode = "CRM";
                long CustID = 0;
                long RoomID = 0;
                if (row.Table.Columns.Contains("memo"))
                {
                    memo = AppGlobal.ChkStr(row["memo"].ToString());
                }
                if (row.Table.Columns.Contains("CustID"))
                {
                    CustID = AppGlobal.StrToLong(row["CustID"].ToString());
                }
                if (row.Table.Columns.Contains("RoomID"))
                {
                    RoomID = AppGlobal.StrToLong(row["RoomID"].ToString());
                }
                if (RoomID == 0 && CustID == 0)
                {
                    return JSONHelper.FromString(false, "客户编码和房屋编码二者必填一项");
                }

                //构建链接字符串
                GetConnectionString(Community);
                string ContionString = Global_Var.CorpSQLConnstr;

                IDbConnection con = new SqlConnection(ContionString);
                StringBuilder sb = new StringBuilder();
                #region 客户和费项 验证


                //客户和费项 验证
                sb.Append("select FeesID from Tb_HSPR_Fees as f with(nolock)  ");
                sb.AppendFormat("inner join funSplitTabel('{0}',',') as t ", FeesIds);
                sb.Append(" on f.FeesID=t.ColName");
                sb.AppendFormat(" where CommID ='{0}'  ", Community.CommID);
                if (CustID > 0)
                {
                    sb.AppendFormat(" and CustID='{0}' ", CustID);
                }
                if (RoomID > 0)
                {
                    sb.AppendFormat(" and RoomID='{0}' ", RoomID);
                }
                DataSet ds = con.ExecuteReader(sb.ToString()).ToDataSet();
                sb.Clear();
                con.Dispose();
                if (ds == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                {
                    return JSONHelper.FromString(false, "业主费用不匹配");
                }
                ds.Dispose();
                #endregion


                #region 费用金额 验证


                //费用金额 验证
                con = new SqlConnection(ContionString);
                sb.Append("select FeesID from Tb_HSPR_Fees as f with(nolock)  ");
                sb.AppendFormat("inner join funSplitTabel('{0}',',') as t ", FeesIds);
                sb.Append(" on f.FeesID=t.ColName");
                sb.AppendFormat(" where CommID ='{0}'  ", Community.CommID);
                if (CustID > 0)
                {
                    sb.AppendFormat(" and CustID='{0}' ", CustID);
                }
                if (RoomID > 0)
                {
                    sb.AppendFormat(" and RoomID='{0}' ", RoomID);
                }
                sb.Append(" and   isnull(DebtsAmount,0)>0");
                ds = con.ExecuteReader(sb.ToString()).ToDataSet();
                sb.Clear();
                con.Dispose();
                if (ds == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                {
                    //如果没有欠费时查询费用的票据信息
                    IDbConnection recon = new SqlConnection(ContionString);
                    string resql = "select ReceID,BillsSign,CustName,RoomSign,BillsDate,PaidAmount from  view_HSPR_FeesReceipts_Filter"
                    + " where ReceID = ("
                    + " select  top 1"
                    + " case ISNULL(f.IsPrec, 0) when 1 then rp1.ReceID else rp.ReceID end as ReceID"
                    + " from"
                    + " Tb_HSPR_Fees as f"
                    + " left join Tb_HSPR_FeesDetail as fd"
                    + " on f.FeesID = fd.FeesID and fd.ChargeAmount > 0 and fd.IsDelete = 0"
                    + " left join Tb_HSPR_FeesReceipts as rp"
                    + " on fd.ReceID = rp.ReceID"
                    + " left join Tb_HSPR_OffsetPreDetail as opd"
                    + " on f.FeesID = opd.FeesID and f.DueAmount = f.PrecAmount and opd.IsDelete = 0"
                    + " left join Tb_HSPR_PreCostsDetail as pcd"
                    + " on f.FeesID = pcd.FeesID and pcd.IsDelete = 0"
                    + " left join Tb_HSPR_FeesReceipts as rp1"
                    + " on pcd.ReceID = rp1.ReceID"
                    + " where f.FeesID in (" + FeesIds + "))";
                    DataSet reds = recon.ExecuteReader(resql).ToDataSet();
                    if (reds != null && reds.Tables.Count > 0 && reds.Tables[0].Rows.Count > 0)
                    {
                        return JSONHelper.FromString(reds.Tables[0]);
                    }
                    else
                    {
                        return JSONHelper.FromString(false, "欠费金额必须大于0");
                    }



                }
                ds.Dispose();
                #endregion


                #region 是否收取 验证


                //是否收取 验证
                con = new SqlConnection(ContionString);
                sb.Append("select FeesID from Tb_HSPR_Fees as f with(nolock)  ");
                sb.AppendFormat("inner join funSplitTabel('{0}',',') as t ", FeesIds);
                sb.Append(" on f.FeesID=t.ColName");
                sb.AppendFormat(" where CommID ='{0}'  ", Community.CommID);
                if (CustID > 0)
                {
                    sb.AppendFormat(" and CustID='{0}' ", CustID);
                }
                if (RoomID > 0)
                {
                    sb.AppendFormat(" and RoomID='{0}' ", RoomID);
                }
                sb.Append(" and   isnull(IsCharge,0)=1");
                ds = con.ExecuteReader(sb.ToString()).ToDataSet();
                sb.Clear();
                con.Dispose();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    return JSONHelper.FromString(false, "该费用已收取");
                }
                ds.Dispose();
                #endregion


                #region 总金额 验证


                //总金额 验证
                con = new SqlConnection(ContionString);
                sb.Append("select isnull(sum(DebtsAmount),0) as DebtsAmount, isnull( sum(LateFeeAmount) ,0) as DebtsLateAmount  from view_HSPR_Fees_SearchFilter as f with(nolock)  ");
                sb.AppendFormat("inner join funSplitTabel('{0}',',') as t ", FeesIds);
                sb.Append(" on f.FeesID=t.ColName");
                sb.AppendFormat(" where CommID ='{0}'  ", Community.CommID);
                if (CustID > 0)
                {
                    sb.AppendFormat(" and CustID='{0}' ", CustID);
                }
                if (RoomID > 0)
                {
                    sb.AppendFormat(" and RoomID='{0}' ", RoomID);
                }
                sb.Append(" and   isnull(IsCharge,0)=0");
                ds = con.ExecuteReader(sb.ToString()).ToDataSet();
                sb.Clear();
                con.Dispose();
                //2017-12-19 金额验证加入违约金
                if (ds == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0 || AppGlobal.StrToDec(ds.Tables[0].Rows[0]["DebtsAmount"].ToString()) + AppGlobal.StrToDec(ds.Tables[0].Rows[0]["DebtsLateAmount"].ToString()) != PrecAmount)
                {
                    return JSONHelper.FromString(false, "下帐金额与实际应付金额不等");
                }
                ds.Dispose();
                #endregion

                #region 冻结费用



                //冻结费用           
                con = new SqlConnection(ContionString);
                sb.Append(" update Tb_HSPR_Fees set IsFreeze = 0 ");
                sb.Append(" where ");
                sb.Append(" FeesID in (");
                sb.Append(" select FeesID  from Tb_HSPR_Fees as f with(nolock)  ");
                sb.AppendFormat("inner join funSplitTabel('{0}',',') as t ", FeesIds);
                sb.Append(" on f.FeesID=t.ColName");
                sb.AppendFormat(" where CommID ='{0}'  ", Community.CommID);
                if (CustID > 0)
                {
                    sb.AppendFormat(" and CustID='{0}' ", CustID);
                }
                if (RoomID > 0)
                {
                    sb.AppendFormat(" and RoomID='{0}' ", RoomID);
                }
                sb.Append(" and   isnull(IsCharge,0)=0");
                sb.Append(" )");
                con.Execute(sb.ToString());
                sb.Clear();
                con.Dispose();
                #endregion

                backstr = ReceFees(ContionString, row, ChargeMode, memo, CustID, RoomID);
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.Message + ex.StackTrace;
            }
            if (ErrorMsg == "")
            {
                return backstr;
            }
            else
            {
                return JSONHelper.FromString(false, ErrorMsg);
            }
        }
        /// <summary>
        /// 实收、预收下帐
        /// </summary>
        /// <param name="CommunityId"></param>
        /// <param name="OrderId"></param>
        public string ReceFees(string ContionString, DataRow Row, string ChargeMode, string memo, long CustID, long RoomID)
        {
            Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(Row["CommID"].ToString());
            IDbConnection con = new SqlConnection(ContionString);
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@CustIDs", CustID);
            dp.Add("@CommID", Community.CommID);
            dp.Add("@RoomIDs", RoomID);

            dp.Add("@ChargeMode", ChargeMode);
            dp.Add("@ReceMemo", memo);

            dp.Add("@FeesIds", Row["FeesIds"].ToString());
            dp.Add("@PrecAmount", Row["PrecAmount"].ToString());

            DataSet ds = con.ExecuteReader("Proc_HSPR_ReceFees", dp, null, null, CommandType.StoredProcedure).ToDataSet();
            if (ds == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
            {
                return JSONHelper.FromString(false, "下帐失败");
            }
            return JSONHelper.FromString(ds.Tables[0]);
        }

        /// <summary>
        /// 查询冲抵记录
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string GetOffsetPreDetail(DataRow row)
        {
            if (!row.Table.Columns.Contains("CustID") || string.IsNullOrEmpty(row["CustID"].ToString()) || AppGlobal.StrToLong(row["CustID"].ToString()) <= 0)
            {
                return JSONHelper.FromString(false, "用户编码不能为空");
            }
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编码不能为空");
            }
            //if (!row.Table.Columns.Contains("RoomID") || string.IsNullOrEmpty(row["RoomID"].ToString()))
            //{
            //    return JSONHelper.FromString(false, "房间编号不能为空");
            //}      

            string CustID = row["CustID"].ToString();
            string CommID = row["CommID"].ToString();
            //查询小区
            Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(CommID);
            //构造链接字符串
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }
            //构建链接字符串
            GetConnectionString(Community);
            StringBuilder sb = new StringBuilder();
            sb.Append("Select CustName,roomid,RoomSign,AuditDate,CostName, Cast(Year(FeesDueDate) as varchar(10) )+'年' + Cast(MONTH(FeesDueDate) as varchar(10)) + '月' as FeesDueDate,OffsetAmount from  view_HSPR_OffsetPreDetail_Filter  ");

            sb.AppendFormat(" where CommID='{0}'", Community.CommID);
            sb.AppendFormat(" and CustID='{0}'", CustID);
            if (row.Table.Columns.Contains("RoomID") && AppGlobal.StrToLong(row["RoomID"].ToString()) > 0)
            {
                sb.AppendFormat(" and RoomID='{0}'", row["RoomID"].ToString());
            }

            DateTime BeginFeesDueDate = new DateTime(2000, 1, 1);

            DateTime EndFeesDueDate = new DateTime(2050, 12, 31);
            if (row.Table.Columns.Contains("FeesStarteDate") && row["FeesStarteDate"].ToString() != "")
            {
                BeginFeesDueDate = AppGlobal.StrToDate(row["FeesStarteDate"].ToString());
            }
            if (row.Table.Columns.Contains("FeesEndDate") && row["FeesEndDate"].ToString() != "")
            {
                EndFeesDueDate = AppGlobal.StrToDate(row["FeesEndDate"].ToString());
            }

            sb.AppendFormat(" and FeesDueDate between '" + BeginFeesDueDate + "' and  '" + EndFeesDueDate + "' ");

            sb.Append("  order by FeesDueDate desc,AuditDate desc");

            DataSet ds = new DbHelperSQLP(Global_Var.CorpSQLConnstr).Query(sb.ToString());
            return JSONHelper.FromString(ds.Tables[0]);

        }

        #region 查询预存费项

        /// <summary>
        /// 查询预存费项
        /// </summary>
        /// <param name="Row"></param>
        /// CommunityId             小区编号
        /// CustID                  客户编号
        /// RoomID                  房间编码
        /// 返回
        ///     key         费项编号
        ///     value       费项名称
        /// <returns></returns>
        private string GetPrecCost(DataRow Row)
        {
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            if (!Row.Table.Columns.Contains("CommunityId"))
            {
                return new ApiResult(true, list).toJson();
            }
            string communityId = Row["CommunityId"].ToString();
            if (string.IsNullOrEmpty(communityId))
            {
                return new ApiResult(true, list).toJson();
            }

            Tb_Community community = GetCommunity(communityId);
            string connStr = GetConnectionStr(community);

            string CustID = Row["CustID"].ToString();
            string RoomID = Row["RoomID"].ToString();

            //是否华南城微信
            bool isHnc = false;
            if (Row.Table.Columns.Contains("Channel") && "hnc".Equals(Row["Channel"].ToString()))
            {
                isHnc = true;
            }

            DataTable dt = null;
            using (IDbConnection conn = new SqlConnection(connStr))
            {
                if (isHnc)
                {
                    //华南城微信不对预存费项做限制
                    dt = conn.ExecuteReader(@"SELECT CostID, CostName FROM view_HSPR_CostStanSetting_Filter 
                                                WHERE CustID = @CustID AND RoomID = @RoomID GROUP BY CostID, CostName",
                                                new { CustID = CustID, RoomID = RoomID }).ToDataSet().Tables[0];
                }
                else
                {
                    dt = conn.ExecuteReader(@"SELECT b.CostID, b.CostName, b.StanID,b.HandID,
                                                (SELECT TOP 1 FeesEndDate FROM Tb_HSPR_Fees a WHERE a.CostID=b.CostID AND a.CustID=b.CustID AND a.RoomID=b.RoomID 
                                                    ORDER BY FeesEndDate DESC) AS FeesEndDate
                                                FROM view_HSPR_CostStanSetting_Filter b
                                                WHERE CustID = @CustID AND RoomID = @RoomID AND ( SysCostSign = 'B0001' OR SysCostSign = 'B0002' )
                                                GROUP BY CostID, CostName, StanID,HandID,CustID,RoomID",
                                                new { CustID = CustID, RoomID = RoomID }).ToDataSet().Tables[0];
                }
            }
            if (null == dt || dt.Rows.Count == 0)
            {
                return new ApiResult(true, list).toJson();
            }
            Dictionary<string, object> dictionary;
            if ("f99195dc-9e4a-4094-9f1d-621b89ef6586".Equals(communityId))
            {
                foreach (DataRow item in dt.Select(" CostName <> '地面停车服务费'"))
                {
                    dictionary = new Dictionary<string, object>();
                    dictionary.Add("key", item["CostName"]);
                    dictionary.Add("value", item["CostID"].ToString());
                    list.Add(dictionary);
                }
            }
            else
            {
                foreach (DataRow item in dt.Rows)
                {
                    dictionary = new Dictionary<string, object>();
                    dictionary.Add("key", item["CostName"]);
                    dictionary.Add("value", item["CostID"].ToString());
                    if (dt.Columns.Contains("StanID"))
                    {
                        dictionary.Add("StanID", item["StanID"]);
                    }
                    if (dt.Columns.Contains("HandID"))
                    {
                        dictionary.Add("HandID", item["HandID"]);
                    }
                    if (dt.Columns.Contains("FeesEndDate"))
                    {
                        if (item["FeesEndDate"] != null && item["FeesEndDate"].ToString() != "")
                        {
                            DateTime dateTime = AppGlobal.StrToDate(item["FeesEndDate"].ToString());
                            dateTime = dateTime.AddMonths(1);

                            dictionary.Add("FeesEndDate", dateTime.ToString("yyyy-MM-01 00:00:00"));
                        }
                        else
                        {
                            dictionary.Add("FeesEndDate", DateTime.Now.ToString("yyyy-MM-01 00:00:00"));
                        }
                    }

                    list.Add(dictionary);
                }
            }
            return new ApiResult(true, list).toJson();
        }


        private string GetPrecCost2(DataRow row)
        {
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(true, "缺少参数CommID");
            }
            if (!row.Table.Columns.Contains("CustID") || string.IsNullOrEmpty(row["CustID"].ToString()))
            {
                return JSONHelper.FromString(true, "缺少参数CommID");
            }
            if (!row.Table.Columns.Contains("RoomID") || string.IsNullOrEmpty(row["RoomID"].ToString()))
            {
                return JSONHelper.FromString(true, "缺少参数CommID");
            }
            string CommID = row["CommID"].ToString();
            string CustID = row["CustID"].ToString();
            string RoomID = row["RoomID"].ToString();

            Tb_Community tb_Community = GetCommunity(CommID);
            PubConstant.hmWyglConnectionString = GetConnectionStr(tb_Community);

            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var resultSet = conn.Query(@"SELECT CostID, CostName FROM view_HSPR_CostStanSetting_Filter WHERE CustID=@CustID AND RoomID=@RoomID",
                                            new { CustID = CustID, RoomID = RoomID });

                return new ApiResult(true, resultSet).toJson();
            }
        }

        #endregion

        #region 未缴费用
        /// <summary>
        /// 未缴费用查询
        /// </summary>
        /// 用户编码：CustID【必填】
        /// 小区编码：CommID[必填]    
        /// 房屋编号：RoomID 【选填】
        /// 返回信息：
        ///         费用编号：FeesID
        ///         费项：CostName
        ///         应收：DueAmount
        ///         减免金额：WaivAmount
        ///         冲抵：PrecAmount
        /// <param name="row"></param>
        /// <returns></returns>
        private string GetArrearsList(DataRow row)
        {
            if (!row.Table.Columns.Contains("CustID") || string.IsNullOrEmpty(row["CustID"].ToString()))
            {
                return JSONHelper.FromString(false, "用户编码不能为空");
            }
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编码不能为空");
            }
            string RoomID = "";
            if (row.Table.Columns.Contains("RoomID"))
            {
                RoomID = row["RoomID"].ToString();
            }

            string CustID = row["CustID"].ToString();
            string CommID = row["CommID"].ToString();

            string IgonCorpCostIDStr = "";
            //不显示的费项CorpCostID
            if (row.Table.Columns.Contains("IgonCorpCostID"))
            {
                IgonCorpCostIDStr = row["IgonCorpCostID"].ToString();
            }
            List<string> IgonCorpCostIDList = new List<string>();
            if (!string.IsNullOrEmpty(IgonCorpCostIDStr))
            {
                foreach (var item in IgonCorpCostIDStr.Split(','))
                {
                    if (!string.IsNullOrEmpty(item) && !IgonCorpCostIDList.Contains(item))
                    {
                        IgonCorpCostIDList.Add(item);
                    }
                }
            }

            //查询小区
            Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(CommID);
            //构造链接字符串
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }
            //构建链接字符串
            GetConnectionString(Community);

            //应收DueAmount
            //减免WaivAmount
            //冲抵PrecAmount
            //欠费DebtsAmount
            //应收-减免-冲抵=欠费
            //构造查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append(@"SELECT x.FeesID,x.CostName,x.CustName,x.FeeDueYearMonth,isnull(x.DebtsAmount,0) AS DueAmount,x.MeterID,x.SysCostSign,
                        isnull(x.WaivAmount,0) AS WaivAmount,isnull(x.LateFeeAmount,0) AS LateFeeAmount,x.StanMemo,x.FeesStateDate,x.FeesEndDate
                        FROM view_HSPR_Fees_SearchFilter x WHERE x.DebtsAmount>0 AND isnull(x.IsCharge,0)=0 
                        AND isnull(x.IsBank,0)=0 AND isnull(x.IsPrec,0)=0 AND isnull(IsFreeze,0)=0");
            sb.AppendFormat(" AND x.CommID={0}", Community.CommID);
            sb.AppendFormat(" AND x.CustID={0}", CustID);
            if (RoomID != "")
            {
                sb.AppendFormat(" AND x.RoomID={0}", RoomID);
            }

            if (Community.CommID == "182101")
            {
                sb.Append(" AND x.CostName<>'单位物业管理费1' AND x.CostName<> '单位物业管理费2' AND x.CostName<>'单位车位物管费1' AND x.CostName<>'单位车位物管费2' ");
            }
            // 鸿坤，物管类都能交
            if (Community.CorpID == 1973)
            {
                sb.Append(" AND (x.SysCostSign='B0001' OR x.CostName = '公共照明' OR x.CostName = '生活垃圾清运' OR x.CommID='197302') ");
            }
            if (Community.CorpID == 1975)
            {
                sb.Append(" AND x.accountsduedate<dateadd(day,-1,dateadd(month,2,convert(nvarchar(7),getdate(),120)+'-1' )) ");
            }
            if (IgonCorpCostIDList.Count > 0)
            {
                sb.Append(" AND x.CorpCostID NOT IN ('" + string.Join("','", IgonCorpCostIDList.ToArray()) + "')");
            }
            sb.Append(" ORDER BY FeesDueDate DESC");

            DataSet ds = new DbHelperSQLP(Global_Var.CorpSQLConnstr).Query(sb.ToString());
            return GetDataSetForGroupJsonn(ds);
        }

        #endregion

        #region 本期账单
        /// <summary>
        /// 本期账单
        /// </summary>
        /// 用户编码：CustID【必填】
        /// 小区编码：CommID[必填]
        /// 开始日期：StateDate【必填】
        /// 结束日期：EndDate【必填】
        /// 房屋编号：RoomID 【选填】
        /// 返回信息：
        ///         费项：CostName
        ///         单价：Price
        ///         数量：Number
        ///         金额：DueAmount
        /// <param name="row"></param>
        /// <returns></returns>
        private string GetBillList(DataRow row)
        {
            if (!row.Table.Columns.Contains("CustID") || string.IsNullOrEmpty(row["CustID"].ToString()))
            {
                return JSONHelper.FromString(false, "用户编码不能为空");
            }
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编码不能为空");
            }
            if (!row.Table.Columns.Contains("StateDate") || string.IsNullOrEmpty(row["StateDate"].ToString()))
            {
                return JSONHelper.FromString(false, "开始日期不能为空");
            }
            if (!row.Table.Columns.Contains("EndDate") || string.IsNullOrEmpty(row["EndDate"].ToString()))
            {
                return JSONHelper.FromString(false, "结束日期不能为空");
            }
            if (AppGlobal.StrToDate(row["EndDate"].ToString()) < AppGlobal.StrToDate(row["StateDate"].ToString()))
            {
                return JSONHelper.FromString(false, "开始日期不能小于结束日期");
            }

            string RoomID = "";
            if (row.Table.Columns.Contains("RoomID"))
            {
                RoomID = row["RoomID"].ToString();
            }

            string CustID = row["CustID"].ToString();
            string CommID = row["CommID"].ToString();

            //查询小区
            Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(CommID);
            //构造链接字符串
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }
            //构建链接字符串
            GetConnectionString(Community);

            //构造查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append(" select FeesID,CostName,DebtsAmount,DueAmount,StanMemo,SysCostSign,FeesChargeDate,FeesDueDate,FeesStateDate,FeesEndDate,ISNULL(isCharge,0) as isCharge, ISNULL(IsPrec,0) as IsPrec from view_HSPR_Fees_Filter ");
            sb.AppendFormat(" where CommID={0}", Community.CommID);
            sb.AppendFormat(" and CustID={0}", CustID);
            if (RoomID != "")
            {
                sb.AppendFormat(" and RoomID={0}", RoomID);
            }
            sb.AppendFormat(" and FeesDueDate between '{0}' and '{1}'", row["StateDate"].ToString(), row["EndDate"].ToString());
            sb.Append(" and IsBank =0 and IsFreeze=0 and IsProperty=0 and ISNULL(IsPrec,0)=0");
            //去掉IsCharge=0  and DebtsAmount>0限制
            //sb.Append(" and IsBank =0 and IsFreeze=0 and IsProperty=0 and IsCharge=0  and DebtsAmount>0  and ISNULL(IsPrec,0)=0");
            sb.Append("  order by FeesID desc ");
            DataSet ds = new DbHelperSQLP(Global_Var.CorpSQLConnstr).Query(sb.ToString());
            //动态构建本期账单
            DataTable dt = SetTable(ds);

            return JSONHelper.FromString(dt);
        }

        #endregion

        #region 历史缴费
        /// <summary>
        /// 历史缴费
        /// </summary>
        /// 用户编码：CustID【必填】
        /// 小区编码：CommID[必填]
        /// 开始日期：StateDate【必填】
        /// 结束日期：EndDate【必填】
        /// 房屋编号：RoomID 【选填】
        /// 页码：PageIndex【必填】默认1
        /// 分页条数：PageSize【必填】默认20
        /// 返回信息：
        ///         缴费方式：ChargeMode
        ///         缴费期间：FeeDueYearMonth
        ///         费项：CostName
        ///         金额：ChargeAmount
        ///         缴费类型：BillType
        /// <param name="row"></param>
        /// <returns></returns>
        private string GetHistoricalPaymentList(DataRow row)
        {
            if (!row.Table.Columns.Contains("CustID") || string.IsNullOrEmpty(row["CustID"].ToString()))
            {
                return JSONHelper.FromString(false, "用户编码不能为空");
            }
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编码不能为空");
            }
            if (!row.Table.Columns.Contains("StateDate") || string.IsNullOrEmpty(row["StateDate"].ToString()))
            {
                return JSONHelper.FromString(false, "开始日期不能为空");
            }
            if (!row.Table.Columns.Contains("EndDate") || string.IsNullOrEmpty(row["EndDate"].ToString()))
            {
                return JSONHelper.FromString(false, "结束日期不能为空");
            }
            if (AppGlobal.StrToDate(row["EndDate"].ToString()) < AppGlobal.StrToDate(row["StateDate"].ToString()))
            {
                return JSONHelper.FromString(false, "开始日期不能小于结束日期");
            }

            int PageIndex = 1;
            int PageSize = 20;
            string RoomID = "0";
            if (row.Table.Columns.Contains("RoomID"))
            {
                RoomID = row["RoomID"].ToString();
            }
            if (row.Table.Columns.Contains("PageIndex") && AppGlobal.StrToInt(row["PageIndex"].ToString()) > 0)
            {
                PageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());
            }
            if (row.Table.Columns.Contains("PageSize") && AppGlobal.StrToInt(row["PageSize"].ToString()) > 0)
            {
                PageSize = AppGlobal.StrToInt(row["PageSize"].ToString());
            }
            string CustID = row["CustID"].ToString();
            string CommID = row["CommID"].ToString();

            //查询小区
            Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(CommID);
            //构造链接字符串
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }

            //构建链接字符串
            GetConnectionString(Community);

            //生成票据信息
            int rowsAffected;
            SqlParameter[] sqlstr = {
                new SqlParameter("@CommID",SqlDbType.Int),
                new SqlParameter("@CustID",SqlDbType.BigInt),
                new SqlParameter("@RoomID",SqlDbType.BigInt)
            };
            sqlstr[0].Value = Community.CommID;
            sqlstr[1].Value = CustID;
            sqlstr[2].Value = RoomID;
            new DbHelperSQLP(Global_Var.CorpSQLConnstr).RunProcedure("Proc_HSPR_CustomerBillSign_Cre", sqlstr, out rowsAffected);

            //查询历史缴费单据
            StringBuilder ss = new StringBuilder();
            ss.Append("select CommID,BillsDate,BillsAmount,ReceID,BillType,ChargeMode from view_HSPR_CustomerBillSign_Filter");
            ss.AppendFormat(" where CommID={0}", Community.CommID);
            ss.AppendFormat(" and CustID={0}", CustID);
            ss.AppendFormat("  and RoomID={0}", RoomID);
            ss.AppendFormat(" and BillsDate between '{0}' and '{1}'", row["StateDate"].ToString(), row["EndDate"].ToString());
            ss.Append(" and IsDelete=0");

            int pageCount;
            int Counts;
            DataSet ds_lishi = GetList(out pageCount, out Counts, ss.ToString(), PageIndex, PageSize, "BillsDate", 1, "BillsDate");

            return GetResult(row, RoomID, CustID, Community, ds_lishi);
        }

        private string GetResult(DataRow row, string RoomID, string CustID, Tb_Community Community, DataSet ds)
        {
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            if (null == ds || ds.Tables.Count == 0)
            {
                return new ApiResult(true, list).toJson();
            }
            DataTable dt_history = ds.Tables[0];
            if (null == dt_history || dt_history.Rows.Count == 0)
            {
                return new ApiResult(true, list).toJson();
            }
            Dictionary<string, object> dictionary;
            foreach (DataRow item in dt_history.Rows)
            {
                dictionary = new Dictionary<string, object>();
                dictionary.Add("BillsAmount", item["BillsAmount"]);
                dictionary.Add("BillsDate", item["BillsDate"]);
                dictionary.Add("ChargeMode", item["ChargeMode"]);

                DataSet dataSet = null;

                //BillType缴费类型：1实收，2预存，3退款，4预存退款
                //获取明细 类型为1 实收
                if ("1".Equals(item["BillType"].ToString()))
                {
                    SqlParameter[] par = {
                            new SqlParameter("@CommID",SqlDbType.Int),
                            new SqlParameter("@ReceID",SqlDbType.BigInt),
                            new SqlParameter("@ReceiptType",SqlDbType.Int)
                        };
                    par[0].Value = item["CommID"].ToString();
                    par[1].Value = item["ReceID"].ToString();
                    par[2].Value = 1;
                    dataSet = new DbHelperSQLP(Global_Var.CorpSQLConnstr).RunProcedure("Proc_HSPR_NewFeesReceipts_DetailFilter", par, "RetDataSet");

                }
                else if ("2".Equals(item["BillType"].ToString()) || "4".Equals(item["BillType"].ToString()))
                {
                    string strsql = "select  CommID,CostID,CustID,RoomID,CostName,PrecAmount as TotalAmount,CONVERT(varchar(50), YEAR( PrecDate))+'年'+CONVERT(varchar(50), MONTH (PrecDate))+'月' as YearMonth, '预存' as 'BillType'  from  view_HSPR_PreCostsDetail_Filter where CommID=" + item["CommID"].ToString() + " and ReceID='" + item["ReceID"].ToString() + "' and IsDelete=0 and isnull(SourceType,0) = 0 ";
                    dataSet = new DbHelperSQLP(Global_Var.CorpSQLConnstr).Query(strsql);
                }

                if (null == dataSet || dataSet.Tables.Count == 0)
                {
                    dictionary.Add("data", new ArrayList());
                    list.Add(dictionary);
                    continue;
                }
                DataTable dataTable = dataSet.Tables[0];
                if (null == dataTable || dataTable.Rows.Count == 0)
                {
                    dictionary.Add("data", new ArrayList());
                    list.Add(dictionary);
                    continue;
                }
                if (!dataTable.Columns.Contains("BillType"))
                {
                    dataTable.Columns.Add(new DataColumn("BillType"));
                }
                List<Dictionary<string, object>> dataList = new List<Dictionary<string, object>>();
                HashSet<string> yearMonthList = new HashSet<string>();
                foreach (DataRow row_Item in dataTable.Rows)
                {
                    string billType = row_Item["BillType"].ToString();
                    if (string.IsNullOrEmpty(billType))
                    {
                        row_Item["BillType"] = "实收";
                    }
                    yearMonthList.Add(row_Item["YearMonth"].ToString());
                }
                if (null == yearMonthList || yearMonthList.Count == 0)
                {
                    dictionary.Add("data", dataList);
                    list.Add(dictionary);
                    continue;
                }
                Dictionary<string, object> dataDictionary;
                foreach (string yearMonth in yearMonthList)
                {
                    dataDictionary = new Dictionary<string, object>();
                    dataDictionary.Add("Title", yearMonth);

                    List<Dictionary<string, object>> contentList = new List<Dictionary<string, object>>();
                    Dictionary<string, object> contentDictionary;
                    foreach (var item_row in dataTable.Select("YearMonth='" + yearMonth + "'"))
                    {
                        contentDictionary = new Dictionary<string, object>();
                        contentDictionary.Add("CommID", item_row["CommID"]);
                        contentDictionary.Add("CustID", item_row["CustID"]);
                        contentDictionary.Add("RoomID", item_row["RoomID"]);
                        contentDictionary.Add("CostID", item_row["CostID"]);
                        contentDictionary.Add("YearMonth", item_row["YearMonth"]);
                        contentDictionary.Add("CostName", item_row["CostName"]);
                        //实收查询过程中有查询包含违约金的字段    (isnull(ChargeAmount,0) + isnull(LateFeeAmount,0)) as TotalAmount
                        contentDictionary.Add("ChargeAmount", item_row["TotalAmount"]);
                        contentDictionary.Add("BillType", item_row["BillType"]);
                        contentList.Add(contentDictionary);
                    }
                    dataDictionary.Add("Content", contentList);
                    dataList.Add(dataDictionary);
                }
                dictionary.Add("data", dataList);
                list.Add(dictionary);
            }

            return new ApiResult(true, list).toJson();
        }

        #endregion

        #region 预缴费用
        /// <summary>
        /// 预缴费用
        /// </summary>
        /// 用户编码：CustID【必填】
        /// 小区编码：CommID[必填]
        /// 房屋编号：RoomID 【选填】
        /// 返回信息：
        ///         预缴金额：PrecAmount
        ///         预缴费项：CostNames
        /// 
        /// <param name="row"></param>
        /// <returns></returns>
        private string GetAdvanceList(DataRow row)
        {
            if (!row.Table.Columns.Contains("CustID") || string.IsNullOrEmpty(row["CustID"].ToString()))
            {
                return JSONHelper.FromString(false, "用户编码不能为空");
            }
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编码不能为空");
            }

            string CustID = row["CustID"].ToString();
            string CommID = row["CommID"].ToString();
            long RoomID = 0;
            if (row.Table.Columns.Contains("RoomID"))
            {
                RoomID = AppGlobal.StrToLong(row["RoomID"].ToString());
            }
            //查询小区
            Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(CommID);
            //构造链接字符串
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }

            //构建链接字符串
            GetConnectionString(Community);

            string sql = $@"SELECT PrecID,[value],[value] as PrecAmount,[key],[key] as CostNames,
                            RoomID FROM (SELECT PrecID,PrecAmount AS [value],case when CostID=0 OR 
                            (CostNames is null or CAST(CostNames as varchar(max))='') 
                            then '通用预存' else CAST(CostNames as varchar(max)) end AS[key],
                            RoomID  FROM view_HSPR_PreCosts_Filter
                WHERE CommID = {Community.CommID} AND CustID = {CustID} AND (RoomID = {RoomID} OR RoomID=0) AND PrecAmount>0 ) as a";
            if (Global_Var.LoginCorpID == "2045")//嘉和需求
            {

                 sql = $@"SELECT PrecID,[value],[value] as PrecAmount,[key],[key] as CostNames,
                                RoomID,ParkName FROM (SELECT PrecID,PrecAmount AS [value],case when CostID=0 OR 
                                (CostNames is null or CAST(CostNames as varchar(max))='') 
                                then '通用预存' else CAST(CostNames as varchar(max)) end AS[key],
                                RoomID,ParkName  FROM view_HSPR_PreCosts_Filter
                    WHERE CommID = {Community.CommID} AND CustID = {CustID} AND (RoomID = {RoomID} OR RoomID=0) AND PrecAmount>0 ) as a";
            }


            DataSet ds = new DbHelperSQLP(Global_Var.CorpSQLConnstr).Query(sql.ToString());
            return JSONHelper.FromString(ds.Tables[0]);
        }

        #endregion


        #region 查询是否欠费
        /// <summary>
        /// 查询是否欠费  CheckArrears
        /// CustID 用户编码
        /// CommID 小区编码
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public string CheckArrears(DataRow row)
        {
            if (!row.Table.Columns.Contains("CustID") || string.IsNullOrEmpty(row["CustID"].ToString()))
            {
                return JSONHelper.FromString(false, "用户编码不能为空");
            }
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编码不能为空");
            }

            string CustID = row["CustID"].ToString();
            string CommID = row["CommID"].ToString();
            string RoomID = "";
            if (row.Table.Columns.Contains("RoomID") && !string.IsNullOrEmpty(row["RoomID"].ToString()))
            {
                RoomID = row["RoomID"].ToString();
            }
            //查询小区
            Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(CommID);
            //构造链接字符串
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }
            //构建链接字符串
            GetConnectionString(Community);

            StringBuilder sb = new StringBuilder();
            sb.Append(" select  FeesID from view_HSPR_Fees_Filter ");
            sb.AppendFormat(" where CommID={0}", Community.CommID);
            sb.AppendFormat(" and CustID={0}", CustID);
            if (string.IsNullOrEmpty(RoomID) == false)
            {
                sb.AppendFormat(" and RoomID={0}", RoomID);
            }
            // 2017年7月10日17:10:41，谭洋，新增费用应收时间限制功能
            sb.Append(" and AccountsDueDate<GETDATE()");
            sb.Append(" and DebtsAmount > 0 and isnull(IsCharge,0) = 0 and isnull(IsBank,0) = 0  and Isnull(IsPrec,0) = 0 and isnull(IsFreeze,0) = 0 and isnull(IsBank,0) = 0");
            DataSet ds = new DbHelperSQLP(Global_Var.CorpSQLConnstr).Query(sb.ToString());
            int count = ds.Tables[0].Rows.Count;
            if (count > 0)
            {
                return JSONHelper.FromString(true, "1");
            }
            else
            {
                return JSONHelper.FromString(true, "0");
            }
        }

        #endregion

        #region 计算预交费用
        /// <summary>
        /// 计算预存费用CalcAdvancePrice
        /// 
        /// </summary>
        /// <param name="row"></param>
        /// CustID                  用户编码
        /// CommID                  小区编码
        /// RoomID                  房间编码
        /// CostIDs                 费项编码
        /// FeesStateDate           开始日期
        /// FeesEndDate             结束日期
        /// 返回：
        ///     false    错误信息
        ///     true    data:["yyyy-mm-dd 费项 *****","*****"],Count:90.34
        /// <returns></returns>        
        private string CalcAdvancePrice(DataRow row)
        {
            if (!row.Table.Columns.Contains("CustID") || string.IsNullOrEmpty(row["CustID"].ToString()))
            {
                return JSONHelper.FromString(false, "用户编码不能为空");
            }
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编码不能为空");
            }
            if (!row.Table.Columns.Contains("RoomID") || string.IsNullOrEmpty(row["RoomID"].ToString()))
            {
                return JSONHelper.FromString(false, "房间编码不能为空");
            }
            if (!row.Table.Columns.Contains("CostIDs") || string.IsNullOrEmpty(row["CostIDs"].ToString()))
            {
                return JSONHelper.FromString(false, "费项编码不能为空");
            }

            if (!row.Table.Columns.Contains("FeesStateDate") || string.IsNullOrEmpty(row["FeesStateDate"].ToString()))
            {
                return JSONHelper.FromString(false, "开始日期不能为空");
            }
            if (!row.Table.Columns.Contains("FeesEndDate") || string.IsNullOrEmpty(row["FeesEndDate"].ToString()))
            {
                return JSONHelper.FromString(false, "结束日期不能为空");
            }
            if (AppGlobal.StrToDate(row["FeesEndDate"].ToString()) < AppGlobal.StrToDate(row["FeesStateDate"].ToString()))
            {
                return JSONHelper.FromString(false, "结束日期不能小于开始日期");
            }

            string CustID = row["CustID"].ToString();
            string CommID = row["CommID"].ToString();
            string RoomID = row["RoomID"].ToString();
            string CostIDs = row["CostIDs"].ToString();
            string FeesStartDate = row["FeesStateDate"].ToString();
            string FeesEndDate = row["FeesEndDate"].ToString();

            //查询小区
            Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(CommID);
            //构造链接字符串
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }
            //构建链接字符串
            GetConnectionString(Community);

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" and CommID={0}", Community.CommID);
            sb.AppendFormat(" and CustID={0}", CustID);
            sb.AppendFormat(" and RoomID={0}", RoomID);
            sb.AppendFormat(" and CostID={0}", CostIDs);

            SqlParameter[] parameters = {
                    new SqlParameter("@SQLEx", SqlDbType.VarChar, 255)
                    };
            parameters[0].Value = sb.ToString();

            DataSet Ds = new DbHelperSQLP(Global_Var.CorpSQLConnstr).RunProcedure("Proc_HSPR_CostStanSetting_Filter", parameters, "RetDataSet");

            if (Ds == null || Ds.Tables.Count <= 0)
            {
                return JSONHelper.FromString(true, "未查询到该费项");
            }


            long iRoomID = 0;
            long iCustID = 0;
            decimal iCostAmount = 0;
            try
            {
                foreach (DataRow DRow in Ds.Tables[0].Rows)
                {
                    long iStanID = AppGlobal.StrToLong(DRow["StanID"].ToString());
                    iRoomID = AppGlobal.StrToLong(DRow["RoomID"].ToString());
                    iCustID = AppGlobal.StrToLong(DRow["CustID"].ToString());
                    long iCostID = AppGlobal.StrToLong(DRow["CostID"].ToString());
                    string strCostName = DRow["CostName"].ToString();
                    string strRoomSign = DRow["RoomSign"].ToString();

                    //获取月份差
                    int iChargeCycle = (AppGlobal.StrToDate(FeesEndDate).Year - AppGlobal.StrToDate(FeesStartDate).Year) * 12 + (AppGlobal.StrToDate(FeesEndDate).Month - AppGlobal.StrToDate(FeesStartDate).Month);

                    sb = new StringBuilder();
                    sb.Append("{\"Result\":\"true\",");
                    sb.Append("\"data\":[");
                    for (int i = 1; i <= iChargeCycle; i++)
                    {
                        string str = "";
                        //DataTable dTableCalc = (new BusinessRule.TWBusinRule(LoginSQLConnStr)).HSPR_Fees_CalcAmount(CommID, iCustID, iRoomID, iHandID, iCostID, iStanID, FeesStateDate, FeesEndDate, 0, 1);

                        SqlParameter[] paramete = {
                            new SqlParameter("@CommID",  SqlDbType.Int),
                            new SqlParameter("@CustID",  SqlDbType.BigInt),
                            new SqlParameter("@RoomID",  SqlDbType.BigInt),

                            new SqlParameter("@HandID",  SqlDbType.BigInt),
                            new SqlParameter("@CostID",  SqlDbType.BigInt),
                            new SqlParameter("@StanID",  SqlDbType.BigInt),

                            new SqlParameter("@FeesStateDate",  SqlDbType.NVarChar),
                            new SqlParameter("@FeesEndDate",  SqlDbType.NVarChar),
                            new SqlParameter("@Amount",  SqlDbType.Decimal),

                            new SqlParameter("@Amount2",  SqlDbType.Decimal)
                        };
                        paramete[0].Value = Community.CommID;
                        paramete[1].Value = CustID;
                        paramete[2].Value = iRoomID;
                        paramete[3].Value = 0;
                        paramete[4].Value = iCostID;

                        paramete[5].Value = iStanID;
                        paramete[6].Value = FeesStartDate;
                        paramete[7].Value = FeesEndDate;
                        paramete[8].Value = 0;
                        paramete[9].Value = 1;

                        DataTable dTableCalc = new DbHelperSQLP(Global_Var.CorpSQLConnstr).RunProcedure("Proc_HSPR_Fees_CalcAmount", paramete, "RetDataSet").Tables[0];

                        if (dTableCalc.Rows.Count > 0)
                        {
                            DataRow DRowCalc = dTableCalc.Rows[0];
                            iCostAmount = iCostAmount + AppGlobal.StrToDec(DRowCalc["DueAmount"].ToString());

                            //strErrorMsg = strErrorMsg + strRoomSign + " "
                            //        + iCreYear.ToString() + "年"
                            //        + iCreMonth.ToString() + "月 "
                            //        + strCostName + ":"
                            //        + DRowCalc["ErrorMsg"].ToString() + "<br>";
                            if (i > 1)
                            {
                                str = "," + strRoomSign + "  " + AppGlobal.StrToDate(FeesStartDate).AddMonths(1).ToString("yyyy-MM-dd") + strCostName + ":" + DRowCalc["ErrorMsg"].ToString();
                            }
                            else
                            {
                                str = strRoomSign + "  " + AppGlobal.StrToDate(FeesStartDate).AddMonths(1).ToString("yyyy-MM-dd") + strCostName + ":" + DRowCalc["ErrorMsg"].ToString();
                            }

                            sb.Append(str);
                        }
                        dTableCalc.Dispose();
                    }
                    sb.Append("],\"Count\":\"" + iCostAmount + "\"}");
                }
            }
            catch (Exception ex)
            {
                return JSONHelper.FromString(false, ex.Message + Environment.NewLine + ex.StackTrace);
            }

            return JSONHelper.FromString(sb.ToString());
        }

        /// <summary>
        /// 计算预存费用CalcAdvancePrice
        /// <para>谭洋，2017年6月16日11:48:16</para>
        /// </summary>
        /// <param name="row"></param>
        /// CustID                  用户编码
        /// CommID                  小区编码
        /// RoomID                  房间编码
        /// CostIDs                 费项编码
        /// FeesStateDate           开始日期
        /// FeesEndDate             结束日期
        /// <returns></returns>    
        public string CalcAdvancePrice_TY(DataRow row)
        {
            if (!row.Table.Columns.Contains("CustID") || string.IsNullOrEmpty(row["CustID"].ToString()))
            {
                return JSONHelper.FromString(false, "用户编码不能为空");
            }
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编码不能为空");
            }
            if (!row.Table.Columns.Contains("RoomID") || string.IsNullOrEmpty(row["RoomID"].ToString()))
            {
                return JSONHelper.FromString(false, "房间编码不能为空");
            }
            if (!row.Table.Columns.Contains("CostIDs") || string.IsNullOrEmpty(row["CostIDs"].ToString()))
            {
                return JSONHelper.FromString(false, "费项编码不能为空");
            }

            if (!row.Table.Columns.Contains("FeesStateDate") || string.IsNullOrEmpty(row["FeesStateDate"].ToString()))
            {
                return JSONHelper.FromString(false, "开始日期不能为空");
            }
            if (!row.Table.Columns.Contains("FeesEndDate") || string.IsNullOrEmpty(row["FeesEndDate"].ToString()))
            {
                return JSONHelper.FromString(false, "结束日期不能为空");
            }
            if (AppGlobal.StrToDate(row["FeesEndDate"].ToString()) < AppGlobal.StrToDate(row["FeesStateDate"].ToString()))
            {
                return JSONHelper.FromString(false, "结束日期不能小于开始日期");
            }

            string CustID = row["CustID"].ToString();
            string CommID = row["CommID"].ToString();
            string RoomID = row["RoomID"].ToString();
            string CostIDs = row["CostIDs"].ToString();
            string FeesStartDate = row["FeesStateDate"].ToString();
            string FeesEndDate = row["FeesEndDate"].ToString();

            return CalcAdvancePrice_TY(CustID, CommID, RoomID, CostIDs, FeesStartDate, FeesEndDate);
        }

        public string CalcAdvancePrice_TY(string CustID, string CommID, string RoomID, string CostIDs, string FeesStartDate, string FeesEndDate)
        {
            //查询小区
            Tb_Community Community = GetCommunity(CommID);
            //构造链接字符串
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }
            //构建链接字符串
            GetConnectionString(Community);

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" and CommID={0}", Community.CommID);
            sb.AppendFormat(" and CustID={0}", CustID);
            sb.AppendFormat(" and RoomID={0}", RoomID);
            sb.AppendFormat(" and CostID={0}", CostIDs);

            SqlParameter[] parameters = {
                    new SqlParameter("@SQLEx", SqlDbType.VarChar, 255)
                    };
            parameters[0].Value = sb.ToString();

            DataSet Ds = new DbHelperSQLP(Global_Var.CorpSQLConnstr).RunProcedure("Proc_HSPR_CostStanSetting_Filter", parameters, "RetDataSet");

            if (Ds == null || Ds.Tables.Count <= 0)
            {
                return JSONHelper.FromString(true, "未查询到该费项");
            }


            long iRoomID = 0;
            long iCustID = 0;
            decimal iCostAmount = 0;

            try
            {
                foreach (DataRow DRow in Ds.Tables[0].Rows)
                {
                    long iStanID = AppGlobal.StrToLong(DRow["StanID"].ToString());
                    iRoomID = AppGlobal.StrToLong(DRow["RoomID"].ToString());
                    iCustID = AppGlobal.StrToLong(DRow["CustID"].ToString());
                    long iCostID = AppGlobal.StrToLong(DRow["CostID"].ToString());
                    string strCostName = DRow["CostName"].ToString();
                    string strRoomSign = DRow["RoomSign"].ToString();

                    var arr1 = FeesStartDate.Split(new[] { '-', '/' });
                    var arr2 = FeesEndDate.Split(new[] { '-', '/' });

                    DateTime date1 = new DateTime(AppGlobal.StrToInt(arr1[0]), AppGlobal.StrToInt(arr1[1]), 1, 0, 0, 0);
                    DateTime date2 = new DateTime(AppGlobal.StrToInt(arr2[0]), AppGlobal.StrToInt(arr2[1]), 1, 0, 0, 0);
                    date2 = date2.AddMonths(1).AddDays(-1);

                    DataTable table = new DataTable();
                    table.Columns.Add("RoomSign", typeof(string));
                    table.Columns.Add("StartDate", typeof(string));
                    table.Columns.Add("EndDate", typeof(string));
                    table.Columns.Add("CostName", typeof(string));
                    table.Columns.Add("Amount", typeof(decimal));

                    while (date1 < date2)
                    {
                        string str = "";
                        SqlParameter[] paramete = {
                            new SqlParameter("@CommID",  SqlDbType.Int),
                            new SqlParameter("@CustID",  SqlDbType.BigInt),
                            new SqlParameter("@RoomID",  SqlDbType.BigInt),

                            new SqlParameter("@HandID",  SqlDbType.BigInt),
                            new SqlParameter("@CostID",  SqlDbType.BigInt),
                            new SqlParameter("@StanID",  SqlDbType.BigInt),

                            new SqlParameter("@FeesStateDate",  SqlDbType.DateTime),
                            new SqlParameter("@FeesEndDate",  SqlDbType.DateTime),
                            new SqlParameter("@Amount",  SqlDbType.Decimal),

                            new SqlParameter("@Amount2",  SqlDbType.Decimal)
                        };
                        paramete[0].Value = Community.CommID;
                        paramete[1].Value = CustID;
                        paramete[2].Value = iRoomID;
                        paramete[3].Value = 0;
                        paramete[4].Value = iCostID;

                        paramete[5].Value = iStanID;
                        paramete[6].Value = date1;
                        paramete[7].Value = date1.AddMonths(1).AddDays(-1);
                        paramete[8].Value = 0;
                        paramete[9].Value = 1;

                        DataTable dTableCalc = new DbHelperSQLP(Global_Var.CorpSQLConnstr).RunProcedure("Proc_HSPR_Fees_CalcAmount", paramete, "RetDataSet").Tables[0];

                        if (dTableCalc.Rows.Count > 0)
                        {
                            DataRow DRowCalc = dTableCalc.Rows[0];
                            decimal amount = AppGlobal.StrToDec(DRowCalc["DueAmount"].ToString());
                            iCostAmount = iCostAmount + amount;

                            //dataList.Add(AppGlobal.StrToDate(FeesStateDate).AddMonths(1).ToString("yyyy-MM-dd") + ":" + strRoomSign + ":" + strCostName + ":" + amount + ":" + DRowCalc["ErrorMsg"].ToString());

                            DataRow resultRow = table.NewRow();
                            resultRow["RoomSign"] = strRoomSign;
                            resultRow["StartDate"] = date1.ToShortDateString();
                            resultRow["EndDate"] = date1.AddMonths(1).AddDays(-1).ToShortDateString();
                            resultRow["CostName"] = strCostName;
                            resultRow["Amount"] = amount;
                            table.Rows.Add(resultRow);
                        }
                        dTableCalc.Dispose();

                        date1 = date1.AddMonths(1);
                    }

                    return JSONHelper.FromString(table);
                }

            }
            catch (Exception ex)
            {
                return JSONHelper.FromString(false, ex.Message + Environment.NewLine + ex.StackTrace);
            }

            return JSONHelper.FromString(true, new DataTable());
        }

        #endregion
        #region 北方预存接口

        /// <summary>
        /// 查询预存费项
        /// </summary>
        /// <param name="Row"></param>
        /// CommunityId             小区编号
        /// CustID                  客户编号
        /// RoomID                  房间编码
        /// 返回
        ///     key         费项编号
        ///     value       费项名称
        /// <returns></returns>
        private string GetPrecCost_BF(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return new ApiResult(false, "小区编码不能为空").toJson();
            }
            string CommID = row["CommID"].ToString();
            if (!row.Table.Columns.Contains("CustID") || string.IsNullOrEmpty(row["CustID"].ToString()))
            {
                return new ApiResult(false, "用户编码不能为空").toJson();
            }
            string CustID = row["CustID"].ToString();
            if (!row.Table.Columns.Contains("RoomID") || string.IsNullOrEmpty(row["RoomID"].ToString()))
            {
                return new ApiResult(false, "房间编码不能为空").toJson();
            }
            string RoomID = row["RoomID"].ToString();
            #region 获取tw2bs链接字符串
            string NetType = "";
            if (row.Table.Columns.Contains("Net"))
            {
                NetType = AppGlobal.ChkStr(row["Net"].ToString());
            }
            PubConstant.tw2bsConnectionString = Connection.GetConnection(NetType);
            #endregion
            string connStr = "";
            #region 查询小区
            using (IDbConnection conn = new SqlConnection(PubConstant.tw2bsConnectionString))
            {
                dynamic info = conn.QueryFirstOrDefault<dynamic>("Proc_HSPR_Community_Filter", new { SQLEx = string.Format("AND CommID = '{0}' AND ISNULL(IsDelete,0) = 0", CommID) }, null, null, CommandType.StoredProcedure);
                if (null == info)
                {
                    return new ApiResult(false, "该小区不存在").toJson();
                }
                int CorpId = Convert.ToInt32(info.CorpID);
                if (0 == CorpId)
                {
                    return new ApiResult(false, "无该企业号信息").toJson();
                }
                connStr = Connection.GetCorpSQLContion(CorpId);
            }
            if (string.IsNullOrEmpty(connStr))
            {
                return new ApiResult(false, "获取连接字符串失败").toJson();
            }
            #endregion

            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();

            DataTable dt = null;
            using (IDbConnection conn = new SqlConnection(connStr))
            {
                dt = conn.ExecuteReader("SELECT CostID, CostName FROM view_HSPR_CostStanSetting_Filter WHERE CustID = @CustID AND RoomID = @RoomID AND ( SysCostSign = 'B0001' OR SysCostSign = 'B0002' ) GROUP BY CostID, CostName", new { CustID = CustID, RoomID = RoomID }, null, null, CommandType.Text).ToDataSet().Tables[0];
            }
            if (null == dt || dt.Rows.Count == 0)
            {
                return new ApiResult(true, list).toJson();
            }
            Dictionary<string, object> dictionary;
            foreach (DataRow item in dt.Rows)
            {
                dictionary = new Dictionary<string, object>();
                dictionary.Add("CostName", item["CostName"]);
                dictionary.Add("CostID", item["CostID"]);
                list.Add(dictionary);
            }
            return new ApiResult(true, list).toJson();
        }

        /// <summary>
        /// 计算预存费用CalcAdvancePrice
        /// </summary>
        /// <param name="row"></param>
        /// CustID                  用户编码
        /// CommID                  小区编码
        /// RoomID                  房间编码
        /// CostIDs                 费项编码
        /// FeesStateDate           开始日期
        /// FeesEndDate             结束日期
        /// <returns></returns>    
        private string CalcAdvancePrice_BF(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return new ApiResult(false, "小区编码不能为空").toJson();
            }
            string CommID = row["CommID"].ToString();

            if (!row.Table.Columns.Contains("CustID") || string.IsNullOrEmpty(row["CustID"].ToString()))
            {
                return new ApiResult(false, "用户编码不能为空").toJson();
            }
            string CustID = row["CustID"].ToString();
            if (!row.Table.Columns.Contains("RoomID") || string.IsNullOrEmpty(row["RoomID"].ToString()))
            {
                return new ApiResult(false, "房间编码不能为空").toJson();
            }
            string RoomID = row["RoomID"].ToString();
            if (!row.Table.Columns.Contains("CostID") || string.IsNullOrEmpty(row["CostID"].ToString()))
            {
                return new ApiResult(false, "费项编码不能为空").toJson();
            }
            string CostID = row["CostID"].ToString();

            if (!row.Table.Columns.Contains("FeesStateDate") || string.IsNullOrEmpty(row["FeesStateDate"].ToString()))
            {
                return new ApiResult(false, "开始日期不能为空").toJson();
            }
            DateTime FeesStateDate;
            if (!DateTime.TryParse(row["FeesStateDate"].ToString(), out FeesStateDate))
            {
                return new ApiResult(false, "开始日期格式有误").toJson();
            }

            if (!row.Table.Columns.Contains("FeesEndDate") || string.IsNullOrEmpty(row["FeesEndDate"].ToString()))
            {
                return new ApiResult(false, "结束日期不能为空").toJson();
            }
            DateTime FeesEndDate;
            if (!DateTime.TryParse(row["FeesEndDate"].ToString(), out FeesEndDate))
            {
                return new ApiResult(false, "结束日期格式有误").toJson();
            }
            if (FeesEndDate.CompareTo(FeesStateDate) < 0)
            {
                return new ApiResult(false, "结束日期不能小于开始日期").toJson();
            }
            FeesStateDate = new DateTime(FeesStateDate.Year, FeesStateDate.Month, 1, 0, 0, 0);
            FeesEndDate = new DateTime(FeesEndDate.Year, FeesEndDate.Month + 1, 1, 0, 0, 0).AddDays(-1);

            #region 获取tw2bs链接字符串
            string NetType = "";
            if (row.Table.Columns.Contains("Net"))
            {
                NetType = AppGlobal.ChkStr(row["Net"].ToString());
            }
            PubConstant.tw2bsConnectionString = Connection.GetConnection(NetType);
            #endregion
            string connStr = "";
            #region 查询小区
            using (IDbConnection conn = new SqlConnection(PubConstant.tw2bsConnectionString))
            {
                dynamic info = conn.QueryFirstOrDefault<dynamic>("Proc_HSPR_Community_Filter", new { SQLEx = string.Format("AND CommID = '{0}' AND ISNULL(IsDelete,0) = 0", CommID) }, null, null, CommandType.StoredProcedure);
                if (null == info)
                {
                    return new ApiResult(false, "该小区不存在").toJson();
                }
                int CorpId = Convert.ToInt32(info.CorpID);
                if (0 == CorpId)
                {
                    return new ApiResult(false, "无该企业号信息").toJson();
                }
                connStr = Connection.GetCorpSQLContion(CorpId);
            }
            if (string.IsNullOrEmpty(connStr))
            {
                return new ApiResult(false, "获取连接字符串失败").toJson();
            }
            #endregion
            using (IDbConnection conn = new SqlConnection(connStr))
            {
                string SQLEx = string.Format("AND CommID = {0} AND CustID = {1} AND RoomID = {2} AND CostID = {3}", CommID, CustID, RoomID, CostID);
                dynamic info = conn.QueryFirstOrDefault<dynamic>("Proc_HSPR_CostStanSetting_Filter", new { SQLEx = SQLEx }, null, null, CommandType.StoredProcedure);
                if (null == info)
                {
                    return new ApiResult(false, "未查询到该费项").toJson();
                }
                long iStanID = Convert.ToInt64(info.StanID);
                long iRoomID = Convert.ToInt64(info.RoomID);
                long iCustID = Convert.ToInt64(info.CustID);
                long iCostID = Convert.ToInt64(info.CostID);
                string strCostName = Convert.ToString(info.CostName);
                string strRoomSign = Convert.ToString(info.RoomSign);

                List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
                while (FeesStateDate < FeesEndDate)
                {
                    dynamic result = conn.QueryFirstOrDefault<dynamic>("Proc_HSPR_Fees_CalcAmount", new { CommID = CommID, CustID = CustID, RoomID = iRoomID, HandID = 0, CostID = iCostID, StanID = iStanID, FeesStateDate = FeesStateDate, FeesEndDate = FeesStateDate.AddMonths(1).AddDays(-1), Amount = 0, Amount2 = 1 }, null, null, CommandType.StoredProcedure);
                    if (null != result)
                    {
                        decimal amount = Convert.ToDecimal(result.DueAmount);
                        list.Add(new Dictionary<string, object> { { "RoomSign", strRoomSign }, { "StartDate", FeesStateDate.ToShortDateString() }, { "EndDate", FeesStateDate.AddMonths(1).AddDays(-1).ToShortDateString() }, { "CostName", strCostName }, { "Amount", amount } });
                    }
                    FeesStateDate = FeesStateDate.AddMonths(1);
                }
                return new ApiResult(true, list).toJson();
            }
        }

        private string ChargePrice_BF(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return new ApiResult(false, "小区编码不能为空").toJson();
            }
            string CommID = row["CommID"].ToString();

            if (!row.Table.Columns.Contains("Amount") || string.IsNullOrEmpty(row["Amount"].ToString()))
            {
                return new ApiResult(false, "金额不能为空").toJson();
            }
            decimal Amount = Convert.ToDecimal(row["Amount"].ToString());
            if (Amount <= 0)
            {
                return new ApiResult(false, "金额不能小于等于0").toJson();
            }
            if (!row.Table.Columns.Contains("CustID") || string.IsNullOrEmpty(row["CustID"].ToString()))
            {
                return new ApiResult(false, "用户编码不能为空").toJson();
            }
            long CustID = Convert.ToInt64(row["CustID"].ToString());
            if (!row.Table.Columns.Contains("RoomID") || string.IsNullOrEmpty(row["RoomID"].ToString()))
            {
                return new ApiResult(false, "房间编码不能为空").toJson();
            }
            long RoomID = Convert.ToInt64(row["RoomID"].ToString());
            if (!row.Table.Columns.Contains("CostID") || string.IsNullOrEmpty(row["CostID"].ToString()))
            {
                return new ApiResult(false, "费项编码不能为空").toJson();
            }
            string CostID = row["CostID"].ToString();
            #region 获取tw2bs链接字符串
            string NetType = "";
            if (row.Table.Columns.Contains("Net"))
            {
                NetType = AppGlobal.ChkStr(row["Net"].ToString());
            }
            PubConstant.tw2bsConnectionString = Connection.GetConnection(NetType);
            #endregion
            string connStr = "";
            #region 查询小区
            using (IDbConnection conn = new SqlConnection(PubConstant.tw2bsConnectionString))
            {
                dynamic info = conn.QueryFirstOrDefault<dynamic>("Proc_HSPR_Community_Filter", new { SQLEx = string.Format("AND CommID = '{0}' AND ISNULL(IsDelete,0) = 0", CommID) }, null, null, CommandType.StoredProcedure);
                if (null == info)
                {
                    return new ApiResult(false, "该小区不存在").toJson();
                }
                int CorpId = Convert.ToInt32(info.CorpID);
                if (0 == CorpId)
                {
                    return new ApiResult(false, "无该企业号信息").toJson();
                }
                connStr = Connection.GetCorpSQLContion(CorpId);
            }
            if (string.IsNullOrEmpty(connStr))
            {
                return new ApiResult(false, "获取连接字符串失败").toJson();
            }
            Global_Var.CorpSQLConnstr = connStr;
            #endregion
            //预存收款
            string strUserCode = "_Sys_";
            string Result = "";
            string ErrorMsg = "";
            string chargeMode = "自助缴费";
            long iReceID = 0;
            if (1 == ReceFeesPrec.ReceivePrecFees(AppGlobal.StrToInt(CommID), CustID, RoomID, CostID, chargeMode, Amount, strUserCode, ref Result, ref ErrorMsg, ref iReceID))
            {
                return new ApiResult(true, "缴费成功").toJson();
            }
            else
            {
                return new ApiResult(false, "缴费失败,请重试").toJson();
            }
        }


        /// <summary>
        /// 预缴费用
        /// </summary>
        /// 用户编码：CustID【必填】
        /// 小区编码：CommID[必填]
        /// 房屋编号：RoomID 【选填】
        /// 返回信息：
        ///         预缴金额：PrecAmount
        ///         预缴费项：CostNames
        /// 
        /// <param name="row"></param>
        /// <returns></returns>
        private string GetAdvanceList_BF(DataRow row)
        {
            if (!row.Table.Columns.Contains("CustID") || string.IsNullOrEmpty(row["CustID"].ToString()))
            {
                return JSONHelper.FromString(false, "用户编码不能为空");
            }
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编码不能为空");
            }

            string CustID = row["CustID"].ToString();
            string CommID = row["CommID"].ToString();
            string RoomID = "";
            if (row.Table.Columns.Contains("RoomID"))
            {
                RoomID = row["RoomID"].ToString();
            }
            #region 获取tw2bs链接字符串
            string NetType = "";
            if (row.Table.Columns.Contains("Net"))
            {
                NetType = AppGlobal.ChkStr(row["Net"].ToString());
            }
            PubConstant.tw2bsConnectionString = Connection.GetConnection(NetType);
            #endregion
            string connStr = "";
            #region 查询小区
            using (IDbConnection conn = new SqlConnection(PubConstant.tw2bsConnectionString))
            {
                dynamic info = conn.QueryFirstOrDefault<dynamic>("Proc_HSPR_Community_Filter", new { SQLEx = string.Format("AND CommID = '{0}' AND ISNULL(IsDelete,0) = 0", CommID) }, null, null, CommandType.StoredProcedure);
                if (null == info)
                {
                    return new ApiResult(false, "该小区不存在").toJson();
                }
                int CorpId = Convert.ToInt32(info.CorpID);
                if (0 == CorpId)
                {
                    return new ApiResult(false, "无该企业号信息").toJson();
                }
                connStr = Connection.GetCorpSQLContion(CorpId);
            }
            if (string.IsNullOrEmpty(connStr))
            {
                return new ApiResult(false, "获取连接字符串失败").toJson();
            }
            Global_Var.CorpSQLConnstr = connStr;
            #endregion

            // 查询缴费信息
            StringBuilder sb = new StringBuilder();
            //sb.Append("select PrecID,CommID,CustID,RoomID,CostID,PrecAmount,CostNames,RoomSign from     view_HSPR_PreCosts_Filter ");
            sb.Append(" SELECT PrecID,PrecAmount AS [value],CAST(CostNames as varchar(max)) AS [key]  FROM view_HSPR_PreCosts_Filter ");
            sb.AppendFormat(" WHERE CommID = {0} ", CommID);
            sb.AppendFormat(" AND CustID = {0} ", CustID);
            if (RoomID != "")
            {
                sb.AppendFormat("  AND RoomID = {0} ", RoomID);
            }
            sb.Append(" AND PrecAmount>0 ");
            sb.Append("  GROUP BY PrecID,PrecAmount,CAST(CostNames as varchar(max)) ");

            DataSet ds = new DbHelperSQLP(Global_Var.CorpSQLConnstr).Query(sb.ToString());
            return JSONHelper.FromString(ds.Tables[0]);
        }
        #endregion

        #region 公共

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="PageCount">总页数</param>
        /// <param name="Counts">总条数</param>
        /// <param name="StrCondition">执行语句</param>
        /// <param name="PageIndex">第几页</param>
        /// <param name="PageSize">每页多少条</param>
        /// <param name="SortField">排序字段</param>
        /// <param name="Sort">升序/降序</param>
        /// <param name="ID">主键</param>
        /// <returns></returns>
        internal DataSet GetList(out int PageCount, out int Counts, string StrCondition, int PageIndex, int PageSize, string SortField, int Sort, string ID)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@FldName", SqlDbType.VarChar, 255),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@FldSort", SqlDbType.VarChar, 1000),
                    new SqlParameter("@Sort", SqlDbType.Int),
                    new SqlParameter("@StrCondition", SqlDbType.VarChar, 8000),
                    new SqlParameter("@Id", SqlDbType.VarChar, 50),
                    new SqlParameter("@PageCount", SqlDbType.Int, 4,ParameterDirection.Output, false, 0, 0,string.Empty, DataRowVersion.Default, null),
                    new SqlParameter("@Counts", SqlDbType.Int, 4,ParameterDirection.Output, false, 0, 0,string.Empty, DataRowVersion.Default, null),
                    };
            parameters[0].Value = "*";
            parameters[1].Value = PageSize;
            parameters[2].Value = PageIndex;
            parameters[3].Value = SortField;
            parameters[4].Value = Sort;
            parameters[5].Value = StrCondition;
            parameters[6].Value = ID;
            DataSet Ds = new DbHelperSQLP(Global_Var.CorpSQLConnstr).RunProcedure("Proc_System_TurnPage", parameters, "RetDataSet");
            PageCount = AppGlobal.StrToInt(parameters[7].Value.ToString());
            Counts = AppGlobal.StrToInt(parameters[8].Value.ToString());
            return Ds;
        }

        /// <summary>
        /// 判断是否包含此字符
        /// </summary>
        /// <param name="str"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        private bool CheckStr(string str, string[] s)
        {
            bool bl = true;
            foreach (string item in s)
            {
                if (str == item)
                {
                    bl = false;
                }
            }
            return bl;
        }


        /// <summary>
        /// 动态构建本期账单
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        private static DataTable SetTable(DataSet ds)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Price"));//单价
            dt.Columns.Add(new DataColumn("Number"));//数量 
            dt.Columns.Add(new DataColumn("CostName"));//费项名称
            dt.Columns.Add(new DataColumn("DueAmount")); //小记
            dt.Columns.Add(new DataColumn("FeesChargeDate"));//费用改变时间
            dt.Columns.Add(new DataColumn("FeesDueDate"));//费用产生时间
            dt.Columns.Add(new DataColumn("isCharge"));//是否已收
            dt.Columns.Add(new DataColumn("IsPrec"));//是否预存
            foreach (DataRow item in ds.Tables[0].Rows)
            {
                if (!string.IsNullOrEmpty(item["StanMemo"].ToString()))
                {
                    string[] strSprit = item["StanMemo"].ToString().Split('|');
                    if (strSprit.Length > 0)//StartDegree:130.00|EndDegree:149.00:
                    {
                        DataRow dr = dt.NewRow();
                        foreach (string itemstr in strSprit)
                        {
                            string[] str = itemstr.Split(':');//EndDegree:149.00
                            if (str.Length > 0)
                            {
                                for (int i = 0; i < str.Length; i++)
                                {
                                    if (str[i].ToString() == "Price" || str[i].ToString() == "StanAmount" || str[i].ToString() == "Price")//获取各种费项单价
                                    {
                                        dr["Price"] = str[i + 1].ToString();
                                    }

                                    if (str[i].ToString() == "Dosage" || str[i].ToString() == "Num" || str[i].ToString() == "ShareDosage:")//获取各种费项数量
                                    {
                                        dr["Number"] = str[i + 1].ToString();
                                    }
                                    if (str[i].ToString() == "CarSign")
                                    {
                                        dr["Number"] = "1";
                                    }
                                }
                                if (dr["Price"].ToString() != "" && dr["Number"].ToString() != "")
                                {
                                    dr["CostName"] = item["CostName"].ToString();
                                    dr["DueAmount"] = item["DueAmount"].ToString();
                                    dr["FeesChargeDate"] = item["FeesChargeDate"].ToString();
                                    dr["FeesDueDate"] = item["FeesDueDate"].ToString();
                                    dr["isCharge"] = item["isCharge"].ToString();
                                    dr["IsPrec"] = item["IsPrec"].ToString();
                                    dt.Rows.Add(dr);
                                    dr = dt.NewRow();
                                }
                            }
                        }
                    }
                }
                else
                {
                    DataRow dr = dt.NewRow();
                    dr["CostName"] = item["CostName"].ToString();
                    dr["DueAmount"] = item["DueAmount"].ToString();
                    dr["Price"] = item["DueAmount"].ToString();
                    dr["Number"] = "1";
                    dr["FeesChargeDate"] = item["FeesChargeDate"].ToString();
                    dr["FeesDueDate"] = item["FeesDueDate"].ToString();
                    dr["isCharge"] = item["isCharge"].ToString();
                    dr["IsPrec"] = item["IsPrec"].ToString();
                    dt.Rows.Add(dr);
                    dr = dt.NewRow();
                }
            }

            return dt;
        }

        /// <summary>
        /// 根据日期分组
        /// </summary>
        /// <param name="ds">查询结果集</param>
        /// <returns>JSON</returns>
        public string GetDataSetForGroupJsonn(DataSet ds)
        {
            StringBuilder sbStr = new StringBuilder();
            if (ds != null && ds.Tables.Count > 0)
            {
                string str = "";
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        str += ds.Tables[0].Rows[i]["FeeDueYearMonth"];
                    }
                    if (i > 0 && CheckStr(ds.Tables[0].Rows[i]["FeeDueYearMonth"].ToString(), str.Split(',')))
                    {
                        str += "," + ds.Tables[0].Rows[i]["FeeDueYearMonth"];
                    }
                }

                sbStr.Append("{\"Result\":\"true\",");
                sbStr.Append("\"data\":[");
                if (!string.IsNullOrEmpty(str))
                {
                    int j = 0;
                    foreach (string item in str.Split(','))
                    {
                        if (j == 0)
                        {
                            sbStr.Append("{\"Title\":\"" + item + "\",\"Content\":[");
                            int i = 0;
                            foreach (var item_row in ds.Tables[0].Select(" FeeDueYearMonth='" + item + "'"))
                            {
                                if (i == 0)
                                {
                                    sbStr.Append(JSONHelper.FromDataRow(item_row));
                                }
                                else
                                {
                                    sbStr.Append("," + JSONHelper.FromDataRow(item_row));
                                }
                                i++;
                            }
                            sbStr.Append("]}");
                        }
                        else
                        {
                            sbStr.Append(",{\"Title\":\"" + item + "\",\"Content\":[");
                            int i = 0;
                            foreach (var item_row in ds.Tables[0].Select(" FeeDueYearMonth='" + item + "'"))
                            {
                                if (i == 0)
                                {
                                    sbStr.Append(JSONHelper.FromDataRow(item_row));
                                }
                                else
                                {
                                    sbStr.Append("," + JSONHelper.FromDataRow(item_row));
                                }
                                i++;
                            }
                            sbStr.Append("]}");
                        }
                        j++;
                    }
                }
                sbStr.Append("]}");
            }
            return sbStr.ToString();
        }


        #endregion

    }
}
