using allinpay.utils;
using Common;
using Dapper;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using swiftpass.utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Business
{
    public class PMSFeesManage : PubInfo
    {
        public PMSFeesManage()
        {
            base.Token = "20200121PMSFeesManage";
        }

        public override void Operate(ref Transfer Trans)
        {
            DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
            DataRow Row = dAttributeTable.Rows[0];

            //验证登录
            if (!new Login().isLogin(ref Trans))
                return;

            //防止未捕获异常出现
            try
            {
                switch (Trans.Command)
                {
                    case "ReceFees":                               // App权限
                        Trans.Result = ReceFees(Row);
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


        private string ReceFees(DataRow row)
        {
            #region 获取基本参数
            string CommID = string.Empty;
            if (row.Table.Columns.Contains("CommID"))
            {
                CommID = row["CommID"].ToString();
            }
            string RoomID = string.Empty;
            if (row.Table.Columns.Contains("RoomID"))
            {
                RoomID = row["RoomID"].ToString();
            }
            string CustID = string.Empty;
            if (row.Table.Columns.Contains("CustID"))
            {
                CustID = row["CustID"].ToString();
            }
            #endregion
            #region 计算金额
            if (!row.Table.Columns.Contains("PayData") || string.IsNullOrEmpty(row["PayData"].ToString()))
            {
                return new ApiResult(false, "缺少参数PayData").toJson();
            }
            string PayData = row["PayData"].ToString();
            if (!CheckPayData(Global_Fun.BurstConnectionString(Convert.ToInt32(CommID), Global_Fun.BURST_TYPE_CHARGE), Convert.ToInt64(CustID), Convert.ToInt64(RoomID), PayData, out decimal Amt, out string errMsg, true, false, !"1940".Equals(Global_Var.LoginCorpID)))
            {
                return new ApiResult(false, errMsg).toJson();
            }
            if (Amt <= 0.00M)
            {
                return new ApiResult(false, "金额必须大于0").toJson();
            }
            #endregion
            JObject jObj = JObject.Parse(PayData);
            int Type = (int)jObj["Type"];
            if (Type != 1)
            {
                return new ApiResult(false, "收费类型有误").toJson();
            }
            JArray Data = (JArray)jObj["Data"];
            StringBuilder FeesIds = new StringBuilder();
            foreach (JObject item in Data)
            {
                FeesIds.Append((string)item["FeesId"] + ",");
            }
            if (!PubInfo.ReceFees(Global_Fun.BurstConnectionString(Convert.ToInt32(CommID), Global_Fun.BURST_TYPE_CHARGE), out long ReceID, CommID, CustID, RoomID, FeesIds.ToString(), 0.00M, "员工APP", "扫码支付", Global_Var.UserCode, 1))
            {
                return new ApiResult(false, "下账失败").toJson();
            }
            return new ApiResult(true, "下账成功").toJson();
        }

    }
}
