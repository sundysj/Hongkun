using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//�����������
namespace MobileSoft.DAL.HSPR
{
	/// <summary>
	/// ���ݷ�����Dal_Tb_HSPR_CommunityActivities��
	/// </summary>
	public class Dal_Tb_HSPR_CommunityActivities
	{
		public Dal_Tb_HSPR_CommunityActivities()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  ��Ա����

		/// <summary>
		/// �õ����ID
		/// </summary>
		public long GetMaxId()
		{
		return DbHelperSQL.GetMaxID("InfoID", "Tb_HSPR_CommunityActivities"); 
		}

		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(long InfoID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoID", SqlDbType.BigInt)};
			parameters[0].Value = InfoID;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_HSPR_CommunityActivities_Exists",parameters,out rowsAffected);
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
		public long Add(MobileSoft.Model.HSPR.Tb_HSPR_CommunityActivities model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoID", SqlDbType.BigInt,8),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@ActHeading", SqlDbType.NVarChar,50),
					new SqlParameter("@ActPlace", SqlDbType.NVarChar,50),
					new SqlParameter("@Moderator", SqlDbType.NVarChar,20),
					new SqlParameter("@ActDate", SqlDbType.NVarChar,30),
					new SqlParameter("@ActPeople", SqlDbType.NVarChar,200),
					new SqlParameter("@IsAudit", SqlDbType.SmallInt,2),
					new SqlParameter("@ActContent", SqlDbType.NText)};
			parameters[0].Direction = ParameterDirection.Output;
			parameters[1].Value = model.CommID;
			parameters[2].Value = model.ActHeading;
			parameters[3].Value = model.ActPlace;
			parameters[4].Value = model.Moderator;
			parameters[5].Value = model.ActDate;
			parameters[6].Value = model.ActPeople;
			parameters[7].Value = model.IsAudit;
			parameters[8].Value = model.ActContent;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_CommunityActivities_ADD",parameters,out rowsAffected);
			return (long)parameters[0].Value;
		}

		/// <summary>
		///  ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.HSPR.Tb_HSPR_CommunityActivities model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoID", SqlDbType.BigInt,8),
					new SqlParameter("@CommID", SqlDbType.Int,4),
					new SqlParameter("@ActHeading", SqlDbType.NVarChar,50),
					new SqlParameter("@ActPlace", SqlDbType.NVarChar,50),
					new SqlParameter("@Moderator", SqlDbType.NVarChar,20),
					new SqlParameter("@ActDate", SqlDbType.NVarChar,30),
					new SqlParameter("@ActPeople", SqlDbType.NVarChar,200),
					new SqlParameter("@IsAudit", SqlDbType.SmallInt,2),
					new SqlParameter("@ActContent", SqlDbType.NText)};
			parameters[0].Value = model.InfoID;
			parameters[1].Value = model.CommID;
			parameters[2].Value = model.ActHeading;
			parameters[3].Value = model.ActPlace;
			parameters[4].Value = model.Moderator;
			parameters[5].Value = model.ActDate;
			parameters[6].Value = model.ActPeople;
			parameters[7].Value = model.IsAudit;
			parameters[8].Value = model.ActContent;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_CommunityActivities_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(long InfoID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoID", SqlDbType.BigInt)};
			parameters[0].Value = InfoID;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_CommunityActivities_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_CommunityActivities GetModel(long InfoID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@InfoID", SqlDbType.BigInt)};
			parameters[0].Value = InfoID;

			MobileSoft.Model.HSPR.Tb_HSPR_CommunityActivities model=new MobileSoft.Model.HSPR.Tb_HSPR_CommunityActivities();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_HSPR_CommunityActivities_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["InfoID"].ToString()!="")
				{
					model.InfoID=long.Parse(ds.Tables[0].Rows[0]["InfoID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CommID"].ToString()!="")
				{
					model.CommID=int.Parse(ds.Tables[0].Rows[0]["CommID"].ToString());
				}
				model.ActHeading=ds.Tables[0].Rows[0]["ActHeading"].ToString();
				model.ActPlace=ds.Tables[0].Rows[0]["ActPlace"].ToString();
				model.Moderator=ds.Tables[0].Rows[0]["Moderator"].ToString();
				model.ActDate=ds.Tables[0].Rows[0]["ActDate"].ToString();
				model.ActPeople=ds.Tables[0].Rows[0]["ActPeople"].ToString();
				if(ds.Tables[0].Rows[0]["IsAudit"].ToString()!="")
				{
					model.IsAudit=int.Parse(ds.Tables[0].Rows[0]["IsAudit"].ToString());
				}
				model.ActContent=ds.Tables[0].Rows[0]["ActContent"].ToString();
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
			strSql.Append("select InfoID,CommID,ActHeading,ActPlace,Moderator,ActDate,ActPeople,IsAudit,ActContent ");
			strSql.Append(" FROM Tb_HSPR_CommunityActivities ");
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
			strSql.Append(" InfoID,CommID,ActHeading,ActPlace,Moderator,ActDate,ActPeople,IsAudit,ActContent ");
			strSql.Append(" FROM Tb_HSPR_CommunityActivities ");
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
			parameters[5].Value = "SELECT * FROM Tb_HSPR_CommunityActivities WHERE 1=1 " + StrCondition;
			parameters[6].Value = "InfoID";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  ��Ա����
	}
}

