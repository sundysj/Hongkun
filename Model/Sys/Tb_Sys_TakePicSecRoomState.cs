using System;
namespace MobileSoft.Model.Sys
{
	/// <summary>
	/// 实体类Tb_Sys_TakePicSecRoomState 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_Sys_TakePicSecRoomState
	{
		public Tb_Sys_TakePicSecRoomState()
		{}
		#region Model
		private long _statid;
		private int? _stattype;
		private int? _commid;
		private string _organcode;
		private DateTime? _statdate;
		private int? _roomstate;
		private int? _counts;
		/// <summary>
		/// 
		/// </summary>
		public long StatID
		{
			set{ _statid=value;}
			get{return _statid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? StatType
		{
			set{ _stattype=value;}
			get{return _stattype;}
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
		public string OrganCode
		{
			set{ _organcode=value;}
			get{return _organcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? StatDate
		{
			set{ _statdate=value;}
			get{return _statdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? RoomState
		{
			set{ _roomstate=value;}
			get{return _roomstate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Counts
		{
			set{ _counts=value;}
			get{return _counts;}
		}
		#endregion Model

	}
}

