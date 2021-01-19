using Common;
using Dapper;
using MobileSoft.DBUtility;
using Model.App;
using Model.Buss;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class PMSFreight : PubInfo
    {
        public PMSFreight()
        {
            base.Token = "202007221852PMSFreight";
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
                    //发送验证码
                    case "GetFreightBYShopCar":
                        Trans.Result = GetFreightBYShopCar(dAttributeTable);
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


        private String GetFreightBYShopCar(DataTable dataTable)
        {
            //var shoppingsInfo = dataTable.DataSet.Tables["Shoppings"].Rows;
            List<String> shoppingId = new List<String>();
            DataRow row = dataTable.Rows[0];
            if (!row.Table.Columns.Contains("Province") || string.IsNullOrEmpty(row["Province"].ToString()))
            {
                return new ApiResult(false, "参数错误").toJson();
            }
            if (!row.Table.Columns.Contains("Shoppings") || string.IsNullOrEmpty(row["Shoppings"].ToString()))
            {
                return new ApiResult(false, "参数错误").toJson();
            }

            shoppingId = JsonConvert.DeserializeObject<List<String>>(row["Shoppings"].ToString());
            String province = row["Province"].ToString();
            var fee = GetGetFreight(province, shoppingId);
            if (fee >= 0) return new ApiResult(true, fee).toJson();
            if (fee == -2) return new ApiResult(false, "对不起，您的收货地址不在配送范围内").toJson();
            return new ApiResult(false, "获取失败").toJson();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="address"></param>
        /// <param name="shoppings"></param>
        /// <returns></returns>
        public static decimal GetGetFreight(String address, List<String> shoppings)
        {
            if (address.Length > 3) address = address.Substring(0, 3);
            try
            {
                decimal fee = 0M;
                using (var conn = new SqlConnection(PubConstant.BusinessContionString))
                {
                    String sqlStr = @"SELECT Area,FirstCost,FirstRange,ContinuedCost,T.Number FROM (
                                    SELECT 
									  CASE
											WHEN C.FreightID is null THEN  D.FreightID
											WHEN  C.FreightID='' THEN  D.FreightID
											ELSE  C.FreightID
									END AS FreightID
                                    ,A.ResourcesID,A.Number FROM 
                                    (SELECT * FROM   Tb_ShoppingCar where Id in @ShoppingId ) AS A
                                    LEFT JOIN 
                                    ( SELECT * FROM   Tb_ShoppingDetailed ) as B on A.Id=b.ShoppingId
                                    LEFT JOIN 
                                    (SELECT * FROM  Tb_ResourcesSpecificationsPrice) AS C ON C.SpecId= B.SpecId AND C.ResourcesID=A.ResourcesID
                                    LEFT JOIN 
                                    (SELECT * FROM  Tb_Resources_Details) AS D ON D.ResourcesID= A.ResourcesID
                                    )AS T 
                                    LEFT JOIN (SELECT * FROM  Tb_Resources_FreightDetail where ISNULL (IsDelete,0)=0) AS U ON T.FreightID= U.FreightID
                                    WHERE U.ID IS NOT NULL ";
                    var freightDetails = conn.Query<ReturnFeeModel>(sqlStr, new { ShoppingId = shoppings });
                    if (null == freightDetails || !freightDetails.Any()) return fee;
                    //if (!freightDetails.Select(l => l.Area).Contains(address)) return -2;

                    if (!String.Join(",", freightDetails.Select(l => l.Area)).Contains(address)) return -2;

                    foreach (var model in freightDetails)
                    {
                        if (String.IsNullOrEmpty(model.Area)) continue;
                        if (!model.Area.Contains(address)) continue;
                        fee += (decimal)model.FirstCost;
                        if (model.FirstRange < model.Number)
                        {
                            fee += ((int)model.Number - (int)model.FirstRange) * (decimal)model.ContinuedCost;
                        }
                    }
                    return fee;
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
    }
}
