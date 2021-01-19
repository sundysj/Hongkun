using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.SQMSys
{
	/// <summary>
	/// 数据访问类Dal_Tb_Sys_UserCalendar。
	/// </summary>
	public class Dal_Tb_Sys_UserCalendar
	{
		public Dal_Tb_Sys_UserCalendar()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(Guid UserCalendarCode)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@UserCalendarCode", SqlDbType.UniqueIdentifier)};
			parameters[0].Value = UserCalendarCode;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_Sys_UserCalendar_Exists",parameters,out rowsAffected);
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
		public void Add(MobileSoft.Model.SQMSys.Tb_Sys_UserCalendar model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@UserCalendarCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@UserCode", SqlDbType.NVarChar,20),
					new SqlParameter("@Title", SqlDbType.NVarChar,200),
					new SqlParameter("@Place", SqlDbType.NVarChar,200),
					new SqlParameter("@StartTime", SqlDbType.DateTime),
					new SqlParameter("@EndTime", SqlDbType.DateTime),
					new SqlParameter("@Scratchpad", SqlDbType.NText),
					new SqlParameter("@IsRemind", SqlDbType.SmallInt,2),
					new SqlParameter("@RemindHours", SqlDbType.Decimal,9),
					new SqlParameter("@RemindState", SqlDbType.SmallInt,2)};
			parameters[0].Value = model.UserCalendarCode;
			parameters[1].Value = model.UserCode;
			parameters[2].Value = model.Title;
			parameters[3].Value = model.Place;
			parameters[4].Value = model.StartTime;
			parameters[5].Value = model.EndTime;
			parameters[6].Value = model.Scratchpad;
			parameters[7].Value = model.IsRemind;
			parameters[8].Value = model.RemindHours;
			parameters[9].Value = model.RemindState;

			DbHelperSQL.RunProcedure("Proc_Tb_Sys_UserCalendar_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.SQMSys.Tb_Sys_UserCalendar model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@UserCalendarCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@UserCode", SqlDbType.NVarChar,20),
					new SqlParameter("@Title", SqlDbType.NVarChar,200),
					new SqlParameter("@Place", SqlDbType.NVarChar,200),
					new SqlParameter("@StartTime", SqlDbType.DateTime),
					new SqlParameter("@EndTime", SqlDbType.DateTime),
					new SqlParameter("@Scratchpad", SqlDbType.NText),
					new SqlParameter("@IsRemind", SqlDbType.SmallInt,2),
					new SqlParameter("@RemindHours", SqlDbType.Decimal,9),
					new SqlParameter("@RemindState", SqlDbType.SmallInt,2)};
			parameters[0].Value = model.UserCalendarCode;
			parameters[1].Value = model.UserCode;
			parameters[2].Value = model.Title;
			parameters[3].Value = model.Place;
			parameters[4].Value = model.StartTime;
			parameters[5].Value = model.EndTime;
			parameters[6].Value = model.Scratchpad;
			parameters[7].Value = model.IsRemind;
			parameters[8].Value = model.RemindHours;
			parameters[9].Value = model.RemindState;

			DbHelperSQL.RunProcedure("Proc_Tb_Sys_UserCalendar_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(Guid UserCalendarCode)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@UserCalendarCode", SqlDbType.UniqueIdentifier)};
			parameters[0].Value = UserCalendarCode;

			DbHelperSQL.RunProcedure("Proc_Tb_Sys_UserCalendar_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.SQMSys.Tb_Sys_UserCalendar GetModel(Guid UserCalendarCode)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@UserCalendarCode", SqlDbType.UniqueIdentifier)};
			parameters[0].Value = UserCalendarCode;

			MobileSoft.Model.SQMSys.Tb_Sys_UserCalendar model=new MobileSoft.Model.SQMSys.Tb_Sys_UserCalendar();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_Sys_UserCalendar_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["UserCalendarCode"].ToString()!="")
				{
					model.UserCalendarCode=new Guid(ds.Tables[0].Rows[0]["UserCalendarCode"].ToString());
				}
				model.UserCode=ds.Tables[0].Rows[0]["UserCode"].ToString();
				model.Title=ds.Tables[0].Rows[0]["Title"].ToString();
				model.Place=ds.Tables[0].Rows[0]["Place"].ToString();
				if(ds.Tables[0].Rows[0]["StartTime"].ToString()!="")
				{
					model.StartTime=DateTime.Parse(ds.Tables[0].Rows[0]["StartTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["EndTime"].ToString()!="")
				{
					model.EndTime=DateTime.Parse(ds.Tables[0].Rows[0]["EndTime"].ToString());
				}
				model.Scratchpad=ds.Tables[0].Rows[0]["Scratchpad"].ToString();
				if(ds.Tables[0].Rows[0]["IsRemind"].ToString()!="")
				{
					model.IsRemind=int.Parse(ds.Tables[0].Rows[0]["IsRemind"].ToString());
				}
				if(ds.Tables[0].Rows[0]["RemindHours"].ToString()!="")
				{
					model.RemindHours=decimal.Parse(ds.Tables[0].Rows[0]["RemindHours"].ToString());
				}
				if(ds.Tables[0].Rows[0]["RemindState"].ToString()!="")
				{
					model.RemindState=int.Parse(ds.Tables[0].Rows[0]["RemindState"].ToString());
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
			strSql.Append("select UserCalendarCode,UserCode,Title,Place,StartTime,EndTime,Scratchpad,IsRemind,RemindHours,RemindState ");
			strSql.Append(" FROM Tb_Sys_UserCalendar ");
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
			strSql.Append(" UserCalendarCode,UserCode,Title,Place,StartTime,EndTime,Scratchpad,IsRemind,RemindHours,RemindState ");
			strSql.Append(" FROM Tb_Sys_UserCalendar ");
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
			parameters[5].Value = "SELECT * FROM Tb_Sys_UserCalendar WHERE 1=1 " + StrCondition;
			parameters[6].Value = "UserCalendarCode";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

