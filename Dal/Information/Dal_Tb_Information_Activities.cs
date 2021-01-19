using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//�����������
namespace MobileSoft.DAL.Information
{
	/// <summary>
	/// ���ݷ�����Dal_Tb_Information_Activities��
	/// </summary>
	public class Dal_Tb_Information_Activities
	{
		public Dal_Tb_Information_Activities()
		{
			DbHelperSQL.ConnectionString = PubConstant.SQMBSContionString;
		}
		#region  ��Ա����

		/// <summary>
		/// �õ����ID
		/// </summary>
		public long GetMaxId()
		{
		return DbHelperSQL.GetMaxID("ActId", "Tb_Information_Activities"); 
		}

		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(long ActId)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ActId", SqlDbType.BigInt)};
			parameters[0].Value = ActId;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_Information_Activities_Exists",parameters,out rowsAffected);
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
		public long Add(MobileSoft.Model.Information.Tb_Information_Activities model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ActId", SqlDbType.BigInt,8),
					new SqlParameter("@BussId", SqlDbType.BigInt,8),
					new SqlParameter("@Title", SqlDbType.NVarChar,100),
					new SqlParameter("@ActPublisher", SqlDbType.NVarChar,100),
					new SqlParameter("@PublishDate", SqlDbType.DateTime),
					new SqlParameter("@ActContent", SqlDbType.NVarChar),
					new SqlParameter("@ActImage", SqlDbType.NVarChar,200),
					new SqlParameter("@NumID", SqlDbType.BigInt,8),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2)};
			parameters[0].Direction = ParameterDirection.Output;
			parameters[1].Value = model.BussId;
			parameters[2].Value = model.Title;
			parameters[3].Value = model.ActPublisher;
			parameters[4].Value = model.PublishDate;
			parameters[5].Value = model.ActContent;
			parameters[6].Value = model.ActImage;
			parameters[7].Value = model.NumID;
			parameters[8].Value = model.IsDelete;

			DbHelperSQL.RunProcedure("Proc_Tb_Information_Activities_ADD",parameters,out rowsAffected);
			return (long)parameters[0].Value;
		}

		/// <summary>
		///  ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.Information.Tb_Information_Activities model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ActId", SqlDbType.BigInt,8),
					new SqlParameter("@BussId", SqlDbType.BigInt,8),
					new SqlParameter("@Title", SqlDbType.NVarChar,100),
					new SqlParameter("@ActPublisher", SqlDbType.NVarChar,100),
					new SqlParameter("@PublishDate", SqlDbType.DateTime),
					new SqlParameter("@ActContent", SqlDbType.NVarChar),
					new SqlParameter("@ActImage", SqlDbType.NVarChar,200),
					new SqlParameter("@NumID", SqlDbType.BigInt,8),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2)};
			parameters[0].Value = model.ActId;
			parameters[1].Value = model.BussId;
			parameters[2].Value = model.Title;
			parameters[3].Value = model.ActPublisher;
			parameters[4].Value = model.PublishDate;
			parameters[5].Value = model.ActContent;
			parameters[6].Value = model.ActImage;
			parameters[7].Value = model.NumID;
			parameters[8].Value = model.IsDelete;

			DbHelperSQL.RunProcedure("Proc_Tb_Information_Activities_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(long ActId)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ActId", SqlDbType.BigInt)};
			parameters[0].Value = ActId;

			DbHelperSQL.RunProcedure("Proc_Tb_Information_Activities_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.Information.Tb_Information_Activities GetModel(long ActId)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@ActId", SqlDbType.BigInt)};
			parameters[0].Value = ActId;

			MobileSoft.Model.Information.Tb_Information_Activities model=new MobileSoft.Model.Information.Tb_Information_Activities();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_Information_Activities_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ActId"].ToString()!="")
				{
					model.ActId=long.Parse(ds.Tables[0].Rows[0]["ActId"].ToString());
				}
				if(ds.Tables[0].Rows[0]["BussId"].ToString()!="")
				{
					model.BussId=long.Parse(ds.Tables[0].Rows[0]["BussId"].ToString());
				}
				model.Title=ds.Tables[0].Rows[0]["Title"].ToString();
				model.ActPublisher=ds.Tables[0].Rows[0]["ActPublisher"].ToString();
				if(ds.Tables[0].Rows[0]["PublishDate"].ToString()!="")
				{
					model.PublishDate=DateTime.Parse(ds.Tables[0].Rows[0]["PublishDate"].ToString());
				}
				model.ActContent=ds.Tables[0].Rows[0]["ActContent"].ToString();
				model.ActImage=ds.Tables[0].Rows[0]["ActImage"].ToString();
				if(ds.Tables[0].Rows[0]["NumID"].ToString()!="")
				{
					model.NumID=long.Parse(ds.Tables[0].Rows[0]["NumID"].ToString());
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
            strSql.Append(" FROM View_Tb_Information_Activities_Filter ");
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
			strSql.Append(" * ");
            strSql.Append(" FROM View_Tb_Information_Activities_Filter ");
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
            parameters[5].Value = "SELECT * FROM View_Tb_Information_Activities_Filter WHERE 1=1 " + StrCondition;
			parameters[6].Value = "ActId";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  ��Ա����
	}
}

