using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.Order
{
	/// <summary>
	/// 数据访问类Dal_Tb_Order_RegistWare。
	/// </summary>
	public class Dal_Tb_Order_RegistWare
	{
		public Dal_Tb_Order_RegistWare()
		{
            DbHelperSQL.ConnectionString = PubConstant.SQMBSContionString;
		}
		#region  成员方法

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(Guid RegistWareID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@RegistWareID", SqlDbType.UniqueIdentifier)};
			parameters[0].Value = RegistWareID;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_Order_RegistWare_Exists",parameters,out rowsAffected);
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
		public void Add(MobileSoft.Model.Order.Tb_Order_RegistWare model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@RegistWareID", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@ReleaseID", SqlDbType.BigInt,8),
					new SqlParameter("@Count", SqlDbType.Float,8),
					new SqlParameter("@RegistID", SqlDbType.VarChar,36),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2),
                    new SqlParameter("@ResourcesSalePrice", SqlDbType.Float,8),
                    new SqlParameter("@ResourcesDisCountPrice", SqlDbType.Float,8),
                    new SqlParameter("@GroupBuyPrice", SqlDbType.Float,8)};
			parameters[0].Value = model.RegistWareID;
			parameters[1].Value = model.ReleaseID;
			parameters[2].Value = model.Count;
			parameters[3].Value = model.RegistID;
			parameters[4].Value = model.IsDelete;
            parameters[5].Value = model.ResourcesSalePrice;
            parameters[6].Value = model.ResourcesDisCountPrice;
            parameters[7].Value = model.GroupBuyPrice;

			DbHelperSQL.RunProcedure("Proc_Tb_Order_RegistWare_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.Order.Tb_Order_RegistWare model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@RegistWareID", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@ReleaseID", SqlDbType.BigInt,8),
					new SqlParameter("@Count", SqlDbType.Float,8),
					new SqlParameter("@RegistID", SqlDbType.VarChar,36),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2),
                    new SqlParameter("@ResourcesSalePrice", SqlDbType.Float,8),
                    new SqlParameter("@ResourcesDisCountPrice", SqlDbType.Float,8),
                    new SqlParameter("@GroupBuyPrice", SqlDbType.Float,8)};
			parameters[0].Value = model.RegistWareID;
			parameters[1].Value = model.ReleaseID;
			parameters[2].Value = model.Count;
			parameters[3].Value = model.RegistID;
			parameters[4].Value = model.IsDelete;
            parameters[5].Value = model.ResourcesSalePrice;
            parameters[6].Value = model.ResourcesDisCountPrice;
            parameters[7].Value = model.GroupBuyPrice;

			DbHelperSQL.RunProcedure("Proc_Tb_Order_RegistWare_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(Guid RegistWareID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@RegistWareID", SqlDbType.UniqueIdentifier)};
			parameters[0].Value = RegistWareID;

			DbHelperSQL.RunProcedure("Proc_Tb_Order_RegistWare_Delete",parameters,out rowsAffected);
		}

        public void DeleteNotIn(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DELETE");
            strSql.Append(" Tb_Order_RegistWare ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
                DbHelperSQL.Query(strSql.ToString());
            }
        }

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.Order.Tb_Order_RegistWare GetModel(Guid RegistWareID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@RegistWareID", SqlDbType.UniqueIdentifier)};
			parameters[0].Value = RegistWareID;

			MobileSoft.Model.Order.Tb_Order_RegistWare model=new MobileSoft.Model.Order.Tb_Order_RegistWare();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_Order_RegistWare_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["RegistWareID"].ToString()!="")
				{
					model.RegistWareID=new Guid(ds.Tables[0].Rows[0]["RegistWareID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ReleaseID"].ToString()!="")
				{
					model.ReleaseID=long.Parse(ds.Tables[0].Rows[0]["ReleaseID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Count"].ToString()!="")
				{
					model.Count=decimal.Parse(ds.Tables[0].Rows[0]["Count"].ToString());
				}
				model.RegistID=ds.Tables[0].Rows[0]["RegistID"].ToString();
				if(ds.Tables[0].Rows[0]["IsDelete"].ToString()!="")
				{
					model.IsDelete=int.Parse(ds.Tables[0].Rows[0]["IsDelete"].ToString());
				}
                if (ds.Tables[0].Rows[0]["ResourcesSalePrice"].ToString() != "")
                {
                    model.ResourcesSalePrice = decimal.Parse(ds.Tables[0].Rows[0]["ResourcesSalePrice"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ResourcesDisCountPrice"].ToString() != "")
                {
                    model.ResourcesDisCountPrice = decimal.Parse(ds.Tables[0].Rows[0]["ResourcesDisCountPrice"].ToString());
                }
                if (ds.Tables[0].Rows[0]["GroupBuyPrice"].ToString() != "")
                {
                    model.GroupBuyPrice = decimal.Parse(ds.Tables[0].Rows[0]["GroupBuyPrice"].ToString());
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
            strSql.Append(" FROM View_Order_RegistWare_Filter ");
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
            strSql.Append(" FROM View_Order_RegistWare_Filter ");
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
            parameters[5].Value = "SELECT * FROM View_Order_RegistWare_Filter WHERE 1=1 " + StrCondition;
			parameters[6].Value = "RegistWareID";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

