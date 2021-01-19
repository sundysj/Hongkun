using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.Information
{
	/// <summary>
	/// 数据访问类Dal_Tb_Information_ContactUs。
	/// </summary>
	public class Dal_Tb_Information_ContactUs
	{
		public Dal_Tb_Information_ContactUs()
		{
            DbHelperSQL.ConnectionString = PubConstant.SQMBSContionString;
		}
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public long GetMaxId()
		{
		return DbHelperSQL.GetMaxID("ID", "Tb_Information_ContactUs"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long ID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt)};
			parameters[0].Value = ID;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_Information_ContactUs_Exists",parameters,out rowsAffected);
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
		public long Add(MobileSoft.Model.Information.Tb_Information_ContactUs model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt,8),
					new SqlParameter("@BussId", SqlDbType.BigInt,8),
					new SqlParameter("@BussName", SqlDbType.NVarChar,200),
					new SqlParameter("@Address", SqlDbType.NVarChar,200),
					new SqlParameter("@Postal", SqlDbType.NVarChar,200),
					new SqlParameter("@LinkMan", SqlDbType.NVarChar,200),
					new SqlParameter("@Tel", SqlDbType.NVarChar,200),
					new SqlParameter("@Phone", SqlDbType.NVarChar,200),
					new SqlParameter("@Email", SqlDbType.NVarChar,200),
					new SqlParameter("@QQ", SqlDbType.NVarChar,200),
					new SqlParameter("@Wechat", SqlDbType.NVarChar,200),
					new SqlParameter("@URL", SqlDbType.NVarChar,200),
					new SqlParameter("@Map", SqlDbType.NVarChar,200),
					new SqlParameter("@ContactUsContent", SqlDbType.NVarChar)};
			parameters[0].Direction = ParameterDirection.Output;
			parameters[1].Value = model.BussId;
			parameters[2].Value = model.BussName;
			parameters[3].Value = model.Address;
			parameters[4].Value = model.Postal;
			parameters[5].Value = model.LinkMan;
			parameters[6].Value = model.Tel;
			parameters[7].Value = model.Phone;
			parameters[8].Value = model.Email;
			parameters[9].Value = model.QQ;
			parameters[10].Value = model.Wechat;
			parameters[11].Value = model.URL;
			parameters[12].Value = model.Map;
			parameters[13].Value = model.ContactUsContent;

			DbHelperSQL.RunProcedure("Proc_Tb_Information_ContactUs_ADD",parameters,out rowsAffected);
			return (long)parameters[0].Value;
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.Information.Tb_Information_ContactUs model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt,8),
					new SqlParameter("@BussId", SqlDbType.BigInt,8),
					new SqlParameter("@BussName", SqlDbType.NVarChar,200),
					new SqlParameter("@Address", SqlDbType.NVarChar,200),
					new SqlParameter("@Postal", SqlDbType.NVarChar,200),
					new SqlParameter("@LinkMan", SqlDbType.NVarChar,200),
					new SqlParameter("@Tel", SqlDbType.NVarChar,200),
					new SqlParameter("@Phone", SqlDbType.NVarChar,200),
					new SqlParameter("@Email", SqlDbType.NVarChar,200),
					new SqlParameter("@QQ", SqlDbType.NVarChar,200),
					new SqlParameter("@Wechat", SqlDbType.NVarChar,200),
					new SqlParameter("@URL", SqlDbType.NVarChar,200),
					new SqlParameter("@Map", SqlDbType.NVarChar,200),
					new SqlParameter("@ContactUsContent", SqlDbType.NVarChar)};
			parameters[0].Value = model.ID;
			parameters[1].Value = model.BussId;
			parameters[2].Value = model.BussName;
			parameters[3].Value = model.Address;
			parameters[4].Value = model.Postal;
			parameters[5].Value = model.LinkMan;
			parameters[6].Value = model.Tel;
			parameters[7].Value = model.Phone;
			parameters[8].Value = model.Email;
			parameters[9].Value = model.QQ;
			parameters[10].Value = model.Wechat;
			parameters[11].Value = model.URL;
			parameters[12].Value = model.Map;
			parameters[13].Value = model.ContactUsContent;

			DbHelperSQL.RunProcedure("Proc_Tb_Information_ContactUs_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long ID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt)};
			parameters[0].Value = ID;

			DbHelperSQL.RunProcedure("Proc_Tb_Information_ContactUs_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.Information.Tb_Information_ContactUs GetModel(long ID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt)};
			parameters[0].Value = ID;

			MobileSoft.Model.Information.Tb_Information_ContactUs model=new MobileSoft.Model.Information.Tb_Information_ContactUs();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_Information_ContactUs_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ID"].ToString()!="")
				{
					model.ID=long.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["BussId"].ToString()!="")
				{
					model.BussId=long.Parse(ds.Tables[0].Rows[0]["BussId"].ToString());
				}
				model.BussName=ds.Tables[0].Rows[0]["BussName"].ToString();
				model.Address=ds.Tables[0].Rows[0]["Address"].ToString();
				model.Postal=ds.Tables[0].Rows[0]["Postal"].ToString();
				model.LinkMan=ds.Tables[0].Rows[0]["LinkMan"].ToString();
				model.Tel=ds.Tables[0].Rows[0]["Tel"].ToString();
				model.Phone=ds.Tables[0].Rows[0]["Phone"].ToString();
				model.Email=ds.Tables[0].Rows[0]["Email"].ToString();
				model.QQ=ds.Tables[0].Rows[0]["QQ"].ToString();
				model.Wechat=ds.Tables[0].Rows[0]["Wechat"].ToString();
				model.URL=ds.Tables[0].Rows[0]["URL"].ToString();
				model.Map=ds.Tables[0].Rows[0]["Map"].ToString();
				model.ContactUsContent=ds.Tables[0].Rows[0]["ContactUsContent"].ToString();
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
			strSql.Append("select ID,BussId,BussName,Address,Postal,LinkMan,Tel,Phone,Email,QQ,Wechat,URL,Map,ContactUsContent ");
			strSql.Append(" FROM Tb_Information_ContactUs ");
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
			strSql.Append(" ID,BussId,BussName,Address,Postal,LinkMan,Tel,Phone,Email,QQ,Wechat,URL,Map,ContactUsContent ");
			strSql.Append(" FROM Tb_Information_ContactUs ");
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
			parameters[5].Value = "SELECT * FROM Tb_Information_ContactUs WHERE 1=1 " + StrCondition;
			parameters[6].Value = "ID";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

