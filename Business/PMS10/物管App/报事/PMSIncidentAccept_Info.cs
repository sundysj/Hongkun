using Business.PMS10.报事.Models;
using Business.PMS10.物管App.报事.Models;
using Dapper;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using static Dapper.SqlMapper;

namespace Business
{
    public partial class PMSIncidentAccept
    {
        public static PMSIncidentAcceptModel GetIncidentInfo(long incidentId)
        {
            try
            {
                using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
                {
                    var sql = @"SELECT * FROM Tb_HSPR_IncidentAccept WHERE IncidentID=@IncidentID";

                    var incidentInfo = conn.Query<PMSIncidentAcceptModel>(sql, new { IncidentID = incidentId }).FirstOrDefault();
                    if (incidentInfo != null)
                    {
                        sql = @"SELECT CommName FROM Tb_HSPR_Community WHERE CommID=@CommID;
                                SELECT RoomName FROM Tb_HSPR_Room WHERE RoomID=@RoomID;
                                SELECT RegionalPlace FROM Tb_HSPR_IncidentRegional WHERE RegionalID=@RegionalID;";

                        var reader = conn.QueryMultiple(sql, new
                        {
                            CommID = incidentInfo.CommID,
                            RoomID = incidentInfo.RoomID,
                            RegionalID = incidentInfo.RegionalID
                        });

                        incidentInfo.CommName = reader.Read<string>().FirstOrDefault();
                        incidentInfo.RoomName = reader.Read<string>().FirstOrDefault();
                        incidentInfo.RegionalPlace = reader.Read<string>().FirstOrDefault();
                    }

                    return incidentInfo;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取报事详情
        /// </summary>
        private string GetIncidentDetail(DataRow row)
        {
            if (!row.Table.Columns.Contains("IncidentID") || string.IsNullOrEmpty(row["IncidentID"].ToString()))
            {
                return new ApiResult(false, "缺少报事ID").toJson();
            }

            var incidentId = AppGlobal.StrToLong(row["IncidentID"].ToString());

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var chargeDbName = Global_Var.ERPDatabaseName;

                var sql = $@"SELECT isnull(DataBaseName_Read, DataBaseName) FROM Tb_System_Burst
                            WHERE BurstType='CHARGE' AND CommID IN 
                            (
                                SELECT CommID FROM { Global_Var.ERPDatabaseName }.dbo.Tb_HSPR_IncidentAccept WHERE IncidentID={ incidentId }
                            )";

                using (var tw2bsConn = new SqlConnection(PubConstant.tw2bsConnectionString))
                {
                    var tmp = tw2bsConn.Query<string>(sql, new { IncidentID = incidentId }).FirstOrDefault();
                    if (!string.IsNullOrEmpty(tmp))
                    {
                        chargeDbName = tmp;
                    }
                }

                var extraField = "";
                sql = @"SELECT isnull(col_length('Tb_HSPR_CorpIncidentType', 'IsPresentPhoto'),0)";
                if (conn.Query<long>(sql).FirstOrDefault() > 0)
                {
                    extraField = "isnull(k.IsPresentPhoto,0) AS ArriveMustTakePhoto,";
                }

                sql = $@"SELECT a.IncidentID,a.IncidentNum,a.CommID,b.CommName,a.CustID,c.CustName,e.BuildSNum,a.RoomID,d.RoomSign,d.RoomName,
                            a.RegionalID,f.RegionalPlace,g.DictionaryName AS LocalePosition,h.DictionaryName AS LocaleFunction,
                            a.EqID,i.EquipmentName,i.EquipmentNO,j.SpaceId,j.SpaceName,j.SpaceNo,
                            a.IncidentPlace,a.IncidentSource,a.IncidentMan,a.IncidentDate,a.IncidentMode,a.IncidentContent,a.IncidentMemo,
                            a.DrClass,a.IsTouSu,a.Duty,a.EmergencyDegree,a.ReserveDate,a.ReserveLimit,a.DispLimit,
                            a.BigCorpTypeID,a.BigCorpTypeCode,k.TypeName AS BigTypeName,a.Phone,a.AdmiMan,a.AdmiDate,
                            a.CoordinateNum,a.DispMan,a.DispUserCode,a.DispDate,a.DispRemarks,a.DealManCode,a.DealMan,
                            isnull(a.DealLimit,0) AS DealLimit,a.RatedWorkHour,a.RatedWorkHourNumber,a.KPIRatio,
                            a.FineCorpTypeID,a.FineCorpTypeCode,l.TypeName AS FineTypeName,a.IsReWork,a.ReWorkContent,
                            a.IsReceipt,a.ReceiptUserName,a.ReceivingDate,a.ArriveData,a.DealSituation,a.DealState,
                            a.IsFinish,a.FinishUser,m.UserName AS FinishUserName,isnull(a.FinishDate,a.MainEndDate) AS FinishDate,a.MainEndDate,
                            a.ReplyLimit,a.Signatory,a.SignatoryImg,a.SignatoryConfirmImg,a.SignatoryImgDate,a.SignatoryConfirmImgDate,isnull(a.IsFee,0) AS IsFee,
                            isnull(k.TypeName,l.TypeName) AS TypeName,isnull(k.IsUploadPictures,0) AS FinishMustTaskPhoto,

                            {extraField}                  
    
                            /* 协助人 */
                            STUFF((SELECT ',' + UserCode FROM Tb_HSPR_IncidentAssistDetail x
                                WHERE AssistID IN
                                (
                                    SELECT IID FROM Tb_HSPR_IncidentAssistApply x WHERE x.IncidentID={ incidentId } AND x.AuditState='已审核'
                                ) FOR XML PATH('')), 1, 1, '') AS AssistantUserCode,
                            STUFF((SELECT ',' + UserName FROM Tb_Sys_User y WHERE y.UserCode IN
                                (
                                    SELECT x.UserCode FROM Tb_HSPR_IncidentAssistDetail x
                                    WHERE AssistID IN
                                    (
                                        SELECT IID FROM Tb_HSPR_IncidentAssistApply x WHERE x.IncidentID={ incidentId } AND x.AuditState='已审核'
                                    )
                                ) FOR XML PATH('')), 1, 1, '') AS Assistant,

                            /* 费用 */
                            CASE (SELECT count(1) FROM { chargeDbName }.dbo.Tb_HSPR_Fees x WHERE x.CommID=a.CommID AND x.IncidentID={ incidentId })
                            WHEN 0 THEN 0
                            ELSE 1 END AS HasFees,
                            (SELECT isnull(sum(isnull(DueAmount,0.0)),0.0) FROM { chargeDbName }.dbo.Tb_HSPR_Fees x
                                WHERE x.CommID=a.CommID AND x.IncidentID={ incidentId }) AS DueAmount,
                            (SELECT isnull(sum(isnull(DebtsAmount,0.0)),0.0) FROM { chargeDbName }.dbo.Tb_HSPR_Fees x
                                WHERE x.CommID=a.CommID AND x.IncidentID={ incidentId }) AS DebtsAmount,

                            /* 图片 */
                            replace(rtrim(ltrim(isnull(a.IncidentImgs,'')+' '+
                            isnull(STUFF((SELECT ',' + FilPath FROM Tb_HSPR_IncidentAdjunct x
                                WHERE x.IncidentID={ incidentId } AND x.AdjunctName='受理图片' FOR XML PATH('')), 1, 1, ''), ''))),' ',',') AS IncidentImgs,

                            isnull(STUFF((SELECT ',' + FilPath FROM Tb_HSPR_IncidentAdjunct x
                                WHERE x.IncidentID={ incidentId } AND x.AdjunctName='分派图片' FOR XML PATH('')), 1, 1, ''), '') AS AssignIncidentImgs,

                            isnull(STUFF((SELECT ',' + FilPath FROM Tb_HSPR_IncidentAdjunct x
                                WHERE x.IncidentID={ incidentId } AND x.AdjunctName='到场确认图片' FOR XML PATH('')), 1, 1, ''), '') AS ArriveConfirmImgs,

                            isnull(STUFF((SELECT ',' + FilPath FROM Tb_HSPR_IncidentAdjunct x
                                WHERE x.IncidentID={ incidentId } AND x.AdjunctName='跟进图片' FOR XML PATH('')), 1, 1, ''), '') AS FollowImgs,

                            replace(rtrim(ltrim(isnull(a.ProcessIncidentImgs,'')+' '+
                            isnull(STUFF((SELECT ',' + FilPath FROM Tb_HSPR_IncidentAdjunct x
                                WHERE x.IncidentID={ incidentId } AND x.AdjunctName='处理图片' FOR XML PATH('')), 1, 1, ''), ''))),' ',',') AS ProcessIncidentImgs,

                            isnull(STUFF((SELECT ',' + FilPath FROM Tb_HSPR_IncidentAdjunct x
                                WHERE x.IncidentID={ incidentId } AND x.AdjunctName='关闭图片' FOR XML PATH('')), 1, 1, ''), '') AS CloseIncidentImgs,
        
                            /* 权限 */
                            CASE WHEN a.DrClass=2 THEN 0 
                            END AS CanTransmit 

                    FROM Tb_HSPR_IncidentAccept a
                        LEFT JOIN Tb_HSPR_Community b ON a.CommID=b.CommID
                        LEFT JOIN Tb_HSPR_Customer c ON a.CustID=c.CustID
                        LEFT JOIN Tb_HSPR_Room d ON a.RoomID=d.RoomID
                        LEFT JOIN Tb_HSPR_Building e ON b.CommID=e.CommID AND d.BuildSNum=e.BuildSNum AND isnull(e.IsDelete,0)=0
                        LEFT JOIN Tb_HSPR_IncidentRegional f ON a.RegionalID=f.RegionalID
                        LEFT JOIN Tb_HSPR_IncidentPublicLocale g ON a.LocalePosition=g.IID
                        LEFT JOIN Tb_HSPR_IncidentPublicLocale h ON a.LocaleFunction=h.IID
                        LEFT JOIN Tb_Eq_Equipment i ON i.EquipmentId=a.EqID
                        LEFT JOIN Tb_Eq_Space j ON j.SpaceId=i.SpaceId
                        LEFT JOIN Tb_HSPR_CorpIncidentType k ON a.BigCorpTypeID=k.CorpTypeID
                        LEFT JOIN Tb_HSPR_CorpIncidentType l ON a.FineCorpTypeID=l.CorpTypeID
                        LEFT JOIN Tb_Sys_User m ON a.FinishUser=m.UserCode
                    WHERE a.IncidentID={ incidentId }";

                var incidentInfo = conn.Query(sql).FirstOrDefault();

                // 判断其它情况下的转发权限
                sql = @"SELECT count(1) FROM
                        (
                            /* 当前处理人 */
                            SELECT DealManCode AS RoleCode FROM Tb_HSPR_IncidentAccept WHERE IncidentID=@IncidentID AND DealManCode=@DealManCode
                            UNION ALL

                            /* 分派岗位 */
                            SELECT RoleCode FROM Tb_HSPR_IncidentTypeAssignedPost WHERE CorpTypeID=@BigCorpTypeID 
                            AND RoleCode IN
                            (
                                SELECT SysRoleCode FROM Tb_Sys_Role WHERE RoleCode IN(SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode=@UserCode)
                            )
                            UNION ALL

                            /* 房屋管家 */
                            SELECT UserCode AS RoleCode FROM Tb_HSPR_Room WHERE RoomID=@RoomID AND UserCode=@UserCode
                            UNION ALL

                            /* 房屋管家 */
                            SELECT UserCode AS RoleCode FROM Tb_HSPR_RoomHousekeeper WHERE RoomID=@RoomID AND UserCode=@UserCode 
                            UNION ALL

                            /* 楼栋管家 */
                            SELECT RoleCode FROM Tb_HSPR_BuildHousekeeper WHERE CommID=@CommID AND BuildSNum=@BuildSNum AND UserCode=@UserCode 
                            UNION ALL

                            /* 楼栋管家 */
                            SELECT RoleCode FROM Tb_HSPR_BuildHousekeeper WHERE CommID=@CommID AND BuildSNum=@BuildSNum AND isnull(UserCode,'')=''
                            AND RoleCode IN
                            (
                                SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode=@UserCode
                            ) 
                            UNION ALL

                            /* 公区管家 */
                            SELECT RoleCode FROM Tb_HSPR_RegionHousekeeper WHERE CommID=@CommID AND RegionID=@RegionalID AND UserCode=@UserCode 
                            UNION ALL

                            /* 公区管家 */
                            SELECT RoleCode FROM Tb_HSPR_RegionHousekeeper WHERE CommID=@CommID AND RegionID=@RegionalID AND isnull(UserCode,'')=''
                            AND RoleCode IN
                            (
                                SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode=@UserCode
                            )
                        ) AS a";

                var i = conn.Query<int>(sql, new
                {
                    IncidentID = incidentId,
                    DealManCode = Global_Var.LoginUserCode,
                    CommID = incidentInfo.CommID,
                    RoomID = incidentInfo.RoomID,
                    BuildSNum = incidentInfo.BuildSNum,
                    RegionalID = incidentInfo.RegionalID,
                    BigCorpTypeID = incidentInfo.BigCorpTypeID,
                    UserCode = Global_Var.LoginUserCode
                }).FirstOrDefault();

                incidentInfo.CanTransmit = (object)(i > 0 ? 1 : 0);

                // 到场完成，是否强制拍照


                // 到场、完成时照片上传方式
                incidentInfo.IncidentDealSelectImgMode = ((int)GetIncidentControlSet(conn).IncidentDealSelectImgMode).ToString();

          
                // 如果启用了上传到OSS
                if (Global_Fun.AppWebSettings("UseOssClient") == "1")
                {
                    var addDays = 30;
                    string ossPerfix = Global_Fun.AppWebSettings("OssPerfix");
                    string ossEndPoint = Global_Fun.AppWebSettings("OssEndPoint");
                    string ossAccessKeyId = Global_Fun.AppWebSettings("OssAccessKeyId");
                    string ossAccessKeySecret = Global_Fun.AppWebSettings("OssAccessKeySecret");
                    string ossBucket = Global_Fun.AppWebSettings("OssBucket");
                    string OssSavePath = Global_Fun.AppWebSettings("OssSavePath");


                    //// 1、报事受理图片
                    //string incidentImgsStr = Convert.ToString(incidentInfo.IncidentImgs);
                    //var incidentImgs = new List<string>();

                    //foreach (string img in incidentImgsStr.Split(','))
                    //{
                    //    var converImgUrl = img;
                    //    if (PubInfo.XingheOssGeneratePresignedUriRequest(ossPerfix, ossEndPoint, ossAccessKeyId, ossAccessKeySecret, ossBucket, OssSavePath, converImgUrl, DateTime.Now.AddDays(addDays),
                    //        out string url))
                    //    {
                    //        converImgUrl = url;
                    //    }
                    //    incidentImgs.Add(converImgUrl);
                    //}
                    //incidentInfo.IncidentImgs = string.Join(",", incidentImgs.ToArray());

                    
                    //// 2、报事分派图片
                    //string assignIncidentImgsStr = Convert.ToString(incidentInfo.AssignIncidentImgs);
                    //var assignIncidentImgs = new List<string>();

                    //foreach (string img in assignIncidentImgsStr.Split(','))
                    //{
                    //    var converImgUrl = img;
                    //    if (PubInfo.XingheOssGeneratePresignedUriRequest(ossPerfix, ossEndPoint, ossAccessKeyId, ossAccessKeySecret, ossBucket, OssSavePath, converImgUrl, DateTime.Now.AddDays(addDays),
                    //        out string url))
                    //    {
                    //        converImgUrl = url;
                    //    }
                    //    assignIncidentImgs.Add(converImgUrl);
                    //}
                    //incidentInfo.AssignIncidentImgs = string.Join(",", assignIncidentImgs.ToArray());


                    //// 3、报事处理图片
                    //string processIncidentImgsStr = Convert.ToString(incidentInfo.ProcessIncidentImgs);
                    //var processIncidentImgs = new List<string>();

                    //foreach (string img in processIncidentImgsStr.Split(','))
                    //{
                    //    var converImgUrl = img;
                    //    if (PubInfo.XingheOssGeneratePresignedUriRequest(ossPerfix, ossEndPoint, ossAccessKeyId, ossAccessKeySecret, ossBucket, OssSavePath, converImgUrl, DateTime.Now.AddDays(addDays),
                    //        out string url))
                    //    {
                    //        converImgUrl = url;
                    //    }
                    //    processIncidentImgs.Add(converImgUrl);
                    //}
                    //incidentInfo.ProcessIncidentImgs = string.Join(",", processIncidentImgs.ToArray());


                    //// 4、报事跟进图片
                    //string followImgsStr = Convert.ToString(incidentInfo.FollowImgs);
                    //var followImgs = new List<string>();

                    //foreach (string img in followImgsStr.Split(','))
                    //{
                    //    var converImgUrl = img;
                    //    if (PubInfo.XingheOssGeneratePresignedUriRequest(ossPerfix, ossEndPoint, ossAccessKeyId, ossAccessKeySecret, ossBucket, OssSavePath, converImgUrl, DateTime.Now.AddDays(addDays),
                    //        out string url))
                    //    {
                    //        converImgUrl = url;
                    //    }
                    //    followImgs.Add(converImgUrl);
                    //}
                    //incidentInfo.FollowImgs = string.Join(",", followImgs.ToArray());


                    //// 5、报事到场图片
                    //string arriveConfirmImgsStr = Convert.ToString(incidentInfo.ArriveConfirmImgs);
                    //var arriveConfirmImgs = new List<string>();

                    //foreach (string img in arriveConfirmImgsStr.Split(','))
                    //{
                    //    var converImgUrl = img;
                    //    if (PubInfo.XingheOssGeneratePresignedUriRequest(ossPerfix, ossEndPoint, ossAccessKeyId, ossAccessKeySecret, ossBucket, OssSavePath, converImgUrl, DateTime.Now.AddDays(addDays),
                    //        out string url))
                    //    {
                    //        converImgUrl = url;
                    //    }
                    //    arriveConfirmImgs.Add(converImgUrl);
                    //}
                    //incidentInfo.ArriveConfirmImgs = string.Join(",", arriveConfirmImgs.ToArray());


                    //// 6、报事关闭图片
                    //string closeIncidentImgsStr = Convert.ToString(incidentInfo.CloseIncidentImgs);
                    //var closeIncidentImgs = new List<string>();

                    //foreach (string img in closeIncidentImgsStr.Split(','))
                    //{
                    //    var converImgUrl = img;
                    //    if (PubInfo.XingheOssGeneratePresignedUriRequest(ossPerfix, ossEndPoint, ossAccessKeyId, ossAccessKeySecret, ossBucket, OssSavePath, converImgUrl, DateTime.Now.AddDays(addDays),
                    //        out string url))
                    //    {
                    //        converImgUrl = url;
                    //    }
                    //    closeIncidentImgs.Add(converImgUrl);
                    //}
                    //incidentInfo.CloseIncidentImgs = string.Join(",", closeIncidentImgs.ToArray());

                    
                    //// 6、签字图片
                    //string signatoryImgStr = Convert.ToString(incidentInfo.SignatoryImg);
                    //var signatoryImg = new List<string>();

                    //foreach (string img in signatoryImgStr.Split(','))
                    //{
                    //    var converImgUrl = img;
                    //    if (PubInfo.XingheOssGeneratePresignedUriRequest(ossPerfix, ossEndPoint, ossAccessKeyId, ossAccessKeySecret, ossBucket, OssSavePath, converImgUrl, DateTime.Now.AddDays(addDays),
                    //        out string url))
                    //    {
                    //        converImgUrl = url;
                    //    }
                    //    signatoryImg.Add(converImgUrl);
                    //}
                    //incidentInfo.SignatoryImg = string.Join(",", signatoryImg.ToArray());


                    //// 7、签字确认图片
                    //string signatoryConfirmImgStr = Convert.ToString(incidentInfo.SignatoryConfirmImg);
                    //var signatoryConfirmImg = new List<string>();

                    //foreach (string img in signatoryConfirmImgStr.Split(','))
                    //{
                    //    var converImgUrl = img;
                    //    if (PubInfo.XingheOssGeneratePresignedUriRequest(ossPerfix, ossEndPoint, ossAccessKeyId, ossAccessKeySecret, ossBucket, OssSavePath, converImgUrl, DateTime.Now.AddDays(addDays),
                    //        out string url))
                    //    {
                    //        converImgUrl = url;
                    //    }
                    //    signatoryConfirmImg.Add(converImgUrl);
                    //}
                    //incidentInfo.SignatoryConfirmImg = string.Join(",", signatoryConfirmImg.ToArray());


                }


                return new ApiResult(true, incidentInfo).toJson();
            }
        }

        /// <summary>
        /// 获取报事详情
        /// </summary>
        private string GetIncidentSimpleDetail(DataRow row)
        {
            if (!row.Table.Columns.Contains("IncidentID") || string.IsNullOrEmpty(row["IncidentID"].ToString()))
            {
                return new ApiResult(false, "缺少报事ID").toJson();
            }

            var incidentId = AppGlobal.StrToLong(row["IncidentID"].ToString());

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = $@"SELECT a.IncidentID,a.IncidentNum,a.CommID,b.CommName,c.CustName,d.RoomSign,d.RoomName,
                                a.RegionalID,f.RegionalPlace,g.DictionaryName AS LocalePosition,h.DictionaryName AS LocaleFunction,
                                a.EqID,i.EquipmentName,i.EquipmentNO,j.SpaceId,j.SpaceName,j.SpaceNo,
                                a.IncidentPlace,a.IncidentSource,a.IncidentMan,a.IncidentDate,a.IncidentMode,a.IncidentContent,
                                a.DrClass,a.IsTouSu,a.EmergencyDegree,a.ReserveDate,a.ReserveLimit,a.DispLimit,
                                a.BigCorpTypeID,a.BigCorpTypeCode,k.TypeName AS BigTypeName,a.Phone,isnull(a.IsFee,0) AS IsFee,
                                isnull(k.TypeName,l.TypeName) AS TypeName,isnull(a.Duty,'物业类') AS Duty,

                                /* 图片 */
                                isnull(a.IncidentImgs,'')+','+
                                isnull(STUFF((SELECT ',' + FilPath FROM Tb_HSPR_IncidentAdjunct x
                                    WHERE x.IncidentID={ incidentId } AND x.AdjunctName='受理图片' FOR XML PATH('')), 1, 1, ''), '') AS IncidentImgs 

                            FROM Tb_HSPR_IncidentAccept a
                            LEFT JOIN Tb_HSPR_Community b ON a.CommID=b.CommID
                            LEFT JOIN Tb_HSPR_Customer c ON a.CustID=c.CustID
                            LEFT JOIN Tb_HSPR_Room d ON a.RoomID=d.RoomID
                            /*LEFT JOIN Tb_HSPR_Building e ON b.CommID=e.CommID AND d.BuildSNum=e.BuildSNum AND isnull(e.IsDelete,0)=0*/
                            LEFT JOIN Tb_HSPR_IncidentRegional f ON a.RegionalID=f.RegionalID
                            LEFT JOIN Tb_HSPR_IncidentPublicLocale g ON a.LocalePosition=g.IID
                            LEFT JOIN Tb_HSPR_IncidentPublicLocale h ON a.LocaleFunction=h.IID
                            LEFT JOIN Tb_Eq_Equipment i ON i.EquipmentId=a.EqID
                            LEFT JOIN Tb_Eq_Space j ON j.SpaceId=i.SpaceId
                            LEFT JOIN Tb_HSPR_CorpIncidentType k ON a.BigCorpTypeID=k.CorpTypeID
                            LEFT JOIN Tb_HSPR_CorpIncidentType l ON a.FineCorpTypeID=l.CorpTypeID
                            WHERE a.IncidentID={ incidentId }";

                var incidentInfo = conn.Query(sql).FirstOrDefault();

         
                //// 如果启用了上传到OSS
                //if (Global_Fun.AppWebSettings("UseOssClient") == "1")
                //{
                //    // 1、报事受理图片
                //    string incidentImgsStr = Convert.ToString(incidentInfo.IncidentImgs);
                //    var incidentImgs = new List<string>();

                //    foreach (string img in incidentImgsStr.Split(','))
                //    {
                //        var converImgUrl = img;
                //        if (PubInfo.XingheOssGeneratePresignedUriRequest(Global_Fun.AppWebSettings("OssPerfix"), Global_Fun.AppWebSettings("OssEndPoint"), Global_Fun.AppWebSettings("OssAccessKeyId"), Global_Fun.AppWebSettings("OssAccessKeySecret"), Global_Fun.AppWebSettings("OssBucket"), Global_Fun.AppWebSettings("OssSavePath"), converImgUrl, DateTime.Now.AddDays(30), out string url))
                //        {
                //            converImgUrl = url;
                //        }
                //        incidentImgs.Add(converImgUrl);
                //    }
                //    incidentInfo.ImgUrl = string.Join(",", incidentImgs.ToArray());
                //}
                
                return new ApiResult(true, incidentInfo).toJson();
            }
        }

        /// <summary>
        /// 报事生命周期
        /// </summary>
        private string GetIncidentLifeCycle(DataRow row)
        {
            if (!row.Table.Columns.Contains("IncidentID") || string.IsNullOrEmpty(row["IncidentID"].ToString()))
            {
                return JSONHelper.FromString(false, "报事编号不能为空");
            }

            var incidentId = AppGlobal.StrToLong(row["IncidentID"].ToString());
            var loadReply = 0;
            if (row.Table.Columns.Contains("LoadReply"))
            {
                loadReply = AppGlobal.StrToInt(row["LoadReply"].ToString());
            }

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var chargeDbName = Global_Var.ERPDatabaseName;

                var sql = $@"SELECT isnull(DataBaseName_Read, DataBaseName) FROM Tb_System_Burst
                            WHERE BurstType='CHARGE' AND CommID IN 
                            (
                                SELECT CommID FROM { Global_Var.ERPDatabaseName }.dbo.Tb_HSPR_IncidentAccept WHERE IncidentID={ incidentId }
                            )";

                using (var tw2bsConn = new SqlConnection(PubConstant.tw2bsConnectionString))
                {
                    var tmp = tw2bsConn.Query<string>(sql, new { IncidentID = incidentId }).FirstOrDefault();
                    if (!string.IsNullOrEmpty(tmp))
                    {
                        chargeDbName = tmp;
                    }
                }

                sql = $@"SELECT a.IncidentID,a.IncidentNum,a.CommID,b.CommName,a.CustID,c.CustName,e.BuildSNum,a.RoomID,d.RoomSign,d.RoomName,
                            a.RegionalID,f.RegionalPlace,g.DictionaryName AS LocalePosition,h.DictionaryName AS LocaleFunction,
                            a.EqID,i.EquipmentName,i.EquipmentNO,j.SpaceId,j.SpaceName,j.SpaceNo,
                            a.IncidentPlace,a.IncidentSource,a.IncidentMan,a.IncidentDate,a.IncidentMode,a.IncidentContent,a.IncidentMemo,
                            a.DrClass,a.IsTouSu,a.Duty,a.EmergencyDegree,a.ReserveDate,a.ReserveLimit,a.DispLimit,
                            a.BigCorpTypeID,a.BigCorpTypeCode,k.TypeName AS BigTypeName,a.Phone,a.AdmiMan,a.AdmiDate,
                            a.CoordinateNum,a.DispMan,a.DispUserCode,a.DispDate,a.DispRemarks,m.MobileTel AS DispUserMobile,
                            a.DealManCode,a.DealMan,n.MobileTel AS DealManMobile,isnull(a.DealLimit,0) AS DealLimit,
                            a.FineCorpTypeID,a.FineCorpTypeCode,l.TypeName AS FineTypeName,a.IsReWork,a.ReWorkContent,
                            a.IsReceipt,a.ReceiptUserName,a.ReceivingDate,a.ArriveData,a.DealSituation,a.DealState,
                            a.IsFinish,a.FinishUser,o.UserName AS FinishUserName,o.MobileTel AS FinishUserMobile,isnull(a.FinishDate,a.MainEndDate) AS FinishDate,
                            a.ReplyLimit,a.Signatory,a.SignatoryImg,a.SignatoryConfirmImg,a.SignatoryImgDate,a.SignatoryConfirmImgDate,
                            a.IsClose,a.CloseUserCode,a.CloseUser,a.CloseTime,a.CloseSituation,
                            isnull(k.TypeName,l.TypeName) AS TypeName,                        
    
                            /* 协助人 */
                            STUFF((SELECT ',' + UserCode FROM Tb_HSPR_IncidentAssistDetail x
                                WHERE AssistID IN
                                (
                                    SELECT IID FROM Tb_HSPR_IncidentAssistApply x WHERE x.IncidentID={ incidentId } AND x.AuditState='已审核'
                                ) FOR XML PATH('')), 1, 1, '') AS AssistantUserCode,
                            STUFF((SELECT ',' + UserName FROM Tb_Sys_User y WHERE y.UserCode IN
                                (
                                    SELECT x.UserCode FROM Tb_HSPR_IncidentAssistDetail x
                                    WHERE AssistID IN
                                    (
                                        SELECT IID FROM Tb_HSPR_IncidentAssistApply x WHERE x.IncidentID={ incidentId } AND x.AuditState='已审核'
                                    )
                                ) FOR XML PATH('')), 1, 1, '') AS Assistant,

                            /* 费用 */
                            CASE (SELECT count(1) FROM { chargeDbName }.dbo.Tb_HSPR_Fees x WHERE x.CommID=a.CommID AND x.IncidentID={ incidentId })
                            WHEN 0 THEN 0
                            ELSE 1 END AS HasFees,
                            (SELECT isnull(sum(isnull(DueAmount,0.0)),0.0) FROM { chargeDbName }.dbo.Tb_HSPR_Fees x
                                WHERE x.CommID=a.CommID AND x.IncidentID={ incidentId }) AS DueAmount,
                            (SELECT isnull(sum(isnull(DebtsAmount,0.0)),0.0) FROM { chargeDbName }.dbo.Tb_HSPR_Fees x
                                WHERE x.CommID=a.CommID AND x.IncidentID={ incidentId }) AS DebtsAmount,

                            /* 图片 */
                            isnull(a.IncidentImgs,'')+','+
                            isnull(STUFF((SELECT ',' + FilPath FROM Tb_HSPR_IncidentAdjunct x
                                WHERE x.IncidentID={ incidentId } AND x.AdjunctName='受理图片' FOR XML PATH('')), 1, 1, ''),'') AS IncidentImgs,

                            isnull(STUFF((SELECT ',' + FilPath FROM Tb_HSPR_IncidentAdjunct x
                                WHERE x.IncidentID={ incidentId } AND x.AdjunctName='分派图片' FOR XML PATH('')), 1, 1, ''),'') AS AssignIncidentImgs,

                            isnull(STUFF((SELECT ',' + FilPath FROM Tb_HSPR_IncidentAdjunct x
                                WHERE x.IncidentID={ incidentId } AND x.AdjunctName='到场确认图片' FOR XML PATH('')), 1, 1, ''),'') AS ArriveConfirmImgs,

                            isnull(STUFF((SELECT ',' + FilPath FROM Tb_HSPR_IncidentAdjunct x
                                WHERE x.IncidentID={ incidentId } AND x.AdjunctName='跟进图片' FOR XML PATH('')), 1, 1, ''),'') AS FollowImgs,

                            isnull(a.ProcessIncidentImgs,'')+','+
                            isnull(STUFF((SELECT ',' + FilPath FROM Tb_HSPR_IncidentAdjunct x
                                WHERE x.IncidentID={ incidentId } AND x.AdjunctName='处理图片' FOR XML PATH('')), 1, 1, ''),'') AS ProcessIncidentImgs,

                            isnull(STUFF((SELECT ',' + FilPath FROM Tb_HSPR_IncidentAdjunct x
                                WHERE x.IncidentID={ incidentId } AND x.AdjunctName='关闭图片' FOR XML PATH('')), 1, 1, ''),'') AS CloseIncidentImgs

                        FROM Tb_HSPR_IncidentAccept a
                        LEFT JOIN Tb_HSPR_Community b ON a.CommID=b.CommID
                        LEFT JOIN Tb_HSPR_Customer c ON a.CustID=c.CustID
                        LEFT JOIN Tb_HSPR_Room d ON a.RoomID=d.RoomID
                        LEFT JOIN Tb_HSPR_Building e ON b.CommID=e.CommID AND d.BuildSNum=e.BuildSNum AND isnull(e.IsDelete,0)=0
                        LEFT JOIN Tb_HSPR_IncidentRegional f ON a.RegionalID=f.RegionalID
                        LEFT JOIN Tb_HSPR_IncidentPublicLocale g ON a.LocalePosition=g.IID
                        LEFT JOIN Tb_HSPR_IncidentPublicLocale h ON a.LocaleFunction=h.IID
                        LEFT JOIN Tb_Eq_Equipment i ON i.EquipmentId=a.EqID
                        LEFT JOIN Tb_Eq_Space j ON j.SpaceId=i.SpaceId
                        LEFT JOIN Tb_HSPR_CorpIncidentType k ON a.BigCorpTypeID=k.CorpTypeID
                        LEFT JOIN Tb_HSPR_CorpIncidentType l ON a.FineCorpTypeID=l.CorpTypeID
                        LEFT JOIN Tb_Sys_User m ON a.DispUserCode=m.UserCode
                        LEFT JOIN Tb_Sys_User n ON a.DealManCode=n.UserCode
                        LEFT JOIN Tb_Sys_User o ON a.FinishUser=o.UserCode
                        WHERE a.IncidentID={ incidentId }";

                var incidentInfo = conn.Query(sql, new { IncidentID = incidentId }).FirstOrDefault();

                var list = new List<PMSIncidentLifeCycle>();

                // 报事方式
                var incidentMode = incidentInfo.IncidentMode.ToString();
                if (!incidentMode.EndsWith("报事"))
                {
                    incidentMode += "报事";
                }

                // 报事人手机号码
                var incidentManMobileList = new List<ContactInfo>();
                if (!string.IsNullOrEmpty(incidentInfo.Phone))
                {
                    var array = incidentInfo.Phone.Split(new char[] { ',', '，', '|', '、', '&', '；', ';', '/', '\\', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var item in array)
                    {
                        incidentManMobileList.Add(new ContactInfo { UserName = incidentInfo.IncidentMan, Mobile = item });
                    }
                }

                // 创建任务
                list.Add(new PMSIncidentLifeCycle()
                {
                    PhaseName = "受理",
                    Title = "创建任务",
                    MainInfo = $@"报事人：{incidentInfo.IncidentMan}，报事方式：{incidentMode}",
                    SubInfo = $"报事内容：{incidentInfo.IncidentContent}",
                    SubInfoHighlight = false,
                    ActionTime = incidentInfo.IncidentDate,
                    DateTitle = $@"报事时间：{incidentInfo.IncidentDate?.ToString("yyyy'/'MM'/'dd HH:mm:ss")}",
                    Mobile = incidentManMobileList,
                    Files = incidentInfo.IncidentImgs?.ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                });


                // 分派人
                var dispManMobileList = new List<ContactInfo>();
                var dispManMobile = incidentInfo.DispUserMobile?.ToString();

                // 处理人
                var dealManMobileList = new List<ContactInfo>();
                var dealManMobile = incidentInfo.DealManMobile?.ToString();

                // 已分派
                if (incidentInfo.DispType?.ToString() == "1")
                {
                    // 是否抢单
                    bool isSnatch = (incidentInfo.DealManCode != null && incidentInfo.DispUserCode == incidentInfo.DealManCode);

                    // 分派人手机号
                    if (!string.IsNullOrEmpty(dispManMobile))
                    {
                        var array = dispManMobile.Split(new char[] { ',', '，', '|', '、', '&', '；', ';', '/', '\\', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var item in array)
                        {
                            dispManMobileList.Add(new ContactInfo { UserName = incidentInfo.DispMan, Mobile = item });
                        }
                    }

                    if (isSnatch)
                    {
                        dealManMobileList = dispManMobileList;
                    }
                    else
                    {
                        // 处理人手机号
                        if (!string.IsNullOrEmpty(dealManMobile))
                        {
                            var array = dealManMobile.Split(new char[] { ',', '，', '|', '、', '&', '；', ';', '/', '\\', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (var item in array)
                            {
                                dealManMobileList.Add(new ContactInfo { UserName = incidentInfo.DealMan, Mobile = item });
                            }
                        }
                    }

                    // 是否分派超时
                    DateTime startTime = Convert.ToDateTime(incidentInfo.IncidentDate);
                    DateTime dispTime = Convert.ToDateTime(incidentInfo.DispDate ?? DateTime.Now.ToString("yyyy'/'MM'/'dd HH:mm:ss"));
                    TimeSpan timeSpan = dispTime - startTime;
                    decimal dispLimit = Convert.ToDecimal(incidentInfo.DispLimit) ?? 0;
                    bool isOver = (timeSpan.TotalMilliseconds > Convert.ToDouble(dispLimit * 60 * 60 * 1000));

                    list.Add(new PMSIncidentLifeCycle()
                    {
                        PhaseName = "分派",
                        Title = (isOver ? "分派超时" : "分派任务"),
                        MainInfo = (isSnatch ? $@"“{incidentInfo.DispMan}”抢单成功" : $@"派工人“{incidentInfo.DispMan}”分派给了“{incidentInfo.DealMan}”"),
                        SubInfo = (isOver ? $@"分派超时{timeSpan.Days}天{timeSpan.Hours}时{timeSpan.Minutes}分" : ""),
                        SubInfoHighlight = isOver,
                        DateTitle = $@"{(isSnatch ? "抢单" : "派工")}时间：{incidentInfo.DispDate?.ToString("yyyy'/'MM'/'dd HH:mm:ss")}",
                        Mobile = dispManMobileList,
                        Files = incidentInfo.AssignIncidentImgs?.ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    });
                }

                // 接单
                if (!string.IsNullOrEmpty(incidentInfo.ReceivingDate?.ToString()))
                {
                    list.Add(new PMSIncidentLifeCycle()
                    {
                        PhaseName = "接单",
                        Title = "确认接单",
                        MainInfo = $@"“{incidentInfo.DealMan}”已接单，准备处理",
                        SubInfo = "",
                        SubInfoHighlight = false,
                        ActionTime = incidentInfo.ReceivingDate,
                        DateTitle = $@"接单时间：{incidentInfo.ReceivingDate?.ToString("yyyy'/'MM'/'dd HH:mm:ss")}",
                        Mobile = dealManMobileList,
                        Files = new string[] { }
                    });
                }

                // 到场
                if (!string.IsNullOrEmpty(incidentInfo.ArriveData?.ToString()))
                {
                    list.Add(new PMSIncidentLifeCycle()
                    {
                        PhaseName = "到场",
                        Title = "确认到场",
                        MainInfo = $@"“{incidentInfo.DealMan}”已到场，开始处理",
                        SubInfo = "",
                        SubInfoHighlight = false,
                        ActionTime = incidentInfo.ArriveData,
                        DateTitle = $@"到场时间：{incidentInfo.ArriveData?.ToString("yyyy'/'MM'/'dd HH:mm:ss")}",
                        Mobile = dealManMobileList,
                        Files = incidentInfo.ArriveConfirmImgs?.ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    });
                }

                // 跟进
                var follows = conn.Query(@"SELECT * FROM Tb_HSPR_IncidentForward WHERE IncidentID=@IncidentID
                                            ORDER BY ForwardDateTime", new { IncidentID = incidentId });
                if (follows.Count() > 0)
                {
                    foreach (var item in follows)
                    {
                        list.Add(new PMSIncidentLifeCycle()
                        {
                            PhaseName = "跟进",
                            Title = item.ForwardType,
                            MainInfo = $@"“{item.ForwardUser}”添加跟进信息",
                            SubInfo = $@"跟进内容：{item.ForwardReasons.ToString()}",
                            SubInfoHighlight = false,
                            ActionTime = item.ForwardDateTime,
                            DateTitle = $@"跟进时间：{item.ForwardDateTime?.ToString("yyyy'/'MM'/'dd HH:mm:ss")}",
                            Mobile = dealManMobileList,
                            Files = new string[] { }
                        });
                    }
                }

                // 协助人
                var assistant = incidentInfo.Assistant?.ToString().Trim(",");
                if (assistant != "")
                {
                    assistant = $"，协助人：{assistant}";
                }

                // 处理
                if (incidentInfo.DealState?.ToString() == "1")
                {
                    list.Add(new PMSIncidentLifeCycle()
                    {
                        PhaseName = "处理",
                        Title = "处理完毕",
                        MainInfo = $@"处理人“{incidentInfo.DealMan}”处理完毕",
                        SubInfo = $"处理情况：{(incidentInfo.DealSituation == null ? "无" : incidentInfo.DealSituation.ToString())}{assistant}",
                        SubInfoHighlight = false,
                        ActionTime = incidentInfo.FinishDate,
                        DateTitle = $@"完结时间：{incidentInfo.FinishDate?.ToString("yyyy'/'MM'/'dd HH:mm:ss")}",
                        Mobile = dealManMobileList,
                        Files = incidentInfo.ProcessIncidentImgs?.ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    });
                }
                else
                {
                    if (incidentInfo.DispType?.ToString() == "1")
                    {
                        // 判断是否处理逾期
                        DateTime startTime = Convert.ToDateTime(incidentInfo.ReserveDate ?? incidentInfo.DispDate);
                        TimeSpan timeSpan = DateTime.Now - startTime;
                        decimal dealLimit = Convert.ToDecimal(incidentInfo.DealLimit) ?? 0;
                        bool isOver = (timeSpan.TotalMilliseconds > Convert.ToDouble(dealLimit * 60 * 60 * 1000));

                        list.Add(new PMSIncidentLifeCycle()
                        {
                            PhaseName = "处理",
                            Title = (isOver ? "处理已超时" : "任务处理中"),
                            MainInfo = $@"处理人“{incidentInfo.DealMan}”正在处理{assistant}",
                            SubInfo = (isOver ? $@"已超时{timeSpan.Days}天{timeSpan.Hours}时{timeSpan.Minutes}分" : ""),
                            SubInfoHighlight = isOver,
                            Mobile = dealManMobileList,
                            Files = incidentInfo.ProcessIncidentImgs?.ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList()
                        });
                    }
                }

                // 关闭
                if (incidentInfo.IsClose?.ToString() == "1")
                {
                    list.Add(new PMSIncidentLifeCycle()
                    {
                        PhaseName = "关闭",
                        Title = "报事关闭",
                        MainInfo = $@"“{incidentInfo.CloseUser}”关闭了该工单",
                        SubInfo = $"关闭情况：{incidentInfo.CloseSituation ?? ""}",
                        SubInfoHighlight = false,
                        ActionTime = incidentInfo.CloseTime,
                        DateTitle = $@"关闭时间：{incidentInfo.CloseTime?.ToString("yyyy'/'MM'/'dd HH:mm:ss")}",
                        Mobile = dealManMobileList,
                        Files = incidentInfo.CloseIncidentImgs?.ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    });
                }

                // 回访
                if (loadReply != 0 && incidentInfo.DealState?.ToString() == "1" && incidentInfo.IsClose?.ToString() == "1")
                {
                    var replys = conn.Query(@"SELECT * FROM Tb_HSPR_IncidentReply WHERE isnull(IsDelete,0)=0 AND IncidentID=@IncidentID 
                                                ORDER BY ReplyDate", new { IncidentID = incidentId });

                    foreach (var item in replys)
                    {
                        list.Add(new PMSIncidentLifeCycle()
                        {
                            PhaseName = "回访",
                            Title = item.ReplyWay ?? "回访",
                            MainInfo = $@"回访人“{item.ReplyMan ?? ""}”，{(item.ReplyResult?.ToString() == "1" ? "成功" : "不成功")}回访",
                            SubInfo = $"服务评价：{item.ServiceQuality}",
                            SubInfoHighlight = false,
                            ActionTime = item.ReplyDate,
                            DateTitle = $@"回访时间：{item.ReplyDate?.ToString("yyyy'/'MM'/'dd HH:mm:ss")}",
                            Mobile = dealManMobileList,
                            Files = new string[] { }
                        });
                    }
                }

                // 排序
                list.Sort((a, b) =>
                {
                    if (a.ActionTime < b.ActionTime) return -1;
                    else if (a.ActionTime == b.ActionTime) return 0;
                    else return 1;
                });

                return new ApiResult(true, list).toJson();
            }
        }

        /// <summary>
        /// 获取报事跟进历史
        /// </summary>
        private string GetIncidentFollowHistory(DataRow row)
        {
            if (!row.Table.Columns.Contains("IncidentID") || string.IsNullOrEmpty(row["IncidentID"].AsString()))
            {
                return new ApiResult(false, "缺少参数IncidentID").toJson();
            }

            var incidentId = AppGlobal.StrToLong(row["IncidentID"].AsString());

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = @"SELECT a.IID,a.ForwardType,a.ForwardDateTime,a.ForwardReasons,a.ForwardUser,b.UserName,
                                   stuff((SELECT ','+FilPath FROM Tb_HSPR_IncidentAdjunct x WHERE x.ImgGUID=a.IID FOR XML PATH('')),1,1,'') AS FollowImgs
                            FROM Tb_HSPR_IncidentForward a
                                LEFT JOIN Tb_Sys_User b ON a.ForwardUserCode=b.UserCode
                            WHERE IncidentID=@IncidentID";

                var data = conn.Query(sql, new { IncidentID = incidentId });

                return new ApiResult(true, data).toJson();
            }
        }

        /// <summary>
        /// 获取报事转发历史
        /// </summary>
        private string GetIncidentTranspondHistory(DataRow row)
        {
            if (!row.Table.Columns.Contains("IncidentID") || string.IsNullOrEmpty(row["IncidentID"].AsString()))
            {
                return new ApiResult(false, "缺少参数IncidentID").toJson();
            }

            var incidentId = AppGlobal.StrToLong(row["IncidentID"].AsString());

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = @"SELECT a.IID,a.CommID,a.IncidentID,a.TransmitReasons,a.Duty,a.CorpTypeID,
                                a.CorpTypeCode,a.PersonLiable,a.TransmitDateTime,a.TransmitUserCode,
                                b.UserName AS TransmitUserName,c.TypeName AS CorpTypeName
                            FROM Tb_HSPR_IncidentTransmit a
                            LEFT JOIN Tb_Sys_User b ON a.TransmitUserCode=b.UserCode
                            LEFT JOIN Tb_HSPR_CorpIncidentType c ON a.CorpTypeID=c.CorpTypeID 
                            WHERE a.IncidentID=@IncidentID ORDER BY TransmitDateTime DESC";

                var data = conn.Query(sql, new { IncidentID = incidentId });

                return new ApiResult(true, data).toJson();
            }
        }

        /// <summary>
        /// 获取报事费用信息
        /// </summary>
        private string GetIncidentFees(DataRow row)
        {
            if (!row.Table.Columns.Contains("IncidentID") || string.IsNullOrEmpty(row["IncidentID"].ToString()))
            {
                return JSONHelper.FromString(false, "报事id不能为空");
            }

            var commId = 0;
            var incidentId = AppGlobal.StrToLong(row["IncidentID"].ToString());

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                commId = conn.Query<int>(@"SELECT CommID FROM Tb_HSPR_IncidentAccept WHERE IncidentID=@IncidentID",
                    new { IncidentID = incidentId }).FirstOrDefault();
            }

            using (var conn = new SqlConnection(Global_Fun.BurstConnectionString(commId, Global_Fun.BURST_TYPE_CHARGE)))
            {
                var sql = @"SELECT FeesID,CostName,StanName,FeesDueDate,AccountsDueDate,DueAmount,DebtsAmount,
                                isnull(PaidAmount,0) AS PaidAmount,
                                isnull(IsCharge,0) AS IsCharge
                            FROM view_HSPR_Fees_Filter WHERE IncidentID = @IncidentID 
                            ORDER BY FeesChargeDate DESC";

                var list = conn.Query(sql, new { IncidentID = incidentId }).ToList();

                return new ApiResult(true, list).toJson();
            }
        }

        /// <summary>
        /// 获取报事关闭退回列表
        /// </summary>
        private string GetIncidentCloseBackList(DataRow row)
        {
            if (!row.Table.Columns.Contains("IncidentID") || string.IsNullOrEmpty(row["IncidentID"].AsString()))
            {
                return new ApiResult(false, "缺少参数IncidentID").toJson();
            }

            var incidentId = AppGlobal.StrToLong(row["IncidentID"].ToString());

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = @"SELECT LastFinishTime,LastFinishUser,CloseGoBackReasons,CloseGoBackTime,CloseGoBackUser
                            FROM Tb_HSPR_IncidentCloseGoBack 
                            WHERE IncidentID=@IncidentID";

                var data = conn.Query(sql, new { IncidentID = incidentId });

                return new ApiResult(true, data).toJson();
            }
        }

        /// <summary>
        /// 获取报事回访退回列表
        /// </summary>
        private string GetIncidentReplyBackList(DataRow row)
        {
            if (!row.Table.Columns.Contains("IncidentID") || string.IsNullOrEmpty(row["IncidentID"].AsString()))
            {
                return new ApiResult(false, "缺少参数IncidentID").toJson();
            }

            var incidentId = AppGlobal.StrToLong(row["IncidentID"].ToString());

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = @"SELECT LastCloseTime,LastCloseUser,ReplyGoBackReasons,ReplyGoBackTime,ReplyGoBackUser
                            FROM Tb_HSPR_IncidentReplyGoBack 
                            WHERE IncidentID=@IncidentID";

                var data = conn.Query(sql, new { IncidentID = incidentId });

                return new ApiResult(true, data).toJson();
            }
        }
    }
}