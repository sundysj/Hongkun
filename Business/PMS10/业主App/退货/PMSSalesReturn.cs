using Common;
using Common.Enum;
using Common.Extenions;
using Dapper;
using MobileSoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class PMSSalesReturn : PubInfo
    {
        public PMSSalesReturn()
        {
            base.Token = "202007211029PMSSalesReturn";
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
                    case "ReturnRequest":              // 申请退货
                        Trans.Result = ReturnRequest(Row);
                        break;
                    case "ReturnInfoList":              // 退货信息查询
                        Trans.Result = ReturnInfoList(Row);
                        break;
                    case "SalesReturn":              // 退货发货
                        Trans.Result = SalesReturn(Row);
                        break;
                    case "DistributionSalesReturn":              // 配送自提退货发货
                        Trans.Result = DistributionSalesReturn(Row);
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
        /// 申请退货
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private String ReturnRequest(DataRow row)
        {
            if (!row.Table.Columns.Contains("OrderId") || string.IsNullOrEmpty(row["OrderId"].ToString()))
            {
                return new ApiResult(false, "参数错误").toJson();
            }
            if (!row.Table.Columns.Contains("Reason") || string.IsNullOrEmpty(row["Reason"].ToString()))
            {
                return new ApiResult(false, "参数错误").toJson();
            }
            String orderId = row["OrderId"].ToString();
            String reason = row["Reason"].ToString();
            String explain = String.Empty;
            String imgs = String.Empty;

            if (row.Table.Columns.Contains("Explain") && !string.IsNullOrEmpty(row["Explain"].ToString())) explain = row["Explain"].ToString();
            if (row.Table.Columns.Contains("Imgs") && !string.IsNullOrEmpty(row["Imgs"].ToString())) imgs = row["Imgs"].ToString();

            try
            {
                using (var conn = new SqlConnection(PubConstant.BusinessContionString))
                {
                    String sqlStr = "select IsReceive,ConfirmReceivedTime from Tb_Charge_Receipt where Id=@Id";
                    var orderInfo = conn.QueryFirstOrDefault(sqlStr, new { Id = orderId });
                    if (null == orderInfo) return new ApiResult(false, "订单信息不存在").toJson();
                    if (orderInfo.IsReceive != "已收货") return new ApiResult(false, "订单未收货").toJson();
                    if ((DateTime.Now - ((DateTime)orderInfo.ConfirmReceivedTime)).Days > 7) return new ApiResult(false, "订单已超过退款时间").toJson();
                    sqlStr = @"UPDATE Tb_Charge_Receipt SET IsRetreat='是',CustomerApplicationTime=GETDATE(),RetreatReason=@RetreatReason,RetreatExplain=@RetreatExplain,RetreatImages=@RetreatImages  WHERE ID=@Id";
                    var count = conn.Execute(sqlStr, new { RetreatReason = reason, RetreatExplain = explain, RetreatImages = imgs, Id = orderId });
                    if (count > 0) return new ApiResult(true, "申请退货成功").toJson();
                    return new ApiResult(false, "申请退货失败").toJson();
                }
            }
            catch (Exception ex)
            {
                return new ApiResult(false, "申请退货失败").toJson();
            }
        }

        /// <summary>
        /// 退货信息查询
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private String ReturnInfoList(DataRow row)
        {
            if (!row.Table.Columns.Contains("OrderId") || string.IsNullOrEmpty(row["OrderId"].ToString()))
            {
                return new ApiResult(false, "参数错误").toJson();
            }
            String orderId = row["OrderId"].ToString();
            try
            {
                using (var conn = new SqlConnection(PubConstant.BusinessContionString))
                {
                    String sqlStr = @"SELECT  Id,[CustomerApplicationTime],[RetreatReason],[RetreatExplain],[RetreatImages] ,[IsAgree],[DispatchingType],Consignee,DeliveryInitiationTime
                                      ,[AgreeTime],[RetreatAddress],[Recipient],[RecipientTelephone] ,[DisagreeReason],[RetreatTime]
                                      ,[RetreatCourierNumber],[RetreatExpressName],[IsMerchantReceived],[MerchantReceivedTime]
                                      ,[IsPlatformRefund],[PlatformRefundTime],[CancellationType],[CancellationReason]
                                      ,[IsCancellation],[CancellationTime],[IsRetreat],[ConfirmReceivedTime]
                                  FROM [BussinessDb].[dbo].[Tb_Charge_Receipt] where Id=@OrderId";
                    var orderInfo = conn.QueryFirstOrDefault(sqlStr, new { OrderId = orderId });
                    if (null == orderInfo) return new ApiResult(true, "订单不存在").toJson();

                    List<Object> result = new List<Object>();
                    int status = 0;

                    //判断是否退货
                    if (orderInfo.IsRetreat == "是")
                    {
                        status = (int)OrderSalesReturnEnum.ApplyFor;
                        result.Add(new
                        {
                            Type = "退货申请",
                            Time = orderInfo.CustomerApplicationTime,
                            Reason = orderInfo.RetreatReason,
                            Explain = orderInfo.RetreatExplain,
                            Images = orderInfo.RetreatImages,
                        });
                        //判断是否审核状态
                        if (!String.IsNullOrEmpty(orderInfo.IsAgree))
                        {
                            status = orderInfo.IsAgree == "是" ? (int)OrderSalesReturnEnum.Agree : (int)OrderSalesReturnEnum.Refuse;
                            if (status == 2) orderInfo.DisagreeReason = "您的退货申请已经审核通过";
                            result.Add(new
                            {
                                Type = "商家审核",
                                IsAgree = orderInfo.IsAgree,
                                Time = orderInfo.AgreeTime,
                                Reason = orderInfo.DisagreeReason,
                                RetreatAddress = orderInfo.RetreatAddress,
                                Recipient = orderInfo.RetreatAddress,
                                RecipientTelephone = orderInfo.RecipientTelephone,
                            });

                            //判断是否发货
                            if (
                                orderInfo.IsAgree == "是" &&
                                (orderInfo.DispatchingType == 1 ? !String.IsNullOrEmpty(orderInfo.RetreatExpressName) : !String.IsNullOrEmpty(orderInfo.Consignee))
                                )
                            {
                                status = (int)OrderSalesReturnEnum.SalesReturn;
                                result.Add(new
                                {
                                    Type = "退货办理",
                                    Time = orderInfo.RetreatTime,
                                    RetreatCourierNumber = orderInfo.RetreatCourierNumber,
                                    RetreatExpressName = orderInfo.RetreatExpressName,
                                    Consignee = orderInfo.Consignee,
                                    DeliveryInitiationTime = orderInfo.DeliveryInitiationTime,
                                });

                                //判断是否到货
                                if (!String.IsNullOrEmpty(orderInfo.IsMerchantReceived) && orderInfo.IsMerchantReceived == "是")
                                {
                                    status = (int)OrderSalesReturnEnum.Receiving;
                                    result.Add(new
                                    {
                                        Type = "收货确认",
                                        Time = orderInfo.MerchantReceivedTime,
                                    });

                                    //平台是否退款
                                    if (!String.IsNullOrEmpty(orderInfo.IsPlatformRefund))
                                    {
                                        status = (int)OrderSalesReturnEnum.Refund;
                                        result.Add(new
                                        {
                                            Type = "退款办理",
                                            Time = orderInfo.PlatformRefundTime,
                                            Reason = "退款已完成！",
                                        });
                                    }
                                }
                            }
                        }
                    }
                    return new ApiResult(true, new { Data = result, Status = status }).toJson();
                }
            }
            catch (Exception ex)
            {
                return new ApiResult(false, "获取退货信息失败").toJson();
            }
        }

        /// <summary>
        /// 退货发货
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private String SalesReturn(DataRow row)
        {
            if (!row.Table.Columns.Contains("OrderId") || string.IsNullOrEmpty(row["OrderId"].ToString()))
            {
                return new ApiResult(false, "参数错误").toJson();
            }
            if (!row.Table.Columns.Contains("RetreatCourierNumber") || string.IsNullOrEmpty(row["RetreatCourierNumber"].ToString()))
            {
                return new ApiResult(false, "参数错误").toJson();
            }
            if (!row.Table.Columns.Contains("RetreatExpressName") || string.IsNullOrEmpty(row["RetreatExpressName"].ToString()))
            {
                return new ApiResult(false, "参数错误").toJson();
            }

            String order = row["OrderId"].ToString();
            String num = row["RetreatCourierNumber"].ToString();
            String name = row["RetreatExpressName"].ToString();
            DateTime time = DateTime.Now;

            if (row.Table.Columns.Contains("Time") && !string.IsNullOrEmpty(row["Time"].ToString()))
            {
                DateTime.TryParse(row["Time"].ToString(), out time);
            }
            try
            {
                String orderInfo = null;
                using (var conn = new SqlConnection(PubConstant.BusinessContionString))
                {
                    String sqlStr = "select IsAgree from Tb_Charge_Receipt where Id=@Id";
                    orderInfo = conn.QueryFirstOrDefault<String>(sqlStr, new { Id = order });
                    if (null == orderInfo) return new ApiResult(false, "订单信息不存在").toJson();
                    if (String.IsNullOrEmpty(orderInfo) || orderInfo == "否")
                        return new ApiResult(false, "当前订单审核失败").toJson();

                    sqlStr = @" UPDATE Tb_Charge_Receipt SET  RetreatExpressName=@RetreatExpressName,RetreatCourierNumber=@RetreatCourierNumber,RetreatTime=@RetreatTime WHERE Id=@Id ";
                    var count = conn.Execute(sqlStr, new { RetreatExpressName = name, RetreatCourierNumber = num, RetreatTime = time, Id = order });

                    if (count > 0) return new ApiResult(true, "提交信息成功").toJson();
                    return new ApiResult(false, "提交信息失败").toJson();
                }
            }
            catch (Exception ex) { return new ApiResult(false, "提交信息错误").toJson(); }
        }



        /// <summary>
        /// 配送自提退货发货
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private String DistributionSalesReturn(DataRow row)
        {
            if (!row.Table.Columns.Contains("OrderId") || string.IsNullOrEmpty(row["OrderId"].ToString()))
            {
                return new ApiResult(false, "参数错误").toJson();
            }
            if (!row.Table.Columns.Contains("Consignee") || string.IsNullOrEmpty(row["Consignee"].ToString()))
            {
                return new ApiResult(false, "参数错误").toJson();
            }

            String order = row["OrderId"].ToString();
            String consignee = row["Consignee"].ToString();
            DateTime time = DateTime.Now;
            if (row.Table.Columns.Contains("Time") && !string.IsNullOrEmpty(row["Time"].ToString()))
            {
                DateTime.TryParse(row["Time"].ToString(), out time);
            }
            try
            {
                String orderInfo = null;
                using (var conn = new SqlConnection(PubConstant.BusinessContionString))
                {
                    String sqlStr = "select IsAgree from Tb_Charge_Receipt where Id=@Id";
                    orderInfo = conn.QueryFirstOrDefault<String>(sqlStr, new { Id = order });
                    if (null == orderInfo) return new ApiResult(false, "订单信息不存在").toJson();
                    if (String.IsNullOrEmpty(orderInfo) || orderInfo == "否")
                        return new ApiResult(false, "当前订单审核失败").toJson();

                    sqlStr = @" UPDATE Tb_Charge_Receipt SET Consignee=@Consignee,DeliveryInitiationTime=@DeliveryInitiationTime,RetreatTime=@RetreatTime WHERE Id=@Id ";
                    var count = conn.Execute(sqlStr, new { Consignee = consignee, DeliveryInitiationTime = time, Id = order, RetreatTime = time });

                    if (count > 0) return new ApiResult(true, "提交信息成功").toJson();
                    return new ApiResult(false, "提交信息失败").toJson();
                }
            }
            catch (Exception ex) { return new ApiResult(false, "提交信息错误").toJson(); }
        }
    }
}
