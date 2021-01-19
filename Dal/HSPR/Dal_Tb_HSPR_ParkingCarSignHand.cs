using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.HSPR
{
	/// <summary>
	/// 数据访问类Dal_Tb_HSPR_ParkingCarSignHand。
	/// </summary>
	public class Dal_Tb_HSPR_ParkingCarSignHand
	{
		public Dal_Tb_HSPR_ParkingCarSignHand()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public long GetMaxId()
		{
		return DbHelperSQL.GetMaxID("CarSignID", "Tb_HSPR_ParkingCarSignHand"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long CarSignID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@CarSignID", SqlDbType.BigInt)};
			parameters[0].Value = CarSignID;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_HSPR_ParkingCarSignHand_Exists",parameters,out rowsAffected);
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
		public long Add(MobileSoft.Model.HSPR.Tb_HSPR_ParkingCarSignHand model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@CarSignID", SqlDbType.BigInt,8),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@CustID", SqlDbType.BigInt,8),
					new SqlParameter("@RoomID", SqlDbType.BigInt,8),
					new SqlParameter("@ReceID", SqlDbType.BigInt,8),
					new SqlParameter("@ParkID", SqlDbType.BigInt,8),
					new SqlParameter("@HandID", SqlDbType.BigInt,8),
					new SqlParameter("@CostID", SqlDbType.BigInt,8),
					new SqlParameter("@FeesStateDate", SqlDbType.DateTime),
					new SqlParameter("@FeesEndDate", SqlDbType.DateTime),
					new SqlParameter("@ChargeAmount", SqlDbType.Decimal,9),
					new SqlParameter("@SerialNum", SqlDbType.NVarChar,20),
					new SqlParameter("@BarrierSign", SqlDbType.NVarChar,20),
					new SqlParameter("@IsHisFees", SqlDbType.SmallInt,2),
					new SqlParameter("@IsHand", SqlDbType.SmallInt,2),
					new SqlParameter("@AcceptUserCode", SqlDbType.NVarChar,20),
					new SqlParameter("@HandUserCode", SqlDbType.NVarChar,20),
					new SqlParameter("@HandDate", SqlDbType.DateTime),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2),
					new SqlParameter("@SubmitDate", SqlDbType.DateTime)};
			parameters[0].Direction = ParameterDirection.Output;
			parameters[1].Value = model.CommID;
			parameters[2].Value = model.CustID;
			parameters[3].Value = model.RoomID;
			parameters[4].Value = model.ReceID;
			parameters[5].Value = model.ParkID;
			parameters[6].Value = model.HandID;
			parameters[7].Value = model.CostID;
			parameters[8].Value = model.FeesStateDate;
			parameters[9].Value = model.FeesEndDate;
			parameters[10].Value = model.ChargeAmount;
			parameters[11].Value = model.SerialNum;
			parameters[12].Value = model.BarrierSign;
			parameters[13].Value = model.IsHisFees;
			parameters[14].Value = model.IsHand;
			parameters[15].Value = model.AcceptUserCode;
			parameters[16].Value = model.HandUserCode;
			parameters[17].Value = model.HandDate;
			parameters[18].Value = model.IsDelete;
			parameters[19].Value = model.SubmitDate;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_ParkingCarSignHand_ADD",parameters,out rowsAffected);
			return (long)parameters[0].Value;
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.HSPR.Tb_HSPR_ParkingCarSignHand model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@CarSignID", SqlDbType.BigInt,8),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@CustID", SqlDbType.BigInt,8),
					new SqlParameter("@RoomID", SqlDbType.BigInt,8),
					new SqlParameter("@ReceID", SqlDbType.BigInt,8),
					new SqlParameter("@ParkID", SqlDbType.BigInt,8),
					new SqlParameter("@HandID", SqlDbType.BigInt,8),
					new SqlParameter("@CostID", SqlDbType.BigInt,8),
					new SqlParameter("@FeesStateDate", SqlDbType.DateTime),
					new SqlParameter("@FeesEndDate", SqlDbType.DateTime),
					new SqlParameter("@ChargeAmount", SqlDbType.Decimal,9),
					new SqlParameter("@SerialNum", SqlDbType.NVarChar,20),
					new SqlParameter("@BarrierSign", SqlDbType.NVarChar,20),
					new SqlParameter("@IsHisFees", SqlDbType.SmallInt,2),
					new SqlParameter("@IsHand", SqlDbType.SmallInt,2),
					new SqlParameter("@AcceptUserCode", SqlDbType.NVarChar,20),
					new SqlParameter("@HandUserCode", SqlDbType.NVarChar,20),
					new SqlParameter("@HandDate", SqlDbType.DateTime),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2),
					new SqlParameter("@SubmitDate", SqlDbType.DateTime)};
			parameters[0].Value = model.CarSignID;
			parameters[1].Value = model.CommID;
			parameters[2].Value = model.CustID;
			parameters[3].Value = model.RoomID;
			parameters[4].Value = model.ReceID;
			parameters[5].Value = model.ParkID;
			parameters[6].Value = model.HandID;
			parameters[7].Value = model.CostID;
			parameters[8].Value = model.FeesStateDate;
			parameters[9].Value = model.FeesEndDate;
			parameters[10].Value = model.ChargeAmount;
			parameters[11].Value = model.SerialNum;
			parameters[12].Value = model.BarrierSign;
			parameters[13].Value = model.IsHisFees;
			parameters[14].Value = model.IsHand;
			parameters[15].Value = model.AcceptUserCode;
			parameters[16].Value = model.HandUserCode;
			parameters[17].Value = model.HandDate;
			parameters[18].Value = model.IsDelete;
			parameters[19].Value = model.SubmitDate;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_ParkingCarSignHand_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long CarSignID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@CarSignID", SqlDbType.BigInt)};
			parameters[0].Value = CarSignID;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_ParkingCarSignHand_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_ParkingCarSignHand GetModel(long CarSignID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@CarSignID", SqlDbType.BigInt)};
			parameters[0].Value = CarSignID;

			MobileSoft.Model.HSPR.Tb_HSPR_ParkingCarSignHand model=new MobileSoft.Model.HSPR.Tb_HSPR_ParkingCarSignHand();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_HSPR_ParkingCarSignHand_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["CarSignID"].ToString()!="")
				{
					model.CarSignID=long.Parse(ds.Tables[0].Rows[0]["CarSignID"].ToString());
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
				if(ds.Tables[0].Rows[0]["ReceID"].ToString()!="")
				{
					model.ReceID=long.Parse(ds.Tables[0].Rows[0]["ReceID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ParkID"].ToString()!="")
				{
					model.ParkID=long.Parse(ds.Tables[0].Rows[0]["ParkID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["HandID"].ToString()!="")
				{
					model.HandID=long.Parse(ds.Tables[0].Rows[0]["HandID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CostID"].ToString()!="")
				{
					model.CostID=long.Parse(ds.Tables[0].Rows[0]["CostID"].ToString());
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
				model.SerialNum=ds.Tables[0].Rows[0]["SerialNum"].ToString();
				model.BarrierSign=ds.Tables[0].Rows[0]["BarrierSign"].ToString();
				if(ds.Tables[0].Rows[0]["IsHisFees"].ToString()!="")
				{
					model.IsHisFees=int.Parse(ds.Tables[0].Rows[0]["IsHisFees"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsHand"].ToString()!="")
				{
					model.IsHand=int.Parse(ds.Tables[0].Rows[0]["IsHand"].ToString());
				}
				model.AcceptUserCode=ds.Tables[0].Rows[0]["AcceptUserCode"].ToString();
				model.HandUserCode=ds.Tables[0].Rows[0]["HandUserCode"].ToString();
				if(ds.Tables[0].Rows[0]["HandDate"].ToString()!="")
				{
					model.HandDate=DateTime.Parse(ds.Tables[0].Rows[0]["HandDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsDelete"].ToString()!="")
				{
					model.IsDelete=int.Parse(ds.Tables[0].Rows[0]["IsDelete"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SubmitDate"].ToString()!="")
				{
					model.SubmitDate=DateTime.Parse(ds.Tables[0].Rows[0]["SubmitDate"].ToString());
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
			strSql.Append("select CarSignID,CommID,CustID,RoomID,ReceID,ParkID,HandID,CostID,FeesStateDate,FeesEndDate,ChargeAmount,SerialNum,BarrierSign,IsHisFees,IsHand,AcceptUserCode,HandUserCode,HandDate,IsDelete,SubmitDate ");
			strSql.Append(" FROM Tb_HSPR_ParkingCarSignHand ");
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
			strSql.Append(" CarSignID,CommID,CustID,RoomID,ReceID,ParkID,HandID,CostID,FeesStateDate,FeesEndDate,ChargeAmount,SerialNum,BarrierSign,IsHisFees,IsHand,AcceptUserCode,HandUserCode,HandDate,IsDelete,SubmitDate ");
			strSql.Append(" FROM Tb_HSPR_ParkingCarSignHand ");
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
			parameters[5].Value = "SELECT * FROM Tb_HSPR_ParkingCarSignHand WHERE 1=1 " + StrCondition;
			parameters[6].Value = "CarSignID";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

