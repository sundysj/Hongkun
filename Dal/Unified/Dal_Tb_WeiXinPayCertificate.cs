using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.Unified
{
	/// <summary>
	/// 数据访问类Dal_Tb_WeiXinPayCertificate。
	/// </summary>
	public class Dal_Tb_WeiXinPayCertificate
	{
		public Dal_Tb_WeiXinPayCertificate()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string Id)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.VarChar,50)};
			parameters[0].Value = Id;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_WeiXinPayCertificate_Exists",parameters,out rowsAffected);
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
		public void Add(MobileSoft.Model.Unified.Tb_WeiXinPayCertificate model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.VarChar,36),
					new SqlParameter("@CommunityId", SqlDbType.VarChar,36),
					new SqlParameter("@appid", SqlDbType.VarChar,36),
					new SqlParameter("@mch_id", SqlDbType.VarChar,36),
					new SqlParameter("@appkey", SqlDbType.VarChar,36),
					new SqlParameter("@appsecret", SqlDbType.VarChar,36),
					new SqlParameter("@SSLCERT_PATH", SqlDbType.VarChar,3999),
					new SqlParameter("@SSLCERT_PASSWORD", SqlDbType.VarChar,36)};
			parameters[0].Value = model.Id;
			parameters[1].Value = model.CommunityId;
			parameters[2].Value = model.appid;
			parameters[3].Value = model.mch_id;
			parameters[4].Value = model.appkey;
			parameters[5].Value = model.appsecret;
			parameters[6].Value = model.SSLCERT_PATH;
			parameters[7].Value = model.SSLCERT_PASSWORD;

			DbHelperSQL.RunProcedure("Proc_Tb_WeiXinPayCertificate_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.Unified.Tb_WeiXinPayCertificate model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.VarChar,36),
					new SqlParameter("@CommunityId", SqlDbType.VarChar,36),
					new SqlParameter("@appid", SqlDbType.VarChar,36),
					new SqlParameter("@mch_id", SqlDbType.VarChar,36),
					new SqlParameter("@appkey", SqlDbType.VarChar,36),
					new SqlParameter("@appsecret", SqlDbType.VarChar,36),
					new SqlParameter("@SSLCERT_PATH", SqlDbType.VarChar,3999),
					new SqlParameter("@SSLCERT_PASSWORD", SqlDbType.VarChar,36)};
			parameters[0].Value = model.Id;
			parameters[1].Value = model.CommunityId;
			parameters[2].Value = model.appid;
			parameters[3].Value = model.mch_id;
			parameters[4].Value = model.appkey;
			parameters[5].Value = model.appsecret;
			parameters[6].Value = model.SSLCERT_PATH;
			parameters[7].Value = model.SSLCERT_PASSWORD;

			DbHelperSQL.RunProcedure("Proc_Tb_WeiXinPayCertificate_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(string Id)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.VarChar,50)};
			parameters[0].Value = Id;

			DbHelperSQL.RunProcedure("Proc_Tb_WeiXinPayCertificate_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.Unified.Tb_WeiXinPayCertificate GetModel(string Id)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.VarChar,50)};
			parameters[0].Value = Id;

			MobileSoft.Model.Unified.Tb_WeiXinPayCertificate model=new MobileSoft.Model.Unified.Tb_WeiXinPayCertificate();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_WeiXinPayCertificate_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				model.Id=ds.Tables[0].Rows[0]["Id"].ToString();
				model.CommunityId=ds.Tables[0].Rows[0]["CommunityId"].ToString();
				model.appid=ds.Tables[0].Rows[0]["appid"].ToString();
				model.mch_id=ds.Tables[0].Rows[0]["mch_id"].ToString();
				model.appkey=ds.Tables[0].Rows[0]["appkey"].ToString();
				model.appsecret=ds.Tables[0].Rows[0]["appsecret"].ToString();
				model.SSLCERT_PATH=ds.Tables[0].Rows[0]["SSLCERT_PATH"].ToString();
				model.SSLCERT_PASSWORD=ds.Tables[0].Rows[0]["SSLCERT_PASSWORD"].ToString();
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
			strSql.Append("select Id,CommunityId,appid,mch_id,appkey,appsecret,SSLCERT_PATH,SSLCERT_PASSWORD ");
			strSql.Append(" FROM Tb_WeiXinPayCertificate ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" Id,CommunityId,appid,mch_id,appkey,appsecret,SSLCERT_PATH,SSLCERT_PASSWORD ");
			strSql.Append(" FROM Tb_WeiXinPayCertificate ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
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
			parameters[5].Value = "SELECT * FROM Tb_WeiXinPayCertificate WHERE 1=1 " + StrCondition;
			parameters[6].Value = "Id";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

