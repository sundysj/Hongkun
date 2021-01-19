using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.Resources
{
	/// <summary>
	/// 数据访问类Dal_Tb_Resources_Release。
	/// </summary>
	public class Dal_Tb_Resources_Release
	{
		public Dal_Tb_Resources_Release()
		{
			DbHelperSQL.ConnectionString = PubConstant.SQMBSContionString;
		}
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public long GetMaxId()
		{
		return DbHelperSQL.GetMaxID("ReleaseID", "Tb_Resources_Release"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long ReleaseID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ReleaseID", SqlDbType.BigInt)};
			parameters[0].Value = ReleaseID;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_Resources_Release_Exists",parameters,out rowsAffected);
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
		public long Add(MobileSoft.Model.Resources.Tb_Resources_Release model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ReleaseID", SqlDbType.BigInt,8),
					new SqlParameter("@ResourcesID", SqlDbType.BigInt,8),
					new SqlParameter("@ReleaseAdContent", SqlDbType.NVarChar,200),
					new SqlParameter("@ReleaseCount", SqlDbType.Float,8),
					new SqlParameter("@IsGroupBuy", SqlDbType.Bit,1),
					new SqlParameter("@GroupBuyPrice", SqlDbType.Float,8),
					new SqlParameter("@GroupBuyStartData", SqlDbType.DateTime),
					new SqlParameter("@GroupBuyEndData", SqlDbType.DateTime),
					new SqlParameter("@PaymentType", SqlDbType.NVarChar,50),
					new SqlParameter("@BussId", SqlDbType.BigInt,8),
					new SqlParameter("@IsStop", SqlDbType.Bit,1),
					new SqlParameter("@IsDelete", SqlDbType.SmallDateTime)};
			parameters[0].Direction = ParameterDirection.Output;
			parameters[1].Value = model.ResourcesID;
			parameters[2].Value = model.ReleaseAdContent;
			parameters[3].Value = model.ReleaseCount;
			parameters[4].Value = model.IsGroupBuy;
			parameters[5].Value = model.GroupBuyPrice;
			parameters[6].Value = model.GroupBuyStartData;
			parameters[7].Value = model.GroupBuyEndData;
			parameters[8].Value = model.PaymentType;
			parameters[9].Value = model.BussId;
			parameters[10].Value = model.IsStop;
			parameters[11].Value = model.IsDelete;

			DbHelperSQL.RunProcedure("Proc_Tb_Resources_Release_ADD",parameters,out rowsAffected);
			return (long)parameters[0].Value;
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.Resources.Tb_Resources_Release model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ReleaseID", SqlDbType.BigInt,8),
					new SqlParameter("@ResourcesID", SqlDbType.BigInt,8),
					new SqlParameter("@ReleaseAdContent", SqlDbType.NVarChar,200),
					new SqlParameter("@ReleaseCount", SqlDbType.Float,8),
					new SqlParameter("@IsGroupBuy", SqlDbType.Bit,1),
					new SqlParameter("@GroupBuyPrice", SqlDbType.Float,8),
					new SqlParameter("@GroupBuyStartData", SqlDbType.DateTime),
					new SqlParameter("@GroupBuyEndData", SqlDbType.DateTime),
					new SqlParameter("@PaymentType", SqlDbType.NVarChar,50),
					new SqlParameter("@BussId", SqlDbType.BigInt,8),
					new SqlParameter("@IsStop", SqlDbType.Bit,1),
					new SqlParameter("@IsDelete", SqlDbType.SmallDateTime)};
			parameters[0].Value = model.ReleaseID;
			parameters[1].Value = model.ResourcesID;
			parameters[2].Value = model.ReleaseAdContent;
			parameters[3].Value = model.ReleaseCount;
			parameters[4].Value = model.IsGroupBuy;
			parameters[5].Value = model.GroupBuyPrice;
			parameters[6].Value = model.GroupBuyStartData;
			parameters[7].Value = model.GroupBuyEndData;
			parameters[8].Value = model.PaymentType;
			parameters[9].Value = model.BussId;
			parameters[10].Value = model.IsStop;
			parameters[11].Value = model.IsDelete;

			DbHelperSQL.RunProcedure("Proc_Tb_Resources_Release_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long ReleaseID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ReleaseID", SqlDbType.BigInt)};
			parameters[0].Value = ReleaseID;

			DbHelperSQL.RunProcedure("Proc_Tb_Resources_Release_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.Resources.Tb_Resources_Release GetModel(long ReleaseID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@ReleaseID", SqlDbType.BigInt)};
			parameters[0].Value = ReleaseID;

			MobileSoft.Model.Resources.Tb_Resources_Release model=new MobileSoft.Model.Resources.Tb_Resources_Release();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_Resources_Release_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ReleaseID"].ToString()!="")
				{
					model.ReleaseID=long.Parse(ds.Tables[0].Rows[0]["ReleaseID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ResourcesID"].ToString()!="")
				{
					model.ResourcesID=long.Parse(ds.Tables[0].Rows[0]["ResourcesID"].ToString());
				}
				model.ReleaseAdContent=ds.Tables[0].Rows[0]["ReleaseAdContent"].ToString();
				if(ds.Tables[0].Rows[0]["ReleaseCount"].ToString()!="")
				{
					model.ReleaseCount=decimal.Parse(ds.Tables[0].Rows[0]["ReleaseCount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsGroupBuy"].ToString()!="")
				{
					if((ds.Tables[0].Rows[0]["IsGroupBuy"].ToString()=="1")||(ds.Tables[0].Rows[0]["IsGroupBuy"].ToString().ToLower()=="true"))
					{
						model.IsGroupBuy=true;
					}
					else
					{
						model.IsGroupBuy=false;
					}
				}
				if(ds.Tables[0].Rows[0]["GroupBuyPrice"].ToString()!="")
				{
					model.GroupBuyPrice=decimal.Parse(ds.Tables[0].Rows[0]["GroupBuyPrice"].ToString());
				}
				if(ds.Tables[0].Rows[0]["GroupBuyStartData"].ToString()!="")
				{
					model.GroupBuyStartData=DateTime.Parse(ds.Tables[0].Rows[0]["GroupBuyStartData"].ToString());
				}
				if(ds.Tables[0].Rows[0]["GroupBuyEndData"].ToString()!="")
				{
					model.GroupBuyEndData=DateTime.Parse(ds.Tables[0].Rows[0]["GroupBuyEndData"].ToString());
				}
				model.PaymentType=ds.Tables[0].Rows[0]["PaymentType"].ToString();
				if(ds.Tables[0].Rows[0]["BussId"].ToString()!="")
				{
					model.BussId=long.Parse(ds.Tables[0].Rows[0]["BussId"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsStop"].ToString()!="")
				{
					if((ds.Tables[0].Rows[0]["IsStop"].ToString()=="1")||(ds.Tables[0].Rows[0]["IsStop"].ToString().ToLower()=="true"))
					{
						model.IsStop=true;
					}
					else
					{
						model.IsStop=false;
					}
				}
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
            strSql.Append(" FROM view_Resources_Release_Filter ");
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
            strSql.Append(" FROM view_Resources_Release_Filter ");
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
            parameters[5].Value = "SELECT * FROM view_Resources_Release_Filter WHERE 1=1 " + StrCondition;
			parameters[6].Value = "ReleaseID";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

        public DataSet GetFreeList(out int PageCount, out int Counts, string StrCondition, int PageIndex, int PageSize, string SortField, int Sort)
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
            parameters[5].Value = StrCondition;
            parameters[6].Value = "ReleaseID";
            DataSet Ds = DbHelperSQL.RunProcedure("Proc_System_TurnPage", parameters, "RetDataSet");
            PageCount = Convert.ToInt32(parameters[7].Value);
            Counts = Convert.ToInt32(parameters[8].Value);
            return Ds;
        }
		#endregion  成员方法
	}
}

