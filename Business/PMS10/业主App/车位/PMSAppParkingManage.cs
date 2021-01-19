using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Common;
using Dapper;
using MobileSoft.Common;
using MobileSoft.DBUtility;

namespace Business
{
    public class PMSAppParkingManage : PubInfo
    {
        public PMSAppParkingManage()
        {
            base.Token = "20200616PMSAppParkingManage";
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
                    case "GetMyParking":
                        Trans.Result = GetMyParking(Row);
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

        private string GetMyParking(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].AsString()))
            {
                return new ApiResult(false, "缺少参数CommunityId").toJson();
            }
            if (!row.Table.Columns.Contains("UserId") || string.IsNullOrEmpty(row["UserId"].AsString()))
            {
                return new ApiResult(false, "缺少参数UserId").toJson();
            }

            var communityId = row["CommunityId"].AsString();
            var userId = row["UserId"].AsString();

            var community = GetCommunity(communityId);
            if (community == null)
            {
                return JSONHelper.FromString(false, "未查询到小区信息");
            }

            var usermobile = "";
            using (var conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                var sql = @"SELECT Mobile FROM Tb_User WHERE Id=@UserId;";

                usermobile = conn.Query<string>(sql, new { UserId = userId }).FirstOrDefault();
            }

            using (var conn = new SqlConnection(Global_Fun.BurstConnectionString(AppGlobal.StrToInt(community.CommID),Global_Fun.BURST_TYPE_CHARGE)))
            {
                var sql = $@"SELECT b.HandID,b.CommID,a.CustID,a.ParkName,a.CustName,a.ParkStartDate,a.ParkEndDate,'' AS FeesEndDate 
                            FROM view_HSPR_Parking_Filter a
                                LEFT JOIN { community.DBName }.dbo.Tb_HSPR_ParkingHand b ON a.HandID=b.HandID
                            WHERE isnull(a.IsDelete,0)=0
                            AND b.CustID IN
                            (
                                SELECT CustID FROM { community.DBName }.dbo.Tb_HSPR_Customer
                                WHERE isnull(IsDelete,0)=0
                                AND (isnull(MobilePhone,'') LIKE @Mobile OR isnull(LinkmanTel,'') LIKE @Mobile)
                            ) ORDER BY ParkEndDate";

                var data = conn.Query(sql, new { Mobile = $"%{usermobile}%" });

                sql = $@"SELECT a.CostID,b.CostName,convert(varchar(20),max(isnull(a.FeesEndDate,a.FeesDueDate))) AS FeesEndDate
                        FROM Tb_HSPR_Fees AS a WITH(NOLOCK)
                            INNER JOIN { community.DBName }.dbo.Tb_HSPR_CostItem AS b WITH(NOLOCK)
                            ON a.CommID=b.CommID AND a.CostID=b.CostID AND isnull(b.IsDelete,0)=0 AND (b.CostType=1 OR b.CostType=2)
                        WHERE a.CommID=@CommID AND a.CustID=@CustID AND a.HandID=@HandID
                        AND (isnull(a.IsCharge,0)=1 OR isnull(a.IsPrec,0)=1)
                        GROUP BY a.CostID,b.CostName";

                foreach (var item in data)
                {
                    item.FeesEndDate = conn.QueryFirstOrDefault<string>(sql, new
                    {
                        CommID = community.CommID,
                        CustID = item.CustID,
                        HandID = item.HandID
                    });
                }

                return new ApiResult(true, data).toJson();
            }
        }
    }
}
