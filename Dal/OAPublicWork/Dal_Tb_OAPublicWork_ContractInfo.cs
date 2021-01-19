using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.OAPublicWork
{
	/// <summary>
	/// 数据访问类Dal_Tb_OAPublicWork_ContractInfo。
	/// </summary>
	public class Dal_Tb_OAPublicWork_ContractInfo
	{
		public Dal_Tb_OAPublicWork_ContractInfo()
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

			int result= DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_ContractInfo_Exists",parameters,out rowsAffected);
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
		public int Add(MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_ContractInfo model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.Int,4),
					new SqlParameter("@Tb_WorkFlow_FlowSort_InfoId", SqlDbType.Int,4),
					new SqlParameter("@UserCode", SqlDbType.NVarChar,20),
					new SqlParameter("@ContractCode", SqlDbType.VarChar,100),
					new SqlParameter("@ContractName", SqlDbType.VarChar,100),
					new SqlParameter("@CompanyName", SqlDbType.VarChar,100),
					new SqlParameter("@ContractMoney", SqlDbType.NVarChar,50),
					new SqlParameter("@Writer", SqlDbType.VarChar,20),
					new SqlParameter("@WriteDate", SqlDbType.DateTime),
					new SqlParameter("@OverDate", SqlDbType.DateTime),
					new SqlParameter("@ContractDate", SqlDbType.DateTime),
					new SqlParameter("@InfoContent", SqlDbType.Text),
					new SqlParameter("@DocumentUrl", SqlDbType.VarChar,2000),
					new SqlParameter("@WorkStartDate", SqlDbType.DateTime)};
			parameters[0].Direction = ParameterDirection.Output;
			parameters[1].Value = model.Tb_WorkFlow_FlowSort_InfoId;
			parameters[2].Value = model.UserCode;
			parameters[3].Value = model.ContractCode;
			parameters[4].Value = model.ContractName;
			parameters[5].Value = model.CompanyName;
			parameters[6].Value = model.ContractMoney;
			parameters[7].Value = model.Writer;
			parameters[8].Value = model.WriteDate;
			parameters[9].Value = model.OverDate;
			parameters[10].Value = model.ContractDate;
			parameters[11].Value = model.InfoContent;
			parameters[12].Value = model.DocumentUrl;
			parameters[13].Value = model.WorkStartDate;

			DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_ContractInfo_ADD",parameters,out rowsAffected);
			return (int)parameters[0].Value;
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_ContractInfo model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.Int,4),
					new SqlParameter("@Tb_WorkFlow_FlowSort_InfoId", SqlDbType.Int,4),
					new SqlParameter("@UserCode", SqlDbType.NVarChar,20),
					new SqlParameter("@ContractCode", SqlDbType.VarChar,100),
					new SqlParameter("@ContractName", SqlDbType.VarChar,100),
					new SqlParameter("@CompanyName", SqlDbType.VarChar,100),
					new SqlParameter("@ContractMoney", SqlDbType.NVarChar,50),
					new SqlParameter("@Writer", SqlDbType.VarChar,20),
					new SqlParameter("@WriteDate", SqlDbType.DateTime),
					new SqlParameter("@OverDate", SqlDbType.DateTime),
					new SqlParameter("@ContractDate", SqlDbType.DateTime),
					new SqlParameter("@InfoContent", SqlDbType.Text),
					new SqlParameter("@DocumentUrl", SqlDbType.VarChar,2000),
					new SqlParameter("@WorkStartDate", SqlDbType.DateTime)};
			parameters[0].Value = model.InfoId;
			parameters[1].Value = model.Tb_WorkFlow_FlowSort_InfoId;
			parameters[2].Value = model.UserCode;
			parameters[3].Value = model.ContractCode;
			parameters[4].Value = model.ContractName;
			parameters[5].Value = model.CompanyName;
			parameters[6].Value = model.ContractMoney;
			parameters[7].Value = model.Writer;
			parameters[8].Value = model.WriteDate;
			parameters[9].Value = model.OverDate;
			parameters[10].Value = model.ContractDate;
			parameters[11].Value = model.InfoContent;
			parameters[12].Value = model.DocumentUrl;
			parameters[13].Value = model.WorkStartDate;

			DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_ContractInfo_Update",parameters,out rowsAffected);
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

			DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_ContractInfo_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_ContractInfo GetModel(int InfoId)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.Int,4)};
			parameters[0].Value = InfoId;

			MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_ContractInfo model=new MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_ContractInfo();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_ContractInfo_GetModel",parameters,"ds");
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
				model.ContractCode=ds.Tables[0].Rows[0]["ContractCode"].ToString();
				model.ContractName=ds.Tables[0].Rows[0]["ContractName"].ToString();
				model.CompanyName=ds.Tables[0].Rows[0]["CompanyName"].ToString();
				model.ContractMoney=ds.Tables[0].Rows[0]["ContractMoney"].ToString();
				model.Writer=ds.Tables[0].Rows[0]["Writer"].ToString();
				if(ds.Tables[0].Rows[0]["WriteDate"].ToString()!="")
				{
					model.WriteDate=DateTime.Parse(ds.Tables[0].Rows[0]["WriteDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["OverDate"].ToString()!="")
				{
					model.OverDate=DateTime.Parse(ds.Tables[0].Rows[0]["OverDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ContractDate"].ToString()!="")
				{
					model.ContractDate=DateTime.Parse(ds.Tables[0].Rows[0]["ContractDate"].ToString());
				}
				model.InfoContent=ds.Tables[0].Rows[0]["InfoContent"].ToString();
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
			strSql.Append("select InfoId,Tb_WorkFlow_FlowSort_InfoId,UserCode,ContractCode,ContractName,CompanyName,ContractMoney,Writer,WriteDate,OverDate,ContractDate,InfoContent,DocumentUrl,WorkStartDate ");
			strSql.Append(" FROM Tb_OAPublicWork_ContractInfo ");
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
			strSql.Append(" InfoId,Tb_WorkFlow_FlowSort_InfoId,UserCode,ContractCode,ContractName,CompanyName,ContractMoney,Writer,WriteDate,OverDate,ContractDate,InfoContent,DocumentUrl,WorkStartDate ");
			strSql.Append(" FROM Tb_OAPublicWork_ContractInfo ");
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
			parameters[5].Value = "SELECT * FROM Tb_OAPublicWork_ContractInfo WHERE 1=1 " + StrCondition;
			parameters[6].Value = "InfoId";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

