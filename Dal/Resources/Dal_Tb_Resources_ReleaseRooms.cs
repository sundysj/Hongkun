using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//�����������
namespace MobileSoft.DAL.Resources
{
	/// <summary>
	/// ���ݷ�����Dal_Tb_Resources_ReleaseRooms��
	/// </summary>
	public class Dal_Tb_Resources_ReleaseRooms
	{
		public Dal_Tb_Resources_ReleaseRooms()
		{
			DbHelperSQL.ConnectionString = PubConstant.SQMBSContionString;
		}
		#region  ��Ա����

		/// <summary>
		/// �õ����ID
		/// </summary>
		public long GetMaxId()
		{
		return DbHelperSQL.GetMaxID("ReleaseRoomsID", "Tb_Resources_ReleaseRooms"); 
		}

		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(long ReleaseRoomsID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ReleaseRoomsID", SqlDbType.BigInt)};
			parameters[0].Value = ReleaseRoomsID;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_Resources_ReleaseRooms_Exists",parameters,out rowsAffected);
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
		public long Add(MobileSoft.Model.Resources.Tb_Resources_ReleaseRooms model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ReleaseRoomsID", SqlDbType.BigInt,8),
					new SqlParameter("@ReleaseID", SqlDbType.BigInt,8),
					new SqlParameter("@ReleaseRoomsContent", SqlDbType.NText),
					new SqlParameter("@ReleaseRoomsNeedKnow", SqlDbType.NText)};
			parameters[0].Direction = ParameterDirection.Output;
			parameters[1].Value = model.ReleaseID;
			parameters[2].Value = model.ReleaseRoomsContent;
			parameters[3].Value = model.ReleaseRoomsNeedKnow;

			DbHelperSQL.RunProcedure("Proc_Tb_Resources_ReleaseRooms_ADD",parameters,out rowsAffected);
			return (long)parameters[0].Value;
		}

		/// <summary>
		///  ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.Resources.Tb_Resources_ReleaseRooms model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ReleaseRoomsID", SqlDbType.BigInt,8),
					new SqlParameter("@ReleaseID", SqlDbType.BigInt,8),
					new SqlParameter("@ReleaseRoomsContent", SqlDbType.NText),
					new SqlParameter("@ReleaseRoomsNeedKnow", SqlDbType.NText)};
			parameters[0].Value = model.ReleaseRoomsID;
			parameters[1].Value = model.ReleaseID;
			parameters[2].Value = model.ReleaseRoomsContent;
			parameters[3].Value = model.ReleaseRoomsNeedKnow;

			DbHelperSQL.RunProcedure("Proc_Tb_Resources_ReleaseRooms_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(long ReleaseRoomsID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ReleaseRoomsID", SqlDbType.BigInt)};
			parameters[0].Value = ReleaseRoomsID;

			DbHelperSQL.RunProcedure("Proc_Tb_Resources_ReleaseRooms_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.Resources.Tb_Resources_ReleaseRooms GetModel(long ReleaseRoomsID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@ReleaseRoomsID", SqlDbType.BigInt)};
			parameters[0].Value = ReleaseRoomsID;

			MobileSoft.Model.Resources.Tb_Resources_ReleaseRooms model=new MobileSoft.Model.Resources.Tb_Resources_ReleaseRooms();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_Resources_ReleaseRooms_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ReleaseRoomsID"].ToString()!="")
				{
					model.ReleaseRoomsID=long.Parse(ds.Tables[0].Rows[0]["ReleaseRoomsID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ReleaseID"].ToString()!="")
				{
					model.ReleaseID=long.Parse(ds.Tables[0].Rows[0]["ReleaseID"].ToString());
				}
				model.ReleaseRoomsContent=ds.Tables[0].Rows[0]["ReleaseRoomsContent"].ToString();
				model.ReleaseRoomsNeedKnow=ds.Tables[0].Rows[0]["ReleaseRoomsNeedKnow"].ToString();
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
			strSql.Append("select ReleaseRoomsID,ReleaseID,ReleaseRoomsContent,ReleaseRoomsNeedKnow ");
			strSql.Append(" FROM Tb_Resources_ReleaseRooms ");
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
			strSql.Append(" ReleaseRoomsID,ReleaseID,ReleaseRoomsContent,ReleaseRoomsNeedKnow ");
			strSql.Append(" FROM Tb_Resources_ReleaseRooms ");
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
			parameters[5].Value = "SELECT * FROM Tb_Resources_ReleaseRooms WHERE 1=1 " + StrCondition;
			parameters[6].Value = "ReleaseRoomsID";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  ��Ա����
	}
}

