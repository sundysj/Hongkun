using System.ComponentModel;

namespace Business.PMS10.物管App.报事.Enum
{
    /// <summary>
    /// 员工在线状态
    /// </summary>
    public enum PMSUserOnlineStatus
    {
        /// <summary>
        /// 在线
        /// </summary>
        [Description("在线")]
        Online = 1 << 0,

        /// <summary>
        /// 离线
        /// </summary>
        [Description("离线")]
        Offline = 1 << 1,

        /// <summary>
        /// 不限
        /// </summary>
        [Description("不限")]
        All = 1 << 2
    }
}
