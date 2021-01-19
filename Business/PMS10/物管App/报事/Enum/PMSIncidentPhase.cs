namespace Business.PMS10.物管App.报事.Enum
{
    /// <summary>
    /// 报事生命周期所处阶段
    /// </summary>
    public enum PMSIncidentPhase
    {
        /// <summary>
        /// 已受理待分派
        /// </summary>
        AcceptedWaitingAssign,

        /// <summary>
        /// 分派催办
        /// </summary>
        UrgdWaitingAssign,

        /// <summary>
        /// 已派单待接单
        /// </summary>
        AssignedWaitingReceive,

        /// <summary>
        /// 已接单待到场
        /// </summary>
        ReceivedWaitingArrive,

        /// <summary>
        /// 已到场处理中
        /// </summary>
        ArrivedWaitingDeal,

        /// <summary>
        /// 已处理待关闭
        /// </summary>
        DealtWaitingClose,

        /// <summary>
        /// 已关闭待回访
        /// </summary>
        ClosedWaitingReply,

        /// <summary>
        /// 已回访
        /// </summary>
        Replied,

        /// <summary>
        /// 已废弃
        /// </summary>
        Deleted
    }
}
