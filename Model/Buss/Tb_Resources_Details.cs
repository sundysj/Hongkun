using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Model.Model.Buss
{
	/// <summary>
	/// ʵ����Tb_Resources_Details ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class Tb_Resources_Details
	{
		public Tb_Resources_Details()
		{}
		#region Model
		private string _resourcesid;
		private string _bussid;
		private string _resourcestypeid;
		private string _resourcesname;
		private string _resourcessimple;
		private int _resourcesindex;
		private string _resourcesbarcode;
		private string _resourcescode;
		private string _resourcesunit;
		private long _resourcescount;
		private string _resourcespriceunit;
		private decimal _resourcessaleprice;
		private decimal _resourcesdiscountprice;
		private string _isrelease;
		private string _scheduletype;
		private string _isstoprelease;
		private string _remark;
		private string _releaseadcontent;
		private string _isgroupbuy;
		private decimal? _groupbuyprice;
		private DateTime? _groupbuystartdate;
		private DateTime? _groupbuyenddate;
		private string _paymenttype;
		private DateTime? _createdate;
		private string _isbp;
		private string _img;
		private int? _isdelete;
		[DisplayName("��Դ��ID")]
		public string ResourcesID
		{
			set{ _resourcesid=value;}
			get{return _resourcesid;}
		}
		[DisplayName("�̼�ID")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "����")]
		public string BussId
		{
			set{ _bussid=value;}
			get{return _bussid;}
		}
		[DisplayName("��Դ���ID")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "����")]
		public string ResourcesTypeID
		{
			set{ _resourcestypeid=value;}
			get{return _resourcestypeid;}
		}
		[DisplayName("��Դ����")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "����")]
		public string ResourcesName
		{
			set{ _resourcesname=value;}
			get{return _resourcesname;}
		}
		[DisplayName("��Դ��ƴ")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "����")]
		public string ResourcesSimple
		{
			set{ _resourcessimple=value;}
			get{return _resourcessimple;}
		}
		[DisplayName("���")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "����")]
		public int ResourcesIndex
		{
			set{ _resourcesindex=value;}
			get{return _resourcesindex;}
		}
		[DisplayName("����")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "����")]
		public string ResourcesBarCode
		{
			set{ _resourcesbarcode=value;}
			get{return _resourcesbarcode;}
		}
		[DisplayName("����")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "����")]
		public string ResourcesCode
		{
			set{ _resourcescode=value;}
			get{return _resourcescode;}
		}
		[DisplayName("��λ")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "����")]
		public string ResourcesUnit
		{
			set{ _resourcesunit=value;}
			get{return _resourcesunit;}
		}
		[DisplayName("��Դ����")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "����")]
		public long ResourcesCount
		{
			set{ _resourcescount=value;}
			get{return _resourcescount;}
		}
		[DisplayName("�۸�λ")]
		public string ResourcesPriceUnit
		{
			set{ _resourcespriceunit=value;}
			get{return _resourcespriceunit;}
		}
		[DisplayName("���۵���")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "����")]
		public decimal ResourcesSalePrice
		{
			set{ _resourcessaleprice=value;}
			get{return _resourcessaleprice;}
		}
		[DisplayName("�Żݵ���")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "����")]
		public decimal ResourcesDisCountPrice
		{
			set{ _resourcesdiscountprice=value;}
			get{return _resourcesdiscountprice;}
		}
		[DisplayName("�Ƿ񷢲�")]
		public string IsRelease
		{
			set{ _isrelease=value;}
			get{return _isrelease;}
		}
		[DisplayName("Ԥ������")]
		public string ScheduleType
		{
			set{ _scheduletype=value;}
			get{return _scheduletype;}
		}
		[DisplayName("ֹͣ����")]
		public string IsStopRelease
		{
			set{ _isstoprelease=value;}
			get{return _isstoprelease;}
		}
		[DisplayName("��ע")]
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		[DisplayName("��Ʒ����")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "����")]
		public string ReleaseAdContent
		{
			set{ _releaseadcontent=value;}
			get{return _releaseadcontent;}
		}
		[DisplayName("�Ƿ��Ź�")]
		public string IsGroupBuy
		{
			set{ _isgroupbuy=value;}
			get{return _isgroupbuy;}
		}
		[DisplayName("�Ź�����")]
		public decimal? GroupBuyPrice
		{
			set{ _groupbuyprice=value;}
			get{return _groupbuyprice;}
		}
		[DisplayName("�Ź���ʼ����")]
		public DateTime? GroupBuyStartDate
		{
			set{ _groupbuystartdate=value;}
			get{return _groupbuystartdate;}
		}
		[DisplayName("�Ź���������")]
		public DateTime? GroupBuyEndDate
		{
			set{ _groupbuyenddate=value;}
			get{return _groupbuyenddate;}
		}
		[DisplayName("֧����ʽ")]
		public string PaymentType
		{
			set{ _paymenttype=value;}
			get{return _paymenttype;}
		}
		[DisplayName("��������")]
		public DateTime? CreateDate
		{
			set{ _createdate=value;}
			get{return _createdate;}
		}
		[DisplayName("�Ƿ�Ʒ")]
		public string IsBp
		{
			set{ _isbp=value;}
			get{return _isbp;}
		}
		[DisplayName("��ƷͼƬ")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "����")]
		public string Img
		{
			set{ _img=value;}
			get{return _img;}
		}
		[DisplayName("�Ƿ�ɾ��")]
		public int? IsDelete
		{
			set{ _isdelete=value;}
			get{return _isdelete;}
		}
        /// <summary>
        /// �Ƿ�֧���Ż�ȯ
        /// </summary>
        public string IsSupportCoupon { get; set; }

        /// <summary>
        /// ���ֿ۽��
        /// </summary>
        public decimal? MaximumCouponMoney { get; set; }

        #endregion Model

    }
}

