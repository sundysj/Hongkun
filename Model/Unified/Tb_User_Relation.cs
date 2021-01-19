using System;
namespace MobileSoft.Model.Unified
{
    /// <summary>
    /// ʵ����Tb_User_Relation ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
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
        /// ��ʱ��
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
        /// �û�����
        /// </summary>
        public string UserId
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// С��ID
        /// </summary>
        public string CommunityId
        {
            set { _communityid = value; }
            get { return _communityid; }
        }
        /// <summary>
        /// ҵ��ID
        /// </summary>
        public string CustId
        {
            set { _custid = value; }
            get { return _custid; }
        }
        /// <summary>
        /// ��ͥ��ԱID
        /// </summary>
        public string CustHoldId
        {
            set { _custholdid = value; }
            get { return _custholdid; }
        }
        /// <summary>
        /// ����ID
        /// </summary>
        public string RoomId
        {
            set { _roomid = value; }
            get { return _roomid; }
        }
        /// <summary>
        /// ��λID
        /// </summary>
        public string ParkingId
        {
            set { _parkingid = value; }
            get { return _parkingid; }
        }
        /// <summary>
        /// �Ƿ�Ĭ��С��
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

