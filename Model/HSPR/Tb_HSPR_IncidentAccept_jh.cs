using System;
using KernelDev.DataAccess;
using System.Runtime.Serialization;

namespace MobileSoft.Model.HSPR
{
    [Serializable()]
    public class Tb_HSPR_IncidentAccept_jh : Tb_HSPR_IncidentAccept
    {

        public int DrClass
        {
            get;
            set;
        }
        

        public string DealManCode { get; set; }

        public string EqId { get; set; }

        public string Warranty
        {
            get;
            set;
        }
        

        public string DeleteUserCode
        {
            get;
            set;
        }

        public string KPToSMReasons
        {
            get;
            set;
        }

        public string KPToSMTime
        {
            get;
            set;
        }

        public string KPToSMUserCode
        {
            get;
            set;
        }

        public int IsReply
        {
            get;
            set;
        }

        public string ReplyResult
        {
            get;
            set;
        }

        public string ReplyeValuate
        {
            get;
            set;
        }

        public int IsClose
        {
            get;
            set;
        }

        public string CloseSituation
        {
            get;
            set;
        }

        public int CloseType
        {
            get;
            set;
        }

        public string CloseTime
        {
            get;
            set;
        }

        public string CloseUserCode
        {
            get;
            set;
        }

        public string NoNormalCloseReasons
        {
            get;
            set;
        }

        public int IsTouSu
        {
            get;
            set;
        }

        public string Duty
        {
            get;
            set;
        }

        public string IncidentSource
        {
            get;
            set;
        }

        public Int64 BigCorpTypeID
        {
            get;
            set;
        }

        public string BigCorpTypeCode
        {
            get;
            set;
        }

        public Int64 FineCorpTypeID
        {
            get;
            set;
        }

        public string FineCorpTypeCode
        {
            get;
            set;
        }

        public string RatedWorkHour
        {
            get;
            set;
        }

        public string RatedWorkHourNumber
        {
            get;
            set;
        }

        public string KPIRatio
        {
            get;
            set;
        }

        public string DealResult
        {
            get;
            set;
        }

        public string OverdueReason
        {
            get;
            set;
        }

        public int IsFee
        {
            get;
            set;
        }

        public int IsFinish
        {
            get;
            set;
        }

        public string FinishDate
        {
            get;
            set;
        }

        public string FinishUserCode
        {
            get;
            set;
        }

        public string SQLEx
        {
            get;
            set;
        }
    }
}
