using System;
namespace MobileSoft.Model.HSPR
{
	/// <summary>
	/// 实体类Tb_HSPR_CostItem 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_HSPR_CostItem
	{
		public Tb_HSPR_CostItem()
		{}
		#region Model
		private long _costid;
		private int? _commid;
		private int? _costsnum;
		private string _costname;
		private int? _costtype;
		private int? _costgenetype;
		private long _collunitid;
		private int? _duedate;
		private string _accountssign;
		private string _accountsname;
		private int? _chargecycle;
		private int? _roundingnum;
		private int? _isbank;
		private int? _delindelay;
		private decimal? _delinrates;
		private string _precostsign;
		private string _memo;
		private int? _isdelete;
		private long _corpcostid;
		private string _costcode;
		private string _syscostsign;
		private int? _dueplotdate;
		private long _highcorpcostid;
		private int? _costbigtype;
		private int? _delintype;
		private int? _delinday;

        private string  _pk_project;
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
		public int? CommID
		{
			set{ _commid=value;}
			get{return _commid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? CostSNum
		{
			set{ _costsnum=value;}
			get{return _costsnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CostName
		{
			set{ _costname=value;}
			get{return _costname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? CostType
		{
			set{ _costtype=value;}
			get{return _costtype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? CostGeneType
		{
			set{ _costgenetype=value;}
			get{return _costgenetype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long CollUnitID
		{
			set{ _collunitid=value;}
			get{return _collunitid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? DueDate
		{
			set{ _duedate=value;}
			get{return _duedate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AccountsSign
		{
			set{ _accountssign=value;}
			get{return _accountssign;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AccountsName
		{
			set{ _accountsname=value;}
			get{return _accountsname;}
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
		public int? RoundingNum
		{
			set{ _roundingnum=value;}
			get{return _roundingnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsBank
		{
			set{ _isbank=value;}
			get{return _isbank;}
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
		public decimal? DelinRates
		{
			set{ _delinrates=value;}
			get{return _delinrates;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PreCostSign
		{
			set{ _precostsign=value;}
			get{return _precostsign;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Memo
		{
			set{ _memo=value;}
			get{return _memo;}
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
		public long CorpCostID
		{
			set{ _corpcostid=value;}
			get{return _corpcostid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CostCode
		{
			set{ _costcode=value;}
			get{return _costcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SysCostSign
		{
			set{ _syscostsign=value;}
			get{return _syscostsign;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? DuePlotDate
		{
			set{ _dueplotdate=value;}
			get{return _dueplotdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long HighCorpCostID
		{
			set{ _highcorpcostid=value;}
			get{return _highcorpcostid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? CostBigType
		{
			set{ _costbigtype=value;}
			get{return _costbigtype;}
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

        public string pk_project
        {
            set { _pk_project = value; }
            get { return _pk_project; }
        }

        #endregion Model

    }
}

