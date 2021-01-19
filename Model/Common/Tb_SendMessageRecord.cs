using System;
namespace MobileSoft.Model.Common
{
    /// <summary>
    /// 实体类Tb_SendMessageRecord 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class Tb_SendMessageRecord
    {
        public Tb_SendMessageRecord()
        { }
        #region Model
        private string _id;
        private string _mobile;
        private string _sendcontent;
        private DateTime? _sendtime;
        private string _maccode;
        private string _sendtype;
        private string _sendstate;
        /// <summary>
        /// 发送状态
        /// </summary>
        public string SendState
        {
            set { _sendstate = value; }
            get { return _sendstate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile
        {
            set { _mobile = value; }
            get { return _mobile; }
        }
        /// <summary>
        /// 发送内容
        /// </summary>
        public string SendContent
        {
            set { _sendcontent = value; }
            get { return _sendcontent; }
        }
        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime? SendTime
        {
            set { _sendtime = value; }
            get { return _sendtime; }
        }
        /// <summary>
        /// mac编码
        /// </summary>
        public string MacCode
        {
            set { _maccode = value; }
            get { return _maccode; }
        }
        /// <summary>
        /// 发送类型
        /// </summary>
        public string SendType
        {
            set { _sendtype = value; }
            get { return _sendtype; }
        }
        #endregion Model

    }
}

