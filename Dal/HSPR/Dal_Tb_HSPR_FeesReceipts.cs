using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.HSPR
{
	/// <summary>
	/// 数据访问类Dal_Tb_HSPR_FeesReceipts。
	/// </summary>
	public class Dal_Tb_HSPR_FeesReceipts
	{
		public Dal_Tb_HSPR_FeesReceipts()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}

		#region  成员方法

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long ReceID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ReceID", SqlDbType.BigInt)};
			parameters[0].Value = ReceID;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_HSPR_FeesReceipts_Exists",parameters,out rowsAffected);
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
		public void Add(MobileSoft.Model.HSPR.Tb_HSPR_FeesReceipts model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ReceID", SqlDbType.BigInt,8),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@CustID", SqlDbType.BigInt,8),
					new SqlParameter("@RoomID", SqlDbType.BigInt,8),
					new SqlParameter("@BillsSign", SqlDbType.NVarChar,20),
					new SqlParameter("@PrintTimes", SqlDbType.Int,4),
					new SqlParameter("@BillsDate", SqlDbType.DateTime),
					new SqlParameter("@UserCode", SqlDbType.NVarChar,20),
					new SqlParameter("@ChargeMode", SqlDbType.NVarChar,20),
					new SqlParameter("@AccountWay", SqlDbType.SmallInt,2),
					new SqlParameter("@ReceMemo", SqlDbType.NVarChar,500),
					new SqlParameter("@PerSurplus", SqlDbType.Decimal,9),
					new SqlParameter("@SurplusAmount", SqlDbType.Decimal,9),
					new SqlParameter("@PrecAmount", SqlDbType.Decimal,9),
					new SqlParameter("@PaidAmount", SqlDbType.Decimal,9),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2),
					new SqlParameter("@UseRepID", SqlDbType.BigInt,8),
					new SqlParameter("@InvoiceBill", SqlDbType.NVarChar,20),
					new SqlParameter("@InvoiceUnit", SqlDbType.NVarChar,100),
					new SqlParameter("@RemitterUnit", SqlDbType.NVarChar,100),
					new SqlParameter("@BankName", SqlDbType.NVarChar,50),
					new SqlParameter("@BankAccount", SqlDbType.NVarChar,30),
					new SqlParameter("@ChequeBill", SqlDbType.NVarChar,30),
					new SqlParameter("@RenderType", SqlDbType.SmallInt,2),
					new SqlParameter("@RenderCustID", SqlDbType.BigInt,8),
					new SqlParameter("@RenderCustName", SqlDbType.NVarChar,50),
					new SqlParameter("@IsRefer", SqlDbType.SmallInt,2),
					new SqlParameter("@ReferReason", SqlDbType.NVarChar,200),
					new SqlParameter("@ReferUserCode", SqlDbType.NVarChar,20),
					new SqlParameter("@ReferDate", SqlDbType.DateTime),
					new SqlParameter("@AuditUserCode", SqlDbType.NVarChar,20),
					new SqlParameter("@AuditDate", SqlDbType.DateTime),
					new SqlParameter("@IsAudit", SqlDbType.SmallInt,2),
					new SqlParameter("@BillTypeID", SqlDbType.BigInt,8)};
			parameters[0].Value = model.ReceID;
			parameters[1].Value = model.CommID;
			parameters[2].Value = model.CustID;
			parameters[3].Value = model.RoomID;
			parameters[4].Value = model.BillsSign;
			parameters[5].Value = model.PrintTimes;
			parameters[6].Value = model.BillsDate;
			parameters[7].Value = model.UserCode;
			parameters[8].Value = model.ChargeMode;
			parameters[9].Value = model.AccountWay;
			parameters[10].Value = model.ReceMemo;
			parameters[11].Value = model.PerSurplus;
			parameters[12].Value = model.SurplusAmount;
			parameters[13].Value = model.PrecAmount;
			parameters[14].Value = model.PaidAmount;
			parameters[15].Value = model.IsDelete;
			parameters[16].Value = model.UseRepID;
			parameters[17].Value = model.InvoiceBill;
			parameters[18].Value = model.InvoiceUnit;
			parameters[19].Value = model.RemitterUnit;
			parameters[20].Value = model.BankName;
			parameters[21].Value = model.BankAccount;
			parameters[22].Value = model.ChequeBill;
			parameters[23].Value = model.RenderType;
			parameters[24].Value = model.RenderCustID;
			parameters[25].Value = model.RenderCustName;
			parameters[26].Value = model.IsRefer;
			parameters[27].Value = model.ReferReason;
			parameters[28].Value = model.ReferUserCode;
			parameters[29].Value = model.ReferDate;
			parameters[30].Value = model.AuditUserCode;
			parameters[31].Value = model.AuditDate;
			parameters[32].Value = model.IsAudit;
			parameters[33].Value = model.BillTypeID;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_FeesReceipts_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.HSPR.Tb_HSPR_FeesReceipts model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ReceID", SqlDbType.BigInt,8),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@CustID", SqlDbType.BigInt,8),
					new SqlParameter("@RoomID", SqlDbType.BigInt,8),
					new SqlParameter("@BillsSign", SqlDbType.NVarChar,20),
					new SqlParameter("@PrintTimes", SqlDbType.Int,4),
					new SqlParameter("@BillsDate", SqlDbType.DateTime),
					new SqlParameter("@UserCode", SqlDbType.NVarChar,20),
					new SqlParameter("@ChargeMode", SqlDbType.NVarChar,20),
					new SqlParameter("@AccountWay", SqlDbType.SmallInt,2),
					new SqlParameter("@ReceMemo", SqlDbType.NVarChar,500),
					new SqlParameter("@PerSurplus", SqlDbType.Decimal,9),
					new SqlParameter("@SurplusAmount", SqlDbType.Decimal,9),
					new SqlParameter("@PrecAmount", SqlDbType.Decimal,9),
					new SqlParameter("@PaidAmount", SqlDbType.Decimal,9),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2),
					new SqlParameter("@UseRepID", SqlDbType.BigInt,8),
					new SqlParameter("@InvoiceBill", SqlDbType.NVarChar,20),
					new SqlParameter("@InvoiceUnit", SqlDbType.NVarChar,100),
					new SqlParameter("@RemitterUnit", SqlDbType.NVarChar,100),
					new SqlParameter("@BankName", SqlDbType.NVarChar,50),
					new SqlParameter("@BankAccount", SqlDbType.NVarChar,30),
					new SqlParameter("@ChequeBill", SqlDbType.NVarChar,30),
					new SqlParameter("@RenderType", SqlDbType.SmallInt,2),
					new SqlParameter("@RenderCustID", SqlDbType.BigInt,8),
					new SqlParameter("@RenderCustName", SqlDbType.NVarChar,50),
					new SqlParameter("@IsRefer", SqlDbType.SmallInt,2),
					new SqlParameter("@ReferReason", SqlDbType.NVarChar,200),
					new SqlParameter("@ReferUserCode", SqlDbType.NVarChar,20),
					new SqlParameter("@ReferDate", SqlDbType.DateTime),
					new SqlParameter("@AuditUserCode", SqlDbType.NVarChar,20),
					new SqlParameter("@AuditDate", SqlDbType.DateTime),
					new SqlParameter("@IsAudit", SqlDbType.SmallInt,2),
					new SqlParameter("@BillTypeID", SqlDbType.BigInt,8)};
			parameters[0].Value = model.ReceID;
			parameters[1].Value = model.CommID;
			parameters[2].Value = model.CustID;
			parameters[3].Value = model.RoomID;
			parameters[4].Value = model.BillsSign;
			parameters[5].Value = model.PrintTimes;
			parameters[6].Value = model.BillsDate;
			parameters[7].Value = model.UserCode;
			parameters[8].Value = model.ChargeMode;
			parameters[9].Value = model.AccountWay;
			parameters[10].Value = model.ReceMemo;
			parameters[11].Value = model.PerSurplus;
			parameters[12].Value = model.SurplusAmount;
			parameters[13].Value = model.PrecAmount;
			parameters[14].Value = model.PaidAmount;
			parameters[15].Value = model.IsDelete;
			parameters[16].Value = model.UseRepID;
			parameters[17].Value = model.InvoiceBill;
			parameters[18].Value = model.InvoiceUnit;
			parameters[19].Value = model.RemitterUnit;
			parameters[20].Value = model.BankName;
			parameters[21].Value = model.BankAccount;
			parameters[22].Value = model.ChequeBill;
			parameters[23].Value = model.RenderType;
			parameters[24].Value = model.RenderCustID;
			parameters[25].Value = model.RenderCustName;
			parameters[26].Value = model.IsRefer;
			parameters[27].Value = model.ReferReason;
			parameters[28].Value = model.ReferUserCode;
			parameters[29].Value = model.ReferDate;
			parameters[30].Value = model.AuditUserCode;
			parameters[31].Value = model.AuditDate;
			parameters[32].Value = model.IsAudit;
			parameters[33].Value = model.BillTypeID;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_FeesReceipts_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long ReceID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ReceID", SqlDbType.BigInt)};
			parameters[0].Value = ReceID;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_FeesReceipts_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_FeesReceipts GetModel(long ReceID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@ReceID", SqlDbType.BigInt)};
			parameters[0].Value = ReceID;

			MobileSoft.Model.HSPR.Tb_HSPR_FeesReceipts model=new MobileSoft.Model.HSPR.Tb_HSPR_FeesReceipts();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_HSPR_FeesReceipts_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ReceID"].ToString()!="")
				{
					model.ReceID=long.Parse(ds.Tables[0].Rows[0]["ReceID"].ToString());
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
				model.BillsSign=ds.Tables[0].Rows[0]["BillsSign"].ToString();
				if(ds.Tables[0].Rows[0]["PrintTimes"].ToString()!="")
				{
					model.PrintTimes=int.Parse(ds.Tables[0].Rows[0]["PrintTimes"].ToString());
				}
				if(ds.Tables[0].Rows[0]["BillsDate"].ToString()!="")
				{
					model.BillsDate=DateTime.Parse(ds.Tables[0].Rows[0]["BillsDate"].ToString());
				}
				model.UserCode=ds.Tables[0].Rows[0]["UserCode"].ToString();
				model.ChargeMode=ds.Tables[0].Rows[0]["ChargeMode"].ToString();
				if(ds.Tables[0].Rows[0]["AccountWay"].ToString()!="")
				{
					model.AccountWay=int.Parse(ds.Tables[0].Rows[0]["AccountWay"].ToString());
				}
				model.ReceMemo=ds.Tables[0].Rows[0]["ReceMemo"].ToString();
				if(ds.Tables[0].Rows[0]["PerSurplus"].ToString()!="")
				{
					model.PerSurplus=decimal.Parse(ds.Tables[0].Rows[0]["PerSurplus"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SurplusAmount"].ToString()!="")
				{
					model.SurplusAmount=decimal.Parse(ds.Tables[0].Rows[0]["SurplusAmount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["PrecAmount"].ToString()!="")
				{
					model.PrecAmount=decimal.Parse(ds.Tables[0].Rows[0]["PrecAmount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["PaidAmount"].ToString()!="")
				{
					model.PaidAmount=decimal.Parse(ds.Tables[0].Rows[0]["PaidAmount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsDelete"].ToString()!="")
				{
					model.IsDelete=int.Parse(ds.Tables[0].Rows[0]["IsDelete"].ToString());
				}
				if(ds.Tables[0].Rows[0]["UseRepID"].ToString()!="")
				{
					model.UseRepID=long.Parse(ds.Tables[0].Rows[0]["UseRepID"].ToString());
				}
				model.InvoiceBill=ds.Tables[0].Rows[0]["InvoiceBill"].ToString();
				model.InvoiceUnit=ds.Tables[0].Rows[0]["InvoiceUnit"].ToString();
				model.RemitterUnit=ds.Tables[0].Rows[0]["RemitterUnit"].ToString();
				model.BankName=ds.Tables[0].Rows[0]["BankName"].ToString();
				model.BankAccount=ds.Tables[0].Rows[0]["BankAccount"].ToString();
				model.ChequeBill=ds.Tables[0].Rows[0]["ChequeBill"].ToString();
				if(ds.Tables[0].Rows[0]["RenderType"].ToString()!="")
				{
					model.RenderType=int.Parse(ds.Tables[0].Rows[0]["RenderType"].ToString());
				}
				if(ds.Tables[0].Rows[0]["RenderCustID"].ToString()!="")
				{
					model.RenderCustID=long.Parse(ds.Tables[0].Rows[0]["RenderCustID"].ToString());
				}
				model.RenderCustName=ds.Tables[0].Rows[0]["RenderCustName"].ToString();
				if(ds.Tables[0].Rows[0]["IsRefer"].ToString()!="")
				{
					model.IsRefer=int.Parse(ds.Tables[0].Rows[0]["IsRefer"].ToString());
				}
				model.ReferReason=ds.Tables[0].Rows[0]["ReferReason"].ToString();
				model.ReferUserCode=ds.Tables[0].Rows[0]["ReferUserCode"].ToString();
				if(ds.Tables[0].Rows[0]["ReferDate"].ToString()!="")
				{
					model.ReferDate=DateTime.Parse(ds.Tables[0].Rows[0]["ReferDate"].ToString());
				}
				model.AuditUserCode=ds.Tables[0].Rows[0]["AuditUserCode"].ToString();
				if(ds.Tables[0].Rows[0]["AuditDate"].ToString()!="")
				{
					model.AuditDate=DateTime.Parse(ds.Tables[0].Rows[0]["AuditDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsAudit"].ToString()!="")
				{
					model.IsAudit=int.Parse(ds.Tables[0].Rows[0]["IsAudit"].ToString());
				}
				if(ds.Tables[0].Rows[0]["BillTypeID"].ToString()!="")
				{
					model.BillTypeID=long.Parse(ds.Tables[0].Rows[0]["BillTypeID"].ToString());
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
			strSql.Append("SELECT * ");
                  strSql.Append(" FROM view_HSPR_FeesReceipts_Filter ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" WHERE "+strWhere);
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
            strSql.Append(" FROM view_HSPR_FeesReceipts_Filter ");
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
			parameters[5].Value = "SELECT * FROM Tb_HSPR_FeesReceipts WHERE 1=1 " + StrCondition;
			parameters[6].Value = "ReceID";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法

            #region 收据是否已经审核
            //收据是否已经审核
            public bool FeesReceiptsIsCheck(long ReceID)
            {
                  bool IsOK = false;
                  int MaxID = 1;
                  SqlParameter[] parameters = {
					new SqlParameter("@ReceID", SqlDbType.BigInt)
                                              };
                  parameters[0].Value = ReceID;

                  DataTable dTable = DbHelperSQL.RunProcedure("Proc_HSPR_FeesReceipts_IsCheck", parameters, "RetDataSet").Tables[0];

                  if (dTable.Rows.Count > 0)
                  {
                        try
                        {
                              MaxID = Convert.ToInt32(dTable.Rows[0][0].ToString());
                        }
                        catch
                        {

                        }
                  }

                  if (MaxID == 1)
                  {
                        IsOK = true;
                  }
                  else
                  {
                        IsOK = false;
                  }

                  return IsOK;
            }

            #endregion


            #region 收据是否可以撤销

            public string FeesReceiptsCheckRepeal(long ReceID)
            {
                  string Result = "";
                  SqlParameter[] parameters = {
					new SqlParameter("@ReceID", SqlDbType.BigInt),
                              new SqlParameter("@ErrorMsg", SqlDbType.VarChar, 200,ParameterDirection.Output, false, 0, 0,string.Empty, DataRowVersion.Default, null),
                                              };
                  parameters[0].Value = ReceID;

                  DbHelperSQL.RunProcedure("Proc_HSPR_FeesReceipts_CheckRepeal", parameters, "RetDataSet");

                  Result = parameters[1].Value.ToString();

                  return Result;

            }
            #endregion

            public void FeesReceiptDelete(long ReceID)
            {
                  SqlParameter[] parameters = {
					new SqlParameter("@ReceID", SqlDbType.BigInt)
                                              };
                  parameters[0].Value = ReceID;
                  DbHelperSQL.RunProcedure("Proc_HSPR_FeesReceipts_Delete", parameters, "RetDataSet");

            }

            public void FeesReceiptsUpdateAudit(long ReceID, int IsAudit, string AuditUserCode, string LoginRoles)
            {
                  SqlParameter[] parameters = {
					new SqlParameter("@ReceID", SqlDbType.BigInt),
                              new SqlParameter("@IsAudit", SqlDbType.Int),
                              new SqlParameter("@AuditUserCode", SqlDbType.VarChar,50),
                              new SqlParameter("@AuditUserRoles", SqlDbType.VarChar,1500)
                                              };
                  parameters[0].Value = ReceID;
                  parameters[1].Value = IsAudit;
                  parameters[2].Value = AuditUserCode;
                  parameters[3].Value = LoginRoles;
                  DbHelperSQL.RunProcedure("Proc_HSPR_FeesReceipts_UpdateAudit", parameters, "RetDataSet");

            }

      }
}

