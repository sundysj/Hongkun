using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//�����������
namespace MobileSoft.DAL.OAPublicWork
{
	/// <summary>
	/// ���ݷ�����Dal_Tb_OAPublicWork_FixedAssetsLossTable��
	/// </summary>
	public class Dal_Tb_OAPublicWork_FixedAssetsLossTable
	{
		public Dal_Tb_OAPublicWork_FixedAssetsLossTable()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  ��Ա����

		/// <summary>
		/// �õ����ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("InfoID", "Tb_OAPublicWork_FixedAssetsLossTable"); 
		}

		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(int InfoID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoID", SqlDbType.Int,4)};
			parameters[0].Value = InfoID;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_FixedAssetsLossTable_Exists",parameters,out rowsAffected);
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
		public int Add(MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_FixedAssetsLossTable model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoID", SqlDbType.Int,4),
					new SqlParameter("@Tb_WorkFlow_FlowSort_InfoId", SqlDbType.Int,4),
					new SqlParameter("@UserCode", SqlDbType.NVarChar,20),
					new SqlParameter("@LossUserName", SqlDbType.NVarChar,100),
					new SqlParameter("@LossAssets", SqlDbType.NVarChar,100),
					new SqlParameter("@LossDepart", SqlDbType.NVarChar,100),
					new SqlParameter("@LossDate", SqlDbType.DateTime),
					new SqlParameter("@LossMark", SqlDbType.Text),
					new SqlParameter("@WorkStartDate", SqlDbType.DateTime),
					new SqlParameter("@DocumentUrl", SqlDbType.VarChar,2000)};
			parameters[0].Direction = ParameterDirection.Output;
			parameters[1].Value = model.Tb_WorkFlow_FlowSort_InfoId;
			parameters[2].Value = model.UserCode;
			parameters[3].Value = model.LossUserName;
			parameters[4].Value = model.LossAssets;
			parameters[5].Value = model.LossDepart;
			parameters[6].Value = model.LossDate;
			parameters[7].Value = model.LossMark;
			parameters[8].Value = model.WorkStartDate;
			parameters[9].Value = model.DocumentUrl;

			DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_FixedAssetsLossTable_ADD",parameters,out rowsAffected);
			return (int)parameters[0].Value;
		}

		/// <summary>
		///  ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_FixedAssetsLossTable model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoID", SqlDbType.Int,4),
					new SqlParameter("@Tb_WorkFlow_FlowSort_InfoId", SqlDbType.Int,4),
					new SqlParameter("@UserCode", SqlDbType.NVarChar,20),
					new SqlParameter("@LossUserName", SqlDbType.NVarChar,100),
					new SqlParameter("@LossAssets", SqlDbType.NVarChar,100),
					new SqlParameter("@LossDepart", SqlDbType.NVarChar,100),
					new SqlParameter("@LossDate", SqlDbType.DateTime),
					new SqlParameter("@LossMark", SqlDbType.Text),
					new SqlParameter("@WorkStartDate", SqlDbType.DateTime),
					new SqlParameter("@DocumentUrl", SqlDbType.VarChar,2000)};
			parameters[0].Value = model.InfoID;
			parameters[1].Value = model.Tb_WorkFlow_FlowSort_InfoId;
			parameters[2].Value = model.UserCode;
			parameters[3].Value = model.LossUserName;
			parameters[4].Value = model.LossAssets;
			parameters[5].Value = model.LossDepart;
			parameters[6].Value = model.LossDate;
			parameters[7].Value = model.LossMark;
			parameters[8].Value = model.WorkStartDate;
			parameters[9].Value = model.DocumentUrl;

			DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_FixedAssetsLossTable_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(int InfoID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoID", SqlDbType.Int,4)};
			parameters[0].Value = InfoID;

			DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_FixedAssetsLossTable_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_FixedAssetsLossTable GetModel(int InfoID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@InfoID", SqlDbType.Int,4)};
			parameters[0].Value = InfoID;

			MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_FixedAssetsLossTable model=new MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_FixedAssetsLossTable();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_FixedAssetsLossTable_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["InfoID"].ToString()!="")
				{
					model.InfoID=int.Parse(ds.Tables[0].Rows[0]["InfoID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Tb_WorkFlow_FlowSort_InfoId"].ToString()!="")
				{
					model.Tb_WorkFlow_FlowSort_InfoId=int.Parse(ds.Tables[0].Rows[0]["Tb_WorkFlow_FlowSort_InfoId"].ToString());
				}
				model.UserCode=ds.Tables[0].Rows[0]["UserCode"].ToString();
				model.LossUserName=ds.Tables[0].Rows[0]["LossUserName"].ToString();
				model.LossAssets=ds.Tables[0].Rows[0]["LossAssets"].ToString();
				model.LossDepart=ds.Tables[0].Rows[0]["LossDepart"].ToString();
				if(ds.Tables[0].Rows[0]["LossDate"].ToString()!="")
				{
					model.LossDate=DateTime.Parse(ds.Tables[0].Rows[0]["LossDate"].ToString());
				}
				model.LossMark=ds.Tables[0].Rows[0]["LossMark"].ToString();
				if(ds.Tables[0].Rows[0]["WorkStartDate"].ToString()!="")
				{
					model.WorkStartDate=DateTime.Parse(ds.Tables[0].Rows[0]["WorkStartDate"].ToString());
				}
				model.DocumentUrl=ds.Tables[0].Rows[0]["DocumentUrl"].ToString();
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
			strSql.Append("select InfoID,Tb_WorkFlow_FlowSort_InfoId,UserCode,LossUserName,LossAssets,LossDepart,LossDate,LossMark,WorkStartDate,DocumentUrl ");
			strSql.Append(" FROM Tb_OAPublicWork_FixedAssetsLossTable ");
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
			strSql.Append(" InfoID,Tb_WorkFlow_FlowSort_InfoId,UserCode,LossUserName,LossAssets,LossDepart,LossDate,LossMark,WorkStartDate,DocumentUrl ");
			strSql.Append(" FROM Tb_OAPublicWork_FixedAssetsLossTable ");
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
			parameters[5].Value = "SELECT * FROM Tb_OAPublicWork_FixedAssetsLossTable WHERE 1=1 " + StrCondition;
			parameters[6].Value = "InfoID";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  ��Ա����
	}
}

