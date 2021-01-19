using System;
using System.Collections.Generic;

namespace MobileSoft.Common
{
    /// <summary>
    /// 推送命令
    /// </summary>
    public static partial class PushCommand
    {
        /// <summary>
        /// 报事受理
        /// </summary>
        public static readonly string INCIDENT_ACCEPT = "INCIDENT_ACCEPT";

        /// <summary>
        /// 报事接单
        /// </summary>
        public static readonly string INCIDENT_RECEIVING = "INCIDENT_RECEIVING";

        /// <summary>
        /// 报事分派
        /// </summary>
        public static readonly string INCIDENT_ASSIGN = "INCIDENT_ASSIGN";

        /// <summary>
        /// 报事退回
        /// </summary>
        public static readonly string INCIDENT_BACK = "INCIDENT_BACK";

        /// <summary>
        /// 报事抢单
        /// </summary>
        public static readonly string INCIDENT_SNATCH = "INCIDENT_SNATCH";

        /// <summary>
        /// 报事已关闭
        /// </summary>
        public static readonly string INCIDENT_CLOSED = "INCIDENT_CLOSED";

        /// <summary>
        /// 报事协助审核
        /// </summary>
        public static readonly string INCIDENT_APPLY_ASSIST = "INCIDENT_APPLY_ASSIST";

        /// <summary>
        /// 报事延期审核
        /// </summary>
        public static readonly string INCIDENT_APPLY_DELAY = "INCIDENT_APPLY_DELAY";

        /// <summary>
        /// 报事非正常关闭审核
        /// </summary>
        public static readonly string INCIDENT_APPLY_UNNORMAL_CLOSE = "INCIDENT_APPLY_UNNORMAL_CLOSED";

        /// <summary>
        /// 报事废弃
        /// </summary>
        public static readonly string INCIDENT_DELETE = "INCIDENT_DELETE";

        /// <summary>
        /// 报事催办
        /// </summary>
        public static readonly string INCIDENT_URGING = "INCIDENT_URGING";

        /// <summary>
        /// 报事抢单&派单
        /// </summary>
        public static readonly string INCIDENT_SNATCH_ASSIGN = "INCIDENT_SNATCH_ASSIGN";

        /// <summary>
        /// 报事处理，预计到场
        /// </summary>
        public static readonly string INCIDENT_PROCESSING_PLAN_ARRIVE = "INCIDENT_PROCESSING_PLAN_ARRIVE";

        /// <summary>
        /// 报事处理
        /// </summary>
        public static readonly string INCIDENT_PROCESSING = "INCIDENT_PROCESSING";

        /// <summary>
        /// 报事转发
        /// </summary>
        public static readonly string INCIDENT_TRANSMIT = "INCIDENT_TRANSMIT";

        /// <summary>
        /// 报事关闭
        /// </summary>
        public static readonly string INCIDENT_CLOSE = "INCIDENT_CLOSE";

        /// <summary>
        /// 报事关闭后回访时退回
        /// </summary>
        public static readonly string INCIDENT_GOBACK_AFTERCLOSE = "INCIDENT_GOBACK_AFTERCLOSE";

        /// <summary>
        /// 报事处理完结
        /// </summary>
        public static readonly string INCIDENT_END = "INCIDENT_END";

        /// <summary>
        /// 业主取消报事
        /// </summary>
        public static readonly string INCIDENT_CUSTCANCEL = "INCIDENT_CUSTCANCEL";

        /// <summary>
        /// 总部工作
        /// </summary>
        public static readonly string NEWS_ORGANIZATION = "NEWS_ORGANIZATION";

        /// <summary>
        /// 公司工作
        /// </summary>
        public static readonly string NEWS_COMPANY = "NEWS_COMPANY";

        /// <summary>
        /// 社区公告
        /// </summary>
        public static readonly string NEWS_COMMUNITY = "NEWS_COMMUNITY";

        /// <summary>
        /// 平台公告
        /// </summary>
        public static readonly string NEWS_PLATFORM = "NEWS_PLATFORM";

        /// <summary>
        /// 房下新增账号
        /// </summary>
        public static readonly string NEW_ROOM_RELATION = "NEW_ROOM_RELATION";

        /// <summary>
        /// 新回复提醒
        /// </summary>
        public static readonly string BBS_REPLY = "BBS_REPLY";

        /// <summary>
        /// ERP在线消息
        /// </summary>
        public static readonly string ERP_SMS = "ERP_SMS";

        /// <summary>
        /// 办公审批流程
        /// </summary>
        public static readonly string OFFICE_WORKFLOW = "OFFICE_WORKFLOW";

        /// <summary>
        /// 办公审批流程结束
        /// </summary>
        public static readonly string OFFICE_WORKFLOW_END = "OFFICE_WORKFLOW_END";


        /// <summary>
        /// 提醒
        /// </summary>
        public static readonly string NORMAL = "NORMAL";

        /// <summary>
        /// 品质整改
        /// </summary>
        public static readonly string QUALITY_ABARBEITUNG = "QUALITY_ABARBEITUNG";

        /// <summary>
        /// 品质或设备任务生成
        /// </summary>
        public static readonly string QUALITY_EQUIPMENT_GENERATE = "QUALITY_EQUIPMENT_GENERATE";

        // ------------------------ 合景版本，新增相关命令
        /// <summary>
        /// 协商升级
        /// </summary>
        public static readonly string INCIDENT_UPGRADE = "INCIDENT_UPGRADE";


        #region 品质
        /// <summary>
        /// 品质核查，添加临时任务
        /// </summary>
        public static readonly string CP_TASK_CREATE = "CP_TASK_CREATE";

        /// <summary>
        /// 品质核查，删除临时任务
        /// </summary>
        public static readonly string CP_TASK_REMOVE = "CP_TASK_REMOVE";

        /// <summary>
        /// 品质核查，临时任务完成，通知任务创建人，离线任务完成，通知任务汇报岗位或人
        /// </summary>
        public static readonly string CP_TASK_COMPLETE = "CP_TASK_COMPLETE";

        /// <summary>
        /// 品质核查，临时任务添加点位
        /// </summary>
        public static readonly string CP_TASK_POINT_ADD = "CP_TASK_POINT_ADD";

        /// <summary>
        /// 品质核查，临时任务删除点位
        /// </summary>
        public static readonly string CP_TASK_POINT_REMOVE = "CP_TASK_POINT_REMOVE";

        /// <summary>
        /// 品质核查，任务催办
        /// </summary>
        public static readonly string CP_TASK_URGE = "CP_TASK_URGE";

        #endregion

        #region 巡查

        /// <summary>
        /// 公区巡查
        /// </summary>
        public static readonly string PATROL_TASK_COMPLETE_1 = "PATROL_TASK_COMPLETE_1";

        /// <summary>
        /// 空房巡查
        /// </summary>

        public static readonly string PATROL_TASK_COMPLETE_2 = "PATROL_TASK_COMPLETE_2";

        /// <summary>
        /// 装修巡查
        /// </summary>

        public static readonly string PATROL_TASK_COMPLETE_3 = "PATROL_TASK_COMPLETE_3";

        /// <summary>
        /// 公区巡查，任务催办
        /// </summary>
        public static readonly string PATROL_TASK_URGE_1 = "PATROL_TASK_URGE_1";

        /// <summary>
        /// 空房巡查，任务催办
        /// </summary>
        public static readonly string PATROL_TASK_URGE_2 = "PATROL_TASK_URGE_2";

        /// <summary>
        /// 装修巡查，任务催办
        /// </summary>
        public static readonly string PATROL_TASK_URGE_3 = "PATROL_TASK_URGE_3";

        #endregion

        #region 安全
        /// <summary>
        /// 安全巡更
        /// </summary>
        public static readonly string SAFE_TASK_COMPLETE_1 = "SAFE_TASK_COMPLETE_1";

        /// <summary>
        /// 消防巡检
        /// </summary>
        public static readonly string EQ_TASK_COMPLETE_3 = "EQ_TASK_COMPLETE_3";

        /// <summary>
        /// 安全巡更，任务催办
        /// </summary>
        public static readonly string SAFE_TASK_URGE_1 = "SAFE_TASK_URGE_1";

        /// <summary>
        /// 消防巡检，任务催办
        /// </summary>
        public static readonly string EQ_TASK_URGE_3 = "EQ_TASK_URGE_3";
        #endregion

        #region 设备
        /// <summary>
        /// 设备巡检
        /// </summary>
        public static readonly string EQ_TASK_COMPLETE_1 = "EQ_TASK_COMPLETE_1";

        /// <summary>
        /// 设备维保
        /// </summary>
        public static readonly string EQ_TASK_COMPLETE_2 = "EQ_TASK_COMPLETE_2";

        /// <summary>
        /// 设备巡检，任务催办
        /// </summary>
        public static readonly string EQ_TASK_URGE_1 = "EQ_TASK_URGE_1";

        /// <summary>
        /// 设备维保，任务催办
        /// </summary>
        public static readonly string EQ_TASK_URGE_2 = "EQ_TASK_URGE_2";
        #endregion

        #region 环境
        /// <summary>
        /// 环境保洁
        /// </summary>
        public static readonly string ENV_TASK_COMPLETE_1 = "ENV_TASK_COMPLETE_1";

        /// <summary>
        /// 绿化养护
        /// </summary>
        public static readonly string ENV_TASK_COMPLETE_2 = "ENV_TASK_COMPLETE_2";

        /// <summary>
        /// 四害消杀
        /// </summary>
        public static readonly string ENV_TASK_COMPLETE_3 = "ENV_TASK_COMPLETE_3";

        /// <summary>
        /// 垃圾清运
        /// </summary>
        public static readonly string ENV_TASK_COMPLETE_4 = "ENV_TASK_COMPLETE_4";

        /// <summary>
        /// 环境保洁，任务催办
        /// </summary>
        public static readonly string ENV_TASK_URGE_1 = "ENV_TASK_URGE_1";

        /// <summary>
        /// 绿化养护，任务催办
        /// </summary>
        public static readonly string ENV_TASK_URGE_2 = "ENV_TASK_URGE_2";

        /// <summary>
        /// 四害消杀，任务催办
        /// </summary>
        public static readonly string ENV_TASK_URGE_3 = "ENV_TASK_URGE_3";

        /// <summary>
        /// 垃圾清运，任务催办
        /// </summary>
        public static readonly string ENV_TASK_URGE_4 = "ENV_TASK_URGE_4";

        /// <summary>
        /// 门禁通知
        /// Entrance_Guard_Notice
        /// ENTRANCE_GUARD_NOTICE
        /// </summary>
        public static readonly string Entrance_Guard_Call = "ENTRANCE_GUARD_CALL";


        /// <summary>
        /// 商家端订单通知
        /// Entrance_Guard_Notice
        /// ENTRANCE_GUARD_NOTICE
        /// </summary>
        public static readonly string BusinessCorp_Order = "BusinessCorp_Order";
        #endregion


        /// <summary>
        /// 命令名称字典
        /// </summary>
        public static readonly Dictionary<string, string> CommandNameDict;



        static PushCommand()
        {
            CommandNameDict = new Dictionary<string, string>();
            CommandNameDict.Add(PushCommand.INCIDENT_ACCEPT, "报事受理");
            CommandNameDict.Add(PushCommand.INCIDENT_RECEIVING, "报事接单");
            CommandNameDict.Add(PushCommand.INCIDENT_ASSIGN, "报事派单");
            CommandNameDict.Add(PushCommand.INCIDENT_BACK, "报事退回");
            CommandNameDict.Add(PushCommand.INCIDENT_SNATCH, "报事抢单");
            CommandNameDict.Add(PushCommand.INCIDENT_URGING, "报事催办");
            CommandNameDict.Add(PushCommand.INCIDENT_SNATCH_ASSIGN, "报事抢单派单");
            CommandNameDict.Add(PushCommand.INCIDENT_PROCESSING, "报事处理");
            CommandNameDict.Add(PushCommand.INCIDENT_PROCESSING_PLAN_ARRIVE, "预计到场");
            CommandNameDict.Add(PushCommand.INCIDENT_TRANSMIT, "报事转发");
            CommandNameDict.Add(PushCommand.INCIDENT_CLOSE, "报事关闭");
            CommandNameDict.Add(PushCommand.INCIDENT_GOBACK_AFTERCLOSE, "报事退回");
            CommandNameDict.Add(PushCommand.INCIDENT_END, "报事处理完毕");
            CommandNameDict.Add(PushCommand.INCIDENT_UPGRADE, "报事协商升级");
            CommandNameDict.Add(PushCommand.INCIDENT_CLOSED, "报事已关闭");
            CommandNameDict.Add(PushCommand.INCIDENT_APPLY_ASSIST, "协助审核");
            CommandNameDict.Add(PushCommand.INCIDENT_APPLY_DELAY, "延期审核");
            CommandNameDict.Add(PushCommand.INCIDENT_APPLY_UNNORMAL_CLOSE, "非正常关闭审核");
            CommandNameDict.Add(PushCommand.INCIDENT_DELETE, "报事废弃");

            CommandNameDict.Add(PushCommand.NEWS_ORGANIZATION, "总部工作");
            CommandNameDict.Add(PushCommand.NEWS_COMPANY, "公司工作");
            CommandNameDict.Add(PushCommand.NEWS_COMMUNITY, "社区资讯");
            CommandNameDict.Add(PushCommand.NEWS_PLATFORM, "平台公告");


            CommandNameDict.Add(PushCommand.NEW_ROOM_RELATION, "房屋绑定");
            CommandNameDict.Add(PushCommand.BBS_REPLY, "新回复");

            CommandNameDict.Add(PushCommand.ERP_SMS, "ERP在线短信");
            CommandNameDict.Add(PushCommand.OFFICE_WORKFLOW, "办公审批处理");
            CommandNameDict.Add(PushCommand.OFFICE_WORKFLOW_END, "办公审批流程完毕");

            CommandNameDict.Add(PushCommand.NORMAL, "一般提醒");

            CommandNameDict.Add(PushCommand.QUALITY_ABARBEITUNG, "品质整改");
            CommandNameDict.Add(PushCommand.QUALITY_EQUIPMENT_GENERATE, "品质或设备任务生成");



            CommandNameDict.Add(PushCommand.CP_TASK_CREATE, "添加临时任务");
            CommandNameDict.Add(PushCommand.CP_TASK_REMOVE, "删除临时任务");
            CommandNameDict.Add(PushCommand.CP_TASK_COMPLETE, "品质核查任务完成");
            CommandNameDict.Add(PushCommand.CP_TASK_POINT_ADD, "临时任务添加点位");
            CommandNameDict.Add(PushCommand.CP_TASK_POINT_REMOVE, "临时任务删除点位");
            CommandNameDict.Add(PushCommand.CP_TASK_URGE, "品质核查任务催办");


            CommandNameDict.Add(PushCommand.PATROL_TASK_COMPLETE_1, "公区巡查完成任务");
            CommandNameDict.Add(PushCommand.PATROL_TASK_COMPLETE_2, "空房巡查完成任务");
            CommandNameDict.Add(PushCommand.PATROL_TASK_COMPLETE_3, "装修巡查完成任务");
            CommandNameDict.Add(PushCommand.PATROL_TASK_URGE_1, "公区巡查任务催办");
            CommandNameDict.Add(PushCommand.PATROL_TASK_URGE_2, "空房巡查任务催办");
            CommandNameDict.Add(PushCommand.PATROL_TASK_URGE_3, "装修巡查任务催办");

            CommandNameDict.Add(PushCommand.SAFE_TASK_COMPLETE_1, "安全巡更完成任务");
            CommandNameDict.Add(PushCommand.SAFE_TASK_URGE_1, "安全巡更任务催办");

            CommandNameDict.Add(PushCommand.EQ_TASK_COMPLETE_1, "设备巡检完成任务");
            CommandNameDict.Add(PushCommand.EQ_TASK_COMPLETE_2, "设备维保完成任务");
            CommandNameDict.Add(PushCommand.EQ_TASK_COMPLETE_3, "消防巡检完成任务");
            CommandNameDict.Add(PushCommand.EQ_TASK_URGE_1, "设备巡检任务催办");
            CommandNameDict.Add(PushCommand.EQ_TASK_URGE_2, "设备维保任务催办");
            CommandNameDict.Add(PushCommand.EQ_TASK_URGE_3, "消防巡检任务催办");

            CommandNameDict.Add(PushCommand.ENV_TASK_COMPLETE_1, "环境保洁完成任务");
            CommandNameDict.Add(PushCommand.ENV_TASK_COMPLETE_2, "绿化养护完成任务");
            CommandNameDict.Add(PushCommand.ENV_TASK_COMPLETE_3, "四害消杀完成任务");
            CommandNameDict.Add(PushCommand.ENV_TASK_COMPLETE_4, "垃圾清运完成任务");
            CommandNameDict.Add(PushCommand.ENV_TASK_URGE_1, "环境保洁任务催办");
            CommandNameDict.Add(PushCommand.ENV_TASK_URGE_2, "绿化养护任务催办");
            CommandNameDict.Add(PushCommand.ENV_TASK_URGE_3, "四害消杀任务催办");
            CommandNameDict.Add(PushCommand.ENV_TASK_URGE_4, "垃圾清运任务催办");

            CommandNameDict.Add(PushCommand.Entrance_Guard_Call, "门禁呼叫");
            CommandNameDict.Add(PushCommand.BusinessCorp_Order, "商家端订单通知");
        }
    }
}

