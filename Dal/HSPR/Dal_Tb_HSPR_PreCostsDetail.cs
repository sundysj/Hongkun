using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.HSPR
{
	/// <summary>
	/// 数据访问类Dal_Tb_HSPR_PreCostsDetail。
	/// </summary>
	public class Dal_Tb_HSPR_PreCostsDetail
	{
		public Dal_Tb_HSPR_PreCostsDetail()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long RecdID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@RecdID", SqlDbType.BigInt)};
			parameters[0].Value = RecdID;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_HSPR_PreCostsDetail_Exists",parameters,out rowsAffected);
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
		public void Add(MobileSoft.Model.HSPR.Tb_HSPR_PreCostsDetail model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@RecdID", SqlDbType.BigInt,8),
					new SqlParameter("@PrecID", SqlDbType.BigInt,8),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@CustID", SqlDbType.BigInt,8),
					new SqlParameter("@RoomID", SqlDbType.BigInt,8),
					new SqlParameter("@CostID", SqlDbType.BigInt,8),
					new SqlParameter("@PrecAmount", SqlDbType.Decimal,9),
					new SqlParameter("@PrecDate", SqlDbType.DateTime),
					new SqlParameter("@PrecMemo", SqlDbType.NVarChar,200),
					new SqlParameter("@UserCode", SqlDbType.NVarChar,20),
					new SqlParameter("@BillsSign", SqlDbType.NVarChar,20),
					new SqlParameter("@ChargeMode", SqlDbType.NVarChar,20),
					new SqlParameter("@AccountWay", SqlDbType.SmallInt,2),
					new SqlParameter("@IsAudit", SqlDbType.SmallInt,2),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2),
					new SqlParameter("@ReceID", SqlDbType.BigInt,8),
					new SqlParameter("@OldPrecAmount", SqlDbType.Decimal,9),
					new SqlParameter("@NewPrecAmount", SqlDbType.Decimal,9),
					new SqlParameter("@FeesID", SqlDbType.BigInt,8),
					new SqlParameter("@SourceType", SqlDbType.SmallInt,2),
					new SqlParameter("@FeesDueDate", SqlDbType.DateTime),
					new SqlParameter("@IsProperty", SqlDbType.SmallInt,2),
					new SqlParameter("@CommisionCostID", SqlDbType.BigInt,8),
					new SqlParameter("@DueAmount", SqlDbType.Decimal,9),
					new SqlParameter("@WaivAmount", SqlDbType.Decimal,9),
					new SqlParameter("@CommisionAmount", SqlDbType.Decimal,9),
					new SqlParameter("@WaivCommisAmount", SqlDbType.Decimal,9),
					new SqlParameter("@HandID", SqlDbType.BigInt,8),
					new SqlParameter("@CarParkID", SqlDbType.BigInt,8),
					new SqlParameter("@ParkID", SqlDbType.BigInt,8),
					new SqlParameter("@CostIDs", SqlDbType.NText),
					new SqlParameter("@CostNames", SqlDbType.NText),
					new SqlParameter("@HandIDs", SqlDbType.NText),
					new SqlParameter("@ParkNames", SqlDbType.NText)};
			parameters[0].Value = model.RecdID;
			parameters[1].Value = model.PrecID;
			parameters[2].Value = model.CommID;
			parameters[3].Value = model.CustID;
			parameters[4].Value = model.RoomID;
			parameters[5].Value = model.CostID;
			parameters[6].Value = model.PrecAmount;
			parameters[7].Value = model.PrecDate;
			parameters[8].Value = model.PrecMemo;
			parameters[9].Value = model.UserCode;
			parameters[10].Value = model.BillsSign;
			parameters[11].Value = model.ChargeMode;
			parameters[12].Value = model.AccountWay;
			parameters[13].Value = model.IsAudit;
			parameters[14].Value = model.IsDelete;
			parameters[15].Value = model.ReceID;
			parameters[16].Value = model.OldPrecAmount;
			parameters[17].Value = model.NewPrecAmount;
			parameters[18].Value = model.FeesID;
			parameters[19].Value = model.SourceType;
			parameters[20].Value = model.FeesDueDate;
			parameters[21].Value = model.IsProperty;
			parameters[22].Value = model.CommisionCostID;
			parameters[23].Value = model.DueAmount;
			parameters[24].Value = model.WaivAmount;
			parameters[25].Value = model.CommisionAmount;
			parameters[26].Value = model.WaivCommisAmount;
			parameters[27].Value = model.HandID;
			parameters[28].Value = model.CarParkID;
			parameters[29].Value = model.ParkID;
			parameters[30].Value = model.CostIDs;
			parameters[31].Value = model.CostNames;
			parameters[32].Value = model.HandIDs;
			parameters[33].Value = model.ParkNames;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_PreCostsDetail_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.HSPR.Tb_HSPR_PreCostsDetail model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@RecdID", SqlDbType.BigInt,8),
					new SqlParameter("@PrecID", SqlDbType.BigInt,8),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@CustID", SqlDbType.BigInt,8),
					new SqlParameter("@RoomID", SqlDbType.BigInt,8),
					new SqlParameter("@CostID", SqlDbType.BigInt,8),
					new SqlParameter("@PrecAmount", SqlDbType.Decimal,9),
					new SqlParameter("@PrecDate", SqlDbType.DateTime),
					new SqlParameter("@PrecMemo", SqlDbType.NVarChar,200),
					new SqlParameter("@UserCode", SqlDbType.NVarChar,20),
					new SqlParameter("@BillsSign", SqlDbType.NVarChar,20),
					new SqlParameter("@ChargeMode", SqlDbType.NVarChar,20),
					new SqlParameter("@AccountWay", SqlDbType.SmallInt,2),
					new SqlParameter("@IsAudit", SqlDbType.SmallInt,2),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2),
					new SqlParameter("@ReceID", SqlDbType.BigInt,8),
					new SqlParameter("@OldPrecAmount", SqlDbType.Decimal,9),
					new SqlParameter("@NewPrecAmount", SqlDbType.Decimal,9),
					new SqlParameter("@FeesID", SqlDbType.BigInt,8),
					new SqlParameter("@SourceType", SqlDbType.SmallInt,2),
					new SqlParameter("@FeesDueDate", SqlDbType.DateTime),
					new SqlParameter("@IsProperty", SqlDbType.SmallInt,2),
					new SqlParameter("@CommisionCostID", SqlDbType.BigInt,8),
					new SqlParameter("@DueAmount", SqlDbType.Decimal,9),
					new SqlParameter("@WaivAmount", SqlDbType.Decimal,9),
					new SqlParameter("@CommisionAmount", SqlDbType.Decimal,9),
					new SqlParameter("@WaivCommisAmount", SqlDbType.Decimal,9),
					new SqlParameter("@HandID", SqlDbType.BigInt,8),
					new SqlParameter("@CarParkID", SqlDbType.BigInt,8),
					new SqlParameter("@ParkID", SqlDbType.BigInt,8),
					new SqlParameter("@CostIDs", SqlDbType.NText),
					new SqlParameter("@CostNames", SqlDbType.NText),
					new SqlParameter("@HandIDs", SqlDbType.NText),
					new SqlParameter("@ParkNames", SqlDbType.NText)};
			parameters[0].Value = model.RecdID;
			parameters[1].Value = model.PrecID;
			parameters[2].Value = model.CommID;
			parameters[3].Value = model.CustID;
			parameters[4].Value = model.RoomID;
			parameters[5].Value = model.CostID;
			parameters[6].Value = model.PrecAmount;
			parameters[7].Value = model.PrecDate;
			parameters[8].Value = model.PrecMemo;
			parameters[9].Value = model.UserCode;
			parameters[10].Value = model.BillsSign;
			parameters[11].Value = model.ChargeMode;
			parameters[12].Value = model.AccountWay;
			parameters[13].Value = model.IsAudit;
			parameters[14].Value = model.IsDelete;
			parameters[15].Value = model.ReceID;
			parameters[16].Value = model.OldPrecAmount;
			parameters[17].Value = model.NewPrecAmount;
			parameters[18].Value = model.FeesID;
			parameters[19].Value = model.SourceType;
			parameters[20].Value = model.FeesDueDate;
			parameters[21].Value = model.IsProperty;
			parameters[22].Value = model.CommisionCostID;
			parameters[23].Value = model.DueAmount;
			parameters[24].Value = model.WaivAmount;
			parameters[25].Value = model.CommisionAmount;
			parameters[26].Value = model.WaivCommisAmount;
			parameters[27].Value = model.HandID;
			parameters[28].Value = model.CarParkID;
			parameters[29].Value = model.ParkID;
			parameters[30].Value = model.CostIDs;
			parameters[31].Value = model.CostNames;
			parameters[32].Value = model.HandIDs;
			parameters[33].Value = model.ParkNames;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_PreCostsDetail_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long RecdID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@RecdID", SqlDbType.BigInt)};
			parameters[0].Value = RecdID;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_PreCostsDetail_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_PreCostsDetail GetModel(long RecdID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@RecdID", SqlDbType.BigInt)};
			parameters[0].Value = RecdID;

			MobileSoft.Model.HSPR.Tb_HSPR_PreCostsDetail model=new MobileSoft.Model.HSPR.Tb_HSPR_PreCostsDetail();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_HSPR_PreCostsDetail_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["RecdID"].ToString()!="")
				{
					model.RecdID=long.Parse(ds.Tables[0].Rows[0]["RecdID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["PrecID"].ToString()!="")
				{
					model.PrecID=long.Parse(ds.Tables[0].Rows[0]["PrecID"].ToString());
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
				if(ds.Tables[0].Rows[0]["PrecAmount"].ToString()!="")
				{
					model.PrecAmount=decimal.Parse(ds.Tables[0].Rows[0]["PrecAmount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["PrecDate"].ToString()!="")
				{
					model.PrecDate=DateTime.Parse(ds.Tables[0].Rows[0]["PrecDate"].ToString());
				}
				model.PrecMemo=ds.Tables[0].Rows[0]["PrecMemo"].ToString();
				model.UserCode=ds.Tables[0].Rows[0]["UserCode"].ToString();
				model.BillsSign=ds.Tables[0].Rows[0]["BillsSign"].ToString();
				model.ChargeMode=ds.Tables[0].Rows[0]["ChargeMode"].ToString();
				if(ds.Tables[0].Rows[0]["AccountWay"].ToString()!="")
				{
					model.AccountWay=int.Parse(ds.Tables[0].Rows[0]["AccountWay"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsAudit"].ToString()!="")
				{
					model.IsAudit=int.Parse(ds.Tables[0].Rows[0]["IsAudit"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsDelete"].ToString()!="")
				{
					model.IsDelete=int.Parse(ds.Tables[0].Rows[0]["IsDelete"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ReceID"].ToString()!="")
				{
					model.ReceID=long.Parse(ds.Tables[0].Rows[0]["ReceID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["OldPrecAmount"].ToString()!="")
				{
					model.OldPrecAmount=decimal.Parse(ds.Tables[0].Rows[0]["OldPrecAmount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["NewPrecAmount"].ToString()!="")
				{
					model.NewPrecAmount=decimal.Parse(ds.Tables[0].Rows[0]["NewPrecAmount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["FeesID"].ToString()!="")
				{
					model.FeesID=long.Parse(ds.Tables[0].Rows[0]["FeesID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SourceType"].ToString()!="")
				{
					model.SourceType=int.Parse(ds.Tables[0].Rows[0]["SourceType"].ToString());
				}
				if(ds.Tables[0].Rows[0]["FeesDueDate"].ToString()!="")
				{
					model.FeesDueDate=DateTime.Parse(ds.Tables[0].Rows[0]["FeesDueDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsProperty"].ToString()!="")
				{
					model.IsProperty=int.Parse(ds.Tables[0].Rows[0]["IsProperty"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CommisionCostID"].ToString()!="")
				{
					model.CommisionCostID=long.Parse(ds.Tables[0].Rows[0]["CommisionCostID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["DueAmount"].ToString()!="")
				{
					model.DueAmount=decimal.Parse(ds.Tables[0].Rows[0]["DueAmount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["WaivAmount"].ToString()!="")
				{
					model.WaivAmount=decimal.Parse(ds.Tables[0].Rows[0]["WaivAmount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CommisionAmount"].ToString()!="")
				{
					model.CommisionAmount=decimal.Parse(ds.Tables[0].Rows[0]["CommisionAmount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["WaivCommisAmount"].ToString()!="")
				{
					model.WaivCommisAmount=decimal.Parse(ds.Tables[0].Rows[0]["WaivCommisAmount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["HandID"].ToString()!="")
				{
					model.HandID=long.Parse(ds.Tables[0].Rows[0]["HandID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CarParkID"].ToString()!="")
				{
					model.CarParkID=long.Parse(ds.Tables[0].Rows[0]["CarParkID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ParkID"].ToString()!="")
				{
					model.ParkID=long.Parse(ds.Tables[0].Rows[0]["ParkID"].ToString());
				}
				model.CostIDs=ds.Tables[0].Rows[0]["CostIDs"].ToString();
				model.CostNames=ds.Tables[0].Rows[0]["CostNames"].ToString();
				model.HandIDs=ds.Tables[0].Rows[0]["HandIDs"].ToString();
				model.ParkNames=ds.Tables[0].Rows[0]["ParkNames"].ToString();
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
			strSql.Append("select RecdID,PrecID,CommID,CustID,RoomID,CostID,PrecAmount,PrecDate,PrecMemo,UserCode,BillsSign,ChargeMode,AccountWay,IsAudit,IsDelete,ReceID,OldPrecAmount,NewPrecAmount,FeesID,SourceType,FeesDueDate,IsProperty,CommisionCostID,DueAmount,WaivAmount,CommisionAmount,WaivCommisAmount,HandID,CarParkID,ParkID,CostIDs,CostNames,HandIDs,ParkNames ");
			strSql.Append(" FROM Tb_HSPR_PreCostsDetail ");
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
			strSql.Append(" RecdID,PrecID,CommID,CustID,RoomID,CostID,PrecAmount,PrecDate,PrecMemo,UserCode,BillsSign,ChargeMode,AccountWay,IsAudit,IsDelete,ReceID,OldPrecAmount,NewPrecAmount,FeesID,SourceType,FeesDueDate,IsProperty,CommisionCostID,DueAmount,WaivAmount,CommisionAmount,WaivCommisAmount,HandID,CarParkID,ParkID,CostIDs,CostNames,HandIDs,ParkNames ");
			strSql.Append(" FROM Tb_HSPR_PreCostsDetail ");
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
			parameters[5].Value = "SELECT * FROM Tb_HSPR_PreCostsDetail WHERE 1=1 " + StrCondition;
			parameters[6].Value = "RecdID";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法

            #region 撤销预收

            public void PreCostsDetailRepeal(long RecdID)
            {
                  SqlParameter[] parameters = {
					new SqlParameter("@RecdID", SqlDbType.BigInt)
                                              };
                  parameters[0].Value = RecdID;
                  DbHelperSQL.RunProcedure("Proc_HSPR_PreCostsDetail_Repeal", parameters, "RetDataSet");

            }
            #endregion

            public bool PreCostsDetailCheckRepeal(long RecdID)
            {
                  bool IsOK = false;
                  int MaxID = 1;

                  SqlParameter[] parameters = {
					new SqlParameter("@RecdID", SqlDbType.BigInt)
                                              };
                  parameters[0].Value = RecdID;

                  DataTable dTable = DbHelperSQL.RunProcedure("Proc_HSPR_PreCostsDetail_CheckRepeal", parameters, "RetDataSet").Tables[0];

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


            public void PreCostsDetailDelete(long RecdID)
            {
                  SqlParameter[] parameters = {
					new SqlParameter("@RecdID", SqlDbType.BigInt)
                                              };
                  parameters[0].Value = RecdID;
                  DbHelperSQL.RunProcedure("Proc_HSPR_PreCostsDetail_Delete", parameters, "RetDataSet");

            }
	}
}

