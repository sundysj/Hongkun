using System;
namespace MobileSoft.Model.HSPR
{
	/// <summary>
	/// 实体类Tb_HSPR_PreCostsDetail 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_HSPR_PreCostsDetail
	{
		public Tb_HSPR_PreCostsDetail()
		{}
		#region Model
		private long _recdid;
		private long _precid;
		private int _commid;
		private long _custid;
		private long _roomid;
		private long _costid;
		private decimal? _precamount;
		private DateTime? _precdate;
		private string _precmemo;
		private string _usercode;
		private string _billssign;
		private string _chargemode;
		private int? _accountway;
		private int? _isaudit;
		private int? _isdelete;
		private long _receid;
		private decimal? _oldprecamount;
		private decimal? _newprecamount;
		private long _feesid;
		private int? _sourcetype;
		private DateTime? _feesduedate;
		private int? _isproperty;
		private long _commisioncostid;
		private decimal? _dueamount;
		private decimal? _waivamount;
		private decimal? _commisionamount;
		private decimal? _waivcommisamount;
		private long _handid;
		private long _carparkid;
		private long _parkid;
		private string _costids;
		private string _costnames;
		private string _handids;
		private string _parknames;
		/// <summary>
		/// 
		/// </summary>
		public long RecdID
		{
			set{ _recdid=value;}
			get{return _recdid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long PrecID
		{
			set{ _precid=value;}
			get{return _precid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int CommID
		{
			set{ _commid=value;}
			get{return _commid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long CustID
		{
			set{ _custid=value;}
			get{return _custid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long RoomID
		{
			set{ _roomid=value;}
			get{return _roomid;}
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
		public decimal? PrecAmount
		{
			set{ _precamount=value;}
			get{return _precamount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? PrecDate
		{
			set{ _precdate=value;}
			get{return _precdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PrecMemo
		{
			set{ _precmemo=value;}
			get{return _precmemo;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UserCode
		{
			set{ _usercode=value;}
			get{return _usercode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BillsSign
		{
			set{ _billssign=value;}
			get{return _billssign;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ChargeMode
		{
			set{ _chargemode=value;}
			get{return _chargemode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? AccountWay
		{
			set{ _accountway=value;}
			get{return _accountway;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsAudit
		{
			set{ _isaudit=value;}
			get{return _isaudit;}
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
		public long ReceID
		{
			set{ _receid=value;}
			get{return _receid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? OldPrecAmount
		{
			set{ _oldprecamount=value;}
			get{return _oldprecamount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? NewPrecAmount
		{
			set{ _newprecamount=value;}
			get{return _newprecamount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long FeesID
		{
			set{ _feesid=value;}
			get{return _feesid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? SourceType
		{
			set{ _sourcetype=value;}
			get{return _sourcetype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? FeesDueDate
		{
			set{ _feesduedate=value;}
			get{return _feesduedate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsProperty
		{
			set{ _isproperty=value;}
			get{return _isproperty;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long CommisionCostID
		{
			set{ _commisioncostid=value;}
			get{return _commisioncostid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? DueAmount
		{
			set{ _dueamount=value;}
			get{return _dueamount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? WaivAmount
		{
			set{ _waivamount=value;}
			get{return _waivamount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? CommisionAmount
		{
			set{ _commisionamount=value;}
			get{return _commisionamount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? WaivCommisAmount
		{
			set{ _waivcommisamount=value;}
			get{return _waivcommisamount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long HandID
		{
			set{ _handid=value;}
			get{return _handid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long CarParkID
		{
			set{ _carparkid=value;}
			get{return _carparkid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long ParkID
		{
			set{ _parkid=value;}
			get{return _parkid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CostIDs
		{
			set{ _costids=value;}
			get{return _costids;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CostNames
		{
			set{ _costnames=value;}
			get{return _costnames;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string HandIDs
		{
			set{ _handids=value;}
			get{return _handids;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ParkNames
		{
			set{ _parknames=value;}
			get{return _parknames;}
		}
		#endregion Model

	}
}

