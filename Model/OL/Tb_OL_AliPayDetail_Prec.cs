using System;
namespace MobileSoft.Model.OL
{
	/// <summary>
	/// 实体类Tb_OL_AliPayDetail_Prec 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_OL_AliPayDetail_Prec
	{
		public Tb_OL_AliPayDetail_Prec()
		{}
		#region Model
		private string _id;
		private string _payorderid;
		private long _roomid;
		private long _costid;
		private decimal? _dueamount;
		private decimal? _latefeeamount;
		private decimal? _paidamount;
		/// <summary>
		/// 
		/// </summary>
		public string Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PayOrderId
		{
			set{ _payorderid=value;}
			get{return _payorderid;}
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
		public long CostId
		{
			set{ _costid=value;}
			get{return _costid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? DueAmount
		{
			set{ _dueamount=value;}
			get{return _dueamount;}
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
		public decimal? PaidAmount
		{
			set{ _paidamount=value;}
			get{return _paidamount;}
		}
		#endregion Model

	}
}

