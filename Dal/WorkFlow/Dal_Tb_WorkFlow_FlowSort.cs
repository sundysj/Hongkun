using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.WorkFlow
{
	/// <summary>
	/// 数据访问类Dal_Tb_WorkFlow_FlowSort。
	/// </summary>
	public class Dal_Tb_WorkFlow_FlowSort
	{
		public Dal_Tb_WorkFlow_FlowSort()
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

			int result= DbHelperSQL.RunProcedure("Proc_Tb_WorkFlow_FlowSort_Exists",parameters,out rowsAffected);
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
		public int Add(MobileSoft.Model.WorkFlow.Tb_WorkFlow_FlowSort model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.Int,4),
					new SqlParameter("@Pid", SqlDbType.Int,4),
					new SqlParameter("@FlowSortName", SqlDbType.VarChar,1000),
					new SqlParameter("@IsUpdate", SqlDbType.Int,4),
					new SqlParameter("@IsFlow", SqlDbType.Int,4),
					new SqlParameter("@DocumentUrl", SqlDbType.VarChar,1000),
					new SqlParameter("@SystemSign", SqlDbType.Int,4),
					new SqlParameter("@DirectionaryCode", SqlDbType.NVarChar,20),
					new SqlParameter("@Sort", SqlDbType.Int,4),
					new SqlParameter("@IsDelete", SqlDbType.Int,4),
					new SqlParameter("@UseStartDate", SqlDbType.NVarChar,50),
					new SqlParameter("@UseEndDate", SqlDbType.NVarChar,50),
					new SqlParameter("@UseUserList", SqlDbType.NVarChar,1000),
					new SqlParameter("@UseUserNameList", SqlDbType.NVarChar,1000)};
			parameters[0].Direction = ParameterDirection.Output;
			parameters[1].Value = model.Pid;
			parameters[2].Value = model.FlowSortName;
			parameters[3].Value = model.IsUpdate;
			parameters[4].Value = model.IsFlow;
			parameters[5].Value = model.DocumentUrl;
			parameters[6].Value = model.SystemSign;
			parameters[7].Value = model.DirectionaryCode;
			parameters[8].Value = model.Sort;
			parameters[9].Value = model.IsDelete;
			parameters[10].Value = model.UseStartDate;
			parameters[11].Value = model.UseEndDate;
			parameters[12].Value = model.UseUserList;
			parameters[13].Value = model.UseUserNameList;

			DbHelperSQL.RunProcedure("Proc_Tb_WorkFlow_FlowSort_ADD",parameters,out rowsAffected);
			return (int)parameters[0].Value;
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.WorkFlow.Tb_WorkFlow_FlowSort model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.Int,4),
					new SqlParameter("@Pid", SqlDbType.Int,4),
					new SqlParameter("@FlowSortName", SqlDbType.VarChar,1000),
					new SqlParameter("@IsUpdate", SqlDbType.Int,4),
					new SqlParameter("@IsFlow", SqlDbType.Int,4),
					new SqlParameter("@DocumentUrl", SqlDbType.VarChar,1000),
					new SqlParameter("@SystemSign", SqlDbType.Int,4),
					new SqlParameter("@DirectionaryCode", SqlDbType.NVarChar,20),
					new SqlParameter("@Sort", SqlDbType.Int,4),
					new SqlParameter("@IsDelete", SqlDbType.Int,4),
					new SqlParameter("@UseStartDate", SqlDbType.NVarChar,50),
					new SqlParameter("@UseEndDate", SqlDbType.NVarChar,50),
					new SqlParameter("@UseUserList", SqlDbType.NVarChar,1000),
					new SqlParameter("@UseUserNameList", SqlDbType.NVarChar,1000)};
			parameters[0].Value = model.InfoId;
			parameters[1].Value = model.Pid;
			parameters[2].Value = model.FlowSortName;
			parameters[3].Value = model.IsUpdate;
			parameters[4].Value = model.IsFlow;
			parameters[5].Value = model.DocumentUrl;
			parameters[6].Value = model.SystemSign;
			parameters[7].Value = model.DirectionaryCode;
			parameters[8].Value = model.Sort;
			parameters[9].Value = model.IsDelete;
			parameters[10].Value = model.UseStartDate;
			parameters[11].Value = model.UseEndDate;
			parameters[12].Value = model.UseUserList;
			parameters[13].Value = model.UseUserNameList;

			DbHelperSQL.RunProcedure("Proc_Tb_WorkFlow_FlowSort_Update",parameters,out rowsAffected);
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

			DbHelperSQL.RunProcedure("Proc_Tb_WorkFlow_FlowSort_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.WorkFlow.Tb_WorkFlow_FlowSort GetModel(int InfoId)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.Int,4)};
			parameters[0].Value = InfoId;

			MobileSoft.Model.WorkFlow.Tb_WorkFlow_FlowSort model=new MobileSoft.Model.WorkFlow.Tb_WorkFlow_FlowSort();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_WorkFlow_FlowSort_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["InfoId"].ToString()!="")
				{
					model.InfoId=int.Parse(ds.Tables[0].Rows[0]["InfoId"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Pid"].ToString()!="")
				{
					model.Pid=int.Parse(ds.Tables[0].Rows[0]["Pid"].ToString());
				}
				model.FlowSortName=ds.Tables[0].Rows[0]["FlowSortName"].ToString();
				if(ds.Tables[0].Rows[0]["IsUpdate"].ToString()!="")
				{
					model.IsUpdate=int.Parse(ds.Tables[0].Rows[0]["IsUpdate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsFlow"].ToString()!="")
				{
					model.IsFlow=int.Parse(ds.Tables[0].Rows[0]["IsFlow"].ToString());
				}
				model.DocumentUrl=ds.Tables[0].Rows[0]["DocumentUrl"].ToString();
				if(ds.Tables[0].Rows[0]["SystemSign"].ToString()!="")
				{
					model.SystemSign=int.Parse(ds.Tables[0].Rows[0]["SystemSign"].ToString());
				}
				model.DirectionaryCode=ds.Tables[0].Rows[0]["DirectionaryCode"].ToString();
				if(ds.Tables[0].Rows[0]["Sort"].ToString()!="")
				{
					model.Sort=int.Parse(ds.Tables[0].Rows[0]["Sort"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsDelete"].ToString()!="")
				{
					model.IsDelete=int.Parse(ds.Tables[0].Rows[0]["IsDelete"].ToString());
				}
				model.UseStartDate=ds.Tables[0].Rows[0]["UseStartDate"].ToString();
				model.UseEndDate=ds.Tables[0].Rows[0]["UseEndDate"].ToString();
				model.UseUserList=ds.Tables[0].Rows[0]["UseUserList"].ToString();
				model.UseUserNameList=ds.Tables[0].Rows[0]["UseUserNameList"].ToString();
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
			strSql.Append("select InfoId,Pid,FlowSortName,IsUpdate,IsFlow,DocumentUrl,SystemSign,DirectionaryCode,Sort,IsDelete,UseStartDate,UseEndDate,UseUserList,UseUserNameList ");
			strSql.Append(" FROM Tb_WorkFlow_FlowSort ");
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
			strSql.Append(" InfoId,Pid,FlowSortName,IsUpdate,IsFlow,DocumentUrl,SystemSign,DirectionaryCode,Sort,IsDelete,UseStartDate,UseEndDate,UseUserList,UseUserNameList ");
			strSql.Append(" FROM Tb_WorkFlow_FlowSort ");
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
			parameters[5].Value = "SELECT * FROM Tb_WorkFlow_FlowSort WHERE 1=1 " + StrCondition;
			parameters[6].Value = "InfoId";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

