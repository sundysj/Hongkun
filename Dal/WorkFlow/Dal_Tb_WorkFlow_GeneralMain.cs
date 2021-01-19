using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.WorkFlow
{
	/// <summary>
	/// 数据访问类Dal_Tb_WorkFlow_GeneralMain。
	/// </summary>
	public class Dal_Tb_WorkFlow_GeneralMain
	{
		public Dal_Tb_WorkFlow_GeneralMain()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long CutID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@CutID", SqlDbType.BigInt)};
			parameters[0].Value = CutID;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_WorkFlow_GeneralMain_Exists",parameters,out rowsAffected);
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
		public int Add(MobileSoft.Model.WorkFlow.Tb_WorkFlow_GeneralMain model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@GeneralMainCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@CutID", SqlDbType.BigInt,8),
					new SqlParameter("@Title", SqlDbType.NVarChar,200),
					new SqlParameter("@FlowDegree", SqlDbType.NVarChar,20),
					new SqlParameter("@Content", SqlDbType.NText),
					new SqlParameter("@Memo", SqlDbType.NText),
					new SqlParameter("@DrafMan", SqlDbType.NVarChar,20),
					new SqlParameter("@DrafDate", SqlDbType.SmallDateTime),
					new SqlParameter("@FinishDate", SqlDbType.SmallDateTime),
					new SqlParameter("@WorkState", SqlDbType.SmallInt,2),
					new SqlParameter("@DocumentID", SqlDbType.NVarChar,100),
					new SqlParameter("@msrepl_tran_version", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@TYPE", SqlDbType.NVarChar,10),
					new SqlParameter("@DocumentTypeCode", SqlDbType.NVarChar,100),
					new SqlParameter("@DocType", SqlDbType.SmallInt,2)};
			parameters[0].Value = model.GeneralMainCode;
			parameters[1].Direction = ParameterDirection.Output;
			parameters[2].Value = model.Title;
			parameters[3].Value = model.FlowDegree;
			parameters[4].Value = model.Content;
			parameters[5].Value = model.Memo;
			parameters[6].Value = model.DrafMan;
			parameters[7].Value = model.DrafDate;
			parameters[8].Value = model.FinishDate;
			parameters[9].Value = model.WorkState;
			parameters[10].Value = model.DocumentID;
			parameters[11].Value = model.msrepl_tran_version;
			parameters[12].Value = model.TYPE;
			parameters[13].Value = model.DocumentTypeCode;
			parameters[14].Value = model.DocType;

			DbHelperSQL.RunProcedure("Proc_Tb_WorkFlow_GeneralMain_ADD",parameters,out rowsAffected);
			return (int)parameters[1].Value;
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.WorkFlow.Tb_WorkFlow_GeneralMain model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@GeneralMainCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@CutID", SqlDbType.BigInt,8),
					new SqlParameter("@Title", SqlDbType.NVarChar,200),
					new SqlParameter("@FlowDegree", SqlDbType.NVarChar,20),
					new SqlParameter("@Content", SqlDbType.NText),
					new SqlParameter("@Memo", SqlDbType.NText),
					new SqlParameter("@DrafMan", SqlDbType.NVarChar,20),
					new SqlParameter("@DrafDate", SqlDbType.SmallDateTime),
					new SqlParameter("@FinishDate", SqlDbType.SmallDateTime),
					new SqlParameter("@WorkState", SqlDbType.SmallInt,2),
					new SqlParameter("@DocumentID", SqlDbType.NVarChar,100),
					new SqlParameter("@msrepl_tran_version", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@TYPE", SqlDbType.NVarChar,10),
					new SqlParameter("@DocumentTypeCode", SqlDbType.NVarChar,100),
					new SqlParameter("@DocType", SqlDbType.SmallInt,2)};
			parameters[0].Value = model.GeneralMainCode;
			parameters[1].Value = model.CutID;
			parameters[2].Value = model.Title;
			parameters[3].Value = model.FlowDegree;
			parameters[4].Value = model.Content;
			parameters[5].Value = model.Memo;
			parameters[6].Value = model.DrafMan;
			parameters[7].Value = model.DrafDate;
			parameters[8].Value = model.FinishDate;
			parameters[9].Value = model.WorkState;
			parameters[10].Value = model.DocumentID;
			parameters[11].Value = model.msrepl_tran_version;
			parameters[12].Value = model.TYPE;
			parameters[13].Value = model.DocumentTypeCode;
			parameters[14].Value = model.DocType;

			DbHelperSQL.RunProcedure("Proc_Tb_WorkFlow_GeneralMain_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long CutID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@CutID", SqlDbType.BigInt)};
			parameters[0].Value = CutID;

			DbHelperSQL.RunProcedure("Proc_Tb_WorkFlow_GeneralMain_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.WorkFlow.Tb_WorkFlow_GeneralMain GetModel(long CutID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@CutID", SqlDbType.BigInt)};
			parameters[0].Value = CutID;

			MobileSoft.Model.WorkFlow.Tb_WorkFlow_GeneralMain model=new MobileSoft.Model.WorkFlow.Tb_WorkFlow_GeneralMain();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_WorkFlow_GeneralMain_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["GeneralMainCode"].ToString()!="")
				{
					model.GeneralMainCode=new Guid(ds.Tables[0].Rows[0]["GeneralMainCode"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CutID"].ToString()!="")
				{
					model.CutID=long.Parse(ds.Tables[0].Rows[0]["CutID"].ToString());
				}
				model.Title=ds.Tables[0].Rows[0]["Title"].ToString();
				model.FlowDegree=ds.Tables[0].Rows[0]["FlowDegree"].ToString();
				model.Content=ds.Tables[0].Rows[0]["Content"].ToString();
				model.Memo=ds.Tables[0].Rows[0]["Memo"].ToString();
				model.DrafMan=ds.Tables[0].Rows[0]["DrafMan"].ToString();
				if(ds.Tables[0].Rows[0]["DrafDate"].ToString()!="")
				{
					model.DrafDate=DateTime.Parse(ds.Tables[0].Rows[0]["DrafDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["FinishDate"].ToString()!="")
				{
					model.FinishDate=DateTime.Parse(ds.Tables[0].Rows[0]["FinishDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["WorkState"].ToString()!="")
				{
					model.WorkState=int.Parse(ds.Tables[0].Rows[0]["WorkState"].ToString());
				}
				model.DocumentID=ds.Tables[0].Rows[0]["DocumentID"].ToString();
				if(ds.Tables[0].Rows[0]["msrepl_tran_version"].ToString()!="")
				{
					model.msrepl_tran_version=new Guid(ds.Tables[0].Rows[0]["msrepl_tran_version"].ToString());
				}
				model.TYPE=ds.Tables[0].Rows[0]["TYPE"].ToString();
				model.DocumentTypeCode=ds.Tables[0].Rows[0]["DocumentTypeCode"].ToString();
				if(ds.Tables[0].Rows[0]["DocType"].ToString()!="")
				{
					model.DocType=int.Parse(ds.Tables[0].Rows[0]["DocType"].ToString());
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
			strSql.Append("select GeneralMainCode,CutID,Title,FlowDegree,Content,Memo,DrafMan,DrafDate,FinishDate,WorkState,DocumentID,msrepl_tran_version,TYPE,DocumentTypeCode,DocType ");
			strSql.Append(" FROM Tb_WorkFlow_GeneralMain ");
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
			strSql.Append(" GeneralMainCode,CutID,Title,FlowDegree,Content,Memo,DrafMan,DrafDate,FinishDate,WorkState,DocumentID,msrepl_tran_version,TYPE,DocumentTypeCode,DocType ");
			strSql.Append(" FROM Tb_WorkFlow_GeneralMain ");
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
			parameters[5].Value = "SELECT * FROM Tb_WorkFlow_GeneralMain WHERE 1=1 " + StrCondition;
			parameters[6].Value = "CutID";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

