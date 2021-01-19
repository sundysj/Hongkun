using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.HSPR
{
	/// <summary>
	/// 数据访问类Dal_Tb_HSPR_CommunitySuggestions。
	/// </summary>
	public class Dal_Tb_HSPR_CommunitySuggestions
	{
		public Dal_Tb_HSPR_CommunitySuggestions()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string SuggestionsID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@SuggestionsID", SqlDbType.NVarChar,50)};
			parameters[0].Value = SuggestionsID;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_HSPR_CommunitySuggestions_Exists",parameters,out rowsAffected);
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
		public void Add(MobileSoft.Model.HSPR.Tb_HSPR_CommunitySuggestions model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@SuggestionsID", SqlDbType.NVarChar,50),
					new SqlParameter("@CommID", SqlDbType.BigInt,8),
					new SqlParameter("@CustID", SqlDbType.BigInt,8),
					new SqlParameter("@CustName", SqlDbType.NVarChar,50),
					new SqlParameter("@RoomID", SqlDbType.BigInt,8),
					new SqlParameter("@RoomSign", SqlDbType.NVarChar,50),
					new SqlParameter("@SuggestionsType", SqlDbType.NVarChar,50),
					new SqlParameter("@SuggestionsTitle", SqlDbType.NVarChar,200),
					new SqlParameter("@SuggestionsContent", SqlDbType.NVarChar,2000),
					new SqlParameter("@IssueDate", SqlDbType.DateTime),
					new SqlParameter("@LinkPhone", SqlDbType.NVarChar,80),
					new SqlParameter("@SuggestionsImages", SqlDbType.NVarChar,2000),
					new SqlParameter("@ReplyStats", SqlDbType.NVarChar,50),
					new SqlParameter("@ReplyDate", SqlDbType.DateTime),
					new SqlParameter("@ReplyContent", SqlDbType.NVarChar,2000),
					new SqlParameter("@CustEvaluation", SqlDbType.NVarChar,500),
					new SqlParameter("@EvaluationDate", SqlDbType.DateTime),
					new SqlParameter("@IsDelete", SqlDbType.Int,4),
                    new SqlParameter("@EvaluationLevel",SqlDbType.Int,4)
            };
			parameters[0].Value = model.SuggestionsID;
			parameters[1].Value = model.CommID;
			parameters[2].Value = model.CustID;
			parameters[3].Value = model.CustName;
			parameters[4].Value = model.RoomID;
			parameters[5].Value = model.RoomSign;
			parameters[6].Value = model.SuggestionsType;
			parameters[7].Value = model.SuggestionsTitle;
			parameters[8].Value = model.SuggestionsContent;
			parameters[9].Value = model.IssueDate;
			parameters[10].Value = model.LinkPhone;
			parameters[11].Value = model.SuggestionsImages;
			parameters[12].Value = model.ReplyStats;
			parameters[13].Value = model.ReplyDate;
			parameters[14].Value = model.ReplyContent;
			parameters[15].Value = model.CustEvaluation;
			parameters[16].Value = model.EvaluationDate;
			parameters[17].Value = model.IsDelete;
            parameters[18].Value = model.EvaluationLevel;


            DbHelperSQL.RunProcedure("Proc_Tb_HSPR_CommunitySuggestions_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.HSPR.Tb_HSPR_CommunitySuggestions model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@SuggestionsID", SqlDbType.NVarChar,50),
					new SqlParameter("@CommID", SqlDbType.BigInt,8),
					new SqlParameter("@CustID", SqlDbType.BigInt,8),
					new SqlParameter("@CustName", SqlDbType.NVarChar,50),
					new SqlParameter("@RoomID", SqlDbType.BigInt,8),
					new SqlParameter("@RoomSign", SqlDbType.NVarChar,50),
					new SqlParameter("@SuggestionsType", SqlDbType.NVarChar,50),
					new SqlParameter("@SuggestionsTitle", SqlDbType.NVarChar,200),
					new SqlParameter("@SuggestionsContent", SqlDbType.NVarChar,2000),
					new SqlParameter("@IssueDate", SqlDbType.DateTime),
					new SqlParameter("@LinkPhone", SqlDbType.NVarChar,80),
					new SqlParameter("@SuggestionsImages", SqlDbType.NVarChar,2000),
					new SqlParameter("@ReplyStats", SqlDbType.NVarChar,50),
					new SqlParameter("@ReplyDate", SqlDbType.DateTime),
					new SqlParameter("@ReplyContent", SqlDbType.NVarChar,2000),
					new SqlParameter("@CustEvaluation", SqlDbType.NVarChar,500),
					new SqlParameter("@EvaluationDate", SqlDbType.DateTime),
					new SqlParameter("@IsDelete", SqlDbType.Int,4),
            new SqlParameter("@EvaluationLevel",SqlDbType.Int,4)};
			parameters[0].Value = model.SuggestionsID;
			parameters[1].Value = model.CommID;
			parameters[2].Value = model.CustID;
			parameters[3].Value = model.CustName;
			parameters[4].Value = model.RoomID;
			parameters[5].Value = model.RoomSign;
			parameters[6].Value = model.SuggestionsType;
			parameters[7].Value = model.SuggestionsTitle;
			parameters[8].Value = model.SuggestionsContent;
			parameters[9].Value = model.IssueDate;
			parameters[10].Value = model.LinkPhone;
			parameters[11].Value = model.SuggestionsImages;
			parameters[12].Value = model.ReplyStats;
			parameters[13].Value = model.ReplyDate;
			parameters[14].Value = model.ReplyContent;
			parameters[15].Value = model.CustEvaluation;
			parameters[16].Value = model.EvaluationDate;
			parameters[17].Value = model.IsDelete;
            parameters[18].Value = model.EvaluationLevel;
            DbHelperSQL.RunProcedure("Proc_Tb_HSPR_CommunitySuggestions_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(string SuggestionsID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@SuggestionsID", SqlDbType.NVarChar,50)};
			parameters[0].Value = SuggestionsID;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_CommunitySuggestions_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_CommunitySuggestions GetModel(string SuggestionsID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@SuggestionsID", SqlDbType.NVarChar,50)};
			parameters[0].Value = SuggestionsID;

			MobileSoft.Model.HSPR.Tb_HSPR_CommunitySuggestions model=new MobileSoft.Model.HSPR.Tb_HSPR_CommunitySuggestions();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_HSPR_CommunitySuggestions_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				model.SuggestionsID=ds.Tables[0].Rows[0]["SuggestionsID"].ToString();
				if(ds.Tables[0].Rows[0]["CommID"].ToString()!="")
				{
					model.CommID=long.Parse(ds.Tables[0].Rows[0]["CommID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CustID"].ToString()!="")
				{
					model.CustID=long.Parse(ds.Tables[0].Rows[0]["CustID"].ToString());
				}
				model.CustName=ds.Tables[0].Rows[0]["CustName"].ToString();
				if(ds.Tables[0].Rows[0]["RoomID"].ToString()!="")
				{
					model.RoomID=long.Parse(ds.Tables[0].Rows[0]["RoomID"].ToString());
				}
				model.RoomSign=ds.Tables[0].Rows[0]["RoomSign"].ToString();
				model.SuggestionsType=ds.Tables[0].Rows[0]["SuggestionsType"].ToString();
				model.SuggestionsTitle=ds.Tables[0].Rows[0]["SuggestionsTitle"].ToString();
				model.SuggestionsContent=ds.Tables[0].Rows[0]["SuggestionsContent"].ToString();
				if(ds.Tables[0].Rows[0]["IssueDate"].ToString()!="")
				{
					model.IssueDate=DateTime.Parse(ds.Tables[0].Rows[0]["IssueDate"].ToString());
				}
				model.LinkPhone=ds.Tables[0].Rows[0]["LinkPhone"].ToString();
				model.SuggestionsImages=ds.Tables[0].Rows[0]["SuggestionsImages"].ToString();
				model.ReplyStats=ds.Tables[0].Rows[0]["ReplyStats"].ToString();
				if(ds.Tables[0].Rows[0]["ReplyDate"].ToString()!="")
				{
					model.ReplyDate=DateTime.Parse(ds.Tables[0].Rows[0]["ReplyDate"].ToString());
				}
				model.ReplyContent=ds.Tables[0].Rows[0]["ReplyContent"].ToString();
				model.CustEvaluation=ds.Tables[0].Rows[0]["CustEvaluation"].ToString();
				if(ds.Tables[0].Rows[0]["EvaluationDate"].ToString()!="")
				{
					model.EvaluationDate=DateTime.Parse(ds.Tables[0].Rows[0]["EvaluationDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsDelete"].ToString()!="")
				{
					model.IsDelete=int.Parse(ds.Tables[0].Rows[0]["IsDelete"].ToString());
				}
                if (ds.Tables[0].Rows[0]["EvaluationLevel"].ToString() != "")
                {
                    model.EvaluationLevel = int.Parse(ds.Tables[0].Rows[0]["EvaluationLevel"].ToString());
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
			strSql.Append("select SuggestionsID,CommID,CustID,CustName,RoomID,RoomSign,SuggestionsType,SuggestionsTitle,SuggestionsContent,IssueDate,LinkPhone,SuggestionsImages,ReplyStats,ReplyDate,ReplyContent,CustEvaluation,EvaluationDate,IsDelete,EvaluationLevel ");
			strSql.Append(" FROM Tb_HSPR_CommunitySuggestions ");
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
			strSql.Append(" SuggestionsID,CommID,CustID,CustName,RoomID,RoomSign,SuggestionsType,SuggestionsTitle,SuggestionsContent,IssueDate,LinkPhone,SuggestionsImages,ReplyStats,ReplyDate,ReplyContent,CustEvaluation,EvaluationDate,IsDelete,EvaluationLevel ");
			strSql.Append(" FROM Tb_HSPR_CommunitySuggestions ");
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
			parameters[5].Value = "SELECT * FROM Tb_HSPR_CommunitySuggestions WHERE 1=1 " + StrCondition;
			parameters[6].Value = "SuggestionsID";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

