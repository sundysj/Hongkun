using System;
namespace MobileSoft.Model.HSPR
{
	/// <summary>
	/// 实体类Tb_HSPR_CommunityCircle 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_HSPR_CommunityCircle
	{
		public Tb_HSPR_CommunityCircle()
		{}
		#region Model
		private string _circleid;
		private string _circlename;
		private string _circlememo;
		private int? _circleindex;
		private string _bussname;
		private long _bussid;
		private long _commid;
		private int? _isdelete;
		/// <summary>
		/// 
		/// </summary>
		public string CircleID
		{
			set{ _circleid=value;}
			get{return _circleid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CircleName
		{
			set{ _circlename=value;}
			get{return _circlename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CircleMemo
		{
			set{ _circlememo=value;}
			get{return _circlememo;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? CircleIndex
		{
			set{ _circleindex=value;}
			get{return _circleindex;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BussName
		{
			set{ _bussname=value;}
			get{return _bussname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long BussID
		{
			set{ _bussid=value;}
			get{return _bussid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long CommID
		{
			set{ _commid=value;}
			get{return _commid;}
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

