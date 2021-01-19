using System;
namespace MobileSoft.Model.Unify
{
	/// <summary>
	/// 实体类Tb_Unify_CustomerLive 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_Unify_CustomerLive
	{
		public Tb_Unify_CustomerLive()
		{}
		#region Model
		private Guid _livesynchcode;
		private Guid _commsynchcode;
		private Guid _custsynchcode;
		private Guid _roomsynchcode;
		private long _unliveid;
		private int? _livetype;
		private string _l_broadbandnum;
		private string _l_fixedtel;
		private string _l_mobilephone;
		private string _l_iptvaccount;
		private DateTime? _submittime;
		private DateTime? _fittingtime;
		private DateTime? _staytime;
		private string _staytype;
		private DateTime? _chargingtime;
		private string _chargecause;
		private DateTime? _chargetime;
		private string _contractsign;
		private string _rightssign;
		private string _propertyuses;
		private DateTime? _startdate;
		private DateTime? _enddate;
		private DateTime? _reletdate;
		private int? _isactive;
		private int? _isdellive;
		private int? _isdebts;
		private int? _issale;
		private decimal? _purchasearea;
		private decimal? _propertyarea;
		private int? _livestate;
		private string _bankname;
		private string _bankids;
		private string _bankaccount;
		private string _bankagreement;
		private string _bankcode;
		private string _bankno;
		private long _oldcustid;
		private int? _synchflag;
		private long _ordsnum;
		/// <summary>
		/// 
		/// </summary>
		public Guid LiveSynchCode
		{
			set{ _livesynchcode=value;}
			get{return _livesynchcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public Guid CommSynchCode
		{
			set{ _commsynchcode=value;}
			get{return _commsynchcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public Guid CustSynchCode
		{
			set{ _custsynchcode=value;}
			get{return _custsynchcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public Guid RoomSynchCode
		{
			set{ _roomsynchcode=value;}
			get{return _roomsynchcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long UnLiveID
		{
			set{ _unliveid=value;}
			get{return _unliveid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? LiveType
		{
			set{ _livetype=value;}
			get{return _livetype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string L_BroadbandNum
		{
			set{ _l_broadbandnum=value;}
			get{return _l_broadbandnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string L_FixedTel
		{
			set{ _l_fixedtel=value;}
			get{return _l_fixedtel;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string L_MobilePhone
		{
			set{ _l_mobilephone=value;}
			get{return _l_mobilephone;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string L_IPTVAccount
		{
			set{ _l_iptvaccount=value;}
			get{return _l_iptvaccount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? SubmitTime
		{
			set{ _submittime=value;}
			get{return _submittime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? FittingTime
		{
			set{ _fittingtime=value;}
			get{return _fittingtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? StayTime
		{
			set{ _staytime=value;}
			get{return _staytime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string StayType
		{
			set{ _staytype=value;}
			get{return _staytype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? ChargingTime
		{
			set{ _chargingtime=value;}
			get{return _chargingtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ChargeCause
		{
			set{ _chargecause=value;}
			get{return _chargecause;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? ChargeTime
		{
			set{ _chargetime=value;}
			get{return _chargetime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ContractSign
		{
			set{ _contractsign=value;}
			get{return _contractsign;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RightsSign
		{
			set{ _rightssign=value;}
			get{return _rightssign;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PropertyUses
		{
			set{ _propertyuses=value;}
			get{return _propertyuses;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? StartDate
		{
			set{ _startdate=value;}
			get{return _startdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? EndDate
		{
			set{ _enddate=value;}
			get{return _enddate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? ReletDate
		{
			set{ _reletdate=value;}
			get{return _reletdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsActive
		{
			set{ _isactive=value;}
			get{return _isactive;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsDelLive
		{
			set{ _isdellive=value;}
			get{return _isdellive;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsDebts
		{
			set{ _isdebts=value;}
			get{return _isdebts;}
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
		public decimal? PurchaseArea
		{
			set{ _purchasearea=value;}
			get{return _purchasearea;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? PropertyArea
		{
			set{ _propertyarea=value;}
			get{return _propertyarea;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? LiveState
		{
			set{ _livestate=value;}
			get{return _livestate;}
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
		public string BankIDs
		{
			set{ _bankids=value;}
			get{return _bankids;}
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
		public string BankAgreement
		{
			set{ _bankagreement=value;}
			get{return _bankagreement;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BankCode
		{
			set{ _bankcode=value;}
			get{return _bankcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BankNo
		{
			set{ _bankno=value;}
			get{return _bankno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long OldCustID
		{
			set{ _oldcustid=value;}
			get{return _oldcustid;}
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
		public long OrdSNum
		{
			set{ _ordsnum=value;}
			get{return _ordsnum;}
		}
		#endregion Model

	}
}

