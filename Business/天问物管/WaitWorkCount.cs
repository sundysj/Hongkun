using Common;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Business
{
    public class WaitWorkCount : PubInfo
    {
        public WaitWorkCount()
        {
            base.Token = "20160604WaitWorkCount";
        }

        public override void Operate(ref Common.Transfer Trans)
        {
            Trans.Result = JSONHelper.FromString(false, "未知错误!");
            DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
            DataRow Row = dAttributeTable.Rows[0];
            //验证登录
            if (!new Login().isLogin(ref Trans)) return;

            switch (Trans.Command.ToString())
            {
                //报事抢单数量
                case "QiangDanCount":
                    break;
                   // 报事分派数量
                case "IncidentAssignedCount":
                    Trans.Result = GetWaitWorkCount_Assigned(Row);
                    break;
                //报事处理数量
                case "IncidentCount":
                    Trans.Result = GetWaitWorkCount_Incident(Row);
                    break;
              
                //业务审批数量
                case "BusinessCheckCount":
                    Trans.Result = GetWaitWorkCount_Business(Row);
                    break;
                //OA待办审批数量
                case "OACheckCount":
                    Trans.Result = GetWaitWorkCount_OA(Row);
                    break;
            }
        }

        public int OAPublicWork_GetWaitWorkListNew_Filter(string Code, string Type, string Key)
        {
            //@StartDate @EndDate 暂未使用传空值
            SqlParameter[] parameters = {
                    new SqlParameter("@Code", SqlDbType.VarChar),
                    new SqlParameter("@Type", SqlDbType.VarChar),
                    new SqlParameter("@Key", SqlDbType.VarChar),
                    new SqlParameter("@StartDate", SqlDbType.VarChar),
                    new SqlParameter("@EndDate", SqlDbType.VarChar),
            };
            parameters[0].Value = Code;
            parameters[1].Value = Type;
            parameters[2].Value = Key;
            parameters[3].Value = "";
            parameters[4].Value = "";
            DataTable dTable = new DbHelperSQLP(PubConstant.hmWyglConnectionString.ToString()).RunProcedure("Proc_OAPublicWork_GetWaitWorkListNew", parameters, "RetDataSet").Tables[0];
            return dTable.Rows.Count;
        }

        private string GetWaitWorkCount_OA(DataRow Row)
        {
            string Result = JSONHelper.FromString(false, "未知错误!"); ;

            //除业务审批外的OA待办数量
            int Total = 0;
            int typeCount = 13;
            if (Global_Var.CorpID == "1829")
            {
                typeCount = 20;
            }
            else {
                //2018-01-04,敬志强修改
                //解决数量不一致的问题
                typeCount = 20;
            }
            for(int i=1;i<= typeCount; i++)
            {
                int Ret = OAPublicWork_GetWaitWorkListNew_Filter(Row["UserCode"].ToString(), i.ToString().PadLeft(4, '0'),"");
                Total = Total + Ret;
            }
            Result = Total.ToString();
            return JSONHelper.FromString(true, Result);
        }

        private string GetWaitWorkCount_Business(DataRow Row)
        {
            string Result = JSONHelper.FromString(false, "未知错误!"); ;


            string strSQL = " and UserCode = '" + Row["UserCode"].ToString() + "' ";

            SqlParameter[] parameters = {
                    new SqlParameter("@SQLEx", SqlDbType.VarChar)
            };
            parameters[0].Value = strSQL;
            DataTable dTable = new DbHelperSQLP(PubConstant.hmWyglConnectionString.ToString()).RunProcedure("Proc_Sys_TakePicWork_Filter", parameters, "RetDataSet").Tables[0];

            bool temp = false;
            if (dTable.Columns.Contains("Counts"))
            {
                temp = true;
            }

            int Total = 0;
            foreach (DataRow DRow in dTable.Rows)
            {
                if (temp)
                {
                    Total = Total + AppGlobal.StrToInt(DRow["Counts"].ToString());
                    continue;
                }

                Total = Total + AppGlobal.StrToInt(DRow["InsBatchFeesCount"].ToString());
                Total = Total + AppGlobal.StrToInt(DRow["InsOneFeesCount"].ToString());
                Total = Total + AppGlobal.StrToInt(DRow["OffsetPrecCount"].ToString());

                Total = Total + AppGlobal.StrToInt(DRow["WaivCount"].ToString());
                Total = Total + AppGlobal.StrToInt(DRow["ReceCount"].ToString());
                Total = Total + AppGlobal.StrToInt(DRow["RefundCount"].ToString());

                Total = Total + AppGlobal.StrToInt(DRow["PrecRefundCount"].ToString());
                Total = Total + AppGlobal.StrToInt(DRow["LeaseContCount"].ToString());
                Total = Total + AppGlobal.StrToInt(DRow["ContCount"].ToString());
                Total = Total + AppGlobal.StrToInt(DRow["RoomStateCount"].ToString());
            }
            Result = Total.ToString();
            return JSONHelper.FromString(true,Result);
        }


        private string GetWaitWorkCount_Assigned(DataRow Row)
        {
            string Result = JSONHelper.FromString(false, "未知错误!"); ;
            DataTable dTable = Mobile_GetWaitWorkCount_Assigned(Row["UserCode"].ToString(), AppGlobal.StrToLong(Row["CommID"].ToString()));
            if (dTable.Rows.Count > 0)
            {
                Result = JSONHelper.FromString(dTable);
            }
            return Result;
        }

        private string GetWaitWorkCount_Incident(DataRow Row)
        {
            string Result = JSONHelper.FromString(false, "未知错误!"); ;
            DataTable dTable = Mobile_GetWaitWorkCount_Incident(Row["UserCode"].ToString(), AppGlobal.StrToLong(Row["CommID"].ToString()));
            if (dTable.Rows.Count > 0)
            {
                Result = JSONHelper.FromString(dTable);
            }
            return Result;
        }

        private string GetWaitWorkCount_QiangDan(DataRow Row)
        {
            string Result = JSONHelper.FromString(false, "未知错误!"); ;
            DataTable dTable = Mobile_GetWaitWorkCount_QiangDan(Row["UserCode"].ToString(), AppGlobal.StrToLong(Row["CommID"].ToString()), Row["OrganCode"].ToString());
            if (dTable.Rows.Count > 0)
            {
                Result = JSONHelper.FromString(dTable);
            }
            return Result;
        }

       

        #region 查询报事分派工作数量
        private DataTable Mobile_GetWaitWorkCount_Assigned(string UserName, long CommID)
        {

            SqlParameter[] parameters = {
                    new SqlParameter("@UserName", SqlDbType.VarChar),
                    new SqlParameter("@CommID", SqlDbType.BigInt)
            };
            parameters[0].Value = UserName;
            parameters[1].Value = CommID;
            DataTable dTable = new DbHelperSQLP(PubConstant.hmWyglConnectionString.ToString()).RunProcedure("Proc_Mobile_GetWaitWorkCount_Assigned", parameters, "RetDataSet").Tables[0];

            return dTable;
        }
        #endregion

        #region 查询报事处理工作数量
        private DataTable Mobile_GetWaitWorkCount_Incident(string UserName, long CommID)
        {

            SqlParameter[] parameters = {
                    new SqlParameter("@UserName", SqlDbType.VarChar),
                    new SqlParameter("@CommID", SqlDbType.BigInt)
            };
            parameters[0].Value = UserName;
            parameters[1].Value = CommID;
            DataTable dTable = new DbHelperSQLP(PubConstant.hmWyglConnectionString.ToString()).RunProcedure("Proc_Mobile_GetWaitWorkCount_Incident", parameters, "RetDataSet").Tables[0];

            return dTable;
        }

         

        private DataTable Mobile_GetWaitWorkCount_QiangDan(string UserName, long CommID, string OrganCode)
        {

            SqlParameter[] parameters = {
                    new SqlParameter("@UserCode", SqlDbType.NVarChar,50),
                              new SqlParameter("@OrganCode", SqlDbType.VarChar,50),
                              new SqlParameter("@CommID", SqlDbType.Int)
                                              };
            parameters[0].Value = UserName;
            parameters[1].Value = OrganCode;
            parameters[2].Value = CommID;

            DataTable DataTableResult = new DbHelperSQLP(PubConstant.hmWyglConnectionString.ToString()).RunProcedure("Proc_Sys_User_FilterRoles", parameters, "RetDataSet").Tables[0];
            string result = "";

            if (DataTableResult.Rows.Count > 0)
            {
                result = DataTableResult.Rows[0][0].ToString();
            }
            var SQL = new StringBuilder();
            var roleList = result.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            string sql = string.Format("  AND CommID='{0}' AND DealMan is null AND (DealState is null or DealState <> 1) and ISNULL(IsDelete,0)=0 ", CommID);
            SQL.Append(sql);
            SQL.Append(" AND (ProcessRoleID = ',' ");
            foreach (var item in roleList)
            {
                var roleItem = item.Replace("'", string.Empty);
                SQL.Append(string.Format(" OR ProcessRoleID LIKE '%{0}%' ", roleItem));
            }
            SQL.Append(" ) ");

            DataTable dTable = new DbHelperSQLP(PubConstant.hmWyglConnectionString.ToString()).Query("SELECT Count(*) AS Ct FROM (SELECT B.*, dbo.funGetProcessRoleCode(B.TypeID) AS ProcessRoleID, dbo.funGetAssignedRoleCode(B.TypeID) AS AssignedRoleID,C.EmergencyDegree FROM view_HSPR_IncidentAssigned_Filter AS B LEFT JOIN Tb_HSPR_IncidentAccept AS C ON B.IncidentID = C.IncidentID) AS A WHERE 1=1 " + SQL.ToString()).Tables[0];

            return dTable;
        }
        #endregion
    }
}
