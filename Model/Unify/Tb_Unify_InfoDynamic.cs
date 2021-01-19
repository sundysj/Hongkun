using System;
namespace MobileSoft.Model.Unify
{
	/// <summary>
	/// 实体类Tb_Unify_InfoDynamic 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_Unify_InfoDynamic
	{
		public Tb_Unify_InfoDynamic()
		{}
		#region Model
		private long _infoid;
		private int? _infotype;
		private string _typename;
		private string _heading;
		private DateTime? _issuedate;
		private string _infosource;
		private string _imageurl;
		private int? _recommended;
		private int? _hitcount;
		private string _infocontent;
		/// <summary>
		/// 
		/// </summary>
		public long InfoID
		{
			set{ _infoid=value;}
			get{return _infoid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? InfoType
		{
			set{ _infotype=value;}
			get{return _infotype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TypeName
		{
			set{ _typename=value;}
			get{return _typename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Heading
		{
			set{ _heading=value;}
			get{return _heading;}
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
		public string InfoSource
		{
			set{ _infosource=value;}
			get{return _infosource;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ImageUrl
		{
			set{ _imageurl=value;}
			get{return _imageurl;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Recommended
		{
			set{ _recommended=value;}
			get{return _recommended;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? HitCount
		{
			set{ _hitcount=value;}
			get{return _hitcount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string InfoContent
		{
			set{ _infocontent=value;}
			get{return _infocontent;}
		}
		#endregion Model

	}
}

