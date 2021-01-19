using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.Sys
{
	/// <summary>
	/// 数据访问类Dal_Tb_Sys_TakePicWork。
	/// </summary>
	public class Dal_Tb_Sys_TakePicWork
	{
		public Dal_Tb_Sys_TakePicWork()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long StatID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@StatID", SqlDbType.BigInt)};
			parameters[0].Value = StatID;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_Sys_TakePicWork_Exists",parameters,out rowsAffected);
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
		public int Add(MobileSoft.Model.Sys.Tb_Sys_TakePicWork model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@StatID", SqlDbType.BigInt,8),
					new SqlParameter("@UserCode", SqlDbType.NVarChar,20),
					new SqlParameter("@InsBatchFeesCount", SqlDbType.Int,4),
					new SqlParameter("@InsOneFeesCount", SqlDbType.Int,4),
					new SqlParameter("@OffsetPrecCount", SqlDbType.Int,4),
					new SqlParameter("@WaivCount", SqlDbType.Int,4),
					new SqlParameter("@ReceCount", SqlDbType.Int,4),
					new SqlParameter("@RefundCount", SqlDbType.Int,4),
					new SqlParameter("@PrecRefundCount", SqlDbType.Int,4),
					new SqlParameter("@LeaseContCount", SqlDbType.Int,4),
					new SqlParameter("@ContCount", SqlDbType.Int,4),
					new SqlParameter("@RoomStateCount", SqlDbType.Int,4),
					new SqlParameter("@AskForCount", SqlDbType.Int,4),
					new SqlParameter("@PurchaseCount", SqlDbType.Int,4),
					new SqlParameter("@InCount", SqlDbType.Int,4),
					new SqlParameter("@OutCount", SqlDbType.Int,4),
					new SqlParameter("@InventCount", SqlDbType.Int,4),
					new SqlParameter("@MaterialTracCount", SqlDbType.Int,4)};
			parameters[0].Direction = ParameterDirection.Output;
			parameters[1].Value = model.UserCode;
			parameters[2].Value = model.InsBatchFeesCount;
			parameters[3].Value = model.InsOneFeesCount;
			parameters[4].Value = model.OffsetPrecCount;
			parameters[5].Value = model.WaivCount;
			parameters[6].Value = model.ReceCount;
			parameters[7].Value = model.RefundCount;
			parameters[8].Value = model.PrecRefundCount;
			parameters[9].Value = model.LeaseContCount;
			parameters[10].Value = model.ContCount;
			parameters[11].Value = model.RoomStateCount;
			parameters[12].Value = model.AskForCount;
			parameters[13].Value = model.PurchaseCount;
			parameters[14].Value = model.InCount;
			parameters[15].Value = model.OutCount;
			parameters[16].Value = model.InventCount;
			parameters[17].Value = model.MaterialTracCount;

			DbHelperSQL.RunProcedure("Proc_Tb_Sys_TakePicWork_ADD",parameters,out rowsAffected);
			return (int)parameters[0].Value;
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.Sys.Tb_Sys_TakePicWork model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@StatID", SqlDbType.BigInt,8),
					new SqlParameter("@UserCode", SqlDbType.NVarChar,20),
					new SqlParameter("@InsBatchFeesCount", SqlDbType.Int,4),
					new SqlParameter("@InsOneFeesCount", SqlDbType.Int,4),
					new SqlParameter("@OffsetPrecCount", SqlDbType.Int,4),
					new SqlParameter("@WaivCount", SqlDbType.Int,4),
					new SqlParameter("@ReceCount", SqlDbType.Int,4),
					new SqlParameter("@RefundCount", SqlDbType.Int,4),
					new SqlParameter("@PrecRefundCount", SqlDbType.Int,4),
					new SqlParameter("@LeaseContCount", SqlDbType.Int,4),
					new SqlParameter("@ContCount", SqlDbType.Int,4),
					new SqlParameter("@RoomStateCount", SqlDbType.Int,4),
					new SqlParameter("@AskForCount", SqlDbType.Int,4),
					new SqlParameter("@PurchaseCount", SqlDbType.Int,4),
					new SqlParameter("@InCount", SqlDbType.Int,4),
					new SqlParameter("@OutCount", SqlDbType.Int,4),
					new SqlParameter("@InventCount", SqlDbType.Int,4),
					new SqlParameter("@MaterialTracCount", SqlDbType.Int,4)};
			parameters[0].Value = model.StatID;
			parameters[1].Value = model.UserCode;
			parameters[2].Value = model.InsBatchFeesCount;
			parameters[3].Value = model.InsOneFeesCount;
			parameters[4].Value = model.OffsetPrecCount;
			parameters[5].Value = model.WaivCount;
			parameters[6].Value = model.ReceCount;
			parameters[7].Value = model.RefundCount;
			parameters[8].Value = model.PrecRefundCount;
			parameters[9].Value = model.LeaseContCount;
			parameters[10].Value = model.ContCount;
			parameters[11].Value = model.RoomStateCount;
			parameters[12].Value = model.AskForCount;
			parameters[13].Value = model.PurchaseCount;
			parameters[14].Value = model.InCount;
			parameters[15].Value = model.OutCount;
			parameters[16].Value = model.InventCount;
			parameters[17].Value = model.MaterialTracCount;

			DbHelperSQL.RunProcedure("Proc_Tb_Sys_TakePicWork_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long StatID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@StatID", SqlDbType.BigInt)};
			parameters[0].Value = StatID;

			DbHelperSQL.RunProcedure("Proc_Tb_Sys_TakePicWork_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.Sys.Tb_Sys_TakePicWork GetModel(long StatID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@StatID", SqlDbType.BigInt)};
			parameters[0].Value = StatID;

			MobileSoft.Model.Sys.Tb_Sys_TakePicWork model=new MobileSoft.Model.Sys.Tb_Sys_TakePicWork();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_Sys_TakePicWork_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["StatID"].ToString()!="")
				{
					model.StatID=long.Parse(ds.Tables[0].Rows[0]["StatID"].ToString());
				}
				model.UserCode=ds.Tables[0].Rows[0]["UserCode"].ToString();
				if(ds.Tables[0].Rows[0]["InsBatchFeesCount"].ToString()!="")
				{
					model.InsBatchFeesCount=int.Parse(ds.Tables[0].Rows[0]["InsBatchFeesCount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["InsOneFeesCount"].ToString()!="")
				{
					model.InsOneFeesCount=int.Parse(ds.Tables[0].Rows[0]["InsOneFeesCount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["OffsetPrecCount"].ToString()!="")
				{
					model.OffsetPrecCount=int.Parse(ds.Tables[0].Rows[0]["OffsetPrecCount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["WaivCount"].ToString()!="")
				{
					model.WaivCount=int.Parse(ds.Tables[0].Rows[0]["WaivCount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ReceCount"].ToString()!="")
				{
					model.ReceCount=int.Parse(ds.Tables[0].Rows[0]["ReceCount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["RefundCount"].ToString()!="")
				{
					model.RefundCount=int.Parse(ds.Tables[0].Rows[0]["RefundCount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["PrecRefundCount"].ToString()!="")
				{
					model.PrecRefundCount=int.Parse(ds.Tables[0].Rows[0]["PrecRefundCount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["LeaseContCount"].ToString()!="")
				{
					model.LeaseContCount=int.Parse(ds.Tables[0].Rows[0]["LeaseContCount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ContCount"].ToString()!="")
				{
					model.ContCount=int.Parse(ds.Tables[0].Rows[0]["ContCount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["RoomStateCount"].ToString()!="")
				{
					model.RoomStateCount=int.Parse(ds.Tables[0].Rows[0]["RoomStateCount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["AskForCount"].ToString()!="")
				{
					model.AskForCount=int.Parse(ds.Tables[0].Rows[0]["AskForCount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["PurchaseCount"].ToString()!="")
				{
					model.PurchaseCount=int.Parse(ds.Tables[0].Rows[0]["PurchaseCount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["InCount"].ToString()!="")
				{
					model.InCount=int.Parse(ds.Tables[0].Rows[0]["InCount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["OutCount"].ToString()!="")
				{
					model.OutCount=int.Parse(ds.Tables[0].Rows[0]["OutCount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["InventCount"].ToString()!="")
				{
					model.InventCount=int.Parse(ds.Tables[0].Rows[0]["InventCount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["MaterialTracCount"].ToString()!="")
				{
					model.MaterialTracCount=int.Parse(ds.Tables[0].Rows[0]["MaterialTracCount"].ToString());
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
			strSql.Append("select StatID,UserCode,InsBatchFeesCount,InsOneFeesCount,OffsetPrecCount,WaivCount,ReceCount,RefundCount,PrecRefundCount,LeaseContCount,ContCount,RoomStateCount,AskForCount,PurchaseCount,InCount,OutCount,InventCount,MaterialTracCount ");
			strSql.Append(" FROM Tb_Sys_TakePicWork ");
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
			strSql.Append(" StatID,UserCode,InsBatchFeesCount,InsOneFeesCount,OffsetPrecCount,WaivCount,ReceCount,RefundCount,PrecRefundCount,LeaseContCount,ContCount,RoomStateCount,AskForCount,PurchaseCount,InCount,OutCount,InventCount,MaterialTracCount ");
			strSql.Append(" FROM Tb_Sys_TakePicWork ");
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
			parameters[5].Value = "SELECT * FROM Tb_Sys_TakePicWork WHERE 1=1 " + StrCondition;
			parameters[6].Value = "StatID";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

