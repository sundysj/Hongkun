using MobileSoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileSoft.DAL.Sys
{
    public class Dal_Tb_Sys_TakePicIncidentTypeDeskTop
    {
        public DataSet GetList(string OrganCode, int CommID)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@OrganCode", SqlDbType.VarChar),
                new SqlParameter("@CommID", SqlDbType.Int)
            };
            parameters[0].Value = OrganCode;
            parameters[1].Value = CommID;

            DataSet Ds = DbHelperSQL.RunProcedure("Proc_Sys_TakePicIncidentTypeDeskTop_Filter", parameters, "RetDataSet");

            return Ds;
        }
    }
}
