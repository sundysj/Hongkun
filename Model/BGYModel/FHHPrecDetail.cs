using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   public class FHHPrecDetail
    {
        private string _custID;

        private string _roomSign;

        private string _precDate;

        private string _costID;

        private string _costName;

        private string _recdID;

        private string _invoiceNo;

        private string _pdfUrl;

        private decimal _chargeAmount;

        private decimal _canBillingAmount;

        private string _eInvoiceState;

        private string _errMsg;

        private string _unableReason;

        public string CustID
        {
            get
            {
                return _custID;
            }

            set
            {
                _custID = value;
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

        public string PrecDate
        {
            get
            {
                return _precDate;
            }

            set
            {
                _precDate = value;
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

        public string CostID
        {
            get
            {
                return _costID;
            }

            set
            {
                _costID = value;
            }
        }

        public string RecdID
        {
            get
            {
                return _recdID;
            }

            set
            {
                _recdID = value;
            }
        }

        public string InvoiceNo
        {
            get
            {
                return _invoiceNo;
            }

            set
            {
                _invoiceNo = value;
            }
        }

        public string PdfUrl
        {
            get
            {
                return _pdfUrl;
            }

            set
            {
                _pdfUrl = value;
            }
        }

        public decimal ChargeAmount
        {
            get
            {
                return _chargeAmount;
            }

            set
            {
                _chargeAmount = value;
            }
        }

        public decimal CanBillingAmount
        {
            get
            {
                return _canBillingAmount;
            }

            set
            {
                _canBillingAmount = value;
            }
        }

        public string EInvoiceState
        {
            get
            {
                return _eInvoiceState;
            }

            set
            {
                _eInvoiceState = value;
            }
        }

        public string ErrMsg
        {
            get
            {
                return _errMsg;
            }

            set
            {
                _errMsg = value;
            }
        }

        public string UnableReason
        {
            get
            {
                return _unableReason;
            }

            set
            {
                _unableReason = value;
            }
        }
    }
}
