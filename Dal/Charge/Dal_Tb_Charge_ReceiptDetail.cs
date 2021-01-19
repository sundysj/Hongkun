using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//�����������
namespace MobileSoft.DAL.Charge
{
	/// <summary>
	/// ���ݷ�����Dal_Tb_Charge_ReceiptDetail��
	/// </summary>
	public class Dal_Tb_Charge_ReceiptDetail
	{
		public Dal_Tb_Charge_ReceiptDetail()
		{
            DbHelperSQL.ConnectionString = PubConstant.tw2bsConnectionString;
		}
		#region  ��Ա����

		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(string RpdCode)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@RpdCode", SqlDbType.VarChar,50)};
			parameters[0].Value = RpdCode;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_Charge_ReceiptDetail_Exists",parameters,out rowsAffected);
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
		public void Add(MobileSoft.Model.Charge.Tb_Charge_ReceiptDetail model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@RpdCode", SqlDbType.VarChar,36),
					new SqlParameter("@ReceiptCode", SqlDbType.VarChar,36),
					new SqlParameter("@ResourcesID", SqlDbType.BigInt,8),
					new SqlParameter("@Quantity", SqlDbType.Int,4),
					new SqlParameter("@SalesPrice", SqlDbType.Decimal,9),
					new SqlParameter("@DiscountPrice", SqlDbType.Decimal,9),
					new SqlParameter("@MemberDiscountPrice", SqlDbType.Decimal,9),
					new SqlParameter("@GroupPrice", SqlDbType.Decimal,9),
					new SqlParameter("@DetailAmount", SqlDbType.Decimal,9),
					new SqlParameter("@RpdMemo", SqlDbType.VarChar,1000),
					new SqlParameter("@RpdIsDelete", SqlDbType.Int,4)};
			parameters[0].Value = model.RpdCode;
			parameters[1].Value = model.ReceiptCode;
			parameters[2].Value = model.ResourcesID;
			parameters[3].Value = model.Quantity;
			parameters[4].Value = model.SalesPrice;
			parameters[5].Value = model.DiscountPrice;
			parameters[6].Value = model.MemberDiscountPrice;
			parameters[7].Value = model.GroupPrice;
			parameters[8].Value = model.DetailAmount;
			parameters[9].Value = model.RpdMemo;
			parameters[10].Value = model.RpdIsDelete;

			DbHelperSQL.RunProcedure("Proc_Tb_Charge_ReceiptDetail_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  ����һ������
		/// </summary>
		public void Update(MobileSoft.Model.Charge.Tb_Charge_ReceiptDetail model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@RpdCode", SqlDbType.VarChar,36),
					new SqlParameter("@ReceiptCode", SqlDbType.VarChar,36),
					new SqlParameter("@ResourcesID", SqlDbType.BigInt,8),
					new SqlParameter("@Quantity", SqlDbType.Int,4),
					new SqlParameter("@SalesPrice", SqlDbType.Decimal,9),
					new SqlParameter("@DiscountPrice", SqlDbType.Decimal,9),
					new SqlParameter("@MemberDiscountPrice", SqlDbType.Decimal,9),
					new SqlParameter("@GroupPrice", SqlDbType.Decimal,9),
					new SqlParameter("@DetailAmount", SqlDbType.Decimal,9),
					new SqlParameter("@RpdMemo", SqlDbType.VarChar,1000),
					new SqlParameter("@RpdIsDelete", SqlDbType.Int,4)};
			parameters[0].Value = model.RpdCode;
			parameters[1].Value = model.ReceiptCode;
			parameters[2].Value = model.ResourcesID;
			parameters[3].Value = model.Quantity;
			parameters[4].Value = model.SalesPrice;
			parameters[5].Value = model.DiscountPrice;
			parameters[6].Value = model.MemberDiscountPrice;
			parameters[7].Value = model.GroupPrice;
			parameters[8].Value = model.DetailAmount;
			parameters[9].Value = model.RpdMemo;
			parameters[10].Value = model.RpdIsDelete;

			DbHelperSQL.RunProcedure("Proc_Tb_Charge_ReceiptDetail_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(string RpdCode)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@RpdCode", SqlDbType.VarChar,50)};
			parameters[0].Value = RpdCode;

			DbHelperSQL.RunProcedure("Proc_Tb_Charge_ReceiptDetail_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public MobileSoft.Model.Charge.Tb_Charge_ReceiptDetail GetModel(string RpdCode)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@RpdCode", SqlDbType.VarChar,50)};
			parameters[0].Value = RpdCode;

			MobileSoft.Model.Charge.Tb_Charge_ReceiptDetail model=new MobileSoft.Model.Charge.Tb_Charge_ReceiptDetail();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_Charge_ReceiptDetail_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				model.RpdCode=ds.Tables[0].Rows[0]["RpdCode"].ToString();
				model.ReceiptCode=ds.Tables[0].Rows[0]["ReceiptCode"].ToString();
				if(ds.Tables[0].Rows[0]["ResourcesID"].ToString()!="")
				{
					model.ResourcesID=long.Parse(ds.Tables[0].Rows[0]["ResourcesID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Quantity"].ToString()!="")
				{
					model.Quantity=int.Parse(ds.Tables[0].Rows[0]["Quantity"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SalesPrice"].ToString()!="")
				{
					model.SalesPrice=decimal.Parse(ds.Tables[0].Rows[0]["SalesPrice"].ToString());
				}
				if(ds.Tables[0].Rows[0]["DiscountPrice"].ToString()!="")
				{
					model.DiscountPrice=decimal.Parse(ds.Tables[0].Rows[0]["DiscountPrice"].ToString());
				}
				if(ds.Tables[0].Rows[0]["MemberDiscountPrice"].ToString()!="")
				{
					model.MemberDiscountPrice=decimal.Parse(ds.Tables[0].Rows[0]["MemberDiscountPrice"].ToString());
				}
				if(ds.Tables[0].Rows[0]["GroupPrice"].ToString()!="")
				{
					model.GroupPrice=decimal.Parse(ds.Tables[0].Rows[0]["GroupPrice"].ToString());
				}
				if(ds.Tables[0].Rows[0]["DetailAmount"].ToString()!="")
				{
					model.DetailAmount=decimal.Parse(ds.Tables[0].Rows[0]["DetailAmount"].ToString());
				}
				model.RpdMemo=ds.Tables[0].Rows[0]["RpdMemo"].ToString();
				if(ds.Tables[0].Rows[0]["RpdIsDelete"].ToString()!="")
				{
					model.RpdIsDelete=int.Parse(ds.Tables[0].Rows[0]["RpdIsDelete"].ToString());
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
			strSql.Append(" FROM view_Charge_ReceiptDetail_Filter");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
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
			strSql.Append(" RpdCode,ReceiptCode,ResourcesID,Quantity,SalesPrice,DiscountPrice,MemberDiscountPrice,GroupPrice,DetailAmount,RpdMemo,RpdIsDelete ");
			strSql.Append(" FROM Tb_Charge_ReceiptDetail ");
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
			parameters[5].Value = "SELECT * FROM Tb_Charge_ReceiptDetail WHERE 1=1 " + StrCondition;
			parameters[6].Value = "RpdCode";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  ��Ա����
	}
}

