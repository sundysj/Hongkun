using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.MJModel
{
  public  class NC_SendOrder:NC_OrderCommon
    {

        private string _feesid;

        private int _feesType;

        private string _pk_faretypeid;


        /// <summary>
        /// NC应收单ID
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
        /// 费用编号
        /// </summary>
        public string pk_faretypeid
        {
            get
            {
                return _pk_faretypeid;
            }

            set
            {
                _pk_faretypeid = value;
            }
        }
     

    }
}
