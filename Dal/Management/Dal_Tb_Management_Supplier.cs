using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.Management
{
	/// <summary>
	/// 数据访问类Dal_Tb_Management_Supplier。
	/// </summary>
	public class Dal_Tb_Management_Supplier
	{
		public Dal_Tb_Management_Supplier()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string SupplierCode)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@SupplierCode", SqlDbType.VarChar,50)};
			parameters[0].Value = SupplierCode;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_Management_Supplier_Exists",parameters,out rowsAffected);
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
		///  增加一条数据
		/// </summary>
		public void Add(MobileSoft.Model.Management.Tb_Management_Supplier model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@SupplierCode", SqlDbType.VarChar,36),
					new SqlParameter("@OrganCode", SqlDbType.VarChar,36),
					new SqlParameter("@CommID", SqlDbType.VarChar,36),
					new SqlParameter("@RefDate", SqlDbType.VarChar,36),
					new SqlParameter("@BussType", SqlDbType.VarChar,10),
					new SqlParameter("@BussName", SqlDbType.VarChar,36),
					new SqlParameter("@LegalPerson", SqlDbType.VarChar,36),
					new SqlParameter("@LegalPaperCode", SqlDbType.VarChar,36),
					new SqlParameter("@LicenseNum", SqlDbType.VarChar,36),
					new SqlParameter("@OrganizationCode", SqlDbType.VarChar,36),
					new SqlParameter("@RegNumber", SqlDbType.VarChar,36),
					new SqlParameter("@Province", SqlDbType.VarChar,36),
					new SqlParameter("@City", SqlDbType.VarChar,36),
					new SqlParameter("@Street", SqlDbType.VarChar,36),
					new SqlParameter("@Addr", SqlDbType.VarChar,299),
					new SqlParameter("@ZipCode", SqlDbType.VarChar,36),
					new SqlParameter("@ContactPerson", SqlDbType.VarChar,36),
					new SqlParameter("@ContactPhone", SqlDbType.VarChar,36),
					new SqlParameter("@MainBusiness", SqlDbType.VarChar,3999),
					new SqlParameter("@Memo", SqlDbType.VarChar,3999),
					new SqlParameter("@IsDelete", SqlDbType.Int,4)};
			parameters[0].Value = model.SupplierCode;
			parameters[1].Value = model.OrganCode;
			parameters[2].Value = model.CommID;
			parameters[3].Value = model.RefDate;
			parameters[4].Value = model.BussType;
			parameters[5].Value = model.BussName;
			parameters[6].Value = model.LegalPerson;
			parameters[7].Value = model.LegalPaperCode;
			parameters[8].Value = model.LicenseNum;
			parameters[9].Value = model.OrganizationCode;
			parameters[10].Value = model.RegNumber;
			parameters[11].Value = model.Province;
			parameters[12].Value = model.City;
			parameters[13].Value = model.Street;
			parameters[14].Value = model.Addr;
			parameters[15].Value = model.ZipCode;
			parameters[16].Value = model.ContactPerson;
			parameters[17].Value = model.ContactPhone;
			parameters[18].Value = model.MainBusiness;
			parameters[19].Value = model.Memo;
			parameters[20].Value = model.IsDelete;

			DbHelperSQL.RunProcedure("Proc_Tb_Management_Supplier_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.Management.Tb_Management_Supplier model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@SupplierCode", SqlDbType.VarChar,36),
					new SqlParameter("@OrganCode", SqlDbType.VarChar,36),
					new SqlParameter("@CommID", SqlDbType.VarChar,36),
					new SqlParameter("@RefDate", SqlDbType.VarChar,36),
					new SqlParameter("@BussType", SqlDbType.VarChar,10),
					new SqlParameter("@BussName", SqlDbType.VarChar,36),
					new SqlParameter("@LegalPerson", SqlDbType.VarChar,36),
					new SqlParameter("@LegalPaperCode", SqlDbType.VarChar,36),
					new SqlParameter("@LicenseNum", SqlDbType.VarChar,36),
					new SqlParameter("@OrganizationCode", SqlDbType.VarChar,36),
					new SqlParameter("@RegNumber", SqlDbType.VarChar,36),
					new SqlParameter("@Province", SqlDbType.VarChar,36),
					new SqlParameter("@City", SqlDbType.VarChar,36),
					new SqlParameter("@Street", SqlDbType.VarChar,36),
					new SqlParameter("@Addr", SqlDbType.VarChar,299),
					new SqlParameter("@ZipCode", SqlDbType.VarChar,36),
					new SqlParameter("@ContactPerson", SqlDbType.VarChar,36),
					new SqlParameter("@ContactPhone", SqlDbType.VarChar,36),
					new SqlParameter("@MainBusiness", SqlDbType.VarChar,3999),
					new SqlParameter("@Memo", SqlDbType.VarChar,3999),
					new SqlParameter("@IsDelete", SqlDbType.Int,4)};
			parameters[0].Value = model.SupplierCode;
			parameters[1].Value = model.OrganCode;
			parameters[2].Value = model.CommID;
			parameters[3].Value = model.RefDate;
			parameters[4].Value = model.BussType;
			parameters[5].Value = model.BussName;
			parameters[6].Value = model.LegalPerson;
			parameters[7].Value = model.LegalPaperCode;
			parameters[8].Value = model.LicenseNum;
			parameters[9].Value = model.OrganizationCode;
			parameters[10].Value = model.RegNumber;
			parameters[11].Value = model.Province;
			parameters[12].Value = model.City;
			parameters[13].Value = model.Street;
			parameters[14].Value = model.Addr;
			parameters[15].Value = model.ZipCode;
			parameters[16].Value = model.ContactPerson;
			parameters[17].Value = model.ContactPhone;
			parameters[18].Value = model.MainBusiness;
			parameters[19].Value = model.Memo;
			parameters[20].Value = model.IsDelete;

			DbHelperSQL.RunProcedure("Proc_Tb_Management_Supplier_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(string SupplierCode)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@SupplierCode", SqlDbType.VarChar,50)};
			parameters[0].Value = SupplierCode;

			DbHelperSQL.RunProcedure("Proc_Tb_Management_Supplier_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.Management.Tb_Management_Supplier GetModel(string SupplierCode)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@SupplierCode", SqlDbType.VarChar,50)};
			parameters[0].Value = SupplierCode;

			MobileSoft.Model.Management.Tb_Management_Supplier model=new MobileSoft.Model.Management.Tb_Management_Supplier();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_Management_Supplier_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				model.SupplierCode=ds.Tables[0].Rows[0]["SupplierCode"].ToString();
				model.OrganCode=ds.Tables[0].Rows[0]["OrganCode"].ToString();
				model.CommID=ds.Tables[0].Rows[0]["CommID"].ToString();
				model.RefDate=ds.Tables[0].Rows[0]["RefDate"].ToString();
				model.BussType=ds.Tables[0].Rows[0]["BussType"].ToString();
				model.BussName=ds.Tables[0].Rows[0]["BussName"].ToString();
				model.LegalPerson=ds.Tables[0].Rows[0]["LegalPerson"].ToString();
				model.LegalPaperCode=ds.Tables[0].Rows[0]["LegalPaperCode"].ToString();
				model.LicenseNum=ds.Tables[0].Rows[0]["LicenseNum"].ToString();
				model.OrganizationCode=ds.Tables[0].Rows[0]["OrganizationCode"].ToString();
				model.RegNumber=ds.Tables[0].Rows[0]["RegNumber"].ToString();
				model.Province=ds.Tables[0].Rows[0]["Province"].ToString();
				model.City=ds.Tables[0].Rows[0]["City"].ToString();
				model.Street=ds.Tables[0].Rows[0]["Street"].ToString();
				model.Addr=ds.Tables[0].Rows[0]["Addr"].ToString();
				model.ZipCode=ds.Tables[0].Rows[0]["ZipCode"].ToString();
				model.ContactPerson=ds.Tables[0].Rows[0]["ContactPerson"].ToString();
				model.ContactPhone=ds.Tables[0].Rows[0]["ContactPhone"].ToString();
				model.MainBusiness=ds.Tables[0].Rows[0]["MainBusiness"].ToString();
				model.Memo=ds.Tables[0].Rows[0]["Memo"].ToString();
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
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select SupplierCode,OrganCode,CommID,RefDate,BussType,BussName,LegalPerson,LegalPaperCode,LicenseNum,OrganizationCode,RegNumber,Province,City,Street,Addr,ZipCode,ContactPerson,ContactPhone,MainBusiness,Memo,IsDelete ");
			strSql.Append(" FROM Tb_Management_Supplier ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" SupplierCode,OrganCode,CommID,RefDate,BussType,BussName,LegalPerson,LegalPaperCode,LicenseNum,OrganizationCode,RegNumber,Province,City,Street,Addr,ZipCode,ContactPerson,ContactPhone,MainBusiness,Memo,IsDelete ");
			strSql.Append(" FROM Tb_Management_Supplier ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		
		/// <summary>
		/// 分页获取数据列表
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
			parameters[5].Value = "SELECT * FROM Tb_Management_Supplier WHERE 1=1 " + StrCondition;
			parameters[6].Value = "SupplierCode";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

