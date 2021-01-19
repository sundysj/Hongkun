using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.System
{
	/// <summary>
	/// 数据访问类Dal_Tb_System_Help。
	/// </summary>
	public class Dal_Tb_System_Help
	{
		public Dal_Tb_System_Help()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("CorpID", "Tb_System_Help"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int CorpID,string PNodeCode,long IID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@CorpID", SqlDbType.Int,4),
					new SqlParameter("@PNodeCode", SqlDbType.NVarChar,50),
					new SqlParameter("@IID", SqlDbType.BigInt)};
			parameters[0].Value = CorpID;
			parameters[1].Value = PNodeCode;
			parameters[2].Value = IID;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_System_Help_Exists",parameters,out rowsAffected);
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
		public int Add(MobileSoft.Model.System.Tb_System_Help model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@IID", SqlDbType.BigInt,8),
					new SqlParameter("@CorpID", SqlDbType.Int,4),
					new SqlParameter("@PNodeCode", SqlDbType.NVarChar,20),
					new SqlParameter("@HelpTitle", SqlDbType.NVarChar,50),
					new SqlParameter("@HelpContent", SqlDbType.NText)};
			parameters[0].Direction = ParameterDirection.Output;
			parameters[1].Value = model.CorpID;
			parameters[2].Value = model.PNodeCode;
			parameters[3].Value = model.HelpTitle;
			parameters[4].Value = model.HelpContent;

			DbHelperSQL.RunProcedure("Proc_Tb_System_Help_ADD",parameters,out rowsAffected);
			return (int)parameters[0].Value;
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.System.Tb_System_Help model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@IID", SqlDbType.BigInt,8),
					new SqlParameter("@CorpID", SqlDbType.Int,4),
					new SqlParameter("@PNodeCode", SqlDbType.NVarChar,20),
					new SqlParameter("@HelpTitle", SqlDbType.NVarChar,50),
					new SqlParameter("@HelpContent", SqlDbType.NText)};
			parameters[0].Value = model.IID;
			parameters[1].Value = model.CorpID;
			parameters[2].Value = model.PNodeCode;
			parameters[3].Value = model.HelpTitle;
			parameters[4].Value = model.HelpContent;

			DbHelperSQL.RunProcedure("Proc_Tb_System_Help_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int CorpID,string PNodeCode,long IID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@CorpID", SqlDbType.Int,4),
					new SqlParameter("@PNodeCode", SqlDbType.NVarChar,50),
					new SqlParameter("@IID", SqlDbType.BigInt)};
			parameters[0].Value = CorpID;
			parameters[1].Value = PNodeCode;
			parameters[2].Value = IID;

			DbHelperSQL.RunProcedure("Proc_Tb_System_Help_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.System.Tb_System_Help GetModel(int CorpID,string PNodeCode,long IID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@CorpID", SqlDbType.Int,4),
					new SqlParameter("@PNodeCode", SqlDbType.NVarChar,50),
					new SqlParameter("@IID", SqlDbType.BigInt)};
			parameters[0].Value = CorpID;
			parameters[1].Value = PNodeCode;
			parameters[2].Value = IID;

			MobileSoft.Model.System.Tb_System_Help model=new MobileSoft.Model.System.Tb_System_Help();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_System_Help_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["IID"].ToString()!="")
				{
					model.IID=long.Parse(ds.Tables[0].Rows[0]["IID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CorpID"].ToString()!="")
				{
					model.CorpID=int.Parse(ds.Tables[0].Rows[0]["CorpID"].ToString());
				}
				model.PNodeCode=ds.Tables[0].Rows[0]["PNodeCode"].ToString();
				model.HelpTitle=ds.Tables[0].Rows[0]["HelpTitle"].ToString();
				model.HelpContent=ds.Tables[0].Rows[0]["HelpContent"].ToString();
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
			strSql.Append("select IID,CorpID,PNodeCode,HelpTitle,HelpContent ");
			strSql.Append(" FROM Tb_System_Help ");
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
			strSql.Append(" IID,CorpID,PNodeCode,HelpTitle,HelpContent ");
			strSql.Append(" FROM Tb_System_Help ");
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
			parameters[5].Value = "SELECT * FROM Tb_System_Help WHERE 1=1 " + StrCondition;
			parameters[6].Value = "CorpID,PNodeCode,IID";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

