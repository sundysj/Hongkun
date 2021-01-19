using System;
namespace MobileSoft.Model.Club
{
	/// <summary>
	/// 实体类Tb_Club_Merchandise 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_Club_Merchandise
	{
		public Tb_Club_Merchandise()
		{}
		#region Model
		private long _merchid;
		private string _organcode;
		private int? _commid;
		private string _merchcode;
		private string _merchname;
		private string _merchtypecode;
		private string _merchspell;
		private decimal? _merchdefaultprice;
		private decimal? _merchcostprice;
		private decimal? _merchstock;
		private long _merchalarmstock;
		private string _merchdescription;
		private string _merchmemo;
		private int? _salesprop;
		private int? _costingmethod;
		private string _merchunit;
		private int? _isallowpurchase;
		private int? _isdiscount;
		private decimal? _pointsredeem;
		private int? _ispointsredeem;
		private int? _isdelete;
		private int? _ispackage;
		private long _merchandiseid;
		private int? _quantity;
		private long _costid;
		private decimal? _merchcounts;
		private int? _merchstate;
		private int? _issale;
		private string _standard;
		private int? _ishelpsale;
		private int? _getmethod;
		private decimal? _ishelpsalevalue;
		private long _supplierid;
		private int? _outmethod;
		private int? _islease;
		private int? _ispurchase;
		private decimal? _purchaseprice;
		private string _usemerchtypecode;
		private int? _nooutstore;
		private int? _expirationdate;
		private string _pricechecksort;
		private string _pricelimitstartdate;
		private string _pricelimitenddate;
		private string _brand;
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
		public string OrganCode
		{
			set{ _organcode=value;}
			get{return _organcode;}
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
		public string MerchCode
		{
			set{ _merchcode=value;}
			get{return _merchcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string MerchName
		{
			set{ _merchname=value;}
			get{return _merchname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string MerchTypeCode
		{
			set{ _merchtypecode=value;}
			get{return _merchtypecode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string MerchSpell
		{
			set{ _merchspell=value;}
			get{return _merchspell;}
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
		public decimal? MerchStock
		{
			set{ _merchstock=value;}
			get{return _merchstock;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long MerchAlarmStock
		{
			set{ _merchalarmstock=value;}
			get{return _merchalarmstock;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string MerchDescription
		{
			set{ _merchdescription=value;}
			get{return _merchdescription;}
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
		public int? SalesProp
		{
			set{ _salesprop=value;}
			get{return _salesprop;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? CostingMethod
		{
			set{ _costingmethod=value;}
			get{return _costingmethod;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string MerchUnit
		{
			set{ _merchunit=value;}
			get{return _merchunit;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsAllowPurchase
		{
			set{ _isallowpurchase=value;}
			get{return _isallowpurchase;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsDiscount
		{
			set{ _isdiscount=value;}
			get{return _isdiscount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? PointsRedeem
		{
			set{ _pointsredeem=value;}
			get{return _pointsredeem;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsPointsRedeem
		{
			set{ _ispointsredeem=value;}
			get{return _ispointsredeem;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsDelete
		{
			set{ _isdelete=value;}
			get{return _isdelete;}
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
		public decimal? MerchCounts
		{
			set{ _merchcounts=value;}
			get{return _merchcounts;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? MerchState
		{
			set{ _merchstate=value;}
			get{return _merchstate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsSale
		{
			set{ _issale=value;}
			get{return _issale;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Standard
		{
			set{ _standard=value;}
			get{return _standard;}
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
		public long SupplierID
		{
			set{ _supplierid=value;}
			get{return _supplierid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? OutMethod
		{
			set{ _outmethod=value;}
			get{return _outmethod;}
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
		public int? IsPurchase
		{
			set{ _ispurchase=value;}
			get{return _ispurchase;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? PurchasePrice
		{
			set{ _purchaseprice=value;}
			get{return _purchaseprice;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UseMerchTypeCode
		{
			set{ _usemerchtypecode=value;}
			get{return _usemerchtypecode;}
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
		public int? ExpirationDate
		{
			set{ _expirationdate=value;}
			get{return _expirationdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PriceCheckSort
		{
			set{ _pricechecksort=value;}
			get{return _pricechecksort;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PriceLimitStartDate
		{
			set{ _pricelimitstartdate=value;}
			get{return _pricelimitstartdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PriceLimitEndDate
		{
			set{ _pricelimitenddate=value;}
			get{return _pricelimitenddate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Brand
		{
			set{ _brand=value;}
			get{return _brand;}
		}
		#endregion Model

	}
}

