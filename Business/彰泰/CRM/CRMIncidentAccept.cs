using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using MobileSoft.Common;
using System.Data;
using MobileSoft.DBUtility;
using System.Data.SqlClient;
using Dapper;
using Model.HSPR;
using KernelDev.DataAccess;
using MobileSoft.Model.HSPR;
#pragma warning disable CS0105 // “Dapper”的 using 指令以前在此命名空间中出现过
using Dapper;
#pragma warning restore CS0105 // “Dapper”的 using 指令以前在此命名空间中出现过

namespace Business 
{
    public class    CRMIncidentAccept : PubInfo
    {
        //构建业务主库链接字符串 
        string ContionString_Base = PubConstant.GetConnectionString("ZTWYConnectionString").ToString();
        Tb_HSPR_IncidentError log = new Tb_HSPR_IncidentError(); //写入数据库日志

        public CRMIncidentAccept()
        {
            base.Token = "182C8632FAC4840C9A67A5842B0B6A0E";
        }

        public override void Operate(ref Transfer Trans)
        {
            Trans.Result = JSONHelper.FromString(false, "未知错误");
            DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
            DataRow Row = dAttributeTable.Rows[0];



            log.Parameter = Trans.Attribute;
            log.Method = "CRMIncidentAccept-" + Trans.Command;
            log.ErrorDate = DateTime.Now;

            switch (Trans.Command)
            {
         
                #region CRM 所需
                case "PushIncidentCRM":
                    Trans.Result = PushIncidentCRM(Row);//接受来自ERP  APP 需推送到CMR
                    break; 
                #endregion

                default:
                    Trans.Result = "";
                    break;
            }
        }

        private string PushIncidentCRM(DataRow Row) {

            //参数： incidentid, typename
            //type: 
            //
            // 0：受理  1:分派、2:处理、3.转派 4:关闭、5:退回（备注：关闭退回处理）  6:回访、  7:作废、
             
            string backstr = "";
            try
            {  
                string type = "";
                string incidentid = "";
                if (!Row.Table.Columns.Contains("IncidentID") || string.IsNullOrEmpty(Row["IncidentID"].ToString()))
                {
                    return JSONHelper.FromString(false, "工单ID不能为空");
                }
                if (!Row.Table.Columns.Contains("type") || string.IsNullOrEmpty(Row["type"].ToString()))
                {
                    return JSONHelper.FromString(false, "请求状态不能为空");
                }
           
                 
                incidentid = Row["IncidentID"].ToString();
                type= Row["type"].ToString();

             
                CRMZTType tmp = (CRMZTType)(Enum.Parse(typeof(CRMZTType), type));

                backstr=  new IncidentAcceptCRM_ZT().IncidentCRM(incidentid, tmp);

                  

                log.ErrorContent = backstr;
                LogAdd(log);
                return backstr;

            }
            catch (Exception ex)
            {
                backstr = ex.ToString() + "--PushIncidentCRM";
                log.ErrorContent = "false," + backstr;
                LogAdd(log);
                return JSONHelper.FromString(false, backstr);
            }

        } 

        public void LogAdd(Tb_HSPR_IncidentError model)
        {
            DataAccess DAccess = new DataAccess(ContionString_Base);
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_Tb_HSPR_IncidentError_ADD";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@State,@ErrorContent,@Method,@ErrorDate,@Parameter";

            dbParams.Add("State", model.State, SqlDbType.NVarChar);
            dbParams.Add("ErrorContent", model.ErrorContent, SqlDbType.Text);
            dbParams.Add("Method", model.Method, SqlDbType.Text);
            dbParams.Add("ErrorDate", model.ErrorDate, SqlDbType.DateTime);
            dbParams.Add("Parameter", model.Parameter, SqlDbType.Text);

            DAccess.DataTable(dbParams);
        }








    }
}
