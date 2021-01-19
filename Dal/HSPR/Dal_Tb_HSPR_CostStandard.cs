using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.HSPR
{
	/// <summary>
	/// 数据访问类Dal_Tb_HSPR_CostStandard。
	/// </summary>
	public class Dal_Tb_HSPR_CostStandard
	{
		public Dal_Tb_HSPR_CostStandard()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public long GetMaxId()
		{
		return DbHelperSQL.GetMaxID("StanID", "Tb_HSPR_CostStandard"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long StanID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@StanID", SqlDbType.BigInt)};
			parameters[0].Value = StanID;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_HSPR_CostStandard_Exists",parameters,out rowsAffected);
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
		public void Add(MobileSoft.Model.HSPR.Tb_HSPR_CostStandard model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@StanID", SqlDbType.BigInt,8),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@CostID", SqlDbType.BigInt,8),
					new SqlParameter("@StanSign", SqlDbType.NVarChar,20),
					new SqlParameter("@StanName", SqlDbType.NVarChar,30),
					new SqlParameter("@StanExplain", SqlDbType.NVarChar,50),
					new SqlParameter("@StanFormula", SqlDbType.NVarChar,20),
					new SqlParameter("@StanAmount", SqlDbType.Decimal,9),
					new SqlParameter("@StanStartDate", SqlDbType.DateTime),
					new SqlParameter("@StanEndDate", SqlDbType.DateTime),
					new SqlParameter("@IsCondition", SqlDbType.SmallInt,2),
					new SqlParameter("@ConditionField", SqlDbType.NVarChar,20),
					new SqlParameter("@DelinRates", SqlDbType.Decimal,9),
					new SqlParameter("@DelinDelay", SqlDbType.Int,4),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2),
					new SqlParameter("@IsStanRange", SqlDbType.SmallInt,2),
					new SqlParameter("@ChargeCycle", SqlDbType.Int,4),
					new SqlParameter("@ManageFeesStyle", SqlDbType.SmallInt,2),
					new SqlParameter("@ManageFeesAmount", SqlDbType.Decimal,9),
					new SqlParameter("@CorpStanID", SqlDbType.BigInt,8),
					new SqlParameter("@CorpCostID", SqlDbType.BigInt,8),
					new SqlParameter("@AmountRounded", SqlDbType.Decimal,9),
					new SqlParameter("@Modulus", SqlDbType.Decimal,9),
					new SqlParameter("@DelinType", SqlDbType.SmallInt,2),
					new SqlParameter("@DelinDay", SqlDbType.Int,4)};
			parameters[0].Value = model.StanID;
			parameters[1].Value = model.CommID;
			parameters[2].Value = model.CostID;
			parameters[3].Value = model.StanSign;
			parameters[4].Value = model.StanName;
			parameters[5].Value = model.StanExplain;
			parameters[6].Value = model.StanFormula;
			parameters[7].Value = model.StanAmount;
			parameters[8].Value = model.StanStartDate;
			parameters[9].Value = model.StanEndDate;
			parameters[10].Value = model.IsCondition;
			parameters[11].Value = model.ConditionField;
			parameters[12].Value = model.DelinRates;
			parameters[13].Value = model.DelinDelay;
			parameters[14].Value = model.IsDelete;
			parameters[15].Value = model.IsStanRange;
			parameters[16].Value = model.ChargeCycle;
			parameters[17].Value = model.ManageFeesStyle;
			parameters[18].Value = model.ManageFeesAmount;
			parameters[19].Value = model.CorpStanID;
			parameters[20].Value = model.CorpCostID;
			parameters[21].Value = model.AmountRounded;
			parameters[22].Value = model.Modulus;
			parameters[23].Value = model.DelinType;
			parameters[24].Value = model.DelinDay;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_CostStandard_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.HSPR.Tb_HSPR_CostStandard model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@StanID", SqlDbType.BigInt,8),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@CostID", SqlDbType.BigInt,8),
					new SqlParameter("@StanSign", SqlDbType.NVarChar,20),
					new SqlParameter("@StanName", SqlDbType.NVarChar,30),
					new SqlParameter("@StanExplain", SqlDbType.NVarChar,50),
					new SqlParameter("@StanFormula", SqlDbType.NVarChar,20),
					new SqlParameter("@StanAmount", SqlDbType.Decimal,9),
					new SqlParameter("@StanStartDate", SqlDbType.DateTime),
					new SqlParameter("@StanEndDate", SqlDbType.DateTime),
					new SqlParameter("@IsCondition", SqlDbType.SmallInt,2),
					new SqlParameter("@ConditionField", SqlDbType.NVarChar,20),
					new SqlParameter("@DelinRates", SqlDbType.Decimal,9),
					new SqlParameter("@DelinDelay", SqlDbType.Int,4),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2),
					new SqlParameter("@IsStanRange", SqlDbType.SmallInt,2),
					new SqlParameter("@ChargeCycle", SqlDbType.Int,4),
					new SqlParameter("@ManageFeesStyle", SqlDbType.SmallInt,2),
					new SqlParameter("@ManageFeesAmount", SqlDbType.Decimal,9),
					new SqlParameter("@CorpStanID", SqlDbType.BigInt,8),
					new SqlParameter("@CorpCostID", SqlDbType.BigInt,8),
					new SqlParameter("@AmountRounded", SqlDbType.Decimal,9),
					new SqlParameter("@Modulus", SqlDbType.Decimal,9),
					new SqlParameter("@DelinType", SqlDbType.SmallInt,2),
					new SqlParameter("@DelinDay", SqlDbType.Int,4)};
			parameters[0].Value = model.StanID;
			parameters[1].Value = model.CommID;
			parameters[2].Value = model.CostID;
			parameters[3].Value = model.StanSign;
			parameters[4].Value = model.StanName;
			parameters[5].Value = model.StanExplain;
			parameters[6].Value = model.StanFormula;
			parameters[7].Value = model.StanAmount;
			parameters[8].Value = model.StanStartDate;
			parameters[9].Value = model.StanEndDate;
			parameters[10].Value = model.IsCondition;
			parameters[11].Value = model.ConditionField;
			parameters[12].Value = model.DelinRates;
			parameters[13].Value = model.DelinDelay;
			parameters[14].Value = model.IsDelete;
			parameters[15].Value = model.IsStanRange;
			parameters[16].Value = model.ChargeCycle;
			parameters[17].Value = model.ManageFeesStyle;
			parameters[18].Value = model.ManageFeesAmount;
			parameters[19].Value = model.CorpStanID;
			parameters[20].Value = model.CorpCostID;
			parameters[21].Value = model.AmountRounded;
			parameters[22].Value = model.Modulus;
			parameters[23].Value = model.DelinType;
			parameters[24].Value = model.DelinDay;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_CostStandard_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long StanID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@StanID", SqlDbType.BigInt)};
			parameters[0].Value = StanID;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_CostStandard_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_CostStandard GetModel(long StanID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@StanID", SqlDbType.BigInt)};
			parameters[0].Value = StanID;

			MobileSoft.Model.HSPR.Tb_HSPR_CostStandard model=new MobileSoft.Model.HSPR.Tb_HSPR_CostStandard();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_HSPR_CostStandard_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["StanID"].ToString()!="")
				{
					model.StanID=long.Parse(ds.Tables[0].Rows[0]["StanID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CommID"].ToString()!="")
				{
					model.CommID=int.Parse(ds.Tables[0].Rows[0]["CommID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CostID"].ToString()!="")
				{
					model.CostID=long.Parse(ds.Tables[0].Rows[0]["CostID"].ToString());
				}
				model.StanSign=ds.Tables[0].Rows[0]["StanSign"].ToString();
				model.StanName=ds.Tables[0].Rows[0]["StanName"].ToString();
				model.StanExplain=ds.Tables[0].Rows[0]["StanExplain"].ToString();
				model.StanFormula=ds.Tables[0].Rows[0]["StanFormula"].ToString();
				if(ds.Tables[0].Rows[0]["StanAmount"].ToString()!="")
				{
					model.StanAmount=decimal.Parse(ds.Tables[0].Rows[0]["StanAmount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["StanStartDate"].ToString()!="")
				{
					model.StanStartDate=DateTime.Parse(ds.Tables[0].Rows[0]["StanStartDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["StanEndDate"].ToString()!="")
				{
					model.StanEndDate=DateTime.Parse(ds.Tables[0].Rows[0]["StanEndDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsCondition"].ToString()!="")
				{
					model.IsCondition=int.Parse(ds.Tables[0].Rows[0]["IsCondition"].ToString());
				}
				model.ConditionField=ds.Tables[0].Rows[0]["ConditionField"].ToString();
				if(ds.Tables[0].Rows[0]["DelinRates"].ToString()!="")
				{
					model.DelinRates=decimal.Parse(ds.Tables[0].Rows[0]["DelinRates"].ToString());
				}
				if(ds.Tables[0].Rows[0]["DelinDelay"].ToString()!="")
				{
					model.DelinDelay=int.Parse(ds.Tables[0].Rows[0]["DelinDelay"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsDelete"].ToString()!="")
				{
					model.IsDelete=int.Parse(ds.Tables[0].Rows[0]["IsDelete"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsStanRange"].ToString()!="")
				{
					model.IsStanRange=int.Parse(ds.Tables[0].Rows[0]["IsStanRange"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ChargeCycle"].ToString()!="")
				{
					model.ChargeCycle=int.Parse(ds.Tables[0].Rows[0]["ChargeCycle"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ManageFeesStyle"].ToString()!="")
				{
					model.ManageFeesStyle=int.Parse(ds.Tables[0].Rows[0]["ManageFeesStyle"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ManageFeesAmount"].ToString()!="")
				{
					model.ManageFeesAmount=decimal.Parse(ds.Tables[0].Rows[0]["ManageFeesAmount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CorpStanID"].ToString()!="")
				{
					model.CorpStanID=long.Parse(ds.Tables[0].Rows[0]["CorpStanID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CorpCostID"].ToString()!="")
				{
					model.CorpCostID=long.Parse(ds.Tables[0].Rows[0]["CorpCostID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["AmountRounded"].ToString()!="")
				{
					model.AmountRounded=decimal.Parse(ds.Tables[0].Rows[0]["AmountRounded"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Modulus"].ToString()!="")
				{
					model.Modulus=decimal.Parse(ds.Tables[0].Rows[0]["Modulus"].ToString());
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
			strSql.Append("select StanID,CommID,CostID,StanSign,StanName,StanExplain,StanFormula,StanAmount,StanStartDate,StanEndDate,IsCondition,ConditionField,DelinRates,DelinDelay,IsDelete,IsStanRange,ChargeCycle,ManageFeesStyle,ManageFeesAmount,CorpStanID,CorpCostID,AmountRounded,Modulus,DelinType,DelinDay ");
			strSql.Append(" FROM Tb_HSPR_CostStandard ");
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
			strSql.Append(" StanID,CommID,CostID,StanSign,StanName,StanExplain,StanFormula,StanAmount,StanStartDate,StanEndDate,IsCondition,ConditionField,DelinRates,DelinDelay,IsDelete,IsStanRange,ChargeCycle,ManageFeesStyle,ManageFeesAmount,CorpStanID,CorpCostID,AmountRounded,Modulus,DelinType,DelinDay ");
			strSql.Append(" FROM Tb_HSPR_CostStandard ");
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
			parameters[5].Value = "SELECT * FROM Tb_HSPR_CostStandard WHERE 1=1 " + StrCondition;
			parameters[6].Value = "StanID";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

