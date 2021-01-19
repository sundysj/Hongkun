using System;
namespace MobileSoft.Model.HSPR
{
	/// <summary>
	/// 实体类Tb_HSPR_CommActivities 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_HSPR_CommActivities
	{
		public Tb_HSPR_CommActivities()
		{}
		#region Model
		private string _activitiesid;
		private long _commid;
		private string _activitiestype;
		private string _activitiestheme;
		private string _activitiescontent;
		private DateTime? _activitiesstartdate;
		private DateTime? _activitiesenddate;
		private string _activitiesplan;
		private string _custname;
		private long _custid;
		private string _roomsign;
		private long _roomid;
		private string _linkphone;
		private string _activitiesimages;
		private DateTime? _issuedate;
		private int? _isdelete;
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
		public long CommID
		{
			set{ _commid=value;}
			get{return _commid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ActivitiesType
		{
			set{ _activitiestype=value;}
			get{return _activitiestype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ActivitiesTheme
		{
			set{ _activitiestheme=value;}
			get{return _activitiestheme;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ActivitiesContent
		{
			set{ _activitiescontent=value;}
			get{return _activitiescontent;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? ActivitiesStartDate
		{
			set{ _activitiesstartdate=value;}
			get{return _activitiesstartdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? ActivitiesEndDate
		{
			set{ _activitiesenddate=value;}
			get{return _activitiesenddate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ActivitiesPlan
		{
			set{ _activitiesplan=value;}
			get{return _activitiesplan;}
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
		public string RoomSign
		{
			set{ _roomsign=value;}
			get{return _roomsign;}
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
		public string ActivitiesImages
		{
			set{ _activitiesimages=value;}
			get{return _activitiesimages;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? IssueDate
		{
			set{ _issuedate=value;}
			get{return _issuedate;}
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

