using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//�����������
namespace MobileSoft.DAL.Resources
{
	/// <summary>
	/// ���ݷ�����Dal_Tb_Resources_ReleaseTraveling��
	/// </summary>
	public class Dal_Tb_Resources_ReleaseTraveling
	{
		public Dal_Tb_Resources_ReleaseTraveling()
		{
            DbHelperSQL.ConnectionString = PubConstant.SQMBSContionString;
		}
		#region  ��Ա����

		/// <summary>
		/// �õ����ID
		/// </summary>
		public long GetMaxId()
		{
		return DbHelperSQL.GetMaxID("ReleaseTravelingID", "Tb_Resources_ReleaseTraveling"); 
		}

		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(long ReleaseTravelingID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ReleaseTravelingID", SqlDbType.BigInt)};
			parameters[0].Value = ReleaseTravelingID;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_Resources_ReleaseTraveling_Exists",parameters,out rowsAffected);
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
		public long Add(MobileSoft.Model.Resources.Tb_Resources_ReleaseTraveling model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ReleaseTravelingID", SqlDbType.BigInt,8),
					new SqlParameter("@ReleaseTravelingContent", SqlDbType.NText),
					new SqlParameter("@ReleaseTravelingServicedemo", SqlDbType.NText),
					new SqlParameter("@ReleaseTravelingNeedKnow", SqlDbType.NText),
					new SqlParameter("@ReleaseID", SqlDbType.BigInt,8)};
			parameters[0].Direction = ParameterDirection.Output;
			parameters[1].Value = model.ReleaseTravelingContent;
			parameters[2].Value = model.ReleaseTravelingServicedemo;
			parameters[3].Value = model.ReleaseTravelingNeedKnow;
			parameters[4].Value = model.ReleaseID;

			DbHelperSQL.RunProcedure("Proc_Tb_Resources_ReleaseTraveling_ADD",parameters,out rowsAffected);
			return (long)parameters[0].Value;
		}

		/// <summary>
		///  ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.Resources.Tb_Resources_ReleaseTraveling model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ReleaseTravelingID", SqlDbType.BigInt,8),
					new SqlParameter("@ReleaseTravelingContent", SqlDbType.NText),
					new SqlParameter("@ReleaseTravelingServicedemo", SqlDbType.NText),
					new SqlParameter("@ReleaseTravelingNeedKnow", SqlDbType.NText),
					new SqlParameter("@ReleaseID", SqlDbType.BigInt,8)};
			parameters[0].Value = model.ReleaseTravelingID;
			parameters[1].Value = model.ReleaseTravelingContent;
			parameters[2].Value = model.ReleaseTravelingServicedemo;
			parameters[3].Value = model.ReleaseTravelingNeedKnow;
			parameters[4].Value = model.ReleaseID;

			DbHelperSQL.RunProcedure("Proc_Tb_Resources_ReleaseTraveling_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(long ReleaseTravelingID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ReleaseTravelingID", SqlDbType.BigInt)};
			parameters[0].Value = ReleaseTravelingID;

			DbHelperSQL.RunProcedure("Proc_Tb_Resources_ReleaseTraveling_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.Resources.Tb_Resources_ReleaseTraveling GetModel(long ReleaseTravelingID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@ReleaseTravelingID", SqlDbType.BigInt)};
			parameters[0].Value = ReleaseTravelingID;

			MobileSoft.Model.Resources.Tb_Resources_ReleaseTraveling model=new MobileSoft.Model.Resources.Tb_Resources_ReleaseTraveling();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_Resources_ReleaseTraveling_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ReleaseTravelingID"].ToString()!="")
				{
					model.ReleaseTravelingID=long.Parse(ds.Tables[0].Rows[0]["ReleaseTravelingID"].ToString());
				}
				model.ReleaseTravelingContent=ds.Tables[0].Rows[0]["ReleaseTravelingContent"].ToString();
				model.ReleaseTravelingServicedemo=ds.Tables[0].Rows[0]["ReleaseTravelingServicedemo"].ToString();
				model.ReleaseTravelingNeedKnow=ds.Tables[0].Rows[0]["ReleaseTravelingNeedKnow"].ToString();
				if(ds.Tables[0].Rows[0]["ReleaseID"].ToString()!="")
				{
					model.ReleaseID=long.Parse(ds.Tables[0].Rows[0]["ReleaseID"].ToString());
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
			strSql.Append("select ReleaseTravelingID,ReleaseTravelingContent,ReleaseTravelingServicedemo,ReleaseTravelingNeedKnow,ReleaseID ");
			strSql.Append(" FROM Tb_Resources_ReleaseTraveling ");
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
			strSql.Append(" ReleaseTravelingID,ReleaseTravelingContent,ReleaseTravelingServicedemo,ReleaseTravelingNeedKnow,ReleaseID ");
			strSql.Append(" FROM Tb_Resources_ReleaseTraveling ");
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
			parameters[5].Value = "SELECT * FROM Tb_Resources_ReleaseTraveling WHERE 1=1 " + StrCondition;
			parameters[6].Value = "ReleaseTravelingID";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  ��Ա����
	}
}

