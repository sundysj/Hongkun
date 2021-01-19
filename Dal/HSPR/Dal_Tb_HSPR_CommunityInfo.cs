using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.HSPR
{
	/// <summary>
	/// 数据访问类Dal_Tb_HSPR_CommunityInfo。
	/// </summary>
	public class Dal_Tb_HSPR_CommunityInfo
	{
		public Dal_Tb_HSPR_CommunityInfo()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long InfoID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoID", SqlDbType.BigInt)};
			parameters[0].Value = InfoID;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_HSPR_CommunityInfo_Exists",parameters,out rowsAffected);
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
		public void Add(MobileSoft.Model.HSPR.Tb_HSPR_CommunityInfo model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoID", SqlDbType.BigInt,8),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@Heading", SqlDbType.NVarChar,50),
					new SqlParameter("@IssueDate", SqlDbType.DateTime),
					new SqlParameter("@ShowEndDate", SqlDbType.DateTime),
					new SqlParameter("@InfoSource", SqlDbType.NVarChar,20),
					new SqlParameter("@InfoType", SqlDbType.NVarChar,20),
					new SqlParameter("@IsAudit", SqlDbType.SmallInt,2),
					new SqlParameter("@InfoContent", SqlDbType.NText),
					new SqlParameter("@ImageUrl", SqlDbType.NVarChar,100),
					new SqlParameter("@CommInfoSynchCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@SynchFlag", SqlDbType.SmallInt,2)};
			parameters[0].Value = model.InfoID;
			parameters[1].Value = model.CommID;
			parameters[2].Value = model.Heading;
			parameters[3].Value = model.IssueDate;
			parameters[4].Value = model.ShowEndDate;
			parameters[5].Value = model.InfoSource;
			parameters[6].Value = model.InfoType;
			parameters[7].Value = model.IsAudit;
			parameters[8].Value = model.InfoContent;
			parameters[9].Value = model.ImageUrl;
			parameters[10].Value = model.CommInfoSynchCode;
			parameters[11].Value = model.SynchFlag;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_CommunityInfo_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.HSPR.Tb_HSPR_CommunityInfo model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoID", SqlDbType.BigInt,8),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@Heading", SqlDbType.NVarChar,50),
					new SqlParameter("@IssueDate", SqlDbType.DateTime),
					new SqlParameter("@ShowEndDate", SqlDbType.DateTime),
					new SqlParameter("@InfoSource", SqlDbType.NVarChar,20),
					new SqlParameter("@InfoType", SqlDbType.NVarChar,20),
					new SqlParameter("@IsAudit", SqlDbType.SmallInt,2),
					new SqlParameter("@InfoContent", SqlDbType.NText),
					new SqlParameter("@ImageUrl", SqlDbType.NVarChar,100),
					new SqlParameter("@CommInfoSynchCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@SynchFlag", SqlDbType.SmallInt,2)};
			parameters[0].Value = model.InfoID;
			parameters[1].Value = model.CommID;
			parameters[2].Value = model.Heading;
			parameters[3].Value = model.IssueDate;
			parameters[4].Value = model.ShowEndDate;
			parameters[5].Value = model.InfoSource;
			parameters[6].Value = model.InfoType;
			parameters[7].Value = model.IsAudit;
			parameters[8].Value = model.InfoContent;
			parameters[9].Value = model.ImageUrl;
			parameters[10].Value = model.CommInfoSynchCode;
			parameters[11].Value = model.SynchFlag;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_CommunityInfo_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long InfoID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoID", SqlDbType.BigInt)};
			parameters[0].Value = InfoID;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_CommunityInfo_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_CommunityInfo GetModel(long InfoID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@InfoID", SqlDbType.BigInt)};
			parameters[0].Value = InfoID;

			MobileSoft.Model.HSPR.Tb_HSPR_CommunityInfo model=new MobileSoft.Model.HSPR.Tb_HSPR_CommunityInfo();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_HSPR_CommunityInfo_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["InfoID"].ToString()!="")
				{
					model.InfoID=long.Parse(ds.Tables[0].Rows[0]["InfoID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CommID"].ToString()!="")
				{
					model.CommID=int.Parse(ds.Tables[0].Rows[0]["CommID"].ToString());
				}
				model.Heading=ds.Tables[0].Rows[0]["Heading"].ToString();
				if(ds.Tables[0].Rows[0]["IssueDate"].ToString()!="")
				{
					model.IssueDate=DateTime.Parse(ds.Tables[0].Rows[0]["IssueDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ShowEndDate"].ToString()!="")
				{
					model.ShowEndDate=DateTime.Parse(ds.Tables[0].Rows[0]["ShowEndDate"].ToString());
				}
				model.InfoSource=ds.Tables[0].Rows[0]["InfoSource"].ToString();
				model.InfoType=ds.Tables[0].Rows[0]["InfoType"].ToString();
				if(ds.Tables[0].Rows[0]["IsAudit"].ToString()!="")
				{
					model.IsAudit=int.Parse(ds.Tables[0].Rows[0]["IsAudit"].ToString());
				}
				model.InfoContent=ds.Tables[0].Rows[0]["InfoContent"].ToString();
				model.ImageUrl=ds.Tables[0].Rows[0]["ImageUrl"].ToString();
				if(ds.Tables[0].Rows[0]["CommInfoSynchCode"].ToString()!="")
				{
					model.CommInfoSynchCode=new Guid(ds.Tables[0].Rows[0]["CommInfoSynchCode"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SynchFlag"].ToString()!="")
				{
					model.SynchFlag=int.Parse(ds.Tables[0].Rows[0]["SynchFlag"].ToString());
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
                  strSql.Append(" FROM View_HSPR_CommunityInfo_Filter ");
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
                  strSql.Append(" FROM View_HSPR_CommunityInfo_Filter ");
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
                  parameters[5].Value = "SELECT * FROM View_HSPR_CommunityInfo_Filter WHERE 1=1 " + StrCondition;
			parameters[6].Value = "InfoID";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法

	}
}

