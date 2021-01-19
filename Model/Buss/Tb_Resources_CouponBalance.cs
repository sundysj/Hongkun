using System;

namespace Model.Buss
{
    /// <summary>
    /// 用户优惠券余额信息
    /// </summary>
    [Serializable]
    public class Tb_Resources_CouponBalance
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public string BussId { get; set; }

        public decimal Balance { get; set; }

        public int CorpId { get; set; }
    }
}
