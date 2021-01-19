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
    public class Audit_BindingRoom : PubInfo
    {
        public Audit_BindingRoom()
        {
            base.Token = "20180730AuditBindingRoom";
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
                case "AuditList":
                    Trans.Result = AuditList(Row);
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
            if (!row.Table.Columns.Contains("CustName") || string.IsNullOrEmpty(row["CustName"].ToString()))
            {
                return JSONHelper.FromString(false, "缺少CustName参数");
            }
            if (!row.Table.Columns.Contains("CustMobile") || string.IsNullOrEmpty(row["CustMobile"].ToString()))
            {
                return JSONHelper.FromString(false, "缺少CustMobile参数");
            }

            string communityId = row["CommunityId"].AsString();
            string RoomID = row["RoomID"].ToString();
            string CustName = row["CustName"].ToString();
            string CustMobile = row["CustMobile"].ToString();


            string ApplicantUserID = null;
            if (row.Table.Columns.Contains("ApplicantUserID") && !string.IsNullOrEmpty(row["ApplicantUserID"].ToString()))
            {
                ApplicantUserID = row["ApplicantUserID"].ToString();
            }

            string IDCardNum = null;
            if (row.Table.Columns.Contains("IDCardNum") && !string.IsNullOrEmpty(row["IDCardNum"].ToString()))
            {
                IDCardNum = row["IDCardNum"].ToString();
            }
            // 身份证正反面
            string IDCard1 = null;
            if (row.Table.Columns.Contains("IDCard1") && !string.IsNullOrEmpty(row["IDCard1"].ToString()))
            {
                IDCard1 = row["IDCard1"].ToString();
            }
            string IDCard2 = null;
            if (row.Table.Columns.Contains("IDCard2") && !string.IsNullOrEmpty(row["IDCard2"].ToString()))
            {
                IDCard2 = row["IDCard2"].ToString();
            }

            //查询小区
            Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(communityId);
            //构造链接字符串
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }

            string RoomName = null;

            using (var conn = new SqlConnection(GetConnectionStr(Community)))
            {
                string sql = @"SELECT isnull(RoomName,RoomSign) FROM Tb_HSPR_Room WHERE RoomID=@RoomID";

                // 敏捷，取NC系统房屋名称
                if (Community.CorpID == 1973)
                {
                    sql = @"SELECT isnull(NC_NewRoomSign, isnull(RoomName, RoomSign)) FROM Tb_HSPR_Room WHERE RoomID=@RoomID";
                }
                RoomName = conn.Query<string>(sql, new { RoomID = RoomID }).FirstOrDefault();
            }

            using (var conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                string sql = @"SELECT IsAutoAudit FROM Tb_AuditControl_BindingRoom WHERE CorpID=@CorpID;";

                int isAutoAudit = conn.Query<int>(sql, new { CorpID = Community.CorpID }).FirstOrDefault();

                sql = @"SELECT * FROM Tb_Audit_BindingRoom WHERE ApplicantUserID=@ApplicantUserID
                        AND RoomID=@RoomID AND IsAllows IS NULL AND IsDelete=0";

                if (conn.Query(sql, new { ApplicantUserID = ApplicantUserID, RoomID = RoomID }).Count() > 0)
                {
                    return new ApiResult(false, "已申请绑定该房屋").toJson();
                }

                string IID = Guid.NewGuid().ToString().ToUpper();
                sql = @"INSERT INTO Tb_Audit_BindingRoom(IID, ApplicantUserID, CommunityID, 
                            RoomID, RoomName, CustName, CustMobile, IDCard1, IDCard2, IDCardNum) 
                        VALUES (@IID, @ApplicantUserID, @CommunityID, @RoomID, @RoomName, @CustName, @CustMobile, 
                        @IDCard1, @IDCard2, @IDCardNum)";

                // 插入审核信息
                conn.Execute(sql, new
                {
                    IID = IID,
                    CommunityID = Community.Id,
                    RoomID = RoomID,
                    RoomName = RoomName,
                    ApplicantUserID = ApplicantUserID,
                    CustName = CustName,
                    CustMobile = CustMobile,
                    IDCard1 = IDCard1,
                    IDCard2 = IDCard2,
                    IDCardNum = IDCardNum,
                });

                // 是否自动审核
                if (isAutoAudit > 0)
                {
                    return Audit(1, IID, "自动审核通过");
                }
                return new ApiResult(true, IID).toJson();
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
                conn.Execute(@"UPDATE Tb_Audit_BindingRoom SET IsDelete=1,Remark='业主取消审核' 
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

            int IsAllows = 0;
            if (row["IsAllows"].ToString() == "1" || row["IsAllows"].ToString() == "通过")
            {
                IsAllows = 1;
            }
                        
            string IID = row["IID"].ToString();
            string Remark = row["Remark"].ToString();

            return Audit(IsAllows, IID, Remark);
        }

        /// <summary>
        /// 审核操作
        /// </summary>
        private string Audit(int IsAllows, string IID, string Remark)
        {
            try
            {
                using (var conn = new SqlConnection(PubConstant.UnifiedContionString))
                {
                    // 小区信息
                    Tb_Community Community = conn.Query<Tb_Community>(@"SELECT * FROM Tb_Community WHERE Id IN 
                            (SELECT CommunityID FROM Tb_Audit_BindingRoom WHERE IID=@IID)",
                            new { IID = IID }).FirstOrDefault();

                    if (Community == null)
                    {
                        return JSONHelper.FromString(false, "未找到小区信息");
                    }

                    // ERP数据库连接字符串
                    PubConstant.hmWyglConnectionString = GetConnectionStringStr(Community);

                    string sql = @"UPDATE Tb_Audit_BindingRoom SET IsAllows=@IsAllows,AuditTime=getdate(),Remark=@Remark 
                                WHERE IID=@IID;";

                    int i = conn.Execute(sql, new
                    {
                        IsAllows = IsAllows,
                        IID = IID,
                        Remark = Remark,
                    });

                    if (i > 0)
                    {
                        // 审核信息
                        dynamic AuditInfo = conn.Query(@"SELECT * FROM Tb_Audit_BindingRoom WHERE IID=@IID;",
                            new { IID = IID }).FirstOrDefault();

                        // 用户信息
                        dynamic UserInfo = conn.Query(@"SELECT * FROM Tb_User WHERE Id=@Id",
                            new { Id = AuditInfo.ApplicantUserID }).FirstOrDefault();

                        // 绑定关系
                        dynamic BindedInfo = conn.Query(@"SELECT * FROM Tb_User_Relation WHERE UserId=@UserID AND RoomId=@RoomID;",
                            new { UserID = AuditInfo.ApplicantUserID, RoomID = AuditInfo.RoomID }).FirstOrDefault();

                        string UserID = AuditInfo.ApplicantUserID.ToString();
                        string UserMobile = UserInfo.Mobile.ToString();
                        string NickName = UserInfo.NickName.ToString();
                        string CommunityID = AuditInfo.CommunityID.ToString();
                        string RoomID = AuditInfo.RoomID.ToString();
                        string CustName = AuditInfo.CustName.ToString();
                        string CustMobile = AuditInfo.CustMobile.ToString();

                        // 房屋名称
                        string RoomName;
                        if (Community.CorpID == 1971)
                        {
                            RoomName = new SqlConnection(PubConstant.hmWyglConnectionString)
                                .Query<string>(@"SELECT isnull(NC_NewRoomSign,isnull(RoomName,RoomSign)) 
                                                    FROM Tb_HSPR_Room WHERE RoomID=@RoomID",
                                new { RoomID = AuditInfo.RoomID }).FirstOrDefault();
                        }
                        else
                        {
                            RoomName = new SqlConnection(PubConstant.hmWyglConnectionString)
                                .Query<string>(@"SELECT isnull(RoomName,RoomSign) FROM Tb_HSPR_Room WHERE RoomID=@RoomID",
                                new { RoomID = AuditInfo.RoomID }).FirstOrDefault();
                        }

                        // 短信信息
                        string messageForTenant, messageForCust, errorMessage;
                        if (IsAllows == 1)
                        {
                            messageForTenant = $"您申请绑定小区[{Community.CommName}]房屋地址为：{RoomName}的申请已审核通过。";
                            messageForCust = $"用户{NickName}申请绑定您在[{Community.CommName}]房屋地址为：{RoomName}的申请已审核通过。";
                        }
                        else
                        {
                            Remark = string.IsNullOrEmpty(Remark) ? "无" : Remark;
                            messageForTenant = $"您申请绑定小区[{Community.CommName}]房屋地址为：{RoomName}的申请已被驳回，驳回原因：{Remark}";
                            messageForCust = $"用户{NickName}申请绑定您在[{Community.CommName}]房屋地址为：{RoomName}的申请已被驳回，驳回原因：{Remark}";
                        }


                        SendShortMessage(UserMobile, messageForTenant, out errorMessage, Community.CorpID);
                        SendShortMessage(CustMobile, messageForCust, out errorMessage, Community.CorpID);

                        // 该房屋已绑定，但被锁定
                        if (BindedInfo != null)
                        {
                            conn.Execute("UPDATE Tb_User_Relation SET Locked=0 WHERE Id=@Id", new { Id = BindedInfo.Id });
                            return JSONHelper.FromString(true, "操作成功");
                        }

                        using (var erpConn = new SqlConnection(GetConnectionStringStr(Community)))
                        {
                            // 读取业主信息
                            sql = @"SELECT CustID,(SELECT TOP 1 CustName FROM Tb_HSPR_Customer b 
                                                    WHERE b.CustID=x.CustID) AS CustName,
                                    (SELECT TOP 1 isnull(isnull(MobilePhone,LinkmanTel),'') FROM Tb_HSPR_Customer b 
                                                    WHERE b.CustID=x.CustID) AS CustMobile,
                                    RoomID,
                                    (SELECT TOP 1 isnull(RoomName,RoomSign) FROM Tb_HSPR_Room 
                                                    WHERE RoomID=@RoomID) AS RoomSign
                                    FROM Tb_HSPR_CustomerLive x WHERE RoomID=@RoomID AND isnull(IsDelLive,0)=0 AND LiveType=1";

                            dynamic CustInfo = erpConn.Query(sql, new { RoomID = RoomID }).FirstOrDefault();
                            string custId = CustInfo.CustID.ToString();
                            string ERPCustName = CustInfo.CustName.ToString();
                            string ERPCustMobile = CustInfo.CustMobile.ToString();
                            string roomSign = CustInfo.RoomSign.ToString();

                            // 家庭成员信息、绑定房屋关系
                            string newRelation = Guid.NewGuid().ToString();
                            conn.Execute(@"INSERT INTO Tb_User_Relation(Id,UserId,CommunityId,CustId,RoomId,RegDate,
                                            CustName,RoomSign,CustMobile,Locked)
                                           VALUES(@Id,@UserId,@CommunityId,@CustId,@RoomId,getdate(),
                                            @CustName,@RoomSign,@CustMobile,0)",
                                            new
                                            {
                                                Id = newRelation,
                                                UserId = UserID,
                                                CommunityId = CommunityID,
                                                CustId = custId,
                                                RoomId = RoomID,
                                                CustName = ERPCustName,
                                                CustMobile = ERPCustMobile,
                                                RoomSign = roomSign
                                            });

                            // 家庭成员信息是否存在
                            sql = @"SELECT HoldID FROM Tb_HSPR_Household WHERE RoomID=@RoomID AND MobilePhone LIKE @MobilePhone";

                            long holdId = erpConn.Query<long>(sql, new { RoomID = RoomID, MobilePhone = $"%{UserInfo.Mobile.ToString()}%" }).FirstOrDefault();

                            // 存在则更新
                            if (holdId > 0)
                            {
                                erpConn.Execute(@"UPDATE Tb_HSPR_Household SET Relationship='0029',IsDelete=0 
                                                    WHERE HoldID=@HoldID",
                                                new { HoldID = holdId });

                                return JSONHelper.FromString(true, "操作成功");
                            }

                            // 插入家庭成员信息
                            DynamicParameters parameters = new DynamicParameters();
                            parameters.Add("@HoldID", 0, DbType.Int64, ParameterDirection.Output);
                            parameters.Add("@CommID", Community.CommID);
                            parameters.Add("@CustID", custId);
                            parameters.Add("@RoomID", RoomID);
                            parameters.Add("@Name", CustName);
                            parameters.Add("@MobilePhone", UserInfo.Mobile.ToString());

                            if (Community.CorpID == 1971)
                            {
                                // 敏捷默认为租户
                                parameters.Add("@Relationship", "0031");
                            }
                            else
                            {   // 其他默认为家庭成员
                                parameters.Add("@Relationship", "0030");
                            }

                            erpConn.Execute("Proc_HSPR_Household_Insert_Phone", parameters, null, null, CommandType.StoredProcedure);
                            holdId = parameters.Get<long>("@HoldID");

                            conn.Execute(@"UPDATE Tb_User_Relation SET CustHoldId=@HoldID WHERE Id=@RelationId",
                                new { HoldID = holdId, RelationId = newRelation });

                            return JSONHelper.FromString(true, "操作成功");
                        }
                    }

                    return new ApiResult(false, "操作失败").toJson();
                }
            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                return new ApiResult(false, ex.Message + ex.StackTrace).toJson();
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
            if (!row.Table.Columns.Contains("PageSize") || string.IsNullOrEmpty(row["PageSize"].ToString()))
            {
                return JSONHelper.FromString(false, "缺少PageSize参数");
            }
            if (!row.Table.Columns.Contains("PageIndex") || string.IsNullOrEmpty(row["PageIndex"].ToString()))
            {
                return JSONHelper.FromString(false, "缺少PageIndex参数");
            }

            string communityId = row["CommunityId"].AsString();
            int PageSize = AppGlobal.StrToInt(row["PageSize"].AsString());
            int PageIndex = AppGlobal.StrToInt(row["PageIndex"].AsString());
            string RoomID = null;
            string ApplicantUserID = null;

            if (row.Table.Columns.Contains("RoomID") && !string.IsNullOrEmpty(row["RoomID"].ToString()))
            {
                RoomID = row["RoomID"].ToString();
            }
            if (row.Table.Columns.Contains("ApplicantUserID") && !string.IsNullOrEmpty(row["ApplicantUserID"].ToString()))
            {
                ApplicantUserID = row["ApplicantUserID"].ToString();
            }

            //查询小区
            Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(communityId);
            //构造链接字符串
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }

            string sql = $@"SELECT IID,ApplicantUserID,CommunityID,
                            (SELECT CommName FROM Tb_Community x WHERE x.ID=CommunityID) AS CommName,
                            RoomID,RoomName,CustName,CustMobile,AddTime,IDCardNum,IDCard1,IDCard2 
                            FROM Tb_Audit_BindingRoom  
                            WHERE CommunityID='{Community.Id}' AND IsAllows IS NULL AND isnull(IsDelete,0)=0";
 
            if (!string.IsNullOrEmpty(RoomID))
            {
                sql += $@" AND RoomID={RoomID}";
            }
            if (!string.IsNullOrEmpty(ApplicantUserID))
            {
                sql += $@" AND ApplicantUserID='{ApplicantUserID}'";
            }

            DataTable dataTable = GetList(out int pageCount, out int count, sql, PageIndex, PageSize,
                "AddTime", 0, "IID", PubConstant.UnifiedContionString).Tables[0];

            using (var erpConn = new SqlConnection(GetConnectionStr(Community)))
            {
                dataTable.Columns.Add(new DataColumn("BuildSNum", typeof(string)));
                dataTable.Columns.Add(new DataColumn("UnitSNum", typeof(string)));
                dataTable.Columns.Add(new DataColumn("FloorSNum", typeof(string)));

                foreach (DataRow item in dataTable.Rows)
                {
                    dynamic roomInfo = erpConn.Query<dynamic>($@"SELECT BuildSNum,UnitSNum,FloorSNum FROM Tb_HSPR_Room 
                                                                    WHERE RoomID={item["RoomID"]}").FirstOrDefault();

                    if (roomInfo != null)
                    {
                        item["BuildSNum"] = roomInfo.BuildSNum.ToString();
                        item["UnitSNum"] = roomInfo.UnitSNum.ToString();
                        item["FloorSNum"] = roomInfo.FloorSNum.ToString();
                    }
                }
            }

            string result = JSONHelper.FromString(true, dataTable);
            result = result.Insert(result.Length - 1, ",\"PageCount\":" + pageCount);
            return result;
        }


        /// <summary>
        /// 已审核列表
        /// </summary>
        private string AuditList(DataRow row)
        {
            //commID
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "缺少CommunityId参数");
            }
            if (!row.Table.Columns.Contains("PageSize") || string.IsNullOrEmpty(row["PageSize"].ToString()))
            {
                return JSONHelper.FromString(false, "缺少PageSize参数");
            }
            if (!row.Table.Columns.Contains("PageIndex") || string.IsNullOrEmpty(row["PageIndex"].ToString()))
            {
                return JSONHelper.FromString(false, "缺少PageIndex参数");
            }

            string CommID = row["CommID"].AsString();
            string IsAllows = null;
            int PageSize = AppGlobal.StrToInt(row["PageSize"].AsString());
            int PageIndex = AppGlobal.StrToInt(row["PageIndex"].AsString());
            string RoomID = null;

            if (row.Table.Columns.Contains("RoomID") && !string.IsNullOrEmpty(row["RoomID"].ToString()))
            {
                RoomID = row["RoomID"].ToString();
            }
            if (row.Table.Columns.Contains("IsAllows") && !string.IsNullOrEmpty(row["IsAllows"].ToString()))
            {
                IsAllows = row["IsAllows"].ToString();
            }

            // 查询小区
            Tb_Community Community = GetCommunity(CommID);
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }

                var sql = $@"SELECT IID,ApplicantUserID,CommunityID,
                            (SELECT CommName FROM Tb_Community x WHERE x.ID=CommunityID) AS CommName,
                            RoomID,RoomName,CustName,CustMobile,AddTime,IDCardNum,IDCard1,IDCard2 
                            FROM Tb_Audit_BindingRoom  
                            WHERE CommunityID='{Community.Id}' AND isnull(IsDelete,0)=0";

                if (!string.IsNullOrEmpty(IsAllows))
                {
                    sql += $" AND IsAllows={IsAllows}";
                }

                if (!string.IsNullOrEmpty(RoomID))
                {
                    sql += $" AND RoomID={RoomID}";
                }

            DataTable dataTable = GetList(out int pageCount, out int counts, sql, PageIndex, PageSize, "AddTime", 1, "IID",
                PubConstant.UnifiedContionString).Tables[0];

            string result = JSONHelper.FromString(true, dataTable);
            result = result.Insert(result.Length - 1, ",PageCount:" + pageCount);
            return result;
        }


        /// <summary>
        /// 业主解绑审核
        /// </summary>
        private string RemoveBind(DataRow row)
        {
            if (!row.Table.Columns.Contains("IID") || string.IsNullOrEmpty(row["IID"].ToString()))
            {
                return JSONHelper.FromString(false, "缺少IID参数");
            }

            string IID = row["IID"].AsString();

            using (var conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                conn.Execute(@"delete Tb_User_Relation where id=@IID
                   ", new { IID = IID });

                return JSONHelper.FromString(true, "解绑成功");
            }
        }

    }
}
