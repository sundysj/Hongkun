using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.HSPR
{
	/// <summary>
	/// 数据访问类Dal_Tb_HSPR_ParkingHand。
	/// </summary>
	public class Dal_Tb_HSPR_ParkingHand
	{
		public Dal_Tb_HSPR_ParkingHand()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public long GetMaxId()
		{
		return DbHelperSQL.GetMaxID("HandID", "Tb_HSPR_ParkingHand"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long HandID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@HandID", SqlDbType.BigInt)};
			parameters[0].Value = HandID;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_HSPR_ParkingHand_Exists",parameters,out rowsAffected);
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
		public void Add(MobileSoft.Model.HSPR.Tb_HSPR_ParkingHand model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@HandID", SqlDbType.BigInt,8),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@ParkID", SqlDbType.BigInt,8),
					new SqlParameter("@CustID", SqlDbType.BigInt,8),
					new SqlParameter("@RoomID", SqlDbType.BigInt,8),
					new SqlParameter("@ApplyDate", SqlDbType.DateTime),
					new SqlParameter("@InfoSource", SqlDbType.NVarChar,20),
					new SqlParameter("@UseProperty", SqlDbType.VarChar,30),
					new SqlParameter("@ParkStartDate", SqlDbType.DateTime),
					new SqlParameter("@ParkEndDate", SqlDbType.DateTime),
					new SqlParameter("@PayPeriod", SqlDbType.NVarChar,10),
					new SqlParameter("@NextPayDate", SqlDbType.DateTime),
					new SqlParameter("@RentMode", SqlDbType.NVarChar,10),
					new SqlParameter("@Contact", SqlDbType.VarChar,50),
					new SqlParameter("@handling", SqlDbType.NVarChar,20),
					new SqlParameter("@HandDate", SqlDbType.DateTime),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2),
					new SqlParameter("@IsSubmit", SqlDbType.SmallInt,2),
					new SqlParameter("@ParkingCarSign", SqlDbType.NVarChar,200),
					new SqlParameter("@CarSign", SqlDbType.NVarChar,20),
					new SqlParameter("@CarType", SqlDbType.NVarChar,30),
					new SqlParameter("@FacBrands", SqlDbType.NVarChar,30),
					new SqlParameter("@CarColor", SqlDbType.NVarChar,20),
					new SqlParameter("@CarEmission", SqlDbType.NVarChar,20),
					new SqlParameter("@Phone", SqlDbType.NVarChar,20),
					new SqlParameter("@IsUse", SqlDbType.SmallInt,2),
					new SqlParameter("@IsInput", SqlDbType.SmallInt,2),
					new SqlParameter("@NextDate", SqlDbType.DateTime),
					new SqlParameter("@HandSynchCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@SynchFlag", SqlDbType.SmallInt,2),
					new SqlParameter("@IsListing", SqlDbType.SmallInt,2),
					new SqlParameter("@ListingData", SqlDbType.DateTime)};
			parameters[0].Value = model.HandID;
			parameters[1].Value = model.CommID;
			parameters[2].Value = model.ParkID;
			parameters[3].Value = model.CustID;
			parameters[4].Value = model.RoomID;
			parameters[5].Value = model.ApplyDate;
			parameters[6].Value = model.InfoSource;
			parameters[7].Value = model.UseProperty;
			parameters[8].Value = model.ParkStartDate;
			parameters[9].Value = model.ParkEndDate;
			parameters[10].Value = model.PayPeriod;
			parameters[11].Value = model.NextPayDate;
			parameters[12].Value = model.RentMode;
			parameters[13].Value = model.Contact;
			parameters[14].Value = model.handling;
			parameters[15].Value = model.HandDate;
			parameters[16].Value = model.IsDelete;
			parameters[17].Value = model.IsSubmit;
			parameters[18].Value = model.ParkingCarSign;
			parameters[19].Value = model.CarSign;
			parameters[20].Value = model.CarType;
			parameters[21].Value = model.FacBrands;
			parameters[22].Value = model.CarColor;
			parameters[23].Value = model.CarEmission;
			parameters[24].Value = model.Phone;
			parameters[25].Value = model.IsUse;
			parameters[26].Value = model.IsInput;
			parameters[27].Value = model.NextDate;
			parameters[28].Value = model.HandSynchCode;
			parameters[29].Value = model.SynchFlag;
			parameters[30].Value = model.IsListing;
			parameters[31].Value = model.ListingData;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_ParkingHand_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.HSPR.Tb_HSPR_ParkingHand model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@HandID", SqlDbType.BigInt,8),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@ParkID", SqlDbType.BigInt,8),
					new SqlParameter("@CustID", SqlDbType.BigInt,8),
					new SqlParameter("@RoomID", SqlDbType.BigInt,8),
					new SqlParameter("@ApplyDate", SqlDbType.DateTime),
					new SqlParameter("@InfoSource", SqlDbType.NVarChar,20),
					new SqlParameter("@UseProperty", SqlDbType.VarChar,30),
					new SqlParameter("@ParkStartDate", SqlDbType.DateTime),
					new SqlParameter("@ParkEndDate", SqlDbType.DateTime),
					new SqlParameter("@PayPeriod", SqlDbType.NVarChar,10),
					new SqlParameter("@NextPayDate", SqlDbType.DateTime),
					new SqlParameter("@RentMode", SqlDbType.NVarChar,10),
					new SqlParameter("@Contact", SqlDbType.VarChar,50),
					new SqlParameter("@handling", SqlDbType.NVarChar,20),
					new SqlParameter("@HandDate", SqlDbType.DateTime),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2),
					new SqlParameter("@IsSubmit", SqlDbType.SmallInt,2),
					new SqlParameter("@ParkingCarSign", SqlDbType.NVarChar,200),
					new SqlParameter("@CarSign", SqlDbType.NVarChar,20),
					new SqlParameter("@CarType", SqlDbType.NVarChar,30),
					new SqlParameter("@FacBrands", SqlDbType.NVarChar,30),
					new SqlParameter("@CarColor", SqlDbType.NVarChar,20),
					new SqlParameter("@CarEmission", SqlDbType.NVarChar,20),
					new SqlParameter("@Phone", SqlDbType.NVarChar,20),
					new SqlParameter("@IsUse", SqlDbType.SmallInt,2),
					new SqlParameter("@IsInput", SqlDbType.SmallInt,2),
					new SqlParameter("@NextDate", SqlDbType.DateTime),
					new SqlParameter("@HandSynchCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@SynchFlag", SqlDbType.SmallInt,2),
					new SqlParameter("@IsListing", SqlDbType.SmallInt,2),
					new SqlParameter("@ListingData", SqlDbType.DateTime)};
			parameters[0].Value = model.HandID;
			parameters[1].Value = model.CommID;
			parameters[2].Value = model.ParkID;
			parameters[3].Value = model.CustID;
			parameters[4].Value = model.RoomID;
			parameters[5].Value = model.ApplyDate;
			parameters[6].Value = model.InfoSource;
			parameters[7].Value = model.UseProperty;
			parameters[8].Value = model.ParkStartDate;
			parameters[9].Value = model.ParkEndDate;
			parameters[10].Value = model.PayPeriod;
			parameters[11].Value = model.NextPayDate;
			parameters[12].Value = model.RentMode;
			parameters[13].Value = model.Contact;
			parameters[14].Value = model.handling;
			parameters[15].Value = model.HandDate;
			parameters[16].Value = model.IsDelete;
			parameters[17].Value = model.IsSubmit;
			parameters[18].Value = model.ParkingCarSign;
			parameters[19].Value = model.CarSign;
			parameters[20].Value = model.CarType;
			parameters[21].Value = model.FacBrands;
			parameters[22].Value = model.CarColor;
			parameters[23].Value = model.CarEmission;
			parameters[24].Value = model.Phone;
			parameters[25].Value = model.IsUse;
			parameters[26].Value = model.IsInput;
			parameters[27].Value = model.NextDate;
			parameters[28].Value = model.HandSynchCode;
			parameters[29].Value = model.SynchFlag;
			parameters[30].Value = model.IsListing;
			parameters[31].Value = model.ListingData;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_ParkingHand_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long HandID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@HandID", SqlDbType.BigInt)};
			parameters[0].Value = HandID;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_ParkingHand_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_ParkingHand GetModel(long HandID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@HandID", SqlDbType.BigInt)};
			parameters[0].Value = HandID;

			MobileSoft.Model.HSPR.Tb_HSPR_ParkingHand model=new MobileSoft.Model.HSPR.Tb_HSPR_ParkingHand();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_HSPR_ParkingHand_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
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
				if(ds.Tables[0].Rows[0]["RoomID"].ToString()!="")
				{
					model.RoomID=long.Parse(ds.Tables[0].Rows[0]["RoomID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ApplyDate"].ToString()!="")
				{
					model.ApplyDate=DateTime.Parse(ds.Tables[0].Rows[0]["ApplyDate"].ToString());
				}
				model.InfoSource=ds.Tables[0].Rows[0]["InfoSource"].ToString();
				model.UseProperty=ds.Tables[0].Rows[0]["UseProperty"].ToString();
				if(ds.Tables[0].Rows[0]["ParkStartDate"].ToString()!="")
				{
					model.ParkStartDate=DateTime.Parse(ds.Tables[0].Rows[0]["ParkStartDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ParkEndDate"].ToString()!="")
				{
					model.ParkEndDate=DateTime.Parse(ds.Tables[0].Rows[0]["ParkEndDate"].ToString());
				}
				model.PayPeriod=ds.Tables[0].Rows[0]["PayPeriod"].ToString();
				if(ds.Tables[0].Rows[0]["NextPayDate"].ToString()!="")
				{
					model.NextPayDate=DateTime.Parse(ds.Tables[0].Rows[0]["NextPayDate"].ToString());
				}
				model.RentMode=ds.Tables[0].Rows[0]["RentMode"].ToString();
				model.Contact=ds.Tables[0].Rows[0]["Contact"].ToString();
				model.handling=ds.Tables[0].Rows[0]["handling"].ToString();
				if(ds.Tables[0].Rows[0]["HandDate"].ToString()!="")
				{
					model.HandDate=DateTime.Parse(ds.Tables[0].Rows[0]["HandDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsDelete"].ToString()!="")
				{
					model.IsDelete=int.Parse(ds.Tables[0].Rows[0]["IsDelete"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsSubmit"].ToString()!="")
				{
					model.IsSubmit=int.Parse(ds.Tables[0].Rows[0]["IsSubmit"].ToString());
				}
				model.ParkingCarSign=ds.Tables[0].Rows[0]["ParkingCarSign"].ToString();
				model.CarSign=ds.Tables[0].Rows[0]["CarSign"].ToString();
				model.CarType=ds.Tables[0].Rows[0]["CarType"].ToString();
				model.FacBrands=ds.Tables[0].Rows[0]["FacBrands"].ToString();
				model.CarColor=ds.Tables[0].Rows[0]["CarColor"].ToString();
				model.CarEmission=ds.Tables[0].Rows[0]["CarEmission"].ToString();
				model.Phone=ds.Tables[0].Rows[0]["Phone"].ToString();
				if(ds.Tables[0].Rows[0]["IsUse"].ToString()!="")
				{
					model.IsUse=int.Parse(ds.Tables[0].Rows[0]["IsUse"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsInput"].ToString()!="")
				{
					model.IsInput=int.Parse(ds.Tables[0].Rows[0]["IsInput"].ToString());
				}
				if(ds.Tables[0].Rows[0]["NextDate"].ToString()!="")
				{
					model.NextDate=DateTime.Parse(ds.Tables[0].Rows[0]["NextDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["HandSynchCode"].ToString()!="")
				{
					model.HandSynchCode=new Guid(ds.Tables[0].Rows[0]["HandSynchCode"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SynchFlag"].ToString()!="")
				{
					model.SynchFlag=int.Parse(ds.Tables[0].Rows[0]["SynchFlag"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsListing"].ToString()!="")
				{
					model.IsListing=int.Parse(ds.Tables[0].Rows[0]["IsListing"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ListingData"].ToString()!="")
				{
					model.ListingData=DateTime.Parse(ds.Tables[0].Rows[0]["ListingData"].ToString());
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
			strSql.Append(" FROM View_HSPR_ParkingHand_Filter ");
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
            strSql.Append(" FROM View_HSPR_ParkingHand_Filter ");
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
			parameters[5].Value = "SELECT * FROM Tb_HSPR_ParkingHand WHERE 1=1 " + StrCondition;
			parameters[6].Value = "HandID";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

