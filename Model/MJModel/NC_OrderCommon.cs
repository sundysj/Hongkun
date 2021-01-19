using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.MJModel
{
   public class NC_OrderCommon
    {
        private string _pk_paytype;

        private string _custid;

        private string _roomid;

        private int _feestype;

        private double _nrevlfmny;

        private double _nrevmny;

        /// <summary>
        /// 固定值
        /// </summary>
        public string pk_paytype
        {
            get
            {
                return _pk_paytype;
            }

            set
            {
                _pk_paytype = value;
            }
        }

        /// <summary>
        /// NC客户编号
        /// </summary>
        public string custid
        {
            get
            {
                return _custid;
            }

            set
            {
                _custid = value;
            }
        }
        /// <summary>
        /// NC房屋编号
        /// </summary>
        public string roomid
        {
            get
            {
                return _roomid;
            }

            set
            {
                _roomid = value;
            }
        }
        /// <summary>
        /// 0实收 1预存
        /// </summary>
        public int feestype
        {
            get
            {
                return _feestype;
            }

            set
            {
                _feestype = value;
            }
        }
        /// <summary>
        /// 滞纳金
        /// </summary>
        public double nrevlfmny
        {
            get
            {
                return _nrevlfmny;
            }

            set
            {
                _nrevlfmny = value;
            }
        }
        /// <summary>
        /// 应收金额
        /// </summary>
        public double nrevmny
        {
            get
            {
                return _nrevmny;
            }

            set
            {
                _nrevmny = value;
            }
        }
    }
}
