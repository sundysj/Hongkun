using System;
namespace MobileSoft.Model.SQMSys
{
	/// <summary>
	/// 实体类Tb_Sys_UserCalendar 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_Sys_UserCalendar
	{
		public Tb_Sys_UserCalendar()
		{}
		#region Model
		private Guid _usercalendarcode;
		private string _usercode;
		private string _title;
		private string _place;
		private DateTime? _starttime;
		private DateTime? _endtime;
		private string _scratchpad;
		private int _isremind;
		private decimal _remindhours;
		private int _remindstate;
		/// <summary>
		/// 
		/// </summary>
		public Guid UserCalendarCode
		{
			set{ _usercalendarcode=value;}
			get{return _usercalendarcode;}
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
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Place
		{
			set{ _place=value;}
			get{return _place;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? StartTime
		{
			set{ _starttime=value;}
			get{return _starttime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? EndTime
		{
			set{ _endtime=value;}
			get{return _endtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Scratchpad
		{
			set{ _scratchpad=value;}
			get{return _scratchpad;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsRemind
		{
			set{ _isremind=value;}
			get{return _isremind;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal RemindHours
		{
			set{ _remindhours=value;}
			get{return _remindhours;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int RemindState
		{
			set{ _remindstate=value;}
			get{return _remindstate;}
		}
		#endregion Model

	}
}

