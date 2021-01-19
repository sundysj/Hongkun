using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//�����������
namespace MobileSoft.DAL.Resources
{
	/// <summary>
	/// ���ݷ�����Dal_Tb_Resources_ReleaseVenues��
	/// </summary>
	public class Dal_Tb_Resources_ReleaseVenues
	{
		public Dal_Tb_Resources_ReleaseVenues()
		{
            DbHelperSQL.ConnectionString = PubConstant.SQMBSContionString;
		}
		#region  ��Ա����

		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(long ReleaseVenuesID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ReleaseVenuesID", SqlDbType.BigInt)};
			parameters[0].Value = ReleaseVenuesID;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_Resources_ReleaseVenues_Exists",parameters,out rowsAffected);
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
		public long Add(MobileSoft.Model.Resources.Tb_Resources_ReleaseVenues model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ReleaseVenuesID", SqlDbType.BigInt,8),
					new SqlParameter("@ReleaseID", SqlDbType.BigInt,8),
					new SqlParameter("@ReleaseVenuesContent", SqlDbType.NText),
					new SqlParameter("@ReleaseVenuesNeedKnow", SqlDbType.NText)};
			parameters[0].Direction = ParameterDirection.Output;
			parameters[1].Value = model.ReleaseID;
			parameters[2].Value = model.ReleaseVenuesContent;
			parameters[3].Value = model.ReleaseVenuesNeedKnow;

			DbHelperSQL.RunProcedure("Proc_Tb_Resources_ReleaseVenues_ADD",parameters,out rowsAffected);
            return (long)parameters[0].Value;
		}

		/// <summary>
		///  ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.Resources.Tb_Resources_ReleaseVenues model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ReleaseVenuesID", SqlDbType.BigInt,8),
					new SqlParameter("@ReleaseID", SqlDbType.BigInt,8),
					new SqlParameter("@ReleaseVenuesContent", SqlDbType.NText),
					new SqlParameter("@ReleaseVenuesNeedKnow", SqlDbType.NText)};
			parameters[0].Value = model.ReleaseVenuesID;
			parameters[1].Value = model.ReleaseID;
			parameters[2].Value = model.ReleaseVenuesContent;
			parameters[3].Value = model.ReleaseVenuesNeedKnow;

			DbHelperSQL.RunProcedure("Proc_Tb_Resources_ReleaseVenues_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(long ReleaseVenuesID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ReleaseVenuesID", SqlDbType.BigInt)};
			parameters[0].Value = ReleaseVenuesID;

			DbHelperSQL.RunProcedure("Proc_Tb_Resources_ReleaseVenues_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.Resources.Tb_Resources_ReleaseVenues GetModel(long ReleaseVenuesID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@ReleaseVenuesID", SqlDbType.BigInt)};
			parameters[0].Value = ReleaseVenuesID;

			MobileSoft.Model.Resources.Tb_Resources_ReleaseVenues model=new MobileSoft.Model.Resources.Tb_Resources_ReleaseVenues();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_Resources_ReleaseVenues_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ReleaseVenuesID"].ToString()!="")
				{
					model.ReleaseVenuesID=long.Parse(ds.Tables[0].Rows[0]["ReleaseVenuesID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ReleaseID"].ToString()!="")
				{
					model.ReleaseID=long.Parse(ds.Tables[0].Rows[0]["ReleaseID"].ToString());
				}
				model.ReleaseVenuesContent=ds.Tables[0].Rows[0]["ReleaseVenuesContent"].ToString();
				model.ReleaseVenuesNeedKnow=ds.Tables[0].Rows[0]["ReleaseVenuesNeedKnow"].ToString();
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
			strSql.Append("select ReleaseVenuesID,ReleaseID,ReleaseVenuesContent,ReleaseVenuesNeedKnow ");
			strSql.Append(" FROM Tb_Resources_ReleaseVenues ");
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
			strSql.Append(" ReleaseVenuesID,ReleaseID,ReleaseVenuesContent,ReleaseVenuesNeedKnow ");
			strSql.Append(" FROM Tb_Resources_ReleaseVenues ");
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
			parameters[5].Value = "SELECT * FROM Tb_Resources_ReleaseVenues WHERE 1=1 " + StrCondition;
			parameters[6].Value = "ReleaseVenuesID";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  ��Ա����
	}
}

