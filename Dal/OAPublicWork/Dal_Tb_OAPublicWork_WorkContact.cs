using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.OAPublicWork
{
	/// <summary>
	/// 数据访问类Dal_Tb_OAPublicWork_WorkContact。
	/// </summary>
	public class Dal_Tb_OAPublicWork_WorkContact
	{
		public Dal_Tb_OAPublicWork_WorkContact()
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

			int result= DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_WorkContact_Exists",parameters,out rowsAffected);
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
		public int Add(MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_WorkContact model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.Int,4),
					new SqlParameter("@Tb_WorkFlow_FlowSort_InfoId", SqlDbType.Int,4),
					new SqlParameter("@UserCode", SqlDbType.NVarChar,20),
					new SqlParameter("@Title", SqlDbType.VarChar,100),
					new SqlParameter("@WorkSper", SqlDbType.VarChar,50),
					new SqlParameter("@Ask", SqlDbType.VarChar,50),
					new SqlParameter("@ConferenceEndDate", SqlDbType.DateTime),
					new SqlParameter("@WorkEndDate", SqlDbType.DateTime),
					new SqlParameter("@ContactImportant", SqlDbType.NVarChar,20),
					new SqlParameter("@InfoContent", SqlDbType.Text),
					new SqlParameter("@DocumentUrl", SqlDbType.VarChar,2000),
					new SqlParameter("@ArrangeDate", SqlDbType.DateTime),
					new SqlParameter("@AssumeDepart", SqlDbType.VarChar,50),
					new SqlParameter("@AssumeUser", SqlDbType.VarChar,20),
					new SqlParameter("@CooperDepart", SqlDbType.VarChar,20),
					new SqlParameter("@CooperUser", SqlDbType.VarChar,20),
					new SqlParameter("@WorkStartDate", SqlDbType.DateTime)};
			parameters[0].Direction = ParameterDirection.Output;
			parameters[1].Value = model.Tb_WorkFlow_FlowSort_InfoId;
			parameters[2].Value = model.UserCode;
			parameters[3].Value = model.Title;
			parameters[4].Value = model.WorkSper;
			parameters[5].Value = model.Ask;
			parameters[6].Value = model.ConferenceEndDate;
			parameters[7].Value = model.WorkEndDate;
			parameters[8].Value = model.ContactImportant;
			parameters[9].Value = model.InfoContent;
			parameters[10].Value = model.DocumentUrl;
			parameters[11].Value = model.ArrangeDate;
			parameters[12].Value = model.AssumeDepart;
			parameters[13].Value = model.AssumeUser;
			parameters[14].Value = model.CooperDepart;
			parameters[15].Value = model.CooperUser;
			parameters[16].Value = model.WorkStartDate;

			DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_WorkContact_ADD",parameters,out rowsAffected);
			return (int)parameters[0].Value;
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_WorkContact model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.Int,4),
					new SqlParameter("@Tb_WorkFlow_FlowSort_InfoId", SqlDbType.Int,4),
					new SqlParameter("@UserCode", SqlDbType.NVarChar,20),
					new SqlParameter("@Title", SqlDbType.VarChar,100),
					new SqlParameter("@WorkSper", SqlDbType.VarChar,50),
					new SqlParameter("@Ask", SqlDbType.VarChar,50),
					new SqlParameter("@ConferenceEndDate", SqlDbType.DateTime),
					new SqlParameter("@WorkEndDate", SqlDbType.DateTime),
					new SqlParameter("@ContactImportant", SqlDbType.NVarChar,20),
					new SqlParameter("@InfoContent", SqlDbType.Text),
					new SqlParameter("@DocumentUrl", SqlDbType.VarChar,2000),
					new SqlParameter("@ArrangeDate", SqlDbType.DateTime),
					new SqlParameter("@AssumeDepart", SqlDbType.VarChar,50),
					new SqlParameter("@AssumeUser", SqlDbType.VarChar,20),
					new SqlParameter("@CooperDepart", SqlDbType.VarChar,20),
					new SqlParameter("@CooperUser", SqlDbType.VarChar,20),
					new SqlParameter("@WorkStartDate", SqlDbType.DateTime)};
			parameters[0].Value = model.InfoId;
			parameters[1].Value = model.Tb_WorkFlow_FlowSort_InfoId;
			parameters[2].Value = model.UserCode;
			parameters[3].Value = model.Title;
			parameters[4].Value = model.WorkSper;
			parameters[5].Value = model.Ask;
			parameters[6].Value = model.ConferenceEndDate;
			parameters[7].Value = model.WorkEndDate;
			parameters[8].Value = model.ContactImportant;
			parameters[9].Value = model.InfoContent;
			parameters[10].Value = model.DocumentUrl;
			parameters[11].Value = model.ArrangeDate;
			parameters[12].Value = model.AssumeDepart;
			parameters[13].Value = model.AssumeUser;
			parameters[14].Value = model.CooperDepart;
			parameters[15].Value = model.CooperUser;
			parameters[16].Value = model.WorkStartDate;

			DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_WorkContact_Update",parameters,out rowsAffected);
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

			DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_WorkContact_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_WorkContact GetModel(int InfoId)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.Int,4)};
			parameters[0].Value = InfoId;

			MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_WorkContact model=new MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_WorkContact();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_WorkContact_GetModel",parameters,"ds");
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
				model.UserCode=ds.Tables[0].Rows[0]["UserCode"].ToString();
				model.Title=ds.Tables[0].Rows[0]["Title"].ToString();
				model.WorkSper=ds.Tables[0].Rows[0]["WorkSper"].ToString();
				model.Ask=ds.Tables[0].Rows[0]["Ask"].ToString();
				if(ds.Tables[0].Rows[0]["ConferenceEndDate"].ToString()!="")
				{
					model.ConferenceEndDate=DateTime.Parse(ds.Tables[0].Rows[0]["ConferenceEndDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["WorkEndDate"].ToString()!="")
				{
					model.WorkEndDate=DateTime.Parse(ds.Tables[0].Rows[0]["WorkEndDate"].ToString());
				}
				model.ContactImportant=ds.Tables[0].Rows[0]["ContactImportant"].ToString();
				model.InfoContent=ds.Tables[0].Rows[0]["InfoContent"].ToString();
				model.DocumentUrl=ds.Tables[0].Rows[0]["DocumentUrl"].ToString();
				if(ds.Tables[0].Rows[0]["ArrangeDate"].ToString()!="")
				{
					model.ArrangeDate=DateTime.Parse(ds.Tables[0].Rows[0]["ArrangeDate"].ToString());
				}
				model.AssumeDepart=ds.Tables[0].Rows[0]["AssumeDepart"].ToString();
				model.AssumeUser=ds.Tables[0].Rows[0]["AssumeUser"].ToString();
				model.CooperDepart=ds.Tables[0].Rows[0]["CooperDepart"].ToString();
				model.CooperUser=ds.Tables[0].Rows[0]["CooperUser"].ToString();
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
			strSql.Append("select InfoId,Tb_WorkFlow_FlowSort_InfoId,UserCode,Title,WorkSper,Ask,ConferenceEndDate,WorkEndDate,ContactImportant,InfoContent,DocumentUrl,ArrangeDate,AssumeDepart,AssumeUser,CooperDepart,CooperUser,WorkStartDate ");
			strSql.Append(" FROM Tb_OAPublicWork_WorkContact ");
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
			strSql.Append(" InfoId,Tb_WorkFlow_FlowSort_InfoId,UserCode,Title,WorkSper,Ask,ConferenceEndDate,WorkEndDate,ContactImportant,InfoContent,DocumentUrl,ArrangeDate,AssumeDepart,AssumeUser,CooperDepart,CooperUser,WorkStartDate ");
			strSql.Append(" FROM Tb_OAPublicWork_WorkContact ");
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
			parameters[5].Value = "SELECT * FROM Tb_OAPublicWork_WorkContact WHERE 1=1 " + StrCondition;
			parameters[6].Value = "InfoId";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

