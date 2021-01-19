using com.sun.crypto.provider;
using Common;
using Common.Enum;
using Dapper;
using MobileSoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class PMSCommunityService : PubInfo
    {
        public PMSCommunityService()
        {
            base.Token = "202009071128PMSCommunityService";
        }

        public override void Operate(ref Transfer Trans)
        {
            DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
            DataRow row = dAttributeTable.Rows[0];
            //防止未捕获异常出现
            try
            {
                switch (Trans.Command)
                {
                    case "GetServiceList":
                        Trans.Result = GetServiceList(row);
                        break;
                    case "GetServiceDetail":
                        Trans.Result = GetServiceDetail(row);
                        break;
                    case "AddServiceIntention":
                        Trans.Result = AddServiceIntention(row);
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
        /// 获取社区服务列表
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private String GetServiceList(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || String.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return new ApiPageResult(false, "参数错误").toJson();
            }
            int type = (int)EnumCommunityServiceType.Financial;
            if (row.Table.Columns.Contains("Type") && String.IsNullOrEmpty(row["Type"].ToString()))
            {
                int.TryParse(row["Type"].ToString(), out type);
            }
            String communityId = row["CommunityId"].ToString();
            var pageInfoModel = GetParamEntity(row);
            try
            {
                using (var appConn = new SqlConnection(PubConstant.UnifiedContionString))
                {
                    String sqlStr = $@"SELECT * FROM
                                        (SELECT *,ROW_NUMBER() OVER(ORDER BY AddTime DESC ) AS  RowNumber  FROM 
                                        (SELECT * FROM  Tb_CommunityService WHERE CommunityId is null
                                        Union ALL
                                        SELECT * FROM  Tb_CommunityService WHERE CommunityId LIKE '%{communityId}%'
                                        ) AS  Tb_CommunityService WHERE CustomizedType=@CustomizedType AND ISNULL(IsDelete,0)=0  AND IsRelease=1) AS T
                                        WHERE T.RowNumber BETWEEN @Start AND @End";

                    var param = new { CustomizedType = EnumHelper.GetEnumDesc(typeof(EnumCommunityServiceType), type), CommunityId = communityId, Start = pageInfoModel.GetStart(), End = pageInfoModel.GetEnd() };
                    var data = appConn.Query(sqlStr, param);
                    if (data != null) return new ApiPageResult(true, data).toJson();
                    return new ApiPageResult(false, "获取失败").toJson();
                }
            }
            catch (Exception ex)
            {
                //ignore
            }
            return null;
        }

        /// <summary>
        /// 获取社区服务列表
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private String GetServiceDetail(DataRow row)
        {
            if (!row.Table.Columns.Contains("SercviceId") || String.IsNullOrEmpty(row["SercviceId"].ToString()))
            {
                return new ApiPageResult(false, "参数错误").toJson();
            }
            int type = (int)EnumCommunityServiceType.Financial;
            if (row.Table.Columns.Contains("Type") && String.IsNullOrEmpty(row["Type"].ToString()))
            {
                int.TryParse(row["Type"].ToString(), out type);
            }
            String sercviceId = row["SercviceId"].ToString();
            try
            {
                using (var appConn = new SqlConnection(PubConstant.UnifiedContionString))
                {
                    String sqlStr = @"SELECT * FROM Tb_CommunityService  WHERE CustomizedType=@CustomizedType AND ISNULL(IsDelete,0)=0  AND IsRelease=1 AND ID=@Id";
                    var param = new { Id = sercviceId, CustomizedType = type.ToString() };
                    var data = appConn.QueryFirstOrDefault(sqlStr, param);
                    if (data != null && data.Any()) return new ApiPageResult(true, data).toJson();
                    return new ApiPageResult(false, "获取失败").toJson();
                }
            }
            catch (Exception ex)
            {
                //ignore
            }
            return null;
        }

        /// <summary>
        /// 针对服务有意向
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private String AddServiceIntention(DataRow row)
        {
            if (!row.Table.Columns.Contains("ServiceId") || String.IsNullOrEmpty(row["ServiceId"].ToString()))
            {
                return new ApiPageResult(false, "参数错误").toJson();
            }
            if (!row.Table.Columns.Contains("LinkMan") || String.IsNullOrEmpty(row["LinkMan"].ToString()))
            {
                return new ApiPageResult(false, "参数错误").toJson();
            }
            if (!row.Table.Columns.Contains("UserId") || String.IsNullOrEmpty(row["UserId"].ToString()))
            {
                return new ApiPageResult(false, "参数错误").toJson();
            }
            if (!row.Table.Columns.Contains("LinkMobile") || String.IsNullOrEmpty(row["LinkMobile"].ToString()))
            {
                return new ApiPageResult(false, "参数错误").toJson();
            }
            String remark = String.Empty;
            if (row.Table.Columns.Contains("Remark") && !String.IsNullOrEmpty(row["Remark"].ToString()))
            {
                remark = row["Remark"].ToString();
            }

            String serviceId = row["ServiceId"].ToString();
            String linkMan = row["LinkMan"].ToString();
            String linkMobile = row["LinkMobile"].ToString();
            String userId = row["UserId"].ToString();
            try
            {
                using (var appConn = new SqlConnection(PubConstant.UnifiedContionString))
                {
                    String sqlStr = @"INSERT [TB_Service_Intention]  VALUES(NEWID(),@UserId, @ServiceId, @LinkMan, @LinkMobile, GETDATE(), @Remark)";
                    var count = appConn.Execute(sqlStr, new { ServiceId = serviceId, UserId = userId, LinkMan = linkMan, LinkMobile = linkMobile, Remark = remark });
                    if (count > 0) return new ApiPageResult(true, "插入成功").toJson();
                    return new ApiPageResult(false, "插入失败").toJson();
                }
            }
            catch (Exception ex)
            {
                //ignore
            }
            return null;
        }
    }
}
