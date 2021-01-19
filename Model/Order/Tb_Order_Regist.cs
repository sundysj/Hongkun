using System;
namespace MobileSoft.Model.Order
{
	/// <summary>
	/// 实体类Tb_Order_Regist 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_Order_Regist
	{
		public Tb_Order_Regist()
		{}
		#region Model
		private string _registid;
		private string _registnumber;
		private string _customertype;
		private int? _commid;
		private string _custname;
		private long _custid;
		private string _mobilephone;
		private string _address;
		private DateTime? _documentdate;
		private string _registmessage;
		private string _processingstatus;
		private string _processingexplain;
		private long _releaseid;
		private string _scheduletype;
		private DateTime? _releaseservicecomdate;
		private string _releaseservicecomtime;
		private DateTime? _releasestarttime;
		private DateTime? _releaseendtime;
		private string _releaseperiodofstarttime;
		private string _releaseperiodofendtime;
		private string _usercode;
		private DateTime? _issuedate;
		private long _bussid;
		private int? _isdelete;
		private decimal? _sumcount;
		private decimal? _sumprice;
        private decimal? _resourcessaleprice;
        private decimal? _resourcesdiscountprice;
        private decimal? _groupbuyprice;
        private string _registwareidcount;
        private string _releasecountlist;
        private string _releaseidlist;

        private string _releasespricelist;//销售
        private string _releasedpricelist;//优惠
        private string _releasetpricelist;//团购
        private string _commname;

        public string ReleaseGroupBuyPriceList
        {
            set { _releasetpricelist = value; }
            get { return _releasetpricelist; }
        }

        public string ReleaseDisCountPriceList
        {
            set { _releasedpricelist = value; }
            get { return _releasedpricelist; }
        }

        public string ReleaseSalePriceList
        {
            set { _releasespricelist = value; }
            get { return _releasespricelist; }
        }


        /// <summary>
        /// 
        /// </summary>
        public decimal? GroupBuyPrice
        {
            set { _groupbuyprice = value; }
            get { return _groupbuyprice; }
        }

        /// <summary>
        /// 
        /// </summary>
        public decimal? ResourcesDisCountPrice
        {
            set { _resourcesdiscountprice = value; }
            get { return _resourcesdiscountprice; }
        }

        /// <summary>
        /// 
        /// </summary>
        public decimal? ResourcesSalePrice
        {
            set { _resourcessaleprice = value; }
            get { return _resourcessaleprice; }
        }

        public string CommName
        {
            set { _commname = value; }
            get { return _commname; }
        }
        public string RegistWareIDCount
        {
            set { _registwareidcount = value; }
            get { return _registwareidcount; }
        }
        public string ReleaseCountList
        {
            set { _releasecountlist = value; }
            get { return _releasecountlist; }
        }
        public string ReleaseIDList
        {
            set { _releaseidlist = value; }
            get { return _releaseidlist; }
        }
		/// <summary>
		/// 
		/// </summary>
		public string RegistID
		{
			set{ _registid=value;}
			get{return _registid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RegistNumber
		{
			set{ _registnumber=value;}
			get{return _registnumber;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CustomerType
		{
			set{ _customertype=value;}
			get{return _customertype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? CommID
		{
			set{ _commid=value;}
			get{return _commid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CustName
		{
			set{ _custname=value;}
			get{return _custname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long CustID
		{
			set{ _custid=value;}
			get{return _custid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string MobilePhone
		{
			set{ _mobilephone=value;}
			get{return _mobilephone;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Address
		{
			set{ _address=value;}
			get{return _address;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? DocumentDate
		{
			set{ _documentdate=value;}
			get{return _documentdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RegistMessage
		{
			set{ _registmessage=value;}
			get{return _registmessage;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ProcessingStatus
		{
			set{ _processingstatus=value;}
			get{return _processingstatus;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ProcessingExplain
		{
			set{ _processingexplain=value;}
			get{return _processingexplain;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long ReleaseID
		{
			set{ _releaseid=value;}
			get{return _releaseid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ScheduleType
		{
			set{ _scheduletype=value;}
			get{return _scheduletype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? ReleaseServiceComDate
		{
			set{ _releaseservicecomdate=value;}
			get{return _releaseservicecomdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ReleaseServiceComtime
		{
			set{ _releaseservicecomtime=value;}
			get{return _releaseservicecomtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? ReleaseStartTime
		{
			set{ _releasestarttime=value;}
			get{return _releasestarttime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? ReleaseEndTime
		{
			set{ _releaseendtime=value;}
			get{return _releaseendtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ReleasePeriodOfStarttime
		{
			set{ _releaseperiodofstarttime=value;}
			get{return _releaseperiodofstarttime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ReleasePeriodOfEndtime
		{
			set{ _releaseperiodofendtime=value;}
			get{return _releaseperiodofendtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UserCode
		{
			set{ _usercode=value;}
			get{return _usercode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? IssueDate
		{
			set{ _issuedate=value;}
			get{return _issuedate;}
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
		public int? IsDelete
		{
			set{ _isdelete=value;}
			get{return _isdelete;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? SumCount
		{
			set{ _sumcount=value;}
			get{return _sumcount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? SumPrice
		{
			set{ _sumprice=value;}
			get{return _sumprice;}
		}
		#endregion Model

	}
}

