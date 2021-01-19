using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//�����������
namespace MobileSoft.DAL.SQMSys
{
	/// <summary>
	/// ���ݷ�����Dal_Tb_Sys_Log��
	/// </summary>
	public class Dal_Tb_Sys_Log
	{
		public Dal_Tb_Sys_Log()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  ��Ա����

		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(long LogCode)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@LogCode", SqlDbType.BigInt)};
			parameters[0].Value = LogCode;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_Sys_Log_Exists",parameters,out rowsAffected);
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
		///  ����һ������
		/// </summary>
		public int Add(MobileSoft.Model.SQMSys.Tb_Sys_Log model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@LogCode", SqlDbType.BigInt,8),
					new SqlParameter("@StreetCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@ManagerCode", SqlDbType.NVarChar,20),
					new SqlParameter("@LocationIP", SqlDbType.NVarChar,50),
					new SqlParameter("@LogTime", SqlDbType.DateTime),
					new SqlParameter("@PNodeName", SqlDbType.NVarChar,50),
					new SqlParameter("@OperateName", SqlDbType.NVarChar,50),
					new SqlParameter("@OperateURL", SqlDbType.NText),
					new SqlParameter("@Memo", SqlDbType.NText)};
			parameters[0].Direction = ParameterDirection.Output;
			parameters[1].Value = model.StreetCode;
			parameters[2].Value = model.ManagerCode;
			parameters[3].Value = model.LocationIP;
			parameters[4].Value = model.LogTime;
			parameters[5].Value = model.PNodeName;
			parameters[6].Value = model.OperateName;
			parameters[7].Value = model.OperateURL;
			parameters[8].Value = model.Memo;

			DbHelperSQL.RunProcedure("Proc_Tb_Sys_Log_ADD",parameters,out rowsAffected);
			return (int)parameters[0].Value;
		}

		/// <summary>
		///  ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.SQMSys.Tb_Sys_Log model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@LogCode", SqlDbType.BigInt,8),
					new SqlParameter("@StreetCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@ManagerCode", SqlDbType.NVarChar,20),
					new SqlParameter("@LocationIP", SqlDbType.NVarChar,50),
					new SqlParameter("@LogTime", SqlDbType.DateTime),
					new SqlParameter("@PNodeName", SqlDbType.NVarChar,50),
					new SqlParameter("@OperateName", SqlDbType.NVarChar,50),
					new SqlParameter("@OperateURL", SqlDbType.NText),
					new SqlParameter("@Memo", SqlDbType.NText)};
			parameters[0].Value = model.LogCode;
			parameters[1].Value = model.StreetCode;
			parameters[2].Value = model.ManagerCode;
			parameters[3].Value = model.LocationIP;
			parameters[4].Value = model.LogTime;
			parameters[5].Value = model.PNodeName;
			parameters[6].Value = model.OperateName;
			parameters[7].Value = model.OperateURL;
			parameters[8].Value = model.Memo;

			DbHelperSQL.RunProcedure("Proc_Tb_Sys_Log_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(long LogCode)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@LogCode", SqlDbType.BigInt)};
			parameters[0].Value = LogCode;

			DbHelperSQL.RunProcedure("Proc_Tb_Sys_Log_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.SQMSys.Tb_Sys_Log GetModel(long LogCode)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@LogCode", SqlDbType.BigInt)};
			parameters[0].Value = LogCode;

			MobileSoft.Model.SQMSys.Tb_Sys_Log model=new MobileSoft.Model.SQMSys.Tb_Sys_Log();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_Sys_Log_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["LogCode"].ToString()!="")
				{
					model.LogCode=long.Parse(ds.Tables[0].Rows[0]["LogCode"].ToString());
				}
				if(ds.Tables[0].Rows[0]["StreetCode"].ToString()!="")
				{
					model.StreetCode=new Guid(ds.Tables[0].Rows[0]["StreetCode"].ToString());
				}
				model.ManagerCode=ds.Tables[0].Rows[0]["ManagerCode"].ToString();
				model.LocationIP=ds.Tables[0].Rows[0]["LocationIP"].ToString();
				if(ds.Tables[0].Rows[0]["LogTime"].ToString()!="")
				{
					model.LogTime=DateTime.Parse(ds.Tables[0].Rows[0]["LogTime"].ToString());
				}
				model.PNodeName=ds.Tables[0].Rows[0]["PNodeName"].ToString();
				model.OperateName=ds.Tables[0].Rows[0]["OperateName"].ToString();
				model.OperateURL=ds.Tables[0].Rows[0]["OperateURL"].ToString();
				model.Memo=ds.Tables[0].Rows[0]["Memo"].ToString();
				return model;
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// ��������б�
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select LogCode,StreetCode,ManagerCode,LocationIP,LogTime,PNodeName,OperateName,OperateURL,Memo ");
			strSql.Append(" FROM Tb_Sys_Log ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// ���ǰ��������
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string fieldOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" LogCode,StreetCode,ManagerCode,LocationIP,LogTime,PNodeName,OperateName,OperateURL,Memo ");
			strSql.Append(" FROM Tb_Sys_Log ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + fieldOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		
		/// <summary>
		/// ��ҳ��ȡ�����б�
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
			parameters[5].Value = "SELECT * FROM Tb_Sys_Log WHERE 1=1 " + StrCondition;
			parameters[6].Value = "LogCode";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  ��Ա����
	}
}

