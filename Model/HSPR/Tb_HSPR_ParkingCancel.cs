using System;
namespace MobileSoft.Model.HSPR
{
	/// <summary>
	/// 实体类Tb_HSPR_ParkingCancel 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_HSPR_ParkingCancel
	{
		public Tb_HSPR_ParkingCancel()
		{}
		#region Model
		private long _iid;
		private long _handid;
		private int? _commid;
		private long _parkid;
		private long _custid;
		private long _roomid;
		private string _cancelcause;
		private DateTime? _canceldate;
		private string _handling;
		private DateTime? _handdate;
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
		public long HandID
		{
			set{ _handid=value;}
			get{return _handid;}
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
		public long ParkID
		{
			set{ _parkid=value;}
			get{return _parkid;}
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
		public string CancelCause
		{
			set{ _cancelcause=value;}
			get{return _cancelcause;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CancelDate
		{
			set{ _canceldate=value;}
			get{return _canceldate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Handling
		{
			set{ _handling=value;}
			get{return _handling;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? HandDate
		{
			set{ _handdate=value;}
			get{return _handdate;}
		}
		#endregion Model

	}
}

