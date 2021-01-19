using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace HM.DAL.Eq
{
	/// <summary>
	/// 数据访问类Dal_Tb_EQ_WbTask。
	/// </summary>
	public class Dal_Tb_EQ_WbTask
	{
		public Dal_Tb_EQ_WbTask()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string TaskId)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@TaskId", SqlDbType.VarChar,50)};
			parameters[0].Value = TaskId;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_EQ_WbTask_Exists",parameters,out rowsAffected);
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
		public void Add(HM.Model.Eq.Tb_EQ_WbTask model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@TaskId", SqlDbType.VarChar,36),
					new SqlParameter("@CommId", SqlDbType.Int,4),
					new SqlParameter("@PlanId", SqlDbType.VarChar,36),
					new SqlParameter("@TaskNO", SqlDbType.NVarChar,50),
					new SqlParameter("@EqId", SqlDbType.VarChar,36),
					new SqlParameter("@SpaceId", SqlDbType.VarChar,36),
					new SqlParameter("@Content", SqlDbType.NVarChar,500),
					new SqlParameter("@BeginTime", SqlDbType.DateTime),
					new SqlParameter("@EndTime", SqlDbType.DateTime),
					new SqlParameter("@WorkDayBegin", SqlDbType.NVarChar,50),
					new SqlParameter("@RoleCode", SqlDbType.NVarChar,50),
					new SqlParameter("@Statue", SqlDbType.NVarChar,50),
					new SqlParameter("@ClosePerson", SqlDbType.VarChar,36),
					new SqlParameter("@CloseTime", SqlDbType.DateTime),
					new SqlParameter("@CloseReason", SqlDbType.NVarChar,50),
					new SqlParameter("@PollingPerson", SqlDbType.VarChar,36),
					new SqlParameter("@PollingDate", SqlDbType.DateTime),
					new SqlParameter("@Remark", SqlDbType.NVarChar,500),
					new SqlParameter("@IsDelete", SqlDbType.Int,4),
					new SqlParameter("@AddPId", SqlDbType.VarChar,36),
					new SqlParameter("@AddDate", SqlDbType.DateTime),
					new SqlParameter("@PerfNum", SqlDbType.NVarChar,50),
					new SqlParameter("@WorkTime", SqlDbType.NVarChar,50),
					new SqlParameter("@IsEntrust", SqlDbType.Int,4),
					new SqlParameter("@EntrustCompany", SqlDbType.NVarChar,50),
					new SqlParameter("@CheckRoleCode", SqlDbType.NVarChar,20),
					new SqlParameter("@PersonInCharge", SqlDbType.VarChar,36),
					new SqlParameter("@DoTime", SqlDbType.DateTime),
					new SqlParameter("@CheckMan", SqlDbType.VarChar,36),
					new SqlParameter("@CheckNoto", SqlDbType.NVarChar,500),
					new SqlParameter("@CheckRusult", SqlDbType.NVarChar,10),
					new SqlParameter("@CheckTime", SqlDbType.DateTime)};
			parameters[0].Value = model.TaskId;
			parameters[1].Value = model.CommId;
			parameters[2].Value = model.PlanId;
			parameters[3].Value = model.TaskNO;
			parameters[4].Value = model.EqId;
			parameters[5].Value = model.SpaceId;
			parameters[6].Value = model.Content;
			parameters[7].Value = model.BeginTime;
			parameters[8].Value = model.EndTime;
			parameters[9].Value = model.WorkDayBegin;
			parameters[10].Value = model.RoleCode;
			parameters[11].Value = model.Statue;
			parameters[12].Value = model.ClosePerson;
			parameters[13].Value = model.CloseTime;
			parameters[14].Value = model.CloseReason;
			parameters[15].Value = model.PollingPerson;
			parameters[16].Value = model.PollingDate;
			parameters[17].Value = model.Remark;
			parameters[18].Value = model.IsDelete;
			parameters[19].Value = model.AddPId;
			parameters[20].Value = model.AddDate;
			parameters[21].Value = model.PerfNum;
			parameters[22].Value = model.WorkTime;
			parameters[23].Value = model.IsEntrust;
			parameters[24].Value = model.EntrustCompany;
			parameters[25].Value = model.CheckRoleCode;
			parameters[26].Value = model.PersonInCharge;
			parameters[27].Value = model.DoTime;
			parameters[28].Value = model.CheckMan;
			parameters[29].Value = model.CheckNoto;
			parameters[30].Value = model.CheckRusult;
			parameters[31].Value = model.CheckTime;

			DbHelperSQL.RunProcedure("Proc_Tb_EQ_WbTask_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(HM.Model.Eq.Tb_EQ_WbTask model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@TaskId", SqlDbType.VarChar,36),
					new SqlParameter("@CommId", SqlDbType.Int,4),
					new SqlParameter("@PlanId", SqlDbType.VarChar,36),
					new SqlParameter("@TaskNO", SqlDbType.NVarChar,50),
					new SqlParameter("@EqId", SqlDbType.VarChar,36),
					new SqlParameter("@SpaceId", SqlDbType.VarChar,36),
					new SqlParameter("@Content", SqlDbType.NVarChar,500),
					new SqlParameter("@BeginTime", SqlDbType.DateTime),
					new SqlParameter("@EndTime", SqlDbType.DateTime),
					new SqlParameter("@WorkDayBegin", SqlDbType.NVarChar,50),
					new SqlParameter("@RoleCode", SqlDbType.NVarChar,50),
					new SqlParameter("@Statue", SqlDbType.NVarChar,50),
					new SqlParameter("@ClosePerson", SqlDbType.VarChar,36),
					new SqlParameter("@CloseTime", SqlDbType.DateTime),
					new SqlParameter("@CloseReason", SqlDbType.NVarChar,50),
					new SqlParameter("@PollingPerson", SqlDbType.VarChar,36),
					new SqlParameter("@PollingDate", SqlDbType.DateTime),
					new SqlParameter("@Remark", SqlDbType.NVarChar,500),
					new SqlParameter("@IsDelete", SqlDbType.Int,4),
					new SqlParameter("@AddPId", SqlDbType.VarChar,36),
					new SqlParameter("@AddDate", SqlDbType.DateTime),
					new SqlParameter("@PerfNum", SqlDbType.NVarChar,50),
					new SqlParameter("@WorkTime", SqlDbType.NVarChar,50),
					new SqlParameter("@IsEntrust", SqlDbType.Int,4),
					new SqlParameter("@EntrustCompany", SqlDbType.NVarChar,50),
					new SqlParameter("@CheckRoleCode", SqlDbType.NVarChar,20),
					new SqlParameter("@PersonInCharge", SqlDbType.VarChar,36),
					new SqlParameter("@DoTime", SqlDbType.DateTime),
					new SqlParameter("@CheckMan", SqlDbType.VarChar,36),
					new SqlParameter("@CheckNoto", SqlDbType.NVarChar,500),
					new SqlParameter("@CheckRusult", SqlDbType.NVarChar,10),
					new SqlParameter("@CheckTime", SqlDbType.DateTime)};
			parameters[0].Value = model.TaskId;
			parameters[1].Value = model.CommId;
			parameters[2].Value = model.PlanId;
			parameters[3].Value = model.TaskNO;
			parameters[4].Value = model.EqId;
			parameters[5].Value = model.SpaceId;
			parameters[6].Value = model.Content;
			parameters[7].Value = model.BeginTime;
			parameters[8].Value = model.EndTime;
			parameters[9].Value = model.WorkDayBegin;
			parameters[10].Value = model.RoleCode;
			parameters[11].Value = model.Statue;
			parameters[12].Value = model.ClosePerson;
			parameters[13].Value = model.CloseTime;
			parameters[14].Value = model.CloseReason;
			parameters[15].Value = model.PollingPerson;
			parameters[16].Value = model.PollingDate;
			parameters[17].Value = model.Remark;
			parameters[18].Value = model.IsDelete;
			parameters[19].Value = model.AddPId;
			parameters[20].Value = model.AddDate;
			parameters[21].Value = model.PerfNum;
			parameters[22].Value = model.WorkTime;
			parameters[23].Value = model.IsEntrust;
			parameters[24].Value = model.EntrustCompany;
			parameters[25].Value = model.CheckRoleCode;
			parameters[26].Value = model.PersonInCharge;
			parameters[27].Value = model.DoTime;
			parameters[28].Value = model.CheckMan;
			parameters[29].Value = model.CheckNoto;
			parameters[30].Value = model.CheckRusult;
			parameters[31].Value = model.CheckTime;

			DbHelperSQL.RunProcedure("Proc_Tb_EQ_WbTask_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(string TaskId)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@TaskId", SqlDbType.VarChar,50)};
			parameters[0].Value = TaskId;

			DbHelperSQL.RunProcedure("Proc_Tb_EQ_WbTask_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public HM.Model.Eq.Tb_EQ_WbTask GetModel(string TaskId)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@TaskId", SqlDbType.VarChar,50)};
			parameters[0].Value = TaskId;

			HM.Model.Eq.Tb_EQ_WbTask model=new HM.Model.Eq.Tb_EQ_WbTask();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_EQ_WbTask_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				model.TaskId=ds.Tables[0].Rows[0]["TaskId"].ToString();
				if(ds.Tables[0].Rows[0]["CommId"].ToString()!="")
				{
					model.CommId=int.Parse(ds.Tables[0].Rows[0]["CommId"].ToString());
				}
				model.PlanId=ds.Tables[0].Rows[0]["PlanId"].ToString();
				model.TaskNO=ds.Tables[0].Rows[0]["TaskNO"].ToString();
				model.EqId=ds.Tables[0].Rows[0]["EqId"].ToString();
				model.SpaceId=ds.Tables[0].Rows[0]["SpaceId"].ToString();
				model.Content=ds.Tables[0].Rows[0]["Content"].ToString();
				if(ds.Tables[0].Rows[0]["BeginTime"].ToString()!="")
				{
					model.BeginTime=DateTime.Parse(ds.Tables[0].Rows[0]["BeginTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["EndTime"].ToString()!="")
				{
					model.EndTime=DateTime.Parse(ds.Tables[0].Rows[0]["EndTime"].ToString());
				}
				model.WorkDayBegin=ds.Tables[0].Rows[0]["WorkDayBegin"].ToString();
				model.RoleCode=ds.Tables[0].Rows[0]["RoleCode"].ToString();
				model.Statue=ds.Tables[0].Rows[0]["Statue"].ToString();
				model.ClosePerson=ds.Tables[0].Rows[0]["ClosePerson"].ToString();
				if(ds.Tables[0].Rows[0]["CloseTime"].ToString()!="")
				{
					model.CloseTime=DateTime.Parse(ds.Tables[0].Rows[0]["CloseTime"].ToString());
				}
				model.CloseReason=ds.Tables[0].Rows[0]["CloseReason"].ToString();
				model.PollingPerson=ds.Tables[0].Rows[0]["PollingPerson"].ToString();
				if(ds.Tables[0].Rows[0]["PollingDate"].ToString()!="")
				{
					model.PollingDate=DateTime.Parse(ds.Tables[0].Rows[0]["PollingDate"].ToString());
				}
				model.Remark=ds.Tables[0].Rows[0]["Remark"].ToString();
				if(ds.Tables[0].Rows[0]["IsDelete"].ToString()!="")
				{
					model.IsDelete=int.Parse(ds.Tables[0].Rows[0]["IsDelete"].ToString());
				}
				model.AddPId=ds.Tables[0].Rows[0]["AddPId"].ToString();
				if(ds.Tables[0].Rows[0]["AddDate"].ToString()!="")
				{
					model.AddDate=DateTime.Parse(ds.Tables[0].Rows[0]["AddDate"].ToString());
				}
				model.PerfNum=ds.Tables[0].Rows[0]["PerfNum"].ToString();
				model.WorkTime=ds.Tables[0].Rows[0]["WorkTime"].ToString();
				if(ds.Tables[0].Rows[0]["IsEntrust"].ToString()!="")
				{
					model.IsEntrust=int.Parse(ds.Tables[0].Rows[0]["IsEntrust"].ToString());
				}
				model.EntrustCompany=ds.Tables[0].Rows[0]["EntrustCompany"].ToString();
				model.CheckRoleCode=ds.Tables[0].Rows[0]["CheckRoleCode"].ToString();
				model.PersonInCharge=ds.Tables[0].Rows[0]["PersonInCharge"].ToString();
				if(ds.Tables[0].Rows[0]["DoTime"].ToString()!="")
				{
					model.DoTime=DateTime.Parse(ds.Tables[0].Rows[0]["DoTime"].ToString());
				}
				model.CheckMan=ds.Tables[0].Rows[0]["CheckMan"].ToString();
				model.CheckNoto=ds.Tables[0].Rows[0]["CheckNoto"].ToString();
				model.CheckRusult=ds.Tables[0].Rows[0]["CheckRusult"].ToString();
				if(ds.Tables[0].Rows[0]["CheckTime"].ToString()!="")
				{
					model.CheckTime=DateTime.Parse(ds.Tables[0].Rows[0]["CheckTime"].ToString());
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
			strSql.Append("select TaskId,CommId,PlanId,TaskNO,EqId,SpaceId,Content,BeginTime,EndTime,WorkDayBegin,RoleCode,Statue,ClosePerson,CloseTime,CloseReason,PollingPerson,PollingDate,Remark,IsDelete,AddPId,AddDate,PerfNum,WorkTime,IsEntrust,EntrustCompany,CheckRoleCode,PersonInCharge,DoTime,CheckMan,CheckNoto,CheckRusult,CheckTime ");
			strSql.Append(" FROM Tb_EQ_WbTask ");
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
			strSql.Append(" TaskId,CommId,PlanId,TaskNO,EqId,SpaceId,Content,BeginTime,EndTime,WorkDayBegin,RoleCode,Statue,ClosePerson,CloseTime,CloseReason,PollingPerson,PollingDate,Remark,IsDelete,AddPId,AddDate,PerfNum,WorkTime,IsEntrust,EntrustCompany,CheckRoleCode,PersonInCharge,DoTime,CheckMan,CheckNoto,CheckRusult,CheckTime ");
			strSql.Append(" FROM Tb_EQ_WbTask ");
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
			parameters[5].Value = "SELECT * FROM Tb_EQ_WbTask WHERE 1=1 " + StrCondition;
			parameters[6].Value = "TaskId";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

