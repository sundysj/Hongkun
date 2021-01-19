using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.Resources
{
	/// <summary>
	/// 数据访问类Dal_Tb_Resources_ReleaseVenuesSchedulingDetails。
	/// </summary>
	public class Dal_Tb_Resources_ReleaseVenuesSchedulingDetails
	{
		public Dal_Tb_Resources_ReleaseVenuesSchedulingDetails()
		{
            DbHelperSQL.ConnectionString = PubConstant.SQMBSContionString;
		}
		#region  成员方法

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long ReleaseVenuesSchedulingDetailsID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ReleaseVenuesSchedulingDetailsID", SqlDbType.BigInt)};
			parameters[0].Value = ReleaseVenuesSchedulingDetailsID;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_Resources_ReleaseVenuesSchedulingDetails_Exists",parameters,out rowsAffected);
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
        public long Add(MobileSoft.Model.Resources.Tb_Resources_ReleaseVenuesSchedulingDetails model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ReleaseVenuesSchedulingDetailsID", SqlDbType.BigInt,8),
					new SqlParameter("@ReleaseVenuesSchedulingDetailsStartTime", SqlDbType.NVarChar,50),
					new SqlParameter("@ReleaseVenuesSchedulingDetailsEndTime", SqlDbType.NVarChar,50),
					new SqlParameter("@ReleaseVenuesSchedulingDetailsSalePrice", SqlDbType.Float,8),
					new SqlParameter("@ReleaseVenuesSchedulingDetailsDiscountPrice", SqlDbType.Float,8),
					new SqlParameter("@ReleaseVenuesID", SqlDbType.BigInt,8)};
			parameters[0].Direction = ParameterDirection.Output;
			parameters[1].Value = model.ReleaseVenuesSchedulingDetailsStartTime;
			parameters[2].Value = model.ReleaseVenuesSchedulingDetailsEndTime;
			parameters[3].Value = model.ReleaseVenuesSchedulingDetailsSalePrice;
			parameters[4].Value = model.ReleaseVenuesSchedulingDetailsDiscountPrice;
			parameters[5].Value = model.ReleaseVenuesID;

			DbHelperSQL.RunProcedure("Proc_Tb_Resources_ReleaseVenuesSchedulingDetails_ADD",parameters,out rowsAffected);
            return (long)parameters[0].Value;
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.Resources.Tb_Resources_ReleaseVenuesSchedulingDetails model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ReleaseVenuesSchedulingDetailsID", SqlDbType.BigInt,8),
					new SqlParameter("@ReleaseVenuesSchedulingDetailsStartTime", SqlDbType.NVarChar,50),
					new SqlParameter("@ReleaseVenuesSchedulingDetailsEndTime", SqlDbType.NVarChar,50),
					new SqlParameter("@ReleaseVenuesSchedulingDetailsSalePrice", SqlDbType.Float,8),
					new SqlParameter("@ReleaseVenuesSchedulingDetailsDiscountPrice", SqlDbType.Float,8),
					new SqlParameter("@ReleaseVenuesID", SqlDbType.BigInt,8)};
			parameters[0].Value = model.ReleaseVenuesSchedulingDetailsID;
			parameters[1].Value = model.ReleaseVenuesSchedulingDetailsStartTime;
			parameters[2].Value = model.ReleaseVenuesSchedulingDetailsEndTime;
			parameters[3].Value = model.ReleaseVenuesSchedulingDetailsSalePrice;
			parameters[4].Value = model.ReleaseVenuesSchedulingDetailsDiscountPrice;
			parameters[5].Value = model.ReleaseVenuesID;

			DbHelperSQL.RunProcedure("Proc_Tb_Resources_ReleaseVenuesSchedulingDetails_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long ReleaseVenuesSchedulingDetailsID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ReleaseVenuesSchedulingDetailsID", SqlDbType.BigInt)};
			parameters[0].Value = ReleaseVenuesSchedulingDetailsID;

			DbHelperSQL.RunProcedure("Proc_Tb_Resources_ReleaseVenuesSchedulingDetails_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.Resources.Tb_Resources_ReleaseVenuesSchedulingDetails GetModel(long ReleaseVenuesSchedulingDetailsID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@ReleaseVenuesSchedulingDetailsID", SqlDbType.BigInt)};
			parameters[0].Value = ReleaseVenuesSchedulingDetailsID;

			MobileSoft.Model.Resources.Tb_Resources_ReleaseVenuesSchedulingDetails model=new MobileSoft.Model.Resources.Tb_Resources_ReleaseVenuesSchedulingDetails();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_Resources_ReleaseVenuesSchedulingDetails_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ReleaseVenuesSchedulingDetailsID"].ToString()!="")
				{
					model.ReleaseVenuesSchedulingDetailsID=long.Parse(ds.Tables[0].Rows[0]["ReleaseVenuesSchedulingDetailsID"].ToString());
				}
				model.ReleaseVenuesSchedulingDetailsStartTime=ds.Tables[0].Rows[0]["ReleaseVenuesSchedulingDetailsStartTime"].ToString();
				model.ReleaseVenuesSchedulingDetailsEndTime=ds.Tables[0].Rows[0]["ReleaseVenuesSchedulingDetailsEndTime"].ToString();
				if(ds.Tables[0].Rows[0]["ReleaseVenuesSchedulingDetailsSalePrice"].ToString()!="")
				{
					model.ReleaseVenuesSchedulingDetailsSalePrice=decimal.Parse(ds.Tables[0].Rows[0]["ReleaseVenuesSchedulingDetailsSalePrice"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ReleaseVenuesSchedulingDetailsDiscountPrice"].ToString()!="")
				{
					model.ReleaseVenuesSchedulingDetailsDiscountPrice=decimal.Parse(ds.Tables[0].Rows[0]["ReleaseVenuesSchedulingDetailsDiscountPrice"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ReleaseVenuesID"].ToString()!="")
				{
					model.ReleaseVenuesID=long.Parse(ds.Tables[0].Rows[0]["ReleaseVenuesID"].ToString());
				}
				return model;
			}
			else
			{
				return null;
			}
		}

        /// <summary>
        /// 删除执行条件数据
        /// </summary>
        public int DeleteAll(string strWhere)
        {
            string strSql = "DELETE Tb_Resources_ReleaseVenuesSchedulingDetails";
            strSql = strSql + " where " + strWhere;
            return DbHelperSQL.ExecuteSql(strSql.ToString());
        }


		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ReleaseVenuesSchedulingDetailsID,ReleaseVenuesSchedulingDetailsStartTime,ReleaseVenuesSchedulingDetailsEndTime,ReleaseVenuesSchedulingDetailsSalePrice,ReleaseVenuesSchedulingDetailsDiscountPrice,ReleaseVenuesID ");
			strSql.Append(" FROM Tb_Resources_ReleaseVenuesSchedulingDetails ");
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
			strSql.Append(" ReleaseVenuesSchedulingDetailsID,ReleaseVenuesSchedulingDetailsStartTime,ReleaseVenuesSchedulingDetailsEndTime,ReleaseVenuesSchedulingDetailsSalePrice,ReleaseVenuesSchedulingDetailsDiscountPrice,ReleaseVenuesID ");
			strSql.Append(" FROM Tb_Resources_ReleaseVenuesSchedulingDetails ");
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
			parameters[5].Value = "SELECT * FROM Tb_Resources_ReleaseVenuesSchedulingDetails WHERE 1=1 " + StrCondition;
			parameters[6].Value = "ReleaseVenuesSchedulingDetailsID";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

