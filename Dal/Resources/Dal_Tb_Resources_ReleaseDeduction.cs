using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//�����������
namespace MobileSoft.DAL.Resources
{
	/// <summary>
	/// ���ݷ�����Dal_Tb_Resources_ReleaseDeduction��
	/// </summary>
	public class Dal_Tb_Resources_ReleaseDeduction
	{
		public Dal_Tb_Resources_ReleaseDeduction()
		{
            DbHelperSQL.ConnectionString = PubConstant.SQMBSContionString;
		}
		#region  ��Ա����

		/// <summary>
		/// �õ����ID
		/// </summary>
		public long GetMaxId()
		{
		return DbHelperSQL.GetMaxID("ReleaseDeductionID", "Tb_Resources_ReleaseDeduction"); 
		}

		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(long ReleaseDeductionID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ReleaseDeductionID", SqlDbType.BigInt)};
			parameters[0].Value = ReleaseDeductionID;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_Resources_ReleaseDeduction_Exists",parameters,out rowsAffected);
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
		public long Add(MobileSoft.Model.Resources.Tb_Resources_ReleaseDeduction model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ReleaseDeductionID", SqlDbType.BigInt,8),
					new SqlParameter("@ReleaseDeductionContent", SqlDbType.NText),
					new SqlParameter("@ReleaseDeductionNeedKnow", SqlDbType.NText),
					new SqlParameter("@ReleaseID", SqlDbType.BigInt,8)};
			parameters[0].Direction = ParameterDirection.Output;
			parameters[1].Value = model.ReleaseDeductionContent;
			parameters[2].Value = model.ReleaseDeductionNeedKnow;
			parameters[3].Value = model.ReleaseID;

			DbHelperSQL.RunProcedure("Proc_Tb_Resources_ReleaseDeduction_ADD",parameters,out rowsAffected);
			return (long)parameters[0].Value;
		}

		/// <summary>
		///  ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.Resources.Tb_Resources_ReleaseDeduction model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ReleaseDeductionID", SqlDbType.BigInt,8),
					new SqlParameter("@ReleaseDeductionContent", SqlDbType.NText),
					new SqlParameter("@ReleaseDeductionNeedKnow", SqlDbType.NText),
					new SqlParameter("@ReleaseID", SqlDbType.BigInt,8)};
			parameters[0].Value = model.ReleaseDeductionID;
			parameters[1].Value = model.ReleaseDeductionContent;
			parameters[2].Value = model.ReleaseDeductionNeedKnow;
			parameters[3].Value = model.ReleaseID;

			DbHelperSQL.RunProcedure("Proc_Tb_Resources_ReleaseDeduction_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(long ReleaseDeductionID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ReleaseDeductionID", SqlDbType.BigInt)};
			parameters[0].Value = ReleaseDeductionID;

			DbHelperSQL.RunProcedure("Proc_Tb_Resources_ReleaseDeduction_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.Resources.Tb_Resources_ReleaseDeduction GetModel(long ReleaseDeductionID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@ReleaseDeductionID", SqlDbType.BigInt)};
			parameters[0].Value = ReleaseDeductionID;

			MobileSoft.Model.Resources.Tb_Resources_ReleaseDeduction model=new MobileSoft.Model.Resources.Tb_Resources_ReleaseDeduction();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_Resources_ReleaseDeduction_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ReleaseDeductionID"].ToString()!="")
				{
					model.ReleaseDeductionID=long.Parse(ds.Tables[0].Rows[0]["ReleaseDeductionID"].ToString());
				}
				model.ReleaseDeductionContent=ds.Tables[0].Rows[0]["ReleaseDeductionContent"].ToString();
				model.ReleaseDeductionNeedKnow=ds.Tables[0].Rows[0]["ReleaseDeductionNeedKnow"].ToString();
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
			strSql.Append("select ReleaseDeductionID,ReleaseDeductionContent,ReleaseDeductionNeedKnow,ReleaseID ");
			strSql.Append(" FROM Tb_Resources_ReleaseDeduction ");
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
			strSql.Append(" ReleaseDeductionID,ReleaseDeductionContent,ReleaseDeductionNeedKnow,ReleaseID ");
			strSql.Append(" FROM Tb_Resources_ReleaseDeduction ");
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
			parameters[5].Value = "SELECT * FROM Tb_Resources_ReleaseDeduction WHERE 1=1 " + StrCondition;
			parameters[6].Value = "ReleaseDeductionID";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  ��Ա����
	}
}

