using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace MobileSoft.BLL.HSPR
{
    public class Bll_Tb_HSPR_TempIncidentWarning
    {
        private readonly MobileSoft.DAL.HSPR.Dal_Tb_HSPR_TempIncidentWarning dal = new MobileSoft.DAL.HSPR.Dal_Tb_HSPR_TempIncidentWarning();

        public DataSet GetList(string UserCode, int CommID)
        {
            return dal.GetList(UserCode, CommID);
        }
    }
}
