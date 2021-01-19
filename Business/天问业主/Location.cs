using System;
using MobileSoft.DBUtility;
using MobileSoft.Common;
using System.Data;
using System.Text;
using System.Xml;
using System.Net;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using MobileSoft.Model.Unified;
using System.Data.SqlClient;
using Dapper;
using DapperExtensions;
using System.Linq;

namespace Business
{
    /// <summary>
    /// 获取城市小区信息
    /// </summary>
    public class Location : PubInfo
    {
        public Location() //获取小区、项目信息
        {
            base.Token = "20160803Location";
        }
        public override void Operate(ref Common.Transfer Trans)
        {
            Trans.Result = JSONHelper.FromString(false, "未知错误!");
            DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
            DataRow Row = dAttributeTable.Rows[0];
            switch (Trans.Command)
            {
                case "GetCityList":
                    Trans.Result = GetCityList(Row);//城市列表
                    break;
                case "GetProvinceList":
                    Trans.Result = GetProvinceList(Row);//省列表
                    break;
                case "GetCommList":
                    Trans.Result = GetCommList(Row);//小区列表
                    break;
                case "GetCityList_new":
                    Trans.Result = GetCityList_new(Row);
                    break;
                case "GetCommList_new":
                    Trans.Result = GetCommList_new(Row);
                    break;
                case "GetCommListforH5"://小区列表H5
                    Trans.Result = GetCommListforH5(Row);
                    break;
                    //获取定位周边1KM内最近的一个小区,没有返回空
                case "GetCommListNearBy":
                    Trans.Result = GetCommListNearBy(Row);
                    break;
                    // 获取组团区域
                case "GetRegionList":
                    Trans.Result = GetRegionList(Row);
                    break;
                case "GetServicePersonList":
                    Trans.Result = GetServicePersonList(Row);
                    break;
                case "GetServicePersonDetail": // 获取人员信息
                    Trans.Result = GetServicePersonDetail(Row);
                    break;
                case "GetServicePersonEvalInfo":// 获取评价信息
                    Trans.Result = GetServicePersonEvalInfo(Row);
                    break;
                case "SetServicePersonEval":// 评价
                    Trans.Result = SetServicePersonEval(Row);
                    break;
                default:
                    break;
            }
        }
        private string SetServicePersonEval(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return new ApiResult(false, "请选择小区").toJson();
            }
            string communityId = row["CommunityId"].ToString();

            if (!row.Table.Columns.Contains("RoomID") || string.IsNullOrEmpty(row["RoomID"].ToString()))
            {
                return new ApiResult(false, "请选择房屋").toJson();
            }
            string RoomID = row["RoomID"].ToString();


            if (!row.Table.Columns.Contains("PersonID") || string.IsNullOrEmpty(row["PersonID"].ToString()))
            {
                return new ApiResult(false, "请选择评价人员").toJson();
            }
            string personID = row["PersonID"].ToString();

            if (!row.Table.Columns.Contains("EevalLevel") || string.IsNullOrEmpty(row["EevalLevel"].ToString()))
            {
                return new ApiResult(false, "请评价").toJson();
            }
            int EvalLevel;
            if (!int.TryParse(row["EevalLevel"].ToString(), out EvalLevel)) {
                return new ApiResult(false, "请评价(2)").toJson();
            }

            if (0 != EvalLevel && 1 != EvalLevel && 2 != EvalLevel) {
                return new ApiResult(false, "请评价(3)").toJson();
            }
            if (!row.Table.Columns.Contains("EvalContent"))
            {
                return new ApiResult(false, "缺少参数EvalContent").toJson();
            }
            string EvalContent = row["EvalContent"].ToString();
            if (2 == EvalLevel) {
                if (string.IsNullOrEmpty(EvalContent))
                {
                    return new ApiResult(false, "请输入意见反馈").toJson();
                }
            }
            Tb_Community tb_Community = GetCommunity(communityId);
            if (null == tb_Community)
            {
                return new ApiResult(false, "该小区不存在").toJson();
            }
            string connStr = GetConnectionStringStr(tb_Community);
            // 先获取评价计算周期
            // 0为按月,1为按季度
            int evalType = 0;
            using (IDbConnection conn = new SqlConnection(connStr))
            {
                dynamic evalSet = conn.QueryFirstOrDefault<dynamic>("SELECT * FROM Tb_HSPR_ServicePerson_Eval_Set");
                if (null == evalSet)
                {
                    return new ApiResult(false, "未配置评价周期").toJson();
                }
                evalType = evalSet.EvalType;
            }

            // 查询出最新的一条评价,判断是否评价过
            dynamic eval;
            using (IDbConnection conn = new SqlConnection(connStr))
            {
                eval = conn.QueryFirstOrDefault<dynamic>("SELECT * FROM Tb_HSPR_ServicePerson_Eval WHERE RoomID = @RoomID AND PersonID = @PersonID ORDER BY EvalDate DESC", new { RoomID = RoomID, PersonID = personID });
            }
            if (null != eval)
            {
                DateTime evalDate = eval.EvalDate;
                DateTime dateTime = DateTime.Now;
                #region 如果统计周期是每月
                if (0 == evalType)
                {
                    //先判断年份
                    if (dateTime.Year == evalDate.Year && dateTime.Month != evalDate.Month)
                    {
                        return new ApiResult(false, "该评价周期内已经评价过,无法再次评价").toJson();
                    }
                }
                #endregion

                #region 如果统计周期是每季
                if (1 == evalType)
                {
                    //先判断年份
                    if (dateTime.Year == evalDate.Year && GetQuarter(dateTime.Month) == GetQuarter(evalDate.Month))
                    {
                        return new ApiResult(false, "该评价周期内已经评价过,无法再次评价").toJson();
                    }
                }
                #endregion
            }
            int result = 0;
            using (IDbConnection conn = new SqlConnection(connStr)) {
                result = conn.Execute("INSERT INTO Tb_HSPR_ServicePerson_Eval(Id, PersonID, EvalLevel, EvalContent, EvalDate, RoomID) VALUES (NEWID(), @PersonID, @EvalLevel, @EvalContent, GETDATE(), @RoomID);", new { PersonID = personID, EvalLevel = EvalLevel, EvalContent = EvalContent, RoomID = RoomID });
            }
            if (result > 0)
            {
                return new ApiResult(true, "评价成功").toJson();
            }
            else {
                return new ApiResult(false, "评价失败").toJson();
            }
        }

        /// <summary>
        /// 获取本次评价周期的评价信息
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string GetServicePersonEvalInfo(DataRow row) {

            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return new ApiResult(false, "请选择小区").toJson();
            }
            string communityId = row["CommunityId"].ToString();

            if (!row.Table.Columns.Contains("RoomID") || string.IsNullOrEmpty(row["RoomID"].ToString()))
            {
                return new ApiResult(false, "请选择房屋").toJson();
            }
            string RoomID = row["RoomID"].ToString();

            if (!row.Table.Columns.Contains("PersonID") || string.IsNullOrEmpty(row["PersonID"].ToString()))
            {
                return new ApiResult(false, "请选择评价人员").toJson();
            }
            string personID = row["PersonID"].ToString();
            Tb_Community tb_Community = GetCommunity(communityId);
            if (null == tb_Community)
            {
                return new ApiResult(false, "该小区不存在").toJson();
            }
            string connStr = GetConnectionStringStr(tb_Community);

            // 先获取评价计算周期
            // 0为按月,1为按季度
            int evalType = 0;
            using (IDbConnection conn = new SqlConnection(connStr))
            {
                dynamic evalSet = conn.QueryFirstOrDefault<dynamic>("SELECT * FROM Tb_HSPR_ServicePerson_Eval_Set");
                if (null == evalSet)
                {
                    return new ApiResult(false, "未配置评价周期").toJson();
                }
                evalType = evalSet.EvalType;
            }

            Dictionary<string, object> resultDic = new Dictionary<string, object>();

            // 查询出最新的一条评价
            dynamic eval;
            using (IDbConnection conn = new SqlConnection(connStr))
            {
                eval = conn.QueryFirstOrDefault<dynamic>("SELECT * FROM Tb_HSPR_ServicePerson_Eval WHERE RoomID = @RoomID AND PersonID = @PersonID ORDER BY EvalDate DESC", new { RoomID = RoomID, PersonID = personID });
            }
            if (null == eval)
            {
                resultDic.Add("EvalInfo", null);
                resultDic.Add("EvalStatis", null);
                return new ApiResult(true, resultDic).toJson();
            }
            DateTime evalDate = eval.EvalDate;
            DateTime dateTime = DateTime.Now;

            #region 如果统计周期是每月
            if (0 == evalType)
            {
                //先判断年份
                if (dateTime.Year != evalDate.Year)
                {
                    resultDic.Add("EvalInfo", null);
                    resultDic.Add("EvalStatis", null);
                    return new ApiResult(true, resultDic).toJson();
                }
                //再判断月份是否一致
                if (dateTime.Month != evalDate.Month)
                {
                    resultDic.Add("EvalInfo", null);
                    resultDic.Add("EvalStatis", null);
                    return new ApiResult(true, resultDic).toJson();
                }
            }
            #endregion

            #region 如果统计周期是每季
            if (1 == evalType)
            {
                //先判断年份
                if (dateTime.Year != evalDate.Year)
                {
                    resultDic.Add("EvalInfo", null);
                    resultDic.Add("EvalStatis", null);
                    return new ApiResult(true, resultDic).toJson();
                }
                if (GetQuarter(dateTime.Month) != GetQuarter(evalDate.Month))
                {
                    resultDic.Add("EvalInfo", null);
                    resultDic.Add("EvalStatis", null);
                    return new ApiResult(true, resultDic).toJson();
                }
            }
            #endregion

            // 如果已经评价过,就将评价信息填充到返回里边
            Dictionary<string, object> evalDic = new Dictionary<string, object>();
            evalDic.Add("Id", eval.Id);
            evalDic.Add("PersonID", eval.PersonID);
            evalDic.Add("EvalLevel", eval.EvalLevel);
            evalDic.Add("EvalContent", eval.EvalContent);
            evalDic.Add("EvalDate", eval.EvalDate);
            evalDic.Add("RoomID", eval.RoomID);
            resultDic.Add("EvalInfo", evalDic);


            Dictionary<string, object> evalStatisDic = new Dictionary<string, object>();

            DataTable evalDataTable = null;
            // 如果是月
            if (0 == evalType)
            {
                using (IDbConnection conn = new SqlConnection(connStr))
                {
                    evalDataTable = conn.ExecuteReader("SELECT * FROM Tb_HSPR_ServicePerson_Eval WHERE RoomID = @RoomID AND PersonID = @PersonID AND DATEPART(Month, EvalDate) = DATEPART(Month, GETDATE()) AND DATEPART(Year, EvalDate) = DATEPART(Year, GETDATE())", new { RoomID = RoomID, PersonID = personID }).ToDataSet().Tables[0];
                }
            }
            // 如果是按季度
            if (1 == evalType) {
                using (IDbConnection conn = new SqlConnection(connStr))
                {
                    evalDataTable = conn.ExecuteReader("SELECT * FROM Tb_HSPR_ServicePerson_Eval WHERE RoomID = @RoomID AND PersonID = @PersonID AND DATEPART(Quarter, EvalDate) = DATEPART(Quarter, GETDATE()) AND DATEPART(Year, EvalDate) = DATEPART(Year, GETDATE())", new { RoomID = RoomID, PersonID = personID }).ToDataSet().Tables[0];
                }
            }
            if (null != evalDataTable)
            {
                // 总量
                evalStatisDic.Add("Count", evalDataTable.Rows.Count);
                // 非常满意
                evalStatisDic.Add("Good", evalDataTable.Select("EvalLevel = 0").Length);
                // 满意
                evalStatisDic.Add("Nomal", evalDataTable.Select("EvalLevel = 1").Length);
                // 不满意
                evalStatisDic.Add("Bad", evalDataTable.Select("EvalLevel = 2").Length);
            }
            else
            {
                evalStatisDic.Add("Count", 0);
                evalStatisDic.Add("Good", 0);
                evalStatisDic.Add("Nomal", 0);
                evalStatisDic.Add("Bad", 0);
            }
            resultDic.Add("EvalStatis", evalStatisDic);
            return new ApiResult(true, resultDic).toJson();
        }

        /// <summary>
        /// 获取人员详情
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string GetServicePersonDetail(DataRow row) {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return new ApiResult(false, "请选择小区").toJson();
            }
            string communityId = row["CommunityId"].ToString();


            if (!row.Table.Columns.Contains("PersonID") || string.IsNullOrEmpty(row["PersonID"].ToString()))
            {
                return new ApiResult(false, "请选择评价人员").toJson();
            }
            string personID = row["PersonID"].ToString();

            Tb_Community tb_Community = GetCommunity(communityId);
            if (null == tb_Community)
            {
                return new ApiResult(false, "该小区不存在").toJson();
            }
            string connStr = GetConnectionStringStr(tb_Community);

            Dictionary<string, object> personDic = new Dictionary<string, object>();
            // 先获取人员基本信息
            using (IDbConnection conn = new SqlConnection(connStr))
            {
               
                DataTable dt = conn.ExecuteReader("SELECT  * FROM view_HSPR_ServPerson_Filter WHERE CommID = @CommID AND PersonID = @PersonID", new { CommID = tb_Community.CommID, PersonID = personID }, null, null, CommandType.Text).ToDataSet().Tables[0];
                if (null == dt || dt.Rows.Count == 0) {
                    return new ApiResult(false, "该人员不存在").toJson();
                }
                DataRow personRow = dt.Rows[0];
                foreach (DataColumn column in dt.Columns)
                {
                    personDic.Add(column.ColumnName, personRow[column.ColumnName]);
                }
            }
            return new ApiResult(true, personDic).toJson();
        }

        /// <summary>
        /// 根据月份获取季度
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        private int GetQuarter(int month) {
            //return (int)Math.Ceiling((decimal)(month / 3));
            switch (month) {
                case 1:
                case 2:
                case 3:
                    return 1;
                case 4:
                case 5:
                case 6:
                    return 2;
                case 7:
                case 8:
                case 9:
                    return 3;
                case 10:
                case 11:
                case 12:
                    return 4;
                default:
                    return 0;
            }
        }
        private string GetServicePersonList(DataRow row) {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString())) {
                return new ApiResult(false, "请选择小区").toJson();
            }
            string communityId = row["CommunityId"].ToString();
            Tb_Community tb_Community = GetCommunity(communityId);
            if (null == tb_Community)
            {
                return new ApiResult(false, "该小区不存在").toJson();
            }
            string connStr = GetConnectionStringStr(tb_Community);

            DataTable dt;
            using (IDbConnection conn = new SqlConnection(connStr))
            {
                dt = conn.ExecuteReader("SELECT  * FROM view_HSPR_ServPerson_Filter WHERE CommID = @CommID", new { CommID = tb_Community.CommID }, null, null, CommandType.Text).ToDataSet().Tables[0];
            }
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            if (null != dt) {
                Dictionary<string, object> dictionary;
                foreach (DataRow item in dt.Rows)
                {
                    dictionary = new Dictionary<string, object>();
                    foreach (DataColumn column in dt.Columns)
                    {
                        dictionary.Add(column.ColumnName, item[column.ColumnName]);
                    }
                    list.Add(dictionary);
                }
            }
            return new ApiResult(true, list).toJson();
        } 
        /// <summary>
        /// 根据CommunityID获取CommID再获取组团区域
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string GetRegionList(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return new ApiResult(false, "缺少CommunityId").toJson();
            }
            string CommunityId = row["CommunityId"].ToString();

            Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(CommunityId);
            if (null == Community) {
                return new ApiResult(false, "没有找到对应的小区信息").toJson();
            }
            string connStr = new Business.CostInfo().GetConnectionStringStr(Community);

            DataTable dt = null;
            using (IDbConnection conn = new SqlConnection(connStr)) {
                string SQLEx = "AND CommID = " + Community.CommID + " AND ISNULL(IsDelete,0) = 0";
                dt = conn.ExecuteReader("Proc_HSPR_Region_Filter", new { SQLEx = SQLEx }, null, null, CommandType.StoredProcedure).ToDataSet().Tables[0];
            }
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            if (null != dt && dt.Rows.Count > 0) {
                Dictionary<string, object> dic;
                foreach (DataRow item in dt.Rows)
                {
                    dic = new Dictionary<string, object>
                    {
                        { "RegionID", item["RegionID"] },
                        { "RegionName", item["RegionName"] }
                    };
                    list.Add(dic);
                }
            }
            return new ApiResult(true, list).toJson();
        }
        /// <summary>
        /// 省列表
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string GetProvinceList(DataRow row)
        {

            DataSet ds = new DbHelperSQLP(PubConstant.UnifiedContionString.ToString()).Query("SELECT Province FROM Tb_Community GROUP BY Province");
           
            if (ds.Tables.Count>0)
            {
                return JSONHelper.FromString(ds.Tables[0]);
            }
            else
            {
                return JSONHelper.FromString(false, "暂无数据");
            }
        }



        #region 未区分数据权限

        /// <summary>
        /// 城市列表
        /// </summary>
        /// <param name="row"></param>
        /// CorpID 公司ID可不填
        /// <returns></returns>
        public string GetCityList(DataRow row)
        {
            string CorpID = "";
            if (row.Table.Columns.Contains("CorpID") && AppGlobal.StrToInt(row["CorpID"].ToString()) > 0)
            {
                CorpID = row["CorpID"].ToString();
            }

            DataTable dTable = null;
            if (CorpID != "")
            {
                string[] str = CorpID.Split(',');
                if (str.Length > 0)
                {
                    CorpID = "";
                    for (int i = 0; i < str.Length; i++)
                    {
                        if (i == 0)
                        {
                            CorpID = str[i];
                        }
                        else
                        {
                            CorpID += "," + str[i];
                        }
                    }
                    dTable = new DbHelperSQLP(PubConstant.UnifiedContionString.ToString()).Query("SELECT City FROM Tb_Community where CorpID in (" + CorpID + ") GROUP BY City").Tables[0];
                }
                else
                {
                    dTable = new DbHelperSQLP(PubConstant.UnifiedContionString.ToString()).Query("SELECT City FROM Tb_Community GROUP BY City").Tables[0];
                }
            }
            if (CorpID == "")
            {
                dTable = new DbHelperSQLP(PubConstant.UnifiedContionString.ToString()).Query("SELECT City FROM Tb_Community GROUP BY City").Tables[0];
            }

            if (dTable.Rows.Count > 0)
            {
                return JSONHelper.FromString(dTable);
            }
            else
            {
                return JSONHelper.FromString(false, "暂无数据");
            }

        }


        /// <summary>
        /// 小区列表
        /// </summary>
        /// <param name="Row"></param>
        /// <returns></returns>
        public string GetCommList(DataRow Row)
        {
            if (Row.Table.Columns.Contains("City"))
            {

                DataTable dTable = new DbHelperSQLP(PubConstant.UnifiedContionString.ToString()).Query(
                    string.Format(@"SELECT Id,Province,Area,City,CorpID,CommID,CorpName,CommName,ModuleRights,isnull(Tel,'') AS Tel 
                        FROM Tb_Community WHERE City = '{0}' ORDER BY CommName ASC", AppGlobal.ChkStr(Row["City"].ToString().Trim()))).Tables[0];
                if (dTable.Rows.Count > 0)
                {
                    return JSONHelper.FromString(dTable);
                }
                else
                {
                    return JSONHelper.FromString(false, "暂无数据");
                }
            }
            else
            {
                return JSONHelper.FromString(false, "缺少参数City!");
            }

        }

        #endregion

        #region 根据城市获取小区列表html5
        /// <summary>
        /// 根据城市获取小区列表html5   GetCommListforH5
        /// </summary>
        /// <param name="Row"></param>
        /// <returns></returns>
        public string GetCommListforH5(DataRow Row)
        {
            string CorpID = "";
            if (Row.Table.Columns.Contains("CorpID") && Row["CorpID"].ToString() != "")
            {
                CorpID = Row["CorpID"].ToString();
            }
            if (Row.Table.Columns.Contains("City"))
            {
                string sql;
                if (string.IsNullOrEmpty(CorpID))
                {
                    sql = "SELECT c.Id,c.Province,c.Area,c.City,c.CorpID,c.CommID,c.CorpName,c.CommName,c.ModuleRights,c.Tel,h.Appid,h.Secret FROM Tb_Community as c inner join Tb_WeiXinH5_Config as h on c.Id = h.communityId WHERE c.City = '"+ AppGlobal.ChkStr(Row["City"].ToString()) + "' ORDER BY CommName ASC";
                }
                else
                {
                    string[] str = CorpID.Split(',');
                    if (str.Length > 0)
                    {
                        CorpID = "";
                        for (int i = 0; i < str.Length; i++)
                        {
                            if (i == 0)
                            {
                                CorpID = str[i];
                            }
                            else
                            {
                                CorpID += "," + str[i];
                            }
                        }
                    }
                    sql = "SELECT c.Id,c.Province,c.Area,c.City,c.CorpID,c.CommID,c.CorpName,c.CommName,c.ModuleRights,c.Tel,h.Appid,h.Secret FROM Tb_Community as c inner join Tb_WeiXinH5_Config as h on c.Id = h.communityId WHERE c.City = '" + AppGlobal.ChkStr(Row["City"].ToString()) + "' AND c.CorpID IN (" + CorpID + ") ORDER BY CommName ASC";
                }
                DataTable dTable = new DbHelperSQLP(PubConstant.UnifiedContionString.ToString()).Query(sql).Tables[0];
                if (dTable.Rows.Count > 0)
                {
                    return JSONHelper.FromString(dTable);
                }
                else
                {
                    return JSONHelper.FromString(false, "暂无数据");
                }
            }
            else
            {
                return JSONHelper.FromString(false, "缺少参数City!");
            }

        }


        #endregion

        #region 根据经纬度获取周围1KM内最近的一个小区
        /// <summary>
        /// 根据传入的经纬度(百度坐标系),获取周边1000m内最近的一个小区
        /// 使用百度LBS云检索(http://lbsyun.baidu.com/index.php?title=lbscloud/api/geosearch)
        /// 百度平台帐号:15528116260
        /// 创建人:敬志强(QQ:437936609,Tel:15528116260)
        /// AK=QZeG5phVOEOGhXNjE2uwPoMB6iunE142
        /// 数据服务ID=171709,NAME=tw_community(虎鲸数据管理平台/百度旗下)(http://lbsyun.baidu.com/data/mydata#/?_k=7qubta)
        /// </summary>
        /// <param name="Row"></param>
        /// <returns></returns>
        public string GetCommListNearBy(DataRow Row)
        {
            try
            {
                if (!Row.Table.Columns.Contains("location"))
                {
                    return new ApiResult(false, "缺少参数location").toJson();
                }
                string location = Row["location"].AsString();

                #region 对传入的经纬度进行数据校验
                //经纬度是以逗号分割的字符串
                string[] latlng = location.Split(',');
                double lng;
                double lat;
                bool isDouble = false;
                //尝试转为double
                isDouble = double.TryParse(latlng[0], out lng);
                isDouble = double.TryParse(latlng[1], out lat);
                //如果转换失败,就是经纬度问题
                if (!isDouble)
                {
                    return new ApiResult(false, "传入的经纬度有误").toJson();
                }
                #endregion

                #region 百度云检索参数
                //构造经纬度
                location = lng.ToString() + "," + lat.ToString();
                //访问AK
                string ak = "QZeG5phVOEOGhXNjE2uwPoMB6iunE142";
                //数据表ID
                string geotable_id = "171709";
                //检索半径
                string radius = "1000";
                //排序规则
                string sortby = "distance:1";
                //分页页码(从0开始)
                string page_index = "0";
                //分页大小(当前接口只需要获取1个小区)
                string page_size = "1";
                #endregion

                #region 请求百度云检索数据并解析
                //请求地址
                string url = "http://api.map.baidu.com/geosearch/v3/nearby?ak={0}&geotable_id={1}&location={2}&radius={3}&sortby={4}&page_index={5}&page_size={6}";
                //格式化参数
                url = string.Format(url, ak, geotable_id, location, radius, sortby, page_index, page_size);

                //请求百度云检索数据
                WebClient client = new WebClient();
                string result = client.DownloadString(url);
                JObject obj = (JObject)JsonConvert.DeserializeObject(result);
                //如果转json失败,就默认返回请求失败
                if (null == obj) {
                    return new ApiResult(false, "获取小区失败(数据请求失败)请重试").toJson();
                }
                //尝试获取status,只有当status=0时才是请求成功
                //否则请根据错误信息和错误代码去(http://bbs.lbsyun.baidu.com/forum.php?mod=viewthread&tid=42223&page=1&extra=#pid93042)
                JToken token;
                if (!obj.TryGetValue("status", out token)) {
                    return new ApiResult(false, "获取小区失败(数据解析失败)请重试").toJson();
                }
                if(0!= (int)token)
                {
                    if (!obj.TryGetValue("message", out token))
                    {
                        return new ApiResult(false, "获取小区失败(检索失败)请重试").toJson();
                    }
                    return new ApiResult(false, "获取小区失败(" + token.AsString() + ")请重试").toJson();
                }
                if (!obj.TryGetValue("contents", out token)){
                    return new ApiResult(false, "获取小区失败(没有获取到小区数据)请重试").toJson();
                }
                //contents是一个数组
                JArray jArr = (JArray)token;
                if (null == jArr || jArr.Count == 0) {
                    return new ApiResult(false, "获取小区失败(没有获取到任何小区数据)请重试").toJson();
                }
                //只取第一个内容
                obj = (JObject)jArr[0];
                if (null == obj ||! obj.TryGetValue("community_id", out token)) {
                    return new ApiResult(false, "获取小区失败(获取小区ID失败)请重试").toJson();
                }
                //只需要拿到存储在百度云检索的这个自定义字段
                string community_id = token.AsString();
                DataTable dTable = new DbHelperSQLP(PubConstant.UnifiedContionString.ToString()).Query(string.Format("SELECT Id,Province,Area,City,CorpID,CommID,CorpName,CommName,ModuleRights,Tel FROM Tb_Community WHERE Id = '{0}' ORDER BY CommName ASC", community_id)).Tables[0];
                if (dTable.Rows.Count > 0)
                {
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic.Add("Id", dTable.Rows[0]["Id"]);
                    dic.Add("Province", dTable.Rows[0]["Province"]);
                    dic.Add("Area", dTable.Rows[0]["Area"]);
                    dic.Add("City", dTable.Rows[0]["City"]);
                    dic.Add("CorpID", dTable.Rows[0]["CorpID"]);
                    dic.Add("CommID", dTable.Rows[0]["CommID"]);
                    dic.Add("CorpName", dTable.Rows[0]["CorpName"]);
                    dic.Add("CommName", dTable.Rows[0]["CommName"]);
                    dic.Add("ModuleRights", dTable.Rows[0]["ModuleRights"]);
                    dic.Add("Tel", dTable.Rows[0]["Tel"]);
                    return new ApiResult(true, dic).toJson();
                }
                else
                {
                    return new ApiResult(false, "获取小区失败(没有找到对应的小区)请重试").toJson();
                }
                #endregion
            }
            catch (Exception ex)
            {
                return new ApiResult(false, ex.Message).toJson();
            }
        }
        #endregion

        #region 区分数据权限
        /// <summary>
        /// 城市列表
        /// </summary>
        /// <param name="row"></param>
        /// CorpID 公司ID可不填
        /// <returns></returns>
        public string GetCityList_new(DataRow row)
        {
            string CorpID = "";
            if (row.Table.Columns.Contains("CorpID") && row["CorpID"].ToString()!="")
            {
                CorpID = row["CorpID"].ToString();
            }
            string strWhere = "  ";
            if (row.Table.Columns.Contains("AppleBundleID") && row["AppleBundleID"].ToString()!="")
            {
                strWhere += " and AppleBundleID='" + row["AppleBundleID"]+"' ";
            }
            if (row.Table.Columns.Contains("AndroidPackageName") && row["AndroidPackageName"].ToString()!="")
            {
                strWhere += " and AndroidPackageName='" + row["AndroidPackageName"]+"' ";
            }
            //strWhere += ")";


            DataTable dTable = null;
            if (CorpID != "")
            {
                string[] str = CorpID.Split(',');
                if (str.Length > 0)
                {
                    CorpID = "";
                    for (int i = 0; i < str.Length; i++)
                    {
                        if (i == 0)
                        {
                            CorpID = str[i];
                        }
                        else
                        {
                            CorpID += "," + str[i];
                        }
                    }
                    dTable = new DbHelperSQLP(PubConstant.UnifiedContionString.ToString()).Query("SELECT City FROM Tb_Community where CorpID in (" + CorpID + ") "+ strWhere + " GROUP BY City").Tables[0];
                }
                else
                {
                    dTable = new DbHelperSQLP(PubConstant.UnifiedContionString.ToString()).Query("SELECT City FROM Tb_Community where 1=1 "+ strWhere + " GROUP BY City").Tables[0];
                }
            }
            if (CorpID == "")
            {
                dTable = new DbHelperSQLP(PubConstant.UnifiedContionString.ToString()).Query("SELECT City FROM Tb_Community where 1=1 "+ strWhere + " GROUP BY City").Tables[0];
            }

            if (dTable.Rows.Count > 0)
            {
                return JSONHelper.FromString(dTable);
            }
            else
            {
                return JSONHelper.FromString(false, "暂无数据");
            }

        }


        /// <summary>
        /// 小区列表
        /// </summary>
        /// <param name="Row"></param>
        /// <returns></returns>
        public string GetCommList_new(DataRow Row)
        {
            if (Row.Table.Columns.Contains("City"))
            {
                string strWhere = "  ";

                if (Row.Table.Columns.Contains("AppleBundleID") && Row["AppleBundleID"].ToString() != "")
                {
                    strWhere += " and AppleBundleID='" + Row["AppleBundleID"] + "' ";
                }
                if (Row.Table.Columns.Contains("AndroidPackageName") && Row["AndroidPackageName"].ToString() != "")
                {
                    strWhere += " and AndroidPackageName='" + Row["AndroidPackageName"] + "' ";
                }

                using (var conn = new SqlConnection(PubConstant.UnifiedContionString))
                {
                    var sql = @"SELECT count(1) FROM syscolumns WHERE id=object_id('Tb_Community') AND name='IsMultiDoorControlServer'";
                    if (conn.Query<int>(sql).FirstOrDefault() > 0)
                    {
                        sql = @"SELECT Id,Province,Area,City,CorpID,CommID,CorpName,CommName,ModuleRights,
                                    isnull(Tel,'') AS Tel, isnull(Tel,'') AS ServiceTel,IsMultiDoorControlServer 
                                FROM Tb_Community";
                    }
                    else
                    {
                        sql = @"SELECT Id,Province,Area,City,CorpID,CommID,CorpName,CommName,ModuleRights,
                                    isnull(Tel,'') AS Tel, isnull(Tel,'') AS ServiceTel,convert(bit, 0) AS IsMultiDoorControlServer 
                                FROM Tb_Community";
                    }

                    sql += $" WHERE City='{ Row["City"].ToString() }' { strWhere } ORDER BY CommName ASC";

                    var comms = conn.Query(sql);
                    return new ApiResult(true, comms).toJson();
                }
            }
            else
            {
                return JSONHelper.FromString(false, "缺少参数City!");
            }

        }

        #endregion

    }

}