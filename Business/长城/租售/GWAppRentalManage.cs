using Common;
using Common.Extenions;
using Dapper;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using MobileSoft.Model.Unified;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using static Dapper.SqlMapper;

namespace Business
{
    public partial class GWAppRentalManage : PubInfo
    {
        public GWAppRentalManage()
        {
            base.Token = "GWAppRentalManage20200326";
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
                    case "GetBuildingType":
                        Trans.Result = GetBuildingType();
                        break;
                    case "GetTagSetting":
                        Trans.Result = GetTagSetting(Row);
                        break;
                    case "AddRentInfo":
                        Trans.Result = AddRentInfo(Row);
                        break;
                    case "AddSaleInfo":
                        Trans.Result = AddSaleInfo(Row);
                        break;
                    case "GetQualityRooms":
                        Trans.Result = GetQualityRooms(Row);
                        break;
                    case "GetRecommendRooms":
                        Trans.Result = GetRecommendRooms(Row);
                        break;
                    case "GetPublishHistory":
                        Trans.Result = GetPublishHistory(Row);
                        break;
                    case "GetRentalDetail":
                        Trans.Result = GetRentalDetail(Row);
                        break;
                    // 搜索相关
                    case "GetSearchAreas":
                        Trans.Result = GetSearchAreas();
                        break;
                    case "SearchRoom":
                        Trans.Result = SearchRoom(Row);
                        break;
                    //更新相关
                    case "UpdateRentInfo":
                        Trans.Result = UpdateRentInfo(Row);
                        break;
                    case "UpdateSaleInfo":
                        Trans.Result = UpdateSaleInfo(Row);
                        break;
                    case "GetRotation":
                        Trans.Result = GetRotation(Row);
                        break;
                    case "DeleteRental":
                        Trans.Result = DeleteRental(Row);
                        break;
                    case "GetPublishBuyAndRentHistory":
                        Trans.Result = GetPublishBuyAndRentHistory(Row);
                        break;
                    case "AddBuyAndRentalInfo":
                        Trans.Result = AddBuyAndRentalInfo(Row);
                        break;
                    case "UpdateBuyAndRentalInfo":
                        Trans.Result = UpdateBuyAndRentalInfo(Row);
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
        /// 获取建筑类型
        /// </summary>
        private string GetBuildingType()
        {
            using (var conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                var sql = @"SELET Name FROM Tb_Rental_BuildingType WHERE IsDelete=0";

                var data = conn.Query(sql);

                return new ApiResult(true, data).toJson();
            }
        }

        private string GetTagSetting(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].AsString()))
            {
                return new ApiResult(false, "缺少参数CommunityId").toJson();
            }
            if (!row.Table.Columns.Contains("BussType") || string.IsNullOrEmpty(row["BussType"].AsString()))
            {
                return new ApiResult(false, "缺少参数BussType").toJson();
            }

            var bussType = row["BussType"].ToString();
            var communityId = row["CommunityId"].AsString();

            var community = GetCommunity(communityId);
            if (community == null)
            {
                return JSONHelper.FromString(false, "未查询到小区信息");
            }

            using (var conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                var sql = $@"SELECT IID,Name FROM Tb_Rental_TagCategory WHERE CorpID=@CorpID AND IsDelete=0;
                            SELECT IID,Name,CategoryID FROM Tb_Rental_TagSetting
                            WHERE CategoryID IN
                            (
                                  SELECT IID FROM Tb_Rental_TagCategory WHERE CorpID=@CorpID AND IsDelete=0 AND BussType like '%{bussType}%'
                            ) AND ISNULL(IsDelete,0)=0;";

                var reader = conn.QueryMultiple(sql, new { CorpID = community.CorpID });
                var categories = reader.Read();
                var tags = reader.Read().ToList();

                var list = new List<dynamic>();

                foreach (dynamic item in categories)
                {
                    var tmp = tags.FindAll(obj => obj.CategoryID == item.IID)
                        .Select(obj => new { ID = obj.IID, Name = obj.Name });

                    if (tmp.Any())
                    {
                        list.Add(new
                        {
                            ID = item.IID,
                            Name = item.Name,
                            Tags = tmp
                        });
                    }
                }

                return new ApiResult(true, list).toJson();
            }
        }
    }
}
