using Business.PMS10.报事.Models;
using Common;
using Dapper;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using static Dapper.SqlMapper;

namespace Business
{
    public class PMSCustomerInfo : PubInfo
    {
        public PMSCustomerInfo()
        {
            base.Token = "20191107PMSCustomerInfo";
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
                    case "GetCustomerSimpleInfo":               // 获取公区方位列表
                        Trans.Result = GetCustomerSimpleInfo(Row);
                        break;
                    case "GetCustomerDetailInfo":               // 获取公区方位列表
                        Trans.Result = GetCustomerDetailInfo(Row);
                        break;
                    case "GetCustomerHouseholdSimpleInfo":
                        Trans.Result = GetCustomerHouseholdSimpleInfo(Row);
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
        /// 
        /// </summary>
        private string GetCustomerSimpleInfo(DataRow row)
        {
            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {

            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        private string GetCustomerDetailInfo(DataRow row)
        {
            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {

            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        private string GetCustomerHouseholdSimpleInfo(DataRow row)
        {
            if (!row.Table.Columns.Contains("CustID") || string.IsNullOrEmpty(row["CustID"].AsString()))
            {
                return new ApiResult(false, "缺少参数CustID").toJson();
            }
            var custId = AppGlobal.StrToLong(row["CustID"].AsString());
            var roomId = 0L;

            if (!row.Table.Columns.Contains("RoomID") || string.IsNullOrEmpty(row["RoomID"].AsString()))
            {
                roomId = AppGlobal.StrToLong(row["RoomID"].AsString());
            }

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = @"SELECT CustID,CustName,isnull(MobilePhone,LinkmanTel) AS CustMobile 
                            FROM Tb_HSPR_Customer WHERE CustID=@CustID;

                            SELECT HoldID,isnull(MemberName,isnull(Surname,isnull(Name,'未登记姓名'))) AS HoldName,
                                   isnull(MobilePhone,LinkManTel) AS HoldMobile,isnull(b.DictionaryName,'未知') AS RelationShip 
                            FROM Tb_HSPR_Household a 
                            LEFT JOIN Tb_Dictionary_Relation b ON a.Relationship=b.DictionaryCode
                            WHERE a.CustID=@CustID AND isnull(a.IsDelete,0)=0";

                if (roomId != 0)
                {
                    sql += " AND a.RoomID=@RoomID";
                }

                var reader = conn.QueryMultiple(sql, new { CustID = custId, RoomID = roomId });
                var custInfo = reader.Read().FirstOrDefault();
                var holdInfo = reader.Read();

                return new ApiResult(true, new { CustInfo = custInfo, HoldInfo = holdInfo }).toJson();
            }
        }
    }
}
