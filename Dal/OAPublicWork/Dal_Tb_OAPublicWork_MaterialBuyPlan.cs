using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.OAPublicWork
{
	/// <summary>
	/// 数据访问类Dal_Tb_OAPublicWork_MaterialBuyPlan。
	/// </summary>
	public class Dal_Tb_OAPublicWork_MaterialBuyPlan
	{
		public Dal_Tb_OAPublicWork_MaterialBuyPlan()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("InfoId", "Tb_OAPublicWork_MaterialBuyPlan"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int InfoId)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.Int,4)};
			parameters[0].Value = InfoId;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_MaterialBuyPlan_Exists",parameters,out rowsAffected);
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
		public int Add(MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_MaterialBuyPlan model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.Int,4),
					new SqlParameter("@Tb_WorkFlow_FlowSort_InfoId", SqlDbType.Int,4),
					new SqlParameter("@UserCode", SqlDbType.NVarChar,20),
					new SqlParameter("@Title", SqlDbType.VarChar,100),
					new SqlParameter("@DepCode", SqlDbType.NVarChar,20),
					new SqlParameter("@BuyPlanDate", SqlDbType.DateTime),
					new SqlParameter("@BuyPlanMoney", SqlDbType.Decimal,5),
					new SqlParameter("@NeedDate", SqlDbType.DateTime),
					new SqlParameter("@BuyPlanReadMe", SqlDbType.NText),
					new SqlParameter("@DocumentUrl", SqlDbType.VarChar,2000),
					new SqlParameter("@WorkStartDate", SqlDbType.DateTime)};
			parameters[0].Direction = ParameterDirection.Output;
			parameters[1].Value = model.Tb_WorkFlow_FlowSort_InfoId;
			parameters[2].Value = model.UserCode;
			parameters[3].Value = model.Title;
			parameters[4].Value = model.DepCode;
			parameters[5].Value = model.BuyPlanDate;
			parameters[6].Value = model.BuyPlanMoney;
			parameters[7].Value = model.NeedDate;
			parameters[8].Value = model.BuyPlanReadMe;
			parameters[9].Value = model.DocumentUrl;
			parameters[10].Value = model.WorkStartDate;

			DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_MaterialBuyPlan_ADD",parameters,out rowsAffected);
			return (int)parameters[0].Value;
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_MaterialBuyPlan model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.Int,4),
					new SqlParameter("@Tb_WorkFlow_FlowSort_InfoId", SqlDbType.Int,4),
					new SqlParameter("@UserCode", SqlDbType.NVarChar,20),
					new SqlParameter("@Title", SqlDbType.VarChar,100),
					new SqlParameter("@DepCode", SqlDbType.NVarChar,20),
					new SqlParameter("@BuyPlanDate", SqlDbType.DateTime),
					new SqlParameter("@BuyPlanMoney", SqlDbType.Decimal,5),
					new SqlParameter("@NeedDate", SqlDbType.DateTime),
					new SqlParameter("@BuyPlanReadMe", SqlDbType.NText),
					new SqlParameter("@DocumentUrl", SqlDbType.VarChar,2000),
					new SqlParameter("@WorkStartDate", SqlDbType.DateTime)};
			parameters[0].Value = model.InfoId;
			parameters[1].Value = model.Tb_WorkFlow_FlowSort_InfoId;
			parameters[2].Value = model.UserCode;
			parameters[3].Value = model.Title;
			parameters[4].Value = model.DepCode;
			parameters[5].Value = model.BuyPlanDate;
			parameters[6].Value = model.BuyPlanMoney;
			parameters[7].Value = model.NeedDate;
			parameters[8].Value = model.BuyPlanReadMe;
			parameters[9].Value = model.DocumentUrl;
			parameters[10].Value = model.WorkStartDate;

			DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_MaterialBuyPlan_Update",parameters,out rowsAffected);
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

			DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_MaterialBuyPlan_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_MaterialBuyPlan GetModel(int InfoId)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.Int,4)};
			parameters[0].Value = InfoId;

			MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_MaterialBuyPlan model=new MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_MaterialBuyPlan();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_MaterialBuyPlan_GetModel",parameters,"ds");
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
				model.DepCode=ds.Tables[0].Rows[0]["DepCode"].ToString();
				if(ds.Tables[0].Rows[0]["BuyPlanDate"].ToString()!="")
				{
					model.BuyPlanDate=DateTime.Parse(ds.Tables[0].Rows[0]["BuyPlanDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["BuyPlanMoney"].ToString()!="")
				{
					model.BuyPlanMoney=decimal.Parse(ds.Tables[0].Rows[0]["BuyPlanMoney"].ToString());
				}
				if(ds.Tables[0].Rows[0]["NeedDate"].ToString()!="")
				{
					model.NeedDate=DateTime.Parse(ds.Tables[0].Rows[0]["NeedDate"].ToString());
				}
				model.BuyPlanReadMe=ds.Tables[0].Rows[0]["BuyPlanReadMe"].ToString();
				model.DocumentUrl=ds.Tables[0].Rows[0]["DocumentUrl"].ToString();
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
			strSql.Append("select InfoId,Tb_WorkFlow_FlowSort_InfoId,UserCode,Title,DepCode,BuyPlanDate,BuyPlanMoney,NeedDate,BuyPlanReadMe,DocumentUrl,WorkStartDate ");
			strSql.Append(" FROM Tb_OAPublicWork_MaterialBuyPlan ");
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
			strSql.Append(" InfoId,Tb_WorkFlow_FlowSort_InfoId,UserCode,Title,DepCode,BuyPlanDate,BuyPlanMoney,NeedDate,BuyPlanReadMe,DocumentUrl,WorkStartDate ");
			strSql.Append(" FROM Tb_OAPublicWork_MaterialBuyPlan ");
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
			parameters[5].Value = "SELECT * FROM Tb_OAPublicWork_MaterialBuyPlan WHERE 1=1 " + StrCondition;
			parameters[6].Value = "InfoId";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

