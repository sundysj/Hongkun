using System;
using Newtonsoft.Json;

namespace App.Model
{
    /// <summary>
    /// 用户积分使用记录表
    /// </summary>
    [Serializable]
    public class Tb_App_Point_UseHistory
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
        /// 积分使用方式
        /// </summary>
        public String UseWay
        {
            get;
            set;
        }

        /// <summary>
        /// 本次积分使用量
        /// </summary>
        public Int32 UsePoints
        {
            get;
            set;
        }

        /// <summary>
        /// 使用时间
        /// </summary>
        public DateTime UseTime
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

        /// <summary>
        /// 是否生效
        /// </summary>
        public Boolean? IsEffect
        {
            get;
            set;
        }

        /// <summary>
        /// 备注信息
        /// </summary>
        public String Remark
        {
            get;
            set;
        }
    }
}