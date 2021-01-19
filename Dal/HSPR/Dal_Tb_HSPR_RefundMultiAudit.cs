using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.HSPR
{
	/// <summary>
	/// 数据访问类Dal_Tb_HSPR_RefundMultiAudit。
	/// </summary>
	public class Dal_Tb_HSPR_RefundMultiAudit
	{
		public Dal_Tb_HSPR_RefundMultiAudit()
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

			int result= DbHelperSQL.RunProcedure("Proc_Tb_HSPR_RefundMultiAudit_Exists",parameters,out rowsAffected);
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
		public int Add(MobileSoft.Model.HSPR.Tb_HSPR_RefundMultiAudit model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@IID", SqlDbType.BigInt,8),
					new SqlParameter("@BusinessType", SqlDbType.SmallInt,2),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@CustID", SqlDbType.BigInt,8),
					new SqlParameter("@RoomID", SqlDbType.BigInt,8),
					new SqlParameter("@CostID", SqlDbType.BigInt,8),
					new SqlParameter("@FeesID", SqlDbType.BigInt,8),
					new SqlParameter("@RefundID", SqlDbType.BigInt,8),
					new SqlParameter("@PrecID", SqlDbType.BigInt,8)};
			parameters[0].Direction = ParameterDirection.Output;
			parameters[1].Value = model.BusinessType;
			parameters[2].Value = model.CommID;
			parameters[3].Value = model.CustID;
			parameters[4].Value = model.RoomID;
			parameters[5].Value = model.CostID;
			parameters[6].Value = model.FeesID;
			parameters[7].Value = model.RefundID;
			parameters[8].Value = model.PrecID;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_RefundMultiAudit_ADD",parameters,out rowsAffected);
			return (int)parameters[0].Value;
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.HSPR.Tb_HSPR_RefundMultiAudit model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@IID", SqlDbType.BigInt,8),
					new SqlParameter("@BusinessType", SqlDbType.SmallInt,2),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@CustID", SqlDbType.BigInt,8),
					new SqlParameter("@RoomID", SqlDbType.BigInt,8),
					new SqlParameter("@CostID", SqlDbType.BigInt,8),
					new SqlParameter("@FeesID", SqlDbType.BigInt,8),
					new SqlParameter("@RefundID", SqlDbType.BigInt,8),
					new SqlParameter("@PrecID", SqlDbType.BigInt,8)};
			parameters[0].Value = model.IID;
			parameters[1].Value = model.BusinessType;
			parameters[2].Value = model.CommID;
			parameters[3].Value = model.CustID;
			parameters[4].Value = model.RoomID;
			parameters[5].Value = model.CostID;
			parameters[6].Value = model.FeesID;
			parameters[7].Value = model.RefundID;
			parameters[8].Value = model.PrecID;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_RefundMultiAudit_Update",parameters,out rowsAffected);
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

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_RefundMultiAudit_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_RefundMultiAudit GetModel(long IID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@IID", SqlDbType.BigInt)};
			parameters[0].Value = IID;

			MobileSoft.Model.HSPR.Tb_HSPR_RefundMultiAudit model=new MobileSoft.Model.HSPR.Tb_HSPR_RefundMultiAudit();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_HSPR_RefundMultiAudit_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["IID"].ToString()!="")
				{
					model.IID=long.Parse(ds.Tables[0].Rows[0]["IID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["BusinessType"].ToString()!="")
				{
					model.BusinessType=int.Parse(ds.Tables[0].Rows[0]["BusinessType"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CommID"].ToString()!="")
				{
					model.CommID=int.Parse(ds.Tables[0].Rows[0]["CommID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CustID"].ToString()!="")
				{
					model.CustID=long.Parse(ds.Tables[0].Rows[0]["CustID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["RoomID"].ToString()!="")
				{
					model.RoomID=long.Parse(ds.Tables[0].Rows[0]["RoomID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CostID"].ToString()!="")
				{
					model.CostID=long.Parse(ds.Tables[0].Rows[0]["CostID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["FeesID"].ToString()!="")
				{
					model.FeesID=long.Parse(ds.Tables[0].Rows[0]["FeesID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["RefundID"].ToString()!="")
				{
					model.RefundID=long.Parse(ds.Tables[0].Rows[0]["RefundID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["PrecID"].ToString()!="")
				{
					model.PrecID=long.Parse(ds.Tables[0].Rows[0]["PrecID"].ToString());
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
			strSql.Append("select * ");
                  strSql.Append(" FROM view_HSPR_RefundMultiAudit_Filter ");
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
			strSql.Append(" * ");
                  strSql.Append(" FROM view_HSPR_RefundMultiAudit_Filter ");
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
                  parameters[5].Value = "SELECT * FROM view_HSPR_RefundMultiAudit_Filter WHERE 1=1 " + StrCondition;
			parameters[6].Value = "IID";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法

            public void RefundMultiAuditCre(int CommID, long CustID, long RoomID, string SubdataStarttime, string SubdataEndtime, string LoginRoles, string CostIDs, int BusinessType)
            {
                  SqlParameter[] parameters = {
					new SqlParameter("@CommID", SqlDbType.Int),
                              new SqlParameter("@CustID", SqlDbType.VarChar,50),
                              new SqlParameter("@RoomID", SqlDbType.Int),
                              new SqlParameter("@SubdataStarttime", SqlDbType.VarChar,50),
                              new SqlParameter("@SubdataEndtime", SqlDbType.VarChar,50),
                              new SqlParameter("@LoginRoles", SqlDbType.VarChar,2000),
                              new SqlParameter("@CostIDs", SqlDbType.VarChar,2000),
                              new SqlParameter("@BusinessType", SqlDbType.Int)
                                              };
                  parameters[0].Value = CommID;
                  parameters[1].Value = CustID;
                  parameters[2].Value = RoomID;
                  parameters[3].Value = SubdataStarttime;
                  parameters[4].Value = SubdataEndtime;
                  parameters[5].Value = LoginRoles;
                  parameters[6].Value = CostIDs;
                  parameters[7].Value = BusinessType;

                  DbHelperSQL.RunProcedure("Proc_HSPR_RefundMultiAudit_Cre", parameters, "RetDataSet");
            }

            public void RefundSecFeesUpdateAudit(int CommID, long IID, string AuditUsercode, string UserRoles, int IsAudit)
            {
                  SqlParameter[] parameters = {
					new SqlParameter("@CommID", SqlDbType.Int),
                              new SqlParameter("@IID", SqlDbType.VarChar,50),
                              new SqlParameter("@AuditUsercode", SqlDbType.Int),
                              new SqlParameter("@UserRoles", SqlDbType.VarChar,50),
                              new SqlParameter("@IsAudit", SqlDbType.VarChar,50),
                                              };
                  parameters[0].Value = CommID;
                  parameters[1].Value = IID;
                  parameters[2].Value = AuditUsercode;
                  parameters[3].Value = UserRoles;
                  parameters[4].Value = IsAudit;
            
                  DbHelperSQL.RunProcedure("Proc_HSPR_RefundFees_SecUpdateAudit", parameters, "RetDataSet");
            }
            
	}
}

