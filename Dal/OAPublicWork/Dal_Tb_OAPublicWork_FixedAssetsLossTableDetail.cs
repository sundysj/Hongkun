using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.OAPublicWork
{
	/// <summary>
	/// 数据访问类Dal_Tb_OAPublicWork_FixedAssetsLossTableDetail。
	/// </summary>
	public class Dal_Tb_OAPublicWork_FixedAssetsLossTableDetail
	{
		public Dal_Tb_OAPublicWork_FixedAssetsLossTableDetail()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("InfoID", "Tb_OAPublicWork_FixedAssetsLossTableDetail"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int InfoID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoID", SqlDbType.Int,4)};
			parameters[0].Value = InfoID;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_FixedAssetsLossTableDetail_Exists",parameters,out rowsAffected);
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
		public int Add(MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_FixedAssetsLossTableDetail model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoID", SqlDbType.Int,4),
					new SqlParameter("@FixedAssetsLossTableID", SqlDbType.Int,4),
					new SqlParameter("@PName", SqlDbType.NVarChar,50),
					new SqlParameter("@Model", SqlDbType.NVarChar,50),
					new SqlParameter("@Unit", SqlDbType.NVarChar,50),
					new SqlParameter("@Price", SqlDbType.Decimal,9),
					new SqlParameter("@Quantity", SqlDbType.Int,4),
					new SqlParameter("@OriginalValue", SqlDbType.Decimal,9),
					new SqlParameter("@NetWorth", SqlDbType.Decimal,9)};
			parameters[0].Direction = ParameterDirection.Output;
			parameters[1].Value = model.FixedAssetsLossTableID;
			parameters[2].Value = model.PName;
			parameters[3].Value = model.Model;
			parameters[4].Value = model.Unit;
			parameters[5].Value = model.Price;
			parameters[6].Value = model.Quantity;
			parameters[7].Value = model.OriginalValue;
			parameters[8].Value = model.NetWorth;

			DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_FixedAssetsLossTableDetail_ADD",parameters,out rowsAffected);
			return (int)parameters[0].Value;
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_FixedAssetsLossTableDetail model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoID", SqlDbType.Int,4),
					new SqlParameter("@FixedAssetsLossTableID", SqlDbType.Int,4),
					new SqlParameter("@PName", SqlDbType.NVarChar,50),
					new SqlParameter("@Model", SqlDbType.NVarChar,50),
					new SqlParameter("@Unit", SqlDbType.NVarChar,50),
					new SqlParameter("@Price", SqlDbType.Decimal,9),
					new SqlParameter("@Quantity", SqlDbType.Int,4),
					new SqlParameter("@OriginalValue", SqlDbType.Decimal,9),
					new SqlParameter("@NetWorth", SqlDbType.Decimal,9)};
			parameters[0].Value = model.InfoID;
			parameters[1].Value = model.FixedAssetsLossTableID;
			parameters[2].Value = model.PName;
			parameters[3].Value = model.Model;
			parameters[4].Value = model.Unit;
			parameters[5].Value = model.Price;
			parameters[6].Value = model.Quantity;
			parameters[7].Value = model.OriginalValue;
			parameters[8].Value = model.NetWorth;

			DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_FixedAssetsLossTableDetail_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int InfoID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoID", SqlDbType.Int,4)};
			parameters[0].Value = InfoID;

			DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_FixedAssetsLossTableDetail_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_FixedAssetsLossTableDetail GetModel(int InfoID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@InfoID", SqlDbType.Int,4)};
			parameters[0].Value = InfoID;

			MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_FixedAssetsLossTableDetail model=new MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_FixedAssetsLossTableDetail();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_FixedAssetsLossTableDetail_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["InfoID"].ToString()!="")
				{
					model.InfoID=int.Parse(ds.Tables[0].Rows[0]["InfoID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["FixedAssetsLossTableID"].ToString()!="")
				{
					model.FixedAssetsLossTableID=int.Parse(ds.Tables[0].Rows[0]["FixedAssetsLossTableID"].ToString());
				}
				model.PName=ds.Tables[0].Rows[0]["PName"].ToString();
				model.Model=ds.Tables[0].Rows[0]["Model"].ToString();
				model.Unit=ds.Tables[0].Rows[0]["Unit"].ToString();
				if(ds.Tables[0].Rows[0]["Price"].ToString()!="")
				{
					model.Price=decimal.Parse(ds.Tables[0].Rows[0]["Price"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Quantity"].ToString()!="")
				{
					model.Quantity=int.Parse(ds.Tables[0].Rows[0]["Quantity"].ToString());
				}
				if(ds.Tables[0].Rows[0]["OriginalValue"].ToString()!="")
				{
					model.OriginalValue=decimal.Parse(ds.Tables[0].Rows[0]["OriginalValue"].ToString());
				}
				if(ds.Tables[0].Rows[0]["NetWorth"].ToString()!="")
				{
					model.NetWorth=decimal.Parse(ds.Tables[0].Rows[0]["NetWorth"].ToString());
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
			strSql.Append("select InfoID,FixedAssetsLossTableID,PName,Model,Unit,Price,Quantity,OriginalValue,NetWorth ");
			strSql.Append(" FROM Tb_OAPublicWork_FixedAssetsLossTableDetail ");
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
			strSql.Append(" InfoID,FixedAssetsLossTableID,PName,Model,Unit,Price,Quantity,OriginalValue,NetWorth ");
			strSql.Append(" FROM Tb_OAPublicWork_FixedAssetsLossTableDetail ");
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
			parameters[5].Value = "SELECT * FROM Tb_OAPublicWork_FixedAssetsLossTableDetail WHERE 1=1 " + StrCondition;
			parameters[6].Value = "InfoID";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

