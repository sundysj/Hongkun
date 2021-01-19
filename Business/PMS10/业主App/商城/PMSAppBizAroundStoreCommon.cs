using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Business.PMS10.业主App.商城.Model;
using Common;
using Dapper;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using static Dapper.SqlMapper;


namespace Business
{
    /// <summary>
    /// 周边商家首页接口
    /// </summary>
    public class PMSAppBizAroundStoreCommon : PubInfo
    {
        public PMSAppBizAroundStoreCommon()
        {
            base.Token = "20200227PMSAppBizAroundStoreCommon";
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
                    case "GetAllAroundStoreGoodsCategory":              // 获取项目周边所有周边商家已有商品的商品分类
                        Trans.Result = GetAllAroundStoreGoodsCategory(Row);
                        break;
                    case "GetAroundStoreTopRecommendGoodsList":         // 获取周边商家首页推荐商品列表
                        Trans.Result = GetAroundStoreTopRecommendGoodsList(Row);
                        break;
                    case "GetAroundStoreTopCategoryGoodsList":          // 获取周边商家分类商品集合
                        Trans.Result = GetAroundStoreTopCategoryGoodsList(Row);
                        break;
                    case "GetAroundStoreRecommendGoodsMore":            // 获取周边商家首页更多推荐商品
                        Trans.Result = GetAroundStoreRecommendGoodsMore(Row);
                        break;
                    case "GetAroundStoreCategoryGoodsMore":             // 获取周边商家指定商品分类下更多商品 
                        Trans.Result = GetAroundStoreCategoryGoodsMore(Row);
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
        /// 获取项目周边所有周边商家商品分类
        /// </summary>
        private string GetAllAroundStoreGoodsCategory(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编码不能为空");
            }

            var communityId = row["CommunityId"].AsString();
            var community = GetCommunity(communityId);
            if (community == null)
                return JSONHelper.FromString(false, "未查找到小区信息");

            using (var conn = new SqlConnection(PubConstant.BusinessContionString))
            {
                var sql = @"SELECT ResourcesTypeID AS TypeID,ResourcesTypeName AS TypeName,ResourcesTypeImgUrl AS Icon,Remark
                            FROM Tb_Resources_Type
                            WHERE (isnull(CorpID,1000)=1000 OR CorpID=@CorpID) AND isnull(IsDelete,0)=0 
                            AND ResourcesTypeID IN
                            (
                                SELECT ResourcesTypeID FROM Tb_Resources_Details
                                WHERE IsRelease='是' AND isnull(IsStopRelease,'否')='否' AND isnull(IsDelete,0)=0
                                AND BussId IN
                                (
                                    SELECT BussId FROM Tb_System_BusinessCorp
                                    WHERE isnull(BussNature,'周边商家')='周边商家' AND isnull(IsDelete,0)=0
                                    AND BussId IN
                                    (
                                        SELECT BussId FROM Tb_System_BusinessConfig WHERE CommunityId=@CommunityId
                                        UNION ALL
                                        SELECT BussId FROM Tb_System_BusinessCorp_Config WHERE CorpId=@CorpID
                                    )
                                )
                            ) 
                            ORDER BY ResourcesTypeIndex;";

                var data = conn.Query<PMSAppGoodsCategoryModel>(sql, new { CorpID = community.CorpID, CommunityId = community.Id }).ToList();
                data.ForEach(obj => obj.Icon = obj.Icon?.Trim(','));
                return new ApiResult(true, data).toJson();
            }
        }

        private string GetAroundStoreTopRecommendGoodsList(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编码不能为空");
            }

            var communityId = row["CommunityId"].AsString();
            var community = GetCommunity(communityId);
            if (community == null)
                return JSONHelper.FromString(false, "未查找到小区信息");

            using (var conn = new SqlConnection(PubConstant.BusinessContionString))
            {
                var sql = @"SELECT TOP 6 BussID,ResourcesID AS GoodsID,ResourcesName AS GoodsName,ResourcesSimple AS GoodsSimpleName,Img AS GoodsImg,
                                   IsGroupBuy,ResourcesSalePrice AS SalePrice,ResourcesDisCountPrice AS DiscountPrice,
                                   CASE WHEN IsGroupBuy='是' AND GroupBuyStartDate<=getdate() AND GroupBuyEndDate>=getdate()
                                        THEN isnull(GroupBuyPrice,-1.0)
                                        ELSE ResourcesSalePrice-isnull(ResourcesDisCountPrice,0.0) END AS ActualPrice,
                                   CASE WHEN IsBp='是' THEN 1 ELSE 0 END AS IsHot,
                                   CASE WHEN IsRecommend='是' THEN 1 ELSE 0 END AS IsRecommend
                            FROM Tb_Resources_Details
                            WHERE IsRelease='是' AND isnull(IsStopRelease,'否')='否' AND isnull(IsDelete,0)=0 AND IsRecommend='是'
                            AND BussId IN 
                            (
                                SELECT BussId FROM Tb_System_BusinessCorp
                                WHERE isnull(BussNature,'周边商家')='周边商家' AND isnull(IsDelete,0)=0
                                AND BussId IN
                                (
                                    SELECT BussId FROM Tb_System_BusinessConfig WHERE CommunityId=@CommunityId
                                    UNION ALL
                                    SELECT BussId FROM Tb_System_BusinessCorp_Config WHERE CorpId=@CorpID
                                )
                            )
                            ORDER BY IsRecommend DESC,RecommendSetDate DESC,CreateDate DESC;";

                var data = conn.Query<PMSAppGoodsSimpleModel>(sql, new { CorpID = community.CorpID, CommunityId = community.Id }).ToList();
                data.ForEach(obj =>
                {
                    obj.GoodsImg = obj.GoodsImg?.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)[0];
                });
                return new ApiResult(true, data).toJson();
            }
        }

        /// <summary>
        /// 获取周边商家首页分类商品集合
        /// </summary>
        private string GetAroundStoreTopCategoryGoodsList(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编码不能为空");
            }

            var communityId = row["CommunityId"].AsString();
            var community = GetCommunity(communityId);
            if (community == null)
                return JSONHelper.FromString(false, "未查找到小区信息");

            using (var conn = new SqlConnection(PubConstant.BusinessContionString))
            {
                var sql = @"SELECT ResourcesTypeID AS TypeID,ResourcesTypeName AS TypeName,ResourcesTypeImgUrl AS Icon,Remark
                            FROM Tb_Resources_Type
                            WHERE (isnull(CorpID,1000)=1000 OR CorpID=@CorpID) AND isnull(IsDelete,0)=0 
                            AND ResourcesTypeID IN
                            (
                                SELECT ResourcesTypeID FROM Tb_Resources_Details
                                WHERE IsRelease='是' AND isnull(IsStopRelease,'否')='否' AND isnull(IsDelete,0)=0
                                AND BussId IN
                                (
                                    SELECT BussId FROM Tb_System_BusinessCorp
                                    WHERE isnull(BussNature,'周边商家')='周边商家' AND isnull(IsDelete,0)=0
                                    AND BussId IN
                                    (
                                        SELECT BussId FROM Tb_System_BusinessConfig WHERE CommunityId=@CommunityId
                                        UNION ALL
                                        SELECT BussId FROM Tb_System_BusinessCorp_Config WHERE CorpId=@CorpID
                                    )
                                )
                            ) 
                            ORDER BY ResourcesTypeIndex ASC,CorpID DESC;";

                var data = conn.Query<PMSAppGoodsCategoryModel>(sql, new { CorpID = community.CorpID, CommunityId = community.Id }).ToList();
                data.ForEach(obj => obj.Icon = obj.Icon?.Trim(','));

                if (data.Count > 3)
                {
                    data.RemoveRange(3, data.Count - 3);
                }

                sql = @"SELECT TOP 6 BussID,ResourcesID AS GoodsID,ResourcesName AS GoodsName,ResourcesSimple AS GoodsSimpleName,Img AS GoodsImg,
                                IsGroupBuy,ResourcesSalePrice AS SalePrice,ResourcesDisCountPrice AS DiscountPrice,
                                CASE WHEN IsGroupBuy='是' AND GroupBuyStartDate<=getdate() AND GroupBuyEndDate>=getdate()
                                    THEN isnull(GroupBuyPrice,-1.0)
                                    ELSE ResourcesSalePrice-isnull(ResourcesDisCountPrice,0.0) END AS ActualPrice,
                                CASE WHEN IsBp='是' THEN 1 ELSE 0 END AS IsHot,
                                CASE WHEN IsRecommend='是' THEN 1 ELSE 0 END AS IsRecommend
                        FROM Tb_Resources_Details
                        WHERE ResourcesTypeID=@TypeID AND IsRelease='是' AND isnull(IsStopRelease,'否')='否' AND isnull(IsDelete,0)=0  
                        AND BussId IN 
                        (
                            SELECT BussId FROM Tb_System_BusinessCorp
                            WHERE isnull(BussNature,'周边商家')='周边商家' AND isnull(IsDelete,0)=0
                            AND BussId IN
                            (
                                SELECT BussId FROM Tb_System_BusinessConfig WHERE CommunityId=@CommunityId
                                UNION ALL
                                SELECT BussId FROM Tb_System_BusinessCorp_Config WHERE CorpId=@CorpID
                            )
                        )
                        ORDER BY IsRecommend DESC,RecommendSetDate DESC,CreateDate DESC;";

                foreach (var item in data)
                {
                    var goods = conn.Query<PMSAppGoodsSimpleModel>(sql, new { TypeID = item.TypeID, CorpID = community.CorpID, CommunityId = community.Id }).ToList();
                    goods.ForEach(obj =>
                    {
                        obj.GoodsImg = obj.GoodsImg?.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)[0];
                    });
                    item.Goods = new List<PMSAppGoodsSimpleModel>();
                    item.Goods.AddRange(goods);
                }

                return new ApiResult(true, data).toJson();
            }
        }

        /// <summary>
        /// 获取周边商家首页更多推荐商品
        /// </summary>
        private string GetAroundStoreRecommendGoodsMore(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编码不能为空");
            }

            var communityId = row["CommunityId"].AsString();
            var community = GetCommunity(communityId);
            if (community == null)
                return JSONHelper.FromString(false, "未查找到小区信息");

            var pageSize = 10;
            var pageIndex = 1;
            if (row.Table.Columns.Contains("PageSize") && !string.IsNullOrEmpty(row["PageSize"].AsString()))
            {
                pageSize = AppGlobal.StrToInt(row["PageSize"].AsString());
            }
            if (row.Table.Columns.Contains("PageIndex") && !string.IsNullOrEmpty(row["PageIndex"].AsString()))
            {
                pageIndex = AppGlobal.StrToInt(row["PageIndex"].AsString());
            }

            using (var conn = new SqlConnection(PubConstant.BusinessContionString))
            {
                var sql = $@"SELECT BussID,ResourcesID AS GoodsID,ResourcesName AS GoodsName,ResourcesSimple AS GoodsSimpleName,Img AS GoodsImg,
                                   CreateDate,IsGroupBuy,ResourcesSalePrice AS SalePrice,ResourcesDisCountPrice AS DiscountPrice,
                                   CASE WHEN IsGroupBuy='是' AND GroupBuyStartDate<=getdate() AND GroupBuyEndDate>=getdate()
                                        THEN isnull(GroupBuyPrice,-1.0)
                                        ELSE ResourcesSalePrice-isnull(ResourcesDisCountPrice,0.0) END AS ActualPrice,
                                   CASE WHEN IsBp='是' THEN 1 ELSE 0 END AS IsHot,
                                   CASE WHEN IsRecommend='是' THEN 1 ELSE 0 END AS IsRecommend,RecommendSetDate,
                                   (
                                        SELECT count(1) FROM Tb_Charge_ReceiptDetail x
                                        LEFT JOIN Tb_Charge_Receipt y ON x.ReceiptCode=y.Id
                                        WHERE x.ResourcesID=a.ResourcesID AND isnull(y.IsDelete,0)=0 AND y.IsPay='已付款'
                                    ) AS SoldCount 
                            FROM Tb_Resources_Details a
                            WHERE IsRelease='是' AND isnull(IsStopRelease,'否')='否' AND IsRecommend='是' AND isnull(IsDelete,0)=0
                            AND BussID IN
                            (
                                SELECT BussId FROM Tb_System_BusinessCorp
                                WHERE isnull(BussNature,'周边商家')='周边商家' AND isnull(IsDelete,0)=0
                                AND BussId IN
                                (
                                    SELECT BussId FROM Tb_System_BusinessConfig WHERE CommunityId='{community.Id}'
                                    UNION ALL 
                                    SELECT BussId FROM Tb_System_BusinessCorp_Config WHERE CorpId={community.CorpID}
                                )
                            )";

                var data = GetListDapper<PMSAppGoodsSimpleModel>(out int pageCount, out int count, sql, pageIndex, pageSize,
                    "RecommendSetDate", 1, "GoodsID", conn).ToList();
                data.ForEach(obj =>
                {
                    obj.GoodsImg = obj.GoodsImg?.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)[0];
                });

                var json = new ApiResult(true, data).toJson();
                return json.Insert(json.Length - 1, ",\"PageCount\":" + pageCount);
            }
        }

        /// <summary>
        /// 获取周边商家首页指定商品分类下更多商品
        /// </summary>
        private string GetAroundStoreCategoryGoodsMore(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编码不能为空");
            }
            if (!row.Table.Columns.Contains("TypeID") || string.IsNullOrEmpty(row["TypeID"].ToString()))
            {
                return JSONHelper.FromString(false, "商品分类不能为空");
            }

            var communityId = row["CommunityId"].AsString();
            var community = GetCommunity(communityId);
            if (community == null)
                return JSONHelper.FromString(false, "未查找到小区信息");

            var typeId = row["TypeID"].AsString();

            var pageSize = 10;
            var pageIndex = 1;
            if (row.Table.Columns.Contains("PageSize") && !string.IsNullOrEmpty(row["PageSize"].AsString()))
            {
                pageSize = AppGlobal.StrToInt(row["PageSize"].AsString());
            }
            if (row.Table.Columns.Contains("PageIndex") && !string.IsNullOrEmpty(row["PageIndex"].AsString()))
            {
                pageIndex = AppGlobal.StrToInt(row["PageIndex"].AsString());
            }

            using (var conn = new SqlConnection(PubConstant.BusinessContionString))
            {
                var sql = $@"SELECT BussID,ResourcesID AS GoodsID,ResourcesName AS GoodsName,ResourcesSimple AS GoodsSimpleName,Img AS GoodsImg,
                                   CreateDate,IsGroupBuy,ResourcesSalePrice AS SalePrice,ResourcesDisCountPrice AS DiscountPrice,
                                   CASE WHEN IsGroupBuy='是' AND GroupBuyStartDate<=getdate() AND GroupBuyEndDate>=getdate()
                                        THEN isnull(GroupBuyPrice,-1.0)
                                        ELSE ResourcesSalePrice-isnull(ResourcesDisCountPrice,0.0) END AS ActualPrice,
                                   CASE WHEN IsBp='是' THEN 1 ELSE 0 END AS IsHot,
                                   CASE WHEN IsRecommend='是' THEN 1 ELSE 0 END AS IsRecommend,RecommendSetDate,
                                   (
                                        SELECT count(1) FROM Tb_Charge_ReceiptDetail x
                                        LEFT JOIN Tb_Charge_Receipt y ON x.ReceiptCode=y.Id
                                        WHERE x.ResourcesID=a.ResourcesID AND isnull(y.IsDelete,0)=0 AND y.IsPay='已付款'
                                    ) AS SoldCount 
                            FROM Tb_Resources_Details a
                            WHERE ResourcesTypeID='{typeId}' AND IsRelease='是' AND isnull(IsStopRelease,'否')='否' AND isnull(IsDelete,0)=0
                            AND BussID IN
                            (
                                SELECT BussId FROM Tb_System_BusinessCorp
                                WHERE isnull(BussNature,'周边商家')='周边商家' AND isnull(IsDelete,0)=0
                                AND BussId IN
                                (
                                    SELECT BussId FROM Tb_System_BusinessConfig WHERE CommunityId='{community.Id}'
                                    UNION ALL 
                                    SELECT BussId FROM Tb_System_BusinessCorp_Config WHERE CorpId={community.CorpID}
                                )
                            )";

                var data = GetListDapper<PMSAppGoodsSimpleModel>(out int pageCount, out int count, sql, pageIndex, pageSize,
                    "CreateDate", 1, "GoodsID", conn).ToList();
                data.ForEach(obj =>
                {
                    obj.GoodsImg = obj.GoodsImg?.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)[0];
                });

                var json = new ApiResult(true, data).toJson();
                return json.Insert(json.Length - 1, ",\"PageCount\":" + pageCount);
            }
        }
    }
}
