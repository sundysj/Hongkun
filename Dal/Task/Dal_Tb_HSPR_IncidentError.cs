using MobileSoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text; 
namespace Dal.Task
{
    public class Dal_Tb_HSPR_IncidentError
    {
        public Dal_Tb_HSPR_IncidentError()
        {
            DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
        }

        /// <summary>
        ///  增加一条数据
        /// </summary>
        public void Add(Model.Task.Tb_HSPR_IncidentError model)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
                    new SqlParameter("@State", SqlDbType.NVarChar),
                    new SqlParameter("@ErrorContent", SqlDbType.Text),
                    new SqlParameter("@Method", SqlDbType.Text),
                    new SqlParameter("@ErrorDate", SqlDbType.DateTime),
                    new SqlParameter("@Parameter", SqlDbType.Text)
            };
            parameters[0].Value = model.State;
            parameters[1].Value = model.ErrorContent;
            parameters[2].Value = model.Method;
            parameters[3].Value = model.ErrorDate;
            parameters[4].Value = model.Parameter;

            DbHelperSQL.RunProcedure("Proc_Tb_HSPR_IncidentError_ADD", parameters, out rowsAffected);
        }

    }
}
