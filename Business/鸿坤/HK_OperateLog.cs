using Dapper;
using DapperExtensions;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using System;
using System.Data.SqlClient;

namespace Business
{
    /// <summary>
    /// 鸿坤记录操作日志
    /// </summary>
    public static class HK_OperateLog
    {
        public static void WriteLog(string className, string commandName)
        {
            switch (className)
            {
                case "IncidentAcceptManage":
                    HK_OperateLog.IncidentAccept_Log(commandName);
                    break;

                case "HouseInspectManage_new":
                    HK_OperateLog.HouseInspect_Log(commandName);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 查验模块Log
        /// </summary>
        /// <param name="commandName"></param>
        private static void HouseInspect_Log(string commandName)
        {
            switch (commandName)
            {
                case "GetHouseInspectList":
                    Write($"【{Global_Var.LoginUserName}】点击了【查验计划】功能模块", "查验计划");
                    break;
                case "GetHouseInspectPollingList":
                    Write($"【{Global_Var.LoginUserName}】点击了【查验整改】功能模块", "查验整改");
                    break;
                default:
                    break;
            }
        }

        private static void IncidentAccept_Log(string commandName)
        {
            switch (commandName)
            {
                case "GetUntreatedDispatching":
                    Write($"【{Global_Var.LoginUserName}】点击了【报事派单】功能模块", "报事派单");
                    break;
                case "GetUntreatedRobWorkOrder":
                    Write($"【{Global_Var.LoginUserName}】点击了【报事抢单】功能模块", "报事抢单");
                    break;
                case "GetUntreatedHandleWorkOrder":
                    Write($"【{Global_Var.LoginUserName}】点击了【报事处理】功能模块", "报事处理");
                    break;
                case "GetIncidentWarningList":
                    Write($"【{Global_Var.LoginUserName}】点击了【报事预警】功能模块", "报事预警");
                    break;
                case "SetIncidentAcceptPhoneInsert":
                    Write($"【{Global_Var.LoginUserName}】点击了【室内报事】功能模块", "室内报事");
                    break;
                case "SetIncidentAcceptPhoneInsertRegion":
                    Write($"【{Global_Var.LoginUserName}】点击了【公共区域报事】功能模块", "公共区域报事");
                    break;
                default:
                    break;
            }
        }

        private static void DynamicManage_Log()
        { }

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
                                        Memo = "物管App"
                                    });
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
