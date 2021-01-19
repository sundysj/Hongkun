using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model.Pay
{
    [DataContract]
    public class QueryBillRequest 
    {
        private static readonly long serialVersionUID = 1L;

        /** 格式 */
        [DataMember(Name = "format")]
        private string format;

        /** 消息 */
        [DataMember(Name = "message")]
        private Message message;

        public override string ToString()
        {
            return "QueryBillRequest[format=" + format + ",message=" + message.ToString() + "]";
        }

        public string QueryBillRequestFormat
        {
            get { return format; }
            set { this.format = value; }
        }

        public Message QueryBillRequestMessage
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

            /** 消息头部 */
            [DataMember(Name = "head")]
            private Head head;

            /** 消息体  */
            [DataMember(Name = "info")]
            private Info info;

            public override string ToString()
            {
                return "QueryBillRequest.Message[head=" + head.ToString() + ",info=" + info.ToString() + "]";
            }

            public Head QueryBillRequestHead
            {
                get { return head; }
                set { this.head = value; }
            }

            public Info QueryBillRequestInfo
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

                /**  渠道编码 */
                [DataMember(Name = "channel")]
                private string channel;

                /**  交易码  */
                [DataMember(Name = "transCode")]
                private string transCode;

                /**  交易上行下送标志位  */
                [DataMember(Name = "transFlag")]
                private string transFlag;

                /**  缴费中心交易序列号 */
                [DataMember(Name = "transSeqNum")]
                private string transSeqNum;

                /**   时间戳  */
                [DataMember(Name = "timeStamp")]
                private string timeStamp;

                /**   4为分行iGoal码  */
                [DataMember(Name = "branchCode")]
                private string branchCode;

                public override string ToString()
                {
                    return "QueryBillRequest.Message.Head[channel=" + channel + ",transCode=" + transCode +
                                        ",transFlag=" + transFlag + ",transSeqNum=" + transSeqNum + ",timestamp=" + timeStamp + ",branchCode=" + branchCode + "]";
                }

                public string Channel
                {
                    get { return channel; }
                    set { this.channel = value; }
                }


                public string TransFlag
                {
                    get { return transFlag; }
                    set { this.transFlag = value; }
                }

                public string TransCode
                {
                    get { return transCode; }
                    set { this.transCode = value; }
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

                /** 缴费项目编号*/
                [DataMember(Name = "epayCode")]
                private string epayCode;

                /** 第三方商户编号*/
                [DataMember(Name = "merchantId")]
                private string merchantId;

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

                /** 农行16位客户号*/
                [DataMember(Name = "userId")]
                private string userId;

                /** 缴费中心流水号*/
                [DataMember(Name = "traceNo")]
                private string traceNo;

                public override string ToString()
                {
                    return "QueryBillRequest.Message.Info[epayCode=" + epayCode + ",merchantId=" + merchantId +
                                        ",input1=" + input1 + ",input2=" + input2 + ",input3=" + input3 + ",input4=" + input4 + ",input5=" + input5 +
                                        ",userId=" + userId + ",traceNo=" + traceNo + "]";
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

                public string UserId
                {
                    get { return userId; }
                    set { this.userId = value; }
                }

                public string TraceNo
                {
                    get { return traceNo; }
                    set { this.traceNo = value; }
                }
            }
        }
    }
}
