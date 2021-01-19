using System;
namespace MobileSoft.Model.OL
{
	/// <summary>
	/// ʵ����Tb_OL_UnionPayOrder ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
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
		/// ����֧���µ�
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
		public long merId
		{
			set{ _merid=value;}
			get{return _merid;}
		}
		/// <summary>
		/// ������
		/// </summary>
		public string orderId
		{
			set{ _orderid=value;}
			get{return _orderid;}
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
		/// ������ˮ��
		/// </summary>
		public string Tn
		{
			set{ _tn=value;}
			get{return _tn;}
		}
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime OrderDate
		{
			set{ _orderdate=value;}
			get{return _orderdate;}
		}
		/// <summary>
		/// Ӧ����
		/// </summary>
		public string respCode
		{
			set{ _respcode=value;}
			get{return _respcode;}
		}
		/// <summary>
		/// Ӧ����Ϣ
		/// </summary>
		public string respMsg
		{
			set{ _respmsg=value;}
			get{return _respmsg;}
		}
		/// <summary>
		/// �ص�ʱ��
		/// </summary>
		public DateTime? respDate
		{
			set{ _respdate=value;}
			get{return _respdate;}
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

