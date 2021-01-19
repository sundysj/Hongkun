using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.OAPublicWork
{
	/// <summary>
	/// 数据访问类Dal_Tb_OAPublicWork_ConferenceResource。
	/// </summary>
	public class Dal_Tb_OAPublicWork_ConferenceResource
	{
		public Dal_Tb_OAPublicWork_ConferenceResource()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int InfoId)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.Int,4)};
			parameters[0].Value = InfoId;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_ConferenceResource_Exists",parameters,out rowsAffected);
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
		public int Add(MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_ConferenceResource model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.Int,4),
					new SqlParameter("@ConferenceName", SqlDbType.VarChar,100),
					new SqlParameter("@ConferencePlace", SqlDbType.VarChar,100),
					new SqlParameter("@ConferenceMj", SqlDbType.VarChar,20),
					new SqlParameter("@ConferenceHowMany", SqlDbType.VarChar,20),
					new SqlParameter("@ConferenceThing", SqlDbType.VarChar,100),
					new SqlParameter("@ConferencePrice", SqlDbType.VarChar,20),
					new SqlParameter("@ConferencePassWord", SqlDbType.VarChar,100),
					new SqlParameter("@Mark", SqlDbType.VarChar,1000)};
			parameters[0].Direction = ParameterDirection.Output;
			parameters[1].Value = model.ConferenceName;
			parameters[2].Value = model.ConferencePlace;
			parameters[3].Value = model.ConferenceMj;
			parameters[4].Value = model.ConferenceHowMany;
			parameters[5].Value = model.ConferenceThing;
			parameters[6].Value = model.ConferencePrice;
			parameters[7].Value = model.ConferencePassWord;
			parameters[8].Value = model.Mark;

			DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_ConferenceResource_ADD",parameters,out rowsAffected);
			return (int)parameters[0].Value;
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_ConferenceResource model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.Int,4),
					new SqlParameter("@ConferenceName", SqlDbType.VarChar,100),
					new SqlParameter("@ConferencePlace", SqlDbType.VarChar,100),
					new SqlParameter("@ConferenceMj", SqlDbType.VarChar,20),
					new SqlParameter("@ConferenceHowMany", SqlDbType.VarChar,20),
					new SqlParameter("@ConferenceThing", SqlDbType.VarChar,100),
					new SqlParameter("@ConferencePrice", SqlDbType.VarChar,20),
					new SqlParameter("@ConferencePassWord", SqlDbType.VarChar,100),
					new SqlParameter("@Mark", SqlDbType.VarChar,1000)};
			parameters[0].Value = model.InfoId;
			parameters[1].Value = model.ConferenceName;
			parameters[2].Value = model.ConferencePlace;
			parameters[3].Value = model.ConferenceMj;
			parameters[4].Value = model.ConferenceHowMany;
			parameters[5].Value = model.ConferenceThing;
			parameters[6].Value = model.ConferencePrice;
			parameters[7].Value = model.ConferencePassWord;
			parameters[8].Value = model.Mark;

			DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_ConferenceResource_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int InfoId)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.Int,4)};
			parameters[0].Value = InfoId;

			DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_ConferenceResource_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_ConferenceResource GetModel(int InfoId)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.Int,4)};
			parameters[0].Value = InfoId;

			MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_ConferenceResource model=new MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_ConferenceResource();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_ConferenceResource_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["InfoId"].ToString()!="")
				{
					model.InfoId=int.Parse(ds.Tables[0].Rows[0]["InfoId"].ToString());
				}
				model.ConferenceName=ds.Tables[0].Rows[0]["ConferenceName"].ToString();
				model.ConferencePlace=ds.Tables[0].Rows[0]["ConferencePlace"].ToString();
				model.ConferenceMj=ds.Tables[0].Rows[0]["ConferenceMj"].ToString();
				model.ConferenceHowMany=ds.Tables[0].Rows[0]["ConferenceHowMany"].ToString();
				model.ConferenceThing=ds.Tables[0].Rows[0]["ConferenceThing"].ToString();
				model.ConferencePrice=ds.Tables[0].Rows[0]["ConferencePrice"].ToString();
				model.ConferencePassWord=ds.Tables[0].Rows[0]["ConferencePassWord"].ToString();
				model.Mark=ds.Tables[0].Rows[0]["Mark"].ToString();
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
			strSql.Append("select InfoId,ConferenceName,ConferencePlace,ConferenceMj,ConferenceHowMany,ConferenceThing,ConferencePrice,ConferencePassWord,Mark ");
			strSql.Append(" FROM Tb_OAPublicWork_ConferenceResource ");
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
			strSql.Append(" InfoId,ConferenceName,ConferencePlace,ConferenceMj,ConferenceHowMany,ConferenceThing,ConferencePrice,ConferencePassWord,Mark ");
			strSql.Append(" FROM Tb_OAPublicWork_ConferenceResource ");
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
			parameters[5].Value = "SELECT * FROM Tb_OAPublicWork_ConferenceResource WHERE 1=1 " + StrCondition;
			parameters[6].Value = "InfoId";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

