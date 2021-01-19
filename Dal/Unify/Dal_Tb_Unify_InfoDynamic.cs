using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//�����������
namespace MobileSoft.DAL.Unify
{
	/// <summary>
	/// ���ݷ�����Dal_Tb_Unify_InfoDynamic��
	/// </summary>
	public class Dal_Tb_Unify_InfoDynamic
	{
		public Dal_Tb_Unify_InfoDynamic()
		{
            DbHelperSQL.ConnectionString = PubConstant.SQIBSContionString;
		}
		#region  ��Ա����

		/// <summary>
		/// �õ����ID
		/// </summary>
		public long GetMaxId()
		{
		return DbHelperSQL.GetMaxID("InfoID", "Tb_Unify_InfoDynamic"); 
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

			int result= DbHelperSQL.RunProcedure("Proc_Tb_Unify_InfoDynamic_Exists",parameters,out rowsAffected);
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
		public long Add(MobileSoft.Model.Unify.Tb_Unify_InfoDynamic model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoID", SqlDbType.BigInt,8),
					new SqlParameter("@InfoType", SqlDbType.SmallInt,2),
					new SqlParameter("@TypeName", SqlDbType.NVarChar,20),
					new SqlParameter("@Heading", SqlDbType.NVarChar,50),
					new SqlParameter("@IssueDate", SqlDbType.DateTime),
					new SqlParameter("@InfoSource", SqlDbType.NVarChar,20),
					new SqlParameter("@ImageUrl", SqlDbType.VarChar,1000),
					new SqlParameter("@Recommended", SqlDbType.SmallInt,2),
					new SqlParameter("@HitCount", SqlDbType.Int,4),
					new SqlParameter("@InfoContent", SqlDbType.NText)};
			parameters[0].Direction = ParameterDirection.Output;
			parameters[1].Value = model.InfoType;
			parameters[2].Value = model.TypeName;
			parameters[3].Value = model.Heading;
			parameters[4].Value = model.IssueDate;
			parameters[5].Value = model.InfoSource;
			parameters[6].Value = model.ImageUrl;
			parameters[7].Value = model.Recommended;
			parameters[8].Value = model.HitCount;
			parameters[9].Value = model.InfoContent;

			DbHelperSQL.RunProcedure("Proc_Tb_Unify_InfoDynamic_ADD",parameters,out rowsAffected);
			return (long)parameters[0].Value;
		}

		/// <summary>
		///  ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.Unify.Tb_Unify_InfoDynamic model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@InfoID", SqlDbType.BigInt,8),
					new SqlParameter("@InfoType", SqlDbType.SmallInt,2),
					new SqlParameter("@TypeName", SqlDbType.NVarChar,20),
					new SqlParameter("@Heading", SqlDbType.NVarChar,50),
					new SqlParameter("@IssueDate", SqlDbType.DateTime),
					new SqlParameter("@InfoSource", SqlDbType.NVarChar,20),
					new SqlParameter("@ImageUrl", SqlDbType.VarChar,1000),
					new SqlParameter("@Recommended", SqlDbType.SmallInt,2),
					new SqlParameter("@HitCount", SqlDbType.Int,4),
					new SqlParameter("@InfoContent", SqlDbType.NText)};
			parameters[0].Value = model.InfoID;
			parameters[1].Value = model.InfoType;
			parameters[2].Value = model.TypeName;
			parameters[3].Value = model.Heading;
			parameters[4].Value = model.IssueDate;
			parameters[5].Value = model.InfoSource;
			parameters[6].Value = model.ImageUrl;
			parameters[7].Value = model.Recommended;
			parameters[8].Value = model.HitCount;
			parameters[9].Value = model.InfoContent;

			DbHelperSQL.RunProcedure("Proc_Tb_Unify_InfoDynamic_Update",parameters,out rowsAffected);
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

			DbHelperSQL.RunProcedure("Proc_Tb_Unify_InfoDynamic_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.Unify.Tb_Unify_InfoDynamic GetModel(long InfoID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@InfoID", SqlDbType.BigInt)};
			parameters[0].Value = InfoID;

			MobileSoft.Model.Unify.Tb_Unify_InfoDynamic model=new MobileSoft.Model.Unify.Tb_Unify_InfoDynamic();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_Unify_InfoDynamic_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["InfoID"].ToString()!="")
				{
					model.InfoID=long.Parse(ds.Tables[0].Rows[0]["InfoID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["InfoType"].ToString()!="")
				{
					model.InfoType=int.Parse(ds.Tables[0].Rows[0]["InfoType"].ToString());
				}
				model.TypeName=ds.Tables[0].Rows[0]["TypeName"].ToString();
				model.Heading=ds.Tables[0].Rows[0]["Heading"].ToString();
				if(ds.Tables[0].Rows[0]["IssueDate"].ToString()!="")
				{
					model.IssueDate=DateTime.Parse(ds.Tables[0].Rows[0]["IssueDate"].ToString());
				}
				model.InfoSource=ds.Tables[0].Rows[0]["InfoSource"].ToString();
				model.ImageUrl=ds.Tables[0].Rows[0]["ImageUrl"].ToString();
				if(ds.Tables[0].Rows[0]["Recommended"].ToString()!="")
				{
					model.Recommended=int.Parse(ds.Tables[0].Rows[0]["Recommended"].ToString());
				}
				if(ds.Tables[0].Rows[0]["HitCount"].ToString()!="")
				{
					model.HitCount=int.Parse(ds.Tables[0].Rows[0]["HitCount"].ToString());
				}
				model.InfoContent=ds.Tables[0].Rows[0]["InfoContent"].ToString();
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
			strSql.Append(" FROM View_Unify_InfoDynamic_Filter ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

        public DataSet GetList(string top,string Fields,string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top "+top+" "+Fields);
            strSql.Append(" FROM View_Unify_InfoDynamic_Filter ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

		/// <summary>
		/// ���ǰ��������
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" * ");
            strSql.Append(" FROM View_Unify_InfoDynamic_Filter ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
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
			parameters[5].Value = StrCondition;
			parameters[6].Value = "InfoID";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  ��Ա����
	}
}

