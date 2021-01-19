using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//�����������
namespace MobileSoft.DAL.HSPR
{
	/// <summary>
	/// ���ݷ�����Dal_Tb_HSPR_IncidentRegional��
	/// </summary>
	public class Dal_Tb_HSPR_IncidentRegional
	{
		public Dal_Tb_HSPR_IncidentRegional()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  ��Ա����

		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(long RegionalID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@RegionalID", SqlDbType.BigInt)};
			parameters[0].Value = RegionalID;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_HSPR_IncidentRegional_Exists",parameters,out rowsAffected);
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
		public void Add(MobileSoft.Model.HSPR.Tb_HSPR_IncidentRegional model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@RegionalID", SqlDbType.BigInt,8),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@RegionalNum", SqlDbType.Int,4),
					new SqlParameter("@RegionalPlace", SqlDbType.NVarChar,50),
					new SqlParameter("@RegionalName", SqlDbType.NVarChar,50),
					new SqlParameter("@RegionalMemo", SqlDbType.NVarChar,200),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2)};
			parameters[0].Value = model.RegionalID;
			parameters[1].Value = model.CommID;
			parameters[2].Value = model.RegionalNum;
			parameters[3].Value = model.RegionalPlace;
			parameters[4].Value = model.RegionalName;
			parameters[5].Value = model.RegionalMemo;
			parameters[6].Value = model.IsDelete;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_IncidentRegional_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.HSPR.Tb_HSPR_IncidentRegional model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@RegionalID", SqlDbType.BigInt,8),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@RegionalNum", SqlDbType.Int,4),
					new SqlParameter("@RegionalPlace", SqlDbType.NVarChar,50),
					new SqlParameter("@RegionalName", SqlDbType.NVarChar,50),
					new SqlParameter("@RegionalMemo", SqlDbType.NVarChar,200),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2)};
			parameters[0].Value = model.RegionalID;
			parameters[1].Value = model.CommID;
			parameters[2].Value = model.RegionalNum;
			parameters[3].Value = model.RegionalPlace;
			parameters[4].Value = model.RegionalName;
			parameters[5].Value = model.RegionalMemo;
			parameters[6].Value = model.IsDelete;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_IncidentRegional_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(long RegionalID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@RegionalID", SqlDbType.BigInt)};
			parameters[0].Value = RegionalID;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_IncidentRegional_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_IncidentRegional GetModel(long RegionalID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@RegionalID", SqlDbType.BigInt)};
			parameters[0].Value = RegionalID;

			MobileSoft.Model.HSPR.Tb_HSPR_IncidentRegional model=new MobileSoft.Model.HSPR.Tb_HSPR_IncidentRegional();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_HSPR_IncidentRegional_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["RegionalID"].ToString()!="")
				{
					model.RegionalID=long.Parse(ds.Tables[0].Rows[0]["RegionalID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CommID"].ToString()!="")
				{
					model.CommID=int.Parse(ds.Tables[0].Rows[0]["CommID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["RegionalNum"].ToString()!="")
				{
					model.RegionalNum=int.Parse(ds.Tables[0].Rows[0]["RegionalNum"].ToString());
				}
				model.RegionalPlace=ds.Tables[0].Rows[0]["RegionalPlace"].ToString();
				model.RegionalName=ds.Tables[0].Rows[0]["RegionalName"].ToString();
				model.RegionalMemo=ds.Tables[0].Rows[0]["RegionalMemo"].ToString();
				if(ds.Tables[0].Rows[0]["IsDelete"].ToString()!="")
				{
					model.IsDelete=int.Parse(ds.Tables[0].Rows[0]["IsDelete"].ToString());
				}
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
			strSql.Append("select RegionalID,CommID,RegionalNum,RegionalPlace,RegionalName,RegionalMemo,IsDelete ");
			strSql.Append(" FROM Tb_HSPR_IncidentRegional ");
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
			strSql.Append(" RegionalID,CommID,RegionalNum,RegionalPlace,RegionalName,RegionalMemo,IsDelete ");
			strSql.Append(" FROM Tb_HSPR_IncidentRegional ");
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
			parameters[5].Value = "SELECT * FROM Tb_HSPR_IncidentRegional WHERE 1=1 " + StrCondition;
			parameters[6].Value = "RegionalID";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  ��Ա����
	}
}

