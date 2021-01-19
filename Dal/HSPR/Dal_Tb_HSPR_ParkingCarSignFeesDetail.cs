using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.HSPR
{
	/// <summary>
	/// 数据访问类Dal_Tb_HSPR_ParkingCarSignFeesDetail。
	/// </summary>
	public class Dal_Tb_HSPR_ParkingCarSignFeesDetail
	{
		public Dal_Tb_HSPR_ParkingCarSignFeesDetail()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public long GetMaxId()
		{
		return DbHelperSQL.GetMaxID("IID", "Tb_HSPR_ParkingCarSignFeesDetail"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long IID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@IID", SqlDbType.BigInt)};
			parameters[0].Value = IID;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_HSPR_ParkingCarSignFeesDetail_Exists",parameters,out rowsAffected);
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
		public long Add(MobileSoft.Model.HSPR.Tb_HSPR_ParkingCarSignFeesDetail model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@IID", SqlDbType.BigInt,8),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@CustID", SqlDbType.BigInt,8),
					new SqlParameter("@RoomID", SqlDbType.BigInt,8),
					new SqlParameter("@CostID", SqlDbType.BigInt,8),
					new SqlParameter("@ReceID", SqlDbType.BigInt,8),
					new SqlParameter("@RecdID", SqlDbType.BigInt,8),
					new SqlParameter("@ParkID", SqlDbType.BigInt,8),
					new SqlParameter("@HandID", SqlDbType.BigInt,8),
					new SqlParameter("@IsHisFees", SqlDbType.SmallInt,2),
					new SqlParameter("@FeesStateDate", SqlDbType.DateTime),
					new SqlParameter("@FeesEndDate", SqlDbType.DateTime),
					new SqlParameter("@ChargeAmount", SqlDbType.Decimal,9)};
			parameters[0].Direction = ParameterDirection.Output;
			parameters[1].Value = model.CommID;
			parameters[2].Value = model.CustID;
			parameters[3].Value = model.RoomID;
			parameters[4].Value = model.CostID;
			parameters[5].Value = model.ReceID;
			parameters[6].Value = model.RecdID;
			parameters[7].Value = model.ParkID;
			parameters[8].Value = model.HandID;
			parameters[9].Value = model.IsHisFees;
			parameters[10].Value = model.FeesStateDate;
			parameters[11].Value = model.FeesEndDate;
			parameters[12].Value = model.ChargeAmount;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_ParkingCarSignFeesDetail_ADD",parameters,out rowsAffected);
			return (long)parameters[0].Value;
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.HSPR.Tb_HSPR_ParkingCarSignFeesDetail model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@IID", SqlDbType.BigInt,8),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@CustID", SqlDbType.BigInt,8),
					new SqlParameter("@RoomID", SqlDbType.BigInt,8),
					new SqlParameter("@CostID", SqlDbType.BigInt,8),
					new SqlParameter("@ReceID", SqlDbType.BigInt,8),
					new SqlParameter("@RecdID", SqlDbType.BigInt,8),
					new SqlParameter("@ParkID", SqlDbType.BigInt,8),
					new SqlParameter("@HandID", SqlDbType.BigInt,8),
					new SqlParameter("@IsHisFees", SqlDbType.SmallInt,2),
					new SqlParameter("@FeesStateDate", SqlDbType.DateTime),
					new SqlParameter("@FeesEndDate", SqlDbType.DateTime),
					new SqlParameter("@ChargeAmount", SqlDbType.Decimal,9)};
			parameters[0].Value = model.IID;
			parameters[1].Value = model.CommID;
			parameters[2].Value = model.CustID;
			parameters[3].Value = model.RoomID;
			parameters[4].Value = model.CostID;
			parameters[5].Value = model.ReceID;
			parameters[6].Value = model.RecdID;
			parameters[7].Value = model.ParkID;
			parameters[8].Value = model.HandID;
			parameters[9].Value = model.IsHisFees;
			parameters[10].Value = model.FeesStateDate;
			parameters[11].Value = model.FeesEndDate;
			parameters[12].Value = model.ChargeAmount;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_ParkingCarSignFeesDetail_Update",parameters,out rowsAffected);
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

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_ParkingCarSignFeesDetail_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_ParkingCarSignFeesDetail GetModel(long IID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@IID", SqlDbType.BigInt)};
			parameters[0].Value = IID;

			MobileSoft.Model.HSPR.Tb_HSPR_ParkingCarSignFeesDetail model=new MobileSoft.Model.HSPR.Tb_HSPR_ParkingCarSignFeesDetail();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_HSPR_ParkingCarSignFeesDetail_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["IID"].ToString()!="")
				{
					model.IID=long.Parse(ds.Tables[0].Rows[0]["IID"].ToString());
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
				if(ds.Tables[0].Rows[0]["ReceID"].ToString()!="")
				{
					model.ReceID=long.Parse(ds.Tables[0].Rows[0]["ReceID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["RecdID"].ToString()!="")
				{
					model.RecdID=long.Parse(ds.Tables[0].Rows[0]["RecdID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ParkID"].ToString()!="")
				{
					model.ParkID=long.Parse(ds.Tables[0].Rows[0]["ParkID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["HandID"].ToString()!="")
				{
					model.HandID=long.Parse(ds.Tables[0].Rows[0]["HandID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsHisFees"].ToString()!="")
				{
					model.IsHisFees=int.Parse(ds.Tables[0].Rows[0]["IsHisFees"].ToString());
				}
				if(ds.Tables[0].Rows[0]["FeesStateDate"].ToString()!="")
				{
					model.FeesStateDate=DateTime.Parse(ds.Tables[0].Rows[0]["FeesStateDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["FeesEndDate"].ToString()!="")
				{
					model.FeesEndDate=DateTime.Parse(ds.Tables[0].Rows[0]["FeesEndDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ChargeAmount"].ToString()!="")
				{
					model.ChargeAmount=decimal.Parse(ds.Tables[0].Rows[0]["ChargeAmount"].ToString());
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
			strSql.Append("select IID,CommID,CustID,RoomID,CostID,ReceID,RecdID,ParkID,HandID,IsHisFees,FeesStateDate,FeesEndDate,ChargeAmount ");
			strSql.Append(" FROM Tb_HSPR_ParkingCarSignFeesDetail ");
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
			strSql.Append(" IID,CommID,CustID,RoomID,CostID,ReceID,RecdID,ParkID,HandID,IsHisFees,FeesStateDate,FeesEndDate,ChargeAmount ");
			strSql.Append(" FROM Tb_HSPR_ParkingCarSignFeesDetail ");
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
			parameters[5].Value = "SELECT * FROM Tb_HSPR_ParkingCarSignFeesDetail WHERE 1=1 " + StrCondition;
			parameters[6].Value = "IID";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

