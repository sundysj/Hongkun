using Dapper;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Service.BusinessDetailsHtml
{
    public partial class BusinessDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    string ResourcesID = "";
                    if (Request.QueryString["ResourcesID"] != null)
                    {
                        ResourcesID = Request.QueryString["ResourcesID"];
                    }
                    else
                    {
                        ResourcesID = "c8bd2e57-b3fe-49cd-ab12-8f82915f1d4a";
                        //ResourcesID = "e2888658-63db-4cfd-ac9b-1a78c45d0986";
                        //ResourcesID = "4a957929-5724-4504-a98b-eb404ca3b40a";
                    }
                    DataTable dt = GetResourcesDetailsModel(ResourcesID);
                    string BussId = "";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        //动态添加商品logo
                        if (dt.Rows[i]["Img"] != null)
                        {
                            string[] imgs = dt.Rows[i]["Img"]?.ToString().Split(',');
                            if (imgs.Length > 0)
                            {
                                string li = "";
                                for (int j = 0; j < imgs.Length; j++)
                                {
                                    li += "<li data-background=" + imgs[j] + " />";
                                    //break;
                                }
                                this.BusinessLogoUl.InnerHtml = li;
                            }
                        }
                        //动态添加商品详情信息
                        if (dt.Rows[i]["ResourcesSimple"] != null)
                        {
                            int size = 0;
                            size = dt.Rows[i]["ResourcesSimple"].ToString().Length;
                            if (size > 13)
                            {
                                this.Name.InnerText = dt.Rows[i]["ResourcesSimple"]?.ToString().Substring(0, 13) + "...";
                            }
                            else
                            {
                                this.Name.InnerText = dt.Rows[i]["ResourcesSimple"]?.ToString();
                            }
                        }
                        //this.Name.InnerText = dt.Rows[i]["ResourcesSimple"].ToString();
                        if (dt.Rows[i]["BussNature"] != null)
                        {
                            if (dt.Rows[i]["BussNature"].ToString() == "物管商城")
                            {
                                this.Nature.InnerText = "生活馆";
                            }
                            if (dt.Rows[i]["BussNature"].ToString() == "周边商家")
                            {
                                this.Nature.InnerText = "便利圈";
                            }
                            if (dt.Rows[i]["BussNature"].ToString() == "平台商城")
                            {
                                this.Nature.InnerText = "品牌店";
                            }
                        }
                        else
                        {
                            this.Nature.InnerText = "";
                        }
                        //是否支持优惠券
                        if (dt.Rows[i]["IsSupportCoupon"] != null)
                        {
                            if (dt.Rows[i]["IsSupportCoupon"].ToString() == "是")
                            {
                                this.IsSupportCoupon.InnerText = "优惠券";
                            }
                        }
                        else
                        {
                            this.IsSupportCoupon.InnerText = "";
                        }
                        //Convert.ToDecimal(Convert.ToDecimal(dt.Rows[i]["ResourcesSalePrice"].ToString()) - Convert.ToDecimal(dt.Rows[i]["ResourcesDisCountPrice"].ToString())).ToString();
                        this.liDisCountPrice.InnerText = "￥" + Convert.ToDecimal(Convert.ToDecimal(dt.Rows[i]["ResourcesSalePrice"]?.ToString()) - Convert.ToDecimal(dt.Rows[i]["ResourcesDisCountPrice"]?.ToString())).ToString();
                        if (dt.Rows[i]["ResourcesName"] != null)
                        {
                            int size = 0;
                            size = dt.Rows[i]["ResourcesName"].ToString().Length;
                            if (size > 20)
                            {
                                this.AdContent.InnerText = dt.Rows[i]["ResourcesName"]?.ToString().Substring(0, 19) + "...";
                            }
                            else
                            {
                                this.AdContent.InnerText = dt.Rows[i]["ResourcesName"]?.ToString();
                            }
                        }
                        this.liSalePrice.InnerHtml = "￥" + dt.Rows[i]["ResourcesSalePrice"]?.ToString();
                        this.BusinessDetaildivImg.InnerHtml = dt.Rows[i]["ReleaseAdContent"]?.ToString();
                        //if (!string.IsNullOrEmpty(dt.Rows[i]["ReleaseAdContent"].ToString()))
                        //{
                        //    if (dt.Rows[i]["ReleaseAdContent"].ToString().Trim().Contains("暂无") == true)
                        //    {
                        //        this.BusinessDetaildivImg1.InnerHtml = dt.Rows[i]["ReleaseAdContent"].ToString();
                        //        this.BusinessDetaildivImg1.Style.Add("display", "block");
                        //        this.BusinessDetaildivImg.Style.Add("display", "none");
                        //    }
                        //    else
                        //    {
                        //        this.BusinessDetaildivImg.InnerHtml = dt.Rows[i]["ReleaseAdContent"].ToString();
                        //        this.BusinessDetaildivImg.Style.Add("display", "block");
                        //        this.BusinessDetaildivImg1.Style.Add("display", "none");
                        //    }
                        //}

                        //商家id
                        BussId = dt.Rows[i]["BussId"]?.ToString();
                        //商品ID
                        ResourcesID = dt.Rows[i]["ResourcesID"]?.ToString();
                    }

                    //获取下单数量 
                    if (GetCorpIDByResources(ResourcesID) == "1509")
                    {
                        string OrderNumStr = GetResourcesCount(ResourcesID);
                        this.OrderNum.InnerHtml = OrderNumStr;
                    }
                    else
                    {
                        try
                        {
                            li7.Visible = false;
                        }
                        catch (Exception ex)
                        {
                            
                        }
                    }


                    //获取同店商品
                    DataTable dtTD = GetResourcesList(BussId, ResourcesID);
                    string divStr = "";
                    this.HidLength.Value = dtTD.Rows.Count.ToString();
                    for (int i = 0; i < dtTD.Rows.Count; i++)
                    {
                        string[] strImg = dtTD.Rows[i]["Img"].ToString().Split(',');
                        divStr += "<div>" +
                                     "<ul>" +
                                         "<li>" +
                                             "<a href='BusinessDetails.aspx?ResourcesID=" + dtTD.Rows[i]["ResourcesID"] + "'><img src = '" + (strImg.Length > 0 ? strImg[0] : "") + "' /></a>" +
                                          "</li >" +
                                          "<li class=\"liHeight\">" +
                                               "<a href='BusinessDetails.aspx?ResourcesID=" + dtTD.Rows[i]["ResourcesID"] + "'><span id='ResourcesName" + i + "'> " + dtTD.Rows[i]["ResourcesName"] + " </span></a><br/>" +
                                          "</li>" +
                                          //"<li id=\"liLength" + i + "\" style=\"dispaly:none\"></li>" +
                                          "<li>" +
                                               "<span id = \"BusinessDetaildivTableSpan\"/>￥" + Convert.ToDecimal(Convert.ToDecimal(dtTD.Rows[i]["ResourcesSalePrice"]?.ToString()) - Convert.ToDecimal(dtTD.Rows[i]["ResourcesDisCountPrice"]?.ToString())).ToString() + " </span>" +
                                               "<span><del id = \"BusinessDetaildivTableDel\">￥" + dtTD.Rows[i]["ResourcesSalePrice"] + " </del></span>" +
                                          "</li>" +
                                    "</ul>" +
                                 "</div>";
                    }
                    this.BusinessDetaildivTable.InnerHtml = divStr;

                    //添加商品评论
                    DataTable dtPL = GetResourcesEvaluation(ResourcesID);
                    string divStrPL = "";
                    string EvaluateContent = "";
                    if (dtPL.Rows.Count > 0)
                    {
                        divStrPL = "<div id=\"BuyerEvaluationBorber\"></div>";
                        for (int i = 0; i < dtPL.Rows.Count; i++)
                        {
                            //根据用户评星判断显示星级
                            string str = dtPL.Rows[i]["Star"]?.ToString();
                            string Star = "";
                            if (str == "1.00")
                            {
                                Star = "<img src = '../Images/x1.png' />";
                            }
                            if (str == "2.00")
                            {
                                Star = "<img src = '../Images/x2.png' />";
                            }
                            if (str == "3.00")
                            {
                                Star = "<img src = '../Images/x3.png' />";
                            }
                            if (str == "4.00")
                            {
                                Star = "<img src = '../Images/x4.png' />";
                            }
                            if (str == "5.00")
                            {
                                Star = "<img src = '../Images/x5.png' />";
                            }
                            if (dtPL.Rows[i]["EvaluateContent"] != null)
                            {
                                int size = 0;
                                size = dtPL.Rows[i]["EvaluateContent"].ToString().Length;
                                if (size > 30)
                                {
                                    EvaluateContent = dtPL.Rows[i]["EvaluateContent"]?.ToString().Substring(0, 29) + "...";
                                }
                                else
                                {
                                    EvaluateContent = dtPL.Rows[i]["EvaluateContent"]?.ToString();
                                }
                            }
                            divStrPL += "<div class=\"BuyerEvaluationdiv\">" +
                                        "<div class=\"DivImg\">" +
                                        //"<img src = '../Images/9.png' />" +
                                        "<img src = '" + dtPL.Rows[i]["UserPic"] + "' />" +
                                        "</div>" +
                                        "<div class=\"DivUl\">" +
                                              "<ul>" +
                                                  "<li>" + dtPL.Rows[i]["NickName"] + "" +
                                                      "<span>" + dtPL.Rows[i]["EvaluateDate"] + "</span >" +
                                                   "</li>" +
                                                   "<li>" +
                                                        Star +
                                                   "</li>" +
                                                   "<li class=\"liContent\">" + EvaluateContent + "</li>" +
                                             //"<li>" + dtPL.Rows[i][""] + "</li>" +
                                             "</ul>" +
                                        "</div>" +
                                     "</div>";
                        }
                    }
                    else
                    {
                        divStrPL = "<p>暂无评论</p>";
                    }
                    this.BuyerEvaluation.InnerHtml = divStrPL;
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }

        /// <summary>
        /// 获取商品详情   GetResourcesDetailsModel
        /// </summary>
        /// <param name="row"></param>
        /// ResourcesID         商品编号【必填】
        /// 返回：
        /// <returns></returns>
        //private DataTable GetResourcesDetailsModel(DataRow row)
        private DataTable GetResourcesDetailsModel(string ResourcesID)
        {
            try
            {
                //if (!row.Table.Columns.Contains("ResourcesID") || string.IsNullOrEmpty(row["ResourcesID"].ToString()))
                //{
                //    return JSONHelper.FromString(false, "商品编号不能为空");
                //}
                IDbConnection con = new SqlConnection(PubConstant.GetConnectionString("BusinessContionString"));
                string str = " select R.*,b.BussNature from Tb_Resources_Details as r inner join Tb_System_BusinessCorp as b on r.BussId = b.BussId where ResourcesID='" + ResourcesID + "'";
                DataSet ds = con.ExecuteReader(str, null, null, null, CommandType.Text).ToDataSet();
                if (ds == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                {
                    //return JSONHelper.FromString(false, "未找到此商品");
                }
                return ds.Tables[0];
                //return JSONHelper.FromString(ds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取商家商品列表   GetResourcesList
        /// </summary>
        /// <param name="row"></param>
        /// BussId:商家ID【必填】
        /// PageIndex   默认1
        /// PageSize    默认4
        /// 返回：
        /// BussId 商家ID
        /// ResourcesID 资源表ID
        /// ResourcesName  商品名称
        /// Img 商品图片
        /// <returns></returns>
        //private DataTable GetResourcesList(DataRow row)
        private DataTable GetResourcesList(string BussId, string ResourcesID)
        {
            try
            {
                //var bussId = "";
                //if (!row.Table.Columns.Contains("BussId") || string.IsNullOrEmpty(row["BussId"].ToString()))
                //{
                //    return JSONHelper.FromString(false, "商家编码不能为空");
                //}
                //else
                //{
                //    bussId = row["BussId"].ToString();
                //}

                int PageIndex = 1;
                int PageSize = 4;

                //if (row.Table.Columns.Contains("PageIndex") && AppGlobal.StrToInt(row["PageIndex"].ToString()) > 0)
                //{
                //    PageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());
                //}
                //if (row.Table.Columns.Contains("PageSize") && AppGlobal.StrToInt(row["PageSize"].ToString()) > 0)
                //{
                //    PageSize = AppGlobal.StrToInt(row["PageSize"].ToString());
                //}

                int pageCount;
                int Counts;
                string sql = "SELECT * FROM Tb_Resources_Details where IsRelease='是' and IsStopRelease='否' and ISNULL(IsDelete,0)=0 and BussId=" + BussId + " and ResourcesID!='" + ResourcesID + "'";
                //查询商家的商品信息
                DataSet ds = GetList(out pageCount, out Counts, sql, PageIndex, PageSize, "ResourcesID", 1, "ResourcesID", PubConstant.GetConnectionString("BusinessContionString"), "BussId,ResourcesID,ResourcesName,Img,ResourcesDisCountPrice,ResourcesSalePrice");
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取商品评价 GetResourcesEvaluation
        /// </summary>
        /// <param name="row"></param>
        /// ResourcesID         商品编号【必填】
        /// <returns></returns>
        //private string GetResourcesEvaluation(DataRow row)
        private DataTable GetResourcesEvaluation(string ResourcesID)
        {
            try
            {
                //if (!row.Table.Columns.Contains("ResourcesID") || string.IsNullOrEmpty(row["ResourcesID"].ToString()))
                //{
                //    return JSONHelper.FromString(false, "商品编号不能为空");
                //}
                IDbConnection con = new SqlConnection(PubConstant.GetConnectionString("BusinessContionString"));
                //DataSet ds = con.ExecuteReader("select * from Tb_Resources_CommodityEvaluation where ResourcesID=@ResourcesID", null, null, null, CommandType.Text).ToDataSet();
                DataSet ds = con.ExecuteReader("SELECT A.*,C.NickName ,C.UserPic FROM Tb_Resources_CommodityEvaluation as A LEFT Join dbo.Tb_Charge_ReceiptDetail as B on A.RpdCode = B.RpdCode LEFT JOIN dbo.Tb_Charge_Receipt D ON B.ReceiptCode = D.Id LEFT Join Unified.dbo.Tb_User as C on D.UserId = C.Id WHERE  ISNULL(A.IsDelete, 0) = 0 and  A.ResourcesID ='" + ResourcesID + "'", null, null, null, CommandType.Text).ToDataSet();
                return ds.Tables[0];
                //return JSONHelper.FromString(ds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
        public DataSet GetList(out int PageCount, out int Counts, string StrCondition, int PageIndex, int PageSize, string SortField, int Sort, string ID, string ConStr, string FldName)
        {
            try
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

                return Ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 获取订单数量
        /// </summary>
        /// <param name="ResourcesID"></param>
        /// <returns></returns>
        private string GetResourcesCount(string ResourcesID)
        {
            try
            {
                IDbConnection con = new SqlConnection(PubConstant.GetConnectionString("BusinessContionString"));
                string str = " select convert(int,isnull(sum(Quantity),0)) as Quantity from Tb_Charge_ReceiptDetail  where ResourcesID='" + ResourcesID + "'";
                DataSet ds = con.ExecuteReader(str, null, null, null, CommandType.Text).ToDataSet();
                if (ds == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                {
                    return "0";
                }
                return ds.Tables[0].Rows[0]["Quantity"].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 获取商品的商家CorpID
        /// </summary>
        /// <param name="ResourcesID"></param>
        /// <returns></returns>
        private string GetCorpIDByResources(string ResourcesID)
        {
            try
            {
                IDbConnection con = new SqlConnection(PubConstant.GetConnectionString("BusinessContionString"));
                string str = " SELECT c.CorpId FROM dbo.Tb_Resources_Details a LEFT JOIN dbo.Tb_System_BusinessCorp b ON a.BussId = b.BussId LEFT JOIN dbo.Tb_System_BusinessCorp_Config c ON b.BussId = c.BussId WHERE a.ResourcesID = '" + ResourcesID + "'";
                DataSet ds = con.ExecuteReader(str, null, null, null, CommandType.Text).ToDataSet();
                if (ds == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                {
                    return "0";
                }
                return ds.Tables[0].Rows[0]["CorpId"].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}