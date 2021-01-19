using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model.Pay
{
    /// <summary>
    /// 直连商户平台账单销账返回对象
    /// </summary>
    [DataContract]
    public class ChargeBillResponse
    {
        private static readonly long serialVersionUID = 1L;

        /// <summary>
        /// 格式
        /// </summary>
        [DataMember(Name = "format")]
        private string format;

        /// <summary>
        /// 消息体
        /// </summary>
        [DataMember(Name = "message")]
        private Message message;


        public ChargeBillResponse() { }
        
        /// <summary>
        /// 构造函数，通过输入对象，构造返回对象数据信息
        /// </summary>
        /// <param name="request">@param request</param>
        public ChargeBillResponse(ChargeBillRequest request)
        {
            this.ChargeBillResponseFormat = request.ChargeBillRequestFormat;
            this.ChargeBillResponseMessage = new Message(request.ChargeBillRequestMessage);
        }

        public override string ToString()
        {
            return "ChargeBillResponse[format=" + format + ",message=" + message == null ? "" : message.ToString() + "]";
        }

        /// <summary>
        /// 格式
        /// </summary>
        public string ChargeBillResponseFormat
        {
            get { return format; }
            set { this.format = value; }
        }

        /// <summary>
        /// 消息体
        /// </summary>
        public Message ChargeBillResponseMessage
        {
            get { return message; }
            set { this.message = value; }
        }

        /// <summary>
        /// 账单查询内部消息对象返回实体message内部类
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
            /// 消息实体
            /// </summary>
            [DataMember(Name = "info")]
            private Info info;

            public Message()
            {
                this.head = new Head();
                this.info = new Info();
            }

            public Message(ChargeBillRequest.Message requestMessage)
            {
                this.ChargeBillResponseHead = new Head(requestMessage.ChargeBillRequestHead);
                this.ChargeBillResponseInfo = new Info(requestMessage.ChargeBillRequestInfo);
            }

            public override string ToString()
            {
                return "ChargeBillResponse.Message[head=" + head.ToString() + ",info=" + info.ToString() + "]";
            }

            /// <summary>
            /// 消息头部
            /// </summary>
            public Head ChargeBillResponseHead
            {
                get { return head; }
                set { this.head = value; }
            }

            /// <summary>
            /// 消息实体
            /// </summary>
            public Info ChargeBillResponseInfo
            {
                get { return info; }
                set { this.info = value; }
            }

            /// <summary>
            /// 账单销账内部消息对象返回实体Head内部类
            /// </summary>
            [DataContract]
            public class Head
            {
                private static readonly long serialVersionUID = 1L;

                /// <summary>
                /// 渠道
                /// </summary>
                [DataMember(Name = "channel")]
                private string channel;

                /// <summary>
                /// 交易码
                /// </summary>
                [DataMember(Name = "transCode")]
                private string transCode;

                /// <summary>
                /// 交易上行下送标志
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
                [DataMember(Name = "timeStamp")]
                private string timeStamp;

                /// <summary>
                /// 查询返回码
                /// </summary>
                [DataMember(Name = "returnCode")]
                private string returnCode;

                /// <summary>
                /// 返回提示信息
                /// </summary>
                [DataMember(Name = "returnMessage")]
                private string returnMessage;

                public Head() { }
                

                public Head(ChargeBillRequest.Message.Head reqMessHead)
                {
                    this.Channel = reqMessHead.Channel;
                    this.TransSeqNum = reqMessHead.TransSeqNum;
                    this.TransCode = reqMessHead.TransCode;
                }

                public override string ToString()
                {
                    return "ChargeBillResponse.Message.Head[channel=" + channel + ",transCode=" + transCode + ",transSeqNum=" + transSeqNum
                                        + ",timeStamp=" + timeStamp + ",returnCode=" + returnCode + ",returnMessage=" + returnMessage + "]";
                }

                /// <summary>
                /// 渠道
                /// </summary>
                public string Channel
                {
                    get { return channel; }
                    set { this.channel = value; }
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
                /// 交易上行下送标志
                /// </summary>
                public string TransFlag
                {
                    get { return transFlag; }
                    set { this.transFlag = value; }
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
                /// 查询返回码
                /// </summary>
                public string ReturnCode
                {
                    get { return returnCode; }
                    set { this.returnCode = value; }
                }

                /// <summary>
                /// 返回提示信息
                /// </summary>
                public string ReturnMessage
                {
                    get { return returnMessage; }
                    set { this.returnMessage = value; }
                }
            }

            /// <summary>
            /// 账单查询内部消息对象返回实体Info内部类
            /// </summary>
            [DataContract]
            public class Info 
            {
                private static readonly long serialVersionUID = 1L;

                /// <summary>
                /// 缴费项目唯一标识号
                /// </summary>
                [DataMember(Name = "epayCode")]
                private string epayCode;

                /// <summary>
                /// 缴费中心流水号
                /// </summary>
                [DataMember(Name = "traceNo")]
                private string traceNo;

                /// <summary>
                /// 退款标志位
                /// </summary>
                [DataMember(Name = "refundFlag")]
                private string refundFlag;

                public Info() { }

                public Info(ChargeBillRequest.Message.Info reqMessInfo)
                {
                    this.EpayCode = reqMessInfo.EpayCode;
                    this.TraceNo = reqMessInfo.TraceNo;
                }

                public override string ToString()
                {
                    return "ChargeBillResponse.Message.Info[epayCode=" + epayCode + "traceNo=" + traceNo + "refundFlag=" + refundFlag + "]"; ;
                }

                /// <summary>
                /// 缴费项目唯一标识号
                /// </summary>
                public string EpayCode
                {
                    get { return epayCode; }
                    set { this.epayCode = value; }
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
                /// 退款标志位
                /// </summary>
                public string RefundFlag
                {
                    get { return refundFlag; }
                    set { this.refundFlag = value; }
                }
            }
        }
    }
}
