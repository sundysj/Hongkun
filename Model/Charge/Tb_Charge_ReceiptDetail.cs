using System;
namespace MobileSoft.Model.Charge
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
		private long _resourcesid;
		private int? _quantity;
		private decimal? _salesprice;
		private decimal? _discountprice;
		private decimal? _memberdiscountprice;
		private decimal? _groupprice;
		private decimal? _detailamount;
		private string _rpdmemo;
		private int? _rpdisdelete;
		/// <summary>
		/// 
		/// </summary>
		public string RpdCode
		{
			set{ _rpdcode=value;}
			get{return _rpdcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ReceiptCode
		{
			set{ _receiptcode=value;}
			get{return _receiptcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long ResourcesID
		{
			set{ _resourcesid=value;}
			get{return _resourcesid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Quantity
		{
			set{ _quantity=value;}
			get{return _quantity;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? SalesPrice
		{
			set{ _salesprice=value;}
			get{return _salesprice;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? DiscountPrice
		{
			set{ _discountprice=value;}
			get{return _discountprice;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? MemberDiscountPrice
		{
			set{ _memberdiscountprice=value;}
			get{return _memberdiscountprice;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? GroupPrice
		{
			set{ _groupprice=value;}
			get{return _groupprice;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? DetailAmount
		{
			set{ _detailamount=value;}
			get{return _detailamount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RpdMemo
		{
			set{ _rpdmemo=value;}
			get{return _rpdmemo;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? RpdIsDelete
		{
			set{ _rpdisdelete=value;}
			get{return _rpdisdelete;}
		}
		#endregion Model

	}
}

