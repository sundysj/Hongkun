using System;
using Newtonsoft.Json;

namespace App.Model
{
    /// <summary>
    /// �û��Ż�ȯ������
    /// </summary>
    [Serializable]
    public class Tb_App_Coupon_Locked
    {
        [JsonConverter(typeof(GuidConverter))]
        public Guid IID
        {
            get;
            set;
        }

        /// <summary>
        /// �û�id
        /// </summary>
        [JsonConverter(typeof(GuidConverter))]
        public Guid UserID
        {
            get;
            set;
        }

        /// <summary>
        /// �Ż�ȯid
        /// </summary>
        [JsonConverter(typeof(GuidConverter))]
        public Guid CouponID
        {
            get;
            set;
        }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime LockedTime
        {
            get;
            set;
        }

    }
}