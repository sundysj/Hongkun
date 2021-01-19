using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.HSPR
{
	/// <summary>
	/// 数据访问类Dal_Tb_HSPR_CostItem。
	/// </summary>
	public class Dal_Tb_HSPR_CostItem
	{
		public Dal_Tb_HSPR_CostItem()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public long GetMaxId()
		{
		return DbHelperSQL.GetMaxID("CostID", "Tb_HSPR_CostItem"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long CostID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@CostID", SqlDbType.BigInt)};
			parameters[0].Value = CostID;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_HSPR_CostItem_Exists",parameters,out rowsAffected);
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
		public void Add(MobileSoft.Model.HSPR.Tb_HSPR_CostItem model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@CostID", SqlDbType.BigInt,8),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@CostSNum", SqlDbType.Int,4),
					new SqlParameter("@CostName", SqlDbType.NVarChar,30),
					new SqlParameter("@CostType", SqlDbType.SmallInt,2),
					new SqlParameter("@CostGeneType", SqlDbType.SmallInt,2),
					new SqlParameter("@CollUnitID", SqlDbType.BigInt,8),
					new SqlParameter("@DueDate", SqlDbType.Int,4),
					new SqlParameter("@AccountsSign", SqlDbType.NVarChar,20),
					new SqlParameter("@AccountsName", SqlDbType.NVarChar,30),
					new SqlParameter("@ChargeCycle", SqlDbType.Int,4),
					new SqlParameter("@RoundingNum", SqlDbType.SmallInt,2),
					new SqlParameter("@IsBank", SqlDbType.SmallInt,2),
					new SqlParameter("@DelinDelay", SqlDbType.Int,4),
					new SqlParameter("@DelinRates", SqlDbType.Decimal,9),
					new SqlParameter("@PreCostSign", SqlDbType.NVarChar,20),
					new SqlParameter("@Memo", SqlDbType.NVarChar,100),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2),
					new SqlParameter("@CorpCostID", SqlDbType.BigInt,8),
					new SqlParameter("@CostCode", SqlDbType.NVarChar,40),
					new SqlParameter("@SysCostSign", SqlDbType.NVarChar,20),
					new SqlParameter("@DuePlotDate", SqlDbType.Int,4),
					new SqlParameter("@HighCorpCostID", SqlDbType.BigInt,8),
					new SqlParameter("@CostBigType", SqlDbType.SmallInt,2),
					new SqlParameter("@DelinType", SqlDbType.SmallInt,2),
					new SqlParameter("@DelinDay", SqlDbType.Int,4)};
			parameters[0].Value = model.CostID;
			parameters[1].Value = model.CommID;
			parameters[2].Value = model.CostSNum;
			parameters[3].Value = model.CostName;
			parameters[4].Value = model.CostType;
			parameters[5].Value = model.CostGeneType;
			parameters[6].Value = model.CollUnitID;
			parameters[7].Value = model.DueDate;
			parameters[8].Value = model.AccountsSign;
			parameters[9].Value = model.AccountsName;
			parameters[10].Value = model.ChargeCycle;
			parameters[11].Value = model.RoundingNum;
			parameters[12].Value = model.IsBank;
			parameters[13].Value = model.DelinDelay;
			parameters[14].Value = model.DelinRates;
			parameters[15].Value = model.PreCostSign;
			parameters[16].Value = model.Memo;
			parameters[17].Value = model.IsDelete;
			parameters[18].Value = model.CorpCostID;
			parameters[19].Value = model.CostCode;
			parameters[20].Value = model.SysCostSign;
			parameters[21].Value = model.DuePlotDate;
			parameters[22].Value = model.HighCorpCostID;
			parameters[23].Value = model.CostBigType;
			parameters[24].Value = model.DelinType;
			parameters[25].Value = model.DelinDay;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_CostItem_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.HSPR.Tb_HSPR_CostItem model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@CostID", SqlDbType.BigInt,8),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@CostSNum", SqlDbType.Int,4),
					new SqlParameter("@CostName", SqlDbType.NVarChar,30),
					new SqlParameter("@CostType", SqlDbType.SmallInt,2),
					new SqlParameter("@CostGeneType", SqlDbType.SmallInt,2),
					new SqlParameter("@CollUnitID", SqlDbType.BigInt,8),
					new SqlParameter("@DueDate", SqlDbType.Int,4),
					new SqlParameter("@AccountsSign", SqlDbType.NVarChar,20),
					new SqlParameter("@AccountsName", SqlDbType.NVarChar,30),
					new SqlParameter("@ChargeCycle", SqlDbType.Int,4),
					new SqlParameter("@RoundingNum", SqlDbType.SmallInt,2),
					new SqlParameter("@IsBank", SqlDbType.SmallInt,2),
					new SqlParameter("@DelinDelay", SqlDbType.Int,4),
					new SqlParameter("@DelinRates", SqlDbType.Decimal,9),
					new SqlParameter("@PreCostSign", SqlDbType.NVarChar,20),
					new SqlParameter("@Memo", SqlDbType.NVarChar,100),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2),
					new SqlParameter("@CorpCostID", SqlDbType.BigInt,8),
					new SqlParameter("@CostCode", SqlDbType.NVarChar,40),
					new SqlParameter("@SysCostSign", SqlDbType.NVarChar,20),
					new SqlParameter("@DuePlotDate", SqlDbType.Int,4),
					new SqlParameter("@HighCorpCostID", SqlDbType.BigInt,8),
					new SqlParameter("@CostBigType", SqlDbType.SmallInt,2),
					new SqlParameter("@DelinType", SqlDbType.SmallInt,2),
					new SqlParameter("@DelinDay", SqlDbType.Int,4)};
			parameters[0].Value = model.CostID;
			parameters[1].Value = model.CommID;
			parameters[2].Value = model.CostSNum;
			parameters[3].Value = model.CostName;
			parameters[4].Value = model.CostType;
			parameters[5].Value = model.CostGeneType;
			parameters[6].Value = model.CollUnitID;
			parameters[7].Value = model.DueDate;
			parameters[8].Value = model.AccountsSign;
			parameters[9].Value = model.AccountsName;
			parameters[10].Value = model.ChargeCycle;
			parameters[11].Value = model.RoundingNum;
			parameters[12].Value = model.IsBank;
			parameters[13].Value = model.DelinDelay;
			parameters[14].Value = model.DelinRates;
			parameters[15].Value = model.PreCostSign;
			parameters[16].Value = model.Memo;
			parameters[17].Value = model.IsDelete;
			parameters[18].Value = model.CorpCostID;
			parameters[19].Value = model.CostCode;
			parameters[20].Value = model.SysCostSign;
			parameters[21].Value = model.DuePlotDate;
			parameters[22].Value = model.HighCorpCostID;
			parameters[23].Value = model.CostBigType;
			parameters[24].Value = model.DelinType;
			parameters[25].Value = model.DelinDay;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_CostItem_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long CostID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@CostID", SqlDbType.BigInt)};
			parameters[0].Value = CostID;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_CostItem_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_CostItem GetModel(long CostID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@CostID", SqlDbType.BigInt)};
			parameters[0].Value = CostID;

			MobileSoft.Model.HSPR.Tb_HSPR_CostItem model=new MobileSoft.Model.HSPR.Tb_HSPR_CostItem();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_HSPR_CostItem_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["CostID"].ToString()!="")
				{
					model.CostID=long.Parse(ds.Tables[0].Rows[0]["CostID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CommID"].ToString()!="")
				{
					model.CommID=int.Parse(ds.Tables[0].Rows[0]["CommID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CostSNum"].ToString()!="")
				{
					model.CostSNum=int.Parse(ds.Tables[0].Rows[0]["CostSNum"].ToString());
				}
				model.CostName=ds.Tables[0].Rows[0]["CostName"].ToString();
				if(ds.Tables[0].Rows[0]["CostType"].ToString()!="")
				{
					model.CostType=int.Parse(ds.Tables[0].Rows[0]["CostType"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CostGeneType"].ToString()!="")
				{
					model.CostGeneType=int.Parse(ds.Tables[0].Rows[0]["CostGeneType"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CollUnitID"].ToString()!="")
				{
					model.CollUnitID=long.Parse(ds.Tables[0].Rows[0]["CollUnitID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["DueDate"].ToString()!="")
				{
					model.DueDate=int.Parse(ds.Tables[0].Rows[0]["DueDate"].ToString());
				}
				model.AccountsSign=ds.Tables[0].Rows[0]["AccountsSign"].ToString();
				model.AccountsName=ds.Tables[0].Rows[0]["AccountsName"].ToString();
				if(ds.Tables[0].Rows[0]["ChargeCycle"].ToString()!="")
				{
					model.ChargeCycle=int.Parse(ds.Tables[0].Rows[0]["ChargeCycle"].ToString());
				}
				if(ds.Tables[0].Rows[0]["RoundingNum"].ToString()!="")
				{
					model.RoundingNum=int.Parse(ds.Tables[0].Rows[0]["RoundingNum"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsBank"].ToString()!="")
				{
					model.IsBank=int.Parse(ds.Tables[0].Rows[0]["IsBank"].ToString());
				}
				if(ds.Tables[0].Rows[0]["DelinDelay"].ToString()!="")
				{
					model.DelinDelay=int.Parse(ds.Tables[0].Rows[0]["DelinDelay"].ToString());
				}
				if(ds.Tables[0].Rows[0]["DelinRates"].ToString()!="")
				{
					model.DelinRates=decimal.Parse(ds.Tables[0].Rows[0]["DelinRates"].ToString());
				}
				model.PreCostSign=ds.Tables[0].Rows[0]["PreCostSign"].ToString();
				model.Memo=ds.Tables[0].Rows[0]["Memo"].ToString();
				if(ds.Tables[0].Rows[0]["IsDelete"].ToString()!="")
				{
					model.IsDelete=int.Parse(ds.Tables[0].Rows[0]["IsDelete"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CorpCostID"].ToString()!="")
				{
					model.CorpCostID=long.Parse(ds.Tables[0].Rows[0]["CorpCostID"].ToString());
				}
				model.CostCode=ds.Tables[0].Rows[0]["CostCode"].ToString();
				model.SysCostSign=ds.Tables[0].Rows[0]["SysCostSign"].ToString();
				if(ds.Tables[0].Rows[0]["DuePlotDate"].ToString()!="")
				{
					model.DuePlotDate=int.Parse(ds.Tables[0].Rows[0]["DuePlotDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["HighCorpCostID"].ToString()!="")
				{
					model.HighCorpCostID=long.Parse(ds.Tables[0].Rows[0]["HighCorpCostID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CostBigType"].ToString()!="")
				{
					model.CostBigType=int.Parse(ds.Tables[0].Rows[0]["CostBigType"].ToString());
				}
				if(ds.Tables[0].Rows[0]["DelinType"].ToString()!="")
				{
					model.DelinType=int.Parse(ds.Tables[0].Rows[0]["DelinType"].ToString());
				}
				if(ds.Tables[0].Rows[0]["DelinDay"].ToString()!="")
				{
					model.DelinDay=int.Parse(ds.Tables[0].Rows[0]["DelinDay"].ToString());
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
			strSql.Append("select CostID,CommID,CostSNum,CostName,CostType,CostGeneType,CollUnitID,DueDate,AccountsSign,AccountsName,ChargeCycle,RoundingNum,IsBank,DelinDelay,DelinRates,PreCostSign,Memo,IsDelete,CorpCostID,CostCode,SysCostSign,DuePlotDate,HighCorpCostID,CostBigType,DelinType,DelinDay ");
			strSql.Append(" FROM Tb_HSPR_CostItem ");
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
			strSql.Append(" CostID,CommID,CostSNum,CostName,CostType,CostGeneType,CollUnitID,DueDate,AccountsSign,AccountsName,ChargeCycle,RoundingNum,IsBank,DelinDelay,DelinRates,PreCostSign,Memo,IsDelete,CorpCostID,CostCode,SysCostSign,DuePlotDate,HighCorpCostID,CostBigType,DelinType,DelinDay ");
			strSql.Append(" FROM Tb_HSPR_CostItem ");
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
			parameters[5].Value = "SELECT * FROM Tb_HSPR_CostItem WHERE 1=1 " + StrCondition;
			parameters[6].Value = "CostID";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

