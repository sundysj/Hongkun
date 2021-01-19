using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.OAPublicWork
{
	/// <summary>
	/// 数据访问类Dal_Tb_OAPublicWork_OfficialConference。
	/// </summary>
	public class Dal_Tb_OAPublicWork_OfficialConference
	{
		public Dal_Tb_OAPublicWork_OfficialConference()
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

			int result= DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_OfficialConference_Exists",parameters,out rowsAffected);
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
		public int Add(MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_OfficialConference model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.Int,4),
					new SqlParameter("@Tb_WorkFlow_FlowSort_InfoId", SqlDbType.Int,4),
					new SqlParameter("@ConferenceName", SqlDbType.NVarChar,200),
					new SqlParameter("@UserCode", SqlDbType.NVarChar,20),
					new SqlParameter("@ConferenceCode", SqlDbType.VarChar,20),
					new SqlParameter("@ConferenceTitle", SqlDbType.VarChar,100),
					new SqlParameter("@ConferenceExplain", SqlDbType.VarChar,5000),
					new SqlParameter("@StartDate", SqlDbType.VarChar,20),
					new SqlParameter("@EndDate", SqlDbType.VarChar,20),
					new SqlParameter("@Moderator", SqlDbType.VarChar,20),
					new SqlParameter("@Convenor", SqlDbType.VarChar,20),
					new SqlParameter("@ConferenceList", SqlDbType.VarChar,500),
					new SqlParameter("@DraftingUser", SqlDbType.VarChar,500),
					new SqlParameter("@DocumentUrl", SqlDbType.VarChar,500),
					new SqlParameter("@ConferencePlace", SqlDbType.NVarChar,500),
					new SqlParameter("@WorkStartDate", SqlDbType.DateTime)};
			parameters[0].Direction = ParameterDirection.Output;
			parameters[1].Value = model.Tb_WorkFlow_FlowSort_InfoId;
			parameters[2].Value = model.ConferenceName;
			parameters[3].Value = model.UserCode;
			parameters[4].Value = model.ConferenceCode;
			parameters[5].Value = model.ConferenceTitle;
			parameters[6].Value = model.ConferenceExplain;
			parameters[7].Value = model.StartDate;
			parameters[8].Value = model.EndDate;
			parameters[9].Value = model.Moderator;
			parameters[10].Value = model.Convenor;
			parameters[11].Value = model.ConferenceList;
			parameters[12].Value = model.DraftingUser;
			parameters[13].Value = model.DocumentUrl;
			parameters[14].Value = model.ConferencePlace;
			parameters[15].Value = model.WorkStartDate;

			DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_OfficialConference_ADD",parameters,out rowsAffected);
			return (int)parameters[0].Value;
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_OfficialConference model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.Int,4),
					new SqlParameter("@Tb_WorkFlow_FlowSort_InfoId", SqlDbType.Int,4),
					new SqlParameter("@ConferenceName", SqlDbType.NVarChar,200),
					new SqlParameter("@UserCode", SqlDbType.NVarChar,20),
					new SqlParameter("@ConferenceCode", SqlDbType.VarChar,20),
					new SqlParameter("@ConferenceTitle", SqlDbType.VarChar,100),
					new SqlParameter("@ConferenceExplain", SqlDbType.VarChar,5000),
					new SqlParameter("@StartDate", SqlDbType.VarChar,20),
					new SqlParameter("@EndDate", SqlDbType.VarChar,20),
					new SqlParameter("@Moderator", SqlDbType.VarChar,20),
					new SqlParameter("@Convenor", SqlDbType.VarChar,20),
					new SqlParameter("@ConferenceList", SqlDbType.VarChar,500),
					new SqlParameter("@DraftingUser", SqlDbType.VarChar,500),
					new SqlParameter("@DocumentUrl", SqlDbType.VarChar,500),
					new SqlParameter("@ConferencePlace", SqlDbType.NVarChar,500),
					new SqlParameter("@WorkStartDate", SqlDbType.DateTime)};
			parameters[0].Value = model.InfoId;
			parameters[1].Value = model.Tb_WorkFlow_FlowSort_InfoId;
			parameters[2].Value = model.ConferenceName;
			parameters[3].Value = model.UserCode;
			parameters[4].Value = model.ConferenceCode;
			parameters[5].Value = model.ConferenceTitle;
			parameters[6].Value = model.ConferenceExplain;
			parameters[7].Value = model.StartDate;
			parameters[8].Value = model.EndDate;
			parameters[9].Value = model.Moderator;
			parameters[10].Value = model.Convenor;
			parameters[11].Value = model.ConferenceList;
			parameters[12].Value = model.DraftingUser;
			parameters[13].Value = model.DocumentUrl;
			parameters[14].Value = model.ConferencePlace;
			parameters[15].Value = model.WorkStartDate;

			DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_OfficialConference_Update",parameters,out rowsAffected);
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

			DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_OfficialConference_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_OfficialConference GetModel(int InfoId)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.Int,4)};
			parameters[0].Value = InfoId;

			MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_OfficialConference model=new MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_OfficialConference();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_OfficialConference_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["InfoId"].ToString()!="")
				{
					model.InfoId=int.Parse(ds.Tables[0].Rows[0]["InfoId"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Tb_WorkFlow_FlowSort_InfoId"].ToString()!="")
				{
					model.Tb_WorkFlow_FlowSort_InfoId=int.Parse(ds.Tables[0].Rows[0]["Tb_WorkFlow_FlowSort_InfoId"].ToString());
				}
				model.ConferenceName=ds.Tables[0].Rows[0]["ConferenceName"].ToString();
				model.UserCode=ds.Tables[0].Rows[0]["UserCode"].ToString();
				model.ConferenceCode=ds.Tables[0].Rows[0]["ConferenceCode"].ToString();
				model.ConferenceTitle=ds.Tables[0].Rows[0]["ConferenceTitle"].ToString();
				model.ConferenceExplain=ds.Tables[0].Rows[0]["ConferenceExplain"].ToString();
				model.StartDate=ds.Tables[0].Rows[0]["StartDate"].ToString();
				model.EndDate=ds.Tables[0].Rows[0]["EndDate"].ToString();
				model.Moderator=ds.Tables[0].Rows[0]["Moderator"].ToString();
				model.Convenor=ds.Tables[0].Rows[0]["Convenor"].ToString();
				model.ConferenceList=ds.Tables[0].Rows[0]["ConferenceList"].ToString();
				model.DraftingUser=ds.Tables[0].Rows[0]["DraftingUser"].ToString();
				model.DocumentUrl=ds.Tables[0].Rows[0]["DocumentUrl"].ToString();
				model.ConferencePlace=ds.Tables[0].Rows[0]["ConferencePlace"].ToString();
				if(ds.Tables[0].Rows[0]["WorkStartDate"].ToString()!="")
				{
					model.WorkStartDate=DateTime.Parse(ds.Tables[0].Rows[0]["WorkStartDate"].ToString());
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
			strSql.Append("select InfoId,Tb_WorkFlow_FlowSort_InfoId,ConferenceName,UserCode,ConferenceCode,ConferenceTitle,ConferenceExplain,StartDate,EndDate,Moderator,Convenor,ConferenceList,DraftingUser,DocumentUrl,ConferencePlace,WorkStartDate ");
			strSql.Append(" FROM Tb_OAPublicWork_OfficialConference ");
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
			strSql.Append(" InfoId,Tb_WorkFlow_FlowSort_InfoId,ConferenceName,UserCode,ConferenceCode,ConferenceTitle,ConferenceExplain,StartDate,EndDate,Moderator,Convenor,ConferenceList,DraftingUser,DocumentUrl,ConferencePlace,WorkStartDate ");
			strSql.Append(" FROM Tb_OAPublicWork_OfficialConference ");
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
			parameters[5].Value = "SELECT * FROM Tb_OAPublicWork_OfficialConference WHERE 1=1 " + StrCondition;
			parameters[6].Value = "InfoId";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

