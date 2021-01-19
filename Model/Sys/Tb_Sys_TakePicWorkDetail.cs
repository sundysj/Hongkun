using System;
namespace MobileSoft.Model.Sys
{
	/// <summary>
	/// 实体类Tb_Sys_TakePicWorkDetail 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_Sys_TakePicWorkDetail
	{
		public Tb_Sys_TakePicWorkDetail()
		{}
		#region Model
		private long _detailid;
		private string _usercode;
		private int? _stattype;
		private int? _commid;
		private string _organcode;
		private int? _counts;
		/// <summary>
		/// 
		/// </summary>
		public long DetailID
		{
			set{ _detailid=value;}
			get{return _detailid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UserCode
		{
			set{ _usercode=value;}
			get{return _usercode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? StatType
		{
			set{ _stattype=value;}
			get{return _stattype;}
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
		public string OrganCode
		{
			set{ _organcode=value;}
			get{return _organcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Counts
		{
			set{ _counts=value;}
			get{return _counts;}
		}
		#endregion Model

	}
}

