using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.SQMSys
{
	/// <summary>
	/// 数据访问类Dal_Tb_Sys_Message。
	/// </summary>
	public class Dal_Tb_Sys_Message
	{
		public Dal_Tb_Sys_Message()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(Guid MessageCode,long CutID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@MessageCode", SqlDbType.UniqueIdentifier),
					new SqlParameter("@CutID", SqlDbType.BigInt)};
			parameters[0].Value = MessageCode;
			parameters[1].Value = CutID;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_Sys_Message_Exists",parameters,out rowsAffected);
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
		public int Add(MobileSoft.Model.SQMSys.Tb_Sys_Message model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@MessageCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@CutID", SqlDbType.BigInt,8),
					new SqlParameter("@UserCode", SqlDbType.NVarChar,20),
					new SqlParameter("@MsgTitle", SqlDbType.NVarChar,4000),
					new SqlParameter("@Content", SqlDbType.NText),
					new SqlParameter("@SendTime", SqlDbType.DateTime),
					new SqlParameter("@MsgType", SqlDbType.SmallInt,2),
					new SqlParameter("@SendMan", SqlDbType.NVarChar,20),
					new SqlParameter("@MsgState", SqlDbType.SmallInt,2),
					new SqlParameter("@IsDeleteSend", SqlDbType.SmallInt,2),
					new SqlParameter("@IsDeleteRead", SqlDbType.SmallInt,2),
					new SqlParameter("@URL", SqlDbType.NText),
					new SqlParameter("@HaveSendUsers", SqlDbType.NText),
					new SqlParameter("@IsRemind", SqlDbType.Int,4)};
			parameters[0].Value = model.MessageCode;
			parameters[1].Direction = ParameterDirection.Output;
			parameters[2].Value = model.UserCode;
			parameters[3].Value = model.MsgTitle;
			parameters[4].Value = model.Content;
			parameters[5].Value = model.SendTime;
			parameters[6].Value = model.MsgType;
			parameters[7].Value = model.SendMan;
			parameters[8].Value = model.MsgState;
			parameters[9].Value = model.IsDeleteSend;
			parameters[10].Value = model.IsDeleteRead;
			parameters[11].Value = model.URL;
			parameters[12].Value = model.HaveSendUsers;
			parameters[13].Value = model.IsRemind;

			DbHelperSQL.RunProcedure("Proc_Tb_Sys_Message_ADD",parameters,out rowsAffected);
			return (int)parameters[1].Value;
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.SQMSys.Tb_Sys_Message model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@MessageCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@CutID", SqlDbType.BigInt,8),
					new SqlParameter("@UserCode", SqlDbType.NVarChar,20),
					new SqlParameter("@MsgTitle", SqlDbType.NVarChar,4000),
					new SqlParameter("@Content", SqlDbType.NText),
					new SqlParameter("@SendTime", SqlDbType.DateTime),
					new SqlParameter("@MsgType", SqlDbType.SmallInt,2),
					new SqlParameter("@SendMan", SqlDbType.NVarChar,20),
					new SqlParameter("@MsgState", SqlDbType.SmallInt,2),
					new SqlParameter("@IsDeleteSend", SqlDbType.SmallInt,2),
					new SqlParameter("@IsDeleteRead", SqlDbType.SmallInt,2),
					new SqlParameter("@URL", SqlDbType.NText),
					new SqlParameter("@HaveSendUsers", SqlDbType.NText),
					new SqlParameter("@IsRemind", SqlDbType.Int,4)};
			parameters[0].Value = model.MessageCode;
			parameters[1].Value = model.CutID;
			parameters[2].Value = model.UserCode;
			parameters[3].Value = model.MsgTitle;
			parameters[4].Value = model.Content;
			parameters[5].Value = model.SendTime;
			parameters[6].Value = model.MsgType;
			parameters[7].Value = model.SendMan;
			parameters[8].Value = model.MsgState;
			parameters[9].Value = model.IsDeleteSend;
			parameters[10].Value = model.IsDeleteRead;
			parameters[11].Value = model.URL;
			parameters[12].Value = model.HaveSendUsers;
			parameters[13].Value = model.IsRemind;

			DbHelperSQL.RunProcedure("Proc_Tb_Sys_Message_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(Guid MessageCode,long CutID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@MessageCode", SqlDbType.UniqueIdentifier),
					new SqlParameter("@CutID", SqlDbType.BigInt)};
			parameters[0].Value = MessageCode;
			parameters[1].Value = CutID;

			DbHelperSQL.RunProcedure("Proc_Tb_Sys_Message_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.SQMSys.Tb_Sys_Message GetModel(Guid MessageCode,long CutID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@MessageCode", SqlDbType.UniqueIdentifier),
					new SqlParameter("@CutID", SqlDbType.BigInt)};
			parameters[0].Value = MessageCode;
			parameters[1].Value = CutID;

			MobileSoft.Model.SQMSys.Tb_Sys_Message model=new MobileSoft.Model.SQMSys.Tb_Sys_Message();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_Sys_Message_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["MessageCode"].ToString()!="")
				{
					model.MessageCode=new Guid(ds.Tables[0].Rows[0]["MessageCode"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CutID"].ToString()!="")
				{
					model.CutID=long.Parse(ds.Tables[0].Rows[0]["CutID"].ToString());
				}
				model.UserCode=ds.Tables[0].Rows[0]["UserCode"].ToString();
				model.MsgTitle=ds.Tables[0].Rows[0]["MsgTitle"].ToString();
				model.Content=ds.Tables[0].Rows[0]["Content"].ToString();
				if(ds.Tables[0].Rows[0]["SendTime"].ToString()!="")
				{
					model.SendTime=DateTime.Parse(ds.Tables[0].Rows[0]["SendTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["MsgType"].ToString()!="")
				{
					model.MsgType=int.Parse(ds.Tables[0].Rows[0]["MsgType"].ToString());
				}
				model.SendMan=ds.Tables[0].Rows[0]["SendMan"].ToString();
				if(ds.Tables[0].Rows[0]["MsgState"].ToString()!="")
				{
					model.MsgState=int.Parse(ds.Tables[0].Rows[0]["MsgState"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsDeleteSend"].ToString()!="")
				{
					model.IsDeleteSend=int.Parse(ds.Tables[0].Rows[0]["IsDeleteSend"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsDeleteRead"].ToString()!="")
				{
					model.IsDeleteRead=int.Parse(ds.Tables[0].Rows[0]["IsDeleteRead"].ToString());
				}
				model.URL=ds.Tables[0].Rows[0]["URL"].ToString();
				model.HaveSendUsers=ds.Tables[0].Rows[0]["HaveSendUsers"].ToString();
				if(ds.Tables[0].Rows[0]["IsRemind"].ToString()!="")
				{
					model.IsRemind=int.Parse(ds.Tables[0].Rows[0]["IsRemind"].ToString());
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
			strSql.Append("select MessageCode,CutID,UserCode,MsgTitle,Content,SendTime,MsgType,SendMan,MsgState,IsDeleteSend,IsDeleteRead,URL,HaveSendUsers,IsRemind ");
			strSql.Append(" FROM Tb_Sys_Message ");
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
			strSql.Append(" MessageCode,CutID,UserCode,MsgTitle,Content,SendTime,MsgType,SendMan,MsgState,IsDeleteSend,IsDeleteRead,URL,HaveSendUsers,IsRemind ");
			strSql.Append(" FROM Tb_Sys_Message ");
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
			parameters[5].Value = "SELECT * FROM Tb_Sys_Message WHERE 1=1 " + StrCondition;
			parameters[6].Value = "MessageCode,CutID";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

