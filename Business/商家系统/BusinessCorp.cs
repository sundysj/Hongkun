using Common;
using Common.Enum;
using Common.Extenions;
using Dapper;
using MobileSoft.DBUtility;
using Model.Model.Buss;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KdGoldAPI.KdApiSearchMonitor;

namespace Business
{
    public class BusinessCorp : PubInfo
    {

        public BusinessCorp()
        {
            base.Token = "202007131638_BusinessCorp";
        }

        public override void Operate(ref Transfer Trans)
        {

            DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
            DataRow Row = dAttributeTable.Rows[0];

            switch (Trans.Command)
            {
                case "BussinessDispatchingType":
                    Trans.Result = BussinessDispatchingType(Row);//获取首页商品
                    break;
                case "ConfirmOrder":
                    Trans.Result = ConfirmOrder(Row);
                    break;
                case "DistributionInfo":
                    Trans.Result = DistributionInfo(Row);
                    break;
                case "CourierCompany":
                    Trans.Result = CourierCompany(Row);
                    break;
                case "BusinessAddress":
                    Trans.Result = BusinessAddress(Row);
                    break;
                case "ExternalContactNumber":
                    Trans.Result = ExternalContactNumber(Row);
                    break;

                default:
                    Trans.Result = new ApiResult(false, "未知错误").toJson();
                    break;
            }
        }

        /// <summary>
        /// 获取商家配送类型
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public String BussinessDispatchingType(DataRow row)
        {
            if (!row.Table.Columns.Contains("BusinessId") || string.IsNullOrEmpty(row["BusinessId"].AsString()))
            {
                return new ApiResult(false, "缺少参数BusinessId").toJson();
            }
            String bussId = row["BusinessId"].ToString();
            try
            {
                using (var conn = new SqlConnection(PubConstant.BusinessContionString))
                {
                    String sqlStr = "SELECT  [DispatchingType] FROM Tb_System_BusinessCorp WHERE BussId = @BussId; ";
                    var dispatchingType = conn.QueryFirstOrDefault<String>(sqlStr, new { BussId = bussId });
                    if (!String.IsNullOrEmpty(dispatchingType))
                    {
                        String[] result = dispatchingType.Split(',');
                        return new ApiResult(true, result).toJson();
                    }
                    return new ApiResult(false, "获取商家配送类型失败").toJson();
                }
            }
            catch (Exception ex)
            {
                return new ApiResult(false, "获取商家配送类型错误").toJson();
            }
        }

        /// <summary>
        /// 确认收货
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private String ConfirmOrder(DataRow row)
        {
            if (!row.Table.Columns.Contains("OrderId") || string.IsNullOrEmpty(row["OrderId"].AsString()))
            {
                return new ApiResult(false, "缺少参数OrderId").toJson();
            }
            String orderId = row["OrderId"].ToString();
            try
            {
                using (var conn = new SqlConnection(PubConstant.BusinessContionString))
                {
                    String updateStr = @" UPDATE Tb_Charge_Receipt SET IsReceive='已收货' ,ConfirmReceivedTime =GETDATE()   WHERE Id= @Id ";
                    var count = conn.Execute(updateStr, new { Id = orderId });
                    if (count > 0) return new ApiResult(true, "成功").toJson();
                    return new ApiResult(false, "失败").toJson();
                }
            }
            catch (Exception ex)
            {
                return new ApiResult(false, "失败").toJson();
            }
        }

        /// <summary>
        /// 配送信息
        /// </summary>
        /// <returns></returns>
        private String DistributionInfo(DataRow row)
        {
            if (!row.Table.Columns.Contains("OrderId") || string.IsNullOrEmpty(row["OrderId"].AsString()))
            {
                return new ApiResult(false, "缺少参数OrderId").toJson();
            }
            String orderId = row["OrderId"].ToString();
            try
            {
                using (var conn = new SqlConnection(PubConstant.BusinessContionString))
                {
                    String updateStr = @" SELECT * FROM Tb_Charge_Receipt  WHERE Id= @Id or OrderId=@Id ";
                    var data = conn.QueryFirstOrDefault<Tb_Charge_Receipt>(updateStr, new { Id = orderId });

                    if (data != null)
                    {
                        String businessNameSql = "SELECT BussName FROM Tb_System_BusinessCorp WHERE BussId=@bussId";
                        var businessName = conn.QueryFirstOrDefault<String>(businessNameSql, new { bussId = data.BussId });

                        Object result = null;
                        if (data.DispatchingType == (int)BusiinessDispatchingEnum.Dispatching)
                        {
                            result = new
                            {
                                RequestDeliveryTime = data.RequestDeliveryTime,
                                DeliveredBy = data.DeliveredBy,
                                DeliveredPhone = data.DeliveredPhone,
                                IsSendOut = data.IsSendOut,
                                ExpectedDeliveryTime = data.ExpectedDeliveryTime,
                            };
                        }
                        if (data.DispatchingType == (int)BusiinessDispatchingEnum.TakeTheir)
                        {
                            result = new
                            {
                                BusinessName = businessName,
                                EstimatedPickUpTime = data.EstimatedPickUpTime,
                                MerchantPickUpTime = data.MerchantPickUpTime,
                                MerchantPickUpLocation = data.MerchantPickUpLocation,
                                PickUpContacts = data.PickUpContacts,
                                PickUpContactsPhone = data.PickUpContactsPhone,
                                PickUpRemarks = data.PickUpRemarks,
                                ExtractionCode = data.ExtractionCode
                            };
                        }
                        return new ApiResult(true, result).toJson();
                    }
                    return new ApiResult(false, "失败").toJson();
                }
            }
            catch (Exception ex)
            {
                return new ApiResult(false, "失败").toJson();
            }
        }

        /// <summary>
        /// 快递公司姓名
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private String CourierCompany(DataRow row)
        {
            if (!row.Table.Columns.Contains("ExpressName") || string.IsNullOrEmpty(row["ExpressName"].AsString()))
            {
                return new ApiResult(false, "缺少参数ExpressName").toJson();
            }
            String expressName = row["ExpressName"].ToString();
            try
            {
                var name = EnumHelper.GetEnumByDesc(typeof(ShipperCode), expressName);
                return new ApiResult(true, name).toJson();
            }
            catch (Exception ex)
            {
                return new ApiResult(false, "失败").toJson();
            }
        }

        /// <summary>
        /// 商家地址
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private String BusinessAddress(DataRow row)
        {
            if (!row.Table.Columns.Contains("BusinessId") || string.IsNullOrEmpty(row["BusinessId"].AsString()))
            {
                return new ApiResult(false, "缺少参数BusinessId").toJson();
            }
            String businessId = row["BusinessId"].ToString();
            try
            {
                String result = String.Empty;
                using (var conn = new SqlConnection(PubConstant.BusinessContionString))
                {
                    String sqlStr = "SELECT Province,City,Area,BussAddress FROM Tb_System_BusinessCorp WHERE BussId = @BussId; ";
                    var address = conn.QueryFirstOrDefault(sqlStr, new { BussId = businessId });
                    if (null != address)
                    {
                        result = String.Format("{0}{1}{2}{3}", address.Province, address.City, address.Area, address.BussAddress);
                        return new ApiResult(true, result).toJson();
                    }
                    return new ApiResult(false, "获取商家地址").toJson();
                }
            }
            catch (Exception ex)
            {
                return new ApiResult(false, "失败").toJson();
            }
        }

        /// <summary>
        ///  物业ERP--总部--外部信息--联系我们电话
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private String ExternalContactNumber(DataRow row)
        {
            int corpID = 0;
            if (
                !row.Table.Columns.Contains("CorpID")
                || string.IsNullOrEmpty(row["CorpID"].AsString())
                || !int.TryParse(row["CorpID"].ToString(), out corpID)
                )
            {
                return new ApiResult(false, "缺少参数CorpID").toJson();
            }

            try
            {

                String result = String.Empty;
                using (var conn = new SqlConnection(PubConstant.tw2bsConnectionString))
                {
                    String sqlStr = "SELECT TOP 1 CorpTel  FROM [PMS_Bs].[dbo].[Tb_Corp_Contact] where CorpID=@CorpID";
                    result = conn.QueryFirstOrDefault<String>(sqlStr, new { CorpID = corpID });
                    if (null != result)
                    {
                        return new ApiResult(true, result).toJson();
                    }
                    return new ApiResult(false, "获取联系电话").toJson();
                }
            }
            catch (Exception ex)
            {
                return new ApiResult(false, "失败").toJson();
            }
        }
    }
}
