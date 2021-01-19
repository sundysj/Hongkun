using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//�����������
namespace MobileSoft.DAL.SQMSys
{
	/// <summary>
	/// ���ݷ�����Dal_Tb_Sys_MessageAdjunct��
	/// </summary>
	public class Dal_Tb_Sys_MessageAdjunct
	{
		public Dal_Tb_Sys_MessageAdjunct()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  ��Ա����

		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(string AdjunctCode)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@AdjunctCode", SqlDbType.NVarChar,50)};
			parameters[0].Value = AdjunctCode;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_Sys_MessageAdjunct_Exists",parameters,out rowsAffected);
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
		public void Add(MobileSoft.Model.SQMSys.Tb_Sys_MessageAdjunct model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@AdjunctCode", SqlDbType.NVarChar,20),
					new SqlParameter("@MessageCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@AdjunctName", SqlDbType.NVarChar,100),
					new SqlParameter("@FilPath", SqlDbType.NVarChar,300),
					new SqlParameter("@FileName", SqlDbType.NVarChar,20),
					new SqlParameter("@FileExName", SqlDbType.NVarChar,10),
					new SqlParameter("@FileSize", SqlDbType.Decimal,9)};
			parameters[0].Value = model.AdjunctCode;
			parameters[1].Value = model.MessageCode;
			parameters[2].Value = model.AdjunctName;
			parameters[3].Value = model.FilPath;
			parameters[4].Value = model.FileName;
			parameters[5].Value = model.FileExName;
			parameters[6].Value = model.FileSize;

			DbHelperSQL.RunProcedure("Proc_Tb_Sys_MessageAdjunct_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.SQMSys.Tb_Sys_MessageAdjunct model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@AdjunctCode", SqlDbType.NVarChar,20),
					new SqlParameter("@MessageCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@AdjunctName", SqlDbType.NVarChar,100),
					new SqlParameter("@FilPath", SqlDbType.NVarChar,300),
					new SqlParameter("@FileName", SqlDbType.NVarChar,20),
					new SqlParameter("@FileExName", SqlDbType.NVarChar,10),
					new SqlParameter("@FileSize", SqlDbType.Decimal,9)};
			parameters[0].Value = model.AdjunctCode;
			parameters[1].Value = model.MessageCode;
			parameters[2].Value = model.AdjunctName;
			parameters[3].Value = model.FilPath;
			parameters[4].Value = model.FileName;
			parameters[5].Value = model.FileExName;
			parameters[6].Value = model.FileSize;

			DbHelperSQL.RunProcedure("Proc_Tb_Sys_MessageAdjunct_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(string AdjunctCode)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@AdjunctCode", SqlDbType.NVarChar,50)};
			parameters[0].Value = AdjunctCode;

			DbHelperSQL.RunProcedure("Proc_Tb_Sys_MessageAdjunct_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.SQMSys.Tb_Sys_MessageAdjunct GetModel(string AdjunctCode)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@AdjunctCode", SqlDbType.NVarChar,50)};
			parameters[0].Value = AdjunctCode;

			MobileSoft.Model.SQMSys.Tb_Sys_MessageAdjunct model=new MobileSoft.Model.SQMSys.Tb_Sys_MessageAdjunct();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_Sys_MessageAdjunct_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				model.AdjunctCode=ds.Tables[0].Rows[0]["AdjunctCode"].ToString();
				if(ds.Tables[0].Rows[0]["MessageCode"].ToString()!="")
				{
					model.MessageCode=new Guid(ds.Tables[0].Rows[0]["MessageCode"].ToString());
				}
				model.AdjunctName=ds.Tables[0].Rows[0]["AdjunctName"].ToString();
				model.FilPath=ds.Tables[0].Rows[0]["FilPath"].ToString();
				model.FileName=ds.Tables[0].Rows[0]["FileName"].ToString();
				model.FileExName=ds.Tables[0].Rows[0]["FileExName"].ToString();
				if(ds.Tables[0].Rows[0]["FileSize"].ToString()!="")
				{
					model.FileSize=decimal.Parse(ds.Tables[0].Rows[0]["FileSize"].ToString());
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
			strSql.Append("select AdjunctCode,MessageCode,AdjunctName,FilPath,FileName,FileExName,FileSize ");
			strSql.Append(" FROM Tb_Sys_MessageAdjunct ");
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
			strSql.Append(" AdjunctCode,MessageCode,AdjunctName,FilPath,FileName,FileExName,FileSize ");
			strSql.Append(" FROM Tb_Sys_MessageAdjunct ");
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
			parameters[5].Value = "SELECT * FROM Tb_Sys_MessageAdjunct WHERE 1=1 " + StrCondition;
			parameters[6].Value = "AdjunctCode";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  ��Ա����
	}
}

