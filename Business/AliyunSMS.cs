using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Exceptions;
using Aliyun.Acs.Core.Http;
using Aliyun.Acs.Core.Profile;
using Common;
using MobileSoft.Common;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Configuration;


namespace Business
{
    class AliyunSMS
    {
        // 报事分派 SMS_177255339 
        // ${DispMan}通过ERP给您分派了一条${IncidentPlace}报事，报事人：${IncidentMan}，请及时处理。
        public const string TEMPLATE_INCIDENT_ASSIGN = "SMS_177255339";

        // 报事转发 
        // ${DispMan}给您转发了一条报事编号为${IncidentNumber}的${IncidentPlace}报事，请及时处理。
        public const string TEMPLATE_INCIDENT_TRANSMIT = "SMS_177552945";

        // 报事审核-非正常关闭-已通过-申请人 
        // 您请求非正常关闭的报事编号为${IncidentNumber}的申请已通过审核，审核人：${AuditUser}。
        public const string TEMPLATE_INCIDENT_AUDIT_UNNORMALCLOSE_PASSED = "SMS_177538266";

        // 报事审核-非正常关闭-未通过-申请人 
        // 您请求非正常关闭的报事编号为${IncidentNumber}的申请被拒，审核人：${AuditUser}
        public const string TEMPLATE_INCIDENT_AUDIT_UNNORMALCLOSE_FAILED = "SMS_177548198";

        // 报事审核-协助-已通过-申请人 
        // 您请求协助处理的报事编号为${IncidentNumber}的申请已通过审核，审核人：${AuditUser}。
        public const string TEMPLATE_INCIDENT_AUDIT_ASSIST_PASSED = "SMS_177553261";

        // 报事审核-协助-未通过-申请人 
        // 您请求协助处理的报事编号为${IncidentNumber}的申请已通过审核，审核人：${AuditUser}。
        public const string TEMPLATE_INCIDENT_AUDIT_ASSIST_FAILED = "SMS_177543286";

        // 报事审核-延期-已通过-申请人 
        // 您请求延期处理的报事编号为${IncidentNumber}的申请已通过审核，审核人：${AuditUser}。
        public const string TEMPLATE_INCIDENT_AUDIT_DELAY_PASSED = "SMS_177543423";

        // 报事审核-延期-未通过-申请人 
        // 您请求延期处理的报事编号为${IncidentNumber}的申请已被拒，审核人：${AuditUser}。
        public const string TEMPLATE_INCIDENT_AUDIT_DELAY_FAILED = "SMS_177543424";

        // 报事审核 审核人 
        // 报事编号为${IncidentNumber}的报事申请${AuditType}审核，申请人：${AuditUser}
        public const string TEMPLATE_INCIDENT_AUDIT = "SMS_177542161";

        public static void sendSMS(string templateCode,string phone,string param) 
        {

            int isRun = AppGlobal.StrToInt(ConfigurationManager.AppSettings["SMS_RUN"]);
            string id = ConfigurationManager.AppSettings["AliyunSMS_AccessKeyId"];
            string secret = ConfigurationManager.AppSettings["AliyunSMS_AccessSecret"];
            string signName = ConfigurationManager.AppSettings["AliyunSMS_SignName"];
            if (isRun==1)
            {
                IClientProfile profile = DefaultProfile.GetProfile("cn-hangzhou", id, secret);
                DefaultAcsClient client = new DefaultAcsClient(profile);
                CommonRequest request = new CommonRequest();
                request.Method = MethodType.POST;
                request.Domain = "dysmsapi.aliyuncs.com";
                request.Version = "2017-05-25";
                request.Action = "SendSms";
                // request.Protocol = ProtocolType.HTTP;
                request.AddQueryParameters("PhoneNumbers", phone);
                request.AddQueryParameters("SignName", signName);
                request.AddQueryParameters("TemplateCode", templateCode);
                request.AddQueryParameters("TemplateParam", param);
                try
                {
                    CommonResponse response = client.GetCommonResponse(request);
                    new Logger().WriteLog("阿里云短信发送日志", "发送参数:" + param+"ID:"+ templateCode + "电话:"+phone);
                    new Logger().WriteLog("阿里云短信发送日志", "发送结果:" + System.Text.Encoding.UTF8.GetString(response.HttpResponse.Content));
                }
                catch (ServerException e)
                {
                    new Logger().WriteLog("阿里云短信发送日志", "异常:" + e.Message);
                }
                catch (ClientException e)
                {
                    new Logger().WriteLog("阿里云短信发送日志", "异常:" + e.Message);
                }
            }
        }
        public static void sendBatchSms(string templateCode, List<string> phones, string param)
        {
            foreach (string phone in phones)
            {
                sendSMS(templateCode, phone, param);
            }
        }

        //public static void sendBatchSms(string templateCode, List<string> phones, string param)
        //{
        //    string phoneJson = JsonConvert.SerializeObject(phones);

        //    string id = ConfigurationManager.AppSettings["AliyunSMS_AccessKeyId"];
        //    string secret = ConfigurationManager.AppSettings["AliyunSMS_AccessSecret"];
        //    string signName = ConfigurationManager.AppSettings["AliyunSMS_SignName"];

        //    IClientProfile profile = DefaultProfile.GetProfile("cn-hangzhou", id, secret);
        //    DefaultAcsClient client = new DefaultAcsClient(profile);
        //    CommonRequest request = new CommonRequest();
        //    request.Method = MethodType.POST;
        //    request.Domain = "dysmsapi.aliyuncs.com";
        //    request.Version = "2017-05-25"; 
        //    request.Action = "SendBatchSms";
        //    // request.Protocol = ProtocolType.HTTP;
        //    request.AddQueryParameters("PhoneNumberJson", "[\"15900000000\",\"13500000000\"]");
        //    request.AddQueryParameters("SignNameJson", "[\"阿里云\",\"阿里巴巴\"]");
        //    request.AddQueryParameters("TemplateCode", "SMS_152550005");
        //    request.AddQueryParameters("TemplateParamJson", "[{\"name\":\"TemplateParamJson\"},{\"name\":\"TemplateParamJson\"}]");
        //    try
        //    {
        //        CommonResponse response = client.GetCommonResponse(request);
        //        new Logger().WriteLog("阿里云短信发送日志", "发送参数:" + param);
        //        new Logger().WriteLog("阿里云短信发送日志", "发送结果:" + Encoding.Default.GetString(response.HttpResponse.Content));
        //    }
        //    catch (ServerException e)
        //    {
        //        new Logger().WriteLog("阿里云短信发送日志", "异常:" + e.Message);
        //    }
        //    catch (ClientException e)
        //    {
        //        new Logger().WriteLog("阿里云短信发送日志", "异常:" + e.Message);
        //    }
        //}



        public static string incidentAssignParam(string DispMan, string incidentPlace, string incidentMan)
        {

            Dictionary<string, string> dic = new Dictionary<string, string>
                            {
                            { "DispMan", DispMan },
                            { "IncidentPlace", incidentPlace },
                            { "IncidentMan",incidentMan}
                            };
            return JsonConvert.SerializeObject(dic);
        }

        public static string incidentTransmitParam(string DispMan,string incidentPlace, string incidentNum)
        {

            Dictionary<string, string> dic = new Dictionary<string, string>
                            {
                            { "DispMan", DispMan },
                            { "IncidentPlace", incidentPlace },
                            { "IncidentNumber",incidentNum}
                            };
            return JsonConvert.SerializeObject(dic);
        }

        public static string incidentAuditParam(string incidentNum, string auditType, string auditUser)
        {

            Dictionary<string, string> dic = new Dictionary<string, string>
                            {
                            { "AuditUser", auditUser },
                            { "AuditType", auditType },
                            { "IncidentNumber",incidentNum}
                            };
            return JsonConvert.SerializeObject(dic);
        }

        public static string incidentAuditUnormalCloseParam(string incidentNum, string auditUser)
        {

            Dictionary<string, string> dic = new Dictionary<string, string>
                            {
                            { "AuditUser", auditUser },
                            { "IncidentNumber",incidentNum}
                            };
            return JsonConvert.SerializeObject(dic);
        }

        public static string incidentAuditAssistParam(string incidentNum, string auditUser)
        {

            Dictionary<string, string> dic = new Dictionary<string, string>
                            {
                            { "AuditUser", auditUser },
                            { "IncidentNumber",incidentNum}
                            };
            return JsonConvert.SerializeObject(dic);
        }

        public static string incidentAuditDelayParam(string incidentNum, string auditUser)
        {

            Dictionary<string, string> dic = new Dictionary<string, string>
                            {
                            { "AuditUser", auditUser },
                            { "IncidentNumber",incidentNum}
                            };
            return JsonConvert.SerializeObject(dic);
        }
    }
}
