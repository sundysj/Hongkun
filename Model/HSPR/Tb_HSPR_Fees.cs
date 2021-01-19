using System;
namespace MobileSoft.Model.HSPR
{
	/// <summary>
	/// 实体类Tb_HSPR_Fees 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_HSPR_Fees
	{
		public Tb_HSPR_Fees()
		{}
		#region Model
		private long _feesid;
		private int? _commid;
		private long _costid;
		private long _custid;
		private long _roomid;
		private DateTime? _feesduedate;
		private DateTime? _feesstatedate;
		private DateTime? _feesenddate;
		private decimal? _dueamount;
		private decimal? _debtsamount;
		private decimal? _waivamount;
		private decimal? _precamount;
		private decimal? _paidamount;
		private decimal? _refundamount;
		private int? _isaudit;
		private string _feesmemo;
		private int? _accountflag;
		private int? _isbank;
		private int? _ischarge;
		private int? _isfreeze;
		private int? _isproperty;
		private long _corpstanid;
		private long _stanid;
		private long _ownerfeesid;
		private DateTime? _accountsduedate;
		private long _handid;
		private string _metersign;
		private decimal? _calcamount;
		private decimal? _calcamount2;
		private long _incidentid;
		private long _leasecontid;
		private long _contid;
		private string _stanmemo;
		private long _commisioncostid;
		private decimal? _commisionamount;
		private decimal? _waivcommisamount;
		private decimal? _perstanamount;
		private int? _isprec;
		private long _parkid;
		private long _carparkid;
		private long _meterid;
		private long _pmeterid;
		private Guid _feessynchcode;
		private int? _synchflag;
		private long _bedid;
		private int? _roomstate;
		private string _ordercode;
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
		public int? CommID
		{
			set{ _commid=value;}
			get{return _commid;}
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
		public DateTime? FeesDueDate
		{
			set{ _feesduedate=value;}
			get{return _feesduedate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? FeesStateDate
		{
			set{ _feesstatedate=value;}
			get{return _feesstatedate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? FeesEndDate
		{
			set{ _feesenddate=value;}
			get{return _feesenddate;}
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
		public decimal? DebtsAmount
		{
			set{ _debtsamount=value;}
			get{return _debtsamount;}
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
		public decimal? RefundAmount
		{
			set{ _refundamount=value;}
			get{return _refundamount;}
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
		public string FeesMemo
		{
			set{ _feesmemo=value;}
			get{return _feesmemo;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? AccountFlag
		{
			set{ _accountflag=value;}
			get{return _accountflag;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsBank
		{
			set{ _isbank=value;}
			get{return _isbank;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsCharge
		{
			set{ _ischarge=value;}
			get{return _ischarge;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsFreeze
		{
			set{ _isfreeze=value;}
			get{return _isfreeze;}
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
		public long CorpStanID
		{
			set{ _corpstanid=value;}
			get{return _corpstanid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long StanID
		{
			set{ _stanid=value;}
			get{return _stanid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long OwnerFeesID
		{
			set{ _ownerfeesid=value;}
			get{return _ownerfeesid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? AccountsDueDate
		{
			set{ _accountsduedate=value;}
			get{return _accountsduedate;}
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
		public string MeterSign
		{
			set{ _metersign=value;}
			get{return _metersign;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? CalcAmount
		{
			set{ _calcamount=value;}
			get{return _calcamount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? CalcAmount2
		{
			set{ _calcamount2=value;}
			get{return _calcamount2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long IncidentID
		{
			set{ _incidentid=value;}
			get{return _incidentid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long LeaseContID
		{
			set{ _leasecontid=value;}
			get{return _leasecontid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long ContID
		{
			set{ _contid=value;}
			get{return _contid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string StanMemo
		{
			set{ _stanmemo=value;}
			get{return _stanmemo;}
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
		public decimal? PerStanAmount
		{
			set{ _perstanamount=value;}
			get{return _perstanamount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsPrec
		{
			set{ _isprec=value;}
			get{return _isprec;}
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
		public long CarparkID
		{
			set{ _carparkid=value;}
			get{return _carparkid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long MeterID
		{
			set{ _meterid=value;}
			get{return _meterid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long PMeterID
		{
			set{ _pmeterid=value;}
			get{return _pmeterid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public Guid FeesSynchCode
		{
			set{ _feessynchcode=value;}
			get{return _feessynchcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? SynchFlag
		{
			set{ _synchflag=value;}
			get{return _synchflag;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long BedID
		{
			set{ _bedid=value;}
			get{return _bedid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? RoomState
		{
			set{ _roomstate=value;}
			get{return _roomstate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OrderCode
		{
			set{ _ordercode=value;}
			get{return _ordercode;}
		}
        public string UserCode { get; set; }

        #endregion Model

    }
}

