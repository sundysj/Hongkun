using Common;
using Dapper;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using MobileSoft.Model.Unified;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using static Dapper.SqlMapper;
namespace Business
{
    public class PMSAppRoomManage : PubInfo
    {
        class CustomerIdentity
        {
            public long CustID { get; set; }
            public long HoldID { get; set; }
            public string CustName { get; set; }
            public string CustMobile { get; set; }
            public string Linkman { get; set; }
            public string LinkmanTel { get; set; }
            public string CustPapers { get; set; }
            public int LiveType { get; set; }
            public string Relationship { get; set; }
        }

        public PMSAppRoomManage()
        {
            base.Token = "20200106PMSAppRoomManage";
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
                    case "GetAppBindingRoomControl":
                        Trans.Result = GetAppBindingRoomControl(Row);
                        break;
                    case "BindingRoom":
                        Trans.Result = BindingRoom(Row);
                        break;
                    case "AutoBindingRoom":
                        Trans.Result = AutoBindingRoom(Row);
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
        /// 获取房屋绑定管控设置
        /// </summary>
        private string GetAppBindingRoomControl(DataRow row)
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

            return new ApiResult(true, GetAppBindingRoomControl(community.CorpID, community.Id)).toJson();
        }

        /// <summary>
        /// 获取房屋绑定管控设置
        /// </summary>
        private Tb_Control_AppBindingRoom GetAppBindingRoomControl(int corpId, string communityId)
        {
            using (var conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                var control = Tb_Control_AppBindingRoom.Default;

                var sql = @"SELECT convert(varchar(10),object_id(N'Tb_Control_AppBindingRoom',N'U'));";
                var tag = conn.Query<string>(sql).FirstOrDefault();

                if (!string.IsNullOrEmpty(tag))
                {
                    sql = @"SELECT * FROM
                            (
                                SELECT * FROM Tb_Control_AppBindingRoom WHERE CorpID=@CorpID AND ltrim(rtrim(isnull(CommunityID,'')))=''
                                UNION ALL
                                SELECT * FROM Tb_Control_AppBindingRoom WHERE CorpID=@CorpID AND CommunityID IS NOT NULL
                                    AND CommunityID LIKE @CommunityID
                            ) AS t ORDER BY t.CommunityID DESC;";

                    control = conn.Query<Tb_Control_AppBindingRoom>(sql, new { CorpID = corpId, CommunityID = $"%{communityId}%" }).FirstOrDefault();
                }

                return control ?? Tb_Control_AppBindingRoom.Default;
            }
        }

        /// <summary>
        /// 手动绑定房屋
        /// </summary>
        private string BindingRoom(DataRow row)
        {
            if (!row.Table.Columns.Contains("UserID") || string.IsNullOrEmpty(row["UserID"].AsString()))
            {
                return new ApiResult(false, "缺少参数UserID").toJson();
            }
            if (!row.Table.Columns.Contains("RoomID") || string.IsNullOrEmpty(row["RoomID"].AsString()))
            {
                return new ApiResult(false, "缺少参数RoomID").toJson();
            }
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].AsString()))
            {
                return new ApiResult(false, "缺少参数CommunityId").toJson();
            }

            var userId = row["UserID"].AsString();
            var roomId = AppGlobal.StrToLong(row["RoomID"].AsString());
            var communityId = row["CommunityId"].AsString();

            var community = GetCommunity(communityId);
            if (community == null)
            {
                return JSONHelper.FromString(false, "未查询到小区信息");
            }

            // 读取当前用户身份标识及关联客户CustID
            var userIdentity = GetUserIdentity(community, userId, roomId, out long custId, out long holdId);

            // 绑定管控
            var control = GetAppBindingRoomControl(community.CorpID, community.Id);
            if (control.CheckCustName == false && control.CheckCustMobile == false && control.CheckCustPapersInfo == false)
            {
                if (userIdentity == UserRoomIdentity.FamilyMember && control.AllowHouseholdBind == false)
                    return JSONHelper.FromString(false, "抱歉，此房屋暂不支持家庭成员绑定");

                if (userIdentity == UserRoomIdentity.Tenant && control.AllowLesseeBind == false)
                    return JSONHelper.FromString(false, "抱歉，此房屋暂不支持租户绑定");

                if (userIdentity == UserRoomIdentity.Other && control.AllowStrangerBind == false)
                    return JSONHelper.FromString(false, "抱歉，此房屋暂不支持陌生人绑定");
            }

            using (var erpConn = new SqlConnection(GetConnectionStr(community)))
            {
                var chars = new char[] { ',', '，', '、', '/', '／', '\\', '＼', '|', '｜', ' ', ';', '；', ':', '：', '.', '．', '。' };

                var tmp = new CustomerIdentity[3];

                var sql = @"SELECT a.CustID,a.LiveType,
                                ltrim(rtrim(isnull(b.CustName,''))) AS CustName,
                                ltrim(rtrim(isnull(b.MobilePhone,''))) AS CustMobile,
                                ltrim(rtrim(isnull(b.Linkman,''))) AS Linkman,
                                ltrim(rtrim(isnull(b.LinkmanTel,''))) AS LinkmanTel,
                                ltrim(rtrim(isnull(b.PaperCode,''))) AS CustPapers 
                            FROM Tb_HSPR_CustomerLive a
                            LEFT JOIN Tb_HSPR_Customer b ON a.CustID=b.CustID
                            WHERE a.RoomID=@RoomID AND isnull(a.IsDelLive,0)=0 
                            ORDER BY a.IsActive DESC,a.LiveType ASC;";

                var custs = erpConn.Query<CustomerIdentity>(sql, new { RoomID = roomId }).ToList();
                if (custs.Count == 0)
                {
                    return JSONHelper.FromString(false, "该房屋暂未关联客户，无法绑定");
                }

                // 需要验证客户姓名
                if (control.CheckCustName)
                {
                    if (!row.Table.Columns.Contains("CustName") || string.IsNullOrEmpty(row["CustName"].AsString()))
                    {
                        return JSONHelper.FromString(false, "客户姓名不能为空");
                    }

                    tmp[0] = custs.Find(obj =>
                    {
                        if (!string.IsNullOrEmpty(obj.CustName))
                        {
                            if (obj.CustName.Split(chars, StringSplitOptions.RemoveEmptyEntries).Contains(row["CustName"].AsString()))
                            {
                                return true;
                            }
                        }

                        if (!string.IsNullOrEmpty(obj.Linkman))
                        {
                            if (obj.Linkman.Split(chars, StringSplitOptions.RemoveEmptyEntries).Contains(row["CustName"].AsString()))
                            {
                                return true;
                            }
                        }

                        return false;
                    });

                    if (tmp[0] == null)
                    {
                        return JSONHelper.FromString(false, "客户姓名验证失败");
                    }
                }

                // 需要验证客户手机号
                if (control.CheckCustMobile)
                {
                    if (!row.Table.Columns.Contains("CustMobile") || string.IsNullOrEmpty(row["CustMobile"].AsString()))
                    {
                        return JSONHelper.FromString(false, "客户手机号不能为空");
                    }

                    tmp[1] = custs.Find(obj =>
                    {
                        if (!string.IsNullOrEmpty(obj.CustMobile))
                        {
                            if (obj.CustMobile.Split(chars, StringSplitOptions.RemoveEmptyEntries).Contains(row["CustMobile"].AsString()))
                            {
                                return true;
                            }
                        }

                        if (!string.IsNullOrEmpty(obj.LinkmanTel))
                        {
                            if (obj.LinkmanTel.Split(chars, StringSplitOptions.RemoveEmptyEntries).Contains(row["CustMobile"].AsString()))
                            {
                                return true;
                            }
                        }

                        return false;
                    });

                    if (tmp[1] == null)
                    {
                        return JSONHelper.FromString(false, "客户手机号验证失败");
                    }
                }

                // 需要验证客户证件号码
                if (control.CheckCustPapersInfo)
                {
                    if (!row.Table.Columns.Contains("CustPapers") || string.IsNullOrEmpty(row["CustPapers"].AsString()))
                    {
                        return JSONHelper.FromString(false, "客户证件号码不能为空");
                    }

                    String custPapers = row["CustPapers"].ToString();
                    //如果参数仅有四位 则验证后四位
                    if (!String.IsNullOrEmpty(custPapers) && custPapers.Length == 4)
                    {
                        tmp[2] = custs.Find(obj =>
                        {
                            if (!string.IsNullOrEmpty(obj.CustPapers))
                            {
                                var array = obj.CustPapers.Split(chars, StringSplitOptions.RemoveEmptyEntries);
                                foreach (var model in array)
                                {
                                    if (model.ToLower().EndsWith(custPapers.ToLower()))
                                    {
                                        return true;
                                    }
                                }
                            }
                            return false;
                        });
                    }
                    else
                    {
                        tmp[2] = custs.Find(obj =>
                        {
                            if (!string.IsNullOrEmpty(obj.CustPapers))
                            {
                                if (obj.CustPapers.Split(chars, StringSplitOptions.RemoveEmptyEntries).Contains(custPapers))
                                {
                                    return true;
                                }
                            }

                            return false;
                        });
                    }
                    if (tmp[2] == null)
                    {
                        return JSONHelper.FromString(false, "客户证件号码验证失败");
                    }
                }

                tmp = tmp.Where(obj => obj != null).ToArray();
                var codes = tmp.Select(obj => obj.GetHashCode());

                // 如果存在多个，说明验证是在多个客户之间完成（如：满足客户1姓名 + 满足客户2手机号），不允许
                if (codes.Distinct().Count() != 1)
                {
                    return JSONHelper.FromString(false, "客户信息验证失败");
                }

                return BindingRoom(control, community, userId, tmp[0].CustID, roomId);
            }

        }

        /// <summary>
        /// 手动绑定房屋
        /// </summary>
        private string BindingRoom(Tb_Control_AppBindingRoom control, Tb_Community community, string userId, long custId, long roomId)
        {
            using (var appConn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                // 用户信息
                var sql = @"SELECT Mobile,
                                CASE WHEN Name IS NULL OR len(ltrim(rtrim(Name)))=0 THEN NickName 
                                    ELSE Name END AS Name
                            FROM Tb_User WHERE Id=@Id;";

                var userInfo = appConn.Query(sql, new { Id = userId }).FirstOrDefault();
                var userName = userInfo.Name;
                var userMobile = userInfo.Mobile;

                using (var erpConn = new SqlConnection(GetConnectionStr(community)))
                {
                    // 房屋名称
                    sql = @"SELECT isnull(RoomName,isnull(RoomSign,'')) FROM Tb_HSPR_Room WHERE RoomID=@RoomID;";
                    var roomName = erpConn.Query<string>(sql, new { RoomID = roomId }).FirstOrDefault();

                    // 客户信息
                    sql = "SELECT CustID,CustName,MobilePhone,LinkmanTel FROM Tb_HSPR_Customer WHERE CustID=@CustID;";

                    var custInfo = erpConn.Query(sql, new { CustID = custId }).FirstOrDefault();

                    var relationShip = UserRoomIdentityCode.FamilyMember;
                    var custName = custInfo.CustName;
                    var custMobile = default(string);
                    if (custInfo.CustMobile != null && custInfo.CustMobile.Contains(userMobile))
                    {
                        custMobile = custInfo.CustMobile;
                        relationShip = UserRoomIdentityCode.Customer2;
                    }
                    else if (custInfo.LinkmanTel != null && custInfo.LinkmanTel.Contains(userMobile))
                    {
                        custMobile = custInfo.LinkmanTel;
                        relationShip = UserRoomIdentityCode.Customer2;
                    }

                    // 绑定房屋
                    sql = @"IF exists(SELECT * FROM Tb_User_Relation WHERE UserId=@UserId AND CommunityId=@CommunityId AND RoomId=@RoomId)
                                UPDATE Tb_User_Relation SET CustId=@CustId,CustName=@CustName,Locked=0
                                WHERE UserId=@UserId AND CommunityId=@CommunityId AND RoomId=@RoomID;
                            ELSE
                                BEGIN 
                                    INSERT INTO Tb_User_Relation(Id,UserId,CommunityId,CustId,CustHoldId,RoomId,
                                        RegDate,CustName,RoomSign,CustMobile,Locked)
                                    VALUES(@Id,@UserId,@CommunityId,@CustId,@CustHoldId,@RoomId,getdate(),@CustName,@RoomSign,@CustMobile,0);
                                END;";

                    appConn.Execute(sql, new
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserId = userId,
                        CommunityId = community.Id,
                        CustId = custId,
                        CustName = custName,
                        CustMobile = custMobile,
                        RoomId = roomId,
                        RoomSign = roomName,
                        CustHoldId = 0
                    });

                    return JSONHelper.FromString(true, "绑定成功");
                }
            }
        }

        /// <summary>
        /// 获取用户对于某房屋的身份标识
        /// </summary>
        private UserRoomIdentity GetUserIdentity(Tb_Community community, string userId, long roomId, out long custId, out long holdId)
        {
            custId = 0;
            holdId = 0;
            var userMobile = "";

            using (var conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                var sql = @"SELECT Mobile FROM Tb_User WHERE Id=@Id";

                userMobile = conn.Query<string>(sql, new { Id = userId }).FirstOrDefault();
            }

            using (var conn = new SqlConnection(GetConnectionStr(community)))
            {
                // 获取客户信息，可能是业主、租户、家属、临时客户，多个身份时，取最小 LiveType 或最大 HoldID 值
                var sql = @"SELECT a.CustID,b.CustName,a.LiveType, 
                                ltrim(rtrim(isnull(b.MobilePhone,'')+','+isnull(b.LinkmanTel,''))) AS CustMobile
                            FROM Tb_HSPR_CustomerLive a 
                            LEFT JOIN Tb_HSPR_Customer b ON a.CustID=b.CustID AND isnull(b.IsDelete,0)=0
                            WHERE a.RoomID=@RoomID AND isnull(a.IsDelLive,0)=0
                            AND (b.MobilePhone LIKE @Mobile OR b.LinkmanTel LIKE @Mobile)
                            ORDER BY a.IsActive DESC,a.LiveType ASC;

                            SELECT a.HoldID,a.CustID,c.CustName,a.Relationship, 
                                ltrim(rtrim(isnull(c.MobilePhone,'')+','+isnull(c.LinkmanTel,''))) AS CustMobile 
                            FROM Tb_HSPR_Household a
                            LEFT JOIN Tb_HSPR_CustomerLive b ON a.CustID=b.CustID
                            LEFT JOIN Tb_HSPR_Customer c ON a.CustID=c.CustID
                            WHERE a.RoomID=@RoomID AND isnull(a.IsDelete,0)=0 AND isnull(b.IsDelLive,0)=0 AND isnull(c.IsDelete,0)=0
                            AND (a.MobilePhone LIKE @Mobile OR a.LinkmanTel LIKE @Mobile)
                            ORDER BY HoldID DESC;";

                var reader = conn.QueryMultiple(sql, new { RoomID = roomId, Mobile = $"%{userMobile}%" });
                var ci1 = reader.Read<CustomerIdentity>().ToList();
                var ci2 = reader.Read<CustomerIdentity>().ToList();

                var liveType1 = UserRoomIdentity.Other;
                var liveType2 = UserRoomIdentity.Other;

                // 客户信息
                var tmp = ci1.Find(obj => obj.CustMobile != null && obj.CustMobile.Contains(userMobile));
                if (tmp != null)
                {
                    custId = tmp.CustID;

                    if (tmp.LiveType == 3)
                        liveType1 = UserRoomIdentity.Tenant;
                    if (tmp.LiveType == 2)
                        liveType1 = UserRoomIdentity.FamilyMember;
                    if (tmp.LiveType == 1)
                        liveType1 = UserRoomIdentity.Customer;
                }

                // 家属信息
                tmp = ci2.Find(obj => obj.CustMobile != null && obj.CustMobile.Contains(userMobile));
                if (tmp != null)
                {
                    custId = tmp.CustID;

                    if (tmp.Relationship == UserRoomIdentityCode.Tenant)
                        liveType2 = UserRoomIdentity.Tenant;
                    if (tmp.Relationship == UserRoomIdentityCode.FamilyMember)
                        liveType2 = UserRoomIdentity.FamilyMember;
                    if (tmp.Relationship == UserRoomIdentityCode.Customer1 || tmp.Relationship == UserRoomIdentityCode.Customer2)
                        liveType2 = UserRoomIdentity.Customer;

                    // 当客户身份与 UserRoomIdentityCode 中定义的不符时，默认为家属
                    if (liveType2 == UserRoomIdentity.Other)
                        liveType2 = UserRoomIdentity.FamilyMember;
                }

                return (UserRoomIdentity)Math.Min((int)liveType1, (int)liveType2);
            }
        }


        /// <summary>
        /// 自动绑定房屋
        /// </summary>
        private string AutoBindingRoom(DataRow row)
        {
            if (!row.Table.Columns.Contains("UserID") || string.IsNullOrEmpty(row["UserID"].AsString()))
            {
                return new ApiResult(false, "缺少参数UserID").toJson();
            }
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].AsString()))
            {
                return new ApiResult(false, "缺少参数CommunityId").toJson();
            }

            var userId = row["UserID"].AsString();
            var communityId = row["CommunityId"].AsString();

            var community = GetCommunity(communityId);
            if (community == null)
            {
                return JSONHelper.FromString(false, "未查询到小区信息");
            }

            var control = GetAppBindingRoomControl(community.CorpID, community.Id);

            return AutoBindingRoom(control, community, userId);
        }

        /// <summary>
        /// 自动绑定房屋
        /// </summary>
        private string AutoBindingRoom(Tb_Control_AppBindingRoom control, Tb_Community community, string userId)
        {
            using (var appConn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                // 用户手机号
                var sql = @"SELECT Mobile FROM Tb_User WHERE Id=@Id;";
                var userMobile = appConn.Query<string>(sql, new { Id = userId }).FirstOrDefault();

                using (var erpConn = new SqlConnection(GetConnectionStr(community)))
                {
                    try
                    {
                        sql = @"/* 客户(业主、租户、临时客户)信息 */
                                SELECT a.CustID,a.CustName,isnull(a.MobilePhone,a.LinkManTel) AS CustMobile,0 AS HoldID,
                                    c.CommID,isnull(b.RoomID,0) AS RoomID,c.RoomSign,c.RoomName,b.LiveType,
                                    CASE WHEN (isnull(a.IsDelete,0)+isnull(b.IsDelLive,0)+isnull(c.IsDelete,0))>0 THEN 1
                                         ELSE 0 END AS IsDelete, 
                                    isnull(b.IsDelLive,0) AS IsDelLive 
                                FROM Tb_HSPR_Customer a
                                LEFT JOIN Tb_HSPR_CustomerLive b ON a.CustID=b.CustID 
                                LEFT JOIN Tb_HSPR_Room c ON c.RoomID=b.RoomID
                                WHERE a.MobilePhone LIKE @Mobile OR a.LinkManTel LIKE @Mobile;

                                /* 家庭成员信息 */
                                SELECT a.CustID,b.CustName,isnull(a.MobilePhone,a.LinkManTel) AS CustMobile,a.HoldID,
                                    a.CommID,isnull(a.RoomID,0) AS RoomID,d.RoomSign,d.RoomName,c.LiveType,
                                    CASE WHEN (isnull(a.IsDelete,0)+isnull(b.IsDelete,0)+isnull(c.IsDelLive,0)+isnull(d.IsDelete,0))>0 THEN 1
                                         ELSE 0 END AS IsDelete, 
                                    isnull(c.IsDelLive,0) AS IsDelLive   
                                FROM Tb_HSPR_Household a
                                LEFT JOIN Tb_HSPR_Customer b ON a.CustID=b.CustID
                                LEFT JOIN Tb_HSPR_CustomerLive c ON a.CustID=c.CustID AND a.RoomID=c.RoomID
                                LEFT JOIN Tb_HSPR_Room d ON c.RoomID=d.RoomID
                                WHERE a.MobilePhone LIKE @Mobile OR a.LinkManTel LIKE @Mobile;";

                        var reader = erpConn.QueryMultiple(sql, new { Mobile = $"%{userMobile}%" });
                        var customerData = reader.Read();
                        var householdData = reader.Read();

                        // 如果房屋绑定关系已存在，则更新绑定信息，同时同步 Locked 状态
                        // 若不存在绑定关系且为有效信息，则插入绑定关系
                        sql = @"DECLARE @CommunityId varchar(36);
                                SELECT @CommunityId=Id FROM Tb_Community WHERE CorpID=@CorpID AND CommID=@CommID;

                                IF exists(SELECT * FROM Tb_User_Relation WHERE UserId=@UserId AND CommunityId=@CommunityId AND RoomId=@RoomId)
                                    UPDATE Tb_User_Relation SET CustId=@CustId,CustName=@CustName,CustHoldID=@CustHoldID,Locked=@Locked
                                    WHERE UserId=@UserId AND CommunityId=@CommunityId AND RoomId=@RoomID;
                                ELSE
                                    IF @Locked=0
                                        INSERT INTO Tb_User_Relation(Id,UserId,CommunityId,CustId,CustHoldId,RoomId,
                                            RegDate,CustName,RoomSign,CustMobile,Locked)
                                        VALUES(@Id,@UserId,@CommunityId,@CustId,@CustHoldId,@RoomId,
                                            getdate(),@CustName,@RoomSign,@CustMobile,0);
                                    ;";

                        foreach (dynamic item in householdData)
                        {
                            // 不允许无具体房产客户绑定
                            if (item.RoomID == 0) continue;
                            if (item.RoomName == null && item.RoomName == null) continue;

                            // 是否锁定房屋
                            var locked = Convert.ToInt32((item.IsDelete > 0 || item.IsDelLive > 0));
                            if (locked == 0)
                            {
                                // 不允许租户绑定
                                if (control.AllowLesseeBind == false && item.LiveType == 2) continue;

                                // 不允许临时客户绑定
                                if (control.AllowTemporaryBind == false && item.LiveType == 3) continue;
                            }

                            appConn.Execute(sql, new
                            {
                                Id = Guid.NewGuid().ToString(),
                                UserId = userId,
                                CorpID = community.CorpID,
                                CommID = item.CommID,
                                CustId = item.CustID,
                                CustName = item.CustName,
                                CustMobile = item.CustMobile,
                                RoomId = item.RoomID,
                                RoomSign = (string.IsNullOrEmpty(item.RoomName) ? item.RoomSign : item.RoomName),
                                Locked = locked,
                                CustHoldId = item.HoldID
                            });
                        }

                        foreach (dynamic item in customerData)
                        {
                            // 不允许无具体房产客户绑定
                            if (item.RoomID == 0) continue;

                            // 是否锁定房屋
                            var locked = Convert.ToInt32((item.IsDelete > 0 || item.IsDelLive > 0));
                            if (locked == 0)
                            {
                                // 不允许租户绑定
                                if (control.AllowLesseeBind == false && item.LiveType == 2) continue;

                                // 不允许临时客户绑定
                                if (control.AllowTemporaryBind == false && item.LiveType == 3) continue;
                            }

                            appConn.Execute(sql, new
                            {
                                Id = Guid.NewGuid().ToString(),
                                UserId = userId,
                                CorpID = community.CorpID,
                                CommID = item.CommID,
                                CustId = item.CustID,
                                CustName = item.CustName,
                                CustMobile = item.CustMobile,
                                RoomId = item.RoomID,
                                RoomSign = (string.IsNullOrEmpty(item.RoomName) ? item.RoomSign : item.RoomName),
                                Locked = locked,
                                CustHoldId = 0
                            });
                        }

                        // 读取用户已绑定房屋信息
                        sql = @"SELECT a.Id AS RelationID,a.CommunityID,b.CommName,a.CustID,a.CustName,a.CustMobile,
                                    a.RoomID,a.RoomSign,'' AS RoomName,'' AS BuildSNum,'' AS BuildName,'' AS UnitSNum,
                                    null AS TenantName,
                                    null AS MemberName,
                                    '0' AS IsCust,
                                    '0' AS IsHousehold,
                                    '0' AS IsTenant,
                                    (SELECT count(1) FROM Tb_User_Relation WHERE UserId=@UserId AND Locked=0) AS BindingCount 
                                FROM Tb_User_Relation a
                                LEFT JOIN Tb_Community b ON a.CommunityId=b.Id
                                WHERE UserId=@UserId";

                        var rooms = appConn.Query(sql, new { UserId = userId });

                        sql = @"/*房屋信息*/
                                SELECT a.RoomName,b.BuildSNum,b.BuildName,a.UnitSNum 
                                FROM Tb_HSPR_Room a
                                LEFT JOIN Tb_HSPR_Building b ON a.CommID=b.CommID AND a.BuildSNum=b.BuildSNum AND isnull(b.IsDelete,0)=0
                                WHERE RoomID=@RoomID;

                                /*家属信息*/
                                SELECT isnull(isnull(isnull(a.MemberName,a.Name),a.Surname),a.PaperName) AS MemberName,
                                    a.Relationship,b.DictionaryName AS RelationshipName   
                                FROM Tb_HSPR_Household a
                                LEFT JOIN Tb_Dictionary_Relation b ON a.Relationship=b.DictionaryCode
                                WHERE RoomID=@RoomID AND isnull(IsDelete,0)=0
                                AND (isnull(MobilePhone,'') LIKE @Mobile OR isnull(LinkManTel,'') LIKE @Mobile);

                                /*客户信息*/
                                SELECT a.CustID,a.CustName,b.LiveType 
                                    FROM Tb_HSPR_Customer a 
                                LEFT JOIN Tb_HSPR_CustomerLive b ON a.CustID=b.CustID AND isnull(b.IsDelLive,0)=0
                                WHERE (isnull(a.MobilePhone,'') LIKE @Mobile OR isnull(a.LinkManTel,'') LIKE @Mobile)
                                AND isnull(a.IsDelete,0)=0
                                ORDER BY LiveType DESC;";

                        foreach (var roomInfo in rooms)
                        {
                            reader = erpConn.QueryMultiple(sql, new { RoomID = roomInfo.RoomID, Mobile = $"%{ userMobile }%" });
                            var tmp1 = reader.Read().FirstOrDefault();
                            var tmp2 = reader.Read().FirstOrDefault();
                            var tmp3 = reader.Read().FirstOrDefault();

                            if (tmp1 != null)
                            {
                                roomInfo.RoomName = tmp1.RoomName;
                                roomInfo.BuildSNum = tmp1.BuildSNum;
                                roomInfo.BuildName = tmp1.BuildName;
                                roomInfo.UnitSNum = tmp1.UnitSNum;
                            }

                            // 家属
                            if (tmp2 != null)
                            {
                                if (tmp2.RelationshipName?.Contains("业主")) // 本身是业主或共有业主
                                {
                                    roomInfo.IsCust = "1";
                                    roomInfo.CustName = tmp2.MemberName;
                                }
                                else
                                {
                                    roomInfo.IsHousehold = "1";
                                    roomInfo.MemberName = tmp2.MemberName;
                                }
                            }

                            if (tmp3.LiveType == 1)      // 业主
                            {
                                roomInfo.IsCust = "1";
                                roomInfo.CustName = tmp3.CustName;

                                roomInfo.IsHousehold = '0';
                                roomInfo.MemberName = null;

                                roomInfo.IsTenant = '0';
                                roomInfo.TenantName = null;
                            }
                            else if (tmp3.LiveType == 2) // 租户
                            {
                                roomInfo.IsTenant = "1";
                                roomInfo.TenantName = tmp3.CustName;

                                roomInfo.IsHousehold = '0';
                                roomInfo.MemberName = null;
                            }
                        }

                        return new ApiResult(true, rooms).toJson();
                    }
                    catch (Exception ex)
                    {
                        return JSONHelper.FromString(false, ex.Message + Environment.NewLine + ex.StackTrace);
                    }
                }
            }
        }

        /// <summary>
        /// 添加或更新家庭成员信息
        /// </summary>
        private long AddHouseholdInfo(Tb_Community community, long custId, long roomId,
            string userName, string mobile, string relationShip, string memo = "业主App手动绑定房屋")
        {
            using (var conn = new SqlConnection(GetConnectionStr(community)))
            {
                var sql = @"DECLARE @HoldID bigint;
                            DECLARE @RelationID int;
                            DECLARE @MemberCode bigint;

                            IF @Relationship='0013' OR @Relationship='0029'
                            BEGIN
                                SET @RelationID=1;
                                SET @MemberCode=@CustID;
                            END

                            SELECT TOP 1 @HoldID=HoldID FROM Tb_HSPR_Household
                            WHERE CustID=@CustID AND RoomID=@RoomID AND (MobilePhone LIKE @Mobile OR LinkManTel LIKE @Mobile)
                            ORDER BY IsDelete;

                            IF @HoldID IS NULL OR @HoldID=0
                                BEGIN
                                    SELECT @HoldID=isnull(max(HoldID)+1, (@CommID*cast(100000000 AS BIGINT)+1))
                                    FROM Tb_HSPR_Household WHERE CommID=@CommID;

                                    INSERT INTO Tb_HSPR_Household(HoldID,RelationID,CommID,CustID,RoomID,Surname,[Name],
                                        MobilePhone,Relationship,InquirePwd,Memo,MemberName,MemberCode, Linkman, LinkManTel,IsDelete,SynchFlag)
                                    VALUES(@HoldID,@RelationID,@CommID,@CustID,@RoomID,@Name,@Name,@MobilePhone,@Relationship,'123',@Memo,
                                        @Name,@MemberCode,@Name,@MobilePhone,0,0);
                                END
                            ELSE
                                BEGIN
                                    UPDATE Tb_HSPR_Household SET CommID=@CommID,CustID=@CustID,RoomID=@RoomID,
                                        Surname=@Name,[Name]=@Name,MobilePhone=@MobilePhone,Relationship=@Relationship,
                                        Linkman=@Name,LinkManTel=@MobilePhone,MemberCode=@MemberCode,MemberName=@Name,
                                        IsDelete=0,SynchFlag=0
                                    WHERE HoldID=@HoldID;
                                END

                            --更新电话表
                            EXEC Proc_HSPR_Telephone_InsUpate @CommID, @CustID, @HoldID,1;

                            SELECT @HoldID;";

                var holdId = conn.Query<long>(sql, new
                {
                    CommID = community.CommID,
                    RoomID = roomId,
                    CustID = custId,
                    Mobile = $"%{mobile}%",
                    MobilePhone = mobile,
                    Relationship = relationShip,
                    Name = userName,
                    Memo = memo
                }).FirstOrDefault();

                return holdId;
            }
        }
    }
}
