using MobileSoft.Common;
using MobileSoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class PersonnelManage : PubInfo
    {
        public PersonnelManage()
        {
            Token = @"20180918PersonnelManage";
            log = GetLog();
        }

        public override void Operate(ref Common.Transfer Trans)
        {
            try
            {
                Trans.Result = JSONHelper.FromString(false, "未知错误");
                DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
                DataRow Row = dAttributeTable.Rows[0];

                //验证登录
                if (!new Login().isLogin(ref Trans))
                    return;

                switch (Trans.Command)
                {
                    case "GetListWithState":            // 审批列表
                        Trans.Result = GetListWithState(Row);
                        break;
                    case "GetEntryApprovalDetail":      // 入职审批详情
                        Trans.Result = GetEntryApprovalDetail(Row);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace + Environment.NewLine + ex.Source + Environment.NewLine + Trans.Attribute);
                Trans.Result = new ApiResult(false, "接口抛出了一个异常").toJson();
            }
        }

        // 审批列表
        private string GetListWithState(DataRow row)
        {
            if (!row.Table.Columns.Contains("State") || string.IsNullOrEmpty(row["State"].ToString()))
            {
                return JSONHelper.FromString(false, "流程状态不能为空");
            }

            if (!row.Table.Columns.Contains("PageSize") || string.IsNullOrEmpty(row["PageSize"].ToString()))
            {
                return JSONHelper.FromString(false, "PageSize不能为空");
            }

            if (!row.Table.Columns.Contains("PageIndex") || string.IsNullOrEmpty(row["PageIndex"].ToString()))
            {
                return JSONHelper.FromString(false, "PageIndex不能为空");
            }

            int pageSize = AppGlobal.StrToInt(row["PageSize"].ToString());
            int pageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());

            string State = row["State"].ToString();
            string sql = @"SELECT * FROM
                            (
                            SELECT Id AS IID, BussType, StartDate AS CreateDate, DepName, NULL AS AuditName,
                                TitleName, PName AS PersonName, Img AS PersonHeaderImg
                                FROM View_Tb_Pm_Change_Filter x WHERE ChangeState = '{0}' 
                                AND (UserCode='{1}' OR )
                                AND DepCode IN(SELECT DepCode FROM Tb_Sys_DepartmentRolePope WHERE CHARINDEX(RoleCode,'{1}')>0)
                                AND RoleCode IN(SELECT CRoleCode FROM Tb_Sys_RoleRolePope WHERE CHARINDEX(RoleCode,'{1}')>0)
                            UNION
                            SELECT Id AS IID, '合同审批' AS BussType, RegDate AS CreateDate, PDepName AS DepName, 
                                NULL AS AuditName,(PDepName + '-' + Name + '-' + TypeName) AS TitleName, 
                                Name AS PersonName, Img AS PersonHeaderImg
                                FROM View_Tb_Pm_Contract_Filter x WHERE CheckState = '{0}'
                                AND DepCode IN(SELECT DepCode FROM Tb_Sys_DepartmentRolePope WHERE CHARINDEX(RoleCode,'{1}')>0)
                                AND RoleCode IN(SELECT CRoleCode FROM Tb_Sys_RoleRolePope WHERE CHARINDEX(RoleCode,'{1}')>0)

                            UNION
                            SELECT Id AS IID, '奖惩审批' AS BussType, RegDate AS CreateDate, SalaryDepName AS DepName, 
                                NULL AS AuditName,(SalaryDepName + '-' + PersonName + '-' + DictionaryName) AS TitleName,         PersonName AS PersonName, Img AS PersonHeaderImg
                                FROM View_Tb_Pm_RewardPunishment_Filter x WHERE CheckState = '{0}'
                                AND DepCode IN(SELECT DepCode FROM Tb_Sys_DepartmentRolePope WHERE CHARINDEX(RoleCode,'{1}')>0)
                                AND RoleCode IN(SELECT CRoleCode FROM Tb_Sys_RoleRolePope WHERE CHARINDEX(RoleCode,'{1}')>0)

                            UNION
                            SELECT Id AS IID, '请假审批' AS BussType, TransactDate AS CreateDate, DepName,NULL AS AuditName,
                                (DepName + '-' + RoleName + '-' + TransactMan + '-' + LeaveTypeName) AS TitleName, 
                                TransactMan AS PersonName, Img AS PersonHeaderImg
                                FROM View_Pm_LeaveFilter x WHERE CheckState = '{0}'
                                AND DepCode IN(SELECT DepCode FROM Tb_Sys_DepartmentRolePope WHERE CHARINDEX(RoleCode,'{1}')>0)
                                AND RoleCode IN(SELECT CRoleCode FROM Tb_Sys_RoleRolePope WHERE CHARINDEX(RoleCode,'{1}')>0)
                            ) AS tmp";

            if (State == "1")       // 处理中
            {
                State = "审核中";
            }
            else if (State == "2")  // 已驳回
            {

            }
            else if (State == "3")  // 未通过
            {

            }
            else if (State == "4")  // 已完成
            {
                State = "已审核";
            }

            sql = string.Format(sql, State, Global_Var.LoginRoles);

            DataTable dataTable = GetList(out int pageCount, out int count, sql, pageIndex, pageSize, "CreateDate", 0, "IID",
                    PubConstant.hmWyglConnectionString).Tables[0];

            return JSONHelper.FromString(true, dataTable);
        }

        // 入职审批详情
        private string GetEntryApprovalDetail(DataRow row)
        {
            return null;
        }
    }
}
