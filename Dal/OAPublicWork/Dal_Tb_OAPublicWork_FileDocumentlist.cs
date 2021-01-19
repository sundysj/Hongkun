using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.OAPublicWork
{
	/// <summary>
	/// 数据访问类Dal_Tb_OAPublicWork_FileDocumentlist。
	/// </summary>
	public class Dal_Tb_OAPublicWork_FileDocumentlist
	{
		public Dal_Tb_OAPublicWork_FileDocumentlist()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("InfoId", "Tb_OAPublicWork_FileDocumentlist"); 
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

			int result= DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_FileDocumentlist_Exists",parameters,out rowsAffected);
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
		public int Add(MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_FileDocumentlist model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.Int,4),
					new SqlParameter("@WorkFlowTypeName", SqlDbType.NVarChar,100),
					new SqlParameter("@StartUserName", SqlDbType.NVarChar,50),
					new SqlParameter("@FName", SqlDbType.NVarChar,1000),
					new SqlParameter("@FileCode", SqlDbType.NVarChar,100),
					new SqlParameter("@SendCompany", SqlDbType.NVarChar,100),
					new SqlParameter("@DraftUserName", SqlDbType.NVarChar,50),
					new SqlParameter("@DraftDate", SqlDbType.DateTime),
					new SqlParameter("@ThemeWord", SqlDbType.NVarChar,50),
					new SqlParameter("@ThemeDate", SqlDbType.DateTime),
					new SqlParameter("@FileSecretary", SqlDbType.NVarChar,50),
					new SqlParameter("@Urgency", SqlDbType.NVarChar,50),
					new SqlParameter("@ComeFileType", SqlDbType.NVarChar,50),
					new SqlParameter("@TwoFileType", SqlDbType.NVarChar,50),
					new SqlParameter("@FileTypeName", SqlDbType.NVarChar,50),
					new SqlParameter("@FilePageSize", SqlDbType.Int,4),
					new SqlParameter("@ResponsibilityUserName", SqlDbType.NVarChar,50),
					new SqlParameter("@RetentionDate", SqlDbType.NVarChar,20),
					new SqlParameter("@ReceCompany", SqlDbType.NVarChar,100),
					new SqlParameter("@FileRemark", SqlDbType.NVarChar,3999),
					new SqlParameter("@FileComRemark", SqlDbType.NVarChar,3999),
					new SqlParameter("@FileCheckInfo", SqlDbType.NVarChar,3999),
					new SqlParameter("@FileAddress", SqlDbType.VarChar,3999),
					new SqlParameter("@UserCode", SqlDbType.NVarChar,50),
					new SqlParameter("@IsRead", SqlDbType.Int,4),
					new SqlParameter("@IsDelete", SqlDbType.Int,4)};
			parameters[0].Direction = ParameterDirection.Output;
			parameters[1].Value = model.WorkFlowTypeName;
			parameters[2].Value = model.StartUserName;
			parameters[3].Value = model.FName;
			parameters[4].Value = model.FileCode;
			parameters[5].Value = model.SendCompany;
			parameters[6].Value = model.DraftUserName;
			parameters[7].Value = model.DraftDate;
			parameters[8].Value = model.ThemeWord;
			parameters[9].Value = model.ThemeDate;
			parameters[10].Value = model.FileSecretary;
			parameters[11].Value = model.Urgency;
			parameters[12].Value = model.ComeFileType;
			parameters[13].Value = model.TwoFileType;
			parameters[14].Value = model.FileTypeName;
			parameters[15].Value = model.FilePageSize;
			parameters[16].Value = model.ResponsibilityUserName;
			parameters[17].Value = model.RetentionDate;
			parameters[18].Value = model.ReceCompany;
			parameters[19].Value = model.FileRemark;
			parameters[20].Value = model.FileComRemark;
			parameters[21].Value = model.FileCheckInfo;
			parameters[22].Value = model.FileAddress;
			parameters[23].Value = model.UserCode;
			parameters[24].Value = model.IsRead;
			parameters[25].Value = model.IsDelete;

			DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_FileDocumentlist_ADD",parameters,out rowsAffected);
			return (int)parameters[0].Value;
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_FileDocumentlist model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.Int,4),
					new SqlParameter("@WorkFlowTypeName", SqlDbType.NVarChar,100),
					new SqlParameter("@StartUserName", SqlDbType.NVarChar,50),
					new SqlParameter("@FName", SqlDbType.NVarChar,1000),
					new SqlParameter("@FileCode", SqlDbType.NVarChar,100),
					new SqlParameter("@SendCompany", SqlDbType.NVarChar,100),
					new SqlParameter("@DraftUserName", SqlDbType.NVarChar,50),
					new SqlParameter("@DraftDate", SqlDbType.DateTime),
					new SqlParameter("@ThemeWord", SqlDbType.NVarChar,50),
					new SqlParameter("@ThemeDate", SqlDbType.DateTime),
					new SqlParameter("@FileSecretary", SqlDbType.NVarChar,50),
					new SqlParameter("@Urgency", SqlDbType.NVarChar,50),
					new SqlParameter("@ComeFileType", SqlDbType.NVarChar,50),
					new SqlParameter("@TwoFileType", SqlDbType.NVarChar,50),
					new SqlParameter("@FileTypeName", SqlDbType.NVarChar,50),
					new SqlParameter("@FilePageSize", SqlDbType.Int,4),
					new SqlParameter("@ResponsibilityUserName", SqlDbType.NVarChar,50),
					new SqlParameter("@RetentionDate", SqlDbType.NVarChar,20),
					new SqlParameter("@ReceCompany", SqlDbType.NVarChar,100),
					new SqlParameter("@FileRemark", SqlDbType.NVarChar,3999),
					new SqlParameter("@FileComRemark", SqlDbType.NVarChar,3999),
					new SqlParameter("@FileCheckInfo", SqlDbType.NVarChar,3999),
					new SqlParameter("@FileAddress", SqlDbType.VarChar,3999),
					new SqlParameter("@UserCode", SqlDbType.NVarChar,50),
					new SqlParameter("@IsRead", SqlDbType.Int,4),
					new SqlParameter("@IsDelete", SqlDbType.Int,4)};
			parameters[0].Value = model.InfoId;
			parameters[1].Value = model.WorkFlowTypeName;
			parameters[2].Value = model.StartUserName;
			parameters[3].Value = model.FName;
			parameters[4].Value = model.FileCode;
			parameters[5].Value = model.SendCompany;
			parameters[6].Value = model.DraftUserName;
			parameters[7].Value = model.DraftDate;
			parameters[8].Value = model.ThemeWord;
			parameters[9].Value = model.ThemeDate;
			parameters[10].Value = model.FileSecretary;
			parameters[11].Value = model.Urgency;
			parameters[12].Value = model.ComeFileType;
			parameters[13].Value = model.TwoFileType;
			parameters[14].Value = model.FileTypeName;
			parameters[15].Value = model.FilePageSize;
			parameters[16].Value = model.ResponsibilityUserName;
			parameters[17].Value = model.RetentionDate;
			parameters[18].Value = model.ReceCompany;
			parameters[19].Value = model.FileRemark;
			parameters[20].Value = model.FileComRemark;
			parameters[21].Value = model.FileCheckInfo;
			parameters[22].Value = model.FileAddress;
			parameters[23].Value = model.UserCode;
			parameters[24].Value = model.IsRead;
			parameters[25].Value = model.IsDelete;

			DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_FileDocumentlist_Update",parameters,out rowsAffected);
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

			DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_FileDocumentlist_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_FileDocumentlist GetModel(int InfoId)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.Int,4)};
			parameters[0].Value = InfoId;

			MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_FileDocumentlist model=new MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_FileDocumentlist();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_FileDocumentlist_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["InfoId"].ToString()!="")
				{
					model.InfoId=int.Parse(ds.Tables[0].Rows[0]["InfoId"].ToString());
				}
				model.WorkFlowTypeName=ds.Tables[0].Rows[0]["WorkFlowTypeName"].ToString();
				model.StartUserName=ds.Tables[0].Rows[0]["StartUserName"].ToString();
				model.FName=ds.Tables[0].Rows[0]["FName"].ToString();
				model.FileCode=ds.Tables[0].Rows[0]["FileCode"].ToString();
				model.SendCompany=ds.Tables[0].Rows[0]["SendCompany"].ToString();
				model.DraftUserName=ds.Tables[0].Rows[0]["DraftUserName"].ToString();
				if(ds.Tables[0].Rows[0]["DraftDate"].ToString()!="")
				{
					model.DraftDate=DateTime.Parse(ds.Tables[0].Rows[0]["DraftDate"].ToString());
				}
				model.ThemeWord=ds.Tables[0].Rows[0]["ThemeWord"].ToString();
				if(ds.Tables[0].Rows[0]["ThemeDate"].ToString()!="")
				{
					model.ThemeDate=DateTime.Parse(ds.Tables[0].Rows[0]["ThemeDate"].ToString());
				}
				model.FileSecretary=ds.Tables[0].Rows[0]["FileSecretary"].ToString();
				model.Urgency=ds.Tables[0].Rows[0]["Urgency"].ToString();
				model.ComeFileType=ds.Tables[0].Rows[0]["ComeFileType"].ToString();
				model.TwoFileType=ds.Tables[0].Rows[0]["TwoFileType"].ToString();
				model.FileTypeName=ds.Tables[0].Rows[0]["FileTypeName"].ToString();
				if(ds.Tables[0].Rows[0]["FilePageSize"].ToString()!="")
				{
					model.FilePageSize=int.Parse(ds.Tables[0].Rows[0]["FilePageSize"].ToString());
				}
				model.ResponsibilityUserName=ds.Tables[0].Rows[0]["ResponsibilityUserName"].ToString();
				model.RetentionDate=ds.Tables[0].Rows[0]["RetentionDate"].ToString();
				model.ReceCompany=ds.Tables[0].Rows[0]["ReceCompany"].ToString();
				model.FileRemark=ds.Tables[0].Rows[0]["FileRemark"].ToString();
				model.FileComRemark=ds.Tables[0].Rows[0]["FileComRemark"].ToString();
				model.FileCheckInfo=ds.Tables[0].Rows[0]["FileCheckInfo"].ToString();
				model.FileAddress=ds.Tables[0].Rows[0]["FileAddress"].ToString();
				model.UserCode=ds.Tables[0].Rows[0]["UserCode"].ToString();
				if(ds.Tables[0].Rows[0]["IsRead"].ToString()!="")
				{
					model.IsRead=int.Parse(ds.Tables[0].Rows[0]["IsRead"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsDelete"].ToString()!="")
				{
					model.IsDelete=int.Parse(ds.Tables[0].Rows[0]["IsDelete"].ToString());
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
			strSql.Append("select InfoId,WorkFlowTypeName,StartUserName,FName,FileCode,SendCompany,DraftUserName,DraftDate,ThemeWord,ThemeDate,FileSecretary,Urgency,ComeFileType,TwoFileType,FileTypeName,FilePageSize,ResponsibilityUserName,RetentionDate,ReceCompany,FileRemark,FileComRemark,FileCheckInfo,FileAddress,UserCode,IsRead,IsDelete ");
			strSql.Append(" FROM Tb_OAPublicWork_FileDocumentlist ");
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
			strSql.Append(" InfoId,WorkFlowTypeName,StartUserName,FName,FileCode,SendCompany,DraftUserName,DraftDate,ThemeWord,ThemeDate,FileSecretary,Urgency,ComeFileType,TwoFileType,FileTypeName,FilePageSize,ResponsibilityUserName,RetentionDate,ReceCompany,FileRemark,FileComRemark,FileCheckInfo,FileAddress,UserCode,IsRead,IsDelete ");
			strSql.Append(" FROM Tb_OAPublicWork_FileDocumentlist ");
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
			parameters[5].Value = "SELECT * FROM Tb_OAPublicWork_FileDocumentlist WHERE 1=1 " + StrCondition;
			parameters[6].Value = "InfoId";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

