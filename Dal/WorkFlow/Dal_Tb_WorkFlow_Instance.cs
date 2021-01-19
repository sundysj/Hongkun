using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//�����������
namespace MobileSoft.DAL.WorkFlow
{
	/// <summary>
	/// ���ݷ�����Dal_Tb_WorkFlow_Instance��
	/// </summary>
	public class Dal_Tb_WorkFlow_Instance
	{
		public Dal_Tb_WorkFlow_Instance()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  ��Ա����

		/// <summary>
		/// �õ����ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("InfoId", "Tb_WorkFlow_Instance"); 
		}

		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(int InfoId)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.Int,4)};
			parameters[0].Value = InfoId;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_WorkFlow_Instance_Exists",parameters,out rowsAffected);
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
		public int Add(MobileSoft.Model.WorkFlow.Tb_WorkFlow_Instance model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.Int,4),
					new SqlParameter("@InstanceId", SqlDbType.Int,4),
					new SqlParameter("@Tb_Dictionary_InstanceType_DictionaryCode", SqlDbType.NVarChar,20),
					new SqlParameter("@InstanceMark", SqlDbType.VarChar,2000),
					new SqlParameter("@NoticeMsg", SqlDbType.Int,4),
					new SqlParameter("@NoticeMail", SqlDbType.Int,4),
					new SqlParameter("@NoticePhone", SqlDbType.Int,4),
					new SqlParameter("@NoticeHaveDeal", SqlDbType.Int,4),
					new SqlParameter("@NoticeStartDeal", SqlDbType.Int,4),
					new SqlParameter("@NoticeOtherUser", SqlDbType.Int,4),
					new SqlParameter("@IsComplete", SqlDbType.Int,4),
					new SqlParameter("@IsSuccess", SqlDbType.Int,4)};
			parameters[0].Direction = ParameterDirection.Output;
			parameters[1].Value = model.InstanceId;
			parameters[2].Value = model.Tb_Dictionary_InstanceType_DictionaryCode;
			parameters[3].Value = model.InstanceMark;
			parameters[4].Value = model.NoticeMsg;
			parameters[5].Value = model.NoticeMail;
			parameters[6].Value = model.NoticePhone;
			parameters[7].Value = model.NoticeHaveDeal;
			parameters[8].Value = model.NoticeStartDeal;
			parameters[9].Value = model.NoticeOtherUser;
			parameters[10].Value = model.IsComplete;
			parameters[11].Value = model.IsSuccess;

			DbHelperSQL.RunProcedure("Proc_Tb_WorkFlow_Instance_ADD",parameters,out rowsAffected);
			return (int)parameters[0].Value;
		}

		/// <summary>
		///  ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.WorkFlow.Tb_WorkFlow_Instance model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.Int,4),
					new SqlParameter("@InstanceId", SqlDbType.Int,4),
					new SqlParameter("@Tb_Dictionary_InstanceType_DictionaryCode", SqlDbType.NVarChar,20),
					new SqlParameter("@InstanceMark", SqlDbType.VarChar,2000),
					new SqlParameter("@NoticeMsg", SqlDbType.Int,4),
					new SqlParameter("@NoticeMail", SqlDbType.Int,4),
					new SqlParameter("@NoticePhone", SqlDbType.Int,4),
					new SqlParameter("@NoticeHaveDeal", SqlDbType.Int,4),
					new SqlParameter("@NoticeStartDeal", SqlDbType.Int,4),
					new SqlParameter("@NoticeOtherUser", SqlDbType.Int,4),
					new SqlParameter("@IsComplete", SqlDbType.Int,4),
					new SqlParameter("@IsSuccess", SqlDbType.Int,4)};
			parameters[0].Value = model.InfoId;
			parameters[1].Value = model.InstanceId;
			parameters[2].Value = model.Tb_Dictionary_InstanceType_DictionaryCode;
			parameters[3].Value = model.InstanceMark;
			parameters[4].Value = model.NoticeMsg;
			parameters[5].Value = model.NoticeMail;
			parameters[6].Value = model.NoticePhone;
			parameters[7].Value = model.NoticeHaveDeal;
			parameters[8].Value = model.NoticeStartDeal;
			parameters[9].Value = model.NoticeOtherUser;
			parameters[10].Value = model.IsComplete;
			parameters[11].Value = model.IsSuccess;

			DbHelperSQL.RunProcedure("Proc_Tb_WorkFlow_Instance_Update",parameters,out rowsAffected);
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

			DbHelperSQL.RunProcedure("Proc_Tb_WorkFlow_Instance_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.WorkFlow.Tb_WorkFlow_Instance GetModel(int InfoId)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.Int,4)};
			parameters[0].Value = InfoId;

			MobileSoft.Model.WorkFlow.Tb_WorkFlow_Instance model=new MobileSoft.Model.WorkFlow.Tb_WorkFlow_Instance();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_WorkFlow_Instance_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["InfoId"].ToString()!="")
				{
					model.InfoId=int.Parse(ds.Tables[0].Rows[0]["InfoId"].ToString());
				}
				if(ds.Tables[0].Rows[0]["InstanceId"].ToString()!="")
				{
					model.InstanceId=int.Parse(ds.Tables[0].Rows[0]["InstanceId"].ToString());
				}
				model.Tb_Dictionary_InstanceType_DictionaryCode=ds.Tables[0].Rows[0]["Tb_Dictionary_InstanceType_DictionaryCode"].ToString();
				model.InstanceMark=ds.Tables[0].Rows[0]["InstanceMark"].ToString();
				if(ds.Tables[0].Rows[0]["NoticeMsg"].ToString()!="")
				{
					model.NoticeMsg=int.Parse(ds.Tables[0].Rows[0]["NoticeMsg"].ToString());
				}
				if(ds.Tables[0].Rows[0]["NoticeMail"].ToString()!="")
				{
					model.NoticeMail=int.Parse(ds.Tables[0].Rows[0]["NoticeMail"].ToString());
				}
				if(ds.Tables[0].Rows[0]["NoticePhone"].ToString()!="")
				{
					model.NoticePhone=int.Parse(ds.Tables[0].Rows[0]["NoticePhone"].ToString());
				}
				if(ds.Tables[0].Rows[0]["NoticeHaveDeal"].ToString()!="")
				{
					model.NoticeHaveDeal=int.Parse(ds.Tables[0].Rows[0]["NoticeHaveDeal"].ToString());
				}
				if(ds.Tables[0].Rows[0]["NoticeStartDeal"].ToString()!="")
				{
					model.NoticeStartDeal=int.Parse(ds.Tables[0].Rows[0]["NoticeStartDeal"].ToString());
				}
				if(ds.Tables[0].Rows[0]["NoticeOtherUser"].ToString()!="")
				{
					model.NoticeOtherUser=int.Parse(ds.Tables[0].Rows[0]["NoticeOtherUser"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsComplete"].ToString()!="")
				{
					model.IsComplete=int.Parse(ds.Tables[0].Rows[0]["IsComplete"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsSuccess"].ToString()!="")
				{
					model.IsSuccess=int.Parse(ds.Tables[0].Rows[0]["IsSuccess"].ToString());
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
			strSql.Append("select InfoId,InstanceId,Tb_Dictionary_InstanceType_DictionaryCode,InstanceMark,NoticeMsg,NoticeMail,NoticePhone,NoticeHaveDeal,NoticeStartDeal,NoticeOtherUser,IsComplete,IsSuccess ");
			strSql.Append(" FROM Tb_WorkFlow_Instance ");
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
			strSql.Append(" InfoId,InstanceId,Tb_Dictionary_InstanceType_DictionaryCode,InstanceMark,NoticeMsg,NoticeMail,NoticePhone,NoticeHaveDeal,NoticeStartDeal,NoticeOtherUser,IsComplete,IsSuccess ");
			strSql.Append(" FROM Tb_WorkFlow_Instance ");
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
			parameters[5].Value = "SELECT * FROM Tb_WorkFlow_Instance WHERE 1=1 " + StrCondition;
			parameters[6].Value = "InfoId";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  ��Ա����

            public DataTable GetWorkFlowInstance(int InstanceId, string DictionaryCode)
            {
                  SqlParameter[] parameters = {
					new SqlParameter("@InstanceId", SqlDbType.Int),
                              new SqlParameter("@DictionaryCode", SqlDbType.VarChar,50)
                                              };
                  parameters[0].Value = InstanceId;
                  parameters[1].Value = DictionaryCode;

                  DataSet Ds = DbHelperSQL.RunProcedure("Proc_OAWorkFlow_GetWorkFlowInstance", parameters, "RetDataSet");

                  DataTable dTable = new DataTable();

                  if (Ds.Tables.Count > 0)
                  {
                        dTable = Ds.Tables[0];
                  }

                  return dTable;
            }
	}
}

