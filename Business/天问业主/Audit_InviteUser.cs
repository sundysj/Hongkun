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
    /// <summary>
    /// 房屋绑定审核
    /// </summary>
    public class Audit_InviteUser : PubInfo
    {
        public Audit_InviteUser()
        {
            base.Token = "20180730AuditInviteUser";
        }

        public override void Operate(ref Common.Transfer Trans)
        {
            Trans.Result = JSONHelper.FromString(false, "未知错误");

            DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
            DataRow Row = dAttributeTable.Rows[0];

            switch (Trans.Command)
            {
                case "Add":
                    Trans.Result = Add(Row);
                    break;
                case "Delete":
                    Trans.Result = Delete(Row);
                    break;
                case "Audit":
                    Trans.Result = Audit(Row);
                    break;
                case "WaitingAuditList":
                    Trans.Result = WaitingAuditList(Row);
                    break;
            }
        }

        /// <summary>
        /// 添加审核信息
        /// </summary>
        private string Add(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return JSONHelper.FromString(false, "缺少CommunityId参数");
            }
            if (!row.Table.Columns.Contains("RoomID") || string.IsNullOrEmpty(row["RoomID"].ToString()))
            {
                return JSONHelper.FromString(false, "缺少RoomID参数");
            }
            if (!row.Table.Columns.Contains("ApplicantUserID") || string.IsNullOrEmpty(row["ApplicantUserID"].ToString()))
            {
                return JSONHelper.FromString(false, "缺少ApplicantUserID参数");
            }
            if (!row.Table.Columns.Contains("InviteeName") || string.IsNullOrEmpty(row["InviteeName"].ToString()))
            {
                return JSONHelper.FromString(false, "缺少InviteeName参数");
            }
            if (!row.Table.Columns.Contains("InviteeMobile") || string.IsNullOrEmpty(row["InviteeMobile"].ToString()))
            {
                return JSONHelper.FromString(false, "缺少InviteeMobile参数");
            }
            if (!row.Table.Columns.Contains("InviteeIdentity") || string.IsNullOrEmpty(row["InviteeIdentity"].ToString()))
            {
                return JSONHelper.FromString(false, "缺少InviteeIdentity参数");
            }

            string communityId = row["CommunityId"].AsString();
            string RoomID = row["RoomID"].ToString();
            string ApplicantUserID = row["ApplicantUserID"].ToString();
            string InviteeName = row["InviteeName"].ToString();
            string InviteeMobile = row["InviteeMobile"].ToString();
            int InviteeIdentity = AppGlobal.StrToInt(row["InviteeIdentity"].ToString());
            string IDCard1 = null;
            string IDCard2 = null;
            string IDCardNum = null;

            if (row.Table.Columns.Contains("IDCard1") && !string.IsNullOrEmpty(row["IDCard1"].ToString()))
            {
                IDCard1 = row["IDCard1"].ToString();
            }
            if (row.Table.Columns.Contains("IDCard2") && !string.IsNullOrEmpty(row["IDCard2"].ToString()))
            {
                IDCard2 = row["IDCard2"].ToString();
            }
            if (row.Table.Columns.Contains("IDCardNum") && !string.IsNullOrEmpty(row["IDCardNum"].ToString()))
            {
                IDCardNum = row["IDCardNum"].ToString();
            }

            //查询小区
            Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(communityId);
            //构造链接字符串
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }

            using (var conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                string sql = @"SELECT count(*) FROM Tb_User_Relation WHERE RoomId=@RoomID AND isnull(Locked,0)=0;
                               SELECT MaxInvite FROM Tb_AuditControl_InviteUser WHERE CorpID=@CorpID;";

                GridReader reader = conn.QueryMultiple(sql, new { RoomID = RoomID, CorpID = Community.CorpID });
                int count = reader.Read<int>().FirstOrDefault();
                int maxInvite = reader.Read<int>().FirstOrDefault();

                string IID = Guid.NewGuid().ToString().ToUpper();

                sql = @"INSERT INTO Tb_Audit_InviteUser(IID, CommunityID, RoomID, ApplicantUserID, 
                                InviteeName, InviteeMobile, InviteeIdentity, IDCard1, IDCard2, IDCardNum)
                            VALUES(@IID,@CommunityID, @RoomID, @ApplicantUserID, @InviteeName, 
                                @InviteeMobile, @InviteeIdentity, @IDCard1, @IDCard2, @IDCardNum)";

                conn.Execute(sql, new
                {
                    IID = IID,
                    CommunityID = Community.Id,
                    RoomID = RoomID,
                    ApplicantUserID = ApplicantUserID,
                    InviteeName = InviteeName,
                    InviteeMobile = InviteeMobile,
                    InviteeIdentity = InviteeIdentity,
                    IDCard1 = IDCard1,
                    IDCard2 = IDCard2,
                    IDCardNum = IDCardNum
                });

                // 如果当前绑定用户数已经小于设置的最大绑定数，并且邀请的身份不是业主或家属，自动审核
                if (count < (maxInvite + 1) && (InviteeIdentity == 0 || InviteeIdentity == 1))
                {
                    return Audit(1, IID, "自动审核通过");
                }
                return new ApiResult(true, "提交审核成功").toJson();
            }
        }

        /// <summary>
        /// 业主取消邀请审核
        /// </summary>
        private string Delete(DataRow row)
        {
            if (!row.Table.Columns.Contains("IID") || string.IsNullOrEmpty(row["IID"].ToString()))
            {
                return JSONHelper.FromString(false, "缺少IID参数");
            }

            string IID = row["IID"].AsString();

            using (var conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                conn.Execute(@"UPDATE Tb_Audit_InviteUser SET IsDelete=1,Remark='业主取消邀请' 
                    WHERE IID=@IID;", new { IID = IID });

                return JSONHelper.FromString(true, "取消成功");
            }
        }

        /// <summary>
        /// 审核操作
        /// </summary>
        private string Audit(DataRow row)
        {
            if (!row.Table.Columns.Contains("IID") || string.IsNullOrEmpty(row["IID"].ToString()))
            {
                return JSONHelper.FromString(false, "缺少IID参数");
            }
            if (!row.Table.Columns.Contains("IsAllows") || string.IsNullOrEmpty(row["IsAllows"].ToString()))
            {
                return JSONHelper.FromString(false, "缺少IsAllows参数");
            }
            if (!row.Table.Columns.Contains("Remark"))
            {
                return JSONHelper.FromString(false, "缺少Remark参数");
            }

            int IsAllows = AppGlobal.StrToInt(row["IsAllows"].ToString());
            string IID = row["IID"].ToString();
            string Remark = row["Remark"].ToString();

            return Audit(IsAllows, IID, Remark);
        }

        /// <summary>
        /// 审核操作
        /// </summary>
        private string Audit(int IsAllows, string IID, string Remark)
        {
            using (var conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                string sql = @"UPDATE Tb_Audit_InviteUser SET IsAllows=@IsAllows,AuditTime=getdate(),Remark=@Remark 
                                WHERE IID=@IID;";

                int i = conn.Execute(sql, new
                {
                    IsAllows = IsAllows,
                    IID = IID,
                    Remark = Remark,
                });

                if (i > 0)
                {
                    // 小区信息
                    Tb_Community Community = conn.Query<Tb_Community>(@"SELECT * FROM Tb_Community WHERE Id IN 
                            (SELECT CommunityID FROM Tb_Audit_InviteUser WHERE IID=@IID)",
                            new { IID = IID }).FirstOrDefault();

                    // 审核信息
                    dynamic InviteInfo = conn.Query(@"SELECT * FROM Tb_Audit_InviteUser WHERE IID=@IID;", new { IID = IID }).FirstOrDefault();

                    // 申请人绑定关系
                    dynamic BindingInfo = conn.Query(@"SELECT * FROM Tb_User_Relation WHERE UserId=@UserID AND RoomId=@RoomID;", new { UserID = InviteInfo.ApplicantUserID, RoomID = InviteInfo.RoomID }).FirstOrDefault();

                    if (Community != null && InviteInfo != null && BindingInfo != null)
                    {
                        string strcon = GetConnectionStringStr(Community);
                        string relationId = BindingInfo.Id.ToString();
                        string newRelation = string.Empty;
                        string inviteUserId = null;
                        string communityId = BindingInfo.CommunityId.ToString();
                        string inviteMobile = InviteInfo.InviteeMobile.ToString();
                        string inviteName = InviteInfo.InviteeName.ToString();
                        string relationship = UserRoomIdentityCode.Other;

                        if (InviteInfo.InviteeIdentity == 0)
                        {
                            relationship = UserRoomIdentityCode.Customer2;
                        }
                        else if (InviteInfo.InviteeIdentity == 1)
                        {
                            relationship = UserRoomIdentityCode.FamilyMember;
                        }
                        else if(InviteInfo.InviteeIdentity == 2)
                        {
                            relationship = UserRoomIdentityCode.Tenant;
                        }

                        string message, errorMessage;

                        sql = @"SELECT CustId,RoomId,RoomSign,CustName FROM Tb_User_Relation WHERE Id=@RelationId";
                        dynamic relationInfo = conn.Query(sql, new { RelationId = relationId }).First();
                        string custId = relationInfo.CustId.ToString();
                        string roomId = relationInfo.RoomId.ToString();
                        string roomSign = relationInfo.RoomSign.ToString();
                        string custName = relationInfo.CustName.ToString();
                        long holdId = 0;

                        if (IsAllows == 0)
                        {
                            string mobile = conn.Query<string>(@"SELECT Mobile FROM Tb_User WHERE Id=@UserID", new { UserID = InviteInfo.ApplicantUserID }).FirstOrDefault();

                            message = string.Format(@"您邀请手机号为“{0}”的住户{1}的申请已被驳回，驳回原因：{2}",
                                inviteMobile, inviteName, (string.IsNullOrEmpty(Remark) ? "无" : Remark));
                            SendShortMessage(mobile, message, out errorMessage, Community.CorpID);

                            return JSONHelper.FromString(true, "邀请住户申请驳回成功");
                        }

                        // 1、查询被邀者的手机号是否已经注册
                        sql = "SELECT top 1 Id FROM Tb_User WHERE Mobile=@Mobile";
                        IEnumerable<string> resultSet = conn.Query<string>(sql, new { Mobile = inviteMobile });

                        if (resultSet.Count() > 0)
                        {
                            inviteUserId = resultSet.First();

                            // 1.2、查询被邀者是否已经绑定该房屋
                            sql = @"SELECT RoomSign,isnull(CustHoldId,0) AS CustHoldId FROM Tb_User_Relation 
                                        WHERE UserId=@UserId AND RoomId IN 
                                    (SELECT RoomId FROM Tb_User_Relation WHERE Id=@Relation)";
                            IEnumerable<dynamic> userRelationSet = conn.Query(sql, new { UserId = inviteUserId, Relation = relationId });

                            // 该已绑定该房屋
                            if (userRelationSet.Count() > 0)
                            {
                                // 家庭成员id
                                holdId = long.Parse(userRelationSet.First().CustHoldId);

                                // 已经绑定但锁定则解锁
                                sql = @"UPDATE Tb_User_Relation SET Locked=0 WHERE UserId=@UserId AND RoomId IN 
                                    (SELECT RoomId FROM Tb_User_Relation WHERE Id=@Relation)";
                                conn.Execute(sql, new { UserId = inviteUserId, Relation = relationId });

                                // 更新家庭成员信息
                                using (IDbConnection conn2 = new SqlConnection(strcon))
                                {
                                    conn2.Execute(@"UPDATE Tb_HSPR_Household SET IsDelete=0,Relationship=@Relationship,
                                                MemberName=@Name,Linkman=@Name,Surname=@Name,Name=@Name
                                                WHERE HoldID=@HoldID", new
                                    {
                                        HoldID = holdId,
                                        Relationship = relationship,
                                        Name = inviteName
                                    });

                                    return JSONHelper.FromString(true, "操作成功");
                                }
                            }
                            // 未绑定
                            else
                            {
                                // 1.3、直接给被邀者绑定该房屋
                                newRelation = Guid.NewGuid().ToString();
                                sql = @"INSERT INTO Tb_User_Relation(Id,UserId,CommunityId,CustId,RoomId,RegDate,CustName,
                                            RoomSign,CustMobile,Locked,CustHoldId)
                                        SELECT @NewRelation AS Id,@UserId AS UserId,CommunityId,CustId,roomid,getdate(),
                                            CustName,RoomSign,CustMobile,0 AS Locked,0 AS CustHoldId
                                            FROM Tb_User_Relation WHERE Id=@RelationId";
                                conn.Execute(sql, new { UserId = inviteUserId, NewRelation = newRelation, RelationId = relationId });
                            }

                            // 短信内容
                            message = string.Format("温馨提示：\"{0}\"业主{1}为您绑定了编号为：{2}的房屋，您可以对该房屋进行报事和缴费等操作。",
                                    Community.CommName, custName, roomSign);
                        }
                        else
                        {
                            // 2、被邀者没有注册账号，创建账号并绑定房屋关系
                            inviteUserId = Guid.NewGuid().ToString();
                            newRelation = Guid.NewGuid().ToString();
                            conn.Execute(@"INSERT INTO Tb_User(Id,Name,Mobile,NickName,Pwd,Sex,RegDate) VALUES(@UserId,@Name,@Mobile,@Name,'123456',1,getdate());
                                   INSERT INTO Tb_User_Relation(Id,UserId,CommunityId,CustId,RoomId,RegDate,CustName,RoomSign,CustMobile,Locked)
                                    SELECT @NewRelation AS Id, @UserId AS UserId,CommunityId,CustId,roomid,getdate(),CustName,RoomSign,CustMobile, 
                                    0 AS Locked FROM Tb_User_Relation WHERE Id=@RelationId",
                                            new
                                            {
                                                UserId = inviteUserId,
                                                Name = inviteName,
                                                Mobile = inviteMobile,
                                                NewRelation = newRelation,
                                                RelationId = relationId
                                            });

                            

                            message = string.Format("温馨提示：“{0}”业主{1}邀请您使用App，并为您绑定了编号为：{2}的房屋，您的账号为：{3}，初始密码为：123456。",
                                    Community.CommName, custName, roomSign, inviteMobile);
                        }

                        SendShortMessage(inviteMobile, message, out errorMessage, Community.CorpID);

                        using (IDbConnection conn2 = new SqlConnection(strcon))
                        {
                            DynamicParameters parameters = new DynamicParameters();
                            parameters.Add("@HoldID", 0, DbType.Int64, ParameterDirection.Output);
                            parameters.Add("@CommID", Community.CommID);
                            parameters.Add("@CustID", custId);
                            parameters.Add("@RoomID", roomId);
                            parameters.Add("@Name", inviteName);
                            parameters.Add("@MobilePhone", inviteMobile);
                            parameters.Add("@Relationship", relationship);
                            if (conn2.State == ConnectionState.Closed)
                            {
                                conn2.Open();
                            }
                            conn2.Execute("Proc_HSPR_Household_Insert_Phone", parameters, null, null, CommandType.StoredProcedure);
                            long custHoldId = parameters.Get<long>("@HoldID");

                            conn.Execute(@"UPDATE Tb_User_Relation SET CustHoldId=@HoldID WHERE Id=@RelationId",
                                new { HoldID = custHoldId, RelationId = newRelation });
                        }

                        return JSONHelper.FromString(true, "操作成功");
                    }
                }

                return new ApiResult(false, "操作失败").toJson();
            }
        }

        /// <summary>
        /// 等待审核列表
        /// </summary>
        private string WaitingAuditList(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return JSONHelper.FromString(false, "缺少CommunityId参数");
            }
            if (!row.Table.Columns.Contains("RoomID") || string.IsNullOrEmpty(row["RoomID"].ToString()))
            {
                return JSONHelper.FromString(false, "缺少RoomID参数");
            }
            if (!row.Table.Columns.Contains("ApplicantUserID") || string.IsNullOrEmpty(row["ApplicantUserID"].ToString()))
            {
                return JSONHelper.FromString(false, "缺少ApplicantUserID参数");
            }

            string communityId = row["CommunityId"].AsString();
            string RoomID = row["RoomID"].ToString();
            string ApplicantUserID = row["ApplicantUserID"].ToString();

            //查询小区
            Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(communityId);
            //构造链接字符串
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }

            using (var conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                string sql = @"SELECT IID,InviteeName,InviteeMobile,InviteeIdentity FROM Tb_Audit_InviteUser 
                                WHERE ApplicantUserID=@ApplicantUserID AND RoomID=@RoomID AND IsAllows IS NULL 
                                AND isnull(IsDelete,0)=0";

                IEnumerable<dynamic> resultSet = conn.Query(sql, new
                {
                    RoomID = RoomID,
                    ApplicantUserID = ApplicantUserID
                });

                string result = new ApiResult(true, resultSet).toJson();

                int maxInvite = conn.Query<int>(@"SELECT MaxInvite FROM Tb_AuditControl_InviteUser WHERE CorpID=@CorpID;", new { CorpID = Community.CorpID }).FirstOrDefault();

                result = result.Insert(result.Length - 1, ",\"MaxInvite\":" + maxInvite);
                return result;
            }
        }
    }
}
