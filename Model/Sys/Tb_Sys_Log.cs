using System;
namespace MobileSoft.Model.Sys
{
    /// <summary>
    /// 实体类Tb_Sys_Department 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class Tb_Sys_Log
    {
        public Tb_Sys_Log()
        { }
       
        #region Model
        private Int32 _corpid;
        private Int32 _branchid;
        private Int32 _commid;
        private string _managercode;
        private string _locationip;
        private DateTime? _logtime;
        private string _pnodename;
        private string _operatename;
        private string _operateurl;
        private string _memo;
        /// <summary>
        /// 
        /// </summary>
        public Int32 CorpID
        {
            set { _corpid = value; }
            get { return _corpid; }
        }
        public Int32 BranchID
        {
            set { _branchid = value; }
            get { return _branchid; }
        }
        public Int32 CommID
        {
            set { _commid = value; }
            get { return _commid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ManagerCode
        {
            set { _managercode = value; }
            get { return _managercode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LocationIP
        {
            set { _locationip = value; }
            get { return _locationip; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? LogTime
        {
            set { _logtime = value; }
            get { return _logtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PNodeName
        {
            set { _pnodename = value; }
            get { return _pnodename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OperateName
        {
            set { _operatename = value; }
            get { return _operatename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OperateURL
        {
            set { _operateurl = value; }
            get { return _operateurl; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Memo
        {
            set { _memo = value; }
            get { return _memo; }
        }
        #endregion Model
    }
}
