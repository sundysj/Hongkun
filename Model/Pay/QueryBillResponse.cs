using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Model.Pay
{
    [DataContract]
    public class QueryBillResponse
    {

        private static readonly long serialVersionUID = 1L;

        /** 格式 */
        [DataMember(Name = "format")]
        private string format;

        /** 消息体 */
        [DataMember(Name = "message")]
        private Message message;


        public QueryBillResponse()
        {

        }

        /**
         * 构造函数，通过输入对象，构造返回对象数据信息
         * @param request
         */
        public QueryBillResponse(QueryBillRequest request)
        {
            this.QueryBillResponseFormat = request.QueryBillRequestFormat;
            this.QueryBillResponseMessage = new Message(request.QueryBillRequestMessage);
        }

        public override string ToString()
        {
            return "QueryBillResponse[format=" + format + ",message=" + message == null ? "" : message.ToString() + "]";
        }

        public string QueryBillResponseFormat
        {
            get { return format; }
            set { this.format = value; }
        }

        public Message QueryBillResponseMessage
        {
            get { return message; }
            set { this.message = value; }
        }


        /**
         *  
         * 账单查询内部消息对象返回实体message内部类
         * 
         */
        [DataContract]
        public class Message
        {

            private static readonly long serialVersionUID = 1L;

            /** 消息头部 */
            [DataMember(Name = "head")]
            private Head head;

            /** 消息实体  */
            [DataMember(Name = "info")]
            private Info info;

            public Message()
            {
                this.head = new Head();
                this.info = new Info();
            }

            public Message(QueryBillRequest.Message requestMessage)
            {
                this.QueryBillResponseHead = new Head(requestMessage.QueryBillRequestHead);
                this.QueryBillResponseInfo = new Info(requestMessage.QueryBillRequestInfo);
            }

            public override string ToString()
            {
                return "QueryBillResponse.Message[head=" + head.ToString() + ",info=" + info.ToString() + "]";
            }

            public Head QueryBillResponseHead
            {
                get { return head; }
                set { this.head = value; }
            }

            public Info QueryBillResponseInfo
            {
                get { return info; }
                set { this.info = value; }
            }


            /// <summary>
            /// 账单查询内部消息对象返回实体Head内部类
            /// </summary>
            [DataContract]
            public class Head
            {

                private static readonly long serialVersionUID = 1L;

                /**  渠道 */
                [DataMember(Name = "channel")]
                private string channel;

                /**  交易码  */
                [DataMember(Name = "transCode")]
                private string transCode;

                /**  交易上行下送标志  */
                [DataMember(Name = "transFlag")]
                private string transFlag;

                /**  缴费中心交易序列号 */
                [DataMember(Name = "transSeqNum")]
                private string transSeqNum;

                /** 时间戳  */
                [DataMember(Name = "timeStamp")]
                private string timeStamp;

                /**  查询返回码 */
                [DataMember(Name = "returnCode")]
                private string returnCode;

                /**  返回提示信息  */
                [DataMember(Name = "returnMessage")]
                private string returnMessage;


                public Head(QueryBillRequest.Message.Head reqMessHead)
                {
                    this.Channel = reqMessHead.Channel;
                    this.TransSeqNum = reqMessHead.TransSeqNum;
                    this.TransCode = reqMessHead.TransCode;
                }


                public Head()
                {

                }

                public override string ToString()
                {
                    return "QueryBillResponse.Message.Head[channel=" + channel + ",transCode=" + transCode + ",transSeqNum=" + transSeqNum
                                        + ",timeStamp=" + timeStamp + ",returnCode=" + returnCode + ",returnMessage=" + returnMessage + "]";
                }
                public string Channel
                {
                    get { return channel; }
                    set { this.channel = value; }
                }

                public string TransCode
                {
                    get { return transCode; }
                    set { this.transCode = value; }
                }

                public string TransFlag
                {
                    get { return transFlag; }
                    set { this.transFlag = value; }
                }

                public string TransSeqNum
                {
                    get { return transSeqNum; }
                    set { this.transSeqNum = value; }
                }

                public string TimeStamp
                {
                    get { return timeStamp; }
                    set { this.timeStamp = value; }
                }

                public string ReturnCode
                {
                    get { return returnCode; }
                    set { this.returnCode = value; }
                }

                public string ReturnMessage
                {
                    get { return returnMessage; }
                    set { this.returnMessage = value; }
                }

            }

            /**
             *  
             * 账单查询内部消息对象返回实体Info内部类
             * 
             */
            [DataContract]
            public class Info
            {

                private static readonly long serialVersionUID = 1L;

                /** 缴费项目唯一标识号*/
                [DataMember(Name = "epayCode")]
                private string epayCode;

                /** 直连商户第三方商户号*/
                [DataMember(Name = "merchantId")]
                private string merchantId;

                /** 缴费中心流水号*/
                [DataMember(Name = "traceNo")]
                private string traceNo;

                /** 输入要素1*/
                [DataMember(Name = "input1")]
                private string input1;

                /** 输入要素2*/
                [DataMember(Name = "input2")]
                private string input2;

                /** 输入要素3*/
                [DataMember(Name = "input3")]
                private string input3;

                /** 输入要素4*/
                [DataMember(Name = "input4")]
                private string input4;

                /** 输入要素5*/
                [DataMember(Name = "input5")]
                private string input5;

                /** 户主名称*/
                [DataMember(Name = "custName")]
                private string custName;

                /** 户主地址*/
                [DataMember(Name = "custAddress")]
                private string custAddress;

                /** 缓存域信息*/
                [DataMember(Name = "cacheMem")]
                private string cacheMem;

                /** 备注字段*/
                [DataMember(Name = "remark")]
                private string remark;

                /** 缴费金额计算规则*/
                [DataMember(Name = "amtRule")]
                private string amtRule;

                /** 子账单数量*/
                [DataMember(Name = "totalBillCount")]
                private string totalBillCount;

                /** 账单信息体*/
                [DataMember(Name = "bills")]
                private List<Bill> bills;

                public Info()
                {

                }

                public Info(QueryBillRequest.Message.Info reqMessInfo)
                {
                    this.EpayCode = reqMessInfo.EpayCode;
                    this.Input1 = reqMessInfo.Input1;
                    this.Input2 = reqMessInfo.Input2;
                    this.Input3 = reqMessInfo.Input3;
                    this.Input4 = reqMessInfo.Input4;
                    this.Input5 = reqMessInfo.Input5;
                    this.bills = new List<Bill>();
                }

                public override string ToString()
                {
                    return "QueryBillResponse.Message.Info[epayCode=" + epayCode + ", input1="
                                        + input1 + ", input2=" + input2 + ", input3=" + input3
                                        + ", input4=" + input4 + ", input5=" + input5
                                        + ", totalBillCount=" + totalBillCount + ", bills="
                                        + bills.ToString() + "]";
                }

                public string EpayCode
                {
                    get { return epayCode; }
                    set { this.epayCode = value; }
                }

                public string MerchantId
                {
                    get { return merchantId; }
                    set { this.merchantId = value; }
                }

                public string TraceNo
                {
                    get { return traceNo; }
                    set { this.traceNo = value; }
                }

                public string Input1
                {
                    get { return input1; }
                    set { this.input1 = value; }
                }

                public string Input2
                {
                    get { return input2; }
                    set { this.input2 = value; }
                }

                public string Input3
                {
                    get { return input3; }
                    set { this.input3 = value; }
                }

                public string Input4
                {
                    get { return input4; }
                    set { this.input4 = value; }
                }

                public string Input5
                {
                    get { return input5; }
                    set { this.input5 = value; }
                }

                public string CustName
                {
                    get { return custName; }
                    set { this.custName = value; }
                }

                public string CustAddress
                {
                    get { return custAddress; }
                    set { this.custAddress = value; }
                }

                public string CacheMem
                {
                    get { return cacheMem; }
                    set { this.cacheMem = value; }
                }

                public string Remark
                {
                    get { return remark; }
                    set { this.remark = value; }
                }

                public string AmtRule
                {
                    get { return amtRule; }
                    set { this.amtRule = value; }
                }

                public string TotalBillCount
                {
                    get { return totalBillCount; }
                    set { this.totalBillCount = value; }
                }

                /**
                 *  @param bills
                 *  设置账单循环域子账单
                 */
                public List<Bill> Bills
                {
                    get { return bills; }
                    set { this.bills = value; }
                }


                /**
                 *  
                 * 账单查询内部消息对象返回实体Bill内部类
                 * 
                 */
                [DataContract]
                public class Bill
                {

                    private static readonly long serialVersionUID = 1L;

                    /** 账单编号*/
                    [DataMember(Name = "billNo")]
                    private string billNo;

                    /** 账单名称*/
                    [DataMember(Name = "billName")]
                    private string billName;

                    /** 欠费金额*/
                    [DataMember(Name = "oweAmt")]
                    private string oweAmt;

                    /** 手续费*/
                    [DataMember(Name = "feeAmt")]
                    private string feeAmt;

                    /** 最小金额*/
                    [DataMember(Name = "minAmt")]
                    private string minAmt;

                    /** 最大金额*/
                    [DataMember(Name = "maxAmt")]
                    private string maxAmt;

                    /** 余额*/
                    [DataMember(Name = "balance")]
                    private string balance;

                    /** 缴费账单到期日*/
                    [DataMember(Name = "expireDate")]
                    private string expireDate;

                    /** 收款商户号*/
                    [DataMember(Name = "rcvMerchantId")]
                    private string rcvMerchantId;

                    /** 收款账号*/
                    [DataMember(Name = "rcvAcc")]
                    private string rcvAcc;

                    /** 分账模板号*/
                    [DataMember(Name = "tempSplitAcc")]
                    private string tempSplitAcc;

                    /** 均匀时段缴费 */
                    [DataMember(Name = "unitDetail")]
                    private UnitDetail unitDetail;

                    /** 选择套餐*/
                    [DataMember(Name = "optionDetails")]
                    private List<OptionDetail> optionDetails;

                    /** 账单详情描述*/
                    [DataMember(Name = "descDetails")]
                    private List<DescDetail> descDetails;

                    public Bill()
                    {

                    }

                    public override string ToString()
                    {
                        return "QueryBillResponse.Message.Info.Bill[billNo=" + billNo
                                                + ", billName=" + billName + ", oweAmt=" + oweAmt + ", feeAmt=" + feeAmt + ", minAmt=" + minAmt
                                                + ", balance=" + balance + ", expireDate=" + expireDate + ", rcvMerchantId=" + rcvMerchantId
                                                + ", rcvAcc=" + rcvAcc + ", tempSplitAcc=" + tempSplitAcc
                                                + ", unitDetail=" + unitDetail
                                                + ", optionDetails=" + optionDetails
                                                + ", descDetails=" + descDetails + "]";
                    }

                    public string BillNo
                    {
                        get { return billNo; }
                        set { this.billNo = value; }
                    }

                    public string BillName
                    {
                        get { return billName; }
                        set { this.billName = value; }
                    }

                    public string OweAmt
                    {
                        get { return oweAmt; }
                        set { this.oweAmt = value; }
                    }

                    public string FeeAmt
                    {
                        get { return feeAmt; }
                        set { this.feeAmt = value; }
                    }

                    public string MinAmt
                    {
                        get { return minAmt; }
                        set { this.minAmt = value; }
                    }

                    public string MaxAmt
                    {
                        get { return maxAmt; }
                        set { this.maxAmt = value; }
                    }

                    public string Balance
                    {
                        get { return balance; }
                        set { this.balance = value; }
                    }

                    public string ExpireDate
                    {
                        get { return expireDate; }
                        set { this.expireDate = value; }
                    }

                    public string RcvMerchantId
                    {
                        get { return rcvMerchantId; }
                        set { this.rcvMerchantId = value; }
                    }

                    public string RcvAcc
                    {
                        get { return rcvAcc; }
                        set { this.rcvAcc = value; }
                    }

                    /**
                     * 分账模板号
                     * @return
                     */
                    public string TempSplitAcc
                    {
                        get { return tempSplitAcc; }
                        set { this.tempSplitAcc = value; }
                    }

                    public UnitDetail BillUnitDetail
                    {
                        get { return unitDetail; }
                        set { this.unitDetail = value; }
                    }

                    /**
                     *  设置账单描述详情键值对
                     *  @param feeDetails
                     */
                    public List<DescDetail> DescDetails
                    {
                        get { return descDetails; }
                        set { this.descDetails = value; }
                    }

                    public List<OptionDetail> OptionDetails
                    {
                        get { return optionDetails; }
                        set { this.optionDetails = value; }
                    }

                    /**
                     * 均匀时段
                     * 
                     * @date 2017年12月21日
                     */
                    [DataContract]
                    public class UnitDetail
                    {

                        private static readonly long serialVersionUID = 1L;

                        /** 单位名称 */
                        [DataMember(Name = "unitName")]
                        private string unitName;

                        /** 单位金额 */
                        [DataMember(Name = "unitAmount")]
                        private string unitAmount;

                        /** 最小单位数量 */
                        [DataMember(Name = "minUnitNum")]
                        private string minUnitNum;
                        public UnitDetail() { }

                        public UnitDetail(string unitName, string unitAmount, string minUnitNum)
                        {
                            this.unitName = unitName;
                            this.unitAmount = unitAmount;
                            this.minUnitNum = minUnitNum;
                        }

                        public string UnitName
                        {
                            get { return unitName; }
                            set { this.unitName = value; ; }
                        }

                        public string UnitAmount
                        {
                            get { return unitAmount; }
                            set { this.unitAmount = value; }
                        }

                        public string MinUnitNum
                        {
                            get { return minUnitNum; }
                            set { this.minUnitNum = value; }
                        }


                        public override string ToString()
                        {
                            return "QueryBillResponse.Message.Info.Bill.unitDetail[unitName=" + unitName
                                                        + ", unitAmount=" + unitAmount + ", minUnitNum=" + minUnitNum + "]";
                        }


                    }

                    /**
                     * @title 选择套餐循环
                     *  
                     *  @date 2017-12-18
                     *  
                     * 
                     */
                    [DataContract]
                    public class OptionDetail
                    {

                        private static readonly long serialVersionUID = 1L;

                        /**套餐编号 根据编号排序*/
                        [DataMember(Name = "optionCode")]
                        private string optionCode;

                        /**套餐名称*/
                        [DataMember(Name = "optionName")]
                        private string optionName;

                        /**套餐金额*/
                        [DataMember(Name = "optionAmt")]
                        private string optionAmt;

                        public OptionDetail()
                        {

                        }

                        public OptionDetail(string optionCode, string optionName, string optionAmt)
                        {
                            this.optionCode = optionCode;
                            this.optionName = optionName;
                            this.optionAmt = optionAmt;
                        }

                        public string OptionCode
                        {
                            get { return optionCode; }
                            set { this.optionCode = value; }
                        }

                        public string OptionName
                        {
                            get { return optionName; }
                            set { this.optionName = value; }
                        }

                        public string OptionAmt
                        {
                            get { return optionAmt; }
                            set { this.optionAmt = value; }
                        }

                        public override string ToString()
                        {
                            return "QueryBillResponse.Message.Info.Bill.OptionDetail[optionCode=" + optionCode
                                                        + ", optionName=" + optionName + ", optionAmt=" + optionAmt + "]";
                        }



                    }
                    /**
                     * @title 账单详情
                     *  
                     *  @date 2017-12-18
                     */
                    [DataContract]
                    public class DescDetail
                    {

                        private static readonly long serialVersionUID = 1L;

                        /**账单详情-名称*/
                        [DataMember(Name = "sCpt")]
                        private string sCpt;

                        /**账单详情-取值*/
                        [DataMember(Name = "sVal")]
                        private string sVal;


                        public DescDetail()
                        {

                        }

                        public DescDetail(string sCpt, string sVal)
                        {
                            this.sCpt = sCpt;
                            this.sVal = sVal;
                        }

                        public string SCpt
                        {
                            get { return sCpt; }
                            set { this.sCpt = value; }
                        }

                        public string SVal
                        {
                            get { return sVal; }
                            set { this.sVal = value; }
                        }

                        public override string ToString()
                        {
                            return "QueryBillResponse.Message.Info.Bill.FeeDetail[sCpt=" + sCpt + ",sVal=" + sVal + "]";
                        }
                    }
                }
            }
        }
    }
}
