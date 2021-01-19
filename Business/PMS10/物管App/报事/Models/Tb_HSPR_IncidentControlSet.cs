using System;
using Business.PMS10.物管App.报事.Enum;
using Newtonsoft.Json;

namespace Business.PMS10.物管App.报事.Models
{
    /// <summary>
    /// Tb_HSPR_IncidentControlSet的模型
    /// </summary>
    [Serializable()]
    public class Tb_HSPR_IncidentControlSet
    {
        private int iID;
        private long defaultIndoorIncidentAcceptTypeID;
        private long defaultPublicIncidentAcceptTypeID;
        private long defaultNotifyOpenWaterIncidentAcceptTypeID;
        private long defaultNotifyOpenPowerIncidentAcceptTypeID;
        private long defaultLetPersonPassIncidentAcceptTypeID;
        private long defaultLetCarPassIncidentAcceptTypeID;
        private bool allowCustomerSelectDealMan;
        private PMSUserOnlineStatus customerSelectDealManOnlineStatus;
        private int customerSelectDealManMaxOrderLimit;
        private bool allowCrossRegionWorkOrder;
        private int dealManMaxOrderLimit;
        private PMSUserOnlineStatus systemAssignSelectDealManOnlineStatus;
        private int systemAssignSelectDealManMaxOrderLimit;
        private PMSImageSourceMode incidentDealSelectImgMode;
        private bool incidentFeesNeedConfirm;
        private PMSIncidentSource incidentReplyIncidentSource;
        private PMSIncidentPlace incidentReplyIncidentPlace;
        private string incidentComprehensiveEvaluationOptional;
        private string incidentOtherEvaluationOptional;
        private int incidentWarningStatisticsInterval;

        public Tb_HSPR_IncidentControlSet() { }

        public int IID
        {
            get { return this.iID; }
            set { this.iID = value; }
        }

        /// <summary>
        /// 业主端户内报事系统默认报事大类
        /// </summary>
        public long DefaultIndoorIncidentAcceptTypeID
        {
            get { return this.defaultIndoorIncidentAcceptTypeID; }
            set { this.defaultIndoorIncidentAcceptTypeID = value; }
        }

        /// <summary>
        /// 业主端公区报事系统默认报事大类
        /// </summary>
        public long DefaultPublicIncidentAcceptTypeID
        {
            get { return this.defaultPublicIncidentAcceptTypeID; }
            set { this.defaultPublicIncidentAcceptTypeID = value; }
        }

        /// <summary>
        /// 通知开水
        /// </summary>
        public long DefaultNotifyOpenWaterIncidentAcceptTypeID
        {
            get { return this.defaultNotifyOpenWaterIncidentAcceptTypeID; }
            set { this.defaultNotifyOpenWaterIncidentAcceptTypeID = value; }
        }

        /// <summary>
        /// 通知开电
        /// </summary>
        public long DefaultNotifyOpenPowerIncidentAcceptTypeID
        {
            get { return this.defaultNotifyOpenPowerIncidentAcceptTypeID; }
            set { this.defaultNotifyOpenPowerIncidentAcceptTypeID = value; }
        }

        /// <summary>
        /// 人员放行
        /// </summary>
        public long DefaultLetPersonPassIncidentAcceptTypeID
        {
            get { return this.defaultLetPersonPassIncidentAcceptTypeID; }
            set { this.defaultLetPersonPassIncidentAcceptTypeID = value; }
        }

        /// <summary>
        /// 车辆放行
        /// </summary>
        public long DefaultLetCarPassIncidentAcceptTypeID
        {
            get { return this.defaultLetCarPassIncidentAcceptTypeID; }
            set { this.defaultLetCarPassIncidentAcceptTypeID = value; }
        }

        /// <summary>
        /// 业主端报事是否允许选择人员
        /// </summary>
        public bool AllowCustomerSelectDealMan
        {
            get { return this.allowCustomerSelectDealMan; }
            set { this.allowCustomerSelectDealMan = value; }
        }

        /// <summary>
        /// 业主端报事客户选择人员登录状态
        /// </summary>
        public PMSUserOnlineStatus CustomerSelectDealManOnlineStatus
        {
            get { return this.customerSelectDealManOnlineStatus; }
            set { this.customerSelectDealManOnlineStatus = value; }
        }

        /// <summary>
        /// 业主端报事选择人员在办工单限制
        /// </summary>
        public int CustomerSelectDealManMaxOrderLimit
        {
            get { return this.customerSelectDealManMaxOrderLimit; }
            set { this.customerSelectDealManMaxOrderLimit = value; }
        }

        /// <summary>
        /// 是否开通区域工单
        /// </summary>
        public bool AllowCrossRegionWorkOrder
        {
            get { return this.allowCrossRegionWorkOrder; }
            set { this.allowCrossRegionWorkOrder = value; }
        }

        /// <summary>
        /// 员工抢单时在办工单限制
        /// </summary>
        public int DealManMaxOrderLimit
        {
            get { return this.dealManMaxOrderLimit; }
            set { this.dealManMaxOrderLimit = value; }
        }

        /// <summary>
        /// 系统派单选择人员登录状态
        /// </summary>
        public PMSUserOnlineStatus SystemAssignSelectDealManOnlineStatus
        {
            get { return this.systemAssignSelectDealManOnlineStatus; }
            set { this.systemAssignSelectDealManOnlineStatus = value; }
        }

        /// <summary>
        /// 系统派单选择人员在办工单限制
        /// </summary>
        public int SystemAssignSelectDealManMaxOrderLimit
        {
            get { return this.systemAssignSelectDealManMaxOrderLimit; }
            set { this.systemAssignSelectDealManMaxOrderLimit = value; }
        }

        /// <summary>
        /// 到场、完成时照片上传方式
        /// </summary>
        public PMSImageSourceMode IncidentDealSelectImgMode
        {
            get { return this.incidentDealSelectImgMode; }
            set { this.incidentDealSelectImgMode = value; }
        }

        /// <summary>
        /// 有偿服务是否需要事前确认
        /// </summary>
        public bool IncidentFeesNeedConfirm
        {
            get { return this.incidentFeesNeedConfirm; }
            set { this.incidentFeesNeedConfirm = value; }
        }

        /// <summary>
        /// 报事回访限制报事来源
        /// </summary>
        public PMSIncidentSource IncidentReplyIncidentSource
        {
            get { return this.incidentReplyIncidentSource; }
            set { this.incidentReplyIncidentSource = value; }
        }

        /// <summary>
        /// 报事回访限制报事位置
        /// </summary>
        public PMSIncidentPlace IncidentReplyIncidentPlace
        {
            get { return this.incidentReplyIncidentPlace; }
            set { this.incidentReplyIncidentPlace = value; }
        }

        /// <summary>
        /// 报事回访综合满意度评价维度
        /// </summary>
        public string IncidentComprehensiveEvaluationOptional
        {
            get { return this.incidentComprehensiveEvaluationOptional; }
            set { this.incidentComprehensiveEvaluationOptional = value; }
        }

        /// <summary>
        /// 报事回访其他评价维度
        /// </summary>
        public string IncidentOtherEvaluationOptional
        {
            get { return this.incidentOtherEvaluationOptional; }
            set { this.incidentOtherEvaluationOptional = value; }
        }

        /// <summary>
        /// 系统派单、报事预警统计时间周期
        /// </summary>
        public int IncidentWarningStatisticsInterval
        {
            get { return this.incidentWarningStatisticsInterval; }
            set { this.incidentWarningStatisticsInterval = value; }
        }

        /// <summary>
        /// 默认管控配置
        /// </summary>
        public static Tb_HSPR_IncidentControlSet Default => new Tb_HSPR_IncidentControlSet
        {
            AllowCustomerSelectDealMan = false,
            CustomerSelectDealManMaxOrderLimit = 99,
            AllowCrossRegionWorkOrder = false,
            DealManMaxOrderLimit = 99,
            SystemAssignSelectDealManMaxOrderLimit = 99,
            IncidentFeesNeedConfirm = true,
            IncidentWarningStatisticsInterval = 5,
            CustomerSelectDealManOnlineStatus = PMSUserOnlineStatus.All,
            SystemAssignSelectDealManOnlineStatus = PMSUserOnlineStatus.All,
            IncidentDealSelectImgMode = PMSImageSourceMode.TakePicture | PMSImageSourceMode.FromAlbum,
            IncidentReplyIncidentSource = PMSIncidentSource.FromCustomer | PMSIncidentSource.FromEmployee,
            IncidentReplyIncidentPlace = PMSIncidentPlace.Indoor | PMSIncidentPlace.Public,
            IncidentComprehensiveEvaluationOptional = "非常满意,满意,一般,不满意,非常不满意",
            IncidentOtherEvaluationOptional = "服务是否及时,问题是否解决,言行是否合规"
        };
    }

    /// <summary>
    /// Tb_HSPR_IncidentControlModelSet的模型
    /// </summary>
    [Serializable()]
    public class Tb_HSPR_IncidentControlModelSet
    {
        private int iID;
        private long typeCode_Accep;
        private long pubilcTypeCode_Accep;

        private long defaultNotifyOpenWaterIncidentAcceptTypeID;
        private long defaultNotifyOpenPowerIncidentAcceptTypeID;
        private long defaultLetPersonPassIncidentAcceptTypeID;
        private long defaultLetCarPassIncidentAcceptTypeID;

        private string isSelPeople_Accep;
        private string employeeStatus_Accep;
        private string condition_Accep;
        private int num_Accep;
        private string isOrge_Assigned;
        private string condition_Assigned;
        private int num_Assigned;
        private string employeeStatus_Assigned;
        private string condition2_Assigned;
        private int num2_Assigned;
        private string isUploadImg_Handle;
        private string isConfirm_Handle;
        private string source_Reply;
        private string position_Reply;
        private int checkbox_1;
        private int checkbox_2;
        private int checkbox_3;
        private int checkbox_4;
        private int checkbox_5;
        private int checkbox_6;
        private int checkbox_7;
        private int checkbox_8;
        private int checkbox_9;
        private int minute;

        public Tb_HSPR_IncidentControlModelSet() { }

        public int IID
        {
            get { return this.iID; }
            set { this.iID = value; }
        }

        public long TypeCode_Accep
        {
            get { return this.typeCode_Accep; }
            set { this.typeCode_Accep = value; }
        }

        public long PubilcTypeCode_Accep
        {
            get { return this.pubilcTypeCode_Accep; }
            set { this.pubilcTypeCode_Accep = value; }
        }



        /// <summary>
        /// 通知开水
        /// </summary>
        public long DefaultNotifyOpenWaterIncidentAcceptTypeID
        {
            get { return this.defaultNotifyOpenWaterIncidentAcceptTypeID; }
            set { this.defaultNotifyOpenWaterIncidentAcceptTypeID = value; }
        }

        /// <summary>
        /// 通知开电
        /// </summary>
        public long DefaultNotifyOpenPowerIncidentAcceptTypeID
        {
            get { return this.defaultNotifyOpenPowerIncidentAcceptTypeID; }
            set { this.defaultNotifyOpenPowerIncidentAcceptTypeID = value; }
        }

        /// <summary>
        /// 人员放行
        /// </summary>
        public long DefaultLetPersonPassIncidentAcceptTypeID
        {
            get { return this.defaultLetPersonPassIncidentAcceptTypeID; }
            set { this.defaultLetPersonPassIncidentAcceptTypeID = value; }
        }

        /// <summary>
        /// 车辆放行
        /// </summary>
        public long DefaultLetCarPassIncidentAcceptTypeID
        {
            get { return this.defaultLetCarPassIncidentAcceptTypeID; }
            set { this.defaultLetCarPassIncidentAcceptTypeID = value; }
        }

        public string IsSelPeople_Accep
        {
            get { return this.isSelPeople_Accep; }
            set { this.isSelPeople_Accep = value; }
        }

        public string EmployeeStatus_Accep
        {
            get { return this.employeeStatus_Accep; }
            set { this.employeeStatus_Accep = value; }
        }

        public string Condition_Accep
        {
            get { return this.condition_Accep; }
            set { this.condition_Accep = value; }
        }

        public int Num_Accep
        {
            get { return this.num_Accep; }
            set { this.num_Accep = value; }
        }

        public string IsOrge_Assigned
        {
            get { return this.isOrge_Assigned; }
            set { this.isOrge_Assigned = value; }
        }

        public string Condition_Assigned
        {
            get { return this.condition_Assigned; }
            set { this.condition_Assigned = value; }
        }

        public int Num_Assigned
        {
            get { return this.num_Assigned; }
            set { this.num_Assigned = value; }
        }

        public string EmployeeStatus_Assigned
        {
            get { return this.employeeStatus_Assigned; }
            set { this.employeeStatus_Assigned = value; }
        }

        public string Condition2_Assigned
        {
            get { return this.condition2_Assigned; }
            set { this.condition2_Assigned = value; }
        }

        public int Num2_Assigned
        {
            get { return this.num2_Assigned; }
            set { this.num2_Assigned = value; }
        }

        public string IsUploadImg_Handle
        {
            get { return this.isUploadImg_Handle; }
            set { this.isUploadImg_Handle = value; }
        }

        public string IsConfirm_Handle
        {
            get { return this.isConfirm_Handle; }
            set { this.isConfirm_Handle = value; }
        }

        public string Source_Reply
        {
            get { return this.source_Reply; }
            set { this.source_Reply = value; }
        }

        public string Position_Reply
        {
            get { return this.position_Reply; }
            set { this.position_Reply = value; }
        }

        public int Checkbox_1
        {
            get { return this.checkbox_1; }
            set { this.checkbox_1 = value; }
        }

        public int Checkbox_2
        {
            get { return this.checkbox_2; }
            set { this.checkbox_2 = value; }
        }

        public int Checkbox_3
        {
            get { return this.checkbox_3; }
            set { this.checkbox_3 = value; }
        }

        public int Checkbox_4
        {
            get { return this.checkbox_4; }
            set { this.checkbox_4 = value; }
        }

        public int Checkbox_5
        {
            get { return this.checkbox_5; }
            set { this.checkbox_5 = value; }
        }

        public int Checkbox_6
        {
            get { return this.checkbox_6; }
            set { this.checkbox_6 = value; }
        }

        public int Checkbox_7
        {
            get { return this.checkbox_7; }
            set { this.checkbox_7 = value; }
        }

        public int Checkbox_8
        {
            get { return this.checkbox_8; }
            set { this.checkbox_8 = value; }
        }

        public int Checkbox_9
        {
            get { return this.checkbox_9; }
            set { this.checkbox_9 = value; }
        }

        public int Minute
        {
            get { return this.minute; }
            set { this.minute = value; }
        }


        public static Tb_HSPR_IncidentControlModelSet Default => new Tb_HSPR_IncidentControlModelSet()
        {
            IsSelPeople_Accep = "否",
            EmployeeStatus_Accep = "在线",
            IsOrge_Assigned = "否",

        };
    }
}
