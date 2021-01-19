using System;
namespace MobileSoft.Model.HSPR
{
	/// <summary>
	/// 实体类Tb_HSPR_ParkingCarSignHand 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_HSPR_ParkingCarSignHand
	{
		public Tb_HSPR_ParkingCarSignHand()
		{}
		#region Model
		private long _carsignid;
		private int? _commid;
		private long _custid;
		private long _roomid;
		private long _receid;
		private long _parkid;
		private long _handid;
		private long _costid;
		private DateTime? _feesstatedate;
		private DateTime? _feesenddate;
		private decimal? _chargeamount;
		private string _serialnum;
		private string _barriersign;
		private int? _ishisfees;
		private int? _ishand;
		private string _acceptusercode;
		private string _handusercode;
		private DateTime? _handdate;
		private int? _isdelete;
		private DateTime? _submitdate;
		/// <summary>
		/// 
		/// </summary>
		public long CarSignID
		{
			set{ _carsignid=value;}
			get{return _carsignid;}
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
		public long ReceID
		{
			set{ _receid=value;}
			get{return _receid;}
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
		public long HandID
		{
			set{ _handid=value;}
			get{return _handid;}
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
		public decimal? ChargeAmount
		{
			set{ _chargeamount=value;}
			get{return _chargeamount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SerialNum
		{
			set{ _serialnum=value;}
			get{return _serialnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BarrierSign
		{
			set{ _barriersign=value;}
			get{return _barriersign;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsHisFees
		{
			set{ _ishisfees=value;}
			get{return _ishisfees;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsHand
		{
			set{ _ishand=value;}
			get{return _ishand;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AcceptUserCode
		{
			set{ _acceptusercode=value;}
			get{return _acceptusercode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string HandUserCode
		{
			set{ _handusercode=value;}
			get{return _handusercode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? HandDate
		{
			set{ _handdate=value;}
			get{return _handdate;}
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
		public DateTime? SubmitDate
		{
			set{ _submitdate=value;}
			get{return _submitdate;}
		}
		#endregion Model

	}
}

