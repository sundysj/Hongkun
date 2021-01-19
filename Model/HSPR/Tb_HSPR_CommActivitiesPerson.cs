using System;
namespace MobileSoft.Model.HSPR
{
	/// <summary>
	/// 实体类Tb_HSPR_CommActivitiesPerson 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_HSPR_CommActivitiesPerson
	{
		public Tb_HSPR_CommActivitiesPerson()
		{}
		#region Model
		private string _activitiespersonid;
		private string _activitiesid;
		private string _custname;
		private long _custid;
		private string _roomname;
		private long _roomid;
		private string _linkphone;
		private int? _personcount;
		private long _commid;
		private int? _isdelete;
		/// <summary>
		/// 
		/// </summary>
		public string ActivitiesPersonID
		{
			set{ _activitiespersonid=value;}
			get{return _activitiespersonid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ActivitiesID
		{
			set{ _activitiesid=value;}
			get{return _activitiesid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CustName
		{
			set{ _custname=value;}
			get{return _custname;}
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
		public string RoomName
		{
			set{ _roomname=value;}
			get{return _roomname;}
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
		public string LinkPhone
		{
			set{ _linkphone=value;}
			get{return _linkphone;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? PersonCount
		{
			set{ _personcount=value;}
			get{return _personcount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long CommID
		{
			set{ _commid=value;}
			get{return _commid;}
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

