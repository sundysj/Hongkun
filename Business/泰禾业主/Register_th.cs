using Dapper;
using MobileSoft.BLL.Common;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using MobileSoft.Model.Common;
using MobileSoft.Model.Unified;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using static Dapper.SqlMapper;
using TaiHeSmsSdk;

namespace Business
{
    public class Register_th : PubInfo
    {
        public Register_th() //获取小区、项目信息
        {
            base.Token = "20171024Register";
        }

        public override void Operate(ref Common.Transfer Trans)
        {
            Trans.Result = JSONHelper.FromString(false, "未知错误");

            DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
            DataRow Row = dAttributeTable.Rows[0];

            switch (Trans.Command)
            {
                case "GetVerifyCode":   //获取验证码
                    Trans.Result = GetVerifyCode(Row, Trans);
                    break;
                case "RegisterUser":
                    Trans.Result = RegisterUser(Row);//注册用户
                    break;
                case "UpdateUserPwd":   //修改密码
                    Trans.Result = UpdateUserPwd(Row);
                    break;
                case "InviteUser":      // 邀请用户
                    Trans.Result = InviteUser(Row);
                    break;
                case "InviteUser_jf":   // 邀请用户(俊发，同泰禾)
                    Trans.Result = InviteUser(Row);
                    break;
                case "InviteUser_hnc":  // 邀请用户(华南城微信，增加同时邀请多套房屋)
                    Trans.Result = InviteUser_hnc(Row);
                    break;
                default:
                    break;
            }
        }


        /// <summary>
        /// 邀请用户_华南城微信
        /// </summary>
        private string InviteUser_hnc(DataRow Row)
        {


            #region 基础参数校验
            if (!Row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(Row["CommunityId"].ToString()))
            {
                return JSONHelper.FromString(false, "缺少CommunityId参数");
            }
            if (!Row.Table.Columns.Contains("UserMobile") || string.IsNullOrEmpty(Row["UserMobile"].ToString()))
            {
                return JSONHelper.FromString(false, "缺少UserMobile参数");
            }
            if (!Row.Table.Columns.Contains("RelationIds") || string.IsNullOrEmpty(Row["RelationIds"].ToString()))
            {
                return JSONHelper.FromString(false, "缺少RelationIds参数");
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
            string RelationIds = Row["RelationIds"].ToString().Trim();
            string inviteMobile = Row["InviteMobile"].ToString();
            string inviteName = Row["InviteName"].ToString();
            string inviteType = Row["InviteType"].ToString();

            List<string> RelationList = new List<string>();
            foreach (var item in RelationIds.Split(','))
            {
                if (!string.IsNullOrEmpty(item))
                {
                    RelationList.Add(item);
                }
            }
            if (RelationList.Count == 0)
            {
                return new ApiResult(false, "邀请失败,请选择要邀请的房屋").toJson();
            }
            #endregion

            #region 处理邀请类型
            // ERP添加家属信息，0：业主，1：家属，2：租户，3：其它
            if (inviteType == "0")
            {
                inviteType = "0029";
            }
            else if (inviteType == "1")
            {
                inviteType = "0030";
            }
            else if (inviteType == "2")
            {
                inviteType = "0031";
            }
            else
            {
                inviteType = "0032";
            }
            #endregion


            //查询小区
            Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(communityId);
            //构造链接字符串
            if (null == Community)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }

            string strcon = new Business.CostInfo().GetConnectionStringStr(Community);

            try
            {
                using (IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString))
                {
                    List<string> RoomSignList = new List<string>();

                    // 当前小区
                    string sql = @"SELECT CommName FROM Tb_Community WHERE Id=@Id";
                    string commName = conn.Query<string>(sql, new { Id = communityId }).First();
                    // 要绑定的房屋
                    sql = @"SELECT Id AS RelationId, Convert(bigint,CustId) AS CustId, Convert(bigint,RoomId) AS RoomId,RoomSign,CustName FROM Tb_User_Relation WHERE Id IN ('" + string.Join("','", RelationList) + "')";
                    List<dynamic> relationSet = conn.Query(sql).ToList();

                    for (int i = 0; i < relationSet.Count; i++)
                    {
                        RoomSignList.Add(relationSet[i].RoomSign);
                        relationSet[i].CustHoldId = Proc_HSPR_Household_Insert_Phone(strcon, Community.CommID, relationSet[i].CustId, relationSet[i].RoomId, inviteName, inviteMobile, inviteType);
                    }

                    // 1、查询被邀者的手机号是否已经注册
                    sql = "SELECT * FROM Tb_User WHERE Mobile = @Mobile";
                    Tb_User tb_User = conn.QueryFirstOrDefault<Tb_User>(sql, new { Mobile = inviteMobile });

                    if (null != tb_User)
                    {
                        for (int i = 0; i < relationSet.Count; i++)
                        {

                            // 1.2、查询被邀者是否已经绑定该房屋
                            sql = @"SELECT RoomSign,Convert(bigint,ISNULL(CustHoldId,0)) AS CustHoldId FROM Tb_User_Relation WHERE UserId=@UserId AND RoomId IN 
                                    (SELECT RoomId FROM Tb_User_Relation WHERE Id=@Relation)";
                            IEnumerable<dynamic> userRelationSet = conn.Query(sql, new { UserId = tb_User.Id, Relation = relationSet[i].RelationId });
                            if (userRelationSet.Count() > 0)
                            {
                                // 已经绑定但锁定则解锁
                                sql = @"UPDATE Tb_User_Relation SET Locked = 0, CustHoldId = @HoldID WHERE UserId=@UserId AND RoomId IN 
                                    (SELECT RoomId FROM Tb_User_Relation WHERE Id=@Relation)";
                                conn.Execute(sql, new { UserId = tb_User.Id, HoldID = relationSet[i].CustHoldId, Relation = relationSet[i].RelationId });
                            }
                            // 未绑定
                            else
                            {
                                // 1.3、直接给被邀者绑定该房屋
                                string newRelation = Guid.NewGuid().ToString();
                                sql = @"INSERT INTO Tb_User_Relation(Id,UserId,CommunityId,CustId,RoomId,RegDate,CustName,RoomSign,CustMobile,Locked,CustHoldId)
                                SELECT @NewRelation AS Id,@Id AS UserId,CommunityId,CustId,roomid,getdate(),CustName,RoomSign,CustMobile,
                                    0 AS Locked,0 AS CustHoldId
                                FROM Tb_User_Relation WHERE Id=@RelationId";
                                conn.Execute(sql, new { Id = tb_User.Id, NewRelation = newRelation, RelationId = relationSet[i].RelationId });
                                conn.Execute(@"UPDATE Tb_User_Relation SET CustHoldId=@HoldID WHERE Id=@RelationId", new { HoldID = relationSet[i].CustHoldId, RelationId = newRelation });
                            }
                        }
                    }
                    else
                    {
                        // 2、被邀者没有注册账号，创建账号并绑定房屋关系
                        string UserId = Guid.NewGuid().ToString();

                        conn.Execute("INSERT INTO Tb_User(Id,Name,Mobile,NickName,Pwd,Sex,RegDate) VALUES(@UserId,@Name,@Mobile,@Name,'123456',1,GETDATE())",
                            new
                            {
                                UserId = UserId,
                                Name = inviteName,
                                Mobile = inviteMobile,

                            },
                            null, null, CommandType.Text);

                        for (int i = 0; i < relationSet.Count; i++)
                        {
                            string newRelation = Guid.NewGuid().ToString();
                            conn.Execute(@"INSERT INTO Tb_User_Relation(Id,UserId,CommunityId,CustId,RoomId,RegDate,CustName,RoomSign,CustMobile,Locked)
                                    SELECT @Id AS Id, @UserId AS UserId,CommunityId,CustId,RoomID,GETDATE(),CustName,RoomSign,CustMobile, 
                                    0 AS Locked FROM Tb_User_Relation WHERE Id = @RelationId",
                                            new
                                            {
                                                Id = newRelation,
                                                UserId = UserId,
                                                RelationId = relationSet[i].RelationId
                                            });

                            conn.Execute(@"UPDATE Tb_User_Relation SET CustHoldId=@HoldID WHERE Id=@RelationId", new { HoldID = relationSet[i].CustHoldId, RelationId = newRelation });
                        }
                    }

                    string message = string.Format("{0}业主为您绑定了编号为{1}的房屋，关注微信公众号“第一亚太物业”，您可以对该房屋进行报事和缴费等操作。",
                                commName, string.Join("、", RoomSignList));

                    SendShortMessage(Row["Mobile"].ToString(), message, out string errorMessage);


                    return JSONHelper.FromString(true, "邀请成功");
                }
            }
            catch (Exception ex)
            {
                return JSONHelper.FromString(false, "邀请失败(" + ex.Message + ")");
            }
        }
        private long Proc_HSPR_Household_Insert_Phone(string connStr, string CommID, long CustId, long RoomId, string inviteName, string inviteMobile, string inviteType)
        {
            using (IDbConnection conn = new SqlConnection(connStr))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@HoldID", 0, DbType.Int64, ParameterDirection.Output);
                parameters.Add("@CommID", CommID);
                parameters.Add("@CustID", CustId);
                parameters.Add("@RoomID", RoomId);
                parameters.Add("@Name", inviteName);
                parameters.Add("@MobilePhone", inviteMobile);
                parameters.Add("@Relationship", inviteType);
                conn.Execute("Proc_HSPR_Household_Insert_Phone", parameters, null, null, CommandType.StoredProcedure);
                long custHoldId = parameters.Get<long>("@HoldID");
                return custHoldId;
            }
        }
        /// <summary>
        /// 添加租户信息
        /// </summary>
        /// <returns></returns>
        private bool Proc_AddTenantInfo(Tb_Community tb_Community, string RoomId, out string CustID)
        {
            CustID = "";
            if (null == tb_Community)
            {
                return false;
            }
            if (string.IsNullOrEmpty(RoomId))
            {
                return false;
            }
            //获取ERP连接字符串
            string connStr = GetConnectionStringStr(tb_Community);
            using (IDbConnection conn = new SqlConnection(connStr))
            {
                //生成CustID
                CustID = conn.Query<string>("SELECT ISNULL(max(CustID) + 1, @CommID * CAST(100000000 AS BIGINT) + 1) AS CustID FROM Tb_HSPR_Customer WHERE CommID = @CommID", new { CommID = tb_Community.CommID }).FirstOrDefault();
                if (string.IsNullOrEmpty(CustID))
                {
                    return false;
                }
                //生成LiveID
                string LiveID = conn.Query<string>("SELECT (max(LiveID) + 1) AS CustID FROM Tb_HSPR_CustomerLive", new { }).FirstOrDefault();
                if (string.IsNullOrEmpty(LiveID))
                {
                    return false;
                }
                dynamic info = conn.Query("SELECT RoomSign, BuildArea FROM Tb_HSPR_Room WHERE ISNULL(IsDelete,0) = 0 AND CommID = @CommID AND RoomId = @RoomId", new { CommID = tb_Community.CommID, RoomId = RoomId }).FirstOrDefault();
                if (null == info)
                {
                    return false;
                }
                string RoomSign = info.RoomSign;
                decimal BuildArea = info.BuildArea;
                if (string.IsNullOrEmpty(RoomSign))
                {
                    return false;
                }
                RoomSign = RoomSign + "<font color=red>[" + BuildArea.ToString("#0.00") + "]</font><font color=green>[正常入住]</font>";

                if (conn.Execute("INSERT INTO Tb_HSPR_CustomerLive(LiveID, RoomID, CustID, LiveType, IsActive, IsDelLive) VALUES(@LiveID, @RoomId, @CustID, 2, 1, 0)", new { LiveID = LiveID, RoomId = RoomId, CustID = CustID }) == 0)
                {
                    return false;
                }
                return true;
            }
        }
        /// <summary>
        /// 获取验证码
        /// </summary>
        private string GetVerifyCode(DataRow Row, Common.Transfer Trans)
        {
            if (!Row.Table.Columns.Contains("Mobile") || string.IsNullOrEmpty(Row["Mobile"].ToString()))
            {
                return JSONHelper.FromString(false, "缺少Mobile参数");
            }

            string forgetPassword = null;

            // 忘记密码功能时传入该参数
            if (Row.Table.Columns.Contains("IsForget") && !string.IsNullOrEmpty(Row["IsForget"].ToString()))
            {
                forgetPassword = Row["IsForget"].ToString();
            }

            int code = new Random(unchecked((int)DateTime.Now.Ticks)).Next(100000, 999999);
            string Content = "您的验证码为：" + code;

            if (forgetPassword != "1")
            {
                DataTable dTable = new DbHelperSQLP(PubConstant.UnifiedContionString.ToString()).Query("SELECT Name FROM Tb_User where Mobile='" + Row["Mobile"].ToString() + "'").Tables[0];
                if (dTable.Rows.Count > 0)
                {
                    return JSONHelper.FromString(false, "此用户已存在");
                }

                try
                {
                    if (HttpContext.Current.Request.Url.Host.Contains("tahoecn"))
                    {
                        // 泰禾版本
                        string connString = @"Connect Timeout=100;Connection Lifetime=60;Max Pool Size=2000;Min Pool Size=0;Pooling=true;data source=10.0.102.32;initial catalog=HM_wygl_new_1970;PWD=LF123SPoss;persist security info=False;user id=LFUser;packet size=4096";

                        if (HttpContext.Current.Request.Url.Host.ToLower() == "test.wyerp.tahoecndemo.com")
                        {
                            connString = @"Connect Timeout=100;Connection Lifetime=60;Max Pool Size=2000;Min Pool Size=0;Pooling=true;data source=(local);initial catalog=HM_wygl_new_1970;PWD=LF123SPoss;persist security info=False;user id=LFUser;packet size=4096";
                        }

                        // 泰禾在此时需要验证是否是业主身份
                        using (IDbConnection conn = new SqlConnection(connString))
                        {
                            string sql = @"SELECT * FROM Tb_HSPR_Customer x WHERE isnull(x.IsDelete,0)=0 AND x.LiveType1=1 AND x.MobilePhone LIKE @MobilePhone;
                                        SELECT * FROM Tb_HSPR_Customer x WHERE isnull(x.IsDelete,0)=0 AND x.LiveType2=2 AND x.MobilePhone LIKE @MobilePhone;
                                        SELECT * FROM Tb_HSPR_Household x WHERE isnull(x.IsDelete,0)=0 AND x.MobilePhone LIKE @MobilePhone;
                                        SELECT * FROM Tb_HSPR_Customer x WHERE isnull(x.IsDelete,0)=0 AND x.LiveType1=1 AND x.MobilePhone LIKE @MobilePhone
                                            AND (SELECT count(0) FROM Tb_HSPR_CustomerLive y WHERE x.CustID=y.CustID AND isnull(y.IsDelLive,0)=0 
                                            AND y.LiveType=1 AND CommID IN(SELECT CommID FROM Unified.dbo.Tb_Community))>0;
                                        SELECT * FROM Tb_HSPR_Customer x WHERE isnull(x.IsDelete,0)=0 AND x.LiveType2=2 AND x.MobilePhone LIKE @MobilePhone
                                            AND (SELECT count(0) FROM Tb_HSPR_CustomerLive y WHERE x.CustID=y.CustID AND isnull(y.IsDelLive,0)=0 
                                            AND y.LiveType=2 AND CommID IN(SELECT CommID FROM Unified.dbo.Tb_Community))>0;
                                        SELECT * FROM Tb_HSPR_Household x WHERE isnull(x.IsDelete,0)=0 AND x.MobilePhone LIKE @MobilePhone
                                            AND (SELECT count(0) FROM Tb_HSPR_CustomerLive y WHERE x.CustID=y.CustID AND isnull(y.IsDelLive,0)=0 
                                            AND y.LiveType=1 AND CommID IN(SELECT CommID FROM Unified.dbo.Tb_Community))>0;";

                            GridReader reader = conn.QueryMultiple(sql, new { MobilePhone = $"%{Row["Mobile"].ToString()}%" });

                            int[] counts = new int[] {
                                reader.Read().Count(),
                                reader.Read().Count(),
                                reader.Read().Count(),
                                reader.Read().Count(),
                                reader.Read().Count(),
                                reader.Read().Count() };

                            if (counts[0] + counts[1] + counts[2] + counts[3] + counts[4] + counts[5] == 0)
                            {
                                return JSONHelper.FromString(false, "手机号未登记，请联系物管");
                            }
                            if (counts[0] + counts[1] + counts[2] != 0 && counts[3] + counts[4] + counts[5] == 0)
                            {
                                return JSONHelper.FromString(false, "此园区暂未开通，敬请期待");
                            }
                        }
                    }
                }
                catch
                {
                    return JSONHelper.FromString(false, "获取验证码失败");
                }
            }

            Tb_Sms_Account smsModel = SmsInfo.GetSms_Account();
            Tb_SendMessageRecord m = new Tb_SendMessageRecord();
            string errorMessage = "";
            int Result = 0;

            // 泰禾
            try
            {
                if (HttpContext.Current.Request.Url.Host.ToLower().Contains("tahoecn"))
                {
                    Result = SendShortMessage(Row["Mobile"].ToString(), Content, out errorMessage, 1970);
                }
                else
                {
                    Result = SendShortMessage(Row["Mobile"].ToString(), Content, out errorMessage);
                }
            }
            catch (Exception ex)
            {
                GetLog().Info("验证码发送失败:" + ex.Message + ex.StackTrace);
                return JSONHelper.FromString(false, "发送失败，请稍后重试");
            }

            if (Result == 0)
            {
                return JSONHelper.FromString(true, (code * 369).ToString());
            }
            else
            {
                return JSONHelper.FromString(false, errorMessage);
            }
        }

        /// <summary>
        /// 注册用户
        /// </summary>
        public string RegisterUser(DataRow Row)
        {
            if (!Row.Table.Columns.Contains("Mobile") || string.IsNullOrEmpty(Row["Mobile"].ToString()))
            {
                return JSONHelper.FromString(false, "缺少Mobile参数");
            }

            if (!Row.Table.Columns.Contains("Password") || string.IsNullOrEmpty(Row["Password"].ToString()))
            {
                return JSONHelper.FromString(false, "缺少Password参数");
            }

            string mobile = Row["Mobile"].ToString();
            string password = Row["Password"].ToString();

            string backstr = "注册失败";
            string connString = "";
            string dataSource = "";

            try
            {
                #region App版本判断
                if (HttpContext.Current.Request.Url.Host.Contains("wyerp.tahoecn.com"))
                {
                    // 泰禾正式
                    connString = @"Connect Timeout=100;Connection Lifetime=60;Max Pool Size=2000;Min Pool Size=0;Pooling=true;data source=10.0.102.32;initial catalog=HM_wygl_new_1970;PWD=LF123SPoss;persist security info=False;user id=LFUser;packet size=4096";
                    dataSource = "HM_wygl_new_1970";
                }

                // 泰禾测试环境
                else if (HttpContext.Current.Request.Url.Host.Contains("test.wyerp.tahoecndemo.com"))
                {
                    connString = @"Connect Timeout=100;Connection Lifetime=60;Max Pool Size=2000;Min Pool Size=0;Pooling=true;data source=(local);initial catalog=HM_wygl_new_1970;PWD=LF123SPoss;persist security info=False;user id=LFUser;packet size=4096";
                    dataSource = "HM_wygl_new_1970";
                }

                // 俊发版本
                else if (HttpContext.Current.Request.Url.Host.Contains("120.79.213.157"))
                {
                    connString = @"Connect Timeout=100;Connection Lifetime=60;Max Pool Size=2000;Min Pool Size=0;Pooling=true;data source=172.18.148.25;initial catalog=HM_wygl_new_1985;PWD=LF123SPoss;persist security info=False;user id=LFUser;packet size=4096";
                    dataSource = "HM_wygl_new_1985";
                }

                // 海亮版本
                else if (HttpContext.Current.Request.Url.Host.Contains("hlland"))
                {
                    connString = @"Connect Timeout=100;Connection Lifetime=60;Max Pool Size=2000;Min Pool Size=0;Pooling=true;data source=(local);initial catalog=HM_wygl_new_2021;PWD=LF123SPoss;persist security info=False;user id=LFUser;packet size=4096";
                    dataSource = "HM_wygl_new_2021";
                }

                // 大发
                else if (HttpContext.Current.Request.Url.Host.Contains("dafa"))
                {
                    connString = @"Connect Timeout=100;Connection Lifetime=60;Max Pool Size=2000;Min Pool Size=0;Pooling=true;data source=(local);initial catalog=HM_wygl_new_2046;PWD=LF123SPoss;persist security info=False;user id=LFUser;packet size=4096";
                    dataSource = "HM_wygl_new_2046";
                }

                // 嘉和
                else if (HttpContext.Current.Request.Url.Host.Contains("180.139.137.50"))
                {
                    connString = @"Connect Timeout=100;Connection Lifetime=60;Max Pool Size=2000;Min Pool Size=0;Pooling=true;data source=192.168.1.42;initial catalog=HM_wygl_new_2046;PWD=LF123SPoss;persist security info=False;user id=LFUser;packet size=4096";
                    dataSource = "HM_wygl_new_2045";
                }

                // 鸿坤
                else if (HttpContext.Current.Request.Url.Host.Contains("wyyth-app.hongkun.com.cn"))
                {
                    connString = @"Connect Timeout=100;Connection Lifetime=60;Max Pool Size=2000;Min Pool Size=0;Pooling=true;data source=192.168.0.174;initial catalog=HM_wygl_new_1973;PWD=LF123SPoss;persist security info=False;user id=LFUser;packet size=4096";
                    dataSource = "HM_wygl_new_1973";
                }

                // 鸿坤测试
                else if (HttpContext.Current.Request.Url.Host.Contains("wuyth-test.hongkun.com.cn"))
                {
                    connString = @"Connect Timeout=100;Connection Lifetime=60;Max Pool Size=2000;Min Pool Size=0;Pooling=true;data source=192.168.0.236;initial catalog=HM_wygl_new_test;PWD=LF123SPoss;persist security info=False;user id=LFUser;packet size=4096";
                    dataSource = "HM_wygl_new_test";
                }

                // 金帝物业
                else if (HttpContext.Current.Request.Url.Host.Contains("218.56.176.26"))
                {
                    connString = @"Connect Timeout=100;Connection Lifetime=60;Max Pool Size=2000;Min Pool Size=0;Pooling=true;data source=218.56.176.27;initial catalog=HM_wygl_new_2091;PWD=LF123SPoss;persist security info=False;user id=LFUser;packet size=4096";
                    dataSource = "HM_wygl_new_2091";
                }

                // 润达
                else if (HttpContext.Current.Request.Url.Host.Contains("106.46.169.14"))
                {
                    connString = @"Connect Timeout=100;Connection Lifetime=60;Max Pool Size=2000;Min Pool Size=0;Pooling=true;data source=106.46.169.14;initial catalog=HM_wygl_new_2091;PWD=LF123SPoss;persist security info=False;user id=LFUser;packet size=4096";
                    dataSource = "HM_wygl_new_2091";
                }

                // 和家
                else if (HttpContext.Current.Request.Url.Host.Contains("47.105.34.12"))
                {
                    connString = @"Connect Timeout=100;Connection Lifetime=60;Max Pool Size=2000;Min Pool Size=0;Pooling=true;data source=47.104.233.12;initial catalog=Erp_Wygl_6008;PWD=LF123SPoss;persist security info=False;user id=LFUser;packet size=4096";
                    dataSource = "Erp_Wygl_6008";
                }

                // 丽创
                else if (HttpContext.Current.Request.Url.Host.Contains("14.23.45.58"))
                {
                    connString = @"Connect Timeout=100;Connection Lifetime=60;Max Pool Size=2000;Min Pool Size=0;Pooling=true;data source=172.16.200.21;initial catalog=HM_wygl_new_1329;PWD=LF123SPoss;persist security info=False;user id=LFUser;packet size=4096";
                    dataSource = "HM_wygl_new_1329";
                }

                // 珠光
                else if (HttpContext.Current.Request.Url.Host.Contains("ygapp.zgproperty.cn"))
                {
                    connString = @"Connect Timeout=100;Connection Lifetime=60;Max Pool Size=2000;Min Pool Size=0;Pooling=true;data source=172.16.0.8;initial catalog=HM_wygl_new_1329;PWD=LF123SPoss;persist security info=False;user id=LFUser;packet size=4096";
                    dataSource = "HM_wygl_new_1329";
                }


                // 中集
                else if (HttpContext.Current.Request.Url.Host.Contains("120.79.228.111"))
                {
                    connString = @"Connect Timeout=100;Connection Lifetime=60;Max Pool Size=2000;Min Pool Size=0;Pooling=true;data source=120.79.228.111,8433;initial catalog=HM_wygl_new_1953;PWD=LF123SPoss;persist security info=False;user id=LFUser;packet size=4096";
                    dataSource = "HM_wygl_new_1953";
                }
                //else if (HttpContext.Current.Request.Url.Host.Contains("localhost"))
                //{
                //    connString = @"Connect Timeout=100;Connection Lifetime=60;Max Pool Size=2000;Min Pool Size=0;Pooling=true;data source=120.79.228.111,8433;initial catalog=HM_wygl_new_1953;PWD=LF123SPoss;persist security info=False;user id=LFUser;packet size=4096";
                //    dataSource = "HM_wygl_new_1953";
                //}


                else if (HttpContext.Current.Request.Url.Host.Contains("47.107.176.138"))
                {
                    connString = @"Connect Timeout=100;Connection Lifetime=60;Max Pool Size=2000;Min Pool Size=0;Pooling=true;data source=47.107.176.138;initial catalog=PMS_Base;PWD=LF123SPoss;persist security info=False;user id=LFUser;packet size=4096";
                    dataSource = "PMS_Base";
                }
                #endregion 

                if (string.IsNullOrEmpty(connString) || string.IsNullOrEmpty(dataSource))
                {
                    return JSONHelper.FromString(false, "未配置ERP信息");
                }

                DataTable dTable = new DbHelperSQLP(PubConstant.UnifiedContionString).Query("SELECT Name FROM Tb_User where Mobile='" + Row["Mobile"].ToString() + "'").Tables[0];
                if (dTable.Rows.Count > 0)
                {
                    return JSONHelper.FromString(false, "此用户已存在");
                }

                MobileSoft.Model.Unified.Tb_User User = new MobileSoft.Model.Unified.Tb_User();
                User.Id = Guid.NewGuid().ToString();
                User.Name = mobile;
                User.Mobile = mobile;
                User.Pwd = password;
                User.NickName = mobile;
                User.Sex = 1;
                User.RegDate = DateTime.Now;
                new MobileSoft.BLL.Unified.Bll_Tb_User().Add(User);

                using (var erpConn = new SqlConnection(connString))
                {
                    var holds = new List<long>();

                    // 1、查询客户信息
                    var sql = $@"SELECT CustID FROM Tb_HSPR_Customer 
                                 WHERE (MobilePhone LIKE '%{mobile}%' OR LinkmanTel LIKE '%{mobile}%') AND IsDelete=0";

                    var custId = erpConn.Query<long>(sql, null).FirstOrDefault();
                    if (custId != 0)
                    {
                        sql = @"SELECT a.*, b.CustName,b.MobilePhone,b.CommID,c.RoomSign,c.RoomName  
                                FROM Tb_HSPR_CustomerLive a 
                                LEFT JOIN Tb_HSPR_Customer b ON a.CustID=b.CustID 
                                LEFT JOIN Tb_HSPR_Room c ON a.RoomID=c.RoomID 
                                WHERE a.CustID=@CustID AND a.RoomID>=1 AND isnull(a.IsActive,0)=1 AND isnull(a.IsDelLive,0)=0";

                        // 查询客户拥有的房屋
                        var rooms = erpConn.Query(sql, new { CustID = custId });
                        if (rooms.Count() > 0)
                        {
                            foreach (dynamic roomInfo in rooms)
                            {
                                // 判断客户信息是否已经插入到家庭成员表
                                sql = @"SELECT HoldID FROM Tb_HSPR_Household 
                                        WHERE CustID=@CustID AND RoomID=@RoomID 
                                        AND (MobilePhone like @MobilePhone OR LinkManTel like @MobilePhone) AND isnull(IsDelete,0)=0";

                                var holdId = erpConn.Query<long>(sql, new { CustID = custId, RoomID = roomInfo.RoomID, MobilePhone = $"%{mobile}%" }).FirstOrDefault();
                                if (holdId == 0)
                                {
                                    // 判断客户身份
                                    var identity = "";
                                    if (roomInfo.LiveType == 1)     // 业主
                                        identity = UserRoomIdentityCode.Customer2;
                                    if (roomInfo.LiveType == 2)     // 租户
                                        identity = UserRoomIdentityCode.Tenant;

                                    // 客户信息插入到家庭成员表
                                    var parameters = new DynamicParameters();
                                    parameters.Add("@HoldID", 0, DbType.Int64, ParameterDirection.Output);
                                    parameters.Add("@CommID", roomInfo.CommID);
                                    parameters.Add("@CustID", roomInfo.CustID);
                                    parameters.Add("@RoomID", roomInfo.RoomID);
                                    parameters.Add("@Name", roomInfo.CustName);
                                    parameters.Add("@MobilePhone", mobile);
                                    parameters.Add("@Relationship", identity);
                                    parameters.Add("@IsDelete", 0);

                                    erpConn.Execute("Proc_HSPR_Household_Insert_Phone", parameters, null, null, CommandType.StoredProcedure);

                                    // 家庭成员表id
                                    holdId = parameters.Get<long>("@HoldID");
                                }

                                holds.Add(holdId);

                                // 自动绑定房屋
                                using (var appConn = new SqlConnection(PubConstant.UnifiedContionString))
                                {
                                    // 小区是否已经添加App系统
                                    sql = @"SELECT * FROM Tb_Community WHERE CommID=@CommID";
                                    var commInfo = appConn.Query(sql, new { CommID = roomInfo.CommID }).FirstOrDefault();
                                    if (commInfo != null)
                                    {
                                        sql = @"INSERT INTO Tb_User_Relation
                                                    (Id,UserId,CommunityId,CustId,CustHoldId,RoomId,RegDate,CustName,RoomSign,CustMobile)
                                                VALUES(newid(),@UserId,@CommunityId,@CustID,@HoldID,@RoomID,getdate(),@CustName,@RoomSign,@CustMobile)";

                                        appConn.Execute(sql, new
                                        {
                                            UserId = User.Id,
                                            CommunityId = commInfo.Id,
                                            CustID = custId,
                                            HoldID = holdId,
                                            RoomID = roomInfo.RoomID,
                                            CustName = roomInfo.CustName,
                                            RoomSign = (roomInfo.RoomName ?? roomInfo.RoomSign),
                                            CustMobile = mobile
                                        });
                                    }
                                }
                            }
                        }
                    }

                    // 2、查询家庭成员表
                    sql = @"SELECT a.HoldID,a.CommID,a.CustID,b.CustName,b.MobilePhone AS CustMobile,a.RoomID,c.RoomSign,c.RoomName 
                            FROM Tb_HSPR_Household a 
                            LEFT JOIN Tb_HSPR_Customer b ON a.CustID=b.CustID
                            LEFT JOIN Tb_HSPR_Room c ON a.RoomID=c.RoomID
                            WHERE (a.MobilePhone like @MobilePhone OR a.LinkManTel like @MobilePhone) AND isnull(a.IsDelete,0)=0";

                    if (holds.Count != 0)
                    {
                        sql += $" AND a.HoldID NOT IN({ string.Join(",", holds) })";
                    }

                    var holdsInfo = erpConn.Query(sql, new { MobilePhone = $"%{mobile}%" });
                    if (holdsInfo.Count() > 0)
                    {
                        // 自动绑定房屋
                        using (var appConn = new SqlConnection(PubConstant.UnifiedContionString))
                        {
                            foreach (var holdInfo in holdsInfo)
                            {
                                // 小区是否已经添加App系统
                                sql = @"SELECT * FROM Tb_Community WHERE CommID=@CommID";
                                var commInfo = appConn.Query(sql, new { CommID = holdInfo.CommID }).FirstOrDefault();
                                if (commInfo != null)
                                {
                                    sql = @"INSERT INTO Tb_User_Relation
                                                    (Id,UserId,CommunityId,CustId,CustHoldId,RoomId,RegDate,CustName,RoomSign,CustMobile)
                                                VALUES(newid(),@UserId,@CommunityId,@CustID,@HoldID,@RoomID,getdate(),@CustName,@RoomSign,@CustMobile)";

                                    appConn.Execute(sql, new
                                    {
                                        UserId = User.Id,
                                        CommunityId = commInfo.Id,
                                        CustID = custId,
                                        HoldID = holdInfo.HoldID,
                                        RoomID = holdInfo.RoomID,
                                        CustName = holdInfo.CustName,
                                        RoomSign = (holdInfo.RoomName ?? holdInfo.RoomSign),
                                        CustMobile = holdInfo.CustMobile
                                    });
                                }
                            }
                        }
                    }

                    return JSONHelper.FromString(true, "注册成功");
                }
            }
            catch (Exception ex)
            {
                backstr = ex.Message;
            }

            return JSONHelper.FromString(false, backstr);
        }

        /// <summary>
        /// 忘记密码
        /// </summary>
        private string UpdateUserPwd(DataRow Row)
        {
            if (!Row.Table.Columns.Contains("Mobile") || string.IsNullOrEmpty(Row["Mobile"].ToString()))
            {
                return JSONHelper.FromString(false, "缺少Mobile参数");
            }

            if (!Row.Table.Columns.Contains("Password") || string.IsNullOrEmpty(Row["Password"].ToString()))
            {
                return JSONHelper.FromString(false, "缺少Password参数");
            }

            string mobile = Row["Mobile"].ToString();
            string password = Row["Password"].ToString();

            string backstr = "修改密码失败";
            try
            {
                DataTable dTable = new DbHelperSQLP(PubConstant.UnifiedContionString.ToString()).Query("SELECT * FROM Tb_User where Mobile='" + Row["Mobile"].ToString() + "'").Tables[0];
                if (dTable.Rows.Count == 0)
                {
                    return JSONHelper.FromString(false, "此用户不存在");
                }

                new DbHelperSQLP(PubConstant.UnifiedContionString.ToString()).ExecuteSql(string.Format("UPDATE Tb_User SET Pwd='{0}' WHERE Mobile='{1}'", password, mobile));

                return JSONHelper.FromString(true, "密码修改成功");
            }
            catch (Exception ex)
            {
                backstr = ex.Message;
            }

            return JSONHelper.FromString(false, backstr);
        }

        /// <summary>
        /// 邀请用户
        /// </summary>
        private string InviteUser(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return JSONHelper.FromString(false, "缺少CommunityId参数");
            }
            if (!row.Table.Columns.Contains("UserMobile") || string.IsNullOrEmpty(row["UserMobile"].ToString()))
            {
                return JSONHelper.FromString(false, "缺少UserMobile参数");
            }
            if (!row.Table.Columns.Contains("InviteMobile") || string.IsNullOrEmpty(row["InviteMobile"].ToString()))
            {
                return JSONHelper.FromString(false, "缺少InviteMobile参数");
            }
            if (!row.Table.Columns.Contains("InviteName") || string.IsNullOrEmpty(row["InviteName"].ToString()))
            {
                return JSONHelper.FromString(false, "缺少InviteName参数");
            }
            if (!row.Table.Columns.Contains("InviteType") || string.IsNullOrEmpty(row["InviteType"].ToString()))
            {
                return JSONHelper.FromString(false, "缺少InviteType参数");
            }

            var communityId = row["CommunityId"].AsString();
            var userMobile = row["UserMobile"].ToString();
            var inviteMobile = row["InviteMobile"].ToString();
            var inviteName = row["InviteName"].ToString();
            var inviteType = row["InviteType"].ToString();
            var relationship = UserRoomIdentityCode.Other;

            var roomId = 0L;
            var relationId = default(string);
            if (row.Table.Columns.Contains("RelationId") && !string.IsNullOrEmpty(row["RelationId"].ToString()))
            {
                relationId = row["RelationId"].ToString();
            }
            if (row.Table.Columns.Contains("RoomID") && !string.IsNullOrEmpty(row["RoomID"].ToString()))
            {
                roomId = AppGlobal.StrToLong(row["RoomID"].ToString());
            }

            if (roomId == 0 && string.IsNullOrEmpty(relationId))
            {
                return JSONHelper.FromString(false, "未指定绑定房屋");
            }

            if (inviteType == ((int)UserRoomIdentity.Customer).ToString())
            {
                relationship = UserRoomIdentityCode.Customer2;
            }
            else if (inviteType == ((int)UserRoomIdentity.FamilyMember).ToString())
            {
                relationship = UserRoomIdentityCode.FamilyMember;
            }
            else if (inviteType == ((int)UserRoomIdentity.Tenant).ToString())
            {
                relationship = UserRoomIdentityCode.Tenant;
            }

            //查询小区
            Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(communityId);
            //构造链接字符串
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }

            string strcon = GetConnectionStringStr(Community);
            string message;

            using (var appConn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                var sql = @"SELECT CommName FROM Tb_Community WHERE Id=@Id";
                var commName = appConn.Query<string>(sql, new { Id = communityId }).FirstOrDefault();

                if (roomId == 0)
                {
                    sql = @"SELECT CustId,RoomId,RoomSign,CustName FROM Tb_User_Relation WHERE Id=@RelationId";
                }
                else
                {
                    sql = @"SELECT CustId,RoomId,RoomSign,CustName FROM Tb_User_Relation WHERE UserId IN(SELECT Id FROM Tb_User WHERE Mobile=@Mobile)";
                }

                var relationInfo = appConn.Query(sql, new { RelationId = relationId, Mobile = userMobile }).FirstOrDefault();
                if (relationInfo == null)
                {
                    return JSONHelper.FromString(false, "该房屋所属业主还未注册到业主App");
                }

                var custId = AppGlobal.StrToLong(relationInfo.CustId);
                var roomSign = relationInfo.RoomSign?.ToString();
                var custName = relationInfo.CustName?.ToString();

                var holdId = 0L;

                // 判断客户信息是否已经插入到家庭成员表
                using (var erpConn = new SqlConnection(strcon))
                {
                    sql = @"SELECT HoldID FROM Tb_HSPR_Household 
                                WHERE CustID=@CustID AND RoomID=@RoomID 
                                AND (MobilePhone like @MobilePhone OR LinkManTel like @MobilePhone) AND isnull(IsDelete,0)=0";

                    holdId = erpConn.Query<long>(sql, new { CustID = custId, RoomID = roomId, MobilePhone = $"%{inviteMobile}%" }).FirstOrDefault();
                    if (holdId == 0)
                    {
                        // 客户信息插入到家庭成员表
                        var parameters = new DynamicParameters();
                        parameters.Add("@HoldID", 0, DbType.Int64, ParameterDirection.Output);
                        parameters.Add("@CommID", Community.CommID);
                        parameters.Add("@CustID", custId);
                        parameters.Add("@RoomID", roomId);
                        parameters.Add("@Name", inviteName);
                        parameters.Add("@MobilePhone", inviteMobile);
                        parameters.Add("@Relationship", relationship);
                        parameters.Add("@IsDelete", 0);

                        erpConn.Execute("Proc_HSPR_Household_Insert_Phone", parameters, null, null, CommandType.StoredProcedure);

                        // 家庭成员表id
                        holdId = parameters.Get<long>("@HoldID");
                    }
                }

                var userInfo = new MobileSoft.Model.Unified.Tb_User();
                var isAdd = false;

                // 判断用户是否已经注册
                sql = "SELECT Id FROM Tb_User WHERE Mobile=@Mobile";
                userInfo.Id = appConn.Query<string>(sql, new { Mobile = inviteMobile }).FirstOrDefault();
                if (userInfo.Id == null)
                {
                    isAdd = true;

                    // 注册用户
                    userInfo.Id = Guid.NewGuid().ToString();
                    userInfo.Name = inviteName;
                    userInfo.Mobile = inviteMobile;
                    userInfo.Pwd = "123456";
                    userInfo.NickName = inviteMobile;
                    userInfo.Sex = 1;
                    userInfo.RegDate = DateTime.Now;
                    new MobileSoft.BLL.Unified.Bll_Tb_User().Add(userInfo);
                }

                // 查询用户是否已经绑定该房屋
                sql = @"SELECT Id FROM Tb_User_Relation WHERE UserId=@UserId AND RoomId=@RoomID";
                var hasBinded = appConn.Query(sql, new { UserId = userInfo.Id, RoomID = roomId }).Count();
                if (hasBinded > 0)
                {
                    // 已经绑定，解锁，更新家庭成员id
                    sql = @"UPDATE Tb_User_Relation SET Locked=0,CustHoldId=@HoldID WHERE UserId=@UserId AND RoomId=@RoomID";
                    appConn.Execute(sql, new { UserId = userInfo.Id, RoomID = roomId, HoldID = holdId });
                }
                else
                {
                    // 未绑定，绑定房屋
                    sql = @"INSERT INTO Tb_User_Relation(Id,UserId,CommunityId,CustId,RoomId,RegDate,CustName,RoomSign,CustMobile,Locked) 
                            VALUES(@Id,@UserId,@CommunityId,@CustId,@RoomId,getdate(),@CustName,@RoomSign,@CustMobile,0)";

                    appConn.Execute(sql, new
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserId = userInfo.Id,
                        CommunityId = communityId,
                        CustId = custId,
                        RoomId = roomId,
                        CustName = custName,
                        RoomSign = roomSign,
                        CustMobile = userMobile
                    });
                }

                // 短信内容
                if (Community.CorpID == 1975)
                {
                    message = string.Format("\"{0}\"业主{1}为您绑定了编号为：{2}的房屋，关注微信公众号“第一亚太物业”，您可以对该房屋进行报事和缴费等操作。",
                            commName, custName, roomSign);

                    if (isAdd)
                    {
                        message = string.Format("\"{0}\"业主{1}邀请您关注微信公众号“第一亚太物业”随时掌握房屋、小区信息，您的账号为：{2}，初始密码为：123456。",
                            commName, custName, inviteMobile);
                    }
                }
                else if (Community.CorpID == 1970)
                {
                    message = string.Format("温馨提示：\"{0}\"业主{1}为您绑定了编号为：{2}房屋，您可以对该房屋进行报事和缴费等操作。",
                        commName, custName, roomSign);
                    if (isAdd)
                    {
                        message = string.Format(@"温馨提示：“{0}”业主{1}邀请您使用泰禾业主App，您的账号为：{2}，初始密码为：123456，请及时下载登录泰禾App并修改密码,下载地址：http://t.cn/Rdo20xY", 
                            commName, custName, inviteMobile);
                    }
                }
                else
                {
                    message = string.Format("温馨提示：\"{0}\"业主{1}为您绑定了编号为：{2}的房屋，您可以对该房屋进行报事和缴费等操作。",
                        commName, custName, roomSign);
                    if (isAdd)
                    {
                        message = string.Format("温馨提示：“{0}”业主{1}为您绑定了编号为：{2}的房屋，您的账号为：{3}，初始密码为：123456，您可以对该房屋进行报事和缴费等操作。",
                            commName, custName, roomSign, inviteMobile);
                    }
                }

                SendShortMessage(inviteMobile, message, out string errorMessage, Community.CorpID);
            }

            return JSONHelper.FromString(true, "邀请成功");
        }
    }
}
