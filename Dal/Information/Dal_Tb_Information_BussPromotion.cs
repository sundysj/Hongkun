using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.Information
{
	/// <summary>
	/// 数据访问类Dal_Tb_Information_BussPromotion。
	/// </summary>
	public class Dal_Tb_Information_BussPromotion
	{
		public Dal_Tb_Information_BussPromotion()
		{
            DbHelperSQL.ConnectionString = PubConstant.SQMBSContionString;
		}
		#region  成员方法

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long ProID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ProID", SqlDbType.BigInt)};
			parameters[0].Value = ProID;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_Information_BussPromotion_Exists",parameters,out rowsAffected);
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
		public long Add(MobileSoft.Model.Information.Tb_Information_BussPromotion model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ProID", SqlDbType.BigInt,8),
					new SqlParameter("@BussId", SqlDbType.BigInt,8),
					new SqlParameter("@Project", SqlDbType.NVarChar,100),
					new SqlParameter("@Publisher", SqlDbType.NVarChar,100),
					new SqlParameter("@PublishDate", SqlDbType.DateTime),
					new SqlParameter("@Reason", SqlDbType.NVarChar,500),
					new SqlParameter("@ProImage", SqlDbType.NVarChar,100),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2),
					new SqlParameter("@NumID", SqlDbType.BigInt,8)};
			parameters[0].Direction = ParameterDirection.Output;
			parameters[1].Value = model.BussId;
			parameters[2].Value = model.Project;
			parameters[3].Value = model.Publisher;
			parameters[4].Value = model.PublishDate;
			parameters[5].Value = model.Reason;
			parameters[6].Value = model.ProImage;
			parameters[7].Value = model.IsDelete;
			parameters[8].Value = model.NumID;

			DbHelperSQL.RunProcedure("Proc_Tb_Information_BussPromotion_ADD",parameters,out rowsAffected);
			return (long)parameters[0].Value;
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.Information.Tb_Information_BussPromotion model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ProID", SqlDbType.BigInt,8),
					new SqlParameter("@BussId", SqlDbType.BigInt,8),
					new SqlParameter("@Project", SqlDbType.NVarChar,100),
					new SqlParameter("@Publisher", SqlDbType.NVarChar,100),
					new SqlParameter("@PublishDate", SqlDbType.DateTime),
					new SqlParameter("@Reason", SqlDbType.NVarChar,500),
					new SqlParameter("@ProImage", SqlDbType.NVarChar,100),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2),
					new SqlParameter("@NumID", SqlDbType.BigInt,8)};
			parameters[0].Value = model.ProID;
			parameters[1].Value = model.BussId;
			parameters[2].Value = model.Project;
			parameters[3].Value = model.Publisher;
			parameters[4].Value = model.PublishDate;
			parameters[5].Value = model.Reason;
			parameters[6].Value = model.ProImage;
			parameters[7].Value = model.IsDelete;
			parameters[8].Value = model.NumID;

			DbHelperSQL.RunProcedure("Proc_Tb_Information_BussPromotion_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long ProID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ProID", SqlDbType.BigInt)};
			parameters[0].Value = ProID;

			DbHelperSQL.RunProcedure("Proc_Tb_Information_BussPromotion_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.Information.Tb_Information_BussPromotion GetModel(long ProID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@ProID", SqlDbType.BigInt)};
			parameters[0].Value = ProID;

			MobileSoft.Model.Information.Tb_Information_BussPromotion model=new MobileSoft.Model.Information.Tb_Information_BussPromotion();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_Information_BussPromotion_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ProID"].ToString()!="")
				{
					model.ProID=long.Parse(ds.Tables[0].Rows[0]["ProID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["BussId"].ToString()!="")
				{
					model.BussId=long.Parse(ds.Tables[0].Rows[0]["BussId"].ToString());
				}
				model.Project=ds.Tables[0].Rows[0]["Project"].ToString();
				model.Publisher=ds.Tables[0].Rows[0]["Publisher"].ToString();
				if(ds.Tables[0].Rows[0]["PublishDate"].ToString()!="")
				{
					model.PublishDate=DateTime.Parse(ds.Tables[0].Rows[0]["PublishDate"].ToString());
				}
				model.Reason=ds.Tables[0].Rows[0]["Reason"].ToString();
				model.ProImage=ds.Tables[0].Rows[0]["ProImage"].ToString();
				if(ds.Tables[0].Rows[0]["IsDelete"].ToString()!="")
				{
					model.IsDelete=int.Parse(ds.Tables[0].Rows[0]["IsDelete"].ToString());
				}
				if(ds.Tables[0].Rows[0]["NumID"].ToString()!="")
				{
					model.NumID=long.Parse(ds.Tables[0].Rows[0]["NumID"].ToString());
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
			strSql.Append("select ProID,BussId,Project,Publisher,PublishDate,Reason,ProImage,IsDelete,NumID ");
			strSql.Append(" FROM Tb_Information_BussPromotion ");
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
			strSql.Append(" ProID,BussId,Project,Publisher,PublishDate,Reason,ProImage,IsDelete,NumID ");
			strSql.Append(" FROM Tb_Information_BussPromotion ");
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
			parameters[5].Value = "SELECT * FROM Tb_Information_BussPromotion WHERE 1=1 " + StrCondition;
			parameters[6].Value = "ProID";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

