using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.Management
{
	/// <summary>
	/// 数据访问类Dal_Tb_Management_Order。
	/// </summary>
	public class Dal_Tb_Management_Order
	{
		public Dal_Tb_Management_Order()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string OrderCode)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@OrderCode", SqlDbType.VarChar,50)};
			parameters[0].Value = OrderCode;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_Management_Order_Exists",parameters,out rowsAffected);
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
		public void Add(MobileSoft.Model.Management.Tb_Management_Order model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@OrderCode", SqlDbType.VarChar,36),
					new SqlParameter("@CommID", SqlDbType.VarChar,36),
					new SqlParameter("@ServiceCode", SqlDbType.VarChar,36),
					new SqlParameter("@OrderNum", SqlDbType.VarChar,36),
					new SqlParameter("@CustID", SqlDbType.BigInt,8),
					new SqlParameter("@RoomID", SqlDbType.BigInt,8),
					new SqlParameter("@OrderDate", SqlDbType.DateTime),
					new SqlParameter("@OrderMethod", SqlDbType.VarChar,36),
					new SqlParameter("@Contact", SqlDbType.VarChar,36),
					new SqlParameter("@ContactTel", SqlDbType.VarChar,36),
					new SqlParameter("@StartDate", SqlDbType.DateTime),
					new SqlParameter("@EndDate", SqlDbType.DateTime),
					new SqlParameter("@OrderCount", SqlDbType.Int,4),
					new SqlParameter("@OrderAmount", SqlDbType.Decimal,9),
					new SqlParameter("@TotalAmount", SqlDbType.Decimal,9),
					new SqlParameter("@IsPay", SqlDbType.VarChar,10),
					new SqlParameter("@Memo", SqlDbType.VarChar,3999),
					new SqlParameter("@IsAssign", SqlDbType.VarChar,36),
					new SqlParameter("@AssignName", SqlDbType.VarChar,36),
					new SqlParameter("@AssignDate", SqlDbType.DateTime),
					new SqlParameter("@IsDeal", SqlDbType.VarChar,36),
					new SqlParameter("@DealName", SqlDbType.VarChar,36),
					new SqlParameter("@DealDate", SqlDbType.DateTime),
					new SqlParameter("@DeliveryCount", SqlDbType.Int,4),
					new SqlParameter("@IsVisit", SqlDbType.VarChar,10),
					new SqlParameter("@VisitName", SqlDbType.VarChar,36),
					new SqlParameter("@VisitDate", SqlDbType.DateTime),
					new SqlParameter("@Evaluation", SqlDbType.VarChar,3999),
					new SqlParameter("@CustOpinion", SqlDbType.VarChar,3999),
					new SqlParameter("@IsExit", SqlDbType.VarChar,10),
					new SqlParameter("@ExitName", SqlDbType.VarChar,36),
					new SqlParameter("@ExitDate", SqlDbType.DateTime),
					new SqlParameter("@ExitReason", SqlDbType.VarChar,3999),
					new SqlParameter("@DealMemo", SqlDbType.VarChar,3999),
					new SqlParameter("@StanName", SqlDbType.VarChar,36),
					new SqlParameter("@StanFormulaName", SqlDbType.VarChar,36),
					new SqlParameter("@StanAmount", SqlDbType.Decimal,9),
					new SqlParameter("@IsDelete", SqlDbType.Int,4)};
			parameters[0].Value = model.OrderCode;
			parameters[1].Value = model.CommID;
			parameters[2].Value = model.ServiceCode;
			parameters[3].Value = model.OrderNum;
			parameters[4].Value = model.CustID;
			parameters[5].Value = model.RoomID;
			parameters[6].Value = model.OrderDate;
			parameters[7].Value = model.OrderMethod;
			parameters[8].Value = model.Contact;
			parameters[9].Value = model.ContactTel;
			parameters[10].Value = model.StartDate;
			parameters[11].Value = model.EndDate;
			parameters[12].Value = model.OrderCount;
			parameters[13].Value = model.OrderAmount;
			parameters[14].Value = model.TotalAmount;
			parameters[15].Value = model.IsPay;
			parameters[16].Value = model.Memo;
			parameters[17].Value = model.IsAssign;
			parameters[18].Value = model.AssignName;
			parameters[19].Value = model.AssignDate;
			parameters[20].Value = model.IsDeal;
			parameters[21].Value = model.DealName;
			parameters[22].Value = model.DealDate;
			parameters[23].Value = model.DeliveryCount;
			parameters[24].Value = model.IsVisit;
			parameters[25].Value = model.VisitName;
			parameters[26].Value = model.VisitDate;
			parameters[27].Value = model.Evaluation;
			parameters[28].Value = model.CustOpinion;
			parameters[29].Value = model.IsExit;
			parameters[30].Value = model.ExitName;
			parameters[31].Value = model.ExitDate;
			parameters[32].Value = model.ExitReason;
			parameters[33].Value = model.DealMemo;
			parameters[34].Value = model.StanName;
			parameters[35].Value = model.StanFormulaName;
			parameters[36].Value = model.StanAmount;
			parameters[37].Value = model.IsDelete;

			DbHelperSQL.RunProcedure("Proc_Tb_Management_Order_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.Management.Tb_Management_Order model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@OrderCode", SqlDbType.VarChar,36),
					new SqlParameter("@CommID", SqlDbType.VarChar,36),
					new SqlParameter("@ServiceCode", SqlDbType.VarChar,36),
					new SqlParameter("@OrderNum", SqlDbType.VarChar,36),
					new SqlParameter("@CustID", SqlDbType.BigInt,8),
					new SqlParameter("@RoomID", SqlDbType.BigInt,8),
					new SqlParameter("@OrderDate", SqlDbType.DateTime),
					new SqlParameter("@OrderMethod", SqlDbType.VarChar,36),
					new SqlParameter("@Contact", SqlDbType.VarChar,36),
					new SqlParameter("@ContactTel", SqlDbType.VarChar,36),
					new SqlParameter("@StartDate", SqlDbType.DateTime),
					new SqlParameter("@EndDate", SqlDbType.DateTime),
					new SqlParameter("@OrderCount", SqlDbType.Int,4),
					new SqlParameter("@OrderAmount", SqlDbType.Decimal,9),
					new SqlParameter("@TotalAmount", SqlDbType.Decimal,9),
					new SqlParameter("@IsPay", SqlDbType.VarChar,10),
					new SqlParameter("@Memo", SqlDbType.VarChar,3999),
					new SqlParameter("@IsAssign", SqlDbType.VarChar,36),
					new SqlParameter("@AssignName", SqlDbType.VarChar,36),
					new SqlParameter("@AssignDate", SqlDbType.DateTime),
					new SqlParameter("@IsDeal", SqlDbType.VarChar,36),
					new SqlParameter("@DealName", SqlDbType.VarChar,36),
					new SqlParameter("@DealDate", SqlDbType.DateTime),
					new SqlParameter("@DeliveryCount", SqlDbType.Int,4),
					new SqlParameter("@IsVisit", SqlDbType.VarChar,10),
					new SqlParameter("@VisitName", SqlDbType.VarChar,36),
					new SqlParameter("@VisitDate", SqlDbType.DateTime),
					new SqlParameter("@Evaluation", SqlDbType.VarChar,3999),
					new SqlParameter("@CustOpinion", SqlDbType.VarChar,3999),
					new SqlParameter("@IsExit", SqlDbType.VarChar,10),
					new SqlParameter("@ExitName", SqlDbType.VarChar,36),
					new SqlParameter("@ExitDate", SqlDbType.DateTime),
					new SqlParameter("@ExitReason", SqlDbType.VarChar,3999),
					new SqlParameter("@DealMemo", SqlDbType.VarChar,3999),
					new SqlParameter("@StanName", SqlDbType.VarChar,36),
					new SqlParameter("@StanFormulaName", SqlDbType.VarChar,36),
					new SqlParameter("@StanAmount", SqlDbType.Decimal,9),
					new SqlParameter("@IsDelete", SqlDbType.Int,4)};
			parameters[0].Value = model.OrderCode;
			parameters[1].Value = model.CommID;
			parameters[2].Value = model.ServiceCode;
			parameters[3].Value = model.OrderNum;
			parameters[4].Value = model.CustID;
			parameters[5].Value = model.RoomID;
			parameters[6].Value = model.OrderDate;
			parameters[7].Value = model.OrderMethod;
			parameters[8].Value = model.Contact;
			parameters[9].Value = model.ContactTel;
			parameters[10].Value = model.StartDate;
			parameters[11].Value = model.EndDate;
			parameters[12].Value = model.OrderCount;
			parameters[13].Value = model.OrderAmount;
			parameters[14].Value = model.TotalAmount;
			parameters[15].Value = model.IsPay;
			parameters[16].Value = model.Memo;
			parameters[17].Value = model.IsAssign;
			parameters[18].Value = model.AssignName;
			parameters[19].Value = model.AssignDate;
			parameters[20].Value = model.IsDeal;
			parameters[21].Value = model.DealName;
			parameters[22].Value = model.DealDate;
			parameters[23].Value = model.DeliveryCount;
			parameters[24].Value = model.IsVisit;
			parameters[25].Value = model.VisitName;
			parameters[26].Value = model.VisitDate;
			parameters[27].Value = model.Evaluation;
			parameters[28].Value = model.CustOpinion;
			parameters[29].Value = model.IsExit;
			parameters[30].Value = model.ExitName;
			parameters[31].Value = model.ExitDate;
			parameters[32].Value = model.ExitReason;
			parameters[33].Value = model.DealMemo;
			parameters[34].Value = model.StanName;
			parameters[35].Value = model.StanFormulaName;
			parameters[36].Value = model.StanAmount;
			parameters[37].Value = model.IsDelete;

			DbHelperSQL.RunProcedure("Proc_Tb_Management_Order_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(string OrderCode)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@OrderCode", SqlDbType.VarChar,50)};
			parameters[0].Value = OrderCode;

			DbHelperSQL.RunProcedure("Proc_Tb_Management_Order_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.Management.Tb_Management_Order GetModel(string OrderCode)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@OrderCode", SqlDbType.VarChar,50)};
			parameters[0].Value = OrderCode;

			MobileSoft.Model.Management.Tb_Management_Order model=new MobileSoft.Model.Management.Tb_Management_Order();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_Management_Order_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				model.OrderCode=ds.Tables[0].Rows[0]["OrderCode"].ToString();
				model.CommID=ds.Tables[0].Rows[0]["CommID"].ToString();
				model.ServiceCode=ds.Tables[0].Rows[0]["ServiceCode"].ToString();
				model.OrderNum=ds.Tables[0].Rows[0]["OrderNum"].ToString();
				if(ds.Tables[0].Rows[0]["CustID"].ToString()!="")
				{
					model.CustID=long.Parse(ds.Tables[0].Rows[0]["CustID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["RoomID"].ToString()!="")
				{
					model.RoomID=long.Parse(ds.Tables[0].Rows[0]["RoomID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["OrderDate"].ToString()!="")
				{
					model.OrderDate=DateTime.Parse(ds.Tables[0].Rows[0]["OrderDate"].ToString());
				}
				model.OrderMethod=ds.Tables[0].Rows[0]["OrderMethod"].ToString();
				model.Contact=ds.Tables[0].Rows[0]["Contact"].ToString();
				model.ContactTel=ds.Tables[0].Rows[0]["ContactTel"].ToString();
				if(ds.Tables[0].Rows[0]["StartDate"].ToString()!="")
				{
					model.StartDate=DateTime.Parse(ds.Tables[0].Rows[0]["StartDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["EndDate"].ToString()!="")
				{
					model.EndDate=DateTime.Parse(ds.Tables[0].Rows[0]["EndDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["OrderCount"].ToString()!="")
				{
					model.OrderCount=int.Parse(ds.Tables[0].Rows[0]["OrderCount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["OrderAmount"].ToString()!="")
				{
					model.OrderAmount=decimal.Parse(ds.Tables[0].Rows[0]["OrderAmount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["TotalAmount"].ToString()!="")
				{
					model.TotalAmount=decimal.Parse(ds.Tables[0].Rows[0]["TotalAmount"].ToString());
				}
				model.IsPay=ds.Tables[0].Rows[0]["IsPay"].ToString();
				model.Memo=ds.Tables[0].Rows[0]["Memo"].ToString();
				model.IsAssign=ds.Tables[0].Rows[0]["IsAssign"].ToString();
				model.AssignName=ds.Tables[0].Rows[0]["AssignName"].ToString();
				if(ds.Tables[0].Rows[0]["AssignDate"].ToString()!="")
				{
					model.AssignDate=DateTime.Parse(ds.Tables[0].Rows[0]["AssignDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsDeal"].ToString()!="")
				{
					model.IsDeal=ds.Tables[0].Rows[0]["IsDeal"].ToString();
				}
				model.DealName=ds.Tables[0].Rows[0]["DealName"].ToString();
				if(ds.Tables[0].Rows[0]["DealDate"].ToString()!="")
				{
					model.DealDate=DateTime.Parse(ds.Tables[0].Rows[0]["DealDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["DeliveryCount"].ToString()!="")
				{
					model.DeliveryCount=int.Parse(ds.Tables[0].Rows[0]["DeliveryCount"].ToString());
				}
				model.IsVisit=ds.Tables[0].Rows[0]["IsVisit"].ToString();
				model.VisitName=ds.Tables[0].Rows[0]["VisitName"].ToString();
				if(ds.Tables[0].Rows[0]["VisitDate"].ToString()!="")
				{
					model.VisitDate=DateTime.Parse(ds.Tables[0].Rows[0]["VisitDate"].ToString());
				}
				model.Evaluation=ds.Tables[0].Rows[0]["Evaluation"].ToString();
				model.CustOpinion=ds.Tables[0].Rows[0]["CustOpinion"].ToString();
				model.IsExit=ds.Tables[0].Rows[0]["IsExit"].ToString();
				model.ExitName=ds.Tables[0].Rows[0]["ExitName"].ToString();
				if(ds.Tables[0].Rows[0]["ExitDate"].ToString()!="")
				{
					model.ExitDate=DateTime.Parse(ds.Tables[0].Rows[0]["ExitDate"].ToString());
				}
				model.ExitReason=ds.Tables[0].Rows[0]["ExitReason"].ToString();
				model.DealMemo=ds.Tables[0].Rows[0]["DealMemo"].ToString();
				model.StanName=ds.Tables[0].Rows[0]["StanName"].ToString();
				model.StanFormulaName=ds.Tables[0].Rows[0]["StanFormulaName"].ToString();
				if(ds.Tables[0].Rows[0]["StanAmount"].ToString()!="")
				{
					model.StanAmount=decimal.Parse(ds.Tables[0].Rows[0]["StanAmount"].ToString());
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
			strSql.Append(" FROM View_Management_Order_Filter ");
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
			strSql.Append(" * ");
            strSql.Append(" FROM View_Management_Order_Filter ");
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
            parameters[5].Value = "SELECT * FROM View_Management_Order_Filter WHERE 1=1 " + StrCondition;
			parameters[6].Value = "OrderCode";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

