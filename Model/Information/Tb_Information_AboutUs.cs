using System;
namespace MobileSoft.Model.Information
{
	/// <summary>
	/// 实体类Tb_Information_AboutUs 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_Information_AboutUs
	{
		public Tb_Information_AboutUs()
		{}
		#region Model
		private long _aboutid;
		private long _bussid;
		private string _title;
		private string _aboutpublisher;
		private DateTime? _pubulishdate;
		private string _aboutcontent;
		private string _aboutimage;
		private int? _isdelete;
		/// <summary>
		/// 
		/// </summary>
		public long AboutId
		{
			set{ _aboutid=value;}
			get{return _aboutid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long BussId
		{
			set{ _bussid=value;}
			get{return _bussid;}
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
		public string AboutPublisher
		{
			set{ _aboutpublisher=value;}
			get{return _aboutpublisher;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? PubulishDate
		{
			set{ _pubulishdate=value;}
			get{return _pubulishdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AboutContent
		{
			set{ _aboutcontent=value;}
			get{return _aboutcontent;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AboutImage
		{
			set{ _aboutimage=value;}
			get{return _aboutimage;}
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

