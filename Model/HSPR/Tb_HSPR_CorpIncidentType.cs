using System;
namespace MobileSoft.Model.HSPR
{
	/// <summary>
	/// 实体类Tb_HSPR_CorpIncidentType 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_HSPR_CorpIncidentType
	{
		public Tb_HSPR_CorpIncidentType()
		{}
		#region Model
		private long _corptypeid;
		private string _typecode;
		private string _typename;
		private int? _deallimit;
		private int? _reservehint;
		private string _typememo;
		private int? _istreeroot;
		private int? _isdelete;
		private string _incidentplace;
		private int? _deallimit2;
		private int? _classid;
		/// <summary>
		/// 
		/// </summary>
		public long CorpTypeID
		{
			set{ _corptypeid=value;}
			get{return _corptypeid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TypeCode
		{
			set{ _typecode=value;}
			get{return _typecode;}
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
		public int? DealLimit
		{
			set{ _deallimit=value;}
			get{return _deallimit;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ReserveHint
		{
			set{ _reservehint=value;}
			get{return _reservehint;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TypeMemo
		{
			set{ _typememo=value;}
			get{return _typememo;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsTreeRoot
		{
			set{ _istreeroot=value;}
			get{return _istreeroot;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsDelete
		{
			set{ _isdelete=value;}
			get{return _isdelete;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string IncidentPlace
		{
			set{ _incidentplace=value;}
			get{return _incidentplace;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? DealLimit2
		{
			set{ _deallimit2=value;}
			get{return _deallimit2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ClassID
		{
			set{ _classid=value;}
			get{return _classid;}
		}
		#endregion Model

	}
}

