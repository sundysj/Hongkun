using Common;
using Dapper;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Business
{
    /// <summary>
    /// App操作权限管理
    /// </summary>
    public class FunctionPopeManage : PubInfo
    {
        public FunctionPopeManage()
        {
            base.Token = "201812206FunctionPopeManage";
        }

        public override void Operate(ref Transfer Trans)
        {
            Trans.Result = new ApiResult(false, "未知错误").toJson();
            DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
            DataRow Row = dAttributeTable.Rows[0];

            //验证登录
            if (!new Login().isLogin(ref Trans))
                return;

            switch (Trans.Command)
            {
                case "CanAbandon":
                    Trans.Result = CanAbandon(Row);         // 客服管理相关废弃权限
                    break;
                case "CanMeterRead":
                    Trans.Result = CanMeterRead(Row);       // 抄表权限
                    break;
                case "CanSearchtCust":
                    Trans.Result = CanSearchtCust(Row);     // 查询客户信息权限
                    break;
            }
        }

        /// <summary>
        /// 是否具有客服管理相关废弃权限
        /// </summary>
        private string CanAbandon(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return new ApiResult(false, "CommID不能为空").toJson();
            }

            if (!row.Table.Columns.Contains("Type") || string.IsNullOrEmpty(row["Type"].AsString()))
            {
                return new ApiResult(false, "缺少参数Type").toJson();
            }

            int commId = AppGlobal.StrToInt(row["CommID"].ToString());
            string Type = row["Type"].AsString();

            if (Type == "1")
            {
                Type = "1025";  // APP工单池废弃
            }
            if (Type == "2")
            {
                Type = "1026";  // APP口派报事废弃
            }
            if (Type == "3")
            {
                Type = "1027";  // APP书面报事废弃
            }

            return new ApiResult(true, HasFunctionCode(commId, Type)).toJson();
        }

        /// <summary>
        /// 是否具有抄表权限
        /// </summary>
        private string CanMeterRead(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return new ApiResult(false, "CommID不能为空").toJson();
            }

            if (!row.Table.Columns.Contains("Type") || string.IsNullOrEmpty(row["Type"].AsString()))
            {
                return new ApiResult(false, "缺少参数Type").toJson();
            }

            int commId = AppGlobal.StrToInt(row["CommID"].ToString());
            string Type = row["Type"].AsString();

            if (Type == "1")
            {
                Type = "1029";  // App户内抄表
            }
            if (Type == "2")
            {
                Type = "1030";  // App公区抄表  
            }

            return new ApiResult(true, HasFunctionCode(commId, Type)).toJson();
        }

        /// <summary>
        /// 是否具有查询客户信息权限
        /// </summary>
        private string CanSearchtCust(DataRow row)
        {
            int commId = AppGlobal.StrToInt(row["CommID"].ToString());

            return new ApiResult(true, HasFunctionCode(commId, "1028")).toJson();
        }


        private int HasFunctionCode(int commId, string type)
        {
            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                string userRoles = conn.Query<string>(@"Proc_Sys_User_FilterRoles", new
                {
                    CommID = commId,
                    OrganCode = "01",
                    UserCode = Global_Var.LoginUserCode
                }, null, false, null, CommandType.StoredProcedure).FirstOrDefault();

                if (string.IsNullOrEmpty(userRoles) == false)
                {
                    string sql = $@"SELECT Id, RoleCode, FunCode FROM Tb_Sys_FunctionPope WHERE FunCode='{type}' AND RoleCode IN({userRoles});";

                    return (conn.Query(sql).Count() > 0 ? 1 : 0);
                }

                return 0;
            }
        }
    }
}
