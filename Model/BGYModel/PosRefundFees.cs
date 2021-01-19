using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   public class PosRefundFees
    {
        private string _tradeID;

        private string _chargeMode;

        private string _paidAmount;

        private string _tradeDate;

        private string _tradeNumber;

        private string _accountNumber;

        private string _refNumber;

        private string _feesID;

        private string _costName;

        private string _feesAmount;

        private string _roomName;

        private string _roomSign;

        private string _roomID;

        public string RoomID
        {
            get
            {
                return _roomID;
            }

            set
            {
                _roomID = value;
            }
        }
        public string RoomName
        {
            get
            {
                return _roomName;
            }

            set
            {
                _roomName = value;
            }
        }

        public string RoomSign
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

        public string TradeID
        {
            get
            {
                return _tradeID;
            }

            set
            {
                _tradeID = value;
            }
        }

        public string PaidAmount
        {
            get
            {
                return _paidAmount;
            }

            set
            {
                _paidAmount = value;
            }
        }

        public string TradeDate
        {
            get
            {
                return _tradeDate;
            }

            set
            {
                _tradeDate = value;
            }
        }

        public string TradeNumber
        {
            get
            {
                return _tradeNumber;
            }

            set
            {
                _tradeNumber = value;
            }
        }

        public string AccountNumber
        {
            get
            {
                return _accountNumber;
            }

            set
            {
                _accountNumber = value;
            }
        }

        public string RefNumber
        {
            get
            {
                return _refNumber;
            }

            set
            {
                _refNumber = value;
            }
        }

        public string FeesID
        {
            get
            {
                return _feesID;
            }

            set
            {
                _feesID = value;
            }
        }

        public string CostName
        {
            get
            {
                return _costName;
            }

            set
            {
                _costName = value;
            }
        }

        public string FeesAmount
        {
            get
            {
                return _feesAmount;
            }

            set
            {
                _feesAmount = value;
            }
        }

        public string ChargeMode
        {
            get
            {
                return _chargeMode;
            }

            set
            {
                _chargeMode = value;
            }
        }

    }
}
