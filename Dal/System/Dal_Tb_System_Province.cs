using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//�����������
namespace MobileSoft.DAL.System
{
	/// <summary>
	/// ���ݷ�����Dal_Tb_System_Province��
	/// </summary>
	public class Dal_Tb_System_Province
	{
		public Dal_Tb_System_Province()
		{
                  DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  ��Ա����

		/// <summary>
		/// �õ����ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("ProvinceID", "Tb_System_Province"); 
		}

		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(int ProvinceID,string ProvinceName)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ProvinceID", SqlDbType.Int,4),
					new SqlParameter("@ProvinceName", SqlDbType.NVarChar,50)};
			parameters[0].Value = ProvinceID;
			parameters[1].Value = ProvinceName;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_System_Province_Exists",parameters,out rowsAffected);
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
		public void Add(MobileSoft.Model.System.Tb_System_Province model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ProvinceID", SqlDbType.Int,4),
					new SqlParameter("@ProvinceName", SqlDbType.NVarChar,30)};
			parameters[0].Value = model.ProvinceID;
			parameters[1].Value = model.ProvinceName;

			DbHelperSQL.RunProcedure("Proc_Tb_System_Province_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.System.Tb_System_Province model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ProvinceID", SqlDbType.Int,4),
					new SqlParameter("@ProvinceName", SqlDbType.NVarChar,30)};
			parameters[0].Value = model.ProvinceID;
			parameters[1].Value = model.ProvinceName;

			DbHelperSQL.RunProcedure("Proc_Tb_System_Province_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(int ProvinceID,string ProvinceName)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ProvinceID", SqlDbType.Int,4),
					new SqlParameter("@ProvinceName", SqlDbType.NVarChar,50)};
			parameters[0].Value = ProvinceID;
			parameters[1].Value = ProvinceName;

			DbHelperSQL.RunProcedure("Proc_Tb_System_Province_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.System.Tb_System_Province GetModel(int ProvinceID,string ProvinceName)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@ProvinceID", SqlDbType.Int,4),
					new SqlParameter("@ProvinceName", SqlDbType.NVarChar,50)};
			parameters[0].Value = ProvinceID;
			parameters[1].Value = ProvinceName;

			MobileSoft.Model.System.Tb_System_Province model=new MobileSoft.Model.System.Tb_System_Province();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_System_Province_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ProvinceID"].ToString()!="")
				{
					model.ProvinceID=int.Parse(ds.Tables[0].Rows[0]["ProvinceID"].ToString());
				}
				model.ProvinceName=ds.Tables[0].Rows[0]["ProvinceName"].ToString();
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
			strSql.Append("select ProvinceID,ProvinceName ");
			strSql.Append(" FROM Tb_System_Province ");
			if(strWhere.Trim()!="")
			{
                strSql.Append(" where " + strWhere + " ORDER BY ProvinceID ASC");
			}

            strSql.Append(" ORDER BY ProvinceID ASC");
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
			strSql.Append(" ProvinceID,ProvinceName ");
			strSql.Append(" FROM Tb_System_Province ");
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
			parameters[5].Value = "SELECT * FROM Tb_System_Province WHERE 1=1 " + StrCondition;
			parameters[6].Value = "ProvinceID,ProvinceName";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  ��Ա����
	}
}

