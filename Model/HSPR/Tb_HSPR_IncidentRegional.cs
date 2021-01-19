using System;
namespace MobileSoft.Model.HSPR
{
	/// <summary>
	/// 实体类Tb_HSPR_IncidentRegional 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_HSPR_IncidentRegional
	{
		public Tb_HSPR_IncidentRegional()
		{}
		#region Model
		private long _regionalid;
		private int? _commid;
		private int? _regionalnum;
		private string _regionalplace;
		private string _regionalname;
		private string _regionalmemo;
		private int? _isdelete;
		/// <summary>
		/// 
		/// </summary>
		public long RegionalID
		{
			set{ _regionalid=value;}
			get{return _regionalid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? CommID
		{
			set{ _commid=value;}
			get{return _commid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? RegionalNum
		{
			set{ _regionalnum=value;}
			get{return _regionalnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RegionalPlace
		{
			set{ _regionalplace=value;}
			get{return _regionalplace;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RegionalName
		{
			set{ _regionalname=value;}
			get{return _regionalname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RegionalMemo
		{
			set{ _regionalmemo=value;}
			get{return _regionalmemo;}
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

