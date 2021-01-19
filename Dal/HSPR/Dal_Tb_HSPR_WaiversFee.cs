using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.HSPR
{
	/// <summary>
	/// 数据访问类Dal_Tb_HSPR_WaiversFee。
	/// </summary>
	public class Dal_Tb_HSPR_WaiversFee
	{
		public Dal_Tb_HSPR_WaiversFee()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long WaivID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@WaivID", SqlDbType.BigInt)};
			parameters[0].Value = WaivID;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_HSPR_WaiversFee_Exists",parameters,out rowsAffected);
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
		public void Add(MobileSoft.Model.HSPR.Tb_HSPR_WaiversFee model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@WaivID", SqlDbType.BigInt,8),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@CustID", SqlDbType.BigInt,8),
					new SqlParameter("@RoomID", SqlDbType.BigInt,8),
					new SqlParameter("@CostID", SqlDbType.BigInt,8),
					new SqlParameter("@WaivAmount", SqlDbType.Decimal,9),
					new SqlParameter("@WaivedAmount", SqlDbType.Decimal,9),
					new SqlParameter("@WaivStateDuring", SqlDbType.DateTime),
					new SqlParameter("@WaivEndDuring", SqlDbType.DateTime),
					new SqlParameter("@WaivDate", SqlDbType.DateTime),
					new SqlParameter("@WaivMonthAmount", SqlDbType.Decimal,9),
					new SqlParameter("@UserCode", SqlDbType.NVarChar,20),
					new SqlParameter("@WaivReason", SqlDbType.NVarChar,200),
					new SqlParameter("@AuditReason", SqlDbType.NVarChar,200),
					new SqlParameter("@WaivMemo", SqlDbType.NVarChar,50),
					new SqlParameter("@AuditUserCode", SqlDbType.NVarChar,20),
					new SqlParameter("@IsWaiv", SqlDbType.SmallInt,2),
					new SqlParameter("@IsAudit", SqlDbType.SmallInt,2),
					new SqlParameter("@WaivType", SqlDbType.SmallInt,2),
					new SqlParameter("@WaivRates", SqlDbType.Decimal,9),
					new SqlParameter("@WaivCostID", SqlDbType.BigInt,8),
					new SqlParameter("@HandID", SqlDbType.BigInt,8),
					new SqlParameter("@MeterSign", SqlDbType.NVarChar,10),
					new SqlParameter("@AuditUserName", SqlDbType.NVarChar,100),
					new SqlParameter("@WaivCreDate", SqlDbType.DateTime)};
			parameters[0].Value = model.WaivID;
			parameters[1].Value = model.CommID;
			parameters[2].Value = model.CustID;
			parameters[3].Value = model.RoomID;
			parameters[4].Value = model.CostID;
			parameters[5].Value = model.WaivAmount;
			parameters[6].Value = model.WaivedAmount;
			parameters[7].Value = model.WaivStateDuring;
			parameters[8].Value = model.WaivEndDuring;
			parameters[9].Value = model.WaivDate;
			parameters[10].Value = model.WaivMonthAmount;
			parameters[11].Value = model.UserCode;
			parameters[12].Value = model.WaivReason;
			parameters[13].Value = model.AuditReason;
			parameters[14].Value = model.WaivMemo;
			parameters[15].Value = model.AuditUserCode;
			parameters[16].Value = model.IsWaiv;
			parameters[17].Value = model.IsAudit;
			parameters[18].Value = model.WaivType;
			parameters[19].Value = model.WaivRates;
			parameters[20].Value = model.WaivCostID;
			parameters[21].Value = model.HandID;
			parameters[22].Value = model.MeterSign;
			parameters[23].Value = model.AuditUserName;
			parameters[24].Value = model.WaivCreDate;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_WaiversFee_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.HSPR.Tb_HSPR_WaiversFee model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@WaivID", SqlDbType.BigInt,8),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@CustID", SqlDbType.BigInt,8),
					new SqlParameter("@RoomID", SqlDbType.BigInt,8),
					new SqlParameter("@CostID", SqlDbType.BigInt,8),
					new SqlParameter("@WaivAmount", SqlDbType.Decimal,9),
					new SqlParameter("@WaivedAmount", SqlDbType.Decimal,9),
					new SqlParameter("@WaivStateDuring", SqlDbType.DateTime),
					new SqlParameter("@WaivEndDuring", SqlDbType.DateTime),
					new SqlParameter("@WaivDate", SqlDbType.DateTime),
					new SqlParameter("@WaivMonthAmount", SqlDbType.Decimal,9),
					new SqlParameter("@UserCode", SqlDbType.NVarChar,20),
					new SqlParameter("@WaivReason", SqlDbType.NVarChar,200),
					new SqlParameter("@AuditReason", SqlDbType.NVarChar,200),
					new SqlParameter("@WaivMemo", SqlDbType.NVarChar,50),
					new SqlParameter("@AuditUserCode", SqlDbType.NVarChar,20),
					new SqlParameter("@IsWaiv", SqlDbType.SmallInt,2),
					new SqlParameter("@IsAudit", SqlDbType.SmallInt,2),
					new SqlParameter("@WaivType", SqlDbType.SmallInt,2),
					new SqlParameter("@WaivRates", SqlDbType.Decimal,9),
					new SqlParameter("@WaivCostID", SqlDbType.BigInt,8),
					new SqlParameter("@HandID", SqlDbType.BigInt,8),
					new SqlParameter("@MeterSign", SqlDbType.NVarChar,10),
					new SqlParameter("@AuditUserName", SqlDbType.NVarChar,100),
					new SqlParameter("@WaivCreDate", SqlDbType.DateTime)};
			parameters[0].Value = model.WaivID;
			parameters[1].Value = model.CommID;
			parameters[2].Value = model.CustID;
			parameters[3].Value = model.RoomID;
			parameters[4].Value = model.CostID;
			parameters[5].Value = model.WaivAmount;
			parameters[6].Value = model.WaivedAmount;
			parameters[7].Value = model.WaivStateDuring;
			parameters[8].Value = model.WaivEndDuring;
			parameters[9].Value = model.WaivDate;
			parameters[10].Value = model.WaivMonthAmount;
			parameters[11].Value = model.UserCode;
			parameters[12].Value = model.WaivReason;
			parameters[13].Value = model.AuditReason;
			parameters[14].Value = model.WaivMemo;
			parameters[15].Value = model.AuditUserCode;
			parameters[16].Value = model.IsWaiv;
			parameters[17].Value = model.IsAudit;
			parameters[18].Value = model.WaivType;
			parameters[19].Value = model.WaivRates;
			parameters[20].Value = model.WaivCostID;
			parameters[21].Value = model.HandID;
			parameters[22].Value = model.MeterSign;
			parameters[23].Value = model.AuditUserName;
			parameters[24].Value = model.WaivCreDate;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_WaiversFee_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long WaivID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@WaivID", SqlDbType.BigInt)};
			parameters[0].Value = WaivID;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_WaiversFee_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_WaiversFee GetModel(long WaivID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@WaivID", SqlDbType.BigInt)};
			parameters[0].Value = WaivID;

			MobileSoft.Model.HSPR.Tb_HSPR_WaiversFee model=new MobileSoft.Model.HSPR.Tb_HSPR_WaiversFee();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_HSPR_WaiversFee_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["WaivID"].ToString()!="")
				{
					model.WaivID=long.Parse(ds.Tables[0].Rows[0]["WaivID"].ToString());
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
				if(ds.Tables[0].Rows[0]["WaivAmount"].ToString()!="")
				{
					model.WaivAmount=decimal.Parse(ds.Tables[0].Rows[0]["WaivAmount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["WaivedAmount"].ToString()!="")
				{
					model.WaivedAmount=decimal.Parse(ds.Tables[0].Rows[0]["WaivedAmount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["WaivStateDuring"].ToString()!="")
				{
					model.WaivStateDuring=DateTime.Parse(ds.Tables[0].Rows[0]["WaivStateDuring"].ToString());
				}
				if(ds.Tables[0].Rows[0]["WaivEndDuring"].ToString()!="")
				{
					model.WaivEndDuring=DateTime.Parse(ds.Tables[0].Rows[0]["WaivEndDuring"].ToString());
				}
				if(ds.Tables[0].Rows[0]["WaivDate"].ToString()!="")
				{
					model.WaivDate=DateTime.Parse(ds.Tables[0].Rows[0]["WaivDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["WaivMonthAmount"].ToString()!="")
				{
					model.WaivMonthAmount=decimal.Parse(ds.Tables[0].Rows[0]["WaivMonthAmount"].ToString());
				}
				model.UserCode=ds.Tables[0].Rows[0]["UserCode"].ToString();
				model.WaivReason=ds.Tables[0].Rows[0]["WaivReason"].ToString();
				model.AuditReason=ds.Tables[0].Rows[0]["AuditReason"].ToString();
				model.WaivMemo=ds.Tables[0].Rows[0]["WaivMemo"].ToString();
				model.AuditUserCode=ds.Tables[0].Rows[0]["AuditUserCode"].ToString();
				if(ds.Tables[0].Rows[0]["IsWaiv"].ToString()!="")
				{
					model.IsWaiv=int.Parse(ds.Tables[0].Rows[0]["IsWaiv"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsAudit"].ToString()!="")
				{
					model.IsAudit=int.Parse(ds.Tables[0].Rows[0]["IsAudit"].ToString());
				}
				if(ds.Tables[0].Rows[0]["WaivType"].ToString()!="")
				{
					model.WaivType=int.Parse(ds.Tables[0].Rows[0]["WaivType"].ToString());
				}
				if(ds.Tables[0].Rows[0]["WaivRates"].ToString()!="")
				{
					model.WaivRates=decimal.Parse(ds.Tables[0].Rows[0]["WaivRates"].ToString());
				}
				if(ds.Tables[0].Rows[0]["WaivCostID"].ToString()!="")
				{
					model.WaivCostID=long.Parse(ds.Tables[0].Rows[0]["WaivCostID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["HandID"].ToString()!="")
				{
					model.HandID=long.Parse(ds.Tables[0].Rows[0]["HandID"].ToString());
				}
				model.MeterSign=ds.Tables[0].Rows[0]["MeterSign"].ToString();
				model.AuditUserName=ds.Tables[0].Rows[0]["AuditUserName"].ToString();
				if(ds.Tables[0].Rows[0]["WaivCreDate"].ToString()!="")
				{
					model.WaivCreDate=DateTime.Parse(ds.Tables[0].Rows[0]["WaivCreDate"].ToString());
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
                  strSql.Append(" FROM view_HSPR_WaiversFee_Audit_Filter ");
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
            strSql.Append(" FROM view_HSPR_WaiversFee_Audit_Filter ");
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
                  parameters[5].Value = "SELECT * FROM view_HSPR_WaiversFee_Audit_Filter WHERE 1=1 " + StrCondition;
			parameters[6].Value = "WaivID";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法

           public void WaiversFeeUpdateAudit(int CommID,long WaivID,string AuditUsercode,string UserRoles,string AuditReason,int IsAudit)
		{
                  SqlParameter[] parameters = {
					new SqlParameter("@CommID", SqlDbType.BigInt),
                              new SqlParameter("@WaivID", SqlDbType.BigInt),
                              new SqlParameter("@AuditUsercode", SqlDbType.VarChar,50),
                              new SqlParameter("@UserRoles", SqlDbType.VarChar,2000),
                              new SqlParameter("@AuditReason", SqlDbType.VarChar,1000),
                              new SqlParameter("@IsAudit", SqlDbType.Int)
                                              };
                  parameters[0].Value = CommID;
                  parameters[1].Value = WaivID;
                  parameters[2].Value = AuditUsercode;
                  parameters[3].Value = UserRoles;
                  parameters[4].Value = AuditReason;
                  parameters[5].Value = IsAudit;

                  DbHelperSQL.RunProcedure("Proc_HSPR_WaiversFee_UpdateAudit", parameters, "RetDataSet");
		}

            public bool WaiversFeeIsCheck(long WaivID, int CommID, long CustID, long RoomID, long CostID, string WaivStateDuring, string WaivEndDuring)
            {
                  bool IsOK = true;

                  SqlParameter[] parameters = {
					new SqlParameter("@WaivID", SqlDbType.BigInt),
                              new SqlParameter("@CommID", SqlDbType.Int),
                              new SqlParameter("@CustID", SqlDbType.BigInt),
                              new SqlParameter("@RoomID", SqlDbType.BigInt),
                              new SqlParameter("@CostID", SqlDbType.BigInt),
                              new SqlParameter("@WaivStateDuring", SqlDbType.VarChar,50),
                              new SqlParameter("@WaivEndDuring", SqlDbType.VarChar,50)
                                              };
                  parameters[0].Value = WaivID;
                  parameters[1].Value = CommID;
                  parameters[2].Value = CustID;
                  parameters[3].Value = RoomID;
                  parameters[4].Value = CostID;
                  parameters[5].Value = WaivStateDuring;
                  parameters[6].Value = WaivEndDuring;

                  DataTable dTable = DbHelperSQL.RunProcedure("Proc_HSPR_WaiversFee_IsCheck", parameters, "RetDataSet").Tables[0];

                  if (dTable.Rows.Count > 0)
                  {
                        IsOK = false;
                  }
                  return IsOK;
            }

	}
}

