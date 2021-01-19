using Aop.Api.Domain;
using App.Model;
using Business.PMS10.业主App.缴费.Enum;
using Business.PMS10.业主App.缴费.Model;
using Common;
using Dapper;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceModel.Channels;
using static Dapper.SqlMapper;


namespace Business
{
    public class PMSFeesManageV2 : PubInfo
    {
        public PMSFeesManageV2()
        {
            base.Token = "20200619PMSFeesManageV2";
        }


        public override void Operate(ref Transfer Trans)
        {
            DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
            DataRow Row = dAttributeTable.Rows[0];

            //防止未捕获异常出现
            try
            {
                switch (Trans.Command)
                {
                    case "GetArrearsFees":              // 获取欠费列表
                        Trans.Result = GetArrearsFees(Row);
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
        /// 获取绑定费用设置
        /// </summary>
        private string GetPaymentBingdingControl(int commId)
        {
            var community = GetCommunity(commId.ToString());
            if (community == null)
                return PMSAppFeesSelectionMode.Month;

            using (var conn = new SqlConnection(GetConnectionStr(community)))
            {
                var sql = @"SELECT * FROM Tb_HSPR_PaymentBindingDateModelSet WHERE CommID=@CommID";

                var control = conn.Query<Tb_HSPR_PaymentBindingDateModelSet>(sql, new { CommID = commId }).FirstOrDefault();
                if (control != null)
                {
                    if (control.Checkbox_1 == 1)    // 全缴
                        return PMSAppFeesSelectionMode.All;

                    if (control.Checkbox_2 == 1)    // 单项费用按单月
                        return PMSAppFeesSelectionMode.Month;

                    if (control.Checkbox_4 == 1)    // 绑定费项按单月
                        return PMSAppFeesSelectionMode.Month;

                    if (control.Checkbox_6 == 1)    // 绑定费项按季度
                        return PMSAppFeesSelectionMode.Quarter;

                    if (control.Checkbox_8 == 1)    // 绑定费项按半年
                        return PMSAppFeesSelectionMode.HalfYear;

                    if (control.Checkbox_10 == 1)   // 绑定费项按整年
                        return PMSAppFeesSelectionMode.Year;

                    if (control.Checkbox_12 == 1)   // 绑定费项按n月
                        return PMSAppFeesSelectionMode.MultiMonth;
                }

                return PMSAppFeesSelectionMode.Month;
            }
        }

        private string GetArrearsFees(DataRow row)
        {
            if (!row.Table.Columns.Contains("UserId") || string.IsNullOrEmpty(row["UserId"].AsString()))
            {
                return new ApiResult(false, "用户信息错误").toJson();
            }
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].AsString()))
            {
                return new ApiResult(false, "请选择房屋所在项目").toJson();
            }
            if (!row.Table.Columns.Contains("CustID") || string.IsNullOrEmpty(row["CustID"].AsString()))
            {
                return new ApiResult(false, "未指定欠费主体").toJson();
            }

            var userId = row["UserId"].AsString();
            var communityId = row["CommunityId"].AsString();
            var custId = AppGlobal.StrToLong(row["CustID"].AsString());
            var roomId = 0L;

            if (row.Table.Columns.Contains("RoomID") && !string.IsNullOrEmpty(row["RoomID"].AsString()))
            {
                roomId = AppGlobal.StrToLong(row["RoomID"].AsString());
            }

            var community = GetCommunity(communityId);
            if (community == null)
                return JSONHelper.FromString(false, "未查找到小区信息");

            var commId = AppGlobal.StrToInt(community.CommID);
            PubConstant.tw2bsConnectionString = Global_Fun.Tw2bsConnectionString(Global_Fun.GetNetType(community.DBServer));
            PubConstant.hmWyglConnectionString = GetConnectionStr(community);
            var chargeConnectionString = Global_Fun.BurstConnectionString(commId, Global_Fun.BURST_TYPE_CHARGE);

            var shieldCost = "";

            // 查询需要屏蔽的费用
            using (var appConn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                var sql = @"SELECT isnull(object_id(N'Tb_Control_AppCostItem',N'U'),0)";
                if (appConn.Query<long>(sql).FirstOrDefault() != 0)
                {
                    sql = @"SELECT CorpCostID FROM Tb_Control_AppCostItem WHERE CorpID=@CorpID AND CommunityID=@Id AND AllowShow=0;
                            SELECT CorpCostID FROM Tb_Control_AppCostItem WHERE CorpID=@CorpID AND isnull(CommunityID,'')='' AND AllowShow=0;";

                    var reader = appConn.QueryMultiple(sql, new { CorpID = community.CorpID, Id = community.Id });
                    var data1 = reader.Read<string>().FirstOrDefault();
                    var data2 = reader.Read<string>().FirstOrDefault();

                    var data = data1 ?? data2;

                    if (!string.IsNullOrEmpty(data))
                    {
                        using (var erpConn = new SqlConnection(PubConstant.hmWyglConnectionString))
                        {
                            sql = $@"SELECT CostCode FROM Tb_HSPR_CorpCostItem 
                                    WHERE CorpCostID IN
                                    (
                                        SELECT Value FROM SplitString('{data}',',',1)
                                    )";

                            var costCodes = erpConn.Query<string>(sql).Select(obj => $"'{obj}'");

                            shieldCost = $@"AND CostCode NOT IN ({ string.Join(",", costCodes.ToArray()) })";
                        }
                    }
                }
            }

            using (var chargeConn = new SqlConnection(chargeConnectionString))
            {
                var list = new List<PMSAppFeesCostSimpleModelV2>();

                var sql = $@"/* 欠费未缴清 */
                            SELECT FeesID,x.CostID,CostName,isnull(SysCostSign,'Unknown') AS SysCostSign,
                                isnull(DueAmount,0.00) AS DueAmount,
                                isnull(DebtsAmount,0.00) AS DebtsAmount,
                                CASE WHEN isnull(x.DebtsLateAmount, 0)>0
                                    THEN dbo.funGetLateFeeDebts(x.CommID,x.FeesID,isnull(x.DebtsLateAmount, 0))
                                ELSE 0.0 END AS LateFeeAmount,
                                convert(varchar(10),isnull(FeesDueDate,getdate()),120) AS FeesDueDate
                            FROM Tb_HSPR_Fees x
                            LEFT JOIN { community.DBName }.dbo.Tb_HSPR_CostItem y ON x.CostID=y.CostID 
                            WHERE x.CustID=@CustID AND x.RoomID=@RoomID 
                            AND isnull(x.IsCharge,0)=0 AND isnull(x.IsBank,0)=0 AND isnull(x.IsPrec,0)=0 AND isnull(IsFreeze,0)=0 
                            { shieldCost } 
                            ORDER BY SysCostSign ASC,FeesDueDate DESC;";

                var data = chargeConn.Query(sql, new { CommID = community.CommID, RoomID = roomId, CustID = custId }).ToList();

                for (int i = 0; i < data.Count; i++)
                {
                    var feesInfo = data[i];

                    var tmp = list.Find(obj => obj.CostID == feesInfo.CostID);
                    if (tmp == null)
                    {
                        tmp = new PMSAppFeesCostSimpleModelV2()
                        {
                            CostID = feesInfo.CostID,
                            CostName = feesInfo.CostName,
                            SysCostSign = feesInfo.SysCostSign == "Unknown" ? null : feesInfo.SysCostSign,
                            Fees = new List<PMSAppFeesSimpleModelV2>()
                        };
                        list.Add(tmp);
                    }

                    var model = new PMSAppFeesSimpleModelV2()
                    {
                        FeesID = feesInfo.FeesID,
                        DueAmount = feesInfo.DueAmount,
                        DebtsAmount = feesInfo.DebtsAmount,
                        LateFeeAmount = feesInfo.LateFeeAmount,
                        FeesDueDate = feesInfo.FeesDueDate
                    };

                    tmp.TotalDueAmount += model.DueAmount;
                    tmp.TotalDebtsAmount += model.DebtsAmount;
                    tmp.TotalLateFeeAmount += model.LateFeeAmount;

                    if (feesInfo.SysCostSign == "B0001")
                    {
                        tmp.Expanded = 1;
                    }

                    tmp.Fees.Add(model);
                }

                using (var appConn = new SqlConnection(PubConstant.UnifiedContionString))
                {
                    // 读取积分信息
                    sql = @"SELECT * FROM Tb_Control_AppPoint WHERE CommunityID=@CommunityId AND isnull(CommunityID,'')<>'' AND IsEnable=1
                            UNION ALL 
                            SELECT * FROM Tb_Control_AppPoint WHERE CorpID=@CorpId AND isnull(CommunityID,'')='' AND IsEnable=1";

                    var controlInfo = appConn.Query<Tb_Control_AppPoint>(sql, new { CommunityId = communityId, CorpId = community.CorpID }).FirstOrDefault();
                    if (controlInfo == null || controlInfo.IsEnable == false)
                    {
                        controlInfo = Tb_Control_AppPoint.DefaultControl;
                    }

                    // 用户积分余额
                    sql = @"SELECT PointBalance FROM Tb_App_UserPoint WHERE UserID=@UserID";
                    var balance = appConn.Query<int>(sql, new { UserID = userId }).FirstOrDefault();

                    var costSign = new List<string>();
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
                        ArrearsFees = list,
                        Points = new
                        {
                            UserPointBalance = balance,
                            AllowDeductionPropertyFees = controlInfo.AllowDeductionPropertyFees,
                            AllowDeductionParkingFees = controlInfo.AllowDeductionParkingFees,
                            AllowDeductionOtherPropertyFees = controlInfo.AllowDeductionOtherPropertyFees,
                            PointExchangeRatio = controlInfo.PointExchangeRatio,
                            SysCostSign = costSign
                        }
                    }).toJson();
                }
            }
        }


        private void Test(List<PMSAppFeesCostSimpleModelV2> list, string mode)
        {
            foreach (var item in list)
            {
                item.Mode = mode;

                if (mode == PMSAppFeesSelectionMode.Quarter)
                {

                }
            }
        }

        /// <summary>
        /// 季度、半年、年
        /// </summary>
        private void SelectQuarter(List<PMSAppFeesCostSimpleModelV2> list)
        {
            var i = 3;
            var group = list.Select(obj => obj.Fees.Count).Sum();       // 分组编号
            var nextGroup = false;                                      // 是否到下一分组
            var prevFeesYear = 0;

            foreach (var costInfo in list)
            {
                // 待同步Group的绑定费项的费用信息
                var waitSynchCostInfo = new List<PMSAppFeesCostSimpleModelV2>();

                // 存在绑定费项
                if (costInfo.BindingCosts != null && costInfo.BindingCosts.Length > 0)
                {
                    foreach (var bindingCostID in costInfo.BindingCosts)
                    {
                        var bindingCostInfo = list.Find(obj => obj.CostID == bindingCostID);
                        if (bindingCostInfo != null)
                        {
                            waitSynchCostInfo.Add(bindingCostInfo);
                        }
                    }
                }

                // 费用已倒序排列
                foreach (var feesInfo in costInfo.Fees)
                {
                    if (nextGroup)
                    {
                        nextGroup = false;
                        group--;
                    }

                    var date = Convert.ToDateTime(feesInfo.FeesDueDate);
                    if (date.Year != prevFeesYear || date.Month % i == 0)
                    {
                        nextGroup = true;
                    }

                    if (feesInfo.Group != 0)
                    {
                        feesInfo.Group = group;
                    }
                }
            }
        }
    }
}
