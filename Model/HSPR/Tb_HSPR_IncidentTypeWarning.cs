using System;
namespace MobileSoft.Model.HSPR
{
	/// <summary>
	/// 实体类Tb_HSPR_IncidentTypeWarning 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_HSPR_IncidentTypeWarning
	{
		public Tb_HSPR_IncidentTypeWarning()
		{}
		#region Model
		private long _warningid;
		private int? _commid;
		private string _warningtype;
		private int? _warninghour;
		private string _warningmedo;
		private int? _isdelete;
		/// <summary>
		/// 
		/// </summary>
		public long WarningID
		{
			set{ _warningid=value;}
			get{return _warningid;}
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
		public string WarningType
		{
			set{ _warningtype=value;}
			get{return _warningtype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? WarningHour
		{
			set{ _warninghour=value;}
			get{return _warninghour;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string WarningMedo
		{
			set{ _warningmedo=value;}
			get{return _warningmedo;}
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

