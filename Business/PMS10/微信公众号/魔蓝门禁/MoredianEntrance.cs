using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using allinpay.utils;
using Aop.Api;
using Aop.Api.Model;
using Aop.Api.Request;
using Aop.Api.Response;
using Business.WChat2020.Libs;
using Business.WChat2020.Model;
using Common;
using Common.Extenions;
using Dapper;
using DapperExtensions;
using fjnxpay.utils;
using log4net;
using mixuespay.utils;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using Model.支付配置模型;
using Model.支付配置模型.华宇通联;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TianChengEntranceSyncService.Redis;
using TianChengEntranceSyncService.同步任务;

namespace Business.WChat2020
{
    public partial class Wx_PMS
    {
        private string GetVisitorInfo(DataRow row)
        {
            try
            {
                #region 获取参数并简单校验
                string RecordID = string.Empty;
                if (row.Table.Columns.Contains("RecordID"))
                {
                    RecordID = row["RecordID"].ToString();
                }
                if (string.IsNullOrEmpty(RecordID))
                {
                    return new WxResponse(0, "该记录不存在或已过期", null).toJson();
                }
                #endregion
                using (IDbConnection erpConn = new SqlConnection(erpConnStr))
                {
                    dynamic info = erpConn.QueryFirstOrDefault("SELECT b.CommName, b.RoomSign, b.RoomName, a.MemberName, a.MemberMobile, a.MemberType, a.BeginDate, a.EndDate FROM Tb_HSPR_Entrance_Group_Visitor_Record a WITH(NOLOCK) JOIN view_HSPR_Room_Filter b WITH(NOLOCK) ON a.CommID = b.CommID AND a.RoomID = b.RoomID WHERE a.RecordID = @RecordID", new { RecordID });
                    if(null == info)
                    {
                        return new WxResponse(0, "该记录不存在或已过期", null).toJson();
                    }
                    string Mobile = Convert.ToString(info.MemberMobile);
                    #region 先查询/创建组织机构
                    TianChengEntranceSyncService.Config.EntranceConnectionStr = erpConnStr;
                    TianChengEntranceSyncService.Config.WChat2020ConnectionStr = PubConstant.WChat2020ConnectionString;
                    MoredianOrg moredianOrg = OrgRedis.GetMoredianOrg();
                    if (null == moredianOrg)
                    {
                        return new WxResponse(0, "组织机构配置错误，请联系管理员", null).toJson();
                    }
                    #endregion
                    #region 获取组织机构的AccessToken
                    string access_token = AccessTokenRedis.GetAccessToken(Convert.ToString(moredianOrg.orgId), moredianOrg.orgAuthKey);
                    if (string.IsNullOrEmpty(access_token))
                    {
                        return new WxResponse(0, "获取AccessToken失败，请联系管理员", null).toJson();
                    }
                    #endregion
                    MoredianMember moredianMember = MemberRedis.GetMoredianMember(Mobile);
                    if(null != moredianMember)
                    {
                        info.showFace = moredianMember.showFace;
                    }
                    else
                    {
                        info.showFace = "";
                    }
                    return new WxResponse(200, "查询成功", info).toJson();
                }
            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                return new WxResponse(0, "响应异常", null).toJson();
            }
        }
        /// <summary>
        /// 创建访客记录
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>

        private string CreateVisitor(DataRow row)
        {
            try
            {
                #region 获取参数并简单校验
                string Mobile = string.Empty;
                if (row.Table.Columns.Contains("Mobile"))
                {
                    Mobile = row["Mobile"].ToString();
                }
                if (string.IsNullOrEmpty(Mobile))
                {
                    return new WxResponse(0, "用户不存在", null).toJson();
                }
                if (Mobile.Length != 11)
                {
                    return new WxResponse(0, "用户不存在", null).toJson();
                }
                if (!row.Table.Columns.Contains("UserID") || !int.TryParse(row["UserID"].ToString(), out int UserID))
                {
                    return new WxResponse(0, "用户不存在", null).toJson();
                }
                string MemberName = string.Empty;
                if (row.Table.Columns.Contains("MemberName"))
                {
                    MemberName = row["MemberName"].ToString();
                }
                if (string.IsNullOrEmpty(MemberName))
                {
                    return new WxResponse(0, "访客姓名不能为空", null).toJson();
                }
                string MemberMobile = string.Empty;
                if (row.Table.Columns.Contains("MemberMobile"))
                {
                    MemberMobile = row["MemberMobile"].ToString();
                }
                if (string.IsNullOrEmpty(MemberMobile))
                {
                    return new WxResponse(0, "访客电话不能为空", null).toJson();
                }
                if (MemberMobile.Length != 11)
                {
                    return new WxResponse(0, "访客电话不正确", null).toJson();
                }
                if (!row.Table.Columns.Contains("MemberType") || !int.TryParse(row["MemberType"].ToString(), out int MemberType))
                {
                    return new WxResponse(0, "请选择访客类型", null).toJson();
                }
                if(MemberType != 0 && MemberType != 1&& MemberType != 2)
                {
                    return new WxResponse(0, "访客类型不正确", null).toJson();
                }
                if (!row.Table.Columns.Contains("BeginDate") || !DateTime.TryParse(row["BeginDate"].ToString(), out DateTime BeginDate))
                {
                    return new WxResponse(0, "请选择来访时间", null).toJson();
                }
                if (!row.Table.Columns.Contains("EndDate") || !DateTime.TryParse(row["EndDate"].ToString(), out DateTime EndDate))
                {
                    return new WxResponse(0, "请选择来访时间", null).toJson();
                }
                DateTime DateNow = DateTime.Now;
                if (DateNow.CompareTo(BeginDate) > 0)
                {
                    return new WxResponse(0, "请重新选择正确的来访时间", null).toJson();
                }
                if (BeginDate.CompareTo(EndDate) > 0)
                {
                    return new WxResponse(0, "请重新选择正确的来访时间", null).toJson();
                }
                string CommID = string.Empty;
                if (row.Table.Columns.Contains("CommID"))
                {
                    CommID = row["CommID"].ToString();
                }
                if (string.IsNullOrEmpty(CommID))
                {
                    return new WxResponse(0, "请选择默认房屋(1001)", null).toJson();
                }
                string RoomID = string.Empty;
                if (row.Table.Columns.Contains("RoomID"))
                {
                    RoomID = row["RoomID"].ToString();
                }
                if (string.IsNullOrEmpty(RoomID))
                {
                    return new WxResponse(0, "请选择默认房屋(1002)", null).toJson();
                }
                #endregion

                using (IDbConnection erpConn = new SqlConnection(erpConnStr))
                {
                    erpConn.Open();
                    var trans = erpConn.BeginTransaction();
                    try
                    {
                        dynamic RoomInfo = erpConn.QueryFirstOrDefault("SELECT CommName, RoomSign, RoomName FROM view_HSPR_Room_Filter WITH(NOLOCK) WHERE ISNULL(IsDelete, 0) = 0 AND CommID = @CommID AND RoomID = @RoomID", new { CommID, RoomID }, trans);
                        if(null == RoomInfo)
                        {
                            return new WxResponse(0, "请选择默认房屋(1003)", null).toJson();
                        }
                        string CommName = Convert.ToString(RoomInfo.CommName);
                        string RoomSign = Convert.ToString(RoomInfo.RoomSign);
                        string RoomName = Convert.ToString(RoomInfo.RoomName);
                        string RecordID = Guid.NewGuid().ToString();
                        // 只负责保存到数据库以及发送短信，剩下的群组创建由同步程序执行
                        if (erpConn.Execute("INSERT INTO Tb_HSPR_Entrance_Group_Visitor_Record(CommID, RoomID, MemberName, MemberMobile, MemberType, BeginDate, EndDate, RecordID) VALUES(@CommID, @RoomID, @MemberName, @MemberMobile, @MemberType, @BeginDate, @EndDate, @RecordID)", new { CommID, RoomID, MemberName, MemberMobile, MemberType, BeginDate, EndDate, RecordID }, trans) <= 0)
                        {
                            trans.Rollback();
                            return new WxResponse(0, "保存失败,请重试", null).toJson();
                        }
                        string Content = $"(访客预约)尊敬的{MemberName}，{CommName}-{RoomSign}欢迎您于{BeginDate:MM月dd日HH时mm分}来访，点击http://wx.hoppowy.com/moredian/invite/{RecordID}上传人脸";
                        int result = SendShortMessage(MemberMobile, Content, out string errMsg, Convert.ToInt32(CorpID));
                        if (result != 0)
                        {
                            trans.Rollback();
                            GetLog().Warn($"短信发送失败(Mobile={MemberMobile},Content={Content},msg={errMsg})");
                            return new WxResponse(0, "操作失败,请重试", null).toJson();
                        }
                        GetLog().Warn($"短信发送成功(Mobile={MemberMobile},Content={Content})");
                        trans.Commit();
                        return new WxResponse(200, "操作成功", null).toJson();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                        return new WxResponse(0, "操作失败,请重试", null).toJson();
                    }
                }
            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                return new WxResponse(0, "响应异常", null).toJson();
            }
        }

        /// <summary>
        /// 读取当前魔蓝成员信息
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string GetMoredianMember(DataRow row)
        {
            try
            {
                #region 获取参数并简单校验
                string Mobile = string.Empty;
                if (row.Table.Columns.Contains("Mobile"))
                {
                    Mobile = row["Mobile"].ToString();
                }
                if (string.IsNullOrEmpty(Mobile))
                {
                    return new WxResponse(0, "用户不存在", null).toJson();
                }
                if (Mobile.Length != 11)
                {
                    return new WxResponse(0, "用户不存在", null).toJson();
                }
                DateTime DateNow = DateTime.Now;
                #endregion
                using (IDbConnection conn = new SqlConnection(PubConstant.WChat2020ConnectionString),
                    erpConn = new SqlConnection(erpConnStr))
                {
                    #region 查询用户信息
                    Tb_User tb_User = conn.QueryFirstOrDefault<Tb_User>("SELECT * FROM Tb_User WITH(NOLOCK) WHERE Mobile = @Mobile", new { Mobile });
                    if (null == tb_User)
                    {
                        return new WxResponse(0, "用户不存在", null).toJson();
                    }
                    #endregion

                    #region 获取AppToken
                    TianChengEntranceSyncService.Config.EntranceConnectionStr = erpConnStr;
                    TianChengEntranceSyncService.Config.WChat2020ConnectionStr = PubConstant.WChat2020ConnectionString;
                    string app_token = AppTokenRedis.GetAppToken();
                    if (string.IsNullOrEmpty(app_token))
                    {
                        return new WxResponse(0, "获取AppToken失败，请联系管理员", null).toJson();
                    }
                    #endregion
                    // 成员信息不存在，进行创建
                    #region 先查询/创建组织机构
                    MoredianOrg moredianOrg = OrgRedis.GetMoredianOrg();
                    if (null == moredianOrg)
                    {
                        return new WxResponse(0, "组织机构配置错误，请联系管理员", null).toJson();
                    }
                    #endregion
                    #region 查询人员信息
                    MoredianMember moredianMember = MemberRedis.GetMoredianMember(Mobile);
                    #endregion
                    return new WxResponse(200, "获取成功", moredianMember).toJson();
                }
            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                return new WxResponse(0, "响应异常", null).toJson();
            }
        }
        
        /// <summary>
        /// 提交魔蓝成员信息（创建或者修改成员信息）
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string SubmitMoredianMember(DataRow row)
        {
            try
            {
                #region 获取参数并简单校验
                string Mobile = string.Empty;
                if (row.Table.Columns.Contains("Mobile"))
                {
                    Mobile = row["Mobile"].ToString();
                }
                if (string.IsNullOrEmpty(Mobile))
                {
                    return new WxResponse(0, "用户不存在", null).toJson();
                }
                if (Mobile.Length != 11)
                {
                    return new WxResponse(0, "用户不存在", null).toJson();
                }
                string Face = string.Empty;
                if (row.Table.Columns.Contains("Face"))
                {
                    Face = row["Face"].ToString();
                }
                DateTime DateNow = DateTime.Now;
                #endregion
                
                using (IDbConnection conn = new SqlConnection(PubConstant.WChat2020ConnectionString),
                    erpConn = new SqlConnection(erpConnStr))
                {
                    #region 获取AppToken
                    TianChengEntranceSyncService.Config.EntranceConnectionStr = erpConnStr;
                    TianChengEntranceSyncService.Config.WChat2020ConnectionStr = PubConstant.WChat2020ConnectionString;
                    string app_token = AppTokenRedis.GetAppToken();
                    if (string.IsNullOrEmpty(app_token))
                    {
                        return new WxResponse(0, "获取AppToken失败，请联系管理员", null).toJson();
                    }
                    #endregion
                    #region 先查询/创建组织机构
                    MoredianOrg moredianOrg = OrgRedis.GetMoredianOrg();
                    if (null == moredianOrg)
                    {
                        return new WxResponse(0, "组织机构配置错误，请联系管理员", null).toJson();
                    }
                    #endregion
                    #region 获取组织机构的AccessToken
                    string access_token = AccessTokenRedis.GetAccessToken(Convert.ToString(moredianOrg.orgId), moredianOrg.orgAuthKey);
                    if (string.IsNullOrEmpty(access_token))
                    {
                        return new WxResponse(0, "获取AccessToken失败，请联系管理员", null).toJson();
                    }
                    #endregion
                    MoredianMember moredianMember = MemberRedis.GetMoredianMember(Mobile);

                    #region 读取人脸图片
                    byte[] showFace = null;
                    string IncidentAcceptImageUrl = Global_Fun.AppWebSettings("IncidentAcceptImageUrl");
                    if (Face.Contains(IncidentAcceptImageUrl))
                    {
                        string IncidentAcceptImageSavePath = Global_Fun.AppWebSettings("IncidentAcceptImageSavePath");
                        #region 从本地服务器读取图片
                        // 替换文件路径为本地路径
                        string localFacePath = Face.Replace(IncidentAcceptImageUrl, IncidentAcceptImageSavePath);
                        if (!File.Exists(localFacePath))
                        {
                            return new WxResponse(0, "人脸图片不存在，请重试", null).toJson();
                        }
                        FileStream fileStream = File.OpenRead(localFacePath);
                        int filelength = (int)fileStream.Length;
                        showFace = new byte[filelength];
                        fileStream.Read(showFace, 0, filelength);
                        #endregion
                    }
                    else
                    {
                        #region 从网络获取图片内容
                        {
                            try
                            {
                                HttpHelper http = new HttpHelper();
                                HttpItem item = new HttpItem()
                                {
                                    URL = Face,//URL     必需项  
                                    Method = "GET",//URL     可选项 默认为Get  
                                    Timeout = 5000,//连接超时时间     可选项默认为100000  
                                    ReadWriteTimeout = 5000,//写入Post数据超时时间     可选项默认为30000  
                                    ResultType = ResultType.Byte,//返回数据类型，是Byte还是String  
                                    ProtocolVersion = HttpVersion.Version11,//获取或设置用于请求的 HTTP 版本。默认为 System.Net.HttpVersion.Version11  
                                };
                                HttpResult result = http.GetHtml(item);
                                showFace = result.ResultByte;
                            }
                            catch (Exception ex)
                            {
                                return new WxResponse(0, "读取人脸图片失败，请重试", ex.Message).toJson();
                            }
                        }
                        #endregion
                    }
                    #endregion

                    #region 成员不存在，创建成员
                    if (null == moredianMember)
                    {
                        // 进行创建人员信息
                        IMoredianApiClient client = new DefaultMoredianApiClient(TianChengEntranceSyncService.Config.MoreDian.APIURL);
                        MoredianCreateMemberRequest request = new MoredianCreateMemberRequest
                        {
                            moredianMember = new MoredianMember
                            {
                                mobile = Mobile,
                                memberName = Mobile,
                            },

                            showFace = showFace,
                            verifyFace = showFace,
                        };
                        MoredianCreateMemberResponse response = client.Execute(request, app_token, access_token);
                        if (!response.IsSucc())
                        {
                            return new WxResponse(0, "上传人脸信息失败，请重试", response.Message).toJson();
                        }
                        moredianMember = new MoredianMember
                        {
                            memberId = response.memberId,
                            mobile = Mobile,
                            showFace = Face
                        };
                        erpConn.Execute("INSERT INTO [dbo].[Tb_HSPR_Entrance_Member](Mobile, MemberId, Face) VALUES (@Mobile, @MemberId, @Face)", new { Mobile, Face, MemberId = moredianMember.memberId });
                        moredianMember = MemberRedis.UpdateMoredianMember(Mobile);
                        return new WxResponse(200, "上传人脸信息成功", moredianMember).toJson();
                    }
                    #endregion

                    if (string.IsNullOrEmpty(moredianMember.showFace) && string.IsNullOrEmpty(Face))
                    {
                        return new WxResponse(200, "操作成功", null).toJson();
                    }

                    #region 删除人脸
                    if (string.IsNullOrEmpty(Face))
                    {
                        // 更新显示图片
                        IMoredianApiClient client = new DefaultMoredianApiClient(TianChengEntranceSyncService.Config.MoreDian.APIURL);
                        MoredianDeleteMemberVerifyFaceRequest request = new MoredianDeleteMemberVerifyFaceRequest
                        {
                            memberId = moredianMember.memberId,
                        };
                        MoredianDeleteMemberVerifyFaceResponse response = client.Execute(request, app_token, access_token);
                        if (!response.IsSucc())
                        {
                            return new WxResponse(0, "删除人脸信息失败", response.Message).toJson();
                        }
                        erpConn.Execute("UPDATE Tb_HSPR_Entrance_Member SET Face = @Face WHERE Mobile = @Mobile AND MemberId = @MemberId", new { Mobile, Face, MemberId = moredianMember.memberId });
                        moredianMember = MemberRedis.UpdateMoredianMember(Mobile);
                        return new WxResponse(200, "删除人脸信息成功", moredianMember).toJson();
                    }
                    #endregion

                    
                    #region 更新成员人脸
                    {
                        // 更新显示图片
                        IMoredianApiClient client = new DefaultMoredianApiClient(TianChengEntranceSyncService.Config.MoreDian.APIURL);
                        MoredianUpdateMemberVerifyFaceRequest request = new MoredianUpdateMemberVerifyFaceRequest
                        {
                            memberId = moredianMember.memberId,
                            face = showFace
                        };
                        MoredianUpdateMemberVerifyFaceResponse response = client.Execute(request, app_token, access_token);
                        if (!response.IsSucc())
                        {
                            return new WxResponse(0, "更新人脸信息失败，请重试", response.Message).toJson();
                        }
                    }
                    {
                        // 更新显示图片
                        IMoredianApiClient client = new DefaultMoredianApiClient(TianChengEntranceSyncService.Config.MoreDian.APIURL);
                        MoredianUpdateMemberShowFaceRequest request = new MoredianUpdateMemberShowFaceRequest
                        {
                            memberId = moredianMember.memberId,
                            face = showFace
                        };
                        MoredianUpdateMemberShowFaceResponse response = client.Execute(request, app_token, access_token);
                        // 显示图片不处理失败情况
                    }
                    erpConn.Execute("UPDATE Tb_HSPR_Entrance_Member SET Face = @Face WHERE Mobile = @Mobile AND MemberId = @MemberId", new { Mobile, Face, MemberId = moredianMember.memberId });
                    moredianMember = MemberRedis.UpdateMoredianMember(Mobile);
                    return new WxResponse(200, "更新人脸信息成功", moredianMember).toJson();
                    #endregion
                }
            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                return new WxResponse(0, "响应异常", null).toJson();
            }
        }

        /// <summary>
        /// 获取当前项目设备列表
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string GetRemeteDeviceList(DataRow row)
        {
            try
            {
                #region 获取参数并简单校验
                string Mobile = string.Empty;
                if (row.Table.Columns.Contains("Mobile"))
                {
                    Mobile = row["Mobile"].ToString();
                }
                if (string.IsNullOrEmpty(Mobile))
                {
                    return new WxResponse(0, "用户不存在", null).toJson();
                }
                if (Mobile.Length != 11)
                {
                    return new WxResponse(0, "用户不存在", null).toJson();
                }
                if (!row.Table.Columns.Contains("UserID") || !int.TryParse(row["UserID"].ToString(), out int UserID))
                {
                    return new WxResponse(0, "用户不存在", null).toJson();
                }
                string CommID = string.Empty;
                if (row.Table.Columns.Contains("CommID"))
                {
                    CommID = row["CommID"].ToString();
                }
                if (string.IsNullOrEmpty(CommID))
                {
                    return new WxResponse(0, "请选择默认房屋(1001)", null).toJson();
                }
                DateTime DateNow = DateTime.Now;
                #endregion
                using (IDbConnection conn = new SqlConnection(PubConstant.WChat2020ConnectionString),
                    erpConn = new SqlConnection(erpConnStr))
                {
                    #region 查询用户信息
                    Tb_User tb_User = conn.QueryFirstOrDefault<Tb_User>("SELECT * FROM Tb_User WITH(NOLOCK) WHERE Id = @Id AND Mobile = @Mobile", new { Id = UserID, Mobile });
                    if (null == tb_User)
                    {
                        return new WxResponse(0, "用户不存在", null).toJson();
                    }
                    #endregion
                    List<dynamic> list = erpConn.Query<dynamic>("SELECT DeviceId, DeviceName FROM Tb_HSPR_Entrance_Device WITH(NOLOCK) WHERE CommID = @CommID GROUP BY DeviceId, DeviceName", new { CommID }).ToList();
                    if(null == list)
                    {
                        list = new List<dynamic>();
                    }
                    List<Dictionary<string, string>> resultList = new List<Dictionary<string, string>>();
                    foreach (var item in list)
                    {
                        resultList.Add(new Dictionary<string, string>
                        {
                            { "DeviceId", Convert.ToString(item.DeviceId) },
                            { "DeviceName", Convert.ToString(item.DeviceName) },
                        });
                    }
                    return new WxResponse(200, "获取成功", resultList).toJson();
                }
            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                return new WxResponse(0, "响应异常", null).toJson();
            }
        }

        /// <summary>
        /// 发起远程开门
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string RemoteOpenDoor(DataRow row)
        {
            try
            {
                #region 获取参数并简单校验
                string Mobile = string.Empty;
                if (row.Table.Columns.Contains("Mobile"))
                {
                    Mobile = row["Mobile"].ToString();
                }
                if (string.IsNullOrEmpty(Mobile))
                {
                    return new WxResponse(0, "用户不存在", null).toJson();
                }
                if (Mobile.Length != 11)
                {
                    return new WxResponse(0, "用户不存在", null).toJson();
                }
                if (!row.Table.Columns.Contains("UserID") || !int.TryParse(row["UserID"].ToString(), out int UserID))
                {
                    return new WxResponse(0, "用户不存在", null).toJson();
                }
                string DeviceId = string.Empty;
                if (row.Table.Columns.Contains("DeviceId"))
                {
                    DeviceId = row["DeviceId"].ToString();
                }
                if (string.IsNullOrEmpty(DeviceId))
                {
                    return new WxResponse(0, "请选择要开的门", null).toJson();
                }
                DateTime DateNow = DateTime.Now;
                #endregion
                using (IDbConnection conn = new SqlConnection(PubConstant.WChat2020ConnectionString),
                    erpConn = new SqlConnection(erpConnStr))
                {
                    #region 查询用户信息
                    Tb_User tb_User = conn.QueryFirstOrDefault<Tb_User>("SELECT * FROM Tb_User WITH(NOLOCK) WHERE Id = @Id AND Mobile = @Mobile", new { Id = UserID, Mobile });
                    if (null == tb_User)
                    {
                        return new WxResponse(0, "用户不存在", null).toJson();
                    }
                    #endregion

                    #region 获取AppToken
                    TianChengEntranceSyncService.Config.EntranceConnectionStr = erpConnStr;
                    TianChengEntranceSyncService.Config.WChat2020ConnectionStr = PubConstant.WChat2020ConnectionString;
                    string app_token = AppTokenRedis.GetAppToken();
                    if (string.IsNullOrEmpty(app_token))
                    {
                        return new WxResponse(0, "获取AppToken失败，请联系管理员", null).toJson();
                    }
                    #endregion
                    
                    // 成员信息不存在，进行创建
                    #region 先查询/创建组织机构
                    MoredianOrg moredianOrg = OrgRedis.GetMoredianOrg();
                    if (null == moredianOrg)
                    {
                        return new WxResponse(0, "组织机构配置错误，请联系管理员", null).toJson();
                    }
                    #endregion
                    #region 获取AccessToken
                    string access_token = AccessTokenRedis.GetAccessToken(Convert.ToString(moredianOrg.orgId), moredianOrg.orgAuthKey);
                    if (string.IsNullOrEmpty(access_token))
                    {
                        return new WxResponse(0, "获取AccessToken失败，请联系管理员", null).toJson();
                    }
                    #endregion
                    #region 查询人员信息
                    MoredianMember moredianMember = MemberRedis.GetMoredianMember(Mobile);
                    if(null == moredianMember)
                    {
                        return new WxResponse(0, "请先使用“人脸采集”功能录入人脸信息后再使用远程开门", null).toJson();
                    }
                    long memberId = moredianMember.memberId;
                    string memberName = moredianMember.memberName;
                    if (string.IsNullOrEmpty(memberName))
                    {
                        memberName = moredianMember.mobile;
                    }
                    #endregion
                    #region 查询设备是否存在
                    DeviceId = erpConn.QueryFirstOrDefault<string>("SELECT DeviceId FROM Tb_HSPR_Entrance_Device WITH(NOLOCK) WHERE DeviceId = @DeviceId", new { DeviceId });
                    if (string.IsNullOrEmpty(DeviceId))
                    {
                        return new WxResponse(0, "该设备不存在", null).toJson();
                    }
                    #endregion
                    #region 发起远程开门
                    {
                        IMoredianApiClient client = new DefaultMoredianApiClient(TianChengEntranceSyncService.Config.MoreDian.APIURL);
                        MoredianRemoteOpenDoorRequest request = new MoredianRemoteOpenDoorRequest
                        {
                           deviceId = Convert.ToInt64(DeviceId),
                           memberId = memberId,
                           memberName = memberName
                        };
                        MoredianRemoteOpenDoorResponse response = client.Execute(request, app_token, access_token);
                        if (!response.IsSucc())
                        {
                            return new WxResponse(0, response.Message, response).toJson();
                        }
                        return new WxResponse(200, "操作成功", response).toJson();
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                return new WxResponse(0, "响应异常", null).toJson();
            }
        }
    }
}
