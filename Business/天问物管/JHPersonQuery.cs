using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Dapper;
using log4net;
using MobileSoft.Common;
using MobileSoft.DBUtility;

namespace Business
{
    public class JHPersonQuery : PubInfo
    {
        private static ILog log;
        public JHPersonQuery() {
            base.Token = "20180611JHPersonQuery";
            log = LogManager.GetLogger(typeof(JHPersonQuery));
        }
        public override void Operate(ref Transfer Trans)
        {
            Trans.Result = new ApiResult(false, "接口不存在").toJson();
            DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
            DataRow Row = dAttributeTable.Rows[0];

            //验证登录
            if (!new Login().isLogin(ref Trans))
            {
                Trans.Result = new ApiResult(false, "登录失败,请重试").toJson();
                return;
            }
            //防止未捕获异常出现
            try
            {
                switch (Trans.Command) {
                    case "GetBaseInfo":
                        // 获取基本信息
                        Trans.Result = GetBaseInfo(Row);
                        break;
                    case "GetWageInfo":
                        // 获取工资信息
                        Trans.Result = GetBaseInfo(Row);
                        break;
                    case "GetAnnualLeaveInfo":
                        // 获取年假信息
                        Trans.Result = GetAnnualLeaveInfo(Row);
                        break;
                    case "GetLeaveList":
                        // 获取请假记录信息
                        Trans.Result = GetLeaveList(Row);
                        break;

                    case "GetAttendanceList":
                        // 获取考勤记录信息
                        Trans.Result = GetAttendanceList(Row);
                        break;
                        
                }
            }
            catch (Exception ex)
            {
                log.Error("Opearate", ex);
                Trans.Result = new ApiResult(false, "接口抛出了一个异常").toJson();
            }
        }

        /// <summary>
        /// 是否存在该用户的人事档案
        /// </summary>
        /// <param name="LoginCode">不带CorpId的登录名</param>
        /// <param name="Id">输出人事档案记录ID</param>
        /// <returns></returns>
        private bool hasUserInfo(string LoginCode, out string Id)
        {
            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                Id = conn.QueryFirstOrDefault<string>("SELECT * FROM Tb_Pm_PersonList WHERE SysAccount = @SysAccount", new { SysAccount = LoginCode });
                if (string.IsNullOrEmpty(Id))
                {
                    return false;
                }
                return true;
            }
        }

        /// <summary>
        /// 获取人员基本信息
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string GetBaseInfo(DataRow row)
        {
            string Id;
            if (!hasUserInfo(Global_Var.LoginCode, out Id))
            {
                return new ApiResult(false, "该用户暂未关联人事档案").toJson();
            }
            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                dynamic baseInfo = conn.QueryFirstOrDefault<dynamic>("SELECT Name, Sex, Age, DepName, PersonRoleName, EducationName, PersonTypeName, EntryDate, FormalDate FROM View_Edit_Tb_Pm_PersonList_Filter WHERE Id = @Id", new { Id = Id });
                if (null == baseInfo)
                {
                    return new ApiResult(false, "该用户基础档案不存在").toJson();
                }
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("Name", baseInfo.Name);
                dictionary.Add("Sex", baseInfo.Sex);
                dictionary.Add("Age", baseInfo.Age);
                dictionary.Add("DepName", baseInfo.DepName);
                dictionary.Add("PersonRoleName", baseInfo.PersonRoleName);
                dictionary.Add("EducationName", baseInfo.EducationName);
                dictionary.Add("PersonTypeName", baseInfo.PersonTypeName);
                dictionary.Add("EntryDate", baseInfo.EntryDate);
                dictionary.Add("FormalDate", baseInfo.FormalDate);
                return new ApiResult(true, baseInfo).toJson();
            }
        }


        /// <summary>
        /// 获取人员工资信息
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string GetWageInfo(DataRow row)
        {
            DateTime dateNow = DateTime.Now;
            DateTime wageDate = dateNow;
            // 要查询的工资时间
            if (row.Table.Columns.Contains("Date") && !string.IsNullOrEmpty(row["Date"].ToString()))
            {
                if (!DateTime.TryParse(row["Date"].ToString(), out wageDate))
                {
                    wageDate = dateNow;
                }
            }

            if (wageDate > dateNow)
            {
                wageDate = dateNow;
            }
            // 人事档案Id
            string Id;
            if (!hasUserInfo(Global_Var.LoginCode, out Id))
            {
                return new ApiResult(false, "该用户暂未关联人事档案").toJson();
            }
            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                string SalaryYear = wageDate.Year.ToString();
                string SalaryMonth = wageDate.Month < 10 ? "0" + wageDate.Month.ToString() : wageDate.Month.ToString();
                DataSet ds = conn.ExecuteReader("SELECT *  FROM View_Pm_SalaryData_Filter_new WHERE PersonId = @Id AND SalaryYear = @SalaryYear AND SalaryMonth = @SalaryMonth", new { Id, SalaryYear, SalaryMonth }).ToDataSet();
                if (null == ds || ds.Tables.Count == 0)
                {
                    return new ApiResult(false, "未查询到该月的工资信息").toJson();
                }
                DataTable dt = ds.Tables[0];
                if (null == dt || dt.Rows.Count == 0)
                {
                    return new ApiResult(false, "未查询到该月的工资信息").toJson();
                }
                DataRow item = dt.Rows[0];
                if (null == item)
                {
                    return new ApiResult(false, "未查询到该月的工资信息").toJson();
                }

                Dictionary<string, object> dictionary = new Dictionary<string, object>();

                // 姓名
                string Name = null;
                if (dt.Columns.Contains("Name"))
                {
                    Name = item["Name"].ToString();
                }
                dictionary.Add("Name", Name);

                // 工资日期
                dictionary.Add("Date", wageDate.ToString("yyyy-MM"));

                // 基本工资
                decimal BaseWage = 0.00M;
                if (dt.Columns.Contains("基本工资"))
                {
                    BaseWage = Convert.ToDecimal(item["基本工资"].ToString());
                }
                dictionary.Add("BaseWage", BaseWage);

                // 岗位工资
                decimal JobWage = 0.00M;
                if (dt.Columns.Contains("岗位工资"))
                {
                    JobWage = Convert.ToDecimal(item["岗位工资"].ToString());
                }
                dictionary.Add("JobWage", JobWage);

                // 月度工资(岗位+基本)
                decimal MonthWage = BaseWage + JobWage;
                dictionary.Add("MonthWage", MonthWage);

                // 缺勤扣款
                decimal AbsenceDeduction = 0.00M;
                if (dt.Columns.Contains("缺勤扣款"))
                {
                    AbsenceDeduction = Convert.ToDecimal(item["缺勤扣款"].ToString());
                }
                dictionary.Add("AbsenceDeduction", AbsenceDeduction);

                // 事假扣款
                decimal PALDeduction = 0.00M;
                if (dt.Columns.Contains("事假扣款"))
                {
                    PALDeduction = Convert.ToDecimal(item["事假扣款"].ToString());
                }
                dictionary.Add("PALDeduction", PALDeduction);

                // 病假扣款
                decimal SLDeduction = 0.00M;
                if (dt.Columns.Contains("病假扣款"))
                {
                    SLDeduction = Convert.ToDecimal(item["病假扣款"].ToString());
                }
                dictionary.Add("SLDeduction", SLDeduction);

                // 旷工扣款
                decimal AbsenteeismDeduction = 0.00M;
                if (dt.Columns.Contains("旷工扣款"))
                {
                    AbsenteeismDeduction = Convert.ToDecimal(item["旷工扣款"].ToString());
                }
                dictionary.Add("AbsenteeismDeduction", AbsenteeismDeduction);

                // 迟到/早退扣款
                decimal LADeduction = 0.00M;
                if (dt.Columns.Contains("迟到/早退扣款"))
                {
                    LADeduction = Convert.ToDecimal(item["迟到/早退扣款"].ToString());
                }
                dictionary.Add("LADeduction", LADeduction);

                // 产假扣款
                decimal MLDeduction = 0.00M;
                if (dt.Columns.Contains("产假扣款"))
                {
                    MLDeduction = Convert.ToDecimal(item["产假扣款"].ToString());
                }
                dictionary.Add("MLDeduction", MLDeduction);

                // 工伤假扣款
                decimal IIVDeduction = 0.00M;
                if (dt.Columns.Contains("工伤假扣款"))
                {
                    IIVDeduction = Convert.ToDecimal(item["工伤假扣款"].ToString());
                }
                dictionary.Add("IIVDeduction", IIVDeduction);

                // 日常扣款
                decimal DailyDeduction = 0.00M;
                if (dt.Columns.Contains("日常扣款"))
                {
                    DailyDeduction = Convert.ToDecimal(item["日常扣款"].ToString());
                }
                dictionary.Add("DailyDeduction", DailyDeduction);

                // 其他扣款
                decimal OtherDeduction = 0.00M;
                if (dt.Columns.Contains("其他扣款"))
                {
                    OtherDeduction = Convert.ToDecimal(item["其他扣款"].ToString());
                }
                dictionary.Add("OtherDeduction", OtherDeduction);

                // 应发工资
                decimal ShouldPayWage = 0.00M;
                if (dt.Columns.Contains("应发工资"))
                {
                    ShouldPayWage = Convert.ToDecimal(item["应发工资"].ToString());
                }
                dictionary.Add("ShouldPayWage", ShouldPayWage);

                // 养老个人扣款
                decimal PPDeduction = 0.00M;
                if (dt.Columns.Contains("养老个人扣款"))
                {
                    PPDeduction = Convert.ToDecimal(item["养老个人扣款"].ToString());
                }
                dictionary.Add("PPDeduction", PPDeduction);

                // 医疗个人扣款
                decimal MPDeduction = 0.00M;
                if (dt.Columns.Contains("医疗个人扣款"))
                {
                    MPDeduction = Convert.ToDecimal(item["医疗个人扣款"].ToString());
                }
                dictionary.Add("MPDeduction", PPDeduction);

                // 失业个人扣款
                decimal UIDeduction = 0.00M;
                if (dt.Columns.Contains("失业个人扣款"))
                {
                    UIDeduction = Convert.ToDecimal(item["失业个人扣款"].ToString());
                }
                dictionary.Add("UIDeduction", UIDeduction);

                // 公积金个人扣款
                decimal PFPDeduction = 0.00M;
                if (dt.Columns.Contains("公积金个人扣款"))
                {
                    PFPDeduction = Convert.ToDecimal(item["公积金个人扣款"].ToString());
                }
                dictionary.Add("PFPDeduction", PFPDeduction);

                // 个税
                decimal IncomeTax = 0.00M;
                if (dt.Columns.Contains("个税"))
                {
                    IncomeTax = Convert.ToDecimal(item["个税"].ToString());
                }
                dictionary.Add("IncomeTax", IncomeTax);

                // 社保公积金个税应扣合计
                decimal AFSSTotal = PPDeduction + MPDeduction + UIDeduction + PFPDeduction + IncomeTax;
                dictionary.Add("AFSSTotal", AFSSTotal);

                // 实发工资
                decimal RealWage = 0.00M;
                if (dt.Columns.Contains("实发工资"))
                {
                    RealWage = Convert.ToDecimal(item["实发工资"].ToString());
                }
                dictionary.Add("RealWage", RealWage);

                return new ApiResult(true, dictionary).toJson();
            }
        }

        /// <summary>
        /// 获取年假情况
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string GetAnnualLeaveInfo(DataRow row)
        {
            // 人事档案Id
            string Id;
            if (!hasUserInfo(Global_Var.LoginCode, out Id))
            {
                return new ApiResult(false, "该用户暂未关联人事档案").toJson();
            }

            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                DataSet ds = conn.ExecuteReader("Proc_Tb_Pm_AnnualLeaveStatistics", new { DepCode = "", EntryDateStart = "", EntryDateEnd = "", EntryYearStart = "", EntryYearEnd = "", Where = string.Format("AND PersonList.Id = '{0}'", Id) }).ToDataSet();
                if (null == ds || ds.Tables.Count == 0)
                {
                    return new ApiResult(false, "未查询到该用户的年假信息").toJson();
                }
                DataTable dt = ds.Tables[0];
                if (null == dt || dt.Rows.Count == 0)
                {
                    return new ApiResult(false, "未查询到该用户的年假信息").toJson();
                }
                DataRow item = dt.Rows[0];
                if (null == item)
                {
                    return new ApiResult(false, "未查询到该用户的年假信息").toJson();
                }
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("Name", item["Name"]);
                dictionary.Add("EntryDate", item["EntryDate"]);
                dictionary.Add("Years", item["Years"]);
                dictionary.Add("LeaveDay", item["LeaveDay"]);
                dictionary.Add("RestDay", item["RestDay"]);
                return new ApiResult(true, dictionary).toJson();
            }
        }

        /// <summary>
        /// 获取请假记录
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string GetLeaveList(DataRow row)
        {
            // 分页页码
            int page = 1;
            if (row.Table.Columns.Contains("Page"))
            {
                if (!int.TryParse(row["Page"].ToString(), out page))
                {
                    page = 1;
                }
                else
                {
                    if (page <= 0)
                    {
                        page = 1;
                    }
                }
            }

            // 分页大小
            int size = 10;
            if (row.Table.Columns.Contains("Size"))
            {
                if (!int.TryParse(row["Size"].ToString(), out size))
                {
                    size = 10;
                }
                else
                {
                    if (size <= 0)
                    {
                        size = 10;
                    }
                }
            }

            // 人事档案Id
            string Id;
            if (!hasUserInfo(Global_Var.LoginCode, out Id))
            {
                return new ApiResult(false, "该用户暂未关联人事档案").toJson();
            }

            int PageCount;
            int Counts;
            Dictionary<string, object> data = new Dictionary<string, object>();
            List<Dictionary<string, object>> resultList = new List<Dictionary<string, object>>();

            string sql = string.Format("SELECT BType,StartTime,EndTime,LeaveDay FROM View_Pm_LeaveShowFilter WHERE PersonId = '{0}' AND ISNULL(IsDelete,0) = 0 AND CheckState = '已审核'", Id);

            DataSet ds = GetList(out PageCount, out Counts, sql, page, size, "TransactDate", 1, "Id", PubConstant.hmWyglConnectionString);
            if (null != ds && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (null != dt && dt.Rows.Count > 0)
                {
                    Dictionary<string, object> dictionary;
                    foreach (DataRow item in dt.Rows)
                    {
                        dictionary = new Dictionary<string, object>();
                        foreach (DataColumn colum in dt.Columns)
                        {
                            dictionary.Add(colum.ColumnName, item[colum.ColumnName]);
                        }
                        resultList.Add(dictionary);
                    }
                }
            }
            data.Add("pages", PageCount);
            data.Add("total", Counts);
            data.Add("list", resultList);
            return new ApiResult(true, data).toJson();
        }


        /// <summary>
        /// 获取考勤记录
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string GetAttendanceList(DataRow row)
        {
            // 分页页码
            int page = 1;
            if (row.Table.Columns.Contains("Page"))
            {
                if (!int.TryParse(row["Page"].ToString(), out page))
                {
                    page = 1;
                }
                else
                {
                    if (page <= 0)
                    {
                        page = 1;
                    }
                }
            }

            // 分页大小
            int size = 10;
            if (row.Table.Columns.Contains("Size"))
            {
                if (!int.TryParse(row["Size"].ToString(), out size))
                {
                    size = 10;
                }
                else
                {
                    if (size <= 0)
                    {
                        size = 10;
                    }
                }
            }

            // 人事档案Id
            string Id;
            if (!hasUserInfo(Global_Var.LoginCode, out Id))
            {
                return new ApiResult(false, "该用户暂未关联人事档案").toJson();
            }

            int PageCount;
            int Counts;
            Dictionary<string, object> data = new Dictionary<string, object>();
            List<Dictionary<string, object>> resultList = new List<Dictionary<string, object>>();

            string sql = string.Format("SELECT CheckDate,DictionaryName,CheckUser,RewardsMode,BaseValue,SalaryMonth FROM View_Tb_Pm_RewardPunishment_Filter WHERE Pid = '{0}' AND ISNULL(IsDelete,0) = 0 AND CheckState = '已审核'", Id);

            DataSet ds = GetList(out PageCount, out Counts, sql, page, size, "CheckDate", 1, "Id", PubConstant.hmWyglConnectionString);
            if (null != ds && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (null != dt && dt.Rows.Count > 0)
                {
                    Dictionary<string, object> dictionary;
                    foreach (DataRow item in dt.Rows)
                    {
                        dictionary = new Dictionary<string, object>();
                        foreach (DataColumn colum in dt.Columns)
                        {
                            dictionary.Add(colum.ColumnName, item[colum.ColumnName]);
                        }
                        resultList.Add(dictionary);
                    }
                }
            }
            data.Add("pages", PageCount);
            data.Add("total", Counts);
            data.Add("list", resultList);
            return new ApiResult(true, data).toJson();
        }
        
    }
}
