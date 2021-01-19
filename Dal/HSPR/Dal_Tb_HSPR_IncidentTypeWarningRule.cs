using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.HSPR
{
	/// <summary>
	/// 数据访问类Dal_Tb_HSPR_IncidentTypeWarningRule。
	/// </summary>
	public class Dal_Tb_HSPR_IncidentTypeWarningRule
	{
		public Dal_Tb_HSPR_IncidentTypeWarningRule()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public long GetMaxId()
		{
		return DbHelperSQL.GetMaxID("WarningRuleID", "Tb_HSPR_IncidentTypeWarningRule"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long WarningRuleID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@WarningRuleID", SqlDbType.BigInt)};
			parameters[0].Value = WarningRuleID;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_HSPR_IncidentTypeWarningRule_Exists",parameters,out rowsAffected);
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
		public void Add(MobileSoft.Model.HSPR.Tb_HSPR_IncidentTypeWarningRule model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@WarningRuleID", SqlDbType.BigInt,8),
					new SqlParameter("@WarningID", SqlDbType.BigInt,8),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@RuleID", SqlDbType.NVarChar,50),
					new SqlParameter("@IsDelete", SqlDbType.Int,4)};
			parameters[0].Value = model.WarningRuleID;
			parameters[1].Value = model.WarningID;
			parameters[2].Value = model.CommID;
			parameters[3].Value = model.RuleID;
			parameters[4].Value = model.IsDelete;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_IncidentTypeWarningRule_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.HSPR.Tb_HSPR_IncidentTypeWarningRule model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@WarningRuleID", SqlDbType.BigInt,8),
					new SqlParameter("@WarningID", SqlDbType.BigInt,8),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@RuleID", SqlDbType.NVarChar,50),
					new SqlParameter("@IsDelete", SqlDbType.Int,4)};
			parameters[0].Value = model.WarningRuleID;
			parameters[1].Value = model.WarningID;
			parameters[2].Value = model.CommID;
			parameters[3].Value = model.RuleID;
			parameters[4].Value = model.IsDelete;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_IncidentTypeWarningRule_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long WarningRuleID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@WarningRuleID", SqlDbType.BigInt)};
			parameters[0].Value = WarningRuleID;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_IncidentTypeWarningRule_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_IncidentTypeWarningRule GetModel(long WarningRuleID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@WarningRuleID", SqlDbType.BigInt)};
			parameters[0].Value = WarningRuleID;

			MobileSoft.Model.HSPR.Tb_HSPR_IncidentTypeWarningRule model=new MobileSoft.Model.HSPR.Tb_HSPR_IncidentTypeWarningRule();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_HSPR_IncidentTypeWarningRule_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["WarningRuleID"].ToString()!="")
				{
					model.WarningRuleID=long.Parse(ds.Tables[0].Rows[0]["WarningRuleID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["WarningID"].ToString()!="")
				{
					model.WarningID=long.Parse(ds.Tables[0].Rows[0]["WarningID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CommID"].ToString()!="")
				{
					model.CommID=int.Parse(ds.Tables[0].Rows[0]["CommID"].ToString());
				}
				model.RuleID=ds.Tables[0].Rows[0]["RuleID"].ToString();
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
			strSql.Append("select WarningRuleID,WarningID,CommID,RuleID,IsDelete ");
			strSql.Append(" FROM Tb_HSPR_IncidentTypeWarningRule ");
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
			strSql.Append(" WarningRuleID,WarningID,CommID,RuleID,IsDelete ");
			strSql.Append(" FROM Tb_HSPR_IncidentTypeWarningRule ");
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
			parameters[5].Value = "SELECT * FROM Tb_HSPR_IncidentTypeWarningRule WHERE 1=1 " + StrCondition;
			parameters[6].Value = "WarningRuleID";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

