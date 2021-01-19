using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.System
{
	/// <summary>
	/// 数据访问类Dal_Tb_System_SmsNatAccount。
	/// </summary>
	public class Dal_Tb_System_SmsNatAccount
	{
		public Dal_Tb_System_SmsNatAccount()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(Guid CommCode,long CommID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@CommCode", SqlDbType.UniqueIdentifier),
					new SqlParameter("@CommID", SqlDbType.BigInt)};
			parameters[0].Value = CommCode;
			parameters[1].Value = CommID;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_System_SmsNatAccount_Exists",parameters,out rowsAffected);
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
		public void Add(MobileSoft.Model.System.Tb_System_SmsNatAccount model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@CommCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@CommID", SqlDbType.BigInt,8),
					new SqlParameter("@Circle", SqlDbType.NVarChar,20),
					new SqlParameter("@PassWord", SqlDbType.NVarChar,20),
					new SqlParameter("@Balance", SqlDbType.Int,4),
					new SqlParameter("@WayType", SqlDbType.SmallInt,2)};
			parameters[0].Value = model.CommCode;
			parameters[1].Value = model.CommID;
			parameters[2].Value = model.Circle;
			parameters[3].Value = model.PassWord;
			parameters[4].Value = model.Balance;
			parameters[5].Value = model.WayType;

			DbHelperSQL.RunProcedure("Proc_Tb_System_SmsNatAccount_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.System.Tb_System_SmsNatAccount model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@CommCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@CommID", SqlDbType.BigInt,8),
					new SqlParameter("@Circle", SqlDbType.NVarChar,20),
					new SqlParameter("@PassWord", SqlDbType.NVarChar,20),
					new SqlParameter("@Balance", SqlDbType.Int,4),
					new SqlParameter("@WayType", SqlDbType.SmallInt,2)};
			parameters[0].Value = model.CommCode;
			parameters[1].Value = model.CommID;
			parameters[2].Value = model.Circle;
			parameters[3].Value = model.PassWord;
			parameters[4].Value = model.Balance;
			parameters[5].Value = model.WayType;

			DbHelperSQL.RunProcedure("Proc_Tb_System_SmsNatAccount_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(Guid CommCode,long CommID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@CommCode", SqlDbType.UniqueIdentifier),
					new SqlParameter("@CommID", SqlDbType.BigInt)};
			parameters[0].Value = CommCode;
			parameters[1].Value = CommID;

			DbHelperSQL.RunProcedure("Proc_Tb_System_SmsNatAccount_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.System.Tb_System_SmsNatAccount GetModel(Guid CommCode,long CommID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@CommCode", SqlDbType.UniqueIdentifier),
					new SqlParameter("@CommID", SqlDbType.BigInt)};
			parameters[0].Value = CommCode;
			parameters[1].Value = CommID;

			MobileSoft.Model.System.Tb_System_SmsNatAccount model=new MobileSoft.Model.System.Tb_System_SmsNatAccount();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_System_SmsNatAccount_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["CommCode"].ToString()!="")
				{
					model.CommCode=new Guid(ds.Tables[0].Rows[0]["CommCode"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CommID"].ToString()!="")
				{
					model.CommID=long.Parse(ds.Tables[0].Rows[0]["CommID"].ToString());
				}
				model.Circle=ds.Tables[0].Rows[0]["Circle"].ToString();
				model.PassWord=ds.Tables[0].Rows[0]["PassWord"].ToString();
				if(ds.Tables[0].Rows[0]["Balance"].ToString()!="")
				{
					model.Balance=int.Parse(ds.Tables[0].Rows[0]["Balance"].ToString());
				}
				if(ds.Tables[0].Rows[0]["WayType"].ToString()!="")
				{
					model.WayType=int.Parse(ds.Tables[0].Rows[0]["WayType"].ToString());
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
			strSql.Append("select CommCode,CommID,Circle,PassWord,Balance,WayType ");
			strSql.Append(" FROM Tb_System_SmsNatAccount ");
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
			strSql.Append(" CommCode,CommID,Circle,PassWord,Balance,WayType ");
			strSql.Append(" FROM Tb_System_SmsNatAccount ");
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
			parameters[5].Value = "SELECT * FROM Tb_System_SmsNatAccount WHERE 1=1 " + StrCondition;
			parameters[6].Value = "CommCode,CommID";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

