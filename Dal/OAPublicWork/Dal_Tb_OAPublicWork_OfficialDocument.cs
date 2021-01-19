using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//�����������
namespace MobileSoft.DAL.OAPublicWork
{
	/// <summary>
	/// ���ݷ�����Dal_Tb_OAPublicWork_OfficialDocument��
	/// </summary>
	public class Dal_Tb_OAPublicWork_OfficialDocument
	{
		public Dal_Tb_OAPublicWork_OfficialDocument()
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

			int result= DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_OfficialDocument_Exists",parameters,out rowsAffected);
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
		public int Add(MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_OfficialDocument model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.Int,4),
					new SqlParameter("@Tb_WorkFlow_FlowSort_InfoId", SqlDbType.Int,4),
					new SqlParameter("@UserName", SqlDbType.NVarChar,20),
					new SqlParameter("@FileCode", SqlDbType.VarChar,100),
					new SqlParameter("@FileTitle", SqlDbType.VarChar,100),
					new SqlParameter("@DispatchUnits", SqlDbType.VarChar,500),
					new SqlParameter("@Writer", SqlDbType.VarChar,50),
					new SqlParameter("@WriteDate", SqlDbType.DateTime),
					new SqlParameter("@ReciveUnits", SqlDbType.VarChar,500),
					new SqlParameter("@FileSecret", SqlDbType.NVarChar,20),
					new SqlParameter("@Nervous", SqlDbType.NVarChar,20),
					new SqlParameter("@KeyWords", SqlDbType.VarChar,500),
					new SqlParameter("@InfoContent", SqlDbType.Text),
					new SqlParameter("@DocumentUrl", SqlDbType.VarChar,2000),
					new SqlParameter("@WorkStartDate", SqlDbType.DateTime)};
			parameters[0].Direction = ParameterDirection.Output;
			parameters[1].Value = model.Tb_WorkFlow_FlowSort_InfoId;
			parameters[2].Value = model.UserName;
			parameters[3].Value = model.FileCode;
			parameters[4].Value = model.FileTitle;
			parameters[5].Value = model.DispatchUnits;
			parameters[6].Value = model.Writer;
			parameters[7].Value = model.WriteDate;
			parameters[8].Value = model.ReciveUnits;
			parameters[9].Value = model.FileSecret;
			parameters[10].Value = model.Nervous;
			parameters[11].Value = model.KeyWords;
			parameters[12].Value = model.InfoContent;
			parameters[13].Value = model.DocumentUrl;
			parameters[14].Value = model.WorkStartDate;

			DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_OfficialDocument_ADD",parameters,out rowsAffected);
			return (int)parameters[0].Value;
		}

		/// <summary>
		///  ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_OfficialDocument model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.Int,4),
					new SqlParameter("@Tb_WorkFlow_FlowSort_InfoId", SqlDbType.Int,4),
					new SqlParameter("@UserName", SqlDbType.NVarChar,20),
					new SqlParameter("@FileCode", SqlDbType.VarChar,100),
					new SqlParameter("@FileTitle", SqlDbType.VarChar,100),
					new SqlParameter("@DispatchUnits", SqlDbType.VarChar,500),
					new SqlParameter("@Writer", SqlDbType.VarChar,50),
					new SqlParameter("@WriteDate", SqlDbType.DateTime),
					new SqlParameter("@ReciveUnits", SqlDbType.VarChar,500),
					new SqlParameter("@FileSecret", SqlDbType.NVarChar,20),
					new SqlParameter("@Nervous", SqlDbType.NVarChar,20),
					new SqlParameter("@KeyWords", SqlDbType.VarChar,500),
					new SqlParameter("@InfoContent", SqlDbType.Text),
					new SqlParameter("@DocumentUrl", SqlDbType.VarChar,2000),
					new SqlParameter("@WorkStartDate", SqlDbType.DateTime)};
			parameters[0].Value = model.InfoId;
			parameters[1].Value = model.Tb_WorkFlow_FlowSort_InfoId;
			parameters[2].Value = model.UserName;
			parameters[3].Value = model.FileCode;
			parameters[4].Value = model.FileTitle;
			parameters[5].Value = model.DispatchUnits;
			parameters[6].Value = model.Writer;
			parameters[7].Value = model.WriteDate;
			parameters[8].Value = model.ReciveUnits;
			parameters[9].Value = model.FileSecret;
			parameters[10].Value = model.Nervous;
			parameters[11].Value = model.KeyWords;
			parameters[12].Value = model.InfoContent;
			parameters[13].Value = model.DocumentUrl;
			parameters[14].Value = model.WorkStartDate;

			DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_OfficialDocument_Update",parameters,out rowsAffected);
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

			DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_OfficialDocument_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_OfficialDocument GetModel(int InfoId)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@InfoId", SqlDbType.Int,4)};
			parameters[0].Value = InfoId;

			MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_OfficialDocument model=new MobileSoft.Model.OAPublicWork.Tb_OAPublicWork_OfficialDocument();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_OAPublicWork_OfficialDocument_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["InfoId"].ToString()!="")
				{
					model.InfoId=int.Parse(ds.Tables[0].Rows[0]["InfoId"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Tb_WorkFlow_FlowSort_InfoId"].ToString()!="")
				{
					model.Tb_WorkFlow_FlowSort_InfoId=int.Parse(ds.Tables[0].Rows[0]["Tb_WorkFlow_FlowSort_InfoId"].ToString());
				}
				model.UserName=ds.Tables[0].Rows[0]["UserName"].ToString();
				model.FileCode=ds.Tables[0].Rows[0]["FileCode"].ToString();
				model.FileTitle=ds.Tables[0].Rows[0]["FileTitle"].ToString();
				model.DispatchUnits=ds.Tables[0].Rows[0]["DispatchUnits"].ToString();
				model.Writer=ds.Tables[0].Rows[0]["Writer"].ToString();
				if(ds.Tables[0].Rows[0]["WriteDate"].ToString()!="")
				{
					model.WriteDate=DateTime.Parse(ds.Tables[0].Rows[0]["WriteDate"].ToString());
				}
				model.ReciveUnits=ds.Tables[0].Rows[0]["ReciveUnits"].ToString();
				model.FileSecret=ds.Tables[0].Rows[0]["FileSecret"].ToString();
				model.Nervous=ds.Tables[0].Rows[0]["Nervous"].ToString();
				model.KeyWords=ds.Tables[0].Rows[0]["KeyWords"].ToString();
				model.InfoContent=ds.Tables[0].Rows[0]["InfoContent"].ToString();
				model.DocumentUrl=ds.Tables[0].Rows[0]["DocumentUrl"].ToString();
				if(ds.Tables[0].Rows[0]["WorkStartDate"].ToString()!="")
				{
					model.WorkStartDate=DateTime.Parse(ds.Tables[0].Rows[0]["WorkStartDate"].ToString());
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
			strSql.Append("select InfoId,Tb_WorkFlow_FlowSort_InfoId,UserName,FileCode,FileTitle,DispatchUnits,Writer,WriteDate,ReciveUnits,FileSecret,Nervous,KeyWords,InfoContent,DocumentUrl,WorkStartDate ");
			strSql.Append(" FROM Tb_OAPublicWork_OfficialDocument ");
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
			strSql.Append(" InfoId,Tb_WorkFlow_FlowSort_InfoId,UserName,FileCode,FileTitle,DispatchUnits,Writer,WriteDate,ReciveUnits,FileSecret,Nervous,KeyWords,InfoContent,DocumentUrl,WorkStartDate ");
			strSql.Append(" FROM Tb_OAPublicWork_OfficialDocument ");
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
			parameters[5].Value = "SELECT * FROM Tb_OAPublicWork_OfficialDocument WHERE 1=1 " + StrCondition;
			parameters[6].Value = "InfoId";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  ��Ա����

            public DataTable GetWaitWorkList(string LoginUserCode, string Type)
            {
                  SqlParameter[] parameters = {
					new SqlParameter("@Code", SqlDbType.VarChar,50),
                              new SqlParameter("@Type", SqlDbType.VarChar,50)
                                              };
                  parameters[0].Value = LoginUserCode;
                  parameters[1].Value = Type;

                  DataTable dTable = DbHelperSQL.RunProcedure("Proc_OAPublicWork_GetWaitWorkListMobile", parameters, "RetDataSet").Tables[0];

                  return dTable;
            }
	}
}

