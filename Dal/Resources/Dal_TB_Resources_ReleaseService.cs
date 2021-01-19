using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.Resources
{
	/// <summary>
	/// 数据访问类Dal_TB_Resources_ReleaseService。
	/// </summary>
	public class Dal_TB_Resources_ReleaseService
	{
		public Dal_TB_Resources_ReleaseService()
		{
            DbHelperSQL.ConnectionString = PubConstant.SQMBSContionString;
		}
		#region  成员方法

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long ReleaseServiceID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ReleaseServiceID", SqlDbType.BigInt)};
			parameters[0].Value = ReleaseServiceID;

			int result= DbHelperSQL.RunProcedure("Proc_TB_Resources_ReleaseService_Exists",parameters,out rowsAffected);
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
        public void Add(MobileSoft.Model.Resources.TB_Resources_ReleaseService model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ReleaseServiceID", SqlDbType.BigInt,8),
					new SqlParameter("@ReleaseID", SqlDbType.BigInt,8),
					new SqlParameter("@ReleaseServiceContent", SqlDbType.NText),
					new SqlParameter("@ReleaseServiceNeedKnow", SqlDbType.NText)};
			parameters[0].Direction = ParameterDirection.Output;
			parameters[1].Value = model.ReleaseID;
			parameters[2].Value = model.ReleaseServiceContent;
			parameters[3].Value = model.ReleaseServiceNeedKnow;

			DbHelperSQL.RunProcedure("Proc_TB_Resources_ReleaseService_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.Resources.TB_Resources_ReleaseService model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ReleaseServiceID", SqlDbType.BigInt,8),
					new SqlParameter("@ReleaseID", SqlDbType.BigInt,8),
					new SqlParameter("@ReleaseServiceContent", SqlDbType.NText),
					new SqlParameter("@ReleaseServiceNeedKnow", SqlDbType.NText)};
			parameters[0].Value = model.ReleaseServiceID;
			parameters[1].Value = model.ReleaseID;
			parameters[2].Value = model.ReleaseServiceContent;
			parameters[3].Value = model.ReleaseServiceNeedKnow;

			DbHelperSQL.RunProcedure("Proc_TB_Resources_ReleaseService_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long ReleaseServiceID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ReleaseServiceID", SqlDbType.BigInt)};
			parameters[0].Value = ReleaseServiceID;

			DbHelperSQL.RunProcedure("Proc_TB_Resources_ReleaseService_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.Resources.TB_Resources_ReleaseService GetModel(long ReleaseServiceID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@ReleaseServiceID", SqlDbType.BigInt)};
			parameters[0].Value = ReleaseServiceID;

			MobileSoft.Model.Resources.TB_Resources_ReleaseService model=new MobileSoft.Model.Resources.TB_Resources_ReleaseService();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_TB_Resources_ReleaseService_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ReleaseServiceID"].ToString()!="")
				{
					model.ReleaseServiceID=long.Parse(ds.Tables[0].Rows[0]["ReleaseServiceID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ReleaseID"].ToString()!="")
				{
					model.ReleaseID=long.Parse(ds.Tables[0].Rows[0]["ReleaseID"].ToString());
				}
				model.ReleaseServiceContent=ds.Tables[0].Rows[0]["ReleaseServiceContent"].ToString();
				model.ReleaseServiceNeedKnow=ds.Tables[0].Rows[0]["ReleaseServiceNeedKnow"].ToString();
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
			strSql.Append("select ReleaseServiceID,ReleaseID,ReleaseServiceContent,ReleaseServiceNeedKnow ");
			strSql.Append(" FROM TB_Resources_ReleaseService ");
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
			strSql.Append(" ReleaseServiceID,ReleaseID,ReleaseServiceContent,ReleaseServiceNeedKnow ");
			strSql.Append(" FROM TB_Resources_ReleaseService ");
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
			parameters[5].Value = "SELECT * FROM TB_Resources_ReleaseService WHERE 1=1 " + StrCondition;
			parameters[6].Value = "ReleaseServiceID";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

