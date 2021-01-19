using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.System
{
	/// <summary>
	/// 数据访问类Dal_Tb_System_City。
	/// </summary>
	public class Dal_Tb_System_City
	{
		public Dal_Tb_System_City()
		{
                  DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("CityID", "Tb_System_City"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int CityID,int ProvinceID,string CityName)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@CityID", SqlDbType.Int,4),
					new SqlParameter("@ProvinceID", SqlDbType.Int,4),
					new SqlParameter("@CityName", SqlDbType.NVarChar,50)};
			parameters[0].Value = CityID;
			parameters[1].Value = ProvinceID;
			parameters[2].Value = CityName;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_System_City_Exists",parameters,out rowsAffected);
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
		public void Add(MobileSoft.Model.System.Tb_System_City model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@CityID", SqlDbType.Int,4),
					new SqlParameter("@ProvinceID", SqlDbType.Int,4),
					new SqlParameter("@CityName", SqlDbType.NVarChar,30)};
			parameters[0].Value = model.CityID;
			parameters[1].Value = model.ProvinceID;
			parameters[2].Value = model.CityName;

			DbHelperSQL.RunProcedure("Proc_Tb_System_City_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.System.Tb_System_City model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@CityID", SqlDbType.Int,4),
					new SqlParameter("@ProvinceID", SqlDbType.Int,4),
					new SqlParameter("@CityName", SqlDbType.NVarChar,30)};
			parameters[0].Value = model.CityID;
			parameters[1].Value = model.ProvinceID;
			parameters[2].Value = model.CityName;

			DbHelperSQL.RunProcedure("Proc_Tb_System_City_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int CityID,int ProvinceID,string CityName)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@CityID", SqlDbType.Int,4),
					new SqlParameter("@ProvinceID", SqlDbType.Int,4),
					new SqlParameter("@CityName", SqlDbType.NVarChar,50)};
			parameters[0].Value = CityID;
			parameters[1].Value = ProvinceID;
			parameters[2].Value = CityName;

			DbHelperSQL.RunProcedure("Proc_Tb_System_City_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.System.Tb_System_City GetModel(int CityID,int ProvinceID,string CityName)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@CityID", SqlDbType.Int,4),
					new SqlParameter("@ProvinceID", SqlDbType.Int,4),
					new SqlParameter("@CityName", SqlDbType.NVarChar,50)};
			parameters[0].Value = CityID;
			parameters[1].Value = ProvinceID;
			parameters[2].Value = CityName;

			MobileSoft.Model.System.Tb_System_City model=new MobileSoft.Model.System.Tb_System_City();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_System_City_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["CityID"].ToString()!="")
				{
					model.CityID=int.Parse(ds.Tables[0].Rows[0]["CityID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ProvinceID"].ToString()!="")
				{
					model.ProvinceID=int.Parse(ds.Tables[0].Rows[0]["ProvinceID"].ToString());
				}
				model.CityName=ds.Tables[0].Rows[0]["CityName"].ToString();
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
			strSql.Append("select CityID,ProvinceID,CityName ");
			strSql.Append(" FROM Tb_System_City ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
            strSql.Append(" ORDER BY CityID ASC");
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
			strSql.Append(" CityID,ProvinceID,CityName ");
			strSql.Append(" FROM Tb_System_City ");
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
			parameters[5].Value = "SELECT * FROM Tb_System_City WHERE 1=1 " + StrCondition;
			parameters[6].Value = "CityID,ProvinceID,CityName";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

