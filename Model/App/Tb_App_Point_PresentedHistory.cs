using System;
using Newtonsoft.Json;

namespace App.Model
{
    /// <summary>
    /// 用户积分赠送记录表
    /// </summary>
    [Serializable]
    public class Tb_App_Point_PresentedHistory
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
        /// 积分赠送方式
        /// </summary>
        public String PresentedWay
        {
            get;
            set;
        }

        /// <summary>
        /// 积分赠送时间
        /// </summary>
        public DateTime PresentedTime
        {
            get;
            set;
        }

        /// <summary>
        /// 本次赠送积分数量
        /// </summary>
        public Int32 PresentedPoints
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
        /// 备注信息
        /// </summary>
        public String Remark
        {
            get;
            set;
        }
    }
}