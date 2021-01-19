using Dapper;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using MobileSoft.Model.Unified;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;

namespace Business
{
    public class CommunityManager_th : PubInfo
    {
        public CommunityManager_th() //获取小区、项目信息
        {
            base.Token = "20171028CommunityManager";
        }

        public override void Operate(ref Common.Transfer Trans)
        {
            Trans.Result = JSONHelper.FromString(false, "未知错误");

            DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
            DataRow Row = dAttributeTable.Rows[0];

            switch (Trans.Command)
            {
                case "GetCommunityInfo":                // 获取小区信息
                    Trans.Result = GetCommunityInfo(Row);
                    break;
                case "GetCommunityRedWYList":            // 获取红色物业
                    Trans.Result = GetCommunityRedWYList(Row);
                    break;
                case "GetCommunityQQTSList":            // 获取亲情提示
                    Trans.Result = GetCommunityQQTSList(Row);
                    break;
                case "GetCommunitySQZXList":            // 获取社区资讯
                    Trans.Result = GetCommunitySQZXList(Row);
                    break;
                case "GetCommunityServiceTel":          // 获取楼栋管家号码
                    Trans.Result = GetCommunityServiceTel(Row);
                    break;
                case "GetCommunityServiceTel_ZhongJi":          // 获取楼栋管家号码
                    Trans.Result = GetCommunityServiceTel_ZhongJi(Row);
                    break;
                case "GetCommunityServiceTel_Junfa":          // 获取楼栋管家号码
                    Trans.Result = GetCommunityServiceTel_Junfa(Row);
                    break;
                case "GetCommunityServices":            // 获取社区服务，高端定制
                    Trans.Result = GetCommunityServices(Row);
                    break;
                case "CommunityActivitiesList":         // 社区活动(跳蚤市场)
                    Trans.Result = CommunityActivitiesList(Row);
                    break;
                case "HasUnreadNotice":                 // 获取是否有未读物业公告
                    Trans.Result = HasUnreadNotice(Row);
                    break;
                case "LoadKeyDoor":                     // 读取小区门禁信息
                    Trans.Result = LoadKeyDoor(Row);
                    break;
                default:
                    break;
            }
        }
        #region 社区活动
        /// <summary>
        /// 社区活动
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public string CommunityActivitiesList(DataRow Row)
        {
            string result = "";

            #region 接受参数
            string strCommunityId = "";//项目ID
            string strCurrPage = "1";//第几页
            string strPageSize = "10";//分页的大小

            if (Row.Table.Columns.Contains("CommunityId"))
            {
                strCommunityId = AppGlobal.ChkStr(Row["CommunityId"].ToString());
            }
            else
            {
                return JSONHelper.FromString(false, "缺少参数CommID");
            }
            if (Row.Table.Columns.Contains("CurrPage"))
            {
                strCurrPage = AppGlobal.ChkNum(Row["CurrPage"].ToString());
            }

            if (Row.Table.Columns.Contains("PageSize"))
            {
                strPageSize = AppGlobal.ChkNum(Row["PageSize"].ToString());
            }


            #endregion

            #region 变量定义
            string strErrMsg = "";
            string strCommID = "";

            string SQLContionString = "";

            int PageCount = 0;
            int Counts = 0;
            StringBuilder sListContent = new StringBuilder("");

            int iCurrPage = AppGlobal.StrToInt(strCurrPage);
            int iPageSize = AppGlobal.StrToInt(strPageSize);


            #endregion
            SQLContionString = ConnectionDb.GetConnection(Row["CommunityId"].ToString());
            MobileSoft.Model.Unified.Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(Row["CommunityId"].ToString());

            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }

            #region 查询社区活动
            string strSQLCommAct = "and ActivitiesType<>'0001' and isnull(IsDelete, 0)=0 And isnull(IsRun, 0)=1 AND CommID = " + Community.CommID;

            DataTable dTableCommAct = null;
            dTableCommAct = (new Business.TWBusinRule(SQLContionString)).HSPR_CommActivities_CutPage(out PageCount, out Counts, strSQLCommAct, iCurrPage, iPageSize);


            if (dTableCommAct.Rows.Count > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("InfoID", typeof(string)));
                dt.Columns.Add(new DataColumn("Heading", typeof(string)));
                dt.Columns.Add(new DataColumn("IssueDate", typeof(string)));
                dt.Columns.Add(new DataColumn("ImageUrl", typeof(string)));

                foreach (DataRow DRow in dTableCommAct.Rows)
                {
                    DataRow dr = dt.NewRow();
                    dr["InfoID"] = DRow["ActivitiesID"].ToString();
                    dr["Heading"] = DRow["ActivitiesTheme"].ToString();
                    dr["IssueDate"] = DRow["IssueDate"].ToString();
                    dr["ImageUrl"] = string.IsNullOrEmpty(DRow["ActivitiesImages"].ToString()) ? "" : DRow["ActivitiesImages"].ToString().IndexOf("http") >= 0 ? DRow["ActivitiesImages"].ToString() : DRow["ActivitiesImages"].ToString();

                    dt.Rows.Add(dr);
                }
                result = JSONHelper.FromString(dt);
            }
            else
            {
                result = JSONHelper.FromString(dTableCommAct);
            }
            #endregion
            dTableCommAct.Dispose();
            return result;
        }
        #endregion
        /// <summary>
        /// 获取小区信息
        /// </summary>
        private string GetCommunityInfo(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }

            string communityId = row["CommunityId"].AsString();

            //查询小区
            Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(communityId);
            //构造链接字符串
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }
            return new ApiResult(true, new
            {
                Id = communityId,
                CorpID = Community.CorpID,
                CorpName = Community.CorpName,
                CommID = Community.CommID,
                CommName = Community.CommName,
                Province = Community.Province,
                Area = Community.Area,
                City = Community.City,
                Tel = Community.Tel,
                ModuleRights = Community.ModuleRights,
                IsMultiDoorControlServer = Community.IsMultiDoorControlServer
            }).toJson();
        }

        /// <summary>
        /// 获取社区公告
        /// </summary>
        private string GetCommunityQQTSList(DataRow row)
        {
            return GetComunityNewsList(row, "qqts");
        }

        /// <summary>
        /// 获取社区资讯
        /// </summary>
        private string GetCommunitySQZXList(DataRow row)
        {
            return GetComunityNewsList(row, "dtzx");
        }

        private string GetCommunityRedWYList(DataRow row)
        {
            return GetComunityNewsList(row, "sqwh");
        }

        /// <summary>
        /// 获取社区信息，亲情提示、社区资讯等。
        /// </summary>
        private string GetComunityNewsList(DataRow row, string infoType)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }

            string communityId = row["CommunityId"].AsString();

            //查询小区
            Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(communityId);
            //构造链接字符串
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }
            string strcon = new Business.CostInfo().GetConnectionStringStr(Community);

            int pageSize = 10;
            int pageIndex = 1;
            if (row.Table.Columns.Contains("PageSize") && !string.IsNullOrEmpty(row["PageSize"].ToString()))
            {
                pageSize = AppGlobal.StrToInt(row["PageSize"].ToString());
            }
            if (row.Table.Columns.Contains("PageIndex") && !string.IsNullOrEmpty(row["PageIndex"].ToString()))
            {
                pageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());
            }

            using (IDbConnection conn = new SqlConnection(strcon))
            {
                string sql = string.Format(@"SELECT InfoID,Heading,IssueDate,ImageUrl FROM view_HSPR_CommunityInfo_Filter 
                                                WHERE isnull(IsDelete,0)=0 AND isnull(IsAudit, 0)=0 
                                AND (CommID={0} OR CommID=0) AND InfoType='{1}' AND (ShowEndDate IS NULL OR ShowEndDate >= getdate())", Community.CommID, infoType);

                if (infoType == "sqwh")
                {
                    int intType = 0;
                    string strType = "";
                    if (row.Table.Columns.Contains("RedType") && !string.IsNullOrEmpty(row["RedType"].ToString()))
                    {
                        intType = AppGlobal.StrToInt(row["RedType"].ToString());
                    }

                    switch (intType)
                    {
                        case 1:
                            strType = ";党建团队";
                            break;
                        case 2:
                            strType = ";党建报道";
                            break;
                        case 3:
                            strType = ";党建百科";
                            break;
                        case 4:
                            strType = ";志愿之星";
                            break;
                        case 5:
                            strType = ";志愿组织";
                            break;
                        case 6:
                            strType = ";志愿活动";
                            break;
                    }

                    if (string.IsNullOrEmpty(strType))
                    {
                        sql = string.Format(@"SELECT InfoID,Heading,IssueDate,ImageUrl FROM view_HSPR_CommunityInfo_Filter 
                                                WHERE isnull(IsDelete,0)=0 AND isnull(IsAudit, 0)=0 
                                AND (CommID={0} OR CommID=0) AND InfoType='{1}' AND (ShowEndDate IS NULL OR ShowEndDate >= getdate()) and (Heading like '%{2}' or Heading like '%{3}' or Heading like '%{4}' or Heading like '%{5}' or Heading like '%{6}' or Heading like '%{7}')", Community.CommID, infoType, ";党建团队", ";党建报道", ";党建百科", ";志愿之星", ";志愿组织", ";志愿活动");
                    }
                    else
                    {
                        sql = string.Format(@"SELECT InfoID,Heading,IssueDate,ImageUrl FROM view_HSPR_CommunityInfo_Filter 
                                                WHERE isnull(IsDelete,0)=0 AND isnull(IsAudit, 0)=0 
                                AND (CommID={0} OR CommID=0) AND InfoType='{1}' AND (ShowEndDate IS NULL OR ShowEndDate >= getdate()) and Heading like '%{2}'", Community.CommID, infoType, strType);
                    }
                }

                DataSet ds = GetList(out int pageCount, out int count, sql, pageIndex, pageSize, "IssueDate", 1, "InfoID", strcon);

                DataTable dt = ds.Tables[0];
                if (infoType == "sqwh")
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataColumn dc1 = new DataColumn("redType", typeof(string));
                        dt.Columns.Add(dc1);

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string headType = "";
                            string headName = dt.Rows[i]["Heading"].ToString();
                            string[] arrayInfo = headName.Split(';');
                            headName = arrayInfo[0];
                            if (arrayInfo.Length >= 2)
                            {
                                switch (arrayInfo[1])
                                {
                                    case "党建团队":
                                        headType = "1";
                                        break;
                                    case "党建报道":
                                        headType = "2";
                                        break;
                                    case "党建百科":
                                        headType = "3";
                                        break;
                                    case "志愿之星":
                                        headType = "4";
                                        break;
                                    case "志愿组织":
                                        headType = "5";
                                        break;
                                    case "志愿活动":
                                        headType = "6";
                                        break;
                                }
                            }

                            dt.Rows[i]["Heading"] = headName;
                            dt.Rows[i]["redType"] = headType;
                        }
                    }
                }

                string result = new ApiResult(true, dt).toJson();
                return result.Insert(result.Length - 1, ",\"PageCount\":" + pageCount);
            }
        }

        /// <summary>
        /// 获取楼栋管家号码
        /// </summary>
        private string GetCommunityServiceTel(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }


            string communityId = row["CommunityId"].AsString();

            //查询小区
            Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(communityId);
            //构造链接字符串
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }

            if (Community.CorpID == 1985)
            {
                if (!row.Table.Columns.Contains("RoomID") || string.IsNullOrEmpty(row["RoomID"].ToString()))
                {
                    return JSONHelper.FromString(false, "房屋编号不能为空");
                }
            }


            string roomId = null;
            if (row.Table.Columns.Contains("RoomID") && !string.IsNullOrEmpty(row["RoomID"].ToString()))
            {
                roomId = row["RoomID"].ToString();
            }

            // 没有房号，读取物业中心电话
            if (string.IsNullOrEmpty(roomId) || roomId == "0")
            {
                using (IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString))
                {
                    string sql = @"SELECT isnull(Tel,'') AS ServiceTel FROM Tb_Community WHERE Id=@Id";

                    string serviceTel = conn.Query<string>(sql, new { Id = row["CommunityId"].ToString() }).FirstOrDefault();


                    if (string.IsNullOrEmpty(serviceTel))
                    {
                        return JSONHelper.FromString(false, "未设置物业中心电话");
                    }
                    return JSONHelper.FromString(true, serviceTel);
                }
            }
            else
            {


                string strcon = new Business.CostInfo().GetConnectionStringStr(Community);

                // 有房号，从erp中读取楼栋管家电话
                using (IDbConnection conn = new SqlConnection(strcon))
                {
                    string sql = "";

                    // 金辉版本，楼栋管家设置的是岗位，俊发、海亮、嘉和
                    if (Community.CorpID == 2021 || Community.CorpID == 2045 || Community.CorpID == 2046)
                    {
                        sql = @"SELECT UserName,MobileTel FROM Tb_Sys_User WHERE isnull(IsDelete,0)=0 AND IsMobile=1 AND isnull(MobileTel,'')<>'' 
                                  AND UserCode IN (SELECT DISTINCT UserCode FROM Tb_Sys_UserRole WHERE RoleCode IN (SELECT RoleCode FROM Tb_HSPR_BuildHousekeeper WHERE CommID=@CommID AND BuildSNum IN (SELECT BuildSNum FROM Tb_HSPR_Room WHERE CommID=@CommID AND RoomID=@RoomID AND isnull(IsDelete,0)=0)))";
                    }
                    else
                    {
                        if (Community.CorpID == 1985)//需求4773 新增管家字段
                        {
                            sql = @"SELECT ISNULL(HouseKeeperAlias,'') AS UserName,ISNULL(HousekeeperTel,'') AS MobileTel
                                        FROM Tb_HSPR_Building WHERE BuildSNum = 
                                        (SELECT BuildSNum FROM Tb_HSPR_Room WHERE RoomID =@RoomID )AND CommID =@CommID";
                        }
                        else
                        {

                            sql = @"SELECT UserName,MobileTel FROM Tb_Sys_User 
                        WHERE isnull(IsDelete,0)=0 AND isnull(MobileTel,'')<>'' AND UserCode IN (
                        SELECT UserCode FROM Tb_HSPR_BuildHousekeeper WHERE CommID=@CommID AND BuildSNum=(
                            SELECT BuildSNum FROM Tb_HSPR_Room WHERE RoomID=@RoomID))";
                            if (Community.CorpID == 2087)
                            {
                                sql = @"SELECT UserName,MobileTel FROM Tb_Sys_User 
                        WHERE isnull(IsDelete,0)=0 AND isnull(MobileTel,'')<>'' AND UserCode IN (
						SELECT UserCode FROM Tb_Sys_UserRole WHERE RoleCode IN(
                        SELECT RoleCode FROM Tb_HSPR_BuildHousekeeper WHERE CommID=@CommID AND BuildSNum=(
                            SELECT BuildSNum FROM Tb_HSPR_Room WHERE RoomID=@RoomID)))";
                            }
                        }
                    }

                    DataTable dt = conn.ExecuteReader(sql, new { CommID = Community.CommID, RoomID = roomId }).ToDataSet().Tables[0];
                    if (dt.Rows.Count <= 0)
                    {
                        if (Community.CorpID == 1953)//中集
                        {
                            var list = new List<object>();
                            list.Add(new { UserName = "中集管家", MobileTel = "02861629888" });
                            return new ApiResult(true, list).toJson();
                        }
                    }
                    return JSONHelper.FromString(dt);
                }
            }
        }


        /// <summary>
        /// 获取楼栋管家号码
        /// </summary>
        private string GetCommunityServiceTel_Junfa(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }

            if (!row.Table.Columns.Contains("RoomID") || string.IsNullOrEmpty(row["RoomID"].ToString()))
            {
                return JSONHelper.FromString(false, "房屋编号不能为空");
            }

            string roomId = row["RoomID"].ToString();
            string communityId = row["CommunityId"].ToString();

            //查询小区
            Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(communityId);
            //构造链接字符串
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }

            string strcon = new Business.CostInfo().GetConnectionStringStr(Community);

            using (IDbConnection conn = new SqlConnection(strcon))
            {
                string sql = @"SELECT HousekeeperName,HousekeeperTel 
                                FROM Tb_HSPR_Building WHERE BuildSNum = 
                                (SELECT BuildSNum FROM Tb_HSPR_Room WHERE RoomID =@RoomID )AND CommID =@CommID";

                DataTable dt = conn.ExecuteReader(sql, new { CommID = Community.CommID, RoomID = roomId }).ToDataSet().Tables[0];

                return JSONHelper.FromString(dt);

            }
        }


        private string GetCommunityServiceTel_ZhongJi(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }

            if (!row.Table.Columns.Contains("RoomID") || string.IsNullOrEmpty(row["RoomID"].ToString()))
            {
                return JSONHelper.FromString(false, "房屋编号不能为空");
            }

            string roomId = row["RoomID"].ToString();
            string communityId = row["CommunityId"].ToString();

            //查询小区
            Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(communityId);
            //构造链接字符串
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }

            string strcon = new Business.CostInfo().GetConnectionStringStr(Community);

            using (IDbConnection conn = new SqlConnection(strcon))
            {
                string sql = @"SELECT Heading AS UserName,InfoContent AS MobileTel FROM view_Tb_HSPR_CommunityInfo_filter_CommServiceTel WHERE CommID = @CommID";

                DataTable dt = conn.ExecuteReader(sql, new { CommID = Community.CommID }).ToDataSet().Tables[0];

                return JSONHelper.FromString(dt);

            }
        }

        /// <summary>
        /// 泰禾高端定制
        /// </summary>
        private string GetCommunityServices(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }

            string communityId = row["CommunityId"].AsString();

            //查询小区
            Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(communityId);
            //构造链接字符串
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }
            string strcon = new Business.CostInfo().GetConnectionStringStr(Community);

            using (IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                DataTable dataTable = conn.ExecuteReader(string.Format(@"SELECT TOP 9 Sort,CustomizedType,CustomizedImage,CarouselDescribe,CarouselImage,ImgLink,Phone FROM Tb_CommunityService 
                    WHERE isnull(IsDelete,0)=0 AND CommunityId LIKE '%{0}%'", communityId)).ToDataSet().Tables[0];
                return JSONHelper.FromString(dataTable);
            }
        }

        /// <summary>
        /// 读取是否有未读物业公告
        /// </summary>
        private string HasUnreadNotice(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            if (!row.Table.Columns.Contains("UserId") || string.IsNullOrEmpty(row["UserId"].ToString()))
            {
                return JSONHelper.FromString(false, "用户编号不能为空");
            }

            string communityId = row["CommunityId"].AsString();
            string userId = row["UserId"].AsString();

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
                string sql = @"SELECT  InfoID FROM Tb_HSPR_CommunityInfo WHERE CommID=@CommID AND
                                (InfoType='qqts' OR InfoType='dtzx') AND (ShowEndDate IS NULL OR ShowEndDate>GETDATE())
                                and isnull(IsAudit, 0)=0 and isnull(IsDelete,0)=0
                                ORDER BY IssueDate DESC";
                IEnumerable<long> infoSet = conn.Query<long>(sql, new { CommID = Community.CommID });

                if (infoSet.Count() > 0)
                {
                    using (IDbConnection conn2 = new SqlConnection(PubConstant.UnifiedContionString))
                    {
                        int count = conn2.Query<int>($"SELECT count(0) AS COUNT FROM Tb_BBS_ReadRecord WHERE InfoID IN('{string.Join("','", infoSet)}') AND UserID=@UserID",
                            new
                            {
                                UserID = userId
                            }).FirstOrDefault();

                        return JSONHelper.FromString(true, count < infoSet.Count() ? "1" : "0");

                    }
                }
                return JSONHelper.FromString(true, "0");
            }
        }

        /// <summary>
        /// 读取门禁钥匙
        /// </summary>
        private string LoadKeyDoor(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            if (!row.Table.Columns.Contains("UserId") || string.IsNullOrEmpty(row["UserId"].ToString()))
            {
                return JSONHelper.FromString(false, "用户编号不能为空");
            }

            string communityId = row["CommunityId"].AsString();
            string userId = row["UserId"].AsString();

            //查询小区
            Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(communityId);
            //构造链接字符串
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }

            if (string.IsNullOrEmpty(Community.ModuleRights) || Community.ModuleRights == "否")
            {
                return JSONHelper.FromString(false, "该小区暂未开通门禁功能");
            }

            string strcon = GetConnectionStringStr(Community);
            string mobile = null;

            // 读取用户手机号
            using (IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                mobile = conn.Query(@"SELECT Mobile FROM Tb_User WHERE Id=@UserID", new { UserID = userId }).FirstOrDefault();
            }

            string productKey = "0f4a878bc9f1d8e900dad0ccba569225";
            string productSecret = "21734b3545b48672991305f64fd2e56abd612b6b";
            string baseUrl = "https://api-iot.tslsmart.com/";
            string token = AppGlobal.getMd5Hash(productKey + productSecret).ToLower();

            string parameter = string.Format("mobile={0}&token={1}{2}", mobile, token, productKey).ToLower();
            string signData = AppGlobal.getMd5Hash(parameter).ToLower();

            string accessTokenUrl = string.Format(@"{0}OpenApi/User/Login?{1}&SignData={2}&ProductKey={3}", baseUrl, parameter, signData, productKey);

            string data = SendHttp(accessTokenUrl, "GET", null);
            dynamic obj = JsonConvert.DeserializeObject<dynamic>(data);

            if (obj.ErrorCode != 0)
            {
                return JSONHelper.FromString(false, "您暂未有门禁授权");
            }

            // 当前用户的授权AccessToken
            string accessToken = obj.Data.AccessToken;

            string userAuthUrl = string.Format(@"{0}/OpenApi/Authorization/GetUserAuth?AccessToken={1}", baseUrl, accessToken);

            data = SendHttp(accessTokenUrl, "GET", null);
            obj = JsonConvert.DeserializeObject<dynamic>(data);

            if (obj.ErrorCode != 0 || ((Array)obj.Data).Length == 0)
            {
                return JSONHelper.FromString(false, "您暂未有门禁授权");
            }

            return new ApiResult(true, obj.Data).toJson();
        }
    }
}
