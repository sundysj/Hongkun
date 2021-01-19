using Dapper;
using DapperExtensions;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using System;
using System.Data.SqlClient;

namespace Business
{
    public static class JH_OperateLog
    {
        public static void WriteLog(string className)
        {
            switch (className)
            {
                case "LoginLog":
                    Write($"【{Global_Var.LoginUserName}】点击了【登录】", "登录");
                    break;

                default:
                    break;
            }
        }


        private static void Write(string OperateName, string nodeName)
        {
            try
            {
                using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
                {
                    conn.Execute(@"INSERT INTO Tb_Sys_Log(CorpID, BranchID, CommID, ManagerCode, LogTime, PNodeName, OperateName, Memo)
                                    VALUES (@CorpID, 0, @CommID, @ManagerCode, getdate(), @PNodeName, @OperateName, @Memo)",
                                    new
                                    {
                                        CorpID = Global_Var.LoginCorpID,
                                        CommID = Global_Var.LoginCommID,
                                        ManagerCode = Global_Var.LoginUserCode,
                                        PNodeName = nodeName,
                                        OperateName = OperateName,
                                        Memo = "业主App"
                                    });
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
