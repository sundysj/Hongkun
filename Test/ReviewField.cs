namespace Test
{
    #region 模板ID
    /// <summary>
    /// 模板ID
    /// </summary>
    public static class ReviewTemplate
    {
        /// <summary>
        /// 减免流程模板ID
        /// </summary>
        public static string WaivTemplateId = "154dc3f66fa87c0c215fc6245b39506a";//减免流程ID
        /// <summary>
        /// 票据撤销流程模板ID
        /// </summary>
        public static string ReceCancelTemplateId = "154dc43d42b8fcc9221b94e499289a99";//票据撤销流程ID
        /// <summary>
        /// 退款流程模板ID
        /// </summary>
        public static string RefundTemplateId = "154dc47b7675c0d649eb705492cb8f6d";//退款流程ID
    }
    #endregion

    #region 减免
    /// <summary>
    /// 减免
    /// </summary>
    public static class ReviewWaivField
    {
       
        /// <summary>
        /// 小区
        /// </summary>
        public static string CommName = "fd_3402dbe660716e";
        /// <summary>
        /// 客户
        /// </summary>
        public static string CustName = "fd_3402dbec541464"; 
        /// <summary>
        /// 房号
        /// </summary>
        public static string RoomSign = "fd_3402dbeddc37c4";
        /// <summary>
        /// 费项
        /// </summary>
        public static string CostName = "fd_3402dbee572a2e";
        /// <summary>
        /// 减免方式
        /// </summary>
        public static string WaivType = "fd_3402dbed538224";
        /// <summary>
        /// 单月减免金额
        /// </summary>
        public static string WaivMonthAmount = "fd_3402dbecae5160";
        /// <summary>
        /// 减免比例
        /// </summary>
        public static string WaivRates = "fd_3402dbef194ce4";
        /// <summary>
        /// 减免总金额
        /// </summary>
        public static string WaivAmount = "fd_3402dbeeb15b98";
        /// <summary>
        /// 减免开始时间
        /// </summary>
        public static string WaivStartDuring = "fd_3402dbf01d7252";
        /// <summary>
        /// 减免结束时间
        /// </summary>
        public static string WaivEndDuring = "fd_3402dbef7857f2";
        /// <summary>
        /// 减免原因
        /// </summary>
        public static string WaivReason = "fd_30f8026e49c3be";

    }
    #endregion

    #region 票据撤销
    /// <summary>
    /// 票据撤销
    /// </summary>
    public static class ReviewReceCancelField
    {
        
        /// <summary>
        /// 小区
        /// </summary>
        public static string CommName = "fd_3402dbe660716e";
        /// <summary>
        /// 客户
        /// </summary>
        public static string CustName = "fd_3402dbec541464";
        /// <summary>
        /// 房号
        /// </summary>
        public static string RoomSign = "fd_3402dbeddc37c4";
        /// <summary>
        /// 费项
        /// </summary>
        public static string CostName = "fd_3402dbee572a2e";
        /// <summary>
        /// 费用开始时间
        /// </summary>
        public static string FeesStartDate = "fd_3402dbed538224";
        /// <summary>
        /// 费用结束时间
        /// </summary>
        public static string FeesEndDate = "fd_3402dbecae5160";
        /// <summary>
        /// 金额
        /// </summary>
        public static string PaidAmount = "fd_3402dbef194ce4";
        /// <summary>
        /// 收款时间
        /// </summary>
        public static string ChargeDate = "fd_3402dbeeb15b98";
        /// <summary>
        /// 票据号
        /// </summary>
        public static string BillsSign = "fd_3402dbf01d7252";
        /// <summary>
        /// 撤销原因
        /// </summary>
        public static string CancelReason = "fd_30f8026e49c3be";
       
    }
    #endregion

    #region 退款
    /// <summary>
    /// 退款
    /// </summary>
    public static class ReviewRefundField
    {
       
        /// <summary>
        /// 合计金额
        /// </summary>
        public static string TotalAmount = "fd_3402dd25b522f2";

        /// <summary>
        /// 明细表ID
        /// </summary>
        public static string DetailTable = "fd_3402dcd99292fe";

        /// <summary>
        /// 小区
        /// </summary>
        public static string CommName = DetailTable + "."+ "fd_3402dd20a4c392";
        /// <summary>
        /// 客户
        /// </summary>
        public static string CustName = DetailTable + "." + "fd_3402dd20ee06e4";
        /// <summary>
        /// 房号
        /// </summary>
        public static string RoomSign = DetailTable + "." + "fd_3402dd2137d4ae";
        /// <summary>
        /// 费项
        /// </summary>
        public static string CostName = DetailTable + "." + "fd_3402dd21807bb4";
        /// <summary>
        /// 费用开始时间
        /// </summary>
        public static string FeesStartDate = DetailTable + "." + "fd_3402dd21cb0eb0";
        /// <summary>
        /// 费用结束时间
        /// </summary>
        public static string FeesEndDate = DetailTable + "." + "fd_3402dd2219676e";
        /// <summary>
        /// 金额
        /// </summary>
        public static string RefundAmount = DetailTable + "." + "fd_3402dd2268a312";       
        /// <summary>
        /// 退款原因
        /// </summary>
        public static string RefundReason = DetailTable + "." + "fd_3402dd9c9dfa70";

    }
    #endregion
}
