using Dapper;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using MobileSoft.Model.Unified;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Business
{
    public class Entrance_JinDi : LingLingEntrance
    {
        public Entrance_JinDi()
        {
            base.Token = "20190528LingLingEntrance";

            LL_Signature = "7e321cf4-9f6f-43e4-b183-c460003829c3";
            LL_Token = "1552529343738";
            LL_OpenToken = "7A57DC4EFD8CC6E333E5CC2BFA11144A";
        }

        protected override string GetDeviceList(DataRow row)
        {
            if (!row.Table.Columns.Contains("UserID") || string.IsNullOrEmpty(row["UserID"].ToString()))
            {
                return JSONHelper.FromString(false, "用户id不能为空");
            }

            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区id不能为空");
            }

            var userId = row["UserID"].ToString();
            var commId = row["CommID"].ToString();

            using (var appConn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                var sql = @"SELECT * FROM Tb_Community WHERE Id=convert(nvarchar(36),@CommID) OR convert(nvarchar(36),CommID)=@CommID";
                var community = appConn.Query<Tb_Community>(sql, new { CommID = commId, UserId = userId }).FirstOrDefault();

                if (community == null)
                {
                    return JSONHelper.FromString(false, "未找到小区");
                }

                sql = @"SELECT RoomID FROM Tb_User_Relation WHERE CommunityId=@CommunityId AND UserId=@UserId AND isnull(Locked,0)=0";
                var rooms = appConn.Query<long>(sql, new { CommunityId = community.Id, UserId = userId });

                if (null == rooms || rooms.Count() <= 0)
                {
                    return JSONHelper.FromString(false, "您还未绑定房屋，请先绑定");
                }



                using (var erpConn = new SqlConnection(GetConnectionStr(community)))
                {
                    sql = @"SELECT count(1) FROM Tb_HSPR_KeyDoorDeviceSetting WHERE CommID=@CommID AND isnull(IsDelete,0)=0";
                    var count = erpConn.Query<int>(sql, new { CommID = commId }).FirstOrDefault();
                    if (count == 0)
                    {
                        return new ApiResult(false, "该小区暂未开通手机开门功能").toJson();
                    }
                    
                    sql = $@"SELECT DeviceId,DeviceName,DeviceCode FROM Tb_HSPR_KeyDoorDeviceSetting 
                        WHERE CommID=@CommID AND DoorType=1 AND isnull(IsDelete,0)=0
                        UNION
                        SELECT DeviceId,DeviceName,DeviceCode FROM Tb_HSPR_KeyDoorDeviceSetting a
                        WHERE CommID=@CommID AND DoorType=2 AND isnull(IsDelete,0)=0 AND isnull(RoomIDs,'')<>''
                        AND (SELECT count(1) FROM SplitString(a.RoomIDs,',',1) WHERE value IN({ string.Join(",", rooms) }))>0";

                    var resultSet = erpConn.Query(sql, new { CommID = community.CommID, UserId = userId, RoomIDList = rooms.ToArray() });
                    return new ApiResult(true, resultSet).toJson();
                }
            }
        }
    }
}
