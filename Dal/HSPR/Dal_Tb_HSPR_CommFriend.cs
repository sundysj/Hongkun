using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.HSPR
{
	/// <summary>
	/// 数据访问类Dal_Tb_HSPR_CommFriend。
	/// </summary>
	public class Dal_Tb_HSPR_CommFriend
	{
		public Dal_Tb_HSPR_CommFriend()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string FriendCode)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@FriendCode", SqlDbType.VarChar,50)};
			parameters[0].Value = FriendCode;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_HSPR_CommFriend_Exists",parameters,out rowsAffected);
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
		public void Add(MobileSoft.Model.HSPR.Tb_HSPR_CommFriend model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@FriendCode", SqlDbType.VarChar,36),
					new SqlParameter("@CustId", SqlDbType.VarChar,36),
					new SqlParameter("@FriendCustId", SqlDbType.VarChar,36),
					new SqlParameter("@RoomSign", SqlDbType.VarChar,36),
					new SqlParameter("@IsDelete", SqlDbType.Int,4)};
			parameters[0].Value = model.FriendCode;
			parameters[1].Value = model.CustId;
			parameters[2].Value = model.FriendCustId;
			parameters[3].Value = model.RoomSign;
			parameters[4].Value = model.IsDelete;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_CommFriend_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.HSPR.Tb_HSPR_CommFriend model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@FriendCode", SqlDbType.VarChar,36),
					new SqlParameter("@CustId", SqlDbType.VarChar,36),
					new SqlParameter("@FriendCustId", SqlDbType.VarChar,36),
					new SqlParameter("@RoomSign", SqlDbType.VarChar,36),
					new SqlParameter("@IsDelete", SqlDbType.Int,4)};
			parameters[0].Value = model.FriendCode;
			parameters[1].Value = model.CustId;
			parameters[2].Value = model.FriendCustId;
			parameters[3].Value = model.RoomSign;
			parameters[4].Value = model.IsDelete;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_CommFriend_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(string FriendCode)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@FriendCode", SqlDbType.VarChar,50)};
			parameters[0].Value = FriendCode;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_CommFriend_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_CommFriend GetModel(string FriendCode)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@FriendCode", SqlDbType.VarChar,50)};
			parameters[0].Value = FriendCode;

			MobileSoft.Model.HSPR.Tb_HSPR_CommFriend model=new MobileSoft.Model.HSPR.Tb_HSPR_CommFriend();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_HSPR_CommFriend_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				model.FriendCode=ds.Tables[0].Rows[0]["FriendCode"].ToString();
				model.CustId=ds.Tables[0].Rows[0]["CustId"].ToString();
				model.FriendCustId=ds.Tables[0].Rows[0]["FriendCustId"].ToString();
				model.RoomSign=ds.Tables[0].Rows[0]["RoomSign"].ToString();
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
            strSql.Append(" FROM View_HSPR_CommFriend_Filter ");
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
			strSql.Append(" * ");
            strSql.Append(" FROM View_HSPR_CommFriend_Filter ");
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
            parameters[5].Value = "SELECT * FROM View_HSPR_CommFriend_Filter WHERE 1=1 " + StrCondition;
			parameters[6].Value = "FriendCode";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

