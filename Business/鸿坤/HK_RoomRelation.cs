using Dapper;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using MobileSoft.Model.Unified;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Business
{
    public class HK_RoomRelation : PubInfo
    {
        internal static class InviteeUserIdentity
        {
            /// <summary>
            /// 夫妻
            /// </summary>
            public static readonly string Couple = "0041";

            /// <summary>
            /// 父母
            /// </summary>
            public static readonly string Parents = "0042";

            /// <summary>
            /// 子女
            /// </summary>
            public static readonly string Children = "0043";

            /// <summary>
            /// 亲友
            /// </summary>
            public static readonly string Friends = "0044";

            /// <summary>
            /// 租户
            /// </summary>
            public static readonly string Tenant = "0045";

            public static string GetIdentityName(string identityName)
            {
                if (identityName == InviteeUserIdentity.Couple) return "App邀请(夫妻)";
                else if (identityName == InviteeUserIdentity.Parents) return "App邀请(父母)";
                else if (identityName == InviteeUserIdentity.Children) return "App邀请(子女)";
                else if (identityName == InviteeUserIdentity.Friends) return "App邀请(亲友)";
                else if (identityName == InviteeUserIdentity.Tenant) return "App邀请(租户)";
                else return "App邀请(未知身份)";
            }
        }

        public HK_RoomRelation()
        {
            base.Token = "20181128HK_RoomRelation";
        }

        public override void Operate(ref Common.Transfer Trans)
        {
            Trans.Result = JSONHelper.FromString(false, "未知错误");

            DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
            DataRow Row = dAttributeTable.Rows[0];

            switch (Trans.Command)
            {
                case "InviteUser":          // 邀请用户
                    Trans.Result = InviteUser(Row);
                    break;
                case "Unbinding":           // 房屋解绑
                    Trans.Result = Unbinding(Row);
                    break;
                case "BindingState":        // 房屋绑定状态
                    Trans.Result = BindingState(Row);
                    break;
                case "GetSmsCode_ManualBinding":    // 手动绑定房屋，获取短信验证码
                    Trans.Result = GetSmsCode_ManualBinding(Row);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 邀请用户
        /// </summary>
        private string InviteUser(DataRow Row)
        {
            if (!Row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(Row["CommunityId"].ToString()))
            {
                return JSONHelper.FromString(false, "缺少CommunityId参数");
            }
            if (!Row.Table.Columns.Contains("UserMobile") || string.IsNullOrEmpty(Row["UserMobile"].ToString()))
            {
                return JSONHelper.FromString(false, "缺少UserMobile参数");
            }
            if (!Row.Table.Columns.Contains("RelationId") || string.IsNullOrEmpty(Row["RelationId"].ToString()))
            {
                return JSONHelper.FromString(false, "缺少RelationId参数");
            }
            if (!Row.Table.Columns.Contains("InviteMobile") || string.IsNullOrEmpty(Row["InviteMobile"].ToString()))
            {
                return JSONHelper.FromString(false, "缺少InviteMobile参数");
            }
            if (!Row.Table.Columns.Contains("InviteName") || string.IsNullOrEmpty(Row["InviteName"].ToString()))
            {
                return JSONHelper.FromString(false, "缺少InviteName参数");
            }
            if (!Row.Table.Columns.Contains("InviteType") || string.IsNullOrEmpty(Row["InviteType"].ToString()))
            {
                return JSONHelper.FromString(false, "缺少InviteType参数");
            }

            string communityId = Row["CommunityId"].AsString();
            string userMobile = Row["UserMobile"].ToString();
            string relationId = Row["RelationId"].ToString();
            string inviteMobile = Row["InviteMobile"].ToString();
            string inviteName = Row["InviteName"].ToString();
            string inviteType = Row["InviteType"].ToString();
            string memo = InviteeUserIdentity.GetIdentityName(inviteType);

            //查询小区
            Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(communityId);
            //构造链接字符串
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }

            PubConstant.hmWyglConnectionString = GetConnectionStringStr(Community);

            string sql, message, newUserId, newRelationId = string.Empty;

            using (IDbConnection appConn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                using (IDbConnection erpConn = new SqlConnection(PubConstant.hmWyglConnectionString))
                {
                    // App数据库事物
                    if (appConn.State == ConnectionState.Closed)
                    {
                        appConn.Open();
                    }
                    var appTrans = appConn.BeginTransaction();

                    try
                    {
                        sql = @"SELECT CommName FROM Tb_Community WHERE Id=@Id";
                        string commName = appConn.Query<string>(sql, new { Id = communityId }, appTrans).FirstOrDefault();

                        sql = @"SELECT CustId,RoomId,RoomSign,CustName FROM Tb_User_Relation WHERE Id=@RelationId";
                        dynamic relationInfo = appConn.Query(sql, new { RelationId = relationId }, appTrans).FirstOrDefault();
                        string custId = relationInfo.CustId;
                        string roomId = relationInfo.RoomId;
                        string roomSign = relationInfo.RoomSign;
                        string custName = relationInfo.CustName;
                        long holdId = 0;
                        bool needAddHold = false;       // 是否需要添加家庭成员信息
                        bool needUpdateHold = false;    // 是否需要更新家庭成员信息

                        // 1、查询被邀者的手机号是否已经注册
                        sql = "SELECT top 1 Id FROM Tb_User WHERE Mobile=@Mobile";
                        string userId = appConn.Query<string>(sql, new { Mobile = inviteMobile }, appTrans).FirstOrDefault();

                        // 手机号已注册
                        if (!string.IsNullOrEmpty(userId))
                        {
                            newUserId = userId;

                            // 1.2、查询被邀者是否已经绑定该房屋
                            sql = @"SELECT RoomSign,isnull(CustHoldId,0) AS CustHoldId FROM Tb_User_Relation WHERE UserId=@UserId AND RoomId IN 
                                    (SELECT RoomId FROM Tb_User_Relation WHERE Id=@Relation)";
                            var userRelation = appConn.Query(sql, new { UserId = newUserId, Relation = relationId }, appTrans).FirstOrDefault();

                            // 该已绑定该房屋
                            if (userRelation != null)
                            {
                                // 已经绑定但锁定则解锁
                                appConn.Execute(@"UPDATE Tb_User_Relation SET Locked=0 WHERE UserId=@UserId AND RoomId IN 
                                                    (SELECT RoomId FROM Tb_User_Relation WHERE Id=@Relation)",
                                                    new { UserId = newUserId, Relation = relationId }, appTrans);

                                // 家庭成员id
                                holdId = long.Parse((userRelation.CustHoldId == null ? 0 : userRelation.CustHoldId));

                                // 无家庭成员信息
                                if (holdId == 0)
                                {
                                    needAddHold = true;
                                }
                                else
                                {
                                    needUpdateHold = true;
                                }
                            }
                            // 未绑定该房屋
                            else
                            {
                                needAddHold = true;

                                // 1.3、直接给被邀者绑定该房屋
                                newRelationId = Guid.NewGuid().ToString();

                                // 用户绑定房屋
                                appConn.Execute(@"INSERT INTO Tb_User_Relation(Id,UserId,CommunityId,CustId,RoomId,RegDate,CustName,RoomSign,CustMobile,Locked) 
                                                    VALUES(@Id,@UserId,@CommunityId,@CustId,@RoomId,getdate(),@CustName,@RoomSign,@CustMobile,0)",
                                                   new
                                                   {
                                                       Id = newRelationId,
                                                       UserId = newUserId,
                                                       CommunityId = communityId,
                                                       CustId = custId,
                                                       RoomId = roomId,
                                                       CustName = custName,
                                                       RoomSign = roomSign,
                                                       CustMobile = userMobile
                                                   }, appTrans);
                            }

                            message = string.Format("温馨提示：“{0}”业主{1}已成功为您绑定了：{2}房屋，您可以使用鸿坤荟App对该房屋进行在线报事和开启门禁等操作了。",
                                        commName, custName, roomSign);
                        }
                        else
                        {
                            // 2、被邀者没有注册账号，创建账号并绑定房屋关系
                            newUserId = Guid.NewGuid().ToString();
                            newRelationId = Guid.NewGuid().ToString();

                            // 2.1、创建用户
                            appConn.Execute(@"INSERT INTO Tb_User(Id,Name,Mobile,NickName,Pwd,Sex,RegDate) VALUES(@UserId,@Name,@Mobile,@Name,'123456',1,getdate())",
                                new
                                {
                                    UserId = newUserId,
                                    Name = inviteName,
                                    Mobile = inviteMobile
                                }, appTrans);

                            // 2.2、用户绑定房屋
                            appConn.Execute(@"INSERT INTO Tb_User_Relation(Id,UserId,CommunityId,CustId,RoomId,RegDate,CustName,RoomSign,CustMobile,Locked) 
                                            VALUES(@Id,@UserId,@CommunityId,@CustId,@RoomId,getdate(),@CustName,@RoomSign,@CustMobile,0)",
                                               new
                                               {
                                                   Id = newRelationId,
                                                   UserId = newUserId,
                                                   CommunityId = communityId,
                                                   CustId = custId,
                                                   RoomId = roomId,
                                                   CustName = custName,
                                                   RoomSign = roomSign,
                                                   CustMobile = userMobile
                                               }, appTrans);

                            message = string.Format("温馨提示：“{0}”业主{1}已成功为您绑定了：{2}房屋，APP下载链接为：http://t.cn/ELN6tEr，登录账号：{3}，初始密码：123456，您可以使用鸿坤荟App对该房屋进行在线报事和开启门禁等操作了。",
                                        commName, custName, roomSign, inviteMobile);
                        }

                        // 添加家庭程序信息
                        if (needAddHold)
                        {
                            DynamicParameters parameters = new DynamicParameters();
                            parameters.Add("@HoldID", 0, DbType.Int64, ParameterDirection.Output);
                            parameters.Add("@CommID", Community.CommID);
                            parameters.Add("@CustID", custId);
                            parameters.Add("@RoomID", roomId);
                            parameters.Add("@Name", inviteName);
                            parameters.Add("@MobilePhone", inviteMobile);
                            parameters.Add("@Relationship", inviteType);

                            erpConn.Execute("Proc_HSPR_Household_Insert_Phone", parameters, null, null, CommandType.StoredProcedure);
                            long custHoldId = parameters.Get<long>("@HoldID");

                            appConn.Execute(@"UPDATE Tb_User_Relation SET CustHoldId=@HoldID WHERE Id=@RelationId",
                                new { HoldID = custHoldId, RelationId = newRelationId }, appTrans);
                        }

                        // 更新家庭成员信息
                        if (needUpdateHold)
                        {
                            erpConn.Execute(@"UPDATE Tb_HSPR_Household SET IsDelete=0,Relationship=@Relationship,MemberName=@Name,Linkman=@Name,Name=@Name,
                                                Surname=@Name,CustID=@CustID,RoomID=@RoomID,Memo=@Memo,MobilePhone=@MobilePhone,LinkManTel=@MobilePhone
                                                WHERE HoldID=@HoldID",
                                                new
                                                {
                                                    HoldID = holdId,
                                                    Relationship = inviteType,
                                                    Name = inviteName,
                                                    CustID = custId,
                                                    RoomID = roomId,
                                                    Memo = memo,
                                                    MobilePhone = inviteMobile
                                                });
                        }

                        appTrans.Commit();

                        // 发送短信
                        SendShortMessage(inviteMobile, message, out string errorMessage, Community.CorpID);

                        return JSONHelper.FromString(true, "邀请成功");
                    }
                    catch (Exception ex)
                    {
                        appTrans.Rollback();
                        return JSONHelper.FromString(false, ex.Message + Environment.NewLine + ex.StackTrace);
                    }
                }
            }
        }

        /// <summary>
        /// 房屋解绑
        /// </summary>
        private string Unbinding(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            if (!row.Table.Columns.Contains("RelationID") || string.IsNullOrEmpty(row["RelationID"].ToString()))
            {
                return JSONHelper.FromString(false, "绑定编号不能为空");
            }

            string communityId = row["CommunityId"].AsString();
            string relationID = row["RelationID"].AsString();

            //查询小区
            Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(communityId);
            //构造链接字符串
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }
            PubConstant.hmWyglConnectionString = GetConnectionStringStr(Community);

            using (IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                if (conn.Execute(@"UPDATE Tb_User_Relation SET Locked=1 WHERE Id=@Id", new { Id = relationID }) > 0)
                {
                    dynamic holdInfo = conn.Query(@"SELECT CustHoldId,(SELECT Mobile FROM Tb_User x WHERE x.Id=UserId) AS Mobile
                                                FROM Tb_User_Relation WHERE Id=@Id", new { Id = relationID }).FirstOrDefault();

                    // 家庭成员存在
                    if (holdInfo.CustHoldId != null && !string.IsNullOrEmpty(holdInfo.CustHoldId.ToString()) && holdInfo.CustHoldId.ToString() != "0")
                    {
                        using (IDbConnection erpConn = new SqlConnection(PubConstant.hmWyglConnectionString))
                        {
                            erpConn.Execute("UPDATE Tb_HSPR_Household SET IsDelete=1 WHERE HoldID=@HoldID",
                                new { HoldID = holdInfo.CustHoldId });
                        }
                    }

                    return JSONHelper.FromString(true, "解绑成功");
                }

                return JSONHelper.FromString(false, "解绑失败");
            }
        }

        /// <summary>
        /// 房屋绑定状态
        /// </summary>
        private string BindingState(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            if (!row.Table.Columns.Contains("UserId") || string.IsNullOrEmpty(row["UserId"].ToString()))
            {
                return JSONHelper.FromString(false, "用户编号不能为空");
            }

            using (IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                int state = conn.Query(@"SELECT * FROM Tb_User_Relation WHERE UserId=@UserId AND CommunityId=@CommunityId AND isnull(Locked,0)=0",
                    new { UserId = row["UserId"], CommunityId = row["CommunityId"] }).Count();

                return new ApiResult(true, (state > 0 ? 1 : 0)).toJson();
            }
        }

        /// <summary>
        /// 手动绑定房屋，获取短信验证码
        /// </summary>
        private string GetSmsCode_ManualBinding(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            if (!row.Table.Columns.Contains("RoomID") || string.IsNullOrEmpty(row["RoomID"].ToString()))
            {
                return JSONHelper.FromString(false, "房屋编号不能为空");
            }
            if (!row.Table.Columns.Contains("CustMobile") || string.IsNullOrEmpty(row["CustMobile"].ToString()))
            {
                return JSONHelper.FromString(false, "业主手机号不能为空");
            }

            string communityId = row["CommunityId"].AsString();
            string roomID = row["RoomID"].AsString();
            string custMobile = row["CustMobile"].AsString().Trim();

            if (custMobile.Trim().Length != 11)
            {
                return JSONHelper.FromString(false, "手机号格式错误");
            }

            //查询小区
            Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(communityId);
            //构造链接字符串
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }
            PubConstant.hmWyglConnectionString = GetConnectionStringStr(Community);

            using (IDbConnection erpConn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                string sql = @"SELECT TOP 1 isnull(b.MobilePhone,b.LinkmanTel) 
                                FROM Tb_HSPR_CustomerLive a LEFT JOIN Tb_HSPR_Customer b ON a.CustID=b.CustID
                                WHERE a.LiveType=1 AND isnull(a.IsDelLive,0)=0 AND a.RoomID=@RoomID;";

                string mobile = erpConn.Query<string>(sql, new { RoomID = roomID }).FirstOrDefault();

                if (!string.IsNullOrEmpty(mobile))
                {
                    if (!mobile.Contains(custMobile))
                    {
                        return JSONHelper.FromString(false, "提交的业主手机号不匹配");
                    }

                    int code = new Random(unchecked((int)DateTime.Now.Ticks)).Next(100000, 999999);
                    string msg = "您的验证码为：" + code;

                    SendShortMessage(custMobile, msg, out string errorMessage);

                    return new ApiResult(true, code * 369).toJson();
                }

                return JSONHelper.FromString(false, "业主未登记手机号，请联系物管");
            }
        }
    }
}
