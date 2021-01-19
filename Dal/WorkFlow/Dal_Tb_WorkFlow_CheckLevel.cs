using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.WorkFlow
{
	/// <summary>
	/// 数据访问类Dal_Tb_WorkFlow_CheckLevel。
	/// </summary>
	public class Dal_Tb_WorkFlow_CheckLevel
	{
		public Dal_Tb_WorkFlow_CheckLevel()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("InfoId", "Tb_WorkFlow_CheckLevel"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int InfoId)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.Int,4)};
			parameters[0].Value = InfoId;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_WorkFlow_CheckLevel_Exists",parameters,out rowsAffected);
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
		public int Add(MobileSoft.Model.WorkFlow.Tb_WorkFlow_CheckLevel model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.Int,4),
					new SqlParameter("@InstanceInfoId", SqlDbType.Int,4),
					new SqlParameter("@WorkFlowInfoId", SqlDbType.Int,4),
					new SqlParameter("@StartUserCode", SqlDbType.NVarChar,50),
					new SqlParameter("@CheckUserCode", SqlDbType.NVarChar,50),
					new SqlParameter("@CheckType", SqlDbType.Int,4),
					new SqlParameter("@OprState", SqlDbType.Int,4),
					new SqlParameter("@RecordDate", SqlDbType.DateTime),
					new SqlParameter("@Sort", SqlDbType.Int,4),
					new SqlParameter("@IsGoToNext", SqlDbType.Int,4)};
			parameters[0].Direction = ParameterDirection.Output;
			parameters[1].Value = model.InstanceInfoId;
			parameters[2].Value = model.WorkFlowInfoId;
			parameters[3].Value = model.StartUserCode;
			parameters[4].Value = model.CheckUserCode;
			parameters[5].Value = model.CheckType;
			parameters[6].Value = model.OprState;
			parameters[7].Value = model.RecordDate;
			parameters[8].Value = model.Sort;
			parameters[9].Value = model.IsGoToNext;

			DbHelperSQL.RunProcedure("Proc_Tb_WorkFlow_CheckLevel_ADD",parameters,out rowsAffected);
			return (int)parameters[0].Value;
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.WorkFlow.Tb_WorkFlow_CheckLevel model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.Int,4),
					new SqlParameter("@InstanceInfoId", SqlDbType.Int,4),
					new SqlParameter("@WorkFlowInfoId", SqlDbType.Int,4),
					new SqlParameter("@StartUserCode", SqlDbType.NVarChar,50),
					new SqlParameter("@CheckUserCode", SqlDbType.NVarChar,50),
					new SqlParameter("@CheckType", SqlDbType.Int,4),
					new SqlParameter("@OprState", SqlDbType.Int,4),
					new SqlParameter("@RecordDate", SqlDbType.DateTime),
					new SqlParameter("@Sort", SqlDbType.Int,4),
					new SqlParameter("@IsGoToNext", SqlDbType.Int,4)};
			parameters[0].Value = model.InfoId;
			parameters[1].Value = model.InstanceInfoId;
			parameters[2].Value = model.WorkFlowInfoId;
			parameters[3].Value = model.StartUserCode;
			parameters[4].Value = model.CheckUserCode;
			parameters[5].Value = model.CheckType;
			parameters[6].Value = model.OprState;
			parameters[7].Value = model.RecordDate;
			parameters[8].Value = model.Sort;
			parameters[9].Value = model.IsGoToNext;

			DbHelperSQL.RunProcedure("Proc_Tb_WorkFlow_CheckLevel_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int InfoId)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.Int,4)};
			parameters[0].Value = InfoId;

			DbHelperSQL.RunProcedure("Proc_Tb_WorkFlow_CheckLevel_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.WorkFlow.Tb_WorkFlow_CheckLevel GetModel(int InfoId)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.Int,4)};
			parameters[0].Value = InfoId;

			MobileSoft.Model.WorkFlow.Tb_WorkFlow_CheckLevel model=new MobileSoft.Model.WorkFlow.Tb_WorkFlow_CheckLevel();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_WorkFlow_CheckLevel_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["InfoId"].ToString()!="")
				{
					model.InfoId=int.Parse(ds.Tables[0].Rows[0]["InfoId"].ToString());
				}
				if(ds.Tables[0].Rows[0]["InstanceInfoId"].ToString()!="")
				{
					model.InstanceInfoId=int.Parse(ds.Tables[0].Rows[0]["InstanceInfoId"].ToString());
				}
				if(ds.Tables[0].Rows[0]["WorkFlowInfoId"].ToString()!="")
				{
					model.WorkFlowInfoId=int.Parse(ds.Tables[0].Rows[0]["WorkFlowInfoId"].ToString());
				}
				model.StartUserCode=ds.Tables[0].Rows[0]["StartUserCode"].ToString();
				model.CheckUserCode=ds.Tables[0].Rows[0]["CheckUserCode"].ToString();
				if(ds.Tables[0].Rows[0]["CheckType"].ToString()!="")
				{
					model.CheckType=int.Parse(ds.Tables[0].Rows[0]["CheckType"].ToString());
				}
				if(ds.Tables[0].Rows[0]["OprState"].ToString()!="")
				{
					model.OprState=int.Parse(ds.Tables[0].Rows[0]["OprState"].ToString());
				}
				if(ds.Tables[0].Rows[0]["RecordDate"].ToString()!="")
				{
					model.RecordDate=DateTime.Parse(ds.Tables[0].Rows[0]["RecordDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Sort"].ToString()!="")
				{
					model.Sort=int.Parse(ds.Tables[0].Rows[0]["Sort"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsGoToNext"].ToString()!="")
				{
					model.IsGoToNext=int.Parse(ds.Tables[0].Rows[0]["IsGoToNext"].ToString());
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
			strSql.Append("select InfoId,InstanceInfoId,WorkFlowInfoId,StartUserCode,CheckUserCode,CheckType,OprState,RecordDate,Sort,IsGoToNext ");
			strSql.Append(" FROM Tb_WorkFlow_CheckLevel ");
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
			strSql.Append(" InfoId,InstanceInfoId,WorkFlowInfoId,StartUserCode,CheckUserCode,CheckType,OprState,RecordDate,Sort,IsGoToNext ");
			strSql.Append(" FROM Tb_WorkFlow_CheckLevel ");
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
			parameters[5].Value = "SELECT * FROM Tb_WorkFlow_CheckLevel WHERE 1=1 " + StrCondition;
			parameters[6].Value = "InfoId";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法

            public DataTable CheckLevelFilter(int InstanceInfoId)
            {
                  SqlParameter[] parameters = {
					new SqlParameter("@InstanceInfoId", SqlDbType.Int)
                                              };
                  parameters[0].Value = InstanceInfoId;

                  DataSet Ds = DbHelperSQL.RunProcedure("Proc_WorkFlow_CheckLevel_Filter", parameters, "RetDataSet");

                  DataTable dTable = new DataTable();

                  if (Ds.Tables.Count > 0)
                  {
                        dTable = Ds.Tables[0];
                  }

                  return dTable;
            }

            public DataTable IsHavePrintCheck(int InstanceId, string UserCode)
            {
                  SqlParameter[] parameters = {
					new SqlParameter("@InstanceId", SqlDbType.Int),
                              new SqlParameter("@UserCode", SqlDbType.VarChar,50)
                                              };
                  parameters[0].Value = InstanceId;
                  parameters[1].Value = UserCode;

                  DataSet Ds = DbHelperSQL.RunProcedure("Proc_WorkFlow_IsHavePrintCheck", parameters, "RetDataSet");

                  DataTable dTable = new DataTable();

                  if (Ds.Tables.Count > 0)
                  {
                        dTable = Ds.Tables[0];
                  }

                  return dTable;
            }

            public void WorkFlowCheckLevelUpdate(int InstanceInfoId, int WorkFlowInfoId, string UserCode)
            {
                  SqlParameter[] parameters = {
					new SqlParameter("@InstanceInfoId", SqlDbType.Int),
                              new SqlParameter("@WorkFlowInfoId", SqlDbType.VarChar,50),
                              new SqlParameter("@UserCode", SqlDbType.VarChar,50)
                                              };
                  parameters[0].Value = InstanceInfoId;
                  parameters[1].Value = WorkFlowInfoId;
                  parameters[2].Value = UserCode;

                  DbHelperSQL.RunProcedure("Proc_WorkFlow_CheckLevel_Update", parameters, "RetDataSet");
            }
	}
}

