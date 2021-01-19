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
using Model.HSPR;
using System.Security.Cryptography;
using KernelDev.DataAccess;

namespace Business
{
    public partial class HKParkCostInfo : PubInfo
    {
        Tb_HSPR_IncidentError log = new Tb_HSPR_IncidentError();
        public HKParkCostInfo() { }

        public override void Operate(ref Transfer Trans)
        {
            Trans.Result = JSONHelper.FromString(false, "未知错误");


            log.Method = Trans.Command;
            log.Parameter = Trans.Attribute;
            log.ErrorDate = DateTime.Now;

            switch (Trans.Command)
            {
                case "TempPayFees":
                    Trans.Result = TempPayFees(Trans.Attribute);//新增临时停车信息
                    break;

            }

        }
        /// <summary>
        /// 新增临时停车信息
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string TempPayFees(string requestParam)
        {
            try
            {
                if (string.IsNullOrEmpty(requestParam))
                {
                    new Logger().WriteLog("鸿坤道闸临停信息请求参数", DateTime.Now.ToString() + "--" + requestParam);
                    string State = "false";
                    string ErrorContent = JSONHelper.FromStringHK(false, "请求参数不能为空");
                    log.ErrorContent = ErrorContent;
                    log.State = State;
                    LogAdd(log);
                    return ErrorContent;
                }

                string ContionString = PubConstant.GetConnectionString("HKSQLConnection").ToString();
                IDbConnection con = new SqlConnection(ContionString);

                Tb_HSPR_HKCostInfo costInfo = JsonHelp.JsonDeserialize<Tb_HSPR_HKCostInfo>(requestParam);
                string CommID = null;//项目ID
                string sKey = "A2YGWFV7QP5KFOMLXIYRW";

                int parkId = costInfo.parkId;
                int duration = costInfo.duration;
                string dutyId = costInfo.dutyId;
                string endTime = costInfo.endTime;
                string feeAli = costInfo.feeAli;
                string feeCash = costInfo.feeCash;
                string feeCoupon = costInfo.feeCoupon;
                string feeException = costInfo.feeException;
                string feeExpected = costInfo.feeExpected;
                string feeFree = costInfo.feeFree;
                string feeWechat = costInfo.feeWechat;
                string roomNum = costInfo.roomNum;
                string startTime = costInfo.startTime;
                string tollManName = costInfo.tollManName;
                string HtcSign = costInfo.sign;

                //查询当前数据是否已经存在天问系统
                string tmpStr = "SELECT * FROM Tb_HSPR_TemporaryCarPrecostsDetail WHERE dutyId='" + dutyId + "'";
                DataTable dtTemporaryCar = new DbHelperSQLP(ContionString).Query(tmpStr).Tables[0];
                if (dtTemporaryCar.Rows.Count > 0)
                {
                    new Logger().WriteLog("鸿坤道闸临停信息同步异常", DateTime.Now.ToString() + "--" + "该条临停数据已经存在天问系统:" + tmpStr);

                    string State = "false";
                    string ErrorContent = JSONHelper.FromStringHK(false, "该条临停数据已经存在天问系统");
                    log.ErrorContent = ErrorContent;
                    log.State = State;
                    LogAdd(log);
                    return ErrorContent;
                }


                if (parkId <= 0)
                {
                    string State = "false";
                    string ErrorContent = JSONHelper.FromStringHK(false, "车场ID不能为空");
                    log.ErrorContent = ErrorContent;
                    log.State = State;
                    LogAdd(log);
                    return ErrorContent;
                }
                string signParm = "{\"duration\":" + duration + ",\"dutyId\":\"" + dutyId + "\",\"endTime\":\"" + endTime
                    + "\",\"feeAli\":" + feeAli + ",\"feeCash\":" + feeCash + ",\"feeCoupon\":" + feeCoupon + ",\"feeException\":"
                    + feeException + ",feeExpected:" + feeExpected + ",feeFree:" + feeFree + ",feeWechat:" + feeWechat
                    + ",\"roomNum\":\"" + roomNum + "\",\"startTime\":\"" + startTime + "\",\"tollManName\":\"" + tollManName + "\"}";

                Dictionary<String, String> param = JSONHelper.StringToDictionary<string, string>(signParm);
                string signStr = BuildParamStr(param) + "&key=" + sKey + "";
                string sign = MD5Encrypt(signStr);
                if (sign != HtcSign)
                {
                    new Logger().WriteLog("鸿坤道闸临停信息同步异常", DateTime.Now.ToString() + "--" + "签名错误:" + sign);

                    string State = "false";
                    string ErrorContent = JSONHelper.FromStringHK(false, "签名错误");
                    log.ErrorContent = ErrorContent;
                    log.State = State;
                    LogAdd(log);
                    return ErrorContent;
                }



                string ctpStr = "SELECT * FROM Tb_HTC_CommToPark WHERE ParkId='" + parkId + "'";
                DataTable dtCommToPark = new DbHelperSQLP(ContionString).Query(ctpStr).Tables[0];
                if (dtCommToPark.Rows.Count > 0)
                {
                    DataRow retRow = dtCommToPark.Rows[0];
                    CommID = retRow["CommID"].ToString();
                }
                else
                {
                    new Logger().WriteLog("鸿坤道闸临停信息同步异常", DateTime.Now.ToString() + "--" + "车场ID无法匹配天问CommID:" + ctpStr);

                    string State = "false";
                    string ErrorContent = JSONHelper.FromStringHK(false, "车场ID无法匹配天问CommID");
                    log.ErrorContent = ErrorContent;
                    log.State = State;
                    LogAdd(log);
                    return ErrorContent;
                }

                long CustId = 0;
                long RoomId = 0;
                long CostId = 0;
                long CorpStanId = 0;
                long StanId = 0;
                string UserCode = "";
                string csStr = "SELECT * FROM Tb_HTC_CostStandard WHERE ParkId='" + parkId + "'";
                DataTable dtCostStandard = new DbHelperSQLP(ContionString).Query(csStr).Tables[0];
                if (dtCostStandard.Rows.Count > 0)
                {
                    DataRow retRow = dtCostStandard.Rows[0];
                    CustId = AppGlobal.StrToLong(retRow["CustId"].ToString());
                    RoomId = AppGlobal.StrToLong(retRow["RoomId"].ToString());
                    CostId = AppGlobal.StrToLong(retRow["CostId"].ToString());
                    CorpStanId = AppGlobal.StrToLong(retRow["CorpStanId"].ToString());
                    StanId = AppGlobal.StrToLong(retRow["StanId"].ToString());
                    UserCode = retRow["UserCode"].ToString();
                }
                else
                {
                    new Logger().WriteLog("鸿坤道闸临停信息同步异常", DateTime.Now.ToString() + "--" + "车场ID无法匹配天问收费标准:" + csStr);
                    string State = "false";
                    string ErrorContent = JSONHelper.FromStringHK(false, "车场ID无法匹配天问收费标准");
                    log.ErrorContent = ErrorContent;
                    log.State = State;
                    LogAdd(log);
                    return ErrorContent;
                }
                try
                {
                    DynamicParameters parameters = new DynamicParameters();
                    var InfoCode = Guid.NewGuid();
                    parameters.Add("@InfoCode", InfoCode);
                    parameters.Add("@CardID", null);
                    parameters.Add("@CardType", null);
                    parameters.Add("@GoName", null);
                    parameters.Add("@CommID", CommID);
                    parameters.Add("@OverTime", startTime);
                    parameters.Add("@OutTime", endTime);
                    parameters.Add("@CarNumber", null);
                    parameters.Add("@Money", feeExpected);
                    parameters.Add("@IssueDate", null);
                    parameters.Add("@dutyId", dutyId);
                    parameters.Add("@feeCash", feeCash);
                    parameters.Add("@feeWechat", feeWechat);
                    parameters.Add("@feeAli", feeAli);

                    int b = con.Execute("Proc_HSPR_TemporaryCarPrecostsDetail_Insert", parameters, null, null, CommandType.StoredProcedure);
                    con.Dispose();
                    if (b > 0)
                    {
                        string Memo = "";
                        if (AppGlobal.StrToDec(feeAli) > 0)
                        {
                            Memo = "支付宝收款";
                            CreateCarFees(AppGlobal.StrToInt(CommID), CustId, RoomId, AppGlobal.StrToDec(feeAli), CorpStanId,
                                StanId, Memo, CostId, UserCode);
                        }
                        if (AppGlobal.StrToDec(feeWechat) > 0)
                        {
                            Memo = "微信收款";
                            CreateCarFees(AppGlobal.StrToInt(CommID), CustId, RoomId, AppGlobal.StrToDec(feeWechat), CorpStanId,
                                StanId, Memo, CostId, UserCode);
                        }
                        if (AppGlobal.StrToDec(feeCash) > 0)
                        {
                            Memo = "现金收款";
                            CreateCarFees(AppGlobal.StrToInt(CommID), CustId, RoomId, AppGlobal.StrToDec(feeCash), CorpStanId,
                                StanId, Memo, CostId, UserCode);
                        }
                        new Logger().WriteLog("鸿坤道闸临停信息请求参数", DateTime.Now.ToString() + "--同步成功 " + Memo);
                        string State = "true";
                        string ErrorContent = JSONHelper.FromStringHK(false, "同步成功");
                        log.ErrorContent = ErrorContent;
                        log.State = State;
                        LogAdd(log);
                        return ErrorContent;

                    }
                    else
                    {
                        new Logger().WriteLog("鸿坤道闸临停信息请求参数", DateTime.Now.ToString() + "--同步失败!");
                        string State = "false";
                        string ErrorContent = JSONHelper.FromStringHK(false, "同步失败");
                        log.ErrorContent = ErrorContent;
                        log.State = State;
                        LogAdd(log);
                        return ErrorContent;
                    }
                }
                catch (Exception ex)
                {
                    new Logger().WriteLog("鸿坤道闸临停信息同步异常", DateTime.Now.ToString() + "--" + ex.ToString());
                    string State = "false";
                    string ErrorContent = JSONHelper.FromStringHK(false, ex.Message);
                    log.ErrorContent = ErrorContent;
                    log.State = State;
                    LogAdd(log);
                    return ErrorContent;

                }

            }
            catch (Exception ex)
            {
                new Logger().WriteLog("鸿坤道闸临停信息同步异常", DateTime.Now.ToString() + "--" + ex.ToString());
                string State = "false";
                string ErrorContent = JSONHelper.FromStringHK(false, ex.Message);
                log.ErrorContent = ErrorContent;
                log.State = State;
                LogAdd(log);
                return ErrorContent;
            }



        }



        private void CreateCarFees(int CommID, long CustID, long RoomID, decimal DueAmount, long CorpStanID,
            long StanID, string Memo, long CostID, string LoginCode)
        {
            string strcon = PubConstant.GetConnectionString("HKSQLConnection").ToString();
            try
            {

                SqlParameter[] parameters = {
                    new SqlParameter("@FeesID", SqlDbType.BigInt),
                    new SqlParameter("@CommID", SqlDbType.Int),
                    new SqlParameter("@CostID", SqlDbType.BigInt),
                    new SqlParameter("@CustID", SqlDbType.BigInt),
                    new SqlParameter("@RoomID", SqlDbType.BigInt),
                    new SqlParameter("@FeesDueDate", SqlDbType.DateTime),
                    new SqlParameter("@FeesStateDate",SqlDbType.DateTime),
                    new SqlParameter("@FeesEndDate", SqlDbType.DateTime),
                    new SqlParameter("@DueAmount", SqlDbType.Decimal),
                    new SqlParameter("@DebtsAmount", SqlDbType.Decimal),
                    new SqlParameter("@WaivAmount", SqlDbType.Decimal),
                    new SqlParameter("@PrecAmount",SqlDbType.Decimal),
                    new SqlParameter("@PaidAmount", SqlDbType.Decimal),
                    new SqlParameter("@RefundAmount", SqlDbType.Decimal),
                    new SqlParameter("@IsAudit", SqlDbType.SmallInt),
                    new SqlParameter("@FeesMemo", SqlDbType.NVarChar),
                    new SqlParameter("@AccountFlag",SqlDbType.Int),
                    new SqlParameter("@IsBank", SqlDbType.SmallInt),
                    new SqlParameter("@IsCharge", SqlDbType.SmallInt),
                    new SqlParameter("@CorpStanID", SqlDbType.BigInt),
                    new SqlParameter("@StanID", SqlDbType.BigInt),
                    new SqlParameter("@AccountsDueDate",SqlDbType.DateTime),
                    new SqlParameter("@HandID", SqlDbType.BigInt),
                    new SqlParameter("@MeterSign", SqlDbType.NVarChar),
                    new SqlParameter("@CalcAmount", SqlDbType.Decimal),
                    new SqlParameter("@CalcAmount2", SqlDbType.Decimal),
                    new SqlParameter("@IncidentID",SqlDbType.BigInt),
                    new SqlParameter("@LeaseContID", SqlDbType.BigInt),
                    new SqlParameter("@ContID", SqlDbType.BigInt),
                    new SqlParameter("@PMeterID", SqlDbType.BigInt),
                    new SqlParameter("@StanMemo", SqlDbType.NVarChar),
                    new SqlParameter("@UserCode",SqlDbType.NVarChar),
                    new SqlParameter("@OrderCode", SqlDbType.NVarChar),
                    new SqlParameter("@IsPast", SqlDbType.SmallInt),
                    new SqlParameter("@AssumeCustID", SqlDbType.BigInt),
                    new SqlParameter("@ContSetID", SqlDbType.BigInt)
            };
                parameters[0].Value = 0;
                parameters[1].Value = CommID;
                parameters[2].Value = CostID;
                parameters[3].Value = CustID;
                parameters[4].Value = RoomID;
                parameters[5].Value = DateTime.Now;
                parameters[6].Value = DateTime.Now;
                parameters[7].Value = DateTime.Now;
                parameters[8].Value = DueAmount;
                parameters[9].Value = null;
                parameters[10].Value = null;
                parameters[11].Value = null;
                parameters[12].Value = null;
                parameters[13].Value = null;
                parameters[14].Value = null;
                parameters[15].Value = Memo;
                parameters[16].Value = 100;
                parameters[17].Value = null;
                parameters[18].Value = null;
                parameters[19].Value = CorpStanID;
                parameters[20].Value = StanID;
                parameters[21].Value = DateTime.Now;
                parameters[22].Value = 0;
                parameters[23].Value = null;
                parameters[24].Value = 0;
                parameters[25].Value = 1;
                parameters[26].Value = 0;
                parameters[27].Value = 0;
                parameters[28].Value = 0;
                parameters[29].Value = null;
                parameters[30].Value = null;
                parameters[31].Value = LoginCode;
                parameters[32].Value = null;
                parameters[33].Value = null;
                parameters[34].Value = null;
                parameters[35].Value = null;
                new DbHelperSQLP(strcon).RunProcedure("Proc_HSPR_Fees_Insert", parameters, "Ds");

            }
            catch (Exception ex)
            {
                new Logger().WriteLog("鸿坤道闸临停信息同步异常", DateTime.Now.ToString() + "--" + ex.ToString());
            }

        }

        /// <summary>
        /// 将参数排序组装,参数按照ASCII码从小到大排序
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static String BuildParamStr(Dictionary<String, String> param)
        {
            if (param == null || param.Count == 0)
            {
                return "";
            }
            Dictionary<string, string> asciiDic = new Dictionary<string, string>();
            string[] arrKeys = param.Keys.ToArray();
            //参数按照ASCII码从小到大排序
            Array.Sort(arrKeys, string.CompareOrdinal);

            foreach (var key in arrKeys)
            {
                string value = param[key];
                asciiDic.Add(key, value);
            }

            StringBuilder sb = new StringBuilder();
            foreach (var item in asciiDic)
            {
                sb.Append(item.Key).Append("=").Append(item.Value).Append("&");
            }

            return sb.ToString().Substring(0, sb.ToString().Length - 1);
        }


        /// <summary>
        /// md5加签
        /// </summary>
        /// <param name="strText"></param>
        /// <returns></returns>
        public static string MD5Encrypt(string strText)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(strText));
            return BitConverter.ToString(result).Replace("-", "");
        }

        public void LogAdd(Tb_HSPR_IncidentError model)
        {
            string strcon = PubConstant.GetConnectionString("HKSQLConnection").ToString();
            DataAccess DAccess = new DataAccess(strcon);
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
    }

}
