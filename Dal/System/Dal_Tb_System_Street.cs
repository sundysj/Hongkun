using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//�����������
namespace MobileSoft.DAL.System
{
	/// <summary>
	/// ���ݷ�����Dal_Tb_System_Street��
	/// </summary>
	public class Dal_Tb_System_Street
	{
		public Dal_Tb_System_Street()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  ��Ա����

		/// <summary>
		/// �õ����ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("StreetID", "Tb_System_Street"); 
		}

		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(int StreetID,int BoroughID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@StreetID", SqlDbType.Int,4),
					new SqlParameter("@BoroughID", SqlDbType.Int,4)};
			parameters[0].Value = StreetID;
			parameters[1].Value = BoroughID;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_System_Street_Exists",parameters,out rowsAffected);
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
		public void Add(MobileSoft.Model.System.Tb_System_Street model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@StreetID", SqlDbType.Int,4),
					new SqlParameter("@BoroughID", SqlDbType.Int,4),
					new SqlParameter("@StreetName", SqlDbType.NVarChar,200)};
			parameters[0].Value = model.StreetID;
			parameters[1].Value = model.BoroughID;
			parameters[2].Value = model.StreetName;

			DbHelperSQL.RunProcedure("Proc_Tb_System_Street_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.System.Tb_System_Street model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@StreetID", SqlDbType.Int,4),
					new SqlParameter("@BoroughID", SqlDbType.Int,4),
					new SqlParameter("@StreetName", SqlDbType.NVarChar,200)};
			parameters[0].Value = model.StreetID;
			parameters[1].Value = model.BoroughID;
			parameters[2].Value = model.StreetName;

			DbHelperSQL.RunProcedure("Proc_Tb_System_Street_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(int StreetID,int BoroughID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@StreetID", SqlDbType.Int,4),
					new SqlParameter("@BoroughID", SqlDbType.Int,4)};
			parameters[0].Value = StreetID;
			parameters[1].Value = BoroughID;

			DbHelperSQL.RunProcedure("Proc_Tb_System_Street_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.System.Tb_System_Street GetModel(int StreetID,int BoroughID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@StreetID", SqlDbType.Int,4),
					new SqlParameter("@BoroughID", SqlDbType.Int,4)};
			parameters[0].Value = StreetID;
			parameters[1].Value = BoroughID;

			MobileSoft.Model.System.Tb_System_Street model=new MobileSoft.Model.System.Tb_System_Street();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_System_Street_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["StreetID"].ToString()!="")
				{
					model.StreetID=int.Parse(ds.Tables[0].Rows[0]["StreetID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["BoroughID"].ToString()!="")
				{
					model.BoroughID=int.Parse(ds.Tables[0].Rows[0]["BoroughID"].ToString());
				}
				model.StreetName=ds.Tables[0].Rows[0]["StreetName"].ToString();
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
			strSql.Append("select StreetID,BoroughID,StreetName ");
			strSql.Append(" FROM Tb_System_Street ");
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
			strSql.Append(" StreetID,BoroughID,StreetName ");
			strSql.Append(" FROM Tb_System_Street ");
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
			parameters[5].Value = "SELECT * FROM Tb_System_Street WHERE 1=1 " + StrCondition;
			parameters[6].Value = "StreetID,BoroughID";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  ��Ա����
	}
}

