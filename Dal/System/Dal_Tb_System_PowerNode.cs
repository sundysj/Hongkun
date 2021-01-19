using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.System
{
	/// <summary>
	/// 数据访问类Dal_Tb_System_PowerNode。
	/// </summary>
	public class Dal_Tb_System_PowerNode
	{
		public Dal_Tb_System_PowerNode()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string PNodeCode)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@PNodeCode", SqlDbType.NVarChar,50)};
			parameters[0].Value = PNodeCode;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_System_PowerNode_Exists",parameters,out rowsAffected);
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
		public void Add(MobileSoft.Model.System.Tb_System_PowerNode model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
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
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2)};
			parameters[0].Value = model.PNodeCode;
			parameters[1].Value = model.PNodeName;
			parameters[2].Value = model.URLPage;
			parameters[3].Value = model.URLTarget;
			parameters[4].Value = model.BackTitleImg;
			parameters[5].Value = model.Narrate;
			parameters[6].Value = model.InPopedom;
			parameters[7].Value = model.Functions;
			parameters[8].Value = model.NodeType;
			parameters[9].Value = model.PNodeSort;
			parameters[10].Value = model.IsDelete;

			DbHelperSQL.RunProcedure("Proc_Tb_System_PowerNode_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.System.Tb_System_PowerNode model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
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
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2)};
			parameters[0].Value = model.PNodeCode;
			parameters[1].Value = model.PNodeName;
			parameters[2].Value = model.URLPage;
			parameters[3].Value = model.URLTarget;
			parameters[4].Value = model.BackTitleImg;
			parameters[5].Value = model.Narrate;
			parameters[6].Value = model.InPopedom;
			parameters[7].Value = model.Functions;
			parameters[8].Value = model.NodeType;
			parameters[9].Value = model.PNodeSort;
			parameters[10].Value = model.IsDelete;

			DbHelperSQL.RunProcedure("Proc_Tb_System_PowerNode_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(string PNodeCode)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@PNodeCode", SqlDbType.NVarChar,50)};
			parameters[0].Value = PNodeCode;

			DbHelperSQL.RunProcedure("Proc_Tb_System_PowerNode_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.System.Tb_System_PowerNode GetModel(string PNodeCode)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@PNodeCode", SqlDbType.NVarChar,50)};
			parameters[0].Value = PNodeCode;

			MobileSoft.Model.System.Tb_System_PowerNode model=new MobileSoft.Model.System.Tb_System_PowerNode();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_System_PowerNode_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
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
			strSql.Append("select PNodeCode,PNodeName,URLPage,URLTarget,BackTitleImg,Narrate,InPopedom,Functions,NodeType,PNodeSort,IsDelete ");
			strSql.Append(" FROM Tb_System_PowerNode ");
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
			strSql.Append(" PNodeCode,PNodeName,URLPage,URLTarget,BackTitleImg,Narrate,InPopedom,Functions,NodeType,PNodeSort,IsDelete ");
			strSql.Append(" FROM Tb_System_PowerNode ");
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
			parameters[5].Value = "SELECT * FROM Tb_System_PowerNode WHERE 1=1 " + StrCondition;
			parameters[6].Value = "PNodeCode";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

