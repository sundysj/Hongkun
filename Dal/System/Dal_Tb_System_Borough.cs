using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.System
{
	/// <summary>
	/// 数据访问类Dal_Tb_System_Borough。
	/// </summary>
	public class Dal_Tb_System_Borough
	{
		public Dal_Tb_System_Borough()
		{
            DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("BoroughID", "Tb_System_Borough"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int BoroughID,int CityID,string BoroughName)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@BoroughID", SqlDbType.Int,4),
					new SqlParameter("@CityID", SqlDbType.Int,4),
					new SqlParameter("@BoroughName", SqlDbType.NVarChar,50)};
			parameters[0].Value = BoroughID;
			parameters[1].Value = CityID;
			parameters[2].Value = BoroughName;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_System_Borough_Exists",parameters,out rowsAffected);
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
		public void Add(MobileSoft.Model.System.Tb_System_Borough model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@BoroughID", SqlDbType.Int,4),
					new SqlParameter("@CityID", SqlDbType.Int,4),
					new SqlParameter("@BoroughName", SqlDbType.NVarChar,40)};
			parameters[0].Value = model.BoroughID;
			parameters[1].Value = model.CityID;
			parameters[2].Value = model.BoroughName;

			DbHelperSQL.RunProcedure("Proc_Tb_System_Borough_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.System.Tb_System_Borough model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@BoroughID", SqlDbType.Int,4),
					new SqlParameter("@CityID", SqlDbType.Int,4),
					new SqlParameter("@BoroughName", SqlDbType.NVarChar,40)};
			parameters[0].Value = model.BoroughID;
			parameters[1].Value = model.CityID;
			parameters[2].Value = model.BoroughName;

			DbHelperSQL.RunProcedure("Proc_Tb_System_Borough_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int BoroughID,int CityID,string BoroughName)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@BoroughID", SqlDbType.Int,4),
					new SqlParameter("@CityID", SqlDbType.Int,4),
					new SqlParameter("@BoroughName", SqlDbType.NVarChar,50)};
			parameters[0].Value = BoroughID;
			parameters[1].Value = CityID;
			parameters[2].Value = BoroughName;

			DbHelperSQL.RunProcedure("Proc_Tb_System_Borough_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.System.Tb_System_Borough GetModel(int BoroughID,int CityID,string BoroughName)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@BoroughID", SqlDbType.Int,4),
					new SqlParameter("@CityID", SqlDbType.Int,4),
					new SqlParameter("@BoroughName", SqlDbType.NVarChar,50)};
			parameters[0].Value = BoroughID;
			parameters[1].Value = CityID;
			parameters[2].Value = BoroughName;

			MobileSoft.Model.System.Tb_System_Borough model=new MobileSoft.Model.System.Tb_System_Borough();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_System_Borough_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["BoroughID"].ToString()!="")
				{
					model.BoroughID=int.Parse(ds.Tables[0].Rows[0]["BoroughID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CityID"].ToString()!="")
				{
					model.CityID=int.Parse(ds.Tables[0].Rows[0]["CityID"].ToString());
				}
				model.BoroughName=ds.Tables[0].Rows[0]["BoroughName"].ToString();
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
			strSql.Append("select BoroughID,CityID,BoroughName ");
			strSql.Append(" FROM Tb_System_Borough ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
            strSql.Append(" ORDER BY BoroughID ASC");
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
			strSql.Append(" BoroughID,CityID,BoroughName ");
			strSql.Append(" FROM Tb_System_Borough ");
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
			parameters[5].Value = "SELECT * FROM Tb_System_Borough WHERE 1=1 " + StrCondition;
			parameters[6].Value = "BoroughID,CityID,BoroughName";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

