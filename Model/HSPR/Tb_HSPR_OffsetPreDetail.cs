using System;
namespace MobileSoft.Model.HSPR
{
	/// <summary>
	/// 实体类Tb_HSPR_OffsetPreDetail 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_HSPR_OffsetPreDetail
	{
		public Tb_HSPR_OffsetPreDetail()
		{}
		#region Model
		private long _iid;
		private long _offsetid;
		private int? _commid;
		private long _costid;
		private long _custid;
		private long _roomid;
		private DateTime? _feesduedate;
		private decimal? _debtsamount;
		private decimal? _oldprecamount;
		private decimal? _newprecamount;
		private decimal? _offsetamount;
		private long _feesid;
		private long _precid;
		private int? _takewise;
		private int? _isaudit;
		private int? _isdelete;
		private decimal? _offsetlatefeeamount;
		private long _receid;
		private Guid _rececode;
		private string _delusercode;
		private DateTime? _deldate;
		private string _changememo;
		private int? _isproperty;
		private int? _isautoprec;
		/// <summary>
		/// 
		/// </summary>
		public long IID
		{
			set{ _iid=value;}
			get{return _iid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long OffsetID
		{
			set{ _offsetid=value;}
			get{return _offsetid;}
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
		public long CustID
		{
			set{ _custid=value;}
			get{return _custid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long RoomID
		{
			set{ _roomid=value;}
			get{return _roomid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? FeesDueDate
		{
			set{ _feesduedate=value;}
			get{return _feesduedate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? DebtsAmount
		{
			set{ _debtsamount=value;}
			get{return _debtsamount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? OldPrecAmount
		{
			set{ _oldprecamount=value;}
			get{return _oldprecamount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? NewPrecAmount
		{
			set{ _newprecamount=value;}
			get{return _newprecamount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? OffsetAmount
		{
			set{ _offsetamount=value;}
			get{return _offsetamount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long FeesID
		{
			set{ _feesid=value;}
			get{return _feesid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long PrecID
		{
			set{ _precid=value;}
			get{return _precid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? TakeWise
		{
			set{ _takewise=value;}
			get{return _takewise;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsAudit
		{
			set{ _isaudit=value;}
			get{return _isaudit;}
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
		public decimal? OffsetLateFeeAmount
		{
			set{ _offsetlatefeeamount=value;}
			get{return _offsetlatefeeamount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long ReceID
		{
			set{ _receid=value;}
			get{return _receid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public Guid ReceCode
		{
			set{ _rececode=value;}
			get{return _rececode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DelUserCode
		{
			set{ _delusercode=value;}
			get{return _delusercode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? DelDate
		{
			set{ _deldate=value;}
			get{return _deldate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ChangeMemo
		{
			set{ _changememo=value;}
			get{return _changememo;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsProperty
		{
			set{ _isproperty=value;}
			get{return _isproperty;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsAutoPrec
		{
			set{ _isautoprec=value;}
			get{return _isautoprec;}
		}
		#endregion Model

	}
}

