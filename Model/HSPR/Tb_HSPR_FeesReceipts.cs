using System;
namespace MobileSoft.Model.HSPR
{
	/// <summary>
	/// 实体类Tb_HSPR_FeesReceipts 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_HSPR_FeesReceipts
	{
		public Tb_HSPR_FeesReceipts()
		{}
		#region Model
		private long _receid;
		private int? _commid;
		private long _custid;
		private long _roomid;
		private string _billssign;
		private int? _printtimes;
		private DateTime? _billsdate;
		private string _usercode;
		private string _chargemode;
		private int? _accountway;
		private string _recememo;
		private decimal? _persurplus;
		private decimal? _surplusamount;
		private decimal? _precamount;
		private decimal? _paidamount;
		private int? _isdelete;
		private long _userepid;
		private string _invoicebill;
		private string _invoiceunit;
		private string _remitterunit;
		private string _bankname;
		private string _bankaccount;
		private string _chequebill;
		private int? _rendertype;
		private long _rendercustid;
		private string _rendercustname;
		private int? _isrefer;
		private string _referreason;
		private string _referusercode;
		private DateTime? _referdate;
		private string _auditusercode;
		private DateTime? _auditdate;
		private int? _isaudit;
		private long _billtypeid;
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
		public int? CommID
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
		public string BillsSign
		{
			set{ _billssign=value;}
			get{return _billssign;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? PrintTimes
		{
			set{ _printtimes=value;}
			get{return _printtimes;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? BillsDate
		{
			set{ _billsdate=value;}
			get{return _billsdate;}
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
		public string ReceMemo
		{
			set{ _recememo=value;}
			get{return _recememo;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? PerSurplus
		{
			set{ _persurplus=value;}
			get{return _persurplus;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? SurplusAmount
		{
			set{ _surplusamount=value;}
			get{return _surplusamount;}
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
		public decimal? PaidAmount
		{
			set{ _paidamount=value;}
			get{return _paidamount;}
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
		public long UseRepID
		{
			set{ _userepid=value;}
			get{return _userepid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string InvoiceBill
		{
			set{ _invoicebill=value;}
			get{return _invoicebill;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string InvoiceUnit
		{
			set{ _invoiceunit=value;}
			get{return _invoiceunit;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RemitterUnit
		{
			set{ _remitterunit=value;}
			get{return _remitterunit;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BankName
		{
			set{ _bankname=value;}
			get{return _bankname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BankAccount
		{
			set{ _bankaccount=value;}
			get{return _bankaccount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ChequeBill
		{
			set{ _chequebill=value;}
			get{return _chequebill;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? RenderType
		{
			set{ _rendertype=value;}
			get{return _rendertype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long RenderCustID
		{
			set{ _rendercustid=value;}
			get{return _rendercustid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RenderCustName
		{
			set{ _rendercustname=value;}
			get{return _rendercustname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsRefer
		{
			set{ _isrefer=value;}
			get{return _isrefer;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ReferReason
		{
			set{ _referreason=value;}
			get{return _referreason;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ReferUserCode
		{
			set{ _referusercode=value;}
			get{return _referusercode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? ReferDate
		{
			set{ _referdate=value;}
			get{return _referdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AuditUserCode
		{
			set{ _auditusercode=value;}
			get{return _auditusercode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? AuditDate
		{
			set{ _auditdate=value;}
			get{return _auditdate;}
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
		public long BillTypeID
		{
			set{ _billtypeid=value;}
			get{return _billtypeid;}
		}
		#endregion Model

	}
}

