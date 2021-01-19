using System;
namespace MobileSoft.Model.Unified
{
    /// <summary>
    /// 实体类Tb_Notice 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class Tb_Notice
    {
        public Tb_Notice()
        { }
        #region Model
        private string _id;
        private string _userid;
        private int? _noticetype;
        private string _title;
        private string _content;
        private DateTime? _issuedate;
        private string _communityid;
        private int? _isdelete;
        private string _imageurl;
        private string _contenturl;
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
        public string UserId
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? NoticeType
        {
            set { _noticetype = value; }
            get { return _noticetype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Content
        {
            set { _content = value; }
            get { return _content; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? IssueDate
        {
            set { _issuedate = value; }
            get { return _issuedate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CommunityId
        {
            set { _communityid = value; }
            get { return _communityid; }
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
        public string ImageURL
        {
            set { _imageurl = value; }
            get { return _imageurl; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ContentURL
        {
            set { _contenturl = value; }
            get { return _contenturl; }
        }
        #endregion Model

    }
}

