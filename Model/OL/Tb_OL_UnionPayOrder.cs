using System;
namespace MobileSoft.Model.OL
{
	/// <summary>
	/// 实体类Tb_OL_UnionPayOrder 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_OL_UnionPayOrder
	{
		public Tb_OL_UnionPayOrder()
		{}
		#region Model
		private string _id;
		private int _commid;
		private string _communityid;
		private long _custid;
		private long _merid;
		private string _orderid;
		private string _txntime;
		private string _tn;
		private DateTime _orderdate;
		private string _respcode;
		private string _respmsg;
		private DateTime? _respdate;
		private int? _issucc;
		private string _memo;
		private int? _isdelete;
		/// <summary>
		/// 银联支付下单
		/// </summary>
		public string Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 小区ID
		/// </summary>
		public int CommID
		{
			set{ _commid=value;}
			get{return _commid;}
		}
		/// <summary>
		/// 统一数据小区ID
		/// </summary>
		public string CommunityId
		{
			set{ _communityid=value;}
			get{return _communityid;}
		}
		/// <summary>
		/// 业主ID
		/// </summary>
		public long CustId
		{
			set{ _custid=value;}
			get{return _custid;}
		}
		/// <summary>
		/// 商户号
		/// </summary>
		public long merId
		{
			set{ _merid=value;}
			get{return _merid;}
		}
		/// <summary>
		/// 订单号
		/// </summary>
		public string orderId
		{
			set{ _orderid=value;}
			get{return _orderid;}
		}
		/// <summary>
		/// 订单时间
		/// </summary>
		public string txnTime
		{
			set{ _txntime=value;}
			get{return _txntime;}
		}
		/// <summary>
		/// 银行流水号
		/// </summary>
		public string Tn
		{
			set{ _tn=value;}
			get{return _tn;}
		}
		/// <summary>
		/// 发生时间
		/// </summary>
		public DateTime OrderDate
		{
			set{ _orderdate=value;}
			get{return _orderdate;}
		}
		/// <summary>
		/// 应答码
		/// </summary>
		public string respCode
		{
			set{ _respcode=value;}
			get{return _respcode;}
		}
		/// <summary>
		/// 应答信息
		/// </summary>
		public string respMsg
		{
			set{ _respmsg=value;}
			get{return _respmsg;}
		}
		/// <summary>
		/// 回调时间
		/// </summary>
		public DateTime? respDate
		{
			set{ _respdate=value;}
			get{return _respdate;}
		}
		/// <summary>
		/// 是否下账
		/// </summary>
		public int? IsSucc
		{
			set{ _issucc=value;}
			get{return _issucc;}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string Memo
		{
			set{ _memo=value;}
			get{return _memo;}
		}
		/// <summary>
		/// 是否撤销
		/// </summary>
		public int? IsDelete
		{
			set{ _isdelete=value;}
			get{return _isdelete;}
		}
		#endregion Model

	}
}

