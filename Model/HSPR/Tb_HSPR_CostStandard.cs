using System;
namespace MobileSoft.Model.HSPR
{
	/// <summary>
	/// 实体类Tb_HSPR_CostStandard 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_HSPR_CostStandard
	{
		public Tb_HSPR_CostStandard()
		{}
		#region Model
		private long _stanid;
		private int? _commid;
		private long _costid;
		private string _stansign;
		private string _stanname;
		private string _stanexplain;
		private string _stanformula;
		private decimal? _stanamount;
		private DateTime? _stanstartdate;
		private DateTime? _stanenddate;
		private int? _iscondition;
		private string _conditionfield;
		private decimal? _delinrates;
		private int? _delindelay;
		private int? _isdelete;
		private int? _isstanrange;
		private int? _chargecycle;
		private int? _managefeesstyle;
		private decimal? _managefeesamount;
		private long _corpstanid;
		private long _corpcostid;
		private decimal? _amountrounded;
		private decimal? _modulus;
		private int? _delintype;
		private int? _delinday;
		/// <summary>
		/// 
		/// </summary>
		public long StanID
		{
			set{ _stanid=value;}
			get{return _stanid;}
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
		public long CostID
		{
			set{ _costid=value;}
			get{return _costid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string StanSign
		{
			set{ _stansign=value;}
			get{return _stansign;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string StanName
		{
			set{ _stanname=value;}
			get{return _stanname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string StanExplain
		{
			set{ _stanexplain=value;}
			get{return _stanexplain;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string StanFormula
		{
			set{ _stanformula=value;}
			get{return _stanformula;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? StanAmount
		{
			set{ _stanamount=value;}
			get{return _stanamount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? StanStartDate
		{
			set{ _stanstartdate=value;}
			get{return _stanstartdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? StanEndDate
		{
			set{ _stanenddate=value;}
			get{return _stanenddate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsCondition
		{
			set{ _iscondition=value;}
			get{return _iscondition;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ConditionField
		{
			set{ _conditionfield=value;}
			get{return _conditionfield;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? DelinRates
		{
			set{ _delinrates=value;}
			get{return _delinrates;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? DelinDelay
		{
			set{ _delindelay=value;}
			get{return _delindelay;}
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
		public int? IsStanRange
		{
			set{ _isstanrange=value;}
			get{return _isstanrange;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ChargeCycle
		{
			set{ _chargecycle=value;}
			get{return _chargecycle;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ManageFeesStyle
		{
			set{ _managefeesstyle=value;}
			get{return _managefeesstyle;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? ManageFeesAmount
		{
			set{ _managefeesamount=value;}
			get{return _managefeesamount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long CorpStanID
		{
			set{ _corpstanid=value;}
			get{return _corpstanid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long CorpCostID
		{
			set{ _corpcostid=value;}
			get{return _corpcostid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? AmountRounded
		{
			set{ _amountrounded=value;}
			get{return _amountrounded;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? Modulus
		{
			set{ _modulus=value;}
			get{return _modulus;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? DelinType
		{
			set{ _delintype=value;}
			get{return _delintype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? DelinDay
		{
			set{ _delinday=value;}
			get{return _delinday;}
		}
		#endregion Model

	}
}

