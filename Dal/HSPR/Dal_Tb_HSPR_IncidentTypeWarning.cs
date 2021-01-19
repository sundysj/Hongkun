using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//�����������
namespace MobileSoft.DAL.HSPR
{
	/// <summary>
	/// ���ݷ�����Dal_Tb_HSPR_IncidentTypeWarning��
	/// </summary>
	public class Dal_Tb_HSPR_IncidentTypeWarning
	{
		public Dal_Tb_HSPR_IncidentTypeWarning()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  ��Ա����

		/// <summary>
		/// �õ����ID
		/// </summary>
		public long GetMaxId()
		{
		return DbHelperSQL.GetMaxID("WarningID", "Tb_HSPR_IncidentTypeWarning"); 
		}

		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(long WarningID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@WarningID", SqlDbType.BigInt)};
			parameters[0].Value = WarningID;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_HSPR_IncidentTypeWarning_Exists",parameters,out rowsAffected);
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
		public void Add(MobileSoft.Model.HSPR.Tb_HSPR_IncidentTypeWarning model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@WarningID", SqlDbType.BigInt,8),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@WarningType", SqlDbType.NVarChar,50),
					new SqlParameter("@WarningHour", SqlDbType.Int,4),
					new SqlParameter("@WarningMedo", SqlDbType.NVarChar,200),
					new SqlParameter("@IsDelete", SqlDbType.Int,4)};
			parameters[0].Value = model.WarningID;
			parameters[1].Value = model.CommID;
			parameters[2].Value = model.WarningType;
			parameters[3].Value = model.WarningHour;
			parameters[4].Value = model.WarningMedo;
			parameters[5].Value = model.IsDelete;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_IncidentTypeWarning_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.HSPR.Tb_HSPR_IncidentTypeWarning model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@WarningID", SqlDbType.BigInt,8),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@WarningType", SqlDbType.NVarChar,50),
					new SqlParameter("@WarningHour", SqlDbType.Int,4),
					new SqlParameter("@WarningMedo", SqlDbType.NVarChar,200),
					new SqlParameter("@IsDelete", SqlDbType.Int,4)};
			parameters[0].Value = model.WarningID;
			parameters[1].Value = model.CommID;
			parameters[2].Value = model.WarningType;
			parameters[3].Value = model.WarningHour;
			parameters[4].Value = model.WarningMedo;
			parameters[5].Value = model.IsDelete;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_IncidentTypeWarning_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(long WarningID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@WarningID", SqlDbType.BigInt)};
			parameters[0].Value = WarningID;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_IncidentTypeWarning_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_IncidentTypeWarning GetModel(long WarningID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@WarningID", SqlDbType.BigInt)};
			parameters[0].Value = WarningID;

			MobileSoft.Model.HSPR.Tb_HSPR_IncidentTypeWarning model=new MobileSoft.Model.HSPR.Tb_HSPR_IncidentTypeWarning();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_HSPR_IncidentTypeWarning_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["WarningID"].ToString()!="")
				{
					model.WarningID=long.Parse(ds.Tables[0].Rows[0]["WarningID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CommID"].ToString()!="")
				{
					model.CommID=int.Parse(ds.Tables[0].Rows[0]["CommID"].ToString());
				}
				model.WarningType=ds.Tables[0].Rows[0]["WarningType"].ToString();
				if(ds.Tables[0].Rows[0]["WarningHour"].ToString()!="")
				{
					model.WarningHour=int.Parse(ds.Tables[0].Rows[0]["WarningHour"].ToString());
				}
				model.WarningMedo=ds.Tables[0].Rows[0]["WarningMedo"].ToString();
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
			strSql.Append("select WarningID,CommID,WarningType,WarningHour,WarningMedo,IsDelete ");
			strSql.Append(" FROM Tb_HSPR_IncidentTypeWarning ");
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
			strSql.Append(" WarningID,CommID,WarningType,WarningHour,WarningMedo,IsDelete ");
			strSql.Append(" FROM Tb_HSPR_IncidentTypeWarning ");
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
			parameters[5].Value = "SELECT * FROM Tb_HSPR_IncidentTypeWarning WHERE 1=1 " + StrCondition;
			parameters[6].Value = "WarningID";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  ��Ա����
	}
}

