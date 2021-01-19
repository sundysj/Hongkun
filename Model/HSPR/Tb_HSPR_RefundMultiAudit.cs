using System;
namespace MobileSoft.Model.HSPR
{
	/// <summary>
	/// 实体类Tb_HSPR_RefundMultiAudit 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_HSPR_RefundMultiAudit
	{
		public Tb_HSPR_RefundMultiAudit()
		{}
		#region Model
		private long _iid;
		private int? _businesstype;
		private int? _commid;
		private long _custid;
		private long _roomid;
		private long _costid;
		private long _feesid;
		private long _refundid;
		private long _precid;
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
		public int? BusinessType
		{
			set{ _businesstype=value;}
			get{return _businesstype;}
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
		public long CostID
		{
			set{ _costid=value;}
			get{return _costid;}
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
		public long RefundID
		{
			set{ _refundid=value;}
			get{return _refundid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long PrecID
		{
			set{ _precid=value;}
			get{return _precid;}
		}
		#endregion Model

	}
}

