using Dapper;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using static Dapper.SqlMapper;
namespace Business
{
    public partial class GWAppRentalManage
    {
        /// <summary>
        /// 获取城市及城市下项目列表
        /// </summary>
        private string GetSearchAreas()
        {
            using (var conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                var sql = @"SELECT a.City,b.Id AS CommunityId,b.CommName 
                            FROM Tb_Rental a
                            LEFT JOIN Tb_Community b ON a.CommunityId=b.Id
                            WHERE IsDelete=0 GROUP BY a.City,b.CommName;";

                var data = conn.Query(sql);
                var list = new List<dynamic>();

                foreach (dynamic item in data)
                {
                    var tmp = list.Find(obj => obj.City == item.City);
                    if (tmp == null)
                    {
                        tmp = new
                        {
                            City = item.City,
                            Commsunitys = new List<Dictionary<string, string>>()
                            {
                                new Dictionary<string, string>()
                                {
                                    ["ID"] = item.CommunityId,
                                    ["Name"] = item.Name
                                }
                            }
                        };

                        list.Add(tmp);
                    }

                    ((List<Dictionary<string, string>>)tmp.Commsunitys).Add(new Dictionary<string, string>
                    {
                        ["ID"] = item.CommunityId,
                        ["Name"] = item.Name
                    });
                }

                return new ApiResult(true, list).toJson();
            }
        }

        /// <summary>
        /// 搜索房屋
        /// </summary>
        private string SearchRoom(DataRow row)
        {
            var bussType = "";
            var title = "";
            var city = "";
            var communityId = "";
            var houseType = "";
            var amount = "";
            var pageIndex = 1;
            var pageSize = 10;

            #region 查询条件
            if (row.Table.Columns.Contains("BussType") && !string.IsNullOrEmpty(row["BussType"].AsString()))
            {
                bussType = row["BussType"].AsString();
            }
            if (row.Table.Columns.Contains("Title") && !string.IsNullOrEmpty(row["Title"].AsString()))
            {
                title = row["Title"].AsString();
            }
            if (row.Table.Columns.Contains("City") && !string.IsNullOrEmpty(row["City"].AsString()))
            {
                city = row["City"].AsString();
            }
            if (row.Table.Columns.Contains("CommunityId") && !string.IsNullOrEmpty(row["CommunityId"].AsString()))
            {
                communityId = row["CommunityId"].AsString();
            }
            if (row.Table.Columns.Contains("HouseType") && !string.IsNullOrEmpty(row["HouseType"].AsString()))
            {
                houseType = row["HouseType"].AsString();
            }
            if (row.Table.Columns.Contains("Amount") && !string.IsNullOrEmpty(row["Amount"].AsString()))
            {
                amount = row["Amount"].AsString();
            }
            if (row.Table.Columns.Contains("PageIndex") && !string.IsNullOrEmpty(row["PageIndex"].AsString()))
            {
                pageIndex = AppGlobal.StrToInt(row["PageIndex"].AsString());
            }
            #endregion 

            using (var conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                var condition = "";
                var parameter = new DynamicParameters();

                #region 查询条件
                // 业务类型
                if (!string.IsNullOrEmpty(bussType))
                {
                    if (bussType == "出租")
                    {
                        condition += " AND BussType in ('整租','合租')";
                    }
                    else
                    {
                        condition += $" AND BussType=@BussType";
                        parameter.Add("@BussType", bussType);
                    }
                }
                // 标题
                if (!string.IsNullOrEmpty(title))
                {
                    condition += $" AND Title LIKE @Title";
                    parameter.Add("@Title", $"%{ title }%");
                }

                // 城市
                if (!string.IsNullOrEmpty(city))
                {
                    condition += $" AND City=@City";
                    parameter.Add("@City", city);
                }

                // 项目
                if (!string.IsNullOrEmpty(communityId))
                {
                    condition += $" AND CommunityId=@CommunityId";
                    parameter.Add("@CommunityId", communityId);
                }

                // 户型
                if (!string.IsNullOrEmpty(houseType))
                {
                    if (houseType.Contains("+"))
                    {
                        condition += $" AND BedRooms>@BedRooms";
                        parameter.Add("@BedRooms", AppGlobal.StrToInt(houseType.Replace("+", "")));
                    }
                    else
                    {
                        condition += $" AND BedRooms=@BedRooms";
                        parameter.Add("@BedRooms", AppGlobal.StrToInt(houseType));
                    }
                }

                // 金额
                if (!string.IsNullOrEmpty(amount))
                {
                    var tmp = amount.Split('-').ToList();
                    if (tmp.Count() < 2)
                    {
                        tmp.Insert(0, "0");
                    }

                    condition += $" AND Amount>=@A1 AND Amount<=@A2";
                    parameter.Add("@A1", AppGlobal.StrToDec(tmp[0]));
                    parameter.Add("@A2", AppGlobal.StrToDec(tmp[1]));
                }
                #endregion

                String sqlNewAdd = String.Empty;
                String sqlTbRental = String.Empty;
                String sqlStr = "SELECT isnull(col_length('Tb_Rental','AmountMax'),0)";
                if (conn.Query<int>(sqlStr).FirstOrDefault() > 0)
                {
                    sqlNewAdd = "[Description],LinkMan,LinkManTel,Orientation,AmountMax,BuildingAreaMax,HasElevator,CommName,SalesMan,SalesMobile,";
                    sqlTbRental = "[Description],LinkMan,LinkManTel,Orientation,AmountMax,BuildingAreaMax,HasElevator,CommunityName as CommName,SalesMan,SalesMobile,";
                }

                var sql = $@"SELECT * FROM
                            (
                                SELECT Id,Title,CommunityId,City,BussType,BuildingArea,HouseType,Renovation,Amount,PubDate,Img,Tags,{sqlNewAdd}
                                    row_number() OVER (ORDER BY PUBDATE DESC) AS RID
                                FROM
                                (
                                    SELECT Id,Title,BussType,BuildingArea,Renovation,Amount,PubDate,CommunityId,City,{sqlTbRental}
                                        CASE a.BedRooms WHEN 0 THEN '' ELSE substring('123456789',a.BedRooms,1)+'室' END+
                                        CASE a.LivingRooms WHEN 0 THEN '' ELSE substring('123456789',a.LivingRooms,1)+'厅' END+
                                        CASE ISNULL(a.KitchenRooms ,0) WHEN 0 THEN '' ELSE substring('123456789',a.KitchenRooms,1)+'厨' END+
                                        CASE a.BathRooms WHEN 0 THEN '' ELSE substring('123456789',a.BathRooms,1)+'卫' END AS HouseType,
                                        CASE CommunityId WHEN @CommunityId THEN 0 ELSE 1 END AS SN1,
                                        CASE City WHEN @City THEN 0 ELSE 1 END AS SN2,
                                        (SELECT Top 1 Value FROM SplitString(Img,',',1)) AS Img,
                                        CASE WHEN BussType IN ('整租','合租')  THEN
										STUFF((
											SELECT  ','+[Name] FROM (
											SELECT  row_number() over(partition by CATEGORYID order by IID  ) as rowIndex,* FROM Tb_Rental_TagSetting 
											 WHERE IID IN
											(
											SELECT VALUE FROM SplitString(
											(SELECT stuff((SELECT ','+Tags FROM (SELECT top 3 * FROM Tb_Rental_RoomTag x  WHERE x.RentalID =a.Id AND X.CategoryID IN (2,5)) as R  FOR XML PATH('')),1,1,'')),',',1)
											) 
											) AS T   ORDER BY Sort DESC FOR XML PATH('')
											),1,1,'')

										WHEN BussType = '出售'  THEN
										STUFF((
											SELECT  ','+[Name] FROM (
											SELECT  row_number() over(partition by CATEGORYID order by IID  ) as rowIndex,* FROM Tb_Rental_TagSetting 
											 WHERE IID IN
											(
											SELECT VALUE FROM SplitString(
											(SELECT stuff((SELECT ','+Tags FROM (SELECT top 3 * FROM Tb_Rental_RoomTag x  WHERE x.RentalID =a.Id AND X.CategoryID IN (2,5)) as R  FOR XML PATH('')),1,1,'')),',',1)
											) 
											) AS T   ORDER BY Sort DESC FOR XML PATH('')
											),1,1,'')
										else 
                                        stuff((SELECT ','+Name FROM Tb_Rental_TagSetting 
                                            WHERE IID IN
                                            (
                                                SELECT VALUE FROM SplitString(
                                                (SELECT stuff((SELECT ','+Tags FROM (SELECT top 3 * FROM Tb_Rental_RoomTag x  WHERE x.RentalID =a.Id AND X.CategoryID IN (2,5)) as R FOR XML PATH('')),1,1,'')),',',1)
                                                ) ORDER BY Sort DESC FOR XML PATH('')
                                        ),1,1,'')
										end as Tags 
                                    FROM Tb_Rental a
                                    WHERE ProcessState='发布' AND IsDelete=0 { condition }
                                ) AS t
                            ) AS t
                            WHERE t.RID>(@PageIndex-1)*@PageSize AND t.RID<=(@PageIndex*@PageSize);

                            SELECT count(1) FROM Tb_Rental 
                            WHERE ProcessState='发布' AND IsDelete=0 { condition };";

                parameter.Add("@CommunityId", communityId);
                parameter.Add("@City", city);
                parameter.Add("@PageIndex", pageIndex);
                parameter.Add("@PageSize", pageSize);

                var reader = conn.QueryMultiple(sql, parameter);
                var data = reader.Read();
                var total = reader.Read<int>().FirstOrDefault();

                var pageCount = (int)Math.Ceiling((double)total / pageSize);

                var json = new ApiResult(true, data).toJson();
                json = json.Insert(json.Length - 1, $",\"PageCount\":{pageCount}");
                return json;
            }
        }
    }
}
