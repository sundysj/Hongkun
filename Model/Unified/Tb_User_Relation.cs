using System;
namespace MobileSoft.Model.Unified
{
    /// <summary>
    /// 实体类Tb_User_Relation 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class Tb_User_Relation
    {
        public Tb_User_Relation()
        { }
        #region Model
        private string _id;
        private string _userid;
        private string _communityid;
        private string _custid;
        private string _custholdid;
        private string _roomid;
        private string _parkingid;
        private string _iscurr;
        private DateTime _regDate;
        private string _custName;
        private string _roomSign;
        private string _custmobile;



        /// <summary>
        /// 绑定时间
        /// </summary>
        public DateTime RegDate
        {
            set { _regDate = value; }
            get { return _regDate; }
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
        /// 用户编码
        /// </summary>
        public string UserId
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 小区ID
        /// </summary>
        public string CommunityId
        {
            set { _communityid = value; }
            get { return _communityid; }
        }
        /// <summary>
        /// 业主ID
        /// </summary>
        public string CustId
        {
            set { _custid = value; }
            get { return _custid; }
        }
        /// <summary>
        /// 家庭成员ID
        /// </summary>
        public string CustHoldId
        {
            set { _custholdid = value; }
            get { return _custholdid; }
        }
        /// <summary>
        /// 房屋ID
        /// </summary>
        public string RoomId
        {
            set { _roomid = value; }
            get { return _roomid; }
        }
        /// <summary>
        /// 车位ID
        /// </summary>
        public string ParkingId
        {
            set { _parkingid = value; }
            get { return _parkingid; }
        }
        /// <summary>
        /// 是否默认小区
        /// </summary>
        public string IsCurr
        {
            set { _iscurr = value; }
            get { return _iscurr; }
        }

        public String CustName
        {
            get
            {
                return _custName;
            }

            set
            {
                _custName = value;
            }
        }

        public String RoomSign
        {
            get
            {
                return _roomSign;
            }

            set
            {
                _roomSign = value;
            }
        }

        public String Custmobile
        {
            get
            {
                return _custmobile;
            }

            set
            {
                _custmobile = value;
            }
        }
        #endregion Model

    }
}

