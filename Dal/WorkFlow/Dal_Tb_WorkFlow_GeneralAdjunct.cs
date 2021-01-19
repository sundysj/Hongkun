using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.WorkFlow
{
	/// <summary>
	/// 数据访问类Dal_Tb_WorkFlow_GeneralAdjunct。
	/// </summary>
	public class Dal_Tb_WorkFlow_GeneralAdjunct
	{
		public Dal_Tb_WorkFlow_GeneralAdjunct()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法


		/// <summary>
		///  增加一条数据
		/// </summary>
		public void Add(MobileSoft.Model.WorkFlow.Tb_WorkFlow_GeneralAdjunct model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@GeneralAdjunctCode", SqlDbType.NVarChar,20),
					new SqlParameter("@GeneralCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@AdjunctName", SqlDbType.NVarChar,100),
					new SqlParameter("@FilPath", SqlDbType.NVarChar,300),
					new SqlParameter("@FileExName", SqlDbType.NVarChar,10),
					new SqlParameter("@FileSize", SqlDbType.Decimal,9),
					new SqlParameter("@msrepl_tran_version", SqlDbType.UniqueIdentifier,16)};
			parameters[0].Value = model.GeneralAdjunctCode;
			parameters[1].Value = model.GeneralCode;
			parameters[2].Value = model.AdjunctName;
			parameters[3].Value = model.FilPath;
			parameters[4].Value = model.FileExName;
			parameters[5].Value = model.FileSize;
			parameters[6].Value = model.msrepl_tran_version;

			DbHelperSQL.RunProcedure("Proc_Tb_WorkFlow_GeneralAdjunct_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.WorkFlow.Tb_WorkFlow_GeneralAdjunct model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@GeneralAdjunctCode", SqlDbType.NVarChar,20),
					new SqlParameter("@GeneralCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@AdjunctName", SqlDbType.NVarChar,100),
					new SqlParameter("@FilPath", SqlDbType.NVarChar,300),
					new SqlParameter("@FileExName", SqlDbType.NVarChar,10),
					new SqlParameter("@FileSize", SqlDbType.Decimal,9),
					new SqlParameter("@msrepl_tran_version", SqlDbType.UniqueIdentifier,16)};
			parameters[0].Value = model.GeneralAdjunctCode;
			parameters[1].Value = model.GeneralCode;
			parameters[2].Value = model.AdjunctName;
			parameters[3].Value = model.FilPath;
			parameters[4].Value = model.FileExName;
			parameters[5].Value = model.FileSize;
			parameters[6].Value = model.msrepl_tran_version;

			DbHelperSQL.RunProcedure("Proc_Tb_WorkFlow_GeneralAdjunct_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete()
		{
			int rowsAffected;
			SqlParameter[] parameters = {
};

			DbHelperSQL.RunProcedure("Proc_Tb_WorkFlow_GeneralAdjunct_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.WorkFlow.Tb_WorkFlow_GeneralAdjunct GetModel()
		{
			SqlParameter[] parameters = {
};

			MobileSoft.Model.WorkFlow.Tb_WorkFlow_GeneralAdjunct model=new MobileSoft.Model.WorkFlow.Tb_WorkFlow_GeneralAdjunct();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_WorkFlow_GeneralAdjunct_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				model.GeneralAdjunctCode=ds.Tables[0].Rows[0]["GeneralAdjunctCode"].ToString();
				if(ds.Tables[0].Rows[0]["GeneralCode"].ToString()!="")
				{
					model.GeneralCode=new Guid(ds.Tables[0].Rows[0]["GeneralCode"].ToString());
				}
				model.AdjunctName=ds.Tables[0].Rows[0]["AdjunctName"].ToString();
				model.FilPath=ds.Tables[0].Rows[0]["FilPath"].ToString();
				model.FileExName=ds.Tables[0].Rows[0]["FileExName"].ToString();
				if(ds.Tables[0].Rows[0]["FileSize"].ToString()!="")
				{
					model.FileSize=decimal.Parse(ds.Tables[0].Rows[0]["FileSize"].ToString());
				}
				if(ds.Tables[0].Rows[0]["msrepl_tran_version"].ToString()!="")
				{
					model.msrepl_tran_version=new Guid(ds.Tables[0].Rows[0]["msrepl_tran_version"].ToString());
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
			strSql.Append("select GeneralAdjunctCode,GeneralCode,AdjunctName,FilPath,FileExName,FileSize,msrepl_tran_version ");
			strSql.Append(" FROM Tb_WorkFlow_GeneralAdjunct ");
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
			strSql.Append(" GeneralAdjunctCode,GeneralCode,AdjunctName,FilPath,FileExName,FileSize,msrepl_tran_version ");
			strSql.Append(" FROM Tb_WorkFlow_GeneralAdjunct ");
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
			parameters[5].Value = "SELECT * FROM Tb_WorkFlow_GeneralAdjunct WHERE 1=1 " + StrCondition;
			parameters[6].Value = "";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

