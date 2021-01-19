using Business;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.HongKun
{
    /// <summary>
    /// DataRequest 的摘要说明
    /// </summary>
    public class DataRequest : IHttpHandler
    {
        private static BasePage mBasePage;

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain;charset=utf-8";

            string command = context.Request.QueryString.Get("command");
            if (string.IsNullOrEmpty(command))
            {
                context.Response.Write(new ApiResult(false, "未知的command"));
                return;
            }

            string organCode = context.Request.QueryString.Get("organcode");
            string loginCode = context.Request.QueryString.Get("logincode");
            string userCode = context.Request.QueryString.Get("usercode");

            if (string.IsNullOrEmpty(organCode))
            {
                context.Response.Write(new ApiResult(false, "organcode参数错误").toJson());
            }
            if (string.IsNullOrEmpty(loginCode) && string.IsNullOrEmpty(userCode))
            {
                context.Response.Write(new ApiResult(false, "不允许匿名用户访问"));
                return;
            }
            string PreProduct = context.Request.QueryString.Get("PreProduct");
            mBasePage = new BasePage("1".Equals(PreProduct));

            if (string.IsNullOrEmpty(userCode))
            {
                userCode = mBasePage.GetUserCode(loginCode);

                if (string.IsNullOrEmpty(userCode))
                {
                    context.Response.Write(new ApiResult(false, "用户不存在"));
                    return;
                }
            }

            try
            {
                switch (command.ToLower())
                {

                    case "getuserorgantree":            // 获取用户有管理权限的机构/项目
                        context.Response.Write(new ApiResult(true, mBasePage.GetUserOrganTree(userCode)).toJson());
                        return;

                    // 收费
                    case "getchargekpi":
                        context.Response.Write(GetChargeApi(organCode));
                        return;
                    case "getchargelist":
                        context.Response.Write(GetChargeList(organCode, context));
                        return;
                    case "getchargerate":
                        context.Response.Write(GetChargeRate(organCode));
                        return;

                    // 设备
                    case "getequipmentdynamic":
                        context.Response.Write(GetEquipmentDynamic(organCode));
                        return;
                    case "getequipmentstatus":
                        context.Response.Write(GetEquipmentStatus(organCode));
                        return;

                    // 人事
                    case "getpersonelkpi":
                        context.Response.Write(GetPersonalKPI());
                        return;
                    case "getpersoneldynamiccompany":
                        context.Response.Write(GetPersonelCompanyStaffing(organCode, context));
                        return;
                    case "getpersoneldynamicstructure":
                        context.Response.Write(GetPersonelCompanyStructure(organCode, userCode, context));
                        return;

                    // 品质
                    case "getqualityquestion":
                        context.Response.Write(GetQualityQuestion(organCode));
                        return;
                    case "getqualitycompanystaffing":
                        context.Response.Write(GetQualityCompanyStaffing(organCode, context));
                        return;

                    // 资源
                    case "getresourcekpi":
                        context.Response.Write(GetResourceKPI(organCode));
                        return;
                    case "getproject":
                        context.Response.Write(GetProject(context,organCode));
                        return;
                    case "getmanageformat":
                        context.Response.Write(GetManageformat(organCode, context));
                        return;

                    // 客服
                    case "getservicetrends":
                        context.Response.Write(GetServiceTrends(organCode, context));
                        return;
                    case "getservicesource":
                        context.Response.Write(GetServiceSource(organCode, context));
                        return;
                    case "getservicecomplaints":
                        context.Response.Write(GetServiceComplaints(organCode, context));
                        return;
                    case "getservicekpi":
                        context.Response.Write(GetServiceKPI(organCode, context));
                        return;

                    default:
                        context.Response.Write(new ApiResult(false, "Hello"));
                        break;
                }
            }
            catch (Exception ex)
            {
                context.Response.Write(new ApiResult(false, ex.Message + Environment.NewLine + ex.StackTrace));
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        #region 收费模块
        private string GetChargeApi(string organCode)
        {
            return new DynamicManage().GetChargeKpi(organCode, organCode, mBasePage.ERPConnectionString);
        }

        private string GetChargeList(string organCode, HttpContext context)
        {
            int page = AppGlobal.StrToInt(context.Request.QueryString.Get("page"));
            int size = AppGlobal.StrToInt(context.Request.QueryString.Get("size"));
            string type = context.Request.QueryString.Get("type");

            return new DynamicManage().GetChargeList(organCode, mBasePage.ERPConnectionString, type, page, size);
        }

        private string GetChargeRate(string organCode)
        {
            return new DynamicManage().GetChargeRate(organCode, organCode, mBasePage.ERPConnectionString);
        }
        #endregion

        #region 设备模块
        private string GetEquipmentDynamic(string organCode)
        {
            return new DynamicManage().GetEquipmentDynamic(organCode, organCode, mBasePage.ERPConnectionString);
        }

        private string GetEquipmentStatus(string organCode)
        {
            return new DynamicManage().GetEquipmentStatus(organCode, organCode, mBasePage.ERPConnectionString);
        }
        #endregion

        #region 人事模块
        private string GetPersonalKPI()
        {
            return new DynamicManage().GetPersonalKPI(mBasePage.ERPConnectionString);
        }

        private string GetPersonelCompanyStaffing(string organCode, HttpContext context)
        {
            int startindex = AppGlobal.StrToInt(context.Request.QueryString.Get("startindex"));
            int endindex = AppGlobal.StrToInt(context.Request.QueryString.Get("endindex"));
            string type = context.Request.QueryString.Get("type");

            return new DynamicManage().GetPersonelCompanyStaffing(mBasePage.ERPConnectionString, type, startindex, endindex);
        }

        private string GetPersonelCompanyStructure(string organCode, string userCode, HttpContext context)
        {
            if (organCode.Length == 2 || organCode.Length == 4)
            {
                return new DynamicManage().GetPersonelCompanyStructure(organCode, null, userCode, mBasePage.ERPConnectionString);
            }
            else
            {
                return new DynamicManage().GetPersonelCompanyStructure(null, organCode, userCode, mBasePage.ERPConnectionString);
            }
        }
        #endregion

        #region 品质模块
        private string GetQualityQuestion(string organCode)
        {
            return new DynamicManage().GetQualityQuestion(organCode, organCode, mBasePage.ERPConnectionString);
        }

        private string GetQualityCompanyStaffing(string organCode, HttpContext context)
        {
            int page = AppGlobal.StrToInt(context.Request.QueryString.Get("page"));
            int rows = AppGlobal.StrToInt(context.Request.QueryString.Get("rows"));

            return new DynamicManage().GetQualityCompanyStaffing(organCode, organCode, mBasePage.ERPConnectionString, page, rows);
        }
        #endregion

        #region 资源模块
        private string GetResourceKPI(string organCode)
        {
            return new DynamicManage().GetResourceKPI(organCode, organCode, mBasePage.ERPConnectionString);
        }

        private string GetProject(HttpContext context ,String organCode)
        {
            int type = AppGlobal.StrToInt(context.Request.QueryString.Get("type"));

            return new DynamicManage().GetProject(mBasePage.ERPConnectionString, type, organCode);
        }

        private string GetManageformat(string organCode, HttpContext context)
        {
            int type = AppGlobal.StrToInt(context.Request.QueryString.Get("type"));
            return new DynamicManage().GetManageformat(organCode, organCode, mBasePage.ERPConnectionString, type);

        }
        #endregion

        #region 客服模块
        private string GetServiceTrends(string organCode, HttpContext context)
        {
            return new DynamicManage().GetServiceTrends(organCode, organCode, mBasePage.ERPConnectionString);
        }

        private string GetServiceSource(string organCode, HttpContext context)
        {
            return new DynamicManage().GetServiceSource(organCode, organCode, mBasePage.ERPConnectionString);
        }

        private string GetServiceComplaints(string organCode, HttpContext context)
        {
            return new DynamicManage().GetServiceComplaints(organCode, organCode, mBasePage.ERPConnectionString);
        }

        private string GetServiceKPI(string organCode, HttpContext context)
        {
            return new DynamicManage().GetServiceKPI(organCode, organCode, mBasePage.ERPConnectionString);
        }
        #endregion
    }
}