using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    class E_Model
    {
    }
    /// <summary>
    /// 订单主信息
    /// </summary>
    public class E_OrderMain
    {

        private string _groupFlag;

        private string _storeCode;

        private string _posNo;

        private string _posDate;

        private decimal _amountWithTax;

        private string _invoiceType;

        private string _miAccount;

        private string _remark;

        private string _cashierName;

        private string _checkerName;

        private string _invoicerName;

        private string _originInvoiceNo;

        private string _originInvoiceCode;

        private string _email;

        private string _purchaserInfoFill;

        private string _sellerInfoFill;

        private List<E_OrderDetail> _details;


        /// <summary>
        /// 分组标记
        /// </summary>
        public string groupFlag
        {
            get
            {
                return _groupFlag;
            }

            set
            {
                _groupFlag = value;
            }
        }

        /// <summary>
        /// 门店代码
        /// </summary>
        public string storeCode
        {
            get
            {
                return _storeCode;
            }

            set
            {
                _storeCode = value;
            }
        }

        /// <summary>
        /// 小票订单号
        /// </summary>
        public string posNo
        {
            get
            {
                return _posNo;
            }

            set
            {
                _posNo = value;
            }
        }

        /// <summary>
        /// 小票日期
        /// </summary>
        public string posDate
        {
            get
            {
                return _posDate;
            }

            set
            {
                _posDate = value;
            }
        }

        /// <summary>
        /// 开具发票的含税金额
        /// </summary>
        public decimal amountWithTax
        {
            get
            {
                return _amountWithTax;
            }

            set
            {
                _amountWithTax = value;
            }
        }

        /// <summary>
        /// 发票类型，c：普票 ce：普电票 s：专票
        /// </summary>
        public string invoiceType
        {
            get
            {
                return _invoiceType;
            }

            set
            {
                _invoiceType = value;
            }
        }

        /// <summary>
        /// 客户端账号
        /// </summary>
        public string miAccount
        {
            get
            {
                return _miAccount;
            }

            set
            {
                _miAccount = value;
            }
        }

        /// <summary>
        /// 发票备注
        /// </summary>
        public string remark
        {
            get
            {
                return _remark;
            }

            set
            {
                _remark = value;
            }
        }

        /// <summary>
        /// 收款人姓名
        /// </summary>
        public string cashierName
        {
            get
            {
                return _cashierName;
            }

            set
            {
                _cashierName = value;
            }
        }

        /// <summary>
        /// 复核人姓名
        /// </summary>
        public string checkerName
        {
            get
            {
                return _checkerName;
            }

            set
            {
                _checkerName = value;
            }
        }

        /// <summary>
        /// 开票人姓名
        /// </summary>
        public string invoicerName
        {
            get
            {
                return _invoicerName;
            }

            set
            {
                _invoicerName = value;
            }
        }

        /// <summary>
        /// 原发票号码(红冲用)
        /// </summary>
        public string originInvoiceNo
        {
            get
            {
                return _originInvoiceNo;
            }

            set
            {
                _originInvoiceNo = value;
            }
        }

        /// <summary>
        /// 原发票代码(红冲用)
        /// </summary>
        public string originInvoiceCode
        {
            get
            {
                return _originInvoiceCode;
            }

            set
            {
                _originInvoiceCode = value;
            }
        }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string email
        {
            get
            {
                return _email;
            }

            set
            {
                _email = value;
            }
        }
        /// <summary>
        /// 固定值1
        /// </summary>
        public string purchaserInfoFill
        {
            get
            {
                return _purchaserInfoFill;
            }

            set
            {
                _purchaserInfoFill = value;
            }
        }
        /// <summary>
        /// 固定值0
        /// </summary>
        public string sellerInfoFill
        {
            get
            {
                return _sellerInfoFill;
            }
            set
            {
                _sellerInfoFill = value;
            }
        }

        /// <summary>
        /// 订单明细
        /// </summary>
        public List<E_OrderDetail> details
        {
            get
            {
                return _details;
            }

            set
            {
                _details = value;
            }
        }

    }


    /// <summary>
    /// 订单明细
    /// </summary>
    public class E_OrderDetail
    {
        private string _itemName;

        private string _itemSpec;

        private decimal _quantity;

        private decimal _unitPrice;

        private decimal _outerDiscountWithTax;

        private string _volunCode;

        private string _itemCode;

        private decimal _amountWithTax;

        private decimal _taxRate;

        private decimal _amountWithoutTax;

        private decimal _taxAmount;


        /// <summary>
        /// 明细名称
        /// </summary>
        public string itemName
        {
            get
            {
                return _itemName;
            }

            set
            {
                _itemName = value;
            }
        }

        /// <summary>
        /// 规格
        /// </summary>
        public string itemSpec
        {
            get
            {
                return _itemSpec;
            }

            set
            {
                _itemSpec = value;
            }
        }

        /// <summary>
        /// 数量
        /// </summary>
        public decimal quantity
        {
            get
            {
                return _quantity;
            }

            set
            {
                _quantity = value;
            }
        }

        /// <summary>
        /// 单价
        /// </summary>
        public decimal unitPrice
        {
            get
            {
                return _unitPrice;
            }

            set
            {
                _unitPrice = value;
            }
        }

        /// <summary>
        /// 明细直接折扣
        /// </summary>
        public decimal outerDiscountWithTax
        {
            get
            {
                return _outerDiscountWithTax;
            }

            set
            {
                _outerDiscountWithTax = value;
            }
        }

        /// <summary>
        /// 商品编码
        /// </summary>
        public string volunCode
        {
            get
            {
                return _volunCode;
            }

            set
            {
                _volunCode = value;
            }
        }
        /// <summary>
        /// 商品税编
        /// </summary>
        public string itemCode
        {
            get
            {
                return _itemCode;
            }

            set
            {
                _itemCode = value;
            }
        }
        /// <summary>
        /// 含税总价
        /// </summary>
        public decimal amountWithTax
        {
            get
            {
                return _amountWithTax;
            }

            set
            {
                _amountWithTax = value;
            }
        }
        /// <summary>
        /// 税率,整数
        /// </summary>
        public decimal taxRate
        {
            get
            {
                return _taxRate;
            }
            set
            {
                _taxRate = value;
            }
        }
        /// <summary>
        /// 不含税金额
        /// </summary>
        public decimal amountWithoutTax
        {
            get
            {
                return _amountWithoutTax;
            }
            set
            {
                _amountWithoutTax = value;
            }
        }
        /// <summary>
        /// 税额
        /// </summary>
        public decimal taxAmount
        {
            get
            {
                return _taxAmount;
            }
            set
            {
                _taxAmount = value;
            }
        }
    }

    /// <summary>
    /// 发票抬头
    /// </summary>
    public class E_Title
    {
        private string _name;

        private string _taxerId;

        private string _bankName;

        private string _bankAccount;

        private string _companyAddr;

        private string _companyPhone;

        /// <summary>
        /// 公司/个人
        /// </summary>
        public string name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
            }
        }

        /// <summary>
        /// 纳税人识别号
        /// </summary>
        public string taxerId
        {
            get
            {
                return _taxerId;
            }

            set
            {
                _taxerId = value;
            }
        }

        /// <summary>
        /// 开户行名称
        /// </summary>
        public string bankName
        {
            get
            {
                return _bankName;
            }

            set
            {
                _bankName = value;
            }
        }

        /// <summary>
        /// 银行账号
        /// </summary>
        public string bankAccount
        {
            get
            {
                return _bankAccount;
            }

            set
            {
                _bankAccount = value;
            }
        }

        /// <summary>
        /// 公司地址
        /// </summary>
        public string companyAddr
        {
            get
            {
                return _companyAddr;
            }

            set
            {
                _companyAddr = value;
            }
        }

        /// <summary>
        /// 公司电话
        /// </summary>
        public string companyPhone
        {
            get
            {
                return _companyPhone;
            }

            set
            {
                _companyPhone = value;
            }
        }


    }

    /// <summary>
    /// 用户信息
    /// </summary>
    public class E_User
    {
        private string _mobilePhone;

        /// <summary>
        /// 用户手机
        /// </summary>
        public string mobilePhone
        {
            get
            {
                return _mobilePhone;
            }

            set
            {
                _mobilePhone = value;
            }
        }
    }

    public class FeesType
    {
        private string _RFID;

        private int _FType;

        public string RFID
        {
            get
            {
                return _RFID;
            }

            set
            {
                _RFID = value;
            }
        }

        public int FType
        {
            get
            {
                return _FType;
            }

            set
            {
                _FType = value;
            }
        }

    }


    public class EIDNo
    {
        private string _InvocieID;

        private string _PosNO;

        public string InvoiceID
        {
            get
            {
                return _InvocieID;
            }

            set
            {
                _InvocieID = value;
            }
        }

        public string PosNo
        {
            get
            {
                return _PosNO;
            }

            set
            {
                _PosNO = value;
            }
        }

    }

    public class BillDetail
    {
        private double _billAmount;

        private string _costName;

        private int _fType;

        private string _feesStateDate;

        private string _feesEndDate;

        private string _rFID;

        private string _roomName;

        private double _taxAmount;

        private int _taxRate;

        private string _billCode;

        public double BillAmount
        {
            get
            {
                return _billAmount;
            }

            set
            {
                _billAmount = value;
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

        public int FType
        {
            get
            {
                return _fType;
            }

            set
            {
                _fType = value;
            }
        }

        public string FeesStateDate
        {
            get
            {
                return _feesStateDate;
            }

            set
            {
                _feesStateDate = value;
            }
        }

        public string FeesEndDate
        {
            get
            {
                return _feesEndDate;
            }

            set
            {
                _feesEndDate = value;
            }
        }

        public string RFID
        {
            get
            {
                return _rFID;
            }

            set
            {
                _rFID = value;
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

        public double TaxAmount
        {
            get
            {
                return _taxAmount;
            }

            set
            {
                _taxAmount = value;
            }
        }

        public int TaxRate
        {
            get
            {
                return _taxRate;
            }

            set
            {
                _taxRate = value;
            }
        }

        public string BillCode
        {
            get
            {
                return _billCode;
            }

            set
            {
                _billCode = value;
            }
        }
    }
}
