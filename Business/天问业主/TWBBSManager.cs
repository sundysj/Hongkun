using Common;
using Dapper;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using MobileSoft.Model.Unified;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TWTools.Push;
using static Dapper.SqlMapper;

namespace Business
{
    public class TWBBSManager : PubInfo
    {
        public TWBBSManager() { base.Token = "20180327TWBBSManager"; }
        public override void Operate(ref Transfer Trans)
        {
            Trans.Result = "false:";

            DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
            DataRow Row = dAttributeTable.Rows[0];

            switch (Trans.Command)
            {
                // 社区聊吧
                case "GetChatBarList":
                    Trans.Result = GetChatBarList(Row);
                    break;
                case "GetChatBarTop":
                    Trans.Result = GetChatBarTop(Row);
                    break;
                case "GetChatBarInfoContent":
                    Trans.Result = GetChatBarInfoContent(Row);
                    break;
                case "GetMyPubChatBarList":
                    Trans.Result = GetMyPubChatBarList(Row);
                    break;
                case "SaveChatBarInfo":
                    Trans.Result = SaveChatBarInfo(Row);
                    break;
                case "DeleteChatBarInfo":
                    Trans.Result = DeleteChatBarInfo(Row);
                    break;

                // 社区圈子
                case "GetCircleType":
                    Trans.Result = GetCircleType(Row);
                    break;
                case "GetCircleList":
                    Trans.Result = GetCircleList(Row);
                    break;
                case "GetCircleTop":
                    Trans.Result = GetCircleTop(Row);
                    break;
                case "GetCircleInfoContent":
                    Trans.Result = GetCircleInfoContent(Row);
                    break;
                case "GetMyPubCircleList":
                    Trans.Result = GetMyPubCircleList(Row);
                    break;
                case "SaveCircleInfo":
                    Trans.Result = SaveCircleInfo(Row);
                    break;
                case "DeleteCircleInfo":
                    Trans.Result = DeleteCircleInfo(Row);
                    break;

                // 二手市场
                case "GetSecondHandMarketType":
                    Trans.Result = GetSecondHandMarketType(Row);
                    break;
                case "GetSecondHandMarketList":
                    Trans.Result = GetSecondHandMarketList(Row);
                    break;
                case "GetSecondHandMarketRecommend":
                    Trans.Result = GetSecondHandMarketRecommend(Row);
                    break;
                case "GetSecondHandMarketDetail":
                    Trans.Result = GetSecondHandMarketDetail(Row);
                    break;
                case "GetMyPubSecondHandMarketList":
                    Trans.Result = GetMyPubSecondHandMarketList(Row);
                    break;
                case "SaveSecondHandMarketInfo":
                    Trans.Result = SaveSecondHandMarketInfo(Row);
                    break;
                case "DeleteSecondHandMarketInfo":
                    Trans.Result = DeleteSecondHandMarketInfo(Row);
                    break;

                // 评论
                case "GetCommentList":
                    Trans.Result = GetCommentList(Row);
                    break;
                case "GetCommentFollowList":
                    Trans.Result = GetCommentFollowList(Row);
                    break;
                case "AddComment":
                    Trans.Result = AddComment(Row);
                    break;
                case "DeleteComment":
                    Trans.Result = DeleteComment(Row);
                    break;
                case "AgreeComment":
                    Trans.Result = AgreeComment(Row);
                    break;
                case "AgreeInfo":
                    Trans.Result = AgreeInfo(Row);
                    break;

                // 其他
                case "GetInfoStatistics":
                    Trans.Result = GetInfoStatistics(Row);
                    break;
                case "SetInfoRead":
                    Trans.Result = SetInfoRead(Row);
                    break;
                case "GetReportType":
                    Trans.Result = GetReportType(Row);
                    break;
                case "SaveReport":
                    Trans.Result = SaveReport(Row);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 获取聊吧帖子列表
        /// </summary>
        private string GetChatBarList(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "小区Id不能为空");
            }
            if (!row.Table.Columns.Contains("PageSize") || string.IsNullOrEmpty(row["PageSize"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "页长不能为空");
            }
            if (!row.Table.Columns.Contains("PageIndex") || string.IsNullOrEmpty(row["PageIndex"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "页码不能为空");
            }

            string communityId = row["CommunityId"].ToString();
            int pageSize = AppGlobal.StrToInt(row["PageSize"].ToString());
            int pageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());
            string userID = "";

            if (row.Table.Columns.Contains("UserID") && !string.IsNullOrEmpty(row["UserID"].ToString()))
            {
                userID = row["UserID"].ToString();
            }

            string sql = string.Format(@"SELECT a.ID,a.Title,a.Images,a.ReadCount,a.CommentCount,a.AgreeCount,a.PubDate,a.UserID,a.UserName,a.UserPic,
                                            (SELECT count(0) FROM Tb_BBS_Agree b WHERE b.InfoID=a.ID AND b.UserID='{0}') AS IsAgree
                                         FROM View_BBS_ChatBar_List_Phone a WHERE a.IsDelete=0 AND a.IsAudit=1 AND a.CommunityId='{1}'", userID, communityId);

            DataTable dataTable = GetList(out int pageCount, out int count, sql, pageIndex, pageSize, "PubDate", 1, "ID",
                PubConstant.UnifiedContionString).Tables[0];

            string result = JSONHelper.FromString(dataTable);
            result = result.Insert(result.Length - 1, ",\"PageCount\":" + pageCount);
            return result;
        }

        /// <summary>
        /// 获取聊吧置顶信息
        /// </summary>
        private string GetChatBarTop(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "小区Id不能为空");
            }

            using (IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                IEnumerable<dynamic> resultSet = conn.Query("Proc_BBS_ChatBar_HOT_Phone", new { CommunityId = row["CommunityId"].ToString() }, null, false, null, CommandType.StoredProcedure);

                return new ApiResult(true, resultSet).toJson();
            }
        }

        /// <summary>
        /// 获取聊吧帖子详情
        /// </summary>
        private string GetChatBarInfoContent(DataRow row)
        {
            if (!row.Table.Columns.Contains("ID") || string.IsNullOrEmpty(row["ID"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "帖子Id不能为空");
            }
            if (!row.Table.Columns.Contains("UserID") || string.IsNullOrEmpty(row["UserID"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "用户Id不能为空");
            }
            string userId = row["UserID"].ToString();

            using (IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                string content = conn.Query<string>(@"SELECT Content FROM Tb_BBS_ChatBar WHERE ID=@ID", new { ID = row["ID"].ToString() }).FirstOrDefault();
                conn.Execute("Proc_BBS_ReadRecord_Add_Phone", new { UserID = userId, InfoID = row["ID"].ToString() }, null, null, CommandType.StoredProcedure);
                return JSONHelper.FromString(true, content);
            }
        }

        /// <summary>
        /// 获取我发布的聊吧帖子列表
        /// </summary>
        private string GetMyPubChatBarList(DataRow row)
        {
            if (!row.Table.Columns.Contains("UserID") || string.IsNullOrEmpty(row["UserID"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "用户id不能为空");
            }
            if (!row.Table.Columns.Contains("PageSize") || string.IsNullOrEmpty(row["PageSize"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "页长不能为空");
            }
            if (!row.Table.Columns.Contains("PageIndex") || string.IsNullOrEmpty(row["PageIndex"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "页码不能为空");
            }

            string userID = row["UserID"].ToString();
            int pageSize = AppGlobal.StrToInt(row["PageSize"].ToString());
            int pageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());

            string sql = string.Format(@"SELECT ID,CommunityID,CommName,Title,Images,ReadCount,CommentCount,AgreeCount,PubDate
                                         FROM View_BBS_ChatBar_List_Phone WHERE IsDelete=0 AND UserID='{0}'", userID);

            DataTable dataTable = GetList(out int pageCount, out int count, sql, pageIndex, pageSize, "PubDate", 1, "ID", PubConstant.UnifiedContionString).Tables[0];

            string result = JSONHelper.FromString(dataTable);
            result = result.Insert(result.Length - 1, ",\"PageCount\":" + pageCount);
            return result;
        }

        /// <summary>
        /// 保存聊吧帖子信息
        /// </summary>
        private string SaveChatBarInfo(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "小区Id不能为空");
            }
            if (!row.Table.Columns.Contains("UserID") || string.IsNullOrEmpty(row["UserID"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "用户Id不能为空");
            }
            if (!row.Table.Columns.Contains("Title") || string.IsNullOrEmpty(row["Title"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "帖子标题不能为空");
            }
            if (!row.Table.Columns.Contains("Content") || string.IsNullOrEmpty(row["Content"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "帖子内容不能为空");
            }

            string communityId = row["CommunityId"].ToString();
            string userID = row["UserID"].ToString();
            string title = row["Title"].ToString();
            string content = row["Content"].ToString();
            string images = null;
            string tags = null;
            string id = null;

            if (IsInBlackList(userID))
            {
                return JSONHelper.FromString(false, "操作失败，无权限");
            }

            if (row.Table.Columns.Contains("Images") && !string.IsNullOrEmpty(row["Images"].ToString()))
            {
                images = row["Images"].ToString();
            }

            if (row.Table.Columns.Contains("Tags") && !string.IsNullOrEmpty(row["Tags"].ToString()))
            {
                tags = row["Tags"].ToString();
            }

            if (row.Table.Columns.Contains("ID") && !string.IsNullOrEmpty(row["ID"].ToString()))
            {
                id = row["ID"].ToString();
            }

            // 获取数据库连接字符串
            Tb_Community tb_Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(communityId);
            if (null == tb_Community)
            {
                return new ApiResult(false, "该小区不存在").toJson();
            }

            using (IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                if (string.IsNullOrEmpty(id))
                {
                    int isAutoAudit = 1;

                    dynamic controlInfo = conn.Query(@"SELECT IsAutoAudit FROM Tb_AuditControl_BBS 
                                WHERE CorpID=@CorpID", new { CorpID = tb_Community.CorpID }).FirstOrDefault();

                    if (controlInfo != null)
                    {
                        isAutoAudit = Convert.ToInt32(controlInfo.IsAutoAudit);
                    }

                    conn.Execute(@"INSERT INTO Tb_BBS_ChatBar(ID, CommunityId, UserID, Tags, Title, Content, Images, IsTop, IsDelete, IsAudit) VALUES(newid(),@CommunityId, @UserID, @Tags, @Title, @Content, @Images, 0, 0, @IsAudit)",
                                    new
                                    {
                                        CommunityId = communityId,
                                        UserID = userID,
                                        Tags = tags,
                                        Title = title,
                                        Content = content,
                                        Images = images,
                                        IsAudit = isAutoAudit,
                                    });
                }
                else
                {
                    conn.Execute(@"UPDATE Tb_BBS_ChatBar SET CommunityId=@CommunityId, Title=@Title, Content=@Content, Images=@Images, Tags=@Tags WHERE ID=@ID",
                                     new
                                     {
                                         CommunityId = communityId,
                                         Tags = tags,
                                         Title = title,
                                         Content = content,
                                         Images = images,
                                         ID = id
                                     });
                }

                return new ApiResult(true, "保存成功").toJson();
            }
        }

        /// <summary>
        /// 删除聊吧帖子
        /// </summary>
        private string DeleteChatBarInfo(DataRow row)
        {
            if (!row.Table.Columns.Contains("ID") || string.IsNullOrEmpty(row["ID"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "帖子Id不能为空");
            }
            if (!row.Table.Columns.Contains("UserID") || string.IsNullOrEmpty(row["UserID"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "用户Id不能为空");
            }

            string ID = row["ID"].ToString();
            string UserID = row["UserID"].ToString();

            using (IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                int i = conn.Execute("UPDATE Tb_BBS_ChatBar SET IsDelete=1 WHERE ID=@ID AND UserID=@UserID", new { ID = ID, UserID = UserID });

                if (i > 0)
                {
                    return JSONHelper.FromString(true, "删除成功");
                }
                return JSONHelper.FromString(true, "删除失败");
            }
        }

        /// <summary>
        /// 获取圈子类型
        /// </summary>
        private string GetCircleType(DataRow row)
        {
            if (!row.Table.Columns.Contains("CorpID") || string.IsNullOrEmpty(row["CorpID"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "公司Id不能为空");
            }

            using (IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                IEnumerable<dynamic> resultSet = conn.Query(@"SELECT ID,CircleName,CircleImage,SortIndex FROM Tb_BBS_Circle_Type 
                                                                WHERE CorpID=@CorpID AND IsDelete=0", new { CorpID = row["CorpID"].ToString() });

                return new ApiResult(true, resultSet).toJson();
            }
        }

        /// <summary>
        /// 获取圈子帖子列表
        /// </summary>
        private string GetCircleList(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "小区Id不能为空");
            }

            if (!row.Table.Columns.Contains("PageSize") || string.IsNullOrEmpty(row["PageSize"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "页长不能为空");
            }
            if (!row.Table.Columns.Contains("PageIndex") || string.IsNullOrEmpty(row["PageIndex"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "页码不能为空");
            }

            string communityId = row["CommunityId"].ToString();
            int pageSize = AppGlobal.StrToInt(row["PageSize"].ToString());
            int pageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());
            string typeId = null;
            if (row.Table.Columns.Contains("TypeID") && !string.IsNullOrEmpty(row["TypeID"].ToString()))
            {
                typeId = row["TypeID"].ToString();
            }

            string userID = "";
            if (row.Table.Columns.Contains("UserID") && !string.IsNullOrEmpty(row["UserID"].ToString()))
            {
                userID = row["UserID"].ToString();
            }

            string sql = string.Format(@"SELECT a.ID,a.Title,a.Images,a.ReadCount,a.CommentCount,a.AgreeCount,a.PubDate,a.UserID,a.UserName,a.UserPic,
                                            (SELECT count(0) FROM Tb_BBS_Agree b WHERE b.InfoID=a.ID AND b.UserID='{0}') AS IsAgree
                                         FROM View_BBS_Circle_List_Phone a WHERE a.IsDelete=0 AND a.IsAudit=1 AND a.CommunityId='{1}' {2}",
                                          userID, communityId, (string.IsNullOrEmpty(typeId) ? "" : "AND TypeID='" + typeId + "'"));

            DataTable dataTable = GetList(out int pageCount, out int count, sql, pageIndex, pageSize, "PubDate", 1, "ID", PubConstant.UnifiedContionString).Tables[0];

            string result = JSONHelper.FromString(dataTable);
            result = result.Insert(result.Length - 1, ",\"PageCount\":" + pageCount);
            return result;
        }

        /// <summary>
        /// 获取圈子置顶帖子
        /// </summary>
        private string GetCircleTop(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "小区Id不能为空");
            }

            using (IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                IEnumerable<dynamic> resultSet = conn.Query("Proc_BBS_Circle_HOT_Phone", new { CommunityId = row["CommunityId"].ToString() }, null, false, null, CommandType.StoredProcedure);

                return new ApiResult(true, resultSet).toJson();
            }
        }

        /// <summary>
        /// 获取圈子帖子详情
        /// </summary>
        private string GetCircleInfoContent(DataRow row)
        {
            if (!row.Table.Columns.Contains("ID") || string.IsNullOrEmpty(row["ID"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "帖子Id不能为空");
            }
            if (!row.Table.Columns.Contains("UserID") || string.IsNullOrEmpty(row["UserID"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "用户Id不能为空");
            }
            string userId = row["UserID"].ToString();

            using (IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                string content = conn.Query<string>(@"SELECT Content FROM Tb_BBS_Circle WHERE ID=@ID", new { ID = row["ID"].ToString() }).FirstOrDefault();
                conn.Execute("Proc_BBS_ReadRecord_Add_Phone", new { UserID = userId, InfoID = row["ID"].ToString() }, null, null, CommandType.StoredProcedure);
                return JSONHelper.FromString(true, content);
            }
        }

        /// <summary>
        /// 获取我发布的圈子帖子列表
        /// </summary>
        private string GetMyPubCircleList(DataRow row)
        {
            if (!row.Table.Columns.Contains("UserID") || string.IsNullOrEmpty(row["UserID"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "用户id不能为空");
            }
            if (!row.Table.Columns.Contains("PageSize") || string.IsNullOrEmpty(row["PageSize"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "页长不能为空");
            }
            if (!row.Table.Columns.Contains("PageIndex") || string.IsNullOrEmpty(row["PageIndex"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "页码不能为空");
            }

            string userID = row["UserID"].ToString();
            int pageSize = AppGlobal.StrToInt(row["PageSize"].ToString());
            int pageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());

            string sql = string.Format(@"SELECT ID,CommunityID,CommName,TypeID,CircleName,Title,Images,ReadCount,CommentCount,AgreeCount,PubDate
                                         FROM View_BBS_Circle_List_Phone WHERE IsDelete=0 AND UserID='{0}'", userID);

            DataTable dataTable = GetList(out int pageCount, out int count, sql, pageIndex, pageSize, "PubDate", 1, "ID", PubConstant.UnifiedContionString).Tables[0];

            string result = JSONHelper.FromString(dataTable);
            result = result.Insert(result.Length - 1, ",\"PageCount\":" + pageCount);
            return result;
        }

        /// <summary>
        /// 保存圈子帖子信息
        /// </summary>
        private string SaveCircleInfo(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "小区Id不能为空");
            }
            if (!row.Table.Columns.Contains("UserID") || string.IsNullOrEmpty(row["UserID"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "用户Id不能为空");
            }
            if (!row.Table.Columns.Contains("TypeID") || string.IsNullOrEmpty(row["TypeID"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "圈子类型不能为空");
            }
            if (!row.Table.Columns.Contains("Title") || string.IsNullOrEmpty(row["Title"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "帖子标题不能为空");
            }
            if (!row.Table.Columns.Contains("Content") || string.IsNullOrEmpty(row["Content"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "帖子内容不能为空");
            }

            string communityId = row["CommunityId"].ToString();
            string userID = row["UserID"].ToString();
            string typeID = row["TypeID"].ToString();
            string title = row["Title"].ToString();
            string content = row["Content"].ToString();
            string images = null;
            string id = null;

            if (IsInBlackList(userID))
            {
                return JSONHelper.FromString(false, "操作失败，无权限");
            }

            if (row.Table.Columns.Contains("Images") && !string.IsNullOrEmpty(row["Images"].ToString()))
            {
                images = row["Images"].ToString();
            }

            if (row.Table.Columns.Contains("ID") && !string.IsNullOrEmpty(row["ID"].ToString()))
            {
                id = row["ID"].ToString();
            }

            // 获取数据库连接字符串
            Tb_Community tb_Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(communityId);
            if (null == tb_Community)
            {
                return new ApiResult(false, "该小区不存在").toJson();
            }

            using (IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                if (string.IsNullOrEmpty(id))
                {
                    int isAutoAudit = 1;

                    dynamic controlInfo = conn.Query(@"SELECT IsAutoAudit FROM Tb_AuditControl_BBS 
                                WHERE CorpID=@CorpID", new { CorpID = tb_Community.CorpID }).FirstOrDefault();

                    if (controlInfo != null)
                    {
                        isAutoAudit = Convert.ToInt32(controlInfo.IsAutoAudit);
                    }

                    conn.Execute(@"INSERT INTO Tb_BBS_Circle(ID, CommunityId, TypeID, UserID, Title, Content, Images, IsTop, IsDelete, IsAudit) VALUES(newid(), @CommunityId, @TypeID, @UserID, @Title, @Content, @Images, 0, 0, @IsAudit)",
                                    new
                                    {
                                        CommunityId = communityId,
                                        UserID = userID,
                                        TypeID = typeID,
                                        Title = title,
                                        Content = content,
                                        Images = images,
                                        IsAudit = isAutoAudit,
                                    });
                }
                else
                {
                    conn.Execute(@"UPDATE Tb_BBS_Circle SET CommunityId=@CommunityId, TypeID=@TypeID, Title=@Title, Content=@Content, Images=@Images
                                     WHERE ID=@ID",
                                     new
                                     {
                                         CommunityId = communityId,
                                         TypeID = typeID,
                                         Title = title,
                                         Content = content,
                                         Images = images,
                                         ID = id
                                     });
                }

                return new ApiResult(true, "保存成功").toJson();
            }
        }

        /// <summary>
        /// 删除圈子帖子
        /// </summary>
        private string DeleteCircleInfo(DataRow row)
        {
            if (!row.Table.Columns.Contains("ID") || string.IsNullOrEmpty(row["ID"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "帖子Id不能为空");
            }
            if (!row.Table.Columns.Contains("UserID") || string.IsNullOrEmpty(row["UserID"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "用户Id不能为空");
            }

            string ID = row["ID"].ToString();
            string UserID = row["UserID"].ToString();

            using (IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                int i = conn.Execute("UPDATE Tb_BBS_Circle SET IsDelete=1 WHERE ID=@ID AND UserID=@UserID", new { ID = ID, UserID = UserID });

                if (i > 0)
                {
                    return JSONHelper.FromString(true, "删除成功");
                }
                return JSONHelper.FromString(true, "删除失败");
            }
        }

        /// <summary>
        /// 获取二手市场类型
        /// </summary>
        private string GetSecondHandMarketType(DataRow row)
        {
            if (!row.Table.Columns.Contains("CorpID") || string.IsNullOrEmpty(row["CorpID"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "公司Id不能为空");
            }

            using (IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                IEnumerable<dynamic> resultSet = conn.Query(@"SELECT ID,MarketName,MarketImage,SortIndex FROM Tb_BBS_SecondHandMarket_Type 
                                                                WHERE CorpID=@CorpID AND IsDelete=0", new { CorpID = row["CorpID"].ToString() });

                return new ApiResult(true, resultSet).toJson();
            }
        }

        /// <summary>
        /// 获取二手信息列表
        /// </summary>
        private string GetSecondHandMarketList(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "小区Id不能为空");
            }

            if (!row.Table.Columns.Contains("PageSize") || string.IsNullOrEmpty(row["PageSize"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "页长不能为空");
            }
            if (!row.Table.Columns.Contains("PageIndex") || string.IsNullOrEmpty(row["PageIndex"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "页码不能为空");
            }

            string communityId = row["CommunityId"].ToString();
            int pageSize = AppGlobal.StrToInt(row["PageSize"].ToString());
            int pageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());
            string typeId = null;
            int unshelve = 0;
            int soldOut = 0;
            if (row.Table.Columns.Contains("TypeID") && !string.IsNullOrEmpty(row["TypeID"].ToString()))
            {
                typeId = row["TypeID"].ToString();
            }
            if (row.Table.Columns.Contains("Unshelve") && !string.IsNullOrEmpty(row["Unshelve"].ToString()))
            {
                unshelve = AppGlobal.StrToInt(row["Unshelve"].ToString());
            }
            if (row.Table.Columns.Contains("SoldOut") && !string.IsNullOrEmpty(row["SoldOut"].ToString()))
            {
                soldOut = AppGlobal.StrToInt(row["SoldOut"].ToString());
            }

            string sql = string.Format(@"SELECT ID,UserID,UserName,UserPic,Title,Quality,Price,Images,PropertyAuth,Unshelve,IsSoldOut,
                                            LastEditDate FROM View_BBS_SecondHandMarket_List_Phone 
                                            WHERE IsDelete=0 AND PropertyAllowShow=1 AND Unshelve={0} AND IsSoldOut={1} 
                                                AND CommunityId='{2}' {3}",
                                               unshelve, soldOut, communityId,
                                                (string.IsNullOrEmpty(typeId) ? "" : "AND TypeID='" + typeId + "'"));

            DataTable dataTable = GetList(out int pageCount, out int count, sql, pageIndex, pageSize, "LastEditDate", 1, "ID", PubConstant.UnifiedContionString).Tables[0];

            string result = JSONHelper.FromString(dataTable);
            result = result.Insert(result.Length - 1, ",\"PageCount\":" + pageCount);
            return result;
        }

        /// <summary>
        /// 获取置顶二手信息
        /// </summary>
        private string GetSecondHandMarketRecommend(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "小区Id不能为空");
            }

            using (IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                IEnumerable<dynamic> resultSet = conn.Query("Proc_BBS_SecondHandMarket_Recommend_Phone",
                    new { CommunityId = row["CommunityId"].ToString() }, null, false, null, CommandType.StoredProcedure);

                return new ApiResult(true, resultSet).toJson();
            }
        }

        /// <summary>
        /// 获取二手信息详情
        /// </summary>
        private string GetSecondHandMarketDetail(DataRow row)
        {
            if (!row.Table.Columns.Contains("ID") || string.IsNullOrEmpty(row["ID"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "二手信息Id不能为空");
            }
            if (!row.Table.Columns.Contains("UserID") || string.IsNullOrEmpty(row["UserID"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "用户Id不能为空");
            }
            string userId = row["UserID"].ToString();

            using (IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                dynamic info = conn.Query(@"SELECT ID,CommunityID,CommName,MarketName,OriginalPrice,LinkPhone,Content,PubDate,
                                            (SELECT count(0) FROM Tb_BBS_Agree WHERE InfoID=@ID AND UserID=@UserID) AS IsAgree 
                                            FROM View_BBS_SecondHandMarket_List_Phone WHERE ID=@ID",
                                    new { ID = row["ID"].ToString(), UserID = userId, }).FirstOrDefault();

                conn.Execute("Proc_BBS_ReadRecord_Add_Phone", new { UserID = userId, InfoID = row["ID"].ToString() }, null, null, CommandType.StoredProcedure);
                return new ApiResult(true, info).toJson();
            }
        }

        /// <summary>
        /// 获取我发布的二手信息列表
        /// </summary>
        private string GetMyPubSecondHandMarketList(DataRow row)
        {
            if (!row.Table.Columns.Contains("UserID") || string.IsNullOrEmpty(row["UserID"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "用户id不能为空");
            }
            if (!row.Table.Columns.Contains("PageSize") || string.IsNullOrEmpty(row["PageSize"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "页长不能为空");
            }
            if (!row.Table.Columns.Contains("PageIndex") || string.IsNullOrEmpty(row["PageIndex"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "页码不能为空");
            }

            string userID = row["UserID"].ToString();
            int pageSize = AppGlobal.StrToInt(row["PageSize"].ToString());
            int pageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());

            string sql = string.Format(@"SELECT ID,TypeID,MarketName,Title,Quality,Price,Images,PropertyAuth,PropertyAllowShow,Unshelve,IsSoldOut,LastEditDate 
                            FROM View_BBS_SecondHandMarket_List_Phone WHERE IsDelete=0 AND UserID='{0}'", userID);

            DataTable dataTable = GetList(out int pageCount, out int count, sql, pageIndex, pageSize, "LastEditDate", 1, "ID", PubConstant.UnifiedContionString).Tables[0];

            string result = JSONHelper.FromString(dataTable);
            result = result.Insert(result.Length - 1, ",\"PageCount\":" + pageCount);
            return result;
        }

        /// <summary>
        /// 保存二手信息
        /// </summary>
        private string SaveSecondHandMarketInfo(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "小区Id不能为空");
            }
            if (!row.Table.Columns.Contains("UserID") || string.IsNullOrEmpty(row["UserID"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "用户Id不能为空");
            }
            if (!row.Table.Columns.Contains("LinkPhone") || string.IsNullOrEmpty(row["LinkPhone"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "联系电话不能为空");
            }
            if (!row.Table.Columns.Contains("TypeID") || string.IsNullOrEmpty(row["TypeID"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "二手类型不能为空");
            }
            if (!row.Table.Columns.Contains("Title") || string.IsNullOrEmpty(row["Title"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "帖子标题不能为空");
            }
            if (!row.Table.Columns.Contains("Content") || string.IsNullOrEmpty(row["Content"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "帖子内容不能为空");
            }
            if (!row.Table.Columns.Contains("Quality") || string.IsNullOrEmpty(row["Quality"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "新旧程度不能为空");
            }
            if (!row.Table.Columns.Contains("OriginalPrice") || string.IsNullOrEmpty(row["OriginalPrice"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "商品原价不能为空");
            }
            if (!row.Table.Columns.Contains("Price") || string.IsNullOrEmpty(row["Price"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "二手售价不能为空");
            }

            string communityId = row["CommunityId"].ToString();
            string linkPhone = row["LinkPhone"].ToString();
            string userID = row["UserID"].ToString();
            string typeID = row["TypeID"].ToString();
            string title = row["Title"].ToString();
            string content = row["Content"].ToString();
            string quality = row["Quality"].ToString();
            decimal originalPrice = AppGlobal.StrToDec(row["OriginalPrice"].ToString());
            decimal price = AppGlobal.StrToDec(row["Price"].ToString());

            if (IsInBlackList(userID))
            {
                return JSONHelper.FromString(false, "操作失败，无权限");
            }

            string images = null;
            string id = null;
            int unshelve = 0;
            int soldOut = 0;

            if (row.Table.Columns.Contains("Images") && !string.IsNullOrEmpty(row["Images"].ToString()))
            {
                images = row["Images"].ToString();
            }
            if (row.Table.Columns.Contains("ID") && !string.IsNullOrEmpty(row["ID"].ToString()))
            {
                id = row["ID"].ToString();
            }
            if (row.Table.Columns.Contains("Unshelve") && !string.IsNullOrEmpty(row["Unshelve"].ToString()))
            {
                unshelve = AppGlobal.StrToInt(row["Unshelve"].ToString());
            }
            if (row.Table.Columns.Contains("SoldOut") && !string.IsNullOrEmpty(row["SoldOut"].ToString()))
            {
                soldOut = AppGlobal.StrToInt(row["SoldOut"].ToString());
            }

            // 获取数据库连接字符串
            Tb_Community tb_Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(communityId);
            if (null == tb_Community)
            {
                return new ApiResult(false, "该小区不存在").toJson();
            }

            using (IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                if (string.IsNullOrEmpty(id))
                {
                    int isAutoAudit = 1;

                    dynamic controlInfo = conn.Query(@"SELECT IsAutoAudit FROM Tb_AuditControl_BBS 
                                WHERE CorpID=@CorpID", new { CorpID = tb_Community.CorpID }).FirstOrDefault();

                    if (controlInfo != null)
                    {
                        isAutoAudit = Convert.ToInt32(controlInfo.IsAutoAudit);
                    }

                    conn.Execute(@"INSERT INTO Tb_BBS_SecondHandMarket(ID, CommunityId, TypeID, UserID, LinkPhone, Title, Content, Images, Quality,
                                   OriginalPrice, Price, PubDate, LastEditDate, Unshelve, IsSoldOut, PropertyAuth, PropertyAllowShow, IsDelete)
                                  VALUES(newid(),@CommunityId, @TypeID, @UserID, @LinkPhone, @Title, @Content, @Images, @Quality, @OriginalPrice,
                                    @Price, getdate(), getdate(), @Unshelve, 0, 0, @PropertyAllowShow, 0)",
                                    new
                                    {
                                        CommunityId = communityId,
                                        UserID = userID,
                                        TypeID = typeID,
                                        LinkPhone = linkPhone,
                                        Title = title,
                                        Content = content,
                                        Images = images,
                                        Quality = quality,
                                        OriginalPrice = originalPrice,
                                        Price = price,
                                        Unshelve = unshelve,
                                        PropertyAllowShow = isAutoAudit
                                    });
                }
                else
                {
                    string sql = @"UPDATE Tb_BBS_SecondHandMarket SET CommunityId=@CommunityId, TypeID=@TypeID, LinkPhone=@LinkPhone, Title=@Title, 
                                    Content=@Content, Images=@Images, Quality=@Quality, OriginalPrice=@OriginalPrice, Price=@Price, LastEditDate=getdate(), Unshelve=@Unshelve, IsSoldOut=@IsSoldOut  
                                     WHERE ID=@ID";

                    if (string.IsNullOrEmpty(typeID))
                    {
                        sql = @"UPDATE Tb_BBS_SecondHandMarket SET CommunityId=@CommunityId, LinkPhone=@LinkPhone, Title=@Title, 
                                    Content=@Content, Images=@Images, Quality=@Quality, OriginalPrice=@OriginalPrice, Price=@Price, LastEditDate=getdate(), Unshelve=@Unshelve, IsSoldOut=@IsSoldOut  
                                     WHERE ID=@ID";
                    }

                    conn.Execute(sql, new
                    {
                        CommunityId = communityId,
                        TypeID = typeID,
                        LinkPhone = linkPhone,
                        Title = title,
                        Content = content,
                        Images = images,
                        Quality = quality,
                        OriginalPrice = originalPrice,
                        Price = price,
                        Unshelve = unshelve,
                        IsSoldOut = soldOut,
                        ID = id
                    });
                }

                return new ApiResult(true, "保存成功").toJson();
            }
        }

        /// <summary>
        /// 删除二手信息
        /// </summary>
        private string DeleteSecondHandMarketInfo(DataRow row)
        {
            if (!row.Table.Columns.Contains("ID") || string.IsNullOrEmpty(row["ID"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "帖子Id不能为空");
            }
            if (!row.Table.Columns.Contains("UserID") || string.IsNullOrEmpty(row["UserID"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "用户Id不能为空");
            }

            string ID = row["ID"].ToString();
            string UserID = row["UserID"].ToString();

            using (IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                int i = conn.Execute("UPDATE Tb_BBS_SecondHandMarket SET IsDelete=1 WHERE ID=@ID AND UserID=@UserID", new { ID = ID, UserID = UserID });

                if (i > 0)
                {
                    return JSONHelper.FromString(true, "删除成功");
                }
                return JSONHelper.FromString(true, "删除失败");
            }
        }

        /// <summary>
        /// 获取评论列表
        /// </summary>
        private string GetCommentList(DataRow row)
        {
            if (!row.Table.Columns.Contains("UserID") || string.IsNullOrEmpty(row["UserID"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "用户Id不能为空");
            }
            if (!row.Table.Columns.Contains("InfoID") || string.IsNullOrEmpty(row["InfoID"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "帖子Id不能为空");
            }
            if (!row.Table.Columns.Contains("Type") || string.IsNullOrEmpty(row["Type"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "帖子类型不能为空");
            }
            if (!row.Table.Columns.Contains("PageSize") || string.IsNullOrEmpty(row["PageSize"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "页长不能为空");
            }
            if (!row.Table.Columns.Contains("PageIndex") || string.IsNullOrEmpty(row["PageIndex"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "页码不能为空");
            }

            string userID = row["UserID"].ToString();
            string infoID = row["InfoID"].ToString();
            int type = AppGlobal.StrToInt(row["Type"].ToString());
            int pageSize = AppGlobal.StrToInt(row["PageSize"].ToString());
            int pageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());

            string sql = string.Format(@"SELECT a.ID,a.Content,a.FollowCount,a.AgreeCount,a.PubDate,a.UserID,a.UserName,a.UserPic,
                                          (SELECT count(0) FROM Tb_BBS_Comment_Agree WHERE CommentID=a.ID AND UserID='{0}') AS IsAgree  
                                         FROM View_BBS_Comment_List_Phone AS a WHERE InfoID='{1}' AND Type={2} AND IsDelete=0", userID, infoID, type);

            DataTable dataTable = GetList(out int pageCount, out int count, sql, pageIndex, pageSize, "PubDate", 1, "ID", PubConstant.UnifiedContionString).Tables[0];

            int commentCount = 0;
            using (IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                commentCount = conn.Query<int>(@"SELECT count(0) FROM Tb_BBS_Comment WHERE IsDelete=0 AND InfoID=@InfoID",
                    new { InfoID = infoID }).FirstOrDefault();
            }

            string result = JSONHelper.FromString(dataTable);
            result = result.Insert(result.Length - 1, ",\"PageCount\":" + pageCount + ",\"CommentCount\":" + commentCount);
            return result;
        }

        /// <summary>
        /// 获取跟评列表
        /// </summary>
        private string GetCommentFollowList(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommentID") || string.IsNullOrEmpty(row["CommentID"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "评论Id不能为空");
            }
            if (!row.Table.Columns.Contains("PageSize") || string.IsNullOrEmpty(row["PageSize"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "页长不能为空");
            }
            if (!row.Table.Columns.Contains("PageIndex") || string.IsNullOrEmpty(row["PageIndex"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "页码不能为空");
            }

            string commentID = row["CommentID"].ToString();
            int pageSize = AppGlobal.StrToInt(row["PageSize"].ToString());
            int pageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());

            string sql = string.Format(@"SELECT ID,Content,PubDate,UserID,UserName,UserPic FROM View_BBS_Comment_Follow_List_Phone 
                                            WHERE CommentID='{0}' AND IsDelete=0", commentID);

            DataTable dataTable = GetList(out int pageCount, out int count, sql, pageIndex, pageSize, "PubDate", 1, "ID", PubConstant.UnifiedContionString).Tables[0];

            string result = JSONHelper.FromString(dataTable);
            result = result.Insert(result.Length - 1, ",\"PageCount\":" + pageCount);
            return result;
        }

        /// <summary>
        /// 添加评论
        /// </summary>
        private string AddComment(DataRow row)
        {
            if (!row.Table.Columns.Contains("UserID") || string.IsNullOrEmpty(row["UserID"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "用户Id不能为空");
            }
            if (!row.Table.Columns.Contains("Content") || string.IsNullOrEmpty(row["Content"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "评论内容不能为空");
            }

            string userId = row["UserID"].ToString();
            string content = row["Content"].ToString();

            if (IsInBlackList(userId))
            {
                return JSONHelper.FromString(false, "操作失败，无权限");
            }

            int type = 0;
            string infoId = null;
            string commentId = null;

            if (row.Table.Columns.Contains("InfoID") && !string.IsNullOrEmpty(row["InfoID"].ToString()) &&
                row.Table.Columns.Contains("Type") && !string.IsNullOrEmpty(row["Type"].ToString()))
            {
                infoId = row["InfoID"].ToString();
                type = AppGlobal.StrToInt(row["Type"].ToString());
            }
            if (row.Table.Columns.Contains("CommentID") && !string.IsNullOrEmpty(row["CommentID"].ToString()))
            {
                commentId = row["CommentID"].ToString();
            }

            using (IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                if (!string.IsNullOrEmpty(infoId))
                {
                    conn.Execute(@"INSERT INTO Tb_BBS_Comment(ID, Type, UserID, InfoID, Content, PubDate, IsDelete) 
                                VALUES(newid(), @Type, @UserID, @InfoID, @Content, getdate(), 0)",
                                new { Type = type, UserID = userId, InfoID = infoId, Content = content });
                }
                if (!string.IsNullOrEmpty(commentId))
                {
                    conn.Execute(@"INSERT INTO Tb_BBS_Comment_Follow(ID, UserID, CommentID, Content, PubDate, IsDelete) 
                                VALUES(newid(), @UserID, @CommentID, @Content, getdate(), 0)",
                                new { UserID = userId, CommentID = commentId, Content = content });
                }

                string userMobile = conn.Query<string>(@"SELECT Mobile FROM Tb_USER WHERE Id=@Id", new { Id = userId }).FirstOrDefault();
                string tw2bsConnectionString = PubConstant.tw2bsConnectionString;
                string hmWyglConnectionString = PubConstant.hmWyglConnectionString;
                string corpId = Global_Var.CorpId;

                Task.Run(() =>
                {
                    if (Common.Push.GetAppKeyAndAppSecret(tw2bsConnectionString, corpId, out string appIdentifier, out string appKey, out string appSecret, true))
                    {
                        string message = @"您的帖子有新回复内容啦~，点击查看详情";

                        if (string.IsNullOrEmpty(infoId))
                        {
                            message = @"您的回复有新评论啦~，点击查看详情";
                        }

                        // 通知业主有用户绑定了房屋
                        PushModel pushModel = new PushModel(appKey, appSecret)
                        {
                            AppIdentifier = appIdentifier,
                            Badge = 1,
                            KeyInfomation = "BBS_Reply",
                            Message = message,
                            Command = PushCommand.BBS_REPLY
                        };

                        pushModel.Audience.Category = PushAudienceCategory.Alias;
                        pushModel.Audience.Objects.Add(userMobile);

                        pushModel.Audience.SecondObjects.Category = PushAudienceSecondCategory.TagsAnd;
                        pushModel.Audience.SecondObjects.Objects.Add(PushCommand.BBS_REPLY);

                        TWTools.Push.Push.SendAsync(pushModel);
                    };
                });

                return JSONHelper.FromString(true, "评论成功");
            }
        }

        /// <summary>
        /// 删除评论
        /// </summary>
        private string DeleteComment(DataRow row)
        {
            if (!row.Table.Columns.Contains("UserID") || string.IsNullOrEmpty(row["UserID"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "用户Id不能为空");
            }
            if (!row.Table.Columns.Contains("ID") || string.IsNullOrEmpty(row["ID"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "评论id不能为空");
            }
            if (!row.Table.Columns.Contains("IsFollow") || string.IsNullOrEmpty(row["IsFollow"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "IsFollow不能为空");
            }

            string userId = row["UserID"].ToString();
            string ID = row["ID"].ToString();
            int IsFollow = AppGlobal.StrToInt(row["IsFollow"].ToString()); ;

            using (IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                if (IsFollow == 1)
                {
                    conn.Execute(@"UPDATE Tb_BBS_Comment_Follow SET IsDelete=1 WHERE ID=@ID AND UserID=@UserID",
                                new { ID = ID, UserID = userId });
                }
                else
                {
                    conn.Execute(@"UPDATE Tb_BBS_Comment SET IsDelete=1 WHERE ID=@ID AND UserID=@UserID",
                                new { ID = ID, UserID = userId });
                }

                return JSONHelper.FromString(true, "删除成功");
            }
        }

        /// <summary>
        /// 评论点赞
        /// </summary>
        private string AgreeComment(DataRow row)
        {
            if (!row.Table.Columns.Contains("UserID") || string.IsNullOrEmpty(row["UserID"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "用户Id不能为空");
            }
            if (!row.Table.Columns.Contains("CommentID") || string.IsNullOrEmpty(row["CommentID"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "评论Id不能为空");
            }
            if (!row.Table.Columns.Contains("IsAgree") || string.IsNullOrEmpty(row["IsAgree"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "IsAgree不能为空");
            }

            string userId = row["UserID"].ToString();
            string commentID = row["CommentID"].ToString();
            int isagree = AppGlobal.StrToInt(row["IsAgree"].ToString());

            using (IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                conn.Execute("Proc_BBS_Comment_Agree_Add_Phone", new { UserID = userId, CommentID = commentID, IsAgree = isagree },
                    null, null, CommandType.StoredProcedure);

                return JSONHelper.FromString(true, "操作成功");

            }
        }

        /// <summary>
        /// 帖子点赞
        /// </summary>
        private string AgreeInfo(DataRow row)
        {
            if (!row.Table.Columns.Contains("UserID") || string.IsNullOrEmpty(row["UserID"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "用户Id不能为空");
            }
            if (!row.Table.Columns.Contains("InfoID") || string.IsNullOrEmpty(row["InfoID"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "评论Id不能为空");
            }
            if (!row.Table.Columns.Contains("IsAgree") || string.IsNullOrEmpty(row["IsAgree"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "IsAgree不能为空");
            }

            string userId = row["UserID"].ToString();
            string infoID = row["InfoID"].ToString();
            int isagree = AppGlobal.StrToInt(row["IsAgree"].ToString());

            using (IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                conn.Execute("Proc_BBS_Agree_Add_Phone", new { UserID = userId, InfoID = infoID, IsAgree = isagree },
                    null, null, CommandType.StoredProcedure);

                return JSONHelper.FromString(true, "操作成功");
            }
        }

        /// <summary>
        /// 获取信息评论、点赞、浏览量统计信息
        /// </summary>
        private string GetInfoStatistics(DataRow row)
        {
            if (!row.Table.Columns.Contains("UserID") || string.IsNullOrEmpty(row["UserID"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "信息Id不能为空");
            }
            if (!row.Table.Columns.Contains("InfoID") || string.IsNullOrEmpty(row["InfoID"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "信息Id不能为空");
            }

            string userId = row["UserID"].ToString();
            string infoId = row["InfoID"].ToString();

            using (IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                GridReader reader = conn.QueryMultiple(@"SELECT count(0) FROM Tb_BBS_Comment WHERE IsDelete=0 AND InfoID=@InfoID;
                                                         SELECT count(0) FROM Tb_BBS_Agree WHERE InfoID=@InfoID;
                                                         SELECT isnull(sum(ReadCount),0) FROM Tb_BBS_ReadRecord WHERE InfoID=@InfoID;
                                                         SELECT count(0) FROM Tb_BBS_Agree WHERE InfoID=@InfoID AND UserID=@UserID;",
                                                         new { InfoID = infoId, UserID = userId });

                int commentCount = reader.Read<int>().FirstOrDefault();
                int agreeCount = reader.Read<int>().FirstOrDefault();
                int readCount = reader.Read<int>().FirstOrDefault();
                int agree = reader.Read<int>().FirstOrDefault();

                return new ApiResult(true, new
                {
                    CommentCount = commentCount,
                    AgreeCount = agreeCount,
                    ReadCount = readCount,
                    IsAgree = agree
                }).toJson();
            }
        }

        /// <summary>
        /// 设置信息为已读
        /// </summary>
        private string SetInfoRead(DataRow row)
        {
            if (!row.Table.Columns.Contains("UserID") || string.IsNullOrEmpty(row["UserID"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "用户Id不能为空");
            }
            if (!row.Table.Columns.Contains("InfoIDs") || string.IsNullOrEmpty(row["InfoIDs"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "信息Id不能为空");
            }

            string userId = row["UserID"].ToString();
            string infoIds = row["InfoIDs"].ToString();

            string[] array = infoIds.Split(',');

            using (IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                foreach (var item in array)
                {
                    conn.Execute("Proc_BBS_ReadRecord_Add_Phone", new { UserID = userId, InfoID = item }, null, null, CommandType.StoredProcedure);
                }
                return JSONHelper.FromString(true, "操作成功");
            }
        }

        /// <summary>
        /// 获取举报类别
        /// </summary>
        private string GetReportType(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityID") || string.IsNullOrEmpty(row["CommunityID"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "小区Id不能为空");
            }

            using (IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                IEnumerable<dynamic> resultSet = conn.Query(@"SELECT ID,TypeName FROM Tb_BBS_Report_Type WHERE IsDelete=0");
                return new ApiResult(true, resultSet).toJson();
            }
        }

        /// <summary>
        /// 保存举报信息
        /// </summary>
        private string SaveReport(DataRow row)
        {
            if (!row.Table.Columns.Contains("InfoID") || string.IsNullOrEmpty(row["InfoID"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "帖子Id不能为空");
            }
            if (!row.Table.Columns.Contains("InfoType") || string.IsNullOrEmpty(row["InfoType"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "帖子类型不能为空");
            }
            if (!row.Table.Columns.Contains("ReportUser") || string.IsNullOrEmpty(row["ReportUser"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "举报人不能为空");
            }
            if (!row.Table.Columns.Contains("ReportType") || string.IsNullOrEmpty(row["ReportType"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "举报类别不能为空");
            }
            if (!row.Table.Columns.Contains("ReportReason") || string.IsNullOrEmpty(row["ReportReason"].ToString()))
            {
                return JSONHelper.FromJsonString(false, "举报理由不能为空");
            }

            string infoId = row["InfoID"].ToString();
            string infoType = row["InfoType"].ToString();
            string reportUser = row["ReportUser"].ToString();
            string reportType = row["ReportType"].ToString();
            string reportReason = row["ReportReason"].ToString();
            string images = null;

            if (IsInBlackList(reportUser))
            {
                return JSONHelper.FromString(false, "操作失败，无权限");
            }

            if (row.Table.Columns.Contains("ReportReason") && !string.IsNullOrEmpty(row["Images"].ToString()))
            {
                images = row["Images"].ToString();
            }

            using (IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                if (conn.Query(@"SELECT * FROM Tb_BBS_Report WHERE InfoID=@InfoID AND ReportUser=@ReportUser", new
                {
                    InfoID = infoId,
                    ReportUser = reportUser
                }).Count() > 0)
                {
                    return JSONHelper.FromString(false, "您已举报该违法内容");
                }

                int result = conn.Execute(@"INSERT INTO Tb_BBS_Report(ID,InfoID,InfoType,ReportUser,ReportDate,ReportType,ReportReason,Images,DealState) 
                                            VALUES(newid(),@InfoID, @InfoType, @ReportUser,getdate(),@ReportType,@ReportReason,@Images,0)",
                                    new
                                    {
                                        InfoID = infoId,
                                        InfoType = infoType,
                                        ReportUser = reportUser,
                                        ReportType = reportType,
                                        ReportReason = reportReason,
                                        Images = images
                                    });
                if (result > 0)
                {
                    return JSONHelper.FromString(true, "举报成功");
                }
                return JSONHelper.FromString(false, "举报失败");
            }
        }

        /// <summary>
        /// 判断是否拉入了黑名单
        /// </summary>
        private bool IsInBlackList(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return false;
            }

            using (IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                return conn.Query(@"SELECT * FROM Tb_BBS_Blacklist WHERE UserID=@UserID AND Locked=1", new { UserID = userId }).Count() > 0;
            }
        }
    }
}
