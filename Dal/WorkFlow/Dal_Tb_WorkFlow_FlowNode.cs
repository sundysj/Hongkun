using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
using STP = System.Type;
namespace MobileSoft.DAL.WorkFlow
{
	/// <summary>
	/// 数据访问类Dal_Tb_WorkFlow_FlowNode。
	/// </summary>
	public class Dal_Tb_WorkFlow_FlowNode
	{
		public Dal_Tb_WorkFlow_FlowNode()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("InfoId", "Tb_WorkFlow_FlowNode"); 
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

			int result= DbHelperSQL.RunProcedure("Proc_Tb_WorkFlow_FlowNode_Exists",parameters,out rowsAffected);
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
		public int Add(MobileSoft.Model.WorkFlow.Tb_WorkFlow_FlowNode model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.Int,4),
					new SqlParameter("@Tb_WorkFlow_Instance_InfoId", SqlDbType.Int,4),
					new SqlParameter("@FlowNodeName", SqlDbType.VarChar,100),
					new SqlParameter("@FlowSort", SqlDbType.Int,4),
					new SqlParameter("@TimeOutDay", SqlDbType.Int,4),
					new SqlParameter("@TimeOutDays", SqlDbType.Int,4),
					new SqlParameter("@Tb_Dictionary_NodeOprMethod_DictionaryCode", SqlDbType.NVarChar,20),
					new SqlParameter("@Tb_Dictionary_NodeOprType_DictionaryCode", SqlDbType.NVarChar,20),
					new SqlParameter("@JumpFlowSort", SqlDbType.Int,4),
					new SqlParameter("@IsUpdateFlow", SqlDbType.Int,4),
					new SqlParameter("@Tb_Dictionary_OprState_DictionaryCode", SqlDbType.NVarChar,20),
					new SqlParameter("@IsPrint", SqlDbType.Int,4),
					new SqlParameter("@IsStartUser", SqlDbType.Int,4),
					new SqlParameter("@WorkFlowStartDate", SqlDbType.DateTime),
					new SqlParameter("@ReturnNode", SqlDbType.Int,4),
					new SqlParameter("@CheckLevel", SqlDbType.Int,4)};
			parameters[0].Direction = ParameterDirection.Output;
			parameters[1].Value = model.Tb_WorkFlow_Instance_InfoId;
			parameters[2].Value = model.FlowNodeName;
			parameters[3].Value = model.FlowSort;
			parameters[4].Value = model.TimeOutDay;
			parameters[5].Value = model.TimeOutDays;
			parameters[6].Value = model.Tb_Dictionary_NodeOprMethod_DictionaryCode;
			parameters[7].Value = model.Tb_Dictionary_NodeOprType_DictionaryCode;
			parameters[8].Value = model.JumpFlowSort;
			parameters[9].Value = model.IsUpdateFlow;
			parameters[10].Value = model.Tb_Dictionary_OprState_DictionaryCode;
			parameters[11].Value = model.IsPrint;
			parameters[12].Value = model.IsStartUser;
			parameters[13].Value = model.WorkFlowStartDate;
			parameters[14].Value = model.ReturnNode;
			parameters[15].Value = model.CheckLevel;

			DbHelperSQL.RunProcedure("Proc_Tb_WorkFlow_FlowNode_ADD",parameters,out rowsAffected);
			return (int)parameters[0].Value;
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.WorkFlow.Tb_WorkFlow_FlowNode model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.Int,4),
					new SqlParameter("@Tb_WorkFlow_Instance_InfoId", SqlDbType.Int,4),
					new SqlParameter("@FlowNodeName", SqlDbType.VarChar,100),
					new SqlParameter("@FlowSort", SqlDbType.Int,4),
					new SqlParameter("@TimeOutDay", SqlDbType.Int,4),
					new SqlParameter("@TimeOutDays", SqlDbType.Int,4),
					new SqlParameter("@Tb_Dictionary_NodeOprMethod_DictionaryCode", SqlDbType.NVarChar,20),
					new SqlParameter("@Tb_Dictionary_NodeOprType_DictionaryCode", SqlDbType.NVarChar,20),
					new SqlParameter("@JumpFlowSort", SqlDbType.Int,4),
					new SqlParameter("@IsUpdateFlow", SqlDbType.Int,4),
					new SqlParameter("@Tb_Dictionary_OprState_DictionaryCode", SqlDbType.NVarChar,20),
					new SqlParameter("@IsPrint", SqlDbType.Int,4),
					new SqlParameter("@IsStartUser", SqlDbType.Int,4),
					new SqlParameter("@WorkFlowStartDate", SqlDbType.DateTime),
					new SqlParameter("@ReturnNode", SqlDbType.Int,4),
					new SqlParameter("@CheckLevel", SqlDbType.Int,4)};
			parameters[0].Value = model.InfoId;
			parameters[1].Value = model.Tb_WorkFlow_Instance_InfoId;
			parameters[2].Value = model.FlowNodeName;
			parameters[3].Value = model.FlowSort;
			parameters[4].Value = model.TimeOutDay;
			parameters[5].Value = model.TimeOutDays;
			parameters[6].Value = model.Tb_Dictionary_NodeOprMethod_DictionaryCode;
			parameters[7].Value = model.Tb_Dictionary_NodeOprType_DictionaryCode;
			parameters[8].Value = model.JumpFlowSort;
			parameters[9].Value = model.IsUpdateFlow;
			parameters[10].Value = model.Tb_Dictionary_OprState_DictionaryCode;
			parameters[11].Value = model.IsPrint;
			parameters[12].Value = model.IsStartUser;
			parameters[13].Value = model.WorkFlowStartDate;
			parameters[14].Value = model.ReturnNode;
			parameters[15].Value = model.CheckLevel;

			DbHelperSQL.RunProcedure("Proc_Tb_WorkFlow_FlowNode_Update",parameters,out rowsAffected);
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

			DbHelperSQL.RunProcedure("Proc_Tb_WorkFlow_FlowNode_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.WorkFlow.Tb_WorkFlow_FlowNode GetModel(int InfoId)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.Int,4)};
			parameters[0].Value = InfoId;

			MobileSoft.Model.WorkFlow.Tb_WorkFlow_FlowNode model=new MobileSoft.Model.WorkFlow.Tb_WorkFlow_FlowNode();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_WorkFlow_FlowNode_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["InfoId"].ToString()!="")
				{
					model.InfoId=int.Parse(ds.Tables[0].Rows[0]["InfoId"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Tb_WorkFlow_Instance_InfoId"].ToString()!="")
				{
					model.Tb_WorkFlow_Instance_InfoId=int.Parse(ds.Tables[0].Rows[0]["Tb_WorkFlow_Instance_InfoId"].ToString());
				}
				model.FlowNodeName=ds.Tables[0].Rows[0]["FlowNodeName"].ToString();
				if(ds.Tables[0].Rows[0]["FlowSort"].ToString()!="")
				{
					model.FlowSort=int.Parse(ds.Tables[0].Rows[0]["FlowSort"].ToString());
				}
				if(ds.Tables[0].Rows[0]["TimeOutDay"].ToString()!="")
				{
					model.TimeOutDay=int.Parse(ds.Tables[0].Rows[0]["TimeOutDay"].ToString());
				}
				if(ds.Tables[0].Rows[0]["TimeOutDays"].ToString()!="")
				{
					model.TimeOutDays=int.Parse(ds.Tables[0].Rows[0]["TimeOutDays"].ToString());
				}
				model.Tb_Dictionary_NodeOprMethod_DictionaryCode=ds.Tables[0].Rows[0]["Tb_Dictionary_NodeOprMethod_DictionaryCode"].ToString();
				model.Tb_Dictionary_NodeOprType_DictionaryCode=ds.Tables[0].Rows[0]["Tb_Dictionary_NodeOprType_DictionaryCode"].ToString();
				if(ds.Tables[0].Rows[0]["JumpFlowSort"].ToString()!="")
				{
					model.JumpFlowSort=int.Parse(ds.Tables[0].Rows[0]["JumpFlowSort"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsUpdateFlow"].ToString()!="")
				{
					model.IsUpdateFlow=int.Parse(ds.Tables[0].Rows[0]["IsUpdateFlow"].ToString());
				}
				model.Tb_Dictionary_OprState_DictionaryCode=ds.Tables[0].Rows[0]["Tb_Dictionary_OprState_DictionaryCode"].ToString();
				if(ds.Tables[0].Rows[0]["IsPrint"].ToString()!="")
				{
					model.IsPrint=int.Parse(ds.Tables[0].Rows[0]["IsPrint"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsStartUser"].ToString()!="")
				{
					model.IsStartUser=int.Parse(ds.Tables[0].Rows[0]["IsStartUser"].ToString());
				}
				if(ds.Tables[0].Rows[0]["WorkFlowStartDate"].ToString()!="")
				{
					model.WorkFlowStartDate=DateTime.Parse(ds.Tables[0].Rows[0]["WorkFlowStartDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ReturnNode"].ToString()!="")
				{
					model.ReturnNode=int.Parse(ds.Tables[0].Rows[0]["ReturnNode"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CheckLevel"].ToString()!="")
				{
					model.CheckLevel=int.Parse(ds.Tables[0].Rows[0]["CheckLevel"].ToString());
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
			strSql.Append("select InfoId,Tb_WorkFlow_Instance_InfoId,FlowNodeName,FlowSort,TimeOutDay,TimeOutDays,Tb_Dictionary_NodeOprMethod_DictionaryCode,Tb_Dictionary_NodeOprType_DictionaryCode,JumpFlowSort,IsUpdateFlow,Tb_Dictionary_OprState_DictionaryCode,IsPrint,IsStartUser,WorkFlowStartDate,ReturnNode,CheckLevel ");
			strSql.Append(" FROM Tb_WorkFlow_FlowNode ");
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
			strSql.Append(" InfoId,Tb_WorkFlow_Instance_InfoId,FlowNodeName,FlowSort,TimeOutDay,TimeOutDays,Tb_Dictionary_NodeOprMethod_DictionaryCode,Tb_Dictionary_NodeOprType_DictionaryCode,JumpFlowSort,IsUpdateFlow,Tb_Dictionary_OprState_DictionaryCode,IsPrint,IsStartUser,WorkFlowStartDate,ReturnNode,CheckLevel ");
			strSql.Append(" FROM Tb_WorkFlow_FlowNode ");
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
			parameters[5].Value = "SELECT * FROM Tb_WorkFlow_FlowNode WHERE 1=1 " + StrCondition;
			parameters[6].Value = "InfoId";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法

            public DataTable GetWorkFlowNodeList(int InstanceId, string DictionaryCode)
            {
                  SqlParameter[] parameters = {
					new SqlParameter("@InstanceId", SqlDbType.Int),
                              new SqlParameter("@DictionaryCode", SqlDbType.VarChar,50)
                                              };
                  parameters[0].Value = InstanceId;
                  parameters[1].Value = DictionaryCode;

                  DataTable dTable = DbHelperSQL.RunProcedure("Proc_OAWorkFlow_GetWorkFlowNode", parameters, "RetDataSet").Tables[0];

                  DataColumn NewColumn = new DataColumn();
                  NewColumn.ColumnName = "RandCode";
                  NewColumn.DataType = STP.GetType("System.String");
                  dTable.Columns.Add(NewColumn);
                  foreach (DataRow Row in dTable.Rows)
                  {
                        Row["RandCode"] = Row["InfoId"].ToString();
                  }

                  return dTable;
            }
	}
}

