using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.Unified
{
	/// <summary>
	/// 数据访问类Dal_Tb_User。
	/// </summary>
	public class Dal_Tb_User
	{
		public Dal_Tb_User()
		{
			DbHelperSQL.ConnectionString = PubConstant.UnifiedContionString;
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

			int result= DbHelperSQL.RunProcedure("Proc_Tb_User_Exists",parameters,out rowsAffected);
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
		public void Add(MobileSoft.Model.Unified.Tb_User model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.VarChar,36),
					new SqlParameter("@Name", SqlDbType.VarChar,36),
					new SqlParameter("@Mobile", SqlDbType.VarChar,11),
					new SqlParameter("@Email", SqlDbType.VarChar,36),
					new SqlParameter("@QQ", SqlDbType.VarChar,36),
					new SqlParameter("@QQToken", SqlDbType.VarChar,3999),
					new SqlParameter("@WeChatNum", SqlDbType.VarChar,36),
					new SqlParameter("@WeChatToken", SqlDbType.VarChar,3999),
					new SqlParameter("@NickName", SqlDbType.VarChar,36),
					new SqlParameter("@Pwd", SqlDbType.VarChar,36),
                    new SqlParameter("@UserPic", SqlDbType.VarChar,500),
                    new SqlParameter("@Sex", SqlDbType.Bit),
                    new SqlParameter("@RegDate",SqlDbType.DateTime)
            };
			parameters[0].Value = model.Id;
			parameters[1].Value = model.Name;
			parameters[2].Value = model.Mobile;
			parameters[3].Value = model.Email;
			parameters[4].Value = model.QQ;
			parameters[5].Value = model.QQToken;
			parameters[6].Value = model.WeChatNum;
			parameters[7].Value = model.WeChatToken;
			parameters[8].Value = model.NickName;
			parameters[9].Value = model.Pwd;
            parameters[10].Value = model.UserPic;
            parameters[11].Value = model.Sex;
            parameters[12].Value = model.RegDate;

            DbHelperSQL.RunProcedure("Proc_Tb_User_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.Unified.Tb_User model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.VarChar,36),
					new SqlParameter("@Name", SqlDbType.VarChar,36),
					new SqlParameter("@Mobile", SqlDbType.VarChar,11),
					new SqlParameter("@Email", SqlDbType.VarChar,36),
					new SqlParameter("@QQ", SqlDbType.VarChar,36),
					new SqlParameter("@QQToken", SqlDbType.VarChar,3999),
					new SqlParameter("@WeChatNum", SqlDbType.VarChar,36),
					new SqlParameter("@WeChatToken", SqlDbType.VarChar,3999),
					new SqlParameter("@NickName", SqlDbType.VarChar,36),
					new SqlParameter("@Pwd", SqlDbType.VarChar,36),
                     new SqlParameter("@UserPic", SqlDbType.VarChar,500),
                    new SqlParameter("@Sex", SqlDbType.Bit)
            };
			parameters[0].Value = model.Id;
			parameters[1].Value = model.Name;
			parameters[2].Value = model.Mobile;
			parameters[3].Value = model.Email;
			parameters[4].Value = model.QQ;
			parameters[5].Value = model.QQToken;
			parameters[6].Value = model.WeChatNum;
			parameters[7].Value = model.WeChatToken;
			parameters[8].Value = model.NickName;
			parameters[9].Value = model.Pwd;
            parameters[10].Value = model.UserPic;
            parameters[11].Value = model.Sex;

            DbHelperSQL.RunProcedure("Proc_Tb_User_Update",parameters,out rowsAffected);
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

			DbHelperSQL.RunProcedure("Proc_Tb_User_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.Unified.Tb_User GetModel(string Id)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.VarChar,50)};
			parameters[0].Value = Id;

			MobileSoft.Model.Unified.Tb_User model=new MobileSoft.Model.Unified.Tb_User();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_User_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				model.Id=ds.Tables[0].Rows[0]["Id"].ToString();
				model.Name=ds.Tables[0].Rows[0]["Name"].ToString();
				model.Mobile=ds.Tables[0].Rows[0]["Mobile"].ToString();
				model.Email=ds.Tables[0].Rows[0]["Email"].ToString();
				model.QQ=ds.Tables[0].Rows[0]["QQ"].ToString();
				model.QQToken=ds.Tables[0].Rows[0]["QQToken"].ToString();
				model.WeChatNum=ds.Tables[0].Rows[0]["WeChatNum"].ToString();
				model.WeChatToken=ds.Tables[0].Rows[0]["WeChatToken"].ToString();
				model.NickName=ds.Tables[0].Rows[0]["NickName"].ToString();
				model.Pwd=ds.Tables[0].Rows[0]["Pwd"].ToString();
                model.UserPic = ds.Tables[0].Rows[0]["UserPic"].ToString();
                model.Sex =Convert.ToInt32( ds.Tables[0].Rows[0]["Sex"]);
                model.RegDate =Convert.ToDateTime( ds.Tables[0].Rows[0]["RegDate"]);
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
			strSql.Append("select Id,Name,Mobile,Email,QQ,QQToken,WeChatNum,WeChatToken,NickName,Pwd,UserPic,Sex,RegDate");
			strSql.Append(" FROM Tb_User ");
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
			strSql.Append(" Id,Name,Mobile,Email,QQ,QQToken,WeChatNum,WeChatToken,NickName,Pwd,UserPic,Sex,RegDate ");
			strSql.Append(" FROM Tb_User ");
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
			parameters[5].Value = "SELECT * FROM Tb_User WHERE 1=1 " + StrCondition;
			parameters[6].Value = "Id";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

