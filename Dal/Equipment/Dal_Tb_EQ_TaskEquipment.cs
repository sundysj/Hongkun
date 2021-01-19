using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace HM.DAL.Eq
{
	/// <summary>
	/// 数据访问类Dal_Tb_EQ_TaskEquipment。
	/// </summary>
	public class Dal_Tb_EQ_TaskEquipment
	{
		public Dal_Tb_EQ_TaskEquipment()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string TaskEqId)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@TaskEqId", SqlDbType.VarChar,50)};
			parameters[0].Value = TaskEqId;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_EQ_TaskEquipment_Exists",parameters,out rowsAffected);
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
		public void Add(HM.Model.Eq.Tb_EQ_TaskEquipment model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@TaskEqId", SqlDbType.VarChar,36),
					new SqlParameter("@TaskId", SqlDbType.VarChar,36),
					new SqlParameter("@EquiId", SqlDbType.VarChar,36),
					new SqlParameter("@PollingNote", SqlDbType.NVarChar,500),
					new SqlParameter("@PollingResult", SqlDbType.NVarChar,4),
					new SqlParameter("@IsMend", SqlDbType.NVarChar,4),
					new SqlParameter("@BSBH", SqlDbType.NVarChar,50),
					new SqlParameter("@IsHandle", SqlDbType.NVarChar,4),
					new SqlParameter("@HandlePId", SqlDbType.VarChar,36),
					new SqlParameter("@IsDelete", SqlDbType.Int,4),
					new SqlParameter("@AddDate", SqlDbType.DateTime)};
			parameters[0].Value = model.TaskEqId;
			parameters[1].Value = model.TaskId;
			parameters[2].Value = model.EquiId;
			parameters[3].Value = model.PollingNote;
			parameters[4].Value = model.PollingResult;
			parameters[5].Value = model.IsMend;
			parameters[6].Value = model.BSBH;
			parameters[7].Value = model.IsHandle;
			parameters[8].Value = model.HandlePId;
			parameters[9].Value = model.IsDelete;
			parameters[10].Value = model.AddDate;

			DbHelperSQL.RunProcedure("Proc_Tb_EQ_TaskEquipment_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(HM.Model.Eq.Tb_EQ_TaskEquipment model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@TaskEqId", SqlDbType.VarChar,36),
					new SqlParameter("@TaskId", SqlDbType.VarChar,36),
					new SqlParameter("@EquiId", SqlDbType.VarChar,36),
					new SqlParameter("@PollingNote", SqlDbType.NVarChar,500),
					new SqlParameter("@PollingResult", SqlDbType.NVarChar,4),
					new SqlParameter("@IsMend", SqlDbType.NVarChar,4),
					new SqlParameter("@BSBH", SqlDbType.NVarChar,50),
					new SqlParameter("@IsHandle", SqlDbType.NVarChar,4),
					new SqlParameter("@HandlePId", SqlDbType.VarChar,36),
					new SqlParameter("@IsDelete", SqlDbType.Int,4),
					new SqlParameter("@AddDate", SqlDbType.DateTime)};
			parameters[0].Value = model.TaskEqId;
			parameters[1].Value = model.TaskId;
			parameters[2].Value = model.EquiId;
			parameters[3].Value = model.PollingNote;
			parameters[4].Value = model.PollingResult;
			parameters[5].Value = model.IsMend;
			parameters[6].Value = model.BSBH;
			parameters[7].Value = model.IsHandle;
			parameters[8].Value = model.HandlePId;
			parameters[9].Value = model.IsDelete;
			parameters[10].Value = model.AddDate;

			DbHelperSQL.RunProcedure("Proc_Tb_EQ_TaskEquipment_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(string TaskEqId)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@TaskEqId", SqlDbType.VarChar,50)};
			parameters[0].Value = TaskEqId;

			DbHelperSQL.RunProcedure("Proc_Tb_EQ_TaskEquipment_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public HM.Model.Eq.Tb_EQ_TaskEquipment GetModel(string TaskEqId)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@TaskEqId", SqlDbType.VarChar,50)};
			parameters[0].Value = TaskEqId;

			HM.Model.Eq.Tb_EQ_TaskEquipment model=new HM.Model.Eq.Tb_EQ_TaskEquipment();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_EQ_TaskEquipment_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				model.TaskEqId=ds.Tables[0].Rows[0]["TaskEqId"].ToString();
				model.TaskId=ds.Tables[0].Rows[0]["TaskId"].ToString();
				model.EquiId=ds.Tables[0].Rows[0]["EquiId"].ToString();
				model.PollingNote=ds.Tables[0].Rows[0]["PollingNote"].ToString();
				model.PollingResult=ds.Tables[0].Rows[0]["PollingResult"].ToString();
				model.IsMend=ds.Tables[0].Rows[0]["IsMend"].ToString();
				model.BSBH=ds.Tables[0].Rows[0]["BSBH"].ToString();
				model.IsHandle=ds.Tables[0].Rows[0]["IsHandle"].ToString();
				model.HandlePId=ds.Tables[0].Rows[0]["HandlePId"].ToString();
				if(ds.Tables[0].Rows[0]["IsDelete"].ToString()!="")
				{
					model.IsDelete=int.Parse(ds.Tables[0].Rows[0]["IsDelete"].ToString());
				}
				if(ds.Tables[0].Rows[0]["AddDate"].ToString()!="")
				{
					model.AddDate=DateTime.Parse(ds.Tables[0].Rows[0]["AddDate"].ToString());
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
			strSql.Append("select TaskEqId,TaskId,EquiId,PollingNote,PollingResult,IsMend,BSBH,IsHandle,HandlePId,IsDelete,AddDate ");
			strSql.Append(" FROM Tb_EQ_TaskEquipment ");
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
			strSql.Append(" TaskEqId,TaskId,EquiId,PollingNote,PollingResult,IsMend,BSBH,IsHandle,HandlePId,IsDelete,AddDate ");
			strSql.Append(" FROM Tb_EQ_TaskEquipment ");
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
			parameters[5].Value = "SELECT * FROM Tb_EQ_TaskEquipment WHERE 1=1 " + StrCondition;
			parameters[6].Value = "TaskEqId";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

