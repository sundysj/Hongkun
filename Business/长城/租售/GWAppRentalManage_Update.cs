using Dapper;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public partial class GWAppRentalManage
    {
        /// <summary>
        /// 更新出租信息
        /// </summary>
        private string UpdateRentInfo(DataRow row)
        {
            #region 参数校验
            if (!row.Table.Columns.Contains("Id") || string.IsNullOrEmpty(row["Id"].AsString()))
            {
                return new ApiResult(false, "缺少参数Id").toJson();
            }
            if (!row.Table.Columns.Contains("UserId") || string.IsNullOrEmpty(row["UserId"].AsString()))
            {
                return new ApiResult(false, "缺少参数UserId").toJson();
            }
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].AsString()))
            {
                return new ApiResult(false, "缺少参数CommunityId").toJson();
            }
            if (!row.Table.Columns.Contains("Address") || string.IsNullOrEmpty(row["Address"].AsString()))
            {
                return new ApiResult(false, "缺少参数Address").toJson();
            }
            if (!row.Table.Columns.Contains("Title") || string.IsNullOrEmpty(row["Title"].AsString()))
            {
                return new ApiResult(false, "缺少参数Title").toJson();
            }
            if (!row.Table.Columns.Contains("LivingRooms") || string.IsNullOrEmpty(row["LivingRooms"].AsString()))
            {
                return new ApiResult(false, "缺少参数LivingRooms").toJson();
            }
            if (!row.Table.Columns.Contains("BedRooms") || string.IsNullOrEmpty(row["BedRooms"].AsString()))
            {
                return new ApiResult(false, "缺少参数BedRooms").toJson();
            }
            if (!row.Table.Columns.Contains("BathRooms") || string.IsNullOrEmpty(row["BathRooms"].AsString()))
            {
                return new ApiResult(false, "缺少参数BathRooms").toJson();
            }
            if (!row.Table.Columns.Contains("KitchenRooms") || string.IsNullOrEmpty(row["KitchenRooms"].AsString()))
            {
                return new ApiResult(false, "缺少参数KitchenRooms").toJson();
            }
            if (!row.Table.Columns.Contains("Orientation") || string.IsNullOrEmpty(row["Orientation"].AsString()))
            {
                return new ApiResult(false, "缺少参数Orientation").toJson();
            }
            if (!row.Table.Columns.Contains("BuildingArea") || string.IsNullOrEmpty(row["BuildingArea"].AsString()))
            {
                return new ApiResult(false, "缺少参数BuildingArea").toJson();
            }
            if (!row.Table.Columns.Contains("BuildingType") || string.IsNullOrEmpty(row["BuildingType"].AsString()))
            {
                return new ApiResult(false, "缺少参数BuildingType").toJson();
            }
            if (!row.Table.Columns.Contains("Floor") || string.IsNullOrEmpty(row["Floor"].AsString()))
            {
                return new ApiResult(false, "缺少参数Floor").toJson();
            }
            if (!row.Table.Columns.Contains("FloorCount") || string.IsNullOrEmpty(row["FloorCount"].AsString()))
            {
                return new ApiResult(false, "缺少参数FloorCount").toJson();
            }
            if (!row.Table.Columns.Contains("BussType") || string.IsNullOrEmpty(row["BussType"].AsString()))
            {
                return new ApiResult(false, "缺少参数BussType").toJson();
            }
            if (!row.Table.Columns.Contains("Renovation") || string.IsNullOrEmpty(row["Renovation"].AsString()))
            {
                return new ApiResult(false, "缺少参数Renovation").toJson();
            }
            if (!row.Table.Columns.Contains("Amount") || string.IsNullOrEmpty(row["Amount"].AsString()))
            {
                return new ApiResult(false, "缺少参数Amount").toJson();
            }
            if (!row.Table.Columns.Contains("Linkman") || string.IsNullOrEmpty(row["Linkman"].AsString()))
            {
                return new ApiResult(false, "缺少参数Linkman").toJson();
            }
            if (!row.Table.Columns.Contains("LinkmanTel") || string.IsNullOrEmpty(row["LinkmanTel"].AsString()))
            {
                return new ApiResult(false, "缺少参数LinkmanTel").toJson();
            }
            if (!row.Table.Columns.Contains("LinkmanSex") || string.IsNullOrEmpty(row["LinkmanSex"].AsString()))
            {
                return new ApiResult(false, "缺少参数LinkmanSex").toJson();
            }
            if (!row.Table.Columns.Contains("HasElevator") || string.IsNullOrEmpty(row["HasElevator"].AsString()))
            {
                return new ApiResult(false, "缺少参数HasElevator").toJson();
            }
            if (!row.Table.Columns.Contains("Imgs") || string.IsNullOrEmpty(row["Imgs"].AsString()))
            {
                return new ApiResult(false, "缺少参数Imgs").toJson();
            }

            #endregion-
            var id = row["Id"].ToString();
            var title = row["Title"].ToString();
            var userId = row["UserId"].ToString();
            var communityId = row["CommunityId"].ToString();
            var address = row["Address"].ToString();

            var livingRooms = AppGlobal.StrToInt(row["LivingRooms"].ToString());
            var bedRooms = AppGlobal.StrToInt(row["BedRooms"].ToString());
            var bathRooms = AppGlobal.StrToInt(row["BathRooms"].ToString());
            var kitchenRooms = AppGlobal.StrToInt(row["KitchenRooms"].ToString());

            var buildingType = row["BuildingType"].ToString();
            var buildingArea = AppGlobal.StrToDec(row["BuildingArea"].ToString());
            var orientation = row["Orientation"].ToString();
            var floor = AppGlobal.StrToInt(row["Floor"].ToString());
            var floorCount = AppGlobal.StrToInt(row["FloorCount"].ToString());
            var bussType = row["BussType"].ToString();
            var renovation = row["Renovation"].ToString();
            var amount = AppGlobal.StrToDec(row["Amount"].ToString());
            var linkman = row["Linkman"].ToString();
            var linkmanTel = row["LinkmanTel"].ToString();
            var linkmanSex = AppGlobal.StrToInt(row["LinkmanSex"].ToString());
            var hasElevator = AppGlobal.StrToInt(row["HasElevator"].ToString());
            var imgs = row["Imgs"].ToString();

            var description = default(string);
            if (row.Table.Columns.Contains("Description") && !string.IsNullOrEmpty(row["Description"].AsString()))
            {
                description = row["Description"].AsString();
            }

            var community = GetCommunity(communityId);
            if (community == null)
            {
                return JSONHelper.FromString(false, "未查询到小区信息");
            }

            using (var conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                conn.Open();

                var trans = conn.BeginTransaction();

                try
                {
                    var sql = @"UPDATE Tb_Rental
                                SET CommunityId=@CommunityId,Address=@Address,UserId=@UserId,Title=@Title,
                                    BussType=@BussType,City=@City,Area=@Area,BuildingArea=@BuildingArea,
                                    BuildingType=@BuildingType,Floor=@Floor,FloorCount=@FloorCount,
                                    LivingRooms=@LivingRooms,BedRooms=@BedRooms,BathRooms=@BathRooms,
                                    KitchenRooms=@KitchenRooms,Orientation=@Orientation,Renovation=@Renovation,
                                    Amount=@Amount,Description=@Description,Img=@Img,LinkMan=@LinkMan,
                                    LinkManTel=@LinkManTel,LinkManSex=@LinkManSex,
                                    PubDate=getdate(),HasElevator=@HasElevator
                                WHERE Id=@Id";

                    var i = conn.Execute(sql, new
                    {
                        Id = id,
                        CommunityId = community.Id,
                        Address = address,
                        UserId = userId,
                        Title = title,
                        BussType = bussType,
                        City = community.City,
                        Area = community.Area,
                        LivingRooms = livingRooms,
                        BedRooms = bedRooms,
                        BathRooms = bathRooms,
                        KitchenRooms = kitchenRooms,
                        BuildingType = buildingType,
                        BuildingArea = buildingArea,
                        Floor = floor,
                        FloorCount = floorCount,
                        Orientation = orientation,
                        Renovation = renovation,
                        Amount = amount,
                        Description = description,
                        Img = imgs,
                        LinkMan = linkman,
                        LinkManTel = linkmanTel,
                        LinkManSex = linkmanSex,
                        HasElevator = hasElevator
                    }, trans);

                    if (i == 1)
                    {
                        //删除标签信息

                        sql = @"DELETE Tb_Rental_RoomTag  WHERE RentalID=@RentalID";
                        conn.Execute(sql, new
                        {
                            RentalID = id
                        }, trans);

                        // 读取标签信息
                        var tags = (JArray)JsonConvert.DeserializeObject(row["Tags"].ToString());

                        foreach (var item in tags)
                        {
                            sql = @"INSERT INTO Tb_Rental_RoomTag(RentalID,CategoryID,Tags)
                                    VALUES(@RentalID,@CategoryID,@Tags);";

                            conn.Execute(sql, new
                            {
                                RentalID = id,
                                CategoryID = item["ID"].ToString(),
                                Tags = string.Join(",", ((JArray)item["Tags"]).ToArray().Select(obj => obj.ToString()))
                            }, trans);
                        }
                    }

                    trans.Commit();
                    return JSONHelper.FromString(true, "修改成功");
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    return JSONHelper.FromString(false, ex.Message);
                }
            }
        }

        /// <summary>
        /// 更新出售信息
        /// </summary>
        private string UpdateSaleInfo(DataRow row)
        {
            #region 参数校验

            if (!row.Table.Columns.Contains("Id") || string.IsNullOrEmpty(row["Id"].AsString()))
            {
                return new ApiResult(false, "缺少参数Id").toJson();
            }
            if (!row.Table.Columns.Contains("UserId") || string.IsNullOrEmpty(row["UserId"].AsString()))
            {
                return new ApiResult(false, "缺少参数UserId").toJson();
            }
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].AsString()))
            {
                return new ApiResult(false, "缺少参数CommunityId").toJson();
            }
            if (!row.Table.Columns.Contains("Address") || string.IsNullOrEmpty(row["Address"].AsString()))
            {
                return new ApiResult(false, "缺少参数Address").toJson();
            }
            if (!row.Table.Columns.Contains("Title") || string.IsNullOrEmpty(row["Title"].AsString()))
            {
                return new ApiResult(false, "缺少参数Title").toJson();
            }
            if (!row.Table.Columns.Contains("LivingRooms") || string.IsNullOrEmpty(row["LivingRooms"].AsString()))
            {
                return new ApiResult(false, "缺少参数LivingRooms").toJson();
            }
            if (!row.Table.Columns.Contains("BedRooms") || string.IsNullOrEmpty(row["BedRooms"].AsString()))
            {
                return new ApiResult(false, "缺少参数BedRooms").toJson();
            }
            if (!row.Table.Columns.Contains("BathRooms") || string.IsNullOrEmpty(row["BathRooms"].AsString()))
            {
                return new ApiResult(false, "缺少参数BathRooms").toJson();
            }
            if (!row.Table.Columns.Contains("KitchenRooms") || string.IsNullOrEmpty(row["KitchenRooms"].AsString()))
            {
                return new ApiResult(false, "缺少参数KitchenRooms").toJson();
            }
            if (!row.Table.Columns.Contains("Orientation") || string.IsNullOrEmpty(row["Orientation"].AsString()))
            {
                return new ApiResult(false, "缺少参数Orientation").toJson();
            }
            if (!row.Table.Columns.Contains("BuildingAge") || string.IsNullOrEmpty(row["BuildingAge"].AsString()))
            {
                return new ApiResult(false, "缺少参数BuildingAge").toJson();
            }
            if (!row.Table.Columns.Contains("BuildingArea") || string.IsNullOrEmpty(row["BuildingArea"].AsString()))
            {
                return new ApiResult(false, "缺少参数BuildingArea").toJson();
            }
            if (!row.Table.Columns.Contains("BuildingType") || string.IsNullOrEmpty(row["BuildingType"].AsString()))
            {
                return new ApiResult(false, "缺少参数BuildingType").toJson();
            }
            if (!row.Table.Columns.Contains("TranRoomTime") || string.IsNullOrEmpty(row["TranRoomTime"].AsString()))
            {
                return new ApiResult(false, "缺少参数TranRoomTime").toJson();
            }
            if (!row.Table.Columns.Contains("Floor") || string.IsNullOrEmpty(row["Floor"].AsString()))
            {
                return new ApiResult(false, "缺少参数Floor").toJson();
            }
            if (!row.Table.Columns.Contains("FloorCount") || string.IsNullOrEmpty(row["FloorCount"].AsString()))
            {
                return new ApiResult(false, "缺少参数FloorCount").toJson();
            }
            if (!row.Table.Columns.Contains("Renovation") || string.IsNullOrEmpty(row["Renovation"].AsString()))
            {
                return new ApiResult(false, "缺少参数Renovation").toJson();
            }
            if (!row.Table.Columns.Contains("Amount") || string.IsNullOrEmpty(row["Amount"].AsString()))
            {
                return new ApiResult(false, "缺少参数Amount").toJson();
            }
            if (!row.Table.Columns.Contains("Linkman") || string.IsNullOrEmpty(row["Linkman"].AsString()))
            {
                return new ApiResult(false, "缺少参数Linkman").toJson();
            }
            if (!row.Table.Columns.Contains("LinkmanTel") || string.IsNullOrEmpty(row["LinkmanTel"].AsString()))
            {
                return new ApiResult(false, "缺少参数LinkmanTel").toJson();
            }
            if (!row.Table.Columns.Contains("LinkmanSex") || string.IsNullOrEmpty(row["LinkmanSex"].AsString()))
            {
                return new ApiResult(false, "缺少参数LinkmanSex").toJson();
            }
            if (!row.Table.Columns.Contains("HasElevator") || string.IsNullOrEmpty(row["HasElevator"].AsString()))
            {
                return new ApiResult(false, "缺少参数HasElevator").toJson();
            }
            if (!row.Table.Columns.Contains("Imgs") || string.IsNullOrEmpty(row["Imgs"].AsString()))
            {
                return new ApiResult(false, "缺少参数Imgs").toJson();
            }

            #endregion

            var id = row["Id"].ToString();
            var title = row["Title"].ToString();
            var userId = row["UserId"].ToString();
            var communityId = row["CommunityId"].ToString();
            var address = row["Address"].ToString();

            var livingRooms = AppGlobal.StrToInt(row["LivingRooms"].ToString());
            var bedRooms = AppGlobal.StrToInt(row["BedRooms"].ToString());
            var bathRooms = AppGlobal.StrToInt(row["BathRooms"].ToString());
            var kitchenRooms = AppGlobal.StrToInt(row["KitchenRooms"].ToString());

            var buildingAge = AppGlobal.StrToInt(row["BuildingAge"].ToString());
            var buildingType = row["BuildingType"].ToString();
            var buildingArea = AppGlobal.StrToDec(row["BuildingArea"].ToString());
            var tranRoomTime = AppGlobal.StrToDate(row["TranRoomTime"].ToString());
            var orientation = row["Orientation"].ToString();
            var floor = AppGlobal.StrToInt(row["Floor"].ToString());
            var floorCount = AppGlobal.StrToInt(row["FloorCount"].ToString());
            var bussType = "出售";
            var renovation = row["Renovation"].ToString();
            var amount = AppGlobal.StrToDec(row["Amount"].ToString());
            var linkman = row["Linkman"].ToString();
            var linkmanTel = row["LinkmanTel"].ToString();
            var linkmanSex = AppGlobal.StrToInt(row["LinkmanSex"].ToString());
            var hasElevator = AppGlobal.StrToInt(row["HasElevator"].ToString());
            var imgs = row["Imgs"].ToString();

            var description = default(string);
            if (row.Table.Columns.Contains("Description") && !string.IsNullOrEmpty(row["Description"].AsString()))
            {
                description = row["Description"].AsString();
            }

            var community = GetCommunity(communityId);
            if (community == null)
            {
                return JSONHelper.FromString(false, "未查询到小区信息");
            }

            using (var conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                conn.Open();

                var trans = conn.BeginTransaction();

                try
                {
                    var sql = @"UPDATE  Tb_Rental
                                    SET CommunityId=@CommunityId,Address=@Address,UserId=@UserId,Title=@Title,
                                    BussType=@BussType,City=@City,Area=@Area, BuildingAge=@BuildingAge, 
                                    BuildingArea=@BuildingArea,BuildingType=@BuildingType,Floor=@Floor,FloorCount=@FloorCount,
                                    LivingRooms=@LivingRooms,BedRooms=@BedRooms, BathRooms=@BathRooms,KitchenRooms=@KitchenRooms,
                                    Orientation=@Orientation, TranRoomTime=@TranRoomTime,Renovation=@Renovation,
                                    Amount=@Amount,Description=@Description,Img=@Img,LinkMan=@LinkMan, LinkManTel=@LinkManTel,
                                    LinkManSex=@LinkManSex,PubDate=getdate(), HasElevator=@HasElevator
                                WHERE Id=@Id";

                    var i = conn.Execute(sql, new
                    {
                        Id = id,
                        CommunityId = community.Id,
                        Address = address,
                        UserId = userId,
                        Title = title,
                        BussType = bussType,
                        City = community.City,
                        Area = community.Area,
                        LivingRooms = livingRooms,
                        BedRooms = bedRooms,
                        BathRooms = bathRooms,
                        KitchenRooms = kitchenRooms,
                        BuildingAge = buildingAge,
                        BuildingArea = buildingArea,
                        BuildingType = buildingType,
                        Floor = floor,
                        FloorCount = floorCount,
                        Orientation = orientation,
                        TranRoomTime = tranRoomTime,
                        Renovation = renovation,
                        Amount = amount,
                        Description = description,
                        Img = imgs,
                        LinkMan = linkman,
                        LinkManTel = linkmanTel,
                        LinkManSex = linkmanSex,
                        HasElevator = hasElevator
                    }, trans);

                    if (i == 1)
                    {

                        //删除标签信息
                        sql = @"DELETE Tb_Rental_RoomTag  WHERE RentalID=@RentalID";
                        conn.Execute(sql, new
                        {
                            RentalID = id
                        }, trans);


                        // 读取标签信息
                        var tags = (JArray)JsonConvert.DeserializeObject(row["Tags"].ToString());
                        if (tags != null)
                        {
                            foreach (var item in tags)
                            {
                                sql = @"INSERT INTO Tb_Rental_RoomTag(RentalID,CategoryID,Tags)
                                    VALUES(@RentalID,@CategoryID,@Tags);";
                                conn.Execute(sql, new
                                {
                                    RentalID = id,
                                    CategoryID = item["ID"].ToString(),
                                    Tags = string.Join(",", ((JArray)item["Tags"]).ToArray().Select(obj => obj.ToString()))
                                }, trans);
                            }
                        }
                    }

                    trans.Commit();
                    return JSONHelper.FromString(true, "修改成功");
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    return JSONHelper.FromString(false, ex.Message);
                }
            }
        }

        /// <summary>
        /// 刪除房屋信息
        /// </summary>
        private string DeleteRental(DataRow row)
        {
            #region 参数校验

            if (!row.Table.Columns.Contains("Id") || string.IsNullOrEmpty(row["Id"].AsString()))
            {
                return new ApiResult(false, "缺少参数Id").toJson();
            }

            #endregion
            var id = row["Id"].ToString();
            using (var conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                conn.Open();
                var trans = conn.BeginTransaction();
                try
                {
                    var sql = @"DELETE FROM Tb_Rental WHERE Id=@Id";
                    var i = conn.Execute(sql, new { Id = id, }, trans);
                    trans.Commit();
                    return JSONHelper.FromString(true, "刪除成功");
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    return JSONHelper.FromString(false, ex.Message);
                }
            }
        }

        /// <summary>
        /// 修改求租/求售信息
        /// </summary>
        private string UpdateBuyAndRentalInfo(DataRow row)
        {
            #region 参数校验
            if (!row.Table.Columns.Contains("Id") || string.IsNullOrEmpty(row["Id"].AsString()))
            {
                return new ApiResult(false, "缺少参数Id").toJson();
            }
            if (!row.Table.Columns.Contains("CommunityId") || string.IsNullOrEmpty(row["CommunityId"].AsString()))
            {
                return new ApiResult(false, "缺少参数CommunityId").toJson();
            }
            if (!row.Table.Columns.Contains("LivingRooms") || string.IsNullOrEmpty(row["LivingRooms"].AsString()))
            {
                return new ApiResult(false, "缺少参数LivingRooms").toJson();
            }
            if (!row.Table.Columns.Contains("BedRooms") || string.IsNullOrEmpty(row["BedRooms"].AsString()))
            {
                return new ApiResult(false, "缺少参数BedRooms").toJson();
            }
            if (!row.Table.Columns.Contains("BathRooms") || string.IsNullOrEmpty(row["BathRooms"].AsString()))
            {
                return new ApiResult(false, "缺少参数BathRooms").toJson();
            }
            if (!row.Table.Columns.Contains("KitchenRooms") || string.IsNullOrEmpty(row["KitchenRooms"].AsString()))
            {
                return new ApiResult(false, "缺少参数KitchenRooms").toJson();
            }
            if (!row.Table.Columns.Contains("Orientation") || string.IsNullOrEmpty(row["Orientation"].AsString()))
            {
                return new ApiResult(false, "缺少参数Orientation").toJson();
            }
            if (!row.Table.Columns.Contains("BuildingArea") || string.IsNullOrEmpty(row["BuildingArea"].AsString()))
            {
                return new ApiResult(false, "缺少参数BuildingArea").toJson();
            }
            if (!row.Table.Columns.Contains("Amount") || string.IsNullOrEmpty(row["Amount"].AsString()))
            {
                return new ApiResult(false, "缺少参数Amount").toJson();
            }
            if (!row.Table.Columns.Contains("Linkman") || string.IsNullOrEmpty(row["Linkman"].AsString()))
            {
                return new ApiResult(false, "缺少参数Linkman").toJson();
            }
            if (!row.Table.Columns.Contains("LinkmanTel") || string.IsNullOrEmpty(row["LinkmanTel"].AsString()))
            {
                return new ApiResult(false, "缺少参数LinkmanTel").toJson();
            }
            if (!row.Table.Columns.Contains("HasElevator") || string.IsNullOrEmpty(row["HasElevator"].AsString()))
            {
                return new ApiResult(false, "缺少参数HasElevator").toJson();
            }
            if (!row.Table.Columns.Contains("AmountMax") || string.IsNullOrEmpty(row["AmountMax"].AsString()))
            {
                return new ApiResult(false, "缺少参数最大金额").toJson();
            }
            if (!row.Table.Columns.Contains("BuildingAreaMax") || string.IsNullOrEmpty(row["BuildingAreaMax"].AsString()))
            {
                return new ApiResult(false, "缺少参数最大建筑面积").toJson();
            }
            if (!row.Table.Columns.Contains("Title") || string.IsNullOrEmpty(row["Title"].AsString()))
            {
                return new ApiResult(false, "缺少参数标题").toJson();
            }

            #endregion
            var title = row["Title"].ToString();
            var iid = row["Id"].ToString();
            var communityId = row["CommunityId"].ToString();
            var livingRooms = AppGlobal.StrToInt(row["LivingRooms"].ToString());
            var bedRooms = AppGlobal.StrToInt(row["BedRooms"].ToString());
            var bathRooms = AppGlobal.StrToInt(row["BathRooms"].ToString());
            var kitchenRooms = AppGlobal.StrToInt(row["KitchenRooms"].ToString());
            var buildingArea = AppGlobal.StrToDec(row["BuildingArea"].ToString());
            var orientation = row["Orientation"].ToString();
            var amount = AppGlobal.StrToDec(row["Amount"].ToString());
            var linkman = row["Linkman"].ToString();
            var linkmanTel = row["LinkmanTel"].ToString();
            var hasElevator = AppGlobal.StrToInt(row["HasElevator"].ToString());
            var amountMax = row["AmountMax"].ToString();
            var buildingAreaMax = row["BuildingAreaMax"].ToString();
            var community = GetCommunity(communityId);
            if (community == null)
            {
                return JSONHelper.FromString(false, "未查询到小区信息");
            }
            using (var conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                try
                {
                    var sql = @"  UPDATE Tb_Rental SET  Address=@Address, Province=@Province,City=@City, Area=@Area,LivingRooms=@LivingRooms,BedRooms=@BedRooms,BathRooms=@BathRooms ,KitchenRooms =@KitchenRooms,
                     BuildingArea=@BuildingArea,Floor=@Floor,FloorCount=@FloorCount,Orientation=@Orientation,Amount=@Amount,LinkMan=@LinkMan,LinkManSex=@LinkManSex,HasElevator=@HasElevator,AmountMax=@AmountMax,
                    BuildingAreaMax=@BuildingAreaMax ,Title=@Title
                    WHERE Id=@Id  ";
                    var i = conn.Execute(sql, new
                    {
                        Id = iid,
                        Address = String.Format("{0}{1}{2}", community.Province, community.City, community.Area),
                        Province = community.Province,
                        City = community.City,
                        Area = community.Area,
                        LivingRooms = livingRooms,
                        BedRooms = bedRooms,
                        BathRooms = bathRooms,
                        KitchenRooms = kitchenRooms,
                        BuildingArea = buildingArea,
                        Floor = 0,
                        FloorCount = 0,
                        Orientation = orientation,
                        Amount = amount,
                        LinkMan = linkman,
                        LinkManTel = linkmanTel,
                        LinkManSex = 0,
                        HasElevator = hasElevator,
                        AmountMax = amountMax,
                        BuildingAreaMax = buildingAreaMax,
                        Title = title
                    });
                    if (i > 0)
                    {
                        return JSONHelper.FromString(true, "修改成功");
                    }
                    return JSONHelper.FromString(false, "修改失败");
                }
                catch (Exception ex)
                {
                    return JSONHelper.FromString(false, ex.Message);
                }
            }
        }
    }
}
