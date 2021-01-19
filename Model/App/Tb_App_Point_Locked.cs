using System;
using Newtonsoft.Json;

namespace App.Model
{
    /// <summary>
    /// 用户积分锁定表
    /// </summary>
    [Serializable]
    public class Tb_App_Point_Locked
    {
        [JsonConverter(typeof(GuidConverter))]
        public Guid IID
        {
            get;
            set;
        }

        /// <summary>
        /// 用户id
        /// </summary>
        [JsonConverter(typeof(GuidConverter))]
        public Guid UserID
        {
            get;
            set;
        }

        /// <summary>
        /// 使用记录id
        /// </summary>
        [JsonConverter(typeof(GuidConverter))]
        public Guid UseHistoryID
        {
            get;
            set;
        }

        /// <summary>
        /// 已锁定积分数量
        /// </summary>
        public Int32 LockedPoints
        {
            get;
            set;
        }

        /// <summary>
        /// 锁定时间
        /// </summary>
        public DateTime LockedTime
        {
            get;
            set;
        }

    }
}