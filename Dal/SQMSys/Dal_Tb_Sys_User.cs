using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.SQMSys
{
	/// <summary>
	/// 数据访问类Dal_Tb_Sys_User。
	/// </summary>
	public class Dal_Tb_Sys_User
	{
		public Dal_Tb_Sys_User()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string UserCode)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@UserCode", SqlDbType.NVarChar,50)};
			parameters[0].Value = UserCode;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_Sys_User_Exists",parameters,out rowsAffected);
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
		public void Add(MobileSoft.Model.SQMSys.Tb_Sys_User model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@UserCode", SqlDbType.NVarChar,20),
					new SqlParameter("@StreetCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@UserName", SqlDbType.NVarChar,50),
					new SqlParameter("@LoginCode", SqlDbType.NVarChar,20),
					new SqlParameter("@PassWord", SqlDbType.NVarChar,50),
					new SqlParameter("@DepCode", SqlDbType.NVarChar,20),
					new SqlParameter("@EmployeeCode", SqlDbType.NVarChar,20),
					new SqlParameter("@MobileTel", SqlDbType.NVarChar,50),
					new SqlParameter("@Memo", SqlDbType.NVarChar,1000),
					new SqlParameter("@IsMobile", SqlDbType.SmallInt,2),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2),
					new SqlParameter("@DeleteDate", SqlDbType.DateTime)};
			parameters[0].Value = model.UserCode;
			parameters[1].Value = model.StreetCode;
			parameters[2].Value = model.UserName;
			parameters[3].Value = model.LoginCode;
			parameters[4].Value = model.PassWord;
			parameters[5].Value = model.DepCode;
			parameters[6].Value = model.EmployeeCode;
			parameters[7].Value = model.MobileTel;
			parameters[8].Value = model.Memo;
			parameters[9].Value = model.IsMobile;
			parameters[10].Value = model.IsDelete;
			parameters[11].Value = model.DeleteDate;

			DbHelperSQL.RunProcedure("Proc_Tb_Sys_User_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.SQMSys.Tb_Sys_User model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@UserCode", SqlDbType.NVarChar,20),
					new SqlParameter("@StreetCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@UserName", SqlDbType.NVarChar,50),
					new SqlParameter("@LoginCode", SqlDbType.NVarChar,20),
					new SqlParameter("@PassWord", SqlDbType.NVarChar,50),
					new SqlParameter("@DepCode", SqlDbType.NVarChar,20),
					new SqlParameter("@EmployeeCode", SqlDbType.NVarChar,20),
					new SqlParameter("@MobileTel", SqlDbType.NVarChar,50),
					new SqlParameter("@Memo", SqlDbType.NVarChar,1000),
					new SqlParameter("@IsMobile", SqlDbType.SmallInt,2),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2),
					new SqlParameter("@DeleteDate", SqlDbType.DateTime)};
			parameters[0].Value = model.UserCode;
			parameters[1].Value = model.StreetCode;
			parameters[2].Value = model.UserName;
			parameters[3].Value = model.LoginCode;
			parameters[4].Value = model.PassWord;
			parameters[5].Value = model.DepCode;
			parameters[6].Value = model.EmployeeCode;
			parameters[7].Value = model.MobileTel;
			parameters[8].Value = model.Memo;
			parameters[9].Value = model.IsMobile;
			parameters[10].Value = model.IsDelete;
			parameters[11].Value = model.DeleteDate;

			DbHelperSQL.RunProcedure("Proc_Tb_Sys_User_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(string UserCode)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@UserCode", SqlDbType.NVarChar,50)};
			parameters[0].Value = UserCode;

			DbHelperSQL.RunProcedure("Proc_Tb_Sys_User_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.SQMSys.Tb_Sys_User GetModel(string UserCode)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@UserCode", SqlDbType.NVarChar,50)};
			parameters[0].Value = UserCode;

			MobileSoft.Model.SQMSys.Tb_Sys_User model=new MobileSoft.Model.SQMSys.Tb_Sys_User();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_Sys_User_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				model.UserCode=ds.Tables[0].Rows[0]["UserCode"].ToString();
				if(ds.Tables[0].Rows[0]["StreetCode"].ToString()!="")
				{
					model.StreetCode=new Guid(ds.Tables[0].Rows[0]["StreetCode"].ToString());
				}
				model.UserName=ds.Tables[0].Rows[0]["UserName"].ToString();
				model.LoginCode=ds.Tables[0].Rows[0]["LoginCode"].ToString();
				model.PassWord=ds.Tables[0].Rows[0]["PassWord"].ToString();
				model.DepCode=ds.Tables[0].Rows[0]["DepCode"].ToString();
				model.EmployeeCode=ds.Tables[0].Rows[0]["EmployeeCode"].ToString();
				model.MobileTel=ds.Tables[0].Rows[0]["MobileTel"].ToString();
				model.Memo=ds.Tables[0].Rows[0]["Memo"].ToString();
				if(ds.Tables[0].Rows[0]["IsMobile"].ToString()!="")
				{
					model.IsMobile=int.Parse(ds.Tables[0].Rows[0]["IsMobile"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsDelete"].ToString()!="")
				{
					model.IsDelete=int.Parse(ds.Tables[0].Rows[0]["IsDelete"].ToString());
				}
				if(ds.Tables[0].Rows[0]["DeleteDate"].ToString()!="")
				{
					model.DeleteDate=DateTime.Parse(ds.Tables[0].Rows[0]["DeleteDate"].ToString());
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
			strSql.Append("select UserCode,StreetCode,UserName,LoginCode,PassWord,DepCode,EmployeeCode,MobileTel,Memo,IsMobile,IsDelete,DeleteDate ");
			strSql.Append(" FROM Tb_Sys_User ");
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
			strSql.Append(" UserCode,StreetCode,UserName,LoginCode,PassWord,DepCode,EmployeeCode,MobileTel,Memo,IsMobile,IsDelete,DeleteDate ");
			strSql.Append(" FROM Tb_Sys_User ");
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
			parameters[5].Value = "SELECT * FROM Tb_Sys_User WHERE 1=1 " + StrCondition;
			parameters[6].Value = "UserCode";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

