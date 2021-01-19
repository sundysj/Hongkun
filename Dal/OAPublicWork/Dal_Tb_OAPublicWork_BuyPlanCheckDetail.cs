using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//�����������
namespace MobileSoft.DAL.OAPublicWork
{
	/// <summary>
	/// ���ݷ�����Dal_Tb_OAPublicWork_BuyPlanCheckDetail��
	/// </summary>
	public class Dal_Tb_OAPublicWork_BuyPlanCheckDetail
	{
		public Dal_Tb_OAPublicWork_BuyPlanCheckDetail()
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

			int result= DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_BuyPlanCheckDetail_Exists",parameters,out rowsAffected);
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
		public int Add(MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_BuyPlanCheckDetail model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.Int,4),
					new SqlParameter("@Tb_OAPublicWork_BuyPlanCheck_InfoId", SqlDbType.Int,4),
					new SqlParameter("@Num", SqlDbType.Int,4),
					new SqlParameter("@SubjectName", SqlDbType.NVarChar,200),
					new SqlParameter("@SubjectType", SqlDbType.NVarChar,50),
					new SqlParameter("@BuyPlanMoney", SqlDbType.Decimal,9),
					new SqlParameter("@HowMany", SqlDbType.Decimal,9),
					new SqlParameter("@NeedDate", SqlDbType.DateTime),
					new SqlParameter("@HowUse", SqlDbType.VarChar,100),
					new SqlParameter("@Mark", SqlDbType.VarChar,100)};
			parameters[0].Direction = ParameterDirection.Output;
			parameters[1].Value = model.Tb_OAPublicWork_BuyPlanCheck_InfoId;
			parameters[2].Value = model.Num;
			parameters[3].Value = model.SubjectName;
			parameters[4].Value = model.SubjectType;
			parameters[5].Value = model.BuyPlanMoney;
			parameters[6].Value = model.HowMany;
			parameters[7].Value = model.NeedDate;
			parameters[8].Value = model.HowUse;
			parameters[9].Value = model.Mark;

			DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_BuyPlanCheckDetail_ADD",parameters,out rowsAffected);
			return (int)parameters[0].Value;
		}

		/// <summary>
		///  ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_BuyPlanCheckDetail model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.Int,4),
					new SqlParameter("@Tb_OAPublicWork_BuyPlanCheck_InfoId", SqlDbType.Int,4),
					new SqlParameter("@Num", SqlDbType.Int,4),
					new SqlParameter("@SubjectName", SqlDbType.NVarChar,200),
					new SqlParameter("@SubjectType", SqlDbType.NVarChar,50),
					new SqlParameter("@BuyPlanMoney", SqlDbType.Decimal,9),
					new SqlParameter("@HowMany", SqlDbType.Decimal,9),
					new SqlParameter("@NeedDate", SqlDbType.DateTime),
					new SqlParameter("@HowUse", SqlDbType.VarChar,100),
					new SqlParameter("@Mark", SqlDbType.VarChar,100)};
			parameters[0].Value = model.InfoId;
			parameters[1].Value = model.Tb_OAPublicWork_BuyPlanCheck_InfoId;
			parameters[2].Value = model.Num;
			parameters[3].Value = model.SubjectName;
			parameters[4].Value = model.SubjectType;
			parameters[5].Value = model.BuyPlanMoney;
			parameters[6].Value = model.HowMany;
			parameters[7].Value = model.NeedDate;
			parameters[8].Value = model.HowUse;
			parameters[9].Value = model.Mark;

			DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_BuyPlanCheckDetail_Update",parameters,out rowsAffected);
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

			DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_BuyPlanCheckDetail_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_BuyPlanCheckDetail GetModel(int InfoId)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.Int,4)};
			parameters[0].Value = InfoId;

			MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_BuyPlanCheckDetail model=new MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_BuyPlanCheckDetail();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_BuyPlanCheckDetail_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["InfoId"].ToString()!="")
				{
					model.InfoId=int.Parse(ds.Tables[0].Rows[0]["InfoId"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Tb_OAPublicWork_BuyPlanCheck_InfoId"].ToString()!="")
				{
					model.Tb_OAPublicWork_BuyPlanCheck_InfoId=int.Parse(ds.Tables[0].Rows[0]["Tb_OAPublicWork_BuyPlanCheck_InfoId"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Num"].ToString()!="")
				{
					model.Num=int.Parse(ds.Tables[0].Rows[0]["Num"].ToString());
				}
				model.SubjectName=ds.Tables[0].Rows[0]["SubjectName"].ToString();
				model.SubjectType=ds.Tables[0].Rows[0]["SubjectType"].ToString();
				if(ds.Tables[0].Rows[0]["BuyPlanMoney"].ToString()!="")
				{
					model.BuyPlanMoney=decimal.Parse(ds.Tables[0].Rows[0]["BuyPlanMoney"].ToString());
				}
				if(ds.Tables[0].Rows[0]["HowMany"].ToString()!="")
				{
					model.HowMany=decimal.Parse(ds.Tables[0].Rows[0]["HowMany"].ToString());
				}
				if(ds.Tables[0].Rows[0]["NeedDate"].ToString()!="")
				{
					model.NeedDate=DateTime.Parse(ds.Tables[0].Rows[0]["NeedDate"].ToString());
				}
				model.HowUse=ds.Tables[0].Rows[0]["HowUse"].ToString();
				model.Mark=ds.Tables[0].Rows[0]["Mark"].ToString();
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
			strSql.Append("select InfoId,Tb_OAPublicWork_BuyPlanCheck_InfoId,Num,SubjectName,SubjectType,BuyPlanMoney,HowMany,NeedDate,HowUse,Mark ");
			strSql.Append(" FROM Tb_OAPublicWork_BuyPlanCheckDetail ");
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
			strSql.Append(" InfoId,Tb_OAPublicWork_BuyPlanCheck_InfoId,Num,SubjectName,SubjectType,BuyPlanMoney,HowMany,NeedDate,HowUse,Mark ");
			strSql.Append(" FROM Tb_OAPublicWork_BuyPlanCheckDetail ");
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
			parameters[5].Value = "SELECT * FROM Tb_OAPublicWork_BuyPlanCheckDetail WHERE 1=1 " + StrCondition;
			parameters[6].Value = "InfoId";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  ��Ա����
	}
}

