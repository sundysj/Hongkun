using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.Common
{
	/// <summary>
	/// 数据访问类Dal_Tb_Common_TelList。
	/// </summary>
	public class Dal_Tb_Common_TelList
	{
		public Dal_Tb_Common_TelList()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public long GetMaxId()
		{
		return DbHelperSQL.GetMaxID("InfoId", "Tb_Common_TelList"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long InfoId)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.BigInt)};
			parameters[0].Value = InfoId;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_Common_TelList_Exists",parameters,out rowsAffected);
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
		public long Add(MobileSoft.Model.Common.Tb_Common_TelList model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.BigInt,8),
					new SqlParameter("@UserCode", SqlDbType.NVarChar,255),
					new SqlParameter("@TelNum", SqlDbType.NVarChar,255),
					new SqlParameter("@Mark", SqlDbType.NVarChar,255),
					new SqlParameter("@Duty", SqlDbType.NVarChar,255),
					new SqlParameter("@Sort", SqlDbType.Int,4),
					new SqlParameter("@Phone", SqlDbType.NVarChar,255),
					new SqlParameter("@Mail", SqlDbType.NVarChar,50),
					new SqlParameter("@CompanyName", SqlDbType.NVarChar,255),
					new SqlParameter("@CommName", SqlDbType.NVarChar,255),
					new SqlParameter("@OprName", SqlDbType.NVarChar,255),
					new SqlParameter("@CompanyNameSort", SqlDbType.Int,4),
					new SqlParameter("@CommNameSort", SqlDbType.Int,4),
					new SqlParameter("@DutySort", SqlDbType.Int,4)};
			parameters[0].Direction = ParameterDirection.Output;
			parameters[1].Value = model.UserCode;
			parameters[2].Value = model.TelNum;
			parameters[3].Value = model.Mark;
			parameters[4].Value = model.Duty;
			parameters[5].Value = model.Sort;
			parameters[6].Value = model.Phone;
			parameters[7].Value = model.Mail;
			parameters[8].Value = model.CompanyName;
			parameters[9].Value = model.CommName;
			parameters[10].Value = model.OprName;
			parameters[11].Value = model.CompanyNameSort;
			parameters[12].Value = model.CommNameSort;
			parameters[13].Value = model.DutySort;

			DbHelperSQL.RunProcedure("Proc_Tb_Common_TelList_ADD",parameters,out rowsAffected);
			return (long)parameters[0].Value;
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.Common.Tb_Common_TelList model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.BigInt,8),
					new SqlParameter("@UserCode", SqlDbType.NVarChar,255),
					new SqlParameter("@TelNum", SqlDbType.NVarChar,255),
					new SqlParameter("@Mark", SqlDbType.NVarChar,255),
					new SqlParameter("@Duty", SqlDbType.NVarChar,255),
					new SqlParameter("@Sort", SqlDbType.Int,4),
					new SqlParameter("@Phone", SqlDbType.NVarChar,255),
					new SqlParameter("@Mail", SqlDbType.NVarChar,50),
					new SqlParameter("@CompanyName", SqlDbType.NVarChar,255),
					new SqlParameter("@CommName", SqlDbType.NVarChar,255),
					new SqlParameter("@OprName", SqlDbType.NVarChar,255),
					new SqlParameter("@CompanyNameSort", SqlDbType.Int,4),
					new SqlParameter("@CommNameSort", SqlDbType.Int,4),
					new SqlParameter("@DutySort", SqlDbType.Int,4)};
			parameters[0].Value = model.InfoId;
			parameters[1].Value = model.UserCode;
			parameters[2].Value = model.TelNum;
			parameters[3].Value = model.Mark;
			parameters[4].Value = model.Duty;
			parameters[5].Value = model.Sort;
			parameters[6].Value = model.Phone;
			parameters[7].Value = model.Mail;
			parameters[8].Value = model.CompanyName;
			parameters[9].Value = model.CommName;
			parameters[10].Value = model.OprName;
			parameters[11].Value = model.CompanyNameSort;
			parameters[12].Value = model.CommNameSort;
			parameters[13].Value = model.DutySort;

			DbHelperSQL.RunProcedure("Proc_Tb_Common_TelList_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long InfoId)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.BigInt)};
			parameters[0].Value = InfoId;

			DbHelperSQL.RunProcedure("Proc_Tb_Common_TelList_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.Common.Tb_Common_TelList GetModel(long InfoId)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.BigInt)};
			parameters[0].Value = InfoId;

			MobileSoft.Model.Common.Tb_Common_TelList model=new MobileSoft.Model.Common.Tb_Common_TelList();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_Common_TelList_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["InfoId"].ToString()!="")
				{
					model.InfoId=long.Parse(ds.Tables[0].Rows[0]["InfoId"].ToString());
				}
				model.UserCode=ds.Tables[0].Rows[0]["UserCode"].ToString();
				model.TelNum=ds.Tables[0].Rows[0]["TelNum"].ToString();
				model.Mark=ds.Tables[0].Rows[0]["Mark"].ToString();
				model.Duty=ds.Tables[0].Rows[0]["Duty"].ToString();
				if(ds.Tables[0].Rows[0]["Sort"].ToString()!="")
				{
					model.Sort=int.Parse(ds.Tables[0].Rows[0]["Sort"].ToString());
				}
				model.Phone=ds.Tables[0].Rows[0]["Phone"].ToString();
				model.Mail=ds.Tables[0].Rows[0]["Mail"].ToString();
				model.CompanyName=ds.Tables[0].Rows[0]["CompanyName"].ToString();
				model.CommName=ds.Tables[0].Rows[0]["CommName"].ToString();
				model.OprName=ds.Tables[0].Rows[0]["OprName"].ToString();
				if(ds.Tables[0].Rows[0]["CompanyNameSort"].ToString()!="")
				{
					model.CompanyNameSort=int.Parse(ds.Tables[0].Rows[0]["CompanyNameSort"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CommNameSort"].ToString()!="")
				{
					model.CommNameSort=int.Parse(ds.Tables[0].Rows[0]["CommNameSort"].ToString());
				}
				if(ds.Tables[0].Rows[0]["DutySort"].ToString()!="")
				{
					model.DutySort=int.Parse(ds.Tables[0].Rows[0]["DutySort"].ToString());
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
			strSql.Append("select InfoId,UserCode,TelNum,Mark,Duty,Sort,Phone,Mail,CompanyName,CommName,OprName,CompanyNameSort,CommNameSort,DutySort ");
			strSql.Append(" FROM Tb_Common_TelList ");
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
			strSql.Append(" InfoId,UserCode,TelNum,Mark,Duty,Sort,Phone,Mail,CompanyName,CommName,OprName,CompanyNameSort,CommNameSort,DutySort ");
			strSql.Append(" FROM Tb_Common_TelList ");
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
			parameters[5].Value = "SELECT * FROM Tb_Common_TelList WHERE 1=1 " + StrCondition;
			parameters[6].Value = "InfoId";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

