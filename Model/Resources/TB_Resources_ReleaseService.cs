using System;
namespace MobileSoft.Model.Resources
{
	/// <summary>
	/// 实体类TB_Resources_ReleaseService 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class TB_Resources_ReleaseService
	{
		public TB_Resources_ReleaseService()
		{}
		#region Model
		private long _releaseserviceid;
		private long _releaseid;
		private string _releaseservicecontent;
		private string _releaseserviceneedknow;
		/// <summary>
		/// 
		/// </summary>
		public long ReleaseServiceID
		{
			set{ _releaseserviceid=value;}
			get{return _releaseserviceid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long ReleaseID
		{
			set{ _releaseid=value;}
			get{return _releaseid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ReleaseServiceContent
		{
			set{ _releaseservicecontent=value;}
			get{return _releaseservicecontent;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ReleaseServiceNeedKnow
		{
			set{ _releaseserviceneedknow=value;}
			get{return _releaseserviceneedknow;}
		}
		#endregion Model

	}
}

