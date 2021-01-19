using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Configuration;
using System.Data;

namespace MobileSoft.Common
{
    public class Global_Var
    {
        public static string CustSystemName = "爱享";//公司默认登陆ID

        private Global_Var()
        {

        }

        public static string ERPDatabaseName
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["ERPDatabaseName"] != null)
                {
                    return System.Web.HttpContext.Current.Session["ERPDatabaseName"].ToString();
                }
                else
                {
                    return "";
                }
            }
            set
            {
                System.Web.HttpContext.Current.Session["ERPDatabaseName"] = value;
            }
        }

        public class BurstConnectionModel
        {
            public int CommID { get; set; }
            public string Type { get; set; }
            public string ReadonlyConnectionString { get; set; }
            public string ConnectionString { get; set; }
        }

        public static List<BurstConnectionModel> BurstConnectionPool
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["BurstConnectionPool"] != null)
                {
                    return System.Web.HttpContext.Current.Session["BurstConnectionPool"] as List<BurstConnectionModel>;
                }
                else
                {
                    var x = new List<BurstConnectionModel>();
                    System.Web.HttpContext.Current.Session["BurstConnectionPool"] = x;
                    return x;
                }
            }
        }


        public static string SystemType
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["SystemType"] != null)
                {
                    return System.Web.HttpContext.Current.Session["SystemType"].ToString();
                }
                else
                {
                    return "";
                }
            }
            set
            {
                System.Web.HttpContext.Current.Session["SystemType"] = value;
            }
        }

        //用户IP地址
        public static string UserHostAddress
        {
            get
            {
                string _UserHostAddress = "";

                try
                {
                    if (System.Web.HttpContext.Current.Session["UserHostAddress"] != null)
                    {
                        _UserHostAddress = System.Web.HttpContext.Current.Session["UserHostAddress"].ToString();
                    }
                }
                catch
                { }

                return _UserHostAddress;
            }
            set
            {
                System.Web.HttpContext.Current.Session["UserHostAddress"] = value;
            }
        }

        public static string SystemName
        {
            get
            {
                string Ret = "";
                if (SystemType == "cust")
                {
                    if (CommName != null)
                    {
                        Ret = CommName.ToString() + "掌上家园";

                    }
                }
                else
                {
                    Ret = "掌上物管";
                }
                return Ret;
            }
        }

        public static string IPAddRessArr
        {
            get
            {
                return WebConfigurationManager.AppSettings["IPAddRessArr"].ToString();
            }
        }

        public static string ehome
        {
            get
            {
                return WebConfigurationManager.AppSettings["ehome"].ToString();
            }
        }

        public static string LoginSQLALLSQBSConnStr
        {
            get
            {
                string _LoginSQLALLSQBSConnStr = "";

                try
                {
                    if (System.Web.HttpContext.Current.Session["LoginSQLALLSQBSConnStr"] != null)
                    {
                        _LoginSQLALLSQBSConnStr = System.Web.HttpContext.Current.Session["LoginSQLALLSQBSConnStr"].ToString();
                    }
                }
                catch
                { }

                return _LoginSQLALLSQBSConnStr;
            }
            set
            {
                System.Web.HttpContext.Current.Session["LoginSQLALLSQBSConnStr"] = value;
            }
        }

        public static string SQMContionString
        {
            get
            {
                return WebConfigurationManager.AppSettings["SQMContionString"].ToString();
            }
            set
            {
                System.Web.HttpContext.Current.Session["SQMContionString"] = value;
            }
        }

        public static string SQIBSContionString
        {
            get
            {
                return WebConfigurationManager.AppSettings["SQIBSContionString"].ToString();
            }
            set
            {
                System.Web.HttpContext.Current.Session["SQIBSContionString"] = value;
            }
        }

        public static string LoginSQLConnStr
        {
            get
            {
                string _LoginSQLConnStr = "";

                try
                {
                    if (System.Web.HttpContext.Current.Session["LoginSQLConnStr"] != null)
                    {
                        _LoginSQLConnStr = System.Web.HttpContext.Current.Session["LoginSQLConnStr"].ToString();
                    }
                }
                catch
                { }

                return _LoginSQLConnStr;
            }
            set
            {
                System.Web.HttpContext.Current.Session["LoginSQLConnStr"] = value;
            }
        }

        public static string CorpSQLConnstr
        {
            get
            {
                string _CorpSQLConnstr = "";

                try
                {
                    if (System.Web.HttpContext.Current.Session["CorpSQLConnstr"] != null)
                    {
                        _CorpSQLConnstr = System.Web.HttpContext.Current.Session["CorpSQLConnstr"].ToString();
                    }
                }
                catch
                { }

                return _CorpSQLConnstr;
            }
            set
            {
                System.Web.HttpContext.Current.Session["CorpSQLConnstr"] = value;
            }
        }


        public static string CorpServerIP
        {
            get
            {
                string _CorpServerIP = "";
                try
                {
                    if (System.Web.HttpContext.Current.Session["CorpServerIP"] != null)
                    {
                        _CorpServerIP = System.Web.HttpContext.Current.Session["CorpServerIP"].ToString();
                    }
                }
                catch
                { }

                return _CorpServerIP;
            }
            set
            {
                System.Web.HttpContext.Current.Session["CorpServerIP"] = value;
            }
        }

        #region OA文件相对路径

        public static string OaMobileFileBuildAddr
        {
            get
            {
                string _OaMobileFileBuildAddr = "";

                _OaMobileFileBuildAddr = WebConfigurationManager.AppSettings["OaMobileFileBuildAddr"].ToString();

                return _OaMobileFileBuildAddr;
            }
        }
        public static string OaFileDocument
        {
            get
            {
                string _OaFileDocument = "";

                _OaFileDocument = WebConfigurationManager.AppSettings["OaFileDocument"].ToString();

                return _OaFileDocument;
            }
        }

        public static string OaFileAddr
        {
            get
            {
                string _OaFileAddr = "";

                _OaFileAddr = WebConfigurationManager.AppSettings["OaFileAddr"].ToString();

                return _OaFileAddr;
            }
        }

        public static string LoginSQLDX1SQConnStr
        {
            get
            {
                string _SQLContionDX1SQString = "";

                _SQLContionDX1SQString = WebConfigurationManager.AppSettings["SQLContionDX1SQString"].ToString();

                return _SQLContionDX1SQString;
            }
        }

        public static string OaMobileFileAddr
        {
            get
            {
                string _OaMobileFileAddr = "";

                _OaMobileFileAddr = WebConfigurationManager.AppSettings["OaMobileFileAddr"].ToString();

                return _OaMobileFileAddr;
            }
        }

        //OA上传文档
        public static string DocumentUrl
        {
            get
            {
                string _DocumentUrl = "";
                try
                {
                    if (System.Web.HttpContext.Current.Session["DocumentUrl"] != null)
                    {
                        _DocumentUrl = System.Web.HttpContext.Current.Session["DocumentUrl"].ToString();
                    }
                }
                catch
                { }

                return _DocumentUrl;
            }
            set
            {
                System.Web.HttpContext.Current.Session["DocumentUrl"] = value;
            }
        }

        #endregion



        #region 社区图片上传路径

        //项目ID
        public static string UploadImageUrl
        {
            get
            {
                string _UploadImageUrl = "";

                _UploadImageUrl = WebConfigurationManager.AppSettings["UploadImageUrl"].ToString();

                return _UploadImageUrl;
            }
        }

        #endregion

        #region 居民信息

        //居民帐户ID
        public static string LoginCustID
        {
            get
            {
                string _LoginCustID = "";

                try
                {
                    if (System.Web.HttpContext.Current.Session["LoginCustID"] != null)
                    {
                        _LoginCustID = System.Web.HttpContext.Current.Session["LoginCustID"].ToString();
                    }
                }
                catch
                { }

                return _LoginCustID;
            }
            set
            {
                System.Web.HttpContext.Current.Session["LoginCustID"] = value;
            }
        }

        //居民帐户图片
        public static string LoginCustImgUrl
        {
            get
            {
                string _LoginCustImgUrl = "";

                try
                {
                    if (System.Web.HttpContext.Current.Session["LoginCustImgUrl"] != null)
                    {
                        _LoginCustImgUrl = System.Web.HttpContext.Current.Session["LoginCustImgUrl"].ToString();
                    }
                }
                catch
                { }

                return _LoginCustImgUrl;
            }
            set
            {
                System.Web.HttpContext.Current.Session["LoginCustImgUrl"] = value;
            }
        }

        //居民帐户密码
        public static string LoginCustPwd
        {
            get
            {
                string _LoginCustPwd = "";

                try
                {
                    if (System.Web.HttpContext.Current.Session["_LoginCustPwd"] != null)
                    {
                        _LoginCustPwd = System.Web.HttpContext.Current.Session["_LoginCustPwd"].ToString();
                    }
                }
                catch
                { }

                return _LoginCustPwd;
            }
            set
            {
                System.Web.HttpContext.Current.Session["_LoginCustPwd"] = value;
            }
        }

        //昵称
        public static string LoginNickName
        {
            get
            {
                string _LoginNickName = "";

                try
                {
                    if (System.Web.HttpContext.Current.Session["LoginNickName"] != null)
                    {
                        _LoginNickName = System.Web.HttpContext.Current.Session["LoginNickName"].ToString();
                    }
                }
                catch
                { }

                return _LoginNickName;
            }
            set
            {
                System.Web.HttpContext.Current.Session["LoginNickName"] = value;
            }
        }


        //所在小区
        public static string Title
        {
            get
            {
                string _Title = "";

                try
                {
                    if (DataSecurity.StrToInt(MobileSoft.Common.Global_Var.LoginCommID.ToString()) != 0)
                    {
                        _Title = Global_Var.LoginCommName.ToString() + "首页";
                    }
                    else
                    {
                        _Title = Global_Var.LoginOrganName.ToString() + "首页";
                    }
                }
                catch
                { }

                return _Title;
            }
            set
            {
                System.Web.HttpContext.Current.Session["Title"] = value;
            }
        }

        //所在小区
        public static string LoginCommName
        {
            get
            {
                string _LoginCommName = "";

                try
                {
                    if (System.Web.HttpContext.Current.Session["LoginCommName"] != null)
                    {
                        _LoginCommName = System.Web.HttpContext.Current.Session["LoginCommName"].ToString();
                    }
                }
                catch
                { }

                return _LoginCommName;
            }
            set
            {
                System.Web.HttpContext.Current.Session["LoginCommName"] = value;
            }
        }

        //所在市区
        public static string LoginCityName
        {
            get
            {
                string _LoginCityName = "";

                try
                {
                    if (System.Web.HttpContext.Current.Session["LoginCityName"] != null)
                    {
                        _LoginCityName = System.Web.HttpContext.Current.Session["LoginCityName"].ToString();
                    }
                }
                catch
                { }

                return _LoginCityName;
            }
            set
            {
                System.Web.HttpContext.Current.Session["LoginCityName"] = value;
            }
        }

        //所在区县
        public static string LoginBoroughName
        {
            get
            {
                string _LoginBoroughName = "";

                try
                {
                    if (System.Web.HttpContext.Current.Session["LoginBoroughName"] != null)
                    {
                        _LoginBoroughName = System.Web.HttpContext.Current.Session["LoginBoroughName"].ToString();
                    }
                }
                catch
                { }

                return _LoginBoroughName;
            }
            set
            {
                System.Web.HttpContext.Current.Session["LoginBoroughName"] = value;
            }
        }
        //所在街道
        public static string LoginStreetName
        {
            get
            {
                string _LoginStreetName = "";

                try
                {
                    if (System.Web.HttpContext.Current.Session["LoginStreetName"] != null)
                    {
                        _LoginStreetName = System.Web.HttpContext.Current.Session["LoginStreetName"].ToString();
                    }
                }
                catch
                { }

                return _LoginStreetName;
            }
            set
            {
                System.Web.HttpContext.Current.Session["LoginStreetName"] = value;
            }
        }

        //所在街道ID
        public static string LoginStreetID
        {
            get
            {
                string _LoginStreetID = "";

                try
                {
                    if (System.Web.HttpContext.Current.Session["LoginStreetID"] != null)
                    {
                        _LoginStreetID = System.Web.HttpContext.Current.Session["LoginStreetID"].ToString();
                    }
                }
                catch
                { }

                return _LoginStreetID;
            }
            set
            {
                System.Web.HttpContext.Current.Session["LoginStreetID"] = value;
            }
        }

        public static string LoginStreetCode
        {
            get
            {
                string _LoginStreetCode = "";

                try
                {
                    if (System.Web.HttpContext.Current.Session["LoginStreetCode"] != null)
                    {
                        _LoginStreetCode = System.Web.HttpContext.Current.Session["LoginStreetCode"].ToString();
                    }
                }
                catch
                { }

                return _LoginStreetCode;
            }
            set
            {
                System.Web.HttpContext.Current.Session["LoginStreetCode"] = value;
            }
        }

        //所在街道图片
        public static string LoginLogoImgSrc
        {
            get
            {
                string _LoginLogoImgSrc = "";

                try
                {
                    if (System.Web.HttpContext.Current.Session["LoginLogoImgSrc"] != null)
                    {
                        _LoginLogoImgSrc = System.Web.HttpContext.Current.Session["LoginLogoImgSrc"].ToString();
                    }
                }
                catch
                { }

                return _LoginLogoImgSrc;
            }
            set
            {
                System.Web.HttpContext.Current.Session["LoginLogoImgSrc"] = value;
            }
        }
        //居民帐户ID
        public static string IsLogin
        {
            get
            {
                string _IsLogin = "";

                try
                {
                    if (System.Web.HttpContext.Current.Session["IsLogin"] != null)
                    {
                        _IsLogin = System.Web.HttpContext.Current.Session["IsLogin"].ToString();
                    }
                }
                catch
                { }

                return _IsLogin;
            }
            set
            {
                System.Web.HttpContext.Current.Session["IsLogin"] = value;
            }
        }
        #endregion

        //服务器IP
        public static string ServerIp
        {
            get
            {
                string _ServerIp = "";

                try
                {
                    if (System.Web.HttpContext.Current.Session["ServerIp"] != null)
                    {
                        _ServerIp = System.Web.HttpContext.Current.Session["ServerIp"].ToString();
                    }
                }
                catch
                { }

                return _ServerIp;
            }
            set
            {
                System.Web.HttpContext.Current.Session["ServerIp"] = value;
            }
        }

        //上传图片路径
        public static string CommImageUrl
        {
            get
            {
                string _CommImageUrl = "";

                try
                {
                    if (System.Web.HttpContext.Current.Session["CommImageUrl"] != null)
                    {
                        _CommImageUrl = System.Web.HttpContext.Current.Session["CommImageUrl"].ToString();
                    }
                }
                catch
                { }

                return _CommImageUrl;
            }
            set
            {
                System.Web.HttpContext.Current.Session["CommImageUrl"] = value;
            }
        }
        //公司ID
        public static string CorpId
        {
            get
            {
                string _CorpId = "";

                try
                {
                    if (System.Web.HttpContext.Current.Session["CorpId"] != null)
                    {
                        _CorpId = System.Web.HttpContext.Current.Session["CorpId"].ToString();
                    }
                }
                catch
                { }

                return _CorpId;
            }
            set
            {
                System.Web.HttpContext.Current.Session["CorpId"] = value;
            }
        }

        //公司ID
        public static string LoginCode
        {
            get
            {
                string _LoginCode = "";

                try
                {
                    if (System.Web.HttpContext.Current.Session["LoginCode"] != null)
                    {
                        _LoginCode = System.Web.HttpContext.Current.Session["LoginCode"].ToString();
                    }
                }
                catch
                { }

                return _LoginCode;
            }
            set
            {
                System.Web.HttpContext.Current.Session["LoginCode"] = value;
            }
        }


        //机构编码
        public static string OrganCode
        {
            get
            {
                string _OrganCode = "";

                try
                {
                    if (System.Web.HttpContext.Current.Session["OrganCode"] != null)
                    {
                        _OrganCode = System.Web.HttpContext.Current.Session["OrganCode"].ToString();
                    }
                }
                catch
                { }

                return _OrganCode;
            }
            set
            {
                System.Web.HttpContext.Current.Session["OrganCode"] = value;
            }
        }

        //分公司编码
        public static string OrganCorp
        {
            get
            {
                string _OrganCorp = "";

                try
                {
                    if (System.Web.HttpContext.Current.Session["OrganCorp"] != null)
                    {
                        _OrganCorp = System.Web.HttpContext.Current.Session["OrganCorp"].ToString();
                    }
                }
                catch
                { }

                return _OrganCorp;
            }
            set
            {
                System.Web.HttpContext.Current.Session["OrganCorp"] = value;
            }
        }

        //登陆人员所属岗位
        public static string LoginRoles
        {
            get
            {
                string _LoginRoles = "";

                try
                {
                    if (System.Web.HttpContext.Current.Session["LoginRoles"] != null)
                    {
                        _LoginRoles = System.Web.HttpContext.Current.Session["LoginRoles"].ToString();
                    }
                }
                catch
                { }

                return _LoginRoles;
            }
            set
            {
                System.Web.HttpContext.Current.Session["LoginRoles"] = value;
            }
        }

        //用户编码
        public static string UserCode
        {
            get
            {
                string _UserCode = "";

                try
                {
                    if (System.Web.HttpContext.Current.Session["UserCode"] != null)
                    {
                        _UserCode = System.Web.HttpContext.Current.Session["UserCode"].ToString();
                    }
                }
                catch
                { }

                return _UserCode;
            }
            set
            {
                System.Web.HttpContext.Current.Session["UserCode"] = value;
            }
        }

        //小区名称
        public static string CommName
        {
            get
            {
                string _CommName = "";

                try
                {
                    if (System.Web.HttpContext.Current.Session["CommName"] != null)
                    {
                        _CommName = System.Web.HttpContext.Current.Session["CommName"].ToString();
                    }
                }
                catch
                { }

                return _CommName;
            }
            set
            {
                System.Web.HttpContext.Current.Session["CommName"] = value;
            }
        }

        public static string UserName
        {
            get
            {
                string _UserName = "";

                try
                {
                    if (System.Web.HttpContext.Current.Session["UserName"] != null)
                    {
                        _UserName = System.Web.HttpContext.Current.Session["UserName"].ToString();
                    }
                }
                catch
                { }

                return _UserName;
            }
            set
            {
                System.Web.HttpContext.Current.Session["UserName"] = value;
            }
        }

        public static DataTable Tb_WorkFlow_Instance
        {
            get
            {
                DataTable _Tb_WorkFlow_Instance = null;

                try
                {
                    if (System.Web.HttpContext.Current.Session["Tb_WorkFlow_Instance"] != null)
                    {
                        _Tb_WorkFlow_Instance = (DataTable)System.Web.HttpContext.Current.Session["Tb_WorkFlow_Instance"];
                    }
                }
                catch
                { }

                return _Tb_WorkFlow_Instance;
            }
            set
            {
                System.Web.HttpContext.Current.Session["Tb_WorkFlow_Instance"] = value;
            }
        }

        public static DataTable Tb_WorkFlow_FlowNode
        {
            get
            {
                DataTable _Tb_WorkFlow_FlowNode = null;

                try
                {
                    if (System.Web.HttpContext.Current.Session["Tb_WorkFlow_FlowNode"] != null)
                    {
                        _Tb_WorkFlow_FlowNode = (DataTable)System.Web.HttpContext.Current.Session["Tb_WorkFlow_FlowNode"];
                    }
                }
                catch
                { }

                return _Tb_WorkFlow_FlowNode;
            }
            set
            {
                System.Web.HttpContext.Current.Session["Tb_WorkFlow_FlowNode"] = value;
            }
        }

        #region 社区

        public static string LoginUserCode
        {
            get
            {
                string _LoginUserCode = "";

                try
                {
                    if (System.Web.HttpContext.Current.Session["LoginUserCode"] != null)
                    {
                        _LoginUserCode = System.Web.HttpContext.Current.Session["LoginUserCode"].ToString();
                    }
                }
                catch
                { }

                return _LoginUserCode;
            }
            set
            {
                System.Web.HttpContext.Current.Session["LoginUserCode"] = value;
            }
        }
        public static string LoginUserName
        {
            get
            {
                string _LoginUserName = "";

                try
                {
                    if (System.Web.HttpContext.Current.Session["LoginUserName"] != null)
                    {
                        _LoginUserName = System.Web.HttpContext.Current.Session["LoginUserName"].ToString();
                    }
                }
                catch
                { }

                return _LoginUserName;
            }
            set
            {
                System.Web.HttpContext.Current.Session["LoginUserName"] = value;
            }
        }
        public static string LoginDepCode
        {
            get
            {
                string _LoginDepCode = "";

                try
                {
                    if (System.Web.HttpContext.Current.Session["LoginDepCode"] != null)
                    {
                        _LoginDepCode = System.Web.HttpContext.Current.Session["LoginDepCode"].ToString();
                    }
                }
                catch
                { }

                return _LoginDepCode;
            }
            set
            {
                System.Web.HttpContext.Current.Session["LoginDepCode"] = value;
            }
        }

        /// <summary>
        /// 2017年6月12日16:04:41，谭洋，用于解决获取新闻公告，总部工作无法获取的问题
        /// </summary>
        public static string LoginSortDepCode
        {
            get
            {
                string _LoginSortDepCode = "";

                try
                {
                    if (System.Web.HttpContext.Current.Session["LoginSortDepCode"] != null)
                    {
                        _LoginSortDepCode = System.Web.HttpContext.Current.Session["LoginSortDepCode"].ToString();
                    }
                }
                catch
                { }

                return _LoginSortDepCode;
            }
            set
            {
                System.Web.HttpContext.Current.Session["LoginSortDepCode"] = value;
            }
        }


        public static string LoginMobile
        {
            get
            {
                string _LoginMobile = "";

                try
                {
                    if (System.Web.HttpContext.Current.Session["LoginMobile"] != null)
                    {
                        _LoginMobile = System.Web.HttpContext.Current.Session["LoginMobile"].ToString();
                    }
                }
                catch
                { }

                return _LoginMobile;
            }
            set
            {
                System.Web.HttpContext.Current.Session["LoginMobile"] = value;
            }
        }
        public static string LoginEmployeeCode
        {
            get
            {
                string _LoginEmployeeCode = "";

                try
                {
                    if (System.Web.HttpContext.Current.Session["LoginEmployeeCode"] != null)
                    {
                        _LoginEmployeeCode = System.Web.HttpContext.Current.Session["LoginEmployeeCode"].ToString();
                    }
                }
                catch
                { }

                return _LoginEmployeeCode;
            }
            set
            {
                System.Web.HttpContext.Current.Session["LoginEmployeeCode"] = value;
            }
        }
        public static string LoginUserCodePssword
        {
            get
            {
                string _LoginUserCodePssword = "";

                try
                {
                    if (System.Web.HttpContext.Current.Session["LoginUserCodePssword"] != null)
                    {
                        _LoginUserCodePssword = System.Web.HttpContext.Current.Session["LoginUserCodePssword"].ToString();
                    }
                }
                catch
                { }

                return _LoginUserCodePssword;
            }
            set
            {
                System.Web.HttpContext.Current.Session["LoginUserCodePssword"] = value;
            }
        }
        public static string LoginSysCode
        {
            get
            {
                string _LoginSysCode = "";

                try
                {
                    if (System.Web.HttpContext.Current.Session["LoginSysCode"] != null)
                    {
                        _LoginSysCode = System.Web.HttpContext.Current.Session["LoginSysCode"].ToString();
                    }
                }
                catch
                { }

                return _LoginSysCode;
            }
            set
            {
                System.Web.HttpContext.Current.Session["LoginSysCode"] = value;
            }
        }
        public static string LoginSysName
        {
            get
            {
                string _LoginSysName = "";

                try
                {
                    if (System.Web.HttpContext.Current.Session["LoginSysName"] != null)
                    {
                        _LoginSysName = System.Web.HttpContext.Current.Session["LoginSysName"].ToString();
                    }
                }
                catch
                { }

                return _LoginSysName;
            }
            set
            {
                System.Web.HttpContext.Current.Session["LoginSysName"] = value;
            }
        }

        public static string LoginFunType
        {
            get
            {
                string _LoginFunType = "";

                try
                {
                    if (System.Web.HttpContext.Current.Session["LoginFunType"] != null)
                    {
                        _LoginFunType = System.Web.HttpContext.Current.Session["LoginFunType"].ToString();
                    }
                }
                catch
                { }

                return _LoginFunType;
            }
            set
            {
                System.Web.HttpContext.Current.Session["LoginFunType"] = value;
            }
        }
        public static string LoginSysTitle
        {
            get
            {
                string _LoginSysTitle = "";

                try
                {
                    if (System.Web.HttpContext.Current.Session["LoginSysTitle"] != null)
                    {
                        _LoginSysTitle = System.Web.HttpContext.Current.Session["LoginSysTitle"].ToString();
                    }
                }
                catch
                { }

                return _LoginSysTitle;
            }
            set
            {
                System.Web.HttpContext.Current.Session["LoginSysTitle"] = value;
            }
        }
        public static string LoginCommCode
        {
            get
            {
                string _LoginCommCode = "";

                try
                {
                    if (System.Web.HttpContext.Current.Session["LoginCommCode"] != null)
                    {
                        _LoginCommCode = System.Web.HttpContext.Current.Session["LoginCommCode"].ToString();
                    }
                }
                catch
                { }

                return _LoginCommCode;
            }
            set
            {
                System.Web.HttpContext.Current.Session["LoginCommCode"] = value;
            }
        }

        public static string NowMonthFirstDay
        {
            get
            {
                string strNowMonthFirstDay = "";

                DateTime dTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                strNowMonthFirstDay = dTime.ToString("yyyy-MM-dd");
                return strNowMonthFirstDay;
            }
        }
        public static string NowMonthLastDay
        {
            get
            {
                string strNowMonthLastDay = "";

                DateTime dTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);
                strNowMonthLastDay = dTime.ToString("yyyy-MM-dd");
                return strNowMonthLastDay;
            }
        }
        public static string DateTimeNow
        {
            get
            {
                string strDateTimeNow = "";
                DateTime dTime = DateTime.Now;
                strDateTimeNow = dTime.ToString("yyyy-MM-dd");
                return strDateTimeNow;
            }
        }
        public static string LoginCorpID
        {
            get
            {
                string _LoginCorpID = "";

                try
                {
                    if (System.Web.HttpContext.Current.Session["LoginCorpID"] != null)
                    {
                        _LoginCorpID = System.Web.HttpContext.Current.Session["LoginCorpID"].ToString();
                    }
                }
                catch
                { }

                return _LoginCorpID;
            }
            set
            {
                System.Web.HttpContext.Current.Session["LoginCorpID"] = value;
            }
        }

        public static string LoginBranchID
        {
            get
            {
                string _LoginBranchID = "";

                try
                {
                    if (System.Web.HttpContext.Current.Session["LoginBranchID"] != null)
                    {
                        _LoginBranchID = System.Web.HttpContext.Current.Session["LoginBranchID"].ToString();
                    }
                }
                catch
                { }

                return _LoginBranchID;
            }
            set
            {
                System.Web.HttpContext.Current.Session["LoginBranchID"] = value;
            }
        }

        public static string LoginCommID
        {
            get
            {
                string _LoginCommID = "";

                try
                {
                    if (System.Web.HttpContext.Current.Session["LoginCommID"] != null)
                    {
                        _LoginCommID = System.Web.HttpContext.Current.Session["LoginCommID"].ToString();
                    }
                }
                catch
                { }

                return _LoginCommID;
            }
            set
            {
                System.Web.HttpContext.Current.Session["LoginCommID"] = value;
            }
        }

        public static string LoginOrganCode
        {
            get
            {
                string _LoginOrganCode = "";

                try
                {
                    if (System.Web.HttpContext.Current.Session["LoginOrganCode"] != null)
                    {
                        _LoginOrganCode = System.Web.HttpContext.Current.Session["LoginOrganCode"].ToString();
                    }
                }
                catch
                { }

                return _LoginOrganCode;
            }
            set
            {
                System.Web.HttpContext.Current.Session["LoginOrganCode"] = value;
            }
        }

        public static string LoginCorpRegionCode
        {
            get
            {
                string _LoginCorpRegionCode = "";

                try
                {
                    if (System.Web.HttpContext.Current.Session["LoginCorpRegionCode"] != null)
                    {
                        _LoginCorpRegionCode = System.Web.HttpContext.Current.Session["LoginCorpRegionCode"].ToString();
                    }
                }
                catch
                { }

                return _LoginCorpRegionCode;
            }
            set
            {
                System.Web.HttpContext.Current.Session["LoginCorpRegionCode"] = value;
            }
        }

        public static string LoginCommType
        {
            get
            {
                string _LoginCommType = "";

                try
                {
                    if (System.Web.HttpContext.Current.Session["LoginCommType"] != null)
                    {
                        _LoginCommType = System.Web.HttpContext.Current.Session["LoginCommType"].ToString();
                    }
                }
                catch
                { }

                return _LoginCommType;
            }
            set
            {
                System.Web.HttpContext.Current.Session["LoginCommType"] = value;
            }
        }

        public static string LoginOrganName
        {
            get
            {
                string _LoginOrganName = "";

                try
                {
                    if (System.Web.HttpContext.Current.Session["LoginOrganName"] != null)
                    {
                        _LoginOrganName = System.Web.HttpContext.Current.Session["LoginOrganName"].ToString();
                    }
                }
                catch
                { }

                return _LoginOrganName;
            }
            set
            {
                System.Web.HttpContext.Current.Session["LoginOrganName"] = value;
            }
        }


        public static string LoginRegMode
        {
            get
            {
                string _LoginRegMode = "";

                try
                {
                    if (System.Web.HttpContext.Current.Session["LoginRegMode"] != null)
                    {
                        _LoginRegMode = System.Web.HttpContext.Current.Session["LoginRegMode"].ToString();
                    }
                }
                catch
                { }

                return _LoginRegMode;
            }
            set
            {
                System.Web.HttpContext.Current.Session["LoginRegMode"] = value;
            }
        }

        public static string LoginOrganCorp
        {
            get
            {
                string _LoginOrganCorp = "";

                try
                {
                    if (System.Web.HttpContext.Current.Session["LoginOrganCorp"] != null)
                    {
                        _LoginOrganCorp = System.Web.HttpContext.Current.Session["LoginOrganCorp"].ToString();
                    }
                }
                catch
                { }

                return _LoginOrganCorp;
            }
            set
            {
                System.Web.HttpContext.Current.Session["LoginOrganCorp"] = value;
            }
        }

        public static string SysVersion
        {
            get
            {
                string _SysVersion = "";

                try
                {
                    if (System.Web.HttpContext.Current.Session["SysVersion"] != null)
                    {
                        _SysVersion = System.Web.HttpContext.Current.Session["SysVersion"].ToString();
                    }
                }
                catch
                { }

                return _SysVersion;
            }
            set
            {
                System.Web.HttpContext.Current.Session["SysVersion"] = value;
            }
        }

        #endregion

        #region 个人中心

        public static string CustSynchCode
        {
            get
            {
                string _CustSynchCode = "";

                try
                {
                    if (System.Web.HttpContext.Current.Session["CustSynchCode"] != null)
                    {
                        _CustSynchCode = System.Web.HttpContext.Current.Session["CustSynchCode"].ToString();
                    }
                }
                catch
                { }

                return _CustSynchCode;
            }
            set
            {
                System.Web.HttpContext.Current.Session["CustSynchCode"] = value;
            }
        }

        public static string CommSynchCode
        {
            get
            {
                string _CommSynchCode = "";

                try
                {
                    if (System.Web.HttpContext.Current.Session["CommSynchCode"] != null)
                    {
                        _CommSynchCode = System.Web.HttpContext.Current.Session["CommSynchCode"].ToString();
                    }
                }
                catch
                { }

                return _CommSynchCode;
            }
            set
            {
                System.Web.HttpContext.Current.Session["CommSynchCode"] = value;
            }
        }

        public static string Email
        {
            get
            {
                string _Email = "";

                try
                {
                    if (System.Web.HttpContext.Current.Session["Email"] != null)
                    {
                        _Email = System.Web.HttpContext.Current.Session["Email"].ToString();
                    }
                }
                catch
                { }

                return _Email;
            }
            set
            {
                System.Web.HttpContext.Current.Session["Email"] = value;
            }
        }

        public static string CustCardSign
        {
            get
            {
                string _CustCardSign = "";

                try
                {
                    if (System.Web.HttpContext.Current.Session["CustCardSign"] != null)
                    {
                        _CustCardSign = System.Web.HttpContext.Current.Session["CustCardSign"].ToString();
                    }
                }
                catch
                { }

                return _CustCardSign;
            }
            set
            {
                System.Web.HttpContext.Current.Session["CustCardSign"] = value;
            }
        }

        public static string CustName
        {
            get
            {
                string _CustName = "";

                try
                {
                    if (System.Web.HttpContext.Current.Session["CustName"] != null)
                    {
                        _CustName = System.Web.HttpContext.Current.Session["CustName"].ToString();
                    }
                }
                catch
                { }

                return _CustName;
            }
            set
            {
                System.Web.HttpContext.Current.Session["CustName"] = value;
            }
        }


        public static string CorpSynchCode
        {
            get
            {
                string _CorpSynchCode = "";

                try
                {
                    if (System.Web.HttpContext.Current.Session["CorpSynchCode"] != null)
                    {
                        _CorpSynchCode = System.Web.HttpContext.Current.Session["CorpSynchCode"].ToString();
                    }
                }
                catch
                { }

                return _CorpSynchCode;
            }
            set
            {
                System.Web.HttpContext.Current.Session["CorpSynchCode"] = value;
            }
        }

        public static string UnCorpID
        {
            get
            {
                string _UnCorpID = "";

                try
                {
                    if (System.Web.HttpContext.Current.Session["UnCorpID"] != null)
                    {
                        _UnCorpID = System.Web.HttpContext.Current.Session["UnCorpID"].ToString();
                    }
                }
                catch
                { }

                return _UnCorpID;
            }
            set
            {
                System.Web.HttpContext.Current.Session["UnCorpID"] = value;
            }
        }

        public static string UnCustID
        {
            get
            {
                string _UnCustID = "";

                try
                {
                    if (System.Web.HttpContext.Current.Session["UnCustID"] != null)
                    {
                        _UnCustID = System.Web.HttpContext.Current.Session["UnCustID"].ToString();
                    }
                }
                catch
                { }
                return _UnCustID;
            }
            set
            {
                System.Web.HttpContext.Current.Session["UnCustID"] = value;
            }
        }

        public static string UserType
        {
            get
            {
                string _UserType = "";

                try
                {
                    if (System.Web.HttpContext.Current.Session["UserType"] != null)
                    {
                        _UserType = System.Web.HttpContext.Current.Session["UserType"].ToString();
                    }
                }
                catch
                { }
                return _UserType;
            }
            set
            {
                System.Web.HttpContext.Current.Session["UserType"] = value;
            }
        }

        public static string ServerIP
        {
            get
            {
                string _ServerIP = "";

                try
                {
                    if (System.Web.HttpContext.Current.Session["ServerIP"] != null)
                    {
                        _ServerIP = System.Web.HttpContext.Current.Session["ServerIP"].ToString();
                    }
                }
                catch
                { }
                return _ServerIP;
            }
            set
            {
                System.Web.HttpContext.Current.Session["ServerIP"] = value;
            }
        }

        public static string SysDir
        {
            get
            {
                string _SysDir = "";

                try
                {
                    if (System.Web.HttpContext.Current.Session["SysDir"] != null)
                    {
                        _SysDir = System.Web.HttpContext.Current.Session["SysDir"].ToString();
                    }
                }
                catch
                { }
                return _SysDir;
            }
            set
            {
                System.Web.HttpContext.Current.Session["SysDir"] = value;
            }
        }

        public static string EmailSign
        {
            get
            {
                string _EmailSign = "";

                try
                {
                    if (System.Web.HttpContext.Current.Session["EmailSign"] != null)
                    {
                        _EmailSign = System.Web.HttpContext.Current.Session["EmailSign"].ToString();
                    }
                }
                catch
                { }
                return _EmailSign;
            }
            set
            {
                System.Web.HttpContext.Current.Session["EmailSign"] = value;
            }
        }

        public static string NickName
        {
            get
            {
                string _NickName = "";

                try
                {
                    if (System.Web.HttpContext.Current.Session["NickName"] != null)
                    {
                        _NickName = System.Web.HttpContext.Current.Session["NickName"].ToString();
                    }
                }
                catch
                { }
                return _NickName;
            }
            set
            {
                System.Web.HttpContext.Current.Session["NickName"] = value;
            }
        }

        public static string HeadImgURL
        {
            get
            {
                string _HeadImgURL = "";

                try
                {
                    if (System.Web.HttpContext.Current.Session["HeadImgURL"] != null)
                    {
                        _HeadImgURL = System.Web.HttpContext.Current.Session["HeadImgURL"].ToString();
                    }
                }
                catch
                { }
                return _HeadImgURL;
            }
            set
            {
                System.Web.HttpContext.Current.Session["HeadImgURL"] = value;
            }
        }

        public static string CorpName
        {
            get
            {
                string _CorpName = "";

                try
                {
                    if (System.Web.HttpContext.Current.Session["CorpName"] != null)
                    {
                        _CorpName = System.Web.HttpContext.Current.Session["CorpName"].ToString();
                    }
                }
                catch
                { }
                return _CorpName;
            }
            set
            {
                System.Web.HttpContext.Current.Session["CorpName"] = value;
            }
        }

        public static string LoginState
        {
            get
            {
                string _LoginState = "";

                try
                {
                    if (System.Web.HttpContext.Current.Session["LoginState"] != null)
                    {
                        _LoginState = System.Web.HttpContext.Current.Session["LoginState"].ToString();
                    }
                }
                catch
                { }
                return _LoginState;
            }
            set
            {
                System.Web.HttpContext.Current.Session["LoginState"] = value;
            }
        }

        public static string CustContact
        {
            get
            {
                string _CustContact = "";

                try
                {
                    if (System.Web.HttpContext.Current.Session["CustContact"] != null)
                    {
                        _CustContact = System.Web.HttpContext.Current.Session["CustContact"].ToString();
                    }
                }
                catch
                { }
                return _CustContact;
            }
            set
            {
                System.Web.HttpContext.Current.Session["CustContact"] = value;
            }
        }

        public static string CorpID
        {
            get
            {
                string _CorpID = "";

                try
                {
                    if (System.Web.HttpContext.Current.Session["CorpID"] != null)
                    {
                        _CorpID = System.Web.HttpContext.Current.Session["CorpID"].ToString();
                    }
                }
                catch
                { }
                return _CorpID;
            }
            set
            {
                System.Web.HttpContext.Current.Session["CorpID"] = value;
            }
        }

        public static string WgTel
        {
            get
            {
                string _WgTel = "";

                try
                {
                    if (System.Web.HttpContext.Current.Session["WgTel"] != null)
                    {
                        _WgTel = System.Web.HttpContext.Current.Session["WgTel"].ToString();
                    }
                }
                catch
                { }
                return _WgTel;
            }
            set
            {
                System.Web.HttpContext.Current.Session["WgTel"] = value;
            }
        }

        public static string CurrentModule
        {
            get
            {
                string _CurrentModule = "";

                try
                {
                    if (System.Web.HttpContext.Current.Session["CurrentModule"] != null)
                    {
                        _CurrentModule = System.Web.HttpContext.Current.Session["CurrentModule"].ToString();
                    }
                }
                catch
                { }
                return _CurrentModule;
            }
            set
            {
                System.Web.HttpContext.Current.Session["CurrentModule"] = value;
            }
        }
        /// <summary>
        /// 门禁控制器mac地址
        /// </summary>
        public static string CommDeviceAddRess
        {
            get
            {
                string _CommDeviceAddRess = "";

                try
                {
                    if (System.Web.HttpContext.Current.Session["CommDeviceAddRess"] != null)
                    {
                        _CommDeviceAddRess = System.Web.HttpContext.Current.Session["CommDeviceAddRess"].ToString();
                    }
                }
                catch
                { }
                return _CommDeviceAddRess;
            }
            set
            {
                System.Web.HttpContext.Current.Session["CommDeviceAddRess"] = value;
            }
        }

        /// <summary>
        /// 门禁控制器wifi地址
        /// </summary>
        public static string CommDeviceAddRessWifi
        {
            get
            {
                string _CommDeviceAddRessWifi = "";

                try
                {
                    if (System.Web.HttpContext.Current.Session["CommDeviceAddRessWifi"] != null)
                    {
                        _CommDeviceAddRessWifi = System.Web.HttpContext.Current.Session["CommDeviceAddRessWifi"].ToString();
                    }
                }
                catch
                { }
                return _CommDeviceAddRessWifi;
            }
            set
            {
                System.Web.HttpContext.Current.Session["CommDeviceAddRessWifi"] = value;
            }
        }

        /// <summary>
        /// 门禁控制器名称
        /// </summary>
        public static string CommDeviceName
        {
            get
            {
                string _CommDeviceName = "";

                try
                {
                    if (System.Web.HttpContext.Current.Session["CommDeviceName"] != null)
                    {
                        _CommDeviceName = System.Web.HttpContext.Current.Session["CommDeviceName"].ToString();
                    }
                }
                catch
                { }
                return _CommDeviceName;
            }
            set
            {
                System.Web.HttpContext.Current.Session["CommDeviceName"] = value;
            }
        }

        #endregion

        #region 手机APP上传图片文件目录
        public static string UpLoadImgUrl
        {
            get
            {
                string _UpLoadImgUrl = "";

                try
                {
                    if (System.Web.HttpContext.Current.Session["UpLoadImgUrl"] != null)
                    {
                        _UpLoadImgUrl = System.Web.HttpContext.Current.Session["UpLoadImgUrl"].ToString();
                    }
                }
                catch
                { }
                return _UpLoadImgUrl;
            }
            set
            {
                System.Web.HttpContext.Current.Session["UpLoadImgUrl"] = value;
            }
        }
        #endregion



        #region 推荐商家浏览目录
        public static string RecommendUpLoadUrl
        {
            get
            {
                string _RecommendUpLoadUrl = "";

                try
                {
                    if (System.Web.HttpContext.Current.Session["RecommendUpLoadUrl"] != null)
                    {
                        _RecommendUpLoadUrl = System.Web.HttpContext.Current.Session["RecommendUpLoadUrl"].ToString();
                    }
                    else
                    {
                        _RecommendUpLoadUrl = WebConfigurationManager.AppSettings["RecommendUpLoadUrl"].ToString();
                        System.Web.HttpContext.Current.Session["RecommendUpLoadUrl"] = _RecommendUpLoadUrl;
                    }
                }
                catch
                { }
                return _RecommendUpLoadUrl;
            }
        }
        #endregion

        #region 业主浏览目录
        public static string CustFileAddr
        {
            get
            {
                string _CustFileAddr = "";

                try
                {
                    if (System.Web.HttpContext.Current.Session["CustFileAddr"] != null)
                    {
                        _CustFileAddr = System.Web.HttpContext.Current.Session["CustFileAddr"].ToString();
                    }
                    else
                    {
                        _CustFileAddr = WebConfigurationManager.AppSettings["CustFileAddr"].ToString().Replace("[Server]", Global_Var.ServerIP.ToString());
                        System.Web.HttpContext.Current.Session["CustFileAddr"] = _CustFileAddr;
                    }
                }
                catch
                { }

                return _CustFileAddr;
            }
            set
            {
                System.Web.HttpContext.Current.Session["CustFileAddr"] = value;
            }
        }
        #endregion

        #region 商家浏览目录
        public static string BussUpLoadUrl
        {
            get
            {
                string _BussUpLoadUrl = "";

                try
                {
                    if (System.Web.HttpContext.Current.Session["BussUpLoadUrl"] != null)
                    {
                        _BussUpLoadUrl = System.Web.HttpContext.Current.Session["BussUpLoadUrl"].ToString();
                    }
                    else
                    {
                        _BussUpLoadUrl = WebConfigurationManager.AppSettings["BussUpLoadUrl"].ToString();
                        System.Web.HttpContext.Current.Session["BussUpLoadUrl"] = _BussUpLoadUrl;
                    }
                }
                catch
                { }

                return _BussUpLoadUrl;
            }
        }
        #endregion

        #region 商家kineditor图片目录
        public static string UpLoadBussImgURL
        {
            get
            {
                string _UpLoadBussImgURL = "";

                try
                {
                    if (System.Web.HttpContext.Current.Session["UpLoadBussImgURL"] != null)
                    {
                        _UpLoadBussImgURL = System.Web.HttpContext.Current.Session["UpLoadBussImgURL"].ToString();
                    }
                    else
                    {
                        _UpLoadBussImgURL = WebConfigurationManager.AppSettings["UpLoadBussImgURL"].ToString();
                        System.Web.HttpContext.Current.Session["UpLoadBussImgURL"] = _UpLoadBussImgURL;
                    }
                }
                catch
                { }
                return _UpLoadBussImgURL;
            }
        }
        #endregion

        #region 业主实体表
        public static Model.Unify.Tb_Unify_Customer Customer
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["Tb_Unify_Customer"] != null)
                {
                    Model.Unify.Tb_Unify_Customer _Customer = (Model.Unify.Tb_Unify_Customer)System.Web.HttpContext.Current.Session["Tb_Unify_Customer"];

                    return _Customer;
                }
                else
                {
                    Model.Unify.Tb_Unify_Customer _Customer = new Model.Unify.Tb_Unify_Customer();
                    return _Customer;
                }
            }
            set
            {
                System.Web.HttpContext.Current.Session["Tb_Unify_Customer"] = value;
            }
        }
        #endregion



    }
}
