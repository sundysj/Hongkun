using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.Order
{
	/// <summary>
	/// 数据访问类Dal_Tb_Order_Regist。
	/// </summary>
	public class Dal_Tb_Order_Regist
	{
		public Dal_Tb_Order_Regist()
		{
            DbHelperSQL.ConnectionString = PubConstant.SQMBSContionString;
		}
		#region  成员方法

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string RegistID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@RegistID", SqlDbType.VarChar,50)};
			parameters[0].Value = RegistID;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_Order_Regist_Exists",parameters,out rowsAffected);
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
		public void Add(MobileSoft.Model.Order.Tb_Order_Regist model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@RegistID", SqlDbType.VarChar,36),
					new SqlParameter("@RegistNumber", SqlDbType.NVarChar,50),
					new SqlParameter("@CustomerType", SqlDbType.NVarChar,50),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@CustName", SqlDbType.NVarChar,50),
					new SqlParameter("@CustID", SqlDbType.BigInt,8),
					new SqlParameter("@MobilePhone", SqlDbType.NVarChar,50),
					new SqlParameter("@Address", SqlDbType.NVarChar,200),
					new SqlParameter("@DocumentDate", SqlDbType.DateTime),
					new SqlParameter("@RegistMessage", SqlDbType.NVarChar,500),
					new SqlParameter("@ProcessingStatus", SqlDbType.NVarChar,50),
					new SqlParameter("@ProcessingExplain", SqlDbType.NVarChar,500),
					new SqlParameter("@ReleaseID", SqlDbType.BigInt,8),
					new SqlParameter("@ScheduleType", SqlDbType.NVarChar,50),
					new SqlParameter("@ReleaseServiceComDate", SqlDbType.DateTime),
					new SqlParameter("@ReleaseServiceComtime", SqlDbType.NVarChar,50),
					new SqlParameter("@ReleaseStartTime", SqlDbType.DateTime),
					new SqlParameter("@ReleaseEndTime", SqlDbType.DateTime),
					new SqlParameter("@ReleasePeriodOfStarttime", SqlDbType.NVarChar,50),
					new SqlParameter("@ReleasePeriodOfEndtime", SqlDbType.NVarChar,50),
					new SqlParameter("@UserCode", SqlDbType.NVarChar,50),
					new SqlParameter("@IssueDate", SqlDbType.DateTime),
					new SqlParameter("@BussId", SqlDbType.BigInt,8),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2),
					new SqlParameter("@SumCount", SqlDbType.Float,8),
					new SqlParameter("@SumPrice", SqlDbType.Float,8),
                    new SqlParameter("@ResourcesSalePrice", SqlDbType.Float,8),
                    new SqlParameter("@ResourcesDisCountPrice", SqlDbType.Float,8),
                    new SqlParameter("@GroupBuyPrice", SqlDbType.Float,8),
                    new SqlParameter("@CommName", SqlDbType.NVarChar,500)};
			parameters[0].Value = model.RegistID;
			parameters[1].Value = model.RegistNumber;
			parameters[2].Value = model.CustomerType;
			parameters[3].Value = model.CommID;
			parameters[4].Value = model.CustName;
			parameters[5].Value = model.CustID;
			parameters[6].Value = model.MobilePhone;
			parameters[7].Value = model.Address;
			parameters[8].Value = model.DocumentDate;
			parameters[9].Value = model.RegistMessage;
			parameters[10].Value = model.ProcessingStatus;
			parameters[11].Value = model.ProcessingExplain;
			parameters[12].Value = model.ReleaseID;
			parameters[13].Value = model.ScheduleType;
			parameters[14].Value = model.ReleaseServiceComDate;
			parameters[15].Value = model.ReleaseServiceComtime;
			parameters[16].Value = model.ReleaseStartTime;
			parameters[17].Value = model.ReleaseEndTime;
			parameters[18].Value = model.ReleasePeriodOfStarttime;
			parameters[19].Value = model.ReleasePeriodOfEndtime;
			parameters[20].Value = model.UserCode;
			parameters[21].Value = model.IssueDate;
			parameters[22].Value = model.BussId;
			parameters[23].Value = model.IsDelete;
			parameters[24].Value = model.SumCount;
			parameters[25].Value = model.SumPrice;
            parameters[26].Value = model.ResourcesSalePrice;
            parameters[27].Value = model.ResourcesDisCountPrice;
            parameters[28].Value = model.GroupBuyPrice;
            parameters[29].Value = model.CommName;

			DbHelperSQL.RunProcedure("Proc_Tb_Order_Regist_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.Order.Tb_Order_Regist model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@RegistID", SqlDbType.VarChar,36),
					new SqlParameter("@RegistNumber", SqlDbType.NVarChar,50),
					new SqlParameter("@CustomerType", SqlDbType.NVarChar,50),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@CustName", SqlDbType.NVarChar,50),
					new SqlParameter("@CustID", SqlDbType.BigInt,8),
					new SqlParameter("@MobilePhone", SqlDbType.NVarChar,50),
					new SqlParameter("@Address", SqlDbType.NVarChar,200),
					new SqlParameter("@DocumentDate", SqlDbType.DateTime),
					new SqlParameter("@RegistMessage", SqlDbType.NVarChar,500),
					new SqlParameter("@ProcessingStatus", SqlDbType.NVarChar,50),
					new SqlParameter("@ProcessingExplain", SqlDbType.NVarChar,500),
					new SqlParameter("@ReleaseID", SqlDbType.BigInt,8),
					new SqlParameter("@ScheduleType", SqlDbType.NVarChar,50),
					new SqlParameter("@ReleaseServiceComDate", SqlDbType.DateTime),
					new SqlParameter("@ReleaseServiceComtime", SqlDbType.NVarChar,50),
					new SqlParameter("@ReleaseStartTime", SqlDbType.DateTime),
					new SqlParameter("@ReleaseEndTime", SqlDbType.DateTime),
					new SqlParameter("@ReleasePeriodOfStarttime", SqlDbType.NVarChar,50),
					new SqlParameter("@ReleasePeriodOfEndtime", SqlDbType.NVarChar,50),
					new SqlParameter("@UserCode", SqlDbType.NVarChar,50),
					new SqlParameter("@IssueDate", SqlDbType.DateTime),
					new SqlParameter("@BussId", SqlDbType.BigInt,8),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2),
					new SqlParameter("@SumCount", SqlDbType.Float,8),
					new SqlParameter("@SumPrice", SqlDbType.Float,8),
                    new SqlParameter("@ResourcesSalePrice", SqlDbType.Float,8),
                    new SqlParameter("@ResourcesDisCountPrice", SqlDbType.Float,8),
                    new SqlParameter("@GroupBuyPrice", SqlDbType.Float,8)};
			parameters[0].Value = model.RegistID;
			parameters[1].Value = model.RegistNumber;
			parameters[2].Value = model.CustomerType;
			parameters[3].Value = model.CommID;
			parameters[4].Value = model.CustName;
			parameters[5].Value = model.CustID;
			parameters[6].Value = model.MobilePhone;
			parameters[7].Value = model.Address;
			parameters[8].Value = model.DocumentDate;
			parameters[9].Value = model.RegistMessage;
			parameters[10].Value = model.ProcessingStatus;
			parameters[11].Value = model.ProcessingExplain;
			parameters[12].Value = model.ReleaseID;
			parameters[13].Value = model.ScheduleType;
			parameters[14].Value = model.ReleaseServiceComDate;
			parameters[15].Value = model.ReleaseServiceComtime;
			parameters[16].Value = model.ReleaseStartTime;
			parameters[17].Value = model.ReleaseEndTime;
			parameters[18].Value = model.ReleasePeriodOfStarttime;
			parameters[19].Value = model.ReleasePeriodOfEndtime;
			parameters[20].Value = model.UserCode;
			parameters[21].Value = model.IssueDate;
			parameters[22].Value = model.BussId;
			parameters[23].Value = model.IsDelete;
			parameters[24].Value = model.SumCount;
			parameters[25].Value = model.SumPrice;
            parameters[26].Value = model.ResourcesSalePrice;
            parameters[27].Value = model.ResourcesDisCountPrice;
            parameters[28].Value = model.GroupBuyPrice;

			DbHelperSQL.RunProcedure("Proc_Tb_Order_Regist_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(string RegistID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@RegistID", SqlDbType.VarChar,50)};
			parameters[0].Value = RegistID;

			DbHelperSQL.RunProcedure("Proc_Tb_Order_Regist_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.Order.Tb_Order_Regist GetModel(string RegistID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@RegistID", SqlDbType.VarChar,50)};
			parameters[0].Value = RegistID;

			MobileSoft.Model.Order.Tb_Order_Regist model=new MobileSoft.Model.Order.Tb_Order_Regist();
            
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_Order_Regist_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				model.RegistID=ds.Tables[0].Rows[0]["RegistID"].ToString();
				model.RegistNumber=ds.Tables[0].Rows[0]["RegistNumber"].ToString();
				model.CustomerType=ds.Tables[0].Rows[0]["CustomerType"].ToString();
				if(ds.Tables[0].Rows[0]["CommID"].ToString()!="")
				{
					model.CommID=int.Parse(ds.Tables[0].Rows[0]["CommID"].ToString());
				}
				model.CustName=ds.Tables[0].Rows[0]["CustName"].ToString();
                model.CommName = ds.Tables[0].Rows[0]["CommName"].ToString();
				if(ds.Tables[0].Rows[0]["CustID"].ToString()!="")
				{
					model.CustID=long.Parse(ds.Tables[0].Rows[0]["CustID"].ToString());
				}
				model.MobilePhone=ds.Tables[0].Rows[0]["MobilePhone"].ToString();
				model.Address=ds.Tables[0].Rows[0]["Address"].ToString();
				if(ds.Tables[0].Rows[0]["DocumentDate"].ToString()!="")
				{
					model.DocumentDate=DateTime.Parse(ds.Tables[0].Rows[0]["DocumentDate"].ToString());
				}
				model.RegistMessage=ds.Tables[0].Rows[0]["RegistMessage"].ToString();
				model.ProcessingStatus=ds.Tables[0].Rows[0]["ProcessingStatus"].ToString();
				model.ProcessingExplain=ds.Tables[0].Rows[0]["ProcessingExplain"].ToString();
				if(ds.Tables[0].Rows[0]["ReleaseID"].ToString()!="")
				{
					model.ReleaseID=long.Parse(ds.Tables[0].Rows[0]["ReleaseID"].ToString());
				}
				model.ScheduleType=ds.Tables[0].Rows[0]["ScheduleType"].ToString();
				if(ds.Tables[0].Rows[0]["ReleaseServiceComDate"].ToString()!="")
				{
					model.ReleaseServiceComDate=DateTime.Parse(ds.Tables[0].Rows[0]["ReleaseServiceComDate"].ToString());
				}
				model.ReleaseServiceComtime=ds.Tables[0].Rows[0]["ReleaseServiceComtime"].ToString();
				if(ds.Tables[0].Rows[0]["ReleaseStartTime"].ToString()!="")
				{
					model.ReleaseStartTime=DateTime.Parse(ds.Tables[0].Rows[0]["ReleaseStartTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ReleaseEndTime"].ToString()!="")
				{
					model.ReleaseEndTime=DateTime.Parse(ds.Tables[0].Rows[0]["ReleaseEndTime"].ToString());
				}
				model.ReleasePeriodOfStarttime=ds.Tables[0].Rows[0]["ReleasePeriodOfStarttime"].ToString();
				model.ReleasePeriodOfEndtime=ds.Tables[0].Rows[0]["ReleasePeriodOfEndtime"].ToString();
				model.UserCode=ds.Tables[0].Rows[0]["UserCode"].ToString();
				if(ds.Tables[0].Rows[0]["IssueDate"].ToString()!="")
				{
					model.IssueDate=DateTime.Parse(ds.Tables[0].Rows[0]["IssueDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["BussId"].ToString()!="")
				{
					model.BussId=long.Parse(ds.Tables[0].Rows[0]["BussId"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsDelete"].ToString()!="")
				{
					model.IsDelete=int.Parse(ds.Tables[0].Rows[0]["IsDelete"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SumCount"].ToString()!="")
				{
					model.SumCount=decimal.Parse(ds.Tables[0].Rows[0]["SumCount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SumPrice"].ToString()!="")
				{
					model.SumPrice=decimal.Parse(ds.Tables[0].Rows[0]["SumPrice"].ToString());
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
            strSql.Append(" FROM view_Order_Regist_Filter ");
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
			strSql.Append(" FROM Tb_Order_Regist ");
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
            parameters[5].Value = "SELECT * FROM view_Order_Regist_Filter WHERE 1=1 " + StrCondition;
			parameters[6].Value = "RegistID";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

