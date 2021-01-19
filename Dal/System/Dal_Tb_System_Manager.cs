using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.System
{
	/// <summary>
	/// 数据访问类Dal_Tb_System_Manager。
	/// </summary>
	public class Dal_Tb_System_Manager
	{
		public Dal_Tb_System_Manager()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法


		/// <summary>
		///  增加一条数据
		/// </summary>
		public void Add(MobileSoft.Model.System.Tb_System_Manager model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ManagerCode", SqlDbType.NVarChar,20),
					new SqlParameter("@ManagerType", SqlDbType.NVarChar,20),
					new SqlParameter("@ManagerName", SqlDbType.NVarChar,100),
					new SqlParameter("@LoginCode", SqlDbType.NVarChar,20),
					new SqlParameter("@LoginPassWD", SqlDbType.NVarChar,20),
					new SqlParameter("@IPAddress", SqlDbType.NVarChar,30),
					new SqlParameter("@MobileTel", SqlDbType.NVarChar,20),
					new SqlParameter("@EMail", SqlDbType.NVarChar,50),
					new SqlParameter("@Province", SqlDbType.NVarChar,20),
					new SqlParameter("@City", SqlDbType.NVarChar,20),
					new SqlParameter("@Borough", SqlDbType.NVarChar,20),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2)};
			parameters[0].Value = model.ManagerCode;
			parameters[1].Value = model.ManagerType;
			parameters[2].Value = model.ManagerName;
			parameters[3].Value = model.LoginCode;
			parameters[4].Value = model.LoginPassWD;
			parameters[5].Value = model.IPAddress;
			parameters[6].Value = model.MobileTel;
			parameters[7].Value = model.EMail;
			parameters[8].Value = model.Province;
			parameters[9].Value = model.City;
			parameters[10].Value = model.Borough;
			parameters[11].Value = model.IsDelete;

			DbHelperSQL.RunProcedure("Proc_Tb_System_Manager_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.System.Tb_System_Manager model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ManagerCode", SqlDbType.NVarChar,20),
					new SqlParameter("@ManagerType", SqlDbType.NVarChar,20),
					new SqlParameter("@ManagerName", SqlDbType.NVarChar,100),
					new SqlParameter("@LoginCode", SqlDbType.NVarChar,20),
					new SqlParameter("@LoginPassWD", SqlDbType.NVarChar,20),
					new SqlParameter("@IPAddress", SqlDbType.NVarChar,30),
					new SqlParameter("@MobileTel", SqlDbType.NVarChar,20),
					new SqlParameter("@EMail", SqlDbType.NVarChar,50),
					new SqlParameter("@Province", SqlDbType.NVarChar,20),
					new SqlParameter("@City", SqlDbType.NVarChar,20),
					new SqlParameter("@Borough", SqlDbType.NVarChar,20),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2)};
			parameters[0].Value = model.ManagerCode;
			parameters[1].Value = model.ManagerType;
			parameters[2].Value = model.ManagerName;
			parameters[3].Value = model.LoginCode;
			parameters[4].Value = model.LoginPassWD;
			parameters[5].Value = model.IPAddress;
			parameters[6].Value = model.MobileTel;
			parameters[7].Value = model.EMail;
			parameters[8].Value = model.Province;
			parameters[9].Value = model.City;
			parameters[10].Value = model.Borough;
			parameters[11].Value = model.IsDelete;

			DbHelperSQL.RunProcedure("Proc_Tb_System_Manager_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete()
		{
			int rowsAffected;
			SqlParameter[] parameters = {
};

			DbHelperSQL.RunProcedure("Proc_Tb_System_Manager_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.System.Tb_System_Manager GetModel()
		{
			SqlParameter[] parameters = {
};

			MobileSoft.Model.System.Tb_System_Manager model=new MobileSoft.Model.System.Tb_System_Manager();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_System_Manager_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				model.ManagerCode=ds.Tables[0].Rows[0]["ManagerCode"].ToString();
				model.ManagerType=ds.Tables[0].Rows[0]["ManagerType"].ToString();
				model.ManagerName=ds.Tables[0].Rows[0]["ManagerName"].ToString();
				model.LoginCode=ds.Tables[0].Rows[0]["LoginCode"].ToString();
				model.LoginPassWD=ds.Tables[0].Rows[0]["LoginPassWD"].ToString();
				model.IPAddress=ds.Tables[0].Rows[0]["IPAddress"].ToString();
				model.MobileTel=ds.Tables[0].Rows[0]["MobileTel"].ToString();
				model.EMail=ds.Tables[0].Rows[0]["EMail"].ToString();
				model.Province=ds.Tables[0].Rows[0]["Province"].ToString();
				model.City=ds.Tables[0].Rows[0]["City"].ToString();
				model.Borough=ds.Tables[0].Rows[0]["Borough"].ToString();
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
			strSql.Append("select ManagerCode,ManagerType,ManagerName,LoginCode,LoginPassWD,IPAddress,MobileTel,EMail,Province,City,Borough,IsDelete ");
			strSql.Append(" FROM Tb_System_Manager ");
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
			strSql.Append(" ManagerCode,ManagerType,ManagerName,LoginCode,LoginPassWD,IPAddress,MobileTel,EMail,Province,City,Borough,IsDelete ");
			strSql.Append(" FROM Tb_System_Manager ");
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
			parameters[5].Value = "SELECT * FROM Tb_System_Manager WHERE 1=1 " + StrCondition;
			parameters[6].Value = "";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

