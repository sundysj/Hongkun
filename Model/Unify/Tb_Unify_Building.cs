using System;
namespace ehome.Model.Unify
{
	/// <summary>
	/// 实体类Tb_Unify_Building 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_Unify_Building
	{
		public Tb_Unify_Building()
		{}
		#region Model
		private Guid _buildsynchcode;
		private Guid _commsynchcode;
		private long _unbuildid;
		private int? _buildsnum;
		private int? _regionsnum;
		private string _buildsign;
		private string _buildname;
		private string _buildtype;
		private string _builduses;
		private string _propertyrights;
		private string _propertyuses;
		private string _buildheight;
		private int? _floorsnum;
		private int? _underfloorsnum;
		private int? _unitnum;
		private string _namingpatterns;
		private int? _perfloornum;
		private int? _householdsnum;
		private int? _isdelete;
		private int? _synchflag;
		private long _ordsnum;
		/// <summary>
		/// 
		/// </summary>
		public Guid BuildSynchCode
		{
			set{ _buildsynchcode=value;}
			get{return _buildsynchcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public Guid CommSynchCode
		{
			set{ _commsynchcode=value;}
			get{return _commsynchcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long UnBuildID
		{
			set{ _unbuildid=value;}
			get{return _unbuildid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? BuildSNum
		{
			set{ _buildsnum=value;}
			get{return _buildsnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? RegionSNum
		{
			set{ _regionsnum=value;}
			get{return _regionsnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BuildSign
		{
			set{ _buildsign=value;}
			get{return _buildsign;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BuildName
		{
			set{ _buildname=value;}
			get{return _buildname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BuildType
		{
			set{ _buildtype=value;}
			get{return _buildtype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BuildUses
		{
			set{ _builduses=value;}
			get{return _builduses;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PropertyRights
		{
			set{ _propertyrights=value;}
			get{return _propertyrights;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PropertyUses
		{
			set{ _propertyuses=value;}
			get{return _propertyuses;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BuildHeight
		{
			set{ _buildheight=value;}
			get{return _buildheight;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? FloorsNum
		{
			set{ _floorsnum=value;}
			get{return _floorsnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? UnderFloorsNum
		{
			set{ _underfloorsnum=value;}
			get{return _underfloorsnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? UnitNum
		{
			set{ _unitnum=value;}
			get{return _unitnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string NamingPatterns
		{
			set{ _namingpatterns=value;}
			get{return _namingpatterns;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? PerFloorNum
		{
			set{ _perfloornum=value;}
			get{return _perfloornum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? HouseholdsNum
		{
			set{ _householdsnum=value;}
			get{return _householdsnum;}
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
		public int? SynchFlag
		{
			set{ _synchflag=value;}
			get{return _synchflag;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long OrdSNum
		{
			set{ _ordsnum=value;}
			get{return _ordsnum;}
		}
		#endregion Model

	}
}

