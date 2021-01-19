using Business.PMS10.物管App.人员出入.Models;
using Common;
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
using static Dapper.SqlMapper;

namespace Business
{
    public class PMSPersonInOutManage : PubInfo
    {
        public PMSPersonInOutManage()
        {
            base.Token = "20200215PMSPersonInOutManage";
        }

        public override void Operate(ref Transfer Trans)
        {
            DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
            DataRow Row = dAttributeTable.Rows[0];
            //验证登录
            if (!new Login().isLogin(ref Trans))
                return;

            //防止未捕获异常出现
            try
            {
                switch (Trans.Command)
                {
                    case "GetAdditionalInfoList":
                        Trans.Result = GetAdditionalInfoList(Row);
                        break;
                    case "PersonRegistration":
                        Trans.Result = PersonRegistration(Row);
                        break;
                    case "ScanPassCard":
                        Trans.Result = ScanPassCard(Row);
                        break;
                    case "ScanPassCard_v2":
                        Trans.Result = ScanPassCard_v2(Row);
                        break;
                    case "AddInOutRecord":
                        Trans.Result = AddInOutRecord(Row);
                        break;
                    case "GetInOutRecordList":
                        Trans.Result = GetInOutRecordList(Row);
                        break;
                    case "GetInOutPurpose":
                        Trans.Result = GetInOutPurpose(Row);
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
        /// 获取人员申报/放行额外记录信息
        /// </summary>
        private string GetAdditionalInfoList(DataRow row)
        {
            var type = default(string);
            if (row.Table.Columns.Contains("Type") && !string.IsNullOrEmpty(row["Type"].AsString()))
            {
                type = row["Type"].AsString();
            }

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var condition = "";
                if (type == "0")        // 住户自行申报
                    condition = " AND IsUseOneselfRegistration=1 ";
                else if (type == "1")   // 物业人员申报
                    condition = " AND IsUsePersonRegistration=1 ";
                else if (type == "2")   // 放出
                    condition = " AND IsUseOutRecord=1 ";
                else if (type == "3")   // 放入
                    condition = " AND IsUseInRecord=1 ";

                var sql = $@"SELECT convert(varchar(36),IID) AS IID,InfoTitle,IsOption 
                             FROM Tb_IOM_AdditionalInfoSetting WHERE IsDelete=0 {condition} ORDER BY Sort;";

                var data = conn.Query<PMSIOMAdditionalInfoModel>(sql);
                if (data.Count() > 0)
                {
                    sql = @"SELECT convert(varchar(36),IID) AS IID,OptionValue,IsAbnormal
                            FROM Tb_IOM_AdditionalInfoOption
                            WHERE AdditionalInfoID=@IID AND IsDelete=0 ORDER BY Sort;";

                    foreach (var item in data)
                    {
                        var tmp = conn.Query<PMSIOMAdditionalInfoOptionModel>(sql, new { IID = item.IID });
                        item.Options = new List<PMSIOMAdditionalInfoOptionModel>();
                        item.Options.AddRange(tmp);
                    }
                }

                return new ApiResult(true, data).toJson();
            }
        }

        /// <summary>
        /// 人员申报
        /// </summary>
        private string PersonRegistration(DataRow row)
        {
            #region 基础数据验证
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].AsString()))
            {
                return new ApiResult(false, "缺少参数CommID").toJson();
            }
            if (!row.Table.Columns.Contains("CustID") || string.IsNullOrEmpty(row["CustID"].AsString()))
            {
                return new ApiResult(false, "缺少参数CustID").toJson();
            }
            if (!row.Table.Columns.Contains("RoomID") || string.IsNullOrEmpty(row["RoomID"].AsString()))
            {
                return new ApiResult(false, "缺少参数RoomID").toJson();
            }
            if (!row.Table.Columns.Contains("CustName") || string.IsNullOrEmpty(row["CustName"].AsString()))
            {
                return new ApiResult(false, "缺少参数CustName").toJson();
            }
            if (!row.Table.Columns.Contains("PersonName") || string.IsNullOrEmpty(row["PersonName"].AsString()))
            {
                return new ApiResult(false, "缺少参数PersonName").toJson();
            }
            if (!row.Table.Columns.Contains("Gender") || string.IsNullOrEmpty(row["Gender"].AsString()))
            {
                return new ApiResult(false, "缺少参数Gender").toJson();
            }
            if (!row.Table.Columns.Contains("IDNumber") || string.IsNullOrEmpty(row["IDNumber"].AsString()))
            {
                return new ApiResult(false, "缺少参数IDNumber").toJson();
            }
            if (!row.Table.Columns.Contains("Phone") || string.IsNullOrEmpty(row["Phone"].AsString()))
            {
                return new ApiResult(false, "缺少参数Phone").toJson();
            }
            if (!row.Table.Columns.Contains("Relation") || string.IsNullOrEmpty(row["Relation"].AsString()))
            {
                return new ApiResult(false, "缺少参数Relation").toJson();
            }
            if (!row.Table.Columns.Contains("Whence") || string.IsNullOrEmpty(row["Whence"].AsString()))
            {
                return new ApiResult(false, "缺少参数Whence").toJson();
            }

            var commId = AppGlobal.StrToInt(row["CommID"].AsString());
            var custId = AppGlobal.StrToLong(row["CustID"].AsString());
            var roomId = AppGlobal.StrToLong(row["RoomID"].AsString());
            var custName = row["CustName"].AsString();
            var personName = row["PersonName"].AsString();
            var gender = AppGlobal.StrToInt(row["Gender"].AsString());
            var idNumber = row["IDNumber"].AsString();
            var phone = row["Phone"].AsString();
            var relation = row["Relation"].AsString();
            var whence = row["Whence"].AsString();

            var remark = default(string);
            if (row.Table.Columns.Contains("Remark") && !string.IsNullOrEmpty(row["Remark"].AsString()))
            {
                remark = row["Remark"].AsString();
            }

            var additionalInfo = default(string);
            if (row.Table.Columns.Contains("AdditionalInfo") && !string.IsNullOrEmpty(row["AdditionalInfo"].AsString()))
            {
                additionalInfo = row["AdditionalInfo"].AsString();
            }
            #endregion

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                conn.Open();
                var trans = conn.BeginTransaction();

                try
                {
                    var sql = @"SELECT PersonName FROM Tb_IOM_PersonRegistration WHERE RoomID=@RoomID AND IDNumber=@IDNumber AND IsDelete=0";
                    var persons = conn.Query<string>(sql, new { RoomID = roomId, IDNumber = idNumber }, trans);
                    if (persons.Count() > 0)
                    {
                        return JSONHelper.FromString(false, $"该身份已在本项目登记，登记名为：{persons.First()}");
                    }

                    var regId = Guid.NewGuid().ToString();
                    var cardId = Guid.NewGuid().ToString();

                    sql = @"INSERT INTO Tb_IOM_PersonRegistration(IID,CommID,CustID,CustName,RoomID,PersonName,Gender,
                                IDNumber,Phone,Relation,Whence,RegistrationUser,Remark) 
                            VALUES(@IID,@CommID,@CustID,@CustName,@RoomID,@PersonName,@Gender,
                                @IDNumber,@Phone,@Relation,@Whence,@RegistrationUser,@Remark);";

                    var i = conn.Execute(sql, new
                    {
                        IID = regId,
                        CommID = commId,
                        CustID = custId,
                        CustName = custName,
                        RoomID = roomId,
                        PersonName = personName,
                        Gender = gender,
                        IDNumber = idNumber,
                        Phone = phone,
                        Relation = relation,
                        Whence = whence,
                        RegistrationUser = Global_Var.LoginUserCode,
                        Remark = remark
                    }, trans);


                    if (i == 1)
                    {
                        // 添加出入证记录信息
                        sql = @"DECLARE @PassNo varchar(12);
                                SELECT @PassNo=dbo.PadLeft(convert(varchar(12),isnull(max(convert(bigint,PassNo)),0)+1),'0',6) 
                                FROM Tb_IOM_PassCard WHERE CommID=@CommID AND IsDelete=0;

                                INSERT INTO Tb_IOM_PassCard(IID,CommID,RoomID,PersonRegistrationID,PassNo,AddUser)
                                VALUES(@IID,@CommID,@RoomID,@PersonRegistrationID,@PassNo,@AddUser);";

                        conn.Execute(sql, new
                        {
                            IID = cardId,
                            CommID = commId,
                            RoomID = roomId,
                            PersonRegistrationID = regId,
                            AddUser = Global_Var.LoginUserCode
                        }, trans);

                        // 存在额外登记信息
                        sql = @"INSERT INTO Tb_IOM_PersonRegistrationAdditionalInfo(IID,PersonRegistrationID,AdditionalInfoID,Result) 
                                VALUES(newid(),@PersonRegistrationID,@AdditionalInfoID,@Result);";

                        var jArray = (JArray)JsonConvert.DeserializeObject(additionalInfo);
                        foreach (var item in jArray)
                        {
                            conn.Execute(sql, new
                            {
                                PersonRegistrationID = regId,
                                AdditionalInfoID = item["IID"].ToString(),
                                Result = item["Value"].ToString()
                            }, trans);
                        }
                    }

                    trans.Commit();
                    return JSONHelper.FromString(true, cardId);
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    return new ApiResult(false, ex.Message + ex.StackTrace).toJson();
                }
            }
        }

        /// <summary>
        /// 扫描出入证二维码
        /// </summary>
        private string ScanPassCard(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].AsString()))
            {
                return new ApiResult(false, "缺少参数CommID").toJson();
            }
            if (!row.Table.Columns.Contains("IID") || string.IsNullOrEmpty(row["IID"].AsString()))
            {
                return new ApiResult(false, "无效的出入证二维码").toJson();
            }

            var commId = AppGlobal.StrToInt(row["CommID"].AsString());
            var iid = row["IID"].AsString();

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = @"SELECT CommID,InvalidTime,IsDelete FROM Tb_IOM_PassCard WHERE IID=@IID;";

                var data = conn.Query(sql, new { IID = iid }).FirstOrDefault();

                if (data == null)
                    return JSONHelper.FromString(false, "未能识别此二维码");
                else if (data.CommID != commId)
                    return JSONHelper.FromString(false, "此出入证不是本项目的出入证");
                else if (data.IsDelete == true)
                    return JSONHelper.FromString(false, "此出入证已失效，请联系物业处理");
                else if (data.InvalidTime < DateTime.Now)
                    return JSONHelper.FromString(false, "此出入证已过期，请联系物业处理");

                sql = @"SELECT isnull(col_length('Tb_IOM_PassCard', 'PassCardType'),0);";

                if (conn.Query<int>(sql).FirstOrDefault() > 0)
                {
                    sql = @"SELECT b.IID AS PassCardID,a.PersonName,convert(int,a.Gender) AS Gender,isnull(a.IDNumber,'') AS IDNumber,
                                a.Phone,(b.PassNo+'【'+b.PassCardType+'】') AS PassNo,c.RoomSign,c.RoomName
                            FROM Tb_IOM_PersonRegistration a
                            LEFT JOIN Tb_IOM_PassCard b ON a.IID=b.PersonRegistrationID AND a.IsDelete=0
                            LEFT JOIN Tb_HSPR_Room c ON a.RoomID=c.RoomID
                            WHERE b.IID=@PassCardID;";
                }
                else
                {
                    sql = @"SELECT b.IID AS PassCardID,a.PersonName,convert(int,a.Gender) AS Gender,isnull(a.IDNumber,'') AS IDNumber,
                                a.Phone,(b.PassNo+'【'+c.TypeName+'】') AS PassNo,d.RoomSign,d.RoomName
                            FROM Tb_IOM_PersonRegistration a
                            LEFT JOIN Tb_IOM_PassCard b ON a.IID=b.PersonRegistrationID AND a.IsDelete=0
                            LEFT JOIN Tb_IOM_PassCardType c ON b.PassTypeCode=c.TypeCode AND c.IsDelete=0
                            LEFT JOIN Tb_HSPR_Room d ON a.RoomID=d.RoomID
                            WHERE b.IID=@PassCardID;";
                }

                var passCardInfo = conn.Query(sql, new { PassCardID = iid }).FirstOrDefault();

                var idNumber = ((string)passCardInfo.IDNumber).Trim();
                if (idNumber != null && idNumber.Length >= 10)
                {
                    var i = new int[] { 10, 3, 4 };
                    var j = new int[] { 15, 6, 4 };
                    var k = new int[] { 18, 6, 4 };

                    var len = idNumber.Length;
                    var tmp = len > i[0] ? (len > j[0] ? k : j) : i;

                    idNumber = idNumber.Substring(0, tmp[1]) + idNumber.Substring(len - tmp[2], tmp[2]).PadLeft(len - (tmp[1]), '*');

                    passCardInfo.IDNumber = idNumber;
                }

                return new ApiResult(true, passCardInfo).toJson();
            }
        }

        /// <summary>
        /// 扫描出入证二维码
        /// </summary>
        private string ScanPassCard_v2(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].AsString()))
            {
                return new ApiResult(false, "缺少参数CommID").toJson();
            }
            if (!row.Table.Columns.Contains("IID") || string.IsNullOrEmpty(row["IID"].AsString()))
            {
                return new ApiResult(false, "无效的出入证二维码").toJson();
            }

            var commId = AppGlobal.StrToInt(row["CommID"].AsString());
            var iid = row["IID"].AsString();

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = @"SELECT CommID,InvalidTime,IsDelete FROM Tb_IOM_PassCard WHERE IID=@IID;";

                var data = conn.Query(sql, new { IID = iid }).FirstOrDefault();

                if (data == null)
                    return JSONHelper.FromString(false, "未能识别此二维码");
                else if (data.CommID != commId)
                    return JSONHelper.FromString(false, "此出入证不是本项目的出入证");
                else if (data.IsDelete == true)
                    return JSONHelper.FromString(false, "此出入证已失效，请联系物业处理");
                else if (data.InvalidTime < DateTime.Now)
                    return JSONHelper.FromString(false, "此出入证已过期，请联系物业处理");

                sql = @"SELECT b.IID AS PassCardID,a.PersonName,convert(int,a.Gender) AS Gender,isnull(a.IDNumber,'') AS IDNumber,
                            a.Phone,a.Relation,b.PassNo,b.PassTypeCode,c.TypeName AS PassTypeName,a.CommID,a.RoomID,d.RoomSign,d.RoomName
                        FROM Tb_IOM_PersonRegistration a
                        LEFT JOIN Tb_IOM_PassCard b ON a.IID=b.PersonRegistrationID AND a.IsDelete=0
                        LEFT JOIN Tb_IOM_PassCardType c ON b.PassTypeCode=c.TypeCode AND c.IsDelete=0
                        LEFT JOIN Tb_HSPR_Room d ON a.RoomID=d.RoomID
                        WHERE b.IID=@PassCardID;";

                var passCardInfo = conn.Query(sql, new { PassCardID = iid }).FirstOrDefault();

                var idNumber = ((string)passCardInfo.IDNumber).Trim();
                if (idNumber != null && idNumber.Length >= 10)
                {
                    var i = new int[] { 10, 3, 4 };
                    var j = new int[] { 15, 6, 4 };
                    var k = new int[] { 18, 6, 4 };

                    var len = idNumber.Length;
                    var tmp = len > i[0] ? (len > j[0] ? k : j) : i;

                    idNumber = idNumber.Substring(0, tmp[1]) + idNumber.Substring(len - tmp[2], tmp[2]).PadLeft(len - (tmp[1]), '*');

                    passCardInfo.IDNumber = idNumber;
                }

                // 出行管控
                sql = @"SELECT PassTypeCode,CollectHealthData,ControlObject,InOutLimit,InOutWarningInterval
                        FROM Tb_IOM_InOutControl
                        WHERE CommID=@CommID AND PassTypeCode=@PassTypeCode AND IsDelete=0;";

                var control = conn.Query<PMSIOMInOutRecordControl>(sql, new
                {
                    CommID = commId,
                    PassTypeCode = passCardInfo.PassTypeCode
                }).FirstOrDefault();

                if (control == null)
                {
                    control = control ?? PMSIOMInOutRecordControl.Default;
                };

                passCardInfo.Control = PMSIOMInOutRecordControl.Default;


                // 最后一次通行记录
                if (control.ControlObject == 1)
                {
                    // 管控到房屋
                    sql = @"DECLARE @RoomID bigint;
                            DECLARE @PassTypeCode varchar(4);
                            SELECT @RoomID=RoomID,@PassTypeCode=PassTypeCode FROM Tb_IOM_PassCard WHERE IID=@PassCardID;
                            
                            SELECT TOP 1 b.UserName AS ReleaseUser,convert(varchar(16),a.ReleaseTime,120) AS ReleaseTime,
                                CASE a.ActionType WHEN 1 THEN '放出' ELSE '放入' END AS ActionName,
                                (
                                    SELECT count(1) FROM Tb_IOM_InOutRecord x
                                    WHERE x.RoomID=@RoomID AND x.ActionType=0 AND x.IsDelete=0
                                    AND PassCardID IN
                                    (
                                        SELECT IID FROM Tb_IOM_PassCard
                                        WHERE RoomID=@RoomID AND PassTypeCode=@PassTypeCode AND IsDelete=0
                                    )
                                ) AS InCount,
                                (
                                    SELECT count(1) FROM Tb_IOM_InOutRecord x
                                    WHERE x.RoomID=@RoomID AND x.ActionType=1 AND x.IsDelete=0
                                    AND PassCardID IN
                                    (
                                        SELECT IID FROM Tb_IOM_PassCard
                                        WHERE RoomID=@RoomID AND PassTypeCode=@PassTypeCode AND IsDelete=0
                                    )
                                ) AS OutCount
                            FROM Tb_IOM_InOutRecord a
                            LEFT JOIN Tb_Sys_User b ON a.ReleaseUser=b.UserCode 
                            LEFT JOIN Tb_IOM_PassCard c ON a.PassCardID=c.IID
                            WHERE a.RoomID=@RoomID AND c.PassTypeCode=@PassTypeCode 
                            ORDER BY a.ReleaseTime DESC;";
                }
                else
                {
                    // 管控到卡
                    sql = @"SELECT TOP 1 b.UserName AS ReleaseUser,convert(varchar(16),a.ReleaseTime,120) AS ReleaseTime,
                                CASE a.ActionType WHEN 1 THEN '放出' ELSE '放入' END AS ActionName,
                                (
                                    SELECT count(1) FROM Tb_IOM_InOutRecord x
                                    WHERE x.PassCardID=@PassCardID AND x.ActionType=0 AND x.IsDelete=0
                                ) AS InCount,
                                (
                                    SELECT count(1) FROM Tb_IOM_InOutRecord x
                                    WHERE x.PassCardID=@PassCardID AND x.ActionType=1 AND x.IsDelete=0
                                ) AS OutCount 
                            FROM Tb_IOM_InOutRecord a
                            LEFT JOIN Tb_Sys_User b ON a.ReleaseUser=b.UserCode 
                            LEFT JOIN Tb_IOM_PassCard c ON a.PassCardID=c.IID
                            WHERE PassCardID=@PassCardID ORDER BY a.ReleaseTime DESC;";
                }

                passCardInfo.LastInOutRecord = conn.Query(sql, new { PassCardID = iid }).FirstOrDefault();
                passCardInfo.CanRelease = (object)1;
                passCardInfo.Warning = (object)0;
                passCardInfo.WarningMessage = default(string);

                // 提醒信息，对比卡类型
                if (passCardInfo.LastInOutRecord != null && passCardInfo.PassTypeCode == control.PassTypeCode)
                {
                    if (control.InOutWarningInterval >= 0)
                    {
                        var date1 = Convert.ToDateTime(passCardInfo.LastInOutRecord.ReleaseTime);
                        var date2 = DateTime.Now;
                        if ((date2 - date1).Days < control.InOutWarningInterval)
                        {
                            var action = passCardInfo.LastInOutRecord.ActionName == "放出" ? "外出" : "进入";

                            passCardInfo.Warning = (object)1;
                            passCardInfo.WarningMessage = $"此二维码在{ control.InOutWarningInterval }天内已有{action}记录";
                        }
                    }

                    if (control.InOutLimit >= 0)
                    {
                        if (passCardInfo.LastInOutRecord.InCount > control.InOutLimit ||
                            passCardInfo.LastInOutRecord.OutCount > control.InOutLimit)
                        {
                            passCardInfo.Warning = (object)1;
                            passCardInfo.WarningMessage = $"此二维码已达到出入使用限制次数";
                        }
                    }
                }

                return new ApiResult(true, passCardInfo).toJson();
            }
        }

        /// <summary>
        /// 人员放行登记
        /// </summary>
        private string AddInOutRecord(DataRow row)
        {
            if (!row.Table.Columns.Contains("IID") || string.IsNullOrEmpty(row["IID"].AsString()))
            {
                return new ApiResult(false, "无效的出入证二维码").toJson();
            }
            if (!row.Table.Columns.Contains("Action") || string.IsNullOrEmpty(row["Action"].AsString()))
            {
                return new ApiResult(false, "缺少参数Action").toJson();
            }
            if (!row.Table.Columns.Contains("BodyTemperature") || string.IsNullOrEmpty(row["BodyTemperature"].AsString()))
            {
                return new ApiResult(false, "缺少参数BodyTemperature").toJson();
            }

            var iid = row["IID"].AsString();
            var action = AppGlobal.StrToInt(row["Action"].AsString());
            var bodyTemperature = AppGlobal.StrToDec(row["BodyTemperature"].AsString());

            var recordTime = DateTime.Now;
            if (row.Table.Columns.Contains("RecordTime") && !string.IsNullOrEmpty(row["RecordTime"].AsString()))
            {
                recordTime = Convert.ToDateTime(row["RecordTime"].AsString());
                var interval = (recordTime - DateTime.Now).TotalMinutes;
                if (interval > 10 || interval < -30)
                {
                    return JSONHelper.FromString(false, "记录时间有误，请重新记录");
                }
            }

            var purpose = default(string);
            if (row.Table.Columns.Contains("Purpose") && !string.IsNullOrEmpty(row["Purpose"].AsString()))
            {
                purpose = row["Purpose"].AsString();
            }

            var additionalInfo = default(string);
            if (row.Table.Columns.Contains("AdditionalInfo") && !string.IsNullOrEmpty(row["AdditionalInfo"].AsString()))
            {
                additionalInfo = row["AdditionalInfo"].AsString();
            }

            var roomId = 0;
            if (row.Table.Columns.Contains("RoomID") && !string.IsNullOrEmpty(row["RoomID"].AsString()))
            {
                roomId = AppGlobal.StrToInt(row["RoomID"].AsString());
            }

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                conn.Open();
                var trans = conn.BeginTransaction();

                try
                {
                    var recordId = Guid.NewGuid().ToString();

                    var sql = @"INSERT INTO Tb_IOM_InOutRecord(IID,CommID,RoomID,PersonName,Gender,IDNumber,PassCardID,ActionType,BodyTemperature,ReleaseUser,Purpose)
                                SELECT @RecordID,b.CommID,b.RoomID,b.PersonName,b.Gender,b.IDNumber,a.IID,@ActionType,@BodyTemperature,@ReleaseUser,@Purpose
                                FROM Tb_IOM_PassCard a 
                                LEFT JOIN Tb_IOM_PersonRegistration b ON a.PersonRegistrationID=b.IID
                                WHERE a.IID=@PassCardID;";

                    var i = conn.Execute(sql, new
                    {
                        RecordID = recordId,
                        ActionType = action,
                        BodyTemperature = bodyTemperature,
                        ReleaseUser = Global_Var.LoginUserCode,
                        PassCardID = iid,
                        Purpose = purpose
                    }, trans);

                    if (i == 1 && !string.IsNullOrEmpty(additionalInfo))
                    {
                        // 存在额外登记信息
                        sql = @"INSERT INTO Tb_IOM_InOutRecordAdditionalInfo(IID,InOutRecordID,AdditionalInfoID,Result) 
                                VALUES(newid(),@InOutRecordID,@AdditionalInfoID,@Result);";

                        var jArray = (JArray)JsonConvert.DeserializeObject(additionalInfo);
                        foreach (var item in jArray)
                        {
                            conn.Execute(sql, new
                            {
                                InOutRecordID = recordId,
                                AdditionalInfoID = item["IID"].ToString(),
                                Result = item["Value"].ToString()
                            }, trans);
                        }
                    }

                    if (roomId != 0)
                    {
                        sql = @"DECLARE @PersonRegistrationID varchar(36);
                                SELECT @PersonRegistrationID=PersonRegistrationID FROM Tb_IOM_PassCard WHERE IID=@PassCardID;

                                UPDATE Tb_IOM_PassCard SET RoomID=@RoomID WHERE IID=@PassCardID;
                                UPDATE Tb_IOM_PersonRegistration SET RoomID=@RoomID WHERE IID=@PersonRegistrationID;";

                        conn.Execute(sql, new { RoomID = roomId, PassCardID = iid });
                    }

                    trans.Commit();
                    return JSONHelper.FromString(true, "信息记录成功，请放行");
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    return new ApiResult(false, ex.Message + ex.StackTrace).toJson();
                }
            }
        }

        /// <summary>
        /// 获取放行记录
        /// </summary>
        private string GetInOutRecordList(DataRow row)
        {
            if (!row.Table.Columns.Contains("IID") || string.IsNullOrEmpty(row["IID"].AsString()))
            {
                return new ApiResult(false, "无效的出入证二维码").toJson();
            }

            var iid = row["IID"].AsString();
            var pageSize = 20;
            var pageIndex = 1;

            //if (row.Table.Columns.Contains("PageSize") && !string.IsNullOrEmpty(row["PageSize"].AsString()))
            //{
            //    pageSize = AppGlobal.StrToInt(row["PageSize"].AsString());
            //    pageSize = (pageSize <= 0 ? 10 : pageSize);
            //}

            if (row.Table.Columns.Contains("PageIndex") && !string.IsNullOrEmpty(row["PageIndex"].AsString()))
            {
                pageIndex = AppGlobal.StrToInt(row["PageIndex"].AsString());
                pageIndex = (pageIndex <= 0 ? 1 : pageIndex);
            }

            //var conditoin = "";
            //if (row.Table.Columns.Contains("CardType") && !string.IsNullOrEmpty(row["CardType"].AsString()))
            //{
            //    var cardType = row["CardType"].AsString();
            //    if (string.IsNullOrEmpty(cardType))
            //    {
            //        cardType = "普通卡";
            //    }
            //    if (!string.IsNullOrEmpty(cardType))
            //    {
            //        conditoin = $" AND b.PassCardType='{cardType}' ";
            //    }
            //}

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = @"SELECT PassTypeCode FROM Tb_IOM_PassCard WHERE IID=@PassCardID;";
                var passTypeCode = conn.Query<string>(sql, new { PassCardID = iid, }).FirstOrDefault();

                sql = @"SELECT * FROM
                        (
                            SELECT a.PersonName,d.UserName AS ReleaseUser,a.ReleaseTime,a.BodyTemperature,a.Purpose,
                                CASE WHEN a.BodyTemperature>37.3 OR a.BodyTemperature<35.7 THEN 1 ELSE 0 END AS BodyTemperatureIsAbnormal,
                                CASE a.ActionType WHEN 1 THEN '出' ELSE '入' END AS ActionName,
                                (SELECT count(1) FROM Tb_IOM_InOutRecordAdditionalInfo x
                                    LEFT JOIN Tb_IOM_AdditionalInfoSetting y ON x.AdditionalInfoID=y.IID
                                    LEFT JOIN Tb_IOM_AdditionalInfoOption z ON x.Result=convert(varchar(36),z.IID)
                                    WHERE x.InOutRecordID=a.IID AND z.IsAbnormal=1) AS AbnormaCount,
                                row_number() OVER (ORDER BY a.ReleaseTime DESC) AS RID
                            FROM Tb_IOM_InOutRecord a
                            LEFT JOIN Tb_IOM_PassCard b ON a.PassCardID=b.IID /*{conditoin}*/
                            LEFT JOIN Tb_IOM_PersonRegistration c ON b.PersonRegistrationID=c.IID
                            LEFT JOIN Tb_Sys_User d ON a.ReleaseUser=d.UserCode
                            WHERE a.RoomID IN
                            (
                                SELECT RoomID FROM Tb_IOM_PassCard WHERE IID=@PassCardID
                            ) AND a.IsDelete=0 AND b.PassTypeCode=@PassTypeCode 
                        ) AS t
                        WHERE t.RID>(@PageIndex-1)*@PageSize AND t.RID<=(@PageIndex*@PageSize);";

                var data = conn.Query(sql, new { PassCardID = iid, PassTypeCode = passTypeCode, PageSize = pageSize, PageIndex = pageIndex });

                return new ApiResult(true, data).toJson();
            }
        }

        /// <summary>
        /// 获取出入目的
        /// </summary>
        private string GetInOutPurpose(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].AsString()))
            {
                return new ApiResult(false, "缺少CommID参数").toJson();
            }

            var commId = AppGlobal.StrToInt(row["CommID"].AsString());

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = @"SELECT Purpose FROM Tb_IOM_InOutPurpose WHERE (CommID=@CommID OR CommID=0) AND ActionType=1 AND IsDelete=0;
                            SELECT Purpose FROM Tb_IOM_InOutPurpose WHERE (CommID=@CommID OR CommID=0) AND ActionType=0 AND IsDelete=0;";

                var reader = conn.QueryMultiple(sql, new { CommID = commId });
                var inPurpose = reader.Read<string>();
                var outPurpose = reader.Read<string>();

                return new ApiResult(true, new { InPurpose = inPurpose, OutPurpose = outPurpose }).toJson();
            }
        }
    }
}
