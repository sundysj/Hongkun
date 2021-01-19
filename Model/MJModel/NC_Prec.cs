using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.MJModel
{
  public  class NC_Prec
    {
        private string _schemeid;

        private double _price;

        private string _schemename;
        /// <summary>
        /// 方案ID
        /// </summary>
        public string schemeid
        {
            get
            {
                return _schemeid;
            }

            set
            {
                _schemeid = value;
            }
        }
        /// <summary>
        ///  方案金额
        /// </summary>
        public double price
        {
            get
            {
                return _price;
            }

            set
            {
                _price = value;
            }
        }

        /// <summary>
        /// 方案名称
        /// </summary>
        public string schemename
        {
            get
            {
                return _schemename;
            }

            set
            {
                _schemename = value;
            }
        }
    }
}
