using Common;
using Dapper;
using DapperExtensions;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using Model.Buss;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Business
{
    public class PMSAppHomepage : PubInfo
    {
        public PMSAppHomepage()
        {
            base.Token = "20200417PMSAppHomepage";
        }
        public override void Operate(ref Transfer Trans)
        {
            Trans.Result = JSONHelper.FromString(false, "未知错误");
            DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
            DataRow Row = dAttributeTable.Rows[0];

            switch (Trans.Command)
            {
                case "GetCommunityServiceTel":
                    Trans.Result = GetCommunityServiceTel(Row);//获取服务电话
                    break;
            }
        }

        /// <summary>
        /// 获取服务电话
        /// </summary>
        private string GetCommunityServiceTel(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return JSONHelper.FromString(false, "小区Id不能为空");
            }

            var communityId = row["CommunityId"].ToString();
            var community = GetCommunity(communityId);

            var roomId = "";
            if (row.Table.Columns.Contains("RoomID") && !string.IsNullOrEmpty(row["RoomID"].ToString()))
            {
                roomId = row["RoomID"].ToString();
            }

            var dataList = new List<dynamic>();

            using (var conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                var sql = @"SELECT isnull(TEL,'') AS TEL FROM Tb_Community WHERE Id=@CommunityId";

                var data = conn.Query<string>(sql, new { CommunityId = community.Id }).FirstOrDefault();

                foreach (var tel in data.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    var tmp = tel.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);

                    var value = tmp.LastOrDefault();
                    var key = "";
                    if (tmp.Length > 1) { key = tmp[0]; }

                    dataList.Add(new { Name = key, Tel = value });
                };
            }

            // 业主App如需调用物管数据库，需先链接字符串
            PubConstant.hmWyglConnectionString = GetConnectionStr(community);
            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = @"SELECT isnull(Heading,'') AS Heading,isnull(InfoContent,'') AS InfoContent
                            FROM Tb_HSPR_CommunityInfo WHERE InfoType='fwdh' AND CommID=@CommID;";

                var data = conn.Query(sql, new { CommID = community.CommID }).ToList();
                if (data.Count > 0)
                {
                    dataList.AddRange(data.Select(obj => new { Name = obj.Heading, Tel = obj.InfoContent }));
                }

                // 有房屋Id，就要去读取管家电话
                if (!string.IsNullOrEmpty(roomId))
                {
                    sql = "SELECT isnull(object_id('Tb_HSPR_RoomHousekeeper', N'U'),0);";

                    if (conn.Query<long>(sql).FirstOrDefault() != 0)
                    {
                        // 管家设置到房屋
                        sql = @"SELECT b.UserName,a.Tel FROM Tb_HSPR_RoomHousekeeper a
                                INNER JOIN Tb_Sys_User b ON a.UserCode=b.UserCode
                                WHERE a.RoomID=@RoomID";

                        var housekeeperInfo = conn.Query(sql, new { RoomID = roomId }).FirstOrDefault();
                        if (housekeeperInfo != null)
                        {
                            dataList.Add(new { Name = $"管家.{housekeeperInfo.UserName}", Tel = housekeeperInfo.Tel });
                        }
                        else
                        {
                            sql = @"SELECT b.UserName,b.MobileTel AS Tel FROM Tb_HSPR_Room a 
                                    INNER JOIN Tb_Sys_User b ON a.UserCode=b.UserCode
                                    WHERE a.RoomID=@RoomID;";

                            housekeeperInfo = conn.Query(sql, new { RoomID = roomId }).FirstOrDefault();
                            if (housekeeperInfo != null)
                            {
                                dataList.Add(new { Name = $"管家.{housekeeperInfo.UserName}", Tel = housekeeperInfo.Tel });
                            }
                        }
                    }
                    else
                    {
                        // 管家设置到楼栋
                        sql = @"SELECT isnull(c.UserName,'') AS UserName,isnull(a.Tel,isnull(c.MobileTel,'')) AS Tel 
                                FROM Tb_HSPR_BuildHousekeeper a
                                INNER JOIN Tb_HSPR_Room b ON a.CommID=b.CommID AND b.BuildSNum=a.BuildSNum
                                LEFT JOIN Tb_Sys_User c ON c.UserCode=b.UserCode
                                WHERE a.CommID=@CommID AND b.RoomID=@RoomID;";

                        var housekeeperInfo = conn.Query(sql, new { CommID = community.CommID, RoomID = roomId }).FirstOrDefault();
                        if (housekeeperInfo != null)
                        {
                            dataList.Add(new { Name = $"管家.{housekeeperInfo.UserName}", Tel = housekeeperInfo.Tel });
                        }
                    }
                }

                dataList.RemoveAll(obj => obj.Tel == null || obj.Tel.ToString().Trim().Length == 0);
            }
            return new ApiResult(true, dataList).toJson();
        }
    }
}
