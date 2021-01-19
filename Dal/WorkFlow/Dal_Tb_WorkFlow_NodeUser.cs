using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
using STP = System.Type;
namespace MobileSoft.DAL.WorkFlow
{
	/// <summary>
	/// 数据访问类Dal_Tb_WorkFlow_NodeUser。
	/// </summary>
	public class Dal_Tb_WorkFlow_NodeUser
	{
		public Dal_Tb_WorkFlow_NodeUser()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法


		/// <summary>
		///  增加一条数据
		/// </summary>
		public void Add(MobileSoft.Model.WorkFlow.Tb_WorkFlow_NodeUser model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@Tb_WorkFlow_FlowNode_InfoId", SqlDbType.Int,4),
					new SqlParameter("@Tb_Sys_User_UserCode", SqlDbType.NVarChar,20)};
			parameters[0].Value = model.Tb_WorkFlow_FlowNode_InfoId;
			parameters[1].Value = model.Tb_Sys_User_UserCode;

			DbHelperSQL.RunProcedure("Proc_Tb_WorkFlow_NodeUser_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.WorkFlow.Tb_WorkFlow_NodeUser model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@Tb_WorkFlow_FlowNode_InfoId", SqlDbType.Int,4),
					new SqlParameter("@Tb_Sys_User_UserCode", SqlDbType.NVarChar,20)};
			parameters[0].Value = model.Tb_WorkFlow_FlowNode_InfoId;
			parameters[1].Value = model.Tb_Sys_User_UserCode;

			DbHelperSQL.RunProcedure("Proc_Tb_WorkFlow_NodeUser_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete()
		{
			int rowsAffected;
			SqlParameter[] parameters = {
};

			DbHelperSQL.RunProcedure("Proc_Tb_WorkFlow_NodeUser_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.WorkFlow.Tb_WorkFlow_NodeUser GetModel()
		{
			SqlParameter[] parameters = {
};

			MobileSoft.Model.WorkFlow.Tb_WorkFlow_NodeUser model=new MobileSoft.Model.WorkFlow.Tb_WorkFlow_NodeUser();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_WorkFlow_NodeUser_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["Tb_WorkFlow_FlowNode_InfoId"].ToString()!="")
				{
					model.Tb_WorkFlow_FlowNode_InfoId=int.Parse(ds.Tables[0].Rows[0]["Tb_WorkFlow_FlowNode_InfoId"].ToString());
				}
				model.Tb_Sys_User_UserCode=ds.Tables[0].Rows[0]["Tb_Sys_User_UserCode"].ToString();
				return model;
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select Tb_WorkFlow_FlowNode_InfoId,Tb_Sys_User_UserCode ");
			strSql.Append(" FROM Tb_WorkFlow_NodeUser ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string fieldOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" Tb_WorkFlow_FlowNode_InfoId,Tb_Sys_User_UserCode ");
			strSql.Append(" FROM Tb_WorkFlow_NodeUser ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + fieldOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(out int PageCount, out int Counts, string StrCondition, int PageIndex, int PageSize,string SortField,int Sort)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@FldName", SqlDbType.VarChar, 255),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@FldSort", SqlDbType.VarChar, 1000),
					new SqlParameter("@Sort", SqlDbType.Int),
					new SqlParameter("@StrCondition", SqlDbType.VarChar, 8000),
					new SqlParameter("@Id", SqlDbType.VarChar, 50),
					new SqlParameter("@PageCount", SqlDbType.Int, 4,ParameterDirection.Output, false, 0, 0,string.Empty, DataRowVersion.Default, null),
					new SqlParameter("@Counts", SqlDbType.Int, 4,ParameterDirection.Output, false, 0, 0,string.Empty, DataRowVersion.Default, null),
					};
			parameters[0].Value = "*";
			parameters[1].Value = PageSize;
			parameters[2].Value = PageIndex;
			parameters[3].Value = SortField;
			parameters[4].Value = Sort;
			parameters[5].Value = "SELECT * FROM Tb_WorkFlow_NodeUser WHERE 1=1 " + StrCondition;
			parameters[6].Value = "";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法

            public DataTable GetWorkFlowNodeUser(int InstanceId, string DictionaryCode)
            {
                  SqlParameter[] parameters = {
					new SqlParameter("@InstanceId", SqlDbType.Int),
                              new SqlParameter("@DictionaryCode", SqlDbType.VarChar,50),
                              new SqlParameter("@Type", SqlDbType.VarChar,50)
                                              };
                  parameters[0].Value = InstanceId;
                  parameters[1].Value = DictionaryCode;
                  parameters[2].Value = "Tb_WorkFlow_NodeUser";

                  DataSet Ds = DbHelperSQL.RunProcedure("Proc_OAWorkFlow_GetWorkFlowChildTable", parameters, "RetDataSet");

                  DataTable dTable = new DataTable();

                  if (Ds.Tables.Count > 0)
                  {
                        dTable = Ds.Tables[0];

                        DataColumn NewColumn = new DataColumn();
                        NewColumn.ColumnName = "RandCode";
                        NewColumn.DataType = STP.GetType("System.String");
                        dTable.Columns.Add(NewColumn);
                        foreach (DataRow Row in dTable.Rows)
                        {
                              Row["RandCode"] = Row["Tb_WorkFlow_FlowNode_InfoId"].ToString();
                        }

                  }
                  return dTable;
            }

            public DataTable GetWorkFlowUsersFilter(string UserCode, int WorkFlowId)
            {
                  SqlParameter[] parameters = {
                              new SqlParameter("@UserCode", SqlDbType.VarChar,50),
					new SqlParameter("@WorkFlowId", SqlDbType.Int)
                              
                                              };
                  parameters[0].Value = UserCode;
                  parameters[1].Value = WorkFlowId;

                  DataSet Ds = DbHelperSQL.RunProcedure("Proc_WorkFlow_GetNodeUsers", parameters, "RetDataSet");

                  DataTable dTable = new DataTable();

                  if (Ds.Tables.Count > 0)
                  {
                        dTable = Ds.Tables[0];
                  }

                  return dTable;
            }
	}
}

