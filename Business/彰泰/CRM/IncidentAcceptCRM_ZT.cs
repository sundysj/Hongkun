using Dapper;
using KernelDev.DataAccess;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using Model.HSPR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using System.Configuration;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Business
{
    public class IncidentAcceptCRM_ZT
    {
        //构建业务主库链接字符串 
        string ContionString_Base = PubConstant.GetConnectionString("ZTWYConnectionString").ToString();
        Tb_HSPR_IncidentError log = new Tb_HSPR_IncidentError(); //写入数据库日志


        public string IncidentCRM(string incidentid, CRMZTType type)
        {
            string strreturn = "";
            #region 验证是否存在工单
            string resultIncident = IsExistenceIncident(AppGlobal.StrToStr(incidentid));
            if (resultIncident.Split('|')[0] == "false")
            {
                return JSONHelper.FromString(false, resultIncident.Split('|')[1]);
            }
            #endregion

            switch (type.ToString())
            {
                case "受理":
                    strreturn = IncidentIns(incidentid);
                    break;
                case "分派":
                    strreturn = IncidentState(incidentid, "2");
                    break;
                //case "抢单":
                //    strreturn = IncidentState(incidentid, "2");
                //    break;
                //case "接单":
                //    strreturn = IncidentState(incidentid,"2");
                //    break;
                //case "到场":
                //    strreturn = IncidentState(incidentid, "2");
                //    break; 
                case "处理":
                    strreturn = IncidentState(incidentid, "3");
                    break;
                case "关闭":
                    strreturn = IncidentState(incidentid, "4");
                    break;
                case "回访":
                    strreturn = IncidentState(incidentid, "5");
                    break;
                case "转派":
                    strreturn = IncidentState(incidentid, "6");
                    break;
                case "作废":
                    strreturn = IncidentState(incidentid, "7");
                    break;
                //关闭退回
                case "退回":
                    strreturn = IncidentState(incidentid, "8");
                    break;
                case "回访退回":
                    strreturn = IncidentState(incidentid, "9");
                    break;

            }


            return strreturn;
        }



        //是否存在工单 
        public string IsExistenceIncident(string IncidentID)
        {
            string backstr = "";
            string errorContent = "";
            try
            {
                using (IDbConnection Conn = new SqlConnection(ContionString_Base))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(" SELECT IncidentID  FROM Tb_HSPR_Incidentaccept  where  1=1 ");

                    if (!string.IsNullOrEmpty(IncidentID))
                    {
                        sb.AppendFormat(" and IncidentID={0}", IncidentID);
                    }


                    DataSet ds = Conn.ExecuteReader(sb.ToString(), null, null, null, CommandType.Text).ToDataSet();

                    Conn.Close();
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        backstr = "true|";
                    }
                    else
                    {
                        errorContent = sb.ToString() + " " + ",暂无匹配工单";
                        backstr = "false|" + "暂无匹配工单";
                    }
                }

                return backstr;
            }
            catch (Exception ex)
            {
                backstr = ex.Message;
                log.ErrorContent = "false|" + backstr;
                LogAdd(log);
                return "false|" + backstr;
            }
        }
        //添加数据库日志
        public void LogAdd(Tb_HSPR_IncidentError model)
        {
            DataAccess DAccess = new DataAccess(Connection.GetConnection("15"));
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_Tb_HSPR_IncidentError_ADD";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@State,@ErrorContent,@Method,@ErrorDate,@Parameter";

            dbParams.Add("State", model.State, SqlDbType.NVarChar);
            dbParams.Add("ErrorContent", model.ErrorContent, SqlDbType.Text);
            dbParams.Add("Method", model.Method, SqlDbType.Text);
            dbParams.Add("ErrorDate", model.ErrorDate, SqlDbType.DateTime);
            dbParams.Add("Parameter", model.Parameter, SqlDbType.Text);

            DAccess.DataTable(dbParams);
        }


        //新增工单到CRM
        public string IncidentIns(string IncidentID)
        {

            Dictionary<string, string> dir = new Dictionary<string, string>();
            string Results = "";
            string strSQL = "select  * from view_HSPR_IncidentNewZT_IncidentFaceSearch_Filter where IncidentID =" + AppGlobal.StrToStr(IncidentID);
            DataTable dt2 = new DataTable();

            //   List<dynamic> list = new List<dynamic>(); 
            using (IDbConnection Conn = new SqlConnection(ContionString_Base))
            {
                dt2 = Conn.ExecuteReader(strSQL).ToDataSet().Tables[0];
                // list = Conn.Query<dynamic>("").ToList();
            }

            if (dt2.Rows.Count > 0)
            {

                DataRow row = dt2.Rows[0];

                dir.Add("erp_order_id", row["IncidentID"].ToString());
                dir.Add("incident_num", row["IncidentNum"].ToString());
                dir.Add("commid", row["CommID"].ToString());
                dir.Add("roomid", row["RoomID"].ToString());
                dir.Add("regional_id", row["RegionalID"].ToString());


                dir.Add("custid", row["CustID"].ToString());
                dir.Add("incident_Place", row["IncidentPlace"].ToString());
                dir.Add("incident_man", row["IncidentMan"].ToString());
                dir.Add("phone", row["Phone"].ToString());
                dir.Add("address", row["RoomName"].ToString());//报事地址
                dir.Add("incident_source", row["IncidentSource"].ToString());

                dir.Add("incident_mode", row["IncidentMode"].ToString());
                dir.Add("order_type", row["BigCorpTypeID"].ToString());//报事类型（大分类）
                dir.Add("question_type", row["FineCorpTypeID"].ToString());// 问题类型（小分类）
                dir.Add("resp_party", row["Duty"].ToString() == "物业类" ? "1" : "2");
                dir.Add("memo", row["IncidentContent"].ToString());
                dir.Add("answer_memo", "");//处理描述（我方处理问题的描述，咨询类工单客服直接回答业主时使用）
                dir.Add("incident_imgs", row["IncidentImgs"].ToString());
                dir.Add("emer_level", "");
                dir.Add("insert_time", row["IncidentDate"].ToString());
                dir.Add("insert_user_id", row["AdmiMan"].ToString());


                //JObject obj = (JObject)JsonConvert.DeserializeObject(new HttpHelpersZT().HttpPost("/api/addWorkOrder.idop", dir));

                JObject obj = null;

                //反写数据
                if (obj["errCode"].ToString().ToLower() == "1")
                {
                    int rowsAffected;
                    SqlParameter[] parameters = {
                                                new SqlParameter("@IncidentID", SqlDbType.NVarChar,100),
                                                new SqlParameter("@CRMGuid", SqlDbType.NVarChar,200)
                        };
                    parameters[0].Value = Convert.ToString(obj["data"]["erp_order_id"]);
                    parameters[1].Value = Convert.ToString(obj["data"]["crm_order_id"]);
                    new DbHelperSQLP(ContionString_Base).RunProcedure("UpdateCRMIncidentAccept", parameters, out rowsAffected);
                }

                Results = obj["errCode"].ToString().ToLower() + "|" + obj["errMsg"].ToString();

            }

            return Results;
        }

        //状态更新
        public string IncidentState(string IncidentID, string type)
        {



            Dictionary<string, string> dir = new Dictionary<string, string>();
            string Results = "";
            string strSQL = "select  * from view_HSPR_IncidentNewZT_IncidentFaceSearch_Filter where IncidentID =" + AppGlobal.StrToStr(IncidentID);
            DataTable dt2 = new DataTable();

            //   List<dynamic> list = new List<dynamic>(); 
            using (IDbConnection Conn = new SqlConnection(ContionString_Base))
            {
                dt2 = Conn.ExecuteReader(strSQL).ToDataSet().Tables[0];
                // list = Conn.Query<dynamic>("").ToList();
            }

            if (dt2.Rows.Count > 0)
            {

                DataRow row = dt2.Rows[0];
                dir.Add("erp_order_id", row["IncidentID"].ToString());
                dir.Add("crm_order_id", row["CRMGuid"].ToString());
                dir.Add("deal_type", type);

                //根据不同类别生成相应json
                Dictionary<string, string> dir2 = new Dictionary<string, string>();

                switch (type)
                {
                    //分派
                    case "2":

                        dir2.Add("disp_user_id", row["DispUserCode"].ToString());
                        dir2.Add("disp_user_name", row["DispMan"].ToString());

                        dir2.Add("corp_type_id", row["BigCorpTypeID"].ToString());
                        dir2.Add("corp_type_name", row["BigCorpTypeName"].ToString());

                        dir2.Add("deal_memo", row["DispRemarks"].ToString());
                        dir2.Add("deal_user_id", row["DealManCode"].ToString());
                        dir2.Add("deal_user_name", row["DealMan"].ToString());
                        dir2.Add("deal_time", row["DispDate"].ToString());

                        break;
                    //处理完成
                    case "3":



                        dir2.Add("deal_memo", row["DealSituation"].ToString());
                        dir2.Add("over_due_reason", row["OverdueReason"].ToString());
                        dir2.Add("deal_img", GetImgsUrl(row["IncidentID"].ToString(), "处理图片"));
                        dir2.Add("fine_type_id", row["FineCorpTypeID"].ToString());
                        dir2.Add("fine_type_name", row["FineTypeName"].ToString());

                        dir2.Add("deal_user_id", row["FinishUserCode"].ToString());
                        dir2.Add("deal_user_name", row["FinishUser"].ToString());
                        dir2.Add("deal_time", row["MainEndDate"].ToString());

                        break;
                    //报事关闭
                    case "4":

                        dir2.Add("deal_memo", row["CloseSituation"].ToString());
                        dir2.Add("close_type", row["CloseType"].ToString());
                        dir2.Add("no_normal_reason", row["NoNormalCloseReasons"].ToString());
                        dir2.Add("deal_img", GetImgsUrl(row["IncidentID"].ToString(), "关闭图片"));

                        dir2.Add("deal_user_id", row["CloseUserCode"].ToString());
                        dir2.Add("deal_user_name", row["CloseUser"].ToString());
                        dir2.Add("deal_time", row["CloseTime"].ToString());

                        break;
                    //报事回访
                    case "5":

                        dir2.Add("reply_type", row["ReplyResult"].ToString());

                        dir2.Add("reply_user_id", "0");//受访人
                        dir2.Add("reply_user_name", row["CustName"].ToString());

                        dir2.Add("deal_memo", row["ReplyContent"].ToString());
                        dir2.Add("deal_img", "");
                        dir2.Add("service_quality", row["ServiceQuality"].ToString());
                        dir2.Add("reply_result", row["ReplyResult"].ToString());
                        dir2.Add("reply_way", row["ReplyWay"].ToString());
                        dir2.Add("reply_situation", row["ReplyContent"].ToString());
                        dir2.Add("deal_user_id", row["ReplyManUserCode"].ToString());
                        dir2.Add("deal_user_name", row["ReplyMan"].ToString());
                        dir2.Add("deal_time", row["ReplyDate"].ToString());

                        break;
                    case "6":
                        // 转派责任方



                        dir2.Add("disp_user_id", row["DealManCode"].ToString());
                        dir2.Add("disp_user_name", row["DealMan"].ToString());
                        dir2.Add("corp_type_id", row["BigCorpTypeID"].ToString());
                        dir2.Add("corp_type_name", row["BigCorpTypeName"].ToString());


                        using (IDbConnection Conn = new SqlConnection(ContionString_Base))
                        {



                            DataTable dt3 = Conn.ExecuteReader($@" 
                            select   b.UserName,a.* ,b.HeadImg from Tb_HSPR_IncidentTransmit a
                            left join tb_sys_user b
                            on a.TransmitUserCode=b.UserCode
                            where isnull(b.IsDelete,0)=0 and IncidentID={IncidentID}").ToDataSet().Tables[0];
                            if (dt3.Rows.Count > 0)
                            {

                                dir2.Add("deal_memo", dt3.Rows[0]["TransmitReasons"].ToString());
                                dir2.Add("deal_img", dt3.Rows[0]["HeadImg"].ToString());
                                dir2.Add("deal_user_id", dt3.Rows[0]["TransmitUserCode"].ToString());
                                dir2.Add("deal_user_name", dt3.Rows[0]["UserName"].ToString());
                                dir2.Add("deal_time", dt3.Rows[0]["TransmitDateTime"].ToString());
                            }
                            Conn.Close();

                        }

                        break;
                    case "7":
                        //报事废弃   
                        if (row["IsDelete"].ToString() == "1")
                        {
                            dir2.Add("deal_memo", row["DeleteReasons"].ToString());
                            dir2.Add("deal_img", "");
                            dir2.Add("deal_user_id", row["DeleteManCode"].ToString());
                            dir2.Add("deal_user_name", row["DeleteMan"].ToString());
                            dir2.Add("deal_time", row["DeleteDate"].ToString());
                        }

                        break;
                    case "8":
                        //关闭回退 
                        using (IDbConnection Conn = new SqlConnection(ContionString_Base))
                        {

                            DataTable dt3 = Conn.ExecuteReader($@" 
                            select   * from Tb_HSPR_IncidentCloseGoBack  
                            where  IncidentID={IncidentID}").ToDataSet().Tables[0];
                            if (dt3.Rows.Count > 0)
                            {
                                dir2.Add("fallback_type", "关闭回退");
                                dir2.Add("deal_memo", dt3.Rows[0]["CloseGoBackReasons"].ToString());
                                dir2.Add("deal_img", "");
                                dir2.Add("deal_user_id", dt3.Rows[0]["CloseGoBackUserCode"].ToString());
                                dir2.Add("deal_user_name", dt3.Rows[0]["CloseGoBackUser"].ToString());
                                dir2.Add("deal_time", dt3.Rows[0]["CloseGoBackTime"].ToString());
                            }
                            Conn.Close();

                        }
                        break;

                    case "9":
                        //回访回退 

                        using (IDbConnection Conn = new SqlConnection(ContionString_Base))
                        {
                            DataTable dt3 = Conn.ExecuteReader($@" 
                            select   * from Tb_HSPR_IncidentReplyGoBack  
                            where  IncidentID={IncidentID}").ToDataSet().Tables[0];
                            if (dt3.Rows.Count > 0)
                            {
                                dir2.Add("fallback_type", "回访回退");
                                dir2.Add("deal_memo", dt3.Rows[0]["LastCloseSituation"].ToString());
                                dir2.Add("deal_img", "");
                                dir2.Add("deal_user_id", dt3.Rows[0]["ReplyGoBackUserCode"].ToString());
                                dir2.Add("deal_user_name", dt3.Rows[0]["ReplyGoBackUser"].ToString());
                                dir2.Add("deal_time", dt3.Rows[0]["ReplyGoBackTime"].ToString());
                            }
                            Conn.Close();
                        }
                        break;
                }

                dir.Add("deal_info", JsonConvert.SerializeObject(dir2));

                //JObject obj = (JObject)JsonConvert.DeserializeObject(new HttpHelpersZT().HttpPost("/api/workOrderOper.idop", dir));

                JObject obj = null;



                ////反写数据
                //if (obj["errCode"].ToString().ToLower() == "1")
                //{
                //    int rowsAffected;
                //    SqlParameter[] parameters = {
                //                                new SqlParameter("@IncidentID", SqlDbType.NVarChar,100),
                //                                new SqlParameter("@CRMGuid", SqlDbType.NVarChar,200)
                //        };
                //    parameters[0].Value = Convert.ToString(obj["data"]["erp_order_id"]);
                //    parameters[1].Value = Convert.ToString(obj["data"]["crm_order_id"]);
                //    new DbHelperSQLP(ContionString_Base).RunProcedure("UpdateMtIncidentAccept", parameters, out rowsAffected);
                //}

                Results = obj["errCode"].ToString().ToLower() + "|" + obj["errMsg"].ToString();

            }

            return Results;

        }

        public string GetImgsUrl(string incidentid, string type)
        {
            string url = "";
            StringBuilder sburl = new StringBuilder();
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(@" select  (
SELECT    CAST((xxx.FilPath)AS NVARCHAR(MAX))
                                    + ','
                          FROM      Tb_HSPR_IncidentAdjunct xxx
                          WHERE     xxx.IncidentID = {0} and AdjunctName='{1}'
                          ORDER BY  AdjunctCode DESC
                        FOR
                          XML PATH('')
                          ) as name", incidentid, type);
            using (IDbConnection Conn = new SqlConnection(ContionString_Base))
            {
                DataTable dt3 = Conn.ExecuteReader(sb.ToString()).ToDataSet().Tables[0];
                DataRow row = dt3.Rows[0];
                if (dt3.Rows.Count > 0)
                {
                    // url= string.Join(",", row["name"].ToString().Split(',').Select(a =>!string.IsNullOrEmpty( a)?a:""));

                    foreach (string item in row["name"].ToString().Split(','))
                    {
                        if (!string.IsNullOrEmpty(item))
                        {
                            sburl.Append(string.Join(",", item));

                        }
                    }

                }
                Conn.Close();

            }

            return sburl.ToString();
        }
    }


    public enum CRMZTType : int
    {

        受理 = 0,
        抢单,       // App-暂时未用，都叫分派
        分派,
        接单,       // App
        到场,       // App
        转派,       // App叫转发-口派转书面和书面转发都是这个
        跟进,       // App
        处理,       // App
        关闭,
        退回,       // 关闭退回处理
        回访退回,       // 关闭退回处理
        回访,
        作废        // App叫废弃
    }




}
