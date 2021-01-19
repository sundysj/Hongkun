using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using MobileSoft.DBUtility;

namespace Business.PMS10.物管App.员工
{
    public class PMSUserIdentity
    {
        /// <summary>
        /// 是否房屋管家
        /// </summary>
        public static bool IsRoomHousekeeper(long roomId, string usercode, IDbConnection db)
        {
            if (roomId == 0)
                return false;

            var conn = db ?? new SqlConnection(PubConstant.hmWyglConnectionString);
            {
                var sql = @"SELECT UserCode FROM Tb_HSPR_Room WHERE RoomID=@RoomID AND UserCode=@UserCode
                            UNION ALL
                            SELECT UserCode FROM Tb_HSPR_RoomHousekeeper WHERE RoomID=@RoomID AND UserCode=@UserCode";

                var users = conn.Query<string>(sql, new { RoomID = roomId, UserCode = usercode });

                if (db == null)
                    conn.Dispose();

                return users.Count() > 0;
            }
        }

        /// <summary>
        /// 是否楼栋管家
        /// </summary>
        public static bool IsBuildHousekeeper(long roomId, string usercode, IDbConnection db)
        {
            if (roomId == 0)
                return false;

            var conn = db ?? new SqlConnection(PubConstant.hmWyglConnectionString);
            {
                var sql = @"DECLARE @CommID int;
                            SELECT @CommID=CommID FROM Tb_HSPR_Room WHERE RoomID=@RoomID;
                            SELECT UserCode FROM Tb_HSPR_BuildHousekeeper 
                            WHERE CommID=@CommID AND UserCode=@UserCode 
                            AND BuildSNum=(SELECT BuildSNum FROM Tb_HSPR_Room WHERE RoomID=@RoomID)";

                var users = conn.Query<string>(sql, new { RoomID = roomId, UserCode = usercode });

                if (db == null)
                    conn.Dispose();

                return users.Count() > 0;
            }
        }

        /// <summary>
        /// 是否公区管家
        /// </summary>
        public static bool IsRegionHousekeeper(long regionalId, string usercode, IDbConnection db)
        {
            var conn = db ?? new SqlConnection(PubConstant.hmWyglConnectionString);
            {
                var sql = @"SELECT UserCode FROM Tb_HSPR_IncidentRegional WHERE RegionalID=@RegionalID AND UserCode=@UserCode
                            UNION ALL
                            SELECT UserCode FROM Tb_HSPR_RegionHousekeeper WHERE RegionID=@RegionalID AND UserCode=@UserCode";

                var users = conn.Query<string>(sql, new { RegionalID = regionalId, UserCode = usercode });

                if (db == null)
                    conn.Dispose();

                return users.Count() > 0;
            }
        }
    }
}
