using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileSoft.BLL.Sys
{
    public class Bll_Tb_Sys_TakePicIncidentTypeDeskTop
    {
        private readonly MobileSoft.DAL.Sys.Dal_Tb_Sys_TakePicIncidentTypeDeskTop dal = new MobileSoft.DAL.Sys.Dal_Tb_Sys_TakePicIncidentTypeDeskTop();

        public DataSet GetList(string OrganCode, int CommID)
        {
            return dal.GetList(OrganCode, CommID);
        }
    }
}
