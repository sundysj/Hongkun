using System;
namespace MobileSoft.Model.Unified
{
    /// <summary>
    /// 实体类Tb_Sms_Account 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class Tb_Sms_Account
    {
        public Tb_Sms_Account()
        { }
        #region Model
        private string _id;
        private string _smsaccount;
        private string _smspwd;
        private string _smsaddress;
        private string _sign;
        private int _smsuserid;
        /// <summary>
        /// 
        /// </summary>
        public string Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SmsAccount
        {
            set { _smsaccount = value; }
            get { return _smsaccount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SmsPwd
        {
            set { _smspwd = value; }
            get { return _smspwd; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SmsAddress
        {
            set { _smsaddress = value; }
            get { return _smsaddress; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Sign
        {
            set { _sign = value; }
            get { return _sign; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int SmsUserId
        {
            set { _smsuserid = value; }
            get { return _smsuserid; }
        }
        #endregion Model

    }
}

