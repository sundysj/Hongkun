using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.Information
{
	/// <summary>
	/// 数据访问类Dal_Tb_Information_AboutUs。
	/// </summary>
	public class Dal_Tb_Information_AboutUs
	{
		public Dal_Tb_Information_AboutUs()
		{
            DbHelperSQL.ConnectionString = PubConstant.SQMBSContionString;
		}
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public long GetMaxId()
		{
		return DbHelperSQL.GetMaxID("AboutId", "Tb_Information_AboutUs"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long AboutId)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@AboutId", SqlDbType.BigInt)};
			parameters[0].Value = AboutId;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_Information_AboutUs_Exists",parameters,out rowsAffected);
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
		public long Add(MobileSoft.Model.Information.Tb_Information_AboutUs model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@AboutId", SqlDbType.BigInt,8),
					new SqlParameter("@BussId", SqlDbType.BigInt,8),
					new SqlParameter("@Title", SqlDbType.NVarChar,200),
					new SqlParameter("@AboutPublisher", SqlDbType.NVarChar,200),
					new SqlParameter("@PubulishDate", SqlDbType.DateTime),
					new SqlParameter("@AboutContent", SqlDbType.NVarChar),
					new SqlParameter("@AboutImage", SqlDbType.NVarChar,200),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2)};
			parameters[0].Direction = ParameterDirection.Output;
			parameters[1].Value = model.BussId;
			parameters[2].Value = model.Title;
			parameters[3].Value = model.AboutPublisher;
			parameters[4].Value = model.PubulishDate;
			parameters[5].Value = model.AboutContent;
			parameters[6].Value = model.AboutImage;
			parameters[7].Value = model.IsDelete;

			DbHelperSQL.RunProcedure("Proc_Tb_Information_AboutUs_ADD",parameters,out rowsAffected);
			return (long)parameters[0].Value;
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.Information.Tb_Information_AboutUs model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@AboutId", SqlDbType.BigInt,8),
					new SqlParameter("@BussId", SqlDbType.BigInt,8),
					new SqlParameter("@Title", SqlDbType.NVarChar,200),
					new SqlParameter("@AboutPublisher", SqlDbType.NVarChar,200),
					new SqlParameter("@PubulishDate", SqlDbType.DateTime),
					new SqlParameter("@AboutContent", SqlDbType.NVarChar),
					new SqlParameter("@AboutImage", SqlDbType.NVarChar,200),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2)};
			parameters[0].Value = model.AboutId;
			parameters[1].Value = model.BussId;
			parameters[2].Value = model.Title;
			parameters[3].Value = model.AboutPublisher;
			parameters[4].Value = model.PubulishDate;
			parameters[5].Value = model.AboutContent;
			parameters[6].Value = model.AboutImage;
			parameters[7].Value = model.IsDelete;

			DbHelperSQL.RunProcedure("Proc_Tb_Information_AboutUs_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long AboutId)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@AboutId", SqlDbType.BigInt)};
			parameters[0].Value = AboutId;

			DbHelperSQL.RunProcedure("Proc_Tb_Information_AboutUs_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.Information.Tb_Information_AboutUs GetModel(long AboutId)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@AboutId", SqlDbType.BigInt)};
			parameters[0].Value = AboutId;

			MobileSoft.Model.Information.Tb_Information_AboutUs model=new MobileSoft.Model.Information.Tb_Information_AboutUs();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_Information_AboutUs_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["AboutId"].ToString()!="")
				{
					model.AboutId=long.Parse(ds.Tables[0].Rows[0]["AboutId"].ToString());
				}
				if(ds.Tables[0].Rows[0]["BussId"].ToString()!="")
				{
					model.BussId=long.Parse(ds.Tables[0].Rows[0]["BussId"].ToString());
				}
				model.Title=ds.Tables[0].Rows[0]["Title"].ToString();
				model.AboutPublisher=ds.Tables[0].Rows[0]["AboutPublisher"].ToString();
				if(ds.Tables[0].Rows[0]["PubulishDate"].ToString()!="")
				{
					model.PubulishDate=DateTime.Parse(ds.Tables[0].Rows[0]["PubulishDate"].ToString());
				}
				model.AboutContent=ds.Tables[0].Rows[0]["AboutContent"].ToString();
				model.AboutImage=ds.Tables[0].Rows[0]["AboutImage"].ToString();
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
			strSql.Append("select AboutId,BussId,Title,AboutPublisher,PubulishDate,AboutContent,AboutImage,IsDelete ");
			strSql.Append(" FROM Tb_Information_AboutUs ");
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
			strSql.Append(" AboutId,BussId,Title,AboutPublisher,PubulishDate,AboutContent,AboutImage,IsDelete ");
			strSql.Append(" FROM Tb_Information_AboutUs ");
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
			parameters[5].Value = "SELECT * FROM Tb_Information_AboutUs WHERE 1=1 " + StrCondition;
			parameters[6].Value = "AboutId";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

