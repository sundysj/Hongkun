using System;
namespace MobileSoft.Model.HSPR
{
	/// <summary>
	/// 实体类Tb_HSPR_Parking 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_HSPR_Parking
	{
		public Tb_HSPR_Parking()
		{}
		#region Model
		private long _parkid;
		private int? _commid;
		private long _custid;
		private long _roomid;
		private string _parktype;
		private decimal? _parkarea;
		private int? _carparkid;
		private string _parkname;
		private int? _parkingnum;
		private string _propertyright;
		private long _stanid;
		private string _propertyuses;
		private string _usestate;
		private int? _isdelete;
		private int? _parkcategory;
		private int? _residetype;
		private Guid _parksynchcode;
		private int? _synchflag;
		private string _parkmemo;
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
		public string ParkType
		{
			set{ _parktype=value;}
			get{return _parktype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? ParkArea
		{
			set{ _parkarea=value;}
			get{return _parkarea;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? CarparkID
		{
			set{ _carparkid=value;}
			get{return _carparkid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ParkName
		{
			set{ _parkname=value;}
			get{return _parkname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ParkingNum
		{
			set{ _parkingnum=value;}
			get{return _parkingnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PropertyRight
		{
			set{ _propertyright=value;}
			get{return _propertyright;}
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
		public string PropertyUses
		{
			set{ _propertyuses=value;}
			get{return _propertyuses;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UseState
		{
			set{ _usestate=value;}
			get{return _usestate;}
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
		public int? ParkCategory
		{
			set{ _parkcategory=value;}
			get{return _parkcategory;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ResideType
		{
			set{ _residetype=value;}
			get{return _residetype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public Guid ParkSynchCode
		{
			set{ _parksynchcode=value;}
			get{return _parksynchcode;}
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
		public string ParkMemo
		{
			set{ _parkmemo=value;}
			get{return _parkmemo;}
		}
		#endregion Model

	}
}

