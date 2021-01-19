using Common;
using Dapper;
using DapperExtensions;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Business
{
    public partial class PMSRoomManage : PubInfo
    {
        public PMSRoomManage()
        {
            base.Token = "20200103PMSRoomManage";
        }

        public override void Operate(ref Transfer Trans)
        {
            Trans.Result = JSONHelper.FromString(false, "未知错误");

            DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
            DataRow Row = dAttributeTable.Rows[0];

            //验证登录
            if (!new Login().isLogin(ref Trans))
                return;

            switch (Trans.Command)
            {
                case "GetBuildingList":                     // 获取小区下楼宇列表
                    Trans.Result = GetBuildingList(Row);
                    break;
                case "GetUnitList":                         // 获取楼宇下单元列表
                    Trans.Result = GetBuildingUnitList(Row);
                    break;
                case "GetFloorList":                        // 获取单元内楼层列表
                    Trans.Result = GetUnitFloorList(Row);
                    break;
                case "GetRoomList":                         // 获取楼层内房屋列表
                    Trans.Result = GetFloorRoomList(Row);
                    break;
                case "GetBuildingTreeList":                 // 获取楼宇树形结构信息
                    Trans.Result = GetBuildingTreeList(Row);
                    break;
                case "GetRoomSimpleInfo":                   // 获取房产简单信息
                    Trans.Result = GetRoomSimpleInfo(Row);
                    break;
                case "GetRoomInfo":                         // 获取房产信息
                    Trans.Result = GetRoomInfo(Row);
                    break;
                case "SimpleRoomSearch":                    // 查询房产简单信息
                    Trans.Result = SimpleRoomSearch(Row);
                    break;
                case "GetRoomStateList":                    // 获取房屋状态类型列表
                    Trans.Result = GetRoomStateList(Row);
                    break;
            }
        }

        /// <summary>
        /// 获取小区下楼宇列表
        /// </summary>
        private string GetBuildingList(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }

            var commId = AppGlobal.StrToInt(row["CommID"].ToString());

            var sql = @"SELECT BuildName,BuildSNum FROM Tb_HSPR_Building 
                        WHERE CommID=@CommID AND isnull(IsDelete,0)=0 
                        ORDER BY right('0000000000'+BuildName,10);";

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var data = conn.Query(sql, new { CommID = commId });

                return new ApiResult(true, data).toJson();
            }
        }

        /// <summary>
        /// 获取楼宇下单元列表
        /// </summary>
        private string GetBuildingUnitList(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            if (!row.Table.Columns.Contains("BuildSNum") || string.IsNullOrEmpty(row["BuildSNum"].ToString()))
            {
                return JSONHelper.FromString(false, "楼宇编号不能为空");
            }

            var commId = AppGlobal.StrToInt(row["CommID"].ToString());
            var buildSNum = row["BuildSNum"].ToString();

            var sql = @"SELECT UnitName,UnitSNum FROM Tb_HSPR_Room 
                        WHERE CommID=@CommID AND BuildSNum=@BuildSNum AND isnull(IsDelete,0)=0 
                        GROUP BY UnitSNum,UnitName
                        ORDER BY right('0000000000'+convert(nvarchar(10),UnitSNum),10);";

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var data = conn.Query(sql, new { CommID = commId, BuildSNum = buildSNum });

                return new ApiResult(true, data).toJson();
            }
        }

        /// <summary>
        /// 获取单元内楼层列表
        /// </summary>
        private string GetUnitFloorList(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            if (!row.Table.Columns.Contains("BuildSNum") || string.IsNullOrEmpty(row["BuildSNum"].ToString()))
            {
                return JSONHelper.FromString(false, "楼宇编号不能为空");
            }
            if (!row.Table.Columns.Contains("UnitSNum") || string.IsNullOrEmpty(row["UnitSNum"].ToString()))
            {
                return JSONHelper.FromString(false, "单元编号不能为空");
            }

            var commId = AppGlobal.StrToInt(row["CommID"].ToString());
            var buildSNum = row["BuildSNum"].ToString();
            var unitSNum = row["UnitSNum"].ToString();

            var sql = @"SELECT FloorName,FloorSNum FROM Tb_HSPR_Room
                        WHERE CommID=@CommID AND BuildSNum=@BuildSNum AND UnitSNum=@UnitSNum AND isnull(IsDelete,0)=0
                        GROUP BY FloorSNum,FloorName
                        ORDER BY right('0000000000'+convert(nvarchar(10),FloorSNum),10)";

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var data = conn.Query(sql, new { CommID = commId, BuildSNum = buildSNum, UnitSNum = unitSNum });

                return new ApiResult(true, data).toJson();
            }
        }

        /// <summary>
        /// 获取楼层内房屋列表
        /// </summary>
        private string GetFloorRoomList(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            if (!row.Table.Columns.Contains("BuildSNum") || string.IsNullOrEmpty(row["BuildSNum"].ToString()))
            {
                return JSONHelper.FromString(false, "楼宇编号不能为空");
            }
            if (!row.Table.Columns.Contains("UnitSNum") || string.IsNullOrEmpty(row["UnitSNum"].ToString()))
            {
                return JSONHelper.FromString(false, "单元编号不能为空");
            }
            if (!row.Table.Columns.Contains("FloorSNum") || string.IsNullOrEmpty(row["FloorSNum"].ToString()))
            {
                return JSONHelper.FromString(false, "楼层编号不能为空");
            }

            var commId = AppGlobal.StrToInt(row["CommID"].ToString());
            var buildSNum = row["BuildSNum"].ToString();
            var unitSNum = row["UnitSNum"].ToString();
            var floorSNum = row["FloorSNum"].ToString();

            var sql = @"SELECT RoomID,RoomName,RoomSign,CustName,MobilePhone,CustID FROM view_HSPR_FloorRoom_Filter_Api
                        WHERE CommID=@CommID AND BuildSNum=@BuildSNum AND UnitSNum=@UnitSNum 
                        AND FloorSNum=@FloorSNum AND isnull(IsDelete,0)=0
                        ORDER BY RoomID;";

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var data = conn.Query(sql, new
                {
                    CommID = commId,
                    BuildSNum = buildSNum,
                    UnitSNum = unitSNum,
                    FloorSNum = floorSNum
                });

                return new ApiResult(true, data).toJson();
            }
        }

        /// <summary>
        /// 获取楼宇树形结构信息
        /// </summary>
        private string GetBuildingTreeList(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区ID不能为空");
            }

            var commId = AppGlobal.StrToInt(row["CommID"].ToString());

            var sql = @"SELECT BuildName,BuildSNum FROM Tb_HSPR_Building 
                        WHERE CommID=@CommID AND isnull(IsDelete,0)=0
                        GROUP BY BuildName,BuildSNum
                        ORDER BY right('0000000000'+BuildName,10);

                        SELECT UnitName,UnitSNum,BuildSNum FROM Tb_HSPR_Room 
                        WHERE CommID=@CommID AND UnitSNum IS NOT NULL AND isnull(IsDelete,0)=0
                        GROUP BY UnitName,UnitSNum,BuildSNum 
                        ORDER BY BuildSNum,right('0000000000'+convert(nvarchar(10),UnitSNum),10);

                        SELECT FloorName,FloorSNum,UnitSNum,BuildSNum FROM Tb_HSPR_Room 
                        WHERE CommID=@CommID AND UnitSNum IS NOT NULL AND FloorSNum IS NOT NULL AND isnull(IsDelete, 0)=0
                        GROUP BY FloorName,FloorSNum,UnitSNum,BuildSNum 
                        ORDER BY BuildSNum,UnitSNum,right('0000000000'+convert(nvarchar(10),FloorSNum),10);";


            using (var con = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var reader = con.QueryMultiple(sql, new { CommID = commId });

                var building = reader.Read();
                var unit = reader.Read();
                var floor = reader.Read().ToList();

                var tempBuilding = new List<object>();

                if (building.Count() > 0)
                {
                    // 楼栋
                    foreach (dynamic buildingItem in building)
                    {
                        var tempUnit = new List<object>();

                        // 单元
                        foreach (var unitItem in unit)
                        {
                            if (unitItem.BuildSNum == buildingItem.BuildSNum)
                            {
                                // 单元下楼层
                                var tempFloor = floor.FindAll(obj => (obj.BuildSNum == unitItem.BuildSNum && obj.UnitSNum == unitItem.UnitSNum));

                                var floors = tempFloor.Select(obj => new { FloorName = obj.FloorName, FloorSNum = obj.FloorSNum });

                                tempUnit.Add(new { UnitName = unitItem.UnitName, UnitSNum = unitItem.UnitSNum, Floors = floors });
                            }
                        }

                        tempBuilding.Add(new { BuildName = buildingItem.BuildName, BuildSNum = buildingItem.BuildSNum, Units = tempUnit });
                    }
                }

                return new ApiResult(true, tempBuilding).toJson();
            }
        }

        /// <summary>
        /// 获取房产简单信息
        /// </summary>
        private string GetRoomSimpleInfo(DataRow row)
        {
            if (!row.Table.Columns.Contains("RoomID") || string.IsNullOrEmpty(row["RoomID"].ToString()))
            {
                return JSONHelper.FromString(false, "房屋ID不能为空");
            }

            var roomId = AppGlobal.StrToLong(row["RoomID"].ToString());

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = @"SELECT a.RoomID,a.RoomName,a.RoomSign,b.StateName AS RoomState
                            FROM Tb_HSPR_Room a
                            LEFT JOIN Tb_HSPR_RoomState b ON a.RoomState=b.RoomState
                            WHERE a.RoomID=@RoomID";

                var data = conn.Query(sql, new { RoomID = roomId }).FirstOrDefault();
                if (data == null)
                {
                    return JSONHelper.FromString(false, "未查询到房屋信息");
                }

                return new ApiResult(true, data).toJson();
            }
        }

        /// <summary>
        /// 获取房产信息
        /// </summary>
        private string GetRoomInfo(DataRow row)
        {
            return null;
        }

        /// <summary>
        /// 查询房产简单信息
        /// </summary>
        private string SimpleRoomSearch(DataRow row)
        {
            return null;
        }

        /// <summary>
        /// 获取房屋状态类型列表
        /// </summary>
        private string GetRoomStateList(DataRow row)
        {
            using (var con = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = @"SELECT RoomState,StateName FROM Tb_HSPR_RoomState ORDER BY SortNum";

                return new ApiResult(true, con.Query(sql)).toJson();
            }
        }
    }
}
