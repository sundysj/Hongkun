using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//�����������
namespace MobileSoft.DAL.OAPublicWork
{
	/// <summary>
	/// ���ݷ�����Dal_Tb_OAPublicWork_ReimbursementDetail��
	/// </summary>
	public class Dal_Tb_OAPublicWork_ReimbursementDetail
	{
		public Dal_Tb_OAPublicWork_ReimbursementDetail()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  ��Ա����

		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(int InfoId)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.Int,4)};
			parameters[0].Value = InfoId;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_ReimbursementDetail_Exists",parameters,out rowsAffected);
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
		public int Add(MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_ReimbursementDetail model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.Int,4),
					new SqlParameter("@Tb_OAPublicWork_Reimbursement_InfoId", SqlDbType.Int,4),
					new SqlParameter("@SubjectName", SqlDbType.NVarChar,100),
					new SqlParameter("@SummaryName", SqlDbType.VarChar,100),
					new SqlParameter("@ReimbursementMoney", SqlDbType.Decimal,9),
					new SqlParameter("@ReimbursementType", SqlDbType.NVarChar,50)};
			parameters[0].Direction = ParameterDirection.Output;
			parameters[1].Value = model.Tb_OAPublicWork_Reimbursement_InfoId;
			parameters[2].Value = model.SubjectName;
			parameters[3].Value = model.SummaryName;
			parameters[4].Value = model.ReimbursementMoney;
			parameters[5].Value = model.ReimbursementType;

			DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_ReimbursementDetail_ADD",parameters,out rowsAffected);
			return (int)parameters[0].Value;
		}

		/// <summary>
		///  ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_ReimbursementDetail model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.Int,4),
					new SqlParameter("@Tb_OAPublicWork_Reimbursement_InfoId", SqlDbType.Int,4),
					new SqlParameter("@SubjectName", SqlDbType.NVarChar,100),
					new SqlParameter("@SummaryName", SqlDbType.VarChar,100),
					new SqlParameter("@ReimbursementMoney", SqlDbType.Decimal,9),
					new SqlParameter("@ReimbursementType", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.InfoId;
			parameters[1].Value = model.Tb_OAPublicWork_Reimbursement_InfoId;
			parameters[2].Value = model.SubjectName;
			parameters[3].Value = model.SummaryName;
			parameters[4].Value = model.ReimbursementMoney;
			parameters[5].Value = model.ReimbursementType;

			DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_ReimbursementDetail_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(int InfoId)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.Int,4)};
			parameters[0].Value = InfoId;

			DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_ReimbursementDetail_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_ReimbursementDetail GetModel(int InfoId)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.Int,4)};
			parameters[0].Value = InfoId;

			MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_ReimbursementDetail model=new MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_ReimbursementDetail();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_ReimbursementDetail_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["InfoId"].ToString()!="")
				{
					model.InfoId=int.Parse(ds.Tables[0].Rows[0]["InfoId"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Tb_OAPublicWork_Reimbursement_InfoId"].ToString()!="")
				{
					model.Tb_OAPublicWork_Reimbursement_InfoId=int.Parse(ds.Tables[0].Rows[0]["Tb_OAPublicWork_Reimbursement_InfoId"].ToString());
				}
				model.SubjectName=ds.Tables[0].Rows[0]["SubjectName"].ToString();
				model.SummaryName=ds.Tables[0].Rows[0]["SummaryName"].ToString();
				if(ds.Tables[0].Rows[0]["ReimbursementMoney"].ToString()!="")
				{
					model.ReimbursementMoney=decimal.Parse(ds.Tables[0].Rows[0]["ReimbursementMoney"].ToString());
				}
				model.ReimbursementType=ds.Tables[0].Rows[0]["ReimbursementType"].ToString();
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
			strSql.Append("select InfoId,Tb_OAPublicWork_Reimbursement_InfoId,SubjectName,SummaryName,ReimbursementMoney,ReimbursementType ");
			strSql.Append(" FROM Tb_OAPublicWork_ReimbursementDetail ");
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
			strSql.Append(" InfoId,Tb_OAPublicWork_Reimbursement_InfoId,SubjectName,SummaryName,ReimbursementMoney,ReimbursementType ");
			strSql.Append(" FROM Tb_OAPublicWork_ReimbursementDetail ");
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
			parameters[5].Value = "SELECT * FROM Tb_OAPublicWork_ReimbursementDetail WHERE 1=1 " + StrCondition;
			parameters[6].Value = "InfoId";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  ��Ա����
	}
}

