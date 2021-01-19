using System;
namespace MobileSoft.Model.Resources
{
	/// <summary>
	/// 实体类Tb_Resources_Details 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_Resources_Details
	{
		public Tb_Resources_Details()
		{}
		#region Model
		private long _resourcesid;
		private long _bussid;
		private long _resourcestypeid;
		private string _resourcesname;
		private string _resourcessimple;
		private int? _resourcesindex;
		private string _resourcesbarcode;
		private string _resourcescode;
		private string _resourcesunit;
		private decimal? _resourcescount;
		private string _resourcespriceunit;
		private decimal? _resourcessaleprice;
		private decimal? _resourcesdiscountprice;
		private bool _isrelease;
		private string _scheduletype;
		private bool _isstoprelease;
		private string _remark;
		private int? _isdelete;
		/// <summary>
		/// 
		/// </summary>
		public long ResourcesID
		{
			set{ _resourcesid=value;}
			get{return _resourcesid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long BussId
		{
			set{ _bussid=value;}
			get{return _bussid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long ResourcesTypeID
		{
			set{ _resourcestypeid=value;}
			get{return _resourcestypeid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ResourcesName
		{
			set{ _resourcesname=value;}
			get{return _resourcesname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ResourcesSimple
		{
			set{ _resourcessimple=value;}
			get{return _resourcessimple;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ResourcesIndex
		{
			set{ _resourcesindex=value;}
			get{return _resourcesindex;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ResourcesBarCode
		{
			set{ _resourcesbarcode=value;}
			get{return _resourcesbarcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ResourcesCode
		{
			set{ _resourcescode=value;}
			get{return _resourcescode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ResourcesUnit
		{
			set{ _resourcesunit=value;}
			get{return _resourcesunit;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? ResourcesCount
		{
			set{ _resourcescount=value;}
			get{return _resourcescount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ResourcesPriceUnit
		{
			set{ _resourcespriceunit=value;}
			get{return _resourcespriceunit;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? ResourcesSalePrice
		{
			set{ _resourcessaleprice=value;}
			get{return _resourcessaleprice;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? ResourcesDisCountPrice
		{
			set{ _resourcesdiscountprice=value;}
			get{return _resourcesdiscountprice;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool IsRelease
		{
			set{ _isrelease=value;}
			get{return _isrelease;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ScheduleType
		{
			set{ _scheduletype=value;}
			get{return _scheduletype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool IsStopRelease
		{
			set{ _isstoprelease=value;}
			get{return _isstoprelease;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
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

