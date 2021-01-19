using MobileSoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileSoft.DAL.HSPR
{
    public class Dal_Tb_HSPR_TempIncidentWarning
    {
        public DataSet GetList(string UserCode, int CommID)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@usercode", SqlDbType.VarChar),
                new SqlParameter("@commid", SqlDbType.Int)
            };
            parameters[0].Value = UserCode;
            parameters[1].Value = CommID;

            DataSet Ds = DbHelperSQL.RunProcedure("Proc_HSPR_IncidentWarningCount1_Filter", parameters, "RetDataSet");

            return Ds;
        }
    }
}
