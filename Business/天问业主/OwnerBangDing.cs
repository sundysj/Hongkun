using System;
using MobileSoft.DBUtility;
using MobileSoft.Common;
using System.Data;
using System.Text;
using System.Linq;
using System.Data.SqlClient;
using MobileSoft.Model.Unified;
using Dapper;
using DapperExtensions;
using System.Collections.Generic;

namespace Business
{
    /// <summary>
    /// 绑定业主
    /// </summary>
    public class OwnerBangDing : PubInfo
    {
        public OwnerBangDing() //获取小区、项目信息
        {
            base.Token = "20160817OwnerBangDing";

        }
        public override void Operate(ref Common.Transfer Trans)
        {
            Trans.Result = JSONHelper.FromString(false, "未知错误");

            DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
            DataRow Row = dAttributeTable.Rows[0];

            switch (Trans.Command)
            {
                case "BindCommunity":
                    Trans.Result = BindCommunity(Row);//绑定房屋
                    break;
                case "BindRoom_20161018":
                    Trans.Result = BindRoom_20161018(Row);//绑定房屋20161018
                    break;
                case "UnBindCustomer"://解绑房屋
                    Trans.Result = UnBindCustomer(Row);
                    break;
                case "GetBindList":
                    Trans.Result = GetBindList(Row);//获取已绑定小区列表
                    break;
                case "GetBindListfroCom":
                    Trans.Result = GetBindListfroCom(Row);//获取本小区已绑定房屋列表
                    break;
                case "GetBanList":
                    Trans.Result = GetBanList(Row);//根据小区ID100-10003获取楼栋【ID、name】
                    break;
                case "GetRoomLIst":
                    Trans.Result = GetRoomLIst(Row);//根据楼栋获取房间【ID、name】
                    break;
                case "BindRoomNoMobile":
                    Trans.Result = BindRoomNoMobile(Row);
                    break;
                case "GetBuildListByRegion":
                    Trans.Result = GetBuildListByRegion(Row); // 根据组团区域获取楼栋
                    break;
                case "CheckBindMobile":
                    Trans.Result = CheckBindMobile(Row); // 检查绑定手机
                    break;
                case "GetHouseKeeper":
                    Trans.Result = GetHouseKeeper(Row); // 获取楼栋管家信息
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 根据roomid获取楼栋管家手机号和名字
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string GetHouseKeeper(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return new ApiResult(false, "小区编码不能为空").toJson();
            }
            string communityId = row["CommunityId"].ToString();

            if (!row.Table.Columns.Contains("RoomId") || string.IsNullOrEmpty(row["RoomId"].ToString()))
            {
                return new ApiResult(false, "请选择房屋").toJson();
            }
            string roomId = row["RoomId"].ToString();
            Tb_Community tb_Community = getCommunity(communityId);
            if (null == tb_Community)
            {
                return new ApiResult(false, "小区不存在").toJson();
            }
            string connStr = GetConnectionStringStr(tb_Community);
            DataTable dt;
            using (IDbConnection conn = new SqlConnection(connStr))
            {
                dt = conn.ExecuteReader("SELECT UserName,MobileTel FROM Tb_Sys_User a INNER JOIN Tb_HSPR_BuildHousekeeper b ON a.UserCode = b.UserCode INNER JOIN Tb_HSPR_Room c ON b.CommID = c.CommID AND b.BuildSNum = c.BuildSNum WHERE c.RoomId = @RoomId", new { RoomId = roomId }, null, null, CommandType.Text).ToDataSet().Tables[0];
            }
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();

            if (null != dt)
            {
                Dictionary<string, object> dictionary;
                foreach (DataRow item in dt.Rows)
                {
                    dictionary = new Dictionary<string, object>();
                    foreach (DataColumn colum in dt.Columns)
                    {
                        dictionary.Add(colum.ColumnName, item[colum.ColumnName]);
                    }
                    list.Add(dictionary);
                }
            }
            return new ApiResult(true, list).toJson();
        }

        private string CheckBindMobile(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return new ApiResult(false, "小区编码不能为空").toJson();
            }
            string communityId = row["CommunityId"].ToString();
            if (!row.Table.Columns.Contains("Mobile") || string.IsNullOrEmpty(row["Mobile"].ToString()))
            {
                return new ApiResult(false, "请输入手机号").toJson();
            }
            string mobile = row["Mobile"].ToString();
            if (!row.Table.Columns.Contains("RoomId") || string.IsNullOrEmpty(row["RoomId"].ToString()))
            {
                return new ApiResult(false, "请选择房屋").toJson();
            }
            string roomId = row["RoomId"].ToString();
            Tb_Community tb_Community = getCommunity(communityId);
            if (null == tb_Community)
            {
                return new ApiResult(false, "小区不存在").toJson();
            }

            //验证手机号格式
            if (mobile.Length != 11 && mobile.Length != 8 && mobile.Length != 13)
            {
                return JSONHelper.FromString(false, "手机号格式不正确");
            }


            //查询该小区内是否存在此业主或家庭成员
            DataSet ds = GetOwnerInfo(mobile, tb_Community, roomId);
            if (ds == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
            {
                return new ApiResult(false, "该业主或家庭成员不存在").toJson();
            }
            return new ApiResult(true, "存在该业主/家庭成员").toJson();
        }

        private string GetBindListfroCom(DataRow row)
        {
            try
            {
                if (!row.Table.Columns.Contains("Mobile") || string.IsNullOrEmpty(row["Mobile"].ToString()))
                {
                    return JSONHelper.FromString(false, "账号不正确");
                }
                if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
                {
                    return JSONHelper.FromString(false, "小区编码不能为空");
                }

                string mobile = row["Mobile"].ToString();
                string communityId = row["CommunityId"].ToString();
                Tb_Community tb_Community = getCommunity(communityId);
                if (null == tb_Community)
                {
                    return new ApiResult(false, "小区不存在").toJson();
                }

                string strcon = GetConnectionStringStr(tb_Community);

                using (IDbConnection conn1 = new SqlConnection(Connection.GetConnection("4")))
                {
                    IEnumerable<dynamic> resultSet = conn1.Query(@"select r.Id as RelationID, r.CommunityId,ISNULL(r.CustId,0) as CustID,ISNULL(r.CustId,0) as CustId,ISNULL(r.CustHoldId,0) as CustHoldId,r.RoomSign,r.RoomSign AS RoomName,r.RoomID,r.RoomId,c.CommName as CommName,r.CustName as CustName from Tb_User_Relation as r left join Tb_User as u on u.Id = r.UserId left JOIN Tb_Community as c on r.CommunityId = c.Id where isnull(r.locked,0)=0 AND u.Mobile = @Mobile and (r.CommunityId=@CommunityId OR c.CommID=@CommunityId) group by r.Id,r.CommunityId,r.CustId,r.CustHoldId,r.RoomSign,r.RoomId,c.CommName,r.CustName",
                        new { Mobile = mobile, CommunityId = communityId });

                    if (resultSet.Count() != 0)
                    {
                        using (IDbConnection conn2 = new SqlConnection(strcon))
                        {
                            // 查询房屋所在楼栋单元
                            foreach (dynamic item in resultSet)
                            {
                                dynamic itemInfo = conn2.Query(@"SELECT (SELECT b.BuildName from Tb_HSPR_Building b WHERE b.CommID=a.CommID AND b.BuildSNum=a.BuildSNum AND ISNULL(b.IsDelete,0)=0) AS BuildName ,a.UnitSNum FROM Tb_HSPR_Room a WHERE a.RoomID = @RoomID", new { RoomID = item.RoomId }).FirstOrDefault();

                                if (itemInfo != null)
                                {
                                    item.BuildName = itemInfo.BuildName;
                                    item.UnitSNum = itemInfo.UnitSNum;
                                }
                                else
                                {
                                    item.BuildName = null;
                                    item.UnitSNum = null;
                                }
                            }
                        }
                    }

                    return new ApiResult(true, resultSet).toJson();

                }
            }
            catch (Exception ex)
            {
                return new ApiResult(false, ex.Message + Environment.NewLine + ex.StackTrace).toJson();
            }

            

            //IDbConnection Conn = new SqlConnection(Connection.GetConnection("4"));
            //StringBuilder sb = new StringBuilder();
            //sb.AppendFormat(" select r.Id as RelationID, r.CommunityId,ISNULL(r.CustId,0) as CustId,ISNULL(r.CustHoldId,0) as CustHoldId,r.RoomSign,r.RoomId,c.CommName as CommName,r.CustName as CustName from Tb_User_Relation as r left join Tb_User as u on u.Id = r.UserId left JOIN Tb_Community as c on r.CommunityId = c.Id where u.Mobile = '{0}' and (r.CommunityId='{1}' OR c.CommID='{1}') group by r.Id,r.CommunityId,r.CustId,r.CustHoldId,r.RoomSign,r.RoomId,c.CommName,r.CustName", row["Mobile"].ToString(), row["CommunityId"].ToString());
            //DataSet ds = Conn.ExecuteReader(sb.ToString(), null, null, null, CommandType.Text).ToDataSet();
            //return JSONHelper.FromString(ds.Tables[0]);
        }

        /// <summary>
        /// 解绑绑定的业主
        /// </summary>
        /// <param name="row">行</param>
        /// <returns></returns>
        private string UnBindCustomer(DataRow Row)
        {
            string backstr = "";
            try
            {
                IDbConnection Conn = new SqlConnection(Connection.GetConnection("4"));
                string Query = "UPDATE Tb_User_Relation SET Locked=1 WHERE Id=@Id";
                Conn.Execute(Query, new { Id = Row["RelationId"].ToString() });
            }
            catch (Exception ex)
            {

                backstr = ex.Message;
            }
            if (backstr == "")
            {
                return JSONHelper.FromString(true, "success");
            }
            else
            {
                return JSONHelper.FromString(false, backstr);
            }

        }

        #region 绑定房屋

        /// <summary>
        /// 绑定房屋
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string BindCommunity(DataRow row)
        {
            if (!row.Table.Columns.Contains("UserId") || string.IsNullOrEmpty(row["UserId"].ToString()))
            {
                return JSONHelper.FromString(false, "用户编码不能为空");
            }
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编码不能为空");
            }
            if ((!row.Table.Columns.Contains("RoomId") && !row.Table.Columns.Contains("ParkingId")))
            {
                return JSONHelper.FromString(false, "房屋编码或者车位编号不能为空");
            }
            if (!row.Table.Columns.Contains("Mobile") && string.IsNullOrEmpty(row["Mobile"].ToString()))
            {
                return JSONHelper.FromString(false, "手机号不能为空");
            }
            string UserId = row["UserId"].ToString();
            string CommunityId = row["CommunityId"].ToString();
            string Mobile = row["Mobile"].ToString();
            string RoomId = row["RoomId"].ToString(); ;
            string ParkingId = "";


            //获取小区
            Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(CommunityId);

            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }
            //验证手机号格式
            if (Mobile.Length!=11&&Mobile.Length!=8&&Mobile.Length !=13)
            {
                return JSONHelper.FromString(false, "手机号格式不正确");
            }

            //查询该小区内是否存在此业主或家庭成员
            DataSet ds = GetOwnerInfo(Mobile, Community, RoomId);
            if (ds == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
            {
                return JSONHelper.FromString(false, "该业主或家庭成员不存在");
            }
            //构建绑定信息
            #region 构建绑定信息

            string CustId = ds.Tables[0].Rows[0]["CustID"].ToString();
            string CustHoldId = ds.Tables[0].Rows[0]["HoldID"].ToString();
            if (row.Table.Columns.Contains("RoomId"))
            {
                RoomId = row["RoomId"].ToString();
            }
            if (row.Table.Columns.Contains("ParkingId"))
            {
                ParkingId = row["ParkingId"].ToString();
            }

            Tb_User_Relation model = new Tb_User_Relation();
            model.Id = Guid.NewGuid().ToString();
            model.UserId = UserId;
            model.CommunityId = CommunityId;
            model.CustId = CustId;
            model.CustHoldId = CustHoldId;
            model.RoomId = RoomId;
            model.ParkingId = ParkingId;
            model.IsCurr = "";
            model.RegDate = DateTime.Now;




            model.CustName = ds.Tables[0].Rows[0]["CustName"].ToString();
            model.RoomSign = ds.Tables[0].Rows[0]["RoomSign"].ToString();


            // model.Custmobile = ds.Tables[0].Rows[0]["Custmobile"].ToString();
            if (ds.Tables[0].Rows[0]["MobilePhone"].ToString() != "")
            {
                model.Custmobile = ds.Tables[0].Rows[0]["MobilePhone"].ToString();
            }
            if (ds.Tables[0].Rows[0]["LinkmanTel"].ToString() != "")
            {
                model.Custmobile = ds.Tables[0].Rows[0]["LinkmanTel"].ToString();
            }
            #endregion

            //切换数据库为运营系统数据库
            IDbConnection Connectionstr = new SqlConnection(Connection.GetConnection("4"));
            //查询此业主是否已绑定房屋
            List<Tb_User_Relation> list = Connectionstr.Query<Tb_User_Relation>("select * from Tb_User_Relation where UserId='" + UserId + "' and RoomId='" + RoomId + "' and CommunityId='" + CommunityId + "'").AsList<Tb_User_Relation>();
            if (list.Count > 0)//如果已绑则换绑
            {
                Tb_User_Relation modelold = list[0];
                model.Id = modelold.Id;
                modelold.UserId = modelold.UserId;
                Connectionstr.Update<Tb_User_Relation>(model);
            }
            else//未绑则新绑
            {
                Connectionstr.Insert<Tb_User_Relation>(model);
            }
            Tb_Sms_Account smsModel = SmsInfo.GetSms_Account();

            string sql = "select * from Tb_User where Mobile=@Mobile";
            Tb_User User = Connectionstr.Query<Tb_User>(sql, new { Mobile = Mobile }).ToList<Tb_User>().SingleOrDefault();
            if (User == null)
            {
                return JSONHelper.FromString(false, "该用户不存在");
            }

            //发送短信
            //手机号 绑定 小区名称+房间号 成功【天问互联】
            //int Result = Common.Sms.Send(smsModel.SmsAccount, smsModel.SmsPwd, Mobile, "用户：" + User.Mobile + "绑定" + Community.CommName + model.RoomSign + "成功" + smsModel.Sign, "", "");

            int Result = Common.Sms.Send_v2(smsModel.SmsUserId, smsModel.SmsAccount, smsModel.SmsPwd, Mobile, "用户：" + User.Mobile + "绑定" + Community.CommName + model.RoomSign + "成功" + smsModel.Sign, out string strErrMsg);

            //记录短信
            new MobileSoft.BLL.Common.Bll_Tb_SendMessageRecord().Add(Mobile, "用户：" + User.Mobile + "绑定" + Community.CommName + model.RoomSign + "成功" + smsModel.Sign, Guid.NewGuid().ToString(), "房屋绑定", Result.ToString());
            return JSONHelper.FromString(true, "绑定成功");
        }
    

        /// <summary>
        /// 绑定房屋20161018
        /// 更新内容：处理【该用户不存在】BUG
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string BindRoom_20161018(DataRow row)
        {
            if (!row.Table.Columns.Contains("UserId") || string.IsNullOrEmpty(row["UserId"].ToString()))
            {
                return JSONHelper.FromString(false, "用户编码不能为空");
            }
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编码不能为空");
            }
            if ((!row.Table.Columns.Contains("RoomId") && !row.Table.Columns.Contains("ParkingId")))
            {
                return JSONHelper.FromString(false, "房屋id或者车位id不能为空");
            }
            if (!row.Table.Columns.Contains("Mobile") && string.IsNullOrEmpty(row["Mobile"].ToString()))
            {
                return JSONHelper.FromString(false, "手机号不能为空");
            }
            string UserId = row["UserId"].ToString();
            string CommunityId = row["CommunityId"].ToString();
            string Mobile = row["Mobile"].ToString();
            string RoomId = row["RoomId"].ToString();
            string ParkingId = "";


            //获取小区
            Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(CommunityId);
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }

            PubConstant.hmWyglConnectionString = GetConnectionStr(Community);

            //验证手机号格式
            if (Mobile.Length != 11 && Mobile.Length != 8 && Mobile.Length != 13)
            {
                return JSONHelper.FromString(false, "手机号格式不正确");
            }


            //查询该小区内是否存在此业主或家庭成员
            DataSet ds = GetOwnerInfo(Mobile, Community, RoomId);
            if (ds == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
            {
                return JSONHelper.FromString(false, "该业主或家庭成员不存在");
            }
            //构建绑定信息
            #region 构建绑定信息

            string CustId = ds.Tables[0].Rows[0]["CustID"].ToString();
            string CustHoldId = ds.Tables[0].Rows[0]["HoldID"].ToString();
            if (row.Table.Columns.Contains("RoomId"))
            {
                RoomId = row["RoomId"].ToString();
            }
            if (row.Table.Columns.Contains("ParkingId"))
            {
                ParkingId = row["ParkingId"].ToString();
            }

            Tb_User_Relation model = new Tb_User_Relation
            {
                Id = Guid.NewGuid().ToString(),
                UserId = UserId,
                CommunityId = Community.Id,
                CustId = CustId,
                CustHoldId = CustHoldId,
                RoomId = RoomId,
                ParkingId = ParkingId,
                IsCurr = "",
                RegDate = DateTime.Now,
                CustName = ds.Tables[0].Rows[0]["CustName"].ToString(),
                RoomSign = ds.Tables[0].Rows[0]["RoomSign"].ToString()
            };

            if (ds.Tables[0].Rows[0]["MobilePhone"].ToString() != "")
            {
                model.Custmobile = ds.Tables[0].Rows[0]["MobilePhone"].ToString();
            }
            else 
            {
                if (ds.Tables[0].Rows[0]["LinkmanTel"].ToString() != "")
                {
                    model.Custmobile = ds.Tables[0].Rows[0]["LinkmanTel"].ToString();
                }
            }

            #endregion

            using (IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                Tb_User User = conn.Query<Tb_User>(@"SELECT * from Tb_User WHERE Id=@UserId", new { UserId = UserId }).FirstOrDefault();
                if (User == null)
                {
                    return JSONHelper.FromString(false, "该用户不存在");
                }

                // 标记是否需要插入家庭成员信息
                bool needAddHousehold = false;

                var relation = conn.Query<Tb_User_Relation>(@"SELECT * FROM Tb_User_Relation WHERE UserID=@UserID AND RoomID=@RoomID",
                    new { UserID = UserId, RoomID = RoomId }).FirstOrDefault();

                // 已绑定该房屋，解绑
                if (relation != null)
                {
                    conn.Execute(@"UPDATE Tb_User_Relation SET Locked=0 WHERE Id=@RelationId", new { RelationId = relation.Id });

                    if (string.IsNullOrEmpty(relation.CustHoldId) || relation.CustHoldId == "0")
                    {
                        needAddHousehold = false;
                        model.Id = relation.Id;
                    }
                }
                else
                {
                    needAddHousehold = true;

                    // 插入绑定关系
                    conn.Insert<Tb_User_Relation>(model);
                }

                if (needAddHousehold)
                {
                    using (IDbConnection erpConn = new SqlConnection(PubConstant.hmWyglConnectionString))
                    {
                        // 业主信息插入到家庭成员表
                        var parameters = new DynamicParameters();
                        parameters.Add("@HoldID", 0, DbType.Int64, ParameterDirection.Output);
                        parameters.Add("@CommID", Community.CommID);
                        parameters.Add("@CustID", CustId);
                        parameters.Add("@RoomID", RoomId);
                        parameters.Add("@Name", model.CustName);
                        parameters.Add("@MobilePhone", Mobile);
                        if (Community.CorpID == 1973)
                        {
                            parameters.Add("@Relationship", "0013");
                        }
                        else
                        {
                            parameters.Add("@Relationship", UserRoomIdentityCode.Customer1);
                        }
                        parameters.Add("@IsDelete", 0);

                        erpConn.Execute("Proc_HSPR_Household_Insert_Phone", parameters, null, null, CommandType.StoredProcedure);

                        model.CustHoldId = parameters.Get<long>("@HoldID").ToString();

                        conn.Update<Tb_User_Relation>(model);
                    }
                }

                if (Community.CorpID == 1983)
                {
                    // 大洋汇丰微信绑定,要求不发送短信提示
                    return JSONHelper.FromString(true, "绑定成功");
                }

                Tb_Sms_Account smsModel = SmsInfo.GetSms_Account();

                string message = "手机用户：" + User.Mobile + "绑定" + Community.CommName + model.RoomSign + "成功。";

                int Result = Common.Sms.Send_v2(smsModel.SmsUserId, smsModel.SmsAccount, smsModel.SmsPwd, Mobile, message + "" + smsModel.Sign, out string strErrMsg);

                //记录短信
                new MobileSoft.BLL.Common.Bll_Tb_SendMessageRecord().Add(Mobile, message + "" + smsModel.Sign, Guid.NewGuid().ToString(), "房屋绑定", Result.ToString());

                return JSONHelper.FromString(true, "绑定成功");
            }
        }


        /// <summary>
        /// 绑定房屋没有客户手机号【合景】
        /// 2016-10-25
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string BindRoomNoMobile(DataRow row)
        {
            if (!row.Table.Columns.Contains("UserId") || string.IsNullOrEmpty(row["UserId"].ToString()))
            {
                return JSONHelper.FromString(false, "用户编码不能为空");
            }
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编码不能为空");
            }
            if ((!row.Table.Columns.Contains("RoomId") && !row.Table.Columns.Contains("ParkingId")))
            {
                return JSONHelper.FromString(false, "房屋编码或者车位编号不能为空");
            }

            string UserId = row["UserId"].ToString();
            string CommunityId = row["CommunityId"].ToString();
            string RoomId = row["RoomId"].ToString(); ;
            string ParkingId = "";
            string Mobile = "";
            string CustId = "";
            string CustHoldId = "";
            //获取小区
            Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(CommunityId);

            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }
            //查询该小区内是否存在此业主或家庭成员
            DataSet ds = GetOwnerInfo(Community, RoomId);

            //构建绑定信息
            #region 构建绑定信息

            if (row.Table.Columns.Contains("RoomId"))
            {
                RoomId = row["RoomId"].ToString();
            }
            if (row.Table.Columns.Contains("ParkingId"))
            {
                ParkingId = row["ParkingId"].ToString();
            }

            Tb_User_Relation model = new Tb_User_Relation();
            model.Id = Guid.NewGuid().ToString();
            model.UserId = UserId;
            model.CommunityId = CommunityId;
            model.CustId = CustId;
            model.CustHoldId = CustHoldId;
            model.RoomId = RoomId;
            model.ParkingId = ParkingId;
            model.IsCurr = "";
            model.RegDate = DateTime.Now;

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["MobilePhone"].ToString() != "")//联系方式
                {
                    Mobile = ds.Tables[0].Rows[0]["MobilePhone"].ToString();
                    model.Custmobile = ds.Tables[0].Rows[0]["MobilePhone"].ToString();
                }
                if (ds.Tables[0].Rows[0]["LinkmanTel"].ToString() != "")//手机号
                {
                    Mobile = ds.Tables[0].Rows[0]["LinkmanTel"].ToString();
                    model.Custmobile = ds.Tables[0].Rows[0]["LinkmanTel"].ToString();
                }
                model.CustId = ds.Tables[0].Rows[0]["CustID"].ToString();
                //CustHoldId = ds.Tables[0].Rows[0]["HoldID"].ToString();
                model.CustName = ds.Tables[0].Rows[0]["CustName"].ToString();
                model.RoomSign = ds.Tables[0].Rows[0]["RoomSign"].ToString();
            }

            #endregion

            //切换数据库为运营系统数据库
            IDbConnection Connectionstr = new SqlConnection(Connection.GetConnection("4"));
            //查询此业主是否已绑定房屋
            List<Tb_User_Relation> list = Connectionstr.Query<Tb_User_Relation>("select * from Tb_User_Relation where UserId='" + UserId + "' and RoomId='" + RoomId + "' and CommunityId='" + CommunityId + "'").AsList<Tb_User_Relation>();
            if (list.Count > 0)//如果已绑则换绑
            {
                Tb_User_Relation modelold = list[0];
                model.Id = modelold.Id;
                modelold.UserId = modelold.UserId;
                Connectionstr.Update<Tb_User_Relation>(model);
            }
            else//未绑则新绑
            {
                Connectionstr.Insert<Tb_User_Relation>(model);
            }
            Tb_Sms_Account smsModel = SmsInfo.GetSms_Account();

            string sql = "select * from Tb_User where Id=@Mobile";
            Tb_User User = Connectionstr.Query<Tb_User>(sql, new { Mobile = UserId }).ToList<Tb_User>().SingleOrDefault();
            if (User == null)
            {
                return JSONHelper.FromString(false, "该用户不存在");
            }

            //发送短信
            //手机号 绑定 小区名称+房间号 成功【天问互联】
            //int Result = Common.Sms.Send(smsModel.SmsAccount, smsModel.SmsPwd, Mobile, "用户：" + User.Mobile + "绑定" + Community.CommName + model.RoomSign + "成功" + smsModel.Sign, "", "");
            int Result = Common.Sms.Send_v2(smsModel.SmsUserId, smsModel.SmsAccount, smsModel.SmsPwd, Mobile, "用户：" + User.Mobile + "绑定" + Community.CommName + model.RoomSign + "成功" + smsModel.Sign, out string strErrMsg);

            //记录短信
            new MobileSoft.BLL.Common.Bll_Tb_SendMessageRecord().Add(Mobile, "用户：" + User.Mobile + "绑定" + Community.CommName + model.RoomSign + "成功" + smsModel.Sign, Guid.NewGuid().ToString(), "房屋绑定", Result.ToString());
            return JSONHelper.FromString(true, "绑定成功");
        }







        #endregion

        ///
        ///返回信息：小区编号、客户编号、房间ID、房间编号、家庭成员编号【如果无则为''】，业主名称
        /// 身份类型：依次为业主、租户、临时客户
        /// <summary>
        /// 获取业主、房屋信息
        /// </summary>
        /// <param name="row">手机号、小区信息ID</param>
        /// <returns></returns>
        internal DataSet GetOwnerInfo(string Mobile, Tb_Community Community, string RoomId)
        {

            //构建链接字符串
            string ContionString = new Business.CostInfo().GetConnectionStringStr(Community);
            IDbConnection con = new SqlConnection(ContionString);
            //根据手机号查询业主、房屋相关信息
            //SqlParameter[] parameters = {
            //        new SqlParameter("@CommID", SqlDbType.VarChar,36),
            //        new SqlParameter("@MobilePhone", SqlDbType.VarChar,36),
            //        new SqlParameter("@RoomId",SqlDbType.VarChar,36)
            //      };
            //parameters[0].Value = Community.CommID;
            //parameters[1].Value = Mobile;
            //parameters[2].Value = RoomId;


            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@CommID", Community.CommID);
            parameters.Add("@MobilePhone", Mobile);
            parameters.Add("@RoomId", RoomId);

            DataSet Ds = con.ExecuteReader("Proc_Tb_HSPR_CustomerMobileQuery", parameters, null, null, CommandType.StoredProcedure).ToDataSet();
            return Ds;
        }

        /// <summary>
        /// 获取业主信息
        /// </summary>
        /// <param name="Community"></param>
        /// <param name="RoomId"></param>
        /// <returns></returns>
        internal DataSet GetOwnerInfo(Tb_Community Community, string RoomId)
        {
            //构建链接字符串
            string ContionString = new Business.CostInfo().GetConnectionStringStr(Community);
            IDbConnection con = new SqlConnection(ContionString);
            DataSet Ds = con.ExecuteReader("select *  from view_HSPR_CustomerLive_AllFilter where RoomID='" + RoomId + "' and IsDelLive=0 and CommID='" + Community.CommID + "'", null, null, null, CommandType.Text).ToDataSet();

            return Ds;
        }


        /// <summary>
        /// 获取已绑定小区列表
        /// </summary>
        /// <param name="row">手机号</param>
        /// <returns></returns>
        private string GetBindList(DataRow row)
        {
            if (!row.Table.Columns.Contains("Mobile") || string.IsNullOrEmpty(row["Mobile"].ToString()))
            {
                return JSONHelper.FromString(false, "账号不正确");
            }
            IDbConnection Conn = new SqlConnection(Connection.GetConnection("4"));
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(@" select r.Id as RelationID, r.CommunityId,ISNULL(r.CustId,0) as CustId,r.CustName,
                                ISNULL(r.CustHoldId,0) as CustHoldId,r.RoomSign,r.RoomId,c.CommName as CommName 
                                from Tb_User_Relation as r 
                                left join Tb_User as u on u.Id = r.UserId 
                                left JOIN Tb_Community as c on r.CommunityId = c.Id where isnull(r.locked,0)=0 AND u.Mobile = '{0}' 
                                group by r.Id,r.CommunityId,r.CustId,r.CustName,r.CustHoldId,r.RoomSign,r.RoomId,c.CommName", row["Mobile"].ToString());
            DataSet ds = Conn.ExecuteReader(sb.ToString(), null, null, null, CommandType.Text).ToDataSet();
            return JSONHelper.FromString(ds.Tables[0]);
        }

        /// <summary>
        /// 获取小区内的楼栋
        /// </summary>
        /// <param name="row"></param>
        /// 小区编号：CommID 必填
        /// 返回：
        ///     楼栋编号：BuildSNum
        ///     楼栋名称：BuildName
        /// <returns></returns>
        private string GetBanList(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编码不能为空");
            }

            //查询小区
            Tb_Community Community = GetCommunity(row["CommID"].ToString());
            //构造链接字符串
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }

            //构建链接字符串
            string ContionString = new Business.CostInfo().GetConnectionStringStr(Community);
            IDbConnection con = new SqlConnection(ContionString);
            // 2017年6月6日10:31:57 谭洋，查询添加 isnull(IsDelete, 0)=0 条件
            DataSet ds = con.ExecuteReader(@"select BuildName,BuildSNum from Tb_HSPR_Building where CommID=@CommID and isnull(IsDelete,0)=0 ORDER BY BuildName",
                new { CommID = Community.CommID }).ToDataSet();
            return JSONHelper.FromString(ds.Tables[0]);

        }

        /// <summary>
        /// 获取房间信息
        /// </summary>
        /// <param name="row"></param>
        /// 小区编号：CommID 必填
        /// 楼栋编号：BuildSNum 必填
        /// 返回：
        ///     房间ID：RoomID
        ///     小区ID:CommID
        ///     房间编码：RoomSign
        ///     单元编码：UnitSNum
        ///     业主名称：CustName
        ///     联系方式：MobilePhone
        /// <returns></returns>
        private string GetRoomLIst(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编码不能为空");
            }
            if (!row.Table.Columns.Contains("BuildSNum") || string.IsNullOrEmpty(row["BuildSNum"].ToString()))
            {
                return JSONHelper.FromString(false, "楼栋编码不能为空");
            }

            //查询小区
            Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(row["CommID"].ToString());
            //构造链接字符串
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }

            using (IDbConnection conn = new SqlConnection(GetConnectionStringStr(Community)))
            {
                string sql = "";

                // 敏捷特殊处理
                if (Community.CorpID == 1971)
                {
                    sql = @"select RoomID,isnull(isnull(NC_NewRoomSign,RoomName),RoomSign) AS RoomSign,UnitSNum from Tb_HSPR_Room  
                                            where isnull(IsDelete,0)=0 and RoomSign is not null and CommID=@CommID and BuildSNum=@BuildSNum
                                            ORDER BY RoomSign";
                }
                else
                {
                    sql = @"select RoomID,isnull(RoomName,RoomSign) AS RoomSign,UnitSNum from Tb_HSPR_Room  
                                            where isnull(IsDelete,0)=0 and RoomSign is not null and CommID=@CommID and BuildSNum=@BuildSNum
                                            ORDER BY RoomSign";
                }

                return new ApiResult(true, conn.Query(sql, new { CommID = Community.CommID, BuildSNum = row["BuildSNum"].ToString() })).toJson();
            }

        }
        /// <summary>
        /// 通过组团区域获取小区内的楼栋
        /// </summary>
        /// <param name="row"></param>
        /// 返回：
        ///     楼栋编号：BuildSNum
        ///     楼栋名称：BuildName
        /// <returns></returns>
        private string GetBuildListByRegion(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return JSONHelper.FromString(false, "请选择小区");
            }
            string CommunityId = row["CommunityId"].ToString();
            if (!row.Table.Columns.Contains("RegionID") || string.IsNullOrEmpty(row["RegionID"].ToString()))
            {
                return JSONHelper.FromString(false, "请选择区域");
            }
            string RegionID = row["RegionID"].ToString();

            //查询小区
            Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(CommunityId);
            //构造链接字符串
            if (null == Community)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }
            //构建链接字符串
            string connStr = new Business.CostInfo().GetConnectionStringStr(Community);

            string sql = "SELECT a.BuildID,a.BuildName,a.BuildSNum FROM Tb_HSPR_Building AS a LEFT JOIN Tb_HSPR_Region AS b ON a.CommID = b.CommID AND a.RegionSNum = b.RegionSNum AND b.IsDelete = 0  WHERE a.CommID = @CommID AND b.RegionID = @RegionID";
            DataTable dt = null;
            using (IDbConnection conn = new SqlConnection(connStr))
            {
                dt = conn.ExecuteReader(sql, new { CommID = Community.CommID, RegionID = RegionID }, null, null, CommandType.Text).ToDataSet().Tables[0];
            }
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            if (null != dt && dt.Rows.Count > 0)
            {
                Dictionary<string, object> dic;
                foreach (DataRow item in dt.Rows)
                {
                    dic = new Dictionary<string, object>
                    {
                        { "BuildID", item["BuildID"] },
                        { "BuildName", item["BuildName"] },
                        { "BuildSNum", item["BuildSNum"] }
                    };
                    list.Add(dic);
                }
            }
            return new ApiResult(true, list).toJson();

        }
        /// <summary>
        /// 获取小区配置
        /// </summary>
        /// <param name="CommunityId"></param>
        /// <returns></returns>
        private Tb_Community getCommunity(string CommunityId)
        {
            if (string.IsNullOrEmpty(CommunityId))
            {
                return null;
            }
            using (IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                return conn.QueryFirstOrDefault<Tb_Community>("SELECT * FROM Tb_Community WHERE Id = @Id OR CommID=@Id", new { Id = CommunityId });
            }
        }
    }
}
