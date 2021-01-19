using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.WorkFlow
{
	/// <summary>
	/// 数据访问类Dal_Tb_WorkFlow_GeneralDecompose。
	/// </summary>
	public class Dal_Tb_WorkFlow_GeneralDecompose
	{
		public Dal_Tb_WorkFlow_GeneralDecompose()
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

			int result= DbHelperSQL.RunProcedure("Proc_Tb_WorkFlow_GeneralDecompose_Exists",parameters,out rowsAffected);
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
		public int Add(MobileSoft.Model.WorkFlow.Tb_WorkFlow_GeneralDecompose model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@GeneralDecomposeCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@GeneralMainCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@CutID", SqlDbType.BigInt,8),
					new SqlParameter("@DecomposeCode", SqlDbType.NVarChar,50),
					new SqlParameter("@DisposeMan", SqlDbType.NVarChar,20),
					new SqlParameter("@DisposePope", SqlDbType.SmallInt,2),
					new SqlParameter("@IsFinishPope", SqlDbType.SmallInt,2),
					new SqlParameter("@IsRemind", SqlDbType.SmallInt,2),
					new SqlParameter("@RemindTime", SqlDbType.DateTime),
					new SqlParameter("@RemindMode", SqlDbType.NVarChar,30),
					new SqlParameter("@InstTime", SqlDbType.DateTime),
					new SqlParameter("@SignTime", SqlDbType.DateTime),
					new SqlParameter("@Memo", SqlDbType.NText),
					new SqlParameter("@RemindState", SqlDbType.SmallInt,2),
					new SqlParameter("@DisposeResult", SqlDbType.SmallInt,2),
					new SqlParameter("@DisposeTime", SqlDbType.DateTime),
					new SqlParameter("@DisposeState", SqlDbType.SmallInt,2),
					new SqlParameter("@msrepl_tran_version", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@TYPE", SqlDbType.NVarChar,10),
					new SqlParameter("@DisposeDepCode", SqlDbType.NVarChar,20)};
			parameters[0].Value = model.GeneralDecomposeCode;
			parameters[1].Value = model.GeneralMainCode;
			parameters[2].Direction = ParameterDirection.Output;
			parameters[3].Value = model.DecomposeCode;
			parameters[4].Value = model.DisposeMan;
			parameters[5].Value = model.DisposePope;
			parameters[6].Value = model.IsFinishPope;
			parameters[7].Value = model.IsRemind;
			parameters[8].Value = model.RemindTime;
			parameters[9].Value = model.RemindMode;
			parameters[10].Value = model.InstTime;
			parameters[11].Value = model.SignTime;
			parameters[12].Value = model.Memo;
			parameters[13].Value = model.RemindState;
			parameters[14].Value = model.DisposeResult;
			parameters[15].Value = model.DisposeTime;
			parameters[16].Value = model.DisposeState;
			parameters[17].Value = model.msrepl_tran_version;
			parameters[18].Value = model.TYPE;
			parameters[19].Value = model.DisposeDepCode;

			DbHelperSQL.RunProcedure("Proc_Tb_WorkFlow_GeneralDecompose_ADD",parameters,out rowsAffected);
			return (int)parameters[2].Value;
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.WorkFlow.Tb_WorkFlow_GeneralDecompose model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@GeneralDecomposeCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@GeneralMainCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@CutID", SqlDbType.BigInt,8),
					new SqlParameter("@DecomposeCode", SqlDbType.NVarChar,50),
					new SqlParameter("@DisposeMan", SqlDbType.NVarChar,20),
					new SqlParameter("@DisposePope", SqlDbType.SmallInt,2),
					new SqlParameter("@IsFinishPope", SqlDbType.SmallInt,2),
					new SqlParameter("@IsRemind", SqlDbType.SmallInt,2),
					new SqlParameter("@RemindTime", SqlDbType.DateTime),
					new SqlParameter("@RemindMode", SqlDbType.NVarChar,30),
					new SqlParameter("@InstTime", SqlDbType.DateTime),
					new SqlParameter("@SignTime", SqlDbType.DateTime),
					new SqlParameter("@Memo", SqlDbType.NText),
					new SqlParameter("@RemindState", SqlDbType.SmallInt,2),
					new SqlParameter("@DisposeResult", SqlDbType.SmallInt,2),
					new SqlParameter("@DisposeTime", SqlDbType.DateTime),
					new SqlParameter("@DisposeState", SqlDbType.SmallInt,2),
					new SqlParameter("@msrepl_tran_version", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@TYPE", SqlDbType.NVarChar,10),
					new SqlParameter("@DisposeDepCode", SqlDbType.NVarChar,20)};
			parameters[0].Value = model.GeneralDecomposeCode;
			parameters[1].Value = model.GeneralMainCode;
			parameters[2].Value = model.CutID;
			parameters[3].Value = model.DecomposeCode;
			parameters[4].Value = model.DisposeMan;
			parameters[5].Value = model.DisposePope;
			parameters[6].Value = model.IsFinishPope;
			parameters[7].Value = model.IsRemind;
			parameters[8].Value = model.RemindTime;
			parameters[9].Value = model.RemindMode;
			parameters[10].Value = model.InstTime;
			parameters[11].Value = model.SignTime;
			parameters[12].Value = model.Memo;
			parameters[13].Value = model.RemindState;
			parameters[14].Value = model.DisposeResult;
			parameters[15].Value = model.DisposeTime;
			parameters[16].Value = model.DisposeState;
			parameters[17].Value = model.msrepl_tran_version;
			parameters[18].Value = model.TYPE;
			parameters[19].Value = model.DisposeDepCode;

			DbHelperSQL.RunProcedure("Proc_Tb_WorkFlow_GeneralDecompose_Update",parameters,out rowsAffected);
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

			DbHelperSQL.RunProcedure("Proc_Tb_WorkFlow_GeneralDecompose_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.WorkFlow.Tb_WorkFlow_GeneralDecompose GetModel(long CutID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@CutID", SqlDbType.BigInt)};
			parameters[0].Value = CutID;

			MobileSoft.Model.WorkFlow.Tb_WorkFlow_GeneralDecompose model=new MobileSoft.Model.WorkFlow.Tb_WorkFlow_GeneralDecompose();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_WorkFlow_GeneralDecompose_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["GeneralDecomposeCode"].ToString()!="")
				{
					model.GeneralDecomposeCode=new Guid(ds.Tables[0].Rows[0]["GeneralDecomposeCode"].ToString());
				}
				if(ds.Tables[0].Rows[0]["GeneralMainCode"].ToString()!="")
				{
					model.GeneralMainCode=new Guid(ds.Tables[0].Rows[0]["GeneralMainCode"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CutID"].ToString()!="")
				{
					model.CutID=long.Parse(ds.Tables[0].Rows[0]["CutID"].ToString());
				}
				model.DecomposeCode=ds.Tables[0].Rows[0]["DecomposeCode"].ToString();
				model.DisposeMan=ds.Tables[0].Rows[0]["DisposeMan"].ToString();
				if(ds.Tables[0].Rows[0]["DisposePope"].ToString()!="")
				{
					model.DisposePope=int.Parse(ds.Tables[0].Rows[0]["DisposePope"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsFinishPope"].ToString()!="")
				{
					model.IsFinishPope=int.Parse(ds.Tables[0].Rows[0]["IsFinishPope"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsRemind"].ToString()!="")
				{
					model.IsRemind=int.Parse(ds.Tables[0].Rows[0]["IsRemind"].ToString());
				}
				if(ds.Tables[0].Rows[0]["RemindTime"].ToString()!="")
				{
					model.RemindTime=DateTime.Parse(ds.Tables[0].Rows[0]["RemindTime"].ToString());
				}
				model.RemindMode=ds.Tables[0].Rows[0]["RemindMode"].ToString();
				if(ds.Tables[0].Rows[0]["InstTime"].ToString()!="")
				{
					model.InstTime=DateTime.Parse(ds.Tables[0].Rows[0]["InstTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SignTime"].ToString()!="")
				{
					model.SignTime=DateTime.Parse(ds.Tables[0].Rows[0]["SignTime"].ToString());
				}
				model.Memo=ds.Tables[0].Rows[0]["Memo"].ToString();
				if(ds.Tables[0].Rows[0]["RemindState"].ToString()!="")
				{
					model.RemindState=int.Parse(ds.Tables[0].Rows[0]["RemindState"].ToString());
				}
				if(ds.Tables[0].Rows[0]["DisposeResult"].ToString()!="")
				{
					model.DisposeResult=int.Parse(ds.Tables[0].Rows[0]["DisposeResult"].ToString());
				}
				if(ds.Tables[0].Rows[0]["DisposeTime"].ToString()!="")
				{
					model.DisposeTime=DateTime.Parse(ds.Tables[0].Rows[0]["DisposeTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["DisposeState"].ToString()!="")
				{
					model.DisposeState=int.Parse(ds.Tables[0].Rows[0]["DisposeState"].ToString());
				}
				if(ds.Tables[0].Rows[0]["msrepl_tran_version"].ToString()!="")
				{
					model.msrepl_tran_version=new Guid(ds.Tables[0].Rows[0]["msrepl_tran_version"].ToString());
				}
				model.TYPE=ds.Tables[0].Rows[0]["TYPE"].ToString();
				model.DisposeDepCode=ds.Tables[0].Rows[0]["DisposeDepCode"].ToString();
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
			strSql.Append("select GeneralDecomposeCode,GeneralMainCode,CutID,DecomposeCode,DisposeMan,DisposePope,IsFinishPope,IsRemind,RemindTime,RemindMode,InstTime,SignTime,Memo,RemindState,DisposeResult,DisposeTime,DisposeState,msrepl_tran_version,TYPE,DisposeDepCode ");
			strSql.Append(" FROM Tb_WorkFlow_GeneralDecompose ");
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
			strSql.Append(" GeneralDecomposeCode,GeneralMainCode,CutID,DecomposeCode,DisposeMan,DisposePope,IsFinishPope,IsRemind,RemindTime,RemindMode,InstTime,SignTime,Memo,RemindState,DisposeResult,DisposeTime,DisposeState,msrepl_tran_version,TYPE,DisposeDepCode ");
			strSql.Append(" FROM Tb_WorkFlow_GeneralDecompose ");
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
			parameters[5].Value = "SELECT * FROM Tb_WorkFlow_GeneralDecompose WHERE 1=1 " + StrCondition;
			parameters[6].Value = "CutID";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

