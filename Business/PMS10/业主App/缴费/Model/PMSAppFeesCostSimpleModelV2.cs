using System.Collections.Generic;

namespace Business.PMS10.业主App.缴费.Model
{
    public class PMSAppFeesCostSimpleModelV2
    {
        public long CostID { get; set; }

        public string CostName { get; set; }

        public string SysCostSign { get; set; }

        public decimal TotalDueAmount { get; set; }

        public decimal TotalDebtsAmount { get; set; }

        public decimal TotalLateFeeAmount { get; set; }

        public decimal TotalPaidAmount { get; set; }

        public decimal TotalPrecAmount { get; set; }

        public List<PMSAppFeesSimpleModelV2> Fees { get; set; }

        /// <summary>
        /// 是否缴纳违约金
        /// </summary>
        public int LateFeeControl = 1;

        /// <summary>
        /// 是否默认展开
        /// </summary>
        public int Expanded = 0;

        /// <summary>
        /// 是否默认选中
        /// </summary>
        public int Selected = 0;

        /// <summary>
        /// 缴费模式
        /// </summary>
        public string Mode = "MONTH";

        /// <summary>
        /// 必须交往期
        /// </summary>
        public int MustSelectPreviousPeriod = 1;

        /// <summary>
        /// 绑定费项
        /// </summary>
        public int[] BindingCosts { get; set; }
    }
}
