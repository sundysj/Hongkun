using System;
using System.Text;
using Common;
using System.Linq;
using Dapper;
using DapperExtensions;
using System.Data;
using MobileSoft.Common;
using Model.Model.Buss;
using System.Data.SqlClient;
using MobileSoft.DBUtility;
using System.Collections.Generic;
using MobileSoft;

namespace Business
{
    /// <summary>
    /// 商家收货地址
    /// </summary>
   public class UserAddress : PubInfo
    {
        public UserAddress()
        {
            base.Token = "20161123UserAddress";
        }

        public override void Operate(ref Transfer Trans)
        {
            Trans.Result = JSONHelper.FromString(false, "未知错误");
            DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
            DataRow Row = dAttributeTable.Rows[0];

            switch (Trans.Command)
            {
                case "SetUserAddress":
                    Trans.Result = SetUserAddress(Row);//添加地址
                    break;
                case "GetUserAddress":
                    Trans.Result = GetUserAddress(Row);//获取收货地址
                    break;
                case "DelUserAddress":
                    Trans.Result = DelUserAddress(Row);//删除收货地址
                    break;
                case "UpdateUserAddressInfo":
                    Trans.Result = UpdateUserAddressInfo(Row);//修改收货地址
                    break;
                case "UpdateUserAddressIsDefault":
                    Trans.Result = UpdateUserAddressIsDefault(Row);//修改默认收货地址
                    break;
                case "GetUserAddressIsDefault":
                    Trans.Result = GetUserAddressIsDefault(Row);//获取默认收货地址
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 获取默认收货地址
        /// </summary>
        /// <param name="row"></param>
        /// UserId          【必填】
        /// <returns></returns>
        private string GetUserAddressIsDefault(DataRow row)
        {
            if (!row.Table.Columns.Contains("UserId") || string.IsNullOrEmpty(row["UserId"].ToString()))
            {
                return JSONHelper.FromString(false, "用户编码不能为空");
            }
            IDbConnection con = new SqlConnection(PubConstant.GetConnectionString("BusinessContionString"));
            
            DataSet ds = con.ExecuteReader("select Id,BussId,UserId,UserName,Address,Mobile,IsDefault from Tb_User_Address where UserId='" + row["UserId"] + "' and IsDefault=1", null, null, null, CommandType.Text).ToDataSet();
            return JSONHelper.FromString(ds.Tables[0]);
        }


        /// <summary>
        /// 修改默认收货地址    UpdateUserAddressIsDefault
        /// 备注：0 不默认，1默认
        /// </summary>
        /// <param name="row"></param>
        /// Id          地址编码【必填】
        /// 返回：
        ///     true:""
        ///     false:错误信息
        /// <returns></returns>
        private string UpdateUserAddressIsDefault(DataRow row)
        {
            if (!row.Table.Columns.Contains("Id") || string.IsNullOrEmpty(row["Id"].ToString()))
            {
                return JSONHelper.FromString(false, "地址编码不能为空");
            }           

            IDbConnection con = new SqlConnection(PubConstant.GetConnectionString("BusinessContionString"));
            string str = "";
            try
            {              
                var Address = new Tb_User_Address();
                
                var sql = @"SELECT * FROM Tb_User_Address where Id=@Id";
                Address= con.QueryFirstOrDefault<Tb_User_Address>(sql, new { Id = row["Id"].ToString() });
                if (Address == null || Address.Id == "")
                {
                    return JSONHelper.FromString(false, "该地址不存在");
                }

                Address.IsDefault = 1;
                Address.UpdataTime = DateTime.Now;
                //修改该地址为 默认状态
                con.Update<Tb_User_Address>(Address);

                //修改其它地址为 不默认状态
                con.ExecuteScalar(" update Tb_User_Address set IsDefault=0 where Id<>@Id and UserId=@UserId", new { Id= row["Id"].ToString(),Address.UserId },  null, null, CommandType.Text);

            }
            catch (Exception ex)
            {
                str = ex.Message;
            }
            if (str != "")
            {
                return JSONHelper.FromString(false, "操作失败");
            }
            else
            {
                return JSONHelper.FromString(true, "操作成功");
            }
        }

        /// <summary>
        /// 修改收货地址信息  UpdateUserAddressInfo
        /// </summary>
        /// <param name="row"></param>
        /// Id          Id【必填】
        /// UserName    收货人名称【必填】
        /// Address     配送地址【必填】
        /// Mobile      联系方式【必填】
        /// 返回：
        ///     true:""
        ///     false:错误信息
        /// <returns></returns>
        private string UpdateUserAddressInfo(DataRow row)
        {
            if (!row.Table.Columns.Contains("Id") || string.IsNullOrEmpty(row["Id"].ToString()))
            {
                return JSONHelper.FromString(false, "地址编码不能为空");
            }
            if (!row.Table.Columns.Contains("UserName") || string.IsNullOrEmpty(row["UserName"].ToString()))
            {
                return JSONHelper.FromString(false, "收货人不能为空");
            }
            if (!row.Table.Columns.Contains("Address") || string.IsNullOrEmpty(row["Address"].ToString()))
            {
                return JSONHelper.FromString(false, "配送地址不能为空");
            }
            if (!row.Table.Columns.Contains("Mobile") || string.IsNullOrEmpty(row["Mobile"].ToString()))
            {
                return JSONHelper.FromString(false, "电话不能为空");
            }

            IDbConnection con = new SqlConnection(PubConstant.GetConnectionString("BusinessContionString"));
            string str = "";
            try
            {
                Tb_User_Address Address = new Tb_User_Address();
                Address = con.Query<Tb_User_Address>("select * from Tb_User_Address where Id=@Id", new {Id= row["Id"].ToString() }).SingleOrDefault<Tb_User_Address>();
                if (Address==null ||Address.Id=="")
                {
                    return JSONHelper.FromString(false, "该地址不存在");
                }
                if (true)
                {

                }
                Address.UserName = row["UserName"].ToString();
                Address.Address = row["Address"].ToString();
                Address.Mobile = row["Mobile"].ToString();
                Address.UpdataTime = DateTime.Now;
                con.Update<Tb_User_Address>(Address);
            }
            catch (Exception ex)
            {
                str = ex.Message;
            }
            if (str != "")
            {
                return JSONHelper.FromString(false, "操作失败");
            }
            else
            {
                return JSONHelper.FromString(true, "");
            }

        }


        /// <summary>
        /// 删除地址        DelUserAddress
        /// 删除时，该地址为默认，则随机指定一个地址为默认
        /// </summary>
        /// <param name="row"></param>
        /// Id          Id【必填】
        /// 返回：
        ///     true:""
        ///     false:错误信息
        /// <returns></returns>
        private string DelUserAddress(DataRow row)
        {
            if (!row.Table.Columns.Contains("Id") || string.IsNullOrEmpty(row["Id"].ToString()))
            {
                return JSONHelper.FromString(false, "地址编码不能为空");
            }
            IDbConnection con = new SqlConnection(PubConstant.GetConnectionString("BusinessContionString"));
            string str = "";
            string id = "";
            try
            {
                Tb_User_Address Address = new Tb_User_Address();
                Address = con.Query<Tb_User_Address>("select * from Tb_User_Address where Id=@Id", new { Id = row["Id"].ToString() }).SingleOrDefault<Tb_User_Address>();
                if (Address == null || Address.Id == "")
                {
                    return JSONHelper.FromString(false, "该地址不存在");
                }
                
                if (Address.IsDefault==1)//如果当前数据为默认
                {
                    DataSet ds= con.ExecuteReader("select * from Tb_User_Address where UserId = '"+Address.UserId+"' and Id<>'" + row["Id"].ToString() + "'").ToDataSet();
                     con = new SqlConnection(PubConstant.GetConnectionString("BusinessContionString"));
                    if (ds!=null && ds.Tables.Count>0 && ds.Tables[0].Rows.Count>0)
                    {
                        con.ExecuteScalar(" update Tb_User_Address set IsDefault=1 where Id='" + ds.Tables[0].Rows[0]["Id"].ToString() + "'");
                        id = ds.Tables[0].Rows[0]["Id"].ToString();
                    }
                }
                con = new SqlConnection(PubConstant.GetConnectionString("BusinessContionString"));
                con.Delete<Tb_User_Address>(Address);
            }
            catch (Exception ex)
            {
                str = ex.Message;
            }
            if (str != "")
            {
                return JSONHelper.FromString(false, "操作失败");
            }
            else
            {
                return JSONHelper.FromString(true, id);
            }
        }

        /// <summary>
        /// 获取所有地址  GetUserAddress
        /// </summary>
        /// <param name="row"></param>
        /// UserId          用户编码【必填】
        /// <returns></returns>
        private string GetUserAddress(DataRow row)
        {
            if (!row.Table.Columns.Contains("UserId") || string.IsNullOrEmpty(row["UserId"].ToString()))
            {
                return JSONHelper.FromString(false, "用户编码不能为空");
            }
            IDbConnection con = new SqlConnection(PubConstant.GetConnectionString("BusinessContionString"));
            DataSet ds = con.ExecuteReader("select Id,BussId,UserName,UserId,Address,Mobile,IsDefault from Tb_User_Address where UserId='" + row["UserId"] + "' order by UpdataTime desc", null,null,null,CommandType.Text).ToDataSet();                              
            return JSONHelper.FromString(ds.Tables[0]);
        }

        /// <summary>
        /// 添加收货地址  SetUserAddress
        /// 
        /// </summary>
        /// <param name="row"></param>
        /// Mobile          联系方式【必填】
        /// UserId          用户编码【必填】
        /// UserName        收货人【必填】
        /// Address         配送地址【必填】
        /// BussId          商家编号【选填】
        /// 返回：
        ///     true:""
        ///     false:操作失败
        /// <returns></returns>
        private string SetUserAddress(DataRow row)
        {
            if (!row.Table.Columns.Contains("Mobile") || string.IsNullOrEmpty(row["Mobile"].ToString()))
            {
                return JSONHelper.FromString(false, "联系方式不能为空");
            }
            if (!row.Table.Columns.Contains("UserId") || string.IsNullOrEmpty(row["UserId"].ToString()))
            {
                return JSONHelper.FromString(false, "用户编码不能为空");
            }
            if (!row.Table.Columns.Contains("UserName") || string.IsNullOrEmpty(row["UserName"].ToString()))
            {
                return JSONHelper.FromString(false, "收货人不能为空");
            }
            if (!row.Table.Columns.Contains("Address") || string.IsNullOrEmpty(row["Address"].ToString()))
            {
                return JSONHelper.FromString(false, "配送地址不能为空");
            }

            using (var conn = new SqlConnection(PubConstant.BusinessContionString))
            {
                var Address = new Tb_User_Address();
                Address.Id = Guid.NewGuid().ToString();
                Address.Mobile = row["Mobile"].ToString();
                Address.UserId = row["UserId"].ToString();
                Address.UpdataTime = DateTime.Now;
                Address.IsDefault = 0;
                Address.Address = row["Address"].ToString();
                Address.UserName = row["UserName"].ToString();

                if (row.Table.Columns.Contains("BussId") && string.IsNullOrEmpty(row["BussId"].ToString()))
                {
                    Address.BussId = AppGlobal.StrToInt(row["BussId"].ToString());
                }

                conn.Insert<Tb_User_Address>(Address);

                var sql = @"IF (SELECT count(1) FROM Tb_User_Address 
                                WHERE UserId=@UserId And isnull(IsDefault,0)=0 AND isnull(IsDelete,0)=0)=0
                                BEGIN
                                    UPDATE Tb_User_Address SET IsDefault=1 WHERE Id=@Id
                                END;";

                conn.Query(sql, new { UserId = Address.UserId, Id = Address.Id });

                return JSONHelper.FromString(true, "保存成功");
            }
        }
    }
}
