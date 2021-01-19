using System;
namespace MobileSoft.Model.HSPR
{
	/// <summary>
	/// 实体类Tb_HSPR_WaiversFee 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_HSPR_WaiversFee
	{
		public Tb_HSPR_WaiversFee()
		{}
		#region Model
		private long _waivid;
		private int? _commid;
		private long _custid;
		private long _roomid;
		private long _costid;
		private decimal? _waivamount;
		private decimal? _waivedamount;
		private DateTime? _waivstateduring;
		private DateTime? _waivendduring;
		private DateTime? _waivdate;
		private decimal? _waivmonthamount;
		private string _usercode;
		private string _waivreason;
		private string _auditreason;
		private string _waivmemo;
		private string _auditusercode;
		private int? _iswaiv;
		private int? _isaudit;
		private int? _waivtype;
		private decimal? _waivrates;
		private long _waivcostid;
		private long _handid;
		private string _metersign;
		private string _auditusername;
		private DateTime? _waivcredate;
		/// <summary>
		/// 
		/// </summary>
		public long WaivID
		{
			set{ _waivid=value;}
			get{return _waivid;}
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
		public long CostID
		{
			set{ _costid=value;}
			get{return _costid;}
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
		public decimal? WaivedAmount
		{
			set{ _waivedamount=value;}
			get{return _waivedamount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? WaivStateDuring
		{
			set{ _waivstateduring=value;}
			get{return _waivstateduring;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? WaivEndDuring
		{
			set{ _waivendduring=value;}
			get{return _waivendduring;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? WaivDate
		{
			set{ _waivdate=value;}
			get{return _waivdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? WaivMonthAmount
		{
			set{ _waivmonthamount=value;}
			get{return _waivmonthamount;}
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
		public string WaivReason
		{
			set{ _waivreason=value;}
			get{return _waivreason;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AuditReason
		{
			set{ _auditreason=value;}
			get{return _auditreason;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string WaivMemo
		{
			set{ _waivmemo=value;}
			get{return _waivmemo;}
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
		public int? IsWaiv
		{
			set{ _iswaiv=value;}
			get{return _iswaiv;}
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
		public int? WaivType
		{
			set{ _waivtype=value;}
			get{return _waivtype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? WaivRates
		{
			set{ _waivrates=value;}
			get{return _waivrates;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long WaivCostID
		{
			set{ _waivcostid=value;}
			get{return _waivcostid;}
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
		public string AuditUserName
		{
			set{ _auditusername=value;}
			get{return _auditusername;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? WaivCreDate
		{
			set{ _waivcredate=value;}
			get{return _waivcredate;}
		}
		#endregion Model

	}
}

