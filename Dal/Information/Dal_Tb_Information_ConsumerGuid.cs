using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//�����������
namespace MobileSoft.DAL.Information
{
	/// <summary>
	/// ���ݷ�����Dal_Tb_Information_ConsumerGuid��
	/// </summary>
	public class Dal_Tb_Information_ConsumerGuid
	{
		public Dal_Tb_Information_ConsumerGuid()
		{
            DbHelperSQL.ConnectionString = PubConstant.SQMBSContionString;
		}
		#region  ��Ա����

		/// <summary>
		/// �õ����ID
		/// </summary>
		public long GetMaxId()
		{
		return DbHelperSQL.GetMaxID("GuideId", "Tb_Information_ConsumerGuid"); 
		}

		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(long GuideId)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@GuideId", SqlDbType.BigInt)};
			parameters[0].Value = GuideId;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_Information_ConsumerGuid_Exists",parameters,out rowsAffected);
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
		public long Add(MobileSoft.Model.Information.Tb_Information_ConsumerGuid model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@GuideId", SqlDbType.BigInt,8),
					new SqlParameter("@BussId", SqlDbType.BigInt,8),
					new SqlParameter("@Title", SqlDbType.NVarChar,100),
					new SqlParameter("@GudPublisher", SqlDbType.NVarChar,100),
					new SqlParameter("@PubulishDate", SqlDbType.DateTime),
					new SqlParameter("@GudContent", SqlDbType.NVarChar),
					new SqlParameter("@GudImage", SqlDbType.NVarChar,100),
					new SqlParameter("@NumID", SqlDbType.BigInt,8),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2)};
			parameters[0].Direction = ParameterDirection.Output;
			parameters[1].Value = model.BussId;
			parameters[2].Value = model.Title;
			parameters[3].Value = model.GudPublisher;
			parameters[4].Value = model.PubulishDate;
			parameters[5].Value = model.GudContent;
			parameters[6].Value = model.GudImage;
			parameters[7].Value = model.NumID;
			parameters[8].Value = model.IsDelete;

			DbHelperSQL.RunProcedure("Proc_Tb_Information_ConsumerGuid_ADD",parameters,out rowsAffected);
			return (long)parameters[0].Value;
		}

		/// <summary>
		///  ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.Information.Tb_Information_ConsumerGuid model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@GuideId", SqlDbType.BigInt,8),
					new SqlParameter("@BussId", SqlDbType.BigInt,8),
					new SqlParameter("@Title", SqlDbType.NVarChar,100),
					new SqlParameter("@GudPublisher", SqlDbType.NVarChar,100),
					new SqlParameter("@PubulishDate", SqlDbType.DateTime),
					new SqlParameter("@GudContent", SqlDbType.NVarChar),
					new SqlParameter("@GudImage", SqlDbType.NVarChar,100),
					new SqlParameter("@NumID", SqlDbType.BigInt,8),
					new SqlParameter("@IsDelete", SqlDbType.SmallInt,2)};
			parameters[0].Value = model.GuideId;
			parameters[1].Value = model.BussId;
			parameters[2].Value = model.Title;
			parameters[3].Value = model.GudPublisher;
			parameters[4].Value = model.PubulishDate;
			parameters[5].Value = model.GudContent;
			parameters[6].Value = model.GudImage;
			parameters[7].Value = model.NumID;
			parameters[8].Value = model.IsDelete;

			DbHelperSQL.RunProcedure("Proc_Tb_Information_ConsumerGuid_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(long GuideId)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@GuideId", SqlDbType.BigInt)};
			parameters[0].Value = GuideId;

			DbHelperSQL.RunProcedure("Proc_Tb_Information_ConsumerGuid_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.Information.Tb_Information_ConsumerGuid GetModel(long GuideId)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@GuideId", SqlDbType.BigInt)};
			parameters[0].Value = GuideId;

			MobileSoft.Model.Information.Tb_Information_ConsumerGuid model=new MobileSoft.Model.Information.Tb_Information_ConsumerGuid();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_Information_ConsumerGuid_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["GuideId"].ToString()!="")
				{
					model.GuideId=long.Parse(ds.Tables[0].Rows[0]["GuideId"].ToString());
				}
				if(ds.Tables[0].Rows[0]["BussId"].ToString()!="")
				{
					model.BussId=long.Parse(ds.Tables[0].Rows[0]["BussId"].ToString());
				}
				model.Title=ds.Tables[0].Rows[0]["Title"].ToString();
				model.GudPublisher=ds.Tables[0].Rows[0]["GudPublisher"].ToString();
				if(ds.Tables[0].Rows[0]["PubulishDate"].ToString()!="")
				{
					model.PubulishDate=DateTime.Parse(ds.Tables[0].Rows[0]["PubulishDate"].ToString());
				}
				model.GudContent=ds.Tables[0].Rows[0]["GudContent"].ToString();
				model.GudImage=ds.Tables[0].Rows[0]["GudImage"].ToString();
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
			strSql.Append("select GuideId,BussId,Title,GudPublisher,PubulishDate,GudContent,GudImage,NumID,IsDelete ");
			strSql.Append(" FROM Tb_Information_ConsumerGuid ");
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
			strSql.Append(" GuideId,BussId,Title,GudPublisher,PubulishDate,GudContent,GudImage,NumID,IsDelete ");
			strSql.Append(" FROM Tb_Information_ConsumerGuid ");
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
			parameters[5].Value = "SELECT * FROM Tb_Information_ConsumerGuid WHERE 1=1 " + StrCondition;
			parameters[6].Value = "GuideId";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  ��Ա����
	}
}

