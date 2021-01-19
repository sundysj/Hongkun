using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using MobileSoft.Common;
using System.Web.Configuration;
using MobileSoft.DBUtility;
using MobileSoft.BLL.System;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Text;
using System.IO;

/// <summary>
///AppGlobal 的摘要说明
/// </summary>

namespace MobileSoft.Common
{
    public class AppGlobal
    {
        public static string mTailTitle = "综合管理服务平台";
        public static string mCommTailTitle = " 综合管理服务平台";

        #region 数据字典
        //*数据字典
        private static string[] aDictionarySort = {"BuildType","BuildUses","PropertyRights","PropertyUses" ,"RoomModel"
													   ,"RentalType","FitmenState","RentPeriod","SeeRoomDate","InfoSource"
													   ,"PaperName" ,"TranNature","IsTran","TranSort","Nationality"
													   
													   ,"Job","Interests","RectUnitSort","ChargeMode","PaymentMethod"
													   ,"RepType","BankName","Relation","ServiceQuality","ArticlesFacilities"
													   ,"RectificationState","Engineering","Orderly","ProductUnit","Qualification"

													   ,"MaritalStatus","Nation","Politics","WorkFormat","PactType"
													   ,"DimissionReason","OprState","NoticeMethod","NodeOprMethod","NodeOprType"
													   ,"InstanceType","FileSecret","Nervous","AskForLeaveNature","ContactImportant"
													   ,"ContactNervous","OprLevel","MandarinLevel","ForeignLang","PracticeQualify"
													   ,"InsuranceSpType","InsuranceStruct","DimissionType","BabyCheckResult","JobRoleType"
													   ,"RegisteredType","WorkPlanImportant","WorkPlanType","WorkPlanResult","DeviceIdentification"
													   ,"UnitMeasurement","RepairLevel","FaultType","UserFilesRecordType","PreferentialPolicy"
													   ,"PersonClassRoleLevel","ReimbursementType","PrecType","Purpose","RecruitMethod","PactUseFulDate","CertificateName"
													   ,"Suigenderism","Lodge","Visitcontent","AssetUnit","ReplyWay","CleaningResult","GreenResult","RemindersType"};
        private static string[] aDictionaryName = {"楼宇类型","楼宇用途","产权性质","使用性质","房屋户型"
													   ,"租售类别","装修情况","租金周期","看房时间","信息来源"
													   ,"证件名称" ,"交接资料性质","交接资料情况","交接类型","国籍"

													   ,"职业","兴趣爱好","整改单位类别","收款方式","交款方式"
													   ,"回访类别","银行名称","与户主关系","服务质量评价","物品设施评价"
													   ,"整改状态","移交工程部资料","移交秩序维护部资料","物资单位","学历"

													   ,"婚姻状况","民族","政治面貌","用工形式","人事合同类别"
													   ,"离职原因","流程节点处理状态","流程节点通知方式","流程节点处理方式","流程节点处理类型"
													   ,"流程节点实例类型","文件密级","缓急程度","请假性质","重要程度"
													   ,"紧急程度","管理级别","普通话水平","外语语种","执业资质"
													   ,"社保分类险种" ,"员工类别","离职类型","体检结果","岗位类别"
													   ,"户口性质","计划重要程度","计划分类","执行结果","设备标识"
													   ,"计量单位","维养级别","故障类别","奖惩类别","优惠政策"
													   ,"岗位级别","报账类别","预交类型","用途区域","应聘方式","合同有效期限","证书名称"
													   ,"党员发展状态","党员所属支部","回访内容","资产单位","回访方式","保洁常用短语","绿化常用短语","催办类别"};
        #endregion

        //获得业务引擎数据库连接字符串
        public static void GetHmWyglConnection()
        {
            Bll_Tb_System_Corp Bll = new Bll_Tb_System_Corp();

            DataTable dTable = Bll.GetList("CorpId='" + Global_Var.CorpId.ToString() + "' AND ISNULL(IsDelete,0)=0").Tables[0];

            if (dTable.Rows.Count > 0)
            {
                DataRow DRow = dTable.Rows[0];
                Global_Var.LoginCorpID = DRow["CorpID"].ToString();
                Global_Var.CorpName = DRow["CorpName"].ToString();
                Global_Var.LoginRegMode = DRow["RegMode"].ToString();
                Global_Var.SysVersion = DRow["SysVersion"].ToString();
                Global_Var.ERPDatabaseName = DRow["DBName"].ToString();
                PubConstant.hmWyglConnectionString = Global_Fun.BuildLoginConnectSql(DRow["DBServer"].ToString(), DRow["DBName"].ToString(), DRow["DBUser"].ToString(), DRow["DBPwd"].ToString());
            }
        }

        #region 分时问候语
        /// <summary>
        /// 分时问候语
        /// </summary>
        /// <param name="dTime"></param>
        /// <returns></returns>
        public static string getRegard(DateTime dTime)
        {
            string strRegard = "您好！";
            int iHour = dTime.Hour;
            if (iHour < 6) { strRegard = "凌晨好！"; }
            else if (iHour < 9) { strRegard = "早上好！"; }
            else if (iHour < 12) { strRegard = "上午好！"; }
            else if (iHour < 14) { strRegard = "中午好！"; }
            else if (iHour < 17) { strRegard = "下午好！"; }
            else if (iHour < 19) { strRegard = "傍晚好！"; }
            else if (iHour < 22) { strRegard = "晚上好！"; }
            else { strRegard = "夜里好！"; }

            return strRegard;
        }
        #endregion

        #region 得到系统版本号
        public static string GetSysVersion(int CorpID)
        {
            string strVersion = "";

            MobileSoft.BLL.System.Bll_Tb_System_Corp A = new MobileSoft.BLL.System.Bll_Tb_System_Corp();
            DataTable dTable = A.System_Corp_GetVersion(CorpID);
            if (dTable.Rows.Count > 0)
            {
                DataRow DRow = dTable.Rows[0];

                strVersion = DRow["SysVersion"].ToString();

            }
            dTable.Dispose();

            if (strVersion == "")
            {
                strVersion = "standard";
            }

            return strVersion;
        }
        #endregion

        #region 字符转化成整型
        /// <summary>
        /// 字符转化成整型
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int StrToInt(string str)
        {
            int i = 0;
            try
            {
                if (str != "")
                {
                    i = Convert.ToInt32(str);
                }
            }
            catch
            {

            }
            return i;
        }
        #endregion

        #region 字符转化成长整型
        /// <summary>
        /// 字符转化成长整型
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static long StrToLong(string str)
        {
            long i = 0;
            try
            {
                if (str != "")
                {
                    i = Convert.ToInt64(str);
                }
            }
            catch
            {

            }
            return i;
        }
        #endregion

        #region 字符转化成数字
        /// <summary>
        /// 字符转化成数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Decimal StrToDec(string str)
        {
            Decimal i = 0;
            try
            {
                if (str != "")
                {
                    i = Convert.ToDecimal(str);
                }
            }
            catch
            {

            }
            return i;
        }
        #endregion

        #region 字符转化成百分数%
        /// <summary>
        /// 字符转化成百分数%
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Decimal StrToPercent(string str)
        {
            Decimal i = 0;
            try
            {
                if (str != "")
                {
                    i = Convert.ToDecimal(str) * Convert.ToDecimal(100.0);
                }
            }
            catch
            {

            }
            return i;
        }
        #endregion

        #region 将通配符作为文字使用
        /// <summary>
        /// 将通配符作为文字使用
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToWildcard(string str)
        {
            str = str.Replace("[", "[[]");
            str = str.Replace("%", "[%]");
            str = str.Replace("_", "[_]");
            str = str.Replace("'", "''");

            return str;
        }

        #endregion

        #region 日期字符转化成yyyy-MM-dd
        public static string StrDateToDate(string str)
        {
            string tmpdate = "";
            try
            {
                if (str != "")
                {
                    tmpdate = Convert.ToDateTime(str).ToString("yyyy-MM-dd");
                }
            }
            catch
            {
            }
            return tmpdate;
        }
        #endregion

        #region 日期字符转化成yyyy-MM-dd HH:mm:ss
        public static string StrDateToDateTime(string str)
        {
            string tmpdate = "";
            try
            {
                if (str != "")
                {
                    tmpdate = Convert.ToDateTime(str).ToString("yyyy-MM-dd HH:mm:ss");
                }
            }
            catch
            {
            }
            return tmpdate;
        }
        #endregion

        #region 日期字符转化成yyyyMM
        public static string StrDateToYearMonth(string str)
        {
            string tmpdate = "";
            try
            {
                if (str != "")
                {
                    tmpdate = Convert.ToDateTime(str).ToString("yyyyMM");
                }
            }
            catch
            {
            }
            return tmpdate;
        }
        #endregion

        #region 字符串转化为日期
        /// <summary>
        /// 字符串转化为日期
        /// </summary>
        /// <param name="str"></param>
        /// <returns>DateTime</returns>
        public static DateTime StrToDate(string str)
        {
            DateTime tmpdate = new DateTime(1900, 1, 1);
            try
            {
                if (str != "")
                {
                    tmpdate = Convert.ToDateTime(str);
                }
            }
            catch
            {
            }
            return tmpdate;
        }
        #endregion

        #region 转换并检查日期
        /// <summary>
        /// 转换并检查日期
        /// </summary>
        /// <param name="strYear">年</param>
        /// <param name="strMonth">月</param>
        /// <param name="strDay">日</param>
        /// <returns>yyyy-MM-dd型日期</returns>
        public static string CheckDate(string strYear, string strMonth, string strDay)
        {
            string NDate = "";
            if ((strYear != "") && (strMonth != "") && (strDay != ""))
            {
                int MaxDay = 31;
                int iYear = 1900;
                int iMonth = 1;
                int iDay = 1;

                try
                {
                    iYear = Convert.ToInt32(strYear);
                }
                catch
                {
                }
                try
                {
                    iMonth = Convert.ToInt32(strMonth);
                }
                catch
                {
                }
                try
                {
                    iDay = Convert.ToInt32(strDay);
                }
                catch
                {
                }


                if (iMonth == 2)
                {
                    if (DateTime.IsLeapYear(iYear))
                    {
                        MaxDay = 29;
                    }
                    else
                    {
                        MaxDay = 28;
                    }
                }
                if ((iMonth == 1) || (iMonth == 3) || (iMonth == 5) || (iMonth == 7) || (iMonth == 8) || (iMonth == 10) || (iMonth == 12))
                {
                    MaxDay = 31;
                }
                if ((iMonth == 4) || (iMonth == 6) || (iMonth == 9) || (iMonth == 11))
                {
                    MaxDay = 30;
                }

                if (iDay > MaxDay)
                {
                    iDay = MaxDay;
                }

                DateTime CkDate = new DateTime(iYear, iMonth, iDay);
                NDate = CkDate.ToString("yyyy-MM-dd");
            }

            return NDate;
        }

        /// <summary>
        /// 转换并检查日期
        /// </summary>
        /// <param name="strYear">年</param>
        /// <param name="strMonth">月</param>
        /// <param name="strDay">日</param>
        /// <returns>日期DateTime</returns>
        public static DateTime CheckDate(int iYear, int iMonth, int iDay)
        {
            DateTime CkDate = new DateTime(1900, 1, 1);
            if ((iYear != 0) && (iMonth != 0) && (iDay != 0))
            {
                int MaxDay = 31;

                if (iMonth == 2)
                {
                    if (DateTime.IsLeapYear(iYear))
                    {
                        MaxDay = 29;
                    }
                    else
                    {
                        MaxDay = 28;
                    }
                }
                if ((iMonth == 1) || (iMonth == 3) || (iMonth == 5) || (iMonth == 7) || (iMonth == 8) || (iMonth == 10) || (iMonth == 12))
                {
                    MaxDay = 31;
                }
                if ((iMonth == 4) || (iMonth == 6) || (iMonth == 9) || (iMonth == 11))
                {
                    MaxDay = 30;
                }

                if (iDay > MaxDay)
                {
                    iDay = MaxDay;
                }

                CkDate = new DateTime(iYear, iMonth, iDay);
            }

            return CkDate;
        }
        #endregion

        #region 转换并检查日期时间
        /// <summary>
        /// 转换并检查日期时间
        /// </summary>
        /// <param name="strYear"></param>
        /// <param name="strMonth"></param>
        /// <param name="strDay"></param>
        /// <param name="strHour"></param>
        /// <param name="strMinute"></param>
        /// <param name="strSecond"></param>
        /// <returns></returns>
        public static string CheckDateTime(string strYear, string strMonth, string strDay, string strHour, string strMinute, string strSecond)
        {
            string NDate = "";
            if ((strYear != "") && (strMonth != "") && (strDay != ""))
            {
                int MaxDay = 31;
                int iYear = 1900;
                int iMonth = 1;
                int iDay = 1;
                int iHour = 0;
                int iMinute = 0;
                int iSecond = 0;

                try
                {
                    iYear = Convert.ToInt32(strYear);
                }
                catch
                {
                }
                try
                {
                    iMonth = Convert.ToInt32(strMonth);
                }
                catch
                {
                }
                try
                {
                    iDay = Convert.ToInt32(strDay);
                }
                catch
                {
                }
                try
                {
                    iHour = Convert.ToInt32(strHour);
                }
                catch
                {
                }
                try
                {
                    iMinute = Convert.ToInt32(strMinute);
                }
                catch
                {
                }
                try
                {
                    iSecond = Convert.ToInt32(strSecond);
                }
                catch
                {
                }


                if (iMonth == 2)
                {
                    if (DateTime.IsLeapYear(iYear))
                    {
                        MaxDay = 29;
                    }
                    else
                    {
                        MaxDay = 28;
                    }
                }
                if ((iMonth == 1) || (iMonth == 3) || (iMonth == 5) || (iMonth == 7) || (iMonth == 8) || (iMonth == 10) || (iMonth == 12))
                {
                    MaxDay = 31;
                }
                if ((iMonth == 4) || (iMonth == 6) || (iMonth == 9) || (iMonth == 11))
                {
                    MaxDay = 30;
                }

                if (iDay > MaxDay)
                {
                    iDay = MaxDay;
                }

                try
                {
                    DateTime CkDate = new DateTime(iYear, iMonth, iDay, iHour, iMinute, iSecond);
                    NDate = CkDate.ToString("yyyy-MM-dd HH:mm:ss");
                }
                catch
                {
                }
            }

            return NDate;
        }

        /// <summary>
        /// 转换并检查日期时间
        /// </summary>
        /// <param name="strYear"></param>
        /// <param name="strMonth"></param>
        /// <param name="strDay"></param>
        /// <param name="strHour"></param>
        /// <param name="strMinute"></param>
        /// <param name="strSecond"></param>
        /// <returns></returns>
        public static DateTime CheckDateTime(int iYear, int iMonth, int iDay, int iHour, int iMinute, int iSecond)
        {


            int MaxDay = 31;

            if (iYear < 0)
            {
                iYear = 1900;
            }
            if (iMonth <= 0)
            {
                iMonth = 1;
            }
            if (iDay <= 0)
            {
                iDay = 1;
            }
            if (iHour < 0)
            {
                iHour = 0;
            }
            if (iMinute < 0)
            {
                iMinute = 0;
            }
            if (iSecond < 0)
            {
                iSecond = 0;
            }

            if (iMonth == 2)
            {
                if (DateTime.IsLeapYear(iYear))
                {
                    MaxDay = 29;
                }
                else
                {
                    MaxDay = 28;
                }
            }
            if ((iMonth == 1) || (iMonth == 3) || (iMonth == 5) || (iMonth == 7) || (iMonth == 8) || (iMonth == 10) || (iMonth == 12))
            {
                MaxDay = 31;
            }
            if ((iMonth == 4) || (iMonth == 6) || (iMonth == 9) || (iMonth == 11))
            {
                MaxDay = 30;
            }

            if (iDay > MaxDay)
            {
                iDay = MaxDay;
            }

            DateTime CkDate = new DateTime(iYear, iMonth, iDay, iHour, iMinute, iSecond);

            return CkDate;
        }

        /// <summary>
        /// 转换并检查日期时间
        /// </summary>
        /// <param name="strYear"></param>
        /// <param name="strMonth"></param>
        /// <param name="strDay"></param>
        /// <param name="strHour"></param>
        /// <param name="strMinute"></param>
        /// <param name="strSecond"></param>
        /// <returns></returns>
        public static DateTime CheckDateTime(int iYear, int iMonth, int iDay, int iHour, int iMinute, int iSecond, int iMillisecond)
        {


            int MaxDay = 31;

            if (iYear < 0)
            {
                iYear = 1900;
            }
            if (iMonth <= 0)
            {
                iMonth = 1;
            }
            if (iDay <= 0)
            {
                iDay = 1;
            }
            if (iHour < 0)
            {
                iHour = 0;
            }
            if (iMinute < 0)
            {
                iMinute = 0;
            }
            if (iSecond < 0)
            {
                iSecond = 0;
            }

            if (iMonth == 2)
            {
                if (DateTime.IsLeapYear(iYear))
                {
                    MaxDay = 29;
                }
                else
                {
                    MaxDay = 28;
                }
            }
            if ((iMonth == 1) || (iMonth == 3) || (iMonth == 5) || (iMonth == 7) || (iMonth == 8) || (iMonth == 10) || (iMonth == 12))
            {
                MaxDay = 31;
            }
            if ((iMonth == 4) || (iMonth == 6) || (iMonth == 9) || (iMonth == 11))
            {
                MaxDay = 30;
            }

            if (iDay > MaxDay)
            {
                iDay = MaxDay;
            }

            DateTime CkDate = new DateTime(iYear, iMonth, iDay, iHour, iMinute, iSecond, iMillisecond);

            return CkDate;
        }
        #endregion

        #region 日期比较，结果天数
        public static int CompDateDay(DateTime d1, DateTime d2)
        {
            TimeSpan ts = d2 - d1;
            return ts.Days;

        }

        public static int CompDateDay(string Date1, string Date2)
        {
            DateTime d1 = new DateTime(1900, 1, 1);
            DateTime d2 = new DateTime(1900, 1, 1);

            try
            {
                if (Date1 != "")
                {
                    d1 = Convert.ToDateTime(Date1);
                }
            }
            catch
            {
            }

            try
            {
                if (Date2 != "")
                {
                    d2 = Convert.ToDateTime(Date2);
                }
            }
            catch
            {
            }

            TimeSpan ts = d2 - d1;

            return ts.Days;

        }
        #endregion

        #region 日期比较，结果月份
        public static int CompDateMonth(DateTime d1, DateTime d2)
        {

            return (d2.Year - d1.Year) * 12 + (d2.Month - d1.Month);
        }

        public static int CompDateMonth(string Date1, string Date2)
        {
            int DateMonth = 0;
            DateTime d1 = new DateTime(1900, 1, 1);
            DateTime d2 = new DateTime(1900, 1, 1);

            try
            {
                if (Date1 != "")
                {
                    d1 = Convert.ToDateTime(Date1);
                }
            }
            catch
            {
            }

            try
            {
                if (Date2 != "")
                {
                    d2 = Convert.ToDateTime(Date2);
                }
            }
            catch
            {

            }

            DateMonth = (d2.Year - d1.Year) * 12 + (d2.Month - d1.Month);

            DateTime d11 = new DateTime(d1.Year, d1.Month, 1);

            DateTime d121 = new DateTime(d2.Year, d2.Month, 1);
            DateTime d12 = d121.AddMonths(1).AddDays(-1);

            if (d1 == d11 && d2 == d12)
            {
                DateMonth = DateMonth + 1;
            }

            return DateMonth;

        }
        #endregion

        #region 得到日期的最大天数
        public static int GetMaxDay(DateTime Date)
        {
            int MaxDay = 30;
            int iYear = Date.Year;
            int iMonth = Date.Month;

            if (iMonth == 2)
            {
                if (DateTime.IsLeapYear(iYear))
                {
                    MaxDay = 29;
                }
                else
                {
                    MaxDay = 28;
                }
            }
            if ((iMonth == 1) || (iMonth == 3) || (iMonth == 5) || (iMonth == 7) || (iMonth == 8) || (iMonth == 10) || (iMonth == 12))
            {
                MaxDay = 31;
            }
            if ((iMonth == 4) || (iMonth == 6) || (iMonth == 9) || (iMonth == 11))
            {
                MaxDay = 30;
            }

            return MaxDay;
        }
        #endregion

        #region 字符转化成未含特殊字符的字符
        /// <summary>
        /// 字符转化成未含特殊字符的字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string StrToStr(string str)
        {
            str = str.Replace("#", "＃");
            str = str.Replace("%", "％");
            str = str.Replace("'", "‘");
            return str;
        }
        #endregion

        #region 实现数据的四舍五入法
        /// <summary>
        /// 实现数据的四舍五入法（两位小数）
        /// </summary>    
        /// <param name="val">要进行处理的数据</param> 		
        ///  <returns>四舍五入后的结果</returns> 
        public static decimal Round(decimal val)
        {
            return Round(val, 2);
        }
        /// <summary>
        /// 实现数据的四舍五入法
        /// </summary>
        /// <param name="d">要进行处理的数据</param>
        /// <param name="i">保留的小数位数</param>
        /// <returns>四舍五入后的结果</returns>
        public static decimal Round(decimal val, int i)
        {
            double d = Convert.ToDouble(val);

            if (d == 0)
            {
                d = 0;
            }
            else
            {
                if (d >= 0)
                {
                    d += 5 * Math.Pow(10, -(i + 1));
                }
                else
                {
                    d += -5 * Math.Pow(10, -(i + 1));
                }
            }

            System.Globalization.NumberFormatInfo provider = new System.Globalization.NumberFormatInfo();
            provider.NumberDecimalDigits = (i + 1);

            string str = d.ToString("N", provider);
            string[] strs = str.Split('.');
            int idot = str.IndexOf('.');
            string prestr = strs[0];
            string poststr = "0";
            if (strs.Length > 1)
            {
                poststr = strs[1];
            }
            if (poststr.Length > i)
            {
                poststr = str.Substring(idot + 1, i);
            }
            string strd = prestr + "." + poststr;

            val = decimal.Parse(strd);

            return val;
        }
        #endregion

        #region 字符串是否是数字的判断
        /// <summary>
        /// 字符串是否是数字的判断
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsNumber(string s)
        {

            string ex = @"^[0-9]*$";//@"^\d*$";
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(ex, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            bool flag = reg.IsMatch(s);
            return flag;
        }
        #endregion


        #region 过滤数字ID
        /// <summary>
        /// 过滤数字ID
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ChkNum(string s)
        {

            string Return = "";
            if (IsNumber(s) == true)
            {
                Return = s;
            }
            else
            {
                string tmpStr = s;
                tmpStr = tmpStr.Replace("  ", " ");
                int iPos = tmpStr.IndexOf(";");
                if (iPos > 0)
                {
                    tmpStr = tmpStr.Substring(0, iPos);
                }

                iPos = tmpStr.IndexOf(" ");
                if (iPos > 0)
                {
                    tmpStr = tmpStr.Substring(0, iPos);
                }

                tmpStr = tmpStr.Replace("'", "''");

                Return = tmpStr;
            }

            return Return;
        }
        #endregion

        #region 过滤字符串
        /// <summary>
        /// 过滤字符串
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ChkStr(string s)
        {
            string tmpStr = s;
            tmpStr = tmpStr.Replace("'", "''");
            return tmpStr;
        }
        #endregion

        #region 获取小区名字
        public static string GetCommName(int iCommID)
        {
            string strCommName = "";

            MobileSoft.BLL.HSPR.Bll_Tb_HSPR_Community Bll = new MobileSoft.BLL.HSPR.Bll_Tb_HSPR_Community();

            DataTable dTable = Bll.GetList(" CommID=" + iCommID.ToString()).Tables[0];

            if (dTable.Rows.Count > 0)
            {
                DataRow DRow = dTable.Rows[0];

                strCommName = DRow["CommName"].ToString();

            }
            dTable.Dispose();

            return strCommName;
        }
        #endregion

        #region 获得地区名称
        //*获得地区名称
        /// <summary>
        /// 获得地区名称
        /// </summary>
        /// <param name="iCommID"></param>
        /// <returns></returns>
        public static string GetOrganName4(string strOrganCode)
        {
            string strOrganName = "";
            if (strOrganCode != "")
            {
                MobileSoft.BLL.Sys.Bll_Tb_Sys_Organ Bll = new MobileSoft.BLL.Sys.Bll_Tb_Sys_Organ();

                DataTable dTable = Bll.GetList(" OrganCode='" + strOrganCode + "'").Tables[0];

                if (dTable.Rows.Count > 0)
                {
                    DataRow DRow = dTable.Rows[0];

                    strOrganName = DRow["OrganName"].ToString();

                }
                dTable.Dispose();
            }
            return strOrganName;
        }

        #endregion

        #region 根据主键获取相应的KEY值
        public static string GetKeyValue(string KeyFileldName, string KeyFileldValue, string TableName, string FiledName)
        {
            string Ret = "";

            SqlParameter[] parameters = {
					new SqlParameter("@KeyFileldName", SqlDbType.VarChar,50),
                              new SqlParameter("@KeyFileldValue", SqlDbType.VarChar,50),
                              new SqlParameter("@TableName", SqlDbType.VarChar,50),
                              new SqlParameter("@FiledName", SqlDbType.VarChar,50)
                                              };
            parameters[0].Value = KeyFileldName;
            parameters[1].Value = KeyFileldValue;
            parameters[2].Value = TableName;
            parameters[3].Value = FiledName;

            DataTable dTable = DbHelperSQL.RunProcedure("Proc_System_GetKeyValue", parameters, "RetDataSet").Tables[0];

            if (dTable.Rows.Count > 0)
            {
                Ret = dTable.Rows[0][0].ToString();
            }

            return Ret;
        }
        #endregion

        #region 报事类型

        /// <summary>
        /// 报事类型DropDownList
        /// </summary>
        /// <param name="cboFill"></param>
        /// <param name="IsNull"></param>
        public static void FillIncidentClass(ref DropDownList cboFill, bool IsNull)
        {
            cboFill.Items.Clear();

            if (IsNull)
            {
                System.Web.UI.WebControls.ListItem lItem = new System.Web.UI.WebControls.ListItem("", "");
                cboFill.Items.Add(lItem);
            }
            string sText, sData;
            //1书面派工、2口头派工、3返修处理、4投诉处理
            sData = TypeRule.TWIncidentClass.Paper.ToString();
            sText = TypeRule.TWIncidentClass.PaperName;
            System.Web.UI.WebControls.ListItem lItem1 = new System.Web.UI.WebControls.ListItem(sText, sData);
            cboFill.Items.Add(lItem1);

            sData = TypeRule.TWIncidentClass.Oral.ToString();
            sText = TypeRule.TWIncidentClass.OralName;
            System.Web.UI.WebControls.ListItem lItem2 = new System.Web.UI.WebControls.ListItem(sText, sData);
            cboFill.Items.Add(lItem2);
            //	
            //			sData=TypeRule.TWIncidentClass.Rework.ToString();
            //			sText=TypeRule.TWIncidentClass.ReworkName;
            //			System.Web.UI.WebControls.ListItem lItem3=new System.Web.UI.WebControls.ListItem(sText,sData);
            //			cboFill.Items.Add(lItem3);
            //
            //			sData=TypeRule.TWIncidentClass.Complaints.ToString();
            //			sText=TypeRule.TWIncidentClass.ComplaintsName;
            //			System.Web.UI.WebControls.ListItem lItem4=new System.Web.UI.WebControls.ListItem(sText,sData);
            //			cboFill.Items.Add(lItem4);

        }

        /// <summary>
        /// 报事类型HtmlSelect
        /// </summary>
        /// <param name="cboFill"></param>
        /// <param name="IsNull"></param>
        public static void FillSelIncidentClass(ref HtmlSelect cboFill, bool IsNull)
        {
            cboFill.Items.Clear();

            if (IsNull)
            {
                System.Web.UI.WebControls.ListItem lItem = new System.Web.UI.WebControls.ListItem("", "");
                cboFill.Items.Add(lItem);
            }
            string sText, sData;
            //1书面派工、2口头派工、3返修处理、4投诉处理

            sData = "0";
            sText = "全部";
            System.Web.UI.WebControls.ListItem lItem5 = new System.Web.UI.WebControls.ListItem(sText, sData);
            cboFill.Items.Add(lItem5);


            sData = TypeRule.TWIncidentClass.Paper.ToString();
            sText = TypeRule.TWIncidentClass.PaperName;
            System.Web.UI.WebControls.ListItem lItem1 = new System.Web.UI.WebControls.ListItem(sText, sData);
            cboFill.Items.Add(lItem1);

            sData = TypeRule.TWIncidentClass.Oral.ToString();
            sText = TypeRule.TWIncidentClass.OralName;
            System.Web.UI.WebControls.ListItem lItem2 = new System.Web.UI.WebControls.ListItem(sText, sData);
            cboFill.Items.Add(lItem2);
            //	
            //			sData=TypeRule.TWIncidentClass.Rework.ToString();
            //			sText=TypeRule.TWIncidentClass.ReworkName;
            //			System.Web.UI.WebControls.ListItem lItem3=new System.Web.UI.WebControls.ListItem(sText,sData);
            //			cboFill.Items.Add(lItem3);
            //
            //			sData=TypeRule.TWIncidentClass.Complaints.ToString();
            //			sText=TypeRule.TWIncidentClass.ComplaintsName;
            //			System.Web.UI.WebControls.ListItem lItem4=new System.Web.UI.WebControls.ListItem(sText,sData);
            //			cboFill.Items.Add(lItem4);

        }

        #endregion


        #region 老HM系统数据字典
        /// <summary>
        /// </summary>
        /// <param name="SQLEx"></param>
        /// <param name="DictionarySort"></param>
        /// <returns></returns>
        public static DataTable GetDictionarySortList(string SQLEx, string DictionarySort)
        {
            DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
            string Ret = "";
            SqlParameter[] parameters = {
					            new SqlParameter("@SQLEx", SqlDbType.VarChar,3999),
                                new SqlParameter("@DictionarySort", SqlDbType.VarChar,50)
                                              };
            parameters[0].Value = SQLEx;
            parameters[1].Value = DictionarySort;

            DataSet  Ds = DbHelperSQL.RunProcedure("Proc_Dictionary_Sort_Filter", parameters, "RetDataSet");
            DataTable dTable = new DataTable(); ;
            if (Ds.Tables.Count > 0)
            {
                dTable = Ds.Tables[0];
            }
            return dTable;
        }
        #endregion

        #region 数据字典

        public static void FillSelectTextDictionary(ref HtmlSelect cboFill, bool IsNull, string DictionarySort, bool IsSearch)
        {
            cboFill.Items.Clear();

            if (IsSearch)
            {
                System.Web.UI.WebControls.ListItem sItem = new System.Web.UI.WebControls.ListItem("", "");
                cboFill.Items.Add(sItem);
            }

            if (IsNull)
            {
                System.Web.UI.WebControls.ListItem lItem = new System.Web.UI.WebControls.ListItem("", "");
                cboFill.Items.Add(lItem);
            }

            string sText, sData, sSign;

            DataTable dTable = GetDictionarySortList("", DictionarySort);
            foreach (DataRow DRow in dTable.Rows)
            {
                sData = DRow["DictionaryName"].ToString();
                sText = DRow["DictionaryName"].ToString();
                sSign = DRow["DictionarySign"].ToString();
                if (sSign != "")
                {
                    sText = sText;
                }
                System.Web.UI.WebControls.ListItem lItem = new System.Web.UI.WebControls.ListItem(sText, sData);
                cboFill.Items.Add(lItem);
            }

            dTable.Dispose();

        }
        public static void FillSelectDictionary(ref HtmlSelect cboFill, bool IsNull, string DictionarySort, bool IsSearch)
        {
            cboFill.Items.Clear();

            if (IsSearch)
            {
                System.Web.UI.WebControls.ListItem sItem = new System.Web.UI.WebControls.ListItem("","");
                cboFill.Items.Add(sItem);
            }

            if (IsNull)
            {
                System.Web.UI.WebControls.ListItem lItem = new System.Web.UI.WebControls.ListItem("", "");
                cboFill.Items.Add(lItem);
            }

            string sText, sData, sSign;

            DataTable dTable = GetDictionarySortList("", DictionarySort);
            foreach (DataRow DRow in dTable.Rows)
            {
                sData = DRow["DictionaryCode"].ToString();
                sText = DRow["DictionaryName"].ToString();
                sSign = DRow["DictionarySign"].ToString();
                if (sSign != "")
                {
                    sText = sText;
                }
                System.Web.UI.WebControls.ListItem lItem = new System.Web.UI.WebControls.ListItem(sText, sData);
                cboFill.Items.Add(lItem);
            }

            dTable.Dispose();

        }

        public static void FillDictionary(ref DropDownList cboFill, bool IsNull, string DictionarySort, bool IsSearch)
        {
            cboFill.Items.Clear();

            if (IsSearch)
            {
                System.Web.UI.WebControls.ListItem sItem = new System.Web.UI.WebControls.ListItem("", "");
                cboFill.Items.Add(sItem);
            }

            DataTable dTable = GetDictionarySortList("", DictionarySort);
            foreach (DataRow DRow in dTable.Rows)
            {
                string sText, sData, sSign;
                sData = DRow["DictionaryCode"].ToString();
                sText = DRow["DictionaryName"].ToString();
                sSign = DRow["DictionarySign"].ToString();
                if (sSign != "")
                {
                    sText = sText;
                }
                System.Web.UI.WebControls.ListItem lItem = new System.Web.UI.WebControls.ListItem(sText, sData);
                cboFill.Items.Add(lItem);
            }

            dTable.Dispose();
        }
        #endregion

        #region MD5 Hash
        public static string getMd5Hash(string input)
        {
            // Create a new instance of the MD5CryptoServiceProvider object. 
            MD5 md5Hasher = MD5.Create();
            // Convert the input string to a byte array and compute the hash. 
            byte[] data = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(input));
            // Create a new Stringbuilder to collect the bytes        
            // and create a string.        
            StringBuilder sBuilder = new StringBuilder();
            // Loop through each byte of the hashed data         
            // and format each one as a hexadecimal string.        
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        /// <summary>
        /// MD5(16位加密)
        /// </summary>
        /// <param name="ConvertString">需要加密的字符串</param>
        /// <returns>MD5加密后的字符串</returns>
        public static string GetMd5Str(string ConvertString)
        {
            string md5Pwd = string.Empty;

            //使用加密服务提供程序
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

            //将指定的字节子数组的每个元素的数值转换为它的等效十六进制字符串表示形式。
            md5Pwd = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(ConvertString)), 4, 8);

            md5Pwd = md5Pwd.Replace("-", "");

            return md5Pwd;
        }
        #endregion        

        #region 写日志文件
        public static void WriteLog(string FileFlag, string Message,string fileFolder = "")
        {
            string fileLogPath = @"d:\logFils\";
            if (!string.IsNullOrWhiteSpace(fileFolder))
            {
                fileLogPath += $@"{fileFolder}\";
            }

            DateTime d = DateTime.Now;
            string LogFileName = FileFlag + d.ToString("yyyyMMdd").ToString() + ".txt";

            //DirectoryInfo path=new DirectoryInfo(LogFileName);
            //如果日志文件目录不存在,则创建
            if (!Directory.Exists(fileLogPath))
            {
                Directory.CreateDirectory(fileLogPath);
            }

            FileInfo finfo = new FileInfo(fileLogPath + LogFileName);

            try
            {
                FileStream fs = new FileStream(fileLogPath + LogFileName, FileMode.Append);
                StreamWriter strwriter = new StreamWriter(fs);
                try
                {
                    strwriter.WriteLine(Message);
                    strwriter.WriteLine();
                    strwriter.Flush();
                }
                catch
                {
                    //Console.WriteLine("日志文件写入失败信息:"+ee.ToString()); 
                }
                finally
                {
                    strwriter.Close();
                    strwriter = null;
                    fs.Close();
                    fs = null;
                }
            }
            catch
            {

            }
        }
        #endregion

        public static void FillModel(DataRow Row, object T)
        {
            foreach (var propertyInfo in T.GetType().GetProperties())
            {
                if (Row.Table.Columns.Contains(propertyInfo.Name) == true)
                {
                    try
                    {
                        string value = Row[propertyInfo.Name].ToString();
                        if (!propertyInfo.PropertyType.IsGenericType)
                        {
                            //非泛型
                            propertyInfo.SetValue(T, string.IsNullOrEmpty(value) ? null : Convert.ChangeType(value, propertyInfo.PropertyType), null);
                        }
                        else
                        {
                            //泛型Nullable<>
                            Type genericTypeDefinition = propertyInfo.PropertyType.GetGenericTypeDefinition();
                            if (genericTypeDefinition == typeof(Nullable<>))
                            {
                                propertyInfo.SetValue(T, string.IsNullOrEmpty(value) ? null : Convert.ChangeType(value, Nullable.GetUnderlyingType(propertyInfo.PropertyType)), null);
                            }
                        }
                    }
                    catch
                    {
                    }
                }
            }
        }

        /// <summary>
        /// 获取配置节的值
        /// </summary>
        public static string GetAppSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        /// <summary>
        /// 获取配置节的值
        /// </summary>
        public static string GetConnectionString(string key)
        {
            return ConfigurationManager.ConnectionStrings[key].ConnectionString;
        }

        /// <summary>
        /// 获取数据行中的单元格值
        /// </summary>
        /// <typeparam name="T">值数据类型</typeparam>
        /// <param name="dataRow">数据行</param>
        /// <param name="dataColumnName">单元格名称【DataColumnName】</param>
        /// <param name="defaultValue">T类型的默认值</param>
        /// <returns>单元格的值</returns>
        public static T GetCellValue<T>(DataRow dataRow, string dataColumnName, T defaultValue = default(T))
        {
            if (dataRow.Table.Columns.Contains(dataColumnName))
            {
                try
                {
                    return (T)Convert.ChangeType(dataRow[dataColumnName], typeof(T));
                }
                catch
                {
                    return defaultValue;
                }
            }
            else
            {
                return defaultValue;
            }
        }
    }
}