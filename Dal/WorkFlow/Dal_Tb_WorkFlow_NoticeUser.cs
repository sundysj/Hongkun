using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//�����������
namespace MobileSoft.DAL.WorkFlow
{
	/// <summary>
	/// ���ݷ�����Dal_Tb_WorkFlow_NoticeUser��
	/// </summary>
	public class Dal_Tb_WorkFlow_NoticeUser
	{
		public Dal_Tb_WorkFlow_NoticeUser()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  ��Ա����

		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(int InfoId)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.Int,4)};
			parameters[0].Value = InfoId;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_WorkFlow_NoticeUser_Exists",parameters,out rowsAffected);
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
		public int Add(MobileSoft.Model.WorkFlow.Tb_WorkFlow_NoticeUser model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.Int,4),
					new SqlParameter("@Tb_Sys_User_UserCode", SqlDbType.NVarChar,20),
					new SqlParameter("@Tb_Dictionary_NoticeMethod_DictionaryCode", SqlDbType.NVarChar,20),
					new SqlParameter("@NoticeContent", SqlDbType.VarChar,2000),
					new SqlParameter("@InstanceId", SqlDbType.NVarChar,20),
					new SqlParameter("@Tb_Dictionary_InstanceType_DictionaryCode", SqlDbType.NVarChar,20)};
			parameters[0].Direction = ParameterDirection.Output;
			parameters[1].Value = model.Tb_Sys_User_UserCode;
			parameters[2].Value = model.Tb_Dictionary_NoticeMethod_DictionaryCode;
			parameters[3].Value = model.NoticeContent;
			parameters[4].Value = model.InstanceId;
			parameters[5].Value = model.Tb_Dictionary_InstanceType_DictionaryCode;

			DbHelperSQL.RunProcedure("Proc_Tb_WorkFlow_NoticeUser_ADD",parameters,out rowsAffected);
			return (int)parameters[0].Value;
		}

		/// <summary>
		///  ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.WorkFlow.Tb_WorkFlow_NoticeUser model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.Int,4),
					new SqlParameter("@Tb_Sys_User_UserCode", SqlDbType.NVarChar,20),
					new SqlParameter("@Tb_Dictionary_NoticeMethod_DictionaryCode", SqlDbType.NVarChar,20),
					new SqlParameter("@NoticeContent", SqlDbType.VarChar,2000),
					new SqlParameter("@InstanceId", SqlDbType.NVarChar,20),
					new SqlParameter("@Tb_Dictionary_InstanceType_DictionaryCode", SqlDbType.NVarChar,20)};
			parameters[0].Value = model.InfoId;
			parameters[1].Value = model.Tb_Sys_User_UserCode;
			parameters[2].Value = model.Tb_Dictionary_NoticeMethod_DictionaryCode;
			parameters[3].Value = model.NoticeContent;
			parameters[4].Value = model.InstanceId;
			parameters[5].Value = model.Tb_Dictionary_InstanceType_DictionaryCode;

			DbHelperSQL.RunProcedure("Proc_Tb_WorkFlow_NoticeUser_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(int InfoId)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.Int,4)};
			parameters[0].Value = InfoId;

			DbHelperSQL.RunProcedure("Proc_Tb_WorkFlow_NoticeUser_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.WorkFlow.Tb_WorkFlow_NoticeUser GetModel(int InfoId)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.Int,4)};
			parameters[0].Value = InfoId;

			MobileSoft.Model.WorkFlow.Tb_WorkFlow_NoticeUser model=new MobileSoft.Model.WorkFlow.Tb_WorkFlow_NoticeUser();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_WorkFlow_NoticeUser_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["InfoId"].ToString()!="")
				{
					model.InfoId=int.Parse(ds.Tables[0].Rows[0]["InfoId"].ToString());
				}
				model.Tb_Sys_User_UserCode=ds.Tables[0].Rows[0]["Tb_Sys_User_UserCode"].ToString();
				model.Tb_Dictionary_NoticeMethod_DictionaryCode=ds.Tables[0].Rows[0]["Tb_Dictionary_NoticeMethod_DictionaryCode"].ToString();
				model.NoticeContent=ds.Tables[0].Rows[0]["NoticeContent"].ToString();
				model.InstanceId=ds.Tables[0].Rows[0]["InstanceId"].ToString();
				model.Tb_Dictionary_InstanceType_DictionaryCode=ds.Tables[0].Rows[0]["Tb_Dictionary_InstanceType_DictionaryCode"].ToString();
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
			strSql.Append("select InfoId,Tb_Sys_User_UserCode,Tb_Dictionary_NoticeMethod_DictionaryCode,NoticeContent,InstanceId,Tb_Dictionary_InstanceType_DictionaryCode ");
			strSql.Append(" FROM Tb_WorkFlow_NoticeUser ");
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
			strSql.Append(" InfoId,Tb_Sys_User_UserCode,Tb_Dictionary_NoticeMethod_DictionaryCode,NoticeContent,InstanceId,Tb_Dictionary_InstanceType_DictionaryCode ");
			strSql.Append(" FROM Tb_WorkFlow_NoticeUser ");
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
			parameters[5].Value = "SELECT * FROM Tb_WorkFlow_NoticeUser WHERE 1=1 " + StrCondition;
			parameters[6].Value = "InfoId";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  ��Ա����


      }
}

