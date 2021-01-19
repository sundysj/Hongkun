using Dapper;
using MobileSoft.BLL.Common;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using MobileSoft.Model.Common;
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
    public class InviteUser: PubInfo
    {
        public InviteUser() //获取小区、项目信息
        {
            base.Token = "20180123InviteUser";
        }

        public override void Operate(ref Common.Transfer Trans)
        {
            Trans.Result = JSONHelper.FromString(false, "未知错误");

            DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
            DataRow Row = dAttributeTable.Rows[0];

            switch (Trans.Command)
            {
                case "Invite":  
                    Trans.Result = Invite(Row);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 邀请用户
        /// </summary>
        private string Invite(DataRow Row)
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
            int inviteType = AppGlobal.StrToInt(Row["InviteType"].ToString());

            //查询小区
            Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(communityId);
            //构造链接字符串
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }

            string strcon = new Business.CostInfo().GetConnectionStringStr(Community);

            string sql, message, inviteUserId, newRelation;
            using (IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                // 获取App信息
                sql = @"SELECT * FROM Tb_AppInfo WHERE Type=2 AND CorpID=@CorpID";
                dynamic appInfo = conn.Query(sql, new { CorpID = Community.CorpID }).FirstOrDefault();
                string appName = (appInfo?.AppName) ?? "";
                string downloadUrl = (appInfo?.DownloadUrl) ?? "";

                sql = @"SELECT CommName FROM Tb_Community WHERE Id=@Id";
                string commName = conn.Query<string>(sql, new { Id = communityId }).FirstOrDefault();

                sql = @"SELECT CustId,RoomId,RoomSign,CustName FROM Tb_User_Relation WHERE Id=@RelationId";
                dynamic relationInfo = conn.Query(sql, new { RelationId = relationId }).FirstOrDefault();
                string custId = relationInfo.CustId;
                string roomId = relationInfo.RoomId;
                string roomSign = relationInfo.RoomSign;
                string custName = relationInfo.CustName;

                // 1、查询被邀者的手机号是否已经注册
                sql = "SELECT top 1 Id FROM Tb_User WHERE Mobile=@Mobile";
                IEnumerable<string> resultSet = conn.Query<string>(sql, new { Mobile = inviteMobile });

                if (resultSet.Count() > 0)
                {
                    inviteUserId = resultSet.First();

                    // 1.2、查询被邀者是否已经绑定该房屋
                    sql = @"SELECT RoomSign FROM Tb_User_Relation WHERE UserId=@UserId AND RoomId IN 
                                    (SELECT RoomId FROM Tb_User_Relation WHERE Id=@Relation)";
                    if (conn.Query<string>(sql, new { UserId = inviteUserId, Relation = relationId }).Count() > 0)
                    {
                        conn.Execute(@"UPDATE Tb_User_Relation SET Locked=0 WHERE UserId=@UserId AND RoomId IN 
                                    (SELECT RoomId FROM Tb_User_Relation WHERE Id=@Relation)", 
                                    new { UserId = inviteUserId, Relation = relationId });

                        return JSONHelper.FromString(false, "该用户已经绑定该房屋");
                    }
                    else
                    {
                        // 1.3、直接给被邀者绑定该房屋
                        newRelation = Guid.NewGuid().ToString();
                        sql = @"INSERT INTO Tb_User_Relation(Id,UserId,CommunityId,CustId,RoomId,RegDate,CustName,RoomSign,CustMobile,Locked,CustHoldId)
                                SELECT @NewRelation AS Id,@Id AS UserId,CommunityId,CustId,roomid,getdate(),CustName,RoomSign,CustMobile,
                                    0 AS Locked,@InviteType AS CustHoldId
                                FROM Tb_User_Relation WHERE Id=@RelationId";
                        conn.Execute(sql, new { Id = inviteUserId, NewRelation = newRelation, RelationId = relationId, InviteType = inviteType });

                        message = string.Format("{0}温馨提示：\"{1}\"业主{2}为您绑定了编号为【{3}】房屋，您可以对该房屋进行报事和缴费等操作。",
                            appName, commName, custName, roomSign);

                        SendMessage(Community.CorpID, inviteMobile, message);
                    }
                }
                else
                {
                    // 2、被邀者没有注册账号
                    inviteUserId = Guid.NewGuid().ToString();
                    newRelation = Guid.NewGuid().ToString();
                    conn.Execute(@"INSERT INTO Tb_User(Id,Name,Mobile,NickName,Pwd,Sex,RegDate) VALUES(@UserId,@Name,@Mobile,@Mobile,'123456',1,getdate());
                                   INSERT INTO Tb_User_Relation(Id,UserId,CommunityId,CustId,RoomId,RegDate,CustName,RoomSign,CustMobile,Locked)
                                    SELECT @NewRelation AS Id, @UserId AS UserId,CommunityId,CustId,roomid,getdate(),CustName,RoomSign,CustMobile, 
                                    0 AS Locked FROM Tb_User_Relation WHERE Id=@RelationId",
                                    new { UserId = inviteUserId, Name = inviteName, Mobile = inviteMobile, NewRelation = newRelation, RelationId = relationId, InviteType = inviteType });

                    if (string.IsNullOrEmpty(downloadUrl))
                    {
                        message = string.Format("{0}温馨提示：\"{1}\"业主{2}邀请您使用{0}，您的账号为：{3}，密码为：123456，请及时下载登录{0}并修改密码，下载地址：{4}", appName, commName, custName, inviteMobile, downloadUrl);
                    }
                    else
                    {
                        message = string.Format("{0}温馨提示：\"{1}\"业主{2}邀请您使用{0}，您的账号为：{3}，密码为：123456，请及时下载登录{0}并修改密码。", appName, commName, custName, inviteMobile);
                    }
                   

                    SendMessage(Community.CorpID, inviteMobile, message);
                }

                using (IDbConnection conn2 = new SqlConnection(strcon))
                {
                    // ERP添加家属信息
                    if (inviteType == 1)
                    {
                        DynamicParameters parameters = new DynamicParameters();
                        parameters.Add("@HoldID", 0, DbType.Int64, ParameterDirection.InputOutput);
                        parameters.Add("@CommID", Community.CommID);
                        parameters.Add("@CustID", custId);
                        parameters.Add("@RoomID", roomId);
                        parameters.Add("@Surname", inviteName);
                        parameters.Add("@Name", inviteName);
                        parameters.Add("@MobilePhone", inviteMobile);
                        parameters.Add("@MemberName", inviteName);
                        parameters.Add("@Linkman", inviteName);
                        parameters.Add("@LinkManTel", inviteMobile);
                        parameters.Add("@Relationship", "0032");
                        conn2.Execute("Proc_HSPR_Household_Insert", parameters, null, null, CommandType.StoredProcedure);
                        long custHoldId = parameters.Get<long>("@HoldID");

                        conn.Execute(@"UPDATE Tb_User_Relation SET CustHoldId=@HoldID WHERE Id=@RelationId",
                            new { HoldID = custHoldId, RelationId = newRelation });
                    }
                    // 添加租户信息
                    else
                    {
                        DynamicParameters parameters = new DynamicParameters();
                        parameters.Add("@TenantName", inviteName);
                        parameters.Add("@TenantMobile", inviteMobile);
                        parameters.Add("@CommID", Community.CommID);
                        parameters.Add("@RoomID", roomId);
                        parameters.Add("@CustID", 0, DbType.Int64, ParameterDirection.Output);

                        conn2.Execute("Proc_HSPR_Customer_AddTenant", parameters, null, null, CommandType.StoredProcedure);

                        conn.Execute("UPDATE Tb_User_Relation SET CustID=@CustID WHERE Id=@RelationId",
                            new { CustID = parameters.Get<Int64>("@CustID"), RelationId = newRelation });
                    }
                }

                if (resultSet.Count() > 0)
                {
                    return JSONHelper.FromString(false, "该手机号已注册，已直接绑定房号");
                }
                else
                {
                    return JSONHelper.FromString(true, "邀请成功");
                }
            }
        }

        private void SendMessage(int corpId, string mobile, string message)
        {
            int code = new Random(unchecked((int)DateTime.Now.Ticks)).Next(100000, 999999);

            Tb_Sms_Account smsModel = SmsInfo.GetSms_Account();
            Tb_SendMessageRecord m = new Tb_SendMessageRecord();

            message = message + smsModel.Sign;

            //发送短信
            //int Result = Common.Sms.Send(smsModel.SmsAccount, smsModel.SmsPwd, mobile, message, "", "");
            int Result = Common.Sms.Send_v2(smsModel.SmsUserId, smsModel.SmsAccount, smsModel.SmsPwd, mobile, message, out string strErrMsg);
            string Resul = "";
            switch (Result)
            {
                case 0:
                    Resul = "发送成功";
                    try
                    {
                        //记录短信
                        m = new Bll_Tb_SendMessageRecord().Add(mobile, message, Guid.NewGuid().ToString(), "泰禾业主App邀请用户", "");
                    }
                    catch (Exception)
                    {

                    }
                    break;
                case -4:
                    Resul = "手机号码格式不正确";
                    break;
                default:
                    Resul = "发送失败：" + Result;
                    break;
            }
            //修改状态
            m.SendState = Result.ToString();
            //重写短信记录状态
            new Bll_Tb_SendMessageRecord().Update(m);
        }
    }
}
