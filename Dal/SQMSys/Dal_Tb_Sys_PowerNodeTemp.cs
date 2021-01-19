using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.SQMSys
{
	/// <summary>
	/// 数据访问类Dal_Tb_Sys_PowerNodeTemp。
	/// </summary>
	public class Dal_Tb_Sys_PowerNodeTemp
	{
		public Dal_Tb_Sys_PowerNodeTemp()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法


		/// <summary>
		///  增加一条数据
		/// </summary>
		public void Add(MobileSoft.Model.SQMSys.Tb_Sys_PowerNodeTemp model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@UserRoles", SqlDbType.NVarChar,400),
					new SqlParameter("@PNodeCode", SqlDbType.NVarChar,20),
					new SqlParameter("@PNodeName", SqlDbType.NVarChar,50),
					new SqlParameter("@URLPage", SqlDbType.NVarChar,100),
					new SqlParameter("@URLTarget", SqlDbType.NVarChar,20),
					new SqlParameter("@BackTitleImg", SqlDbType.NVarChar,100),
					new SqlParameter("@Narrate", SqlDbType.NText),
					new SqlParameter("@InPopedom", SqlDbType.SmallInt,2),
					new SqlParameter("@Functions", SqlDbType.NVarChar,200),
					new SqlParameter("@NodeType", SqlDbType.SmallInt,2),
					new SqlParameter("@PNodeSort", SqlDbType.SmallInt,2),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2),
					new SqlParameter("@PrentPNodeCode", SqlDbType.NVarChar,20)};
			parameters[0].Value = model.UserRoles;
			parameters[1].Value = model.PNodeCode;
			parameters[2].Value = model.PNodeName;
			parameters[3].Value = model.URLPage;
			parameters[4].Value = model.URLTarget;
			parameters[5].Value = model.BackTitleImg;
			parameters[6].Value = model.Narrate;
			parameters[7].Value = model.InPopedom;
			parameters[8].Value = model.Functions;
			parameters[9].Value = model.NodeType;
			parameters[10].Value = model.PNodeSort;
			parameters[11].Value = model.IsDelete;
			parameters[12].Value = model.PrentPNodeCode;

			DbHelperSQL.RunProcedure("Proc_Tb_Sys_PowerNodeTemp_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.SQMSys.Tb_Sys_PowerNodeTemp model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@UserRoles", SqlDbType.NVarChar,400),
					new SqlParameter("@PNodeCode", SqlDbType.NVarChar,20),
					new SqlParameter("@PNodeName", SqlDbType.NVarChar,50),
					new SqlParameter("@URLPage", SqlDbType.NVarChar,100),
					new SqlParameter("@URLTarget", SqlDbType.NVarChar,20),
					new SqlParameter("@BackTitleImg", SqlDbType.NVarChar,100),
					new SqlParameter("@Narrate", SqlDbType.NText),
					new SqlParameter("@InPopedom", SqlDbType.SmallInt,2),
					new SqlParameter("@Functions", SqlDbType.NVarChar,200),
					new SqlParameter("@NodeType", SqlDbType.SmallInt,2),
					new SqlParameter("@PNodeSort", SqlDbType.SmallInt,2),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2),
					new SqlParameter("@PrentPNodeCode", SqlDbType.NVarChar,20)};
			parameters[0].Value = model.UserRoles;
			parameters[1].Value = model.PNodeCode;
			parameters[2].Value = model.PNodeName;
			parameters[3].Value = model.URLPage;
			parameters[4].Value = model.URLTarget;
			parameters[5].Value = model.BackTitleImg;
			parameters[6].Value = model.Narrate;
			parameters[7].Value = model.InPopedom;
			parameters[8].Value = model.Functions;
			parameters[9].Value = model.NodeType;
			parameters[10].Value = model.PNodeSort;
			parameters[11].Value = model.IsDelete;
			parameters[12].Value = model.PrentPNodeCode;

			DbHelperSQL.RunProcedure("Proc_Tb_Sys_PowerNodeTemp_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete()
		{
			int rowsAffected;
			SqlParameter[] parameters = {
};

			DbHelperSQL.RunProcedure("Proc_Tb_Sys_PowerNodeTemp_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.SQMSys.Tb_Sys_PowerNodeTemp GetModel()
		{
			SqlParameter[] parameters = {
};

			MobileSoft.Model.SQMSys.Tb_Sys_PowerNodeTemp model=new MobileSoft.Model.SQMSys.Tb_Sys_PowerNodeTemp();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_Sys_PowerNodeTemp_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				model.UserRoles=ds.Tables[0].Rows[0]["UserRoles"].ToString();
				model.PNodeCode=ds.Tables[0].Rows[0]["PNodeCode"].ToString();
				model.PNodeName=ds.Tables[0].Rows[0]["PNodeName"].ToString();
				model.URLPage=ds.Tables[0].Rows[0]["URLPage"].ToString();
				model.URLTarget=ds.Tables[0].Rows[0]["URLTarget"].ToString();
				model.BackTitleImg=ds.Tables[0].Rows[0]["BackTitleImg"].ToString();
				model.Narrate=ds.Tables[0].Rows[0]["Narrate"].ToString();
				if(ds.Tables[0].Rows[0]["InPopedom"].ToString()!="")
				{
					model.InPopedom=int.Parse(ds.Tables[0].Rows[0]["InPopedom"].ToString());
				}
				model.Functions=ds.Tables[0].Rows[0]["Functions"].ToString();
				if(ds.Tables[0].Rows[0]["NodeType"].ToString()!="")
				{
					model.NodeType=int.Parse(ds.Tables[0].Rows[0]["NodeType"].ToString());
				}
				if(ds.Tables[0].Rows[0]["PNodeSort"].ToString()!="")
				{
					model.PNodeSort=int.Parse(ds.Tables[0].Rows[0]["PNodeSort"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsDelete"].ToString()!="")
				{
					model.IsDelete=int.Parse(ds.Tables[0].Rows[0]["IsDelete"].ToString());
				}
				model.PrentPNodeCode=ds.Tables[0].Rows[0]["PrentPNodeCode"].ToString();
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
			strSql.Append("select UserRoles,PNodeCode,PNodeName,URLPage,URLTarget,BackTitleImg,Narrate,InPopedom,Functions,NodeType,PNodeSort,IsDelete,PrentPNodeCode ");
			strSql.Append(" FROM Tb_Sys_PowerNodeTemp ");
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
			strSql.Append(" UserRoles,PNodeCode,PNodeName,URLPage,URLTarget,BackTitleImg,Narrate,InPopedom,Functions,NodeType,PNodeSort,IsDelete,PrentPNodeCode ");
			strSql.Append(" FROM Tb_Sys_PowerNodeTemp ");
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
			parameters[5].Value = "SELECT * FROM Tb_Sys_PowerNodeTemp WHERE 1=1 " + StrCondition;
			parameters[6].Value = "";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

