using System;
using Newtonsoft.Json;

namespace App.Model
{
    /// <summary>
    /// 用户积分表
    /// </summary>
    [Serializable]
    public class Tb_App_UserPoint
    {
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
        /// 积分余额
        /// </summary>
        public Int32 PointBalance
        {
            get;
            set;
        }

    }
}