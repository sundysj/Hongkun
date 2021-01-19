using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.HSPR
{
	/// <summary>
	/// 数据访问类Dal_Tb_HSPR_CommSocialScope。
	/// </summary>
	public class Dal_Tb_HSPR_CommSocialScope
	{
		public Dal_Tb_HSPR_CommSocialScope()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string ScopeCode)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ScopeCode", SqlDbType.VarChar,50)};
			parameters[0].Value = ScopeCode;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_HSPR_CommSocialScope_Exists",parameters,out rowsAffected);
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
		public void Add(MobileSoft.Model.HSPR.Tb_HSPR_CommSocialScope model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ScopeCode", SqlDbType.VarChar,36),
					new SqlParameter("@CustCode", SqlDbType.VarChar,36),
					new SqlParameter("@CircleID", SqlDbType.VarChar,36),
					new SqlParameter("@CommID", SqlDbType.BigInt,8),
					new SqlParameter("@BussID", SqlDbType.BigInt,8),
					new SqlParameter("@IsDelete", SqlDbType.Int,4)};
			parameters[0].Value = model.ScopeCode;
			parameters[1].Value = model.CustCode;
			parameters[2].Value = model.CircleID;
			parameters[3].Value = model.CommID;
			parameters[4].Value = model.BussID;
			parameters[5].Value = model.IsDelete;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_CommSocialScope_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.HSPR.Tb_HSPR_CommSocialScope model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ScopeCode", SqlDbType.VarChar,36),
					new SqlParameter("@CustCode", SqlDbType.VarChar,36),
					new SqlParameter("@CircleID", SqlDbType.VarChar,36),
					new SqlParameter("@CommID", SqlDbType.BigInt,8),
					new SqlParameter("@BussID", SqlDbType.BigInt,8),
					new SqlParameter("@IsDelete", SqlDbType.Int,4)};
			parameters[0].Value = model.ScopeCode;
			parameters[1].Value = model.CustCode;
			parameters[2].Value = model.CircleID;
			parameters[3].Value = model.CommID;
			parameters[4].Value = model.BussID;
			parameters[5].Value = model.IsDelete;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_CommSocialScope_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(string ScopeCode)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ScopeCode", SqlDbType.VarChar,50)};
			parameters[0].Value = ScopeCode;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_CommSocialScope_Delete",parameters,out rowsAffected);
		}

        /// <summary>
        /// 退出圈子
        /// </summary>
        public void Delete(string CircleID, string CustSynchCode)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@CircleID", SqlDbType.VarChar,50),
                    new SqlParameter("@CustSynchCode", SqlDbType.VarChar,50)              
                                        };
            parameters[0].Value = CircleID;
            parameters[1].Value = CustSynchCode;
            DbHelperSQL.RunProcedure("Proc_Tb_HSPR_CommSocialScope_Exit", parameters, out rowsAffected);
        }

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_CommSocialScope GetModel(string ScopeCode)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@ScopeCode", SqlDbType.VarChar,50)};
			parameters[0].Value = ScopeCode;

			MobileSoft.Model.HSPR.Tb_HSPR_CommSocialScope model=new MobileSoft.Model.HSPR.Tb_HSPR_CommSocialScope();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_HSPR_CommSocialScope_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				model.ScopeCode=ds.Tables[0].Rows[0]["ScopeCode"].ToString();
				model.CustCode=ds.Tables[0].Rows[0]["CustCode"].ToString();
				model.CircleID=ds.Tables[0].Rows[0]["CircleID"].ToString();
				if(ds.Tables[0].Rows[0]["CommID"].ToString()!="")
				{
					model.CommID=long.Parse(ds.Tables[0].Rows[0]["CommID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["BussID"].ToString()!="")
				{
					model.BussID=long.Parse(ds.Tables[0].Rows[0]["BussID"].ToString());
				}
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
			strSql.Append("select * ");
			strSql.Append(" FROM View_HSPR_CommSocialScope_Filter ");
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
			strSql.Append(" * ");
            strSql.Append(" FROM View_HSPR_CommSocialScope_Filter ");
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
			parameters[5].Value = "SELECT * FROM Tb_HSPR_CommSocialScope WHERE 1=1 " + StrCondition;
			parameters[6].Value = "ScopeCode";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

