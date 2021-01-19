using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.Unified
{
	/// <summary>
	/// 数据访问类Dal_Tb_UnionPayCertificate。
	/// </summary>
	public class Dal_Tb_UnionPayCertificate
	{
		public Dal_Tb_UnionPayCertificate()
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

			int result= DbHelperSQL.RunProcedure("Proc_Tb_UnionPayCertificate_Exists",parameters,out rowsAffected);
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
		public void Add(MobileSoft.Model.Unified.Tb_UnionPayCertificate model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.VarChar,36),
					new SqlParameter("@CommunityId", SqlDbType.VarChar,36),
					new SqlParameter("@signCertPath", SqlDbType.VarChar,3999),
					new SqlParameter("@signCertPwd", SqlDbType.VarChar,36),
					new SqlParameter("@validateCertDir", SqlDbType.VarChar,3999),
					new SqlParameter("@encryptCert", SqlDbType.VarChar,3999),
					new SqlParameter("@merId", SqlDbType.VarChar,36),
					new SqlParameter("@accNo", SqlDbType.VarChar,36)};
			parameters[0].Value = model.Id;
			parameters[1].Value = model.CommunityId;
			parameters[2].Value = model.signCertPath;
			parameters[3].Value = model.signCertPwd;
			parameters[4].Value = model.validateCertDir;
			parameters[5].Value = model.encryptCert;
			parameters[6].Value = model.merId;
			parameters[7].Value = model.accNo;

			DbHelperSQL.RunProcedure("Proc_Tb_UnionPayCertificate_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.Unified.Tb_UnionPayCertificate model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.VarChar,36),
					new SqlParameter("@CommunityId", SqlDbType.VarChar,36),
					new SqlParameter("@signCertPath", SqlDbType.VarChar,3999),
					new SqlParameter("@signCertPwd", SqlDbType.VarChar,36),
					new SqlParameter("@validateCertDir", SqlDbType.VarChar,3999),
					new SqlParameter("@encryptCert", SqlDbType.VarChar,3999),
					new SqlParameter("@merId", SqlDbType.VarChar,36),
					new SqlParameter("@accNo", SqlDbType.VarChar,36)};
			parameters[0].Value = model.Id;
			parameters[1].Value = model.CommunityId;
			parameters[2].Value = model.signCertPath;
			parameters[3].Value = model.signCertPwd;
			parameters[4].Value = model.validateCertDir;
			parameters[5].Value = model.encryptCert;
			parameters[6].Value = model.merId;
			parameters[7].Value = model.accNo;

			DbHelperSQL.RunProcedure("Proc_Tb_UnionPayCertificate_Update",parameters,out rowsAffected);
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

			DbHelperSQL.RunProcedure("Proc_Tb_UnionPayCertificate_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.Unified.Tb_UnionPayCertificate GetModel(string Id)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.VarChar,50)};
			parameters[0].Value = Id;

			MobileSoft.Model.Unified.Tb_UnionPayCertificate model=new MobileSoft.Model.Unified.Tb_UnionPayCertificate();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_UnionPayCertificate_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				model.Id=ds.Tables[0].Rows[0]["Id"].ToString();
				model.CommunityId=ds.Tables[0].Rows[0]["CommunityId"].ToString();
				model.signCertPath=ds.Tables[0].Rows[0]["signCertPath"].ToString();
				model.signCertPwd=ds.Tables[0].Rows[0]["signCertPwd"].ToString();
				model.validateCertDir=ds.Tables[0].Rows[0]["validateCertDir"].ToString();
				model.encryptCert=ds.Tables[0].Rows[0]["encryptCert"].ToString();
				model.merId=ds.Tables[0].Rows[0]["merId"].ToString();
				model.accNo=ds.Tables[0].Rows[0]["accNo"].ToString();
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
			strSql.Append("select Id,CommunityId,signCertPath,signCertPwd,validateCertDir,encryptCert,merId,accNo ");
			strSql.Append(" FROM Tb_UnionPayCertificate ");
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
			strSql.Append(" Id,CommunityId,signCertPath,signCertPwd,validateCertDir,encryptCert,merId,accNo ");
			strSql.Append(" FROM Tb_UnionPayCertificate ");
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
			parameters[5].Value = "SELECT * FROM Tb_UnionPayCertificate WHERE 1=1 " + StrCondition;
			parameters[6].Value = "Id";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

