using System;
namespace MobileSoft.Model.Information
{
	/// <summary>
	/// 实体类Tb_Information_ConsumerGuid 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_Information_ConsumerGuid
	{
		public Tb_Information_ConsumerGuid()
		{}
		#region Model
		private long _guideid;
		private long _bussid;
		private string _title;
		private string _gudpublisher;
		private DateTime? _pubulishdate;
		private string _gudcontent;
		private string _gudimage;
		private long _numid;
		private int? _isdelete;
		/// <summary>
		/// 
		/// </summary>
		public long GuideId
		{
			set{ _guideid=value;}
			get{return _guideid;}
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
		public string GudPublisher
		{
			set{ _gudpublisher=value;}
			get{return _gudpublisher;}
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
		public string GudContent
		{
			set{ _gudcontent=value;}
			get{return _gudcontent;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string GudImage
		{
			set{ _gudimage=value;}
			get{return _gudimage;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long NumID
		{
			set{ _numid=value;}
			get{return _numid;}
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

