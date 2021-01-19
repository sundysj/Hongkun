using System;
namespace Erp.Model.System
{
    /// <summary>
    /// 实体类Tb_System_ErrorMessage 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class Tb_System_ErrorMessage
    {
        public Tb_System_ErrorMessage()
        { }
        #region Model
        private long _fdi_errorid;
        private long _fdi_corpid;
        private string _fdv_oprusername;
        private string _fdv_errorpage;
        private string _fdv_errormessage;
        private DateTime? _fdd_errordate;
        /// <summary>
        /// 
        /// </summary>
        public long fdi_ErrorId
        {
            set { _fdi_errorid = value; }
            get { return _fdi_errorid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long fdi_CorpId
        {
            set { _fdi_corpid = value; }
            get { return _fdi_corpid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string fdv_OprUserName
        {
            set { _fdv_oprusername = value; }
            get { return _fdv_oprusername; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string fdv_ErrorPage
        {
            set { _fdv_errorpage = value; }
            get { return _fdv_errorpage; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string fdv_ErrorMessage
        {
            set { _fdv_errormessage = value; }
            get { return _fdv_errormessage; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? fdd_ErrorDate
        {
            set { _fdd_errordate = value; }
            get { return _fdd_errordate; }
        }
        #endregion Model

    }
}

