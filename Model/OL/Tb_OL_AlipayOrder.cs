using System;
namespace MobileSoft.Model.OL
{
	/// <summary>
	/// 实体类Tb_OL_AlipayOrder 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_OL_AlipayOrder
	{
		public Tb_OL_AlipayOrder()
		{}
		#region Model
		private string _id;
		private int? _commid;
		private string _communityid;
		private long _custid;
		private string _partner;
		private string _out_trade_no;
		private string _prepay_str;
		private string _txntime;
		private string _trade_status;
		private string _trade_msg;
		private int? _issucc;
		private string _memo;
		private int? _isdelete;
		private long? _roomid;
		/// <summary>
		/// 
		/// </summary>
		public string Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 小区ID
		/// </summary>
		public int? CommID
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
		/// 合作伙伴ID号
		/// </summary>
		public string partner
		{
			set{ _partner=value;}
			get{return _partner;}
		}
		/// <summary>
		/// 订单号
		/// </summary>
		public string out_trade_no
		{
			set{ _out_trade_no=value;}
			get{return _out_trade_no;}
		}
		/// <summary>
		/// 订单请求串
		/// </summary>
		public string prepay_str
		{
			set{ _prepay_str=value;}
			get{return _prepay_str;}
		}
		/// <summary>
		/// 交易时间
		/// </summary>
		public string txnTime
		{
			set{ _txntime=value;}
			get{return _txntime;}
		}
		/// <summary>
		/// 交易状态
		/// </summary>
		public string trade_status
		{
			set{ _trade_status=value;}
			get{return _trade_status;}
		}
		/// <summary>
		/// 状态说明
		/// </summary>
		public string trade_msg
		{
			set{ _trade_msg=value;}
			get{return _trade_msg;}
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

		/// <summary>
		/// 房间ID
		/// </summary>
		public long? RoomId
		{
			set { _roomid = value; }
			get { return _roomid; }
		}
		#endregion Model

	}
}

