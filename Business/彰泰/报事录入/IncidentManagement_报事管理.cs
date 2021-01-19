using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.Xml;
using Common;
using Dapper;
using KernelDev.DataAccess;
using MobileSoft.DBUtility;
using Newtonsoft.Json;
using TWTools.Push;

namespace Business
{
    public class IncidentManagement : PubInfo
    {
        public IncidentManagement()
        {
            base.Token = "2020820IncidentManagement";
        }
        public override void Operate(ref Transfer Trans)
        {
            try
            {
                switch (Trans.Command)
                {
                    case "InspectManage_Incident":
                        // 新增报事
                        Trans.Result = InspectManage_Incident(Trans.Attribute);
                        break;
                    case "InspectManage_IncidentProcess":
                        // 新增报事
                        Trans.Result = InspectManage_IncidentProcess(Trans.Attribute);
                        break;
                    default:
                        Trans.Result = new ExecResult(-1, "未知错误").ToJson();
                        break;
                }
            }
            catch (Exception ex)
            {
                IncidentTaskLog(true, ex.Message + Environment.CommandLine + ex.StackTrace + Environment.NewLine + ex.Source, 200, "报事入参", Trans.Attribute, "", Trans.Command, "");
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace + Environment.NewLine + ex.Source);
                Trans.Result = new ExecResult(-1, ex.Message + ex.StackTrace).ToJson();
            }
        }
        /// <summary>
        /// 报事受理
        /// </summary>
        /// <param name="strParameter"></param>
        /// <returns></returns>
        private string InspectManage_Incident(string strParameter)
        {
            JavaScriptSerializer jsonSet = new JavaScriptSerializer();
            jsonSet.MaxJsonLength = int.MaxValue;
            string strReturn = string.Empty;
            IncidentAcceptManage_Entity incidentAcceptManage_Entity = null;
            try
            {
                string strConnectionString = PubConstant.ZTWYConnectionString;
                string strIncidentUserCode = PubConstant.ZTWYIncidentUserCode;
                incidentAcceptManage_Entity = jsonSet.Deserialize<IncidentAcceptManage_Entity>(strParameter);
                try
                {
                    incidentAcceptManage_Entity.AdmiMan_UserCode = strIncidentUserCode;
                    DataAccess DAccess = new DataAccess(strConnectionString);
                    DbParamters dbParams = new DbParamters();
                    dbParams.CommandText = "Proc_HSPR_IncidentAccept_Insert_Crm";
                    dbParams.CommandType = CommandType.StoredProcedure;
                    dbParams.ProcParamters = @"@CommID,@CustID,@RoomID,@TypeID,@IncidentDate,@ReserveDate,@IncidentPlace,@IncidentMan,@Phone,@IncidentContent,@AdmiDate,@AdmiManUserLoginCode,@AdmiManUserMan,@AdmiManUserCode,@RegionalID,@IncidentImgs,@IncidentSource,@CrmIncidentID,@OperationType";
                    dbParams.Add("CommID", incidentAcceptManage_Entity.CommId);
                    dbParams.Add("CustID", incidentAcceptManage_Entity.CustId);
                    dbParams.Add("RoomID", incidentAcceptManage_Entity.RoomId);
                    dbParams.Add("TypeID", incidentAcceptManage_Entity.Type_ID);
                    dbParams.Add("IncidentDate", incidentAcceptManage_Entity.Incident_Date);
                    dbParams.Add("ReserveDate", incidentAcceptManage_Entity.Reserve_Date);
                    dbParams.Add("IncidentPlace", incidentAcceptManage_Entity.Incident_Place);
                    dbParams.Add("IncidentMan", incidentAcceptManage_Entity.Incident_Man);
                    dbParams.Add("Phone", incidentAcceptManage_Entity.Phone);
                    dbParams.Add("IncidentContent", incidentAcceptManage_Entity.Incident_Content);

                    dbParams.Add("AdmiDate", incidentAcceptManage_Entity.Incident_Date);
                    dbParams.Add("AdmiManUserLoginCode", incidentAcceptManage_Entity.AdmiMan_UserLoginCode);
                    dbParams.Add("AdmiManUserMan", incidentAcceptManage_Entity.AdmiMan_UserName);
                    dbParams.Add("AdmiManUserCode", incidentAcceptManage_Entity.AdmiMan_UserCode);
                    dbParams.Add("RegionalID", incidentAcceptManage_Entity.Regional_Id);
                    dbParams.Add("IncidentImgs", incidentAcceptManage_Entity.Incident_Imgs);
                    dbParams.Add("IncidentSource", incidentAcceptManage_Entity.IncidentSource);
                    dbParams.Add("CrmIncidentID", incidentAcceptManage_Entity.Incident_CrmId);
                    dbParams.Add("OperationType", 0); //只新增
                    DataTable dtTable = DAccess.DataTable(dbParams);
                    if (dtTable.Rows.Count > 0)
                    {
                        Internal_BackParameter backParameter = jsonSet.Deserialize<Internal_BackParameter>(JsonConvert.SerializeObject(dtTable).Replace("[", "").Replace("]", ""));
                        IncidentAcceptManage_BackParameter retBackParameter = jsonSet.Deserialize<IncidentAcceptManage_BackParameter>(JsonConvert.SerializeObject(dtTable).Replace("[", "").Replace("]", ""));
                        if (backParameter.Code == 0)
                        {
                            strReturn = jsonSet.Serialize(new ExecResult(backParameter.Code, backParameter.Message, ErrType.Normal, retBackParameter));
                        }
                        else
                        {
                            strReturn = new ExecResult(backParameter.Code, backParameter.Message, ErrType.Verification).ToJson();
                        }
                    }
                }

                catch (Exception ex)
                {
                    strReturn = new ExecResult(-20, ex.Message, ErrType.System).ToJson();
                }
            }
            catch (Exception ex)
            {
                strReturn = new ExecResult(-21, $"数据解析异常【{ex.Message}】", ErrType.System).ToJson();
            }
            return strReturn;
        }

        /// <summary>
        /// 报事流程跟进
        /// </summary>
        /// <param name="strParameter"></param>
        /// <returns></returns>
        private string InspectManage_IncidentProcess(string strParameter)
        {
            JavaScriptSerializer jsonSet = new JavaScriptSerializer();
            jsonSet.MaxJsonLength = int.MaxValue;
            string strReturn = string.Empty;
            Process_ToDealWith Process = null;
            try
            {
                string strConnectionString = PubConstant.ZTWYConnectionString;
                string strIncidentUserCode = PubConstant.ZTWYIncidentUserCode;
                Process = jsonSet.Deserialize<Process_ToDealWith>(strParameter);
                string strDealInfoParameter = jsonSet.Serialize(Process.DealInfo);
                try
                {
                    DataTable dtTable = null;
                    object retParameter = null;
                    switch (Process.DealType)
                    {
                        case "报事分派":
                            Process.分派_2 = jsonSet.Deserialize<进程处理_分派_2>(strDealInfoParameter);
                            dtTable = IncidentDisp(Process, strConnectionString, strIncidentUserCode);
                            break;
                        case "报事完成":
                            Process.完成_3 = jsonSet.Deserialize<进程处理_完成_3>(strDealInfoParameter);
                            dtTable = IncidentComplete(Process, strConnectionString, strIncidentUserCode);
                            break;
                        case "报事关闭":
                            Process.关闭_4 = jsonSet.Deserialize<进程处理_关闭_4>(strDealInfoParameter);
                            dtTable = IncidentClose(Process, strConnectionString, strIncidentUserCode);
                            break;
                        case "报事回访":
                            Process.回访_5 = jsonSet.Deserialize<进程处理_回访_5>(strDealInfoParameter);
                            dtTable = IncidentReply(Process, strConnectionString, strIncidentUserCode);
                            //retParameter = jsonSet.Deserialize<进程处理_回访_5_BackParameter>(JsonConvert.SerializeObject(dtTable).Replace("[", "").Replace("]", ""));
                            break;
                        case "报事废弃":
                            Process.废弃_6 = jsonSet.Deserialize<进程处理_废弃_6>(strDealInfoParameter);
                            dtTable = IncidentAbandoned(Process, strConnectionString, strIncidentUserCode);
                            break;
                        case "报事回退":
                            Process.回退_7 = jsonSet.Deserialize<进程处理_回退_7>(strDealInfoParameter);
                            dtTable = IncidentCancleFinish(Process, strConnectionString, strIncidentUserCode);
                            break;
                        case "转派责任方":
                            Process.转发_8 = jsonSet.Deserialize<进程处理_转发_8>(strDealInfoParameter);
                            dtTable = IncidentTransmit(Process, strConnectionString, strIncidentUserCode);
                            break;
                        default:
                            strReturn = new ExecResult(-20, $"此类进程不受理，进程编码【{Process.DealType}】", ErrType.System).ToJson();
                            break;
                    }
                    if (dtTable != null && dtTable.Rows.Count > 0)
                    {
                        Internal_BackParameter backParameter = jsonSet.Deserialize<Internal_BackParameter>(JsonConvert.SerializeObject(dtTable).Replace("[", "").Replace("]", ""));
                        if (backParameter.Code == 0)
                        {
                            if (retParameter == null)
                            {
                                strReturn = new ExecResult(backParameter.Code, backParameter.Message, ErrType.Normal).ToJson();
                            }
                            else
                            {
                                strReturn = jsonSet.Serialize(new ExecResult(backParameter.Code, backParameter.Message, ErrType.Normal, retParameter));
                            }
                        }
                        else
                        {
                            strReturn = new ExecResult(backParameter.Code, backParameter.Message, ErrType.Verification).ToJson();
                        }
                    }
                }
                catch (Exception ex)
                {
                    strReturn = new ExecResult(-20, ex.Message, ErrType.System).ToJson();
                }
            }
            catch (Exception ex)
            {
                strReturn = new ExecResult(-21, $"数据解析异常【{ex.Message}】", ErrType.System).ToJson();
            }
            return strReturn;

        }


        #region 详细处理
        /// <summary>
        /// 报事分派
        /// </summary>
        /// <param name="Process"></param>
        /// <param name="strConnectionString"></param>
        /// <param name="strIncidentUserCode"></param>
        /// <returns></returns>
        private DataTable IncidentDisp(Process_ToDealWith Process, string strConnectionString, string strIncidentUserCode)
        {
            try
            {
                Process.分派_2.DispUserCode = strIncidentUserCode;
                Process.分派_2.DealUserCode = strIncidentUserCode;
                DataAccess DAccess = new DataAccess(strConnectionString);
                DbParamters dbParams = new DbParamters();
                dbParams.CommandText = "Proc_HSPR_IncidentAccept_Assigned_Crm";
                dbParams.CommandType = CommandType.StoredProcedure;
                dbParams.ProcParamters = @"@IncidentID,@DispMan,@DispUserCode,@DispUserLoginCode,@DispDateTime,@DispRemarks,@DealMan,@DealUserCode,@DealUserLoginCode";
                dbParams.Add("IncidentID", Process.Incident_Id);
                dbParams.Add("DispMan", Process.分派_2.DispMan); //分派人
                dbParams.Add("DispUserCode", Process.分派_2.DispUserCode);
                dbParams.Add("DispUserLoginCode", Process.分派_2.Disp_UserLoginCode);
                dbParams.Add("DispDateTime", Process.分派_2.Disp_DateTime);
                dbParams.Add("DispRemarks", Process.分派_2.Disp_Remarks);
                dbParams.Add("DealMan", Process.分派_2.DealMan); //责任人
                dbParams.Add("DealUserCode", Process.分派_2.DealUserCode);
                dbParams.Add("DealUserLoginCode", Process.分派_2.Deal_UserLoginCode);
                return DAccess.DataTable(dbParams);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// <summary>
        /// 报事完成
        /// </summary>
        /// <param name="Process"></param>
        /// <param name="strConnectionString"></param>
        /// <param name="strIncidentUserCode"></param>
        /// <returns></returns>
        private DataTable IncidentComplete(Process_ToDealWith Process, string strConnectionString, string strIncidentUserCode)
        {
            try
            {
                Process.完成_3.FinishUserCode = strIncidentUserCode;
                DataAccess DAccess = new DataAccess(strConnectionString);
                DbParamters dbParams = new DbParamters();
                dbParams.CommandText = "Proc_HSPR_IncidentAccept_IncidentFinish_Crm";
                dbParams.CommandType = CommandType.StoredProcedure;
                dbParams.ProcParamters = @"@IncidentID,@DealSituation,@OverdueReason,@FineTypeID,@FinishMan,@FinishUserCode,@FinishUserLoginCode,@FinishDateTime";
                dbParams.Add("IncidentID", Process.Incident_Id);
                dbParams.Add("DealSituation", Process.完成_3.Deal_Situation);
                dbParams.Add("OverdueReason", Process.完成_3.Over_dueReason);
                dbParams.Add("FineTypeID", Process.完成_3.FineTypeID);
                dbParams.Add("FinishMan", Process.完成_3.FinishMan);
                dbParams.Add("FinishUserCode", Process.完成_3.FinishUserCode);
                dbParams.Add("FinishUserLoginCode", Process.完成_3.Finish_UserLoginCode);
                dbParams.Add("FinishDateTime", Process.完成_3.Finish_DateTime);
                
                return DAccess.DataTable(dbParams);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 报事关闭
        /// </summary>
        /// <param name="Process"></param>
        /// <param name="strConnectionString"></param>
        /// <param name="strIncidentUserCode"></param>
        /// <returns></returns>
        private DataTable IncidentClose(Process_ToDealWith Process, string strConnectionString, string strIncidentUserCode)
        {
            try
            {
                Process.关闭_4.CloseUserCode = strIncidentUserCode;
                DataAccess DAccess = new DataAccess(strConnectionString);
                DbParamters dbParams = new DbParamters();
                dbParams.CommandText = "Proc_HSPR_IncidentAccept_IncidentClose_Crm";
                dbParams.CommandType = CommandType.StoredProcedure;
                dbParams.ProcParamters = @"@IncidentID,@CloseUserCode,@CloseUser,@CloseUserLoginCode,@CloseSituation,@CloseType,@NoNormalCloseReasons,@CloseDateTime";
                dbParams.Add("IncidentID", Process.Incident_Id);
                dbParams.Add("CloseUserCode", Process.关闭_4.CloseUserCode);
                dbParams.Add("CloseUser", Process.关闭_4.CloseUser);
                dbParams.Add("CloseUserLoginCode", Process.关闭_4.Close_UserLoginCode);
                dbParams.Add("CloseSituation", Process.关闭_4.Close_Situation);
                dbParams.Add("CloseType", Process.关闭_4.Close_Type);
                dbParams.Add("NoNormalCloseReasons", Process.关闭_4.NoNormalClose_Reasons); //非关原因
                dbParams.Add("CloseDateTime", Process.关闭_4.Close_DateTime);
                return DAccess.DataTable(dbParams);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 报事回访
        /// </summary>
        /// <param name="Process"></param>
        /// <param name="strConnectionString"></param>
        /// <param name="strIncidentUserCode"></param>
        /// <returns></returns>
        private DataTable IncidentReply(Process_ToDealWith Process, string strConnectionString, string strIncidentUserCode)
        {
            try
            {
                Process.回访_5.ReplyUserCode = strIncidentUserCode;
                DataAccess DAccess = new DataAccess(strConnectionString);
                DbParamters dbParams = new DbParamters();
                dbParams.CommandText = "Proc_HSPR_IncidentReply_Insert_Crm";
                dbParams.CommandType = CommandType.StoredProcedure;
                dbParams.ProcParamters = @"@IncidentID,@ReplyType,@ReplyUserLoginCode,@ReplyUserCode,@ReplyMan,@ReplyDateTime,@ReplyContent,@ServiceQuality,@ReplyResult,@ReplyWay,@ReplySituationCode";
                dbParams.Add("IncidentID", Process.Incident_Id);
                dbParams.Add("ReplyType", Process.回访_5.Reply_Type);
                dbParams.Add("ReplyUserLoginCode", Process.回访_5.Reply_UserLoginCode);
                dbParams.Add("ReplyUserCode", Process.回访_5.ReplyUserCode);
                dbParams.Add("ReplyMan", Process.回访_5.ReplyMan);
                dbParams.Add("ReplyDateTime", Process.回访_5.Deal_DateTime);
                dbParams.Add("ReplyContent", Process.回访_5.Reply_Content); //非关原因
                dbParams.Add("ServiceQuality", Process.回访_5.Service_Quality);
                dbParams.Add("ReplyResult", Process.回访_5.Reply_Result);
                dbParams.Add("ReplyWay", Process.回访_5.Reply_Way);
                dbParams.Add("ReplySituationCode", Process.回访_5.Reply_SituationCode);
                return DAccess.DataTable(dbParams);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 报事废弃
        /// </summary>
        /// <param name="Process"></param>
        /// <param name="strConnectionString"></param>
        /// <param name="strIncidentUserCode"></param>
        /// <returns></returns>
        private DataTable IncidentAbandoned(Process_ToDealWith Process, string strConnectionString, string strIncidentUserCode)
        {
            try
            {
                //废弃_6
                Process.废弃_6.DeleteUserCode = strIncidentUserCode;
                DataAccess DAccess = new DataAccess(strConnectionString);
                DbParamters dbParams = new DbParamters();
                dbParams.CommandText = "Proc_HSPR_IncidentAccept_Delete_Crm";
                dbParams.CommandType = CommandType.StoredProcedure;
                dbParams.ProcParamters = @"@IncidentID,@DeleteDateTime,@DeleteReasons,@DeleteMan,@DeleteUserCode,@DeleteUserLoginCode";
                dbParams.Add("IncidentID", Process.Incident_Id);
                dbParams.Add("DeleteDateTime", Process.废弃_6.Delete_DateTime);
                dbParams.Add("DeleteReasons", Process.废弃_6.Delete_Reasons);
                dbParams.Add("DeleteMan", Process.废弃_6.DeleteMan);
                dbParams.Add("DeleteUserCode", Process.废弃_6.DeleteUserCode);
                dbParams.Add("DeleteUserLoginCode", Process.废弃_6.Delete_UserLoginCode);
                return DAccess.DataTable(dbParams);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 报事回退
        /// </summary>
        /// <param name="Process"></param>
        /// <param name="strConnectionString"></param>
        /// <param name="strIncidentUserCode"></param>
        /// <returns></returns>
        private DataTable IncidentCancleFinish(Process_ToDealWith Process, string strConnectionString, string strIncidentUserCode)
        {
            try
            {
                //回退_7
                Process.回退_7.FinishGoBackUserCode = strIncidentUserCode;
                DataAccess DAccess = new DataAccess(strConnectionString);
                DbParamters dbParams = new DbParamters();
                dbParams.CommandText = "Proc_HSPR_Incident_CancleFinish_Crm";
                dbParams.CommandType = CommandType.StoredProcedure;
                dbParams.ProcParamters = @"@IncidentID,@FallbackType,@FinishGoBackReasons,@FinishGoBackUserCode,@FinishGoBackUser,@FinishGoBackUserLoginCode,@FinishGoBackDateTime";
                dbParams.Add("IncidentID", Process.Incident_Id);
                dbParams.Add("FallbackType", Process.回退_7.Fallback_Type);
                dbParams.Add("FinishGoBackReasons", Process.回退_7.FinishGoBack_Reasons);
                dbParams.Add("FinishGoBackUserCode", Process.回退_7.FinishGoBackUserCode);
                dbParams.Add("FinishGoBackUser", Process.回退_7.FinishGoBackUser);
                dbParams.Add("FinishGoBackUserLoginCode", Process.回退_7.FinishGoBack_UserLoginCode);
                dbParams.Add("FinishGoBackDateTime", Process.回退_7.FinishGoBack_DateTime);
                return DAccess.DataTable(dbParams);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 报事转发
        /// </summary>
        /// <param name="Process"></param>
        /// <param name="strConnectionString"></param>
        /// <param name="strIncidentUserCode"></param>
        /// <returns></returns>
        private DataTable IncidentTransmit(Process_ToDealWith Process, string strConnectionString, string strIncidentUserCode)
        {
            try
            {
                Process.转发_8.DealCode = strIncidentUserCode;
                //转发_8
                DataAccess DAccess = new DataAccess(strConnectionString);
                DbParamters dbParams = new DbParamters();
                dbParams.CommandText = "Proc_HSPR_Incident_Transmit_Crm";
                dbParams.CommandType = CommandType.StoredProcedure;
                dbParams.ProcParamters = @"@IncidentID,@TypeID,@DealMan,@DealCode,@DealLoginCode,@TransmitDateTime,@TransmitUserCode,@TransmitUserLoginCode,@TransmitReasons";
                dbParams.Add("IncidentID", Process.Incident_Id);
                dbParams.Add("TypeID", Process.转发_8.Type_ID);
                dbParams.Add("DealMan", Process.转发_8.DealMan);
                dbParams.Add("DealCode", Process.转发_8.DealCode);
                dbParams.Add("DealLoginCode", Process.转发_8.Deal_UserLoginCode);
                dbParams.Add("TransmitDateTime", Process.转发_8.Transmit_DateTime);
                dbParams.Add("TransmitUserCode", Process.转发_8.TransmitUserCode);
                dbParams.Add("TransmitUserLoginCode", Process.转发_8.Transmit_UserLoginCode);
                dbParams.Add("TransmitReasons", Process.转发_8.Transmit_Reasons);
                return DAccess.DataTable(dbParams);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        private void IncidentTaskLog(bool status, string msg, int code, string type, string content, string commid, string roomid, string taskcode)
        {
            //string strInserProcess = $@" INSERT INTO Tb_Incident_TaskErrorLog
            //                            (IID,Status,Msg,Code,Type,Content,DateTime,CommID,RoomID_ZZ,TaskCode)
            //                            VALUES
            //                            (NEWID(),'{status}','{msg}','{code}','{type}','{content}',GETDATE(),'{commid}','{roomid}','{taskcode}')";
            //int size = DbHelperSQL.ExecuteSqlConn(strInserProcess, PubConstant.ZNZDConnectionString);
        }


        #region 实体类
        class Internal_BackParameter
        {
            /// <summary>
            /// 错误编码
            /// </summary>
            public int Code { get; set; }
            /// <summary>
            /// 错误信息
            /// </summary>
            public string Message { get; set; }
        }

        #region 报事受理
        /// <summary>
        /// 返回参数
        /// </summary>
        class IncidentAcceptManage_BackParameter
        {
            /// <summary>
            /// 报事ID
            /// </summary>
            public string IncidentID { get; set; }

            public string ToJson()
            {
                return JsonConvert.SerializeObject(this);
            }
        }

        class IncidentAcceptManage_Entity
        {
            /// <summary>
            /// 项目ID
            /// </summary>
            public string CommId { get; set; }
            /// <summary>
            /// 客户ID
            /// </summary>
            public string CustId { get; set; }
            /// <summary>
            /// 房屋ID
            /// </summary>
            public string RoomId { get; set; }
            /// <summary>
            /// 报事来源：客户报事和自查报事
            /// </summary>
            public string IncidentSource { get; set; }
            /// <summary>
            /// 报事区域:户内和公区
            /// </summary>
            public string Incident_Place { get; set; }
            /// <summary>
            /// 物业类或者地产类
            /// </summary>
            public string Duty { get; set; }
            /// <summary>
            /// 报事类别ID
            /// </summary>
            public string Type_ID { get; set; }
            /// <summary>
            /// 报事内容
            /// </summary>
            public string Incident_Content { get; set; }
            /// <summary>
            /// 报事人
            /// </summary>
            public string Incident_Man { get; set; }
            /// <summary>
            /// 联系电话
            /// </summary>
            public string Phone { get; set; }
            /// <summary>
            /// 预约时间 ，业主权属需要传递
            /// </summary>
            public string Reserve_Date { get; set; }
            /// <summary>
            /// 提供访问图片地址,多个图片用英文逗号隔开“,”
            /// </summary>
            public string Incident_Imgs { get; set; }
            /// <summary>
            /// 报事受理时间，CRM默认时间
            /// </summary>
            public string Incident_Date { get; set; }
            /// <summary>
            /// CRM报事Guid
            /// </summary>
            public string Incident_CrmId { get; set; }
            /// <summary>
            /// 受理人
            /// </summary>
            public string AdmiMan_UserName { get; set; }
            /// <summary>
            /// 受理人code
            /// </summary>
            public string AdmiMan_UserCode { get; set; }
            /// <summary>
            /// 受理人账号
            /// </summary>
            public string AdmiMan_UserLoginCode { get; set; }

            /// <summary>
            /// 公共区域时需要传入该参数
            /// </summary>
            public string Regional_Id { get; set; }
        }
        #endregion

        #region 进程处理

        /// <summary>
        /// 流程处理实体类
        /// </summary>
        public class Process_ToDealWith
        {
            public Process_ToDealWith() { }
            /// <summary>
            /// 物业工单报事id
            /// </summary>
            public string Incident_Id { get; set; }
            /// <summary>
            /// CRM报事id
            /// </summary>
            public string Incident_CRMId { get; set; }
            /// <summary>
            /// 进程类型：数字
            /// </summary>
            public string DealType { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public object DealInfo { get; set; }


            public 进程处理_分派_2 分派_2 { get; set; }

            public 进程处理_完成_3 完成_3 { get; set; }

            public 进程处理_关闭_4 关闭_4 { get; set; }

            public 进程处理_回访_5 回访_5 { get; set; }

            public 进程处理_废弃_6 废弃_6 { get; set; }

            public 进程处理_回退_7 回退_7 { get; set; }

            public 进程处理_转发_8 转发_8 { get; set; }


        }
        //如果好用，请收藏地址，帮忙分享。
        public class 进程处理_分派_2
        {
            public 进程处理_分派_2() { }

            /// <summary>
            /// 分派人
            /// </summary>
            public string DispMan { get; set; }
            /// <summary>
            /// 分派人usercode
            /// </summary>
            public string DispUserCode { get; set; }
            /// <summary>
            /// 分派人账号
            /// </summary>
            public string Disp_UserLoginCode { get; set; }
            /// <summary>
            /// 分派时间
            /// </summary>
            public string Disp_DateTime { get; set; }
            /// <summary>
            /// 分派备注
            /// </summary>
            public string Disp_Remarks { get; set; }

            /// <summary>
            /// 责任人
            /// </summary>
            public string DealMan { get; set; }
            /// <summary>
            /// 责任人usercode
            /// </summary>
            public string DealUserCode { get; set; }
            /// <summary>
            /// 责任人账号
            /// </summary>
            public string Deal_UserLoginCode { get; set; }


        }
        public class 进程处理_完成_3
        {
            public 进程处理_完成_3() { }
            /// <summary>
            /// 处理情况 必填
            /// </summary>
            public string Deal_Situation { get; set; }
            /// <summary>
            /// 逾期原因
            /// </summary>
            public string Over_dueReason { get; set; }
            /// <summary>
            /// 原因类别细类
            /// </summary>
            public string FineTypeID { get; set; }
            /// <summary>
            /// 完成人
            /// </summary>
            public string FinishMan { get; set; }
            /// <summary>
            /// 完成人UserCode
            /// </summary>
            public string FinishUserCode { get; set; }
            /// <summary>
            /// 完成人UserCode
            /// </summary>
            public string Finish_UserLoginCode { get; set; }
            /// <summary>
            /// 完成时间
            /// </summary>
            public string Finish_DateTime { get; set; }

        }

        public class 进程处理_关闭_4
        {
            public 进程处理_关闭_4() { }

            /// <summary>
            /// 关闭人
            /// </summary>
            public string CloseUser { get; set; }

            /// <summary>
            /// 关闭人usercode
            /// </summary>
            public string CloseUserCode { get; set; }
            /// <summary>
            /// 关闭人账号
            /// </summary>
            public string Close_UserLoginCode { get; set; }
            /// <summary>
            /// 关闭情况
            /// </summary>
            public string Close_Situation { get; set; }
            /// <summary>
            /// 原因类别细类
            /// </summary>
            public string Close_Type { get; set; }
            /// <summary>
            /// 非关原因
            /// </summary>
            public string NoNormalClose_Reasons { get; set; }
            /// <summary>
            /// 关闭时间
            /// </summary>
            public string Close_DateTime { get; set; }
        }

        public class 进程处理_回访_5
        {
            public 进程处理_回访_5() { }

            /// <summary>
            /// 回访类型
            /// </summary>
            public string Reply_Type { get; set; }
            /// <summary>
            /// 回访人code
            /// </summary>
            public string ReplyUserCode { get; set; }
            /// <summary>
            /// 回访人登录账号
            /// </summary>
            public string Reply_UserLoginCode { get; set; }
            /// <summary>
            /// 回访人
            /// </summary>
            public string ReplyMan { get; set; }
            /// <summary>
            /// 回访时间
            /// </summary>
            public string Deal_DateTime { get; set; }
            /// <summary>
            /// 回访内容
            /// </summary>
            public string Reply_Content { get; set; }
            /// <summary>
            /// 回访满意度评价 非常满意，满意，一般，不满意，非常不满意，无效评价
            /// </summary>
            public string Service_Quality { get; set; }
            /// <summary>
            /// 回访结果  1成功回访，0不成功回访 
            /// </summary>
            public string Reply_Result { get; set; }
            /// <summary>
            /// 跟进方案 成功回访为空，不成功回访必填
            /// </summary>
            public string Reply_Way { get; set; }
            /// <summary>
            /// 不成功原因 ，成功回访为空，不成功必填
            /// </summary>
            public string Reply_SituationCode { get; set; }
        }

        public class 进程处理_回访_5_BackParameter
        {
            public 进程处理_回访_5_BackParameter() { }
            /// <summary>
            /// 回访ID
            /// </summary>
            public string ReplyID { get; set; }
        }

        public class 进程处理_废弃_6
        {
            public 进程处理_废弃_6() { }

            /// <summary>
            /// 废弃时间 如果为空则默认为当前系统时间
            /// </summary>
            public string Delete_DateTime { get; set; }
            /// <summary>
            /// 废弃原因
            /// </summary>
            public string Delete_Reasons { get; set; }
            /// <summary>
            /// 废弃人
            /// </summary>
            public string DeleteMan { get; set; }
            /// <summary>
            /// 废弃人Code
            /// </summary>
            public string DeleteUserCode { get; set; }
            /// <summary>
            /// 废弃人loginCode
            /// </summary>
            public string Delete_UserLoginCode { get; set; }
        }

        public class 进程处理_回退_7
        {
            public 进程处理_回退_7() { }

            /// <summary>
            /// 回退类型 完成回退，关闭回退 必填
            /// </summary>
            public string Fallback_Type { get; set; }
            /// <summary>
            /// 回退原因
            /// </summary>
            public string FinishGoBack_Reasons { get; set; }
            /// <summary>
            /// 回退人Code
            /// </summary>
            public string FinishGoBackUserCode { get; set; }
            /// <summary>
            /// 回退人登录Code
            /// </summary>
            public string FinishGoBack_UserLoginCode { get; set; }

            /// <summary>
            /// 回退人
            /// </summary>
            public string FinishGoBackUser { get; set; }
            /// <summary>
            /// 回退时间
            /// </summary>
            public string FinishGoBack_DateTime { get; set; }
        }

        public class 进程处理_转发_8
        {
            public 进程处理_转发_8() { }

            /// <summary>
            /// 报事类别id
            /// </summary>
            public string Type_ID { get; set; }
            /// <summary>
            /// 责任人
            /// </summary>
            public string DealMan { get; set; }
            /// <summary>
            /// 责任人 usercode
            /// </summary>
            public string DealCode { get; set; }
            /// <summary>
            /// 责任人账号
            /// </summary>
            public string Deal_UserLoginCode { get; set; }
            /// <summary>
            /// 转发时间 为空则默认为当前系统时间
            /// </summary>
            public string Transmit_DateTime { get; set; }
            /// <summary>
            /// 转发人 usercode
            /// </summary>
            public string TransmitUserCode { get; set; }
            /// <summary>
            /// 转发人 Logincode
            /// </summary>
            public string Transmit_UserLoginCode { get; set; }
            /// <summary>
            /// 转发备注
            /// </summary>
            public string Transmit_Reasons { get; set; }
        }

        #endregion

        #endregion

    }
}
