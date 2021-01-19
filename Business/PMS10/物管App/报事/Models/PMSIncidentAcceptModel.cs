using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.PMS10.物管App.报事.Models
{
    [Serializable()]
    public class PMSIncidentAcceptModel
    {
        private long incidentID;
        private int commID;
        private long custID;
        private long roomID;
        private int drClass;
        private string typeID;
        private string incidentNum;
        private string incidentPlace;
        private string incidentMan;
        private DateTime? incidentDate;
        private string incidentMode;
        private string incidentContent;
        private DateTime? reserveDate;
        private int reserveLimit;
        private string phone;
        private string admiMan;
        private DateTime? admiDate;
        private string coordinateNum;
        private short dispType;
        private string dispMan;
        private string dispUserCode;
        private decimal dispLimit;
        private DateTime? dispDate;
        private string dealManCode;
        private string dealMan;
        private int dealLimit;
        private string dealSituation;
        private short dealState;
        private DateTime? endDate;
        private DateTime? mainStartDate;
        private DateTime? mainEndDate;
        private string custComments;
        private string articlesFacilities;
        private string incidentMemo;
        private short isDelete;
        private string reasons;
        private long regionalID;
        private string deleteReasons;
        private DateTime? deleteDate;
        private string typeCode;
        private string signatory;
        private short isStatistics;
        private string finishUser;
        private decimal dueAmount;
        private short isTell;
        private long deviceID;
        private DateTime? printTime;
        private int printCount;
        private string printUserName;
        private int isReceipt;
        private string receiptUserName;
        private long locationID;
        private string incidentImgs;
        private DateTime? signDate;
        private string callIncidentType;
        private string eID;
        private string taskEqId;
        private string processIncidentImgs;
        private int emergencyDegree;
        private int isReWork;
        private DateTime? reWorkDate;
        private string reWorkCreater;
        private string reWorkContent;
        private DateTime? receivingDate;
        private DateTime? arriveData;
        private string warranty;
        private string signatoryImg;
        private string deleteUserCode;
        private string kPToSMReasons;
        private DateTime? kPToSMTime;
        private string kPToSMUserCode;
        private int isClose;
        private string closeSituation;
        private int closeType;
        private DateTime? closeTime;
        private string closeUser;
        private string closeUserCode;
        private string noNormalCloseReasons;
        private int isTouSu;
        private string duty;
        private string incidentSource;
        private long bigCorpTypeID;
        private string bigCorpTypeCode;
        private long fineCorpTypeID;
        private string fineCorpTypeCode;
        private decimal ratedWorkHour;
        private decimal ratedWorkHourNumber;
        private decimal kPIRatio;
        private string dealResult;
        private string overdueReason;
        private int isFee;
        private int isFinish;
        private DateTime? finishDate;
        private string finishUserCode;
        private string eqID;
        private byte[] timeStamp;
        private string deleteMan;
        private string deleteManCode;
        private string callLoginNum;
        private string callSeatNO;
        private long incidentCustId;
        private int replyLimit;
        private string serviceQuality;
        private string signatoryConfirmImg;
        private int isSupplementary;
        private DateTime? signatoryImgDate;
        private DateTime? signatoryConfirmImgDate;
        private string dispRemarks;
        private string cMRProblemGUID;
        private string cMRIsConsistent;
        private string localeFunction;
        private string localePosition;
        private string admiManUserCode;
        private string dispWay;
        private string dealWay;
        private bool isSnatch;

        public PMSIncidentAcceptModel() { }

        public long IncidentID
        {
            get { return this.incidentID; }
            set { this.incidentID = value; }
        }

        public int CommID
        {
            get { return this.commID; }
            set { this.commID = value; }
        }

        public long CustID
        {
            get { return this.custID; }
            set { this.custID = value; }
        }

        public long RoomID
        {
            get { return this.roomID; }
            set { this.roomID = value; }
        }

        public int DrClass
        {
            get { return this.drClass; }
            set { this.drClass = value; }
        }

        public string TypeID
        {
            get { return this.typeID; }
            set { this.typeID = value; }
        }

        public string IncidentNum
        {
            get { return this.incidentNum; }
            set { this.incidentNum = value; }
        }

        public string IncidentPlace
        {
            get { return this.incidentPlace; }
            set { this.incidentPlace = value; }
        }

        public string IncidentMan
        {
            get { return this.incidentMan; }
            set { this.incidentMan = value; }
        }

        public DateTime? IncidentDate
        {
            get { return this.incidentDate; }
            set { this.incidentDate = value; }
        }

        public string IncidentMode
        {
            get { return this.incidentMode; }
            set { this.incidentMode = value; }
        }

        public string IncidentContent
        {
            get { return this.incidentContent; }
            set { this.incidentContent = value; }
        }

        public DateTime? ReserveDate
        {
            get { return this.reserveDate; }
            set { this.reserveDate = value; }
        }

        public int ReserveLimit
        {
            get { return this.reserveLimit; }
            set { this.reserveLimit = value; }
        }

        public string Phone
        {
            get { return this.phone; }
            set { this.phone = value; }
        }

        public string AdmiMan
        {
            get { return this.admiMan; }
            set { this.admiMan = value; }
        }

        public DateTime? AdmiDate
        {
            get { return this.admiDate; }
            set { this.admiDate = value; }
        }

        public string CoordinateNum
        {
            get { return this.coordinateNum; }
            set { this.coordinateNum = value; }
        }

        public short DispType
        {
            get { return this.dispType; }
            set { this.dispType = value; }
        }

        public string DispMan
        {
            get { return this.dispMan; }
            set { this.dispMan = value; }
        }

        public string DispUserCode
        {
            get { return this.dispUserCode; }
            set { this.dispUserCode = value; }
        }

        public decimal DispLimit
        {
            get { return this.dispLimit; }
            set { this.dispLimit = value; }
        }

        public DateTime? DispDate
        {
            get { return this.dispDate; }
            set { this.dispDate = value; }
        }

        public string DealManCode
        {
            get { return this.dealManCode; }
            set { this.dealManCode = value; }
        }

        public string DealMan
        {
            get { return this.dealMan; }
            set { this.dealMan = value; }
        }

        public int DealLimit
        {
            get { return this.dealLimit; }
            set { this.dealLimit = value; }
        }

        public string DealSituation
        {
            get { return this.dealSituation; }
            set { this.dealSituation = value; }
        }

        public short DealState
        {
            get { return this.dealState; }
            set { this.dealState = value; }
        }

        public DateTime? EndDate
        {
            get { return this.endDate; }
            set { this.endDate = value; }
        }

        public DateTime? MainStartDate
        {
            get { return this.mainStartDate; }
            set { this.mainStartDate = value; }
        }

        public DateTime? MainEndDate
        {
            get { return this.mainEndDate; }
            set { this.mainEndDate = value; }
        }

        public string CustComments
        {
            get { return this.custComments; }
            set { this.custComments = value; }
        }

        public string ArticlesFacilities
        {
            get { return this.articlesFacilities; }
            set { this.articlesFacilities = value; }
        }

        public string IncidentMemo
        {
            get { return this.incidentMemo; }
            set { this.incidentMemo = value; }
        }

        public short IsDelete
        {
            get { return this.isDelete; }
            set { this.isDelete = value; }
        }

        public string Reasons
        {
            get { return this.reasons; }
            set { this.reasons = value; }
        }

        public long RegionalID
        {
            get { return this.regionalID; }
            set { this.regionalID = value; }
        }

        public string DeleteReasons
        {
            get { return this.deleteReasons; }
            set { this.deleteReasons = value; }
        }

        public DateTime? DeleteDate
        {
            get { return this.deleteDate; }
            set { this.deleteDate = value; }
        }

        public string TypeCode
        {
            get { return this.typeCode; }
            set { this.typeCode = value; }
        }

        public string Signatory
        {
            get { return this.signatory; }
            set { this.signatory = value; }
        }

        public short IsStatistics
        {
            get { return this.isStatistics; }
            set { this.isStatistics = value; }
        }

        public string FinishUser
        {
            get { return this.finishUser; }
            set { this.finishUser = value; }
        }

        public decimal DueAmount
        {
            get { return this.dueAmount; }
            set { this.dueAmount = value; }
        }

        public short IsTell
        {
            get { return this.isTell; }
            set { this.isTell = value; }
        }

        public long DeviceID
        {
            get { return this.deviceID; }
            set { this.deviceID = value; }
        }

        public DateTime? PrintTime
        {
            get { return this.printTime; }
            set { this.printTime = value; }
        }

        public int PrintCount
        {
            get { return this.printCount; }
            set { this.printCount = value; }
        }

        public string PrintUserName
        {
            get { return this.printUserName; }
            set { this.printUserName = value; }
        }

        public int IsReceipt
        {
            get { return this.isReceipt; }
            set { this.isReceipt = value; }
        }

        public string ReceiptUserName
        {
            get { return this.receiptUserName; }
            set { this.receiptUserName = value; }
        }

        public long LocationID
        {
            get { return this.locationID; }
            set { this.locationID = value; }
        }

        public string IncidentImgs
        {
            get { return this.incidentImgs; }
            set { this.incidentImgs = value; }
        }

        public DateTime? SignDate
        {
            get { return this.signDate; }
            set { this.signDate = value; }
        }

        public string CallIncidentType
        {
            get { return this.callIncidentType; }
            set { this.callIncidentType = value; }
        }

        public string EID
        {
            get { return this.eID; }
            set { this.eID = value; }
        }

        public string TaskEqId
        {
            get { return this.taskEqId; }
            set { this.taskEqId = value; }
        }

        public string ProcessIncidentImgs
        {
            get { return this.processIncidentImgs; }
            set { this.processIncidentImgs = value; }
        }

        public int EmergencyDegree
        {
            get { return this.emergencyDegree; }
            set { this.emergencyDegree = value; }
        }

        public int IsReWork
        {
            get { return this.isReWork; }
            set { this.isReWork = value; }
        }

        public DateTime? ReWorkDate
        {
            get { return this.reWorkDate; }
            set { this.reWorkDate = value; }
        }

        public string ReWorkCreater
        {
            get { return this.reWorkCreater; }
            set { this.reWorkCreater = value; }
        }

        public string ReWorkContent
        {
            get { return this.reWorkContent; }
            set { this.reWorkContent = value; }
        }

        public DateTime? ReceivingDate
        {
            get { return this.receivingDate; }
            set { this.receivingDate = value; }
        }

        public DateTime? ArriveData
        {
            get { return this.arriveData; }
            set { this.arriveData = value; }
        }

        public string Warranty
        {
            get { return this.warranty; }
            set { this.warranty = value; }
        }

        public string SignatoryImg
        {
            get { return this.signatoryImg; }
            set { this.signatoryImg = value; }
        }

        public string DeleteUserCode
        {
            get { return this.deleteUserCode; }
            set { this.deleteUserCode = value; }
        }

        public string KPToSMReasons
        {
            get { return this.kPToSMReasons; }
            set { this.kPToSMReasons = value; }
        }

        public DateTime? KPToSMTime
        {
            get { return this.kPToSMTime; }
            set { this.kPToSMTime = value; }
        }

        public string KPToSMUserCode
        {
            get { return this.kPToSMUserCode; }
            set { this.kPToSMUserCode = value; }
        }

        public int IsClose
        {
            get { return this.isClose; }
            set { this.isClose = value; }
        }

        public string CloseSituation
        {
            get { return this.closeSituation; }
            set { this.closeSituation = value; }
        }

        public int CloseType
        {
            get { return this.closeType; }
            set { this.closeType = value; }
        }

        public DateTime? CloseTime
        {
            get { return this.closeTime; }
            set { this.closeTime = value; }
        }

        public string CloseUser
        {
            get { return this.closeUser; }
            set { this.closeUser = value; }
        }

        public string CloseUserCode
        {
            get { return this.closeUserCode; }
            set { this.closeUserCode = value; }
        }

        public string NoNormalCloseReasons
        {
            get { return this.noNormalCloseReasons; }
            set { this.noNormalCloseReasons = value; }
        }

        public int IsTouSu
        {
            get { return this.isTouSu; }
            set { this.isTouSu = value; }
        }

        public string Duty
        {
            get { return this.duty; }
            set { this.duty = value; }
        }

        public string IncidentSource
        {
            get { return this.incidentSource; }
            set { this.incidentSource = value; }
        }

        public long BigCorpTypeID
        {
            get { return this.bigCorpTypeID; }
            set { this.bigCorpTypeID = value; }
        }

        public string BigCorpTypeCode
        {
            get { return this.bigCorpTypeCode; }
            set { this.bigCorpTypeCode = value; }
        }

        public long FineCorpTypeID
        {
            get { return this.fineCorpTypeID; }
            set { this.fineCorpTypeID = value; }
        }

        public string FineCorpTypeCode
        {
            get { return this.fineCorpTypeCode; }
            set { this.fineCorpTypeCode = value; }
        }

        public decimal RatedWorkHour
        {
            get { return this.ratedWorkHour; }
            set { this.ratedWorkHour = value; }
        }

        public decimal RatedWorkHourNumber
        {
            get { return this.ratedWorkHourNumber; }
            set { this.ratedWorkHourNumber = value; }
        }

        public decimal KPIRatio
        {
            get { return this.kPIRatio; }
            set { this.kPIRatio = value; }
        }

        public string DealResult
        {
            get { return this.dealResult; }
            set { this.dealResult = value; }
        }

        public string OverdueReason
        {
            get { return this.overdueReason; }
            set { this.overdueReason = value; }
        }

        public int IsFee
        {
            get { return this.isFee; }
            set { this.isFee = value; }
        }

        public int IsFinish
        {
            get { return this.isFinish; }
            set { this.isFinish = value; }
        }

        public DateTime? FinishDate
        {
            get { return this.finishDate; }
            set { this.finishDate = value; }
        }

        public string FinishUserCode
        {
            get { return this.finishUserCode; }
            set { this.finishUserCode = value; }
        }

        public string EqID
        {
            get { return this.eqID; }
            set { this.eqID = value; }
        }

        public byte[] TimeStamp
        {
            get { return this.timeStamp; }
            set { this.timeStamp = value; }
        }

        public string DeleteMan
        {
            get { return this.deleteMan; }
            set { this.deleteMan = value; }
        }

        public string DeleteManCode
        {
            get { return this.deleteManCode; }
            set { this.deleteManCode = value; }
        }

        public string CallLoginNum
        {
            get { return this.callLoginNum; }
            set { this.callLoginNum = value; }
        }

        public string CallSeatNO
        {
            get { return this.callSeatNO; }
            set { this.callSeatNO = value; }
        }

        public long IncidentCustId
        {
            get { return this.incidentCustId; }
            set { this.incidentCustId = value; }
        }

        public int ReplyLimit
        {
            get { return this.replyLimit; }
            set { this.replyLimit = value; }
        }

        public string ServiceQuality
        {
            get { return this.serviceQuality; }
            set { this.serviceQuality = value; }
        }

        public string SignatoryConfirmImg
        {
            get { return this.signatoryConfirmImg; }
            set { this.signatoryConfirmImg = value; }
        }

        public int IsSupplementary
        {
            get { return this.isSupplementary; }
            set { this.isSupplementary = value; }
        }

        public DateTime? SignatoryImgDate
        {
            get { return this.signatoryImgDate; }
            set { this.signatoryImgDate = value; }
        }

        public DateTime? SignatoryConfirmImgDate
        {
            get { return this.signatoryConfirmImgDate; }
            set { this.signatoryConfirmImgDate = value; }
        }

        public string DispRemarks
        {
            get { return this.dispRemarks; }
            set { this.dispRemarks = value; }
        }

        public string CMRProblemGUID
        {
            get { return this.cMRProblemGUID; }
            set { this.cMRProblemGUID = value; }
        }

        public string CMRIsConsistent
        {
            get { return this.cMRIsConsistent; }
            set { this.cMRIsConsistent = value; }
        }

        public string LocaleFunction
        {
            get { return this.localeFunction; }
            set { this.localeFunction = value; }
        }

        public string LocalePosition
        {
            get { return this.localePosition; }
            set { this.localePosition = value; }
        }

        public string AdmiManUserCode
        {
            get { return this.admiManUserCode; }
            set { this.admiManUserCode = value; }
        }

        public string DispWay
        {
            get { return this.dispWay; }
            set { this.dispWay = value; }
        }

        public string DealWay
        {
            get { return this.dealWay; }
            set { this.dealWay = value; }
        }

        public bool IsSnatch
        {
            get { return this.isSnatch; }
            set { this.isSnatch = value; }
        }

        public string CommName { get; set; }
        public string RoomName { get; set; }
        public string RegionalPlace { get; set; }
    }
}
