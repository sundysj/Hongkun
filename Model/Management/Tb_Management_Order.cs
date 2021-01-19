using System;
namespace MobileSoft.Model.Management
{
	/// <summary>
	/// 实体类Tb_Management_Order 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_Management_Order
	{
		public Tb_Management_Order()
		{}
		#region Model
		private string _ordercode;
		private string _commid;
		private string _servicecode;
		private string _ordernum;
		private long _custid;
		private long _roomid;
		private DateTime? _orderdate;
		private string _ordermethod;
		private string _contact;
		private string _contacttel;
		private DateTime? _startdate;
		private DateTime? _enddate;
		private int? _ordercount;
		private decimal? _orderamount;
		private decimal? _totalamount;
		private string _ispay;
		private string _memo;
		private string _isassign;
		private string _assignname;
		private DateTime? _assigndate;
		private string _isdeal;
		private string _dealname;
		private DateTime? _dealdate;
		private int? _deliverycount;
		private string _isvisit;
		private string _visitname;
		private DateTime? _visitdate;
		private string _evaluation;
		private string _custopinion;
		private string _isexit;
		private string _exitname;
		private DateTime? _exitdate;
		private string _exitreason;
		private string _dealmemo;
		private string _stanname;
		private string _stanformulaname;
		private decimal? _stanamount;
		private int? _isdelete;
		/// <summary>
		/// 
		/// </summary>
		public string OrderCode
		{
			set{ _ordercode=value;}
			get{return _ordercode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CommID
		{
			set{ _commid=value;}
			get{return _commid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ServiceCode
		{
			set{ _servicecode=value;}
			get{return _servicecode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OrderNum
		{
			set{ _ordernum=value;}
			get{return _ordernum;}
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
		public DateTime? OrderDate
		{
			set{ _orderdate=value;}
			get{return _orderdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OrderMethod
		{
			set{ _ordermethod=value;}
			get{return _ordermethod;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Contact
		{
			set{ _contact=value;}
			get{return _contact;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ContactTel
		{
			set{ _contacttel=value;}
			get{return _contacttel;}
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
		public int? OrderCount
		{
			set{ _ordercount=value;}
			get{return _ordercount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? OrderAmount
		{
			set{ _orderamount=value;}
			get{return _orderamount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? TotalAmount
		{
			set{ _totalamount=value;}
			get{return _totalamount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string IsPay
		{
			set{ _ispay=value;}
			get{return _ispay;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Memo
		{
			set{ _memo=value;}
			get{return _memo;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string IsAssign
		{
			set{ _isassign=value;}
			get{return _isassign;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AssignName
		{
			set{ _assignname=value;}
			get{return _assignname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? AssignDate
		{
			set{ _assigndate=value;}
			get{return _assigndate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string  IsDeal
		{
			set{ _isdeal=value;}
			get{return _isdeal;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DealName
		{
			set{ _dealname=value;}
			get{return _dealname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? DealDate
		{
			set{ _dealdate=value;}
			get{return _dealdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? DeliveryCount
		{
			set{ _deliverycount=value;}
			get{return _deliverycount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string IsVisit
		{
			set{ _isvisit=value;}
			get{return _isvisit;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string VisitName
		{
			set{ _visitname=value;}
			get{return _visitname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? VisitDate
		{
			set{ _visitdate=value;}
			get{return _visitdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Evaluation
		{
			set{ _evaluation=value;}
			get{return _evaluation;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CustOpinion
		{
			set{ _custopinion=value;}
			get{return _custopinion;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string IsExit
		{
			set{ _isexit=value;}
			get{return _isexit;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ExitName
		{
			set{ _exitname=value;}
			get{return _exitname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? ExitDate
		{
			set{ _exitdate=value;}
			get{return _exitdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ExitReason
		{
			set{ _exitreason=value;}
			get{return _exitreason;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DealMemo
		{
			set{ _dealmemo=value;}
			get{return _dealmemo;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string StanName
		{
			set{ _stanname=value;}
			get{return _stanname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string StanFormulaName
		{
			set{ _stanformulaname=value;}
			get{return _stanformulaname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? StanAmount
		{
			set{ _stanamount=value;}
			get{return _stanamount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsDelete
		{
			set{ _isdelete=value;}
			get{return _isdelete;}
		}
		#endregion Model

	}
}

