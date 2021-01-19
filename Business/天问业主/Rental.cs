using Dapper;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Business
{
    /// <summary>
    /// 租赁
    /// </summary>
    public class Rental : PubInfo
    {
        public Rental()
        {
            base.Token = "20160803Rental";
        }
        public override void Operate(ref Common.Transfer Trans)
        {
            Trans.Result = "false:";

            DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
            DataRow Row = dAttributeTable.Rows[0];

            switch (Trans.Command)
            {
                case "GetRentalTop":
                    Trans.Result = GetRentalTop(Row);
                    break;
                case "RentalList":
                    Trans.Result = RentalList(Row);
                    break;
                case "RentalList_v2":
                    Trans.Result = RentalList_v2(Row);
                    break;
                case "RentalDetail_v2":
                    Trans.Result = RentalDetail_v2(Row);
                    break;
                case "RentalNew":
                    Trans.Result = RentalNew(Row);
                    break;
                case "RentalHistoryList":
                    Trans.Result = RentalHistoryList(Row);
                    break;
                default:
                    break;
            }
        }

        private string GetRentalTop(DataRow Row)
        {
            string backstr = "";
            DataTable dt = null;
            try
            {
                string ConnStr = ConnectionDb.GetUnifiedConnectionString();

                StringBuilder sb = new StringBuilder();

                if (Row.Table.Columns.Contains("AppleBundleID") && Row["AppleBundleID"].ToString() != "")//苹果包名
                {
                    sb.AppendFormat(" AND CommunityId in(select Id from Tb_Community where AppleBundleID = '{0}') ", Row["AppleBundleID"]);
                }
                if (Row.Table.Columns.Contains("AndroidPackageName") && Row["AndroidPackageName"].ToString() != "")//安桌包名
                {
                    sb.AppendFormat(" AND CommunityId in(select Id from Tb_Community where AndroidPackageName = '{0}') ", Row["AndroidPackageName"]);
                }

                sb.AppendFormat(" AND ProcessState='发布' AND BussType='{0}'", Row["BussType"].ToString());

                if (Row["BussType"].ToString() == "租售")
                {
                    sb.AppendFormat(@" AND (convert(NVARCHAR(20),Rent) <> '面议' OR (convert(DECIMAL(18,2), replace(rent, '面议', '0'))>=0 
                                        AND convert(DECIMAL(18,2), replace(rent, '面议', '0'))<=99999999) OR Rent IS NULL)");
                }
                if (Row["BussType"].ToString() == "转让")
                {
                    sb.AppendFormat(" AND ((Amount>=0 AND Amount<=99999999) OR Amount IS NULL)");
                }

                int page = 1;
                int row = 5;
                dt = BaseGetData.GetList(ConnStr, sb.ToString(), page, row, "RegisterDate", 1, "View_Tb_Rental", "Id").Tables[0];
            }
            catch (Exception ex)
            {

                backstr = ex.Message;
            }
            if (backstr == "")
            {
                return JSONHelper.FromString(dt);
            }
            else
            {
                return JSONHelper.FromString(false, backstr);
            }
        }

        //房屋租赁列表
        public string RentalList(DataRow Row)
        {
            string backstr = "";
            DataTable dt = null;
            try
            {

                string ConnStr = ConnectionDb.GetUnifiedConnectionString();

                StringBuilder sb = new StringBuilder();

                if (Row.Table.Columns.Contains("CommunityId") && Row["CommunityId"].ToString() != "")
                {
                    sb.AppendFormat(" AND CommunityId='{0}'", Row["CommunityId"].ToString());
                }
                else
                {
                    if (string.IsNullOrEmpty(Row["City"].ToString()) == false)
                    {
                        sb.AppendFormat(" AND (City LIKE '%{0}%' OR Province LIKE '%{0}%')", Row["City"].ToString().Replace("市", ""));
                    }

                    if (Row.Table.Columns.Contains("AppleBundleID") && Row["AppleBundleID"].ToString() != "")//苹果包名
                    {
                        sb.AppendFormat(" AND CommunityId in(select Id from Tb_Community where AppleBundleID = '{0}') ", Row["AppleBundleID"]);
                    }
                    if (Row.Table.Columns.Contains("AndroidPackageName") && Row["AndroidPackageName"].ToString() != "")//安桌包名
                    {
                        sb.AppendFormat(" AND CommunityId in(select Id from Tb_Community where AndroidPackageName = '{0}') ", Row["AndroidPackageName"]);
                    }
                }

                sb.AppendFormat(" AND ProcessState='发布' AND BussType='{0}'", Row["BussType"].ToString());

                if (string.IsNullOrEmpty(Row["HouseType"].ToString()) == false)
                {
                    sb.AppendFormat(" AND HouseType LIKE '%{0}%'", Row["HouseType"].ToString());
                }

                if (Row["BussType"].ToString() == "租售")
                {
                    sb.AppendFormat(@" AND (convert(NVARCHAR(20),Rent) <> '面议' AND (convert(DECIMAL(18,2), replace(rent, '面议', '0'))>={0} 
                                        AND convert(DECIMAL(18,2), replace(rent, '面议', '0'))<={1}) OR Rent IS NULL)",
                                    Row["StartAmount"].ToString(), Row["EndAmount"].ToString());
                }
                if (Row["BussType"].ToString() == "转让")
                {
                    sb.AppendFormat(" AND ((Amount>={0} AND Amount<={1}) OR Amount IS NULL)", Row["StartAmount"].ToString(), Row["EndAmount"].ToString());
                }

                int page = AppGlobal.StrToInt(Row["Page"].ToString());
                int row = AppGlobal.StrToInt(Row["PageSize"].ToString());
                dt = BaseGetData.GetList(ConnStr, sb.ToString(), page, row, "RegisterDate", 1, "View_Tb_Rental", "Id").Tables[0];
            }
            catch (Exception ex)
            {

                backstr = ex.Message;
            }
            if (backstr == "")
            {
                return JSONHelper.FromString(dt);
            }
            else
            {
                return JSONHelper.FromString(false, backstr);
            }

        }

        //房屋租赁列表
        public string RentalList_v2(DataRow row)
        {
            try
            {
                var communityId = default(string);
                var city = default(string);
                var houseType = default(string);
                var bussType = "租售";

                var pageIndex = 1;
                var pageSize = 20;

                var condition = " AND ProcessState='发布' ";

                // 筛选地区
                if (row.Table.Columns.Contains("City") && row["City"].ToString() != "")
                {
                    city = row["City"].ToString().Replace("市", "");
                    condition += $"AND (City LIKE '%{ city }%' OR Province LIKE '%{ city }%')";
                }

                // 筛选小区
                if (row.Table.Columns.Contains("CommunityId") && row["CommunityId"].ToString() != "")
                {
                    communityId = row["CommunityId"].ToString();
                    condition += $" AND CommunityId='{ communityId }' ";
                }

                // 筛选户型
                if (row.Table.Columns.Contains("HouseType") && row["HouseType"].ToString() != "")
                {
                    houseType = row["HouseType"].ToString();
                    condition += $" AND HouseType LIKE '%{ communityId }%' ";
                }

                // 筛选租售
                if (row.Table.Columns.Contains("BussType") && row["BussType"].ToString() != "")
                {
                    bussType = row["BussType"].ToString();
                    condition += $" AND BussType='{ bussType }'";
                }

                // 筛选价格
                var minAmount = AppGlobal.StrToLong(row["StartAmount"]?.ToString());
                var maxAmount = AppGlobal.StrToLong(row["EndAmount"]?.ToString());
                maxAmount = maxAmount == 0 ? 9999999999 : maxAmount;
                if (bussType == "租售")
                {
                    condition += $@" AND isnull(Rent, 0)>={minAmount} AND isnull(Rent, 0)<={maxAmount}";
                }
                else
                {
                    condition += $@" AND isnull(Amount, 0)>={minAmount} AND isnull(Amount, 0)<={maxAmount}";
                }

                // App隔离
                if (row.Table.Columns.Contains("AppleBundleID") && row["AppleBundleID"].ToString() != "")//苹果包名
                {
                    condition += $" AND CommunityId in(select Id from Tb_Community where AppleBundleID = '{row["AppleBundleID"]}') ";
                }
                if (row.Table.Columns.Contains("AndroidPackageName") && row["AndroidPackageName"].ToString() != "")//安桌包名
                {
                    condition += $" AND CommunityId in(select Id from Tb_Community where AndroidPackageName = '{row["AndroidPackageName"]}') ";
                }

                using (var conn = new SqlConnection(PubConstant.UnifiedContionString))
                {
                    var sql = $@"SELECT Id,CommName,isnull(BussType,'租售') AS BussType,
                                    CASE WHEN Rent IS NULL THEN '面议'
                                        WHEN convert(nvarchar(20),Rent)='面议' THEN '面议'
                                        WHEN convert(decimal(12,2),Rent)=0 THEN '面议'
                                        ELSE convert(nvarchar(20),convert(decimal(12,0),isnull(Rent,0))) END AS Rent,
                                    CASE WHEN Amount IS NULL THEN '面议'
                                        WHEN convert(nvarchar(20),Amount)='面议' THEN '面议'
                                        WHEN convert(decimal(12,2),Amount)=0 THEN '面议'
                                        ELSE convert(nvarchar(20),CONVERT(decimal(12,1),isnull(Amount,0.0)/10000.0))+'万' END AS Amount,
                                    substring(img,0,charindex(',',isnull(Img,''))) AS Img,isnull(Title,CommName) AS Title,
                                    isnull(HouseType,'住宅') AS HouseType,isnull(BuildingType,'住宅') AS BuildingType,
                                    isnull(ProcessDate,RegisterDate) PubDate,RegisterDate,Orientation,BuildArea 
                                FROM View_Tb_Rental WHERE 1=1 {condition}";

                    var resultSet = GetListDapper(out int pageCount, out int count, sql, pageIndex, pageSize, "RegisterDate", 1, "Id", conn);
                    var json = new ApiResult(true, resultSet).toJson();
                    return json.Insert(json.Length - 1, ",\"PageCount\":" + pageCount);
                }
            }
            catch (Exception ex)
            {
                return JSONHelper.FromString(false, ex.Message);
            }
        }

        // 房屋租赁详情
        public string RentalDetail_v2(DataRow row)
        {
            // 筛选地区
            if (!row.Table.Columns.Contains("Id") || row["Id"].ToString() == "")
            {
                return JSONHelper.FromString(false, "租售记录id不能为空");
            }

            var id = row["Id"].ToString();
            var sql = @"SELECT a.Id,replace(isnull(a.Province,''),'省','') AS Province, replace(isnull(a.City,''),'市','') AS City,
                            CASE WHEN a.Rent IS NULL THEN '面议'
                                WHEN convert(nvarchar(20),a.Rent)='面议' THEN '面议'
                                WHEN convert(decimal(12,2),a.Rent)=0 THEN '面议'
                                ELSE convert(nvarchar(20),convert(decimal(12,0),isnull(Rent,0))) END AS Rent,
                            CASE WHEN a.Amount IS NULL THEN '面议'
                                WHEN convert(nvarchar(20),a.Amount)='面议' THEN '面议'
                                WHEN convert(decimal(12,2),a.Amount)=0 THEN '面议'
                                ELSE convert(nvarchar(20),CONVERT(decimal(12,1),isnull(Amount,0.0)/10000.0))+'万' END AS Amount,
                           isnull(a.Img,'') AS Img,isnull(a.Title,b.CommName) AS Title,isnull(a.HouseType,'住宅') AS HouseType,
                           isnull(a.Orientation,'无') AS Orientation,isnull(a.Renovation,'无') AS Renovation,
                           isnull(a.BuildingType,'住宅') AS BuildingType,a.BuildingAge,isnull(a.ProcessDate,a.RegisterDate) PubDate,
                           isnull(a.SalesMobile,a.Mobile) AS Mobile,isnull(a.Address,b.CommName) AS Address,
                           isnull(BussType,'租售') AS BussType,a.Description,b.CommName,a.BuildArea,a.Floor 
                        FROM Tb_Rental a
                        LEFT JOIN Tb_Community b ON a.CommunityId=b.Id
                        WHERE a.Id=@Id;";

            using (var conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                var result = conn.Query(sql, new { Id = id }).FirstOrDefault();
                return new ApiResult(true, result).toJson();
            }
        }

        //新增房屋出租或房屋转让
        public string RentalNew(DataRow Row)
        {
            string backstr = "";
            try
            {
                decimal Amount = 0.0m, Rent = 0.0m;
                if (Row.Table.Columns.Contains("Price") && !string.IsNullOrEmpty(Row["Price"].ToString()))
                {
                    Amount = AppGlobal.StrToDec(Row["Price"].ToString());
                }
                if (Row["BussType"].ToString() == "租售")
                {
                    Rent = Amount;
                    Amount = 0.0m;
                }


                string ConnStr = ConnectionDb.GetUnifiedConnectionString();
                IDbConnection Conn = new SqlConnection(ConnStr);
                string Query = @"INSERT INTO Tb_Rental
                                (Id,CommunityId,BussType,LinkMan,Province,City,Area,Address,Mobile,Img,UserId,ProcessState,Amount,Rent,RegisterDate,RegisterMan) 
                                VALUES
                                (@Id,@CommunityId,@BussType,@LinkMan,@Province,@City,@Area,@Address,@Mobile,@Img,@UserId,'受理',@Amount,@Rent,getdate(),@LinkMan)";
                Conn.Execute(Query, new
                {
                    Id = Guid.NewGuid().ToString(),
                    CommunityId = Row["CommunityId"].ToString(),
                    BussType = Row["BussType"].ToString(),
                    LinkMan = Row["LinkMan"].ToString(),
                    Province = Row["Province"].ToString(),
                    City = Row["City"].ToString(),
                    Area = Row["Area"].ToString(),
                    Address = Row["Address"].ToString(),
                    Mobile = Row["Mobile"].ToString(),
                    Img = Row["Img"].ToString(),
                    UserId = Row["UserId"].ToString(),
                    Amount = Amount,
                    Rent = Rent
                });
            }
            catch (Exception ex)
            {

                backstr = ex.Message;
            }
            if (backstr == "")
            {
                return JSONHelper.FromString(true, "success");
            }
            else
            {
                return JSONHelper.FromString(false, backstr);
            }

        }

        /// <summary>
        /// 查询用户租赁信息
        /// </summary>
        /// <param name="Row"></param>
        /// <returns></returns>
        public string RentalHistoryList(DataRow Row)
        {
            string backstr = "";
            DataTable dt = null;
            try
            {
                string ConnStr = ConnectionDb.GetUnifiedConnectionString();
                string UserId = Row["UserId"].ToString();
                IDbConnection com = new SqlConnection(ConnStr);
                //Tb_User User = com.Query<Tb_User>("select * from Tb_User  where Id=@Id", new { Id = UserId }).ToList<Tb_User>().SingleOrDefault();
                //if (User==null)
                //{
                //    return JSONHelper.FromString(false, "该用户不存在");
                //}

                int page = AppGlobal.StrToInt(Row["Page"].ToString());
                int row = AppGlobal.StrToInt(Row["PageSize"].ToString());
                dt = BaseGetData.GetList(ConnStr, " and UserId='" + UserId + "'", page, row, "RegisterDate", 0, "View_Tb_Rental", "Id").Tables[0];
            }
            catch (Exception ex)
            {

                backstr = ex.Message;
            }
            if (backstr == "")
            {
                return JSONHelper.FromString(dt);
            }
            else
            {
                return JSONHelper.FromString(false, backstr);
            }


        }

    }
}