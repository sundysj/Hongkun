using System;
namespace MobileSoft.Model.HSPR
{
	/// <summary>
	/// 实体类Tb_HSPR_CommunityBuss 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_HSPR_CommunityBuss
	{
		public Tb_HSPR_CommunityBuss()
		{}
		#region Model
		private string _commbussid;
		private string _bussname;
		private long _bussid;
		private long _commid;
		private string _busstypename;
		private int? _bussindex;
		private DateTime? _startdate;
		private DateTime? _enddate;
		private string _memo;
		private int? _busstype;
		private int? _isdelete;
		/// <summary>
		/// 
		/// </summary>
		public string CommBussID
		{
			set{ _commbussid=value;}
			get{return _commbussid;}
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
		public string BussTypeName
		{
			set{ _busstypename=value;}
			get{return _busstypename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? BussIndex
		{
			set{ _bussindex=value;}
			get{return _bussindex;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? StartDate
		{
			set{ _startdate=value;}
			get{return _startdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? EndDate
		{
			set{ _enddate=value;}
			get{return _enddate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Memo
		{
			set{ _memo=value;}
			get{return _memo;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? BussType
		{
			set{ _busstype=value;}
			get{return _busstype;}
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

