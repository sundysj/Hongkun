using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//�����������
namespace MobileSoft.DAL.Management
{
	/// <summary>
	/// ���ݷ�����Dal_Tb_Management_Type��
	/// </summary>
	public class Dal_Tb_Management_Type
	{
		public Dal_Tb_Management_Type()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  ��Ա����

		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(string SortCode)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@SortCode", SqlDbType.VarChar,50)};
			parameters[0].Value = SortCode;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_Management_Type_Exists",parameters,out rowsAffected);
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
		public void Add(MobileSoft.Model.Management.Tb_Management_Type model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@Code", SqlDbType.VarChar,36),
					new SqlParameter("@SortCode", SqlDbType.VarChar,36),
					new SqlParameter("@Sort", SqlDbType.Int,4),
					new SqlParameter("@TypeName", SqlDbType.VarChar,36),
					new SqlParameter("@TypeCode", SqlDbType.VarChar,36),
					new SqlParameter("@Memo", SqlDbType.VarChar,3999),
					new SqlParameter("@IsDelete", SqlDbType.Int,4)};
			parameters[0].Value = model.Code;
			parameters[1].Value = model.SortCode;
			parameters[2].Value = model.Sort;
			parameters[3].Value = model.TypeName;
			parameters[4].Value = model.TypeCode;
			parameters[5].Value = model.Memo;
			parameters[6].Value = model.IsDelete;

			DbHelperSQL.RunProcedure("Proc_Tb_Management_Type_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.Management.Tb_Management_Type model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@Code", SqlDbType.VarChar,36),
					new SqlParameter("@SortCode", SqlDbType.VarChar,36),
					new SqlParameter("@Sort", SqlDbType.Int,4),
					new SqlParameter("@TypeName", SqlDbType.VarChar,36),
					new SqlParameter("@TypeCode", SqlDbType.VarChar,36),
					new SqlParameter("@Memo", SqlDbType.VarChar,3999),
					new SqlParameter("@IsDelete", SqlDbType.Int,4)};
			parameters[0].Value = model.Code;
			parameters[1].Value = model.SortCode;
			parameters[2].Value = model.Sort;
			parameters[3].Value = model.TypeName;
			parameters[4].Value = model.TypeCode;
			parameters[5].Value = model.Memo;
			parameters[6].Value = model.IsDelete;

			DbHelperSQL.RunProcedure("Proc_Tb_Management_Type_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(string SortCode)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@SortCode", SqlDbType.VarChar,50)};
			parameters[0].Value = SortCode;

			DbHelperSQL.RunProcedure("Proc_Tb_Management_Type_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.Management.Tb_Management_Type GetModel(string SortCode)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@SortCode", SqlDbType.VarChar,50)};
			parameters[0].Value = SortCode;

			MobileSoft.Model.Management.Tb_Management_Type model=new MobileSoft.Model.Management.Tb_Management_Type();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_Management_Type_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				model.Code=ds.Tables[0].Rows[0]["Code"].ToString();
				model.SortCode=ds.Tables[0].Rows[0]["SortCode"].ToString();
				if(ds.Tables[0].Rows[0]["Sort"].ToString()!="")
				{
					model.Sort=int.Parse(ds.Tables[0].Rows[0]["Sort"].ToString());
				}
				model.TypeName=ds.Tables[0].Rows[0]["TypeName"].ToString();
				model.TypeCode=ds.Tables[0].Rows[0]["TypeCode"].ToString();
				model.Memo=ds.Tables[0].Rows[0]["Memo"].ToString();
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
			strSql.Append("select Code,SortCode,Sort,TypeName,TypeCode,Memo,IsDelete ");
			strSql.Append(" FROM Tb_Management_Type ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// ���ǰ��������
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" Code,SortCode,Sort,TypeName,TypeCode,Memo,IsDelete ");
			strSql.Append(" FROM Tb_Management_Type ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
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
			parameters[5].Value = "SELECT * FROM Tb_Management_Type WHERE 1=1 " + StrCondition;
			parameters[6].Value = "SortCode";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  ��Ա����
	}
}

