using Common;
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
    public class PMSAppDownload : PubInfo
    {
        public PMSAppDownload()
        {
            base.Token = "20200302PMSAppDownload";
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
                    case "GetAppDownloadUrl":
                        Trans.Result = GetAppDownloadUrl(Row);
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
        /// 获取App下载链接
        /// </summary>
        private string GetAppDownloadUrl(DataRow row)
        {
            if (!row.Table.Columns.Contains("AppID") || string.IsNullOrEmpty(row["AppID"].AsString()))
            {
                return new ApiResult(false, "缺少参数AppID").toJson();
            }
            if (!row.Table.Columns.Contains("Platform") || string.IsNullOrEmpty(row["Platform"].AsString()))
            {
                return new ApiResult(false, "缺少参数Platform").toJson();
            }

            var appId = row["AppID"].AsString();
            var platform = row["Platform"].AsString().ToLower();

            using (var conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                var sql = @"SELECT DownloadUrl FROM Tb_App_DownloadInfo WHERE IsDelete=0";

                if (platform == "ios") 
                    sql += " AND AppleBundleID=@AppID";
                if (platform == "android") 
                    sql += " AND AndroidPackageName=@AppID";

                var url = conn.Query<string>(sql, new { AppID = appId }).FirstOrDefault();
                if (url != null)
                {
                    return JSONHelper.FromString(true, url);
                }
                return JSONHelper.FromString(false, "还未配置App下载路径");
            }
        }
    }
}
