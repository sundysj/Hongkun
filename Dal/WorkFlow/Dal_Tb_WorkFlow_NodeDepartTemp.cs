using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.WorkFlow
{
	/// <summary>
	/// 数据访问类Dal_Tb_WorkFlow_NodeDepartTemp。
	/// </summary>
	public class Dal_Tb_WorkFlow_NodeDepartTemp
	{
		public Dal_Tb_WorkFlow_NodeDepartTemp()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法


		/// <summary>
		///  增加一条数据
		/// </summary>
		public void Add(MobileSoft.Model.WorkFlow.Tb_WorkFlow_NodeDepartTemp model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@Tb_WorkFlow_FlowNode_InfoId", SqlDbType.Int,4),
					new SqlParameter("@Tb_Sys_Department_DepCode", SqlDbType.NVarChar,20)};
			parameters[0].Value = model.Tb_WorkFlow_FlowNode_InfoId;
			parameters[1].Value = model.Tb_Sys_Department_DepCode;

			DbHelperSQL.RunProcedure("Proc_Tb_WorkFlow_NodeDepartTemp_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.WorkFlow.Tb_WorkFlow_NodeDepartTemp model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@Tb_WorkFlow_FlowNode_InfoId", SqlDbType.Int,4),
					new SqlParameter("@Tb_Sys_Department_DepCode", SqlDbType.NVarChar,20)};
			parameters[0].Value = model.Tb_WorkFlow_FlowNode_InfoId;
			parameters[1].Value = model.Tb_Sys_Department_DepCode;

			DbHelperSQL.RunProcedure("Proc_Tb_WorkFlow_NodeDepartTemp_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete()
		{
			int rowsAffected;
			SqlParameter[] parameters = {
};

			DbHelperSQL.RunProcedure("Proc_Tb_WorkFlow_NodeDepartTemp_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.WorkFlow.Tb_WorkFlow_NodeDepartTemp GetModel()
		{
			SqlParameter[] parameters = {
};

			MobileSoft.Model.WorkFlow.Tb_WorkFlow_NodeDepartTemp model=new MobileSoft.Model.WorkFlow.Tb_WorkFlow_NodeDepartTemp();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_WorkFlow_NodeDepartTemp_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["Tb_WorkFlow_FlowNode_InfoId"].ToString()!="")
				{
					model.Tb_WorkFlow_FlowNode_InfoId=int.Parse(ds.Tables[0].Rows[0]["Tb_WorkFlow_FlowNode_InfoId"].ToString());
				}
				model.Tb_Sys_Department_DepCode=ds.Tables[0].Rows[0]["Tb_Sys_Department_DepCode"].ToString();
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
			strSql.Append("select Tb_WorkFlow_FlowNode_InfoId,Tb_Sys_Department_DepCode ");
			strSql.Append(" FROM Tb_WorkFlow_NodeDepartTemp ");
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
			strSql.Append(" Tb_WorkFlow_FlowNode_InfoId,Tb_Sys_Department_DepCode ");
			strSql.Append(" FROM Tb_WorkFlow_NodeDepartTemp ");
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
			parameters[5].Value = "SELECT * FROM Tb_WorkFlow_NodeDepartTemp WHERE 1=1 " + StrCondition;
			parameters[6].Value = "";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

