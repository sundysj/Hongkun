using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Dapper;
using DapperExtensions;
using System.Data;
using MobileSoft.Common;
using Model.Model.Buss;
using System.Data.SqlClient;
using MobileSoft.DBUtility;
using Model.Resources;

namespace Business
{
    /// <summary>
    /// 订单类
    /// </summary>
    public partial class Orders : PubInfo
    {
        public Orders()
        {
            base.Token = "20161203Orders";
        }

        public override void Operate(ref Transfer Trans)
        {
            Trans.Result = JSONHelper.FromString(false, "未知错误");
            DataSet Ds = base.XmlToDataSet(Trans.Attribute);
            DataTable dAttributeTable = Ds.Tables[0];
            DataRow Row = dAttributeTable.Rows[0];

            switch (Trans.Command)
            {
                case "SetOrder":
                    Trans.Result = SetOrder(Row);//生成订单
                    break;
                case "OrderDetail":
                    Trans.Result = OrderDetail(Row);
                    break;
                case "GetOrderDetail":
                    Trans.Result = GetOrderDetail(Row);
                    break;
                case "GetORdersAll":
                    Trans.Result = GetORdersAll(Row);//查询订单
                    break;
                case "GetORdersAll_New":
                    Trans.Result = GetORdersAll_New(Row);//查询订单
                    break;
                case "GetChargeReceiptSum":
                    Trans.Result = GetChargeReceiptSum(Row); //获取用户订单数量
                    break;
                case "GetChargeReceiptSum_v2":
                    Trans.Result = GetChargeReceiptSum_v2(Row); //获取用户订单数量
                    break;
                case "InsertResourcesCommodityEvaluation":
                    Trans.Result = InsertResourcesCommodityEvaluation(Row); //添加订单评价
                    break;
                case "InsertResourcesCommodityEvaluation_New":
                    Trans.Result = InsertResourcesCommodityEvaluation_New(Ds); //添加订单评价
                    break;
                case "DelChargeReceipt":
                    Trans.Result = DelChargeReceipt(Row); //删除订单
                    break;
                case "DelChargeReceipt_New":
                    Trans.Result = DelChargeReceipt_New(Row); //删除订单
                    break;
                case "UpdateChargeHandleState":
                    Trans.Result = UpdateChargeHandleState(Row); //修改订单处理状态
                    break;
                case "CheckPayInfo":
                    Trans.Result = CheckPayInfo(Row, Trans);//小区开通支付通道查询
                    break;
                case "OperationPay":
                    Trans.Result = OperationPay(Row);//一键关闭所有支付通道
                    break;
                case "SubmitOrder":
                    Trans.Result = SubmitOrder(Ds);//提交订单
                    break;
                case "SubmitOrder_Changcheng":
                    Trans.Result = SubmitOrder_Changcheng(Ds);//提交订单
                    break;
                case "QueryOrderDetail":
                    Trans.Result = QueryOrderDetail(Row);
                    break;
            }
        }

        #region 开通支付通道查询
        /// <summary>
        /// 小区开通支付通道查询
        /// </summary>
        /// <param name="row"></param>
        /// <param name="trans"></param>
        /// CommunityId    小区编号【必填】
        /// <returns></returns>
        private string CheckPayInfo(DataRow row, Transfer trans)
        {
            if (!row.Table.Columns.Contains("BussId") && String.IsNullOrEmpty(row["BussId"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            string BussId = row["BussId"].ToString();
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"Result\":\"true\",\"data\":[{");
            IDbConnection con = new SqlConnection(PubConstant.GetConnectionString("BusinessContionString"));
            //微信
            string sql = "select * from Tb_WeiXinPayCertificate where ISNULL(IsEnable,0)=0 and  BussId=@BussId";
            List<Tb_WeiXinPayCertificate> list_weixin = con.Query<Tb_WeiXinPayCertificate>(sql, new { BussId = BussId }).ToList<Tb_WeiXinPayCertificate>();
            if (list_weixin.Count > 0)
            {
                sb.Append("\"WChatPay\":\"true\",");
            }
            else
            {
                sb.Append("\"WChatPay\":\"false\",");
            }
            //支付宝
            sql = "select * from Tb_AlipayCertifiate where ISNULL(IsEnable,0)=0 and  BussId=@BussId";
            List<Tb_AlipayCertifiate> list_Alipay = con.Query<Tb_AlipayCertifiate>(sql, new { BussId = BussId }).ToList<Tb_AlipayCertifiate>();
            if (list_Alipay.Count > 0)
            {
                sb.Append("\"AliPay\":\"true\",");
            }
            else
            {
                sb.Append("\"AliPay\":\"false\",");
            }
            //银联
            sql = "select * from Tb_UnionPayCertificate where ISNULL(IsEnable,0)=0 and  BussId=@BussId";
            List<Tb_UnionPayCertificate> list_UnionPay = con.Query<Tb_UnionPayCertificate>(sql, new { BussId = BussId }).ToList<Tb_UnionPayCertificate>();
            if (list_UnionPay.Count > 0)
            {
                sb.Append("\"UnionPay\":\"true\"");
            }
            else
            {
                sb.Append("\"UnionPay\":\"false\"");
            }


            sb.Append("}]}");
            return sb.ToString();
        }

        #endregion

        #region 一键关闭或打开支付通道

        private string OperationPay(DataRow row)
        {
            string backStr = "";
            if (!row.Table.Columns.Contains("OperationType") && String.IsNullOrEmpty(row["OperationType"].ToString()))
            {
                return JSONHelper.FromString(false, "操作类型不能为空");
            }
            if (!row.Table.Columns.Contains("PayType") && String.IsNullOrEmpty(row["PayType"].ToString()))
            {
                return JSONHelper.FromString(false, "支付类型不能为空");
            }
            if (!row.Table.Columns.Contains("SysType") && String.IsNullOrEmpty(row["SysType"].ToString()))
            {
                return JSONHelper.FromString(false, "系统类型不能为空");
            }
            try
            {

                //操作类型：开启、关闭
                string OperationType = row["OperationType"].ToString();
                //当前所支持的三种支付通道
                string PayType = row["PayType"].ToString();
                //系统类型：商家、运营
                string SysType = row["SysType"].ToString();
                //查询条件
                string WhereStr = "";
                if (row.Table.Columns.Contains("WhereStr"))
                {
                    WhereStr = row["WhereStr"].ToString();
                }

                switch (PayType)
                {
                    case "WChatPay":
                        OperationPayInfo_WChatPay(OperationType, SysType, WhereStr);
                        break;
                    case "AliPay":
                        OperationPayInfo_AliPay(OperationType, SysType, WhereStr);
                        break;
                    case "UnionPay":
                        OperationPayInfo_UnionPay(OperationType, SysType, WhereStr);
                        break;
                    default:
                        backStr = "支付类型不能为空";
                        break;

                }
            }
            catch (Exception ex)
            {
                backStr = ex.Message;
            }
            if (backStr != "")
            {
                return JSONHelper.FromString(false, backStr);
            }
            else
            {
                return JSONHelper.FromString(true, "修改成功");
            }

        }

        /// <summary>
        /// 微信
        /// </summary>
        /// <param name="OperationType"></param>
        /// <param name="SysType"></param>
        /// <param name="QueryStr"></param>
        private void OperationPayInfo_WChatPay(string OperationType, string SysType, string QueryStr)
        {
            IDbConnection con = null;
            StringBuilder sb = new StringBuilder();
            sb.Append("update Tb_WeiXinPayCertificate");

            if (OperationType == "Open")
            {
                sb.Append(" set IsEnable=0 ");
            }
            else if (OperationType == "Close")
            {
                sb.Append(" set IsEnable=1 ");
            }
            if (SysType == "Bussiness")//商家
            {
                con = new SqlConnection(PubConstant.BusinessContionString);

                if (QueryStr != "")
                {
                    sb.AppendFormat(" where  BussId='{0}'", QueryStr);
                }
            }
            if (SysType == "Unified")//运营
            {
                con = new SqlConnection(PubConstant.UnifiedContionString);

                if (QueryStr != "")
                {
                    sb.AppendFormat(" where  CommunityId='{0}'", QueryStr);
                }
            }
            con.ExecuteScalar(sb.ToString(), null, null, null, CommandType.Text);
            con.Dispose();
        }

        /// <summary>
        /// 支付宝
        /// </summary>
        /// <param name="OperationType"></param>
        /// <param name="SysType"></param>
        /// <param name="QueryStr"></param>
        private void OperationPayInfo_AliPay(string OperationType, string SysType, string QueryStr)
        {
            IDbConnection con = null;
            StringBuilder sb = new StringBuilder();
            sb.Append("update Tb_AlipayCertifiate");

            if (OperationType == "Open")
            {
                sb.Append(" set IsEnable=0 ");
            }
            else if (OperationType == "Close")
            {
                sb.Append(" set IsEnable=1 ");
            }
            if (SysType == "Bussiness")//商家
            {
                con = new SqlConnection(PubConstant.BusinessContionString);

                if (QueryStr != "")
                {
                    sb.AppendFormat(" where  BussId='{0}'", QueryStr);
                }
            }
            if (SysType == "Unified")//运营
            {
                con = new SqlConnection(PubConstant.UnifiedContionString);

                if (QueryStr != "")
                {
                    sb.AppendFormat(" where  CommunityId='{0}'", QueryStr);
                }
            }
            con.ExecuteScalar(sb.ToString(), null, null, null, CommandType.Text);
            con.Dispose();
        }

        /// <summary>
        /// 银联
        /// </summary>
        /// <param name="OperationType"></param>
        /// <param name="SysType"></param>
        /// <param name="QueryStr"></param>
        private void OperationPayInfo_UnionPay(string OperationType, string SysType, string QueryStr)
        {
            IDbConnection con = null;
            StringBuilder sb = new StringBuilder();
            sb.Append("update Tb_UnionPayCertificate");

            if (OperationType == "Open")
            {
                sb.Append(" set IsEnable=0 ");
            }
            else if (OperationType == "Close")
            {
                sb.Append(" set IsEnable=1 ");
            }
            if (SysType == "Bussiness")//商家
            {
                con = new SqlConnection(PubConstant.BusinessContionString);

                if (QueryStr != "")
                {
                    sb.AppendFormat(" where  BussId='{0}'", QueryStr);
                }
            }
            if (SysType == "Unified")//运营
            {
                con = new SqlConnection(PubConstant.UnifiedContionString);

                if (QueryStr != "")
                {
                    sb.AppendFormat(" where  CommunityId='{0}'", QueryStr);
                }
            }
            con.ExecuteScalar(sb.ToString(), null, null, null, CommandType.Text);
            con.Dispose();
        }


        #endregion


        /// <summary>
        /// 订单详情
        /// </summary>
        private string GetOrderDetail(DataRow row)
        {
            if (!row.Table.Columns.Contains("ReceiptCode") || string.IsNullOrEmpty(row["ReceiptCode"].ToString()))
            {
                return JSONHelper.FromString(false, "订单id不能为空");
            }

            var receiptCode = row["ReceiptCode"].ToString();

            using (var conn = new SqlConnection(PubConstant.BusinessContionString))
            {
                var sql = @"SELECT a.Id,a.ReceiptSign,a.Name,a.Mobile,a.DeliverAddress,a.ReceiptDate,a.ReceiptMemo,
                                a.PayDate,a.IsDeliver,a.IsReceive,a.Method,b.Id AS EvaluateId,b.Star AS EvaluateStar,
                                   isnull(a.Express,'暂无') AS Express,isnull(a.ExpressNum,'暂无') AS ExpressNum
                            FROM Tb_Charge_Receipt a
                            LEFT JOIN Tb_Resources_CommodityEvaluation b ON a.Id=b.RpdCode AND isnull(b.IsDelete,0)=0
                            WHERE a.Id=@Id;

                            SELECT DISTINCT c.BussName,c.BussMobileTel
                            FROM Tb_Charge_ReceiptDetail a
                            LEFT JOIN Tb_Resources_Details b ON a.ResourcesID=b.ResourcesID
                            LEFT JOIN Tb_System_BusinessCorp c ON b.BussId=c.BussId
                            WHERE a.ReceiptCode=@Id;

                            SELECT a.ResourcesID,b.ResourcesName,a.Quantity,b.ResourcesSalePrice AS OriginalPrice,
                                   (SELECT TOP 1 Value FROM SplitString(b.Img,',',1)) AS Img,
                                   CASE WHEN b.GroupBuyStartDate<=getdate() AND b.GroupBuyEndDate>=getdate() AND b.IsGroupBuy='是'
                                        THEN b.GroupBuyPrice
                                        ELSE b.ResourcesSalePrice-b.ResourcesDisCountPrice END AS CurrentPrice
                            FROM Tb_Charge_ReceiptDetail a
                            LEFT JOIN Tb_Resources_Details b ON a.ResourcesID=b.ResourcesID
                            LEFT JOIN Tb_ShoppingCar c ON a.ShoppingId=c.Id
                            WHERE ReceiptCode=@Id;

                            SELECT a.ResourcesID,d.PropertyName,e.SpecName
                            FROM Tb_Charge_ReceiptDetail a
                            LEFT JOIN Tb_ShoppingCar b ON a.ShoppingId=b.Id
                            LEFT JOIN Tb_ShoppingDetailed c ON a.ShoppingId=c.Id
                            LEFT JOIN Tb_Resources_Property d ON c.PropertysId=d.Id
                            LEFT JOIN Tb_Resources_Specifications e ON c.SpecId=e.Id
                            WHERE a.ReceiptCode=@Id AND c.PropertysId IS NOT NULL AND c.SpecId IS NOT NULL 
                            ORDER BY d.Sort;";

                var reader = conn.QueryMultiple(sql, new { Id = receiptCode });
                var deliverInfo = reader.Read().FirstOrDefault();
                var bussInfo = reader.Read().FirstOrDefault();
                var goodsInfo = reader.Read();
                var spectInfo = reader.Read().ToList();

                var orderGoods = goodsInfo.Select(obj => new
                {
                    ResourcesID = obj.ResourcesID,
                    ResourcesName = obj.ResourcesName,
                    Img = obj.Img,
                    Quantity = obj.Quantity,
                    OriginalPrice = obj.OriginalPrice,
                    CurrentPrice = obj.CurrentPrice,
                    PropertySpec = new Dictionary<string, string>()
                }).ToList();

                if (spectInfo.Count() > 0)
                {
                    for (int i = 0; i < orderGoods.Count(); i++)
                    {
                        var goods = orderGoods[i];
                        var tmp = spectInfo.FindAll(obj => obj.ResourcesID == goods.ResourcesID);
                        foreach (var item in tmp)
                        {
                            goods.PropertySpec.Add(item.PropertyName, item.SpecName);
                        }
                    }
                }

                // 下单、付款、发货、收货
                var states = new List<object>();
                states.Add(new
                {
                    Desc = $"您创建了订单",
                    State = "下单",
                    Date = deliverInfo.ReceiptDate.ToString("yyyy/MM/dd HH:mm:ss")
                });

                if (deliverInfo.PayDate != null)
                {
                    states.Add(new
                    {
                        Desc = $"您使用{deliverInfo.Method}付款成功",
                        State = "已付款",
                        Date = deliverInfo.PayDate.ToString("yyyy/MM/dd HH:mm:ss")
                    });
                }

                if (deliverInfo.IsDeliver != null)
                {
                    states.Add(new
                    {
                        Desc = $"商家已发货",
                        State = "已发货",
                        Date = ""
                    });
                }

                if (deliverInfo.IsReceive != null && deliverInfo.IsReceive == "已收货")
                {
                    states.Add(new
                    {
                        Desc = $"已确认收货",
                        State = "已收货",
                        Date = ""
                    });
                }

                if (deliverInfo.EvaluateId != null)
                {
                    states.Add(new
                    {
                        Desc = $"已{deliverInfo.Star}星评价",
                        State = "已评价",
                        Date = ""
                    });
                }

                var jsonObj = new
                {
                    Id = deliverInfo.Id,
                    ReceiptSign = deliverInfo.ReceiptSign,
                    Name = deliverInfo.Name,
                    Mobile = deliverInfo.Mobile,
                    DeliverAddress = deliverInfo.DeliverAddress,
                    Express = deliverInfo.Express,
                    ExpressNum = deliverInfo.ExpressNum,
                    Details = new List<object>()
                    {
                        new {  BussName = bussInfo.BussName, BussMobileTel = bussInfo.BussMobileTel, Goods = orderGoods }
                    },
                    States = states
                };

                return new ApiResult(true, jsonObj).toJson();
            }
        }


        #region 查询订单



        /// <summary>
        /// 查询订单    GetORdersAll
        /// </summary>
        /// <param name="row"></param>
        /// UserId                  用户编号【必填】
        /// PageIndex            
        /// PageSize
        /// IsDeliver               是否发货
        /// IsPay                   付款状态
        /// HandleState             处理状态
        /// IsReceive               是否收货
        /// 返回：
        ///     {Result:true,data:[BussName 商家名称,Id 订单ID,ReceiptSign 订单编号,Amount 总金额,HandleState 订单状态,IsReceive 是否收货,IsPay 付款状态,IsDeliver 是否发货,OrderDetailNumber 明细数量,Details:[RpdCode 明细id,Quantity 数量,SalesPrice 销售价格,DetailAmount 小计,ResourcesName 商品名称,Img 商品图片],[]],[],[]}
        ///     备注：当IsReceive 值为'已发货'时，订单字段中多二个物流信息：Express 物流名称，ExpressNum 物流编号
        /// <returns></returns>
        private string GetORdersAll(DataRow row)
        {
            if (!row.Table.Columns.Contains("UserId") || string.IsNullOrEmpty(row["UserId"].ToString()))
            {
                return JSONHelper.FromString(false, "用户编号不能为空");
            }
            int pageCount;
            int Counts;
            int PageIndex = 1;
            int PageSize = 5;
#pragma warning disable CS0219 // 变量“IsDeliver”已被赋值，但从未使用过它的值
            string IsDeliver = "";//是否发货
#pragma warning restore CS0219 // 变量“IsDeliver”已被赋值，但从未使用过它的值
#pragma warning disable CS0219 // 变量“IsPay”已被赋值，但从未使用过它的值
            string IsPay = "";//付款状态
#pragma warning restore CS0219 // 变量“IsPay”已被赋值，但从未使用过它的值
#pragma warning disable CS0219 // 变量“HandleState”已被赋值，但从未使用过它的值
            string HandleState = "";//处理状态
#pragma warning restore CS0219 // 变量“HandleState”已被赋值，但从未使用过它的值
#pragma warning disable CS0219 // 变量“IsReceive”已被赋值，但从未使用过它的值
            string IsReceive = "";//是否收货
#pragma warning restore CS0219 // 变量“IsReceive”已被赋值，但从未使用过它的值

            if (row.Table.Columns.Contains("PageIndex") && AppGlobal.StrToInt(row["PageIndex"].ToString()) > 0)
            {
                PageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());
            }
            if (row.Table.Columns.Contains("PageSize") && AppGlobal.StrToInt(row["PageSize"].ToString()) > 0)
            {
                PageSize = AppGlobal.StrToInt(row["PageSize"].ToString());
            }

            var FldName = @"BussId,BussName,Id,ReceiptSign,Amount,RealAmount,IsUseCoupon,HandleState,
                            IsReceive,IsPay,IsDeliver,OrderDetailNumber,OrderId,
                            IsEevaluate,ReceiptDate,Express,ExpressNum";

            StringBuilder sb = new StringBuilder();

            try
            {
                var condition = $" AND r.UserId='{ row["UserId"].ToString() }'";

                // 支付状态
                if (row.Table.Columns.Contains("IsPay") && row["IsPay"].ToString() != "")
                    condition += $" AND r.IsPay='{ row["IsPay"].ToString() }'";
                else
                    condition += $" AND isnull(r.IsPay,'未付款')='未付款'";

                // 发货状态
                if (row.Table.Columns.Contains("IsDeliver") && row["IsDeliver"].ToString() != "")
                    condition += $" AND r.IsDeliver='{ row["IsDeliver"].ToString() }'";
                else
                    condition += $" AND isnull(r.IsDeliver,'未发货')='未发货'";

                // 收货状态
                if (row.Table.Columns.Contains("IsReceive") && row["IsReceive"].ToString() != "")
                    condition += $" AND r.IsReceive='{ row["IsReceive"].ToString() }'";
                else
                    condition += $" AND isnull(r.IsReceive,'未收货')='未收货'";

                // 操作确认
                if (row.Table.Columns.Contains("HandleState") && row["HandleState"].ToString() != "")
                    condition += $" AND r.HandleState='{ row["HandleState"].ToString() }'";

                //查询订单
                var sql = @"SELECT b.BussName,a.*,
                                OrderDetailNumber=
                                (
                                    SELECT COUNT(1) FROM Tb_Charge_ReceiptDetail x 
                                    LEFT JOIN Tb_Resources_Details y on x.ResourcesID=y.ResourcesID
                                    WHERE x.ReceiptCode=a.Id and isnull(x.RpdIsDelete,0)=0 
                                    AND y.IsRelease='是' AND y.IsStopRelease='否' AND isnull(y.IsDelete,0)=0
                                ),
                                IsEevaluate=
                                (
                                    SELECT count(1) FROM Tb_Resources_CommodityEvaluation e 
                                    LEFT JOIN Tb_Charge_ReceiptDetail t ON t.RpdCode=e.RpdCode
                                    WHERE t.ReceiptCode=a.Id and isnull(t.RpdIsDelete,0)=0 and isnull(e.IsDelete,0)=0
                                )
                            FROM Tb_Charge_Receipt a 
                            LEFT JOIN Tb_System_BusinessCorp b on a.BussId=b.BussId
                            WHERE isnull(a.IsDelete,0)=0 AND isnull(b.IsDelete,0)=0 " + condition;
                DataSet Ds_Order = BussinessCommon.GetList(out pageCount, out Counts, sql, PageIndex, PageSize, "ReceiptDate", 1, "Id", PubConstant.BusinessContionString, FldName);


                sb.Append("{\"Result\":\"true\",");
                if (Ds_Order != null && Ds_Order.Tables.Count > 0 && Ds_Order.Tables[0].Rows.Count > 0)
                {
                    sb.Append("\"data\":[");
                    for (int i = 0; i < Ds_Order.Tables[0].Rows.Count; i++)
                    {
                        DataRow dr = Ds_Order.Tables[0].Rows[i];

                        DataSet Ds_OrderDetails = BussinessCommon.GetShoppingDetailed(dr["Id"].ToString());
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

        #endregion

        #region 生成订单


        /// <summary>
        /// 生成订单        SetOrder
        /// </summary>
        /// <param name="row"></param>
        /// ShoppingDetailed            购物车明细编号【必填】
        /// UserAddressId               收货地址编号【必填】
        /// UserId                      用户编号【必填】
        /// 返回：
        ///     true:""
        ///     false:errmessage
        /// <returns></returns>
        private string SetOrder(DataRow row)
        {
            if (!row.Table.Columns.Contains("ShoppingDetailed") || string.IsNullOrEmpty(row["ShoppingDetailed"].ToString()))
            {
                return JSONHelper.FromString(false, "购物车明细编号不能为空");
            }
            if (!row.Table.Columns.Contains("UserAddressId") || string.IsNullOrEmpty(row["UserAddressId"].ToString()))
            {
                return JSONHelper.FromString(false, "收货地址编号不能为空");
            }
            if (!row.Table.Columns.Contains("UserId") || string.IsNullOrEmpty(row["UserId"].ToString()))
            {
                return JSONHelper.FromString(false, "用户编号不能为空");
            }

            IDbConnection con = new SqlConnection(PubConstant.GetConnectionString("BusinessContionString"));
            string str = "";
            try
            {
                //获取收货地址
                Tb_User_Address UserAddress = BussinessCommon.GetAddressModel(row["UserAddressId"].ToString());

                //获取购物明细
                DataTable Dt_ShoppingDetailed = BussinessCommon.GetShoppingDetailedView(" Id in (" + BussinessCommon.GetShoppingDetailedIdS(row["ShoppingDetailed"].ToString()) + ")");

                if (Dt_ShoppingDetailed == null || Dt_ShoppingDetailed.Rows.Count <= 0)
                {
                    return JSONHelper.FromString(false, "生成订单失败：未找到购物明细");
                }


                Tb_Charge_Receipt Order = new Tb_Charge_Receipt();

                //根据商家ID分组
                var query = from t in Dt_ShoppingDetailed.AsEnumerable()
                            group t by new { t1 = t.Field<string>("BussId") } into m
                            select new
                            {
                                BussId = m.Key.t1
                            };
                if (query.ToList().Count > 0)
                {
                    query.ToList().ForEach(q =>
                    {
                        //生成订单
                        Order = new Tb_Charge_Receipt();
                        Order.Id = Guid.NewGuid().ToString();
                        Order.BussId = q.BussId;
                        Order.OrderId = Guid.NewGuid().ToString();
                        Order.ReceiptSign = DateTime.Now.ToString("yyyyMMddHHmmss") + new Random().Next(1000, 9999).ToString();
                        Order.UserId = row["UserId"].ToString();
                        if (UserAddress != null)
                        {
                            if (UserAddress.Address != "" && UserAddress.Address.Split(',').Length > 0)
                            {
                                Order.Name = UserAddress.Address.Split(',')[0];
                                Order.DeliverAddress = UserAddress.Address.Split(',')[1];
                            }
                            Order.Mobile = UserAddress.Mobile;
                        }
                        else
                        {
                            throw new Exception("该收货地址不存在");
                        }
                        Order.ReceiptDate = DateTime.Now;
                        Order.IsReceive = "未收货";
                        Order.IsDeliver = "未发货";
                        Order.IsPay = "未付款";
                        Order.IsDelete = 0;
                        //查询此商家的所有购物车明细
                        DataRow[] dr = Dt_ShoppingDetailed.Select("BussId=" + q.BussId);
                        //生成订单明细
                        foreach (DataRow item in dr)
                        {
                            Tb_Charge_ReceiptDetail OrderDetail = new Tb_Charge_ReceiptDetail();
                            OrderDetail.RpdCode = Guid.NewGuid().ToString();
                            OrderDetail.ReceiptCode = Order.Id;
                            OrderDetail.ResourcesID = item["ResourcesID"].ToString();
                            OrderDetail.Quantity = AppGlobal.StrToInt(item["Number"].ToString());
                            OrderDetail.SalesPrice = AppGlobal.StrToDec(item["ResourcesSalePrice"].ToString());
                            OrderDetail.DiscountPrice = AppGlobal.StrToDec(item["ResourcesDisCountPrice"].ToString());
                            OrderDetail.GroupPrice = AppGlobal.StrToDec(item["GroupBuyPrice"].ToString());
                            OrderDetail.DetailAmount = AppGlobal.StrToDec(item["SubtotalMoney"].ToString()); ;
                            OrderDetail.RpdMemo = "";
                            OrderDetail.RpdIsDelete = 0;
                            //累计订单金额【取销售价格】
                            Order.Amount += AppGlobal.StrToDec(OrderDetail.SalesPrice.ToString()) * AppGlobal.StrToDec(OrderDetail.Quantity.ToString());
                            //生成订单明细
                            con.Insert<Tb_Charge_ReceiptDetail>(OrderDetail);
                        }
                        //生成订单
                        con.Insert<Tb_Charge_Receipt>(Order);
                        //判断此商家中是否存在该客户
                        string sqlStr = "select BussId from Tb_Customer_List where BussId='" + q.BussId + "' and UserId='" + row["UserId"] + "' and Mobile='" + UserAddress.Mobile + "'";
                        List<string> list = con.Query<string>(sqlStr, null, null, true, null, CommandType.Text).ToList<string>();
                        //如果不存在，将此客户添加至此商家的客户资料中
                        if (list.Count <= 0)
                        {
                            Tb_Customer_List cust = new Tb_Customer_List();
                            cust.Id = Guid.NewGuid().ToString();
                            cust.BussId = q.BussId;
                            cust.UserId = row["UserId"].ToString();
                            cust.Mobile = UserAddress.Mobile;
                            cust.CustName = "";
                            con.Insert<Tb_Customer_List>(cust);
                        }
                    });
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
        #endregion

        #region 获取用户订单数量

        /// <summary>
        /// 获取用户待付款、待收货、已完成订单   GetChargeReceiptSum
        /// </summary>
        /// <param name="row"></param>
        /// UserId:用户ID【必填】
        /// state:订单状态 1 待付款 2 待收货  3 已完成
        /// 返回：
        ///     Data 待付款订单数量
        /// <returns></returns>
        private string GetChargeReceiptSum(DataRow row)
        {
            if (!row.Table.Columns.Contains("UserId") || string.IsNullOrEmpty(row["UserId"].ToString()))
            {
                return JSONHelper.FromString(false, "用户编码不能为空");
            }
            
            string userId = row["UserId"].ToString();

            var sql = @"select   (select count(*)  from Tb_Charge_Receipt as A  INNER JOIN Tb_System_BusinessCorp as B on a.bussiD=  b.bussId   where A.UserId ='" + userId + "'and ISNULL(A.IsDelete,0)= 0 AND ISNULL(b.IsClose,'未关闭')= '未关闭') as own,(select COUNT(*) from Tb_Charge_Receipt  as A  INNER JOIN Tb_System_BusinessCorp as B on a.bussiD=b.bussId where A.UserId = '" + userId + "'  and A.IsPay = '未付款' and ISNULL(A.IsDelete, 0) = 0 AND ISNULL(b.IsClose,'未关闭')= '未关闭') as IsPay,(select COUNT(*) from Tb_Charge_Receipt  as A  INNER JOIN Tb_System_BusinessCorp as B on a.bussiD=b.bussId  where A.UserId = '" + userId + "'and A.IsPay = '已付款' and A.IsReceive = '未收货' and ISNULL(A.IsDelete, 0) = 0 AND ISNULL(b.IsClose,'未关闭')= '未关闭' ) as IsReceive,(select COUNT(*) from Tb_Charge_Receipt as A INNER JOIN Tb_System_BusinessCorp as B on a.bussiD=b.bussId where A.UserId = '" + userId + "'and a.IsReceive = '已收货' and isnull(a.IsEvaluated,0)=0 and ISNULL(a.IsDelete, 0) = 0 AND ISNULL(b.IsClose,'未关闭')= '未关闭') as Evaluation,(select COUNT(*) from Tb_Charge_Receipt  as A  INNER JOIN Tb_System_BusinessCorp as B on a.bussiD=b.bussId where  a.UserId = '" + userId + "'and a.IsPay = '已付款' and a.HandleState = '已完成' and ISNULL(a.IsDelete, 0) = 0 AND ISNULL(b.IsClose,'未关闭')= '未关闭') as HandleState";
            IDbConnection con = new SqlConnection(PubConstant.GetConnectionString("BusinessContionString"));
            DataSet ds = con.ExecuteReader(sql, null, null, null, CommandType.Text).ToDataSet();


            return JSONHelper.FromString(ds.Tables[0]);
        }

        private string GetChargeReceiptSum_v2(DataRow row)
        {
            if (!row.Table.Columns.Contains("UserId") || string.IsNullOrEmpty(row["UserId"].ToString()))
            {
                return JSONHelper.FromString(false, "用户编码不能为空");
            }

            string userId = row["UserId"].ToString();

            var sql = @"select (select count(*)  from Tb_Charge_Receipt where UserId = '" + userId + "' and ISNULL(IsDelete,0)= 0) as own," +
                "(select COUNT(*) from Tb_Charge_Receipt where UserId = '" + userId + "' and IsPay = '未付款' and ISNULL(IsDelete, 0) = 0) as WaitingPay," +
                "(select COUNT(*) from Tb_Charge_Receipt where UserId = '" + userId + "' and IsPay = '已付款' and isnull(IsDeliver,'未发货')='未发货' and ISNULL(IsDelete, 0) = 0) as WaitingDeliver," +
                "(select COUNT(*) from Tb_Charge_Receipt where UserId = '" + userId + "' and IsPay = '已付款' and isnull(IsDeliver,'已发货')='已发货' and isnull(IsReceive,'未收货')='未收货' and isnull(IsEvaluated,0)=0 and ISNULL(IsDelete, 0) = 0) as WaitingReceive," +
                "(select COUNT(*) from Tb_Charge_Receipt where UserId = '" + userId + "' and IsPay = '已付款' and isnull(IsDeliver,'已发货')='已发货' and isnull(IsReceive,'未收货')='已收货' and isnull(IsEvaluated,0)=0 and ISNULL(IsDelete, 0) = 0) as WaitingEvaluate;";
            IDbConnection con = new SqlConnection(PubConstant.GetConnectionString("BusinessContionString"));
            DataSet ds = con.ExecuteReader(sql, null, null, null, CommandType.Text).ToDataSet();


            return JSONHelper.FromString(ds.Tables[0]);
        }


        #endregion

        #region 添加订单评价
        /// <summary>
        /// 添加订单评价  InsertResourcesCommodityEvaluation
        /// </summary>
        /// <param name="row"></param>
        /// RpdCode             订单明细表ID【必填】
        /// ResourcesID         商品编码【必填】
        /// EvaluateContent     评价内容
        /// UploadImg           上传图片
        /// Star                商品评星
        /// EvaluateDate        评价时间
        /// 返回：
        ///     false：错误信息
        ///     true：操作成功
        /// <returns></returns>
        private string InsertResourcesCommodityEvaluation(DataRow row)
        {
            if (!row.Table.Columns.Contains("RpdCode") || string.IsNullOrEmpty(row["RpdCode"].ToString()))
            {
                return JSONHelper.FromString(false, "评价内容不能为空");
            }

            string RpdCode = row["RpdCode"].ToString();

            IDbConnection con = new SqlConnection(PubConstant.GetConnectionString("BusinessContionString"));
            string str = "";
            try
            {
                Tb_Resources_CommodityEvaluation RCE = new Tb_Resources_CommodityEvaluation();

                foreach (string item in RpdCode.Split(','))
                {
                    string[] str_Ev = item.Split(':');
                    RCE.Id = Guid.NewGuid().ToString();
                    RCE.RpdCode = str_Ev[0];
                    RCE.Star = AppGlobal.StrToDec(str_Ev[2]);
                    RCE.EvaluateDate = DateTime.Now;
                    RCE.IsDelete = 0;
                    RCE.EvaluateContent = str_Ev[1];

                    Tb_Charge_ReceiptDetail m = con.Query<Tb_Charge_ReceiptDetail>("select * from Tb_Charge_ReceiptDetail where RpdCode=@ResourcesID", new { ResourcesID = str_Ev[0] }).SingleOrDefault<Tb_Charge_ReceiptDetail>();
                    if (m != null)
                    {
                        RCE.ResourcesID = m.ResourcesID;
                    }

                    con.Insert<Tb_Resources_CommodityEvaluation>(RCE);
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
                return JSONHelper.FromString(true, "操作成功");
            }

        }
        #endregion

        #region 删除订单

        /// <summary>
        /// 删除订单  DelChargeReceipt
        /// </summary>
        /// <param name="row"></param>
        /// ReceiptCode        商品订单票据编码
        /// 返回：
        ///     true:""
        ///     false:删除失败
        /// <returns></returns>
        private string DelChargeReceipt(DataRow row)
        {
            if (!row.Table.Columns.Contains("ReceiptCode") || string.IsNullOrEmpty(row["ReceiptCode"].ToString()))
            {
                return JSONHelper.FromString(false, "订单不能为空");
            }
            string str = "";
            try
            {
                IDbConnection con = new SqlConnection(PubConstant.GetConnectionString("BusinessContionString"));

                Tb_Charge_Receipt charge = BussinessCommon.GetChargeReceiptModel(row["ReceiptCode"].ToString());
                if (charge != null)
                {
                    charge.IsDelete = 1;
                    con.Update<Tb_Charge_Receipt>(charge);
                    con.ExecuteScalar("update Tb_Charge_ReceiptDetail set RpdIsDelete=1 where ReceiptCode=@ReceiptCode", new { ReceiptCode = charge.Id }, null, null, CommandType.Text);
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
        #endregion

        #region 修改订单处理状态
        /// <summary>
        /// 修改订单处理状态  UpdateChargeHandleState
        /// </summary>
        /// <param name="row"></param>
        /// ReceiptCode        商品订单票据编码
        /// 返回：
        ///     true:""
        ///     false:删除失败
        /// <returns></returns>
        private string UpdateChargeHandleState(DataRow row)
        {
            if (!row.Table.Columns.Contains("ReceiptCode") || string.IsNullOrEmpty(row["ReceiptCode"].ToString()))
            {
                return JSONHelper.FromString(false, "订单不能为空");
            }
            string str = "";
            try
            {

                IDbConnection con = new SqlConnection(PubConstant.GetConnectionString("BusinessContionString"));

                Tb_Charge_Receipt charge = BussinessCommon.GetChargeReceiptModel(row["ReceiptCode"].ToString());
                if (charge != null)
                {
                    charge.IsDeliver = "已发货";
                    charge.IsReceive = "已收货";
                    charge.ConfirmReceivedTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    con.Update<Tb_Charge_Receipt>(charge);
                }
                else
                {
                    str = "修改失败";
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
        #endregion

    }
}
