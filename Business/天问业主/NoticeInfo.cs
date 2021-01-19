using Common;
using Dapper;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using MobileSoft.Model.Unified;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Business
{
    public class NoticeInfo : PubInfo
    {
        public NoticeInfo() { base.Token = "20160908NoticeInfo"; }
        public override void Operate(ref Transfer Trans)
        {
            Trans.Result = JSONHelper.FromString(false, "未知错误");

            DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
            DataRow Row = dAttributeTable.Rows[0];

            switch (Trans.Command)
            {

                case "NoticeDeskTopInfo":
                    Trans.Result = GetNoticeDeskTopInfo(Row, Trans.Mac);
                    break;
                case "NoticeTypeDetail":
                    Trans.Result = GetNoticeTypeDetail(Row, Trans.Mac);
                    break;
                case "GetAnnouncement":
                    Trans.Result = GetAnnouncement(Row, Trans.Mac);
                    break;
            }
        }

        /// <summary>
        /// 获取物管广告信息
        /// </summary>
        private string GetNoticeDeskTopInfo(DataRow row, string mac)
        {
            int TopNum = 5;
            string CommunityId = "";
            if (row.Table.Columns.Contains("TopNum"))
            {
                TopNum = AppGlobal.StrToInt(row["TopNum"].ToString());
            }

            if (!row.Table.Columns.Contains("CommunityId"))
            {
                return JSONHelper.FromString(new DataTable());
            }
            else
            {
                CommunityId = row["CommunityId"].ToString();
            }

            string sql = @"SELECT top " + TopNum + @" A.Id,UserId,A.NoticeType,A.Title,CAST(A.Content AS VARCHAR(MAX)) as Content,A.IssueDate,
	                    (SELECT '[' + T.CommName + ']' FROM dbo.Tb_Community T WHERE CHARINDEX(T.Id, A.CommunityId)> 0 FOR XML PATH('')) AS CommName, A.CommunityId,
	                    A.IsDelete,A.ImageURL,A.ContentURL FROM Tb_Notice A
                        LEFT OUTER JOIN dbo.Tb_Community B ON CHARINDEX(B.Id, A.CommunityId)> 0
                        WHERE 1 = 1  and isnull(A.IsDelete,0) = 0 AND b.Id = '" + CommunityId + @"' and isnull(a.Noticetype,0) = '2'
                        GROUP BY A.Id,UserId,A.NoticeType,A.Title,CAST(A.Content AS VARCHAR(MAX)),A.IssueDate,
	                    A.CommunityId,
	                    A.IsDelete,A.ImageURL,A.ContentURL order by a.issuedate desc";

            using (IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                IEnumerable<dynamic> resultSet = conn.Query(sql);

                if (resultSet.Count() > 0)
                {
                    return new ApiResult(true, resultSet).toJson();
                }
                return JSONHelper.FromString(new DataTable());
            }
        }

        /// <summary>
        /// 获取平台广告详情
        /// </summary>
        private string GetNoticeTypeDetail(DataRow row, string mac)
        {
            string Id = "";
            if (!row.Table.Columns.Contains("Id"))
            {
                return JSONHelper.FromString(false, "暂无数据");
            }
            else
            {
                Id = row["Id"].ToString();

                string Sql = "SELECT * FROM Tb_Notice WHERE Id = '" + Id + "' ";

                IDbConnection Connectionstr = new SqlConnection(Connection.GetConnection("4"));
                Tb_Notice Tb_Notice = Connectionstr.Query<Tb_Notice>(string.Format(Sql)).SingleOrDefault();
                if (Tb_Notice == null)
                {

                    return JSONHelper.FromString(new DataTable());
                }
                else
                {
                    return JSONHelper.FromString(Tb_Notice);
                }
            }

        }

        /// <summary>
        /// 获取平台公告
        /// </summary>
        private string GetAnnouncement(DataRow row, string mac)
        {
            int TopNum = 2;
            string CommunityId = "";
            if (row.Table.Columns.Contains("TopNum"))
            {
                TopNum = AppGlobal.StrToInt(row["TopNum"].ToString());
            }

            if (!row.Table.Columns.Contains("CommunityId"))
            {
                return JSONHelper.FromString(new DataTable());
            }
            else
            {
                CommunityId = row["CommunityId"].ToString();
            }

            string sql = @"SELECT top " + TopNum + @" A.Id as [InfoID],UserId,A.NoticeType,A.Title AS [Heading],CAST(A.Content AS VARCHAR(MAX)) as Content,A.IssueDate, (SELECT '[' + T.CommName + ']' FROM dbo.Tb_Community T WHERE CHARINDEX(T.Id, A.CommunityId)> 0 FOR XML PATH('')) AS CommName, A.CommunityId,
	                    A.IsDelete,A.ImageURL as ImageUrl ,A.ContentURL FROM Tb_Notice A
                        LEFT OUTER JOIN dbo.Tb_Community B ON CHARINDEX(B.Id, A.CommunityId)> 0
                        WHERE 1 = 1  and isnull(A.IsDelete,0) = 0 AND b.Id = '" + CommunityId + @"' and isnull(a.Noticetype,0) = '1'
                        GROUP BY A.Id,UserId,A.NoticeType,A.Title,CAST(A.Content AS VARCHAR(MAX)),A.IssueDate,
	                    A.CommunityId,
	                    A.IsDelete,A.ImageURL,A.ContentURL order by a.IssueDate desc";

            DataTable dTable = new DbHelperSQLP(PubConstant.UnifiedContionString.ToString()).Query(string.Format(sql)).Tables[0];
            if (dTable.Rows.Count > 0)
            {
                return JSONHelper.FromString(dTable);
            }
            else
            {
                dTable.Dispose();
                dTable = new DataTable();
                return JSONHelper.FromString(dTable);
            }
        }
       
    }

}
