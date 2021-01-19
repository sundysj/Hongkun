using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.HSPR
{
	/// <summary>
	/// 数据访问类Dal_Tb_HSPR_ParkingHis。
	/// </summary>
	public class Dal_Tb_HSPR_ParkingHis
	{
		public Dal_Tb_HSPR_ParkingHis()
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

			int result= DbHelperSQL.RunProcedure("Proc_Tb_HSPR_ParkingHis_Exists",parameters,out rowsAffected);
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
		public long Add(MobileSoft.Model.HSPR.Tb_HSPR_ParkingHis model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@IID", SqlDbType.BigInt,8),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@ParkID", SqlDbType.BigInt,8),
					new SqlParameter("@OldCustID", SqlDbType.BigInt,8),
					new SqlParameter("@OldRoomID", SqlDbType.BigInt,8),
					new SqlParameter("@CustID", SqlDbType.BigInt,8),
					new SqlParameter("@RoomID", SqlDbType.BigInt,8),
					new SqlParameter("@UserCode", SqlDbType.NVarChar,20),
					new SqlParameter("@ChangeDate", SqlDbType.DateTime),
					new SqlParameter("@ChangeMemo", SqlDbType.NVarChar,100)};
			parameters[0].Direction = ParameterDirection.Output;
			parameters[1].Value = model.CommID;
			parameters[2].Value = model.ParkID;
			parameters[3].Value = model.OldCustID;
			parameters[4].Value = model.OldRoomID;
			parameters[5].Value = model.CustID;
			parameters[6].Value = model.RoomID;
			parameters[7].Value = model.UserCode;
			parameters[8].Value = model.ChangeDate;
			parameters[9].Value = model.ChangeMemo;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_ParkingHis_ADD",parameters,out rowsAffected);
			return (long)parameters[0].Value;
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.HSPR.Tb_HSPR_ParkingHis model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@IID", SqlDbType.BigInt,8),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@ParkID", SqlDbType.BigInt,8),
					new SqlParameter("@OldCustID", SqlDbType.BigInt,8),
					new SqlParameter("@OldRoomID", SqlDbType.BigInt,8),
					new SqlParameter("@CustID", SqlDbType.BigInt,8),
					new SqlParameter("@RoomID", SqlDbType.BigInt,8),
					new SqlParameter("@UserCode", SqlDbType.NVarChar,20),
					new SqlParameter("@ChangeDate", SqlDbType.DateTime),
					new SqlParameter("@ChangeMemo", SqlDbType.NVarChar,100)};
			parameters[0].Value = model.IID;
			parameters[1].Value = model.CommID;
			parameters[2].Value = model.ParkID;
			parameters[3].Value = model.OldCustID;
			parameters[4].Value = model.OldRoomID;
			parameters[5].Value = model.CustID;
			parameters[6].Value = model.RoomID;
			parameters[7].Value = model.UserCode;
			parameters[8].Value = model.ChangeDate;
			parameters[9].Value = model.ChangeMemo;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_ParkingHis_Update",parameters,out rowsAffected);
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

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_ParkingHis_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_ParkingHis GetModel(long IID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@IID", SqlDbType.BigInt)};
			parameters[0].Value = IID;

			MobileSoft.Model.HSPR.Tb_HSPR_ParkingHis model=new MobileSoft.Model.HSPR.Tb_HSPR_ParkingHis();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_HSPR_ParkingHis_GetModel",parameters,"ds");
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
				if(ds.Tables[0].Rows[0]["ParkID"].ToString()!="")
				{
					model.ParkID=long.Parse(ds.Tables[0].Rows[0]["ParkID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["OldCustID"].ToString()!="")
				{
					model.OldCustID=long.Parse(ds.Tables[0].Rows[0]["OldCustID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["OldRoomID"].ToString()!="")
				{
					model.OldRoomID=long.Parse(ds.Tables[0].Rows[0]["OldRoomID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CustID"].ToString()!="")
				{
					model.CustID=long.Parse(ds.Tables[0].Rows[0]["CustID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["RoomID"].ToString()!="")
				{
					model.RoomID=long.Parse(ds.Tables[0].Rows[0]["RoomID"].ToString());
				}
				model.UserCode=ds.Tables[0].Rows[0]["UserCode"].ToString();
				if(ds.Tables[0].Rows[0]["ChangeDate"].ToString()!="")
				{
					model.ChangeDate=DateTime.Parse(ds.Tables[0].Rows[0]["ChangeDate"].ToString());
				}
				model.ChangeMemo=ds.Tables[0].Rows[0]["ChangeMemo"].ToString();
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
			strSql.Append("select IID,CommID,ParkID,OldCustID,OldRoomID,CustID,RoomID,UserCode,ChangeDate,ChangeMemo ");
			strSql.Append(" FROM Tb_HSPR_ParkingHis ");
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
			strSql.Append(" IID,CommID,ParkID,OldCustID,OldRoomID,CustID,RoomID,UserCode,ChangeDate,ChangeMemo ");
			strSql.Append(" FROM Tb_HSPR_ParkingHis ");
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
			parameters[5].Value = "SELECT * FROM Tb_HSPR_ParkingHis WHERE 1=1 " + StrCondition;
			parameters[6].Value = "IID";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

