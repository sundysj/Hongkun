using System;
namespace MobileSoft.Model.Information
{
	/// <summary>
	/// 实体类Tb_Information_Activities 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_Information_Activities
	{
		public Tb_Information_Activities()
		{}
		#region Model
		private long _actid;
		private long _bussid;
		private string _title;
		private string _actpublisher;
		private DateTime? _publishdate;
		private string _actcontent;
		private string _actimage;
		private long _numid;
		private int? _isdelete;
		/// <summary>
		/// 
		/// </summary>
		public long ActId
		{
			set{ _actid=value;}
			get{return _actid;}
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
		public string ActPublisher
		{
			set{ _actpublisher=value;}
			get{return _actpublisher;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? PublishDate
		{
			set{ _publishdate=value;}
			get{return _publishdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ActContent
		{
			set{ _actcontent=value;}
			get{return _actcontent;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ActImage
		{
			set{ _actimage=value;}
			get{return _actimage;}
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

