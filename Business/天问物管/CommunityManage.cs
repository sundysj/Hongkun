using Common;
using Dapper;
using MobileSoft.Common;
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
    public class CommunityManage : PubInfo
    {
        public CommunityManage()
        {
            base.Token = "20180426CommunityManage";
        }

        public override void Operate(ref Transfer Trans)
        {
            Trans.Result = new ApiResult(false, "未知错误").toJson();
            DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
            DataRow Row = dAttributeTable.Rows[0];

            switch (Trans.Command)
            {
                case "GetCommunityList":
                    Trans.Result = GetCommunityList(Row);                        // 获取小区列表
                    break;
                case "GetCommuityListForZL": //左邻需要项目信息
                    Trans.Result = GetCommunityListForZL(Row);
                    break;
            }
        }

        private string GetCommunityList(DataRow row)
        {
            if (!row.Table.Columns.Contains("CorpID") || string.IsNullOrEmpty(row["CorpID"].ToString()))
            {
                return JSONHelper.FromString(false, "公司编号不能为空");
            }

            using (IDbConnection conn = new SqlConnection(Global_Fun.Tw2bsConnectionString("1")))
            {
                dynamic dbInfo = conn.Query(@"SELECT DBServer,DBName,DBUser,DBPwd FROM Tb_System_Corp WHERE CorpID=@CorpID",
                    new { CorpID = row["CorpID"].ToString() }).FirstOrDefault();

                if (dbInfo != null)
                {
                    Tb_Community community = new Tb_Community()
                    {
                        DBServer = dbInfo.DBServer,
                        DBName = dbInfo.DBName,
                        DBUser = dbInfo.DBUser,
                        DBPwd = dbInfo.DBPwd
                    };

                    using (IDbConnection conn2 = new SqlConnection(GetConnectionStringStr(community)))
                    {
                        IEnumerable<dynamic> resultSet = conn2.Query(@"SELECT CommID,CommName FROM Tb_HSPR_Community WHERE isnull(IsDelete,0)=0");

                        return new ApiResult(true, resultSet).toJson();
                    }
                }
                return JSONHelper.FromString(false, "未查询到公司信息");
            }
        }

        private string GetCommunityListForZL(DataRow row)
        {
            if (!row.Table.Columns.Contains("CorpID") || string.IsNullOrEmpty(row["CorpID"].ToString()))
            {
                return JSONHelper.FromString(false, "公司编号不能为空");
            }

            using (IDbConnection conn = new SqlConnection(Connection.GetConnection("4")))
            {
                IEnumerable<dynamic> resultSet = conn.Query(@"SELECT c.Id as CommunityId,c.CommID,c.CommName,z.ProjectID FROM Tb_Community c inner join Tb_Community_ZL z on c.Id = z.communityID");

                return new ApiResult(true, resultSet).toJson();
            }
        }
    }
}
