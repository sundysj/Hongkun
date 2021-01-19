using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.HSPR
{
	/// <summary>
	/// 数据访问类Dal_Tb_HSPR_CommunityServiceMatching。
	/// </summary>
	public class Dal_Tb_HSPR_CommunityServiceMatching
	{
		public Dal_Tb_HSPR_CommunityServiceMatching()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public long GetMaxId()
		{
		return DbHelperSQL.GetMaxID("ScID", "Tb_HSPR_CommunityServiceMatching"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long ScID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ScID", SqlDbType.BigInt)};
			parameters[0].Value = ScID;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_HSPR_CommunityServiceMatching_Exists",parameters,out rowsAffected);
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
		public void Add(MobileSoft.Model.HSPR.Tb_HSPR_CommunityServiceMatching model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ScID", SqlDbType.BigInt,8),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@ScType", SqlDbType.Int,4),
					new SqlParameter("@ScTyCode", SqlDbType.NVarChar,50),
					new SqlParameter("@ScNum", SqlDbType.Int,4),
					new SqlParameter("@ScName", SqlDbType.NVarChar,50),
					new SqlParameter("@ScAddress", SqlDbType.NVarChar,500),
					new SqlParameter("@ScLinkMan", SqlDbType.NVarChar,50),
					new SqlParameter("@ScLinkPhone", SqlDbType.NVarChar,50),
					new SqlParameter("@ScMemo", SqlDbType.NVarChar,500)};
			parameters[0].Value = model.ScID;
			parameters[1].Value = model.CommID;
			parameters[2].Value = model.ScType;
			parameters[3].Value = model.ScTyCode;
			parameters[4].Value = model.ScNum;
			parameters[5].Value = model.ScName;
			parameters[6].Value = model.ScAddress;
			parameters[7].Value = model.ScLinkMan;
			parameters[8].Value = model.ScLinkPhone;
			parameters[9].Value = model.ScMemo;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_CommunityServiceMatching_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.HSPR.Tb_HSPR_CommunityServiceMatching model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ScID", SqlDbType.BigInt,8),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@ScType", SqlDbType.Int,4),
					new SqlParameter("@ScTyCode", SqlDbType.NVarChar,50),
					new SqlParameter("@ScNum", SqlDbType.Int,4),
					new SqlParameter("@ScName", SqlDbType.NVarChar,50),
					new SqlParameter("@ScAddress", SqlDbType.NVarChar,500),
					new SqlParameter("@ScLinkMan", SqlDbType.NVarChar,50),
					new SqlParameter("@ScLinkPhone", SqlDbType.NVarChar,50),
					new SqlParameter("@ScMemo", SqlDbType.NVarChar,500)};
			parameters[0].Value = model.ScID;
			parameters[1].Value = model.CommID;
			parameters[2].Value = model.ScType;
			parameters[3].Value = model.ScTyCode;
			parameters[4].Value = model.ScNum;
			parameters[5].Value = model.ScName;
			parameters[6].Value = model.ScAddress;
			parameters[7].Value = model.ScLinkMan;
			parameters[8].Value = model.ScLinkPhone;
			parameters[9].Value = model.ScMemo;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_CommunityServiceMatching_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long ScID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ScID", SqlDbType.BigInt)};
			parameters[0].Value = ScID;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_CommunityServiceMatching_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_CommunityServiceMatching GetModel(long ScID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@ScID", SqlDbType.BigInt)};
			parameters[0].Value = ScID;

			MobileSoft.Model.HSPR.Tb_HSPR_CommunityServiceMatching model=new MobileSoft.Model.HSPR.Tb_HSPR_CommunityServiceMatching();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_HSPR_CommunityServiceMatching_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ScID"].ToString()!="")
				{
					model.ScID=long.Parse(ds.Tables[0].Rows[0]["ScID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CommID"].ToString()!="")
				{
					model.CommID=int.Parse(ds.Tables[0].Rows[0]["CommID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ScType"].ToString()!="")
				{
					model.ScType=int.Parse(ds.Tables[0].Rows[0]["ScType"].ToString());
				}
				model.ScTyCode=ds.Tables[0].Rows[0]["ScTyCode"].ToString();
				if(ds.Tables[0].Rows[0]["ScNum"].ToString()!="")
				{
					model.ScNum=int.Parse(ds.Tables[0].Rows[0]["ScNum"].ToString());
				}
				model.ScName=ds.Tables[0].Rows[0]["ScName"].ToString();
				model.ScAddress=ds.Tables[0].Rows[0]["ScAddress"].ToString();
				model.ScLinkMan=ds.Tables[0].Rows[0]["ScLinkMan"].ToString();
				model.ScLinkPhone=ds.Tables[0].Rows[0]["ScLinkPhone"].ToString();
				model.ScMemo=ds.Tables[0].Rows[0]["ScMemo"].ToString();
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
            strSql.Append("select ScID,CommID,ScType,ScTyCode,ScNum,ScName,ScAddress,ScLinkMan,ScLinkPhone,ScMemo,ScTyName ");
            strSql.Append(" FROM view_HSPR_CommunityServiceMatching_Filter ");
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
			strSql.Append(" ScID,CommID,ScType,ScTyCode,ScNum,ScName,ScAddress,ScLinkMan,ScLinkPhone,ScMemo ");
			strSql.Append(" FROM Tb_HSPR_CommunityServiceMatching ");
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
			parameters[5].Value = "SELECT * FROM Tb_HSPR_CommunityServiceMatching WHERE 1=1 " + StrCondition;
			parameters[6].Value = "ScID";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

