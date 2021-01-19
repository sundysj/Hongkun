using Aop.Api.Request;
using Common;
using Common.Extenions;
using Dapper;
using java.nio.channels;

using MobileSoft.Common;
using MobileSoft.DBUtility;
using MobileSoft.Model.Unified;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using static Dapper.SqlMapper;
namespace Business
{
    public class PMSAppHousekeeper : PubInfo
    {
        public PMSAppHousekeeper()
        {
            base.Token = "20200602PMSAppHousekeeper";
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
                    case "GetHousekeeperInfo":
                        Trans.Result = GetHousekeeperInfo(Row);
                        break;
                    case "SaveEvaluate":
                        Trans.Result = SaveEvaluate(Row);
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
        /// 获取用户评价
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string GetHousekeeperInfo(DataRow row)
        {
            long roomId;
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].AsString()))
            {
                return new ApiResult(false, "缺少参数CommunityId").toJson();
            }
            if (!row.Table.Columns.Contains("RoomID")
                || string.IsNullOrEmpty(row["RoomID"].AsString())
                || !long.TryParse(row["RoomID"].ToString(), out roomId))
            {
                return new ApiResult(false, "缺少参数RoomID").toJson();
            }

            var communityId = row["CommunityId"].ToString();
            var community = GetCommunity(communityId);
            if (community == null)
            {
                return JSONHelper.FromString(false, "未查询到小区信息");
            }
            PubConstant.hmWyglConnectionString = GetConnectionStr(community);
            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = @"SELECT CustID FROM Tb_HSPR_CustomerLive WHERE RoomID=@RoomID AND LiveType=1 AND isnull(IsDelLive,0)=0";

                var custid = conn.Query<long>(sql, new { RoomID = roomId }).FirstOrDefault();
                GetLog().Info("获取用户评价:" + custid);
                sql = @"SELECT UserCode as KeeperCode,UserName,Tel,HeadImg,isnull(AvgScore,0) AS AvgScore,LastEvaluationDate,(SELECT TOP(1) EvaluationPeriod  FROM Tb_HSPR_KeeperEvaluationSet) As  EvaluationPeriod,
                            CASE WHEN LastEvaluationDate IS NULL THEN 1
                            WHEN datediff(MONTH,LastEvaluationDate,getdate())>
                                    (SELECT TOP 1 isnull(EvaluationPeriod,1) FROM Tb_HSPR_KeeperEvaluationSet)
                            THEN 1
                            ELSE 0 END AS CanEvaluation
                        FROM
                        (
                            SELECT a.UserCode,a.Tel,b.UserName,b.HeadImg,
                                (
                                    SELECT avg(Score) FROM Tb_HSPR_KeeperEvaluation x
                                    WHERE x.KeeperCode=a.UserCode AND isnull(x.IsDelete,0)=0
                                ) AS AvgScore,
                                (
                                    SELECT max(EvaluationDate) FROM Tb_HSPR_KeeperEvaluation x
                                    WHERE x.EvaluationCustID=@CustID AND isnull(x.IsDelete,0)=0
                                ) AS LastEvaluationDate
                            FROM Tb_HSPR_RoomHousekeeper a
                                INNER JOIN Tb_Sys_User b ON a.UserCode=b.UserCode
                            WHERE RoomID=@RoomID ) AS t;";

                var data = conn.Query(sql, new { RoomID = roomId, CustID = custid });
                if (data != null && data.Any()) return new ApiResult(true, data).toJson();

                sql = @"SELECT UserCode as KeeperCode,UserName,Tel,HeadImg,isnull(AvgScore,0) AS AvgScore,LastEvaluationDate,(SELECT TOP(1) EvaluationPeriod  FROM Tb_HSPR_KeeperEvaluationSet) As  EvaluationPeriod,
                            CASE WHEN LastEvaluationDate IS NULL THEN 1
                            WHEN datediff(MONTH,LastEvaluationDate,getdate())>
                                    (SELECT TOP 1 isnull(EvaluationPeriod,1) FROM Tb_HSPR_KeeperEvaluationSet)
                            THEN 1
                            ELSE 0 END AS CanEvaluation
                        FROM
                        (
                            SELECT a.UserCode,a.Tel,b.UserName,b.HeadImg,
                                (
                                    SELECT avg(Score) FROM Tb_HSPR_KeeperEvaluation x
                                    WHERE x.KeeperCode=a.UserCode AND isnull(x.IsDelete,0)=0
                                ) AS AvgScore,
                                (
                                    SELECT max(EvaluationDate) FROM Tb_HSPR_KeeperEvaluation x
                                    WHERE x.EvaluationCustID=@CustID AND isnull(x.IsDelete,0)=0
                                ) AS LastEvaluationDate
                            FROM ( 
                            SELECT o.UserCode,o.Tel
                            FROM Tb_HSPR_Room u 
                            INNER JOIN Tb_HSPR_Building i ON u.CommID=i.CommID AND u.BuildSNum=i.BuildSNum AND isnull(i.IsDelete,0)=0
                            INNER JOIN Tb_HSPR_BuildHouseKeeper o ON i.CommID=o.CommID AND i.BuildSNum=o.BuildSNum
                             WHERE RoomID=@RoomID
                            ) a
                                INNER JOIN Tb_Sys_User b ON a.UserCode=b.UserCode
                             ) AS t;";  

                var dataInfo = conn.Query(sql, new { RoomID = roomId, CustID = custid });
                if (dataInfo != null && dataInfo.Any()) return new ApiResult(true, dataInfo).toJson();

                return new ApiResult(false, null).toJson();
            }

        }


        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private String SaveEvaluate(DataRow row)
        {
            #region 参数验证
            long roomId = 0;
            decimal avgScore = 0;
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].AsString()))
                return new ApiResult(false, "缺少参数CommunityId").toJson();
            if (!row.Table.Columns.Contains("RoomID")
                || string.IsNullOrEmpty(row["RoomID"].AsString())
                || !long.TryParse(row["RoomID"].ToString(), out roomId))
                return new ApiResult(false, "缺少参数RoomID").toJson();
            //评价等计
            if (!row.Table.Columns.Contains("AvgScore")
                || string.IsNullOrEmpty(row["AvgScore"].AsString())
                || !decimal.TryParse(row["AvgScore"].ToString(), out avgScore))
                return new ApiResult(false, "缺少参数AvgScore").toJson();
            //评价的类容
            if (!row.Table.Columns.Contains("EvaluationContent") || string.IsNullOrEmpty(row["EvaluationContent"].AsString()))
                return new ApiResult(false, "缺少参数EvaluationContent").toJson();
            //管家的编码
            if (!row.Table.Columns.Contains("KeeperCode") || string.IsNullOrEmpty(row["KeeperCode"].AsString())) return new ApiResult(false, "缺少参数KeeperCode").toJson();

            String communityId = row["CommunityId"].ToString();
            var evaluationContent = row["EvaluationContent"].ToString();
            String keeperCode = row["KeeperCode"].ToString();

            #endregion

            var community = GetCommunity(communityId);
            if (community == null) return JSONHelper.FromString(false, "未查询到小区信息");
            PubConstant.hmWyglConnectionString = GetConnectionStr(community);
            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = @"SELECT B.* FROM ( SELECT CustID FROM Tb_HSPR_CustomerLive WHERE RoomID=@RoomID AND LiveType=1 AND isnull(IsDelLive,0)=0 ) AS A
						LEFT JOIN (SELECT CustID,CustName FROM Tb_HSPR_Customer where isdelete=0) AS B ON A.CustID=B.CustID";
                var custid = conn.QueryFirstOrDefault(sql, new { RoomID = roomId });
                if(null == custid) return new ApiResult(false, "客户不存在").toJson();

                var sqlCan = @"SELECT  CASE WHEN LastEvaluationDate IS NULL THEN 1
                            WHEN datediff(MONTH,LastEvaluationDate,getdate())> (SELECT TOP 1 isnull(EvaluationPeriod,1) FROM Tb_HSPR_KeeperEvaluationSet)
                            THEN 1  ELSE 0 END AS CanEvaluation FROM
                        ( SELECT (  SELECT max(EvaluationDate) FROM Tb_HSPR_KeeperEvaluation x
                                    WHERE x.EvaluationCustID=@CustID AND isnull(x.IsDelete,0)=0
                                ) AS LastEvaluationDate
                            FROM 
                             (select * from Tb_HSPR_KeeperEvaluation where RoomId=@RoomId AND ISNULL(IsDelete,0)=0) as a
                        ) AS t  ;";
                var canDo = conn.QueryFirstOrDefault<int?>(sqlCan, new { RoomId = roomId, CustID = custid.CustID });
                if (canDo.HasValue && canDo.Value == 0) return new ApiResult(false, "已评价").toJson();

                sql = @"INSERT Tb_HSPR_KeeperEvaluation (ID,CommId,RoomId,EvaluationCustID,KeeperCode,Score,EvaluationContent,EvaluationDate,IsDelete,CustomerName)
                                VALUES (@ID,@CommId,@RoomId,@EvaluationCustID,@KeeperCode,@Score,@EvaluationContent,GETDATE(),0,@CustomerName)";
                var sqlParam =
                    new { ID = Guid.NewGuid().ToString(), CommId = community.CommID, RoomId = roomId, KeeperCode = keeperCode, Score = avgScore, EvaluationContent = evaluationContent, EvaluationCustID = custid.CustID, CustomerName= custid.CustName };

                var count = conn.Execute(sql, sqlParam);
                if (count > 0) return new ApiResult(true, "评价成功").toJson();
                return new ApiResult(false, "评价失败").toJson();
            }
        }
    }
}

