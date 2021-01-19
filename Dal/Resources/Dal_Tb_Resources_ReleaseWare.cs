using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.Resources
{
	/// <summary>
	/// 数据访问类Dal_Tb_Resources_ReleaseWare。
	/// </summary>
	public class Dal_Tb_Resources_ReleaseWare
	{
		public Dal_Tb_Resources_ReleaseWare()
		{
            DbHelperSQL.ConnectionString = PubConstant.SQMBSContionString;
		}
		#region  成员方法

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long ReleaseWareID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ReleaseWareID", SqlDbType.BigInt)};
			parameters[0].Value = ReleaseWareID;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_Resources_ReleaseWare_Exists",parameters,out rowsAffected);
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
        public void Add(MobileSoft.Model.Resources.Tb_Resources_ReleaseWare model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ReleaseWareID", SqlDbType.BigInt,8),
					new SqlParameter("@ReleaseID", SqlDbType.BigInt,8),
					new SqlParameter("@SourceArea", SqlDbType.NVarChar,100),
					new SqlParameter("@Factory", SqlDbType.NVarChar,50),
					new SqlParameter("@Brand", SqlDbType.NVarChar,50),
					new SqlParameter("@Version", SqlDbType.NVarChar,50),
					new SqlParameter("@Colors", SqlDbType.NVarChar,200),
					new SqlParameter("@Size", SqlDbType.NVarChar,200),
					new SqlParameter("@Weight", SqlDbType.NVarChar,50),
					new SqlParameter("@ListedData", SqlDbType.DateTime),
					new SqlParameter("@ShelfLife", SqlDbType.NVarChar,50),
					new SqlParameter("@ReleaseWareContent", SqlDbType.NText),
					new SqlParameter("@PackingList", SqlDbType.NText),
					new SqlParameter("@CustomerService", SqlDbType.NText),
					new SqlParameter("@ShippingMethod", SqlDbType.NText),
					new SqlParameter("@ReleaseWareNeedKnow", SqlDbType.NText)};
			parameters[0].Direction = ParameterDirection.Output;
			parameters[1].Value = model.ReleaseID;
			parameters[2].Value = model.SourceArea;
			parameters[3].Value = model.Factory;
			parameters[4].Value = model.Brand;
			parameters[5].Value = model.Version;
			parameters[6].Value = model.Colors;
			parameters[7].Value = model.Size;
			parameters[8].Value = model.Weight;
			parameters[9].Value = model.ListedData;
			parameters[10].Value = model.ShelfLife;
			parameters[11].Value = model.ReleaseWareContent;
			parameters[12].Value = model.PackingList;
			parameters[13].Value = model.CustomerService;
			parameters[14].Value = model.ShippingMethod;
			parameters[15].Value = model.ReleaseWareNeedKnow;

			DbHelperSQL.RunProcedure("Proc_Tb_Resources_ReleaseWare_ADD",parameters,out rowsAffected);
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.Resources.Tb_Resources_ReleaseWare model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ReleaseWareID", SqlDbType.BigInt,8),
					new SqlParameter("@ReleaseID", SqlDbType.BigInt,8),
					new SqlParameter("@SourceArea", SqlDbType.NVarChar,100),
					new SqlParameter("@Factory", SqlDbType.NVarChar,50),
					new SqlParameter("@Brand", SqlDbType.NVarChar,50),
					new SqlParameter("@Version", SqlDbType.NVarChar,50),
					new SqlParameter("@Colors", SqlDbType.NVarChar,200),
					new SqlParameter("@Size", SqlDbType.NVarChar,200),
					new SqlParameter("@Weight", SqlDbType.NVarChar,50),
					new SqlParameter("@ListedData", SqlDbType.DateTime),
					new SqlParameter("@ShelfLife", SqlDbType.NVarChar,50),
					new SqlParameter("@ReleaseWareContent", SqlDbType.NText),
					new SqlParameter("@PackingList", SqlDbType.NText),
					new SqlParameter("@CustomerService", SqlDbType.NText),
					new SqlParameter("@ShippingMethod", SqlDbType.NText),
					new SqlParameter("@ReleaseWareNeedKnow", SqlDbType.NText)};
			parameters[0].Value = model.ReleaseWareID;
			parameters[1].Value = model.ReleaseID;
			parameters[2].Value = model.SourceArea;
			parameters[3].Value = model.Factory;
			parameters[4].Value = model.Brand;
			parameters[5].Value = model.Version;
			parameters[6].Value = model.Colors;
			parameters[7].Value = model.Size;
			parameters[8].Value = model.Weight;
			parameters[9].Value = model.ListedData;
			parameters[10].Value = model.ShelfLife;
			parameters[11].Value = model.ReleaseWareContent;
			parameters[12].Value = model.PackingList;
			parameters[13].Value = model.CustomerService;
			parameters[14].Value = model.ShippingMethod;
			parameters[15].Value = model.ReleaseWareNeedKnow;

			DbHelperSQL.RunProcedure("Proc_Tb_Resources_ReleaseWare_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long ReleaseWareID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@ReleaseWareID", SqlDbType.BigInt)};
			parameters[0].Value = ReleaseWareID;

			DbHelperSQL.RunProcedure("Proc_Tb_Resources_ReleaseWare_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.Resources.Tb_Resources_ReleaseWare GetModel(long ReleaseWareID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@ReleaseWareID", SqlDbType.BigInt)};
			parameters[0].Value = ReleaseWareID;

			MobileSoft.Model.Resources.Tb_Resources_ReleaseWare model=new MobileSoft.Model.Resources.Tb_Resources_ReleaseWare();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_Resources_ReleaseWare_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ReleaseWareID"].ToString()!="")
				{
					model.ReleaseWareID=long.Parse(ds.Tables[0].Rows[0]["ReleaseWareID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ReleaseID"].ToString()!="")
				{
					model.ReleaseID=long.Parse(ds.Tables[0].Rows[0]["ReleaseID"].ToString());
				}
				model.SourceArea=ds.Tables[0].Rows[0]["SourceArea"].ToString();
				model.Factory=ds.Tables[0].Rows[0]["Factory"].ToString();
				model.Brand=ds.Tables[0].Rows[0]["Brand"].ToString();
				model.Version=ds.Tables[0].Rows[0]["Version"].ToString();
				model.Colors=ds.Tables[0].Rows[0]["Colors"].ToString();
				model.Size=ds.Tables[0].Rows[0]["Size"].ToString();
				model.Weight=ds.Tables[0].Rows[0]["Weight"].ToString();
				if(ds.Tables[0].Rows[0]["ListedData"].ToString()!="")
				{
					model.ListedData=DateTime.Parse(ds.Tables[0].Rows[0]["ListedData"].ToString());
				}
				model.ShelfLife=ds.Tables[0].Rows[0]["ShelfLife"].ToString();
				model.ReleaseWareContent=ds.Tables[0].Rows[0]["ReleaseWareContent"].ToString();
				model.PackingList=ds.Tables[0].Rows[0]["PackingList"].ToString();
				model.CustomerService=ds.Tables[0].Rows[0]["CustomerService"].ToString();
				model.ShippingMethod=ds.Tables[0].Rows[0]["ShippingMethod"].ToString();
				model.ReleaseWareNeedKnow=ds.Tables[0].Rows[0]["ReleaseWareNeedKnow"].ToString();
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
			strSql.Append("select ReleaseWareID,ReleaseID,SourceArea,Factory,Brand,Version,Colors,Size,Weight,ListedData,ShelfLife,ReleaseWareContent,PackingList,CustomerService,ShippingMethod,ReleaseWareNeedKnow ");
			strSql.Append(" FROM Tb_Resources_ReleaseWare ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string fieldOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" ReleaseWareID,ReleaseID,SourceArea,Factory,Brand,Version,Colors,Size,Weight,ListedData,ShelfLife,ReleaseWareContent,PackingList,CustomerService,ShippingMethod,ReleaseWareNeedKnow ");
			strSql.Append(" FROM Tb_Resources_ReleaseWare ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + fieldOrder);
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
			parameters[5].Value = "SELECT * FROM Tb_Resources_ReleaseWare WHERE 1=1 " + StrCondition;
			parameters[6].Value = "ReleaseWareID";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

