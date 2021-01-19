using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model.Pay
{
    /// <summary>
    /// 直连商户平台缴费销账输入对象,需要转换成json串发送给第三方系统
    /// </summary>
    [DataContract]
    public class ChargeBillRequest
    {
        private static readonly long serialVersionUID = 1L;
        
        /// <summary>
        /// 格式
        /// </summary>
        [DataMember(Name = "format")]
        private string format;

        /// <summary>
        /// 消息
        /// </summary>
        [DataMember(Name = "message")]
        private Message message;

        public override string ToString()
        {
            return "ChargeBillRequest[format=" + format + ",message=" + message.ToString() + "]";
        }

        /// <summary>
        /// 格式
        /// </summary>
        public string ChargeBillRequestFormat
        {
            get { return format; }
            set { this.format = value; }
        }

        /// <summary>
        /// 消息
        /// </summary>
        public Message ChargeBillRequestMessage
        {
            get { return message; }
            set { this.message = value; }
        }


        /// <summary>
        /// 账单查询内部消息对象实体message内部类
        /// </summary>
        [DataContract]
        public class Message 
        {
            private static readonly long serialVersionUID = 1L;

            /// <summary>
            /// 消息头部
            /// </summary>
            [DataMember(Name = "head")]
            private Head head;

            /// <summary>
            /// 消息体
            /// </summary>
            [DataMember(Name = "info")]
            private Info info;

            public override string ToString()
            {
                return "ChargeBillRequest.Message[head=" + head.ToString() + ",info=" + info.ToString() + "]";
            }
            
            /// <summary>
            /// 消息头部
            /// </summary>
            public Head ChargeBillRequestHead
            {
                get { return head; }
                set { this.head = value; }
            }

            /// <summary>
            /// 消息体
            /// </summary>
            public Info ChargeBillRequestInfo
            {
                get { return info; }
                set { this.info = value; }
            }

            /// <summary>
            ///  message子对象head消息头内部类
            /// </summary>
            [DataContract]
            public class Head 
            {
                private static readonly long serialVersionUID = 1L;

                /// <summary>
                /// 渠道编码
                /// </summary>
                [DataMember(Name = "channel")]
                private string channel;

                /// <summary>
                /// 交易码
                /// </summary>
                [DataMember(Name = "transCode")]
                private string transCode;

                /// <summary>
                /// 交易上行下送标志位
                /// </summary>
                [DataMember(Name = "transFlag")]
                private string transFlag;

                /// <summary>
                /// 缴费中心交易序列号
                /// </summary>
                [DataMember(Name = "transSeqNum")]
                private string transSeqNum;

                /// <summary>
                /// 时间戳
                /// </summary>
                [DataMember(Name = "timestamp")]
                private string timeStamp;

                /// <summary>
                /// 4为分行iGoal码
                /// </summary>
                [DataMember(Name = "branchCode")]
                private string branchCode;

                public override string ToString()
                {
                    return "ChargeBillRequest.Message.Head[channel=" + channel + ",transCode=" + transCode +
                                        ",transFlag=" + transFlag + ",transSeqNum=" + transSeqNum + ",timestamp=" + timeStamp + ",branchCode=" + branchCode + "]";
                }

                /// <summary>
                /// 渠道编码
                /// </summary>
                public string Channel
                {
                    get { return channel; }
                    set { this.channel = value; }
                }

                /// <summary>
                /// 交易上行下送标志位
                /// </summary>
                public string TransFlag
                {
                    get { return transFlag; }
                    set { this.transFlag = value; }
                }

                /// <summary>
                /// 交易码
                /// </summary>
                public string TransCode
                {
                    get { return transCode; }
                    set { this.transCode = value; }
                }

                /// <summary>
                /// 缴费中心交易序列号
                /// </summary>
                public string TransSeqNum
                {
                    get { return transSeqNum; }
                    set { this.transSeqNum = value; }
                }

                /// <summary>
                /// 时间戳
                /// </summary>
                public string TimeStamp
                {
                    get { return timeStamp; }
                    set { this.timeStamp = value; }
                }

                /// <summary>
                /// 4为分行iGoal码
                /// </summary>
                public string BranchCode
                {
                    get { return branchCode; }
                    set { this.branchCode = value; }
                }
            }

            /// <summary>
            /// message子对象info消息实体内部类
            /// </summary>
            [DataContract]
            public class Info 
            {
                private static readonly long serialVersionUID = 1L;

                /// <summary>
                /// 缴费项目编号
                /// </summary>
                [DataMember(Name = "epayCode")]
                private string epayCode;

                /// <summary>
                /// 第三方商户编号
                /// </summary>
                [DataMember(Name = "merchantId")]
                private string merchantId;

                /// <summary>
                /// 缴费中心流水号
                /// </summary>
                [DataMember(Name = "traceNo")]
                private string traceNo;

                /// <summary>
                /// 输入要素1
                /// </summary>
                [DataMember(Name = "input1")]
                private string input1;

                /// <summary>
                /// 输入要素2
                /// </summary>
                [DataMember(Name = "input2")]
                private string input2;

                /// <summary>
                /// 输入要素3
                /// </summary>
                [DataMember(Name = "input3")]
                private string input3;

                /// <summary>
                /// 输入要素4
                /// </summary>
                [DataMember(Name = "input4")]
                private string input4;

                /// <summary>
                /// 输入要素5
                /// </summary>
                [DataMember(Name = "input5")]
                private string input5;

                /// <summary>
                /// 农行16位客户号
                /// </summary>
                [DataMember(Name = "userId")]
                private string userId;

                /// <summary>
                /// 缴费金额计算规则
                /// </summary>
                [DataMember(Name = "amtRule")]
                private string amtRule;

                /// <summary>
                /// 合并支付的子账单数
                /// </summary>
                [DataMember(Name = "payBillCount")]
                private string payBillCount;

                /// <summary>
                /// 合并支付的子账单累加总金额
                /// </summary>
                [DataMember(Name = "payBillAmt")]
                private string payBillAmt;

                /// <summary>
                /// 合并支付的子账单
                /// </summary>
                [DataMember(Name = "payBillNo")]
                private string payBillNo;

                /// <summary>
                /// 套餐名称
                /// </summary>
                [DataMember(Name = "optionName")]
                private string optionName;

                /// <summary>
                /// 套餐金额
                /// </summary>
                [DataMember(Name = "optionAmt")]
                private string optionAmt;

                /// <summary>
                /// 支付方式交易码
                /// </summary>
                [DataMember(Name = "payType")]
                private string payType;

                /// <summary>
                /// 缴费支付账号
                /// </summary>
                [DataMember(Name = "payAcc")]
                private string payAcc;

                /// <summary>
                /// 支付系统流水号
                /// </summary>
                [DataMember(Name = "transPaySeq")]
                private string transPaySeq;

                /// <summary>
                /// 支付系统时间
                /// </summary>
                [DataMember(Name = "transPayTime")]
                private string transPayTime;

                /// <summary>
                /// 会计日期
                /// </summary>
                [DataMember(Name = "settleDate")]
                private string settleDate;

                /// <summary>
                /// 清算模式
                /// </summary>
                [DataMember(Name = "clearType")]
                private string clearType;

                /// <summary>
                /// 缓存域信息
                /// </summary>
                [DataMember(Name = "cacheMem")]
                private string cacheMem;

                /// <summary>
                /// /销账报文重发次数，
                /// 通过此字段识别销账报文是否为重发的，
                /// 0表示首次、1表示重发一次，2表示重发2次，最多重发3次
                /// </summary>
                [DataMember(Name = "resendTimes")]
                private string resendTimes;

                public string ResendTimes
                {
                    get { return resendTimes; }
                    set { resendTimes = value; }
                }

                /// <summary>
                /// 套餐编码
                /// </summary>
                [DataMember(Name = "optionCode")]
                private string optionCode;

                /// <summary>
                /// 支付系统日期
                /// </summary>
                [DataMember(Name = "transDate")]
                private string transDate;
			
			    /// <summary>
                /// 支付系统时间
			    /// </summary>
                [DataMember(Name = "transTime")]
                private string transTime;


                public override string ToString()
                {
                    return "ChargeBillRequest.Message.Info[epayCode=" + epayCode + ",merchantId=" + merchantId +
                        ",input1=" + input1 + ",input2=" + input2 + ",input3=" + input3 +
                        ",input4=" + input4 + ",input5=" + input5 +
                        ",userId=" + userId + ",traceNo=" + traceNo +
                        ",amtRule=" + amtRule + ",payBillCount=" + payBillCount +
                        ",payBillAmt=" + payBillAmt + ",payBillNo=" +
                        ",optionName=" + optionName + ",optionAmt=" + optionAmt +
                        ",payType=" + payType + ",payAcc=" + payAcc +
                        ",transPaySeq=" + transPaySeq + ",transDate=" + transDate + ",transTime=" + transTime +
                        ",settleDate=" + settleDate + ",clearType=" + clearType +
                        ",cacheMem=" + cacheMem + ",resendTimes=" + resendTimes + "]";
                }
                


                /// <summary>
                /// 缴费项目编号
                /// </summary>
                public string EpayCode
                {
                    get { return epayCode; }
                    set { this.epayCode = value; }
                }

                /// <summary>
                /// 第三方商户编号
                /// </summary>
                public string MerchantId
                {
                    get { return merchantId; }
                    set { this.merchantId = value; }
                }

                /// <summary>
                /// 输入要素1
                /// </summary>
                public string Input1
                {
                    get { return input1; }
                    set { this.input1 = value; }
                }

                /// <summary>
                /// 输入要素2
                /// </summary>
                public string Input2
                {
                    get { return input2; }
                    set { this.input2 = value; }
                }

                /// <summary>
                /// 输入要素3
                /// </summary>
                public string Input3
                {
                    get { return input3; }
                    set { this.input3 = value; }
                }

                /// <summary>
                /// 输入要素4
                /// </summary>
                public string Input4
                {
                    get { return input4; }
                    set { this.input4 = value; }
                }

                /// <summary>
                /// 输入要素5
                /// </summary>
                public string Input5
                {
                    get { return input5; }
                    set { this.input5 = value; }
                }

                /// <summary>
                /// 农行16位客户号
                /// </summary>
                public string UserId
                {
                    get { return userId; }
                    set { this.userId = value; }
                }

                /// <summary>
                /// 缴费中心流水号
                /// </summary>
                public string TraceNo
                {
                    get { return traceNo; }
                    set { this.traceNo = value; }
                }

                /// <summary>
                /// 缴费金额计算规则
                /// </summary>
                public string AmtRule
                {
                    get { return amtRule; }
                    set { this.amtRule = value; }
                }

                /// <summary>
                /// 合并支付的子账单数
                /// </summary>
                public string PayBillCount
                {
                    get { return payBillCount; }
                    set { this.payBillCount = value; }
                }

                /// <summary>
                /// 合并支付的子账单累加总金额
                /// </summary>
                public string PayBillAmt
                {
                    get { return payBillAmt; }
                    set { this.payBillAmt = value; }
                }

                /// <summary>
                /// 合并支付的子账单
                /// </summary>
                public string PayBillNo
                {
                    get { return payBillNo; }
                    set { this.payBillNo = value; }
                }

                /// <summary>
                /// 套餐名称
                /// </summary>
                public string OptionName
                {
                    get { return optionName; }
                    set { this.optionName = value; }
                }

                /// <summary>
                /// 套餐金额
                /// </summary>
                public string OptionAmt
                {
                    get { return optionAmt; }
                    set { this.optionAmt = value; }
                }

                /// <summary>
                /// 支付方式交易码
                /// </summary>
                public string PayType
                {
                    get { return payType; }
                    set { this.payType = value; }
                }

                /// <summary>
                /// 缴费支付账号
                /// </summary>
                public string PayAcc
                {
                    get { return payAcc; }
                    set { this.payAcc = value; }
                }

                /// <summary>
                /// 支付系统流水号
                /// </summary>
                public string TransPaySeq
                {
                    get { return transPaySeq; }
                    set { this.transPaySeq = value; }
                }

                /// <summary>
                /// 支付系统时间
                /// </summary>
                public string TransPayTime
                {
                    get { return transPayTime; }
                    set { this.transPayTime = value; }
                }

                /// <summary>
                /// 会计日期
                /// </summary>
                public string SettleDate
                {
                    get { return settleDate; }
                    set { this.settleDate = value; }
                }

                /// <summary>
                /// 清算模式
                /// </summary>
                public string ClearType
                {
                    get { return clearType; }
                    set { this.clearType = value; }
                }

                /// <summary>
                /// 缓存域信息
                /// </summary>
                public string CacheMem
                {
                    get { return cacheMem; }
                    set { this.cacheMem = value; }
                }
                /// <summary>
                /// 套餐编码
                /// </summary>
                public string OptionCode 
                {
                    get { return optionCode; }
                    set { this.optionCode = value; }
                }

                /// <summary>
                /// 支付系统日期
                /// </summary>
                public string TransDate
                {
                    get { return transDate; }
                    set { this.transDate = value; }
                }

                /// <summary>
                /// 支付系统时间
                /// </summary>
                public string TransTime
                {
                    get { return transTime; }
                    set { this.transTime = value; }
                }
            }
        }
    }
}
