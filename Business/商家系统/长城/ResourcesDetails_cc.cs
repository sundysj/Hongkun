using Dapper;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using Model.Model.Buss;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Business
{
    public partial class ResourcesDetails
    {
        /// <summary>
        /// 根据商品分类获取商品列表
        /// </summary>
        public string GetGoodsWithResourcesType_cc(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return JSONHelper.FromString(false, "小区ID不能为空");
            }

            if (!row.Table.Columns.Contains("ResourcesTypeID") || string.IsNullOrEmpty(row["ResourcesTypeID"].ToString()))
            {
                return JSONHelper.FromString(false, "商品分类不能为空");
            }

            if (!row.Table.Columns.Contains("SecondType") || string.IsNullOrEmpty(row["SecondType"].ToString()))
            {
                return JSONHelper.FromString(false, "商品二级分类不能为空");
            }

            // 二级分类
            var communityId = row["CommunityID"].ToString();
            var resourcesType = row["ResourcesTypeID"].ToString();
            var secondType = row["SecondType"].ToString();

            int pageSize = 10;
            int pageIndex = 1;

            var sort = 0;
            var keywords = "";

            if (row.Table.Columns.Contains("PageSize"))
            {
                pageSize = AppGlobal.StrToInt(row["PageSize"].ToString());
            }
            if (row.Table.Columns.Contains("PageIndex"))
            {
                pageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());
            }
            if (row.Table.Columns.Contains("Sort"))
            {
                sort = AppGlobal.StrToInt(row["Sort"].ToString());
            }
            if (row.Table.Columns.Contains("Keywords"))
            {
                keywords = row["Keywords"].ToString();
            }

            var sql = "";

            // 所有商品
            if (resourcesType == "0")
            {
                sql = @" AND isnull(a.IsRelease,'否')='是' AND isnull(a.IsStopRelease,'否')='否' AND isnull(a.IsDelete,0)=0 
                        AND a.BussId IN
                        (
                            SELECT bb.BussId FROM Tb_System_BusinessCorp bb 
                            WHERE isnull(bb.IsDelete,0)=0   AND  isnull((bb.IsClose,'未关闭')='未关闭'
                            AND bb.BussId IN
                            (
                                SELECT cc.BussId FROM Tb_System_BusinessCorp_Config cc
                                    WHERE CorpId=(SELECT TOP 1 CorpId FROM Unified.dbo.Tb_Community WHERE id=@CommunityId)
                                UNION
                                SELECT dd.BussId FROM Tb_System_BusinessConfig dd WHERE dd.CommunityId=@CommunityId
                            )
                        )";
            }
            else
            {
                // 该一级分类下所有商品
                if (secondType == "0")
                {
                    sql = @" AND isnull(a.IsRelease,'否')='是' AND isnull(a.IsStopRelease,'否')='否' AND isnull(a.IsDelete,0)=0 
                            AND (a.ResourcesTypeID=@FirstResourcesType 
                                    OR a.ResourcesTypeID IN (SELECT ResourcesTypeID FROM Tb_Resources_Type WHERE ParentID=@FirstResourcesType))
                            AND a.BussId IN
                            (
                                SELECT bb.BussId FROM Tb_System_BusinessCorp bb 
                                WHERE isnull(bb.IsDelete,0)=0  AND  isnull(bb.IsClose,'未关闭')='未关闭'
                                AND bb.BussId IN
                                (
                                SELECT cc.BussId FROM Tb_System_BusinessCorp_Config cc
                                    WHERE CorpId=(SELECT TOP 1 CorpId FROM Unified.dbo.Tb_Community WHERE id=@CommunityId)
                                UNION
                                SELECT dd.BussId FROM Tb_System_BusinessConfig dd WHERE dd.CommunityId=@CommunityId
                                )
                            )";
                }
                else // 二级分类下商品
                {
                    sql = @" AND isnull(a.IsRelease,'否')='是' AND isnull(a.IsStopRelease,'否')='否' AND isnull(a.IsDelete,0)=0 
                            AND a.ResourcesTypeID=@SecendResourcesType AND isnull(a.IsServiceType,'否')='是'
                            AND a.BussId IN
                            (
                                SELECT bb.BussId FROM Tb_System_BusinessCorp bb 
                                WHERE isnull(bb.IsDelete,0)=0   AND  isnull(bb.IsClose,'未关闭')='未关闭'
                                AND bb.BussId IN
                                (
                                    SELECT cc.BussId FROM Tb_System_BusinessCorp_Config cc
                                    WHERE CorpId=(SELECT TOP 1 CorpId FROM Unified.dbo.Tb_Community WHERE id=@CommunityId)
                                    UNION
                                    SELECT dd.BussId FROM Tb_System_BusinessConfig dd WHERE dd.CommunityId=@CommunityId
                                )
                            )";
                }
            }

            var sortColumn = "RecommendSetDate";
            if (sort == 1)
                sortColumn = "SalesCount";
            else if (sort == 2)
                sortColumn = "CreateDate";

            if (keywords.Trim().Length > 0)
            {
                sql += $" AND a.ResourcesName LIKE @keywords";
            }

            // 俊发需求6953，查询是否是服务类商品
            sql = $@"SELECT * FROM 
                    (
                        SELECT *,ROW_NUMBER() OVER (ORDER BY t.{sortColumn} DESC) AS RID FROM 
                        (
                            SELECT a.ResourcesID,a.ResourcesName,a.ResourcesSimple,a.ResourcesSalePrice,a.ResourcesDisCountPrice,
                                a.IsGroupBuy,a.GroupBuyPrice,a.GroupBuyStartDate,a.GroupBuyEndDate,a.CreateDate,
                                a.IsBp,a.BpSetDate,a.IsRecommend,a.RecommendSetDate,a.IsSupportCoupon,a.MaximumCouponMoney,
                                (SELECT TOP 1 value FROM SplitString(a.Img,',',1)) AS Img,
                                isnull((
		                            SELECT sum(Quantity) FROM Tb_Charge_ReceiptDetail x 
		                            LEFT JOIN Tb_Charge_Receipt y ON x.ReceiptCode=y.Id 
		                            WHERE x.ResourcesID=a.ResourcesID AND y.IsPay='已付款'
	                            ),0) AS SalesCount, 
                                CASE b.ResourcesTypeName WHEN '生活服务' THEN 1 ELSE 0 END AS IsService 
                            FROM Tb_Resources_Details a
                            LEFT JOIN Tb_Resources_Type b ON a.ResourcesTypeID=b.ResourcesTypeID 
                            WHERE 1=1 {sql}
                        ) AS t
                    ) AS t
                    WHERE t.RID>(@PageIndex-1)*@PageSize AND t.RID<=(@PageIndex*@PageSize);

                    SELECT count(1) FROM Tb_Resources_Details a  
                    WHERE 1=1 {sql}";

            using (var conn = new SqlConnection(PubConstant.BusinessContionString))
            {
                var reader = conn.QueryMultiple(sql, new
                {
                    CommunityId = communityId,
                    FirstResourcesType = resourcesType,
                    SecendResourcesType = secondType,
                    keywords = $"%{keywords}%",
                    PageIndex = pageIndex,
                    PageSize = pageSize
                });

                var data = reader.Read();
                var pageCount = reader.Read<int>().FirstOrDefault();

                var json = new ApiResult(true, data).toJson();
                json = json.Insert(json.Length - 1, $",\"PageCount\":{pageCount}");

                return json;
            }
        }

        /// <summary>
        /// 获取生活首页Banner
        /// </summary>
        private string GetGoodHomeBannerList_cc(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return new ApiResult(false, "小区id不能为空").toJson();
            }

            var communityId = row["CommunityId"].ToString();

            using (IDbConnection con = new SqlConnection(PubConstant.UnifiedContionString))
            {
                var sql = $@"SELECT top 9 *FROM Tb_Notice WHERE EffectiveBegDate<=getdate() AND getdate()<=EffectiveEndDate
                            AND isnull(IsDelete,0)=0 AND NoticeType=3 AND isnull(IsTheShelves,0)=1 AND CommunityId like '%{communityId}%'
                            ORDER BY IssueDate DESC;";

                var data = con.Query(sql);

                return new ApiResult(true, data).toJson();
            }
        }

        /// <summary>
        /// 获取生活首页推荐商品，默认加载10个
        /// </summary>
        public string GetRecommendGoodsList_cc(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return JSONHelper.FromString(false, "小区ID不能为空");
            }
            if (!row.Table.Columns.Contains("PageIndex") || string.IsNullOrEmpty(row["PageIndex"].AsString()))
            {
                return new ApiResult(false, "缺少参数PageIndex").toJson();
            }



            var pageSize = 10;
            if (row.Table.Columns.Contains("PageSize") && !string.IsNullOrEmpty(row["PageSize"].AsString()))
            {
                pageSize = AppGlobal.StrToInt(row["PageSize"].ToString());
            }

            var pageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());
            var CommunityId = row["CommunityId"].ToString();

            using (IDbConnection con = new SqlConnection(PubConstant.BusinessContionString))
            {
                var sql = @"SELECT t.ResourcesID,t.BussId,t.ResourcesTypeID,t.ResourcesName,t.ResourcesSimple,
                                t.ResourcesPriceUnit,t.ResourcesSalePrice,t.ResourcesDisCountPrice,t.IsRecommend,t.CreateDate,
                                (SELECT TOP 1 Value FROM SplitString(t.Img,',',1)) AS Img 
                            FROM (
	                            SELECT row_number() OVER (ORDER BY a.RecommendSetDate DESC) AS RID,
	                            * FROM Tb_Resources_Details a
	                            WHERE BussId IN (SELECT AA.BussId FROM Tb_System_BusinessCorp_Config AA
					                            WHERE CorpId=(SELECT TOP 1 CorpId FROM Unified.dbo.Tb_Community WHERE ID=@CommunityId)
					                            )
	                            AND isnull(IsRelease,'否')='是' AND isnull(IsStopRelease,'否')='否'
	                            AND isnull(IsDelete,0)=0 AND isnull(IsRecommend, '否')='是'
                            ) AS t
                            WHERE t.RID>(@PageIndex-1)*@PageSize AND t.RID<=(@PageIndex*@PageSize);";

                var data = con.Query(sql, new
                {
                    CommunityId = CommunityId,
                    PageIndex = pageIndex,
                    PageSize = pageSize
                });

                return new ApiResult(true, data).toJson();
            }
        }

        /// <summary>
        /// 获取商品二级及商品列表，默认只返回前4个
        /// </summary>
        private string GetSecondCategoriesGoodsList_cc(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].AsString()))
            {
                return new ApiResult(false, "缺少参数CommunityId").toJson();
            }
            if (!row.Table.Columns.Contains("ResourcesTypeID") || string.IsNullOrEmpty(row["ResourcesTypeID"].ToString()))
            {
                return new ApiResult(false, "商品分类不能为空").toJson();
            }

            var resourcesTypeID = row["ResourcesTypeID"].ToString();
            var communityId = row["CommunityId"].AsString();
            var community = GetCommunity(communityId);
            if (community == null)
            {
                return JSONHelper.FromString(false, "未查询到小区信息");
            }
            using (var conn = new SqlConnection(PubConstant.BusinessContionString))
            {

                var sql = $@"SELECT ResourcesTypeID,ResourcesTypeName,
                                (SELECT TOP 1 value FROM SplitString(isnull(ResourcesTypeImgUrl,''),',',1)) AS ResourcesTypeImgUrl  
                            FROM Tb_Resources_Type WHERE ResourcesTypeID IN
                            (
                                SELECT DISTINCT A.ResourcesTypeID FROM Tb_Resources_Details A
                                WHERE isnull(A.IsDelete,0)=0 AND isnull(A.IsRelease,'否')='是' AND isnull(A.IsStopRelease,'否')='否' AND isnull(A.IsServiceType,'否')='否' 
                                AND A.BussId IN (SELECT AA.BussId FROM Tb_System_BusinessCorp_Config AA
                                                WHERE CorpId=(SELECT TOP 1 CorpId FROM Unified.dbo.Tb_Community WHERE id=@CommunityId)
                                                UNION
                                                SELECT bb.BussId FROM Tb_System_BusinessConfig bb WHERE bb.CommunityId=@CommunityId
                                                UNION   SELECT BussId FROM Tb_System_BusinessCorp WHERE BussNature='平台商城' AND  isnull(IsClose,'未关闭')='未关闭' and ISNULL(IsDelete,0)=0 
                                            )
                            ) AND isnull(IsDelete,0)=0 AND ParentID=@ResourcesTypeID;

                            /* 加载只有二级分类下的商品 */       
                            select * from 
                            (SELECT *, ROW_NUMBER() over(partition by ResourcesTypeID order by IsRecommend ,CreateDate  desc) rowNum FROM 
                            (
                            SELECT * FROM (SELECT  a.ResourcesID,a.BussId,a.ResourcesTypeID,a.ResourcesName,a.ResourcesSimple,IsSpecification=0,SpecificationPrice=0 ,Seckill=0,
                                                            a.ResourcesPriceUnit,a.ResourcesSalePrice,a.ResourcesDisCountPrice,a.IsRecommend,a.CreateDate,a.IsGroupBuy,a.GroupBuyStartDate,a.GroupBuyEndDate,
                                                            (SELECT TOP 1 Value FROM SplitString(a.Img,',',1)) AS Img 
                                                        FROM Tb_Resources_Details a WHERE ResourcesTypeID IN
                                                        (
	                                                        SELECT ResourcesTypeID
	                                                        FROM Tb_Resources_Type WHERE ResourcesTypeID IN
	                                                        (
		                                                        SELECT DISTINCT A.ResourcesTypeID FROM Tb_Resources_Details A
			                                                    WHERE isnull(A.IsDelete,0)=0 AND isnull(A.IsRelease,'否')='是' AND isnull(A.IsStopRelease,'否')='否'
			                                                    AND A.BussId IN (SELECT AA.BussId FROM Tb_System_BusinessCorp_Config AA
                                                                                 LEFT JOIN Tb_System_BusinessCorp AS B  ON AA.BussId=B.BussId
							                                                     WHERE CorpId=(SELECT TOP 1 CorpId FROM Unified.dbo.Tb_Community WHERE id=@CommunityId) AND   isnull(b.IsClose,'未关闭')='未关闭'
							                                                     UNION
							                                                     SELECT bb.BussId FROM Tb_System_BusinessConfig bb WHERE bb.CommunityId=@CommunityId
                                                                                UNION   SELECT BussId FROM Tb_System_BusinessCorp WHERE BussNature='平台商城' AND  isnull(IsClose,'未关闭')='未关闭' and ISNULL(IsDelete,0)=0 
                                                            )
	                                                        ) AND isnull(IsDelete,0)=0 AND ParentID=@ResourcesTypeID
                                                        ) 
                                                        AND isnull(A.IsDelete,0)=0 AND isnull(A.IsRelease,'否')='是' AND isnull(A.IsStopRelease,'否')='否'   AND isnull(A.IsServiceType,'否')='否' )AS M
                            )AS N) as s where s.rowNum <= 6;;
                            ";

                var reader = conn.QueryMultiple(sql, new
                {
                    CorpID = community.CorpID,
                    CommunityId = communityId,
                    ResourcesTypeID = resourcesTypeID
                });
                var secondType = reader.Read();
                var goodsList = reader.Read().ToList();

                var list = new List<dynamic>();

                if (goodsList.Any())
                {
                    sql = @"   SELECT * FROM
                         (SELECT ROW_NUMBER() OVER(PARTITION  BY ResourcesID ORDER BY Price ASC) AS Number,* FROM Tb_ResourcesSpecificationsPrice  WHERE   ResourcesID IN @ResourcesID)AS C
                         WHERE C.Number < 2 ";
                    var specification = conn.Query(sql, new { ResourcesID = goodsList.Select(l => l.ResourcesID).ToList() });

                    String isGroupBuy = @" SELECT * FROM
                 (SELECT ROW_NUMBER() OVER(PARTITION  BY ResourcesID ORDER BY GroupBuyingPrice ASC) AS Number,* FROM Tb_ResourcesSpecificationsPrice  WHERE   ResourcesID IN @ResourcesID)AS C
                 WHERE C.Number < 2";
                    var isGroupBuyInfo = conn.Query(isGroupBuy, new { ResourcesID = goodsList.Select(l => l.ResourcesID).ToList() });

                    foreach (var model in goodsList)
                    {
                        var isContanis = specification.FirstOrDefault(l => l.ResourcesID == model.ResourcesID);
                        if (isContanis != null)
                        {
                            model.IsSpecification = (object)1;
                            model.SpecificationPrice = isContanis.Price;
                            model.ResourcesDisCountPrice = isContanis.DiscountAmount;
                            model.GroupBuyPrice = (object)isContanis.GroupBuyingPrice;
                        }

                        var IsGroupBuy = (String)model.IsGroupBuy;
                        var groupBuyStartDate = (DateTime?)model.GroupBuyStartDate;
                        var groupBuyEndDate = (DateTime?)model.GroupBuyEndDate;
                        var isContanisGroup = isGroupBuyInfo.FirstOrDefault(l => l.ResourcesID == model.ResourcesID);
                        if (IsGroupBuy == "是"
                            && groupBuyStartDate.HasValue
                            && groupBuyStartDate.Value <= DateTime.Now
                            && groupBuyEndDate.HasValue
                            && groupBuyEndDate.Value >= DateTime.Now
                            && isContanisGroup != null)
                        {
                            model.Seckill = (object)1;
                            model.ResourcesSalePrice = (object)isContanisGroup.Price;
                            model.GroupBuyPrice = (object)isContanisGroup.GroupBuyingPrice;
                            model.ResourcesDisCountPrice = (object)isContanisGroup.DiscountAmount;
                        }
                    }
                }

                foreach (dynamic item in secondType)
                {
                    var tmp = goodsList.FindAll(obj => obj.ResourcesTypeID == item.ResourcesTypeID);
                    if (tmp.Count() > 0)
                    {
                        list.Add(new
                        {
                            ResourcesTypeID = item.ResourcesTypeID,
                            ResourcesTypeName = item.ResourcesTypeName,
                            GoodsList = tmp
                        });
                    }
                }
                return new ApiResult(true, list).toJson();
            }
        }

        /// <summary>
        /// 获取商品详情
        /// </summary>
        private string GetGoodsDetail_cc(DataRow row)
        {
            if (!row.Table.Columns.Contains("ResourcesID") || string.IsNullOrEmpty(row["ResourcesID"].ToString()))
            {
                return new ApiResult(false, "商品分类不能为空").toJson();
            }
            if (!row.Table.Columns.Contains("UserId") || string.IsNullOrEmpty(row["UserId"].ToString()))
            {
                return new ApiResult(false, "UserId不能为空").toJson();
            }

            var resourcesID = row["ResourcesID"].ToString();
            var userId = row["UserId"].ToString();
            using (var conn = new SqlConnection(PubConstant.BusinessContionString))
            {
                var sql = @"
                        /* 商品详情*/
                        SELECT a.*,b.Express,c.BussMobileTel,c.BussWorkedTel,c.BussLinkMan,c.BussName,
                            A.ActualSalesVolumes + A.FictitiousSalesVolumes  as SaleCount,
                            (SELECT COUNT(1) FROM Tb_Resources_CommodityEvaluation 
                             WHERE ResourcesID=@ResourcesID) AS EvaluationCount,
                            (SELECT COUNT(1) FROM Tb_Collection 
                             WHERE ResourcesID=@ResourcesID AND UserId=@UserId) AS IsCollection
                        FROM Tb_Resources_Details a 
                        LEFT JOIN Tb_Charge_Receipt b ON a.BussId=b.BussId
                        LEFT JOIN Tb_System_BusinessCorp c ON a.BussId=c.BussId
                        WHERE a.ResourcesID=@ResourcesID
                        AND isnull(a.IsDelete,0)=0 AND isnull(a.IsRelease,'否')='是' AND isnull(a.IsStopRelease,'否')='否' 
                        AND isnull(c.IsClose,'未关闭')='未关闭' AND isnull(c.IsDelete,0)=0

                        /* 商品属性 */
                        SELECT x.*, y.PropertyName FROM Tb_Resources_PropertyRelation x
                        LEFT JOIN Tb_Resources_Property y ON x.PropertyId=y.Id
                        WHERE isnull(y.IsDelete,0)=0 AND x.ResourcesID=@ResourcesID;
                        
                        /* 商品属性下的规格 */
                        SELECT x.Id,x.SpecName,z.Price AS SpecPrice,Z.GroupBuyingPrice,Z.DiscountAmount,Z.Inventory,y.PropertyId ,Z.Img
                        FROM Tb_Resources_Specifications x
                        LEFT JOIN Tb_Resources_PropertyRelation y ON x.PropertyId=y.PropertyId
                        LEFT JOIN Tb_ResourcesSpecificationsPrice z ON x.Id=z.SpecId
                        WHERE y.ResourcesID=@ResourcesID AND  Z.ResourcesID=@ResourcesID 
                        ORDER BY x.Sort;";

                var reader = conn.QueryMultiple(sql, new
                {
                    ResourcesID = resourcesID,
                    UserId = userId
                });

                var data = reader.Read().FirstOrDefault();
                var propertysList = reader.Read().ToList();
                var specsList = reader.Read().ToList();

                if (data == null)
                {
                    return new ApiResult(false, "抱歉，未查询到商品信息").toJson();
                }

                var propertysListNew = new List<dynamic>();

                foreach (dynamic item in propertysList)
                {
                    var tmp = specsList.FindAll(obj => obj.PropertyId == item.PropertyId)
                        .Select(obj => new
                        {
                            SpecId = obj.Id,
                            SpecName = obj.SpecName,
                            SpecPrice = obj.SpecPrice,
                            Inventory = obj.Inventory,
                            SpecGroupBuyingPirce = obj.GroupBuyingPrice,
                            SpecDiscountAmount = obj.DiscountAmount,
                            Img = obj.Img,
                        });

                    // 如果属性下有规格，才会返回
                    if (tmp.Count() > 0)
                    {
                        item.SpecsList = tmp;
                        propertysListNew.Add(item);
                    }
                }
                data.PropertysList = propertysListNew;

                return new ApiResult(true, data).toJson();
            }
        }


        /// <summary>
        /// 获取商品的评价信息列表
        /// </summary>
        private string GetGoodEvaluationList_cc(DataRow row)
        {
            if (!row.Table.Columns.Contains("ResourcesID") || string.IsNullOrEmpty(row["ResourcesID"].ToString()))
            {
                return new ApiResult(false, "商品分类不能为空").toJson();
            }
            if (!row.Table.Columns.Contains("PageIndex") || string.IsNullOrEmpty(row["PageIndex"].AsString()))
            {
                return new ApiResult(false, "缺少参数PageIndex").toJson();
            }
            var pageSize = 10;
            if (row.Table.Columns.Contains("PageSize") && !string.IsNullOrEmpty(row["PageSize"].AsString()))
            {
                pageSize = AppGlobal.StrToInt(row["PageSize"].ToString());
            }

            var resourcesID = row["ResourcesID"].ToString();
            var pageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());

            using (IDbConnection con = new SqlConnection(PubConstant.BusinessContionString))
            {
                var sql = @"SELECT * FROM
                            (
                                SELECT row_number() OVER (ORDER BY a.EvaluateDate DESC) AS RID,
                                    a.Id,a.EvaluateContent,a.UploadImg,a.Star,a.EvaluateDate,
                                    d.PropertyName,e.SpecName,b.Quantity,ISNULL(g.NickName,a.Name) as NickName ,ISNULL(g.UserPic,A.HeadPortraitUrl)  as UserPic
                                FROM Tb_Resources_CommodityEvaluation a
                                LEFT JOIN Tb_Charge_ReceiptDetail b ON a.RpdCode=b.RpdCode
                                LEFT JOIN Tb_ShoppingDetailed c ON b.ShoppingId=c.ShoppingId
                                LEFT JOIN Tb_Resources_Property d ON c.PropertysId=d.Id
                                LEFT JOIN Tb_Resources_Specifications e ON c.SpecId=e.Id
                                LEFT JOIN Tb_Charge_Receipt f ON b.ReceiptCode=f.Id
                                LEFT JOIN Unified.dbo.Tb_User g ON f.UserId=g.Id
                                WHERE a.ResourcesID=@ResourcesID AND isnull(a.IsDelete,0)=0 AND ISNULL( A.IsShow,'是')='是'
                            ) AS t
                            WHERE t.RID>(@PageIndex-1)*@PageSize AND t.RID<=(@PageIndex*@PageSize);";

                var data = con.Query(sql, new
                {
                    ResourcesID = resourcesID,
                    PageIndex = pageIndex,
                    PageSize = pageSize
                });

                return new ApiResult(true, data).toJson();
            }
        }


        /// <summary>
        /// 新增/修改收货地址
        /// </summary>
        private string SetUserAddress_cc(DataRow row)
        {
            if (!row.Table.Columns.Contains("UserId") || string.IsNullOrEmpty(row["UserId"].ToString()))
            {
                return JSONHelper.FromString(false, "用户编码不能为空");
            }
            if (!row.Table.Columns.Contains("UserName") || string.IsNullOrEmpty(row["UserName"].ToString()))
            {
                return JSONHelper.FromString(false, "收货人不能为空");
            }
            if (!row.Table.Columns.Contains("Mobile") || string.IsNullOrEmpty(row["Mobile"].ToString()))
            {
                return JSONHelper.FromString(false, "联系电话不能为空");
            }
            if (!row.Table.Columns.Contains("Province") || string.IsNullOrEmpty(row["Province"].ToString()))
            {
                return JSONHelper.FromString(false, "所在地区不能为空");
            }
            if (!row.Table.Columns.Contains("Address") || string.IsNullOrEmpty(row["Address"].ToString()))
            {
                return JSONHelper.FromString(false, "详细地址不能为空");
            }


            var addressId = @"";
            if (row.Table.Columns.Contains("AddressId") && !string.IsNullOrEmpty(row["AddressId"].ToString()))
            {
                addressId = row["AddressId"].ToString();
            }

            var userId = row["UserId"].ToString();
            var userName = row["UserName"].ToString();
            var mobile = row["Mobile"].ToString();
            var province = row["Province"].ToString();
            var address = row["Address"].ToString();
            var isDefault = 0;

            if (row.Table.Columns.Contains("IsDefault") && !string.IsNullOrEmpty(row["IsDefault"].ToString()))
            {
                isDefault = AppGlobal.StrToInt(row["IsDefault"].ToString());
            }

            using (var conn = new SqlConnection(PubConstant.BusinessContionString))
            {
                conn.Open();
                var trans = conn.BeginTransaction();
                try
                {
                    var sql = @"";
                    // 设置了当前为默认收货地址
                    if (isDefault == 1)
                    {
                        sql = @"UPDATE Tb_User_Address SET IsDefault=0 WHERE UserId=@UserId";

                        var result = conn.Execute(sql, new
                        {
                            UserId = userId
                        }, trans);
                    }

                    // 添加地址
                    sql = @"INSERT INTO Tb_User_Address 
                            (Id, BussId, UserId, Address, Mobile, UpdataTime, IsDefault, UserName, Province)
                            VALUES 
                            (@AddressId, 0, @UserId, @Address, @Mobile, @UpdataTime, @IsDefault, @UserName, @Province)";

                    // 更新地址
                    if (!string.IsNullOrEmpty(addressId))
                    {
                        sql = @"UPDATE Tb_User_Address 
                            SET Address=@Address, Mobile=@Mobile, UpdataTime=@UpdataTime, IsDefault=@IsDefault, 
                            UserName=@UserName, Province=@Province
                            WHERE Id=@AddressId AND UserId=@UserId";
                    }

                    conn.Execute(sql, new
                    {
                        AddressId = addressId.Length > 0 ? addressId : Guid.NewGuid().ToString(),
                        UserId = userId,
                        Address = address,
                        Mobile = mobile,
                        UpdataTime = DateTime.Now,
                        IsDefault = isDefault,
                        UserName = userName,
                        Province = province
                    }, trans);

                    trans.Commit();

                    return JSONHelper.FromString(true, "保存成功");
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    return JSONHelper.FromString(false, ex.Message);
                }
            }
        }


        /// <summary>
        /// 获取所有收货地址
        /// </summary>
        private string GetUserAddress_cc(DataRow row)
        {
            if (!row.Table.Columns.Contains("UserId") || string.IsNullOrEmpty(row["UserId"].ToString()))
            {
                return JSONHelper.FromString(false, "用户编码不能为空");
            }

            var userId = row["UserId"].ToString();

            using (var conn = new SqlConnection(PubConstant.BusinessContionString))
            {
                var sql = @"SELECT * FROM Tb_User_Address WHERE UserId=@UserId ORDER BY IsDefault DESC,UpdataTime DESC";

                var data = conn.Query(sql, new { UserId = userId });

                return new ApiResult(true, data).toJson();
            }
        }




        /// <summary>
        /// 获取默认地址
        /// </summary>
        private string GetUserDefaultAddress(DataRow row)
        {
            if (!row.Table.Columns.Contains("UserId") || string.IsNullOrEmpty(row["UserId"].ToString()))
            {
                return JSONHelper.FromString(false, "用户编码不能为空");
            }
            if (!row.Table.Columns.Contains("RoomId") || string.IsNullOrEmpty(row["RoomId"].ToString()))
            {
                return JSONHelper.FromString(false, "房间编码不能为空");
            }
            var userId = row["UserId"].ToString();
            var roomId = row["RoomId"].ToString();
            using (var conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                var sql = @"SELECT A.Name,A.Mobile,B.RoomSign,B.CustName,C.Province,C.Area,C.City,C.CommName FROM 
                              (
                              SELECT * FROM Tb_User WHERE Id= @UserId
                              ) AS A
                              LEFT JOIN 
                              (
                              SELECT * FROM Tb_User_Relation  WHERE  ISNULL( CustHoldId ,0)=0 AND RoomId=@RoomId
                              ) AS B ON B.UserId= A.Id
                              LEFT JOIN 
                              (
                              SELECT * FROM Tb_Community 
                              ) AS C ON C.ID=B.CommunityId
                              WHERE C.Id IS NOT NULL AND B.Id IS NOT NULL AND A.Id IS NOT NULL                                
                              ";

                var data = conn.QueryFirstOrDefault(sql, new { UserId = userId, RoomId = roomId });
                if (null == data) return JSONHelper.FromString(false, "获取失败");
                var result = new
                {
                    UserName = data.CustName,
                    Mobile = data.Mobile,
                    Province = String.Format("{0}{1}{2}", data.Province, data.City, data.Area),
                    City = data.City,
                    Area = data.Area,
                    Address = String.Format("{0}{1}", data.CommName, data.RoomSign)
                };
                return new ApiResult(true, result).toJson();
            }
        }




        /// <summary>
        /// 获取频道列表
        /// </summary>
        private string GetShopChannelList_cc(DataRow row)
        {

            if (!row.Table.Columns.Contains("UserId") || string.IsNullOrEmpty(row["UserId"].ToString()))
            {
                return JSONHelper.FromString(false, "用户Id不能为空");
            }

            using (IDbConnection con = new SqlConnection(PubConstant.BusinessContionString))
            {
                var sql = @"SELECT *FROM Tb_Resources_Channel 
                            WHERE isnull(isdelete,0)=0 AND isnull(isTheshelves,0)=1
                            ORDER BY Sorting ASC";

                var data = con.Query(sql);

                return new ApiResult(true, data).toJson();
            }
        }


        /// <summary>
        /// 获取频道商品列表
        /// </summary>
        public string GetShopChannelGoodsList_cc(DataRow row)
        {
            if (!row.Table.Columns.Contains("ChannelID") || string.IsNullOrEmpty(row["ChannelID"].ToString()))
            {
                return JSONHelper.FromString(false, "频道Id不能为空");
            }
            if (!row.Table.Columns.Contains("PageIndex") || string.IsNullOrEmpty(row["PageIndex"].AsString()))
            {
                return new ApiResult(false, "缺少参数PageIndex").toJson();
            }
            var keyWord = @"";
            if (row.Table.Columns.Contains("KeyWord") && !string.IsNullOrEmpty(row["KeyWord"].ToString()))
            {
                keyWord = row[@"KeyWord"].ToString();
            }
            var pageSize = 10;
            if (row.Table.Columns.Contains("PageSize") && !string.IsNullOrEmpty(row["PageSize"].AsString()))
            {
                pageSize = AppGlobal.StrToInt(row["PageSize"].ToString());
            }
            var filterValue = 0;
            if (row.Table.Columns.Contains("FilterValue") && !string.IsNullOrEmpty(row["FilterValue"].AsString()))
            {
                filterValue = AppGlobal.StrToInt(row["FilterValue"].ToString());
            }
            var sortWay = "a.Sorting ASC";
            switch (filterValue)
            {
                case 0:// sort排序
                    sortWay = "a.Sorting ASC";
                    break;
                case 1:// 价格升序
                    sortWay = "(b.ResourcesSalePrice-b.ResourcesDisCountPrice) ASC";
                    break;
                case 2:// 价格降序
                    sortWay = "(b.ResourcesSalePrice-b.ResourcesDisCountPrice) DESC";
                    break;
                case 3:// 销量降序
                    sortWay = @"isnull((
                                SELECT count(Quantity) FROM Tb_Charge_ReceiptDetail x
                                LEFT JOIN Tb_Charge_Receipt y ON x.ReceiptCode = y.Id
                                WHERE x.ResourcesID = b.ResourcesID AND y.IsPay = '已付款'
	                            ),0) DESC";
                    break;
            }

            var pageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());
            var channelID = row["ChannelID"].ToString();

            using (IDbConnection con = new SqlConnection(PubConstant.BusinessContionString))
            {
                var sql = $@"SELECT *,IsSpecification=0,SpecificationPrice=0,Seckill=0  FROM 
                            (
	                            SELECT 
	                            row_number() OVER (ORDER BY {sortWay}) AS RID,
                                isnull((
		                            SELECT count(Quantity) FROM Tb_Charge_ReceiptDetail x 
		                            LEFT JOIN Tb_Charge_Receipt y ON x.ReceiptCode=y.Id 
		                            WHERE x.ResourcesID=b.ResourcesID AND y.IsPay='已付款'
		                        ),0) AS SaleCount,
	                            b.ResourcesID,b.BussId,b.ResourcesTypeID,b.ResourcesName,b.ResourcesSimple,b.IsGroupBuy,b.GroupBuyStartDate,b.GroupBuyEndDate,
		                        b.ResourcesPriceUnit,b.ResourcesSalePrice,b.GroupBuyPrice,b.ResourcesDisCountPrice,b.IsRecommend,b.CreateDate,
		                        (SELECT TOP 1 Value FROM SplitString(b.Img,',',1)) AS Img  
	                            FROM Tb_Resources_ChannelDetails a
	                            LEFT JOIN Tb_Resources_Details b ON a.ResourcesID=b.ResourcesID
	                            WHERE isnull(a.IsDelete,0)=0 AND isnull(b.IsDelete,0)=0 AND isnull(b.IsStopRelease,'否')='否' AND isnull(b.IsRelease,'否')='是'
                                AND a.ChannelID=@ChannelID AND b.ResourcesName LIKE '%{keyWord}%'
                            ) AS t
                            WHERE  t.RID>(@PageIndex-1)*@PageSize AND t.RID<=(@PageIndex*@PageSize)";

                var data = con.Query(sql, new
                {
                    ChannelID = channelID,
                    PageIndex = pageIndex,
                    PageSize = pageSize
                });

                if (data.Any())
                {
                    //sql = @" SELECT ResourcesID,MIN(Price) as  Price FROM View_Tb_ResourcesSpecificationsPrice_Filter WHERE ISNULL(ISDELETE,0)=0 AND  SpecId IS NOT NULL  AND ResourcesID IN @ResourcesID group by ResourcesID ";
                    //var specification = con.Query(sql, new { ResourcesID = data.Select(l => l.ResourcesID).ToList() });

                    //foreach (var model in data)
                    //{
                    //    var isContanis = specification.FirstOrDefault(l => l.ResourcesID == model.ResourcesID);
                    //    if (isContanis != null)
                    //    {
                    //        model.IsSpecification = (object)1;
                    //        model.SpecificationPrice = isContanis.Price;
                    //    }
                    //}
                    sql = @"   SELECT * FROM
                         (SELECT ROW_NUMBER() OVER(PARTITION  BY ResourcesID ORDER BY Price ASC) AS Number,* FROM Tb_ResourcesSpecificationsPrice  WHERE   ResourcesID IN @ResourcesID)AS C
                         WHERE C.Number < 2 ";
                    var specification = con.Query(sql, new { ResourcesID = data.Select(l => l.ResourcesID).ToList() });

                    String isGroupBuy = @" SELECT * FROM
                 (SELECT ROW_NUMBER() OVER(PARTITION  BY ResourcesID ORDER BY GroupBuyingPrice ASC) AS Number,* FROM Tb_ResourcesSpecificationsPrice  WHERE   ResourcesID IN @ResourcesID)AS C
                 WHERE C.Number < 2";
                    var isGroupBuyInfo = con.Query(isGroupBuy, new { ResourcesID = data.Select(l => l.ResourcesID).ToList() });

                    foreach (var model in data)
                    {
                        var isContanis = specification.FirstOrDefault(l => l.ResourcesID == model.ResourcesID);
                        if (isContanis != null)
                        {
                            model.IsSpecification = (object)1;
                            model.SpecificationPrice = isContanis.Price;
                            model.ResourcesDisCountPrice = isContanis.DiscountAmount;
                            model.GroupBuyPrice = (object)isContanis.GroupBuyingPrice;
                        }

                        var IsGroupBuy = (String)model.IsGroupBuy;
                        var groupBuyStartDate = (DateTime?)model.GroupBuyStartDate;
                        var groupBuyEndDate = (DateTime?)model.GroupBuyEndDate;
                        var isContanisGroup = isGroupBuyInfo.FirstOrDefault(l => l.ResourcesID == model.ResourcesID);
                        if (IsGroupBuy == "是"
                            && groupBuyStartDate.HasValue
                            && groupBuyStartDate.Value <= DateTime.Now
                            && groupBuyEndDate.HasValue
                            && groupBuyEndDate.Value >= DateTime.Now
                            && isContanisGroup != null)
                        {
                            model.Seckill = (object)1;
                            model.ResourcesSalePrice = (object)isContanisGroup.Price;
                            model.GroupBuyPrice = (object)isContanisGroup.GroupBuyingPrice;
                            model.ResourcesDisCountPrice = (object)isContanisGroup.DiscountAmount;
                        }
                    }
                }

                return new ApiResult(true, data).toJson();
            }
        }

        /// <summary>
        /// 加入购物车  
        /// </summary>
        private string AddGoodsToShopCar_cc(DataRow row)
        {
            if (!row.Table.Columns.Contains("UserId") || string.IsNullOrEmpty(row["UserId"].ToString()))
            {
                return JSONHelper.FromString(false, "用户编码不能为空");
            }
            if (!row.Table.Columns.Contains("ResourcesID") || string.IsNullOrEmpty(row["ResourcesID"].ToString()))
            {
                return JSONHelper.FromString(false, "商品编码不能为空");
            }
            if (!row.Table.Columns.Contains("BussId") || string.IsNullOrEmpty(row["BussId"].ToString()))
            {
                return JSONHelper.FromString(false, "商家编码不能为空");
            }

            int? corpId = null;
            if (row.Table.Columns.Contains("CorpId") && !string.IsNullOrEmpty(row["CorpId"].ToString()))
            {
                corpId = AppGlobal.StrToInt(row["CorpId"].ToString());
            }

            var userId = row["UserId"].ToString();
            var bussId = row["BussId"].ToString();
            var resourcesID = row["ResourcesID"].ToString();
            var propertysId = "";
            var specId = "";
            if (row.Table.Columns.Contains("PropertysId") && !string.IsNullOrEmpty(row["PropertysId"].ToString()))
            {
                propertysId = row["PropertysId"].ToString();
            }
            if (row.Table.Columns.Contains("SpecId") && !string.IsNullOrEmpty(row["SpecId"].ToString()))
            {
                specId = row["SpecId"].ToString();
            }

            int number = 1;//加入购物车数量，默认1
            if (row.Table.Columns.Contains("Number") && AppGlobal.StrToInt(row["Number"].ToString()) > 0)
            {
                number = AppGlobal.StrToInt(row["Number"].ToString());
            }

            using (var con = new SqlConnection(PubConstant.BusinessContionString))
            {
                con.Open();
                var trans = con.BeginTransaction();

                try
                {
                    String businessStatusExist = @"SELECT isnull(col_length('Tb_System_BusinessCorp', 'BusinessStatus'),0)";
                    if (con.QueryFirstOrDefault<long>(businessStatusExist, null, trans) > 0)
                    {
                        String sqlStrBusinessStatus = @"SELECT ISNULL(BusinessStatus,1) as BusinessStatus FROM Tb_System_BusinessCorp where BussId=@BussId";
                        var businessStatus = con.QueryFirstOrDefault<int>(sqlStrBusinessStatus, new { BussId = bussId }, trans);
                        if (businessStatus != 1)
                        {
                            return new ApiResult(false, "该商家暂停营业，敬请期待").toJson();
                        }
                    }

                    //现在判断库存 修改为判断规格库存
                    String sql = @"SELECT ISNULL(Inventory,0) AS Inventory FROM Tb_ResourcesSpecificationsPrice WHERE ResourcesID=@ResourcesID AND PropertyId=@PropertyId AND SpecId=@SpecId;";
                    var inventory = con.QueryFirstOrDefault<decimal>(sql, new { ResourcesID = resourcesID, PropertyId = propertysId, SpecId = specId }, trans);
                    if (number > inventory) return JSONHelper.FromString(false, "抱歉，库存不足");

                    sql = @"SELECT * FROM Tb_ShoppingCar 
                                WHERE UserId=@UserId AND ResourcesID=@ResourcesID AND BussId=@BussId AND isnull(IsDelete,0)=0";

                    var data = con.Query(sql, new { UserId = userId, ResourcesID = resourcesID, BussId = bussId }, trans);

                    foreach (dynamic item in data)
                    {
                        sql = @"
                                IF @PropertysId IS NOT NULL AND len(@PropertysId)<>0 
                                   AND @SpecId IS NOT NULL AND len(@SpecId)<>0
                                    SELECT count(1) as Count FROM Tb_ShoppingDetailed 
                                    WHERE ShoppingId=@Id AND PropertysId=@PropertysId AND SpecId=@SpecId
                                ELSE
                                    SELECT count(1) as Count FROM Tb_ShoppingCar 
                                    WHERE UserId=@UserId AND ResourcesID=@ResourcesID AND BussId=@BussId 
                                    AND isnull(IsDelete,0)=0";

                        var count = con.Query<int>(sql, new
                        {
                            Id = item.Id,
                            PropertysId = propertysId,
                            SpecId = specId,
                            UserId = userId,
                            ResourcesID = resourcesID,
                            BussId = bussId
                        }, trans).FirstOrDefault();

                        // 包含
                        if (count > 0)
                        {
                            sql = @"UPDATE Tb_ShoppingCar set Number=Number+@Number WHERE Id=@Id";
                            con.Execute(sql, new { Number = number, Id = item.Id }, trans);

                            trans.Commit();

                            return new ApiResult(true, @"添加成功").toJson();
                        }
                    }

                    var shopCarId = Guid.NewGuid().ToString();
                    sql = @"INSERT INTO Tb_ShoppingCar (Id,UserId,CorpId,BussId,ResourcesID,Number,SubtotalMoney,IsDelete)
                            VALUES (@ShopCarId,@UserId,@CorpId,@BussId,@ResourcesID,@Number,0,0);

                            /* 如果有属性Id和规格Id，就插入到Tb_ShoppingDetailed表中 */
                            IF @PropertysId IS NOT NULL AND len(@PropertysId)<>0 
                                AND @SpecId IS NOT NULL AND len(@SpecId)<>0
                            INSERT INTO Tb_ShoppingDetailed (Id,BussId,PropertysId,SpecId,ShoppingId) 
                            VALUES (newid(),@BussId,@PropertysId,@SpecId,@ShopCarId);";

                    con.Execute(sql, new
                    {
                        ShopCarId = shopCarId,
                        UserId = userId,
                        CorpId = corpId,
                        BussId = bussId,
                        ResourcesID = resourcesID,
                        Number = number,
                        PropertysId = propertysId,
                        SpecId = specId
                    }, trans);

                    trans.Commit();

                    return new ApiResult(true, "添加成功").toJson();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    return new ApiResult(false, ex.Message).toJson();
                }
            }
        }


        /// <summary>
        /// 获取商家信息
        /// </summary>
        public string GetBussInfo_cc(DataRow row)
        {
            if (!row.Table.Columns.Contains("BussId") || string.IsNullOrEmpty(row["BussId"].ToString()))
            {
                return JSONHelper.FromString(false, "BussId不能为空");
            }

            var bussId = row["BussId"].ToString();
            var avgStar = "0";

            using (var conn = new SqlConnection(PubConstant.BusinessContionString))
            {
                var sql = @"SELECT BussId, BussName, BussAddress, BussLinkMan, BussMobileTel, BussWorkedTel, BussWeiXin, LogoImgUrl ,Province,City,ISNULL(Area,Borough) as Area
                        FROM Tb_System_BusinessCorp where BussId=@BussId AND  isnull(IsClose,'未关闭')='未关闭'";
                var data = conn.Query(sql, new { BussId = bussId }).FirstOrDefault();
                data.BussAddress = String.Format("{0}{1}{2}{3}", data.Province, data.City, data.Area, data.BussAddress);
                sql = @"SELECT Round(convert(float,Sum(Star))/convert(float,COUNT(*)),2) as AvgStar FROM Tb_Resources_CommodityEvaluation as A            
                        Left Join Tb_Charge_Receipt as B on A.RpdCode = B.Id
                        Left Join Tb_System_User as C on B.UserId = C.UserCode
                        where ISNULL(C.UserIsDelete, 0) = 0 and ISNULL(B.IsDelete, 0) = 0 and B.BussId=@BussId";
                avgStar = conn.Query<string>(sql, new { BussId = bussId }).FirstOrDefault();

                data.AvgStar = avgStar;

                return new ApiResult(true, data).toJson();
            }
        }
    }
}
