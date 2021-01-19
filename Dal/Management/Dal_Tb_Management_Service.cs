using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.Management
{
	/// <summary>
	/// 数据访问类Dal_Tb_Management_Service。
	/// </summary>
	public class Dal_Tb_Management_Service
	{
		public Dal_Tb_Management_Service()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string ServiceCode)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ServiceCode", SqlDbType.VarChar,50)};
			parameters[0].Value = ServiceCode;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_Management_Service_Exists",parameters,out rowsAffected);
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
		public void Add(MobileSoft.Model.Management.Tb_Management_Service model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ServiceCode", SqlDbType.VarChar,36),
					new SqlParameter("@OrganCode", SqlDbType.VarChar,36),
					new SqlParameter("@CommID", SqlDbType.VarChar,36),
					new SqlParameter("@Sort", SqlDbType.Int,4),
					new SqlParameter("@TypeCode", SqlDbType.VarChar,36),
					new SqlParameter("@ServiceName", SqlDbType.VarChar,36),
					new SqlParameter("@Adv", SqlDbType.VarChar,3999),
					new SqlParameter("@Intro", SqlDbType.VarChar,3999),
					new SqlParameter("@ServiceStartDate", SqlDbType.DateTime),
					new SqlParameter("@ServiceEndDate", SqlDbType.DateTime),
					new SqlParameter("@CorpStanID", SqlDbType.BigInt,8),
					new SqlParameter("@CostUnit", SqlDbType.VarChar,36),
					new SqlParameter("@ServiceTel", SqlDbType.VarChar,36),
					new SqlParameter("@SupplierCode", SqlDbType.VarChar,36),
					new SqlParameter("@ServiceUrl", SqlDbType.VarChar,3999),
					new SqlParameter("@Memo", SqlDbType.VarChar,3999),
					new SqlParameter("@ServiceState", SqlDbType.VarChar,36),
					new SqlParameter("@IsPublish", SqlDbType.VarChar,36),
					new SqlParameter("@CommIDs", SqlDbType.VarChar,3999),
					new SqlParameter("@CommNames", SqlDbType.VarChar,3999),
					new SqlParameter("@IsDelete", SqlDbType.Int,4)};
			parameters[0].Value = model.ServiceCode;
			parameters[1].Value = model.OrganCode;
			parameters[2].Value = model.CommID;
			parameters[3].Value = model.Sort;
			parameters[4].Value = model.TypeCode;
			parameters[5].Value = model.ServiceName;
			parameters[6].Value = model.Adv;
			parameters[7].Value = model.Intro;
			parameters[8].Value = model.ServiceStartDate;
			parameters[9].Value = model.ServiceEndDate;
			parameters[10].Value = model.CorpStanID;
			parameters[11].Value = model.CostUnit;
			parameters[12].Value = model.ServiceTel;
			parameters[13].Value = model.SupplierCode;
			parameters[14].Value = model.ServiceUrl;
			parameters[15].Value = model.Memo;
			parameters[16].Value = model.ServiceState;
			parameters[17].Value = model.IsPublish;
			parameters[18].Value = model.CommIDs;
			parameters[19].Value = model.CommNames;
			parameters[20].Value = model.IsDelete;

			DbHelperSQL.RunProcedure("Proc_Tb_Management_Service_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.Management.Tb_Management_Service model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ServiceCode", SqlDbType.VarChar,36),
					new SqlParameter("@OrganCode", SqlDbType.VarChar,36),
					new SqlParameter("@CommID", SqlDbType.VarChar,36),
					new SqlParameter("@Sort", SqlDbType.Int,4),
					new SqlParameter("@TypeCode", SqlDbType.VarChar,36),
					new SqlParameter("@ServiceName", SqlDbType.VarChar,36),
					new SqlParameter("@Adv", SqlDbType.VarChar,3999),
					new SqlParameter("@Intro", SqlDbType.VarChar,3999),
					new SqlParameter("@ServiceStartDate", SqlDbType.DateTime),
					new SqlParameter("@ServiceEndDate", SqlDbType.DateTime),
					new SqlParameter("@CorpStanID", SqlDbType.BigInt,8),
					new SqlParameter("@CostUnit", SqlDbType.VarChar,36),
					new SqlParameter("@ServiceTel", SqlDbType.VarChar,36),
					new SqlParameter("@SupplierCode", SqlDbType.VarChar,36),
					new SqlParameter("@ServiceUrl", SqlDbType.VarChar,3999),
					new SqlParameter("@Memo", SqlDbType.VarChar,3999),
					new SqlParameter("@ServiceState", SqlDbType.VarChar,36),
					new SqlParameter("@IsPublish", SqlDbType.VarChar,36),
					new SqlParameter("@CommIDs", SqlDbType.VarChar,3999),
					new SqlParameter("@CommNames", SqlDbType.VarChar,3999),
					new SqlParameter("@IsDelete", SqlDbType.Int,4)};
			parameters[0].Value = model.ServiceCode;
			parameters[1].Value = model.OrganCode;
			parameters[2].Value = model.CommID;
			parameters[3].Value = model.Sort;
			parameters[4].Value = model.TypeCode;
			parameters[5].Value = model.ServiceName;
			parameters[6].Value = model.Adv;
			parameters[7].Value = model.Intro;
			parameters[8].Value = model.ServiceStartDate;
			parameters[9].Value = model.ServiceEndDate;
			parameters[10].Value = model.CorpStanID;
			parameters[11].Value = model.CostUnit;
			parameters[12].Value = model.ServiceTel;
			parameters[13].Value = model.SupplierCode;
			parameters[14].Value = model.ServiceUrl;
			parameters[15].Value = model.Memo;
			parameters[16].Value = model.ServiceState;
			parameters[17].Value = model.IsPublish;
			parameters[18].Value = model.CommIDs;
			parameters[19].Value = model.CommNames;
			parameters[20].Value = model.IsDelete;

			DbHelperSQL.RunProcedure("Proc_Tb_Management_Service_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(string ServiceCode)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ServiceCode", SqlDbType.VarChar,50)};
			parameters[0].Value = ServiceCode;

			DbHelperSQL.RunProcedure("Proc_Tb_Management_Service_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.Management.Tb_Management_Service GetModel(string ServiceCode)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@ServiceCode", SqlDbType.VarChar,50)};
			parameters[0].Value = ServiceCode;

			MobileSoft.Model.Management.Tb_Management_Service model=new MobileSoft.Model.Management.Tb_Management_Service();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_Management_Service_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				model.ServiceCode=ds.Tables[0].Rows[0]["ServiceCode"].ToString();
				model.OrganCode=ds.Tables[0].Rows[0]["OrganCode"].ToString();
				model.CommID=ds.Tables[0].Rows[0]["CommID"].ToString();
				if(ds.Tables[0].Rows[0]["Sort"].ToString()!="")
				{
					model.Sort=int.Parse(ds.Tables[0].Rows[0]["Sort"].ToString());
				}
				model.TypeCode=ds.Tables[0].Rows[0]["TypeCode"].ToString();
				model.ServiceName=ds.Tables[0].Rows[0]["ServiceName"].ToString();
				model.Adv=ds.Tables[0].Rows[0]["Adv"].ToString();
				model.Intro=ds.Tables[0].Rows[0]["Intro"].ToString();
				if(ds.Tables[0].Rows[0]["ServiceStartDate"].ToString()!="")
				{
					model.ServiceStartDate=DateTime.Parse(ds.Tables[0].Rows[0]["ServiceStartDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ServiceEndDate"].ToString()!="")
				{
					model.ServiceEndDate=DateTime.Parse(ds.Tables[0].Rows[0]["ServiceEndDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CorpStanID"].ToString()!="")
				{
					model.CorpStanID=long.Parse(ds.Tables[0].Rows[0]["CorpStanID"].ToString());
				}
				model.CostUnit=ds.Tables[0].Rows[0]["CostUnit"].ToString();
				model.ServiceTel=ds.Tables[0].Rows[0]["ServiceTel"].ToString();
				model.SupplierCode=ds.Tables[0].Rows[0]["SupplierCode"].ToString();
				model.ServiceUrl=ds.Tables[0].Rows[0]["ServiceUrl"].ToString();
				model.Memo=ds.Tables[0].Rows[0]["Memo"].ToString();
				model.ServiceState=ds.Tables[0].Rows[0]["ServiceState"].ToString();
				model.IsPublish=ds.Tables[0].Rows[0]["IsPublish"].ToString();
				model.CommIDs=ds.Tables[0].Rows[0]["CommIDs"].ToString();
				model.CommNames=ds.Tables[0].Rows[0]["CommNames"].ToString();
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

        public DataSet GetGroupList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select TypeCode,TypeName ");
            strSql.Append(" FROM View_Management_Service_Filter WHERE ISNULL(IsDelete,0)=0 GROUP BY TypeCode,TypeName");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * ");
            strSql.Append(" FROM View_Management_Service_Filter ");
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
			strSql.Append(" * ");
			strSql.Append(" FROM View_Management_Service_Filter ");
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
			parameters[5].Value = "SELECT * FROM View_Management_Service_Filter WHERE 1=1 " + StrCondition;
			parameters[6].Value = "ServiceCode";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

