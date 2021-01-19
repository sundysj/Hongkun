using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using MobileSoft.Common;
using System.Data;
using MobileSoft.DBUtility;
using System.Data.SqlClient;
using Dapper;
using Newtonsoft.Json;

namespace Business.鸿坤
{
    public class CostInfo : PubInfo
    {
        public CostInfo()
        {
            base.Token = "20190228ParkTempCardFees";
        }
        public override void Operate(ref Transfer Trans)
        {
            Trans.Result = JSONHelper.FromString(false, "未知错误");

            DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
            DataRow Row = dAttributeTable.Rows[0];

            switch (Trans.Command)
            {
                case "TempPayFees":
                    Trans.Result = TempPayFees(Row);//新增临时停车信息
                    break;

            }

        }
        /// <summary>
        /// 新增临时停车信息
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string TempPayFees(DataRow row)
        {
            string CommID = null;//项目ID
            string CustName = null;//客户名称
            string FeesDueDate = null;//费用时间
            string FeesStateDate = null;//开始时间
            string FeesEndDate = null;//结束时间
            string DueAmount = null;//费用金额
            string feesMemo = null;//备注

            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "项目ID不能为空");
            }
            if (!row.Table.Columns.Contains("CustName") || string.IsNullOrEmpty(row["CustName"].ToString()))
            {
                return JSONHelper.FromString(false, "客户名称不能为空");
            }
            if (!row.Table.Columns.Contains("FeesDueDate") || string.IsNullOrEmpty(row["FeesDueDate"].ToString()))
            {
                return JSONHelper.FromString(false, "费用时间不能为空");
            }
            if (!row.Table.Columns.Contains("FeesStateDate") || string.IsNullOrEmpty(row["FeesStateDate"].ToString()))
            {
                return JSONHelper.FromString(false, "开始时间不能为空");
            }
            if (!row.Table.Columns.Contains("FeesEndDate") || string.IsNullOrEmpty(row["FeesEndDate"].ToString()))
            {
                return JSONHelper.FromString(false, "结束时间不能为空");
            }
            if (!row.Table.Columns.Contains("DueAmount") || string.IsNullOrEmpty(row["DueAmount"].ToString()))
            {
                return JSONHelper.FromString(false, "收费金额不能为空");
            }
            if (!row.Table.Columns.Contains("feesMemo") || string.IsNullOrEmpty(row["feesMemo"].ToString()))
            {
                return JSONHelper.FromString(false, "备注不能为空");
            }
            
            CommID = row["CommID"].ToString();
            CustName = row["CustName"].ToString();
            FeesDueDate = row["FeesDueDate"].ToString();
            FeesStateDate = row["FeesStateDate"].ToString();
            FeesEndDate = row["FeesEndDate"].ToString();
            DueAmount = row["DueAmount"].ToString();
            feesMemo = row["feesMemo"].ToString();
           
            string ContionString = PubConstant.GetConnectionString("SQLConnection").ToString();
            IDbConnection con = new SqlConnection(ContionString);

            try
            {
                DynamicParameters parameters = new DynamicParameters();
                var InfoCode = Guid.NewGuid();
                parameters.Add("@InfoCode", InfoCode);
                parameters.Add("@CardID", null);
                parameters.Add("@CardType", null);
                parameters.Add("@GoName", null);
                parameters.Add("@CommID", CommID);
                parameters.Add("@CustName", CustName);
                parameters.Add("@FeesDueDate", FeesDueDate);
                parameters.Add("@FeesStateDate", FeesStateDate);
                parameters.Add("@FeesEndDate", FeesEndDate);
                parameters.Add("@DueAmount", DueAmount);
                parameters.Add("@feesMemo", feesMemo);
                
                int b = con.Execute("Proc_HSPR_TemporaryCarPrecostsDetail_Insert", parameters, null, null, CommandType.StoredProcedure);
                con.Dispose();
                if (b > 0)
                {
                    return JSONHelper.FromString(true, "添加成功");
                }
                else
                {
                    return JSONHelper.FromString(false, "添加未完成");
                }
            }
            catch (Exception e)
            {
                return JSONHelper.FromString(false, e.Message);
            }

        }
    }

}
