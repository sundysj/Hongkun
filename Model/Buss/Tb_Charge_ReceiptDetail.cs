using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Model.Model.Buss
{
	/// <summary>
	/// ʵ����Tb_Charge_ReceiptDetail ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class Tb_Charge_ReceiptDetail
	{
		public Tb_Charge_ReceiptDetail()
		{}
		#region Model
		private string _rpdcode;
		private string _receiptcode;
		private string _resourcesid;
		private int _quantity;
		private decimal? _salesprice;
		private decimal? _discountprice;
		private decimal? _groupprice;
		private decimal? _detailamount;
		private string _rpdmemo;
        private string _shoppingId;
        private int? _rpdisdelete;
		[DisplayName("��Ʒ����Ʊ����ϸID")]
		public string RpdCode
		{
			set{ _rpdcode=value;}
			get{return _rpdcode;}
		}
		[DisplayName("Ʊ�ݱ���")]
		public string ReceiptCode
		{
			set{ _receiptcode=value;}
			get{return _receiptcode;}
		}
		[DisplayName("��ԴID")]
		public string ResourcesID
		{
			set{ _resourcesid=value;}
			get{return _resourcesid;}
		}
		[DisplayName("����")]
		public int Quantity
		{
			set{ _quantity=value;}
			get{return _quantity;}
		}
		[DisplayName("���۵���")]
		public decimal? SalesPrice
		{
			set{ _salesprice=value;}
			get{return _salesprice;}
		}
		[DisplayName("�Żݵ���")]
		public decimal? DiscountPrice
		{
			set{ _discountprice=value;}
			get{return _discountprice;}
		}
		[DisplayName("�Ź�����")]
		public decimal? GroupPrice
		{
			set{ _groupprice=value;}
			get{return _groupprice;}
		}
		[DisplayName("�ϼƵ���")]
		public decimal? DetailAmount
		{
			set{ _detailamount=value;}
			get{return _detailamount;}
		}
		[DisplayName("��ע")]
		public string RpdMemo
		{
			set{ _rpdmemo=value;}
			get{return _rpdmemo;}
		}
		[DisplayName("�Ƿ�ɾ��")]
		public int? RpdIsDelete
		{
			set{ _rpdisdelete=value;}
			get{return _rpdisdelete;}
		}
        [DisplayName("���ﳵ��Ʒ��ϸID")]
        public string ShoppingId
        {
            set { _shoppingId = value; }
            get { return _shoppingId; }
        }
        /// <summary>
        /// �̼�ר��ȯ�ֿ۽��
        /// </summary>
        public decimal? OffsetMoney { get; set; }

        /// <summary>
        /// ���ͨ��ȯ�ֿ۽��
        /// </summary>
        public decimal? OffsetMoney2 { get; set; }

        public int? IsEvaluated { get; set; }

        #endregion Model

    }
}

