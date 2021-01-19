using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.MJModel
{
  public  class NC_Bill
    {
        private string _feesid;

        private string _costcode;

        private string _costname;

        private double _dueamount;

        private double _waivamount;

        private double _precamount;

        private string _feesduedate;

        private string _feesstartdate;

        private string _feesenddate;
        /// <summary>
        /// 费用ID
        /// </summary>
        public string feesid
        {
            get
            {
                return _feesid;
            }

            set
            {
                _feesid = value;
            }
        }
        /// <summary>
        /// 费项编码
        /// </summary>
        public string costcode
        {
            get
            {
                return _costcode;
            }

            set
            {
                _costcode = value;
            }
        }
        /// <summary>
        /// 费项名称
        /// </summary>
        public string costname
        {
            get
            {
                return _costname;
            }

            set
            {
                _costname = value;
            }
        }
        /// <summary>
        /// 应收本金
        /// </summary>
        public double dueamount
        {
            get
            {
                return _dueamount;
            }

            set
            {
                _dueamount = value;
            }
        }
        /// <summary>
        /// 待缴本金
        /// </summary>
        public double waivamount
        {
            get
            {
                return _waivamount;
            }

            set
            {
                _waivamount = value;
            }
        }
        /// <summary>
        /// 待缴滞纳金
        /// </summary>
        public double precamount
        {
            get
            {
                return _precamount;
            }

            set
            {
                _precamount = value;
            }
        }
        /// <summary>
        /// 会计月
        /// </summary>
        public string feesduedate
        {
            get
            {
                return _feesduedate;
            }

            set
            {
                _feesduedate = value;
            }
        }
        /// <summary>
        /// 费用开始时间
        /// </summary>
        public string feesstartdate
        {
            get
            {
                return _feesstartdate;
            }

            set
            {
                _feesstartdate = value;
            }
        }
        /// <summary>
        /// 费用结束时间 
        /// </summary>
        public string feesenddate
        {
            get
            {
                return _feesenddate;
            }

            set
            {
                _feesenddate = value;
            }
        }
    }
}
