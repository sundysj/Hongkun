using Dapper;
using MobileSoft.Common;
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
    public partial class GWAppRentalManage
    {
        /// <summary>
        /// 获取房屋租售详情信息
        /// </summary>
        private string GetRentalDetail(DataRow row)
        {
            if (!row.Table.Columns.Contains("Id") || string.IsNullOrEmpty(row["Id"].AsString()))
            {
                return new ApiResult(false, "缺少参数Id").toJson();
            }

            var id = row["Id"].ToString();
            using (var conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                //获取基本信息
                var sql = @"SELECT a.Id,a.Title,a.Amount,a.PubDate,b.Id AS CommunityId,b.CommName,
                                a.BuildingAge,a.BuildingArea,a.Orientation,a.Floor,a.FloorCount,
                                a.BussType,a.Address,a.BuildingType,a.Renovation,a.HasElevator,a.Img,a.Description,

                                CASE a.BedRooms WHEN 0 THEN '' ELSE substring('123456789',a.BedRooms,1)+'室' END+
                                CASE a.LivingRooms WHEN 0 THEN '' ELSE substring('123456789',a.LivingRooms,1)+'厅' END+
                                CASE ISNULL( a.KitchenRooms,0) WHEN 0 THEN '' ELSE substring('123456789',a.KitchenRooms,1)+'厨' END+
                                CASE a.BathRooms WHEN 0 THEN '' ELSE substring('123456789',a.BathRooms,1)+'卫' END AS HouseType,
                                isnull(Middleman,LinkMan) AS Linkman,
                                isnull(SalesMobile,LinkManTel) AS LinkmanTel,
                                a.BedRooms,a.LivingRooms,a.KitchenRooms,a.BathRooms,cast(a.LinkManSex as int) AS LinkManSex,a.Description,
                                a.TranRoomTime
                            FROM Tb_Rental a
                            LEFT JOIN Tb_Community AS b ON a.CommunityId=b.Id
                            WHERE a.Id=@Id;

                            SELECT a.CategoryID,b.Name AS CategoryName,a.IID AS TagID,a.Name AS TagName
                            FROM Tb_Rental_TagSetting a
                            LEFT JOIN Tb_Rental_TagCategory b ON a.CategoryID=b.IID;";

                //var reader = conn.QueryMultiple(sql, new { Id = id });
                var baseInfo = conn.QueryFirstOrDefault(sql, new { Id = id });
                sql = @" SELECT stuff((SELECT ','+Tags FROM Tb_Rental_RoomTag WHERE RentalID=@Id  FOR XML PATH('')),1,1,'') AS Selected;";
                var selected = conn.QueryFirstOrDefault<String>(sql, new { Id = id });

                String addWhere = String.Empty;
                if (baseInfo.BussType == "出租")
                {
                    addWhere = "  or CategoryID=1 ";
                }

                sql = $@" SELECT a.CategoryID,b.Name AS CategoryName,a.IID AS TagID,a.Name AS TagName  FROM Tb_Rental_TagSetting a  LEFT JOIN Tb_Rental_TagCategory b ON a.CategoryID=b.IID where(CategoryID in (SELECT CategoryID FROM Tb_Rental_RoomTag  WHERE RentalID=@Id)   { addWhere })
                    and ISNULL(a.IsDelete,0)=0;";
                var tagsInfo = conn.Query(sql, new { Id = id });
                List<String> selectedArray = new List<string>();
                if (selected != null) selectedArray = selected.Split(',').ToList();

                List<object> selectedList = new List<object>();
                foreach (var model in selectedArray)
                {
                    selectedList.Add(int.Parse(model));
                }
                selectedList.Distinct();
                var list = new List<dynamic>();
                foreach (dynamic item in tagsInfo)
                {
                    if (item.CategoryID != 1 && !selectedList.Contains(item.TagID))
                    {
                        continue;
                    }
                    var tmp = list.Find(obj => obj.CategoryID == item.CategoryID);
                    if (tmp == null)
                    {
                        tmp = new
                        {
                            CategoryID = item.CategoryID,
                            ID = item.CategoryID,
                            Name = item.CategoryName,
                            Tags = new List<dynamic>(),
                        };
                        list.Add(tmp);
                    }

                    ((List<dynamic>)tmp.Tags).Add(new { ID = item.TagID, Name = item.TagName, Selected = item.CategoryID == 1 ? selectedList.Contains(item.TagID) : true });
                }
                baseInfo.Tags = list;
                return new ApiResult(true, baseInfo).toJson();
            }
        }

    }
}
