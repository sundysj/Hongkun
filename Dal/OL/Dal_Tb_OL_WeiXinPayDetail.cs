using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.OL
{
	/// <summary>
	/// 数据访问类Dal_Tb_OL_WeiXinPayDetail。
	/// </summary>
	public class Dal_Tb_OL_WeiXinPayDetail
	{
		public Dal_Tb_OL_WeiXinPayDetail()
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

			int result= DbHelperSQL.RunProcedure("Proc_Tb_OL_WeiXinPayDetail_Exists",parameters,out rowsAffected);
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
		public void Add(MobileSoft.Model.OL.Tb_OL_WeiXinPayDetail model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.VarChar,36),
					new SqlParameter("@PayOrderId", SqlDbType.VarChar,36),
					new SqlParameter("@FeesId", SqlDbType.BigInt,8),
					new SqlParameter("@DueAmount", SqlDbType.Decimal,9),
					new SqlParameter("@LateFeeAmount", SqlDbType.Decimal,9),
					new SqlParameter("@PaidAmount", SqlDbType.Decimal,9)};
			parameters[0].Value = model.Id;
			parameters[1].Value = model.PayOrderId;
			parameters[2].Value = model.FeesId;
			parameters[3].Value = model.DueAmount;
			parameters[4].Value = model.LateFeeAmount;
			parameters[5].Value = model.PaidAmount;

			DbHelperSQL.RunProcedure("Proc_Tb_OL_WeiXinPayDetail_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.OL.Tb_OL_WeiXinPayDetail model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.VarChar,36),
					new SqlParameter("@PayOrderId", SqlDbType.VarChar,36),
					new SqlParameter("@FeesId", SqlDbType.BigInt,8),
					new SqlParameter("@DueAmount", SqlDbType.Decimal,9),
					new SqlParameter("@LateFeeAmount", SqlDbType.Decimal,9),
					new SqlParameter("@PaidAmount", SqlDbType.Decimal,9)};
			parameters[0].Value = model.Id;
			parameters[1].Value = model.PayOrderId;
			parameters[2].Value = model.FeesId;
			parameters[3].Value = model.DueAmount;
			parameters[4].Value = model.LateFeeAmount;
			parameters[5].Value = model.PaidAmount;

			DbHelperSQL.RunProcedure("Proc_Tb_OL_WeiXinPayDetail_Update",parameters,out rowsAffected);
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

			DbHelperSQL.RunProcedure("Proc_Tb_OL_WeiXinPayDetail_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.OL.Tb_OL_WeiXinPayDetail GetModel(string Id)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.VarChar,50)};
			parameters[0].Value = Id;

			MobileSoft.Model.OL.Tb_OL_WeiXinPayDetail model=new MobileSoft.Model.OL.Tb_OL_WeiXinPayDetail();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_OL_WeiXinPayDetail_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				model.Id=ds.Tables[0].Rows[0]["Id"].ToString();
				model.PayOrderId=ds.Tables[0].Rows[0]["PayOrderId"].ToString();
				if(ds.Tables[0].Rows[0]["FeesId"].ToString()!="")
				{
					model.FeesId=long.Parse(ds.Tables[0].Rows[0]["FeesId"].ToString());
				}
				if(ds.Tables[0].Rows[0]["DueAmount"].ToString()!="")
				{
					model.DueAmount=decimal.Parse(ds.Tables[0].Rows[0]["DueAmount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["LateFeeAmount"].ToString()!="")
				{
					model.LateFeeAmount=decimal.Parse(ds.Tables[0].Rows[0]["LateFeeAmount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["PaidAmount"].ToString()!="")
				{
					model.PaidAmount=decimal.Parse(ds.Tables[0].Rows[0]["PaidAmount"].ToString());
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
			strSql.Append("select Id,PayOrderId,FeesId,DueAmount,LateFeeAmount,PaidAmount ");
			strSql.Append(" FROM Tb_OL_WeiXinPayDetail ");
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
			strSql.Append(" Id,PayOrderId,FeesId,DueAmount,LateFeeAmount,PaidAmount ");
			strSql.Append(" FROM Tb_OL_WeiXinPayDetail ");
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
			parameters[5].Value = "SELECT * FROM Tb_OL_WeiXinPayDetail WHERE 1=1 " + StrCondition;
			parameters[6].Value = "Id";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

