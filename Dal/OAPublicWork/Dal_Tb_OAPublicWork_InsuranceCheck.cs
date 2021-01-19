using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.OAPublicWork
{
	/// <summary>
	/// 数据访问类Dal_Tb_OAPublicWork_InsuranceCheck。
	/// </summary>
	public class Dal_Tb_OAPublicWork_InsuranceCheck
	{
		public Dal_Tb_OAPublicWork_InsuranceCheck()
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

			int result= DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_InsuranceCheck_Exists",parameters,out rowsAffected);
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
		public int Add(MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_InsuranceCheck model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.Int,4),
					new SqlParameter("@Tb_WorkFlow_FlowSort_InfoId", SqlDbType.Int,4),
					new SqlParameter("@UserCode", SqlDbType.NVarChar,20),
					new SqlParameter("@Title", SqlDbType.VarChar,100),
					new SqlParameter("@AgentUserCode", SqlDbType.NVarChar,20),
					new SqlParameter("@HandleContent", SqlDbType.Text),
					new SqlParameter("@HandleDate", SqlDbType.DateTime),
					new SqlParameter("@DocumentUrl", SqlDbType.NVarChar,2000),
					new SqlParameter("@WorkStartDate", SqlDbType.DateTime),
					new SqlParameter("@BeforeClassRole", SqlDbType.NVarChar,50),
					new SqlParameter("@AfterClassRole", SqlDbType.NVarChar,50),
					new SqlParameter("@NatureList", SqlDbType.VarChar,50),
					new SqlParameter("@LeaveDays", SqlDbType.VarChar,20),
					new SqlParameter("@StartDate", SqlDbType.VarChar,20),
					new SqlParameter("@EndDate", SqlDbType.VarChar,20)};
			parameters[0].Direction = ParameterDirection.Output;
			parameters[1].Value = model.Tb_WorkFlow_FlowSort_InfoId;
			parameters[2].Value = model.UserCode;
			parameters[3].Value = model.Title;
			parameters[4].Value = model.AgentUserCode;
			parameters[5].Value = model.HandleContent;
			parameters[6].Value = model.HandleDate;
			parameters[7].Value = model.DocumentUrl;
			parameters[8].Value = model.WorkStartDate;
			parameters[9].Value = model.BeforeClassRole;
			parameters[10].Value = model.AfterClassRole;
			parameters[11].Value = model.NatureList;
			parameters[12].Value = model.LeaveDays;
			parameters[13].Value = model.StartDate;
			parameters[14].Value = model.EndDate;

			DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_InsuranceCheck_ADD",parameters,out rowsAffected);
			return (int)parameters[0].Value;
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_InsuranceCheck model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.Int,4),
					new SqlParameter("@Tb_WorkFlow_FlowSort_InfoId", SqlDbType.Int,4),
					new SqlParameter("@UserCode", SqlDbType.NVarChar,20),
					new SqlParameter("@Title", SqlDbType.VarChar,100),
					new SqlParameter("@AgentUserCode", SqlDbType.NVarChar,20),
					new SqlParameter("@HandleContent", SqlDbType.Text),
					new SqlParameter("@HandleDate", SqlDbType.DateTime),
					new SqlParameter("@DocumentUrl", SqlDbType.NVarChar,2000),
					new SqlParameter("@WorkStartDate", SqlDbType.DateTime),
					new SqlParameter("@BeforeClassRole", SqlDbType.NVarChar,50),
					new SqlParameter("@AfterClassRole", SqlDbType.NVarChar,50),
					new SqlParameter("@NatureList", SqlDbType.VarChar,50),
					new SqlParameter("@LeaveDays", SqlDbType.VarChar,20),
					new SqlParameter("@StartDate", SqlDbType.VarChar,20),
					new SqlParameter("@EndDate", SqlDbType.VarChar,20)};
			parameters[0].Value = model.InfoId;
			parameters[1].Value = model.Tb_WorkFlow_FlowSort_InfoId;
			parameters[2].Value = model.UserCode;
			parameters[3].Value = model.Title;
			parameters[4].Value = model.AgentUserCode;
			parameters[5].Value = model.HandleContent;
			parameters[6].Value = model.HandleDate;
			parameters[7].Value = model.DocumentUrl;
			parameters[8].Value = model.WorkStartDate;
			parameters[9].Value = model.BeforeClassRole;
			parameters[10].Value = model.AfterClassRole;
			parameters[11].Value = model.NatureList;
			parameters[12].Value = model.LeaveDays;
			parameters[13].Value = model.StartDate;
			parameters[14].Value = model.EndDate;

			DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_InsuranceCheck_Update",parameters,out rowsAffected);
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

			DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_InsuranceCheck_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_InsuranceCheck GetModel(int InfoId)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.Int,4)};
			parameters[0].Value = InfoId;

			MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_InsuranceCheck model=new MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_InsuranceCheck();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_InsuranceCheck_GetModel",parameters,"ds");
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
				model.AgentUserCode=ds.Tables[0].Rows[0]["AgentUserCode"].ToString();
				model.HandleContent=ds.Tables[0].Rows[0]["HandleContent"].ToString();
				if(ds.Tables[0].Rows[0]["HandleDate"].ToString()!="")
				{
					model.HandleDate=DateTime.Parse(ds.Tables[0].Rows[0]["HandleDate"].ToString());
				}
				model.DocumentUrl=ds.Tables[0].Rows[0]["DocumentUrl"].ToString();
				if(ds.Tables[0].Rows[0]["WorkStartDate"].ToString()!="")
				{
					model.WorkStartDate=DateTime.Parse(ds.Tables[0].Rows[0]["WorkStartDate"].ToString());
				}
				model.BeforeClassRole=ds.Tables[0].Rows[0]["BeforeClassRole"].ToString();
				model.AfterClassRole=ds.Tables[0].Rows[0]["AfterClassRole"].ToString();
				model.NatureList=ds.Tables[0].Rows[0]["NatureList"].ToString();
				model.LeaveDays=ds.Tables[0].Rows[0]["LeaveDays"].ToString();
				model.StartDate=ds.Tables[0].Rows[0]["StartDate"].ToString();
				model.EndDate=ds.Tables[0].Rows[0]["EndDate"].ToString();
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
			strSql.Append("select InfoId,Tb_WorkFlow_FlowSort_InfoId,UserCode,Title,AgentUserCode,HandleContent,HandleDate,DocumentUrl,WorkStartDate,BeforeClassRole,AfterClassRole,NatureList,LeaveDays,StartDate,EndDate ");
			strSql.Append(" FROM Tb_OAPublicWork_InsuranceCheck ");
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
			strSql.Append(" InfoId,Tb_WorkFlow_FlowSort_InfoId,UserCode,Title,AgentUserCode,HandleContent,HandleDate,DocumentUrl,WorkStartDate,BeforeClassRole,AfterClassRole,NatureList,LeaveDays,StartDate,EndDate ");
			strSql.Append(" FROM Tb_OAPublicWork_InsuranceCheck ");
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
			parameters[5].Value = "SELECT * FROM Tb_OAPublicWork_InsuranceCheck WHERE 1=1 " + StrCondition;
			parameters[6].Value = "InfoId";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

