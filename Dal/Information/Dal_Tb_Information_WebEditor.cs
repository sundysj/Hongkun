using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//�����������
namespace MobileSoft.DAL.Information
{
	/// <summary>
	/// ���ݷ�����Dal_Tb_Information_WebEditor��
	/// </summary>
	public class Dal_Tb_Information_WebEditor
	{
		public Dal_Tb_Information_WebEditor()
		{
            DbHelperSQL.ConnectionString = PubConstant.SQMBSContionString;
		}
		#region  ��Ա����

		/// <summary>
		/// �õ����ID
		/// </summary>
		public long GetMaxId()
		{
		return DbHelperSQL.GetMaxID("ID", "Tb_Information_WebEditor"); 
		}

		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(long ID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt)};
			parameters[0].Value = ID;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_Information_WebEditor_Exists",parameters,out rowsAffected);
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
		public long Add(MobileSoft.Model.Information.Tb_Information_WebEditor model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt,8),
					new SqlParameter("@BussId", SqlDbType.BigInt,8),
					new SqlParameter("@WebContent", SqlDbType.NVarChar),
					new SqlParameter("@Image1", SqlDbType.NVarChar,500),
					new SqlParameter("@Image2", SqlDbType.NVarChar,500),
					new SqlParameter("@Image3", SqlDbType.NVarChar,500)};
			parameters[0].Direction = ParameterDirection.Output;
			parameters[1].Value = model.BussId;
			parameters[2].Value = model.WebContent;
			parameters[3].Value = model.Image1;
			parameters[4].Value = model.Image2;
			parameters[5].Value = model.Image3;

			DbHelperSQL.RunProcedure("Proc_Tb_Information_WebEditor_ADD",parameters,out rowsAffected);
			return (long)parameters[0].Value;
		}

		/// <summary>
		///  ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.Information.Tb_Information_WebEditor model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt,8),
					new SqlParameter("@BussId", SqlDbType.BigInt,8),
					new SqlParameter("@WebContent", SqlDbType.NVarChar),
					new SqlParameter("@Image1", SqlDbType.NVarChar,500),
					new SqlParameter("@Image2", SqlDbType.NVarChar,500),
					new SqlParameter("@Image3", SqlDbType.NVarChar,500)};
			parameters[0].Value = model.ID;
			parameters[1].Value = model.BussId;
			parameters[2].Value = model.WebContent;
			parameters[3].Value = model.Image1;
			parameters[4].Value = model.Image2;
			parameters[5].Value = model.Image3;

			DbHelperSQL.RunProcedure("Proc_Tb_Information_WebEditor_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(long ID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt)};
			parameters[0].Value = ID;

			DbHelperSQL.RunProcedure("Proc_Tb_Information_WebEditor_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.Information.Tb_Information_WebEditor GetModel(long ID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt)};
			parameters[0].Value = ID;

			MobileSoft.Model.Information.Tb_Information_WebEditor model=new MobileSoft.Model.Information.Tb_Information_WebEditor();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_Information_WebEditor_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ID"].ToString()!="")
				{
					model.ID=long.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["BussId"].ToString()!="")
				{
					model.BussId=long.Parse(ds.Tables[0].Rows[0]["BussId"].ToString());
				}
				model.WebContent=ds.Tables[0].Rows[0]["WebContent"].ToString();
				model.Image1=ds.Tables[0].Rows[0]["Image1"].ToString();
				model.Image2=ds.Tables[0].Rows[0]["Image2"].ToString();
				model.Image3=ds.Tables[0].Rows[0]["Image3"].ToString();
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
			strSql.Append("select ID,BussId,WebContent,Image1,Image2,Image3 ");
			strSql.Append(" FROM Tb_Information_WebEditor ");
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
			strSql.Append(" ID,BussId,WebContent,Image1,Image2,Image3 ");
			strSql.Append(" FROM Tb_Information_WebEditor ");
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
			parameters[5].Value = "SELECT * FROM Tb_Information_WebEditor WHERE 1=1 " + StrCondition;
			parameters[6].Value = "ID";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  ��Ա����
	}
}

