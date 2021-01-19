using System;
using MobileSoft.DBUtility;
using MobileSoft.Common;
using System.Data;
using System.Text;
using System.Linq;
using Common;
using System.Data.SqlClient;
using Dapper;
using DapperExtensions;
using System.Collections.Generic;
using MobileSoft.Model.Unified;
using MobileSoft.Model.HSPR;

namespace Business
{
    /// <summary>
    /// 业主建议
    /// </summary>
    public  class UserProposal: PubInfo
    {
        public UserProposal() 
        {
            base.Token = "20160822OUserProposal";
        }

        public override void Operate(ref Common.Transfer Trans)
        {
            Trans.Result = JSONHelper.FromString(false, "未知错误");

            DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
            DataRow Row = dAttributeTable.Rows[0];

            switch (Trans.Command)
            {
                case "SubmitProposal":
                    Trans.Result = SubmitProposal(Row);//提交建议
                    break;
                case "SetEvaluation":
                    Trans.Result = SetEvaluation(Row);//评价
                    break;
                case "GetProposalInfo":
                    Trans.Result = GetProposalInfo(Row);//获取建议
                    break;
                case "GetProposalInfoList"://所有建议
                    Trans.Result = GetProposalInfoList(Row);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 提交建议
        /// </summary>
        /// <param name="row">用户编号、小区编号、建议、客户编号均不能为空</param>       
        /// <returns></returns>
        private string SubmitProposal(DataRow row)
        {
            if (!row.Table.Columns.Contains("UserId") || string.IsNullOrEmpty(row["UserId"].ToString()))
            {
                return JSONHelper.FromString(false, "用户编码不能为空");
            }
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编码不能为空");
            }
            if (!row.Table.Columns.Contains("RecommendedContent")|| string .IsNullOrEmpty(row["RecommendedContent"].ToString()))
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
            if (Community_list == null|| Community_list.Count<=0)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }
            Tb_Community Community = Community_list[0];
            //获取绑定信息
            string sql_user = "select * from Tb_User_Relation where UserId=@UserId and CommunityId=@CommunityId and CustId=@CustId";
            List<Tb_User_Relation> User_Relation = Conn.Query<Tb_User_Relation>(sql_user, new { UserId = UserId, CommunityId= CommunityId, CustId= CustID }).ToList<Tb_User_Relation>();
            if (User_Relation==null||User_Relation.Count<=0)
            {
                return JSONHelper.FromString(false, "该业主没绑定房屋");
            }
            //构建ERP字符串
            Conn=new SqlConnection( new CostInfo().GetConnectionStringStr(Community));
            Tb_HSPR_CommunitySuggestions model = new Tb_HSPR_CommunitySuggestions();
            model.SuggestionsID = Guid.NewGuid().ToString();
            model.CommID =Convert.ToInt64( Community.CommID);
            model.CustID =Convert.ToInt64( User_Relation[0].CustId);
            model.RoomID = Convert.ToInt64(RoomID ?? User_Relation[0].RoomId);
            model.RoomSign = User_Relation[0].RoomSign;
            model.CustName = User_Relation[0].CustName;
            model.LinkPhone = User_Relation[0].Custmobile;
            model.IssueDate = DateTime.Now;
            model.ReplyStats = "未回复";
            model.SuggestionsType = "0002";
            model.IsDelete = 0;
            model.SuggestionsContent = RecommendedContent;
            model.SuggestionsImages = SuggestionsImages;
            Conn.Insert<Tb_HSPR_CommunitySuggestions>(model);
            return JSONHelper.FromString(true, "提交建议成功");
        }


        /// <summary>
        /// 提交评价
        /// </summary>
        /// 此处的评价不能小于等于0，在实际操作中业主可能不会具体去点几星，建议在这种情况时给一个默认值。
        /// <param name="row">评价ID、评价等级</param>
        /// <returns></returns>
        private string SetEvaluation(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编码不能为空");
            }
           
            if (!row.Table.Columns.Contains("Id") || string.IsNullOrEmpty(row["Id"].ToString()))
            {
                return JSONHelper.FromString(false, "建议ID不能为空");
            }
            if (!row.Table.Columns.Contains("EvaluationLevel") || AppGlobal.StrToInt( row["EvaluationLevel"].ToString())<=0)
            {
                return JSONHelper.FromString(false, "评价等级不能为空或者小于等于0");
            }

            string Id = row["Id"].ToString();
            string CommunityId = row["CommunityId"].ToString();
            string ReplyContent = "";
            if (row.Table.Columns.Contains("ReplyContent") &&!string.IsNullOrEmpty(row["ReplyContent"].ToString()))
            {
                ReplyContent = row["ReplyContent"].ToString();
            }
            //运营连接字符串
            IDbConnection Conn = new SqlConnection(Connection.GetConnection("4"));
            //获取小区信息
            string sql_com = "select * from dbo.Tb_Community where Id=@Id";
            Tb_Community Community = Conn.Query<Tb_Community>(sql_com, new { Id = CommunityId }).SingleOrDefault();
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }
            Conn = new SqlConnection(new CostInfo().GetConnectionStringStr(Community));



            string query = "SELECT * FROM Tb_HSPR_CommunitySuggestions WHERE SuggestionsID=@Id";
            Tb_HSPR_CommunitySuggestions model =Conn.Query<Tb_HSPR_CommunitySuggestions>(query, new { Id = Id }).SingleOrDefault();
            model.EvaluationLevel =Convert.ToInt32( row["EvaluationLevel"]);
            model.CustEvaluation = ReplyContent;
            model.EvaluationDate = DateTime.Now;
            Conn.Update<Tb_HSPR_CommunitySuggestions>(model);


            //new MobileSoft.BLL.Unified.Bll_Tb_User_Recommended().Update(model);



            return JSONHelper.FromString(true, "提交服务评价成功");
        }

        /// <summary>
        /// 获取建议
        /// </summary>
        /// <param name="row">评价ID</param>
        /// 返回：
        ///     SuggestionsID   建议ID
        ///     CommID          小区ID
        ///     CustID          客户ID
        ///     RoomID          房间ID
        ///     RoomSign        房间编号
        ///     CustName        客户名称
        ///     LinkPhone       联系方式        
        ///     SuggestionsType 建议类型
        ///     SuggestionsTitle 建议标题
        ///     SuggestionsImages 建议图片
        ///     IsDelete        是否删除
        ///     SuggestionsContent 建议内容
        ///     CustEvaluation  评价内容
        ///     ReplyStats      回复状态
        ///     ReplyContent    回复内容
        ///     ReplyDate       回复日期
        ///     EvaluationDate  评价日期
        ///     EvaluationLevel  评价级别
        /// <returns></returns>
        private string GetProposalInfo(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编码不能为空");
            }
            if (!row.Table.Columns.Contains("Id") || string.IsNullOrEmpty(row["Id"].ToString()))
            {
                return JSONHelper.FromString(false, "建议ID不能为空");
            }
           
            string Id = row["Id"].ToString();
            string CommunityId = row["CommunityId"].ToString();

            //运营连接字符串
            IDbConnection Conn = new SqlConnection(Connection.GetConnection("4"));
            //获取小区信息
            string sql_com = "select * from dbo.Tb_Community where Id=@Id";
            Tb_Community Community = Conn.Query<Tb_Community>(sql_com, new { Id = CommunityId }).SingleOrDefault();
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }
            Conn = new SqlConnection(new CostInfo().GetConnectionStringStr(Community));

            string query = "SELECT * FROM Tb_HSPR_CommunitySuggestions WHERE SuggestionsID='"+ Id + "'";

            DataTable dt = Conn.ExecuteReader(query, null, null, null, CommandType.Text).ToDataSet().Tables[0];
            return JSONHelper.FromString(dt);
        }

        /// <summary>
        /// 获取所有建议
        /// </summary>
        /// <param name="row"></param>
        /// 用户编号：UserId    必填
        /// 小区编号：CommunityId 必填
        /// <returns></returns>
        private string GetProposalInfoList(DataRow row)
        {
            if (!row.Table.Columns.Contains("UserId") || string.IsNullOrEmpty(row["UserId"].ToString()))
            {
                return JSONHelper.FromString(false, "用户编号不能为空");
            }
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }

            string CustID = row["UserId"].ToString();
            string CommunityId = row["CommunityId"].ToString();

            //运营连接字符串
            IDbConnection Conn = new SqlConnection(Connection.GetConnection("4"));
            //获取小区信息
            string sql_com = "select * from dbo.Tb_Community where Id=@Id";
            Tb_Community Community = Conn.Query<Tb_Community>(sql_com, new { Id = CommunityId }).SingleOrDefault();
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }
            //获取绑定的房屋
            string sql_user = "select * from Tb_User_Relation where UserId=@UserId and CommunityId=@CommunityId";
            List<Tb_User_Relation> User_Relation = Conn.Query<Tb_User_Relation>(sql_user, new { UserId = CustID, CommunityId = CommunityId }).ToList<Tb_User_Relation>();
            if (User_Relation==null || User_Relation.Count<=0)
            {
                return JSONHelper.FromString(false, "未绑定任何小区");
            }
            
           
            List<Tb_HSPR_CommunitySuggestions> model = new List<Tb_HSPR_CommunitySuggestions>();
            DataTable dt = new DataTable();
            foreach (Tb_User_Relation item in User_Relation)
            {
             
                Conn = new SqlConnection(new CostInfo().GetConnectionStringStr(Community));
                string query = "select * from Tb_HSPR_CommunitySuggestions where CustID="+ item.CustId + " and CommID=" + Community.CommID + " and IsDelete=0 order by IssueDate desc";

                if (dt.Rows.Count == 0)
                {
                    dt = Conn.ExecuteReader(query, null, null, null, CommandType.Text).ToDataSet().Tables[0].Copy();
                }
                else
                {
                    dt.Merge(Conn.ExecuteReader(query, null, null, null, CommandType.Text).ToDataSet().Tables[0].Copy());
                }
                Conn.Dispose();
            }

            return JSONHelper.FromString(dt);
        }

    }
}
