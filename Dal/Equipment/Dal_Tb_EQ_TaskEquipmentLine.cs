using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace HM.DAL.Eq
{
	/// <summary>
	/// 数据访问类Dal_Tb_EQ_TaskEquipmentLine。
	/// </summary>
	public class Dal_Tb_EQ_TaskEquipmentLine
	{
		public Dal_Tb_EQ_TaskEquipmentLine()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string TaskLineId)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@TaskLineId", SqlDbType.VarChar,50)};
			parameters[0].Value = TaskLineId;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_EQ_TaskEquipmentLine_Exists",parameters,out rowsAffected);
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
		public void Add(HM.Model.Eq.Tb_EQ_TaskEquipmentLine model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@TaskLineId", SqlDbType.VarChar,36),
					new SqlParameter("@TaskId", SqlDbType.VarChar,36),
					new SqlParameter("@StanId", SqlDbType.VarChar,36),
					new SqlParameter("@DetailId", SqlDbType.VarChar,36),
					new SqlParameter("@EquiId", SqlDbType.VarChar,36),
					new SqlParameter("@PollingNote", SqlDbType.NVarChar,500),
					new SqlParameter("@ChooseValue", SqlDbType.NVarChar,4),
					new SqlParameter("@NumValue", SqlDbType.Decimal,5),
					new SqlParameter("@TextValue", SqlDbType.NVarChar,500),
					new SqlParameter("@IsDelete", SqlDbType.Int,4),
					new SqlParameter("@AddDate", SqlDbType.DateTime),
					new SqlParameter("@AddPId", SqlDbType.VarChar,36),
					new SqlParameter("@Sort", SqlDbType.Int,4),
					new SqlParameter("@ReferenceValue", SqlDbType.VarChar,50)};
			parameters[0].Value = model.TaskLineId;
			parameters[1].Value = model.TaskId;
			parameters[2].Value = model.StanId;
			parameters[3].Value = model.DetailId;
			parameters[4].Value = model.EquiId;
			parameters[5].Value = model.PollingNote;
			parameters[6].Value = model.ChooseValue;
			parameters[7].Value = model.NumValue;
			parameters[8].Value = model.TextValue;
			parameters[9].Value = model.IsDelete;
			parameters[10].Value = model.AddDate;
			parameters[11].Value = model.AddPId;
			parameters[12].Value = model.Sort;
			parameters[13].Value = model.ReferenceValue;

			DbHelperSQL.RunProcedure("Proc_Tb_EQ_TaskEquipmentLine_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(HM.Model.Eq.Tb_EQ_TaskEquipmentLine model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@TaskLineId", SqlDbType.VarChar,36),
					new SqlParameter("@TaskId", SqlDbType.VarChar,36),
					new SqlParameter("@StanId", SqlDbType.VarChar,36),
					new SqlParameter("@DetailId", SqlDbType.VarChar,36),
					new SqlParameter("@EquiId", SqlDbType.VarChar,36),
					new SqlParameter("@PollingNote", SqlDbType.NVarChar,500),
					new SqlParameter("@ChooseValue", SqlDbType.NVarChar,4),
					new SqlParameter("@NumValue", SqlDbType.Decimal,5),
					new SqlParameter("@TextValue", SqlDbType.NVarChar,500),
					new SqlParameter("@IsDelete", SqlDbType.Int,4),
					new SqlParameter("@AddDate", SqlDbType.DateTime),
					new SqlParameter("@AddPId", SqlDbType.VarChar,36),
					new SqlParameter("@Sort", SqlDbType.Int,4),
					new SqlParameter("@ReferenceValue", SqlDbType.VarChar,50)};
			parameters[0].Value = model.TaskLineId;
			parameters[1].Value = model.TaskId;
			parameters[2].Value = model.StanId;
			parameters[3].Value = model.DetailId;
			parameters[4].Value = model.EquiId;
			parameters[5].Value = model.PollingNote;
			parameters[6].Value = model.ChooseValue;
			parameters[7].Value = model.NumValue;
			parameters[8].Value = model.TextValue;
			parameters[9].Value = model.IsDelete;
			parameters[10].Value = model.AddDate;
			parameters[11].Value = model.AddPId;
			parameters[12].Value = model.Sort;
			parameters[13].Value = model.ReferenceValue;

			DbHelperSQL.RunProcedure("Proc_Tb_EQ_TaskEquipmentLine_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(string TaskLineId)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@TaskLineId", SqlDbType.VarChar,50)};
			parameters[0].Value = TaskLineId;

			DbHelperSQL.RunProcedure("Proc_Tb_EQ_TaskEquipmentLine_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public HM.Model.Eq.Tb_EQ_TaskEquipmentLine GetModel(string TaskLineId)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@TaskLineId", SqlDbType.VarChar,50)};
			parameters[0].Value = TaskLineId;

			HM.Model.Eq.Tb_EQ_TaskEquipmentLine model=new HM.Model.Eq.Tb_EQ_TaskEquipmentLine();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_EQ_TaskEquipmentLine_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				model.TaskLineId=ds.Tables[0].Rows[0]["TaskLineId"].ToString();
				model.TaskId=ds.Tables[0].Rows[0]["TaskId"].ToString();
				model.StanId=ds.Tables[0].Rows[0]["StanId"].ToString();
				model.DetailId=ds.Tables[0].Rows[0]["DetailId"].ToString();
				model.EquiId=ds.Tables[0].Rows[0]["EquiId"].ToString();
				model.PollingNote=ds.Tables[0].Rows[0]["PollingNote"].ToString();
				model.ChooseValue=ds.Tables[0].Rows[0]["ChooseValue"].ToString();
				if(ds.Tables[0].Rows[0]["NumValue"].ToString()!="")
				{
					model.NumValue=decimal.Parse(ds.Tables[0].Rows[0]["NumValue"].ToString());
				}
				model.TextValue=ds.Tables[0].Rows[0]["TextValue"].ToString();
				if(ds.Tables[0].Rows[0]["IsDelete"].ToString()!="")
				{
					model.IsDelete=int.Parse(ds.Tables[0].Rows[0]["IsDelete"].ToString());
				}
				if(ds.Tables[0].Rows[0]["AddDate"].ToString()!="")
				{
					model.AddDate=DateTime.Parse(ds.Tables[0].Rows[0]["AddDate"].ToString());
				}
				model.AddPId=ds.Tables[0].Rows[0]["AddPId"].ToString();
				if(ds.Tables[0].Rows[0]["Sort"].ToString()!="")
				{
					model.Sort=int.Parse(ds.Tables[0].Rows[0]["Sort"].ToString());
				}
				model.ReferenceValue=ds.Tables[0].Rows[0]["ReferenceValue"].ToString();
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
			strSql.Append("select TaskLineId,TaskId,StanId,DetailId,EquiId,PollingNote,ChooseValue,NumValue,TextValue,IsDelete,AddDate,AddPId,Sort,ReferenceValue ");
			strSql.Append(" FROM Tb_EQ_TaskEquipmentLine ");
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
			strSql.Append(" TaskLineId,TaskId,StanId,DetailId,EquiId,PollingNote,ChooseValue,NumValue,TextValue,IsDelete,AddDate,AddPId,Sort,ReferenceValue ");
			strSql.Append(" FROM Tb_EQ_TaskEquipmentLine ");
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
			parameters[5].Value = "SELECT * FROM Tb_EQ_TaskEquipmentLine WHERE 1=1 " + StrCondition;
			parameters[6].Value = "TaskLineId";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

