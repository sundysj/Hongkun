using Common;
using Dapper;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Business
{
    public class SwitchSystem : PubInfo
    {
        public SwitchSystem()
        {
            base.Token = "20160606SwitchSystem";
        }

        public override void Operate(ref Common.Transfer Trans)
        {
            Trans.Result = JSONHelper.FromString(false, "未知错误!");
            DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
            DataRow Row = dAttributeTable.Rows[0];
            //验证登录
            if (!new Login().isLogin(ref Trans)) return;

            switch (Trans.Command.ToString())
            {
                //切换机构列表
                case "OrganList":
                    Trans.Result = Sys_Organ_GetEntryNodes(Global_Var.LoginUserCode);
                    break;
                case "GetAllCommunity":
                    Trans.Result = GetAllCommunity();
                    break;
            }
        }

        #region 得到用户可进入的机构(没有权限不可见)

        public string Sys_Organ_GetEntryNodes(string UserCode)
        {
            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                IEnumerable<dynamic> resultSet = conn.Query("Proc_Sys_Organ_GetEntryNodes", new { UserCode = UserCode }, null, false, null, CommandType.StoredProcedure);

                //if (Global_Var.CorpId == "2009" || Global_Var.CorpId == "2022")
                //{
                //    List<dynamic> list = new List<dynamic>();
                //    foreach (dynamic item in resultSet)
                //    {
                //        if (!(item.RoleOrgan == null || string.IsNullOrEmpty(item.RoleOrgan.ToString())))
                //        {
                //            list.Add(item);
                //        }
                //    }

                //    return new ApiResult(true, list).toJson();
                //}

                return new ApiResult(true, resultSet).toJson();
            }
        }


        #endregion

        public string GetAllCommunity()
        {
            using (var conn = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                return new ApiResult(true, conn.Query(@"SELECT CommID,CommName FROM tb_hspr_community 
                                                        WHERE isnull(IsDelete,0)=0;")).toJson();
            }
        }
    }
}
