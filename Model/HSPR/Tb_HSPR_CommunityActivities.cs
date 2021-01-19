using System;
namespace MobileSoft.Model.HSPR
{
	/// <summary>
	/// 实体类Tb_HSPR_CommunityActivities 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_HSPR_CommunityActivities
	{
		public Tb_HSPR_CommunityActivities()
		{}
		#region Model
		private long _infoid;
		private int? _commid;
		private string _actheading;
		private string _actplace;
		private string _moderator;
		private string _actdate;
		private string _actpeople;
		private int? _isaudit;
		private string _actcontent;
		/// <summary>
		/// 
		/// </summary>
		public long InfoID
		{
			set{ _infoid=value;}
			get{return _infoid;}
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
		public string ActHeading
		{
			set{ _actheading=value;}
			get{return _actheading;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ActPlace
		{
			set{ _actplace=value;}
			get{return _actplace;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Moderator
		{
			set{ _moderator=value;}
			get{return _moderator;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ActDate
		{
			set{ _actdate=value;}
			get{return _actdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ActPeople
		{
			set{ _actpeople=value;}
			get{return _actpeople;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsAudit
		{
			set{ _isaudit=value;}
			get{return _isaudit;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ActContent
		{
			set{ _actcontent=value;}
			get{return _actcontent;}
		}
		#endregion Model

	}
}

