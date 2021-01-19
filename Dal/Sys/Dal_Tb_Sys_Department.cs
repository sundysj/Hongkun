using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//�����������
namespace MobileSoft.DAL.Sys
{
	/// <summary>
	/// ���ݷ�����Dal_Tb_Sys_Department��
	/// </summary>
	public class Dal_Tb_Sys_Department
	{
		public Dal_Tb_Sys_Department()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  ��Ա����

		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(string DepCode)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@DepCode", SqlDbType.NVarChar,50)};
			parameters[0].Value = DepCode;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_Sys_Department_Exists",parameters,out rowsAffected);
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
		public void Add(MobileSoft.Model.Sys.Tb_Sys_Department model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@DepCode", SqlDbType.NVarChar,20),
					new SqlParameter("@DepName", SqlDbType.NVarChar,50),
					new SqlParameter("@Principal", SqlDbType.NVarChar,20),
					new SqlParameter("@Memo", SqlDbType.NVarChar,1000),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2),
					new SqlParameter("@Sort", SqlDbType.Int,4),
					new SqlParameter("@IsHide", SqlDbType.SmallInt,2)};
			parameters[0].Value = model.DepCode;
			parameters[1].Value = model.DepName;
			parameters[2].Value = model.Principal;
			parameters[3].Value = model.Memo;
			parameters[4].Value = model.CommID;
			parameters[5].Value = model.IsDelete;
			parameters[6].Value = model.Sort;
			parameters[7].Value = model.IsHide;

			DbHelperSQL.RunProcedure("Proc_Tb_Sys_Department_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.Sys.Tb_Sys_Department model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@DepCode", SqlDbType.NVarChar,20),
					new SqlParameter("@DepName", SqlDbType.NVarChar,50),
					new SqlParameter("@Principal", SqlDbType.NVarChar,20),
					new SqlParameter("@Memo", SqlDbType.NVarChar,1000),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2),
					new SqlParameter("@Sort", SqlDbType.Int,4),
					new SqlParameter("@IsHide", SqlDbType.SmallInt,2)};
			parameters[0].Value = model.DepCode;
			parameters[1].Value = model.DepName;
			parameters[2].Value = model.Principal;
			parameters[3].Value = model.Memo;
			parameters[4].Value = model.CommID;
			parameters[5].Value = model.IsDelete;
			parameters[6].Value = model.Sort;
			parameters[7].Value = model.IsHide;

			DbHelperSQL.RunProcedure("Proc_Tb_Sys_Department_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(string DepCode)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@DepCode", SqlDbType.NVarChar,50)};
			parameters[0].Value = DepCode;

			DbHelperSQL.RunProcedure("Proc_Tb_Sys_Department_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.Sys.Tb_Sys_Department GetModel(string DepCode)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@DepCode", SqlDbType.NVarChar,50)};
			parameters[0].Value = DepCode;

			MobileSoft.Model.Sys.Tb_Sys_Department model=new MobileSoft.Model.Sys.Tb_Sys_Department();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_Sys_Department_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				model.DepCode=ds.Tables[0].Rows[0]["DepCode"].ToString();
				model.DepName=ds.Tables[0].Rows[0]["DepName"].ToString();
				model.Principal=ds.Tables[0].Rows[0]["Principal"].ToString();
				model.Memo=ds.Tables[0].Rows[0]["Memo"].ToString();
				if(ds.Tables[0].Rows[0]["CommID"].ToString()!="")
				{
					model.CommID=int.Parse(ds.Tables[0].Rows[0]["CommID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsDelete"].ToString()!="")
				{
					model.IsDelete=int.Parse(ds.Tables[0].Rows[0]["IsDelete"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Sort"].ToString()!="")
				{
					model.Sort=int.Parse(ds.Tables[0].Rows[0]["Sort"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsHide"].ToString()!="")
				{
					model.IsHide=int.Parse(ds.Tables[0].Rows[0]["IsHide"].ToString());
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
			strSql.Append("select DepCode,DepName,Principal,Memo,CommID,IsDelete,Sort,IsHide ");
			strSql.Append(" FROM Tb_Sys_Department ");
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
			strSql.Append(" DepCode,DepName,Principal,Memo,CommID,IsDelete,Sort,IsHide ");
			strSql.Append(" FROM Tb_Sys_Department ");
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
			parameters[5].Value = "SELECT * FROM Tb_Sys_Department WHERE 1=1 " + StrCondition;
			parameters[6].Value = "DepCode";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  ��Ա����
	}
}

