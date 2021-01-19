using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
using STP= System.Type;

namespace MobileSoft.DAL.WorkFlow
{
	/// <summary>
	/// 数据访问类Dal_Tb_WorkFlow_FlowRecord。
	/// </summary>
	public class Dal_Tb_WorkFlow_FlowRecord
	{
		public Dal_Tb_WorkFlow_FlowRecord()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法


		/// <summary>
		///  增加一条数据
		/// </summary>
		public void Add(MobileSoft.Model.WorkFlow.Tb_WorkFlow_FlowRecord model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@Tb_WorkFlow_FlowNode_InfoId", SqlDbType.Int,4),
					new SqlParameter("@UserCode", SqlDbType.NVarChar,20),
					new SqlParameter("@RecordContent", SqlDbType.NVarChar,1000),
					new SqlParameter("@OprState", SqlDbType.NVarChar,20),
					new SqlParameter("@WorkDate", SqlDbType.DateTime)};
			parameters[0].Value = model.Tb_WorkFlow_FlowNode_InfoId;
			parameters[1].Value = model.UserCode;
			parameters[2].Value = model.RecordContent;
			parameters[3].Value = model.OprState;
			parameters[4].Value = model.WorkDate;

			DbHelperSQL.RunProcedure("Proc_Tb_WorkFlow_FlowRecord_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.WorkFlow.Tb_WorkFlow_FlowRecord model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@Tb_WorkFlow_FlowNode_InfoId", SqlDbType.Int,4),
					new SqlParameter("@UserCode", SqlDbType.NVarChar,20),
					new SqlParameter("@RecordContent", SqlDbType.NVarChar,1000),
					new SqlParameter("@OprState", SqlDbType.NVarChar,20),
					new SqlParameter("@WorkDate", SqlDbType.DateTime)};
			parameters[0].Value = model.Tb_WorkFlow_FlowNode_InfoId;
			parameters[1].Value = model.UserCode;
			parameters[2].Value = model.RecordContent;
			parameters[3].Value = model.OprState;
			parameters[4].Value = model.WorkDate;

			DbHelperSQL.RunProcedure("Proc_Tb_WorkFlow_FlowRecord_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete()
		{
			int rowsAffected;
			SqlParameter[] parameters = {
};

			DbHelperSQL.RunProcedure("Proc_Tb_WorkFlow_FlowRecord_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.WorkFlow.Tb_WorkFlow_FlowRecord GetModel()
		{
			SqlParameter[] parameters = {
};

			MobileSoft.Model.WorkFlow.Tb_WorkFlow_FlowRecord model=new MobileSoft.Model.WorkFlow.Tb_WorkFlow_FlowRecord();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_WorkFlow_FlowRecord_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["Tb_WorkFlow_FlowNode_InfoId"].ToString()!="")
				{
					model.Tb_WorkFlow_FlowNode_InfoId=int.Parse(ds.Tables[0].Rows[0]["Tb_WorkFlow_FlowNode_InfoId"].ToString());
				}
				model.UserCode=ds.Tables[0].Rows[0]["UserCode"].ToString();
				model.RecordContent=ds.Tables[0].Rows[0]["RecordContent"].ToString();
				model.OprState=ds.Tables[0].Rows[0]["OprState"].ToString();
				if(ds.Tables[0].Rows[0]["WorkDate"].ToString()!="")
				{
					model.WorkDate=DateTime.Parse(ds.Tables[0].Rows[0]["WorkDate"].ToString());
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
			strSql.Append("select Tb_WorkFlow_FlowNode_InfoId,UserCode,RecordContent,OprState,WorkDate ");
			strSql.Append(" FROM Tb_WorkFlow_FlowRecord ");
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
			strSql.Append(" Tb_WorkFlow_FlowNode_InfoId,UserCode,RecordContent,OprState,WorkDate ");
			strSql.Append(" FROM Tb_WorkFlow_FlowRecord ");
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
			parameters[5].Value = "SELECT * FROM Tb_WorkFlow_FlowRecord WHERE 1=1 " + StrCondition;
			parameters[6].Value = "";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法

            public void FlowRecordStateInit(int InstanceId, string InstanceTypeCode)
            {
                  SqlParameter[] parameters = {
					new SqlParameter("@InstanceId", SqlDbType.VarChar,50),
                              new SqlParameter("@InstanceTypeCode", SqlDbType.VarChar,50)
                                              };
                  parameters[0].Value = InstanceId;
                  parameters[1].Value = InstanceTypeCode;

                  DbHelperSQL.RunProcedure("Proc_WorkFlow_FlowRecordStateInit", parameters, "RetDataSet");
            }

            public DataTable GetWorkFlowNodeRecord(int InstanceId, string DictionaryCode)
            {
                  SqlParameter[] parameters = {
					new SqlParameter("@InstanceId", SqlDbType.VarChar,50),
                              new SqlParameter("@DictionaryCode", SqlDbType.VarChar,50),
                              new SqlParameter("@Type", SqlDbType.VarChar,50)
                                              };
                  parameters[0].Value = InstanceId;
                  parameters[1].Value = DictionaryCode;
                  parameters[2].Value = "Tb_WorkFlow_FlowRecord";

                  DataTable dTable = DbHelperSQL.RunProcedure("Proc_OAWorkFlow_GetWorkFlowChildTable", parameters, "RetDataSet").Tables[0];

                  foreach (DataRow Row in dTable.Select("RecordContent='发起' OR RecordContent='发起系统维护跳转'"))
                  {
                        string TempStr = Row["RecordUserName"].ToString();
                        Row["RecordUserName"] = Row["RecordUserName"].ToString().Substring(TempStr.IndexOf("：") + 1);
                  }

                  
                  DataColumn NewColumn = new DataColumn();
                  NewColumn.ColumnName = "RandCode";
                  NewColumn.DataType = STP.GetType("System.String");
                  dTable.Columns.Add(NewColumn);

                  return dTable;
            }
	}
}

