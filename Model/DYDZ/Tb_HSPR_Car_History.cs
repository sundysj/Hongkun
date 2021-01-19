using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace MobileSoft.Model.DYDZ
{
	/// <summary>
	/// 实体类Tb_HSPR_Car_History 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_HSPR_Car_History
	{
		public Tb_HSPR_Car_History()
		{}
		#region Model
		private string _carid;
		private string _commid;
		private string _carnum;
		private string _brand;
		private string _owner;
		private string _category;
		private string _color;
		private string _displacement;
		private string _custid;
		private string _roomid;
		private string _remark;
		private string _insertuserid;
		private DateTime? _insertdatetime;
		private bool _isdelete;
		private string _deletereason;
		private string _deleteuserid;
		private DateTime? _deletedatetime;
		private string _time;
		[DisplayName("")]
		public string CarID
		{
			set{ _carid=value;}
			get{return _carid;}
		}
		[DisplayName("")]
		public string CommID
		{
			set{ _commid=value;}
			get{return _commid;}
		}
		[DisplayName("")]
		public string CarNum
		{
			set{ _carnum=value;}
			get{return _carnum;}
		}
		[DisplayName("")]
		public string Brand
		{
			set{ _brand=value;}
			get{return _brand;}
		}
		[DisplayName("")]
		public string Owner
		{
			set{ _owner=value;}
			get{return _owner;}
		}
		[DisplayName("")]
		public string Category
		{
			set{ _category=value;}
			get{return _category;}
		}
		[DisplayName("")]
		public string Color
		{
			set{ _color=value;}
			get{return _color;}
		}
		[DisplayName("")]
		public string Displacement
		{
			set{ _displacement=value;}
			get{return _displacement;}
		}
		[DisplayName("")]
		public string CustID
		{
			set{ _custid=value;}
			get{return _custid;}
		}
		[DisplayName("")]
		public string RoomID
		{
			set{ _roomid=value;}
			get{return _roomid;}
		}
		[DisplayName("")]
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		[DisplayName("")]
		public string InsertUserID
		{
			set{ _insertuserid=value;}
			get{return _insertuserid;}
		}
		[DisplayName("")]
		public DateTime? InsertDateTime
		{
			set{ _insertdatetime=value;}
			get{return _insertdatetime;}
		}
		[DisplayName("")]
		public bool IsDelete
		{
			set{ _isdelete=value;}
			get{return _isdelete;}
		}
		[DisplayName("")]
		public string DeleteReason
		{
			set{ _deletereason=value;}
			get{return _deletereason;}
		}
		[DisplayName("")]
		public string DeleteUserID
		{
			set{ _deleteuserid=value;}
			get{return _deleteuserid;}
		}
		[DisplayName("")]
		public DateTime? DeleteDateTime
		{
			set{ _deletedatetime=value;}
			get{return _deletedatetime;}
		}
		[DisplayName("")]
		public string time
		{
			set{ _time=value;}
			get{return _time;}
		}
		#endregion Model

	}
}

