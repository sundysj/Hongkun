using System;
namespace MobileSoft.Model.Club
{
	/// <summary>
	/// 实体类Tb_Club_MaterialDetail 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_Club_MaterialDetail
	{
		public Tb_Club_MaterialDetail()
		{}
		#region Model
		private long _infoid;
		private long _merchid;
		private int? _commid;
		private decimal? _merchdefaultprice;
		private decimal? _merchcostprice;
		private string _merchmemo;
		private int? _ispackage;
		private long _merchandiseid;
		private int? _quantity;
		private long _costid;
		private int? _nooutstore;
		private int? _ishelpsale;
		private int? _getmethod;
		private decimal? _ishelpsalevalue;
		private int? _islease;
		private int? _ispay;
		private decimal? _paymoneyvalue;
		private int? _isdeletedetail;
		/// <summary>
		/// 
		/// </summary>
		public long InfoId
		{
			set{ _infoid=value;}
			get{return _infoid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long MerchID
		{
			set{ _merchid=value;}
			get{return _merchid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? CommID
		{
			set{ _commid=value;}
			get{return _commid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? MerchDefaultPrice
		{
			set{ _merchdefaultprice=value;}
			get{return _merchdefaultprice;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? MerchCostPrice
		{
			set{ _merchcostprice=value;}
			get{return _merchcostprice;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string MerchMemo
		{
			set{ _merchmemo=value;}
			get{return _merchmemo;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsPackage
		{
			set{ _ispackage=value;}
			get{return _ispackage;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long MerchandiseID
		{
			set{ _merchandiseid=value;}
			get{return _merchandiseid;}
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
		public long CostID
		{
			set{ _costid=value;}
			get{return _costid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? NoOutStore
		{
			set{ _nooutstore=value;}
			get{return _nooutstore;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsHelpSale
		{
			set{ _ishelpsale=value;}
			get{return _ishelpsale;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? GetMethod
		{
			set{ _getmethod=value;}
			get{return _getmethod;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? IsHelpSaleValue
		{
			set{ _ishelpsalevalue=value;}
			get{return _ishelpsalevalue;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsLease
		{
			set{ _islease=value;}
			get{return _islease;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsPay
		{
			set{ _ispay=value;}
			get{return _ispay;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? PayMoneyValue
		{
			set{ _paymoneyvalue=value;}
			get{return _paymoneyvalue;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsDeleteDetail
		{
			set{ _isdeletedetail=value;}
			get{return _isdeletedetail;}
		}
		#endregion Model

	}
}

