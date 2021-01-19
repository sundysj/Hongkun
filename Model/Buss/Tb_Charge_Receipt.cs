using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Model.Model.Buss
{
    /// <summary>
    /// ʵ����Tb_Charge_Receipt ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
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

        [DisplayName("��Ʒ����Ʊ�ݱ���")]
        public string Id
        {
            set { _id = value; }
            get { return _id; }
        }
        public int? CorpId { get; set; }

        [DisplayName("�̼�ID")]
        public string BussId
        {
            set { _bussid = value; }
            get { return _bussid; }
        }
        [DisplayName("����ID")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "����")]
        public string OrderId
        {
            set { _orderid = value; }
            get { return _orderid; }
        }
        [DisplayName("�������")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "����")]
        public string ReceiptSign
        {
            set { _receiptsign = value; }
            get { return _receiptsign; }
        }
        [DisplayName("�տʽ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "����")]
        public string Method
        {
            set { _method = value; }
            get { return _method; }
        }
        [DisplayName("�û�ID")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "����")]
        public string UserId
        {
            set { _userid = value; }
            get { return _userid; }
        }
        [DisplayName("�ջ���")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "����")]
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        [DisplayName("�ջ��˵绰")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "����")]
        public string Mobile
        {
            set { _mobile = value; }
            get { return _mobile; }
        }
        [DisplayName("�������")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "����")]
        public decimal Amount
        {
            set { _amount = value; }
            get { return _amount; }
        }

        /// <summary>
        /// ʵ�ʽ��
        /// </summary>
        public decimal RealAmount { get; set; }

        [DisplayName("����")]
        public string ReceiptMemo
        {
            set { _receiptmemo = value; }
            get { return _receiptmemo; }
        }
        [DisplayName("Ʊ�����")]
        public string ReceiptType
        {
            set { _receipttype = value; }
            get { return _receipttype; }
        }
        [DisplayName("�µ�����")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "����")]
        public DateTime ReceiptDate
        {
            set { _receiptdate = value; }
            get { return _receiptdate; }
        }

        [DisplayName("��������")]
        public DateTime? PayDate
        {
            set { _paydate = value; }
            get { return _paydate; }
        }

        [DisplayName("�����̼�ID")]
        public string MchId
        {
            set { _mchid = value; }
            get { return _mchid; }
        }
        [DisplayName("�������ID")]
        public string Partner
        {
            set { _partner = value; }
            get { return _partner; }
        }
        [DisplayName("Ԥ֧������")]
        public string PrepayStr
        {
            set { _prepaystr = value; }
            get { return _prepaystr; }
        }
        [DisplayName("Ԥ֧��ʱ���")]
        public string txnTime
        {
            set { _txntime = value; }
            get { return _txntime; }
        }
        [DisplayName("���ر���")]
        public string ReturnCode
        {
            set { _returncode = value; }
            get { return _returncode; }
        }
        [DisplayName("������Ϣ")]
        public string ReturnMsg
        {
            set { _returnmsg = value; }
            get { return _returnmsg; }
        }
        [DisplayName("�Ƿ񷢻�")]
        public string IsDeliver
        {
            set { _isdeliver = value; }
            get { return _isdeliver; }
        }
        [DisplayName("����̼�")]
        public string Express
        {
            set { _express = value; }
            get { return _express; }
        }
        [DisplayName("��ݺ�")]
        public string ExpressNum
        {
            set { _expressnum = value; }
            get { return _expressnum; }
        }
        [DisplayName("�ջ���ַ")]
        public string DeliverAddress
        {
            set { _deliveraddress = value; }
            get { return _deliveraddress; }
        }
        [DisplayName("����״̬")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "����")]
        public string IsPay
        {
            set { _ispay = value; }
            get { return _ispay; }
        }
        [DisplayName("����˵��")]
        public string HandleMemo
        {
            set { _handlememo = value; }
            get { return _handlememo; }
        }
        [DisplayName("����״̬")]
        public string HandleState
        {
            set { _handlestate = value; }
            get { return _handlestate; }
        }
        [DisplayName("�Ƿ��ջ�")]
        public string IsReceive
        {
            set { _isreceive = value; }
            get { return _isreceive; }
        }
        [DisplayName("�Ƿ�ɾ��")]
        public int? IsDelete
        {
            set { _isdelete = value; }
            get { return _isdelete; }
        }

        [DisplayName("����Id")]
        public string CommunityID
        {
            set { _CommunityID = value; }
            get { return _CommunityID; }
        }


        [DisplayName("���ͷ�ʽ")]
        public int DispatchingType
        {
            set { _DispatchingType = value; }
            get { return _DispatchingType; }
        }

        /// <summary>
        /// �Ƿ�ʹ���Ż�ȯ
        /// </summary>
        public int? IsUseCoupon { get; set; }

        /// <summary>
        /// �Ż�ȯʹ���ܶ�
        /// </summary>
        public decimal? CouponAmount { get; set; }

        /// <summary>
        /// ʹ�û�������
        /// </summary>
        public int UsePoints { get; set; }

        /// <summary>
        /// ʹ�õ��ý��
        /// </summary>
        public decimal PointDiscountAmount { get; set; }

        /// <summary>
        /// ʹ��ʹ�ü�¼id
        /// </summary>
        public string PointUseHistoryID { get; set; }

        #endregion Model



        #region �����ֶ�
        //����ֶ�
        private string _ExpressName;
        private string _CourierNumber;
        private string _TheSender;
        private string _TheSenderPhone;
        private string _deliveryTime;

        /// <summary>
        /// ��ݹ�˾
        /// </summary>
        [DisplayName("��ݹ�˾")]
        public string ExpressName { get => _ExpressName; set => _ExpressName = value; }
        /// <summary>
        /// ��ݵ���
        /// </summary>
        [DisplayName("��ݵ���")]
        public string CourierNumber { get => _CourierNumber; set => _CourierNumber = value; }
        /// <summary>
        /// �ļ���
        /// </summary>
        [DisplayName("�ļ���")]
        public string TheSender { get => _TheSender; set => _TheSender = value; }
        /// <summary>
        /// �ļ��绰
        /// </summary>
        [DisplayName("�ļ��绰")]
        public string TheSenderPhone { get => _TheSenderPhone; set => _TheSenderPhone = value; }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        [DisplayName("����ʱ��")]
        public string DeliveryTime { get => _deliveryTime; set => _deliveryTime = value; }


        //����
        private String _RequestDeliveryTime;
        private string _DeliveredBy;
        private string _DeliveredPhone;
        private string _ExpectedDeliveryTime;
        private String _IsSendOut;
        /// <summary>
        /// Ҫ���ʹ�ʱ��
        /// </summary>
        [DisplayName("Ҫ���ʹ�ʱ��")]
        public String RequestDeliveryTime { get => _RequestDeliveryTime; set => _RequestDeliveryTime = value; }
        /// <summary>
        /// ������
        /// </summary>
        [DisplayName("������")]
        public string DeliveredBy { get => _DeliveredBy; set => _DeliveredBy = value; }
        /// <summary>
        /// ���͵绰
        /// </summary>
        [DisplayName("���͵绰")]
        public string DeliveredPhone { get => _DeliveredPhone; set => _DeliveredPhone = value; }
        /// <summary>
        /// Ԥ���ʹ�ʱ��
        /// </summary>
        [DisplayName("ȷ���ͳ�")]
        public string IsSendOut { get => _IsSendOut; set => _IsSendOut = value; }

        /// <summary>
        /// Ԥ���ʹ�ʱ��
        /// </summary>
        [DisplayName("Ԥ���ʹ�ʱ��")]
        public string ExpectedDeliveryTime { get => _ExpectedDeliveryTime; set => _ExpectedDeliveryTime = value; }


        //����
        private String _EstimatedPickUpTime;
        private String _MerchantPickUpTime;
        private string _MerchantPickUpLocation;
        private string _PickUpContacts;
        private string _PickUpContactsPhone;
        private string _PickUpRemarks;
        private String _ExtractionCode;
        /// <summary>
        /// Ԥ�����ʱ��
        /// </summary>
        [DisplayName("Ԥ�����ʱ��")]
        public String EstimatedPickUpTime { get => _EstimatedPickUpTime; set => _EstimatedPickUpTime = value; }
        /// <summary>
        /// �̼����ʱ��
        /// </summary>
        [DisplayName("�̼����ʱ��")]
        public String MerchantPickUpTime { get => _MerchantPickUpTime; set => _MerchantPickUpTime = value; }
        /// <summary>
        /// �̼������ַ
        /// </summary>
        [DisplayName("�̼������ַ")]
        public string MerchantPickUpLocation { get => _MerchantPickUpLocation; set => _MerchantPickUpLocation = value; }
        /// <summary>
        /// �����ϵ��
        /// </summary>
        [DisplayName("�����ϵ��")]
        public string PickUpContacts { get => _PickUpContacts; set => _PickUpContacts = value; }
        /// <summary>
        /// �����ϵ�˵绰
        /// </summary>
        [DisplayName("�����ϵ�˵绰")]
        public string PickUpContactsPhone { get => _PickUpContactsPhone; set => _PickUpContactsPhone = value; }
        /// <summary>
        /// �����ע
        /// </summary>
        [DisplayName("�����ע")]
        public string PickUpRemarks { get => _PickUpRemarks; set => _PickUpRemarks = value; }

        [DisplayName("������")]
        public String ExtractionCode { get => _ExtractionCode; set => _ExtractionCode = value; }




        #endregion


        #region �˻���Ϣ
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
        /// �ͻ�����ʱ��
        /// </summary>
        [DisplayName("�ͻ�����ʱ��")]
        public string CustomerApplicationTime { get => _CustomerApplicationTime; set => _CustomerApplicationTime = value; }
        /// <summary>
        /// �˻�ԭ��
        /// </summary>
        [DisplayName("�˻�ԭ��")]
        public string RetreatReason { get => _RetreatReason; set => _RetreatReason = value; }
        /// <summary>
        /// �˻�˵��
        /// </summary>
        [DisplayName("�˻�˵��")]
        public string RetreatExplain { get => _RetreatExplain; set => _RetreatExplain = value; }
        /// <summary>
        /// �˻�ͼƬ��������š�,���ָ�
        /// </summary>
        [DisplayName("�˻�ͼƬ")]
        public string RetreatImages { get => _RetreatImages; set => _RetreatImages = value; }
        /// <summary>
        /// �̼��Ƿ�ͬ��
        /// </summary>
        [DisplayName("�̼��Ƿ�ͬ��")]
        public string IsAgree { get => _IsAgree; set => _IsAgree = value; }
        /// <summary>
        /// �̼����ʱ��
        /// </summary>
        [DisplayName("�̼����ʱ��")]
        public string AgreeTime { get => _AgreeTime; set => _AgreeTime = value; }
        /// <summary>
        /// �˻ص�ַ
        /// </summary>
        [DisplayName("�˻ص�ַ")]
        public string RetreatAddress { get => _RetreatAddress; set => _RetreatAddress = value; }
        /// <summary>
        /// �ռ���
        /// </summary>
        [DisplayName("�ռ���")]
        public string Recipient { get => _Recipient; set => _Recipient = value; }
        /// <summary>
        /// �ռ��˵绰
        /// </summary>
        [DisplayName("�ռ��˵绰")]
        public string RecipientTelephone { get => _RecipientTelephone; set => _RecipientTelephone = value; }
        /// <summary>
        /// ��ͬ��ԭ��
        /// </summary>
        [DisplayName("��ͬ��ԭ��")]
        public string DisagreeReason { get => _DisagreeReason; set => _DisagreeReason = value; }
        /// <summary>
        /// �˻�ʱ��
        /// </summary>
        [DisplayName("�˻�ʱ��")]
        public string RetreatTime { get => _RetreatTime; set => _RetreatTime = value; }
        /// <summary>
        /// �˻���ݵ���
        /// </summary>
        [DisplayName("�˻���ݵ���")]
        public string RetreatCourierNumber { get => _RetreatCourierNumber; set => _RetreatCourierNumber = value; }
        /// <summary>
        /// �˻���ݹ�˾
        /// </summary>
        [DisplayName("�˻���ݹ�˾")]
        public string RetreatExpressName { get => _RetreatExpressName; set => _RetreatExpressName = value; }
        /// <summary>
        /// �̼��Ƿ��յ�����/��,Ĭ�Ͽ�
        /// </summary>
        [DisplayName("�̼��Ƿ��յ�")]
        public string IsMerchantReceived { get => _IsMerchantReceived; set => _IsMerchantReceived = value; }
        /// <summary>
        /// �̼��ջ�ʱ��
        /// </summary>
        [DisplayName("�̼��ջ�ʱ��")]
        public string MerchantReceivedTime { get => _MerchantReceivedTime; set => _MerchantReceivedTime = value; }
        /// <summary>
        /// ƽ̨�Ƿ��˿��/��Ĭ�Ͽգ�
        /// </summary>
        [DisplayName("ƽ̨�Ƿ��˿�")]
        public string IsPlatformRefund { get => _IsPlatformRefund; set => _IsPlatformRefund = value; }
        /// <summary>
        /// ƽ̨�˿�ʱ��
        /// </summary>
        [DisplayName("ƽ̨�˿�ʱ��")]
        public string PlatformRefundTime { get => _PlatformRefundTime; set => _PlatformRefundTime = value; }

        /// <summary>
        /// �Ƿ��˻�����/��
        /// </summary>
        [DisplayName("�Ƿ��˻�")]
        public string IsRetreat { get => _IsRetreat; set => _IsRetreat = value; }

        private int _CancellationType;
        private string _CancellationReason;
        private string _IsCancellation;
        private string _CancellationTime;
        private String _ConfirmReceivedTime;
        /// <summary>
        /// ȡ�����ͣ�1.ҵ������ȡ����2.�̼�����ȡ����3.��ʱδ�����Զ�ȡ����4.��ʱδ�����Զ�ȡ����
        /// </summary>
        [DisplayName("ȡ������")]
        public int CancellationType { get => _CancellationType; set => _CancellationType = value; }
        /// <summary>
        /// ȡ��ԭ��
        /// </summary>
        [DisplayName("ȡ��ԭ��")]
        public string CancellationReason { get => _CancellationReason; set => _CancellationReason = value; }
        /// <summary>
        /// �Ƿ�ͬ��ȡ��(��/��)
        /// </summary>
        [DisplayName("�Ƿ�ͬ��ȡ��")]
        public string IsCancellation { get => _IsCancellation; set => _IsCancellation = value; }
        /// <summary>
        /// ȡ��ʱ��
        /// </summary>
        [DisplayName("ȡ��ʱ��")]
        public string CancellationTime { get => _CancellationTime; set => _CancellationTime = value; }


        [DisplayName("�ջ�ʱ��")]
        public String ConfirmReceivedTime { get => _ConfirmReceivedTime; set => _ConfirmReceivedTime = value; }

        #endregion
        private decimal _Freight;

        [DisplayName("�˷���Ϣ")]
        public decimal Freight { get => _Freight; set => _Freight = value; }

    }
}

