using System;
namespace MobileSoft.Model.HSPR
{
	/// <summary>
	/// 实体类Tb_HSPR_FeesDetail 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_HSPR_FeesDetail
	{
		public Tb_HSPR_FeesDetail()
		{}
		#region Model
		private long _recdid;
		private int? _commid;
		private long _costid;
		private long _custid;
		private long _roomid;
		private long _feesid;
		private long _receid;
		private string _chargemode;
		private int? _accountway;
		private decimal? _chargeamount;
		private decimal? _latefeeamount;
		private DateTime? _chargedate;
		private string _feesmemo;
		private int? _isdelete;
		private long _oldcostid;
		/// <summary>
		/// 
		/// </summary>
		public long RecdID
		{
			set{ _recdid=value;}
			get{return _recdid;}
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
		public long FeesID
		{
			set{ _feesid=value;}
			get{return _feesid;}
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
		public string ChargeMode
		{
			set{ _chargemode=value;}
			get{return _chargemode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? AccountWay
		{
			set{ _accountway=value;}
			get{return _accountway;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? ChargeAmount
		{
			set{ _chargeamount=value;}
			get{return _chargeamount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? LateFeeAmount
		{
			set{ _latefeeamount=value;}
			get{return _latefeeamount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? ChargeDate
		{
			set{ _chargedate=value;}
			get{return _chargedate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string FeesMemo
		{
			set{ _feesmemo=value;}
			get{return _feesmemo;}
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
		public long OldCostID
		{
			set{ _oldcostid=value;}
			get{return _oldcostid;}
		}
		#endregion Model

	}
}

