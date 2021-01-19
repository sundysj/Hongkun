using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//�����������
namespace MobileSoft.DAL.SQMSys
{
	/// <summary>
	/// ���ݷ�����Dal_Tb_Sys_Field��
	/// </summary>
	public class Dal_Tb_Sys_Field
	{
		public Dal_Tb_Sys_Field()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  ��Ա����

		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(Guid FieldCode)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@FieldCode", SqlDbType.UniqueIdentifier)};
			parameters[0].Value = FieldCode;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_Sys_Field_Exists",parameters,out rowsAffected);
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
		public void Add(MobileSoft.Model.SQMSys.Tb_Sys_Field model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@FieldCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@TemplateCode", SqlDbType.NVarChar,50),
					new SqlParameter("@FieldSign", SqlDbType.NVarChar,30),
					new SqlParameter("@FieldName", SqlDbType.NVarChar,30),
					new SqlParameter("@DefaultName", SqlDbType.NVarChar,30),
					new SqlParameter("@FieldOrderID", SqlDbType.Int,4),
					new SqlParameter("@FieldType", SqlDbType.Int,4),
					new SqlParameter("@FieldLength", SqlDbType.Int,4),
					new SqlParameter("@IsUsed", SqlDbType.SmallInt,2)};
			parameters[0].Value = model.FieldCode;
			parameters[1].Value = model.TemplateCode;
			parameters[2].Value = model.FieldSign;
			parameters[3].Value = model.FieldName;
			parameters[4].Value = model.DefaultName;
			parameters[5].Value = model.FieldOrderID;
			parameters[6].Value = model.FieldType;
			parameters[7].Value = model.FieldLength;
			parameters[8].Value = model.IsUsed;

			DbHelperSQL.RunProcedure("Proc_Tb_Sys_Field_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.SQMSys.Tb_Sys_Field model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@FieldCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@TemplateCode", SqlDbType.NVarChar,50),
					new SqlParameter("@FieldSign", SqlDbType.NVarChar,30),
					new SqlParameter("@FieldName", SqlDbType.NVarChar,30),
					new SqlParameter("@DefaultName", SqlDbType.NVarChar,30),
					new SqlParameter("@FieldOrderID", SqlDbType.Int,4),
					new SqlParameter("@FieldType", SqlDbType.Int,4),
					new SqlParameter("@FieldLength", SqlDbType.Int,4),
					new SqlParameter("@IsUsed", SqlDbType.SmallInt,2)};
			parameters[0].Value = model.FieldCode;
			parameters[1].Value = model.TemplateCode;
			parameters[2].Value = model.FieldSign;
			parameters[3].Value = model.FieldName;
			parameters[4].Value = model.DefaultName;
			parameters[5].Value = model.FieldOrderID;
			parameters[6].Value = model.FieldType;
			parameters[7].Value = model.FieldLength;
			parameters[8].Value = model.IsUsed;

			DbHelperSQL.RunProcedure("Proc_Tb_Sys_Field_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(Guid FieldCode)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@FieldCode", SqlDbType.UniqueIdentifier)};
			parameters[0].Value = FieldCode;

			DbHelperSQL.RunProcedure("Proc_Tb_Sys_Field_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.SQMSys.Tb_Sys_Field GetModel(Guid FieldCode)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@FieldCode", SqlDbType.UniqueIdentifier)};
			parameters[0].Value = FieldCode;

			MobileSoft.Model.SQMSys.Tb_Sys_Field model=new MobileSoft.Model.SQMSys.Tb_Sys_Field();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_Sys_Field_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["FieldCode"].ToString()!="")
				{
					model.FieldCode=new Guid(ds.Tables[0].Rows[0]["FieldCode"].ToString());
				}
				model.TemplateCode=ds.Tables[0].Rows[0]["TemplateCode"].ToString();
				model.FieldSign=ds.Tables[0].Rows[0]["FieldSign"].ToString();
				model.FieldName=ds.Tables[0].Rows[0]["FieldName"].ToString();
				model.DefaultName=ds.Tables[0].Rows[0]["DefaultName"].ToString();
				if(ds.Tables[0].Rows[0]["FieldOrderID"].ToString()!="")
				{
					model.FieldOrderID=int.Parse(ds.Tables[0].Rows[0]["FieldOrderID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["FieldType"].ToString()!="")
				{
					model.FieldType=int.Parse(ds.Tables[0].Rows[0]["FieldType"].ToString());
				}
				if(ds.Tables[0].Rows[0]["FieldLength"].ToString()!="")
				{
					model.FieldLength=int.Parse(ds.Tables[0].Rows[0]["FieldLength"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsUsed"].ToString()!="")
				{
					model.IsUsed=int.Parse(ds.Tables[0].Rows[0]["IsUsed"].ToString());
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
			strSql.Append("select FieldCode,TemplateCode,FieldSign,FieldName,DefaultName,FieldOrderID,FieldType,FieldLength,IsUsed ");
			strSql.Append(" FROM Tb_Sys_Field ");
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
			strSql.Append(" FieldCode,TemplateCode,FieldSign,FieldName,DefaultName,FieldOrderID,FieldType,FieldLength,IsUsed ");
			strSql.Append(" FROM Tb_Sys_Field ");
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
			parameters[5].Value = "SELECT * FROM Tb_Sys_Field WHERE 1=1 " + StrCondition;
			parameters[6].Value = "FieldCode";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  ��Ա����
	}
}

