using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.OAPublicWork
{
	/// <summary>
	/// 数据访问类Dal_Tb_OAPublicWork_BackupFile。
	/// </summary>
	public class Dal_Tb_OAPublicWork_BackupFile
	{
		public Dal_Tb_OAPublicWork_BackupFile()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string InfoCode)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoCode", SqlDbType.VarChar,50)};
			parameters[0].Value = InfoCode;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_BackupFile_Exists",parameters,out rowsAffected);
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
		public void Add(MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_BackupFile model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoCode", SqlDbType.VarChar,50),
					new SqlParameter("@FName", SqlDbType.VarChar,200),
					new SqlParameter("@OriginallyFilePath", SqlDbType.VarChar,1000),
					new SqlParameter("@PresentFilePath", SqlDbType.VarChar,1000),
					new SqlParameter("@BackupDate", SqlDbType.DateTime),
					new SqlParameter("@RestoreUserName", SqlDbType.VarChar,50),
					new SqlParameter("@RestoreDate", SqlDbType.DateTime),
					new SqlParameter("@IsDelete", SqlDbType.Int,4)};
			parameters[0].Value = model.InfoCode;
			parameters[1].Value = model.FName;
			parameters[2].Value = model.OriginallyFilePath;
			parameters[3].Value = model.PresentFilePath;
			parameters[4].Value = model.BackupDate;
			parameters[5].Value = model.RestoreUserName;
			parameters[6].Value = model.RestoreDate;
			parameters[7].Value = model.IsDelete;

			DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_BackupFile_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_BackupFile model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoCode", SqlDbType.VarChar,50),
					new SqlParameter("@FName", SqlDbType.VarChar,200),
					new SqlParameter("@OriginallyFilePath", SqlDbType.VarChar,1000),
					new SqlParameter("@PresentFilePath", SqlDbType.VarChar,1000),
					new SqlParameter("@BackupDate", SqlDbType.DateTime),
					new SqlParameter("@RestoreUserName", SqlDbType.VarChar,50),
					new SqlParameter("@RestoreDate", SqlDbType.DateTime),
					new SqlParameter("@IsDelete", SqlDbType.Int,4)};
			parameters[0].Value = model.InfoCode;
			parameters[1].Value = model.FName;
			parameters[2].Value = model.OriginallyFilePath;
			parameters[3].Value = model.PresentFilePath;
			parameters[4].Value = model.BackupDate;
			parameters[5].Value = model.RestoreUserName;
			parameters[6].Value = model.RestoreDate;
			parameters[7].Value = model.IsDelete;

			DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_BackupFile_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(string InfoCode)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoCode", SqlDbType.VarChar,50)};
			parameters[0].Value = InfoCode;

			DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_BackupFile_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_BackupFile GetModel(string InfoCode)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@InfoCode", SqlDbType.VarChar,50)};
			parameters[0].Value = InfoCode;

			MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_BackupFile model=new MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_BackupFile();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_BackupFile_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				model.InfoCode=ds.Tables[0].Rows[0]["InfoCode"].ToString();
				model.FName=ds.Tables[0].Rows[0]["FName"].ToString();
				model.OriginallyFilePath=ds.Tables[0].Rows[0]["OriginallyFilePath"].ToString();
				model.PresentFilePath=ds.Tables[0].Rows[0]["PresentFilePath"].ToString();
				if(ds.Tables[0].Rows[0]["BackupDate"].ToString()!="")
				{
					model.BackupDate=DateTime.Parse(ds.Tables[0].Rows[0]["BackupDate"].ToString());
				}
				model.RestoreUserName=ds.Tables[0].Rows[0]["RestoreUserName"].ToString();
				if(ds.Tables[0].Rows[0]["RestoreDate"].ToString()!="")
				{
					model.RestoreDate=DateTime.Parse(ds.Tables[0].Rows[0]["RestoreDate"].ToString());
				}
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
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select InfoCode,FName,OriginallyFilePath,PresentFilePath,BackupDate,RestoreUserName,RestoreDate,IsDelete ");
			strSql.Append(" FROM Tb_OAPublicWork_BackupFile ");
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
			strSql.Append(" InfoCode,FName,OriginallyFilePath,PresentFilePath,BackupDate,RestoreUserName,RestoreDate,IsDelete ");
			strSql.Append(" FROM Tb_OAPublicWork_BackupFile ");
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
			parameters[5].Value = "SELECT * FROM Tb_OAPublicWork_BackupFile WHERE 1=1 " + StrCondition;
			parameters[6].Value = "InfoCode";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

