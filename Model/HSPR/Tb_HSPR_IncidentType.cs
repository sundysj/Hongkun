using System;
namespace MobileSoft.Model.HSPR
{
	/// <summary>
	/// 实体类Tb_HSPR_IncidentType 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_HSPR_IncidentType
	{
		public Tb_HSPR_IncidentType()
		{}
		#region Model
		private long _typeid;
		private long _corptypeid;
		private int? _commid;
		private string _typecode;
		private string _typename;
		private int? _deallimit;
		private int? _reservehint;
		private string _typememo;
		private int? _istreeroot;
		private string _incidentplace;
		private int? _deallimit2;
		private int? _classid;
		/// <summary>
		/// 
		/// </summary>
		public long TypeID
		{
			set{ _typeid=value;}
			get{return _typeid;}
		}
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
		public int? CommID
		{
			set{ _commid=value;}
			get{return _commid;}
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

