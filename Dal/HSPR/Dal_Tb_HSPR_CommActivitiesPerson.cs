using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//�����������
namespace MobileSoft.DAL.HSPR
{
	/// <summary>
	/// ���ݷ�����Dal_Tb_HSPR_CommActivitiesPerson��
	/// </summary>
	public class Dal_Tb_HSPR_CommActivitiesPerson
	{
		public Dal_Tb_HSPR_CommActivitiesPerson()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  ��Ա����

		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(string ActivitiesPersonID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ActivitiesPersonID", SqlDbType.NVarChar,50)};
			parameters[0].Value = ActivitiesPersonID;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_HSPR_CommActivitiesPerson_Exists",parameters,out rowsAffected);
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
		public void Add(MobileSoft.Model.HSPR.Tb_HSPR_CommActivitiesPerson model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ActivitiesPersonID", SqlDbType.NVarChar,50),
					new SqlParameter("@ActivitiesID", SqlDbType.NVarChar,50),
					new SqlParameter("@CustName", SqlDbType.NVarChar,50),
					new SqlParameter("@CustID", SqlDbType.BigInt,8),
					new SqlParameter("@RoomName", SqlDbType.NVarChar,50),
					new SqlParameter("@RoomID", SqlDbType.BigInt,8),
					new SqlParameter("@LinkPhone", SqlDbType.NVarChar,50),
					new SqlParameter("@PersonCount", SqlDbType.Int,4),
					new SqlParameter("@CommID", SqlDbType.BigInt,8),
					new SqlParameter("@IsDelete", SqlDbType.Int,4)};
			parameters[0].Value = model.ActivitiesPersonID;
			parameters[1].Value = model.ActivitiesID;
			parameters[2].Value = model.CustName;
			parameters[3].Value = model.CustID;
			parameters[4].Value = model.RoomName;
			parameters[5].Value = model.RoomID;
			parameters[6].Value = model.LinkPhone;
			parameters[7].Value = model.PersonCount;
			parameters[8].Value = model.CommID;
			parameters[9].Value = model.IsDelete;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_CommActivitiesPerson_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.HSPR.Tb_HSPR_CommActivitiesPerson model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ActivitiesPersonID", SqlDbType.NVarChar,50),
					new SqlParameter("@ActivitiesID", SqlDbType.NVarChar,50),
					new SqlParameter("@CustName", SqlDbType.NVarChar,50),
					new SqlParameter("@CustID", SqlDbType.BigInt,8),
					new SqlParameter("@RoomName", SqlDbType.NVarChar,50),
					new SqlParameter("@RoomID", SqlDbType.BigInt,8),
					new SqlParameter("@LinkPhone", SqlDbType.NVarChar,50),
					new SqlParameter("@PersonCount", SqlDbType.Int,4),
					new SqlParameter("@CommID", SqlDbType.BigInt,8),
					new SqlParameter("@IsDelete", SqlDbType.Int,4)};
			parameters[0].Value = model.ActivitiesPersonID;
			parameters[1].Value = model.ActivitiesID;
			parameters[2].Value = model.CustName;
			parameters[3].Value = model.CustID;
			parameters[4].Value = model.RoomName;
			parameters[5].Value = model.RoomID;
			parameters[6].Value = model.LinkPhone;
			parameters[7].Value = model.PersonCount;
			parameters[8].Value = model.CommID;
			parameters[9].Value = model.IsDelete;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_CommActivitiesPerson_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(string ActivitiesPersonID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ActivitiesPersonID", SqlDbType.NVarChar,50)};
			parameters[0].Value = ActivitiesPersonID;

			DbHelperSQL.RunProcedure("Proc_Tb_HSPR_CommActivitiesPerson_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.HSPR.Tb_HSPR_CommActivitiesPerson GetModel(string ActivitiesPersonID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@ActivitiesPersonID", SqlDbType.NVarChar,50)};
			parameters[0].Value = ActivitiesPersonID;

			MobileSoft.Model.HSPR.Tb_HSPR_CommActivitiesPerson model=new MobileSoft.Model.HSPR.Tb_HSPR_CommActivitiesPerson();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_HSPR_CommActivitiesPerson_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				model.ActivitiesPersonID=ds.Tables[0].Rows[0]["ActivitiesPersonID"].ToString();
				model.ActivitiesID=ds.Tables[0].Rows[0]["ActivitiesID"].ToString();
				model.CustName=ds.Tables[0].Rows[0]["CustName"].ToString();
				if(ds.Tables[0].Rows[0]["CustID"].ToString()!="")
				{
					model.CustID=long.Parse(ds.Tables[0].Rows[0]["CustID"].ToString());
				}
				model.RoomName=ds.Tables[0].Rows[0]["RoomName"].ToString();
				if(ds.Tables[0].Rows[0]["RoomID"].ToString()!="")
				{
					model.RoomID=long.Parse(ds.Tables[0].Rows[0]["RoomID"].ToString());
				}
				model.LinkPhone=ds.Tables[0].Rows[0]["LinkPhone"].ToString();
				if(ds.Tables[0].Rows[0]["PersonCount"].ToString()!="")
				{
					model.PersonCount=int.Parse(ds.Tables[0].Rows[0]["PersonCount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CommID"].ToString()!="")
				{
					model.CommID=long.Parse(ds.Tables[0].Rows[0]["CommID"].ToString());
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
		/// ��������б�
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * ");
			strSql.Append(" FROM View_Tb_HSPR_CommActivitiesPerson_Filter ");
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
			strSql.Append(" ActivitiesPersonID,ActivitiesID,CustName,CustID,RoomName,RoomID,LinkPhone,PersonCount,CommID,IsDelete ");
			strSql.Append(" FROM Tb_HSPR_CommActivitiesPerson ");
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
			parameters[5].Value = "SELECT * FROM Tb_HSPR_CommActivitiesPerson WHERE 1=1 " + StrCondition;
			parameters[6].Value = "ActivitiesPersonID";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  ��Ա����
	}
}

