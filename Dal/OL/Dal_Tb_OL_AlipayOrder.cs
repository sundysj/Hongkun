using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.OL
{
	/// <summary>
	/// 数据访问类Dal_Tb_OL_AlipayOrder。
	/// </summary>
	public class Dal_Tb_OL_AlipayOrder
	{
		public Dal_Tb_OL_AlipayOrder()
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

			int result= DbHelperSQL.RunProcedure("Proc_Tb_OL_AlipayOrder_Exists",parameters,out rowsAffected);
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
		public void Add(MobileSoft.Model.OL.Tb_OL_AlipayOrder model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.VarChar,36),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@CommunityId", SqlDbType.VarChar,36),
					new SqlParameter("@CustId", SqlDbType.BigInt,8),
					new SqlParameter("@partner", SqlDbType.VarChar,36),
					new SqlParameter("@out_trade_no", SqlDbType.VarChar,32),
					new SqlParameter("@prepay_str", SqlDbType.VarChar,3999),
					new SqlParameter("@txnTime", SqlDbType.VarChar,36),
					new SqlParameter("@trade_status", SqlDbType.VarChar,36),
					new SqlParameter("@trade_msg", SqlDbType.VarChar,36),
					new SqlParameter("@IsSucc", SqlDbType.Int,4),
					new SqlParameter("@Memo", SqlDbType.VarChar,3999),
					new SqlParameter("@IsDelete", SqlDbType.Int,4)};
			parameters[0].Value = model.Id;
			parameters[1].Value = model.CommID;
			parameters[2].Value = model.CommunityId;
			parameters[3].Value = model.CustId;
			parameters[4].Value = model.partner;
			parameters[5].Value = model.out_trade_no;
			parameters[6].Value = model.prepay_str;
			parameters[7].Value = model.txnTime;
			parameters[8].Value = model.trade_status;
			parameters[9].Value = model.trade_msg;
			parameters[10].Value = model.IsSucc;
			parameters[11].Value = model.Memo;
			parameters[12].Value = model.IsDelete;

			DbHelperSQL.RunProcedure("Proc_Tb_OL_AlipayOrder_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.OL.Tb_OL_AlipayOrder model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.VarChar,36),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@CommunityId", SqlDbType.VarChar,36),
					new SqlParameter("@CustId", SqlDbType.BigInt,8),
					new SqlParameter("@partner", SqlDbType.VarChar,36),
					new SqlParameter("@out_trade_no", SqlDbType.VarChar,32),
					new SqlParameter("@prepay_str", SqlDbType.VarChar,3999),
					new SqlParameter("@txnTime", SqlDbType.VarChar,36),
					new SqlParameter("@trade_status", SqlDbType.VarChar,36),
					new SqlParameter("@trade_msg", SqlDbType.VarChar,36),
					new SqlParameter("@IsSucc", SqlDbType.Int,4),
					new SqlParameter("@Memo", SqlDbType.VarChar,3999),
					new SqlParameter("@IsDelete", SqlDbType.Int,4)};
			parameters[0].Value = model.Id;
			parameters[1].Value = model.CommID;
			parameters[2].Value = model.CommunityId;
			parameters[3].Value = model.CustId;
			parameters[4].Value = model.partner;
			parameters[5].Value = model.out_trade_no;
			parameters[6].Value = model.prepay_str;
			parameters[7].Value = model.txnTime;
			parameters[8].Value = model.trade_status;
			parameters[9].Value = model.trade_msg;
			parameters[10].Value = model.IsSucc;
			parameters[11].Value = model.Memo;
			parameters[12].Value = model.IsDelete;

			DbHelperSQL.RunProcedure("Proc_Tb_OL_AlipayOrder_Update",parameters,out rowsAffected);
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

			DbHelperSQL.RunProcedure("Proc_Tb_OL_AlipayOrder_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.OL.Tb_OL_AlipayOrder GetModel(string Id)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.VarChar,50)};
			parameters[0].Value = Id;

			MobileSoft.Model.OL.Tb_OL_AlipayOrder model=new MobileSoft.Model.OL.Tb_OL_AlipayOrder();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_OL_AlipayOrder_GetModel",parameters,"ds");
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
				model.partner=ds.Tables[0].Rows[0]["partner"].ToString();
				model.out_trade_no=ds.Tables[0].Rows[0]["out_trade_no"].ToString();
				model.prepay_str=ds.Tables[0].Rows[0]["prepay_str"].ToString();
				model.txnTime=ds.Tables[0].Rows[0]["txnTime"].ToString();
				model.trade_status=ds.Tables[0].Rows[0]["trade_status"].ToString();
				model.trade_msg=ds.Tables[0].Rows[0]["trade_msg"].ToString();
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
			strSql.Append("select Id,CommID,CommunityId,CustId,partner,out_trade_no,prepay_str,txnTime,trade_status,trade_msg,IsSucc,Memo,IsDelete ");
			strSql.Append(" FROM Tb_OL_AlipayOrder ");
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
			strSql.Append(" Id,CommID,CommunityId,CustId,partner,out_trade_no,prepay_str,txnTime,trade_status,trade_msg,IsSucc,Memo,IsDelete ");
			strSql.Append(" FROM Tb_OL_AlipayOrder ");
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
			parameters[5].Value = "SELECT * FROM Tb_OL_AlipayOrder WHERE 1=1 " + StrCondition;
			parameters[6].Value = "Id";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

