using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//�����������
namespace MobileSoft.DAL.SQMSys
{
	/// <summary>
	/// ���ݷ�����Dal_Tb_Sys_UserRole��
	/// </summary>
	public class Dal_Tb_Sys_UserRole
	{
		public Dal_Tb_Sys_UserRole()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  ��Ա����

		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(Guid UserRoleCode)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@UserRoleCode", SqlDbType.UniqueIdentifier)};
			parameters[0].Value = UserRoleCode;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_Sys_UserRole_Exists",parameters,out rowsAffected);
			if(result==1)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		///  ����һ������
		/// </summary>
		public void Add(MobileSoft.Model.SQMSys.Tb_Sys_UserRole model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@UserRoleCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@StreetCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@UserCode", SqlDbType.NVarChar,20),
					new SqlParameter("@RoleCode", SqlDbType.NVarChar,20)};
			parameters[0].Value = model.UserRoleCode;
			parameters[1].Value = model.StreetCode;
			parameters[2].Value = model.UserCode;
			parameters[3].Value = model.RoleCode;

			DbHelperSQL.RunProcedure("Proc_Tb_Sys_UserRole_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.SQMSys.Tb_Sys_UserRole model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@UserRoleCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@StreetCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@UserCode", SqlDbType.NVarChar,20),
					new SqlParameter("@RoleCode", SqlDbType.NVarChar,20)};
			parameters[0].Value = model.UserRoleCode;
			parameters[1].Value = model.StreetCode;
			parameters[2].Value = model.UserCode;
			parameters[3].Value = model.RoleCode;

			DbHelperSQL.RunProcedure("Proc_Tb_Sys_UserRole_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(Guid UserRoleCode)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@UserRoleCode", SqlDbType.UniqueIdentifier)};
			parameters[0].Value = UserRoleCode;

			DbHelperSQL.RunProcedure("Proc_Tb_Sys_UserRole_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.SQMSys.Tb_Sys_UserRole GetModel(Guid UserRoleCode)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@UserRoleCode", SqlDbType.UniqueIdentifier)};
			parameters[0].Value = UserRoleCode;

			MobileSoft.Model.SQMSys.Tb_Sys_UserRole model=new MobileSoft.Model.SQMSys.Tb_Sys_UserRole();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_Sys_UserRole_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["UserRoleCode"].ToString()!="")
				{
					model.UserRoleCode=new Guid(ds.Tables[0].Rows[0]["UserRoleCode"].ToString());
				}
				if(ds.Tables[0].Rows[0]["StreetCode"].ToString()!="")
				{
					model.StreetCode=new Guid(ds.Tables[0].Rows[0]["StreetCode"].ToString());
				}
				model.UserCode=ds.Tables[0].Rows[0]["UserCode"].ToString();
				model.RoleCode=ds.Tables[0].Rows[0]["RoleCode"].ToString();
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
			strSql.Append("select UserRoleCode,StreetCode,UserCode,RoleCode ");
			strSql.Append(" FROM Tb_Sys_UserRole ");
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
			strSql.Append(" UserRoleCode,StreetCode,UserCode,RoleCode ");
			strSql.Append(" FROM Tb_Sys_UserRole ");
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
			parameters[5].Value = "SELECT * FROM Tb_Sys_UserRole WHERE 1=1 " + StrCondition;
			parameters[6].Value = "UserRoleCode";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  ��Ա����

            public string Sys_User_FilterRoles(string UserCode, string StreetCode)
            {
                  SqlParameter[] parameters = {
					new SqlParameter("@UserCode", SqlDbType.NVarChar,50),
                              new SqlParameter("@StreetCode", SqlDbType.VarChar,50)
                                              };
                  parameters[0].Value = UserCode;
                  parameters[1].Value = StreetCode;

                  DataTable result = DbHelperSQL.RunProcedure("Proc_Sys_User_FilterRoles", parameters, "RetDataSet").Tables[0];

                  string strUserRoles = "";

                  if (result.Rows.Count > 0)
                  {
                        strUserRoles = result.Rows[0][0].ToString();
                  }

                  return strUserRoles;
            }
	}
}

