using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Mt
{
    public class Tb_Mt_PurchasePlan
    {
        public Tb_Mt_PurchasePlan()
        { }
        #region Model
        private string _id;
        private string _planorgancode;
        private string _plannum;
        private DateTime _plandate;
        private string _planyearmonth;
        private string _depcode;
        private string _usercode;
        private string _memo;
        private string _state;
        private string _attachfile;
        /// <summary>
        /// 采购计划单
        /// </summary>
        public string Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 计划单位
        /// </summary>
        public string PlanOrganCode
        {
            set { _planorgancode = value; }
            get { return _planorgancode; }
        }
        /// <summary>
        /// 计划单编号
        /// </summary>
        public string PlanNum
        {
            set { _plannum = value; }
            get { return _plannum; }
        }
        /// <summary>
        /// 编制日期
        /// </summary>
        public DateTime PlanDate
        {
            set { _plandate = value; }
            get { return _plandate; }
        }
        /// <summary>
        /// 计划年月(成本预算系统需要)
        /// </summary>
        public string PlanYearMonth
        {
            set { _planyearmonth = value; }
            get { return _planyearmonth; }
        }
        /// <summary>
        /// 编制部门
        /// </summary>
        public string DepCode
        {
            set { _depcode = value; }
            get { return _depcode; }
        }
        /// <summary>
        /// 编制人
        /// </summary>
        public string UserCode
        {
            set { _usercode = value; }
            get { return _usercode; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Memo
        {
            set { _memo = value; }
            get { return _memo; }
        }
        /// <summary>
        /// 计划状态(根据审核是否完成回调写入)
        /// </summary>
        public string State
        {
            set { _state = value; }
            get { return _state; }
        }
        /// <summary>
        /// 相关附件
        /// </summary>
        public string AttachFile
        {
            set { _attachfile = value; }
            get { return _attachfile; }
        }
        #endregion Model
    }
}
