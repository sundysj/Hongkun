using Dapper;
using MobileSoft.Common;
using MobileSoft.Model.HSPR;
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
    public class IncidentAccept_JH : PubInfo
    {
        public IncidentAccept_JH() //获取小区、项目信息
        {
            base.Token = "20181015IncidentAccept_JH";
        }

        public override void Operate(ref Common.Transfer Trans)
        {
            Trans.Result = JSONHelper.FromString(false, "未知错误");

            DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
            DataRow Row = dAttributeTable.Rows[0];

            switch (Trans.Command)
            {
                case "IncidentAccept":              // 报事提交
                    Trans.Result = IncidentAccept(Row);
                    break;
                case "IncidentCancel":              // 报事提交
                    Trans.Result = IncidentCancel(Row);
                    break;
                case "GetIncidentHistory":          // 获取报事历史
                    Trans.Result = GetIncidentHistory(Row);
                    break;
                case "GetIncidentDetail":           // 获取报事详情
                    Trans.Result = GetIncidentDetail(Row);
                    break;
                case "IncidentEvaluate":           // 获取报事详情
                    Trans.Result = IncidentEvaluate(Row);
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// 业主取消报事
        /// </summary>
        private string IncidentCancel(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            if (!row.Table.Columns.Contains("IncidentID") || string.IsNullOrEmpty(row["IncidentID"].ToString()))
            {
                return JSONHelper.FromString(false, "报事编号不能为空");
            }


            string communityId = row["CommunityId"].ToString();

            Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(communityId);
            //构造链接字符串
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }
            string strcon = GetConnectionStringStr(Community);

            using (IDbConnection conn = new SqlConnection(strcon))
            {
                conn.Execute(@"UPDATE Tb_HSPR_IncidentAccept SET IsDelete=1 WHERE IncidentID=@IncidentID", new { IncidentID = row["IncidentID"].ToString() });

                return JSONHelper.FromString(true, "操作成功");
            }
        }

        /// <summary>
        /// 金辉版本报事
        /// </summary>
        private string IncidentAccept(DataRow row)
        {
            #region 基础数据校验
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            if (!row.Table.Columns.Contains("IncidentContent") || string.IsNullOrEmpty(row["IncidentContent"].AsString()))
            {
                return new ApiResult(false, "报事内容不能为空").toJson();
            }
            if (!row.Table.Columns.Contains("Phone") || string.IsNullOrEmpty(row["Phone"].ToString()))
            {
                return JSONHelper.FromString(false, "联系方式不能为空");
            }
            if (!row.Table.Columns.Contains("IncidentMan") || string.IsNullOrEmpty(row["IncidentMan"].AsString()))
            {
                return new ApiResult(false, "报事人不能为空").toJson();
            }
            //户内/公区
            if (!row.Table.Columns.Contains("IncidentPlace") || string.IsNullOrEmpty(row["IncidentPlace"].AsString()))
            {
                return new ApiResult(false, "缺少参数IncidentPlace").toJson();
            }
            if (!row.Table.Columns.Contains("CustID") || string.IsNullOrEmpty(row["CustID"].ToString()))
            {
                return JSONHelper.FromString(false, "客户编号不能为空");
            }
            if (!row.Table.Columns.Contains("RoomID") || string.IsNullOrEmpty(row["RoomID"].ToString()))
            {
                return JSONHelper.FromString(false, "房间编号不能为空");
            }
            if (!row.Table.Columns.Contains("ReserveDate") || string.IsNullOrEmpty(row["ReserveDate"].ToString()))
            {
                return JSONHelper.FromString(false, "预约处理时间不能为空");
            }

            string communityId = row["CommunityId"].ToString();

            Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(communityId);
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }

            string incidentPlace = row["IncidentPlace"].AsString(); // 报事区域
            string custID = row["CustID"].AsString();                                    // 户内报事业主id
            string roomID = row["RoomID"].AsString();                                    // 户内报事房屋编号
            string drClass = "1";                                           // 报事类型

            string commID = Community.CommID;                               // 小区id
            string incidentSource = "客户线上报事";                            // 报事来源
            string incidentMan = row["IncidentMan"].AsString();             // 报事人
            string phone = row["Phone"].AsString();                         // 报事电话
            string incidentContent = row["IncidentContent"].AsString();     // 报事内容
            string reserveDate = row["ReserveDate"].ToString();             // 要求处理时间
            string incidentImgs = null;

            if (row.Table.Columns.Contains("IncidentImgs") && !string.IsNullOrEmpty(row["IncidentImgs"].AsString()))
            {
                incidentImgs = row["IncidentImgs"].AsString();
            }

            #endregion

            string connStr = GetConnectionStringStr(Community);

            #region 获取incidentID
            string incidentID;
            using (IDbConnection conn = new SqlConnection(connStr))
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@CommID", commID);
                param.Add("@SQLEx", "");
                incidentID = conn.ExecuteScalar("Proc_HSPR_IncidentAccept_GetMaxNum", param, null, null, CommandType.StoredProcedure).AsString();
            }

            #endregion 

            #region 获取incidentNum
            string incidentNum;
            using (IDbConnection conn = new SqlConnection(connStr))
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@CommID", commID);
                incidentNum = conn.ExecuteScalar("Proc_HSPR_IncidentAccept_GetMaxIncidentNum", param, null, null, CommandType.StoredProcedure).AsString();
            }

            #endregion

            #region 插入报事记录
            using (IDbConnection conn = new SqlConnection(connStr))
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@AdmiMan", Global_Var.LoginUserName);
                param.Add("@CommID", commID);
                param.Add("@IncidentPlace", incidentPlace);
                param.Add("@IncidentID", incidentID);
                param.Add("@IncidentNum", incidentNum);
                param.Add("@IncidentSource", incidentSource);
                param.Add("@DrClass", drClass);
                param.Add("@IsTouSu", 0);
                param.Add("@CustID", custID);
                param.Add("@RoomID", roomID);
                param.Add("@RegionalID", null);
                param.Add("@EqId", null);
                param.Add("@TaskEqId", null);
                param.Add("@IncidentMan", incidentMan);
                param.Add("@IncidentContent", incidentContent);
                param.Add("@IncidentImgs", incidentImgs);
                param.Add("@Phone", phone);
                param.Add("@ReserveDate", reserveDate);
                param.Add("@Duty", "物业");
                param.Add("@BigCorpTypeID", null);
                param.Add("@FineCorpTypeID", null);

                conn.Execute("Proc_HSPR_IncidentAccept_Insert_Phone_Cust", param, null, null, CommandType.StoredProcedure);

                var incidentInfo = conn.Query<Tb_HSPR_IncidentAccept_jh>(@"SELECT * FROM Tb_HSPR_IncidentAccept WHERE IncidentID=@IncidentID",
                    new { IncidentID = incidentID }).FirstOrDefault();

                return new ApiResult(true, new { IncidentID = incidentID, IncidentNum = incidentNum }).toJson();
            }
            #endregion
        }

        /// <summary>
        /// 报事历史列表
        /// </summary>
        private string GetIncidentHistory(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return new ApiResult(false, "小区编号不能为空").toJson();
            }

            if (!row.Table.Columns.Contains("RoomID") || string.IsNullOrEmpty(row["RoomID"].ToString()))
            {
                return new ApiResult(false, "房屋ID不能为空").toJson();
            }

            string communityId = row["CommunityId"].AsString();
            string RoomID = row["RoomID"].AsString();

            int page = 1;
            int size = 10;

            if (!row.Table.Columns.Contains("PageIndex") || string.IsNullOrEmpty(row["PageIndex"].ToString()))
            {
                page = 1;
            }
            else
            {
                if (!int.TryParse(row["PageIndex"].AsString(), out page))
                {
                    page = 1;
                }
            }

            if (!row.Table.Columns.Contains("PageSize") || string.IsNullOrEmpty(row["PageSize"].ToString()))
            {
                size = 10;
            }
            else
            {
                if (!int.TryParse(row["PageSize"].AsString(), out size))
                {
                    size = 10;
                }
            }

            //查询小区
            Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(communityId);
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }
            string connStr = GetConnectionStringStr(Community);

            string sql = $@"SELECT a.IncidentID,a.IncidentNum,a.IncidentContent,
                        isnull(convert(VARCHAR(30), a.IncidentDate, 120), '') AS IncidentDate,
                        CASE
                            WHEN a.IsDelete=1 THEN '取消'
                            WHEN (a.DealState=1 AND isnull(a.IsDelete,0)=0) THEN '完成'
                            WHEN (a.DispType=1 AND isnull(a.IsDelete,0)=0 AND isnull(a.DealState,0)=0) THEN '进行中'
                            ELSE '已受理'
                        END AS State,
                        (SELECT count(1) FROM Tb_HSPR_IncidentReply WHERE IncidentID=a.IncidentID AND ReplyWay='客户线上评价') AS HasEvaluate 
                        FROM Tb_HSPR_IncidentAccept a 
                        WHERE a.IsStatistics=1 AND a.CommID={Community.CommID}
                        AND a.RoomID={RoomID}";
            if (Community.CommID == "1940")
            {
                 sql = $@"SELECT a.IncidentID,a.IncidentNum,a.IncidentContent,
                        isnull(convert(VARCHAR(30), a.IncidentDate, 120), '') AS IncidentDate,
                        CASE
                            WHEN a.IsDelete=1 THEN '取消'
                            WHEN (a.DealState=1 AND isnull(a.IsDelete,0)=0) THEN '完成'
                            WHEN (a.DispType=1 AND isnull(a.IsDelete,0)=0 AND isnull(a.DealState,0)=0) THEN '进行中'
                            ELSE '已受理'
                        END AS State,
                        (SELECT count(1) FROM Tb_HSPR_IncidentReply WHERE IncidentID=a.IncidentID AND IsDelete = 0 ) AS HasEvaluate 
                        FROM Tb_HSPR_IncidentAccept a 
                        WHERE a.IsStatistics=1 AND a.CommID={Community.CommID}
                        AND a.RoomID={RoomID}";

            }
            DataSet ds = GetList(out int pageCount, out int count, sql, page, size, "IncidentDate", 1, "IncidentID", connStr);
            string result = new ApiResult(true, ds.Tables[0]).toJson();
            return result.Insert(result.Length - 1, ",\"PageCount\":" + pageCount);
        }

        /// <summary>
        /// 报事详情
        /// </summary>
        private string GetIncidentDetail(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return new ApiResult(false, "小区编号不能为空").toJson();
            }
            if (!row.Table.Columns.Contains("IncidentID") || string.IsNullOrEmpty(row["IncidentID"].ToString()))
            {
                return new ApiResult(false, "报事编号不能为空").toJson();
            }

            string communityId = row["CommunityId"].AsString();
            string incidentID = row["IncidentID"].AsString();

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
                DataTable dt = conn.ExecuteReader(@"
                SELECT x.IncidentID,x.IncidentNum,x.IncidentContent,x.IncidentDate,x.IncidentMan,x.CommName,x.RoomName,
                x.DispMan,x.DispDate,x.ReceivingDate,x.ArriveData,x.DealMan,x.MainEndDate,x.Phone,x.FinishUser,
                x.IncidentImgs,x.ProcessIncidentImgs,
                (SELECT TOP 1 isnull(MobileTel,'') FROM Tb_HSPR_IncidentAcceptDeal a 
                    LEFT JOIN Tb_Sys_User b ON a.UserCode=b.UserCode
                    WHERE b.UserName=x.FinishUser AND a.IncidentID=x.IncidentID ) AS FinishUserMobileTel,
                (SELECT TOP 1 isnull(MobileTel,'') FROM Tb_Sys_User WHERE UserCode=x.DispUserCode) AS DispManMobileTel,
                (SELECT STUFF((SELECT ','+ISNULL(MobileTel,'') FROM Tb_Sys_User WHERE UserCode IN(
                      SELECT UserCode FROM Tb_HSPR_IncidentAcceptDeal WHERE IncidentID=x.IncidentID) for xml path('')),1,1,''))
                   AS DealManMobileTel,
                (SELECT TOP 1 ServiceQuality FROM Tb_HSPR_IncidentReply WHERE IncidentID=@IncidentID AND ReplyWay='客户线上评价') AS ServiceQuality, 
                (SELECT TOP 1 ReplyContent FROM Tb_HSPR_IncidentReply WHERE IncidentID=@IncidentID AND ReplyWay='客户线上评价') AS EvaluateContent, 
                (SELECT count(1) FROM Tb_HSPR_IncidentReply WHERE IncidentID=@IncidentID AND ReplyWay='客户线上评价') AS HasEvaluate, 
                case
                    WHEN x.IsDelete=1 THEN '取消'
                    WHEN (x.DealState=1 AND isnull(x.IsDelete,0)=0) THEN '完成'
                    WHEN (x.DispType=1 AND isnull(x.IsDelete,0)=0 AND isnull(x.DealState,0)=0) THEN '进行中'
                    ELSE '已受理'
                    END AS State
                FROM view_HSPR_IncidentAccept_Filter x
                WHERE x.IncidentID=@IncidentID",
                new { IncidentID = incidentID }).ToDataSet().Tables[0];
                return JSONHelper.FromString(dt);
            }
        }

        /// <summary>
        /// 报事评价
        /// </summary>
        /// <param name="row"></param>
        /// ServiceQuality 报事评价【非常满意、满意、一般、不满意、很不满意】 【默认非常满意】
        /// <returns></returns>
        private string IncidentEvaluate(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityID") || string.IsNullOrEmpty(row["CommunityID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }

            if (!row.Table.Columns.Contains("IncidentID") || string.IsNullOrEmpty(row["IncidentID"].ToString()))
            {
                return JSONHelper.FromString(false, "报事编号不能为空");
            }
            if (!row.Table.Columns.Contains("ServiceQuality") || string.IsNullOrEmpty(row["ServiceQuality"].ToString()))
            {
                return JSONHelper.FromString(false, "服务质量不能为空");
            }

            string CommID = row["CommunityID"].ToString();
            string IncidentID = row["IncidentID"].ToString();
            string ServiceQuality = "非常满意";
            string CustComments = "";
            if (row.Table.Columns.Contains("ServiceQuality") && row["ServiceQuality"].ToString() != "")
            {
                ServiceQuality = row["ServiceQuality"].ToString();
            }
            if (row.Table.Columns.Contains("EvaluateContent") && row["EvaluateContent"].ToString() != "")
            {
                CustComments = row["EvaluateContent"].ToString();
            }

            //查询小区
            Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(CommID);
            //构造链接字符串
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }

            string strcon = GetConnectionStringStr(Community);


            // 回访方式、回访类型
            string replyWay = "客户线上评价";
            string replyType = "";


            using (IDbConnection conn = new SqlConnection(strcon))
            {
                if (Community.CommID == "1940")
                {
                    conn.Execute(string.Format(@"INSERT INTO Tb_HSPR_IncidentReply(CommID,IncidentID,ReplyManCode,ReplyType,
                                            ReplyDate,ReplyContent,ServiceQuality,ReplyWay,ReplyResult,IsDelete,ReplySource) 
                                            values({0},{1},null,'{2}','{3}','{4}','{5}','{6}',1, 0,'项目回访')",
                                                Community.CommID, IncidentID, replyType, DateTime.Now,
                                                CustComments, ServiceQuality, replyWay));
                }
                else
                {
                    conn.Execute(string.Format(@"INSERT INTO Tb_HSPR_IncidentReply(CommID,IncidentID,ReplyManCode,ReplyType,
                                            ReplyDate,ReplyContent,ServiceQuality,ReplyWay,ReplyResult,IsDelete) 
                                            values({0},{1},null,'{2}','{3}','{4}','{5}','{6}',0, 0)",
                                                Community.CommID, IncidentID, replyType, DateTime.Now,
                                                CustComments, ServiceQuality, replyWay));
                }
            }

            return JSONHelper.FromString(true, "报事评价成功");
        }
    }
}