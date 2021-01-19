using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Model.Model.Buss
{
	/// <summary>
	/// 实体类Tb_Resources_Details 。(属性说明自动提取数据库字段的描述信息)
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
		[DisplayName("资源表ID")]
		public string ResourcesID
		{
			set{ _resourcesid=value;}
			get{return _resourcesid;}
		}
		[DisplayName("商家ID")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "必填")]
		public string BussId
		{
			set{ _bussid=value;}
			get{return _bussid;}
		}
		[DisplayName("资源类别ID")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "必填")]
		public string ResourcesTypeID
		{
			set{ _resourcestypeid=value;}
			get{return _resourcestypeid;}
		}
		[DisplayName("资源名称")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "必填")]
		public string ResourcesName
		{
			set{ _resourcesname=value;}
			get{return _resourcesname;}
		}
		[DisplayName("资源简拼")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "必填")]
		public string ResourcesSimple
		{
			set{ _resourcessimple=value;}
			get{return _resourcessimple;}
		}
		[DisplayName("序号")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "必填")]
		public int ResourcesIndex
		{
			set{ _resourcesindex=value;}
			get{return _resourcesindex;}
		}
		[DisplayName("条码")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "必填")]
		public string ResourcesBarCode
		{
			set{ _resourcesbarcode=value;}
			get{return _resourcesbarcode;}
		}
		[DisplayName("编码")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "必填")]
		public string ResourcesCode
		{
			set{ _resourcescode=value;}
			get{return _resourcescode;}
		}
		[DisplayName("单位")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "必填")]
		public string ResourcesUnit
		{
			set{ _resourcesunit=value;}
			get{return _resourcesunit;}
		}
		[DisplayName("资源数量")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "必填")]
		public long ResourcesCount
		{
			set{ _resourcescount=value;}
			get{return _resourcescount;}
		}
		[DisplayName("价格单位")]
		public string ResourcesPriceUnit
		{
			set{ _resourcespriceunit=value;}
			get{return _resourcespriceunit;}
		}
		[DisplayName("销售单价")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "必填")]
		public decimal ResourcesSalePrice
		{
			set{ _resourcessaleprice=value;}
			get{return _resourcessaleprice;}
		}
		[DisplayName("优惠单价")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "必填")]
		public decimal ResourcesDisCountPrice
		{
			set{ _resourcesdiscountprice=value;}
			get{return _resourcesdiscountprice;}
		}
		[DisplayName("是否发布")]
		public string IsRelease
		{
			set{ _isrelease=value;}
			get{return _isrelease;}
		}
		[DisplayName("预定类型")]
		public string ScheduleType
		{
			set{ _scheduletype=value;}
			get{return _scheduletype;}
		}
		[DisplayName("停止销售")]
		public string IsStopRelease
		{
			set{ _isstoprelease=value;}
			get{return _isstoprelease;}
		}
		[DisplayName("备注")]
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		[DisplayName("商品介绍")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "必填")]
		public string ReleaseAdContent
		{
			set{ _releaseadcontent=value;}
			get{return _releaseadcontent;}
		}
		[DisplayName("是否团购")]
		public string IsGroupBuy
		{
			set{ _isgroupbuy=value;}
			get{return _isgroupbuy;}
		}
		[DisplayName("团购单价")]
		public decimal? GroupBuyPrice
		{
			set{ _groupbuyprice=value;}
			get{return _groupbuyprice;}
		}
		[DisplayName("团购开始日期")]
		public DateTime? GroupBuyStartDate
		{
			set{ _groupbuystartdate=value;}
			get{return _groupbuystartdate;}
		}
		[DisplayName("团购结束日期")]
		public DateTime? GroupBuyEndDate
		{
			set{ _groupbuyenddate=value;}
			get{return _groupbuyenddate;}
		}
		[DisplayName("支付方式")]
		public string PaymentType
		{
			set{ _paymenttype=value;}
			get{return _paymenttype;}
		}
		[DisplayName("创建日期")]
		public DateTime? CreateDate
		{
			set{ _createdate=value;}
			get{return _createdate;}
		}
		[DisplayName("是否爆品")]
		public string IsBp
		{
			set{ _isbp=value;}
			get{return _isbp;}
		}
		[DisplayName("商品图片")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "必填")]
		public string Img
		{
			set{ _img=value;}
			get{return _img;}
		}
		[DisplayName("是否删除")]
		public int? IsDelete
		{
			set{ _isdelete=value;}
			get{return _isdelete;}
		}
        /// <summary>
        /// 是否支持优惠券
        /// </summary>
        public string IsSupportCoupon { get; set; }

        /// <summary>
        /// 最大抵扣金额
        /// </summary>
        public decimal? MaximumCouponMoney { get; set; }

        #endregion Model

    }
}

