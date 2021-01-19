using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;
using System.IO;//请先添加引用
namespace MobileSoft.DAL.HSPR
{
	/// <summary>
	/// 数据访问类Dal_Tb_HSPR_CommActivities。
	/// </summary>
	public class Dal_Tb_HSPR_CommActivities
	{
		public Dal_Tb_HSPR_CommActivities()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string ActivitiesID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ActivitiesID", SqlDbType.NVarChar,50)};
			parameters[0].Value = ActivitiesID;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_HSPR_CommActivities_Exists",parameters,out rowsAffected);
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
		public void Add(MobileSoft.Model.HSPR.Tb_HSPR_CommActivities model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ActivitiesID", SqlDbType.NVarChar,50),
					new SqlParameter("@CommID", SqlDbType.BigInt,8),
					new SqlParameter("@ActivitiesType", SqlDbType.NVarChar,50),
					new SqlParameter("@ActivitiesTheme", SqlDbType.NVarChar,200),
					new SqlParameter("@ActivitiesContent", SqlDbType.NVarChar,2000),
					new SqlParameter("@ActivitiesStartDate", SqlDbType.DateTime),
					new SqlParameter("@ActivitiesEndDate", SqlDbType.DateTime),
					new SqlParameter("@ActivitiesPlan", SqlDbType.NVarChar,2000),
					new SqlParameter("@CustName", SqlDbType.NVarChar,50),
					new SqlParameter("@CustID", SqlDbType.BigInt,8),
					new SqlParameter("@RoomSign", SqlDbType.NVarChar,50),
					new SqlParameter("@RoomID", SqlDbType.BigInt,8),
					new SqlParameter("@LinkPhone", SqlDbType.NVarChar,50),
					new SqlParameter("@ActivitiesImages", SqlDbType.NVarChar,2000),
					new SqlParameter("@IssueDate", SqlDbType.DateTime),
					new SqlParameter("@IsDelete", SqlDbType.Int,4)};
			parameters[0].Value = model.ActivitiesID;
			parameters[1].Value = model.CommID;
			parameters[2].Value = model.ActivitiesType;
			parameters[3].Value = model.ActivitiesTheme;
			parameters[4].Value = model.ActivitiesContent;
			parameters[5].Value = model.ActivitiesStartDate;
			parameters[6].Value = model.ActivitiesEndDate;
			parameters[7].Value = model.ActivitiesPlan;
			parameters[8].Value = model.CustName;
			parameters[9].Value = model.CustID;
			parameters[10].Value = model.RoomSign;
			parameters[11].Value = model.RoomID;
			parameters[12].Value = model.LinkPhone;
			parameters[13].Value = model.ActivitiesImages;
			parameters[14].Value = model.IssueDate;
			parameters[15].Value = model.IsDelete;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_CommActivities_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.HSPR.Tb_HSPR_CommActivities model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ActivitiesID", SqlDbType.NVarChar,50),
					new SqlParameter("@CommID", SqlDbType.BigInt,8),
					new SqlParameter("@ActivitiesType", SqlDbType.NVarChar,50),
					new SqlParameter("@ActivitiesTheme", SqlDbType.NVarChar,200),
					new SqlParameter("@ActivitiesContent", SqlDbType.NVarChar,2000),
					new SqlParameter("@ActivitiesStartDate", SqlDbType.DateTime),
					new SqlParameter("@ActivitiesEndDate", SqlDbType.DateTime),
					new SqlParameter("@ActivitiesPlan", SqlDbType.NVarChar,2000),
					new SqlParameter("@CustName", SqlDbType.NVarChar,50),
					new SqlParameter("@CustID", SqlDbType.BigInt,8),
					new SqlParameter("@RoomSign", SqlDbType.NVarChar,50),
					new SqlParameter("@RoomID", SqlDbType.BigInt,8),
					new SqlParameter("@LinkPhone", SqlDbType.NVarChar,50),
					new SqlParameter("@ActivitiesImages", SqlDbType.NVarChar,2000),
					new SqlParameter("@IssueDate", SqlDbType.DateTime),
					new SqlParameter("@IsDelete", SqlDbType.Int,4)};
			parameters[0].Value = model.ActivitiesID;
			parameters[1].Value = model.CommID;
			parameters[2].Value = model.ActivitiesType;
			parameters[3].Value = model.ActivitiesTheme;
			parameters[4].Value = model.ActivitiesContent;
			parameters[5].Value = model.ActivitiesStartDate;
			parameters[6].Value = model.ActivitiesEndDate;
			parameters[7].Value = model.ActivitiesPlan;
			parameters[8].Value = model.CustName;
			parameters[9].Value = model.CustID;
			parameters[10].Value = model.RoomSign;
			parameters[11].Value = model.RoomID;
			parameters[12].Value = model.LinkPhone;
			parameters[13].Value = model.ActivitiesImages;
			parameters[14].Value = model.IssueDate;
			parameters[15].Value = model.IsDelete;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_CommActivities_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(string ActivitiesID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ActivitiesID", SqlDbType.NVarChar,50)};
			parameters[0].Value = ActivitiesID;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_CommActivities_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_CommActivities GetModel(string ActivitiesID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@ActivitiesID", SqlDbType.NVarChar,50)};
			parameters[0].Value = ActivitiesID;

			MobileSoft.Model.HSPR.Tb_HSPR_CommActivities model=new MobileSoft.Model.HSPR.Tb_HSPR_CommActivities();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_HSPR_CommActivities_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				model.ActivitiesID=ds.Tables[0].Rows[0]["ActivitiesID"].ToString();
				if(ds.Tables[0].Rows[0]["CommID"].ToString()!="")
				{
					model.CommID=long.Parse(ds.Tables[0].Rows[0]["CommID"].ToString());
				}
				model.ActivitiesType=ds.Tables[0].Rows[0]["ActivitiesType"].ToString();
				model.ActivitiesTheme=ds.Tables[0].Rows[0]["ActivitiesTheme"].ToString();
				model.ActivitiesContent=ds.Tables[0].Rows[0]["ActivitiesContent"].ToString();
				if(ds.Tables[0].Rows[0]["ActivitiesStartDate"].ToString()!="")
				{
					model.ActivitiesStartDate=DateTime.Parse(ds.Tables[0].Rows[0]["ActivitiesStartDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ActivitiesEndDate"].ToString()!="")
				{
					model.ActivitiesEndDate=DateTime.Parse(ds.Tables[0].Rows[0]["ActivitiesEndDate"].ToString());
				}
				model.ActivitiesPlan=ds.Tables[0].Rows[0]["ActivitiesPlan"].ToString();
				model.CustName=ds.Tables[0].Rows[0]["CustName"].ToString();
				if(ds.Tables[0].Rows[0]["CustID"].ToString()!="")
				{
					model.CustID=long.Parse(ds.Tables[0].Rows[0]["CustID"].ToString());
				}
				model.RoomSign=ds.Tables[0].Rows[0]["RoomSign"].ToString();
				if(ds.Tables[0].Rows[0]["RoomID"].ToString()!="")
				{
					model.RoomID=long.Parse(ds.Tables[0].Rows[0]["RoomID"].ToString());
				}
				model.LinkPhone=ds.Tables[0].Rows[0]["LinkPhone"].ToString();
				model.ActivitiesImages=ds.Tables[0].Rows[0]["ActivitiesImages"].ToString();
				if(ds.Tables[0].Rows[0]["IssueDate"].ToString()!="")
				{
					model.IssueDate=DateTime.Parse(ds.Tables[0].Rows[0]["IssueDate"].ToString());
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
			strSql.Append("select * ");
			strSql.Append(" FROM View_HSPR_CommActivities_Filter ");
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
			strSql.Append(" * ");
            strSql.Append(" FROM View_HSPR_CommActivities_Filter A");
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
            parameters[5].Value = "SELECT * FROM View_HSPR_CommActivities_Filter WHERE 1=1 " + StrCondition;
			parameters[6].Value = "ActivitiesID";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

