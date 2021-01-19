using MobileSoft.Model.Unified;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dapper;
using DapperExtensions;
using MobileSoft.DBUtility;
using MobileSoft.Common;
using Model.Model.Buss;


namespace Business
{
    /// <summary>
    /// 商家系统共用类
    /// </summary>
    public static class BussinessCommon
    {
        /// <summary>
        /// 获取商品信息
        /// </summary>
        /// <param name="PageCount"></param>
        /// <param name="Counts"></param>
        /// <param name="StrCondition"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="SortField"></param>
        /// <param name="Sort"></param>
        /// <param name="ID"></param>
        /// <param name="CorpID"></param>
        /// <param name="CommunityId"></param>
        /// <param name="SearchType"></param>
        /// <param name="SearchColumnName"></param>
        /// <returns></returns>
        internal static DataSet GetResourcesDetails(out int PageCount, out int Counts, string StrCondition, int PageIndex, int PageSize, string SortField, int Sort, string ID, string CorpID, string CommunityId, string SearchType, string SearchColumnName)
        {
            PageCount = 0;
            Counts = 0;
            IDbConnection con = new SqlConnection(PubConstant.GetConnectionString("BusinessContionString"));
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@FldName", "*");
            parameters.Add("@PageSize", PageSize);
            parameters.Add("@PageIndex", PageIndex);
            parameters.Add("@FldSort", SortField);
            parameters.Add("@Sort", Sort);

            parameters.Add("@StrCondition", StrCondition);
            parameters.Add("@Id", ID);
            parameters.Add("@PageCount", PageCount);
            parameters.Add("@Counts", Counts);
            parameters.Add("@CorpID", CorpID);

            parameters.Add("@CommunityId", CommunityId);
            parameters.Add("@SearchType", SearchType);
            parameters.Add("@SearchColumnName", SearchColumnName);

            DataSet Ds = con.ExecuteReader("Proc_Tb_Resources_Details_TurnPage", parameters, null, null, CommandType.StoredProcedure).ToDataSet();

            return Ds;
        }


        /// <summary>
        /// 根据选择商品类型列出商品数据
        /// </summary>
        /// <param name="PageCount"></param>
        /// <param name="Counts"></param>
        /// <param name="StrCondition"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="SortField"></param>
        /// <param name="Sort"></param>
        /// <param name="ID"></param>
        /// <param name="CorpID"></param>
        /// <param name="CommunityId"></param>
        /// <param name="SearchType"></param>
        /// <param name="SearchColumnName"></param>
        /// <returns></returns>
        internal static DataSet GetResourcesTypeList(out int PageCount, out int Counts, string StrCondition, int PageIndex, int PageSize, string SortField, int Sort, string ID, string CorpID, string CommunityId, string SearchType, string SearchColumnName)
        {
            PageCount = 0;
            Counts = 0;
            IDbConnection con = new SqlConnection(PubConstant.GetConnectionString("BusinessContionString"));
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@FldName", "*");
            parameters.Add("@PageSize", PageSize);
            parameters.Add("@PageIndex", PageIndex);
            parameters.Add("@FldSort", SortField);
            parameters.Add("@Sort", Sort);

            parameters.Add("@StrCondition", StrCondition);
            parameters.Add("@Id", ID);
            parameters.Add("@PageCount", PageCount);
            parameters.Add("@Counts", Counts);
            parameters.Add("@CorpID", CorpID);

            parameters.Add("@CommunityId", CommunityId);
            parameters.Add("@SearchType", SearchType);
            parameters.Add("@SearchColumnName", SearchColumnName);

            DataSet Ds = con.ExecuteReader("Proc_Tb_Resources_Details_TurnPage_Type", parameters, null, null, CommandType.StoredProcedure).ToDataSet();

            return Ds;
        }


        internal static DataSet GetResourcesDetailsAll(out int PageCount, out int Counts, string StrCondition, int PageIndex, int PageSize, string SortField, int Sort, string ID, string CorpID, string CommunityId, string SearchType, string SearchColumnName)
        {
            PageCount = 0;
            Counts = 0;
            IDbConnection con = new SqlConnection(PubConstant.GetConnectionString("BusinessContionString"));
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@FldName", "*");
            parameters.Add("@PageSize", PageSize);
            parameters.Add("@PageIndex", PageIndex);
            parameters.Add("@FldSort", SortField);
            parameters.Add("@Sort", Sort);

            parameters.Add("@StrCondition", StrCondition);
            parameters.Add("@Id", ID);
            parameters.Add("@PageCount", PageCount);
            parameters.Add("@Counts", Counts);
            parameters.Add("@CorpID", CorpID);

            parameters.Add("@CommunityId", CommunityId);
            parameters.Add("@SearchType", SearchType);
            parameters.Add("@SearchColumnName", SearchColumnName);

            DataSet Ds = con.ExecuteReader("Proc_Tb_Resources_Details_TurnPage_All", parameters, null, null, CommandType.StoredProcedure).ToDataSet();

            return Ds;
        }




        //获取排序值
        internal static string GetSortStr(int sortFile)
        {
            string str = "Asc";
            switch (sortFile)
            {
                case 1:
                    str = "Desc";
                    break;
                case 0:
                    str = "Asc";
                    break;
                default:
                    str = "Asc";
                    break;
            }
            return str;
        }

        /// <summary>
        /// 查询购物车
        /// </summary>
        /// <param name="UserId">用户编码</param>
        /// <returns></returns>
        internal static Tb_ShoppingCar GetShoppingCarModel(string UserId)
        {
            IDbConnection con = new SqlConnection(PubConstant.GetConnectionString("BusinessContionString"));
            Tb_ShoppingCar m = con.Query<Tb_ShoppingCar>("select * from dbo.Tb_ShoppingCar where UserId=@UserId and isnull( IsDelete,0)=0", new { UserId = UserId }).SingleOrDefault<Tb_ShoppingCar>();
            return m;
        }

        /// <summary>
        /// 商品查询
        /// </summary>
        /// <param name="Id">ID查询商品</param>
        /// <returns></returns>
        internal static Tb_Resources_Details GetResourcesModel(string Id)
        {
            IDbConnection con = new SqlConnection(PubConstant.GetConnectionString("BusinessContionString"));
            Tb_Resources_Details m = con.Query<Tb_Resources_Details>("select * from dbo.Tb_Resources_Details where ResourcesID=@UserId and isnull( IsDelete,0)=0", new { UserId = Id }).SingleOrDefault<Tb_Resources_Details>();
            return m;
        }

        /// <summary>
        /// 获取联系我们信息
        /// </summary>
        /// <param name="BussId">商家编码</param>
        /// <returns></returns>
        public static DataSet GetInformationContactUs(string BussId)
        {
            IDbConnection con = new SqlConnection(PubConstant.GetConnectionString("BusinessContionString"));
            //Tb_System_BusinessCorp m = con.Query<Tb_System_BusinessCorp>("select BussId, BussName, BussAddress, BussLinkMan, BussMobileTel, BussWorkedTel, BussWeiXin, LogoImgUrl from dbo.Tb_System_BusinessCorp where BussId=@BussId ", new { BussId = BussId }).SingleOrDefault<Tb_System_BusinessCorp>();

            DataSet ds = con.ExecuteReader("select BussId, BussName, BussAddress, BussLinkMan, BussMobileTel, BussWorkedTel, BussWeiXin, LogoImgUrl from dbo.Tb_System_BusinessCorp where BussId='" + BussId + "'").ToDataSet();
            return ds;
        }


        /// <summary>
        /// 获取购物车明细
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        internal static List<Tb_ShoppingDetailed> GetShoppingDetailedList(string Id)
        {
            IDbConnection con = new SqlConnection(PubConstant.GetConnectionString("BusinessContionString"));
            List<Tb_ShoppingDetailed> list = con.Query<Tb_ShoppingDetailed>("select * from Tb_ShoppingDetailed  where ShoppingId=@ShoppingId and ISNULL(IsDelete,0)=0", new { ShoppingId = Id }).ToList<Tb_ShoppingDetailed>();
            return list;
        }

        /// <summary>
        /// 获取单个购物车明细
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        internal static Tb_ShoppingCar GetShoppingDetailedModel(string Id)
        {
            IDbConnection con = new SqlConnection(PubConstant.GetConnectionString("BusinessContionString"));
            Tb_ShoppingCar m = con.Query<Tb_ShoppingCar>("select * from Tb_ShoppingCar where Id=@Id and ISNULL(IsDelete,0)=0", new { Id = Id }).SingleOrDefault<Tb_ShoppingCar>();
            return m;
        }

        /// <summary>
        /// 查询购物车中是否有此物品
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="ResourcesID"></param>
        /// <returns></returns>
        internal static List<Tb_ShoppingCar> GetShoppingCheck(string Id, string ResourcesID)
        {
            IDbConnection con = new SqlConnection(PubConstant.GetConnectionString("BusinessContionString"));
            List<Tb_ShoppingCar> m = con.Query<Tb_ShoppingCar>("select * from Tb_ShoppingCar where UserId=@UserId and ResourcesID=@ResourcesID and ISNULL(IsDelete,0)=0", new { UserId = Id, ResourcesID = ResourcesID }).ToList<Tb_ShoppingCar>();
            return m;
        }

        /// <summary>
        /// 查询购物车明细中规格
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        internal static DataSet GetShoppingDetailedCheck(string Id, string Propertys)
        {
            IDbConnection con = new SqlConnection(PubConstant.GetConnectionString("BusinessContionString"));
            DataSet ds = con.ExecuteReader("select * from Tb_ShoppingDetailed where ShoppingId='" + Id + "' and PropertysId in (" + Propertys + ")", null, null, null, CommandType.Text).ToDataSet();
            return ds;
        }

        /// <summary>
        /// 获取购物车明细试图
        /// </summary>
        /// <param name="sqlstr"></param>
        /// <returns></returns>
        internal static DataTable GetShoppingDetailedView(string sqlstr)
        {
            IDbConnection con = new SqlConnection(PubConstant.GetConnectionString("BusinessContionString"));
            DataSet ds = con.ExecuteReader("SELECT S.Id,S.ShoppingId,S.Number,S.SubtotalMoney,S.Propertys,R.* FROM Tb_ShoppingDetailed AS S INNER JOIN Tb_Resources_Details AS R ON S.ResourcesID = R.ResourcesID where  ISNULL(S.IsDelete,0)=0 " + sqlstr, null, null, null, CommandType.Text).ToDataSet();
            return ds.Tables[0];
        }



        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="PageCount">总页数</param>
        /// <param name="Counts">总条数</param>
        /// <param name="StrCondition">执行语句</param>
        /// <param name="PageIndex">第几页</param>
        /// <param name="PageSize">每页多少条</param>
        /// <param name="SortField">排序字段</param>
        /// <param name="Sort">升序/降序</param>
        /// <param name="ID">主键</param>
        /// <param name="ConStr">链接字符串</param>
        /// <param name="FldName">显示字段</param>
        /// <returns></returns>
        internal static DataSet GetList(out int PageCount, out int Counts, string StrCondition, int PageIndex, int PageSize, string SortField, int Sort, string ID, string ConStr, string FldName)
        {
            PageCount = 0;
            Counts = 0;
            IDbConnection con = new SqlConnection(ConStr);
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@FldName", FldName);
            parameters.Add("@PageSize", PageSize);
            parameters.Add("@PageIndex", PageIndex);
            parameters.Add("@FldSort", SortField);
            parameters.Add("@Sort", Sort);

            parameters.Add("@StrCondition", StrCondition);
            parameters.Add("@Id", ID);
            parameters.Add("@PageCount", PageCount);
            parameters.Add("@Counts", Counts);


            DataSet Ds = con.ExecuteReader("Proc_System_TurnPage", parameters, null, null, CommandType.StoredProcedure).ToDataSet();
            con.Dispose();
            return Ds;
        }

        /// <summary>
        /// 获取订单实体
        /// </summary>
        /// <param name="ReceiptCode"></param>
        /// <returns></returns>
        public static Tb_Charge_Receipt GetChargeReceiptModel(string ReceiptCode)
        {
            IDbConnection con = new SqlConnection(PubConstant.GetConnectionString("BusinessContionString"));
            Tb_Charge_Receipt m = con.Query<Tb_Charge_Receipt>("select * from Tb_Charge_Receipt where Id=@ReceiptCode", new { ReceiptCode = ReceiptCode }).SingleOrDefault<Tb_Charge_Receipt>();
            return m;
        }

        /// <summary>
        /// 获取收货地址
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        internal static Tb_User_Address GetAddressModel(string Id)
        {
            IDbConnection con = new SqlConnection(PubConstant.GetConnectionString("BusinessContionString"));
            Tb_User_Address m = con.Query<Tb_User_Address>("select * from Tb_User_Address where Id=@Id", new { Id = Id }).SingleOrDefault<Tb_User_Address>();
            return m;
        }

        /// <summary>
        /// 获取订单编号 待实现
        /// </summary>
        /// <returns></returns>
        internal static string GetReceiptSign()
        {
            //待确定编码规则后，再实现。
            return Guid.NewGuid().ToString();
        }

        /// <summary>
        /// 拆分并重组购物车明细查询条件
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        internal static string GetShoppingDetailedIdS(string str)
        {
            string[] str_sprint = str.Split(',');
            string breakStr = "";
            if (str_sprint.Length > 0)
            {
                for (int i = 0; i < str_sprint.Length; i++)
                {
                    if (i == 0)
                    {
                        breakStr = "'" + str_sprint[0] + "'";
                    }
                    else
                    {
                        breakStr += ",'" + str_sprint[i] + "'";
                    }
                }
            }
            return breakStr;
        }

        /// <summary>
        /// 返回属性字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns>‘value’，‘value’，‘value’，‘value’</returns>
        internal static string GetShoppingPropertys(string str)
        {
            string breakStr = "";
            if (str == "")
            {
                return "";
            }
            string[] str_sprint = str.Split(',');

            if (str_sprint.Length > 0)
            {
                for (int i = 0; i < str_sprint.Length; i++)
                {
                    string[] s_Proper = str_sprint[i].Split(':');
                    if (i == 0)
                    {
                        breakStr = "'" + s_Proper[0] + "'";
                    }
                    else
                    {
                        breakStr += ",'" + s_Proper[0] + "'";
                    }
                }
            }
            return breakStr;
        }

        /// <summary>
        /// 获取订单明细
        /// </summary>
        internal static DataSet GetShoppingDetailed(string str)
        {
            IDbConnection con = new SqlConnection(PubConstant.GetConnectionString("BusinessContionString"));
            string sql = @"select d.ShoppingId, d.RpdCode,d.Quantity,isnull(d.OffsetMoney,0) as OffsetMoney,r.IsGroupBuy,R.GroupBuyEndDate,R.GroupBuyStartDate,
                            isnull(d.OffsetMoney2,0) as OffsetMoney2,isnull(d.IsEvaluated,0) as IsEvaluated,
                            r.ResourcesID,d.SalesPrice as ResourcesSalePrice,d.DiscountPrice as ResourcesDisCountPrice,
                            isnull(d.GroupPrice,0) as GroupPrice,r.IsSupportCoupon, r.MaximumCouponMoney,r.ResourcesName,
                            case charindex(',',isnull(r.Img,'')) WHEN 0 then r.Img else left(r.Img,(charindex(',',r.Img)-1)) end AS Img,
                            d.RpdMemo,isnull(d.DetailAmount,0) as DetailAmount ,
							D.UnitPrice
                            from Tb_Charge_ReceiptDetail as d inner join Tb_Resources_Details as r on d.ResourcesID = r.ResourcesID
                            where ISNULL(r.IsDelete, 0)= 0 and ISNULL( d.RpdIsDelete,0) = 0 and  d.ReceiptCode='" + str + "'";
            return con.ExecuteReader(sql, null, null, null, CommandType.Text).ToDataSet();
        }

        /// <summary>
        /// 仅用于未付款
        /// </summary>
        internal static DataSet GetShoppingDetailed_New(string str)
        {
            IDbConnection con = new SqlConnection(PubConstant.GetConnectionString("BusinessContionString"));
            string sql = @"select d.ShoppingId, d.RpdCode,d.Quantity,isnull(d.OffsetMoney,0) as OffsetMoney,
                            isnull(d.OffsetMoney2,0) as OffsetMoney2,isnull(d.IsEvaluated,0) as IsEvaluated,
                            r.ResourcesID,d.SalesPrice As ResourcesSalePrice, d.DiscountPrice as ResourcesDisCountPrice,r.IsGroupBuy,R.GroupBuyEndDate,R.GroupBuyStartDate,
                            isnull(d.GroupPrice,0) as GroupPrice ,r.IsSupportCoupon,r.MaximumCouponMoney,r.ResourcesName,
                            case charindex(',',isnull(r.Img,'')) WHEN 0 then r.Img else left(r.Img,(charindex(',',r.Img)-1)) end AS Img,
                            d.RpdMemo,isnull(d.DetailAmount,0) as DetailAmount ,D.UnitPrice
                            from Tb_Charge_ReceiptDetail as d inner join Tb_Resources_Details as r on d.ResourcesID = r.ResourcesID 
                            where ISNULL(r.IsDelete, 0)= 0 and ISNULL(d.RpdIsDelete,0) = 0 And d.ReceiptCode='" + str + "'";

            return con.ExecuteReader(sql, null, null, null, CommandType.Text).ToDataSet();
        }

        /// <summary>
        /// 获取商品属性例如颜色
        /// </summary>
        /// <param name="BussId"></param>
        /// <param name="ResourcesID"></param>
        /// <returns></returns>
        internal static DataSet GetResourcesProperty(string BussId, string ResourcesID)
        {
            IDbConnection con = new SqlConnection(PubConstant.GetConnectionString("BusinessContionString"));
            string sql = "select r.BussId,r.ResourcesID,p.PropertyName,p.Id from Tb_Resources_PropertyRelation as r inner join Tb_Resources_Property as p on r.PropertyId = p.Id  where r.BussId='" + BussId + "'  and  r.ResourcesID='" + ResourcesID + "'  order by p.Sort asc  ";
            return con.ExecuteReader(sql, null, null, null, CommandType.Text).ToDataSet();
        }

        /// <summary>
        /// 获取商品规格例如：红、黄、蓝
        /// </summary>
        /// <param name="PrepertyId"></param>
        /// <returns></returns>
        internal static DataSet GetGetResourcesProperty(string PrepertyId)
        {
            IDbConnection con = new SqlConnection(PubConstant.GetConnectionString("BusinessContionString"));
            string sql = "select Id,PropertyId,SpecName from Tb_Resources_Specifications where PropertyId='" + PrepertyId + "' order by Sort asc";
            return con.ExecuteReader(sql, null, null, null, CommandType.Text).ToDataSet();
        }

        /// <summary>
        /// 获取所有存在商品的商品类别
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        internal static DataSet GetResourcesType(string sql)
        {
            IDbConnection con = new SqlConnection(PubConstant.GetConnectionString("BusinessContionString"));
            DataSet ds = con.ExecuteReader(sql, null, null, null, CommandType.Text).ToDataSet();
            return ds;
        }
    }
}
