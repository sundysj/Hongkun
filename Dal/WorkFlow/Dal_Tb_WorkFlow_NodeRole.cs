using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//�����������
using STP = System.Type;

namespace MobileSoft.DAL.WorkFlow
{
	/// <summary>
	/// ���ݷ�����Dal_Tb_WorkFlow_NodeRole��
	/// </summary>
	public class Dal_Tb_WorkFlow_NodeRole
	{
		public Dal_Tb_WorkFlow_NodeRole()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  ��Ա����


		/// <summary>
		///  ����һ������
		/// </summary>
		public void Add(MobileSoft.Model.WorkFlow.Tb_WorkFlow_NodeRole model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@Tb_WorkFlow_FlowNode_InfoId", SqlDbType.Int,4),
					new SqlParameter("@Tb_Sys_Role_RoleCode", SqlDbType.NVarChar,20)};
			parameters[0].Value = model.Tb_WorkFlow_FlowNode_InfoId;
			parameters[1].Value = model.Tb_Sys_Role_RoleCode;

			DbHelperSQL.RunProcedure("Proc_Tb_WorkFlow_NodeRole_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.WorkFlow.Tb_WorkFlow_NodeRole model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@Tb_WorkFlow_FlowNode_InfoId", SqlDbType.Int,4),
					new SqlParameter("@Tb_Sys_Role_RoleCode", SqlDbType.NVarChar,20)};
			parameters[0].Value = model.Tb_WorkFlow_FlowNode_InfoId;
			parameters[1].Value = model.Tb_Sys_Role_RoleCode;

			DbHelperSQL.RunProcedure("Proc_Tb_WorkFlow_NodeRole_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete()
		{
			int rowsAffected;
			SqlParameter[] parameters = {
};

			DbHelperSQL.RunProcedure("Proc_Tb_WorkFlow_NodeRole_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.WorkFlow.Tb_WorkFlow_NodeRole GetModel()
		{
			SqlParameter[] parameters = {
};

			MobileSoft.Model.WorkFlow.Tb_WorkFlow_NodeRole model=new MobileSoft.Model.WorkFlow.Tb_WorkFlow_NodeRole();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_WorkFlow_NodeRole_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["Tb_WorkFlow_FlowNode_InfoId"].ToString()!="")
				{
					model.Tb_WorkFlow_FlowNode_InfoId=int.Parse(ds.Tables[0].Rows[0]["Tb_WorkFlow_FlowNode_InfoId"].ToString());
				}
				model.Tb_Sys_Role_RoleCode=ds.Tables[0].Rows[0]["Tb_Sys_Role_RoleCode"].ToString();
				return model;
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// ��������б�
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select Tb_WorkFlow_FlowNode_InfoId,Tb_Sys_Role_RoleCode ");
			strSql.Append(" FROM Tb_WorkFlow_NodeRole ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// ���ǰ��������
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string fieldOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" Tb_WorkFlow_FlowNode_InfoId,Tb_Sys_Role_RoleCode ");
			strSql.Append(" FROM Tb_WorkFlow_NodeRole ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + fieldOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		
		/// <summary>
		/// ��ҳ��ȡ�����б�
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
			parameters[5].Value = "SELECT * FROM Tb_WorkFlow_NodeRole WHERE 1=1 " + StrCondition;
			parameters[6].Value = "";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  ��Ա����

            public DataTable GetWorkFlowNodeRole(int InstanceId, string DictionaryCode)
            {
                  SqlParameter[] parameters = {
					new SqlParameter("@InstanceId", SqlDbType.Int),
                              new SqlParameter("@DictionaryCode", SqlDbType.VarChar,50),
                              new SqlParameter("@Type", SqlDbType.VarChar,50)
                                              };
                  parameters[0].Value = InstanceId;
                  parameters[1].Value = DictionaryCode;
                  parameters[2].Value = "Tb_WorkFlow_NodeRole";

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
	}
}

