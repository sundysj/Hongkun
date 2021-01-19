using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.System
{
	/// <summary>
	/// 数据访问类Dal_Tb_System_Log。
	/// </summary>
	public class Dal_Tb_System_Log
	{
		public Dal_Tb_System_Log()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string ManagerCode,long LogCode)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ManagerCode", SqlDbType.NVarChar,50),
					new SqlParameter("@LogCode", SqlDbType.BigInt)};
			parameters[0].Value = ManagerCode;
			parameters[1].Value = LogCode;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_System_Log_Exists",parameters,out rowsAffected);
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
		public int Add(MobileSoft.Model.System.Tb_System_Log model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@LogCode", SqlDbType.BigInt,8),
					new SqlParameter("@ManagerCode", SqlDbType.NVarChar,20),
					new SqlParameter("@LocationIP", SqlDbType.NVarChar,50),
					new SqlParameter("@LogTime", SqlDbType.DateTime),
					new SqlParameter("@PNodeName", SqlDbType.NVarChar,50),
					new SqlParameter("@OperateName", SqlDbType.NVarChar,50),
					new SqlParameter("@OperateURL", SqlDbType.NText),
					new SqlParameter("@Memo", SqlDbType.NText)};
			parameters[0].Direction = ParameterDirection.Output;
			parameters[1].Value = model.ManagerCode;
			parameters[2].Value = model.LocationIP;
			parameters[3].Value = model.LogTime;
			parameters[4].Value = model.PNodeName;
			parameters[5].Value = model.OperateName;
			parameters[6].Value = model.OperateURL;
			parameters[7].Value = model.Memo;

			DbHelperSQL.RunProcedure("Proc_Tb_System_Log_ADD",parameters,out rowsAffected);
			return (int)parameters[0].Value;
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.System.Tb_System_Log model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@LogCode", SqlDbType.BigInt,8),
					new SqlParameter("@ManagerCode", SqlDbType.NVarChar,20),
					new SqlParameter("@LocationIP", SqlDbType.NVarChar,50),
					new SqlParameter("@LogTime", SqlDbType.DateTime),
					new SqlParameter("@PNodeName", SqlDbType.NVarChar,50),
					new SqlParameter("@OperateName", SqlDbType.NVarChar,50),
					new SqlParameter("@OperateURL", SqlDbType.NText),
					new SqlParameter("@Memo", SqlDbType.NText)};
			parameters[0].Value = model.LogCode;
			parameters[1].Value = model.ManagerCode;
			parameters[2].Value = model.LocationIP;
			parameters[3].Value = model.LogTime;
			parameters[4].Value = model.PNodeName;
			parameters[5].Value = model.OperateName;
			parameters[6].Value = model.OperateURL;
			parameters[7].Value = model.Memo;

			DbHelperSQL.RunProcedure("Proc_Tb_System_Log_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(string ManagerCode,long LogCode)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ManagerCode", SqlDbType.NVarChar,50),
					new SqlParameter("@LogCode", SqlDbType.BigInt)};
			parameters[0].Value = ManagerCode;
			parameters[1].Value = LogCode;

			DbHelperSQL.RunProcedure("Proc_Tb_System_Log_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.System.Tb_System_Log GetModel(string ManagerCode,long LogCode)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@ManagerCode", SqlDbType.NVarChar,50),
					new SqlParameter("@LogCode", SqlDbType.BigInt)};
			parameters[0].Value = ManagerCode;
			parameters[1].Value = LogCode;

			MobileSoft.Model.System.Tb_System_Log model=new MobileSoft.Model.System.Tb_System_Log();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_System_Log_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["LogCode"].ToString()!="")
				{
					model.LogCode=long.Parse(ds.Tables[0].Rows[0]["LogCode"].ToString());
				}
				model.ManagerCode=ds.Tables[0].Rows[0]["ManagerCode"].ToString();
				model.LocationIP=ds.Tables[0].Rows[0]["LocationIP"].ToString();
				if(ds.Tables[0].Rows[0]["LogTime"].ToString()!="")
				{
					model.LogTime=DateTime.Parse(ds.Tables[0].Rows[0]["LogTime"].ToString());
				}
				model.PNodeName=ds.Tables[0].Rows[0]["PNodeName"].ToString();
				model.OperateName=ds.Tables[0].Rows[0]["OperateName"].ToString();
				model.OperateURL=ds.Tables[0].Rows[0]["OperateURL"].ToString();
				model.Memo=ds.Tables[0].Rows[0]["Memo"].ToString();
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
			strSql.Append("select LogCode,ManagerCode,LocationIP,LogTime,PNodeName,OperateName,OperateURL,Memo ");
			strSql.Append(" FROM Tb_System_Log ");
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
			strSql.Append(" LogCode,ManagerCode,LocationIP,LogTime,PNodeName,OperateName,OperateURL,Memo ");
			strSql.Append(" FROM Tb_System_Log ");
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
			parameters[5].Value = "SELECT * FROM Tb_System_Log WHERE 1=1 " + StrCondition;
			parameters[6].Value = "ManagerCode,LogCode";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

