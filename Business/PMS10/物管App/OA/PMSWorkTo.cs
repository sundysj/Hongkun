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
using System.Threading.Tasks;

namespace Business 
{
    internal class PMSWorkTo : PubInfo
    {

        public PMSWorkTo()
        {
            base.Token = "20200826PMS10WorkTo";
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
                    case "GetWorkGoDown":
                        Trans.Result = GetWorkGoDown(Row);
                        break;
                    case "GetGoOutRegisterUser":
                        Trans.Result = GetGoOutRegisterUser(Row);
                        break;
                    case "GetAddGoOutRegister":
                        Trans.Result = GetAddGoOutRegister(Row);
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
        ///获取当前人员的工作去向
        /// </summary>
        private string GetWorkGoDown(DataRow row)
        {
            if (!row.Table.Columns.Contains("UserCode") || string.IsNullOrEmpty(row["UserCode"].AsString()))
            {
                return new ApiResult(false, "缺少参数UserCode").toJson();
            }
            if (!row.Table.Columns.Contains("PageIndex") || string.IsNullOrEmpty(row["PageIndex"].AsString()))
            {
                return new ApiResult(false, "缺少参数PageIndex").toJson();
            }
            var pageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());
            var pageSize = 10;
            if (row.Table.Columns.Contains("PageSize") || !string.IsNullOrEmpty(row["PageSize"].AsString()))
            {
                pageSize = AppGlobal.StrToInt(row["PageSize"].ToString());
            }

            var userCode = row["UserCode"].ToString();
            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = $@"SELECT UserName,RecordDate,OutDate,PlanRetDate,RealRetDate,OutWhere,ISNULL( OutResult,'')OutResult,IsReturn
                                    FROM view_OAPublicWork_PeopleOutWork_Filter 
                                    WHERE UserCode={userCode} ORDER BY RecordDate DESC";

                var data = conn.Query(sql);
                return new ApiResult(true, data).toJson();
            }
         
        }


        /// <summary>
        ///获取外出人员列表
        /// </summary>
        private string GetGoOutRegisterUser(DataRow row)
        {
            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = @"SELECT UserCode,UserName FROM Tb_Sys_User  WHERE  (isnull(IsDelete,0)=0)";

                var data = conn.Query(sql);
                return new ApiResult(true, data).toJson();
            }

        }



        /// <summary>
        ///添加外出登记
        /// </summary>
        private string GetAddGoOutRegister(DataRow row)
        {
            #region 参数校验
            if (!row.Table.Columns.Contains("UserCode") || string.IsNullOrEmpty(row["UserCode"].AsString()))
            {
                return new ApiResult(false, "缺少参数UserCode").toJson();
            }
            if (!row.Table.Columns.Contains("RecordDate") || string.IsNullOrEmpty(row["RecordDate"].AsString()))
            {
                return new ApiResult(false, "缺少参数RecordDate").toJson();
            }
            if (!row.Table.Columns.Contains("LeaveManList") || string.IsNullOrEmpty(row["LeaveManList"].AsString()))
            {
                return new ApiResult(false, "缺少参数LeaveManList").toJson();
            }
            if (!row.Table.Columns.Contains("OutDate") || string.IsNullOrEmpty(row["OutDate"].AsString()))
            {
                return new ApiResult(false, "缺少参数OutDate").toJson();
            }
            if (!row.Table.Columns.Contains("PlanRetDate") || string.IsNullOrEmpty(row["PlanRetDate"].AsString()))
            {
                return new ApiResult(false, "缺少参数PlanRetDate").toJson();
            }
            if (!row.Table.Columns.Contains("OutWhere") || string.IsNullOrEmpty(row["OutWhere"].AsString()))
            {
                return new ApiResult(false, "缺少参数OutWhere").toJson();
            }
            if (!row.Table.Columns.Contains("OutThing") || string.IsNullOrEmpty(row["OutThing"].AsString()))
            {
                return new ApiResult(false, "缺少参数OutThing").toJson();
            }
            if (!row.Table.Columns.Contains("IsReturn") || string.IsNullOrEmpty(row["IsReturn"].AsString()))
            {
                return new ApiResult(false, "缺少参数IsReturn").toJson();
            }
            #endregion 参数校验

            var userCode = row["UserCode"].ToString();
            var recordDate = row["RecordDate"].ToString();
            var leaveManList =row["LeaveManList"].ToString();
            var outDate = row["OutDate"].ToString();
            var planRetDate = row["PlanRetDate"].ToString();
            var outWhere = row["OutWhere"].ToString();
            var outThing = row["OutThing"].ToString();
            var outResult = "";
            if (row.Table.Columns.Contains("OutResult") || !string.IsNullOrEmpty(row["OutResult"].AsString()))
            {
                outResult = AppGlobal.StrToStr(row["OutResult"].ToString());
            }
            var isReturn = row["IsReturn"].ToString();
            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = @"INSERT  INTO Tb_OAPublicWork_PeopleOutWork    
                                     (UserCode,RecordDate,OutDate,PlanRetDate,RealRetDate,OutWhere,OutThing,OutResult,IsReturn,LeaveManList)
		                            VALUES
		                            (@UserCode,@RecordDate,@OutDate,@PlanRetDate,@RealRetDate,@OutWhere,@OutThing,@OutResult,@IsReturn,
                                    @LeaveManList)";

                var data = conn.Execute(sql,new 
                {
                    UserCode=userCode,
                    RecordDate= recordDate,
                    OutDate= outDate,
                    PlanRetDate= planRetDate,
                    RealRetDate= "",
                    OutWhere= outWhere,
                    OutThing= outThing,
                    OutResult= outResult,
                    IsReturn= isReturn,
                    LeaveManList= leaveManList
                });
                return new ApiResult(true, "保存成功").toJson();
            }
            }

        }

}
