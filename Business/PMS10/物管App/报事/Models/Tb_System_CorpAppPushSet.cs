using System;

namespace Business.PMS10.物管App.报事.Models
{
    /// <summary>
    /// Tb_HSPR_IncidentControlSet的模型
    /// </summary>
    [Serializable()]
    public class Tb_System_CorpAppPushSet
    {
        private Guid iID;
        private int corpID;
        private short isAppPushMsg;
        private string appPushPackage;
        private string custAppKey;
        private string custAppSecret;
        private string propertyAppKey;
        private string propertyAppSecret;

        public Tb_System_CorpAppPushSet() { }

        public Guid IID
        {
            get { return this.iID; }
            set { this.iID = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int CorpID
        {
            get { return this.corpID; }
            set { this.corpID = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public short IsAppPushMsg
        {
            get { return this.isAppPushMsg; }
            set { this.isAppPushMsg = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string AppPushPackage
        {
            get { return this.appPushPackage; }
            set { this.appPushPackage = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string CustAppKey
        {
            get { return this.custAppKey; }
            set { this.custAppKey = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string CustAppSecret
        {
            get { return this.custAppSecret; }
            set { this.custAppSecret = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string PropertyAppKey
        {
            get { return this.propertyAppKey; }
            set { this.propertyAppKey = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string PropertyAppSecret
        {
            get { return this.propertyAppSecret; }
            set { this.propertyAppSecret = value; }
        }

    }
}
