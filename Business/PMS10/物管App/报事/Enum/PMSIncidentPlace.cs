using System;

namespace Business.PMS10.物管App.报事.Enum
{
    /// <summary>
    /// 报事位置
    /// </summary>
    [Flags]
    public enum PMSIncidentPlace
    {
        /// <summary>
        /// 户内
        /// </summary>
        Indoor = 1 << 0,

        /// <summary>
        /// 公区
        /// </summary>
        Public = 1 << 1
    }
}
