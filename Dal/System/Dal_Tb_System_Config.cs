using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//�����������
namespace MobileSoft.DAL.System
{
	/// <summary>
	/// ���ݷ�����Dal_Tb_System_Config��
	/// </summary>
	public class Dal_Tb_System_Config
	{
		public Dal_Tb_System_Config()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  ��Ա����

		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(string ConfigKey)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ConfigKey", SqlDbType.NVarChar,50)};
			parameters[0].Value = ConfigKey;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_System_Config_Exists",parameters,out rowsAffected);
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
		public void Add(MobileSoft.Model.System.Tb_System_Config model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ConfigKey", SqlDbType.NVarChar,10),
					new SqlParameter("@ConfigName", SqlDbType.NVarChar,100),
					new SqlParameter("@ConfigValue", SqlDbType.NVarChar,1000),
					new SqlParameter("@Memo", SqlDbType.NVarChar,1000)};
			parameters[0].Value = model.ConfigKey;
			parameters[1].Value = model.ConfigName;
			parameters[2].Value = model.ConfigValue;
			parameters[3].Value = model.Memo;

			DbHelperSQL.RunProcedure("Proc_Tb_System_Config_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.System.Tb_System_Config model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ConfigKey", SqlDbType.NVarChar,10),
					new SqlParameter("@ConfigName", SqlDbType.NVarChar,100),
					new SqlParameter("@ConfigValue", SqlDbType.NVarChar,1000),
					new SqlParameter("@Memo", SqlDbType.NVarChar,1000)};
			parameters[0].Value = model.ConfigKey;
			parameters[1].Value = model.ConfigName;
			parameters[2].Value = model.ConfigValue;
			parameters[3].Value = model.Memo;

			DbHelperSQL.RunProcedure("Proc_Tb_System_Config_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(string ConfigKey)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ConfigKey", SqlDbType.NVarChar,50)};
			parameters[0].Value = ConfigKey;

			DbHelperSQL.RunProcedure("Proc_Tb_System_Config_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.System.Tb_System_Config GetModel(string ConfigKey)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@ConfigKey", SqlDbType.NVarChar,50)};
			parameters[0].Value = ConfigKey;

			MobileSoft.Model.System.Tb_System_Config model=new MobileSoft.Model.System.Tb_System_Config();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_System_Config_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				model.ConfigKey=ds.Tables[0].Rows[0]["ConfigKey"].ToString();
				model.ConfigName=ds.Tables[0].Rows[0]["ConfigName"].ToString();
				model.ConfigValue=ds.Tables[0].Rows[0]["ConfigValue"].ToString();
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
			strSql.Append("select ConfigKey,ConfigName,ConfigValue,Memo ");
			strSql.Append(" FROM Tb_System_Config ");
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
			strSql.Append(" ConfigKey,ConfigName,ConfigValue,Memo ");
			strSql.Append(" FROM Tb_System_Config ");
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
			parameters[5].Value = "SELECT * FROM Tb_System_Config WHERE 1=1 " + StrCondition;
			parameters[6].Value = "ConfigKey";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  ��Ա����
	}
}

