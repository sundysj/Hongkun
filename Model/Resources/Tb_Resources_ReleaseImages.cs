using System;
namespace MobileSoft.Model.Resources
{
	/// <summary>
	/// 实体类Tb_Resources_ReleaseImages 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_Resources_ReleaseImages
	{
		public Tb_Resources_ReleaseImages()
		{}
		#region Model
		private long _releaseimagesid;
		private long _releaseid;
		private string _releaseimagesurl;
		private string _releaseimagesdata;
		private DateTime? _releaseimagesupdate;
		private int? _releaseimagesindex;
		private DateTime? _isdelete;
		private long _bussid;
		/// <summary>
		/// 
		/// </summary>
		public long ReleaseImagesID
		{
			set{ _releaseimagesid=value;}
			get{return _releaseimagesid;}
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
		public string ReleaseImagesUrl
		{
			set{ _releaseimagesurl=value;}
			get{return _releaseimagesurl;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ReleaseImagesData
		{
			set{ _releaseimagesdata=value;}
			get{return _releaseimagesdata;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? ReleaseImagesUpdate
		{
			set{ _releaseimagesupdate=value;}
			get{return _releaseimagesupdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ReleaseImagesIndex
		{
			set{ _releaseimagesindex=value;}
			get{return _releaseimagesindex;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? IsDelete
		{
			set{ _isdelete=value;}
			get{return _isdelete;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long BussId
		{
			set{ _bussid=value;}
			get{return _bussid;}
		}
		#endregion Model

	}
}

