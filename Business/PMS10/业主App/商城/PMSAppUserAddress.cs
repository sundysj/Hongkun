using App.Model;
using Business.PMS10.业主App.商城.Model;
using Business.PMS10.业主App.缴费.Model;
using Common;
using Dapper;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using MobileSoft.Model.Unified;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using static Dapper.SqlMapper;

namespace Business
{
    public class PMSAppUserAddress : PubInfo
    {
        public PMSAppUserAddress()
        {
            base.Token = "20200311PMSAppUserAddress";
        }

        public override void Operate(ref Transfer Trans)
        {
            DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
            DataRow Row = dAttributeTable.Rows[0];

            //防止未捕获异常出现
            try
            {
                switch (Trans.Command)
                {
                    case "GetUserAddress":              // 获取用户收货地址
                        Trans.Result = GetUserAddress(Row);
                        break;
                    case "GetDefaultUserAddress":       // 获取用户默认收货地址
                        Trans.Result = GetDefaultUserAddress(Row);
                        break;
                    case "SaveUserAddress":             // 保存收货地址
                        Trans.Result = SaveUserAddress(Row);
                        break;

                    default:
                        Trans.Result = new ApiResult(false, "未知错误").toJson();
                        break;
                }
            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace + Environment.NewLine + ex.Source);
                Trans.Result = new ApiResult(false, ex.Message + ex.StackTrace).toJson();
            }
        }

        /// <summary>
        /// 获取用户收货地址
        /// </summary>
        private string GetUserAddress(DataRow row)
        {
            if (!row.Table.Columns.Contains("UserId") || string.IsNullOrEmpty(row["UserId"].ToString()))
            {
                return JSONHelper.FromString(false, "用户编码不能为空");
            }

            var userId = row["UserId"].ToString();

            using (var bizConn = new SqlConnection(PubConstant.BusinessContionString))
            {
                var sql = @"SELECT Id,BussId,UserId,UserName,Address,Mobile,IsDefault,
                                CommID,CommName,BuildUnitName,RoomName,Remark 
                            FROM Tb_User_Address 
                            WHERE UserId=@UserId AND isnull(IsDelete,0)=0  
                            ORDER BY IsDefault DESC,UpdataTime DESC;";

                var data = bizConn.Query(sql, new { UserId = userId });

                return new ApiResult(true, data).toJson();
            }
        }

        /// <summary>
        /// 获取默认收货地址
        /// </summary>
        private string GetDefaultUserAddress(DataRow row)
        {
            if (!row.Table.Columns.Contains("UserId") || string.IsNullOrEmpty(row["UserId"].ToString()))
            {
                return JSONHelper.FromString(false, "用户编码不能为空");
            }

            var userId = row["UserId"].ToString();

            using (var bizConn = new SqlConnection(PubConstant.BusinessContionString))
            {
                var sql = @"SELECT Id,BussId,UserId,UserName,Address,Mobile,
                                CommID,CommName,BuildUnitName,RoomName,Remark 
                            FROM Tb_User_Address 
                            WHERE UserId=@UserId AND isnull(IsDelete,0)=0 AND IsDefault=1;";

                var data = bizConn.Query(sql, new { UserId = userId }).FirstOrDefault();

                return new ApiResult(true, data).toJson();
            }
        }

        /// <summary>
        /// 保存收货地址
        /// </summary>
        private string SaveUserAddress(DataRow row)
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
            if (!row.Table.Columns.Contains("CommID") && string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "配送小区不能为空");
            }
            if (!row.Table.Columns.Contains("BuildUnitName") || string.IsNullOrEmpty(row["BuildUnitName"].ToString()))
            {
                return JSONHelper.FromString(false, "楼栋单元不能为空");
            }
            if (!row.Table.Columns.Contains("RoomName") || string.IsNullOrEmpty(row["RoomName"].ToString()))
            {
                return JSONHelper.FromString(false, "房屋名称不能为空");
            }

            var mobile = row["Mobile"].ToString();
            var userId = row["UserId"].ToString();
            var userName = row["UserName"].ToString();
            var address = row["Address"].ToString();

            var iid = default(string);
            var bussId = 0;
            var commId = AppGlobal.StrToInt(row["CommID"].ToString());
            var buildUnitName = row["BuildUnitName"].ToString();
            var roomName = row["RoomName"].ToString();
            var remark = default(string);
            if (row.Table.Columns.Contains("Id") && !string.IsNullOrEmpty(row["Id"].ToString()))
            {
                iid = row["Id"].ToString();
            }
            if (row.Table.Columns.Contains("BussId") && !string.IsNullOrEmpty(row["BussId"].ToString()))
            {
                bussId = AppGlobal.StrToInt(row["BussId"].ToString());
            }
            if (row.Table.Columns.Contains("Remark") && !string.IsNullOrEmpty(row["Remark"].ToString()))
            {
                remark = row["Remark"].ToString();
            }

            using (var conn = new SqlConnection(PubConstant.BusinessContionString))
            {
                var sql = @"DECLARE @CommName varchar(50);
                            SELECT @CommName=CommName FROM Unified.dbo.Tb_Community WHERE CommID=@CommID;

                            UPDATE Tb_User_Address SET BussId=@BussId,
                                UserName=@UserName,Address=@Address,Mobile=@Mobile,UpdataTime=getdate(),
                                CommID=@CommID,CommName=@CommName,BuildUnitName=@BuildUnitName,
                                RoomName=@RoomName,Remark=@Remark 
                            WHERE Id=@Id;";
                if (string.IsNullOrEmpty(iid))
                {
                    iid = Guid.NewGuid().ToString();

                    sql = @"DECLARE @CommName varchar(50);
                            SELECT @CommName=CommName FROM Unified.dbo.Tb_Community WHERE CommID=@CommID;

                            INSERT INTO Tb_User_Address(Id,BussId,UserId,UserName,Address,Mobile,UpdataTime,IsDefault,
                                CommID,CommName,BuildUnitName,RoomName,Remark)
                            VALUES(@Id,@BussId,@UserId,@UserName,@Address,@Mobile,getdate(),0,@CommID,
                                @CommName,@BuildUnitName,@RoomName,@Remark);

                            IF (SELECT count(1) FROM Tb_User_Address WHERE UserId=@UserId)=1
                                UPDATE Tb_User_Address SET IsDefault=1 WHERE Id=@Id;";
                }

                conn.Execute(sql, new
                {
                    Id = iid,
                    BussId = bussId,
                    UserId = userId,
                    UserName = userName,
                    Address = address,
                    Mobile = mobile,
                    CommID = commId,
                    BuildUnitName = buildUnitName,
                    RoomName = roomName,
                    Remark = remark
                });

                return JSONHelper.FromString(true, "保存成功");
            }
        }
    }
}
