using Dapper;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Service.Visit
{
    public partial class Guide : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var corpId = Request["CorpId"].ToString();
            var taskId = Request["TaskId"].ToString();
            var entry = Request["Net"] ?? "1";

            Global_Var.CorpId = corpId;
            Global_Var.CorpID = corpId;
            Global_Var.LoginCorpID = corpId;
            PubConstant.tw2bsConnectionString = Global_Fun.Tw2bsConnectionString(entry);
            AppGlobal.GetHmWyglConnection();

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = @"SELECT GuidanceDocuments FROM Tb_Visit_VisitCategory WHERE ID=(SELECT VisitCategoryID FROM Tb_Visit_Plan WHERE ID=@PlanId);";
                var guide = conn.Query<string>(sql, new { PlanId = taskId }).FirstOrDefault();

                guideContent.InnerText = guide ?? "暂无指引信息";
            }
        }
    }
}