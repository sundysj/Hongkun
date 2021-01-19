using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace MobileSoft.Model.DYDZ
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
		private string _businessid;
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
		private int? _ispropertyservice;
		private DateTime? _contsubdate;
		private DateTime? _actualsubdate;
		private string _time;
		[DisplayName("")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "必填")]
		public string BusinessID
		{
			set{ _businessid=value;}
			get{return _businessid;}
		}
		[DisplayName("")]
		public long ParkID
		{
			set{ _parkid=value;}
			get{return _parkid;}
		}
		[DisplayName("")]
		public int? CommID
		{
			set{ _commid=value;}
			get{return _commid;}
		}
		[DisplayName("")]
		public long CustID
		{
			set{ _custid=value;}
			get{return _custid;}
		}
		[DisplayName("")]
		public long RoomID
		{
			set{ _roomid=value;}
			get{return _roomid;}
		}
		[DisplayName("")]
		public string ParkType
		{
			set{ _parktype=value;}
			get{return _parktype;}
		}
		[DisplayName("")]
		public decimal? ParkArea
		{
			set{ _parkarea=value;}
			get{return _parkarea;}
		}
		[DisplayName("")]
		public int? CarparkID
		{
			set{ _carparkid=value;}
			get{return _carparkid;}
		}
		[DisplayName("")]
		public string ParkName
		{
			set{ _parkname=value;}
			get{return _parkname;}
		}
		[DisplayName("")]
		public int? ParkingNum
		{
			set{ _parkingnum=value;}
			get{return _parkingnum;}
		}
		[DisplayName("")]
		public string PropertyRight
		{
			set{ _propertyright=value;}
			get{return _propertyright;}
		}
		[DisplayName("")]
		public long StanID
		{
			set{ _stanid=value;}
			get{return _stanid;}
		}
		[DisplayName("")]
		public string PropertyUses
		{
			set{ _propertyuses=value;}
			get{return _propertyuses;}
		}
		[DisplayName("")]
		public string UseState
		{
			set{ _usestate=value;}
			get{return _usestate;}
		}
		[DisplayName("")]
		public int? IsDelete
		{
			set{ _isdelete=value;}
			get{return _isdelete;}
		}
		[DisplayName("")]
		public int? ParkCategory
		{
			set{ _parkcategory=value;}
			get{return _parkcategory;}
		}
		[DisplayName("")]
		public int? ResideType
		{
			set{ _residetype=value;}
			get{return _residetype;}
		}
		[DisplayName("")]
		public Guid ParkSynchCode
		{
			set{ _parksynchcode=value;}
			get{return _parksynchcode;}
		}
		[DisplayName("")]
		public int? SynchFlag
		{
			set{ _synchflag=value;}
			get{return _synchflag;}
		}
		[DisplayName("")]
		public string ParkMemo
		{
			set{ _parkmemo=value;}
			get{return _parkmemo;}
		}
		[DisplayName("")]
		public int? IsPropertyService
		{
			set{ _ispropertyservice=value;}
			get{return _ispropertyservice;}
		}
		[DisplayName("")]
		public DateTime? ContSubDate
		{
			set{ _contsubdate=value;}
			get{return _contsubdate;}
		}
		[DisplayName("")]
		public DateTime? ActualSubDate
		{
			set{ _actualsubdate=value;}
			get{return _actualsubdate;}
		}
		[DisplayName("")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "必填")]
		public string time
		{
			set{ _time=value;}
			get{return _time;}
		}
		#endregion Model

	}
}

