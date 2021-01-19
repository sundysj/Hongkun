using Dapper;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using static Dapper.SqlMapper;

namespace Business
{
    public partial class PMSIncidentAccept
    {

        /// <summary>
        /// 获取公区列表
        /// </summary>
        private string GetIncidentRegionals(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return new ApiResult(false, "CommID不能为空").toJson();
            }
            var commId = AppGlobal.StrToInt(row["CommID"].ToString());

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = @"SELECT RegionalID,RegionalPlace FROM Tb_HSPR_IncidentRegional
                            WHERE CommID=@CommID AND isnull(IsDelete,0)=0;";
                return new ApiResult(true, conn.Query(sql, new { CommID = commId })).toJson();
            }
        }

        /// <summary>
        /// 获取公区方位列表
        /// </summary>
        private string GetIncidentPublicPosition()
        {
            return GetIncidentPublicLocale("公区方位");
        }

        /// <summary>
        /// 获取公区功能列表
        /// </summary>
        private string GetIncidentPublicFunction()
        {
            return GetIncidentPublicLocale("公区功能");
        }

        /// <summary>
        /// 获取公区信息列表
        /// </summary>
        private string GetIncidentPublicLocale(string localeType)
        {
            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = $@"SELECT IID,DictionaryName AS Name,Remarks FROM Tb_HSPR_IncidentPublicLocale 
                            WHERE isnull(IsDelete,0)=0 AND LocaleType='{localeType}' ORDER BY OrderId;";

                return new ApiResult(true, conn.Query(sql)).toJson();
            }
        }

        /// <summary>
        /// 模糊查询报事设备
        /// </summary>
        private string GetIncidentEquipments(DataRow row)
        {
            var regionalId = 0L;
            var spaceId = "";
            var keywords = "";
            var pageSize = 20;
            var pageIndex = 1;

            if (row.Table.Columns.Contains("RegionalID") && !string.IsNullOrEmpty(row["RegionalID"].ToString()))
            {
                regionalId = AppGlobal.StrToLong(row["RegionalID"].ToString());
            }
            if (row.Table.Columns.Contains("SpaceID") && !string.IsNullOrEmpty(row["SpaceID"].ToString()))
            {
                spaceId = row["SpaceID"].ToString();
            }
            if (row.Table.Columns.Contains("Keywords") && !string.IsNullOrEmpty(row["Keywords"].ToString()))
            {
                keywords = row["Keywords"].ToString();
            }
            if (row.Table.Columns.Contains("PageSize") && !string.IsNullOrEmpty(row["PageSize"].ToString()))
            {
                pageSize = AppGlobal.StrToInt(row["PageSize"].ToString());
            }
            if (row.Table.Columns.Contains("PageIndex") && !string.IsNullOrEmpty(row["PageIndex"].ToString()))
            {
                pageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());
            }

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = @"SELECT a.EquipmentId,a.EquipmentName,b.SpaceName FROM Tb_Eq_Equipment a
                            LEFT JOIN Tb_Eq_Space b ON a.SpaceId=b.SpaceId 
                            WHERE isnull(a.IsDelete,0)=0 ";

                if (regionalId != 0 || !string.IsNullOrEmpty(spaceId))
                {
                    sql += $@" AND a.SpaceId IN
                            (
                                SELECT SpaceId FROM Tb_Eq_Space WHERE IncidentArea=convert(bigint,{regionalId}) 
                                    AND IncidentArea<>convert(bigint,0) AND IncidentArea<>'' AND isnull(IsDelete,0)=0
                                UNION
                                SELECT '{spaceId}' AS SpaceId
                            )";
                }

                if (!string.IsNullOrEmpty(keywords))
                {
                    sql += $" AND a.EquipmentName LIKE '%{keywords}%' ";
                }

                var data = GetListDapper(out int pageCount, out int count, sql, pageIndex, pageSize, "EquipmentName", 1, "EquipmentId", conn);

                var json = new ApiResult(true, data).toJson();
                json = json.Insert(json.Length - 1, ",\"PageCount:\":" + pageCount);

                return json;
            }
        }

        /// <summary>
        /// 公区方位、公区功能是否必填
        /// </summary>
        private string GetIncidentPublicConfig()
        {
            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = @"SELECT IsMustFile AS IsMustCheck FROM Tb_HSPR_IncidentPublicLocale_Config WHERE LocaleType='公区方位设置';
                            SELECT IsMustFile AS IsMustCheck FROM Tb_HSPR_IncidentPublicLocale_Config WHERE LocaleType='公区功能设置'";

                try
                {
                    var reader = conn.QueryMultiple(sql);
                    var _1 = reader.Read<int>().FirstOrDefault();
                    var _2 = reader.Read<int>().FirstOrDefault();

                    return new ApiResult(true, new { PublicPositionIsMustCheck = _1, PublicFunctionIsMustCheck = _2 }).toJson();
                }
                catch (Exception)
                {
                    return new ApiResult(true, new { PublicPositionIsMustCheck = 0, PublicFunctionIsMustCheck = 0 }).toJson();
                }
            }
        }
    }
}
