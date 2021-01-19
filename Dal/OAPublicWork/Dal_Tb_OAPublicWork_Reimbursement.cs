using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.OAPublicWork
{
	/// <summary>
	/// 数据访问类Dal_Tb_OAPublicWork_Reimbursement。
	/// </summary>
	public class Dal_Tb_OAPublicWork_Reimbursement
	{
		public Dal_Tb_OAPublicWork_Reimbursement()
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

			int result= DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_Reimbursement_Exists",parameters,out rowsAffected);
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
		public int Add(MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_Reimbursement model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.Int,4),
					new SqlParameter("@Tb_WorkFlow_FlowSort_InfoId", SqlDbType.Int,4),
					new SqlParameter("@UserCode", SqlDbType.NVarChar,20),
					new SqlParameter("@Title", SqlDbType.VarChar,100),
					new SqlParameter("@ReimbursementDate", SqlDbType.DateTime),
					new SqlParameter("@InfoContent", SqlDbType.VarChar),
					new SqlParameter("@DocumentUrl", SqlDbType.VarChar,2000),
					new SqlParameter("@RealUserCode", SqlDbType.NVarChar,20),
					new SqlParameter("@RealMoney", SqlDbType.Decimal,9),
					new SqlParameter("@WorkStartDate", SqlDbType.DateTime),
					new SqlParameter("@IsPay", SqlDbType.NVarChar,50),
					new SqlParameter("@PrintCount", SqlDbType.Int,4)};
			parameters[0].Direction = ParameterDirection.Output;
			parameters[1].Value = model.Tb_WorkFlow_FlowSort_InfoId;
			parameters[2].Value = model.UserCode;
			parameters[3].Value = model.Title;
			parameters[4].Value = model.ReimbursementDate;
			parameters[5].Value = model.InfoContent;
			parameters[6].Value = model.DocumentUrl;
			parameters[7].Value = model.RealUserCode;
			parameters[8].Value = model.RealMoney;
			parameters[9].Value = model.WorkStartDate;
			parameters[10].Value = model.IsPay;
			parameters[11].Value = model.PrintCount;

			DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_Reimbursement_ADD",parameters,out rowsAffected);
			return (int)parameters[0].Value;
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_Reimbursement model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.Int,4),
					new SqlParameter("@Tb_WorkFlow_FlowSort_InfoId", SqlDbType.Int,4),
					new SqlParameter("@UserCode", SqlDbType.NVarChar,20),
					new SqlParameter("@Title", SqlDbType.VarChar,100),
					new SqlParameter("@ReimbursementDate", SqlDbType.DateTime),
					new SqlParameter("@InfoContent", SqlDbType.VarChar),
					new SqlParameter("@DocumentUrl", SqlDbType.VarChar,2000),
					new SqlParameter("@RealUserCode", SqlDbType.NVarChar,20),
					new SqlParameter("@RealMoney", SqlDbType.Decimal,9),
					new SqlParameter("@WorkStartDate", SqlDbType.DateTime),
					new SqlParameter("@IsPay", SqlDbType.NVarChar,50),
					new SqlParameter("@PrintCount", SqlDbType.Int,4)};
			parameters[0].Value = model.InfoId;
			parameters[1].Value = model.Tb_WorkFlow_FlowSort_InfoId;
			parameters[2].Value = model.UserCode;
			parameters[3].Value = model.Title;
			parameters[4].Value = model.ReimbursementDate;
			parameters[5].Value = model.InfoContent;
			parameters[6].Value = model.DocumentUrl;
			parameters[7].Value = model.RealUserCode;
			parameters[8].Value = model.RealMoney;
			parameters[9].Value = model.WorkStartDate;
			parameters[10].Value = model.IsPay;
			parameters[11].Value = model.PrintCount;

			DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_Reimbursement_Update",parameters,out rowsAffected);
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

			DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_Reimbursement_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_Reimbursement GetModel(int InfoId)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.Int,4)};
			parameters[0].Value = InfoId;

			MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_Reimbursement model=new MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_Reimbursement();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_Reimbursement_GetModel",parameters,"ds");
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
				if(ds.Tables[0].Rows[0]["ReimbursementDate"].ToString()!="")
				{
					model.ReimbursementDate=DateTime.Parse(ds.Tables[0].Rows[0]["ReimbursementDate"].ToString());
				}
				model.InfoContent=ds.Tables[0].Rows[0]["InfoContent"].ToString();
				model.DocumentUrl=ds.Tables[0].Rows[0]["DocumentUrl"].ToString();
				model.RealUserCode=ds.Tables[0].Rows[0]["RealUserCode"].ToString();
				if(ds.Tables[0].Rows[0]["RealMoney"].ToString()!="")
				{
					model.RealMoney=decimal.Parse(ds.Tables[0].Rows[0]["RealMoney"].ToString());
				}
				if(ds.Tables[0].Rows[0]["WorkStartDate"].ToString()!="")
				{
					model.WorkStartDate=DateTime.Parse(ds.Tables[0].Rows[0]["WorkStartDate"].ToString());
				}
				model.IsPay=ds.Tables[0].Rows[0]["IsPay"].ToString();
				if(ds.Tables[0].Rows[0]["PrintCount"].ToString()!="")
				{
					model.PrintCount=int.Parse(ds.Tables[0].Rows[0]["PrintCount"].ToString());
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
			strSql.Append("select InfoId,Tb_WorkFlow_FlowSort_InfoId,UserCode,Title,ReimbursementDate,InfoContent,DocumentUrl,RealUserCode,RealMoney,WorkStartDate,IsPay,PrintCount ");
			strSql.Append(" FROM Tb_OAPublicWork_Reimbursement ");
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
			strSql.Append(" InfoId,Tb_WorkFlow_FlowSort_InfoId,UserCode,Title,ReimbursementDate,InfoContent,DocumentUrl,RealUserCode,RealMoney,WorkStartDate,IsPay,PrintCount ");
			strSql.Append(" FROM Tb_OAPublicWork_Reimbursement ");
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
			parameters[5].Value = "SELECT * FROM Tb_OAPublicWork_Reimbursement WHERE 1=1 " + StrCondition;
			parameters[6].Value = "InfoId";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

