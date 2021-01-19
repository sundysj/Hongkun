using System;
namespace MobileSoft.Model.HSPR
{
	/// <summary>
	/// 实体类Tb_HSPR_IncidentLocation 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_HSPR_IncidentLocation
	{
		public Tb_HSPR_IncidentLocation()
		{}
		#region Model
		private long _locationid;
		private int? _commid;
		private string _locationname;
		private int? _locationnum;
		private long _regionalid;
		private int? _isdelete;
		/// <summary>
		/// 
		/// </summary>
		public long LocationID
		{
			set{ _locationid=value;}
			get{return _locationid;}
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
		public string LocationName
		{
			set{ _locationname=value;}
			get{return _locationname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? LocationNum
		{
			set{ _locationnum=value;}
			get{return _locationnum;}
		}
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
		public int? IsDelete
		{
			set{ _isdelete=value;}
			get{return _isdelete;}
		}
		#endregion Model

	}
}

