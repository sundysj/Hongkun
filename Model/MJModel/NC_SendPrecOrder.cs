using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.MJModel
{
  public  class NC_SendPrecOrder:NC_OrderCommon
    {
        private string _schemeid;

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
    }
}
