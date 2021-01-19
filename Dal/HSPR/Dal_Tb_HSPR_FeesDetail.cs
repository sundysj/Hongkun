using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.HSPR
{
	/// <summary>
	/// 数据访问类Dal_Tb_HSPR_FeesDetail。
	/// </summary>
	public class Dal_Tb_HSPR_FeesDetail
	{
		public Dal_Tb_HSPR_FeesDetail()
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

			int result= DbHelperSQL.RunProcedure("Proc_Tb_HSPR_FeesDetail_Exists",parameters,out rowsAffected);
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
		public void Add(MobileSoft.Model.HSPR.Tb_HSPR_FeesDetail model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@RecdID", SqlDbType.BigInt,8),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@CostID", SqlDbType.BigInt,8),
					new SqlParameter("@CustID", SqlDbType.BigInt,8),
					new SqlParameter("@RoomID", SqlDbType.BigInt,8),
					new SqlParameter("@FeesID", SqlDbType.BigInt,8),
					new SqlParameter("@ReceID", SqlDbType.BigInt,8),
					new SqlParameter("@ChargeMode", SqlDbType.NVarChar,20),
					new SqlParameter("@AccountWay", SqlDbType.SmallInt,2),
					new SqlParameter("@ChargeAmount", SqlDbType.Decimal,9),
					new SqlParameter("@LateFeeAmount", SqlDbType.Decimal,9),
					new SqlParameter("@ChargeDate", SqlDbType.DateTime),
					new SqlParameter("@FeesMemo", SqlDbType.NVarChar,30),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2),
					new SqlParameter("@OldCostID", SqlDbType.BigInt,8)};
			parameters[0].Value = model.RecdID;
			parameters[1].Value = model.CommID;
			parameters[2].Value = model.CostID;
			parameters[3].Value = model.CustID;
			parameters[4].Value = model.RoomID;
			parameters[5].Value = model.FeesID;
			parameters[6].Value = model.ReceID;
			parameters[7].Value = model.ChargeMode;
			parameters[8].Value = model.AccountWay;
			parameters[9].Value = model.ChargeAmount;
			parameters[10].Value = model.LateFeeAmount;
			parameters[11].Value = model.ChargeDate;
			parameters[12].Value = model.FeesMemo;
			parameters[13].Value = model.IsDelete;
			parameters[14].Value = model.OldCostID;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_FeesDetail_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.HSPR.Tb_HSPR_FeesDetail model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@RecdID", SqlDbType.BigInt,8),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@CostID", SqlDbType.BigInt,8),
					new SqlParameter("@CustID", SqlDbType.BigInt,8),
					new SqlParameter("@RoomID", SqlDbType.BigInt,8),
					new SqlParameter("@FeesID", SqlDbType.BigInt,8),
					new SqlParameter("@ReceID", SqlDbType.BigInt,8),
					new SqlParameter("@ChargeMode", SqlDbType.NVarChar,20),
					new SqlParameter("@AccountWay", SqlDbType.SmallInt,2),
					new SqlParameter("@ChargeAmount", SqlDbType.Decimal,9),
					new SqlParameter("@LateFeeAmount", SqlDbType.Decimal,9),
					new SqlParameter("@ChargeDate", SqlDbType.DateTime),
					new SqlParameter("@FeesMemo", SqlDbType.NVarChar,30),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2),
					new SqlParameter("@OldCostID", SqlDbType.BigInt,8)};
			parameters[0].Value = model.RecdID;
			parameters[1].Value = model.CommID;
			parameters[2].Value = model.CostID;
			parameters[3].Value = model.CustID;
			parameters[4].Value = model.RoomID;
			parameters[5].Value = model.FeesID;
			parameters[6].Value = model.ReceID;
			parameters[7].Value = model.ChargeMode;
			parameters[8].Value = model.AccountWay;
			parameters[9].Value = model.ChargeAmount;
			parameters[10].Value = model.LateFeeAmount;
			parameters[11].Value = model.ChargeDate;
			parameters[12].Value = model.FeesMemo;
			parameters[13].Value = model.IsDelete;
			parameters[14].Value = model.OldCostID;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_FeesDetail_Update",parameters,out rowsAffected);
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

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_FeesDetail_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_FeesDetail GetModel(long RecdID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@RecdID", SqlDbType.BigInt)};
			parameters[0].Value = RecdID;

			MobileSoft.Model.HSPR.Tb_HSPR_FeesDetail model=new MobileSoft.Model.HSPR.Tb_HSPR_FeesDetail();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_HSPR_FeesDetail_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["RecdID"].ToString()!="")
				{
					model.RecdID=long.Parse(ds.Tables[0].Rows[0]["RecdID"].ToString());
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
				if(ds.Tables[0].Rows[0]["FeesID"].ToString()!="")
				{
					model.FeesID=long.Parse(ds.Tables[0].Rows[0]["FeesID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ReceID"].ToString()!="")
				{
					model.ReceID=long.Parse(ds.Tables[0].Rows[0]["ReceID"].ToString());
				}
				model.ChargeMode=ds.Tables[0].Rows[0]["ChargeMode"].ToString();
				if(ds.Tables[0].Rows[0]["AccountWay"].ToString()!="")
				{
					model.AccountWay=int.Parse(ds.Tables[0].Rows[0]["AccountWay"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ChargeAmount"].ToString()!="")
				{
					model.ChargeAmount=decimal.Parse(ds.Tables[0].Rows[0]["ChargeAmount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["LateFeeAmount"].ToString()!="")
				{
					model.LateFeeAmount=decimal.Parse(ds.Tables[0].Rows[0]["LateFeeAmount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ChargeDate"].ToString()!="")
				{
					model.ChargeDate=DateTime.Parse(ds.Tables[0].Rows[0]["ChargeDate"].ToString());
				}
				model.FeesMemo=ds.Tables[0].Rows[0]["FeesMemo"].ToString();
				if(ds.Tables[0].Rows[0]["IsDelete"].ToString()!="")
				{
					model.IsDelete=int.Parse(ds.Tables[0].Rows[0]["IsDelete"].ToString());
				}
				if(ds.Tables[0].Rows[0]["OldCostID"].ToString()!="")
				{
					model.OldCostID=long.Parse(ds.Tables[0].Rows[0]["OldCostID"].ToString());
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
			strSql.Append("select RecdID,CommID,CostID,CustID,RoomID,FeesID,ReceID,ChargeMode,AccountWay,ChargeAmount,LateFeeAmount,ChargeDate,FeesMemo,IsDelete,OldCostID ");
			strSql.Append(" FROM Tb_HSPR_FeesDetail ");
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
			strSql.Append(" RecdID,CommID,CostID,CustID,RoomID,FeesID,ReceID,ChargeMode,AccountWay,ChargeAmount,LateFeeAmount,ChargeDate,FeesMemo,IsDelete,OldCostID ");
			strSql.Append(" FROM Tb_HSPR_FeesDetail ");
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
			parameters[5].Value = "SELECT * FROM Tb_HSPR_FeesDetail WHERE 1=1 " + StrCondition;
			parameters[6].Value = "RecdID";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法

            public void FeesDetailDelete(long RecdID)
            {
                  SqlParameter[] parameters = {
					new SqlParameter("@RecdID", SqlDbType.BigInt)
                                              };
                  parameters[0].Value = RecdID;
                  DbHelperSQL.RunProcedure("Proc_HSPR_FeesDetail_Delete", parameters, "RetDataSet");
            }

	}
}

