using Common;
using Dapper;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using MobileSoft.Model.HSPR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TWTools.Push;

namespace Business
{
    public class IncidentAcceptPush
    {
        /// <summary>
        /// 户内报事推送
        /// </summary>
        public static void SynchPushIndoorIncident(Tb_HSPR_IncidentAccept incidentInfo)
        {
            SynchPushIndoorIncident_v2(incidentInfo);
            return;
        }

        /// <summary>
        /// 户内报事推送第二版，楼栋管家不需要设置分派岗位
        /// </summary>
        public static void SynchPushIndoorIncident_v2(Tb_HSPR_IncidentAccept incidentInfo)
        {
            if (incidentInfo.TypeID == null || incidentInfo.TypeID.Trim(new char[] { ',', ' ' }) == "")
            {
                return;
            }

            string tw2bsConnectionString = PubConstant.tw2bsConnectionString;
            string hmWyglConnectionString = PubConstant.hmWyglConnectionString;
            string corpId = Global_Var.CorpId;

            Task.Run(() =>
            {
                try
                {
                    if (Common.Push.GetAppKeyAndAppSecret(tw2bsConnectionString, corpId, out string appIdentifier, out string appKey, out string appSecret))
                    {
                        using (IDbConnection conn = new SqlConnection(hmWyglConnectionString))
                        {
                            incidentInfo.TypeID = incidentInfo.TypeID.Trim(new char[] { ',', ' ' });

                            // 小区、房屋信息
                            string commName = "", roomSign = "", sql = "";
                            if (incidentInfo.RoomID > 0)
                            {
                                sql = @"SELECT b.CommName,a.RoomSign FROM Tb_HSPR_Room a LEFT JOIN Tb_HSPR_Community b ON a.CommID=b.CommID
                                                 WHERE isnull(b.IsDelete,0)=0 AND isnull(a.IsDelete,0)=0 AND a.RoomID=@RoomId";
                                dynamic roomInfo = conn.Query(sql, new { RoomId = incidentInfo.RoomID }).FirstOrDefault();

                                if (roomInfo != null)
                                {
                                    commName = roomInfo.CommName;
                                    roomSign = roomInfo.RoomSign;
                                }
                            }

                            List<string> houseKeeperObjects = new List<string>();   // 楼栋管家对象
                            List<string> houseKeeperObjectsHuaWei = new List<string>();   // 楼栋管家对象

                            List<string> snatchObjects = new List<string>();        // 具有抢单权限的对象
                            List<string> snatchObjectsHuaWei = new List<string>();        // 具有抢单权限的对象

                            List<string> assignObjects = new List<string>();        // 具有分派权限的对象
                            List<string> assignObjectsHuaWei = new List<string>();        // 具有分派权限的对象

                            IEnumerable<dynamic> houseKeeperResult = new List<dynamic>();
                            IEnumerable<dynamic> assignedPostResult = new List<dynamic>();
                            IEnumerable<dynamic> snatchPostResult = new List<dynamic>();

                            // 1、判断是否设置了楼栋管家人员
                            sql = string.Format(@"SELECT UserCode FROM Tb_HSPR_BuildHousekeeper WHERE CommID={0} AND BuildSNum IN (SELECT BuildSNum FROM Tb_HSPR_Room WHERE CommID={0} AND RoomID={1} AND isnull(IsDelete,0)=0)", incidentInfo.CommID, incidentInfo.RoomID);

                            houseKeeperResult = conn.Query(sql);
                            foreach (dynamic item in houseKeeperResult)
                            {
                                //sql = @"SELECT MobileTel FROM Tb_Sys_User WHERE isnull(MobileTel,'')<>'' AND IsMobile=1 AND isnull(IsDelete,0)=0 AND UserCode =@UserCode";
                                sql = @"SELECT MobileTel,p.phonebrand,p.phoneToken FROM Tb_Sys_User u left join tb_sys_user_phone p on u.UserCode = p.UserCode WHERE isnull(MobileTel,'')<>'' AND IsMobile=1 AND isnull(IsDelete,0)=0 AND u.UserCode =@UserCode";

                                dynamic obj = conn.Query(sql, new { UserCode = item.UserCode }).FirstOrDefault();

                                if (obj != null)
                                {
                                    if (obj.phoneToken != null && !string.IsNullOrEmpty(obj.phoneToken.ToString()))
                                    {
                                        if (obj.phonebrand != null && !string.IsNullOrEmpty(obj.phonebrand.ToString()))
                                        {
                                            if ("honor".Equals(obj.phonebrand.ToString()) || "huawei".Equals(obj.phonebrand.ToString()))
                                            {
                                                houseKeeperObjectsHuaWei.Add(obj.phoneToken.ToString());
                                            }
                                            else
                                            {
                                                houseKeeperObjects.Add(obj.MobileTel.ToString());
                                            }
                                        }
                                        else
                                        {
                                            houseKeeperObjects.Add(obj.MobileTel.ToString());
                                        }
                                    }
                                    else
                                    {
                                        houseKeeperObjects.Add(obj.MobileTel.ToString());
                                    }

                                }
                            }


                            // 2、判断报事类别是否设置了分派岗位，这里的分派岗位，是针对通用岗位
                            sql = string.Format(@"SELECT RoleCode FROM Tb_HSPR_IncidentTypeAssignedPost WHERE CorpTypeID IN (SELECT CorpTypeID FROM Tb_HSPR_IncidentType WHERE TypeID IN (SELECT Convert(nvarchar(50), ColName) FROM dbo.funSplitTabel('{0}', ',')))", incidentInfo.TypeID);

                            assignedPostResult = conn.Query(sql);

                            if (assignedPostResult.Count() > 0)
                            {
                                // 获取具有分派岗位的人员信息
                                //sql = string.Format(@"SELECT MobileTel FROM view_Sys_User_RoleData_Filter2 
                                //                    WHERE CommID={0} AND IsMobile=1 AND isnull(MobileTel,'')<>'' 
                                //                    AND UserCode IN (SELECT UserCode FROM Tb_Sys_UserRole WHERE RoleCode IN(
                                //                        SELECT RoleCode FROM Tb_Sys_Role WHERE SysRoleCode IN (
                                //                            SELECT RoleCode from Tb_HSPR_IncidentTypeAssignedPost WHERE CorpTypeID IN 
                                //                                (SELECT CorpTypeID FROM Tb_HSPR_IncidentType WHERE TypeID IN 
                                //                    (SELECT Convert(nvarchar(50), ColName) FROM dbo.funSplitTabel('{1}', ','))))))", incidentInfo.CommID, incidentInfo.TypeID);

                                sql = string.Format(@"SELECT MobileTel,p.phonebrand,p.phoneToken FROM view_Sys_User_RoleData_Filter2 u left join tb_sys_user_phone p on u.UserCode = p.UserCode 
                                                    WHERE CommID={0} AND IsMobile=1 AND isnull(MobileTel,'')<>'' 
                                                    AND u.UserCode IN (SELECT UserCode FROM Tb_Sys_UserRole WHERE RoleCode IN(
                                                        SELECT RoleCode FROM Tb_Sys_Role WHERE SysRoleCode IN (
                                                            SELECT RoleCode from Tb_HSPR_IncidentTypeAssignedPost WHERE CorpTypeID IN 
                                                                (SELECT CorpTypeID FROM Tb_HSPR_IncidentType WHERE TypeID IN 
                                                    (SELECT Convert(nvarchar(50), ColName) FROM dbo.funSplitTabel('{1}', ','))))))", incidentInfo.CommID, incidentInfo.TypeID);

                                IEnumerable<dynamic> assignedPostUsers = conn.Query(sql);

                                if (assignedPostUsers.Count() > 0)
                                {
                                    foreach (dynamic item in assignedPostUsers)
                                    {
                                        if (item.phoneToken != null && !string.IsNullOrEmpty(item.phoneToken.ToString()))
                                        {
                                            if (item.phonebrand != null && !string.IsNullOrEmpty(item.phonebrand.ToString()))
                                            {
                                                if ("honor".Equals(item.phonebrand.ToString()) || "huawei".Equals(item.phonebrand.ToString()))
                                                {
                                                    assignObjectsHuaWei.Add(item.phoneToken.ToString());
                                                }
                                                else
                                                {
                                                    assignObjects.Add(item.MobileTel);
                                                }
                                            }
                                            else
                                            {
                                                assignObjects.Add(item.MobileTel);
                                            }
                                        }
                                        else
                                        {
                                            assignObjects.Add(item.MobileTel);
                                        }
                                        //snatchObjectsHuaWei

                                    }
                                    assignObjects = assignObjects.Distinct().ToList();
                                    assignObjectsHuaWei = assignObjectsHuaWei.Distinct().ToList();
                                }
                            }


                            // 3、判断报事类别是否设置了处理岗位，获取具有抢单权限的人，这里的处理岗位，是针对通用岗位
                            sql = string.Format(@"SELECT RoleCode FROM Tb_HSPR_IncidentTypeProcessPost WHERE CorpTypeID IN (SELECT CorpTypeID FROM Tb_HSPR_IncidentType WHERE TypeID IN (SELECT Convert(nvarchar(50), ColName) FROM dbo.funSplitTabel('{0}', ',')))", incidentInfo.TypeID);

                            snatchPostResult = conn.Query(sql);

                            if (snatchPostResult.Count() > 0)
                            {
                                // 获取所有具有处理岗位权限的人员的手机号码
                                sql = string.Format(@"SELECT MobileTel,p.phonebrand,p.phoneToken FROM view_Sys_User_RoleData_Filter2  u left join tb_sys_user_phone p on u.UserCode = p.UserCode
                                                    WHERE CommID={0} AND IsMobile=1 AND isnull(MobileTel,'')<>'' 
                                                    AND u.UserCode IN (SELECT UserCode FROM Tb_Sys_UserRole WHERE RoleCode IN(
                                                        SELECT RoleCode FROM Tb_Sys_Role WHERE SysRoleCode IN (
                                                            SELECT RoleCode from Tb_HSPR_IncidentTypeProcessPost WHERE CorpTypeID IN 
                                                                (SELECT CorpTypeID FROM Tb_HSPR_IncidentType WHERE TypeID IN 
                                                    (SELECT Convert(nvarchar(50), ColName) FROM dbo.funSplitTabel('{1}', ','))))))", incidentInfo.CommID, incidentInfo.TypeID);

                                IEnumerable<dynamic> snatchResult = conn.Query(sql);

                                if (snatchResult.Count() > 0)
                                {
                                    foreach (dynamic item in snatchResult)
                                    {
                                        if (item.phoneToken != null && !string.IsNullOrEmpty(item.phoneToken.ToString()))
                                        {
                                            if (item.phonebrand != null && !string.IsNullOrEmpty(item.phonebrand.ToString()))
                                            {
                                                if ("honor".Equals(item.phonebrand.ToString()) || "huawei".Equals(item.phonebrand.ToString()))
                                                {
                                                    snatchObjectsHuaWei.Add(item.phoneToken.ToString());
                                                }
                                                else
                                                {
                                                    snatchObjects.Add(item.MobileTel);
                                                }
                                            }
                                            else
                                            {
                                                snatchObjects.Add(item.MobileTel);
                                            }
                                        }
                                        else
                                        {
                                            snatchObjects.Add(item.MobileTel);
                                        }
                                        // snatchObjects.Add(item.MobileTel);
                                    }
                                }
                                snatchObjects = snatchObjects.Distinct().ToList();
                                snatchObjectsHuaWei = snatchObjectsHuaWei.Distinct().ToList();
                            }

                            // 推送
                            PushModel pushModel = new PushModel(appKey, appSecret)
                            {
                                AppIdentifier = appIdentifier,
                                Badge = 1,
                                KeyInfomation = incidentInfo.IncidentID.ToString(),
                            };

                            // 鸿坤自定义铃声 中集
                            if (corpId == "1973" || corpId == "1953")
                            {
                                pushModel.Sound = "push_alert_property.m4a";
                            }


                            pushModel.Audience.Category = PushAudienceCategory.Alias;
                            pushModel.Extras.Add("CommID", incidentInfo.CommID.ToString());
                            pushModel.Extras.Add("IncidentID", incidentInfo.IncidentID.ToString());
                            pushModel.Extras.Add("IncidentPlace", incidentInfo.IncidentPlace);

                            // 未设置楼栋管家，并且分派和处理岗位都没有设置，那么推送给具有ERP报事分派模块权限的人
                            if (houseKeeperResult.Count() == 0 && assignedPostResult.Count() == 0 && snatchPostResult.Count() == 0 && houseKeeperObjectsHuaWei.Count() == 0 &&
                            assignObjectsHuaWei.Count() == 0 && snatchObjectsHuaWei.Count() == 0)
                            {
                                sql = @"SELECT MobileTel,p.phonebrand,p.phoneToken FROM view_Sys_User_RoleData_Filter2 u left join tb_sys_user_phone p on u.UserCode = p.UserCode WHERE 
                                        RoleCode in(SELECT RoleCode FROM Tb_Sys_RolePope WHERE PNodeCode='0109060303') 
                                        AND CommID=@CommID
                                        AND IsMobile=1 
                                        AND isnull(MobileTel,'')<>''";

                                IEnumerable<dynamic> searchResult = conn.Query(sql, new { CommID = incidentInfo.CommID });

                                if (searchResult.Count() > 0)
                                {
                                    foreach (dynamic item in searchResult)
                                    {
                                        if (item.phoneToken != null && !string.IsNullOrEmpty(item.phoneToken.ToString()))
                                        {
                                            if (item.phonebrand != null && !string.IsNullOrEmpty(item.phonebrand.ToString()))
                                            {
                                                if ("honor".Equals(item.phonebrand.ToString()) || "huawei".Equals(item.phonebrand.ToString()))
                                                {
                                                    assignObjectsHuaWei.Add(item.phoneToken.ToString());
                                                }
                                                else
                                                {
                                                    assignObjects.Add(item.MobileTel);
                                                }
                                            }
                                            else
                                            {
                                                assignObjects.Add(item.MobileTel);
                                            }
                                        }
                                        else
                                        {
                                            assignObjects.Add(item.MobileTel);
                                        }
                                        //assignObjects.Add(item.MobileTel);
                                    }

                                    pushModel.Message = string.Format("【{0}{1}】有一条户内报事需要分派，报事人：{2}，点击查看详情。", commName, roomSign, incidentInfo.IncidentMan);

                                    pushModel.Command = PushCommand.INCIDENT_ASSIGN;
                                    pushModel.CommandName = PushCommand.CommandNameDict[pushModel.Command];
                                    pushModel.Audience.Objects.AddRange(assignObjects);

                                    TWTools.Push.Push.SendAsync(pushModel);

                                    //判断是否有华为推送
                                    if (assignObjectsHuaWei.Count() > 0)
                                    {
                                        PushModel pushModel1 = new PushModel(appKey, appSecret, PushChannel.HWPush)
                                        {
                                            AppIdentifier = appIdentifier,
                                            Badge = 1,
                                            KeyInfomation = incidentInfo.IncidentID.ToString(),
                                            Sound = "push_alert_property.m4a",
                                            Message = string.Format("【{0}{1}】有一条户内报事需要分派，报事人：{2}，点击查看详情。", commName, roomSign, incidentInfo.IncidentMan)
                                        };
                                        pushModel1.Audience.Objects.AddRange(assignObjectsHuaWei);

                                        TWTools.Push.Push.SendAsync(pushModel1);
                                    }
                                }
                                return;
                            }

                            // 具有分派且同时具有抢单权限的人
                            List<string> canAssignAndSnatchObjects;
                            List<string> canAssignAndSnatchObjectsHuaWei;

                            // 存在有权限分派的楼栋管家
                            if (houseKeeperObjects.Count > 0)
                            {
                                /*
                                 *  1、从具有分派权限的人中过滤掉楼栋管家
                                 *  2、获取同时具有抢单权限的楼栋管家
                                 *  3、如果存在同时具有抢单权限的楼栋管家，从具有抢单权限的人中过滤掉该相关楼栋管家
                                 */

                                assignObjects = assignObjects.Except(houseKeeperObjects).ToList();

                                // 具有分派且同时具有抢单权限的人
                                canAssignAndSnatchObjects = houseKeeperObjects.Intersect(snatchObjects).ToList();

                                if (canAssignAndSnatchObjects.Count() > 0)
                                {
                                    // 取差集
                                    snatchObjects = snatchObjects.Except(canAssignAndSnatchObjects).ToList();           // 除开楼栋管家外的具有抢单权限的人
                                    houseKeeperObjects = houseKeeperObjects.Except(canAssignAndSnatchObjects).ToList(); // 只有分派权限的楼栋管家

                                    pushModel.Message = string.Format("楼栋管家请注意：【{0}{1}】有一条新的户内报事需要处理，报事人：{2}，您可以分派给他人处理或自行抢单后处理，点击查看详情。", commName, roomSign, incidentInfo.IncidentMan);
                                    pushModel.Command = PushCommand.INCIDENT_SNATCH_ASSIGN;
                                    pushModel.CommandName = PushCommand.CommandNameDict[pushModel.Command];
                                    pushModel.Audience.Objects.Clear();
                                    pushModel.Audience.Objects.AddRange(canAssignAndSnatchObjects);

                                    TWTools.Push.Push.SendAsync(pushModel);
                                }

                                if (houseKeeperObjects.Count > 0)
                                {
                                    pushModel.Message = string.Format("楼栋管家请注意：【{0}{1}】有一条新的户内报事需要处理，报事人：{2}，点击查看详情。", commName, roomSign, incidentInfo.IncidentMan);
                                    pushModel.Command = PushCommand.INCIDENT_ASSIGN;
                                    pushModel.CommandName = PushCommand.CommandNameDict[pushModel.Command];
                                    pushModel.Audience.Objects.Clear();
                                    pushModel.Audience.Objects.AddRange(houseKeeperObjects);

                                    TWTools.Push.Push.SendAsync(pushModel);
                                }
                            }

                            // 华为推送存在有权限分派的楼栋管家
                            if (houseKeeperObjectsHuaWei.Count > 0)
                            {
                                /*
                                 *  1、从具有分派权限的人中过滤掉楼栋管家
                                 *  2、获取同时具有抢单权限的楼栋管家
                                 *  3、如果存在同时具有抢单权限的楼栋管家，从具有抢单权限的人中过滤掉该相关楼栋管家
                                 */

                                assignObjectsHuaWei = assignObjects.Except(houseKeeperObjectsHuaWei).ToList();

                                // 具有分派且同时具有抢单权限的人
                                canAssignAndSnatchObjectsHuaWei = houseKeeperObjects.Intersect(snatchObjectsHuaWei).ToList();

                                if (canAssignAndSnatchObjectsHuaWei.Count() > 0)
                                {
                                    // 取差集
                                    snatchObjectsHuaWei = snatchObjects.Except(canAssignAndSnatchObjectsHuaWei).ToList();           // 除开楼栋管家外的具有抢单权限的人
                                    houseKeeperObjectsHuaWei = houseKeeperObjects.Except(canAssignAndSnatchObjectsHuaWei).ToList(); // 只有分派权限的楼栋管家

                                    //pushModel.Message = string.Format("楼栋管家请注意：【{0}{1}】有一条新的户内报事需要处理，报事人：{2}，您可以分派给他人处理或自行抢单后处理，点击查看详情。", commName, roomSign, incidentInfo.IncidentMan);
                                    //pushModel.Command = PushCommand.INCIDENT_SNATCH_ASSIGN;
                                    //pushModel.CommandName = PushCommand.CommandNameDict[pushModel.Command];
                                    //pushModel.Audience.Objects.Clear();
                                    //pushModel.Audience.Objects.AddRange(canAssignAndSnatchObjectsHuaWei);

                                    PushModel pushModel2 = new PushModel(appKey, appSecret, PushChannel.HWPush)
                                    {
                                        AppIdentifier = appIdentifier,
                                        Badge = 1,
                                        KeyInfomation = incidentInfo.IncidentID.ToString(),
                                        Sound = "push_alert_property.m4a",
                                        Message = string.Format("楼栋管家请注意：【{0}{1}】有一条新的户内报事需要处理，报事人：{2}，您可以分派给他人处理或自行抢单后处理，点击查看详情。", commName, roomSign, incidentInfo.IncidentMan)
                                    };
                                    pushModel2.Audience.Objects.AddRange(canAssignAndSnatchObjectsHuaWei);

                                    TWTools.Push.Push.SendAsync(pushModel2);
                                }

                                if (houseKeeperObjectsHuaWei.Count > 0)
                                {
                                    //pushModel.Message = string.Format("楼栋管家请注意：【{0}{1}】有一条新的户内报事需要处理，报事人：{2}，点击查看详情。", commName, roomSign, incidentInfo.IncidentMan);
                                    //pushModel.Command = PushCommand.INCIDENT_ASSIGN;
                                    //pushModel.CommandName = PushCommand.CommandNameDict[pushModel.Command];
                                    //pushModel.Audience.Objects.Clear();
                                    //pushModel.Audience.Objects.AddRange(houseKeeperObjectsHuaWei);

                                    PushModel pushModel3 = new PushModel(appKey, appSecret, PushChannel.HWPush)
                                    {
                                        AppIdentifier = appIdentifier,
                                        Badge = 1,
                                        KeyInfomation = incidentInfo.IncidentID.ToString(),
                                        Sound = "push_alert_property.m4a",
                                        Message = string.Format("楼栋管家请注意：【{0}{1}】有一条新的户内报事需要处理，报事人：{2}，点击查看详情。", commName, roomSign, incidentInfo.IncidentMan)
                                    };
                                    pushModel3.Audience.Objects.AddRange(houseKeeperObjectsHuaWei);
                                    TWTools.Push.Push.SendAsync(pushModel3);
                                }
                            }

                            // 具有分派且同时具有抢单权限的人
                            canAssignAndSnatchObjects = assignObjects.Intersect(snatchObjects).ToList();
                            canAssignAndSnatchObjectsHuaWei = assignObjects.Intersect(snatchObjectsHuaWei).ToList();

                            // 书面的时候才推送，口派跳过此步骤
                            if (canAssignAndSnatchObjects.Count > 0)
                            {
                                // 取差集，只有抢单或只有分派权限的
                                assignObjects = assignObjects.Except(canAssignAndSnatchObjects).ToList();
                                snatchObjects = snatchObjects.Except(canAssignAndSnatchObjects).ToList();

                                pushModel.Message = string.Format("【{0}{1}】有一条新的户内报事需要处理，报事人：{2}，您可以分派给他人处理或自行抢单后处理，点击查看详情。", commName, roomSign, incidentInfo.IncidentMan);
                                pushModel.Command = PushCommand.INCIDENT_SNATCH;
                                pushModel.CommandName = PushCommand.CommandNameDict[pushModel.Command];

                                pushModel.Audience.Objects.Clear();
                                pushModel.Audience.Objects.AddRange(canAssignAndSnatchObjects);

                                TWTools.Push.Push.SendAsync(pushModel);
                            }

                            // 华为书面的时候才推送，口派跳过此步骤
                            if (canAssignAndSnatchObjectsHuaWei.Count > 0)
                            {
                                // 取差集，只有抢单或只有分派权限的
                                assignObjectsHuaWei = assignObjectsHuaWei.Except(canAssignAndSnatchObjectsHuaWei).ToList();
                                snatchObjectsHuaWei = snatchObjectsHuaWei.Except(canAssignAndSnatchObjectsHuaWei).ToList();

                                //pushModel.Message = string.Format("【{0}{1}】有一条新的户内报事需要处理，报事人：{2}，您可以分派给他人处理或自行抢单后处理，点击查看详情。", commName, roomSign, incidentInfo.IncidentMan);
                                //pushModel.Command = PushCommand.INCIDENT_SNATCH;
                                //pushModel.CommandName = PushCommand.CommandNameDict[pushModel.Command];

                                //pushModel.Audience.Objects.Clear();
                                //pushModel.Audience.Objects.AddRange(canAssignAndSnatchObjects);
                                PushModel pushModel4 = new PushModel(appKey, appSecret, PushChannel.HWPush)
                                {
                                    AppIdentifier = appIdentifier,
                                    Badge = 1,
                                    KeyInfomation = incidentInfo.IncidentID.ToString(),
                                    Sound = "push_alert_property.m4a",
                                    Message = string.Format("【{0}{1}】有一条新的户内报事需要处理，报事人：{2}，您可以分派给他人处理或自行抢单后处理，点击查看详情。", commName, roomSign, incidentInfo.IncidentMan)
                                };
                                pushModel4.Audience.Objects.AddRange(canAssignAndSnatchObjectsHuaWei);

                                TWTools.Push.Push.SendAsync(pushModel4);
                            }

                            // 推送分派
                            if (assignObjects.Count > 0)
                            {
                                pushModel.Message = string.Format("【{0}{1}】有一条户内报事需要分派，报事人：{2}，请及时分派给相关人员处理，点击查看详情。", commName, roomSign, incidentInfo.IncidentMan);

                                pushModel.Command = PushCommand.INCIDENT_ASSIGN;
                                pushModel.CommandName = PushCommand.CommandNameDict[pushModel.Command];
                                pushModel.Audience.Objects.Clear();
                                pushModel.Audience.Objects.AddRange(assignObjects);

                                TWTools.Push.Push.SendAsync(pushModel);
                            }

                            // 华为推送分派
                            if (assignObjectsHuaWei.Count > 0)
                            {
                                //pushModel.Message = string.Format("【{0}{1}】有一条户内报事需要分派，报事人：{2}，请及时分派给相关人员处理，点击查看详情。", commName, roomSign, incidentInfo.IncidentMan);

                                //pushModel.Command = PushCommand.INCIDENT_ASSIGN;
                                //pushModel.CommandName = PushCommand.CommandNameDict[pushModel.Command];
                                //pushModel.Audience.Objects.Clear();
                                //pushModel.Audience.Objects.AddRange(assignObjects);

                                PushModel pushModel5 = new PushModel(appKey, appSecret, PushChannel.HWPush)
                                {
                                    AppIdentifier = appIdentifier,
                                    Badge = 1,
                                    KeyInfomation = incidentInfo.IncidentID.ToString(),
                                    Sound = "push_alert_property.m4a",
                                    Message = string.Format("【{0}{1}】有一条户内报事需要分派，报事人：{2}，请及时分派给相关人员处理，点击查看详情。", commName, roomSign, incidentInfo.IncidentMan)
                                };
                                pushModel5.Audience.Objects.AddRange(assignObjectsHuaWei);

                                TWTools.Push.Push.SendAsync(pushModel5);
                            }

                            // 推送抢单
                            if (snatchObjects.Count > 0)
                            {
                                // 泰禾不推送抢单
                                if (corpId == "1970")
                                {
                                    return;
                                }

                                pushModel.Message = string.Format("【{0}{1}】有一条户内报事，报事人：{2}，您可以进行抢单处理，点击查看详情", commName, roomSign, incidentInfo.IncidentMan);

                                pushModel.Command = PushCommand.INCIDENT_SNATCH;
                                pushModel.CommandName = PushCommand.CommandNameDict[pushModel.Command];
                                pushModel.Audience.Objects.Clear();
                                pushModel.Audience.Objects.AddRange(snatchObjects);

                                TWTools.Push.Push.SendAsync(pushModel);
                            }

                            //华为推送抢单
                            if (snatchObjectsHuaWei.Count > 0)
                            {
                                // 泰禾不推送抢单
                                if (corpId == "1970")
                                {
                                    return;
                                }

                                //pushModel.Message = string.Format("【{0}{1}】有一条户内报事，报事人：{2}，您可以进行抢单处理，点击查看详情", commName, roomSign, incidentInfo.IncidentMan);

                                //pushModel.Command = PushCommand.INCIDENT_SNATCH;
                                //pushModel.CommandName = PushCommand.CommandNameDict[pushModel.Command];
                                //pushModel.Audience.Objects.Clear();
                                //pushModel.Audience.Objects.AddRange(snatchObjects);

                                PushModel pushModel6 = new PushModel(appKey, appSecret, PushChannel.HWPush)
                                {
                                    AppIdentifier = appIdentifier,
                                    Badge = 1,
                                    KeyInfomation = incidentInfo.IncidentID.ToString(),
                                    Sound = "push_alert_property.m4a",
                                    Message = string.Format("【{0}{1}】有一条户内报事，报事人：{2}，您可以进行抢单处理，点击查看详情", commName, roomSign, incidentInfo.IncidentMan)
                                };
                                pushModel6.Audience.Objects.AddRange(snatchObjectsHuaWei);

                                TWTools.Push.Push.SendAsync(pushModel6);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (!EventLog.SourceExists("TWInterface"))
                    {
                        EventLog.CreateEventSource("TWInterface", "Push");
                    }
                    EventLog.WriteEntry("TWInterface", ex.Message, EventLogEntryType.Error);
                }
            });
        }

        /// <summary>
        /// 户内报事推送，报事没有类别时使用
        /// </summary>
        public static void SynchPushIndoorIncidentWithoutIncidentType(Tb_HSPR_IncidentAccept incidentInfo)
        {
            string tw2bsConnectionString = PubConstant.tw2bsConnectionString;
            string hmWyglConnectionString = PubConstant.hmWyglConnectionString;
            string corpId = Global_Var.CorpId;

            Task.Run(() =>
            {
                try
                {
                    if (Common.Push.GetAppKeyAndAppSecret(tw2bsConnectionString, corpId, out string appIdentifier, out string appKey, out string appSecret))
                    {
                        using (IDbConnection conn = new SqlConnection(hmWyglConnectionString))
                        {
                            // 小区、房屋信息
                            string commName = "", roomSign = "", sql = "";
                            if (incidentInfo.RoomID > 0)
                            {
                                sql = string.Format(@"SELECT b.CommName,a.RoomSign FROM Tb_HSPR_Room a LEFT JOIN Tb_HSPR_Community b ON a.CommID=b.CommID
                                                 WHERE isnull(b.IsDelete,0)=0 AND isnull(a.IsDelete,0)=0 AND a.RoomID=@RoomId");
                                dynamic roomInfo = conn.Query(sql, new { RoomId = incidentInfo.RoomID }).FirstOrDefault();

                                if (roomInfo != null)
                                {
                                    commName = roomInfo.CommName;
                                    roomSign = roomInfo.RoomSign;
                                }
                            }

                            //List<string> houseKeeperObjects = new List<string>();   // 楼栋管家对象
                            //List<string> snatchObjects = new List<string>();        // 具有抢单权限的对象
                            //List<string> assignObjects = new List<string>();        // 具有分派权限的对象

                            List<string> houseKeeperObjects = new List<string>();   // 楼栋管家对象
                            List<string> houseKeeperObjectsHuaWei = new List<string>();   // 楼栋管家对象
                            List<string> houseKeeperObjectsXiaoMi = new List<string>();   // 楼栋管家对象

                            List<string> snatchObjects = new List<string>();        // 具有抢单权限的对象
                            List<string> snatchObjectsHuaWei = new List<string>();        // 具有抢单权限的对象
                            List<string> snatchObjectsXiaoMi = new List<string>();        // 具有抢单权限的对象

                            List<string> assignObjects = new List<string>();        // 具有分派权限的对象
                            List<string> assignObjectsHuaWei = new List<string>();        // 具有分派权限的对象
                            List<string> assignObjectsXiaoMi = new List<string>();        // 具有分派权限的对象

                            IEnumerable<dynamic> houseKeeperResult = new List<dynamic>();
                            IEnumerable<dynamic> assignedPostResult = new List<dynamic>();
                            IEnumerable<dynamic> snatchPostResult = new List<dynamic>();

                            // 1、判断是否设置了楼栋管家
                            sql = @"SELECT UserCode FROM Tb_HSPR_BuildHousekeeper WHERE CommID=@CommID AND BuildSNum IN (SELECT BuildSNum FROM Tb_HSPR_Room WHERE CommID=@CommID AND RoomID=@RoomID AND isnull(IsDelete,0)=0)";

                            houseKeeperResult = conn.Query(sql, new { CommID = incidentInfo.CommID, RoomID = incidentInfo.RoomID });

                            // 存在楼栋管家
                            if (houseKeeperResult.Count() > 0)
                            {
                                // 获取楼栋管家手机号码
                                sql = @"SELECT DISTINCT MobileTel,p.phonebrand,p.phoneToken FROM Tb_Sys_User u left join tb_sys_user_phone p on u.UserCode = p.UserCode WHERE isnull(MobileTel,'')<>'' AND isnull(IsDelete,0)=0 AND u.UserCode IN (SELECT UserCode FROM Tb_HSPR_BuildHousekeeper WHERE CommID=@CommID AND BuildSNum IN (SELECT BuildSNum FROM Tb_HSPR_Room WHERE CommID=@CommID AND RoomID=@RoomID AND isnull(IsDelete,0)=0))";

                                //houseKeeperObjects.AddRange(conn.Query<string>(sql, new { CommID = incidentInfo.CommID, RoomID = incidentInfo.RoomID }));
                                var objList = conn.Query(sql, new { CommID = incidentInfo.CommID, RoomID = incidentInfo.RoomID }).ToList();

                                if (objList != null && objList.Count() > 0)
                                {
                                    foreach (var obj in objList)
                                    {
                                        if (obj.phoneToken != null && !string.IsNullOrEmpty(obj.phoneToken.ToString()))
                                        {
                                            if (obj.phonebrand != null && !string.IsNullOrEmpty(obj.phonebrand.ToString()))
                                            {
                                                if ("honor".Equals(obj.phonebrand.ToString()) || "huawei".Equals(obj.phonebrand.ToString()))
                                                {
                                                    houseKeeperObjectsHuaWei.Add(obj.phoneToken.ToString());
                                                }
                                                else if ("xiaomi".Equals(obj.phonebrand.ToString()))
                                                {
                                                    houseKeeperObjectsXiaoMi.Add(obj.phoneToken.ToString());
                                                }
                                                else
                                                {
                                                    houseKeeperObjects.Add(obj.MobileTel.ToString());
                                                }
                                            }
                                            else
                                            {
                                                houseKeeperObjects.Add(obj.MobileTel.ToString());
                                            }
                                        }
                                        else
                                        {
                                            houseKeeperObjects.Add(obj.MobileTel.ToString());
                                        }
                                    }
                                }
                            }

                            // 获取具有小区和报事分派模块权限的人
                            sql = @"SELECT DISTINCT MobileTel,p.phonebrand,p.phoneToken FROM view_Sys_User_RoleData_Filter2 u left join tb_sys_user_phone p on u.UserCode = p.UserCode WHERE 
                                        RoleCode in(SELECT RoleCode FROM Tb_Sys_RolePope WHERE PNodeCode='0109060303') 
                                        AND CommID=@CommID
                                        AND IsMobile=1 
                                        AND isnull(MobileTel,'')<>''";

                            IEnumerable<dynamic> assignResult = conn.Query(sql, new { CommID = incidentInfo.CommID });

                            if (assignResult.Count() > 0)
                            {
                                foreach (dynamic item in assignResult)
                                {
                                    if (item.phoneToken != null && !string.IsNullOrEmpty(item.phoneToken.ToString()))
                                    {
                                        if (item.phonebrand != null && !string.IsNullOrEmpty(item.phonebrand.ToString()))
                                        {
                                            if ("honor".Equals(item.phonebrand.ToString()) || "huawei".Equals(item.phonebrand.ToString()))
                                            {
                                                assignObjectsHuaWei.Add(item.phoneToken.ToString());
                                            }
                                            else if ("xiaomi".Equals(item.phonebrand.ToString()))
                                            {
                                                assignObjectsXiaoMi.Add(item.phoneToken.ToString());
                                            }
                                            else
                                            {
                                                assignObjects.Add(item.MobileTel);
                                            }
                                        }
                                        else
                                        {
                                            assignObjects.Add(item.MobileTel);
                                        }
                                    }
                                    else
                                    {
                                        assignObjects.Add(item.MobileTel);
                                    }
                                    //assignObjects.Add(item.MobileTel);
                                }
                            }

                            // 推送
                            PushModel pushModel = new PushModel(appKey, appSecret)
                            {
                                AppIdentifier = appIdentifier,
                                Badge = 1,
                                KeyInfomation = incidentInfo.IncidentID.ToString()
                            };

                            // 鸿坤自定义铃声 中集
                            if (corpId == "1973" || corpId == "1953")
                            {
                                pushModel.Sound = "push_alert_property.m4a";
                            }


                            pushModel.Audience.Category = PushAudienceCategory.Alias;
                            pushModel.Extras.Add("CommID", incidentInfo.CommID.ToString());
                            pushModel.Extras.Add("IncidentID", incidentInfo.IncidentID.ToString());
                            pushModel.Extras.Add("IncidentPlace", incidentInfo.IncidentPlace);

                            pushModel.Command = PushCommand.INCIDENT_ASSIGN;
                            pushModel.CommandName = "业主App" + PushCommand.CommandNameDict[pushModel.Command];

                            // 存在有权限分派的楼栋管家
                            if (houseKeeperObjects.Count > 0)
                            {
                                pushModel.Message = string.Format("楼栋管家请注意：【{0}{1}】有一条新的户内报事信息需要分派，报事人：{2}，点击查看详情。", commName, roomSign, incidentInfo.IncidentMan);

                                pushModel.Audience.Objects.Clear();
                                pushModel.Audience.Objects.AddRange(houseKeeperObjects);
                                TWTools.Push.Push.SendAsync(pushModel);
                            }

                            //判断是否有华为推送
                            if (houseKeeperObjectsHuaWei.Count() > 0)
                            {
                                PushModel pushModel1 = new PushModel(appKey, appSecret, PushChannel.HWPush)
                                {
                                    AppIdentifier = appIdentifier,
                                    Badge = 1,
                                    KeyInfomation = incidentInfo.IncidentID.ToString(),
                                    Sound = "push_alert_property.m4a",
                                    Message = string.Format("楼栋管家请注意：【{0}{1}】有一条新的户内报事信息需要分派，报事人：{2}，点击查看详情。", commName, roomSign, incidentInfo.IncidentMan)
                                };
                                pushModel1.Audience.Objects.AddRange(houseKeeperObjectsHuaWei);

                                TWTools.Push.Push.SendAsync(pushModel1);
                            }
                            //判断是否有小米推送
                            if (houseKeeperObjectsXiaoMi.Count() > 0)
                            {
                                PushModel pushModel1 = new PushModel(appKey, appSecret, PushChannel.XMPush)
                                {
                                    AppIdentifier = appIdentifier,
                                    Badge = 1,
                                    KeyInfomation = incidentInfo.IncidentID.ToString(),
                                    Sound = "push_alert_property.m4a",
                                    Message = string.Format("楼栋管家请注意：【{0}{1}】有一条新的户内报事信息需要分派，报事人：{2}，点击查看详情。", commName, roomSign, incidentInfo.IncidentMan)
                                };
                                pushModel1.Audience.Objects.AddRange(houseKeeperObjectsXiaoMi);

                                TWTools.Push.Push.SendAsync(pushModel1);
                            }

                            // 推送分派
                            if (assignObjects.Count > 0)
                            {
                                pushModel.Message = string.Format("【{0}{1}】有一条户内报事需要分派，报事人：{2}，请及时分派给相关人员处理，点击查看详情。", commName, roomSign, incidentInfo.IncidentMan);

                                pushModel.Audience.Objects.Clear();
                                pushModel.Audience.Objects.AddRange(assignObjects);

                                TWTools.Push.Push.SendAsync(pushModel);
                            }

                            //判断是否有华为推送
                            if (assignObjectsHuaWei.Count() > 0)
                            {
                                PushModel pushModel1 = new PushModel(appKey, appSecret, PushChannel.HWPush)
                                {
                                    AppIdentifier = appIdentifier,
                                    Badge = 1,
                                    KeyInfomation = incidentInfo.IncidentID.ToString(),
                                    Sound = "push_alert_property.m4a",
                                    Message = string.Format("【{0}{1}】有一条户内报事需要分派，报事人：{2}，请及时分派给相关人员处理，点击查看详情。", commName, roomSign, incidentInfo.IncidentMan)
                                };
                                pushModel1.Audience.Objects.AddRange(assignObjectsHuaWei);

                                TWTools.Push.Push.SendAsync(pushModel1);
                            }

                            //判断是否有小米推送
                            if (assignObjectsXiaoMi.Count() > 0)
                            {
                                PushModel pushModel1 = new PushModel(appKey, appSecret, PushChannel.XMPush)
                                {
                                    AppIdentifier = appIdentifier,
                                    Badge = 1,
                                    KeyInfomation = incidentInfo.IncidentID.ToString(),
                                    Sound = "push_alert_property.m4a",
                                    Message = string.Format("【{0}{1}】有一条户内报事需要分派，报事人：{2}，请及时分派给相关人员处理，点击查看详情。", commName, roomSign, incidentInfo.IncidentMan)
                                };
                                pushModel1.Audience.Objects.AddRange(assignObjectsXiaoMi);

                                TWTools.Push.Push.SendAsync(pushModel1);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (!EventLog.SourceExists("TWInterface"))
                    {
                        EventLog.CreateEventSource("TWInterface", "Push");
                    }
                    EventLog.WriteEntry("TWInterface", ex.Message + ex.StackTrace, EventLogEntryType.Error);
                }
            });
        }

        /// <summary>
        /// 公区报事推送
        /// </summary>
        public static void SynchPushPublicIncident(Tb_HSPR_IncidentAccept incidentInfo, string sign = "")
        {
            if (incidentInfo.TypeID == null || incidentInfo.TypeID.Trim(new char[] { ',', ' ' }) == "")
            {
                return;
            }

            string tw2bsConnectionString = PubConstant.tw2bsConnectionString;
            string hmWyglConnectionString = PubConstant.hmWyglConnectionString;
            string corpId = Global_Var.CorpId;

            Task.Run(() =>
            {
                try
                {

                    if (Common.Push.GetAppKeyAndAppSecret(tw2bsConnectionString, corpId, out string appIdentifier, out string appKey, out string appSecret))
                    {
                        using (IDbConnection conn = new SqlConnection(hmWyglConnectionString))
                        {
                            incidentInfo.TypeID = incidentInfo.TypeID.Trim(new char[] { ',', ' ' });

                            // 小区信息
                            string commName = "未知";
                            string sql = string.Format(@"SELECT CommName FROM Tb_HSPR_Community WHERE CommID=@CommID");
                            dynamic commInfo = conn.Query(sql, new { CommID = incidentInfo.CommID }).FirstOrDefault();

                            if (commInfo != null)
                            {
                                commName = commInfo.CommName;
                            }

                            List<string> assignObjects = new List<string>();        // 具有分派权限的对象
                            List<string> snatchObjects = new List<string>();        // 具有抢单权限的对象

                            List<string> assignObjectsHuaWei = new List<string>();        // 具有分派权限的对象
                            List<string> snatchObjectsHuaWei = new List<string>();        // 具有抢单权限的对象

                            // 1、判断报事类别是否设置了分派岗位
                            sql = string.Format(@"SELECT RoleCode FROM Tb_HSPR_IncidentTypeAssignedPost WHERE CorpTypeID IN (SELECT CorpTypeID FROM Tb_HSPR_IncidentType WHERE TypeID IN (SELECT Convert(nvarchar(50), ColName) FROM dbo.funSplitTabel('{0}', ',')))", incidentInfo.TypeID);

                            IEnumerable<dynamic> assignedPostResult = conn.Query(sql);

                            if (assignedPostResult.Count() > 0)
                            {
                                // 获取具有分派岗位的人员信息
                                sql = string.Format(@"SELECT MobileTel,p.phonebrand,p.phoneToken FROM view_Sys_User_RoleData_Filter2  u left join tb_sys_user_phone p on u.UserCode = p.UserCode
                                                    WHERE CommID={0} AND IsMobile=1 AND isnull(MobileTel,'')<>'' 
                                                    AND u.UserCode IN (SELECT UserCode FROM Tb_Sys_UserRole WHERE RoleCode IN(
                                                        SELECT RoleCode FROM Tb_Sys_Role WHERE SysRoleCode IN (
                                                            SELECT RoleCode from Tb_HSPR_IncidentTypeAssignedPost WHERE CorpTypeID IN 
                                                                (SELECT CorpTypeID FROM Tb_HSPR_IncidentType WHERE TypeID IN 
                                                    (SELECT Convert(nvarchar(50), ColName) FROM dbo.funSplitTabel('{1}', ','))))))", incidentInfo.CommID, incidentInfo.TypeID);

                                IEnumerable<dynamic> assignedPostUsers = conn.Query(sql);

                                if (assignedPostUsers.Count() > 0)
                                {
                                    foreach (dynamic item in assignedPostUsers)
                                    {
                                        if (item.phoneToken != null && !string.IsNullOrEmpty(item.phoneToken.ToString()))
                                        {
                                            if (item.phonebrand != null && !string.IsNullOrEmpty(item.phonebrand.ToString()))
                                            {
                                                if ("honor".Equals(item.phonebrand.ToString()) || "huawei".Equals(item.phonebrand.ToString()))
                                                {
                                                    assignObjectsHuaWei.Add(item.phoneToken.ToString());
                                                }
                                                else
                                                {
                                                    assignObjects.Add(item.MobileTel);
                                                }
                                            }
                                            else
                                            {
                                                assignObjects.Add(item.MobileTel);
                                            }
                                        }
                                        else
                                        {
                                            assignObjects.Add(item.MobileTel);
                                        }
                                        //assignObjects.Add(item.MobileTel);
                                    }
                                    assignObjects = assignObjects.Distinct().ToList();
                                    assignObjectsHuaWei = assignObjectsHuaWei.Distinct().ToList();
                                }
                            }

                            // 2、判断报事类别是否设置了处理岗位
                            sql = string.Format(@"SELECT RoleCode FROM Tb_HSPR_IncidentTypeProcessPost WHERE CorpTypeID IN (SELECT CorpTypeID FROM Tb_HSPR_IncidentType WHERE TypeID IN (SELECT Convert(nvarchar(50), ColName) FROM dbo.funSplitTabel('{0}', ',')))", incidentInfo.TypeID);

                            IEnumerable<dynamic> snatchRoleResult = conn.Query(sql);

                            if (snatchRoleResult.Count() > 0)
                            {
                                // 获取所有具有处理岗位权限的人员的手机号码
                                sql = string.Format(@"SELECT MobileTel,p.phonebrand,p.phoneToken FROM view_Sys_User_RoleData_Filter2 u left join tb_sys_user_phone p on u.UserCode = p.UserCode
                                                    WHERE CommID={0} AND IsMobile=1 AND isnull(MobileTel,'')<>'' 
                                                    AND UserCode IN (SELECT UserCode FROM Tb_Sys_UserRole WHERE RoleCode IN(
                                                        SELECT RoleCode FROM Tb_Sys_Role WHERE SysRoleCode IN (
                                                            SELECT RoleCode from Tb_HSPR_IncidentTypeProcessPost WHERE CorpTypeID IN 
                                                                (SELECT CorpTypeID FROM Tb_HSPR_IncidentType WHERE TypeID IN 
                                                    (SELECT Convert(nvarchar(50), ColName) FROM dbo.funSplitTabel('{1}', ','))))))", incidentInfo.CommID, incidentInfo.TypeID);

                                IEnumerable<dynamic> snatchResult = conn.Query(sql);

                                if (snatchResult.Count() > 0)
                                {
                                    foreach (dynamic item in snatchResult)
                                    {
                                        if (item.phoneToken != null && !string.IsNullOrEmpty(item.phoneToken.ToString()))
                                        {
                                            if (item.phonebrand != null && !string.IsNullOrEmpty(item.phonebrand.ToString()))
                                            {
                                                if ("honor".Equals(item.phonebrand.ToString()) || "huawei".Equals(item.phonebrand.ToString()))
                                                {
                                                    snatchObjectsHuaWei.Add(item.phoneToken.ToString());
                                                }
                                                else
                                                {
                                                    snatchObjects.Add(item.MobileTel);
                                                }
                                            }
                                            else
                                            {
                                                snatchObjects.Add(item.MobileTel);
                                            }
                                        }
                                        else
                                        {
                                            snatchObjects.Add(item.MobileTel);
                                        }
                                        //snatchObjects.Add(item.MobileTel);
                                    }
                                }
                                snatchObjects = snatchObjects.Distinct().ToList();
                                snatchObjectsHuaWei = snatchObjectsHuaWei.Distinct().ToList();
                            }

                            // 推送
                            PushModel pushModel = new PushModel(appKey, appSecret)
                            {
                                AppIdentifier = appIdentifier,
                                Badge = 1,
                                KeyInfomation = incidentInfo.IncidentID.ToString()
                            };

                            // 鸿坤自定义铃声 中集
                            if (corpId == "1973" || corpId == "1953")
                            {
                                pushModel.Sound = "push_alert_property.m4a";
                            }


                            pushModel.Audience.Category = PushAudienceCategory.Alias;
                            pushModel.Extras.Add("CommID", incidentInfo.CommID.ToString());
                            pushModel.Extras.Add("IncidentID", incidentInfo.IncidentID.ToString());
                            pushModel.Extras.Add("IncidentPlace", incidentInfo.IncidentPlace);

                            // 具有分派且同时具有抢单权限的人
                            List<string> canAssignAndSnatchObjects = assignObjects.Intersect(snatchObjects).ToList();
                            List<string> canAssignAndSnatchObjectsHuaWei = assignObjectsHuaWei.Intersect(snatchObjectsHuaWei).ToList();
                            if (canAssignAndSnatchObjects.Count > 0)
                            {
                                // 取差集
                                assignObjects = assignObjects.Except(canAssignAndSnatchObjects).ToList();
                                snatchObjects = snatchObjects.Except(canAssignAndSnatchObjects).ToList();

                                pushModel.Message = string.Format("【{0}】有一条公区报事需要处理，报事人：{1}，您可以分派给他人处理或自行抢单后处理，点击查看详情。{2}", commName, incidentInfo.IncidentMan, (string.IsNullOrEmpty(sign) ? "" : ("【" + sign + "】")));

                                pushModel.Command = PushCommand.INCIDENT_SNATCH_ASSIGN;
                                pushModel.CommandName = PushCommand.CommandNameDict[pushModel.Command];
                                pushModel.Audience.Objects.AddRange(canAssignAndSnatchObjects);

                                TWTools.Push.Push.SendAsync(pushModel);
                            }
                            if (canAssignAndSnatchObjectsHuaWei.Count > 0)
                            {
                                // 取差集
                                assignObjectsHuaWei = assignObjectsHuaWei.Except(canAssignAndSnatchObjectsHuaWei).ToList();
                                snatchObjectsHuaWei = snatchObjectsHuaWei.Except(canAssignAndSnatchObjectsHuaWei).ToList();

                                PushModel pushModel1 = new PushModel(appKey, appSecret, PushChannel.HWPush)
                                {
                                    AppIdentifier = appIdentifier,
                                    Badge = 1,
                                    KeyInfomation = incidentInfo.IncidentID.ToString(),
                                    Sound = "push_alert_property.m4a",
                                    Message = string.Format("【{0}】有一条公区报事需要处理，报事人：{1}，您可以分派给他人处理或自行抢单后处理，点击查看详情。{2}", commName, incidentInfo.IncidentMan, (string.IsNullOrEmpty(sign) ? "" : ("【" + sign + "】")))
                                };
                                pushModel1.Audience.Objects.AddRange(assignObjectsHuaWei);

                                TWTools.Push.Push.SendAsync(pushModel1);
                            }

                            // 推送分派
                            if (assignObjects.Count > 0)
                            {
                                pushModel.Message = string.Format("【{0}】有一条公区报事需要分派，报事人：{1}，请及时分派给相关人员处理，点击查看详情。{2}", commName, incidentInfo.IncidentMan, (string.IsNullOrEmpty(sign) ? "" : ("【" + sign + "】")));

                                pushModel.Command = PushCommand.INCIDENT_ASSIGN;
                                pushModel.CommandName = PushCommand.CommandNameDict[pushModel.Command];
                                pushModel.Audience.Objects.Clear();
                                pushModel.Audience.Objects.AddRange(assignObjects);

                                TWTools.Push.Push.SendAsync(pushModel);
                            }

                            // 华为推送分派
                            if (assignObjectsHuaWei.Count > 0)
                            {
                                PushModel pushModel5 = new PushModel(appKey, appSecret, PushChannel.HWPush)
                                {
                                    AppIdentifier = appIdentifier,
                                    Badge = 1,
                                    KeyInfomation = incidentInfo.IncidentID.ToString(),
                                    Sound = "push_alert_property.m4a",
                                    Message = string.Format("【{0}】有一条公区报事需要分派，报事人：{1}，请及时分派给相关人员处理，点击查看详情。{2}", commName, incidentInfo.IncidentMan, (string.IsNullOrEmpty(sign) ? "" : ("【" + sign + "】")))
                            };
                                pushModel5.Audience.Objects.AddRange(assignObjectsHuaWei);

                                TWTools.Push.Push.SendAsync(pushModel5);
                            }

                            // 推送抢单
                            if (snatchObjects.Count > 0)
                            {
                                // 泰禾不推送抢单
                                if (corpId == "1970")
                                {
                                    return;
                                }

                                pushModel.Message = string.Format("【{0}】有一条公区报事，报事人：{1}，您可以进行抢单处理，点击查看详情。{2}", commName, incidentInfo.IncidentMan, (string.IsNullOrEmpty(sign) ? "" : ("【" + sign + "】")));

                                pushModel.Command = PushCommand.INCIDENT_SNATCH;
                                pushModel.CommandName = PushCommand.CommandNameDict[pushModel.Command];
                                pushModel.Audience.Objects.Clear();
                                pushModel.Audience.Objects.AddRange(snatchObjects);

                                TWTools.Push.Push.SendAsync(pushModel);
                            }

                            if (snatchObjectsHuaWei.Count > 0)
                            {
                                PushModel pushModel6 = new PushModel(appKey, appSecret, PushChannel.HWPush)
                                {
                                    AppIdentifier = appIdentifier,
                                    Badge = 1,
                                    KeyInfomation = incidentInfo.IncidentID.ToString(),
                                    Sound = "push_alert_property.m4a",
                                    Message = string.Format("【{0}】有一条公区报事，报事人：{1}，您可以进行抢单处理，点击查看详情。{2}", commName, incidentInfo.IncidentMan, (string.IsNullOrEmpty(sign) ? "" : ("【" + sign + "】")))
                            };
                                pushModel6.Audience.Objects.AddRange(snatchObjectsHuaWei);

                                TWTools.Push.Push.SendAsync(pushModel6);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (!EventLog.SourceExists("TWInterface"))
                    {
                        EventLog.CreateEventSource("TWInterface", "Push");
                    }
                    EventLog.WriteEntry("TWInterface", ex.Message, EventLogEntryType.Error);
                }
            });
        }

        /// <summary>
        /// 报事分派后推送给业主及处理人员
        /// </summary>
        public static void SynchPushAfterAssign(Tb_HSPR_IncidentAccept incidentInfo, bool notifyCust = true)
        {
            string tw2bsConnectionString = PubConstant.tw2bsConnectionString;
            string hmWyglConnectionString = PubConstant.hmWyglConnectionString;
            string corpId = Global_Var.CorpId;

            Task.Run(() =>
            {
                try
                {
                    string appIdentifier = null, appKey = null, appSecret = null;

                    // 1、推送给业主
                    if (notifyCust && string.IsNullOrEmpty(incidentInfo.Phone) == false)
                    {
                        if (Common.Push.GetAppKeyAndAppSecret(tw2bsConnectionString, corpId, out appIdentifier, out appKey, out appSecret, true))
                        {
                            PushModel pushModelForCust = new PushModel()
                            {
                                AppIdentifier = appIdentifier,
                                AppKey = appKey,
                                AppSecret = appSecret,
                                Command = PushCommand.INCIDENT_PROCESSING,
                                CommandName = "业主" + PushCommand.CommandNameDict[PushCommand.INCIDENT_PROCESSING],
                                Badge = 1,
                                KeyInfomation = incidentInfo.IncidentID.ToString(),
                                NotificationWay = NotificationWay.All
                            };

                            // 鸿坤自定义铃声
                            if (corpId == "1973")
                            {
                                pushModelForCust.Sound = "push_alert_property.m4a";
                            }

                            pushModelForCust.Extras.Add("CommID", incidentInfo.CommID.ToString());
                            pushModelForCust.Extras.Add("IncidentID", incidentInfo.IncidentID.ToString());
                            pushModelForCust.Extras.Add("IncidentPlace", incidentInfo.IncidentPlace);
                            pushModelForCust.Extras.Add("CoordinateNum", incidentInfo.CoordinateNum);

                            pushModelForCust.Message = "您提交的报事已分派给相关物管工作人员，稍后将会有物管人员联系您，请保持电话畅通。";

                            pushModelForCust.Audience.Category = PushAudienceCategory.Alias;
                            pushModelForCust.Audience.Objects.Add(incidentInfo.Phone);
                            TWTools.Push.Push.SendAsync(pushModelForCust);
                        }
                    }

                    // 2、推送给物管
                    if (Common.Push.GetAppKeyAndAppSecret(tw2bsConnectionString, corpId, out appIdentifier, out appKey, out appSecret))
                    {
                        PushModel pushModel = new PushModel()
                        {
                            AppIdentifier = appIdentifier,
                            AppKey = appKey,
                            AppSecret = appSecret,
                            Command = PushCommand.INCIDENT_PROCESSING,
                            CommandName = "物管" + PushCommand.CommandNameDict[PushCommand.INCIDENT_PROCESSING],
                            Badge = 1,
                            KeyInfomation = incidentInfo.IncidentID.ToString()
                        };

                        // 鸿坤自定义铃声 中集
                        if (corpId == "1973" || corpId == "1953")
                        {
                            pushModel.Sound = "push_alert_property.m4a";
                        }


                        pushModel.Audience.Category = PushAudienceCategory.Alias;

                        pushModel.Extras.Add("CommID", incidentInfo.CommID.ToString());
                        pushModel.Extras.Add("IncidentID", incidentInfo.IncidentID.ToString());
                        pushModel.Extras.Add("IncidentPlace", incidentInfo.IncidentPlace);
                        pushModel.Extras.Add("CoordinateNum", incidentInfo.CoordinateNum);

                        using (IDbConnection conn = new SqlConnection(hmWyglConnectionString))
                        {
                            // 1、获取处理人员的手机号码
                            // FROM view_Sys_User_RoleData_Filter2 u left join tb_sys_user_phone p on u.UserCode = p.UserCode
                            string sql = @"SELECT UserName,UserCode,MobileTel,p.phonebrand,p.phoneToken FROM Tb_Sys_User u left join tb_sys_user_phone p on u.UserCode = p.UserCode
                                        WHERE IsMobile=1 AND isnull(MobileTel,'')<>'' AND isnull(IsDelete,0)=0 AND 
                                        u.UserCode IN (SELECT UserCode FROM Tb_HSPR_IncidentAcceptDeal WHERE IncidentID=@IncidentID)";

                            IEnumerable<dynamic> userInfos = conn.Query(sql, new { IncidentID = incidentInfo.IncidentID });

                            if (userInfos.Count() > 0)
                            {
                                List<string> audienceObjects = new List<string>();
                                List<string> audienceObjectsHuaWei = new List<string>();

                                foreach (dynamic item in userInfos)
                                {
                                    if (item.phoneToken != null && !string.IsNullOrEmpty(item.phoneToken.ToString()))
                                    {
                                        if (item.phonebrand != null && !string.IsNullOrEmpty(item.phonebrand.ToString()))
                                        {
                                            if ("honor".Equals(item.phonebrand.ToString()) || "huawei".Equals(item.phonebrand.ToString()))
                                            {
                                                audienceObjectsHuaWei.Add(item.phoneToken.ToString());
                                            }
                                            else
                                            {
                                                audienceObjects.Add(item.MobileTel.ToString());
                                            }
                                        }
                                        else
                                        {
                                            audienceObjects.Add(item.MobileTel.ToString());
                                        }
                                    }
                                    else
                                    {
                                        audienceObjects.Add(item.MobileTel.ToString());
                                    }

                                }

                                if (audienceObjects.Count > 0)
                                {
                                    audienceObjects = audienceObjects.Distinct().ToList();
                                    pushModel.Audience.Objects.AddRange(audienceObjects);

                                    pushModel.Message = string.Format("【{0}】给您分派了一条报事，报事人：{1}，请及时处理，点击查看详情。", incidentInfo.DispMan, incidentInfo.IncidentMan);

                                    TWTools.Push.Push.SendAsync(pushModel);
                                }

                                //判断是否有华为推送
                                if (audienceObjectsHuaWei.Count() > 0)
                                {
                                    PushModel pushModel1 = new PushModel(appKey, appSecret, PushChannel.HWPush)
                                    {
                                        AppIdentifier = appIdentifier,
                                        Badge = 1,
                                        KeyInfomation = incidentInfo.IncidentID.ToString(),
                                        Sound = "push_alert_property.m4a",
                                        Message = string.Format("【{0}】给您分派了一条报事，报事人：{1}，请及时处理，点击查看详情。", incidentInfo.DispMan, incidentInfo.IncidentMan)
                                    };
                                    pushModel1.Audience.Objects.AddRange(audienceObjectsHuaWei);

                                    TWTools.Push.Push.SendAsync(pushModel1);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (!EventLog.SourceExists("TWInterface"))
                    {
                        EventLog.CreateEventSource("TWInterface", "Push");
                    }
                    EventLog.WriteEntry("TWInterface", ex.Message, EventLogEntryType.Error);
                }
            });
        }

        /// <summary>
        /// 报事退回
        /// </summary>
        /// <param name="incidentInfo"></param>
        public static void SynchPushWhenReturn(Tb_HSPR_IncidentAccept incidentInfo, string userName)
        {
            string tw2bsConnectionString = PubConstant.tw2bsConnectionString;
            string hmWyglConnectionString = PubConstant.hmWyglConnectionString;
            string corpId = Global_Var.CorpId;

            Task.Run(() =>
            {
                try
                {
                    string appIdentifier = null, appKey = null, appSecret = null;

                    if (Common.Push.GetAppKeyAndAppSecret(tw2bsConnectionString, corpId, out appIdentifier, out appKey, out appSecret))
                    {
                        PushModel pushModel = new PushModel()
                        {
                            AppIdentifier = appIdentifier,
                            AppKey = appKey,
                            AppSecret = appSecret,
                            Command = PushCommand.INCIDENT_PROCESSING,
                            CommandName = PushCommand.CommandNameDict[PushCommand.INCIDENT_PROCESSING],
                            Badge = 1,
                            KeyInfomation = incidentInfo.IncidentID.ToString()
                        };

                        // 鸿坤自定义铃声 中集
                        if (corpId == "1973" || corpId == "1953")
                        {
                            pushModel.Sound = "push_alert_property.m4a";
                        }


                        pushModel.Audience.Category = PushAudienceCategory.Alias;

                        pushModel.Extras.Add("CommID", incidentInfo.CommID.ToString());
                        pushModel.Extras.Add("IncidentID", incidentInfo.IncidentID.ToString());
                        pushModel.Extras.Add("IncidentPlace", incidentInfo.IncidentPlace);
                        pushModel.Extras.Add("CoordinateNum", incidentInfo.CoordinateNum);

                        using (IDbConnection conn = new SqlConnection(hmWyglConnectionString))
                        {
                            // 1、获取处理人员的手机号码
                            string sql = @"SELECT UserName,UserCode,MobileTel FROM Tb_Sys_User 
                                        WHERE IsMobile=1 AND isnull(MobileTel,'')<>'' AND isnull(IsDelete,0)=0 AND 
                                        UserCode IN (SELECT UserCode FROM Tb_HSPR_IncidentAcceptDeal WHERE IncidentID=@IncidentID)";

                            IEnumerable<dynamic> userInfos = conn.Query(sql, new { IncidentID = incidentInfo.IncidentID });

                            if (userInfos.Count() > 0)
                            {
                                List<string> audienceObjects = new List<string>();

                                foreach (dynamic item in userInfos)
                                {
                                    audienceObjects.Add(item.MobileTel.ToString());
                                }

                                if (audienceObjects.Count > 0)
                                {
                                    audienceObjects = audienceObjects.Distinct().ToList();
                                    pushModel.Audience.Objects.AddRange(audienceObjects);

                                    pushModel.Message = string.Format("【{0}】退回了您标记处理完成的编号为【{1}】的{2}报事，请及时重新处理，点击查看详情。",
                                        userName, incidentInfo.IncidentNum, incidentInfo.IncidentPlace.Contains("户内") ? "户内" : "公区");

                                    TWTools.Push.Push.SendAsync(pushModel);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (!EventLog.SourceExists("TWInterface"))
                    {
                        EventLog.CreateEventSource("TWInterface", "Push");
                    }
                    EventLog.WriteEntry("TWInterface", ex.Message, EventLogEntryType.Error);
                }
            });
        }

        /// <summary>
        /// 预计到场
        /// </summary>
        public static void SynchPushPlanArrive(Tb_HSPR_IncidentAccept incidentInfo, string planArriveDate)
        {
            if (string.IsNullOrEmpty(incidentInfo.Phone))
            {
                return;
            }

            string tw2bsConnectionString = PubConstant.tw2bsConnectionString;
            string corpId = Global_Var.CorpId;

            Task.Run(() =>
            {
                try
                {
                    if (Common.Push.GetAppKeyAndAppSecret(tw2bsConnectionString, corpId,
                            out string appIdentifier, out string appKey, out string appSecret, true))
                    {
                        PushModel pushModel = new PushModel(appKey, appSecret)
                        {
                            AppIdentifier = appIdentifier,
                            Command = PushCommand.INCIDENT_PROCESSING_PLAN_ARRIVE,
                            CommandName = PushCommand.CommandNameDict[PushCommand.INCIDENT_PROCESSING_PLAN_ARRIVE],
                            Badge = 0,
                            KeyInfomation = incidentInfo.IncidentID.ToString(),
                            NotificationWay = NotificationWay.All,
                            Message = $"您提交的报事已分派给：{incidentInfo.DealMan}，维修师傅预计在{planArriveDate}左右到达现场，请保持手机畅通。",
                        };

                        // 鸿坤自定义铃声 中集
                        if (corpId == "1973" || corpId == "1953")
                        {
                            pushModel.Sound = "push_alert_property.m4a";
                        }

                        pushModel.Audience.Category = PushAudienceCategory.Alias;
                        pushModel.Audience.Objects.Add(incidentInfo.Phone);

                        pushModel.Extras.Add("IncidentID", incidentInfo.IncidentID.ToString());

                        TWTools.Push.Push.SendAsync(pushModel);
                    }
                }
                catch (Exception ex)
                {
                    if (!EventLog.SourceExists("TWInterface"))
                    {
                        EventLog.CreateEventSource("TWInterface", "Push");
                    }
                    EventLog.WriteEntry("TWInterface", ex.Message, EventLogEntryType.Error);
                }
            });
        }

        /// <summary>
        /// 报事处理完后推送给业主
        /// </summary>
        public static void SynchPushWhenProcessed(Tb_HSPR_IncidentAccept incidentInfo)
        {
            if (string.IsNullOrEmpty(incidentInfo.Phone))
            {
                return;
            }

            string tw2bsConnectionString = PubConstant.tw2bsConnectionString;
            string corpId = Global_Var.CorpId;

            Task.Run(() =>
            {
                try
                {
                    if (Common.Push.GetAppKeyAndAppSecret(tw2bsConnectionString, corpId,
                            out string appIdentifier, out string appKey, out string appSecret, true))
                    {
                        PushModel pushModel = new PushModel(appKey, appSecret)
                        {
                            AppIdentifier = appIdentifier,
                            Command = PushCommand.INCIDENT_END,
                            CommandName = PushCommand.CommandNameDict[PushCommand.INCIDENT_END],
                            Badge = 1,
                            KeyInfomation = incidentInfo.IncidentID.ToString(),
                            NotificationWay = NotificationWay.All
                        };

                        // 鸿坤自定义铃声 中集
                        if (corpId == "1973" || corpId == "1953")
                        {
                            pushModel.Sound = "push_alert_property.m4a";
                        }


                        if (incidentInfo.IncidentPlace == "业主权属")
                        {
                            pushModel.Message = "您提交的报事已处理完毕，记得打开App给师傅的服务打分哦。";
                        }
                        else if (incidentInfo.IncidentPlace == "公共区域")
                        {
                            pushModel.Message = "您提交的报事已处理完毕，感谢您的支持！";
                        }
                        else
                        {
                            pushModel.Message = "您提交的报事已处理完毕。";
                        }

                        pushModel.Audience.Category = PushAudienceCategory.Alias;
                        pushModel.Audience.Objects.Add(incidentInfo.Phone);

                        pushModel.Extras.Add("CommID", incidentInfo.CommID.ToString());
                        pushModel.Extras.Add("IncidentID", incidentInfo.IncidentID.ToString());
                        pushModel.Extras.Add("IncidentPlace", incidentInfo.IncidentPlace);
                        pushModel.Extras.Add("CoordinateNum", incidentInfo.CoordinateNum);

                        TWTools.Push.Push.SendAsync(pushModel);
                    }

                    // 以后可能会推送给分派人
                }
                catch (Exception ex)
                {
                    if (!EventLog.SourceExists("TWInterface"))
                    {
                        EventLog.CreateEventSource("TWInterface", "Push");
                    }
                    EventLog.WriteEntry("TWInterface", ex.Message, EventLogEntryType.Error);
                }
            });
        }



        //合景商业微信推送

        public static void SynchPush2WeChatAfterAssign(Tb_HSPR_IncidentAccept incidentInfo)
        {

            dynamic info = GetOpenIdAndType(incidentInfo.IncidentID.ToString());
            if (info != null)
            {
                string openId = info.OpenId;
                int type = info.Type;
                string msg = GetAssignMsg(incidentInfo.IncidentID.ToString());
                string url = Global_Fun.AppWebSettings("WECHAT_PUSH_URL");

                //根据Type来判断 type=0为小程序推送，type=1为公众号推送
                switch (type)
                {
                    case 0:

                        break;
                    case 1:
                        var result = Get($@"{url}&openid={openId}&content={msg}");
                        new Logger().WriteLog("接口推送日志", "IncidentID:" + incidentInfo.IncidentID.ToString() + "返回结果" + result);
                        break;
                }
            }


        }

        public static void SynchPush2WeChatComplete(Tb_HSPR_IncidentAccept incidentInfo)
        {

            dynamic info = GetOpenIdAndType(incidentInfo.IncidentID.ToString());
            if (info != null)
            {
                string openId = info.OpenId;
                int type = info.Type;
                string msg = GetCompleteMsg(incidentInfo.RoomID.ToString(), incidentInfo.CustID.ToString());
                string url = Global_Fun.AppWebSettings("WECHAT_PUSH_URL");

                //根据Type来判断 type=0为小程序推送，type=1为公众号推送
                switch (type)
                {
                    case 0:

                        break;
                    case 1:
                        var result = Get($@"{url}&openid={openId}&content={msg}");
                        new Logger().WriteLog("接口推送日志", "IncidentID:" + incidentInfo.IncidentID.ToString() + "返回结果" + result);
                        break;
                }
            }


        }



        public static string GetAssignMsg(string incidentID)
        {
            string msg = $"<a href=\"http://twsyapp.kwgproperty.com:8666/Incident/Detail_v2?IncidentID={incidentID}\">您提交的报事已分派</a>";

            return HttpUtility.UrlEncode(msg);
        }
        public static string GetCompleteMsg(string roomID, string custID)
        {
            string msg = $"<a href=\"http://twsyapp.kwgproperty.com:8666/Incident/Eval?RoomID={roomID}&CustID={custID}\">您提交的报事已完成</a>";

            return HttpUtility.UrlEncode(msg);
        }


        private static string Get(string url)
        {
            DotNet4.Utilities.HttpHelper http = new DotNet4.Utilities.HttpHelper();
            new Logger().WriteLog("接口推送日志", "url：" + url);
            DotNet4.Utilities.HttpItem httpItem = new DotNet4.Utilities.HttpItem()
            {
                URL = url,//URL     必需项  
                Method = "Get",//URL     可选项 默认为Get  
                Timeout = 5000,//连接超时时间     可选项默认为100000  
                ReadWriteTimeout = 5000,//写入Post数据超时时间     可选项默认为30000  
                Accept = "*/*",
                ContentType = "application/x-www-form-urlencoded",
                Encoding = Encoding.UTF8,
                PostEncoding = Encoding.UTF8,
                ResultType = DotNet4.Utilities.ResultType.String,//返回数据类型，是Byte还是String  
            };
            DotNet4.Utilities.HttpResult result = http.GetHtml(httpItem);
            return result.Html;
        }


        public static dynamic GetOpenIdAndType(string incidentId)
        {
            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                dynamic info = conn.Query(@"SELECT OpenId,Type FROM Tb_TwApi_Incident_Push WHERE IncidentID = @IncidentID",
                    new { @IncidentID = incidentId }).FirstOrDefault();//判断Tb_TwApi_Incident_Push表中对应的IncidentID是否有记录
                conn.Dispose();
                return info;
            }

        }


    }
}
