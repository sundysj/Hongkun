using Dapper;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using MobileSoft.Model.Unified;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Business
{
    /// <summary>
    /// 2017年6月16日09:50:28。谭洋
    /// </summary>
    public class Community : PubInfo
    {
        public Community()
        {
            base.Token = "20170616Community";
        }
        public override void Operate(ref Common.Transfer Trans)
        {
            Trans.Result = JSONHelper.FromString(false, "未知错误!");
            DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
            DataRow Row = dAttributeTable.Rows[0];
            switch (Trans.Command)
            {
                case "KeyDoors":// 小区门禁
                    Trans.Result = GetKeyDoorList(Row);
                    break;
                case "GetCommunityInfo":
                    Trans.Result = GetCommunityInfo(Row);
                    break;
                case "GetCommunityEntrance":
                    Trans.Result = GetCommunityEntrance(Row);
                    break;
                default:
                    break;
            }
        }

        private string GetCommunityEntrance(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return JSONHelper.FromString(false, "小区id不能为空");
            }

            //查询小区
            Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(row["CommunityId"].ToString());
            //构造链接字符串
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }

            using (var conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                var sql = @"SELECT TypeName FROM Tb_Community_EntranceType WHERE CommunityID=@Id";

                try
                {
                    var typeName = conn.Query<string>(sql, new { Id = Community.Id }).FirstOrDefault();
                    if (typeName != null)
                    {
                        return JSONHelper.FromString(true, typeName);
                    }

                    // 金帝
                    if (Community.CorpID == 2091 || Community.CorpID == 2104)
                    {
                        return JSONHelper.FromString(true, "令令");
                    }

                    // 俊发
                    if (Community.CorpID == 1985)
                    {

                    }

                    return JSONHelper.FromString(false, "该小区未设置门禁类别");
                }
                catch (System.Exception)
                {
                    return JSONHelper.FromString(false, "该小区未设置门禁类别");
                }
            }
        }

        /// <summary>
        /// 获取小区门禁列表  KeyDoors
        /// </summary>
        private string GetKeyDoorList(DataRow row)
        {
            if (!row.Table.Columns.Contains("UserID") || string.IsNullOrEmpty(row["UserID"].ToString()))
            {
                return JSONHelper.FromString(false, "用户id不能为空");
            }
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return JSONHelper.FromString(false, "小区id不能为空");
            }

            //查询小区
            Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(row["CommunityId"].ToString());
            //构造链接字符串
            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }

            using (var conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                // 查询用户绑定了的房屋
                string sql = @"SELECT RoomId FROM Tb_User_Relation WHERE ISNULL(Locked,0)=0 AND UserId=@UserId";

                IEnumerable<long> roomSet = conn.Query<long>(sql, new { UserId = row["UserID"].ToString() });

                // 有绑定房屋
                if (roomSet.Count() > 0)
                {
                    // 查询房屋所在楼栋单元及大门
                    using (IDbConnection conn2 = new SqlConnection(GetConnectionStringStr(Community)))
                    {
                        // 鸿坤排除商业用户
                        if (Community.CorpID == 1973)
                        {
                            sql = $@"SELECT RoomID FROM Tb_HSPR_Room WHERE PropertyUses='商业' AND RoomID IN({string.Join(",", roomSet)})";
                            var resultSet = conn2.Query<long>(sql);

                            roomSet = roomSet.Except(resultSet);

                            if (roomSet.Count() == 0)
                            {
                                return new ApiResult(false, "商业用户无开门权限").toJson();
                            }
                        }

                        sql = $@"SELECT CommID,DoorType,DoorName,DeviceAddRess FROM Tb_HSPR_KeyDoorDeviceSetting 
                                WHERE DoorType=2 AND CommID={Community.CommID} 
                                AND BuildSNum IN(SELECT BuildSNum FROM Tb_HSPR_Room WHERE RoomID IN({string.Join(",", roomSet)}))
                                AND UnitSNum IN(SELECT UnitSNum FROM Tb_HSPR_Room WHERE RoomID IN({string.Join(",", roomSet)}))
                                    UNION
                                SELECT CommID,DoorType,DoorName,DeviceAddRess FROM Tb_HSPR_KeyDoorDeviceSetting
                                WHERE DoorType=1 AND CommID=" + Community.CommID;

                        IEnumerable<dynamic> doorSet = conn2.Query(sql);

                        return new ApiResult(true, doorSet).toJson();
                    }
                }

                return JSONHelper.FromString(false, "未绑定房屋，无权限开门");
            }
        }

        private string GetCommunityInfo(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return JSONHelper.FromString(false, "小区id不能为空");
            }

            using (var conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                dynamic info = conn.Query(@"SELECT Id,Province,Area,City,CorpID,CommID,CorpName,CommName,Tel,CommunityImgUrl,ModuleRights FROM Tb_Community WHERE Id=@Id", new { Id = row["CommunityId"].ToString() }).FirstOrDefault();

                if (info != null)
                {
                    return new ApiResult(true, info).toJson();
                }
                return JSONHelper.FromString(false, "未查询到小区信息");
            }
        }
    }
}
