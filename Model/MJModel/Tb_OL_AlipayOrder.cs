using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.MJModel
{
   public class Tb_OL_AlipayOrder
    {
        #region Model
        private string _id;
        private int? _commid;
        private string _communityid;
        private long _custid;
        private string _partner;
        private string _out_trade_no;
        private string _prepay_str;
        private string _txntime;
        private string _trade_status;
        private string _trade_msg;
        private int? _issucc;
        private string _memo;
        private int? _isdelete;
        private long _roomId;
        private int? _isPrec;
        private string _billId;
        private string _billNO;
        /// <summary>
        /// 
        /// </summary>
        public string Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 小区ID
        /// </summary>
        public int? CommID
        {
            set { _commid = value; }
            get { return _commid; }
        }
        /// <summary>
        /// 统一数据小区ID
        /// </summary>
        public string CommunityId
        {
            set { _communityid = value; }
            get { return _communityid; }
        }
        /// <summary>
        /// 业主ID
        /// </summary>
        public long CustId
        {
            set { _custid = value; }
            get { return _custid; }
        }
        /// <summary>
        /// 合作伙伴ID号
        /// </summary>
        public string partner
        {
            set { _partner = value; }
            get { return _partner; }
        }
        /// <summary>
        /// 订单号
        /// </summary>
        public string out_trade_no
        {
            set { _out_trade_no = value; }
            get { return _out_trade_no; }
        }
        /// <summary>
        /// 订单请求串
        /// </summary>
        public string prepay_str
        {
            set { _prepay_str = value; }
            get { return _prepay_str; }
        }
        /// <summary>
        /// 交易时间
        /// </summary>
        public string txnTime
        {
            set { _txntime = value; }
            get { return _txntime; }
        }
        /// <summary>
        /// 交易状态
        /// </summary>
        public string trade_status
        {
            set { _trade_status = value; }
            get { return _trade_status; }
        }
        /// <summary>
        /// 状态说明
        /// </summary>
        public string trade_msg
        {
            set { _trade_msg = value; }
            get { return _trade_msg; }
        }
        /// <summary>
        /// 是否下账
        /// </summary>
        public int? IsSucc
        {
            set { _issucc = value; }
            get { return _issucc; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Memo
        {
            set { _memo = value; }
            get { return _memo; }
        }
        /// <summary>
        /// 是否撤销
        /// </summary>
        public int? IsDelete
        {
            set { _isdelete = value; }
            get { return _isdelete; }
        }
        /// <summary>
        /// 房屋编号
        /// </summary>
        public long RoomId
        {
            get
            {
                return _roomId;
            }

            set
            {
                _roomId = value;
            }
        }
        /// <summary>
        /// 是否预存
        /// </summary>
        public int? IsPrec
        {
            get
            {
                return _isPrec;
            }

            set
            {
                _isPrec = value;
            }
        }
        /// <summary>
        /// 收款单ID
        /// </summary>
        public string BillId
        {
            get
            {
                return _billId;
            }

            set
            {
                _billId = value;
            }
        }
        /// <summary>
        /// 收款单NO
        /// </summary>
        public string BillNO
        {
            get
            {
                return _billNO;
            }

            set
            {
                _billNO = value;
            }
        }
        #endregion Model
    }
}
