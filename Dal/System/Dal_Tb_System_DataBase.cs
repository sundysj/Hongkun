using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.System
{
	/// <summary>
	/// 数据访问类Dal_Tb_System_DataBase。
	/// </summary>
	public class Dal_Tb_System_DataBase
	{
		public Dal_Tb_System_DataBase()
		{
                  DbHelperSQL.ConnectionString = PubConstant.tw2bsConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("DBID", "Tb_System_DataBase"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int DBID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@DBID", SqlDbType.Int,4)};
			parameters[0].Value = DBID;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_System_DataBase_Exists",parameters,out rowsAffected);
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
		public int Add(MobileSoft.Model.System.Tb_System_DataBase model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@DBID", SqlDbType.Int,4),
					new SqlParameter("@ProvinceID", SqlDbType.Int,4),
					new SqlParameter("@CityID", SqlDbType.Int,4),
					new SqlParameter("@BoroughID", SqlDbType.Int,4),
					new SqlParameter("@StreetID", SqlDbType.Int,4),
					new SqlParameter("@DBServer", SqlDbType.NVarChar,20),
					new SqlParameter("@DBName", SqlDbType.NVarChar,20),
					new SqlParameter("@DBUser", SqlDbType.NVarChar,20),
					new SqlParameter("@DBPwd", SqlDbType.NVarChar,20)};
			parameters[0].Direction = ParameterDirection.Output;
			parameters[1].Value = model.ProvinceID;
			parameters[2].Value = model.CityID;
			parameters[3].Value = model.BoroughID;
			parameters[4].Value = model.StreetID;
			parameters[5].Value = model.DBServer;
			parameters[6].Value = model.DBName;
			parameters[7].Value = model.DBUser;
			parameters[8].Value = model.DBPwd;

			DbHelperSQL.RunProcedure("Proc_Tb_System_DataBase_ADD",parameters,out rowsAffected);
			return (int)parameters[0].Value;
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.System.Tb_System_DataBase model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@DBID", SqlDbType.Int,4),
					new SqlParameter("@ProvinceID", SqlDbType.Int,4),
					new SqlParameter("@CityID", SqlDbType.Int,4),
					new SqlParameter("@BoroughID", SqlDbType.Int,4),
					new SqlParameter("@StreetID", SqlDbType.Int,4),
					new SqlParameter("@DBServer", SqlDbType.NVarChar,20),
					new SqlParameter("@DBName", SqlDbType.NVarChar,20),
					new SqlParameter("@DBUser", SqlDbType.NVarChar,20),
					new SqlParameter("@DBPwd", SqlDbType.NVarChar,20)};
			parameters[0].Value = model.DBID;
			parameters[1].Value = model.ProvinceID;
			parameters[2].Value = model.CityID;
			parameters[3].Value = model.BoroughID;
			parameters[4].Value = model.StreetID;
			parameters[5].Value = model.DBServer;
			parameters[6].Value = model.DBName;
			parameters[7].Value = model.DBUser;
			parameters[8].Value = model.DBPwd;

			DbHelperSQL.RunProcedure("Proc_Tb_System_DataBase_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int DBID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@DBID", SqlDbType.Int,4)};
			parameters[0].Value = DBID;

			DbHelperSQL.RunProcedure("Proc_Tb_System_DataBase_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.System.Tb_System_DataBase GetModel(int DBID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@DBID", SqlDbType.Int,4)};
			parameters[0].Value = DBID;

			MobileSoft.Model.System.Tb_System_DataBase model=new MobileSoft.Model.System.Tb_System_DataBase();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_System_DataBase_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["DBID"].ToString()!="")
				{
					model.DBID=int.Parse(ds.Tables[0].Rows[0]["DBID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ProvinceID"].ToString()!="")
				{
					model.ProvinceID=int.Parse(ds.Tables[0].Rows[0]["ProvinceID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CityID"].ToString()!="")
				{
					model.CityID=int.Parse(ds.Tables[0].Rows[0]["CityID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["BoroughID"].ToString()!="")
				{
					model.BoroughID=int.Parse(ds.Tables[0].Rows[0]["BoroughID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["StreetID"].ToString()!="")
				{
					model.StreetID=int.Parse(ds.Tables[0].Rows[0]["StreetID"].ToString());
				}
				model.DBServer=ds.Tables[0].Rows[0]["DBServer"].ToString();
				model.DBName=ds.Tables[0].Rows[0]["DBName"].ToString();
				model.DBUser=ds.Tables[0].Rows[0]["DBUser"].ToString();
				model.DBPwd=ds.Tables[0].Rows[0]["DBPwd"].ToString();
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
			strSql.Append("select DBID,ProvinceID,CityID,BoroughID,StreetID,DBServer,DBName,DBUser,DBPwd ");
			strSql.Append(" FROM Tb_System_DataBase ");
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
			strSql.Append(" DBID,ProvinceID,CityID,BoroughID,StreetID,DBServer,DBName,DBUser,DBPwd ");
			strSql.Append(" FROM Tb_System_DataBase ");
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
			parameters[5].Value = "SELECT * FROM Tb_System_DataBase WHERE 1=1 " + StrCondition;
			parameters[6].Value = "DBID";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

