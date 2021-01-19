using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.OAPublicWork
{
	/// <summary>
	/// 数据访问类Dal_Tb_OAPublicWork_WorkRecord。
	/// </summary>
	public class Dal_Tb_OAPublicWork_WorkRecord
	{
		public Dal_Tb_OAPublicWork_WorkRecord()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int InfoId)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.Int,4)};
			parameters[0].Value = InfoId;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_WorkRecord_Exists",parameters,out rowsAffected);
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
		public int Add(MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_WorkRecord model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.Int,4),
					new SqlParameter("@UserCode", SqlDbType.NVarChar,20),
					new SqlParameter("@NowRecord", SqlDbType.VarChar,3000),
					new SqlParameter("@AfterRecord", SqlDbType.VarChar,3000),
					new SqlParameter("@WriteDate", SqlDbType.DateTime),
					new SqlParameter("@Ex1", SqlDbType.VarChar,200),
					new SqlParameter("@Ex2", SqlDbType.VarChar,200),
					new SqlParameter("@Ex3", SqlDbType.VarChar,200),
					new SqlParameter("@Ex4", SqlDbType.VarChar,200),
					new SqlParameter("@Ex5", SqlDbType.VarChar,200),
					new SqlParameter("@Ex6", SqlDbType.VarChar,200)};
			parameters[0].Direction = ParameterDirection.Output;
			parameters[1].Value = model.UserCode;
			parameters[2].Value = model.NowRecord;
			parameters[3].Value = model.AfterRecord;
			parameters[4].Value = model.WriteDate;
			parameters[5].Value = model.Ex1;
			parameters[6].Value = model.Ex2;
			parameters[7].Value = model.Ex3;
			parameters[8].Value = model.Ex4;
			parameters[9].Value = model.Ex5;
			parameters[10].Value = model.Ex6;

			DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_WorkRecord_ADD",parameters,out rowsAffected);
			return (int)parameters[0].Value;
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_WorkRecord model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.Int,4),
					new SqlParameter("@UserCode", SqlDbType.NVarChar,20),
					new SqlParameter("@NowRecord", SqlDbType.VarChar,3000),
					new SqlParameter("@AfterRecord", SqlDbType.VarChar,3000),
					new SqlParameter("@WriteDate", SqlDbType.DateTime),
					new SqlParameter("@Ex1", SqlDbType.VarChar,200),
					new SqlParameter("@Ex2", SqlDbType.VarChar,200),
					new SqlParameter("@Ex3", SqlDbType.VarChar,200),
					new SqlParameter("@Ex4", SqlDbType.VarChar,200),
					new SqlParameter("@Ex5", SqlDbType.VarChar,200),
					new SqlParameter("@Ex6", SqlDbType.VarChar,200)};
			parameters[0].Value = model.InfoId;
			parameters[1].Value = model.UserCode;
			parameters[2].Value = model.NowRecord;
			parameters[3].Value = model.AfterRecord;
			parameters[4].Value = model.WriteDate;
			parameters[5].Value = model.Ex1;
			parameters[6].Value = model.Ex2;
			parameters[7].Value = model.Ex3;
			parameters[8].Value = model.Ex4;
			parameters[9].Value = model.Ex5;
			parameters[10].Value = model.Ex6;

			DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_WorkRecord_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int InfoId)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.Int,4)};
			parameters[0].Value = InfoId;

			DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_WorkRecord_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_WorkRecord GetModel(int InfoId)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.Int,4)};
			parameters[0].Value = InfoId;

			MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_WorkRecord model=new MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_WorkRecord();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_WorkRecord_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["InfoId"].ToString()!="")
				{
					model.InfoId=int.Parse(ds.Tables[0].Rows[0]["InfoId"].ToString());
				}
				model.UserCode=ds.Tables[0].Rows[0]["UserCode"].ToString();
				model.NowRecord=ds.Tables[0].Rows[0]["NowRecord"].ToString();
				model.AfterRecord=ds.Tables[0].Rows[0]["AfterRecord"].ToString();
				if(ds.Tables[0].Rows[0]["WriteDate"].ToString()!="")
				{
					model.WriteDate=DateTime.Parse(ds.Tables[0].Rows[0]["WriteDate"].ToString());
				}
				model.Ex1=ds.Tables[0].Rows[0]["Ex1"].ToString();
				model.Ex2=ds.Tables[0].Rows[0]["Ex2"].ToString();
				model.Ex3=ds.Tables[0].Rows[0]["Ex3"].ToString();
				model.Ex4=ds.Tables[0].Rows[0]["Ex4"].ToString();
				model.Ex5=ds.Tables[0].Rows[0]["Ex5"].ToString();
				model.Ex6=ds.Tables[0].Rows[0]["Ex6"].ToString();
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
			strSql.Append("select InfoId,UserCode,NowRecord,AfterRecord,WriteDate,Ex1,Ex2,Ex3,Ex4,Ex5,Ex6 ");
			strSql.Append(" FROM Tb_OAPublicWork_WorkRecord ");
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
			strSql.Append(" InfoId,UserCode,NowRecord,AfterRecord,WriteDate,Ex1,Ex2,Ex3,Ex4,Ex5,Ex6 ");
			strSql.Append(" FROM Tb_OAPublicWork_WorkRecord ");
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
			parameters[5].Value = "SELECT * FROM Tb_OAPublicWork_WorkRecord WHERE 1=1 " + StrCondition;
			parameters[6].Value = "InfoId";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

