using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.HSPR
{
	/// <summary>
	/// 数据访问类Dal_Tb_HSPR_Fees。
	/// </summary>
	public class Dal_Tb_HSPR_Fees
	{
		public Dal_Tb_HSPR_Fees()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public long GetMaxId()
		{
		return DbHelperSQL.GetMaxID("FeesID", "Tb_HSPR_Fees"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long FeesID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@FeesID", SqlDbType.BigInt)};
			parameters[0].Value = FeesID;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_HSPR_Fees_Exists",parameters,out rowsAffected);
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
		public void Add(MobileSoft.Model.HSPR.Tb_HSPR_Fees model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@FeesID", SqlDbType.BigInt,8),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@CostID", SqlDbType.BigInt,8),
					new SqlParameter("@CustID", SqlDbType.BigInt,8),
					new SqlParameter("@RoomID", SqlDbType.BigInt,8),
					new SqlParameter("@FeesDueDate", SqlDbType.DateTime),
					new SqlParameter("@FeesStateDate", SqlDbType.DateTime),
					new SqlParameter("@FeesEndDate", SqlDbType.DateTime),
					new SqlParameter("@DueAmount", SqlDbType.Decimal,9),
					new SqlParameter("@DebtsAmount", SqlDbType.Decimal,9),
					new SqlParameter("@WaivAmount", SqlDbType.Decimal,9),
					new SqlParameter("@PrecAmount", SqlDbType.Decimal,9),
					new SqlParameter("@PaidAmount", SqlDbType.Decimal,9),
					new SqlParameter("@RefundAmount", SqlDbType.Decimal,9),
					new SqlParameter("@IsAudit", SqlDbType.SmallInt,2),
					new SqlParameter("@FeesMemo", SqlDbType.NVarChar,255),
					new SqlParameter("@AccountFlag", SqlDbType.Int,4),
					new SqlParameter("@IsBank", SqlDbType.SmallInt,2),
					new SqlParameter("@IsCharge", SqlDbType.SmallInt,2),
					new SqlParameter("@IsFreeze", SqlDbType.SmallInt,2),
					new SqlParameter("@IsProperty", SqlDbType.SmallInt,2),
					new SqlParameter("@CorpStanID", SqlDbType.BigInt,8),
					new SqlParameter("@StanID", SqlDbType.BigInt,8),
					new SqlParameter("@OwnerFeesID", SqlDbType.BigInt,8),
					new SqlParameter("@AccountsDueDate", SqlDbType.DateTime),
					new SqlParameter("@HandID", SqlDbType.BigInt,8),
					new SqlParameter("@MeterSign", SqlDbType.NVarChar,10),
					new SqlParameter("@CalcAmount", SqlDbType.Decimal,9),
					new SqlParameter("@CalcAmount2", SqlDbType.Decimal,9),
					new SqlParameter("@IncidentID", SqlDbType.BigInt,8),
					new SqlParameter("@LeaseContID", SqlDbType.BigInt,8),
					new SqlParameter("@ContID", SqlDbType.BigInt,8),
					new SqlParameter("@StanMemo", SqlDbType.NVarChar,200),
					new SqlParameter("@CommisionCostID", SqlDbType.BigInt,8),
					new SqlParameter("@CommisionAmount", SqlDbType.Decimal,9),
					new SqlParameter("@WaivCommisAmount", SqlDbType.Decimal,9),
					new SqlParameter("@PerStanAmount", SqlDbType.Decimal,9),
					new SqlParameter("@IsPrec", SqlDbType.SmallInt,2),
					new SqlParameter("@ParkID", SqlDbType.BigInt,8),
					new SqlParameter("@CarparkID", SqlDbType.BigInt,8),
					new SqlParameter("@MeterID", SqlDbType.BigInt,8),
					new SqlParameter("@PMeterID", SqlDbType.BigInt,8),
					new SqlParameter("@FeesSynchCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@SynchFlag", SqlDbType.SmallInt,2),
					new SqlParameter("@BedID", SqlDbType.BigInt,8),
					new SqlParameter("@RoomState", SqlDbType.Int,4),
					new SqlParameter("@OrderCode", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.FeesID;
			parameters[1].Value = model.CommID;
			parameters[2].Value = model.CostID;
			parameters[3].Value = model.CustID;
			parameters[4].Value = model.RoomID;
			parameters[5].Value = model.FeesDueDate;
			parameters[6].Value = model.FeesStateDate;
			parameters[7].Value = model.FeesEndDate;
			parameters[8].Value = model.DueAmount;
			parameters[9].Value = model.DebtsAmount;
			parameters[10].Value = model.WaivAmount;
			parameters[11].Value = model.PrecAmount;
			parameters[12].Value = model.PaidAmount;
			parameters[13].Value = model.RefundAmount;
			parameters[14].Value = model.IsAudit;
			parameters[15].Value = model.FeesMemo;
			parameters[16].Value = model.AccountFlag;
			parameters[17].Value = model.IsBank;
			parameters[18].Value = model.IsCharge;
			parameters[19].Value = model.IsFreeze;
			parameters[20].Value = model.IsProperty;
			parameters[21].Value = model.CorpStanID;
			parameters[22].Value = model.StanID;
			parameters[23].Value = model.OwnerFeesID;
			parameters[24].Value = model.AccountsDueDate;
			parameters[25].Value = model.HandID;
			parameters[26].Value = model.MeterSign;
			parameters[27].Value = model.CalcAmount;
			parameters[28].Value = model.CalcAmount2;
			parameters[29].Value = model.IncidentID;
			parameters[30].Value = model.LeaseContID;
			parameters[31].Value = model.ContID;
			parameters[32].Value = model.StanMemo;
			parameters[33].Value = model.CommisionCostID;
			parameters[34].Value = model.CommisionAmount;
			parameters[35].Value = model.WaivCommisAmount;
			parameters[36].Value = model.PerStanAmount;
			parameters[37].Value = model.IsPrec;
			parameters[38].Value = model.ParkID;
			parameters[39].Value = model.CarparkID;
			parameters[40].Value = model.MeterID;
			parameters[41].Value = model.PMeterID;
			parameters[42].Value = model.FeesSynchCode;
			parameters[43].Value = model.SynchFlag;
			parameters[44].Value = model.BedID;
			parameters[45].Value = model.RoomState;
			parameters[46].Value = model.OrderCode;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_Fees_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.HSPR.Tb_HSPR_Fees model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@FeesID", SqlDbType.BigInt,8),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@CostID", SqlDbType.BigInt,8),
					new SqlParameter("@CustID", SqlDbType.BigInt,8),
					new SqlParameter("@RoomID", SqlDbType.BigInt,8),
					new SqlParameter("@FeesDueDate", SqlDbType.DateTime),
					new SqlParameter("@FeesStateDate", SqlDbType.DateTime),
					new SqlParameter("@FeesEndDate", SqlDbType.DateTime),
					new SqlParameter("@DueAmount", SqlDbType.Decimal,9),
					new SqlParameter("@DebtsAmount", SqlDbType.Decimal,9),
					new SqlParameter("@WaivAmount", SqlDbType.Decimal,9),
					new SqlParameter("@PrecAmount", SqlDbType.Decimal,9),
					new SqlParameter("@PaidAmount", SqlDbType.Decimal,9),
					new SqlParameter("@RefundAmount", SqlDbType.Decimal,9),
					new SqlParameter("@IsAudit", SqlDbType.SmallInt,2),
					new SqlParameter("@FeesMemo", SqlDbType.NVarChar,255),
					new SqlParameter("@AccountFlag", SqlDbType.Int,4),
					new SqlParameter("@IsBank", SqlDbType.SmallInt,2),
					new SqlParameter("@IsCharge", SqlDbType.SmallInt,2),
					new SqlParameter("@IsFreeze", SqlDbType.SmallInt,2),
					new SqlParameter("@IsProperty", SqlDbType.SmallInt,2),
					new SqlParameter("@CorpStanID", SqlDbType.BigInt,8),
					new SqlParameter("@StanID", SqlDbType.BigInt,8),
					new SqlParameter("@OwnerFeesID", SqlDbType.BigInt,8),
					new SqlParameter("@AccountsDueDate", SqlDbType.DateTime),
					new SqlParameter("@HandID", SqlDbType.BigInt,8),
					new SqlParameter("@MeterSign", SqlDbType.NVarChar,10),
					new SqlParameter("@CalcAmount", SqlDbType.Decimal,9),
					new SqlParameter("@CalcAmount2", SqlDbType.Decimal,9),
					new SqlParameter("@IncidentID", SqlDbType.BigInt,8),
					new SqlParameter("@LeaseContID", SqlDbType.BigInt,8),
					new SqlParameter("@ContID", SqlDbType.BigInt,8),
					new SqlParameter("@StanMemo", SqlDbType.NVarChar,200),
					new SqlParameter("@CommisionCostID", SqlDbType.BigInt,8),
					new SqlParameter("@CommisionAmount", SqlDbType.Decimal,9),
					new SqlParameter("@WaivCommisAmount", SqlDbType.Decimal,9),
					new SqlParameter("@PerStanAmount", SqlDbType.Decimal,9),
					new SqlParameter("@IsPrec", SqlDbType.SmallInt,2),
					new SqlParameter("@ParkID", SqlDbType.BigInt,8),
					new SqlParameter("@CarparkID", SqlDbType.BigInt,8),
					new SqlParameter("@MeterID", SqlDbType.BigInt,8),
					new SqlParameter("@PMeterID", SqlDbType.BigInt,8),
					new SqlParameter("@FeesSynchCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@SynchFlag", SqlDbType.SmallInt,2),
					new SqlParameter("@BedID", SqlDbType.BigInt,8),
					new SqlParameter("@RoomState", SqlDbType.Int,4),
					new SqlParameter("@OrderCode", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.FeesID;
			parameters[1].Value = model.CommID;
			parameters[2].Value = model.CostID;
			parameters[3].Value = model.CustID;
			parameters[4].Value = model.RoomID;
			parameters[5].Value = model.FeesDueDate;
			parameters[6].Value = model.FeesStateDate;
			parameters[7].Value = model.FeesEndDate;
			parameters[8].Value = model.DueAmount;
			parameters[9].Value = model.DebtsAmount;
			parameters[10].Value = model.WaivAmount;
			parameters[11].Value = model.PrecAmount;
			parameters[12].Value = model.PaidAmount;
			parameters[13].Value = model.RefundAmount;
			parameters[14].Value = model.IsAudit;
			parameters[15].Value = model.FeesMemo;
			parameters[16].Value = model.AccountFlag;
			parameters[17].Value = model.IsBank;
			parameters[18].Value = model.IsCharge;
			parameters[19].Value = model.IsFreeze;
			parameters[20].Value = model.IsProperty;
			parameters[21].Value = model.CorpStanID;
			parameters[22].Value = model.StanID;
			parameters[23].Value = model.OwnerFeesID;
			parameters[24].Value = model.AccountsDueDate;
			parameters[25].Value = model.HandID;
			parameters[26].Value = model.MeterSign;
			parameters[27].Value = model.CalcAmount;
			parameters[28].Value = model.CalcAmount2;
			parameters[29].Value = model.IncidentID;
			parameters[30].Value = model.LeaseContID;
			parameters[31].Value = model.ContID;
			parameters[32].Value = model.StanMemo;
			parameters[33].Value = model.CommisionCostID;
			parameters[34].Value = model.CommisionAmount;
			parameters[35].Value = model.WaivCommisAmount;
			parameters[36].Value = model.PerStanAmount;
			parameters[37].Value = model.IsPrec;
			parameters[38].Value = model.ParkID;
			parameters[39].Value = model.CarparkID;
			parameters[40].Value = model.MeterID;
			parameters[41].Value = model.PMeterID;
			parameters[42].Value = model.FeesSynchCode;
			parameters[43].Value = model.SynchFlag;
			parameters[44].Value = model.BedID;
			parameters[45].Value = model.RoomState;
			parameters[46].Value = model.OrderCode;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_Fees_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long FeesID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@FeesID", SqlDbType.BigInt)};
			parameters[0].Value = FeesID;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_Fees_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_Fees GetModel(long FeesID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@FeesID", SqlDbType.BigInt)};
			parameters[0].Value = FeesID;

			MobileSoft.Model.HSPR.Tb_HSPR_Fees model=new MobileSoft.Model.HSPR.Tb_HSPR_Fees();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_HSPR_Fees_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["FeesID"].ToString()!="")
				{
					model.FeesID=long.Parse(ds.Tables[0].Rows[0]["FeesID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CommID"].ToString()!="")
				{
					model.CommID=int.Parse(ds.Tables[0].Rows[0]["CommID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CostID"].ToString()!="")
				{
					model.CostID=long.Parse(ds.Tables[0].Rows[0]["CostID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CustID"].ToString()!="")
				{
					model.CustID=long.Parse(ds.Tables[0].Rows[0]["CustID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["RoomID"].ToString()!="")
				{
					model.RoomID=long.Parse(ds.Tables[0].Rows[0]["RoomID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["FeesDueDate"].ToString()!="")
				{
					model.FeesDueDate=DateTime.Parse(ds.Tables[0].Rows[0]["FeesDueDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["FeesStateDate"].ToString()!="")
				{
					model.FeesStateDate=DateTime.Parse(ds.Tables[0].Rows[0]["FeesStateDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["FeesEndDate"].ToString()!="")
				{
					model.FeesEndDate=DateTime.Parse(ds.Tables[0].Rows[0]["FeesEndDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["DueAmount"].ToString()!="")
				{
					model.DueAmount=decimal.Parse(ds.Tables[0].Rows[0]["DueAmount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["DebtsAmount"].ToString()!="")
				{
					model.DebtsAmount=decimal.Parse(ds.Tables[0].Rows[0]["DebtsAmount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["WaivAmount"].ToString()!="")
				{
					model.WaivAmount=decimal.Parse(ds.Tables[0].Rows[0]["WaivAmount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["PrecAmount"].ToString()!="")
				{
					model.PrecAmount=decimal.Parse(ds.Tables[0].Rows[0]["PrecAmount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["PaidAmount"].ToString()!="")
				{
					model.PaidAmount=decimal.Parse(ds.Tables[0].Rows[0]["PaidAmount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["RefundAmount"].ToString()!="")
				{
					model.RefundAmount=decimal.Parse(ds.Tables[0].Rows[0]["RefundAmount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsAudit"].ToString()!="")
				{
					model.IsAudit=int.Parse(ds.Tables[0].Rows[0]["IsAudit"].ToString());
				}
				model.FeesMemo=ds.Tables[0].Rows[0]["FeesMemo"].ToString();
				if(ds.Tables[0].Rows[0]["AccountFlag"].ToString()!="")
				{
					model.AccountFlag=int.Parse(ds.Tables[0].Rows[0]["AccountFlag"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsBank"].ToString()!="")
				{
					model.IsBank=int.Parse(ds.Tables[0].Rows[0]["IsBank"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsCharge"].ToString()!="")
				{
					model.IsCharge=int.Parse(ds.Tables[0].Rows[0]["IsCharge"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsFreeze"].ToString()!="")
				{
					model.IsFreeze=int.Parse(ds.Tables[0].Rows[0]["IsFreeze"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsProperty"].ToString()!="")
				{
					model.IsProperty=int.Parse(ds.Tables[0].Rows[0]["IsProperty"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CorpStanID"].ToString()!="")
				{
					model.CorpStanID=long.Parse(ds.Tables[0].Rows[0]["CorpStanID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["StanID"].ToString()!="")
				{
					model.StanID=long.Parse(ds.Tables[0].Rows[0]["StanID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["OwnerFeesID"].ToString()!="")
				{
					model.OwnerFeesID=long.Parse(ds.Tables[0].Rows[0]["OwnerFeesID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["AccountsDueDate"].ToString()!="")
				{
					model.AccountsDueDate=DateTime.Parse(ds.Tables[0].Rows[0]["AccountsDueDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["HandID"].ToString()!="")
				{
					model.HandID=long.Parse(ds.Tables[0].Rows[0]["HandID"].ToString());
				}
				model.MeterSign=ds.Tables[0].Rows[0]["MeterSign"].ToString();
				if(ds.Tables[0].Rows[0]["CalcAmount"].ToString()!="")
				{
					model.CalcAmount=decimal.Parse(ds.Tables[0].Rows[0]["CalcAmount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CalcAmount2"].ToString()!="")
				{
					model.CalcAmount2=decimal.Parse(ds.Tables[0].Rows[0]["CalcAmount2"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IncidentID"].ToString()!="")
				{
					model.IncidentID=long.Parse(ds.Tables[0].Rows[0]["IncidentID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["LeaseContID"].ToString()!="")
				{
					model.LeaseContID=long.Parse(ds.Tables[0].Rows[0]["LeaseContID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ContID"].ToString()!="")
				{
					model.ContID=long.Parse(ds.Tables[0].Rows[0]["ContID"].ToString());
				}
				model.StanMemo=ds.Tables[0].Rows[0]["StanMemo"].ToString();
				if(ds.Tables[0].Rows[0]["CommisionCostID"].ToString()!="")
				{
					model.CommisionCostID=long.Parse(ds.Tables[0].Rows[0]["CommisionCostID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CommisionAmount"].ToString()!="")
				{
					model.CommisionAmount=decimal.Parse(ds.Tables[0].Rows[0]["CommisionAmount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["WaivCommisAmount"].ToString()!="")
				{
					model.WaivCommisAmount=decimal.Parse(ds.Tables[0].Rows[0]["WaivCommisAmount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["PerStanAmount"].ToString()!="")
				{
					model.PerStanAmount=decimal.Parse(ds.Tables[0].Rows[0]["PerStanAmount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsPrec"].ToString()!="")
				{
					model.IsPrec=int.Parse(ds.Tables[0].Rows[0]["IsPrec"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ParkID"].ToString()!="")
				{
					model.ParkID=long.Parse(ds.Tables[0].Rows[0]["ParkID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CarparkID"].ToString()!="")
				{
					model.CarparkID=long.Parse(ds.Tables[0].Rows[0]["CarparkID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["MeterID"].ToString()!="")
				{
					model.MeterID=long.Parse(ds.Tables[0].Rows[0]["MeterID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["PMeterID"].ToString()!="")
				{
					model.PMeterID=long.Parse(ds.Tables[0].Rows[0]["PMeterID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["FeesSynchCode"].ToString()!="")
				{
					model.FeesSynchCode=new Guid(ds.Tables[0].Rows[0]["FeesSynchCode"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SynchFlag"].ToString()!="")
				{
					model.SynchFlag=int.Parse(ds.Tables[0].Rows[0]["SynchFlag"].ToString());
				}
				if(ds.Tables[0].Rows[0]["BedID"].ToString()!="")
				{
					model.BedID=long.Parse(ds.Tables[0].Rows[0]["BedID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["RoomState"].ToString()!="")
				{
					model.RoomState=int.Parse(ds.Tables[0].Rows[0]["RoomState"].ToString());
				}
				model.OrderCode=ds.Tables[0].Rows[0]["OrderCode"].ToString();
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
			strSql.Append("select FeesID,CommID,CostID,CustID,RoomID,FeesDueDate,FeesStateDate,FeesEndDate,DueAmount,DebtsAmount,WaivAmount,PrecAmount,PaidAmount,RefundAmount,IsAudit,FeesMemo,AccountFlag,IsBank,IsCharge,IsFreeze,IsProperty,CorpStanID,StanID,OwnerFeesID,AccountsDueDate,HandID,MeterSign,CalcAmount,CalcAmount2,IncidentID,LeaseContID,ContID,StanMemo,CommisionCostID,CommisionAmount,WaivCommisAmount,PerStanAmount,IsPrec,ParkID,CarparkID,MeterID,PMeterID,FeesSynchCode,SynchFlag,BedID,RoomState,OrderCode ");
			strSql.Append(" FROM Tb_HSPR_Fees ");
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
			strSql.Append(" FeesID,CommID,CostID,CustID,RoomID,FeesDueDate,FeesStateDate,FeesEndDate,DueAmount,DebtsAmount,WaivAmount,PrecAmount,PaidAmount,RefundAmount,IsAudit,FeesMemo,AccountFlag,IsBank,IsCharge,IsFreeze,IsProperty,CorpStanID,StanID,OwnerFeesID,AccountsDueDate,HandID,MeterSign,CalcAmount,CalcAmount2,IncidentID,LeaseContID,ContID,StanMemo,CommisionCostID,CommisionAmount,WaivCommisAmount,PerStanAmount,IsPrec,ParkID,CarparkID,MeterID,PMeterID,FeesSynchCode,SynchFlag,BedID,RoomState,OrderCode ");
			strSql.Append(" FROM Tb_HSPR_Fees ");
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
			parameters[5].Value = "SELECT * FROM Tb_HSPR_Fees WHERE 1=1 " + StrCondition;
			parameters[6].Value = "FeesID";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

