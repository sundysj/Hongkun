using System;
namespace MobileSoft.Model.Resources
{
	/// <summary>
	/// 实体类Tb_Resources_ReleaseCarrental 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_Resources_ReleaseCarrental
	{
		public Tb_Resources_ReleaseCarrental()
		{}
		#region Model
		private long _releasecarrentalid;
		private string _releasecarrentalbrand;
		private string _releasecarrentalmodels;
		private string _releasecarrentalmancount;
		private string _releasecarrentalcontent;
		private string _releasecarrentalneedknow;
		private long _releaseid;
		/// <summary>
		/// 
		/// </summary>
		public long ReleaseCarrentalID
		{
			set{ _releasecarrentalid=value;}
			get{return _releasecarrentalid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ReleaseCarrentalBrand
		{
			set{ _releasecarrentalbrand=value;}
			get{return _releasecarrentalbrand;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ReleaseCarrentalModels
		{
			set{ _releasecarrentalmodels=value;}
			get{return _releasecarrentalmodels;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ReleaseCarrentalManCount
		{
			set{ _releasecarrentalmancount=value;}
			get{return _releasecarrentalmancount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ReleaseCarrentalContent
		{
			set{ _releasecarrentalcontent=value;}
			get{return _releasecarrentalcontent;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ReleaseCarrentalNeedKnow
		{
			set{ _releasecarrentalneedknow=value;}
			get{return _releasecarrentalneedknow;}
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

