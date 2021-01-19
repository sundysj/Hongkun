using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.Resources
{
	/// <summary>
	/// 数据访问类Dal_Tb_Resources_ReleaseTrain。
	/// </summary>
	public class Dal_Tb_Resources_ReleaseTrain
	{
		public Dal_Tb_Resources_ReleaseTrain()
		{
            DbHelperSQL.ConnectionString = PubConstant.SQMBSContionString;
		}
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public long GetMaxId()
		{
		return DbHelperSQL.GetMaxID("ReleaseTrainID", "Tb_Resources_ReleaseTrain"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long ReleaseTrainID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ReleaseTrainID", SqlDbType.BigInt)};
			parameters[0].Value = ReleaseTrainID;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_Resources_ReleaseTrain_Exists",parameters,out rowsAffected);
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
		public long Add(MobileSoft.Model.Resources.Tb_Resources_ReleaseTrain model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ReleaseTrainID", SqlDbType.BigInt,8),
					new SqlParameter("@ReleaseTrainContent", SqlDbType.NText),
					new SqlParameter("@ReleaseTrainNeedKnow", SqlDbType.NText),
					new SqlParameter("@ReleaseID", SqlDbType.BigInt,8)};
			parameters[0].Direction = ParameterDirection.Output;
			parameters[1].Value = model.ReleaseTrainContent;
			parameters[2].Value = model.ReleaseTrainNeedKnow;
			parameters[3].Value = model.ReleaseID;

			DbHelperSQL.RunProcedure("Proc_Tb_Resources_ReleaseTrain_ADD",parameters,out rowsAffected);
			return (long)parameters[0].Value;
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.Resources.Tb_Resources_ReleaseTrain model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ReleaseTrainID", SqlDbType.BigInt,8),
					new SqlParameter("@ReleaseTrainContent", SqlDbType.NText),
					new SqlParameter("@ReleaseTrainNeedKnow", SqlDbType.NText),
					new SqlParameter("@ReleaseID", SqlDbType.BigInt,8)};
			parameters[0].Value = model.ReleaseTrainID;
			parameters[1].Value = model.ReleaseTrainContent;
			parameters[2].Value = model.ReleaseTrainNeedKnow;
			parameters[3].Value = model.ReleaseID;

			DbHelperSQL.RunProcedure("Proc_Tb_Resources_ReleaseTrain_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long ReleaseTrainID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ReleaseTrainID", SqlDbType.BigInt)};
			parameters[0].Value = ReleaseTrainID;

			DbHelperSQL.RunProcedure("Proc_Tb_Resources_ReleaseTrain_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.Resources.Tb_Resources_ReleaseTrain GetModel(long ReleaseTrainID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@ReleaseTrainID", SqlDbType.BigInt)};
			parameters[0].Value = ReleaseTrainID;

			MobileSoft.Model.Resources.Tb_Resources_ReleaseTrain model=new MobileSoft.Model.Resources.Tb_Resources_ReleaseTrain();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_Resources_ReleaseTrain_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ReleaseTrainID"].ToString()!="")
				{
					model.ReleaseTrainID=long.Parse(ds.Tables[0].Rows[0]["ReleaseTrainID"].ToString());
				}
				model.ReleaseTrainContent=ds.Tables[0].Rows[0]["ReleaseTrainContent"].ToString();
				model.ReleaseTrainNeedKnow=ds.Tables[0].Rows[0]["ReleaseTrainNeedKnow"].ToString();
				if(ds.Tables[0].Rows[0]["ReleaseID"].ToString()!="")
				{
					model.ReleaseID=long.Parse(ds.Tables[0].Rows[0]["ReleaseID"].ToString());
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
			strSql.Append("select ReleaseTrainID,ReleaseTrainContent,ReleaseTrainNeedKnow,ReleaseID ");
			strSql.Append(" FROM Tb_Resources_ReleaseTrain ");
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
			strSql.Append(" ReleaseTrainID,ReleaseTrainContent,ReleaseTrainNeedKnow,ReleaseID ");
			strSql.Append(" FROM Tb_Resources_ReleaseTrain ");
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
			parameters[5].Value = "SELECT * FROM Tb_Resources_ReleaseTrain WHERE 1=1 " + StrCondition;
			parameters[6].Value = "ReleaseTrainID";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

