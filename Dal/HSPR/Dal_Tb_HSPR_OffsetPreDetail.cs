using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.HSPR
{
	/// <summary>
	/// 数据访问类Dal_Tb_HSPR_OffsetPreDetail。
	/// </summary>
	public class Dal_Tb_HSPR_OffsetPreDetail
	{
		public Dal_Tb_HSPR_OffsetPreDetail()
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

			int result= DbHelperSQL.RunProcedure("Proc_Tb_HSPR_OffsetPreDetail_Exists",parameters,out rowsAffected);
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
		public int Add(MobileSoft.Model.HSPR.Tb_HSPR_OffsetPreDetail model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@IID", SqlDbType.BigInt,8),
					new SqlParameter("@OffsetID", SqlDbType.BigInt,8),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@CostID", SqlDbType.BigInt,8),
					new SqlParameter("@CustID", SqlDbType.BigInt,8),
					new SqlParameter("@RoomID", SqlDbType.BigInt,8),
					new SqlParameter("@FeesDueDate", SqlDbType.DateTime),
					new SqlParameter("@DebtsAmount", SqlDbType.Decimal,9),
					new SqlParameter("@OldPrecAmount", SqlDbType.Decimal,9),
					new SqlParameter("@NewPrecAmount", SqlDbType.Decimal,9),
					new SqlParameter("@OffsetAmount", SqlDbType.Decimal,9),
					new SqlParameter("@FeesID", SqlDbType.BigInt,8),
					new SqlParameter("@PrecID", SqlDbType.BigInt,8),
					new SqlParameter("@TakeWise", SqlDbType.SmallInt,2),
					new SqlParameter("@IsAudit", SqlDbType.SmallInt,2),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2),
					new SqlParameter("@OffsetLateFeeAmount", SqlDbType.Decimal,9),
					new SqlParameter("@ReceID", SqlDbType.BigInt,8),
					new SqlParameter("@ReceCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@DelUserCode", SqlDbType.NVarChar,20),
					new SqlParameter("@DelDate", SqlDbType.DateTime),
					new SqlParameter("@ChangeMemo", SqlDbType.NVarChar,200),
					new SqlParameter("@IsProperty", SqlDbType.SmallInt,2),
					new SqlParameter("@IsAutoPrec", SqlDbType.SmallInt,2)};
			parameters[0].Direction = ParameterDirection.Output;
			parameters[1].Value = model.OffsetID;
			parameters[2].Value = model.CommID;
			parameters[3].Value = model.CostID;
			parameters[4].Value = model.CustID;
			parameters[5].Value = model.RoomID;
			parameters[6].Value = model.FeesDueDate;
			parameters[7].Value = model.DebtsAmount;
			parameters[8].Value = model.OldPrecAmount;
			parameters[9].Value = model.NewPrecAmount;
			parameters[10].Value = model.OffsetAmount;
			parameters[11].Value = model.FeesID;
			parameters[12].Value = model.PrecID;
			parameters[13].Value = model.TakeWise;
			parameters[14].Value = model.IsAudit;
			parameters[15].Value = model.IsDelete;
			parameters[16].Value = model.OffsetLateFeeAmount;
			parameters[17].Value = model.ReceID;
			parameters[18].Value = model.ReceCode;
			parameters[19].Value = model.DelUserCode;
			parameters[20].Value = model.DelDate;
			parameters[21].Value = model.ChangeMemo;
			parameters[22].Value = model.IsProperty;
			parameters[23].Value = model.IsAutoPrec;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_OffsetPreDetail_ADD",parameters,out rowsAffected);
			return (int)parameters[0].Value;
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.HSPR.Tb_HSPR_OffsetPreDetail model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@IID", SqlDbType.BigInt,8),
					new SqlParameter("@OffsetID", SqlDbType.BigInt,8),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@CostID", SqlDbType.BigInt,8),
					new SqlParameter("@CustID", SqlDbType.BigInt,8),
					new SqlParameter("@RoomID", SqlDbType.BigInt,8),
					new SqlParameter("@FeesDueDate", SqlDbType.DateTime),
					new SqlParameter("@DebtsAmount", SqlDbType.Decimal,9),
					new SqlParameter("@OldPrecAmount", SqlDbType.Decimal,9),
					new SqlParameter("@NewPrecAmount", SqlDbType.Decimal,9),
					new SqlParameter("@OffsetAmount", SqlDbType.Decimal,9),
					new SqlParameter("@FeesID", SqlDbType.BigInt,8),
					new SqlParameter("@PrecID", SqlDbType.BigInt,8),
					new SqlParameter("@TakeWise", SqlDbType.SmallInt,2),
					new SqlParameter("@IsAudit", SqlDbType.SmallInt,2),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2),
					new SqlParameter("@OffsetLateFeeAmount", SqlDbType.Decimal,9),
					new SqlParameter("@ReceID", SqlDbType.BigInt,8),
					new SqlParameter("@ReceCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@DelUserCode", SqlDbType.NVarChar,20),
					new SqlParameter("@DelDate", SqlDbType.DateTime),
					new SqlParameter("@ChangeMemo", SqlDbType.NVarChar,200),
					new SqlParameter("@IsProperty", SqlDbType.SmallInt,2),
					new SqlParameter("@IsAutoPrec", SqlDbType.SmallInt,2)};
			parameters[0].Value = model.IID;
			parameters[1].Value = model.OffsetID;
			parameters[2].Value = model.CommID;
			parameters[3].Value = model.CostID;
			parameters[4].Value = model.CustID;
			parameters[5].Value = model.RoomID;
			parameters[6].Value = model.FeesDueDate;
			parameters[7].Value = model.DebtsAmount;
			parameters[8].Value = model.OldPrecAmount;
			parameters[9].Value = model.NewPrecAmount;
			parameters[10].Value = model.OffsetAmount;
			parameters[11].Value = model.FeesID;
			parameters[12].Value = model.PrecID;
			parameters[13].Value = model.TakeWise;
			parameters[14].Value = model.IsAudit;
			parameters[15].Value = model.IsDelete;
			parameters[16].Value = model.OffsetLateFeeAmount;
			parameters[17].Value = model.ReceID;
			parameters[18].Value = model.ReceCode;
			parameters[19].Value = model.DelUserCode;
			parameters[20].Value = model.DelDate;
			parameters[21].Value = model.ChangeMemo;
			parameters[22].Value = model.IsProperty;
			parameters[23].Value = model.IsAutoPrec;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_OffsetPreDetail_Update",parameters,out rowsAffected);
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

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_OffsetPreDetail_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_OffsetPreDetail GetModel(long IID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@IID", SqlDbType.BigInt)};
			parameters[0].Value = IID;

			MobileSoft.Model.HSPR.Tb_HSPR_OffsetPreDetail model=new MobileSoft.Model.HSPR.Tb_HSPR_OffsetPreDetail();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_HSPR_OffsetPreDetail_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["IID"].ToString()!="")
				{
					model.IID=long.Parse(ds.Tables[0].Rows[0]["IID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["OffsetID"].ToString()!="")
				{
					model.OffsetID=long.Parse(ds.Tables[0].Rows[0]["OffsetID"].ToString());
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
				if(ds.Tables[0].Rows[0]["DebtsAmount"].ToString()!="")
				{
					model.DebtsAmount=decimal.Parse(ds.Tables[0].Rows[0]["DebtsAmount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["OldPrecAmount"].ToString()!="")
				{
					model.OldPrecAmount=decimal.Parse(ds.Tables[0].Rows[0]["OldPrecAmount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["NewPrecAmount"].ToString()!="")
				{
					model.NewPrecAmount=decimal.Parse(ds.Tables[0].Rows[0]["NewPrecAmount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["OffsetAmount"].ToString()!="")
				{
					model.OffsetAmount=decimal.Parse(ds.Tables[0].Rows[0]["OffsetAmount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["FeesID"].ToString()!="")
				{
					model.FeesID=long.Parse(ds.Tables[0].Rows[0]["FeesID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["PrecID"].ToString()!="")
				{
					model.PrecID=long.Parse(ds.Tables[0].Rows[0]["PrecID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["TakeWise"].ToString()!="")
				{
					model.TakeWise=int.Parse(ds.Tables[0].Rows[0]["TakeWise"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsAudit"].ToString()!="")
				{
					model.IsAudit=int.Parse(ds.Tables[0].Rows[0]["IsAudit"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsDelete"].ToString()!="")
				{
					model.IsDelete=int.Parse(ds.Tables[0].Rows[0]["IsDelete"].ToString());
				}
				if(ds.Tables[0].Rows[0]["OffsetLateFeeAmount"].ToString()!="")
				{
					model.OffsetLateFeeAmount=decimal.Parse(ds.Tables[0].Rows[0]["OffsetLateFeeAmount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ReceID"].ToString()!="")
				{
					model.ReceID=long.Parse(ds.Tables[0].Rows[0]["ReceID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ReceCode"].ToString()!="")
				{
					model.ReceCode=new Guid(ds.Tables[0].Rows[0]["ReceCode"].ToString());
				}
				model.DelUserCode=ds.Tables[0].Rows[0]["DelUserCode"].ToString();
				if(ds.Tables[0].Rows[0]["DelDate"].ToString()!="")
				{
					model.DelDate=DateTime.Parse(ds.Tables[0].Rows[0]["DelDate"].ToString());
				}
				model.ChangeMemo=ds.Tables[0].Rows[0]["ChangeMemo"].ToString();
				if(ds.Tables[0].Rows[0]["IsProperty"].ToString()!="")
				{
					model.IsProperty=int.Parse(ds.Tables[0].Rows[0]["IsProperty"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsAutoPrec"].ToString()!="")
				{
					model.IsAutoPrec=int.Parse(ds.Tables[0].Rows[0]["IsAutoPrec"].ToString());
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
			strSql.Append("select IID,OffsetID,CommID,CostID,CustID,RoomID,FeesDueDate,DebtsAmount,OldPrecAmount,NewPrecAmount,OffsetAmount,FeesID,PrecID,TakeWise,IsAudit,IsDelete,OffsetLateFeeAmount,ReceID,ReceCode,DelUserCode,DelDate,ChangeMemo,IsProperty,IsAutoPrec ");
			strSql.Append(" FROM Tb_HSPR_OffsetPreDetail ");
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
			strSql.Append(" IID,OffsetID,CommID,CostID,CustID,RoomID,FeesDueDate,DebtsAmount,OldPrecAmount,NewPrecAmount,OffsetAmount,FeesID,PrecID,TakeWise,IsAudit,IsDelete,OffsetLateFeeAmount,ReceID,ReceCode,DelUserCode,DelDate,ChangeMemo,IsProperty,IsAutoPrec ");
			strSql.Append(" FROM Tb_HSPR_OffsetPreDetail ");
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
			parameters[5].Value = "SELECT * FROM Tb_HSPR_OffsetPreDetail WHERE 1=1 " + StrCondition;
			parameters[6].Value = "IID";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法

            public void HSPROffsetPreDetailDelete(long IID, string UserCode, string ChangeMemo)
            {
                  SqlParameter[] parameters = {
					new SqlParameter("@IID", SqlDbType.BigInt),
                              new SqlParameter("@UserCode", SqlDbType.VarChar,50),
                              new SqlParameter("@ChangeMemo", SqlDbType.VarChar,1000)
                                              };
                  parameters[0].Value = IID;
                  parameters[1].Value = UserCode;
                  parameters[2].Value = ChangeMemo;

                  DbHelperSQL.RunProcedure("Proc_HSPR_OffsetPreDetail_Delete", parameters, "RetDataSet");

            }

	}
}

