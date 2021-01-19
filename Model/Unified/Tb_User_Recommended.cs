using System;
namespace MobileSoft.Model.Unified
{
    /// <summary>
    /// 实体类Tb_User_Recommended 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class Tb_User_Recommended
    {
        public Tb_User_Recommended()
        { }
        #region Model
        private string _id;
        private string _userid;
        private string _communityid;
        private string _recommendedcontent;
        private DateTime? _submittime;
        private string _replycontent;
        private string _replystate;
        private int? _evaluationlevel;
        private DateTime? _replytime;
        private string _replypeople;
        private int? _isdelete;
        /// <summary>
        /// ID
        /// </summary>
        public string Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 业主编号
        /// </summary>
        public string UserId
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 小区编号
        /// </summary>
        public string CommunityId
        {
            set { _communityid = value; }
            get { return _communityid; }
        }
        /// <summary>
        /// 建议内容
        /// </summary>
        public string RecommendedContent
        {
            set { _recommendedcontent = value; }
            get { return _recommendedcontent; }
        }
        /// <summary>
        /// 提交时间
        /// </summary>
        public string SubmitTime
        {
            set { _submittime =Convert.ToDateTime( value); }
            get { return _submittime.ToString(); }
        }
        /// <summary>
        /// 回复内容
        /// </summary>
        public string ReplyContent
        {
            set { _replycontent = value; }
            get { return _replycontent; }
        }
        /// <summary>
        /// 回复状态
        /// </summary>
        public string ReplyState
        {
            set { _replystate = value; }
            get { return _replystate; }
        }
        /// <summary>
        /// 评价等级
        /// </summary>
        public int? EvaluationLevel
        {
            set { _evaluationlevel = value; }
            get { return _evaluationlevel; }
        }
        /// <summary>
        /// 回复时间
        /// </summary>
        public DateTime? ReplyTime
        {
            set { _replytime = value; }
            get { return _replytime; }
        }
        /// <summary>
        /// 回复人
        /// </summary>
        public string ReplyPeople
        {
            set { _replypeople = value; }
            get { return _replypeople; }
        }
        /// <summary>
        /// 删除状态
        /// </summary>
        public int? IsDelete
        {
            set { _isdelete = value; }
            get { return _isdelete; }
        }
        #endregion Model

    }
}

