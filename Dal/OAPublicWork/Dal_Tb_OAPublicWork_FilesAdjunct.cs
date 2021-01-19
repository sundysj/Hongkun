using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.OAPublicWork
{
	/// <summary>
	/// 数据访问类Dal_Tb_OAPublicWork_FilesAdjunct。
	/// </summary>
	public class Dal_Tb_OAPublicWork_FilesAdjunct
	{
		public Dal_Tb_OAPublicWork_FilesAdjunct()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long InfoId)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.BigInt)};
			parameters[0].Value = InfoId;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_FilesAdjunct_Exists",parameters,out rowsAffected);
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
		///  增加一条数据
		/// </summary>
		public int Add(MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_FilesAdjunct model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.BigInt,8),
					new SqlParameter("@DictionaryCode", SqlDbType.NVarChar,20),
					new SqlParameter("@InstanceId", SqlDbType.Int,4),
					new SqlParameter("@UserFilesCode", SqlDbType.NVarChar,20),
					new SqlParameter("@AdjunctName", SqlDbType.NVarChar,100),
					new SqlParameter("@FilPath", SqlDbType.NVarChar,300),
					new SqlParameter("@FileExName", SqlDbType.NVarChar,10),
					new SqlParameter("@FileSize", SqlDbType.NVarChar,20),
					new SqlParameter("@IsCanDown", SqlDbType.NVarChar,50)};
			parameters[0].Direction = ParameterDirection.Output;
			parameters[1].Value = model.DictionaryCode;
			parameters[2].Value = model.InstanceId;
			parameters[3].Value = model.UserFilesCode;
			parameters[4].Value = model.AdjunctName;
			parameters[5].Value = model.FilPath;
			parameters[6].Value = model.FileExName;
			parameters[7].Value = model.FileSize;
			parameters[8].Value = model.IsCanDown;

			DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_FilesAdjunct_ADD",parameters,out rowsAffected);
			return (int)parameters[0].Value;
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_FilesAdjunct model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.BigInt,8),
					new SqlParameter("@DictionaryCode", SqlDbType.NVarChar,20),
					new SqlParameter("@InstanceId", SqlDbType.Int,4),
					new SqlParameter("@UserFilesCode", SqlDbType.NVarChar,20),
					new SqlParameter("@AdjunctName", SqlDbType.NVarChar,100),
					new SqlParameter("@FilPath", SqlDbType.NVarChar,300),
					new SqlParameter("@FileExName", SqlDbType.NVarChar,10),
					new SqlParameter("@FileSize", SqlDbType.NVarChar,20),
					new SqlParameter("@IsCanDown", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.InfoId;
			parameters[1].Value = model.DictionaryCode;
			parameters[2].Value = model.InstanceId;
			parameters[3].Value = model.UserFilesCode;
			parameters[4].Value = model.AdjunctName;
			parameters[5].Value = model.FilPath;
			parameters[6].Value = model.FileExName;
			parameters[7].Value = model.FileSize;
			parameters[8].Value = model.IsCanDown;

			DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_FilesAdjunct_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long InfoId)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.BigInt)};
			parameters[0].Value = InfoId;

			DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_FilesAdjunct_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_FilesAdjunct GetModel(long InfoId)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.BigInt)};
			parameters[0].Value = InfoId;

			MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_FilesAdjunct model=new MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_FilesAdjunct();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_FilesAdjunct_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["InfoId"].ToString()!="")
				{
					model.InfoId=long.Parse(ds.Tables[0].Rows[0]["InfoId"].ToString());
				}
				model.DictionaryCode=ds.Tables[0].Rows[0]["DictionaryCode"].ToString();
				if(ds.Tables[0].Rows[0]["InstanceId"].ToString()!="")
				{
					model.InstanceId=int.Parse(ds.Tables[0].Rows[0]["InstanceId"].ToString());
				}
				model.UserFilesCode=ds.Tables[0].Rows[0]["UserFilesCode"].ToString();
				model.AdjunctName=ds.Tables[0].Rows[0]["AdjunctName"].ToString();
				model.FilPath=ds.Tables[0].Rows[0]["FilPath"].ToString();
				model.FileExName=ds.Tables[0].Rows[0]["FileExName"].ToString();
				model.FileSize=ds.Tables[0].Rows[0]["FileSize"].ToString();
				model.IsCanDown=ds.Tables[0].Rows[0]["IsCanDown"].ToString();
				return model;
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select InfoId,DictionaryCode,InstanceId,UserFilesCode,AdjunctName,FilPath,FileExName,FileSize,IsCanDown ");
			strSql.Append(" FROM Tb_OAPublicWork_FilesAdjunct ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string fieldOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" InfoId,DictionaryCode,InstanceId,UserFilesCode,AdjunctName,FilPath,FileExName,FileSize,IsCanDown ");
			strSql.Append(" FROM Tb_OAPublicWork_FilesAdjunct ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + fieldOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		
		/// <summary>
		/// 分页获取数据列表
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
			parameters[5].Value = "SELECT * FROM Tb_OAPublicWork_FilesAdjunct WHERE 1=1 " + StrCondition;
			parameters[6].Value = "InfoId";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法

            public DataTable OAPublicWork_FilesAdjunct_GetFilter(string InstanceId, string DictionaryCode)
            {
                  SqlParameter[] parameters = {
					new SqlParameter("@InstanceId", SqlDbType.VarChar,50),
                              new SqlParameter("@DictionaryCode", SqlDbType.VarChar,50)
                                              };
                  parameters[0].Value = InstanceId;
                  parameters[1].Value = DictionaryCode;

                  DataTable dTable = DbHelperSQL.RunProcedure("Proc_WorkFlow_GetFilesAdjunct", parameters, "RetDataSet").Tables[0];

                  return dTable;
            }

	}
}

