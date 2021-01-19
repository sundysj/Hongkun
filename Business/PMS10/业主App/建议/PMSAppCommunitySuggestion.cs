using Common;
using Dapper;
using DapperExtensions;
using MobileSoft.Common;
using MobileSoft.Model.HSPR;
using MobileSoft.Model.Unified;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class PMSAppCommunitySuggestion : PubInfo
    {
        public PMSAppCommunitySuggestion()
        {
            base.Token = "202006241038PMSAppCommunitySuggestion";
        }

        public override void Operate(ref Transfer Trans)
        {
            DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
            DataRow Row = dAttributeTable.Rows[0];

            //防止未捕获异常出现
            try
            {
                switch (Trans.Command)
                {
                    case "SubmitSuggestion":
                        Trans.Result = SubmitSuggestion(Row);
                        break;
                    default:
                        Trans.Result = new ApiResult(false, "未知错误").toJson();
                        break;
                }
            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace + Environment.NewLine + ex.Source);
                Trans.Result = new ApiResult(false, ex.Message + ex.StackTrace).toJson();
            }
        }



        /// <summary>
        /// 提交建议
        /// </summary>
        /// <param name="row">用户编号、小区编号、建议、客户编号均不能为空</param>       
        /// <returns></returns>
        private string SubmitSuggestion(DataRow row)
        {
            if (!row.Table.Columns.Contains("UserId") || string.IsNullOrEmpty(row["UserId"].ToString()))
            {
                return JSONHelper.FromString(false, "用户编码不能为空");
            }
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编码不能为空");
            }
            if (!row.Table.Columns.Contains("RecommendedContent") || string.IsNullOrEmpty(row["RecommendedContent"].ToString()))
            {
                return JSONHelper.FromString(false, "建议不能为空");
            }
            if (!row.Table.Columns.Contains("CustID") || string.IsNullOrEmpty(row["CustID"].ToString()))
            {
                return JSONHelper.FromString(false, "客户编号不能为空");
            }
            string UserId = row["UserId"].ToString();
            string CommunityId = row["CommunityId"].ToString();
            string RecommendedContent = row["RecommendedContent"].ToString();
            string CustID = row["CustID"].ToString();

            string SuggestionsImages = null;
            string RoomID = null;

            if (row.Table.Columns.Contains("SuggestionsImages") && !string.IsNullOrEmpty(row["SuggestionsImages"].ToString()))
            {
                SuggestionsImages = row["SuggestionsImages"].ToString();
            }
            if (row.Table.Columns.Contains("RoomID") && string.IsNullOrEmpty(row["RoomID"].ToString()))
            {
                RoomID = row["RoomID"].ToString();
            }

            //运营连接字符串
            IDbConnection Conn = new SqlConnection(Connection.GetConnection("4"));
            //获取小区信息
            string sql_com = "select * from dbo.Tb_Community where Id=@Id";
            List<Tb_Community> Community_list = Conn.Query<Tb_Community>(sql_com, new { Id = CommunityId }).ToList<Tb_Community>();
            if (Community_list == null || Community_list.Count <= 0)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }
            Tb_Community Community = Community_list[0];
            //获取绑定信息
            string sql_user = "select * from Tb_User_Relation where UserId=@UserId and CommunityId=@CommunityId and CustId=@CustId";
            List<Tb_User_Relation> User_Relation = Conn.Query<Tb_User_Relation>(sql_user, new { UserId = UserId, CommunityId = CommunityId, CustId = CustID }).ToList<Tb_User_Relation>();
            if (User_Relation == null || User_Relation.Count <= 0)
            {
                return JSONHelper.FromString(false, "该业主没绑定房屋");
            }
            //构建ERP字符串
            Conn = new SqlConnection(new CostInfo().GetConnectionStringStr(Community));
            Tb_HSPR_CommunitySuggestions model = new Tb_HSPR_CommunitySuggestions();
            model.SuggestionsID = Guid.NewGuid().ToString();
            model.CommID = Convert.ToInt64(Community.CommID);
            model.CustID = Convert.ToInt64(User_Relation[0].CustId);
            model.RoomID = Convert.ToInt64(RoomID ?? User_Relation[0].RoomId);
            model.RoomSign = User_Relation[0].RoomSign;
            model.CustName = User_Relation[0].CustName;
            model.LinkPhone = User_Relation[0].Custmobile;
            model.IssueDate = DateTime.Now;
            model.ReplyStats = "未回复";
            model.SuggestionsType = null;
            model.IsDelete = 0;
            model.SuggestionsContent = RecommendedContent;
            model.SuggestionsImages = SuggestionsImages;
            Conn.Insert<Tb_HSPR_CommunitySuggestions>(model);
            return JSONHelper.FromString(true, "提交建议成功");
        }
    }
}
