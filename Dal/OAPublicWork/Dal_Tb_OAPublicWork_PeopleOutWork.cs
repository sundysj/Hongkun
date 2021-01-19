using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.OAPublicWork
{
	/// <summary>
	/// 数据访问类Dal_Tb_OAPublicWork_PeopleOutWork。
	/// </summary>
	public class Dal_Tb_OAPublicWork_PeopleOutWork
	{
		public Dal_Tb_OAPublicWork_PeopleOutWork()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long InfoId)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.BigInt)};
			parameters[0].Value = InfoId;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_PeopleOutWork_Exists",parameters,out rowsAffected);
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
		public int Add(MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_PeopleOutWork model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.BigInt,8),
					new SqlParameter("@UserCode", SqlDbType.NVarChar,20),
					new SqlParameter("@RecordDate", SqlDbType.DateTime),
					new SqlParameter("@OutDate", SqlDbType.DateTime),
					new SqlParameter("@PlanRetDate", SqlDbType.DateTime),
					new SqlParameter("@RealRetDate", SqlDbType.DateTime),
					new SqlParameter("@OutWhere", SqlDbType.NVarChar,100),
					new SqlParameter("@OutThing", SqlDbType.NVarChar,2000),
					new SqlParameter("@OutResult", SqlDbType.NVarChar,50),
					new SqlParameter("@IsReturn", SqlDbType.NVarChar,10),
					new SqlParameter("@LeaveManList", SqlDbType.NVarChar,2000)};
			parameters[0].Direction = ParameterDirection.Output;
			parameters[1].Value = model.UserCode;
			parameters[2].Value = model.RecordDate;
			parameters[3].Value = model.OutDate;
			parameters[4].Value = model.PlanRetDate;
			parameters[5].Value = model.RealRetDate;
			parameters[6].Value = model.OutWhere;
			parameters[7].Value = model.OutThing;
			parameters[8].Value = model.OutResult;
			parameters[9].Value = model.IsReturn;
			parameters[10].Value = model.LeaveManList;

			DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_PeopleOutWork_ADD",parameters,out rowsAffected);
			return (int)parameters[0].Value;
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_PeopleOutWork model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.BigInt,8),
					new SqlParameter("@UserCode", SqlDbType.NVarChar,20),
					new SqlParameter("@RecordDate", SqlDbType.DateTime),
					new SqlParameter("@OutDate", SqlDbType.DateTime),
					new SqlParameter("@PlanRetDate", SqlDbType.DateTime),
					new SqlParameter("@RealRetDate", SqlDbType.DateTime),
					new SqlParameter("@OutWhere", SqlDbType.NVarChar,100),
					new SqlParameter("@OutThing", SqlDbType.NVarChar,2000),
					new SqlParameter("@OutResult", SqlDbType.NVarChar,50),
					new SqlParameter("@IsReturn", SqlDbType.NVarChar,10),
					new SqlParameter("@LeaveManList", SqlDbType.NVarChar,2000)};
			parameters[0].Value = model.InfoId;
			parameters[1].Value = model.UserCode;
			parameters[2].Value = model.RecordDate;
			parameters[3].Value = model.OutDate;
			parameters[4].Value = model.PlanRetDate;
			parameters[5].Value = model.RealRetDate;
			parameters[6].Value = model.OutWhere;
			parameters[7].Value = model.OutThing;
			parameters[8].Value = model.OutResult;
			parameters[9].Value = model.IsReturn;
			parameters[10].Value = model.LeaveManList;

			DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_PeopleOutWork_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long InfoId)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.BigInt)};
			parameters[0].Value = InfoId;

			DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_PeopleOutWork_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_PeopleOutWork GetModel(long InfoId)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.BigInt)};
			parameters[0].Value = InfoId;

			MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_PeopleOutWork model=new MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_PeopleOutWork();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_PeopleOutWork_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["InfoId"].ToString()!="")
				{
					model.InfoId=long.Parse(ds.Tables[0].Rows[0]["InfoId"].ToString());
				}
				model.UserCode=ds.Tables[0].Rows[0]["UserCode"].ToString();
				if(ds.Tables[0].Rows[0]["RecordDate"].ToString()!="")
				{
					model.RecordDate=DateTime.Parse(ds.Tables[0].Rows[0]["RecordDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["OutDate"].ToString()!="")
				{
					model.OutDate=DateTime.Parse(ds.Tables[0].Rows[0]["OutDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["PlanRetDate"].ToString()!="")
				{
					model.PlanRetDate=DateTime.Parse(ds.Tables[0].Rows[0]["PlanRetDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["RealRetDate"].ToString()!="")
				{
					model.RealRetDate=DateTime.Parse(ds.Tables[0].Rows[0]["RealRetDate"].ToString());
				}
				model.OutWhere=ds.Tables[0].Rows[0]["OutWhere"].ToString();
				model.OutThing=ds.Tables[0].Rows[0]["OutThing"].ToString();
				model.OutResult=ds.Tables[0].Rows[0]["OutResult"].ToString();
				model.IsReturn=ds.Tables[0].Rows[0]["IsReturn"].ToString();
				model.LeaveManList=ds.Tables[0].Rows[0]["LeaveManList"].ToString();
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
			strSql.Append("select InfoId,UserCode,RecordDate,OutDate,PlanRetDate,RealRetDate,OutWhere,OutThing,OutResult,IsReturn,LeaveManList ");
			strSql.Append(" FROM Tb_OAPublicWork_PeopleOutWork ");
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
			strSql.Append(" InfoId,UserCode,RecordDate,OutDate,PlanRetDate,RealRetDate,OutWhere,OutThing,OutResult,IsReturn,LeaveManList ");
			strSql.Append(" FROM Tb_OAPublicWork_PeopleOutWork ");
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
			parameters[5].Value = "SELECT * FROM Tb_OAPublicWork_PeopleOutWork WHERE 1=1 " + StrCondition;
			parameters[6].Value = "InfoId";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

