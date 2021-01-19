using System;
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
    public class PMSAppBizCommon : PubInfo
    {
        public PMSAppBizCommon()
        {
            base.Token = "20200221PMSAppBizCommon";
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
                    // 商城首页
                    case "GetTopAdvertisingGoodsList":      // 获取商城首页顶部商品广告
                        Trans.Result = GetTopAdvertisingGoodsList(Row);
                        break;
                    case "GetTopGoodsCategoryList":         // 获取商城首页商品分类
                        Trans.Result = GetTopGoodsCategoryList(Row);
                        break;
                    case "GetTopRecommendGoodsList":        // 获取商城首页推荐商品列表
                        Trans.Result = GetTopRecommendGoodsList(Row);
                        break;

                    // 店铺首页
                    case "GetStoreGoodsCategory":           // 获取商家店铺首页商品分类
                        Trans.Result = GetStoreGoodsCategory(Row);
                        break;
                    case "GetStoreRecommendGoodsList":      // 获取商家店铺首页推荐商品列表
                        Trans.Result = GetStoreRecommendGoodsList(Row);
                        break;
                    case "GetStoreCategoryGoodsList":       // 获取商家店铺首页商品分类下商品列表
                        Trans.Result = GetStoreCategoryGoodsList(Row);
                        break;

                    // 通用
                    case "GetCategoryGoodsList":            // 获取商品分类下商品列表，不区分平台商城、物管商城、周边商家
                        Trans.Result = GetCategoryGoodsList(Row);
                        break;
                    case "GetRecommendGoodsList":           // 获取推荐商品列表，不区分平台商城、物管商城、周边商家
                        Trans.Result = GetRecommendGoodsList(Row);
                        break;
                    case "GetStoreInfo":                    // 获取店铺信息
                        Trans.Result = GetStoreInfo(Row);
                        break;

                    // 其他
                    case "GetServiceGoodsList":             // 特约服务
                        Trans.Result = GetServiceGoodsList(Row);
                        break;
                    case "GetValueAddedServiceGoodsList":   // 增值服务
                        Trans.Result = GetValueAddedServiceGoodsList(Row);
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
        /// 获取商城首页顶部广告
        /// </summary>
        private string GetTopAdvertisingGoodsList(DataRow row)
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
                var sql = @"SELECT TOP 5 BussID,ResourcesID AS GoodsID,ResourcesName AS GoodsName,Img AS GoodsImg
                            FROM Tb_Resources_Details
                            WHERE IsRelease='是' AND isnull(IsStopRelease,'否')='否' AND IsTopAD='是' AND isnull(IsDelete,0)=0
                            AND BussId IN
                            (
                                SELECT BussId FROM Tb_System_BusinessCorp_Config WHERE CorpId=@CorpID
                                UNION
                                SELECT BussId FROM Tb_System_BusinessConfig WHERE CommunityId=@CommunityId
                            ) ORDER BY TopADSetDate DESC;";

                var data = conn.Query(sql, new { CorpID = community.CorpID, CommunityId = community.Id }).ToList();
                data.ForEach(obj =>
                {
                    obj.GoodsImg = obj.GoodsImg?.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)[0];
                });
                return new ApiResult(true, data).toJson();
            }
        }

        /// <summary>
        /// 获取商城首页商品分类列表
        /// </summary>
        private string GetTopGoodsCategoryList(DataRow row)
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
                var sql = @"SELECT TOP 8 ResourcesTypeID AS TypeID,ResourcesTypeName AS TypeName,ResourcesTypeImgUrl AS Icon,Remark  
                            FROM Tb_Resources_Type 
                            WHERE (isnull(CorpID,1000)=1000 OR CorpID=@CorpID) AND ParentID IS NULL 
                            AND ResourcesTypeID IN
                            (
                                SELECT ResourcesTypeID FROM Tb_Resources_Details
                                WHERE IsRelease='是' AND isnull(IsStopRelease,'否')='否' AND isnull(IsDelete,0)=0
                                AND BussId IN
                                (
                                    SELECT BussId FROM Tb_System_BusinessConfig WHERE CommunityId=@CommunityId
                                    UNION ALL
                                    SELECT BussId FROM Tb_System_BusinessCorp_Config WHERE CorpId=@CorpID
                                )
                             )  ORDER BY ResourcesTypeIndex";

                var data = conn.Query<PMSAppGoodsCategoryModel>(sql, new { CorpID = community.CorpID, CommunityId = community.Id }).ToList();
                data.ForEach(obj => obj.Icon = obj.Icon?.Trim(','));
                return new ApiResult(true, data).toJson();
            }
        }

        /// <summary>
        /// 获取商城首页推荐商品列表
        /// </summary>
        private string GetTopRecommendGoodsList(DataRow row)
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
                                   CASE WHEN IsRecommend='是' THEN 1 ELSE 0 END AS IsRecommend,RecommendSetDate  
                            FROM Tb_Resources_Details
                            WHERE IsRelease='是' AND isnull(IsStopRelease,'否')='否' AND IsRecommend='是' AND isnull(IsDelete,0)=0
                            AND BussId IN 
                            (
                                SELECT BussId FROM Tb_System_BusinessCorp
                                WHERE isnull(IsDelete,0)=0
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
        /// 获取商家店铺首页商品分类
        /// </summary>
        private string GetStoreGoodsCategory(DataRow row)
        {
            if (!row.Table.Columns.Contains("BussID") || string.IsNullOrEmpty(row["BussID"].ToString()))
            {
                return JSONHelper.FromString(false, "店铺id不能为空");
            }

            var bussId = AppGlobal.StrToInt(row["BussID"].AsString());

            using (var conn = new SqlConnection(PubConstant.BusinessContionString))
            {
                var sql = @"SELECT ResourcesTypeID AS TypeID,ResourcesTypeName AS TypeName,ResourcesTypeImgUrl AS Icon,Remark 
                            FROM Tb_Resources_Type
                            WHERE ResourcesTypeID IN
                            (
                                SELECT ResourcesTypeID FROM Tb_Resources_Details
                                WHERE BussId=@BussID AND IsRelease='是' AND isnull(IsStopRelease,'否')='否' AND isnull(IsDelete,0)=0
                            )
                            ORDER BY ResourcesTypeIndex";

                var data = conn.Query(sql, new { BussID = bussId }).ToList();
                data.ForEach(obj => obj.Icon = obj.Icon?.Trim(','));
                return new ApiResult(true, data).toJson();
            }
        }

        /// <summary>
        /// 获取商家店铺首页推荐商品列表
        /// </summary>
        private string GetStoreRecommendGoodsList(DataRow row)
        {
            if (!row.Table.Columns.Contains("BussID") || string.IsNullOrEmpty(row["BussID"].ToString()))
            {
                return JSONHelper.FromString(false, "店铺id不能为空");
            }

            var bussId = AppGlobal.StrToInt(row["BussID"].AsString());

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
                            WHERE BussID={bussId} AND IsRelease='是' AND isnull(IsStopRelease,'否')='否' AND IsRecommend='是' AND isnull(IsDelete,0)=0";

                var data = GetListDapper<PMSAppGoodsSimpleModel>(out int pageCount, out int count, sql, pageIndex, pageSize,
                    "RecommendSetDate", 1, "GoodsID", conn).ToList();
                data.ForEach(obj =>
                {
                    obj.GoodsImg = obj.GoodsImg?.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)[0];
                });
                return new ApiResult(true, data).toJson();
            }
        }

        /// <summary>
        /// 获取商家店铺首页商品分类下商品列表
        /// </summary>
        private string GetStoreCategoryGoodsList(DataRow row)
        {
            if (!row.Table.Columns.Contains("BussID") || string.IsNullOrEmpty(row["BussID"].ToString()))
            {
                return JSONHelper.FromString(false, "店铺id不能为空");
            }
            if (!row.Table.Columns.Contains("TypeID")||string.IsNullOrEmpty(row["TypeID"].AsString()))
            {
                return JSONHelper.FromString(false, "商品分类不能为空");
            }

            var bussId = AppGlobal.StrToInt(row["BussID"].AsString());
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
                                   CASE WHEN IsRecommend='是' THEN 1 ELSE 0 END AS IsRecommend,
                                   (
                                        SELECT count(1) FROM Tb_Charge_ReceiptDetail x
                                        LEFT JOIN Tb_Charge_Receipt y ON x.ReceiptCode=y.Id
                                        WHERE x.ResourcesID=a.ResourcesID AND isnull(y.IsDelete,0)=0 AND y.IsPay='已付款'
                                    ) AS SoldCount  
                            FROM Tb_Resources_Details a
                            WHERE BussID={bussId} AND ResourcesTypeID='{typeId}' 
                            AND IsRelease='是' AND isnull(IsStopRelease,'否')='否' AND isnull(IsDelete,0)=0";

                var data = GetListDapper<PMSAppGoodsSimpleModel>(out int pageCount, out int count, sql, pageIndex, pageSize, 
                    "CreateDate", 1, "GoodsID", conn).ToList();
                data.ForEach(obj =>
                {
                    obj.GoodsImg = obj.GoodsImg?.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)[0];
                });
                return new ApiResult(true, data).toJson();
            }
        }

        /// <summary>
        /// 获取商品分类下商品列表，不区分平台商城、物管商城、周边商家
        /// </summary>
        private string GetCategoryGoodsList(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编码不能为空");
            }
            if (!row.Table.Columns.Contains("TypeID") || string.IsNullOrEmpty(row["TypeID"].AsString()))
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
                                   CASE WHEN IsRecommend='是' THEN 1 ELSE 0 END AS IsRecommend,
                                   (
                                        SELECT count(1) FROM Tb_Charge_ReceiptDetail x
                                        LEFT JOIN Tb_Charge_Receipt y ON x.ReceiptCode=y.Id
                                        WHERE x.ResourcesID=a.ResourcesID AND isnull(y.IsDelete,0)=0 AND y.IsPay='已付款'
                                    ) AS SoldCount  
                            FROM Tb_Resources_Details a
                            WHERE ResourcesTypeID='{typeId}' AND IsRelease='是' AND isnull(IsStopRelease,'否')='否' AND isnull(IsDelete,0)=0
                            AND BussID IN
                            (
                                SELECT BussId FROM Tb_System_BusinessConfig WHERE CommunityId='{community.Id}'
                                UNION ALL 
                                SELECT BussId FROM Tb_System_BusinessCorp_Config WHERE CorpId={community.CorpID}
                            )";

                var data = GetListDapper<PMSAppGoodsSimpleModel>(out int pageCount, out int count, sql, pageIndex, pageSize, 
                    "CreateDate", 1, "GoodsID", conn).ToList();
                data.ForEach(obj =>
                {
                    obj.GoodsImg = obj.GoodsImg?.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)[0];
                });
                return new ApiResult(true, data).toJson();
            }
        }

        /// <summary>
        /// 获取推荐商品列表，不区分平台商城、物管商城、周边商家
        /// </summary>
        private string GetRecommendGoodsList(DataRow row)
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
                                   IsGroupBuy,ResourcesSalePrice AS SalePrice,ResourcesDisCountPrice AS DiscountPrice,
                                   CASE WHEN IsGroupBuy='是' AND GroupBuyStartDate<=getdate() AND GroupBuyEndDate>=getdate()
                                        THEN isnull(GroupBuyPrice,-1.0)
                                        ELSE ResourcesSalePrice-isnull(ResourcesDisCountPrice,0.0) END AS ActualPrice,
                                   CASE WHEN IsBp='是' THEN 1 ELSE 0 END AS IsHot,
                                   CASE WHEN IsRecommend='是' THEN 1 ELSE 0 END AS IsRecommend,RecommendSetDate  
                            FROM Tb_Resources_Details
                            WHERE IsRelease='是' AND isnull(IsStopRelease,'否')='否' AND IsRecommend='是' AND isnull(IsDelete,0)=0
                            AND BussId IN 
                            (
                                SELECT BussId FROM Tb_System_BusinessCorp
                                WHERE isnull(IsDelete,0)=0
                                AND BussId IN
                                (
                                    SELECT BussId FROM Tb_System_BusinessConfig WHERE CommunityId='{community.Id}'
                                    UNION ALL
                                    SELECT BussId FROM Tb_System_BusinessCorp_Config WHERE CorpId={community.CorpID}
                                )
                            )";

                var data = GetListDapper<PMSAppGoodsSimpleModel>(out int pageCount, out int count, sql, pageIndex, pageSize,
                    "RecommendSetDate", 1, "GoodsID", conn).ToList(); ;
                data.ForEach(obj =>
                {
                    obj.GoodsImg = obj.GoodsImg?.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)[0];
                });

                var json = new ApiResult(true, data).toJson();
                return json.Insert(json.Length-1, ",\"PageCount\":" + pageCount);
            }
        }

        /// <summary>
        /// 获取商家信息
        /// </summary>
        private string GetStoreInfo(DataRow row)
        {
            if (!row.Table.Columns.Contains("BussID") || string.IsNullOrEmpty(row["BussID"].ToString()))
            {
                return JSONHelper.FromString(false, "店铺id不能为空");
            }

            var bussId = AppGlobal.StrToInt(row["BussID"].AsString());

            using (var conn = new SqlConnection(PubConstant.BusinessContionString))
            {
                var sql = @"SELECT BussID,BussName FROM Tb_System_BusinessCorp WHERE BussID=@BussID AND isnull(IsDelete,0)=0";

                var data = conn.Query(sql, new { BussID = bussId }).FirstOrDefault();
                return new ApiResult(true, data).toJson();
            }
        }

        /// <summary>
        /// 特约服务商品列表
        /// </summary>
        private string GetServiceGoodsList(DataRow row)
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
                                   IsGroupBuy,ResourcesSalePrice AS SalePrice,ResourcesDisCountPrice AS DiscountPrice,
                                   CASE WHEN IsGroupBuy='是' AND GroupBuyStartDate<=getdate() AND GroupBuyEndDate>=getdate()
                                        THEN isnull(GroupBuyPrice,-1.0)
                                        ELSE ResourcesSalePrice-isnull(ResourcesDisCountPrice,0.0) END AS ActualPrice,
                                   CASE WHEN IsBp='是' THEN 1 ELSE 0 END AS IsHot,
                                   CASE WHEN IsRecommend='是' THEN 1 ELSE 0 END AS IsRecommend 
                            FROM Tb_Resources_Details
                            WHERE IsRelease='是' AND isnull(IsStopRelease,'否')='否' AND isnull(IsDelete,0)=0
                            AND BussID IN
                            (
                                SELECT BussId FROM Tb_System_BusinessConfig WHERE CommunityId='{community.Id}'
                                UNION ALL 
                                SELECT BussId FROM Tb_System_BusinessCorp_Config WHERE CorpId={community.CorpID}
                            )
                            AND ResourcesTypeID IN
                            (
                                SELECT ResourcesTypeID FROM Tb_Resources_Type
                                WHERE CorpID={community.CorpID} AND ResourcesTypeName LIKE '%特约服务%'
                            )";

                var data = GetListDapper<PMSAppGoodsSimpleModel>(out int pageCount, out int count, sql, pageIndex, pageSize, "GoodsID", 1, "GoodsID", conn).ToList();
                data.ForEach(obj =>
                {
                    obj.GoodsImg = obj.GoodsImg.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)[0];
                });

                var json = new ApiResult(true, data).toJson();
                return json.Insert(json.Length-1, ",\"PageCount\":" + pageCount);
            }
        }

        /// <summary>
        /// 增值服务商品类型
        /// </summary>
        private string GetValueAddedServiceGoodsList(DataRow row)
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
                                   IsGroupBuy,ResourcesSalePrice AS SalePrice,ResourcesDisCountPrice AS DiscountPrice,
                                   CASE WHEN IsGroupBuy='是' AND GroupBuyStartDate<=getdate() AND GroupBuyEndDate>=getdate()
                                        THEN isnull(GroupBuyPrice,-1.0)
                                        ELSE ResourcesSalePrice-isnull(ResourcesDisCountPrice,0.0) END AS ActualPrice,
                                   CASE WHEN IsBp='是' THEN 1 ELSE 0 END AS IsHot,
                                   CASE WHEN IsRecommend='是' THEN 1 ELSE 0 END AS IsRecommend 
                            FROM Tb_Resources_Details
                            WHERE IsRelease='是' AND isnull(IsStopRelease,'否')='否' AND isnull(IsDelete,0)=0
                            AND BussID IN
                            (
                                SELECT BussId FROM Tb_System_BusinessConfig WHERE CommunityId='{community.Id}'
                                UNION ALL 
                                SELECT BussId FROM Tb_System_BusinessCorp_Config WHERE CorpId={community.CorpID}
                            )
                            AND ResourcesTypeID IN
                            (
                                SELECT ResourcesTypeID FROM Tb_Resources_Type
                                WHERE CorpID={community.CorpID} AND ResourcesTypeName LIKE '%增值服务%'
                            )";

                var data = GetListDapper<PMSAppGoodsSimpleModel>(out int pageCount, out int count, sql, pageIndex, pageSize, "GoodsID", 1, "GoodsID", conn).ToList();
                data.ForEach(obj =>
                {
                    obj.GoodsImg = obj.GoodsImg.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)[0];
                });

                var json = new ApiResult(true, data).toJson();
                return json.Insert(json.Length-1, ",\"PageCount\":" + pageCount);
            }
        }
    }
}

