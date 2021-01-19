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

namespace Business
{
    public class Visitor_th : PubInfo
    {
        public Visitor_th()
        {
            base.Token = "20171029Visitor";
        }

        public override void Operate(ref Transfer Trans)
        {
            Trans.Result = JSONHelper.FromString(false, "未知错误");

            DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
            DataRow Row = dAttributeTable.Rows[0];

            if ("GetVisitorInfo".Equals(Trans.Command) || "SetVisitorConfirm".Equals(Trans.Command))
            {
                //验证登录
                if (!new Login().isLogin(ref Trans)) return;
            }
            switch (Trans.Command)
            {
                case "GetVisitorRecord":             // 获取历史生成的访客二维码记录
                    Trans.Result = GetVisitorRecord(Row);
                    break;
                case "CreateVisitor":
                    Trans.Result = CreateVisitor(Row);
                    break;
                case "DeleteVisitor":
                    Trans.Result = DeleteVisitor(Row);
                    break;
                case "GetVisitorInfo":
                    Trans.Result = GetVisitorInfo(Row);
                    break;
                case "SetVisitorConfirm":
                    Trans.Result = SetVisitorConfirm(Row);
                    break;
                default:
                    break;
            }
        }

        private string SetVisitorConfirm(DataRow Row)
        {
            if (!Row.Table.Columns.Contains("VisitorId") || string.IsNullOrEmpty(Row["VisitorId"].ToString()))
            {
                return JSONHelper.FromString(false, "缺少VisitorId参数");
            }
            if (!Row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(Row["CommID"].AsString()))
            {
                return new ApiResult(false, "缺少参数CommID").toJson();
            }
            string commID = Row["CommID"].AsString();
            string VisitorId = Row["VisitorId"].ToString();
            string connStr;
            if (!GetDBServerPath(commID, out connStr))
            {
                return new ApiResult(false, "获取数据库信息失败").toJson();
            }

            int result = 0;

            DateTime ScanDate = DateTime.Now;

            using (IDbConnection conn = new SqlConnection(connStr))
            {
                result = conn.Execute("UPDATE Tb_HSPR_APP_Visitor SET ScanDate = @ScanDate WHERE Id = @Id", new { ScanDate = ScanDate, Id = VisitorId }, null, null, CommandType.Text);
            }

            if (result > 0)
            {
                return new ApiResult(true, ScanDate.ToString()).toJson();
            }
            return new ApiResult(false, "确认失败,请重试").toJson();
        }

        private string GetVisitorInfo(DataRow Row)
        {
            if (!Row.Table.Columns.Contains("VisitorId") || string.IsNullOrEmpty(Row["VisitorId"].ToString()))
            {
                return JSONHelper.FromString(false, "缺少VisitorId参数");
            }
            if (!Row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(Row["CommID"].AsString()))
            {
                return new ApiResult(false, "缺少参数CommID").toJson();
            }
            string commID = Row["CommID"].AsString();
            string VisitorId = Row["VisitorId"].ToString();
            string connStr;
            if (!GetDBServerPath(commID, out connStr))
            {
                return new ApiResult(false, "获取数据库信息失败").toJson();
            }

            using (IDbConnection conn = new SqlConnection(connStr))
            {
                dynamic dnc = conn.Query("SELECT * FROM Tb_HSPR_APP_Visitor WHERE Id = @Id", new { Id = VisitorId }, null, true, null, CommandType.Text).FirstOrDefault();
                if (null == dnc)
                {
                    return new ApiResult(false, "没有查询到该访客记录").toJson();
                }

                var dic = new Dictionary<string, object>
                {
                    { "Id", dnc.Id }
                    ,{ "CommID", dnc.CommID }
                    ,{ "UserID", dnc.UserID }
                    ,{ "RoomID", dnc.RoomID }
                    ,{ "RoomSign", dnc.RoomSign }
                    ,{ "CustID", dnc.CustID }
                    ,{ "CustName", dnc.CustName }
                    ,{ "Phone", dnc.Phone }
                    ,{ "VisitorName", dnc.VisitorName }
                    ,{ "VisitorSex", dnc.VisitorSex }
                    ,{ "StartDate", dnc.StartDate }
                    ,{ "EndDate", dnc.EndDate }
                    ,{ "ScanDate", dnc.ScanDate }
                    ,{ "IsDelete", dnc.IsDelete }
                    ,{ "CarSign", dnc.CarSign }
                };
                return new ApiResult(true, dic).toJson();
            }
        }


        /// <summary>
        /// 作废访客
        /// </summary>
        /// <param name="Row"></param>
        /// <returns></returns>
        private string DeleteVisitor(DataRow Row)
        {
            if (!Row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(Row["CommunityId"].ToString()))
            {
                return JSONHelper.FromString(false, "缺少CommunityId参数");
            }
            if (!Row.Table.Columns.Contains("VisitorId") || string.IsNullOrEmpty(Row["VisitorId"].ToString()))
            {
                return JSONHelper.FromString(false, "缺少VisitorId参数");
            }
            //项目小区表ID
            string CommunityId = Row["CommunityId"].ToString();

            //查询小区
            Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(CommunityId);
            //构造链接字符串
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }
            string VisitorId = Row["VisitorId"].ToString();
            string strcon = new Business.CostInfo().GetConnectionStringStr(Community);

            int result = 0;
            using (IDbConnection conn = new SqlConnection(strcon))
            {
                result = conn.Execute("UPDATE Tb_HSPR_APP_Visitor SET IsDelete = 1 WHERE Id = @Id", new { Id = VisitorId }, null, null, CommandType.Text);
            }

            if (result > 0)
            {
                return new ApiResult(true, "作废成功").toJson();
            }
            return new ApiResult(false, "作废失败,请重试").toJson();
        }


        /// <summary>
        /// 创建访客
        /// </summary>
        /// <param name="Row"></param>
        /// <returns></returns>
        private string CreateVisitor(DataRow Row)
        {
            if (!Row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(Row["CommunityId"].ToString()))
            {
                return JSONHelper.FromString(false, "缺少CommunityId参数");
            }
            if (!Row.Table.Columns.Contains("UserID") || string.IsNullOrEmpty(Row["UserID"].ToString()))
            {
                return JSONHelper.FromString(false, "缺少UserID参数");
            }
            if (!Row.Table.Columns.Contains("RelationID") || string.IsNullOrEmpty(Row["RelationID"].ToString()))
            {
                return JSONHelper.FromString(false, "缺少RelationID参数");
            }
            if (!Row.Table.Columns.Contains("VisitorName") || string.IsNullOrEmpty(Row["VisitorName"].ToString()))
            {
                return JSONHelper.FromString(false, "缺少VisitorName参数");
            }
            if (!Row.Table.Columns.Contains("VisitorSex") || string.IsNullOrEmpty(Row["VisitorSex"].ToString()))
            {
                return JSONHelper.FromString(false, "缺少VisitorSex参数");
            }
            if (!Row.Table.Columns.Contains("StartDate") || string.IsNullOrEmpty(Row["StartDate"].ToString()))
            {
                return JSONHelper.FromString(false, "缺少StartDate参数");
            }
            if (!Row.Table.Columns.Contains("EndDate") || string.IsNullOrEmpty(Row["EndDate"].ToString()))
            {
                return JSONHelper.FromString(false, "缺少EndDate参数");
            }

            //预设值邀请ID
            string Id = Guid.NewGuid().ToString();

            //项目小区表ID
            string CommunityId = Row["CommunityId"].ToString();

            //查询小区
            Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(CommunityId);
            //构造链接字符串
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }
            string CommID = Community.CommID;

            string UserID = Row["UserID"].ToString();
            string RelationID = Row["RelationID"].ToString();

            string VisitorName = Row["VisitorName"].ToString();
            string VisitorSex = Row["VisitorSex"].ToString();
            if (!"1".Equals(VisitorSex) && !"0".Equals(VisitorSex))
            {
                VisitorSex = "0";
            }
            DateTime StartDate = Convert.ToDateTime(Row["StartDate"].ToString());
            DateTime EndDate = Convert.ToDateTime(Row["EndDate"].ToString());
            if (EndDate <= StartDate)
            {
                return JSONHelper.FromString(false, "有效期结束时间必须大于开始时间");
            }
            string RoomID;
            string RoomSign;
            string CustID;
            string CustName;
            string Phone;

            var carSign = "";
            if (Row.Table.Columns.Contains("CarSign"))
            {
                carSign = Row["CarSign"].ToString();
            }

            using (IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                dynamic info = conn.Query("SELECT CustId,CustName,CustMobile,RoomId,RoomSign FROM Tb_User_Relation WHERE Id = @RelationID AND UserId = @UserId AND CommunityId = @CommunityId", new { RelationID = RelationID, UserId = UserID, CommunityId = CommunityId }, null, true, null, CommandType.Text).FirstOrDefault();
                if (null == info)
                {
                    return new ApiResult(false, "未查找到该房屋").toJson();
                }
                RoomID = info.RoomId;
                RoomSign = info.RoomSign;
                CustID = info.CustId;
                CustName = info.CustName;
                Phone = info.CustMobile;
            }

            string strcon = new Business.CostInfo().GetConnectionStringStr(Community);
            int result = 0;
            using (IDbConnection conn = new SqlConnection(strcon))
            {
                result = conn.Execute("INSERT INTO Tb_HSPR_APP_Visitor (Id,CommID,UserID,RoomID,RoomSign,CustID,CustName,Phone,VisitorName,VisitorSex,StartDate,EndDate) VALUES(@Id,@CommID,@UserID,@RoomID,@RoomSign,@CustID,@CustName,@Phone,@VisitorName,@VisitorSex,@StartDate,@EndDate)",
                    new { Id = Id, CommID = CommID, UserID = UserID, RoomID = RoomID, RoomSign = RoomSign, CustID = CustID, CustName = CustName, Phone = Phone, VisitorName = VisitorName, VisitorSex = VisitorSex, StartDate = StartDate, EndDate = EndDate }, null, null, CommandType.Text);

                if (!string.IsNullOrEmpty(carSign))
                {
                    conn.Execute($"UPDATE Tb_HSPR_APP_Visitor SET CarSign='{carSign}' WHERE Id='{Id}'");
                }
            }
            if (result > 0)
            {
                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    { "Id", Id }
                    ,{ "CommID", CommID }
                    ,{ "UserID", UserID }
                    ,{ "RoomID", RoomID }
                    ,{ "RoomSign", RoomSign }
                    ,{ "CustID", CustID }
                    ,{ "CustName", CustName }
                    ,{ "Phone", Phone }
                    ,{ "VisitorName", VisitorName }
                    ,{ "VisitorSex", VisitorSex }
                    ,{ "CarSign", carSign }
                    ,{ "StartDate", StartDate }
                    ,{ "EndDate", EndDate }
                    ,{ "ScanDate", null }
                    ,{ "IsDelete", 0 }
                };
                return new ApiResult(true, dic).toJson();
            }
            return new ApiResult(false, "生成邀请码失败,请重试").toJson();

        }


        /// <summary>
        /// 获取访客记录
        /// </summary>
        /// <param name="Row"></param>
        /// <returns></returns>
        private string GetVisitorRecord(DataRow Row)
        {
            if (!Row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(Row["CommunityId"].ToString()))
            {
                return JSONHelper.FromString(false, "缺少CommunityId参数");
            }

            if (!Row.Table.Columns.Contains("UserID") || string.IsNullOrEmpty(Row["UserID"].ToString()))
            {
                return JSONHelper.FromString(false, "缺少UserID参数");
            }
            if (!Row.Table.Columns.Contains("RoomID") || string.IsNullOrEmpty(Row["RoomID"].ToString()))
            {
                return JSONHelper.FromString(false, "缺少RoomID参数");
            }
            string CommunityId = Row["CommunityId"].ToString();
            string UserID = Row["UserID"].ToString();
            string RoomID = Row["RoomID"].ToString();

            //查询小区
            Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(CommunityId);
            //构造链接字符串
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }
            string strcon = new Business.CostInfo().GetConnectionStringStr(Community);

            DataTable dt;
            using (IDbConnection conn = new SqlConnection(strcon))
            {
                dt = conn.ExecuteReader(@"SELECT TOP 20 * FROM Tb_HSPR_APP_Visitor WHERE UserID = @UserID and CommID = @CommID 
                                            and RoomID = @RoomID ORDER BY EndDate DESC",
                    new { CommID = Community.CommID, UserID = UserID, RoomID = RoomID }).ToDataSet().Tables[0];
            }
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            if (null != dt && dt.Rows.Count > 0)
            {
                var hasCar = dt.Columns.Contains("CarSign");

                Dictionary<string, object> dic;
                foreach (DataRow item in dt.Rows)
                {
                    dic = new Dictionary<string, object>
                    {
                        { "Id", item["Id"] }
                        ,{ "CommID", item["CommID"] }
                        ,{ "UserID", item["UserID"] }
                        ,{ "RoomID", item["RoomID"] }
                        ,{ "RoomSign", item["RoomSign"] }
                        ,{ "CustID", item["CustID"] }
                        ,{ "CustName", item["CustName"] }
                        ,{ "Phone", item["Phone"] }
                        ,{ "VisitorName", item["VisitorName"] }
                        ,{ "VisitorSex", item["VisitorSex"] }
                        ,{ "StartDate", item["StartDate"] }
                        ,{ "EndDate", item["EndDate"] }
                        ,{ "ScanDate", item["ScanDate"] }
                        ,{ "IsDelete", item["IsDelete"] }
                    };

                    if (hasCar)
                    {
                        dic.Add("CarSign", item["CarSign"]);
                    }

                    list.Add(dic);

                }
            }
            return new ApiResult(true, list).toJson();
        }
    }
}
