using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.OL
{
	/// <summary>
	/// 数据访问类Dal_Tb_OL_UnionPayOrder。
	/// </summary>
	public class Dal_Tb_OL_UnionPayOrder
	{
		public Dal_Tb_OL_UnionPayOrder()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string Id)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.VarChar,50)};
			parameters[0].Value = Id;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_OL_UnionPayOrder_Exists",parameters,out rowsAffected);
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
		public void Add(MobileSoft.Model.OL.Tb_OL_UnionPayOrder model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.VarChar,36),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@CommunityId", SqlDbType.VarChar,36),
					new SqlParameter("@CustId", SqlDbType.BigInt,8),
					new SqlParameter("@merId", SqlDbType.BigInt,8),
					new SqlParameter("@orderId", SqlDbType.VarChar,32),
					new SqlParameter("@txnTime", SqlDbType.VarChar,36),
					new SqlParameter("@Tn", SqlDbType.VarChar,36),
					new SqlParameter("@OrderDate", SqlDbType.DateTime),
					new SqlParameter("@respCode", SqlDbType.VarChar,36),
					new SqlParameter("@respMsg", SqlDbType.VarChar,36),
					new SqlParameter("@respDate", SqlDbType.DateTime),
					new SqlParameter("@IsSucc", SqlDbType.Int,4),
					new SqlParameter("@Memo", SqlDbType.VarChar,36),
					new SqlParameter("@IsDelete", SqlDbType.Int,4)};
			parameters[0].Value = model.Id;
			parameters[1].Value = model.CommID;
			parameters[2].Value = model.CommunityId;
			parameters[3].Value = model.CustId;
			parameters[4].Value = model.merId;
			parameters[5].Value = model.orderId;
			parameters[6].Value = model.txnTime;
			parameters[7].Value = model.Tn;
			parameters[8].Value = model.OrderDate;
			parameters[9].Value = model.respCode;
			parameters[10].Value = model.respMsg;
			parameters[11].Value = model.respDate;
			parameters[12].Value = model.IsSucc;
			parameters[13].Value = model.Memo;
			parameters[14].Value = model.IsDelete;

			DbHelperSQL.RunProcedure("Proc_Tb_OL_UnionPayOrder_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.OL.Tb_OL_UnionPayOrder model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.VarChar,36),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@CommunityId", SqlDbType.VarChar,36),
					new SqlParameter("@CustId", SqlDbType.BigInt,8),
					new SqlParameter("@merId", SqlDbType.BigInt,8),
					new SqlParameter("@orderId", SqlDbType.VarChar,32),
					new SqlParameter("@txnTime", SqlDbType.VarChar,36),
					new SqlParameter("@Tn", SqlDbType.VarChar,36),
					new SqlParameter("@OrderDate", SqlDbType.DateTime),
					new SqlParameter("@respCode", SqlDbType.VarChar,36),
					new SqlParameter("@respMsg", SqlDbType.VarChar,36),
					new SqlParameter("@respDate", SqlDbType.DateTime),
					new SqlParameter("@IsSucc", SqlDbType.Int,4),
					new SqlParameter("@Memo", SqlDbType.VarChar,36),
					new SqlParameter("@IsDelete", SqlDbType.Int,4)};
			parameters[0].Value = model.Id;
			parameters[1].Value = model.CommID;
			parameters[2].Value = model.CommunityId;
			parameters[3].Value = model.CustId;
			parameters[4].Value = model.merId;
			parameters[5].Value = model.orderId;
			parameters[6].Value = model.txnTime;
			parameters[7].Value = model.Tn;
			parameters[8].Value = model.OrderDate;
			parameters[9].Value = model.respCode;
			parameters[10].Value = model.respMsg;
			parameters[11].Value = model.respDate;
			parameters[12].Value = model.IsSucc;
			parameters[13].Value = model.Memo;
			parameters[14].Value = model.IsDelete;

			DbHelperSQL.RunProcedure("Proc_Tb_OL_UnionPayOrder_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(string Id)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.VarChar,50)};
			parameters[0].Value = Id;

			DbHelperSQL.RunProcedure("Proc_Tb_OL_UnionPayOrder_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.OL.Tb_OL_UnionPayOrder GetModel(string Id)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.VarChar,50)};
			parameters[0].Value = Id;

			MobileSoft.Model.OL.Tb_OL_UnionPayOrder model=new MobileSoft.Model.OL.Tb_OL_UnionPayOrder();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_OL_UnionPayOrder_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				model.Id=ds.Tables[0].Rows[0]["Id"].ToString();
				if(ds.Tables[0].Rows[0]["CommID"].ToString()!="")
				{
					model.CommID=int.Parse(ds.Tables[0].Rows[0]["CommID"].ToString());
				}
				model.CommunityId=ds.Tables[0].Rows[0]["CommunityId"].ToString();
				if(ds.Tables[0].Rows[0]["CustId"].ToString()!="")
				{
					model.CustId=long.Parse(ds.Tables[0].Rows[0]["CustId"].ToString());
				}
				if(ds.Tables[0].Rows[0]["merId"].ToString()!="")
				{
					model.merId=long.Parse(ds.Tables[0].Rows[0]["merId"].ToString());
				}
				model.orderId=ds.Tables[0].Rows[0]["orderId"].ToString();
				model.txnTime=ds.Tables[0].Rows[0]["txnTime"].ToString();
				model.Tn=ds.Tables[0].Rows[0]["Tn"].ToString();
				if(ds.Tables[0].Rows[0]["OrderDate"].ToString()!="")
				{
					model.OrderDate=DateTime.Parse(ds.Tables[0].Rows[0]["OrderDate"].ToString());
				}
				model.respCode=ds.Tables[0].Rows[0]["respCode"].ToString();
				model.respMsg=ds.Tables[0].Rows[0]["respMsg"].ToString();
				if(ds.Tables[0].Rows[0]["respDate"].ToString()!="")
				{
					model.respDate=DateTime.Parse(ds.Tables[0].Rows[0]["respDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsSucc"].ToString()!="")
				{
					model.IsSucc=int.Parse(ds.Tables[0].Rows[0]["IsSucc"].ToString());
				}
				model.Memo=ds.Tables[0].Rows[0]["Memo"].ToString();
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
			strSql.Append("select Id,CommID,CommunityId,CustId,merId,orderId,txnTime,Tn,OrderDate,respCode,respMsg,respDate,IsSucc,Memo,IsDelete ");
			strSql.Append(" FROM Tb_OL_UnionPayOrder ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" Id,CommID,CommunityId,CustId,merId,orderId,txnTime,Tn,OrderDate,respCode,respMsg,respDate,IsSucc,Memo,IsDelete ");
			strSql.Append(" FROM Tb_OL_UnionPayOrder ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
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
			parameters[5].Value = "SELECT * FROM Tb_OL_UnionPayOrder WHERE 1=1 " + StrCondition;
			parameters[6].Value = "Id";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

