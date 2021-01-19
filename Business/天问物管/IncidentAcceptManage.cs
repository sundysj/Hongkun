using Common;
using Dapper;
using DapperExtensions;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using MobileSoft.Model.HSPR;
using Model.HSPR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Business
{
    /// <summary>
    /// 物管报事
    /// </summary>
    public partial class IncidentAcceptManage : PubInfo
    {
        public IncidentAcceptManage()
        {
            base.Token = "20170123IncidentAcceptManage";
        }

        public override void Operate(ref Transfer Trans)
        {
            DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
            DataRow Row = dAttributeTable.Rows[0];
            //验证登录
            if (!new Login().isLogin(ref Trans))
                return;

            if (Global_Var.LoginCorpID == "1973")
            {
                HK_OperateLog.WriteLog(Trans.Class, Trans.Command);
            }

            switch (Trans.Command)
            {
                // 报事生命周期
                case "GetIncidentLifecycle":
                    Trans.Result = GetIncidentLifecycle(Row);
                    break;

                #region 基础信息

                case "GetRoomLIst"://获取楼栋下的房间
                    Trans.Result = GetRoomLIst(Row);
                    break;
                case "GetBanList"://获取小区内的楼栋
                    Trans.Result = GetBanList(Row);
                    break;
                case "GetIncidentInfoListRegional"://获取公共区域
                    Trans.Result = GetIncidentInfoListRegional(Row);
                    break;
                case "GetIncidentLocation"://获取公区位置
                    Trans.Result = GetIncidentLocation(Row);
                    break;
                case "GetIncidentObject":   // 获取公区对象
                    Trans.Result = GetIncidentObject(Row);
                    break;
                case "GetIncidentType"://获取报事类型
                    Trans.Result = GetIncidentType(Row);
                    break;
                case "GetTopRoomList":
                    Trans.Result = GetTopRoomList(Row);
                    break;
                #endregion

                #region 户内报事
                case "SetIncidentAcceptPhoneInsert"://户内报事
                    Trans.Result = SetIncidentAcceptPhoneInsert(Row);
                    break;
                case "GetIncidentAcceptOwnerInfo"://获取业主或租户信息
                    Trans.Result = GetIncidentAcceptOwnerInfo(Row);
                    break;
                #endregion

                #region 公区报事
                case "SetIncidentAcceptPhoneInsertRegion":
                    Trans.Result = SetIncidentAcceptPhoneInsertRegion(Row);//公区报事
                    break;
                case "SetIncidentAcceptPhoneInsertRegion_hnc":
                    Trans.Result = SetIncidentAcceptPhoneInsertRegion_hnc(Row);//华南城公区报事
                    break;
                #endregion

                #region 未分派工单、可抢工单、全部工单
                case "GetUntreatedIncident"://未分派工单、可抢工单、全部工单
                    Trans.Result = GetUntreatedIncident(Row);
                    break;
                #endregion

                #region 本岗未分派
                case "GetUntreatedDispatching": //本岗未分派
                    //Trans.Result = GetUntreatedDispatching(Row);
                    Trans.Result = GetCanAssignIncidentList(Row);
                    break;
                #endregion

                #region 本人分派
                case "GetMineDispatching": //本人分派
                    Trans.Result = GetMineDispatching(Row);
                    break;
                #endregion

                #region 本岗可分派工单数
                case "GetUntreatedDispatching_Num":
                    //Trans.Result = GetUntreatedDispatching_Num(Row);
                    Trans.Result = GetCanAssignIncidentCount(Row);
                    break;
                #endregion

                #region 本岗可抢工单
                case "GetUntreatedRobWorkOrder"://本岗可抢工单
                    //Trans.Result = GetUntreatedRobWorkOrder(Row);
                    Trans.Result = GetCanSnatchIncidentList(Row);
                    break;
                #endregion

                #region 本岗可抢工单数
                case "GetUntreatedRobWorkOrder_Num":
                    //Trans.Result = GetUntreatedRobWorkOrder_Num(Row);
                    Trans.Result = GetCanSnatchIncidentCount(Row);
                    break;
                #endregion


                #region 本人未完结处理工单
                case "GetUntreatedHandleWorkOrder"://本人处理工单
                    Trans.Result = GetUntreatedHandleWorkOrder(Row);
                    break;
                #endregion

                #region 本岗可处理工单数
                case "GetUntreatedHandleWorkOrder_Num":
                    Trans.Result = GetUntreatedHandleWorkOrder_Num(Row);
                    break;
                #endregion



                #region 获取派工单号
                case "GetIncidentAssigned_GetCoordinateNum_P":
                    Trans.Result = GetIncidentAssigned_GetCoordinateNum_P(Row);
                    break;
                #endregion

                #region 获取协调单号
                case "GetIncidentAssigned_GetCoordinateNum_X":
                    Trans.Result = GetIncidentAssigned_GetCoordinateNum_X(Row);
                    break;
                #endregion

                #region 获取口头派工单号
                case "GetIncidentAssigned_GetCoordinateNum_K":
                    Trans.Result = GetIncidentAssigned_GetCoordinateNum_K(Row);
                    break;
                #endregion

                #region 派工协调单权限
                case "GetHSPR_AuditingSet":
                    Trans.Result = GetHSPR_AuditingSet(Row);
                    break;
                #endregion

                #region 报事分派获取处理人 
                case "GetIncidentDealMan_Assign"://报事分派获取处理人 
                    Trans.Result = GetIncidentDealMan_Assign(Row);
                    break;
                #endregion

                #region 报事详情               
                case "GetUntreatedIncidentView"://报事详情
                    Trans.Result = GetUntreatedIncidentView(Row);
                    break;
                #endregion

                #region 抢单、派单权限判断
                case "CheckIncidentID": // 是否具有派单权限
                    Trans.Result = CheckIncidentID(Row);
                    break;
                case "CheckUntreatedRobWorkOrder":
                    Trans.Result = CheckUntreatedRobWorkOrder(Row);
                    break;
                case "CanAssign":
                    Trans.Result = CanAssign(Row);
                    break;
                case "CanSnatch":
                    Trans.Result = CanSnatch(Row);
                    break;
                #endregion

                case "GetIncidentDealMan"://获取处理人
                    Trans.Result = GetIncidentDealMan(Row);
                    break;

                #region 报事分派

                case "IncidentAssignment"://报事分派
                    Trans.Result = IncidentAssignment(Row);
                    break;
                #endregion

                #region 报事跟进
                case "IncidentCoordinate"://报事跟进
                    Trans.Result = IncidentCoordinate(Row);
                    break;
                case "GetIncidentCoordinateType"://报事跟进类型
                    Trans.Result = GetIncidentCoordinateType(Row);
                    break;
                case "GetIncidentCoordinateUnCompleteReason"://报事跟进未完成原因列表
                    Trans.Result = GetIncidentCoordinateUnCompleteReason(Row);
                    break;
                #endregion

                #region 报事处理
                case "IncidentProcessing"://报事处理
                    Trans.Result = IncidentProcessing(Row);
                    break;
                case "IncidentProcessingReceivingDate"://报事处理接单时间
                    Trans.Result = IncidentProcessingReceivingDate(Row);
                    break;
                case "IncidentProcessingPlanArriveDate"://预计到场时间
                    Trans.Result = IncidentProcessingPlanArriveDate(Row);
                    break;
                case "IncidentProcessingArriveDate"://报事处理到场时间
                    Trans.Result = IncidentProcessingArriveDate(Row);
                    break;
                case "GetIncidentServiceList"://获取报事服务费用列表
                    Trans.Result = GetIncidentServiceList(Row);
                    break;
                case "GetIncidentServiceCostItemList"://获取报事特约服务费用项目列表
                    Trans.Result = GetIncidentServiceCostItemList(Row);
                    break;
                case "GetIncidentServiceCostStandardSetting"://该房屋报事特约服务费用项目是否绑定了收费标准
                    Trans.Result = GetIncidentServiceCostStandardSetting(Row);
                    break;
                case "GetIncidentServiceCostItemStandardList":// 获取报事特约服务费用项目收费标准列表
                    Trans.Result = GetIncidentServiceCostItemStandardList(Row);
                    break;
                case "CalcIncidentServiceCostBeginDateAndEndDate": // 获取报事服务费用开始日期和费用结束日期
                    Trans.Result = CalcIncidentServiceCostBeginDateAndEndDate(Row);
                    break;
                case "IncidentServiceCalcAmount": // 报事服务计算收费金额
                    Trans.Result = IncidentServiceCalcAmount(Row);
                    break;
                case "IncidentServiceSave": // 保存报事服务
                    Trans.Result = IncidentServiceSave(Row);
                    break;
                case "DeleteIncidentFees": // 删除报事费用
                    Trans.Result = DeleteIncidentFees(Row);
                    break;

                #endregion

                case "SaveSignatoryImg":
                    Trans.Result = SaveSignatoryImg(Row);
                    break;

                #region 报事工时信息获取
                case "GetIncidentAcceptType"://报事工时信息获取
                    Trans.Result = GetIncidentAcceptType(Row);
                    break;
                #endregion

                #region 报事工时信息登记
                case "SetIncidentAcceptType":
                    Trans.Result = SetIncidentAcceptType(Row);
                    break;
                #endregion

                #region 本岗报事处理按钮权限
                case "CheckUntreatedHandleWorkOrder"://本岗报事处理按钮权限
                    Trans.Result = CheckUntreatedHandleWorkOrder(Row);
                    break;
                #endregion

                #region 模糊查询所有处理人
                case "QueryProcessPostUserAll":
                    Trans.Result = QueryProcessPostUserAll(Row);
                    break;

                #endregion

                #region 模糊查询派工单/协调单号
                case "QueryCoordinateNum":
                    Trans.Result = QueryCoordinateNum(Row);
                    break;
                #endregion

                #region 报事投诉查询
                case "QueryComplaintList":
                    Trans.Result = QueryComplaintList(Row);
                    break;
                #endregion

                case "QueryMaterial"://模糊查询物资
                    Trans.Result = QueryMaterial(Row);
                    break;
                case "QueryWareHouse"://模糊查询仓库
                    Trans.Result = QueryWareHouse(Row);
                    break;
                case "GetIncidentMaterialList_hnc"://获取报事领料列表_华南城
                    Trans.Result = GetIncidentMaterialList_hnc(Row);
                    break;
                case "GetIncidentMaterialWareHouseList_hnc"://获取报事领料仓库列表_华南城
                    Trans.Result = GetIncidentMaterialWareHouseList_hnc(Row);
                    break;
                case "GetIncidentMaterialTypeList_hnc"://获取报事领料物资类别列表_华南城
                    Trans.Result = GetIncidentMaterialTypeList_hnc(Row);
                    break;
                case "GetIncidentMaterialCheckOutList_hnc"://获取报事领料物资出库列表_华南城
                    Trans.Result = GetIncidentMaterialCheckOutList_hnc(Row);
                    break;
                case "GetIncidentMaterialFlowSortList_hnc"://获取报事领料物资出库审批流程列表_华南城
                    Trans.Result = GetIncidentMaterialFlowSortList_hnc(Row);
                    break;
                case "GetIncidentMaterialCheckOutUseList_hnc"://获取报事领料物资出库用途列表_华南城
                    Trans.Result = GetIncidentMaterialCheckOutUseList_hnc(Row);
                    break;
                case "GetIncidentMaterialCheckOutFeesBelongList_hnc"://获取报事领料物资费用归属列表_华南城
                    Trans.Result = GetIncidentMaterialCheckOutFeesBelongList_hnc(Row);
                    break;
                case "SaveIncidentMaterialCheckOut_hnc"://保存物资出库单_华南城
                    Trans.Result = SaveIncidentMaterialCheckOut_hnc(Row);
                    break;
                case "GetIncidentWarningCount": // 报事预警数量
                    Trans.Result = GetIncidentWarningCount(Row);
                    break;
                case "GetIncidentWarningList": // 报事预警列表
                    Trans.Result = GetIncidentWarningList(Row);
                    break;

                case "DelayApply":              // 需求4658平安报事处理延期
                    Trans.Result = DelayApply(Row);
                    break;
                case "IncidentTransmit":        // 需求4489平安报事转发
                    Trans.Result = IncidentTransmit(Row);
                    break;
                case "IncidentTransmitHistory": // 需求4489平安报事转发
                    Trans.Result = IncidentTransmitHistory(Row);
                    break;

                default:
                    Trans.Result = JSONHelper.FromString(false, "接口不存在");
                    break;
            }

        }



        /// <summary>
        /// 报事生命周期
        /// </summary>
        private string GetIncidentLifecycle(DataRow row)
        {
            if (!row.Table.Columns.Contains("IncidentID") || string.IsNullOrEmpty(row["IncidentID"].ToString()))
            {
                return JSONHelper.FromString(false, "报事编号不能为空");
            }
            string IncidentID = row["IncidentID"].ToString();

            // 是否需要加载回访信息
            int loadReply = 0;
            if (row.Table.Columns.Contains("LoadReply"))
            {
                loadReply = AppGlobal.StrToInt(row["LoadReply"].ToString());
            }

            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                dynamic incidentInfo = conn.Query(@"SELECT * FROM view_HSPR_IncidentAccept_Filter WHERE IncidentID=@IncidentID",
                    new { IncidentID = IncidentID }).FirstOrDefault();

                List<object> list = new List<object>();

                string incidentMode = incidentInfo.IncidentMode.ToString();
                if (!incidentMode.EndsWith("报事"))
                {
                    incidentMode += "报事";
                }

                // 报事人手机号码处理
                var incidentManMobileList = new List<object>();
                if (!string.IsNullOrEmpty(incidentInfo.Phone))
                {
                    if (incidentInfo.Phone.Contains(",") || incidentInfo.Phone.Contains("|") || incidentInfo.Phone.Contains("&") || incidentInfo.Phone.Contains("/") || incidentInfo.Phone.Contains("\\") ||
                        incidentInfo.Phone.Contains("，") || incidentInfo.Phone.Contains("、") || incidentInfo.Phone.Contains(";") || incidentInfo.Phone.Contains("；") || incidentInfo.Phone.Contains(" "))
                    {
                        var array = incidentInfo.Phone.Split(new char[] { ',', '，', '|', '、', '&', '；', ';', '/', '\\', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var item in array)
                        {
                            incidentManMobileList.Add(new { UserName = incidentInfo.IncidentMan, Mobile = item });
                        }
                    }

                    else
                    {
                        incidentManMobileList.Add(new { UserName = incidentInfo.IncidentMan, Mobile = incidentInfo.Phone });
                    }
                }

                // 创建任务
                list.Add(new
                {
                    Title = "创建任务",
                    MainInfo = $@"报事人：{incidentInfo.IncidentMan}，报事方式：{incidentMode}",
                    SubInfo = $"报事内容：{incidentInfo.IncidentContent}",
                    SubInfoHighlight = false,
                    DateTitle = $@"报事时间：{incidentInfo.IncidentDate}",
                    Mobile = incidentManMobileList
                });


                var dealManMobileList = new List<object>();

                // 已分派
                if (incidentInfo.DispType != null && incidentInfo.DispType.ToString() == "1")
                {
                    // 查询分派人手机号
                    var dispManMobile = conn.Query<string>(@"SELECT isnull(MobileTel,LinkmanTel) FROM tb_Sys_User 
                                                            WHERE isnull(IsDelete,0)=0 AND UserCode=@UserCode",
                                                            new { UserCode = incidentInfo.DispUserCode }).FirstOrDefault();

                    var dispManMobileList = new List<object>();

                    // 分派人手机号
                    if (!string.IsNullOrEmpty(dispManMobile))
                    {
                        if (dispManMobile.Contains(",") || dispManMobile.Contains("|") || dispManMobile.Contains("&") || dispManMobile.Contains("/") || dispManMobile.Contains("\\") ||
                            dispManMobile.Contains("，") || dispManMobile.Contains("、") || dispManMobile.Contains(";") || dispManMobile.Contains("；") || dispManMobile.Contains(" "))
                        {
                            var array = dispManMobile.Split(new char[] { ',', '，', '|', '、', '&', '；', ';', '/', '\\', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (var item in array)
                            {
                                dispManMobileList.Add(new { UserName = incidentInfo.DispMan, Mobile = item });
                            }
                        }
                        else
                        {
                            dispManMobileList.Add(new { UserName = incidentInfo.DispMan, Mobile = dispManMobile });
                        }
                    }

                    // 查询处理人
                    var dealManUserCode = conn.Query<string>(@"SELECT stuff((SELECT ','+UserCode FROM Tb_HSPR_IncidentAcceptDeal WHERE IncidentID=@IncidentID 
                                                            FOR XML Path('')),1,1,'')", new { IncidentID = IncidentID }).FirstOrDefault();

                    // 查询处理人信息
                    var dealManInfo = conn.Query(@"SELECT UserName,ltrim(rtrim(isnull(MobileTel,LinkmanTel))) AS MobileTel FROM Tb_Sys_User 
                                                    WHERE ltrim(rtrim(isnull(isnull(MobileTel,LinkmanTel),'')))<>''
                                                    AND UserCode IN(SELECT UserCode FROM Tb_HSPR_IncidentAcceptDeal WHERE IncidentID=@IncidentID)",
                                                    new { IncidentID = IncidentID });

                    foreach (var item in dealManInfo)
                    {
                        string userName = item.UserName.ToString();
                        string dealManMobile = item.MobileTel.ToString();
                        if (true)
                        {
                            if (dealManMobile.Contains(",") || dealManMobile.Contains("|") || dealManMobile.Contains("&") || dealManMobile.Contains("/") || dealManMobile.Contains("\\") ||
                                dealManMobile.Contains("，") || dealManMobile.Contains("、") || dealManMobile.Contains(";") || dealManMobile.Contains("；") || dealManMobile.Contains(" "))
                            {
                                var array = dealManMobile.Split(new char[] { ',', '，', '|', '、', '&', '；', ';', '/', '\\', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                foreach (var _m in array)
                                {
                                    dealManMobileList.Add(new { UserName = userName, Mobile = _m });
                                }
                            }
                            else
                            {
                                dealManMobileList.Add(new { UserName = userName, Mobile = dealManMobile });
                            }
                        }
                    }


                    // 是否分派超时
                    DateTime startTime = Convert.ToDateTime(incidentInfo.IncidentDate);
                    DateTime dispTime = Convert.ToDateTime(incidentInfo.DispDate ?? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    TimeSpan timeSpan = dispTime - startTime;
                    decimal dispLimit = Convert.ToDecimal(incidentInfo.DispLimit) ?? 0;
                    bool isOver = (timeSpan.TotalMilliseconds > Convert.ToDouble(dispLimit * 60 * 60 * 1000));
                    bool isSnatch = (dealManUserCode != null && incidentInfo.DispUserCode == dealManUserCode);

                    // 鸿坤暂时不统计超时
                    if (Global_Var.LoginCorpID == "1973")
                    {
                        list.Add(new
                        {
                            Title = "分派任务",
                            MainInfo = (isSnatch ? $@"“{incidentInfo.DispMan}”抢单成功" : $@"派工人“{incidentInfo.DispMan}”分派给了“{incidentInfo.DealMan}”"),
                            SubInfo = "",
                            SubInfoHighlight = false,
                            DateTitle = $@"{(isSnatch ? "抢单" : "分派")}时间：{incidentInfo.DispDate}",
                            Mobile = dispManMobileList
                        });
                    }
                    else
                    {
                        list.Add(new
                        {
                            Title = (isOver ? "分派超时" : "分派任务"),
                            MainInfo = (isSnatch ? $@"“{incidentInfo.DispMan}”抢单成功" : $@"派工人“{incidentInfo.DispMan}”分派给了“{incidentInfo.DealMan}”"),
                            SubInfo = (isOver ? $@"分派超时{timeSpan.Days}天{timeSpan.Hours}时{timeSpan.Minutes}分" : ""),
                            SubInfoHighlight = isOver,
                            DateTitle = $@"{(isSnatch ? "抢单" : "分派")}时间：{incidentInfo.DispDate}",
                            Mobile = dispManMobileList
                        });
                    }
                }

                // 接单
                if (incidentInfo.ReceivingDate != null && !string.IsNullOrEmpty(incidentInfo.ReceivingDate.ToString()))
                {
                    list.Add(new
                    {
                        Title = "确认接单",
                        MainInfo = $@"“{incidentInfo.DealMan}”准备处理",
                        SubInfo = "",
                        SubInfoHighlight = false,
                        DateTitle = $@"接单时间：{incidentInfo.ReceivingDate}",
                        Mobile = dealManMobileList
                    });
                }

                // 到场
                if (incidentInfo.ArriveData != null && !string.IsNullOrEmpty(incidentInfo.ArriveData.ToString()))
                {
                    list.Add(new
                    {
                        Title = "确认到场",
                        MainInfo = $@"“{incidentInfo.DealMan}”开始处理",
                        SubInfo = "",
                        SubInfoHighlight = false,
                        DateTitle = $@"到场时间：{incidentInfo.ArriveData}",
                        Mobile = dealManMobileList
                    });
                }

                // 跟进
                var follows = conn.Query(@"SELECT * FROM Tb_HSPR_IncidentCoordinate WHERE isnull(IsDelete,0)=0 AND IncidentID=@IncidentID
                                            ORDER BY CoordinateDate", new { IncidentID = IncidentID });
                if (follows.Count() > 0)
                {
                    foreach (var item in follows)
                    {
                        list.Add(new
                        {
                            Title = "处理跟进",
                            MainInfo = $@"“{item.CoordinateMan}”添加跟进信息",
                            SubInfo = $@"跟进内容：{item.CoordinateContent.ToString()}",
                            SubInfoHighlight = false,
                            DateTitle = $@"跟进时间：{incidentInfo.ArriveData}",
                            Mobile = dealManMobileList
                        });
                    }
                }

                // 已分派未处理
                if (incidentInfo.DealState == null || string.IsNullOrEmpty(incidentInfo.DealState.ToString()) || incidentInfo.DealState.ToString() == "0")
                {
                    if (incidentInfo.DispType != null && incidentInfo.DispType.ToString() == "1")
                    {
                        // 判断是否处理逾期
                        DateTime startTime = Convert.ToDateTime(incidentInfo.ReserveDate ?? incidentInfo.DispDate);
                        TimeSpan timeSpan = DateTime.Now - startTime;
                        decimal dealLimit = Convert.ToDecimal(incidentInfo.DealLimit) ?? 0;
                        bool isOver = (timeSpan.TotalMilliseconds > Convert.ToDouble(dealLimit * 60 * 60 * 1000));

                        list.Add(new
                        {
                            Title = (isOver ? "处理超时" : "任务处理中"),
                            MainInfo = $@"处理人“{incidentInfo.DealMan}”正在处理",
                            SubInfo = (isOver ? $@"已超时{timeSpan.Days}天{timeSpan.Hours}时{timeSpan.Minutes}分" : ""),
                            SubInfoHighlight = isOver,
                            DateTitle = "",
                            Mobile = dealManMobileList
                        });
                    }
                }
                else
                {
                    if (incidentInfo.DealState.ToString() == "1")
                    {
                        list.Add(new
                        {
                            Title = "处理完毕",
                            MainInfo = $@"处理人“{incidentInfo.DealMan}”处理完毕",
                            SubInfo = $"处理情况：{(incidentInfo.DealSituation == null ? "无" : incidentInfo.DealSituation.ToString())}",
                            SubInfoHighlight = false,
                            DateTitle = $@"完结时间：{incidentInfo.MainEndDate}",
                            Mobile = dealManMobileList
                        });
                    }
                }

                // 回访
                if (loadReply != 0 && incidentInfo.DealState != null && incidentInfo.DealState.ToString() == "1")
                {
                    // 判断是否回访超时
                    var replys = conn.Query(@"SELECT * FROM Tb_HSPR_IncidentReply WHERE isnull(IsDelete,0)=0 AND IncidentID=@IncidentID 
                                                ORDER BY ReplyDate", new { IncidentID = IncidentID });

                    foreach (var item in replys)
                    {
                        list.Add(new
                        {
                            Title = item.ReplyWay,
                            MainInfo = $@"回访人“{item.ReplyMan ?? item.RespondentsMan}”，{(item.ReplyResult.ToString() == "0" ? "成功" : "不成功")}回访",
                            SubInfo = $"服务评价：{item.ServiceQuality}",
                            SubInfoHighlight = false,
                            DateTitle = $@"回访时间：{item.ReplyDate}",
                            Mobile = dealManMobileList
                        });
                    }
                }

                return new ApiResult(true, list).toJson();
            }
        }

        /// <summary>
        /// 查询报事跟进类型
        /// </summary>
        /// <param name="Row"></param>
        /// <returns></returns>
        private string GetIncidentCoordinateType(DataRow Row)
        {
            if (!Row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(Row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            string CommID = Row["CommID"].ToString();

            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            #region 查询报事跟进类型
            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                DataTable dt = conn.ExecuteReader("Proc_Dictionary_Sort_Filter", new { SQLEx = "", DictionarySort = "IncidentFollowUp" }, null, null, CommandType.StoredProcedure).ToDataSet().Tables[0];
                if (null != dt && dt.Rows.Count > 0)
                {
                    Dictionary<string, object> dic;
                    foreach (DataRow item in dt.Rows)
                    {
                        dic = new Dictionary<string, object>
                        {
                            { "DictionaryCode", item["DictionaryCode"] },
                            { "DictionaryName", item["DictionaryName"] }
                        };
                        list.Add(dic);
                    }
                }
            }
            #endregion
            return new ApiResult(true, list).toJson();
        }
        /// <summary>
        /// 查询报事跟进未完成原因
        /// </summary>
        /// <param name="Row"></param>
        /// <returns></returns>
        private string GetIncidentCoordinateUnCompleteReason(DataRow Row)
        {
            if (!Row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(Row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            string CommID = Row["CommID"].ToString();

            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            #region 查询报事跟进未完成原因
            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                DataTable dt = conn.ExecuteReader("Proc_Dictionary_Sort_Filter", new { SQLEx = "", DictionarySort = "IncidentReason" }, null, null, CommandType.StoredProcedure).ToDataSet().Tables[0];
                if (null != dt && dt.Rows.Count > 0)
                {
                    Dictionary<string, object> dic;
                    foreach (DataRow item in dt.Rows)
                    {
                        dic = new Dictionary<string, object>
                        {
                            { "DictionaryCode", item["DictionaryCode"] },
                            { "DictionaryName", item["DictionaryName"] }
                        };
                        list.Add(dic);
                    }
                }
            }
            #endregion
            return new ApiResult(true, list).toJson();
        }
        private string SaveIncidentMaterialCheckOut_hnc(DataRow Row)
        {

            #region 基础参数校验
            if (!Row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(Row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            string CommID = Row["CommID"].ToString();

            if (!Row.Table.Columns.Contains("WareHouseId") || string.IsNullOrEmpty(Row["WareHouseId"].ToString()))
            {
                return JSONHelper.FromString(false, "仓库ID不能为空");
            }
            string WareHouseId = Row["WareHouseId"].ToString();

            if (!Row.Table.Columns.Contains("CheckOutType") || string.IsNullOrEmpty(Row["CheckOutType"].ToString()))
            {
                return JSONHelper.FromString(false, "出库类型不能为空");
            }
            string CheckOutType = Row["CheckOutType"].ToString();


            string Memo = "";
            if (Row.Table.Columns.Contains("Memo") && !string.IsNullOrEmpty(Row["Memo"].ToString()))
            {
                Memo = Row["Memo"].ToString();
            }
            if (!Row.Table.Columns.Contains("IncidentNum") || string.IsNullOrEmpty(Row["IncidentNum"].ToString()))
            {
                return JSONHelper.FromString(false, "报事编号不能为空");
            }
            string IncidentNum = Row["IncidentNum"].ToString();

            string UseDepName = "";
            if (Row.Table.Columns.Contains("UseDepName") && !string.IsNullOrEmpty(Row["UseDepName"].ToString()))
            {
                UseDepName = Row["UseDepName"].ToString();
            }

            string UseUserName = "";
            if (Row.Table.Columns.Contains("UseUserName") && !string.IsNullOrEmpty(Row["UseUserName"].ToString()))
            {
                UseUserName = Row["UseUserName"].ToString();
            }

            if (!Row.Table.Columns.Contains("Purpose") || string.IsNullOrEmpty(Row["Purpose"].ToString()))
            {
                return JSONHelper.FromString(false, "领料用途不能为空");
            }
            string Purpose = Row["Purpose"].ToString();

            string UserReason = "";
            if (Row.Table.Columns.Contains("UserReason") && !string.IsNullOrEmpty(Row["UserReason"].ToString()))
            {
                UserReason = Row["UserReason"].ToString();
            }

            string UsePosition = "";
            if (Row.Table.Columns.Contains("UsePosition") && !string.IsNullOrEmpty(Row["UsePosition"].ToString()))
            {
                UserReason = Row["UsePosition"].ToString();
            }

            // 只有华南城才有费用归属
            string FeesBelong = null;
            if (Global_Var.LoginCorpID == "1975")
            {
                if (!Row.Table.Columns.Contains("FeesBelong") || string.IsNullOrEmpty(Row["FeesBelong"].ToString()))
                {
                    return JSONHelper.FromString(false, "费用归属不能为空");
                }
                FeesBelong = Row["FeesBelong"].ToString();
            }

            if (Row.Table.Columns.Contains("DutyPerson") && !string.IsNullOrEmpty(Row["DutyPerson"].ToString()))
            {
                UserReason = Row["DutyPerson"].ToString();
            }

            if (!Row.Table.Columns.Contains("IncidentID") || string.IsNullOrEmpty(Row["IncidentID"].ToString()))
            {
                return JSONHelper.FromString(false, "报事ID不能为空");
            }
            string IncidentID = Row["IncidentID"].ToString();

            //if (!Row.Table.Columns.Contains("WorkFlowTypeId") || string.IsNullOrEmpty(Row["WorkFlowTypeId"].ToString()))
            //{
            //    return JSONHelper.FromString(false, "审批流程不能为空");
            //}
            //string WorkFlowTypeId = Row["WorkFlowTypeId"].ToString();

            string CheckOutId = Guid.NewGuid().ToString();

            if (!Row.Table.Columns.Contains("CheckOutDetail") || string.IsNullOrEmpty(Row["CheckOutDetail"].ToString()))
            {
                return JSONHelper.FromString(false, "出库物料不能为空(1)");
            }
            DataTable CheckOutDetailDataTable = JSONHelper.JsonToDataTable(Row["CheckOutDetail"].ToString());
            if (null == CheckOutDetailDataTable || CheckOutDetailDataTable.Rows.Count == 0)
            {
                return JSONHelper.FromString(false, "出库物料不能为空(2)");
            }

            DateTime ReceiptDate = DateTime.Now;
            #endregion

            #region 新增出库单
            // 获取入库单号
            string ReceiptSign;
            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                ReceiptSign = conn.Query<string>("Proc_Mt_GetReceiptSign", new { @ReceiptHead = "CK" }, null, true, null, CommandType.StoredProcedure).FirstOrDefault();
            }
            if (string.IsNullOrEmpty(ReceiptSign))
            {
                return new ApiResult(false, "生成出库单号失败,请重试").toJson();
            }

            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                string sqlStr = $@"INSERT INTO Tb_Mt_CheckOut([Id], [WareHouseId], [CheckOutType], [ReceiptSign], [ReceiptDate], 
                                    [DepCode], [CheckOutor], [Originator], [IncidentNum], [Memo], [AllocationId], [InventoryId], 
                                    [IsCancel], [CancelDate], [State], [IncidentID], [UseDepName], [UseUserName], [Purpose], 
                                    [UserReason], [UsePosition]) 
                                VALUES (@Id, @WareHouseId, @CheckOutType, @ReceiptSign, @ReceiptDate, @DepCode, @CheckOutor, 
                                    @Originator, @IncidentNum, @Memo, @AllocationId, @InventoryId, @IsCancel, @CancelDate, 
                                    @State, @IncidentID, @UseDepName, @UseUserName, @Purpose, @UserReason, @UsePosition);";

                // 只有华南城才有费用归属及责任人
                if (Global_Var.LoginCorpID == "1975")
                {
                    sqlStr = $@"INSERT INTO Tb_Mt_CheckOut([Id], [WareHouseId], [CheckOutType], [ReceiptSign], [ReceiptDate], 
                                    [DepCode], [CheckOutor], [Originator], [IncidentNum], [Memo], [AllocationId], [InventoryId], 
                                    [IsCancel], [CancelDate], [State], [IncidentID], [UseDepName], [UseUserName], [Purpose], 
                                    [UserReason], [UsePosition], [FeesBelong], [DutyPerson]) 
                                VALUES (@Id, @WareHouseId, @CheckOutType, @ReceiptSign, @ReceiptDate, @DepCode, @CheckOutor, 
                                    @Originator, @IncidentNum, @Memo, @AllocationId, @InventoryId, @IsCancel, @CancelDate, 
                                    @State, @IncidentID, @UseDepName, @UseUserName, @Purpose, @UserReason, @UsePosition, 
                                    @FeesBelong, @DutyPerson);";
                }

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Id", CheckOutId);
                parameters.Add("WareHouseId", WareHouseId);
                parameters.Add("CheckOutType", CheckOutType);
                parameters.Add("ReceiptSign", ReceiptSign);
                parameters.Add("ReceiptDate", ReceiptDate);
                parameters.Add("DepCode", "");
                parameters.Add("CheckOutor", "");
                parameters.Add("Originator", Global_Var.LoginUserCode);
                parameters.Add("IncidentNum", IncidentNum);
                parameters.Add("Memo", Memo);
                parameters.Add("AllocationId", "");
                parameters.Add("InventoryId", "");
                parameters.Add("IsCancel", "");
                parameters.Add("CancelDate", "");
                parameters.Add("State", "未启动");
                parameters.Add("IncidentID", IncidentID);
                parameters.Add("UseDepName", UseDepName);
                parameters.Add("UseUserName", UseUserName);
                parameters.Add("Purpose", Purpose);
                parameters.Add("UserReason", UserReason);
                parameters.Add("UsePosition", UsePosition);

                // 只有华南城才有费用归属及责任人
                if (Global_Var.LoginCorpID == "1975")
                {
                    parameters.Add("DutyPerson", null);
                    parameters.Add("FeesBelong", FeesBelong);
                }
                int count = conn.Execute(sqlStr, parameters, null, null, CommandType.Text);
                if (count == 0)
                {
                    return new ApiResult(false, "新增出库单失败,请重试").toJson();
                }
            }
            #endregion

            #region 保存物料明细
            #region 删除此盘点单已有的明细
            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                // 乐观模式,不考虑删除失败的情况
                conn.Execute("DELETE Tb_Mt_CheckOutDetail WHERE CheckOutId = @CheckOutId", new { CheckOutId = CheckOutId }, null, null, CommandType.Text);
            }
            #endregion

            #region 保存出库物料明细
            {
                List<DynamicParameters> parameterList = new List<DynamicParameters>();
                double useCount;
                DynamicParameters parameters;
                foreach (DataRow item in CheckOutDetailDataTable.Rows)
                {
                    try
                    {
                        useCount = Double.Parse(item["Count"].ToString());
                    }
                    catch (Exception)
                    {
                        useCount = 0d;
                    }
                    // 获取出库Sql参数
                    parameters = createCheckOutDetailParameter(item["Id"].ToString(), CheckOutId, useCount, Purpose, UseDepName, UseUserName, UsePosition, Memo);
                    if (null != parameters)
                    {
                        parameterList.Add(parameters);
                    }
                }
                // 如果构建的参数数量和添加的物资数量不一致
                // 代表某种物资库存不足或者无法出库
                // 返回失败
                if (parameterList.Count != CheckOutDetailDataTable.Rows.Count)
                {
                    return new ApiResult(false, "有物资由于库存不足或其他原因无法出库,请重试").toJson();
                }
                string sql = "INSERT INTO [Tb_Mt_CheckOutDetail]([Id], [CheckOutId], [MaterialId], [Price], [Quantity], [Amount], [StorageDetailId], [Purpose], [UseDepName], [UseUserName], [UsePosition], [Memo], [CostCode]) VALUES (@Id, @CheckOutId, @MaterialId, @Price, @Quantity, @Amount, @StorageDetailId, @Purpose, @UseDepName, @UseUserName, @UsePosition, @Memo, @CostCode)";
                foreach (DynamicParameters item in parameterList)
                {
                    using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
                    {
                        // 乐观模式,不考虑Sql添加失败的原因,后续根据需求调整
                        conn.Execute(sql, item, null, null, CommandType.Text);
                    }
                }
            }

            #endregion
            #endregion

            string InstanceId;
            string BussId = CheckOutId;
            #region 保存业务审批单
            //using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString)) {
            //    DynamicParameters parameters = new DynamicParameters();
            //    parameters.Add("Id", DBNull.Value, DbType.String, ParameterDirection.Output);
            //    parameters.Add("BType", "物资出库");
            //    parameters.Add("BussId", BussId);
            //    parameters.Add("ClassName", "CheckOutCallBack");
            //    parameters.Add("DocumentUrl", DBNull.Value);
            //    parameters.Add("IsDelete", DBNull.Value);
            //    parameters.Add("Memo", "");
            //    parameters.Add("Description", DBNull.Value);
            //    parameters.Add("OriginatorCode", Global_Var.LoginUserCode);
            //    parameters.Add("Tb_WorkFlow_FlowSort_InfoId", AppGlobal.StrToInt(WorkFlowTypeId));
            //    parameters.Add("TitleName", ReceiptDate.ToString() + "物资出库(" + ReceiptSign + ")");
            //    parameters.Add("UserCode", Global_Var.LoginUserCode);
            //    parameters.Add("WorkStartDate", DateTime.Now);
            //    conn.Execute("Proc_Tb_WorkFlow_BussList_ADD", parameters, null, null, CommandType.StoredProcedure);
            //    InstanceId = parameters.Get<string>("Id");
            //}
            #endregion

            #region 发起审批单 保存工作流(APP暂时不发起审批流程)
            //// 删除原始数据,有问题也可以由发起人重新发起重跑
            //using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString)) {
            //    conn.ExecuteReader("Proc_WorkFlow_InitSaveWorkFlowData", new { InstanceId = Global_Fun.StrToInt(InstanceId), Tb_Dictionary_InstanceType_DictionaryCode = "0015" }, null, null, CommandType.StoredProcedure);
            //}

            ////乐观模式,不考虑获取返回ID不存在的问题
            //string InstanceInfoId = "";
            //// 更新工作流程实例
            //using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            //{
            //    DynamicParameters parameters = new DynamicParameters();
            //    parameters.Add("InstanceId", Global_Fun.StrToInt(InstanceId));
            //    parameters.Add("Tb_Dictionary_InstanceType_DictionaryCode", "0015");
            //    parameters.Add("InstanceMark", DBNull.Value);
            //    parameters.Add("NoticeMsg", 0);
            //    parameters.Add("NoticeMail", 0);
            //    parameters.Add("NoticePhone", 0);
            //    parameters.Add("NoticeHaveDeal", 0);
            //    parameters.Add("NoticeStartDeal", 0);
            //    parameters.Add("NoticeOtherUser", 0);
            //    parameters.Add("IsComplete", 0);
            //    parameters.Add("IsSuccess", 0);
            //    parameters.Add("CompleteDay", DBNull.Value);
            //    DataTable dt = conn.ExecuteReader("Proc_WorkFlow_Instance_Insert", parameters, null, null, CommandType.StoredProcedure).ToDataSet().Tables[0];
            //    if (null != dt && dt.Rows.Count > 0) {
            //        InstanceInfoId = dt.Rows[0]["IDENTITY"].ToString();
            //    }
            //}

            //// 更新工作流程节点
            ////乐观模式,不考虑获取返回ID不存在的问题
            //string InfoId = "";
            //using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            //{

            //    DynamicParameters parameters = new DynamicParameters();
            //    parameters.Add("Tb_WorkFlow_Instance_InfoId", Global_Fun.StrToInt(InstanceInfoId));
            //    parameters.Add("FlowNodeName", "送审");
            //    parameters.Add("FlowSort", 1);
            //    parameters.Add("TimeOutDay", 0);
            //    parameters.Add("TimeOutDays", 0);
            //    parameters.Add("Tb_Dictionary_NodeOprMethod_DictionaryCode", "0001");
            //    parameters.Add("Tb_Dictionary_NodeOprType_DictionaryCode", "0002");
            //    parameters.Add("JumpFlowSort", 0);
            //    parameters.Add("IsUpdateFlow", 0);
            //    parameters.Add("Tb_Dictionary_OprState_DictionaryCode", "0001");
            //    parameters.Add("IsPrint", 1);
            //    parameters.Add("IsStartUser", 0);
            //    parameters.Add("WorkFlowStartDate", DBNull.Value);
            //    parameters.Add("ReturnNode", 0);
            //    parameters.Add("CheckLevel", 2);
            //    DataTable dt = conn.ExecuteReader("Proc_WorkFlow_FlowNode_Insert", parameters, null, null, CommandType.StoredProcedure).ToDataSet().Tables[0];
            //    if (null != dt && dt.Rows.Count > 0)
            //    {
            //        InfoId = dt.Rows[0]["IDENTITY"].ToString();
            //    }
            //}

            //// 更新工作流程节点部门

            #endregion

            return new ApiResult(true, "完成").toJson();
        }

        #region 插入物资详情
        /// <summary>
        /// 获取物资详情
        /// </summary>
        /// <param name="Id">物资ID</param>
        /// <returns></returns>
        private MaterialInfo getMaterialInfo(string Id)
        {
            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                return conn.Query<MaterialInfo>("SELECT * FROM View_Tb_Mt_MaterialCheckOut1001_Filter WHERE Id = @Id", new { Id = Id }, null, true, null, CommandType.Text).FirstOrDefault();
            }
        }
        /// <summary>
        /// 物资详情
        /// </summary>
        class MaterialInfo
        {
            public string Id { get; set; }
            public string StorageId { get; set; }
            public string MaterialId { get; set; }
            public string PurchaseDetailId { get; set; }
            public string PurchasePlanDetailid { get; set; }
            public double PurchasePrice { get; set; }
            public double Price { get; set; }
            public double Taxes { get; set; }
            public double NoTaxPrice { get; set; }
            public double Quantity { get; set; }
            public double Amount { get; set; }
            public double NoTaxAmount { get; set; }
            public string CheckOut { get; set; }
            public string WareHouseId { get; set; }
            public string SupplierId { get; set; }
            public string State { get; set; }
            public string TypeCode { get; set; }
            public string Sort { get; set; }
            public string Num { get; set; }
            public string Name { get; set; }
            public string Spell { get; set; }
            public string Property { get; set; }
            public string Unit { get; set; }
            public string Brand { get; set; }
            public string Specification { get; set; }
            public string Color { get; set; }
            public string OriginPlace { get; set; }
            public string WarrantyPeriod { get; set; }
            public string Describe { get; set; }
            public string SortCode { get; set; }
            public string UnitName { get; set; }
            public string ColorName { get; set; }
            public string MaterialTypeName { get; set; }
            public string WareHouseName { get; set; }
            public DateTime ReceiptDate { get; set; }
            public string CostCode { get; set; }
            public string ReceiptSign { get; set; }
        }

        /// <summary>
        /// 创建出库详情Parameter
        /// </summary>
        /// <param name="Id">物资ID</param>
        /// <param name="CheckOutId">出库单ID</param>
        /// <param name="UseCount">出库数量</param>
        /// <param name="Purpose">领料用途</param>
        /// <param name="UseDepName">使用部门</param>
        /// <param name="UseUserName">使用人</param>
        /// <param name="UsePosition">使用位置</param>
        /// <param name="Memo">备注</param>
        /// <returns>返回DynamicParameters,为空代表不能出库</returns>
        private DynamicParameters createCheckOutDetailParameter(string Id, string CheckOutId, double UseCount, string Purpose, string UseDepName, string UseUserName, string UsePosition, string Memo)
        {
            MaterialInfo materialInfo = getMaterialInfo(Id);
            if (null == materialInfo)
            {
                return null;
            }
            // 检查是否可出库
            if (!checkCanCheckOut(UseCount, materialInfo))
            {
                return null;
            }

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", Guid.NewGuid().ToString());
            parameters.Add("CheckOutId", CheckOutId);
            parameters.Add("MaterialId", materialInfo.MaterialId);
            parameters.Add("StorageDetailId", materialInfo.Id);
            parameters.Add("Price", materialInfo.Price);
            parameters.Add("Quantity", UseCount);
            parameters.Add("Amount", materialInfo.Amount);
            parameters.Add("Purpose", Purpose);
            parameters.Add("UseDepName", UseDepName);
            parameters.Add("UseUserName", UseUserName);
            parameters.Add("UsePosition", UsePosition);
            parameters.Add("Memo", Memo);
            parameters.Add("CostCode", materialInfo.CostCode);
            return parameters;
        }

        /// <summary>
        /// 检查是否可出库
        /// </summary>
        /// <returns></returns>
        private bool checkCanCheckOut(double outCount, MaterialInfo materialInfo)
        {
            if (outCount <= 0 || null == materialInfo)
            {
                return false;
            }
            // 如果物资数量小于出库数量,返回false
            if (materialInfo.Quantity < outCount)
            {
                return false;
            }
            #region 检查库存数量是否足够
            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                double count = conn.Query<double>(@"SELECT SUM(ISNULL(Quantity,0)) AS Count FROM Tb_Mt_Store 
                                                    WHERE WareHouseId = @WareHouseId and MaterialId = @MaterialId",
                                                    new
                                                    {
                                                        WareHouseId = materialInfo.WareHouseId,
                                                        MaterialId = materialInfo.MaterialId
                                                    }
                                                    ).FirstOrDefault();
                if (count < outCount)
                {
                    return false;
                }
            }
            #endregion
            #region 检查出库单可出库数量是否足够
            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                double canCheckOutCount = conn.Query<double>(@"SELECT (SUM(ISNULL(Quantity,0)) - SUM(ISNULL(CheckOut,0))) AS Count 
                                                                FROM Tb_Mt_StorageDetail WHERE Id = @Id",
                                                                new { Id = materialInfo.Id }).FirstOrDefault();
                if (canCheckOutCount < outCount)
                {
                    return false;
                }
            }

            #endregion
            #region 检查出库单的出库顺序是否正确
            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                // 查询当前出库的物资所在的入库明细在今天之前的可出库的入库明细
                DataTable dt = conn.ExecuteReader(@"SELECT * FROM View_Tb_Mt_StorageDetail_Filter 
                                                    WHERE ReceiptDate < @ReceiptDate AND MaterialId = @MaterialId 
                                                    AND WareHouseId = @WareHouseId AND ISNULL(Quantity,0)-ISNULL(CheckOut,0)>0 
                                                    and [State]='已审核'", new
                {
                    ReceiptDate = materialInfo.ReceiptDate,
                    MaterialId = materialInfo.MaterialId,
                    WareHouseId = materialInfo.WareHouseId
                }).ToDataSet().Tables[0];
                // 如果之前还有未出完的
                if (null != dt && dt.Rows.Count > 0)
                {
                    // 判断入库明细中是否有该ID
                    // 没有就违反先入先出规则
                    DataRow[] ExistRows = dt.Select("Id = '" + materialInfo.Id + "'");
                    if (null == ExistRows || ExistRows.Length <= 0)
                    {
                        return false;
                    }

                    double canCheckOutCount;
                    try
                    {
                        canCheckOutCount = Double.Parse(ExistRows[0]["Quantity"].ToString());
                    }
                    catch (Exception)
                    {
                        canCheckOutCount = 0;
                    }
                    try
                    {
                        canCheckOutCount = canCheckOutCount - Double.Parse(ExistRows[0]["CheckOut"].ToString());
                    }
                    catch (Exception)
                    {
                        canCheckOutCount -= 0;
                    }
                    if (canCheckOutCount < outCount)
                    {
                        return false;
                    }
                }

            }
            #endregion
            return true;
        }
        #endregion


        /// <summary>
        /// 获取报事领料物资费用归属列表_华南城
        /// </summary>
        /// <param name="Row"></param>
        /// <returns></returns>
        private string GetIncidentMaterialCheckOutFeesBelongList_hnc(DataRow Row)
        {
            if (!Row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(Row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            string CommID = Row["CommID"].ToString();

            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            DataTable dt;
            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                dt = conn.ExecuteReader("SELECT * FROM Tb_Dict_Dictionary WHERE IsDelete = 0 AND DType = @DType", new { DType = "费用归属" }, null, null, CommandType.Text).ToDataSet().Tables[0];
            }
            if (null != dt && dt.Rows.Count > 0)
            {
                Dictionary<string, object> dic;
                foreach (DataRow item in dt.Rows)
                {
                    dic = new Dictionary<string, object>
                    {
                        {"Id",item["Id"] },
                        {"Name",item["Name"] },
                        {"Code",item["Code"] },
                        {"Sort",item["Sort"] }
                    };
                    list.Add(dic);
                }
            }
            return new ApiResult(true, list).toJson();
        }
        /// <summary>
        /// 获取报事领料物资出库用途列表_华南城
        /// </summary>
        /// <param name="Row"></param>
        /// <returns></returns>
        private string GetIncidentMaterialCheckOutUseList_hnc(DataRow Row)
        {
            if (!Row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(Row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            string CommID = Row["CommID"].ToString();

            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            DataTable dt;
            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                dt = conn.ExecuteReader("SELECT * FROM Tb_Dict_Dictionary WHERE IsDelete = 0 AND DType = @DType", new { DType = "出库用途" }, null, null, CommandType.Text).ToDataSet().Tables[0];
            }
            if (null != dt && dt.Rows.Count > 0)
            {
                Dictionary<string, object> dic;
                foreach (DataRow item in dt.Rows)
                {
                    dic = new Dictionary<string, object>
                    {
                        {"Id",item["Id"] },
                        {"Name",item["Name"] },
                        {"Code",item["Code"] },
                        {"Sort",item["Sort"] }
                    };
                    list.Add(dic);
                }
            }
            return new ApiResult(true, list).toJson();
        }

        #region 获取报事领料物资出库审批流程列表_华南城
        private DataTable LoadChildTree(DataTable dt, int ID)
        {
            DataTable dataTable = new DataTable();
            foreach (DataRow Row in dt.Select("Filter='04' AND Pid=" + ID))
            {
                if (CheckDataRow(LoadChildTree(dt, AppGlobal.StrToInt(Row["InfoId"].ToString())), Row["InfoId"].ToString()))
                {
                    dataTable.ImportRow(Row);
                }
            }
            return dataTable;
        }

        private bool CheckDataRow(DataTable dt, string Id)
        {
            bool bl = true;
            foreach (DataRow item in dt.Select("InfoId='" + Id + "'"))
            {
                if (item["InfoId"].ToString() == Id)
                {
                    bl = false;
                }
            }
            return bl;
        }
        /// <summary>
        /// 获取报事领料物资出库审批流程列表_华南城
        /// </summary>
        /// <param name="Row"></param>
        /// <returns></returns>
        private string GetIncidentMaterialFlowSortList_hnc(DataRow Row)
        {
            if (!Row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(Row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            string CommID = Row["CommID"].ToString();
            DataTable dt;
            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                string sqlEx = " AND ISNULL(IsDelete,0)=0 AND ((UseStartDate IS NULL OR UseStartDate IS NULL) OR (Datepart(day,GETDATE()) NOT BETWEEN UseStartDate AND UseEndDate) OR ((Datepart(day,GETDATE()) BETWEEN UseStartDate AND UseEndDate)  AND UseUserList LIKE '%" + Global_Var.LoginUserCode + "%')) ";
                dt = conn.ExecuteReader("Proc_WorkFlow_FlowSort_Filter", new { SQLEx = sqlEx }, null, null, CommandType.StoredProcedure).ToDataSet().Tables[0];
            }
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            if (null != dt && dt.Rows.Count > 0)
            {
                DataTable dataTable = new DataTable();
                foreach (DataRow dr in dt.Select(" 1=1 AND Filter='02'"))
                {
                    if (CheckDataRow(LoadChildTree(dt, Convert.ToInt32(dr["InfoId"].ToString())), Row["InfoId"].ToString()))
                    {
                        dataTable.ImportRow(Row);
                    }
                }
                if (null != dataTable && dataTable.Rows.Count > 0)
                {
                    #region 生成列表
                    Dictionary<string, object> dic;
                    foreach (DataRow item in dataTable.Rows)
                    {
                        dic = new Dictionary<string, object>
                            {
                                { "Id", item["Id"] },
                                { "ReceiptSign", item["ReceiptSign"] },
                                { "WareHouseName", item["WareHouseName"] },
                                { "Name", item["Name"] },
                                { "Price", item["Price"] },
                                { "Quantity", item["Quantity"] },
                                { "MaterialTypeName", item["MaterialTypeName"] },
                                { "Num", item["Num"] },
                                { "Spell", item["Spell"] },
                                { "Property", item["Property"] },
                                { "Specification", item["Specification"] },
                                { "UnitName", item["UnitName"] },
                                { "Brand", item["Brand"] },
                                { "CostCode", item["CostCode"] },
                                { "ColorName", item["ColorName"] },
                                { "ReceiptDate", DBNull.Value.Equals(item["ReceiptDate"])?"":((DateTime)item["ReceiptDate"]).ToString() }
                            };
                        list.Add(dic);
                    }
                    #endregion
                }
            }
            return new ApiResult(true, list).toJson();

        }

        #endregion
        /// <summary>
        /// 获取报事领料物资出库列表_华南城
        /// </summary>
        /// <param name="Row"></param>
        /// <returns></returns>
        private string GetIncidentMaterialCheckOutList_hnc(DataRow Row)
        {
            if (!Row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(Row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            string CommID = Row["CommID"].ToString();
            if (!Row.Table.Columns.Contains("WareHouseId") || string.IsNullOrEmpty(Row["WareHouseId"].ToString()))
            {
                return JSONHelper.FromString(false, "仓库编号不能为空");
            }
            string WareHouseId = Row["WareHouseId"].ToString();
            if (!Row.Table.Columns.Contains("SortCode") || string.IsNullOrEmpty(Row["SortCode"].ToString()))
            {
                return JSONHelper.FromString(false, "物资类别编号不能为空");
            }
            string SortCode = Row["SortCode"].ToString();
            int PageIndex = 1;
            if (Row.Table.Columns.Contains("PageIndex"))
            {
                PageIndex = Global_Fun.StrToInt(Row["PageIndex"].ToString());
            }
            int PageSize = 1;
            if (Row.Table.Columns.Contains("PageSize"))
            {
                PageSize = Global_Fun.StrToInt(Row["PageSize"].ToString());
            }
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            DataTable dt = GetList(out int pageCount, out int Counts, "SELECT * FROM View_Tb_Mt_MaterialCheckOut1001_Filter WHERE WareHouseId = '" + WareHouseId + "' AND ISNULL( Quantity, 0 ) > 0 AND SortCode LIKE '%" + SortCode + "%' AND [State] = '已审核' ", PageIndex, PageSize, "MaterialId,ReceiptDate", 0, "Id", PubConstant.hmWyglConnectionString).Tables[0];
            #region 生成列表
            if (null != dt && dt.Rows.Count > 0)
            {
                Dictionary<string, object> dic;
                foreach (DataRow item in dt.Rows)
                {
                    dic = new Dictionary<string, object>
                    {
                        { "Id", item["Id"] },
                        { "ReceiptSign", item["ReceiptSign"] },
                        { "WareHouseName", item["WareHouseName"] },
                        { "Name", item["Name"] },
                        { "Price", item["Price"] },
                        { "Quantity", item["Quantity"] },
                        { "MaterialTypeName", item["MaterialTypeName"] },
                        { "Num", item["Num"] },
                        { "Spell", item["Spell"] },
                        { "Property", item["Property"] },
                        { "Specification", item["Specification"] },
                        { "UnitName", item["UnitName"] },
                        { "Brand", item["Brand"] },
                        { "CostCode", item["CostCode"] },
                        { "ColorName", item["ColorName"] },
                        { "ReceiptDate", DBNull.Value.Equals(item["ReceiptDate"])?"":((DateTime)item["ReceiptDate"]).ToString() }
                    };
                    list.Add(dic);
                }
            }
            #endregion
            string result = new ApiResult(true, list).toJson();
            result = result.Insert(result.Length - 1, ",\"PageCount\":" + pageCount);
            return result;
        }

        /// <summary>
        /// 获取报事领料物资类别列表_华南城
        /// </summary>
        /// <param name="Row"></param>
        /// <returns></returns>
        private string GetIncidentMaterialTypeList_hnc(DataRow Row)
        {
        //    if (!Row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(Row["CommID"].ToString()))
        //    {
        //        return JSONHelper.FromString(false, "小区编号不能为空");
        //    }
        //    string CommID = Row["CommID"].ToString();

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var sql = @"SELECT DISTINCT Id,SortCode,Sort,Name,CostCode,Memo,ParentSortCode 
                            FROM View_Tb_Mt_MaterialType_Filter WHERE ISNULL(IsDelete,0)=0";

                var resultSet = conn.Query(sql);
                return new ApiResult(true, resultSet).toJson();
            }
        }
        /// <summary>
        /// 报事领料仓库列表_华南城
        /// </summary>
        /// <param name="Row"></param>
        /// <returns></returns>
        private string GetIncidentMaterialWareHouseList_hnc(DataRow Row)
        {
            if (!Row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(Row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            string CommID = Row["CommID"].ToString();
            string UserCode = Global_Var.LoginUserCode;

            #region 查询用户权限
            List<string> UserRoles = new List<string>();
            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                DataTable dt = conn.ExecuteReader("select RoleCode from Tb_Sys_UserRole where UserCode = @UserCode", new { UserCode = UserCode }, null, null, CommandType.Text).ToDataSet().Tables[0];
                if (null != dt && dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        UserRoles.Add(item["RoleCode"].ToString());
                    }
                }
            }
            #endregion
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            #region 查询物资仓库列表
            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                DataTable dt = conn.ExecuteReader("SELECT *, CASE LEN(SortCode) WHEN 4 THEN '' ELSE LEFT (SortCode, LEN(SortCode)-4) END AS ParentSortCode,IsRight = dbo.funWareHouseRight (Id, @UserRoles) FROM Tb_Mt_WareHouse WHERE ISNULL( IsDelete, 0) = 0", new { UserRoles = string.Join(",", UserRoles.ToArray()) }, null, null, CommandType.Text).ToDataSet().Tables[0];
                if (null != dt && dt.Rows.Count > 0)
                {
                    Dictionary<string, object> dic;
                    foreach (DataRow item in dt.Rows)
                    {
                        dic = new Dictionary<string, object>
                        {
                            { "Id", item["Id"] },
                            { "SortCode", item["SortCode"] },
                            { "Sort", item["Sort"] },
                            { "Name", item["Name"] },
                            { "ParentSortCode", item["ParentSortCode"] },
                            { "IsRight", item["IsRight"] }
                        };
                        list.Add(dic);
                    }
                }
            }
            #endregion
            return new ApiResult(true, list).toJson();

        }

        /// <summary>
        /// 报事领料-华南城
        /// </summary>
        /// <param name="Row"></param>
        /// <returns></returns>
        private string GetIncidentMaterialList_hnc(DataRow Row)
        {

            if (!Row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(Row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            string CommID = Row["CommID"].ToString();
            if (!Row.Table.Columns.Contains("IncidentID") || string.IsNullOrEmpty(Row["IncidentID"].ToString()))
            {
                return JSONHelper.FromString(false, "报事编号不能为空");
            }
            string IncidentID = Row["IncidentID"].ToString();
            DataTable dt;
            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                dt = conn.ExecuteReader("select * from View_Tb_Mt_CheckOut_Filter where IncidentID = @IncidentID", new { IncidentID = IncidentID }, null, null, CommandType.Text).ToDataSet().Tables[0];
            }
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            if (null != dt && dt.Rows.Count > 0)
            {
                Dictionary<string, object> dic;
                foreach (DataRow item in dt.Rows)
                {
                    dic = new Dictionary<string, object>
                    {
                        { "Id", item["Id"] },
                        { "WareHouseId", item["WareHouseId"] },
                        { "OriginatorName", item["OriginatorName"] },
                        { "WareHouseName", item["WareHouseName"] },
                        { "CheckOutType", item["CheckOutType"] },
                        { "ReceiptSign", item["ReceiptSign"] },
                        { "ReceiptDate", DBNull.Value.Equals(item["ReceiptDate"]) ? "" : ((DateTime)item["ReceiptDate"]).ToString() },
                        { "IncidentID", item["IncidentID"] },
                        { "State", item["State"] }
                    };
                    list.Add(dic);
                }
            }
            return new ApiResult(true, list).toJson();
        }

        #region 户内报事       
        /// <summary>
        /// 户内报事 SetIncidentAcceptPhoneInsert
        /// </summary>
        /// <param name="row"></param>
        /// 输入参数：
        ///     CommID          小区编号【必填】
        ///     CustID          客户编号【必填】
        ///     Content         报事内容【必填】
        ///     RoomID          房间编号【必填】
        ///     Phone           联系方式【必填】
        ///     TypeID          报事项目【必填】
        ///     DealLimit       处理时限【必填】
        ///     EmergencyDegree 紧急程度【必填，1：一般，2：紧急，3：非常紧急】
        ///     ClassID         报事类型【必填】
        ///     images          图片路径【选 填】
        ///     IncidentDate    预约时间【选 填，默认为系统当前时间】
        ///     IncidentMan     报事人  【选 填，默认为空】
        ///     以下参数仅在报事类型为：口头派工时【必填】
        ///     DealMan         处理人
        ///     DealUser        处理人编号
        ///     CoordinateNum   派工单号
        /// 返回:
        ///     false:错误信息
        ///     true:报事成功
        /// <returns></returns>
        private string SetIncidentAcceptPhoneInsert(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            if (!row.Table.Columns.Contains("CustID") || string.IsNullOrEmpty(row["CustID"].ToString()))
            {
                return JSONHelper.FromString(false, "客户编号不能为空");
            }
            if (!row.Table.Columns.Contains("Content") || string.IsNullOrEmpty(row["Content"].ToString()))
            {
                return JSONHelper.FromString(false, "报事内容不能为空");
            }
            if (!row.Table.Columns.Contains("RoomID") || string.IsNullOrEmpty(row["RoomID"].ToString()))
            {
                return JSONHelper.FromString(false, "房间编号不能为空");
            }
            if (!row.Table.Columns.Contains("Phone") || string.IsNullOrEmpty(row["Phone"].ToString()))
            {
                return JSONHelper.FromString(false, "联系方式不能为空");
            }
            if (!row.Table.Columns.Contains("TypeID") || string.IsNullOrEmpty(row["TypeID"].ToString()))
            {
                return JSONHelper.FromString(false, "报事类别不能为空");
            }
            if (!row.Table.Columns.Contains("EmergencyDegree") || string.IsNullOrEmpty(row["EmergencyDegree"].ToString()))
            {
                return JSONHelper.FromString(false, "紧急程度不能为空");
            }
            if (!row.Table.Columns.Contains("ClassID") || string.IsNullOrEmpty(row["ClassID"].ToString()))
            {
                return JSONHelper.FromString(false, "报事类型不能为空");
            }

            if (AppGlobal.StrToInt(row["ClassID"].ToString()) == 2)
            {

                if (!row.Table.Columns.Contains("DealMan") || string.IsNullOrEmpty(row["DealMan"].ToString()))
                {
                    return JSONHelper.FromString(false, "处理人不能为空");
                }
                if (!row.Table.Columns.Contains("DealUser") || string.IsNullOrEmpty(row["DealUser"].ToString()))
                {
                    return JSONHelper.FromString(false, "处理人编号不能为空");
                }
            }

            string backStr = "", incidentId = "";
            try
            {
                string Content = row["Content"].ToString();
                string CommID = row["CommID"].ToString();
                string CustID = row["CustID"].ToString();
                string RoomID = row["RoomID"].ToString();
                string Phone = row["Phone"].ToString();
                string TypeID = row["TypeID"].ToString();
                string DealLimit = row["DealLimit"].ToString();
                string images = "";
                string IncidentDate = DateTime.Now.ToString();
                string IncidentMan = "";
                string CustName = "";
                int EmergencyDegree = AppGlobal.StrToInt(row["EmergencyDegree"].ToString());
                int ClassID = AppGlobal.StrToInt(row["ClassID"].ToString());

                if (row.Table.Columns.Contains("images"))
                {
                    images = row["images"].ToString();
                }
                if (row.Table.Columns.Contains("IncidentDate"))
                {
                    IncidentDate = row["IncidentDate"].ToString();
                }
                if (row.Table.Columns.Contains("IncidentMan"))
                {
                    IncidentMan = row["IncidentMan"].ToString();
                }
                if (row.Table.Columns.Contains("CustName"))
                {
                    CustName = row["CustName"].ToString();
                }

                #region 报事重复受理判断
                using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
                {
                    dynamic info = conn.QueryFirstOrDefault("SELECT * FROM Tb_HSPR_IncidentAccept WHERE CommID = @CommID AND IncidentMan = @IncidentMan AND IncidentContent = @IncidentContent AND Phone = @Phone", new { CommID, IncidentMan, IncidentContent = Content, Phone });
                    if (null != info)
                    {
                        return new ApiResult(true, "报事已受理").toJson();
                    }
                }
                #endregion

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@CommID", CommID);
                parameters.Add("@CustID", CustID);
                parameters.Add("@RoomID", RoomID);
                parameters.Add("@IncidentDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                parameters.Add("@IncidentMan", IncidentMan);
                parameters.Add("@IncidentContent", Content);

                parameters.Add("@ReserveDate", IncidentDate);
                parameters.Add("@Phone", Phone);
                parameters.Add("@IncidentImgs", images);
                parameters.Add("@TypeID", TypeID);
                parameters.Add("@DealLimit", DealLimit);
                parameters.Add("@EmergencyDegree", EmergencyDegree);

                // 鸿坤
                if (Global_Var.LoginCorpID == "1973")
                {
                    parameters.Add("@CustName", CustName);
                    parameters.Add("@AdmiMan", Global_Var.LoginUserName);
                }

                //口头派工时
                if (ClassID == 2)
                {
                    parameters.Add("@DispType", 3);
                    parameters.Add("@DispMan", Global_Var.UserName);
                    parameters.Add("@DealMan", row["DealMan"]);
                    parameters.Add("@DealUser", row["DealUser"]);
                    parameters.Add("@CoordinateNum", HSPR_IncidentAssigned_GetCoordinateNum(row["CommID"].ToString(), 3, "K"));
                }
                else
                {
                    parameters.Add("@DispType", 0);
                    parameters.Add("@DispMan", "");
                    parameters.Add("@DealMan", "");
                    parameters.Add("@DealUser", "");
                    parameters.Add("@CoordinateNum", "");
                }

                IDbConnection con = new SqlConnection(PubConstant.hmWyglConnectionString);
                con.Execute("Proc_HSPR_IncidentAccept_PhoneInsert_WG", parameters, null, null, CommandType.StoredProcedure);

                //获取当前报事
                string str = "select top 1 * from Tb_HSPR_IncidentAccept where CommID=@CommID and Phone=@Phone and IncidentImgs=@IncidentImgs and IncidentContent=@IncidentContent order by incidentdate desc";
                Tb_HSPR_IncidentAccept model = con.Query<Tb_HSPR_IncidentAccept>(str, new { CommID = row["CommID"].ToString(), Phone = Phone, IncidentImgs = images, IncidentContent = Content }).LastOrDefault();
                incidentId = model.IncidentID.ToString();

                Dictionary<string, string> data = new Dictionary<string, string>();
                data.Add("Data", model.IncidentID.ToString());
                data.Add("IncidentID", model.IncidentID.ToString());
                data.Add("IncidentPlace", model.IncidentPlace);

                // 消息推送
                //new IncidentAccept().SelectAppMsgSend(strcon, "业主权属", CommID, RoomID, TypeID, model.IncidentID.ToString(), data);

                // 户内报事，消息推送
                IncidentAcceptPush.SynchPushIndoorIncident(model);


                #region  鸿坤报事需要把报事信息推送给第三方400 
                try
                {     //鸿坤单独
                    if (Global_Var.LoginCorpID == "1973")
                    {

                        #region 同步新增报事   
                        Dictionary<string, string> dir = new Dictionary<string, string>();
                        dir.Add("incidentID", model.IncidentID.ToString());
                        dir.Add("commID", model.CommID.ToString());
                        dir.Add("custID", model.CustID.ToString());
                        dir.Add("roomID", model.RoomID.ToString());
                        dir.Add("corpTypeID", model.TypeID);
                        dir.Add("incidentPlace", model.IncidentPlace);
                        dir.Add("incidentMan", model.IncidentMan);
                        dir.Add("incidentDate", model.IncidentDate.ToString());
                        dir.Add("incidentMode", model.IncidentMode);
                        dir.Add("dealLimit", model.DealLimit.ToString());
                        dir.Add("replyLimit", model.ReplyLimit);
                        dir.Add("incidentContent", model.IncidentContent);
                        dir.Add("reserveDate", model.ReserveDate.ToString());
                        dir.Add("phone", model.Phone);
                        dir.Add("admiMan", model.AdmiMan);
                        dir.Add("admiDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        dir.Add("dispType", model.DispType.ToString());
                        dir.Add("dispMan", string.IsNullOrEmpty(model.DispMan) ? "" : model.DispMan.ToString());
                        dir.Add("dispDate", model.DispDate.ToString());
                        dir.Add("dispLimit", model.DispLimit);
                        dir.Add("dealMan", model.DealMan);
                        dir.Add("coordinateNum", string.IsNullOrEmpty(model.CoordinateNum) ? "" : model.CoordinateNum.ToString());
                        dir.Add("mainEndDate", ClassID == 2 ? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") : "");
                        dir.Add("isDeal", ClassID == 2 ? "1" : "0");//0:未完结1：完结    我们的口派直接完结 
                        dir.Add("incidentnum", model.IncidentNum.ToString());
                        dir.Add("operateType", "0");//0：新增，1：修改，2：删除 

                        SynchronizeIncidentData.SynchronizeData_WorkOrderInfo(dir, Connection.GetConnection("8"));

                        #endregion
                    }



                }
                catch
                {


                }


                #endregion

            }
            catch (Exception ex)
            {
                backStr = ex.Message;
            }
            if (backStr != "")
            {
                return JSONHelper.FromString(false, backStr);
            }
            else
            {
                return JSONHelper.FromString(true, incidentId);
            }
        }


        /// <summary>
        /// 获取业主信息
        /// </summary>
        /// <param name="row"></param>
        /// 房屋编号：RoomID【必填】
        /// 返回：
        ///     CustID      客户编号
        ///     CustName    客户名称
        ///     LiveTypeName    业主类型
        ///     Linkmantel      联系电话
        ///     MobilePhone     移动电话【二个号码取一个】
        /// <returns></returns>
        private string GetIncidentAcceptOwnerInfo(DataRow row)
        {
            if (!row.Table.Columns.Contains("RoomID") || string.IsNullOrEmpty(row["RoomID"].ToString()))
            {
                return JSONHelper.FromString(false, "房屋编码不能为空");
            }

            IDbConnection con = new SqlConnection(PubConstant.hmWyglConnectionString);
            StringBuilder sb = new StringBuilder();
            sb.Append("select ");
            sb.Append("  CustID,CustName,LiveTypeName,Linkmantel,MobilePhone  ");
            sb.Append(" From ");
            sb.Append(" view_HSPR_CustomerLive_Filter ");
            sb.Append(" where ");
            sb.Append("  IsSale=0 and LiveType<3  ");//筛掉历史业主和临时客户【IsSale=1 历史业主，LiveType=3 临时客户】
            sb.AppendFormat("and RoomID='{0}'", row["RoomID"]);


            DataSet ds = con.ExecuteReader(sb.ToString(), null, null, null, CommandType.Text).ToDataSet();

            return JSONHelper.FromString(ds.Tables[0]);
        }

        #endregion

        #region 公区报事
        /// <summary>
        /// 公区报事  SetIncidentAcceptPhoneInsertRegion
        /// </summary>
        /// <param name="row"></param>
        /// CommID 小区编号【必填】
        /// Content 报事内容【必填】
        /// RegionalID 报事区域ID【必填】
        /// TypeID    报事项目ID【必填】
        /// ClassID     报事类型【必填】
        /// ObjectIDList  对像集【选填，格式：8888,88888,8888】
        /// Signatory    区域名称
        /// AdmiMan     报事人【默认当前登录用户】
        /// Phone  联系电话【默认当前登录用户手机号】
        /// images 图片
        /// EmergencyDegree 紧急程度【必填，1：一般，2：紧急，3：非常紧急】
        /// DealLimit       【处理时限，默认0】
        ///     以下参数仅在报事类型为：口头派工时【必填】
        ///     DealMan         处理人
        ///     DealUser        处理人编号
        ///     CoordinateNum   派工单号
        /// 返回：
        ///     true   报事成功
        ///     false  错误信息
        /// <returns></returns>
        private string SetIncidentAcceptPhoneInsertRegion(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }

            if (!row.Table.Columns.Contains("Content") || string.IsNullOrEmpty(row["Content"].ToString()))
            {
                return JSONHelper.FromString(false, "报事内容不能为空");
            }
            if (!row.Table.Columns.Contains("RegionalID") || string.IsNullOrEmpty(row["RegionalID"].ToString()))
            {
                return JSONHelper.FromString(false, "报事区域不能为空");
            }
            if (!row.Table.Columns.Contains("TypeID") || string.IsNullOrEmpty(row["TypeID"].ToString()))
            {
                return JSONHelper.FromString(false, "报事项目不能为空");
            }
            if (!row.Table.Columns.Contains("ClassID") || string.IsNullOrEmpty(row["ClassID"].ToString()))
            {
                return JSONHelper.FromString(false, "报事类型不能为空");
            }
            if (!row.Table.Columns.Contains("EmergencyDegree") || string.IsNullOrEmpty(row["EmergencyDegree"].ToString()))
            {
                return JSONHelper.FromString(false, "紧急程度不能为空");
            }

            int returnType = 1;
            if (row.Table.Columns.Contains("ReturnType"))
            {
                returnType = AppGlobal.StrToInt(row["ReturnType"].ToString());
            }

            string IncidentDate = DateTime.Now.ToString();
            if (row.Table.Columns.Contains("IncidentDate") && !string.IsNullOrEmpty(row["IncidentDate"].ToString()))
            {
                IncidentDate = row["IncidentDate"].ToString();
            }

            int ClassID = AppGlobal.StrToInt(row["ClassID"].ToString());
            if (ClassID == 2)
            {
                if (!(Global_Var.LoginCorpID == "1906"))//平安 可以不传处理人
                {
                    if (!row.Table.Columns.Contains("DealMan") || string.IsNullOrEmpty(row["DealMan"].ToString()))
                    {
                        return JSONHelper.FromString(false, "处理人不能为空");
                    }
                    if (!row.Table.Columns.Contains("DealUser") || string.IsNullOrEmpty(row["DealUser"].ToString()))
                    {
                        return JSONHelper.FromString(false, "处理人编号不能为空");
                    }
                }
            }
            string locationId = null;
            string objectId = null;
            if (row.Table.Columns.Contains("LocationID") && !string.IsNullOrEmpty(row["LocationID"].ToString()))
            {
                locationId = row["LocationID"].ToString();
            }
            if (row.Table.Columns.Contains("ObjectID") && !string.IsNullOrEmpty(row["ObjectID"].ToString()))
            {
                objectId = row["ObjectID"].ToString();
            }


            string Content = row["Content"].ToString();
            string CommID = row["CommID"].ToString();
            string RegionalID = row["RegionalID"].ToString();
            string TypeID = row["TypeID"].ToString();
            string Phone = Global_Var.LoginMobile;
            string images = "";
            string ObjectIDList = "";
            int EmergencyDegree = AppGlobal.StrToInt(row["EmergencyDegree"].ToString());
            int DealLimit = 0;
            string IncidentMan = "";
            string IncidenntMode = "员工线上报事";

            if (row.Table.Columns.Contains("IncidentMan"))//平安 需求4511 公区报事增加报事人字段
            {
                IncidentMan = row["IncidentMan"].ToString();
            }

            if (row.Table.Columns.Contains("Phone"))
            {
                Phone = row["Phone"].ToString();
            }
            if (row.Table.Columns.Contains("images"))
            {
                images = row["images"].ToString();
            }
            if (row.Table.Columns.Contains("ObjectIDList"))
            {
                ObjectIDList = row["ObjectIDList"].ToString();
            }
            if (row.Table.Columns.Contains("DealLimit"))
            {
                DealLimit = AppGlobal.StrToInt(row["DealLimit"].ToString());
            }
            if (row.Table.Columns.Contains("IncidenntMode"))
            {
                IncidenntMode = row["IncidenntMode"].ToString();
            }

            // 设备报事相关，数据字段为这两个
            string deviceId = null;
            string taskEqId = null;

            if (row.Table.Columns.Contains("DeviceID") && row["DeviceID"].ToString() != "(null)")
            {
                deviceId = row["DeviceID"].ToString();
            }

            if (row.Table.Columns.Contains("TaskEqId") && row["TaskEqId"].ToString() != "(null)")
            {
                taskEqId = row["TaskEqId"].ToString();
            }

            string backStr = "";
            string incidentId = "";
            string incidentNum = "";
            try
            {
                IDbConnection con = new SqlConnection(PubConstant.hmWyglConnectionString);

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@CommID", CommID);
                parameters.Add("@AdmiMan", Global_Var.LoginUserName);
                if (Global_Var.LoginCorpID == "1906")
                {
                    parameters.Add("@IncidentMan", IncidentMan);
                }
                parameters.Add("@IncidentContent", Content);

                parameters.Add("@Phone", Phone);
                parameters.Add("@IncidentImgs", images);
                parameters.Add("@RegionalID", RegionalID);

                parameters.Add("@TypeID", TypeID);
                parameters.Add("@DealLimit", DealLimit);
                parameters.Add("@EmergencyDegree", EmergencyDegree);
                if (!string.IsNullOrEmpty(locationId))
                {
                    parameters.Add("@LocationID", locationId);
                }
                if (!string.IsNullOrEmpty(objectId))
                {
                    parameters.Add("@ObjectID", objectId);
                }
                //华南城 增加报事类型
                if (Global_Var.LoginCorpID == "1975" || Global_Var.LoginCorpID == "1971")
                {
                    parameters.Add("@IncidentMode", IncidenntMode);
                }

                //口头派工时
                if (ClassID == 2)
                {
                    parameters.Add("@DispType", 3);
                    parameters.Add("@DispMan", Global_Var.UserName);
                    parameters.Add("@DealMan", row["DealMan"]);
                    parameters.Add("@DealUser", row["DealUser"]);
                    parameters.Add("@CoordinateNum", HSPR_IncidentAssigned_GetCoordinateNum(row["CommID"].ToString(), 3, "K"));
                }
                else
                {
                    parameters.Add("@DispType", 0);
                    parameters.Add("@DispMan", "");
                    parameters.Add("@DealMan", "");
                    parameters.Add("@DealUser", "");
                    parameters.Add("@CoordinateNum", "");
                }

                if (string.IsNullOrEmpty(deviceId) && string.IsNullOrEmpty(taskEqId))
                {
                    con.ExecuteScalar("Proc_HSPR_IncidentAccept_PhoneInsert_Region_WG", parameters, null, null, CommandType.StoredProcedure);
                }
                else
                {
                    if (Global_Var.LoginCorpID == "1906")
                    {
                        parameters.Add("@DeviceID", 0);
                    }
                    else
                    {
                        parameters.Add("@DeviceID", deviceId);
                    }

                    parameters.Add("@TaskEqId", taskEqId);
                    con.ExecuteScalar("Proc_HSPR_IncidentAccept_PhoneInsert_Region_WG_Device", parameters, null, null, CommandType.StoredProcedure);
                }

                con.Dispose();
                //获取当前报事
                con = new SqlConnection(PubConstant.hmWyglConnectionString);
                string str = "select top 1 * from Tb_HSPR_IncidentAccept where CommID=@CommID  and Phone=@Phone  and IncidentImgs=@IncidentImgs and IncidentContent=@IncidentContent order by incidentdate desc";
                Tb_HSPR_IncidentAccept model = con.Query<Tb_HSPR_IncidentAccept>(str, new { CommID = CommID, Phone = Phone, IncidentImgs = images, IncidentContent = Content }).LastOrDefault();
                incidentId = (model != null ? model.IncidentID.ToString() : "");
                incidentNum = (model != null ? model.IncidentNum : "");
                //2018-01-17,敬志强
                //解决公区报事无预约时间的问题
                //省时省力的解决办法
                if (!string.IsNullOrEmpty(incidentNum))
                {
                    con.Execute("UPDATE Tb_HSPR_IncidentAccept SET ReserveDate = @ReserveDate WHERE IncidentNum = @IncidentNum", new { ReserveDate = IncidentDate, IncidentNum = incidentNum }, null, null, CommandType.Text);
                }
                if (ObjectIDList != "")
                {
                    if (model != null)
                    {
                        //删除所有子表数据
                        string IncidentID = model.IncidentID.ToString();
                        SqlParameter[] dbParams = new SqlParameter[] {
                        new SqlParameter("@IncidentID",SqlDbType.BigInt)
                    };
                        dbParams[0].Value = IncidentID;
                        int rowNum = con.Execute("Proc_HSPR_IncidentAcceptObject_DeleteALL", dbParams, null, null, CommandType.StoredProcedure);
                        con.Dispose();
                        //插入子表数据
                        string[] idListPar = ObjectIDList.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        for (int i = 0; i < idListPar.Length; i++)
                        {
                            con = new SqlConnection(PubConstant.hmWyglConnectionString);
                            SqlParameter[] sqlpar = new SqlParameter[] {
                                new SqlParameter("@IncidentAcceptObjectID",SqlDbType.BigInt),
                                new SqlParameter("@ObjectID",SqlDbType.BigInt),
                                new SqlParameter("@IncidentID",SqlDbType.BigInt),
                                new SqlParameter("@IsDelete",SqlDbType.Int),
                                new SqlParameter("@CommID",SqlDbType.Int)
                            };
                            sqlpar[0].Value = 0;
                            sqlpar[1].Value = AppGlobal.StrToLong(idListPar[i]);
                            sqlpar[2].Value = IncidentID;
                            sqlpar[3].Value = 0;
                            sqlpar[4].Value = CommID;
                            dbParams[0].Value = IncidentID;
                            int rowNum_inser = con.Execute("Proc_HSPR_IncidentAcceptObject_Insert", sqlpar, null, null, CommandType.StoredProcedure);
                            con.Dispose();
                        }
                    }
                }
                // 推送
                IncidentAcceptPush.SynchPushPublicIncident(model);

                #region  鸿坤报事需要把报事信息推送给第三方400 
                try
                {
                    //鸿坤单独
                    if (Global_Var.LoginCorpID == "1973")
                    {
                        #region 同步新增报事   
                        Dictionary<string, string> dir = new Dictionary<string, string>();
                        dir.Add("incidentID", model.IncidentID.ToString());
                        dir.Add("commID", model.CommID.ToString());
                        dir.Add("custID", model.CustID.ToString());
                        dir.Add("roomID", model.RoomID.ToString());
                        dir.Add("corpTypeID", model.TypeID);
                        dir.Add("incidentPlace", model.IncidentPlace);
                        dir.Add("incidentMan", model.IncidentMan);
                        dir.Add("incidentDate", model.IncidentDate.ToString());
                        dir.Add("incidentMode", model.IncidentMode);
                        dir.Add("dealLimit", model.DealLimit.ToString());
                        dir.Add("replyLimit", model.ReplyLimit);
                        dir.Add("incidentContent", model.IncidentContent);
                        dir.Add("reserveDate", model.ReserveDate.ToString());
                        dir.Add("phone", model.Phone);
                        dir.Add("admiMan", model.AdmiMan);
                        dir.Add("admiDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        dir.Add("dispType", model.DispType.ToString());
                        dir.Add("dispMan", string.IsNullOrEmpty(model.DispMan) ? "" : model.DispMan.ToString());
                        dir.Add("dispDate", model.DispDate.ToString());
                        dir.Add("dispLimit", model.DispLimit);
                        dir.Add("dealMan", model.DealMan);
                        dir.Add("coordinateNum", string.IsNullOrEmpty(model.CoordinateNum) ? "" : model.CoordinateNum.ToString());
                        dir.Add("mainEndDate", ClassID == 2 ? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") : "");
                        dir.Add("isDeal", ClassID == 2 ? "1" : "0");//0:未完结1：完结    我们的口派直接完结 
                        dir.Add("incidentnum", model.IncidentNum.ToString());
                        dir.Add("operateType", "0");//0：新增，1：修改，2：删除 
                        SynchronizeIncidentData.SynchronizeData_WorkOrderInfo(dir, Connection.GetConnection("8"));

                        #endregion
                    }

                }
                catch
                {


                }



                #endregion

            }
            catch (Exception ex)
            {
                backStr = ex.Message + ex.StackTrace;
            }

            if (backStr != "")
            {
                return JSONHelper.FromString(false, backStr);
            }
            else
            {
                if (returnType == 0)
                {
                    return JSONHelper.FromString(true, incidentId);
                }
                else if (returnType == 1)
                {
                    return JSONHelper.FromString(true, incidentNum);
                }
                else
                {
                    return JSONHelper.FromString(true, incidentId + "|" + incidentNum);
                }
            }
        }

        private string SetIncidentAcceptPhoneInsertRegion_hnc(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }

            if (!row.Table.Columns.Contains("Content") || string.IsNullOrEmpty(row["Content"].ToString()))
            {
                return JSONHelper.FromString(false, "报事内容不能为空");
            }
            if (!row.Table.Columns.Contains("RegionalID") || string.IsNullOrEmpty(row["RegionalID"].ToString()))
            {
                return JSONHelper.FromString(false, "报事区域不能为空");
            }
            if (!row.Table.Columns.Contains("LocationID") || string.IsNullOrEmpty(row["LocationID"].ToString()))
            {
                return JSONHelper.FromString(false, "公区位置不能为空");
            }
            if (!row.Table.Columns.Contains("ObjectID") || string.IsNullOrEmpty(row["ObjectID"].ToString()))
            {
                return JSONHelper.FromString(false, "公区对象不能为空");
            }
            if (!row.Table.Columns.Contains("TypeID") || string.IsNullOrEmpty(row["TypeID"].ToString()))
            {
                return JSONHelper.FromString(false, "报事项目不能为空");
            }
            if (!row.Table.Columns.Contains("ClassID") || string.IsNullOrEmpty(row["ClassID"].ToString()))
            {
                return JSONHelper.FromString(false, "报事类型不能为空");
            }
            if (!row.Table.Columns.Contains("EmergencyDegree") || string.IsNullOrEmpty(row["EmergencyDegree"].ToString()))
            {
                return JSONHelper.FromString(false, "紧急程度不能为空");
            }
            string IncidentDate = DateTime.Now.ToString();
            if (row.Table.Columns.Contains("IncidentDate") && !string.IsNullOrEmpty(row["IncidentDate"].ToString()))
            {
                IncidentDate = row["IncidentDate"].ToString();
            }

            int ClassID = AppGlobal.StrToInt(row["ClassID"].ToString());
            if (ClassID == 2)
            {

                if (!row.Table.Columns.Contains("DealMan") || string.IsNullOrEmpty(row["DealMan"].ToString()))
                {
                    return JSONHelper.FromString(false, "处理人不能为空");
                }
                if (!row.Table.Columns.Contains("DealUser") || string.IsNullOrEmpty(row["DealUser"].ToString()))
                {
                    return JSONHelper.FromString(false, "处理人编号不能为空");
                }
            }
            string locationId = null;
            string objectId = null;
            if (row.Table.Columns.Contains("LocationID") && !string.IsNullOrEmpty(row["LocationID"].ToString()))
            {
                locationId = row["LocationID"].ToString();
            }
            if (row.Table.Columns.Contains("ObjectID") && !string.IsNullOrEmpty(row["ObjectID"].ToString()))
            {
                objectId = row["ObjectID"].ToString();
            }


            string Content = row["Content"].ToString();
            string CommID = row["CommID"].ToString();
            string AdmiMan = Global_Var.LoginUserName;
            string RegionalID = row["RegionalID"].ToString();
            string TypeID = row["TypeID"].ToString();
            string Phone = Global_Var.LoginMobile;
            string images = "";
            string ObjectIDList = "";
            int EmergencyDegree = AppGlobal.StrToInt(row["EmergencyDegree"].ToString());
            int DealLimit = 0;

            if (row.Table.Columns.Contains("AdmiMan"))
            {
                AdmiMan = row["AdmiMan"].ToString();
            }
            if (row.Table.Columns.Contains("Phone"))
            {
                Phone = row["Phone"].ToString();
            }
            if (row.Table.Columns.Contains("images"))
            {
                images = row["images"].ToString();
            }
            if (row.Table.Columns.Contains("ObjectIDList"))
            {
                ObjectIDList = row["ObjectIDList"].ToString();
            }
            if (row.Table.Columns.Contains("DealLimit"))
            {
                DealLimit = AppGlobal.StrToInt(row["DealLimit"].ToString());
            }

            // 设备报事相关，数据字段为这两个
            string deviceId = null;
            string taskEqId = null;

            if (row.Table.Columns.Contains("DeviceID") && row["DeviceID"].ToString() != "(null)")
            {
                deviceId = row["DeviceID"].ToString();
            }

            if (row.Table.Columns.Contains("TaskEqId") && row["TaskEqId"].ToString() != "(null)")
            {
                taskEqId = row["TaskEqId"].ToString();
            }

            string backStr = "";
            string incidentNum = "";
            try
            {
                IDbConnection con = new SqlConnection(PubConstant.hmWyglConnectionString);

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@CommID", CommID);
                parameters.Add("@AdmiMan", AdmiMan);
                parameters.Add("@IncidentContent", Content);

                parameters.Add("@Phone", Phone);
                parameters.Add("@IncidentImgs", images);
                parameters.Add("@RegionalID", RegionalID);

                parameters.Add("@TypeID", TypeID);
                parameters.Add("@DealLimit", DealLimit);
                parameters.Add("@EmergencyDegree", EmergencyDegree);
                if (!string.IsNullOrEmpty(locationId))
                {
                    parameters.Add("@LocationID", locationId);
                }
                if (!string.IsNullOrEmpty(objectId))
                {
                    parameters.Add("@ObjectID", objectId);
                }

                //口头派工时
                if (ClassID == 2)
                {
                    parameters.Add("@DispType", 3);
                    parameters.Add("@DispMan", Global_Var.UserName);
                    parameters.Add("@DealMan", row["DealMan"]);
                    parameters.Add("@DealUser", row["DealUser"]);
                    parameters.Add("@CoordinateNum", HSPR_IncidentAssigned_GetCoordinateNum(row["CommID"].ToString(), 3, "K"));
                }
                else
                {
                    parameters.Add("@DispType", 0);
                    parameters.Add("@DispMan", "");
                    parameters.Add("@DealMan", "");
                    parameters.Add("@DealUser", "");
                    parameters.Add("@CoordinateNum", "");
                }

                if (string.IsNullOrEmpty(deviceId) && string.IsNullOrEmpty(taskEqId))
                {
                    con.ExecuteScalar("Proc_HSPR_IncidentAccept_PhoneInsert_Region_WG", parameters, null, null, CommandType.StoredProcedure);
                }
                else
                {
                    parameters.Add("@DeviceID", deviceId);
                    parameters.Add("@TaskEqId", taskEqId);
                    con.ExecuteScalar("Proc_HSPR_IncidentAccept_PhoneInsert_Region_WG_Device", parameters, null, null, CommandType.StoredProcedure);
                }

                con.Dispose();
                //获取当前报事
                con = new SqlConnection(PubConstant.hmWyglConnectionString);
                string str = "select top 1 * from Tb_HSPR_IncidentAccept where CommID=@CommID  and Phone=@Phone  and IncidentImgs=@IncidentImgs and IncidentContent=@IncidentContent order by incidentdate desc";
                Tb_HSPR_IncidentAccept model = con.Query<Tb_HSPR_IncidentAccept>(str, new { CommID = CommID, Phone = Phone, IncidentImgs = images, IncidentContent = Content }).LastOrDefault();
                incidentNum = (model != null ? model.IncidentNum : "");
                //2018-01-17,敬志强
                //解决公区报事无预约时间的问题
                //省时省力的解决办法
                if (!string.IsNullOrEmpty(incidentNum))
                {
                    con.Execute("UPDATE Tb_HSPR_IncidentAccept SET ReserveDate = @ReserveDate WHERE IncidentNum = @IncidentNum", new { ReserveDate = IncidentDate, IncidentNum = incidentNum }, null, null, CommandType.Text);
                }
                if (ObjectIDList != "")
                {
                    if (model != null)
                    {
                        //删除所有子表数据
                        string IncidentID = model.IncidentID.ToString();
                        SqlParameter[] dbParams = new SqlParameter[] {
                        new SqlParameter("@IncidentID",SqlDbType.BigInt)
                    };
                        dbParams[0].Value = IncidentID;
                        int rowNum = con.Execute("Proc_HSPR_IncidentAcceptObject_DeleteALL", dbParams, null, null, CommandType.StoredProcedure);
                        con.Dispose();
                        //插入子表数据
                        string[] idListPar = ObjectIDList.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        for (int i = 0; i < idListPar.Length; i++)
                        {
                            con = new SqlConnection(PubConstant.hmWyglConnectionString);
                            SqlParameter[] sqlpar = new SqlParameter[] {
                        new SqlParameter("@IncidentAcceptObjectID",SqlDbType.BigInt),
                        new SqlParameter("@ObjectID",SqlDbType.BigInt),
                        new SqlParameter("@IncidentID",SqlDbType.BigInt),
                        new SqlParameter("@IsDelete",SqlDbType.Int),
                        new SqlParameter("@CommID",SqlDbType.Int)
                        };
                            sqlpar[0].Value = 0;
                            sqlpar[1].Value = AppGlobal.StrToLong(idListPar[i]);
                            sqlpar[2].Value = IncidentID;
                            sqlpar[3].Value = 0;
                            sqlpar[4].Value = CommID;
                            dbParams[0].Value = IncidentID;
                            int rowNum_inser = con.Execute("Proc_HSPR_IncidentAcceptObject_Insert", sqlpar, null, null, CommandType.StoredProcedure);
                            con.Dispose();
                        }
                    }
                }

                // 推送
                IncidentAcceptPush.SynchPushPublicIncident(model);
            }
            catch (Exception ex)
            {
                backStr = ex.Message;
            }

            if (backStr != "")
            {
                return JSONHelper.FromString(false, backStr);
            }
            else
            {
                if (string.IsNullOrEmpty(deviceId) && string.IsNullOrEmpty(taskEqId))
                {
                    return JSONHelper.FromString(true, "报事成功!");
                }
                else
                {
                    return JSONHelper.FromString(true, incidentNum);
                }
            }
        }
        #endregion


        #region 基础数据
        /// <summary>
        /// 获取派工单、协调单权限GetHSPR_AuditingSet
        /// </summary>
        /// <param name="row"></param>
        /// CommID      小区编码
        /// 返回：
        ///     值为0时，不允许专协调单。
        /// <returns></returns>
        private string GetHSPR_AuditingSet(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编码不能为空");
            }

            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                var result = conn.Query<string>(@"SELECT ISNULL(IsAuditing22,0) FROM Tb_HSPR_AuditingSet WHERE CommID=@CommID",
                    new { CommID = row["CommID"].ToString() }).FirstOrDefault();
                if (result == null)
                {
                    result = "0";
                }

                return JSONHelper.FromString(true, result);
            }
        }

        /// <summary>
        /// 派工单号GetIncidentAssigned_GetCoordinateNum_P
        /// </summary>
        /// <param name="row"></param>
        /// CommID      小区编号
        /// <returns></returns>
        private string GetIncidentAssigned_GetCoordinateNum_P(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编码不能为空");
            }
            string str = HSPR_IncidentAssigned_GetCoordinateNum(row["CommID"].ToString(), 1, "P");
            return JSONHelper.FromString(true, str);
        }

        /// <summary>
        /// 协调单号GetIncidentAssigned_GetCoordinateNum_X
        /// </summary>
        /// <param name="row"></param>
        /// CommID      小区编号
        /// <returns></returns>
        private string GetIncidentAssigned_GetCoordinateNum_X(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编码不能为空");
            }
            string str = HSPR_IncidentAssigned_GetCoordinateNum(row["CommID"].ToString(), 2, "X");
            return JSONHelper.FromString(true, str);
        }
        /// <summary>
        /// 口头派工单号GetIncidentAssigned_GetCoordinateNum_K
        /// </summary>
        /// <param name="row"></param>
        /// CommID      小区编号
        /// <returns></returns>
        private string GetIncidentAssigned_GetCoordinateNum_K(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编码不能为空");
            }
            string str = HSPR_IncidentAssigned_GetCoordinateNum(row["CommID"].ToString(), 3, "K");
            return JSONHelper.FromString(true, str);
        }

        /// <summary>
        /// 获取房间信息 GetRoomLIst
        /// </summary>
        /// <param name="row"></param>
        /// 小区编号：CommID 必填
        /// 楼栋编号：BuildSNum 必填
        /// 返回：
        ///     房间ID：RoomID
        ///     小区ID:CommID
        ///     房间编码：RoomSign
        ///     单元编码：UnitSNum
        ///     业主名称：CustName
        ///     联系方式：MobilePhone
        ///     业主编号：CustID
        /// <returns></returns>
        private string GetRoomLIst(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编码不能为空");
            }
            if (!row.Table.Columns.Contains("BuildSNum") || string.IsNullOrEmpty(row["BuildSNum"].ToString()))
            {
                return JSONHelper.FromString(false, "楼栋编码不能为空");
            }

            IDbConnection con = new SqlConnection(PubConstant.hmWyglConnectionString);
            DataSet ds = con.ExecuteReader("select RoomID,CommID,RoomSign,UnitSNum,CustName,MobilePhone,CustID from view_HSPR_Room_AllFilter  where RoomSign is not null and CommID='" + row["CommID"] + "'  and  BuildSNum=" + row["BuildSNum"].ToString(), null, null, null, CommandType.Text).ToDataSet();

            return JSONHelper.FromString(ds.Tables[0]);
        }

        /// <summary>
        /// 检索房屋编号 GetTopRoomList
        /// </summary>
        /// <param name="row"></param>
        /// roomSign    编号
        /// 返回：
        ///     房间ID：RoomID
        ///     小区ID:CommID
        ///     房间编码：RoomSign
        ///     单元编码：UnitSNum
        ///     业主名称：CustName
        ///     联系方式：MobilePhone
        ///     业主编号：CustID
        /// <returns></returns>
        private string GetTopRoomList(DataRow row)
        {
            string roomSign = "";

            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编码不能为空");
            }
            if (row.Table.Columns.Contains("RoomSign"))
            {
                roomSign = row["RoomSign"].ToString();
            }

            IDbConnection con = new SqlConnection(PubConstant.hmWyglConnectionString);
            string sqlStr;
            if (Global_Var.LoginCorpID == "1971")
            {
                sqlStr = $@"select top 20 RoomID,CommID,isnull(NC_NewRoomSign, RoomSign) AS RoomSign,UnitSNum,CustName,
                MobilePhone,CustID from view_HSPR_CustomerLive_Filter 
                where CommID={row["CommID"].ToString()} and IsDelLive = 0 and 
                (RoomSign like '%{roomSign}%' OR RoomName like '%{roomSign}%') ORDER BY RoomSign";
            }
            else
            {
                sqlStr = $@"select top 20 RoomID,CommID,isnull(RoomName, RoomSign) AS RoomSign,UnitSNum,CustName,
                MobilePhone,CustID from view_HSPR_CustomerLive_Filter 
                where CommID={row["CommID"].ToString()} and IsDelLive = 0 and 
                (RoomSign like '%{roomSign}%' OR RoomName like '%{roomSign}%') ORDER BY RoomSign";
            }

            DataSet ds = con.ExecuteReader(sqlStr, null, null, null, CommandType.Text).ToDataSet();
            return JSONHelper.FromString(ds.Tables[0]);
        }

        /// <summary>
        /// 获取小区内的楼栋 GetBanList
        /// </summary>
        /// <param name="row"></param>
        /// 小区编号：CommID 必填
        /// 返回：
        ///     楼栋编号：BuildSNum
        ///     楼栋名称：BuildName
        /// <returns></returns>
        private string GetBanList(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编码不能为空");
            }

            IDbConnection con = new SqlConnection(PubConstant.hmWyglConnectionString);
            DataSet ds = con.ExecuteReader("select BuildName,BuildSNum from Tb_HSPR_Building where CommID='" + row["CommID"] + "'", null, null, null, CommandType.Text).ToDataSet();
            return JSONHelper.FromString(ds.Tables[0]);
        }

        /// <summary>
        /// 公区报事位置GetIncidentLocation
        /// </summary>
        /// <param name="row"></param>
        /// CommID      小区编号【必填】
        /// RegionalID   区域编号【必填】
        /// 返回：
        ///     LocationID   位置编号
        ///     LocationName   位置名称
        /// <returns></returns>
        private string GetIncidentLocation(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            if (!row.Table.Columns.Contains("RegionalID") || string.IsNullOrEmpty(row["RegionalID"].ToString()))
            {
                return JSONHelper.FromString(false, "区域编号不能为空");
            }

            IDbConnection con = new SqlConnection(PubConstant.hmWyglConnectionString);
            string sqlStr = " select LocationID,LocationName from  Tb_HSPR_IncidentLocation where CommID=@CommID and RegionalID=@RegionalID  and IsDelete=0";

            DataTable dt = con.ExecuteReader(sqlStr, new { CommID = row["CommID"], RegionalID = row["RegionalID"] }).ToDataSet().Tables[0];

            return JSONHelper.FromString(dt);
        }

        private string GetIncidentObject(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            if (!row.Table.Columns.Contains("LocationID") || string.IsNullOrEmpty(row["LocationID"].ToString()))
            {
                return JSONHelper.FromString(false, "区域编号不能为空");
            }

            IDbConnection con = new SqlConnection(PubConstant.hmWyglConnectionString);
            string sqlStr = " select ObjectID,ObjectName from  Tb_HSPR_IncidentObject where CommID=@CommID and RegionalID=@LocationID  and IsDelete=0";

            DataTable dt = con.ExecuteReader(sqlStr, new { CommID = row["CommID"], LocationID = row["LocationID"] }).ToDataSet().Tables[0];

            return JSONHelper.FromString(dt);
        }

        /// <summary>
        /// 获取公共区域  GetIncidentInfoListRegional
        /// 小区编码：CommID【必填】
        /// 返回：
        ///     RegionalID：公区ID
        ///     RegionalPlace：  公区名称
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string GetIncidentInfoListRegional(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编码不能为空");
            }

            IDbConnection con = new SqlConnection(PubConstant.hmWyglConnectionString);

            string sqlStr = " select RegionalID,RegionalPlace from  Tb_HSPR_IncidentRegional where CommID=@CommID and ISNULL(IsDelete,0)=0";

            DataTable dt = con.ExecuteReader(sqlStr, new { CommID = row["CommID"] }).ToDataSet().Tables[0];
            return JSONHelper.FromString(dt);
        }

        /// <summary>
        /// 获取报事类型  GetIncidentType
        /// 小区编码：CommID【必填】
        /// 报事区域:IncidentPlace 【业主权属or公共区域】
        /// 报事类型:ClassID【1：书面派工；2：口头派工】
        /// 返回：
        ///     TypeID          报事类型编号
        ///     TypeCode        报事编码【树型层级关联】
        ///     TypeName        报事类型名称
        ///     DealLimit       分派事限【户内】
        ///     DealLimit2      分派事限【公区】
        ///     IsTreeRoot      当值为2时，表示父级节点【树型层级关联】
        /// </summary>
        private string GetIncidentType(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编码不能为空");
            }

            string IncidentPlace = null;
            if (row.Table.Columns.Contains("IncidentPlace") && !string.IsNullOrEmpty(row["IncidentPlace"].ToString()))
            {
                IncidentPlace = row["IncidentPlace"].ToString();
            }

            string ClassID = null;
            if (row.Table.Columns.Contains("ClassID") && !string.IsNullOrEmpty(row["ClassID"].ToString()))
            {
                ClassID = row["ClassID"].ToString();
            }

            using (IDbConnection Connectionstr = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                string sql = $@"SELECT x.TypeID,x.TypeCode,x.TypeName,y.DealLimit,y.DealLimit2,y.IsTreeRoot 
				                FROM Tb_HSPR_IncidentType x LEFT JOIN Tb_HSPR_CorpIncidentType y ON x.TypeCode=y.TypeCode 
                                WHERE x.TypeCode IN
                                (
                                SELECT a.TypeCode FROM Tb_HSPR_IncidentType a WHERE a.CommID={row["CommID"]} AND ISNULL(a.IsEnabled, 0) = 0
                                InterSect
                                SELECT b.TypeCode FROM Tb_HSPR_CorpIncidentType b WHERE ISNULL(b.IsDelete,0)=0 AND isnull(b.IsEnabled, 0) = 0
                                ) AND x.CommID={row["CommID"]}  AND isnull(x.IsEnabled, 0) = 0 ";

                if (!string.IsNullOrEmpty(ClassID) && ClassID != "0")
                {
                    sql += $@" AND x.ClassID={ClassID}";
                }

                if (!string.IsNullOrEmpty(IncidentPlace))
                {
                    sql += $@" AND (x.IncidentPlace like '%{IncidentPlace}%' or isnull(x.IncidentPlace, '') = '')";
                }

                sql += $@" ORDER BY x.TypeCode,x.IsTreeRoot DESC";

                DataSet ds = Connectionstr.ExecuteReader(sql).ToDataSet();

                return JSONHelper.FromString(ds.Tables[0]);
            }
        }

        #endregion

        #region 查询物资
        /// <summary>
        /// 查询物资 QueryMaterial
        /// </summary>
        /// <param name="row"></param>
        /// CommID          小区编号
        /// QueryType       查询类型【Name or Num】必填
        /// WareHouseId      仓库ID
        /// WhereStr        条件
        /// 返回：
        ///     Value
        /// <returns></returns>
        private string QueryMaterial(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            if (!row.Table.Columns.Contains("QueryType") || string.IsNullOrEmpty(row["QueryType"].ToString()))
            {
                return JSONHelper.FromString(false, "查询类型不能为空");
            }
            if (!row.Table.Columns.Contains("WareHouseId") || string.IsNullOrEmpty(row["WareHouseId"].ToString()))
            {
                return JSONHelper.FromString(false, "仓库编码不能为空");
            }

            DataSet ds = null;
            IDbConnection con = new SqlConnection(PubConstant.hmWyglConnectionString);
            string whereStr = "";
            whereStr = whereStr + " where WareHouseId='" + row["WareHouseId"] + "'";
            if (row["QueryType"].ToString() == "Name" && row.Table.Columns.Contains("WhereStr"))
            {
                whereStr += " and MaterialName like '%" + row["WhereStr"].ToString() + "%'";

                ds = con.ExecuteReader("select [MaterialName] as [Value] from View_Tb_Mt_Store_Filter " + whereStr, null, null, null, CommandType.Text).ToDataSet();
            }
            if (row["QueryType"].ToString() == "Num" && row.Table.Columns.Contains("WhereStr"))
            {
                whereStr += " and Num like '%" + row["WhereStr"].ToString() + "%'";

                ds = con.ExecuteReader("select [Num] as [Value] from View_Tb_Mt_Store_Filter " + whereStr, null, null, null, CommandType.Text).ToDataSet();
            }


            return JSONHelper.FromString(ds.Tables[0]);

        }
        #endregion

        #region 查询仓库
        /// <summary>
        /// 查询仓库 QueryWareHouse
        /// </summary>
        /// <param name="row"></param>
        /// CommID          小区编号
        /// WhereStr        条件
        /// 返回：
        ///     Key
        ///     Value 
        /// <returns></returns>
        private string QueryWareHouse(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            string whereStr = "";
            if (row.Table.Columns.Contains("WhereStr"))
            {
                whereStr = " where isnull(IsDelete,0)=0 AND Name like '%" + row["WhereStr"].ToString() + "%'";
                whereStr = whereStr + " AND Id IN( SELECT WareHouseId FROM Tb_Mt_Permissions WHERE RoleCode IN (SELECT RoleCode FROM Tb_Sys_UserRole WHERE UserCode='" + Global_Var.LoginUserCode + "') )";
            }

            IDbConnection con = new SqlConnection(PubConstant.hmWyglConnectionString);
            DataSet ds = con.ExecuteReader("select [Id] as [Key],[Name] as [Value] from Tb_Mt_WareHouse " + whereStr, null, null, null, CommandType.Text).ToDataSet();
            return JSONHelper.FromString(ds.Tables[0]);
        }

        #endregion


        #region 物管


        #region 未分派工单、可抢工单、全部工单

        /// <summary>
        /// 未分派工单、可抢工单、全部工单GetUntreatedIncident
        /// 小区编码：CommID【必填】
        /// 查询类别：QueryType【查询类别，all:全部工单，其它值为 未分派和可抢工单】
        /// 是否派工：IsDispTYpe【0，未派工，其它值，派工】
        /// 返回值：
        ///     未分派、可抢工单：
        ///     IncidentID【报事编号】,IncidentPlace【报事区域】,ReserveDate【预约时间】,Phone【电话】,TypeName【报事类型】,RoomSign【房间编号】,RegionalPlace【公区区域】
        ///     全部工单：
        ///      CoordinateNum【派工单/协调单号】,IncidentID【报事编号】,IncidentPlace【报事区域】,RoomSign【房间编号】,TypeName【报事类型】,DealMan【处理人】,RegionalPlace【公共区域】,ReserveDate【预约时间】,Phone【电话】 
        /// </summary>
        /// <returns></returns>
        private string GetUntreatedIncident(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编码不能为空");
            }
            if (!row.Table.Columns.Contains("QueryType") || string.IsNullOrEmpty(row["QueryType"].ToString()))
            {
                return JSONHelper.FromString(false, "查询类别不能为空");
            }
            int PageIndex = 1;
            int PageSize = 20;

            //是否派工
            int IsDispTYpe = 0;

            if (row.Table.Columns.Contains("PageIndex") && AppGlobal.StrToInt(row["PageIndex"].ToString()) > 0)
            {
                PageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());
            }
            if (row.Table.Columns.Contains("PageSize") && AppGlobal.StrToInt(row["PageSize"].ToString()) > 0)
            {
                PageSize = AppGlobal.StrToInt(row["PageSize"].ToString());
            }
            if (row.Table.Columns.Contains("IsDispTYpe") && AppGlobal.StrToInt(row["IsDispTYpe"].ToString()) > 0)
            {
                IsDispTYpe = AppGlobal.StrToInt(row["IsDispTYpe"].ToString());
            }

            StringBuilder sb = new StringBuilder();


            if (Global_Var.LoginCorpID == "1970" && IsDispTYpe != 0)//泰禾
            {

                sb.Append($@"SELECT 
                        (CASE (SELECT COUNT(0) FROM Tb_HSPR_IncidentAcceptDeal 
                        WHERE IncidentID = x.IncidentID 
                        AND x.DispMan=x.DealMan
                        AND UserCode ={Global_Var.LoginUserCode})WHEN 1 THEN 1 ELSE 0 END)
                        AS IsSnatch, *
                            FROM view_HSPR_IncidentAccept_SearchFilter x");
            }
            else
            {
                sb.Append("select * from View_HSPR_Incidentseach_FIlter ");
            }

            sb.AppendFormat(" where  CommID ={0} ", DataSecurity.StrToLong(row["CommID"].ToString()));

            string FileName = "";

            if (IsDispTYpe == 0)
            {
                sb.Append(" AND ISNULL( DispType,0)=0 ");
            }
            else
            {
                sb.Append(" AND ISNULL( DispType,0)>0 ");

            }
            if (row["QueryType"].ToString() == "all")
            {
                FileName = " CoordinateNum,IncidentID,IncidentMode,IncidentContent,IncidentPlace,RoomSign,TypeName,DealMan,RegionalPlace,ReserveDate,Phone,IncidentNum ";
            }
            else
            {
                FileName = " IncidentID,IncidentMode,IncidentContent,IncidentPlace,ReserveDate,Phone,TypeName,RoomSign,RegionalPlace,IncidentNum ";
            }
            if (Global_Var.LoginCorpID == "1970")
            {
                FileName += ",IsSnatch ";
            }

            sb.Append(" AND ISNULL(DealState,0)=0 AND ISNULL(IsDelete,0)=0 AND IsStatistics=1");

            // 敏捷排除责任归属为“地产”的
            if (Global_Var.CorpId == "1971")
            {
                sb.Append(" AND ISNULL(IsException, 0)=0 AND Attribution<>'地产'");
            }



            int pageCount;
            int Counts;

            DataSet ds = BussinessCommon.GetList(out pageCount, out Counts, sb.ToString(), PageIndex, PageSize, "IncidentDate", 1, "IncidentID", PubConstant.hmWyglConnectionString, FileName);

            //DataTable dTable = IncidentAssignSearch("*", out pageCount, out Counts, sb.ToString(), PageIndex, PageSize, "IncidentDate", 1).Tables[0];

            return JSONHelper.FromString(ds.Tables[0]);
        }
        #endregion

        #region 查询本岗未分派工单


        /// <summary>
        /// 查询本岗未分派工单GetUntreatedDispatching
        /// </summary>
        /// <param name="row"></param>
        /// CommID          小区编码[必填]
        /// PageIndex       默认1
        /// PageSize        默认20
        /// 返回：
        ///      IncidentID【报事编号】,IncidentPlace【报事区域】,ReserveDate【预约时间】,Phone【电话】,TypeName【报事类型】,RoomSign【房间编号】,RegionalPlace【公区区域】
        /// <returns></returns>
        [Obsolete("使用GetCanAssignIncidentList方法代替")]
        private string GetUntreatedDispatching(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编码不能为空");
            }
            int PageIndex = 1;
            int PageSize = 20;
            if (row.Table.Columns.Contains("PageIndex"))
            {
                PageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());
            }

            if (row.Table.Columns.Contains("PageSize"))
            {
                PageSize = AppGlobal.StrToInt(row["PageSize"].ToString());
            }

            //判断是否有项目和模块权限
            bool b = CheckProjectJurisdiction(PubConstant.hmWyglConnectionString, row["CommID"].ToString());
            if (b)
            {
                //获取所有未派工单的报事类别              
                string str = GetIncidentIDS(PubConstant.hmWyglConnectionString, row["CommID"].ToString());
                if (str.Length > 0)
                {
                    str = " and IncidentID in(" + str.Substring(0, str.Length - 1) + ")";
                    //DataTable dtt = GetPostIncidentAcceptList(strcon, str, "CoordinateNum,IncidentID,IncidentPlace,RoomSign,TypeName,DealMan,ReserveDate,Phone,IncidentNum,RegionalPlace");

                    int PageCount;
                    int Counts;

                    string strsql = "select  CoordinateNum,IncidentID,IncidentPlace,RoomSign,TypeName,DealMan,ReserveDate,Phone,IncidentNum,RegionalPlace from view_HSPR_IncidentAccept_SearchFilter where ISNULL( DealState,0)= 0 AND ISNULL(IsDelete,0)=0 AND IsStatistics=1" + str;

                    if (Global_Var.CorpId == "1971")
                    {
                        strsql = "SELECT CoordinateNum,IncidentID,IncidentPlace,RoomSign,NC_NewRoomSign,TypeName,DealMan,ReserveDate,Phone,IncidentNum,RegionalPlace from view_HSPR_IncidentAccept_SearchFilter where ISNULL( DealState,0)= 0 AND ISNULL(IsDelete,0)=0 AND IsStatistics=1" + str;

                    }
                    DataSet ds = GetList(out PageCount, out Counts, strsql, PageIndex, PageSize, "IncidentNum", 1, "IncidentNum", PubConstant.hmWyglConnectionString);
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        return JSONHelper.FromString(ds.Tables[0]);
                    }
                    else
                    {
                        return JSONHelper.FromString(new DataTable());
                    }
                }
                else
                {
                    return JSONHelper.FromString(new DataTable());
                }
            }
            else
            {
                return JSONHelper.FromString(false, "没有项目或者模块权限");
            }
        }

        private string GetIncidentIDS(string strcon, string commid)
        {
            // 获取所有未派工单的报事类别
            DataTable dt = GetAllIncidentAcceptType(strcon, commid);
            StringBuilder sb = new StringBuilder();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {

                    List<string> list = GetAssignedPost(strcon, item["TypeID"].ToString(), commid);
                    //如果此类别有设置分派岗位
                    if (list.Count > 0)
                    {
                        foreach (string item1 in list)
                        {
                            List<string> li = GetUserRoleCode_AssignedPost(strcon, commid, Global_Var.UserCode, item1);
                            if (li.Count > 0)//如果当前登录人员有该岗位权限
                            {
                                //检测是否是户内报事，并且该房屋设置了管家，还必须具有楼栋权限
                                Tb_HSPR_IncidentAccept IncidentAccept = GetIncidentAcceptModel(strcon, item["IncidentID"].ToString());
                                if (IncidentAccept.IncidentPlace == "业主权属")
                                {
                                    //是否有楼栋管家
                                    List<string> l = GetHSPR_BuildHousekeeper(strcon, IncidentAccept.RoomID.ToString(), IncidentAccept.CommID.ToString());
                                    if (l.Count > 0)
                                    {
                                        foreach (string item_b in l)
                                        {
                                            //如果有，是否有权限
                                            if (item_b == Global_Var.UserCode)
                                            {
                                                sb.Append(item["IncidentID"] + ",");
                                                break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        sb.Append(item["IncidentID"] + ",");
                                    }


                                }
                                else//不是户内报事，添加到结果集中
                                {
                                    sb.Append(item["IncidentID"] + ",");
                                }


                                ////添加到结果集中
                                //sb.Append(item["IncidentID"] + ",");
                            }
                            //没有，就不处理
                        }
                    }
                    else
                    {
                        //检测是否是户内报事，并且该房屋设置了管家，还必须具有楼栋权限
                        Tb_HSPR_IncidentAccept IncidentAccept = GetIncidentAcceptModel(strcon, item["IncidentID"].ToString());
                        if (IncidentAccept.IncidentPlace == "业主权属")
                        {
                            //是否有楼栋管家
                            List<string> l = GetHSPR_BuildHousekeeper(strcon, IncidentAccept.RoomID.ToString(), IncidentAccept.CommID.ToString());
                            if (l.Count > 0)
                            {
                                foreach (string item_b in l)
                                {
                                    //如果有，是否有权限
                                    if (item_b == Global_Var.UserCode)
                                    {
                                        sb.Append(item["IncidentID"] + ",");
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                sb.Append(item["IncidentID"] + ",");
                            }


                        }
                        else//不是户内报事，添加到结果集中
                        {
                            sb.Append(item["IncidentID"] + ",");
                        }
                    }


                }
            }
            return sb.ToString();
        }

        #endregion

        #region 本人分派的工单
        /// <summary>
        /// 查询本人分派的工单 GetMineDispatching
        /// </summary>
        /// <param name="row"></param>
        /// CommID          小区编码[必填]
        /// UserCode        用户编号[必填]
        /// PageIndex       默认1
        /// PageSize        默认20
        /// 返回：
        ///      IncidentID【报事编号】,IncidentPlace【报事区域】,ReserveDate【预约时间】,Phone【电话】,TypeName【报事类型】,RoomSign【房间编号】,RegionalPlace【公区区域】
        private string GetMineDispatching(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编码不能为空");
            }

            string commId = row["CommID"].ToString();
            string userCode = Global_Var.UserCode;

            if (row.Table.Columns.Contains("UserCode") && !string.IsNullOrEmpty(row["UserCode"].ToString()))
            {
                userCode = row["UserCode"].ToString();
            }

            int PageIndex = 1;
            int PageSize = 20;
            if (row.Table.Columns.Contains("PageIndex"))
            {
                PageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());
            }

            if (row.Table.Columns.Contains("PageSize"))
            {

                PageSize = AppGlobal.StrToInt(row["PageSize"].ToString());
            }

            int PageCount;
            int Counts;

            string strsql = @"select CoordinateNum,IncidentID,IncidentContent,IncidentPlace,RoomSign,TypeName,
                                DealMan,ReserveDate,Phone,IncidentNum,RegionalPlace 
                            from view_HSPR_IncidentAccept_Filter where ISNULL(DealState,0)= 0 
                            AND ISNULL(IsDelete,0)=0 AND CommID=" + commId + " and DispUserCode='" + userCode + "'";

            if (Global_Var.CorpId == "1971")
            {
                strsql = @"select CoordinateNum,IncidentID,IncidentContent,IncidentPlace,RoomSign,TypeName,NC_NewRoomSign,
                                DealMan,ReserveDate,Phone,IncidentNum,RegionalPlace 
                            from view_HSPR_IncidentAccept_Filter where ISNULL(DealState,0)= 0 
                            AND ISNULL(IsDelete,0)=0 AND CommID=" + commId + " and DispUserCode='" + userCode + "'";
            }
            if (Global_Var.CorpId == "1973")
            {
                strsql = @"select CoordinateNum,IncidentID,IncidentContent,IncidentPlace,RoomName,RoomSign,TypeName,
                                DealMan,ReserveDate,Phone,IncidentNum,RegionalPlace 
                            from view_HSPR_IncidentAccept_Filter where ISNULL(DealState,0)= 0 
                            AND ISNULL(IsDelete,0)=0 AND CommID=" + commId + " and DispUserCode='" + userCode + "'";
            }
            DataSet ds = GetList(out PageCount, out Counts, strsql, PageIndex, PageSize, "IncidentNum", 1, "IncidentNum", PubConstant.hmWyglConnectionString);
            if (ds != null && ds.Tables.Count > 0)
            {
                return JSONHelper.FromString(ds.Tables[0]);
            }
            else
            {
                return JSONHelper.FromString(new DataTable());
            }
        }

        #endregion


        #region 查询本岗未分派工单数


        /// <summary>
        /// 查询本岗未分派工单数GetUntreatedDispatching_Num
        /// </summary>
        /// <param name="row"></param>
        /// CommID              小区编码
        /// true:  值
        /// <returns></returns>
        [Obsolete("使用GetCanAssignIncidentCount方法代替")]
        private string GetUntreatedDispatching_Num(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编码不能为空");
            }

            //判断是否有项目和模块权限
            bool b = CheckProjectJurisdiction(PubConstant.hmWyglConnectionString, row["CommID"].ToString());
            if (b)
            {
                //获取所有未派工单的报事类别              
                string str = GetIncidentIDS(PubConstant.hmWyglConnectionString, row["CommID"].ToString());
                if (str.Length > 0)
                {
                    str = " and IncidentID in(" + str.Substring(0, str.Length - 1) + ")";
                    DataTable dtt = GetPostIncidentAcceptList(PubConstant.hmWyglConnectionString, str, "IncidentID");

                    return JSONHelper.FromString(true, dtt.Rows.Count.ToString());
                }
                else
                {
                    return JSONHelper.FromString(true, "0");
                }
            }
            else
            {
                return JSONHelper.FromString(true, "0");
            }
        }
        #endregion

        #region 检测是否为本岗未分派工单


        /// <summary>
        /// 判断是否是本岗未分派工单CheckIncidentID
        /// </summary>
        /// <param name="row"></param>
        /// IncidentID      报事编码[必填]
        /// CommID          小区编码[必填]
        /// 返回：true    0本岗 ；1非本岗
        /// <returns></returns>
        [Obsolete("使用CanAssign方法代替")]
        private string CheckIncidentID(DataRow row)
        {
            string result = CanAssign(row);

            if (result.Contains("1"))
            {
                return result.Replace('1', '0');
            }

            if (result.Contains("0"))
            {
                return result.Replace('0', '1');
            }

            return result;

            //if (!row.Table.Columns.Contains("IncidentID") || string.IsNullOrEmpty(row["IncidentID"].ToString()))
            //{
            //    return JSONHelper.FromString(false, "报事编码不能为空");
            //}
            //if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            //{
            //    return JSONHelper.FromString(false, "小区编码不能为空");
            //}

            //string commid = row["CommID"].ToString();
            //string incidentID = row["IncidentID"].ToString();

            //string strcon = "";
            //bool bl = GetDBServerPath(row["CommID"].ToString(), out strcon);
            //if (bl == false)
            //{
            //    return JSONHelper.FromString(false, strcon);
            //}

            //// 读取报事信息
            //Tb_HSPR_IncidentAccept model = GetIncidentAcceptModel(strcon, incidentID);

            //List<string> list = GetAssignedPost(strcon, model.TypeID, commid);
            ////如果此类别有设置分派岗位
            //if (list.Count > 0)
            //{
            //    foreach (string item1 in list)
            //    {
            //        List<string> li = GetUserRoleCode_AssignedPost(strcon, commid, Global_Var.UserCode, item1);
            //        if (li.Count > 0)//如果当前登录人员有该岗位权限
            //        {
            //            //检测是否是户内报事，并且该房屋设置了管家，还必须具有楼栋权限
            //            Tb_HSPR_IncidentAccept IncidentAccept = GetIncidentAcceptModel(strcon, incidentID);
            //            if (IncidentAccept.IncidentPlace == "业主权属")
            //            {
            //                //是否有楼栋管家
            //                List<string> l = GetHSPR_BuildHousekeeper(strcon, IncidentAccept.RoomID.ToString(), IncidentAccept.CommID.ToString());
            //                if (l.Count > 0)
            //                {
            //                    foreach (string item_b in l)
            //                    {
            //                        //如果有，是否有权限
            //                        if (item_b == Global_Var.UserCode)
            //                        {
            //                            return JSONHelper.FromString(true, "0");
            //                        }
            //                    }
            //                    return JSONHelper.FromString(true, "1");
            //                }
            //                else
            //                {
            //                    return JSONHelper.FromString(true, "0");
            //                }
            //            }
            //            else//不是户内报事，添加到结果集中
            //            {
            //                return JSONHelper.FromString(true, "0");
            //            }
            //        }
            //    }

            //    //没有，就不处理
            //    return JSONHelper.FromString(true, "1");
            //}
            //else
            //{
            //    //检测是否是户内报事，并且该房屋设置了管家，还必须具有楼栋权限
            //    Tb_HSPR_IncidentAccept IncidentAccept = GetIncidentAcceptModel(strcon, incidentID);
            //    if (IncidentAccept.IncidentPlace == "业主权属")
            //    {
            //        //是否有楼栋管家
            //        List<string> l = GetHSPR_BuildHousekeeper(strcon, IncidentAccept.RoomID.ToString(), IncidentAccept.CommID.ToString());
            //        if (l.Count > 0)
            //        {
            //            foreach (string item_b in l)
            //            {
            //                //如果有，是否有权限
            //                if (item_b == Global_Var.UserCode)
            //                {
            //                    return JSONHelper.FromString(true, "0");
            //                }
            //            }
            //            return JSONHelper.FromString(true, "1");
            //        }
            //        else
            //        {
            //            return JSONHelper.FromString(true, "0");
            //        }
            //    }
            //    else//不是户内报事，添加到结果集中
            //    {
            //        return JSONHelper.FromString(true, "0");
            //    }
            //}

            //string str = GetIncidentIDS(strcon, row["CommID"].ToString());
            //string[] s = str.Split(',');
            //int i = 1;
            //foreach (string item in s)
            //{
            //    if (item == row["IncidentID"].ToString())
            //    {
            //        i = 0;
            //    }
            //}
            //return JSONHelper.FromString(true, i.ToString());

        }
        #endregion

        #region 查询本岗可抢工单
        /// <summary>
        /// 查询本岗可抢工单GetUntreatedRobWorkOrder
        /// </summary>
        /// <param name="row"></param>
        /// CommID          小区编码[必填]
        /// PageIndex       默认1
        /// PageSize        默认20
        /// 返回：
        ///      IncidentID【报事编号】,IncidentPlace【报事区域】,ReserveDate【预约时间】,Phone【电话】,TypeName【报事类型】,RoomSign【房间编号】,RegionalPlace【公区区域】
        /// <returns></returns>
        [Obsolete("使用GetCanSnatchIncidentList方法代替")]
        private string GetUntreatedRobWorkOrder(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编码不能为空");
            }
            int PageIndex = 1;
            int PageSize = 20;
            if (row.Table.Columns.Contains("PageIndex"))
            {
                PageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());
            }

            if (row.Table.Columns.Contains("PageSize"))
            {
                PageSize = AppGlobal.StrToInt(row["PageSize"].ToString());
            }

            //判断是否有项目和模块权限
            bool b = CheckProjectJurisdiction(PubConstant.hmWyglConnectionString, row["CommID"].ToString());
            if (b)
            {
                //获取所有未派工单的报事类别
                DataTable dt = GetAllIncidentAcceptType(PubConstant.hmWyglConnectionString, row["CommID"].ToString());
                StringBuilder sb = new StringBuilder();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        //处理岗位
                        List<string> list = GetAssignmenRoleCode(PubConstant.hmWyglConnectionString, item["TypeID"].ToString(), row["CommID"].ToString());
                        ////分派岗位
                        //List<string> list_ass = GetAssignedPost(strcon, item["TypeID"].ToString(), row["CommID"].ToString());


                        if (list.Count > 0)
                        {
                            foreach (string item1 in list)
                            {
                                List<string> li = GetUserRoleCode_ProcessPost(PubConstant.hmWyglConnectionString, row["CommID"].ToString(), Global_Var.UserCode, item1);
                                if (li.Count > 0)//如果当前登录人员有该岗位权限
                                {
                                    //添加到结果集中
                                    sb.Append(item["IncidentID"] + ",");
                                }
                                //没有，就不处理
                            }
                        }
                        else
                        {
                            //添加到结果集中
                            sb.Append(item["IncidentID"] + ",");
                        }
                    }
                }

                string str = sb.ToString();
                if (str.Length > 0)
                {
                    str = " and IncidentID in(" + str.Substring(0, str.Length - 1) + ")";
                    //DataTable dtt = GetPostIncidentAcceptList(strcon, str, "CoordinateNum,IncidentID,IncidentPlace,RoomSign,TypeName,DealMan,ReserveDate,IncidentNum,RegionalPlace,RegionalName,Phone");


                    int PageCount;
                    int Counts;

                    string strsql = "select  CoordinateNum,IncidentID,IncidentPlace,RoomSign,TypeName,DealMan,ReserveDate," +
                        "IncidentNum,RegionalPlace,RegionalName,Phone " +
                        "from view_HSPR_IncidentAccept_SearchFilter where ISNULL( DealState,0)= 0 AND ISNULL(IsDelete,0)=0 " + str;
                    if (Global_Var.LoginCorpID == "1970")//泰禾需求 返回非退单过的报事
                    {
                        strsql = "select  CoordinateNum,IncidentID,IncidentPlace,RoomSign,TypeName,DealMan,ReserveDate," +
                            "IncidentNum,RegionalPlace,RegionalName,Phone " +
                            "from view_HSPR_IncidentAccept_SearchFilter where ISNULL( DealState,0)= 0 AND ISNULL(IsDelete,0)=0" + str;

                    }
                    DataSet ds = GetList(out PageCount, out Counts, strsql, PageIndex, PageSize, "IncidentNum", 1, "IncidentNum", PubConstant.hmWyglConnectionString);
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        return JSONHelper.FromString(ds.Tables[0]);
                    }
                    else
                    {
                        return JSONHelper.FromString(new DataTable());
                    }

                }
                else
                {
                    return JSONHelper.FromString(new DataTable());
                }
            }
            else
            {
                return JSONHelper.FromString(false, "没有项目或者模块权限");
            }

        }

        #endregion

        #region 本岗可抢工单数GetUntreatedRobWorkOrder_Num
        /// <summary>
        /// 本岗可抢工单数GetUntreatedRobWorkOrder_Num
        /// </summary>
        /// <param name="row"></param>
        /// CommID                      小区编码
        /// true:值
        /// <returns></returns>
        [Obsolete("使用GetCanSnatchIncidentCount方法代替")]
        private string GetUntreatedRobWorkOrder_Num(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编码不能为空");
            }

            //判断是否有项目和模块权限
            bool b = CheckProjectJurisdiction(PubConstant.hmWyglConnectionString, row["CommID"].ToString());
            if (b)
            {
                //获取所有未派工单的报事类别
                DataTable dt = GetAllIncidentAcceptType(PubConstant.hmWyglConnectionString, row["CommID"].ToString());
                StringBuilder sb = new StringBuilder();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {

                        List<string> list = GetAssignmenRoleCode(PubConstant.hmWyglConnectionString, item["TypeID"].ToString(), row["CommID"].ToString());
                        //如果此类别有设置处理岗位
                        if (list.Count > 0)
                        {
                            foreach (string item1 in list)
                            {
                                List<string> li = GetUserRoleCode_ProcessPost(PubConstant.hmWyglConnectionString, row["CommID"].ToString(), Global_Var.UserCode, item1);
                                if (li.Count > 0)//如果当前登录人员有该岗位权限
                                {
                                    //添加到结果集中
                                    sb.Append(item["IncidentID"] + ",");
                                }
                                //没有，就不处理
                            }
                        }
                        else
                        {
                            //添加到结果集中
                            sb.Append(item["IncidentID"] + ",");
                        }
                    }
                }

                string str = sb.ToString();
                if (str.Length > 0)
                {
                    str = " and IncidentID in(" + str.Substring(0, str.Length - 1) + ")";
                    DataTable dtt = GetPostIncidentAcceptList(PubConstant.hmWyglConnectionString, str, "IncidentID");
                    return JSONHelper.FromString(true, dtt.Rows.Count.ToString());

                }
                else
                {
                    return JSONHelper.FromString(true, "0");
                }
            }
            else
            {
                return JSONHelper.FromString(true, "0");
            }
        }
        #endregion


        #region 检查本岗可抢工单

        /// <summary>
        ///  检查本岗可抢工单CheckUntreatedRobWorkOrder
        /// </summary>
        /// <param name="row"></param>
        /// IncidentID      报事编码[必填]
        /// CommID          小区编码[必填]
        /// 返回：true    0本岗 ；1非本岗
        /// <returns></returns>
        [Obsolete("使用CanSnatch方法代替")]
        private string CheckUntreatedRobWorkOrder(DataRow row)
        {
            string result = CanSnatch(row);

            if (result.Contains("1"))
            {
                return result.Replace('1', '0');
            }

            if (result.Contains("0"))
            {
                return result.Replace('0', '1');
            }

            return result;
            //if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            //{
            //    return JSONHelper.FromString(false, "小区编码不能为空");
            //}
            //if (!row.Table.Columns.Contains("IncidentID") || string.IsNullOrEmpty(row["IncidentID"].ToString()))
            //{
            //    return JSONHelper.FromString(false, "报事编码不能为空");
            //}
            //string strcon = "";
            //bool bl = GetDBServerPath(row["CommID"].ToString(), out strcon);
            //if (bl == false)
            //{
            //    return JSONHelper.FromString(false, strcon);
            //}
            ////获取所有未派工单的报事类别
            //DataTable dt = GetAllIncidentAcceptType(strcon, row["CommID"].ToString());
            //StringBuilder sb = new StringBuilder();
            //if (dt.Rows.Count > 0)
            //{
            //    foreach (DataRow item in dt.Rows)
            //    {
            //        List<string> list = GetAssignmenRoleCode(strcon, item["TypeID"].ToString(), row["CommID"].ToString());
            //        //如果此类别有设置处理岗位
            //        if (list.Count > 0)
            //        {
            //            foreach (string item1 in list)
            //            {
            //                List<string> li = GetUserRoleCode_ProcessPost(strcon, row["CommID"].ToString(), Global_Var.UserCode, item1);
            //                if (li.Count > 0)//如果当前登录人员有该岗位权限
            //                {
            //                    //添加到结果集中
            //                    sb.Append(item["IncidentID"] + ",");
            //                }
            //                //没有，就不处理
            //            }
            //        }
            //        else
            //        {
            //            //添加到结果集中
            //            sb.Append(item["IncidentID"] + ",");
            //        }
            //    }
            //}

            //string str = sb.ToString();
            //string[] s = str.Split(',');
            //int i = 1;
            //foreach (string item in s)
            //{
            //    if (item == row["IncidentID"].ToString())
            //    {
            //        i = 0;
            //    }
            //}
            //return JSONHelper.FromString(true, i.ToString());

        }

        #endregion

        #region 查询本人全部工单未完结工单
        /// <summary>
        /// 查询本人全部工单未完结工单GetUntreatedHandleWorkOrder
        /// </summary>
        /// <param name="row"></param>
        /// CommID          小区编码[必填]
        /// PageIndex       默认1
        /// PageSize        默认20
        /// 返回：
        ///      CoordinateNum【派工单/协调单号】,IncidentID【报事编号】,IncidentPlace【报事区域】,RoomSign【房间编号】,TypeName【报事类型】,DealMan【处理人】,RegionalPlace【公区区域】
        /// <returns></returns>
        private string GetUntreatedHandleWorkOrder(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编码不能为空");
            }
            int PageIndex = 1;
            int PageSize = 20;
            if (row.Table.Columns.Contains("PageIndex"))
            {
                PageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());
            }

            if (row.Table.Columns.Contains("PageSize"))
            {
                PageSize = AppGlobal.StrToInt(row["PageSize"].ToString());
            }

            string str = $@"SELECT DISTINCT IncidentID, CoordinateNum,IncidentPlace,
                            IncidentContent,ReserveDate,Phone,TypeName,RoomSign,RegionalPlace,IncidentNum,DealMan 
                            FROM view_HSPR_IncidentAccept_SearchFilter WHERE IncidentID IN
                            (
                            SELECT IncidentID FROM Tb_HSPR_IncidentAccept WHERE IncidentID IN(SELECT IncidentID FROM Tb_HSPR_IncidentAcceptDeal
                                WHERE UserCode='{Global_Var.LoginUserCode}' AND CommID={row["CommID"]}) AND ISNULL(IsDelete, 0) = 0
                                AND ISNULL(DispType, 0)>0 AND ISNULL(DealState, 0)=0
                            UNION
                            SELECT IncidentID FROM Tb_HSPR_IncidentAccept WHERE DealMan LIKE '%{Global_Var.LoginUserName}%' AND ISNULL(IsDelete, 0) = 0
                                AND ISNULL(DispType, 0)>0 AND ISNULL(DealState, 0)=0 AND CommID={row["CommID"]}
                            )";
            if (Global_Var.LoginCorpID=="1973" || Global_Var.LoginCorpID == "1974")//鸿坤多返回了RoomName
            {
                 str = $@"SELECT DISTINCT IncidentID, CoordinateNum,IncidentPlace,
                            IncidentContent,ReserveDate,Phone,TypeName,RoomSign,RoomName,RegionalPlace,IncidentNum,DealMan 
                            FROM view_HSPR_IncidentAccept_SearchFilter WHERE IncidentID IN
                            (
                            SELECT IncidentID FROM Tb_HSPR_IncidentAccept WHERE IncidentID IN(SELECT IncidentID FROM Tb_HSPR_IncidentAcceptDeal
                                WHERE UserCode='{Global_Var.LoginUserCode}' AND CommID={row["CommID"]}) AND ISNULL(IsDelete, 0) = 0
                                AND ISNULL(DispType, 0)>0 AND ISNULL(DealState, 0)=0
                            UNION
                            SELECT IncidentID FROM Tb_HSPR_IncidentAccept WHERE DealMan LIKE '%{Global_Var.LoginUserName}%' AND ISNULL(IsDelete, 0) = 0
                                AND ISNULL(DispType, 0)>0 AND ISNULL(DealState, 0)=0 AND CommID={row["CommID"]}
                            )";
            }
            if (Global_Var.LoginCorpID == "1970")//泰禾
            {
                str = $@"SELECT DISTINCT x.IncidentID,x.IncidentPlace,x.IncidentContent,
                            x.ReserveDate,x.Phone,x.TypeName,x.RoomSign,x.RegionalPlace,x.IncidentNum,x.DealMan,
                            (CASE (SELECT COUNT(0) FROM Tb_HSPR_IncidentAcceptDeal WHERE IncidentID = x.IncidentID AND x.DispMan=x.DealMan
                            AND UserCode ='{Global_Var.LoginUserCode}')WHEN 1 THEN 1 ELSE 0 END)
                            AS IsSnatch,
                            CASE substring(isnull(CoordinateNum,'00'),0,2) 
                            WHEN 'P' THEN CoordinateNum+'（派工单）' 
                            WHEN 'X' THEN CoordinateNum+'（协调单）' 
                            WHEN 'K' THEN CoordinateNum+'（口派单）' 
                            ELSE CoordinateNum END AS CoordinateNum 
                        FROM view_HSPR_IncidentAccept_SearchFilter x WHERE IncidentID IN
                        (
                        SELECT IncidentID FROM Tb_HSPR_IncidentAccept WHERE IncidentID IN(SELECT IncidentID FROM Tb_HSPR_IncidentAcceptDeal
                            WHERE UserCode='{Global_Var.LoginUserCode}' AND CommID={row["CommID"]}) AND ISNULL(IsDelete, 0) = 0
                            AND ISNULL(DispType, 0)>0 AND ISNULL(DealState, 0)=0
                        UNION
                        SELECT IncidentID FROM Tb_HSPR_IncidentAccept WHERE DealMan LIKE '%{Global_Var.LoginUserName}%' AND ISNULL(IsDelete, 0) = 0
                            AND ISNULL(DispType, 0)>0 AND ISNULL(DealState, 0)=0 AND CommID={row["CommID"]}
                        )";

            }
            if (Global_Var.CorpId == "1971")
            {
                str += " AND ISNULL(IsException, 0)=0 AND Attribution<>'地产'";
            }
            

            int PageCount;
            int Counts;

            DataSet ds = GetList(out PageCount, out Counts, str, PageIndex, PageSize, "IncidentNum", 1, "IncidentID", PubConstant.hmWyglConnectionString);
            if (ds != null && ds.Tables.Count > 0)
            {
                return JSONHelper.FromString(ds.Tables[0]);
            }
            else
            {
                return JSONHelper.FromString(new DataTable());
            }
        }
        #endregion

        #region 查询本人全部工单未完结工单数
        /// <summary>
        /// 查询本人全部工单未完结工单数GetUntreatedHandleWorkOrder_Num
        /// </summary>
        /// <param name="row"></param>
        /// CommID          小区编码[必填]    
        /// 返回：
        ///     true   值
        /// <returns></returns>
        private string GetUntreatedHandleWorkOrder_Num(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编码不能为空");
            }

            string str = $@"SELECT count(*) 
                            FROM Tb_HSPR_IncidentAccept WHERE IncidentID IN
                            (
                            SELECT IncidentID FROM Tb_HSPR_IncidentAccept WHERE IncidentID IN(SELECT IncidentID FROM Tb_HSPR_IncidentAcceptDeal
                                WHERE UserCode='{Global_Var.LoginUserCode}' AND CommID={row["CommID"]}) AND ISNULL(IsDelete, 0) = 0
                                AND ISNULL(DispType, 0)>0 AND ISNULL(DealState, 0)=0
                            UNION
                            SELECT IncidentID FROM Tb_HSPR_IncidentAccept WHERE DealMan LIKE '%{Global_Var.LoginUserName}%' AND ISNULL(IsDelete, 0) = 0
                                AND ISNULL(DispType, 0)>0 AND ISNULL(DealState, 0)=0 AND CommID={row["CommID"]}
                            )";

            if (Global_Var.CorpId == "1971")
            {
                str += " AND ISNULL(IsException, 0) = 0 AND Attribution<>'地产'";
            }
            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                int count = conn.Query<int>(str).FirstOrDefault();

                return JSONHelper.FromString(true, count.ToString());
            }
        }
        #endregion


        #region 检查本岗未完结工单
        /// <summary>
        /// 检查本岗未完结工单CheckUntreatedHandleWorkOrder
        /// </summary>
        /// <param name="row"></param>
        /// IncidentID      报事编码[必填]
        /// CommID          小区编码[必填]
        /// 返回：true    0本岗 ；1非本岗
        /// <returns></returns>
        private string CheckUntreatedHandleWorkOrder(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编码不能为空");
            }
            if (!row.Table.Columns.Contains("IncidentID") || string.IsNullOrEmpty(row["IncidentID"].ToString()))
            {
                return JSONHelper.FromString(false, "报事编码不能为空");
            }


            string str = $@"SELECT IncidentID FROM Tb_HSPR_IncidentAccept WHERE IncidentID IN(SELECT IncidentID FROM Tb_HSPR_IncidentAcceptDeal 
                            WHERE UserCode=@UserCode AND CommID=@CommID AND ISNULL(DealState, 0)=0) AND ISNULL(IsDelete, 0)=0 AND ISNULL(DispType, 0)>0
                            UNION
                            SELECT IncidentID FROM Tb_HSPR_IncidentAccept WHERE DealMan LIKE '%{Global_Var.LoginUserName}%' AND ISNULL(IsDelete, 0)=0
                            AND ISNULL(DispType, 0)>0 AND ISNULL(DealState, 0)=0";

            // 敏捷排除责任归属为“地产”的
            if (Global_Var.CorpId == "1971")
            {
                str += " AND ISNULL(IsException, 0)=0 AND Attribution<>'地产'";
            }

            using (IDbConnection con = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                List<string> IncidentList = con.Query<string>(str, new { UserCode = Global_Var.LoginUserCode, CommID = row["CommID"].ToString(), }).ToList();
                if (IncidentList.Contains(row["IncidentID"].ToString()))
                {
                    return new ApiResult(true, "0").toJson();
                }
                return new ApiResult(true, "1").toJson();
            }
        }


        #endregion


        #region 本岗相关


        /// <summary>
        /// 判断模块权限
        /// </summary>
        /// <param name="conStr"></param>
        /// <param name="CommID"></param>
        /// <returns></returns>
        private bool CheckProjectJurisdiction(string conStr, string CommID)
        {
            IDbConnection con = new SqlConnection(conStr);
            string sql = "select UserCode from view_Sys_User_RoleData_Filter2 where RoleCode in( select RoleCode from Tb_Sys_RolePope where PNodeCode = '0109060303') and CommID = '" + CommID + "'  and UserCode  ='" + Global_Var.UserCode + "'";

            if (con.Query(sql).Count() > 0)
            {
                return true;
            }

            // 判断通用岗位权限，通用岗位不挂小区
            sql = @"SELECT * FROM Tb_Sys_RolePope WHERE PNodeCode = '0109060303'
  AND RoleCode in (SELECT SysRoleCode FROM Tb_Sys_Role WHERE RoleCode in (SELECT RoleCode FROM Tb_Sys_UserRole WHERE usercode='" + Global_Var.UserCode + "'))";

            if (con.Query(sql).Count() > 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 获取所有未派工单
        /// </summary>
        /// <param name="conStr"></param>
        /// <param name="CommID"></param>
        /// <returns></returns>
        private DataTable GetAllDispatching(string conStr, string CommID)
        {
            IDbConnection con = new SqlConnection(conStr);
            string sql = "SELECT IncidentID,IncidentPlace,ReserveDate,Phone,TypeName,RoomSign,RegionalPlace,CommID,CustID,RoomID,TypeID FROM view_HSPR_IncidentAccept_Filter WHERE CommID='" + CommID + "' AND ISNULL( DispType,0)=0  AND ISNULL( DealState,0)= 0  AND ISNULL(IsDelete,0)=0 ";

            // 敏捷排除责任归属为“地产”的
            if (Global_Var.CorpId == "1971")
            {
                sql += " AND ISNULL(IsException, 0)=0 AND Attribution<>'地产'";
            }

            DataTable dt = con.ExecuteReader(sql, null, null, null, CommandType.Text).ToDataSet().Tables[0];
            con.Dispose();
            return dt;
        }

        /// <summary>
        /// 检测是否设置了分派岗位并且有该岗位权限
        /// </summary>
        /// <param name="conStr"></param>
        /// <param name="TypeID"></param>
        /// <returns></returns>
        private bool CheckAssignmentJurisdiction(string conStr, string TypeID, string commID)
        {
            IDbConnection con = new SqlConnection(conStr);
            string sql = "select RoleCode from Tb_HSPR_IncidentTypeAssignedPost where CorpTypeID in (select CorpTypeID from Tb_HSPR_IncidentType where TypeID in ( select Convert(nvarchar(50), ColName) from dbo.funSplitTabel('" + TypeID + "', ','))) INTERSECT select RoleCode from view_Sys_User_RoleData_Filter2 where CommID='" + commID + "'  and UserCode ='" + Global_Var.UserCode + "'  ";
            DataTable dt = con.ExecuteReader(sql, null, null, null, CommandType.Text).ToDataSet().Tables[0];
            con.Dispose();
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 获取类别分派权限
        /// </summary>
        /// <param name="conStr"></param>
        /// <param name="TypeID"></param>
        /// <param name="commID"></param>
        /// <returns></returns>
        private List<string> GetAssignedPost(string conStr, string TypeID, string commID)
        {
            IDbConnection con = new SqlConnection(conStr);

            string sql = "select RoleCode from Tb_HSPR_IncidentTypeAssignedPost where CorpTypeID in (select CorpTypeID from Tb_HSPR_IncidentType where TypeID in ( select Convert(nvarchar(50), ColName) from dbo.funSplitTabel('" + TypeID + "', ','))) ";
            List<string> list = con.Query<string>(sql).ToList<string>();

            con.Dispose();
            return list;
        }



        /// <summary>
        /// 检测是否设置了报事类别处理权限并且有该岗位权限
        /// </summary>
        /// <param name="conStr"></param>
        /// <param name="TypeID"></param>
        /// <param name="commID"></param>
        /// <returns></returns>
        private bool CheckAssignmentHandle(string conStr, string TypeID, string commID)
        {
            IDbConnection con = new SqlConnection(conStr);
            string sql = "select RoleCode from Tb_HSPR_IncidentTypeProcessPost where CorpTypeID in (select CorpTypeID from Tb_HSPR_IncidentType where TypeID in ( select Convert(nvarchar(50), ColName) from dbo.funSplitTabel('" + TypeID + "', ','))) INTERSECT select RoleCode from view_Sys_User_RoleData_Filter2 where CommID='" + commID + "'  and UserCode ='" + Global_Var.UserCode + "'  ";
            DataTable dt = con.ExecuteReader(sql, null, null, null, CommandType.Text).ToDataSet().Tables[0];
            con.Dispose();
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获取报事类别的处理角色权限
        /// </summary>
        /// <param name="conStr"></param>
        /// <param name="TypeID"></param>
        /// <param name="commId"></param>
        /// <returns></returns>
        private List<string> GetAssignmenRoleCode(string conStr, string TypeID, string commId)
        {
            IDbConnection con = new SqlConnection(conStr);
            string sql = "select RoleCode from Tb_HSPR_IncidentTypeProcessPost where CorpTypeID in (select CorpTypeID from Tb_HSPR_IncidentType where TypeID in ( select Convert(nvarchar(50), ColName) from dbo.funSplitTabel('" + TypeID + "', ',')))";
            List<string> list = con.Query<string>(sql, null, null, true, null, CommandType.Text).ToList<string>();
            con.Dispose();
            return list;

        }


        /// <summary>
        /// 获取指定用户的处理角色
        /// </summary>
        /// <param name="conStr"></param>
        /// <param name="commID"></param>
        /// <param name="UserCode"></param>
        /// <returns></returns>
        private List<string> GetUserRoleCode_ProcessPost(string conStr, string commID, string UserCode, string RoleCode)
        {
            IDbConnection con = new SqlConnection(conStr);
            string sql = "select RoleCode from View_HSPR_IncidentTypeProcessPostUser where CommID='" + commID + "'  and UserCode ='" + UserCode + "' and RoleCode='" + RoleCode + "'";
            List<string> list = con.Query<string>(sql, null, null, true, null, CommandType.Text).ToList<string>();
            con.Dispose();
            return list;
        }


        /// <summary>
        /// 获取指定用户的分派角色
        /// </summary>
        /// <param name="conStr"></param>
        /// <param name="commID"></param>
        /// <param name="UserCode"></param>
        /// <returns></returns>
        private List<string> GetUserRoleCode_AssignedPost(string conStr, string commID, string UserCode, string RoleCode)
        {
            IDbConnection con = new SqlConnection(conStr);
            string sql = "select RoleCode from View_HSPR_IncidentTypeAssignedPostUser where CommID='" + commID + "'  and UserCode ='" + UserCode + "' and RoleCode='" + RoleCode + "'";
            List<string> list = con.Query<string>(sql, null, null, true, null, CommandType.Text).ToList<string>();
            con.Dispose();
            return list;
        }

        /// <summary>
        /// 获取报事明细
        /// </summary>
        /// <param name="conStr"></param>
        /// <param name="IncidentID"></param>
        /// <returns></returns>
        private Tb_HSPR_IncidentAccept GetIncidentAcceptModel(string conStr, string IncidentID)
        {
            IDbConnection con = new SqlConnection(conStr);
            string sql = "select * from Tb_HSPR_IncidentAccept where IncidentID=@IncidentID";
            Tb_HSPR_IncidentAccept model = con.Query<Tb_HSPR_IncidentAccept>(sql, new { IncidentID = IncidentID }).SingleOrDefault<Tb_HSPR_IncidentAccept>();
            con.Dispose();
            return model;
        }

        /// <summary>
        /// 查询是否具设置有楼栋管家
        /// </summary>
        /// <param name="conStr"></param>
        /// <param name="RoomID"></param>
        /// <param name="Commid"></param>
        /// <returns></returns>
        private List<string> GetHSPR_BuildHousekeeper(string conStr, string RoomID, string Commid)
        {
            IDbConnection con = new SqlConnection(conStr);
            string sql = "select UserCode from Tb_HSPR_BuildHousekeeper where CommID ='" + Commid + "' and BuildSNum in (select BuildSNum from Tb_HSPR_Room where CommID = '" + Commid + "' and RoomID ='" + RoomID + "' and Isdelete = 0 ) ";
            List<string> model = con.Query<string>(sql).ToList<string>();

            return model;
        }

        /// <summary>
        /// 查询是否有楼栋管理权限
        /// </summary>
        /// <param name="conStr"></param>
        /// <param name="RoomID"></param>
        /// <param name="Commid"></param>
        /// <returns></returns>
        private bool GetHSPR_BuildHousekeeperJurisdiction(string conStr, string RoomID, string Commid)
        {
            IDbConnection con = new SqlConnection(conStr);
            string sql = "select MobileTel from view_Sys_User_RoleData_Filter2 where  CommID = " + Commid + " and IsMobile = 1   and UserCode in (select UserCode from view_Sys_User_RoleData_Filter2 where RoleCode in( select RoleCode from Tb_Sys_RolePope where PNodeCode = '0109060303') and CommID = " + Commid + " and IsMobile = 1 ) and UserCode in (select UserCode from Tb_HSPR_BuildHousekeeper where CommID = " + Commid + " and BuildSNum in (select BuildSNum from Tb_HSPR_Room where CommID = " + Commid + " and RoomID =" + RoomID + " and Isdelete = 0  ))   ";
            List<string> model = con.Query<string>(sql).ToList<string>();
            con.Dispose();
            if (model.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 查询指定报事信息
        /// </summary>
        /// <param name="conStr"></param>
        /// <param name="whereStr">条件</param>
        /// <param name="FidelName">查询列</param>
        /// <returns></returns>
        private DataTable GetPostIncidentAcceptList(string conStr, string whereStr, string FidelName)
        {

            IDbConnection con = new SqlConnection(conStr);
            string sql = "select  row_number() over (order by IncidentID) as Number," + FidelName + " from view_HSPR_IncidentAccept_SearchFilter where ISNULL( DealState,0)= 0 AND ISNULL(IsDelete,0)=0  AND IsStatistics=1" + whereStr + "  order by IncidentNum desc";
            DataTable dt = con.ExecuteReader(sql, null, null, null, CommandType.Text).ToDataSet().Tables[0];
            con.Dispose();
            return dt;
        }

        /// <summary>
        /// 获取所有未派工单的报事类别
        /// </summary>
        /// <param name="conStr"></param>
        /// <param name="CommID"></param>
        /// <returns></returns>
        private DataTable GetAllIncidentAcceptType(string conStr, string CommID)
        {
            IDbConnection con = new SqlConnection(conStr);
            string sql = "SELECT * FROM Tb_HSPR_IncidentAcceptType WHERE IncidentID IN (SELECT IncidentID FROM View_HSPR_Incidentseach_FIlter WHERE CommID='" + CommID + "' AND ISNULL( DispType,0)=0  AND ISNULL( DealState,0)= 0  AND ISNULL(IsDelete,0)=0 )";
            DataTable dt = con.ExecuteReader(sql, null, null, null, CommandType.Text).ToDataSet().Tables[0];
            con.Dispose();
            return dt;
        }
        #endregion

        #region 报事分派获取处理人
        /// <summary>
        /// 报事分派获取处理人 GetIncidentDealMan_Assign
        /// </summary>
        /// <param name="row"></param>
        /// CommID              小区编码[必填]
        /// TypeID              报事类别[必填]
        /// userName            处理人名称【选填】
        /// 返回：
        ///     UserCode：用户编码
        ///     UserName：用户名称
        ///     RoleName：角色名称
        /// <returns></returns>
        private string GetIncidentDealMan_Assign(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编码不能为空");
            }
            if (!row.Table.Columns.Contains("TypeID") || string.IsNullOrEmpty(row["TypeID"].ToString()))
            {
                return JSONHelper.FromString(false, "报事类别不能为空");
            }
            string userName = "";
            string Result = "";
            string Comm = row["userName"].ToString();
            if (row.Table.Columns.Contains("userName"))
            {
                userName = row["userName"].ToString();
            }

            DataTable dt = new DataTable();
            try
            {

                string str = "SELECT CorpTypeID  FROM Tb_HSPR_IncidentType  WHERE CommID=@CommID  and TypeID in (@TypeID)";
                IDbConnection con = new SqlConnection(PubConstant.hmWyglConnectionString);
                //查询公司报事编号
                List<string> list = con.Query<string>(str, new { CommID = Comm, TypeID = row["TypeID"] }).ToList<string>();
                con.Dispose();
                string strCorpTypeID = "";
                foreach (string item in list)
                {
                    strCorpTypeID = strCorpTypeID + ",'" + item + "'";
                }
                strCorpTypeID = strCorpTypeID.Substring(1, strCorpTypeID.Length - 1);
                con = new SqlConnection(PubConstant.hmWyglConnectionString);
                str = "";
                str = "select CorpTypeID from Tb_HSPR_IncidentTypeProcessPost where CorpTypeID in (" + strCorpTypeID + ")";
                list = new List<string>();
                list = con.Query<string>(str).ToList<string>();
                con.Dispose();

                if (list.Count > 0)
                {
                    str = "";
                    str = "select UserCode,UserName,RoleName from View_HSPR_IncidentTypeProcessPostUser";
                    str = str + " where CommID = '" + Comm + "' and CorpTypeID in (" + strCorpTypeID + ")";
                    if (!string.IsNullOrEmpty(userName))
                    {
                        str += " and UserName like '%" + userName + "%'";
                    }
                    str = str + " group by UserCode,UserName,RoleName";
                    con = new SqlConnection(PubConstant.hmWyglConnectionString);
                    dt = con.ExecuteReader(str, null, null, null, CommandType.Text).ToDataSet().Tables[0];
                    con.Dispose();
                }
                else
                {
                    str = "";
                    str = "select UserCode,UserName, dbo.funGetUserSysRoleNamesByCommID(UserCode,CommID) as RoleName, CommID from View_HSPR_IncidentTypeProcessPostUserAll";
                    str = str + " where CommID = '" + Comm + "'";
                    if (!string.IsNullOrEmpty(userName))
                    {
                        str += " and UserName like '%" + userName + "%'";
                    }
                    str = str + " group by UserCode,UserName, CommID";
                    con = new SqlConnection(PubConstant.hmWyglConnectionString);
                    dt = con.ExecuteReader(str, null, null, null, CommandType.Text).ToDataSet().Tables[0];
                    con.Dispose();
                }
            }
            catch (Exception ex)
            {
                Result = ex.Message;
            }
            if (Result != "")
            {
                return JSONHelper.FromString(false, Result);
            }
            else
            {
                return JSONHelper.FromString(dt);
            }

        }
        #endregion

        #region 报事详情

        /// <summary>
        /// 报事详情  GetUntreatedIncidentView
        /// </summary>
        /// 报事编号：IncidentID【必填】
        /// <param name="row"></param>
        /// IncidentID      报事编码[必填]
        /// CommID          小区编码[必填]
        /// 
        /// <returns></returns>
        private string GetUntreatedIncidentView(DataRow row)
        {
            if ((!row.Table.Columns.Contains("IncidentID") || string.IsNullOrEmpty(row["IncidentID"].ToString())) &&
                (!row.Table.Columns.Contains("IncidentNum") || string.IsNullOrEmpty(row["IncidentNum"].ToString())))
            {
                return JSONHelper.FromString(false, "报事编码不能为空");
            }
            //if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            //{
            //    return JSONHelper.FromString(false, "小区编码不能为空");
            //}

            //StringBuilder sb = new StringBuilder();
            //sb.Append("SELECT c.CommName, b.* from ");
            //sb.Append("(");
            //sb.Append(" SELECT *  FROM (");
            //sb.Append(" SELECT B.*, dbo.funGetProcessRoleCode(B.TypeID) AS ProcessRoleID,");
            //sb.Append(" dbo.funGetAssignedRoleCode(B.TypeID) AS AssignedRoleID, ");
            //sb.Append(" C.UserCode,D.ProcessIncidentImgs, D.EmergencyDegree");
            //sb.Append(" FROM view_HSPR_IncidentSeach_Filter AS B ");
            //sb.Append(" LEFT JOIN Tb_HSPR_IncidentAcceptDeal AS C ON B.IncidentID = C.IncidentID ");
            //sb.Append(" LEFT JOIN Tb_HSPR_IncidentAccept AS D ON B.IncidentID = D.IncidentID");
            //sb.Append(" ) AS A ");
            //sb.AppendFormat("where IncidentID='{0}' AND IsDelete = 0", row["IncidentID"]);
            //sb.Append(" ) as b ");
            //sb.Append(" inner join Tb_HSPR_Community as c ");
            //sb.Append(" on b.CommID=c.CommID ");

            string incidentId = "";
            string incidentNum = "";
            string str = @"select x.*,
                (SELECT TOP 1 isnull(MobileTel,'') FROM Tb_Sys_User b WHERE b.UserName=x.FinishUser) AS FinishUserMobile,
                (SELECT TOP 1 isnull(MobileTel,'') FROM Tb_Sys_User WHERE UserCode=x.DispUserCode) AS DispManMobile
                from View_HSPR_Incidentseach_FIlter x where ";
            if (Global_Var.LoginCorpID == "1970")
            {
                str = @"select x.*,
                (SELECT TOP 1 isnull(MobileTel,'') FROM Tb_Sys_User b WHERE b.UserName=x.FinishUser) AS FinishUserMobile,
                (SELECT TOP 1 isnull(MobileTel,'') FROM Tb_Sys_User WHERE UserCode=x.DispUserCode) AS DispManMobile,
                (CASE  WHEN x.DispUserCode = @UserCode THEN 1 ELSE 0 END ) AS IsSnatch
                from View_HSPR_Incidentseach_FIlter x where ";

            }

            if (row.Table.Columns.Contains("IncidentID") && !string.IsNullOrEmpty(row["IncidentID"].ToString()))
            {
                incidentId = row["IncidentID"].ToString();
                str += $" x.IncidentID={incidentId}";
            }

            if (row.Table.Columns.Contains("IncidentNum") && !string.IsNullOrEmpty(row["IncidentNum"].ToString()))
            {
                incidentNum = row["IncidentNum"].ToString();
                if (!string.IsNullOrEmpty(incidentId))
                {
                    str += " OR";
                }
                str += $" x.IncidentNum='{incidentNum}'";
            }

            IDbConnection con = new SqlConnection(PubConstant.hmWyglConnectionString);

            DataSet ds = con.ExecuteReader(str, new { UserCode = Global_Var.LoginUserCode }, null, null, CommandType.Text).ToDataSet();
            con.Dispose();

            string sql = string.Format(@"declare @IncidentID varchar(100),@IncidentNum varchar(100),@TypeID varchar(100);
                                SET @IncidentID = '{0}';
                                SET @IncidentNum = '{1}'; 
                                SELECT @IncidentID=IncidentID,@TypeID=TypeID FROM view_HSPR_IncidentSeach_Filter 
                                    where IncidentID = @IncidentID or IncidentNum = @IncidentNum;
                                SELECT DISTINCT ISNULL(Tb_Sys_User.UserCode, '') as UserCode, 
                                ISNULL(Tb_Sys_User.UserName, '') as UserName, 
                                ISNULL(Tb_Sys_User.MobileTel, '') as Mobile 
                                FROM Tb_HSPR_IncidentAcceptDeal 
                                INNER JOIN Tb_Sys_User ON Tb_HSPR_IncidentAcceptDeal.UserCode = Tb_Sys_User.UserCode 
                                    where Tb_HSPR_IncidentAcceptDeal.IncidentID = @IncidentID;
                                
                                DECLARE @DispLimit INT, @ReplyLimit INT;
                                SELECT @DispLimit=max(isnull(DispLimit,0)),@ReplyLimit=max(isnull(ReplyLimit,0)) 
                                FROM Tb_HSPR_CorpIncidentType WHERE CorpTypeID IN (
                                  SELECT CorpTypeID FROM Tb_HSPR_IncidentType WHERE TypeID IN(
                                    SELECT col FROM Fun_StrSplitToTab(@TypeID,',') WHERE col is NOT NULL AND col<>''));
                                SELECT isnull(@DispLimit,0) AS DispLimit, isnull(@ReplyLimit,0) AS ReplyLimit;",
                                incidentId, incidentNum);

            DataSet ds2 = new DbHelperSQLP(PubConstant.hmWyglConnectionString).Query(sql);

            if (ds.Tables[0].Rows.Count > 0)
            {
                DataColumn newCol1 = new DataColumn()
                {
                    ColumnName = "DealManUserCodes",
                    DataType = typeof(string),
                    MaxLength = 500,
                };
                DataColumn newCol2 = new DataColumn()
                {
                    ColumnName = "DealManMobiles",
                    DataType = typeof(string),
                    MaxLength = 500,
                };
                DataColumn newCol3 = new DataColumn()
                {
                    ColumnName = "DispLimit",
                    DataType = typeof(int)
                };
                DataColumn newCol4 = new DataColumn()
                {
                    ColumnName = "ReplyLimit",
                    DataType = typeof(int)
                };
                ds.Tables[0].Columns.Add(newCol1);
                ds.Tables[0].Columns.Add(newCol2);
                if (!ds.Tables[0].Columns.Contains("DispLimit"))
                {
                    ds.Tables[0].Columns.Add(newCol3);
                }
                if (!ds.Tables[0].Columns.Contains("ReplyLimit"))
                {
                    ds.Tables[0].Columns.Add(newCol4);
                }

                if (ds2.Tables[0].Rows.Count > 0)
                {
                    string userNames = "";
                    string userCodes = "";
                    string mobile = "";
                    foreach (DataRow dr2 in ds2.Tables[0].Rows)
                    {
                        userNames += (dr2["UserName"].ToString() + ",");
                        userCodes += (dr2["UserCode"].ToString() + ",");
                        mobile += (dr2["Mobile"].ToString() + ",");
                    }
                    userNames = userNames.Trim(',');
                    userCodes = userCodes.Trim(',');
                    mobile = mobile.Trim(',');
                    ds.Tables[0].Rows[0]["DealMan"] = userNames;
                    ds.Tables[0].Rows[0]["DealManUserCodes"] = userCodes;
                    ds.Tables[0].Rows[0]["DealManMobiles"] = mobile;
                }

                ds.Tables[0].Rows[0]["DispLimit"] = ds2.Tables[1].Rows[0]["DispLimit"];
                ds.Tables[0].Rows[0]["ReplyLimit"] = ds2.Tables[1].Rows[0]["ReplyLimit"];
                if (string.IsNullOrEmpty(ds.Tables[0].Rows[0]["ClassID"].ToString()))
                {
                    ds.Tables[0].Rows[0]["ClassID"] = "1";
                }
            }

            return JSONHelper.FromString(ds.Tables[0]);
        }
        #endregion


        /// <summary>
        /// 获取处理人 GetIncidentDealMan
        /// </summary>
        /// <param name="row"></param>
        /// 小区编号：CommID【必填】
        /// 报事类别：TypeID【报事类别,多个用逗号隔开】
        /// 返回：
        ///     UserCode,UserName,RoleNametartIndex 
        /// <returns></returns>
        private string GetIncidentDealMan(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编码不能为空");
            }
            if (!row.Table.Columns.Contains("TypeID") || string.IsNullOrEmpty(row["TypeID"].ToString()))
            {
                return JSONHelper.FromString(false, "报事类别不能为空");
            }
            DataTable result = null;
            string TypeID = "";
            string CommID = row["CommID"].ToString();
            if (row.Table.Columns.Contains("TypeID"))
            {
                TypeID = row["TypeID"].ToString();
            }

            StringBuilder sb_Role = new StringBuilder();

            IDbConnection con = new SqlConnection(PubConstant.hmWyglConnectionString);
            if (!string.IsNullOrEmpty(TypeID))
            {
                var typeArray = TypeID.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (typeArray.Length > 0)
                {

                    sb_Role.AppendFormat(" and CommID={0}", CommID);
                    sb_Role.AppendFormat(" and TypeID in ('{0}')", string.Join("','", typeArray));

                    using (DataTable dTable = GetTypeList(sb_Role.ToString(), PubConstant.hmWyglConnectionString))
                    {
                        if (dTable.Rows.Count > 0)
                        {
                            string strCorpTypeID = "";
                            sb_Role = new StringBuilder();
                            foreach (DataRow DRow in dTable.Rows)
                            {
                                sb_Role.AppendFormat(",'{0}'", DRow["CorpTypeID"].ToString());
                            }
                            strCorpTypeID = sb_Role.ToString().Substring(1, sb_Role.ToString().Length - 1);



                            sb_Role = new StringBuilder();
                            sb_Role.Append("SELECT UserCode, UserName FROM Tb_Sys_User WHERE UserCode IN(");
                            sb_Role.Append("select UserCode from View_HSPR_IncidentTypeProcessPostUser");
                            sb_Role.AppendFormat(" where CommID={0} and CorpTypeID in ({1}))", CommID, strCorpTypeID);

                            result = con.ExecuteReader(sb_Role.ToString()).ToDataSet().Tables[0];
                            con.Dispose();
                            if (result.Rows.Count > 0)
                            {
                                return JSONHelper.FromString(result);
                            }
                            con = new SqlConnection(PubConstant.hmWyglConnectionString);
                        }
                    }
                }
            }

            sb_Role = new StringBuilder();
            sb_Role.Append("select");
            sb_Role.Append(" UserCode,UserName, dbo.funGetUserSysRoleNamesByCommID(UserCode,CommID) as RoleName, CommID ");
            sb_Role.Append(" from View_HSPR_IncidentTypeProcessPostUserAll ");
            sb_Role.AppendFormat("  where CommID = '{0}'", CommID);
            sb_Role.Append("  group by UserCode,UserName, CommID ");

            result = con.ExecuteReader(sb_Role.ToString(), null, null, null, CommandType.Text).ToDataSet().Tables[0];
            con.Dispose();
            return JSONHelper.FromString(result);
        }

        #region 报事分派

        /// <summary>
        /// 报事分派IncidentAssignment
        /// </summary>
        /// <param name="row"></param>
        /// DealMan             处理人
        /// UserCodes           处理人编码
        /// IncidentID          报事编码
        /// TypeID              报事类型
        /// DispType            派工类别[1派工，2协调]   
        /// CoordinateNum       派工单号
        /// DealLimit           处理时限
        /// IncidentContent     报事内容(2017-06-19新增需求,需要在分派时可以修改报事内容)
        /// 以上参数全部必填
        /// 返回：false 错误信息              true  报事分派成功
        /// <returns></returns>
        private string IncidentAssignment(DataRow row)
        {
            if (!row.Table.Columns.Contains("DealMan") || string.IsNullOrEmpty(row["DealMan"].ToString()))
            {
                return JSONHelper.FromString(false, "处理人不能为空");
            }
            if (!row.Table.Columns.Contains("UserCodes") || string.IsNullOrEmpty(row["UserCodes"].ToString()))
            {
                return JSONHelper.FromString(false, "处理人编码不能为空");
            }
            if (!row.Table.Columns.Contains("IncidentID") || string.IsNullOrEmpty(row["IncidentID"].ToString()))
            {
                return JSONHelper.FromString(false, "报事编码不能为空");
            }
            string incidentID = row["IncidentID"].AsString();
            if (!row.Table.Columns.Contains("TypeID") || string.IsNullOrEmpty(row["TypeID"].ToString()))
            {
                return JSONHelper.FromString(false, "报事类型不能为空");
            }
            if (!row.Table.Columns.Contains("DispType") || string.IsNullOrEmpty(row["DispType"].ToString()))
            {
                return JSONHelper.FromString(false, "派工类别不能为空");
            }
            //if (!row.Table.Columns.Contains("DealLimit") || string.IsNullOrEmpty(row["DealLimit"].ToString()))
            //{
            //    return JSONHelper.FromString(false, "处理时限不能为空");
            //}
            string IncidentContent = null;
            if (row.Table.Columns.Contains("IncidentContent"))
            {
                if (string.IsNullOrEmpty(row["IncidentContent"].ToString()))
                {
                    return JSONHelper.FromString(false, "报事内容不能为空");
                }
                IncidentContent = row["IncidentContent"].AsString();
            }
            string Resule = "";
            try
            {
                //处理报事类型
                string strTypeID = row["TypeID"].ToString();

                if (strTypeID.IndexOf(",") > -1)
                {
                    if (strTypeID.Substring(0, 1) != ",")
                    {
                        strTypeID = "," + strTypeID;
                    }
                }
                else
                {
                    strTypeID = "," + strTypeID + ",";
                }

                //获取当前报事信息
                IDbConnection con = new SqlConnection(PubConstant.hmWyglConnectionString);
                string Sql = "select IncidentID,CommID,CustID,RoomID,TypeID,IncidentNum,IncidentPlace,IncidentMan,IncidentDate,IncidentMode, DealLimit,IncidentContent ,ReserveDate ,ReserveLimit, Phone,AdmiMan, AdmiDate, DispType, DispMan,DispDate,DealMan , CoordinateNum,EndDate,MainStartDate,MainEndDate,DealSituation   , CustComments  ,ServiceQuality  ,ArticlesFacilities ,DealState, IncidentMemo,IsDelete ,Reasons,RegionalID,DeleteReasons,DeleteDate,TypeCode,Signatory,IsStatistics, FinishUser, DueAmount,IsTell, DeviceID,PrintTime,PrintCount,PrintUserName,IsReceipt,ReceiptUserName,LocationID,IncidentImgs,SignDate ,CallIncidentType, EID, DispUserCode,TaskEqId,ProcessIncidentImgs,EmergencyDegree, IsReWork,ReWorkDate,ReWorkCreater,ReWorkContent ,ReceivingDate,ArriveData from Tb_HSPR_IncidentAccept where   IncidentID=@IncidentID";
                Tb_HSPR_IncidentAccept IncidentAccept = con.Query<Tb_HSPR_IncidentAccept>(Sql, new { IncidentID = row["IncidentID"].ToString() }).SingleOrDefault();
                con.Dispose();

                // 排除华南城
                if (IncidentAccept.DispType.HasValue && IncidentAccept.DispType.Value != 0 && Global_Var.LoginCorpID != "1975")
                {
                    return JSONHelper.FromString(false, "该报事已被分派");
                }

                IncidentAccept.DealMan = row["DealMan"].ToString();
                //派工人为当前登录人员
                IncidentAccept.DispMan = Global_Var.LoginUserName;
                IncidentAccept.DispUserCode = Global_Var.LoginUserCode;

                //派工时间
                IncidentAccept.DispDate = DateTime.Now;

                // 如果是抢单，设置接单时间
                if (row.Table.Columns.Contains("IsSnatch"))
                {
                    int IsSnatch = AppGlobal.StrToInt(row["IsSnatch"].ToString());
                    if (IsSnatch > 0)
                    {
                        IncidentAccept.ReceivingDate = DateTime.Now.AddMinutes(1);
                    }
                }

                //2017-06-19
                //敬志强
                //新增需求:需要在分配时修改报事内容
                //如果提交上来的新的报事内容不为NULL,就设置新的报事内容
                if (null != IncidentContent)
                {
                    IncidentAccept.IncidentContent = IncidentContent;
                }

                string IncidentID = "";
                //如果分派时报事类别发生变动，则获取改变后的类别和处理时限。
                if (strTypeID != IncidentAccept.TypeID)
                {
                    IncidentAccept.TypeID = strTypeID;
                    IncidentAccept.DealLimit = AppGlobal.StrToInt(row["DealLimit"].ToString());
                }

                if (row["DispType"].ToString() == "2")// 派工转协调
                {
                    IncidentAccept.CoordinateNum = HSPR_IncidentAssigned_GetCoordinateNum(row["CommID"].ToString(), 2, "X");

                    IncidentAccept.DispType = 2;

                    //更新派工单
                    HSPR_IncidentAssignedConversion_Update(IncidentAccept);

                    Tb_HSPR_IncidentAcceptChange ItemChange = new Tb_HSPR_IncidentAcceptChange();
                    ItemChange.IncidentID = Global_Fun.StrToInt(IncidentAccept.IncidentID.ToString());
                    ItemChange.ChangeDate = DateTime.Now.ToLongTimeString();
                    ItemChange.ChangeUserCode = row["UserCodes"].ToString();
                    HSPR_IncidentAcceptChange_Insert(ItemChange, IncidentAccept.CommID.ToString());

                    #region 查询新的单ID

                    string strSQL = "select * from view_HSPR_IncidentAccept_Filter where  IncidentNum = " + IncidentAccept.IncidentNum + " And TypeID = '" + strTypeID + "' and DispType = 2 ";

                    con = new SqlConnection(PubConstant.hmWyglConnectionString);
                    DataTable dTable = con.ExecuteReader(strSQL, null, null, null, CommandType.Text).ToDataSet().Tables[0];
                    con.Dispose();
                    if (dTable.Rows.Count > 0)
                    {
                        IncidentID = dTable.Rows[0]["IncidentID"].ToString();
                    }

                    dTable.Dispose();
                    #endregion

                }
                else if (row["DispType"].ToString() == "1")//派工单
                {
                    #region 派工单              
                    IncidentAccept.DispType = 1;
                    IncidentAccept.CoordinateNum = HSPR_IncidentAssigned_GetCoordinateNum(row["CommID"].ToString(), 1, "P");
                    HSPR_IncidentAssigned_Update(IncidentAccept);

                    #endregion
                }

                //派工 保存分派人
                HSPR_IncidentUpdateDispManUserCode(IncidentAccept.IncidentID, IncidentAccept.CommID.ToString(), IncidentAccept.DispUserCode, IncidentAccept.DispMan);

                //派工先删除分派人在保存
                HSPR_IncidentAcceptDeal_Delete(IncidentAccept.CommID.ToString(), IncidentAccept.IncidentID);

                //派工 保存处理人
                foreach (string strTypeIDNew in row["TypeID"].ToString().Split(','))
                {
                    if (strTypeIDNew != "")
                    {
                        foreach (string strDealUserCode in row["UserCodes"].ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                        {
                            //口头派工 保存处理人
                            HSPR_IncidentAcceptDeal_Insert(PubConstant.hmWyglConnectionString, IncidentAccept.CommID.ToString(), IncidentAccept.IncidentID, strTypeIDNew, strDealUserCode);
                        }
                    }
                }
                if (Global_Var.LoginCorpID == "1862")
                {
                    IncidentAcceptPush.SynchPush2WeChatAfterAssign(IncidentAccept);
                }
                else
                {
                    IncidentAcceptPush.SynchPushAfterAssign(IncidentAccept);
                }
                // 推送
             

                #region  鸿坤报事需要把报事信息推送给第三方400 
                try
                {
                    //鸿坤单独
                    if (Global_Var.LoginCorpID == "1973")
                    {
                        #region 同步工单状态    
                        Dictionary<string, string> dir = new Dictionary<string, string>();
                        dir.Add("incidentID", IncidentAccept.IncidentID.ToString());
                        dir.Add("commID", IncidentAccept.CommID.ToString());
                        dir.Add("dispType", "1");
                        dir.Add("dispMan", IncidentAccept.DispMan);
                        dir.Add("dispDate", Convert.ToDateTime(IncidentAccept.DispDate).ToString("yyyy-MM-dd HH:mm:ss"));
                        dir.Add("dispLimit", IncidentAccept.DispLimit);
                        dir.Add("dealMan", IncidentAccept.DealMan);
                        dir.Add("mainEndDate", "");
                        dir.Add("isDeal", "0");
                        dir.Add("isReWork", "1"); //0是返工 1 非返
                        dir.Add("operateType", "1"); //0：新增，1：修改，2：删除
                        dir.Add("callCenterIncidentID", "");
                        dir.Add("dealDescribe", " ");
                        SynchronizeIncidentData.SynchronizeData_WorkOrderStatus(dir, Connection.GetConnection("8"));
                        #endregion
                    }
                }
                catch
                {


                }

                #endregion



            }
            catch (Exception ex)
            {
                Resule = ex.StackTrace;
            }

            if (Resule != "")
            {
                return JSONHelper.FromString(false, Resule);
            }
            else
            {
                return JSONHelper.FromString(true, "操作成功");
            }

        }
        #endregion

        #region 报事处理
        /// <summary>
        /// 报事处理IncidentProcessing
        /// </summary>
        /// <param name="row"></param>
        /// CommID              小区编码【必填】
        /// IncidentID          报事编码【必填】
        /// 以下参数全部选 填
        /// TypeID              报事类型
        /// UserCodes           处理人
        /// ReceivingDate       接单时间
        /// ArriveData          到场时间
        /// DealSituation       处理情况
        /// CustComments        客户意见
        /// ServiceQuality      服务质量
        /// Signatory           实签人
        /// DealState           是否完结默认未完结【如果完结，完结时间为当前时间】
        /// IncidentImgs        图片
        /// 返回：false 错误信息              true  报事分派成功
        /// <returns></returns>
        private string IncidentProcessing(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编码不能为空");
            }
            if (!row.Table.Columns.Contains("IncidentID") || string.IsNullOrEmpty(row["IncidentID"].ToString()))
            {
                return JSONHelper.FromString(false, "报事编码不能为空");
            }
            string backStr = "";
            string cusomerPhone = "";
            try
            {
                //string ReceivingDate = "";//接单时间
                //string ArriveData = "";//到场时间
                string DealSituation = "";//处理情况
                string Signatory = "";//实签人
                int DealState = 0;//是否完结默认未完结【如果完结，完结时间为当前时间】
                string IncidentImgs = "";//图片
                string UserNames = "";
                string UserCodes = "";//处理人
                string ServiceQuality = "";//服务质量

                if (row.Table.Columns.Contains("UserCodes"))
                {
                    UserCodes = row["UserCodes"].ToString();
                }
                if (row.Table.Columns.Contains("UserNames"))
                {
                    UserNames = row["UserNames"].ToString();
                }
                //if (row.Table.Columns.Contains("ReceivingDate"))
                //{
                //    ReceivingDate = row["ReceivingDate"].ToString();
                //}
                //if (row.Table.Columns.Contains("ArriveData"))
                //{
                //    ArriveData = row["ArriveData"].ToString();
                //}
                if (row.Table.Columns.Contains("DealSituation"))
                {
                    DealSituation = row["DealSituation"].ToString();
                }
                if (Global_Var.CorpID == "1824" && (string.IsNullOrEmpty(DealSituation) || DealSituation.Trim().Length < 10))
                {
                    return JSONHelper.FromString(false, "处理情况必须填写10个字以上");
                }
                if (row.Table.Columns.Contains("Signatory"))
                {
                    Signatory = row["Signatory"].ToString();
                }
                if (row.Table.Columns.Contains("DealState"))
                {
                    DealState = AppGlobal.StrToInt(row["DealState"].ToString());
                }
                if (row.Table.Columns.Contains("IncidentImgs"))
                {
                    IncidentImgs = row["IncidentImgs"].ToString();
                }

                if (row.Table.Columns.Contains("ServiceQuality"))
                {
                    ServiceQuality = row["ServiceQuality"].ToString();
                }



                string signatoryImg = null;// 实签人图片
                if (row.Table.Columns.Contains("SignatoryImg"))
                {
                    signatoryImg = row["SignatoryImg"].ToString();
                }

                IDbConnection con = new SqlConnection(PubConstant.hmWyglConnectionString);
                Tb_HSPR_IncidentAccept m = con.Query<Tb_HSPR_IncidentAccept>("select * from Tb_HSPR_IncidentAccept where IncidentID=@IncidentID",
                    new { IncidentID = row["IncidentID"] }).LastOrDefault();
                if (m == null)
                {
                    return JSONHelper.FromString(false, "未找到该报事信息");
                }
                cusomerPhone = m.Phone;
                ////如果到场时间不为空，才保存新的到场时间，否则不做更改。
                ////2017-12-02，敬志强
                //if (!string.IsNullOrEmpty(ReceivingDate))
                //{
                //    m.ReceivingDate = AppGlobal.StrToDate(ReceivingDate);
                //}
                ////如果接单时间不为空，才保存新的接单时间，否则不做更改。
                ////2017-12-02，敬志强
                //if (!string.IsNullOrEmpty(ArriveData))
                //{
                //    m.ArriveData = AppGlobal.StrToDate(ArriveData);
                //}
                m.DealSituation = DealSituation;

                // 实签人
                m.Signatory = Signatory;
                m.SignatoryImg = signatoryImg;

                m.DealState = DealState;

                // 2017年6月7日08:55:53 谭洋，新增 IncidentMemo 备注信息
                if (row.Table.Columns.Contains("IncidentMemo"))
                {
                    m.IncidentMemo = row["IncidentMemo"].ToString();
                }
                // 2017年6月12日15:58:00 敬志强，新增FinishUser  完结人
                // 处理完成，保存完结人信息
                if (DealState == 1)
                {
                    m.FinishUser = Global_Var.UserName;
                    m.MainEndDate = DateTime.Now;
                }

                m.ProcessIncidentImgs = IncidentImgs;//报事处理图片

                if (row.Table.Columns.Contains("TypeID") && row["TypeID"].ToString() != "")
                {
                    m.TypeID = row["TypeID"].ToString();
                }

                if (!string.IsNullOrEmpty(UserNames))
                {
                    m.DealMan = UserNames;
                }
                if (Global_Var.LoginCorpID == "1906" ||
                    Global_Var.LoginCorpID == "2061" ||
                    Global_Var.LoginCorpID == "2062" ||
                    Global_Var.LoginCorpID == "2063")//平安需求5019 要求上传服务质量
                {
                    m.ServiceQuality = ServiceQuality;
                }

                con.Update<Tb_HSPR_IncidentAccept>(m);

                // 重设TypeID
                if (row.Table.Columns.Contains("TypeID") && row.Table.Columns.Contains("UserCodes") && row["TypeID"].ToString() != "" && row["UserCodes"].ToString() != "")
                {
                    con.Execute(@"DELETE FROM Tb_HSPR_IncidentAcceptDeal WHERE IncidentId=@IncidentId;
                                  DELETE FROM Tb_HSPR_IncidentAcceptType WHERE IncidentId=@IncidentId", new { IncidentId = m.IncidentID });

                    string[] newTypeIDArr = row["TypeID"].ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    string[] newDealManArr = row["UserCodes"].ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                    string sql = @" DECLARE @_TypeCode nvarchar(30);
                                    DECLARE @_RatedWorkHour decimal(18, 2);
                                    DECLARE @_RatedWorkHourNumber decimal(18, 2) = 1.0;
                                    DECLARE @_KPIRatio decimal(18, 2);
                                    SELECT @_RatedWorkHour = RatedWorkHour, @_KPIRatio = KPIRatio, @_TypeCode = TypeCode
                                        FROM Tb_HSPR_IncidentType WHERE TypeID = @TypeID;
                                    INSERT INTO Tb_HSPR_IncidentAcceptDeal(IID,CommID,IncidentID,TypeID,UserCode,DealState)
	                                    VALUES(NEWID(),@CommID,@IncidentID,@TypeID,@UserCode,0);
                                    INSERT INTO Tb_HSPR_IncidentAcceptType(CommID,IncidentID,TypeID,TypeCode,RatedWorkHour,RatedWorkHourNumber,KPIRatio)
                                        VALUES(@CommID, @IncidentID, @TypeID, @_TypeCode, @_RatedWorkHour, @_RatedWorkHourNumber, @_KPIRatio);";

                    foreach (string item_typeid in newTypeIDArr)
                    {
                        foreach (string item_dealman in newDealManArr)
                        {
                            con.Execute(sql, new
                            {
                                CommID = m.CommID,
                                IncidentID = m.IncidentID,
                                TypeID = item_typeid,
                                UserCode = item_dealman
                            });
                        }
                    }
                }

                con.Dispose();

                if (Global_Var.LoginCorpID=="1862")
                {
                    IncidentAcceptPush.SynchPush2WeChatComplete(m);
                }
                else
                {
                    IncidentAcceptPush.SynchPushWhenProcessed(m);
                }
              

                #region  鸿坤报事需要把报事信息推送给第三方400 
                try
                {
                    //鸿坤单独
                    if (Global_Var.LoginCorpID == "1973")
                    {
                        using (IDbConnection Conn = new SqlConnection(Connection.GetConnection("8")))
                        {
                            Tb_HSPR_IncidentAccept m_incident = Conn.Query<Tb_HSPR_IncidentAccept>("select * from Tb_HSPR_IncidentAccept where IncidentID=@IncidentID and IsStatistics=1",
                   new { IncidentID = m.IncidentID }).LastOrDefault();

                            #region 同步工单状态    
                            Dictionary<string, string> dir = new Dictionary<string, string>();

                            dir.Add("incidentID", m_incident.IncidentID.ToString());
                            dir.Add("commID", m_incident.CommID.ToString());
                            dir.Add("dispType", m_incident.DispType.ToString());
                            dir.Add("dispMan", m_incident.DispMan);
                            dir.Add("dispDate", m_incident.DispDate.ToString());
                            dir.Add("dispLimit", !string.IsNullOrEmpty(m_incident.DispLimit) ? m_incident.DispLimit.ToString() : "0");
                            dir.Add("dealMan", m_incident.DealMan.ToString());
                            dir.Add("mainEndDate", m_incident.MainEndDate.ToString());
                            dir.Add("isDeal", m_incident.DealState == 1 ? "1" : "0");//0:未完结1：完结     
                            dir.Add("isReWork", "0"); //0是返工 1 非返工
                            dir.Add("operateType", "1"); //0：新增，1：修改，2：删除
                            dir.Add("callCenterIncidentID", "");
                            dir.Add("dealDescribe", DealSituation);
                            SynchronizeIncidentData.SynchronizeData_WorkOrderStatus(dir, Connection.GetConnection("8"));
                            Conn.Dispose();
                            #endregion
                        }

                    }

                }
                catch
                {
                }

                #endregion


            }
            catch (Exception ex)
            {
                backStr = ex.Message;
            }


            if (backStr == "")
            {
                //if (!string.IsNullOrEmpty(cusomerPhone))
                //{
                //    new AppPush().SendAppPushMsg("报事信息", "您有一条新的报事完结信息,请打开APP评价", cusomerPhone, AppPush.JPushMsgType.DEFAULT, "", true);
                //}
                return JSONHelper.FromString(true, "报事处理成功！");
            }
            else
            {
                return JSONHelper.FromString(false, backStr);
            }

        }

        /// <summary>
        /// 报事处理，保存接单时间
        /// </summary>
        ///     参数：CommID，必填
        ///     参数：IncidentID，必填
        /// <returns></returns>
        private string IncidentProcessingReceivingDate(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编码不能为空");
            }
            if (!row.Table.Columns.Contains("IncidentID") || string.IsNullOrEmpty(row["IncidentID"].ToString()))
            {
                return JSONHelper.FromString(false, "报事编码不能为空");
            }

            string incidentId = row["IncidentID"].ToString();

            using (IDbConnection con = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                string date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                int result = con.Execute(@"update Tb_HSPR_IncidentAccept set ReceivingDate='" + date + "' where IncidentID=" + incidentId);

                //wlg 20201228 添加调用接左邻接口处理数据
                try
                {

                }
                catch(Exception ex)
                {

                }

                return JSONHelper.FromString(result > 0, result > 0 ? date : "保存失败");
            };
        }

        /// <summary>
        /// 报事处理，预计到场时间
        /// </summary>
        private string IncidentProcessingPlanArriveDate(DataRow row)
        {
            if (!row.Table.Columns.Contains("Date") || string.IsNullOrEmpty(row["Date"].ToString()))
            {
                return JSONHelper.FromString(false, "预计到场时间不能为空");
            }

            if (!row.Table.Columns.Contains("IncidentID") || string.IsNullOrEmpty(row["IncidentID"].ToString()))
            {
                return JSONHelper.FromString(false, "报事ID不能为空");
            }

            string date = row["Date"].ToString();
            string incidentId = row["IncidentID"].ToString();



            using (IDbConnection con = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                string str = "select * from Tb_HSPR_IncidentAccept where IncidentId=@IncidentId";
                Tb_HSPR_IncidentAccept model = con.Query<Tb_HSPR_IncidentAccept>(str,
                    new { IncidentId = incidentId }).FirstOrDefault();

                if (model != null)
                {
                    IncidentAcceptPush.SynchPushPlanArrive(model, date);

                    // 敏捷需要保持预计到场时间
                    //if (Global_Var.LoginCorpID == "1971")
                    //{
                    if (row.Table.Columns.Contains("Date") && !string.IsNullOrEmpty(row["Date"].ToString()))
                    {
                        con.Execute(@"UPDATE Tb_HSPR_IncidentAccept SET PlanArriveDate=@PlanArriveDate 
                                        WHERE IncidentId=@IncidentId", new
                        {
                            IncidentId = incidentId,
                            PlanArriveDate = row["Date"].ToString()
                        });
                    }
                    //}
                }
                else
                {
                    return JSONHelper.FromString(false, "未找到此报事");
                }

                return JSONHelper.FromString(true, "操作成功");
            };
        }

        /// <summary>
        /// 报事处理，保存到场时间
        /// </summary>
        ///     参数：CommID，必填
        ///     参数：IncidentID，必填
        /// <returns></returns>
        private string IncidentProcessingArriveDate(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编码不能为空");
            }
            if (!row.Table.Columns.Contains("IncidentID") || string.IsNullOrEmpty(row["IncidentID"].ToString()))
            {
                return JSONHelper.FromString(false, "报事编码不能为空");
            }

            string incidentId = row["IncidentID"].ToString();

            using (IDbConnection con = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                string date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                int result = con.Execute(@"update Tb_HSPR_IncidentAccept set ArriveData='" + date + "' where IncidentID=" + incidentId);

                return JSONHelper.FromString(result > 0, result > 0 ? date : "保存失败");
            };
        }

        #endregion

        #region 报事跟进
        /// <summary>
        /// 报事跟进 IncidentCoordinate
        /// </summary>
        /// <param name="row"></param>
        /// CommID              小区编码【必填】
        /// IncidentID          报事编码【必填】
        /// 以下参数全部选 填
        /// CoordinateContent       跟进内容
        ///  CoordinateMan【跟进人名称】  
        /// 返回：
        /// true:   CommID【小区编码】  IncidentID【报事编码】  CoordinateContent【跟进内容】   CoordinateMan【跟进人】  CoordinateDate[跟进时间]
        /// false:错误信息
        /// <returns></returns>
        private string IncidentCoordinate(DataRow row)
        {
            try
            {
                if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
                {
                    return JSONHelper.FromString(false, "小区编码不能为空");
                }
                string CommID = row["CommID"].ToString();
                if (!row.Table.Columns.Contains("IncidentID") || string.IsNullOrEmpty(row["IncidentID"].ToString()))
                {
                    return JSONHelper.FromString(false, "报事编码不能为空");
                }
                string IncidentID = row["IncidentID"].ToString();
                string CoordinateContent = "";
                if (row.Table.Columns.Contains("CoordinateContent"))
                {
                    CoordinateContent = row["CoordinateContent"].ToString();
                }
                string CoordinateMan = Global_Var.UserName;
                if (row.Table.Columns.Contains("CoordinateMan"))
                {
                    CoordinateMan = row["CoordinateMan"].ToString();
                }
                string UnClassify = "";
                if (row.Table.Columns.Contains("UnClassify"))
                {
                    UnClassify = row["UnClassify"].ToString();
                }
                string UnfinishedReason = "";
                if (row.Table.Columns.Contains("UnfinishedReason"))
                {
                    UnfinishedReason = row["UnfinishedReason"].ToString();
                }

                if (CoordinateContent != "")
                {
                    using (IDbConnection con = new SqlConnection(PubConstant.hmWyglConnectionString))
                    {
                        DynamicParameters parameters = new DynamicParameters();
                        parameters.Add("@CommID", CommID);
                        parameters.Add("@IncidentID", IncidentID);
                        parameters.Add("@CoordinateMan", CoordinateMan);
                        parameters.Add("@CoordinateDate", DateTime.Now);
                        parameters.Add("@CoordinateContent", CoordinateContent);
                        parameters.Add("@IsDelete", 0);

                        // 华南城不填写原因
                        if (Global_Var.CorpID != "1975")
                        {
                            parameters.Add("@UnClassify", UnClassify);
                            parameters.Add("@UnfinishedReason", UnfinishedReason);
                        }
                        con.Execute("Proc_HSPR_IncidentCoordinate_Insert", parameters, null, null, CommandType.StoredProcedure);
                    }
                }
                DataTable dt = null;
                using (IDbConnection con = new SqlConnection(PubConstant.hmWyglConnectionString))
                {
                    string sql = "select * from Tb_HSPR_IncidentCoordinate where CommID='" + row["CommID"] + "' and IncidentID='" + row["IncidentID"] + "'";
                    dt = con.ExecuteReader(sql, null, null, null, CommandType.Text).ToDataSet().Tables[0];
                }


                #region  鸿坤报事需要把报事信息推送给第三方400 
                try
                {
                    if (!string.IsNullOrEmpty(CoordinateContent))
                    {
                        //鸿坤单独
                        if (Global_Var.LoginCorpID == "1973")
                        {
                            #region 同步跟进状态   
                            Dictionary<string, string> dir = new Dictionary<string, string>();
                            dir.Add("incidentID", IncidentID.ToString());
                            dir.Add("commID", CommID.ToString());
                            dir.Add("coordinateMan", CoordinateMan.ToString());
                            dir.Add("coordinateDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                            dir.Add("coordinateContent", CoordinateContent.ToString());
                            dir.Add("isDelete", "0");
                            dir.Add("operateType", "0"); //0：新增，1：修改，2：删除  
                            SynchronizeIncidentData.SynchronizeData_workFollowSynchronize(dir, Connection.GetConnection("8"));

                            #endregion
                        }
                    }
                }
                catch
                {

                }

                #endregion




                return JSONHelper.FromString(dt);
            }
            catch (Exception ex)
            {
                return JSONHelper.FromString(false, ex.Message);
            }
        }

        #endregion

        #region 报事处理工时信息查询
        /// <summary>
        /// 报事处理工时信息查询GetIncidentAcceptType
        /// </summary>
        /// <param name="row"></param>
        /// CommID          小区编码
        /// TypeID          报事类型
        /// IncidentID      报事编码
        /// 以上参数均必填
        /// 返回：
        ///     true:
        ///     IID[工时编码]，CommID[小区编码]，IncidentID【报事编码】，TypeID【类别ID，修改时传此参数】，TypeCode【类别编码】，RatedWorkHour【额定工时（分钟）】，KPIRatio【绩效系数】，RatedWorkHourNumber【完成数量】,TypeName [报事类别名称]
        /// <returns></returns>
        private string GetIncidentAcceptType(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编码不能为空");
            }
            if (!row.Table.Columns.Contains("TypeID") || string.IsNullOrEmpty(row["TypeID"].ToString()))
            {
                return JSONHelper.FromString(false, "报事类别不能为空");
            }
            if (!row.Table.Columns.Contains("IncidentID") || string.IsNullOrEmpty(row["IncidentID"].ToString()))
            {
                return JSONHelper.FromString(false, "报事编码不能为空");
            }

            IDbConnection con = new SqlConnection(PubConstant.hmWyglConnectionString);
            string sql = "select t.*,h.TypeName from Tb_HSPR_IncidentAcceptType as t inner join  funSplitTabel('" + row["TypeID"] + "', ',') as r on t.TypeID = r.ColName  left join Tb_HSPR_IncidentType as h on t.TypeID=h.TypeID  where t.CommID=" + row["CommID"] + " and  t.IncidentID=" + row["IncidentID"];
            DataTable dt = con.ExecuteReader(sql, null, null, null, CommandType.Text).ToDataSet().Tables[0];
            con.Dispose();
            return JSONHelper.FromString(dt);

        }
        #endregion

        #region 报事处理工时信息登记
        /// <summary>
        /// 报事处理工时信息登记SetIncidentAcceptType
        /// </summary>
        /// <param name="row"></param>
        /// CommID              小区编码
        /// IID                 工时ID
        /// RatedWorkHourNumber  完成数量【选填】
        /// 返回：
        ///     true:登记成功
        ///     false:错误信息
        /// <returns></returns>
        private string SetIncidentAcceptType(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编码不能为空");
            }
            if (!row.Table.Columns.Contains("IID") || string.IsNullOrEmpty(row["IID"].ToString()))
            {
                return JSONHelper.FromString(false, "工时ID不能为空");
            }
            string backstr = "";
            int RatedWorkHourNumber = 0;

            try
            {

                if (row.Table.Columns.Contains("RatedWorkHourNumber"))
                {
                    RatedWorkHourNumber = AppGlobal.StrToInt(row["RatedWorkHourNumber"].ToString());
                }

                IDbConnection con = new SqlConnection(PubConstant.hmWyglConnectionString);
                string sql = "update Tb_HSPR_IncidentAcceptType set RatedWorkHourNumber=" + RatedWorkHourNumber + " where IID=" + row["IID"];
                con.Execute(sql);
            }
            catch (Exception ex)
            {

                backstr = ex.Message;
            }
            if (backstr == "")
            {
                return JSONHelper.FromString(true, "登记成功");
            }
            else
            {
                return JSONHelper.FromString(false, backstr);
            }

        }
        #endregion

        #region 派工单、协调单模糊查询
        /// <summary>
        /// 派工单、协调单模糊查询QueryCoordinateNum
        /// </summary>
        /// <param name="row"></param>
        /// CommID                  小区编码
        /// DispType                派工类型【1派工、2协调】
        /// Content                 查询内容【选 填】
        /// 返回：
        ///     CoordinateNum   派工单号
        /// <returns></returns>
        private string QueryCoordinateNum(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编码不能为空");
            }
            if (!row.Table.Columns.Contains("DispType") || string.IsNullOrEmpty(row["DispType"].ToString()))
            {
                return JSONHelper.FromString(false, "派工类型不能为空");
            }
            string Content = "";

            if (row.Table.Columns.Contains("Content"))
            {
                Content = row["Content"].ToString();
            }
            IDbConnection con = new SqlConnection(PubConstant.hmWyglConnectionString);
            string sql = "select CoordinateNum from Tb_HSPR_IncidentAccept where CommID=" + row["CommID"] + " and DispType=" + row["DispType"] + " and CoordinateNum like '%" + Content + "%'";
            DataTable dt = con.ExecuteReader(sql, null, null, null, CommandType.Text).ToDataSet().Tables[0];

            return JSONHelper.FromString(dt);

        }
        #endregion

        #region 模糊查询所有处理人
        /// <summary>
        /// 模糊查询所有处理人QueryProcessPostUserAll
        /// </summary>
        /// <param name="row"></param>
        /// 小区编号：CommID【必填】
        /// 返回：
        /// UserCode，UserName，RoleName，CommID
        /// <returns></returns>
        private string QueryProcessPostUserAll(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编码不能为空");
            }

            string sql = "select UserCode,UserName, dbo.funGetUserSysRoleNamesByCommID(UserCode,CommID) as RoleName, CommID from View_HSPR_IncidentTypeProcessPostUserAll where CommID=" + row["CommID"] + " group by UserCode,UserName, CommID";
            IDbConnection con = new SqlConnection(PubConstant.hmWyglConnectionString);
            DataTable dt = con.ExecuteReader(sql, null, null, null, CommandType.Text).ToDataSet().Tables[0];
            return JSONHelper.FromString(dt);
        }

        #endregion

        #region 报事投诉查询
        /// <summary>
        /// 报事投诉查询QueryComplaintList
        /// </summary>
        /// <param name="row"></param>
        /// CommID              小区编码
        /// DispType            派工类型
        /// BeginTime           开始时间
        /// EndTime             结束时间
        /// 以下为选 填参数
        /// CoordinateNum       派工单号
        /// RoomSign            房间编号
        /// DealMan             处理人
        /// PageIndex           默认1
        /// PageSize            默认10
        /// <returns></returns>
        private string QueryComplaintList(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编码不能为空");
            }
            if (!row.Table.Columns.Contains("DispType") || string.IsNullOrEmpty(row["DispType"].ToString()))
            {
                return JSONHelper.FromString(false, "派工类型不能为空");
            }
            if (!row.Table.Columns.Contains("BeginTime") || string.IsNullOrEmpty(row["BeginTime"].ToString()))
            {
                return JSONHelper.FromString(false, "开始时间不能为空");
            }
            if (!row.Table.Columns.Contains("EndTime") || string.IsNullOrEmpty(row["EndTime"].ToString()))
            {
                return JSONHelper.FromString(false, "结束时间不能为空");
            }

            string backstr = "";

            try
            {
                string CoordinateNum = "";
                string RoomSign = "";
                string DealMan = "";
                int PageIndex = 1;
                int PageSize = 10;
                string CommID = row["CommID"].ToString();
                int DispType = AppGlobal.StrToInt(row["DispType"].ToString());
                DateTime BeginTime = DataSecurity.StrToDateTime(row["BeginTime"].ToString());
                DateTime EndTime = DataSecurity.StrToDateTime(row["EndTime"].ToString());

                if (row.Table.Columns.Contains("CoordinateNum"))
                {
                    CoordinateNum = row["CoordinateNum"].ToString();
                }
                if (row.Table.Columns.Contains("RoomSign"))
                {
                    RoomSign = row["RoomSign"].ToString();
                }
                if (row.Table.Columns.Contains("DealMan"))
                {
                    DealMan = row["DealMan"].ToString();
                }

                if (row.Table.Columns.Contains("PageIndex") && AppGlobal.StrToInt(row["PageIndex"].ToString()) > 0)
                {
                    PageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());
                }
                if (row.Table.Columns.Contains("PageSize") && AppGlobal.StrToInt(row["PageSize"].ToString()) > 0)
                {
                    PageSize = AppGlobal.StrToInt(row["PageSize"].ToString());
                }

                StringBuilder sb = new StringBuilder();
                sb.Append("select");
                sb.Append(" IncidentID,CommID,IncidentMode,RoomSign,CustName,IncidentDate,IncidentNum ");
                sb.Append(" from ");
                sb.Append(" view_HSPR_IncidentAccept_SearchFilter ");
                sb.Append(" where ");
                sb.AppendFormat(" CommID={0}", CommID);
                sb.AppendFormat(" and IncidentDate>'{0}'", BeginTime);
                sb.AppendFormat(" and IncidentDate<'{0}'", EndTime);
                sb.AppendFormat(" and DispType={0}", DispType);
                sb.Append(" and ISNULL( IsDelete,0)=0");
                if (CoordinateNum != "")
                {
                    sb.AppendFormat(" and CoordinateNum like '{0}'", CoordinateNum);
                }
                if (RoomSign != "")
                {
                    sb.AppendFormat(" and RoomSign like '{0}'", RoomSign);
                }
                if (DealMan != "")
                {
                    sb.AppendFormat(" and DealMan like '{0}'", DealMan);
                }

                sb.Append(" and dbo.funCheckIncidentClassID(TypeID,CommID)>0 ");

                int pageCount;
                int Counts;
                IDbConnection con = new SqlConnection(PubConstant.hmWyglConnectionString);
                DataTable dt = BussinessCommon.GetList(out pageCount, out Counts, sb.ToString(), PageIndex, PageSize, "IncidentDate", 1, "IncidentID", PubConstant.hmWyglConnectionString, "*").Tables[0];
                con.Dispose();
                return JSONHelper.FromString(dt);

            }
            catch (Exception ex)
            {
                backstr = ex.Message;
            }
            if (backstr == "")
            {
                return JSONHelper.FromString("");
            }
            else
            {
                return JSONHelper.FromString(false, backstr);
            }

        }

        /// <summary>
        /// 延期申请
        /// </summary>
        private string DelayApply(DataRow row)
        {
            // 任务Code
            if (!row.Table.Columns.Contains("IncidentID") || string.IsNullOrEmpty(row["IncidentID"].ToString()))
            {
                return new ApiResult(false, "缺少参数IncidentID").toJson();
            }
            if (!row.Table.Columns.Contains("DelayHours") || string.IsNullOrEmpty(row["DelayHours"].ToString()))
            {
                return new ApiResult(false, "缺少参数DelayHours").toJson();
            }
            if (!row.Table.Columns.Contains("DelayReason") || string.IsNullOrEmpty(row["DelayReason"].ToString()))
            {
                return new ApiResult(false, "缺少参数DelayReason").toJson();
            }

            string IncidentID = row["IncidentID"].ToString();
            int DelayHours = AppGlobal.StrToInt(row["DelayHours"].ToString());
            string DelayReason = row["DelayReason"].ToString();

            if (DelayHours > 720)
            {
                return JSONHelper.FromString(false, "延期时间不能超过30天");
            }

            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                var trans = conn.BeginTransaction();
                try
                {
                    int i = conn.Execute(@"INSERT INTO 
                                    Tb_Incident_DelayApply
                                    (IID,CommID,IncidentID,IncidentNum,Title,WorkStartDate,UserCode,AddHours,UserName,DelayReason)
                                    SELECT
                                    NEWID(),CommID,IncidentID,IncidentNum,@Title,GETDATE(),@UserCode,@DelayHours,@UserName,@DelayReason 
                                    FROM Tb_HSPR_IncidentAccept WHERE IncidentID=@IncidentID;",
                                    new
                                    {
                                        IncidentID = IncidentID,
                                        UserName = Global_Var.LoginUserName,
                                        UserCode = Global_Var.LoginUserCode,
                                        DelayReason = DelayReason,
                                        Title = Global_Var.LoginUserName + "提交的延期申请",
                                        DelayHours = DelayHours,
                                    }, trans);

                    if (i > 0)
                    {
                        int j = conn.Execute(@"UPDATE 
                                                Tb_HSPR_IncidentAccept 
                                                SET DealLimit = ISNULL(DealLimit,0)+@DelayHours WHERE IncidentID=@IncidentID;",
                                    new
                                    {
                                        IncidentID = IncidentID,
                                        DelayHours = DelayHours,
                                    }, trans);
                        if (j > 0)
                        {
                            trans.Commit();
                            return JSONHelper.FromString(true, "申请成功");
                        }

                        trans.Rollback();
                        return JSONHelper.FromString(false, "申请失败未找到对应报事");
                    }

                    trans.Rollback();
                    return JSONHelper.FromString(false, "申请失败");
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    return JSONHelper.FromString(false, ex.Message + Environment.NewLine + ex.StackTrace);
                }
            }
        }

        /// <summary>
        /// 报事转发
        /// </summary>
        private string IncidentTransmit(DataRow row)
        {
            #region 基础数据校验
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].AsString()))
            {
                return new ApiResult(false, "缺少参数CommID").toJson();
            }
            if (!row.Table.Columns.Contains("IncidentID") || string.IsNullOrEmpty(row["IncidentID"].AsString()))
            {
                return new ApiResult(false, "缺少参数IncidentID").toJson();
            }
            if (!row.Table.Columns.Contains("TypeID") || string.IsNullOrEmpty(row["TypeID"].AsString()))
            {
                return new ApiResult(false, "缺少参数TypeID").toJson();
            }
            if (!row.Table.Columns.Contains("DealMan") || string.IsNullOrEmpty(row["DealMan"].AsString()))
            {
                return new ApiResult(false, "缺少参数DealMan").toJson();
            }
            if (!row.Table.Columns.Contains("DealManUserCode") || string.IsNullOrEmpty(row["DealManUserCode"].AsString()))
            {
                return new ApiResult(false, "缺少参数DealManUserCode").toJson();
            }
            if (!row.Table.Columns.Contains("TransmitReason") || string.IsNullOrEmpty(row["TransmitReason"].AsString()))
            {
                return new ApiResult(false, "缺少参数TransmitReason").toJson();
            }

            #endregion

            string commId = row["CommID"].AsString();
            string incidentId = row["IncidentID"].AsString();
            string typeID = row["TypeID"].AsString();
            string dealMan = row["DealMan"].AsString();
            string dealManUserCode = row["DealManUserCode"].AsString();
            string transmitReason = row["TransmitReason"].AsString();

            transmitReason = transmitReason.Replace("0x2B", "+");
            transmitReason = transmitReason.Replace("0x3C", "<");

            using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                dynamic state = conn.Query(@"SELECT DealState,IsDelete FROM Tb_HSPR_IncidentAccept WHERE IncidentID=@IncidentID",
                    new { IncidentID = incidentId }).FirstOrDefault();
                if (state.DealState != null && state.DealState == 1)
                {
                    return JSONHelper.FromString(false, "报事已完成，无法转发");
                }
                if (state.IsDelete != null && state.IsDelete == 1)
                {
                    return JSONHelper.FromString(false, "报事已删除，无法转发");
                }

                conn.Execute(@"Proc_HSPR_IncidentAccept_Transmit_Phone",
                    new
                    {
                        CommID = commId,
                        IncidentID = incidentId,
                        DispMan = Global_Var.LoginUserName,
                        DispUserCode = Global_Var.LoginUserCode,
                        TypeID = typeID,
                        DealMan = dealMan,
                        DealManUserCode = dealManUserCode,
                        TransmitReason = transmitReason
                    }, null, null, CommandType.StoredProcedure);


                //// 推送消息
                //var incidentInfo = conn.Query<Tb_HSPR_IncidentAccept>(@"SELECT * FROM Tb_HSPR_IncidentAccept WHERE IncidentID=@IncidentID",
                //    new { IncidentID = incidentId }).FirstOrDefault();

                //if (incidentInfo != null)
                //{
                //    IncidentAcceptPush.SynchPushWhenTransmit(incidentInfo);
                //}

                return JSONHelper.FromString(true, "转发成功");
            }
        }

        /// <summary>
        /// 转发历史
        /// </summary>
        private string IncidentTransmitHistory(DataRow row)
        {
            if (!row.Table.Columns.Contains("IncidentID") || string.IsNullOrEmpty(row["IncidentID"].AsString()))
            {
                return new ApiResult(false, "缺少参数IncidentID").toJson();
            }

            string commId = row["CommID"].AsString();
            string incidentId = row["IncidentID"].AsString();

            int page = 1;
            int size = 10;
            if (row.Table.Columns.Contains("PageIndex") && !string.IsNullOrEmpty(row["PageIndex"].ToString()))
            {
                page = Convert.ToInt32(row["PageIndex"]);
            }
            if (row.Table.Columns.Contains("PageSize") && !string.IsNullOrEmpty(row["PageSize"].ToString()))
            {
                size = Convert.ToInt32(row["PageSize"]);
            }

            var sql = $@"SELECT IID,IncidentID,TransmitReasons,TypeName,PersonLiableName,TransmitDateTime,b.UserName AS TransmitUserName
                         FROM Tb_HSPR_IncidentTransmit a LEFT JOIN Tb_Sys_User b ON a.TransmitUserCode=b.UserCode
                         WHERE IncidentID={incidentId}";

            DataSet ds = GetList(out int PageCount, out int Counts, sql, page, size, "TransmitDateTime", 1, "IncidentID", PubConstant.hmWyglConnectionString);

            string result = null;

            if (ds == null || ds.Tables.Count == 0)
            {
                result = new ApiResult(true, new string[] { }).toJson();
            }
            result = JSONHelper.FromString(ds.Tables[0]);

            return result.Insert(result.Length - 1, ",\"PageCount\":" + PageCount);
        }

        #endregion

        #endregion

        #region 公共
        public static DataSet IncidentAssignSearch(string Fileld, out int PageCount, out int Counts, string StrCondition, int PageIndex, int PageSize, string SortField, int Sort)
        {
            DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString.ToString();
            SqlParameter[] parameters = {
                    new SqlParameter("@FldName", SqlDbType.VarChar, 255),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@FldSort", SqlDbType.VarChar, 1000),
                    new SqlParameter("@Sort", SqlDbType.Int),
                    new SqlParameter("@StrCondition", SqlDbType.VarChar, 8000),
                    new SqlParameter("@Id", SqlDbType.VarChar, 50),
                    new SqlParameter("@PageCount", SqlDbType.Int, 4,ParameterDirection.Output, false, 0, 0,string.Empty, DataRowVersion.Default, null),
                    new SqlParameter("@Counts", SqlDbType.Int, 4,ParameterDirection.Output, false, 0, 0,string.Empty, DataRowVersion.Default, null),
                    };
            parameters[0].Value = Fileld;
            parameters[1].Value = PageSize;
            parameters[2].Value = PageIndex;
            parameters[3].Value = SortField;
            parameters[4].Value = Sort;
            parameters[5].Value = "SELECT * FROM (SELECT B.*, dbo.funGetProcessRoleCode(B.TypeID) AS ProcessRoleID, dbo.funGetAssignedRoleCode(B.TypeID) AS AssignedRoleID FROM view_HSPR_IncidentAccept_Filter AS B) AS A WHERE 1=1 " + StrCondition;
            parameters[6].Value = "IncidentID";
            DataSet Ds = DbHelperSQL.RunProcedure("Proc_System_TurnPage", parameters, "RetDataSet");
            PageCount = Convert.ToInt32(parameters[7].Value);
            Counts = Convert.ToInt32(parameters[8].Value);
            return Ds;
        }

        private DataTable GetTypeList(string SQL, string str)
        {
            IDbConnection con = new SqlConnection(str);

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@SQLEx", SQL);

            DataSet ds = con.ExecuteReader("Proc_HSPR_IncidentType_Filter", parameters, null, null, CommandType.StoredProcedure).ToDataSet();
            return ds.Tables[0];
        }


        #region 得到分派单号
        /// <summary>
        /// 得到分派单号HSPR_IncidentAssigned_GetCoordinateNum
        /// </summary>
        /// <param name="CommID">小区编码</param>
        /// <param name="IncidentType"></param>
        /// <param name="IncidentHead"></param>
        /// CommID              小区编码
        /// IncidentType        1：派工单；2：协调单
        /// IncidentHead        派工单：P；协调单：X
        /// <returns></returns>
        public string HSPR_IncidentAssigned_GetCoordinateNum(string CommID, int IncidentType, string IncidentHead)
        {
            IDbConnection con = new SqlConnection(PubConstant.hmWyglConnectionString);

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@CommID", CommID);
            parameters.Add("@IncidentType", IncidentType);
            parameters.Add("@IncidentHead", IncidentHead);

            DataTable dTable = con.ExecuteReader("Proc_HSPR_IncidentAssigned_GetCoordinateNum", parameters, null, null, CommandType.StoredProcedure).ToDataSet().Tables[0];

            string strIncidentSign = "";
            if (dTable.Rows.Count > 0)
            {
                strIncidentSign = dTable.Rows[0][0].ToString();
            }
            con.Dispose();
            return strIncidentSign;
        }

        /// <summary>
        /// 派工转协调
        /// </summary>
        /// <param name="Model"></param>
        public void HSPR_IncidentAssignedConversion_Update(Tb_HSPR_IncidentAccept Model)
        {
            IDbConnection con = new SqlConnection(PubConstant.hmWyglConnectionString);

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@IncidentID", Model.IncidentID);
            parameters.Add("@CommID", Model.CommID);
            parameters.Add("@CustID", Model.CustID);
            parameters.Add("@RoomID", Model.RoomID);
            parameters.Add("@TypeID", Model.TypeID);

            parameters.Add("@IncidentNum", Model.IncidentNum);
            parameters.Add("@IncidentPlace", Model.IncidentPlace);
            parameters.Add("@IncidentMan", Model.IncidentMan);
            parameters.Add("@IncidentDate", Model.IncidentDate);
            parameters.Add("@IncidentMode", Model.IncidentMode);

            parameters.Add("@DealLimit", Model.DealLimit);
            parameters.Add("@IncidentContent", Model.IncidentContent);
            parameters.Add("@ReserveDate", Model.ReserveDate);
            parameters.Add("@ReserveLimit", Model.ReserveLimit);
            parameters.Add("@Phone", Model.Phone);

            parameters.Add("@AdmiMan", Model.AdmiMan);
            parameters.Add("@AdmiDate", Model.AdmiDate);
            parameters.Add("@DispType", Model.DispType);
            parameters.Add("@DispMan", Model.DispMan);
            parameters.Add("@DispDate", Model.DispDate);

            parameters.Add("@DealMan", Model.DealMan);
            parameters.Add("@CoordinateNum", Model.CoordinateNum);
            parameters.Add("@Reasons", Model.Reasons);
            parameters.Add("@RegionalID", Model.RegionalID);
            parameters.Add("@DeviceID", Model.DeviceID);

            con.ExecuteScalar("Proc_HSPR_IncidentAccept_Update_AssignedConversion", parameters, null, null, CommandType.StoredProcedure);
            con.Dispose();
        }

        /// <summary>
        /// 派工单新增
        /// </summary>
        /// <param name="Model"></param>
        public void HSPR_IncidentAssigned_Update(Tb_HSPR_IncidentAccept Model)
        {
            IDbConnection con = new SqlConnection(PubConstant.hmWyglConnectionString);

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@IncidentID", Model.IncidentID);
            parameters.Add("@IncidentContent", Model.IncidentContent);
            parameters.Add("@ReserveLimit", Model.ReserveLimit);
            parameters.Add("@DispType", Model.DispType);
            parameters.Add("@DispMan", Model.DispMan);

            parameters.Add("@DispDate", Model.DispDate);
            parameters.Add("@DealMan", Model.DealMan);
            parameters.Add("@CoordinateNum", Model.CoordinateNum);
            parameters.Add("@TypeID", Model.TypeID);
            parameters.Add("@ReceivingDate", Model.ReceivingDate);

            con.ExecuteScalar("Proc_HSPR_IncidentAccept_Update_Assigned_Phone", parameters, null, null, CommandType.StoredProcedure);
            con.Dispose();
        }

        public void HSPR_IncidentAcceptChange_Insert(Tb_HSPR_IncidentAcceptChange Model, string commid)
        {
            IDbConnection con = new SqlConnection(PubConstant.hmWyglConnectionString);

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@IncidentID", Model.IncidentID);
            parameters.Add("@ChangeDate", Model.ChangeDate);
            parameters.Add("@ChangeUserCode", Model.ChangeUserCode);

            con.ExecuteScalar("Proc_HSPR_IncidentAccept_Change_Insert", parameters, null, null, CommandType.StoredProcedure);
            con.Dispose();
        }

        public void HSPR_IncidentUpdateDispManUserCode(object IncidentID, string CommID, object UserCode, object UserName)
        {
            IDbConnection con = new SqlConnection(PubConstant.hmWyglConnectionString);

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@IncidentID", IncidentID);
            parameters.Add("@CommID", CommID);
            parameters.Add("@UserCode", UserCode);
            parameters.Add("@UserName", UserName);

            con.ExecuteScalar("Proc_HSPR_IncidentUpdateDispManUserCode", parameters, null, null, CommandType.StoredProcedure);
            con.Dispose();


        }

        public void HSPR_IncidentAcceptDeal_Delete(string CommID, object IncidentID)
        {
            IDbConnection con = new SqlConnection(PubConstant.hmWyglConnectionString);
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@CommID", CommID);
            parameters.Add("@IncidentID", IncidentID);


            con.ExecuteScalar("Proc_HSPR_IncidentAcceptDeal_Delete", parameters, null, null, CommandType.StoredProcedure);
            con.Dispose();
        }

        public void HSPR_IncidentAcceptDeal_Insert(string strcon, object CommID, object IncidentID, string TypeID, string UserCode)
        {

            IDbConnection con = new SqlConnection(strcon);
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@CommID", CommID);
            parameters.Add("@IncidentID", IncidentID);
            parameters.Add("@TypeID", TypeID.Trim(','));
            parameters.Add("@UserCode", UserCode);

            con.ExecuteScalar("Proc_HSPR_IncidentAcceptDeal_Insert", parameters, null, null, CommandType.StoredProcedure);
            con.Dispose();


        }
        #endregion

        #endregion

    }
}

