using System;

namespace Business.PMS10.业主App.缴费.Model
{
    /// <summary>
    /// Tb_HSPR_IncidentControlSet的模型
    /// </summary>
    [Serializable()]
    public class Tb_HSPR_PaymentBind
    {

        private string iID;
        private int commID;
        private long corpCostID;
        private string isPrec;
        private int isdelete;
        private DateTime? createDate;
        private DateTime? operationTime;
        private byte[] timeStamp;
        private long costid;

        public Tb_HSPR_PaymentBind() { }


        /// <summary>
        /// 
        /// </summary>
        public string IID
        {
            get { return this.iID; }
            set { this.iID = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int CommID
        {
            get { return this.commID; }
            set { this.commID = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public long CorpCostID
        {
            get { return this.corpCostID; }
            set { this.corpCostID = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string IsPrec
        {
            get { return this.isPrec; }
            set { this.isPrec = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Isdelete
        {
            get { return this.isdelete; }
            set { this.isdelete = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreateDate
        {
            get { return this.createDate; }
            set { this.createDate = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? OperationTime
        {
            get { return this.operationTime; }
            set { this.operationTime = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public byte[] TimeStamp
        {
            get { return this.timeStamp; }
            set { this.timeStamp = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public long Costid
        {
            get { return this.costid; }
            set { this.costid = value; }
        }

    }
}
