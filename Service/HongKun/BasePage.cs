using Dapper;
using MobileSoft.Common;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Service.HongKun
{
    public class BasePage : System.Web.UI.Page
    {
        public BasePage(bool isPreProduct = false) {
            this.isPreProduct = isPreProduct;
        }
        private bool isPreProduct;
        private static readonly string m_connectionString = @"Pooling=false;Data Source=192.168.0.174;Initial Catalog=HM_wygl_new_1973;User ID=LFUser;Password=LF123SPoss";
        private static readonly string m_connectionString_test = @"Pooling=false;Data Source=192.168.0.236;Initial Catalog=HM_wygl_new_test;User ID=LFUser;Password=LF123SPoss";
        private static readonly string m_connectionString_pre_product = @"Pooling=false;Data Source=192.168.0.236;Initial Catalog=HM_wygl_new_1973;User ID=LFUser;Password=LF123SPoss";

        public static string HongKunReportEnvironment => AppGlobal.GetAppSetting("HongKunReportEnvironment");
        public string ERPConnectionString => (isPreProduct ? m_connectionString_pre_product : HongKunReportEnvironment == "1" ? m_connectionString : m_connectionString_test);

        public string OrganCodeStr;
        public string LoginCodeStr;

        /// <summary>
        /// 读取用户code
        /// </summary>
        public string GetUserCode(string loginCode)
        {
            using (var conn = new SqlConnection(ERPConnectionString))
            {
                dynamic userInfo = conn.Query("SELECT UserCode FROM Tb_Sys_User WHERE LoginCode=@LoginCode AND isnull(IsDelete,0)=0", 
                    new { LoginCode = loginCode }).FirstOrDefault();

                if (userInfo != null)
                {
                    return userInfo.UserCode;
                }
                return null;
            }
        }

        /// <summary>
        /// 获取机构/项目名称
        /// </summary>
        public string GetOrganName(string organCode)
        {
            if (string.IsNullOrEmpty(organCode))
            {
                return organCode;
            }

            string sql = "";
            if (organCode.Length == 2 || organCode.Length == 4)
            {
                sql = "SELECT OrganName FROM Tb_Sys_Organ WHERE OrganCode=@OrganCode AND isnull(IsDelete,0)=0";
            }
            else if(organCode.Length == 6)
            {
                sql = "SELECT CommName AS OrganName FROM Tb_HSPR_Community WHERE CommID=@OrganCode AND isnull(IsDelete,0)=0";
            }

            using (var conn = new SqlConnection(ERPConnectionString))
            {
                dynamic organInfo = conn.Query(sql, new { OrganCode = organCode }).FirstOrDefault();

                if (organInfo != null)
                {
                    return organInfo.OrganName;
                }
                return string.Empty;
            }
        }

        /// <summary>
        /// 获取用户有管理权限的机构/项目
        /// </summary>
        public List<dynamic> GetUserOrganTree(string userCode)
        {
            using (var conn = new SqlConnection(ERPConnectionString))
            {
                IEnumerable<dynamic> resultSet = conn.Query("Proc_Sys_Organ_GetEntryNodes", new { UserCode = userCode }, null, false, null, CommandType.StoredProcedure);

                List<dynamic> resultList = new List<dynamic>();

                foreach (var item in resultSet)
                {
                    resultList.Add(new
                    {
                        OrganCode = (item.InPopedom == 0 ? item.OrganCode.ToString() : item.InPopedom.ToString()),
                        OrganName = item.OrganName
                    });
                }

                return resultList;
            }
        }

        
    }
}