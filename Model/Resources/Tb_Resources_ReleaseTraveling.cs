using System;
namespace MobileSoft.Model.Resources
{
	/// <summary>
	/// 实体类Tb_Resources_ReleaseTraveling 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_Resources_ReleaseTraveling
	{
		public Tb_Resources_ReleaseTraveling()
		{}
		#region Model
		private long _releasetravelingid;
		private string _releasetravelingcontent;
		private string _releasetravelingservicedemo;
		private string _releasetravelingneedknow;
		private long _releaseid;
		/// <summary>
		/// 
		/// </summary>
		public long ReleaseTravelingID
		{
			set{ _releasetravelingid=value;}
			get{return _releasetravelingid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ReleaseTravelingContent
		{
			set{ _releasetravelingcontent=value;}
			get{return _releasetravelingcontent;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ReleaseTravelingServicedemo
		{
			set{ _releasetravelingservicedemo=value;}
			get{return _releasetravelingservicedemo;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ReleaseTravelingNeedKnow
		{
			set{ _releasetravelingneedknow=value;}
			get{return _releasetravelingneedknow;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long ReleaseID
		{
			set{ _releaseid=value;}
			get{return _releaseid;}
		}
		#endregion Model

	}
}

