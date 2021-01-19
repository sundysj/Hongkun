using Common.Enum;
using Dapper;
using DapperExtensions;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using Model;
using Model.Model.Buss;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Business
{
    public partial class Orders
    {
        private string OrderDetail(DataRow row)
        {
            if (!row.Table.Columns.Contains("UserId") || string.IsNullOrEmpty(row["UserId"].ToString()))
            {
                return JSONHelper.FromString(false, "用户id不能为空");
            }
            if (!row.Table.Columns.Contains("ReceiptCode") || string.IsNullOrEmpty(row["ReceiptCode"].ToString()))
            {
                return JSONHelper.FromString(false, "订单id不能为空");
            }

            var userId = row["UserId"].ToString();
            var receiptCode = row["ReceiptCode"].ToString();

            string whereStr = $" and r.UserId='{userId}' AND R.Id='{receiptCode}'";
            string FldName = @"CorpId,BussId,BussNature,BussName,Id,ReceiptSign,HandleState,IsReceive,IsPay,IsDeliver,
                                OrderDetailNumber,Tb_Charge_Receipt.Id AS OrderId,ReceiptDate,Express,ExpressNum,CouponBalance";
            StringBuilder sb = new StringBuilder();
            try
            {
                //查询订单
                string OrderSql = @"select B.BussName,B.BussNature,R.*,isnull(x.Balance,0) as CouponBalance,
                                        OrderDetailNumber=(select COUNT(1) from Tb_Charge_ReceiptDetail where ReceiptCode=R.Id and RpdIsDelete=0) 
                                    from Tb_Charge_Receipt as R inner join Tb_System_BusinessCorp as B on b.BussId = R.BussId 
                                    LEFT JOIN Tb_Resources_CouponBalance x ON x.BussId=R.BussId AND x.UserId=R.UserId
                                    where ISNULL( R.IsDelete,0)=0 and ISNULL( B.IsDelete,0)=0  " + whereStr;
                DataSet Ds_Order = BussinessCommon.GetList(out int pageCount, out int Counts, OrderSql, 1, 1, "ReceiptDate", 1, "Id", PubConstant.BusinessContionString, FldName);

                sb.Append("{\"Result\":\"true\",");
                if (Ds_Order != null && Ds_Order.Tables.Count > 0 && Ds_Order.Tables[0].Rows.Count > 0)
                {
                    sb.Append("\"data\":");
                    for (int i = 0; i < Ds_Order.Tables[0].Rows.Count; i++)
                    {
                        DataRow dr = Ds_Order.Tables[0].Rows[i];
                        if (dr["IsDeliver"] == null ||
                            dr["IsDeliver"].ToString() == "" ||
                            dr["IsDeliver"].ToString() == "未发货")
                        {
                            dr["IsReceive"] = null;
                        }

                        DataSet Ds_OrderDetails = null;
                        if (dr["IsPay"] != null && dr["IsPay"].ToString() == "未付款")
                        {
                            using (IDbConnection conn = new SqlConnection(ConnectionDb.GetBusinessConnection()))
                            {
                                Ds_OrderDetails = BussinessCommon.GetShoppingDetailed_New(dr["Id"].ToString());
                            }
                        }
                        else
                        {
                            Ds_OrderDetails = BussinessCommon.GetShoppingDetailed(dr["Id"].ToString());
                        }
                        if (i > 0)
                        {
                            sb.Append(",");
                        }

                        //sb.Append(JSONHelper.FromDataRow(dr));

                        string strr = JSONHelper.FromDataRow(dr);
                        strr = strr.Substring(0, strr.Length - 1);
                        sb.Append(strr);

                        if (Ds_OrderDetails != null && Ds_OrderDetails.Tables.Count > 0 && Ds_OrderDetails.Tables[0].Rows.Count > 0)
                        {
                            sb.Append(",\"Details\":[");
                            for (int j = 0; j < Ds_OrderDetails.Tables[0].Rows.Count; j++)
                            {
                                DataRow dr_Details = Ds_OrderDetails.Tables[0].Rows[j];
                                if (j > 0)
                                {
                                    sb.Append(",");
                                }

                                sb.Append(JSONHelper.FromDataRow(dr_Details));

                                // 查询商品属性

                                using (var bzconn = new SqlConnection(PubConstant.BusinessContionString))
                                {
                                    var specArr = bzconn.Query(@"select a.PropertyId,a.SpecId,b.SpecName,a.Price,A.DiscountAmount,A.GroupBuyingPrice from Tb_ResourcesSpecificationsPrice a 
                                                                    left join Tb_Resources_Specifications b ON a.SpecId=b.Id
                                                                    where a.ResourcesID=@ResourcesID AND a.SpecId IS NOT NULL
                                                                    AND a.SpecId IN (SELECT SpecId FROM Tb_ShoppingDetailed WHERE ShoppingId=@ShoppingId);",
                                                                    new
                                                                    {
                                                                        ResourcesID = Ds_OrderDetails.Tables[0].Rows[j]["ResourcesID"],
                                                                        ShoppingId = Ds_OrderDetails.Tables[0].Rows[j]["ShoppingId"]
                                                                    });

                                    List<string> list = new List<string>();

                                    foreach (var spec in specArr)
                                    {
                                        //list.Add("{\"PropertyId\":\"" + spec.PropertyId + "\",\"SpecId\":\"" + spec.SpecId + "\",\"SpecName\":\"" + spec.SpecName + "\",\"Price\":" + spec.Price + " }");
                                        list.Add(JsonConvert.SerializeObject(new
                                        {
                                            PropertyId = spec.PropertyId,
                                            SpecId = spec.SpecId,
                                            SpecName = spec.SpecName,
                                            Price = spec.Price,
                                            DiscountAmount = spec.DiscountAmount,
                                            GroupBuyingPrice = spec.GroupBuyingPrice,

                                        }));

                                    }

                                    sb.Insert(sb.Length - 1, ",\"Property\":[" + string.Join(",", list) + "]");
                                }
                            }
                            sb.Append("]");
                        }
                        else
                        {
                            sb.Append(",\"Details\":[]");
                        }
                        sb.Append("}");
                    }
                }
                else
                {
                    sb.Append("\"data\":[]");

                }
                sb.Append("}");
            }
            catch (Exception ex)
            {
                sb = new StringBuilder();
                sb.Append(JSONHelper.FromString(false, ex.Message));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 获取所有订单
        /// </summary>
        private string GetORdersAll_New(DataRow row)
        {
            if (!row.Table.Columns.Contains("UserId") || string.IsNullOrEmpty(row["UserId"].ToString()))
            {
                return JSONHelper.FromString(false, "用户编号不能为空");
            }
            int pageCount;
            int Counts;
            int PageIndex = 1;
            int PageSize = 2;
            //string QueryType = "all";
            string IsDeliver = "";//是否发货
            string IsPay = "";//付款状态
            string HandleState = "";//处理状态
            string IsReceive = "";//是否收货


            if (row.Table.Columns.Contains("PageIndex") && AppGlobal.StrToInt(row["PageIndex"].ToString()) > 0)
            {
                PageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());
            }
            if (row.Table.Columns.Contains("PageSize") && AppGlobal.StrToInt(row["PageSize"].ToString()) > 0)
            {
                PageSize = AppGlobal.StrToInt(row["PageSize"].ToString());
            }

            string whereStr = "";
            string FldName = @"CorpId,BussId,BussNature,BussName,Id,ReceiptSign,HandleState,IsReceive,IsPay,IsDeliver,
                                OrderDetailNumber,Tb_Charge_Receipt.Id AS OrderId,ReceiptDate,Express,ExpressNum,CouponBalance";
            StringBuilder sb = new StringBuilder();
            try
            {
                whereStr += " and r.UserId='" + row["UserId"] + "'";

                if (row.Table.Columns.Contains("IsPay") && row["IsPay"].ToString() != "")
                {
                    IsPay = row["IsPay"].ToString();
                    whereStr += " and R.IsPay='" + IsPay + "'";
                }
                if (row.Table.Columns.Contains("IsDeliver") && row["IsDeliver"].ToString() != "")
                {
                    IsDeliver = row["IsDeliver"].ToString();
                    whereStr += " and isnull(R.IsDeliver,'未发货')='" + IsDeliver + "'";
                }
                if (row.Table.Columns.Contains("IsReceive") && row["IsReceive"].ToString() != "")
                {
                    IsReceive = row["IsReceive"].ToString();

                    whereStr += " and isnull(R.IsReceive,'未收货')='" + IsReceive + "'";

                    // 乐生活
                    if (IsReceive == "未收货" && string.IsNullOrEmpty(IsDeliver) && HttpContext.Current.Request.Url.Host.Contains("lsc.easylife365.com"))
                    {
                        whereStr += " and isnull(R.IsDeliver,'未收货')='已发货'";
                    }
                    else if (IsPay == "已付款" && IsReceive == "已收货")
                    {
                        whereStr += " and isnull(R.IsDeliver,'未收货')='已发货' AND isnull(R.IsEvaluated,0)=0";
                    }
                }

                //查询订单
                string OrderSql = @"select ReturnSatus=0 ,ISNULL(B.BussWorkedTel,B.BussMobileTel) as BussMobile, B.BussName,B.BussNature,R.*,isnull(x.Balance,0) as CouponBalance,
                                        OrderDetailNumber=(select COUNT(1) from Tb_Charge_ReceiptDetail where ReceiptCode=R.Id and RpdIsDelete=0) 
                                    from Tb_Charge_Receipt as R inner join Tb_System_BusinessCorp as B on b.BussId = R.BussId 
                                    LEFT JOIN Tb_Resources_CouponBalance x ON x.BussId=R.BussId AND x.UserId=R.UserId
                                    where ISNULL( R.IsDelete,0)=0 and isnull(B.IsClose,'未关闭')='未关闭' and ISNULL( B.IsDelete,0)=0  " + whereStr;
                DataSet Ds_Order = BussinessCommon.GetList(out pageCount, out Counts, OrderSql, PageIndex, PageSize, "ReceiptDate", 1, "Id", PubConstant.BusinessContionString, FldName);

                sb.Append("{\"Result\":\"true\",");
                if (Ds_Order != null && Ds_Order.Tables.Count > 0 && Ds_Order.Tables[0].Rows.Count > 0)
                {
                    sb.Append("\"data\":[");
                    for (int i = 0; i < Ds_Order.Tables[0].Rows.Count; i++)
                    {
                        DataRow dr = Ds_Order.Tables[0].Rows[i];
                        if (dr["IsDeliver"] == null ||
                            dr["IsDeliver"].ToString() == "" ||
                            dr["IsDeliver"].ToString() == "未发货")
                        {
                            dr["IsReceive"] = null;
                        }

                        if (!String.IsNullOrEmpty(dr["IsReceive"].ToString())
                            && !String.IsNullOrEmpty(dr["ConfirmReceivedTime"].ToString())
                            && DateTime.TryParse(dr["ConfirmReceivedTime"].ToString(), out DateTime confirmReceivedTime))
                        {

                            dr["ReturnSatus"] = ReturnInfoStatus(dr["IsRetreat"], confirmReceivedTime, (String)dr["IsReceive"]);
                        }

                        DataSet Ds_OrderDetails = null;
                        if (dr["IsPay"] != null && dr["IsPay"].ToString() == "未付款")
                        {
                            using (IDbConnection conn = new SqlConnection(ConnectionDb.GetBusinessConnection()))
                            {
                                Ds_OrderDetails = BussinessCommon.GetShoppingDetailed_New(dr["Id"].ToString());
                            }
                        }
                        else
                        {
                            Ds_OrderDetails = BussinessCommon.GetShoppingDetailed(dr["Id"].ToString());
                        }
                        if (i > 0)
                        {
                            sb.Append(",");
                        }

                        //sb.Append(JSONHelper.FromDataRow(dr));

                        string strr = JSONHelper.FromDataRow(dr);
                        strr = strr.Substring(0, strr.Length - 1);
                        sb.Append(strr);

                        if (Ds_OrderDetails != null && Ds_OrderDetails.Tables.Count > 0 && Ds_OrderDetails.Tables[0].Rows.Count > 0)
                        {
                            sb.Append(",\"Details\":[");
                            for (int j = 0; j < Ds_OrderDetails.Tables[0].Rows.Count; j++)
                            {
                                DataRow dr_Details = Ds_OrderDetails.Tables[0].Rows[j];
                                if (j > 0)
                                {
                                    sb.Append(",");
                                }

                                sb.Append(JSONHelper.FromDataRow(dr_Details));

                                // 查询商品属性

                                using (var bzconn = new SqlConnection(PubConstant.BusinessContionString))
                                {
                                    var specArr = bzconn.Query(@"select a.PropertyId,a.SpecId,b.SpecName,a.Price,A.DiscountAmount,A.GroupBuyingPrice,C.ResourcesName from Tb_ResourcesSpecificationsPrice a 
                                                                    left join Tb_Resources_Specifications b ON a.SpecId=b.Id
                                                                    LEFT JOIN Tb_Resources_Details AS C ON A.ResourcesID=C.ResourcesID
                                                                    where a.ResourcesID=@ResourcesID AND a.SpecId IS NOT NULL
                                                                    AND a.SpecId IN (SELECT SpecId FROM Tb_ShoppingDetailed WHERE ShoppingId=@ShoppingId);",
                                                                    new
                                                                    {
                                                                        ResourcesID = Ds_OrderDetails.Tables[0].Rows[j]["ResourcesID"],
                                                                        ShoppingId = Ds_OrderDetails.Tables[0].Rows[j]["ShoppingId"]
                                                                    });

                                    List<string> list = new List<string>();
                                    foreach (var spec in specArr)
                                    {
                                        var param = new
                                        {
                                            PropertyId = spec.PropertyId,
                                            SpecId = spec.SpecId,
                                            SpecName = spec.SpecName,
                                            DiscountAmount = spec.DiscountAmount,
                                            GroupBuyingPrice = spec.GroupBuyingPrice,
                                            Price = spec.Price,
                                            ResourcesName = spec.ResourcesName,
                                        };
                                        list.Add(JsonConvert.SerializeObject(param));
                                    }
                                    sb.Insert(sb.Length - 1, ",\"Property\":[" + string.Join(",", list) + "]");
                                }
                            }
                            sb.Append("]");
                        }
                        else
                        {
                            sb.Append(",\"Details\":[]");
                        }
                        sb.Append("}");
                    }
                    sb.Append("]");
                }
                else
                {
                    sb.Append("\"data\":[]");

                }
                sb.Append("}");
            }
            catch (Exception ex)
            {
                sb = new StringBuilder();
                sb.Append(JSONHelper.FromString(false, ex.Message));
            }
            return sb.ToString();
        }


        /// <summary>
        /// 状态判断
        /// </summary>
        /// <param name="isRetreat"></param>
        /// <param name="ConfirmReceivedTime"></param>
        /// <param name="IsReceive"></param>
        /// <returns></returns>
        private int ReturnInfoStatus(object isRetreat, DateTime ConfirmReceivedTime, String IsReceive)
        {
            int result = 0;
            if ((DateTime.Now - ConfirmReceivedTime).TotalMilliseconds < 7 * 24 * 60 * 60 * 1000 && IsReceive == "已收货")
            {
                result = (int)SalesReturnEnum.CanApplyFor;
            }
            if (!String.IsNullOrEmpty(isRetreat.ToString()) && (String)isRetreat == "是") result = (int)SalesReturnEnum.CanLook;
            return result;
        }

        /// <summary>
        /// 删除订单
        /// </summary>
        private string DelChargeReceipt_New(DataRow row)
        {
            if (!row.Table.Columns.Contains("ReceiptCode") || string.IsNullOrEmpty(row["ReceiptCode"].ToString()))
            {
                return JSONHelper.FromString(false, "订单id不能为空");
            }
            if (!row.Table.Columns.Contains("CorpId") || string.IsNullOrEmpty(row["CorpId"].ToString()))
            {
                return JSONHelper.FromString(false, "物管公司id不能为空");
            }
            string str = "";
            try
            {
                IDbConnection con = new SqlConnection(PubConstant.GetConnectionString("BusinessContionString"));

                Tb_Charge_Receipt charge = BussinessCommon.GetChargeReceiptModel(row["ReceiptCode"].ToString());
                if (charge != null)
                {
                    charge.IsDelete = 1;
                    charge.CancellationType = 1;//业主主动取消
                    charge.CancellationTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    charge.CancellationReason = "";

                    con.Update<Tb_Charge_Receipt>(charge);
                    con.Execute("update Tb_Charge_ReceiptDetail set RpdIsDelete=1 where ReceiptCode=@ReceiptCode", new { ReceiptCode = charge.Id });

                    if (charge.IsUseCoupon.HasValue && charge.IsUseCoupon.Value == 1)
                    {
                        con.Execute("proc_Resources_CouponBalance_Return",
                            new { CorpId = AppGlobal.StrToInt(row["CorpId"].ToString()), ReceiptCode = charge.Id }, null, null, CommandType.StoredProcedure);
                    }
                }
                else
                {
                    str = "删除失败";
                }
            }
            catch (Exception ex)
            {
                str = ex.Message;
            }
            if (str != "")
            {
                return JSONHelper.FromString(false, str);
            }
            else
            {
                return JSONHelper.FromString(true, "");
            }

        }

        /// <summary>
        /// 添加商品评价
        /// </summary>
        private string InsertResourcesCommodityEvaluation_New(DataSet Ds)
        {
            if (Ds.Tables["Evaluation"] == null || Ds.Tables["Evaluation"].Rows.Count == 0)
            {
                return JSONHelper.FromString(false, "无评价内容");
            }

            string receiptCode = null;

            using (IDbConnection con = new SqlConnection(PubConstant.GetConnectionString("BusinessContionString")))
            {

                foreach (DataRow item in Ds.Tables["Evaluation"].Rows)
                {

                    String urlImg = String.Empty;
                    if (item.Table.Columns.Contains("Imgs") && !String.IsNullOrEmpty(item["Imgs"].ToString()))
                    {
                        urlImg = item["Imgs"].ToString();
                    }

                    if (item.Table.Columns.Contains("RpdCode") &&
                        item.Table.Columns.Contains("ResourcesID") &&
                        item.Table.Columns.Contains("Content") &&
                        item.Table.Columns.Contains("StarRating"))
                    {
                        if (item.Table.Columns.Contains("ReceiptCode"))
                        {
                            receiptCode = item["ReceiptCode"].ToString();
                        }
                        else
                        {
                            receiptCode = con.Query<string>(@"SELECT ReceiptCode FROM Tb_Charge_ReceiptDetail WHERE RpdCode=@RpdCode",
                                new { RpdCode = item["RpdCode"].ToString() }).FirstOrDefault();
                        }

                        int count = con.Query<int>(@"SELECT count(1) FROM Tb_Resources_CommodityEvaluation WHERE isnull(IsDelete,0)=0 AND RpdCode=@RpdCode AND ResourcesID=@ResourcesID ", new { RpdCode = item["RpdCode"].ToString(), ResourcesID = item["ResourcesID"].ToString() }).First();

                        // 已有评价，更新
                        if (count > 0)
                        {
                            con.Execute(@"UPDATE Tb_Resources_CommodityEvaluation SET EvaluateContent=@Content,Star=@StarRating,EvaluateDate=getdate(),UploadImg=@UploadImg  
                                            WHERE isnull(IsDelete,0)=0 AND RpdCode=@RpdCode AND ResourcesID=@ResourcesID",
                                            new
                                            {
                                                Content = item["Content"].ToString(),
                                                StarRating = item["StarRating"].ToString(),
                                                RpdCode = item["RpdCode"].ToString(),
                                                ResourcesID = item["ResourcesID"].ToString(),
                                                UploadImg = urlImg
                                            });
                        }
                        else
                        {
                            con.Execute(@"INSERT INTO Tb_Resources_CommodityEvaluation(id,RpdCode,ResourcesID,EvaluateContent,UploadImg,Star,EvaluateDate) 
                                          VALUES (newid(),@RpdCode, @ResourcesID,@Content,@UploadImg,@StarRating,getdate())",
                                          new
                                          {
                                              Content = item["Content"].ToString(),
                                              StarRating = item["StarRating"].ToString(),
                                              RpdCode = item["RpdCode"].ToString(),
                                              ResourcesID = item["ResourcesID"].ToString(),
                                              UploadImg = urlImg
                                          });
                        }
                        con.Execute(@"UPDATE Tb_Charge_ReceiptDetail SET IsEvaluated=1 WHERE RpdCode=@RpdCode", new { RpdCode = item["RpdCode"].ToString() });
                    }
                }

                if (!string.IsNullOrEmpty(receiptCode))
                {
                    con.Execute(@"UPDATE Tb_Charge_Receipt SET IsEvaluated=1 WHERE Id=@ReceiptCode", new { ReceiptCode = receiptCode });
                }

                return JSONHelper.FromString(true, "评价成功");
            }
        }


        //private string GetGoodsCollectionInfo(DataRow row)
        //{
        //    if (!row.Table.Columns.Contains("UserId") || string.IsNullOrEmpty(row["UserId"].ToString()))
        //    {
        //        return JSONHelper.FromString(false, "用户编号不能为空");
        //    }
        //    if (!row.Table.Columns.Contains("GoodsId") || string.IsNullOrEmpty(row["GoodsId"].ToString()))
        //    {
        //        return JSONHelper.FromString(false, "商品ID不能为空");
        //    }
        //    string userId = row["UserId"].ToString();
        //    string goodsId = row["GoodsId"].ToString();
        //    int counts=0;
        //    using (SqlConnection conn = new SqlConnection(PubConstant.BusinessContionString))
        //    {
        //        string sql = @"SELECT COUNNT(*) FROM Tb_Goods_Collection WHERE UserId = @UserId AND GoodsId = @GoodsId";
        //        counts = conn.QueryFirstOrDefault<int>(sql, new { UserId = userId, GoodsId = goodsId});
        //        if (counts > 0)
        //        {
        //            return JSONHelper.FromString(true, "1");
        //        }
        //        else
        //        {
        //            return JSONHelper.FromString(true, "0");
        //        }
        //    }
        //}

        //private string GoodsCollect(DataRow row)
        //{
        //    if (!row.Table.Columns.Contains("UserId") || string.IsNullOrEmpty(row["UserId"].ToString()))
        //    {
        //        return JSONHelper.FromString(false, "用户编号不能为空");
        //    }
        //    if (!row.Table.Columns.Contains("GoodsId") || string.IsNullOrEmpty(row["GoodsId"].ToString()))
        //    {
        //        return JSONHelper.FromString(false, "商品ID不能为空");
        //    }
        //    string userId = row["UserId"].ToString();
        //    string goodsId = row["GoodsId"].ToString();
        //    int counts = 0;
        //    using (SqlConnection conn = new SqlConnection(PubConstant.BusinessContionString))
        //    {
        //        string sql = @"INSERT INTO Tb_Goods_Collection(UserId,GoodsId) Values(@UserId,@GoodsId)";
        //        counts = conn.Execute(sql, new { UserId = userId, GoodsId = goodsId });
        //        if (counts > 0)
        //        {
        //            return JSONHelper.FromString(true, "收藏成功");
        //        }
        //        else
        //        {
        //            return JSONHelper.FromString(false, "收藏失败");
        //        }
        //    }
        //}

        //private string CancelGoodsCollect(DataRow row)
        //{
        //    if (!row.Table.Columns.Contains("UserId") || string.IsNullOrEmpty(row["UserId"].ToString()))
        //    {
        //        return JSONHelper.FromString(false, "用户编号不能为空");
        //    }
        //    if (!row.Table.Columns.Contains("GoodsId") || string.IsNullOrEmpty(row["GoodsId"].ToString()))
        //    {
        //        return JSONHelper.FromString(false, "商品ID不能为空");
        //    }
        //    string userId = row["UserId"].ToString();
        //    string goodsId = row["GoodsId"].ToString();
        //    int counts = 0;
        //    using (SqlConnection conn = new SqlConnection(PubConstant.BusinessContionString))
        //    {
        //        string sql = @"DELETE FROM Tb_Goods_Collection WHERE UserId = @UserId AND GoodsId = @GoodsId";
        //        counts = conn.Execute(sql, new { UserId = userId, GoodsId = goodsId });
        //        if (counts > 0)
        //        {
        //            return JSONHelper.FromString(true, "取消收藏成功");
        //        }
        //        else
        //        {
        //            return JSONHelper.FromString(false, "取消收藏失败");
        //        }
        //    }
        //}



        /// <summary>
        /// 订单详情
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private String QueryOrderDetail(DataRow row)
        {
            if (!row.Table.Columns.Contains("Id") || string.IsNullOrEmpty(row["Id"].AsString()))
            {
                return JSONHelper.FromString(false, "订单详情失败");
            }
            String orderId = row["Id"].ToString();

            int pageCount;
            int Counts;
            int PageIndex = 1;
            int PageSize = 2;

            string whereStr = "";
            string FldName = @"CorpId,BussId,BussNature,BussName,Id,ReceiptSign,HandleState,IsReceive,IsPay,IsDeliver,
                                OrderDetailNumber,Tb_Charge_Receipt.Id AS OrderId,ReceiptDate,Express,ExpressNum,CouponBalance";
            StringBuilder sb = new StringBuilder();
            try
            {
                whereStr += " and r.Id='" + orderId + "'";
                //查询订单
                string OrderSql = @"select ReturnSatus=0,ISNULL(B.BussWorkedTel,B.BussMobileTel) as BussMobile, B.BussName,B.BussNature,R.*,isnull(x.Balance,0) as CouponBalance,
                                        OrderDetailNumber=(select COUNT(1) from Tb_Charge_ReceiptDetail where ReceiptCode=R.Id and RpdIsDelete=0) 
                                    from Tb_Charge_Receipt as R inner join Tb_System_BusinessCorp as B on b.BussId = R.BussId 
                                    LEFT JOIN Tb_Resources_CouponBalance x ON x.BussId=R.BussId AND x.UserId=R.UserId
                                    where ISNULL( R.IsDelete,0)=0 and isnull(B.IsClose,'未关闭')='未关闭' and ISNULL( B.IsDelete,0)=0  " + whereStr;
                DataSet Ds_Order = null;
                using (IDbConnection con = new SqlConnection(PubConstant.BusinessContionString))
                {
                    Ds_Order = con.ExecuteReader(OrderSql).ToDataSet();
                }
                sb.Append("{\"Result\":\"true\",");
                if (Ds_Order != null && Ds_Order.Tables.Count > 0 && Ds_Order.Tables[0].Rows.Count > 0)
                {
                    sb.Append("\"data\":[");
                    for (int i = 0; i < Ds_Order.Tables[0].Rows.Count; i++)
                    {
                        DataRow dr = Ds_Order.Tables[0].Rows[i];
                        if (dr["IsDeliver"] == null ||
                            dr["IsDeliver"].ToString() == "" ||
                            dr["IsDeliver"].ToString() == "未发货")
                        {
                            dr["IsReceive"] = null;
                        }

                        if (!String.IsNullOrEmpty(dr["IsReceive"].ToString())
                            && !String.IsNullOrEmpty(dr["ConfirmReceivedTime"].ToString())
                            && DateTime.TryParse(dr["ConfirmReceivedTime"].ToString(), out DateTime confirmReceivedTime))
                        {

                            dr["ReturnSatus"] = ReturnInfoStatus(dr["IsRetreat"], confirmReceivedTime, (String)dr["IsReceive"]);
                        }

                        DataSet Ds_OrderDetails = null;
                        if (dr["IsPay"] != null && dr["IsPay"].ToString() == "未付款")
                        {
                            using (IDbConnection conn = new SqlConnection(ConnectionDb.GetBusinessConnection()))
                            {
                                Ds_OrderDetails = BussinessCommon.GetShoppingDetailed_New(dr["Id"].ToString());
                            }
                        }
                        else
                        {
                            Ds_OrderDetails = BussinessCommon.GetShoppingDetailed(dr["Id"].ToString());
                        }
                        if (i > 0)
                        {
                            sb.Append(",");
                        }

                        string strr = JSONHelper.FromDataRow(dr);
                        strr = strr.Substring(0, strr.Length - 1);
                        sb.Append(strr);

                        if (Ds_OrderDetails != null && Ds_OrderDetails.Tables.Count > 0 && Ds_OrderDetails.Tables[0].Rows.Count > 0)
                        {
                            sb.Append(",\"Details\":[");
                            for (int j = 0; j < Ds_OrderDetails.Tables[0].Rows.Count; j++)
                            {
                                DataRow dr_Details = Ds_OrderDetails.Tables[0].Rows[j];
                                if (j > 0)
                                {
                                    sb.Append(",");
                                }
                                sb.Append(JSONHelper.FromDataRow(dr_Details));

                                // 查询商品属性
                                using (var bzconn = new SqlConnection(PubConstant.BusinessContionString))
                                {
                                    var specArr = bzconn.Query(@"select a.PropertyId,a.SpecId,b.SpecName,a.Price,A.DiscountAmount,A.GroupBuyingPrice,C.ResourcesName from Tb_ResourcesSpecificationsPrice a 
                                                                    left join Tb_Resources_Specifications b ON a.SpecId=b.Id
                                                                    LEFT JOIN Tb_Resources_Details AS C ON A.ResourcesID=C.ResourcesID
                                                                    where a.ResourcesID=@ResourcesID AND a.SpecId IS NOT NULL
                                                                    AND a.SpecId IN (SELECT SpecId FROM Tb_ShoppingDetailed WHERE ShoppingId=@ShoppingId);",
                                                                    new
                                                                    {
                                                                        ResourcesID = Ds_OrderDetails.Tables[0].Rows[j]["ResourcesID"],
                                                                        ShoppingId = Ds_OrderDetails.Tables[0].Rows[j]["ShoppingId"]
                                                                    });

                                    List<string> list = new List<string>();
                                    foreach (var spec in specArr)
                                    {
                                        list.Add("{\"PropertyId\":\"" + spec.PropertyId + "\",\"SpecId\":\"" + spec.SpecId + "\",\"SpecName\":\"" + spec.SpecName + "\",\"DiscountAmount\":" + spec.DiscountAmount + ",\"GroupBuyingPrice\":" + spec.GroupBuyingPrice + ",\"Price\":" + spec.Price + ",\"ResourcesName\":\"" + spec.ResourcesName + "\" }");
                                    }

                                    sb.Insert(sb.Length - 1, ",\"Property\":[" + string.Join(",", list) + "]");
                                }
                            }
                            sb.Append("]");
                        }
                        else
                        {
                            sb.Append(",\"Details\":[]");
                        }
                        sb.Append("}");
                    }
                    sb.Append("]");
                }
                else
                {
                    sb.Append("\"data\":[]");

                }
                sb.Append("}");
            }
            catch (Exception ex)
            {
                sb = new StringBuilder();
                sb.Append(JSONHelper.FromString(false, ex.Message));
            }
            return sb.ToString();
        }
    }
}
