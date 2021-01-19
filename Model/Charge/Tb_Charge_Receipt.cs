using System;
namespace MobileSoft.Model.Charge
{
	/// <summary>
	/// 实体类Tb_Charge_Receipt 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_Charge_Receipt
	{
		public Tb_Charge_Receipt()
		{}
		#region Model
		private string _receiptcode;
		private long _bussid;
		private string _orderid;
		private string _receiptsign;
		private decimal? _receivables;
		private string _method;
		private string _cardnum;
		private string _membercardcode;
		private string _custcode;
		private decimal? _balance;
		private decimal? _discount;
		private decimal? _amount;
		private string _receiptmemo;
		private string _receipttype;
		private DateTime? _receiptdate;
		private int? _isdelete;
		/// <summary>
		/// 
		/// </summary>
		public string ReceiptCode
		{
			set{ _receiptcode=value;}
			get{return _receiptcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long BussId
		{
			set{ _bussid=value;}
			get{return _bussid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OrderId
		{
			set{ _orderid=value;}
			get{return _orderid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ReceiptSign
		{
			set{ _receiptsign=value;}
			get{return _receiptsign;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? Receivables
		{
			set{ _receivables=value;}
			get{return _receivables;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Method
		{
			set{ _method=value;}
			get{return _method;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CardNum
		{
			set{ _cardnum=value;}
			get{return _cardnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string MemberCardCode
		{
			set{ _membercardcode=value;}
			get{return _membercardcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CustCode
		{
			set{ _custcode=value;}
			get{return _custcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? Balance
		{
			set{ _balance=value;}
			get{return _balance;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? Discount
		{
			set{ _discount=value;}
			get{return _discount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? Amount
		{
			set{ _amount=value;}
			get{return _amount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ReceiptMemo
		{
			set{ _receiptmemo=value;}
			get{return _receiptmemo;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ReceiptType
		{
			set{ _receipttype=value;}
			get{return _receipttype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? ReceiptDate
		{
			set{ _receiptdate=value;}
			get{return _receiptdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsDelete
		{
			set{ _isdelete=value;}
			get{return _isdelete;}
		}
		#endregion Model

	}
}

