using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.HSPR
{
	/// <summary>
	/// 数据访问类Dal_Tb_HSPR_ParkingCancel。
	/// </summary>
	public class Dal_Tb_HSPR_ParkingCancel
	{
		public Dal_Tb_HSPR_ParkingCancel()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public long GetMaxId()
		{
		return DbHelperSQL.GetMaxID("IID", "Tb_HSPR_ParkingCancel"); 
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

			int result= DbHelperSQL.RunProcedure("Proc_Tb_HSPR_ParkingCancel_Exists",parameters,out rowsAffected);
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
		public long Add(MobileSoft.Model.HSPR.Tb_HSPR_ParkingCancel model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@IID", SqlDbType.BigInt,8),
					new SqlParameter("@HandID", SqlDbType.BigInt,8),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@ParkID", SqlDbType.BigInt,8),
					new SqlParameter("@CustID", SqlDbType.BigInt,8),
					new SqlParameter("@RoomID", SqlDbType.BigInt,8),
					new SqlParameter("@CancelCause", SqlDbType.NVarChar,50),
					new SqlParameter("@CancelDate", SqlDbType.DateTime),
					new SqlParameter("@Handling", SqlDbType.NVarChar,20),
					new SqlParameter("@HandDate", SqlDbType.DateTime)};
			parameters[0].Direction = ParameterDirection.Output;
			parameters[1].Value = model.HandID;
			parameters[2].Value = model.CommID;
			parameters[3].Value = model.ParkID;
			parameters[4].Value = model.CustID;
			parameters[5].Value = model.RoomID;
			parameters[6].Value = model.CancelCause;
			parameters[7].Value = model.CancelDate;
			parameters[8].Value = model.Handling;
			parameters[9].Value = model.HandDate;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_ParkingCancel_ADD",parameters,out rowsAffected);
			return (long)parameters[0].Value;
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.HSPR.Tb_HSPR_ParkingCancel model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@IID", SqlDbType.BigInt,8),
					new SqlParameter("@HandID", SqlDbType.BigInt,8),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@ParkID", SqlDbType.BigInt,8),
					new SqlParameter("@CustID", SqlDbType.BigInt,8),
					new SqlParameter("@RoomID", SqlDbType.BigInt,8),
					new SqlParameter("@CancelCause", SqlDbType.NVarChar,50),
					new SqlParameter("@CancelDate", SqlDbType.DateTime),
					new SqlParameter("@Handling", SqlDbType.NVarChar,20),
					new SqlParameter("@HandDate", SqlDbType.DateTime)};
			parameters[0].Value = model.IID;
			parameters[1].Value = model.HandID;
			parameters[2].Value = model.CommID;
			parameters[3].Value = model.ParkID;
			parameters[4].Value = model.CustID;
			parameters[5].Value = model.RoomID;
			parameters[6].Value = model.CancelCause;
			parameters[7].Value = model.CancelDate;
			parameters[8].Value = model.Handling;
			parameters[9].Value = model.HandDate;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_ParkingCancel_Update",parameters,out rowsAffected);
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

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_ParkingCancel_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_ParkingCancel GetModel(long IID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@IID", SqlDbType.BigInt)};
			parameters[0].Value = IID;

			MobileSoft.Model.HSPR.Tb_HSPR_ParkingCancel model=new MobileSoft.Model.HSPR.Tb_HSPR_ParkingCancel();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_HSPR_ParkingCancel_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["IID"].ToString()!="")
				{
					model.IID=long.Parse(ds.Tables[0].Rows[0]["IID"].ToString());
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
				if(ds.Tables[0].Rows[0]["RoomID"].ToString()!="")
				{
					model.RoomID=long.Parse(ds.Tables[0].Rows[0]["RoomID"].ToString());
				}
				model.CancelCause=ds.Tables[0].Rows[0]["CancelCause"].ToString();
				if(ds.Tables[0].Rows[0]["CancelDate"].ToString()!="")
				{
					model.CancelDate=DateTime.Parse(ds.Tables[0].Rows[0]["CancelDate"].ToString());
				}
				model.Handling=ds.Tables[0].Rows[0]["Handling"].ToString();
				if(ds.Tables[0].Rows[0]["HandDate"].ToString()!="")
				{
					model.HandDate=DateTime.Parse(ds.Tables[0].Rows[0]["HandDate"].ToString());
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
			strSql.Append("select IID,HandID,CommID,ParkID,CustID,RoomID,CancelCause,CancelDate,Handling,HandDate ");
			strSql.Append(" FROM Tb_HSPR_ParkingCancel ");
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
			strSql.Append(" IID,HandID,CommID,ParkID,CustID,RoomID,CancelCause,CancelDate,Handling,HandDate ");
			strSql.Append(" FROM Tb_HSPR_ParkingCancel ");
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
			parameters[5].Value = "SELECT * FROM Tb_HSPR_ParkingCancel WHERE 1=1 " + StrCondition;
			parameters[6].Value = "IID";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

