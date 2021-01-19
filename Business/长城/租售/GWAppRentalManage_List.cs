using Common;
using Common.Enum;
using Common.Extenions;
using Dapper;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using MobileSoft.Model.Unified;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        /// 获取精品房源
        /// </summary>
        private string GetQualityRooms(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].AsString()))
            {
                return new ApiResult(false, "缺少参数CommunityId").toJson();
            }
            if (!row.Table.Columns.Contains("PageIndex") || string.IsNullOrEmpty(row["PageIndex"].AsString()))
            {
                return new ApiResult(false, "缺少参数PageIndex").toJson();
            }

            var pageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());
            var pageSize = 10;
            if (row.Table.Columns.Contains("PageSize") || !string.IsNullOrEmpty(row["PageSize"].AsString()))
            {
                pageSize = AppGlobal.StrToInt(row["PageSize"].ToString());
            }

            var communityId = row["CommunityId"].ToString();
            var community = GetCommunity(communityId);
            if (community == null)
            {
                return JSONHelper.FromString(false, "未查询到小区数据");
            }

            // 首页
            var homepage = false;
            if (row.Table.Columns.Contains("Homepage") || !string.IsNullOrEmpty(row["Homepage"].AsString()))
            {
                if (homepage = AppGlobal.StrToInt(row["Homepage"].AsString()) > 0)
                {
                    pageIndex = 1;
                    pageSize = 6;
                }
            }

            using (var conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                /* 
                 * 1、判断本小区是否有精品房源；
                 * 2、判断本城市是否有精品房源；
                 * 3、判断本公司是否有精品房源；
                 * 4、将本项目的所有房源提升至精品房源，不判断 IsQuality 字段
                 */

                var sql = @"SELECT * FROM
                            (
                                SELECT Id,Title,CommunityId,City,BussType,BuildingArea,HouseType,Renovation,Amount,PubDate,Img,Tags,
                                    row_number() OVER (ORDER BY SN1,SN2,QualitySetTime DESC) AS RID
                                FROM
                                (
                                    SELECT Id,Title,BussType,BuildingArea,Renovation,Amount,PubDate,CommunityId,City,QualitySetTime,
                                        CASE a.BedRooms WHEN 0 THEN '' ELSE substring('123456789',a.BedRooms,1)+'室' END+
                                        CASE a.LivingRooms WHEN 0 THEN '' ELSE substring('123456789',a.LivingRooms,1)+'厅' END+
                                        --CASE ISNULL(a.KitchenRooms ,0) WHEN 0 THEN '' ELSE substring('123456789',a.KitchenRooms,1)+'厨' END+
                                        CASE a.BathRooms WHEN 0 THEN '' ELSE substring('123456789',a.BathRooms,1)+'卫' END AS HouseType,
                                        CASE CommunityId WHEN @CommunityId THEN 0 ELSE 1 END AS SN1,
                                        CASE City WHEN @City THEN 0 ELSE 1 END AS SN2,
                                        (SELECT Top 1 Value FROM SplitString(Img,',',1)) AS Img,
                                        stuff((SELECT ','+Name FROM Tb_Rental_TagSetting 
                                            WHERE IID IN
                                            (
                                                SELECT VALUE FROM SplitString(
                                                (SELECT stuff((SELECT ','+Tags FROM Tb_Rental_RoomTag x 
                                                                WHERE x.RentalID=a.Id AND X.CategoryID IN (2,5) FOR XML PATH('')),1,1,'')),',',1)
                                                ) ORDER BY Sort DESC FOR XML PATH('')
                                        ),1,1,'') AS Tags
                                    FROM Tb_Rental a 
                                    WHERE ProcessState='发布' AND IsQuality=1 AND IsDelete=0 AND BussType in ('整租','出售','求购')
                                ) AS t
                            ) AS t
                            WHERE t.RID>(@PageIndex-1)*@PageSize AND t.RID<=(@PageIndex*@PageSize);

                            SELECT count(1) FROM Tb_Rental 
                            WHERE ProcessState='发布' AND IsQuality=1 AND IsDelete=0;";

                var reader = conn.QueryMultiple(sql, new
                {
                    CommunityId = community.Id,
                    City = community.City,
                    PageIndex = pageIndex,
                    PageSize = pageSize
                });

                var data = reader.Read();
                var total = reader.Read<int>().FirstOrDefault();

                if (data.Count() == 0)
                {
                    var tmp = GetAllRooms(community.Id, community.City, pageIndex, pageSize, conn);
                    data = tmp.Data;
                    total = tmp.Total;
                }

                var pageCount = (int)Math.Ceiling((double)total / pageSize);

                var json = new ApiResult(true, data).toJson();
                json = json.Insert(json.Length - 1, $",\"PageCount\":{pageCount}");
                return json;
            }
        }

        /// <summary>
        /// 获取推荐房源
        /// </summary>
        private string GetRecommendRooms(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].AsString()))
            {
                return new ApiResult(false, "缺少参数CommunityId").toJson();
            }
            if (!row.Table.Columns.Contains("PageIndex") || string.IsNullOrEmpty(row["PageIndex"].AsString()))
            {
                return new ApiResult(false, "缺少参数PageIndex").toJson();
            }

            var pageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());
            var pageSize = 10;
            if (row.Table.Columns.Contains("PageSize") || !string.IsNullOrEmpty(row["PageSize"].AsString()))
            {
                pageSize = AppGlobal.StrToInt(row["PageSize"].ToString());
            }

            var communityId = row["CommunityId"].ToString();
            var community = GetCommunity(communityId);
            if (community == null)
            {
                return JSONHelper.FromString(false, "未查询到小区数据");
            }

            // 首页
            var homepage = false;
            if (row.Table.Columns.Contains("Homepage") || !string.IsNullOrEmpty(row["Homepage"].AsString()))
            {
                if (homepage = AppGlobal.StrToInt(row["Homepage"].AsString()) > 0)
                {
                    pageIndex = 1;
                    pageSize = 10;
                }
            }

            using (var conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                /* 
                 * 1、判断本小区是否有推荐房源；
                 * 2、判断本城市是否有推荐房源；
                 * 3、判断本公司是否有推荐房源；
                 * 4、将本项目的所有房源提升至精品房源，不判断 IsRecommend 字段
                 */

                var sql = @"SELECT * FROM
                            (
                                SELECT Id,Title,CommunityId,City,BussType,BuildingArea,HouseType,Renovation,Amount,PubDate,Img,Tags,
                                    row_number() OVER (ORDER BY SN1,SN2,RecommendSetTime DESC) AS RID
                                FROM
                                (
                                    SELECT Id,Title,BussType,BuildingArea,Renovation,Amount,PubDate,CommunityId,City,RecommendSetTime,
                                        CASE a.BedRooms WHEN 0 THEN '' ELSE substring('123456789',a.BedRooms,1)+'室' END+
                                        CASE a.LivingRooms WHEN 0 THEN '' ELSE substring('123456789',a.LivingRooms,1)+'厅' END+
                                        CASE ISNULL(a.KitchenRooms ,0)  WHEN 0 THEN '' ELSE substring('123456789',a.KitchenRooms,1)+'厨' END+
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
											(SELECT stuff((SELECT ','+Tags FROM Tb_Rental_RoomTag x 
											WHERE x.RentalID =a.Id AND X.CategoryID IN (2,5) FOR XML PATH('')),1,1,'')),',',1)
											) 
											) AS T   ORDER BY Sort DESC FOR XML PATH('')
											),1,1,'')
										else 
                                        stuff((SELECT ','+Name FROM Tb_Rental_TagSetting 
                                            WHERE IID IN
                                            (
                                                SELECT VALUE FROM SplitString(
                                                (SELECT stuff((SELECT ','+Tags FROM Tb_Rental_RoomTag x 
                                                                WHERE x.RentalID=a.Id  AND X.CategoryID IN (2,5) FOR XML PATH('')),1,1,'')),',',1)
                                                ) ORDER BY Sort DESC FOR XML PATH('')
                                        ),1,1,'')
										end as Tags
                                    FROM Tb_Rental a
                                    WHERE ProcessState='发布' AND IsRecommend=1 AND IsDelete=0 AND BussType in ('整租','出售','求购')
                                ) AS t
                            ) AS t
                            WHERE t.RID>(@PageIndex-1)*@PageSize AND t.RID<=(@PageIndex*@PageSize);

                            SELECT count(1) FROM Tb_Rental 
                            WHERE ProcessState='发布' AND IsRecommend=1 AND IsDelete=0;";

                var reader = conn.QueryMultiple(sql, new
                {
                    CommunityId = community.Id,
                    City = community.City,
                    PageIndex = pageIndex,
                    PageSize = pageSize
                });

                var data = reader.Read();
                var total = reader.Read<int>().FirstOrDefault();

                if (data.Count() == 0)
                {
                    var tmp = GetAllRooms(community.Id, community.City, pageIndex, pageSize, conn);
                    data = tmp.Data;
                    total = tmp.Total;
                }

                var pageCount = (int)Math.Ceiling((double)total / pageSize);

                var json = new ApiResult(true, data).toJson();
                json = json.Insert(json.Length - 1, $",\"PageCount\":{pageCount}");
                return json;
            }
        }

        /// <summary>
        /// 分页获取所有房源信息
        /// </summary>
        private static (IEnumerable<dynamic> Data, int Total) GetAllRooms(string communityId, string city,
            int pageIndex, int pageSize, SqlConnection conn)
        {
            var sql = @"SELECT * FROM
                        (
                            SELECT Id,Title,CommunityId,City,BussType,BuildingArea,HouseType,Renovation,Amount,PubDate,Img,Tags,
                                row_number() OVER (ORDER BY SN1,SN2) AS RID
                            FROM
                            (
                                SELECT Id,Title,BussType,BuildingArea,Renovation,Amount,PubDate,CommunityId,City,
                                    CASE a.BedRooms WHEN 0 THEN '' ELSE substring('123456789',a.BedRooms,1)+'室' END+
                                    CASE a.LivingRooms WHEN 0 THEN '' ELSE substring('123456789',a.LivingRooms,1)+'厅' END+
                                    --CASE ISNULL(a.KitchenRooms ,0) WHEN 0 THEN '' ELSE substring('123456789',a.KitchenRooms,1)+'厨' END+
                                    CASE a.BathRooms WHEN 0 THEN '' ELSE substring('123456789',a.BathRooms,1)+'卫' END AS HouseType,
                                    CASE CommunityId WHEN @CommunityId THEN 0 ELSE 1 END AS SN1,
                                    CASE City WHEN @City THEN 0 ELSE 1 END AS SN2,
                                    (SELECT Top 1 Value FROM SplitString(Img,',',1)) AS Img,
                                    stuff((SELECT ','+Name FROM Tb_Rental_TagSetting 
                                            WHERE IID IN
                                            (
                                                SELECT VALUE FROM SplitString(
                                                (SELECT stuff((SELECT ','+Tags FROM Tb_Rental_RoomTag x 
                                                                WHERE x.RentalID=a.Id  AND X.CategoryID IN (2,5) FOR XML PATH('')),1,1,'')),',',1)
                                                ) ORDER BY Sort DESC FOR XML PATH('')
                                        ),1,1,'') AS Tags 
                                FROM Tb_Rental a
                                WHERE ProcessState='发布' AND IsDelete=0
                            ) AS t
                        ) AS t
                        WHERE t.RID>(@PageIndex-1)*@PageSize AND t.RID<=(@PageIndex*@PageSize);

                        SELECT count(1) FROM Tb_Rental 
                        WHERE ProcessState='发布' AND IsDelete=0;";

            var reader = conn.QueryMultiple(sql, new
            {
                CommunityId = communityId,
                City = city,
                PageIndex = pageIndex,
                PageSize = pageSize
            });

            var data = reader.Read();
            var total = reader.Read<int>().FirstOrDefault();

            return (data, total);
        }

        /// <summary>
        /// 获取用户发布历史
        /// </summary>
        private string GetPublishHistory(DataRow row)
        {
            if (!row.Table.Columns.Contains("UserId") || string.IsNullOrEmpty(row["UserId"].AsString()))
            {
                return new ApiResult(false, "缺少参数UserId").toJson();
            }
            if (!row.Table.Columns.Contains("PageIndex") || string.IsNullOrEmpty(row["PageIndex"].AsString()))
            {
                return new ApiResult(false, "缺少参数PageIndex").toJson();
            }

            var userId = row["UserId"].ToString();
            var pageIndex = AppGlobal.StrToInt(row["pageIndex"].ToString());
            var pageSize = 10;

            using (var conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                // 获取当前用户的发布信息
                var sql = @"SELECT * FROM
                            (
                                SELECT a.Id,a.Title,a.BussType,a.BuildingArea,a.BedRooms,a.Amount,a.Address,a.City,a.Renovation,y.CommName,A.PubDate,A.IsAudit,a.ProcessState,LivingRooms,BathRooms,HasElevator,
 LinkManTel,LinkMan,SalesMan,SalesMobile,BuildingAreaMax,Orientation,AmountMax,[Description],CommunityId,
                                    CASE a.ProcessState WHEN '屏蔽' THEN '受理' ELSE a.ProcessState END AS State,
                                    CASE a.BedRooms WHEN 0 THEN '' ELSE substring('123456789',a.BedRooms,1)+'室' END+
                                    CASE a.LivingRooms WHEN 0 THEN '' ELSE substring('123456789',a.LivingRooms,1)+'厅' END+
                                    --CASE a.KitchenRooms WHEN 0 THEN '' ELSE substring('123456789',a.KitchenRooms,1)+'厨' END+
                                    CASE a.BathRooms WHEN 0 THEN '' ELSE substring('123456789',a.BathRooms,1)+'卫' END AS HouseType,
                                    (SELECT TOP 1 Value FROM SplitString(a.Img,',',1)) AS Img,
                                    CASE WHEN BussType IN ('整租','合租')  THEN
										STUFF((
											SELECT  ','+[Name] FROM (
											SELECT  row_number() over(partition by CATEGORYID order by IID  ) as rowIndex,* FROM Tb_Rental_TagSetting 
											 WHERE IID IN
											(
											SELECT VALUE FROM SplitString(
											(SELECT stuff((SELECT ','+Tags FROM (SELECT top 3 * FROM Tb_Rental_RoomTag x  WHERE x.RentalID =a.Id  AND X.CategoryID IN (2,5)) as R  FOR XML PATH('')),1,1,'')),',',1)
											) 
											) AS T  ORDER BY Sort DESC FOR XML PATH('')
											),1,1,'')

										WHEN BussType = '出售'  THEN
										STUFF((
											SELECT  ','+[Name] FROM (
											SELECT  row_number() over(partition by CATEGORYID order by IID  ) as rowIndex,* FROM Tb_Rental_TagSetting 
											 WHERE IID IN
											(
											SELECT VALUE FROM SplitString(
											(SELECT stuff((SELECT ','+Tags FROM (SELECT top 3 * FROM Tb_Rental_RoomTag x  WHERE x.RentalID =a.Id  AND X.CategoryID IN (2,5)) as R  FOR XML PATH('')),1,1,'')),',',1)
											) 
											) AS T   ORDER BY Sort DESC FOR XML PATH('')
											),1,1,'')
										else 
                                        stuff((SELECT ','+Name FROM Tb_Rental_TagSetting 
                                            WHERE IID IN
                                            (
                                                SELECT VALUE FROM SplitString(
                                                (SELECT stuff((SELECT ','+Tags FROM (SELECT top 3 * FROM Tb_Rental_RoomTag x  WHERE x.RentalID =a.Id  AND X.CategoryID IN (2,5)) as R FOR XML PATH('')),1,1,'')),',',1)
                                                ) ORDER BY Sort DESC FOR XML PATH('')
                                        ),1,1,'')
										end as Tags,
                                    row_number() OVER (ORDER BY a.PubDate DESC) AS RID
                                FROM Tb_Rental a
                                LEFT JOIN Tb_Community AS y ON a.CommunityId=y.Id
                                WHERE a.UserId=@UserId AND a.IsDelete=0 
                            ) AS t
                            WHERE t.RID>(@PageIndex-1)*@PageSize AND t.RID<=(@PageIndex*@PageSize)
                            ORDER BY t.PubDate DESC ;";
                var data = conn.Query(sql, new { UserId = userId, PageIndex = pageIndex, PageSize = pageSize });
                return new ApiResult(true, data).toJson();
            }
        }

        /// <summary>
        /// 获取当前小区租赁的轮播图
        /// </summary>
        private string GetRotation(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].AsString()))
            {
                return new ApiResult(false, "缺少参数CommunityId").toJson();
            }
            var communityId = row["CommunityId"].ToString();
            var community = GetCommunity(communityId);
            // 长城业主App，新增了是否上架的字段。
            using (var conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                // 获取当前用户的发布信息
                var sql = @"SELECT  *,
                                (SELECT Top 1 Value FROM SplitString(ImageURL,',',1)) AS ImageURL
                            FROM Tb_Notice 
                            WHERE CommunityId  LIKE @CommunityId 
                                AND NoticeType=4
                                AND IsTheShelves=1
                                AND  CONVERT(NVARCHAR(30),EffectiveBegDate,120) < CONVERT(NVARCHAR(30),GETDATE(),120)
                                AND  CONVERT(NVARCHAR(30),GETDATE(),120) < CONVERT(NVARCHAR(30),EffectiveEndDate,120)
                                AND IsDelete=0;";

                var data = conn.Query(sql, new { CommunityId = String.Format("%{0}%", communityId) });

                sql = "SELECT isnull(col_length('Tb_Notice', 'JumpMode'),0)";
                bool isEx = conn.Query<long>(sql).FirstOrDefault() > 0;
                if (isEx && data.Count() > 0)
                {
                    int jumpType = 0, jumpModel = 0;
                    foreach (var model in data)
                    {
                        if (!String.IsNullOrEmpty((String)model.JumpMode)) jumpType = EnumHelper.GetEnumValueByDesc(typeof(JumpModelEnum), (String)model.JumpMode);
                        if (!String.IsNullOrEmpty((String)model.InternalJumpType)) jumpModel = EnumHelper.GetEnumValueByDesc(typeof(JumpTypeEnum), (String)model.InternalJumpType);

                        model.JumpMode = jumpType.ToString();
                        model.InternalJumpType = jumpModel.ToString();
                    }
                }
                return new ApiResult(true, data).toJson();
            }
        }

        /// <summary>
        /// 获取用户发布求组 求购的历史记录
        /// </summary>
        private string GetPublishBuyAndRentHistory(DataRow row)
        {
            if (!row.Table.Columns.Contains("UserId") || string.IsNullOrEmpty(row["UserId"].AsString()))
            {
                return new ApiResult(false, "缺少参数UserId").toJson();
            }
            if (!row.Table.Columns.Contains("PageIndex") || string.IsNullOrEmpty(row["PageIndex"].AsString()))
            {
                return new ApiResult(false, "缺少参数PageIndex").toJson();
            }

            var userId = row["UserId"].ToString();
            var pageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());
            var pageSize = 10;

            using (var conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                // 获取当前用户的发布信息
                var sql = @"SELECT * FROM (
                                SELECT a.Id,a.Title,a.BussType,a.BuildingArea,a.BedRooms,a.Amount,a.Address,a.City,a.Renovation,y.CommName,A.PubDate,A.IsAudit,a.ProcessState,A.AmountMax,A.BuildingAreamax,
                                    CASE a.ProcessState WHEN '屏蔽' THEN '受理' ELSE a.ProcessState END AS State,
                                    CASE a.BedRooms WHEN 0 THEN '' ELSE substring('123456789',a.BedRooms,1)+'室' END+
                                    CASE a.LivingRooms WHEN 0 THEN '' ELSE substring('123456789',a.LivingRooms,1)+'厅' END+
                                    CASE a.BathRooms WHEN 0 THEN '' ELSE substring('123456789',a.BathRooms,1)+'卫' END AS HouseType,
                                    row_number() OVER (ORDER BY a.PubDate DESC) AS RID
                                FROM Tb_Rental a
                                LEFT JOIN Tb_Community AS y ON a.CommunityId=y.Id
                                WHERE a.UserId=@UserId AND a.IsDelete=0  AND A.BussType IN ('求购','求租') 
                            ) AS t
                            WHERE t.RID>(@PageIndex-1)*@PageSize AND t.RID<=(@PageIndex*@PageSize)
                            ORDER BY t.PubDate DESC ;";

                var data = conn.Query(sql, new { UserId = userId, PageIndex = pageIndex, PageSize = pageSize });
                return new ApiResult(true, data).toJson();
            }
        }
    }
}