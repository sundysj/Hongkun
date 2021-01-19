using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.Charge
{
	/// <summary>
	/// 数据访问类Dal_Tb_Charge_Receipt。
	/// </summary>
	public class Dal_Tb_Charge_Receipt
	{
		public Dal_Tb_Charge_Receipt()
		{
            DbHelperSQL.ConnectionString = PubConstant.tw2bsConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string ReceiptCode)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ReceiptCode", SqlDbType.VarChar,50)};
			parameters[0].Value = ReceiptCode;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_Charge_Receipt_Exists",parameters,out rowsAffected);
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
		public void Add(MobileSoft.Model.Charge.Tb_Charge_Receipt model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ReceiptCode", SqlDbType.VarChar,36),
					new SqlParameter("@BussId", SqlDbType.BigInt,8),
					new SqlParameter("@OrderId", SqlDbType.VarChar,36),
					new SqlParameter("@ReceiptSign", SqlDbType.VarChar,36),
					new SqlParameter("@Receivables", SqlDbType.Decimal,9),
					new SqlParameter("@Method", SqlDbType.VarChar,12),
					new SqlParameter("@CardNum", SqlDbType.VarChar,36),
					new SqlParameter("@MemberCardCode", SqlDbType.VarChar,36),
					new SqlParameter("@CustCode", SqlDbType.VarChar,36),
					new SqlParameter("@Balance", SqlDbType.Decimal,9),
					new SqlParameter("@Discount", SqlDbType.Decimal,9),
					new SqlParameter("@Amount", SqlDbType.Decimal,9),
					new SqlParameter("@ReceiptMemo", SqlDbType.VarChar,1000),
					new SqlParameter("@ReceiptType", SqlDbType.VarChar,30),
					new SqlParameter("@ReceiptDate", SqlDbType.DateTime),
					new SqlParameter("@IsDelete", SqlDbType.Int,4)};
			parameters[0].Value = model.ReceiptCode;
			parameters[1].Value = model.BussId;
			parameters[2].Value = model.OrderId;
			parameters[3].Value = model.ReceiptSign;
			parameters[4].Value = model.Receivables;
			parameters[5].Value = model.Method;
			parameters[6].Value = model.CardNum;
			parameters[7].Value = model.MemberCardCode;
			parameters[8].Value = model.CustCode;
			parameters[9].Value = model.Balance;
			parameters[10].Value = model.Discount;
			parameters[11].Value = model.Amount;
			parameters[12].Value = model.ReceiptMemo;
			parameters[13].Value = model.ReceiptType;
			parameters[14].Value = model.ReceiptDate;
			parameters[15].Value = model.IsDelete;

			DbHelperSQL.RunProcedure("Proc_Tb_Charge_Receipt_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.Charge.Tb_Charge_Receipt model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ReceiptCode", SqlDbType.VarChar,36),
					new SqlParameter("@BussId", SqlDbType.BigInt,8),
					new SqlParameter("@OrderId", SqlDbType.VarChar,36),
					new SqlParameter("@ReceiptSign", SqlDbType.VarChar,36),
					new SqlParameter("@Receivables", SqlDbType.Decimal,9),
					new SqlParameter("@Method", SqlDbType.VarChar,12),
					new SqlParameter("@CardNum", SqlDbType.VarChar,36),
					new SqlParameter("@MemberCardCode", SqlDbType.VarChar,36),
					new SqlParameter("@CustCode", SqlDbType.VarChar,36),
					new SqlParameter("@Balance", SqlDbType.Decimal,9),
					new SqlParameter("@Discount", SqlDbType.Decimal,9),
					new SqlParameter("@Amount", SqlDbType.Decimal,9),
					new SqlParameter("@ReceiptMemo", SqlDbType.VarChar,1000),
					new SqlParameter("@ReceiptType", SqlDbType.VarChar,30),
					new SqlParameter("@ReceiptDate", SqlDbType.DateTime),
					new SqlParameter("@IsDelete", SqlDbType.Int,4)};
			parameters[0].Value = model.ReceiptCode;
			parameters[1].Value = model.BussId;
			parameters[2].Value = model.OrderId;
			parameters[3].Value = model.ReceiptSign;
			parameters[4].Value = model.Receivables;
			parameters[5].Value = model.Method;
			parameters[6].Value = model.CardNum;
			parameters[7].Value = model.MemberCardCode;
			parameters[8].Value = model.CustCode;
			parameters[9].Value = model.Balance;
			parameters[10].Value = model.Discount;
			parameters[11].Value = model.Amount;
			parameters[12].Value = model.ReceiptMemo;
			parameters[13].Value = model.ReceiptType;
			parameters[14].Value = model.ReceiptDate;
			parameters[15].Value = model.IsDelete;

			DbHelperSQL.RunProcedure("Proc_Tb_Charge_Receipt_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(string ReceiptCode)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ReceiptCode", SqlDbType.VarChar,50)};
			parameters[0].Value = ReceiptCode;

			DbHelperSQL.RunProcedure("Proc_Tb_Charge_Receipt_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.Charge.Tb_Charge_Receipt GetModel(string ReceiptCode)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@ReceiptCode", SqlDbType.VarChar,50)};
			parameters[0].Value = ReceiptCode;

			MobileSoft.Model.Charge.Tb_Charge_Receipt model=new MobileSoft.Model.Charge.Tb_Charge_Receipt();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_Charge_Receipt_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				model.ReceiptCode=ds.Tables[0].Rows[0]["ReceiptCode"].ToString();
				if(ds.Tables[0].Rows[0]["BussId"].ToString()!="")
				{
					model.BussId=long.Parse(ds.Tables[0].Rows[0]["BussId"].ToString());
				}
				if(ds.Tables[0].Rows[0]["OrderId"].ToString()!="")
				{
					model.OrderId=ds.Tables[0].Rows[0]["OrderId"].ToString();
				}
				model.ReceiptSign=ds.Tables[0].Rows[0]["ReceiptSign"].ToString();
				if(ds.Tables[0].Rows[0]["Receivables"].ToString()!="")
				{
					model.Receivables=decimal.Parse(ds.Tables[0].Rows[0]["Receivables"].ToString());
				}
				model.Method=ds.Tables[0].Rows[0]["Method"].ToString();
				model.CardNum=ds.Tables[0].Rows[0]["CardNum"].ToString();
				model.MemberCardCode=ds.Tables[0].Rows[0]["MemberCardCode"].ToString();
				model.CustCode=ds.Tables[0].Rows[0]["CustCode"].ToString();
				if(ds.Tables[0].Rows[0]["Balance"].ToString()!="")
				{
					model.Balance=decimal.Parse(ds.Tables[0].Rows[0]["Balance"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Discount"].ToString()!="")
				{
					model.Discount=decimal.Parse(ds.Tables[0].Rows[0]["Discount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Amount"].ToString()!="")
				{
					model.Amount=decimal.Parse(ds.Tables[0].Rows[0]["Amount"].ToString());
				}
				model.ReceiptMemo=ds.Tables[0].Rows[0]["ReceiptMemo"].ToString();
				model.ReceiptType=ds.Tables[0].Rows[0]["ReceiptType"].ToString();
				if(ds.Tables[0].Rows[0]["ReceiptDate"].ToString()!="")
				{
					model.ReceiptDate=DateTime.Parse(ds.Tables[0].Rows[0]["ReceiptDate"].ToString());
				}
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
			strSql.Append("select * ");
            strSql.Append(" FROM view_Charge_Receipt_Filter ");
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
			strSql.Append(" * ");
            strSql.Append(" FROM view_Charge_Receipt_Filter ");
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
            parameters[5].Value = "SELECT * FROM view_Charge_Receipt_Filter WHERE 1=1 " + StrCondition;
			parameters[6].Value = "ReceiptCode";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

