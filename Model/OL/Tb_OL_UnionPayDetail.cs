using System;
namespace MobileSoft.Model.OL
{
	/// <summary>
	/// ʵ����Tb_OL_UnionPayDetail ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class Tb_OL_UnionPayDetail
	{
		public Tb_OL_UnionPayDetail()
		{}
		#region Model
		private string _id;
		private string _payorderid;
		private long _feesid;
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
		public long FeesId
		{
			set{ _feesid=value;}
			get{return _feesid;}
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
