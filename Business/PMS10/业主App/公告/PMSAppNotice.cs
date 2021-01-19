using Common;
using Common.Enum;
using Common.Extenions;
using Dapper;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using MobileSoft.Model.Unified;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using static Dapper.SqlMapper;
namespace Business
{
    public class PMSAppNotice : PubInfo
    {
        public PMSAppNotice()
        {
            base.Token = "20200302PMSAppNotice";
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
                    case "GetERPTopNotice":
                        Trans.Result = GetERPTopNotice(Row);
                        break;
                    case "GetaAvertisingInfo":
                        Trans.Result = GetaAvertisingInfo(Row);
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
        /// 获取社区广播信息，包含社区资讯、亲情提示、社区文化、服务指南、社区活动，只获取ERP数据
        /// </summary>
        private string GetERPTopNotice(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].AsString()))
            {
                return new ApiResult(false, "缺少参数CommunityId").toJson();
            }

            var communityId = row["CommunityId"].AsString();

            var community = GetCommunity(communityId);
            if (community == null)
            {
                return JSONHelper.FromString(false, "未查询到小区信息");
            }

            PubConstant.hmWyglConnectionString = GetConnectionStr(community);

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                // 是否使用10.0版本活动
                var sql = @"SELECT object_id(N'Tb_HSPR_CommActivities_New',N'U');";

                var v10 = (conn.Query<string>(sql).FirstOrDefault() != null);

                sql = @"SELECT TOP 5 * FROM
                        (
                            SELECT convert(varchar(36),InfoID) AS IID,0 AS IsActivity,IssueDate AS PubDate 
                            FROM Tb_HSPR_CommunityInfo 
                            WHERE isnull(IsDelete,0)=0 AND (CommID=@CommID OR CommID=0)
                            UNION ALL
                            SELECT convert(varchar(36),ActivitiesID) AS IID,1 AS IsActivity,isnull(IssueDate,ActivitiesStartDate) AS PubDate
                            FROM Tb_HSPR_CommActivities 
                            WHERE isnull(IsDelete,0)=0 AND (CommID=@CommID OR CommID=0)
                        ) AS t
                        ORDER BY t.PubDate DESC;";

                if (v10)
                {
                    sql = @"SELECT TOP 5 * FROM
                            (
                                SELECT convert(varchar(36),InfoID) AS IID,0 AS IsActivity,IssueDate AS PubDate 
                                FROM Tb_HSPR_CommunityInfo 
                                WHERE isnull(IsDelete,0)=0 AND (CommID=@CommID OR CommID=0)
                                UNION ALL
                                SELECT convert(varchar(36),ActivitiesID) AS IID,1 AS IsActivity,CreateTime AS PubDate 
                                FROM Tb_HSPR_CommActivities_New 
                                WHERE isnull(IsDelete,0)=0 AND (CommID=@CommID OR CommID=0)
                            ) AS t
                            ORDER BY t.PubDate DESC;";
                }

                var data = conn.Query(sql, new { CommID = community.CommID });

                var infos = new List<dynamic>();

                foreach (dynamic item in data)
                {
                    if (item.IsActivity == 0)
                    {
                        sql = @"SELECT convert(varchar(36),InfoID) AS IID,Heading AS Title,ImageUrl AS Image,IssueDate AS PubDate,
                                    InfoType AS Type,0 AS IsActivity,0 AS IsEnd,'5' AS Version  
                                FROM Tb_HSPR_CommunityInfo 
                                WHERE InfoID=@IID";
                    }
                    else
                    {
                        if (v10)
                        {
                            sql = @"SELECT convert(varchar(36),ActivitiesID) AS IID,a.ActivitiesTheme AS Title,a.ActivitiesImages AS Image,
                                        isnull(a.LastModifyTime,a.CreateTime) AS PubDate,
                                        null AS Type,1 AS IsActivity,ActivitiesStartDate,ActivitiesEndDate,
                                        CASE WHEN ActivitiesEndDate<getdate() THEN 1 ELSE 0 END AS IsEnd,
                                        '10' AS Version
                                    FROM Tb_HSPR_CommActivities_New a
                                    LEFT JOIN Tb_Dictionary_Activities b ON a.ActivitiesCategory=b.DictionaryCode
                                    WHERE a.ActivitiesID=@IID";
                        }
                        else
                        {
                            sql = @"SELECT convert(varchar(36),ActivitiesID) AS IID,ActivitiesTheme AS Title,ActivitiesImages AS Image,
                                        isnull(IssueDate,ActivitiesStartDate) AS PubDate,
                                        null AS Type,1 AS IsActivity,ActivitiesStartDate,ActivitiesEndDate,
                                        CASE WHEN ActivitiesEndDate<getdate() THEN 1 ELSE 0 END AS IsEnd,
                                        '5' AS Version 
                                    FROM Tb_HSPR_CommActivities WHERE isnull(IsDelete,0)=0
                                    WHERE a.ActivitiesID=@IID";
                        }
                    }

                    var tmp = conn.Query(sql, new { IID = item.IID }).FirstOrDefault();
                    if (tmp != null)
                    {
                        infos.Add(tmp);
                    }
                }

                infos.ForEach(obj =>
                {
                    if (obj.Image != null && string.IsNullOrEmpty(obj.Image.Trim(new char[] { ',' })))
                    {
                        obj.Image = obj.Image.ToString().Split(',')[0];
                    }
                });

                return new ApiResult(true, infos).toJson();
            }
        }

        /// <summary>
        /// 获取广告信息
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private String GetaAvertisingInfo(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].AsString()))
            {
                return new ApiResult(false, "缺少参数CommunityId").toJson();
            }
            String communityId = row["CommunityId"].ToString();
            try
            {
                DateTime now = DateTime.Now;
                using (var conn = new SqlConnection(PubConstant.UnifiedContionString))
                {
                    String sqlstr = $@"SELECT  TOP 1
                                    [HomepageAdvertisementType],[Title],[Content],[ImageURL],[ContentURL] ,[JumpMode] ,[InternalJumpType] ,[InternalJumpID] ,[InternalJumpContent],[PlaybackDuration] FROM Tb_HomepageAdvertisement 
                                    where 
                                    IsTheShelves=1 AND 
                                    EffectiveBegDate<=@Time AND 
                                    EffectiveEndDate>=@Time AND
                                    ISNULL(IsDelete,0)=0 AND
                                    CommunityId LIKE '%{communityId}%'
                                    ORDER BY IssueDate DESC;";

                    var dataInfo = conn.QueryFirstOrDefault(sqlstr, new { Time = now });
                    if (dataInfo == null) return new ApiResult(true, null).toJson();

                    int jumpType = 0, jumpModel = 0;
                    if (!String.IsNullOrEmpty((String)dataInfo.JumpMode)) jumpType = EnumHelper.GetEnumValueByDesc(typeof(JumpModelEnum), (String)dataInfo.JumpMode);
                    if (!String.IsNullOrEmpty((String)dataInfo.InternalJumpType)) jumpModel = EnumHelper.GetEnumValueByDesc(typeof(JumpTypeEnum), (String)dataInfo.InternalJumpType);
                    dataInfo.JumpMode = jumpType.ToString();
                    dataInfo.InternalJumpType = jumpModel.ToString();

                    return new ApiResult(true, dataInfo).toJson();
                }
            }
            catch (Exception ex)
            {
                return new ApiResult(false, "获取广告信息失败").toJson();
            }
        }
    }
}
