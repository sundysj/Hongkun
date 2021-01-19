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
    public class RoomManage : PubInfo
    {
        public RoomManage()
        {
            base.Token = "20170807RoomManage";
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
                case "GetBuildingList":
                    Trans.Result = GetBuildingList(Row);                // 获取小区下楼栋列表，包含单元，楼层信息
                    break;
                case "GetBuildingUnitList":
                    Trans.Result = GetBuildingUnitList(Row);            // 获取小区下楼栋及单元
                    break;
                case "GetBuildingListContainsRoom":
                    Trans.Result = GetBuildingListContainsRoom(Row);    // 获取小区下楼栋列表，包含单元，楼层、楼层下房屋及业主信息
                    break;

                case "GetFloorRoomList":
                    Trans.Result = GetFloorRoomList(Row);               // 获取该楼层下房屋列表
                    break;
                case "GetBuildingWithoutUnitFloor":
                    Trans.Result = GetBuildingWithoutUnitFloor(Row);    // 获取楼宇信息
                    break;
                case "GetUnitWithBuildSNum": 
                    Trans.Result = GetUnitWithBuildSNum(Row);           // 获取某楼宇单元信息
                    break;
                case "GetFloorsWithBuildSNumAndUnitNum":
                    Trans.Result = GetFloorsWithBuildSNumAndUnitNum(Row); // 获取某单元下楼层信息
                    break;

                /*
                 *  房屋开业、入住登记目前仅有华南城使用，主表Tb_HSPR_RoomStateChangeHis 
                 */
                case "GetRoomStateType":
                    Trans.Result = GetRoomStateType(Row);           // 获取房屋状态类别
                    break;
                case "QueryNoHistoryRoomList":
                    Trans.Result = QueryNoHistoryRoomList(Row);     // 查询未登记相关房屋状态信息的房屋
                    break;
                case "QueryRoomStateChangeHistory":
                    Trans.Result = QueryRoomStateChangeHistory(Row); // 房屋开业、入住历史记录查询
                    break;
                case "SaveRoomStateChange":
                    Trans.Result = SaveRoomStateChange(Row);        // 房屋开业、入住登记保存
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 获取小区下楼栋房屋列表
        /// </summary>
        private string GetBuildingList(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区ID不能为空");
            }

            var commId = AppGlobal.StrToInt(row["CommID"].ToString());

            var sql = @"SELECT BuildName,BuildSNum FROM Tb_HSPR_Building 
                        WHERE isnull(IsDelete,0)=0 AND CommID=@CommID ORDER BY BuildName;

                        SELECT DISTINCT UnitSNum, BuildSNum FROM Tb_HSPR_Room 
                        WHERE isnull(IsDelete,0)=0 AND CommID=@CommID ORDER BY BuildSNum, UnitSNum;

                        SELECT DISTINCT FloorSNum, UnitSNum, BuildSNum FROM Tb_HSPR_Room 
                        WHERE isnull(IsDelete, 0)=0 AND UnitSNum IS NOT NULL AND FloorSNum IS NOT NULL AND CommID=@CommID
                        ORDER BY BuildSNum, UnitSNum, FloorSNum;";


            using (var con = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var queryResult = con.QueryMultiple(sql, new { CommID = commId });

                var building = queryResult.Read();
                var unit = queryResult.Read();
                var floor = queryResult.Read();

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
                                IEnumerable<dynamic> tempFloor = floor.Where<dynamic>((u) => (u.BuildSNum == unitItem.BuildSNum && u.UnitSNum == unitItem.UnitSNum));
                                IEnumerable<dynamic> floorsList = tempFloor.Select(p => p.FloorSNum);

                                tempUnit.Add(new { UnitSNum = unitItem.UnitSNum, Floors = floorsList });
                            }
                        }

                        tempBuilding.Add(new { BuildName = buildingItem.BuildName, BuildSNum = buildingItem.BuildSNum, Unit = tempUnit });
                    }
                }

                return JSONHelper.FromString(tempBuilding);
            }
        }

        /// <summary>
        /// 获取小区下楼栋及单元
        /// </summary>
        private string GetBuildingUnitList(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区ID不能为空");
            }

            string commId = row["CommID"].ToString();

            string sql = $@"SELECT BuildName,BuildSNum FROM Tb_HSPR_Building 
                                WHERE isnull(IsDelete,0)=0 AND CommID={commId} ORDER BY BuildName;
                            SELECT DISTINCT UnitSNum,BuildSNum FROM Tb_HSPR_Room 
                                WHERE isnull(IsDelete,0)=0 AND CommID={commId} ORDER BY BuildSNum, UnitSNum;";

            using (IDbConnection con = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var queryResult = con.QueryMultiple(sql);

                var building = queryResult.Read();
                var unit = queryResult.Read();

                var tempBuilding = new List<object>();

                if (building.Count() > 0)
                {
                    // 楼栋
                    foreach (dynamic buildingItem in building)
                    {
                        // 单元
                        var tempUnit = unit.Where<dynamic>(u => (u.BuildSNum == buildingItem.BuildSNum));
                        //var tempUnitArray = new List<string>();
                        //foreach (var item in tempUnit)
                        //{
                        //    tempUnitArray.Add(item.UnitSNum);
                        //}

                        tempBuilding.Add(new { BuildName = buildingItem.BuildName, BuildSNum = buildingItem.BuildSNum, Unit = tempUnit });
                    }
                }

                return new ApiResult(true, tempBuilding).toJson();
            }
        }

        /// <summary>
        /// 获取小区下楼栋列表，包含单元，楼层、楼层下房屋及业主信息
        /// </summary>
        private string GetBuildingListContainsRoom(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区ID不能为空");
            }

            string commId = row["CommID"].ToString();

            string sql = string.Format(@"SELECT BuildName,BuildSNum FROM Tb_HSPR_Building 
                                            WHERE isnull(IsDelete,0)=0 AND CommID={0} ORDER BY BuildName;
                                        SELECT DISTINCT UnitSNum, BuildSNum FROM Tb_HSPR_Room 
                                            WHERE isnull(IsDelete,0)=0 AND CommID={0} ORDER BY BuildSNum, UnitSNum;
                                        SELECT DISTINCT FloorSNum, UnitSNum, BuildSNum FROM Tb_HSPR_Room WHERE isnull(IsDelete, 0)=0 
                                            AND UnitSNum IS NOT NULL AND FloorSNum IS NOT NULL AND CommID={0} ORDER BY BuildSNum, UnitSNum, FloorSNum;", commId);

            using (IDbConnection con = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var queryResult = con.QueryMultiple(sql);

                var building = queryResult.Read();
                var unit = queryResult.Read();
                var floor = queryResult.Read();

                var tempBuilding = new List<object>();

                var result = con.Query(@"SELECT a.RoomID,a.RoomSign,a.RoomName,b.CustID,c.CustName,c.MobilePhone,
                            a.BuildSNum,a.UnitSNum,a.FloorSNum,d.BuildName
                            FROM Tb_HSPR_Room a
                            LEFT JOIN Tb_HSPR_CustomerLive b ON a.RoomID=b.RoomID
                            LEFT JOIN Tb_HSPR_Customer c ON b.CustID=c.CustID
                            LEFT JOIN Tb_HSPR_Building d ON a.CommID=d.CommID AND d.BuildSNum=a.BuildSNum 
                            WHERE isnull(b.IsDelLive,0)=0 AND b.IsActive=1 AND isnull(c.IsDelete,0)=0 
                            AND isnull(a.IsDelete, 0)=0 AND a.CommID=@CommID", new { CommID = commId });

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
                                IEnumerable<dynamic> tempFloor = floor.Where<dynamic>(u => (u.BuildSNum == unitItem.BuildSNum && u.UnitSNum == unitItem.UnitSNum));
                                IEnumerable<dynamic> floorsList = tempFloor.Select((p) =>
                                {
                                    return new
                                    {
                                        FloorSNum = p.FloorSNum,
                                        Rooms = result.Where(r => r.BuildSNum == unitItem.BuildSNum && r.UnitSNum == unitItem.UnitSNum && r.FloorSNum == p.FloorSNum)
                                    };
                                });

                                tempUnit.Add(new { UnitSNum = unitItem.UnitSNum, Floors = floorsList });
                            }
                        }

                        tempBuilding.Add(new { BuildName = buildingItem.BuildName, BuildSNum = buildingItem.BuildSNum, Unit = tempUnit });
                    }
                }

                return new ApiResult(true, tempBuilding).toJson();
            }
        }

        /// <summary>
        /// 获取楼宇信息
        /// </summary>
        private string GetBuildingWithoutUnitFloor(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区ID不能为空");
            }

            string commId = row["CommID"].ToString();

            string sql = $@"SELECT BuildName,BuildSNum FROM Tb_HSPR_Building 
                            WHERE isnull(IsDelete,0)=0 AND CommID={commId} ORDER BY BuildName;";

            using (IDbConnection con = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                return new ApiResult(true, con.Query(sql)).toJson();
            }
        }

        /// <summary>
        /// 获取某楼宇单元信息
        /// </summary>
        private string GetUnitWithBuildSNum(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区ID不能为空");
            }
            if (!row.Table.Columns.Contains("BuildSNum") || string.IsNullOrEmpty(row["BuildSNum"].ToString()))
            {
                return JSONHelper.FromString(false, "楼宇编号不能为空");
            }

            string commId = row["CommID"].ToString();
            string BuildSNum = row["BuildSNum"].ToString();

            string sql = $@"SELECT DISTINCT UnitSNum FROM Tb_HSPR_Room 
                            WHERE isnull(IsDelete,0)=0 AND CommID={commId} AND BuildSNum='{BuildSNum}' ORDER BY UnitSNum;";

            using (IDbConnection con = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                return new ApiResult(true, con.Query<string>(sql)).toJson();
            }
        }

        /// <summary>
        /// 获取某单元下楼层信息
        /// </summary>
        private string GetFloorsWithBuildSNumAndUnitNum(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区ID不能为空");
            }
            if (!row.Table.Columns.Contains("BuildSNum") || string.IsNullOrEmpty(row["BuildSNum"].ToString()))
            {
                return JSONHelper.FromString(false, "楼宇编号不能为空");
            }
            if (!row.Table.Columns.Contains("UnitSNum") || string.IsNullOrEmpty(row["UnitSNum"].ToString()))
            {
                return JSONHelper.FromString(false, "单元编号不能为空");
            }

            string commId = row["CommID"].ToString();
            string BuildSNum = row["BuildSNum"].ToString();
            string UnitSNum = row["UnitSNum"].ToString();

            string sql = $@"SELECT DISTINCT FloorSNum FROM Tb_HSPR_Room 
                            WHERE isnull(IsDelete, 0)=0 AND UnitSNum IS NOT NULL AND FloorSNum IS NOT NULL AND 
                            CommID={commId} AND BuildSNum='{BuildSNum}' AND UnitSNum='{UnitSNum}' ORDER BY FloorSNum;";

            using (IDbConnection con = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                return new ApiResult(true, con.Query<string>(sql)).toJson();
            }
        }

        /// <summary>
        /// 获取该楼层下房屋列表
        /// </summary>
        private string GetFloorRoomList(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区ID不能为空");
            }

            if (!row.Table.Columns.Contains("BuildSNum") || string.IsNullOrEmpty(row["BuildSNum"].ToString()))
            {
                return JSONHelper.FromString(false, "楼栋不能为空");
            }

            if (!row.Table.Columns.Contains("UnitSNum") || string.IsNullOrEmpty(row["UnitSNum"].ToString()))
            {
                return JSONHelper.FromString(false, "单元不能为空");
            }

            //if (!row.Table.Columns.Contains("FloorNum") || string.IsNullOrEmpty(row["FloorNum"].ToString()))
            //{
            //    return JSONHelper.FromString(false, "楼层不能为空");
            //}

     

            string commId = row["CommID"].ToString();
            string buildSNum = row["BuildSNum"].ToString();
            string unitSNum = row["UnitSNum"].ToString();
            string floorNum = null;
            string sql;

            if (row.Table.Columns.Contains("FloorNum") && !string.IsNullOrEmpty(row["FloorNum"].ToString()))
            {
                floorNum = row["FloorNum"].ToString();
            }

            var roomInfo = ",a.RoomSign,a.RoomName";
            var paidService = "";
            if (Global_Var.LoginCorpID == "1329" && Global_Var.CorpName.Contains("金辉"))
            {
                roomInfo = @",  CASE 
	                            WHEN a.PaidServiceType<>'' AND a.PaidServiceType IS NOT NULL 
		                            AND a.PaidServiceStart<getdate() AND a.PaidServiceEnd>getdate()
	                            THEN isnull(a.RoomSign,'')+'('+e.DictionaryName+')'
	                            ELSE isnull(a.RoomSign,'') END AS RoomSign,
	                            CASE 
	                            WHEN a.PaidServiceType<>'' AND a.PaidServiceType IS NOT NULL 
		                            AND a.PaidServiceStart<getdate() AND a.PaidServiceEnd>getdate()
	                            THEN isnull(a.RoomName,'')+'('+e.DictionaryName+')'
	                            ELSE isnull(a.RoomName,'') END AS RoomName";
                paidService = @" LEFT JOIN Tb_Dictionary_PaidServiceType e ON a.PaidServiceType=e.DictionaryCode ";
            }

            if (floorNum != null)
            {
                sql = $@"SELECT a.RoomID { roomInfo },b.CustID,c.CustName,c.MobilePhone,
                            a.BuildSNum,a.UnitSNum,a.FloorSNum,d.BuildName, 
                            convert(NVARCHAR(20),a.ContSubDate,20) AS ContSubDate 
                        FROM Tb_HSPR_Room a
                        LEFT JOIN Tb_HSPR_CustomerLive b ON a.RoomID=b.RoomID
                        LEFT JOIN Tb_HSPR_Customer c ON b.CustID=c.CustID
                        LEFT JOIN Tb_HSPR_Building d ON a.CommID=d.CommID AND d.BuildSNum=a.BuildSNum 
                        { paidService }
                        WHERE isnull(b.IsDelLive,0)=0 AND b.IsActive=1 AND isnull(c.IsDelete,0)=0 AND isnull(a.IsDelete, 0)=0 
                        AND a.CommID=@CommID AND a.BuildSNum=@BuildSNum AND ltrim(rtrim(a.UnitSNum))=@UnitSNum 
                        AND a.FloorSNum=@FloorSNum";
            }
            else
            {
                sql = $@"SELECT a.RoomID { roomInfo },b.CustID,c.CustName,c.MobilePhone,
                            a.BuildSNum,a.UnitSNum,a.FloorSNum,d.BuildName, 
                            convert(NVARCHAR(20),a.ContSubDate,20) AS ContSubDate 
                        FROM Tb_HSPR_Room a
                        LEFT JOIN Tb_HSPR_CustomerLive b ON a.RoomID=b.RoomID
                        LEFT JOIN Tb_HSPR_Customer c ON b.CustID=c.CustID
                        LEFT JOIN Tb_HSPR_Building d ON a.CommID=d.CommID AND d.BuildSNum=a.BuildSNum 
                        { paidService }
                        WHERE isnull(b.IsDelLive,0)=0 AND b.IsActive=1 AND isnull(c.IsDelete,0)=0 AND isnull(a.IsDelete, 0)=0 
                        AND a.CommID=@CommID AND a.BuildSNum=@BuildSNum AND ltrim(rtrim(a.UnitSNum))=@UnitSNum";
            }



            // 中集
            //if (Global_Var.LoginCorpID == "1953")
            //{
            //    sql = @"SELECT a.RoomID,a.RoomSign,a.RoomName,b.CustID,c.CustName,c.MobilePhone,
            //            a.BuildSNum,a.UnitSNum,a.FloorSNum,d.BuildName, 
            //            convert(NVARCHAR(20),a.ContSubDate,20) AS ContSubDate 
            //            FROM Tb_HSPR_Room a
            //            LEFT JOIN Tb_HSPR_CustomerLive b ON a.RoomID=b.RoomID
            //            LEFT JOIN Tb_HSPR_Customer c ON b.CustID=c.CustID
            //            LEFT JOIN Tb_HSPR_Building d ON a.CommID=d.CommID AND d.BuildSNum=a.BuildSNum 
            //            WHERE isnull(b.IsDelLive,0)=0 AND b.IsActive=1 AND isnull(c.IsDelete,0)=0 AND isnull(a.IsDelete, 0)=0 
            //            AND a.CommID=@CommID AND a.BuildSNum=@BuildSNum AND ltrim(rtrim(a.UnitSNum))=@UnitSNum 
            //            AND a.FloorSNum=@FloorSNum";
            //}

            using (IDbConnection con = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                return JSONHelper.FromString(con.ExecuteReader(sql, new
                {
                    CommID = commId,
                    BuildSNum = buildSNum,
                    UnitSNum = unitSNum,
                    FloorSNum = floorNum
                }).ToDataSet().Tables[0]);
            }
        }


        /// <summary>
        /// 获取房屋状态类别
        /// </summary>
        private string GetRoomStateType(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区ID不能为空");
            }

            string commId = row["CommID"].ToString();

            using (IDbConnection con = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                //string sql = @"SELECT RoomState, StateName FROM Tb_HSPR_RoomState WHERE RoomState IN (
                //                SELECT RoomState FROM Tb_HSPR_RoomStateComm WHERE CommID=@CommID GROUP BY RoomState) 
                //                ORDER BY SortNum";

                string sql = @"SELECT RoomState, StateName FROM Tb_HSPR_RoomState ORDER BY SortNum";

                return new ApiResult(true, con.Query(sql)).toJson();
            }
        }

        /// <summary>
        /// 开业登记查询
        /// </summary>
        private string QueryNoHistoryRoomList(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区ID不能为空");
            }

            if (!row.Table.Columns.Contains("PageSize") || string.IsNullOrEmpty(row["PageSize"].ToString()))
            {
                return JSONHelper.FromString(false, "页长不能为空");
            }

            if (!row.Table.Columns.Contains("PageIndex") || string.IsNullOrEmpty(row["PageIndex"].ToString()))
            {
                return JSONHelper.FromString(false, "页码不能为空");
            }

            string CommID = row["CommID"].ToString();
            int PageSize = AppGlobal.StrToInt(row["PageSize"].ToString());
            int PageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());

            string sql = $@"SELECT a.CommID,d.CommName,a.RoomID,a.RoomSign,a.RoomName,a.RoomState,b.StateName AS RoomStateName,
                            c.BuildName,a.BuildSNum,a.UnitSNum,a.FloorSNum 
                            FROM Tb_HSPR_Room a 
                            LEFT JOIN Tb_HSPR_RoomState b ON a.RoomState=b.RoomState
                            LEFT JOIN Tb_HSPR_Building c ON a.BuildSNum=c.BuildSNum AND a.CommID=c.CommID 
                            LEFT JOIN Tb_HSPR_Community d ON a.CommID=d.CommID 
                            WHERE a.CommID={CommID}";
            if (row.Table.Columns.Contains("BuildSNum") && !string.IsNullOrEmpty(row["BuildSNum"].ToString()))
            {
                sql += $" AND convert(nvarchar(50),a.BuildSNum)='{row["BuildSNum"].ToString()}'";
            }
            if (row.Table.Columns.Contains("UnitSNum") && !string.IsNullOrEmpty(row["UnitSNum"].ToString()))
            {
                sql += $" AND convert(nvarchar(50),a.UnitSNum)='{row["UnitSNum"].ToString()}'";
            }
            if (row.Table.Columns.Contains("FloorSNum") && !string.IsNullOrEmpty(row["FloorSNum"].ToString()))
            {
                sql += $" AND convert(nvarchar(50),a.FloorSNum)='{row["FloorSNum"].ToString()}'";
            }
            if (row.Table.Columns.Contains("RoomID") && !string.IsNullOrEmpty(row["RoomID"].ToString()))
            {
                sql += $" AND a.RoomID={row["RoomID"].ToString()}";
            }
            if (row.Table.Columns.Contains("RoomState") && !string.IsNullOrEmpty(row["RoomState"].ToString()))
            {
                string roomStateStr = row["RoomState"].ToString();
                string[] roomStateArr = roomStateStr.Split(',');
                string[] roomStateSqlArr = new string[roomStateArr.Length];

                for (int i = 0; i < roomStateArr.Length; i++)
                {
                    roomStateSqlArr[i] = $"a.RoomState='{roomStateArr[i]}'";
                }

                sql += " AND " + string.Join(" OR ", roomStateSqlArr);
            }

            string StartTime = null;
            string EndTime = null;
            if (row.Table.Columns.Contains("StartTime") && !string.IsNullOrEmpty(row["StartTime"].ToString()))
            {
                StartTime = row["StartTime"].ToString();
            }

            if (row.Table.Columns.Contains("EndTime") && !string.IsNullOrEmpty(row["EndTime"].ToString()))
            {
                EndTime = row["EndTime"].ToString();
            }

            if (!string.IsNullOrEmpty(StartTime) && !string.IsNullOrEmpty(EndTime))
            {
                sql += $" AND RoomID NOT IN(SELECT DISTINCT RoomID FROM Tb_HSPR_RoomStateChangeHistory WHERE 1=1";

                if (!string.IsNullOrEmpty(StartTime))
                {
                    sql += $" AND ChangeDate>='{StartTime}'";
                }

                if (!string.IsNullOrEmpty(EndTime))
                {
                    sql += $" AND ChangeDate<='{EndTime}'";
                }
                sql += ")";
            }

            DataTable dataTable = GetList(out int pageCount, out int count, sql, PageIndex, PageSize, "RoomID", 0,
                "RoomID", PubConstant.hmWyglConnectionString).Tables[0];

            string result = JSONHelper.FromString(dataTable);
            result = result.Insert(result.Length - 1, ",\"PageCount\":" + pageCount);

            return result;
        }

        /// <summary>
        /// 房屋开业、入住历史记录
        /// </summary>
        private string QueryRoomStateChangeHistory(DataRow row)
        {
            if (!row.Table.Columns.Contains("RoomID") || string.IsNullOrEmpty(row["RoomID"].ToString()))
            {
                return JSONHelper.FromString(false, "房屋ID不能为空");
            }

            if (!row.Table.Columns.Contains("PageSize") || string.IsNullOrEmpty(row["PageSize"].ToString()))
            {
                return JSONHelper.FromString(false, "页长不能为空");
            }

            if (!row.Table.Columns.Contains("PageIndex") || string.IsNullOrEmpty(row["PageIndex"].ToString()))
            {
                return JSONHelper.FromString(false, "页码不能为空");
            }

            string roomId = row["RoomID"].ToString();
            int PageSize = AppGlobal.StrToInt(row["PageSize"].ToString());
            int PageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());

            string sql = $@"SELECT a.IID,a.RoomState,a.ChangeDate,a.AttachmentFile,a.Remark,a.OperateDate,
                            (SELECT UserName FROM Tb_Sys_User x WHERE x.UserCode=a.OperateUserCode) AS OperateUserName,
                            b.RoomName,b.RoomSign,c.CommName,d.BuildName,b.BuildSNum,b.UnitSNum,b.FloorSNum  
                            FROM Tb_HSPR_RoomStateChangeHistory a  
                            LEFT JOIN Tb_HSPR_Room b ON a.RoomID=b.RoomID
                            LEFT JOIN Tb_HSPR_Community c ON b.CommID=c.CommID
                            LEFT JOIN Tb_HSPR_Building d ON b.BuildSNum=d.BuildSNum AND b.CommID=d.CommID
                            WHERE a.RoomID={roomId}";

            DataTable dataTable = GetList(out int pageCount, out int count, sql, PageIndex, PageSize, "OperateDate", 1, "IID",
                PubConstant.hmWyglConnectionString).Tables[0];

            string result = JSONHelper.FromString(true, dataTable);

            result = result.Insert(result.Length - 1, ",\"PageCount\":" + pageCount);
            return result;
        }

        /// <summary>
        /// 房屋开业、入住登记信息保存
        /// </summary>
        private string SaveRoomStateChange(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区ID不能为空");
            }
            if (!row.Table.Columns.Contains("RoomID") || string.IsNullOrEmpty(row["RoomID"].ToString()))
            {
                return JSONHelper.FromString(false, "房屋ID不能为空");
            }
            if (!row.Table.Columns.Contains("RoomState") || string.IsNullOrEmpty(row["RoomState"].ToString()))
            {
                return JSONHelper.FromString(false, "房屋状态不能为空");
            }

            string CommID = row["CommID"].ToString();
            string roomId = row["RoomID"].ToString();
            string roomState = row["RoomState"].ToString();

            string Remark = null;
            if (row.Table.Columns.Contains("Remark") && !string.IsNullOrEmpty(row["Remark"].ToString()))
            {
                Remark = row["Remark"].ToString();
            }
            string AttachmentFile = null;
            if (row.Table.Columns.Contains("AttachmentFile") && !string.IsNullOrEmpty(row["AttachmentFile"].ToString()))
            {
                AttachmentFile = row["AttachmentFile"].ToString();
            }
            string ChangeDate = null;
            if (row.Table.Columns.Contains("ChangeDate") && !string.IsNullOrEmpty(row["ChangeDate"].ToString()))
            {
                ChangeDate = row["ChangeDate"].ToString();
            }
            if (string.IsNullOrEmpty(Remark) && string.IsNullOrEmpty(AttachmentFile) && string.IsNullOrEmpty(ChangeDate))
            {
                return JSONHelper.FromString(false, "登记信息不能为空");
            }

            using (IDbConnection con = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                string sql = @"INSERT INTO Tb_HSPR_RoomStateChangeHistory(IID, CommID, RoomID, RoomState, ChangeDate, 
                                Remark, AttachmentFile, OperateDate, OperateUserCode)
                                VALUES (newid(), @CommID, @RoomID, @RoomState, @ChangeDate, @Remark, @AttachmentFile,
                                getdate(), @OperateUserCode)";

                int i = con.Execute(sql, new
                {
                    CommID = CommID,
                    RoomID = roomId,
                    RoomState = roomState,
                    ChangeDate = ChangeDate,
                    Remark = Remark,
                    AttachmentFile = AttachmentFile,
                    OperateUserCode = Global_Var.LoginUserCode
                });

                // 暂时不处理，后面看客户是否需要同步状态再处理
                if (false)
                {
                    // 更改主表信息
                    con.Execute("UPDATE Tb_HSPR_Room SET RoomState=@RoomState WHERE RoomID=@RoomID",
                        new
                        {
                            RoomID = roomId,
                            RoomState = roomState
                        });

                    // 更改房屋状态变更历史
                    con.Execute("");
                }

                return new ApiResult(true, "保存成功").toJson();
            }
        }
    }
}
