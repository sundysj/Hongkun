using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Model.Model.Buss
{
    /// <summary>
    /// 实体类Tb_Charge_Receipt 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class Tb_Charge_Receipt
    {
        public Tb_Charge_Receipt()
        { }
        #region Model
        private string _id;
        private string _bussid;
        private string _orderid;
        private string _receiptsign;
        private string _method;
        private string _userid;
        private string _name;
        private string _mobile;
        private decimal _amount;
        private string _receiptmemo;
        private string _receipttype;
        private DateTime _receiptdate;

        private DateTime? _paydate;

        private string _mchid;
        private string _partner;
        private string _prepaystr;
        private string _txntime;
        private string _returncode;
        private string _returnmsg;
        private string _isdeliver;
        private string _express;
        private string _expressnum;
        private string _deliveraddress;
        private string _ispay;
        private string _handlememo;
        private string _handlestate;
        private string _isreceive;
        private int? _isdelete;
        private string _CommunityID;
        private int _DispatchingType;

        [DisplayName("商品订单票据编码")]
        public string Id
        {
            set { _id = value; }
            get { return _id; }
        }
        public int? CorpId { get; set; }

        [DisplayName("商家ID")]
        public string BussId
        {
            set { _bussid = value; }
            get { return _bussid; }
        }
        [DisplayName("订单ID")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "必填")]
        public string OrderId
        {
            set { _orderid = value; }
            get { return _orderid; }
        }
        [DisplayName("订单编号")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "必填")]
        public string ReceiptSign
        {
            set { _receiptsign = value; }
            get { return _receiptsign; }
        }
        [DisplayName("收款方式")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "必填")]
        public string Method
        {
            set { _method = value; }
            get { return _method; }
        }
        [DisplayName("用户ID")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "必填")]
        public string UserId
        {
            set { _userid = value; }
            get { return _userid; }
        }
        [DisplayName("收货人")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "必填")]
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        [DisplayName("收货人电话")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "必填")]
        public string Mobile
        {
            set { _mobile = value; }
            get { return _mobile; }
        }
        [DisplayName("订单金额")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "必填")]
        public decimal Amount
        {
            set { _amount = value; }
            get { return _amount; }
        }

        /// <summary>
        /// 实际金额
        /// </summary>
        public decimal RealAmount { get; set; }

        [DisplayName("附言")]
        public string ReceiptMemo
        {
            set { _receiptmemo = value; }
            get { return _receiptmemo; }
        }
        [DisplayName("票据类别")]
        public string ReceiptType
        {
            set { _receipttype = value; }
            get { return _receipttype; }
        }
        [DisplayName("下单日期")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "必填")]
        public DateTime ReceiptDate
        {
            set { _receiptdate = value; }
            get { return _receiptdate; }
        }

        [DisplayName("付款日期")]
        public DateTime? PayDate
        {
            set { _paydate = value; }
            get { return _paydate; }
        }

        [DisplayName("合作商家ID")]
        public string MchId
        {
            set { _mchid = value; }
            get { return _mchid; }
        }
        [DisplayName("合作伙伴ID")]
        public string Partner
        {
            set { _partner = value; }
            get { return _partner; }
        }
        [DisplayName("预支付编码")]
        public string PrepayStr
        {
            set { _prepaystr = value; }
            get { return _prepaystr; }
        }
        [DisplayName("预支付时间截")]
        public string txnTime
        {
            set { _txntime = value; }
            get { return _txntime; }
        }
        [DisplayName("返回编码")]
        public string ReturnCode
        {
            set { _returncode = value; }
            get { return _returncode; }
        }
        [DisplayName("返回信息")]
        public string ReturnMsg
        {
            set { _returnmsg = value; }
            get { return _returnmsg; }
        }
        [DisplayName("是否发货")]
        public string IsDeliver
        {
            set { _isdeliver = value; }
            get { return _isdeliver; }
        }
        [DisplayName("快递商家")]
        public string Express
        {
            set { _express = value; }
            get { return _express; }
        }
        [DisplayName("快递号")]
        public string ExpressNum
        {
            set { _expressnum = value; }
            get { return _expressnum; }
        }
        [DisplayName("收货地址")]
        public string DeliverAddress
        {
            set { _deliveraddress = value; }
            get { return _deliveraddress; }
        }
        [DisplayName("付款状态")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "必填")]
        public string IsPay
        {
            set { _ispay = value; }
            get { return _ispay; }
        }
        [DisplayName("处理说明")]
        public string HandleMemo
        {
            set { _handlememo = value; }
            get { return _handlememo; }
        }
        [DisplayName("处理状态")]
        public string HandleState
        {
            set { _handlestate = value; }
            get { return _handlestate; }
        }
        [DisplayName("是否收货")]
        public string IsReceive
        {
            set { _isreceive = value; }
            get { return _isreceive; }
        }
        [DisplayName("是否删除")]
        public int? IsDelete
        {
            set { _isdelete = value; }
            get { return _isdelete; }
        }

        [DisplayName("社区Id")]
        public string CommunityID
        {
            set { _CommunityID = value; }
            get { return _CommunityID; }
        }


        [DisplayName("配送方式")]
        public int DispatchingType
        {
            set { _DispatchingType = value; }
            get { return _DispatchingType; }
        }

        /// <summary>
        /// 是否使用优惠券
        /// </summary>
        public int? IsUseCoupon { get; set; }

        /// <summary>
        /// 优惠券使用总额
        /// </summary>
        public decimal? CouponAmount { get; set; }

        /// <summary>
        /// 使用积分数量
        /// </summary>
        public int UsePoints { get; set; }

        /// <summary>
        /// 使用抵用金额
        /// </summary>
        public decimal PointDiscountAmount { get; set; }

        /// <summary>
        /// 使用使用记录id
        /// </summary>
        public string PointUseHistoryID { get; set; }

        #endregion Model



        #region 附加字段
        //快递字段
        private string _ExpressName;
        private string _CourierNumber;
        private string _TheSender;
        private string _TheSenderPhone;
        private string _deliveryTime;

        /// <summary>
        /// 快递公司
        /// </summary>
        [DisplayName("快递公司")]
        public string ExpressName { get => _ExpressName; set => _ExpressName = value; }
        /// <summary>
        /// 快递单号
        /// </summary>
        [DisplayName("快递单号")]
        public string CourierNumber { get => _CourierNumber; set => _CourierNumber = value; }
        /// <summary>
        /// 寄件人
        /// </summary>
        [DisplayName("寄件人")]
        public string TheSender { get => _TheSender; set => _TheSender = value; }
        /// <summary>
        /// 寄件电话
        /// </summary>
        [DisplayName("寄件电话")]
        public string TheSenderPhone { get => _TheSenderPhone; set => _TheSenderPhone = value; }
        /// <summary>
        /// 发货时间
        /// </summary>
        [DisplayName("发货时间")]
        public string DeliveryTime { get => _deliveryTime; set => _deliveryTime = value; }


        //配送
        private String _RequestDeliveryTime;
        private string _DeliveredBy;
        private string _DeliveredPhone;
        private string _ExpectedDeliveryTime;
        private String _IsSendOut;
        /// <summary>
        /// 要求送达时间
        /// </summary>
        [DisplayName("要求送达时间")]
        public String RequestDeliveryTime { get => _RequestDeliveryTime; set => _RequestDeliveryTime = value; }
        /// <summary>
        /// 配送人
        /// </summary>
        [DisplayName("配送人")]
        public string DeliveredBy { get => _DeliveredBy; set => _DeliveredBy = value; }
        /// <summary>
        /// 配送电话
        /// </summary>
        [DisplayName("配送电话")]
        public string DeliveredPhone { get => _DeliveredPhone; set => _DeliveredPhone = value; }
        /// <summary>
        /// 预计送达时间
        /// </summary>
        [DisplayName("确认送出")]
        public string IsSendOut { get => _IsSendOut; set => _IsSendOut = value; }

        /// <summary>
        /// 预计送达时间
        /// </summary>
        [DisplayName("预计送达时间")]
        public string ExpectedDeliveryTime { get => _ExpectedDeliveryTime; set => _ExpectedDeliveryTime = value; }


        //自提
        private String _EstimatedPickUpTime;
        private String _MerchantPickUpTime;
        private string _MerchantPickUpLocation;
        private string _PickUpContacts;
        private string _PickUpContactsPhone;
        private string _PickUpRemarks;
        private String _ExtractionCode;
        /// <summary>
        /// 预计提货时间
        /// </summary>
        [DisplayName("预计提货时间")]
        public String EstimatedPickUpTime { get => _EstimatedPickUpTime; set => _EstimatedPickUpTime = value; }
        /// <summary>
        /// 商家提货时间
        /// </summary>
        [DisplayName("商家提货时间")]
        public String MerchantPickUpTime { get => _MerchantPickUpTime; set => _MerchantPickUpTime = value; }
        /// <summary>
        /// 商家提货地址
        /// </summary>
        [DisplayName("商家提货地址")]
        public string MerchantPickUpLocation { get => _MerchantPickUpLocation; set => _MerchantPickUpLocation = value; }
        /// <summary>
        /// 提货联系人
        /// </summary>
        [DisplayName("提货联系人")]
        public string PickUpContacts { get => _PickUpContacts; set => _PickUpContacts = value; }
        /// <summary>
        /// 提货联系人电话
        /// </summary>
        [DisplayName("提货联系人电话")]
        public string PickUpContactsPhone { get => _PickUpContactsPhone; set => _PickUpContactsPhone = value; }
        /// <summary>
        /// 提货备注
        /// </summary>
        [DisplayName("提货备注")]
        public string PickUpRemarks { get => _PickUpRemarks; set => _PickUpRemarks = value; }

        [DisplayName("核销码")]
        public String ExtractionCode { get => _ExtractionCode; set => _ExtractionCode = value; }




        #endregion


        #region 退货信息
        private string _IsRetreat;
        private string _CustomerApplicationTime;
        private string _RetreatReason;
        private string _RetreatExplain;
        private string _RetreatImages;
        private string _IsAgree;
        private string _AgreeTime;
        private string _RetreatAddress;
        private string _Recipient;
        private string _RecipientTelephone;
        private string _DisagreeReason;
        private string _RetreatTime;
        private string _RetreatCourierNumber;
        private string _RetreatExpressName;
        private string _IsMerchantReceived;
        private string _MerchantReceivedTime;
        private string _IsPlatformRefund;
        private string _PlatformRefundTime;


        /// <summary>
        /// 客户申请时间
        /// </summary>
        [DisplayName("客户申请时间")]
        public string CustomerApplicationTime { get => _CustomerApplicationTime; set => _CustomerApplicationTime = value; }
        /// <summary>
        /// 退货原因
        /// </summary>
        [DisplayName("退货原因")]
        public string RetreatReason { get => _RetreatReason; set => _RetreatReason = value; }
        /// <summary>
        /// 退货说明
        /// </summary>
        [DisplayName("退货说明")]
        public string RetreatExplain { get => _RetreatExplain; set => _RetreatExplain = value; }
        /// <summary>
        /// 退货图片，最多四张‘,’分割
        /// </summary>
        [DisplayName("退货图片")]
        public string RetreatImages { get => _RetreatImages; set => _RetreatImages = value; }
        /// <summary>
        /// 商家是否同意
        /// </summary>
        [DisplayName("商家是否同意")]
        public string IsAgree { get => _IsAgree; set => _IsAgree = value; }
        /// <summary>
        /// 商家审核时间
        /// </summary>
        [DisplayName("商家审核时间")]
        public string AgreeTime { get => _AgreeTime; set => _AgreeTime = value; }
        /// <summary>
        /// 退回地址
        /// </summary>
        [DisplayName("退回地址")]
        public string RetreatAddress { get => _RetreatAddress; set => _RetreatAddress = value; }
        /// <summary>
        /// 收件人
        /// </summary>
        [DisplayName("收件人")]
        public string Recipient { get => _Recipient; set => _Recipient = value; }
        /// <summary>
        /// 收件人电话
        /// </summary>
        [DisplayName("收件人电话")]
        public string RecipientTelephone { get => _RecipientTelephone; set => _RecipientTelephone = value; }
        /// <summary>
        /// 不同意原因
        /// </summary>
        [DisplayName("不同意原因")]
        public string DisagreeReason { get => _DisagreeReason; set => _DisagreeReason = value; }
        /// <summary>
        /// 退货时间
        /// </summary>
        [DisplayName("退货时间")]
        public string RetreatTime { get => _RetreatTime; set => _RetreatTime = value; }
        /// <summary>
        /// 退货快递单号
        /// </summary>
        [DisplayName("退货快递单号")]
        public string RetreatCourierNumber { get => _RetreatCourierNumber; set => _RetreatCourierNumber = value; }
        /// <summary>
        /// 退货快递公司
        /// </summary>
        [DisplayName("退货快递公司")]
        public string RetreatExpressName { get => _RetreatExpressName; set => _RetreatExpressName = value; }
        /// <summary>
        /// 商家是否收到（是/否）,默认空
        /// </summary>
        [DisplayName("商家是否收到")]
        public string IsMerchantReceived { get => _IsMerchantReceived; set => _IsMerchantReceived = value; }
        /// <summary>
        /// 商家收货时间
        /// </summary>
        [DisplayName("商家收货时间")]
        public string MerchantReceivedTime { get => _MerchantReceivedTime; set => _MerchantReceivedTime = value; }
        /// <summary>
        /// 平台是否退款（是/否，默认空）
        /// </summary>
        [DisplayName("平台是否退款")]
        public string IsPlatformRefund { get => _IsPlatformRefund; set => _IsPlatformRefund = value; }
        /// <summary>
        /// 平台退款时间
        /// </summary>
        [DisplayName("平台退款时间")]
        public string PlatformRefundTime { get => _PlatformRefundTime; set => _PlatformRefundTime = value; }

        /// <summary>
        /// 是否退货（是/否）
        /// </summary>
        [DisplayName("是否退货")]
        public string IsRetreat { get => _IsRetreat; set => _IsRetreat = value; }

        private int _CancellationType;
        private string _CancellationReason;
        private string _IsCancellation;
        private string _CancellationTime;
        private String _ConfirmReceivedTime;
        /// <summary>
        /// 取消类型（1.业主主动取消，2.商家申请取消，3.超时未付款自动取消，4.超时未发货自动取消）
        /// </summary>
        [DisplayName("取消类型")]
        public int CancellationType { get => _CancellationType; set => _CancellationType = value; }
        /// <summary>
        /// 取消原因
        /// </summary>
        [DisplayName("取消原因")]
        public string CancellationReason { get => _CancellationReason; set => _CancellationReason = value; }
        /// <summary>
        /// 是否同意取消(是/否)
        /// </summary>
        [DisplayName("是否同意取消")]
        public string IsCancellation { get => _IsCancellation; set => _IsCancellation = value; }
        /// <summary>
        /// 取消时间
        /// </summary>
        [DisplayName("取消时间")]
        public string CancellationTime { get => _CancellationTime; set => _CancellationTime = value; }


        [DisplayName("收货时间")]
        public String ConfirmReceivedTime { get => _ConfirmReceivedTime; set => _ConfirmReceivedTime = value; }

        #endregion
        private decimal _Freight;

        [DisplayName("运费信息")]
        public decimal Freight { get => _Freight; set => _Freight = value; }

    }
}

