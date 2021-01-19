using Common;
using Common.Enum;
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
        internal class ResourcesTypeModel
        {
            public string ResourcesTypeID { get; set; }
            public string ResourcesTypeName { get; set; }
            public string ResourcesTypeImgUrl { get; set; }
            public string ResourcesTypeADImgUrl { get; set; }
            public string Remark { get; set; }
        }

        private string GetResourcesType_New(DataRow row)
        {
            var communityId = row["CommunityId"].ToString();

            int type = 0;
            if (row.Table.Columns.Contains("Type") && !string.IsNullOrEmpty(row["Type"].ToString()))
            {
                int.TryParse(row["Type"].ToString(), out type);
            }

            using (var conn = new SqlConnection(PubConstant.BusinessContionString))
            {
                var fields = "";
                var condition = "";

                var sql = "SELECT isnull(col_length('Tb_Resources_Type', 'ResourcesTypeADImgUrl'),0)";
                if (conn.Query<long>(sql).FirstOrDefault() != 0)
                {
                    fields = ",(SELECT TOP 1 value FROM SplitString(isnull(ResourcesTypeADImgUrl,''),',',1)) AS ResourcesTypeADImgUrl ";
                }

                var community = GetCommunity(communityId);
                if (community.CorpID != 1960)
                {
                    condition = $@"ResourcesTypeID IN
                                (
                                    SELECT DISTINCT A.ResourcesTypeID FROM Tb_Resources_Details A
                                        WHERE isnull(A.IsDelete,0)=0 AND isnull(A.IsRelease,'否')='是' AND isnull(A.IsStopRelease,'否')='否'  AND isnull(A.IsServiceType,'否')='否'  
                                        AND A.BussId IN (SELECT BussId FROM Tb_System_BusinessCorp WHERE isnull(IsDelete,0)=0 AND  isnull(IsClose,'未关闭')='未关闭' AND BussId IN 
                                                            (SELECT AA.BussId FROM Tb_System_BusinessCorp_Config AA
                                                            WHERE CorpId=(SELECT TOP 1 CorpId FROM Unified.dbo.Tb_Community WHERE id=@CommunityId)
                                                            UNION
                                                            SELECT bb.BussId FROM Tb_System_BusinessConfig bb WHERE bb.CommunityId=@CommunityId
                                               UNION   SELECT BussId FROM Tb_System_BusinessCorp WHERE BussNature='平台商城' AND  isnull(IsClose,'未关闭')='未关闭' and ISNULL(IsDelete,0)=0
                                    ))
                                ) AND ";
                }

                sql = $@"SELECT DISTINCT * FROM 
                        (
                            SELECT ResourcesTypeID,ResourcesTypeName,Remark,
                                (SELECT TOP 1 value FROM SplitString(isnull(ResourcesTypeImgUrl,''),',',1)) AS ResourcesTypeImgUrl
                                {fields}
                            FROM Tb_Resources_Type WHERE {condition} isnull(IsDelete,0)=0 AND isnull(ParentID,'')=''

                            UNION ALL 

                            SELECT ResourcesTypeID,ResourcesTypeName,Remark,
                                (SELECT TOP 1 value FROM SplitString(isnull(ResourcesTypeImgUrl,''),',',1)) AS ResourcesTypeImgUrl   
                                {fields}
                            FROM Tb_Resources_Type WHERE ResourcesTypeID IN
                            (
                                SELECT ParentID FROM Tb_Resources_Type WHERE {condition} isnull(IsDelete,0)=0 AND ParentID is NOT NULL
                            )
                        ) AS t";

                var data = conn.Query<ResourcesTypeModel>(sql, new { CommunityId = communityId }).ToList();
                data.ForEach(obj =>
                {
                    if (!string.IsNullOrEmpty(obj.ResourcesTypeImgUrl))
                    {
                        var tmp = obj.ResourcesTypeImgUrl.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
                        obj.ResourcesTypeImgUrl = tmp;
                    }
                });
                return new ApiResult(true, data).toJson();
            }
        }

        /// <summary>
        /// 商品二级分类
        /// </summary>
        private string GetResourcesSecondType(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return JSONHelper.FromString(false, "小区ID不能为空");
            }

            if (!row.Table.Columns.Contains("ResourcesTypeID") || string.IsNullOrEmpty(row["ResourcesTypeID"].ToString()))
            {
                return JSONHelper.FromString(false, "商品一级类别不能为空");
            }

            var communityId = row["CommunityId"].ToString();
            var resourcesTypeID = row["ResourcesTypeID"].ToString();

            using (var conn = new SqlConnection(PubConstant.BusinessContionString))
            {
                var fields = "";
                var condition = "";

                var sql = "SELECT isnull(col_length('Tb_Resources_Type', 'ResourcesTypeADImgUrl'),0)";
                if (conn.Query<long>(sql).FirstOrDefault() != 0)
                {
                    fields = ",(SELECT TOP 1 value FROM SplitString(isnull(ResourcesTypeADImgUrl,''),',',1)) AS ResourcesTypeADImgUrl ";
                }

                var community = GetCommunity(communityId);
                if (community.CorpID != 1960)
                {
                    condition = @"ResourcesTypeID IN
                                (
                                    SELECT DISTINCT A.ResourcesTypeID FROM Tb_Resources_Details A
                                        WHERE isnull(A.IsDelete,0)=0 AND isnull(A.IsRelease,'否')='是' AND isnull(A.IsStopRelease,'否')='否'
                                        AND A.BussId IN (SELECT BussId FROM Tb_System_BusinessCorp WHERE isnull(IsDelete,0)=0 AND  isnull(IsClose,'未关闭')='未关闭' AND BussId IN 
                                                            (SELECT AA.BussId FROM Tb_System_BusinessCorp_Config AA
                                                            WHERE CorpId=(SELECT TOP 1 CorpId FROM Unified.dbo.Tb_Community WHERE id=@CommunityId)
                                                            UNION
                                                            SELECT bb.BussId FROM Tb_System_BusinessConfig bb WHERE bb.CommunityId=@CommunityId))
                                ) AND ";
                }

                sql = $@"SELECT ResourcesTypeID,ResourcesTypeName,Remark,
                            (SELECT TOP 1 value FROM SplitString(isnull(ResourcesTypeImgUrl,''),',',1)) AS ResourcesTypeImgUrl
                            {fields}
                        FROM Tb_Resources_Type WHERE {condition} isnull(IsDelete,0)=0 AND ParentID=@ResourcesTypeID order by ResourcesSortCode";

                var data = conn.Query<ResourcesTypeModel>(sql, new { CommunityId = communityId, ResourcesTypeID = resourcesTypeID }).ToList();
                data.ForEach(obj =>
                {
                    if (!string.IsNullOrEmpty(obj.ResourcesTypeImgUrl))
                    {
                        var tmp = obj.ResourcesTypeImgUrl.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
                        obj.ResourcesTypeImgUrl = tmp;
                    }
                });
                return new ApiResult(true, data).toJson();
            }
        }

        /// <summary>
        /// 获取首页推荐商品，默认5个
        /// </summary>
        public string GetHomePageRecommandGoods(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return JSONHelper.FromString(false, "小区ID不能为空");
            }

            using (IDbConnection con = new SqlConnection(PubConstant.GetConnectionString("BusinessContionString")))
            {
                string sql = string.Format(@"SELECT top 5 * FROM Tb_Resources_Details
                                            WHERE BussId IN (SELECT AA.BussId FROM Tb_System_BusinessCorp_Config AA
                                                                WHERE CorpId=(SELECT TOP 1 CorpId FROM Unified.dbo.Tb_Community WHERE id='{0}')
                                                                UNION
                                                                SELECT bb.BussId FROM Tb_System_BusinessConfig bb WHERE bb.CommunityId='{0}')
                                            AND isnull(IsRelease,'否')='是' AND isnull(IsStopRelease,'否')='否'
                                            AND isnull(IsDelete,0)=0 AND isnull(IsRecommend, '否')='是'
                                            ORDER BY RecommendSetDate DESC;", row["CommunityId"].ToString());

                DataTable dt = con.ExecuteReader(sql).ToDataSet().Tables[0];
                return JSONHelper.FromString(dt);
            }
        }

        /// <summary>
        /// 获取首页推荐商品，默认5个,新增返回已付款下单量
        /// </summary>
        public string GetHomePageRecommandGoodsAndQuantity(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return JSONHelper.FromString(false, "小区ID不能为空");
            }

            using (IDbConnection con = new SqlConnection(PubConstant.BusinessContionString))
            {
                string sql = string.Format(@"SELECT top 5 * FROM Tb_Resources_Details
                                            WHERE BussId IN (SELECT AA.BussId FROM Tb_System_BusinessCorp_Config AA
                                                                WHERE CorpId=(SELECT TOP 1 CorpId FROM Unified.dbo.Tb_Community WHERE id='{0}')
                                                                UNION
                                                                SELECT bb.BussId FROM Tb_System_BusinessConfig bb WHERE bb.CommunityId='{0}')
                                            AND isnull(IsRelease,'否')='是' AND isnull(IsStopRelease,'否')='否'
                                            AND isnull(IsDelete,0)=0 AND isnull(IsRecommend, '否')='是'
                                            ORDER BY RecommendSetDate DESC;", row["CommunityId"].ToString());

                DataTable dt = con.ExecuteReader(sql).ToDataSet().Tables[0];
                if (null == dt || dt.Rows.Count == 0)
                {
                    return new ApiResult(true, new string[] { }).toJson();
                }

                List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();

                using (IDbConnection conn = new SqlConnection(PubConstant.BusinessContionString))
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        Dictionary<string, object> dictionary = new Dictionary<string, object>();
                        foreach (DataColumn colum in dt.Columns)
                        {
                            dictionary.Add(colum.ColumnName, item[colum.ColumnName]);
                        }

                        dynamic res = conn.QueryFirstOrDefault<dynamic>("SELECT ISNULL(CONVERT(BIGINT,SUM(Quantity)),0) AS Quantity FROM Tb_Charge_ReceiptDetail WHERE ResourcesID = @ResourcesID", new { ResourcesID = item["ResourcesID"] });
                        dictionary.Add("Quantity", res.Quantity);

                        list.Add(dictionary);
                    }
                }

                return new ApiResult(true, list).toJson();
            }
        }

        /// <summary>
        /// 根据商品分类获取商品列表
        /// </summary>
        public string GetGoodsWithResourcesType(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return JSONHelper.FromString(false, "小区ID不能为空");
            }

            var resourcesType = String.Empty;

            if (!row.Table.Columns.Contains("SecondType") || string.IsNullOrEmpty(row["SecondType"].ToString()))
            {
                return JSONHelper.FromString(false, "商品二级分类不能为空");
            }

            int type = 0;
            if (row.Table.Columns.Contains("Type") && !string.IsNullOrEmpty(row["Type"].ToString()))
            {
                int.TryParse(row["Type"].ToString(), out type);
            }


            // 二级分类
            var communityId = row["CommunityID"].ToString();
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
                            WHERE isnull(bb.IsDelete,0)=0   AND  isnull(IsClose,'未关闭')='未关闭'
                            AND bb.BussId IN
                            (
                                SELECT cc.BussId FROM Tb_System_BusinessCorp_Config cc
                                    WHERE CorpId=(SELECT TOP 1 CorpId FROM Unified.dbo.Tb_Community WHERE id=@CommunityId)
                                UNION
                                SELECT dd.BussId FROM Tb_System_BusinessConfig dd WHERE dd.CommunityId=@CommunityId
                                UNION   SELECT BussId FROM Tb_System_BusinessCorp  WHERE BussNature='平台商城' AND  isnull(IsClose,'未关闭')='未关闭' and ISNULL(IsDelete,0)=0 
                            )
                        )";
            }
            else
            {
                // 该一级分类下所有商品
                if (secondType == "0")
                {
                    if (!row.Table.Columns.Contains("ResourcesTypeID") || string.IsNullOrEmpty(row["ResourcesTypeID"].ToString()))
                    {
                        return JSONHelper.FromString(false, "商品分类不能为空");
                    }
                    resourcesType = row["ResourcesTypeID"].ToString();

                    sql = @" AND isnull(a.IsRelease,'否')='是' AND isnull(a.IsStopRelease,'否')='否' AND isnull(a.IsDelete,0)=0 
                            AND (a.ResourcesTypeID=@FirstResourcesType 
                                    OR a.ResourcesTypeID IN (SELECT ResourcesTypeID FROM Tb_Resources_Type WHERE ParentID=@FirstResourcesType))
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
                                UNION   SELECT BussId FROM Tb_System_BusinessCorp WHERE BussNature='平台商城' AND  isnull(IsClose,'未关闭')='未关闭' and ISNULL(IsDelete,0)=0 
                                )
                            )";
                }
                else // 二级分类下商品
                {
                    sql = @" AND isnull(a.IsRelease,'否')='是' AND isnull(a.IsStopRelease,'否')='否' AND isnull(a.IsDelete,0)=0 
                            AND a.ResourcesTypeID=@SecendResourcesType
                            AND a.BussId IN
                            (
                                SELECT bb.BussId FROM Tb_System_BusinessCorp bb 
                                WHERE isnull(bb.IsDelete,0)=0    AND  isnull(bb.IsClose,'未关闭')='未关闭'
                                AND bb.BussId IN
                                (
                                    SELECT cc.BussId FROM Tb_System_BusinessCorp_Config cc
                                    WHERE CorpId=(SELECT TOP 1 CorpId FROM Unified.dbo.Tb_Community WHERE id=@CommunityId)
                                    UNION
                                    SELECT dd.BussId FROM Tb_System_BusinessConfig dd WHERE dd.CommunityId=@CommunityId
                                    UNION   SELECT BussId FROM Tb_System_BusinessCorp WHERE BussNature='平台商城' AND  isnull(IsClose,'未关闭')='未关闭' and ISNULL(IsDelete,0)=0 
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


            //String andSqlStr = "  AND isnull(A.IsServiceType,'否')='否'   "; 
            String andSqlStr = String.Empty;
            if (type > 0)
            {
                andSqlStr = type == 1 ? "  AND isnull(A.IsServiceType,'否')='否'   " : "  AND isnull(A.IsServiceType,'否')='是' ";

            }

            // 俊发需求6953，查询是否是服务类商品
            sql = $@"SELECT * FROM 
                    (
                        SELECT *,ROW_NUMBER() OVER (ORDER BY t.{sortColumn} DESC) AS RID FROM 
                        (
                            SELECT a.ResourcesID,a.ResourcesName,a.ResourcesSimple,a.ResourcesSalePrice,a.ResourcesDisCountPrice,IsSpecification=0,SpecificationPrice=0,Seckill=0,
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
                            WHERE 1=1 {sql}  {andSqlStr}
                        ) AS t
                    ) AS t
                    WHERE t.RID>(@PageIndex-1)*@PageSize AND t.RID<=(@PageIndex*@PageSize);

                    SELECT count(1) FROM Tb_Resources_Details a  
                    WHERE 1=1 {sql}   {andSqlStr}";

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

                if (data.Any())
                {
                    sql = @"   SELECT * FROM
                         (SELECT ROW_NUMBER() OVER(PARTITION  BY ResourcesID ORDER BY Price ASC) AS Number,* FROM Tb_ResourcesSpecificationsPrice  WHERE   ResourcesID IN @ResourcesID)AS C
                         WHERE C.Number < 2 ";
                    var specification = conn.Query(sql, new { ResourcesID = data.Select(l => l.ResourcesID).ToList() });

                    String isGroupBuy = @" SELECT * FROM
                 (SELECT ROW_NUMBER() OVER(PARTITION  BY ResourcesID ORDER BY GroupBuyingPrice ASC) AS Number,* FROM Tb_ResourcesSpecificationsPrice  WHERE   ResourcesID IN @ResourcesID)AS C
                 WHERE C.Number < 2";
                    var isGroupBuyInfo = conn.Query(isGroupBuy, new { ResourcesID = data.Select(l => l.ResourcesID).ToList() });

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
                var json = new ApiResult(true, data).toJson();
                json = json.Insert(json.Length - 1, $",\"PageCount\":{pageCount}");

                return json;
            }
        }

        /// <summary>
        /// 获取生活首页Banner
        /// </summary>
        private string GetGoodHomeBannerList(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return new ApiResult(false, "小区id不能为空").toJson();
            }

            var communityId = row["CommunityId"].ToString();
            var community = GetCommunity(communityId);
            using (IDbConnection con = new SqlConnection(PubConstant.UnifiedContionString))
            {
                var sql = $@"SELECT TOP 9 * FROM Tb_Notice WHERE EffectiveBegDate<=getdate() AND getdate()<=EffectiveEndDate
                            AND isnull(IsDelete,0)=0 AND NoticeType=3  AND Istheshelves=1  AND CommunityId like '%{communityId}%'
                            ORDER BY IssueDate DESC;";

                var data = con.Query(sql);
                sql = "SELECT isnull(col_length('Tb_Notice', 'JumpMode'),0)";
                if (con.Query<long>(sql).FirstOrDefault() > 0)
                {
                    int jumpType = 0, jumpModel = 0;
                    foreach (var model in data)
                    {
                        if (!String.IsNullOrEmpty((String)model.JumpMode))
                            jumpType = EnumHelper.GetEnumValueByDesc(typeof(JumpModelEnum), (String)model.JumpMode);
                        if (!String.IsNullOrEmpty((String)model.InternalJumpType))
                            jumpModel = EnumHelper.GetEnumValueByDesc(typeof(JumpTypeEnum), (String)model.InternalJumpType);

                        model.JumpMode = jumpType.ToString();
                        model.InternalJumpType = jumpModel.ToString();
                    }
                }
                return new ApiResult(true, data).toJson();
            }
        }


        /// <summary>
        /// 根据类型获取首页图
        /// </summary>
        private string BannerByTypeList(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {

                return new ApiResult(false, "小区id不能为空").toJson();
            }
            int type = 5;
            if (row.Table.Columns.Contains("Type") && string.IsNullOrEmpty(row["Type"].ToString()))
            {
                int.TryParse(row["Type"].ToString(), out type);
            }
            var communityId = row["CommunityId"].ToString();
            var community = GetCommunity(communityId);
            using (IDbConnection con = new SqlConnection(PubConstant.UnifiedContionString))
            {
                var sql = $@"SELECT top 9 * FROM Tb_Notice WHERE EffectiveBegDate<=getdate() AND getdate()<=EffectiveEndDate
                            AND isnull(IsDelete,0)=0 AND NoticeType={type} AND CommunityId like '%{communityId}%'
                            ORDER BY IssueDate DESC;";
                var data = con.Query(sql);

                sql = "SELECT isnull(col_length('Tb_Notice', 'JumpMode'),0)";
                bool isEx = con.Query<long>(sql).FirstOrDefault() > 0;
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
        /// 获取生活首页推荐商品，默认加载10个
        /// Type :首页：1    生活：2
        /// </summary>
        public string GetRecommendGoodsList(DataRow row)
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
            int type = 0;
            if (row.Table.Columns.Contains("Type") && !string.IsNullOrEmpty(row["Type"].AsString()))
            {
                if (!int.TryParse(row["Type"].ToString(), out type))
                {
                    return JSONHelper.FromString(false, "类型错误");
                }
            }

            var pageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());
            var CommunityId = row["CommunityId"].ToString();
            using (IDbConnection con = new SqlConnection(PubConstant.BusinessContionString))
            {
                var field = "IsRecommend";
                var orderField = "a.RecommendSetDate";

                var sql = "SELECT isnull(col_length('Tb_Resources_Details', 'IsTopAD'),0);";
                if (con.Query<long>(sql).FirstOrDefault() > 0)
                {
                    field = "IsTopAD";
                    orderField = "a.TopADSetDate";
                }

                String andSqlStr = "";
                if (type > 0)
                {
                    andSqlStr = type == 1 ? "  AND isnull(IsServiceType,'否')='否'   " : "  AND isnull(IsServiceType,'否')='是' ";
                }

                sql = $@"SELECT t.ResourcesID,t.BussId,t.ResourcesTypeID,t.ResourcesName,t.ResourcesSimple,IsSpecification=0,SpecificationPrice=0,Seckill=0,t.GroupBuyPrice,
                            t.ResourcesPriceUnit,t.ResourcesSalePrice,t.ResourcesDisCountPrice,t.IsRecommend,t.CreateDate,t.IsGroupBuy,t.GroupBuyEndDate,t.GroupBuyStartDate
                            ,(SELECT TOP 1 Value FROM SplitString(t.Img, ',', 1)) AS Img
                        FROM (
	                        SELECT row_number() OVER (ORDER BY A.Sort DESC,{ orderField } DESC) AS RID,
	                        * FROM Tb_Resources_Details a
	                        WHERE BussId IN (SELECT AA.BussId FROM Tb_System_BusinessCorp_Config AA
					                        WHERE CorpId=(SELECT TOP 1 CorpId FROM Unified.dbo.Tb_Community WHERE ID=@CommunityId)
                                            UNION ALL 
											SELECT BussId FROM  Tb_System_BusinessConfig WHERE CommunityId=@CommunityId
                                            EXCEPT 
                                            SELECT BussId FROM Tb_System_BusinessCorp WHERE ISNULL(IsDelete,0)=1 OR isnull(IsClose,'未关闭')='已关闭'
					                        )
	                        AND isnull(IsRelease,'否')='是' AND isnull(IsStopRelease,'否')='否'  { andSqlStr   }
	                        AND isnull(IsDelete,0)=0 AND isnull({ field }, '否')='是'
                        ) AS t
                        WHERE t.RID>(@PageIndex-1)*@PageSize AND t.RID<=(@PageIndex*@PageSize);";

                var data = con.Query(sql, new
                {
                    CommunityId = CommunityId,
                    PageIndex = pageIndex,
                    PageSize = pageSize
                });

                if (data.Any())
                {
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
        /// 获取生活首页爆款商品
        /// </summary>
        public string GetBPGoodsList(DataRow row)
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
                var field = "IsBP";
                var orderField = "a.RecommendSetDate";

                String sql = $@"SELECT t.ResourcesID,t.BussId,t.ResourcesTypeID,t.ResourcesName,t.ResourcesSimple,IsSpecification=0,SpecificationPrice=0,
                            t.ResourcesPriceUnit,t.ResourcesSalePrice,t.ResourcesDisCountPrice,t.IsRecommend,t.CreateDate
                            ,(SELECT TOP 1 Value FROM SplitString(t.Img, ',', 1)) AS Img
                        FROM (
	                        SELECT row_number() OVER (ORDER BY  A.Sort DESC , { orderField } DESC) AS RID,
	                        * FROM Tb_Resources_Details a
	                        WHERE BussId IN (SELECT AA.BussId FROM Tb_System_BusinessCorp_Config AA
					                        WHERE CorpId=(SELECT TOP 1 CorpId FROM Unified.dbo.Tb_Community WHERE ID=@CommunityId)
                                            UNION ALL 
											SELECT BussId FROM  Tb_System_BusinessConfig WHERE CommunityId=@CommunityId
                                            UNION ALL
											SELECT  BussId  FROM Tb_System_BusinessCorp where BussNature='平台商城' and  ISNULL(IsClose,'未关闭') = '未关闭' and  ISNULL(IsDelete,0) =0
                                            EXCEPT 
                                            SELECT BussId FROM Tb_System_BusinessCorp WHERE ISNULL(IsDelete,0)=1 OR isnull(IsClose,'未关闭')='已关闭'
					                        )
	                        AND isnull(IsRelease,'否')='是' AND isnull(IsStopRelease,'否')='否'   AND isnull(IsServiceType,'否')='否' 
	                        AND isnull(IsDelete,0)=0 AND isnull({ field }, '否')='是'
                        ) AS t
                        WHERE t.RID>(@PageIndex-1)*@PageSize AND t.RID<=(@PageIndex*@PageSize);";

                var data = con.Query(sql, new
                {
                    CommunityId = CommunityId,
                    PageIndex = pageIndex,
                    PageSize = pageSize
                });

                if (data.Any())
                {
                    sql = @" SELECT ResourcesID,MIN(Price) as  Price FROM View_Tb_ResourcesSpecificationsPrice_Filter WHERE ISNULL(ISDELETE,0)=0 AND  SpecId IS NOT NULL  AND ResourcesID IN @ResourcesID group by ResourcesID ";
                    var specification = con.Query(sql, new { ResourcesID = data.Select(l => l.ResourcesID).ToList() });

                    foreach (var model in data)
                    {
                        var isContanis = specification.FirstOrDefault(l => l.ResourcesID == model.ResourcesID);
                        if (isContanis != null)
                        {
                            model.IsSpecification = (object)1;
                            model.SpecificationPrice = isContanis.Price;
                        }
                    }
                }
                return new ApiResult(true, data).toJson();
            }
        }

        /// <summary>
        /// 获取生活推荐服务商品
        /// </summary>
        public string GetRecommendationServiceGoodsList(DataRow row)
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
                var field = "IsRecommend";
                var orderField = "a.RecommendSetDate";
                String sql = $@"SELECT t.ResourcesID,t.BussId,t.ResourcesTypeID,t.ResourcesName,t.ResourcesSimple,IsSpecification=0,SpecificationPrice=0,
                            t.ResourcesPriceUnit,t.ResourcesSalePrice,t.ResourcesDisCountPrice,t.IsRecommend,t.CreateDate
                            ,(SELECT TOP 1 Value FROM SplitString(t.Img, ',', 1)) AS Img
                        FROM (
	                        SELECT row_number() OVER (ORDER BY A.Sort DESC,{ orderField } DESC) AS RID,
	                        * FROM Tb_Resources_Details a
	                        WHERE BussId IN (SELECT AA.BussId FROM Tb_System_BusinessCorp_Config AA
					                        WHERE CorpId=(SELECT TOP 1 CorpId FROM Unified.dbo.Tb_Community WHERE ID=@CommunityId)
                                            UNION ALL 
											SELECT BussId FROM  Tb_System_BusinessConfig WHERE CommunityId=@CommunityId
                                            EXCEPT 
                                            SELECT BussId FROM Tb_System_BusinessCorp WHERE ISNULL(IsDelete,0)=1 OR isnull(IsClose,'未关闭')='已关闭'
					                        )
	                        AND isnull(IsRelease,'否')='是' AND isnull(IsStopRelease,'否')='否'
	                        AND isnull(IsDelete,0)=0 AND isnull({ field }, '否')='是' AND ISNULL(A.IsServiceType,'否')='是'
                        ) AS t
                        WHERE t.RID>(@PageIndex-1)*@PageSize AND t.RID<=(@PageIndex*@PageSize);";

                var data = con.Query(sql, new
                {
                    CommunityId = CommunityId,
                    PageIndex = pageIndex,
                    PageSize = pageSize
                });

                if (data.Any())
                {
                    sql = @" SELECT ResourcesID,MIN(Price) as  Price FROM View_Tb_ResourcesSpecificationsPrice_Filter WHERE ISNULL(ISDELETE,0)=0 AND  SpecId IS NOT NULL  AND ResourcesID IN @ResourcesID group by ResourcesID ";
                    var specification = con.Query(sql, new { ResourcesID = data.Select(l => l.ResourcesID).ToList() });

                    foreach (var model in data)
                    {
                        var isContanis = specification.FirstOrDefault(l => l.ResourcesID == model.ResourcesID);
                        if (isContanis != null)
                        {
                            model.IsSpecification = (object)1;
                            model.SpecificationPrice = isContanis.Price;
                        }
                    }
                }
                return new ApiResult(true, data).toJson();
            }
        }


        /// <summary>
        /// 获取生活服务二级分类
        /// </summary>
        public string GetSecondTypeList(DataRow row)
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

                String sql = @"SELECT * FROM 
                    (
                    SELECT A.RecommendSetDate,A.RowNum,b.*,ROW_NUMBER () OVER(ORDER BY A.RecommendSetDate DESC ) AS RID FROM 
                    ( SELECT  ResourcesTypeID,RecommendSetDate,ROW_NUMBER() OVER(  PARTITION BY ResourcesTypeID ORDER BY RecommendSetDate DESC) AS  RowNum FROM Tb_Resources_Details a
                    WHERE BussId IN (SELECT AA.BussId FROM Tb_System_BusinessCorp_Config AA
                    WHERE CorpId=(SELECT TOP 1 CorpId FROM Unified.dbo.Tb_Community WHERE ID=@CommunityId)
                    UNION ALL 
                    SELECT BussId FROM  Tb_System_BusinessConfig WHERE CommunityId=@CommunityId
                    EXCEPT 
                    SELECT BussId FROM Tb_System_BusinessCorp WHERE ISNULL(IsDelete,0)=1 OR isnull(IsClose,'未关闭')='已关闭'
                    )
                    AND isnull(IsRelease,'否')='是' AND isnull(IsStopRelease,'否')='否'
                    AND isnull(IsDelete,0)=0  AND ISNULL(A.IsServiceType,'否')='是'
                    )AS  A
                    LEFT JOIN 
                    (
                    SELECT  *  FROM Tb_Resources_Type  WHERE  CorpId=(SELECT TOP 1 CorpId FROM Unified.dbo.Tb_Community WHERE ID=@CommunityId) OR CorpId=1000
                    )AS B ON A.ResourcesTypeID=B.ResourcesTypeID
                    )AS T WHERE t.RID>(@PageIndex-1)*@PageSize AND t.RID<=(@PageIndex*@PageSize) AND T.RowNum <=1;";

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
        private string GetSecondCategoriesGoodsList(DataRow row)
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

                var sql = @"SELECT ResourcesTypeID,ResourcesTypeName,
                                (SELECT TOP 1 value FROM SplitString(isnull(ResourcesTypeImgUrl,''),',',1)) AS ResourcesTypeImgUrl  
                            FROM Tb_Resources_Type WHERE ResourcesTypeID IN
                            (
                                SELECT DISTINCT A.ResourcesTypeID FROM Tb_Resources_Details A
                                    WHERE isnull(A.IsDelete,0)=0 AND isnull(A.IsRelease,'否')='是' AND isnull(A.IsStopRelease,'否')='否'
                                    AND A.BussId IN (SELECT AA.BussId FROM Tb_System_BusinessCorp_Config AA
                                                    WHERE CorpId=(SELECT TOP 1 CorpId FROM Unified.dbo.Tb_Community WHERE id=@CommunityId)
                                                    UNION
                                                    SELECT bb.BussId FROM Tb_System_BusinessConfig bb WHERE bb.CommunityId=@CommunityId)
                            ) AND isnull(IsDelete,0)=0 AND ParentID=@ResourcesTypeID;

                            /* 加载只有二级分类下的商品 */       
                            SELECT top 6 a.ResourcesID,a.BussId,a.ResourcesTypeID,a.ResourcesName,a.ResourcesSimple,IsSpecification=0,SpecificationPrice=0,
                                a.ResourcesPriceUnit,a.ResourcesSalePrice,a.ResourcesDisCountPrice,a.IsRecommend,a.CreateDate,
                                (SELECT TOP 1 Value FROM SplitString(a.Img,',',1)) AS Img 
                            FROM Tb_Resources_Details a WHERE ResourcesTypeID IN
                            (
	                            SELECT ResourcesTypeID
	                            FROM Tb_Resources_Type WHERE ResourcesTypeID IN
	                            (
		                            SELECT DISTINCT A.ResourcesTypeID FROM Tb_Resources_Details A
			                            WHERE isnull(A.IsDelete,0)=0 AND isnull(A.IsRelease,'否')='是' AND isnull(A.IsStopRelease,'否')='否'
			                            AND A.BussId IN (SELECT AA.BussId FROM Tb_System_BusinessCorp_Config AA
							        WHERE CorpId=(SELECT TOP 1 CorpId FROM Unified.dbo.Tb_Community WHERE id=@CommunityId)
							        UNION
							        SELECT bb.BussId FROM Tb_System_BusinessConfig bb WHERE bb.CommunityId=@CommunityId)
	                            ) AND isnull(IsDelete,0)=0 AND ParentID=@ResourcesTypeID
                            ) ORDER BY a.IsRecommend,a.CreateDate DESC;

                            /* 加载只有一级分类下的商品，默认为全部商品 */
                            SELECT top 6 a.ResourcesID,a.BussId,a.ResourcesTypeID,a.ResourcesName,a.ResourcesSimple,
                                a.ResourcesPriceUnit,a.ResourcesSalePrice,a.ResourcesDisCountPrice,a.IsRecommend,a.CreateDate,
                                (SELECT TOP 1 Value FROM SplitString(a.Img,',',1)) AS Img 
                            FROM Tb_Resources_Details a WHERE ResourcesTypeID IN
                            (
	                            SELECT ResourcesTypeID
	                            FROM Tb_Resources_Type WHERE ResourcesTypeID IN
	                            (
		                            SELECT DISTINCT A.ResourcesTypeID FROM Tb_Resources_Details A
			                            WHERE isnull(A.IsDelete,0)=0 AND isnull(A.IsRelease,'否')='是' AND isnull(A.IsStopRelease,'否')='否'
			                            AND A.BussId IN (SELECT AA.BussId FROM Tb_System_BusinessCorp_Config AA
							        WHERE CorpId=(SELECT TOP 1 CorpId FROM Unified.dbo.Tb_Community WHERE id=@CommunityId)
							        UNION
							        SELECT bb.BussId FROM Tb_System_BusinessConfig bb WHERE bb.CommunityId=@CommunityId)
	                            ) AND isnull(IsDelete,0)=0 AND ResourcesTypeID=@ResourcesTypeID
                            ) ORDER BY a.IsRecommend,a.CreateDate DESC;";


                var reader = conn.QueryMultiple(sql, new
                {
                    CorpID = community.CorpID,
                    CommunityId = communityId,
                    ResourcesTypeID = resourcesTypeID
                });
                var secondType = reader.Read();
                var goodsList = reader.Read().ToList();
                var allGoodsList = reader.Read().ToList();

                var list = new List<dynamic>();


                if (goodsList.Any())
                {
                    sql = @" SELECT ResourcesID,MIN(Price) as  Price FROM View_Tb_ResourcesSpecificationsPrice_Filter WHERE ISNULL(ISDELETE,0)=0 AND  SpecId IS NOT NULL  AND ResourcesID IN @ResourcesID group by ResourcesID ";
                    var specification = conn.Query(sql, new { ResourcesID = goodsList.Select(l => l.ResourcesID).ToList() });

                    foreach (var model in goodsList)
                    {
                        var isContanis = specification.FirstOrDefault(l => l.ResourcesID == model.ResourcesID);
                        if (isContanis != null)
                        {
                            model.IsSpecification = (object)1;
                            model.SpecificationPrice = isContanis.Price;
                        }
                    }
                }
                foreach (dynamic item in secondType)
                {

                    var tmp = goodsList.FindAll(obj => obj.ResourcesTypeID == item.ResourcesTypeID);

                    list.Add(new
                    {
                        ResourcesTypeID = item.ResourcesTypeID,
                        ResourcesTypeName = item.ResourcesTypeName,
                        GoodsList = tmp
                    });
                }

                // 将全部商品默认加在第一位
                list.Insert(0, new
                {
                    ResourcesTypeID = "0",
                    ResourcesTypeName = "全部",
                    GoodsList = allGoodsList
                });

                return new ApiResult(true, list).toJson();
            }
        }

        /// <summary>
        /// 获取商品详情
        /// </summary>
        private string GetGoodsDetail(DataRow row)
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
                        SELECT a.*,b.Express,
                            isnull((
		                            SELECT count(Quantity) FROM Tb_Charge_ReceiptDetail x 
		                            LEFT JOIN Tb_Charge_Receipt y ON x.ReceiptCode=y.Id 
		                            WHERE x.ResourcesID=a.ResourcesID AND y.IsPay='已付款'
	                            ),0) AS SaleCount,
                            (SELECT COUNT(1) FROM Tb_Resources_CommodityEvaluation 
                             WHERE ResourcesID=@ResourcesID) AS EvaluationCount,
                            (SELECT COUNT(1) FROM Tb_Collection 
                             WHERE ResourcesID=@ResourcesID AND UserId=@UserId) AS IsCollection
                        FROM Tb_Resources_Details a 
                        LEFT JOIN Tb_Charge_Receipt b ON a.BussId=b.BussId
                        WHERE a.ResourcesID=@ResourcesID
                        AND isnull(a.IsDelete,0)=0 AND isnull(a.IsRelease,'否')='是' 
                        AND isnull(a.IsStopRelease,'否')='否'

                        /* 商品属性 */
                        SELECT x.*, y.PropertyName FROM Tb_Resources_PropertyRelation x
                        LEFT JOIN Tb_Resources_Property y ON x.PropertyId=y.Id
                        WHERE isnull(y.IsDelete,0)=0 AND x.ResourcesID=@ResourcesID;
                        
                        /* 商品属性下的规格 */
                        SELECT x.Id,x.SpecName,y.PropertyId FROM Tb_Resources_Specifications x
                        LEFT JOIN Tb_Resources_PropertyRelation y ON x.PropertyId=y.PropertyId
                        WHERE y.ResourcesID=@ResourcesID
                        ORDER BY x.Sort;";


                var reader = conn.QueryMultiple(sql, new
                {
                    ResourcesID = resourcesID,
                    UserId = userId
                });

                var data = reader.Read().FirstOrDefault();
                var propertysList = reader.Read().ToList();
                var specsList = reader.Read().ToList();


                foreach (dynamic item in propertysList)
                {

                    var tmp = specsList.FindAll(obj => obj.PropertyId == item.PropertyId)
                        .Select(obj => new
                        {
                            SpecId = obj.Id,
                            SpecName = obj.SpecName
                        });

                    item.SpecsList = tmp;
                }

                data.PropertysList = propertysList;


                return new ApiResult(true, data).toJson();
            }
        }


        /// <summary>
        /// 获取商品的评价信息列表
        /// </summary>
        private string GetGoodEvaluationList(DataRow row)
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
                var sql = @"SELECT *FROM 
                            (
	                            SELECT row_number() OVER (ORDER BY A.EvaluateDate DESC) AS RID,
	                            A.*,C.ResourcesName,E.PropertyName,
	                            F.Name as CustName,F.Mobile,F.Express,G.UserPic,H.SpecName
	                            FROM Tb_Resources_CommodityEvaluation A 
	                            LEFT JOIN Tb_Charge_Receipt B ON A.RpdCode =B.Id
	                            LEFT JOIN Tb_Resources_Details C ON A.ResourcesID=C.ResourcesID
	                            LEFT JOIN Tb_Resources_PropertyRelation D ON C.ResourcesID=D.ResourcesID
	                            LEFT JOIN Tb_Resources_Property E ON D.PropertyId=E.Id
	                            LEFT JOIN Tb_Charge_Receipt F ON B.BussId=F.Id
                                LEFT JOIN Unified.dbo.Tb_User G ON F.UserId=G.Id
                                LEFT JOIN Tb_Resources_Specifications H ON D.PropertyId=H.PropertyId
	                            WHERE isnull(A.IsDelete,0)=0 AND A.ResourcesID=@ResourcesID
                            ) AS t
                            WHERE  t.RID>(@PageIndex-1)*@PageSize AND t.RID<=(@PageIndex*@PageSize);";

                var data = con.Query(sql, new
                {
                    ResourcesID = resourcesID,
                    PageIndex = pageIndex,
                    PageSize = pageSize
                });

                return new ApiResult(true, data).toJson();
            }
        }
    }
}

