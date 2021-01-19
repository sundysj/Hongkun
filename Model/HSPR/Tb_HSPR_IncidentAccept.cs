using System;
namespace MobileSoft.Model.HSPR
{
	/// <summary>
	/// 实体类Tb_HSPR_IncidentAccept 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_HSPR_IncidentAccept
	{
		public Tb_HSPR_IncidentAccept()
		{}
        #region Model
        private long _incidentid;
        private int _commid;
        private long _custid;
        private long _roomid;
        private string _typeid;
        private string _incidentnum;
        private string _incidentplace;
        private string _incidentman;
        private DateTime? _incidentdate;
        private string _incidentmode;
        private int? _deallimit;
        private string _incidentcontent;
        private DateTime? _reservedate;
        private int? _reservelimit;
        private string _phone;
        private string _admiman;
        private DateTime? _admidate;
        private int? _disptype;
        private string _dispman;
        private DateTime? _dispdate;
        private string _dealman;
        private string _coordinatenum;
        private DateTime? _enddate;
        private DateTime? _mainstartdate;
        private DateTime? _mainenddate;
        private string _dealsituation;
        private string _custcomments;
        private string _servicequality;
        private string _articlesfacilities;
        private int? _dealstate;
        private string _incidentmemo;
        private int? _isdelete;
        private string _reasons;
        private long _regionalid;
        private string _deletereasons;
        private DateTime? _deletedate;
        private string _typecode;
        private string _signatory;
        private int? _isstatistics;
        private string _finishuser;
        private decimal? _dueamount;
        private int? _istell;
        private long _deviceid;
        private DateTime? _printtime;
        private int? _printcount;
        private string _printusername;
        private int? _isreceipt;
        private string _receiptusername;
        private long _locationid;
        private string _incidentimgs;
        private DateTime? _signdate;
        private string _callincidenttype;
        private string _eid;
        private string _dispusercode;
        private string _taskeqid;
        private string _processincidentimgs;
        private int? _emergencydegree;
        private int? _isrework;
        private DateTime? _reworkdate;
        private string _reworkcreater;
        private string _reworkcontent;
        private DateTime? _receivingdate;
        private DateTime? _arrivedata;
        private string _replyLimit;
        private string _dispLimit;

        /// <summary>
        /// 
        /// </summary>
        public long IncidentID
        {
            set { _incidentid = value; }
            get { return _incidentid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int CommID
        {
            set { _commid = value; }
            get { return _commid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long CustID
        {
            set { _custid = value; }
            get { return _custid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long RoomID
        {
            set { _roomid = value; }
            get { return _roomid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TypeID
        {
            set { _typeid = value; }
            get { return _typeid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string IncidentNum
        {
            set { _incidentnum = value; }
            get { return _incidentnum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string IncidentPlace
        {
            set { _incidentplace = value; }
            get { return _incidentplace; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string IncidentMan
        {
            set { _incidentman = value; }
            get { return _incidentman; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? IncidentDate
        {
            set { _incidentdate = value; }
            get { return _incidentdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string IncidentMode
        {
            set { _incidentmode = value; }
            get { return _incidentmode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? DealLimit
        {
            set { _deallimit = value; }
            get { return _deallimit; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string IncidentContent
        {
            set { _incidentcontent = value; }
            get { return _incidentcontent; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? ReserveDate
        {
            set { _reservedate = value; }
            get { return _reservedate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ReserveLimit
        {
            set { _reservelimit = value; }
            get { return _reservelimit; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Phone
        {
            set { _phone = value; }
            get { return _phone; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AdmiMan
        {
            set { _admiman = value; }
            get { return _admiman; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? AdmiDate
        {
            set { _admidate = value; }
            get { return _admidate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? DispType
        {
            set { _disptype = value; }
            get { return _disptype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DispMan
        {
            set { _dispman = value; }
            get { return _dispman; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? DispDate
        {
            set { _dispdate = value; }
            get { return _dispdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DealMan
        {
            set { _dealman = value; }
            get { return _dealman; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CoordinateNum
        {
            set { _coordinatenum = value; }
            get { return _coordinatenum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? EndDate
        {
            set { _enddate = value; }
            get { return _enddate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? MainStartDate
        {
            set { _mainstartdate = value; }
            get { return _mainstartdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? MainEndDate
        {
            set { _mainenddate = value; }
            get { return _mainenddate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DealSituation
        {
            set { _dealsituation = value; }
            get { return _dealsituation; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CustComments
        {
            set { _custcomments = value; }
            get { return _custcomments; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ServiceQuality
        {
            set { _servicequality = value; }
            get { return _servicequality; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ArticlesFacilities
        {
            set { _articlesfacilities = value; }
            get { return _articlesfacilities; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? DealState
        {
            set { _dealstate = value; }
            get { return _dealstate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string IncidentMemo
        {
            set { _incidentmemo = value; }
            get { return _incidentmemo; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? IsDelete
        {
            set { _isdelete = value; }
            get { return _isdelete; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Reasons
        {
            set { _reasons = value; }
            get { return _reasons; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long RegionalID
        {
            set { _regionalid = value; }
            get { return _regionalid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DeleteReasons
        {
            set { _deletereasons = value; }
            get { return _deletereasons; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? DeleteDate
        {
            set { _deletedate = value; }
            get { return _deletedate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TypeCode
        {
            set { _typecode = value; }
            get { return _typecode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Signatory
        {
            set { _signatory = value; }
            get { return _signatory; }
        }

        public string SignatoryImg { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? IsStatistics
        {
            set { _isstatistics = value; }
            get { return _isstatistics; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FinishUser
        {
            set { _finishuser = value; }
            get { return _finishuser; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? DueAmount
        {
            set { _dueamount = value; }
            get { return _dueamount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? IsTell
        {
            set { _istell = value; }
            get { return _istell; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long DeviceID
        {
            set { _deviceid = value; }
            get { return _deviceid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? PrintTime
        {
            set { _printtime = value; }
            get { return _printtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? PrintCount
        {
            set { _printcount = value; }
            get { return _printcount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PrintUserName
        {
            set { _printusername = value; }
            get { return _printusername; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? IsReceipt
        {
            set { _isreceipt = value; }
            get { return _isreceipt; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ReceiptUserName
        {
            set { _receiptusername = value; }
            get { return _receiptusername; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long LocationID
        {
            set { _locationid = value; }
            get { return _locationid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string IncidentImgs
        {
            set { _incidentimgs = value; }
            get { return _incidentimgs; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? SignDate
        {
            set { _signdate = value; }
            get { return _signdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CallIncidentType
        {
            set { _callincidenttype = value; }
            get { return _callincidenttype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string EID
        {
            set { _eid = value; }
            get { return _eid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DispUserCode
        {
            set { _dispusercode = value; }
            get { return _dispusercode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TaskEqId
        {
            set { _taskeqid = value; }
            get { return _taskeqid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ProcessIncidentImgs
        {
            set { _processincidentimgs = value; }
            get { return _processincidentimgs; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? EmergencyDegree
        {
            set { _emergencydegree = value; }
            get { return _emergencydegree; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? IsReWork
        {
            set { _isrework = value; }
            get { return _isrework; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? ReWorkDate
        {
            set { _reworkdate = value; }
            get { return _reworkdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ReWorkCreater
        {
            set { _reworkcreater = value; }
            get { return _reworkcreater; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ReWorkContent
        {
            set { _reworkcontent = value; }
            get { return _reworkcontent; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? ReceivingDate
        {
            set { _receivingdate = value; }
            get { return _receivingdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? ArriveData
        {
            set { _arrivedata = value; }
            get { return _arrivedata; }
        }


        public string ReplyLimit
        {
            set { _replyLimit = value; }
            get { return _replyLimit; }
        }
        public string DispLimit
        {
            set { _dispLimit = value; }
            get { return _dispLimit; }
        }
        #endregion Model

    }
}

