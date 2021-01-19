using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.HSPR
{
	/// <summary>
	/// 数据访问类Dal_Tb_HSPR_PreCostsReceipts。
	/// </summary>
	public class Dal_Tb_HSPR_PreCostsReceipts
	{
		public Dal_Tb_HSPR_PreCostsReceipts()
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

			int result= DbHelperSQL.RunProcedure("Proc_Tb_HSPR_PreCostsReceipts_Exists",parameters,out rowsAffected);
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
		public void Add(MobileSoft.Model.HSPR.Tb_HSPR_PreCostsReceipts model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ReceID", SqlDbType.BigInt,8),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@CustID", SqlDbType.BigInt,8),
					new SqlParameter("@RoomID", SqlDbType.BigInt,8),
					new SqlParameter("@BillsSign", SqlDbType.NVarChar,20),
					new SqlParameter("@PrecAmount", SqlDbType.Decimal,9),
					new SqlParameter("@PrintTimes", SqlDbType.Int,4),
					new SqlParameter("@BillsDate", SqlDbType.DateTime),
					new SqlParameter("@UserCode", SqlDbType.NVarChar,20),
					new SqlParameter("@ChargeMode", SqlDbType.NVarChar,20),
					new SqlParameter("@AccountWay", SqlDbType.SmallInt,2),
					new SqlParameter("@ReceMemo", SqlDbType.NVarChar,500),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2),
					new SqlParameter("@UseRepID", SqlDbType.BigInt,8),
					new SqlParameter("@InvoiceBill", SqlDbType.NVarChar,20),
					new SqlParameter("@InvoiceUnit", SqlDbType.NVarChar,100),
					new SqlParameter("@RemitterUnit", SqlDbType.NVarChar,100),
					new SqlParameter("@BankName", SqlDbType.NVarChar,50),
					new SqlParameter("@BankAccount", SqlDbType.NVarChar,30),
					new SqlParameter("@ChequeBill", SqlDbType.NVarChar,30),
					new SqlParameter("@BillTypeID", SqlDbType.BigInt,8),
					new SqlParameter("@SourceType", SqlDbType.SmallInt,2),
					new SqlParameter("@DrawMoneyMan", SqlDbType.NVarChar,50),
					new SqlParameter("@DrawIdentityCard", SqlDbType.NVarChar,50),
					new SqlParameter("@HandleMan", SqlDbType.NVarChar,50),
					new SqlParameter("@AcceptMan", SqlDbType.NVarChar,50),
					new SqlParameter("@IsRefer", SqlDbType.SmallInt,2),
					new SqlParameter("@ReferReason", SqlDbType.NVarChar,200),
					new SqlParameter("@ReferUserCode", SqlDbType.NVarChar,20),
					new SqlParameter("@ReferDate", SqlDbType.DateTime),
					new SqlParameter("@AuditUserCode", SqlDbType.NVarChar,20),
					new SqlParameter("@AuditDate", SqlDbType.DateTime),
					new SqlParameter("@IsAudit", SqlDbType.SmallInt,2)};
			parameters[0].Value = model.ReceID;
			parameters[1].Value = model.CommID;
			parameters[2].Value = model.CustID;
			parameters[3].Value = model.RoomID;
			parameters[4].Value = model.BillsSign;
			parameters[5].Value = model.PrecAmount;
			parameters[6].Value = model.PrintTimes;
			parameters[7].Value = model.BillsDate;
			parameters[8].Value = model.UserCode;
			parameters[9].Value = model.ChargeMode;
			parameters[10].Value = model.AccountWay;
			parameters[11].Value = model.ReceMemo;
			parameters[12].Value = model.IsDelete;
			parameters[13].Value = model.UseRepID;
			parameters[14].Value = model.InvoiceBill;
			parameters[15].Value = model.InvoiceUnit;
			parameters[16].Value = model.RemitterUnit;
			parameters[17].Value = model.BankName;
			parameters[18].Value = model.BankAccount;
			parameters[19].Value = model.ChequeBill;
			parameters[20].Value = model.BillTypeID;
			parameters[21].Value = model.SourceType;
			parameters[22].Value = model.DrawMoneyMan;
			parameters[23].Value = model.DrawIdentityCard;
			parameters[24].Value = model.HandleMan;
			parameters[25].Value = model.AcceptMan;
			parameters[26].Value = model.IsRefer;
			parameters[27].Value = model.ReferReason;
			parameters[28].Value = model.ReferUserCode;
			parameters[29].Value = model.ReferDate;
			parameters[30].Value = model.AuditUserCode;
			parameters[31].Value = model.AuditDate;
			parameters[32].Value = model.IsAudit;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_PreCostsReceipts_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.HSPR.Tb_HSPR_PreCostsReceipts model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ReceID", SqlDbType.BigInt,8),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@CustID", SqlDbType.BigInt,8),
					new SqlParameter("@RoomID", SqlDbType.BigInt,8),
					new SqlParameter("@BillsSign", SqlDbType.NVarChar,20),
					new SqlParameter("@PrecAmount", SqlDbType.Decimal,9),
					new SqlParameter("@PrintTimes", SqlDbType.Int,4),
					new SqlParameter("@BillsDate", SqlDbType.DateTime),
					new SqlParameter("@UserCode", SqlDbType.NVarChar,20),
					new SqlParameter("@ChargeMode", SqlDbType.NVarChar,20),
					new SqlParameter("@AccountWay", SqlDbType.SmallInt,2),
					new SqlParameter("@ReceMemo", SqlDbType.NVarChar,500),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2),
					new SqlParameter("@UseRepID", SqlDbType.BigInt,8),
					new SqlParameter("@InvoiceBill", SqlDbType.NVarChar,20),
					new SqlParameter("@InvoiceUnit", SqlDbType.NVarChar,100),
					new SqlParameter("@RemitterUnit", SqlDbType.NVarChar,100),
					new SqlParameter("@BankName", SqlDbType.NVarChar,50),
					new SqlParameter("@BankAccount", SqlDbType.NVarChar,30),
					new SqlParameter("@ChequeBill", SqlDbType.NVarChar,30),
					new SqlParameter("@BillTypeID", SqlDbType.BigInt,8),
					new SqlParameter("@SourceType", SqlDbType.SmallInt,2),
					new SqlParameter("@DrawMoneyMan", SqlDbType.NVarChar,50),
					new SqlParameter("@DrawIdentityCard", SqlDbType.NVarChar,50),
					new SqlParameter("@HandleMan", SqlDbType.NVarChar,50),
					new SqlParameter("@AcceptMan", SqlDbType.NVarChar,50),
					new SqlParameter("@IsRefer", SqlDbType.SmallInt,2),
					new SqlParameter("@ReferReason", SqlDbType.NVarChar,200),
					new SqlParameter("@ReferUserCode", SqlDbType.NVarChar,20),
					new SqlParameter("@ReferDate", SqlDbType.DateTime),
					new SqlParameter("@AuditUserCode", SqlDbType.NVarChar,20),
					new SqlParameter("@AuditDate", SqlDbType.DateTime),
					new SqlParameter("@IsAudit", SqlDbType.SmallInt,2)};
			parameters[0].Value = model.ReceID;
			parameters[1].Value = model.CommID;
			parameters[2].Value = model.CustID;
			parameters[3].Value = model.RoomID;
			parameters[4].Value = model.BillsSign;
			parameters[5].Value = model.PrecAmount;
			parameters[6].Value = model.PrintTimes;
			parameters[7].Value = model.BillsDate;
			parameters[8].Value = model.UserCode;
			parameters[9].Value = model.ChargeMode;
			parameters[10].Value = model.AccountWay;
			parameters[11].Value = model.ReceMemo;
			parameters[12].Value = model.IsDelete;
			parameters[13].Value = model.UseRepID;
			parameters[14].Value = model.InvoiceBill;
			parameters[15].Value = model.InvoiceUnit;
			parameters[16].Value = model.RemitterUnit;
			parameters[17].Value = model.BankName;
			parameters[18].Value = model.BankAccount;
			parameters[19].Value = model.ChequeBill;
			parameters[20].Value = model.BillTypeID;
			parameters[21].Value = model.SourceType;
			parameters[22].Value = model.DrawMoneyMan;
			parameters[23].Value = model.DrawIdentityCard;
			parameters[24].Value = model.HandleMan;
			parameters[25].Value = model.AcceptMan;
			parameters[26].Value = model.IsRefer;
			parameters[27].Value = model.ReferReason;
			parameters[28].Value = model.ReferUserCode;
			parameters[29].Value = model.ReferDate;
			parameters[30].Value = model.AuditUserCode;
			parameters[31].Value = model.AuditDate;
			parameters[32].Value = model.IsAudit;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_PreCostsReceipts_Update",parameters,out rowsAffected);
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

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_PreCostsReceipts_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_PreCostsReceipts GetModel(long ReceID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@ReceID", SqlDbType.BigInt)};
			parameters[0].Value = ReceID;

			MobileSoft.Model.HSPR.Tb_HSPR_PreCostsReceipts model=new MobileSoft.Model.HSPR.Tb_HSPR_PreCostsReceipts();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_HSPR_PreCostsReceipts_GetModel",parameters,"ds");
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
				if(ds.Tables[0].Rows[0]["PrecAmount"].ToString()!="")
				{
					model.PrecAmount=decimal.Parse(ds.Tables[0].Rows[0]["PrecAmount"].ToString());
				}
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
				if(ds.Tables[0].Rows[0]["BillTypeID"].ToString()!="")
				{
					model.BillTypeID=long.Parse(ds.Tables[0].Rows[0]["BillTypeID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SourceType"].ToString()!="")
				{
					model.SourceType=int.Parse(ds.Tables[0].Rows[0]["SourceType"].ToString());
				}
				model.DrawMoneyMan=ds.Tables[0].Rows[0]["DrawMoneyMan"].ToString();
				model.DrawIdentityCard=ds.Tables[0].Rows[0]["DrawIdentityCard"].ToString();
				model.HandleMan=ds.Tables[0].Rows[0]["HandleMan"].ToString();
				model.AcceptMan=ds.Tables[0].Rows[0]["AcceptMan"].ToString();
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
			strSql.Append(" FROM View_HSPR_PreCostsReceipts_Filter ");
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
            strSql.Append(" FROM View_HSPR_PreCostsReceipts_Filter ");
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
			parameters[5].Value = "SELECT * FROM Tb_HSPR_PreCostsReceipts WHERE 1=1 " + StrCondition;
			parameters[6].Value = "ReceID";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法

            public bool PreCostsReceiptsIsCheck(long ReceID)
            {
                  bool IsOK = false;
                  int MaxID = 1;

                  SqlParameter[] parameters = {
					new SqlParameter("@ReceID", SqlDbType.BigInt)
                                              };
                  parameters[0].Value = ReceID;

                  DataTable dTable = DbHelperSQL.RunProcedure("Proc_HSPR_PreCostsReceipts_IsCheck", parameters, "RetDataSet").Tables[0];

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

            public string FeesReceiptsCheckRepeal(long ReceID)
            {
                  string Result = "";
                  SqlParameter[] parameters = {
					new SqlParameter("@ReceID", SqlDbType.BigInt),
                              new SqlParameter("@ErrorMsg", SqlDbType.VarChar, 200,ParameterDirection.Output, false, 0, 0,string.Empty, DataRowVersion.Default, null),
                                              };
                  parameters[0].Value = ReceID;

                  DbHelperSQL.RunProcedure("Proc_HSPR_PreCostsReceipts_CheckRepeal", parameters, "RetDataSet");

                  Result = parameters[1].Value.ToString();

                  return Result;

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
                  DbHelperSQL.RunProcedure("Proc_HSPR_PreCostsReceipts_UpdateAudit", parameters, "RetDataSet");

            }

            public void PreCostsReceiptDelete(long ReceID)
            {
                  SqlParameter[] parameters = {
					new SqlParameter("@ReceID", SqlDbType.BigInt)
                                              };
                  parameters[0].Value = ReceID;
                  DbHelperSQL.RunProcedure("Proc_HSPR_PreCostsReceipts_Delete", parameters, "RetDataSet");

            }
	}
}

