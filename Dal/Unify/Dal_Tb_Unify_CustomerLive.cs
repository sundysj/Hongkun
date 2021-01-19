using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.Unify
{
	/// <summary>
	/// 数据访问类Dal_Tb_Unify_CustomerLive。
	/// </summary>
	public class Dal_Tb_Unify_CustomerLive
	{
		public Dal_Tb_Unify_CustomerLive()
		{
			DbHelperSQL.ConnectionString = PubConstant.SQIBSContionString;
		}
		#region  成员方法

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(Guid LiveSynchCode)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@LiveSynchCode", SqlDbType.UniqueIdentifier)};
			parameters[0].Value = LiveSynchCode;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_Unify_CustomerLive_Exists",parameters,out rowsAffected);
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
		public void Add(MobileSoft.Model.Unify.Tb_Unify_CustomerLive model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@LiveSynchCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@CommSynchCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@CustSynchCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@RoomSynchCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@UnLiveID", SqlDbType.BigInt,8),
					new SqlParameter("@LiveType", SqlDbType.SmallInt,2),
					new SqlParameter("@L_BroadbandNum", SqlDbType.NVarChar,30),
					new SqlParameter("@L_FixedTel", SqlDbType.NVarChar,50),
					new SqlParameter("@L_MobilePhone", SqlDbType.NVarChar,50),
					new SqlParameter("@L_IPTVAccount", SqlDbType.NVarChar,50),
					new SqlParameter("@SubmitTime", SqlDbType.DateTime),
					new SqlParameter("@FittingTime", SqlDbType.DateTime),
					new SqlParameter("@StayTime", SqlDbType.DateTime),
					new SqlParameter("@StayType", SqlDbType.NVarChar,20),
					new SqlParameter("@ChargingTime", SqlDbType.DateTime),
					new SqlParameter("@ChargeCause", SqlDbType.NVarChar,50),
					new SqlParameter("@ChargeTime", SqlDbType.DateTime),
					new SqlParameter("@ContractSign", SqlDbType.NVarChar,20),
					new SqlParameter("@RightsSign", SqlDbType.NVarChar,20),
					new SqlParameter("@PropertyUses", SqlDbType.NVarChar,20),
					new SqlParameter("@StartDate", SqlDbType.DateTime),
					new SqlParameter("@EndDate", SqlDbType.DateTime),
					new SqlParameter("@ReletDate", SqlDbType.DateTime),
					new SqlParameter("@IsActive", SqlDbType.SmallInt,2),
					new SqlParameter("@IsDelLive", SqlDbType.SmallInt,2),
					new SqlParameter("@IsDebts", SqlDbType.SmallInt,2),
					new SqlParameter("@IsSale", SqlDbType.SmallInt,2),
					new SqlParameter("@PurchaseArea", SqlDbType.Decimal,9),
					new SqlParameter("@PropertyArea", SqlDbType.Decimal,9),
					new SqlParameter("@LiveState", SqlDbType.SmallInt,2),
					new SqlParameter("@BankName", SqlDbType.NVarChar,20),
					new SqlParameter("@BankIDs", SqlDbType.NVarChar,50),
					new SqlParameter("@BankAccount", SqlDbType.NVarChar,30),
					new SqlParameter("@BankAgreement", SqlDbType.NVarChar,20),
					new SqlParameter("@BankCode", SqlDbType.NVarChar,20),
					new SqlParameter("@BankNo", SqlDbType.NVarChar,20),
					new SqlParameter("@OldCustID", SqlDbType.BigInt,8),
					new SqlParameter("@SynchFlag", SqlDbType.SmallInt,2),
					new SqlParameter("@OrdSNum", SqlDbType.BigInt,8)};
			parameters[0].Value = model.LiveSynchCode;
			parameters[1].Value = model.CommSynchCode;
			parameters[2].Value = model.CustSynchCode;
			parameters[3].Value = model.RoomSynchCode;
			parameters[4].Value = model.UnLiveID;
			parameters[5].Value = model.LiveType;
			parameters[6].Value = model.L_BroadbandNum;
			parameters[7].Value = model.L_FixedTel;
			parameters[8].Value = model.L_MobilePhone;
			parameters[9].Value = model.L_IPTVAccount;
			parameters[10].Value = model.SubmitTime;
			parameters[11].Value = model.FittingTime;
			parameters[12].Value = model.StayTime;
			parameters[13].Value = model.StayType;
			parameters[14].Value = model.ChargingTime;
			parameters[15].Value = model.ChargeCause;
			parameters[16].Value = model.ChargeTime;
			parameters[17].Value = model.ContractSign;
			parameters[18].Value = model.RightsSign;
			parameters[19].Value = model.PropertyUses;
			parameters[20].Value = model.StartDate;
			parameters[21].Value = model.EndDate;
			parameters[22].Value = model.ReletDate;
			parameters[23].Value = model.IsActive;
			parameters[24].Value = model.IsDelLive;
			parameters[25].Value = model.IsDebts;
			parameters[26].Value = model.IsSale;
			parameters[27].Value = model.PurchaseArea;
			parameters[28].Value = model.PropertyArea;
			parameters[29].Value = model.LiveState;
			parameters[30].Value = model.BankName;
			parameters[31].Value = model.BankIDs;
			parameters[32].Value = model.BankAccount;
			parameters[33].Value = model.BankAgreement;
			parameters[34].Value = model.BankCode;
			parameters[35].Value = model.BankNo;
			parameters[36].Value = model.OldCustID;
			parameters[37].Value = model.SynchFlag;
			parameters[38].Value = model.OrdSNum;

			DbHelperSQL.RunProcedure("Proc_Tb_Unify_CustomerLive_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.Unify.Tb_Unify_CustomerLive model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@LiveSynchCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@CommSynchCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@CustSynchCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@RoomSynchCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@UnLiveID", SqlDbType.BigInt,8),
					new SqlParameter("@LiveType", SqlDbType.SmallInt,2),
					new SqlParameter("@L_BroadbandNum", SqlDbType.NVarChar,30),
					new SqlParameter("@L_FixedTel", SqlDbType.NVarChar,50),
					new SqlParameter("@L_MobilePhone", SqlDbType.NVarChar,50),
					new SqlParameter("@L_IPTVAccount", SqlDbType.NVarChar,50),
					new SqlParameter("@SubmitTime", SqlDbType.DateTime),
					new SqlParameter("@FittingTime", SqlDbType.DateTime),
					new SqlParameter("@StayTime", SqlDbType.DateTime),
					new SqlParameter("@StayType", SqlDbType.NVarChar,20),
					new SqlParameter("@ChargingTime", SqlDbType.DateTime),
					new SqlParameter("@ChargeCause", SqlDbType.NVarChar,50),
					new SqlParameter("@ChargeTime", SqlDbType.DateTime),
					new SqlParameter("@ContractSign", SqlDbType.NVarChar,20),
					new SqlParameter("@RightsSign", SqlDbType.NVarChar,20),
					new SqlParameter("@PropertyUses", SqlDbType.NVarChar,20),
					new SqlParameter("@StartDate", SqlDbType.DateTime),
					new SqlParameter("@EndDate", SqlDbType.DateTime),
					new SqlParameter("@ReletDate", SqlDbType.DateTime),
					new SqlParameter("@IsActive", SqlDbType.SmallInt,2),
					new SqlParameter("@IsDelLive", SqlDbType.SmallInt,2),
					new SqlParameter("@IsDebts", SqlDbType.SmallInt,2),
					new SqlParameter("@IsSale", SqlDbType.SmallInt,2),
					new SqlParameter("@PurchaseArea", SqlDbType.Decimal,9),
					new SqlParameter("@PropertyArea", SqlDbType.Decimal,9),
					new SqlParameter("@LiveState", SqlDbType.SmallInt,2),
					new SqlParameter("@BankName", SqlDbType.NVarChar,20),
					new SqlParameter("@BankIDs", SqlDbType.NVarChar,50),
					new SqlParameter("@BankAccount", SqlDbType.NVarChar,30),
					new SqlParameter("@BankAgreement", SqlDbType.NVarChar,20),
					new SqlParameter("@BankCode", SqlDbType.NVarChar,20),
					new SqlParameter("@BankNo", SqlDbType.NVarChar,20),
					new SqlParameter("@OldCustID", SqlDbType.BigInt,8),
					new SqlParameter("@SynchFlag", SqlDbType.SmallInt,2),
					new SqlParameter("@OrdSNum", SqlDbType.BigInt,8)};
			parameters[0].Value = model.LiveSynchCode;
			parameters[1].Value = model.CommSynchCode;
			parameters[2].Value = model.CustSynchCode;
			parameters[3].Value = model.RoomSynchCode;
			parameters[4].Value = model.UnLiveID;
			parameters[5].Value = model.LiveType;
			parameters[6].Value = model.L_BroadbandNum;
			parameters[7].Value = model.L_FixedTel;
			parameters[8].Value = model.L_MobilePhone;
			parameters[9].Value = model.L_IPTVAccount;
			parameters[10].Value = model.SubmitTime;
			parameters[11].Value = model.FittingTime;
			parameters[12].Value = model.StayTime;
			parameters[13].Value = model.StayType;
			parameters[14].Value = model.ChargingTime;
			parameters[15].Value = model.ChargeCause;
			parameters[16].Value = model.ChargeTime;
			parameters[17].Value = model.ContractSign;
			parameters[18].Value = model.RightsSign;
			parameters[19].Value = model.PropertyUses;
			parameters[20].Value = model.StartDate;
			parameters[21].Value = model.EndDate;
			parameters[22].Value = model.ReletDate;
			parameters[23].Value = model.IsActive;
			parameters[24].Value = model.IsDelLive;
			parameters[25].Value = model.IsDebts;
			parameters[26].Value = model.IsSale;
			parameters[27].Value = model.PurchaseArea;
			parameters[28].Value = model.PropertyArea;
			parameters[29].Value = model.LiveState;
			parameters[30].Value = model.BankName;
			parameters[31].Value = model.BankIDs;
			parameters[32].Value = model.BankAccount;
			parameters[33].Value = model.BankAgreement;
			parameters[34].Value = model.BankCode;
			parameters[35].Value = model.BankNo;
			parameters[36].Value = model.OldCustID;
			parameters[37].Value = model.SynchFlag;
			parameters[38].Value = model.OrdSNum;

			DbHelperSQL.RunProcedure("Proc_Tb_Unify_CustomerLive_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(Guid LiveSynchCode)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@LiveSynchCode", SqlDbType.UniqueIdentifier)};
			parameters[0].Value = LiveSynchCode;

			DbHelperSQL.RunProcedure("Proc_Tb_Unify_CustomerLive_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.Unify.Tb_Unify_CustomerLive GetModel(Guid LiveSynchCode)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@LiveSynchCode", SqlDbType.UniqueIdentifier)};
			parameters[0].Value = LiveSynchCode;

			MobileSoft.Model.Unify.Tb_Unify_CustomerLive model=new MobileSoft.Model.Unify.Tb_Unify_CustomerLive();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_Unify_CustomerLive_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["LiveSynchCode"].ToString()!="")
				{
					model.LiveSynchCode=new Guid(ds.Tables[0].Rows[0]["LiveSynchCode"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CommSynchCode"].ToString()!="")
				{
					model.CommSynchCode=new Guid(ds.Tables[0].Rows[0]["CommSynchCode"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CustSynchCode"].ToString()!="")
				{
					model.CustSynchCode=new Guid(ds.Tables[0].Rows[0]["CustSynchCode"].ToString());
				}
				if(ds.Tables[0].Rows[0]["RoomSynchCode"].ToString()!="")
				{
					model.RoomSynchCode=new Guid(ds.Tables[0].Rows[0]["RoomSynchCode"].ToString());
				}
				if(ds.Tables[0].Rows[0]["UnLiveID"].ToString()!="")
				{
					model.UnLiveID=long.Parse(ds.Tables[0].Rows[0]["UnLiveID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["LiveType"].ToString()!="")
				{
					model.LiveType=int.Parse(ds.Tables[0].Rows[0]["LiveType"].ToString());
				}
				model.L_BroadbandNum=ds.Tables[0].Rows[0]["L_BroadbandNum"].ToString();
				model.L_FixedTel=ds.Tables[0].Rows[0]["L_FixedTel"].ToString();
				model.L_MobilePhone=ds.Tables[0].Rows[0]["L_MobilePhone"].ToString();
				model.L_IPTVAccount=ds.Tables[0].Rows[0]["L_IPTVAccount"].ToString();
				if(ds.Tables[0].Rows[0]["SubmitTime"].ToString()!="")
				{
					model.SubmitTime=DateTime.Parse(ds.Tables[0].Rows[0]["SubmitTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["FittingTime"].ToString()!="")
				{
					model.FittingTime=DateTime.Parse(ds.Tables[0].Rows[0]["FittingTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["StayTime"].ToString()!="")
				{
					model.StayTime=DateTime.Parse(ds.Tables[0].Rows[0]["StayTime"].ToString());
				}
				model.StayType=ds.Tables[0].Rows[0]["StayType"].ToString();
				if(ds.Tables[0].Rows[0]["ChargingTime"].ToString()!="")
				{
					model.ChargingTime=DateTime.Parse(ds.Tables[0].Rows[0]["ChargingTime"].ToString());
				}
				model.ChargeCause=ds.Tables[0].Rows[0]["ChargeCause"].ToString();
				if(ds.Tables[0].Rows[0]["ChargeTime"].ToString()!="")
				{
					model.ChargeTime=DateTime.Parse(ds.Tables[0].Rows[0]["ChargeTime"].ToString());
				}
				model.ContractSign=ds.Tables[0].Rows[0]["ContractSign"].ToString();
				model.RightsSign=ds.Tables[0].Rows[0]["RightsSign"].ToString();
				model.PropertyUses=ds.Tables[0].Rows[0]["PropertyUses"].ToString();
				if(ds.Tables[0].Rows[0]["StartDate"].ToString()!="")
				{
					model.StartDate=DateTime.Parse(ds.Tables[0].Rows[0]["StartDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["EndDate"].ToString()!="")
				{
					model.EndDate=DateTime.Parse(ds.Tables[0].Rows[0]["EndDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ReletDate"].ToString()!="")
				{
					model.ReletDate=DateTime.Parse(ds.Tables[0].Rows[0]["ReletDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsActive"].ToString()!="")
				{
					model.IsActive=int.Parse(ds.Tables[0].Rows[0]["IsActive"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsDelLive"].ToString()!="")
				{
					model.IsDelLive=int.Parse(ds.Tables[0].Rows[0]["IsDelLive"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsDebts"].ToString()!="")
				{
					model.IsDebts=int.Parse(ds.Tables[0].Rows[0]["IsDebts"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsSale"].ToString()!="")
				{
					model.IsSale=int.Parse(ds.Tables[0].Rows[0]["IsSale"].ToString());
				}
				if(ds.Tables[0].Rows[0]["PurchaseArea"].ToString()!="")
				{
					model.PurchaseArea=decimal.Parse(ds.Tables[0].Rows[0]["PurchaseArea"].ToString());
				}
				if(ds.Tables[0].Rows[0]["PropertyArea"].ToString()!="")
				{
					model.PropertyArea=decimal.Parse(ds.Tables[0].Rows[0]["PropertyArea"].ToString());
				}
				if(ds.Tables[0].Rows[0]["LiveState"].ToString()!="")
				{
					model.LiveState=int.Parse(ds.Tables[0].Rows[0]["LiveState"].ToString());
				}
				model.BankName=ds.Tables[0].Rows[0]["BankName"].ToString();
				model.BankIDs=ds.Tables[0].Rows[0]["BankIDs"].ToString();
				model.BankAccount=ds.Tables[0].Rows[0]["BankAccount"].ToString();
				model.BankAgreement=ds.Tables[0].Rows[0]["BankAgreement"].ToString();
				model.BankCode=ds.Tables[0].Rows[0]["BankCode"].ToString();
				model.BankNo=ds.Tables[0].Rows[0]["BankNo"].ToString();
				if(ds.Tables[0].Rows[0]["OldCustID"].ToString()!="")
				{
					model.OldCustID=long.Parse(ds.Tables[0].Rows[0]["OldCustID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SynchFlag"].ToString()!="")
				{
					model.SynchFlag=int.Parse(ds.Tables[0].Rows[0]["SynchFlag"].ToString());
				}
				if(ds.Tables[0].Rows[0]["OrdSNum"].ToString()!="")
				{
					model.OrdSNum=long.Parse(ds.Tables[0].Rows[0]["OrdSNum"].ToString());
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
            strSql.Append(" FROM View_Unify_CustomerLive_Filter ");
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
            strSql.Append(" FROM View_Unify_CustomerLive_Filter ");
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
			parameters[5].Value = "SELECT * FROM View_Unify_CustomerLive_Filter WHERE 1=1 " + StrCondition;
			parameters[6].Value = "LiveSynchCode";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法

        
	}
}

