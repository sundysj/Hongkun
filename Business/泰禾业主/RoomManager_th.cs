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
    public class RoomManager_th : PubInfo
    {
        public RoomManager_th()
        {
            base.Token = "20171026RoomManager";
        }

        public override void Operate(ref Common.Transfer Trans)
        {
            Trans.Result = JSONHelper.FromString(false, "未知错误");

            DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
            DataRow Row = dAttributeTable.Rows[0];

            switch (Trans.Command)
            {
                case "GetBuildingList":             // 获取小区下楼栋房屋列表
                    Trans.Result = GetBuildingList(Row);
                    break;
                case "GetFloorRoomList":            // 获取该楼层下房屋列表
                    Trans.Result = GetFloorRoomList(Row);
                    break;
                case "GetBindingRoomList":          // 获取已绑定房屋列表
                    Trans.Result = GetBindingRoomList(Row);
                    break;
                case "GetBindingUserList":          // 获取绑定了当前房屋的用户信息
                    Trans.Result = GetBindingUserList(Row);
                    break;
                case "VerifyRoomInfo":              // 绑定房屋时，验证信息
                    Trans.Result = VerifyRoomInfo(Row);
                    break;
                case "GetIdentityAndBindingCount":  // 获取该账号在当前房屋下的身份以及绑定了当前房屋的账号数量
                    Trans.Result = GetIdentityAndBindingCount(Row);
                    break;
                case "Unbinding":                   // 解除绑定
                    Trans.Result = Unbinding(Row);
                    break;
                case "LockBinding":                 // 锁定房屋，锁定后，绑定该账号的房屋不能再操作该房屋
                    Trans.Result = LockBinding(Row);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 获取小区下楼栋房屋列表
        /// </summary>
        public string GetBuildingList(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return JSONHelper.FromString(false, "小区ID不能为空");
            }

            string communityId = row["CommunityId"].ToString();

            //查询小区
            Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(communityId);
            //构造链接字符串
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }
            string strcon = GetConnectionStringStr(Community);

            try
            {
                using (var con = new SqlConnection(strcon))
                {
                    var sql = @"SELECT BuildName,BuildSNum FROM Tb_HSPR_Building WHERE isnull(IsDelete,0)=0 AND CommID=@CommID ORDER BY BuildName;
                               SELECT DISTINCT UnitSNum,BuildSNum FROM Tb_HSPR_Room WHERE isnull(IsDelete,0)=0 AND CommID=@CommID ORDER BY BuildSNum,UnitSNum;
                               SELECT DISTINCT FloorSNum,UnitSNum,BuildSNum FROM Tb_HSPR_Room WHERE isnull(IsDelete,0)=0 AND CommID=@CommID 
                                    ORDER BY BuildSNum,UnitSNum,FloorSNum;";

                    var queryResult = con.QueryMultiple(sql, new { CommID = Community.CommID });

                    var building = queryResult.Read();
                    var unit = queryResult.Read();
                    var floor = queryResult.Read();

                    var tempBuilding = new List<object>();

                    if (building.Count() > 0)
                    {
                        // 楼栋
                        foreach (dynamic buildingItem in building)
                        {
                            var tempUnit = new List<object>();
                            // 单元
                            foreach (var unitItem in unit)
                            {
                                if (unitItem.BuildSNum == buildingItem.BuildSNum)
                                {
                                    // 单元下楼层
                                    var tempFloor = floor.Where<dynamic>((u) => (u.BuildSNum == unitItem.BuildSNum && u.UnitSNum == unitItem.UnitSNum));
                                    var floorsList = tempFloor.Select(p => p.FloorSNum);

                                    tempUnit.Add(new { UnitSNum = unitItem.UnitSNum, Floors = floorsList });
                                }
                            }

                            tempBuilding.Add(new { BuildName = buildingItem.BuildName, BuildSNum = buildingItem.BuildSNum, Unit = tempUnit });
                        }
                    }

                    return JSONHelper.FromString(tempBuilding);
                }
            }
            catch (Exception ex)
            {
                return JSONHelper.FromString(false, strcon + ex.Message + ex.StackTrace);
            }
        }

        /// <summary>
        /// 获取该楼层下房屋列表
        /// </summary>
        public string GetFloorRoomList(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return JSONHelper.FromString(false, "小区ID不能为空");
            }

            if (!row.Table.Columns.Contains("BuildSNum") || string.IsNullOrEmpty(row["BuildSNum"].ToString()))
            {
                return JSONHelper.FromString(false, "楼栋不能为空");
            }

            if (!row.Table.Columns.Contains("UnitSNum") || string.IsNullOrEmpty(row["UnitSNum"].ToString()))
            {
                return JSONHelper.FromString(false, "单元不能为空");
            }

            if (!row.Table.Columns.Contains("FloorNum") || string.IsNullOrEmpty(row["FloorNum"].ToString()))
            {
                return JSONHelper.FromString(false, "楼层不能为空");
            }

            string communityId = row["CommunityId"].ToString();
            int buildSNum = AppGlobal.StrToInt(row["BuildSNum"].ToString());
            int unitSNum = AppGlobal.StrToInt(row["UnitSNum"].ToString());
            int floorNum = AppGlobal.StrToInt(row["FloorNum"].ToString());

            //查询小区
            Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(communityId);
            //构造链接字符串
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }
            string strcon = new CostInfo().GetConnectionStringStr(Community);

            using (IDbConnection con = new SqlConnection(strcon))
            {
                string sql = string.Format(@"SELECT a.RoomID,isnull(a.RoomSign,a.RoomName) AS RoomSign,a.RoomName,b.CustID,c.CustName,c.MobilePhone,a.ContSubDate
                                            FROM Tb_HSPR_Room a
                                            LEFT JOIN Tb_HSPR_CustomerLive b ON a.RoomID=b.RoomID
                                            LEFT JOIN Tb_HSPR_Customer c ON b.CustID=c.CustID
                                            WHERE isnull(b.IsDelLive,0)=0 AND b.LiveType=1 AND isnull(c.IsDelete,0)=0
                                            AND isnull(a.IsDelete, 0)=0 AND a.CommID={0} AND a.BuildSNum={1} AND a.UnitSNum={2} AND a.FloorSNum={3}",
                                Community.CommID, buildSNum, unitSNum, floorNum);

                return JSONHelper.FromString(con.ExecuteReader(sql).ToDataSet().Tables[0]);
            }
        }

        #region 检测绑定房屋业主信息是否还正确,不正确就删除绑定关系
        private void CheckBindRoomExists(string UserID)
        {
            if (string.IsNullOrEmpty(UserID))
            {
                return;
            }
            using (IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                DataTable dt = conn.ExecuteReader("SELECT * FROM Tb_User_Relation WHERE UserId = @UserId", new { UserId = UserID }, null, null, CommandType.Text).ToDataSet().Tables[0];
                if (null != dt && dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        CheckRoomInfoCanDelete(item["CommunityId"].ToString(), item["RoomId"].ToString(), item["CustMobile"].ToString(), item["Id"].ToString());
                    }
                }

            }
        }

        /// <summary>
        /// 检查该绑定关系的业主信息是否还存在,不存在则删除绑定关系
        /// </summary>
        private void CheckRoomInfoCanDelete(string CommunityId, string RoomID, string MobilePhone, string RelationId)
        {
            if (string.IsNullOrEmpty(CommunityId))
            {
                return;
            }
            //查询小区
            Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(CommunityId);
            //构造链接字符串
            if (null == Community)
            {
                return;
            }
            string strcon = new CostInfo().GetConnectionStringStr(Community);

            //默认为1,防止默认删除
            int count = 1;
            using (IDbConnection conn = new SqlConnection(strcon))
            {
                count = conn.Query(@"SELECT b.CustID,b.CustName,b.MobilePhone,c.RoomSign FROM Tb_HSPR_CustomerLive a 
                                    LEFT JOIN Tb_HSPR_Customer b ON a.CustID=b.CustID 
                                    LEFT JOIN Tb_HSPR_Room c on a.RoomID=c.RoomID 
                                    WHERE a.LiveType=1 AND isnull(a.IsDelLive,0)=0 AND a.RoomID = @RoomID 
                                    AND b.MobilePhone like @MobilePhone",
                                    new { RoomID = RoomID, MobilePhone = "%" + MobilePhone + "%" }).Count();
            }
            if (count <= 0)
            {
                using (IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString))
                {
                    conn.Execute("DELETE Tb_User_Relation WHERE Id = @Id", new { Id = RelationId });
                }
            }

        }
        #endregion

        /// <summary>
        /// 获取已绑定房屋列表
        /// </summary>
        private string GetBindingRoomList(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return new ApiResult(false, "小区编号不能为空").toJson();
            }
            if (!row.Table.Columns.Contains("UserID") || string.IsNullOrEmpty(row["UserID"].ToString()))
            {
                return new ApiResult(false, "用户ID不能为空").toJson();
            }
            if (!row.Table.Columns.Contains("Mobile") || string.IsNullOrEmpty(row["Mobile"].ToString()))
            {
                return new ApiResult(false, "用户ID不能为空").toJson();
            }
            string communityId = row["CommunityId"].AsString();
            string userID = row["UserID"].AsString();
            string mobile = row["Mobile"].AsString();

            //查询小区
            Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(communityId);
            //构造链接字符串
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }
            string strcon = GetConnectionStringStr(Community);

            #region 判断业主信息是否变更,如有变更,则删除绑定关系
            if (Community.CorpID == 1970)
            {
                CheckBindRoomExists(userID);
            }

            #endregion

            using (IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                string sql = string.Format(@"SELECT(SELECT cast(Id+'|'+cast(RoomId as nvarchar(20))+'|'+CommunityId+'|'+cast(CustId as nvarchar(20)) AS NVARCHAR(200))+',' FROM Tb_User_Relation 
                                                WHERE isnull(Locked, 0)=0 AND UserId='{0}' FOR XML PATH('')) AS _Values;", userID);

                dynamic v = conn.Query(sql).FirstOrDefault();
                if (v._Values == null || string.IsNullOrEmpty(v._Values.Trim(',').ToString()))
                {
                    return JSONHelper.FromString(new DataTable());
                }

                string[] valueArray = v._Values.Trim(',').Split(',');
                List<string> relaList = new List<string>();
                List<string> roomList = new List<string>();
                List<string> commList = new List<string>();
                List<string> custList = new List<string>();

                foreach (var item in valueArray)
                {
                    relaList.Add(item.Split('|')[0]);  //关系ID集合
                    roomList.Add(item.Split('|')[1]);  //房间ID集合
                    commList.Add(item.Split('|')[2]);  //社区ID集合
                    custList.Add(item.Split('|')[3]);  //住户ID集合
                }

                using (IDbConnection conn2 = new SqlConnection(strcon))
                {
                    //根据上面取出来的用户ID和房间ID获取所有相关信息
                    sql = @"SELECT DISTINCT a.RoomID,a.CustID,isnull(b.MobilePhone,b.LinkmanTel) AS CustMobile,c.CommID,c.RoomSign,c.RoomName,
                            c.BuildSNum,c.UnitSNum,e.CommName,d.BuildName, 0 AS IsCust,0 AS IsTenant,0 AS IsHousehold,
                            '' AS CustName,'' AS TenantName,'' AS MemberName
                            FROM Tb_HSPR_CustomerLive a
                              LEFT JOIN Tb_HSPR_Customer b ON a.CustID=b.CustID
                              LEFT JOIN Tb_HSPR_Room c ON a.RoomID=c.RoomID
                              LEFT JOIN Tb_HSPR_Building d ON d.CommID=left(c.RoomID, 6) AND c.BuildSNum=d.BuildSNum
                              LEFT JOIN Tb_HSPR_Community e ON d.CommID=e.CommID
                            WHERE isnull(a.IsDelLive,0)=0 AND isnull(c.IsDelete,0)=0 AND isnull(d.IsDelete,0)=0 AND isnull(e.IsDelete,0)=0 
                                AND a.CustID IN (SELECT convert(NVARCHAR(30), colName) FROM dbo.funSplitTabel(@CustIDs,','))
                                AND a.RoomID IN (SELECT convert(NVARCHAR(30), colName) FROM dbo.funSplitTabel(@RoomIDs,','));";

                    DataSet ds = conn2.ExecuteReader(sql, new
                    {
                        MobilePhone = "%" + mobile + "%",
                        CustIDs = string.Join(",", custList),
                        RoomIDs = string.Join(",", roomList),
                    }).ToDataSet();

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ds.Tables[0].Columns.Add(new DataColumn { ColumnName = "RelationID", DataType = typeof(string) });
                        ds.Tables[0].Columns.Add(new DataColumn { ColumnName = "CommunityID", DataType = typeof(string) });
                        ds.Tables[0].Columns.Add(new DataColumn { ColumnName = "BindingCount", DataType = typeof(int) });

                        string identitySql = @"SELECT TOP 1 Relationship,ISNULL(ISNULL(memberName,Surname),Name) AS Name FROM Tb_HSPR_Household 
                                                WHERE RoomID=@RoomID AND MobilePhone like @Mobile ORDER BY IsDelete;";
                        string isCustSql = @"SELECT b.CustName AS Name FROM Tb_HSPR_CustomerLive a 
                                                INNER JOIN Tb_HSPR_Customer b ON a.CustID=b.CustID AND isnull(b.IsDelete,0)=0
                                                WHERE a.RoomID=@RoomID AND a.LiveType=1 AND a.IsDelLive=0 
                                                AND (b.MobilePhone like @Mobile OR b.LinkmanTel like @Mobile);";

                        using (IDbConnection conn3 = new SqlConnection(strcon))
                        {
                            foreach (DataRow item in ds.Tables[0].Rows)
                            {
                                for (int i = 0; i < roomList.Count; i++)
                                {
                                    if (item["RoomID"] != null && (roomList[i] == item["RoomID"].ToString()))
                                    {
                                        item["RelationID"] = relaList[i];
                                        item["CommunityID"] = commList[i];
                                        item["BindingCount"] = conn.Query(@"SELECT count(1) AS Count FROM Tb_User_Relation 
                                                WHERE isnull(Locked,0)=0 AND RoomId=@RoomId",
                                            new { RoomId = item["RoomID"] }).First().Count;

                                        // 身份
                                        dynamic identity = conn3.Query(identitySql, new { RoomID = item["RoomID"].ToString(), Mobile = "%" + mobile + "%" }).FirstOrDefault();

                                        if (identity != null && identity.Relationship != null)
                                        {
                                            // 业主
                                            if (identity.Relationship.ToString() == "0013" || identity.Relationship.ToString() == "0029")
                                            {
                                                item["IsCust"] = 1;
                                                item["CustName"] = identity.Name;
                                                break;
                                            }

                                            // 家属
                                            if (identity.Relationship.ToString() == "0030")
                                            {
                                                item["IsHousehold"] = 1;
                                                item["MemberName"] = identity.Name;
                                            }

                                            // 租户
                                            if (identity.Relationship.ToString() == "0031")
                                            {
                                                item["IsTenant"] = 1;
                                                item["TenantName"] = identity.Name;
                                                break;
                                            }
                                        }

                                        // 有些时候，业主本人信息在家属表里面被登记成了家属
                                        if (identity == null || item["IsHousehold"].ToString() == "1")
                                        {
                                            identity = conn3.Query(isCustSql, new
                                            {
                                                RoomID = item["RoomID"].ToString(),
                                                Mobile = "%" + mobile + "%"
                                            }).FirstOrDefault();

                                            if (identity != null)
                                            {
                                                item["IsCust"] = 1;
                                                item["CustName"] = identity.Name;
                                                item["IsHousehold"] = 0;
                                                item["MemberName"] = "";
                                                break;
                                            }
                                        }

                                        // 适配从标准版本迁移时因手动绑定房屋造成未添加家庭成员信息导致的问题
                                        if (item["IsCust"].ToString() == "0" &&
                                            item["IsHousehold"].ToString() == "0" &&
                                            item["IsTenant"].ToString() == "0")
                                        {
                                            identity = conn3.Query(isCustSql, new
                                            {
                                                RoomID = item["RoomID"].ToString(),
                                                Mobile = "%" + mobile + "%"
                                            }).FirstOrDefault();

                                            if (identity != null)
                                            {
                                                item["IsCust"] = 1;
                                                item["CustName"] = identity.Name;
                                                break;
                                            }
                                        }

                                        break;
                                    }
                                }
                            }
                        }

                    }

                    return new ApiResult(true, ds.Tables[0]).toJson();
                }
            }
        }

        /// <summary>
        /// 获取绑定了当前房屋的用户信息
        /// </summary>
        private string GetBindingUserList(DataRow row)
        {
            if (!row.Table.Columns.Contains("UserId") || string.IsNullOrEmpty(row["UserId"].ToString()))
            {
                return JSONHelper.FromString(false, "用户编号不能为空");
            }
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            if (!row.Table.Columns.Contains("RoomID") || string.IsNullOrEmpty(row["RoomID"].ToString()))
            {
                return JSONHelper.FromString(false, "房屋编号不能为空");
            }

            string communityId = row["CommunityId"].AsString();
            string userId = row["UserId"].AsString();
            string roomID = row["RoomID"].AsString();

            //查询小区
            Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(communityId);
            //构造链接字符串
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }
            string strcon = GetConnectionStringStr(Community);

            using (IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                string sql = @"SELECT a.Mobile,a.UserPic,b.Id AS ReletionId,b.CustId,b.CustHoldId,b.CustMobile,
                                    isnull(b.Locked,0) AS Locked 
                                  FROM Tb_User_Relation b LEFT JOIN Tb_User a ON a.Id=b.UserId
                                  WHERE b.UserId<>@UserId AND b.RoomId=@RoomId";

                DataSet ds = conn.ExecuteReader(sql, new { UserId = userId, RoomId = roomID }).ToDataSet();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ds.Tables[0].Columns.Add(new DataColumn() { ColumnName = "Name", DataType = typeof(string) });
                    ds.Tables[0].Columns.Add(new DataColumn() { ColumnName = "Identity", DataType = typeof(string) });

                    using (IDbConnection conn2 = new SqlConnection(strcon))
                    {
                        foreach (DataRow item in ds.Tables[0].Rows)
                        {
                            if (item["CustHoldId"] == null || string.IsNullOrEmpty(item["CustHoldId"].ToString()) || item["CustHoldId"].ToString() == "0")
                            {
                                continue;
                            }
                            sql = @"SELECT ISNULL(MemberName,'未登记姓名') AS Name,Relationship FROM Tb_HSPR_Household WHERE ISNULL(HoldID,0)= @HoldID";
                            dynamic result = conn2.Query(sql, new { HoldID = item["CustHoldId"].ToString() }).FirstOrDefault();
                            if (result != null)
                            {
                                item["Name"] = result.Name;
                                if (result.Relationship == UserRoomIdentityCode.Customer1 || result.Relationship == UserRoomIdentityCode.Customer2)
                                    item["Identity"] = (int)UserRoomIdentity.Customer;

                                else if (result.Relationship == UserRoomIdentityCode.FamilyMember)
                                    item["Identity"] = (int)UserRoomIdentity.FamilyMember;

                                else if (result.Relationship == UserRoomIdentityCode.Tenant)
                                    item["Identity"] = (int)UserRoomIdentity.Tenant;
                                else
                                    item["Identity"] = (int)UserRoomIdentity.Other;
                            }
                            else
                            {
                                item["Name"] = "未登记姓名";
                                item["Identity"] = (int)UserRoomIdentity.Other;
                            }
                        }
                    }

                    ds.Tables[0].Columns.Remove("CustId");
                    ds.Tables[0].Columns.Remove("CustHoldId");
                    ds.Tables[0].Columns.Remove("CustMobile");
                }

                return JSONHelper.FromString(ds.Tables[0]);
            }
        }

        /// <summary>
        /// 绑定房屋时，验证信息
        /// </summary>
        private string VerifyRoomInfo(DataRow row)
        {
            if (!row.Table.Columns.Contains("UserId") || string.IsNullOrEmpty(row["UserId"].ToString()))
            {
                return JSONHelper.FromString(false, "用户编号不能为空");
            }

            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            if (!row.Table.Columns.Contains("RoomID") || string.IsNullOrEmpty(row["RoomID"].ToString()))
            {
                return JSONHelper.FromString(false, "房屋编号不能为空");
            }
            if (!row.Table.Columns.Contains("MobileOrName") || string.IsNullOrEmpty(row["MobileOrName"].ToString()))
            {
                return JSONHelper.FromString(false, "缺少MobileOrName参数");
            }
            if (!row.Table.Columns.Contains("IDCard") || string.IsNullOrEmpty(row["IDCard"].ToString()))
            {
                return JSONHelper.FromString(false, "缺少IDCard参数");
            }

            string userId = row["UserId"].AsString();
            string mobile = null;
            string communityId = row["CommunityId"].AsString();
            string roomID = row["RoomID"].AsString();
            string mobileOrName = row["MobileOrName"].AsString();
            string IDCard = row["IDCard"].AsString();

            //查询小区
            Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(communityId);
            //构造链接字符串
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }

            string strcon = GetConnectionStringStr(Community);

            using (IDbConnection conn = new SqlConnection(strcon))
            {
                // 查询业主信息
                string sql = @"SELECT TOP 1 b.CustID,b.CustName,b.MobilePhone,c.RoomSign
                                FROM Tb_HSPR_CustomerLive a 
                                LEFT JOIN Tb_HSPR_Customer b ON a.CustID=b.CustID
                                LEFT JOIN Tb_HSPR_Room c on a.RoomID=c.RoomID
                                WHERE a.LiveType=1 AND isnull(a.IsDelLive,0)=0 AND a.RoomID=@RoomID AND b.PaperCode=@PaperCode 
                                AND (b.CustName LIKE @MobileOrName or b.MobilePhone LIKE @MobileOrName OR b.LinkmanTel LIKE @MobileOrName)";

                dynamic result = conn.Query(sql, new { RoomID = roomID, PaperCode = IDCard, MobileOrName = "%" + mobileOrName + "%" }).FirstOrDefault();

                // 数据验证正确，开始绑定房屋
                if (result != null)
                {
                    string custId = result.CustID.ToString();
                    string custName = result.CustName;
                    string custMobile = result.MobilePhone;
                    string roomSign = result.RoomSign;

                    using (IDbConnection conn2 = new SqlConnection(PubConstant.UnifiedContionString))
                    {
                        var relationId = Guid.NewGuid().ToString();
                        mobile = conn2.Query<string>(@"SELECT Mobile FROM Tb_User WHERE Id=@UserId", new { UserId = userId }).FirstOrDefault();

                        // 查看是否已绑定该房屋
                        sql = @"Select * from Tb_User_Relation Where UserId=@UserId AND CommunityId=@CommunityId AND RoomID=@RoomID";
                        var bindingInfo = conn2.Query(sql, new { UserId = userId, CommunityId = communityId, RoomID = roomID }).FirstOrDefault();

                        // 未绑定则添加绑定
                        if (bindingInfo == null)
                        {
                            sql = @"INSERT INTO Tb_User_Relation(Id,UserId,CommunityId,CustId,RoomId,CustName,RoomSign,CustMobile,RegDate) 
                                    VALUES (@Id,@UserId,@CommunityId,@CustId,@RoomId,@CustName,@RoomSign,@CustMobile,GETDATE())";

                            conn2.Execute(sql, new
                            {
                                Id = relationId,
                                UserId = userId,
                                CommunityId = communityId,
                                CustId = custId,
                                RoomId = roomID,
                                CustName = custName,
                                RoomSign = roomSign,
                                CustMobile = custMobile
                            });
                        }
                        else
                        {
                            // 更新绑定信息表
                            conn2.Execute(@"UPDATE Tb_User_Relation SET Locked=0 WHERE Id=@Id", new { Id = bindingInfo.Id });
                        }

                        // 家属表是否已经存在信息
                        sql = @"SELECT isnull(sum(isnull(IsDelete,0)),0) AS SUM FROM Tb_HSPR_Household WHERE RoomID=@RoomID AND CustID=@CustID
                                        AND (MobilePhone=@Mobile OR LinkManTel=@Mobile)";
                        if (conn.Query<int>(sql, new { RoomID = roomID, CustID = custId, Mobile = mobile }).FirstOrDefault() > 0)
                        {
                            // 存在则更新
                            conn.Execute(@"UPDATE Tb_HSPR_Household SET IsDelete=0 WHERE RoomID=@RoomID AND CustID=@CustID
                                        AND(MobilePhone = @Mobile OR LinkManTel = @Mobile)", new
                            {
                                RoomID = roomID,
                                CustID = custId,
                                Mobile = mobile
                            });
                        }
                        else
                        {
                            // 不存在，插入
                            DynamicParameters parameters = new DynamicParameters();
                            parameters.Add("@HoldID", 0, DbType.Int64, ParameterDirection.Output);
                            parameters.Add("@CommID", Community.CommID);
                            parameters.Add("@CustID", custId);
                            parameters.Add("@RoomID", roomID);
                            parameters.Add("@Name", mobile);
                            parameters.Add("@MobilePhone", mobile);
                            parameters.Add("@Relationship", UserRoomIdentityCode.FamilyMember);
                            if (conn.State == ConnectionState.Closed)
                            {
                                conn.Open();
                            }
                            conn.Execute("Proc_HSPR_Household_Insert_Phone", parameters, null, null, CommandType.StoredProcedure);
                            long custHoldId = parameters.Get<long>("@HoldID");

                            // 更新绑定信息表
                            conn2.Execute(@"UPDATE Tb_User_Relation SET CustHoldID=@CustHoldID WHERE Id=@Id",
                                new
                                {
                                    CustHoldID = custHoldId,
                                    Id = relationId
                                });
                        }


                        // 这里不适用于电信三服务器，因为电信三服务器是tw2_bs_dx
                        Community.DBName = "tw2_bs";

                        string tw2bsConnectionString = GetConnectionStringStr(Community);
                        string hmWyglConnectionString = strcon;
                        string corpId = Community.CorpID.ToString();

                        Task.Run(() =>
                        {
                            if (Common.Push.GetAppKeyAndAppSecret(tw2bsConnectionString, corpId, out string appIdentifier, out string appKey, out string appSecret, true))
                            {
                                // 通知业主有用户绑定了房屋
                                PushModel pushModel = new PushModel(appKey, appSecret)
                                {
                                    AppIdentifier = appIdentifier,
                                    Badge = 1,
                                    KeyInfomation = custMobile,
                                    Message = $"手机号用户{mobile}绑定了您的房屋{roomSign}，请及时查看。",
                                    Command = PushCommand.NEW_ROOM_RELATION
                                };

                                pushModel.Audience.Category = PushAudienceCategory.Alias;
                                pushModel.Audience.Objects.Add(custMobile);

                                pushModel.Audience.SecondObjects.Category = PushAudienceSecondCategory.TagsAnd;
                                pushModel.Audience.SecondObjects.Objects.Add(PushCommand.NEW_ROOM_RELATION);

                                Push.SendAsync(pushModel);
                            };
                        });

                        return JSONHelper.FromString(true, "绑定成功");
                    }
                }

                return JSONHelper.FromString(false, "验证失败");
            }
        }

        /// <summary>
        /// 获取该账号在当前房屋下的身份以及绑定了当前房屋的账号数量
        /// </summary>
        private string GetIdentityAndBindingCount(DataRow row)
        {
            if (!row.Table.Columns.Contains("Mobile") || string.IsNullOrEmpty(row["Mobile"].ToString()))
            {
                return JSONHelper.FromString(false, "用户手机号不能为空");
            }
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            if (!row.Table.Columns.Contains("RoomID") || string.IsNullOrEmpty(row["RoomID"].ToString()))
            {
                return JSONHelper.FromString(false, "房屋编号不能为空");
            }

            string communityId = row["CommunityId"].AsString();
            string roomID = row["RoomID"].AsString();
            string mobile = row["Mobile"].AsString();

            //查询小区
            Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(communityId);
            //构造链接字符串
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }
            string strcon = new CostInfo().GetConnectionStringStr(Community);

            using (var conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                string sql = string.Format(@"SELECT count(1) AS Count FROM Tb_User_Relation WHERE isnull(Locked,0)=0 AND RoomId='{0}';", roomID);
                string bindingCount = conn.Query(sql).First().Count.ToString();

                using (var conn2 = new SqlConnection(strcon))
                {
                    sql = string.Format(@"DECLARE @RoomID BIGINT, @IsCust INT, @IsTenant INT, @IsHousehold INT;
                                        SET @RoomID={0};
                                        SELECT @IsCust=count(1) FROM Tb_HSPR_CustomerLive a LEFT JOIN Tb_HSPR_Customer b ON a.CustID = b.CustID
                                            WHERE isnull(IsDelLive,0)= 0 AND LiveType = 1
                                                AND a.RoomID = @RoomID AND b.MobilePhone LIKE '%{1}%';
                                        SELECT @IsTenant=count(1) FROM Tb_HSPR_Household a
                                            WHERE a.RoomID=@RoomID AND a.Relationship='{4}' AND a.MobilePhone LIKE '%{1}%';
                                        SELECT @IsHousehold = count(1) FROM Tb_HSPR_Household
                                            WHERE isnull(IsDelete, 0) = 0 AND RoomID = @RoomID AND MobilePhone LIKE '%{1}%'
                                                AND Relationship NOT IN('{2}', '{3}', '{4}', '{5}');

                                        IF @IsCust=0 AND @IsTenant=0 AND @IsHousehold=0
                                            BEGIN
                                                IF exists(SELECT * FROM Tb_HSPR_Household WHERE RoomID=@RoomID AND MobilePhone LIKE '%{1}%' 
                                                AND Relationship='{3}')
                                                    BEGIN
                                                        SET @IsCust=1;
                                                    END
                                            END
                                        IF @IsCust=1 AND @IsHousehold=1
                                            BEGIN
                                                SET @IsHousehold=0;
                                            END
                                        SELECT @IsCust AS IsCust, @IsTenant AS IsTenant, @IsHousehold AS IsHousehold; ",
                                        roomID, mobile, UserRoomIdentityCode.Customer1, UserRoomIdentityCode.Customer2,
                                        UserRoomIdentityCode.Tenant, UserRoomIdentityCode.Other);

                    GridReader multi = conn2.QueryMultiple(sql);
                    int isCust = 0, isTenant = 0, isHousehold = 0;
                    while (!multi.IsConsumed)
                    {
                        //读取当前结果集  
                        var result = multi.Read().ToList()[0];
                        if (result != null)
                        {
                            isCust = result.IsCust;
                            isTenant = result.IsTenant;
                            isHousehold = result.IsHousehold;
                        }
                        break;
                    }

                    return new ApiResult(true, new { BindingCount = bindingCount, IsCust = isCust, IsTenant = isTenant, IsHousehold = isHousehold }).toJson();
                }
            }
        }

        /// <summary>
        /// 解绑房屋
        /// </summary>
        private string Unbinding(DataRow row)
        {
            return RoomBindingManager(row, true);
        }

        /// <summary>
        /// 锁定房屋，锁定后，绑定该账号的房屋不能再操作该房屋
        /// </summary>
        private string LockBinding(DataRow row)
        {
            return RoomBindingManager(row, true);
        }

        private string RoomBindingManager(DataRow row, bool locked)
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
            string strcon = GetConnectionStringStr(Community);

            using (IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                conn.Execute(@"UPDATE Tb_User_Relation SET Locked=@Locked WHERE Id=@Id", new
                {
                    Locked = (locked ? 1 : 0),
                    Id = relationID
                });
                long? holdId = conn.Query<long?>(@"SELECT CustHoldId FROM Tb_User_Relation WHERE Id=@Id", new
                {
                    Id = relationID
                }).FirstOrDefault();

                if (holdId.HasValue && holdId.Value > 0)
                {
                    using (IDbConnection conn2 = new SqlConnection(strcon))
                    {
                        conn2.Execute("UPDATE Tb_HSPR_Household SET IsDelete=@Locked WHERE HoldID=@HoldID",
                            new { Locked = (locked ? 1 : 0), HoldID = holdId.Value });
                    }
                }

                return JSONHelper.FromString(true, "操作成功");
            }
        }
    }
}
