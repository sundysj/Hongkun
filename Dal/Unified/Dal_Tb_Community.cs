using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.Unified
{
	/// <summary>
	/// 数据访问类Dal_Tb_Community。
	/// </summary>
	public class Dal_Tb_Community
	{
		public Dal_Tb_Community()
		{
			DbHelperSQL.ConnectionString = PubConstant.UnifiedContionString;
		}
		#region  成员方法

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string Id)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.VarChar,50)};
			parameters[0].Value = Id;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_Community_Exists",parameters,out rowsAffected);
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
		public void Add(MobileSoft.Model.Unified.Tb_Community model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.VarChar,36),
					new SqlParameter("@Province", SqlDbType.VarChar,36),
					new SqlParameter("@Area", SqlDbType.NChar,10),
					new SqlParameter("@City", SqlDbType.VarChar,36),
					new SqlParameter("@DBServer", SqlDbType.VarChar,36),
					new SqlParameter("@CorpID", SqlDbType.Int,4),
					new SqlParameter("@DBName", SqlDbType.VarChar,99),
					new SqlParameter("@DBUser", SqlDbType.VarChar,36),
					new SqlParameter("@DBPwd", SqlDbType.VarChar,36),
					new SqlParameter("@CommID", SqlDbType.VarChar,36),
					new SqlParameter("@CorpName", SqlDbType.NVarChar,36),
					new SqlParameter("@CommName", SqlDbType.NVarChar,50),
					new SqlParameter("@ModuleRights", SqlDbType.VarChar,3999),
                    new SqlParameter("@Tel", SqlDbType.VarChar,20)};
			parameters[0].Value = model.Id;
			parameters[1].Value = model.Province;
			parameters[2].Value = model.Area;
			parameters[3].Value = model.City;
			parameters[4].Value = model.DBServer;
			parameters[5].Value = model.CorpID;
			parameters[6].Value = model.DBName;
			parameters[7].Value = model.DBUser;
			parameters[8].Value = model.DBPwd;
			parameters[9].Value = model.CommID;
			parameters[10].Value = model.CorpName;
			parameters[11].Value = model.CommName;
			parameters[12].Value = model.ModuleRights;
            parameters[13].Value = model.Tel;
            DbHelperSQL.RunProcedure("Proc_Tb_Community_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.Unified.Tb_Community model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.VarChar,36),
					new SqlParameter("@Province", SqlDbType.VarChar,36),
					new SqlParameter("@Area", SqlDbType.NChar,10),
					new SqlParameter("@City", SqlDbType.VarChar,36),
					new SqlParameter("@DBServer", SqlDbType.VarChar,36),
					new SqlParameter("@CorpID", SqlDbType.Int,4),
					new SqlParameter("@DBName", SqlDbType.VarChar,99),
					new SqlParameter("@DBUser", SqlDbType.VarChar,36),
					new SqlParameter("@DBPwd", SqlDbType.VarChar,36),
					new SqlParameter("@CommID", SqlDbType.VarChar,36),
					new SqlParameter("@CorpName", SqlDbType.NVarChar,36),
					new SqlParameter("@CommName", SqlDbType.NVarChar,50),
					new SqlParameter("@ModuleRights", SqlDbType.VarChar,3999),
                    new SqlParameter("@Tel", SqlDbType.VarChar,20)
            };
			parameters[0].Value = model.Id;
			parameters[1].Value = model.Province;
			parameters[2].Value = model.Area;
			parameters[3].Value = model.City;
			parameters[4].Value = model.DBServer;
			parameters[5].Value = model.CorpID;
			parameters[6].Value = model.DBName;
			parameters[7].Value = model.DBUser;
			parameters[8].Value = model.DBPwd;
			parameters[9].Value = model.CommID;
			parameters[10].Value = model.CorpName;
			parameters[11].Value = model.CommName;
			parameters[12].Value = model.ModuleRights;
            parameters[13].Value = model.Tel;

            DbHelperSQL.RunProcedure("Proc_Tb_Community_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(string Id)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.VarChar,50)};
			parameters[0].Value = Id;

			DbHelperSQL.RunProcedure("Proc_Tb_Community_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.Unified.Tb_Community GetModel(string Id)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.VarChar,50)};
			parameters[0].Value = Id;

			MobileSoft.Model.Unified.Tb_Community model=new MobileSoft.Model.Unified.Tb_Community();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_Community_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				model.Id=ds.Tables[0].Rows[0]["Id"].ToString();
				model.Province=ds.Tables[0].Rows[0]["Province"].ToString();
				model.Area=ds.Tables[0].Rows[0]["Area"].ToString();
				model.City=ds.Tables[0].Rows[0]["City"].ToString();
				model.DBServer=ds.Tables[0].Rows[0]["DBServer"].ToString();
				if(ds.Tables[0].Rows[0]["CorpID"].ToString()!="")
				{
					model.CorpID=int.Parse(ds.Tables[0].Rows[0]["CorpID"].ToString());
				}
				model.DBName=ds.Tables[0].Rows[0]["DBName"].ToString();
				model.DBUser=ds.Tables[0].Rows[0]["DBUser"].ToString();
				model.DBPwd=ds.Tables[0].Rows[0]["DBPwd"].ToString();
				model.CommID=ds.Tables[0].Rows[0]["CommID"].ToString();
				model.CorpName=ds.Tables[0].Rows[0]["CorpName"].ToString();
				model.CommName=ds.Tables[0].Rows[0]["CommName"].ToString();
				model.ModuleRights=ds.Tables[0].Rows[0]["ModuleRights"].ToString();
                model.Tel = ds.Tables[0].Rows[0]["Tel"].ToString();
                if (ds.Tables[0].Columns.Contains("IsMultiDoorControlServer"))
                {
                    model.IsMultiDoorControlServer = (bool)ds.Tables[0].Rows[0]["IsMultiDoorControlServer"];
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
			strSql.Append("select Id,Province,Area,City,DBServer,CorpID,DBName,DBUser,DBPwd,CommID,CorpName,CommName,ModuleRights,Tel ");
			strSql.Append(" FROM Tb_Community ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" Id,Province,Area,City,DBServer,CorpID,DBName,DBUser,DBPwd,CommID,CorpName,CommName,ModuleRights,Tel ");
			strSql.Append(" FROM Tb_Community ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
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
			parameters[5].Value = "SELECT * FROM Tb_Community WHERE 1=1 " + StrCondition;
			parameters[6].Value = "Id";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

