using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.HSPR
{
    /// <summary>
    /// 实体类Tb_HSPR_IncidentReply 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class Tb_HSPR_IncidentReply
    {
        public Tb_HSPR_IncidentReply()
        { }
        #region Model
        private long _iid;
        private int? _commid;
        private long _incidentid;
        private string _replytype;
        private string _respondentsman;
        private string _replyman;
        private DateTime? _replydate;
        private string _replycontent;
        private string _servicequality;
        private int? _isdelete;
        private string _replyway;
        private int? _replyresult;
        private string _replymemo;
        /// <summary>
        /// 
        /// </summary>
        public long IID
        {
            set { _iid = value; }
            get { return _iid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? CommID
        {
            set { _commid = value; }
            get { return _commid; }
        }
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
        public string ReplyType
        {
            set { _replytype = value; }
            get { return _replytype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RespondentsMan
        {
            set { _respondentsman = value; }
            get { return _respondentsman; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ReplyMan
        {
            set { _replyman = value; }
            get { return _replyman; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? ReplyDate
        {
            set { _replydate = value; }
            get { return _replydate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ReplyContent
        {
            set { _replycontent = value; }
            get { return _replycontent; }
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
        public int? IsDelete
        {
            set { _isdelete = value; }
            get { return _isdelete; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ReplyWay
        {
            set { _replyway = value; }
            get { return _replyway; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ReplyResult
        {
            set { _replyresult = value; }
            get { return _replyresult; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ReplyMemo
        {
            set { _replymemo = value; }
            get { return _replymemo; }
        }
        #endregion Model

    }
}

