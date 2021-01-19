using System;
namespace MobileSoft.Model.HSPR
{
	/// <summary>
	/// 实体类Tb_HSPR_KeyDoorDeviceSetting 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_HSPR_KeyDoorDeviceSetting
	{
		public Tb_HSPR_KeyDoorDeviceSetting()
		{}
		#region Model
		private long _doorid;
		private int? _commid;
		private int? _doortype;
		private string _doornum;
		private string _doorname;
		private string _deviceaddress;
		private int? _buildsnum;
		private int? _unitsnum;
		private string _memo;
		private int? _isdelete;
		/// <summary>
		/// 
		/// </summary>
		public long DoorID
		{
			set{ _doorid=value;}
			get{return _doorid;}
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
		public int? DoorType
		{
			set{ _doortype=value;}
			get{return _doortype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DoorNum
		{
			set{ _doornum=value;}
			get{return _doornum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DoorName
		{
			set{ _doorname=value;}
			get{return _doorname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DeviceAddRess
		{
			set{ _deviceaddress=value;}
			get{return _deviceaddress;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? BuildSNum
		{
			set{ _buildsnum=value;}
			get{return _buildsnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? UnitSNum
		{
			set{ _unitsnum=value;}
			get{return _unitsnum;}
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
		public int? IsDelete
		{
			set{ _isdelete=value;}
			get{return _isdelete;}
		}
		#endregion Model

	}
}

