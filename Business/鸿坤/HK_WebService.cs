using System.Xml.Serialization;
using System.Web.Services;
using System.ComponentModel;
using System.Web.Services.Protocols;
using System;
using System.Diagnostics;

namespace Business
{
  
  using System.Xml.Serialization;
    using System.Web.Services;
    using System.ComponentModel;
    using System.Web.Services.Protocols;
    using System;
    using System.Diagnostics;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "0.0.0.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "hkserviceImplServiceSoapBinding", Namespace = "http://service.hk.huayunworld.com/")]
    public partial class hkserviceImplService : System.Web.Services.Protocols.SoapHttpClientProtocol
    {

        private System.Threading.SendOrPostCallback workOrderInfoSynchronizeOperationCompleted;

        private System.Threading.SendOrPostCallback customInfoSynchronizeOperationCompleted;

        private System.Threading.SendOrPostCallback roomInfoSynchronizeOperationCompleted;

        private System.Threading.SendOrPostCallback workOrderStatusSynchronizeOperationCompleted;

        private System.Threading.SendOrPostCallback familyMemberSynchronizeOperationCompleted;

        private System.Threading.SendOrPostCallback workFollowSynchronizeOperationCompleted;

        /// <remarks/>
        public hkserviceImplService()
        {
            this.Url = "http://callcenter.hongkun.com.cn/HK/service/hk";
        }

        /// <remarks/>
        public event workOrderInfoSynchronizeCompletedEventHandler workOrderInfoSynchronizeCompleted;

        /// <remarks/>
        public event customInfoSynchronizeCompletedEventHandler customInfoSynchronizeCompleted;

        /// <remarks/>
        public event roomInfoSynchronizeCompletedEventHandler roomInfoSynchronizeCompleted;

        /// <remarks/>
        public event workOrderStatusSynchronizeCompletedEventHandler workOrderStatusSynchronizeCompleted;

        /// <remarks/>
        public event familyMemberSynchronizeCompletedEventHandler familyMemberSynchronizeCompleted;

        /// <remarks/>
        public event workFollowSynchronizeCompletedEventHandler workFollowSynchronizeCompleted;

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "http://service.hk.huayunworld.com/", ResponseNamespace = "http://service.hk.huayunworld.com/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("return", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string workOrderInfoSynchronize([System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)] string arg0)
        {
            object[] results = this.Invoke("workOrderInfoSynchronize", new object[] {
                        arg0});
            return ((string)(results[0]));
        }

        /// <remarks/>
        public void workOrderInfoSynchronizeAsync(string arg0)
        {
            this.workOrderInfoSynchronizeAsync(arg0, null);
        }

        /// <remarks/>
        public void workOrderInfoSynchronizeAsync(string arg0, object userState)
        {
            if ((this.workOrderInfoSynchronizeOperationCompleted == null))
            {
                this.workOrderInfoSynchronizeOperationCompleted = new System.Threading.SendOrPostCallback(this.OnworkOrderInfoSynchronizeOperationCompleted);
            }
            this.InvokeAsync("workOrderInfoSynchronize", new object[] {
                        arg0}, this.workOrderInfoSynchronizeOperationCompleted, userState);
        }

        private void OnworkOrderInfoSynchronizeOperationCompleted(object arg)
        {
            if ((this.workOrderInfoSynchronizeCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.workOrderInfoSynchronizeCompleted(this, new workOrderInfoSynchronizeCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "http://service.hk.huayunworld.com/", ResponseNamespace = "http://service.hk.huayunworld.com/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("return", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string customInfoSynchronize([System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)] string arg0)
        {
            object[] results = this.Invoke("customInfoSynchronize", new object[] {
                        arg0});
            return ((string)(results[0]));
        }

        /// <remarks/>
        public void customInfoSynchronizeAsync(string arg0)
        {
            this.customInfoSynchronizeAsync(arg0, null);
        }

        /// <remarks/>
        public void customInfoSynchronizeAsync(string arg0, object userState)
        {
            if ((this.customInfoSynchronizeOperationCompleted == null))
            {
                this.customInfoSynchronizeOperationCompleted = new System.Threading.SendOrPostCallback(this.OncustomInfoSynchronizeOperationCompleted);
            }
            this.InvokeAsync("customInfoSynchronize", new object[] {
                        arg0}, this.customInfoSynchronizeOperationCompleted, userState);
        }

        private void OncustomInfoSynchronizeOperationCompleted(object arg)
        {
            if ((this.customInfoSynchronizeCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.customInfoSynchronizeCompleted(this, new customInfoSynchronizeCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "http://service.hk.huayunworld.com/", ResponseNamespace = "http://service.hk.huayunworld.com/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("return", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string roomInfoSynchronize([System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)] string arg0)
        {
            object[] results = this.Invoke("roomInfoSynchronize", new object[] {
                        arg0});
            return ((string)(results[0]));
        }

        /// <remarks/>
        public void roomInfoSynchronizeAsync(string arg0)
        {
            this.roomInfoSynchronizeAsync(arg0, null);
        }

        /// <remarks/>
        public void roomInfoSynchronizeAsync(string arg0, object userState)
        {
            if ((this.roomInfoSynchronizeOperationCompleted == null))
            {
                this.roomInfoSynchronizeOperationCompleted = new System.Threading.SendOrPostCallback(this.OnroomInfoSynchronizeOperationCompleted);
            }
            this.InvokeAsync("roomInfoSynchronize", new object[] {
                        arg0}, this.roomInfoSynchronizeOperationCompleted, userState);
        }

        private void OnroomInfoSynchronizeOperationCompleted(object arg)
        {
            if ((this.roomInfoSynchronizeCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.roomInfoSynchronizeCompleted(this, new roomInfoSynchronizeCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "http://service.hk.huayunworld.com/", ResponseNamespace = "http://service.hk.huayunworld.com/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("return", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string workOrderStatusSynchronize([System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)] string arg0)
        {
            object[] results = this.Invoke("workOrderStatusSynchronize", new object[] {
                        arg0});
            return ((string)(results[0]));
        }

        /// <remarks/>
        public void workOrderStatusSynchronizeAsync(string arg0)
        {
            this.workOrderStatusSynchronizeAsync(arg0, null);
        }

        /// <remarks/>
        public void workOrderStatusSynchronizeAsync(string arg0, object userState)
        {
            if ((this.workOrderStatusSynchronizeOperationCompleted == null))
            {
                this.workOrderStatusSynchronizeOperationCompleted = new System.Threading.SendOrPostCallback(this.OnworkOrderStatusSynchronizeOperationCompleted);
            }
            this.InvokeAsync("workOrderStatusSynchronize", new object[] {
                        arg0}, this.workOrderStatusSynchronizeOperationCompleted, userState);
        }

        private void OnworkOrderStatusSynchronizeOperationCompleted(object arg)
        {
            if ((this.workOrderStatusSynchronizeCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.workOrderStatusSynchronizeCompleted(this, new workOrderStatusSynchronizeCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "http://service.hk.huayunworld.com/", ResponseNamespace = "http://service.hk.huayunworld.com/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("return", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string familyMemberSynchronize([System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)] string arg0)
        {
            object[] results = this.Invoke("familyMemberSynchronize", new object[] {
                        arg0});
            return ((string)(results[0]));
        }

        /// <remarks/>
        public void familyMemberSynchronizeAsync(string arg0)
        {
            this.familyMemberSynchronizeAsync(arg0, null);
        }

        /// <remarks/>
        public void familyMemberSynchronizeAsync(string arg0, object userState)
        {
            if ((this.familyMemberSynchronizeOperationCompleted == null))
            {
                this.familyMemberSynchronizeOperationCompleted = new System.Threading.SendOrPostCallback(this.OnfamilyMemberSynchronizeOperationCompleted);
            }
            this.InvokeAsync("familyMemberSynchronize", new object[] {
                        arg0}, this.familyMemberSynchronizeOperationCompleted, userState);
        }

        private void OnfamilyMemberSynchronizeOperationCompleted(object arg)
        {
            if ((this.familyMemberSynchronizeCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.familyMemberSynchronizeCompleted(this, new familyMemberSynchronizeCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "http://service.hk.huayunworld.com/", ResponseNamespace = "http://service.hk.huayunworld.com/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("return", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string workFollowSynchronize([System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)] string arg0)
        {
            object[] results = this.Invoke("workFollowSynchronize", new object[] {
                        arg0});
            return ((string)(results[0]));
        }

        /// <remarks/>
        public void workFollowSynchronizeAsync(string arg0)
        {
            this.workFollowSynchronizeAsync(arg0, null);
        }

        /// <remarks/>
        public void workFollowSynchronizeAsync(string arg0, object userState)
        {
            if ((this.workFollowSynchronizeOperationCompleted == null))
            {
                this.workFollowSynchronizeOperationCompleted = new System.Threading.SendOrPostCallback(this.OnworkFollowSynchronizeOperationCompleted);
            }
            this.InvokeAsync("workFollowSynchronize", new object[] {
                        arg0}, this.workFollowSynchronizeOperationCompleted, userState);
        }

        private void OnworkFollowSynchronizeOperationCompleted(object arg)
        {
            if ((this.workFollowSynchronizeCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.workFollowSynchronizeCompleted(this, new workFollowSynchronizeCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        public new void CancelAsync(object userState)
        {
            base.CancelAsync(userState);
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "0.0.0.0")]
    public delegate void workOrderInfoSynchronizeCompletedEventHandler(object sender, workOrderInfoSynchronizeCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "0.0.0.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class workOrderInfoSynchronizeCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal workOrderInfoSynchronizeCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "0.0.0.0")]
    public delegate void customInfoSynchronizeCompletedEventHandler(object sender, customInfoSynchronizeCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "0.0.0.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class customInfoSynchronizeCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal customInfoSynchronizeCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "0.0.0.0")]
    public delegate void roomInfoSynchronizeCompletedEventHandler(object sender, roomInfoSynchronizeCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "0.0.0.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class roomInfoSynchronizeCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal roomInfoSynchronizeCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "0.0.0.0")]
    public delegate void workOrderStatusSynchronizeCompletedEventHandler(object sender, workOrderStatusSynchronizeCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "0.0.0.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class workOrderStatusSynchronizeCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal workOrderStatusSynchronizeCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "0.0.0.0")]
    public delegate void familyMemberSynchronizeCompletedEventHandler(object sender, familyMemberSynchronizeCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "0.0.0.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class familyMemberSynchronizeCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal familyMemberSynchronizeCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "0.0.0.0")]
    public delegate void workFollowSynchronizeCompletedEventHandler(object sender, workFollowSynchronizeCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "0.0.0.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class workFollowSynchronizeCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal workFollowSynchronizeCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
}
