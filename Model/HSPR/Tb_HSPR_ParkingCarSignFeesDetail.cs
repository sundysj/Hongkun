using System;
namespace MobileSoft.Model.HSPR
{
	/// <summary>
	/// 实体类Tb_HSPR_ParkingCarSignFeesDetail 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_HSPR_ParkingCarSignFeesDetail
	{
		public Tb_HSPR_ParkingCarSignFeesDetail()
		{}
		#region Model
		private long _iid;
		private int? _commid;
		private long _custid;
		private long _roomid;
		private long _costid;
		private long _receid;
		private long _recdid;
		private long _parkid;
		private long _handid;
		private int? _ishisfees;
		private DateTime? _feesstatedate;
		private DateTime? _feesenddate;
		private decimal? _chargeamount;
		/// <summary>
		/// 
		/// </summary>
		public long IID
		{
			set{ _iid=value;}
			get{return _iid;}
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
		public long ReceID
		{
			set{ _receid=value;}
			get{return _receid;}
		}
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
		public int? IsHisFees
		{
			set{ _ishisfees=value;}
			get{return _ishisfees;}
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
		#endregion Model

	}
}

