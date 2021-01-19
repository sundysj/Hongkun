using System;
namespace MobileSoft.Model.OL
{
	/// <summary>
	/// ʵ����Tb_OL_WeiXinPayOrder ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class Tb_OL_WeiXinPayOrder
	{
		public Tb_OL_WeiXinPayOrder()
		{}
		#region Model
		private string _id;
		private int _commid;
		private string _communityid;
		private long _custid;
		private string _mch_id;
		private string _out_trade_no;
		private string _prepay_id;
		private string _time_start;
		private string _return_code;
		private string _return_msg;
		private int? _issucc;
		private string _memo;
		private int? _isdelete;
		/// <summary>
		/// ΢��֧���µ�
		/// </summary>
		public string Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// С��ID
		/// </summary>
		public int CommID
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
		/// �̻���
		/// </summary>
		public string mch_id
		{
			set{ _mch_id=value;}
			get{return _mch_id;}
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
		/// Ԥ����ID
		/// </summary>
		public string prepay_id
		{
			set{ _prepay_id=value;}
			get{return _prepay_id;}
		}
		/// <summary>
		/// ������ʼʱ��
		/// </summary>
		public string time_start
		{
			set{ _time_start=value;}
			get{return _time_start;}
		}
		/// <summary>
		/// ����״̬��
		/// </summary>
		public string return_code
		{
			set{ _return_code=value;}
			get{return _return_code;}
		}
		/// <summary>
		/// ������Ϣ
		/// </summary>
		public string return_msg
		{
			set{ _return_msg=value;}
			get{return _return_msg;}
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
		#endregion Model

	}
}

