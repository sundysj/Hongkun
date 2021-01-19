using System;

namespace Business.PMS10.业主App.缴费.Model
{
    /// <summary>
    /// Tb_HSPR_IncidentControlSet的模型
    /// </summary>
    [Serializable()]
    public class Tb_HSPR_PaymentBindingDateModelSet
    {
        private string iID;

        private int commID;
        private string commName;
        private string organCode;
        private int checkbox_1;
        private int checkbox_2;
        private int checkbox_3;
        private int checkbox_4;
        private int checkbox_5;
        private int checkbox_6;
        private int checkbox_7;
        private int checkbox_8;
        private int checkbox_9;
        private int checkbox_10;
        private int checkbox_11;
        private int checkbox_12;
        private int checkbox_13;
        private int monthNum;

        public Tb_HSPR_PaymentBindingDateModelSet() { }

        public string IID
        {
            get { return this.iID; }
            set { this.iID = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int CommID
        {
            get { return this.commID; }
            set { this.commID = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string CommName
        {
            get { return this.commName; }
            set { this.commName = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string OrganCode
        {
            get { return this.organCode; }
            set { this.organCode = value; }
        }

        /// <summary>
        /// 全部费用一次缴纳
        /// </summary>
        public int Checkbox_1
        {
            get { return this.checkbox_1; }
            set { this.checkbox_1 = value; }
        }

        /// <summary>
        /// 单项费用任意缴纳
        /// </summary>
        public int Checkbox_2
        {
            get { return this.checkbox_2; }
            set { this.checkbox_2 = value; }
        }

        /// <summary>
        /// 单项费用任意缴纳,同时缴纳历史欠费
        /// </summary>
        public int Checkbox_3
        {
            get { return this.checkbox_3; }
            set { this.checkbox_3 = value; }
        }

        /// <summary>
        /// 按照费项绑定设置，整月费用一次缴纳
        /// </summary>
        public int Checkbox_4
        {
            get { return this.checkbox_4; }
            set { this.checkbox_4 = value; }
        }

        /// <summary>
        /// 按照费项绑定设置，整月费用一次缴纳,同时缴纳历史欠费
        /// </summary>
        public int Checkbox_5
        {
            get { return this.checkbox_5; }
            set { this.checkbox_5 = value; }
        }

        /// <summary>
        /// 按照费项绑定设置，整季费用一次缴纳
        /// </summary>
        public int Checkbox_6
        {
            get { return this.checkbox_6; }
            set { this.checkbox_6 = value; }
        }

        /// <summary>
        /// 按照费项绑定设置，整季费用一次缴纳,同时缴纳历史欠费
        /// </summary>
        public int Checkbox_7
        {
            get { return this.checkbox_7; }
            set { this.checkbox_7 = value; }
        }

        /// <summary>
        /// 按照费项绑定设置，半年费用一次缴纳
        /// </summary>
        public int Checkbox_8
        {
            get { return this.checkbox_8; }
            set { this.checkbox_8 = value; }
        }

        /// <summary>
        /// 按照费项绑定设置，半年费用一次缴纳,同时缴纳历史欠费
        /// </summary>
        public int Checkbox_9
        {
            get { return this.checkbox_9; }
            set { this.checkbox_9 = value; }
        }

        /// <summary>
        /// 按照费项绑定设置，整年费用一次缴纳
        /// </summary>
        public int Checkbox_10
        {
            get { return this.checkbox_10; }
            set { this.checkbox_10 = value; }
        }

        /// <summary>
        /// 按照费项绑定设置，整年费用一次缴纳,同时缴纳历史欠费
        /// </summary>
        public int Checkbox_11
        {
            get { return this.checkbox_11; }
            set { this.checkbox_11 = value; }
        }

        /// <summary>
        /// 按照费项绑定设置，往后缴纳 X 月费用
        /// </summary>
        public int Checkbox_12
        {
            get { return this.checkbox_12; }
            set { this.checkbox_12 = value; }
        }

        /// <summary>
        /// 按照费项绑定设置，往后缴纳 X 月费用,同时缴纳历史欠费
        /// </summary>
        public int Checkbox_13
        {
            get { return this.checkbox_13; }
            set { this.checkbox_13 = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int MonthNum
        {
            get { return this.monthNum; }
            set { this.monthNum = value; }
        }

    }
}
