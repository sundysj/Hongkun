using Common;
using Dapper;
using DapperExtensions;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using Model.Buss;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Business
{
    /// <summary>
    /// 商品
    /// </summary>
    public partial class ResourcesDetails : PubInfo
    {
        public ResourcesDetails()
        {
            base.Token = "20161103ResourcesDetails";
        }
        public override void Operate(ref Transfer Trans)
        {
            Trans.Result = JSONHelper.FromString(false, "未知错误");
            DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
            DataRow Row = dAttributeTable.Rows[0];

            switch (Trans.Command)
            {
                case "GetHomeCommodity":
                    Trans.Result = GetHomeCommodity(Row);//获取首页商品
                    break;
                case "GetHomeAdvertisingPicture":
                    Trans.Result = GetHomeAdvertisingPicture(Row);//获取首页广告
                    break;
                case "GetCommodity":
                    Trans.Result = GetCommodity(Row);//首页搜索
                    break;
                case "GetCategoryCommodity":
                    Trans.Result = GetCategoryCommodity(Row);//获取各分类商品
                    break;
                case "GetResourcesList":
                    Trans.Result = GetResourcesList(Row);//获取商家商品列表
                    break;
                case "GetInformationContactUs":
                    Trans.Result = GetInformationContactUs(Row);//获取商家 联系我们 信息
                    break;
                case "GetResourcesCommodityEvaluationList":
                    Trans.Result = GetResourcesCommodityEvaluationList(Row);//获取商家的评价信息列表
                    break;
                case "GetResourcesDetailsModel"://获取商品明细
                    Trans.Result = GetResourcesDetailsModel(Row);
                    break;
                case "GetResourcesEvaluation"://获取商品评价
                    Trans.Result = GetResourcesEvaluation(Row);
                    break;
                case "GetResourcesSpecificationsPrice"://获取商品属性规格
                    Trans.Result = GetResourcesSpecificationsPrice(Row);
                    break;
                case "GetResourcesType"://获取商品分类下有所属商品的分类信息
                    Trans.Result = GetResourcesType(Row);
                    break;
                case "GetResourcesType_New"://获取商品分类下有所属商品的分类信息
                    Trans.Result = GetResourcesType_New(Row);
                    break;
                case "GetResourcesSecondType": // 获取商品二级分类
                    Trans.Result = GetResourcesSecondType(Row);
                    break;
                case "GetGoodsWithResourcesType": // 获取商品分类下商品
                    Trans.Result = GetGoodsWithResourcesType(Row);
                    break;
                case "GetHomePageRecommandGoods": // 获取首页推荐商品
                    Trans.Result = GetHomePageRecommandGoods(Row);
                    break;
                case "GetHomePageRecommandGoodsAndQuantity": // 获取首页推荐商品和下单量
                    Trans.Result = GetHomePageRecommandGoodsAndQuantity(Row);
                    break;
                case "GetResourcesTypeList": //根据选择商品类型列出商品数据
                    Trans.Result = GetResourcesTypeList(Row);
                    break;
                case "GetResourcesCollection"://获取收藏商品
                    Trans.Result = GetResourcesCollection(Row);
                    break;
                case "SetResourcesCollection"://收藏商品
                    Trans.Result = SetResourcesCollection(Row);
                    break;
                case "DelResourcesCollection"://删除收藏商品
                    Trans.Result = DelResourcesCollection(Row);
                    break;
                case "GetResourcesIsCollected"://获取当前用户是否收藏该商品的状态
                    Trans.Result = GetResourcesIsCollected(Row);
                    break;
                case "GetRecommendGoodsList":  // 获取生活首页推荐商品，默认加载6个
                    Trans.Result = GetRecommendGoodsList(Row);
                    break;
                case "GetBPGoodsList":  // 获取生活首页爆款商品
                    Trans.Result = GetBPGoodsList(Row);
                    break;
                case "GetRecommendationServiceGoodsList":  // 获取生活推荐服务商品
                    Trans.Result = GetRecommendationServiceGoodsList(Row);
                    break;
                case "GetSecondTypeList":  // 获取生活服务二级分类
                    Trans.Result = GetSecondTypeList(Row);
                    break;
                case "GetSecondCategoriesGoodsList":  // 获取商品二级及商品列表
                    Trans.Result = GetSecondCategoriesGoodsList(Row);
                    break;
                case "GetGoodsDetail":  // 获取商品详情
                    Trans.Result = GetGoodsDetail(Row);
                    break;
                case "GetGoodHomeBannerList":  // 获取生活首页Banner
                    Trans.Result = GetGoodHomeBannerList(Row);
                    break;
                case "BannerByTypeList":  // 根据类型获取Banner
                    Trans.Result = BannerByTypeList(Row);
                    break;
                case "GetGoodsDetail_cc":  // 获取商品详情（长城）
                    Trans.Result = GetGoodsDetail_cc(Row);
                    break;
                case "GetGoodEvaluationList_cc":  // 获取商品的评价信息列表
                    Trans.Result = GetGoodEvaluationList_cc(Row);
                    break;
                case "SetUserAddress_cc":  // 新增/修改收货地址
                    Trans.Result = SetUserAddress_cc(Row);
                    break;
                case "GetUserAddress_cc":  // 获取所有收货地址
                    Trans.Result = GetUserAddress_cc(Row);
                    break;
                case "GetUserDefaultAddress":  // 获取默认收货地址
                    Trans.Result = GetUserDefaultAddress(Row);
                    break;
                case "GetShopChannelList_cc":  // 获取频道列表
                    Trans.Result = GetShopChannelList_cc(Row);
                    break;
                case "GetShopChannelGoodsList_cc":  // 获取频道商品列表
                    Trans.Result = GetShopChannelGoodsList_cc(Row);
                    break;
                case "AddGoodsToShopCar_cc": // 加入购物车
                    Trans.Result = AddGoodsToShopCar_cc(Row);
                    break;
                case "GetBussInfo_cc": // 获取商家信息
                    Trans.Result = GetBussInfo_cc(Row);
                    break;
                case "GetSecondCategoriesGoodsList_cc":  // 获取商品二级及商品列表
                    Trans.Result = GetSecondCategoriesGoodsList_cc(Row);
                    break;

            }
        }

        #region 获取所有存在商品的商品类别
        /// <summary>
        /// 获取所有存在商品的商品类别   GetResourcesType
        /// </summary>
        /// <param name="row"></param>
        /// CommunityId
        /// 返回：
        ///     ResourcesTypeName 商品类别名称
        ///     ResourcesTypeID  商品类别ID
        /// <returns></returns>
        private string GetResourcesType(DataRow row)//'"+row["CommunityId"] +"'
        {
            string sql = @"SELECT B.ResourcesTypeName,A.ResourcesTypeID,D.CorpId,E.Id FROM dbo.Tb_Resources_Details A 
                                        LEFT OUTER JOIN Tb_Resources_Type B ON A.ResourcesTypeID = B.ResourcesTypeID 
                                        LEFT OUTER JOIN dbo.Tb_System_BusinessCorp C ON A.BussId=C.BussId
                                        LEFT OUTER JOIN dbo.Tb_System_BusinessCorp_Config D ON C.BussId=D.BussId 
                                        LEFT OUTER JOIN Unified.dbo.Tb_Community E ON D.CorpId=E.CorpID
                                        WHERE ISNULL(A.ResourcesTypeID,'') <> ''
                                        AND E.Id='" + row["CommunityId"] + "'" +
                            @" AND ISNULL(A.IsDelete, 0) = 0
                                        AND ISNULL(B.IsDelete, 0) = 0 and isnull(B.ParentID,'')='' AND A.IsRelease='是' and A.IsStopRelease='否'
                                        GROUP BY B.ResourcesTypeName,A.ResourcesTypeID,D.CorpId,E.Id
            UNION                            
            SELECT B.ResourcesTypeName,A.ResourcesTypeID,E.CorpId,D.CommunityId FROM dbo.Tb_Resources_Details A 
                                        LEFT OUTER JOIN Tb_Resources_Type B ON A.ResourcesTypeID = B.ResourcesTypeID 
                                        LEFT OUTER JOIN dbo.Tb_System_BusinessCorp C ON A.BussId=C.BussId
                                        LEFT OUTER JOIN dbo.Tb_System_BusinessConfig D ON C.BussId=D.BussId
            							LEFT OUTER JOIN Unified.dbo.Tb_Community E ON D.CommunityId=E.Id
                                        WHERE ISNULL(A.ResourcesTypeID,'') <> ''
                                        AND E.Id='" + row["CommunityId"] + "'" +
                            @"AND ISNULL(A.IsDelete, 0) = 0
                                        AND ISNULL(B.IsDelete, 0) = 0 and isnull(B.ParentID,'')='' AND A.IsRelease='是' and A.IsStopRelease='否'
                                        GROUP BY B.ResourcesTypeName,A.ResourcesTypeID,E.CorpId,D.CommunityId";

            DataSet Ds = BussinessCommon.GetResourcesType(sql);
            if (Ds == null || Ds.Tables.Count <= 0 || Ds.Tables[0].Rows.Count <= 0)
            {
                return JSONHelper.FromString("");
            }
            return JSONHelper.FromString(Ds.Tables[0]);
        }

        #endregion

        #region 根据选择商品类型列出商品数据
        /// <summary>
        /// 根据选择商品类型列出商品数据   GetResourcesTypeList
        /// </summary>
        /// <param name="row"></param>
        /// CorpID:物管ID【必填】
        /// CommunityId:小区编号【必填，取运营系统中小区编码】
        /// PageIndex   默认1
        /// PageSize    默认5
        /// ResourcesTypeID 查询条件【资源类别】
        /// CommodityType   查询类别【101：平台商城；102：物管商城；103：周边商家；默认查询所有】
        /// EvaluateNum  评价数量
        /// Sale         销量
        /// DataSort     0升序，1降序    默认时间降序
        /// EvaluateSort 默认评价 降序
        /// SaleSort     默认销量 降序
        /// PriceSort    默认价格  升序
        /// 排序字段顺序：DataSort，EvaluateSort，SaleSort，PriceSort  后期可根据实际需求调整
        /// 应界面要求，暂只支持单一字段排序。默认日期降序
        /// 返回：
        ///     ResourcesID 商品ID
        ///     ResourcesName  商品名称
        ///     ResourcesSalePrice 销售单价
        ///     ResourcesDisCountPrice  优惠单价
        ///     GroupBuyPrice  团购单价
        ///     typename   数据类别【101：平台商城，102物管商城，103：周边商家】
        /// <returns></returns>
        private string GetResourcesTypeList(DataRow row)
        {
            int pageCount;
            int Counts;
            int PageIndex = 1;
            int PageSize = 6;
            string CommodityType = "all";
#pragma warning disable CS0219 // 变量“DataSort”已被赋值，但从未使用过它的值
            int DataSort = 1;//默认时间  降序
#pragma warning restore CS0219 // 变量“DataSort”已被赋值，但从未使用过它的值
#pragma warning disable CS0219 // 变量“EvaluateSort”已被赋值，但从未使用过它的值
            int EvaluateSort = 1;//默认评价 降序
#pragma warning restore CS0219 // 变量“EvaluateSort”已被赋值，但从未使用过它的值
#pragma warning disable CS0219 // 变量“SaleSort”已被赋值，但从未使用过它的值
            int SaleSort = 1;//默认销量 降序
#pragma warning restore CS0219 // 变量“SaleSort”已被赋值，但从未使用过它的值
#pragma warning disable CS0219 // 变量“PriceSort”已被赋值，但从未使用过它的值
            int PriceSort = 0;//默认价格  升序
#pragma warning restore CS0219 // 变量“PriceSort”已被赋值，但从未使用过它的值
            int SortValue = 1;//排序字段值，默认降序

            if (row.Table.Columns.Contains("PageIndex") && AppGlobal.StrToInt(row["PageIndex"].ToString()) > 0)
            {
                PageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());
            }
            if (row.Table.Columns.Contains("PageSize") && AppGlobal.StrToInt(row["PageSize"].ToString()) > 0)
            {
                PageSize = AppGlobal.StrToInt(row["PageSize"].ToString());
            }

            #region 排序字段
            //拼接排序字段，最后一个字段不用加排序标记
            //SortA Asc,SortB Desc,SortC             
            if (row.Table.Columns.Contains("DataSort") && AppGlobal.StrToInt(row["DataSort"].ToString()) > 0)
            {
                SortValue = AppGlobal.StrToInt(row["DataSort"].ToString());
            }
            string sb = "  CreateDate  ";
            //sb.AppendFormat(" CreateDate {0}", BussinessCommon.GetSortStr(DataSort));

            if (row.Table.Columns.Contains("EvaluateSort") && AppGlobal.StrToInt(row["EvaluateSort"].ToString()) > 0)
            {
                SortValue = AppGlobal.StrToInt(row["EvaluateSort"].ToString());
                sb = " EvaluateNum ";
                //sb.AppendFormat(" ,EvaluateNum {0}", BussinessCommon.GetSortStr(EvaluateSort));
            }
            if (row.Table.Columns.Contains("SaleSort") && AppGlobal.StrToInt(row["SaleSort"].ToString()) > 0)
            {
                SortValue = AppGlobal.StrToInt(row["SaleSort"].ToString());
                sb = " Sale ";
                //sb.AppendFormat(" ,Sale {0}", BussinessCommon.GetSortStr(SaleSort));
            }
            if (row.Table.Columns.Contains("PriceSort") && AppGlobal.StrToInt(row["PriceSort"].ToString()) > 0)
            {
                SortValue = AppGlobal.StrToInt(row["PriceSort"].ToString());
                sb = " ResourcesSalePrice ";
                //sb.Append(" ,ResourcesSalePrice {0}");
            }
            #endregion

            #region 商家类别转换
            if (row.Table.Columns.Contains("CommodityType") && row["CommodityType"].ToString() != "")
            {
                switch (row["CommodityType"].ToString())
                {
                    case "101":
                        CommodityType = "平台商城";
                        break;
                    case "102":
                        CommodityType = "物管商城";
                        break;
                    case "103":
                        CommodityType = "周边商家";
                        break;
                    default:
                        CommodityType = "all";
                        break;
                }
            }
            #endregion
            DataSet ds = BussinessCommon.GetResourcesTypeList(out pageCount, out Counts, " AND A.ResourcesTypeID='" + row["ResourcesTypeID"] + "'", PageIndex, PageSize, sb.ToString(), SortValue, "ResourcesID", row["CorpID"].ToString(), row["CommunityId"].ToString(), CommodityType, "t.ResourcesName,t.ResourcesSalePrice,t.ResourcesDisCountPrice,t.GroupBuyPrice,t.Img,t.typename,t.CreateDate,Sale=dbo.funGetCommoditySale(t.ResourcesID),EvaluateNum=dbo.funGetCommodityEvaluate(t.ResourcesID),t.ResourcesTypeID");
            return JSONHelper.FromString(ds.Tables[0]);
        }
        #endregion

        /// <summary>
        /// 获取商品属性规格   GetResourcesSpecificationsPrice
        /// </summary>
        /// <param name="row"></param>
        /// ResourcesID     商品编号【必填】
        /// <returns></returns>
        private string GetResourcesSpecificationsPrice(DataRow row)
        {
            if (!row.Table.Columns.Contains("ResourcesID") || string.IsNullOrEmpty(row["ResourcesID"].ToString()))
            {
                return JSONHelper.FromString(false, "商品编号不能为空");
            }
            IDbConnection con = new SqlConnection(PubConstant.GetConnectionString("BusinessContionString"));
            //获取属性
            DataSet ds = con.ExecuteReader("select p.Id,p.PropertyName from Tb_ResourcesSpecificationsPrice as r left join Tb_Resources_Property as p on R.PropertyId = P.Id where r.ResourcesID = '" + AppGlobal.ChkStr(row["ResourcesID"].ToString()) + "' group by r.ResourceID, p.Id, p.PropertyName", null, null, null, CommandType.Text).ToDataSet();
            con = new SqlConnection(PubConstant.GetConnectionString("BusinessContionString"));
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"Result\":\"true\",");
            //如果属性为空，
            if (ds == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
            {
                sb.Append("\"data\":[]}");
                return sb.ToString();
            }

            //获取规格
            DataSet ds_Spec = con.ExecuteReader("select s.PropertyId,s.SpecName,r.SpecId  from Tb_ResourcesSpecificationsPrice as r left join Tb_Resources_Specifications as s on s.Id = R.SpecId where   r.ResourcesID = '" + AppGlobal.ChkStr(row["ResourcesID"].ToString()) + "'", null, null, null, CommandType.Text).ToDataSet();
            //如果规格为空
            if (ds_Spec == null || ds_Spec.Tables.Count <= 0 || ds_Spec.Tables[0].Rows.Count <= 0)
            {
                sb.Append("\"data\":");
                sb.Append(JSONHelper.FromDataTable(ds.Tables[0]));
                sb.Append("}");
                return sb.ToString();
            }
            sb.Append("\"data\":");
            int i = 0;
            foreach (DataRow item in ds.Tables[0].Rows)
            {
                if (i == 0)
                {
                    sb.Append("[");
                }
                else
                {
                    sb.Append(",[");
                    string sstr = JSONHelper.FromDataRow(item);
                    string ss = sb.ToString().Substring(0, sb.ToString().Length - 1);
                    sb.Append(ss);
                    sb.Append(",");
                    sb.Append("\"Property\":");
                    sb.Append("[");
                    int j = 0;
                    foreach (DataRow dr in ds_Spec.Tables[0].Select(" PropertyId='" + item["Id"] + "'"))
                    {
                        if (j == 0)
                        {
                            sb.Append(JSONHelper.FromDataRow(dr));
                        }
                        else
                        {
                            sb.Append(",");
                            sb.Append(JSONHelper.FromDataRow(dr));
                        }
                        j++;
                    }
                }
                sb.Append("]");
                sb.Append("}");
                sb.Append("]");
                i++;
            }
            sb.Append("}");

            return sb.ToString();
        }

        /// <summary>
        /// 获取商品评价 GetResourcesEvaluation
        /// </summary>
        /// <param name="row"></param>
        /// ResourcesID         商品编号【必填】
        /// <returns></returns>
        private string GetResourcesEvaluation(DataRow row)
        {
            if (!row.Table.Columns.Contains("ResourcesID") || string.IsNullOrEmpty(row["ResourcesID"].ToString()))
            {
                return JSONHelper.FromString(false, "商品编号不能为空");
            }
            IDbConnection con = new SqlConnection(PubConstant.GetConnectionString("BusinessContionString"));

            DataSet ds = con.ExecuteReader("SELECT A.*,C.Name ,C.UserPic FROM Tb_Resources_CommodityEvaluation as A                         Left Join Tb_Charge_Receipt as B on A.RpdCode = B.ReceiptCode  Left Join Unified.dbo.Tb_User as C on B.UserId = C.Id  where  ISNULL(B.IsDelete, 0) = 0 and A.ResourcesID =@ResourcesID ", null, null, null, CommandType.Text).ToDataSet();
            return JSONHelper.FromString(ds);

        }

        #region 获取商品详情


        private string GetResourcesDetailsModel(DataRow row)
        {
            if (!row.Table.Columns.Contains("ResourcesID") || string.IsNullOrEmpty(row["ResourcesID"].ToString()))
            {
                return JSONHelper.FromString(false, "商品编号不能为空");
            }

            var resourcesId = row["ResourcesID"].ToString();
            var userId = "";

            if (row.Table.Columns.Contains("ResourcesID") && !string.IsNullOrEmpty(row["ResourcesID"].ToString()))
            {
                userId = row["ResourcesID"].ToString();
            }

            using (var conn = new SqlConnection(PubConstant.BusinessContionString))
            {
                var sql = "SELECT isnull(col_length('Tb_Resources_Details', 'AllowBuy'),0)";
                if (conn.Query<int>(sql).FirstOrDefault() == 0)
                {
                    sql = @"alter table dbo.Tb_Resources_Details add AllowBuy bit default 1 not null; 
                            exec sp_addextendedproperty 'MS_Description', '是否允许购买', 'SCHEMA', 'dbo', 'TABLE', 'Tb_Resources_Details', 'COLUMN', 'AllowBuy';";
                    conn.Execute(sql);
                }

                sql = @"SELECT a.ResourcesID,a.BussId,a.ResourcesTypeID,ltrim(rtrim(a.ResourcesName)) AS ResourcesName,
                            a.ResourcesSimple,a.ResourcesIndex,ltrim(rtrim(a.ResourcesBarCode)) AS ResourcesBarCode,
                            a.ResourcesUnit,a.ResourcesCount,a.ResourcesPriceUnit,a.ResourcesSalePrice,a.ResourcesDisCountPrice,
                            a.ReleaseAdContent,a.IsRelease,a.ScheduleType,a.IsStopRelease,
                            a.IsGroupBuy,a.GroupBuyPrice,a.GroupBuyStartDate,a.GroupBuyEndDate,
                            a.PaymentType,a.CreateDate,a.IsBp,a.Img,b.BussNature,b.BussName,b.BussMobileTel,
                            CASE c.ResourcesTypeName WHEN '生活服务' THEN 1 ELSE 0 END AS IsService,
                            convert(int,AllowBuy) AS AllowBuy,
                            CASE WHEN (SELECT count(1) FROM Tb_Collection WHERE UserId=@UserId AND ResourcesID=a.ResourcesID AND IsDelete=0)>0
                                THEN 1 ELSE 0 END AS HasCollected
                        FROM Tb_Resources_Details a
                        LEFT JOIN Tb_System_BusinessCorp b ON a.BussId=b.BussId
                        LEFT JOIN Tb_Resources_Type c ON a.ResourcesTypeID=c.ResourcesTypeID
                        WHERE ResourcesID=@ResourcesID AND isnull(a.IsDelete,0)=0 AND isnull(a.IsStopRelease,'否')='否';";

                // 商品详细信息
                var resourcesInfo = conn.Query(sql, new { UserId = userId, ResourcesID = resourcesId }).FirstOrDefault();
                if (resourcesInfo == null)
                {
                    return JSONHelper.FromString(false, "抱歉，该商品已下架");
                }

                sql = @"SELECT CorpId FROM Tb_System_BusinessCorp_Config WHERE BussId=@BussId
                        UNION ALL
                        SELECT CorpId FROM Unified.dbo.Tb_Community 
                        WHERE Id IN(SELECT CommunityId FROM Tb_System_BusinessConfig WHERE BussId=@BussId)";

                // 上上物业，周边商家不允许购买，需求7596
                if (conn.Query<int>(sql, new { BussId = resourcesInfo.BussId }).FirstOrDefault() == 2125)
                {
                    if (string.IsNullOrEmpty(resourcesInfo.BussNature) || resourcesInfo.BussNature == "周边商家")
                    {
                        resourcesInfo.AllowBuy = (object)0;
                    }
                }

                // 属性规格
                sql = @"SELECT a.Id,a.PropertyName,a.BussId,b.ResourcesID
                        FROM Tb_Resources_Property a
                        LEFT JOIN Tb_Resources_PropertyRelation b ON a.Id=b.PropertyId
                        WHERE ResourcesID=@ResourcesID;

                        SELECT a.Id AS SpecId,a.SpecName,b.PropertyId,isnull(b.Price,0.0) AS Price,b.ResourcesID
                        FROM Tb_Resources_Specifications a
                        LEFT JOIN Tb_ResourcesSpecificationsPrice b ON a.Id=b.SpecId
                        WHERE b.ResourcesID=@ResourcesID;";

                var reader = conn.QueryMultiple(sql, new { ResourcesID = resourcesId });
                var properties = reader.Read().ToList();
                var specs = reader.Read().ToList();

                var list = new List<dynamic>();

                if (properties.Count() > 0 && specs.Count() > 0)
                {
                    foreach (var property in properties)
                    {
                        // 筛选属性
                        var tmp = list.Find(obj => obj.Id == property.Id);
                        if (tmp == null)
                        {
                            tmp = property;
                        }

                        // 筛选规格
                        var ss = specs.FindAll(obj => obj.PropertyId == tmp.Id);
                        if (ss.Count() > 0)
                        {
                            tmp.ds_Propertys = ss;

                            list.Add(tmp);
                        }
                    }
                }

                resourcesInfo.Property = list;

                return new ApiResult(true, new[] { resourcesInfo }).toJson();
            }
        }

        #endregion

        #region 获取首页商品
        /// <summary>
        /// 获取首页商品  GetHomeCommodity
        /// </summary>
        /// <param name="row"></param>
        /// CorpID:物管ID【必填】
        /// CommunityId:小区编号【必填，取运营系统中小区编码】
        /// ResourcesTypeID 查询条件【资源类别】
        /// 返回：
        ///     ResourcesID 商品ID
        ///     ResourcesName  商品名称
        ///     ResourcesSalePrice 销售单价
        ///     ResourcesDisCountPrice  优惠单价
        ///     GroupBuyPrice  团购单价
        ///     typename   数据类别【101：平台商城，102物管商城，103：周边商家】
        /// <returns></returns>
        private string GetHomeCommodity(DataRow row)
        {
            //通过CommunityId判断商家

            int pageCount;
            int Counts;
            DataSet ds = null;
            if (row.Table.Columns.Contains("ResourcesTypeID") && row["ResourcesTypeID"].ToString() != "")
            {
                ds = BussinessCommon.GetResourcesTypeList(out pageCount, out Counts, $@" AND A.ResourcesTypeID='" + row["ResourcesTypeID"] + "'", 1, 9, "CreateDate", 1, "ResourcesID", row["CorpID"].ToString(), row["CommunityId"].ToString(), "all", "t.ResourcesName,t.ResourcesSalePrice,t.ResourcesDisCountPrice,t.GroupBuyPrice,t.Img,t.typename,t.CreateDate,t.ResourcesTypeID");
            }
            else
            {
                ds = BussinessCommon.GetResourcesTypeList(out pageCount, out Counts, "", 1, 27, "CreateDate", 1, "ResourcesID", row["CorpID"].ToString(), row["CommunityId"].ToString(), "all", "t.ResourcesName,t.ResourcesSalePrice,t.ResourcesDisCountPrice,t.GroupBuyPrice,t.Img,t.typename,t.CreateDate,t.ResourcesTypeID");
            }
            return JSONHelper.FromString(ds.Tables[0]);
        }


        #endregion

        #region 获取首页广告
        /// <summary>
        /// 获取首页广告   GetHomeAdvertisingPicture
        /// </summary>
        /// <param name="row"></param>
        /// num  条数  默认5条
        /// CorpId  [必填]
        /// CommID [必填]
        /// 返回：
        ///     TopNum,
        ///     CommID  小区ID      
        /// <returns></returns>
        private string GetHomeAdvertisingPicture(DataRow row)
        {
            int TopNum = 5;
            string CommunityId = "";
            if (row.Table.Columns.Contains("TopNum"))
            {
                TopNum = AppGlobal.StrToInt(row["TopNum"].ToString());
            }

            string Sql = "select * from (SELECT top 3  a.Id as [InfoID],a.NoticeType,a.ContentURL,a.ImageURL as ImageUrl ,a.Title as [Heading],a.IssueDate ,a.CommunityId FROM Tb_Notice A  WHERE NoticeType=3  and isnull(a.IsDelete,0) = 0 and a.CommunityId is null  UNION ALL SELECT top 5 a.Id as [InfoID],a.NoticeType,a.ContentURL,a.ImageURL as ImageUrl ,a.Title as [Heading],a.IssueDate ,a.CommunityId FROM Tb_Notice A left Join Tb_Community as b on A.CommunityId = B.Id WHERE NoticeType = 3  and isnull(a.IsDelete, 0) = 0 ";
            if (row.Table.Columns.Contains("CommID"))
            {
                CommunityId = row["CommID"].ToString();
            }

            Sql = Sql + " and  a.CommunityId like '%" + CommunityId + "%'   order by IssueDate desc  ";
            //Sql = Sql + "CHARINDEX(a.CommunityId,'"+ CommunityId + "')>0  order by IssueDate desc   ";
            Sql += " ) as t order by CommunityId desc ,IssueDate desc";

            DataTable dTable = new DbHelperSQLP(PubConstant.UnifiedContionString.ToString()).Query(string.Format(Sql)).Tables[0];
            return JSONHelper.FromString(dTable);
        }
        #endregion

        #region 获取各分类商品

        /// <summary>
        /// 获取各分类商品   GetCategoryCommodity
        /// </summary>
        /// <param name="row"></param>
        /// CorpID:物管ID【必填】
        /// CommunityId:小区编号【必填，取运营系统中小区编码】
        /// PageIndex   默认1
        /// PageSize    默认5
        /// CommodityType   查询类别【101：平台商城；102：物管商城；103：周边商家；默认查询所有】
        /// EvaluateNum  评价数量
        /// Sale         销量
        /// DataSort     0升序，1降序    默认时间降序
        /// EvaluateSort 默认评价 降序
        /// SaleSort     默认销量 降序
        /// PriceSort    默认价格  升序
        /// 排序字段顺序：DataSort，EvaluateSort，SaleSort，PriceSort  后期可根据实际需求调整
        /// 应界面要求，暂只支持单一字段排序。默认日期降序
        /// 返回：
        ///     ResourcesID 商品ID
        ///     ResourcesName  商品名称
        ///     ResourcesSalePrice 销售单价
        ///     ResourcesDisCountPrice  优惠单价
        ///     GroupBuyPrice  团购单价
        ///     typename   数据类别【101：平台商城，102物管商城，103：周边商家】
        /// <returns></returns>
        private string GetCategoryCommodity(DataRow row)
        {
            int pageCount;
            int Counts;
            int PageIndex = 1;
            int PageSize = 6;
            string CommodityType = "all";
#pragma warning disable CS0219 // 变量“DataSort”已被赋值，但从未使用过它的值
            int DataSort = 1;//默认时间  降序
#pragma warning restore CS0219 // 变量“DataSort”已被赋值，但从未使用过它的值
#pragma warning disable CS0219 // 变量“EvaluateSort”已被赋值，但从未使用过它的值
            int EvaluateSort = 1;//默认评价 降序
#pragma warning restore CS0219 // 变量“EvaluateSort”已被赋值，但从未使用过它的值
#pragma warning disable CS0219 // 变量“SaleSort”已被赋值，但从未使用过它的值
            int SaleSort = 1;//默认销量 降序
#pragma warning restore CS0219 // 变量“SaleSort”已被赋值，但从未使用过它的值
#pragma warning disable CS0219 // 变量“PriceSort”已被赋值，但从未使用过它的值
            int PriceSort = 0;//默认价格  升序
#pragma warning restore CS0219 // 变量“PriceSort”已被赋值，但从未使用过它的值
            int SortValue = 1;//排序字段值，默认降序

            if (row.Table.Columns.Contains("PageIndex") && AppGlobal.StrToInt(row["PageIndex"].ToString()) > 0)
            {
                PageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());
            }
            if (row.Table.Columns.Contains("PageSize") && AppGlobal.StrToInt(row["PageSize"].ToString()) > 0)
            {
                PageSize = AppGlobal.StrToInt(row["PageSize"].ToString());
            }

            #region 排序字段
            //拼接排序字段，最后一个字段不用加排序标记
            //SortA Asc,SortB Desc,SortC             
            if (row.Table.Columns.Contains("DataSort") && AppGlobal.StrToInt(row["DataSort"].ToString()) > 0)
            {
                SortValue = AppGlobal.StrToInt(row["DataSort"].ToString());
            }
            string sb = "  CreateDate  ";
            //sb.AppendFormat(" CreateDate {0}", BussinessCommon.GetSortStr(DataSort));

            if (row.Table.Columns.Contains("EvaluateSort") && AppGlobal.StrToInt(row["EvaluateSort"].ToString()) > 0)
            {
                SortValue = AppGlobal.StrToInt(row["EvaluateSort"].ToString());
                sb = " EvaluateNum ";
                //sb.AppendFormat(" ,EvaluateNum {0}", BussinessCommon.GetSortStr(EvaluateSort));
            }
            if (row.Table.Columns.Contains("SaleSort") && AppGlobal.StrToInt(row["SaleSort"].ToString()) > 0)
            {
                SortValue = AppGlobal.StrToInt(row["SaleSort"].ToString());
                sb = " Sale ";
                //sb.AppendFormat(" ,Sale {0}", BussinessCommon.GetSortStr(SaleSort));
            }
            if (row.Table.Columns.Contains("PriceSort") && AppGlobal.StrToInt(row["PriceSort"].ToString()) > 0)
            {
                SortValue = AppGlobal.StrToInt(row["PriceSort"].ToString());
                sb = " ResourcesSalePrice ";
                //sb.Append(" ,ResourcesSalePrice {0}");
            }
            #endregion

            #region 商家类别转换
            if (row.Table.Columns.Contains("CommodityType") && row["CommodityType"].ToString() != "")
            {
                switch (row["CommodityType"].ToString())
                {
                    case "101":
                        CommodityType = "平台商城";
                        break;
                    case "102":
                        CommodityType = "物管商城";
                        break;
                    case "103":
                        CommodityType = "周边商家";
                        break;
                    default:
                        CommodityType = "all";
                        break;
                }
            }

            #endregion

            DataSet ds = BussinessCommon.GetResourcesDetails(out pageCount, out Counts, "", PageIndex, PageSize, sb.ToString(), SortValue, "ResourcesID", row["CorpID"].ToString(), row["CommunityId"].ToString(), CommodityType, "t.ResourcesName,t.ResourcesSalePrice,t.ResourcesDisCountPrice,t.GroupBuyPrice,t.Img,t.typename,t.CreateDate,Sale=dbo.funGetCommoditySale(t.ResourcesID),EvaluateNum=dbo.funGetCommodityEvaluate(t.ResourcesID)");
            return JSONHelper.FromString(ds.Tables[0]);
        }

        #endregion

        #region 搜索商品
        /// <summary>
        /// 搜索商品   GetCommodity
        /// </summary>
        /// <param name="row"></param>
        /// CorpID:物管ID【必填】
        /// CommunityId:小区编号【必填，取运营系统中小区编码】
        /// StrCondition    商品名称 
        /// PageIndex   默认1
        /// PageSize    默认5
        /// CommodityType   查询类别【101：平台商城；102：物管商城；103：周边商家；默认查询所有】
        /// EvaluateNum  评价数量
        /// Sale         销量        
        /// 返回：
        ///     ResourcesID 商品ID
        ///     ResourcesName  商品名称
        ///     ResourcesSalePrice 销售单价
        ///     ResourcesDisCountPrice  优惠单价
        ///     GroupBuyPrice  团购单价
        ///     typename   数据类别【101：平台商城，102物管商城，103：周边商家】
        /// <returns></returns>
        private string GetCommodity(DataRow row)
        {
            int pageCount;
            int Counts;
            int PageIndex = 1;
            int PageSize = 6;
            string CommodityType = "all";
#pragma warning disable CS0219 // 变量“DataSort”已被赋值，但从未使用过它的值
            int DataSort = 1;//默认时间  降序
#pragma warning restore CS0219 // 变量“DataSort”已被赋值，但从未使用过它的值
#pragma warning disable CS0219 // 变量“EvaluateSort”已被赋值，但从未使用过它的值
            int EvaluateSort = 1;//默认评价 降序
#pragma warning restore CS0219 // 变量“EvaluateSort”已被赋值，但从未使用过它的值
#pragma warning disable CS0219 // 变量“SaleSort”已被赋值，但从未使用过它的值
            int SaleSort = 1;//默认销量 降序
#pragma warning restore CS0219 // 变量“SaleSort”已被赋值，但从未使用过它的值
#pragma warning disable CS0219 // 变量“PriceSort”已被赋值，但从未使用过它的值
            int PriceSort = 0;//默认价格  升序
#pragma warning restore CS0219 // 变量“PriceSort”已被赋值，但从未使用过它的值
            int SortValue = 1;//排序字段值，默认降序

            if (row.Table.Columns.Contains("PageIndex") && AppGlobal.StrToInt(row["PageIndex"].ToString()) > 0)
            {
                PageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());
            }
            if (row.Table.Columns.Contains("PageSize") && AppGlobal.StrToInt(row["PageSize"].ToString()) > 0)
            {
                PageSize = AppGlobal.StrToInt(row["PageSize"].ToString());
            }

            string StrCondition = "";
            if (row.Table.Columns.Contains("StrCondition") && row["StrCondition"].ToString() != "")
            {
                StrCondition = " and ResourcesName like '%" + row["StrCondition"].ToString() + "%' ";
            }

            DataSet ds = BussinessCommon.GetResourcesDetailsAll(out pageCount, out Counts, StrCondition, PageIndex, PageSize, "ResourcesSalePrice", SortValue, "ResourcesID", row["CorpID"].ToString(), row["CommunityId"].ToString(), CommodityType, "t.ResourcesName,t.ResourcesSalePrice,t.ResourcesDisCountPrice,t.GroupBuyStartDate,t.GroupBuyEndDate,t.IsGroupBuy,t.GroupBuyPrice,t.Img,t.typename,t.CreateDate,Sale=dbo.funGetCommoditySale(t.ResourcesID),IsSpecification=0,SpecificationPrice=0,Seckill=0,EvaluateNum=dbo.funGetCommodityEvaluate(t.ResourcesID)");

            if (ds.Tables[0].Rows.Count > 0)
            {
                List<Object> resourcesIDs = new List<Object>();
                foreach (DataRow rowData in ds.Tables[0].Rows)
                {
                    resourcesIDs.Add(rowData["ResourcesID"]);
                }
                IDbConnection con = new SqlConnection(PubConstant.GetConnectionString("BusinessContionString"));

                var sql = @"   SELECT * FROM
                         (SELECT ROW_NUMBER() OVER(PARTITION  BY ResourcesID ORDER BY Price ASC) AS Number,* FROM Tb_ResourcesSpecificationsPrice  WHERE   ResourcesID IN @ResourcesID)AS C
                         WHERE C.Number < 2 ";
                var specification = con.Query(sql, new { ResourcesID = resourcesIDs });

                String isGroupBuy = @" SELECT * FROM
                 (SELECT ROW_NUMBER() OVER(PARTITION  BY ResourcesID ORDER BY GroupBuyingPrice ASC) AS Number,* FROM Tb_ResourcesSpecificationsPrice  WHERE   ResourcesID IN @ResourcesID)AS C
                 WHERE C.Number < 2";
                var isGroupBuyInfo = con.Query(isGroupBuy, new { ResourcesID = resourcesIDs });

                foreach (DataRow model in ds.Tables[0].Rows)
                {
                    var isContanis = specification.FirstOrDefault(l => l.ResourcesID == model["ResourcesID"].ToString());
                    if (isContanis != null)
                    {
                        model["IsSpecification"] = (object)1;
                        model["SpecificationPrice"] = isContanis.Price;
                        model["ResourcesDisCountPrice"] = isContanis.DiscountAmount;
                        model["GroupBuyPrice"] = (object)isContanis.GroupBuyingPrice;
                    }


                    if (!String.IsNullOrEmpty(model["GroupBuyStartDate"].ToString()) && !String.IsNullOrEmpty(model["GroupBuyEndDate"].ToString()))
                    {
                        var IsGroupBuy = model["IsGroupBuy"].ToString();
                        var groupBuyStartDate = (DateTime?)model["GroupBuyStartDate"];
                        var groupBuyEndDate = (DateTime?)model["GroupBuyEndDate"];
                        var isContanisGroup = isGroupBuyInfo.FirstOrDefault(l => l.ResourcesID == model["ResourcesID"].ToString());
                        if (IsGroupBuy == "是"
                            && groupBuyStartDate.HasValue
                            && groupBuyStartDate.Value <= DateTime.Now
                            && groupBuyEndDate.HasValue
                            && groupBuyEndDate.Value >= DateTime.Now
                            && isContanisGroup != null)
                        {
                            model["Seckill"] = (object)1;
                            model["ResourcesSalePrice"] = (object)isContanisGroup.Price;
                            model["GroupBuyPrice"] = (object)isContanisGroup.GroupBuyingPrice;
                            model["ResourcesDisCountPrice"] = (object)isContanisGroup.DiscountAmount;
                        }
                    }
                }
            }
            return JSONHelper.FromString(ds.Tables[0]);
        }

        #endregion


        #region 获取商家商品列表

        /// <summary>
        /// 获取商家商品列表   GetResourcesList
        /// </summary>
        /// <param name="row"></param>
        /// BussId:商家ID【必填】
        /// PageIndex   默认1
        /// PageSize    默认4
        /// 返回：
        ///     ResourcesID 资源表ID
        ///     ResourcesName  商品名称
        ///     ResourcesSalePrice 销售价格
        ///     ResourcesDisCountPrice 优惠价格
        ///     GroupBuyPrice   团购价格
        ///     Img 商品图片
        ///     typename  商家性质
        /// <returns></returns>
        private string GetResourcesList(DataRow row)
        {
            var bussId = "";
            if (!row.Table.Columns.Contains("BussId") || string.IsNullOrEmpty(row["BussId"].ToString()))
            {
                return JSONHelper.FromString(false, "商家编码不能为空");
            }
            else
            {
                bussId = row["BussId"].ToString();
            }

            int PageIndex = 1;
            int PageSize = 10;

            if (row.Table.Columns.Contains("PageIndex") && AppGlobal.StrToInt(row["PageIndex"].ToString()) > 0)
            {
                PageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());
            }
            if (row.Table.Columns.Contains("PageSize") && AppGlobal.StrToInt(row["PageSize"].ToString()) > 0)
            {
                PageSize = AppGlobal.StrToInt(row["PageSize"].ToString());
            }

            int pageCount;
            int Counts;
            string sql = @"SELECT d.ResourcesID,d.ResourcesName,d.ResourcesSalePrice,d.ResourcesDisCountPrice,d.GroupBuyPrice,d.Img,d.IsGroupBuy,d.GroupBuyEndDate,d.GroupBuyStartDate,Seckill=0,Case 
                            when b.BussNature = '平台商城' then '101' 
                            when b.BussNature = '物管商城' then '102' 
                            when b.BussNature = '周边商家' then '103'	
                            else '103' end as typename, 
                            isnull((
		                        SELECT count(Quantity) FROM Tb_Charge_ReceiptDetail x 
		                        LEFT JOIN Tb_Charge_Receipt y ON x.ReceiptCode=y.Id 
		                        WHERE x.ResourcesID=d.ResourcesID AND y.IsPay='已付款'
		                    ),0) AS SaleCount
                            FROM Tb_Resources_Details as d inner join Tb_System_BusinessCorp as b on d.BussId = b.BussId 
                            where d.IsRelease='是' and d.IsStopRelease='否' and ISNULL(d.IsDelete,0)=0 and d.BussId=" + bussId;
            //查询商家的商品信息
            DataTable dataTable = GetList(out pageCount, out Counts, sql, PageIndex, PageSize, "ResourcesID", 1, "ResourcesID",
                PubConstant.BusinessContionString).Tables[0];



            if (dataTable.Rows.Count > 0)
            {
                List<Object> resourcesIDs = new List<Object>();
                foreach (DataRow rowData in dataTable.Rows)
                {
                    resourcesIDs.Add(rowData["ResourcesID"]);
                }
                using (SqlConnection con = new SqlConnection(PubConstant.BusinessContionString))
                {
                    String isGroupBuy = @" SELECT * FROM
                 (SELECT ROW_NUMBER() OVER(PARTITION  BY ResourcesID ORDER BY GroupBuyingPrice ASC) AS Number,* FROM Tb_ResourcesSpecificationsPrice  WHERE   ResourcesID IN @ResourcesID)AS C
                 WHERE C.Number < 2";
                    var isGroupBuyInfo = con.Query(isGroupBuy, new { ResourcesID = resourcesIDs });

                    foreach (DataRow model in dataTable.Rows)
                    {
                        var IsGroupBuy = model["IsGroupBuy"].ToString();
                        if (!String.IsNullOrEmpty(model["GroupBuyStartDate"].ToString()) && !String.IsNullOrEmpty(model["GroupBuyEndDate"].ToString()))
                        {
                            var groupBuyStartDate = (DateTime?)model["GroupBuyStartDate"];
                            var groupBuyEndDate = (DateTime?)model["GroupBuyEndDate"];
                            var isContanisGroup = isGroupBuyInfo.FirstOrDefault(l => l.ResourcesID == model["ResourcesID"].ToString());
                            if (IsGroupBuy == "是"
                                && groupBuyStartDate.HasValue
                                && groupBuyStartDate.Value <= DateTime.Now
                                && groupBuyEndDate.HasValue
                                && groupBuyEndDate.Value >= DateTime.Now
                                && isContanisGroup != null)
                            {
                                model["Seckill"] = (object)1;
                                model["ResourcesSalePrice"] = (object)isContanisGroup.Price;
                                model["GroupBuyPrice"] = (object)isContanisGroup.GroupBuyingPrice;
                                model["ResourcesDisCountPrice"] = (object)isContanisGroup.DiscountAmount;
                            }
                        }
                    }
                }
            }

            return JSONHelper.FromString(true, dataTable);
        }

        #endregion

        #region 联系我们
        /// <summary>
        /// 获取商家 联系我们 信息      GetInformationContactUs
        /// </summary>
        /// <param name="row"></param>
        /// BussId    商家编码【必填】
        /// 返回
        ///     ID:联系我们ID
        ///     BussId:商家ID
        ///     BussName：商家名称
        ///     Address：地址
        ///     Tel：办公电话
        ///     Phone：移动电话
        ///     Wechat：微信号
        ///     Map：地址
        /// <returns></returns>
        private string GetInformationContactUs(DataRow row)
        {
            var bussId = "";
            if (!row.Table.Columns.Contains("BussId") || string.IsNullOrEmpty(row["BussId"].ToString()))
            {
                return JSONHelper.FromString(false, "商家编码不能为空");
            }
            else
            {
                bussId = row["BussId"].ToString();
            }

            DataSet Cu = BussinessCommon.GetInformationContactUs(row["BussId"].ToString());
            if (Cu == null || Cu.Tables.Count <= 0 || Cu.Tables[0].Rows.Count <= 0)
            {
                return JSONHelper.FromString("");
            }
            return JSONHelper.FromString(Cu.Tables[0]);
            //StringBuilder sb = new StringBuilder();
            //sb.Append("{\"Result\":\"true\",");
            //sb.Append("\"data\":");
            //if (Cu == null)
            //{
            //    sb.Append("\"\"");
            //}
            //else
            //{
            //    sb.Append(JSONHelper.FromString(Cu));
            //}
            //sb.Append("}");

            //return sb.ToString();
        }
        #endregion

        #region 买家评价信息

        /// <summary>
        /// 获取商家的评价信息列表   GetResourcesCommodityEvaluationList
        /// </summary>
        /// <param name="row"></param>
        /// BussId:商家ID【必填】
        /// PageIndex   默认1
        /// PageSize    默认4
        /// 返回：
        ///     Name 商家用户名称
        ///     Star 商品评星
        ///     EvaluateDate 评价时间
        ///     UploadImg  上传图片
        ///     EvaluateContent 评价内容
        ///     AvgStar 综合评星
        /// <returns></returns>
        private string GetResourcesCommodityEvaluationList(DataRow row)
        {
            var bussId = "";
            if (!row.Table.Columns.Contains("BussId") || string.IsNullOrEmpty(row["BussId"].ToString()))
            {
                return JSONHelper.FromString(false, "商家编码不能为空");
            }
            else
            {
                bussId = row["BussId"].ToString();
            }

            int PageIndex = 1;
            int PageSize = 4;

            if (row.Table.Columns.Contains("PageIndex") && AppGlobal.StrToInt(row["PageIndex"].ToString()) > 0)
            {
                PageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());
            }
            if (row.Table.Columns.Contains("PageSize") && AppGlobal.StrToInt(row["PageSize"].ToString()) > 0)
            {
                PageSize = AppGlobal.StrToInt(row["PageSize"].ToString());
            }

            int pageCount;
            int Counts;
            var avgStar = "0";

            string sql = @"SELECT A.*,C.Name as UserName,D.ResourcesName,F.PropertyName FROM Tb_Resources_CommodityEvaluation as A 
                        Left Join Tb_Charge_Receipt as B on A.RpdCode =B.Id
                        Left Join Tb_System_User as C on B.UserId=C.UserCode
                        Left Join Tb_Resources_Details as D on A.ResourcesID=D.ResourcesID
                        Left Join Tb_Resources_PropertyRelation as E on D.ResourcesID=E.ResourcesID
                        Left Join Tb_Resources_Property as F on E.PropertyId=F.Id
                        where ISNULL(C.UserIsDelete,0)=0 and ISNULL(B.IsDelete,0)=0 and B.BussId=" + bussId;

            //综合评星
            string sql1 = @" SELECT Round(convert(float,Sum(Star))/convert(float,COUNT(*)),2) as AvgStar FROM Tb_Resources_CommodityEvaluation as A            
                        Left Join Tb_Charge_Receipt as B on A.RpdCode = B.Id
                        Left Join Tb_System_User as C on B.UserId = C.UserCode
                        where ISNULL(C.UserIsDelete, 0) = 0 and ISNULL(B.IsDelete, 0) = 0 and B.BussId=" + bussId;

            //DbHelperSQL.ConnectionString = PubConstant.UnifiedContionString.ToString();
            IDbConnection con = new SqlConnection(PubConstant.GetConnectionString("BusinessContionString"));

            avgStar = con.ExecuteReader(sql1, null, null, null, CommandType.Text).ToDataSet().Tables[0].Rows[0][0].ToString();

            //avgStar = DbHelperSQL.Query(string.Format(sql1));

            //查询商家的商品信息
            DataSet ds = BussinessCommon.GetList(out pageCount, out Counts, sql, PageIndex, PageSize, "EvaluateDate", 1, "Id", PubConstant.GetConnectionString("BusinessContionString"),
                "Name,Star,EvaluateDate,UploadImg,EvaluateContent");
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"Result\":\"true\",");
            sb.Append("\"data\":");
            sb.Append("{\"list\":");
            if (ds == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
            {
                sb.Append("[]");
            }
            else
            {
                sb.Append(JSONHelper.FromDataTable(ds.Tables[0]));
            }
            if (avgStar == "")//默认5分
            {
                avgStar = "5";
            }
            sb.Append(",\"AvgStar\":" + avgStar + "");
            sb.Append("}");
            sb.Append("}");

            return sb.ToString();
        }

        #endregion


        #region 商品收藏

        /// <summary>
        /// 收藏商品
        /// </summary>
        /// <param name="row"></param>
        /// UserId                  用户ID
        /// ResourcesID             商品ID
        /// BussId                  商家ID   
        /// <returns></returns>
        private string SetResourcesCollection(DataRow row)
        {
            if (!row.Table.Columns.Contains("BussId") || string.IsNullOrEmpty(row["BussId"].ToString()))
            {
                return JSONHelper.FromString(false, "商家编码不能为空");
            }
            if (!row.Table.Columns.Contains("UserId") || string.IsNullOrEmpty(row["UserId"].ToString()))
            {
                return JSONHelper.FromString(false, "用户编码不能为空");
            }
            if (!row.Table.Columns.Contains("ResourcesID") || string.IsNullOrEmpty(row["ResourcesID"].ToString()))
            {
                return JSONHelper.FromString(false, "商品编码不能为空");
            }

            string UserId = row["UserId"].ToString();
            string ResourcesId = row["ResourcesId"].ToString();
            int counts = 0;
            using (SqlConnection conn = new SqlConnection(PubConstant.BusinessContionString))
            {
                string sql = @" SELECT COUNT(*) FROM Tb_Collection WHERE  ResourcesID= @ResourcesID AND UserId = @UserId";
                counts = conn.QueryFirstOrDefault<int>(sql, new { UserId = UserId, ResourcesID = ResourcesId });
                if (counts > 0)
                {
                    sql = @"DELETE FROM Tb_Collection WHERE ResourcesID= @ResourcesID AND UserId = @UserId";
                    var delCounts = conn.Execute(sql, new { UserId = UserId, ResourcesID = ResourcesId });
                    if (delCounts > 0)
                    {
                        return JSONHelper.FromString(true, "取消收藏成功");
                    }
                    else
                    {
                        return JSONHelper.FromString(false, "取消收藏失败");
                    }
                    //已收藏 取消收藏
                }
                else
                {
                    Tb_Collection collection = new Tb_Collection();
                    string backstr = "";
                    try
                    {
                        collection.Id = Guid.NewGuid().ToString();
                        collection.UserId = row["UserId"].ToString();
                        collection.BussId = row["BussId"].ToString();
                        collection.ResourcesID = row["ResourcesID"].ToString();
                        collection.ReceiptDate = DateTime.Now;
                        collection.IsDelete = 0;

                        conn.Insert<Tb_Collection>(collection);
                        conn.Dispose();
                    }
                    catch (Exception ex)
                    {
                        backstr = ex.Message;
                    }
                    if (backstr == "")
                    {
                        return JSONHelper.FromString(true, "收藏成功");
                    }
                    else
                    {
                        return JSONHelper.FromString(false, "收藏失败" + backstr);
                    }

                }
            }



        }

        /// <summary>
        /// 获取收藏商品   GetResourcesCollection
        /// </summary>
        /// <param name="row"></param>
        /// UserId                      用户编码【必填】
        /// ResourcesName               商品名称
        /// PageIndex                   页码默认1
        /// PageSize                    每页条数默认4
        /// 返回
        /// Id                          收藏ID
        /// ResourcesID                 商品ID
        /// ResourcesName               商品名称
        /// ResourcesUnit               商品单位
        /// ResourcesSalePrice          
        /// ResourcesDisCountPrice
        /// GroupBuyPrice
        /// Img                         图片
        /// BussName                    商家名称
        /// ReceiptDate                 收藏时间
        /// <returns></returns>
        private string GetResourcesCollection(DataRow row)
        {
            if (!row.Table.Columns.Contains("UserId") || string.IsNullOrEmpty(row["UserId"].ToString()))
            {
                return JSONHelper.FromString(false, "用户编码不能为空");
            }
            string UserId = row["UserId"].ToString();
            string ResourcesName = "";
            if (row.Table.Columns.Contains("ResourcesName") && row["ResourcesName"].ToString() != "")
            {
                ResourcesName = row["ResourcesName"].ToString();
            }

            int PageIndex = 1;
            int PageSize = 4;

            if (row.Table.Columns.Contains("PageIndex") && AppGlobal.StrToInt(row["PageIndex"].ToString()) > 0)
            {
                PageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());
            }
            if (row.Table.Columns.Contains("PageSize") && AppGlobal.StrToInt(row["PageSize"].ToString()) > 0)
            {
                PageSize = AppGlobal.StrToInt(row["PageSize"].ToString());
            }

            int pageCount;
            int Counts;

            using (IDbConnection con = new SqlConnection(PubConstant.GetConnectionString("BusinessContionString")))
            {
                String sql = $@"SELECT * FROM (
                        SELECT ROW_NUMBER() OVER( ORDER BY ReceiptDate DESC) AS RowId, IsSpecification=0,SpecificationPrice=0,Seckill=0,
                         c.Id,d.ResourcesID, d.ResourcesName,d.ResourcesUnit,d.ResourcesSalePrice,d.ResourcesDisCountPrice,d.GroupBuyPrice,d.Img,b.BussName,c.ReceiptDate,d.IsGroupBuy,d.GroupBuyStartDate,d.GroupBuyEndDate
                         from Tb_Collection  as c 
                         inner join Tb_Resources_Details as d on c.ResourcesID=d.ResourcesID 
                        inner join Tb_System_BusinessCorp as b on c.BussId=b.BussId 
                        where  ISNULL(c.IsDelete,0)=0 and
                        UserId=@userId  and ISNULL(d.IsDelete,0)=0) AS T 
                WHERE T.RowId >(@PageIndex-1)*@PageSize AND T.RowId<=(@PageIndex*@PageSize);"; ;

                var data = con.Query(sql, new { userId = UserId, PageIndex = PageIndex, PageSize = PageSize });

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
                return JsonConvert.SerializeObject(new
                {
                    Result = "true",
                    data = data
                });
            }
        }


        private string GetResourcesIsCollected(DataRow row)
        {
            if (!row.Table.Columns.Contains("UserId") || string.IsNullOrEmpty(row["UserId"].ToString()))
            {
                return JSONHelper.FromString(false, "用户编码不能为空");
            }
            if (!row.Table.Columns.Contains("ResourcesId") || string.IsNullOrEmpty(row["ResourcesId"].ToString()))
            {
                return JSONHelper.FromString(false, "商品编号不能为空");
            }
            string UserId = row["UserId"].ToString();
            string ResourcesId = row["ResourcesId"].ToString();

            int counts = 0;
            using (SqlConnection conn = new SqlConnection(PubConstant.BusinessContionString))
            {
                string sql = @" SELECT COUNT(*) FROM Tb_Collection WHERE  ResourcesID= @ResourcesID AND UserId = @UserId";
                counts = conn.QueryFirstOrDefault<int>(sql, new { UserId = UserId, ResourcesID = ResourcesId });
                if (counts > 0)
                {
                    return JSONHelper.FromString(true, "收藏中");
                }
                else
                {
                    return JSONHelper.FromString(true, "未收藏");
                }
            }
        }


        /// <summary>
        /// 取消收藏
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string DelResourcesCollection(DataRow row)
        {
            if (!row.Table.Columns.Contains("Id") || string.IsNullOrEmpty(row["Id"].ToString()))
            {
                return JSONHelper.FromString(false, "收藏编码不能为空");
            }

            string Id = row["Id"].ToString();
            int counts = 0;
            using (SqlConnection conn = new SqlConnection(PubConstant.BusinessContionString))
            {
                string sql = @" DELETE FROM Tb_Collection WHERE  Id= @Id ";
                counts = conn.Execute(sql, new { Id = Id });
                if (counts > 0)
                {
                    return JSONHelper.FromString(true, "取消收藏成功");
                }
                else
                {
                    return JSONHelper.FromString(false, "取消收藏失败");
                }
            }
        }



        #endregion


    }
}
