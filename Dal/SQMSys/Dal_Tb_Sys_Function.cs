using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//�����������
namespace MobileSoft.DAL.SQMSys
{
	/// <summary>
	/// ���ݷ�����Dal_Tb_Sys_Function��
	/// </summary>
	public class Dal_Tb_Sys_Function
	{
		public Dal_Tb_Sys_Function()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  ��Ա����

		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(string FunctionCode)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@FunctionCode", SqlDbType.NVarChar,50)};
			parameters[0].Value = FunctionCode;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_Sys_Function_Exists",parameters,out rowsAffected);
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
		public void Add(MobileSoft.Model.SQMSys.Tb_Sys_Function model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@FunctionCode", SqlDbType.NVarChar,2),
					new SqlParameter("@FunctionName", SqlDbType.NVarChar,50),
					new SqlParameter("@Memo", SqlDbType.NVarChar,1000)};
			parameters[0].Value = model.FunctionCode;
			parameters[1].Value = model.FunctionName;
			parameters[2].Value = model.Memo;

			DbHelperSQL.RunProcedure("Proc_Tb_Sys_Function_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.SQMSys.Tb_Sys_Function model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@FunctionCode", SqlDbType.NVarChar,2),
					new SqlParameter("@FunctionName", SqlDbType.NVarChar,50),
					new SqlParameter("@Memo", SqlDbType.NVarChar,1000)};
			parameters[0].Value = model.FunctionCode;
			parameters[1].Value = model.FunctionName;
			parameters[2].Value = model.Memo;

			DbHelperSQL.RunProcedure("Proc_Tb_Sys_Function_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(string FunctionCode)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@FunctionCode", SqlDbType.NVarChar,50)};
			parameters[0].Value = FunctionCode;

			DbHelperSQL.RunProcedure("Proc_Tb_Sys_Function_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.SQMSys.Tb_Sys_Function GetModel(string FunctionCode)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@FunctionCode", SqlDbType.NVarChar,50)};
			parameters[0].Value = FunctionCode;

			MobileSoft.Model.SQMSys.Tb_Sys_Function model=new MobileSoft.Model.SQMSys.Tb_Sys_Function();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_Sys_Function_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				model.FunctionCode=ds.Tables[0].Rows[0]["FunctionCode"].ToString();
				model.FunctionName=ds.Tables[0].Rows[0]["FunctionName"].ToString();
				model.Memo=ds.Tables[0].Rows[0]["Memo"].ToString();
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
			strSql.Append("select FunctionCode,FunctionName,Memo ");
			strSql.Append(" FROM Tb_Sys_Function ");
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
			strSql.Append(" FunctionCode,FunctionName,Memo ");
			strSql.Append(" FROM Tb_Sys_Function ");
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
			parameters[5].Value = "SELECT * FROM Tb_Sys_Function WHERE 1=1 " + StrCondition;
			parameters[6].Value = "FunctionCode";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  ��Ա����
	}
}

