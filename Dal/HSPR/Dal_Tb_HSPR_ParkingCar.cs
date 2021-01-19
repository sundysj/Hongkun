using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.HSPR
{
	/// <summary>
	/// 数据访问类Dal_Tb_HSPR_ParkingCar。
	/// </summary>
	public class Dal_Tb_HSPR_ParkingCar
	{
		public Dal_Tb_HSPR_ParkingCar()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public long GetMaxId()
		{
		return DbHelperSQL.GetMaxID("CarID", "Tb_HSPR_ParkingCar"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long CarID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@CarID", SqlDbType.BigInt)};
			parameters[0].Value = CarID;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_HSPR_ParkingCar_Exists",parameters,out rowsAffected);
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
		public void Add(MobileSoft.Model.HSPR.Tb_HSPR_ParkingCar model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@CarID", SqlDbType.BigInt,8),
					new SqlParameter("@HandID", SqlDbType.BigInt,8),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@ParkID", SqlDbType.BigInt,8),
					new SqlParameter("@CustID", SqlDbType.BigInt,8),
					new SqlParameter("@RoomSign", SqlDbType.VarChar,50),
					new SqlParameter("@ParkingCarSign", SqlDbType.NVarChar,20),
					new SqlParameter("@CarSign", SqlDbType.NVarChar,20),
					new SqlParameter("@CarType", SqlDbType.NVarChar,30),
					new SqlParameter("@CarEngineSign", SqlDbType.NVarChar,30),
					new SqlParameter("@CarShelfSign", SqlDbType.NVarChar,30),
					new SqlParameter("@FacBrands", SqlDbType.NVarChar,30),
					new SqlParameter("@Weight", SqlDbType.VarChar,30),
					new SqlParameter("@Deadweight", SqlDbType.NVarChar,20),
					new SqlParameter("@Passenger", SqlDbType.NVarChar,20),
					new SqlParameter("@FrontPass", SqlDbType.NVarChar,20),
					new SqlParameter("@CarRegDate", SqlDbType.DateTime),
					new SqlParameter("@CarGrantDate", SqlDbType.DateTime),
					new SqlParameter("@CarInsurer", SqlDbType.NVarChar,50),
					new SqlParameter("@CarInsSign", SqlDbType.NVarChar,20),
					new SqlParameter("@CarInsContent", SqlDbType.NVarChar,300),
					new SqlParameter("@CarEmission", SqlDbType.NVarChar,20),
					new SqlParameter("@CarColor", SqlDbType.NVarChar,20),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2)};
			parameters[0].Value = model.CarID;
			parameters[1].Value = model.HandID;
			parameters[2].Value = model.CommID;
			parameters[3].Value = model.ParkID;
			parameters[4].Value = model.CustID;
			parameters[5].Value = model.RoomSign;
			parameters[6].Value = model.ParkingCarSign;
			parameters[7].Value = model.CarSign;
			parameters[8].Value = model.CarType;
			parameters[9].Value = model.CarEngineSign;
			parameters[10].Value = model.CarShelfSign;
			parameters[11].Value = model.FacBrands;
			parameters[12].Value = model.Weight;
			parameters[13].Value = model.Deadweight;
			parameters[14].Value = model.Passenger;
			parameters[15].Value = model.FrontPass;
			parameters[16].Value = model.CarRegDate;
			parameters[17].Value = model.CarGrantDate;
			parameters[18].Value = model.CarInsurer;
			parameters[19].Value = model.CarInsSign;
			parameters[20].Value = model.CarInsContent;
			parameters[21].Value = model.CarEmission;
			parameters[22].Value = model.CarColor;
			parameters[23].Value = model.IsDelete;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_ParkingCar_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.HSPR.Tb_HSPR_ParkingCar model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@CarID", SqlDbType.BigInt,8),
					new SqlParameter("@HandID", SqlDbType.BigInt,8),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@ParkID", SqlDbType.BigInt,8),
					new SqlParameter("@CustID", SqlDbType.BigInt,8),
					new SqlParameter("@RoomSign", SqlDbType.VarChar,50),
					new SqlParameter("@ParkingCarSign", SqlDbType.NVarChar,20),
					new SqlParameter("@CarSign", SqlDbType.NVarChar,20),
					new SqlParameter("@CarType", SqlDbType.NVarChar,30),
					new SqlParameter("@CarEngineSign", SqlDbType.NVarChar,30),
					new SqlParameter("@CarShelfSign", SqlDbType.NVarChar,30),
					new SqlParameter("@FacBrands", SqlDbType.NVarChar,30),
					new SqlParameter("@Weight", SqlDbType.VarChar,30),
					new SqlParameter("@Deadweight", SqlDbType.NVarChar,20),
					new SqlParameter("@Passenger", SqlDbType.NVarChar,20),
					new SqlParameter("@FrontPass", SqlDbType.NVarChar,20),
					new SqlParameter("@CarRegDate", SqlDbType.DateTime),
					new SqlParameter("@CarGrantDate", SqlDbType.DateTime),
					new SqlParameter("@CarInsurer", SqlDbType.NVarChar,50),
					new SqlParameter("@CarInsSign", SqlDbType.NVarChar,20),
					new SqlParameter("@CarInsContent", SqlDbType.NVarChar,300),
					new SqlParameter("@CarEmission", SqlDbType.NVarChar,20),
					new SqlParameter("@CarColor", SqlDbType.NVarChar,20),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2)};
			parameters[0].Value = model.CarID;
			parameters[1].Value = model.HandID;
			parameters[2].Value = model.CommID;
			parameters[3].Value = model.ParkID;
			parameters[4].Value = model.CustID;
			parameters[5].Value = model.RoomSign;
			parameters[6].Value = model.ParkingCarSign;
			parameters[7].Value = model.CarSign;
			parameters[8].Value = model.CarType;
			parameters[9].Value = model.CarEngineSign;
			parameters[10].Value = model.CarShelfSign;
			parameters[11].Value = model.FacBrands;
			parameters[12].Value = model.Weight;
			parameters[13].Value = model.Deadweight;
			parameters[14].Value = model.Passenger;
			parameters[15].Value = model.FrontPass;
			parameters[16].Value = model.CarRegDate;
			parameters[17].Value = model.CarGrantDate;
			parameters[18].Value = model.CarInsurer;
			parameters[19].Value = model.CarInsSign;
			parameters[20].Value = model.CarInsContent;
			parameters[21].Value = model.CarEmission;
			parameters[22].Value = model.CarColor;
			parameters[23].Value = model.IsDelete;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_ParkingCar_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long CarID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@CarID", SqlDbType.BigInt)};
			parameters[0].Value = CarID;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_ParkingCar_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_ParkingCar GetModel(long CarID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@CarID", SqlDbType.BigInt)};
			parameters[0].Value = CarID;

			MobileSoft.Model.HSPR.Tb_HSPR_ParkingCar model=new MobileSoft.Model.HSPR.Tb_HSPR_ParkingCar();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_HSPR_ParkingCar_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["CarID"].ToString()!="")
				{
					model.CarID=long.Parse(ds.Tables[0].Rows[0]["CarID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["HandID"].ToString()!="")
				{
					model.HandID=long.Parse(ds.Tables[0].Rows[0]["HandID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CommID"].ToString()!="")
				{
					model.CommID=int.Parse(ds.Tables[0].Rows[0]["CommID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ParkID"].ToString()!="")
				{
					model.ParkID=long.Parse(ds.Tables[0].Rows[0]["ParkID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CustID"].ToString()!="")
				{
					model.CustID=long.Parse(ds.Tables[0].Rows[0]["CustID"].ToString());
				}
				model.RoomSign=ds.Tables[0].Rows[0]["RoomSign"].ToString();
				model.ParkingCarSign=ds.Tables[0].Rows[0]["ParkingCarSign"].ToString();
				model.CarSign=ds.Tables[0].Rows[0]["CarSign"].ToString();
				model.CarType=ds.Tables[0].Rows[0]["CarType"].ToString();
				model.CarEngineSign=ds.Tables[0].Rows[0]["CarEngineSign"].ToString();
				model.CarShelfSign=ds.Tables[0].Rows[0]["CarShelfSign"].ToString();
				model.FacBrands=ds.Tables[0].Rows[0]["FacBrands"].ToString();
				model.Weight=ds.Tables[0].Rows[0]["Weight"].ToString();
				model.Deadweight=ds.Tables[0].Rows[0]["Deadweight"].ToString();
				model.Passenger=ds.Tables[0].Rows[0]["Passenger"].ToString();
				model.FrontPass=ds.Tables[0].Rows[0]["FrontPass"].ToString();
				if(ds.Tables[0].Rows[0]["CarRegDate"].ToString()!="")
				{
					model.CarRegDate=DateTime.Parse(ds.Tables[0].Rows[0]["CarRegDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CarGrantDate"].ToString()!="")
				{
					model.CarGrantDate=DateTime.Parse(ds.Tables[0].Rows[0]["CarGrantDate"].ToString());
				}
				model.CarInsurer=ds.Tables[0].Rows[0]["CarInsurer"].ToString();
				model.CarInsSign=ds.Tables[0].Rows[0]["CarInsSign"].ToString();
				model.CarInsContent=ds.Tables[0].Rows[0]["CarInsContent"].ToString();
				model.CarEmission=ds.Tables[0].Rows[0]["CarEmission"].ToString();
				model.CarColor=ds.Tables[0].Rows[0]["CarColor"].ToString();
				if(ds.Tables[0].Rows[0]["IsDelete"].ToString()!="")
				{
					model.IsDelete=int.Parse(ds.Tables[0].Rows[0]["IsDelete"].ToString());
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
			strSql.Append("select CarID,HandID,CommID,ParkID,CustID,RoomSign,ParkingCarSign,CarSign,CarType,CarEngineSign,CarShelfSign,FacBrands,Weight,Deadweight,Passenger,FrontPass,CarRegDate,CarGrantDate,CarInsurer,CarInsSign,CarInsContent,CarEmission,CarColor,IsDelete ");
			strSql.Append(" FROM Tb_HSPR_ParkingCar ");
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
			strSql.Append(" CarID,HandID,CommID,ParkID,CustID,RoomSign,ParkingCarSign,CarSign,CarType,CarEngineSign,CarShelfSign,FacBrands,Weight,Deadweight,Passenger,FrontPass,CarRegDate,CarGrantDate,CarInsurer,CarInsSign,CarInsContent,CarEmission,CarColor,IsDelete ");
			strSql.Append(" FROM Tb_HSPR_ParkingCar ");
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
			parameters[5].Value = "SELECT * FROM Tb_HSPR_ParkingCar WHERE 1=1 " + StrCondition;
			parameters[6].Value = "CarID";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

