using Common;
using Dapper;
using MobileSoft.Common;
using MobileSoft.Model.Unified;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    /// <summary>
    /// 产业报事用户房屋对接
    /// </summary>
    public class IncidentRoomAndUser : PubInfo
    {
        public IncidentRoomAndUser()
        {
            base.Token = "20201111RoomAndUserAccept";
        }

        public override void Operate(ref Transfer Trans)
        {
            Trans.Result = JSONHelper.FromString(false, "未知错误");

            DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
            DataRow Row = dAttributeTable.Rows[0];

            switch (Trans.Command)
            {


                case "AddRoomAndUserInfo":
                    Trans.Result = AddRoomAndUserInfo(Row);//报事大类
                    break;
                default:
                    break;
            }
        }

        #region 添加房屋和用户信息
        private string AddRoomAndUserInfo(DataRow row)
        {
            try
            {
                string CustID = row["CustID"].ToString();
                string CommID = row["CommID"].ToString();
                string RoomID = row["RoomID"].ToString();
                string Phone = row["Phone"].ToString();
                string CustName = row["CustName"].ToString();
                string RoomSign = row["RoomSign"].ToString();
                string BuildingName = row["BuildingName"].ToString();
                string floor = row["floor"].ToString();
                string Unit = row["Unit"].ToString();
                //查询小区
                Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(CommID);
                //构建链接字符串
                if (Community == null)
                {
                    return JSONHelper.FromString(false, "该小区不存在");
                }

                DateTime dtNow = DateTime.Now;
                string connStr = new CostInfo().GetConnectionStringStr(Community);
                using (IDbConnection con = new SqlConnection(connStr))
                {
                    //判断用户信息进行处理
                    string sql = "select CustID from Tb_HSPR_Customer with(nolock) where CustID='" + CustID.Trim() + "'";
                    DataTable dt2 = con.ExecuteReader(sql).ToDataSet().Tables[0];
                    if (dt2.Rows.Count <= 0)
                    {
                        //需要添加用户信息
                        string insertSql = @"INSERT INTO Tb_HSPR_Customer(CustID,CommID,CustName,IsDelete,MobilePhone,Memo) VALUES(" + CustID + "," + Community.CommID + ",'" + CustName + "',0,'" + Phone + "','通过同步接口自动添加')";
                        con.Execute(insertSql);
                    }

                    //判断房屋信息进行处理
                    sql = "select RoomID from Tb_HSPR_Room with(nolock) where RoomID='" + RoomID.Trim() + "'";
                    dt2 = con.ExecuteReader(sql).ToDataSet().Tables[0];
                    if (dt2.Rows.Count <= 0)
                    {
                        //判断楼栋信息进行处理
                        string buildSnum = "1";
                        string buildID = Community.CommID + "000001";
                        sql = "select BuildID,BuildSNum from Tb_HSPR_Building with(nolock) where BuildName='" + BuildingName.Trim() + "' and CommID=" + Community.CommID;
                        DataTable dt = con.ExecuteReader(sql).ToDataSet().Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            buildID = dt.Rows[0]["BuildID"].ToString();
                            buildSnum = dt.Rows[0]["BuildSNum"].ToString();
                        }
                        else
                        {
                            sql = "select top 1 BuildID,BuildSNum from tb_hspr_building with(nolock) where commid=" + Community.CommID + " order by BuildID desc";
                            dt = con.ExecuteReader(sql).ToDataSet().Tables[0];
                            if (dt.Rows.Count > 0)
                            {
                                buildID = (long.Parse(dt.Rows[0]["BuildID"].ToString()) + 1).ToString();
                                buildSnum = (int.Parse(dt.Rows[0]["BuildSNum"].ToString()) + 1).ToString();
                            }

                            //添加楼栋信息
                            string insertSq1l = @"INSERT INTO tb_hspr_building(BuildID,CommID,BuildSign,BuildName,BuildSNum,IsDelete) VALUES(" + buildID + "," + Community.CommID + ",'" + BuildingName + "','" + BuildingName + "'," + buildSnum + ",0)";
                            con.Execute(insertSq1l);
                        }

                        //添加房屋信息
                        string insertSql2 = @"INSERT INTO Tb_HSPR_Room(RoomID,CommID,RoomSign,RoomName,RegionSNum,BuildSNum,UnitSNum,FloorSNum,PropertyRights,BuildArea,InteriorArea,CommonArea,PropertyUses,RoomState,ChargeTypeID,UsesState
,IsDelete,PoolRatio,TakeOverDate,ActualSubDate,BuildsRenovation) VALUES(" + RoomID + "," + Community.CommID + ",'" + RoomSign + "','" + RoomSign + "',0,"+buildSnum+","+Unit+","+floor+ ",'自有产权',0.00,0.00,0.00,'住宅',1,0,0,0,0.00,'"+ dtNow.ToString()+ "','"+dtNow.ToString()+ "','非精装')";
                        con.Execute(insertSql2);
                    }

                    //判断房屋和用户关系处理
                    sql = "select LiveID,CustID from Tb_HSPR_CustomerLive with(nolock) where RoomID='" + RoomID.Trim() + "'";
                    DataTable dt1 = con.ExecuteReader(sql).ToDataSet().Tables[0];
                    if (dt1.Rows.Count > 0)
                    {
                        if (!CustID.Equals(dt1.Rows[0]["CustID"].ToString()))
                        {
                            string updateSq1 = @"update Tb_HSPR_CustomerLive set CustID="+CustID+"where LiveID="+ dt1.Rows[0]["LiveID"].ToString();
                            con.Execute(updateSq1);
                        }
                    }
                    else
                    {
                        long liveID = long.Parse(dtNow.ToString("yyyyMMddHHmmssfff") + "1");
                        //添加中间表关系
                        string insertSq1l = @"INSERT INTO Tb_HSPR_CustomerLive(LiveID,RoomID,CustID,LiveType,ChargingTime,IsActive,IsDelLive,IsDebts,IsSale,LiveState) VALUES(" + liveID + "," + RoomID + "," + CustID + ",1,'" + dtNow.ToString() + "',1,0,1,0,0)";
                        con.Execute(insertSq1l);
                    }
                }

                return JSONHelper.FromString(true, "添加成功");
            }
            catch(Exception ex)
            {
                return JSONHelper.FromString(false, "添加失败,请联系管理员");
            }
        }
        #endregion
    }
}
