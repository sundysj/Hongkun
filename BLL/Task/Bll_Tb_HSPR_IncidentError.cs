using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Task
{

    public class Bll_Tb_HSPR_IncidentError
    {
        private readonly Dal.Task.Dal_Tb_HSPR_IncidentError dal = new Dal.Task.Dal_Tb_HSPR_IncidentError();

        public void Add(Model.Task.Tb_HSPR_IncidentError model)
        {
            dal.Add(model);
        }
    }
}
