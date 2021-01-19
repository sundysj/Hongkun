using System;
namespace MobileSoft.Model.OL
{
	/// <summary>
	/// ʵ����Tb_OL_AlipayOrder ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
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
		/// С��ID
		/// </summary>
		public int? CommID
		{
			set{ _commid=value;}
			get{return _commid;}
		}
		/// <summary>
		/// ͳһ����С��ID
		/// </summary>
		public string CommunityId
		{
			set{ _communityid=value;}
			get{return _communityid;}
		}
		/// <summary>
		/// ҵ��ID
		/// </summary>
		public long CustId
		{
			set{ _custid=value;}
			get{return _custid;}
		}
		/// <summary>
		/// �������ID��
		/// </summary>
		public string partner
		{
			set{ _partner=value;}
			get{return _partner;}
		}
		/// <summary>
		/// ������
		/// </summary>
		public string out_trade_no
		{
			set{ _out_trade_no=value;}
			get{return _out_trade_no;}
		}
		/// <summary>
		/// ��������
		/// </summary>
		public string prepay_str
		{
			set{ _prepay_str=value;}
			get{return _prepay_str;}
		}
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public string txnTime
		{
			set{ _txntime=value;}
			get{return _txntime;}
		}
		/// <summary>
		/// ����״̬
		/// </summary>
		public string trade_status
		{
			set{ _trade_status=value;}
			get{return _trade_status;}
		}
		/// <summary>
		/// ״̬˵��
		/// </summary>
		public string trade_msg
		{
			set{ _trade_msg=value;}
			get{return _trade_msg;}
		}
		/// <summary>
		/// �Ƿ�����
		/// </summary>
		public int? IsSucc
		{
			set{ _issucc=value;}
			get{return _issucc;}
		}
		/// <summary>
		/// ��ע
		/// </summary>
		public string Memo
		{
			set{ _memo=value;}
			get{return _memo;}
		}
		/// <summary>
		/// �Ƿ���
		/// </summary>
		public int? IsDelete
		{
			set{ _isdelete=value;}
			get{return _isdelete;}
		}

		/// <summary>
		/// ����ID
		/// </summary>
		public long? RoomId
		{
			set { _roomid = value; }
			get { return _roomid; }
		}
		#endregion Model

	}
}

