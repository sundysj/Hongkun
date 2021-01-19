using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Model.Model.Buss
{
	/// <summary>
	/// 实体类Tb_Charge_ReceiptDetail 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_Charge_ReceiptDetail
	{
		public Tb_Charge_ReceiptDetail()
		{}
		#region Model
		private string _rpdcode;
		private string _receiptcode;
		private string _resourcesid;
		private int _quantity;
		private decimal? _salesprice;
		private decimal? _discountprice;
		private decimal? _groupprice;
		private decimal? _detailamount;
		private string _rpdmemo;
        private string _shoppingId;
        private int? _rpdisdelete;
		[DisplayName("商品订单票据明细ID")]
		public string RpdCode
		{
			set{ _rpdcode=value;}
			get{return _rpdcode;}
		}
		[DisplayName("票据编码")]
		public string ReceiptCode
		{
			set{ _receiptcode=value;}
			get{return _receiptcode;}
		}
		[DisplayName("资源ID")]
		public string ResourcesID
		{
			set{ _resourcesid=value;}
			get{return _resourcesid;}
		}
		[DisplayName("数量")]
		public int Quantity
		{
			set{ _quantity=value;}
			get{return _quantity;}
		}
		[DisplayName("销售单价")]
		public decimal? SalesPrice
		{
			set{ _salesprice=value;}
			get{return _salesprice;}
		}
		[DisplayName("优惠单价")]
		public decimal? DiscountPrice
		{
			set{ _discountprice=value;}
			get{return _discountprice;}
		}
		[DisplayName("团购单价")]
		public decimal? GroupPrice
		{
			set{ _groupprice=value;}
			get{return _groupprice;}
		}
		[DisplayName("合计单价")]
		public decimal? DetailAmount
		{
			set{ _detailamount=value;}
			get{return _detailamount;}
		}
		[DisplayName("备注")]
		public string RpdMemo
		{
			set{ _rpdmemo=value;}
			get{return _rpdmemo;}
		}
		[DisplayName("是否删除")]
		public int? RpdIsDelete
		{
			set{ _rpdisdelete=value;}
			get{return _rpdisdelete;}
		}
        [DisplayName("购物车商品明细ID")]
        public string ShoppingId
        {
            set { _shoppingId = value; }
            get { return _shoppingId; }
        }
        /// <summary>
        /// 商家专用券抵扣金额
        /// </summary>
        public decimal? OffsetMoney { get; set; }

        /// <summary>
        /// 物管通用券抵扣金额
        /// </summary>
        public decimal? OffsetMoney2 { get; set; }

        public int? IsEvaluated { get; set; }

        #endregion Model

    }
}

