using System;
namespace MobileSoft.Model.HSPR
{
	/// <summary>
	/// 实体类Tb_HSPR_ParkingHis 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_HSPR_ParkingHis
	{
		public Tb_HSPR_ParkingHis()
		{}
		#region Model
		private long _iid;
		private int? _commid;
		private long _parkid;
		private long _oldcustid;
		private long _oldroomid;
		private long _custid;
		private long _roomid;
		private string _usercode;
		private DateTime? _changedate;
		private string _changememo;
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
		public long ParkID
		{
			set{ _parkid=value;}
			get{return _parkid;}
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
		public long OldRoomID
		{
			set{ _oldroomid=value;}
			get{return _oldroomid;}
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
		public string UserCode
		{
			set{ _usercode=value;}
			get{return _usercode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? ChangeDate
		{
			set{ _changedate=value;}
			get{return _changedate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ChangeMemo
		{
			set{ _changememo=value;}
			get{return _changememo;}
		}
		#endregion Model

	}
}

