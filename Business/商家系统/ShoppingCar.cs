using System;
using System.Text;
using Common;
using Dapper;
using DapperExtensions;
using System.Data;
using MobileSoft.Common;
using Model.Model.Buss;
using System.Data.SqlClient;
using MobileSoft.DBUtility;
using System.Collections.Generic;
using System.Linq;

namespace Business
{
    /// <summary>
    /// 购物车
    /// </summary>
    public class ShoppingCar : PubInfo
    {
        public ShoppingCar()
        {
            base.Token = "20161121ShoppingCar";
        }

        public override void Operate(ref Transfer Trans)
        {
            Trans.Result = JSONHelper.FromString(false, "未知错误");
            DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
            DataRow Row = dAttributeTable.Rows[0];

            switch (Trans.Command)
            {
                case "SetShoppingCar":
                    Trans.Result = SetShoppingCar(Row);//添加购物车
                    break;
                case "GetShoppingCar":
                    Trans.Result = GetShoppingCar(Row);//获取购物车信息
                    break;
                case "DelShoppingCarDetailed":
                    Trans.Result = DelShoppingCarDetailed(Row);//删除购物车明细
                    break;
                case "DelShoppingCarAll":
                    Trans.Result = DelShoppingCarAll(Row);//清空购物车
                    break;
                case "UpdateShoppingCarDetailedNum":
                    Trans.Result = UpdateShoppingCarDetailedNum(Row);//修改购物车明细数量
                    break;
                case "GetShoppingCarCount":
                    Trans.Result = GetShoppingCarCount(Row);//获取购物车商品条数
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 获取购物车商品条数
        /// </summary>
        private string GetShoppingCarCount(DataRow row)
        {
            if (!row.Table.Columns.Contains("UserId") || string.IsNullOrEmpty(row["UserId"].ToString()))
            {
                return JSONHelper.FromString(false, "用户编码不能为空");
            }

            string sql = @"SELECT COUNT(*) FROM Tb_ShoppingCar a 
                           LEFT JOIN Tb_Resources_Details b on a.ResourcesID=b.ResourcesID
                           LEFT JOIN  Tb_System_BusinessCorp AS C ON C.BussId=A.BussId
                           WHERE b.IsRelease = '是' AND isnull(C.IsClose,'未关闭')='未关闭'
                           AND b.IsStopRelease = '否' AND isnull(b.IsDelete,0)= 0 AND isnull(a.IsDelete,0)= 0 AND UserId=@UserId";

            // 查询购物车明细
            IDbConnection con = new SqlConnection(PubConstant.GetConnectionString("BusinessContionString"));
            string result = con.Query<string>(sql, new { UserId = row["UserId"].ToString() }).FirstOrDefault();
            return JSONHelper.FromString(true, result);
        }

        /// <summary>
        /// 清空购物车   DelShoppingCarAll
        /// </summary>
        /// <param name="row"></param>
        /// UserId          用户编码【必填】
        /// 返回：
        ///     true:
        ///     false:删除失败
        /// <returns></returns>
        private string DelShoppingCarAll(DataRow row)
        {
            if (!row.Table.Columns.Contains("UserId") || string.IsNullOrEmpty(row["UserId"].ToString()))
            {
                return JSONHelper.FromString(false, "用户编码不能为空");
            }
            string str = "";
            try
            {
                IDbConnection con = new SqlConnection(PubConstant.GetConnectionString("BusinessContionString"));
                //批量删除购物车明细
                con.ExecuteScalar(" update Tb_ShoppingCar set IsDelete=1 where UserId=@ShoppingId", new { ShoppingId = row["UserId"].ToString() }, null, null, CommandType.Text);
            }
            catch (Exception ex)
            {
                str = ex.Message;
            }
            if (str != "")
            {
                return JSONHelper.FromString(false, str);
            }
            else
            {
                return JSONHelper.FromString(true, "");
            }
        }


        /// <summary>
        /// 修改购物车明细数量      UpdateShoppingCarDetailedNum
        /// </summary>
        /// <param name="row"></param>
        /// Id                      购物车明细编码
        /// Number                  数量
        /// 返回
        ///     true:""
        ///     false:操作失败
        /// <returns></returns>
        private string UpdateShoppingCarDetailedNum(DataRow row)
        {
            if (!row.Table.Columns.Contains("Id") || string.IsNullOrEmpty(row["Id"].ToString()))
            {
                return JSONHelper.FromString(false, "购物车明细编码不能为空");
            }
            if (!row.Table.Columns.Contains("Number") || string.IsNullOrEmpty(row["Number"].ToString()))
            {
                return JSONHelper.FromString(false, "数量不能为空");
            }

            var shoppingId = row["Id"].ToString();
            var number = AppGlobal.StrToInt(row["Number"].ToString());

            try
            {
                using (var conn = new SqlConnection(PubConstant.BusinessContionString))
                {
                    var sql = @"SELECT * FROM Tb_ShoppingCar WHERE Id=@Id AND isnull(IsDelete,0)=0;";
                    var shoppingCar = conn.Query<Tb_ShoppingCar>(sql, new { Id = shoppingId }).FirstOrDefault();
                    if (shoppingCar == null)
                        return JSONHelper.FromString(false, "抱歉，该商品已从购物车删除");

                    sql = @"SELECT * FROM Tb_Resources_Details WHERE ResourcesID=@ResourcesID AND isnull(IsDelete,0)=0";
                    var resourcesDetail = conn.Query<Tb_Resources_Details>(sql, new { ResourcesID = shoppingCar.ResourcesID }).FirstOrDefault();
                    if (resourcesDetail.IsStopRelease == "是" || resourcesDetail.IsDelete == 1)
                        return JSONHelper.FromString(false, "抱歉，该商品已下架");

                    if (number < shoppingCar.Number)
                    {
                        shoppingCar.Number = number;
                        conn.Update(shoppingCar);
                    }
                    else
                    {
                        //现在判断库存 修改位判断规格库存
                        sql = @"SELECT ISNULL(Inventory,0) FROM  Tb_ResourcesSpecificationsPrice AS A
                                INNER JOIN  (SELECT B.ResourcesID,C.PropertysId,C.SpecId FROM Tb_ShoppingCar AS B
                                LEFT JOIN Tb_ShoppingDetailed AS C ON B.Id=C.ShoppingId
                                WHERE B.Id IS NOT NULL AND B.Id=@Id) AS D  ON  D.ResourcesID=A.ResourcesID AND D.PropertysId =A.PropertyId AND D.SpecId=A.SpecId;";

                        var inventory = conn.QueryFirstOrDefault<decimal>(sql, new { Id = shoppingId });

                        if (number > inventory)
                        {
                            return JSONHelper.FromString(false, "抱歉，库存不足");
                        }

                        shoppingCar.Number = number;
                        conn.Update(shoppingCar);
                    }

                    return JSONHelper.FromString(true, "操作成功");
                }
            }
            catch (Exception ex)
            {
                return JSONHelper.FromString(false, ex.Message);
            }

        }

        /// <summary>
        /// 删除购物车  DelShoppingCarDetailed
        /// </summary>
        /// <param name="row"></param>
        /// Id                  购物车编码
        /// 返回：
        ///     true:""
        ///     false:删除失败
        /// <returns></returns>
        private string DelShoppingCarDetailed(DataRow row)
        {
            //if (!row.Table.Columns.Contains("UserId") || string.IsNullOrEmpty(row["UserId"].ToString()))
            //{
            //    return JSONHelper.FromString(false, "用户编码不能为空");
            //}
            if (!row.Table.Columns.Contains("Id") || string.IsNullOrEmpty(row["Id"].ToString()))
            {
                return JSONHelper.FromString(false, "购物车明细编码不能为空");
            }
            string str = "";
            try
            {
                using (var conn = new SqlConnection(PubConstant.BusinessContionString))
                {
                    var id = row["Id"].ToString();
                    var sql = "";
                    var tmp = id.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (var item in tmp)
                    {
                        sql += $"update Tb_ShoppingCar set IsDelete=1 where Id='{item}';";
                    }

                    conn.Execute(sql);
                }
            }
            catch (Exception ex)
            {
                str = ex.Message;
            }
            if (str != "")
            {
                return JSONHelper.FromString(false, "删除失败");
            }
            else
            {
                return JSONHelper.FromString(true, "");
            }

        }


        /// <summary>
        /// 查询购物车   GetShoppingCar
        /// </summary>
        /// <param name="row"></param>
        /// UserId              用户编码【必填】
        /// PageIndex           默认1
        /// PageSize            默认5
        /// 返回：
        ///     Result：true
        ///     data:Id[购物车明细ID],Number[数量],ResourcesName[商品名称],ResourcesSalePrice[销售价格],Img[图片],ResourcesCount[库存],BussId[商家编号],ResourcesID[商品ID],BussName[商家名称]
        ///     AccountMoney：总金额
        /// <returns></returns>
        private string GetShoppingCar(DataRow row)
        {
            if (!row.Table.Columns.Contains("UserId") || string.IsNullOrEmpty(row["UserId"].ToString()))
            {
                return JSONHelper.FromString(false, "用户编码不能为空");
            }

            int PageIndex = 1;
            int PageSize = 10;

            if (row.Table.Columns.Contains("PageIndex") && AppGlobal.StrToInt(row["PageIndex"].ToString()) > 0)
            {
                PageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());
            }
            if (row.Table.Columns.Contains("PageSize") && AppGlobal.StrToInt(row["PageSize"].ToString()) > 0)
            {
                PageSize = AppGlobal.StrToInt(row["PageSize"].ToString());
            }

            int pageCount;
            int Counts;
            string sql = @"SELECT a.Id,a.Number,a.ResourcesID,a.CorpId,a.BussId,b.BussNature,b.BussName,c.ResourcesName,C.IsServiceType,c.PushContent,a.AddTime,
                                (SELECT TOP 1 value FROM SplitString(c.Img,',',1)) AS Img,
                                c.ResourcesSalePrice,c.ResourcesDisCountPrice,c.IsGroupBuy,c.GroupBuyPrice,C.GroupBuyStartDate,C.GroupBuyEndDate,c.IsSupportCoupon,c.MaximumCouponMoney
                            FROM Tb_ShoppingCar a LEFT JOIN Tb_System_BusinessCorp b on a.BussId=b.BussId
                            LEFT JOIN Tb_Resources_Details c on c.ResourcesID=a.ResourcesID
                            WHERE c.IsRelease = '是' AND c.IsStopRelease = '否' AND isnull(a.IsDelete,0)=0 AND isnull(c.IsDelete,0)=0 AND isnull(b.IsDelete,0)=0
                                 AND isnull(b.IsClose,'未关闭')='未关闭'
                            AND  a.UserId='" + row["UserId"].ToString() + "'";
            //查询购物车明细
            DataSet ds = BussinessCommon.GetList(out pageCount, out Counts, sql, PageIndex, PageSize, "AddTime", 0, "Id", PubConstant.GetConnectionString("BusinessContionString"), "*");
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"Result\":\"true\",");
            sb.Append("\"data\":");
            if (ds == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
            {
                sb.Append("[]");
            }
            else
            {
                //sb.Append(JSONHelper.FromDataTable(ds.Tables[0]));
                int i = 0;
                sb.Append("[");
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    if (i > 0)
                    {
                        sb.Append(",");
                    }

                    string sstr = JSONHelper.FromDataRow(item);
                    string ss = sstr.ToString().Substring(0, sstr.ToString().Length - 1);
                    sb.Append(ss);
                    sb.Append(",");
                    sb.Append("\"Property\":");
                    sb.Append("[");
                    int j = 0;
                    IDbConnection con = new SqlConnection(PubConstant.GetConnectionString("BusinessContionString"));
                    DataSet ds_Property = con.ExecuteReader(@" select p.PropertyName,s.SpecName,E.Price,E.DiscountAmount,E.GroupBuyingPrice
                         from Tb_ShoppingDetailed as d 
                         left join Tb_Resources_Property as p on d.PropertysId = p.Id 
                         left join Tb_Resources_Specifications as s on d.SpecId = s.Id 
                         left join Tb_ShoppingCar as c on c.Id = d.ShoppingId
                         LEFT JOIN  Tb_ResourcesSpecificationsPrice AS E ON E.ResourcesID=C.ResourcesID AND E.BussId=c.BussId and E.PropertyId=d.PropertysId and E.SpecId=d.SpecId
                         where d.ShoppingId =  '" + item["Id"] + "'", null, null, null, CommandType.Text).ToDataSet();
                    foreach (DataRow dr in ds_Property.Tables[0].Rows)
                    {
                        if (j == 0)
                        {
                            sb.Append(JSONHelper.FromDataRow(dr));
                        }
                        else
                        {
                            sb.Append(",");
                            sb.Append(JSONHelper.FromDataRow(dr));
                        }
                        j++;
                    }
                    sb.Append("]");
                    sb.Append("}");
                    i++;
                }
                sb.Append("]");
            }
            sb.Append("}");
            return sb.ToString();
        }

        /// <summary>
        /// 加入购物车  SetShoppingCar
        /// </summary>
        /// <param name="row"></param>
        /// UserId              用户编码【必填】
        /// ResourcesID         商品编码【必填】
        /// BussId              商家编码【必填】
        /// Number              数量 【可不填，默认为1】
        /// PropertysId         属性规格【格式：属性ID：规格ID,属性ID：规格ID,属性ID：规格ID】
        /// 返回：
        ///     false：错误信息
        ///     true：操作成功
        /// <returns></returns>
        private string SetShoppingCar(DataRow row)
        {
            if (!row.Table.Columns.Contains("UserId") || string.IsNullOrEmpty(row["UserId"].ToString()))
            {
                return JSONHelper.FromString(false, "用户编码不能为空");
            }
            if (!row.Table.Columns.Contains("ResourcesID") || string.IsNullOrEmpty(row["ResourcesID"].ToString()))
            {
                return JSONHelper.FromString(false, "商品编码不能为空");
            }
            if (!row.Table.Columns.Contains("BussId") || string.IsNullOrEmpty(row["BussId"].ToString()))
            {
                return JSONHelper.FromString(false, "商家编码不能为空");
            }
            //if (!row.Table.Columns.Contains("PropertysId") || string.IsNullOrEmpty(row["PropertysId"].ToString()))
            //{
            //    return JSONHelper.FromString(false, "规格编码不能为空");
            //}

            // 2017年9月23日16:47:32，谭洋
            int? CorpId = null;
            if (row.Table.Columns.Contains("CorpId") && !string.IsNullOrEmpty(row["CorpId"].ToString()))
            {
                CorpId = AppGlobal.StrToInt(row["CorpId"].ToString());
            }

            string UserId = row["UserId"].ToString();
            string BussId = row["BussId"].ToString();
            string ResourcesID = row["ResourcesID"].ToString();
            int Number = 1;//加入购物车数量，默认1
            //该参数值格式为：  属性ID：规格ID,属性ID：规格ID,属性ID：规格ID
            string PropertysId = row["PropertysId"].ToString();
            //获取属性查询条件
            string PropertysIdWhere = BussinessCommon.GetShoppingPropertys(PropertysId);

            if (row.Table.Columns.Contains("Number") && AppGlobal.StrToInt(row["Number"].ToString()) > 0)
            {
                Number = AppGlobal.StrToInt(row["Number"].ToString());
            }

            IDbConnection con = new SqlConnection(PubConstant.GetConnectionString("BusinessContionString"));
            string str = "";
            try
            {
                List<Tb_ShoppingCar> Car = new List<Tb_ShoppingCar>();
                //查询购物车中是否有该商品
                Car = BussinessCommon.GetShoppingCheck(UserId, ResourcesID);
                Tb_Resources_Details Resources = BussinessCommon.GetResourcesModel(ResourcesID);
                if (Resources == null || string.IsNullOrEmpty(Resources.ResourcesID))
                {
                    return JSONHelper.FromString(false, "该商品不存在");
                }

                if (Car != null && Car.Count > 0)//如果已存在此商品，
                {
                    int j = 0;
                    int z = 0;
                    foreach (Tb_ShoppingCar item in Car)//循环此人该购物车中的这种商品
                    {
                        z++;
                        DataSet ds = null;
                        if (PropertysIdWhere != "")
                        {
                            //查询属性明细中已有的属性
                            ds = BussinessCommon.GetShoppingDetailedCheck(item.Id, PropertysIdWhere);
                        }

                        //如果属性存在，新增
                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            if (item.Number + Number > Resources.ResourcesCount)//库存检测
                            {
                                return JSONHelper.FromString(false, "该商品库存不足");
                            }
                            else//新增
                            {
                                SetShopping(CorpId, UserId, ResourcesID, BussId, Number, PropertysId, con);
                            }
                            j++;
                            break;
                        }
                        //属性不存在，则添加属性
                        if (z == Car.Count)
                        {
                            if (item.Number + Number > Resources.ResourcesCount)//库存检测
                            {
                                return JSONHelper.FromString(false, "该商品库存不足");
                            }
                            item.Number = item.Number + Number;
                            con.Update<Tb_ShoppingCar>(item);
                            //添加属性
                            SetShoppingCarDetailed(BussId, PropertysId, con, item);
                        }
                    }
                }
                else
                {
                    if (Number > Resources.ResourcesCount)//库存检测
                    {
                        return JSONHelper.FromString(false, "该商品库存不足");
                    }
                    else//新增
                    {
                        SetShopping(CorpId, UserId, ResourcesID, BussId, Number, PropertysId, con);
                    }
                }
            }
            catch (Exception ex)
            {
                str = ex.Message;
            }

            if (str != "")
            {
                return JSONHelper.FromString(false, str);
            }
            else
            {
                return JSONHelper.FromString(true, "操作成功");
            }

        }

        /// <summary>
        /// 添加购物车
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="ResourcesID"></param>
        /// <param name="BussId"></param>
        /// <param name="Number"></param>
        /// <param name="PropertysId"></param>
        /// <param name="con"></param>
        /// <returns></returns>
        private static Tb_ShoppingCar SetShopping(int? corpId, string UserId, string ResourcesID, string BussId, int Number, string PropertysId, IDbConnection con)
        {
            Tb_ShoppingCar Car = new Tb_ShoppingCar();
            Car.Id = Guid.NewGuid().ToString();
            Car.IsDelete = 0;
            Car.Number = Number;
            Car.ResourcesID = ResourcesID;
            Car.SubtotalMoney = 0;
            Car.UserId = UserId;
            Car.BussId = BussId;
            Car.CorpId = corpId;
            //增加购物车
            con.Insert<Tb_ShoppingCar>(Car);
            //添加属性
            SetShoppingCarDetailed(BussId, PropertysId, con, Car);
            return Car;
        }

        /// <summary>
        /// 添加属性集
        /// </summary>
        /// <param name="BussId"></param>
        /// <param name="PropertysId"></param>
        /// <param name="con"></param>
        /// <param name="Car"></param>
        private static void SetShoppingCarDetailed(string BussId, string PropertysId, IDbConnection con, Tb_ShoppingCar Car)
        {
            if (PropertysId != "")
            {
                foreach (string item in PropertysId.Split(','))
                {
                    Tb_ShoppingDetailed ShoppingDetailed = new Tb_ShoppingDetailed();
                    ShoppingDetailed.BussId = BussId;
                    ShoppingDetailed.Id = Guid.NewGuid().ToString();
                    ShoppingDetailed.ShoppingId = Car.Id;
                    ShoppingDetailed.PropertysId = item.Split(':')[0].ToString();//属性
                    if (item.Split(':').Length >= 2)
                    {
                        ShoppingDetailed.SpecId = item.Split(':')[1].ToString();//规格
                    }
                    else
                    {
                        ShoppingDetailed.SpecId = "";//规格
                    }

                    con.Insert<Tb_ShoppingDetailed>(ShoppingDetailed);
                }
            }

        }
    }
}
