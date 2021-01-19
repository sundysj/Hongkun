using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Order
{
    [Serializable]
    public class OrderInfo
    {
        public OrderInfo()
        { }
        #region Model
        private Int64 _ReleaseID;
        private string _ResourcesName;
        private float _ResourcesSalePrice;
        private float _ResourcesDisCountPrice;
        private string _ScheduleType;
        private string _ReleaseImagesUrl;
        private float _groupbuyprice;
        private int _isshopping;

        /// <summary>
        /// 是否加入购物车信息
        /// </summary>
        public int isShopping
        {
            set { _isshopping = value; }
            get { return _isshopping; }
        }
        /// <summary>
        /// 商品发布ID号
        /// </summary>
        public Int64 ReleaseID
        {
            set { _ReleaseID = value; }
            get { return _ReleaseID; }
        }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string ResourcesName
        {
            set { _ResourcesName = value; }
            get { return _ResourcesName; }
        }
        /// <summary>
        /// 团购单价
        /// </summary>
        public float GroupBuyPrice
        {
            set { _groupbuyprice = value; }
            get { return _groupbuyprice; }
        }

        /// <summary>
        /// 销售单价
        /// </summary>
        public float ResourcesSalePrice
        {
            set { _ResourcesSalePrice = value; }
            get { return _ResourcesSalePrice; }
        }
        /// <summary>
        /// 优惠单价
        /// </summary>
        public float ResourcesDisCountPrice
        {
            set { _ResourcesDisCountPrice = value; }
            get { return _ResourcesDisCountPrice; }
        }
        /// <summary>
        /// 预定类型
        /// </summary>
        public string ScheduleType
        {
            set { _ScheduleType = value; }
            get { return _ScheduleType; }
        }
        /// <summary>
        /// 图片路径
        /// </summary>
        public string ReleaseImagesUrl
        {
            set { _ReleaseImagesUrl = value; }
            get { return _ReleaseImagesUrl; }
        }
        #endregion Model
    }
}
