using Dapper;
using MobileSoft.Common;
using System.Data;
using System.Data.SqlClient;

namespace Business
{
    /// <summary>
    /// 优惠券管理接口
    /// </summary>
    public class CouponManage:PubInfo
    {
        public CouponManage()
        {
            base.Token = "20170823CouponManage";
        }
        public override void Operate(ref Common.Transfer Trans)
        {
            Trans.Result = JSONHelper.FromString(false, "未知错误!");
            DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
            DataRow Row = dAttributeTable.Rows[0];
            switch (Trans.Command)
            {
                case "GetCouponBalanceList":// 获取业主优惠券余额，以商家为单位
                    Trans.Result = GetCouponBalanceList(Row);
                    break;
                case "GetCouponUsedHistoryList":// 优惠券使用历史记录
                    Trans.Result = GetCouponUsedHistoryList(Row);
                    break;
                case "GetCanUseCouponBalanceInBuss":// 付款时，获取当前可用的优惠券余额
                    Trans.Result = GetCanUseCouponBalanceInBuss(Row);
                    break;
            }
        }

        /// <summary>
        /// 获取业主优惠券余额
        /// </summary>
        public string GetCouponBalanceList(DataRow row)
        {
            if (!row.Table.Columns.Contains("UserId") || string.IsNullOrEmpty(row["UserId"].ToString()))
            {
                return JSONHelper.FromString(false, "用户id不能为空");
            }

            if (!row.Table.Columns.Contains("CorpId") || string.IsNullOrEmpty(row["CorpId"].ToString()))
            {
                return JSONHelper.FromString(false, "物业公司id不能为空");
            }

            using (IDbConnection conn = new SqlConnection(ConnectionDb.GetBusinessConnection()))
            {
                DataTable dt = conn.ExecuteReader(@"SELECT b.CorpId,b.BussId,b.Balance, 
                                                    BussName = (SELECT top 1 BussName FROM Tb_System_BusinessCorp a WHERE a.BussId = b.BussId) 
                                                    FROM Tb_Resources_CouponBalance b WHERE UserId=@UserId AND CorpId=@CorpId", 
                                                    new { UserId = row["UserId"], CorpId = row["CorpId"] }).ToDataSet().Tables[0];
                
                return JSONHelper.FromString(dt);
            }
        }

        /// <summary>
        /// 优惠券使用历史
        /// </summary>
        public string GetCouponUsedHistoryList(DataRow row)
        {
            if (!row.Table.Columns.Contains("UserId") || string.IsNullOrEmpty(row["UserId"].ToString()))
            {
                return JSONHelper.FromString(false, "用户id不能为空");
            }

            int pageCount = 0;
            int Counts = 0;
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

            using (IDbConnection conn = new SqlConnection(ConnectionDb.GetBusinessConnection()))
            {
                string sql = @"SELECT * FROM
                                (
                                    select R.Id, R.Amount, B.BussName,R.ReceiptDate,R.PayDate,
                                    OffsetMoney=(SELECT sum(isnull(x.OffsetMoney,0)) FROM Tb_Charge_ReceiptDetail x WHERE x.ReceiptCode=R.Id)
                                    FROM Tb_Charge_Receipt as R left join Tb_System_BusinessCorp as B on b.BussId = R.BussId
                                    WHERE  R.UserId='" + row["UserId"].ToString() + @"'
                                ) t WHERE t.OffsetMoney>0";

                DataTable dt = BussinessCommon.GetList(out pageCount, out Counts, sql, PageIndex, PageSize, "PayDate", 1, "Id", ConnectionDb.GetBusinessConnection(), "*").Tables[0];
                return JSONHelper.FromString(dt);
            }
        }

        /// <summary>
        /// 获取当前可用的优惠券余额
        /// </summary>
        public string GetCanUseCouponBalanceInBuss(DataRow row)
        {
            if (!row.Table.Columns.Contains("UserId") || string.IsNullOrEmpty(row["UserId"].ToString()))
            {
                return JSONHelper.FromString(false, "用户id不能为空");
            }

            if (!row.Table.Columns.Contains("CorpId") || string.IsNullOrEmpty(row["CorpId"].ToString()))
            {
                return JSONHelper.FromString(false, "物业公司id不能为空");
            }

            if (!row.Table.Columns.Contains("BussId") || string.IsNullOrEmpty(row["BussId"].ToString()))
            {
                return JSONHelper.FromString(false, "商家id不能为空");
            }

            using (IDbConnection conn = new SqlConnection(ConnectionDb.GetBusinessConnection()))
            {
                DataTable dt = conn.ExecuteReader(@"SELECT CorpId,BussId,isnull(Balance,0) as Balance FROM Tb_Resources_CouponBalance WHERE UserId=@UserId
                                                    AND CorpId=@CorpId AND (BussId=@BussId OR BussId IS NULL)",
                    new { UserId = row["UserId"], CorpId = row["CorpId"], BussId = row["BussId"] }).ToDataSet().Tables[0];

                return JSONHelper.FromString(dt);
            }
        }
    }
}
