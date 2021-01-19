using System;
namespace MobileSoft.Model.Common
{
	/// <summary>
	/// 实体类Tb_Common_CommunityService 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_Common_CommunityService
	{
		public Tb_Common_CommunityService()
		{}
		#region Model
		private long _infoid;
		private string _organcode;
		private string _depcode;
		private string _title;
		private string _content;
		private string _usercode;
		private DateTime? _issuedate;
		private string _infotypename;
		/// <summary>
		/// 
		/// </summary>
		public long InfoId
		{
			set{ _infoid=value;}
			get{return _infoid;}
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
		public string DepCode
		{
			set{ _depcode=value;}
			get{return _depcode;}
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
		public string Content
		{
			set{ _content=value;}
			get{return _content;}
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
		public DateTime? IssueDate
		{
			set{ _issuedate=value;}
			get{return _issuedate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string InfoTypeName
		{
			set{ _infotypename=value;}
			get{return _infotypename;}
		}
		#endregion Model

	}
}

