using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.Common
{
	/// <summary>
	/// 数据访问类Dal_Tb_Common_CommonInfo。
	/// </summary>
	public class Dal_Tb_Common_CommonInfo
	{
		public Dal_Tb_Common_CommonInfo()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long IID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@IID", SqlDbType.BigInt)};
			parameters[0].Value = IID;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_Common_CommonInfo_Exists",parameters,out rowsAffected);
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
		public int Add(MobileSoft.Model.Common.Tb_Common_CommonInfo model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@IID", SqlDbType.BigInt,8),
					new SqlParameter("@DepCode", SqlDbType.NVarChar,20),
					new SqlParameter("@OrganCode", SqlDbType.NVarChar,20),
					new SqlParameter("@TypeCode", SqlDbType.NVarChar,20),
					new SqlParameter("@Title", SqlDbType.NVarChar,100),
					new SqlParameter("@Content", SqlDbType.NText),
					new SqlParameter("@UserName", SqlDbType.NVarChar,50),
					new SqlParameter("@IssueDate", SqlDbType.DateTime),
					new SqlParameter("@Type", SqlDbType.NVarChar,20),
					new SqlParameter("@ReadDepartName", SqlDbType.NVarChar,3999),
					new SqlParameter("@ReadDepartCode", SqlDbType.NVarChar,3999),
					new SqlParameter("@ReadUserName", SqlDbType.NVarChar,3999),
					new SqlParameter("@ReadUserCode", SqlDbType.NVarChar,3999),
					new SqlParameter("@HaveReadUserName", SqlDbType.NVarChar,3999),
					new SqlParameter("@HaveReadUserCode", SqlDbType.NVarChar,3999)};
			parameters[0].Direction = ParameterDirection.Output;
			parameters[1].Value = model.DepCode;
			parameters[2].Value = model.OrganCode;
			parameters[3].Value = model.TypeCode;
			parameters[4].Value = model.Title;
			parameters[5].Value = model.Content;
			parameters[6].Value = model.UserName;
			parameters[7].Value = model.IssueDate;
			parameters[8].Value = model.Type;
			parameters[9].Value = model.ReadDepartName;
			parameters[10].Value = model.ReadDepartCode;
			parameters[11].Value = model.ReadUserName;
			parameters[12].Value = model.ReadUserCode;
			parameters[13].Value = model.HaveReadUserName;
			parameters[14].Value = model.HaveReadUserCode;

			DbHelperSQL.RunProcedure("Proc_Tb_Common_CommonInfo_ADD",parameters,out rowsAffected);
			return (int)parameters[0].Value;
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.Common.Tb_Common_CommonInfo model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@IID", SqlDbType.BigInt,8),
					new SqlParameter("@DepCode", SqlDbType.NVarChar,20),
					new SqlParameter("@OrganCode", SqlDbType.NVarChar,20),
					new SqlParameter("@TypeCode", SqlDbType.NVarChar,20),
					new SqlParameter("@Title", SqlDbType.NVarChar,100),
					new SqlParameter("@Content", SqlDbType.NText),
					new SqlParameter("@UserName", SqlDbType.NVarChar,50),
					new SqlParameter("@IssueDate", SqlDbType.DateTime),
					new SqlParameter("@Type", SqlDbType.NVarChar,20),
					new SqlParameter("@ReadDepartName", SqlDbType.NVarChar,3999),
					new SqlParameter("@ReadDepartCode", SqlDbType.NVarChar,3999),
					new SqlParameter("@ReadUserName", SqlDbType.NVarChar,3999),
					new SqlParameter("@ReadUserCode", SqlDbType.NVarChar,3999),
					new SqlParameter("@HaveReadUserName", SqlDbType.NVarChar,3999),
					new SqlParameter("@HaveReadUserCode", SqlDbType.NVarChar,3999)};
			parameters[0].Value = model.IID;
			parameters[1].Value = model.DepCode;
			parameters[2].Value = model.OrganCode;
			parameters[3].Value = model.TypeCode;
			parameters[4].Value = model.Title;
			parameters[5].Value = model.Content;
			parameters[6].Value = model.UserName;
			parameters[7].Value = model.IssueDate;
			parameters[8].Value = model.Type;
			parameters[9].Value = model.ReadDepartName;
			parameters[10].Value = model.ReadDepartCode;
			parameters[11].Value = model.ReadUserName;
			parameters[12].Value = model.ReadUserCode;
			parameters[13].Value = model.HaveReadUserName;
			parameters[14].Value = model.HaveReadUserCode;

			DbHelperSQL.RunProcedure("Proc_Tb_Common_CommonInfo_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long IID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@IID", SqlDbType.BigInt)};
			parameters[0].Value = IID;

			DbHelperSQL.RunProcedure("Proc_Tb_Common_CommonInfo_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.Common.Tb_Common_CommonInfo GetModel(long IID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@IID", SqlDbType.BigInt)};
			parameters[0].Value = IID;

			MobileSoft.Model.Common.Tb_Common_CommonInfo model=new MobileSoft.Model.Common.Tb_Common_CommonInfo();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_Common_CommonInfo_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["IID"].ToString()!="")
				{
					model.IID=long.Parse(ds.Tables[0].Rows[0]["IID"].ToString());
				}
				model.DepCode=ds.Tables[0].Rows[0]["DepCode"].ToString();
				model.OrganCode=ds.Tables[0].Rows[0]["OrganCode"].ToString();
				model.TypeCode=ds.Tables[0].Rows[0]["TypeCode"].ToString();
				model.Title=ds.Tables[0].Rows[0]["Title"].ToString();
				model.Content=ds.Tables[0].Rows[0]["Content"].ToString();
				model.UserName=ds.Tables[0].Rows[0]["UserName"].ToString();
				if(ds.Tables[0].Rows[0]["IssueDate"].ToString()!="")
				{
					model.IssueDate=DateTime.Parse(ds.Tables[0].Rows[0]["IssueDate"].ToString());
				}
				model.Type=ds.Tables[0].Rows[0]["Type"].ToString();
				model.ReadDepartName=ds.Tables[0].Rows[0]["ReadDepartName"].ToString();
				model.ReadDepartCode=ds.Tables[0].Rows[0]["ReadDepartCode"].ToString();
				model.ReadUserName=ds.Tables[0].Rows[0]["ReadUserName"].ToString();
				model.ReadUserCode=ds.Tables[0].Rows[0]["ReadUserCode"].ToString();
				model.HaveReadUserName=ds.Tables[0].Rows[0]["HaveReadUserName"].ToString();
				model.HaveReadUserCode=ds.Tables[0].Rows[0]["HaveReadUserCode"].ToString();
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
			strSql.Append("select IID,DepCode,OrganCode,TypeCode,Title,Content,UserName,IssueDate,Type,ReadDepartName,ReadDepartCode,ReadUserName,ReadUserCode,HaveReadUserName,HaveReadUserCode ");
			strSql.Append(" FROM Tb_Common_CommonInfo ");
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
			strSql.Append(" IID,DepCode,OrganCode,TypeCode,Title,Content,UserName,IssueDate,Type,ReadDepartName,ReadDepartCode,ReadUserName,ReadUserCode,HaveReadUserName,HaveReadUserCode ");
			strSql.Append(" FROM Tb_Common_CommonInfo ");
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
			parameters[5].Value = "SELECT * FROM Tb_Common_CommonInfo WHERE 1=1 " + StrCondition;
			parameters[6].Value = "IID";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法

        public DataTable Common_CommonInfo_TopNum(int TopNum, string SQLEx)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@TopNum", SqlDbType.NVarChar,50),
                              new SqlParameter("@SQLEx", SqlDbType.VarChar,3999)
                                              };
            parameters[0].Value = TopNum;
            parameters[1].Value = SQLEx;

            DataTable result = DbHelperSQL.RunProcedure("Proc_Common_CommonInfo_TopNum", parameters, "RetDataSet").Tables[0];

            return result;
        }
	}
}

